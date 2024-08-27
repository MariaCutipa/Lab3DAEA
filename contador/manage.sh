#!/bin/bash

# Construir la imagen Docker
docker build -t blazor-app .

# Ejecutar el contenedor con la aplicación Blazor
docker run -it -p 5000:5000 blazor-app

