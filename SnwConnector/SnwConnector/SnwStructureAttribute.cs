using System;

namespace Exentials.Snw.SnwConnector
{
    /// <summary>
    /// Indicate the name to use for serialize/deserialize a structure
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class SnwStructureAttribute : Attribute
    {
        private readonly string _name;

        public SnwStructureAttribute(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
