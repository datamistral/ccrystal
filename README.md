Make sure that SAP Crystal runtime for .NET Framework is installed on the local computer and it is the right type (32 or 64 bits)
Compile project CCrystal.csproj using Visual Studio version 2019 (or above) or by running one of the bat files in this folder:
compile_x86.bat     (to compile x86 application)
compile_AnyCPU.bat  (to compile AnyCPU application)

The compiled assemblies will be placed in subfolder CCrystal\Release_x86 or CCrystal\Release_AnyCPU

Check the subfolder for log file named CCrystal.log and make sure that at the bottom you see
    0 Error(s)

If there are no errors, copy ALL assemblies from the compiled folder to the folder where you will use them. 

Most of the time AnyCPU can be used for both 32 and 64 bit applications.