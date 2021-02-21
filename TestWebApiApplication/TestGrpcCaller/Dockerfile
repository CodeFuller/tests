FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

COPY ["TestGrpcCaller/TestGrpcCaller.csproj", "/project/TestGrpcCaller/"]
COPY ["TestWebApiApplication.Shared/TestWebApiApplication.Shared.csproj", "/project/TestWebApiApplication.Shared/"]
RUN dotnet restore "/project/TestGrpcCaller/TestGrpcCaller.csproj"

COPY . "/project/"

RUN dotnet build "/project/TestGrpcCaller/TestGrpcCaller.csproj" --no-restore -c Release

FROM build AS publish
RUN dotnet publish "/project/TestGrpcCaller/TestGrpcCaller.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish /app

ENTRYPOINT ["dotnet", "TestGrpcCaller.dll"]
