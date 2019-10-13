using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Exentials.Snw.SnwConnector;

namespace Exentials.Snw.CodeGenerator
{
    public sealed class SnwCode
    {
        [SuppressMessage("Microsoft.Performance", "CA1802:UseLiteralsWhereAppropriate")]
        private static readonly string _functionNamespace = "Exentials.Snw.Functions";
        [SuppressMessage("Microsoft.Performance", "CA1802:UseLiteralsWhereAppropriate")]
        private static readonly string _structureNamespace = "Exentials.Snw.Structures";
        private readonly SnwConnection _connection;
        private readonly Dictionary<string, CodeNamespace> _classes;

        public SnwCode(SnwConnection connection)
        {
            _connection = connection;
            _classes = new Dictionary<string, CodeNamespace>();
        }

        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        private static string CamelizeName(string name, bool asField)
        {
            string[] tokens = name.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < tokens.Length; i++)
            {
                int length = tokens[i].Length;
                if (length > 1)
                    tokens[i] = tokens[i].Substring(0, 1).ToUpperInvariant() + tokens[i][1..length].ToLowerInvariant();
            }

            string result = string.Join(string.Empty, tokens);

            if (asField) return "_" + result.Substring(0, 1).ToLowerInvariant() + result.Substring(1, result.Length - 1);
            return result;
        }

        private static CodeNamespace CreateNamespace(string @namespace)
        {
            var ns = new CodeNamespace(@namespace);
            ns.Comments.Add(new CodeCommentStatement(Messages.CodeGeneration));
            ns.Imports.Add(new CodeNamespaceImport("System"));
            ns.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            ns.Imports.Add(new CodeNamespaceImport("System.Text"));
            ns.Imports.Add(new CodeNamespaceImport("Exentials.Snw"));
            ns.Imports.Add(new CodeNamespaceImport("Exentials.Snw.SnwConnector"));
            ns.Imports.Add(new CodeNamespaceImport(_functionNamespace));
            ns.Imports.Add(new CodeNamespaceImport(_structureNamespace));
            return ns;
        }

