# API - Prueba API Smart
Solución ejercicio vacante (24007) Desarrollador Backend

## Tecnologías Utilizadas
- ASP.NET Core
- C#
- Entity Framework Core
- SQL Server
- Patron de Diseño Patter Repository
- Arquitectura Limpia y bueenas practicas.

## Instalación y Requisitos

1. Clona el repositorio: git clone https://github.com/krizthiam3/APIPruebaSmart.git
2. Abrir la solucion del proyecto con visual studio ID
3. Restaura los paquetes NuGet: con el comnado : dotnet restore
4. Configura la cadena de conexión a la base de datos en appsettings.json

## Instalación de BASE de DATOS
NOTA: Se  debe crear de manera manual una base de datos co el nombre APIPruebaSmart

PASO 1

![image](https://github.com/krizthiam3/APIPruebaSmart/assets/3958240/b032ecd5-0728-4077-bace-e670ee006e13)

PASO 2 
Ejecutar el script adjunto, para la creacion de las tablas e insertar los datos. 

PASO 3

Configurar la cadena de conexion hacia la base de datos, con los parametros adecuados, detntro del archivo de configuracion: appsettings.json

![image](https://github.com/krizthiam3/APIPruebaSmart/assets/3958240/9b4b4346-5f28-4373-b705-cf728294488a)



##Docuemntacion API

![image](https://github.com/krizthiam3/APIPruebaSmart/assets/3958240/c664f34f-fcd0-463b-9985-59041bae039c)

![image](https://github.com/krizthiam3/APIPruebaSmart/assets/3958240/5862d778-0fa7-40af-a2dd-5766e394ebb0)

![image](https://github.com/krizthiam3/APIPruebaSmart/assets/3958240/7ae19c4c-0a07-47f8-95e4-5c3f20690b17)





##Estructura del Proyecto
Estructura del proyecto y los principales archivos y carpetas. :


## Patron de Diseño 
- Repository Pattern 

![image](https://github.com/krizthiam3/pruebaTekton/assets/3958240/c6a385c9-eda0-470d-be2f-01ffaa50535c)


## ARquitectura N-capas

1. Capa de Presentación (Interfaz de Usuario):
2. Capa de Lógica de Aplicación (o Capa de Negocio):
3. Capa de Acceso a Datos:
5. Capa de Pruebas:

   ![image](https://github.com/krizthiam3/pruebaTekton/assets/3958240/5a388f94-5a8a-46f0-9b8b-ff49baeb46cb)




# Swagger
Para obtener mas información y testear el servicio pude acceder al swagger : http://localhost:5101/swagger/index.html
