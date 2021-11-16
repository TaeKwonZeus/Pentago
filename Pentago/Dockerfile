FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Pentago/Pentago.csproj", "Pentago/"]
RUN dotnet restore "Pentago/Pentago.csproj"
COPY . .
WORKDIR "/src/Pentago"
RUN dotnet build "Pentago.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pentago.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pentago.dll"]
