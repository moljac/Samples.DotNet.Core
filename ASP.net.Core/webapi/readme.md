# Readme

https://github.com/dotnet/templating/wiki/Available-templates-for-dotnet-new


    rm -fr web/
    rm -fr mvc/
    rm -fr webapi/
    
    dotnet new web \
            --output web/
    dotnet new mvc \
            --output mvc/
    dotnet new webapi \
            --output webapi/


Default

    web
    mvc
    webapi

ASP.net Core templates

    dotnet new --install Microsoft.AspNetCore.SpaTemplates::*
        dotnent --version 
            1.4
                aurelia
                konckout
                vue
        dotnent --version 
            2.0
    dotnet new -i "Boilerplate.Templates::*"
    dotnet new -i "cloudscribe.templates::*"

Installed 

    

