FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src

COPY *.csproj .
RUN dotnet restore 

COPY . .
RUN dotnet publish -c release -o /app --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AdvertApi.Search.Api.dll"]