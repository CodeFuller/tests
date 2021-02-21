FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
EXPOSE 80
EXPOSE 81

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

COPY ["TestWebApiApplication/TestWebApiApplication.csproj", "/project/TestWebApiApplication/"]
COPY ["TestWebApiApplication.Shared/TestWebApiApplication.Shared.csproj", "/project/TestWebApiApplication.Shared/"]
RUN dotnet restore "/project/TestWebApiApplication/TestWebApiApplication.csproj"

COPY . "/project/"

RUN dotnet build "/project/TestWebApiApplication/TestWebApiApplication.csproj" --no-restore -c Release

FROM build AS publish
RUN dotnet publish "/project/TestWebApiApplication/TestWebApiApplication.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish /app

ENTRYPOINT ["dotnet", "TestWebApiApplication.dll"]
