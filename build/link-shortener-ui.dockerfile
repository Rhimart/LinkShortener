
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["link-shortener-ui/LinkShortener.csproj", "link-shortener-ui/"]  # Adjust the path based on the actual location
RUN dotnet restore "link-shortener-ui/LinkShortener.csproj"

COPY . .

WORKDIR "/src/link-shortener-ui"

RUN dotnet build "LinkShortener.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LinkShortener.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "LinkShortener.dll"]