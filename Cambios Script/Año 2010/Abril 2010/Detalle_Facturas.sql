/*
   Miércoles, 21 de Abril de 201006:33:19 p.m.
   Usuario: 
   Servidor: JUAN\SQL2005
   Base de datos: SistemaFacturacionBuiler
   Aplicación: 
*/

/* Para evitar posibles problemas de pérdida de datos, debe revisar esta secuencia de comandos detalladamente antes de ejecutarla fuera del contexto del diseñador de base de datos.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Detalle_Facturas
	DROP CONSTRAINT FK_Detalle_Facturas_Productos
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Detalle_Facturas
	DROP CONSTRAINT FK_Detalle_Facturas_Facturas
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Detalle_Facturas
	DROP CONSTRAINT DF_Detalle_Facturas_TasaCambio
GO
CREATE TABLE dbo.Tmp_Detalle_Facturas
	(
	id_Detalle_Factura numeric(18, 0) NOT NULL IDENTITY (1, 1),
	Numero_Factura nvarchar(50) NOT NULL,
	Fecha_Factura smalldatetime NOT NULL,
	Tipo_Factura nvarchar(50) NOT NULL,
	Cod_Producto nvarchar(50) NULL,
	Descripcion_Producto nvarchar(MAX) NULL,
	Cantidad float(53) NULL,
	Precio_Unitario float(53) NULL,
	Descuento float(53) NULL,
	Precio_Neto float(53) NULL,
	Importe float(53) NULL,
	TasaCambio float(53) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Detalle_Facturas ADD CONSTRAINT
	DF_Detalle_Facturas_TasaCambio DEFAULT ((0)) FOR TasaCambio
GO
SET IDENTITY_INSERT dbo.Tmp_Detalle_Facturas ON
GO
IF EXISTS(SELECT * FROM dbo.Detalle_Facturas)
	 EXEC('INSERT INTO dbo.Tmp_Detalle_Facturas (id_Detalle_Factura, Numero_Factura, Fecha_Factura, Tipo_Factura, Cod_Producto, Cantidad, Precio_Unitario, Descuento, Precio_Neto, Importe, TasaCambio)
		SELECT id_Detalle_Factura, Numero_Factura, Fecha_Factura, Tipo_Factura, Cod_Producto, Cantidad, Precio_Unitario, Descuento, Precio_Neto, Importe, TasaCambio FROM dbo.Detalle_Facturas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Detalle_Facturas OFF
GO
DROP TABLE dbo.Detalle_Facturas
GO
EXECUTE sp_rename N'dbo.Tmp_Detalle_Facturas', N'Detalle_Facturas', 'OBJECT' 
GO
ALTER TABLE dbo.Detalle_Facturas ADD CONSTRAINT
	PK_Detalle_Facturas PRIMARY KEY CLUSTERED 
	(
	id_Detalle_Factura,
	Numero_Factura,
	Fecha_Factura,
	Tipo_Factura
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Detalle_Facturas ADD CONSTRAINT
	FK_Detalle_Facturas_Facturas FOREIGN KEY
	(
	Numero_Factura,
	Fecha_Factura,
	Tipo_Factura
	) REFERENCES dbo.Facturas
	(
	Numero_Factura,
	Fecha_Factura,
	Tipo_Factura
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Detalle_Facturas ADD CONSTRAINT
	FK_Detalle_Facturas_Productos FOREIGN KEY
	(
	Cod_Producto
	) REFERENCES dbo.Productos
	(
	Cod_Productos
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
