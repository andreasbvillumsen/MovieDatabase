FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY ./MovieDatabase/bin/Debug/net5.0/ .
ENTRYPOINT ["dotnet", "MovieDatabase.dll"]