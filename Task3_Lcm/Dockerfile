FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

# PROJEYİ AÇIKÇA BELİRT
RUN dotnet publish Task3_Lcm.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:$PORT

ENTRYPOINT ["dotnet", "Task3_Lcm.dll"]