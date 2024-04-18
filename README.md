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
NOTA: Se el comando update, no crea la tabla automicaticamente, se debe crear de manera manual


![image](https://github.com/krizthiam3/pruebaTekton/assets/3958240/a28c700f-fdd1-4b5a-b94d-8d0f212a62a7)


Script 1 

/****** Object:  Database [APIPrueba]    Script Date: 19/03/2024 10:53:37 p. m. ******/
CREATE DATABASE [APIPrueba]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'APIPrueba', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\APIPrueba.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'APIPrueba_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\APIPrueba_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [APIPrueba].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

Script 2

USE [APIPrueba]
GO

/****** Object:  Table [dbo].[products]    Script Date: 19/03/2024 10:53:29 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Stock] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


##Uso
Ejemplo de solicitud GET para obtener todos los elementos: GET /api/products
Ejemplo de solicitud POST para crear un nuevo elemento: POST /api/products
Ejemplo de solicitud GET para obtener todos los elementos: GET /api/products/{productId}

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
