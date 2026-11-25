FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Heizungskonfigurator/Backend/BookLoansDb/BookLoansDb.csproj", "Heizungskonfigurator/Backend/BookLoansDb/"]
COPY ["Heizungskonfigurator/Backend/BookLoansBackend/BookLoansBackend.csproj", "Heizungskonfigurator/Backend/BookLoansBackend/"]

RUN dotnet restore "Heizungskonfigurator/Backend/BookLoansBackend/BookLoansBackend.csproj"

COPY . .
RUN dotnet publish "Heizungskonfigurator/Backend/BookLoansBackend/BookLoansBackend.csproj" \
    --configuration Release \
    --output /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "BookLoansBackend.dll"]
