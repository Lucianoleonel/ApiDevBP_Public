# ApiDevBP Public

## Funcionalidad requerida
Una empresa necesita un servicio en el que pueda realizar la gestión del personal. Éste debe permitirle a los usuarios la opción de consultar, crear, modificar y borrar usuarios mediante diferentes endpoints. 

Actualmente cuentan con una API para leer información y crear nuevos usuarios. 

## Repositorio Original:
https://github.com/josezamp/ApiDevBP

## Repositorio Modificado:
https://github.com/Lucianoleonel/ApiDevBP_Public.git

##Configuración técnica del servicio
.NET 8
SQLite (https://github.com/praeclarum/sqlite-net/wiki/Getting-Started)
Serilog
Swagger 

## Tareas a desarrollar
Separar la lógica del controlador
Crear los endpoint necesarios para realizar CRUD de usuarios.
Utilizar el patrón Options para configurar, por ejemplo, la cadena de conexión a la base de datos.
Implementar manejo de excepciones en, al menos, un método; que además realice logs con detalles acerca de un eventual error.
 
Tareas deseadas no excluyentes
Implementar automapper
Documentar cada endpoint
 

Importante: Realizar un fork del repositorio de GitHub y entregar la solución desde su propio perfil, junto con el/los JSON necesarios para cada endpoint. Debe configurar el repo como público para poder ser accedido.
 

Este proyecto es una API que permite gestionar usuarios, incluyendo creación, actualización, eliminación y obtención de usuarios.

## Endpoints de la API de Usuarios

### 1. Obtener todos los usuarios
**Endpoint**: `https://localhost:7016/api/Users`  
**Método**: `GET`
**Curl**: `curl -X 'GET' \
'https://localhost:7016/api/Users/1' \
-H 'accept: */*'`

### 2. Obtener un usuario por id
**Endpoint**: `https://localhost:7016/api/Users/1`  
**Método**: `GET`
**Curl**: `curl -X 'GET' \
'https://localhost:7016/api/Users//id' \
-H 'accept: */*'`

### 3. Agregar un usuario 
**Endpoint**: `https://localhost:7016/api/Users`
**Método**: `POST`
**Request Body**: `{
  "id": 3,
  "name": "string",
  "lastname": "string"
}`
**Curl**: `curl -X 'POST' \
  'https://localhost:7016/api/Users' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 3,
  "name": "string",
  "lastname": "string"
}'`

### 4. Actualizar un usuario 
**Endpoint**: `https://localhost:7016/api/Users/id`
**Método**: `PUT`
**Request Body**: `{
  "id": 3,
  "name": "string",
  "lastname": "string"
}`
**Curl**: `curl -X 'PUT' \
  'https://localhost:7016/api/Users/id' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
      "id": 1,
      "name": "Name",
      "lastname": "LastName"
    }'`


## Proyecto

**ApiDevBP/ApplicationServices**

Contiene la clase UserApplicationService que realiza las invocaciones a la Capa de Repositorio y tambien contiene validaciones de negocio.

**ApiDevBP/Configuration**

Contiene la clase ConfigurationDB que es la que se utiliza para la configuracion del Patron Option 

**ApiDevBP/Controllers**

Punto de entrada de la Aplicacion que contiene las operaciones CRUD del User

**ApiDevBP/Docs/User**

Contiene los ejemplos de Request y Response

**ApiDevBP/DTO**

Contiene la clase UserDTo que se utilizo para realizar un Mapper Custom

**ApiDevBP/Entities**

Contiene la clase UserEntity que genera la tabla en la base de datos

**ApiDevBP/Exceptions**

Contiene la clase UserException que se utiliza para lanzar excepciones en las validaciones

**ApiDevBP/Infrastructure**

Contiene la clase UserRepository que es la encargada de hacer el ingreso de los datos a la Base de Datos

**ApiDevBP/Mappers**

Contiene la clase MappingProfile que configura el autoMapper

**ApiDevBP/Repositories**

Contiene la interfaz que son los contratos que tiene que implementar en la carpeta Infrastructure

**ApiDevBP/Validations**

Se realizan las validaciones de la entidad
