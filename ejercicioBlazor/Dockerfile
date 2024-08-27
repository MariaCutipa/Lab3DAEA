# Usar la imagen base del SDK de .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Establecer el directorio de trabajo dentro del contenedor
WORKDIR /app

# Crear una nueva aplicación Blazor
RUN dotnet new blazor -o BlazorApp

# Crear un nuevo componente Razor 'Todo'
RUN dotnet new razorcomponent -n Todo -o BlazorApp/Components/Pages

# Copiar los archivos locales necesarios al contenedor
COPY ./Home.razor ./BlazorApp/Components/Pages/Home.razor
COPY ./Counter.razor ./BlazorApp/Components/Pages/Counter.razor
COPY ./Todo.razor ./BlazorApp/Components/Pages/Todo.razor
COPY ./TodoItem.cs ./BlazorApp/TodoItem.cs
COPY ./NavMenu.razor ./BlazorApp/Components/Layout/NavMenu.razor

# Exponer el puerto en el que correrá la aplicación
EXPOSE 5000

# Cambiar al directorio de la aplicación Blazor
WORKDIR /app/BlazorApp

# Ejecutar la aplicación usando dotnet run
CMD ["dotnet", "run", "--urls", "http://0.0.0.0:5000"]

