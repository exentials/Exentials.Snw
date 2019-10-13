# Exentials.Snw
<h2> Dotnet Core 3.0 Wrapper for SAP NetWeaver RFC SDK 7.50 </h2>

<h3> Introduction </h3>

Exentials.Snw wrap SAP native libraries to allow Remote Function Call on SAP systems via dotnet core.
This is a port of the old [SnwConnector](https://archive.codeplex.com/?p=snw), with the addition
to load the native libraries based on the OS platform and the running architecture.

<h3> Prerequisites </h3>

SAP NetWeaver RFC SDK 7.50 are proprietary libraries of SAP, so I could not provide them freely, 
you need an official subscription of [SAP support portal](https://launchpad.support.sap.com/) and get them from the download center.

Once downloaded and unpacked you should place them on the proper folder /Native/Linux32

<h4> for Linux32 and 64 bits </h4>

- libicudata.so.50
- libicudecnumber.so
- libicui18n.so.50
- libicuuc.so.50
- libsapnwrfc.so
- libsapucum.so
- licenses.txt
- release_notes.txt

<h4> for Win32/64 </h4>

- icudt50.dll
- icuin50.dll
- icuuc50.dll
- libicudecnumber.dll
- libsapucum.dll
- licenses.txt
- release_notes.txt
- sapnwrfc.dll

<h4> for OSX </h4>

- libicudata.50.dylib
- libicudecnumber.dylib
- libicui18n.50.dylib
- libicuuc.50.dylib
- libsapnwrfc.dylib
- libsapucum.dylib
- licenses.txt
- release_notes.txt

<h3> API Reference </h3>

The list of RFC api are available on [SAP help](https://help.sap.com/saphelp_nw73ehp1/helpdata/en/48/a88c805134307de10000000a42189b/frameset.htm)

<h3> Notice </h3>

This is a first migration, more test are needed and a revision check of all imported functions.
Future information will be available on my blog [Exentials.net](https://www.exentials.net)
