FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["APIs/MongoDb.API/MongoDb.API.csproj", "MongoDb.API/"]
RUN dotnet restore "./MongoDb.API/MongoDb.API.csproj"

COPY . .
WORKDIR "/src/MongoDb.API"
RUN dotnet build "MongoDb.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MongoDb.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MongoDb.API.dll"]
