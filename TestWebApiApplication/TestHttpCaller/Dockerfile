FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

COPY ["TestHttpCaller/TestHttpCaller.csproj", "/project/TestHttpCaller/"]
COPY ["TestWebApiApplication.Shared/TestWebApiApplication.Shared.csproj", "/project/TestWebApiApplication.Shared/"]
RUN dotnet restore "/project/TestHttpCaller/TestHttpCaller.csproj"

COPY . "/project/"

RUN dotnet build "/project/TestHttpCaller/TestHttpCaller.csproj" --no-restore -c Release

FROM build AS publish
RUN dotnet publish "/project/TestHttpCaller/TestHttpCaller.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish /app

ENTRYPOINT ["dotnet", "TestHttpCaller.dll"]
