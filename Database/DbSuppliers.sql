CREATE DATABASE DbSuppliers;

USE DbSuppliers;

CREATE TABLE Suppliers
(
	EmpresaId			 INT PRIMARY KEY IDENTITY(1,1),
	NombreEmpresa		 VARCHAR(100),
	PersonaRepresentante VARCHAR(100),
	RNC					 VARCHAR(15),
	Direccion			 VARCHAR(500),
	Telefono			 VARCHAR(15),
	EsProveedor			 BIT,
	RPE					 VARCHAR(50),
	Borrado				 BIT
)