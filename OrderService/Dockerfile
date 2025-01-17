FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80

# copy all csproj files and restore as distinct layers; helps at caching
COPY Emag.sln Emag.sln
COPY Emag/Emag.csproj Emag/Emag.csproj
COPY Carts/CartsService/CartsService.csproj Carts/CartsService/CartsService.csproj
COPY GatewayService/GatewayService.csproj GatewayService/GatewayService.csproj
COPY Contracts/Contracts.csproj Contracts/Contracts.csproj
COPY UserService/UserService.csproj UserService/UserService.csproj
COPY OrderService/OrderService.csproj OrderService/OrderService.csproj

# Restore package deps
RUN dotnet restore Emag.sln

# Copy the app folders
COPY OrderService OrderService
COPY Contracts Contracts
WORKDIR /app/OrderService
RUN dotnet publish -c Release -o /app/out

#Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "OrderService.dll"]
