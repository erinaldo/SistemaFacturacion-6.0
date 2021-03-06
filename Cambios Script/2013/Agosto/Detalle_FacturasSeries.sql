/*
   jueves, 29 de agosto de 201306:29:11 a.m.
   Usuario: 
   Servidor: JUAN\SQL2005
   Base de datos: SistemaFacturacionSystemsAndSolutions
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
CREATE TABLE dbo.Detalle_FacturasSeries
	(
	Id_Series numeric(18, 0) NOT NULL,
	Numero_Factura nvarchar(50) NOT NULL,
	Fecha_Factura smalldatetime NOT NULL,
	Tipo_Factura nvarchar(50) NOT NULL,
	Cod_Producto nvarchar(50) NULL,
	NSerie nvarchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Detalle_FacturasSeries ADD CONSTRAINT
	PK_Detalle_FacturasSeries PRIMARY KEY CLUSTERED 
	(
	Id_Series,
	Numero_Factura,
	Fecha_Factura,
	Tipo_Factura
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
