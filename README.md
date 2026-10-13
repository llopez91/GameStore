# GameStore

GameStore es una API REST minimalista para consultar y administrar videojuegos. Está construida con ASP.NET Core Minimal APIs sobre .NET 10.

## Requisitos

- .NET SDK 10

## Ejecutar la API

Desde la raíz del repositorio:

```bash
dotnet restore GameStore.slnx
dotnet run --project GameStore.Api
```

El perfil HTTP de desarrollo inicia la API en `http://localhost:5204`. También está disponible el perfil HTTPS configurado en `GameStore.Api/Properties/launchSettings.json`.

Al iniciar, Entity Framework Core aplica las migraciones a SQLite. La cadena de conexión predeterminada crea `GameStore.Api/GameStore.db`.

## Endpoints

La API expone los siguientes endpoints:

| Método | Ruta | Descripción |
| --- | --- | --- |
| `GET` | `/games` | Devuelve todos los videojuegos. |
| `GET` | `/games/{id}` | Devuelve un videojuego por ID; responde `404` si no existe. |
| `POST` | `/games` | Agrega un videojuego. |
| `PUT` | `/games/{id}` | Reemplaza los datos de un videojuego; responde `404` si no existe. |
| `DELETE` | `/games/games/{id}` | Elimina videojuegos con ese ID. |

Ejemplo para obtener la colección:

```bash
curl http://localhost:5204/games
```

Ejemplo para crear un videojuego:

```bash
curl -X POST http://localhost:5204/games \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Super Mario Bros.",
    "genre": "Platform",
    "price": 29.99,
    "releaseDate": "1985-09-13"
  }'
```

Los campos `name` y `genre` son obligatorios, con longitudes máximas de 50 y 20 caracteres respectivamente. `price` debe estar entre 1 y 100. Las fechas usan el formato `YYYY-MM-DD`.

## Estado de los datos

Los endpoints actualmente leen y modifican una colección en memoria. Por eso, los cambios realizados mediante `POST`, `PUT` o `DELETE` no se conservan al reiniciar la aplicación. SQLite, las migraciones y la inicialización de géneros están configuradas, pero todavía no se usan para almacenar los videojuegos gestionados por los endpoints.

## Estructura

```text
GameStore.Api/
├── Data/       # Contexto, configuración y migraciones de Entity Framework Core
├── Dtos/       # Modelos de entrada y salida de la API
├── Endpoints/  # Rutas HTTP de videojuegos
└── Models/     # Entidades Game y Genre
```
