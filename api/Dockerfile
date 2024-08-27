# Utiliza la imagen oficial de .NET SDK como base para construir el proyecto
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Establece el directorio de trabajo en el contenedor
WORKDIR /app

# Crea el nuevo proyecto web API
RUN dotnet new webapi -o MyMicroservice --no-https

# Establece el directorio de trabajo dentro del nuevo proyecto
WORKDIR /app/MyMicroservice

# Copia el archivo Program.cs al contenedor
COPY ./Program.cs /app/MyMicroservice/

RUN dotnet add package Newtonsoft.Json

# Restaura las dependencias
RUN dotnet restore

# Construye el proyecto
RUN dotnet build -c Release -o /app/build

# Publica la aplicación
RUN dotnet publish -c Release -o /app/publish

# Utiliza la imagen de .NET para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "MyMicroservice.dll"]