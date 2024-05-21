#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/Amazon.Payment.Api/Amazon.Payment.Api.csproj", "src/Presentation/Amazon.Payment.Api/"]
COPY ["src/Application/Amazon.Payment.Business/Amazon.Payment.Business.csproj", "src/Application/Amazon.Payment.Business/"]
COPY ["src/Domain/Amazon.Payment.Domain/Amazon.Payment.Domain.csproj", "src/Domain/Amazon.Payment.Domain/"]
COPY ["src/Infrastructure/Amazon.Payment.Sql/Amazon.Payment.Sql.csproj", "src/Infrastructure/Amazon.Payment.Sql/"]
RUN dotnet restore "./src/Presentation/Amazon.Payment.Api/Amazon.Payment.Api.csproj"
COPY . .
WORKDIR "/src/src/Presentation/Amazon.Payment.Api"
RUN dotnet build "./Amazon.Payment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Amazon.Payment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Amazon.Payment.Api.dll"]