FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["D.MoveOn.Notification/D.MoveOn.Notification.csproj", "D.MoveOn.Notification/"]
RUN dotnet restore "D.MoveOn.Notification/D.MoveOn.Notification.csproj"
COPY . .
WORKDIR "/src/D.MoveOn.Notification"
RUN dotnet build "D.MoveOn.Notification.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "D.MoveOn.Notification.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "D.MoveOn.Notification.dll"]