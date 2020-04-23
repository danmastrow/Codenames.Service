FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Codenames.Service.csproj", ""]
RUN dotnet restore "./Codenames.Service.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Codenames.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Codenames.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Codenames.Service.dll"]