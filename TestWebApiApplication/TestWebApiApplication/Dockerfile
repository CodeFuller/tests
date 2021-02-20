# docker build -t test-web-api-application:latest -f Dockerfile ..
# docker tag test-web-api-application:latest codefuller/test-web-api-application:latest
# docker push codefuller/test-web-api-application:latest

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

COPY ["TestWebApiApplication/TestWebApiApplication.csproj", "/project/TestWebApiApplication/"]
RUN dotnet restore "/project/TestWebApiApplication/TestWebApiApplication.csproj"

COPY . "/project/"

RUN dotnet build "/project/TestWebApiApplication/TestWebApiApplication.csproj" --no-restore -c Release

FROM build AS publish
RUN dotnet publish "/project/TestWebApiApplication/TestWebApiApplication.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish /app

ENTRYPOINT ["dotnet", "TestWebApiApplication.dll"]
