FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NotaFiscalEstoque.API/NotaFiscalEstoque.API.csproj", "NotaFiscalEstoque.API/"]
RUN dotnet restore "NotaFiscalEstoque.API/NotaFiscalEstoque.API.csproj"
COPY . .
WORKDIR "/src/NotaFiscalEstoque.API"
RUN dotnet build "NotaFiscalEstoque.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NotaFiscalEstoque.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotaFiscalEstoque.API.dll"]
