# ProyectoDiseño

Aplicación distribuida para gestionar instrumentos de cuerda mediante servicios web GraphQL.

El proyecto está dividido en dos componentes:

- **Servidor:** Spring Boot y Java 17. Expone la lógica central y administra los instrumentos en memoria.
- **Cliente:** Windows Forms en C# sobre .NET Framework 4.8.1. Consume el servidor GraphQL.

## Estructura

```text
ProyectoDiseño/
├── POCInstrumentosMusicales/
│   └── POCInstrumentosMusicales/
│       ├── pom.xml
│       └── src/
│           ├── main/java/          # Aplicación, modelo, servicio y controlador
│           └── main/resources/
│               └── graphql/        # Esquema GraphQL
└── POCInstrumentoCuerda/
    └── POCInstrumentoCuerda/
        ├── POCInstrumentoCuerda.slnx
        └── POCInstrumentoCuerda/
            ├── POCInstrumentoCuerda.csproj
            ├── GraphQLService.cs   # Comunicación con el servidor
            ├── model/              # Modelos del cliente
            └── GUI*.cs              # Ventanas Windows Forms
```

## Requisitos

### Servidor

- Java 17 o superior.
- Maven Wrapper incluido en el proyecto (`mvnw.cmd`).
- Acceso a Internet la primera vez que Maven descargue dependencias.

### Cliente

- Visual Studio 2019 o superior.
- Carga de trabajo **Desarrollo de escritorio .NET**.
- .NET Framework 4.8.1 Developer Pack.
- Windows Forms.

El cliente utiliza las dependencias GraphQL ubicadas en la carpeta `packages`.

## Ejecutar el servidor

Abrir PowerShell en:

```powershell
cd "C:\Users\danie\Downloads\ProyectoDiseño\POCInstrumentosMusicales\POCInstrumentosMusicales"
```

Iniciar Spring Boot:

```powershell
.\mvnw.cmd spring-boot:run
```

El servidor queda disponible en:

```text
http://localhost:8081/graphql
```

También se puede consultar la interfaz GraphiQL, si está habilitada, en:

```text
http://localhost:8081/graphiql
```

No cerrar la terminal mientras se utiliza el cliente.

## Ejecutar con Docker

Requisitos:

- Docker Desktop iniciado.
- Docker Compose incluido en Docker Desktop.

Desde la raíz de `ProyectoDiseño`, levantar el servidor en segundo plano:

```powershell
docker compose up --build -d
```

El servicio GraphQL quedará disponible en `http://localhost:8081/graphql`. El cliente WinForms puede ejecutarse normalmente desde Visual Studio y continuará usando ese mismo endpoint.

Para ver los logs:

```powershell
docker compose logs -f instrumentos-server
```

Para detener el servicio:

```powershell
docker compose down
```

## Inicio automático con `start.ps1`

También puedes iniciar el servidor y el cliente desde PowerShell ejecutando el script desde la raíz del proyecto:

```powershell
.\start.ps1
```

El script realiza estas acciones:

- Verifica que Docker esté instalado y disponible en el `PATH`.
- Si Docker no está instalado, intenta instalar Docker Desktop automáticamente mediante `winget`.
- Ejecuta `docker compose up --build -d` para reconstruir la imagen cuando sea necesario.
- Descarga automáticamente las dependencias Maven durante la construcción del servidor.
- Comprueba que el cliente WinForms esté compilado.
- Inicia el cliente y abre GraphiQL en `http://localhost:8081/graphiql`.

La primera ejecución puede tardar más porque Docker y Maven deben descargar imágenes y dependencias. Si PowerShell bloquea la ejecución del script, usa `Set-ExecutionPolicy -Scope Process Bypass` como se muestra arriba.

La instalación automática requiere Windows 10/11 con `winget` disponible y puede solicitar permisos de administrador. Si `winget` no está disponible, instala Docker Desktop manualmente y vuelve a ejecutar el script.

No abras directamente `http://localhost:8081/graphql` en el navegador para hacer consultas: esa ruta es el endpoint de la API y espera solicitudes `POST`. Usa GraphiQL para escribir y ejecutar las queries.

## Levantar servidor y cliente juntos

El cliente es Windows Forms, por lo que se ejecuta en Windows fuera del contenedor. Después de compilarlo en Visual Studio, se puede iniciar el servidor Docker y abrir el cliente con un solo comando desde PowerShell:

```powershell
.\start.ps1
```

El script ejecuta `docker compose up -d` y abre `POCInstrumentoCuerda.exe` automáticamente.

En Linux, iniciar únicamente el servidor Docker con:

```bash
chmod +x start.sh
./start.sh
```

El servidor GraphQL funciona en Windows y Linux. El cliente actual es Windows Forms sobre .NET Framework, por lo que no puede abrirse como interfaz gráfica nativa en Linux; para usarlo se requiere Windows, una máquina virtual o una solución de escritorio remoto.

## Ejecutar el cliente

Abrir en Visual Studio la solución:

```text
POCInstrumentoCuerda\POCInstrumentoCuerda\POCInstrumentoCuerda.slnx
```

