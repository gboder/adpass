# Description
This guide describes the steps to publish the applications. It is more a recall of the command to use.

Assumes you're runing in PowerShell

#Build and pack
For windows
>dotnet publish --self-contained -c release -r win-x64

#Packing all file in ZIP
>Compress-Archive .\AdPass\bin\Release\netcoreapp2.2\win-x64\publish\ releases\adpass_0.1.zip

#Copy, unpack
You know how to it

#Other-way round
\\v-app-1\webapps\apps\adpass\prod
stop-service w3svc
robocopy .\AdPass\bin\Release\netcoreapp2.2\win-x64\publish\ \\v-app-1\webapps\apps\adpass\prod
start-service w3svc