/*
   domingo, 12 de julio de 201504:24:58 p.m.
   Usuario: 
   Servidor: JUAN\SQL2012
   Base de datos: SistemaFacturacionRevetsa
   Aplicación: 
*/

/* Para evitar posibles problemas de pérdida de datos, debe revisar este script detalladamente antes de ejecutarlo fuera del contexto del diseñador de base de datos.*/
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
ALTER TABLE dbo.Compras ADD
	CodigoProyecto nvarchar(50) NULL
GO
ALTER TABLE dbo.Compras SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
