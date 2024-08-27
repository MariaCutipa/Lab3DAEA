IMAGE_NAME="mymicroservice"
CONTAINER_NAME="mymicroservice_container"

# Construye la imagen Docker
docker build -t "$IMAGE_NAME" .

# Detiene y elimina el contenedor si ya existe
docker rm -f "$CONTAINER_NAME" 2>/dev/null || true

# Ejecuta el contenedor en segundo plano y expone el puerto 5020
docker run -d --name "$CONTAINER_NAME" -p 5020:80 "$IMAGE_NAME"

# Espera un momento para asegurarse de que el contenedor está listo
sleep 5
