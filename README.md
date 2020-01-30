# Ixs DNA Framework
A cross-platform base framework useful for all projects that use .Net Core

# Installing Ixs DNA Framework

You can install `Ixs.DNA.Framework` from your Visual Studio projects `Manage NuGet Packages` dialog and search for `Ixs.DNA.Framework`

Alternatively from **Package Manager** you can do `Install-Package Ixs.DNA.Framework`

From **.Net CLI** you can do `dotnet add package Ixs.DNA.Framework`

From **Paket CLI** you can do `paket add Ixs.DNA.Framework`

# Publishing New Package

To publish a new package:

- Update the `Project > Properties > Package > Package Version` 
- Change `Configuration` to `Release`
- Right-click project and select `Pack`
- Go to output folder `bin\Release` and you should see a `Ixs.DNA.Framework.x.x.x.nupkg`
- Start a `cmd` in that folder and type: `dotnet nuget push Ixs.DNA.Framework.x.x.x.nupkg -k yournugetkey -s https://api.nuget.org/v3/index.json`