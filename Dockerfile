FROM mcr.microsoft.com/dotnet/aspnet:10.0 as runtime
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 as build
WORKDIR /src

COPY ["CharacterService/CharacterService.csproj","CharacterService/"]
COPY ["CharacterService.Tests/CharacterService.Tests.csproj","CharacterService.Tests/"]

RUN dotnet restore "CharacterService/CharacterService.csproj"

COPY . .
WORKDIR "/src/CharacterService"
RUN dotnet publish "CharacterService.csproj" -c Release -o /app/publish

FROM runtime as final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet","CharacterService.dll"]
