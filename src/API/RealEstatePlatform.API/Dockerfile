FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Directory.Build.props ./
COPY RealEstatePlatform.sln ./
COPY src ./src
COPY tests ./tests

RUN dotnet restore RealEstatePlatform.sln
RUN dotnet publish src/API/RealEstatePlatform.API/RealEstatePlatform.API.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

ENV ASPNETCORE_URLS=http://0.0.0.0:10000
ENV PORT=10000

EXPOSE 10000
ENTRYPOINT ["dotnet", "RealEstatePlatform.API.dll"]
