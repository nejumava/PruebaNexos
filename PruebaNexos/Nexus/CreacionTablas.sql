-- Creación BD
CREATE DATABASE Nexus
GO

-- Uso de la BD 
USE Nexus
GO

-- Creación tabla autor
CREATE TABLE autor(
    id int IDENTITY(1,1) NOT NULL,
	nombre varchar(100) NOT NULL,
	fecha_nacimiento date NOT NULL,
	ciudad varchar(100) NOT NULL,
	email varchar(100) NOT NULL,
	CONSTRAINT PK_autor PRIMARY KEY CLUSTERED(id),
	CONSTRAINT UN_email_autor UNIQUE (email)
)
GO


-- Creación de la tabla libro
CREATE TABLE libro(
    id int IDENTITY(1,1) NOT NULL,
	titulo varchar(255) NOT NULL,
	anio smallint NOT NULL,
	genero varchar(50) NOT NULL,
	num_paginas smallint NOT NULL,
	autor_id int NOT NULL,
	CONSTRAINT UN_titulo_libro UNIQUE (titulo),
	CONSTRAINT PK_libro PRIMARY KEY CLUSTERED(id)
)
GO

-- Creación de relaciones
ALTER TABLE libro ADD FOREIGN KEY(autor_id) REFERENCES autor(id)
GO