Si la solución no se abre correctamente, cargar directamente:

```text
POCInstrumentoCuerda\POCInstrumentoCuerda\POCInstrumentoCuerda\POCInstrumentoCuerda.csproj
```

Después:

1. Seleccionar el proyecto `POCInstrumentoCuerda`.
2. Elegir **Establecer como proyecto de inicio**.
3. Seleccionar `Debug` y `Any CPU`.
4. Ejecutar **Compilar > Recompilar solución**.
5. Presionar `F5`.

El ejecutable se genera en:

```text
POCInstrumentoCuerda\POCInstrumentoCuerda\POCInstrumentoCuerda\bin\Debug\POCInstrumentoCuerda.exe
```

El servidor Java debe estar iniciado antes de ejecutar operaciones desde el cliente.

## Funcionalidades del cliente

La ventana principal contiene el menú de navegación y las siguientes operaciones:

- **Adicionar instrumento:** registra un instrumento con ID, nombre, precio, fecha de venta, número de cuerdas y número de trastes.
- **Consultar instrumento:** busca un único instrumento por ID y muestra todos sus atributos.
- **Listar instrumentos:** obtiene todos los instrumentos y los presenta en una grilla.
- **Filtrar listado:** permite filtrar por nombre y precio máximo.
- **Actualizar instrumento:** primero busca el instrumento por ID, muestra sus datos y luego permite modificar sus atributos.
- **Eliminar instrumento:** primero busca el instrumento por ID, muestra todos sus datos y solicita confirmación antes de eliminarlo.
- **Acerca de:** muestra los integrantes y la versión de la aplicación.

Cada caso de uso se implementa en una ventana independiente.

## Contrato GraphQL actual

El esquema del servidor se encuentra en:

```text
POCInstrumentosMusicales/POCInstrumentosMusicales/src/main/resources/graphql/schema.graphqls
```

El tipo `Instrumento` contiene:

| Campo | Tipo |
|---|---|
| `id` | `ID` |
| `nombre` | `String` |
| `precio` | `Float` |
| `fechaVenta` | `String` |
| `numeroCuerdas` | `Int` |
| `numeroTrastes` | `Int` |

Operaciones disponibles:

```graphql
query {
  instrumento {
    id
    nombre
    precio
    fechaVenta
    numeroCuerdas
    numeroTrastes
  }
}
```

```graphql
query {
  instrumentoPorCodigo(codigo: 222) {
    id
    nombre
    precio
    fechaVenta
    numeroCuerdas
    numeroTrastes
  }
}
```

```graphql
mutation {
  addInstrumento(input: {
    id: 222
    nombre: "Guitarra"
    precio: 1500
    fechaVenta: "2026-09-26"
    numeroCuerdas: 6
    numeroTrastes: 20
  }) {
    id
    nombre
  }
}
```

Las mutaciones implementadas son:

- `addInstrumento`
- `delInstrumento`
- `editInstrumento`

## Prueba manual recomendada

1. Iniciar el servidor Java.
2. Abrir el cliente .NET.
3. Adicionar un instrumento válido, por ejemplo:
   - ID: `222`
   - Nombre: `Guitarra acústica`
   - Precio: `1500`
   - Número de cuerdas: `6`
   - Número de trastes: `20`
4. Consultarlo por ID.
5. Listarlo desde la grilla.
6. Probar los filtros de nombre y precio.
7. Buscarlo desde Actualizar y modificar el nombre o precio.
8. Buscarlo desde Eliminar, revisar todos los datos y confirmar.

## Solución de problemas

### No se puede conectar con GraphQL

Verificar que el servidor esté ejecutándose y que responda en:

```text
http://localhost:8081/graphql
```

El puerto utilizado por el cliente está configurado en `GraphQLService.cs`.

### Error relacionado con `.NET Framework 4.6.1`

El proyecto debe utilizar `.NET Framework 4.8.1`. En las propiedades del proyecto, revisar el framework de destino y recompilar.

### Error de `GraphQLHttpClient` o `System.Text.Json`

Revisar que el proyecto esté apuntando a `.NET Framework 4.8.1` y que las referencias de la carpeta `packages` estén restauradas.

### El ejecutable está siendo utilizado por otro proceso

Detener la depuración con el botón rojo de Visual Studio y volver a compilar. También se puede cerrar cualquier instancia abierta de `POCInstrumentoCuerda.exe` desde el Administrador de tareas.

### El ID aparece como texto

GraphQL serializa los campos de tipo `ID` como texto. El cliente ya contempla esta conversión para las operaciones de consulta, actualización y eliminación.

## Filtrado en el servidor

La operación GraphQL `instrumento` recibe los parámetros opcionales `nombre` y `precioMaximo`. El filtrado se ejecuta en `ServicioInstrumento` antes de devolver los resultados al cliente, cumpliendo la rúbrica de filtrado del lado del servidor.

## Alcance de este trabajo

El proyecto contiene un servidor Java con su colección en memoria y servicios GraphQL, además de un cliente C#/.NET que consume esos servicios.
