Imports System.Data.SqlClient
Imports System.Threading

Module Funciones




    Public Sub LimpiarRecepcionPlanilla()
        Dim SqlString As String, DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)

        My.Forms.FrmRecepcionPlanilla.TxtNumeroEnsamble.Text = "-----0-----"
        My.Forms.FrmRecepcionPlanilla.TxtCodigoProveedor.Text = ""



        FrmRecepcionPlanilla.DTPFecha.Value = Format(Now, "dd/MM/yyyy")
        FrmRecepcionPlanilla.DTVencimiento.Value = Format(Now, "dd/MM/yyyy")

        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////CARGO LAS BODEGAS////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlString = "SELECT  * FROM   Bodegas"
        MiConexion.Open()
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataSet.Reset()
        DataAdapter.Fill(DataSet, "Bodegas")
        FrmRecepcionPlanilla.CboCodigoBodega.DataSource = DataSet.Tables("Bodegas")
        If Not DataSet.Tables("Bodegas").Rows.Count = 0 Then
            FrmRecepcionPlanilla.CboCodigoBodega.Text = DataSet.Tables("Bodegas").Rows(0)("Cod_Bodega")
        End If
        FrmRecepcionPlanilla.CboCodigoBodega.Columns(0).Caption = "Codigo"
        FrmRecepcionPlanilla.CboCodigoBodega.Columns(1).Caption = "Nombre Bodega"


        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////CARGO LAS FORMA DE PAGO////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlString = "SELECT  NombrePago, Monto,NumeroTarjeta,FechaVence FROM Detalle_MetodoCompras WHERE (Numero_Compra = '-1')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "MetodoPago")
        FrmRecepcionPlanilla.BindingMetodo.DataSource = DataSet.Tables("MetodoPago")
        FrmRecepcionPlanilla.TrueDBGridMetodo.DataSource = FrmRecepcionPlanilla.BindingMetodo
        FrmRecepcionPlanilla.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(1).Width = 110
        FrmRecepcionPlanilla.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(1).Width = 70
        FrmRecepcionPlanilla.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(0).Button = True
        FrmRecepcionPlanilla.TrueDBGridMetodo.Columns(1).NumberFormat = "##,##0.00"
        FrmRecepcionPlanilla.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(2).Visible = False
        FrmRecepcionPlanilla.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(3).Visible = False
        MiConexion.Close()

        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////////////CARGO EL DETALLE DE COMPRAS/////////////////////////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlString = "SELECT  Productos.Cod_Productos, Productos.Descripcion_Producto, Detalle_Compras.Cantidad, Detalle_Compras.Precio_Unitario, Detalle_Compras.Descuento, Detalle_Compras.Precio_Neto, Detalle_Compras.Importe FROM  Productos INNER JOIN  Detalle_Compras ON Productos.Cod_Productos = Detalle_Compras.Cod_Producto  WHERE (Detalle_Compras.Numero_Compra = '-1')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleCompra")
        FrmRecepcionPlanilla.BindingDetalle.DataSource = DataSet.Tables("DetalleCompra")
        FrmRecepcionPlanilla.TrueDBGridComponentes.DataSource = FrmRecepcionPlanilla.BindingDetalle
        FrmRecepcionPlanilla.TrueDBGridComponentes.Columns(0).Caption = "Codigo"
        FrmRecepcionPlanilla.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Button = True
        FrmRecepcionPlanilla.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Width = 74
        FrmRecepcionPlanilla.TrueDBGridComponentes.Columns(1).Caption = "Descripcion"
        FrmRecepcionPlanilla.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Width = 259
        FrmRecepcionPlanilla.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Locked = True
        FrmRecepcionPlanilla.TrueDBGridComponentes.Columns(2).Caption = "Ordenado"
        FrmRecepcionPlanilla.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Width = 64
        FrmRecepcionPlanilla.TrueDBGridComponentes.Columns(3).Caption = "Precio Unit"
        FrmRecepcionPlanilla.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Width = 62
        FrmRecepcionPlanilla.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Locked = True
        FrmRecepcionPlanilla.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Visible = False
        FrmRecepcionPlanilla.TrueDBGridComponentes.Columns(4).Caption = "%Desc"
        FrmRecepcionPlanilla.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Width = 43
        FrmRecepcionPlanilla.TrueDBGridComponentes.Columns(5).Caption = "Precio Neto"
        FrmRecepcionPlanilla.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Width = 65
        FrmRecepcionPlanilla.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Locked = True
        FrmRecepcionPlanilla.TrueDBGridComponentes.Columns(6).Caption = "Importe"
        FrmRecepcionPlanilla.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Width = 61
        FrmRecepcionPlanilla.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Locked = True

        FrmRecepcionPlanilla.RadioButton1.Checked = True
        My.Forms.FrmRecepcionPlanilla.TxtSubTotal.Text = ""
        My.Forms.FrmRecepcionPlanilla.TxtIva.Text = ""
        My.Forms.FrmRecepcionPlanilla.TxtPagado.Text = ""
        My.Forms.FrmRecepcionPlanilla.TxtNetoPagar.Text = ""
        My.Forms.FrmRecepcionPlanilla.TxtObservaciones.Text = ""
    End Sub
    Public Sub GrabaMetodoDetalleRecepcion(ByVal ConsecutivoCompra As String, ByVal NombrePago As String, ByVal Monto As Double, ByVal NumeroTarjeta As String, ByVal FechaVence As String)
        Dim Sqldetalle As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String, FechaVencimiento As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter

        Fecha = Format(FrmRecepcionPlanilla.DTPFecha.Value, "yyyy-MM-dd")
        FechaVencimiento = Format(CDate(FechaVence), "dd/MM/yyyy")


        Sqldetalle = "SELECT *  FROM Detalle_MetodoCompras WHERE (Numero_Compra = '" & ConsecutivoCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & FrmRecepcionPlanilla.CboTipoProducto.Text & "') AND (NombrePago = '" & NombrePago & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(Sqldetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleMetodoCompra")
        If Not DataSet.Tables("DetalleMetodoCompra").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlUpdate = "UPDATE [Detalle_MetodoCompras] SET [NombrePago] = '" & NombrePago & "',[Monto] = " & Monto & ",[NumeroTarjeta] = '" & NumeroTarjeta & "',[FechaVence] = '" & FechaVencimiento & "' " & _
                         "WHERE (Numero_Compra = '" & ConsecutivoCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & FrmRecepcionPlanilla.CboTipoProducto.Text & "') AND (NombrePago = '" & NombrePago & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            MiConexion.Close()
            SqlUpdate = "INSERT INTO [Detalle_MetodoCompras] ([Numero_Compra],[Fecha_Compra],[Tipo_Compra],[NombrePago],[Monto],[NumeroTarjeta] ,[FechaVence]) " & _
                        "VALUES ('" & ConsecutivoCompra & "','" & FrmRecepcionPlanilla.DTPFecha.Value & "','" & FrmRecepcionPlanilla.CboTipoProducto.Text & "','" & NombrePago & "'," & Monto & " ,'" & NumeroTarjeta & "','" & FechaVencimiento & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub



    Public Sub GrabaDetalleRecepcionPlanilla(ByVal ConsecutivoCompra As String, ByVal CodProducto As String, ByVal PrecioUnitario As Double, ByVal Descuento As Double, ByVal PrecioNeto As Double, ByVal Importe As Double, ByVal Cantidad As Double)
        Dim Sqldetalle As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, TasaCambio As String
        Dim Fecha As String, MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, MonedaCompra As String, MonedaProducto As String

        MonedaCompra = FrmRecepcionPlanilla.TxtMonedaFactura.Text
        MonedaProducto = "Cordobas"
        TasaCambio = 0

        If MonedaCompra = "Cordobas" Then
            If MonedaProducto = "Cordobas" Then
                TasaCambio = 1
            Else
                If BuscaTasaCambio(FrmRecepcionPlanilla.DTPFecha.Value) <> 0 Then
                    TasaCambio = (1 / BuscaTasaCambio(FrmRecepcionPlanilla.DTPFecha.Value))
                End If
            End If
        ElseIf MonedaCompra = "Dolares" Then
            If MonedaProducto = "Cordobas" Then
                TasaCambio = BuscaTasaCambio(FrmRecepcionPlanilla.DTPFecha.Value)
            Else
                TasaCambio = 1
            End If
        End If


        Fecha = Format(FrmRecepcionPlanilla.DTPFecha.Value, "yyyy-MM-dd")

        Sqldetalle = "SELECT *  FROM Detalle_Compras WHERE (Numero_Compra = '" & ConsecutivoCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & FrmRecepcionPlanilla.CboTipoProducto.Text & "') AND (Cod_Producto = '" & CodProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(Sqldetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleCompra")
        If Not DataSet.Tables("DetalleCompra").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlUpdate = "UPDATE [Detalle_Compras] SET [Cantidad] = " & Cantidad & " ,[Precio_Unitario] = " & PrecioUnitario & ",[Descuento] = " & Descuento & " ,[Precio_Neto] = " & PrecioNeto & ",[Importe] = " & Importe & ",[TasaCambio] = " & TasaCambio & " " & _
                        "WHERE (Numero_Compra = '" & ConsecutivoCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & FrmRecepcionPlanilla.CboTipoProducto.Text & "') AND (Cod_Producto = '" & CodProducto & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else

            SqlUpdate = "INSERT INTO [Detalle_Compras] ([Numero_Compra],[Fecha_Compra],[Tipo_Compra],[Cod_Producto],[Cantidad],[Precio_Unitario],[Descuento],[Precio_Neto],[Importe],[TasaCambio]) " & _
            "VALUES ('" & ConsecutivoCompra & "','" & Format(FrmRecepcionPlanilla.DTPFecha.Value, "dd/MM/yyyy") & "','" & FrmRecepcionPlanilla.CboTipoProducto.Text & "','" & CodProducto & "' ," & Cantidad & "," & PrecioUnitario & "," & Descuento & " ," & PrecioNeto & "," & Importe & "," & TasaCambio & ")"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub


    Public Sub GrabaComprasRecepcion(ByVal ConsecutivoCompra As String)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, MonedaCompra As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Subtotal As Double, Iva As Double, Neto As Double, Pagado As Double


        MonedaCompra = FrmRecepcionPlanilla.TxtMonedaFactura.Text

        Fecha = Format(FrmRecepcionPlanilla.DTPFecha.Value, "yyyy-MM-dd")

        If FrmRecepcionPlanilla.TxtSubTotal.Text <> "" Then
            Subtotal = FrmRecepcionPlanilla.TxtSubTotal.Text
        Else
            Subtotal = 0
        End If

        If FrmRecepcionPlanilla.TxtIva.Text <> "" Then
            Iva = FrmRecepcionPlanilla.TxtIva.Text
        Else
            Iva = 0
        End If

        If FrmRecepcionPlanilla.TxtPagado.Text <> "" Then
            Pagado = FrmRecepcionPlanilla.TxtPagado.Text
        Else
            Pagado = 0
        End If

        If FrmRecepcionPlanilla.TxtNetoPagar.Text <> "" Then
            Neto = FrmRecepcionPlanilla.TxtNetoPagar.Text
        Else
            Neto = 0
        End If

        MiConexion.Close()

        If FrmRecepcionPlanilla.TxtNumeroEnsamble.Text = "-----0-----" Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "INSERT INTO [Compras] ([Numero_Compra] ,[Fecha_Compra],[Tipo_Compra],[Cod_Proveedor],[TipoProductor],[Cod_Bodega],[Nombre_Proveedor],[Apellido_Proveedor],[Direccion_Proveedor],[Telefono_Proveedor],[Fecha_Vencimiento],[Observaciones],[SubTotal],[IVA],[Pagado],[NetoPagar],[MontoCredito],[MonedaCompra]) " & _
            "VALUES ('" & ConsecutivoCompra & "','" & FrmRecepcionPlanilla.DTPFecha.Value & "','" & FrmRecepcionPlanilla.CboTipoProducto.Text & "','" & FrmRecepcionPlanilla.TxtCodigoProveedor.Text & "','Productor','" & FrmRecepcionPlanilla.CboCodigoBodega.Text & "' , '" & FrmRecepcionPlanilla.TxtNombres.Text & "','" & FrmRecepcionPlanilla.TxtApellidos.Text & "','" & FrmRecepcionPlanilla.TxtDireccion.Text & "','" & FrmRecepcionPlanilla.TxtTelefono.Text & "','" & FrmRecepcionPlanilla.DTVencimiento.Value & "','" & FrmRecepcionPlanilla.TxtObservaciones.Text & "'," & Subtotal & "," & Iva & "," & Pagado & "," & Neto & "," & Neto & ",'" & MonedaCompra & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "UPDATE [Compras]  SET [Cod_Proveedor] = '" & FrmRecepcionPlanilla.TxtCodigoProveedor.Text & "',[TipoProductor] = 'Productor',[Nombre_Proveedor] = '" & FrmRecepcionPlanilla.TxtNombres.Text & "',[Apellido_Proveedor] = '" & FrmRecepcionPlanilla.TxtApellidos.Text & "',[Direccion_Proveedor] = '" & FrmRecepcionPlanilla.TxtDireccion.Text & "',[Telefono_Proveedor] = '" & FrmRecepcionPlanilla.TxtTelefono.Text & "',[Fecha_Vencimiento] = '" & FrmRecepcionPlanilla.DTVencimiento.Value & "' ,[Observaciones] = '" & FrmRecepcionPlanilla.TxtObservaciones.Text & "',[SubTotal] = " & Subtotal & ",[IVA] = " & Iva & ",[Pagado] = " & Pagado & ",[NetoPagar] = " & Neto & ",[MontoCredito] = " & Neto & ",[MonedaCompra] = '" & MonedaCompra & "'  " & _
                         "WHERE  (Numero_Compra = '" & ConsecutivoCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & FrmRecepcionPlanilla.CboTipoProducto.Text & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If

    End Sub


    Public Sub ActualizaMETODORecepcionPlanilla()
        Dim Metodo As String, iPosicion As Double, Registros As Double, Monto As Double
        Dim Subtotal As Double, Iva As Double, Neto As Double, CodProducto As String, SQlString As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), CodIva As String, Tasa As Double, Moneda As String, Fecha As String, SQLTasa As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, TasaCambio As Double, TipoMetodo As String, SQLMetodo As String


        Registros = FrmRecepcionPlanilla.BindingMetodo.Count
        iPosicion = 0

        Do While iPosicion < Registros
            Metodo = FrmRecepcionPlanilla.BindingMetodo.Item(iPosicion)("NombrePago")
            TasaCambio = 1
            Fecha = Format(FrmRecepcionPlanilla.DTPFecha.Value, "yyyy-MM-dd")
            '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '//////////////////////////////BUSCO LA MONEDA DEL METODO DE PAGO///////////////////////////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Moneda = "Cordobas"
            TipoMetodo = "Cambio"
            SQLMetodo = "SELECT * FROM MetodoPago WHERE (NombrePago = '" & Metodo & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SQLMetodo, MiConexion)
            DataAdapter.Fill(DataSet, "Metodo")
            If DataSet.Tables("Metodo").Rows.Count <> 0 Then
                Moneda = DataSet.Tables("Metodo").Rows(0)("Moneda")
                TipoMetodo = DataSet.Tables("Metodo").Rows(0)("TipoPago")
            End If
            DataSet.Tables("Metodo").Clear()


            Select Case Moneda
                Case "Cordobas"
                    TasaCambio = 1

                Case "Dolares"
                    SQLTasa = "SELECT  * FROM TasaCambio WHERE (FechaTasa = CONVERT(DATETIME, '" & Fecha & "', 102))"
                    DataAdapter = New SqlClient.SqlDataAdapter(SQLTasa, MiConexion)
                    DataAdapter.Fill(DataSet, "TasaCambio")
                    If DataSet.Tables("TasaCambio").Rows.Count <> 0 Then
                        TasaCambio = DataSet.Tables("TasaCambio").Rows(0)("MontoTasa")
                    Else
                        'TasaCambio = 0
                        MsgBox("La Tasa de Cambio no Existe para esta Fecha", MsgBoxStyle.Critical, "Sistema Facturacion")
                        FrmRecepcionPlanilla.BindingMetodo.Item(iPosicion)("Monto") = 0
                    End If
                    DataSet.Tables("TasaCambio").Clear()
            End Select

            If TipoMetodo = "Cambio" Then
                TasaCambio = TasaCambio * -1
            End If

            Monto = (FrmRecepcionPlanilla.BindingMetodo.Item(iPosicion)("Monto") * TasaCambio) + Monto
            iPosicion = iPosicion + 1
        Loop

        Registros = FrmRecepcionPlanilla.BindingDetalle.Count
        iPosicion = 0

        Do While iPosicion < Registros
            If Not IsDBNull(FrmRecepcionPlanilla.BindingDetalle.Item(iPosicion)("Importe")) Then
                Subtotal = CDbl(FrmRecepcionPlanilla.BindingDetalle.Item(iPosicion)("Importe")) + Subtotal
            End If
            iPosicion = iPosicion + 1
        Loop

        CodProducto = FrmRecepcionPlanilla.TrueDBGridComponentes.Columns(0).Text
        SQlString = "SELECT Productos.*  FROM Productos WHERE (Cod_Productos = '" & CodProducto & "') "
        DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
        DataAdapter.Fill(DataSet, "Productos")
        If Not DataSet.Tables("Productos").Rows.Count = 0 Then
            CodIva = DataSet.Tables("Productos").Rows(0)("Cod_Iva")
            SQlString = "SELECT *  FROM Impuestos WHERE  (Cod_Iva = '" & CodIva & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
            DataAdapter.Fill(DataSet, "IVA")
            If Not DataSet.Tables("IVA").Rows.Count = 0 Then
                Tasa = DataSet.Tables("IVA").Rows(0)("Impuesto")
            End If
        End If

        Iva = Subtotal * Tasa
        Neto = (Subtotal + Iva) - Monto
        FrmRecepcionPlanilla.TxtSubTotal.Text = Format(Subtotal, "##,##0.00")
        FrmRecepcionPlanilla.TxtIva.Text = Format(Iva, "##,##0.00")
        FrmRecepcionPlanilla.TxtPagado.Text = Format(Monto, "##,##0.00")
        FrmRecepcionPlanilla.TxtNetoPagar.Text = Format(Neto, "##,##0.00")
    End Sub
    Public Function SabadosMes(ByVal FechaNomina As Date) As Integer

        Dim MiDiaSemana As Byte, I As Integer
        Dim DiasMes As Integer
        Dim Sabados As Byte
        Dim Fecha As String
        Dim Dias As Date

        Fecha = CDate(FechaNomina)

        Dias = (DateSerial(Year(FechaNomina), Month(FechaNomina) + 1, 0))
        DiasMes = Dias.Day

        Sabados = 0

        For I = 1 To DiasMes
            'construyo la fecha
            Fecha = Str(I) + "/" + Str(Month(FechaNomina)) + "/" + Str(Year(FechaNomina))
            Fecha = CDate(Fecha)
            MiDiaSemana = Weekday(Fecha, vbMonday)
            If MiDiaSemana = 6 Then
                Sabados = Sabados + 1
            End If
        Next

        SabadosMes = Sabados

    End Function



    Public Function GuardarMes(ByVal iMes As Double, ByVal Fecha As Date) As String
        GuardarMes = 0
        Select Case iMes

            Case 1
                GuardarMes = "Enero"
            Case 2
                GuardarMes = "Febrero"
            Case 3
                GuardarMes = "Marzo"
            Case 4
                GuardarMes = "Abril"
            Case 5
                GuardarMes = "Mayo"
            Case 6
                GuardarMes = "Junio"
            Case 7
                GuardarMes = "Julio"
            Case 8
                GuardarMes = "Agosto"
            Case 9
                GuardarMes = "Septiembre"
            Case 10
                GuardarMes = "Octubre"
            Case 11
                GuardarMes = "Noviembre"
            Case 12
                GuardarMes = "Diciembre"
                FechaGuardar = Fecha.AddDays(6)
        End Select

    End Function


    Public Sub Semanas(ByVal a�o As Integer)

        Dim sCad As String
        Dim Fecha As Date
        Dim FechaFin As Date
        Dim iCont As Integer
        Dim iValor As Integer
        Dim iMes As Integer




        sCad = "01/01/"

        'Me.DtaA�o.RecordSource = "SELECT A�o, Actual, FecIni, FecFin From A�o_Fiscal Where (Actual = 1)"
        'Me.DtaA�o.Refresh()

        'If Not Me.DtaA�o.Recordset.EOF Then
        sCad = sCad + CStr(a�o)
        iA�o = a�o
        Fecha = sCad
        sCad = "31/12/"
        sCad = sCad + CStr(a�o)
        FechaFin = sCad
        'End If

        iValor = CInt(Mid$(Fecha, 4, 2))

        iMes = 1

        Do While Fecha <= (FechaFin.AddDays(6))

            If Format(Fecha, "dddd") = "Sabado" And iMes = iValor Then
                iCont = iCont + 1
                Fecha = Fecha.AddDays(8)

            ElseIf iMes <> iValor Then
                sCad = GuardarMes(iMes, Fecha)
                'Fecha = FechaGuardar
                iMes = iValor
                iCont = 0
            Else
                Fecha = DateAdd(DateInterval.Day, 1, Fecha)

            End If

            iValor = CInt(Mid$(Fecha, 4, 2))

        Loop

        'End If

        Exit Sub




        'Me.dtaSem.Recordset.AddNew
        'Me.dtaSem.Recordset.Fields(0) = iA�o
        'Me.dtaSem.Recordset.Fields(1) = sCad
        'Me.dtaSem.Recordset.Fields(2) = iCont
        'Me.dtaSem.UpdateRecord

        'Return




    End Sub


    Public Function DepurarFecha(ByVal TxtFecha As Date) As Boolean

        Dim iCont As Integer
        Dim sCad As String = ""
        Dim sCad2 As String = ""

        Do While iCont < 3


            Select Case iCont

                Case 0
                    sCad = Mid$(TxtFecha, 1, 2)
                    sCad2 = Mid$(TxtFecha, 4, 2)
                    sCad = Trim(sCad)
                    If Not IsNumeric(sCad) Then
                        DepurarFecha = True
                        Exit Function
                    ElseIf CInt(sCad2) = 1 Or CInt(sCad2) = 3 Or _
                           CInt(sCad2) = 5 Or CInt(sCad2) = 7 Or _
                           CInt(sCad2) = 8 Or CInt(sCad2) = 10 Or _
                           CInt(sCad2) = 12 Then
                        If Not (CInt(sCad) >= 1 And CInt(sCad) <= 31) Then
                            DepurarFecha = True
                            Exit Function
                        End If
                    ElseIf CInt(sCad2) = 2 Then
                        If Not (CInt(sCad) >= 1 And CInt(sCad) <= 28) Then
                            DepurarFecha = True
                            Exit Function
                        End If

                    ElseIf Not (CInt(sCad) >= 1 And CInt(sCad) <= 30) Then
                        DepurarFecha = True
                        Exit Function

                    ElseIf Not (CInt(sCad2) >= 1 And CInt(sCad2) <= 12) Then
                        DepurarFecha = True
                        Exit Function


                    End If

                Case 1
                    sCad = Mid$(TxtFecha, 4, 2)
                    sCad = Trim(sCad)
                Case 2
                    sCad = Mid$(TxtFecha, 7, 4)
                    sCad = Trim(sCad)

                    If Not IsNumeric(sCad) Then
                        If CInt(sCad) <= 1995 Then
                            DepurarFecha = True
                            Exit Function
                        End If

                    End If

            End Select

            If Not IsNumeric(sCad) Then
                DepurarFecha = True
                Exit Function
            End If



            iCont = iCont + 1


        Loop

        DepurarFecha = False

    End Function




    Public Sub GenerarPeriodo(ByVal Fecha1 As Date, ByVal Fecha2 As Date, ByVal A�o As Double, ByVal TipoPlanilla As String)
        Dim iPeriodo As Integer
        Dim saMes() As Object = {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"}, Lineas As Integer
        Dim saMes2() As Object = {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"}
        Dim iCont As Integer
        Dim Numfecha2 As String
        Dim iMes As Integer
        Dim iSem As Integer
        Dim FechaIni As Date
        Dim FechaFin As Date
        'Dim iA�o As Integer
        'Dim sIntervalo As String
        'Dim bBisiesto As Boolean
        Dim Fechas As Date

        iPeriodo = 1
        iSem = 1
        iCont = 0

        Select Case TipoPlanilla
            Case "Semanal Domingo"

                'If DepurarFecha(Me.DTPFechaInicio.Value) Then
                '    MsgBox("La Fecha Inicial no es v�lida, corrija", vbInformation)
                '    Me.DTPFechaInicio.Focus()
                '    Exit Sub
                'ElseIf DepurarFecha(Me.DTPFechaFin.Value) Then
                '    MsgBox("La Fecha Final no es v�lida, corrija", vbInformation)
                '    Me.DTPFechaFin.Focus()
                '    Exit Sub
                'End If



                FrmPeriodos.lstPlanilla.Items.Clear()
                FechaIni = Format(Fecha1, "dd/MM/yyyy")
                FechaFin = DateAdd(DateInterval.Day, 6, FechaIni)


                If Not IsNumeric(A�o) Then

                    MsgBox("El a�o actual de planilla no es v�lido", vbInformation, "Error!!!")
                    'Exit Function
                End If

                Semanas(Year(FechaFin))
                'Hay que setear el a�o actual de planilla OJO
                'Para que no de problemas
                Lineas = 1
                If FechaFin < Fecha2 Then
                    Do While FechaFin <= Fecha2  'Hay que setear el a�o actual de planilla OJO
                        'Me.dtaSem.Recordset.FindFirst "[Mes] like '" & saMes(iCont) & "' AND [A�o] like " & Me.DtaA�o.Recordset("A�o") & ""
                        'iMes = 'Me.dtaSem.Recordset.Fields(2)
                        Fechas = "01/" & saMes(iCont) & "/" & A�o
                        Numfecha2 = Fechas
                        iMes = SabadosMes(Numfecha2)
                        FrmPeriodos.lstPlanilla.Items.Add("        " & saMes2(iCont))


                        Do While iMes >= iSem 'And iPeriodo <= 52
                            'GuardarPeriodo
                            FrmPeriodos.lstPlanilla.Items.Add(" " & CStr(iPeriodo) & "   " & CStr(FechaIni) & "  al  " & CStr(FechaFin))
                            FechaIni = DateAdd(DateInterval.Day, 1, FechaFin)
                            FechaFin = DateAdd(DateInterval.Day, 6, FechaIni)


                            iSem = iSem + 1
                            iPeriodo = iPeriodo + 1
                            Lineas = Lineas + 1
                        Loop

                        iSem = 1
                        iCont = iCont + 1
                        FrmPeriodos.lstPlanilla.Items.Add("      ")
                        If Lineas = 14 Then
                            FrmPeriodos.lstPlanilla.Items.Add("      ")
                        End If
                    Loop

                Else
                    MsgBox("El intervalo de la fecha inicial y final no es v�lido", vbInformation, "corrija")
                End If


        End Select


        iPeriodo = 1
        iSem = 1
        iCont = 0
    End Sub


    Public Function TotalRecepcion(ByVal NumeroRecepcion As String, ByVal FechaRecepcion As Date, ByVal TipoRecepcion As String) As Double
        Dim Fecha As String, SqlCompras As String, Registros As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, i As Double, Total As Double

        Fecha = Format(FechaRecepcion, "yyyy-MM-dd")
        'SqlCompras = "SELECT  Cod_Productos, Descripcion_Producto, Codigo_Beams, Cantidad, Unidad_Medida,id_Eventos As Linea  FROM Detalle_Recepcion   WHERE (NumeroRecepcion = '" & NumeroRecepcion & "') AND (Fecha = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (TipoRecepcion = '" & TipoRecepcion & "') "
        SqlCompras = "SELECT  id_Eventos As Linea, Cod_Productos, Descripcion_Producto, Calidad, Estado, Cantidad, PesoKg, Tara, PesoNetoLb, PesoNetoKg, QQ As Saco, Precio  FROM Detalle_Recepcion WHERE (NumeroRecepcion = '" & NumeroRecepcion & "') AND (Fecha = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (TipoRecepcion = '" & TipoRecepcion & "') "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Recepcion")
        Registros = DataSet.Tables("Recepcion").Rows.Count
        i = 0
        Total = 0
        Do While Registros > i
            Total = Total + DataSet.Tables("Recepcion").Rows(i)("PesoNetoKg")
            i = i + 1
        Loop

        TotalRecepcion = Format(Total, "##,##0.00")

    End Function



    Public Sub ActualizaDetalleRecepcion(ByVal ConsecutivoRecepcion As String, ByVal TipoRecepcion As String)

        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Sql As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim Fecha As String

        Fecha = Format(CDate(FrmRecepcion.DTPFecha.Text), "yyyy-MM-dd")

        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////////////CARGO EL DETALLE DE COMPRAS/////////////////////////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Sql = "SELECT  id_Eventos As Linea, Cod_Productos, Descripcion_Producto, Calidad, Estado, Cantidad, PesoKg, Tara, PesoNetoLb, PesoNetoKg, QQ As Saco, Precio  FROM Detalle_Recepcion  " & _
              "WHERE  (NumeroRecepcion = '" & ConsecutivoRecepcion & "') AND (Fecha = CONVERT(DATETIME, '" & Format(CDate(Fecha), "yyyy-MM-dd") & "', 102)) AND (TipoRecepcion = '" & TipoRecepcion & "') "

        DataAdapter = New SqlClient.SqlDataAdapter(Sql, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleRecepcion")
        My.Forms.FrmRecepcion.BindingDetalle.DataSource = DataSet.Tables("DetalleRecepcion")
        My.Forms.FrmRecepcion.TrueDBGridComponentes.DataSource = My.Forms.FrmRecepcion.BindingDetalle
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Width = 40
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Locked = True
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Columns(0).Caption = "Psda"
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Precio").Visible = False
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Columns(1).Caption = "C�digo"
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Button = True
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Width = 63
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Columns(2).Caption = "Descripci�n"
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Width = 300
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Locked = True
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Columns(3).Caption = "Categ"
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Width = 50
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Locked = True
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Visible = False
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Columns(4).Caption = "Estado"
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Width = 50
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Locked = True
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Visible = False
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Width = 75
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Columns(5).Caption = "PesoLb"
        'Me.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Locked = True
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Width = 85
        'Me.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Button = True
        'Me.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Button = True
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(7).Width = 75
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(7).Locked = True
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(8).Width = 75
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(9).Width = 75
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(10).Width = 50
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(11).Width = 75
    End Sub



    Public Sub GrabaDetalleRecepcion(ByVal ConsecutivoRecepcion As String, ByVal CodigoProducto As String, ByVal Cantidad As Double, ByVal Linea As Double, ByVal Descripcion As String, ByVal Precio As Double, ByVal PesoKg As Double, ByVal TipoRecepcion As String, ByVal Tara As Double, ByVal PesoNetoKg As Double, ByVal QQ As Double)
        Dim Sqldetalle As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, PesoNetoLb As Double


        PesoNetoLb = Format((PesoNetoKg / 46) * 100, "##,##0.0000")


        Fecha = Format(CDate(FrmRecepcion.DTPFecha.Text), "yyyy-MM-dd")



        Sqldetalle = "SELECT Detalle_Recepcion.* FROM Detalle_Recepcion " & _
                     "WHERE (id_Eventos = " & Linea & ") AND (NumeroRecepcion = '" & ConsecutivoRecepcion & "') AND (Fecha = CONVERT(DATETIME, '" & Format(CDate(Fecha), "yyyy-MM-dd") & "', 102)) AND (TipoRecepcion = '" & TipoRecepcion & "') "   'AND (Cod_Productos = '" & CodigoProducto & "')
        DataAdapter = New SqlClient.SqlDataAdapter(Sqldetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleRecepcion")
        If Not DataSet.Tables("DetalleRecepcion").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlUpdate = "UPDATE [Detalle_Recepcion] SET [Cod_Productos] = '" & CodigoProducto & "',[Descripcion_Producto] = '" & Descripcion & "',[Cantidad] = " & Cantidad & ",[PesoKg] = " & PesoKg & ", [Precio] = " & Precio & ", [Tara] = " & Tara & ", [PesoNetoLb] = " & PesoNetoLb & ", [PesoNetoKg] = " & PesoNetoKg & " , [QQ] = " & QQ & " " & _
                        "WHERE (id_Eventos = " & Linea & ") AND (NumeroRecepcion = '" & ConsecutivoRecepcion & "') AND (Fecha = CONVERT(DATETIME, '" & Format(CDate(Fecha), "yyyy-MM-dd") & "', 102)) AND (TipoRecepcion = '" & TipoRecepcion & "') "  'AND (Cod_Productos = '" & CodigoProducto & "')
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else

            SqlUpdate = "INSERT INTO [Detalle_Recepcion] ([id_Eventos],[NumeroRecepcion],[Fecha],[TipoRecepcion],[Cod_Productos],[Descripcion_Producto],[Cantidad],[PesoKg],[Precio],[Tara],[PesoNetoLb],[PesoNetoKg],[QQ]) " & _
                        "VALUES (" & Linea & " ,'" & ConsecutivoRecepcion & "','" & Format(CDate(Fecha), "dd/MM/yyyy") & "','" & My.Forms.FrmRecepcion.CboTipoRecepcion.Text & "','" & CodigoProducto & "','" & Descripcion & "'," & Cantidad & "," & PesoKg & ", " & Precio & ", " & Tara & ", " & PesoNetoLb & ", " & PesoNetoKg & ", " & QQ & ")"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub



    Public Function BuscaLinea(ByVal NumeroRecepcion As String, ByVal FechaRecepcion As Date, ByVal TipoRecepcion As String) As Double
        Dim Sql As String, Fecha As Date
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), Registros As Double, i As Double, j As Double
        Dim iResultado As Integer, SqlUpdate As String, ComandoUpdate As New SqlClient.SqlCommand, Linea As Double = 0

        Fecha = Format(FechaRecepcion, "yyyy-MM-dd")

        Sql = "SELECT  Detalle_Recepcion.* FROM Detalle_Recepcion WHERE (NumeroRecepcion = '" & NumeroRecepcion & "')  AND (TipoRecepcion = '" & TipoRecepcion & "') AND (Fecha = CONVERT(DATETIME, '" & Format(FechaRecepcion, "yyyy-MM-dd") & "', 102))"
        'Sql = "SELECT id_Eventos, NumeroRecepcion, Fecha, TipoRecepcion, Cod_Productos, Descripcion_Producto, Codigo_Beams, Cantidad, Unidad_Medida, Liquidado,  Codigo_BeamsOrigen, RecepcionBin, Transferido, Calidad, Estado, Precio, PesoKg, Tara, PesoNetoLb, PesoNetoKg, QQ, Calidad_Cafe FROM Detalle_Recepcion " & _
        '      "WHERE (NumeroRecepcion = '" & NumeroRecepcion & "') AND (TipoRecepcion = '" & TipoRecepcion & "') AND (Fecha = CONVERT(DATETIME,  '" & Format(Fecha, "yyyy-MM-dd") & "', 102))"
        DataAdapter = New SqlClient.SqlDataAdapter(Sql, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleRecepcion")
        Registros = DataSet.Tables("DetalleRecepcion").Rows.Count
        i = 0
        j = 1
        Do While Registros > i
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            Linea = DataSet.Tables("DetalleRecepcion").Rows(i)("id_Eventos")
            SqlUpdate = "UPDATE [Detalle_Recepcion]  SET [id_Eventos] = " & j & " " & _
                        "WHERE (NumeroRecepcion = '" & NumeroRecepcion & "') AND (Fecha = CONVERT(DATETIME, '" & Format(FechaRecepcion, "yyyy-MM-dd") & "', 102)) AND (TipoRecepcion = '" & TipoRecepcion & "') AND (id_Eventos = " & Linea & ")"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()


            i = i + 1
            j = j + 1
        Loop
        BuscaLinea = j

    End Function


    Public Sub GrabaLecturaPeso(ByVal Peso As Double)
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)

        Dim ConsecutivoCompra As Double, NumeroRecepcion As String, Registros As Double, Iposicion As Double
        Dim Linea As Double, CodigoProducto As String, Cantidad As Double, Descripcion As String, CodigoBeams As String, UnidadMedida As String = ""
        Dim CodigoBeamsOrigen As String = "", CodigoRecepcionBin As String = "", Calidad As String, Estado As String, SqlString As String
        Dim DataSet As New DataSet, DataAdapterProductos As New SqlClient.SqlDataAdapter, PesoKg As Double, Precio As Double, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Tara As Double = 0, PesoNetoLb As Double = 0, PesoNetoKg As Double = 0, QQ As Double = 0, LugarAcopio As Integer, SubTotal As Double = 0
        Dim HumedadxDefecto As Double = 0, HumedadReal As Double = 0, Consecutivo As Double, NumeroRecibo As String, Cadena As String, CadenaDiv() As String
        Dim CodLugarAcopio As Double, Fecha As Date
        Dim Factor As Double = 0, IdEsdoFisico As Double = 0, IdCalidad As Double = 0, IdTipoLugarAcopio As Double = 0


        '////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////BUSCO EL CONSECUTIVO DE LA COMPRA /////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////7
        If FrmRecepcion.TxtNumeroEnsamble.Text = "-----0-----" Then
            Select Case FrmRecepcion.CboTipoRecepcion.Text
                Case "Recepcion"
                    ConsecutivoCompra = BuscaConsecutivo("Recepcion")
                Case "SalidaBascula"
                    ConsecutivoCompra = BuscaConsecutivo("SalidaBascula")

                Case "RePesaje"
                    ConsecutivoCompra = BuscaConsecutivo("ReImprime")
                Case "Lote"
                    ConsecutivoCompra = BuscaConsecutivo("Lote")
            End Select

            NumeroRecepcion = FrmRecepcion.CmbSerie.Text & "-" & Format(ConsecutivoCompra, "00000#")
        Else
            NumeroRecepcion = FrmRecepcion.TxtNumeroEnsamble.Text
        End If




        ''////////////////////////////////////////////////////////////////////////////////////////////////////////
        ''/////////////////////////////////BUSCO EL CONSECUTIVO DEL RECIBO ///////////////////////////////////////
        ''/////////////////////////////////////////////////////////////////////////////////////////////////////////
        'If FrmRecepcion.TxtNumeroRecibo.Text = "-----0-----" Then
        '    SqlString = "SELECT Codigo FROM ReciboCafePergamino WHERE (IdCosecha = " & FrmRecepcion.IdCosecha & ") AND (IdLocalidad = " & FrmRecepcion.IdLugarAcopio & ") AND (IdTipoCompra = " & FrmRecepcion.IdTipoCompra & ") AND (IdTipoCafe = " & FrmRecepcion.IdTipoCafe & ")  AND (LEN(Codigo) > 9) AND (Codigo LIKE '%" & CodLugarAcopio & "%') ORDER BY Codigo DESC"
        '    DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        '    DataAdapter.Fill(DataSet, "NumeroRecibo")
        '    If DataSet.Tables("NumeroRecibo").Rows.Count <> 0 Then
        '        Cadena = DataSet.Tables("NumeroRecibo").Rows(0)("Codigo")
        '        If Len(Cadena) >= 6 Then
        '            CadenaDiv = Cadena.Split("-")
        '            Consecutivo = CadenaDiv(1)
        '            Consecutivo = Consecutivo + 1
        '        End If
        '    Else
        '        Consecutivo = 1
        '    End If

        '    NumeroRecibo = Format(Consecutivo, "00000#")
        '    FrmRecepcion.TxtNumeroRecibo.Text = NumeroRecibo

        'Else
        '    NumeroRecibo = FrmRecepcion.TxtNumeroRecibo.Text
        'End If


        'If FrmRecepcion.CboTipoIngresoBascula.Text = "Manual" Then
        '    NumeroRecibo = FrmRecepcion.TxtNumeroRecibo.Text
        'End If





        '////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////GRABO ENCABEZADO DE RECEPCION /////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////7
        GrabaRecepcion(NumeroRecepcion)

        '////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////GRABO EL DETALLE DE LA RECEPCION /////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////7
        Registros = FrmRecepcion.BindingDetalle.Count
        Iposicion = FrmRecepcion.BindingDetalle.Position
        If My.Forms.FrmRecepcion.TrueDBGridComponentes.Columns(0).Text = "" Then
            Linea = BuscaLinea(NumeroRecepcion, CDate(My.Forms.FrmRecepcion.DTPFecha.Text), My.Forms.FrmRecepcion.CboTipoRecepcion.Text)
        Else
            Linea = FrmRecepcion.TrueDBGridComponentes.Columns(0).Text
        End If

        CodigoProducto = FrmRecepcion.CboCodigoProducto.Columns(0).Text
        Cantidad = Peso
        Descripcion = FrmRecepcion.CboCodigoProducto.Columns(1).Text

        'If FrmRecepcion.CboCategoria.Text <> "" Then
        '    Calidad = FrmRecepcion.CboCategoria.Text
        'End If

        'If FrmRecepcion.OptMojado.Checked = True Then
        '    Estado = "Mojado"
        'ElseIf FrmRecepcion.OptHumedo.Checked = True Then
        '    Estado = "Humedo"
        'ElseIf FrmRecepcion.OptOreado.Checked = True Then
        '    Estado = "Oreado"
        'End If

        'Estado = FrmRecepcion.CboEstado.Text


        '/////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////CONSULTO EL PRECIO DE VENTA //////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////
        'SqlString = "SELECT  Productos.* FROM Productos WHERE (Tipo_Producto <> 'Servicio') AND (Tipo_Producto <> 'Descuento')"
        'DataAdapterProductos = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        'DataAdapterProductos.Fill(DataSet, "Precios")
        'If Not DataSet.Tables("Precios").Rows.Count = 0 Then
        '    Select Case FrmRecepcion.CboTipoProducto.Text
        '        Case "A" : Precio = DataSet.Tables("Precios").Rows(0)("Precio_Venta")
        '        Case "B" : Precio = DataSet.Tables("Precios").Rows(0)("Precio_Lista")
        '        Case "C" : Precio = DataSet.Tables("Precios").Rows(0)("Precio_Compra")
        '    End Select

        'End If

        'Precio = PrecioVenta(CodigoProducto, FrmRecepcion.IdLugarAcopio, FrmRecepcion.CboCategoria.Text, CDate(FrmRecepcion.DTPFecha.Text))
        'If FrmRecepcion.CboTipoIngresoBascula.Text = DescripcionTipoIngreso("BA") Then
        '    Fecha = Format(CDate(FrmRecepcion.DTPFecha.Text), "yyyy-MM-dd") & " " & FrmRecepcion.LblHora.Text
        'Else
        '    Fecha = Format(CDate(FrmRecepcion.DtpFechaManual.Value), "yyyy-MM-dd") & " " & Format(FrmRecepcion.DtpHoraManual.Value, "hh:mm:ss tt")
        'End If

        Fecha = Format(CDate(FrmRecepcion.DTPFecha.Text), "yyyy-MM-dd") & " " & FrmRecepcion.LblHora.Text

        'Precio = PrecioVenta(FrmRecepcion.IdLugarAcopio, FrmRecepcion.IdCalidad, FrmRecepcion.CboCategoria.Text, Fecha)
        'Precio = Format(Precio / 46, "##,##0.00")
        Precio = 0

        '-------------------------------PREGUNTO LOS QUINTALES -----------------------------
        '--------------------------------------------------------------------------------------
        Factor = 0
        QQ = 0
        If FrmRecepcion.ChkTaraSaco.Checked = True Then
            Factor = 3
        End If

        My.Forms.FrmQQ.ShowDialog()
        QQ = My.Forms.FrmQQ.QQ

        '///////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////CONVERTIR DE LIBRAS A KG //////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////
        PesoKg = Cantidad
        Cantidad = Format((Cantidad / 46) * 100, "##,##0.00")


        '////////////////////////////////////BUSCO EL ESTADO FISICO ///////////////////////////////////////////////////


        'IdEsdoFisico = My.Forms.FrmRecepcion.IdEstadoFisico
        'IdCalidad = My.Forms.FrmRecepcion.IdCalidad
        'IdTipoLugarAcopio = My.Forms.FrmRecepcion.IdTipoLugarAcopio

        '////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////CONSULTO LAS TARAS /////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////
        'SqlString = "SELECT FactorTara FROM FactorSaco WHERE  (IdEdoFisico = " & IdEsdoFisico & " )  AND (IdTipoLugarAcopio = " & IdTipoLugarAcopio & ") AND (IdUMedida = 1) AND (IdCalidad = " & IdCalidad & ")"
        'DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        'DataAdapter.Fill(DataSet, "Tara")
        'If DataSet.Tables("Tara").Rows.Count <> 0 Then
        '    Factor = DataSet.Tables("Tara").Rows(0)("FactorTara")
        'Else
        '    Factor = 0
        'End If

        Tara = Factor * QQ


        'If FrmRecepcion.CboTipoCalidad.Text = "AP1ra" Then
        '    Select Case Estado
        '        Case "Mojado" : Tara = 0.46 * QQ
        '        Case "Humedo" : Tara = 0.23 * QQ
        '        Case "Oreado" : Tara = 0.23 * QQ
        '    End Select
        'ElseIf FrmRecepcion.CboTipoCalidad.Text = "AP2da" Then
        '    Select Case Estado
        '        Case "Mojado" : Tara = 0.46 * QQ
        '        Case "Humedo" : Tara = 0.23 * QQ
        '        Case "Oreado" : Tara = 0.23 * QQ
        '    End Select
        'ElseIf FrmRecepcion.CboTipoCalidad.Text = "BROZA" Then
        '    Select Case Estado
        '        Case "Mojado" : Tara = 0.46 * QQ
        '        Case "Humedo" : Tara = 0.23 * QQ
        '        Case "Oreado" : Tara = 0.23 * QQ
        '    End Select
        'ElseIf FrmRecepcion.CboTipoCalidad.Text = "FRUTO" Then
        '    Select Case Estado
        '        Case "Mojado" : Tara = 0.46 * QQ
        '        Case "Humedo" : Tara = 0.23 * QQ
        '        Case "Oreado" : Tara = 0.23 * QQ
        '    End Select
        'ElseIf FrmRecepcion.CboTipoCalidad.Text = "PULPON" Then
        '    Select Case Estado
        '        Case "Mojado" : Tara = 0.46 * QQ
        '        Case "Humedo" : Tara = 0.23 * QQ
        '        Case "Oreado" : Tara = 0.23 * QQ
        '    End Select
        'ElseIf FrmRecepcion.CboTipoCalidad.Text = "MP1ra" Then
        '    Select Case Estado
        '        Case "Mojado" : Tara = 0.46 * QQ
        '        Case "Humedo" : Tara = 0.23 * QQ
        '        Case "Oreado" : Tara = 0.23 * QQ
        '    End Select
        'End If

        PesoNetoKg = Format((PesoKg - Tara), "##,##0.0000")
        PesoNetoLb = Format((PesoNetoKg / 46) * 100, "##,##0.0000")

        GrabaDetalleRecepcion(NumeroRecepcion, CodigoProducto, Cantidad, Linea, Descripcion, Precio, PesoKg, FrmRecepcion.CboTipoRecepcion.Text, Tara, PesoNetoKg, QQ)
        ActualizaDetalleRecepcion(NumeroRecepcion, FrmRecepcion.CboTipoRecepcion.Text)


        FrmRecepcion.TrueDBGridComponentes.Columns(1).Text = CodigoProducto
        FrmRecepcion.TrueDBGridComponentes.Columns(2).Text = Descripcion
        FrmRecepcion.TrueDBGridComponentes.Columns(3).Text = Calidad
        FrmRecepcion.TrueDBGridComponentes.Columns(4).Text = Estado
        FrmRecepcion.TrueDBGridComponentes.Columns(5).Text = Cantidad
        FrmRecepcion.TrueDBGridComponentes.Columns(6).Text = PesoKg
        FrmRecepcion.TrueDBGridComponentes.Columns(7).Text = Tara
        FrmRecepcion.TrueDBGridComponentes.Columns(8).Text = PesoNetoLb
        FrmRecepcion.TrueDBGridComponentes.Columns(9).Text = PesoNetoKg
        FrmRecepcion.TrueDBGridComponentes.Columns(10).Text = QQ
        FrmRecepcion.TrueDBGridComponentes.Columns(11).Text = Precio
        FrmRecepcion.TrueDBGridComponentes.Columns(0).Text = Linea
        FrmRecepcion.TxtNumeroEnsamble.Text = NumeroRecepcion
        'FrmRecepcion.TxtNumeroRecibo.Text = NumeroRecibo


        Iposicion = FrmRecepcion.TrueDBGridComponentes.Row
        FrmRecepcion.TrueDBGridComponentes.Row = FrmRecepcion.TrueDBGridComponentes.Row + 1
        FrmRecepcion.TrueDBGridComponentes.Columns(1).Text = CodigoProducto
        FrmRecepcion.TrueDBGridComponentes.Columns(2).Text = Descripcion
        FrmRecepcion.TrueDBGridComponentes.Col = 5


        FrmRecepcion.txtsubtotal.Text = TotalRecepcion(FrmRecepcion.TxtNumeroEnsamble.Text, FrmRecepcion.DTPFecha.Text, FrmRecepcion.CboTipoRecepcion.Text)


        ''////////////////////////////////////////////BUSCO LA RELACION ENTRE CALIDAD /////////////////////////////////////
        'SqlString = "SELECT  EstadoFisico, Codigo, Descripcion, HumedadInicial, HumedadFinal, HumedadXDefecto  FROM EstadoFisico WHERE (Descripcion = '" & FrmRecepcion.CboEstado.Text & "')"
        'DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        'DataAdapter.Fill(DataSet, "Consulta")
        'If DataSet.Tables("Consulta").Rows.Count <> 0 Then
        '    HumedadxDefecto = DataSet.Tables("Consulta").Rows(0)("HumedadXDefecto")
        'End If

        SubTotal = FrmRecepcion.txtsubtotal.Text
        'HumedadxDefecto = FrmRecepcion.HumedadxDefecto
        'HumedadReal = FrmRecepcion.TxtHumedad.Text

        'If CalcularEqOreado(My.Forms.FrmRecepcion.IdCosecha, IdEsdoFisico) = True Then
        '    FrmRecepcion.TxtEqOreado.Text = Format(SubTotal * (1 - (HumedadxDefecto - 42) / 100), "##,##0.00")
        '    FrmRecepcion.TxtOreadoReal.Text = Format(SubTotal * (1 - (HumedadReal - 42) / 100), "##,##0.00")
        'Else
        '    FrmRecepcion.TxtEqOreado.Text = Format(SubTotal, "##,##0.00")
        '    FrmRecepcion.TxtOreadoReal.Text = Format(SubTotal, "##,##0.00")
        'End If




    End Sub

    Public Function SoloNumeros(ByVal strCadena As String) As Object
        Dim SoloNumero As String = ""
        Dim index As Integer
        For index = 1 To Len(strCadena)
            If (Mid$(strCadena, index, 1) Like "#") _
                Or Mid$(strCadena, index, 1) = "-" Or Mid$(strCadena, index, 1) = "." Then
                SoloNumero = SoloNumero & Mid$(strCadena, index, 1)
            End If
        Next

        If Not IsNumeric(SoloNumero) Then
            SoloNumero = ""
        End If

        Return SoloNumero
    End Function


    Public Sub GrabaRecepcion(ByVal ConsecutivoRecepcion As String)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Subtotal As Double, Lote As String, TipoProceso As String = "", idVehiculo As Double, CodConductor As String, TipoPesada As String
        Dim Procesar As Double


        Dim DateFecha As DateTime = FrmRecepcion.DTPFecha.Text
        Fecha = Format(DateFecha, "yyyy-MM-dd") & " " & FrmRecepcion.LblHora.Text

        'Fecha = Format(CDate(FrmRecepcion.DTPFecha.Text), "yyyy-MM-dd")
        Lote = FrmRecepcion.A�o & "-" & FrmRecepcion.Mes & "-" & FrmRecepcion.Dia

        idVehiculo = My.Forms.FrmRecepcion.CboPlaca.Columns(0).Text
        CodConductor = My.Forms.FrmRecepcion.CboConductor.Columns(0).Text
        TipoPesada = My.Forms.FrmRecepcion.CmbTipoPesada.Text


        If FrmRecepcion.txtsubtotal.Text <> "" Then
            Subtotal = FrmRecepcion.txtsubtotal.Text
        Else
            Subtotal = 0
        End If


        If My.Forms.FrmRecepcion.Procesar = True Then
            Procesar = 1
        Else
            Procesar = 0
        End If

        MiConexion.Close()

        If FrmRecepcion.TxtNumeroEnsamble.Text = "-----0-----" Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////

            SqlCompras = "INSERT INTO Recepcion ([NumeroRecepcion],[Fecha],[TipoRecepcion],[Cod_Proveedor],[Conductor] ,[Id_identificacion] ,[Id_Vehiculo],[Cod_Bodega],[Observaciones],[SubTotal],[Lote],[FechaHora],[Contabilizado],[TipoPesada]) " & _
                         "VALUES ('" & ConsecutivoRecepcion & "','" & Format(CDate(Fecha), "dd/MM/yyyy") & "', '" & My.Forms.FrmRecepcion.CboTipoRecepcion.Text & "' ,'" & My.Forms.FrmRecepcion.CboCodigoProveedor.Columns(0).Text & "' ,'" & CodConductor & "' ,'" & My.Forms.FrmRecepcion.txtid.Text & "' ,'" & idVehiculo & "' ,'" & FrmRecepcion.CboCodigoBodega.Text & "' ,'" & FrmRecepcion.txtobservaciones.Text & "' ,'" & Subtotal & "' ,'" & Lote & "','" & Format(CDate(Fecha), "dd/MM/yyyy HH:mm:ss") & "', " & Procesar & ", '" & TipoPesada & "'  ) "

            'SqlCompras = "INSERT INTO [Recepcion] ([NumeroRecepcion],[Fecha],[TipoRecepcion],[Cod_Proveedor],[Conductor],[Id_identificacion],[Id_Vehiculo],[Cod_Bodega],[Observaciones],[SubTotal],[Lote]) " & _
            '             "VALUES ('" & ConsecutivoRecepcion & "','" & Format(FrmRecepcion.DTPFecha.Value, "dd/MM/yyyy") & "','" & FrmRecepcion.CboTipoRecepcion.Text & "','" & FrmRecepcion.CboCodigoProveedor.Columns(0).Text & "','" & FrmRecepcion.CboConductor.Text & "', '" & FrmRecepcion.txtid.Text & "','" & FrmRecepcion.txtplaca.Text & "','" & FrmRecepcion.CboCodigoBodega.Columns(0).Text & "','" & FrmRecepcion.txtobservaciones.Text & "','" & Subtotal & "','" & Lote & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "UPDATE [Recepcion] SET [Cod_Proveedor] = '" & FrmRecepcion.CboCodigoProveedor.Columns(0).Text & "',[Conductor] = '" & CodConductor & "',[Id_identificacion] ='" & FrmRecepcion.txtid.Text & "',[Id_Vehiculo] = '" & idVehiculo & "',[Observaciones] = '" & FrmRecepcion.txtobservaciones.Text & "',[SubTotal] = '" & Subtotal & "',[Lote] = '" & Lote & "', [Contabilizado] = " & Procesar & " ,[TipoPesada] = '" & TipoPesada & "'" & _
                         "WHERE (NumeroRecepcion = '" & ConsecutivoRecepcion & "') AND (TipoRecepcion = '" & FrmRecepcion.CboTipoRecepcion.Text & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If

        If FrmRecepcion.Procesar = True Then




        End If




        FrmRecepcion.Procesar = False

    End Sub




    Public Sub LimpiaRecepcion()
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Sql As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)

        FrmRecepcion.CboCodigoProveedor.Text = ""
        FrmRecepcion.TxtNumeroEnsamble.Text = "-----0-----"
        FrmRecepcion.txtobservaciones.Text = ""
        FrmRecepcion.txtsubtotal.Text = ""
        FrmRecepcion.txtnombre.Text = ""
        FrmRecepcion.LblPeso.Text = "0.00"
        FrmRecepcion.Timer1.Start()
        FrmRecepcion.Procesar = False

        FrmRecepcion.Button7.Enabled = True
        FrmRecepcion.Button10.Enabled = True
        FrmRecepcion.Button11.Enabled = True
        FrmRecepcion.BtnProcesar.Enabled = True

        FrmRecepcion.Button6.Enabled = True
        FrmRecepcion.Button7.Enabled = True
        FrmRecepcion.Button10.Enabled = True
        FrmRecepcion.Button11.Enabled = True
        FrmRecepcion.BtnProcesar.Enabled = True
        FrmRecepcion.TrueDBGridComponentes.Enabled = True
        FrmRecepcion.GroupBox6.Enabled = True
        FrmRecepcion.GroupBox1.Enabled = True
        FrmRecepcion.GroupBox2.Enabled = True
        FrmRecepcion.GroupBox9.Enabled = True


        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////////////CARGO EL DETALLE DE COMPRAS/////////////////////////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Sql = "SELECT  id_Eventos As Linea, Cod_Productos, Descripcion_Producto, Calidad, Estado, Cantidad, PesoKg, Tara, PesoNetoLb, PesoNetoKg, QQ As Saco, Precio  FROM Detalle_Recepcion  WHERE (NumeroRecepcion = N'-100000')"
        DataAdapter = New SqlClient.SqlDataAdapter(Sql, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleRecepcion")
        My.Forms.FrmRecepcion.BindingDetalle.DataSource = DataSet.Tables("DetalleRecepcion")
        My.Forms.FrmRecepcion.TrueDBGridComponentes.DataSource = My.Forms.FrmRecepcion.BindingDetalle
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Width = 40
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Locked = True
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Columns(0).Caption = "Psda"

        My.Forms.FrmRecepcion.TrueDBGridComponentes.Columns(1).Caption = "C�digo"
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Button = True
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Width = 63
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Columns(2).Caption = "Descripci�n"
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Width = 200
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Locked = True
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Columns(3).Caption = "Calidad"
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Width = 50
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Locked = True
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Visible = False
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Columns(4).Caption = "Estado"
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Width = 50
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Locked = True
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Visible = False

        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Width = 75

        My.Forms.FrmRecepcion.TrueDBGridComponentes.Columns(5).Caption = "PesoLb"
        'Me.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Locked = True
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Width = 85
        'Me.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Button = True
        'Me.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Button = True
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(7).Width = 75
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(7).Locked = True
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(8).Width = 75
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(9).Width = 75
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(10).Width = 50
        My.Forms.FrmRecepcion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(11).Width = 75


    End Sub


    Public Sub AuditoriaSistema()
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim Fecha As Date = Format(Now, "dd/MM/yyyy"), FechaIni As Date, FechaFin As Date
        Dim SqlString As String, Registro As Double, i As Double
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim TipoFactura As String, Excento As Boolean = False, NumeroFactura As String, FechaFactura As Date
        Dim SubTotal As Double, Iva As Double, SubTotal2 As Double, Iva2 As Double
        Dim StrSqlUpdate As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer

        '/////////////////////////////////////BUSCO EL TOTAL DE LAS FACTURAS PARA ESTE MES /////////////////////////////////////////////////
        FechaIni = DateSerial(Year(Fecha), Month(Fecha), 1)
        FechaFin = DateSerial(Year(Fecha), Month(Fecha) + 1, 0)

        SqlString = "SELECT  *   FROM Facturas WHERE (Fecha_Factura BETWEEN CONVERT(DATETIME, '" & Format(FechaIni, "yyyy-MM-dd") & "', 102) AND CONVERT(DATETIME, '" & Format(FechaFin, "yyyy-MM-dd") & "', 102)) ORDER BY Numero_Factura"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Facturas")
        Registro = DataSet.Tables("Facturas").Rows.Count

        i = 0
        Excento = False
        Do While Registro > i
            TipoFactura = DataSet.Tables("Facturas").Rows(i)("Tipo_Factura")
            Excento = DataSet.Tables("Facturas").Rows(i)("Tipo_Factura")
            NumeroFactura = DataSet.Tables("Facturas").Rows(i)("Numero_Factura")
            FechaFactura = DataSet.Tables("Facturas").Rows(i)("Fecha_Factura")
            SubTotal = DataSet.Tables("Facturas").Rows(i)("SubTotal")
            Iva = DataSet.Tables("Facturas").Rows(i)("IVA")

            '////////////////////////////////////////////VERIFICO QUE EL DETALLE DE FACTURA SEA IGUAL ////////
            'SqlString = "SELECT     Numero_Factura, SUM(Importe) AS Importe, SUM(Precio_Neto * Cantidad) AS Neto  FROM Detalle_Facturas WHERE (Tipo_Factura = '" & TipoFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Format(FechaFactura, "yyyy-MM-dd") & "', 102)) GROUP BY Numero_Factura HAVING  (Numero_Factura = '" & NumeroFactura & "')"
            SqlString = "SELECT  Detalle_Facturas.Numero_Factura, SUM(Detalle_Facturas.Importe) AS Importe, SUM(Detalle_Facturas.Precio_Neto * Detalle_Facturas.Cantidad) AS Neto, SUM(ROUND(Detalle_Facturas.Precio_Neto * Detalle_Facturas.Cantidad * Impuestos.Impuesto, 4)) AS IVA FROM Detalle_Facturas INNER JOIN Productos ON Detalle_Facturas.Cod_Producto = Productos.Cod_Productos INNER JOIN Impuestos ON Productos.Cod_Iva = Impuestos.Cod_Iva  " & _
                        "WHERE (Detalle_Facturas.Tipo_Factura = '" & TipoFactura & "') AND (Detalle_Facturas.Fecha_Factura = CONVERT(DATETIME, '" & Format(FechaFactura, "yyyy-MM-dd") & "', 102)) GROUP BY Detalle_Facturas.Numero_Factura HAVING  (Detalle_Facturas.Numero_Factura = '" & NumeroFactura & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.Fill(DataSet, "DetalleFacturas")
            If DataSet.Tables("DetalleFacturas").Rows.Count <> 0 Then

                SubTotal2 = DataSet.Tables("DetalleFacturas").Rows(i)("Neto")

                If SubTotal <> SubTotal2 Then
                    '////////////////////////SI LOS SUBTOTAL SON DISTINTOS ENTONCES CALCULO EL IVA ////////////////////
                    SubTotal = SubTotal2


                    If Excento = False Then
                        Iva2 = DataSet.Tables("DetalleFacturas").Rows(i)("IVA")
                        If Iva <> Iva2 Then






                            SqlString = "UPDATE Facturas    SET [SubTotal] = " & SubTotal2 & " ,[IVA] = " & Iva2 & "  " & _
                                        "WHERE (Detalle_Facturas.Tipo_Factura = '" & TipoFactura & "') AND (Detalle_Facturas.Fecha_Factura = CONVERT(DATETIME, '" & Format(FechaFactura, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Numero_Factura = '" & NumeroFactura & "')"
                            ComandoUpdate = New SqlClient.SqlCommand(SqlString, MiConexion)
                            iResultado = ComandoUpdate.ExecuteNonQuery
                            MiConexion.Close()

                        End If

                    End If
                End If





            End If


            i = i + 1
        Loop


    End Sub
    Public Sub CalcularSaldoClienteMain(ByVal FechaFin As Date, ByVal CodigoCliente As String)
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim SQlString As String, NumeroFactura As String, NumeroRecibo As String = "", MontoRecibo As Double
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, Registros As Double, i As Double
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer = 0, TasaCambio As Double, Saldo As Double
        Dim MontoFactura As Double, FechaFactura As Date, Dias As Double, TasaInteres As Double, MontoMora As Double, Total As Double
        Dim Registros2 As Double, j As Double, TasaCambioRecibo As Double, TotalFactura As Double = 0, TotalAbonos As Double = 0, TotalCargos As Double = 0
        Dim TotalMora As Double = 0, FechaVence As Date, NumeroNota As String = "", MontoNota As Double = 0, NumeroNotaCR As String = "", MontoNotaCR As Double = 0, TotalMontoNotaCR As Double = 0, TotalMontoNotaDB As Double = 0
        Dim MontoMetodoFactura As Double = 0, TipoNota As String = "", Moneda As String

        'oDataRow As DataRow, 


        If CodigoCliente = "" Then
            Exit Sub
        End If

        'FechaFin = Format(Now, "dd/MM/yyyy")

        'Moneda = Me.TxtMonedaFactura.Text

        '*******************************************************************************************************************************
        '/////////////////////////AGREGO UNA CONSULTA QUE NUNCA TENDRA REGISTROS PARA PODER AGREGARLOS /////////////////////////////////
        '*******************************************************************************************************************************
        'DataSet.Reset()
        'DatasetReporte.Reset()
        'SQlString = "SELECT Facturas.Fecha_Factura, Facturas.Numero_Factura, Facturas.MetodoPago As Numero_Recibo, Facturas.Numero_Factura As NotaDebito, Facturas.SubTotal As MontoNota, Facturas.SubTotal As Monto, Facturas.Fecha_Factura As FechaVence, Facturas.IVA As Abono, Facturas.SubTotal AS Saldo, Facturas.SubTotal As Moratorio, Facturas.SubTotal As Dias, Facturas.SubTotal AS Total  FROM Facturas INNER JOIN Clientes ON Facturas.Cod_Cliente = Clientes.Cod_Cliente  " & _
        '            "WHERE (Facturas.Tipo_Factura = 'Factura') AND (Facturas.MetodoPago = 'Credito') AND (Facturas.Fecha_Factura BETWEEN CONVERT(DATETIME, '01/01/1900', 102) AND CONVERT(DATETIME, '01/01/1900', 102)) ORDER BY Facturas.Fecha_Factura, Facturas.Numero_Factura"
        'DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
        'DataAdapter.Fill(DatasetReporte, "TotalVentas")


        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////AGREGO LA CONSULTA PARA TODAS LAS FACTURAS DE CREDITO //////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SQlString = "SELECT *  FROM Facturas WHERE (MetodoPago = 'Credito') AND (Tipo_Factura = 'Factura') AND (Cod_Cliente = '" & codigocliente  & "') AND (Nombre_Cliente <> N'******CANCELADO') AND (Fecha_Factura <= CONVERT(DATETIME, '" & Format(FechaFin, "yyyy-MM-dd") & "', 102))"
        SQlString = "SELECT *  FROM Facturas WHERE (Tipo_Factura = 'Factura') AND (Cod_Cliente = '" & CodigoCliente & "') AND (Nombre_Cliente <> N'******CANCELADO') AND (Fecha_Factura <= CONVERT(DATETIME, '" & Format(FechaFin, "yyyy-MM-dd") & "', 102))"
        DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
        DataAdapter.Fill(DataSet, "Clientes")
        Registros = DataSet.Tables("Clientes").Rows.Count
        i = 0
        TotalFactura = 0
        TotalAbonos = 0
        TotalMora = 0
        TotalCargos = 0
        TotalMontoNotaDB = 0
        TotalMontoNotaCR = 0

        Do While Registros > i
            NumeroRecibo = ""
            MontoRecibo = 0

            My.Application.DoEvents()



            If Moneda = "Cordobas" Then
                If DataSet.Tables("Clientes").Rows(i)("MonedaFactura") = "Cordobas" Then
                    TasaCambio = 1
                Else
                    TasaCambio = BuscaTasaCambio(DataSet.Tables("Clientes").Rows(i)("Fecha_Factura"))
                End If
            Else
                If DataSet.Tables("Clientes").Rows(i)("MonedaFactura") = "Dolares" Then
                    TasaCambio = 1
                Else
                    TasaCambio = 1 / BuscaTasaCambio(DataSet.Tables("Clientes").Rows(i)("Fecha_Factura"))
                End If
            End If

            NumeroFactura = DataSet.Tables("Clientes").Rows(i)("Numero_Factura")
            FechaFactura = DataSet.Tables("Clientes").Rows(i)("Fecha_Factura")
            FechaVence = DataSet.Tables("Clientes").Rows(i)("Fecha_Vencimiento")
            'Me.Text = "Procesando Factura No " & NumeroFactura

            If NumeroFactura = "01200" Then
                NumeroFactura = "01200"
            End If

            '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '//////////////////////////////////////BUSCO SI EXISTEN RECIBOS PARA LA FACTURA //////////////////////////////////////////////////////
            '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            'SQlString = "SELECT  MAX(DetalleRecibo.CodReciboPago) AS CodReciboPago, MAX(DetalleRecibo.Fecha_Recibo) AS Fecha_Recibo, DetalleRecibo.Numero_Factura, SUM(DetalleRecibo.MontoPagado) AS MontoPagado, Recibo.MonedaRecibo FROM DetalleRecibo INNER JOIN Recibo ON DetalleRecibo.CodReciboPago = Recibo.CodReciboPago AND DetalleRecibo.Fecha_Recibo = Recibo.Fecha_Recibo WHERE (DetalleRecibo.Fecha_Recibo <= CONVERT(DATETIME, '" & Format(FechaFin, "yyyy-MM-dd") & "', 102)) GROUP BY DetalleRecibo.Numero_Factura, Recibo.MonedaRecibo, Recibo.Cod_Cliente " & _
            '            "HAVING (DetalleRecibo.Numero_Factura = '" & NumeroFactura & "') AND (Recibo.Cod_Cliente = '" & codigocliente  & "')"

            'SQlString = "SELECT MAX(DetalleRecibo.CodReciboPago) AS CodReciboPago, DetalleRecibo.Fecha_Recibo, DetalleRecibo.Numero_Factura, SUM(DetalleRecibo.MontoPagado) AS MontoPagado, Recibo.MonedaRecibo FROM DetalleRecibo INNER JOIN Recibo ON DetalleRecibo.CodReciboPago = Recibo.CodReciboPago AND DetalleRecibo.Fecha_Recibo = Recibo.Fecha_Recibo WHERE  (DetalleRecibo.Fecha_Recibo <= CONVERT(DATETIME, '" & Format(FechaFin, "yyyy-MM-dd") & "', 102)) GROUP BY DetalleRecibo.Numero_Factura, Recibo.MonedaRecibo, Recibo.Cod_Cliente, DetalleRecibo.Fecha_Recibo HAVING (DetalleRecibo.Numero_Factura = '" & NumeroFactura & "') AND (Recibo.Cod_Cliente = '" & codigocliente  & "')"
            'SQlString = "SELECT DetalleRecibo.CodReciboPago, DetalleRecibo.Fecha_Recibo, DetalleRecibo.Numero_Factura, DetalleRecibo.MontoPagado, CASE WHEN Recibo.MonedaRecibo = 'Cordobas' THEN DetalleRecibo.MontoPagado ELSE DetalleRecibo.MontoPagado * TasaCambio.MontoTasa END AS MontoCordobas, CASE WHEN Recibo.MonedaRecibo = 'Dolares' THEN DetalleRecibo.MontoPagado ELSE DetalleRecibo.MontoPagado / TasaCambio.MontoTasa END AS MontoDolares, Recibo.Cod_Cliente FROM DetalleRecibo INNER JOIN Recibo ON DetalleRecibo.CodReciboPago = Recibo.CodReciboPago AND DetalleRecibo.Fecha_Recibo = Recibo.Fecha_Recibo INNER JOIN TasaCambio ON Recibo.Fecha_Recibo = TasaCambio.FechaTasa WHERE (DetalleRecibo.Fecha_Recibo <= CONVERT(DATETIME, '" & Format(FechaFin, "yyyy-MM-dd") & "', 102)) AND (Recibo.Cod_Cliente = '" & codigocliente & "')"

            SQlString = "SELECT DetalleRecibo.CodReciboPago, DetalleRecibo.Fecha_Recibo, DetalleRecibo.Numero_Factura, DetalleRecibo.MontoPagado, CASE WHEN Recibo.MonedaRecibo = 'Cordobas' THEN DetalleRecibo.MontoPagado ELSE DetalleRecibo.MontoPagado * TasaCambio.MontoTasa END AS MontoCordobas, CASE WHEN Recibo.MonedaRecibo = 'Dolares' THEN DetalleRecibo.MontoPagado ELSE DetalleRecibo.MontoPagado / TasaCambio.MontoTasa END AS MontoDolares, Recibo.Cod_Cliente, Recibo.MonedaRecibo FROM DetalleRecibo INNER JOIN Recibo ON DetalleRecibo.CodReciboPago = Recibo.CodReciboPago AND DetalleRecibo.Fecha_Recibo = Recibo.Fecha_Recibo INNER JOIN TasaCambio ON Recibo.Fecha_Recibo = TasaCambio.FechaTasa WHERE (Recibo.Cod_Cliente = '" & CodigoCliente & "') AND (DetalleRecibo.Fecha_Recibo <= CONVERT(DATETIME, '" & Format(FechaFin, "yyyy-MM-dd") & "', 102)) AND " & _
                         "(DetalleRecibo.Numero_Factura = '" & NumeroFactura & "') "

            DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
            DataAdapter.Fill(DataSet, "Recibos")
            Registros2 = DataSet.Tables("Recibos").Rows.Count
            j = 0

            Do While Registros2 > j

                If Moneda = "Cordobas" Then
                    If DataSet.Tables("Recibos").Rows(j)("MonedaRecibo") = "Cordobas" Then
                        TasaCambioRecibo = 1
                    Else
                        TasaCambioRecibo = BuscaTasaCambio(DataSet.Tables("Recibos").Rows(j)("Fecha_Recibo"))
                    End If
                Else
                    If DataSet.Tables("Recibos").Rows(j)("MonedaRecibo") = "Dolares" Then
                        TasaCambioRecibo = 1
                    Else
                        TasaCambioRecibo = 1 / BuscaTasaCambio(DataSet.Tables("Recibos").Rows(j)("Fecha_Recibo"))
                    End If
                End If
                If NumeroRecibo = "" Then
                    NumeroRecibo = DataSet.Tables("Recibos").Rows(j)("CodReciboPago")
                Else
                    NumeroRecibo = NumeroRecibo & "," & DataSet.Tables("Recibos").Rows(j)("CodReciboPago")
                End If
                MontoRecibo = MontoRecibo + DataSet.Tables("Recibos").Rows(j)("MontoPagado") * TasaCambioRecibo

                j = j + 1
            Loop

            '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '//////////////////////////////////////BUSCO SI EXISTEN NOTAS DE DEBITO PARA ESTA FACTURA //////////////////////////////////////////////////////
            '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            'SQlString = "SELECT Detalle_Nota.*, NotaDebito.Tipo, IndiceNota.MonedaNota FROM Detalle_Nota INNER JOIN NotaDebito ON Detalle_Nota.Tipo_Nota = NotaDebito.CodigoNB INNER JOIN IndiceNota ON Detalle_Nota.Numero_Nota = IndiceNota.Numero_Nota WHERE  (NotaDebito.Tipo = 'Debito Clientes') AND (Detalle_Nota.Numero_Factura = '" & NumeroFactura & "')"
            SQlString = "SELECT Detalle_Nota.id_Detalle_Nota, Detalle_Nota.Numero_Nota, Detalle_Nota.Fecha_Nota, Detalle_Nota.Tipo_Nota, Detalle_Nota.CodigoNB, Detalle_Nota.Descripcion, Detalle_Nota.Numero_Factura, Detalle_Nota.Monto, NotaDebito.Tipo, IndiceNota.MonedaNota, IndiceNota.Fecha_Nota AS Expr1, IndiceNota.Tipo_Nota AS Expr2 FROM Detalle_Nota INNER JOIN NotaDebito ON Detalle_Nota.Tipo_Nota = NotaDebito.CodigoNB INNER JOIN IndiceNota ON Detalle_Nota.Numero_Nota = IndiceNota.Numero_Nota AND Detalle_Nota.Fecha_Nota = IndiceNota.Fecha_Nota AND Detalle_Nota.Tipo_Nota = IndiceNota.Tipo_Nota WHERE (Detalle_Nota.Fecha_Nota <= CONVERT(DATETIME, '" & Format(FechaFin, "yyyy-MM-dd") & "', 102)) AND (NotaDebito.Tipo LIKE '%Debito Clientes%') AND (Detalle_Nota.Numero_Factura = '" & NumeroFactura & "') AND (IndiceNota.Cod_Cliente = '" & CodigoCliente & "')"

            DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
            DataAdapter.Fill(DataSet, "NotaDB")
            Registros2 = DataSet.Tables("NotaDB").Rows.Count
            NumeroNota = ""
            j = 0
            MontoNota = 0
            TotalMontoNotaDB = 0
            Do While Registros2 > j

                TipoNota = DataSet.Tables("NotaDB").Rows(j)("Tipo")

                If Moneda = "Cordobas" Then
                    If TipoNota <> "Debito Clientes Dif $" Then
                        If DataSet.Tables("NotaDB").Rows(j)("MonedaNota") = "Cordobas" Then
                            TasaCambioRecibo = 1
                        Else
                            TasaCambioRecibo = BuscaTasaCambio(DataSet.Tables("NotaDB").Rows(j)("Fecha_Nota"))
                        End If
                    Else
                        TasaCambioRecibo = 0
                    End If
                Else
                    If TipoNota <> "Debito Clientes Dif C$" Then
                        If DataSet.Tables("NotaDB").Rows(j)("MonedaNota") = "Dolares" Then
                            TasaCambioRecibo = 1
                        Else
                            TasaCambioRecibo = 1 / BuscaTasaCambio(DataSet.Tables("NotaDB").Rows(j)("Fecha_Nota"))
                        End If
                    Else
                        TasaCambioRecibo = 0
                    End If
                End If


                If NumeroNota = "" Then
                    NumeroNota = DataSet.Tables("NotaDB").Rows(j)("Numero_Nota")
                Else
                    NumeroNota = NumeroNota & "," & DataSet.Tables("NotaDB").Rows(j)("Numero_Nota")
                End If
                MontoNota = DataSet.Tables("NotaDB").Rows(j)("Monto") * TasaCambioRecibo
                TotalMontoNotaDB = TotalMontoNotaDB + MontoNota
                j = j + 1
            Loop

            DataSet.Tables("NotaDB").Reset()
            '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '//////////////////////////////////////BUSCO SI EXISTEN NOTAS DE CREDITO PARA ESTA FACTURA //////////////////////////////////////////////////////
            '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            'SQlString = "SELECT Detalle_Nota.*, NotaDebito.Tipo, IndiceNota.MonedaNota FROM Detalle_Nota INNER JOIN NotaDebito ON Detalle_Nota.Tipo_Nota = NotaDebito.CodigoNB INNER JOIN IndiceNota ON Detalle_Nota.Numero_Nota = IndiceNota.Numero_Nota WHERE  (NotaDebito.Tipo = 'Credito Clientes') AND (Detalle_Nota.Numero_Factura = '" & NumeroFactura & "')"
            SQlString = "SELECT Detalle_Nota.id_Detalle_Nota, Detalle_Nota.Numero_Nota, Detalle_Nota.Fecha_Nota, Detalle_Nota.Tipo_Nota, Detalle_Nota.CodigoNB, Detalle_Nota.Descripcion, Detalle_Nota.Numero_Factura, Detalle_Nota.Monto, NotaDebito.Tipo, IndiceNota.MonedaNota, IndiceNota.Fecha_Nota AS Expr1, IndiceNota.Tipo_Nota AS Expr2 FROM Detalle_Nota INNER JOIN NotaDebito ON Detalle_Nota.Tipo_Nota = NotaDebito.CodigoNB INNER JOIN IndiceNota ON Detalle_Nota.Numero_Nota = IndiceNota.Numero_Nota AND Detalle_Nota.Fecha_Nota = IndiceNota.Fecha_Nota AND Detalle_Nota.Tipo_Nota = IndiceNota.Tipo_Nota WHERE (Detalle_Nota.Fecha_Nota <= CONVERT(DATETIME, '" & Format(FechaFin, "yyyy-MM-dd") & "', 102)) AND (NotaDebito.Tipo LIKE '%Credito Clientes%') AND (Detalle_Nota.Numero_Factura = '" & NumeroFactura & "') AND (IndiceNota.Cod_Cliente = '" & CodigoCliente & "')"

            DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
            DataAdapter.Fill(DataSet, "NotaCR")
            Registros2 = DataSet.Tables("NotaCR").Rows.Count
            NumeroNotaCR = ""
            j = 0
            MontoNotaCR = 0
            TotalMontoNotaCR = 0
            Do While Registros2 > j

                TipoNota = DataSet.Tables("NotaCR").Rows(j)("Tipo")

                If Moneda = "Cordobas" Then
                    If TipoNota <> "Credito Clientes Dif $" Then
                        If DataSet.Tables("NotaCR").Rows(j)("MonedaNota") = "Cordobas" Then
                            TasaCambioRecibo = 1
                        Else
                            TasaCambioRecibo = BuscaTasaCambio(DataSet.Tables("NotaCR").Rows(j)("Fecha_Nota"))
                        End If
                    Else
                        TasaCambioRecibo = 0
                    End If
                Else
                    If TipoNota <> "Credito Clientes Dif C$" Then
                        If DataSet.Tables("NotaCR").Rows(j)("MonedaNota") = "Dolares" Then
                            TasaCambioRecibo = 1
                        Else
                            TasaCambioRecibo = 1 / BuscaTasaCambio(DataSet.Tables("NotaCR").Rows(j)("Fecha_Nota"))
                        End If
                    Else
                        TasaCambioRecibo = 0
                    End If
                End If


                If NumeroNotaCR = "" Then
                    NumeroNotaCR = DataSet.Tables("NotaCR").Rows(j)("Numero_Nota")
                Else
                    NumeroNotaCR = NumeroNotaCR & "," & DataSet.Tables("NotaCR").Rows(j)("Numero_Nota")
                End If
                MontoNotaCR = DataSet.Tables("NotaCR").Rows(j)("Monto") * TasaCambioRecibo
                'MontoNotaCR +
                TotalMontoNotaCR = TotalMontoNotaCR + MontoNotaCR
                j = j + 1
            Loop
            DataSet.Tables("NotaCR").Reset()

            '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '//////////////////////////////////////BUSCO EL DETALLE DE METODO PARA LAS FACTURAS DE CONTADO //////////////////////////////////////////////////////
            '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            MontoMetodoFactura = 0
            SQlString = "SELECT * FROM Detalle_MetodoFacturas INNER JOIN MetodoPago ON Detalle_MetodoFacturas.NombrePago = MetodoPago.NombrePago WHERE (Detalle_MetodoFacturas.Tipo_Factura = 'Factura') AND (Detalle_MetodoFacturas.Numero_Factura = '" & NumeroFactura & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
            DataAdapter.Fill(DataSet, "MetodoFactura")
            If DataSet.Tables("MetodoFactura").Rows.Count <> 0 Then
                If Moneda = "Cordobas" Then
                    If DataSet.Tables("MetodoFactura").Rows(0)("Moneda") = "Cordobas" Then
                        TasaCambioRecibo = 1
                    Else
                        TasaCambioRecibo = BuscaTasaCambio(DataSet.Tables("MetodoFactura").Rows(0)("Fecha_Factura"))
                    End If
                Else
                    If DataSet.Tables("MetodoFactura").Rows(0)("Moneda") = "Dolares" Then
                        TasaCambioRecibo = 1
                    Else
                        TasaCambioRecibo = 1 / BuscaTasaCambio(DataSet.Tables("MetodoFactura").Rows(0)("Fecha_Factura"))
                    End If
                End If

                MontoMetodoFactura = DataSet.Tables("MetodoFactura").Rows(0)("Monto") * TasaCambioRecibo

            End If
            DataSet.Tables("MetodoFactura").Reset()






            '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '//////////////////////////////////////BUSCO EL INTERES MORATORIO PARA ESTE CLIENTE //////////////////////////////////////////////////////
            '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            SQlString = "SELECT  * FROM Clientes WHERE (Cod_Cliente = '" & CodigoCliente & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
            DataAdapter.Fill(DataSet, "DatosCliente")
            If Not DataSet.Tables("DatosCliente").Rows.Count = 0 Then
                If Not IsDBNull(DataSet.Tables("DatosCliente").Rows(0)("InteresMoratorio")) Then
                    TasaInteres = (DataSet.Tables("DatosCliente").Rows(0)("InteresMoratorio") / 100)
                End If
            End If

            MontoFactura = (DataSet.Tables("Clientes").Rows(i)("SubTotal") + DataSet.Tables("Clientes").Rows(i)("IVA")) * TasaCambio



            Dias = DateDiff(DateInterval.Day, FechaVence, FechaFin)
            Saldo = MontoFactura - MontoRecibo + TotalMontoNotaDB - TotalMontoNotaCR - MontoMetodoFactura
            If Format(Saldo, "##,##0.00") = "0.00" Then
                Dias = 0
            End If
            MontoMora = Dias * Saldo * TasaInteres
            Total = Saldo + MontoMora

            'oDataRow = DatasetReporte.Tables("TotalVentas").NewRow
            'oDataRow("Fecha_Factura") = DataSet.Tables("Clientes").Rows(i)("Fecha_Factura")
            'oDataRow("Numero_Factura") = DataSet.Tables("Clientes").Rows(i)("Numero_Factura")
            'oDataRow("Numero_Recibo") = NumeroRecibo
            'If NumeroNota = "" Then
            '    If NumeroNotaCR <> "" Then
            '        oDataRow("NotaDebito") = "NC:" & NumeroNotaCR
            '    End If
            'ElseIf NumeroNotaCR = "" Then
            '    If NumeroNota <> "" Then
            '        oDataRow("NotaDebito") = "NB:" & NumeroNota
            '    End If
            'Else
            '    oDataRow("NotaDebito") = "NC:" & NumeroNotaCR & " NB:" & NumeroNota
            'End If
            'oDataRow("Monto") = Format(MontoFactura, "##,##0.00")
            'oDataRow("FechaVence") = DataSet.Tables("Clientes").Rows(i)("Fecha_Vencimiento")
            'oDataRow("Abono") = Format(MontoRecibo + MontoMetodoFactura, "##,##0.00")
            'oDataRow("MontoNota") = Format(TotalMontoNotaDB - TotalMontoNotaCR, "##,##0.00")
            'oDataRow("Saldo") = Format(Saldo, "##,##0.00")
            'oDataRow("Moratorio") = Format(MontoMora, "##,##0.00")
            'oDataRow("Dias") = Dias
            'oDataRow("Total") = Format(Total, "##,##0.00")
            'DatasetReporte.Tables("TotalVentas").Rows.Add(oDataRow)

            If Moneda = "Cordobas" Then
                ActualizaMontoCredito(DataSet.Tables("Clientes").Rows(i)("Numero_Factura"), DataSet.Tables("Clientes").Rows(i)("Fecha_Factura"), Saldo, "Cordobas")
            Else
                ActualizaMontoCredito(DataSet.Tables("Clientes").Rows(i)("Numero_Factura"), DataSet.Tables("Clientes").Rows(i)("Fecha_Factura"), Saldo, "Dolares")
            End If



            i = i + 1


            TotalFactura = TotalFactura + Saldo
            TotalAbonos = TotalAbonos + MontoRecibo
            TotalCargos = TotalCargos + MontoFactura
            TotalMora = TotalMora + MontoMora
            DataSet.Tables("DatosCliente").Reset()
            DataSet.Tables("Recibos").Reset()
        Loop


        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////////////////BUSCO SI EXISTEN NOTAS DE DEBITO SIN FACTURAS //////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SQlString = "SELECT Detalle_Nota.id_Detalle_Nota, Detalle_Nota.Numero_Nota, Detalle_Nota.Fecha_Nota, Detalle_Nota.Tipo_Nota, Detalle_Nota.CodigoNB, Detalle_Nota.Descripcion, Detalle_Nota.Numero_Factura, Detalle_Nota.Monto, NotaDebito.Tipo, IndiceNota.MonedaNota, IndiceNota.Fecha_Nota AS Expr1, IndiceNota.Tipo_Nota AS Expr2 FROM Detalle_Nota INNER JOIN NotaDebito ON Detalle_Nota.Tipo_Nota = NotaDebito.CodigoNB INNER JOIN IndiceNota ON Detalle_Nota.Numero_Nota = IndiceNota.Numero_Nota AND Detalle_Nota.Fecha_Nota = IndiceNota.Fecha_Nota AND Detalle_Nota.Tipo_Nota = IndiceNota.Tipo_Nota WHERE (NotaDebito.Tipo LIKE '%Debito Clientes%') AND (IndiceNota.Cod_Cliente = '" & CodigoCliente & "')  AND (Detalle_Nota.Numero_Factura = '0000')"

        DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
        DataAdapter.Fill(DataSet, "NotaDB")
        Registros2 = DataSet.Tables("NotaDB").Rows.Count
        NumeroNota = ""
        j = 0
        MontoNota = 0
        Do While Registros2 > j

            TipoNota = DataSet.Tables("NotaDB").Rows(j)("Tipo")

            If Moneda = "Cordobas" Then
                If TipoNota <> "Debito Clientes Dif $" Then
                    If DataSet.Tables("NotaDB").Rows(j)("MonedaNota") = "Cordobas" Then
                        TasaCambioRecibo = 1
                    Else
                        TasaCambioRecibo = BuscaTasaCambio(DataSet.Tables("NotaDB").Rows(j)("Fecha_Nota"))
                    End If
                Else
                    TasaCambioRecibo = 0
                End If
            Else
                If TipoNota <> "Debito Clientes Dif C$" Then
                    If DataSet.Tables("NotaDB").Rows(j)("MonedaNota") = "Dolares" Then
                        TasaCambioRecibo = 1
                    Else
                        TasaCambioRecibo = 1 / BuscaTasaCambio(DataSet.Tables("NotaDB").Rows(j)("Fecha_Nota"))
                    End If
                Else
                    TasaCambioRecibo = 0
                End If
            End If

            If NumeroNota = "" Then
                NumeroNota = DataSet.Tables("NotaDB").Rows(j)("Numero_Nota")
            Else
                NumeroNota = DataSet.Tables("NotaDB").Rows(j)("Numero_Nota")
            End If
            MontoNota = DataSet.Tables("NotaDB").Rows(j)("Monto") * TasaCambioRecibo
            TotalMontoNotaDB = TotalMontoNotaDB + MontoNota

            Dim Abono As Double = 0, AbonoNota As Double = 0

            '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '/////////////////////////////////////////////////////////BUSCO SI EXISTEN RECIBOS PARA LA NOTA DE DEBITO ///////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            If Moneda = "Cordobas" Then
                Abono = MontoReciboNotas(NumeroNota, "Cordobas")
            Else
                Abono = MontoReciboNotas(NumeroNota, "Dolares")
            End If

            If CadenaRecibo = "" Then
                CadenaRecibo = "0000"
            End If

            RefNotaDebito = ""
            '////////////////////////////////////BUSCO SI EXISTEN NOTAS PARA LA NOTA DE DEBITO /////////////////////////
            If Moneda = "Cordobas" Then
                AbonoNota = MontoNotaCreditoNota(NumeroNota, "Cordobas", CodigoCliente, NumeroNota)
            Else
                AbonoNota = MontoNotaCreditoNota(NumeroNota, "Dolares", CodigoCliente, NumeroNota)
            End If


            Dias = DateDiff(DateInterval.Day, DataSet.Tables("NotaDB").Rows(j)("Fecha_Nota"), FechaFin)

            'oDataRow = DatasetReporte.Tables("TotalVentas").NewRow
            'oDataRow("Fecha_Factura") = DataSet.Tables("NotaDB").Rows(j)("Fecha_Nota")
            'oDataRow("Numero_Factura") = "0000"
            'oDataRow("Numero_Recibo") = CadenaRecibo
            'oDataRow("NotaDebito") = "NB:" & NumeroNota & RefNotaDebito
            'oDataRow("Monto") = "0"
            'oDataRow("FechaVence") = DataSet.Tables("NotaDB").Rows(j)("Fecha_Nota")
            'oDataRow("Abono") = Abono
            'oDataRow("MontoNota") = Format(MontoNota, "##,##0.00")
            'oDataRow("Saldo") = Format(MontoNota - Abono, "##,##0.00")
            'oDataRow("Moratorio") = "0"
            'oDataRow("Dias") = Dias
            'oDataRow("Total") = Format(MontoNota - Abono, "##,##0.00")
            'DatasetReporte.Tables("TotalVentas").Rows.Add(oDataRow)


            TotalFactura = TotalFactura + MontoNota

            j = j + 1
        Loop

        DataSet.Tables("NotaDB").Reset()
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////////////////BUSCO SI EXISTEN NOTAS DE CREDITO SIN FACTURAS //////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SQlString = "SELECT Detalle_Nota.*, NotaDebito.Tipo, IndiceNota.MonedaNota FROM Detalle_Nota INNER JOIN NotaDebito ON Detalle_Nota.Tipo_Nota = NotaDebito.CodigoNB INNER JOIN IndiceNota ON Detalle_Nota.Numero_Nota = IndiceNota.Numero_Nota WHERE  (NotaDebito.Tipo = 'Credito Clientes') AND (Detalle_Nota.Numero_Factura = '" & NumeroFactura & "')"
        SQlString = "SELECT Detalle_Nota.id_Detalle_Nota, Detalle_Nota.Numero_Nota, Detalle_Nota.Fecha_Nota, Detalle_Nota.Tipo_Nota, Detalle_Nota.CodigoNB, Detalle_Nota.Descripcion, Detalle_Nota.Numero_Factura, Detalle_Nota.Monto, NotaDebito.Tipo, IndiceNota.MonedaNota, IndiceNota.Fecha_Nota AS Expr1, IndiceNota.Tipo_Nota AS Expr2 FROM Detalle_Nota INNER JOIN NotaDebito ON Detalle_Nota.Tipo_Nota = NotaDebito.CodigoNB INNER JOIN IndiceNota ON Detalle_Nota.Numero_Nota = IndiceNota.Numero_Nota AND Detalle_Nota.Fecha_Nota = IndiceNota.Fecha_Nota AND Detalle_Nota.Tipo_Nota = IndiceNota.Tipo_Nota WHERE (NotaDebito.Tipo LIKE '%Credito Clientes%') AND (IndiceNota.Cod_Cliente = '" & CodigoCliente & "')  AND (Detalle_Nota.Numero_Factura = '0000')"

        DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
        DataAdapter.Fill(DataSet, "NotaCR")
        Registros2 = DataSet.Tables("NotaCR").Rows.Count
        NumeroNotaCR = ""
        j = 0
        MontoNotaCR = 0
        Do While Registros2 > j

            TipoNota = DataSet.Tables("NotaCR").Rows(j)("Tipo")

            If Moneda = "Cordobas" Then
                If TipoNota <> "Credito Clientes Dif $" Then
                    If DataSet.Tables("NotaCR").Rows(j)("MonedaNota") = "Cordobas" Then
                        TasaCambioRecibo = 1
                    Else
                        TasaCambioRecibo = BuscaTasaCambio(DataSet.Tables("NotaCR").Rows(j)("Fecha_Nota"))
                    End If
                Else
                    TasaCambioRecibo = 0
                End If
            Else
                If TipoNota <> "Credito Clientes Dif C$" Then
                    If DataSet.Tables("NotaCR").Rows(j)("MonedaNota") = "Dolares" Then
                        TasaCambioRecibo = 1
                    Else
                        TasaCambioRecibo = 1 / BuscaTasaCambio(DataSet.Tables("NotaCR").Rows(j)("Fecha_Nota"))
                    End If
                Else
                    TasaCambioRecibo = 0
                End If
            End If

            If NumeroNotaCR = "" Then
                NumeroNotaCR = DataSet.Tables("NotaCR").Rows(j)("Numero_Nota")
            Else
                NumeroNotaCR = DataSet.Tables("NotaCR").Rows(j)("Numero_Nota")
            End If
            MontoNotaCR = DataSet.Tables("NotaCR").Rows(j)("Monto") * TasaCambioRecibo
            TotalMontoNotaCR = TotalMontoNotaCR + MontoNotaCR

            'MontoNotaCR +

            Dias = DateDiff(DateInterval.Day, DataSet.Tables("NotaCR").Rows(j)("Fecha_Nota"), FechaFin)

            'oDataRow = DatasetReporte.Tables("TotalVentas").NewRow
            'oDataRow("Fecha_Factura") = DataSet.Tables("NotaCR").Rows(j)("Fecha_Nota")
            'oDataRow("Numero_Factura") = "0000"
            'oDataRow("Numero_Recibo") = "0000"
            'oDataRow("NotaDebito") = "NC:" & NumeroNotaCR
            'oDataRow("Monto") = "0"
            'oDataRow("FechaVence") = DataSet.Tables("NotaCR").Rows(j)("Fecha_Nota")
            'oDataRow("Abono") = "0"
            'oDataRow("MontoNota") = Format(-1 * MontoNotaCR, "##,##0.00")
            'oDataRow("Saldo") = Format(-1 * MontoNotaCR, "##,##0.00")
            'oDataRow("Moratorio") = "0"
            'oDataRow("Dias") = Dias
            'oDataRow("Total") = Format(-1 * MontoNotaCR, "##,##0.00")
            'DatasetReporte.Tables("TotalVentas").Rows.Add(oDataRow)

            TotalFactura = TotalFactura - MontoNotaCR

            j = j + 1
        Loop
        DataSet.Tables("NotaCR").Reset()


        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////////////////BUSCO SI EXISTEN RECIBOS SIN FACTURAS //////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SQlString = "SELECT  MAX(DetalleRecibo.CodReciboPago) AS CodReciboPago, MAX(DetalleRecibo.Fecha_Recibo) AS Fecha_Recibo, DetalleRecibo.Numero_Factura, SUM(DetalleRecibo.MontoPagado) AS MontoPagado, Recibo.MonedaRecibo FROM DetalleRecibo INNER JOIN Recibo ON DetalleRecibo.CodReciboPago = Recibo.CodReciboPago AND DetalleRecibo.Fecha_Recibo = Recibo.Fecha_Recibo GROUP BY DetalleRecibo.Numero_Factura, Recibo.MonedaRecibo, Recibo.Cod_Cliente " & _
        '            "HAVING (DetalleRecibo.Numero_Factura = '0') AND (Recibo.Cod_Cliente = '" & codigocliente  & "')"

        SQlString = "SELECT  DetalleRecibo.CodReciboPago, DetalleRecibo.Fecha_Recibo, DetalleRecibo.Numero_Factura, DetalleRecibo.MontoPagado, Recibo.MonedaRecibo FROM  DetalleRecibo INNER JOIN Recibo ON DetalleRecibo.CodReciboPago = Recibo.CodReciboPago AND DetalleRecibo.Fecha_Recibo = Recibo.Fecha_Recibo WHERE (DetalleRecibo.Numero_Factura = '0') AND (Recibo.Cod_Cliente = '" & CodigoCliente & "') ORDER BY DetalleRecibo.Fecha_Recibo"
        DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
        DataAdapter.Fill(DataSet, "RecibosSF")
        Registros2 = DataSet.Tables("RecibosSF").Rows.Count
        j = 0
        MontoRecibo = 0

        Do While Registros2 > j

            If Moneda = "Cordobas" Then
                If DataSet.Tables("RecibosSF").Rows(j)("MonedaRecibo") = "Cordobas" Then
                    TasaCambioRecibo = 1
                Else
                    TasaCambioRecibo = BuscaTasaCambio(DataSet.Tables("RecibosSF").Rows(j)("Fecha_Recibo"))
                End If
            Else
                If DataSet.Tables("RecibosSF").Rows(j)("MonedaRecibo") = "Dolares" Then
                    TasaCambioRecibo = 1
                Else
                    TasaCambioRecibo = 1 / BuscaTasaCambio(DataSet.Tables("RecibosSF").Rows(j)("Fecha_Recibo"))
                End If
            End If
            If NumeroRecibo = "" Then
                NumeroRecibo = DataSet.Tables("RecibosSF").Rows(j)("CodReciboPago")
            Else
                NumeroRecibo = DataSet.Tables("RecibosSF").Rows(j)("CodReciboPago")
            End If
            MontoRecibo = DataSet.Tables("RecibosSF").Rows(j)("MontoPagado") * TasaCambioRecibo

            Dias = DateDiff(DateInterval.Day, DataSet.Tables("RecibosSF").Rows(j)("Fecha_Recibo"), FechaFin)

            'oDataRow = DatasetReporte.Tables("TotalVentas").NewRow
            'oDataRow("Fecha_Factura") = DataSet.Tables("RecibosSF").Rows(j)("Fecha_Recibo")
            'oDataRow("Numero_Factura") = "0000"
            'oDataRow("Numero_Recibo") = NumeroRecibo
            'oDataRow("Monto") = "0"
            'oDataRow("FechaVence") = DataSet.Tables("RecibosSF").Rows(j)("Fecha_Recibo")
            'oDataRow("Abono") = Format(MontoRecibo, "##,##0.00")
            'oDataRow("MontoNota") = "0"
            'oDataRow("Saldo") = Format(-1 * MontoRecibo, "##,##0.00")
            'oDataRow("Moratorio") = "0"
            'oDataRow("Dias") = Dias
            'oDataRow("Total") = Format(-1 * MontoRecibo, "##,##0.00")
            'DatasetReporte.Tables("TotalVentas").Rows.Add(oDataRow)

            TotalAbonos = TotalAbonos + MontoRecibo
            TotalFactura = TotalFactura - MontoRecibo
            j = j + 1
        Loop



        'Me.TxtCargos.Text = Format(TotalCargos, "##,##0.00")
        'Me.TxtAbonos.Text = Format(TotalAbonos, "##,##0.00")
        'Me.TxtMora.Text = Format(TotalMora, "##,##0.00")
        'Me.TxtNB.Text = Format(TotalMontoNotaDB - TotalMontoNotaCR, "##,##0.00")
        'Me.TxtSaldoFinal.Text = Format(TotalFactura, "##,##0.00")

        'SaldoClienteH = TotalFactura
        'oHebraCliente.Abort()

    End Sub


    Public Function LoteDefecto(ByVal CodigoProducto As String, ByVal CodigoBodega As String) As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim SQlString As String, iPosicion As Double = 0
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim oDataRow As DataRow, i As Double, Registro As Double = 0
        Dim ExistenciaLote As Double, FechaVence As Date, NumeroLote As String
        Dim StrSqlUpdate As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer


        SQlString = "SELECT     MAX(Detalle_Compras.Cod_Producto) AS Cod_Producto, Detalle_Compras.Numero_Lote, Lote.FechaVence FROM Detalle_Compras INNER JOIN Lote ON Detalle_Compras.Numero_Lote = Lote.Numero_Lote WHERE  (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Lote.Activo = 1) GROUP BY Detalle_Compras.Numero_Lote, Lote.FechaVence  HAVING(Not (Detalle_Compras.Numero_Lote Is NULL)) ORDER BY Lote.FechaVence"
        DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
        DataAdapter.Fill(DataSet, "Lotes")
        iPosicion = 0
        LoteDefecto = ""

        Do While DataSet.Tables("Lotes").Rows.Count > iPosicion

            'CodigoProducto = DataSet.Tables("Lotes").Rows(iPosicion)("Cod_Producto")
            NumeroLote = DataSet.Tables("Lotes").Rows(iPosicion)("Numero_Lote")
            If Not IsDBNull(DataSet.Tables("Lotes").Rows(iPosicion)("FechaVence")) Then
                FechaVence = DataSet.Tables("Lotes").Rows(iPosicion)("FechaVence")
            End If

            ExistenciaLote = BuscaExistenciaBodegaLote(CodigoProducto, CodigoBodega, NumeroLote)

            '//////////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////SI LA EXISTENCIA DEL LOTE ES CERO LO INACTIVO ///////////////////////////////
            '////////////////////////////////////////////////////////////////////////////////////////////////////


            If ExistenciaLote = 0 Then

                'MiConexion.Close()
                'StrSqlUpdate = "UPDATE [Lote] SET [Activo] = 0 WHERE (Numero_Lote = '" & NumeroLote & "')"
                'MiConexion.Open()
                'ComandoUpdate = New SqlClient.SqlCommand(StrSqlUpdate, MiConexion)
                'iResultado = ComandoUpdate.ExecuteNonQuery
                'MiConexion.Close()

            ElseIf ExistenciaLote > 0 Then
                LoteDefecto = NumeroLote
                Exit Function
            End If

            iPosicion = iPosicion + 1
        Loop





    End Function

    Public Function MontoNotaCreditoNota(ByVal NumeroNota As String, ByVal Moneda As String, ByVal CodigoCliente As String, ByVal NumeroNotaDB As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim SqlString As String, Registros2 As Double, NumeroNotaCR As String, MontoNotaCR As Double, TotalMontoNotaCR As Double, j As Double, TipoNota As String, TasaCambioRecibo As Double

        'DataSet.Tables("NotaDB").Reset()
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////////////////BUSCO SI EXISTEN NOTAS DE CREDITO PARA ESTA FACTURA //////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SQlString = "SELECT Detalle_Nota.*, NotaDebito.Tipo, IndiceNota.MonedaNota FROM Detalle_Nota INNER JOIN NotaDebito ON Detalle_Nota.Tipo_Nota = NotaDebito.CodigoNB INNER JOIN IndiceNota ON Detalle_Nota.Numero_Nota = IndiceNota.Numero_Nota WHERE  (NotaDebito.Tipo = 'Credito Clientes') AND (Detalle_Nota.Numero_Factura = '" & NumeroFactura & "')"
        SqlString = "SELECT Detalle_Nota.id_Detalle_Nota, Detalle_Nota.Numero_Nota, Detalle_Nota.Fecha_Nota, Detalle_Nota.Tipo_Nota, Detalle_Nota.CodigoNB, Detalle_Nota.Descripcion, Detalle_Nota.Numero_Factura, Detalle_Nota.Monto, NotaDebito.Tipo, IndiceNota.MonedaNota, IndiceNota.Fecha_Nota AS Expr1, IndiceNota.Tipo_Nota AS Expr2 FROM Detalle_Nota INNER JOIN NotaDebito ON Detalle_Nota.Tipo_Nota = NotaDebito.CodigoNB INNER JOIN IndiceNota ON Detalle_Nota.Numero_Nota = IndiceNota.Numero_Nota AND Detalle_Nota.Fecha_Nota = IndiceNota.Fecha_Nota AND Detalle_Nota.Tipo_Nota = IndiceNota.Tipo_Nota WHERE (NotaDebito.Tipo LIKE '%Credito Clientes%') AND (Detalle_Nota.Numero_Factura = '" & NumeroNotaDB & "') AND (IndiceNota.Cod_Cliente = '" & CodigoCliente & "')"

        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "NotaCR")
        Registros2 = DataSet.Tables("NotaCR").Rows.Count
        NumeroNotaCR = ""
        j = 0
        MontoNotaCR = 0
        TotalMontoNotaCR = 0
        Do While Registros2 > j

            TipoNota = DataSet.Tables("NotaCR").Rows(j)("Tipo")

            If Moneda = "Cordobas" Then
                If TipoNota <> "Credito Clientes Dif $" Then
                    If DataSet.Tables("NotaCR").Rows(j)("MonedaNota") = "Cordobas" Then
                        TasaCambioRecibo = 1
                    Else
                        TasaCambioRecibo = BuscaTasaCambio(DataSet.Tables("NotaCR").Rows(j)("Fecha_Nota"))
                    End If
                Else
                    TasaCambioRecibo = 0
                End If
            Else
                If TipoNota <> "Credito Clientes Dif C$" Then
                    If DataSet.Tables("NotaCR").Rows(j)("MonedaNota") = "Dolares" Then
                        TasaCambioRecibo = 1
                    Else
                        TasaCambioRecibo = 1 / BuscaTasaCambio(DataSet.Tables("NotaCR").Rows(j)("Fecha_Nota"))
                    End If
                Else
                    TasaCambioRecibo = 0
                End If
            End If


            If NumeroNotaCR = "" Then
                NumeroNotaCR = DataSet.Tables("NotaCR").Rows(j)("Numero_Nota")
            Else
                NumeroNotaCR = NumeroNotaCR & "," & DataSet.Tables("NotaCR").Rows(j)("Numero_Nota")
            End If
            MontoNotaCR = DataSet.Tables("NotaCR").Rows(j)("Monto") * TasaCambioRecibo
            'MontoNotaCR +
            TotalMontoNotaCR = TotalMontoNotaCR + MontoNotaCR
            j = j + 1
        Loop
        DataSet.Tables("NotaCR").Reset()

        MontoNotaCreditoNota = TotalMontoNotaCR
        RefNotaDebito = NumeroNotaCR

    End Function

    Public Function TieneMovimientos(ByVal CodigoProducto As String, ByVal Fecha As Date) As Boolean
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim SqlString As String

        DataSet.Reset()
        Mensaje = ""

        SqlString = "SELECT * FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                    "WHERE (Detalle_Facturas.Tipo_Factura <> 'Cotizacion') AND (Facturas.FechaHora > CONVERT(DATETIME, '" & Format(Fecha, "yyyy-MM-dd HH:mm") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Nombre_Cliente <> '******CANCELADO')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Facturas")
        If Not DataSet.Tables("Facturas").Rows.Count = 0 Then
            TieneMovimientos = True
            Mensaje = "Este Producto tiene registros en el Modulo Facturacion para esta fecha," & _
                      "Afectaria los costo de los productos"
            Exit Function
        Else
            TieneMovimientos = False
        End If


        SqlString = "SELECT * FROM Compras INNER JOIN Detalle_Compras ON Compras.Numero_Compra = Detalle_Compras.Numero_Compra AND Compras.Fecha_Compra = Detalle_Compras.Fecha_Compra AND Compras.Tipo_Compra = Detalle_Compras.Tipo_Compra  " & _
                    "WHERE (Compras.Nombre_Proveedor <> '******CANCELADO') AND (Compras.FechaHora > CONVERT(DATETIME, '" & Format(Fecha, "yyyy-MM-dd HH:mm") & "', 102)) AND (Compras.Tipo_Compra <> 'Orden de Compra') AND (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        If Not DataSet.Tables("Compras").Rows.Count = 0 Then
            TieneMovimientos = True
            Mensaje = "Este Producto tiene registros en el Modulo Compras para esta fecha, " & _
                      "Afectaria los costos de los productos"
            Exit Function
        Else
            TieneMovimientos = False
        End If

    End Function



    Public Function MontoReciboNotas(ByVal NumeroNota As String, ByVal Moneda As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Registros As Double = 0, i As Double = 0, SqlString As String
        Dim Saldo As Double

        Saldo = 0

        'SqlString = "SELECT SUM(MontoPagado) AS MontoPagado, Numero_Factura FROM DetalleRecibo GROUP BY Numero_Factura HAVING (Numero_Factura = 'NB" & NumeroNota & "')"

        If Moneda = "Cordobas" Then
            SqlString = "SELECT   DetalleRecibo.Numero_Factura, SUM(CASE WHEN Recibo.MonedaRecibo = 'Cordobas' THEN DetalleRecibo.MontoPagado ELSE DetalleRecibo.MontoPagado * TasaCambio.MontoTasa END) AS MontoPagado, MAX(Recibo.CodReciboPago) AS CodReciboPago FROM  DetalleRecibo INNER JOIN  Recibo ON DetalleRecibo.CodReciboPago = Recibo.CodReciboPago AND DetalleRecibo.Fecha_Recibo = Recibo.Fecha_Recibo INNER JOIN TasaCambio ON Recibo.Fecha_Recibo = TasaCambio.FechaTasa GROUP BY DetalleRecibo.Numero_Factura   " & _
                        "HAVING (DetalleRecibo.Numero_Factura = 'NB" & NumeroNota & "')"
        Else
            SqlString = "SELECT   DetalleRecibo.Numero_Factura, SUM(CASE WHEN Recibo.MonedaRecibo = 'Dolares' THEN DetalleRecibo.MontoPagado ELSE DetalleRecibo.MontoPagado / TasaCambio.MontoTasa END) AS MontoPagado, MAX(Recibo.CodReciboPago) AS CodReciboPago FROM  DetalleRecibo INNER JOIN  Recibo ON DetalleRecibo.CodReciboPago = Recibo.CodReciboPago AND DetalleRecibo.Fecha_Recibo = Recibo.Fecha_Recibo INNER JOIN TasaCambio ON Recibo.Fecha_Recibo = TasaCambio.FechaTasa GROUP BY DetalleRecibo.Numero_Factura   " & _
                        "HAVING (DetalleRecibo.Numero_Factura = 'NB" & NumeroNota & "')"

        End If
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Saldo")
        MiConexion.Close()
        If DataSet.Tables("Saldo").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("Saldo").Rows(0)("MontoPagado")) Then
                Saldo = DataSet.Tables("Saldo").Rows(0)("MontoPagado")
                CadenaRecibo = DataSet.Tables("Saldo").Rows(0)("CodReciboPago")
            End If
        Else
            Saldo = 0
        End If

        MontoReciboNotas = Saldo

        DataSet.Tables("Saldo").Reset()
    End Function

    Public Sub ConfiguracionAcceso(ByVal NombrePerfil As String, ByVal AccesoModulo As String)
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Registros As Double = 0, i As Double = 0, Sql As String
        Dim Cadena As String, Permiso As String

        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////CIERRO TODAS LAS OPCIONES QUE EL PERFIL NO TIENE ACCESO /////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        i = 0
        Sql = "SELECT     Accesos.IdPerfil, Accesos.Modulo, Accesos.Acceso FROM Accesos INNER JOIN Perfil ON Accesos.IdPerfil = Perfil.IdPerfil  " & _
              "WHERE (Accesos.Acceso LIKE '%No%') AND (Perfil.NombrePerfil = '" & NombrePerfil & "') AND (Accesos.Modulo = '" & AccesoModulo & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(Sql, MiConexion)
        DataAdapter.Fill(DataSet, "Acceso")
        Registros = DataSet.Tables("Acceso").Rows.Count

        My.Application.DoEvents()
        If Registros <> 0 Then

            Cadena = DataSet.Tables("Acceso").Rows(i)("Acceso")
            Dim Opciones() = Split(Cadena, ",")

            For i = 0 To Opciones.Length - 1
                If Opciones(i) <> "" Then
                    Permiso = Opciones(i)

                    Select Case Permiso
                        Case "Abrir" : FrmAccesos.ChkAbrir.Checked = True
                        Case "NoAbrir" : FrmAccesos.ChkAbrir.Checked = False
                        Case "Grabar" : FrmAccesos.ChkGrabar.Checked = True
                        Case "NoGrabar" : FrmAccesos.ChkGrabar.Checked = False
                        Case "Eliminar" : FrmAccesos.ChkEliminar.Checked = True
                        Case "NoEliminar" : FrmAccesos.ChkEliminar.Checked = False
                        Case "Anular" : FrmAccesos.ChkAnular.Checked = True
                        Case "NoAnular" : FrmAccesos.ChkAnular.Checked = False
                        Case "Procesar" : FrmAccesos.ChkProcesar.Checked = True
                        Case "NoProcesar" : FrmAccesos.ChkProcesar.Checked = False
                        Case "Imprimir" : FrmAccesos.ChkImprimir.Checked = True
                        Case "NoImprimir" : FrmAccesos.ChkImprimir.Checked = False
                        Case "Editar" : FrmAccesos.ChkEditar.Checked = True
                        Case "NoEditar" : FrmAccesos.ChkEditar.Checked = False
                        Case "CambiarBodega" : FrmAccesos.ChkCambiarBodega.Checked = True
                        Case "NoCambiarBodega" : FrmAccesos.ChkCambiarBodega.Checked = False


                    End Select

                End If
            Next
        End If
    End Sub


    Public Sub ObtenerContenedores(ByVal Parent As Control, ByVal Permiso As String)
        Dim i As Double = 0

        For Each Label As Control In Parent.Controls
            If TypeOf Label Is Button Then
                Select Case Label.Tag
                    Case 25 : If Permiso = "NoGrabar" Then Label.Enabled = False
                    Case 26 : If Permiso = "NoAnular" Then Label.Enabled = False
                    Case 27 : If Permiso = "NoImprimir" Then Label.Enabled = False
                    Case 28 : If Permiso = "NoProcesar" Then Label.Enabled = False
                    Case 29 : If Permiso = "NoEliminar" Then Label.Enabled = False
                    Case 30 : If Permiso = "NoCambiarBodega" Then Label.Enabled = False
                End Select
            ElseIf TypeOf Label Is DevExpress.XtraTab.XtraTabPage Then
                ObtenerContenedores(Label, Permiso)
            ElseIf TypeOf Label Is GroupBox Then
                ObtenerContenedores(Label, Permiso)
            ElseIf TypeOf Label Is C1.Win.C1List.C1Combo Then
                If Permiso = "NoCambiarBodega" Then
                    If Label.Tag = 30 Then
                        Label.Enabled = False
                    End If
                End If
                End If
        Next
    End Sub
    Public Function PermiteEditar(ByVal NombrePerfil As String, ByVal Modulo As String) As Boolean

        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Registros As Double = 0, i As Double = 0, Sql As String
        Dim Cadena As String, Permiso As String

        If Modulo = "Factura" Then
            Modulo = "Facturacion"
        ElseIf Modulo = "Mercancia Recibida" Then
            Modulo = "Compras"
        End If


        PermiteEditar = True

        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////CIERRO TODAS LAS OPCIONES QUE EL PERFIL NO TIENE ACCESO /////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        i = 0
        Sql = "SELECT     Accesos.IdPerfil, Accesos.Modulo, Accesos.Acceso FROM Accesos INNER JOIN Perfil ON Accesos.IdPerfil = Perfil.IdPerfil  " & _
              "WHERE (Accesos.Acceso LIKE '%No%') AND (Perfil.NombrePerfil = '" & NombrePerfil & "') AND (Accesos.Modulo = '" & Modulo & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(Sql, MiConexion)
        DataAdapter.Fill(DataSet, "Acceso")
        Registros = DataSet.Tables("Acceso").Rows.Count


        My.Application.DoEvents()
        If Registros <> 0 Then

            Cadena = DataSet.Tables("Acceso").Rows(i)("Acceso")
            Dim Opciones() = Split(Cadena, ",")

            For i = 0 To Opciones.Length - 1
                If Opciones(i) <> "" Then
                    Permiso = Opciones(i)

                    If Permiso = "NoEditar" Then
                        PermiteEditar = False
                    ElseIf Permiso = "Editar" Then
                        PermiteEditar = True
                    End If

                End If
            Next

        Else

            '////////////////////////////////SI NO EXISTEN REGISTRO DE ACCESO ////////////////////////////
            PermiteEditar = True
        End If



        Select Case Modulo
            Case "Facturacion" : If PrimerRegistroFactura = True Then PermiteEditar = True
            Case "Cotizacion" : PermiteEditar = True
            Case "Devolucion de Venta" : If PrimerRegistroFactura = True Then PermiteEditar = True
            Case "Salida Bodega" : If PrimerRegistroFactura = True Then PermiteEditar = True
            Case "Compras" : If PrimerRegistroCompra = True Then PermiteEditar = True
            Case "Devolucion de Compra" : If PrimerRegistroCompra = True Then PermiteEditar = True
            Case "Orden de Compra" : PermiteEditar = True

        End Select






    End Function




    Public Sub Bloqueo(ByVal Parent As Control, ByVal NombrePerfil As String, ByVal Modulo As String)

        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Registros As Double = 0, i As Double = 0, Sql As String
        Dim Cadena As String, Permiso As String

        ' SIGNIFICADO TAG.  GRABAR=25  ANULAR =26  IMPRIMIR=27  PROCESAR=28 ELIMINAR=29


        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////CIERRO TODAS LAS OPCIONES QUE EL PERFIL NO TIENE ACCESO /////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        i = 0  '(Accesos.Acceso LIKE '%No%') AND
        Sql = "SELECT     Accesos.IdPerfil, Accesos.Modulo, Accesos.Acceso FROM Accesos INNER JOIN Perfil ON Accesos.IdPerfil = Perfil.IdPerfil  " & _
              "WHERE  (Perfil.NombrePerfil = '" & NombrePerfil & "') AND (Accesos.Modulo = '" & Modulo & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(Sql, MiConexion)
        DataAdapter.Fill(DataSet, "Acceso")
        Registros = DataSet.Tables("Acceso").Rows.Count

        My.Application.DoEvents()
        If Registros <> 0 Then

            Cadena = DataSet.Tables("Acceso").Rows(i)("Acceso")
            Dim Opciones() = Split(Cadena, ",")

            For i = 0 To Opciones.Length - 1
                If Opciones(i) <> "" Then
                    Permiso = Opciones(i)

                    'EditarFactura = True
                    If Permiso = "NoGrabar" Or Permiso = "NoEliminar" Or Permiso = "NoAnular" Or Permiso = "NoImprimir" Or Permiso = "NoProcesar" Or Permiso = "NoEditar" Or Permiso = "NoCambiarBodega" Then
                        For Each Label As Control In Parent.Controls
                            If TypeOf Label Is Button Then

                                If Permiso = "NoEditar" And Modulo = "Facturacion" Then
                                    EditarFactura = False
                                ElseIf Permiso = "Editar" And Modulo = "Facturacion" Then
                                    EditarFactura = True
                                ElseIf Permiso = "Editar" And Modulo = "Cotizacion" Then
                                    EditarFactura = True
                                ElseIf Permiso = "NoEditar" And Modulo = "Cotizacion" Then
                                    EditarFactura = False
                                ElseIf Permiso = "Editar" And Modulo = "Devolucion de Venta" Then
                                    EditarFactura = True
                                ElseIf Permiso = "NoEditar" And Modulo = "Devolucion de Venta" Then
                                    EditarFactura = False
                                ElseIf Permiso = "Editar" And Modulo = "Salida Bodega" Then
                                    EditarFactura = True
                                ElseIf Permiso = "NoEditar" And Modulo = "Salida Bodega" Then
                                    EditarFactura = False
                                End If


                                Select Case Label.Tag
                                    Case 25 : If Permiso = "NoGrabar" Then Label.Enabled = False
                                    Case 26 : If Permiso = "NoAnular" Then Label.Enabled = False
                                    Case 27 : If Permiso = "NoImprimir" Then Label.Enabled = False
                                    Case 28 : If Permiso = "NoProcesar" Then Label.Enabled = False
                                    Case 29 : If Permiso = "NoEliminar" Then Label.Enabled = False
                                    Case 30 : If Permiso = "NoCambiarBodega" Then Label.Enabled = False
                                End Select
                            ElseIf TypeOf Label Is GroupBox Then
                                ObtenerContenedores(Label, Permiso)
                            ElseIf TypeOf Label Is DevExpress.XtraTab.XtraTabControl Then
                                ObtenerContenedores(Label, Permiso)
                            End If

                        Next
                    ElseIf Permiso = "Grabar" Or Permiso = "Eliminar" Or Permiso = "Anular" Or Permiso = "Imprimir" Or Permiso = "Procesar" Or Permiso = "Editar" Or Permiso = "CambiarBodega" Then
                        For Each Label As Control In Parent.Controls
                            If TypeOf Label Is Button Then

                                If Permiso = "NoEditar" And Modulo = "Facturacion" Then
                                    EditarFactura = False
                                ElseIf Permiso = "Editar" And Modulo = "Facturacion" Then
                                    EditarFactura = True
                                ElseIf Permiso = "Editar" And Modulo = "Cotizacion" Then
                                    EditarFactura = True
                                ElseIf Permiso = "NoEditar" And Modulo = "Cotizacion" Then
                                    EditarFactura = False
                                ElseIf Permiso = "Editar" And Modulo = "Devolucion de Venta" Then
                                    EditarFactura = True
                                ElseIf Permiso = "NoEditar" And Modulo = "Devolucion de Venta" Then
                                    EditarFactura = False
                                ElseIf Permiso = "Editar" And Modulo = "Salida Bodega" Then
                                    EditarFactura = True
                                ElseIf Permiso = "NoEditar" And Modulo = "Salida Bodega" Then
                                    EditarFactura = False
                                End If


                                Select Case Label.Tag
                                    Case 25 : If Permiso = "Grabar" Then Label.Enabled = True
                                    Case 26 : If Permiso = "Anular" Then Label.Enabled = True
                                    Case 27 : If Permiso = "Imprimir" Then Label.Enabled = True
                                    Case 28 : If Permiso = "Procesar" Then Label.Enabled = True
                                    Case 29 : If Permiso = "Eliminar" Then Label.Enabled = True
                                    Case 30 : If Permiso = "CambiarBodega" Then Label.Enabled = True
                                End Select
                            ElseIf TypeOf Label Is GroupBox Then
                                ObtenerContenedores(Label, Permiso)
                            ElseIf TypeOf Label Is DevExpress.XtraTab.XtraTabControl Then
                                ObtenerContenedores(Label, Permiso)
                            End If

                        Next


                    End If

                End If
            Next
        End If


    End Sub

    Public Function EsDescuento(ByVal CodProducto As String) As Boolean
        Dim SqlString As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim TipoProducto As String, TipoDescuento As String

        TipoDescuento = "Nada"

        SqlString = "SELECT  * FROM Productos WHERE (Cod_Productos = '" & CodProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Productos")
        If DataSet.Tables("Productos").Rows.Count <> 0 Then

            TipoProducto = DataSet.Tables("Productos").Rows(0)("Tipo_Producto")
            If Not IsDBNull(DataSet.Tables("Productos").Rows(0)("Unidad_Medida")) Then
                TipoDescuento = DataSet.Tables("Productos").Rows(0)("Unidad_Medida")
            Else
                TipoDescuento = "Nada"
            End If


            Select Case TipoProducto
                Case "Descuento" : EsDescuento = True
                Case Else : EsDescuento = False
            End Select

        End If

    End Function

    Public Function ExisteRecibo(ByVal FechaRecibo As Date, ByVal NumeroRecibo As String) As String
        Dim SqlString As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim FechaConsulta As Date

        DataSet.Reset()

        SqlString = "SELECT *  FROM Recibo WHERE  (CodReciboPago = '" & NumeroRecibo & "') "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Recibo")
        If Not DataSet.Tables("Recibo").Rows.Count = 0 Then
            FechaConsulta = DataSet.Tables("Recibo").Rows(0)("Fecha_Recibo")
            If DateDiff(DateInterval.Day, FechaRecibo, FechaConsulta) = 0 Then
                ExisteRecibo = "ExisteReciboIgual"
            Else
                ExisteRecibo = "ExisteReciboDifFecha"
            End If


        Else
            ExisteRecibo = "NoExisteRecibo"
        End If


    End Function


    Public Function ExisteFactura(ByVal FechaFactura As Date, ByVal NumeroFactura As String, ByVal TipoFactura As String) As String
        Dim SqlString As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim FechaConsulta As Date

        DataSet.Reset()

        SqlString = "SELECT   Facturas.* FROM Facturas WHERE  (Numero_Factura = '" & NumeroFactura & "') AND (Tipo_Factura = '" & TipoFactura & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Facturas")
        If Not DataSet.Tables("Facturas").Rows.Count = 0 Then
            FechaConsulta = DataSet.Tables("Facturas").Rows(0)("Fecha_Factura")
            FechaFacturacion = DataSet.Tables("Facturas").Rows(0)("Fecha_Factura")
            If DateDiff(DateInterval.Day, FechaFactura, FechaConsulta) = 0 Then
                ExisteFactura = "ExisteFacturaIgual"
            Else
                ExisteFactura = "ExisteFacturaDifFecha"
            End If


        Else
            ExisteFactura = "NoExisteFactura"
        End If


    End Function
    Public Function ExisteCompra(ByVal FechaCompra As Date, ByVal NumeroCompra As String, ByVal TipoCompra As String) As String
        Dim SqlString As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim FechaConsulta As Date

        DataSet.Reset()

        ExisteCompra = "NoExisteCompra"

        If FrmCompras.TxtNumeroEnsamble.Text <> "-----0-----" Then
            SqlString = "SELECT * FROM Compras WHERE  (Numero_Compra = '" & NumeroCompra & "') AND (Tipo_Compra = '" & TipoCompra & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.Fill(DataSet, "Compras")
            If Not DataSet.Tables("Compras").Rows.Count = 0 Then
                FechaConsulta = DataSet.Tables("Compras").Rows(0)("Fecha_Compra")
                If DateDiff(DateInterval.Day, FechaCompra, FechaConsulta) = 0 Then
                    ExisteCompra = "ExisteCompraIgual"
                Else
                    ExisteCompra = "ExisteCompraDifFecha"
                End If


            Else
                ExisteCompra = "NoExisteCompra"
            End If

        End If


    End Function

    Public Sub ProcesarArqueo(ByVal Fecha As Date, ByVal Cajero As String, ByVal TipoFactura As String)
        Dim StrSqlUpdate As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer

        '/////////////////////////////////////////////////////////////GenerarNumeroFactura///////////////////////////////////////
        '/////////////////////////////GRABO LOS CHEQUES Y TARJETAS CORDOBAS /////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////
        StrSqlUpdate = "UPDATE Detalle_MetodoFacturas SET Arqueado = 'True' WHERE (Tipo_Factura = 'Factura') AND (Arqueado = 0) AND (Fecha_Factura <= CONVERT(DATETIME, '" & Format(Fecha, "yyyy-MM-dd") & "', 102))"
        MiConexion.Open()
        ComandoUpdate = New SqlClient.SqlCommand(StrSqlUpdate, MiConexion)
        iResultado = ComandoUpdate.ExecuteNonQuery
        MiConexion.Close()

        StrSqlUpdate = "UPDATE Detalle_MetodoRecibo SET Arqueado = 'True'  WHERE (Arqueado = 0) AND (Fecha_Recibo <= CONVERT(DATETIME, '" & Format(Fecha, "yyyy-MM-dd") & "', 102))"
        MiConexion.Open()
        ComandoUpdate = New SqlClient.SqlCommand(StrSqlUpdate, MiConexion)
        iResultado = ComandoUpdate.ExecuteNonQuery
        MiConexion.Close()
    End Sub


    Public Function TipoSujeto(ByVal CodigoCliente As String) As String
        Dim SqlString As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter

        TipoSujeto = "F"

        SqlString = "SELECT  * FROM Clientes WHERE  (Cod_Cliente = '" & CodigoCliente & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Cliente")
        If Not DataSet.Tables("Cliente").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Cliente").Rows(0)("RUC")) Then
                If Len(DataSet.Tables("Cliente").Rows(0)("RUC")) >= 10 Then
                    If Mid(DataSet.Tables("Cliente").Rows(0)("RUC"), 1, 1) = "J" Then
                        TipoSujeto = "J"
                    Else
                        TipoSujeto = "F"
                    End If

                End If
            Else
                TipoSujeto = "F"
            End If
        End If


    End Function

    Public Function BuscaPrecioTipo(ByVal CodProducto As String, ByVal CodBodega As String, ByVal Moneda As String) As Double
        Dim SqlString As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter

        DataSet.Reset()
        BuscaPrecioTipo = 0

        'SqlString = "SELECT Precios.Cod_TipoPrecio, Precios.Cod_Productos, Precios.Monto_Precio, Precios.Monto_PrecioDolar, Bodegas.Cod_Bodega FROM Precios INNER JOIN Bodegas ON Precios.Cod_TipoPrecio = Bodegas.Cod_TipoPrecio  " & _
        '            "WHERE (Precios.Cod_Productos = '" & CodProducto & "') AND (Bodegas.Cod_Bodega = '" & CodBodega & "')"

        SqlString = "SELECT Cod_TipoPrecio, Cod_Productos, Monto_Precio, Monto_PrecioDolar FROM Precios WHERE (Cod_Productos = '" & CodBodega & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Precio")
        If Not DataSet.Tables("Precio").Rows.Count = 0 Then
            If Moneda = "Cordobas" Then
                BuscaPrecioTipo = DataSet.Tables("Precio").Rows(0)("Monto_Precio")

            Else
                BuscaPrecioTipo = DataSet.Tables("Precio").Rows(0)("Monto_Precio")
            End If

        End If


    End Function

    Public Function ValidarFacturaEnRecibo(ByVal CodigoCliente As String, ByVal NumeroFactura As String, ByVal MontoRecibo As Double, ByVal Moneda As String) As Double
        Dim SqlString As String, Iposicion As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim ComandoUpdate As New SqlClient.SqlCommand
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim DatasetReporte As New DataSet, Registros As Double = 0, NumeroRecibo As String = ""
        Dim i As Double, TotalFactura As Double, TotalAbonos As Double, TotalMora As Double, TotalCargos As Double, TotalMontoNotaDB As Double, TotalMontoNotaCR As Double
        Dim TasaCambio As Double = 0, FechaFactura As Date, FechaVence As Date, Registros2 As Double = 0, TasaCambioRecibo As Double = 0, J As Double = 0
        Dim NumeroNota As String = "", MontoNota As Double = 0, NumeroNotaCR As String = "", MontoNotaCR As Double = 0, MontoMetodoFactura As Double = 0
        Dim TasaInteres As Double = 0, MontoFactura As Double = 0, Dias As Double = 0, Saldo As Double = 0, Total As Double = 0, MontoMora = 0
        Dim oDataRow As DataRow

        '*******************************************************************************************************************************
        '/////////////////////////AGREGO UNA CONSULTA QUE NUNCA TENDRA REGISTROS PARA PODER AGREGARLOS /////////////////////////////////
        '*******************************************************************************************************************************
        DataSet.Reset()
        DatasetReporte.Reset()
        SqlString = "SELECT Facturas.Fecha_Factura, Facturas.Numero_Factura, Facturas.MetodoPago As Numero_Recibo, Facturas.Numero_Factura As NotaDebito, Facturas.SubTotal As MontoNota, Facturas.SubTotal As Monto, Facturas.Fecha_Factura As FechaVence, Facturas.IVA As Abono, Facturas.SubTotal AS Saldo, Facturas.SubTotal As Moratorio, Facturas.SubTotal As Dias, Facturas.SubTotal AS Total  FROM Facturas INNER JOIN Clientes ON Facturas.Cod_Cliente = Clientes.Cod_Cliente  " & _
                    "WHERE (Facturas.Tipo_Factura = 'Factura') AND (Facturas.MetodoPago = 'Credito') AND (Facturas.Fecha_Factura BETWEEN CONVERT(DATETIME, '01/01/1900', 102) AND CONVERT(DATETIME, '01/01/1900', 102)) ORDER BY Facturas.Fecha_Factura, Facturas.Numero_Factura"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DatasetReporte, "TotalVentas")


        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////AGREGO LA CONSULTA PARA TODAS LAS FACTURAS DE CREDITO //////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SQlString = "SELECT *  FROM Facturas WHERE (MetodoPago = 'Credito') AND (Tipo_Factura = 'Factura') AND (Cod_Cliente = '" & Me.CboCodigoCliente.Text & "') AND (Nombre_Cliente <> N'******CANCELADO') AND (Fecha_Factura <= CONVERT(DATETIME, '" & Format(Me.DTPFechaFin.Value, "yyyy-MM-dd") & "', 102))"
        SqlString = "SELECT *  FROM Facturas WHERE (Tipo_Factura = 'Factura') AND (Numero_Factura = '" & NumeroFactura & "') AND (Cod_Cliente = '" & CodigoCliente & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Clientes")
        Registros = DataSet.Tables("Clientes").Rows.Count
        i = 0
        TotalFactura = 0
        TotalAbonos = 0
        TotalMora = 0
        TotalCargos = 0
        TotalMontoNotaDB = 0
        TotalMontoNotaCR = 0

        Do While Registros > i
            NumeroRecibo = ""
            MontoRecibo = 0

            My.Application.DoEvents()


            If Moneda = "Cordobas" Then
                If DataSet.Tables("Clientes").Rows(i)("MonedaFactura") = "Cordobas" Then
                    TasaCambio = 1
                Else
                    TasaCambio = BuscaTasaCambio(DataSet.Tables("Clientes").Rows(i)("Fecha_Factura"))
                End If
            Else
                If DataSet.Tables("Clientes").Rows(i)("MonedaFactura") = "Dolares" Then
                    TasaCambio = 1
                Else
                    TasaCambio = 1 / BuscaTasaCambio(DataSet.Tables("Clientes").Rows(i)("Fecha_Factura"))
                End If
            End If

            NumeroFactura = DataSet.Tables("Clientes").Rows(i)("Numero_Factura")
            FechaFactura = DataSet.Tables("Clientes").Rows(i)("Fecha_Factura")
            FechaVence = DataSet.Tables("Clientes").Rows(i)("Fecha_Vencimiento")

            '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '//////////////////////////////////////BUSCO SI EXISTEN RECIBOS PARA LA FACTURA //////////////////////////////////////////////////////
            '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            SqlString = "SELECT  MAX(DetalleRecibo.CodReciboPago) AS CodReciboPago, MAX(DetalleRecibo.Fecha_Recibo) AS Fecha_Recibo, DetalleRecibo.Numero_Factura, SUM(DetalleRecibo.MontoPagado) AS MontoPagado, Recibo.MonedaRecibo FROM DetalleRecibo INNER JOIN Recibo ON DetalleRecibo.CodReciboPago = Recibo.CodReciboPago AND DetalleRecibo.Fecha_Recibo = Recibo.Fecha_Recibo WHERE (DetalleRecibo.Fecha_Recibo <= CONVERT(DATETIME, '" & Format(FrmRecibos.DTPFecha.Value, "yyyy-MM-dd") & "', 102)) GROUP BY DetalleRecibo.Numero_Factura, Recibo.MonedaRecibo, Recibo.Cod_Cliente " & _
                        "HAVING (DetalleRecibo.Numero_Factura = '" & NumeroFactura & "') AND (Recibo.Cod_Cliente = '" & CodigoCliente & "')"


            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.Fill(DataSet, "Recibos")
            Registros2 = DataSet.Tables("Recibos").Rows.Count
            J = 0

            Do While Registros2 > J

                If Moneda = "Cordobas" Then
                    If DataSet.Tables("Recibos").Rows(J)("MonedaRecibo") = "Cordobas" Then
                        TasaCambioRecibo = 1
                    Else
                        TasaCambioRecibo = BuscaTasaCambio(DataSet.Tables("Recibos").Rows(J)("Fecha_Recibo"))
                    End If
                Else
                    If DataSet.Tables("Recibos").Rows(J)("MonedaRecibo") = "Dolares" Then
                        TasaCambioRecibo = 1
                    Else
                        TasaCambioRecibo = 1 / BuscaTasaCambio(DataSet.Tables("Recibos").Rows(J)("Fecha_Recibo"))
                    End If
                End If
                If NumeroRecibo = "" Then
                    NumeroRecibo = DataSet.Tables("Recibos").Rows(J)("CodReciboPago")
                Else
                    NumeroRecibo = NumeroRecibo & "," & DataSet.Tables("Recibos").Rows(J)("CodReciboPago")
                End If
                MontoRecibo = MontoRecibo + DataSet.Tables("Recibos").Rows(J)("MontoPagado") * TasaCambioRecibo

                J = J + 1
            Loop

            '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '//////////////////////////////////////BUSCO SI EXISTEN NOTAS DE DEBITO PARA ESTA FACTURA //////////////////////////////////////////////////////
            '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            'SQlString = "SELECT Detalle_Nota.*, NotaDebito.Tipo, IndiceNota.MonedaNota FROM Detalle_Nota INNER JOIN NotaDebito ON Detalle_Nota.Tipo_Nota = NotaDebito.CodigoNB INNER JOIN IndiceNota ON Detalle_Nota.Numero_Nota = IndiceNota.Numero_Nota WHERE  (NotaDebito.Tipo = 'Debito Clientes') AND (Detalle_Nota.Numero_Factura = '" & NumeroFactura & "')"
            SqlString = "SELECT Detalle_Nota.id_Detalle_Nota, Detalle_Nota.Numero_Nota, Detalle_Nota.Fecha_Nota, Detalle_Nota.Tipo_Nota, Detalle_Nota.CodigoNB, Detalle_Nota.Descripcion, Detalle_Nota.Numero_Factura, Detalle_Nota.Monto, NotaDebito.Tipo, IndiceNota.MonedaNota, IndiceNota.Fecha_Nota AS Expr1, IndiceNota.Tipo_Nota AS Expr2 FROM Detalle_Nota INNER JOIN NotaDebito ON Detalle_Nota.Tipo_Nota = NotaDebito.CodigoNB INNER JOIN IndiceNota ON Detalle_Nota.Numero_Nota = IndiceNota.Numero_Nota AND Detalle_Nota.Fecha_Nota = IndiceNota.Fecha_Nota AND Detalle_Nota.Tipo_Nota = IndiceNota.Tipo_Nota WHERE (Detalle_Nota.Fecha_Nota <= CONVERT(DATETIME, '" & Format(FrmRecibos.DTPFecha.Value, "yyyy-MM-dd") & "', 102)) AND (NotaDebito.Tipo = 'Debito Clientes') AND (Detalle_Nota.Numero_Factura = '" & NumeroFactura & "') AND (IndiceNota.Cod_Cliente = '" & CodigoCliente & "')"

            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.Fill(DataSet, "NotaDB")
            Registros2 = DataSet.Tables("NotaDB").Rows.Count
            NumeroNota = ""
            J = 0
            MontoNota = 0
            Do While Registros2 > J

                If Moneda = "Cordobas" Then
                    If DataSet.Tables("NotaDB").Rows(J)("MonedaNota") = "Cordobas" Then
                        TasaCambioRecibo = 1
                    Else
                        TasaCambioRecibo = BuscaTasaCambio(DataSet.Tables("NotaDB").Rows(J)("Fecha_Nota"))
                    End If
                Else
                    If DataSet.Tables("NotaDB").Rows(J)("MonedaNota") = "Dolares" Then
                        TasaCambioRecibo = 1
                    Else
                        TasaCambioRecibo = 1 / BuscaTasaCambio(DataSet.Tables("NotaDB").Rows(J)("Fecha_Nota"))
                    End If
                End If
                If NumeroNota = "" Then
                    NumeroNota = DataSet.Tables("NotaDB").Rows(J)("Numero_Nota")
                Else
                    NumeroNota = NumeroNota & "," & DataSet.Tables("NotaDB").Rows(J)("Numero_Nota")
                End If
                MontoNota = MontoNota + DataSet.Tables("NotaDB").Rows(J)("Monto") * TasaCambioRecibo
                TotalMontoNotaDB = TotalMontoNotaDB + MontoNota
                J = J + 1
            Loop

            DataSet.Tables("NotaDB").Reset()
            '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '//////////////////////////////////////BUSCO SI EXISTEN NOTAS DE CREDITO PARA ESTA FACTURA //////////////////////////////////////////////////////
            '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            'SQlString = "SELECT Detalle_Nota.*, NotaDebito.Tipo, IndiceNota.MonedaNota FROM Detalle_Nota INNER JOIN NotaDebito ON Detalle_Nota.Tipo_Nota = NotaDebito.CodigoNB INNER JOIN IndiceNota ON Detalle_Nota.Numero_Nota = IndiceNota.Numero_Nota WHERE  (NotaDebito.Tipo = 'Credito Clientes') AND (Detalle_Nota.Numero_Factura = '" & NumeroFactura & "')"
            SqlString = "SELECT Detalle_Nota.id_Detalle_Nota, Detalle_Nota.Numero_Nota, Detalle_Nota.Fecha_Nota, Detalle_Nota.Tipo_Nota, Detalle_Nota.CodigoNB, Detalle_Nota.Descripcion, Detalle_Nota.Numero_Factura, Detalle_Nota.Monto, NotaDebito.Tipo, IndiceNota.MonedaNota, IndiceNota.Fecha_Nota AS Expr1, IndiceNota.Tipo_Nota AS Expr2 FROM Detalle_Nota INNER JOIN NotaDebito ON Detalle_Nota.Tipo_Nota = NotaDebito.CodigoNB INNER JOIN IndiceNota ON Detalle_Nota.Numero_Nota = IndiceNota.Numero_Nota AND Detalle_Nota.Fecha_Nota = IndiceNota.Fecha_Nota AND Detalle_Nota.Tipo_Nota = IndiceNota.Tipo_Nota WHERE (Detalle_Nota.Fecha_Nota <= CONVERT(DATETIME, '" & Format(FrmRecibos.DTPFecha.Value, "yyyy-MM-dd") & "', 102)) AND (NotaDebito.Tipo = 'Credito Clientes') AND (Detalle_Nota.Numero_Factura = '" & NumeroFactura & "') AND (IndiceNota.Cod_Cliente = '" & CodigoCliente & "')"

            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.Fill(DataSet, "NotaCR")
            Registros2 = DataSet.Tables("NotaCR").Rows.Count
            NumeroNotaCR = ""
            J = 0
            MontoNotaCR = 0
            Do While Registros2 > J

                If Moneda = "Cordobas" Then
                    If DataSet.Tables("NotaCR").Rows(J)("MonedaNota") = "Cordobas" Then
                        TasaCambioRecibo = 1
                    Else
                        TasaCambioRecibo = BuscaTasaCambio(DataSet.Tables("NotaCR").Rows(J)("Fecha_Nota"))
                    End If
                Else
                    If DataSet.Tables("NotaCR").Rows(J)("MonedaNota") = "Dolares" Then
                        TasaCambioRecibo = 1
                    Else
                        TasaCambioRecibo = 1 / BuscaTasaCambio(DataSet.Tables("NotaCR").Rows(J)("Fecha_Nota"))
                    End If
                End If
                If NumeroNotaCR = "" Then
                    NumeroNotaCR = DataSet.Tables("NotaCR").Rows(J)("Numero_Nota")
                Else
                    NumeroNotaCR = NumeroNotaCR & "," & DataSet.Tables("NotaCR").Rows(J)("Numero_Nota")
                End If
                MontoNotaCR = MontoNotaCR + DataSet.Tables("NotaCR").Rows(J)("Monto") * TasaCambioRecibo
                TotalMontoNotaCR = TotalMontoNotaCR + MontoNotaCR
                J = J + 1
            Loop
            DataSet.Tables("NotaCR").Reset()

            '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '//////////////////////////////////////BUSCO EL DETALLE DE METODO PARA LAS FACTURAS DE CONTADO //////////////////////////////////////////////////////
            '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            MontoMetodoFactura = 0
            SqlString = "SELECT * FROM Detalle_MetodoFacturas INNER JOIN MetodoPago ON Detalle_MetodoFacturas.NombrePago = MetodoPago.NombrePago WHERE (Detalle_MetodoFacturas.Tipo_Factura = 'Factura') AND (Detalle_MetodoFacturas.Numero_Factura = '" & NumeroFactura & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.Fill(DataSet, "MetodoFactura")
            If DataSet.Tables("MetodoFactura").Rows.Count <> 0 Then
                If Moneda = "Cordobas" Then
                    If DataSet.Tables("MetodoFactura").Rows(0)("Moneda") = "Cordobas" Then
                        TasaCambioRecibo = 1
                    Else
                        TasaCambioRecibo = BuscaTasaCambio(DataSet.Tables("MetodoFactura").Rows(0)("Fecha_Factura"))
                    End If
                Else
                    If DataSet.Tables("MetodoFactura").Rows(0)("Moneda") = "Dolares" Then
                        TasaCambioRecibo = 1
                    Else
                        TasaCambioRecibo = 1 / BuscaTasaCambio(DataSet.Tables("MetodoFactura").Rows(0)("Fecha_Factura"))
                    End If
                End If

                MontoMetodoFactura = DataSet.Tables("MetodoFactura").Rows(0)("Monto") * TasaCambioRecibo

            End If
            DataSet.Tables("MetodoFactura").Reset()






            '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '//////////////////////////////////////BUSCO EL INTERES MORATORIO PARA ESTE CLIENTE //////////////////////////////////////////////////////
            '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            SqlString = "SELECT  * FROM Clientes WHERE (Cod_Cliente = '" & CodigoCliente & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.Fill(DataSet, "DatosCliente")
            If Not DataSet.Tables("DatosCliente").Rows.Count = 0 Then
                If Not IsDBNull(DataSet.Tables("DatosCliente").Rows(0)("InteresMoratorio")) Then
                    TasaInteres = (DataSet.Tables("DatosCliente").Rows(0)("InteresMoratorio") / 100)
                End If
            End If

            MontoFactura = (DataSet.Tables("Clientes").Rows(i)("SubTotal") + DataSet.Tables("Clientes").Rows(i)("IVA")) * TasaCambio
            FSubTotal = DataSet.Tables("Clientes").Rows(i)("SubTotal") * TasaCambio
            FIva = DataSet.Tables("Clientes").Rows(i)("IVA") * TasaCambio
            Dias = DateDiff(DateInterval.Day, FechaVence, FrmRecibos.DTPFecha.Value)
            Saldo = MontoFactura - MontoRecibo + MontoNota - MontoNotaCR - MontoMetodoFactura
            If Format(Saldo, "##,##0.00") = "0.00" Then
                Dias = 0
            End If
            MontoMora = Dias * Saldo * TasaInteres
            Total = Saldo + MontoMora

            oDataRow = DatasetReporte.Tables("TotalVentas").NewRow
            oDataRow("Fecha_Factura") = DataSet.Tables("Clientes").Rows(i)("Fecha_Factura")
            oDataRow("Numero_Factura") = DataSet.Tables("Clientes").Rows(i)("Numero_Factura")
            oDataRow("Numero_Recibo") = NumeroRecibo
            If NumeroNota = "" Then
                If NumeroNotaCR <> "" Then
                    oDataRow("NotaDebito") = "NC:" & NumeroNotaCR
                End If
            ElseIf NumeroNotaCR = "" Then
                If NumeroNota <> "" Then
                    oDataRow("NotaDebito") = "NB:" & NumeroNota
                End If
            Else
                oDataRow("NotaDebito") = "NC:" & NumeroNotaCR & " NB:" & NumeroNota
            End If
            oDataRow("Monto") = Format(MontoFactura, "##,##0.00")
            oDataRow("FechaVence") = DataSet.Tables("Clientes").Rows(i)("Fecha_Vencimiento")
            oDataRow("Abono") = Format(MontoRecibo + MontoMetodoFactura, "##,##0.00")
            oDataRow("MontoNota") = Format(MontoNota - MontoNotaCR, "##,##0.00")
            oDataRow("Saldo") = Format(Saldo, "##,##0.00")
            oDataRow("Moratorio") = Format(MontoMora, "##,##0.00")
            oDataRow("Dias") = Dias
            oDataRow("Total") = Format(Total, "##,##0.00")
            DatasetReporte.Tables("TotalVentas").Rows.Add(oDataRow)

            ValidarFacturaEnRecibo = Saldo

            i = i + 1

            TotalFactura = TotalFactura + Saldo
            TotalAbonos = TotalAbonos + MontoRecibo
            TotalCargos = TotalCargos + MontoFactura
            TotalMora = TotalMora + MontoMora
            DataSet.Tables("DatosCliente").Reset()
            DataSet.Tables("Recibos").Reset()
        Loop

    End Function

    Public Function PerteneceProductoBodega(ByVal CodigoBodega As String, ByVal CodigoProducto As String) As Boolean
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, Existencia As Double
        Dim SqlDatos As String

        '---------------------------LEER DATOS EMPRESA ------------------------
        SqlDatos = "SELECT Cod_Bodegas, Cod_Productos, Existencia, Costo, Ultimo_Precio_Compra, Existencia_Dinero, Existencia_Unidades, Existencia_DineroDolar FROM DetalleBodegas " & _
                   "WHERE (Cod_Productos = '" & CodigoProducto & "') AND (Cod_Bodegas = '" & CodigoBodega & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlDatos, MiConexion)
        DataAdapter.Fill(DataSet, "Consulta")
        If Not DataSet.Tables("Consulta").Rows.Count = 0 Then
            PerteneceProductoBodega = True
        Else
            PerteneceProductoBodega = False

            '---------------------------BUSCO EL CODIGO ALTERNO ------------------------
            SqlDatos = "SELECT  * FROM Codigos_Alternos INNER JOIN Productos ON Codigos_Alternos.Cod_Producto = Productos.Cod_Productos INNER JOIN DetalleBodegas ON Productos.Cod_Productos = DetalleBodegas.Cod_Productos " & _
                       "WHERE (Codigos_Alternos.Cod_Alternativo = '" & CodigoProducto & "') AND (DetalleBodegas.Cod_Bodegas = '" & CodigoBodega & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlDatos, MiConexion)
            DataAdapter.Fill(DataSet, "CodigoAlterno")
            If Not DataSet.Tables("CodigoAlterno").Rows.Count = 0 Then
                PerteneceProductoBodega = True
            Else
                PerteneceProductoBodega = False
            End If
        End If






    End Function

    Public Sub ActualizaDetalleBodega(ByVal CodigoBodega As String, ByVal CodigoProducto As String)
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, Existencia As Double
        Dim iPosicionFila As Double = 0, StrSQLUpdate As String
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer

        My.Application.DoEvents()
        'CodigoBodega = DataSet.Tables("Bodegas").Rows(iPosicionFila)("Cod_Bodegas")


        ExistenciaBodega = BuscaExistenciaBodega(CodigoProducto, CodigoBodega)
        Existencia = Existencia + ExistenciaBodega
        MiConexion.Close()
        '///////////ACTUALIZO LA EXISTENCIA PARA CADA BODEGA ////////////////////////////////////////
        StrSQLUpdate = "UPDATE [DetalleBodegas]  SET [Existencia_Unidades] = '" & ExistenciaBodega & "',[Existencia] = '" & ExistenciaBodega & "' WHERE (Cod_Productos = '" & CodigoProducto & "') AND (Cod_Bodegas = '" & CodigoBodega & "')"
        MiConexion.Open()
        ComandoUpdate = New SqlClient.SqlCommand(StrSQLUpdate, MiConexion)
        iResultado = ComandoUpdate.ExecuteNonQuery
        MiConexion.Close()
    End Sub



    Public Function BloquearPrecioVentas(ByVal CodigoProducto As String, ByVal PrecioVenta As Double, ByVal FechaFactura As Date, ByVal Moneda As String) As Boolean
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim SqlDatos As String, Iposicion As Double = 0
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim CostoUnitario As Double


        BloquearPrecioVentas = False

        '---------------------------LEER DATOS EMPRESA ------------------------
        SqlDatos = "SELECT * FROM DatosEmpresa"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlDatos, MiConexion)
        DataAdapter.Fill(DataSet, "DatosEmpresa")
        If Not DataSet.Tables("DatosEmpresa").Rows.Count = 0 Then

            If Not IsDBNull(DataSet.Tables("DatosEmpresa").Rows(0)("BloquearPreciosFactura")) Then
                If DataSet.Tables("DatosEmpresa").Rows(0)("BloquearPreciosFactura") = "True" Then

                    '------------------SOLICITO AUTORIZACION DE PRECIOS ------------------------------------
                    My.Forms.FrmPermisos.ShowDialog()
                    If FrmPermisos.RContrase�a = False Then
                        BloquearPrecioVentas = True
                    Else
                        BloquearPrecioVentas = False
                    End If

                    Exit Function
                End If
            End If

            If Not IsDBNull(DataSet.Tables("DatosEmpresa").Rows(0)("BloquearBajoCosto")) Then
                If DataSet.Tables("DatosEmpresa").Rows(0)("BloquearBajoCosto") = "True" Then

                    CostoUnitario = CostoPromedioKardex(CodigoProducto, FechaFactura)

                    If Moneda <> "Cordobas" Then
                        PrecioVenta = PrecioVenta * BuscaTasaCambio(FechaFactura)
                    End If

                    If CDbl(Format(CostoUnitario, "##,##0.00")) > CDbl(Format(PrecioVenta, "##,##0.00")) Then

                        '------------------SOLICITO AUTORIZACION DE PRECIOS ------------------------------------
                        My.Forms.FrmPermisos.ShowDialog()
                        If FrmPermisos.RContrase�a = False Then
                            BloquearPrecioVentas = True
                        Else
                            BloquearPrecioVentas = False
                        End If

                        Exit Function
                    End If

                End If
            End If

        End If




    End Function

    Public Function CategoriaPrecio(ByVal CodigoProducto As String, ByVal Precio As Double, ByVal Moneda As String) As String
        Dim SqlString As String, Iposicion As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim ComandoUpdate As New SqlClient.SqlCommand
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, PrecioVentaDolar As Double
        Dim FechaFin As Date, Porciento As Double, Incremento As Double, TasaCambio As Double, CostoUnitario As Double, PrecioVenta As Double, PrecioAnterior As Double = 0


        CategoriaPrecio = "0"


        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        '++++++++++++++++++++++CONSULTO SI EL TIPO DE PRECIO ES PORCENTUAL +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        SqlString = "SELECT  TipoPrecio.Tipo_Precio AS Descripcion, Precios.Monto_Precio, Precios.Monto_PrecioDolar, TipoPrecio.Cod_TipoPrecio, TipoPrecio.PrecioPorcentual, TipoPrecio.Porciento, TipoPrecio.Categoria FROM Precios INNER JOIN  TipoPrecio ON Precios.Cod_TipoPrecio = TipoPrecio.Cod_TipoPrecio " & _
                    "WHERE (Precios.Cod_Productos = '" & CodigoProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "PreciosFactura")
        Iposicion = 0
        Do While Iposicion < DataSet.Tables("PreciosFactura").Rows.Count

            'If Not IsDBNull(DataSet.Tables("PreciosFactura").Rows(Iposicion)("Categoria")) Then
            '    CategoriaPrecio = DataSet.Tables("PreciosFactura").Rows(Iposicion)("Categoria")
            'Else
            '    CategoriaPrecio = "00"
            'End If

            Porciento = 0

            If DataSet.Tables("PreciosFactura").Rows(Iposicion)("PrecioPorcentual") = "True" Then
                FechaFin = Format(Now, "dd/MM/yyyy")
                If Not IsDBNull(DataSet.Tables("PreciosFactura").Rows(Iposicion)("Porciento")) Then
                    If DataSet.Tables("PreciosFactura").Rows(Iposicion)("Porciento") <> "" Then
                        Porciento = DataSet.Tables("PreciosFactura").Rows(Iposicion)("Porciento")
                    End If

                End If
                Incremento = 1 + (CDbl(Porciento) / 100)
                TasaCambio = BuscaTasaCambio(FechaFin)
                CostoUnitario = CostoPromedioKardex(CodigoProducto, FechaFin)
                CostoUnitarioB = CostoUnitario
                PrecioVenta = Format(CostoUnitario * Incremento, "##,##0.00")
                PrecioVentaDolar = Format(PrecioVenta / TasaCambio, "##,##0.00")
            Else
                PrecioVenta = DataSet.Tables("PreciosFactura").Rows(Iposicion)("Monto_Precio")
                PrecioVentaDolar = DataSet.Tables("PreciosFactura").Rows(Iposicion)("Monto_PrecioDolar")
            End If

            If Moneda = "Cordobas" Then

                If Precio >= PrecioVenta Then
                    If PrecioAnterior < PrecioVenta Then
                        CategoriaPrecio = DataSet.Tables("PreciosFactura").Rows(Iposicion)("Categoria")
                        PrecioAnterior = PrecioVenta
                    End If
                Else
                    CategoriaPrecio = "-00"
                End If
            ElseIf Moneda = "Dolares" Then

                If Precio >= PrecioVentaDolar Then
                    If PrecioAnterior < PrecioVenta Then
                        CategoriaPrecio = DataSet.Tables("PreciosFactura").Rows(Iposicion)("Categoria")
                        PrecioAnterior = PrecioVenta
                    End If
                Else
                    'CategoriaPrecio = "-00"
                End If

            End If


            Iposicion = Iposicion + 1
        Loop



        If FrmFacturas.CboTipoProducto.Text = "Salida Bodega" Then
            CategoriaPrecio = "SB"
        End If

    End Function




    Public Sub Demo(ByVal FechaSalida As Date)
        Dim Horas As Double, HorasEncriptadas As String
        Dim Cadena As String, Cadena2 As String = "", Cadena3 As String  ' Cadena3 its an decrypted value from cadena2
        Dim result As Double, SqlDatos As String

        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, SQlUpdate As String = ""
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, SqlString As String = ""
        Dim StrSqlUpdate As String

        ' Cadena = Encrypt("Siempre")
        Cadena = "ABC"

        '---------------------------LEER DATOS EMPRESA ------------------------
        SqlDatos = "SELECT * FROM DatosEmpresa"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlDatos, MiConexion)
        DataAdapter.Fill(DataSet, "DatosEmpresa")
        If Not DataSet.Tables("DatosEmpresa").Rows.Count = 0 Then

            If Not IsDBNull(DataSet.Tables("DatosEmpresa").Rows(0)("Valor")) Then
                If Not DataSet.Tables("DatosEmpresa").Rows(0)("Valor") = "" Then
                    Cadena2 = DataSet.Tables("DatosEmpresa").Rows(0)("Valor")
                    Cadena2 = Decrypt(Cadena2)
                End If
            End If

        End If


        Cadena3 = Mid(Cadena2, 1, 3)
        If Cadena3 = "ABC" Then
            Horas = DateDiff("n", FechaIngreso, FechaSalida)
            result = CDbl(Mid(Cadena2, 4, 9)) + Horas
            Cadena = Cadena & result
            HorasEncriptadas = Encrypt(Cadena)
            '      Cadena2 = Decrypt(HorasEncriptadas)


            '///////////SI EXISTE EL USUARIO LO ACTUALIZO////////////////

            StrSqlUpdate = "UPDATE [DatosEmpresa] SET [Valor] = '" & HorasEncriptadas & "' "
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(StrSqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        ElseIf Not Cadena2 = "Siempre" Then

            Cadena = "ABC3600"
            HorasEncriptadas = Encrypt(Cadena)
            '///////////SI EXISTE EL USUARIO LO ACTUALIZO////////////////

            StrSqlUpdate = "UPDATE [DatosEmpresa] SET [Valor] = '" & HorasEncriptadas & "' "
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(StrSqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub



    Function Encrypt(ByVal Frase As String) As String
        Dim Ilen As Integer, X As Integer
        Dim sFrase As String = "", sCurrent As String, sNew As String
        Ilen = Len(Frase)
        For X = 1 To Ilen
            sCurrent = Mid$(Frase, X, 1)
            sNew = Chr(Asc(sCurrent) + 110)
            sFrase = sFrase & sNew
        Next
        Encrypt = sFrase
    End Function

    Function Decrypt(ByVal Frase As String) As String
        Dim Ilen As Integer, X As Integer
        Dim sFrase As String = "", sCurrent As String, sNew As String
        Ilen = Len(Frase)
        For X = 1 To Ilen
            sCurrent = Mid$(Frase, X, 1)
            sNew = Chr(Asc(sCurrent) - 110)
            sFrase = sFrase & sNew
        Next
        Decrypt = sFrase
    End Function

    '/////Mensaje - Constante para establecer el ancho  
    Public Declare Function SendMessage Lib "user32" Alias _
                        "SendMessageA" (ByVal hwnd As Long, _
                         ByVal wMsg As Long, ByVal wParam As Long, _
    ByVal lParam As Long) As Long
    Private Const CB_SETDROPPEDWIDTH = &H160
    Public Sub ActualizaMontoCredito(ByVal NumeroFactura As String, ByVal FechaFactura As Date, ByVal Saldo As Double, ByVal MonedaSaldo As String)
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, SQlUpdate As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, SqlString As String
        Dim Fecha As String, MonedaFactura As String, TasaCambio As Double

        Fecha = Format(FechaFactura, "yyyy-MM-dd")

        SqlString = "SELECT * FROM Facturas WHERE (Numero_Factura = '" & NumeroFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = 'Factura')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Actualiza")
        If DataSet.Tables("Actualiza").Rows.Count <> 0 Then
            MonedaFactura = DataSet.Tables("Actualiza").Rows(0)("MonedaFactura")

            If MonedaFactura = "Cordobas" Then
                If MonedaSaldo = "Cordobas" Then
                    TasaCambio = 1
                Else
                    TasaCambio = BuscaTasaCambio(FechaFactura)
                End If
            Else
                If MonedaSaldo = "Dolares" Then
                    TasaCambio = 1
                Else
                    TasaCambio = 1 / BuscaTasaCambio(FechaFactura)
                End If
            End If




            '(Saldo)= -1.#INF Then
            '           Saldo = 0
            '       End If
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SQlUpdate = "UPDATE [Facturas] SET [MontoCredito] = " & Saldo * TasaCambio & " WHERE (Numero_Factura = '" & NumeroFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = 'Factura')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SQlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If

    End Sub

    Public Function FacturaSerie() As Boolean
        Dim SqlConsecutivo As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, FacturaBodega As Boolean = False, CompraBodega As Boolean = False


        FacturaSerie = False
        '/////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////BUSCO SI TIENE ACTIVADA LA OPCION DE CONSECUTIVO X BODEGA /////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////
        SqlConsecutivo = "SELECT * FROM  DatosEmpresa"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsecutivo, MiConexion)
        DataAdapter.Fill(DataSet, "Configuracion")
        If Not DataSet.Tables("Configuracion").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacBodega")) Then
                FacturaBodega = DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacBodega")
            End If

            If Not IsDBNull(DataSet.Tables("Configuracion").Rows(0)("ConsecutivoComBodega")) Then
                CompraBodega = DataSet.Tables("Configuracion").Rows(0)("ConsecutivoComBodega")
            End If

            If Not IsDBNull(DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacSerie")) Then
                FacturaSerie = DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacSerie")
            End If

        End If

    End Function


    Public Function GenerarNumeroFactura(ByVal ConsecutivoFacturaManual As Boolean, ByVal TipoFactura As String) As String
        Dim ConsecutivoFactura As Double, SqlConsecutivo As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, FacturaBodega As Boolean = False, CompraBodega As Boolean = False
        Dim NumeroFactura As String, FacturaSerie As Boolean = False, SqlString As String, Numero As Double = 0
        Dim CadenaDiv() As String


        '/////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////BUSCO SI TIENE ACTIVADA LA OPCION DE CONSECUTIVO X BODEGA /////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////
        SqlConsecutivo = "SELECT * FROM  DatosEmpresa"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsecutivo, MiConexion)
        DataAdapter.Fill(DataSet, "Configuracion")
        If Not DataSet.Tables("Configuracion").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacBodega")) Then
                FacturaBodega = DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacBodega")
            End If

            If Not IsDBNull(DataSet.Tables("Configuracion").Rows(0)("ConsecutivoComBodega")) Then
                CompraBodega = DataSet.Tables("Configuracion").Rows(0)("ConsecutivoComBodega")
            End If

            If Not IsDBNull(DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacSerie")) Then
                FacturaSerie = DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacSerie")
            End If

        End If

        '////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////BUSCO EL CONSECUTIVO DE LA COMPRA /////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////7

        If FrmFacturas.TxtNumeroEnsamble.Text = "-----0-----" Then

            Select Case TipoFactura
                Case "Cotizacion"
                    If FacturaSerie = False Then
                        ConsecutivoFactura = BuscaConsecutivo("Cotizacion")
                    Else
                        ConsecutivoFactura = BuscaConsecutivoSerie("Cotizacion", FrmFacturas.CmbSerie.Text)
                    End If
                Case "Factura"
                    If ConsecutivoFacturaManual = False Then
                        If FacturaSerie = False Then
                            ConsecutivoFactura = BuscaConsecutivo("Factura")
                        Else
                            ConsecutivoFactura = BuscaConsecutivoSerie("Factura", FrmFacturas.CmbSerie.Text)
                        End If
                    Else
                        FrmConsecutivos.ShowDialog()
                        If FrmConsecutivos.TxtConsecutivo.Text <> "-----0-----" Then
                            ConsecutivoFactura = FrmConsecutivos.NumeroFactura
                        Else
                            ConsecutivoFactura = -1
                        End If
                    End If
                Case "Devolucion de Venta"
                    If FacturaSerie = False Then
                        ConsecutivoFactura = BuscaConsecutivo("DevFactura")
                    Else
                        ConsecutivoFactura = BuscaConsecutivoSerie("DevFactura", FrmFacturas.CmbSerie.Text)
                    End If
                Case "Transferencia Enviada"
                    If FacturaSerie = False Then
                        ConsecutivoFactura = BuscaConsecutivo("Transferencia_Enviada")
                    Else
                        ConsecutivoFactura = BuscaConsecutivoSerie("Transferencia_Enviada", FrmFacturas.CmbSerie.Text)
                    End If
                Case "Salida Bodega"
                    If FacturaSerie = False Then
                        ConsecutivoFactura = BuscaConsecutivo("SalidaBodega")
                    Else
                        ConsecutivoFactura = BuscaConsecutivoSerie("SalidaBodega", FrmFacturas.CmbSerie.Text)
                    End If

                Case "Orden de Trabajo"
                    If FacturaSerie = False Then
                        ConsecutivoFactura = BuscaConsecutivo("OrdenSalida")
                    Else
                        ConsecutivoFactura = BuscaConsecutivoSerie("OrdenSalida", FrmFacturas.CmbSerie.Text)
                    End If

            End Select

            If ConsecutivoFactura <> -1 Then
                If FacturaBodega = True Then
                    NumeroFactura = FrmFacturas.CboCodigoBodega.Columns(0).Text & "-" & Format(ConsecutivoFactura, "0000#")
                ElseIf FacturaSerie = True Then
                    NumeroFactura = FrmFacturas.CmbSerie.Text & Format(ConsecutivoFactura, "0000#")
                    FrmFacturas.CmbSerie.Enabled = False
                Else
                    NumeroFactura = Format(ConsecutivoFactura, "0000#")
                End If

            Else
                NumeroFactura = "-----0-----"
            End If

            '----------------------------------------------------------------------------------------------------------------------------------------
            '-----------------------------VERIFICO QUE EL CONSECUTIVO NO EXISTE EN LAS FACTURAS GRABADAS--------------------------------------------
            '----------------------------------------------------------------------------------------------------------------------------------------
            SqlString = "SELECT  *  FROM Facturas WHERE (Numero_Factura = '" & NumeroFactura & "') AND (Tipo_Factura = '" & TipoFactura & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.Fill(DataSet, "Consulta")
            If DataSet.Tables("Consulta").Rows.Count <> 0 Then
                '-------------------------SI EXISTE ESTA FACTURA GRABADA, ENTONCES RECOMIENDO EL SIGUIENTE SEGUN LA FACTURACION ------------------------------

                If FacturaBodega = True Then
                    CadenaDiv = NumeroFactura.Split("-")
                    SqlString = "SELECT  *  FROM Facturas WHERE (Tipo_Factura = '" & TipoFactura & "') AND (Numero_Factura LIKE '" & CadenaDiv(0) & "%') ORDER BY Numero_Factura DESC"
                ElseIf FacturaSerie = True Then
                    SqlString = "SELECT  *  FROM Facturas WHERE (Tipo_Factura = '" & TipoFactura & "') AND (Numero_Factura LIKE '" & My.Forms.FrmFacturas.CmbSerie.Text & "%') ORDER BY Numero_Factura DESC"
                Else
                    SqlString = "SELECT  *  FROM Facturas WHERE (Tipo_Factura = '" & TipoFactura & "') ORDER BY Numero_Factura DESC"
                End If


                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "Facturas")
                If DataSet.Tables("Facturas").Rows.Count <> 0 Then
                    NumeroFactura = DataSet.Tables("Facturas").Rows(0)("Numero_Factura")
                    Numero = Mid(NumeroFactura, Len(My.Forms.FrmFacturas.CmbSerie.Text) + 1, Len(NumeroFactura))
                    ConsecutivoFactura = Numero + 1
                End If


                If FacturaBodega = True Then
                    NumeroFactura = FrmFacturas.CboCodigoBodega.Columns(0).Text & "-" & Format(ConsecutivoFactura, "0000#")
                ElseIf FacturaSerie = True Then
                    NumeroFactura = FrmFacturas.CmbSerie.Text & Format(ConsecutivoFactura, "0000#")
                    FrmFacturas.CmbSerie.Enabled = False
                Else
                    NumeroFactura = Format(ConsecutivoFactura, "0000#")
                End If
            End If

        Else
            NumeroFactura = FrmFacturas.TxtNumeroEnsamble.Text

        End If
        GenerarNumeroFactura = NumeroFactura


    End Function
    Public Function CostoProyecto(ByVal CodigoProyecto As String, ByVal FechaFactura As Date, ByVal MonedaFactura As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), SqlString As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, i As Double
        Dim Fecha As String, CostoPromedio As String, CodProducto As String, CostoPromedioDolares As Double
        Dim Cantidad As Double = 0, TasaCambio As Double

        Fecha = Format(FechaFactura, "yyyy-MM-dd")

        'SqlString = "SELECT id_Detalle_Factura, Numero_Factura, Fecha_Factura, Tipo_Factura, Cod_Producto, Descripcion_Producto, Cantidad, Precio_Unitario, Descuento, Precio_Neto, Importe, TasaCambio, CodTarea, Costo_Unitario FROM Detalle_Facturas WHERE (Factura.CodigoProyecto = '" & CodigoProyecto & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = 'Factura')"
        SqlString = "SELECT Detalle_Facturas.id_Detalle_Factura, Detalle_Facturas.Numero_Factura, Detalle_Facturas.Fecha_Factura, Detalle_Facturas.Tipo_Factura, Detalle_Facturas.Cod_Producto, Detalle_Facturas.Descripcion_Producto, Detalle_Facturas.Cantidad, Detalle_Facturas.Precio_Unitario, Detalle_Facturas.Descuento, Detalle_Facturas.Precio_Neto, Detalle_Facturas.Importe, Detalle_Facturas.TasaCambio, Detalle_Facturas.CodTarea, Detalle_Facturas.Costo_Unitario, Facturas.CodigoProyecto FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                    "WHERE (Facturas.CodigoProyecto = '" & CodigoProyecto & "') AND (Facturas.Tipo_Factura <> 'Cotizacion' AND Facturas.Tipo_Factura <> 'Devolucion de Venta')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Facturas")

        '//////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////BUSCO LA EXISTENCIA DE ESTE PRODUCTO PARA CADA BODEGA//////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////

        TasaCambio = BuscaTasaCambio(Fecha)

        i = 0
        CostoPromedio = 0
        Cantidad = 0
        Do While i < (DataSet.Tables("Facturas").Rows.Count)
            CodProducto = DataSet.Tables("Facturas").Rows(i)("Cod_Producto")
            Cantidad = DataSet.Tables("Facturas").Rows(i)("Cantidad")
            'CostoPromedio = CostoPromedioKardex(CodProducto, FechaFactura) * Cantidad + Coomedio
            CostoPromedio = DataSet.Tables("Facturas").Rows(i)("Costo_Unitario") * Cantidad + CostoPromedio
            CostoPromedioDolares = (CostoPromedio / TasaCambio) + CostoPromedioDolares
            i = i + 1
        Loop

        If MonedaFactura = "Cordobas" Then
            CostoProyecto = CostoPromedio
        Else
            CostoProyecto = CostoPromedioDolares
        End If

    End Function






    Public Function CostoFactura(ByVal NumeroFactura As String, ByVal FechaFactura As Date, ByVal MonedaFactura As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), SqlString As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, i As Double
        Dim Fecha As String, CostoPromedio As String, CodProducto As String, CostoPromedioDolares As Double
        Dim Cantidad As Double = 0, TasaCambio As Double

        Fecha = Format(FechaFactura, "yyyy-MM-dd")

        SqlString = "SELECT id_Detalle_Factura, Numero_Factura, Fecha_Factura, Tipo_Factura, Cod_Producto, Descripcion_Producto, Cantidad, Precio_Unitario, Descuento, Precio_Neto, Importe, TasaCambio, CodTarea, Costo_Unitario FROM Detalle_Facturas WHERE (Numero_Factura = '" & NumeroFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = 'Factura')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Facturas")

        '//////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////BUSCO LA EXISTENCIA DE ESTE PRODUCTO PARA CADA BODEGA//////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////

        TasaCambio = BuscaTasaCambio(Fecha)

        i = 0
        CostoPromedio = 0
        Cantidad = 0
        Do While i < (DataSet.Tables("Facturas").Rows.Count)
            CodProducto = DataSet.Tables("Facturas").Rows(i)("Cod_Producto")
            Cantidad = DataSet.Tables("Facturas").Rows(i)("Cantidad")
            'CostoPromedio = CostoPromedioKardex(CodProducto, FechaFactura) * Cantidad + CostoPromedio
            If Not IsDBNull(DataSet.Tables("Facturas").Rows(i)("Costo_Unitario")) Then
                CostoPromedio = DataSet.Tables("Facturas").Rows(i)("Costo_Unitario") * Cantidad + CostoPromedio
                CostoPromedioDolares = (CostoPromedio / TasaCambio) + CostoPromedioDolares
            End If
            i = i + 1
        Loop

        If MonedaFactura = "Cordobas" Then
            CostoFactura = CostoPromedio
        Else
            CostoFactura = CostoPromedioDolares
        End If

    End Function




    Public Sub VerificarCompras(ByVal NumeroCompra As String, ByVal FechaCompra As Date, ByVal Moneda As String, ByVal Tipo As String)
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), SqlString As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, i As Double, TotalImporte As Double, Cantidad As Double, PrecioUnitario As Double, Importe As Double
        Dim TasaCambio As Double, Fecha As String
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer

        Fecha = Format(FechaCompra, "yyyy-MM-dd")

        SqlString = "SELECT  *  FROM Detalle_Compras WHERE (Numero_Compra = '" & NumeroCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & Tipo & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")

        '//////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////BUSCO LA EXISTENCIA DE ESTE PRODUCTO PARA CADA BODEGA//////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////

        i = 0
        TotalImporte = 0
        Do While i < (DataSet.Tables("Compras").Rows.Count)
            Cantidad = DataSet.Tables("Compras").Rows(i)("Cantidad")
            PrecioUnitario = DataSet.Tables("Compras").Rows(i)("Precio_Unitario")
            Importe = Cantidad * PrecioUnitario
            TotalImporte = TotalImporte + Importe
            TasaCambio = BuscaTasaCambio(Fecha)



            SqlString = "SELECT  * FROM Detalle_Compras WHERE (Numero_Compra = '" & NumeroCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & Tipo & "') "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.Fill(DataSet, "NotaDebito")
            If DataSet.Tables("NotaDebito").Rows.Count <> 0 Then

                '//////////////////////////////////////////////////////////////////////////////////////////////
                '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
                '/////////////////////////////////////////////////////////////////////////////////////////////////
                SqlCompras = "UPDATE [Detalle_Compras] SET [TasaCambio] = " & TasaCambio & " WHERE (Numero_Compra = '" & NumeroCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & Tipo & "') "
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()
            End If







            i = i + 1
        Loop


    End Sub
    Public Function EmailVendedor(ByVal CodigoVendedor As String) As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Sql As String
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////////////BUSCO EL CODIGO DEL IMPUESTO PARA EL PRODUCTO///////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        EmailVendedor = ""
        Sql = "SELECT  * FROM Vendedores WHERE (Cod_Vendedor = '" & CodigoVendedor & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(Sql, MiConexion)
        DataAdapter.Fill(DataSet, "Vendedor")
        If DataSet.Tables("Vendedor").Rows.Count <> 0 Then
            EmailVendedor = DataSet.Tables("Vendedor").Rows(0)("Direccion_Vendedor")
        End If


    End Function
    Public Function TelefonoVendedor(ByVal CodigoVendedor As String) As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Sql As String
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////////////BUSCO EL CODIGO DEL IMPUESTO PARA EL PRODUCTO///////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        TelefonoVendedor = ""
        Sql = "SELECT  * FROM Vendedores WHERE (Cod_Vendedor = '" & CodigoVendedor & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(Sql, MiConexion)
        DataAdapter.Fill(DataSet, "Vendedor")
        If DataSet.Tables("Vendedor").Rows.Count <> 0 Then
            TelefonoVendedor = DataSet.Tables("Vendedor").Rows(0)("Telefono_Vendedor")
        End If


    End Function

    Public Function ValidarExistenciaNegativa(ByVal CodigoProducto As String) As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim SQL As String, ExistenciaNegativa As String = "NO"
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////////////BUSCO EL CODIGO DEL IMPUESTO PARA EL PRODUCTO///////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SQL = "SELECT  * FROM Productos WHERE (Cod_Productos = '" & CodigoProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SQL, MiConexion)
        DataAdapter.Fill(DataSet, "Productos")
        If DataSet.Tables("Productos").Rows.Count <> 0 Then
            ExistenciaNegativa = DataSet.Tables("Productos").Rows(0)("Existencia_Negativa")
        End If

        ValidarExistenciaNegativa = ExistenciaNegativa
    End Function

    Public Function ValidarConexionContable(ByVal FechaTasa As Date) As Boolean
        Dim MiconexionContabilidad As New SqlClient.SqlConnection(ConexionContabilidad)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter

        Try

            MiconexionContabilidad.Open()
            If MiconexionContabilidad.State = ConnectionState.Open Then
                ValidarConexionContable = True
            Else
                ValidarConexionContable = False
            End If

        Catch ex As Exception
            ValidarConexionContable = False
        End Try



    End Function

    Public Function BuscaPrimerCuentaContable(ByVal TipoCuenta) As String

        Dim SqlString As String = ""
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), MiConexionContabilidad As New SqlClient.SqlConnection(ConexionContabilidad)
        Dim DataSet As New DataSet, TotalCantidad As Double = 0
        Dim DataAdapter As New SqlClient.SqlDataAdapter


        BuscaPrimerCuentaContable = "5101"
        Try

            SqlString = "SELECT  *  FROM Cuentas WHERE (TipoCuenta = '" & TipoCuenta & "') ORDER BY TipoCuenta"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexionContabilidad)
            DataAdapter.Fill(DataSet, "Cuenta")
            If DataSet.Tables("Cuenta").Rows.Count <> 0 Then
                BuscaPrimerCuentaContable = DataSet.Tables("Cuenta").Rows(0)("CodCuentas")
            End If





        Catch ex As Exception

            If Err.Number = 5 Then
                Select Case TipoCuenta
                    Case "Inventario" : BuscaPrimerCuentaContable = "1141"
                    Case "Ingresos - Ventas" : BuscaPrimerCuentaContable = "4101"
                    Case "Costos" : BuscaPrimerCuentaContable = "5101"
                    Case "Gastos" : BuscaPrimerCuentaContable = "6101"


                End Select
            End If


        End Try





    End Function


    Public Function TasaCambioCompara(ByVal Fecha As Date, ByVal MonedaCompra As String, ByVal MonedaEnsamble As String) As Double

        If MonedaCompra = "Cordobas" Then
            If MonedaEnsamble = "Cordobas" Then
                TasaCambioCompara = 1
            Else
                If BuscaTasaCambio(Fecha) <> 0 Then
                    TasaCambioCompara = (1 / BuscaTasaCambio(Fecha))
                End If
            End If
        ElseIf MonedaCompra = "Dolares" Then
            If MonedaEnsamble = "Cordobas" Then
                TasaCambioCompara = BuscaTasaCambio(Fecha)
            Else
                TasaCambioCompara = 1
            End If
        End If
    End Function

    Public Sub ActualizaLotes()
        Dim SQlString As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim i As Double = 0
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)

        SQlString = "SELECT id_Detalle_Lote, Cantidad, Numero_Lote, FechaVence, Tipo_Documento, Numero_Documento, Fecha, Codigo_Producto  FROM Detalle_Lote WHERE(id_Detalle_Lote = -1000000)"
        DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
        DataAdapter.Fill(DataSet, "Lotes")

        FrmLotes.BindingDetalle.DataSource = DataSet.Tables("Lotes")
        FrmLotes.TrueDBGridComponentes.DataSource = FrmLotes.BindingDetalle
        FrmLotes.TrueDBGridComponentes.Col = 1
        FrmLotes.TrueDBGridComponentes.Row = 0
        FrmLotes.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Visible = False
        FrmLotes.TrueDBGridComponentes.Columns(1).Caption = "Cantidad"
        FrmLotes.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Width = 73
        FrmLotes.TrueDBGridComponentes.Columns(2).Caption = "Nombre Lote"
        FrmLotes.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Width = 330
        FrmLotes.TrueDBGridComponentes.Columns(3).Caption = "Fecha Vence"
        FrmLotes.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Width = 100
        FrmLotes.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Visible = False
        FrmLotes.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Visible = False
        FrmLotes.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Visible = False
        FrmLotes.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(7).Visible = False
    End Sub
    Public Sub ActualizaSeries()
        Dim NumeroSerie As String, SqlString As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim oDataRow As DataRow, i As Double

        Select Case FrmSeries.Tipo
            Case "Transferencia Enviada"
                SqlString = "SELECT Id_Series, NSeries FROM Detalle_ComprasSeries WHERE (Numero_Compra = N'-1000000')"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "Series")
            Case "Devolucion de Compra"
                SqlString = "SELECT Id_Series, NSeries FROM Detalle_ComprasSeries WHERE (Numero_Compra = N'-1000000')"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "Series")
            Case "Mercancia Recibida"
                SqlString = "SELECT Id_Series, NSeries FROM Detalle_ComprasSeries WHERE (Numero_Compra = N'-1000000')"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "Series")
            Case "Factura"
                SqlString = "SELECT Id_Series, NSeries FROM Detalle_ComprasSeries WHERE (Numero_Compra = N'-1000000')"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "Series")
            Case "Devolucion de Venta"
                SqlString = "SELECT Id_Series, NSeries FROM Detalle_ComprasSeries WHERE (Numero_Compra = N'-1000000')"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "Series")
            Case "Salida Bodega"
                SqlString = "SELECT Id_Series, NSeries FROM Detalle_ComprasSeries WHERE (Numero_Compra = N'-1000000')"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "Series")
            Case "Ensamble Recibido"
                SqlString = "SELECT Id_Series, NSeries FROM Detalle_ComprasSeries WHERE (Numero_Compra = N'-1000000')"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "Series")
            Case "Deshacer Ensamble"
                SqlString = "SELECT Id_Series, NSeries FROM Detalle_ComprasSeries WHERE (Numero_Compra = N'-1000000')"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "Series")
        End Select

        For i = 1 To 100
            oDataRow = DataSet.Tables("Series").NewRow
            oDataRow("Id_Series") = i
            NumeroSerie = BuscaSerie(FrmSeries.Numero, FrmSeries.Fecha, FrmSeries.Tipo, i, FrmSeries.CodigoProducto)
            oDataRow("NSeries") = NumeroSerie
            DataSet.Tables("Series").Rows.Add(oDataRow)
        Next
        FrmSeries.BindingDetalle.DataSource = DataSet.Tables("Series")
        FrmSeries.TrueDBGridComponentes.DataSource = FrmSeries.BindingDetalle
        FrmSeries.TrueDBGridComponentes.Col = 1
        FrmSeries.TrueDBGridComponentes.Row = 0
        FrmSeries.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Locked = True
        FrmSeries.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Width = 74
        FrmSeries.TrueDBGridComponentes.Columns(0).Caption = "Linea"
        FrmSeries.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Width = 209
        FrmSeries.TrueDBGridComponentes.Columns(1).Caption = "Numero de Serie"
    End Sub



    Public Function BuscaSerie(ByVal Numero As String, ByVal FechaSerie As Date, ByVal Tipo As String, ByVal Id As Double, ByVal CodigoProducto As String) As String
        Dim ComandoUpdate As New SqlClient.SqlCommand, Fecha As String
        Dim SqlString As String, MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter

        Fecha = Format(FechaSerie, "yyyy-MM-dd")
        BuscaSerie = ""

        SqlString = "SELECT  * FROM Detalle_ComprasSeries WHERE (Numero_Compra = '" & Numero & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & Tipo & "') AND (Id_Series = " & Id & ") AND (Cod_Producto = '" & CodigoProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "NotaDebito")
        If DataSet.Tables("NotaDebito").Rows.Count <> 0 Then
            BuscaSerie = DataSet.Tables("NotaDebito").Rows(0)("NSeries")
        End If
    End Function

    Public Sub GrabaSeries(ByVal Numero As String, ByVal FechaSerie As Date, ByVal Tipo As String, ByVal Id As Double, ByVal CodigoProducto As String, ByVal NumeroSerie As String)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, Fecha As String
        Dim SqlString As String, MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter

        Fecha = Format(FechaSerie, "yyyy-MM-dd")

        SqlString = "SELECT  * FROM Detalle_ComprasSeries WHERE (Numero_Compra = '" & Numero & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & Tipo & "') AND (Id_Series = " & Id & ") AND (Cod_Producto = '" & CodigoProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "NotaDebito")
        If DataSet.Tables("NotaDebito").Rows.Count = 0 Then

            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "INSERT INTO [Detalle_ComprasSeries] ([Id_Series],[Numero_Compra],[Fecha_Compra],[Tipo_Compra],[Cod_Producto],[NSeries]) " & _
                         "VALUES (" & Id & ",'" & Numero & "','" & Format(FechaSerie, "dd/MM/yyyy") & "','" & Tipo & "','" & CodigoProducto & "','" & NumeroSerie & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "UPDATE [Detalle_ComprasSeries] SET [NSeries] = '" & NumeroSerie & "' WHERE (Numero_Compra = '" & Numero & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & Tipo & "') AND (Id_Series = " & Id & ") AND (Cod_Producto = '" & CodigoProducto & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If
    End Sub
    Public Sub GrabaDetalleLotes(ByVal NumeroDocumento As String, ByVal FechaDocumento As Date, ByVal TipoDocumento As String, ByVal Id As Double, ByVal CodigoProducto As String, ByVal Cantidad As String, ByVal NombreLote As String, ByVal FechaVence As Date)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, Fecha As String
        Dim SqlString As String, MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter

        Fecha = Format(FechaDocumento, "yyyy-MM-dd")

        SqlString = "SELECT  * FROM Detalle_Lote WHERE (Numero_Documento = '" & NumeroDocumento & "') AND (Fecha = CONVERT(DATETIME, '" & Format(FechaDocumento, "yyyy-MM-dd") & "', 102)) AND (Tipo_Documento = '" & TipoDocumento & "') AND (id_Detalle_Lote = " & Id & ") AND (Codigo_Producto = '" & CodigoProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "NotaDebito")
        If DataSet.Tables("NotaDebito").Rows.Count = 0 Then

            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "INSERT INTO [Detalle_Lote] ([Numero_Lote],[Fecha],[Tipo_Documento],[Numero_Documento],[FechaVence],[Codigo_Producto] ,[Cantidad]) " & _
                         "VALUES ('" & NombreLote & "' ,'" & Format(FechaDocumento, "dd/MM/yyyy") & "','" & TipoDocumento & "' ,'" & NumeroDocumento & "' ,'" & Format(FechaVence, "dd/MM/yyyy") & "','" & CodigoProducto & "','" & Cantidad & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "UPDATE [Detalle_Lote] SET [Numero_Lote] = '" & NombreLote & "',[FechaVence] = '" & Format(FechaVence, "dd/MM/yyyy") & "',[Cantidad] = '" & Cantidad & "' WHERE (Numero_Documento = '" & NumeroDocumento & "') AND (Fecha = CONVERT(DATETIME, '" & Format(FechaDocumento, "yyyy-MM-dd") & "', 102)) AND (Tipo_Documento = '" & TipoDocumento & "') AND (id_Detalle_Lote = " & Id & ") AND (Codigo_Producto = '" & CodigoProducto & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If
    End Sub

    Public Sub LimpiaEnsamble()
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim SqlString As String, CodComponente As Double


        CodComponente = -10000


        FrmEnsamble.TxtCodigo.Text = ""
        FrmEnsamble.TxtDescripcion.Text = ""
        FrmEnsamble.TxtCantidad.Text = 1
        FrmEnsamble.TxtNumeroEnsamble.Text = ""
        FrmEnsamble.TxtNumero.Text = ""


        '//////////////////////////////////////////////////////////////////////////////////////////////
        '///////////////BUSCO LOS COMPONENTES DEL PRODUCTO/////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////

        SqlString = "SELECT *  FROM Componentes WHERE (Cod_Componente = '" & CodComponente & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Componentes")
        FrmEnsamble.BindingComponentes.DataSource = DataSet.Tables("Componentes")
        FrmEnsamble.TrueDBGridComponentes.DataSource = FrmEnsamble.BindingComponentes

        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Visible = False
        FrmEnsamble.TrueDBGridComponentes.Columns(1).Caption = "Componente"
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Width = 74
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Locked = True
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Width = 250
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Locked = True
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Width = 62
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Locked = True
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Width = 68
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Visible = False
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Locked = True
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Width = 74
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Locked = True
    End Sub
    Public Sub ActulizacionFacturaEnsamble(ByVal Numero As String, ByVal Tipo As String, ByVal Fecha As Date, ByVal Excento As Boolean)
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim SqlString As String = "", i As Double = 0, Cantidad As Double, PrecioUnitario As Double, Importe As Double
        Dim DataSet As New DataSet, Iva As Double, CodIva As String, CodProducto As String
        Dim DataAdapter As New SqlClient.SqlDataAdapter, TotalImporte As Double, Tasa As Double, TasaIva As Double
        Dim StrSQLUpdate As String, ComandoUpdate As New SqlClient.SqlCommand
        Dim iResultado As Integer, Exonerado As Double = 0


        SqlString = "SELECT   *  FROM Detalle_Facturas WHERE (Numero_Factura = '" & Numero & "') AND (Tipo_Factura = '" & Tipo & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Format(Fecha, "yyyy-MM-dd") & "', 102))"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Facturas")

        '//////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////BUSCO LA EXISTENCIA DE ESTE PRODUCTO PARA CADA BODEGA//////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////

        i = 0
        TotalImporte = 0
        Do While i < (DataSet.Tables("Facturas").Rows.Count)
            CodProducto = DataSet.Tables("Facturas").Rows(i)("Cod_Producto")
            Cantidad = DataSet.Tables("Facturas").Rows(i)("Cantidad")
            PrecioUnitario = DataSet.Tables("Facturas").Rows(i)("Precio_Unitario")
            Importe = Cantidad * PrecioUnitario
            TotalImporte = TotalImporte + Importe

            SqlString = "SELECT Productos.*  FROM Productos WHERE (Cod_Productos = '" & CodProducto & "') "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.Fill(DataSet, "Productos")
            If Not DataSet.Tables("Productos").Rows.Count = 0 Then
                CodIva = DataSet.Tables("Productos").Rows(0)("Cod_Iva")
                SqlString = "SELECT *  FROM Impuestos WHERE  (Cod_Iva = '" & CodIva & "')"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "IVA")
                If Not DataSet.Tables("IVA").Rows.Count = 0 Then
                    Tasa = DataSet.Tables("IVA").Rows(0)("Impuesto")
                    TasaIva = DataSet.Tables("IVA").Rows(0)("Impuesto")
                End If
                DataSet.Tables("IVA").Reset()
            End If
            DataSet.Tables("Productos").Reset()


            Iva = Format(Iva + Importe * Tasa, "####0.00000000")


            i = i + 1
        Loop

        If Excento = True Then
            Iva = 0
            Exonerado = 1
        Else
            Exonerado = 0
        End If


        '///////////SIsEXISTE EL USUARIO LO ACTUALIZO////////////////
        StrSQLUpdate = "UPDATE [Facturas]  SET [SubTotal] = " & TotalImporte & " ,[IVA] = " & Iva & " ,[Pagado] = 0,[NetoPagar] = " & TotalImporte + Iva & ",[MontoCredito] = 0,[Exonerado] = " & Exonerado & "  WHERE (Numero_Factura = '" & Numero & "') AND (Tipo_Factura = '" & Tipo & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Format(Fecha, "yyyy-MM-dd") & "', 102))"
        MiConexion.Open()
        ComandoUpdate = New SqlClient.SqlCommand(StrSQLUpdate, MiConexion)
        iResultado = ComandoUpdate.ExecuteNonQuery
        MiConexion.Close()


    End Sub

    Public Sub ActulizacionFactura(ByVal Numero As String, ByVal Tipo As String, ByVal Fecha As Date, ByVal Excento As Boolean)
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim SqlString As String = "", i As Double = 0, Cantidad As Double, PrecioUnitario As Double, Importe As Double
        Dim DataSet As New DataSet, Iva As Double, CodIva As String, CodProducto As String
        Dim DataAdapter As New SqlClient.SqlDataAdapter, TotalImporte As Double, Tasa As Double, TasaIva As Double
        Dim StrSQLUpdate As String, ComandoUpdate As New SqlClient.SqlCommand
        Dim iResultado As Integer, Exonerado As Double = 0


        SqlString = "SELECT   *  FROM Detalle_Facturas WHERE (Numero_Factura = '" & Numero & "') AND (Tipo_Factura = '" & Tipo & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Format(Fecha, "yyyy-MM-dd") & "', 102))"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Facturas")

        '//////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////BUSCO LA EXISTENCIA DE ESTE PRODUCTO PARA CADA BODEGA//////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////

        i = 0
        TotalImporte = 0
        Do While i < (DataSet.Tables("Facturas").Rows.Count)
            CodProducto = DataSet.Tables("Facturas").Rows(i)("Cod_Producto")
            Cantidad = DataSet.Tables("Facturas").Rows(i)("Cantidad")
            PrecioUnitario = DataSet.Tables("Facturas").Rows(i)("Precio_Unitario")
            Importe = Cantidad * PrecioUnitario
            TotalImporte = TotalImporte + Importe

            SqlString = "SELECT Productos.*  FROM Productos WHERE (Cod_Productos = '" & CodProducto & "') "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.Fill(DataSet, "Productos")
            If Not DataSet.Tables("Productos").Rows.Count = 0 Then
                CodIva = DataSet.Tables("Productos").Rows(0)("Cod_Iva")
                SqlString = "SELECT *  FROM Impuestos WHERE  (Cod_Iva = '" & CodIva & "')"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "IVA")
                If Not DataSet.Tables("IVA").Rows.Count = 0 Then
                    Tasa = DataSet.Tables("IVA").Rows(0)("Impuesto")
                    TasaIva = DataSet.Tables("IVA").Rows(0)("Impuesto")
                End If
                DataSet.Tables("IVA").Reset()
            End If
            DataSet.Tables("Productos").Reset()


            Iva = Format(Iva + Importe * Tasa, "####0.00000000")


            i = i + 1
        Loop

        If Excento = True Then
            Iva = 0
            Exonerado = 1
        Else
            Exonerado = 0
        End If


        '///////////SIsEXISTE EL USUARIO LO ACTUALIZO////////////////
        StrSQLUpdate = "UPDATE [Facturas]  SET [SubTotal] = " & TotalImporte & " ,[IVA] = " & Iva & " ,[Pagado] = " & TotalImporte + Iva & ",[NetoPagar] = 0,[MontoCredito] = 0,[Exonerado] = " & Exonerado & "  WHERE (Numero_Factura = '" & Numero & "') AND (Tipo_Factura = '" & Tipo & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Format(Fecha, "yyyy-MM-dd") & "', 102))"
        MiConexion.Open()
        ComandoUpdate = New SqlClient.SqlCommand(StrSQLUpdate, MiConexion)
        iResultado = ComandoUpdate.ExecuteNonQuery
        MiConexion.Close()


    End Sub
    Public Sub ActulizacionCompraEnsamble(ByVal Numero As String, ByVal Tipo As String, ByVal Fecha As Date, ByVal Excento As Boolean)
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim SqlString As String = "", i As Double = 0, Cantidad As Double, PrecioUnitario As Double, Importe As Double
        Dim DataSet As New DataSet, Iva As Double, CodIva As String, CodProducto As String
        Dim DataAdapter As New SqlClient.SqlDataAdapter, TotalImporte As Double, Tasa As Double, TasaIva As Double
        Dim StrSQLUpdate As String, ComandoUpdate As New SqlClient.SqlCommand
        Dim iResultado As Integer, Exonerado As Double



        SqlString = "SELECT  *  FROM Detalle_Compras WHERE  (Numero_Compra = '" & Numero & "') AND (Tipo_Compra = '" & Tipo & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Format(Fecha, "yyyy-MM-dd") & "', 102))"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")

        '//////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////BUSCO LA EXISTENCIA DE ESTE PRODUCTO PARA CADA BODEGA//////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////

        i = 0
        TotalImporte = 0
        Do While i < (DataSet.Tables("Compras").Rows.Count)
            CodProducto = DataSet.Tables("Compras").Rows(i)("Cod_Producto")
            Cantidad = DataSet.Tables("Compras").Rows(i)("Cantidad")
            PrecioUnitario = DataSet.Tables("Compras").Rows(i)("Precio_Unitario")
            Importe = Cantidad * PrecioUnitario
            TotalImporte = TotalImporte + Importe

            SqlString = "SELECT Productos.*  FROM Productos WHERE (Cod_Productos = '" & CodProducto & "') "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.Fill(DataSet, "Productos")
            If Not DataSet.Tables("Productos").Rows.Count = 0 Then
                CodIva = DataSet.Tables("Productos").Rows(0)("Cod_Iva")
                SqlString = "SELECT *  FROM Impuestos WHERE  (Cod_Iva = '" & CodIva & "')"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "IVA")
                If Not DataSet.Tables("IVA").Rows.Count = 0 Then
                    Tasa = DataSet.Tables("IVA").Rows(0)("Impuesto")
                    TasaIva = DataSet.Tables("IVA").Rows(0)("Impuesto")
                End If
                DataSet.Tables("IVA").Reset()
            End If
            DataSet.Tables("Productos").Reset()

            Iva = Format(Iva + Importe * Tasa, "####0.00000000")
            i = i + 1
        Loop

        If Excento = True Then
            Iva = 0
            Exonerado = 1
        Else
            Exonerado = 0
        End If

        '///////////SIsEXISTE EL USUARIO LO ACTUALIZO////////////////
        StrSQLUpdate = "UPDATE [Compras]  SET [SubTotal] = " & TotalImporte & " ,[IVA] = " & Iva & " ,[Pagado] = 0,[NetoPagar] = " & TotalImporte + Iva & ",[MontoCredito] = 0,[Exonerado] = " & Exonerado & "  WHERE (Numero_Compra = '" & Numero & "') AND (Tipo_Compra = '" & Tipo & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Format(Fecha, "yyyy-MM-dd") & "', 102))"
        MiConexion.Open()
        ComandoUpdate = New SqlClient.SqlCommand(StrSQLUpdate, MiConexion)
        iResultado = ComandoUpdate.ExecuteNonQuery
        MiConexion.Close()


    End Sub


    Public Sub ActulizacionCompra(ByVal Numero As String, ByVal Tipo As String, ByVal Fecha As Date, ByVal Excento As Boolean)
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim SqlString As String = "", i As Double = 0, Cantidad As Double, PrecioUnitario As Double, Importe As Double
        Dim DataSet As New DataSet, Iva As Double, CodIva As String, CodProducto As String
        Dim DataAdapter As New SqlClient.SqlDataAdapter, TotalImporte As Double, Tasa As Double, TasaIva As Double
        Dim StrSQLUpdate As String, ComandoUpdate As New SqlClient.SqlCommand
        Dim iResultado As Integer, Exonerado As Double



        SqlString = "SELECT  *  FROM Detalle_Compras WHERE  (Numero_Compra = '" & Numero & "') AND (Tipo_Compra = '" & Tipo & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Format(Fecha, "yyyy-MM-dd") & "', 102))"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")

        '//////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////BUSCO LA EXISTENCIA DE ESTE PRODUCTO PARA CADA BODEGA//////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////

        i = 0
        TotalImporte = 0
        Do While i < (DataSet.Tables("Compras").Rows.Count)
            CodProducto = DataSet.Tables("Compras").Rows(i)("Cod_Producto")
            Cantidad = DataSet.Tables("Compras").Rows(i)("Cantidad")
            PrecioUnitario = DataSet.Tables("Compras").Rows(i)("Precio_Unitario")
            Importe = Cantidad * PrecioUnitario
            TotalImporte = TotalImporte + Importe

            SqlString = "SELECT Productos.*  FROM Productos WHERE (Cod_Productos = '" & CodProducto & "') "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.Fill(DataSet, "Productos")
            If Not DataSet.Tables("Productos").Rows.Count = 0 Then
                CodIva = DataSet.Tables("Productos").Rows(0)("Cod_Iva")
                SqlString = "SELECT *  FROM Impuestos WHERE  (Cod_Iva = '" & CodIva & "')"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "IVA")
                If Not DataSet.Tables("IVA").Rows.Count = 0 Then
                    Tasa = DataSet.Tables("IVA").Rows(0)("Impuesto")
                    TasaIva = DataSet.Tables("IVA").Rows(0)("Impuesto")
                End If
                DataSet.Tables("IVA").Reset()
            End If
            DataSet.Tables("Productos").Reset()

            Iva = Format(Iva + Importe * Tasa, "####0.00000000")
            i = i + 1
        Loop

        If Excento = True Then
            Iva = 0
            Exonerado = 1
        Else
            Exonerado = 0
        End If

        '///////////SIsEXISTE EL USUARIO LO ACTUALIZO////////////////
        StrSQLUpdate = "UPDATE [Compras]  SET [SubTotal] = " & TotalImporte & " ,[IVA] = " & Iva & " ,[Pagado] = " & TotalImporte + Iva & ",[NetoPagar] = 0,[MontoCredito] = 0,[Exonerado] = " & Exonerado & "  WHERE (Numero_Compra = '" & Numero & "') AND (Tipo_Compra = '" & Tipo & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Format(Fecha, "yyyy-MM-dd") & "', 102))"
        MiConexion.Open()
        ComandoUpdate = New SqlClient.SqlCommand(StrSQLUpdate, MiConexion)
        iResultado = ComandoUpdate.ExecuteNonQuery
        MiConexion.Close()


    End Sub

    Public Function UltimoPrecioCompra(ByVal CodProducto As String) As Double
        Dim SqlString As String
        Dim DataSet As New DataSet
        Dim DataAdapter As New SqlClient.SqlDataAdapter
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim Registros As Double
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////BUSCO EL ULTIMO PRECIO DE COMPRA /////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SqlString = "SELECT Cod_Producto, Cantidad, Precio_Unitario, Importe FROM Detalle_Compras WHERE (Cod_Producto = '" & CodProducto & "')"
        SqlString = "SELECT  Detalle_Compras.Cod_Producto, Detalle_Compras.Cantidad, Detalle_Compras.Precio_Unitario, Detalle_Compras.Importe, Compras.MonedaCompra FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra WHERE (Detalle_Compras.Cod_Producto = '" & CodProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Productos")
        Registros = DataSet.Tables("Productos").Rows.Count
        StringMoneda = ""

        If Registros = 0 Then
            'UltimoPrecioCompra = DataSet.Tables("Productos").Rows(0)("Costo_Promedio")
        Else
            If Not IsDBNull(DataSet.Tables("Productos").Rows(Registros - 1)("Precio_Unitario")) Then
                UltimoPrecioCompra = DataSet.Tables("Productos").Rows(Registros - 1)("Precio_Unitario")
                StringMoneda = DataSet.Tables("Productos").Rows(Registros - 1)("MonedaCompra")
            Else
                UltimoPrecioCompra = 0
            End If
        End If
    End Function

    Public Function UltimoPrecioVenta(ByVal CodProducto As String) As Double
        Dim SqlString As String
        Dim DataSet As New DataSet
        Dim DataAdapter As New SqlClient.SqlDataAdapter
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim Registros As Double
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////BUSCO EL ULTIMO PRECIO DE COMPRA /////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlString = "SELECT Numero_Factura, Cod_Producto, Descripcion_Producto, Cantidad, Precio_Unitario, Descuento, Precio_Neto, Importe FROM  Detalle_Facturas WHERE (Cod_Producto ='" & CodProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Productos")
        Registros = DataSet.Tables("Productos").Rows.Count

        If Registros = 0 Then
            'UltimoPrecioVenta = DataSet.Tables("Productos").Rows(0)("Costo_Promedio")
        Else
            If Not IsDBNull(DataSet.Tables("Productos").Rows(Registros - 1)("Precio_Unitario")) Then
                UltimoPrecioVenta = DataSet.Tables("Productos").Rows(Registros - 1)("Precio_Unitario")
            Else
                UltimoPrecioVenta = 0
            End If
        End If
    End Function

    Public Function ActualizaCostoPromedio(ByVal CodigoProducto As String) As Double
        Dim PrecioCosto As Double, Existencia As Double, CodigoBodega As String
        Dim DataSet As New DataSet, SQLProductos As String, i As Double
        Dim DataAdapter As New SqlClient.SqlDataAdapter
        Dim StrSQLUpdate As String, ComandoUpdate As New SqlClient.SqlCommand
        Dim iResultado As Integer, SqlString As String, PrecioCostoDolar As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)

        PrecioCosto = CostoPromedio(CodigoProducto)
        PrecioCostoDolar = CostoPromedioDolar
        '//////////////////////////////////////////////////////////////////////////////////////////////
        '///////////////BUSCO EL PRODUCTO/////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////

        SQLProductos = "SELECT Productos.*  FROM Productos WHERE (Cod_Productos = '" & CodigoProducto & "') "
        DataAdapter = New SqlClient.SqlDataAdapter(SQLProductos, MiConexion)
        DataAdapter.Fill(DataSet, "Proveedores")
        If Not DataSet.Tables("Proveedores").Rows.Count = 0 Then
            '///////////SIsEXISTE EL USUARIO LO ACTUALIZO////////////////
            StrSQLUpdate = "UPDATE [Productos] SET [Costo_Promedio] = '" & Math.Abs(PrecioCosto) & "',[Costo_Promedio_Dolar] = '" & Math.Abs(PrecioCostoDolar) & "' WHERE (Cod_Productos = '" & CodigoProducto & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(StrSQLUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If

        '*******************************************************************************************
        '/////////////////////////ACTUALIZADO POR BODEGA //////////////////////////////////////////
        '*******************************************************************************************
        SqlString = "SELECT  DetalleBodegas.Cod_Bodegas, Bodegas.Nombre_Bodega,DetalleBodegas.Existencia FROM DetalleBodegas INNER JOIN Bodegas ON DetalleBodegas.Cod_Bodegas = Bodegas.Cod_Bodega  " & _
                    "WHERE (DetalleBodegas.Cod_Productos = '" & CodigoProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Bodegas")

        '//////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////BUSCO LA EXISTENCIA DE ESTE PRODUCTO PARA CADA BODEGA//////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////
        Existencia = 0
        i = 0
        Do While i < (DataSet.Tables("Bodegas").Rows.Count)
            My.Application.DoEvents()
            CodigoBodega = DataSet.Tables("Bodegas").Rows(i)("Cod_Bodegas")

            'SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, MAX(Detalle_Compras.Fecha_Compra) AS Fecha_Compra, Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra GROUP BY Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, Compras.MonedaCompra HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida')"
            'DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
            'DataAdapter.Fill(DataSet, "Compras")
            'Registros = DataSet.Tables("Compras").Rows.Count - 1
            'Iposicion = 0
            'TotalImporte = 0
            'Iposicion = 0
            'Cantidad = 0
            'Do While Iposicion < DataSet.Tables("Compras").Rows.Count
            '    MonedaCompra = DataSet.Tables("Compras").Rows(Iposicion)("MonedaCompra")
            '    If MonedaCompra = "Cordobas" Then
            '        PrecioUnitario = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Precio_Unitario"))
            '        Cantidad = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Cantidad")) + Cantidad
            '        Importe = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Importe"))
            '        TotalImporte = TotalImporte + Importe
            '        FechaCompra = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Fecha_Compra"))
            '        TasaCambio = BuscaTasaCambio(FechaCompra)
            '        If TasaCambio = 0 Then
            '            MsgBox("TASA DE CAMBIO CERO,", MsgBoxStyle.Critical, "Zeus Facturacion ")

            '        Else
            '            PrecioCostoDolar = (PrecioCosto / TasaCambio)
            '        End If
            '    Else
            '        PrecioUnitario = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Precio_Unitario"))
            '        Cantidad = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Cantidad")) + Cantidad
            '        Importe = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Importe"))
            '        FechaCompra = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Fecha_Compra"))
            '        TasaCambio = BuscaTasaCambio(FechaCompra)
            '        If TasaCambio = 0 Then
            '            MsgBox("TASA DE CAMBIO CERO,", MsgBoxStyle.Critical, "Zeus Facturacion ")
            '        Else
            '            TotalImporte = (Importe * TasaCambio) + TotalImporte
            '        End If

            '    End If

            '    If Cantidad <> 0 Then
            '        PrecioCosto = TotalImporte / Cantidad
            '        PrecioCostoDolar = (PrecioCosto / TasaCambio)
            '    Else
            '        PrecioCosto = PrecioUnitario
            '        If PrecioCosto <> 0 Then
            '            PrecioCostoDolar = (PrecioCosto / TasaCambio)
            '        Else
            '            PrecioCostoDolar = 0
            '        End If
            '    End If
            'PrecioCosto = Format(TotalImporte / Cantidad, "##,##0.00")
            'PrecioCostoDolar = Format((PrecioCosto / TasaCambio), "##,##0.00")

            'DataSet.Tables("Compras").Reset()

            PrecioCosto = CostoPromedioBodega(CodigoProducto, CodigoBodega)
            PrecioCostoDolar = CostoPromedioDolar

            '///////////ACTUALIZO LA EXISTENCIA PARA CADA BODEGA ////////////////////////////////////////
            StrSQLUpdate = "UPDATE [DetalleBodegas]  SET [Costo] = '" & Math.Abs(PrecioCosto) & "',[CostoDolar] = '" & Math.Abs(PrecioCostoDolar) & "'  WHERE (Cod_Productos = '" & CodigoProducto & "') AND (Cod_Bodegas = '" & CodigoBodega & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(StrSQLUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

            '    Iposicion = Iposicion + 1

            'Loop
            i = i + 1

        Loop
    End Function


    Public Function CostoPromedio(ByVal CodigoProducto As String) As Double
        Dim SqlCompras As String, Iposicion As Double, TotalImporte As Double, CantidadCompra As Double, MonedaCompra As String, Importe As Double, PrecioUnitario As Double, PrecioCostoDolar As Double, PrecioCosto As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), Registros As Double, ImporteD As Double, TotalImporteD As Double
        Dim DataSet As New DataSet, TotalCantidad As Double = 0, CantDevCompra As Double = 0, CantDevVenta As Double = 0, CantVentas As Double = 0, CantSalida As Double = 0
        Dim DataAdapter As New SqlClient.SqlDataAdapter
        Dim TotalCompras As Double = 0, TotalDevCompras As Double = 0, TotalVentas As Double = 0, TotalDevVentas As Double = 0, TotalSalidaBodega As Double = 0, TotalSalidaBodegaD As Double = 0
        Dim TotalComprasD As Double = 0, TotalDevComprasD As Double = 0, TotalVentasD As Double = 0, TotalDevVentasD As Double = 0
        Dim TotalTransEnviada As Double = 0, TotalTransRecibida As Double = 0



        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE COMPRAS DOLARES ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario * TasaCambio.MontoTasa) AS Precio_Unitario, SUM(Detalle_Compras.Descuento * TasaCambio.MontoTasa) AS Descuento, SUM(Detalle_Compras.Descuento) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto * TasaCambio.MontoTasa) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad * TasaCambio.MontoTasa) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS ImporteD, Compras.Cod_Bodega FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                     "WHERE (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.MonedaCompra = N'Dolares')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "ComprasD")
        Registros = DataSet.Tables("ComprasD").Rows.Count - 1
        Iposicion = 0
        TotalImporte = 0
        ImporteD = 0
        TotalImporteD = 0
        CantidadCompra = 0
        TotalCantidad = 0

        Do While Iposicion < DataSet.Tables("ComprasD").Rows.Count
            MonedaCompra = DataSet.Tables("ComprasD").Rows(Iposicion)("MonedaCompra")
            PrecioUnitario = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("Precio_Neto"))
            CantidadCompra = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("Cantidad")) + CantidadCompra
            Importe = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("ImporteD"))
            TotalCompras = TotalCompras + Importe
            TotalComprasD = TotalComprasD + ImporteD
            Iposicion = Iposicion + 1
        Loop



        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE COMPRAS CORDOBAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SqlCompras = "SELECT Detalle_Compras.Numero_Compra, Detalle_Compras.Fecha_Compra, Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, Detalle_Compras.Cantidad, Detalle_Compras.Precio_Unitario, Detalle_Compras.Descuento, Detalle_Compras.Precio_Neto, Compras.MonedaCompra, Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad AS Importe, Compras.Cod_Bodega FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') AND (Compras.Cod_Bodega = '" & CodBodega & "')"
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario / TasaCambio.MontoTasa) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Descuento / TasaCambio.MontoTasa) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad / TasaCambio.MontoTasa) AS ImporteD, Compras.Cod_Bodega FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.MonedaCompra <> N'Dolares')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        Registros = DataSet.Tables("Compras").Rows.Count - 1
        Iposicion = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Compras").Rows.Count
            MonedaCompra = DataSet.Tables("Compras").Rows(Iposicion)("MonedaCompra")
            PrecioUnitario = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Precio_Neto"))
            CantidadCompra = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Cantidad")) + CantidadCompra
            Importe = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("Compras").Rows(Iposicion)("ImporteD"))
            TotalCompras = TotalCompras + Importe
            TotalComprasD = TotalComprasD + ImporteD
            'FechaCompra = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Fecha_Compra"))
            Iposicion = Iposicion + 1
        Loop




        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DEVOLUCIONES COMPRA DOLARES////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, MAX(Detalle_Compras.Fecha_Compra) AS Fecha_Compra, Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Unitario * Detalle_Compras.Cantidad) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra GROUP BY Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, Compras.MonedaCompra HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra')"
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario * TasaCambio.MontoTasa) AS Precio_Unitario, SUM(Detalle_Compras.Descuento * TasaCambio.MontoTasa) AS Descuento, SUM(Detalle_Compras.Descuento) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto * TasaCambio.MontoTasa) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad * TasaCambio.MontoTasa) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS ImporteD, Compras.Cod_Bodega FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                     "WHERE (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.MonedaCompra = N'Dolares')"

        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionD")
        Registros = DataSet.Tables("DevolucionD").Rows.Count - 1
        Iposicion = 0
        CantDevCompra = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("DevolucionD").Rows.Count
            MonedaCompra = DataSet.Tables("DevolucionD").Rows(Iposicion)("MonedaCompra")
            PrecioUnitario = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("Precio_Neto"))
            CantDevCompra = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("Cantidad")) + CantDevCompra
            Importe = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("ImporteD"))
            TotalDevCompras = TotalDevCompras + Importe
            TotalDevComprasD = TotalDevComprasD + ImporteD

            Iposicion = Iposicion + 1

        Loop


        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE DEVOLUCION COMPRAS CORDOBAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario / TasaCambio.MontoTasa) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Descuento / TasaCambio.MontoTasa) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad / TasaCambio.MontoTasa) AS ImporteD, Compras.Cod_Bodega FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.MonedaCompra <> 'Dolares')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Devolucion")
        Registros = DataSet.Tables("Devolucion").Rows.Count - 1
        Iposicion = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Devolucion").Rows.Count
            MonedaCompra = DataSet.Tables("Devolucion").Rows(Iposicion)("MonedaCompra")

            PrecioUnitario = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("Precio_Neto"))
            CantDevCompra = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("Cantidad")) + CantDevCompra
            Importe = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("ImporteD"))
            TotalDevCompras = TotalDevCompras + Importe
            TotalDevComprasD = TotalDevComprasD + ImporteD
            'FechaCompra = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Fecha_Compra"))


            Iposicion = Iposicion + 1

        Loop

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE SALIDA DE BODEGA ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Costo,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa WHERE (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Salida Bodega') AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario <> 0)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Salida")
        Registros = DataSet.Tables("Salida").Rows.Count - 1
        Iposicion = 0
        CantSalida = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Salida").Rows.Count
            If Not IsDBNull(DataSet.Tables("Salida").Rows(Iposicion)("Cantidad")) Then
                CantSalida = Trim(DataSet.Tables("Salida").Rows(Iposicion)("Cantidad"))
                If Not IsDBNull(DataSet.Tables("Salida").Rows(Iposicion)("Costo")) Then
                    Importe = Trim(DataSet.Tables("Salida").Rows(Iposicion)("Costo"))
                    ImporteD = Trim(DataSet.Tables("Salida").Rows(Iposicion)("CostoD"))
                End If
            End If
            TotalSalidaBodega = TotalSalidaBodega + Importe
            TotalSalidaBodegaD = TotalSalidaBodegaD + ImporteD
            Iposicion = Iposicion + 1
        Loop

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE VENTAS EN CORDOBAS Y DOLARES ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) AS Costo,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa WHERE (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Factura') AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario <> 0)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Ventas")
        Registros = DataSet.Tables("Ventas").Rows.Count - 1
        Iposicion = 0
        CantVentas = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Ventas").Rows.Count
            If Not IsDBNull(DataSet.Tables("Ventas").Rows(Iposicion)("Cantidad")) Then
                CantVentas = Trim(DataSet.Tables("Ventas").Rows(Iposicion)("Cantidad"))
            End If
            If Not IsDBNull(DataSet.Tables("Ventas").Rows(Iposicion)("Costo")) Then
                Importe = Trim(DataSet.Tables("Ventas").Rows(Iposicion)("Costo"))
                ImporteD = Trim(DataSet.Tables("Ventas").Rows(Iposicion)("CostoD"))
            End If
            TotalVentas = TotalVentas + Importe
            TotalVentasD = TotalVentasD + ImporteD
            Iposicion = Iposicion + 1
        Loop

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE DEVOLUCIONES VENTAS EN CORDOBAS Y DOLARES ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) AS Costo,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa WHERE (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Devolucion de Venta') AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario <> 0)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "DevVentas")
        Registros = DataSet.Tables("DevVentas").Rows.Count - 1
        Iposicion = 0
        CantDevVenta = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("DevVentas").Rows.Count
            If Not IsDBNull(DataSet.Tables("DevVentas").Rows(Iposicion)("Cantidad")) Then
                CantDevVenta = Trim(DataSet.Tables("DevVentas").Rows(Iposicion)("Cantidad"))
                Importe = Trim(DataSet.Tables("DevVentas").Rows(Iposicion)("Costo"))
                ImporteD = Trim(DataSet.Tables("DevVentas").Rows(Iposicion)("CostoD"))
            End If

            TotalDevVentas = TotalDevVentas + Importe
            TotalDevVentasD = TotalDevVentasD + ImporteD
            Iposicion = Iposicion + 1
        Loop





        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////FORMULA COSTO PROMEDIO /////////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        'CostoPromedio =  TotalCompras+TotalDevVenas-TotalVentas-TotalDevCompra
        '                 ------------------------------------------------------
        '                 UnidCompra+UnidadDevVentas-UnidadVentas-UnidadDevCompra


        TotalCantidad = CantidadCompra + CantDevVenta - CantVentas - CantDevCompra - CantSalida
        TotalImporte = TotalCompras + TotalDevVentas - TotalVentas - TotalDevCompras - TotalSalidaBodega
        TotalImporteD = TotalComprasD + TotalDevVentasD - TotalVentasD - TotalDevComprasD - TotalSalidaBodegaD


        If TotalCantidad <> 0 Then
            PrecioCosto = Format(TotalImporte / TotalCantidad, "##,##0.000000")
            PrecioCostoDolar = Format((TotalImporteD / TotalCantidad), "##,##0.000000")
        Else
            'PrecioCosto = PrecioUnitario
            'If PrecioCosto <> 0 Then
            '    PrecioCostoDolar = Format((TotalImporteD / TotalCantidad), "##,##0.000000")
            'Else
            '    PrecioCostoDolar = 0
            'End If
            PrecioCosto = 0
            PrecioCostoDolar = 0

        End If

        CostoPromedio = PrecioCosto
        CostoPromedioDolar = PrecioCostoDolar

        DataSet.Tables("Compras").Reset()
    End Function
    Public Function CostoPromedioBodega(ByVal CodigoProducto As String, ByVal CodBodega As String) As Double
        Dim SqlCompras As String, Iposicion As Double, TotalImporte As Double, CantidadCompras As Double, MonedaCompra As String, Importe As Double, PrecioUnitario As Double, PrecioCostoDolar As Double, PrecioCosto As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), Registros As Double, ImporteD As Double, TotalImporteD As Double
        Dim DataSet As New DataSet, TotalCantidad As Double = 0
        Dim DataAdapter As New SqlClient.SqlDataAdapter
        Dim TotalCompras As Double = 0, TotalDevCompras As Double = 0, TotalVentas As Double = 0, TotalDevVentas As Double = 0, TotalSalidaBodega As Double = 0, TotalSalidaBodegaD As Double
        Dim TotalComprasD As Double = 0, TotalDevComprasD As Double = 0, TotalVentasD As Double = 0, TotalDevVentasD As Double = 0, TotalTransEnviada As Double = 0, TotalTransRecibida As Double = 0, TotalTransEnviadaD As Double = 0, TotalTransRecibidaD As Double = 0
        Dim CantDevCompra As Double = 0, CantDevVenta As Double = 0, CantVentas As Double = 0, CantTransEnviada As Double = 0, CantTransRecibida As Double = 0, CantSalida As Double = 0


        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE COMPRAS DOLARES ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario * TasaCambio.MontoTasa) AS Precio_Unitario, SUM(Detalle_Compras.Descuento * TasaCambio.MontoTasa) AS Descuento, SUM(Detalle_Compras.Descuento) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto * TasaCambio.MontoTasa) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad * TasaCambio.MontoTasa) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS ImporteD, Compras.Cod_Bodega FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                     "WHERE (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodBodega & "') AND (Compras.MonedaCompra = 'Dolares')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "ComprasD")
        Registros = DataSet.Tables("ComprasD").Rows.Count - 1
        Iposicion = 0
        TotalCompras = 0
        ImporteD = 0
        TotalComprasD = 0
        CantidadCompras = 0
        TotalCantidad = 0

        Do While Iposicion < DataSet.Tables("ComprasD").Rows.Count
            MonedaCompra = DataSet.Tables("ComprasD").Rows(Iposicion)("MonedaCompra")
            PrecioUnitario = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("Precio_Neto"))
            CantidadCompras = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("Cantidad")) + CantidadCompras
            Importe = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("ImporteD"))
            TotalCompras = TotalCompras + Importe
            TotalComprasD = TotalComprasD + ImporteD
            Iposicion = Iposicion + 1
        Loop

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE COMPRAS CORDOBAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario / TasaCambio.MontoTasa) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Descuento / TasaCambio.MontoTasa) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad / TasaCambio.MontoTasa) AS ImporteD, Compras.Cod_Bodega FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodBodega & "') AND (Compras.MonedaCompra <> N'Dolares')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        Registros = DataSet.Tables("Compras").Rows.Count - 1
        Iposicion = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Compras").Rows.Count
            MonedaCompra = DataSet.Tables("Compras").Rows(Iposicion)("MonedaCompra")
            PrecioUnitario = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Precio_Neto"))
            CantidadCompras = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Cantidad")) + CantidadCompras
            Importe = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("Compras").Rows(Iposicion)("ImporteD"))
            TotalCompras = TotalCompras + Importe
            TotalComprasD = TotalComprasD + ImporteD
            Iposicion = Iposicion + 1

        Loop

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DEVOLUCIONES COMPRAS DOLARES////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, MAX(Detalle_Compras.Fecha_Compra) AS Fecha_Compra, Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Unitario * Detalle_Compras.Cantidad) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra GROUP BY Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, Compras.MonedaCompra HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra')"
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario * TasaCambio.MontoTasa) AS Precio_Unitario, SUM(Detalle_Compras.Descuento * TasaCambio.MontoTasa) AS Descuento, SUM(Detalle_Compras.Descuento) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto * TasaCambio.MontoTasa) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad * TasaCambio.MontoTasa) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS ImporteD, Compras.Cod_Bodega FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                     "WHERE (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodBodega & "') AND (Compras.MonedaCompra = N'Dolares')"

        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionD")
        Registros = DataSet.Tables("DevolucionD").Rows.Count - 1
        Iposicion = 0
        CantDevCompra = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("DevolucionD").Rows.Count
            MonedaCompra = DataSet.Tables("DevolucionD").Rows(Iposicion)("MonedaCompra")
            PrecioUnitario = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("Precio_Neto"))
            CantDevCompra = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("Cantidad")) + CantDevCompra
            Importe = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("ImporteD"))
            TotalDevCompras = TotalDevCompras + Importe
            TotalDevComprasD = TotalDevComprasD + ImporteD

            Iposicion = Iposicion + 1

        Loop


        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE DEVOLUCION COMPRAS CORDOBAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SqlCompras = "SELECT Detalle_Compras.Numero_Compra, Detalle_Compras.Fecha_Compra, Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, Detalle_Compras.Cantidad, Detalle_Compras.Precio_Unitario, Detalle_Compras.Descuento, Detalle_Compras.Precio_Neto, Compras.MonedaCompra, Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad AS Importe, Compras.Cod_Bodega FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') AND (Compras.Cod_Bodega = '" & CodBodega & "')"
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario / TasaCambio.MontoTasa) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Descuento / TasaCambio.MontoTasa) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad / TasaCambio.MontoTasa) AS ImporteD, Compras.Cod_Bodega FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodBodega & "') AND (Compras.MonedaCompra <> 'Dolares')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Devolucion")
        Registros = DataSet.Tables("Devolucion").Rows.Count - 1
        Iposicion = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Devolucion").Rows.Count
            MonedaCompra = DataSet.Tables("Devolucion").Rows(Iposicion)("MonedaCompra")
            PrecioUnitario = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("Precio_Neto"))
            CantDevCompra = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("Cantidad")) + CantDevCompra
            Importe = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("ImporteD"))
            TotalDevCompras = TotalDevCompras + Importe
            TotalDevComprasD = TotalDevComprasD + ImporteD
            Iposicion = Iposicion + 1
        Loop


        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////BUSCO EL TOTAL TRANSFERENCIAS RECIBIDAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT  MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario / TasaCambio.MontoTasa) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Descuento / TasaCambio.MontoTasa) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS Precio_NetoD, SUM(Detalle_Compras.Precio_Unitario * Detalle_Compras.Cantidad) AS Importe, SUM(Detalle_Compras.Precio_Unitario * Detalle_Compras.Cantidad / TasaCambio.MontoTasa) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                     "WHERE (Detalle_Compras.Tipo_Compra = 'Transferencia Recibida') AND (Compras.Cod_Bodega = '" & CodBodega & "') GROUP BY Detalle_Compras.Cod_Producto HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "TransRecibida")
        Registros = DataSet.Tables("TransRecibida").Rows.Count - 1
        Iposicion = 0
        CantTransRecibida = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("TransRecibida").Rows.Count
            If Not IsDBNull(DataSet.Tables("TransRecibida").Rows(Iposicion)("Cantidad")) Then
                CantTransRecibida = Trim(DataSet.Tables("TransRecibida").Rows(Iposicion)("Cantidad"))
                Importe = Trim(DataSet.Tables("TransRecibida").Rows(Iposicion)("Importe"))
                ImporteD = Trim(DataSet.Tables("TransRecibida").Rows(Iposicion)("ImporteD"))
            End If
            TotalTransRecibida = TotalTransRecibida + Importe
            TotalTransRecibidaD = TotalTransRecibidaD + ImporteD
            Iposicion = Iposicion + 1
        Loop


        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE VENTAS EN CORDOBAS Y DOLARES ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) AS Costo,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa WHERE (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Factura') AND (Facturas.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario <> 0)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Ventas")
        Registros = DataSet.Tables("Ventas").Rows.Count - 1
        Iposicion = 0
        CantVentas = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Ventas").Rows.Count
            If Not IsDBNull(DataSet.Tables("Ventas").Rows(Iposicion)("Cantidad")) Then
                CantVentas = Trim(DataSet.Tables("Ventas").Rows(Iposicion)("Cantidad"))
                Importe = Trim(DataSet.Tables("Ventas").Rows(Iposicion)("Costo"))
                ImporteD = Trim(DataSet.Tables("Ventas").Rows(Iposicion)("CostoD"))
            End If
            TotalVentas = TotalVentas + Importe
            TotalVentasD = TotalVentasD + ImporteD
            Iposicion = Iposicion + 1
        Loop

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE SALIDA DE BODEGAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Costo,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa WHERE (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Salida Bodega') AND (Facturas.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario <> 0)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Salida")
        Registros = DataSet.Tables("Salida").Rows.Count - 1
        Iposicion = 0
        CantSalida = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Salida").Rows.Count
            If Not IsDBNull(DataSet.Tables("Salida").Rows(Iposicion)("Cantidad")) Then
                CantSalida = Trim(DataSet.Tables("Salida").Rows(Iposicion)("Cantidad"))
                Importe = Trim(DataSet.Tables("Salida").Rows(Iposicion)("Costo"))
                ImporteD = Trim(DataSet.Tables("Salida").Rows(Iposicion)("CostoD"))
            End If
            TotalSalidaBodega = TotalSalidaBodega + Importe
            TotalSalidaBodegaD = TotalSalidaBodegaD + ImporteD
            Iposicion = Iposicion + 1
        Loop

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE DEVOLUCIONES VENTAS EN CORDOBAS Y DOLARES ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) AS Costo,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa WHERE (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Devolucion de Venta') AND (Facturas.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario <> 0)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "DevVentas")
        Registros = DataSet.Tables("DevVentas").Rows.Count - 1
        Iposicion = 0
        CantDevVenta = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("DevVentas").Rows.Count
            If Not IsDBNull(DataSet.Tables("DevVentas").Rows(Iposicion)("Cantidad")) Then
                CantDevVenta = Trim(DataSet.Tables("DevVentas").Rows(Iposicion)("Cantidad"))
                Importe = Trim(DataSet.Tables("DevVentas").Rows(Iposicion)("Costo"))
                ImporteD = Trim(DataSet.Tables("DevVentas").Rows(Iposicion)("CostoD"))
            End If
            TotalDevVentas = TotalDevVentas + Importe
            TotalDevVentasD = TotalDevVentasD + ImporteD
            Iposicion = Iposicion + 1
        Loop

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////BUSCO EL TOTAL TRANSFERENCIAS ENVIADAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Costo,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa WHERE (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Transferencia Enviada') AND (Facturas.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario <> 0)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "TransEnviada")
        Registros = DataSet.Tables("TransEnviada").Rows.Count - 1
        Iposicion = 0
        CantTransEnviada = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("TransEnviada").Rows.Count
            If Not IsDBNull(DataSet.Tables("TransEnviada").Rows(Iposicion)("Cantidad")) Then
                CantTransEnviada = Trim(DataSet.Tables("TransEnviada").Rows(Iposicion)("Cantidad"))
                Importe = Trim(DataSet.Tables("TransEnviada").Rows(Iposicion)("Costo"))
                ImporteD = Trim(DataSet.Tables("TransEnviada").Rows(Iposicion)("CostoD"))
            End If
            TotalTransEnviada = TotalTransEnviada + Importe
            TotalTransEnviadaD = TotalTransEnviadaD + ImporteD
            Iposicion = Iposicion + 1
        Loop


        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////FORMULA COSTO PROMEDIO /////////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        'CostoPromedio =  TotalCompras+TotalDevVenas-TotalVentas-TotalDevCompra
        '                 ------------------------------------------------------
        '                 UnidCompra+UnidadDevVentas-UnidadVentas-UnidadDevCompra


        TotalCantidad = CantidadCompras + CantDevVenta + CantTransRecibida - CantVentas - CantDevCompra - CantTransEnviada - CantSalida
        TotalImporte = TotalCompras + TotalDevVentas + TotalTransRecibida - TotalVentas - TotalDevCompras - TotalTransEnviada - TotalSalidaBodega
        TotalImporteD = TotalComprasD + TotalDevVentasD + TotalTransRecibidaD - TotalVentasD - TotalDevComprasD - TotalTransEnviadaD - TotalSalidaBodegaD


        If TotalCantidad <> 0 Then
            PrecioCosto = Format(TotalImporte / TotalCantidad, "##,##0.000000")
            PrecioCostoDolar = Format((TotalImporteD / TotalCantidad), "##,##0.000000")
        Else
            'PrecioCosto = PrecioUnitario
            'If PrecioCosto <> 0 Then
            '    PrecioCostoDolar = Format((TotalImporteD / TotalCantidad), "##,##0.000000")
            'Else
            '    PrecioCostoDolar = 0
            'End If
            PrecioCosto = 0
            PrecioCostoDolar = 0
        End If

        CostoPromedioBodega = PrecioCosto
        If TotalCantidad <> 0 Then
            CostoPromedioDolar = PrecioCostoDolar
        End If

        DataSet.Tables("Compras").Reset()
    End Function


    Public Function CostoPromedioKardexBodega(ByVal CodigoProducto As String, ByVal FechaCompras As Date, ByVal CodBodega As String) As Double
        Dim SqlCompras As String, Iposicion As Double, TotalImporte As Double, CantidadCompras As Double, MonedaCompra As String, Importe As Double, PrecioUnitario As Double, PrecioCostoDolar As Double, PrecioCosto As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), Registros As Double, ImporteD As Double, TotalImporteD As Double
        Dim DataSet As New DataSet, TotalCantidad As Double = 0, TasaCambio As Double
        Dim DataAdapter As New SqlClient.SqlDataAdapter
        Dim TotalCompras As Double = 0, TotalDevCompras As Double = 0, TotalVentas As Double = 0, TotalDevVentas As Double = 0
        Dim TotalComprasD As Double = 0, TotalDevComprasD As Double = 0, TotalVentasD As Double = 0, TotalDevVentasD As Double = 0, TotalTransEnviada As Double = 0, TotalTransRecibida As Double = 0, TotalTransEnviadaD As Double = 0, TotalTransRecibidaD As Double = 0
        Dim CantDevCompra As Double = 0, CantDevVenta As Double = 0, CantVentas As Double = 0, CantTransEnviada As Double = 0, CantTransRecibida As Double = 0
        Dim TotalSalidaBodega As Double = 0, TotalSalidaBodegaD As Double = 0, CantSalida As Double = 0


        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE COMPRAS DOLARES ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario * TasaCambio.MontoTasa) AS Precio_Unitario, SUM(Detalle_Compras.Descuento * TasaCambio.MontoTasa) AS Descuento, SUM(Detalle_Compras.Descuento) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto * TasaCambio.MontoTasa) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad * TasaCambio.MontoTasa) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS ImporteD, Compras.Cod_Bodega FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                     "WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodBodega & "') AND (Compras.MonedaCompra = N'Dolares')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "ComprasD")
        Registros = DataSet.Tables("ComprasD").Rows.Count - 1
        Iposicion = 0
        TotalImporte = 0
        ImporteD = 0
        TotalImporteD = 0
        CantidadCompras = 0
        TotalCantidad = 0

        Do While Iposicion < DataSet.Tables("ComprasD").Rows.Count
            MonedaCompra = DataSet.Tables("ComprasD").Rows(Iposicion)("MonedaCompra")
            PrecioUnitario = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("Precio_Neto"))
            CantidadCompras = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("Cantidad")) + CantidadCompras
            Importe = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("ImporteD"))
            TotalCompras = TotalCompras + Importe
            TotalComprasD = TotalComprasD + ImporteD
            Iposicion = Iposicion + 1

        Loop

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE COMPRAS CORDOBAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SqlCompras = "SELECT Detalle_Compras.Numero_Compra, Detalle_Compras.Fecha_Compra, Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, Detalle_Compras.Cantidad, Detalle_Compras.Precio_Unitario, Detalle_Compras.Descuento, Detalle_Compras.Precio_Neto, Compras.MonedaCompra, Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad AS Importe, Compras.Cod_Bodega FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') AND (Compras.Cod_Bodega = '" & CodBodega & "')"
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario / TasaCambio.MontoTasa) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Descuento / TasaCambio.MontoTasa) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad / TasaCambio.MontoTasa) AS ImporteD, Compras.Cod_Bodega FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodBodega & "') AND (Compras.MonedaCompra <> N'Dolares')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        Registros = DataSet.Tables("Compras").Rows.Count - 1
        Iposicion = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Compras").Rows.Count
            MonedaCompra = DataSet.Tables("Compras").Rows(Iposicion)("MonedaCompra")
            PrecioUnitario = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Precio_Neto"))
            CantidadCompras = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Cantidad")) + CantidadCompras
            Importe = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("Compras").Rows(Iposicion)("ImporteD"))
            TotalCompras = TotalCompras + Importe
            TotalComprasD = TotalComprasD + ImporteD
            Iposicion = Iposicion + 1

        Loop


        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DEVOLUCIONES COMPRAS DOLARES////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, MAX(Detalle_Compras.Fecha_Compra) AS Fecha_Compra, Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Unitario * Detalle_Compras.Cantidad) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra GROUP BY Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, Compras.MonedaCompra HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra')"
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario * TasaCambio.MontoTasa) AS Precio_Unitario, SUM(Detalle_Compras.Descuento * TasaCambio.MontoTasa) AS Descuento, SUM(Detalle_Compras.Descuento) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto * TasaCambio.MontoTasa) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad * TasaCambio.MontoTasa) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS ImporteD, Compras.Cod_Bodega FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                     "WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodBodega & "') AND (Compras.MonedaCompra = N'Dolares')"

        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionD")
        Registros = DataSet.Tables("DevolucionD").Rows.Count - 1
        Iposicion = 0
        CantDevCompra = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("DevolucionD").Rows.Count
            MonedaCompra = DataSet.Tables("DevolucionD").Rows(Iposicion)("MonedaCompra")
            PrecioUnitario = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("Precio_Neto"))
            CantDevCompra = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("Cantidad")) + CantDevCompra
            Importe = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("ImporteD"))
            TotalDevCompras = TotalDevCompras + Importe
            TotalDevComprasD = TotalDevComprasD + ImporteD

            Iposicion = Iposicion + 1

        Loop


        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE DEVOLUCION COMPRAS CORDOBAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SqlCompras = "SELECT Detalle_Compras.Numero_Compra, Detalle_Compras.Fecha_Compra, Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, Detalle_Compras.Cantidad, Detalle_Compras.Precio_Unitario, Detalle_Compras.Descuento, Detalle_Compras.Precio_Neto, Compras.MonedaCompra, Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad AS Importe, Compras.Cod_Bodega FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') AND (Compras.Cod_Bodega = '" & CodBodega & "')"
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario / TasaCambio.MontoTasa) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Descuento / TasaCambio.MontoTasa) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad / TasaCambio.MontoTasa) AS ImporteD, Compras.Cod_Bodega FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodBodega & "') AND (Compras.MonedaCompra <> 'Dolares')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Devolucion")
        Registros = DataSet.Tables("Devolucion").Rows.Count - 1
        Iposicion = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Devolucion").Rows.Count
            MonedaCompra = DataSet.Tables("Devolucion").Rows(Iposicion)("MonedaCompra")

            PrecioUnitario = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("Precio_Neto"))
            CantDevCompra = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("Cantidad")) + CantDevCompra
            Importe = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("ImporteD"))
            TotalDevCompras = TotalDevCompras + Importe
            TotalDevComprasD = TotalDevComprasD + ImporteD
            Iposicion = Iposicion + 1

        Loop


        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////BUSCO EL TOTAL TRANSFERENCIAS RECIBIDAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT  MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario / TasaCambio.MontoTasa) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Descuento / TasaCambio.MontoTasa) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS Precio_NetoD, SUM(Detalle_Compras.Precio_Unitario * Detalle_Compras.Cantidad) AS Importe, SUM(Detalle_Compras.Precio_Unitario * Detalle_Compras.Cantidad / TasaCambio.MontoTasa) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                     "WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Tipo_Compra = 'Transferencia Recibida') AND (Compras.Cod_Bodega = '" & CodBodega & "') GROUP BY Detalle_Compras.Cod_Producto HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "TransRecibida")
        Registros = DataSet.Tables("TransRecibida").Rows.Count - 1
        Iposicion = 0
        CantTransRecibida = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("TransRecibida").Rows.Count
            If Not IsDBNull(DataSet.Tables("TransRecibida").Rows(Iposicion)("Cantidad")) Then
                CantTransRecibida = Trim(DataSet.Tables("TransRecibida").Rows(Iposicion)("Cantidad"))
                Importe = Trim(DataSet.Tables("TransRecibida").Rows(Iposicion)("Importe"))
                ImporteD = Trim(DataSet.Tables("TransRecibida").Rows(Iposicion)("ImporteD"))
            End If
            TotalTransRecibida = TotalTransRecibida + Importe
            TotalTransRecibidaD = TotalTransRecibidaD + ImporteD
            Iposicion = Iposicion + 1
        Loop


        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE VENTAS EN CORDOBAS Y DOLARES ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) AS Costo,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Facturas.Fecha_Factura <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Factura') AND (Facturas.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario <> 0)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Ventas")
        Registros = DataSet.Tables("Ventas").Rows.Count - 1
        Iposicion = 0
        CantVentas = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Ventas").Rows.Count
            If Not IsDBNull(DataSet.Tables("Ventas").Rows(Iposicion)("Cantidad")) Then
                CantVentas = Trim(DataSet.Tables("Ventas").Rows(Iposicion)("Cantidad"))
                Importe = Trim(DataSet.Tables("Ventas").Rows(Iposicion)("Costo"))
                ImporteD = Trim(DataSet.Tables("Ventas").Rows(Iposicion)("CostoD"))
            End If
            TotalVentas = TotalVentas + Importe
            TotalVentasD = TotalVentasD + ImporteD
            Iposicion = Iposicion + 1
        Loop

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE SALIDAS////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Costo,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Facturas.Fecha_Factura <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Salida Bodega') AND (Facturas.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario <> 0)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Salida")
        Registros = DataSet.Tables("Salida").Rows.Count - 1
        Iposicion = 0
        CantSalida = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Salida").Rows.Count
            If Not IsDBNull(DataSet.Tables("Salida").Rows(Iposicion)("Cantidad")) Then
                CantSalida = Trim(DataSet.Tables("Salida").Rows(Iposicion)("Cantidad"))
                Importe = Trim(DataSet.Tables("Salida").Rows(Iposicion)("Costo"))
                ImporteD = Trim(DataSet.Tables("Salida").Rows(Iposicion)("CostoD"))
            End If
            TotalSalidaBodega = TotalSalidaBodega + Importe
            TotalSalidaBodegaD = TotalSalidaBodegaD + ImporteD
            Iposicion = Iposicion + 1
        Loop

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE DEVOLUCIONES VENTAS EN CORDOBAS Y DOLARES ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) AS Costo,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Facturas.Fecha_Factura <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Devolucion de Venta') AND (Facturas.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario <> 0)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "DevVentas")
        Registros = DataSet.Tables("DevVentas").Rows.Count - 1
        Iposicion = 0
        CantDevVenta = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("DevVentas").Rows.Count
            If Not IsDBNull(DataSet.Tables("DevVentas").Rows(Iposicion)("Cantidad")) Then
                CantDevVenta = Trim(DataSet.Tables("DevVentas").Rows(Iposicion)("Cantidad"))
                Importe = Trim(DataSet.Tables("DevVentas").Rows(Iposicion)("Costo"))
                ImporteD = Trim(DataSet.Tables("DevVentas").Rows(Iposicion)("CostoD"))
            End If
            TotalDevVentas = TotalDevVentas + Importe
            TotalDevVentasD = TotalDevVentasD + ImporteD
            Iposicion = Iposicion + 1
        Loop

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////BUSCO EL TOTAL TRANSFERENCIAS ENVIADAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Costo,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Facturas.Fecha_Factura <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Transferencia Enviada') AND (Facturas.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario <> 0)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "TransEnviada")
        Registros = DataSet.Tables("TransEnviada").Rows.Count - 1
        Iposicion = 0
        CantTransEnviada = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("TransEnviada").Rows.Count
            If Not IsDBNull(DataSet.Tables("TransEnviada").Rows(Iposicion)("Cantidad")) Then
                CantTransEnviada = Trim(DataSet.Tables("TransEnviada").Rows(Iposicion)("Cantidad"))
                Importe = Trim(DataSet.Tables("TransEnviada").Rows(Iposicion)("Costo"))
                ImporteD = Trim(DataSet.Tables("TransEnviada").Rows(Iposicion)("CostoD"))
            End If
            TotalTransEnviada = TotalTransEnviada + Importe
            TotalTransEnviadaD = TotalTransEnviadaD + ImporteD
            Iposicion = Iposicion + 1
        Loop


        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////FORMULA COSTO PROMEDIO /////////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        'CostoPromedio =  TotalCompras+TotalDevVenas-TotalVentas-TotalDevCompra
        '                 ------------------------------------------------------
        '                 UnidCompra+UnidadDevVentas-UnidadVentas-UnidadDevCompra


        TotalCantidad = CantidadCompras + CantDevVenta + CantTransRecibida - CantVentas - CantDevCompra - CantTransEnviada - CantSalida
        TotalImporte = TotalCompras + TotalDevVentas + TotalTransRecibida - TotalVentas - TotalDevCompras - TotalTransEnviada - TotalSalidaBodega
        TotalImporteD = TotalComprasD + TotalDevVentasD + TotalTransRecibidaD - TotalVentasD - TotalDevComprasD - TotalTransEnviadaD - TotalSalidaBodegaD



        TasaCambio = BuscaTasaCambio(FechaCompras)

        If TotalCantidad <> 0 Then
            PrecioCosto = Format(TotalImporte / TotalCantidad, "##,##0.000000")
            PrecioCostoDolar = Format((TotalImporteD / TotalCantidad), "##,##0.000000")
        Else
            'PrecioCosto = PrecioUnitario
            'If PrecioCosto <> 0 Then
            '    PrecioCostoDolar = Format((TotalImporteD / TotalCantidad), "##,##0.000000")
            'Else
            '    PrecioCostoDolar = 0
            'End If
            PrecioCosto = 0
            PrecioCostoDolar = 0
        End If

        CostoPromedioKardexBodega = PrecioCosto
        CostoPromedioDolar = PrecioCostoDolar

        DataSet.Tables("Compras").Reset()
    End Function
    Public Function CostoPromedioKardex(ByVal CodigoProducto As String, ByVal FechaCompras As Date) As Double
        Dim SqlCompras As String, Iposicion As Double, TotalImporte As Double, CantidadCompras As Double, MonedaCompra As String, Importe As Double, PrecioUnitario As Double, PrecioCostoDolar As Double, PrecioCosto As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), Registros As Double, ImporteD As Double, TotalImporteD As Double
        Dim DataSet As New DataSet, TotalCantidad As Double = 0, TasaCambio As Double
        Dim DataAdapter As New SqlClient.SqlDataAdapter
        Dim TotalCompras As Double = 0, TotalDevCompras As Double = 0, TotalVentas As Double = 0, TotalDevVentas As Double = 0
        Dim TotalComprasD As Double = 0, TotalDevComprasD As Double = 0, TotalVentasD As Double = 0, TotalDevVentasD As Double = 0, TotalTransEnviada As Double = 0, TotalTransRecibida As Double = 0, TotalTransEnviadaD As Double = 0, TotalTransRecibidaD As Double = 0
        Dim CantDevCompra As Double = 0, CantDevVenta As Double = 0, CantVentas As Double = 0, CantTransEnviada As Double = 0, CantTransRecibida As Double = 0
        Dim TotalSalidaBodega As Double = 0, TotalSalidaBodegaD As Double = 0, CantSalida As Double = 0
        Dim TotalCantEntradas As Double = 0, TotalCantSalidas As Double = 0, TotalImporteEntradas As Double = 0, TotalImporteSalidas As Double = 0, TotalImporteEntradasD As Double = 0, TotalImporteSalidasD As Double = 0
        Dim FechaFactura As Date

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE COMPRAS DOLARES ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario * TasaCambio.MontoTasa) AS Precio_Unitario, SUM(Detalle_Compras.Descuento * TasaCambio.MontoTasa) AS Descuento, SUM(Detalle_Compras.Descuento) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto * TasaCambio.MontoTasa) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad * TasaCambio.MontoTasa) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS ImporteD, Compras.Cod_Bodega FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                     "WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.MonedaCompra = N'Dolares')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "ComprasD")
        Registros = DataSet.Tables("ComprasD").Rows.Count - 1
        Iposicion = 0
        TotalImporte = 0
        ImporteD = 0
        TotalImporteD = 0
        CantidadCompras = 0
        TotalCantidad = 0

        Do While Iposicion < DataSet.Tables("ComprasD").Rows.Count
            MonedaCompra = DataSet.Tables("ComprasD").Rows(Iposicion)("MonedaCompra")
            PrecioUnitario = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("Precio_Neto"))
            CantidadCompras = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("Cantidad")) + CantidadCompras
            Importe = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("ImporteD"))
            TotalCompras = TotalCompras + Importe
            TotalComprasD = TotalComprasD + ImporteD
            Iposicion = Iposicion + 1
            My.Application.DoEvents()
        Loop

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE COMPRAS CORDOBAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SqlCompras = "SELECT Detalle_Compras.Numero_Compra, Detalle_Compras.Fecha_Compra, Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, Detalle_Compras.Cantidad, Detalle_Compras.Precio_Unitario, Detalle_Compras.Descuento, Detalle_Compras.Precio_Neto, Compras.MonedaCompra, Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad AS Importe, Compras.Cod_Bodega FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') AND (Compras.Cod_Bodega = '" & CodBodega & "')"
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario / TasaCambio.MontoTasa) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Descuento / TasaCambio.MontoTasa) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad / TasaCambio.MontoTasa) AS ImporteD, Compras.Cod_Bodega FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.MonedaCompra <> N'Dolares')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        Registros = DataSet.Tables("Compras").Rows.Count - 1
        Iposicion = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Compras").Rows.Count
            MonedaCompra = DataSet.Tables("Compras").Rows(Iposicion)("MonedaCompra")
            PrecioUnitario = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Precio_Neto"))
            CantidadCompras = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Cantidad")) + CantidadCompras
            Importe = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("Compras").Rows(Iposicion)("ImporteD"))
            TotalCompras = TotalCompras + Importe
            TotalComprasD = TotalComprasD + ImporteD
            Iposicion = Iposicion + 1
            My.Application.DoEvents()
        Loop


        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DEVOLUCIONES COMPRAS DOLARES////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, MAX(Detalle_Compras.Fecha_Compra) AS Fecha_Compra, Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Unitario * Detalle_Compras.Cantidad) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra GROUP BY Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, Compras.MonedaCompra HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra')"
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario * TasaCambio.MontoTasa) AS Precio_Unitario, SUM(Detalle_Compras.Descuento * TasaCambio.MontoTasa) AS Descuento, SUM(Detalle_Compras.Descuento) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto * TasaCambio.MontoTasa) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad * TasaCambio.MontoTasa) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS ImporteD, Compras.Cod_Bodega FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                     "WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.MonedaCompra = N'Dolares')"

        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionD")
        Registros = DataSet.Tables("DevolucionD").Rows.Count - 1
        Iposicion = 0
        CantDevCompra = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("DevolucionD").Rows.Count
            MonedaCompra = DataSet.Tables("DevolucionD").Rows(Iposicion)("MonedaCompra")
            PrecioUnitario = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("Precio_Neto"))
            CantDevCompra = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("Cantidad")) + CantDevCompra
            Importe = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("ImporteD"))
            TotalDevCompras = TotalDevCompras + Importe
            TotalDevComprasD = TotalDevComprasD + ImporteD

            Iposicion = Iposicion + 1
            My.Application.DoEvents()
        Loop


        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE DEVOLUCION COMPRAS CORDOBAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SqlCompras = "SELECT Detalle_Compras.Numero_Compra, Detalle_Compras.Fecha_Compra, Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, Detalle_Compras.Cantidad, Detalle_Compras.Precio_Unitario, Detalle_Compras.Descuento, Detalle_Compras.Precio_Neto, Compras.MonedaCompra, Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad AS Importe, Compras.Cod_Bodega FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') AND (Compras.Cod_Bodega = '" & CodBodega & "')"
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario / TasaCambio.MontoTasa) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Descuento / TasaCambio.MontoTasa) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad / TasaCambio.MontoTasa) AS ImporteD, Compras.Cod_Bodega FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "')  AND (Compras.MonedaCompra <> 'Dolares')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Devolucion")
        Registros = DataSet.Tables("Devolucion").Rows.Count - 1
        Iposicion = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Devolucion").Rows.Count
            MonedaCompra = DataSet.Tables("Devolucion").Rows(Iposicion)("MonedaCompra")

            PrecioUnitario = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("Precio_Neto"))
            CantDevCompra = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("Cantidad")) + CantDevCompra
            Importe = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("ImporteD"))
            TotalDevCompras = TotalDevCompras + Importe
            TotalDevComprasD = TotalDevComprasD + ImporteD
            Iposicion = Iposicion + 1

        Loop


        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////BUSCO EL TOTAL TRANSFERENCIAS RECIBIDAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SqlCompras = "SELECT  MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario / TasaCambio.MontoTasa) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Descuento / TasaCambio.MontoTasa) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS Precio_NetoD, SUM(Detalle_Compras.Precio_Unitario * Detalle_Compras.Cantidad) AS Importe, SUM(Detalle_Compras.Precio_Unitario * Detalle_Compras.Cantidad / TasaCambio.MontoTasa) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
        '             "WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Tipo_Compra = 'Transferencia Recibida')  GROUP BY Detalle_Compras.Cod_Producto HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "')"
        'DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        'DataAdapter.Fill(DataSet, "TransRecibida")
        'Registros = DataSet.Tables("TransRecibida").Rows.Count - 1
        'Iposicion = 0
        'CantTransRecibida = 0
        'Importe = 0
        'ImporteD = 0
        'Do While Iposicion < DataSet.Tables("TransRecibida").Rows.Count
        '    If Not IsDBNull(DataSet.Tables("TransRecibida").Rows(Iposicion)("Cantidad")) Then
        '        CantTransRecibida = Trim(DataSet.Tables("TransRecibida").Rows(Iposicion)("Cantidad"))
        '        Importe = Trim(DataSet.Tables("TransRecibida").Rows(Iposicion)("Importe"))
        '        ImporteD = Trim(DataSet.Tables("TransRecibida").Rows(Iposicion)("ImporteD"))
        '    End If
        '    TotalTransRecibida = TotalTransRecibida + Importe
        '    TotalTransRecibidaD = TotalTransRecibidaD + ImporteD
        '    Iposicion = Iposicion + 1
        'Loop


        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE VENTAS EN CORDOBAS Y DOLARES ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT SUM(CASE WHEN Detalle_Facturas.Costo_Unitario = 0 THEN 0 ELSE Detalle_Facturas.Cantidad END) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) AS Costo,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Facturas.Fecha_Factura <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Factura')  AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario <> 0)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Ventas")
        Registros = DataSet.Tables("Ventas").Rows.Count - 1
        Iposicion = 0
        CantVentas = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Ventas").Rows.Count
            If Not IsDBNull(DataSet.Tables("Ventas").Rows(Iposicion)("Cantidad")) Then
                CantVentas = Trim(DataSet.Tables("Ventas").Rows(Iposicion)("Cantidad"))
                Importe = Trim(DataSet.Tables("Ventas").Rows(Iposicion)("Costo"))
                ImporteD = Trim(DataSet.Tables("Ventas").Rows(Iposicion)("CostoD"))
            End If
            TotalVentas = TotalVentas + Importe
            TotalVentasD = TotalVentasD + ImporteD
            Iposicion = Iposicion + 1
        Loop

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE SALIDAS////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT  SUM(CASE WHEN Detalle_Facturas.Costo_Unitario = 0 THEN 0 ELSE Detalle_Facturas.Cantidad END) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) AS Costo,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Facturas.Fecha_Factura <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Salida Bodega')  AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario <> 0)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Salida")
        Registros = DataSet.Tables("Salida").Rows.Count - 1
        Iposicion = 0
        CantSalida = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Salida").Rows.Count
            If Not IsDBNull(DataSet.Tables("Salida").Rows(Iposicion)("Cantidad")) Then
                CantSalida = Trim(DataSet.Tables("Salida").Rows(Iposicion)("Cantidad"))
                If Not IsDBNull(DataSet.Tables("Salida").Rows(Iposicion)("Costo")) Then
                    Importe = Trim(DataSet.Tables("Salida").Rows(Iposicion)("Costo"))
                End If
                If Not IsDBNull(DataSet.Tables("Salida").Rows(Iposicion)("Costo")) Then
                    ImporteD = Trim(DataSet.Tables("Salida").Rows(Iposicion)("CostoD"))
                End If

                If Importe = 0 Then
                    CantSalida = 0
                End If

            End If
            TotalSalidaBodega = TotalSalidaBodega + Importe
            TotalSalidaBodegaD = TotalSalidaBodegaD + ImporteD
            Iposicion = Iposicion + 1

            My.Application.DoEvents()
        Loop

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE DEVOLUCIONES VENTAS EN CORDOBAS Y DOLARES ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT SUM(CASE WHEN Detalle_Facturas.Costo_Unitario = 0 THEN 0 ELSE Detalle_Facturas.Cantidad END) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) AS Costo,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Facturas.Fecha_Factura <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Devolucion de Venta')  AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario <> 0)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "DevVentas")
        Registros = DataSet.Tables("DevVentas").Rows.Count - 1
        Iposicion = 0
        CantDevVenta = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("DevVentas").Rows.Count
            If Not IsDBNull(DataSet.Tables("DevVentas").Rows(Iposicion)("Cantidad")) Then
                CantDevVenta = Trim(DataSet.Tables("DevVentas").Rows(Iposicion)("Cantidad"))
                Importe = Trim(DataSet.Tables("DevVentas").Rows(Iposicion)("Costo"))
                ImporteD = Trim(DataSet.Tables("DevVentas").Rows(Iposicion)("CostoD"))
            End If
            TotalDevVentas = TotalDevVentas + Importe
            TotalDevVentasD = TotalDevVentasD + ImporteD
            Iposicion = Iposicion + 1

            My.Application.DoEvents()
        Loop

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////BUSCO EL TOTAL TRANSFERENCIAS ENVIADAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SqlCompras = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Costo,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
        '             "WHERE (Detalle_Facturas.Fecha_Factura <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Transferencia Enviada') AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario <> 0)"
        'DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        'DataAdapter.Fill(DataSet, "TransEnviada")
        'Registros = DataSet.Tables("TransEnviada").Rows.Count - 1
        'Iposicion = 0
        'CantTransEnviada = 0
        'Importe = 0
        'ImporteD = 0
        'Do While Iposicion < DataSet.Tables("TransEnviada").Rows.Count
        '    If Not IsDBNull(DataSet.Tables("TransEnviada").Rows(Iposicion)("Cantidad")) Then
        '        CantTransEnviada = Trim(DataSet.Tables("TransEnviada").Rows(Iposicion)("Cantidad"))
        '        Importe = Trim(DataSet.Tables("TransEnviada").Rows(Iposicion)("Costo"))
        '        ImporteD = Trim(DataSet.Tables("TransEnviada").Rows(Iposicion)("CostoD"))
        '    End If
        '    TotalTransEnviada = TotalTransEnviada + Importe
        '    TotalTransEnviadaD = TotalTransEnviadaD + ImporteD
        '    Iposicion = Iposicion + 1
        'Loop


        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////FORMULA COSTO PROMEDIO /////////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        'CostoPromedio =  TotalCompras+TotalDevVenas-TotalVentas-TotalDevCompra
        '                 ------------------------------------------------------
        '                 UnidCompra+UnidadDevVentas-UnidadVentas-UnidadDevCompra


        TotalCantidad = CantidadCompras + CantDevVenta + CantTransRecibida - CantVentas - CantDevCompra - CantTransEnviada - CantSalida
        TotalImporte = TotalCompras + TotalDevVentas + TotalTransRecibida - TotalVentas - TotalDevCompras - TotalTransEnviada - TotalSalidaBodega
        TotalImporteD = TotalComprasD + TotalDevVentasD + TotalTransRecibidaD - TotalVentasD - TotalDevComprasD - TotalTransEnviadaD - TotalSalidaBodegaD

 

        TasaCambio = BuscaTasaCambio(FechaCompras)

        If TotalCantidad <> 0 Then
            PrecioCosto = Format(TotalImporte / TotalCantidad, "##,##0.000000")
            PrecioCostoDolar = Format((TotalImporteD / TotalCantidad), "##,##0.000000")
        Else
            'PrecioCosto = PrecioUnitario
            'If PrecioCosto <> 0 Then
            '    PrecioCostoDolar = Format((TotalImporteD / TotalCantidad), "##,##0.000000")
            'Else
            '    PrecioCostoDolar = 0
            'End If
            PrecioCosto = 0
            PrecioCostoDolar = 0
        End If


        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------SI NO EXISTE COSTO PROMEDIO, BUSCO EL ULTIMO COSTO UTILIZADO ---------------------------------
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        If PrecioCosto = 0 Then
            SqlCompras = "SELECT Detalle_Facturas.id_Detalle_Factura, Detalle_Facturas.Numero_Factura, Detalle_Facturas.Fecha_Factura, Detalle_Facturas.Tipo_Factura, Detalle_Facturas.Cod_Producto, Detalle_Facturas.Descripcion_Producto, Detalle_Facturas.Cantidad, Detalle_Facturas.Precio_Unitario, Detalle_Facturas.Descuento, Detalle_Facturas.Precio_Neto, Detalle_Facturas.Importe, Detalle_Facturas.TasaCambio, Detalle_Facturas.CodTarea, Detalle_Facturas.Costo_Unitario, Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa AS Costo_UnitarioDolar FROM Detalle_Facturas INNER JOIN TasaCambio ON Detalle_Facturas.Fecha_Factura = TasaCambio.FechaTasa  " & _
                        "WHERE (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Costo_Unitario <> 0) AND (Detalle_Facturas.Fecha_Factura <= CONVERT(DATETIME,'" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) ORDER BY Detalle_Facturas.Fecha_Factura"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
            DataAdapter.Fill(DataSet, "UltimoCosto")
            If DataSet.Tables("UltimoCosto").Rows.Count <> 0 Then
                Registros = DataSet.Tables("UltimoCosto").Rows.Count
                PrecioCosto = DataSet.Tables("UltimoCosto").Rows(Registros - 1)("Costo_Unitario")
                FechaFactura = DataSet.Tables("UltimoCosto").Rows(Registros - 1)("Fecha_Factura")
                PrecioCostoDolar = PrecioCosto * BuscaTasaCambio(FechaFactura)
            End If
            DataSet.Tables("UltimoCosto").Reset()

        End If


        CostoPromedioKardex = PrecioCosto
        CostoPromedioDolar = PrecioCostoDolar

        DataSet.Tables("Compras").Reset()
    End Function


    Public Function CostoPromedioActualizaBodega(ByVal CodigoProducto As String, ByVal FechaCompras As Date, ByVal CodBodega As String) As Double
        Dim SqlCompras As String, Iposicion As Double, TotalImporte As Double, CantidadCompras As Double, MonedaCompra As String, Importe As Double, PrecioUnitario As Double, PrecioCostoDolar As Double, PrecioCosto As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), Registros As Double, ImporteD As Double, TotalImporteD As Double
        Dim DataSet As New DataSet, TotalCantidad As Double = 0, TasaCambio As Double
        Dim DataAdapter As New SqlClient.SqlDataAdapter
        Dim TotalCompras As Double = 0, TotalDevCompras As Double = 0, TotalVentas As Double = 0, TotalDevVentas As Double = 0
        Dim TotalComprasD As Double = 0, TotalDevComprasD As Double = 0, TotalVentasD As Double = 0, TotalDevVentasD As Double = 0, TotalTransEnviada As Double = 0, TotalTransRecibida As Double = 0, TotalTransEnviadaD As Double = 0, TotalTransRecibidaD As Double = 0
        Dim CantDevCompra As Double = 0, CantDevVenta As Double = 0, CantVentas As Double = 0, CantTransEnviada As Double = 0, CantTransRecibida As Double = 0
        Dim TotalSalidaBodega As Double = 0, TotalSalidaBodegaD As Double = 0, CantSalida As Double = 0
        Dim TotalCantEntradas As Double = 0, TotalCantSalidas As Double = 0, TotalImporteEntradas As Double = 0, TotalImporteSalidas As Double = 0, TotalImporteEntradasD As Double = 0, TotalImporteSalidasD As Double = 0

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE COMPRAS DOLARES ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario * TasaCambio.MontoTasa) AS Precio_Unitario, SUM(Detalle_Compras.Descuento * TasaCambio.MontoTasa) AS Descuento, SUM(Detalle_Compras.Descuento) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto * TasaCambio.MontoTasa) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad * TasaCambio.MontoTasa) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS ImporteD, Compras.Cod_Bodega FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                     "WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodBodega & "') AND (Compras.MonedaCompra = N'Dolares')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "ComprasD")
        Registros = DataSet.Tables("ComprasD").Rows.Count - 1
        Iposicion = 0
        TotalImporte = 0
        ImporteD = 0
        TotalImporteD = 0
        CantidadCompras = 0
        TotalCantidad = 0

        Do While Iposicion < DataSet.Tables("ComprasD").Rows.Count
            MonedaCompra = DataSet.Tables("ComprasD").Rows(Iposicion)("MonedaCompra")
            PrecioUnitario = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("Precio_Neto"))
            CantidadCompras = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("Cantidad")) + CantidadCompras
            Importe = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("ComprasD").Rows(Iposicion)("ImporteD"))
            TotalCompras = TotalCompras + Importe
            TotalComprasD = TotalComprasD + ImporteD
            Iposicion = Iposicion + 1

        Loop

        DataSet.Tables("ComprasD").Reset()

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE COMPRAS CORDOBAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SqlCompras = "SELECT Detalle_Compras.Numero_Compra, Detalle_Compras.Fecha_Compra, Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, Detalle_Compras.Cantidad, Detalle_Compras.Precio_Unitario, Detalle_Compras.Descuento, Detalle_Compras.Precio_Neto, Compras.MonedaCompra, Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad AS Importe, Compras.Cod_Bodega FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') AND (Compras.Cod_Bodega = '" & CodBodega & "')"
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario / TasaCambio.MontoTasa) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Descuento / TasaCambio.MontoTasa) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad / TasaCambio.MontoTasa) AS ImporteD, Compras.Cod_Bodega FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodBodega & "') AND (Compras.MonedaCompra <> N'Dolares')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        Registros = DataSet.Tables("Compras").Rows.Count - 1
        Iposicion = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Compras").Rows.Count
            MonedaCompra = DataSet.Tables("Compras").Rows(Iposicion)("MonedaCompra")
            PrecioUnitario = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Precio_Neto"))
            CantidadCompras = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Cantidad")) + CantidadCompras
            Importe = Trim(DataSet.Tables("Compras").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("Compras").Rows(Iposicion)("ImporteD"))
            TotalCompras = TotalCompras + Importe
            TotalComprasD = TotalComprasD + ImporteD
            Iposicion = Iposicion + 1

        Loop

        DataSet.Tables("Compras").Reset()
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DEVOLUCIONES COMPRAS DOLARES////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, MAX(Detalle_Compras.Fecha_Compra) AS Fecha_Compra, Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Unitario * Detalle_Compras.Cantidad) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra GROUP BY Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, Compras.MonedaCompra HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra')"
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario * TasaCambio.MontoTasa) AS Precio_Unitario, SUM(Detalle_Compras.Descuento * TasaCambio.MontoTasa) AS Descuento, SUM(Detalle_Compras.Descuento) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto * TasaCambio.MontoTasa) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad * TasaCambio.MontoTasa) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS ImporteD, Compras.Cod_Bodega FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                     "WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodBodega & "') AND (Compras.MonedaCompra = N'Dolares')"

        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionD")
        Registros = DataSet.Tables("DevolucionD").Rows.Count - 1
        Iposicion = 0
        CantDevCompra = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("DevolucionD").Rows.Count
            MonedaCompra = DataSet.Tables("DevolucionD").Rows(Iposicion)("MonedaCompra")
            PrecioUnitario = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("Precio_Neto"))
            CantDevCompra = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("Cantidad")) + CantDevCompra
            Importe = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("DevolucionD").Rows(Iposicion)("ImporteD"))
            TotalDevCompras = TotalDevCompras + Importe
            TotalDevComprasD = TotalDevComprasD + ImporteD

            Iposicion = Iposicion + 1

        Loop

        DataSet.Tables("DevolucionD").Reset()
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE DEVOLUCION COMPRAS CORDOBAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        'SqlCompras = "SELECT Detalle_Compras.Numero_Compra, Detalle_Compras.Fecha_Compra, Detalle_Compras.Tipo_Compra, Detalle_Compras.Cod_Producto, Detalle_Compras.Cantidad, Detalle_Compras.Precio_Unitario, Detalle_Compras.Descuento, Detalle_Compras.Precio_Neto, Compras.MonedaCompra, Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad AS Importe, Compras.Cod_Bodega FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') AND (Compras.Cod_Bodega = '" & CodBodega & "')"
        SqlCompras = "SELECT MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario / TasaCambio.MontoTasa) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Descuento / TasaCambio.MontoTasa) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS Precio_NetoD, Compras.MonedaCompra, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad / TasaCambio.MontoTasa) AS ImporteD, Compras.Cod_Bodega FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra') GROUP BY Detalle_Compras.Cod_Producto, Compras.MonedaCompra, Compras.Cod_Bodega HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodBodega & "') AND (Compras.MonedaCompra <> 'Dolares')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Devolucion")
        Registros = DataSet.Tables("Devolucion").Rows.Count - 1
        Iposicion = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Devolucion").Rows.Count
            MonedaCompra = DataSet.Tables("Devolucion").Rows(Iposicion)("MonedaCompra")

            PrecioUnitario = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("Precio_Neto"))
            CantDevCompra = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("Cantidad")) + CantDevCompra
            Importe = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("Importe"))
            ImporteD = Trim(DataSet.Tables("Devolucion").Rows(Iposicion)("ImporteD"))
            TotalDevCompras = TotalDevCompras + Importe
            TotalDevComprasD = TotalDevComprasD + ImporteD
            Iposicion = Iposicion + 1

        Loop
        DataSet.Tables("Devolucion").Reset()

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////BUSCO EL TOTAL TRANSFERENCIAS RECIBIDAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT  MAX(Detalle_Compras.Numero_Compra) AS Numero_Compra, Detalle_Compras.Cod_Producto, SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Unitario / TasaCambio.MontoTasa) AS Precio_UnitarioD, SUM(Detalle_Compras.Precio_Unitario) AS Precio_Unitario, SUM(Detalle_Compras.Descuento) AS Descuento, SUM(Detalle_Compras.Descuento / TasaCambio.MontoTasa) AS DescuentoD, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS Precio_NetoD, SUM(Detalle_Compras.Precio_Unitario * Detalle_Compras.Cantidad) AS Importe, SUM(Detalle_Compras.Precio_Unitario * Detalle_Compras.Cantidad / TasaCambio.MontoTasa) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                     "WHERE (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Tipo_Compra = 'Transferencia Recibida') AND (Compras.Cod_Bodega = '" & CodBodega & "') GROUP BY Detalle_Compras.Cod_Producto HAVING (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "TransRecibida")
        Registros = DataSet.Tables("TransRecibida").Rows.Count - 1
        Iposicion = 0
        CantTransRecibida = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("TransRecibida").Rows.Count
            If Not IsDBNull(DataSet.Tables("TransRecibida").Rows(Iposicion)("Cantidad")) Then
                CantTransRecibida = Trim(DataSet.Tables("TransRecibida").Rows(Iposicion)("Cantidad"))
                Importe = Trim(DataSet.Tables("TransRecibida").Rows(Iposicion)("Importe"))
                ImporteD = Trim(DataSet.Tables("TransRecibida").Rows(Iposicion)("ImporteD"))
            End If
            TotalTransRecibida = TotalTransRecibida + Importe
            TotalTransRecibidaD = TotalTransRecibidaD + ImporteD
            Iposicion = Iposicion + 1
        Loop
        DataSet.Tables("TransRecibida").Reset()

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE VENTAS EN CORDOBAS Y DOLARES ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) AS Costo,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Facturas.Fecha_Factura <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Factura') AND (Facturas.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario <> 0)
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Ventas")
        Registros = DataSet.Tables("Ventas").Rows.Count - 1
        Iposicion = 0
        CantVentas = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Ventas").Rows.Count
            If Not IsDBNull(DataSet.Tables("Ventas").Rows(Iposicion)("Cantidad")) Then
                CantVentas = Trim(DataSet.Tables("Ventas").Rows(Iposicion)("Cantidad"))
                Importe = Trim(DataSet.Tables("Ventas").Rows(Iposicion)("Costo"))
                ImporteD = Trim(DataSet.Tables("Ventas").Rows(Iposicion)("CostoD"))
                TotalVentas = TotalVentas + Importe
                TotalVentasD = TotalVentasD + ImporteD
            End If

            Iposicion = Iposicion + 1
        Loop
        DataSet.Tables("Ventas").Reset()

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE SALIDAS////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Costo,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Facturas.Fecha_Factura <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Salida Bodega') AND (Facturas.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario <> 0)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "Salida")
        Registros = DataSet.Tables("Salida").Rows.Count - 1
        Iposicion = 0
        CantSalida = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("Salida").Rows.Count
            If Not IsDBNull(DataSet.Tables("Salida").Rows(Iposicion)("Cantidad")) Then
                CantSalida = Trim(DataSet.Tables("Salida").Rows(Iposicion)("Cantidad"))
                Importe = Trim(DataSet.Tables("Salida").Rows(Iposicion)("Costo"))
                ImporteD = Trim(DataSet.Tables("Salida").Rows(Iposicion)("CostoD"))
                TotalSalidaBodega = TotalSalidaBodega + Importe
                TotalSalidaBodegaD = TotalSalidaBodegaD + ImporteD
            End If

            Iposicion = Iposicion + 1
        Loop
        DataSet.Tables("Salida").Reset()


        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////BUSCO EL TOTAL DE DEVOLUCIONES VENTAS EN CORDOBAS Y DOLARES ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) AS Costo,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Facturas.Fecha_Factura <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Devolucion de Venta') AND (Facturas.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario <> 0)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "DevVentas")
        Registros = DataSet.Tables("DevVentas").Rows.Count - 1
        Iposicion = 0
        CantDevVenta = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("DevVentas").Rows.Count
            If Not IsDBNull(DataSet.Tables("DevVentas").Rows(Iposicion)("Cantidad")) Then
                CantDevVenta = Trim(DataSet.Tables("DevVentas").Rows(Iposicion)("Cantidad"))
                Importe = Trim(DataSet.Tables("DevVentas").Rows(Iposicion)("Costo"))
                ImporteD = Trim(DataSet.Tables("DevVentas").Rows(Iposicion)("CostoD"))
                TotalDevVentas = TotalDevVentas + Importe
                TotalDevVentasD = TotalDevVentasD + ImporteD
            End If

            Iposicion = Iposicion + 1
        Loop
        DataSet.Tables("DevVentas").Reset()

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////BUSCO EL TOTAL TRANSFERENCIAS ENVIADAS ////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlCompras = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Costo,SUM((Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) / TasaCambio.MontoTasa) AS CostoD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                     "WHERE (Detalle_Facturas.Fecha_Factura <= CONVERT(DATETIME, '" & Format(FechaCompras, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Transferencia Enviada') AND (Facturas.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario <> 0)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "TransEnviada")
        Registros = DataSet.Tables("TransEnviada").Rows.Count - 1
        Iposicion = 0
        CantTransEnviada = 0
        Importe = 0
        ImporteD = 0
        Do While Iposicion < DataSet.Tables("TransEnviada").Rows.Count
            If Not IsDBNull(DataSet.Tables("TransEnviada").Rows(Iposicion)("Cantidad")) Then
                CantTransEnviada = Trim(DataSet.Tables("TransEnviada").Rows(Iposicion)("Cantidad"))
                Importe = Trim(DataSet.Tables("TransEnviada").Rows(Iposicion)("Costo"))
                ImporteD = Trim(DataSet.Tables("TransEnviada").Rows(Iposicion)("CostoD"))
                TotalTransEnviada = TotalTransEnviada + Importe
                TotalTransEnviadaD = TotalTransEnviadaD + ImporteD
            End If

            Iposicion = Iposicion + 1
        Loop
        DataSet.Tables("TransEnviada").Reset()


        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////FORMULA COSTO PROMEDIO /////////////////////////////////////////////////////
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        'CostoPromedio =  TotalCompras+TotalDevVenas-TotalVentas-TotalDevCompra
        '                 ------------------------------------------------------
        '                 UnidCompra+UnidadDevVentas-UnidadVentas-UnidadDevCompra

        'TotalCantidad = CantidadCompras + CantDevVenta + CantTransRecibida - CantVentas - CantDevCompra - CantSalida
        'TotalImporte = TotalCompras + TotalDevVentas + TotalTransRecibida - TotalVentas - TotalDevCompras - TotalSalidaBodega
        'TotalImporteD = TotalComprasD + TotalDevVentasD - TotalVentasD - TotalDevComprasD - TotalSalidaBodegaD


        '/////////////////////////////////ENTRADAS DE BODEGA ///////////////////////////////////////////
        TotalCantEntradas = 0
        TotalImporteEntradas = 0
        TotalImporteEntradasD = 0

        If TotalCompras <> 0 Then
            If CantidadCompras <> 0 Then
                TotalCantEntradas = TotalCantEntradas + CantidadCompras
                TotalImporteEntradas = TotalImporteEntradas + TotalCompras
                TotalImporteEntradasD = TotalImporteEntradasD + TotalComprasD
            End If
        End If

        If TotalDevVentas <> 0 Then
            If CantDevVenta <> 0 Then
                TotalCantEntradas = TotalCantEntradas + CantDevVenta
                TotalImporteEntradas = TotalImporteEntradas + TotalDevVentas
                TotalImporteEntradasD = TotalImporteEntradasD + TotalDevVentasD
            End If
        End If

        If TotalTransRecibida <> 0 Then
            If CantTransRecibida <> 0 Then
                TotalCantEntradas = TotalCantEntradas + CantTransRecibida
                TotalImporteEntradas = TotalImporteEntradas + TotalTransRecibida
                TotalImporteEntradasD = TotalImporteEntradasD + TotalTransRecibidaD
            End If
        End If



        '///////////////////////////SALIDAS DE BODEGAS /////////////////////////////////
        TotalCantSalidas = 0
        TotalImporteSalidas = 0
        TotalImporteSalidasD = 0

        If TotalVentas <> 0 Then
            If CantVentas <> 0 Then
                TotalCantSalidas = TotalCantSalidas + CantVentas
                TotalImporteSalidas = TotalImporteSalidas + TotalVentas
                TotalImporteSalidasD = TotalImporteSalidasD + TotalVentasD
            End If
        End If

        If TotalDevCompras <> 0 Then
            If CantDevCompra <> 0 Then
                TotalCantSalidas = TotalCantSalidas + CantDevCompra
                TotalImporteSalidas = TotalImporteSalidas + TotalDevCompras
                TotalImporteSalidasD = TotalImporteSalidasD + TotalDevComprasD
            End If
        End If

        If TotalTransEnviada <> 0 Then
            If CantTransEnviada <> 0 Then
                TotalCantSalidas = TotalCantSalidas + CantTransEnviada
                TotalImporteSalidas = TotalImporteSalidas + TotalTransEnviada
                TotalImporteSalidasD = TotalImporteSalidasD + TotalTransEnviadaD
            End If
        End If

        If TotalSalidaBodega <> 0 Then
            If CantSalida <> 0 Then
                TotalCantSalidas = TotalCantSalidas + CantSalida
                TotalImporteSalidas = TotalImporteSalidas + TotalSalidaBodega
                TotalImporteSalidasD = TotalImporteSalidasD + TotalSalidaBodegaD
            End If
        End If



        TotalCantidad = TotalCantEntradas - TotalCantSalidas
        TotalImporte = TotalImporteEntradas - TotalImporteSalidas
        TotalImporteD = TotalImporteEntradasD - TotalImporteSalidasD



        TasaCambio = BuscaTasaCambio(FechaCompras)

        If TotalCantidad <> 0 Then
            PrecioCosto = Format(TotalImporte / TotalCantidad, "##,##0.000000")
            PrecioCostoDolar = Format((TotalImporteD / TotalCantidad), "##,##0.000000")
        Else
            'PrecioCosto = PrecioUnitario
            'If PrecioCosto <> 0 Then
            '    PrecioCostoDolar = Format((TotalImporteD / TotalCantidad), "##,##0.000000")
            'Else
            '    PrecioCostoDolar = 0
            'End If
            PrecioCosto = 0
            PrecioCostoDolar = 0
        End If

        CostoPromedioActualizaBodega = PrecioCosto
        CostoPromedioDolar = PrecioCostoDolar

        DataSet.Tables("Compras").Reset()
    End Function


    Public Function Bitacora(ByVal FechaRegistro As Date, ByVal NombreUsuario As String, ByVal Modulo As String, ByVal Accion As String) As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, StrSQLUpdate As String = ""

        MiConexion.Close()
        '///////////ACTUALIZO LA EXISTENCIA PARA CADA BODEGA ////////////////////////////////////////
        StrSQLUpdate = "INSERT INTO [Bitacora] ([FechaRegistro],[Hora],[NombreUsuario],[Modulo],[Accion]) " & _
                       "VALUES ('" & Format(FechaRegistro, "dd/MM/yyyy") & "','" & Format(CDate(FechaRegistro), "HH:MM:ss") & "','" & NombreUsuario & "','" & Modulo & "','" & Mid(Accion, 1, 100) & "')"
        MiConexion.Open()
        ComandoUpdate = New SqlClient.SqlCommand(StrSQLUpdate, MiConexion)
        iResultado = ComandoUpdate.ExecuteNonQuery
        MiConexion.Close()

        Bitacora = "Grabado"
    End Function


    Public Sub ActualizaExistencia(ByVal CodigoProducto As String)
        Dim Existencia As Double, iPosicionFila As Double = 0, CodigoBodega As String = "", StrSQLUpdate As String = ""
        Dim ExistenciaBodega As Double, SqlString As String
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet
        Dim DataAdapter As New SqlClient.SqlDataAdapter


        '//////////////////////////////////////////////////////////////////////////////////////////////
        '///////////////BUSCO LAS BODEGAS DEL PRODUCTO/////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////

        SqlString = "SELECT  DetalleBodegas.Cod_Bodegas, Bodegas.Nombre_Bodega,DetalleBodegas.Existencia FROM DetalleBodegas INNER JOIN Bodegas ON DetalleBodegas.Cod_Bodegas = Bodegas.Cod_Bodega  " & _
                    "WHERE (DetalleBodegas.Cod_Productos = '" & CodigoProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Bodegas")
        '//////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////BUSCO LA EXISTENCIA DE ESTE PRODUCTO PARA CADA BODEGA//////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////
        Existencia = 0
        iPosicionFila = 0
        Do While iPosicionFila < (DataSet.Tables("Bodegas").Rows.Count)
            My.Application.DoEvents()
            CodigoBodega = DataSet.Tables("Bodegas").Rows(iPosicionFila)("Cod_Bodegas")

            ExistenciaBodega = BuscaExistenciaBodega(CodigoProducto, CodigoBodega)
            Existencia = Existencia + ExistenciaBodega
            MiConexion.Close()
            '///////////ACTUALIZO LA EXISTENCIA PARA CADA BODEGA ////////////////////////////////////////
            StrSQLUpdate = "UPDATE [DetalleBodegas]  SET [Existencia] = '" & ExistenciaBodega & "'  WHERE (Cod_Productos = '" & CodigoProducto & "') AND (Cod_Bodegas = '" & CodigoBodega & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(StrSQLUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()


            iPosicionFila = iPosicionFila + 1
        Loop
    End Sub


    Public Function CostoBodega(ByVal CodigoProducto As String, ByVal CodBodega As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0

        DataSet.Reset()
        SqlConsulta = "SELECT   *  FROM DetalleBodegas WHERE (Cod_Bodegas = '" & CodBodega & "') AND (Cod_Productos = '" & CodigoProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "ConsultaBodega")


        If Not DataSet.Tables("ConsultaBodega").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("ConsultaBodega").Rows(0)("Costo")) Then
                CostoBodega = DataSet.Tables("ConsultaBodega").Rows(0)("Costo")
            End If
        End If

    End Function



    Public Sub EscribirArchivo()
        Dim Ruta As String, Cadena As String
        Dim SqlDatos As String, ProveedorImprime As String = ""
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)



        SqlDatos = "SELECT * FROM DatosEmpresa"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlDatos, MiConexion)
        DataAdapter.Fill(DataSet, "DatosEmpresa")
        If Not DataSet.Tables("DatosEmpresa").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("DatosEmpresa").Rows(0)("ConexionImpresionSQL")) Then
                ProveedorImprime = DataSet.Tables("DatosEmpresa").Rows(0)("ConexionImpresionSQL")
            Else
                ProveedorImprime = "Provider=SQLOLEDB.1"
            End If
        End If

        Ruta = My.Application.Info.DirectoryPath & "\SysInfo.txt"
        '////////////////////////////////////////////////ESCRIBO EL ARCHIVO /////////////////////////////////////////////////////////

        Cadena = ProveedorImprime & ";" & Conexion
        'Cadena = Conexion

        My.Computer.FileSystem.WriteAllText(Ruta, Cadena, False)

        '///////////////////////////////////////////////LEELO EL ARCHIVO ///////////////////////////////////////////////////////////
        'fileReader = My.Computer.FileSystem.ReadAllText(Ruta)

    End Sub



    Public Sub ActualizaBodegas()
        Dim CodigoBodega As String, SQlString As String, SQL As String
        Dim DataAdapter As New SqlClient.SqlDataAdapter
        Dim DataSetDetalle As New DataSet
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)

        CodigoBodega = FrmBodegas.CboCodigoBodega.Text
        DataSetDetalle.Reset()
        SQL = "SELECT *  FROM Bodegas WHERE (Cod_Bodega = '" & CodigoBodega & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SQL, MiConexion)
        DataAdapter.Fill(DataSetDetalle, "Consulta")


        If Not DataSetDetalle.Tables("Consulta").Rows.Count = 0 Then

            FrmBodegas.TxtNombre.Text = DataSetDetalle.Tables("Consulta").Rows(0)("Nombre_Bodega")
            SQlString = "SELECT  DetalleBodegas.Cod_Productos, Productos.Descripcion_Producto FROM DetalleBodegas INNER JOIN Productos ON DetalleBodegas.Cod_Productos = Productos.Cod_Productos WHERE (DetalleBodegas.Cod_Bodegas = '" & CodigoBodega & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)

            DataAdapter.Fill(DataSetDetalle, "DetalleBodegas")
            FrmBodegas.BindingBodega.DataSource = DataSetDetalle.Tables("DetalleBodegas")
            FrmBodegas.TrueDBGridConsultas.DataSource = FrmBodegas.BindingBodega
            FrmBodegas.CboCodigoBodega.Columns(0).Caption = "Codigo"
            FrmBodegas.CboCodigoBodega.Columns(1).Caption = "Nombre Bodega"
            FrmBodegas.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(1).Width = 200

        Else
            FrmBodegas.TxtNombre.Text = ""
            SQlString = "SELECT  DetalleBodegas.Cod_Productos, Productos.Descripcion_Producto FROM DetalleBodegas INNER JOIN Productos ON DetalleBodegas.Cod_Productos = Productos.Cod_Productos WHERE (DetalleBodegas.Cod_Bodegas = '" & CodigoBodega & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)

            DataAdapter.Fill(DataSetDetalle, "DetalleBodegas")
            FrmBodegas.BindingBodega.DataSource = DataSetDetalle.Tables("DetalleBodegas")
            FrmBodegas.TrueDBGridConsultas.DataSource = FrmBodegas.BindingBodega
            FrmBodegas.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(1).Width = 200
        End If


    End Sub


    Public Sub ActualizaBodegaProducto()
        Dim SqlString As String
        Dim DataAdapter As New SqlClient.SqlDataAdapter
        Dim DataSetDetalle As New DataSet
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)

        SqlString = "SELECT  DetalleBodegas.Cod_Bodegas, Bodegas.Nombre_Bodega FROM DetalleBodegas INNER JOIN Bodegas ON DetalleBodegas.Cod_Bodegas = Bodegas.Cod_Bodega  " & _
            "WHERE (DetalleBodegas.Cod_Productos = '" & My.Forms.FrmProductos.CboCodigoProducto.Text & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSetDetalle, "Bodegas")
        FrmProductos.BindingBodegas.DataSource = DataSetDetalle.Tables("Bodegas")
        FrmProductos.TrueDBGridConsultas.DataSource = FrmProductos.BindingBodegas
        FrmProductos.TrueDBGridConsultas.Splits.Item(0).DisplayColumns(1).Width = 160
    End Sub

    Public Sub ActualizaComboProducto()
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapterProductos As New SqlClient.SqlDataAdapter, SqlProductos As String


        SqlProductos = "SELECT Cod_Productos, Descripcion_Producto FROM Productos"
        DataAdapterProductos = New SqlClient.SqlDataAdapter(SqlProductos, MiConexion)
        DataAdapterProductos.Fill(DataSet, "ListaProductos")
        If Not DataSet.Tables("ListaProductos").Rows.Count = 0 Then
            FrmProductos.CboCodigoProducto.DataSource = DataSet.Tables("ListaProductos")
        End If
    End Sub

    Public Sub ActualizaComponenteProducto(ByVal CodComponente As String)
        Dim SqlString As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        '//////////////////////////////////////////////////////////////////////////////////////////////
        '///////////////BUSCO LOS COMPONENTES DEL PRODUCTO/////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////

        SqlString = "SELECT *  FROM Componentes WHERE (Cod_Componente = '" & CodComponente & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Componentes")
        FrmProductos.BindingComponentes.DataSource = DataSet.Tables("Componentes")
        FrmProductos.TrueDBGridComponentes.DataSource = FrmProductos.BindingComponentes

        FrmProductos.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Visible = False
        FrmProductos.TrueDBGridComponentes.Columns(1).Caption = "Componente"
        FrmProductos.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Width = 74
        FrmProductos.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Locked = True
        FrmProductos.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Width = 182
        FrmProductos.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Locked = True
        FrmProductos.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Width = 62
        FrmProductos.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Width = 68
        FrmProductos.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Width = 74
    End Sub

    Public Sub ActualizaComponenteEnsamble(ByVal CodComponente As String, ByVal Cantidad As Double)
        Dim SqlString As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        '//////////////////////////////////////////////////////////////////////////////////////////////
        '///////////////BUSCO LOS COMPONENTES DEL PRODUCTO/////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////

        SqlString = "SELECT  Cod_Componente, Cod_Producto, Descripcion_Producto, Requerido * " & Cantidad & " AS Requerido, Recuperable, Desecho * " & Cantidad & " AS Desecho  FROM Componentes WHERE (Cod_Componente = '" & CodComponente & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Componentes")
        FrmEnsamble.BindingComponentes.DataSource = DataSet.Tables("Componentes")
        FrmEnsamble.TrueDBGridComponentes.DataSource = FrmEnsamble.BindingComponentes

        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Visible = False
        FrmEnsamble.TrueDBGridComponentes.Columns(1).Caption = "Componente"
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Width = 74
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Locked = True
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Width = 250
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Locked = True
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Width = 62
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Width = 68
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Visible = False
        FrmEnsamble.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Width = 74
    End Sub
    Public Function ConsultaConsecutivo(ByVal NombreCampo As String) As Double

        Dim SqlConsecutivo As String, CodConsecutivo As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim ComandoUpdate As New SqlClient.SqlCommand, FacturaBodega As Boolean = False, CompraBodega As Boolean = False

        '/////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////BUSCO SI TIENE ACTIVADA LA OPCION DE CONSECUTIVO X BODEGA /////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////
        SqlConsecutivo = "SELECT * FROM  DatosEmpresa"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsecutivo, MiConexion)
        DataAdapter.Fill(DataSet, "Configuracion")
        If Not DataSet.Tables("Configuracion").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacBodega")) = True Then
                FacturaBodega = True
            End If

            If Not IsDBNull(DataSet.Tables("Configuracion").Rows(0)("ConsecutivoComBodega")) = True Then
                CompraBodega = True
            End If

        End If


        '/////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////BUSCO EL CONSECUTIVO COMPONENTES/////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////
        SqlConsecutivo = "SELECT  * FROM  Consecutivos"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsecutivo, MiConexion)
        DataAdapter.Fill(DataSet, "Consecutivo")
        If Not DataSet.Tables("Consecutivo").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Consecutivo").Rows(0)(NombreCampo)) Then
                CodConsecutivo = DataSet.Tables("Consecutivo").Rows(0)(NombreCampo) + 1
            Else
                CodConsecutivo = 1
            End If
            ConsultaConsecutivo = CodConsecutivo

        Else
            ConsultaConsecutivo = 0
        End If


    End Function
    Public Function BuscaConsecutivoSerie(ByVal NombreCampo As String, ByVal TipoSerie As String) As Double

        Dim SqlConsecutivo As String, SQlUpdate As String, CodConsecutivo As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, FacturaBodega As Boolean = False, CompraBodega As Boolean = False


        '/////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////BUSCO EL CONSECUTIVO COMPONENTES/////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////

        SqlConsecutivo = "SELECT  * FROM ConsecutivoSerie WHERE (Serie = '" & TipoSerie & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsecutivo, MiConexion)
        DataAdapter.Fill(DataSet, "Consecutivo")
        If Not DataSet.Tables("Consecutivo").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Consecutivo").Rows(0)(NombreCampo)) Then
                CodConsecutivo = DataSet.Tables("Consecutivo").Rows(0)(NombreCampo) + 1
            Else
                CodConsecutivo = 1
            End If
            BuscaConsecutivoSerie = CodConsecutivo

            '//////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////ACTUALIZO EL CONSECUTIVO///////////////////////////////////////////////////
            '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            MiConexion.Close()
            SQlUpdate = "UPDATE [ConsecutivoSerie]  SET [" & NombreCampo & "] = " & CodConsecutivo & " WHERE (Serie = '" & TipoSerie & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SQlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            BuscaConsecutivoSerie = 0
        End If

    End Function
    Public Function BuscaConsecutivoSerieNoEdita(ByVal NombreCampo As String, ByVal TipoSerie As String) As Double

        Dim SqlConsecutivo As String, SQlUpdate As String, CodConsecutivo As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, FacturaBodega As Boolean = False, CompraBodega As Boolean = False


        '/////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////BUSCO EL CONSECUTIVO COMPONENTES/////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////

        SqlConsecutivo = "SELECT  * FROM ConsecutivoSerie WHERE (Serie = '" & TipoSerie & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsecutivo, MiConexion)
        DataAdapter.Fill(DataSet, "Consecutivo")
        If Not DataSet.Tables("Consecutivo").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Consecutivo").Rows(0)(NombreCampo)) Then
                CodConsecutivo = DataSet.Tables("Consecutivo").Rows(0)(NombreCampo) + 1
            Else
                CodConsecutivo = 1
            End If
            BuscaConsecutivoSerieNoEdita = CodConsecutivo

        Else
            BuscaConsecutivoSerieNoEdita = 0
        End If

    End Function




    Public Function BuscaConsecutivo(ByVal NombreCampo As String) As Double

        Dim SqlConsecutivo As String, SQlUpdate As String, CodConsecutivo As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, FacturaBodega As Boolean = False, CompraBodega As Boolean = False

        '/////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////BUSCO SI TIENE ACTIVADA LA OPCION DE CONSECUTIVO X BODEGA /////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////
        SqlConsecutivo = "SELECT * FROM  DatosEmpresa"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsecutivo, MiConexion)
        DataAdapter.Fill(DataSet, "Configuracion")
        If Not DataSet.Tables("Configuracion").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Configuracion").Rows(0)("ConsecutivoFacBodega")) = True Then
                FacturaBodega = True
            End If

            If Not IsDBNull(DataSet.Tables("Configuracion").Rows(0)("ConsecutivoComBodega")) = True Then
                CompraBodega = True
            End If

        End If


        '/////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////BUSCO EL CONSECUTIVO COMPONENTES/////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////

        SqlConsecutivo = "SELECT  * FROM  Consecutivos"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsecutivo, MiConexion)
        DataAdapter.Fill(DataSet, "Consecutivo")
        If Not DataSet.Tables("Consecutivo").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Consecutivo").Rows(0)(NombreCampo)) Then
                CodConsecutivo = DataSet.Tables("Consecutivo").Rows(0)(NombreCampo) + 1
            Else
                CodConsecutivo = 1
            End If
            BuscaConsecutivo = CodConsecutivo

            '//////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////ACTUALIZO EL CONSECUTIVO///////////////////////////////////////////////////
            '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            MiConexion.Close()
            SQlUpdate = "UPDATE [Consecutivos]  SET [" & NombreCampo & "] = " & CodConsecutivo & ""
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SQlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            BuscaConsecutivo = 0
        End If

    End Function
    Public Function BuscaConsecutivoNoEdita(ByVal NombreCampo As String) As Double

        Dim SqlConsecutivo As String, CodConsecutivo As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim ComandoUpdate As New SqlClient.SqlCommand
        '/////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////BUSCO EL CONSECUTIVO COMPONENTES/////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////

        SqlConsecutivo = "SELECT  * FROM  Consecutivos"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsecutivo, MiConexion)
        DataAdapter.Fill(DataSet, "Consecutivo")
        If Not DataSet.Tables("Consecutivo").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Consecutivo").Rows(0)(NombreCampo)) Then
                CodConsecutivo = DataSet.Tables("Consecutivo").Rows(0)(NombreCampo) + 1
            Else
                CodConsecutivo = 1
            End If
            BuscaConsecutivoNoEdita = CodConsecutivo

        Else
            BuscaConsecutivoNoEdita = 0
        End If

    End Function



    Public Sub ActualizaMETODOcOMPRA()
        Dim Metodo As String, iPosicion As Double, Registros As Double, Monto As Double
        Dim Subtotal As Double, Iva As Double, Neto As Double, CodProducto As String, SQlString As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), CodIva As String, Tasa As Double, Moneda As String, Fecha As String, SQLTasa As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, TasaCambio As Double, TipoMetodo As String, SQLMetodo As String, TasaIva As Double = 0


        Registros = FrmCompras.BindingMetodo.Count
        iPosicion = 0

        Do While iPosicion < Registros
            Metodo = FrmCompras.BindingMetodo.Item(iPosicion)("NombrePago")
            TasaCambio = 1
            Fecha = Format(FrmFacturas.DTPFecha.Value, "yyyy-MM-dd")
            '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '//////////////////////////////BUSCO LA MONEDA DEL METODO DE PAGO///////////////////////////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Moneda = "Cordobas"
            TipoMetodo = "Cambio"
            SQLMetodo = "SELECT * FROM MetodoPago WHERE (NombrePago = '" & Metodo & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SQLMetodo, MiConexion)
            DataAdapter.Fill(DataSet, "Metodo")
            If DataSet.Tables("Metodo").Rows.Count <> 0 Then
                Moneda = DataSet.Tables("Metodo").Rows(0)("Moneda")
                TipoMetodo = DataSet.Tables("Metodo").Rows(0)("TipoPago")
            End If
            DataSet.Tables("Metodo").Clear()


            Select Case Moneda
                Case "Cordobas"
                    TasaCambio = 1

                Case "Dolares"
                    SQLTasa = "SELECT  * FROM TasaCambio WHERE (FechaTasa = CONVERT(DATETIME, '" & Fecha & "', 102))"
                    DataAdapter = New SqlClient.SqlDataAdapter(SQLTasa, MiConexion)
                    DataAdapter.Fill(DataSet, "TasaCambio")
                    If DataSet.Tables("TasaCambio").Rows.Count <> 0 Then
                        TasaCambio = DataSet.Tables("TasaCambio").Rows(0)("MontoTasa")
                    Else
                        'TasaCambio = 0
                        MsgBox("La Tasa de Cambio no Existe para esta Fecha", MsgBoxStyle.Critical, "Sistema Facturacion")
                        FrmFacturas.BindingMetodo.Item(iPosicion)("Monto") = 0
                    End If
                    DataSet.Tables("TasaCambio").Clear()
            End Select

            If TipoMetodo = "Cambio" Then
                TasaCambio = TasaCambio * -1
            End If





            If Not IsDBNull(FrmCompras.BindingMetodo.Item(iPosicion)("Monto")) Then
                Monto = (FrmCompras.BindingMetodo.Item(iPosicion)("Monto") * TasaCambio) + Monto
                iPosicion = iPosicion + 1
            Else
                Monto = 0
                iPosicion = iPosicion + 1
            End If
        Loop


        Registros = FrmCompras.BindingDetalle.Count
        iPosicion = 0

        Do While iPosicion < Registros
            If Not IsDBNull(FrmCompras.BindingDetalle.Item(iPosicion)("Importe")) Then
                Subtotal = CDbl(FrmCompras.BindingDetalle.Item(iPosicion)("Importe")) + Subtotal
            End If

            If IsDBNull(FrmCompras.BindingDetalle.Item(iPosicion)("Cod_Producto")) Then
                Exit Sub
            End If

            CodProducto = FrmCompras.BindingDetalle.Item(iPosicion)("Cod_Producto")
            'CodProducto = FrmCompras.TrueDBGridComponentes.Columns(0).Text
            SQlString = "SELECT Productos.*  FROM Productos WHERE (Cod_Productos = '" & CodProducto & "') "
            DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
            DataAdapter.Fill(DataSet, "Productos")
            If Not DataSet.Tables("Productos").Rows.Count = 0 Then
                CodIva = DataSet.Tables("Productos").Rows(0)("Cod_Iva")
                SQlString = "SELECT *  FROM Impuestos WHERE  (Cod_Iva = '" & CodIva & "')"
                DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
                DataAdapter.Fill(DataSet, "IVA")
                If Not DataSet.Tables("IVA").Rows.Count = 0 Then
                    Tasa = DataSet.Tables("IVA").Rows(0)("Impuesto")
                    TasaIva = DataSet.Tables("IVA").Rows(0)("Impuesto")
                End If
                DataSet.Tables("IVA").Reset()
            End If
            DataSet.Tables("Productos").Reset()

            If Not IsDBNull(FrmCompras.BindingDetalle.Item(iPosicion)("Importe")) Then
                Iva = Format(Iva + CDbl(FrmCompras.BindingDetalle.Item(iPosicion)("Importe")) * Tasa, "####0.00000000")
            Else
                Iva = 0
            End If
            iPosicion = iPosicion + 1




        Loop


        If FrmCompras.OptExsonerado.Checked = False Then
            'Iva = Subtotal * Tasa
        Else
            Iva = 0
        End If

        'Iva = Subtotal * Tasa
        Neto = (Subtotal + Iva) - Monto
        FrmCompras.TxtSubTotal.Text = Format(Subtotal, "##,##0.00")
        FrmCompras.TxtIva.Text = Format(Iva, "##,##0.00")
        FrmCompras.TxtPagado.Text = Format(Monto, "##,##0.00")
        FrmCompras.TxtNetoPagar.Text = Format(Neto, "##,##0.00")
    End Sub
    Public Sub GrabaDetalleNotaDebito(ByVal ConsecutivoNotaDebito As String, ByVal FechaNota As Date, ByVal TipoNota As String, ByVal Descripcion As String, ByVal NumeroFactura As String, ByVal Monto As Double)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, Fecha As String
        Dim SqlString As String, MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter

        Fecha = Format(FechaNota, "yyyy-MM-dd")

        SqlString = "SELECT * FROM Detalle_Nota WHERE (Numero_Nota = '" & ConsecutivoNotaDebito & "') AND (Fecha_Nota = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Nota = '" & TipoNota & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "NotaDebito")
        If DataSet.Tables("NotaDebito").Rows.Count = 0 Then

            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "INSERT INTO [Detalle_Nota] ([Numero_Nota],[Fecha_Nota],[Tipo_Nota],[Descripcion],[Numero_Factura],[Monto]) " & _
                         "VALUES ('" & ConsecutivoNotaDebito & "','" & Format(FechaNota, "dd/MM/yyyy") & "','" & TipoNota & "','" & Descripcion & "','" & NumeroFactura & "'," & Monto & ")"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "UPDATE [Detalle_Nota]  SET [Tipo_Nota] = '" & TipoNota & "' ,[Descripcion] = '" & Descripcion & "',[Monto] =" & Monto & "  WHERE (Numero_Nota = '" & ConsecutivoNotaDebito & "') AND (Fecha_Nota = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Nota = '" & TipoNota & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If
    End Sub

    Public Sub GrabaNotaDebito(ByVal ConsecutivoNotaDebito As String, ByVal FechaNota As Date, ByVal TipoNota As String, ByVal CuentaContable As String, ByVal Moneda As String, ByVal CodigoCliente As String, ByVal NombreCliente As String, ByVal Observaciones As String, ByVal Activo As Boolean, ByVal Contabilizado As Boolean, ByVal TipoCuenta As Boolean)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, Fecha As String
        Dim SqlString As String, MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim NotaActivo As Integer, NotaContabilizado As Integer

        If Activo = True Then
            NotaActivo = 1
        Else
            NotaActivo = 0
        End If

        If Contabilizado = True Then
            NotaContabilizado = 1
        Else
            NotaContabilizado = 0
        End If

        Fecha = Format(FechaNota, "yyyy-MM-dd")

        SqlString = "SELECT  *  FROM IndiceNota WHERE (Numero_Nota = '" & ConsecutivoNotaDebito & "') AND (Fecha_Nota = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Nota = '" & TipoNota & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "NotaDebito")
        If DataSet.Tables("NotaDebito").Rows.Count = 0 Then

            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "INSERT INTO [IndiceNota] ([Numero_Nota],[Fecha_Nota],[Tipo_Nota],[MonedaNota],[Cod_Cliente],[Nombre_Cliente],[Observaciones],[Marca]) " & _
                         "VALUES ('" & ConsecutivoNotaDebito & "','" & Format(FechaNota, "dd/MM/yyyy") & "','" & TipoNota & "','" & Moneda & "','" & CodigoCliente & "','" & NombreCliente & "' ,'" & Observaciones & "','True')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "UPDATE [IndiceNota] SET [Fecha_Nota] = '" & Format(FechaNota, "dd/MM/yyyy") & "',[Tipo_Nota] = '" & TipoNota & "',[MonedaNota] = '" & Moneda & "',[Cod_Cliente] = '" & CodigoCliente & "',[Nombre_Cliente] = '" & NombreCliente & "' ,[Observaciones] = '" & Observaciones & "' ,[Activo] =" & NotaActivo & ",[Contabilizado] =" & NotaContabilizado & ",[Marca] ='True',[TipoCuenta] =" & TipoCuenta & " " & _
                         "WHERE (Numero_Nota = '" & ConsecutivoNotaDebito & "') AND (Fecha_Nota = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Nota = '" & TipoNota & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If
    End Sub


    Public Sub GrabaCompras(ByVal ConsecutivoCompra As String)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, MonedaCompra As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Subtotal As Double, Iva As Double, Neto As Double, Pagado As Double, Exonerado As Integer
        Dim SuReferencia As String = "", CodigoProyecto As String, Referencia As String = "", FechaHora As Date


        '**********************************************************************************************************************
        '///////////CON ESTA CONSULTA BUSCO LOS TOTALES DE CADA COMPRA ////////////////////////////////////////////////////////
        '**********************************************************************************************************************
        ActualizaMETODOcOMPRA()

        If FrmCompras.CboReferencia.Text <> "" Then
            Referencia = FrmCompras.CboReferencia.Text
        End If



        MonedaCompra = FrmCompras.TxtMonedaFactura.Text
        CodigoProyecto = ""
        If Not FrmCompras.CboProyecto.Text = "" Then
            CodigoProyecto = FrmCompras.CboProyecto.Columns(0).Text
        End If

        Fecha = Format(FrmCompras.DTPFecha.Value, "yyyy-MM-dd")

        If FrmCompras.TxtSubTotal.Text <> "" Then
            Subtotal = FrmCompras.TxtSubTotal.Text
        Else
            Subtotal = 0
        End If

        If FrmCompras.TxtReferencia.Text <> "" Then
            SuReferencia = FrmCompras.TxtReferencia.Text
        End If

        If FrmCompras.TxtIva.Text <> "" Then
            Iva = FrmCompras.TxtIva.Text
        Else
            Iva = 0
        End If

        If FrmCompras.TxtPagado.Text <> "" Then
            Pagado = FrmCompras.TxtPagado.Text
        Else
            Pagado = 0
        End If

        If FrmCompras.TxtNetoPagar.Text <> "" Then
            Neto = FrmCompras.TxtNetoPagar.Text
        Else
            Neto = 0
        End If

        If FrmCompras.OptExsonerado.Checked = True Then
            Exonerado = 1
        Else
            Exonerado = 0
        End If

        MiConexion.Close()

        FechaHora = FrmCompras.DTPFecha.Value & " " & Format(Now, "HH:mm")


        CambiarFechaCompra = False

        'If ExisteCompra(Fecha, ConsecutivoCompra, FrmCompras.CboTipoProducto.Text) = "ExisteCompraDifFecha" Then

        'If MsgBox("Existe Diferencia de Fechas, �Desea Grabar este Cambio?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

        '    CambiarFechaCompra = True
        '    '///////////////////////////////////////////////////////////////////////////////////////////////////////////
        '    '///////////////////////////////////BORRO EL METODO DE FACTURA /////////////////////////////////////////////
        '    '/////////////////////////////////////////////
        '    MiConexion.Close()
        '    SqlCompras = "DELETE FROM [Detalle_MetodoCompras] WHERE  (Numero_Compra = '" & ConsecutivoCompra & "') AND (Tipo_Compra = '" & FrmFacturas.CboTipoProducto.Text & "')"
        '    MiConexion.Open()
        '    ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
        '    iResultado = ComandoUpdate.ExecuteNonQuery
        '    MiConexion.Close()

        '    '///////////////////////////////////////////////////////////////////////////////////////////////////////////
        '    '///////////////////////////////////BORRO EL DETALLE DE FACTURA /////////////////////////////////////////////
        '    '/////////////////////////////////////////////
        '    MiConexion.Close()
        '    SqlCompras = "DELETE FROM [Detalle_Compras] WHERE  (Numero_Compra = '" & ConsecutivoCompra & "') AND (Tipo_Compra = '" & FrmCompras.CboTipoProducto.Text & "')"
        '    MiConexion.Open()
        '    ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
        '    iResultado = ComandoUpdate.ExecuteNonQuery
        '    MiConexion.Close()

        '    '///////////////////////////////////////////////////////////////////////////////////////////////////////////
        '    '///////////////////////////////////BORRO LA FACTURA /////////////////////////////////////////////
        '    '/////////////////////////////////////////////
        '    MiConexion.Close()
        '    SqlCompras = "DELETE FROM [Compras] WHERE  (Numero_Compra = '" & ConsecutivoCompra & "') AND (Tipo_Compra = '" & FrmCompras.CboTipoProducto.Text & "')"
        '    MiConexion.Open()
        '    ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
        '    iResultado = ComandoUpdate.ExecuteNonQuery
        '    MiConexion.Close()
        'End If


        'End If


        SqlCompras = "SELECT Compras.* FROM Compras WHERE  (Numero_Compra = '" & ConsecutivoCompra & "') AND (Tipo_Compra = '" & FrmCompras.CboTipoProducto.Text & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlCompras, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleCompra")
        If DataSet.Tables("DetalleCompra").Rows.Count = 0 Then

            FrmCompras.DTPFechaHora.Value = FechaHora
            'If FrmCompras.TxtNumeroEnsamble.Text = "-----0-----" Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "INSERT INTO [Compras] ([Numero_Compra] ,[Fecha_Compra],[Tipo_Compra],[Cod_Proveedor],[Cod_Bodega],[Nombre_Proveedor],[Apellido_Proveedor],[Direccion_Proveedor],[Telefono_Proveedor],[Fecha_Vencimiento],[Observaciones],[SubTotal],[IVA],[Pagado],[NetoPagar],[MontoCredito],[MonedaCompra],[Exonerado],[Su_Referencia],[CodigoProyecto],[Referencia],[FechaHora]) " & _
            "VALUES ('" & ConsecutivoCompra & "','" & FrmCompras.DTPFecha.Value & "','" & FrmCompras.CboTipoProducto.Text & "','" & FrmCompras.TxtCodigoProveedor.Text & "','" & FrmCompras.CboCodigoBodega.Text & "' , '" & FrmCompras.TxtNombres.Text & "','" & FrmCompras.TxtApellidos.Text & "','" & FrmCompras.TxtDireccion.Text & "','" & FrmCompras.TxtTelefono.Text & "','" & FrmCompras.DTVencimiento.Value & "','" & FrmCompras.TxtObservaciones.Text & "'," & Subtotal & "," & Iva & "," & Pagado & "," & Neto & "," & Neto & ",'" & MonedaCompra & "'," & Exonerado & ",'" & SuReferencia & "','" & CodigoProyecto & "','" & Referencia & "','" & Format(FechaHora, "dd/MM/yyyy HH:mm") & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "UPDATE [Compras]  SET [Cod_Proveedor] = '" & FrmCompras.TxtCodigoProveedor.Text & "',[Nombre_Proveedor] = '" & FrmCompras.TxtNombres.Text & "',[Apellido_Proveedor] = '" & FrmCompras.TxtApellidos.Text & "',[Direccion_Proveedor] = '" & FrmCompras.TxtDireccion.Text & "',[Telefono_Proveedor] = '" & FrmCompras.TxtTelefono.Text & "',[Cod_Bodega]='" & FrmCompras.CboCodigoBodega.Text & "',[Fecha_Vencimiento] = '" & FrmCompras.DTVencimiento.Value & "' ,[Observaciones] = '" & FrmCompras.TxtObservaciones.Text & "',[SubTotal] = " & Subtotal & ",[IVA] = " & Iva & ",[Pagado] = " & Pagado & ",[NetoPagar] = " & Neto & ",[MontoCredito] = " & Neto & ",[MonedaCompra] = '" & MonedaCompra & "',[Exonerado] = " & Exonerado & ",[Su_Referencia] = '" & SuReferencia & "',[CodigoProyecto] = '" & CodigoProyecto & "',[Referencia] = '" & Referencia & "'  " & _
                         "WHERE  (Numero_Compra = '" & ConsecutivoCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & FrmCompras.CboTipoProducto.Text & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If

    End Sub

    Public Sub GrabaEncabezadoCompras(ByVal ConsecutivoCompra As String, ByVal FechaCompra As String, ByVal TipoCompra As String, ByVal CodProveedor As String, ByVal CodBodega As String, ByVal Nombres As String, ByVal Apellidos As String, ByVal FechaVencimiento As String, ByVal SubTotal As Double, ByVal IVA As Double, ByVal Pagado As Double, ByVal Neto As Double, ByVal MonedaCompra As String, ByVal Observaciones As String)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Fecha As Date, FechaHora As Date

        Fecha = FechaCompra
        FechaCompra = Format(Fecha, "dd/MM/yyyy")
        Fecha = FechaVencimiento
        FechaVencimiento = Format(Fecha, "dd/MM/yyyy")
        FechaHora = Format(Fecha, "dd/MM/yyyy") & " " & Format(Now, "HH:mm")

        MiConexion.Close()


        If ConsecutivoCompra <> "-----0-----" Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "INSERT INTO [Compras] ([Numero_Compra] ,[Fecha_Compra],[Tipo_Compra],[Cod_Proveedor],[Cod_Bodega],[Nombre_Proveedor],[Apellido_Proveedor],[Fecha_Vencimiento],[Observaciones],[SubTotal],[IVA],[Pagado],[NetoPagar],[MontoCredito],[MonedaCompra],[FechaHora]) " & _
            "VALUES ('" & ConsecutivoCompra & "','" & FechaCompra & "','" & TipoCompra & "','" & CodProveedor & "','" & CodBodega & "' , '" & Nombres & "','" & Apellidos & "','" & FechaVencimiento & "','" & Observaciones & "'," & SubTotal & "," & IVA & "," & Pagado & "," & Neto & "," & Neto & ",'" & MonedaCompra & "','" & Format(FechaHora, "dd/MM/yyyy HH:mm") & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "UPDATE [Compras]  SET [Cod_Proveedor] = '" & CodProveedor & "',[Nombre_Proveedor] = '" & Nombres & "',[Apellido_Proveedor] = '" & Apellidos & "',[Fecha_Vencimiento] = '" & FechaVencimiento & "' ,[Observaciones] = '" & Observaciones & "',[SubTotal] = " & SubTotal & ",[IVA] = " & IVA & ",[Pagado] = " & Pagado & ",[NetoPagar] = " & Neto & ",[MontoCredito] = " & Neto & ",[MonedaCompra] = '" & MonedaCompra & "', [FechaHora]= '" & Format(FechaHora, "dd/MM/yyyy HH:mm") & "' " & _
                         "WHERE  (Numero_Compra = '" & ConsecutivoCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & FechaCompra & "', 102)) AND (Tipo_Compra = '" & TipoCompra & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If

    End Sub

    Public Sub GrabaEncabezadoFacturas(ByVal ConsecutivoFactura As String, ByVal FechaFactura As String, ByVal TipoFactura As String, ByVal CodCliente As String, ByVal CodBodega As String, ByVal Nombres As String, ByVal Apellidos As String, ByVal FechaVencimiento As String, ByVal SubTotal As Double, ByVal IVA As Double, ByVal Pagado As Double, ByVal Neto As Double, ByVal MonedaFactura As String, ByVal Observaciones As String)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Fecha As Date, FechaHora As Date

        Fecha = FechaFactura
        FechaFactura = Format(Fecha, "dd/MM/yyyy")
        Fecha = FechaVencimiento
        FechaVencimiento = Format(Fecha, "dd/MM/yyyy")
        FechaHora = Format(FrmCompras.DTPFecha.Value, "dd/MM/yyyy") & " " & Format(Now, "HH:mm")

        MiConexion.Close()


        If ConsecutivoFactura <> "-----0-----" Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "INSERT INTO [Facturas] ([Numero_Factura] ,[Fecha_Factura],[Tipo_Factura],[Cod_Cliente],[Cod_Bodega],[Nombre_Cliente],[Apellido_Cliente],[Fecha_Vencimiento],[Observaciones],[SubTotal],[IVA],[Pagado],[NetoPagar],[MontoCredito],[MonedaFactura],[FechaHora]) " & _
            "VALUES ('" & ConsecutivoFactura & "','" & FechaFactura & "','" & TipoFactura & "','" & CodCliente & "','" & CodBodega & "' , '" & Nombres & "','" & Apellidos & "','" & FechaVencimiento & "','" & Observaciones & "'," & SubTotal & "," & IVA & "," & Pagado & "," & Neto & "," & Neto & ",'" & MonedaFactura & "', '" & Format(FechaHora, "dd/MM/yyyy HH:mm") & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "UPDATE [Facturas]  SET [Cod_Cliente] = '" & CodCliente & "',[Nombre_Cliente] = '" & Nombres & "',[Apellido_Cliente] = '" & Apellidos & "',[Fecha_Vencimiento] = '" & FechaVencimiento & "' ,[Observaciones] = '" & Observaciones & "',[SubTotal] = " & SubTotal & ",[IVA] = " & IVA & ",[Pagado] = " & Pagado & ",[NetoPagar] = " & Neto & ",[MontoCredito] = " & Neto & ",[MonedaFactura] = '" & MonedaFactura & "', [FechaHora]='" & Format(FechaHora, "dd/MM/yyyy HH:mm") & "'  " & _
                         "WHERE  (Numero_Factura = '" & ConsecutivoFactura & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & FechaFactura & "', 102)) AND (Tipo_Compra = '" & TipoFactura & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If

    End Sub



    Public Sub GrabaDetalleCompra(ByVal ConsecutivoCompra As String, ByVal CodProducto As String, ByVal PrecioUnitario As Double, ByVal Descuento As Double, ByVal PrecioNeto As Double, ByVal Importe As Double, ByVal Cantidad As Double, ByVal Numero_Lote As String, ByVal Fecha_Lote As Date, ByVal DescripcionProducto As String)
        Dim Sqldetalle As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, TasaCambio As String
        Dim Fecha As String, MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, MonedaCompra As String, MonedaProducto As String


        MonedaCompra = FrmCompras.TxtMonedaFactura.Text
        MonedaProducto = "Cordobas"
        TasaCambio = 0



        If MonedaCompra = "Cordobas" Then
            If MonedaProducto = "Cordobas" Then
                TasaCambio = 1
            Else
                If BuscaTasaCambio(FrmFacturas.DTPFecha.Value) <> 0 Then
                    TasaCambio = (1 / BuscaTasaCambio(FrmCompras.DTPFecha.Value))
                End If
            End If
        ElseIf MonedaCompra = "Dolares" Then
            If MonedaProducto = "Cordobas" Then
                TasaCambio = BuscaTasaCambio(FrmCompras.DTPFecha.Value)
            Else
                TasaCambio = 1
            End If
        End If


        Fecha = Format(FrmCompras.DTPFecha.Value, "yyyy-MM-dd")

        Sqldetalle = "SELECT *  FROM Detalle_Compras WHERE (Numero_Compra = '" & ConsecutivoCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & FrmCompras.CboTipoProducto.Text & "') AND (Cod_Producto = '" & CodProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(Sqldetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleCompra")
        If Not DataSet.Tables("DetalleCompra").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlUpdate = "UPDATE [Detalle_Compras] SET [Cantidad] = " & Cantidad & " ,[Precio_Unitario] = " & PrecioUnitario & ",[Descuento] = " & Descuento & " ,[Precio_Neto] = " & PrecioNeto & ",[Importe] = " & Importe & ",[TasaCambio] = " & TasaCambio & ",[Numero_Lote] = '" & Numero_Lote & "' ,[Fecha_Vence] = " & Format(Fecha_Lote, "dd/MM/yyyy") & " " & _
                        "WHERE (Numero_Compra = '" & ConsecutivoCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & FrmCompras.CboTipoProducto.Text & "') AND (Cod_Producto = '" & CodProducto & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else

            SqlUpdate = "INSERT INTO [Detalle_Compras] ([Numero_Compra],[Fecha_Compra],[Tipo_Compra],[Cod_Producto],[Cantidad],[Precio_Unitario],[Descuento],[Precio_Neto],[Importe],[TasaCambio],[Numero_Lote],[Fecha_Vence],[Descripcion_Producto])" & _
            "VALUES ('" & ConsecutivoCompra & "','" & FrmCompras.DTPFecha.Value & "','" & FrmCompras.CboTipoProducto.Text & "','" & CodProducto & "' ," & Cantidad & "," & PrecioUnitario & "," & Descuento & " ," & PrecioNeto & "," & Importe & "," & TasaCambio & ",'" & Numero_Lote & "','" & Format(Fecha_Lote, "dd/MM/yyyy") & "', '" & DescripcionProducto & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub


    Public Sub GrabaMetodoDetalleCompra(ByVal ConsecutivoCompra As String, ByVal NombrePago As String, ByVal Monto As Double, ByVal NumeroTarjeta As String, ByVal FechaVence As String)
        Dim Sqldetalle As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String, FechaVencimiento As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim NumeroCompra As String = ""

        Fecha = Format(FrmCompras.DTPFecha.Value, "yyyy-MM-dd")
        FechaVencimiento = Format(CDate(FechaVence), "dd/MM/yyyy")
        NumeroCompra = My.Forms.FrmCompras.TxtNumeroEnsamble.Text

        Sqldetalle = "SELECT *  FROM Detalle_MetodoCompras WHERE (Numero_Compra = '" & ConsecutivoCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & FrmCompras.CboTipoProducto.Text & "') AND (NombrePago = '" & NombrePago & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(Sqldetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleMetodoCompra")
        If Not DataSet.Tables("DetalleMetodoCompra").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlUpdate = "UPDATE [Detalle_MetodoCompras] SET [NombrePago] = '" & NombrePago & "',[Monto] = " & Monto & ",[NumeroTarjeta] = '" & NumeroTarjeta & "',[FechaVence] = '" & FechaVencimiento & "' " & _
                         "WHERE (Numero_Compra = '" & ConsecutivoCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & FrmCompras.CboTipoProducto.Text & "') AND (NombrePago = '" & NombrePago & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            MiConexion.Close()
            SqlUpdate = "INSERT INTO [Detalle_MetodoCompras] ([Numero_Compra],[Fecha_Compra],[Tipo_Compra],[NombrePago],[Monto],[NumeroTarjeta] ,[FechaVence]) " & _
                        "VALUES ('" & NumeroCompra & "','" & FrmCompras.DTPFecha.Value & "','" & FrmCompras.CboTipoProducto.Text & "','" & NombrePago & "'," & Monto & " ,'" & NumeroTarjeta & "','" & FechaVencimiento & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub


    Public Sub LimpiarCompras()
        Dim SqlString As String, DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)

        My.Forms.FrmCompras.TxtNumeroEnsamble.Text = "-----0-----"
        My.Forms.FrmCompras.TxtCodigoProveedor.Text = ""



        FrmCompras.DTPFecha.Value = Format(Now, "dd/MM/yyyy")
        FrmCompras.DTVencimiento.Value = Format(Now, "dd/MM/yyyy")

        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////CARGO LAS BODEGAS////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlString = "SELECT  * FROM   Bodegas"
        MiConexion.Open()
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataSet.Reset()
        DataAdapter.Fill(DataSet, "Bodegas")
        FrmCompras.CboCodigoBodega.DataSource = DataSet.Tables("Bodegas")
        If Not DataSet.Tables("Bodegas").Rows.Count = 0 Then
            FrmCompras.CboCodigoBodega.Text = DataSet.Tables("Bodegas").Rows(0)("Cod_Bodega")
        End If
        FrmCompras.CboCodigoBodega.Columns(0).Caption = "Codigo"
        FrmCompras.CboCodigoBodega.Columns(1).Caption = "Nombre Bodega"


        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////CARGO LAS FORMA DE PAGO////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlString = "SELECT  NombrePago, Monto,NumeroTarjeta,FechaVence FROM Detalle_MetodoCompras WHERE (Numero_Compra = '-1')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "MetodoPago")
        FrmCompras.BindingMetodo.DataSource = DataSet.Tables("MetodoPago")
        FrmCompras.TrueDBGridMetodo.DataSource = FrmCompras.BindingMetodo
        FrmCompras.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(1).Width = 110
        FrmCompras.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(1).Width = 70
        FrmCompras.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(0).Button = True
        FrmCompras.TrueDBGridMetodo.Columns(1).NumberFormat = "##,##0.00"
        FrmCompras.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(2).Visible = False
        FrmCompras.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(3).Visible = False
        MiConexion.Close()


        My.Forms.FrmCompras.ds.Tables("DetalleCompra").Reset()

        If My.Forms.FrmCompras.FacturaTarea = True Then
            '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '///////////////////////////////CARGO EL DETALLE DE COMPRAS/////////////////////////////////////////////////////////////////
            '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            SqlString = "SELECT  Detalle_Compras.Cod_Producto, Detalle_Compras.Descripcion_Producto, Detalle_Compras.Cantidad, Detalle_Compras.Precio_Unitario, Detalle_Compras.Descuento, Detalle_Compras.Precio_Neto, Detalle_Compras.Importe,Detalle_Compras.id_Detalle_Compra, Detalle_Compras.Numero_Lote,Detalle_Compras.Fecha_Vence, TasaCambio, Numero_Compra, Fecha_Compra, Tipo_Compra FROM  Detalle_Compras WHERE (Detalle_Compras.Numero_Compra = '-1')"
            'DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            'DataAdapter.Fill(DataSet, "DetalleCompra")
            FrmCompras.ds = New DataSet
            FrmCompras.da = New SqlDataAdapter(SqlString, MiConexion)
            FrmCompras.CmdBuilder = New SqlCommandBuilder(FrmCompras.da)
            FrmCompras.da.Fill(FrmCompras.ds, "DetalleCompra")
            FrmCompras.BindingDetalle.DataSource = FrmCompras.ds.Tables("DetalleCompra")
            FrmCompras.TrueDBGridComponentes.DataSource = FrmCompras.BindingDetalle
            FrmCompras.TrueDBGridComponentes.Columns(0).Caption = "Codigo"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Button = True
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Width = 74
            FrmCompras.TrueDBGridComponentes.Columns(1).Caption = "Descripcion"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Width = 259
            'Me.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Locked = True
            FrmCompras.TrueDBGridComponentes.Columns(2).Caption = "Ordenado"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Width = 64
            FrmCompras.TrueDBGridComponentes.Columns(3).Caption = "Precio Unit"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Width = 62
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Locked = False
            FrmCompras.TrueDBGridComponentes.Columns(4).Caption = "%Desc"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Width = 43
            FrmCompras.TrueDBGridComponentes.Columns(5).Caption = "Precio Neto"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Width = 65
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Locked = True
            FrmCompras.TrueDBGridComponentes.Columns(6).Caption = "Importe"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Width = 61
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Locked = True
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(7).Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(8).Button = True
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("TasaCambio").Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Numero_Compra").Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Fecha_Compra").Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Tipo_Compra").Visible = False

        Else

            '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '///////////////////////////////CARGO EL DETALLE DE COMPRAS/////////////////////////////////////////////////////////////////
            '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            SqlString = "SELECT  Detalle_Compras.Cod_Producto, Detalle_Compras.Descripcion_Producto, Detalle_Compras.Cantidad, Detalle_Compras.Precio_Unitario, Detalle_Compras.Descuento, Detalle_Compras.Precio_Neto, Detalle_Compras.Importe,Detalle_Compras.id_Detalle_Compra, TasaCambio, Numero_Compra, Fecha_Compra, Tipo_Compra FROM  Detalle_Compras  WHERE (Detalle_Compras.Numero_Compra = '-1')"
            'DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            'DataAdapter.Fill(DataSet, "DetalleCompra")
            'Me.BindingDetalle.DataSource = DataSet.Tables("DetalleCompra")
            FrmCompras.ds = New DataSet
            FrmCompras.da = New SqlDataAdapter(SqlString, MiConexion)
            FrmCompras.CmdBuilder = New SqlCommandBuilder(FrmCompras.da)
            FrmCompras.da.Fill(FrmCompras.ds, "DetalleCompra")
            FrmCompras.BindingDetalle.DataSource = FrmCompras.ds.Tables("DetalleCompra")
            FrmCompras.TrueDBGridComponentes.DataSource = FrmCompras.BindingDetalle
            FrmCompras.TrueDBGridComponentes.Columns(0).Caption = "Codigo"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Button = True
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Width = 74
            FrmCompras.TrueDBGridComponentes.Columns(1).Caption = "Descripcion"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Width = 259
            'FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Locked = True
            FrmCompras.TrueDBGridComponentes.Columns(2).Caption = "Ordenado"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Width = 64
            FrmCompras.TrueDBGridComponentes.Columns(3).Caption = "Precio Unit"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Width = 62
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Locked = False
            FrmCompras.TrueDBGridComponentes.Columns(4).Caption = "%Desc"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Width = 43
            FrmCompras.TrueDBGridComponentes.Columns(5).Caption = "Precio Neto"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Width = 65
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Locked = True
            FrmCompras.TrueDBGridComponentes.Columns(6).Caption = "Importe"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Width = 61
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Locked = True
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(7).Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("TasaCambio").Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Numero_Compra").Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Fecha_Compra").Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Tipo_Compra").Visible = False

        End If


        If FrmCompras.CboTipoProducto.Text = "Cuenta" Then
            FrmCompras.TrueDBGridComponentes.Columns(0).Caption = "Codigo"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Button = True
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Width = 74
            FrmCompras.TrueDBGridComponentes.Columns(1).Caption = "Descripcion"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Width = 350
            'Me.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Locked = True
            FrmCompras.TrueDBGridComponentes.Columns(2).Caption = "Ordenado"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Width = 64
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Visible = False
            FrmCompras.TrueDBGridComponentes.Columns(3).Caption = "Monto"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Width = 62
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Locked = False
            FrmCompras.TrueDBGridComponentes.Columns(4).Caption = "%Desc"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Width = 43
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Visible = False
            FrmCompras.TrueDBGridComponentes.Columns(5).Caption = "Precio Neto"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Width = 65
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Locked = True
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Visible = False
            FrmCompras.TrueDBGridComponentes.Columns(6).Caption = "Importe"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Width = 61
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Locked = True
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Visible = True
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(7).Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(8).Button = True
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(8).Visible = True
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("TasaCambio").Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Numero_Compra").Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Fecha_Compra").Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Tipo_Compra").Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Fecha_Vence").Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Numero_Lote").Visible = False

        ElseIf FrmCompras.CboTipoProducto.Text = "Cuenta DB" Then
            FrmCompras.TrueDBGridComponentes.Columns(0).Caption = "Codigo"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Button = True
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Width = 74
            FrmCompras.TrueDBGridComponentes.Columns(1).Caption = "Descripcion"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Width = 350
            'Me.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Locked = True
            FrmCompras.TrueDBGridComponentes.Columns(2).Caption = "Ordenado"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Width = 64
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Visible = False
            FrmCompras.TrueDBGridComponentes.Columns(3).Caption = "Monto"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Width = 62
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Locked = False
            FrmCompras.TrueDBGridComponentes.Columns(4).Caption = "%Desc"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Width = 43
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Visible = False
            FrmCompras.TrueDBGridComponentes.Columns(5).Caption = "Precio Neto"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Width = 65
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Locked = True
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Visible = False
            FrmCompras.TrueDBGridComponentes.Columns(6).Caption = "Importe"
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Width = 61
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Locked = True
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Visible = True
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(7).Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(8).Button = True
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(8).Visible = True
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("TasaCambio").Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("NuFrmComprasro_Compra").Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Fecha_Compra").Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Tipo_Compra").Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Fecha_Vence").Visible = False
            FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Numero_Lote").Visible = False
        End If

        FrmCompras.RadioButton1.Checked = True
        My.Forms.FrmCompras.TxtSubTotal.Text = ""
        My.Forms.FrmCompras.TxtIva.Text = ""
        My.Forms.FrmCompras.TxtPagado.Text = ""
        My.Forms.FrmCompras.TxtNetoPagar.Text = ""
        My.Forms.FrmCompras.TxtObservaciones.Text = ""
        My.Forms.FrmCompras.CboProyecto.Text = ""
        PrimerRegistroCompra = True

        My.Forms.FrmCompras.TrueDBGridComponentes.AllowAddNew = True
        My.Forms.FrmCompras.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Button = True
    End Sub
    Public Function BuscaCosto(ByVal CodigoProductos As String, ByVal CodBodega As String) As Double
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim SqlString As String, Existencia As Double = 0, Costo As Double = 0

        SqlString = "SELECT  * FROM DetalleBodegas WHERE (Cod_Bodegas = '" & CodBodega & "') AND (Cod_Productos = '" & CodigoProductos & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Consulta")
        Existencia = 0
        Costo = 0
        If Not DataSet.Tables("Consulta").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Consulta").Rows(0)("Existencia_Unidades")) Then
                'Existencia = DataSet.Tables("Consulta").Rows(0)("Existencia_Unidades")
            End If
            If Not IsDBNull(DataSet.Tables("Consulta").Rows(0)("Costo")) Then
                Costo = DataSet.Tables("Consulta").Rows(0)("Costo")
            End If
        End If

        BuscaCosto = Costo
    End Function


    Public Sub GrabaComprasHistoricos(ByVal NumeroCompra As String, ByVal FechaCompra As Date, ByVal TipoCompra As String, ByVal CodProducto As String, ByVal CostoUnitAnt As Double, ByVal CostoUnitNuevo As Double, ByVal CodBodega As String)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim SqlUpdate As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim SqlDetalle As String

        SqlDetalle = "SELECT *  FROM HistoricoCostos WHERE (Numero_Compra = '" & NumeroCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Format(FechaCompra, "yyyy-MM-dd") & "', 102)) AND (Tipo_Compra = '" & TipoCompra & "') AND (CodProducto = '" & CodProducto & "') AND (CodBodega = '" & CodBodega & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlDetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleCompra")
        If Not DataSet.Tables("DetalleCompra").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlUpdate = "UPDATE [HistoricoCostos] SET [CostoUnitNuevo] = '" & CostoUnitNuevo & "'  WHERE (Numero_Compra = '" & NumeroCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Format(FechaCompra, "yyyy-MM-dd") & "', 102)) AND (Tipo_Compra = '" & TipoCompra & "') AND (CodProducto = '" & CodProducto & "') AND (CodBodega = '" & CodBodega & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else

            SqlUpdate = "INSERT INTO [HistoricoCostos] ([Numero_Compra] ,[Fecha_Compra],[Tipo_Compra] ,[CodProducto],[CodBodega],[CostoUnitAnt],[CostoUnitNuevo]) " & _
                        "VALUES ('" & NumeroCompra & "','" & Format(FechaCompra, "dd/MM/yyyy") & "','" & TipoCompra & "','" & CodProducto & "','" & CodBodega & "','" & CostoUnitAnt & "' ,'" & CostoUnitNuevo & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub

    Public Function BuscoCostoHistorico(ByVal FechaCompraIni As Date, ByVal FechaCompraFin As Date, ByVal TipoCompra As String, ByVal CodProducto As String, ByVal CodBodega As String) As Double
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim SqlString As String, Registros As Double

        'SqlString = "SELECT  * FROM HistoricoCostos WHERE (Tipo_Compra = N'Mercancia Recibida') AND (CodProducto = '" & CodProducto & "') AND (CodBodega = '" & CodBodega & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Format(FechaCompra, "yyyy-MM-dd") & "', 102)) ORDER BY IdDetalle"
        SqlString = "SELECT * FROM HistoricoCostos WHERE  (Tipo_Compra = '" & TipoCompra & "') AND (CodProducto = '" & CodProducto & "') AND (CodBodega = '" & CodBodega & "') AND (Fecha_Compra BETWEEN CONVERT(DATETIME, '" & Format(FechaCompraIni, "yyyy-MM-dd") & "', 102) AND CONVERT(DATETIME, '" & Format(FechaCompraFin, "yyyy-MM-dd") & "', 102)) ORDER BY IdDetalle"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Consulta")
        If Not DataSet.Tables("Consulta").Rows.Count = 0 Then
            Registros = DataSet.Tables("Consulta").Rows.Count - 1
            BuscoCostoHistorico = DataSet.Tables("Consulta").Rows(Registros)("CostoUnitNuevo")
        Else
            BuscoCostoHistorico = 0
        End If

    End Function

    Public Sub ExistenciasCostos(ByVal CodigoProductos As String, ByVal CantidadCompra As Double, ByVal PrecioCompra As Double, ByVal Tipo As String, ByVal CodBodega As String)
        Dim SqlString As String, DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, CodigoBodega As String, iPosicionFila As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), Existencia As Double, CostoPromedio As Double, Costo As Double
        Dim SqlUpdate As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, ExistenciaBodega As Double, ExistenciaTotal As Double
        Dim TasaCambio As String, CostoPromedioDolar As Double

        DataSet.Reset()
        'SqlString = "SELECT *  FROM Productos WHERE (Cod_Productos = '" & CodigoProductos & "')"
        SqlString = "SELECT SUM(Cantidad) AS Cantidad, AVG(Precio_Neto) AS PrecioNeto, SUM(Cantidad * Precio_Neto) AS Costo_Promedio, Cod_Producto FROM Detalle_Compras GROUP BY Cod_Producto  " & _
                    "HAVING  (Cod_Producto = '" & CodigoProductos & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Consulta")
        Existencia = 0
        Costo = 0
        If Not DataSet.Tables("Consulta").Rows.Count = 0 Then
            'If Not IsDBNull(DataSet.Tables("Consulta").Rows(0)("Existencia_Unidades")) Then
            '    'Existencia = DataSet.Tables("Consulta").Rows(0)("Existencia_Unidades")
            'End If
            If Not IsDBNull(DataSet.Tables("Consulta").Rows(0)("Costo_Promedio")) Then
                Costo = DataSet.Tables("Consulta").Rows(0)("Costo_Promedio")
            End If
        End If



        DataSet.Tables("Consulta").Clear()
        'SqlString = "SELECT Cod_Bodegas, Cod_Productos, Existencia  FROM DetalleBodegas WHERE (Cod_Bodegas = '" & CodBodega & "') AND (Cod_Productos = '" & CodigoProductos & "')"
        SqlString = "SELECT Cod_Bodegas, Cod_Productos, Existencia  FROM DetalleBodegas WHERE (Cod_Productos = '" & CodigoProductos & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Consulta")

        'If Not DataSet.Tables("Consulta").Rows.Count = 0 Then
        '    If Not IsDBNull(DataSet.Tables("Consulta").Rows(0)("Existencia")) Then
        '        'ExistenciaBodega = DataSet.Tables("Consulta").Rows(0)("Existencia")
        '    End If
        'End If
        Existencia = 0
        iPosicionFila = 0
        Do While iPosicionFila < (DataSet.Tables("Consulta").Rows.Count)
            My.Application.DoEvents()
            CodigoBodega = DataSet.Tables("Consulta").Rows(iPosicionFila)("Cod_Bodegas")
            Existencia = Existencia + BuscaExistenciaBodega(CodigoProductos, CodigoBodega)

            iPosicionFila = iPosicionFila + 1
        Loop

        '////////////////////777LA EXISTENCIA YA TIENE ACUMULADO TODO INCLUSIVE LA COMPRA O VENTA/////////////////
        ExistenciaBodega = BuscaExistenciaBodega(CodigoProductos, CodBodega)


        Select Case Tipo
            Case "Mercancia Recibida"
                '///////////////////////////FORMULA DE COMPRA PROMEDIO////////////////////////////////////////////////////////////////
                ' CostoPromedio= ((Existencia*Costo)+(PrecioCompra*CantidadCompra))/(Existencia+CantidadComprada)
                '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                '///////////////////LA VARIABLE EXISTENCIA TIENE TAMBIEN ACUMULADA LA COMPRA ACTUAL////////////////////////////////////
                'Existencia + CantidadCompra
                Existencia = Math.Abs(Existencia - CantidadCompra)
                TasaCambio = BuscaTasaCambio(FrmCompras.DTPFecha.Value)

                If TasaCambio = 0 Then
                    MsgBox("la Tasa de Cambio es Cero", MsgBoxStyle.Critical, "Zeus Facturacion")
                End If

                If (Existencia + CantidadCompra) <> 0 Then
                    If FrmCompras.TxtMonedaFactura.Text = "Cordobas" Then
                        'CostoPromedio = Format(((Existencia * Costo) + (PrecioCompra * CantidadCompra)) / (Existencia + CantidadCompra), "##,##0.00")
                        CostoPromedio = (Costo / (Existencia + CantidadCompra))
                        CostoPromedioDolar = Format(CostoPromedio / TasaCambio, "##,##0.00")
                    Else
                        CostoPromedioDolar = Format(((Existencia * Costo) + (PrecioCompra * CantidadCompra)) / (Existencia + CantidadCompra), "##,##0.000")
                        CostoPromedio = Format(CostoPromedioDolar * TasaCambio, "##,##0.00")
                    End If
                End If

                ExistenciaTotal = Existencia + CantidadCompra



                '///////////////////////////////////////ACTUALIZO LA EXISTENCIA DE PRODUCTOS////////////////////////////////////////////////////////////////
                SqlUpdate = "UPDATE [Productos] SET [Existencia_Unidades] = " & ExistenciaTotal & ",[Costo_Promedio] = " & CostoPromedio & " ,[Costo_Promedio_Dolar] = " & CostoPromedioDolar & ", [Ultimo_Precio_Compra] = " & PrecioCompra & " ,[Existencia_Dinero] = " & ExistenciaTotal * CostoPromedio & ",[Existencia_DineroDolar] = " & ExistenciaTotal * CostoPromedioDolar & " " & _
                            "WHERE (Cod_Productos = '" & CodigoProductos & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()


                '////////////////////////////////////////////ACTUALIZO LA EXISTENCIA DE LA BODEGA////////////////////////////////////////////////////////
                SqlUpdate = "UPDATE [DetalleBodegas] SET [Existencia] = " & ExistenciaBodega & " " & _
                            "WHERE (Cod_Bodegas = '" & CodBodega & "') AND (Cod_Productos = '" & CodigoProductos & "') "
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()



            Case "Devolucion de Compra"
                '///////////////////////////FORMULA DE COMPRA PROMEDIO////////////////////////////////////////////////////////////////
                ' CostoPromedio= ((Existencia*Costo)+(PrecioCompra*CantidadCompra))/(Existencia+CantidadComprada)
                '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                '///////////////////LA VARIABLE EXISTENCIA TIENE TAMBIEN ACUMULADA LA DEVOLUCION ACTUAL////////////////////////////////////
                'Existencia + CantidadCompra
                Existencia = Math.Abs(Existencia + CantidadCompra)

                If (Existencia - CantidadCompra) <> 0 Then
                    CostoPromedio = Format(((Existencia * Costo) - (PrecioCompra * CantidadCompra)) / (Existencia - CantidadCompra), "##,##0.00")
                Else
                    CostoPromedio = 0
                End If

                ExistenciaTotal = Existencia - CantidadCompra
                '///////////////////////////////////////ACTUALIZO LA EXISTENCIA DE PRODUCTOS////////////////////////////////////////////////////////////////
                SqlUpdate = "UPDATE [Productos] SET [Existencia_Unidades] = " & ExistenciaTotal & ",[Costo_Promedio] = " & CostoPromedio & " ,[Ultimo_Precio_Compra] = " & PrecioCompra & " ,[Existencia_Dinero] = " & ExistenciaTotal * CostoPromedio & " " & _
                            "WHERE (Cod_Productos = '" & CodigoProductos & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()


                '////////////////////////////////////////////ACTUALIZO LA EXISTENCIA DE LA BODEGA////////////////////////////////////////////////////////
                SqlUpdate = "UPDATE [DetalleBodegas] SET [Existencia] = " & ExistenciaBodega & " " & _
                            "WHERE (Cod_Bodegas = '" & CodBodega & "') AND (Cod_Productos = '" & CodigoProductos & "') "
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()
            Case "Factura"

                '///////////////////LA VARIABLE EXISTENCIA TIENE TAMBIEN ACUMULADA LA FACTURA ACTUAL////////////////////////////////////
                'Existencia + CantidadCompra
                Existencia = Math.Abs(Existencia + CantidadCompra)

                ExistenciaTotal = Existencia - CantidadCompra



                '///////////////////////////////////////ACTUALIZO LA EXISTENCIA DE PRODUCTOS////////////////////////////////////////////////////////////////
                SqlUpdate = "UPDATE [Productos] SET [Existencia_Unidades] = " & ExistenciaTotal & " ,[Ultimo_Precio_Venta] = " & PrecioCompra & " ,[Existencia_Dinero] = " & ExistenciaTotal * Costo & " " & _
                            "WHERE (Cod_Productos = '" & CodigoProductos & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()


                '////////////////////////////////////////////ACTUALIZO LA EXISTENCIA DE LA BODEGA////////////////////////////////////////////////////////
                SqlUpdate = "UPDATE [DetalleBodegas] SET [Existencia] = " & ExistenciaBodega & " " & _
                            "WHERE (Cod_Bodegas = '" & CodBodega & "') AND (Cod_Productos = '" & CodigoProductos & "') "
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()

            Case "Devolucion de Venta"
                '///////////////////LA VARIABLE EXISTENCIA TIENE TAMBIEN ACUMULADA LA FACTURA ACTUAL////////////////////////////////////
                'Existencia + CantidadCompra
                Existencia = Math.Abs(Existencia - CantidadCompra)

                ExistenciaTotal = Existencia + CantidadCompra



                '///////////////////////////////////////ACTUALIZO LA EXISTENCIA DE PRODUCTOS////////////////////////////////////////////////////////////////
                SqlUpdate = "UPDATE [Productos] SET [Existencia_Unidades] = " & ExistenciaTotal & " ,[Ultimo_Precio_Venta] = " & PrecioCompra & " ,[Existencia_Dinero] = " & ExistenciaTotal * Costo & " " & _
                            "WHERE (Cod_Productos = '" & CodigoProductos & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()


                '////////////////////////////////////////////ACTUALIZO LA EXISTENCIA DE LA BODEGA////////////////////////////////////////////////////////
                SqlUpdate = "UPDATE [DetalleBodegas] SET [Existencia] = " & ExistenciaBodega & " " & _
                            "WHERE (Cod_Bodegas = '" & CodBodega & "') AND (Cod_Productos = '" & CodigoProductos & "') "
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()
        End Select



    End Sub
    Public Sub ExistenciasCostosBodega(ByVal CodigoProductos As String, ByVal CantidadCompra As Double, ByVal PrecioCompra As Double, ByVal Tipo As String, ByVal CodBodega As String, ByVal FechaCompra As Date)
        Dim SqlString As String, DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, CodigoBodega As String, iPosicionFila As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), Existencia As Double, CostoPromedio As Double, Costo As Double
        Dim SqlUpdate As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, ExistenciaBodega As Double, ExistenciaTotal As Double
        Dim TasaCambio As String, CostoPromedioDolar As Double

        DataSet.Reset()
        SqlString = "SELECT *  FROM Productos WHERE (Cod_Productos = '" & CodigoProductos & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Consulta")
        Existencia = 0
        Costo = 0
        If Not DataSet.Tables("Consulta").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Consulta").Rows(0)("Existencia_Unidades")) Then
                'Existencia = DataSet.Tables("Consulta").Rows(0)("Existencia_Unidades")
            End If
            If Not IsDBNull(DataSet.Tables("Consulta").Rows(0)("Costo_Promedio")) Then
                Costo = DataSet.Tables("Consulta").Rows(0)("Costo_Promedio")
            End If
        End If



        DataSet.Tables("Consulta").Clear()
        'SqlString = "SELECT Cod_Bodegas, Cod_Productos, Existencia  FROM DetalleBodegas WHERE (Cod_Bodegas = '" & CodBodega & "') AND (Cod_Productos = '" & CodigoProductos & "')"
        SqlString = "SELECT Cod_Bodegas, Cod_Productos, Existencia  FROM DetalleBodegas WHERE (Cod_Productos = '" & CodigoProductos & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Consulta")

        'If Not DataSet.Tables("Consulta").Rows.Count = 0 Then
        '    If Not IsDBNull(DataSet.Tables("Consulta").Rows(0)("Existencia")) Then
        '        'ExistenciaBodega = DataSet.Tables("Consulta").Rows(0)("Existencia")
        '    End If
        'End If
        Existencia = 0
        iPosicionFila = 0
        Do While iPosicionFila < (DataSet.Tables("Consulta").Rows.Count)
            My.Application.DoEvents()
            CodigoBodega = DataSet.Tables("Consulta").Rows(iPosicionFila)("Cod_Bodegas")
            Existencia = Existencia + BuscaExistenciaBodega(CodigoProductos, CodigoBodega)

            iPosicionFila = iPosicionFila + 1
        Loop

        '////////////////////777LA EXISTENCIA YA TIENE ACUMULADO TODO INCLUSIVE LA COMPRA O VENTA/////////////////
        ExistenciaBodega = BuscaExistenciaBodega(CodigoProductos, CodBodega)


        Select Case Tipo
            Case "Mercancia Recibida"
                '///////////////////////////FORMULA DE COMPRA PROMEDIO////////////////////////////////////////////////////////////////
                ' CostoPromedio= ((Existencia*Costo)+(PrecioCompra*CantidadCompra))/(Existencia+CantidadComprada)
                '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                '///////////////////LA VARIABLE EXISTENCIA TIENE TAMBIEN ACUMULADA LA COMPRA ACTUAL////////////////////////////////////
                'Existencia + CantidadCompra
                Existencia = Math.Abs(Existencia - CantidadCompra)
                TasaCambio = BuscaTasaCambio(FechaCompra)

                If TasaCambio = 0 Then
                    MsgBox("la Tasa de Cambio es Cero", MsgBoxStyle.Critical, "Zeus Facturacion")
                End If

                If (Existencia + CantidadCompra) <> 0 Then
                    If FrmCompras.TxtMonedaFactura.Text = "Cordobas" Then
                        CostoPromedio = Format(((Existencia * Costo) + (PrecioCompra * CantidadCompra)) / (Existencia + CantidadCompra), "##,##0.00")
                        CostoPromedioDolar = Format(CostoPromedio / TasaCambio, "##,##0.00")
                    Else
                        CostoPromedioDolar = Format(((Existencia * Costo) + (PrecioCompra * CantidadCompra)) / (Existencia + CantidadCompra), "##,##0.000")
                        CostoPromedio = Format(CostoPromedioDolar * TasaCambio, "##,##0.00")
                    End If
                End If

                ExistenciaTotal = Existencia + CantidadCompra



                '///////////////////////////////////////ACTUALIZO LA EXISTENCIA DE PRODUCTOS////////////////////////////////////////////////////////////////
                SqlUpdate = "UPDATE [Productos] SET [Existencia_Unidades] = " & ExistenciaTotal & ",[Costo_Promedio] = " & CostoPromedio & " ,[Costo_Promedio_Dolar] = " & CostoPromedioDolar & ", [Ultimo_Precio_Compra] = " & PrecioCompra & " ,[Existencia_Dinero] = " & ExistenciaTotal * CostoPromedio & ",[Existencia_DineroDolar] = " & ExistenciaTotal * CostoPromedioDolar & " " & _
                            "WHERE (Cod_Productos = '" & CodigoProductos & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()


                '////////////////////////////////////////////ACTUALIZO LA EXISTENCIA DE LA BODEGA////////////////////////////////////////////////////////
                SqlUpdate = "UPDATE [DetalleBodegas] SET [Existencia] = " & ExistenciaBodega & " " & _
                            "WHERE (Cod_Bodegas = '" & CodBodega & "') AND (Cod_Productos = '" & CodigoProductos & "') "
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()
            Case "Devolucion de Compra"
                '///////////////////////////FORMULA DE COMPRA PROMEDIO////////////////////////////////////////////////////////////////
                ' CostoPromedio= ((Existencia*Costo)+(PrecioCompra*CantidadCompra))/(Existencia+CantidadComprada)
                '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                '///////////////////LA VARIABLE EXISTENCIA TIENE TAMBIEN ACUMULADA LA DEVOLUCION ACTUAL////////////////////////////////////
                'Existencia + CantidadCompra
                Existencia = Math.Abs(Existencia + CantidadCompra)

                If (Existencia - CantidadCompra) <> 0 Then
                    CostoPromedio = Format(((Existencia * Costo) - (PrecioCompra * CantidadCompra)) / (Existencia - CantidadCompra), "##,##0.00")
                Else
                    CostoPromedio = 0
                End If

                ExistenciaTotal = Existencia - CantidadCompra
                '///////////////////////////////////////ACTUALIZO LA EXISTENCIA DE PRODUCTOS////////////////////////////////////////////////////////////////
                SqlUpdate = "UPDATE [Productos] SET [Existencia_Unidades] = " & ExistenciaTotal & ",[Costo_Promedio] = " & CostoPromedio & " ,[Ultimo_Precio_Compra] = " & PrecioCompra & " ,[Existencia_Dinero] = " & ExistenciaTotal * CostoPromedio & " " & _
                            "WHERE (Cod_Productos = '" & CodigoProductos & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()


                '////////////////////////////////////////////ACTUALIZO LA EXISTENCIA DE LA BODEGA////////////////////////////////////////////////////////
                SqlUpdate = "UPDATE [DetalleBodegas] SET [Existencia] = " & ExistenciaBodega & " " & _
                            "WHERE (Cod_Bodegas = '" & CodBodega & "') AND (Cod_Productos = '" & CodigoProductos & "') "
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()
            Case "Factura"

                '///////////////////LA VARIABLE EXISTENCIA TIENE TAMBIEN ACUMULADA LA FACTURA ACTUAL////////////////////////////////////
                'Existencia + CantidadCompra
                Existencia = Math.Abs(Existencia + CantidadCompra)

                ExistenciaTotal = Existencia - CantidadCompra



                '///////////////////////////////////////ACTUALIZO LA EXISTENCIA DE PRODUCTOS////////////////////////////////////////////////////////////////
                SqlUpdate = "UPDATE [Productos] SET [Existencia_Unidades] = " & ExistenciaTotal & " ,[Ultimo_Precio_Venta] = " & PrecioCompra & " ,[Existencia_Dinero] = " & ExistenciaTotal * Costo & " " & _
                            "WHERE (Cod_Productos = '" & CodigoProductos & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()


                '////////////////////////////////////////////ACTUALIZO LA EXISTENCIA DE LA BODEGA////////////////////////////////////////////////////////
                SqlUpdate = "UPDATE [DetalleBodegas] SET [Existencia] = " & ExistenciaBodega & " " & _
                            "WHERE (Cod_Bodegas = '" & CodBodega & "') AND (Cod_Productos = '" & CodigoProductos & "') "
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()

            Case "Devolucion de Venta"
                '///////////////////LA VARIABLE EXISTENCIA TIENE TAMBIEN ACUMULADA LA FACTURA ACTUAL////////////////////////////////////
                'Existencia + CantidadCompra
                Existencia = Math.Abs(Existencia - CantidadCompra)

                ExistenciaTotal = Existencia + CantidadCompra



                '///////////////////////////////////////ACTUALIZO LA EXISTENCIA DE PRODUCTOS////////////////////////////////////////////////////////////////
                SqlUpdate = "UPDATE [Productos] SET [Existencia_Unidades] = " & ExistenciaTotal & " ,[Ultimo_Precio_Venta] = " & PrecioCompra & " ,[Existencia_Dinero] = " & ExistenciaTotal * Costo & " " & _
                            "WHERE (Cod_Productos = '" & CodigoProductos & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()


                '////////////////////////////////////////////ACTUALIZO LA EXISTENCIA DE LA BODEGA////////////////////////////////////////////////////////
                SqlUpdate = "UPDATE [DetalleBodegas] SET [Existencia] = " & ExistenciaBodega & " " & _
                            "WHERE (Cod_Bodegas = '" & CodBodega & "') AND (Cod_Productos = '" & CodigoProductos & "') "
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()
        End Select



    End Sub


    Public Sub CostoBodega(ByVal CodigoProductos As String, ByVal CantidadCompra As Double, ByVal PrecioCompra As Double, ByVal Tipo As String, ByVal CodBodega As String, ByVal FechaCompra As Date)
        Dim SqlString As String, DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, CodigoBodega As String, iPosicionFila As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), Existencia As Double, CostoPromedio As Double, Costo As Double
        Dim SqlUpdate As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, ExistenciaBodega As Double, ExistenciaTotal As Double
        Dim TasaCambio As String, CostoPromedioDolar As Double

        DataSet.Reset()

        SqlString = "SELECT  * FROM DetalleBodegas WHERE (Cod_Bodegas = '" & CodBodega & "') AND (Cod_Productos = '" & CodigoProductos & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Consulta")
        Existencia = 0
        Costo = 0
        If Not DataSet.Tables("Consulta").Rows.Count = 0 Then
            If Not IsDBNull(DataSet.Tables("Consulta").Rows(0)("Existencia_Unidades")) Then
                'Existencia = DataSet.Tables("Consulta").Rows(0)("Existencia_Unidades")
            End If
            If Not IsDBNull(DataSet.Tables("Consulta").Rows(0)("Costo")) Then
                Costo = DataSet.Tables("Consulta").Rows(0)("Costo")
            End If
        End If



        DataSet.Tables("Consulta").Clear()

        SqlString = "SELECT  * FROM DetalleBodegas WHERE (Cod_Bodegas = '" & CodBodega & "') AND (Cod_Productos = '" & CodigoProductos & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Consulta")

        Existencia = 0
        iPosicionFila = 0
        Do While iPosicionFila < (DataSet.Tables("Consulta").Rows.Count)
            My.Application.DoEvents()
            CodigoBodega = DataSet.Tables("Consulta").Rows(iPosicionFila)("Cod_Bodegas")
            Existencia = Existencia + BuscaExistenciaBodega(CodigoProductos, CodigoBodega)

            iPosicionFila = iPosicionFila + 1
        Loop

        '////////////////////777LA EXISTENCIA YA TIENE ACUMULADO TODO INCLUSIVE LA COMPRA O VENTA/////////////////
        ExistenciaBodega = BuscaExistenciaBodega(CodigoProductos, CodBodega)


        Select Case Tipo
            Case "Mercancia Recibida"
                '///////////////////////////FORMULA DE COMPRA PROMEDIO////////////////////////////////////////////////////////////////
                ' CostoPromedio= ((Existencia*Costo)+(PrecioCompra*CantidadCompra))/(Existencia+CantidadComprada)
                '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                '///////////////////LA VARIABLE EXISTENCIA TIENE TAMBIEN ACUMULADA LA COMPRA ACTUAL////////////////////////////////////
                'Existencia + CantidadCompra
                Existencia = Math.Abs(Existencia - CantidadCompra)
                TasaCambio = BuscaTasaCambio(FechaCompra)

                If TasaCambio = 0 Then
                    MsgBox("la Tasa de Cambio es Cero", MsgBoxStyle.Critical, "Zeus Facturacion")
                End If

                If (Existencia + CantidadCompra) <> 0 Then
                    'If FrmCompras.TxtMonedaFactura.Text = "Cordobas" Then
                    CostoPromedio = Format(((Existencia * Costo) + (PrecioCompra * CantidadCompra)) / (Existencia + CantidadCompra), "##,##0.00")
                    CostoPromedioDolar = Format(CostoPromedio / TasaCambio, "##,##0.0000")
                    'Else
                    '    CostoPromedioDolar = Format(((Existencia * Costo) + (PrecioCompra * CantidadCompra)) / (Existencia + CantidadCompra), "##,##0.000")
                    '    CostoPromedio = Format(CostoPromedioDolar * TasaCambio, "##,##0.00")
                    'End If
                End If

                ExistenciaTotal = Existencia + CantidadCompra



                '////////////////////////////////////////////ACTUALIZO LA EXISTENCIA DE LA BODEGA////////////////////////////////////////////////////////

                SqlUpdate = "UPDATE [DetalleBodegas]  SET [Existencia] = " & ExistenciaBodega & " ,[Costo] = " & CostoPromedio & " ,[Ultimo_Precio_Compra] = " & PrecioCompra & ",[Existencia_Dinero] = " & (ExistenciaBodega * CostoPromedio) & " ,[Existencia_Unidades] = " & ExistenciaBodega & " ,[Existencia_DineroDolar] = " & (ExistenciaBodega * CostoPromedioDolar) & " " & _
                            "WHERE (Cod_Bodegas = '" & CodBodega & "') AND (Cod_Productos = '" & CodigoProductos & "') "
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()
            Case "Devolucion de Compra"
                '///////////////////////////FORMULA DE COMPRA PROMEDIO////////////////////////////////////////////////////////////////
                ' CostoPromedio= ((Existencia*Costo)+(PrecioCompra*CantidadCompra))/(Existencia+CantidadComprada)
                '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                '///////////////////LA VARIABLE EXISTENCIA TIENE TAMBIEN ACUMULADA LA DEVOLUCION ACTUAL////////////////////////////////////
                'Existencia + CantidadCompra
                Existencia = Math.Abs(Existencia + CantidadCompra)

                If (Existencia - CantidadCompra) <> 0 Then
                    CostoPromedio = Format(((Existencia * Costo) - (PrecioCompra * CantidadCompra)) / (Existencia - CantidadCompra), "##,##0.00")
                Else
                    CostoPromedio = 0
                End If

                ExistenciaTotal = Existencia - CantidadCompra

                '////////////////////////////////////////////ACTUALIZO LA EXISTENCIA DE LA BODEGA////////////////////////////////////////////////////////

                SqlUpdate = "UPDATE [DetalleBodegas]  SET [Existencia] = " & ExistenciaBodega & " ,[Costo] = " & CostoPromedio & " ,[Ultimo_Precio_Compra] = " & PrecioCompra & ",[Existencia_Dinero] = " & (ExistenciaBodega * CostoPromedio) & " ,[Existencia_Unidades] = " & ExistenciaBodega & " ,[Existencia_DineroDolar] = " & (ExistenciaBodega * CostoPromedioDolar) & " " & _
                            "WHERE (Cod_Bodegas = '" & CodBodega & "') AND (Cod_Productos = '" & CodigoProductos & "') "
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()

        End Select



    End Sub

    Public Sub ActualizaMETODOFactura()
        Dim Metodo As String, iPosicion As Double, Registros As Double, Monto As Double
        Dim Subtotal As Double, Iva As Double, Neto As Double, CodProducto As String, SQlString As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), CodIva As String, Tasa As Double, SQlMetodo As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, Moneda As String, TasaCambio As Double
        Dim Fecha As String, SQlTasa As String, TipoMetodo As String, MonedaFactura As String, TasaIva As Double = 0
        Dim Descuento As Double = 0, SqlUpdate As String, Retencion1Porciento As Double = 0, Retencion2Porciento As Double = 0
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, CalcularPropina As Boolean = False, PorcentajePropina As Integer = 0
        Dim MontoPropina As Double = 0

        Registros = FrmFacturas.BindingMetodo.Count
        iPosicion = 0

        Fecha = Format(FrmFacturas.DTPFecha.Value, "yyyy-MM-dd")


        Do While iPosicion < Registros

            If Not IsDBNull(FrmFacturas.BindingMetodo.Item(iPosicion)("NombrePago")) Then
                Metodo = FrmFacturas.BindingMetodo.Item(iPosicion)("NombrePago")
                TasaCambio = 1
                Fecha = Format(FrmFacturas.DTPFecha.Value, "yyyy-MM-dd")
                '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                '//////////////////////////////BUSCO LA MONEDA DEL METODO DE PAGO///////////////////////////////////////////////////////
                '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                MonedaFactura = FrmFacturas.TxtMonedaFactura.Text
                Moneda = "Cordobas"
                TipoMetodo = "Cambio"
                SQlMetodo = "SELECT * FROM MetodoPago WHERE (NombrePago = '" & Metodo & "')"
                DataAdapter = New SqlClient.SqlDataAdapter(SQlMetodo, MiConexion)
                DataAdapter.Fill(DataSet, "Metodo")
                If DataSet.Tables("Metodo").Rows.Count <> 0 Then
                    Moneda = DataSet.Tables("Metodo").Rows(0)("Moneda")
                    TipoMetodo = DataSet.Tables("Metodo").Rows(0)("TipoPago")
                End If
                DataSet.Tables("Metodo").Clear()


                Select Case Moneda
                    Case "Cordobas"
                        If MonedaFactura = "Cordobas" Then
                            TasaCambio = 1
                        Else
                            SQlTasa = "SELECT  * FROM TasaCambio WHERE (FechaTasa = CONVERT(DATETIME, '" & Fecha & "', 102))"
                            DataAdapter = New SqlClient.SqlDataAdapter(SQlTasa, MiConexion)
                            DataAdapter.Fill(DataSet, "TasaCambio")
                            If DataSet.Tables("TasaCambio").Rows.Count <> 0 Then
                                TasaCambio = (1 / DataSet.Tables("TasaCambio").Rows(0)("MontoTasa"))
                            Else
                                'TasaCambio = 0
                                MsgBox("La Tasa de Cambio no Existe para esta Fecha", MsgBoxStyle.Critical, "Sistema Facturacion")
                                FrmFacturas.BindingMetodo.Item(iPosicion)("Monto") = 0
                            End If
                            DataSet.Tables("TasaCambio").Clear()
                        End If

                    Case "Dolares"
                        If MonedaFactura = "Cordobas" Then
                            SQlTasa = "SELECT  * FROM TasaCambio WHERE (FechaTasa = CONVERT(DATETIME, '" & Fecha & "', 102))"
                            DataAdapter = New SqlClient.SqlDataAdapter(SQlTasa, MiConexion)
                            DataAdapter.Fill(DataSet, "TasaCambio")
                            If DataSet.Tables("TasaCambio").Rows.Count <> 0 Then
                                TasaCambio = DataSet.Tables("TasaCambio").Rows(0)("MontoTasa")
                            Else
                                'TasaCambio = 0
                                MsgBox("La Tasa de Cambio no Existe para esta Fecha", MsgBoxStyle.Critical, "Sistema Facturacion")
                                FrmFacturas.BindingMetodo.Item(iPosicion)("Monto") = 0
                            End If
                            DataSet.Tables("TasaCambio").Clear()
                        Else
                            TasaCambio = 1
                        End If
                End Select

                If TipoMetodo = "Cambio" Then
                    TasaCambio = TasaCambio * -1
                End If



                If Not IsDBNull(FrmFacturas.BindingMetodo.Item(iPosicion)("Monto")) Then
                    Monto = (FrmFacturas.BindingMetodo.Item(iPosicion)("Monto") * TasaCambio) + Monto
                Else
                    Monto = 0
                End If

            End If
            iPosicion = iPosicion + 1
        Loop



        '**********************************************************************************************************************************
        '//////////////////////////////BUSCO EL SUB TOTAL Y EL IVA ////////////////////////////////////////////////////////////////////////
        '************************************************************************************************************************************

        Registros = FrmFacturas.BindingDetalle.Count
        iPosicion = 0

        Do While iPosicion < Registros
            If Not IsDBNull(FrmFacturas.BindingDetalle.Item(iPosicion)("Importe")) Then
                Subtotal = Format(CDbl(FrmFacturas.BindingDetalle.Item(iPosicion)("Importe")) + Subtotal, "####0.00")
                If Not IsDBNull(FrmFacturas.BindingDetalle.Item(iPosicion)("Cod_Producto")) Then
                    CodProducto = FrmFacturas.BindingDetalle.Item(iPosicion)("Cod_Producto")
                Else
                    CodProducto = ""
                End If
                SQlString = "SELECT Productos.*  FROM Productos WHERE (Cod_Productos = '" & CodProducto & "') "
                DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
                DataAdapter.Fill(DataSet, "Productos")
                If Not DataSet.Tables("Productos").Rows.Count = 0 Then
                    CodIva = DataSet.Tables("Productos").Rows(0)("Cod_Iva")
                    SQlString = "SELECT *  FROM Impuestos WHERE  (Cod_Iva = '" & CodIva & "')"
                    DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
                    DataAdapter.Fill(DataSet, "IVA")
                    If Not DataSet.Tables("IVA").Rows.Count = 0 Then
                        Tasa = DataSet.Tables("IVA").Rows(0)("Impuesto")
                        TasaIva = DataSet.Tables("IVA").Rows(0)("Impuesto")
                    End If
                    Iva = Format(Iva + CDbl(FrmFacturas.BindingDetalle.Item(iPosicion)("Importe")) * Tasa, "####0.00000000")
                    DataSet.Tables("IVA").Clear()
                End If
                DataSet.Tables("Productos").Clear()


            End If
            iPosicion = iPosicion + 1
        Loop

        '----------------------------------------------------------------------------------------------------------------------------------------
        '///////////////////////////BUSCO SI EXISTE NOTAS DE DEBITO Y CREDITO ///////////////////////////////////////////////////////////////////
        '----------------------------------------------------------------------------------------------------------------------------------------
        Retencion1Porciento = 0
        If FrmFacturas.OptRet1Porciento.Checked = True Then
            SQlString = "SELECT CodigoNB, Tipo, Descripcion, CuentaContable FROM NotaDebito WHERE (Tipo = 'Credito Clientes') AND (Descripcion LIKE N'%1%%')"
            DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
            DataAdapter.Fill(DataSet, "Retencion")
            If DataSet.Tables("Retencion").Rows.Count <> 0 Then
                Retencion1Porciento = Format(Subtotal * 0.01, "##,##0.00")
            Else
                MsgBox("No Existe Nota de Credito para Retencion 1%", MsgBoxStyle.Critical, "Zeus Facturacion")
                FrmFacturas.OptRet1Porciento.Checked = False
            End If
            DataSet.Tables("Retencion").Reset()
        End If

        Retencion2Porciento = 0
        If FrmFacturas.OptRet2Porciento.Checked = True Then
            SQlString = "SELECT CodigoNB, Tipo, Descripcion, CuentaContable FROM NotaDebito WHERE (Tipo = 'Credito Clientes') AND (Descripcion LIKE N'%2%%')"
            DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
            DataAdapter.Fill(DataSet, "Retencion")
            If DataSet.Tables("Retencion").Rows.Count <> 0 Then
                Retencion2Porciento = Format(Subtotal * 0.02, "##,##0.00")
            Else
                MsgBox("No Existe Nota de Credito para Retencion 2%", MsgBoxStyle.Critical, "Zeus Facturacion")
                FrmFacturas.OptRet2Porciento.Checked = False
            End If
            DataSet.Tables("Retencion").Reset()
        End If


        If FrmFacturas.OptExsonerado.Checked = False Then
            'Iva = Subtotal * Tasa
        Else
            Iva = 0
        End If




        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////////////BUSCO SI TIENE CONFIGURADO EFECTIVO DEFAUL/////////////////////////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SQlString = "SELECT  * FROM DatosEmpresa "
        DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
        DataAdapter.Fill(DataSet, "DatosEmpresa")
        If DataSet.Tables("DatosEmpresa").Rows.Count <> 0 Then

            If Not IsDBNull(DataSet.Tables("DatosEmpresa").Rows(0)("CalcularPropina")) Then
                If DataSet.Tables("DatosEmpresa").Rows(0)("CalcularPropina") = True Then
                    If Not IsDBNull(DataSet.Tables("DatosEmpresa").Rows(0)("PorcentajePropina")) Then
                        PorcentajePropina = DataSet.Tables("DatosEmpresa").Rows(0)("PorcentajePropina")
                    Else
                        PorcentajePropina = 0
                    End If


                Else
                    PorcentajePropina = 0
                End If
            End If

            If FrmFacturas.ChkPropina.Checked = True Then
                MontoPropina = Subtotal * (PorcentajePropina / 100)
            End If




            If DataSet.Tables("DatosEmpresa").Rows(0)("MetodoPagoDefecto") = "Efectivo" Then
                '*************************************************************************************************************************
                '//////////////////////////////////BUSCO LA FORMA DE PAGO PARA ESTA FACTURA /////////////////////////////////////////////
                '**************************************************************************************************************************
                SQlString = "SELECT  * FROM MetodoPago WHERE  (TipoPago = 'Efectivo') AND (Moneda = '" & FrmFacturas.TxtMonedaFactura.Text & "')"
                DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
                DataAdapter.Fill(DataSet, "Metodo")
                If DataSet.Tables("Metodo").Rows.Count <> 0 Then

                    Metodo = DataSet.Tables("Metodo").Rows(0)("NombrePago")

                    Registros = FrmFacturas.BindingMetodo.Count
                    iPosicion = 0
                    Fecha = Format(FrmFacturas.DTPFecha.Value, "yyyy-MM-dd")
                    Do While iPosicion < Registros

                        If Not IsDBNull(FrmFacturas.BindingMetodo.Item(iPosicion)("NombrePago")) Then
                            If Metodo = FrmFacturas.BindingMetodo.Item(iPosicion)("NombrePago") Then
                                FrmFacturas.BindingMetodo.Item(iPosicion)("Monto") = (Subtotal + Iva + MontoPropina - Retencion1Porciento - Retencion2Porciento)
                                FrmFacturas.TrueDBGridMetodo.Columns(1).Text = (Subtotal + Iva + MontoPropina - Retencion1Porciento - Retencion2Porciento)
                                Monto = (Subtotal + Iva + MontoPropina)
                            End If
                        End If

                        iPosicion = iPosicion + 1
                    Loop

                End If
            Else
                If Monto = 0 Then
                    Monto = Retencion1Porciento + Retencion2Porciento
                Else
                    Monto = Monto + Retencion1Porciento + Retencion2Porciento
                End If
            End If
        End If







        '**********************************************************************************************************************************
        '/////////////////////////////SI ES CREDITO BORRO LOS METODOS DE PAGO ////////////////////////////////////////////////////////////////////////
        '************************************************************************************************************************************
        If FrmFacturas.RadioButton1.Checked = True Then
            SqlUpdate = "DELETE FROM [Detalle_MetodoFacturas] WHERE (Numero_Factura = '" & FrmFacturas.TxtNumeroEnsamble.Text & "') AND (Tipo_Factura = '" & FrmFacturas.CboTipoProducto.Text & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102))"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
            Monto = 0

            If Retencion1Porciento <> 0 Then
                Monto = Retencion1Porciento
            End If

            If Retencion2Porciento <> 0 Then
                Monto = Monto + Retencion2Porciento
            End If
        End If



        Descuento = CDbl(Val(FrmFacturas.TxtDescuento.Text))


        Neto = CDbl((Format(Subtotal + Iva, "####0.00"))) - Monto - Descuento + MontoPropina
        FrmFacturas.TxtSubTotal.Text = Format(Subtotal, "##,##0.00")
        FrmFacturas.TxtIva.Text = Format(Iva, "##,##0.00")
        FrmFacturas.TxtPagado.Text = Format(Monto, "##,##0.00")
        FrmFacturas.TxtNetoPagar.Text = Format(Neto, "##,##0.00")
        FrmFacturas.TxtPropina.Text = Format(MontoPropina, "##,##0.00")
    End Sub

    Public Sub LimpiarFacturas()
        Dim SqlString As String, DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), SqlDatos As String, FacturaTarea As Boolean
        Dim oDataRow As DataRow

        My.Forms.FrmFacturas.TxtNumeroEnsamble.Text = "-----0-----"
        My.Forms.FrmFacturas.TxtCodigoClientes.Text = ""

        SqlDatos = "SELECT * FROM DatosEmpresa"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlDatos, MiConexion)
        DataAdapter.Fill(DataSet, "DatosEmpresa")
        If Not DataSet.Tables("DatosEmpresa").Rows.Count = 0 Then
            FacturaTarea = DataSet.Tables("DatosEmpresa").Rows(0)("Factura_Tarea")
        End If



        FrmFacturas.DTPFecha.Value = Format(Now, "dd/MM/yyyy")
        FrmFacturas.DTVencimiento.Value = Format(Now, "dd/MM/yyyy")

        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////CARGO LAS BODEGAS////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlString = "SELECT  * FROM   Bodegas"
        MiConexion.Open()
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataSet.Reset()
        DataAdapter.Fill(DataSet, "Bodegas")
        FrmFacturas.CboCodigoBodega.DataSource = DataSet.Tables("Bodegas")
        If Not DataSet.Tables("Bodegas").Rows.Count = 0 Then
            FrmFacturas.CboCodigoBodega.Text = DataSet.Tables("Bodegas").Rows(0)("Cod_Bodega")
        End If
        FrmFacturas.CboCodigoBodega.Columns(0).Caption = "Codigo"
        FrmFacturas.CboCodigoBodega.Columns(1).Caption = "Nombre Bodega"


        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////CARGO LAS FORMA DE PAGO////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlString = "SELECT  NombrePago, Monto,NumeroTarjeta,FechaVence FROM Detalle_MetodoCompras WHERE (Numero_Compra = '-1')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "MetodoPago")
        FrmFacturas.BindingMetodo.DataSource = DataSet.Tables("MetodoPago")
        FrmFacturas.TrueDBGridMetodo.DataSource = FrmFacturas.BindingMetodo
        FrmFacturas.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(1).Width = 110
        FrmFacturas.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(1).Width = 70
        FrmFacturas.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(0).Button = True
        FrmFacturas.TrueDBGridMetodo.Columns(1).NumberFormat = "##,##0.00"
        FrmFacturas.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(2).Visible = False
        FrmFacturas.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(3).Visible = False
        MiConexion.Close()



        If FacturaTarea = True Then
            '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '///////////////////////////////CARGO EL DETALLE DE COMPRAS/////////////////////////////////////////////////////////////////
            '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            'SqlString = "SELECT Productos.Cod_Productos, Productos.Descripcion_Producto, Detalle_Facturas.CodTarea ,Detalle_Facturas.Cantidad, Detalle_Facturas.Precio_Unitario, Detalle_Facturas.Descuento, Detalle_Facturas.Precio_Neto, Detalle_Facturas.Importe, Detalle_Facturas.id_Detalle_Factura,Detalle_Facturas.Costo_Unitario FROM Detalle_Facturas INNER JOIN  Productos ON Detalle_Facturas.Cod_Producto = Productos.Cod_Productos  " & _
            '             "WHERE (Detalle_Facturas.Numero_Factura = '-00001') ORDER BY id_Detalle_Factura "
            'DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            'DataAdapter.Fill(DataSet, "DetalleFactura")
            'FrmFacturas.BindingDetalle.DataSource = DataSet.Tables("DetalleFactura")

            SqlString = "SELECT Detalle_Facturas.Cod_Producto, Detalle_Facturas.Descripcion_Producto, Detalle_Facturas.CodTarea, Detalle_Facturas.Cantidad, Detalle_Facturas.Precio_Unitario, Detalle_Facturas.Descuento, Detalle_Facturas.Precio_Neto, Detalle_Facturas.Importe, Detalle_Facturas.Costo_Unitario,Detalle_Facturas.Numero_Factura,Detalle_Facturas.Fecha_Factura,Detalle_Facturas.Tipo_Factura, Detalle_Facturas.id_Detalle_Factura FROM Detalle_Facturas WHERE (Detalle_Facturas.Numero_Factura = N'-1')"
            'SqlString = "SELECT Productos.Cod_Productos, Productos.Descripcion_Producto, Detalle_Facturas.CodTarea ,Detalle_Facturas.Cantidad, Detalle_Facturas.Precio_Unitario, Detalle_Facturas.Descuento, Detalle_Facturas.Precio_Neto, Detalle_Facturas.Importe, Detalle_Facturas.id_Detalle_Factura,Detalle_Facturas.Costo_Unitario,Detalle_Facturas.Numero_Factura,Detalle_Facturas.Fecha_Factura,Detalle_Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN  Productos ON Detalle_Facturas.Cod_Producto = Productos.Cod_Productos WHERE (Detalle_Facturas.Numero_Factura = N'-1')"
            'DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            'DataAdapter.Fill(DataSet, "DetalleFactura")
            FrmFacturas.ds = New DataSet
            FrmFacturas.da = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            FrmFacturas.CmdBuilder = New SqlClient.SqlCommandBuilder(FrmFacturas.da)
            FrmFacturas.da.Fill(FrmFacturas.ds, "DetalleFactura")
            FrmFacturas.BindingDetalle.DataSource = FrmFacturas.ds.Tables("DetalleFactura")
            FrmFacturas.TrueDBGridComponentes.DataSource = FrmFacturas.BindingDetalle
            FrmFacturas.TrueDBGridComponentes.Columns("Cod_Producto").Caption = "Codigo"
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Cod_Producto").Button = True
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Cod_Producto").Width = 63
            FrmFacturas.TrueDBGridComponentes.Columns("Descripcion_Producto").Caption = "Descripcion"
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Descripcion_Producto").Width = 227
            'FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Descripcion_Producto").Locked = True
            FrmFacturas.TrueDBGridComponentes.Columns("CodTarea").Caption = "Tarea"
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("CodTarea").Width = 54
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("CodTarea").Button = True
            FrmFacturas.TrueDBGridComponentes.Columns("Cantidad").Caption = "Cantidad"
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Cantidad").Width = 54
            FrmFacturas.TrueDBGridComponentes.Columns("Precio_Unitario").Caption = "Precio Unit"
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Precio_Unitario").Width = 62
            FrmFacturas.TrueDBGridComponentes.Columns("Descuento").Caption = "%Desc"
            FrmFacturas.TrueDBGridComponentes.Columns("Descuento").DefaultValue = 0
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Descuento").Width = 43
            FrmFacturas.TrueDBGridComponentes.Columns("Precio_Neto").Caption = "Precio Neto"
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Precio_Neto").Width = 65
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Precio_Neto").Locked = True
            FrmFacturas.TrueDBGridComponentes.Columns("Importe").Caption = "Importe"
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Importe").Width = 61
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Costo_Unitario").Locked = True
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Costo_Unitario").Visible = False
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Numero_Factura").Visible = False
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Fecha_Factura").Visible = False
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Tipo_Factura").Visible = False
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("id_Detalle_Factura").Visible = False
            'FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("CodTarea").Visible = False
            'FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Fecha_Vence").Visible = False

        Else
            '///////////////////////////////////////BUSCO EL DETALLE DE LA FACTURA///////////////////////////////////////////////////////
            'SqlString = "SELECT Productos.Cod_Productos, Detalle_Facturas.Descripcion_Producto, Detalle_Facturas.Cantidad, Detalle_Facturas.Precio_Unitario,Detalle_Facturas.Descuento, Detalle_Facturas.Precio_Neto, Detalle_Facturas.Importe, Detalle_Facturas.id_Detalle_Factura,Detalle_Facturas.Costo_Unitario FROM  Productos INNER JOIN Detalle_Facturas ON Productos.Cod_Productos = Detalle_Facturas.Cod_Producto " & _
            '    "WHERE (Detalle_Facturas.Numero_Factura = '-0001')  ORDER BY id_Detalle_Factura"
            'DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            'DataAdapter.Fill(DataSet, "DetalleFacturas")
            SqlString = "SELECT Detalle_Facturas.Cod_Producto, Detalle_Facturas.Descripcion_Producto, Detalle_Facturas.Cantidad, Detalle_Facturas.Precio_Unitario, Detalle_Facturas.Descuento, Detalle_Facturas.Precio_Neto, Detalle_Facturas.Importe, Detalle_Facturas.Costo_Unitario,Detalle_Facturas.Numero_Factura,Detalle_Facturas.Fecha_Factura,Detalle_Facturas.Tipo_Factura, Detalle_Facturas.id_Detalle_Factura FROM Detalle_Facturas WHERE (Detalle_Facturas.Numero_Factura = N'-1')"
            'SqlString = "SELECT Productos.Cod_Productos, Productos.Descripcion_Producto, Detalle_Facturas.CodTarea ,Detalle_Facturas.Cantidad, Detalle_Facturas.Precio_Unitario, Detalle_Facturas.Descuento, Detalle_Facturas.Precio_Neto, Detalle_Facturas.Importe, Detalle_Facturas.id_Detalle_Factura,Detalle_Facturas.Costo_Unitario,Detalle_Facturas.Numero_Factura,Detalle_Facturas.Fecha_Factura,Detalle_Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN  Productos ON Detalle_Facturas.Cod_Producto = Productos.Cod_Productos WHERE (Detalle_Facturas.Numero_Factura = N'-1')"
            'DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            'DataAdapter.Fill(DataSet, "DetalleFactura")
            FrmFacturas.ds = New DataSet
            FrmFacturas.da = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            FrmFacturas.CmdBuilder = New SqlClient.SqlCommandBuilder(FrmFacturas.da)
            FrmFacturas.da.Fill(FrmFacturas.ds, "DetalleFactura")
            FrmFacturas.BindingDetalle.DataSource = FrmFacturas.ds.Tables("DetalleFactura")
            'FrmFacturas.BindingDetalle.DataSource = DataSet.Tables("DetalleFacturas")
            FrmFacturas.TrueDBGridComponentes.DataSource = FrmFacturas.BindingDetalle
            FrmFacturas.TrueDBGridComponentes.Columns("Cod_Producto").Caption = "Codigo"
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Cod_Producto").Button = True
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Cod_Producto").Width = 74
            FrmFacturas.TrueDBGridComponentes.Columns(1).Caption = "Descripcion"
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Descripcion_Producto").Width = 259
            'FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Descripcion_Producto").Locked = True
            FrmFacturas.TrueDBGridComponentes.Columns("Cantidad").Caption = "Cantidad"
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Cantidad").Width = 64
            FrmFacturas.TrueDBGridComponentes.Columns("Precio_Unitario").Caption = "Precio Unit"
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Precio_Unitario").Width = 62
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Precio_Unitario").Locked = False
            FrmFacturas.TrueDBGridComponentes.Columns("Descuento").Caption = "%Desc"
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Descuento").Width = 43
            FrmFacturas.TrueDBGridComponentes.Columns("Precio_Neto").Caption = "Precio Neto"
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Precio_Neto").Width = 65
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Precio_Neto").Locked = True
            FrmFacturas.TrueDBGridComponentes.Columns("Importe").Caption = "Importe"
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Importe").Width = 61
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Importe").Locked = True
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Costo_Unitario").Visible = False
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Numero_Factura").Visible = False
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Fecha_Factura").Visible = False
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("Tipo_Factura").Visible = False
            FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns("id_Detalle_Factura").Visible = False

        End If

        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////////////BUSCO SI TIENE CONFIGURADO EFECTIVO DEFAUL/////////////////////////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlString = "SELECT  * FROM DatosEmpresa "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "DatosEmpresa")
        If DataSet.Tables("DatosEmpresa").Rows.Count <> 0 Then
            If DataSet.Tables("DatosEmpresa").Rows(0)("MetodoPagoDefecto") = "Efectivo" Then
                FrmFacturas.RadioButton2.Checked = True
                '        '*************************************************************************************************************************
                '        '//////////////////////////////////BUSCO LA FORMA DE PAGO PARA ESTA FACTURA /////////////////////////////////////////////
                '        '**************************************************************************************************************************
                SqlString = "SELECT  * FROM MetodoPago WHERE  (TipoPago = 'Efectivo') AND (Moneda = '" & FrmFacturas.TxtMonedaFactura.Text & "')"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "Metodo")
                If DataSet.Tables("Metodo").Rows.Count <> 0 Then
                    '************************************************************************************************************************
                    '  ///////////////////////////AGREO LA FORMA DE PAGO DEL TOTAL //////////////////////////////////////////////////////////
                    '*************************************************************************************************************************
                    SqlString = "SELECT  NombrePago, Monto,NumeroTarjeta,FechaVence FROM Detalle_MetodoFacturas WHERE (Numero_Factura = '-1')"
                    DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                    DataAdapter.Fill(DataSet, "MetodoPago")

                    oDataRow = DataSet.Tables("MetodoPago").NewRow
                    oDataRow("NombrePago") = DataSet.Tables("Metodo").Rows(0)("NombrePago")
                    If FrmFacturas.TxtNetoPagar.Text <> "" Then
                        oDataRow("Monto") = FrmFacturas.TxtNetoPagar.Text
                    Else
                        oDataRow("Monto") = 0
                    End If
                    oDataRow("NumeroTarjeta") = 0
                    oDataRow("FechaVence") = FrmFacturas.DTVencimiento.Value
                    DataSet.Tables("MetodoPago").Rows.Add(oDataRow)

                    FrmFacturas.BindingMetodo.DataSource = DataSet.Tables("MetodoPago")
                    FrmFacturas.TrueDBGridMetodo.DataSource = FrmFacturas.BindingMetodo
                    FrmFacturas.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(1).Width = 110
                    FrmFacturas.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(1).Width = 70
                    FrmFacturas.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(0).Button = True
                    FrmFacturas.TrueDBGridMetodo.Columns(1).NumberFormat = "##,##0.00"
                    FrmFacturas.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(2).Visible = False
                    FrmFacturas.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(3).Visible = False


                Else
                    FrmFacturas.RadioButton2.Checked = False
                End If
            End If
        End If
        'FrmFacturas.RadioButton1.Checked = True
        My.Forms.FrmFacturas.CboTipoProducto.Enabled = True
        My.Forms.FrmFacturas.TxtSubTotal.Text = ""
        My.Forms.FrmFacturas.TxtIva.Text = ""
        My.Forms.FrmFacturas.TxtPagado.Text = ""
        My.Forms.FrmFacturas.TxtNetoPagar.Text = ""
        My.Forms.FrmFacturas.TxtObservaciones.Text = ""
        My.Forms.FrmFacturas.TxtDescuento.Text = ""
        My.Forms.FrmFacturas.CboProyecto.Text = ""
        My.Forms.FrmFacturas.GroupBox2.Enabled = True
        My.Forms.FrmFacturas.GroupBox3.Enabled = True
        My.Forms.FrmFacturas.DTVencimiento.Enabled = True
        My.Forms.FrmFacturas.CboCodigoBodega.Enabled = True
        My.Forms.FrmFacturas.CboCodigoBodega2.Enabled = True
        My.Forms.FrmFacturas.CboCodigoVendedor.Enabled = True
        My.Forms.FrmFacturas.CboProyecto.Enabled = True
        My.Forms.FrmFacturas.ChkPorcientoTarjeta.Enabled = True
        My.Forms.FrmFacturas.CboCajero.Enabled = True
        My.Forms.FrmFacturas.TxtMonedaFactura.Enabled = True
        My.Forms.FrmFacturas.TxtMonedaImprime.Enabled = True
        My.Forms.FrmFacturas.Button3.Enabled = True
        My.Forms.FrmFacturas.TxtObservaciones.Enabled = True
        My.Forms.FrmFacturas.C1Button5.Enabled = True
        My.Forms.FrmFacturas.C1Button4.Enabled = True
        My.Forms.FrmFacturas.Button1.Enabled = True
        My.Forms.FrmFacturas.DTPFecha.Enabled = True
        My.Forms.FrmFacturas.C1Button1.Enabled = True
        My.Forms.FrmFacturas.C1Button2.Enabled = True
        My.Forms.FrmFacturas.C1Button3.Enabled = True
        My.Forms.FrmFacturas.CboTipoProducto.Enabled = True
        My.Forms.FrmFacturas.TrueDBGridComponentes.Splits.Item(0).Locked = False
        My.Forms.FrmFacturas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Button = True
        PrimerRegistroFactura = True
    End Sub

    Public Sub GrabaMetodoDetalleFactura(ByVal ConsecutivoFactura As String, ByVal NombrePago As String, ByVal Monto As Double, ByVal NumeroTarjeta As String, ByVal FechaVence As String)
        Dim Sqldetalle As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String, FechaVencimiento As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter

        Fecha = Format(FrmFacturas.DTPFecha.Value, "yyyy-MM-dd")
        FechaVencimiento = Format(CDate(FechaVence), "dd/MM/yyyy")


        Sqldetalle = "SELECT *  FROM Detalle_MetodoFacturas WHERE (Numero_Factura = '" & ConsecutivoFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = '" & FrmFacturas.CboTipoProducto.Text & "') AND (NombrePago = '" & NombrePago & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(Sqldetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleMetodoFactura")
        If Not DataSet.Tables("DetalleMetodoFactura").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlUpdate = "UPDATE [Detalle_MetodoFacturas] SET [NombrePago] = '" & NombrePago & "',[Monto] = " & Monto & ",[NumeroTarjeta] = '" & NumeroTarjeta & "',[FechaVence] = '" & FechaVencimiento & "' " & _
                         "WHERE (Numero_Factura = '" & ConsecutivoFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = '" & FrmFacturas.CboTipoProducto.Text & "') AND (NombrePago = '" & NombrePago & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            MiConexion.Close()
            SqlUpdate = "INSERT INTO [Detalle_MetodoFacturas] ([Numero_Factura],[Fecha_Factura],[Tipo_Factura],[NombrePago],[Monto],[NumeroTarjeta] ,[FechaVence]) " & _
                        "VALUES ('" & ConsecutivoFactura & "','" & FrmFacturas.DTPFecha.Value & "','" & FrmFacturas.CboTipoProducto.Text & "','" & NombrePago & "'," & Monto & " ,'" & NumeroTarjeta & "','" & FechaVencimiento & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub
    Public Sub GrabaDetalleFacturaTarea(ByVal ConsecutivoFactura As String, ByVal CodProducto As String, ByVal Descripcion_Producto As String, ByVal PrecioUnitario As Double, ByVal Descuento As Double, ByVal PrecioNeto As Double, ByVal Importe As Double, ByVal Cantidad As Double, ByVal IdDetalleFactura As Double, ByVal Tarea As String)
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String, TasaCambio As Double
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, MonedaFactura As String, MonedaProducto As String
        Dim SqlDetalle As String

        Descripcion_Producto = Replace(Descripcion_Producto, "'", "")



        MonedaFactura = FrmFacturas.TxtMonedaFactura.Text
        MonedaProducto = "Cordobas"

        If MonedaFactura = "Cordobas" Then
            If MonedaProducto = "Cordobas" Then
                TasaCambio = 1
            Else
                If BuscaTasaCambio(FrmFacturas.DTPFecha.Value) <> 0 Then
                    'TasaCambio = (1 / BuscaTasaCambio(FrmFacturas.DTPFecha.Value))
                    TasaCambio = (1 / BuscaTasaCambio(Now))
                End If
            End If
        ElseIf MonedaFactura = "Dolares" Then
            If MonedaProducto = "Cordobas" Then
                'TasaCambio = BuscaTasaCambio(FrmFacturas.DTPFecha.Value)
                TasaCambio = BuscaTasaCambio(Now)
            Else
                TasaCambio = 1
            End If
        End If



        Fecha = Format(FrmFacturas.DTPFecha.Value, "yyyy-MM-dd")


        SqlDetalle = "SELECT *  FROM Detalle_Facturas WHERE (Numero_Factura = '" & ConsecutivoFactura & "') AND (Tipo_Factura = '" & FrmFacturas.CboTipoProducto.Text & "') AND (Cod_Producto = '" & CodProducto & "') AND (Descripcion_Producto = '" & Descripcion_Producto & "') AND (id_Detalle_Factura = '" & IdDetalleFactura & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlDetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleFactura")
        If Not DataSet.Tables("DetalleFactura").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            'AND (Descripcion_Producto = '" & Descripcion_Producto & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102))
            SqlUpdate = "UPDATE [Detalle_Facturas] SET [Cantidad] = " & Cantidad & " ,[Precio_Unitario] = " & PrecioUnitario & ",[Descuento] = " & Descuento & " ,[Precio_Neto] = " & PrecioNeto & ",[Importe] = " & Importe & ",[Descripcion_Producto] = '" & Descripcion_Producto & "',[CodTarea] = '" & Tarea & "' " & _
                        "WHERE (Numero_Factura = '" & ConsecutivoFactura & "')  AND (Tipo_Factura = '" & FrmFacturas.CboTipoProducto.Text & "') AND (Cod_Producto = '" & CodProducto & "')  AND (Descripcion_Producto = '" & Descripcion_Producto & "') AND (id_Detalle_Factura = '" & IdDetalleFactura & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else

            SqlUpdate = "INSERT INTO [Detalle_Facturas] ([Numero_Factura],[Fecha_Factura],[Tipo_Factura],[Cod_Producto],[Cantidad],[Precio_Unitario],[Descuento],[Precio_Neto],[Importe],[TasaCambio],[Descripcion_Producto],[CodTarea]) " & _
            "VALUES ('" & ConsecutivoFactura & "','" & FrmFacturas.DTPFecha.Value & "','" & FrmFacturas.CboTipoProducto.Text & "','" & CodProducto & "' ," & Cantidad & "," & PrecioUnitario & "," & Descuento & " ," & PrecioNeto & "," & Importe & "," & TasaCambio & ",'" & Descripcion_Producto & "','" & Tarea & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub
    Public Sub GrabaDetalleFacturaLotes(ByVal ConsecutivoFactura As String, ByVal CodProducto As String, ByVal Descripcion_Producto As String, ByVal PrecioUnitario As Double, ByVal Descuento As Double, ByVal PrecioNeto As Double, ByVal Importe As Double, ByVal Cantidad As Double, ByVal IdDetalleFactura As Double, ByVal Tarea As String, ByVal FechaVence As Date)
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String, TasaCambio As Double
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, MonedaFactura As String, MonedaProducto As String
        Dim SqlDetalle As String

        Descripcion_Producto = Replace(Descripcion_Producto, "'", "")



        MonedaFactura = FrmFacturas.TxtMonedaFactura.Text
        MonedaProducto = "Cordobas"

        If MonedaFactura = "Cordobas" Then
            If MonedaProducto = "Cordobas" Then
                TasaCambio = 1
            Else
                If BuscaTasaCambio(FrmFacturas.DTPFecha.Value) <> 0 Then
                    'TasaCambio = (1 / BuscaTasaCambio(FrmFacturas.DTPFecha.Value))
                    TasaCambio = (1 / BuscaTasaCambio(Now))
                End If
            End If
        ElseIf MonedaFactura = "Dolares" Then
            If MonedaProducto = "Cordobas" Then
                'TasaCambio = BuscaTasaCambio(FrmFacturas.DTPFecha.Value)
                TasaCambio = BuscaTasaCambio(Now)
            Else
                TasaCambio = 1
            End If
        End If



        Fecha = Format(FrmFacturas.DTPFecha.Value, "yyyy-MM-dd")


        SqlDetalle = "SELECT *  FROM Detalle_Facturas WHERE (Numero_Factura = '" & ConsecutivoFactura & "') AND (Tipo_Factura = '" & FrmFacturas.CboTipoProducto.Text & "') AND (Cod_Producto = '" & CodProducto & "') AND (Descripcion_Producto = '" & Descripcion_Producto & "') AND (id_Detalle_Factura = '" & IdDetalleFactura & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlDetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleFactura")
        If Not DataSet.Tables("DetalleFactura").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            'AND (Descripcion_Producto = '" & Descripcion_Producto & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102))
            SqlUpdate = "UPDATE [Detalle_Facturas] SET [Cantidad] = " & Cantidad & " ,[Precio_Unitario] = " & PrecioUnitario & ",[Descuento] = " & Descuento & " ,[Precio_Neto] = " & PrecioNeto & ",[Importe] = " & Importe & ",[Descripcion_Producto] = '" & Descripcion_Producto & "',[CodTarea] = '" & Tarea & "', [Numero_Lote] = '" & Tarea & "', [Fecha_Vence] = '" & Format(FechaVence, "dd/MM/yyyy") & "' " & _
                        "WHERE (Numero_Factura = '" & ConsecutivoFactura & "')  AND (Tipo_Factura = '" & FrmFacturas.CboTipoProducto.Text & "') AND (Cod_Producto = '" & CodProducto & "')  AND (Descripcion_Producto = '" & Descripcion_Producto & "') AND (id_Detalle_Factura = '" & IdDetalleFactura & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else

            SqlUpdate = "INSERT INTO [Detalle_Facturas] ([Numero_Factura],[Fecha_Factura],[Tipo_Factura],[Cod_Producto],[Cantidad],[Precio_Unitario],[Descuento],[Precio_Neto],[Importe],[TasaCambio],[Descripcion_Producto],[CodTarea],[Numero_Lote],[Fecha_Vence]) " & _
            "VALUES ('" & ConsecutivoFactura & "','" & FrmFacturas.DTPFecha.Value & "','" & FrmFacturas.CboTipoProducto.Text & "','" & CodProducto & "' ," & Cantidad & "," & PrecioUnitario & "," & Descuento & " ," & PrecioNeto & "," & Importe & "," & TasaCambio & ",'" & Descripcion_Producto & "','" & Tarea & "','" & Tarea & "','" & Format(FechaVence, "dd/MM/yyyy") & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub

    'Public Sub GrabaDetalleFactura(ByVal ConsecutivoFactura As String, ByVal CodProducto As String, ByVal Descripcion_Producto As String, ByVal PrecioUnitario As Double, ByVal Descuento As Double, ByVal PrecioNeto As Double, ByVal Importe As Double, ByVal Cantidad As Double, ByVal IdDetalleFactura As Double)
    '    Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
    '    Dim Fecha As String, MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String, TasaCambio As Double
    '    Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, MonedaFactura As String, MonedaProducto As String
    '    Dim SqlDetalle As String

    '    Descripcion_Producto = Replace(Descripcion_Producto, "'", "")



    '    MonedaFactura = FrmFacturas.TxtMonedaFactura.Text
    '    MonedaProducto = "Cordobas"

    '    If MonedaFactura = "Cordobas" Then
    '        If MonedaProducto = "Cordobas" Then
    '            TasaCambio = 1
    '        Else
    '            If BuscaTasaCambio(FrmFacturas.DTPFecha.Value) <> 0 Then
    '                TasaCambio = (1 / BuscaTasaCambio(FrmFacturas.DTPFecha.Value))
    '            End If
    '        End If
    '    ElseIf MonedaFactura = "Dolares" Then
    '        If MonedaProducto = "Cordobas" Then
    '            TasaCambio = BuscaTasaCambio(FrmFacturas.DTPFecha.Value)
    '        Else
    '            TasaCambio = 1
    '        End If
    '    End If



    '    Fecha = Format(FrmFacturas.DTPFecha.Value, "yyyy-MM-dd")

    '    'SqlUpdate = "INSERT INTO [Detalle_Facturas] ([Numero_Factura],[Fecha_Factura],[Tipo_Factura],[Cod_Producto],[Cantidad],[Precio_Unitario],[Descuento],[Precio_Neto],[Importe],[TasaCambio],[Descripcion_Producto]) " & _
    '    '"VALUES ('" & ConsecutivoFactura & "','" & FrmFacturas.DTPFecha.Value & "','" & FrmFacturas.CboTipoProducto.Text & "','" & CodProducto & "' ," & Cantidad & "," & PrecioUnitario & "," & Descuento & " ," & PrecioNeto & "," & Importe & "," & TasaCambio & ",'" & Descripcion_Producto & "')"
    '    'MiConexion.Open()
    '    'ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
    '    'iResultado = ComandoUpdate.ExecuteNonQuery
    '    'MiConexion.Close()

    '    'AND (Descripcion_Producto = '" & Descripcion_Producto & "')

    '    SqlDetalle = "SELECT *  FROM Detalle_Facturas WHERE (Numero_Factura = '" & ConsecutivoFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = '" & FrmFacturas.CboTipoProducto.Text & "') AND (Cod_Producto = '" & CodProducto & "') AND (Descripcion_Producto = '" & Descripcion_Producto & "') AND (id_Detalle_Factura = '" & IdDetalleFactura & "')"
    '    DataAdapter = New SqlClient.SqlDataAdapter(SqlDetalle, MiConexion)
    '    DataAdapter.Fill(DataSet, "DetalleFactura")
    '    If Not DataSet.Tables("DetalleFactura").Rows.Count = 0 Then
    '        '//////////////////////////////////////////////////////////////////////////////////////////////
    '        '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
    '        '/////////////////////////////////////////////////////////////////////////////////////////////////
    '        'AND (Descripcion_Producto = '" & Descripcion_Producto & "')
    '        SqlUpdate = "UPDATE [Detalle_Facturas] SET [Cantidad] = " & Cantidad & " ,[Precio_Unitario] = " & PrecioUnitario & ",[Descuento] = " & Descuento & " ,[Precio_Neto] = " & PrecioNeto & ",[Importe] = " & Importe & ",[Descripcion_Producto] = '" & Descripcion_Producto & "' " & _
    '                    "WHERE (Numero_Factura = '" & ConsecutivoFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = '" & FrmFacturas.CboTipoProducto.Text & "') AND (Cod_Producto = '" & CodProducto & "')  AND (Descripcion_Producto = '" & Descripcion_Producto & "') AND (id_Detalle_Factura = '" & IdDetalleFactura & "')"
    '        MiConexion.Open()
    '        ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
    '        iResultado = ComandoUpdate.ExecuteNonQuery
    '        MiConexion.Close()

    '    Else

    '        SqlUpdate = "INSERT INTO [Detalle_Facturas] ([Numero_Factura],[Fecha_Factura],[Tipo_Factura],[Cod_Producto],[Cantidad],[Precio_Unitario],[Descuento],[Precio_Neto],[Importe],[TasaCambio],[Descripcion_Producto]) " & _
    '        "VALUES ('" & ConsecutivoFactura & "','" & FrmFacturas.DTPFecha.Value & "','" & FrmFacturas.CboTipoProducto.Text & "','" & CodProducto & "' ," & Cantidad & "," & PrecioUnitario & "," & Descuento & " ," & PrecioNeto & "," & Importe & "," & TasaCambio & ",'" & Descripcion_Producto & "')"
    '        MiConexion.Open()
    '        ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
    '        iResultado = ComandoUpdate.ExecuteNonQuery
    '        MiConexion.Close()

    '    End If

    'End Sub

    Public Sub GrabaDetalleFactura(ByVal ConsecutivoFactura As String, ByVal CodProducto As String, ByVal Descripcion_Producto As String, ByVal PrecioUnitario As Double, ByVal Descuento As Double, ByVal PrecioNeto As Double, ByVal Importe As Double, ByVal Cantidad As Double, ByVal IdDetalleFactura As Double, ByVal CostoUnitario As Double)
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String, TasaCambio As Double
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, MonedaFactura As String, MonedaProducto As String
        Dim SqlDetalle As String

        Descripcion_Producto = Replace(Descripcion_Producto, "'", "")



        MonedaFactura = FrmFacturas.TxtMonedaFactura.Text
        MonedaProducto = "Cordobas"

        If MonedaFactura = "Cordobas" Then
            If MonedaProducto = "Cordobas" Then
                TasaCambio = 1
            Else
                If BuscaTasaCambio(FrmFacturas.DTPFecha.Value) <> 0 Then
                    'TasaCambio = (1 / BuscaTasaCambio(FrmFacturas.DTPFecha.Value))
                    TasaCambio = (1 / BuscaTasaCambio(Now))
                End If
            End If
        ElseIf MonedaFactura = "Dolares" Then
            If MonedaProducto = "Cordobas" Then
                'TasaCambio = BuscaTasaCambio(FrmFacturas.DTPFecha.Value)
                TasaCambio = BuscaTasaCambio(Now)
            Else
                TasaCambio = 1
            End If
        End If



        Fecha = Format(FrmFacturas.DTPFecha.Value, "yyyy-MM-dd")

        'SqlUpdate = "INSERT INTO [Detalle_Facturas] ([Numero_Factura],[Fecha_Factura],[Tipo_Factura],[Cod_Producto],[Cantidad],[Precio_Unitario],[Descuento],[Precio_Neto],[Importe],[TasaCambio],[Descripcion_Producto]) " & _
        '"VALUES ('" & ConsecutivoFactura & "','" & FrmFacturas.DTPFecha.Value & "','" & FrmFacturas.CboTipoProducto.Text & "','" & CodProducto & "' ," & Cantidad & "," & PrecioUnitario & "," & Descuento & " ," & PrecioNeto & "," & Importe & "," & TasaCambio & ",'" & Descripcion_Producto & "')"
        'MiConexion.Open()
        'ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
        'iResultado = ComandoUpdate.ExecuteNonQuery
        'MiConexion.Close()

        'AND (Descripcion_Producto = '" & Descripcion_Producto & "')

        SqlDetalle = "SELECT *  FROM Detalle_Facturas WHERE (Numero_Factura = '" & ConsecutivoFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = '" & FrmFacturas.CboTipoProducto.Text & "') AND (Cod_Producto = '" & CodProducto & "')  AND (id_Detalle_Factura = '" & IdDetalleFactura & "')"   'AND (Descripcion_Producto = '" & Descripcion_Producto & "')
        DataAdapter = New SqlClient.SqlDataAdapter(SqlDetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleFactura")
        If Not DataSet.Tables("DetalleFactura").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            'AND (Descripcion_Producto = '" & Descripcion_Producto & "')
            SqlUpdate = "UPDATE [Detalle_Facturas] SET [Cantidad] = " & Cantidad & " ,[Precio_Unitario] = " & PrecioUnitario & ",[Descuento] = " & Descuento & " ,[Precio_Neto] = " & PrecioNeto & ",[Importe] = " & Importe & ",[Descripcion_Producto] = '" & Descripcion_Producto & "',[TasaCambio]= " & TasaCambio & " ,[Costo_Unitario]= " & CostoUnitario & " " & _
                        "WHERE (Numero_Factura = '" & ConsecutivoFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = '" & FrmFacturas.CboTipoProducto.Text & "') AND (Cod_Producto = '" & CodProducto & "')  AND (id_Detalle_Factura = '" & IdDetalleFactura & "')"  'AND (Descripcion_Producto = '" & Descripcion_Producto & "')
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else

            SqlUpdate = "INSERT INTO [Detalle_Facturas] ([Numero_Factura],[Fecha_Factura],[Tipo_Factura],[Cod_Producto],[Cantidad],[Precio_Unitario],[Descuento],[Precio_Neto],[Importe],[TasaCambio],[Descripcion_Producto],[Costo_Unitario]) " & _
            "VALUES ('" & ConsecutivoFactura & "','" & FrmFacturas.DTPFecha.Value & "','" & FrmFacturas.CboTipoProducto.Text & "','" & CodProducto & "' ," & Cantidad & "," & PrecioUnitario & "," & Descuento & " ," & PrecioNeto & "," & Importe & "," & TasaCambio & ",'" & Descripcion_Producto & "'," & CostoUnitario & ")"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub
    Public Sub GrabaDetalleTransferenciaEntrada(ByVal ConsecutivoCompra As String, ByVal CodProducto As String, ByVal Descripcion_Producto As String, ByVal PrecioUnitario As Double, ByVal Descuento As Double, ByVal PrecioNeto As Double, ByVal Importe As Double, ByVal Cantidad As Double, ByVal IdDetalleCompra As Double, ByVal FechaTransferencia As Date, ByVal TipoCompra As String, ByVal Numero_Lote As String)
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String, TasaCambio As Double
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim SqlDetalle As String

        TasaCambio = BuscaTasaCambio(FechaTransferencia)
        Descripcion_Producto = Replace(Descripcion_Producto, "'", "")
        Fecha = Format(FechaTransferencia, "yyyy-MM-dd")


        SqlDetalle = "SELECT *  FROM Detalle_Compras WHERE (Numero_Compra = '" & ConsecutivoCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & TipoCompra & "') AND (Cod_Producto = '" & CodProducto & "') AND (id_Detalle_Transferencia = " & IdDetalleCompra & ")"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlDetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleCompra")
        If Not DataSet.Tables("DetalleCompra").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlUpdate = "UPDATE [Detalle_Compras] SET [Cantidad] = " & Cantidad & " ,[Precio_Unitario] = " & PrecioUnitario & ",[Descuento] = " & Descuento & " ,[Precio_Neto] = " & PrecioNeto & ",[Importe] = " & Importe & ",[TasaCambio] = " & TasaCambio & ",[Numero_Lote] = '" & Numero_Lote & "'  " & _
                        "WHERE (Numero_Compra = '" & ConsecutivoCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & TipoCompra & "') AND (Cod_Producto = '" & CodProducto & "') AND (id_Detalle_Transferencia = " & IdDetalleCompra & ")"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else

            SqlUpdate = "INSERT INTO [Detalle_Compras] ([Numero_Compra],[Fecha_Compra],[Tipo_Compra],[Cod_Producto],[Cantidad],[Precio_Unitario],[Descuento],[Precio_Neto],[Importe],[TasaCambio],[id_Detalle_Transferencia],[Numero_Lote]) " & _
            "VALUES ('" & ConsecutivoCompra & "','" & FechaTransferencia & "','" & TipoCompra & "','" & CodProducto & "' ," & Cantidad & "," & PrecioUnitario & "," & Descuento & " ," & PrecioNeto & "," & Importe & "," & TasaCambio & "," & IdDetalleCompra & ",'" & Numero_Lote & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If


    End Sub



    Public Sub GrabaDetalleTransferenciaSalida(ByVal ConsecutivoFactura As String, ByVal CodProducto As String, ByVal Descripcion_Producto As String, ByVal PrecioUnitario As Double, ByVal Descuento As Double, ByVal PrecioNeto As Double, ByVal Importe As Double, ByVal Cantidad As Double, ByVal IdDetalleFactura As Double, ByVal FechaTransferencia As Date, ByVal TipoFactura As String, ByVal Numero_Lote As String)
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String, TasaCambio As Double
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim SqlDetalle As String

        Descripcion_Producto = Replace(Descripcion_Producto, "'", "")
        Fecha = Format(FechaTransferencia, "yyyy-MM-dd")

        SqlDetalle = "SELECT *  FROM Detalle_Facturas WHERE (Numero_Factura = '" & ConsecutivoFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = '" & TipoFactura & "') AND (Cod_Producto = '" & CodProducto & "') AND (Descripcion_Producto = '" & Descripcion_Producto & "') AND (id_Detalle_Factura = '" & IdDetalleFactura & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlDetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleFactura")
        If Not DataSet.Tables("DetalleFactura").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            'AND (Descripcion_Producto = '" & Descripcion_Producto & "')
            SqlUpdate = "UPDATE [Detalle_Facturas] SET [Cantidad] = " & Cantidad & " ,[Precio_Unitario] = " & PrecioUnitario & ",[Descuento] = " & Descuento & " ,[Precio_Neto] = " & PrecioNeto & ",[Importe] = " & Importe & ",[Descripcion_Producto] = '" & Descripcion_Producto & "',[CodTarea] = '" & Numero_Lote & "' " & _
                        "WHERE (Numero_Factura = '" & ConsecutivoFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = '" & TipoFactura & "') AND (Cod_Producto = '" & CodProducto & "')  AND (Descripcion_Producto = '" & Descripcion_Producto & "') AND (id_Detalle_Factura = '" & IdDetalleFactura & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else

            SqlUpdate = "INSERT INTO [Detalle_Facturas] ([Numero_Factura],[Fecha_Factura],[Tipo_Factura],[Cod_Producto],[Cantidad],[Precio_Unitario],[Descuento],[Precio_Neto],[Importe],[TasaCambio],[Descripcion_Producto],[CodTarea]) " & _
            "VALUES ('" & ConsecutivoFactura & "','" & FechaTransferencia & "','" & TipoFactura & "','" & CodProducto & "' ," & Cantidad & "," & PrecioUnitario & "," & Descuento & " ," & PrecioNeto & "," & Importe & "," & TasaCambio & ",'" & Descripcion_Producto & "','" & Numero_Lote & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub


    Public Sub GrabaFacturas(ByVal ConsecutivoFactura As String)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, MetodoPago As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Subtotal As Double, Iva As Double, Neto As Double, Pagado As Double, Descuento As Double, Exonerado As Boolean
        Dim MonedaFactura As String = My.Forms.FrmFacturas.TxtMonedaFactura.Text
        Dim MonedaImprime As String = My.Forms.FrmFacturas.TxtMonedaImprime.Text, CodigoProyecto As String
        Dim SqlDetalle As String, MontoRetencion1Porciento As Double = 0, MontoRetencion2Porciento As Double = 0, Retener1 As Boolean, Retener2 As Boolean
        Dim Referencia As String = "", FechaHora As Date


        Try



            Fecha = Format(FrmFacturas.DTPFecha.Value, "yyyy-MM-dd")


            If FrmFacturas.TxtSubTotal.Text <> "" Then
                Subtotal = FrmFacturas.TxtSubTotal.Text
            Else
                Subtotal = 0
            End If

            If FrmFacturas.TxtIva.Text <> "" Then
                Iva = FrmFacturas.TxtIva.Text
            Else
                Iva = 0
            End If

            If FrmFacturas.TxtPagado.Text <> "" Then
                Pagado = FrmFacturas.TxtPagado.Text
            Else
                Pagado = 0
            End If

            If FrmFacturas.TxtNetoPagar.Text <> "" Then
                Neto = FrmFacturas.TxtNetoPagar.Text
            Else
                Neto = 0
            End If

            If FrmFacturas.RadioButton1.Checked = True Then
                MetodoPago = "Credito"
            Else
                MetodoPago = "Contado"
            End If

            If FrmFacturas.OptExsonerado.Checked = True Then
                Exonerado = True
            Else
                Exonerado = False
            End If

            If FrmFacturas.OptRet1Porciento.Checked = True Then
                MontoRetencion1Porciento = Subtotal * 0.01
                Retener1 = True
            Else
                MontoRetencion1Porciento = 0
                Retener1 = False
            End If

            If FrmFacturas.OptRet2Porciento.Checked = True Then
                MontoRetencion2Porciento = Subtotal * 0.02
                Retener2 = True
            Else
                MontoRetencion2Porciento = 0
                Retener2 = False
            End If

            CodigoProyecto = ""
            If Not FrmFacturas.CboProyecto.Text = "" Then
                CodigoProyecto = FrmFacturas.CboProyecto.Columns(0).Text
            End If

            If FrmFacturas.CboReferencia.Text <> "" Then
                Referencia = FrmFacturas.CboReferencia.Text
            End If

            CambiarFechaFactura = False

            If ExisteFactura(Fecha, ConsecutivoFactura, FrmFacturas.CboTipoProducto.Text) = "ExisteFacturaDifFecha" Then

                If MsgBox("Existe Diferencia de Fechas, �Desea Grabar este Cambio?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                    CambioFechaRespuesta = True
                    CambiarFechaFactura = True
                    '///////////////////////////////////////////////////////////////////////////////////////////////////////////
                    '///////////////////////////////////BORRO EL METODO DE FACTURA /////////////////////////////////////////////
                    '/////////////////////////////////////////////
                    MiConexion.Close()
                    SqlCompras = "DELETE FROM [Detalle_MetodoFacturas] WHERE  (Numero_Factura = '" & ConsecutivoFactura & "') AND (Tipo_Factura = '" & FrmFacturas.CboTipoProducto.Text & "')"
                    MiConexion.Open()
                    ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
                    iResultado = ComandoUpdate.ExecuteNonQuery
                    MiConexion.Close()

                    '///////////////////////////////////////////////////////////////////////////////////////////////////////////
                    '///////////////////////////////////BORRO EL DETALLE DE FACTURA /////////////////////////////////////////////
                    '/////////////////////////////////////////////
                    MiConexion.Close()
                    SqlCompras = "DELETE FROM [Detalle_Facturas] WHERE  (Numero_Factura = '" & ConsecutivoFactura & "') AND (Tipo_Factura = '" & FrmFacturas.CboTipoProducto.Text & "')"
                    MiConexion.Open()
                    ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
                    iResultado = ComandoUpdate.ExecuteNonQuery
                    MiConexion.Close()

                    '///////////////////////////////////////////////////////////////////////////////////////////////////////////
                    '///////////////////////////////////BORRO LA FACTURA /////////////////////////////////////////////
                    '/////////////////////////////////////////////
                    MiConexion.Close()
                    SqlCompras = "DELETE FROM [Facturas] WHERE  (Numero_Factura = '" & ConsecutivoFactura & "') AND (Tipo_Factura = '" & FrmFacturas.CboTipoProducto.Text & "')"
                    MiConexion.Open()
                    ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
                    iResultado = ComandoUpdate.ExecuteNonQuery
                    MiConexion.Close()

                Else
                    CambiarFechaFactura = False
                    CambioFechaRespuesta = False

                    FrmFacturas.DTPFecha.Value = FechaFacturacion

                End If


            End If


            FechaHora = FrmFacturas.DTPFecha.Value & " " & Format(Now, "HH:mm")

            Descuento = CDbl(Val(FrmFacturas.TxtDescuento.Text))

            MiConexion.Close()

            'SqlDetalle = "SELECT Facturas.* FROM Facturas WHERE  (Numero_Factura = '" & ConsecutivoFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = '" & FrmFacturas.CboTipoProducto.Text & "')"
            SqlDetalle = "SELECT Facturas.* FROM Facturas WHERE  (Numero_Factura = '" & ConsecutivoFactura & "') AND (Tipo_Factura = '" & FrmFacturas.CboTipoProducto.Text & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlDetalle, MiConexion)
            DataAdapter.Fill(DataSet, "DetalleFactura")
            If DataSet.Tables("DetalleFactura").Rows.Count = 0 Then
                'If FrmFacturas.TxtNumeroEnsamble.Text = "-----0-----" Then
                '//////////////////////////////////////////////////////////////////////////////////////////////
                '////////////////////////////AGREGO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
                '/////////////////////////////////////////////////////////////////////////////////////////////////
                MiConexion.Close()
                SqlCompras = "INSERT INTO [Facturas] ([Numero_Factura] ,[Fecha_Factura],[Tipo_Factura],[Cod_Cliente],[Cod_Vendedor],[Cod_Bodega],[Cod_Cajero],[Nombre_Cliente],[Apellido_Cliente],[Direccion_Cliente],[Telefono_Cliente],[Fecha_Vencimiento],[Observaciones],[SubTotal],[IVA],[Pagado],[NetoPagar],[MontoCredito],[MetodoPago],[Descuentos],[Exonerado],[MonedaFactura],[MonedaImprime],[CodigoProyecto],[Retener1Porciento],[Retener2Porciento],[Referencia],[FechaHora]) " & _
                "VALUES ('" & ConsecutivoFactura & "','" & FrmFacturas.DTPFecha.Value & "','" & FrmFacturas.CboTipoProducto.Text & "','" & FrmFacturas.TxtCodigoClientes.Text & "','" & FrmFacturas.CboCodigoVendedor.Text & "','" & FrmFacturas.CboCodigoBodega.Text & "','" & FrmFacturas.CboCajero.Text & "' , '" & FrmFacturas.TxtNombres.Text & "','" & FrmFacturas.TxtApellidos.Text & "','" & FrmFacturas.TxtDireccion.Text & "','" & FrmFacturas.TxtTelefono.Text & "','" & FrmFacturas.DTVencimiento.Value & "','" & FrmFacturas.TxtObservaciones.Text & "'," & Subtotal & "," & Iva & "," & Pagado & "," & Neto & "," & Neto & ",'" & MetodoPago & "'," & Descuento & ",'" & Exonerado & "','" & MonedaFactura & "','" & MonedaImprime & "','" & CodigoProyecto & "','" & Retener1 & "','" & Retener2 & "','" & Referencia & "','" & Format(FechaHora, "dd/MM/yyyy HH:mm") & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()

            Else
                '//////////////////////////////////////////////////////////////////////////////////////////////
                '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
                '/////////////////////////////////////////////////////////////////////////////////////////////////
                SqlCompras = "UPDATE [Facturas]  SET [Cod_Vendedor]='" & FrmFacturas.CboCodigoVendedor.Text & "' , [Cod_Cajero] = '" & FrmFacturas.CboCajero.Text & "',[Cod_Cliente] = '" & FrmFacturas.TxtCodigoClientes.Text & "',[Nombre_Cliente] = '" & FrmFacturas.TxtNombres.Text & "',[Apellido_Cliente] = '" & FrmFacturas.TxtApellidos.Text & "',[Direccion_Cliente] = '" & FrmFacturas.TxtDireccion.Text & "',[Telefono_Cliente] = '" & FrmFacturas.TxtTelefono.Text & "',[Fecha_Vencimiento] = '" & FrmFacturas.DTVencimiento.Value & "' ,[Observaciones] = '" & FrmFacturas.TxtObservaciones.Text & "',[SubTotal] = " & Subtotal & ",[IVA] = " & Iva & ",[Pagado] = " & Pagado & ",[NetoPagar] = " & Neto & ",[MontoCredito] = " & Neto & ",[MetodoPago] = '" & MetodoPago & "',[Descuentos]= " & Descuento & ",[Exonerado]= '" & Exonerado & "',[MonedaFactura]= '" & MonedaFactura & "',[MonedaImprime]= '" & MonedaImprime & "',[CodigoProyecto]= '" & CodigoProyecto & "',[Retener1Porciento]= '" & Retener1 & "',[Retener2Porciento]= '" & Retener2 & "',[Referencia]= '" & Referencia & "'  " & _
                             "WHERE  (Numero_Factura = '" & ConsecutivoFactura & "') AND (Tipo_Factura = '" & My.Forms.FrmFacturas.CboTipoProducto.Text & "')"
                'SqlCompras = "UPDATE [Facturas]  SET [Cod_Vendedor]='" & FrmFacturas.CboCodigoVendedor.Text & "' , [Cod_Cajero] = '" & FrmFacturas.CboCajero.Text & "',[Cod_Cliente] = '" & FrmFacturas.TxtCodigoClientes.Text & "',[Nombre_Cliente] = '" & FrmFacturas.TxtNombres.Text & "',[Apellido_Cliente] = '" & FrmFacturas.TxtApellidos.Text & "',[Direccion_Cliente] = '" & FrmFacturas.TxtDireccion.Text & "',[Telefono_Cliente] = '" & FrmFacturas.TxtTelefono.Text & "',[Fecha_Vencimiento] = '" & FrmFacturas.DTVencimiento.Value & "' ,[Observaciones] = '" & FrmFacturas.TxtObservaciones.Text & "',[SubTotal] = " & Subtotal & ",[IVA] = " & Iva & ",[Pagado] = " & Pagado & ",[NetoPagar] = " & Neto & ",[MontoCredito] = " & Neto & ",[MetodoPago] = '" & MetodoPago & "',[Descuentos]= " & Descuento & ",[Exonerado]= '" & Exonerado & "',[MonedaFactura]= '" & MonedaFactura & "',[MonedaImprime]= '" & MonedaImprime & "',[CodigoProyecto]= '" & CodigoProyecto & "',[Retener1Porciento]= '" & Retener1 & "',[Retener2Porciento]= '" & Retener2 & "',[Referencia]= '" & Referencia & "'  " & _
                '             "WHERE  (Numero_Factura = '" & ConsecutivoFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = '" & FrmFacturas.CboTipoProducto.Text & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub GrabaTransferenciasEntrada(ByVal ConsecutivoCompra As String, ByVal FechaTransferencia As Date, ByVal TipoCompra As String, ByVal CodigoBodega As String, ByVal BodegaOrigen As String, ByVal BodegaDestino As String)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Subtotal As Double, Iva As Double, Neto As Double, Pagado As Double, Descuento As Double
        Dim MonedaFactura As String = My.Forms.FrmFacturas.TxtMonedaFactura.Text

        Try

            Fecha = Format(FechaTransferencia, "yyyy-MM-dd")

            If FrmTransferencias.TxtTotalCosto.Text <> "" Then
                Subtotal = FrmTransferencias.TxtTotalCosto.Text
            Else
                Subtotal = 0
            End If

            '//////////////////////////////////BUSCO CUALQUIER PROVEEDOR PARA PODER GRABAR LA TRANSFERENCIA 


            Iva = 0
            Pagado = 0
            Neto = 0
            Descuento = 0

            MiConexion.Close()


            If FrmTransferencias.TxtNumeroEnsamble.Text = "-----0-----" Then
                '//////////////////////////////////////////////////////////////////////////////////////////////
                '////////////////////////////AGREGO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
                '/////////////////////////////////////////////////////////////////////////////////////////////////
                MiConexion.Close()
                SqlCompras = "INSERT INTO [Compras] ([Numero_Compra] ,[Fecha_Compra],[Tipo_Compra],[Cod_Bodega],[Observaciones],[SubTotal],[Su_Referencia],[Nuestra_Referencia]) " & _
                "VALUES ('" & ConsecutivoCompra & "','" & FechaTransferencia & "','" & TipoCompra & "','" & CodigoBodega & "','" & FrmTransferencias.TxtObservaciones.Text & "'," & Subtotal & ",'" & BodegaOrigen & "','" & BodegaDestino & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()

            Else
                '//////////////////////////////////////////////////////////////////////////////////////////////
                '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
                '/////////////////////////////////////////////////////////////////////////////////////////////////
                SqlCompras = "UPDATE [Compras]  SET [Observaciones] = '" & FrmFacturas.TxtObservaciones.Text & "',[SubTotal] = " & Subtotal & " ,[Su_Referencia] = '" & BodegaOrigen & "',[Nuestra_Referencia] = '" & BodegaDestino & "'   WHERE (Numero_Compra = '" & ConsecutivoCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = '" & TipoCompra & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub GrabaTransferenciasSalida(ByVal ConsecutivoFactura As String, ByVal FechaTransferencia As Date, ByVal TipoFactura As String, ByVal CodigoBodega As String, ByVal BodegaOrigen As String, ByVal BodegaDestino As String)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Subtotal As Double, Iva As Double, Neto As Double, Pagado As Double, Descuento As Double
        Dim MonedaFactura As String = My.Forms.FrmFacturas.TxtMonedaFactura.Text

        Try

            Fecha = Format(FechaTransferencia, "yyyy-MM-dd")

            If FrmTransferencias.TxtTotalCosto.Text <> "" Then
                Subtotal = FrmTransferencias.TxtTotalCosto.Text
            Else
                Subtotal = 0
            End If


            Iva = 0
            Pagado = 0
            Neto = 0
            Descuento = 0

            MiConexion.Close()

            If FrmTransferencias.TxtNumeroEnsamble.Text = "-----0-----" Then
                '//////////////////////////////////////////////////////////////////////////////////////////////
                '////////////////////////////AGREGO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
                '/////////////////////////////////////////////////////////////////////////////////////////////////
                MiConexion.Close()
                SqlCompras = "INSERT INTO [Facturas] ([Numero_Factura] ,[Fecha_Factura],[Tipo_Factura],[Cod_Bodega],[Observaciones],[SubTotal],[Su_Referencia],[Nuestra_Referencia]) " & _
                "VALUES ('" & ConsecutivoFactura & "','" & FechaTransferencia & "','" & TipoFactura & "','" & CodigoBodega & "','" & FrmTransferencias.TxtObservaciones.Text & "'," & Subtotal & ",'" & BodegaOrigen & "','" & BodegaDestino & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()

            Else
                '//////////////////////////////////////////////////////////////////////////////////////////////
                '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
                '/////////////////////////////////////////////////////////////////////////////////////////////////
                SqlCompras = "UPDATE [Facturas]  SET [Observaciones] = '" & FrmFacturas.TxtObservaciones.Text & "',[SubTotal] = " & Subtotal & " ,[Su_Referencia] = '" & BodegaOrigen & "',[Nuestra_Referencia] = '" & BodegaDestino & "'   WHERE (Numero_Factura = '" & ConsecutivoFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = '" & TipoFactura & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub


    Public Sub GrabaFacturasPlantillas(ByVal ConsecutivoFactura As String, ByVal CodigoCliente As String, ByVal CodBodega As String, ByVal NombreCliente As String, ByVal ApellidoCliente As String, ByVal DireccionCliente As String, ByVal TelefonoCliente As String, ByVal SubTotal As Double, ByVal Iva As Double, ByVal Pagado As Double, ByVal Neto As Double, ByVal Fecha As String, ByVal FechaVencimiento As String)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim MetodoPago As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Descuento As Double, Exonerado As Boolean
        Dim MonedaFactura As String = My.Forms.FrmFacturas.TxtMonedaFactura.Text

        Try



            'Fecha = Format(FrmPlantillas.DTPFecha.Value, "yyyy-MM-dd")
            'FechaVencimiento = DateAdd(DateInterval.Day, Val(FrmPlantillas.TxtDiasVencimiento.Value), FrmPlantillas.DTPFecha.Value)


            MetodoPago = "Credito"


            If FrmPlantillas.OptExsonerado.Checked = True Then
                Exonerado = True
            Else
                Exonerado = False
            End If

            Descuento = CDbl(Val(FrmPlantillas.TxtDescuento.Text))

            MiConexion.Close()

            'If FrmPlantillas.TxtNumeroEnsamble.Text = "-----0-----" Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            MiConexion.Close()
            SqlCompras = "INSERT INTO [Facturas] ([Numero_Factura] ,[Fecha_Factura],[Tipo_Factura],[Cod_Cliente],[Cod_Vendedor],[Cod_Bodega],[Nombre_Cliente],[Apellido_Cliente],[Direccion_Cliente],[Telefono_Cliente],[Fecha_Vencimiento],[Observaciones],[SubTotal],[IVA],[Pagado],[NetoPagar],[MontoCredito],[MetodoPago],[Descuentos],[Exonerado],[MonedaFactura]) " & _
            "VALUES ('" & ConsecutivoFactura & "','" & CDate(Fecha) & "','" & FrmPlantillas.CboTipoProducto.Text & "','" & CodigoCliente & "','" & FrmPlantillas.CboCodigoVendedor.Text & "','" & CodBodega & "','" & NombreCliente & "','" & ApellidoCliente & "','" & DireccionCliente & "','" & TelefonoCliente & "','" & FechaVencimiento & "','" & FrmPlantillas.TxtObservaciones.Text & "'," & SubTotal & "," & Iva & "," & Pagado & "," & Neto & "," & Neto & ",'" & MetodoPago & "'," & Descuento & ",'" & Exonerado & "','" & MonedaFactura & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

            'Else
            '    '//////////////////////////////////////////////////////////////////////////////////////////////
            '    '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '    '/////////////////////////////////////////////////////////////////////////////////////////////////
            '    SqlCompras = "UPDATE [Facturas]  SET [Cod_Vendedor]='" & FrmPlantillas.CboCodigoVendedor.Text & "' , [Cod_Cliente] = '" & FrmPlantillas.TxtCodigoClientes.Text & "',[Nombre_Cliente] = '" & FrmPlantillas.TxtNombres.Text & "',[Apellido_Cliente] = '" & FrmPlantillas.TxtApellidos.Text & "',[Direccion_Cliente] = '" & FrmPlantillas.TxtDireccion.Text & "',[Telefono_Cliente] = '" & FrmPlantillas.TxtTelefono.Text & "',[Fecha_Vencimiento] = '" & FechaVencimiento & "' ,[Observaciones] = '" & FrmPlantillas.TxtObservaciones.Text & "',[SubTotal] = " & Subtotal & ",[IVA] = " & Iva & ",[Pagado] = " & Pagado & ",[NetoPagar] = " & Neto & ",[MontoCredito] = " & Neto & ",[MetodoPago] = '" & MetodoPago & "',[Descuentos]= " & Descuento & ",[Exonerado]= '" & Exonerado & "',[MonedaFactura]= '" & MonedaFactura & "'  " & _
            '                 "WHERE  (Numero_Factura = '" & ConsecutivoFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = '" & FrmPlantillas.CboTipoProducto.Text & "')"
            '    MiConexion.Open()
            '    ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            '    iResultado = ComandoUpdate.ExecuteNonQuery
            '    MiConexion.Close()
            'End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub GrabaDetalleFacturaPlantilla(ByVal ConsecutivoFactura As String, ByVal CodProducto As String, ByVal Descripcion_Producto As String, ByVal PrecioUnitario As Double, ByVal Descuento As Double, ByVal PrecioNeto As Double, ByVal Importe As Double, ByVal Cantidad As Double, ByVal IdDetalleFactura As Double, ByVal FechaFactura As Date)
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String, TasaCambio As Double
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, MonedaFactura As String, MonedaProducto As String
        Dim SqlDetalle As String, Fecha As String, Fecha2 As String

        MonedaFactura = FrmPlantillas.TxtMonedaFactura.Text
        MonedaProducto = "Cordobas"

        If MonedaFactura = "Cordobas" Then
            If MonedaProducto = "Cordobas" Then
                TasaCambio = 1
            Else
                If BuscaTasaCambio(FrmPlantillas.DTPFecha.Value) <> 0 Then
                    TasaCambio = (1 / BuscaTasaCambio(FrmPlantillas.DTPFecha.Value))
                End If
            End If
        ElseIf MonedaFactura = "Dolares" Then
            If MonedaProducto = "Cordobas" Then
                TasaCambio = BuscaTasaCambio(FrmPlantillas.DTPFecha.Value)
            Else
                TasaCambio = 1
            End If
        End If



        Fecha = Format(FechaFactura, "yyyy-MM-dd")
        Fecha2 = FechaFactura

        'SqlUpdate = "INSERT INTO [Detalle_Facturas] ([Numero_Factura],[Fecha_Factura],[Tipo_Factura],[Cod_Producto],[Cantidad],[Precio_Unitario],[Descuento],[Precio_Neto],[Importe],[TasaCambio],[Descripcion_Producto]) " & _
        '"VALUES ('" & ConsecutivoFactura & "','" & FrmFacturas.DTPFecha.Value & "','" & FrmFacturas.CboTipoProducto.Text & "','" & CodProducto & "' ," & Cantidad & "," & PrecioUnitario & "," & Descuento & " ," & PrecioNeto & "," & Importe & "," & TasaCambio & ",'" & Descripcion_Producto & "')"
        'MiConexion.Open()
        'ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
        'iResultado = ComandoUpdate.ExecuteNonQuery
        'MiConexion.Close()

        SqlDetalle = "SELECT *  FROM Detalle_Facturas WHERE (Numero_Factura = '" & ConsecutivoFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = '" & FrmPlantillas.CboTipoProducto.Text & "') AND (Cod_Producto = '" & CodProducto & "') AND (Descripcion_Producto = '" & Descripcion_Producto & "') AND (id_Detalle_Factura = '" & IdDetalleFactura & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlDetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleFactura")
        If Not DataSet.Tables("DetalleFactura").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlUpdate = "UPDATE [Detalle_Facturas] SET [Cantidad] = " & Cantidad & " ,[Precio_Unitario] = " & PrecioUnitario & ",[Descuento] = " & Descuento & " ,[Precio_Neto] = " & PrecioNeto & ",[Importe] = " & Importe & ",[Descripcion_Producto] = '" & Descripcion_Producto & "' " & _
                        "WHERE (Numero_Factura = '" & ConsecutivoFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = '" & FrmPlantillas.CboTipoProducto.Text & "') AND (Cod_Producto = '" & CodProducto & "') AND (Descripcion_Producto = '" & Descripcion_Producto & "') AND (id_Detalle_Factura = '" & IdDetalleFactura & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else

            SqlUpdate = "INSERT INTO [Detalle_Facturas] ([Numero_Factura],[Fecha_Factura],[Tipo_Factura],[Cod_Producto],[Cantidad],[Precio_Unitario],[Descuento],[Precio_Neto],[Importe],[TasaCambio],[Descripcion_Producto]) " & _
            "VALUES ('" & ConsecutivoFactura & "','" & CDate(Fecha2) & "','" & FrmPlantillas.CboTipoProducto.Text & "','" & CodProducto & "' ," & Cantidad & "," & PrecioUnitario & "," & Descuento & " ," & PrecioNeto & "," & Importe & "," & TasaCambio & ",'" & Descripcion_Producto & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub


    Public Sub ActualizaMETODOPagosProveedores(ByVal MonedaRecibo As String)
        Dim Metodo As String, iPosicion As Double, Registros As Double, Monto As Double
        Dim Subtotal As Double, TipoMetodo As String, SQlMetodo As String, SQLTasa As String, Fecha As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), Descuento As Double, Moneda As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, TasaCambio As Double




        Registros = FrmPagos.BindingMetodo.Count
        iPosicion = 0

        Do While iPosicion < Registros
            Metodo = FrmPagos.BindingMetodo.Item(iPosicion)("NombrePago")
            TasaCambio = 1
            Fecha = Format(FrmPagos.DTPFecha.Value, "yyyy-MM-dd")
            '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '//////////////////////////////BUSCO LA MONEDA DEL METODO DE PAGO///////////////////////////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Moneda = "Cordobas"
            TipoMetodo = "Cambio"
            SQlMetodo = "SELECT * FROM MetodoPago WHERE (NombrePago = '" & Metodo & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SQlMetodo, MiConexion)
            DataAdapter.Fill(DataSet, "Metodo")
            If DataSet.Tables("Metodo").Rows.Count <> 0 Then
                Moneda = DataSet.Tables("Metodo").Rows(0)("Moneda")
                TipoMetodo = DataSet.Tables("Metodo").Rows(0)("TipoPago")
            End If
            DataSet.Tables("Metodo").Clear()

            TasaCambio = BuscaTasaCambio(Fecha)

            'Select Case Moneda
            '    Case "Cordobas"
            '        TasaCambio = 1

            '    Case "Dolares"
            '        SQLTasa = "SELECT  * FROM TasaCambio WHERE (FechaTasa = CONVERT(DATETIME, '" & Fecha & "', 102))"
            '        DataAdapter = New SqlClient.SqlDataAdapter(SQLTasa, MiConexion)
            '        DataAdapter.Fill(DataSet, "TasaCambio")
            '        If DataSet.Tables("TasaCambio").Rows.Count <> 0 Then
            '            TasaCambio = DataSet.Tables("TasaCambio").Rows(0)("MontoTasa")
            '        Else
            '            'TasaCambio = 0
            '            MsgBox("La Tasa de Cambio no Existe para esta Fecha", MsgBoxStyle.Critical, "Sistema Facturacion")
            '            FrmFacturas.BindingMetodo.Item(iPosicion)("Monto") = 0
            '        End If
            '        DataSet.Tables("TasaCambio").Clear()
            'End Select

            If TipoMetodo = "Cambio" Then
                TasaCambio = TasaCambio * -1
            End If

            Monto = 0
            If Not IsDBNull(FrmPagos.BindingMetodo.Item(iPosicion)("Monto")) Then
                If MonedaRecibo = Moneda Then
                    Monto = (FrmPagos.BindingMetodo.Item(iPosicion)("Monto")) + Monto
                ElseIf MonedaRecibo = "Dolares" Then
                    Monto = (FrmPagos.BindingMetodo.Item(iPosicion)("Monto") / TasaCambio) + Monto
                ElseIf MonedaRecibo = "Cordobas" Then
                    Monto = (FrmPagos.BindingMetodo.Item(iPosicion)("Monto") * TasaCambio) + Monto
                End If
            End If

            iPosicion = iPosicion + 1
        Loop


        Registros = FrmPagos.BindingFacturas.Count
        iPosicion = 0

        Do While iPosicion < Registros
            If Not IsDBNull(FrmPagos.BindingFacturas.Item(iPosicion)("MontoPagado")) Then
                Subtotal = CDbl(FrmPagos.BindingFacturas.Item(iPosicion)("MontoPagado")) + Subtotal
            End If
            iPosicion = iPosicion + 1
        Loop


        If FrmPagos.TxtDescuento.Text = "" Then
            Descuento = 0
        Else
            'Descuento = FrmPagos.TxtDescuento.Text
            'If Monto < Descuento Then
            '    MsgBox("El descuento, no puede ser mayor que el moto de la factura", MsgBoxStyle.Critical, "Sistema Facturacion")
            '    Descuento = 0
            '    FrmPagos.TxtDescuento.Text = ""
            'End If
        End If



        FrmPagos.TxtImporteRecibido.Text = Format(Monto, "##,##0.00")
        FrmPagos.TxtImporteAplicado.Text = Format(Subtotal, "##,##0.00")
        FrmPagos.TxtPorAplicar.Text = Format(Monto - Subtotal, "##,##0.00")
        FrmPagos.TxtSubTotal.Text = Format(Monto, "##,##0.00")
        FrmPagos.TxtDescuento.Text = Format(Descuento, "##,##0.00")
        FrmPagos.TxtNetoPagar.Text = Format(Monto - Descuento, "##,##0.00")

    End Sub


    Public Sub GrabaPagos(ByVal ConsecutivoPago As String)
        Dim SqlPagos As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim MonedaRecibo As String, Retencion1 As String = "", Retencion2 As String = "", Retencion3 As String = "", Retencion4 As String = ""


        If FrmPagos.OptRet1Porciento.Checked = True Then
            Retencion1 = "1%"
        Else
            Retencion1 = "0%"
        End If

        If FrmPagos.OptRet2Porciento.Checked = True Then
            Retencion2 = "2%"
        Else
            Retencion2 = "0%"
        End If

        If FrmPagos.OptRet7Porciento.Checked = True Then
            Retencion3 = "7%"
        Else
            Retencion3 = "0%"
        End If

        If FrmPagos.OptRet10Porciento.Checked = True Then
            Retencion4 = "10%"
        Else
            Retencion4 = "0%"
        End If



        MonedaRecibo = FrmPagos.TxtMonedaFactura.Text
        Fecha = Format(FrmPagos.DTPFecha.Value, "yyyy-MM-dd")


        MiConexion.Close()

        If FrmPagos.TxtNumeroEnsamble.Text = "-----0-----" Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlPagos = "INSERT INTO [ReciboPago] ([CodReciboPago],[Fecha_Recibo],[Cod_Proveedor],[Sub_Total],[Descuento],[Total],[MonedaRecibo],[Retencion1],[Retencion2],[Retencion3],[Retencion4],[Observaciones]) " & _
                       "VALUES('" & ConsecutivoPago & "','" & Format(FrmPagos.DTPFecha.Value, "dd/MM/yyyy") & "','" & FrmPagos.TxtCodigoProveedor.Text & "','" & CDbl(FrmPagos.TxtSubTotal.Text) & "','" & CDbl(FrmPagos.TxtDescuento.Text) & "','" & CDbl(FrmPagos.TxtNetoPagar.Text) & "','" & MonedaRecibo & "','" & Retencion1 & "','" & Retencion2 & "','" & Retencion3 & "','" & Retencion4 & "','" & FrmPagos.TxtObservaciones.Text & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlPagos, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlPagos = "UPDATE [ReciboPago] SET [Cod_Proveedor] = '" & FrmPagos.TxtCodigoProveedor.Text & "',[Sub_Total] = '" & CDbl(FrmPagos.TxtSubTotal.Text) & "',[Descuento] = '" & CDbl(FrmPagos.TxtDescuento.Text) & "',[Total] = '" & CDbl(FrmPagos.TxtNetoPagar.Text) & "' ,[MonedaRecibo] = '" & MonedaRecibo & "',[Retencion1] = '" & Retencion1 & "',[Retencion2] = '" & Retencion2 & "',[Retencion3] = '" & Retencion3 & "',[Retencion4] = '" & Retencion4 & "',[Observaciones] = '" & FrmPagos.TxtObservaciones.Text & "'  " & _
                       "WHERE (CodReciboPago = '" & ConsecutivoPago & "') AND (Fecha_Recibo = CONVERT(DATETIME, '" & Fecha & "', 102))"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlPagos, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If

    End Sub

    Public Sub GrabaDetallePagos(ByVal ConsecutivoPago As String, ByVal NumeroCompra As String, ByVal MontoPagado As Double, ByVal Retencion As Double)
        Dim SqlPagos As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim MonedaRecibo As String = ""




        Fecha = Format(FrmPagos.DTPFecha.Value, "yyyy-MM-dd")


        MiConexion.Close()

        If FrmPagos.TxtNumeroEnsamble.Text = "-----0-----" Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL DETALLE DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlPagos = "INSERT INTO [DetalleReciboPago]([CodReciboPago],[Fecha_Recibo],[Numero_Compra],[MontoPagado],[MontoRetencion]) " & _
                       "VALUES('" & ConsecutivoPago & "','" & Format(FrmPagos.DTPFecha.Value, "dd/MM/yyyy") & "','" & NumeroCompra & "','" & MontoPagado & "','" & Retencion & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlPagos, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlPagos = "UPDATE [DetalleReciboPago] SET [MontoPagado] = '" & MontoPagado & "', [MontoRetencion] = '" & Retencion & "'" & _
                       "WHERE ([CodReciboPago]='" & ConsecutivoPago & "') AND (Fecha_Recibo = CONVERT(DATETIME, '" & Fecha & "', 102)) AND ([Numero_Compra]='" & NumeroCompra & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlPagos, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If
    End Sub


    Public Sub ActualizaMontoCreditoCompra(ByVal NumeroCompra As String, ByVal MontoCredito As Double, ByVal MonedaRecibo As String)
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim SqlPagos As String, DataAdapter As New SqlClient.SqlDataAdapter
        Dim DataSet As New DataSet, MonedaCompra As String = "Cordobas", TasaCambio As Double
        Dim FechaRecibo As Date

        FechaRecibo = FrmPagos.DTPFecha.Value

        SqlPagos = "SELECT  *  FROM Compras WHERE  (Numero_Compra = '" & NumeroCompra & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlPagos, MiConexion)
        DataAdapter.Fill(DataSet, "Compra")
        If Not DataSet.Tables("Compra").Rows.Count = 0 Then
            MonedaCompra = DataSet.Tables("Compra").Rows(0)("MonedaCompra")
        End If

        TasaCambio = 1
        If MonedaRecibo <> MonedaCompra Then
            If MonedaCompra = "Cordobas" Then
                TasaCambio = BuscaTasaCambio(FechaRecibo)
                MontoCredito = MontoCredito * TasaCambio
            ElseIf MonedaCompra = "Dolares" Then
                TasaCambio = BuscaTasaCambio(FechaRecibo)
                MontoCredito = MontoCredito / TasaCambio
            End If
        End If


        SqlPagos = "UPDATE [Compras] SET [MontoCredito] = " & MontoCredito & " WHERE(Numero_Compra = '" & NumeroCompra & "')"
        MiConexion.Open()
        ComandoUpdate = New SqlClient.SqlCommand(SqlPagos, MiConexion)
        iResultado = ComandoUpdate.ExecuteNonQuery
        MiConexion.Close()

    End Sub

    Public Sub LimpiaPago()
        Dim SqlString As String, DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)

        FrmPagos.TxtNumeroEnsamble.Text = "-----0-----"
        FrmPagos.TxtCodigoProveedor.Text = ""
        FrmPagos.DTPFecha.Value = Now

        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////CARGO LAS FORMA DE PAGO////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlString = "SELECT  NombrePago, Monto,NumeroTarjeta,FechaVence FROM Detalle_MetodoCompras WHERE (Numero_Compra = '-1')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "MetodoPago")
        FrmPagos.BindingMetodo.DataSource = DataSet.Tables("MetodoPago")
        FrmPagos.TrueDBGridMetodo.DataSource = FrmPagos.BindingMetodo
        FrmPagos.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(1).Width = 110
        FrmPagos.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(1).Width = 70
        FrmPagos.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(0).Button = True
        FrmPagos.TrueDBGridMetodo.Columns(1).NumberFormat = "##,##0.00"
        FrmPagos.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(2).Visible = False
        FrmPagos.TrueDBGridMetodo.Splits.Item(0).DisplayColumns(3).Visible = False
        MiConexion.Close()

        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////CARGO LAS FORMA DE PAGO////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlString = "SELECT DISTINCT Compras.Numero_Compra, Compras.Fecha_Compra, Compras.MontoCredito,  DetalleReciboPago.MontoPagado - DetalleReciboPago.MontoPagado AS MontoPagado, Compras.MontoCredito AS Saldo FROM  DetalleReciboPago FULL OUTER JOIN Compras ON DetalleReciboPago.Numero_Compra = Compras.Numero_Compra WHERE (Compras.Tipo_Compra = N'Mercancia Recibida') AND (Compras.MontoCredito <> 0) AND (Compras.Cod_Proveedor = '-1')"
        MiConexion.Open()
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Consultas")
        FrmPagos.BindingFacturas.DataSource = DataSet.Tables("Consultas")
        FrmPagos.TrueDBGridComponentes.DataSource = FrmPagos.BindingFacturas

        ActualizaMETODOPagosProveedores(FrmPagos.TxtMonedaFactura.Text)
    End Sub

    Public Sub ActualizaMETODORecibos()
        Dim Metodo As String, iPosicion As Double, Registros As Double, Monto As Double
        Dim Subtotal As Double, TasaCambio As Double, Fecha As String, Moneda As String, TipoMetodo As String, SQLMetodo As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), Descuento As Double, SQLTasa As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter



        Registros = FrmRecibos.BindingDetalleRecibo.Count

        iPosicion = 0

        Do While iPosicion < Registros
            If Not IsDBNull(FrmRecibos.BindingDetalleRecibo.Item(iPosicion)("MontoPagado")) Then
                Metodo = FrmRecibos.BindingDetalleRecibo.Item(iPosicion)("NombrePago")
                TasaCambio = 1
                Fecha = Format(FrmRecibos.DTPFecha.Value, "yyyy-MM-dd")
                '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                '//////////////////////////////BUSCO LA MONEDA DEL METODO DE PAGO///////////////////////////////////////////////////////
                '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                Moneda = FrmRecibos.TxtMonedaFactura.Text
                TipoMetodo = "Cambio"
                SQLMetodo = "SELECT * FROM MetodoPago WHERE (NombrePago = '" & Metodo & "')"
                DataAdapter = New SqlClient.SqlDataAdapter(SQLMetodo, MiConexion)
                DataAdapter.Fill(DataSet, "Metodo")
                If DataSet.Tables("Metodo").Rows.Count <> 0 Then
                    Moneda = DataSet.Tables("Metodo").Rows(0)("Moneda")
                    TipoMetodo = DataSet.Tables("Metodo").Rows(0)("TipoPago")
                End If
                DataSet.Tables("Metodo").Clear()

                '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                '///////////////////////////////BUSCO LA TASA DE CAMBIO///////////////////////////////////////////////////////////////////////
                '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                Select Case Moneda
                    Case "Cordobas"
                        TasaCambio = 1

                    Case "Dolares"
                        SQLTasa = "SELECT  * FROM TasaCambio WHERE (FechaTasa = CONVERT(DATETIME, '" & Fecha & "', 102))"
                        DataAdapter = New SqlClient.SqlDataAdapter(SQLTasa, MiConexion)
                        DataAdapter.Fill(DataSet, "TasaCambio")
                        If DataSet.Tables("TasaCambio").Rows.Count <> 0 Then
                            TasaCambio = DataSet.Tables("TasaCambio").Rows(0)("MontoTasa")
                        Else
                            'TasaCambio = 0
                            MsgBox("La Tasa de Cambio no Existe para esta Fecha", MsgBoxStyle.Critical, "Sistema Facturacion")
                            'FrmFacturas.BindingMetodo.Item(iPosicion)("Monto") = 0
                        End If
                        DataSet.Tables("TasaCambio").Clear()
                End Select

                If TipoMetodo = "Cambio" Then
                    TasaCambio = TasaCambio * -1
                End If


                '* TasaCambio

                Monto = (FrmRecibos.BindingDetalleRecibo.Item(iPosicion)("MontoPagado")) + Monto
            End If
            iPosicion = iPosicion + 1
        Loop


        Registros = FrmRecibos.BindingDetalleRecibo.Count
        iPosicion = 0

        Do While iPosicion < Registros
            If Not IsDBNull(FrmRecibos.BindingDetalleRecibo.Item(iPosicion)("MontoPagado")) Then
                Subtotal = CDbl(FrmRecibos.BindingDetalleRecibo.Item(iPosicion)("MontoPagado")) + Subtotal
            End If
            iPosicion = iPosicion + 1
        Loop


        If FrmRecibos.TxtDescuento.Text = "" Then
            Descuento = 0
        Else
            Descuento = FrmRecibos.TxtDescuento.Text
            If Monto < Descuento Then
                MsgBox("El descuento, no puede ser mayor que el moto de la factura", MsgBoxStyle.Critical, "Sistema Facturacion")
                Descuento = 0
                FrmRecibos.TxtDescuento.Text = ""
            End If
        End If



        FrmRecibos.TxtImporteRecibido.Text = Format(Monto, "##,##0.00")
        FrmRecibos.TxtImporteAplicado.Text = Format(Subtotal, "##,##0.00")
        FrmRecibos.TxtPorAplicar.Text = Format(Monto - Subtotal, "##,##0.00")
        FrmRecibos.TxtSubTotal.Text = Format(Monto, "##,##0.00")
        FrmRecibos.TxtDescuento.Text = Format(Descuento, "##,##0.00")
        FrmRecibos.TxtNetoPagar.Text = Format(Monto - Descuento, "##,##0.00")

    End Sub

    Public Sub ActualizaMETODORecibos2()
        Dim Metodo As String, iPosicion As Double, Registros As Double, Monto As Double
        Dim Subtotal As Double, TasaCambio As Double, Fecha As String, Moneda As String, TipoMetodo As String, SQLMetodo As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), Descuento As Double, SQLTasa As String = ""
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, MonedaRecibo As String

        MonedaRecibo = My.Forms.FrmRecibos.TxtMonedaFactura.Text

        Registros = FrmRecibosFacturas.BindingMetodo.Count

        iPosicion = 0

        Do While iPosicion < Registros
            If Not IsDBNull(FrmRecibosFacturas.BindingMetodo.Item(iPosicion)("Monto")) Then
                Metodo = FrmRecibosFacturas.BindingMetodo.Item(iPosicion)("NombrePago")
                TasaCambio = 1
                Fecha = Format(FrmRecibos.DTPFecha.Value, "yyyy-MM-dd")

                '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                '//////////////////////////////BUSCO LA MONEDA DEL METODO DE PAGO///////////////////////////////////////////////////////
                '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                MonedaRecibo = My.Forms.FrmRecibos.TxtMonedaFactura.Text
                Moneda = "Cordobas"
                TipoMetodo = "Cambio"
                SQLMetodo = "SELECT * FROM MetodoPago WHERE (NombrePago = '" & Metodo & "')"
                DataAdapter = New SqlClient.SqlDataAdapter(SQLMetodo, MiConexion)
                DataAdapter.Fill(DataSet, "Metodo")
                If DataSet.Tables("Metodo").Rows.Count <> 0 Then
                    Moneda = DataSet.Tables("Metodo").Rows(0)("Moneda")
                    TipoMetodo = DataSet.Tables("Metodo").Rows(0)("TipoPago")
                End If
                DataSet.Tables("Metodo").Clear()

                '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                '///////////////////////////////BUSCO LA TASA DE CAMBIO///////////////////////////////////////////////////////////////////////
                '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                Select Case Moneda   '///////////////////////////////////////////MONEDA DEL METODO DE PAGO //////////////////////////////////////////
                    Case "Cordobas"
                        If MonedaRecibo = "Cordobas" Then
                            TasaCambio = 1
                        Else
                            SQLTasa = "SELECT  * FROM TasaCambio WHERE (FechaTasa = CONVERT(DATETIME, '" & Fecha & "', 102))"
                            DataAdapter = New SqlClient.SqlDataAdapter(SQLTasa, MiConexion)
                            DataAdapter.Fill(DataSet, "TasaCambio")
                            If DataSet.Tables("TasaCambio").Rows.Count <> 0 Then
                                '//////////////////PARA LOS CORDOBAS USO TASA DE CAMBIO 2 ////////////////////////////////////////
                                If Not IsDBNull(DataSet.Tables("TasaCambio").Rows(0)("MontoTasa")) Then
                                    TasaCambio = (1 / DataSet.Tables("TasaCambio").Rows(0)("MontoTasa"))
                                Else
                                    TasaCambio = 1
                                End If
                            Else
                                'TasaCambio = 0
                                MsgBox("La Tasa de Cambio no Existe para esta Fecha", MsgBoxStyle.Critical, "Sistema Facturacion")
                                FrmFacturas.BindingMetodo.Item(iPosicion)("Monto") = 0
                            End If
                            DataSet.Tables("TasaCambio").Clear()
                        End If

                    Case "Dolares"
                        If MonedaRecibo = "Cordobas" Then
                            SQLTasa = "SELECT  * FROM TasaCambio WHERE (FechaTasa = CONVERT(DATETIME, '" & Fecha & "', 102))"
                            DataAdapter = New SqlClient.SqlDataAdapter(SQLTasa, MiConexion)
                            DataAdapter.Fill(DataSet, "TasaCambio")
                            If DataSet.Tables("TasaCambio").Rows.Count <> 0 Then
                                TasaCambio = DataSet.Tables("TasaCambio").Rows(0)("MontoTasa")
                            Else
                                'TasaCambio = 0
                                MsgBox("La Tasa de Cambio no Existe para esta Fecha", MsgBoxStyle.Critical, "Sistema Facturacion")
                                FrmFacturas.BindingMetodo.Item(iPosicion)("Monto") = 0
                            End If
                            DataSet.Tables("TasaCambio").Clear()
                        Else
                            TasaCambio = 1
                        End If
                End Select

                'If TipoMetodo = "Cambio" Then
                '    TasaCambio = TasaCambio * -1
                'End If




                'If Moneda = MonedaRecibo Then
                '    TasaCambio = 1
                'Else
                '    TasaCambio = BuscaTasaCambio(Fecha)
                'End If


                If TipoMetodo = "Cambio" Then
                    TasaCambio = TasaCambio * -1
                End If




                Monto = (FrmRecibosFacturas.BindingMetodo.Item(iPosicion)("Monto") * TasaCambio) + Monto
            End If
            iPosicion = iPosicion + 1
        Loop


        Registros = FrmRecibosFacturas.BindingFacturas.Count
        iPosicion = 0

        Do While iPosicion < Registros
            If Not IsDBNull(FrmRecibosFacturas.BindingFacturas.Item(iPosicion)("MontoPagado")) Then
                Subtotal = CDbl(FrmRecibosFacturas.BindingFacturas.Item(iPosicion)("MontoPagado")) + Subtotal
            End If
            iPosicion = iPosicion + 1
        Loop


        If FrmRecibos.TxtDescuento.Text = "" Then
            Descuento = 0
        Else
            Descuento = FrmRecibos.TxtDescuento.Text
            If Monto < Descuento Then
                MsgBox("El descuento, no puede ser mayor que el moto de la factura", MsgBoxStyle.Critical, "Sistema Facturacion")
                Descuento = 0
                FrmRecibos.TxtDescuento.Text = ""
            End If
        End If



        FrmRecibosFacturas.TxtImporteRecibido.Text = Format(Monto, "##,##0.00")
        FrmRecibosFacturas.TxtImporteAplicado.Text = Format(Subtotal, "##,##0.00")
        FrmRecibosFacturas.TxtPorAplicar.Text = Format(Monto - Subtotal, "##,##0.00")
        FrmRecibos.TxtSubTotal.Text = Format(Monto, "##,##0.00")
        FrmRecibos.TxtDescuento.Text = Format(Descuento, "##,##0.00")
        FrmRecibos.TxtNetoPagar.Text = Format(Monto - Descuento, "##,##0.00")

    End Sub


    Public Sub LimpiaRecibo()

        Dim SqlString As String, DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)

        FrmRecibos.TxtNumeroEnsamble.Text = "-----0-----"
        FrmRecibos.TxtCodigoClientes.Text = ""
        FrmRecibos.DTPFecha.Value = Now
        FrmRecibos.CmbSerie.Enabled = True

        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////CARGO LAS FORMA DE PAGO////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        FrmRecibos.ds.Reset()
        SqlString = "SELECT NombrePago, Descripcion, Numero_Factura, MontoPagado,NumeroTarjeta,FechaVence,MontoFactura,AplicaFactura,SaldoFactura, idDetalleRecibo, CodReciboPago, Fecha_Recibo FROM DetalleRecibo WHERE (CodReciboPago = '-1') AND (Fecha_Recibo = CONVERT(DATETIME, '2010-01-01 00:00:00', 102))"
        FrmRecibos.ds = New DataSet
        FrmRecibos.da = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        FrmRecibos.CmdBuilder = New SqlClient.SqlCommandBuilder(FrmRecibos.da)
        FrmRecibos.da.Fill(FrmRecibos.ds, "DetalleRecibo")
        FrmRecibos.BindingDetalleRecibo.DataSource = FrmRecibos.ds.Tables("DetalleRecibo")

        'DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        'DataAdapter.Fill(DataSet, "DetalleRecibo")
        'FrmRecibos.BindingDetalleRecibo.DataSource = DataSet.Tables("DetalleRecibo")
        FrmRecibos.TDBGridDetalle.DataSource = FrmRecibos.BindingDetalleRecibo
        FrmRecibos.TDBGridDetalle.Splits.Item(0).DisplayColumns(0).Button = True
        FrmRecibos.TDBGridDetalle.Splits.Item(0).DisplayColumns(0).Width = 85
        FrmRecibos.TDBGridDetalle.Splits.Item(0).DisplayColumns(1).Width = 213
        FrmRecibos.TDBGridDetalle.Splits.Item(0).DisplayColumns(2).Width = 100
        FrmRecibos.TDBGridDetalle.Splits.Item(0).DisplayColumns(3).Width = 75
        FrmRecibos.TDBGridDetalle.Columns(3).NumberFormat = "##,##0.00"
        FrmRecibos.TDBGridDetalle.Splits.Item(0).DisplayColumns(4).Visible = False
        FrmRecibos.TDBGridDetalle.Splits.Item(0).DisplayColumns(5).Visible = False
        FrmRecibos.TDBGridDetalle.Splits.Item(0).DisplayColumns(6).Visible = False
        FrmRecibos.TDBGridDetalle.Splits.Item(0).DisplayColumns(7).Visible = False
        FrmRecibos.TDBGridDetalle.Splits.Item(0).DisplayColumns(8).Visible = False
        FrmRecibos.TDBGridDetalle.Splits.Item(0).DisplayColumns(9).Visible = False
        FrmRecibos.TDBGridDetalle.Splits.Item(0).DisplayColumns(10).Visible = False
        FrmRecibos.TDBGridDetalle.Splits.Item(0).DisplayColumns(11).Visible = False
        MiConexion.Close()

        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////CARGO LAS FORMA DE PAGO////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlString = "SELECT DISTINCT Compras.Numero_Compra, Compras.Fecha_Compra, Compras.MontoCredito,  DetalleReciboPago.MontoPagado - DetalleReciboPago.MontoPagado AS MontoPagado, Compras.MontoCredito AS Saldo FROM  DetalleReciboPago FULL OUTER JOIN Compras ON DetalleReciboPago.Numero_Compra = Compras.Numero_Compra WHERE (Compras.Tipo_Compra = N'Mercancia Recibida') AND (Compras.MontoCredito <> 0) AND (Compras.Cod_Proveedor = '-1')"
        MiConexion.Open()
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Consultas")
        FrmRecibos.BindingFacturas.DataSource = DataSet.Tables("Consultas")
        FrmRecibos.TrueDBGridComponentes.DataSource = FrmPagos.BindingFacturas

        ActualizaMETODORecibos()
    End Sub

    Public Sub GrabaRecibos(ByVal ConsecutivoPago As String)
        Dim SqlPagos As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, MonedaRecibo As String = "Cordobas", SQlString As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim SubTotal As Double, Total As Double, Descuento As Double


        If FrmRecibos.CboCajero.Text = "" Then
            MsgBox("Se necesita un Cajero", MsgBoxStyle.Critical, "Sistem Facturacion")
            Exit Sub
        End If

        MonedaRecibo = FrmRecibos.TxtMonedaFactura.Text
        SubTotal = FrmRecibos.TxtSubTotal.Text
        Descuento = FrmRecibos.TxtDescuento.Text
        Total = FrmRecibos.TxtNetoPagar.Text



        Fecha = Format(FrmRecibos.DTPFecha.Value, "yyyy-MM-dd")



        CambiaFechaRecibo = False

        If ExisteRecibo(Fecha, ConsecutivoPago) = "ExisteReciboDifFecha" Then

            If MsgBox("Existe Diferencia de Fechas, �Desea Grabar este Cambio?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                CambiaFechaRecibo = True
                '///////////////////////////////////////////////////////////////////////////////////////////////////////////
                '///////////////////////////////////BORRO EL METODO DE RECIBO /////////////////////////////////////////////
                '/////////////////////////////////////////////
                MiConexion.Close()
                SQlString = "DELETE FROM [Detalle_MetodoRecibo] WHERE  (CodRecibo = '" & ConsecutivoPago & "') "
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SQlString, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()

                '///////////////////////////////////////////////////////////////////////////////////////////////////////////
                '///////////////////////////////////BORRO EL DETALLE DEL RECIBO /////////////////////////////////////////////
                '/////////////////////////////////////////////
                MiConexion.Close()
                SQlString = "DELETE FROM [DetalleRecibo] WHERE  (CodReciboPago = '" & ConsecutivoPago & "') "
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SQlString, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()

                '///////////////////////////////////////////////////////////////////////////////////////////////////////////
                '///////////////////////////////////BORRO LOS RECIBOS /////////////////////////////////////////////
                '/////////////////////////////////////////////
                MiConexion.Close()
                SQlString = "DELETE FROM [Recibo] WHERE  (CodReciboPago = '" & ConsecutivoPago & "') "
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SQlString, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()
            End If


        End If


        MiConexion.Close()

        SQlString = "SELECT * FROM Recibo WHERE  (CodReciboPago = '" & ConsecutivoPago & "') "
        DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleRecibo")
        If DataSet.Tables("DetalleRecibo").Rows.Count = 0 Then

            'If FrmRecibos.TxtNumeroEnsamble.Text = "-----0-----" Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlPagos = "INSERT INTO [Recibo] ([CodReciboPago],[Fecha_Recibo],[Cod_Cliente],[Cod_Cajero],[Sub_Total],[Descuento],[Total],[NombreCliente],[ApellidosCliente],[DireccionCliente],[TelefonoCliente],[MonedaRecibo],[Observaciones]) " & _
                       "VALUES('" & ConsecutivoPago & "','" & Format(FrmRecibos.DTPFecha.Value, "dd/MM/yyyy") & "','" & FrmRecibos.TxtCodigoClientes.Text & "','" & FrmRecibos.CboCajero.Text & "'," & SubTotal & "," & Descuento & "," & Total & ",'" & FrmRecibos.TxtNombres.Text & "','" & FrmRecibos.TxtApellidos.Text & "','" & FrmRecibos.TxtDireccion.Text & "','" & FrmRecibos.TxtTelefono.Text & "','" & MonedaRecibo & "','" & FrmRecibos.TxtObservaciones.Text & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlPagos, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlPagos = "UPDATE [Recibo] SET [Cod_Cliente] = '" & FrmRecibos.TxtCodigoClientes.Text & "',[Cod_Cajero] = '" & FrmRecibos.CboCajero.Text & "',[Sub_Total] = " & SubTotal & ",[Descuento] = " & Descuento & ",[Total] = " & Total & ",[NombreCliente] = '" & FrmRecibos.TxtNombres.Text & "' ,[ApellidosCliente] = '" & FrmRecibos.TxtApellidos.Text & "',[DireccionCliente] = '" & FrmRecibos.TxtDireccion.Text & "',[TelefonoCliente] = '" & FrmRecibos.TxtTelefono.Text & "',[MonedaRecibo] = '" & MonedaRecibo & "',[Observaciones] = '" & FrmRecibos.TxtObservaciones.Text & "'  " & _
                       "WHERE (CodReciboPago = '" & ConsecutivoPago & "') "
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlPagos, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If
    End Sub

    Public Sub GrabaDetalleRecibos(ByVal ConsecutivoPago As String, ByVal NumeroFactura As String, ByVal MontoPagado As Double, ByVal NombrePago As String, ByVal Descripcion As String, ByVal NumeroTarjeta As String, ByVal FechaVence As Date, ByVal idDetalleRecibo As Double)
        Dim SqlPagos As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, TasaCambio As Double, MonedaRecibos As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim FechaVencimiento As String




        Fecha = Format(FrmRecibos.DTPFecha.Value, "yyyy-MM-dd")
        FechaVencimiento = Format(FechaVence, "dd/MM/yyyy")

        MonedaRecibos = FrmRecibos.TxtMonedaFactura.Text
        If MonedaRecibos = "Cordobas" Then
            TasaCambio = 1
        Else
            TasaCambio = BuscaTasaCambio(Fecha)
        End If


        MiConexion.Close()

        SqlPagos = "SELECT * FROM DetalleRecibo WHERE (CodReciboPago = '" & ConsecutivoPago & "') AND (idDetalleRecibo = " & idDetalleRecibo & ")"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlPagos, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleRecibo")
        If DataSet.Tables("DetalleRecibo").Rows.Count = 0 Then
            'If FrmRecibos.TxtNumeroEnsamble.Text = "-----0-----" Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL DETALLE DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlPagos = "INSERT INTO [DetalleRecibo]([CodReciboPago],[Fecha_Recibo],[Numero_Factura],[MontoPagado],[NombrePago],[Descripcion],[NumeroTarjeta],[FechaVence],[TasaCambio]) " & _
                       "VALUES('" & ConsecutivoPago & "','" & Format(FrmRecibos.DTPFecha.Value, "dd/MM/yyyy") & "','" & NumeroFactura & "'," & MontoPagado & ",'" & NombrePago & "','" & Descripcion & "','" & NumeroTarjeta & "','" & FechaVencimiento & "', " & TasaCambio & ")"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlPagos, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlPagos = "UPDATE [DetalleRecibo] SET [MontoPagado] = '" & MontoPagado & "',[NombrePago] = '" & NombrePago & "',[Descripcion] = '" & Descripcion & "',[NumeroTarjeta] = '" & NumeroTarjeta & "',[FechaVence] = '" & FechaVencimiento & "',[TasaCambio]=" & TasaCambio & "  " & _
                       "WHERE ([CodReciboPago]='" & ConsecutivoPago & "') AND (Fecha_Recibo = CONVERT(DATETIME, '" & Fecha & "', 102)) "
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlPagos, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If
    End Sub

    Public Sub ActualizaMontoCreditoFactura(ByVal NumeroFactura As String, ByVal MontoCredito As Double)
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim SqlPagos As String

        SqlPagos = "UPDATE [Facturas] SET [MontoCredito] = '" & MontoCredito & "' WHERE(Numero_Factura = '" & NumeroFactura & "')"
        MiConexion.Open()
        ComandoUpdate = New SqlClient.SqlCommand(SqlPagos, MiConexion)
        iResultado = ComandoUpdate.ExecuteNonQuery
        MiConexion.Close()

    End Sub
    Public Sub GrabaMetodoDetallePago(ByVal ConsecutivoRecibo As String, ByVal NombrePago As String, ByVal Monto As Double)
        Dim Sqldetalle As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String, FechaVencimiento As String = ""
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, FechaRecibo As String

        Fecha = Format(FrmPagos.DTPFecha.Value, "yyyy-MM-dd")
        'FechaVencimiento = Format(CDate(FechaVence), "dd/MM/yyyy")
        FechaRecibo = Format(FrmPagos.DTPFecha.Value, "dd/MM/yyyy")

        Sqldetalle = "SELECT *  FROM Detalle_MetodoPagoProveedores WHERE (CodReciboPago = '" & ConsecutivoRecibo & "') AND (Fecha_Recibo = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (NombrePago = '" & NombrePago & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(Sqldetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleMetodoRecibo")
        If Not DataSet.Tables("DetalleMetodoRecibo").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlUpdate = "UPDATE [Detalle_MetodoPagoProveedores] SET [NombrePago] = '" & NombrePago & "',[Monto] = " & Monto & " " & _
                         "WHERE (CodReciboPago = '" & ConsecutivoRecibo & "') AND (Fecha_Recibo = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (NombrePago = '" & NombrePago & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            MiConexion.Close()
            SqlUpdate = "INSERT INTO [Detalle_MetodoPagoProveedores] ([CodReciboPago],[Fecha_Recibo],[NombrePago],[Monto]) " & _
                        "VALUES('" & ConsecutivoRecibo & "','" & FechaRecibo & "','" & NombrePago & "'," & Monto & " )"

            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub


    Public Sub GrabaMetodoDetalleRecibo(ByVal ConsecutivoRecibo As String, ByVal NombrePago As String, ByVal Monto As Double, ByVal NumeroTarjeta As String, ByVal FechaVence As String)
        Dim Sqldetalle As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String, FechaVencimiento As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, FechaRecibo As String

        Fecha = Format(FrmRecibos.DTPFecha.Value, "yyyy-MM-dd")
        If FechaVence <> Nothing Then
            FechaVencimiento = Format(CDate(FechaVence), "dd/MM/yyyy")
        End If
        FechaRecibo = Format(FrmRecibos.DTPFecha.Value, "dd/MM/yyyy")

        Sqldetalle = "SELECT *  FROM Detalle_MetodoRecibo WHERE (CodRecibo = '" & ConsecutivoRecibo & "') AND (Fecha_Recibo = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (NombrePago = '" & NombrePago & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(Sqldetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleMetodoRecibo")
        If Not DataSet.Tables("DetalleMetodoRecibo").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlUpdate = "UPDATE [Detalle_MetodoRecibo] SET [NombrePago] = '" & NombrePago & "',[Monto] = " & Monto & ",[NumeroTarjeta] = '" & NumeroTarjeta & "',[Fecha_Vence] = '" & FechaVencimiento & "' " & _
                         "WHERE (CodRecibo = '" & ConsecutivoRecibo & "') AND (Fecha_Recibo = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (NombrePago = '" & NombrePago & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            MiConexion.Close()
            SqlUpdate = "INSERT INTO [Detalle_MetodoRecibo] ([CodRecibo],[Fecha_Recibo],[NombrePago],[Monto],[NumeroTarjeta],[Fecha_Vence]) " & _
                        "VALUES('" & ConsecutivoRecibo & "','" & FechaRecibo & "','" & NombrePago & "'," & Monto & " ,'" & NumeroTarjeta & "','" & FechaVencimiento & "')"

            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub

    Public Sub GrabaArqueo(ByVal ConsecutivoArqueo As String)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, Fecha2 As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter

        Fecha = Format(FrmArqueo.DTFecha.Value, "yyyy-MM-dd")
        Fecha2 = Format(FrmArqueo.DTFecha.Value, "dd/MM/yyyy")

        MiConexion.Close()

        If FrmArqueo.TxtNumeroEnsamble.Text = "-----0-----" Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL ENCABEZADO DEL ARQUEO//////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "INSERT INTO [Arqueo] ([CodArqueo],[FechaArqueo],[Cod_Cajero]) " & _
                         "VALUES('" & ConsecutivoArqueo & "','" & Fecha2 & "','" & FrmArqueo.CboCajero.Text & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL ENCABEZADO DEL ARQUEO///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "UPDATE [Arqueo] SET [TotalCordobasDolares] = " & CDbl(FrmArqueo.TxtCordobasDolares.Text) & ",[ValorFacturas] = " & CDbl(FrmArqueo.TxtValorFacturas.Text) & ",[Observaciones] = '" & FrmArqueo.TxtObservaciones.Text & "',[PracticadoPor] = '" & FrmArqueo.TxtPracticadoPor.Text & "' " & _
                         "WHERE (CodArqueo = '" & ConsecutivoArqueo & "') AND (FechaArqueo = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Cod_Cajero = '" & FrmArqueo.CboCajero.Text & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If

    End Sub


    Public Sub GrabaDetalleArqueoCheque(ByVal FechaArqueo As Date, ByVal ConsecutivoArqueo As String, ByVal Moneda As String, ByVal Monto As Double, ByVal NumeroFactura As String, ByVal NombrePago As String, ByVal NumeroTarjeta As String, ByVal FechaVence As Date)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, Fecha2 As String, SQlString As String = ""
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter

        Fecha = Format(FechaArqueo, "yyyy-MM-dd")
        Fecha2 = Format(FechaArqueo, "dd/MM/yyyy")

        MiConexion.Close()

        'SQlString = "SELECT * FROM Detalle_Arqueo WHERE (CodArqueo = '" & ConsecutivoArqueo & "') AND (FechaArqueo = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (idDenominacion = " & idDenominacion & ") AND (Moneda = '" & Moneda & "')"
        SQlString = "SELECT  idDetalleArqueoCheque, CodArqueo, FechaArqueo, Modena, NumeroFactura, NombrePago, NumeroTarjeta, Fecha_Vence, Monto FROM Detalle_ArqueoCheque WHERE (CodArqueo = '" & ConsecutivoArqueo & "') AND (NumeroFactura = '" & NumeroFactura & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
        DataAdapter.Fill(DataSet, "Arqueo")
        If DataSet.Tables("Arqueo").Rows.Count = 0 Then

            'If FrmArqueo.TxtNumeroEnsamble.Text = "-----0-----" Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL ENCABEZADO DEL ARQUEO//////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "INSERT INTO [Detalle_ArqueoCheque] ([CodArqueo],[FechaArqueo],[Modena],[NumeroFactura],[NombrePago],[NumeroTarjeta],[Fecha_Vence],[Monto]) " & _
                         "VALUES ('" & ConsecutivoArqueo & "' , CONVERT(DATETIME, '" & Fecha & "', 102) , '" & Moneda & "', '" & NumeroFactura & "', '" & NombrePago & "' , '" & NumeroTarjeta & "', CONVERT(DATETIME, '" & FechaVence & "', 102) ," & Monto & ")"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL ENCABEZADO DEL ARQUEO///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "UPDATE [Detalle_ArqueoCheque] SET [NombrePago] = '" & NombrePago & "',[Monto] = '" & Monto & "' " & _
                         "WHERE (CodArqueo = '" & ConsecutivoArqueo & "') AND (NumeroFactura = '" & NumeroFactura & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If

    End Sub
    Public Sub GrabaDetalleArqueo(ByVal ConsecutivoArqueo As String, ByVal Moneda As String, ByVal idDenominacion As Double, ByVal Valor As String, ByVal Cantidad As Double, ByVal Monto As Double)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, Fecha2 As String, SQlString As String = ""
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter

        Fecha = Format(FrmArqueo.DTFecha.Value, "yyyy-MM-dd")
        Fecha2 = Format(FrmArqueo.DTFecha.Value, "dd/MM/yyyy")

        MiConexion.Close()

        SQlString = "SELECT * FROM Detalle_Arqueo WHERE (CodArqueo = '" & ConsecutivoArqueo & "') AND (FechaArqueo = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (idDenominacion = " & idDenominacion & ") AND (Moneda = '" & Moneda & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
        DataAdapter.Fill(DataSet, "Arqueo")
        If DataSet.Tables("Arqueo").Rows.Count = 0 Then

            'If FrmArqueo.TxtNumeroEnsamble.Text = "-----0-----" Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL ENCABEZADO DEL ARQUEO//////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "INSERT INTO [Detalle_Arqueo] ([CodArqueo],[FechaArqueo],[Moneda],[idDenominacion],[Valor]) " & _
                         "VALUES ('" & ConsecutivoArqueo & "','" & Fecha2 & "','" & Moneda & "','" & idDenominacion & "','" & Valor & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL ENCABEZADO DEL ARQUEO///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "UPDATE [Detalle_Arqueo] SET [Canitdad] = " & Cantidad & ",[Monto] = " & Monto & " " & _
                         "WHERE (CodArqueo = '" & ConsecutivoArqueo & "') AND (FechaArqueo = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (idDenominacion = " & idDenominacion & ") AND (Moneda = '" & Moneda & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If

    End Sub

    Public Sub ActualizaMetodoArqueo()
        Dim iPosicion As Double, Registros As Double, Monto As Double, MontoDolar As Double, MontoCheque As Double, MontoDolarCheque As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), SQlEfectivo As String, MontoEfectivo As Double, MontoEfectivoDolar As Double
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, CodCajero As String, Total As Double
        Dim TasaCambio As Double, FechaTasa As String, SubTotal As Double, TotalRecibido As Double

        FechaTasa = FrmArqueo.DTFecha.Value
        CodCajero = FrmArqueo.CboCajero.Text

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////SUMO EL TOTAL DEL METODO CORDOBAS/////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Registros = FrmArqueo.BindingDetalleCordobas.Count
        iPosicion = 0
        Monto = 0
        Do While iPosicion < Registros
            Monto = FrmArqueo.BindingDetalleCordobas.Item(iPosicion)("Monto") + Monto
            iPosicion = iPosicion + 1
        Loop
        Total = Monto

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////SUMO EL TOTAL DEL METODO DOLARES/////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Registros = FrmArqueo.BindingDetalleDolares.Count
        iPosicion = 0
        MontoDolar = 0
        SubTotal = 0
        Do While iPosicion < Registros
            MontoDolar = FrmArqueo.BindingDetalleDolares.Item(iPosicion)("Monto") + MontoDolar
            TasaCambio = BuscaTasaCambio(FechaTasa)
            SubTotal = (FrmArqueo.BindingDetalleDolares.Item(iPosicion)("Monto") * TasaCambio) + SubTotal
            iPosicion = iPosicion + 1
        Loop

        Total = SubTotal + Total
        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////SUMO EL TOTAL DEL METODO CHEQUE CORDOBAS/////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Registros = FrmArqueo.BindingDetalleChequeCordobas.Count
        iPosicion = 0
        MontoCheque = 0
        TotalRecibido = 0
        Do While iPosicion < Registros
            MontoCheque = FrmArqueo.BindingDetalleChequeCordobas.Item(iPosicion)("Monto") + MontoCheque
            iPosicion = iPosicion + 1
        Loop
        Total = MontoCheque + Total
        TotalRecibido = MontoCheque

        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////SUMO EL TOTAL DEL METODO CHEQUE DOLARES/////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Registros = FrmArqueo.BindingDetalleChequeDolares.Count
        SubTotal = 0
        iPosicion = 0
        MontoDolarCheque = 0
        Do While iPosicion < Registros
            MontoDolarCheque = FrmArqueo.BindingDetalleChequeDolares.Item(iPosicion)("Monto") + MontoDolarCheque
            TasaCambio = BuscaTasaCambio(FechaTasa)
            SubTotal = (FrmArqueo.BindingDetalleChequeDolares.Item(iPosicion)("Monto") * TasaCambio) + SubTotal
            iPosicion = iPosicion + 1
        Loop

        Total = SubTotal + Total
        TotalRecibido = SubTotal + TotalRecibido

        '////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////SUMO EL EFECTIVO CORDOBAS DE LA FACTURA /////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////

        SQlEfectivo = "SELECT Detalle_MetodoFacturas.Numero_Factura, Detalle_MetodoFacturas.NombrePago, Detalle_MetodoFacturas.NumeroTarjeta,Detalle_MetodoFacturas.Monto, Detalle_MetodoFacturas.FechaVence FROM  Detalle_MetodoFacturas INNER JOIN MetodoPago ON Detalle_MetodoFacturas.NombrePago = MetodoPago.NombrePago INNER JOIN Facturas ON Detalle_MetodoFacturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_MetodoFacturas.Fecha_Factura = Facturas.Fecha_Factura And Detalle_MetodoFacturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                     "WHERE (Detalle_MetodoFacturas.Arqueado = 0) AND (Detalle_MetodoFacturas.Tipo_Factura = 'Factura') AND (MetodoPago.TipoPago = 'Efectivo') AND (MetodoPago.Moneda = 'Cordobas') AND (Facturas.Cod_Cajero = '" & CodCajero & "') AND (Facturas.Fecha_Factura = CONVERT(DATETIME, '" & Format(FrmArqueo.DTFecha.Value, "yyyy-MM-dd") & "', 102)) " & _
                     "ORDER BY Detalle_MetodoFacturas.Numero_Factura "
        DataAdapter = New SqlClient.SqlDataAdapter(SQlEfectivo, MiConexion)
        DataAdapter.Fill(DataSet, "Efectivo")
        iPosicion = 0
        MontoEfectivo = 0
        Registros = DataSet.Tables("Efectivo").Rows.Count
        If DataSet.Tables("Efectivo").Rows.Count <> 0 Then
            Do While iPosicion < Registros
                MontoEfectivo = DataSet.Tables("Efectivo").Rows(iPosicion)("Monto") + MontoEfectivo
                iPosicion = iPosicion + 1
            Loop
        End If
        TotalRecibido = MontoEfectivo + TotalRecibido
        DataSet.Reset()


        '////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////SUMO EL EFECTIVO  DOLARES DE LAS FACTURAS /////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////


        SQlEfectivo = "SELECT Detalle_MetodoFacturas.Numero_Factura, Detalle_MetodoFacturas.NombrePago, Detalle_MetodoFacturas.NumeroTarjeta,Detalle_MetodoFacturas.Monto, Detalle_MetodoFacturas.FechaVence FROM  Detalle_MetodoFacturas INNER JOIN MetodoPago ON Detalle_MetodoFacturas.NombrePago = MetodoPago.NombrePago INNER JOIN Facturas ON Detalle_MetodoFacturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_MetodoFacturas.Fecha_Factura = Facturas.Fecha_Factura And Detalle_MetodoFacturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                     "WHERE (Detalle_MetodoFacturas.Arqueado = 0) AND (Detalle_MetodoFacturas.Tipo_Factura = 'Factura') AND (MetodoPago.TipoPago = 'Efectivo') AND (MetodoPago.Moneda = 'Dolares') AND (Facturas.Cod_Cajero = '" & CodCajero & "') AND (Facturas.Fecha_Factura = CONVERT(DATETIME, '" & Format(FrmArqueo.DTFecha.Value, "yyyy-MM-dd") & "', 102))" & _
                     "ORDER BY Detalle_MetodoFacturas.Numero_Factura "
        DataAdapter = New SqlClient.SqlDataAdapter(SQlEfectivo, MiConexion)
        DataAdapter.Fill(DataSet, "EfectivoD")
        iPosicion = 0
        MontoEfectivoDolar = 0
        SubTotal = 0
        Registros = DataSet.Tables("EfectivoD").Rows.Count
        If DataSet.Tables("EfectivoD").Rows.Count <> 0 Then
            Do While iPosicion < Registros
                MontoEfectivoDolar = DataSet.Tables("EfectivoD").Rows(iPosicion)("Monto") + MontoEfectivoDolar
                TasaCambio = BuscaTasaCambio(FechaTasa)
                SubTotal = (DataSet.Tables("EfectivoD").Rows(iPosicion)("Monto") * TasaCambio) + SubTotal
                iPosicion = iPosicion + 1
            Loop
        End If
        TotalRecibido = SubTotal + TotalRecibido
        DataSet.Reset()

        '////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////SUMO EL EFECTIVO DOLARES RECIBOS /////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////
        SQlEfectivo = "SELECT Detalle_MetodoRecibo.CodRecibo, Detalle_MetodoRecibo.NombrePago, Detalle_MetodoRecibo.NumeroTarjeta, Detalle_MetodoRecibo.Monto,Recibo.Cod_Cajero, MetodoPago.Moneda FROM Detalle_MetodoRecibo INNER JOIN Recibo ON Detalle_MetodoRecibo.CodRecibo = Recibo.CodReciboPago AND Detalle_MetodoRecibo.Fecha_Recibo = Recibo.Fecha_Recibo INNER JOIN MetodoPago ON Detalle_MetodoRecibo.NombrePago = MetodoPago.NombrePago  " & _
                      "WHERE (Detalle_MetodoRecibo.Arqueado = 0) AND (MetodoPago.TipoPago = N'Efectivo') AND (Recibo.Cod_Cajero = '" & CodCajero & "') AND (MetodoPago.Moneda = N'Dolares')  AND (Recibo.Fecha_Recibo = CONVERT(DATETIME, '" & Format(FrmArqueo.DTFecha.Value, "yyyy-MM-dd") & "', 102))"
        DataAdapter = New SqlClient.SqlDataAdapter(SQlEfectivo, MiConexion)
        DataAdapter.Fill(DataSet, "Recibo")
        iPosicion = 0
        SubTotal = 0
        Registros = DataSet.Tables("Recibo").Rows.Count
        If DataSet.Tables("Recibo").Rows.Count <> 0 Then
            Do While iPosicion < Registros
                MontoEfectivoDolar = DataSet.Tables("Recibo").Rows(iPosicion)("Monto") + MontoEfectivoDolar
                TasaCambio = BuscaTasaCambio(FechaTasa)
                SubTotal = (DataSet.Tables("Recibo").Rows(iPosicion)("Monto") * TasaCambio) + SubTotal
                iPosicion = iPosicion + 1
            Loop
        End If
        TotalRecibido = SubTotal + TotalRecibido
        DataSet.Reset()

        '////////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////////SUMO EL EFECTIVO CORDOBAS DEL RECIBO /////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////
        SQlEfectivo = "SELECT Detalle_MetodoRecibo.CodRecibo, Detalle_MetodoRecibo.NombrePago, Detalle_MetodoRecibo.NumeroTarjeta, Detalle_MetodoRecibo.Monto,Recibo.Cod_Cajero, MetodoPago.Moneda FROM Detalle_MetodoRecibo INNER JOIN Recibo ON Detalle_MetodoRecibo.CodRecibo = Recibo.CodReciboPago AND Detalle_MetodoRecibo.Fecha_Recibo = Recibo.Fecha_Recibo INNER JOIN MetodoPago ON Detalle_MetodoRecibo.NombrePago = MetodoPago.NombrePago  " & _
                      "WHERE (Detalle_MetodoRecibo.Arqueado = 0) AND (MetodoPago.TipoPago = 'Efectivo') AND (Recibo.Cod_Cajero = '" & CodCajero & "') AND (MetodoPago.Moneda = 'Cordobas') AND (Recibo.Fecha_Recibo = CONVERT(DATETIME, '" & Format(FrmArqueo.DTFecha.Value, "yyyy-MM-dd") & "', 102))"
        DataAdapter = New SqlClient.SqlDataAdapter(SQlEfectivo, MiConexion)
        DataAdapter.Fill(DataSet, "Efectivo")
        iPosicion = 0
        Registros = DataSet.Tables("Efectivo").Rows.Count
        If DataSet.Tables("Efectivo").Rows.Count <> 0 Then
            Do While iPosicion < Registros
                MontoEfectivo = DataSet.Tables("Efectivo").Rows(iPosicion)("Monto") + MontoEfectivo
                iPosicion = iPosicion + 1
            Loop
        End If
        TotalRecibido = MontoEfectivo + TotalRecibido
        DataSet.Reset()

        FrmArqueo.TxtDiferencia.Text = Format(TotalRecibido - Total, "##,##0.00")
        FrmArqueo.TxtValorFacturas.Text = Format(TotalRecibido, "##,##0.00")
        FrmArqueo.TxtCordobasDolares.Text = Format(Total, "##,##0.00")
        FrmArqueo.TxtSumaFacturaCordobas.Text = Format(MontoEfectivo, "##,##0.00")
        FrmArqueo.TxtSumaFacturaDolares.Text = Format(MontoEfectivoDolar, "##,##0.00")
        FrmArqueo.TxtTotalChequeCordobas.Text = Format(MontoCheque, "##,##0.00")
        FrmArqueo.TxtTotalChequeDolares.Text = Format(MontoDolarCheque, "##,##0.00")
        FrmArqueo.TxtSubTotalCordobas.Text = Format(Monto, "##,##0.00")
        FrmArqueo.TxtSubTotalDolares.Text = Format(MontoDolar, "##,##0.00")
    End Sub

    'Public Sub GrabaDetalleArqueoCheque(ByVal ConsecutivoArqueo As String, ByVal Moneda As String, ByVal NumeroTarjeta As String, ByVal FechaVence As String, ByVal Monto As Double, ByVal NumeroFactura As String, ByVal NombrePago As String)
    '    Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
    '    Dim Fecha As String, Fecha2 As String
    '    Dim MiConexion As New SqlClient.SqlConnection(Conexion)
    '    Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter

    '    Fecha = Format(FrmArqueo.DTFecha.Value, "yyyy-MM-dd")
    '    Fecha2 = Format(FrmArqueo.DTFecha.Value, "dd/MM/yyyy")

    '    MiConexion.Close()

    '    If FrmArqueo.TxtNumeroEnsamble.Text = "-----0-----" Then
    '        '//////////////////////////////////////////////////////////////////////////////////////////////
    '        '////////////////////////////AGREGO EL DETALLE CHEQUES/////////////////////////////////
    '        '/////////////////////////////////////////////////////////////////////////////////////////////////
    '        SqlCompras = "INSERT INTO [Detalle_ArqueoCheque] ([CodArqueo],[FechaArqueo],[Modena],[NumeroFactura],[NombrePago],[NumeroTarjeta],[Fecha_Vence],[Monto]) " & _
    '                     "VALUES('" & ConsecutivoArqueo & "','" & Fecha2 & "','" & Moneda & "','" & NumeroFactura & "','" & NombrePago & "','" & NumeroTarjeta & "','" & FechaVence & "'," & Monto & ")"
    '        MiConexion.Open()
    '        ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
    '        iResultado = ComandoUpdate.ExecuteNonQuery
    '        MiConexion.Close()

    '    Else
    '        '//////////////////////////////////////////////////////////////////////////////////////////////
    '        '////////////////////////////EDITO EL ENCABEZADO DEL ARQUEO///////////////////////////////////
    '        '////////////////////////////////////////////////////////////////////////////////////////////////
    '        SqlCompras = "UPDATE [Detalle_ArqueoCheque] SET [NumeroTarjeta] = '" & NumeroTarjeta & "',[Fecha_Vence] = '" & FechaVence & "',[Monto] = " & Monto & ",[NumeroFactura] = '" & NumeroFactura & "',[NombrePago] = '" & NombrePago & "'  " & _
    '                     "WHERE (CodArqueo = '" & ConsecutivoArqueo & "') AND (FechaArqueo = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Moneda = '" & Moneda & "')"
    '        MiConexion.Open()
    '        ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
    '        iResultado = ComandoUpdate.ExecuteNonQuery
    '        MiConexion.Close()
    '    End If

    'End Sub


    Public Function BuscaTasaCambio(ByVal FechaTasa As Date) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim SQlTasa As String, TasaCambio As Double, Fecha As String

        Fecha = Format(FechaTasa, "yyyy-MM-dd")
        TasaCambio = 0
        SQlTasa = "SELECT  * FROM TasaCambio WHERE (FechaTasa = CONVERT(DATETIME, '" & Fecha & "', 102))"
        DataAdapter = New SqlClient.SqlDataAdapter(SQlTasa, MiConexion)
        My.Application.DoEvents()
        DataAdapter.Fill(DataSet, "TasaCambio")
        If DataSet.Tables("TasaCambio").Rows.Count <> 0 Then
            TasaCambio = DataSet.Tables("TasaCambio").Rows(0)("MontoTasa")
        End If

        'If TasaCambio = 0 Then
        '    TasaCambio = 1
        'End If

        BuscaTasaCambio = TasaCambio
    End Function

    Public Function BuscaTasaCambioContabilidad(ByVal FechaTasa As Date) As Double
        Dim MiconexionContabilidad As New SqlClient.SqlConnection(ConexionContabilidad)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim SQlTasa As String, TasaCambio As Double, Fecha As String

        Try
            Fecha = Format(FechaTasa, "yyyy-MM-dd")
            TasaCambio = 0
            SQlTasa = "SELECT  *  FROM Tasas WHERE (FechaTasas = CONVERT(DATETIME, '" & Fecha & "', 102))"
            DataAdapter = New SqlClient.SqlDataAdapter(SQlTasa, MiconexionContabilidad)
            DataAdapter.Fill(DataSet, "TasaCambio")
            If DataSet.Tables("TasaCambio").Rows.Count <> 0 Then
                TasaCambio = DataSet.Tables("TasaCambio").Rows(0)("MontoCordobas")
            End If

            BuscaTasaCambioContabilidad = TasaCambio
            ExisteConexion = True
        Catch ex As Exception
            MsgBox("No se pudo Sincronizar la tasa con el Sistema Contable", MsgBoxStyle.Critical, "Zeus Facturacion")
            ExisteConexion = False
        End Try



    End Function

    Public Function BuscaExistencia(ByVal CodigoProducto As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, Fecha As String, UnidadComprada As Double
        Dim SQlInventarioFisico As String, TasaCambio As Double, Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0, TransferenciaEnviada As Double = 0, TransferenciaRecibida = 0

        '///////////////////////////FORMULA DE COMPRA PROMEDIO////////////////////////////////////////////////////////////////
        ' CostoPromedio= ((Existencia*Costo)+(PrecioCompra*CantidadCompra))/(Existencia+CantidadComprada)
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        '///////////////////////////BUSCO LA EXISTENCIA SEGUN EL ULTIMO INVENTARIO FISICO////////////////////////////////////////


        TasaCambio = 0
        SQlInventarioFisico = "SELECT * FROM InventarioFisico WHERE (Cod_Producto = '" & CodigoProducto & "') AND (Activo = 0) ORDER BY Fecha_Conteo DESC"
        DataAdapter = New SqlClient.SqlDataAdapter(SQlInventarioFisico, MiConexion)
        DataAdapter.Fill(DataSet, "InventarioFisico")
        If DataSet.Tables("InventarioFisico").Rows.Count <> 0 Then
            Existencia = 0
            'Existencia = DataSet.Tables("InventarioFisico").Rows(0)("Cantidad_Contada")

            Fecha = Format(CDate(DataSet.Tables("InventarioFisico").Rows(0)("Fecha_Conteo")), "yyyy-MM-dd")

            '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad  FROM Detalle_Compras WHERE (Cod_Producto = '" & CodigoProducto & "') GROUP BY Tipo_Compra HAVING  (Tipo_Compra = N'Mercancia Recibida') OR (Tipo_Compra = N'Transferencia Recibida')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "Compras")
            If DataSet.Tables("Compras").Rows.Count <> 0 Then
                UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
            End If

            '//////////////////////////////////BUSCO EL TOTAL DE LAS DEVOLUCION DE COMPRAS////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad  FROM Detalle_Compras WHERE (Cod_Producto = '" & CodigoProducto & "') GROUP BY Tipo_Compra HAVING  (Tipo_Compra = N'Devolucion de Compra') "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "DevolucionCompras")
            If DataSet.Tables("DevolucionCompras").Rows.Count <> 0 Then
                DevolucionCompra = DataSet.Tables("Compras").Rows(0)("Cantidad")
            End If

            '////////////////////////////////////BUSCO EL TOTAL DE LAS FACTURAS//////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad FROM   Detalle_Facturas WHERE (Tipo_Factura = 'Factura') AND (Cod_Producto = '" & CodigoProducto & "') OR (Tipo_Factura = 'Transferencia Enviada') "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "Facturas")
            If DataSet.Tables("Facturas").Rows.Count <> 0 Then
                If Not IsDBNull(DataSet.Tables("Facturas").Rows(0)("Cantidad")) Then
                    UnidadFacturada = DataSet.Tables("Facturas").Rows(0)("Cantidad")
                Else
                    UnidadFacturada = 0
                End If
            End If

            DataSet.Reset()
            '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad FROM   Detalle_Facturas WHERE (Tipo_Factura = 'Devolucion de Venta') AND (Cod_Producto = '" & CodigoProducto & "') "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "DevolucionFacturas")
            If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
                If Not IsDBNull(DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")) Then
                    DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
                End If
            End If

            DataSet.Reset()
            '////////////////////////////////////BUSCO EL TOTAL DE LA TRANSFERENCIA ENVIADA //////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad FROM   Detalle_Facturas WHERE (Tipo_Factura = 'Transferencia Enviada') AND (Cod_Producto = '" & CodigoProducto & "') "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "TransferenciaEnviada")
            If DataSet.Tables("TransferenciaEnviada").Rows.Count <> 0 Then
                If Not IsDBNull(DataSet.Tables("TransferenciaEnviada").Rows(0)("Cantidad")) Then
                    TransferenciaEnviada = DataSet.Tables("TransferenciaEnviada").Rows(0)("Cantidad")
                End If
            End If

            DataSet.Reset()
            '////////////////////////////////////BUSCO EL TOTAL DE LA TRANSFERENCIA ENVIADA //////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad FROM   Detalle_Facturas WHERE (Tipo_Factura = 'Transferencia Recibida') AND (Cod_Producto = '" & CodigoProducto & "') "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "TransferenciaRecibida")
            If DataSet.Tables("TransferenciaRecibida").Rows.Count <> 0 Then
                If Not IsDBNull(DataSet.Tables("TransferenciaRecibida").Rows(0)("Cantidad")) Then
                    TransferenciaRecibida = DataSet.Tables("TransferenciaRecibida").Rows(0)("Cantidad")
                End If
            End If


        Else
            '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad  FROM Detalle_Compras WHERE (Cod_Producto = '" & CodigoProducto & "') GROUP BY Tipo_Compra HAVING  (Tipo_Compra = N'Mercancia Recibida') OR (Tipo_Compra = N'Transferencia Recibida')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "Compras")
            If DataSet.Tables("Compras").Rows.Count <> 0 Then
                UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
            End If

            '//////////////////////////////////BUSCO EL TOTAL DE LAS DEVOLUCION DE COMPRAS////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad  FROM Detalle_Compras WHERE (Cod_Producto = '" & CodigoProducto & "') GROUP BY Tipo_Compra HAVING  (Tipo_Compra = N'Devolucion de Compra') "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "DevolucionCompras")
            If DataSet.Tables("DevolucionCompras").Rows.Count <> 0 Then
                DevolucionCompra = DataSet.Tables("Compras").Rows(0)("Cantidad")
            End If

            '////////////////////////////////////BUSCO EL TOTAL DE LAS FACTURAS//////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad FROM   Detalle_Facturas WHERE (Tipo_Factura = 'Factura') AND (Cod_Producto = '" & CodigoProducto & "') OR (Tipo_Factura = 'Transferencia Enviada') "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "Facturas")
            If DataSet.Tables("Facturas").Rows.Count <> 0 Then
                If Not IsDBNull(DataSet.Tables("Facturas").Rows(0)("Cantidad")) Then
                    UnidadFacturada = DataSet.Tables("Facturas").Rows(0)("Cantidad")
                Else
                    UnidadFacturada = 0
                End If
            End If

            DataSet.Reset()
            '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad FROM   Detalle_Facturas WHERE (Tipo_Factura = 'Devolucion de Venta') AND (Cod_Producto = '" & CodigoProducto & "') "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "DevolucionFacturas")
            If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
                If Not IsDBNull(DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")) Then
                    DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
                End If
            End If

            DataSet.Reset()
            '////////////////////////////////////BUSCO EL TOTAL DE LA TRANSFERENCIA ENVIADA //////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad FROM   Detalle_Facturas WHERE (Tipo_Factura = 'Transferencia Enviada') AND (Cod_Producto = '" & CodigoProducto & "') "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "TransferenciaEnviada")
            If DataSet.Tables("TransferenciaEnviada").Rows.Count <> 0 Then
                If Not IsDBNull(DataSet.Tables("TransferenciaEnviada").Rows(0)("Cantidad")) Then
                    TransferenciaEnviada = DataSet.Tables("TransferenciaEnviada").Rows(0)("Cantidad")
                End If
            End If

            DataSet.Reset()
            '////////////////////////////////////BUSCO EL TOTAL DE LA TRANSFERENCIA ENVIADA //////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad FROM   Detalle_Facturas WHERE (Tipo_Factura = 'Transferencia Recibida') AND (Cod_Producto = '" & CodigoProducto & "') "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "TransferenciaRecibida")
            If DataSet.Tables("TransferenciaRecibida").Rows.Count <> 0 Then
                If Not IsDBNull(DataSet.Tables("TransferenciaRecibida").Rows(0)("Cantidad")) Then
                    TransferenciaRecibida = DataSet.Tables("TransferenciaRecibida").Rows(0)("Cantidad")
                End If
            End If
        End If

        Existencia = Existencia + UnidadComprada - DevolucionCompra - UnidadFacturada + DevolucionFactura - TransferenciaEnviada + TransferenciaRecibida
        BuscaExistencia = Existencia
    End Function

    Public Function BuscaExistenciaBodega(ByVal CodigoProducto As String, ByVal CodigoBodega As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, Fecha As String, UnidadComprada As Double
        Dim SQlInventarioFisico As String, TasaCambio As Double, Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0, TransferenciaEnviada As Double = 0, TransferenciaRecibida As Double = 0
        Dim SalidaBodega As Double = 0, CostoVenta As Double = 0, ImporteFactura As Double = 0
        Dim ImporteCompra As Double, ImporteDevCompra As Double = 0, ImporteVenta As Double = 0, ImporteSalida As Double = 0
        Dim ImporteDevFactura As Double = 0

        '///////////////////////////FORMULA DE COMPRA PROMEDIO////////////////////////////////////////////////////////////////
        ' CostoPromedio= ((Existencia*Costo)+(PrecioCompra*CantidadCompra))/(Existencia+CantidadComprada)
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        '///////////////////////////BUSCO LA EXISTENCIA SEGUN EL ULTIMO INVENTARIO FISICO////////////////////////////////////////

        CostoVenta = CostoPromedio(CodigoProducto)

        TasaCambio = 0
        SQlInventarioFisico = "SELECT * FROM InventarioFisico WHERE (Cod_Producto = '" & CodigoProducto & "') AND (Activo = 0) AND (CodBodega = '" & CodigoBodega & "') ORDER BY Fecha_Conteo DESC"
        DataAdapter = New SqlClient.SqlDataAdapter(SQlInventarioFisico, MiConexion)
        DataAdapter.Fill(DataSet, "InventarioFisico")
        If DataSet.Tables("InventarioFisico").Rows.Count <> 0 Then
            Existencia = 0
            'Existencia = DataSet.Tables("InventarioFisico").Rows(0)("Cantidad_Contada")

            Fecha = Format(CDate(DataSet.Tables("InventarioFisico").Rows(0)("Fecha_Conteo")), "yyyy-MM-dd")

            '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra  " & _
                          "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodigoBodega & "')  GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "Compras")
            If DataSet.Tables("Compras").Rows.Count <> 0 Then
                UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
            End If

            '//////////////////////////////////BUSCO EL TOTAL DE LAS DEVOLUCION DE COMPRAS////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT  SUM(Detalle_Compras.Cantidad) AS Cantidad FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra " & _
                          "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodigoBodega & "') GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = N'Devolucion de Compra')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "DevolucionCompras")
            If DataSet.Tables("DevolucionCompras").Rows.Count <> 0 Then
                DevolucionCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Cantidad")
            End If

            '////////////////////////////////////BUSCO EL TOTAL DE LAS FACTURAS//////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura " & _
                          "WHERE (Detalle_Facturas.Tipo_Factura = 'Factura') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega =  '" & CodigoBodega & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "Facturas")
            If DataSet.Tables("Facturas").Rows.Count <> 0 Then
                If Not IsDBNull(DataSet.Tables("Facturas").Rows(0)("Cantidad")) Then
                    UnidadFacturada = DataSet.Tables("Facturas").Rows(0)("Cantidad")
                    ImporteVenta = DataSet.Tables("Facturas").Rows(0)("Cantidad") * CostoVenta
                Else
                    UnidadFacturada = 0
                    ImporteVenta = 0
                End If
            End If

            '////////////////////////////////////BUSCO EL TOTAL DE LA SALIDA DE BODEGA//////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura " & _
                          "WHERE (Detalle_Facturas.Tipo_Factura = 'Salida Bodega') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega =  '" & CodigoBodega & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "SalidaBodega")
            If DataSet.Tables("SalidaBodega").Rows.Count <> 0 Then
                If Not IsDBNull(DataSet.Tables("SalidaBodega").Rows(0)("Cantidad")) Then
                    SalidaBodega = DataSet.Tables("SalidaBodega").Rows(0)("Cantidad")
                    ImporteSalida = DataSet.Tables("SalidaBodega").Rows(0)("Cantidad") * CostoVenta
                Else
                    SalidaBodega = 0
                    ImporteSalida = 0
                End If
            End If

            DataSet.Reset()
            '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT     SUM(Detalle_Facturas.Cantidad) AS Cantidad FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                          "WHERE  (Detalle_Facturas.Tipo_Factura = 'Devolucion de Venta') AND (Detalle_Facturas.Cod_Producto =  '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodigoBodega & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "DevolucionFacturas")
            If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
                If Not IsDBNull(DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")) Then
                    DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")

                End If
            End If

            DataSet.Reset()
            '////////////////////////////////////BUSCO EL TOTAL DE TRANSFERENCIAS ENVIADAS  //////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura " & _
                          "WHERE (Detalle_Facturas.Tipo_Factura = 'Transferencia Enviada') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Su_Referencia = '" & CodigoBodega & "') AND (Facturas.TransferenciaProcesada = 1) "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "DevolucionFacturas")
            If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
                If Not IsDBNull(DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")) Then
                    TransferenciaEnviada = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
                End If
            End If

            DataSet.Reset()
            '////////////////////////////////////BUSCO EL TOTAL DE TRANSFERENCIAS RECIBIDAS  //////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT     SUM(Detalle_Compras.Cantidad) AS Cantidad FROM Compras INNER JOIN Detalle_Compras ON Compras.Numero_Compra = Detalle_Compras.Numero_Compra AND Compras.Fecha_Compra = Detalle_Compras.Fecha_Compra AND Compras.Tipo_Compra = Detalle_Compras.Tipo_Compra " & _
                          "WHERE (Compras.TransferenciaProcesada = 1) AND (Compras.Cod_Bodega = '" & CodigoBodega & "') AND (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Tipo_Compra = 'Transferencia Recibida')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "TransferenciasRecibidas")
            If DataSet.Tables("TransferenciasRecibidas").Rows.Count <> 0 Then
                If Not IsDBNull(DataSet.Tables("TransferenciasRecibidas").Rows(0)("Cantidad")) Then
                    TransferenciaRecibida = DataSet.Tables("TransferenciasRecibidas").Rows(0)("Cantidad")
                End If
            End If

            'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura " & _
            '              "WHERE (Detalle_Facturas.Tipo_Factura = 'Transferencia Enviada') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Nuestra_Referencia =  '" & CodigoBodega & "') AND (Facturas.TransferenciaProcesada = 1)"
            'DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            'DataAdapter.Fill(DataSet, "TransferenciasRecibidas")
            'If DataSet.Tables("TransferenciasRecibidas").Rows.Count <> 0 Then
            '    If Not IsDBNull(DataSet.Tables("TransferenciasRecibidas").Rows(0)("Cantidad")) Then
            '        TransferenciaRecibida = DataSet.Tables("TransferenciasRecibidas").Rows(0)("Cantidad")
            '    End If
            'End If
        Else
            '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) AS Importe  FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra  " & _
                          "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodigoBodega & "')  GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "Compras")
            If DataSet.Tables("Compras").Rows.Count <> 0 Then
                UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
                ImporteCompra = DataSet.Tables("Compras").Rows(0)("Importe")
            End If

            '//////////////////////////////////BUSCO EL TOTAL DE LAS DEVOLUCION DE COMPRAS////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT  SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra " & _
                          "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodigoBodega & "') GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = N'Devolucion de Compra')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "DevolucionCompras")
            If DataSet.Tables("DevolucionCompras").Rows.Count <> 0 Then
                DevolucionCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Cantidad")
                ImporteDevCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
            End If

            '////////////////////////////////////BUSCO EL TOTAL DE LAS FACTURAS//////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura " & _
                          "WHERE (Detalle_Facturas.Tipo_Factura = 'Factura') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega =  '" & CodigoBodega & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "Facturas")
            If DataSet.Tables("Facturas").Rows.Count <> 0 Then
                If Not IsDBNull(DataSet.Tables("Facturas").Rows(0)("Cantidad")) Then
                    UnidadFacturada = DataSet.Tables("Facturas").Rows(0)("Cantidad")
                    ImporteVenta = DataSet.Tables("Facturas").Rows(0)("Cantidad") * CostoVenta
                Else
                    UnidadFacturada = 0
                    ImporteVenta = 0
                End If
            End If

            '////////////////////////////////////BUSCO EL TOTAL DE LA SALIDA DE BODEGA//////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura " & _
                          "WHERE (Detalle_Facturas.Tipo_Factura = 'Salida Bodega') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega =  '" & CodigoBodega & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "SalidaBodega")
            If DataSet.Tables("SalidaBodega").Rows.Count <> 0 Then
                If Not IsDBNull(DataSet.Tables("SalidaBodega").Rows(0)("Cantidad")) Then
                    SalidaBodega = DataSet.Tables("SalidaBodega").Rows(0)("Cantidad")
                Else
                    SalidaBodega = 0
                End If
            End If

            DataSet.Reset()
            '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT     SUM(Detalle_Facturas.Cantidad) AS Cantidad FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                          "WHERE  (Detalle_Facturas.Tipo_Factura = 'Devolucion de Venta') AND (Detalle_Facturas.Cod_Producto =  '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodigoBodega & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "DevolucionFacturas")
            If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
                If Not IsDBNull(DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")) Then
                    DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
                    ImporteDevFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad") * CostoVenta
                End If
            End If

            DataSet.Reset()
            '////////////////////////////////////BUSCO EL TOTAL DE TRANSFERENCIAS ENVIADAS  //////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura " & _
                          "WHERE (Detalle_Facturas.Tipo_Factura = 'Transferencia Enviada') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Su_Referencia = '" & CodigoBodega & "') AND (Facturas.TransferenciaProcesada = 1) "
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "DevolucionFacturas")
            If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
                If Not IsDBNull(DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")) Then
                    TransferenciaEnviada = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
                End If
            End If

            DataSet.Reset()
            '////////////////////////////////////BUSCO EL TOTAL DE TRANSFERENCIAS RECIBIDAS  //////////////////////////////////////////////////////////////////////
            SqlConsulta = "SELECT     SUM(Detalle_Compras.Cantidad) AS Cantidad FROM Compras INNER JOIN Detalle_Compras ON Compras.Numero_Compra = Detalle_Compras.Numero_Compra AND Compras.Fecha_Compra = Detalle_Compras.Fecha_Compra AND Compras.Tipo_Compra = Detalle_Compras.Tipo_Compra " & _
                          "WHERE (Compras.TransferenciaProcesada = 1) AND (Compras.Cod_Bodega = '" & CodigoBodega & "') AND (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Tipo_Compra = 'Transferencia Recibida')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "TransferenciasRecibidas")
            If DataSet.Tables("TransferenciasRecibidas").Rows.Count <> 0 Then
                If Not IsDBNull(DataSet.Tables("TransferenciasRecibidas").Rows(0)("Cantidad")) Then
                    TransferenciaRecibida = DataSet.Tables("TransferenciasRecibidas").Rows(0)("Cantidad")
                End If
            End If

            'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura " & _
            '              "WHERE (Detalle_Facturas.Tipo_Factura = 'Transferencia Enviada') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Nuestra_Referencia =  '" & CodigoBodega & "') AND (Facturas.TransferenciaProcesada = 1)"
            'DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            'DataAdapter.Fill(DataSet, "TransferenciasRecibidas")
            'If DataSet.Tables("TransferenciasRecibidas").Rows.Count <> 0 Then
            '    If Not IsDBNull(DataSet.Tables("TransferenciasRecibidas").Rows(0)("Cantidad")) Then
            '        TransferenciaRecibida = DataSet.Tables("TransferenciasRecibidas").Rows(0)("Cantidad")
            '    End If
            'End If
        End If

        Existencia = Existencia + UnidadComprada - DevolucionCompra - UnidadFacturada - SalidaBodega + DevolucionFactura - TransferenciaEnviada + TransferenciaRecibida
        BuscaExistenciaBodega = Existencia
    End Function
    Public Function BuscaExistenciaBodegaLote(ByVal CodigoProducto As String, ByVal CodigoBodega As String, ByVal NumeroLote As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, Fecha As String, UnidadComprada As Double
        Dim SQlInventarioFisico As String, TasaCambio As Double, Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0, TransferenciaEnviada As Double = 0, TransferenciaRecibida As Double = 0
        Dim SalidaBodega As Double = 0, CostoVenta As Double = 0, ImporteFactura As Double = 0
        Dim ImporteCompra As Double, ImporteDevCompra As Double = 0, ImporteVenta As Double = 0, ImporteSalida As Double = 0
        Dim ImporteDevFactura As Double = 0

        '///////////////////////////FORMULA DE COMPRA PROMEDIO////////////////////////////////////////////////////////////////
        ' CostoPromedio= ((Existencia*Costo)+(PrecioCompra*CantidadCompra))/(Existencia+CantidadComprada)
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        '///////////////////////////BUSCO LA EXISTENCIA SEGUN EL ULTIMO INVENTARIO FISICO////////////////////////////////////////

        'CostoVenta = CostoPromedio(CodigoProducto)

        TasaCambio = 0

        '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) AS Importe  FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra  " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodigoBodega & "') AND (Detalle_Compras.Numero_Lote = '" & NumeroLote & "')  GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        If DataSet.Tables("Compras").Rows.Count <> 0 Then
            UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
            ImporteCompra = DataSet.Tables("Compras").Rows(0)("Importe")
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS DEVOLUCION DE COMPRAS////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT  SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodigoBodega & "') AND (Detalle_Compras.Numero_Lote = '" & NumeroLote & "') GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = N'Devolucion de Compra')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionCompras")
        If DataSet.Tables("DevolucionCompras").Rows.Count <> 0 Then
            DevolucionCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Cantidad")
            ImporteDevCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LAS FACTURAS//////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura " & _
                      "WHERE (Detalle_Facturas.Tipo_Factura = 'Factura') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega =  '" & CodigoBodega & "') AND (Detalle_Facturas.CodTarea = '" & NumeroLote & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Facturas")
        If DataSet.Tables("Facturas").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("Facturas").Rows(0)("Cantidad")) Then
                UnidadFacturada = DataSet.Tables("Facturas").Rows(0)("Cantidad")
                ImporteVenta = DataSet.Tables("Facturas").Rows(0)("Cantidad") * CostoVenta
            Else
                UnidadFacturada = 0
                ImporteVenta = 0
            End If
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA SALIDA DE BODEGA//////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura " & _
                      "WHERE (Detalle_Facturas.Tipo_Factura = 'Salida Bodega') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega =  '" & CodigoBodega & "') AND (Detalle_Facturas.CodTarea = '" & NumeroLote & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "SalidaBodega")
        If DataSet.Tables("SalidaBodega").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("SalidaBodega").Rows(0)("Cantidad")) Then
                SalidaBodega = DataSet.Tables("SalidaBodega").Rows(0)("Cantidad")
            Else
                SalidaBodega = 0
            End If
        End If

        DataSet.Reset()
        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT     SUM(Detalle_Facturas.Cantidad) AS Cantidad FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                      "WHERE  (Detalle_Facturas.Tipo_Factura = 'Devolucion de Venta') AND (Detalle_Facturas.Cod_Producto =  '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodigoBodega & "') AND (Detalle_Facturas.CodTarea = '" & NumeroLote & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")) Then
                DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
                ImporteDevFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad") * CostoVenta
            End If
        End If

        DataSet.Reset()
        '////////////////////////////////////BUSCO EL TOTAL DE TRANSFERENCIAS ENVIADAS  //////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura " & _
                      "WHERE (Detalle_Facturas.Tipo_Factura = 'Transferencia Enviada') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Su_Referencia = '" & CodigoBodega & "') AND (Facturas.TransferenciaProcesada = 1) AND (Detalle_Facturas.CodTarea = '" & NumeroLote & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")) Then
                TransferenciaEnviada = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            End If
        End If

        DataSet.Reset()
        '////////////////////////////////////BUSCO EL TOTAL DE TRANSFERENCIAS RECIBIDAS  //////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT     SUM(Detalle_Compras.Cantidad) AS Cantidad FROM Compras INNER JOIN Detalle_Compras ON Compras.Numero_Compra = Detalle_Compras.Numero_Compra AND Compras.Fecha_Compra = Detalle_Compras.Fecha_Compra AND Compras.Tipo_Compra = Detalle_Compras.Tipo_Compra " & _
                      "WHERE (Compras.TransferenciaProcesada = 1) AND (Compras.Cod_Bodega = '" & CodigoBodega & "') AND (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Tipo_Compra = 'Transferencia Recibida') AND (Detalle_Compras.Numero_Lote = '" & NumeroLote & "') "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "TransferenciasRecibidas")
        If DataSet.Tables("TransferenciasRecibidas").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("TransferenciasRecibidas").Rows(0)("Cantidad")) Then
                TransferenciaRecibida = DataSet.Tables("TransferenciasRecibidas").Rows(0)("Cantidad")
            End If
        End If

        'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura " & _
        '              "WHERE (Detalle_Facturas.Tipo_Factura = 'Transferencia Enviada') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Nuestra_Referencia =  '" & CodigoBodega & "') AND (Facturas.TransferenciaProcesada = 1)"
        'DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        'DataAdapter.Fill(DataSet, "TransferenciasRecibidas")
        'If DataSet.Tables("TransferenciasRecibidas").Rows.Count <> 0 Then
        '    If Not IsDBNull(DataSet.Tables("TransferenciasRecibidas").Rows(0)("Cantidad")) Then
        '        TransferenciaRecibida = DataSet.Tables("TransferenciasRecibidas").Rows(0)("Cantidad")
        '    End If
        'End If


        Existencia = UnidadComprada - DevolucionCompra - UnidadFacturada - SalidaBodega + DevolucionFactura - TransferenciaEnviada + TransferenciaRecibida
        BuscaExistenciaBodegaLote = Existencia
    End Function

    Public Function BuscaExistenciaLoteTotal(ByVal CodigoProducto As String, ByVal NombreLote As String, ByVal CodigoBodega As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, UnidadComprada As Double
        Dim Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0, TransferenciaEnviada As Double = 0, TransferenciaRecibida As Double = 0
        Dim SalidaBodega As Double = 0, CostoVenta As Double = 0, ImporteFactura As Double = 0
        Dim ImporteDevCompra As Double = 0, ImporteVenta As Double = 0, ImporteSalida As Double = 0
        Dim ImporteDevFactura As Double = 0


        If NombreLote = "SIN LOTE" Then
            SqlConsulta = "SELECT  MAX(Compras.Cod_Bodega) AS Cod_Bodega, SUM(Detalle_Compras.Cantidad) AS Cantidad, Detalle_Compras.Cod_Producto FROM Compras INNER JOIN Detalle_Compras ON Compras.Numero_Compra = Detalle_Compras.Numero_Compra AND Compras.Fecha_Compra = Detalle_Compras.Fecha_Compra AND Compras.Tipo_Compra = Detalle_Compras.Tipo_Compra " & _
                          "WHERE (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') AND (Detalle_Compras.Cod_Producto NOT IN (SELECT Codigo_Producto FROM Detalle_Lote  " & _
                          "WHERE (Tipo_Documento = 'Mercancia Recibida') AND (Codigo_Producto = '" & CodigoProducto & "') AND (NOT (Numero_Lote IS NULL)))) GROUP BY Detalle_Compras.Cod_Producto HAVING  (MAX(Compras.Cod_Bodega) = '" & CodigoBodega & "') AND (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "')"

            'SqlConsulta = "SELECT MAX(Compras.Cod_Bodega) AS Cod_Bodega, SUM(Detalle_Compras.Cantidad) AS Cantidad, Detalle_Compras.Cod_Producto FROM Compras INNER JOIN Detalle_Compras ON Compras.Numero_Compra = Detalle_Compras.Numero_Compra AND Compras.Fecha_Compra = Detalle_Compras.Fecha_Compra AND Compras.Tipo_Compra = Detalle_Compras.Tipo_Compra " & _
            '              "WHERE (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') AND (Detalle_Compras.Cod_Producto NOT IN (SELECT Codigo_Producto FROM(Detalle_Lote) " & _
            '              "WHERE (Tipo_Documento = 'Mercancia Recibida') AND (Codigo_Producto = N'15RCAOXI5250') AND (NOT (Numero_Lote IS NULL)))) GROUP BY Detalle_Compras.Cod_Producto HAVING (MAX(Compras.Cod_Bodega) = '01') AND (Detalle_Compras.Cod_Producto = N'01RERBOL50')"

            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "Compras")
            If DataSet.Tables("Compras").Rows.Count <> 0 Then
                UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
            End If

        Else
            '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS////////////////////////////////////////////////////////////////////
            'SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad, Numero_Lote, MIN(FechaVence) AS FechaVence, MAX(Tipo_Documento) AS Tipo_Documento, MAX(Fecha) AS Fecha, MAX(Codigo_Producto) AS Codigo_Producto FROM Detalle_Lote WHERE  (Tipo_Documento = 'Mercancia Recibida') AND (Codigo_Producto = '" & CodigoProducto & "') GROUP BY Numero_Lote "
            SqlConsulta = "SELECT SUM(Detalle_Lote.Cantidad) AS Cantidad, Detalle_Lote.Numero_Lote, MIN(Detalle_Lote.FechaVence) AS FechaVence, MAX(Detalle_Lote.Tipo_Documento) AS Tipo_Documento, MAX(Detalle_Lote.Fecha) AS Fecha, MAX(Detalle_Lote.Codigo_Producto) AS Codigo_Producto, MAX(Compras.Cod_Bodega) AS Cod_Bodega FROM Detalle_Lote INNER JOIN Compras ON Detalle_Lote.Fecha = Compras.Fecha_Compra AND Detalle_Lote.Tipo_Documento = Compras.Tipo_Compra AND Detalle_Lote.Numero_Documento = Compras.Numero_Compra  " & _
                          "WHERE (Detalle_Lote.Tipo_Documento = 'Mercancia Recibida') AND (Detalle_Lote.Codigo_Producto = '" & CodigoProducto & "') GROUP BY Detalle_Lote.Numero_Lote HAVING (Detalle_Lote.Numero_Lote = '" & NombreLote & "') AND (MAX(Compras.Cod_Bodega) = '" & CodigoBodega & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
            DataAdapter.Fill(DataSet, "Compras")
            If DataSet.Tables("Compras").Rows.Count <> 0 Then
                UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
            End If
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS DEVOLUCION DE COMPRAS////////////////////////////////////////////////////////////////////

        'SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad, Numero_Lote, MIN(FechaVence) AS FechaVence, MAX(Tipo_Documento) AS Tipo_Documento, MAX(Fecha) AS Fecha, MAX(Codigo_Producto) AS Codigo_Producto FROM Detalle_Lote WHERE  (Tipo_Documento = 'Devolucion de Compra') AND (Codigo_Producto = '" & CodigoProducto & "') GROUP BY Numero_Lote "
        SqlConsulta = "SELECT SUM(Detalle_Lote.Cantidad) AS Cantidad, Detalle_Lote.Numero_Lote, MIN(Detalle_Lote.FechaVence) AS FechaVence, MAX(Detalle_Lote.Tipo_Documento) AS Tipo_Documento, MAX(Detalle_Lote.Fecha) AS Fecha, MAX(Detalle_Lote.Codigo_Producto) AS Codigo_Producto, MAX(Compras.Cod_Bodega) AS Cod_Bodega FROM Detalle_Lote INNER JOIN Compras ON Detalle_Lote.Fecha = Compras.Fecha_Compra AND Detalle_Lote.Tipo_Documento = Compras.Tipo_Compra AND Detalle_Lote.Numero_Documento = Compras.Numero_Compra  " & _
                              "WHERE (Detalle_Lote.Tipo_Documento = 'Devolucion de Compra') AND (Detalle_Lote.Codigo_Producto = '" & CodigoProducto & "') GROUP BY Detalle_Lote.Numero_Lote HAVING (Detalle_Lote.Numero_Lote = '" & NombreLote & "') AND (MAX(Compras.Cod_Bodega) = '" & CodigoBodega & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionCompras")
        If DataSet.Tables("DevolucionCompras").Rows.Count <> 0 Then
            DevolucionCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Cantidad")
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LAS FACTURAS//////////////////////////////////////////////////////////////////////

        'SqlConsulta = "SELECT SUM(Detalle_Lote.Cantidad) AS Cantidad, Detalle_Lote.Numero_Lote, MIN(Detalle_Lote.FechaVence) AS FechaVence, MAX(Detalle_Lote.Tipo_Documento) AS Tipo_Documento, MAX(Detalle_Lote.Fecha) AS Fecha, MAX(Detalle_Lote.Codigo_Producto) AS Codigo_Producto, MAX(Facturas.Cod_Bodega) AS Cod_Bodega FROM Detalle_Lote INNER JOIN Facturas ON Detalle_Lote.Numero_Documento = Facturas.Numero_Factura AND Detalle_Lote.Fecha = Facturas.Fecha_Factura AND Detalle_Lote.Tipo_Documento = Facturas.Tipo_Factura  " & _
        '"WHERE (Detalle_Lote.Tipo_Documento = 'Factura') AND (Detalle_Lote.Codigo_Producto = '" & CodigoProducto & "') GROUP BY Detalle_Lote.Numero_Lote HAVING (MAX(Facturas.Cod_Bodega) = '" & CodigoBodega & "')"
        SqlConsulta = "SELECT SUM(Detalle_Lote.Cantidad) AS Cantidad, Detalle_Lote.Numero_Lote, MIN(Detalle_Lote.FechaVence) AS FechaVence, MAX(Detalle_Lote.Tipo_Documento) AS Tipo_Documento, MAX(Detalle_Lote.Fecha) AS Fecha, MAX(Detalle_Lote.Codigo_Producto) AS Codigo_Producto, MAX(Facturas.Cod_Bodega) AS Cod_Bodega FROM Detalle_Lote INNER JOIN Facturas ON Detalle_Lote.Numero_Documento = Facturas.Numero_Factura AND Detalle_Lote.Fecha = Facturas.Fecha_Factura AND Detalle_Lote.Tipo_Documento = Facturas.Tipo_Factura " & _
                      "WHERE (Detalle_Lote.Tipo_Documento = 'Factura') AND (Detalle_Lote.Codigo_Producto = '" & CodigoProducto & "') GROUP BY Detalle_Lote.Numero_Lote HAVING  (MAX(Facturas.Cod_Bodega) = '" & CodigoBodega & "') AND (Detalle_Lote.Numero_Lote = '" & NombreLote & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Facturas")
        If DataSet.Tables("Facturas").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("Facturas").Rows(0)("Cantidad")) Then
                UnidadFacturada = DataSet.Tables("Facturas").Rows(0)("Cantidad")
            Else
                UnidadFacturada = 0
                ImporteVenta = 0
            End If
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA SALIDA DE BODEGA//////////////////////////////////////////////////////////////////////

        'SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad, Numero_Lote, MIN(FechaVence) AS FechaVence, MAX(Tipo_Documento) AS Tipo_Documento, MAX(Fecha) AS Fecha, MAX(Codigo_Producto) AS Codigo_Producto FROM Detalle_Lote WHERE  (Tipo_Documento = 'Salida Bodega') AND (Codigo_Producto = '" & CodigoProducto & "') GROUP BY Numero_Lote "
        SqlConsulta = "SELECT SUM(Detalle_Lote.Cantidad) AS Cantidad, Detalle_Lote.Numero_Lote, MIN(Detalle_Lote.FechaVence) AS FechaVence, MAX(Detalle_Lote.Tipo_Documento) AS Tipo_Documento, MAX(Detalle_Lote.Fecha) AS Fecha, MAX(Detalle_Lote.Codigo_Producto) AS Codigo_Producto, MAX(Facturas.Cod_Bodega) AS Cod_Bodega FROM Detalle_Lote INNER JOIN Facturas ON Detalle_Lote.Numero_Documento = Facturas.Numero_Factura AND Detalle_Lote.Fecha = Facturas.Fecha_Factura AND Detalle_Lote.Tipo_Documento = Facturas.Tipo_Factura " & _
                      "WHERE (Detalle_Lote.Tipo_Documento = 'Salida Bodega') AND (Detalle_Lote.Codigo_Producto = '" & CodigoProducto & "') GROUP BY Detalle_Lote.Numero_Lote HAVING  (MAX(Facturas.Cod_Bodega) = '" & CodigoBodega & "') AND (Detalle_Lote.Numero_Lote = '" & NombreLote & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "SalidaBodega")
        If DataSet.Tables("SalidaBodega").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("SalidaBodega").Rows(0)("Cantidad")) Then
                SalidaBodega = DataSet.Tables("SalidaBodega").Rows(0)("Cantidad")
            Else
                SalidaBodega = 0
            End If
        End If

        DataSet.Reset()
        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad, Numero_Lote, MIN(FechaVence) AS FechaVence, MAX(Tipo_Documento) AS Tipo_Documento, MAX(Fecha) AS Fecha, MAX(Codigo_Producto) AS Codigo_Producto FROM Detalle_Lote WHERE  (Tipo_Documento = 'Devolucion de Venta') AND (Codigo_Producto = '" & CodigoProducto & "') GROUP BY Numero_Lote "
        SqlConsulta = "SELECT SUM(Detalle_Lote.Cantidad) AS Cantidad, Detalle_Lote.Numero_Lote, MIN(Detalle_Lote.FechaVence) AS FechaVence, MAX(Detalle_Lote.Tipo_Documento) AS Tipo_Documento, MAX(Detalle_Lote.Fecha) AS Fecha, MAX(Detalle_Lote.Codigo_Producto) AS Codigo_Producto, MAX(Facturas.Cod_Bodega) AS Cod_Bodega FROM Detalle_Lote INNER JOIN Facturas ON Detalle_Lote.Numero_Documento = Facturas.Numero_Factura AND Detalle_Lote.Fecha = Facturas.Fecha_Factura AND Detalle_Lote.Tipo_Documento = Facturas.Tipo_Factura " & _
                       "WHERE (Detalle_Lote.Tipo_Documento = 'Devolucion de Venta') AND (Detalle_Lote.Codigo_Producto = '" & CodigoProducto & "') GROUP BY Detalle_Lote.Numero_Lote HAVING  (MAX(Facturas.Cod_Bodega) = '" & CodigoBodega & "') AND (Detalle_Lote.Numero_Lote = '" & NombreLote & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")) Then
                DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            End If
        End If

        DataSet.Reset()
        '////////////////////////////////////BUSCO EL TOTAL DE TRANSFERENCIAS ENVIADAS  //////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad, Numero_Lote, MIN(FechaVence) AS FechaVence, MAX(Tipo_Documento) AS Tipo_Documento, MAX(Fecha) AS Fecha, MAX(Codigo_Producto) AS Codigo_Producto FROM Detalle_Lote WHERE  (Tipo_Documento = 'Transferencia Enviada') AND (Codigo_Producto = '" & CodigoProducto & "') GROUP BY Numero_Lote "
        SqlConsulta = "SELECT SUM(Detalle_Lote.Cantidad) AS Cantidad, Detalle_Lote.Numero_Lote, MIN(Detalle_Lote.FechaVence) AS FechaVence, MAX(Detalle_Lote.Tipo_Documento) AS Tipo_Documento, MAX(Detalle_Lote.Fecha) AS Fecha, MAX(Detalle_Lote.Codigo_Producto) AS Codigo_Producto, MAX(Facturas.Cod_Bodega) AS Cod_Bodega FROM Detalle_Lote INNER JOIN Facturas ON Detalle_Lote.Numero_Documento = Facturas.Numero_Factura AND Detalle_Lote.Fecha = Facturas.Fecha_Factura AND Detalle_Lote.Tipo_Documento = Facturas.Tipo_Factura " & _
                       "WHERE (Detalle_Lote.Tipo_Documento = 'Transferencia Enviada') AND (Detalle_Lote.Codigo_Producto = '" & CodigoProducto & "') GROUP BY Detalle_Lote.Numero_Lote HAVING  (MAX(Facturas.Cod_Bodega) = '" & CodigoBodega & "') AND (Detalle_Lote.Numero_Lote = '" & NombreLote & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")) Then
                TransferenciaEnviada = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            End If
        End If

        DataSet.Reset()
        '////////////////////////////////////BUSCO EL TOTAL DE TRANSFERENCIAS RECIBIDAS  //////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad, Numero_Lote, MIN(FechaVence) AS FechaVence, MAX(Tipo_Documento) AS Tipo_Documento, MAX(Fecha) AS Fecha, MAX(Codigo_Producto) AS Codigo_Producto FROM Detalle_Lote WHERE  (Tipo_Documento = 'Transferencia Recibida') AND (Codigo_Producto = '" & CodigoProducto & "') GROUP BY Numero_Lote "
        SqlConsulta = "SELECT SUM(Detalle_Lote.Cantidad) AS Cantidad, Detalle_Lote.Numero_Lote, MIN(Detalle_Lote.FechaVence) AS FechaVence, MAX(Detalle_Lote.Tipo_Documento) AS Tipo_Documento, MAX(Detalle_Lote.Fecha) AS Fecha, MAX(Detalle_Lote.Codigo_Producto) AS Codigo_Producto, MAX(Compras.Cod_Bodega) AS Cod_Bodega FROM Detalle_Lote INNER JOIN Compras ON Detalle_Lote.Fecha = Compras.Fecha_Compra AND Detalle_Lote.Tipo_Documento = Compras.Tipo_Compra AND Detalle_Lote.Numero_Documento = Compras.Numero_Compra  " & _
                               "WHERE (Detalle_Lote.Tipo_Documento = 'Transferencia Recibida') AND (Detalle_Lote.Codigo_Producto = '" & CodigoProducto & "') GROUP BY Detalle_Lote.Numero_Lote HAVING (Detalle_Lote.Numero_Lote = '" & NombreLote & "') AND (MAX(Compras.Cod_Bodega) = '" & CodigoBodega & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "TransferenciasRecibidas")
        If DataSet.Tables("TransferenciasRecibidas").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("TransferenciasRecibidas").Rows(0)("Cantidad")) Then
                TransferenciaRecibida = DataSet.Tables("TransferenciasRecibidas").Rows(0)("Cantidad")
            End If
        End If



        Existencia = Existencia + UnidadComprada - DevolucionCompra - UnidadFacturada - SalidaBodega + DevolucionFactura - TransferenciaEnviada + TransferenciaRecibida
        BuscaExistenciaLoteTotal = Existencia
    End Function

    Public Function BuscaExistenciaLote(ByVal CodigoProducto As String, ByVal CodigoBodega As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, UnidadComprada As Double
        Dim Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0, TransferenciaEnviada As Double = 0, TransferenciaRecibida As Double = 0
        Dim SalidaBodega As Double = 0, CostoVenta As Double = 0, ImporteFactura As Double = 0
        Dim ImporteDevCompra As Double = 0, ImporteVenta As Double = 0, ImporteSalida As Double = 0
        Dim ImporteDevFactura As Double = 0


        '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad, Numero_Lote, MIN(FechaVence) AS FechaVence, MAX(Tipo_Documento) AS Tipo_Documento, MAX(Fecha) AS Fecha, MAX(Codigo_Producto) AS Codigo_Producto FROM Detalle_Lote WHERE  (Tipo_Documento = 'Mercancia Recibida') AND (Codigo_Producto = '" & CodigoProducto & "') GROUP BY Numero_Lote "
        SqlConsulta = "SELECT SUM(Detalle_Lote.Cantidad) AS Cantidad, Detalle_Lote.Numero_Lote, MIN(Detalle_Lote.FechaVence) AS FechaVence, MAX(Detalle_Lote.Tipo_Documento) AS Tipo_Documento, MAX(Detalle_Lote.Fecha) AS Fecha, MAX(Detalle_Lote.Codigo_Producto) AS Codigo_Producto, MAX(Compras.Cod_Bodega) AS Cod_Bodega FROM Detalle_Lote INNER JOIN Compras ON Detalle_Lote.Fecha = Compras.Fecha_Compra AND Detalle_Lote.Tipo_Documento = Compras.Tipo_Compra AND Detalle_Lote.Numero_Documento = Compras.Numero_Compra  " & _
                      "WHERE (Detalle_Lote.Tipo_Documento = 'Mercancia Recibida') AND (Detalle_Lote.Codigo_Producto = '" & CodigoProducto & "') GROUP BY Detalle_Lote.Numero_Lote HAVING (Detalle_Lote.Numero_Lote <> 'SIN LOTE') AND (MAX(Compras.Cod_Bodega) = '" & CodigoBodega & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        If DataSet.Tables("Compras").Rows.Count <> 0 Then
            UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS DEVOLUCION DE COMPRAS////////////////////////////////////////////////////////////////////

        'SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad, Numero_Lote, MIN(FechaVence) AS FechaVence, MAX(Tipo_Documento) AS Tipo_Documento, MAX(Fecha) AS Fecha, MAX(Codigo_Producto) AS Codigo_Producto FROM Detalle_Lote WHERE  (Tipo_Documento = 'Devolucion de Compra') AND (Codigo_Producto = '" & CodigoProducto & "') GROUP BY Numero_Lote "
        SqlConsulta = "SELECT SUM(Detalle_Lote.Cantidad) AS Cantidad, Detalle_Lote.Numero_Lote, MIN(Detalle_Lote.FechaVence) AS FechaVence, MAX(Detalle_Lote.Tipo_Documento) AS Tipo_Documento, MAX(Detalle_Lote.Fecha) AS Fecha, MAX(Detalle_Lote.Codigo_Producto) AS Codigo_Producto, MAX(Compras.Cod_Bodega) AS Cod_Bodega FROM Detalle_Lote INNER JOIN Compras ON Detalle_Lote.Fecha = Compras.Fecha_Compra AND Detalle_Lote.Tipo_Documento = Compras.Tipo_Compra AND Detalle_Lote.Numero_Documento = Compras.Numero_Compra  " & _
                              "WHERE (Detalle_Lote.Tipo_Documento = 'Devolucion de Compra') AND (Detalle_Lote.Codigo_Producto = '" & CodigoProducto & "') GROUP BY Detalle_Lote.Numero_Lote HAVING (Detalle_Lote.Numero_Lote <> 'SIN LOTE') AND (MAX(Compras.Cod_Bodega) = '" & CodigoBodega & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionCompras")
        If DataSet.Tables("DevolucionCompras").Rows.Count <> 0 Then
            DevolucionCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Cantidad")
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LAS FACTURAS//////////////////////////////////////////////////////////////////////

        'SqlConsulta = "SELECT SUM(Detalle_Lote.Cantidad) AS Cantidad, Detalle_Lote.Numero_Lote, MIN(Detalle_Lote.FechaVence) AS FechaVence, MAX(Detalle_Lote.Tipo_Documento) AS Tipo_Documento, MAX(Detalle_Lote.Fecha) AS Fecha, MAX(Detalle_Lote.Codigo_Producto) AS Codigo_Producto, MAX(Facturas.Cod_Bodega) AS Cod_Bodega FROM Detalle_Lote INNER JOIN Facturas ON Detalle_Lote.Numero_Documento = Facturas.Numero_Factura AND Detalle_Lote.Fecha = Facturas.Fecha_Factura AND Detalle_Lote.Tipo_Documento = Facturas.Tipo_Factura  " & _
        '"WHERE (Detalle_Lote.Tipo_Documento = 'Factura') AND (Detalle_Lote.Codigo_Producto = '" & CodigoProducto & "') GROUP BY Detalle_Lote.Numero_Lote HAVING (MAX(Facturas.Cod_Bodega) = '" & CodigoBodega & "')"
        SqlConsulta = "SELECT SUM(Detalle_Lote.Cantidad) AS Cantidad, Detalle_Lote.Numero_Lote, MIN(Detalle_Lote.FechaVence) AS FechaVence, MAX(Detalle_Lote.Tipo_Documento) AS Tipo_Documento, MAX(Detalle_Lote.Fecha) AS Fecha, MAX(Detalle_Lote.Codigo_Producto) AS Codigo_Producto, MAX(Facturas.Cod_Bodega) AS Cod_Bodega FROM Detalle_Lote INNER JOIN Facturas ON Detalle_Lote.Numero_Documento = Facturas.Numero_Factura AND Detalle_Lote.Fecha = Facturas.Fecha_Factura AND Detalle_Lote.Tipo_Documento = Facturas.Tipo_Factura " & _
                      "WHERE (Detalle_Lote.Tipo_Documento = 'Factura') AND (Detalle_Lote.Codigo_Producto = '" & CodigoProducto & "') GROUP BY Detalle_Lote.Numero_Lote HAVING  (MAX(Facturas.Cod_Bodega) = '" & CodigoBodega & "') AND (Detalle_Lote.Numero_Lote <> 'SIN LOTE')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Facturas")
        If DataSet.Tables("Facturas").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("Facturas").Rows(0)("Cantidad")) Then
                UnidadFacturada = DataSet.Tables("Facturas").Rows(0)("Cantidad")
            Else
                UnidadFacturada = 0
                ImporteVenta = 0
            End If
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA SALIDA DE BODEGA//////////////////////////////////////////////////////////////////////

        'SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad, Numero_Lote, MIN(FechaVence) AS FechaVence, MAX(Tipo_Documento) AS Tipo_Documento, MAX(Fecha) AS Fecha, MAX(Codigo_Producto) AS Codigo_Producto FROM Detalle_Lote WHERE  (Tipo_Documento = 'Salida Bodega') AND (Codigo_Producto = '" & CodigoProducto & "') GROUP BY Numero_Lote "
        SqlConsulta = "SELECT SUM(Detalle_Lote.Cantidad) AS Cantidad, Detalle_Lote.Numero_Lote, MIN(Detalle_Lote.FechaVence) AS FechaVence, MAX(Detalle_Lote.Tipo_Documento) AS Tipo_Documento, MAX(Detalle_Lote.Fecha) AS Fecha, MAX(Detalle_Lote.Codigo_Producto) AS Codigo_Producto, MAX(Facturas.Cod_Bodega) AS Cod_Bodega FROM Detalle_Lote INNER JOIN Facturas ON Detalle_Lote.Numero_Documento = Facturas.Numero_Factura AND Detalle_Lote.Fecha = Facturas.Fecha_Factura AND Detalle_Lote.Tipo_Documento = Facturas.Tipo_Factura " & _
                      "WHERE (Detalle_Lote.Tipo_Documento = 'Salida Bodega') AND (Detalle_Lote.Codigo_Producto = '" & CodigoProducto & "') GROUP BY Detalle_Lote.Numero_Lote HAVING  (MAX(Facturas.Cod_Bodega) = '" & CodigoBodega & "') AND (Detalle_Lote.Numero_Lote = 'SIN LOTE')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "SalidaBodega")
        If DataSet.Tables("SalidaBodega").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("SalidaBodega").Rows(0)("Cantidad")) Then
                SalidaBodega = DataSet.Tables("SalidaBodega").Rows(0)("Cantidad")
            Else
                SalidaBodega = 0
            End If
        End If

        DataSet.Reset()
        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad, Numero_Lote, MIN(FechaVence) AS FechaVence, MAX(Tipo_Documento) AS Tipo_Documento, MAX(Fecha) AS Fecha, MAX(Codigo_Producto) AS Codigo_Producto FROM Detalle_Lote WHERE  (Tipo_Documento = 'Devolucion de Venta') AND (Codigo_Producto = '" & CodigoProducto & "') GROUP BY Numero_Lote "
        SqlConsulta = "SELECT SUM(Detalle_Lote.Cantidad) AS Cantidad, Detalle_Lote.Numero_Lote, MIN(Detalle_Lote.FechaVence) AS FechaVence, MAX(Detalle_Lote.Tipo_Documento) AS Tipo_Documento, MAX(Detalle_Lote.Fecha) AS Fecha, MAX(Detalle_Lote.Codigo_Producto) AS Codigo_Producto, MAX(Facturas.Cod_Bodega) AS Cod_Bodega FROM Detalle_Lote INNER JOIN Facturas ON Detalle_Lote.Numero_Documento = Facturas.Numero_Factura AND Detalle_Lote.Fecha = Facturas.Fecha_Factura AND Detalle_Lote.Tipo_Documento = Facturas.Tipo_Factura " & _
                       "WHERE (Detalle_Lote.Tipo_Documento = 'Devolucion de Venta') AND (Detalle_Lote.Codigo_Producto = '" & CodigoProducto & "') GROUP BY Detalle_Lote.Numero_Lote HAVING  (MAX(Facturas.Cod_Bodega) = '" & CodigoBodega & "') AND (Detalle_Lote.Numero_Lote = 'SIN LOTE')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")) Then
                DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            End If
        End If

        DataSet.Reset()
        '////////////////////////////////////BUSCO EL TOTAL DE TRANSFERENCIAS ENVIADAS  //////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad, Numero_Lote, MIN(FechaVence) AS FechaVence, MAX(Tipo_Documento) AS Tipo_Documento, MAX(Fecha) AS Fecha, MAX(Codigo_Producto) AS Codigo_Producto FROM Detalle_Lote WHERE  (Tipo_Documento = 'Transferencia Enviada') AND (Codigo_Producto = '" & CodigoProducto & "') GROUP BY Numero_Lote "
        SqlConsulta = "SELECT SUM(Detalle_Lote.Cantidad) AS Cantidad, Detalle_Lote.Numero_Lote, MIN(Detalle_Lote.FechaVence) AS FechaVence, MAX(Detalle_Lote.Tipo_Documento) AS Tipo_Documento, MAX(Detalle_Lote.Fecha) AS Fecha, MAX(Detalle_Lote.Codigo_Producto) AS Codigo_Producto, MAX(Facturas.Cod_Bodega) AS Cod_Bodega FROM Detalle_Lote INNER JOIN Facturas ON Detalle_Lote.Numero_Documento = Facturas.Numero_Factura AND Detalle_Lote.Fecha = Facturas.Fecha_Factura AND Detalle_Lote.Tipo_Documento = Facturas.Tipo_Factura " & _
                       "WHERE (Detalle_Lote.Tipo_Documento = 'Transferencia Enviada') AND (Detalle_Lote.Codigo_Producto = '" & CodigoProducto & "') GROUP BY Detalle_Lote.Numero_Lote HAVING  (MAX(Facturas.Cod_Bodega) = '" & CodigoBodega & "') AND (Detalle_Lote.Numero_Lote = 'SIN LOTE')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")) Then
                TransferenciaEnviada = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            End If
        End If

        DataSet.Reset()
        '////////////////////////////////////BUSCO EL TOTAL DE TRANSFERENCIAS RECIBIDAS  //////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Cantidad) AS Cantidad, Numero_Lote, MIN(FechaVence) AS FechaVence, MAX(Tipo_Documento) AS Tipo_Documento, MAX(Fecha) AS Fecha, MAX(Codigo_Producto) AS Codigo_Producto FROM Detalle_Lote WHERE  (Tipo_Documento = 'Transferencia Recibida') AND (Codigo_Producto = '" & CodigoProducto & "') GROUP BY Numero_Lote "
        SqlConsulta = "SELECT SUM(Detalle_Lote.Cantidad) AS Cantidad, Detalle_Lote.Numero_Lote, MIN(Detalle_Lote.FechaVence) AS FechaVence, MAX(Detalle_Lote.Tipo_Documento) AS Tipo_Documento, MAX(Detalle_Lote.Fecha) AS Fecha, MAX(Detalle_Lote.Codigo_Producto) AS Codigo_Producto, MAX(Compras.Cod_Bodega) AS Cod_Bodega FROM Detalle_Lote INNER JOIN Compras ON Detalle_Lote.Fecha = Compras.Fecha_Compra AND Detalle_Lote.Tipo_Documento = Compras.Tipo_Compra AND Detalle_Lote.Numero_Documento = Compras.Numero_Compra  " & _
                               "WHERE (Detalle_Lote.Tipo_Documento = 'Transferencia Recibida') AND (Detalle_Lote.Codigo_Producto = '" & CodigoProducto & "') GROUP BY Detalle_Lote.Numero_Lote HAVING (Detalle_Lote.Numero_Lote = 'SIN LOTE') AND (MAX(Compras.Cod_Bodega) = '" & CodigoBodega & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "TransferenciasRecibidas")
        If DataSet.Tables("TransferenciasRecibidas").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("TransferenciasRecibidas").Rows(0)("Cantidad")) Then
                TransferenciaRecibida = DataSet.Tables("TransferenciasRecibidas").Rows(0)("Cantidad")
            End If
        End If



        Existencia = Existencia + UnidadComprada - DevolucionCompra - UnidadFacturada - SalidaBodega + DevolucionFactura - TransferenciaEnviada + TransferenciaRecibida
        BuscaExistenciaLote = Existencia
    End Function





    Public Function BuscaCompraAcumulada(ByVal CodigoProducto As String, ByVal FechaCompraIni As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, UnidadComprada As Double
        Dim Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0


        ImporteCompra = 0
        UnidadComprada = 0
        '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra WHERE (Detalle_Compras.Fecha_Compra >= CONVERT(DATETIME, '" & FechaCompraFin & "', 102)) AND (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') GROUP BY Detalle_Compras.Tipo_Compra, Compras.Cod_Bodega HAVING (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') OR (Detalle_Compras.Tipo_Compra = N'Transferencia Recibida')"
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(Detalle_Compras.Importe) AS Importe FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & FechaCompraIni & "', 102)) GROUP BY Detalle_Compras.Tipo_Compra, Compras.Cod_Bodega HAVING (Detalle_Compras.Tipo_Compra = N'Mercancia Recibida') OR (Detalle_Compras.Tipo_Compra = N'Transferencia Recibida') "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        If DataSet.Tables("Compras").Rows.Count <> 0 Then
            UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
            ImporteCompra = DataSet.Tables("Compras").Rows(0)("Importe")
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT  SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Importe) AS Importe, Facturas.Tipo_Factura FROM  Detalle_Facturas INNER JOIN  Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND  Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                      "WHERE  (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & FechaCompraIni & "', 102)) GROUP BY Facturas.Tipo_Factura, Detalle_Facturas.Cod_Producto HAVING  (Facturas.Tipo_Factura = N'Devolucion de Venta') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            ImporteVenta = ImporteVenta - DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
        End If


        BuscaCompraAcumulada = UnidadComprada + DevolucionFactura


    End Function
    Public Function BuscaCompraBodega(ByVal CodigoProducto As String, ByVal FechaCompraIni As Date, ByVal FechaCompraFin As Date, ByVal CodBodega As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, UnidadComprada As Double
        Dim Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0, UnidadTransferencia As Double, ImporteTransferencia As Double, ImporteTransferenciaD As Double
        Dim CostoPromedio As Double = 0, ImporteCompraD As Double = 0, ImporteVentaD As Double = 0


        'CostoPromedio = CostoPromedioKardexBodega(CodigoProducto, FechaCompraFin, CodBodega)

        ImporteCompra = 0
        ImporteCompraD = 0
        UnidadComprada = 0
        MontoEntrada = 0
        MontoEntradaD = 0
        ImporteTransferencia = 0
        ImporteTransferenciaD = 0
        ImporteVenta = 0
        ImporteVentaD = 0
        '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS CORDOBAS////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad ) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad / TasaCambio.MontoTasa) AS ImporteD,Compras.MonedaCompra AS Moneda FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra BETWEEN CONVERT(DATETIME, '" & Format(FechaCompraIni, "yyyy-MM-dd") & "', 102) AND CONVERT(DATETIME, '" & Format(FechaCompraFin, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') GROUP BY Compras.MonedaCompra HAVING (Compras.MonedaCompra = N'Cordobas')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        If DataSet.Tables("Compras").Rows.Count <> 0 Then
            UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
            ImporteCompra = DataSet.Tables("Compras").Rows(0)("Importe")
            ImporteCompraD = DataSet.Tables("Compras").Rows(0)("ImporteD")
        End If

        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Neto * TasaCambio.MontoTasa) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad * TasaCambio.MontoTasa) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS ImporteD,Compras.MonedaCompra AS Moneda FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra BETWEEN CONVERT(DATETIME,'" & Format(FechaCompraIni, "yyyy-MM-dd") & "', 102) AND CONVERT(DATETIME, '" & Format(FechaCompraFin, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') GROUP BY Compras.MonedaCompra HAVING (Compras.MonedaCompra = 'Dolares')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "ComprasD")
        If DataSet.Tables("ComprasD").Rows.Count <> 0 Then
            UnidadComprada = DataSet.Tables("ComprasD").Rows(0)("Cantidad") + UnidadComprada
            ImporteCompra = DataSet.Tables("ComprasD").Rows(0)("Importe") + ImporteCompra
            ImporteCompraD = DataSet.Tables("ComprasD").Rows(0)("ImporteD") + ImporteCompraD
        End If

        UnidadTransferencia = 0
        ImporteTransferencia = 0
        '//////////////////////////////////BUSCO EL TOTAL DE LAS TRANSFERENCIAS RECIBIDAS////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario) AS Importe, SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario / TasaCambio.MontoTasa) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra BETWEEN CONVERT(DATETIME, '" & Format(FechaCompraIni, "yyyy-MM-dd") & "', 102) AND CONVERT(DATETIME,'" & Format(FechaCompraFin, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega = '" & CodBodega & "') GROUP BY Detalle_Compras.Tipo_Compra HAVING (Detalle_Compras.Tipo_Compra = 'Transferencia Recibida') OR (Detalle_Compras.Tipo_Compra = 'Transferencia Recibida') "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "TransferenciaRecibida")
        If DataSet.Tables("TransferenciaRecibida").Rows.Count <> 0 Then
            UnidadTransferencia = DataSet.Tables("TransferenciaRecibida").Rows(0)("Cantidad")
            ImporteTransferencia = DataSet.Tables("TransferenciaRecibida").Rows(0)("Importe")   'DataSet.Tables("TransferenciaRecibida").Rows(0)("Importe")
            ImporteTransferenciaD = DataSet.Tables("TransferenciaRecibida").Rows(0)("ImporteD")
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Precio_Neto) AS Precio_Neto, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) AS Importe, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa  " & _
                      "WHERE (Facturas.Fecha_Factura BETWEEN CONVERT(DATETIME, '" & Format(FechaCompraIni, "yyyy-MM-dd") & "', 102) AND CONVERT(DATETIME, '" & Format(FechaCompraFin, "yyyy-MM-dd") & "', 102)) AND (Facturas.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') GROUP BY Facturas.Tipo_Factura, TasaCambio.MontoTasa HAVING (Facturas.Tipo_Factura = N'Devolucion de Venta')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            ImporteVenta = DataSet.Tables("DevolucionFacturas").Rows(0)("Importe") '* CostoPromedio DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
            ImporteVentaD = DataSet.Tables("DevolucionFacturas").Rows(0)("ImporteD") '* CostoPromedioDolar
        End If


        BuscaCompraBodega = UnidadComprada + DevolucionFactura + UnidadTransferencia
        MontoEntrada = ImporteCompra + ImporteTransferencia + ImporteVenta
        MontoEntradaD = ImporteCompraD + ImporteTransferenciaD + ImporteVentaD

    End Function
    Public Function BuscaInventarioInicialBodegaMov(ByVal CodigoProducto As String, ByVal FechaIni As String, ByVal FechaFin As String, ByVal CodBodega As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), FechaIni2 As Date
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, UnidadComprada As Double
        Dim Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0, UnidadVendida As Double, ImporteVenta As Double, ImporteDevCompra As Double, ImporteDevVenta As Double
        Dim SalidaBodega As Double = 0, ImporteSalida As Double = 0, CostoPromedio As Double, CostoPromedioD As Double
        Dim ImporteCompraD As Double = 0, ImporteDevVentaD As Double = 0, ImporteVentaD As Double = 0, ImporteSalidaD As Double = 0, ImporteDevCompraD As Double

        FechaIni2 = FechaIni

        CostoPromedio = CostoPromedioKardexBodega(CodigoProducto, FechaIni2.AddDays(-1), CodBodega)
        CostoPromedioD = CostoPromedioDolar



        ImporteCompra = 0
        UnidadComprada = 0
        MontoInicial = 0
        ImporteDevVenta = 0
        ImporteVenta = 0
        ImporteSalida = 0
        ImporteDevCompra = 0
        ImporteCompraD = 0

        UnidadComprada = BuscaInventarioInicialBodega(CodigoProducto, FechaIni, CodBodega)
        ImporteCompra = MontoInicial

        '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS CORDOBAS////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT  SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad*Detalle_Compras.Precio_Neto) AS Importe FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra  " & _
        '              "WHERE (Detalle_Compras.Cod_Producto ='" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & FechaIni & "', 102)) AND (Compras.Cod_Bodega = '" & CodBodega & "') HAVING (MAX(Detalle_Compras.Tipo_Compra) = N'Mercancia Recibida') OR (MAX(Detalle_Compras.Tipo_Compra) = N'Transferencia Recibida') "
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) AS Importe, SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS ImporteD, Compras.MonedaCompra FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                      "WHERE (Detalle_Compras.Fecha_Compra BETWEEN CONVERT(DATETIME, '" & FechaIni & "', 102) AND CONVERT(DATETIME, '" & FechaFin & "', 102)) AND (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodBodega & "') GROUP BY Compras.MonedaCompra HAVING (MAX(Detalle_Compras.Tipo_Compra) = 'Mercancia Recibida') AND (Compras.MonedaCompra <> 'Dolares') OR (MAX(Detalle_Compras.Tipo_Compra) = 'Transferencia Recibida') AND (Compras.MonedaCompra <> 'Dolares')"

        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        If DataSet.Tables("Compras").Rows.Count <> 0 Then
            UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad") + UnidadComprada
            ImporteCompra = DataSet.Tables("Compras").Rows(0)("Importe") + ImporteCompra
            ImporteCompraD = DataSet.Tables("Compras").Rows(0)("ImporteD") + ImporteCompraD
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS TRANSFERENCIAS RECIBIDAS////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario * TasaCambio.MontoTasa) AS Importe,  SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario) AS ImporteD,Compras.MonedaCompra FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                      "WHERE (Detalle_Compras.Fecha_Compra BETWEEN CONVERT(DATETIME, '" & FechaIni & "', 102) AND CONVERT(DATETIME, '" & FechaFin & "', 102)) AND (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodBodega & "') GROUP BY Compras.MonedaCompra HAVING (MAX(Detalle_Compras.Tipo_Compra) = 'Mercancia Recibida') AND (Compras.MonedaCompra = 'Dolares') OR (MAX(Detalle_Compras.Tipo_Compra) = 'Transferencia Recibida') AND (Compras.MonedaCompra = N'Dolares') "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "ComprasD")
        If DataSet.Tables("ComprasD").Rows.Count <> 0 Then
            UnidadComprada = DataSet.Tables("ComprasD").Rows(0)("Cantidad") + UnidadComprada
            ImporteCompra = DataSet.Tables("ComprasD").Rows(0)("Importe") + ImporteCompra
            ImporteCompraD = DataSet.Tables("ComprasD").Rows(0)("ImporteD") + ImporteCompraD
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT  SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Importe) AS Importe, Facturas.Tipo_Factura FROM  Detalle_Facturas INNER JOIN  Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND  Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
        '              "WHERE  (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & FechaIni & "', 102)) GROUP BY Facturas.Tipo_Factura, Detalle_Facturas.Cod_Producto HAVING  (Facturas.Tipo_Factura = N'Devolucion de Venta') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "')"
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Neto) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                      "WHERE (Facturas.Fecha_Factura BETWEEN CONVERT(DATETIME, '" & FechaIni & "', 102) AND CONVERT(DATETIME, '" & FechaFin & "', 102)) AND (Facturas.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Devolucion de Venta')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            ImporteDevVenta = ImporteVenta - (DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad") * CostoPromedio) 'DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
            ImporteDevVentaD = ImporteVentaD - (DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad") * CostoPromedioDolar)
        End If


        UnidadVendida = 0

        '//////////////////////////////////BUSCO EL TOTAL DE LAS VENTAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Neto) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                      "WHERE (Facturas.Fecha_Factura BETWEEN CONVERT(DATETIME, '" & FechaIni & "', 102) AND CONVERT(DATETIME, '" & FechaFin & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodBodega & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = N'Factura') OR (Facturas.Tipo_Factura = N'Transferencia Enviada')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Ventas")
        If DataSet.Tables("Ventas").Rows.Count <> 0 Then
            UnidadVendida = DataSet.Tables("Ventas").Rows(0)("Cantidad")
            ImporteVenta = DataSet.Tables("Ventas").Rows(0)("Cantidad") * CostoPromedio 'DataSet.Tables("Ventas").Rows(0)("Importe")
            ImporteVentaD = DataSet.Tables("Ventas").Rows(0)("Cantidad") * CostoPromedioDolar
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS SALIDAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                      "WHERE (Facturas.Fecha_Factura BETWEEN CONVERT(DATETIME, '" & FechaIni & "', 102) AND CONVERT(DATETIME, '" & FechaFin & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodBodega & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Salida Bodega') "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Salida")
        If DataSet.Tables("Salida").Rows.Count <> 0 Then
            SalidaBodega = DataSet.Tables("Salida").Rows(0)("Cantidad")
            ImporteSalida = DataSet.Tables("Salida").Rows(0)("Cantidad") * CostoPromedio ' DataSet.Tables("Salida").Rows(0)("Importe")
            ImporteSalidaD = DataSet.Tables("Salida").Rows(0)("Cantidad") * CostoPromedioDolar
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  COMPRAS//////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Importe) AS Importe FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & FechaIni & "', 102)) GROUP BY Detalle_Compras.Tipo_Compra, Compras.Cod_Bodega HAVING (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra') "
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad*Detalle_Compras.Precio_Neto) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra  " & _
                      "WHERE (Detalle_Compras.Fecha_Compra BETWEEN CONVERT(DATETIME, '" & FechaIni & "', 102) AND CONVERT(DATETIME, '" & FechaFin & "', 102)) AND (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Compras.Cod_Bodega = '" & CodBodega & "') GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionCompras")
        If DataSet.Tables("DevolucionCompras").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionCompras").Rows(0)("Cantidad")
            ImporteDevCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Cantidad") * CostoPromedio 'DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
            ImporteDevCompraD = DataSet.Tables("DevolucionCompras").Rows(0)("Cantidad") * CostoPromedioDolar
        End If


        BuscaInventarioInicialBodegaMov = UnidadComprada + DevolucionFactura - UnidadVendida - SalidaBodega
        MontoInicial = ImporteCompra + ImporteDevVenta - ImporteSalida - ImporteDevCompra - ImporteVenta

    End Function

    Public Function ConsultaInventarioInicialBodega(ByVal CodigoProducto As String, ByVal FechaIni As String, ByVal CodBodega As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), FechaIni2 As Date
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, UnidadComprada As Double
        Dim Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0, UnidadVendida As Double, ImporteVenta As Double, ImporteDevCompra As Double, ImporteDevVenta As Double
        Dim SalidaBodega As Double = 0, ImporteSalida As Double = 0, CostoPromedio As Double = 0, CostoPromedioD As Double = 0, TransferenciaRecibida As Double, TransferenciaEnviada As Double
        Dim ImporteCompraD As Double = 0, ImporteDevVentaD As Double = 0, ImporteVentaD As Double = 0, ImporteSalidaD As Double = 0, ImporteDevCompraD As Double
        Dim ImporteTransEnviada As Double, ImporteTransRecibida As Double = 0, UnidadTransEnviada As Double, UnidadTransRecibida As Double = 0, ImporteTransEnviadaD As Double = 0, ImporteTransRecibidaD As Double = 0

        FechaIni2 = FechaIni

        'CostoPromedio = CostoPromedioKardexBodega(CodigoProducto, FechaIni2.AddDays(-1), CodBodega)
        'CostoPromedioD = CostoPromedioDolar

        ImporteCompra = 0
        UnidadComprada = 0
        MontoInicial = 0
        MontoInicialD = 0
        ImporteDevVenta = 0
        ImporteVenta = 0
        ImporteSalida = 0
        ImporteDevCompra = 0
        ImporteCompraD = 0
        TransferenciaRecibida = 0
        TransferenciaEnviada = 0

        '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS CORDOBAS////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
        '              "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega = '" & CodBodega & "')"
        SqlConsulta = "SELECT     SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) * ISNULL(TasaCambio.MontoTasa, 1) END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) / ISNULL(TasaCambio.MontoTasa, 1) END) AS ImporteD FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra LEFT OUTER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') AND (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega = '" & CodBodega & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        If DataSet.Tables("Compras").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("Cantidad")) Then
                UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
            End If
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("Importe")) Then
                ImporteCompra = DataSet.Tables("Compras").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("ImporteD")) Then
                ImporteCompraD = DataSet.Tables("Compras").Rows(0)("ImporteD")
            End If
        End If


        '//////////////////////////////////BUSCO EL TOTAL DE TRANSFERENCIAS RECIBIDAS////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Transferencia Recibida') AND (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega = '" & CodBodega & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "ComprasD")
        If DataSet.Tables("ComprasD").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("Cantidad")) Then
                UnidadTransRecibida = DataSet.Tables("ComprasD").Rows(0)("Cantidad")
            End If
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("Importe")) Then
                ImporteTransRecibida = DataSet.Tables("ComprasD").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("ImporteD")) Then
                ImporteTransRecibidaD = DataSet.Tables("ComprasD").Rows(0)("ImporteD")
            End If
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Costo_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                      "WHERE (Facturas.Fecha_Factura <= CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Facturas.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Devolucion de Venta')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            ImporteDevVenta = DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")  'DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
            ImporteDevVentaD = (DataSet.Tables("DevolucionFacturas").Rows(0)("Importe"))
        End If


        UnidadVendida = 0

        '//////////////////////////////////BUSCO EL TOTAL DE LAS VENTAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Costo_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                      "WHERE (Facturas.Fecha_Factura <= CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodBodega & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = N'Factura') "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Ventas")
        If DataSet.Tables("Ventas").Rows.Count <> 0 Then
            UnidadVendida = DataSet.Tables("Ventas").Rows(0)("Cantidad")
            If Not IsDBNull(DataSet.Tables("Ventas").Rows(0)("Importe")) Then
                ImporteVenta = DataSet.Tables("Ventas").Rows(0)("Importe")
            Else
                ImporteVenta = 0
            End If

            If Not IsDBNull(DataSet.Tables("Ventas").Rows(0)("Importe")) Then
                ImporteVentaD = DataSet.Tables("Ventas").Rows(0)("Importe")
            Else
                ImporteVentaD = 0
            End If
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS TRANSFERENCIAS ENVIADAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                      "WHERE (Facturas.Fecha_Factura <= CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodBodega & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Transferencia Enviada')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Transferencias")
        If DataSet.Tables("Transferencias").Rows.Count <> 0 Then
            UnidadTransEnviada = DataSet.Tables("Transferencias").Rows(0)("Cantidad")
            ImporteTransEnviada = DataSet.Tables("Transferencias").Rows(0)("Importe")  'DataSet.Tables("Ventas").Rows(0)("Importe")
            ImporteTransEnviadaD = DataSet.Tables("Transferencias").Rows(0)("Importe")
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS SALIDAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
        '              "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & FechaIni & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodBodega & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Salida Bodega') "
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Importe, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                      "WHERE (Facturas.Fecha_Factura <= CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102))  AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodBodega & "') GROUP BY Facturas.Tipo_Factura HAVING  (Facturas.Tipo_Factura = 'Salida Bodega')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Salida")
        If DataSet.Tables("Salida").Rows.Count <> 0 Then
            SalidaBodega = DataSet.Tables("Salida").Rows(0)("Cantidad")
            ImporteSalida = DataSet.Tables("Salida").Rows(0)("Importe") ' DataSet.Tables("Salida").Rows(0)("Importe")
            ImporteSalidaD = DataSet.Tables("Salida").Rows(0)("Cantidad") * CostoPromedioDolar
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  COMPRAS//////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Importe) AS Importe FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & FechaIni & "', 102)) GROUP BY Detalle_Compras.Tipo_Compra, Compras.Cod_Bodega HAVING (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra') "
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad*Detalle_Compras.Precio_Neto) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra  " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra <= CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega = '" & CodBodega & "') GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionCompras")
        If DataSet.Tables("DevolucionCompras").Rows.Count <> 0 Then
            DevolucionCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Cantidad")
            ImporteDevCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Importe")  'DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
            ImporteDevCompraD = DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
        End If


        ConsultaInventarioInicialBodega = UnidadComprada + DevolucionFactura + UnidadTransRecibida - UnidadVendida - SalidaBodega - UnidadTransEnviada - DevolucionCompra
        MontoInicial = ImporteCompra + ImporteDevVenta + ImporteTransRecibida - ImporteSalida - ImporteDevCompra - ImporteVenta - ImporteTransEnviada
        MontoInicialD = ImporteCompraD + ImporteDevVentaD + ImporteTransRecibidaD - ImporteSalidaD - ImporteDevCompraD - ImporteVentaD - ImporteTransEnviadaD

    End Function



    Public Function BuscaInventarioInicialBodega(ByVal CodigoProducto As String, ByVal FechaIni As String, ByVal CodBodega As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), FechaIni2 As Date
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, UnidadComprada As Double
        Dim Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0, UnidadVendida As Double, ImporteVenta As Double, ImporteDevCompra As Double, ImporteDevVenta As Double
        Dim SalidaBodega As Double = 0, ImporteSalida As Double = 0, CostoPromedio As Double = 0, CostoPromedioD As Double = 0, TransferenciaRecibida As Double, TransferenciaEnviada As Double
        Dim ImporteCompraD As Double = 0, ImporteDevVentaD As Double = 0, ImporteVentaD As Double = 0, ImporteSalidaD As Double = 0, ImporteDevCompraD As Double
        Dim ImporteTransEnviada As Double, ImporteTransRecibida As Double = 0, UnidadTransEnviada As Double, UnidadTransRecibida As Double = 0, ImporteTransEnviadaD As Double = 0, ImporteTransRecibidaD As Double = 0

        FechaIni2 = FechaIni

        'CostoPromedio = CostoPromedioKardexBodega(CodigoProducto, FechaIni2.AddDays(-1), CodBodega)
        'CostoPromedioD = CostoPromedioDolar

        ImporteCompra = 0
        UnidadComprada = 0
        MontoInicial = 0
        MontoInicialD = 0
        ImporteDevVenta = 0
        ImporteVenta = 0
        ImporteSalida = 0
        ImporteDevCompra = 0
        ImporteCompraD = 0
        TransferenciaRecibida = 0
        TransferenciaEnviada = 0

        '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS CORDOBAS////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
        '              "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega = '" & CodBodega & "')"
        SqlConsulta = "SELECT     SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) * ISNULL(TasaCambio.MontoTasa, 1) END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) / ISNULL(TasaCambio.MontoTasa, 1) END) AS ImporteD FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra LEFT OUTER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega = '" & CodBodega & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        If DataSet.Tables("Compras").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("Cantidad")) Then
                UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
            End If
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("Importe")) Then
                ImporteCompra = DataSet.Tables("Compras").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("ImporteD")) Then
                ImporteCompraD = DataSet.Tables("Compras").Rows(0)("ImporteD")
            End If
        End If


        '//////////////////////////////////BUSCO EL TOTAL DE TRANSFERENCIAS RECIBIDAS////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Transferencia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega = '" & CodBodega & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "ComprasD")
        If DataSet.Tables("ComprasD").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("Cantidad")) Then
                UnidadTransRecibida = DataSet.Tables("ComprasD").Rows(0)("Cantidad")
            End If
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("Importe")) Then
                ImporteTransRecibida = DataSet.Tables("ComprasD").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("ImporteD")) Then
                ImporteTransRecibidaD = DataSet.Tables("ComprasD").Rows(0)("ImporteD")
            End If
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Costo_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                      "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Facturas.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Devolucion de Venta')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            ImporteDevVenta = DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")  'DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
            ImporteDevVentaD = (DataSet.Tables("DevolucionFacturas").Rows(0)("Importe"))
        End If


        UnidadVendida = 0

        '//////////////////////////////////BUSCO EL TOTAL DE LAS VENTAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Costo_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                      "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodBodega & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = N'Factura') "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Ventas")
        If DataSet.Tables("Ventas").Rows.Count <> 0 Then
            UnidadVendida = DataSet.Tables("Ventas").Rows(0)("Cantidad")
            If Not IsDBNull(DataSet.Tables("Ventas").Rows(0)("Importe")) Then
                ImporteVenta = DataSet.Tables("Ventas").Rows(0)("Importe")
            Else
                ImporteVenta = 0
            End If

            If Not IsDBNull(DataSet.Tables("Ventas").Rows(0)("Importe")) Then
                ImporteVentaD = DataSet.Tables("Ventas").Rows(0)("Importe")
            Else
                ImporteVentaD = 0
            End If
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS TRANSFERENCIAS ENVIADAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                      "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodBodega & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Transferencia Enviada')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Transferencias")
        If DataSet.Tables("Transferencias").Rows.Count <> 0 Then
            UnidadTransEnviada = DataSet.Tables("Transferencias").Rows(0)("Cantidad")
            ImporteTransEnviada = DataSet.Tables("Transferencias").Rows(0)("Importe")  'DataSet.Tables("Ventas").Rows(0)("Importe")
            ImporteTransEnviadaD = DataSet.Tables("Transferencias").Rows(0)("Importe")
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS SALIDAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
        '              "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & FechaIni & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodBodega & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Salida Bodega') "
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Importe, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                      "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102))  AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodBodega & "') GROUP BY Facturas.Tipo_Factura HAVING  (Facturas.Tipo_Factura = 'Salida Bodega')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Salida")
        If DataSet.Tables("Salida").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("Salida").Rows(0)("Cantidad")) Then
                SalidaBodega = DataSet.Tables("Salida").Rows(0)("Cantidad")
                ImporteSalidaD = DataSet.Tables("Salida").Rows(0)("Cantidad") * CostoPromedioDolar
            End If
            If Not IsDBNull(DataSet.Tables("Salida").Rows(0)("Importe")) Then
                ImporteSalida = DataSet.Tables("Salida").Rows(0)("Importe")
            End If
            ' DataSet.Tables("Salida").Rows(0)("Importe")

        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  COMPRAS//////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Importe) AS Importe FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & FechaIni & "', 102)) GROUP BY Detalle_Compras.Tipo_Compra, Compras.Cod_Bodega HAVING (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra') "
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad*Detalle_Compras.Precio_Neto) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra  " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega = '" & CodBodega & "') GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionCompras")
        If DataSet.Tables("DevolucionCompras").Rows.Count <> 0 Then
            DevolucionCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Cantidad")
            ImporteDevCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Importe")  'DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
            ImporteDevCompraD = DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
        End If


        BuscaInventarioInicialBodega = UnidadComprada + DevolucionFactura + UnidadTransRecibida - UnidadVendida - SalidaBodega - UnidadTransEnviada - DevolucionCompra
        MontoInicial = ImporteCompra + ImporteDevVenta + ImporteTransRecibida - ImporteSalida - ImporteDevCompra - ImporteVenta - ImporteTransEnviada
        MontoInicialD = ImporteCompraD + ImporteDevVentaD + ImporteTransRecibidaD - ImporteSalidaD - ImporteDevCompraD - ImporteVentaD - ImporteTransEnviadaD

    End Function
    Public Function BuscaInventarioInicialProyectos(ByVal CodigoProyecto As String, ByVal FechaIni As String, ByVal CodBodega As String, ByVal CodBodega2 As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), FechaIni2 As Date
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, UnidadComprada As Double
        Dim Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0, UnidadVendida As Double, ImporteVenta As Double, ImporteDevCompra As Double, ImporteDevVenta As Double
        Dim SalidaBodega As Double = 0, ImporteSalida As Double = 0, CostoPromedio As Double = 0, CostoPromedioD As Double = 0, TransferenciaRecibida As Double, TransferenciaEnviada As Double
        Dim ImporteCompraD As Double = 0, ImporteDevVentaD As Double = 0, ImporteVentaD As Double = 0, ImporteSalidaD As Double = 0, ImporteDevCompraD As Double
        Dim UnidadTransEnviada As Double, ImporteTransEnviada As Double, ImporteTransEnviadaD As Double, UnidadTransRecibida As Double, ImporteTransRecibida As Double, ImporteTransRecibidaD As Double

        FechaIni2 = FechaIni

        'CostoPromedio = CostoPromedioKardexBodega(CodigoProducto, FechaIni2.AddDays(-1), CodBodega)
        'CostoPromedioD = CostoPromedioDolar

        ImporteCompra = 0
        UnidadComprada = 0
        MontoInicial = 0
        MontoInicialD = 0
        ImporteDevVenta = 0
        ImporteVenta = 0
        ImporteSalida = 0
        ImporteDevCompra = 0
        ImporteCompraD = 0
        TransferenciaRecibida = 0
        TransferenciaEnviada = 0

        '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS CORDOBAS////////////////////////////////////////////////////////////////////

        'SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) AS Importe,  SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS ImporteD,Compras.MonedaCompra FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
        '"WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & FechaIni & "', 102)) AND (Compras.Cod_Bodega = '" & CodBodega & "') GROUP BY Compras.MonedaCompra HAVING (MAX(Detalle_Compras.Tipo_Compra) = 'Mercancia Recibida') AND (Compras.MonedaCompra <> 'Dolares') OR (MAX(Detalle_Compras.Tipo_Compra) = 'Transferencia Recibida') AND (Compras.MonedaCompra <> 'Dolares')"

        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                      "WHERE (Compras.CodigoProyecto = '" & CodigoProyecto & "') AND (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        If DataSet.Tables("Compras").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("Cantidad")) Then
                UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
            End If
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("Importe")) Then
                ImporteCompra = DataSet.Tables("Compras").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("ImporteD")) Then
                ImporteCompraD = DataSet.Tables("Compras").Rows(0)("ImporteD")
            End If
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS TRANSFERENCIAS RECIBIDAS////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                      "WHERE (Compras.CodigoProyecto = '" & CodigoProyecto & "') AND (Detalle_Compras.Tipo_Compra = 'Transferencia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "ComprasD")
        If DataSet.Tables("ComprasD").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("Cantidad")) Then
                UnidadTransRecibida = DataSet.Tables("ComprasD").Rows(0)("Cantidad")
            End If
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("Importe")) Then
                ImporteTransRecibida = DataSet.Tables("ComprasD").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("ImporteD")) Then
                ImporteTransRecibidaD = DataSet.Tables("ComprasD").Rows(0)("ImporteD")
            End If
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Neto) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                      "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') AND (Facturas.CodigoProyecto = '" & CodigoProyecto & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Devolucion de Venta')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            ImporteDevVenta = DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
            ImporteDevVentaD = DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
        End If


        UnidadVendida = 0

        '//////////////////////////////////BUSCO EL TOTAL DE LAS VENTAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Neto) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
        '              "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & FechaIni & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = N'Factura') "
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Costo_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                            "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Facturas.CodigoProyecto = '" & CodigoProyecto & "') AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = N'Factura') "

        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Ventas")
        If DataSet.Tables("Ventas").Rows.Count <> 0 Then
            UnidadVendida = DataSet.Tables("Ventas").Rows(0)("Cantidad")
            ImporteVenta = DataSet.Tables("Ventas").Rows(0)("Importe")
            ImporteVentaD = DataSet.Tables("Ventas").Rows(0)("Importe")
        End If

        '//////////////////////////////////BUSCO EL TOTAL TRANSFERENCIAS ENVIADAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Neto) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
        '              "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & FechaIni & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Transferencia Enviada')"
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                              "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Facturas.CodigoProyecto = '" & CodigoProyecto & "') AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Transferencia Enviada')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "TransferenciaEnviada")
        If DataSet.Tables("TransferenciaEnviada").Rows.Count <> 0 Then
            UnidadTransEnviada = DataSet.Tables("TransferenciaEnviada").Rows(0)("Cantidad")
            ImporteTransEnviada = DataSet.Tables("TransferenciaEnviada").Rows(0)("Importe")  'DataSet.Tables("Ventas").Rows(0)("Importe")
            ImporteTransEnviadaD = DataSet.Tables("TransferenciaEnviada").Rows(0)("Importe")
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS SALIDAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
        '              "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & FechaIni & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Salida Bodega') "
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Importe, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                              "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102))  AND (Facturas.CodigoProyecto = '" & CodigoProyecto & "') AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Facturas.Tipo_Factura HAVING  (Facturas.Tipo_Factura = 'Salida Bodega')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Salida")
        If DataSet.Tables("Salida").Rows.Count <> 0 Then
            SalidaBodega = DataSet.Tables("Salida").Rows(0)("Cantidad")
            ImporteSalida = DataSet.Tables("Salida").Rows(0)("Importe")
            ImporteSalidaD = DataSet.Tables("Salida").Rows(0)("ImporteD")
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  COMPRAS//////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad*Detalle_Compras.Precio_Neto) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra  " & _
        '              "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & FechaIni & "', 102)) AND (Compras.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra')"
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad*Detalle_Compras.Precio_Neto) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra  " & _
                     "WHERE (Compras.CodigoProyecto = '" & CodigoProyecto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & FechaIni & "', 102)) AND (Compras.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionCompras")
        If DataSet.Tables("DevolucionCompras").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionCompras").Rows(0)("Cantidad")
            ImporteDevCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
            ImporteDevCompraD = DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
        End If

        'BuscaInventarioInicialEntreBodega = UnidadComprada + DevolucionFactura - UnidadVendida - SalidaBodega
        'MontoInicial = ImporteCompra + ImporteDevVenta - ImporteSalida - ImporteDevCompra - ImporteVenta
        'MontoInicialD = ImporteCompraD + ImporteDevVentaD - ImporteSalidaD - ImporteDevCompraD - ImporteVentaD

        BuscaInventarioInicialProyectos = UnidadComprada + DevolucionFactura + UnidadTransRecibida - UnidadVendida - SalidaBodega - UnidadTransEnviada - DevolucionCompra
        MontoInicial = ImporteCompra + ImporteDevVenta + ImporteTransRecibida - ImporteSalida - ImporteDevCompra - ImporteVenta - ImporteTransEnviada
        MontoInicialD = ImporteCompraD + ImporteDevVentaD + ImporteTransRecibidaD - ImporteSalidaD - ImporteDevCompraD - ImporteVentaD - ImporteTransEnviadaD


    End Function
    Public Function BuscaInventarioInicialLote(ByVal CodigoProducto As String, ByVal FechaIni As String, ByVal CodBodega2 As String, ByVal LoteIni As String, ByVal LoteFin As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), FechaIni2 As Date
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, UnidadComprada As Double
        Dim Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0, UnidadVendida As Double, ImporteVenta As Double, ImporteDevCompra As Double, ImporteDevVenta As Double
        Dim SalidaBodega As Double = 0, ImporteSalida As Double = 0, CostoPromedio As Double = 0, CostoPromedioD As Double = 0, TransferenciaRecibida As Double, TransferenciaEnviada As Double
        Dim ImporteCompraD As Double = 0, ImporteDevVentaD As Double = 0, ImporteVentaD As Double = 0, ImporteSalidaD As Double = 0, ImporteDevCompraD As Double
        Dim UnidadTransEnviada As Double, ImporteTransEnviada As Double, ImporteTransEnviadaD As Double, UnidadTransRecibida As Double, ImporteTransRecibida As Double, ImporteTransRecibidaD As Double

        FechaIni2 = FechaIni

        'CostoPromedio = CostoPromedioKardexBodega(CodigoProducto, FechaIni2.AddDays(-1), CodBodega)
        'CostoPromedioD = CostoPromedioDolar

        ImporteCompra = 0
        UnidadComprada = 0
        MontoInicial = 0
        MontoInicialD = 0
        ImporteDevVenta = 0
        ImporteVenta = 0
        ImporteSalida = 0
        ImporteDevCompra = 0
        ImporteCompraD = 0
        TransferenciaRecibida = 0
        TransferenciaEnviada = 0

        '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS CORDOBAS////////////////////////////////////////////////////////////////////

        If LoteIni = "" And LoteFin = "" Then
            SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                          "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) "
        Else
            SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                           "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Numero_Lote BETWEEN '" & LoteIni & "' AND '" & LoteFin & "')"
        End If
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        If DataSet.Tables("Compras").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("Cantidad")) Then
                UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
            End If
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("Importe")) Then
                ImporteCompra = DataSet.Tables("Compras").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("ImporteD")) Then
                ImporteCompraD = DataSet.Tables("Compras").Rows(0)("ImporteD")
            End If
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS TRANSFERENCIAS RECIBIDAS////////////////////////////////////////////////////////////////////
        If LoteIni = "" And LoteFin = "" Then
            SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                          "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Transferencia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) "
        Else
            SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                          "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Transferencia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Detalle_Compras.Numero_Lote BETWEEN '" & LoteIni & "' AND '" & LoteFin & "')"
        End If
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "ComprasD")
        If DataSet.Tables("ComprasD").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("Cantidad")) Then
                UnidadTransRecibida = DataSet.Tables("ComprasD").Rows(0)("Cantidad")
            End If
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("Importe")) Then
                ImporteTransRecibida = DataSet.Tables("ComprasD").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("ImporteD")) Then
                ImporteTransRecibidaD = DataSet.Tables("ComprasD").Rows(0)("ImporteD")
            End If
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
        If LoteIni = "" And LoteFin = "" Then
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Neto) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                          "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102))  AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Devolucion de Venta')"
        Else
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Neto) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                          "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102))  AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.CodTarea BETWEEN '" & LoteIni & "' AND '" & LoteFin & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Devolucion de Venta') "
        End If
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            ImporteDevVenta = DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
            ImporteDevVentaD = DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
        End If


        UnidadVendida = 0

        '//////////////////////////////////BUSCO EL TOTAL DE LAS VENTAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        If LoteIni = "" And LoteFin = "" Then
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Costo_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                          "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "')  GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = N'Factura') "
        Else
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Costo_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                           "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.CodTarea BETWEEN '" & LoteIni & "' AND '" & LoteFin & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = N'Factura') "
        End If
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Ventas")
        If DataSet.Tables("Ventas").Rows.Count <> 0 Then
            UnidadVendida = DataSet.Tables("Ventas").Rows(0)("Cantidad")
            If Not IsDBNull(DataSet.Tables("Ventas").Rows(0)("Importe")) Then
                ImporteVenta = DataSet.Tables("Ventas").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("Ventas").Rows(0)("Importe")) Then
                ImporteVentaD = DataSet.Tables("Ventas").Rows(0)("Importe")
            End If
        End If

        '//////////////////////////////////BUSCO EL TOTAL TRANSFERENCIAS ENVIADAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        If LoteIni = "" And LoteFin = "" Then
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                                  "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Transferencia Enviada')"
        Else
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                                  "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "')  AND (Detalle_Facturas.CodTarea BETWEEN '" & LoteIni & "' AND '" & LoteFin & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Transferencia Enviada')  "
        End If
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "TransferenciaEnviada")
        If DataSet.Tables("TransferenciaEnviada").Rows.Count <> 0 Then
            UnidadTransEnviada = DataSet.Tables("TransferenciaEnviada").Rows(0)("Cantidad")
            ImporteTransEnviada = DataSet.Tables("TransferenciaEnviada").Rows(0)("Importe")  'DataSet.Tables("Ventas").Rows(0)("Importe")
            ImporteTransEnviadaD = DataSet.Tables("TransferenciaEnviada").Rows(0)("Importe")
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS SALIDAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        If LoteIni = "" And LoteFin = "" Then
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Importe, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                           "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102))  AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') GROUP BY Facturas.Tipo_Factura HAVING  (Facturas.Tipo_Factura = 'Salida Bodega')"
        Else
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Importe, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                            "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102))  AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "')  AND (Detalle_Facturas.CodTarea BETWEEN '" & LoteIni & "' AND '" & LoteFin & "') GROUP BY Facturas.Tipo_Factura HAVING  (Facturas.Tipo_Factura = 'Salida Bodega')  "
        End If
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Salida")
        If DataSet.Tables("Salida").Rows.Count <> 0 Then
            SalidaBodega = DataSet.Tables("Salida").Rows(0)("Cantidad")
            ImporteSalida = DataSet.Tables("Salida").Rows(0)("Importe")
            ImporteSalidaD = DataSet.Tables("Salida").Rows(0)("ImporteD")
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  COMPRAS//////////////////////////////////////////////////////////////////////
        If LoteIni = "" And LoteFin = "" Then
            SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad*Detalle_Compras.Precio_Neto) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra  " & _
                         "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102))  GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra')"
        Else
            SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad*Detalle_Compras.Precio_Neto) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra  " & _
                         "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102))  AND (Detalle_Compras.Numero_Lote BETWEEN '" & LoteIni & "' AND '" & LoteFin & "') GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra')"
        End If
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionCompras")
        If DataSet.Tables("DevolucionCompras").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionCompras").Rows(0)("Cantidad")
            ImporteDevCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
            ImporteDevCompraD = DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
        End If

        'BuscaInventarioInicialEntreBodega = UnidadComprada + DevolucionFactura - UnidadVendida - SalidaBodega
        'MontoInicial = ImporteCompra + ImporteDevVenta - ImporteSalida - ImporteDevCompra - ImporteVenta
        'MontoInicialD = ImporteCompraD + ImporteDevVentaD - ImporteSalidaD - ImporteDevCompraD - ImporteVentaD

        BuscaInventarioInicialLote = UnidadComprada + DevolucionFactura + UnidadTransRecibida - UnidadVendida - SalidaBodega - UnidadTransEnviada - DevolucionCompra
        MontoInicial = ImporteCompra + ImporteDevVenta + ImporteTransRecibida - ImporteSalida - ImporteDevCompra - ImporteVenta - ImporteTransEnviada
        MontoInicialD = ImporteCompraD + ImporteDevVentaD + ImporteTransRecibidaD - ImporteSalidaD - ImporteDevCompraD - ImporteVentaD - ImporteTransEnviadaD


    End Function



    Public Function BuscaInventarioInicialEntreBodegaLote(ByVal CodigoProducto As String, ByVal FechaIni As String, ByVal CodBodega As String, ByVal CodBodega2 As String, ByVal LoteIni As String, ByVal LoteFin As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), FechaIni2 As Date
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, UnidadComprada As Double
        Dim Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0, UnidadVendida As Double, ImporteVenta As Double, ImporteDevCompra As Double, ImporteDevVenta As Double
        Dim SalidaBodega As Double = 0, ImporteSalida As Double = 0, CostoPromedio As Double = 0, CostoPromedioD As Double = 0, TransferenciaRecibida As Double, TransferenciaEnviada As Double
        Dim ImporteCompraD As Double = 0, ImporteDevVentaD As Double = 0, ImporteVentaD As Double = 0, ImporteSalidaD As Double = 0, ImporteDevCompraD As Double
        Dim UnidadTransEnviada As Double, ImporteTransEnviada As Double, ImporteTransEnviadaD As Double, UnidadTransRecibida As Double, ImporteTransRecibida As Double, ImporteTransRecibidaD As Double

        FechaIni2 = FechaIni

        'CostoPromedio = CostoPromedioKardexBodega(CodigoProducto, FechaIni2.AddDays(-1), CodBodega)
        'CostoPromedioD = CostoPromedioDolar

        ImporteCompra = 0
        UnidadComprada = 0
        MontoInicial = 0
        MontoInicialD = 0
        ImporteDevVenta = 0
        ImporteVenta = 0
        ImporteSalida = 0
        ImporteDevCompra = 0
        ImporteCompraD = 0
        TransferenciaRecibida = 0
        TransferenciaEnviada = 0

        '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS CORDOBAS////////////////////////////////////////////////////////////////////

        If LoteIni = "" And LoteFin = "" Then
            SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                          "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "')"
        Else
            SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                           "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') AND (Detalle_Compras.Numero_Lote BETWEEN '" & LoteIni & "' AND '" & LoteFin & "')"
        End If
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        If DataSet.Tables("Compras").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("Cantidad")) Then
                UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
            End If
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("Importe")) Then
                ImporteCompra = DataSet.Tables("Compras").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("ImporteD")) Then
                ImporteCompraD = DataSet.Tables("Compras").Rows(0)("ImporteD")
            End If
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS TRANSFERENCIAS RECIBIDAS////////////////////////////////////////////////////////////////////
        If LoteIni = "" And LoteFin = "" Then
            SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                          "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Transferencia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "')"
        Else
            SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                          "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Transferencia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') AND (Detalle_Compras.Numero_Lote BETWEEN '" & LoteIni & "' AND '" & LoteFin & "')"
        End If
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "ComprasD")
        If DataSet.Tables("ComprasD").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("Cantidad")) Then
                UnidadTransRecibida = DataSet.Tables("ComprasD").Rows(0)("Cantidad")
            End If
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("Importe")) Then
                ImporteTransRecibida = DataSet.Tables("ComprasD").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("ImporteD")) Then
                ImporteTransRecibidaD = DataSet.Tables("ComprasD").Rows(0)("ImporteD")
            End If
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
        If LoteIni = "" And LoteFin = "" Then
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Neto) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                          "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Devolucion de Venta')"
        Else
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Neto) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                          "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.CodTarea BETWEEN '" & LoteIni & "' AND '" & LoteFin & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Devolucion de Venta') "
        End If
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            ImporteDevVenta = DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
            ImporteDevVentaD = DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
        End If


        UnidadVendida = 0

        '//////////////////////////////////BUSCO EL TOTAL DE LAS VENTAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        If LoteIni = "" And LoteFin = "" Then
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Costo_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                          "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = N'Factura') "
        Else
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Costo_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                           "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') AND (Detalle_Facturas.CodTarea BETWEEN '" & LoteIni & "' AND '" & LoteFin & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = N'Factura') "
        End If
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Ventas")
        If DataSet.Tables("Ventas").Rows.Count <> 0 Then
            UnidadVendida = DataSet.Tables("Ventas").Rows(0)("Cantidad")
            If Not IsDBNull(DataSet.Tables("Ventas").Rows(0)("Importe")) Then
                ImporteVenta = DataSet.Tables("Ventas").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("Ventas").Rows(0)("Importe")) Then
                ImporteVentaD = DataSet.Tables("Ventas").Rows(0)("Importe")
            End If
        End If

        '//////////////////////////////////BUSCO EL TOTAL TRANSFERENCIAS ENVIADAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        If LoteIni = "" And LoteFin = "" Then
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                                  "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Transferencia Enviada')"
        Else
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                                  "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') AND (Detalle_Facturas.CodTarea BETWEEN '" & LoteIni & "' AND '" & LoteFin & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Transferencia Enviada')  "
        End If
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "TransferenciaEnviada")
        If DataSet.Tables("TransferenciaEnviada").Rows.Count <> 0 Then
            UnidadTransEnviada = DataSet.Tables("TransferenciaEnviada").Rows(0)("Cantidad")
            ImporteTransEnviada = DataSet.Tables("TransferenciaEnviada").Rows(0)("Importe")  'DataSet.Tables("Ventas").Rows(0)("Importe")
            ImporteTransEnviadaD = DataSet.Tables("TransferenciaEnviada").Rows(0)("Importe")
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS SALIDAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        If LoteIni = "" And LoteFin = "" Then
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Importe, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                           "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102))  AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Facturas.Tipo_Factura HAVING  (Facturas.Tipo_Factura = 'Salida Bodega')"
        Else
            SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Importe, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                            "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102))  AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') AND (Detalle_Facturas.CodTarea BETWEEN '" & LoteIni & "' AND '" & LoteFin & "') GROUP BY Facturas.Tipo_Factura HAVING  (Facturas.Tipo_Factura = 'Salida Bodega')  "
        End If
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Salida")
        If DataSet.Tables("Salida").Rows.Count <> 0 Then
            SalidaBodega = DataSet.Tables("Salida").Rows(0)("Cantidad")
            ImporteSalida = DataSet.Tables("Salida").Rows(0)("Importe")
            ImporteSalidaD = DataSet.Tables("Salida").Rows(0)("ImporteD")
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  COMPRAS//////////////////////////////////////////////////////////////////////
        If LoteIni = "" And LoteFin = "" Then
            SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad*Detalle_Compras.Precio_Neto) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra  " & _
                         "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra')"
        Else
            SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad*Detalle_Compras.Precio_Neto) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra  " & _
                         "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') AND (Detalle_Compras.Numero_Lote BETWEEN '" & LoteIni & "' AND '" & LoteFin & "') GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra')"
        End If
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionCompras")
        If DataSet.Tables("DevolucionCompras").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionCompras").Rows(0)("Cantidad")
            ImporteDevCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
            ImporteDevCompraD = DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
        End If

        'BuscaInventarioInicialEntreBodega = UnidadComprada + DevolucionFactura - UnidadVendida - SalidaBodega
        'MontoInicial = ImporteCompra + ImporteDevVenta - ImporteSalida - ImporteDevCompra - ImporteVenta
        'MontoInicialD = ImporteCompraD + ImporteDevVentaD - ImporteSalidaD - ImporteDevCompraD - ImporteVentaD

        BuscaInventarioInicialEntreBodegaLote = UnidadComprada + DevolucionFactura + UnidadTransRecibida - UnidadVendida - SalidaBodega - UnidadTransEnviada - DevolucionCompra
        MontoInicial = ImporteCompra + ImporteDevVenta + ImporteTransRecibida - ImporteSalida - ImporteDevCompra - ImporteVenta - ImporteTransEnviada
        MontoInicialD = ImporteCompraD + ImporteDevVentaD + ImporteTransRecibidaD - ImporteSalidaD - ImporteDevCompraD - ImporteVentaD - ImporteTransEnviadaD


    End Function
    Public Function BuscaInventarioInicialEntreBodega(ByVal CodigoProducto As String, ByVal FechaIni As String, ByVal CodBodega As String, ByVal CodBodega2 As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), FechaIni2 As Date
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, UnidadComprada As Double
        Dim Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0, UnidadVendida As Double, ImporteVenta As Double, ImporteDevCompra As Double, ImporteDevVenta As Double
        Dim SalidaBodega As Double = 0, ImporteSalida As Double = 0, CostoPromedio As Double = 0, CostoPromedioD As Double = 0, TransferenciaRecibida As Double, TransferenciaEnviada As Double
        Dim ImporteCompraD As Double = 0, ImporteDevVentaD As Double = 0, ImporteVentaD As Double = 0, ImporteSalidaD As Double = 0, ImporteDevCompraD As Double
        Dim UnidadTransEnviada As Double, ImporteTransEnviada As Double, ImporteTransEnviadaD As Double, UnidadTransRecibida As Double, ImporteTransRecibida As Double, ImporteTransRecibidaD As Double

        FechaIni2 = FechaIni

        'CostoPromedio = CostoPromedioKardexBodega(CodigoProducto, FechaIni2.AddDays(-1), CodBodega)
        'CostoPromedioD = CostoPromedioDolar

        ImporteCompra = 0
        UnidadComprada = 0
        MontoInicial = 0
        MontoInicialD = 0
        ImporteDevVenta = 0
        ImporteVenta = 0
        ImporteSalida = 0
        ImporteDevCompra = 0
        ImporteCompraD = 0
        TransferenciaRecibida = 0
        TransferenciaEnviada = 0

        '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS CORDOBAS////////////////////////////////////////////////////////////////////

        'SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) AS Importe,  SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS ImporteD,Compras.MonedaCompra FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
        '"WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & FechaIni & "', 102)) AND (Compras.Cod_Bodega = '" & CodBodega & "') GROUP BY Compras.MonedaCompra HAVING (MAX(Detalle_Compras.Tipo_Compra) = 'Mercancia Recibida') AND (Compras.MonedaCompra <> 'Dolares') OR (MAX(Detalle_Compras.Tipo_Compra) = 'Transferencia Recibida') AND (Compras.MonedaCompra <> 'Dolares')"

        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        If DataSet.Tables("Compras").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("Cantidad")) Then
                UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
            End If
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("Importe")) Then
                ImporteCompra = DataSet.Tables("Compras").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("ImporteD")) Then
                ImporteCompraD = DataSet.Tables("Compras").Rows(0)("ImporteD")
            End If
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS TRANSFERENCIAS RECIBIDAS////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Transferencia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "ComprasD")
        If DataSet.Tables("ComprasD").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("Cantidad")) Then
                UnidadTransRecibida = DataSet.Tables("ComprasD").Rows(0)("Cantidad")
            End If
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("Importe")) Then
                ImporteTransRecibida = DataSet.Tables("ComprasD").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("ImporteD")) Then
                ImporteTransRecibidaD = DataSet.Tables("ComprasD").Rows(0)("ImporteD")
            End If
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Neto) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                      "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Devolucion de Venta')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            ImporteDevVenta = DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
            ImporteDevVentaD = DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
        End If


        UnidadVendida = 0

        '//////////////////////////////////BUSCO EL TOTAL DE LAS VENTAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Neto) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
        '              "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & FechaIni & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = N'Factura') "
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Costo_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                            "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = N'Factura') "

        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Ventas")
        If DataSet.Tables("Ventas").Rows.Count <> 0 Then
            UnidadVendida = DataSet.Tables("Ventas").Rows(0)("Cantidad")
            If Not IsDBNull(DataSet.Tables("Ventas").Rows(0)("Importe")) Then
                ImporteVenta = DataSet.Tables("Ventas").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("Ventas").Rows(0)("Importe")) Then
                ImporteVentaD = DataSet.Tables("Ventas").Rows(0)("Importe")
            End If
        End If

        '//////////////////////////////////BUSCO EL TOTAL TRANSFERENCIAS ENVIADAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Neto) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
        '              "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & FechaIni & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Transferencia Enviada')"
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                              "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') AND (Facturas.TransferenciaProcesada = 1) GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Transferencia Enviada')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "TransferenciaEnviada")
        If DataSet.Tables("TransferenciaEnviada").Rows.Count <> 0 Then
            UnidadTransEnviada = DataSet.Tables("TransferenciaEnviada").Rows(0)("Cantidad")
            ImporteTransEnviada = DataSet.Tables("TransferenciaEnviada").Rows(0)("Importe")  'DataSet.Tables("Ventas").Rows(0)("Importe")
            ImporteTransEnviadaD = DataSet.Tables("TransferenciaEnviada").Rows(0)("Importe")
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS SALIDAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
        '              "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & FechaIni & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Salida Bodega') "
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Importe, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                              "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102))  AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Facturas.Tipo_Factura HAVING  (Facturas.Tipo_Factura = 'Salida Bodega')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Salida")
        If DataSet.Tables("Salida").Rows.Count <> 0 Then
            SalidaBodega = DataSet.Tables("Salida").Rows(0)("Cantidad")
            ImporteSalida = DataSet.Tables("Salida").Rows(0)("Importe")
            ImporteSalidaD = DataSet.Tables("Salida").Rows(0)("ImporteD")
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  COMPRAS//////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad*Detalle_Compras.Precio_Neto) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra  " & _
        '              "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & FechaIni & "', 102)) AND (Compras.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra')"
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad*Detalle_Compras.Precio_Neto) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra  " & _
                     "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega Between '" & CodBodega & "' and '" & CodBodega2 & "') GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionCompras")
        If DataSet.Tables("DevolucionCompras").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionCompras").Rows(0)("Cantidad")
            ImporteDevCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
            ImporteDevCompraD = DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
        End If

        'BuscaInventarioInicialEntreBodega = UnidadComprada + DevolucionFactura - UnidadVendida - SalidaBodega
        'MontoInicial = ImporteCompra + ImporteDevVenta - ImporteSalida - ImporteDevCompra - ImporteVenta
        'MontoInicialD = ImporteCompraD + ImporteDevVentaD - ImporteSalidaD - ImporteDevCompraD - ImporteVentaD

        BuscaInventarioInicialEntreBodega = UnidadComprada + DevolucionFactura + UnidadTransRecibida - UnidadVendida - SalidaBodega - UnidadTransEnviada - DevolucionCompra
        MontoInicial = ImporteCompra + ImporteDevVenta + ImporteTransRecibida - ImporteSalida - ImporteDevCompra - ImporteVenta - ImporteTransEnviada
        MontoInicialD = ImporteCompraD + ImporteDevVentaD + ImporteTransRecibidaD - ImporteSalidaD - ImporteDevCompraD - ImporteVentaD - ImporteTransEnviadaD


    End Function


    Public Function BuscaInventarioInicialProyectos(ByVal CodigoProyecto As String, ByVal FechaIni As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), FechaIni2 As Date
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, UnidadComprada As Double
        Dim Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0, UnidadVendida As Double, ImporteVenta As Double, ImporteDevCompra As Double, ImporteDevVenta As Double
        Dim SalidaBodega As Double = 0, ImporteSalida As Double = 0, CostoPromedio As Double = 0, CostoPromedioD As Double = 0, TransferenciaRecibida As Double, TransferenciaEnviada As Double
        Dim ImporteCompraD As Double = 0, ImporteDevVentaD As Double = 0, ImporteVentaD As Double = 0, ImporteSalidaD As Double = 0, ImporteDevCompraD As Double
        Dim ImporteTransEnviada As Double, ImporteTransRecibida As Double = 0, UnidadTransEnviada As Double, UnidadTransRecibida As Double = 0, ImporteTransEnviadaD As Double = 0, ImporteTransRecibidaD As Double = 0

        FechaIni2 = FechaIni

        'CostoPromedio = CostoPromedioKardexBodega(CodigoProducto, FechaIni2.AddDays(-1), CodBodega)
        'CostoPromedioD = CostoPromedioDolar

        ImporteCompra = 0
        UnidadComprada = 0
        MontoInicial = 0
        MontoInicialD = 0
        ImporteDevVenta = 0
        ImporteVenta = 0
        ImporteSalida = 0
        ImporteDevCompra = 0
        ImporteCompraD = 0
        TransferenciaRecibida = 0
        TransferenciaEnviada = 0

        '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS CORDOBAS////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                      "WHERE (Compras.CodigoProyecto = '" & CodigoProyecto & "') AND (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        If DataSet.Tables("Compras").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("Cantidad")) Then
                UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
            End If
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("Importe")) Then
                ImporteCompra = DataSet.Tables("Compras").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("ImporteD")) Then
                ImporteCompraD = DataSet.Tables("Compras").Rows(0)("ImporteD")
            End If
        End If


        '//////////////////////////////////BUSCO EL TOTAL DE TRANSFERENCIAS RECIBIDAS////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                      "WHERE (Compras.CodigoProyecto = '" & CodigoProyecto & "') AND (Detalle_Compras.Tipo_Compra = 'Transferencia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "ComprasD")
        If DataSet.Tables("ComprasD").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("Cantidad")) Then
                UnidadTransRecibida = DataSet.Tables("ComprasD").Rows(0)("Cantidad")
            End If
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("Importe")) Then
                ImporteTransRecibida = DataSet.Tables("ComprasD").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("ImporteD")) Then
                ImporteTransRecibidaD = DataSet.Tables("ComprasD").Rows(0)("ImporteD")
            End If
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Costo_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                      "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Facturas.CodigoProyecto = '" & CodigoProyecto & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Devolucion de Venta')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            ImporteDevVenta = DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")  'DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
            ImporteDevVentaD = (DataSet.Tables("DevolucionFacturas").Rows(0)("Importe"))
        End If


        UnidadVendida = 0

        '//////////////////////////////////BUSCO EL TOTAL DE LAS VENTAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Costo_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                      "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Facturas.CodigoProyecto = '" & CodigoProyecto & "')  GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = N'Factura') "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Ventas")
        If DataSet.Tables("Ventas").Rows.Count <> 0 Then
            UnidadVendida = DataSet.Tables("Ventas").Rows(0)("Cantidad")
            ImporteVenta = DataSet.Tables("Ventas").Rows(0)("Importe")  'DataSet.Tables("Ventas").Rows(0)("Importe")
            ImporteVentaD = DataSet.Tables("Ventas").Rows(0)("Importe")
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS TRANSFERENCIAS ENVIADAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                      "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Facturas.CodigoProyecto = '" & CodigoProyecto & "')  GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Transferencia Enviada')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Transferencias")
        If DataSet.Tables("Transferencias").Rows.Count <> 0 Then
            UnidadTransEnviada = DataSet.Tables("Transferencias").Rows(0)("Cantidad")
            ImporteTransEnviada = DataSet.Tables("Transferencias").Rows(0)("Importe")  'DataSet.Tables("Ventas").Rows(0)("Importe")
            ImporteTransEnviadaD = DataSet.Tables("Transferencias").Rows(0)("Importe")
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS SALIDAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
        '              "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & FechaIni & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodBodega & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Salida Bodega') "
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Importe, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                      "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102))  AND (Facturas.CodigoProyecto = '" & CodigoProyecto & "')  GROUP BY Facturas.Tipo_Factura HAVING  (Facturas.Tipo_Factura = 'Salida Bodega')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Salida")
        If DataSet.Tables("Salida").Rows.Count <> 0 Then
            SalidaBodega = DataSet.Tables("Salida").Rows(0)("Cantidad")
            ImporteSalida = DataSet.Tables("Salida").Rows(0)("Importe") ' DataSet.Tables("Salida").Rows(0)("Importe")
            ImporteSalidaD = DataSet.Tables("Salida").Rows(0)("Cantidad") * CostoPromedioDolar
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  COMPRAS//////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Importe) AS Importe FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & FechaIni & "', 102)) GROUP BY Detalle_Compras.Tipo_Compra, Compras.Cod_Bodega HAVING (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra') "
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad*Detalle_Compras.Precio_Neto) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra  " & _
                      "WHERE (Compras.CodigoProyecto = '" & CodigoProyecto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102))  GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionCompras")
        If DataSet.Tables("DevolucionCompras").Rows.Count <> 0 Then
            DevolucionCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Cantidad")
            ImporteDevCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Importe")  'DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
            ImporteDevCompraD = DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
        End If


        BuscaInventarioInicialProyectos = UnidadComprada + DevolucionFactura + UnidadTransRecibida - UnidadVendida - SalidaBodega - UnidadTransEnviada - DevolucionCompra
        MontoInicial = ImporteCompra + ImporteDevVenta + ImporteTransRecibida - ImporteSalida - ImporteDevCompra - ImporteVenta - ImporteTransEnviada
        MontoInicialD = ImporteCompraD + ImporteDevVentaD + ImporteTransRecibidaD - ImporteSalidaD - ImporteDevCompraD - ImporteVentaD - ImporteTransEnviadaD

    End Function
    Public Function BuscaInventarioInicial(ByVal CodigoProducto As String, ByVal FechaIni As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), FechaIni2 As Date
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, UnidadComprada As Double
        Dim Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0, UnidadVendida As Double, ImporteVenta As Double, ImporteDevCompra As Double, ImporteDevVenta As Double
        Dim SalidaBodega As Double = 0, ImporteSalida As Double = 0, CostoPromedio As Double = 0, CostoPromedioD As Double = 0, TransferenciaRecibida As Double, TransferenciaEnviada As Double
        Dim ImporteCompraD As Double = 0, ImporteDevVentaD As Double = 0, ImporteVentaD As Double = 0, ImporteSalidaD As Double = 0, ImporteDevCompraD As Double
        Dim ImporteTransEnviada As Double, ImporteTransRecibida As Double = 0, UnidadTransEnviada As Double, UnidadTransRecibida As Double = 0, ImporteTransEnviadaD As Double = 0, ImporteTransRecibidaD As Double = 0

        FechaIni2 = FechaIni

        'CostoPromedio = CostoPromedioKardexBodega(CodigoProducto, FechaIni2.AddDays(-1), CodBodega)
        'CostoPromedioD = CostoPromedioDolar

        ImporteCompra = 0
        UnidadComprada = 0
        MontoInicial = 0
        MontoInicialD = 0
        ImporteDevVenta = 0
        ImporteVenta = 0
        ImporteSalida = 0
        ImporteDevCompra = 0
        ImporteCompraD = 0
        TransferenciaRecibida = 0
        TransferenciaEnviada = 0

        '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS CORDOBAS////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        If DataSet.Tables("Compras").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("Cantidad")) Then
                UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
            End If
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("Importe")) Then
                ImporteCompra = DataSet.Tables("Compras").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("Compras").Rows(0)("ImporteD")) Then
                ImporteCompraD = DataSet.Tables("Compras").Rows(0)("ImporteD")
            End If
        End If


        '//////////////////////////////////BUSCO EL TOTAL DE TRANSFERENCIAS RECIBIDAS////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad,SUM(CASE WHEN Compras.MonedaCompra = 'Cordobas' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario) * TasaCambio.MontoTasa END) AS Importe, SUM(CASE WHEN Compras.MonedaCompra = 'Dolares' THEN Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario ELSE (Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario)/ TasaCambio.MontoTasa END) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Tipo_Compra = 'Transferencia Recibida') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "ComprasD")
        If DataSet.Tables("ComprasD").Rows.Count <> 0 Then
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("Cantidad")) Then
                UnidadTransRecibida = DataSet.Tables("ComprasD").Rows(0)("Cantidad")
            End If
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("Importe")) Then
                ImporteTransRecibida = DataSet.Tables("ComprasD").Rows(0)("Importe")
            End If
            If Not IsDBNull(DataSet.Tables("ComprasD").Rows(0)("ImporteD")) Then
                ImporteTransRecibidaD = DataSet.Tables("ComprasD").Rows(0)("ImporteD")
            End If
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Costo_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                      "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & FechaIni & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Devolucion de Venta')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            ImporteDevVenta = DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")  'DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
            ImporteDevVentaD = (DataSet.Tables("DevolucionFacturas").Rows(0)("Importe"))
        End If


        UnidadVendida = 0

        '//////////////////////////////////BUSCO EL TOTAL DE LAS VENTAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Costo_Unitario) AS Importe, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
        '              "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & FechaIni & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "')  GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = N'Factura') "

        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) AS Importe, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                      "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Factura')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Ventas")
        If DataSet.Tables("Ventas").Rows.Count <> 0 Then
            UnidadVendida = DataSet.Tables("Ventas").Rows(0)("Cantidad")
            ImporteVenta = DataSet.Tables("Ventas").Rows(0)("Importe")  'DataSet.Tables("Ventas").Rows(0)("Importe")
            ImporteVentaD = DataSet.Tables("Ventas").Rows(0)("ImporteD")
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS TRANSFERENCIAS ENVIADAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
                      "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.TransferenciaProcesada = 1) GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Transferencia Enviada') "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Transferencias")
        If DataSet.Tables("Transferencias").Rows.Count <> 0 Then
            UnidadTransEnviada = DataSet.Tables("Transferencias").Rows(0)("Cantidad")
            ImporteTransEnviada = DataSet.Tables("Transferencias").Rows(0)("Importe")  'DataSet.Tables("Ventas").Rows(0)("Importe")
            ImporteTransEnviadaD = DataSet.Tables("Transferencias").Rows(0)("Importe")
        End If

        '//////////////////////////////////BUSCO EL TOTAL DE LAS SALIDAS////////////////////////////////////////////////////////////////////
        DataSet.Tables.Clear()
        'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Precio_Unitario) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
        '              "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & FechaIni & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodBodega & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Salida Bodega') "
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Importe, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa " & _
                      "WHERE (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102))  AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "')  GROUP BY Facturas.Tipo_Factura HAVING  (Facturas.Tipo_Factura = 'Salida Bodega')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Salida")
        If DataSet.Tables("Salida").Rows.Count <> 0 Then
            SalidaBodega = DataSet.Tables("Salida").Rows(0)("Cantidad")
            ImporteSalida = DataSet.Tables("Salida").Rows(0)("Importe") ' DataSet.Tables("Salida").Rows(0)("Importe")
            ImporteSalidaD = DataSet.Tables("Salida").Rows(0)("Cantidad") * CostoPromedioDolar
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  COMPRAS//////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Importe) AS Importe FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & FechaIni & "', 102)) GROUP BY Detalle_Compras.Tipo_Compra, Compras.Cod_Bodega HAVING (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra') "
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad*Detalle_Compras.Precio_Neto) AS Importe FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra  " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & Format(FechaIni2, "yyyy-MM-dd") & "', 102))  GROUP BY Detalle_Compras.Tipo_Compra HAVING  (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionCompras")
        If DataSet.Tables("DevolucionCompras").Rows.Count <> 0 Then
            DevolucionCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Cantidad")
            ImporteDevCompra = DataSet.Tables("DevolucionCompras").Rows(0)("Importe")  'DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
            ImporteDevCompraD = DataSet.Tables("DevolucionCompras").Rows(0)("Importe")
        End If


        BuscaInventarioInicial = UnidadComprada + DevolucionFactura + UnidadTransRecibida - UnidadVendida - SalidaBodega - UnidadTransEnviada - DevolucionCompra
        MontoInicial = ImporteCompra + ImporteDevVenta + ImporteTransRecibida - ImporteSalida - ImporteDevCompra - ImporteVenta - ImporteTransEnviada
        MontoInicialD = ImporteCompraD + ImporteDevVentaD + ImporteTransRecibidaD - ImporteSalidaD - ImporteDevCompraD - ImporteVentaD - ImporteTransEnviadaD

    End Function

    Public Function BuscaCompra(ByVal CodigoProducto As String, ByVal FechaCompraIni As String, ByVal FechaCompraFin As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, UnidadComprada As Double
        Dim Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0, UnidadTransferencia As Double, ImporteTransferencia As Double, ImporteTransferenciaD As Double
        Dim CostoPromedio As Double = 0, ImporteCompraD As Double = 0, ImporteVentaD As Double = 0
        Dim FechaIni2 As Date


        'CostoPromedio = CostoPromedioKardexBodega(CodigoProducto, FechaCompraFin, CodBodega)

        ImporteCompra = 0
        ImporteCompraD = 0
        UnidadComprada = 0
        MontoEntrada = 0
        MontoEntradaD = 0
        ImporteTransferencia = 0
        ImporteTransferenciaD = 0
        ImporteVenta = 0
        ImporteVentaD = 0
        '//////////////////////////////////BUSCO EL TOTAL DE LAS COMPRAS CORDOBAS////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad ) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad / TasaCambio.MontoTasa) AS ImporteD,Compras.MonedaCompra AS Moneda FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra BETWEEN CONVERT(DATETIME, '" & FechaCompraIni & "', 102) AND CONVERT(DATETIME, '" & FechaCompraFin & "', 102))  AND (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') GROUP BY Compras.MonedaCompra HAVING (Compras.MonedaCompra = N'Cordobas')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Compras")
        If DataSet.Tables("Compras").Rows.Count <> 0 Then
            UnidadComprada = DataSet.Tables("Compras").Rows(0)("Cantidad")
            ImporteCompra = DataSet.Tables("Compras").Rows(0)("Importe")
            ImporteCompraD = DataSet.Tables("Compras").Rows(0)("ImporteD")
        End If

        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Neto * TasaCambio.MontoTasa) AS Precio_Neto, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad * TasaCambio.MontoTasa) AS Importe, SUM(Detalle_Compras.Precio_Neto * Detalle_Compras.Cantidad) AS ImporteD,Compras.MonedaCompra AS Moneda FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra BETWEEN CONVERT(DATETIME, '" & FechaCompraIni & "', 102) AND CONVERT(DATETIME, '" & FechaCompraFin & "', 102))  AND (Detalle_Compras.Tipo_Compra = 'Mercancia Recibida') GROUP BY Compras.MonedaCompra HAVING (Compras.MonedaCompra = 'Dolares')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "ComprasD")
        If DataSet.Tables("ComprasD").Rows.Count <> 0 Then
            UnidadComprada = DataSet.Tables("ComprasD").Rows(0)("Cantidad") + UnidadComprada
            ImporteCompra = DataSet.Tables("ComprasD").Rows(0)("Importe") + ImporteCompra
            ImporteCompraD = DataSet.Tables("ComprasD").Rows(0)("ImporteD") + ImporteCompraD
        End If

        'UnidadTransferencia = 0
        'ImporteTransferencia = 0
        ''//////////////////////////////////BUSCO EL TOTAL DE LAS TRANSFERENCIAS RECIBIDAS////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Precio_Neto) AS Precio_Neto, SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario) AS Importe, SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Unitario / TasaCambio.MontoTasa) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa " & _
        '              "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra BETWEEN CONVERT(DATETIME, '" & FechaCompraIni & "', 102) AND CONVERT(DATETIME,'" & FechaCompraFin & "', 102))  GROUP BY Detalle_Compras.Tipo_Compra HAVING (Detalle_Compras.Tipo_Compra = 'Transferencia Recibida') OR (Detalle_Compras.Tipo_Compra = 'Transferencia Recibida') "
        'DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        'DataAdapter.Fill(DataSet, "TransferenciaRecibida")
        'If DataSet.Tables("TransferenciaRecibida").Rows.Count <> 0 Then
        '    UnidadTransferencia = DataSet.Tables("TransferenciaRecibida").Rows(0)("Cantidad")
        '    ImporteTransferencia = DataSet.Tables("TransferenciaRecibida").Rows(0)("Importe")   'DataSet.Tables("TransferenciaRecibida").Rows(0)("Importe")
        '    ImporteTransferenciaD = DataSet.Tables("TransferenciaRecibida").Rows(0)("ImporteD")
        'End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  FACTURAS//////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Precio_Neto) AS Precio_Neto, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) AS Importe, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa  " & _
                      "WHERE (Facturas.Fecha_Factura BETWEEN CONVERT(DATETIME, '" & FechaCompraIni & "', 102) AND CONVERT(DATETIME, '" & FechaCompraFin & "', 102))  AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = N'Devolucion de Venta')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            ImporteVenta = DataSet.Tables("DevolucionFacturas").Rows(0)("Importe") '* CostoPromedio DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
            ImporteVentaD = DataSet.Tables("DevolucionFacturas").Rows(0)("ImporteD") '* CostoPromedioDolar
        End If


        BuscaCompra = UnidadComprada + DevolucionFactura + UnidadTransferencia
        MontoEntrada = ImporteCompra + ImporteTransferencia + ImporteVenta
        MontoEntradaD = ImporteCompraD + ImporteTransferenciaD + ImporteVentaD

    End Function


    Public Function BuscaVentaAcumulada(ByVal CodigoProducto As String, ByVal FechaCompraFin As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, UnidadComprada As Double
        Dim Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0



        ImporteVenta = 0
        UnidadComprada = 0
        '//////////////////////////////////BUSCO EL TOTAL DE LAS VENTAS////////////////////////////////////////////////////////////////////

        DataSet.Tables.Clear()
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Importe) AS Importe, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN  Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  WHERE  (Facturas.Fecha_Factura < CONVERT(DATETIME, '" & FechaCompraFin & "', 102)) GROUP BY Facturas.Tipo_Factura, Detalle_Facturas.Cod_Producto HAVING (Facturas.Tipo_Factura = N'Factura') AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') OR (Facturas.Tipo_Factura = N'Transferencia Enviada') "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Ventas")
        If DataSet.Tables("Ventas").Rows.Count <> 0 Then
            UnidadComprada = DataSet.Tables("Ventas").Rows(0)("Cantidad")
            ImporteVenta = DataSet.Tables("Ventas").Rows(0)("Importe")
        End If



        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  COMPRAS//////////////////////////////////////////////////////////////////////
        'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura WHERE (Detalle_Facturas.Tipo_Factura = 'Devolucion de Venta') AND (Detalle_Facturas.Fecha_Factura >= CONVERT(DATETIME, '" & FechaCompraFin & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') GROUP BY Facturas.Cod_Bodega  "
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Importe) AS Importe FROM  Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra < CONVERT(DATETIME, '" & FechaCompraFin & "', 102)) GROUP BY Detalle_Compras.Tipo_Compra, Compras.Cod_Bodega HAVING (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra') "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            ImporteCompra = ImporteCompra - DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
        End If


        BuscaVentaAcumulada = UnidadComprada + DevolucionFactura


    End Function


    Public Function BuscaVentaBodega(ByVal CodigoProducto As String, ByVal FechaCompraIni As Date, ByVal FechaCompraFin As Date, ByVal CodBodega As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, UnidadComprada As Double
        Dim Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0, SalidaBodega As Double = 0, UnidadTransferencia As Double = 0, ImporteTransferencia As Double = 0
        Dim ImporteCompraD As Double = 0, ImporteTransferenciaD As Double = 0, ImporteVentaD As Double = 0, ImporteSalida As Double = 0, ImporteSalidaD As Double = 0
        Dim ImporteDevFacturaD As Double = 0


        ImporteVenta = 0
        UnidadComprada = 0
        MontoSalida = 0
        ImporteDevFactura = 0
        ImporteCompra = 0
        '//////////////////////////////////BUSCO EL TOTAL DE LAS VENTAS////////////////////////////////////////////////////////////////////

        DataSet.Tables.Clear()
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) AS Importe,SUM((Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa  " & _
                      "WHERE (Facturas.Fecha_Factura BETWEEN CONVERT(DATETIME, '" & Format(FechaCompraIni, "yyyy-MM-dd") & "', 102) AND CONVERT(DATETIME, '" & Format(FechaCompraFin, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodBodega & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Factura') "  'AND (SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) <> 0)
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Ventas")
        If DataSet.Tables("Ventas").Rows.Count <> 0 Then
            UnidadComprada = DataSet.Tables("Ventas").Rows(0)("Cantidad")
            ImporteCompra = DataSet.Tables("Ventas").Rows(0)("Importe")
            ImporteCompraD = DataSet.Tables("Ventas").Rows(0)("ImporteD")
        End If

        ImporteTransferencia = 0
        UnidadTransferencia = 0
        '//////////////////////////////////BUSCO EL TOTAL DE LAS TRANSFERENCIA ENVIADAS////////////////////////////////////////////////////////////////////

        DataSet.Tables.Clear()
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Importe,SUM((Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa  " & _
                      "WHERE (Facturas.Fecha_Factura BETWEEN CONVERT(DATETIME, '" & Format(FechaCompraIni, "yyyy-MM-dd") & "', 102) AND CONVERT(DATETIME, '" & Format(FechaCompraFin, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodBodega & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Transferencia Enviada') "  'AND (SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) <> 0)

        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "TransEnviada")
        If DataSet.Tables("TransEnviada").Rows.Count <> 0 Then
            UnidadTransferencia = DataSet.Tables("TransEnviada").Rows(0)("Cantidad")
            ImporteTransferencia = DataSet.Tables("TransEnviada").Rows(0)("Importe")
            ImporteTransferenciaD = DataSet.Tables("TransEnviada").Rows(0)("ImporteD")
        End If


        SalidaBodega = 0
        '//////////////////////////////////BUSCO EL TOTAL DE LAS SALIDAS////////////////////////////////////////////////////////////////////

        DataSet.Tables.Clear()
        'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Costo_Unitario) AS Importe, Facturas.Tipo_Factura FROM  Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
        '              "WHERE (Facturas.Fecha_Factura BETWEEN CONVERT(DATETIME, '" & FechaCompraIni & "', 102) AND CONVERT(DATETIME, '" & FechaCompraFin & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodBodega & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Salida Bodega') "
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Importe,SUM((Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa  " & _
                      "WHERE (Facturas.Fecha_Factura BETWEEN CONVERT(DATETIME, '" & Format(FechaCompraIni, "yyyy-MM-dd") & "', 102) AND CONVERT(DATETIME, '" & Format(FechaCompraFin, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodBodega & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Salida Bodega') " 'AND (SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) <> 0)

        'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) AS Importe,SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario / TasaCambio.MontoTasa) AS ImporteD FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa WHERE (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Facturas.Tipo_Factura = 'Salida Bodega') AND (Facturas.Cod_Bodega = '" & CodBodega & "') AND (Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario <> 0)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Salida")
        If DataSet.Tables("Salida").Rows.Count <> 0 Then
            SalidaBodega = DataSet.Tables("Salida").Rows(0)("Cantidad")
            ImporteSalida = DataSet.Tables("Salida").Rows(0)("Importe")
            ImporteSalidaD = DataSet.Tables("Salida").Rows(0)("ImporteD")
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  COMPRAS//////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) AS Importe, SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra BETWEEN CONVERT(DATETIME, '" & Format(FechaCompraIni, "yyyy-MM-dd") & "', 102) AND CONVERT(DATETIME, '" & Format(FechaCompraFin, "yyyy-MM-dd") & "', 102)) AND (Compras.Cod_Bodega = '" & CodBodega & "') GROUP BY Detalle_Compras.Tipo_Compra HAVING (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra') "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            ImporteDevFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
            ImporteDevFacturaD = DataSet.Tables("DevolucionFacturas").Rows(0)("ImporteD")
        End If


        BuscaVentaBodega = UnidadComprada + DevolucionFactura + SalidaBodega + UnidadTransferencia
        MontoSalida = ImporteCompra + ImporteDevFactura + ImporteSalida + ImporteTransferencia
        MontoSalidaD = ImporteCompraD + ImporteDevFacturaD + ImporteSalidaD + ImporteTransferenciaD

    End Function

    Public Function BuscaVenta(ByVal CodigoProducto As String, ByVal FechaCompraIni As Date, ByVal FechaCompraFin As Date) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, UnidadComprada As Double
        Dim Existencia As Double = 0, SqlConsulta As String, DevolucionCompra As Double = 0
        Dim UnidadFacturada As Double = 0, DevolucionFactura As Double = 0, SalidaBodega As Double = 0, UnidadTransferencia As Double = 0, ImporteTransferencia As Double = 0
        Dim ImporteCompraD As Double = 0, ImporteTransferenciaD As Double = 0, ImporteVentaD As Double = 0, ImporteSalida As Double = 0, ImporteSalidaD As Double = 0
        Dim ImporteDevFacturaD As Double = 0


        ImporteVenta = 0
        UnidadComprada = 0
        MontoSalida = 0
        ImporteDevFactura = 0
        ImporteCompra = 0
        '//////////////////////////////////BUSCO EL TOTAL DE LAS VENTAS////////////////////////////////////////////////////////////////////

        DataSet.Tables.Clear()
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) AS Importe,SUM((Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa  " & _
                      "WHERE (Facturas.Fecha_Factura BETWEEN CONVERT(DATETIME, '" & Format(FechaCompraIni, "yyyy-MM-dd") & "', 102) AND CONVERT(DATETIME, '" & Format(FechaCompraFin, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "')  GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Factura') "  'AND (SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) <> 0)
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Ventas")
        If DataSet.Tables("Ventas").Rows.Count <> 0 Then
            UnidadComprada = DataSet.Tables("Ventas").Rows(0)("Cantidad")
            If Not IsDBNull(DataSet.Tables("Ventas").Rows(0)("Importe")) Then
                ImporteCompra = DataSet.Tables("Ventas").Rows(0)("Importe")
            Else
                ImporteCompra = 0
                MsgBox("Producto sin Costo: " & CodigoProducto, MsgBoxStyle.Critical, "Zeus Facturacion")
            End If
            If Not IsDBNull(DataSet.Tables("Ventas").Rows(0)("ImporteD")) Then
                ImporteCompraD = DataSet.Tables("Ventas").Rows(0)("ImporteD")
            Else
                ImporteCompraD = 0
            End If
        End If

        'ImporteTransferencia = 0
        'UnidadTransferencia = 0
        ''//////////////////////////////////BUSCO EL TOTAL DE LAS TRANSFERENCIA ENVIADAS////////////////////////////////////////////////////////////////////

        'DataSet.Tables.Clear()
        'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Importe,SUM((Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa  " & _
        '              "WHERE (Facturas.Fecha_Factura BETWEEN CONVERT(DATETIME, '" & FechaCompraIni & "', 102) AND CONVERT(DATETIME, '" & FechaCompraFin & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "')  GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Transferencia Enviada') "  'AND (SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) <> 0)

        'DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        'DataAdapter.Fill(DataSet, "TransEnviada")
        'If DataSet.Tables("TransEnviada").Rows.Count <> 0 Then
        '    UnidadTransferencia = DataSet.Tables("TransEnviada").Rows(0)("Cantidad")
        '    ImporteTransferencia = DataSet.Tables("TransEnviada").Rows(0)("Importe")
        '    ImporteTransferenciaD = DataSet.Tables("TransEnviada").Rows(0)("ImporteD")
        'End If


        SalidaBodega = 0
        '//////////////////////////////////BUSCO EL TOTAL DE LAS SALIDAS////////////////////////////////////////////////////////////////////

        DataSet.Tables.Clear()
        'SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad*Detalle_Facturas.Costo_Unitario) AS Importe, Facturas.Tipo_Factura FROM  Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura  " & _
        '              "WHERE (Facturas.Fecha_Factura BETWEEN CONVERT(DATETIME, '" & FechaCompraIni & "', 102) AND CONVERT(DATETIME, '" & FechaCompraFin & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') AND (Facturas.Cod_Bodega = '" & CodBodega & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Salida Bodega') "
        SqlConsulta = "SELECT SUM(Detalle_Facturas.Cantidad) AS Cantidad, SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) AS Importe,SUM((Detalle_Facturas.Cantidad * Detalle_Facturas.Precio_Unitario) / TasaCambio.MontoTasa) AS ImporteD, Facturas.Tipo_Factura FROM Detalle_Facturas INNER JOIN Facturas ON Detalle_Facturas.Numero_Factura = Facturas.Numero_Factura AND Detalle_Facturas.Fecha_Factura = Facturas.Fecha_Factura AND Detalle_Facturas.Tipo_Factura = Facturas.Tipo_Factura INNER JOIN TasaCambio ON Facturas.Fecha_Factura = TasaCambio.FechaTasa  " & _
                      "WHERE (Facturas.Fecha_Factura BETWEEN CONVERT(DATETIME, '" & Format(FechaCompraIni, "yyyy-MM-dd") & "', 102) AND CONVERT(DATETIME, '" & Format(FechaCompraFin, "yyyy-MM-dd") & "', 102)) AND (Detalle_Facturas.Cod_Producto = '" & CodigoProducto & "') GROUP BY Facturas.Tipo_Factura HAVING (Facturas.Tipo_Factura = 'Salida Bodega') " 'AND (SUM(Detalle_Facturas.Cantidad * Detalle_Facturas.Costo_Unitario) <> 0)


        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "Salida")
        If DataSet.Tables("Salida").Rows.Count <> 0 Then
            SalidaBodega = DataSet.Tables("Salida").Rows(0)("Cantidad")
            ImporteSalida = DataSet.Tables("Salida").Rows(0)("Importe")
            ImporteSalidaD = DataSet.Tables("Salida").Rows(0)("ImporteD")
        End If

        '////////////////////////////////////BUSCO EL TOTAL DE LA DEVOLUCION DE LAS  COMPRAS//////////////////////////////////////////////////////////////////////
        SqlConsulta = "SELECT SUM(Detalle_Compras.Cantidad) AS Cantidad, SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto) AS Importe, SUM(Detalle_Compras.Cantidad * Detalle_Compras.Precio_Neto / TasaCambio.MontoTasa) AS ImporteD FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra INNER JOIN TasaCambio ON Compras.Fecha_Compra = TasaCambio.FechaTasa  " & _
                      "WHERE (Detalle_Compras.Cod_Producto = '" & CodigoProducto & "') AND (Detalle_Compras.Fecha_Compra BETWEEN CONVERT(DATETIME, '" & Format(FechaCompraIni, "yyyy-MM-dd") & "', 102) AND CONVERT(DATETIME, '" & Format(FechaCompraFin, "yyyy-MM-dd") & "', 102)) GROUP BY Detalle_Compras.Tipo_Compra HAVING (Detalle_Compras.Tipo_Compra = 'Devolucion de Compra') "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlConsulta, MiConexion)
        DataAdapter.Fill(DataSet, "DevolucionFacturas")
        If DataSet.Tables("DevolucionFacturas").Rows.Count <> 0 Then
            DevolucionFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Cantidad")
            ImporteDevFactura = DataSet.Tables("DevolucionFacturas").Rows(0)("Importe")
            ImporteDevFacturaD = DataSet.Tables("DevolucionFacturas").Rows(0)("ImporteD")
        End If


        BuscaVenta = UnidadComprada + DevolucionFactura + SalidaBodega + UnidadTransferencia
        MontoSalida = ImporteCompra + ImporteDevFactura + ImporteSalida + ImporteTransferencia
        MontoSalidaD = ImporteCompraD + ImporteDevFacturaD + ImporteSalidaD + ImporteTransferenciaD

    End Function
    Public Function ExistenciaProductoFecha(ByVal CodigoProducto As String, ByVal FechaBusca As Date) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, SqlString As String
        Dim Existencia As Double, iPosicionFila As Double, CodigoBodega As String, Fecha As String

        ExistenciaBodega = 0
        Fecha = Format(FechaBusca, "yyyy-MM-dd")

        '//////////////////////////////////////////////////////////////////////////////////////////////
        '///////////////BUSCO LAS BODEGAS DEL PRODUCTO/////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////

        SqlString = "SELECT  DetalleBodegas.Cod_Bodegas, Bodegas.Nombre_Bodega,DetalleBodegas.Existencia FROM DetalleBodegas INNER JOIN Bodegas ON DetalleBodegas.Cod_Bodegas = Bodegas.Cod_Bodega  " & _
                    "WHERE (DetalleBodegas.Cod_Productos = '" & CodigoProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Bodegas")


        '//////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////BUSCO LA EXISTENCIA DE ESTE PRODUCTO PARA CADA BODEGA//////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////
        Existencia = 0
        iPosicionFila = 0
        Do While iPosicionFila < (DataSet.Tables("Bodegas").Rows.Count)
            My.Application.DoEvents()
            CodigoBodega = DataSet.Tables("Bodegas").Rows(iPosicionFila)("Cod_Bodegas")

            ExistenciaBodega = BuscaExistenciaBodega(CodigoProducto, CodigoBodega)
            Existencia = Existencia + ExistenciaBodega


            iPosicionFila = iPosicionFila + 1
        Loop

        ExistenciaProductoFecha = Existencia
    End Function
    Public Function ExistenciaProducto(ByVal CodigoProducto As String) As Double
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, SqlString As String
        Dim Existencia As Double, iPosicionFila As Double, CodigoBodega As String

        ExistenciaBodega = 0

        '//////////////////////////////////////////////////////////////////////////////////////////////
        '///////////////BUSCO LAS BODEGAS DEL PRODUCTO/////////////////////////////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////

        SqlString = "SELECT  DetalleBodegas.Cod_Bodegas, Bodegas.Nombre_Bodega,DetalleBodegas.Existencia FROM DetalleBodegas INNER JOIN Bodegas ON DetalleBodegas.Cod_Bodegas = Bodegas.Cod_Bodega  " & _
                    "WHERE (DetalleBodegas.Cod_Productos = '" & CodigoProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Bodegas")


        '//////////////////////////////////////////////////////////////////////////////////////////////////////
        '////////////////////////BUSCO LA EXISTENCIA DE ESTE PRODUCTO PARA CADA BODEGA//////////////////////////
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////
        Existencia = 0
        iPosicionFila = 0
        Do While iPosicionFila < (DataSet.Tables("Bodegas").Rows.Count)
            My.Application.DoEvents()
            CodigoBodega = DataSet.Tables("Bodegas").Rows(iPosicionFila)("Cod_Bodegas")

            ExistenciaBodega = BuscaExistenciaBodega(CodigoProducto, CodigoBodega)
            Existencia = Existencia + ExistenciaBodega


            iPosicionFila = iPosicionFila + 1
        Loop

        ExistenciaProducto = Existencia
    End Function
    Public Function BuscaProducto(ByVal CodProducto As String, ByVal CodBodega As String) As Boolean
        Dim SqlBodega As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter

        DataSet.Tables.Clear()
        SqlBodega = "SELECT Cod_Bodegas, Cod_Productos, Existencia FROM DetalleBodegas WHERE (Cod_Bodegas = '" & CodBodega & "') AND (Cod_Productos = '" & CodProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlBodega, MiConexion)
        DataAdapter.Fill(DataSet, "Bodega")
        If DataSet.Tables("Bodega").Rows.Count <> 0 Then
            BuscaProducto = True
        Else
            BuscaProducto = False
        End If

    End Function
    Public Function BuscaProductoIdFactura(ByVal NumeroFactura As String, ByVal FechaFactura As Date, ByVal TipoFactura As String, ByVal CodigoProducto As String, ByVal Cantidad As Double) As Double
        Dim SqlBodega As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, Registro As Double

        DataSet.Tables.Clear()
        SqlBodega = "SELECT  * FROM Detalle_Facturas " & _
                    "WHERE (Numero_Factura = '" & NumeroFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Format(FechaFactura, "yyyy-MM-dd") & "', 102)) AND (Tipo_Factura = '" & TipoFactura & "') AND (Cod_Producto = '" & CodigoProducto & "') AND (Cantidad = " & Cantidad & ") ORDER BY id_Detalle_Factura"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlBodega, MiConexion)
        DataAdapter.Fill(DataSet, "Detalle")
        Registro = DataSet.Tables("Detalle").Rows.Count
        Registro = Registro - 1
        If DataSet.Tables("Detalle").Rows.Count <> 0 Then
            '//////////////////BUSCO EL ULTIMO REGISTRO ////////////////////////////////
            BuscaProductoIdFactura = DataSet.Tables("Detalle").Rows(Registro)("id_Detalle_Factura")
        Else

            BuscaProductoIdFactura = -1
        End If

    End Function

    Public Sub GrabaEncabezadoLiquidacion(ByVal ConsecutivoCompra As String)
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim TotalCosto As Double, TotalFob As Double

        TotalCosto = FrmLiquidacion.TxtTotalCosto.Text
        TotalFob = FrmLiquidacion.TxtTotalFob.Text


        If FrmLiquidacion.TxtNumeroEnsamble.Text = "-----0-----" Then
            '/////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "INSERT INTO [Liquidacion] ([Numero_Liquidacion],[Fecha_Liquidacion],[Cod_Proveedor],[Nombre_Proveedor],[Apellido_Proveedor],[TotalFOB],[TotalCosto],[Seguro],[Transporte],[Almacen],[Fletes],[CodBodega],[MonedaLiquidacion],[MonedaImpuestos],[GtoAgenteAduana],[GtoCustodio],[GtoAduana],[GtoOtros],[GtoFletesInternos],[TasaCambio]) " & _
                         "VALUES('" & ConsecutivoCompra & "','" & FrmLiquidacion.DTPFecha.Text & "','" & FrmLiquidacion.TxtCodigoProveedor.Text & "','" & FrmLiquidacion.TxtNombres.Text & "','" & FrmLiquidacion.TxtApellidos.Text & "'," & TotalFob & "," & TotalCosto & "," & CDbl(FrmLiquidacion.TxtSeguro.Text) & "," & CDbl(FrmLiquidacion.TxtTransporte.Text) & "," & CDbl(FrmLiquidacion.TxtAlmacen.Text) & "," & CDbl(FrmLiquidacion.TxtFletes.Text) & ",'" & FrmLiquidacion.CboCodigoBodega.Text & "','" & FrmLiquidacion.CmbMoneda.Text & "','" & FrmLiquidacion.CmbImpuesto.Text & "'," & CDbl(FrmLiquidacion.TxtAgente.Text) & "," & CDbl(FrmLiquidacion.TxtCustodio.Text) & "," & CDbl(FrmLiquidacion.TxtGastosAduana.Text) & "," & CDbl(FrmLiquidacion.TxtOtrosGastos.Text) & "," & CDbl(FrmLiquidacion.TxtFletesInternos.Text) & "," & CDbl(FrmLiquidacion.TxtTasaCambio.Text) & ")"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "UPDATE [Liquidacion]  SET [Cod_Proveedor] = '" & FrmLiquidacion.TxtCodigoProveedor.Text & "',[Nombre_Proveedor] = '" & FrmLiquidacion.TxtNombres.Text & "',[Apellido_Proveedor] = '" & FrmLiquidacion.TxtApellidos.Text & "',[TotalFOB] = " & TotalFob & ",[TotalCosto] = " & TotalCosto & ",[Seguro] = " & CDbl(FrmLiquidacion.TxtSeguro.Text) & ",[Transporte] = " & CDbl(FrmLiquidacion.TxtTransporte.Text) & ",[Almacen] = " & CDbl(FrmLiquidacion.TxtAlmacen.Text) & ",[Fletes] = " & CDbl(FrmLiquidacion.TxtFletes.Text) & ",[CodBodega] = '" & FrmLiquidacion.CboCodigoBodega.Text & "',[GtoAgenteAduana]= " & CDbl(FrmLiquidacion.TxtAgente.Text) & ",[GtoCustodio]= " & CDbl(FrmLiquidacion.TxtCustodio.Text) & ",[GtoAduana]= " & CDbl(FrmLiquidacion.TxtGastosAduana.Text) & ",[GtoOtros]= " & CDbl(FrmLiquidacion.TxtOtrosGastos.Text) & ",[GtoFletesInternos]= " & CDbl(FrmLiquidacion.TxtFletesInternos.Text) & ",[TasaCambio]= " & CDbl(FrmLiquidacion.TxtTasaCambio.Text) & " " & _
                         ",[MonedaLiquidacion] = '" & FrmLiquidacion.CmbMoneda.Text & "',[MonedaImpuestos] = '" & FrmLiquidacion.CmbImpuesto.Text & "' " & _
                         " WHERE  (Fecha_Liquidacion = CONVERT(DATETIME, '" & Format(FrmLiquidacion.DTPFecha.Value, "yyyy-MM-dd") & "', 102)) AND (Numero_Liquidacion = '" & ConsecutivoCompra & "') "

            '"WHERE (Numero_Liquidacion = '" & ConsecutivoCompra & "') AND (Fecha_Liquidacion = " & FrmLiquidacion.DTPFecha.Text & ")"

            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If
    End Sub
    Public Sub GrabaDetalleImpuestosLiquidacion(ByVal ConsecutivoLiquidacion As String, ByVal FechaLiquidacion As Date, ByVal CodProducto As String, ByVal CodIva As String, ByVal Monto As Double)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, Fecha As String
        Dim SqlString As String

        Fecha = Format(FechaLiquidacion, "yyyy-MM-dd")
        'Fecha2 = Format(FrmArqueo.DTFecha.Value, "dd/MM/yyyy")


        MiConexion.Close()

        SqlString = "SELECT * FROM ImpuestosLiquidacion WHERE (Numero_Liquidacion = '" & ConsecutivoLiquidacion & "') AND (Fecha_Liquidacion = CONVERT(DATETIME, '" & Format(FechaLiquidacion, "yyyy-MM-dd") & "', 102)) AND (Cod_Producto = '" & CodProducto & "') AND (Cod_Iva = '" & CodIva & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "ImpuestosLiquida")
        If DataSet.Tables("ImpuestosLiquida").Rows.Count = 0 Then

            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL DETALLE CHEQUES/////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "INSERT INTO [ImpuestosLiquidacion] ([Numero_Liquidacion] ,[Fecha_Liquidacion],[Cod_Producto],[Cod_Iva],[Monto]) " & _
                         "VALUES('" & ConsecutivoLiquidacion & "','" & Format(FechaLiquidacion, "dd/MM/yyyy") & "','" & CodProducto & "','" & CodIva & "'," & Monto & ")"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL ENCABEZADO DEL ARQUEO///////////////////////////////////
            '////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "UPDATE [ImpuestosLiquidacion] SET [Monto] = " & Monto & " " & _
                         "WHERE (Numero_Liquidacion = '" & ConsecutivoLiquidacion & "') AND (Fecha_Liquidacion = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Cod_Producto = '" & CodProducto & "') AND (Cod_Iva = '" & CodIva & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If

    End Sub



    Public Sub GrabaDetalleLiquidacion(ByVal ConsecutivoLiquidacion As String, ByVal FechaLiquidacion As Date, ByVal CodProducto As String, ByVal Cantidad As Double, ByVal PrecioUnitario As Double, ByVal Descuento As Double, ByVal FOB As Double, ByVal PrecioCosto As Double, ByVal TasaCambio As Double, ByVal GtoImpuesto As Double)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), SqlString As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter

        'Fecha = Format(FrmArqueo.DTFecha.Value, "yyyy-MM-dd")
        'Fecha2 = Format(FrmArqueo.DTFecha.Value, "dd/MM/yyyy")


        MiConexion.Close()

        SqlString = "SELECT * FROM Detalle_Liquidacion WHERE (Numero_Liquidacion = '" & ConsecutivoLiquidacion & "') AND (Fecha_Liquidacion = CONVERT(DATETIME, '" & Format(FechaLiquidacion, "yyyy-MM-dd") & "', 102)) AND (Cod_Producto ='" & CodProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleLiquidacion")
        If DataSet.Tables("DetalleLiquidacion").Rows.Count = 0 Then

            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////AGREGO EL DETALLE CHEQUES/////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "INSERT INTO [Detalle_Liquidacion] ([Numero_Liquidacion],[Fecha_Liquidacion],[Cod_Producto],[Cantidad],[Precio_Compra],[Gasto_Compra],[FOB],[Precio_Costo],[TasaCambio],[Gasto_Impuesto]) " & _
                         "VALUES('" & ConsecutivoLiquidacion & "','" & FechaLiquidacion & "','" & CodProducto & "','" & Cantidad & "','" & PrecioUnitario & "','" & Descuento & "','" & FOB & "','" & PrecioCosto & "','" & TasaCambio & "'," & GtoImpuesto & ")"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL ENCABEZADO DEL ARQUEO///////////////////////////////////
            '////////////////////////////////////////////////////////////////////////////////////////////////
            SqlCompras = "UPDATE [Detalle_Liquidacion] SET [Cantidad] = '" & Cantidad & "',[Precio_Compra] = '" & PrecioUnitario & "',[Descuento] = '" & Descuento & "',[FOB] = '" & FOB & "',[Precio_Costo] = '" & PrecioCosto & "',[TasaCambio] = '" & TasaCambio & "',[Gasto_Impuesto] = " & GtoImpuesto & " " & _
                         "WHERE (Numero_Liquidacion = '" & ConsecutivoLiquidacion & "') AND  (Fecha_Liquidacion = CONVERT(DATETIME, '" & Format(FechaLiquidacion, "yyyy-MM-dd") & "', 102)) AND  (Cod_Producto = '" & CodProducto & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If

    End Sub

    Public Sub GrabaDetalleCompraLiquidacion(ByVal ConsecutivoCompra As String, ByVal CodProducto As String, ByVal PrecioUnitario As Double, ByVal Descuento As Double, ByVal PrecioNeto As Double, ByVal Importe As Double, ByVal Cantidad As Double, ByVal Moneda As String, ByVal FechaCompra As Date)
        Dim Sqldetalle As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, TasaCambio As String
        Dim Fecha As String, MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, MonedaCompra As String, MonedaProducto As String, Descripcion As String

        MonedaCompra = Moneda
        MonedaProducto = "Cordobas"
        TasaCambio = 0

        If MonedaCompra = "Cordobas" Then
            If MonedaProducto = "Cordobas" Then
                TasaCambio = 1
            Else
                If BuscaTasaCambio(FechaCompra) <> 0 Then
                    TasaCambio = (1 / BuscaTasaCambio(FechaCompra))
                End If
            End If
        ElseIf MonedaCompra = "Dolares" Then
            If MonedaProducto = "Cordobas" Then
                TasaCambio = BuscaTasaCambio(FechaCompra)
            Else
                TasaCambio = 1
            End If
        End If

        Sqldetalle = "SELECT  *  FROM Productos WHERE (Cod_Productos = '" & CodProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(Sqldetalle, MiConexion)
        DataAdapter.Fill(DataSet, "Consulta")
        If Not DataSet.Tables("Consulta").Rows.Count = 0 Then
            Descripcion = DataSet.Tables("Consulta").Rows(0)("Descripcion_Producto")
        End If


        Fecha = Format(FechaCompra, "yyyy-MM-dd")

        Sqldetalle = "SELECT *  FROM Detalle_Compras WHERE (Numero_Compra = '" & ConsecutivoCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = 'Mercancia Recibida') AND (Cod_Producto = '" & CodProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(Sqldetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleCompra")
        If Not DataSet.Tables("DetalleCompra").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            MiConexion.Close()
            SqlUpdate = "UPDATE [Detalle_Compras] SET [Cantidad] = " & Cantidad & " ,[Precio_Unitario] = " & PrecioUnitario & ",[Descuento] = " & Descuento & " ,[Precio_Neto] = " & PrecioNeto & ",[Importe] = " & Importe & ",[TasaCambio] = " & TasaCambio & " " & _
                        "WHERE (Numero_Compra = '" & ConsecutivoCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = 'Mercancia Recibida') AND (Cod_Producto = '" & CodProducto & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            MiConexion.Close()
            SqlUpdate = "INSERT INTO [Detalle_Compras] ([Numero_Compra],[Fecha_Compra],[Tipo_Compra],[Cod_Producto],[Descripcion_Producto],[Cantidad],[Precio_Unitario],[Descuento],[Precio_Neto],[Importe],[TasaCambio]) " & _
            "VALUES ('" & ConsecutivoCompra & "','" & Format(FechaCompra, "dd/MM/yyyy") & "','Mercancia Recibida','" & CodProducto & "','" & Descripcion & "' ," & Cantidad & "," & PrecioUnitario & "," & Descuento & " ," & PrecioNeto & "," & Importe & "," & TasaCambio & ")"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub

    Public Sub GrabaDetalleCompraInventarioFisico(ByVal ConsecutivoCompra As String, ByVal CodProducto As String, ByVal PrecioUnitario As Double, ByVal Descuento As Double, ByVal PrecioNeto As Double, ByVal Importe As Double, ByVal Cantidad As Double, ByVal Moneda As String, ByVal FechaCompra As Date, ByVal NumeroLote As String, ByVal FechaVence As Date)
        Dim Sqldetalle As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, TasaCambio As String
        Dim Fecha As String, MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, MonedaCompra As String, MonedaProducto As String
        Dim Descripcion As String

        MonedaCompra = Moneda
        MonedaProducto = "Cordobas"
        TasaCambio = 0

        If MonedaCompra = "Cordobas" Then
            If MonedaProducto = "Cordobas" Then
                TasaCambio = 1
            Else
                If BuscaTasaCambio(FechaCompra) <> 0 Then
                    TasaCambio = (1 / BuscaTasaCambio(FechaCompra))
                End If
            End If
        ElseIf MonedaCompra = "Dolares" Then
            If MonedaProducto = "Cordobas" Then
                TasaCambio = BuscaTasaCambio(FechaCompra)
            Else
                TasaCambio = 1
            End If
        End If

        Sqldetalle = "SELECT  *  FROM Productos WHERE (Cod_Productos = '" & CodProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(Sqldetalle, MiConexion)
        DataAdapter.Fill(DataSet, "Consulta")
        If Not DataSet.Tables("Consulta").Rows.Count = 0 Then
            Descripcion = DataSet.Tables("Consulta").Rows(0)("Descripcion_Producto")
        End If


        Fecha = Format(FechaCompra, "yyyy-MM-dd")

        Sqldetalle = "SELECT *  FROM Detalle_Compras WHERE (Numero_Compra = '" & ConsecutivoCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = 'Mercancia Recibida') AND (Cod_Producto = '" & CodProducto & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(Sqldetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleCompra")
        If Not DataSet.Tables("DetalleCompra").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            MiConexion.Close()
            'SqlUpdate = "UPDATE [Detalle_Compras] SET [Cantidad] = " & Cantidad & " ,[Precio_Unitario] = " & PrecioUnitario & ",[Descuento] = " & Descuento & " ,[Precio_Neto] = " & PrecioNeto & ",[Importe] = " & Importe & ",[TasaCambio] = " & TasaCambio & ",[Numero_Lote] = '" & NumeroLote & "',[Fecha_Vence] = '" & Format(FechaVence, "dd/MM/yyyy") & "' " & _
            '            "WHERE (Numero_Compra = '" & ConsecutivoCompra & "') AND (Fecha_Compra = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Compra = 'Mercancia Recibida') AND (Cod_Producto = '" & CodProducto & "')"
            SqlUpdate = "INSERT INTO [Detalle_Compras] ([Numero_Compra],[Fecha_Compra],[Tipo_Compra],[Cod_Producto],[Descripcion_Producto],[Cantidad],[Precio_Unitario],[Descuento],[Precio_Neto],[Importe],[TasaCambio],[Numero_Lote],[Fecha_Vence]) " & _
                        "VALUES ('" & ConsecutivoCompra & "','" & Format(FechaCompra, "dd/MM/yyyy") & "','Mercancia Recibida','" & CodProducto & "','" & Descripcion & "' ," & Cantidad & "," & PrecioUnitario & "," & Descuento & " ," & PrecioNeto & "," & Importe & "," & TasaCambio & ",'" & NumeroLote & "','" & FechaVence & "')"

            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            MiConexion.Close()
            SqlUpdate = "INSERT INTO [Detalle_Compras] ([Numero_Compra],[Fecha_Compra],[Tipo_Compra],[Cod_Producto],[Cantidad],[Precio_Unitario],[Descuento],[Precio_Neto],[Importe],[TasaCambio],[Numero_Lote],[Fecha_Vence],[Descripcion_Producto]) " & _
            "VALUES ('" & ConsecutivoCompra & "','" & Format(FechaCompra, "dd/MM/yyyy") & "','Mercancia Recibida','" & CodProducto & "' ," & Cantidad & "," & PrecioUnitario & "," & Descuento & " ," & PrecioNeto & "," & Importe & "," & TasaCambio & ",'" & NumeroLote & "','" & FechaVence & "','" & Descripcion & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub


    Public Sub GrabaDetalleFacturaSalida(ByVal ConsecutivoFactura As String, ByVal CodProducto As String, ByVal NombreProductos As String, ByVal PrecioUnitario As Double, ByVal Descuento As Double, ByVal PrecioNeto As Double, ByVal Importe As Double, ByVal Cantidad As Double, ByVal Moneda As String, ByVal FechaFactura As Date, ByVal TipoFactura As String, ByVal CostoUnitario As Double)
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String, TasaCambio As Double
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, MonedaFactura As String, MonedaProducto As String
        Dim SqlDetalle As String

        NombreProductos = Replace(NombreProductos, "'", "")



        MonedaFactura = Moneda
        MonedaProducto = "Cordobas"

        If MonedaFactura = "Cordobas" Then
            If MonedaProducto = "Cordobas" Then
                TasaCambio = 1
            Else
                If BuscaTasaCambio(FechaFactura) <> 0 Then
                    'TasaCambio = (1 / BuscaTasaCambio(FrmFacturas.DTPFecha.Value))
                    TasaCambio = (1 / BuscaTasaCambio(Now))
                End If
            End If
        ElseIf MonedaFactura = "Dolares" Then
            If MonedaProducto = "Cordobas" Then
                'TasaCambio = BuscaTasaCambio(FrmFacturas.DTPFecha.Value)
                TasaCambio = BuscaTasaCambio(Now)
            Else
                TasaCambio = 1
            End If
        End If



        Fecha = Format(FechaFactura, "yyyy-MM-dd")

        'SqlUpdate = "INSERT INTO [Detalle_Facturas] ([Numero_Factura],[Fecha_Factura],[Tipo_Factura],[Cod_Producto],[Cantidad],[Precio_Unitario],[Descuento],[Precio_Neto],[Importe],[TasaCambio],[Descripcion_Producto]) " & _
        '"VALUES ('" & ConsecutivoFactura & "','" & FrmFacturas.DTPFecha.Value & "','" & FrmFacturas.CboTipoProducto.Text & "','" & CodProducto & "' ," & Cantidad & "," & PrecioUnitario & "," & Descuento & " ," & PrecioNeto & "," & Importe & "," & TasaCambio & ",'" & Descripcion_Producto & "')"
        'MiConexion.Open()
        'ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
        'iResultado = ComandoUpdate.ExecuteNonQuery
        'MiConexion.Close()

        'AND (Descripcion_Producto = '" & Descripcion_Producto & "')

        SqlDetalle = "SELECT *  FROM Detalle_Facturas WHERE (Numero_Factura = '" & ConsecutivoFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = '" & TipoFactura & "') AND (Cod_Producto = '" & CodProducto & "')  "   'AND (Descripcion_Producto = '" & Descripcion_Producto & "')
        DataAdapter = New SqlClient.SqlDataAdapter(SqlDetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleFactura")
        If Not DataSet.Tables("DetalleFactura").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////
            'AND (Descripcion_Producto = '" & Descripcion_Producto & "')
            SqlUpdate = "UPDATE [Detalle_Facturas] SET [Cantidad] = " & Cantidad & " ,[Precio_Unitario] = " & PrecioUnitario & ",[Descuento] = " & Descuento & " ,[Precio_Neto] = " & PrecioNeto & ",[Importe] = " & Importe & ",[Descripcion_Producto] = '" & NombreProductos & "',[TasaCambio]= " & TasaCambio & ",[Costo_Unitario]= " & CostoUnitario & " " & _
                        "WHERE (Numero_Factura = '" & ConsecutivoFactura & "') AND (Fecha_Factura = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Factura = '" & FrmFacturas.CboTipoProducto.Text & "') AND (Cod_Producto = '" & CodProducto & "')"   'AND (Descripcion_Producto = '" & Descripcion_Producto & "')
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else

            SqlUpdate = "INSERT INTO [Detalle_Facturas] ([Numero_Factura],[Fecha_Factura],[Tipo_Factura],[Cod_Producto],[Cantidad],[Precio_Unitario],[Descuento],[Precio_Neto],[Importe],[TasaCambio],[Descripcion_Producto],[Costo_Unitario]) " & _
            "VALUES ('" & ConsecutivoFactura & "','" & FechaFactura & "','" & TipoFactura & "','" & CodProducto & "' ," & Cantidad & "," & PrecioUnitario & "," & Descuento & " ," & PrecioNeto & "," & Importe & "," & TasaCambio & ",'" & NombreProductos & "'," & CostoUnitario & ")"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub

    Public Sub LimpiaLiquidacion()
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim SqlString As String, MiConexion As New SqlClient.SqlConnection(Conexion)

        FrmLiquidacion.CmdFacturar.Enabled = False
        FrmLiquidacion.TxtCodigoProveedor.Text = ""
        FrmLiquidacion.TxtNombres.Text = ""
        FrmLiquidacion.TxtApellidos.Text = ""
        FrmLiquidacion.CmbImpuesto.Text = "Codobas"
        FrmLiquidacion.CmbMoneda.Text = "Codobas"
        FrmLiquidacion.TxtNumeroEnsamble.Text = "-----0-----"
        FrmLiquidacion.TxtSeguro.Text = ""
        FrmLiquidacion.TxtTransporte.Text = ""
        FrmLiquidacion.TxtAlmacen.Text = ""
        FrmLiquidacion.TxtTotalCosto.Text = ""
        FrmLiquidacion.TxtTotalFob.Text = ""

        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////////////CARGO EL DETALLE DE LIQUIDACION/////////////////////////////////////////////////////////////////
        ''//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlString = "SELECT Detalle_Liquidacion.Cod_Producto, Productos.Descripcion_Producto, Detalle_Liquidacion.Cantidad, Detalle_Liquidacion.Precio_Compra,Detalle_Liquidacion.Descuento, Detalle_Liquidacion.FOB, Detalle_Liquidacion.Precio_Costo FROM Detalle_Liquidacion INNER JOIN Productos ON Detalle_Liquidacion.Cod_Producto = Productos.Cod_Productos  WHERE (Detalle_Liquidacion.Numero_Liquidacion = N'-1')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleCompra")
        FrmLiquidacion.BindingDetalle.DataSource = DataSet.Tables("DetalleCompra")
        FrmLiquidacion.TrueDBGridComponentes.DataSource = FrmLiquidacion.BindingDetalle
        FrmLiquidacion.TrueDBGridComponentes.Columns(0).Caption = "Codigo"
        FrmLiquidacion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Button = True
        FrmLiquidacion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Width = 74
        FrmLiquidacion.TrueDBGridComponentes.Columns(1).Caption = "Descripcion"
        FrmLiquidacion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Width = 259
        FrmLiquidacion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Locked = True
        FrmLiquidacion.TrueDBGridComponentes.Columns(2).Caption = "Cantidad"
        FrmLiquidacion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Width = 64
        FrmLiquidacion.TrueDBGridComponentes.Columns(3).Caption = "Precio Comp"
        FrmLiquidacion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Width = 70
        FrmLiquidacion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Locked = False
        FrmLiquidacion.TrueDBGridComponentes.Columns(3).NumberFormat = "##,##0.00"
        FrmLiquidacion.TrueDBGridComponentes.Columns(4).Caption = "%Desc"
        FrmLiquidacion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Width = 43
        FrmLiquidacion.TrueDBGridComponentes.Columns(5).Caption = "FOB"
        FrmLiquidacion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Width = 65
        FrmLiquidacion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Locked = True
        FrmLiquidacion.TrueDBGridComponentes.Columns(5).NumberFormat = "##,##0.00"
        FrmLiquidacion.TrueDBGridComponentes.Columns(6).Caption = "Precio Costo"
        FrmLiquidacion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Width = 70
        FrmLiquidacion.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Locked = True
        FrmLiquidacion.TrueDBGridComponentes.Columns(6).NumberFormat = "##,##0.00"
    End Sub

    Public Sub GrabaPlantillas(ByVal ConsecutivoFactura As String)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, Referencia As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim Subtotal As Double, Iva As Double, Neto As Double, Pagado As Double, Descuento As Double, Exonerado As Boolean
        Dim MonedaFactura As String = My.Forms.FrmFacturas.TxtMonedaFactura.Text, ClienteSeleccionado As String, SqlString As String

        Try



            Fecha = Format(FrmPlantillas.DTPFecha.Value, "yyyy-MM-dd")




            If FrmPlantillas.TxtSubTotal.Text <> "" Then
                Subtotal = FrmPlantillas.TxtSubTotal.Text
            Else
                Subtotal = 0
            End If

            If FrmPlantillas.TxtIva.Text <> "" Then
                Iva = FrmPlantillas.TxtIva.Text
            Else
                Iva = 0
            End If

            If FrmPlantillas.TxtPagado.Text <> "" Then
                Pagado = FrmPlantillas.TxtPagado.Text
            Else
                Pagado = 0
            End If

            If FrmPlantillas.TxtNetoPagar.Text <> "" Then
                Neto = FrmPlantillas.TxtNetoPagar.Text
            Else
                Neto = 0
            End If

            If FrmPlantillas.OptExsonerado.Checked = True Then
                Exonerado = True
            Else
                Exonerado = False
            End If


            If FrmPlantillas.OptTodos.Checked = True Then
                ClienteSeleccionado = FrmPlantillas.OptTodos.Text
            Else
                ClienteSeleccionado = FrmPlantillas.OptSeleccionado.Text
            End If

            Descuento = CDbl(Val(FrmPlantillas.TxtDescuento.Text))

            MiConexion.Close()

            If FrmPlantillas.TxtReferencia.Text = "" Then
                Referencia = "Plantilla para " & ClienteSeleccionado & "  " & Fecha
            Else
                Referencia = FrmPlantillas.TxtReferencia.Text
            End If

            'SqlString = "SELECT Plantilla.*  FROM Plantilla " & _
            '             "WHERE  (NumeroPlantilla = '" & FrmPlantillas.TxtNumeroEnsamble.Text & "') AND (Fecha_Plantilla = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Plantilla = '" & FrmPlantillas.CboTipoProducto.Text & "')"
            'DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            'DataAdapter.Fill(DataSet, "Consulta")
            'If Not DataSet.Tables("Consulta").Rows.Count = 0 Then

            If FrmPlantillas.TxtNumeroEnsamble.Text = "-----0-----" Then
                '//////////////////////////////////////////////////////////////////////////////////////////////
                '////////////////////////////AGREGO EL ENCABEZADO DE LA PLANTILLA///////////////////////////////////
                '/////////////////////////////////////////////////////////////////////////////////////////////////
                MiConexion.Close()
                SqlCompras = "INSERT INTO [Plantilla] ([NumeroPlantilla],[Fecha_Plantilla],[Tipo_Plantilla],[MonedaPlantilla],[Seleccion_Cliente],[Cod_Cliente],[Cod_Bodega],[Nombre_Plantilla],[Apellido_Plantilla],[Direccion_Plantilla],[Telefono_Plantilla],[Dias_Vencimiento],[Observaciones],[Exonerado],[Descuento],[SubTotal],[IVA],[Pagado],[NetoPagar],[Referencia_Plantilla]) " & _
                "VALUES ('" & ConsecutivoFactura & "','" & FrmPlantillas.DTPFecha.Text & "','" & FrmPlantillas.CboTipoProducto.Text & "','" & FrmPlantillas.TxtMonedaFactura.Text & "','" & ClienteSeleccionado & "','" & FrmPlantillas.TxtCodigoClientes.Text & "','" & FrmPlantillas.CboCodigoBodega.Text & "','" & Trim(FrmPlantillas.TxtNombres.Text) & "','" & Trim(FrmPlantillas.TxtApellidos.Text) & "','" & FrmPlantillas.TxtDireccion.Text & "','" & FrmPlantillas.TxtTelefono.Text & "'," & FrmPlantillas.TxtDiasVencimiento.Value & ",'" & FrmPlantillas.TxtObservaciones.Text & "','" & Exonerado & "'," & Descuento & " ," & Subtotal & "," & Iva & "," & Pagado & "," & Neto & ",'" & Referencia & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()

            Else
                '//////////////////////////////////////////////////////////////////////////////////////////////
                '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
                '/////////////////////////////////////////////////////////////////////////////////////////////////
                SqlCompras = "UPDATE [Plantilla] SET [NumeroPlantilla] = '" & ConsecutivoFactura & "',[Fecha_Plantilla] = '" & FrmPlantillas.DTPFecha.Text & "',[Tipo_Plantilla] = '" & FrmPlantillas.CboTipoProducto.Text & "',[MonedaPlantilla] = '" & FrmPlantillas.TxtMonedaFactura.Text & "',[Seleccion_Cliente] = '" & ClienteSeleccionado & "',[Cod_Cliente] = '" & FrmPlantillas.TxtCodigoClientes.Text & "',[Cod_Bodega] = '" & FrmPlantillas.CboCodigoBodega.Text & "',[Nombre_Plantilla] = '" & FrmPlantillas.TxtNombres.Text & "',[Apellido_Plantilla] = '" & FrmPlantillas.TxtApellidos.Text & "',[Direccion_Plantilla] = '" & FrmPlantillas.TxtDireccion.Text & "',[Telefono_Plantilla] = '" & FrmPlantillas.TxtTelefono.Text & "',[Dias_Vencimiento] = " & FrmPlantillas.TxtDiasVencimiento.Value & ",[Observaciones] = '" & FrmPlantillas.TxtObservaciones.Text & "',[Exonerado] = '" & Exonerado & "',[Descuento] = " & Descuento & ",[SubTotal] = " & Subtotal & ",[IVA] = " & Iva & ",[Pagado] = " & Pagado & ",[NetoPagar] = " & Neto & ",[Referencia_Plantilla]='" & Referencia & "'  " & _
                             "WHERE  (NumeroPlantilla = '" & FrmPlantillas.TxtNumeroEnsamble.Text & "') AND (Fecha_Plantilla = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Plantilla = '" & FrmPlantillas.CboTipoProducto.Text & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub GrabaPlantillaClientes(ByVal NumeroPlantilla As String, ByVal FechaPlantilla As Date, ByVal TipoPlantilla As String, ByVal CodCliente As String, ByVal NombreCliente As String)
        Dim SqlCompras As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim SqlDetalle As String


        Try

            Fecha = Format(FechaPlantilla, "yyyy-MM-dd")

            MiConexion.Close()
            SqlDetalle = "SELECT NumeroPlantilla, FechaPlantilla, Tipo_Plantilla, Cod_Cliente, Nombre_Cliente FROM PlantillaClientes WHERE (NumeroPlantilla = '" & NumeroPlantilla & "') AND (FechaPlantilla = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Plantilla = '" & TipoPlantilla & "') AND (Cod_Cliente = '" & CodCliente & "')"
            'SqlDetalle = "SELECT  *  FROM PlantillaClientes WHERE (NumeroPlantilla = '" & NumeroPlantilla & "') AND (FechaPlantilla = '" & Format(FechaPlantilla, "dd/MM/yyyy") & "') AND (Tipo_Plantilla = '" & TipoPlantilla & "') AND (Cod_Cliente = '" & CodCliente & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlDetalle, MiConexion)
            DataAdapter.Fill(DataSet, "PlantillaClientes")
            If DataSet.Tables("PlantillaClientes").Rows.Count = 0 Then

                '//////////////////////////////////////////////////////////////////////////////////////////////
                '////////////////////////////AGREGO EL ENCABEZADO DE LA PLANTILLA///////////////////////////////////
                '/////////////////////////////////////////////////////////////////////////////////////////////////
                MiConexion.Close()
                SqlCompras = "INSERT INTO [PlantillaClientes] ([NumeroPlantilla],[FechaPlantilla],[Tipo_Plantilla],[Cod_Cliente],[Nombre_Cliente]) " & _
                             "VALUES('" & NumeroPlantilla & "','" & FechaPlantilla & "','" & TipoPlantilla & "','" & CodCliente & "','" & NombreCliente & "')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()

            Else
                '//////////////////////////////////////////////////////////////////////////////////////////////
                '////////////////////////////EDITO EL ENCABEZADO DE LA COMPRA///////////////////////////////////
                '/////////////////////////////////////////////////////////////////////////////////////////////////
                '               SqlCompras = "UPDATE [PlantillaClientes] SET [NumeroPlantilla] = '" & NumeroPlantilla & "',[FechaPlantilla] = <FechaPlantilla, nvarchar(50),>"
                '     ,[Tipo_Plantilla] = <Tipo_Plantilla, nvarchar(50),>
                '     ,[Cod_Cliente] = <Cod_Cliente, nvarchar(50),>
                '     ,[Nombre_Cliente] = <Nombre_Cliente, nvarchar(max),>
                'WHERE <Condiciones de b�squeda,,>
                '               MiConexion.Open()
                '               ComandoUpdate = New SqlClient.SqlCommand(SqlCompras, MiConexion)
                '               iResultado = ComandoUpdate.ExecuteNonQuery
                '               MiConexion.Close()
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub GrabaDetallePlantilla(ByVal ConsecutivoFactura As String, ByVal CodProducto As String, ByVal Descripcion_Producto As String, ByVal PrecioUnitario As Double, ByVal Descuento As Double, ByVal PrecioNeto As Double, ByVal Importe As Double, ByVal Cantidad As Double, ByVal IdDetalleFactura As Double)
        Dim ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Fecha As String, MiConexion As New SqlClient.SqlConnection(Conexion), SqlUpdate As String, TasaCambio As Double
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, MonedaFactura As String, MonedaProducto As String
        Dim SqlDetalle As String

        MonedaFactura = FrmPlantillas.TxtMonedaFactura.Text
        MonedaProducto = "Cordobas"

        If MonedaFactura = "Cordobas" Then
            If MonedaProducto = "Cordobas" Then
                TasaCambio = 1
            Else
                If BuscaTasaCambio(FrmPlantillas.DTPFecha.Value) <> 0 Then
                    TasaCambio = (1 / BuscaTasaCambio(FrmPlantillas.DTPFecha.Value))
                End If
            End If
        ElseIf MonedaFactura = "Dolares" Then
            If MonedaProducto = "Cordobas" Then
                TasaCambio = BuscaTasaCambio(FrmPlantillas.DTPFecha.Value)
            Else
                TasaCambio = 1
            End If
        End If



        Fecha = Format(FrmPlantillas.DTPFecha.Value, "yyyy-MM-dd")

        SqlDetalle = "SELECT *  FROM Detalle_Plantilla WHERE (Numero_Plantilla = '" & ConsecutivoFactura & "') AND (Fecha_Plantilla = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Plantilla = '" & FrmPlantillas.CboTipoProducto.Text & "') AND (Cod_Producto = '" & CodProducto & "') AND (Descripcion_Producto = '" & Descripcion_Producto & "') AND (id_Detalle_Plantilla = '" & IdDetalleFactura & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlDetalle, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleFactura")
        If Not DataSet.Tables("DetalleFactura").Rows.Count = 0 Then
            '//////////////////////////////////////////////////////////////////////////////////////////////
            '////////////////////////////EDITO EL DETALLE DE COMPRAS///////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////

            SqlUpdate = "UPDATE [Detalle_Plantilla] SET [Cantidad] = " & Cantidad & " ,[Precio_Unitario] = " & PrecioUnitario & ",[Descuento] = " & Descuento & " ,[Precio_Neto] = " & PrecioNeto & ",[Importe] = " & Importe & ",[Descripcion_Producto] = '" & Descripcion_Producto & "' " & _
                        "WHERE (Numero_Plantilla = '" & ConsecutivoFactura & "') AND (Fecha_Plantilla = CONVERT(DATETIME, '" & Fecha & "', 102)) AND (Tipo_Plantilla = '" & FrmPlantillas.CboTipoProducto.Text & "') AND (Cod_Producto = '" & CodProducto & "') AND (Descripcion_Producto = '" & Descripcion_Producto & "') AND (id_Detalle_Plantilla = '" & IdDetalleFactura & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else

            SqlUpdate = "INSERT INTO [Detalle_Plantilla] ([Numero_Plantilla],[Fecha_Plantilla],[Tipo_Plantilla],[Cod_Producto],[Cantidad],[Precio_Unitario],[Descuento],[Precio_Neto],[Importe],[TasaCambio],[Descripcion_Producto]) " & _
            "VALUES ('" & ConsecutivoFactura & "','" & FrmPlantillas.DTPFecha.Value & "','" & FrmPlantillas.CboTipoProducto.Text & "','" & CodProducto & "' ," & Cantidad & "," & PrecioUnitario & "," & Descuento & " ," & PrecioNeto & "," & Importe & "," & TasaCambio & ",'" & Descripcion_Producto & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(SqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If

    End Sub

    Public Sub LimpiarPlantillas()
        Dim SqlString As String, DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim MiConexion As New SqlClient.SqlConnection(Conexion)

        My.Forms.FrmPlantillas.TxtNumeroEnsamble.Text = "-----0-----"
        My.Forms.FrmPlantillas.TxtCodigoClientes.Text = ""



        FrmPlantillas.DTPFecha.Value = Format(Now, "dd/MM/yyyy")
        FrmPlantillas.TxtDiasVencimiento.Value = 30

        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////CARGO LAS BODEGAS////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlString = "SELECT  * FROM   Bodegas"
        MiConexion.Open()
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataSet.Reset()
        DataAdapter.Fill(DataSet, "Bodegas")
        FrmPlantillas.CboCodigoBodega.DataSource = DataSet.Tables("Bodegas")
        If Not DataSet.Tables("Bodegas").Rows.Count = 0 Then
            FrmPlantillas.CboCodigoBodega.Text = DataSet.Tables("Bodegas").Rows(0)("Cod_Bodega")
        End If
        FrmPlantillas.CboCodigoBodega.Columns(0).Caption = "Codigo"
        FrmPlantillas.CboCodigoBodega.Columns(1).Caption = "Nombre Bodega"



        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////////////CARGO EL DETALLE DE COMPRAS/////////////////////////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlString = "SELECT Productos.Cod_Productos, Productos.Descripcion_Producto, Detalle_Plantilla.Cantidad, Detalle_Plantilla.Precio_Unitario, Detalle_Plantilla.Descuento,Detalle_Plantilla.Precio_Neto, Detalle_Plantilla.Importe, Detalle_Plantilla.id_Detalle_Plantilla FROM  Detalle_Plantilla INNER JOIN Productos ON Detalle_Plantilla.Cod_Producto = Productos.Cod_Productos  WHERE (Detalle_Plantilla.Numero_Plantilla = N'-1')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleCompra")
        FrmPlantillas.BindingDetalle.DataSource = DataSet.Tables("DetalleCompra")
        FrmPlantillas.TrueDBGridComponentes.DataSource = FrmPlantillas.BindingDetalle
        FrmPlantillas.TrueDBGridComponentes.Columns(0).Caption = "Codigo"
        FrmPlantillas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Button = True
        FrmPlantillas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(0).Width = 74
        FrmPlantillas.TrueDBGridComponentes.Columns(1).Caption = "Descripcion"
        FrmPlantillas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Width = 259
        FrmPlantillas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(1).Locked = True
        FrmPlantillas.TrueDBGridComponentes.Columns(2).Caption = "Ordenado"
        FrmPlantillas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(2).Width = 64
        FrmPlantillas.TrueDBGridComponentes.Columns(3).Caption = "Precio Unit"
        FrmPlantillas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Width = 62
        FrmPlantillas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(3).Locked = True
        FrmPlantillas.TrueDBGridComponentes.Columns(4).Caption = "%Desc"
        FrmPlantillas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(4).Width = 43
        FrmPlantillas.TrueDBGridComponentes.Columns(5).Caption = "Precio Neto"
        FrmPlantillas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Width = 65
        FrmPlantillas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(5).Locked = True
        FrmPlantillas.TrueDBGridComponentes.Columns(6).Caption = "Importe"
        FrmPlantillas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Width = 61
        FrmPlantillas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(6).Locked = True
        FrmPlantillas.TrueDBGridComponentes.Splits.Item(0).DisplayColumns(7).Visible = False

        My.Forms.FrmPlantillas.OptTodos.Checked = True
        My.Forms.FrmPlantillas.TxtSubTotal.Text = ""
        My.Forms.FrmPlantillas.TxtIva.Text = ""
        My.Forms.FrmPlantillas.TxtPagado.Text = ""
        My.Forms.FrmPlantillas.TxtNetoPagar.Text = ""
        My.Forms.FrmPlantillas.TxtObservaciones.Text = ""
        My.Forms.FrmPlantillas.TxtDescuento.Text = ""
        My.Forms.FrmPlantillas.TxtReferencia.Text = ""

        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '///////////////////////////////CARGO EL DETALLE LOS CLIENTES/////////////////////////////////////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        FrmPlantillas.LstClientes.ClearItems()
        My.Forms.FrmPlantillas.DataSetListBox.Reset()
        SqlString = "SELECT Cod_Cliente, Nombre_Cliente FROM PlantillaClientes WHERE (NumeroPlantilla = N'-1') AND (FechaPlantilla = '01/01/1900') AND (Tipo_Plantilla = N'-1') AND (Cod_Cliente = N'-1')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(My.Forms.FrmPlantillas.DataSetListBox, "PlantillaClientes")
        FrmPlantillas.LstClientes.DataSource = My.Forms.FrmPlantillas.DataSetListBox.Tables("PlantillaClientes")
        FrmPlantillas.LstClientes.Splits.Item(0).DisplayColumns(0).Width = 73
        FrmPlantillas.LstClientes.Splits.Item(0).DisplayColumns(1).Width = 155

    End Sub


    Public Sub ActualizaMETODOPlantilla()
        Dim Metodo As String = "", iPosicion As Double, Registros As Double, Monto As Double
        Dim Subtotal As Double, Iva As Double, Neto As Double, CodProducto As String, SQlString As String
        Dim MiConexion As New SqlClient.SqlConnection(Conexion), CodIva As String, Tasa As Double, SQlMetodo As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, Moneda As String, TasaCambio As Double
        Dim Fecha As String, SQlTasa As String, TipoMetodo As String, MonedaFactura As String
        Dim Descuento As Double = 0

        'Registros = FrmPlantillas.BindingMetodo.Count
        iPosicion = 0

        Fecha = Format(FrmPlantillas.DTPFecha.Value, "yyyy-MM-dd")

        Do While iPosicion < Registros
            'Metodo = FrmPlantillas.BindingMetodo.Item(iPosicion)("NombrePago")
            TasaCambio = 1
            Fecha = Format(FrmPlantillas.DTPFecha.Value, "yyyy-MM-dd")
            '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            '//////////////////////////////BUSCO LA MONEDA DEL METODO DE PAGO///////////////////////////////////////////////////////
            '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            MonedaFactura = FrmPlantillas.TxtMonedaFactura.Text
            Moneda = "Cordobas"
            TipoMetodo = "Cambio"
            SQlMetodo = "SELECT * FROM MetodoPago WHERE (NombrePago = '" & Metodo & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SQlMetodo, MiConexion)
            DataAdapter.Fill(DataSet, "Metodo")
            If DataSet.Tables("Metodo").Rows.Count <> 0 Then
                Moneda = DataSet.Tables("Metodo").Rows(0)("Moneda")
                TipoMetodo = DataSet.Tables("Metodo").Rows(0)("TipoPago")
            End If
            DataSet.Tables("Metodo").Clear()


            Select Case Moneda
                Case "Cordobas"
                    If MonedaFactura = "Cordobas" Then
                        TasaCambio = 1
                    Else
                        SQlTasa = "SELECT  * FROM TasaCambio WHERE (FechaTasa = CONVERT(DATETIME, '" & Fecha & "', 102))"
                        DataAdapter = New SqlClient.SqlDataAdapter(SQlTasa, MiConexion)
                        DataAdapter.Fill(DataSet, "TasaCambio")
                        If DataSet.Tables("TasaCambio").Rows.Count <> 0 Then
                            TasaCambio = (1 / DataSet.Tables("TasaCambio").Rows(0)("MontoTasa"))
                        Else
                            'TasaCambio = 0
                            MsgBox("La Tasa de Cambio no Existe para esta Fecha", MsgBoxStyle.Critical, "Sistema Facturacion")
                            'FrmPlantillas.BindingMetodo.Item(iPosicion)("Monto") = 0
                        End If
                        DataSet.Tables("TasaCambio").Clear()
                    End If

                Case "Dolares"
                    If MonedaFactura = "Cordobas" Then
                        SQlTasa = "SELECT  * FROM TasaCambio WHERE (FechaTasa = CONVERT(DATETIME, '" & Fecha & "', 102))"
                        DataAdapter = New SqlClient.SqlDataAdapter(SQlTasa, MiConexion)
                        DataAdapter.Fill(DataSet, "TasaCambio")
                        If DataSet.Tables("TasaCambio").Rows.Count <> 0 Then
                            TasaCambio = DataSet.Tables("TasaCambio").Rows(0)("MontoTasa")
                        Else
                            'TasaCambio = 0
                            MsgBox("La Tasa de Cambio no Existe para esta Fecha", MsgBoxStyle.Critical, "Sistema Facturacion")
                            'FrmPlantillas.BindingMetodo.Item(iPosicion)("Monto") = 0
                        End If
                        DataSet.Tables("TasaCambio").Clear()
                    Else
                        TasaCambio = 1
                    End If
            End Select

            If TipoMetodo = "Cambio" Then
                TasaCambio = TasaCambio * -1
            End If



            'If Not IsDBNull(FrmPlantillas.BindingMetodo.Item(iPosicion)("Monto")) Then
            '    Monto = (FrmPlantillas.BindingMetodo.Item(iPosicion)("Monto") * TasaCambio) + Monto
            'Else
            '    Monto = 0
            'End If
            Monto = 0
            iPosicion = iPosicion + 1
        Loop


        Registros = FrmPlantillas.BindingDetalle.Count
        iPosicion = 0

        Do While iPosicion < Registros
            If Not IsDBNull(FrmPlantillas.BindingDetalle.Item(iPosicion)("Importe")) Then
                Subtotal = CDbl(FrmPlantillas.BindingDetalle.Item(iPosicion)("Importe")) + Subtotal

            End If
            iPosicion = iPosicion + 1
        Loop

        CodProducto = FrmPlantillas.TrueDBGridComponentes.Columns(0).Text
        SQlString = "SELECT Productos.*  FROM Productos WHERE (Cod_Productos = '" & CodProducto & "') "
        DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
        DataAdapter.Fill(DataSet, "Productos")
        If Not DataSet.Tables("Productos").Rows.Count = 0 Then
            CodIva = DataSet.Tables("Productos").Rows(0)("Cod_Iva")
            SQlString = "SELECT *  FROM Impuestos WHERE  (Cod_Iva = '" & CodIva & "')"
            DataAdapter = New SqlClient.SqlDataAdapter(SQlString, MiConexion)
            DataAdapter.Fill(DataSet, "IVA")
            If Not DataSet.Tables("IVA").Rows.Count = 0 Then
                Tasa = DataSet.Tables("IVA").Rows(0)("Impuesto")
            End If


        End If
        If FrmPlantillas.OptExsonerado.Checked = False Then
            Iva = Subtotal * Tasa
        Else
            Iva = 0
        End If

        Descuento = CDbl(Val(FrmPlantillas.TxtDescuento.Text))

        Neto = (Subtotal + Iva) - Monto - Descuento
        FrmPlantillas.TxtSubTotal.Text = Format(Subtotal, "##,##0.00")
        FrmPlantillas.TxtIva.Text = Format(Iva, "##,##0.00")
        FrmPlantillas.TxtPagado.Text = Format(Monto, "##,##0.00")
        FrmPlantillas.TxtNetoPagar.Text = Format(Neto, "##,##0.00")
    End Sub
End Module
