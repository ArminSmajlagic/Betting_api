#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Betting_api.evona_hackathon/Betting_api.evona_hackathon.csproj", "Betting_api.evona_hackathon/"]
RUN dotnet restore "Betting_api.evona_hackathon/Betting_api.evona_hackathon.csproj"
COPY . .
WORKDIR "/src/Betting_api.evona_hackathon"
RUN dotnet build "Betting_api.evona_hackathon.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Betting_api.evona_hackathon.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Betting_api.evona_hackathon.dll"]