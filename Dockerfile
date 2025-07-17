FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build

WORKDIR /build

COPY FamilyApp.sln .

COPY FamilyApp/FamilyApp.csproj FamilyApp/

RUN dotnet restore

COPY FamilyApp/ FamilyApp/

RUN dotnet publish FamilyApp -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine

WORKDIR /app

COPY --from=build /build/out .

CMD ["dotnet", "FamilyApp.dll"]