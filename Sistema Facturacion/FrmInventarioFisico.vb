Public Class FrmInventarioFisico
    Public MiConexion As New SqlClient.SqlConnection(Conexion)
    Public DataSet As New DataSet, Numero_Repesaje As String, PesoBruto As Double = 0, NumeroTemporal As Double, Random As New Random()
    Public MiConexionExcel As New OleDb.OleDbConnection
    Public DataAdapterExcel As New OleDb.OleDbDataAdapter
    Public MiDataSet As New DataSet()
    Public MiEnlazador As New BindingSource
    Public ConexionExcel As String
    Public RutaBD As String

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Me.Close()
    End Sub

    Private Sub FrmInventarioFisico_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Bloqueo(Me, Acceso, "Inventario Fisico")
    End Sub

    Private Sub FrmInventarioFisico_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim SQLTasa As String, SQlString As String
        Dim DataAdapter As New SqlClient.SqlDataAdapter

        If Quien = "Repesaje" Then
            Me.CmdNuevo.Visible = True
        Else
            Me.CmdNuevo.Visible = False
        End If

        SQLString = "SELECT  * FROM   Bodegas"
        MiConexion.Open()
        DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
        DataSet.Reset()
        DataAdapter.Fill(DataSet, "Bodegas")
        Me.CboCodigoBodega.DataSource = DataSet.Tables("Bodegas")


        SQLTasa = "SELECT *  FROM InventarioFisico WHERE (Activo = 1)"
        DataAdapter = New SqlClient.SqlDataAdapter(SQLTasa, MiConexion)
        DataAdapter.Fill(DataSet, "Consultas")
        If DataSet.Tables("Consultas").Rows.Count <> 0 Then
            Me.DTPFechaConteo.Value = DataSet.Tables("Consultas").Rows(0)("Fecha_Conteo")
            If Not IsDBNull(DataSet.Tables("Consultas").Rows(0)("CodBodega")) Then
                Me.CboCodigoBodega.Text = DataSet.Tables("Consultas").Rows(0)("CodBodega")
            End If
        End If
        Me.BindingInventario.DataSource = DataSet.Tables("Consultas")


        Me.TrueDBGridConsultas.DataSource = Me.BindingInventario
        MiConexion.Close()

        Me.TrueDBGridConsultas.Columns(0).Caption = "Codigo"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(0).Locked = True
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(1).Visible = False
        Me.TrueDBGridConsultas.Columns(2).Caption = "Fecha Conteo"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(2).Visible = False
        Me.TrueDBGridConsultas.Columns(3).Caption = "Descripcion"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(3).Width = 250
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(3).Locked = True
        Me.TrueDBGridConsultas.Columns(4).Caption = "Existencia"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(4).Width = 100
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(4).Locked = True
        Me.TrueDBGridConsultas.Columns(5).Caption = "Cantidad Contada"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(5).Width = 100
        Me.TrueDBGridConsultas.Columns(6).Caption = "Agotado"
        Me.TrueDBGridConsultas.Columns(7).Caption = "Contabilizado"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(7).Visible = False
        Me.TrueDBGridConsultas.Columns(8).Caption = "Activo"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(8).Visible = False

        If Not DataSet.Tables("Consultas").Rows.Count = 0 Then
            Me.DTPFechaConteo.Enabled = False
            Me.CmdIniciar.Enabled = False
            Me.CboCodigoBodega.Enabled = False
        End If
    End Sub

    Private Sub CmdIniciar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdIniciar.Click
        Dim iPosicionFila As Double, FechaConteo As String, Fecha As String
        Dim Sql As String
        Dim DataAdapter As New SqlClient.SqlDataAdapter
        Dim CodProductos As String, DescripcionProducto As String, Existencia As Double
        Dim StrSQLUpdate As String, ComandoUpdate As New SqlClient.SqlCommand, CodBodega As String
        Dim iResultado As Integer, SQlString As String
        MiConexion.Close()

        If Me.CboCodigoBodega.Text = "" Then
            MsgBox("Debe Seleccionar una Bodega", MsgBoxStyle.Critical, "Sistema de Facturacion")
            Exit Sub
        End If

        CodBodega = Me.CboCodigoBodega.Text

        Sql = "SELECT Productos.Cod_Productos, Productos.Descripcion_Producto, Productos.Tipo_Producto, DetalleBodegas.Existencia FROM Productos INNER JOIN DetalleBodegas ON Productos.Cod_Productos = DetalleBodegas.Cod_Productos WHERE (DetalleBodegas.Cod_Bodegas = '" & Me.CboCodigoBodega.Text & "')"

        MiConexion.Open()
        DataAdapter = New SqlClient.SqlDataAdapter(Sql, MiConexion)
        DataAdapter.Fill(DataSet, "ListaProductos")
        MiConexion.Close()
        If Not DataSet.Tables("ListaProductos").Rows.Count = 0 Then
            iPosicionFila = 0
            Me.ProgressBar.Minimum = 0
            Me.ProgressBar.Visible = True
            Me.ProgressBar.Value = 0
            Me.ProgressBar.Maximum = DataSet.Tables("ListaProductos").Rows.Count
            FechaConteo = Format(Me.DTPFechaConteo.Value, "yyyy-MM-dd")
            Fecha = Format(Me.DTPFechaConteo.Value, "dd/MM/yyyy")
            Do While iPosicionFila < (DataSet.Tables("ListaProductos").Rows.Count)
                My.Application.DoEvents()

                CodProductos = Trim(DataSet.Tables("ListaProductos").Rows(iPosicionFila)("Cod_Productos"))



                DescripcionProducto = Replace(Trim(DataSet.Tables("ListaProductos").Rows(iPosicionFila)("Descripcion_Producto")), "'", " ")
                If Not IsDBNull(DataSet.Tables("ListaProductos").Rows(iPosicionFila)("Existencia")) Then
                    'Existencia = Trim(DataSet.Tables("ListaProductos").Rows(iPosicionFila)("Existencia_Unidades"))
                    Existencia = BuscaExistenciaBodega(CodProductos, CodBodega)
                End If
                Sql = "SELECT  *  FROM InventarioFisico WHERE (Cod_Producto = '" & CodProductos & "') AND (Fecha_Conteo = CONVERT(DATETIME, '" & FechaConteo & "', 102)) AND (Activo = 0) AND (CodBodega = '" & Me.CboCodigoBodega.Text & "') ORDER BY Cod_Producto"
                DataAdapter = New SqlClient.SqlDataAdapter(Sql, MiConexion)

                MiConexion.Open()
                DataAdapter.Fill(DataSet, "InventarioFisico")
                DataAdapter = Nothing
                MiConexion.Close()

                If DataSet.Tables("InventarioFisico").Rows.Count = 0 Then
                    MiConexion.Open()
                    StrSQLUpdate = "INSERT INTO [InventarioFisico] ([Cod_Producto],[CodBodega],[Fecha_Conteo],[Descripcion],[Existencia_Unidades]) " & _
                                   "VALUES('" & CodProductos & "','" & Me.CboCodigoBodega.Text & "','" & Fecha & "','" & DescripcionProducto & "','" & Existencia & "')"
                    ComandoUpdate = New SqlClient.SqlCommand(StrSQLUpdate, MiConexion)
                    iResultado = ComandoUpdate.ExecuteNonQuery
                    MiConexion.Close()


                End If


                iPosicionFila = iPosicionFila + 1
                Me.ProgressBar.Value = iPosicionFila

            Loop

        End If

        DataSet.Reset()
        SqlString = "SELECT *  FROM InventarioFisico WHERE (Activo = 1)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Consultas")
        If DataSet.Tables("Consultas").Rows.Count <> 0 Then
            Me.DTPFechaConteo.Value = DataSet.Tables("Consultas").Rows(0)("Fecha_Conteo")
        End If
        Me.BindingInventario.DataSource = DataSet.Tables("Consultas")


        Me.TrueDBGridConsultas.DataSource = Me.BindingInventario
        MiConexion.Close()

        Me.CboCodigoBodega.Text = CodBodega

        Me.TrueDBGridConsultas.Columns(0).Caption = "Codigo"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(0).Locked = True
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(1).Visible = False
        Me.TrueDBGridConsultas.Columns(2).Caption = "Fecha Conteo"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(2).Visible = False
        Me.TrueDBGridConsultas.Columns(3).Caption = "Descripcion"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(3).Width = 250
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(3).Locked = True
        Me.TrueDBGridConsultas.Columns(4).Caption = "Existencia"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(4).Width = 100
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(4).Locked = True
        Me.TrueDBGridConsultas.Columns(5).Caption = "Cantidad Contada"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(5).Width = 100
        Me.TrueDBGridConsultas.Columns(6).Caption = "Agotado"
        Me.TrueDBGridConsultas.Columns(7).Caption = "Contabilizado"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(7).Visible = False
        Me.TrueDBGridConsultas.Columns(8).Caption = "Activo"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(8).Visible = False

    End Sub

    Private Sub TrueDBGridConsultas_AfterColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles TrueDBGridConsultas.AfterColUpdate
        Dim Cols As Double, Valor As Boolean
        Cols = e.ColIndex
        Select Case Cols
            Case 4
                Valor = Me.TrueDBGridConsultas.Columns(5).Value
                If Valor = True Then
                    'Me.TrueDBGridConsultas.Columns(4).Text = 0
                End If
            Case 5
                Valor = Me.TrueDBGridConsultas.Columns(5).Value
                If Valor = True Then
                    'Me.TrueDBGridConsultas.Columns(4).Text = 0
                End If

        End Select

    End Sub

    Private Sub TrueDBGridConsultas_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles TrueDBGridConsultas.AfterUpdate
        Dim Sql = "SELECT  *  FROM InventarioFisico WHERE (Activo = 0) ORDER BY Cod_Producto"
        Dim DataAdapter = New SqlClient.SqlDataAdapter(Sql, MiConexion)
        Dim StrSQLUpdate As String, ComandoUpdate As New SqlClient.SqlCommand
        Dim iResultado As Integer, FechaConteo As String
        Dim CodProductos As String, Fecha As String
        Dim CantidadContada As Double, Agotado As Double, Valor As Boolean

        MiConexion.Open()
        DataAdapter.Fill(DataSet, "InventarioFisico")
        DataAdapter = Nothing
        MiConexion.Close()

        FechaConteo = Format(Me.DTPFechaConteo.Value, "yyyy-MM-dd")
        Fecha = Format(Me.DTPFechaConteo.Value, "dd/MM/yyyy")

        If Not DataSet.Tables("InventarioFisico").Rows.Count = 0 Then



            My.Application.DoEvents()
            CodProductos = Me.TrueDBGridConsultas.Columns(0).Text
            CantidadContada = Me.TrueDBGridConsultas.Columns(4).Text
            Valor = Me.TrueDBGridConsultas.Columns(5).Text
            If Valor = False Then
                Agotado = 0
            Else
                Agotado = 1
            End If

            MiConexion.Open()
            StrSQLUpdate = "UPDATE [InventarioFisico] SET [Cantidad_Contada] = " & CantidadContada & " ,[Agotado] = " & Agotado & " WHERE (Cod_Producto = '" & CodProductos & "') AND (Fecha_Conteo = CONVERT(DATETIME, '" & FechaConteo & "', 102)) AND (Activo = 0) "
            ComandoUpdate = New SqlClient.SqlCommand(StrSQLUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub




    Private Sub TrueDBGridConsultas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrueDBGridConsultas.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim Sql As String
        Dim DataAdapter = New SqlClient.SqlDataAdapter
        Dim StrSQLUpdate As String, ComandoUpdate As New SqlClient.SqlCommand
        Dim iResultado As Integer, iPosicionFila As Double, FechaConteo As String
        Dim CodProductos As String, Fecha As String, SqlString As String
        Dim CantidadContada As Double, Agotado As Boolean, Agotado2 As Integer
        Dim ConsecutivoCompra As Double, NumeroCompra As String, CodCliente As String, CodProveedor As String, NombreProveedor As String, NombreCliente As String
        Dim ConsecutivoFactura As Double, NumeroFactura As String, PrecioCompra As Double, PrecioVenta As Double, Existencia As Double
        Dim oDataRow As DataRow, Iposicion As Double, Registros As Double, Cantidad As Double, Importe As Double, NombreProductos As String



        '////////////////////BUSCO EL CLIENTE PARA INVENTARIO //////////////////////////////////////
        SqlString = "SELECT  * FROM Clientes WHERE(InventarioFisico = 1)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "ConsultasCliente")
        If DataSet.Tables("ConsultasCliente").Rows.Count <> 0 Then
            CodCliente = DataSet.Tables("ConsultasCliente").Rows(0)("Cod_Cliente")
            NombreCliente = DataSet.Tables("ConsultasCliente").Rows(0)("Nombre_Cliente")
        Else
            MsgBox("Seleccione una Cuenta cliente para Ajustes de Inventario", MsgBoxStyle.Critical, "Zeus Facturacion")
            Exit Sub
        End If


        '////////////////////BUSCO EL CLIENTE PARA INVENTARIO //////////////////////////////////////
        SqlString = "SELECT  * FROM Proveedor  WHERE(InventarioFisico = 1)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "ConsultasProveedor")
        If DataSet.Tables("ConsultasProveedor").Rows.Count <> 0 Then
            CodProveedor = DataSet.Tables("ConsultasProveedor").Rows(0)("Cod_Proveedor")
            NombreProveedor = DataSet.Tables("ConsultasProveedor").Rows(0)("Nombre_Proveedor")
        Else
            MsgBox("Seleccione una Cuenta Proveedor para Ajustes de Inventario", MsgBoxStyle.Critical, "Zeus Facturacion")
            Exit Sub
        End If

        'ConsecutivoCompra = ConsultaConsecutivo("Compra")
        'NumeroCompra = Format(ConsecutivoCompra, "0000#")

        'ConsecutivoFactura = ConsultaConsecutivo("Factura")
        'NumeroFactura = Format(ConsecutivoFactura, "0000#")

        FechaConteo = Format(Me.DTPFechaConteo.Value, "yyyy-MM-dd")
        Fecha = Format(Me.DTPFechaConteo.Value, "dd/MM/yyyy")

        '*******************************************************************************************************************************
        '/////////////////////////AGREGO UNA CONSULTA QUE NUNCA TENDRA REGISTROS PARA PODER AGREGARLOS /////////////////////////////////
        '*******************************************************************************************************************************
        SqlString = "SELECT Cod_Producto, Cantidad, Precio_Unitario, Descuento, Precio_Neto, Importe  FROM Detalle_Compras WHERE (Numero_Compra = N'-100000')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleCompras")

        SqlString = "SELECT     Numero_Factura, Cod_Producto, Descripcion_Producto, Cantidad, Precio_Unitario, Descuento, Precio_Neto, Importe  FROM Detalle_Facturas WHERE (Numero_Factura = N'-1000000')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleFacturas")



        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////BUSCO TODOS LOS PRODUCTOS DE MAS SEGUN INVENTARIO ////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        'Sql = "SELECT  *  FROM InventarioFisico WHERE (Activo = 1) ORDER BY Cod_Producto"
        Sql = "SELECT  Cod_Producto, Fecha_Conteo, CodBodega, Descripcion, Existencia_Unidades, Cantidad_Contada, Agotado, Contabilizado, Activo, Existencia_Unidades - Cantidad_Contada AS Diferencia, Existencia_Unidades + Cantidad_Contada AS Expr1  FROM InventarioFisico WHERE (Activo = 1) AND (Existencia_Unidades + Cantidad_Contada <> 0) ORDER BY Cod_Producto"
        DataAdapter = New SqlClient.SqlDataAdapter(Sql, MiConexion)
        MiConexion.Close()
        MiConexion.Open()
        DataAdapter.Fill(DataSet, "InventarioFisico")
        DataAdapter = Nothing
        MiConexion.Close()

        If Not DataSet.Tables("InventarioFisico").Rows.Count = 0 Then
            iPosicionFila = 0
            Me.ProgressBar.Minimum = 0
            Me.ProgressBar.Visible = True
            Me.ProgressBar.Value = 0
            Me.ProgressBar.Maximum = Me.BindingInventario.Count

            Do While iPosicionFila < (Me.BindingInventario.Count)
                My.Application.DoEvents()
                CodProductos = Trim(Me.BindingInventario.Item(iPosicionFila)("Cod_Producto"))
                NombreProductos = Trim(Me.BindingInventario.Item(iPosicionFila)("Descripcion"))
                CantidadContada = Trim(Me.BindingInventario.Item(iPosicionFila)("Cantidad_Contada"))
                Existencia = Trim(Me.BindingInventario.Item(iPosicionFila)("Existencia_Unidades"))
                Agotado = Trim(Me.BindingInventario.Item(iPosicionFila)("Agotado"))
                If Agotado = False Then
                    Agotado2 = 0
                Else
                    Agotado2 = 1
                End If

                MiConexion.Open()
                StrSQLUpdate = "UPDATE [InventarioFisico] SET [Agotado] = " & Agotado2 & " ,[Activo] = '0'  WHERE (Cod_Producto = '" & CodProductos & "') AND (Fecha_Conteo = CONVERT(DATETIME, '" & FechaConteo & "', 102)) AND (Activo = 1) "
                ComandoUpdate = New SqlClient.SqlCommand(StrSQLUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()

                MiConexion.Open()
                StrSQLUpdate = "UPDATE [Productos] SET [Existencia_Unidades] = " & CantidadContada & " WHERE (Cod_Productos = '" & CodProductos & "')"
                ComandoUpdate = New SqlClient.SqlCommand(StrSQLUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()


                'PrecioCompra = UltimoPrecioCompra(CodProductos)
                PrecioVenta = UltimoPrecioVenta(CodProductos)


                PrecioCompra = CostoPromedioKardex(CodProductos, Fecha)
                '///////////SI EL COSTO ES CERO PERO SE DESCARGAR, SIGNIFICA FACTURACION EN NEGATIVO ///////////

                If PrecioCompra = 0 Then
                    SqlString = "SELECT Detalle_Facturas.id_Detalle_Factura, Detalle_Facturas.Numero_Factura, Detalle_Facturas.Fecha_Factura, Detalle_Facturas.Tipo_Factura, Detalle_Facturas.Cod_Producto, Detalle_Facturas.Descripcion_Producto, Detalle_Facturas.Cantidad, Detalle_Facturas.Precio_Unitario, Detalle_Facturas.Descuento, Detalle_Facturas.Precio_Neto, Detalle_Facturas.Importe, Detalle_Facturas.TasaCambio, Detalle_Facturas.CodTarea, Detalle_Facturas.Costo_Unitario, Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa AS Costo_UnitarioDolar FROM Detalle_Facturas INNER JOIN TasaCambio ON Detalle_Facturas.Fecha_Factura = TasaCambio.FechaTasa  " & _
                                "WHERE (Detalle_Facturas.Cod_Producto = '" & CodProductos & "') AND (Detalle_Facturas.Costo_Unitario <> 0) AND (Detalle_Facturas.Fecha_Factura <= CONVERT(DATETIME,'" & Format(CDate(Fecha), "yyyy-MM-dd") & "', 102)) ORDER BY Detalle_Facturas.Fecha_Factura"
                    DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                    DataAdapter.Fill(DataSet, "UltimoCosto")
                    If DataSet.Tables("UltimoCosto").Rows.Count <> 0 Then
                        Registros = DataSet.Tables("UltimoCosto").Rows.Count
                        PrecioCompra = DataSet.Tables("UltimoCosto").Rows(Registros - 1)("Costo_Unitario")
                    End If
                    DataSet.Tables("UltimoCosto").Reset()

                End If


                '////////////////////////////////////////////////////////////////////////////////////////////////////
                '/////////////////////////////GRABO EL ENCABEZADO DE LA COMPRA /////////////////////////////////////////////
                '//////////////////////////////////////////////////////////////////////////////////////////////////////////
                If Existencia < 0 Then
                    oDataRow = DataSet.Tables("DetalleCompras").NewRow
                    oDataRow("Cod_Producto") = CodProductos
                    oDataRow("Cantidad") = Math.Abs(Existencia) + CantidadContada
                    oDataRow("Precio_Unitario") = PrecioCompra
                    oDataRow("Descuento") = 0
                    oDataRow("Precio_Neto") = PrecioCompra
                    oDataRow("Importe") = PrecioCompra * (Math.Abs(Existencia) + CantidadContada)
                    DataSet.Tables("DetalleCompras").Rows.Add(oDataRow)
                ElseIf Existencia < CantidadContada Then
                    oDataRow = DataSet.Tables("DetalleCompras").NewRow
                    oDataRow("Cod_Producto") = CodProductos
                    oDataRow("Cantidad") = CantidadContada - Existencia
                    oDataRow("Precio_Unitario") = PrecioCompra
                    oDataRow("Descuento") = 0
                    oDataRow("Precio_Neto") = PrecioCompra
                    oDataRow("Importe") = PrecioCompra * (CantidadContada - Existencia)
                    DataSet.Tables("DetalleCompras").Rows.Add(oDataRow)
                ElseIf Existencia > CantidadContada Then
                    oDataRow = DataSet.Tables("DetalleFacturas").NewRow
                    oDataRow("Cod_Producto") = CodProductos
                    oDataRow("Descripcion_Producto") = NombreProductos
                    oDataRow("Cantidad") = Existencia - CantidadContada
                    oDataRow("Precio_Unitario") = PrecioCompra
                    oDataRow("Descuento") = 0
                    oDataRow("Precio_Neto") = PrecioCompra
                    oDataRow("Importe") = PrecioCompra * (Existencia - CantidadContada)
                    DataSet.Tables("DetalleFacturas").Rows.Add(oDataRow)

                End If

                iPosicionFila = iPosicionFila + 1
                Me.ProgressBar.Value = iPosicionFila
            Loop


            '///////////////////////////////////////////////GRABO LAS COMPBRAS////////////////////////////////////////////////////
            Registros = DataSet.Tables("DetalleCompras").Rows.Count
            Iposicion = 0
            NumeroCompra = 0

            iPosicionFila = 0
            Me.ProgressBar.Minimum = 0
            Me.ProgressBar.Visible = True
            Me.ProgressBar.Value = 0
            Me.ProgressBar.Maximum = Registros
            Do While Iposicion < Registros

                '////////////////////////////////////////////////////////////////////////////////////////////////////
                '/////////////////////////////GRABO EL ENCABEZADO DE LA COMPRA /////////////////////////////////////////////
                '//////////////////////////////////////////////////////////////////////////////////////////////////////////7
                If Iposicion = 0 Then
                    ConsecutivoCompra = BuscaConsecutivo("Compra")
                    NumeroCompra = Format(ConsecutivoCompra, "0000#")
                    GrabaEncabezadoCompras(NumeroCompra, FechaConteo, "Mercancia Recibida", CodProveedor, Me.CboCodigoBodega.Text, NombreCliente, NombreCliente, FechaConteo, Val(0), 0, Val(0), Val(0), "Cordobas", "Procesado por Inventario Fisico  ")
                End If

                CodProductos = DataSet.Tables("DetalleCompras").Rows(Iposicion)("Cod_Producto")
                PrecioCompra = DataSet.Tables("DetalleCompras").Rows(Iposicion)("Precio_Unitario")
                'PrecioCompra = CostoPromedioKardex(CodProductos, Fecha)
                ''///////////SI EL COSTO ES CERO PERO SE DESCARGAR, SIGNIFICA FACTURACION EN NEGATIVO ///////////

                'If PrecioCompra = 0 Then
                '    SqlString = "SELECT Detalle_Facturas.id_Detalle_Factura, Detalle_Facturas.Numero_Factura, Detalle_Facturas.Fecha_Factura, Detalle_Facturas.Tipo_Factura, Detalle_Facturas.Cod_Producto, Detalle_Facturas.Descripcion_Producto, Detalle_Facturas.Cantidad, Detalle_Facturas.Precio_Unitario, Detalle_Facturas.Descuento, Detalle_Facturas.Precio_Neto, Detalle_Facturas.Importe, Detalle_Facturas.TasaCambio, Detalle_Facturas.CodTarea, Detalle_Facturas.Costo_Unitario, Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa AS Costo_UnitarioDolar FROM Detalle_Facturas INNER JOIN TasaCambio ON Detalle_Facturas.Fecha_Factura = TasaCambio.FechaTasa  " & _
                '                "WHERE (Detalle_Facturas.Cod_Producto = '" & CodProductos & "') AND (Detalle_Facturas.Costo_Unitario <> 0) AND (Detalle_Facturas.Fecha_Factura <= CONVERT(DATETIME,'" & Format(Fecha, "yyyy-MM-dd") & "', 102)) ORDER BY Detalle_Facturas.Fecha_Factura"
                '    DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                '    DataAdapter.Fill(DataSet, "UltimoCosto")
                '    If DataSet.Tables("UltimoCosto").Rows.Count <> 0 Then
                '        Registros = DataSet.Tables("UltimoCosto").Rows.Count
                '        PrecioCompra = DataSet.Tables("UltimoCosto").Rows(Registros - 1)("Costo_Unitario")
                '    End If
                '    DataSet.Tables("UltimoCosto").Reset()

                'End If

                Cantidad = DataSet.Tables("DetalleCompras").Rows(Iposicion)("Cantidad")
                Importe = DataSet.Tables("DetalleCompras").Rows(Iposicion)("Importe")

                GrabaDetalleCompraLiquidacion(NumeroCompra, CodProductos, PrecioCompra, 0, PrecioCompra, Importe, Cantidad, "Cordobas", FechaConteo)
                ActualizaExistencia(CodProductos)


                Iposicion = Iposicion + 1
                Me.ProgressBar.Value = Me.ProgressBar.Value + 1
            Loop


            '///////////////////////////////////////////////GRABO LAS SALIDA DE BODEGAS////////////////////////////////////////////////////
            Registros = DataSet.Tables("DetalleFacturas").Rows.Count
            Iposicion = 0
            NumeroFactura = 0
            iPosicionFila = 0
            Me.ProgressBar.Minimum = 0
            Me.ProgressBar.Visible = True
            Me.ProgressBar.Value = 0
            Me.ProgressBar.Maximum = Registros
            Do While Iposicion < Registros

                '////////////////////////////////////////////////////////////////////////////////////////////////////
                '/////////////////////////////GRABO EL ENCABEZADO DE LA SALIDA DE BODEGA /////////////////////////////////////////////
                '//////////////////////////////////////////////////////////////////////////////////////////////////////////7
                If Iposicion = 0 Then
                    ConsecutivoFactura = BuscaConsecutivo("SalidaBodega")
                    NumeroFactura = Format(ConsecutivoFactura, "0000#")
                    GrabaEncabezadoFacturas(NumeroFactura, FechaConteo, "Salida Bodega", CodCliente, Me.CboCodigoBodega.Text, NombreCliente, NombreCliente, FechaConteo, Val(0), 0, Val(0), Val(0), "Cordobas", "Procesado por Inventario Fisico  ")
                End If

                CodProductos = DataSet.Tables("DetalleFacturas").Rows(Iposicion)("Cod_Producto")
                NombreProductos = DataSet.Tables("DetalleFacturas").Rows(Iposicion)("Descripcion_Producto")
                PrecioCompra = DataSet.Tables("DetalleFacturas").Rows(Iposicion)("Precio_Unitario")
                Cantidad = DataSet.Tables("DetalleFacturas").Rows(Iposicion)("Cantidad")
                Importe = DataSet.Tables("DetalleFacturas").Rows(Iposicion)("Importe")


                GrabaDetalleFacturaSalida(NumeroFactura, CodProductos, NombreProductos, PrecioCompra, 0, PrecioCompra, Importe, Cantidad, "Cordobas", FechaConteo, "Salida Bodega", PrecioCompra)
                ActualizaExistencia(CodProductos)


                Iposicion = Iposicion + 1
                Me.ProgressBar.Value = Me.ProgressBar.Value + 1
            Loop


            MiConexion.Open()


        End If

        DataSet.Reset()
        SqlString = "SELECT *  FROM InventarioFisico WHERE (Activo = 1)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Consultas")
        If DataSet.Tables("Consultas").Rows.Count <> 0 Then
            Me.DTPFechaConteo.Value = DataSet.Tables("Consultas").Rows(0)("Fecha_Conteo")
        End If
        Me.BindingInventario.DataSource = DataSet.Tables("Consultas")


        Me.TrueDBGridConsultas.DataSource = Me.BindingInventario
        MiConexion.Close()

        Me.TrueDBGridConsultas.Columns(0).Caption = "Codigo"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(0).Locked = True
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(1).Visible = False
        Me.TrueDBGridConsultas.Columns(2).Caption = "Fecha Conteo"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(2).Visible = False
        Me.TrueDBGridConsultas.Columns(3).Caption = "Descripcion"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(3).Width = 250
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(3).Locked = True
        Me.TrueDBGridConsultas.Columns(4).Caption = "Existencia"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(4).Width = 100
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(4).Locked = True
        Me.TrueDBGridConsultas.Columns(5).Caption = "Cantidad Contada"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(5).Width = 100
        Me.TrueDBGridConsultas.Columns(6).Caption = "Agotado"
        Me.TrueDBGridConsultas.Columns(7).Caption = "Contabilizado"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(7).Visible = False
        Me.TrueDBGridConsultas.Columns(8).Caption = "Activo"
        Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(8).Visible = False

    End Sub

    Private Sub CmdGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdGrabar.Click
        Dim Sql = "SELECT  *  FROM InventarioFisico WHERE (Activo = 1) ORDER BY Cod_Producto"
        Dim DataAdapter = New SqlClient.SqlDataAdapter(Sql, MiConexion)
        Dim StrSQLUpdate As String, ComandoUpdate As New SqlClient.SqlCommand
        Dim iResultado As Integer, iPosicionFila As Double, FechaConteo As String
        Dim CodProductos As String, Fecha As String
        Dim CantidadContada As Double, Agotado As Boolean, Agotado2 As Integer


        MiConexion.Close()
        MiConexion.Open()
        DataAdapter.Fill(DataSet, "InventarioFisico")
        DataAdapter = Nothing
        MiConexion.Close()

        FechaConteo = Format(Me.DTPFechaConteo.Value, "yyyy-MM-dd")
        Fecha = Format(Me.DTPFechaConteo.Value, "dd/MM/yyyy")

        If Not DataSet.Tables("InventarioFisico").Rows.Count = 0 Then
            iPosicionFila = 0
            Me.ProgressBar.Minimum = 0
            Me.ProgressBar.Visible = True
            Me.ProgressBar.Value = 0
            Me.ProgressBar.Maximum = Me.BindingInventario.Count

            Do While iPosicionFila < (Me.BindingInventario.Count)
                My.Application.DoEvents()
                CodProductos = Trim(Me.BindingInventario.Item(iPosicionFila)("Cod_Producto"))
                CantidadContada = Trim(Me.BindingInventario.Item(iPosicionFila)("Cantidad_Contada"))
                Agotado = Trim(Me.BindingInventario.Item(iPosicionFila)("Agotado"))
                If Agotado = False Then
                    Agotado2 = 0
                Else
                    Agotado2 = 1
                End If

                MiConexion.Open()
                StrSQLUpdate = "UPDATE [InventarioFisico] SET [Cantidad_Contada] = " & CantidadContada & " ,[Agotado] = " & Agotado2 & ",[CodBodega] = '" & Me.CboCodigoBodega.Text & "'  WHERE (Cod_Producto = '" & CodProductos & "') AND (Fecha_Conteo = CONVERT(DATETIME, '" & FechaConteo & "', 102)) AND (Activo = 1) "
                ComandoUpdate = New SqlClient.SqlCommand(StrSQLUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()
                iPosicionFila = iPosicionFila + 1
                Me.ProgressBar.Value = iPosicionFila
            Loop

            MiConexion.Open()

        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdImprimir.Click
        Dim ArepInventarioFisico As New ArepInventarioFisico, RutaLogo As String
        Dim SqlDatos As String, DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim SQL As New DataDynamics.ActiveReports.DataSources.SqlDBDataSource, SqlDetalle As String


        'CmdGrabar_Click(sender, e)

        SqlDatos = "SELECT * FROM DatosEmpresa"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlDatos, MiConexion)
        DataAdapter.Fill(DataSet, "DatosEmpresa")

        If Not DataSet.Tables("DatosEmpresa").Rows.Count = 0 Then
            ArepInventarioFisico.LblTitulo.Text = DataSet.Tables("DatosEmpresa").Rows(0)("Nombre_Empresa")
            ArepInventarioFisico.LblDireccion.Text = DataSet.Tables("DatosEmpresa").Rows(0)("Direccion_Empresa")
            If Not IsDBNull(DataSet.Tables("DatosEmpresa").Rows(0)("Numero_Ruc")) Then
                ArepInventarioFisico.LblRuc.Text = "Numero RUC " & DataSet.Tables("DatosEmpresa").Rows(0)("Numero_Ruc")
            End If

            If Not IsDBNull(DataSet.Tables("DatosEmpresa").Rows(0)("Ruta_Logo")) Then
                RutaLogo = DataSet.Tables("DatosEmpresa").Rows(0)("Ruta_Logo")
                If Dir(RutaLogo) <> "" Then
                    ArepInventarioFisico.ImgLogo.Image = New System.Drawing.Bitmap(RutaLogo)
                End If
            End If
        End If

        SqlDetalle = "SELECT InventarioFisico.Cod_Producto, InventarioFisico.Fecha_Conteo, InventarioFisico.CodBodega, InventarioFisico.Descripcion, InventarioFisico.Existencia_Unidades, InventarioFisico.Cantidad_Contada, InventarioFisico.Agotado, InventarioFisico.Contabilizado, InventarioFisico.Activo, Bodegas.Cod_Bodega, Bodegas.Nombre_Bodega, Bodegas.CuentaInventario, Bodegas.CuentaIngresoAjustes, Bodegas.CuentaGastosAjustes, Bodegas.CuentaVentas,Bodegas.CuentaCostos, InventarioFisico.Existencia_Unidades - InventarioFisico.Cantidad_Contada AS Diferencia FROM InventarioFisico INNER JOIN Bodegas ON InventarioFisico.CodBodega = Bodegas.Cod_Bodega WHERE     (InventarioFisico.Activo = 1) AND (InventarioFisico.Existencia_Unidades + InventarioFisico.Cantidad_Contada <> 0) ORDER BY InventarioFisico.Cod_Producto"

        SQL.ConnectionString = Conexion
        SQL.SQL = SQlDetalle
        ArepInventarioFisico.DataSource = SQL
        ArepInventarioFisico.Document.Name = "Reporte de Inventario Fisico"


        Dim ViewerForm As New FrmViewer()
        ViewerForm.arvMain.Document = ArepInventarioFisico.Document
        ViewerForm.Show()
        ArepInventarioFisico.Run(True)



    End Sub

    Private Sub CboCodigoBodega_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboCodigoBodega.TextChanged

    End Sub

    Private Sub C1Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Button3.Click
        Me.OpenFileDialog.ShowDialog()
        RutaBD = OpenFileDialog.FileName
        Me.TxtRuta.Text = RutaBD

        ConexionExcel = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties = 'Excel 4.0'; Data Source= " & RutaBD & " "
        MiConexionExcel = New OleDb.OleDbConnection(ConexionExcel)
        DataAdapterExcel = New OleDb.OleDbDataAdapter("SELECT * FROM [Hoja1$]", MiConexionExcel)

        Dim commandbuilder As New OleDb.OleDbCommandBuilder(Me.DataAdapterExcel)
        MiConexionExcel.Open()
        DataAdapterExcel.Fill(MiDataSet, "DatosExcel")
        Me.TDBGridLotes.DataSource = MiDataSet.Tables("DatosExcel")

        'Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(0).Width = 75
        'Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(1).Width = 75
        'Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(2).Width = 180
        'Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(3).Width = 75
        'Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(4).Width = 75
        'Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(5).Width = 75
        'Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(6).Width = 75
        'Me.TrueDBGridConsultas.Columns(6).NumberFormat = "##,##0.00"
        'Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(7).Width = 75
        'Me.TrueDBGridConsultas.Columns(7).NumberFormat = "##,##0.00"
        'Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(8).Width = 75
        'Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(9).Width = 75
        'Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(10).Width = 75
        'Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(11).Width = 75
        'Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(12).Width = 75
        'Me.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(13).Width = 75

        MiConexionExcel.Close()
    End Sub

    Private Sub C1Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Button1.Click
        Dim Sql As String, SqlString As String
        Dim DataAdapter = New SqlClient.SqlDataAdapter
        Dim CodCliente As String, CodProveedor As String, NombreProveedor As String

        Dim StrSQLUpdate As String, ComandoUpdate As New SqlClient.SqlCommand
        Dim iResultado As Integer, iPosicionFila As Double, FechaConteo As String
        Dim CodProductos As String, Fecha As String
        Dim Cantidad As Double, Agotado As Boolean, Agotado2 As Integer, FechaVence As Date
        Dim ConsecutivoCompra As Double, NumeroCompra As String, NombreCliente As String
        Dim ConsecutivoFactura As Double, NumeroFactura As String, PrecioCompra As Double, PrecioVenta As Double, Existencia As Double
        Dim oDataRow As DataRow, Iposicion As Double, Registros As Double, Importe As Double, NombreProductos As String
        Dim NumeroLote As String, CodBodega As String, Nombres As String, Apellidos As String, PrecioUnitario As Double = 0, PrecioNeto As Double = 0, Descuento As Double = 0
        Dim SubTotal As Double, IVA As Double

        NumeroCompra = 0
        CodProductos = ""
        NombreProductos = ""

        iPosicionFila = 0
        Me.ProgressBar.Minimum = 0
        Me.ProgressBar.Visible = True
        Me.ProgressBar.Value = 0
        Me.ProgressBar.Maximum = MiDataSet.Tables("DatosExcel").Rows.Count
        Do While iPosicionFila < (MiDataSet.Tables("DatosExcel").Rows.Count)

            My.Application.DoEvents()
            If Not IsDBNull(MiDataSet.Tables("DatosExcel").Rows(iPosicionFila)("DESCRIPCION")) Then
                NombreProductos = MiDataSet.Tables("DatosExcel").Rows(iPosicionFila)("DESCRIPCION")
            End If

            If Not IsDBNull(MiDataSet.Tables("DatosExcel").Rows(iPosicionFila)("CODIGO")) Then
                CodProductos = MiDataSet.Tables("DatosExcel").Rows(iPosicionFila)("CODIGO")
                CodProductos = Replace(CodProductos, "'", " ")
                Me.Text = "Procesando " & iPosicionFila & " de " & MiDataSet.Tables("DatosExcel").Rows.Count & " " & NombreProductos
            End If


            If Not IsDBNull(MiDataSet.Tables("DatosExcel").Rows(iPosicionFila)("CANTIDAD")) Then
                Cantidad = MiDataSet.Tables("DatosExcel").Rows(iPosicionFila)("CANTIDAD")
            Else
                Cantidad = 0
            End If


            If Not IsDBNull(MiDataSet.Tables("DatosExcel").Rows(iPosicionFila)("LOTE")) Then
                NumeroLote = MiDataSet.Tables("DatosExcel").Rows(iPosicionFila)("LOTE")
            Else
                NumeroLote = "SINLOTE"
            End If

            If Not IsDBNull(MiDataSet.Tables("DatosExcel").Rows(iPosicionFila)("FECHAVENCE")) Then
                FechaVence = MiDataSet.Tables("DatosExcel").Rows(iPosicionFila)("FECHAVENCE")
            End If

            PrecioCompra = CostoPromedioKardex(CodProductos, Me.DTPFechaLote.Value)

            '/////////////////////////////////////////////////////////////////////////////////////////////////////
            '///////////////////////////BUSCO SI EXISTEN LO LOTES ////////////////////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////////
            Sql = "SELECT * FROM Lote WHERE  (Numero_Lote = '" & NumeroLote & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(Sql, MiConexion)
            DataAdapter.Fill(DataSet, "BuscaLote")
            If Not DataSet.Tables("BuscaLote").Rows.Count = 0 Then
                '///////////SI EXISTE EL USUARIO LO ACTUALIZO////////////////
                StrSQLUpdate = "UPDATE [Lote]  SET [Nombre_Lote] =  '" & NumeroLote & "' ,[FechaVence] = '" & Format(FechaVence, "dd/MM/yyyy") & "'  WHERE (Numero_Lote = '" & NumeroLote & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(StrSQLUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()

            Else

                '/////////SI NO EXISTE LO AGREGO COMO NUEVO/////////////////
                StrSQLUpdate = "INSERT INTO [Lote] ([Numero_Lote],[Nombre_Lote],[FechaVence]) VALUES ('" & NumeroLote & "' ,'" & NumeroLote & "' ,'" & Format(FechaVence, "dd/MM/yyyy") & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(StrSQLUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()

            End If


            DataSet.Tables("BuscaLote").Reset()


            CodBodega = Me.CboCodigoBodega.Text


            '////////////////////////////////////////////////////////////////////////////////////////////////////
            '/////////////////////////////BUSCO EL CONSECUTIVO DE LA COMPRA /////////////////////////////////////////////
            '//////////////////////////////////////////////////////////////////////////////////////////////////////////7
            If NumeroCompra = 0 Then


                Apellidos = ""
                '//////////////////////////BUSCO EL PROVEEDOR ///////////////////////////////////////////////////////////
                CodProveedor = "01"
                Sql = "SELECT  * FROM Proveedor  WHERE (Cod_Proveedor = '" & CodProveedor & "')"
                DataAdapter = New SqlClient.SqlDataAdapter(Sql, MiConexion)
                DataAdapter.Fill(DataSet, "Proveedor")
                If Not DataSet.Tables("Proveedor").Rows.Count = 0 Then
                    Nombres = DataSet.Tables("Proveedor").Rows(0)("Nombre_Proveedor")
                    If Not IsDBNull(DataSet.Tables("Proveedor").Rows(0)("Apellido_Proveedor")) Then
                        Apellidos = DataSet.Tables("Proveedor").Rows(0)("Apellido_Proveedor")
                    End If
                Else
                    MsgBox("El Proveedor  no existe, Se detendra el Proceso " & CodProveedor, MsgBoxStyle.Critical, "Zeus Facturacion")
                    Exit Sub
                End If

                DataSet.Tables("Proveedor").Reset()




                ConsecutivoCompra = BuscaConsecutivo("Compra")
                NumeroCompra = Format(ConsecutivoCompra, "0000#")

                '////////////////////////////////////////////////////////////////////////////////////////////////////
                '/////////////////////////////GRABO EL ENCABEZADO DE LA COMPRA /////////////////////////////////////////////
                '//////////////////////////////////////////////////////////////////////////////////////////////////////////7
                GrabaEncabezadoCompras(NumeroCompra, Me.DTPFechaLote.Value, "Mercancia Recibida", CodProveedor, CodBodega, Nombres, Apellidos, Me.DTPFechaLote.Value, Val(0), Val(0), Val(0), Val(0), "Cordobas", "Procesando por la importacion")
            End If


            '////////////////////////////////////////////////////////////////////////////////////////////////////
            '/////////////////////////////GRABO EL DETALLE DE LA COMPRA /////////////////////////////////////////////
            '//////////////////////////////////////////////////////////////////////////////////////////////////////////7




            Descuento = 0

            Importe = Cantidad * PrecioCompra


            ',[Numero_Lote] = '" & Numero_Lote & "' ,[Fecha_Vence] = " & Format(Fecha_Lote, "dd/MM/yyyy") & "

            If PrecioCompra <> 0 And Cantidad <> 0 Then
                GrabaDetalleCompraInventarioFisico(NumeroCompra, CodProductos, PrecioCompra, Descuento, PrecioCompra, Importe, Cantidad, "Cordobas", Me.DTPFechaLote.Value, NumeroLote, FechaVence)
                'GrabaDetalleCompra(NumeroCompra, CodProductos, PrecioCompra, Descuento, PrecioCompra, Importe, Cantidad, NumeroLote, FechaVence)
                ExistenciasCostosBodega(CodProductos, Cantidad, PrecioCompra, "Mercancia Recibida", CodBodega, Me.DTPFecha.Value)
                CostoBodega(CodProductos, Cantidad, PrecioCompra, "Mercancia Recibida", CodBodega, Me.DTPFecha.Value)


                SubTotal = SubTotal + (PrecioCompra * Cantidad)
                IVA = IVA + SubTotal

            End If








            Me.Text = "Procesando " & iPosicionFila & " de " & MiDataSet.Tables("DatosExcel").Rows.Count & " " & NombreProductos
            iPosicionFila = iPosicionFila + 1
            Me.ProgressBar.Value = iPosicionFila
        Loop





    End Sub

    Private Sub ChkReiniciar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)




    End Sub

    Private Sub CmdNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdNuevo.Click
        Quien = "Repesaje"
        If Me.TrueDBGridConsultas.Columns("NumeroRecepcion").Text <> "" Then
            Me.Numero_Repesaje = Me.TrueDBGridConsultas.Columns("NumeroRecepcion").Text
        Else
            Me.Numero_Repesaje = "-----0-----"
            Me.NumeroTemporal = Me.Random.Next()
        End If


        My.Forms.FrmBascula.Show()
    End Sub
End Class