        private static CodeTypeDeclaration CreateFunctionClass(string functionName)
        {
            var functionClassName = CamelizeName(functionName, false);
            var functionClass = new CodeTypeDeclaration
                                    {
                                        Name = functionClassName,
                                        IsClass = true,
                                        TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed
                                    };
            functionClass.BaseTypes.Add(typeof(SnwFunction));

            var constructor = new CodeConstructor { Name = functionName, Attributes = MemberAttributes.Public };
            constructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(SnwConnection), "connection"));
            constructor.BaseConstructorArgs.Add(new CodePrimitiveExpression(functionName));
            constructor.BaseConstructorArgs.Add(new CodeVariableReferenceExpression("connection"));

            functionClass.Members.Add(constructor);

            return functionClass;
        }

        private static CodeTypeDeclaration AddNestedFunctionClass(CodeTypeDeclaration functionClass, Type type, string name)
        {
            string nestedClassName = name + "Parameters";
            var nestedClass = new CodeTypeDeclaration
                                  {
                                      Name = nestedClassName,
                                      TypeAttributes = TypeAttributes.Sealed | TypeAttributes.NestedPublic
                                  };
            nestedClass.BaseTypes.Add(typeof(SnwParametersContainer));

            //Code analysis attribute
            nestedClass.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(SuppressMessageAttribute)), new CodeAttributeArgument(new CodePrimitiveExpression("Microsoft.Design")), new CodeAttributeArgument(new CodePrimitiveExpression("CA1034:NestedTypesShouldNotBeVisible"))));

            var constructor = new CodeConstructor {Name = nestedClassName, Attributes = MemberAttributes.Public};
            constructor.Parameters.Add(new CodeParameterDeclarationExpression(functionClass.Name, "container"));
            constructor.BaseConstructorArgs.Add(new CodeVariableReferenceExpression("container"));
            nestedClass.Members.Add(constructor);

            var nestedReference = new CodeTypeReference(type);
            nestedReference.TypeArguments.Add(functionClass.Name + "." + nestedClassName);

            functionClass.Members.Add(nestedClass);
            functionClass.BaseTypes.Add(nestedReference);


            //
            var field = new CodeMemberField(nestedClass.Name, CamelizeName(name, true));
            functionClass.Members.Add(field);

            var property = new CodeMemberProperty
                               {
                                   Attributes = MemberAttributes.Public | MemberAttributes.Final,
                                   Type = new CodeTypeReference(nestedClass.Name),
                                   Name = name
                               };


            var condition = new CodeConditionStatement();
            var fieldReference = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name);
            condition.Condition = new CodeBinaryOperatorExpression(fieldReference, CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(null));
            condition.TrueStatements.Add(new CodeAssignStatement(fieldReference, new CodeObjectCreateExpression(nestedClass.Name, new CodeThisReferenceExpression())));
            var ret = new CodeMethodReturnStatement(fieldReference);

            property.GetStatements.Add(condition);
            property.GetStatements.Add(ret);

            functionClass.Members.Add(property);

            return nestedClass;
        }

        private static void AddPropertyGetSetParameter(CodeMemberProperty property, string name, int length, int decimals)
        {
            // Get
            var getParameter = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "GetParameter"), new CodePrimitiveExpression(name), new CodePrimitiveExpression(length), new CodePrimitiveExpression(decimals));
            getParameter.Method.TypeArguments.Add(property.Type);
            var ret = new CodeMethodReturnStatement(getParameter);
            property.GetStatements.Add(ret);
            // Set
            var setParameter = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "SetParameter"), new CodePrimitiveExpression(name), new CodePropertySetValueReferenceExpression(), new CodePrimitiveExpression(length), new CodePrimitiveExpression(decimals));
            property.SetStatements.Add(setParameter);
        }

        private static void AddParameterProperty(SnwCode codeContainer, CodeTypeDeclaration nestedClass, SnwParameterInfo parameter)
        {
            CodeTypeReference type;
            CodeMemberField field;

            if (parameter.IsStructure)
            {
                type = new CodeTypeReference(CamelizeName(parameter.TypeName, false));
            }
            else if (parameter.IsTable)
            {
                type = new CodeTypeReference(parameter.GetParameterType());
                type.TypeArguments.Add(CamelizeName(parameter.TypeName, false));
            }
            else
            {
                type = new CodeTypeReference(parameter.GetParameterType());
            }

            // public Property
            var property = new CodeMemberProperty();

            // Add Intellisense property description
            if (!string.IsNullOrEmpty(parameter.Description))
            {
                property.Comments.Add(new CodeCommentStatement("<summary>", true));
                property.Comments.Add(new CodeCommentStatement(parameter.Description, true));
                property.Comments.Add(new CodeCommentStatement("</summary>", true));
            }

            property.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            property.Type = type;
            property.Name = CamelizeName(parameter.Name, false);
            nestedClass.Members.Add(property);

            if ((parameter.RfcType == RfcType.Structure) || (parameter.RfcType == RfcType.Table))
            {
                string structureName = CamelizeName(parameter.TypeName, false);
                if (!codeContainer._classes.ContainsKey(structureName) && (!IsBuildInStructure(parameter.TypeName)))
                    codeContainer._classes.Add(structureName, CreateStructure(structureName, parameter));

                // Only get for structure or table
                // private field                
                field = new CodeMemberField(type, CamelizeName(parameter.Name, true));
                nestedClass.Members.Add(field);

                var condition = new CodeConditionStatement();
                var fieldReference = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name);
                condition.Condition = new CodeBinaryOperatorExpression(fieldReference, CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(null));

                var getParameter = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "GetParameter"), new CodePrimitiveExpression(parameter.Name), new CodePrimitiveExpression(parameter.Length), new CodePrimitiveExpression(parameter.Decimals));
                getParameter.Method.TypeArguments.Add(type);
                condition.TrueStatements.Add(new CodeAssignStatement(fieldReference, getParameter));
                var ret = new CodeMethodReturnStatement(fieldReference);

                property.GetStatements.Add(condition);
                property.GetStatements.Add(ret);
            }
            else
            {
                AddPropertyGetSetParameter(property, parameter.Name, parameter.Length, parameter.Decimals);
            }
        }

        private static void CreateNestedClass(SnwCode codeContainer, CodeTypeDeclaration functionClass, Type type, string name, IEnumerable<SnwParameterInfo> parameters)
        {
            if (parameters.Count() > 0)
            {
                CodeTypeDeclaration nestedClass = AddNestedFunctionClass(functionClass, type, name);
                foreach (var p in parameters)
                {
                    AddParameterProperty(codeContainer, nestedClass, p);
                }
            }
        }

        /// <summary>
        /// Generate class function and structures namespaces
        /// </summary>
        /// <param name="name">Name of the function to generate</param>
        public void CreateFunctionCode(string name)
        {
            var function = new SnwFunction(name, _connection);

            var functionNamespace = CreateNamespace(_functionNamespace);

            var functionClass = CreateFunctionClass(function.Name);
            functionNamespace.Types.Add(functionClass);

            var parameters = SnwMetadata.GetParameters(function);

            var import = parameters.Where(t => t.Direction == RfcDirection.Import);
            CreateNestedClass(this, functionClass, typeof(IImport<>), "Import", import);

            var export = parameters.Where(t => t.Direction == RfcDirection.Export);
            CreateNestedClass(this, functionClass, typeof(IExport<>), "Export", export);

            var tables = parameters.Where(t => t.Direction == RfcDirection.Tables);
            CreateNestedClass(this, functionClass, typeof(ITables<>), "Tables", tables);

            _classes.Add(functionClass.Name, functionNamespace);
        }

        private static CodeNamespace CreateStructure(string structureName, SnwParameterInfo parameter)
        {
            var structureNamespace = CreateNamespace(_structureNamespace);

            var structureClass = new CodeTypeDeclaration {IsClass = true, Name = structureName};
            structureClass.BaseTypes.Add(typeof(SnwStructure));
            structureClass.TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;

            structureClass.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(SnwStructureAttribute)), new CodeAttributeArgument(new CodePrimitiveExpression(parameter.TypeName))));

            var constructor = new CodeConstructor {Attributes = MemberAttributes.Public};
            constructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(SnwStructure), "structure"));
            constructor.BaseConstructorArgs.Add(new CodeVariableReferenceExpression("structure"));
            structureClass.Members.Add(constructor);

            foreach (var field in SnwMetadata.GetStructureFields(parameter))
            {
                var property = new CodeMemberProperty
                                   {
                                       Attributes = MemberAttributes.Public | MemberAttributes.Final,
                                       Type = new CodeTypeReference(field.GetFieldType()),
                                       Name = CamelizeName(field.Name, false)
                                   };
                AddPropertyGetSetParameter(property, field.Name, field.Length, field.Decimals);

                structureClass.Members.Add(property);
            }

            structureNamespace.Types.Add(structureClass);

            return structureNamespace;
        }

        private static bool IsBuildInStructure(string structureName)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            return types.SelectMany(t => t.GetCustomAttributes(typeof (SnwStructureAttribute), false).Cast<SnwStructureAttribute>()).Any(attr => attr.Name == structureName);
        }

        /// <summary>
        /// List of generated namespace items
        /// </summary>
        public IDictionary<string, CodeNamespace> Items
        {
            get { return _classes; }
        }

        /// <summary>
        /// Create source code files for all generated namespaces
        /// </summary>
        /// <param name="path">Destination file path</param>
        /// <param name="codeDomProvider">Code generator provider</param>
        /// <param name="options">Addition code generation options</param>
        public void CreateCode(string path, System.CodeDom.Compiler.CodeDomProvider codeDomProvider, System.CodeDom.Compiler.CodeGeneratorOptions options)
        {
            foreach (var cl in _classes)
            {
                var sb = new StringBuilder();
                TextWriter tw = new StringWriter(sb, CultureInfo.InvariantCulture);
                codeDomProvider.GenerateCodeFromNamespace(cl.Value, tw, options);

                File.WriteAllText(Path.Combine(path, cl.Key + "." + codeDomProvider.FileExtension), sb.ToString());
            }
        }
    }
}
