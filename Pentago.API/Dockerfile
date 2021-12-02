FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Pentago.API/Pentago.API.csproj", "Pentago.API/"]
RUN dotnet restore "Pentago.API/Pentago.API.csproj"
COPY . .
WORKDIR "/src/Pentago.API"
RUN dotnet build "Pentago.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pentago.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pentago.API.dll"]
