UPDATE Detalle_Compras
   SET Detalle_Compras.Descripcion_Producto = prod.Descripcion_Producto
FROM         Productos AS prod INNER JOIN
                      Detalle_Compras AS DCompras ON prod.Cod_Productos = DCompras.Cod_Producto 