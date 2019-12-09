FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

# Copy everything else and build

WORKDIR /app/D.MoveOn.Common
COPY ../D.MoveOn.Commo\ .

WORKDIR /app/JD.MoveOn.Product
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app/JD.MoveOn.Product
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "JD.MoveOn.Product.dll"]
