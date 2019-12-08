FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["D.MoveOn.FE.Management/D.MoveOn.FE.Management.csproj", "D.MoveOn.FE.Management/"]
RUN dotnet restore "D.MoveOn.FE.Management/D.MoveOn.FE.Management.csproj"
COPY . .
WORKDIR "/src/D.MoveOn.FE.Management"
RUN dotnet build "D.MoveOn.FE.Management.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "D.MoveOn.FE.Management.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "D.MoveOn.FE.Management.dll"]