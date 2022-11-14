# Hbx
Unity toolkit


Notes
To generate xml documentation in VS Comunity for Mac need to open the Hbx.csproj file and add

  <PropertyGroup>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <DocumentationFile>Docs\Hbx.XML</DocumentationFile>
    <NoWarn>$(NoWarn);1591</NoWarn>
  </PropertyGroup>
