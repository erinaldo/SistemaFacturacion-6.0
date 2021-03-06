Public Class FrmPlanilla
    Public MiConexion As New SqlClient.SqlConnection(Conexion)
    Private Sub FrmPlanilla_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim SqlString As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter

        SqlString = "SELECT TipoNomina.CodTipoNomina, TipoNomina.TipoNomina, Periodos.Actual, Periodos.Periodo, Periodos.Inicio, Periodos.Final FROM  TipoNomina INNER JOIN Periodos ON TipoNomina.TipoNomina = Periodos.TipoNomina  WHERE(Periodos.Actual = 1)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "TipoNomina")
        Me.CboTipoPlanilla.DataSource = DataSet.Tables("TipoNomina")



    End Sub

    Private Sub CboTipoPlanilla_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboTipoPlanilla.TextChanged
        Dim SqlString As String, CodTipoNomina As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, NumNomina As String

        CodTipoNomina = Me.CboTipoPlanilla.Text

        SqlString = "SELECT TipoNomina.CodTipoNomina, TipoNomina.TipoNomina, Periodos.Actual, Periodos.Periodo, Periodos.Inicio, Periodos.Final FROM  TipoNomina INNER JOIN Periodos ON TipoNomina.TipoNomina = Periodos.TipoNomina  WHERE(Periodos.Actual = 1) And (TipoNomina.CodTipoNomina='" & CodTipoNomina & "') "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "TipoNomina")
        If DataSet.Tables("TipoNomina").Rows.Count <> 0 Then
            Me.DTPFechaIni.Value = DataSet.Tables("TipoNomina").Rows(0)("Inicio")
            Me.DTPFechaFin.Value = DataSet.Tables("TipoNomina").Rows(0)("Final")
            Me.CmdCalcular.Enabled = True

            SqlString = "SELECT * FROM Periodos WHERE (Inicio = CONVERT(DATETIME, '" & Format(Me.DTPFechaIni.Value, "yyyy-MM-dd") & "', 102)) AND (Actual = 1)"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.Fill(DataSet, "PeriodoNomina")
            If DataSet.Tables("PeriodoNomina").Rows.Count <> 0 Then
                NumNomina = DataSet.Tables("PeriodoNomina").Rows(0)("NumNomina")
                Me.TxtNumNomina.Text = NumNomina
                Me.TDGridDeducciones2.Enabled = True

                SqlString = "SELECT * FROM Nomina WHERE (NumPlanilla = '" & NumNomina & "')"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "Nomina")
                If DataSet.Tables("Nomina").Rows.Count <> 0 Then
                    Me.TxtPrecioUnitario.Text = DataSet.Tables("Nomina").Rows(0)("PrecioUnitario")
                    Me.TxtIR.Text = DataSet.Tables("Nomina").Rows(0)("PorcientoIR")
                    Me.TxtDeduccionPolicia.Text = DataSet.Tables("Nomina").Rows(0)("PorcientoPolicia")
                End If


                SqlString = "SELECT * FROM Deducciones_Planilla WHERE (NumNomina = '" & NumNomina & "')ORDER BY CodProductor"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "Deducciones")
                Me.BindingDeducciones2.DataSource = DataSet.Tables("Deducciones")
                Me.TDGridDeducciones2.DataSource = Me.BindingDeducciones2
                Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(0).Visible = False
                Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(1).Visible = False
                Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(2).Button = True
                Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(2).Width = 100
                Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(3).Visible = False
                Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(4).Width = 156
                Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(5).Width = 78
                Me.TDGridDeducciones2.Columns(5).NumberFormat = "##,##0.00"
                Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(6).Width = 78
                Me.TDGridDeducciones2.Columns(6).NumberFormat = "##,##0.00"
                Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(7).Width = 78
                Me.TDGridDeducciones2.Columns(7).NumberFormat = "##,##0.00"
                Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(8).Width = 78
                Me.TDGridDeducciones2.Columns(8).NumberFormat = "##,##0.00"
                Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(9).Width = 78
                Me.TDGridDeducciones2.Columns(9).NumberFormat = "##,##0.00"

                Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns("Trazabilidad").Width = 70
                Me.TDGridDeducciones2.Columns("Trazabilidad").NumberFormat = "##,##0.00"
                Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns("OtrasDeducciones").Width = 70
                Me.TDGridDeducciones2.Columns("OtrasDeducciones").NumberFormat = "##,##0.00"

                SqlString = "SELECT Detalle_Nomina.CodProductor, Productor.NombreProductor + ' ' + Productor.ApellidoProductor AS Nombres, Detalle_Nomina.Lunes,Detalle_Nomina.Martes, Detalle_Nomina.Miercoles, Detalle_Nomina.Jueves, Detalle_Nomina.Viernes, Detalle_Nomina.Sabado,Detalle_Nomina.Domingo, Detalle_Nomina.Total, Detalle_Nomina.PrecioVenta, Detalle_Nomina.TotalIngresos FROM Detalle_Nomina INNER JOIN Productor ON Detalle_Nomina.CodProductor = Productor.CodProductor AND Detalle_Nomina.TipoProductor = Productor.TipoProductor WHERE (Detalle_Nomina.NumNomina = '" & Me.TxtNumNomina.Text & "') AND (Detalle_Nomina.TipoProductor = 'Productor')"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "DetalleIngresos")
                Me.TDGridIngresos.DataSource = DataSet.Tables("DetalleIngresos")
                Me.TDGridIngresos.Splits(0).DisplayColumns(0).Width = 70
                Me.TDGridIngresos.Columns(0).Caption = "Codigo"
                Me.TDGridIngresos.Splits(0).DisplayColumns(0).Locked = True
                Me.TDGridIngresos.Splits(0).DisplayColumns(1).Width = 190
                Me.TDGridIngresos.Splits(0).DisplayColumns(1).Locked = True
                Me.TDGridIngresos.Splits(0).DisplayColumns(2).Width = 61
                Me.TDGridIngresos.Splits(0).DisplayColumns(2).Locked = True
                Me.TDGridIngresos.Splits(0).DisplayColumns(3).Width = 61
                Me.TDGridIngresos.Splits(0).DisplayColumns(3).Locked = True
                Me.TDGridIngresos.Splits(0).DisplayColumns(4).Width = 61
                Me.TDGridIngresos.Splits(0).DisplayColumns(4).Locked = True
                Me.TDGridIngresos.Splits(0).DisplayColumns(5).Width = 61
                Me.TDGridIngresos.Splits(0).DisplayColumns(5).Locked = True
                Me.TDGridIngresos.Splits(0).DisplayColumns(6).Width = 61
                Me.TDGridIngresos.Splits(0).DisplayColumns(6).Locked = True
                Me.TDGridIngresos.Splits(0).DisplayColumns(7).Width = 61
                Me.TDGridIngresos.Splits(0).DisplayColumns(7).Locked = True
                Me.TDGridIngresos.Splits(0).DisplayColumns(8).Width = 61
                Me.TDGridIngresos.Splits(0).DisplayColumns(8).Locked = True
                Me.TDGridIngresos.Splits(0).DisplayColumns(9).Width = 61
                Me.TDGridIngresos.Splits(0).DisplayColumns(9).Locked = True
                Me.TDGridIngresos.Splits(0).DisplayColumns(10).Width = 70
                Me.TDGridIngresos.Splits(0).DisplayColumns(10).Locked = True
                Me.TDGridIngresos.Columns(9).Caption = "Total Litros"
                Me.TDGridIngresos.Columns(10).NumberFormat = "##,##0.00"
                Me.TDGridIngresos.Columns(10).Caption = "PrecioUnit"
                Me.TDGridIngresos.Splits(0).DisplayColumns(11).Width = 80
                Me.TDGridIngresos.Splits(0).DisplayColumns(11).Locked = True
                Me.TDGridIngresos.Columns(11).NumberFormat = "##,##0.00"



                SqlString = "SELECT Detalle_Nomina.CodProductor, Productor.NombreProductor + ' ' + Productor.ApellidoProductor AS Nombres, Detalle_Nomina.IR,Detalle_Nomina.DeduccionPolicia, Detalle_Nomina.Anticipo, Detalle_Nomina.DeduccionTransporte, Detalle_Nomina.Pulperia,Detalle_Nomina.Inseminacion, Detalle_Nomina.Trazabilidad, Detalle_Nomina.ProductosVeterinarios,Detalle_Nomina.OtrasDeducciones, Detalle_Nomina.IR + Detalle_Nomina.DeduccionPolicia + Detalle_Nomina.Anticipo + Detalle_Nomina.DeduccionTransporte + Detalle_Nomina.Pulperia + Detalle_Nomina.Inseminacion + Detalle_Nomina.ProductosVeterinarios + Detalle_Nomina.OtrasDeducciones + Detalle_Nomina.Trazabilidad AS TotalEgresos, Detalle_Nomina.TotalIngresos - (Detalle_Nomina.IR + Detalle_Nomina.DeduccionPolicia + Detalle_Nomina.Anticipo + Detalle_Nomina.DeduccionTransporte + Detalle_Nomina.Pulperia + Detalle_Nomina.Inseminacion + Detalle_Nomina.ProductosVeterinarios + Detalle_Nomina.OtrasDeducciones + Detalle_Nomina.Trazabilidad) AS NetoPagar FROM  Detalle_Nomina INNER JOIN Productor ON Detalle_Nomina.CodProductor = Productor.CodProductor AND Detalle_Nomina.TipoProductor = Productor.TipoProductor " & _
                            "WHERE (Detalle_Nomina.NumNomina = '" & Me.TxtNumNomina.Text & "') AND (Detalle_Nomina.TipoProductor = 'Productor')"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "DetalleEgresos")
                Me.TDGridDeducciones.DataSource = DataSet.Tables("DetalleEgresos")
                Me.TDGridDeducciones.Splits(0).DisplayColumns(0).Width = 50
                Me.TDGridDeducciones.Columns(0).Caption = "Codigo"
                Me.TDGridDeducciones.Splits(0).DisplayColumns(0).Locked = True
                Me.TDGridDeducciones.Splits(0).DisplayColumns(1).Width = 180
                Me.TDGridDeducciones.Splits(0).DisplayColumns(1).Locked = True
                Me.TDGridDeducciones.Splits(0).DisplayColumns(2).Width = 60
                Me.TDGridDeducciones.Splits(0).DisplayColumns(2).Locked = True
                Me.TDGridDeducciones.Columns(2).NumberFormat = "##,##0.00"
                Me.TDGridDeducciones.Splits(0).DisplayColumns(3).Width = 60
                Me.TDGridDeducciones.Splits(0).DisplayColumns(3).Locked = True
                Me.TDGridDeducciones.Splits(0).DisplayColumns("DeduccionPolicia").Visible = False
                Me.TDGridDeducciones.Columns(3).NumberFormat = "##,##0.00"
                Me.TDGridDeducciones.Columns(3).Caption = "Policia"
                Me.TDGridDeducciones.Columns(4).NumberFormat = "##,##0.00"
                Me.TDGridDeducciones.Splits(0).DisplayColumns(4).Width = 60
                Me.TDGridDeducciones.Splits(0).DisplayColumns(4).Locked = True
                Me.TDGridDeducciones.Columns(5).NumberFormat = "##,##0.00"
                Me.TDGridDeducciones.Splits(0).DisplayColumns(5).Width = 70
                Me.TDGridDeducciones.Splits(0).DisplayColumns(5).Locked = True
                Me.TDGridDeducciones.Columns(5).Caption = "Transporte"
                Me.TDGridDeducciones.Columns(6).NumberFormat = "##,##0.00"
                Me.TDGridDeducciones.Splits(0).DisplayColumns(6).Width = 70
                Me.TDGridDeducciones.Splits(0).DisplayColumns(6).Locked = True
                Me.TDGridDeducciones.Columns(7).NumberFormat = "##,##0.00"
                Me.TDGridDeducciones.Splits(0).DisplayColumns(7).Width = 70
                Me.TDGridDeducciones.Splits(0).DisplayColumns(7).Locked = True
                Me.TDGridDeducciones.Columns("Trazabilidad").NumberFormat = "##,##0.00"
                Me.TDGridDeducciones.Splits(0).DisplayColumns("Trazabilidad").Width = 70
                Me.TDGridDeducciones.Splits(0).DisplayColumns("Trazabilidad").Locked = True
                Me.TDGridDeducciones.Columns("ProductosVeterinarios").NumberFormat = "##,##0.00"
                Me.TDGridDeducciones.Splits(0).DisplayColumns("ProductosVeterinarios").Width = 70
                Me.TDGridDeducciones.Splits(0).DisplayColumns("ProductosVeterinarios").Locked = True
                Me.TDGridDeducciones.Columns("ProductosVeterinarios").Caption = "Veterinario"

                Me.TDGridDeducciones.Columns("OtrasDeducciones").Caption = "Otras"
                Me.TDGridDeducciones.Columns("OtrasDeducciones").NumberFormat = "##,##0.00"
                Me.TDGridDeducciones.Splits(0).DisplayColumns("OtrasDeducciones").Width = 80
                Me.TDGridDeducciones.Splits(0).DisplayColumns("OtrasDeducciones").Locked = True
                Me.TDGridDeducciones.Columns("TotalEgresos").NumberFormat = "##,##0.00"
                Me.TDGridDeducciones.Splits(0).DisplayColumns("TotalEgresos").Width = 80
                Me.TDGridDeducciones.Splits(0).DisplayColumns("TotalEgresos").Locked = True

                Me.TDGridDeducciones.Columns("NetoPagar").NumberFormat = "##,##0.00"
                Me.TDGridDeducciones.Splits(0).DisplayColumns("NetoPagar").Width = 80
                Me.TDGridDeducciones.Splits(0).DisplayColumns("NetoPagar").Locked = True

                'Me.TDGridDeducciones.Columns(10).NumberFormat = "##,##0.00"
                'Me.TDGridDeducciones.Splits(0).DisplayColumns(10).Width = 80
                'Me.TDGridDeducciones.Splits(0).DisplayColumns(10).Locked = True

            Else
                MsgBox("No Existe nomina asignada al periodo Seleccionado", MsgBoxStyle.Critical, "Zeus Acopio")
                Exit Sub
                NumNomina = -1
            End If






        Else
            Me.CmdCalcular.Enabled = False
        End If
    End Sub

    Private Sub CmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSalir.Click
        Me.Close()
    End Sub

    Private Sub CmdCalcular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdCalcular.Click
        Dim SqlString As String, iPosicion As Double, Registros As Double
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim CodProductor As String, Nombres As String, Fecha As Date, Cantidad As Double, CantidadTotal As Double
        Dim MontoLunes As Double, MontoMartes As Double, MontoMiercoles As Double, MontoJueves As Double, MontoViernes As Double, MontoSabado As Double, MontoDomingo As Double
        Dim iPosicion2 As Double = 0, Registros2 As Double = 0, Contador As Double = 1, PrecioUnitario As Double, IngresoBruto, PorcientoIr As Double
        Dim MontoIr As Double, PorcientoPolicia As Double, MontoPolicia As Double, MontoVeterinario As Double, ROC1 As String = 0, ROC2 As String = 0, ROC3 As String = 0, ROC4 As String = 0, ROC5 As String = 0, ROC6 As String = 0, ROC7 As String = 0
        Dim Anticipo As Double, Transporte As Double, Pulperia As Double, Inseminacion As Double, NCompra As String
        Dim StrSqlUpdate As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer, Trazabilidad As Double, Otros As Double


        If Me.CboTipoPlanilla.Text = "" Then
            MsgBox("Seleccione la Nomina, para Calcular", MsgBoxStyle.Critical, "Zeus Acopio")
            Exit Sub
        End If

        Fecha = Me.DTPFechaIni.Value
        PrecioUnitario = Me.TxtPrecioUnitario.Text
        PorcientoIr = Val(Me.TxtIR.Text) / 100
        PorcientoPolicia = Val(Me.TxtDeduccionPolicia.Text) / 100

        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////////////////////////ACTUALIZO LOS ENCABEZADOS DE LA NOMINA//////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        StrSqlUpdate = "UPDATE [Nomina] SET [PorcientoIR] = " & Val(Me.TxtIR.Text) & ",[PorcientoPolicia] =  " & Val(Me.TxtDeduccionPolicia.Text) & ",[PrecioUnitario] = " & Val(Me.TxtPrecioUnitario.Text) & " " & _
                       "WHERE (NumPlanilla = '" & Me.TxtNumNomina.Text & "') "
        MiConexion.Open()
        ComandoUpdate = New SqlClient.SqlCommand(StrSqlUpdate, MiConexion)
        iResultado = ComandoUpdate.ExecuteNonQuery
        MiConexion.Close()


        '/////////////////////////////////////////CARGO LOS PRODUCTORES ACTIVOS/////////////////////////////////////////

        SqlString = "SELECT * FROM Productor WHERE (TipoProductor = 'Productor') AND (Activo = 1)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Productor")
        MiConexion.Close()

        iPosicion = 0
        Me.ProgressBar.Visible = True
        Me.ProgressBar.Minimum = 0
        Me.ProgressBar.Visible = True
        Me.ProgressBar.Value = 0
        Registros = DataSet.Tables("Productor").Rows.Count
        Me.ProgressBar.Maximum = Registros
        Do While iPosicion < Registros
            My.Application.DoEvents()
            CodProductor = DataSet.Tables("Productor").Rows(iPosicion)("CodProductor")
            Fecha = Me.DTPFechaIni.Value
            Nombres = DataSet.Tables("Productor").Rows(iPosicion)("NombreProductor") + " " + DataSet.Tables("Productor").Rows(iPosicion)("ApellidoProductor")
            Me.LblProcesando.Text = "PROCESANDO PRODUCTOR: " & CodProductor & " " & Nombres
            CantidadTotal = 0
            Contador = 1
            '///////////////////////////////////////////////77////////////////////////////////////////////////////////////////////////////
            '//////////////////////////////////BUSCO LAS RECEPCIONES DEL PRODUCTOR///////////////////////////////////////////////////////
            '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Me.ProgressBar2.Visible = True
            Me.ProgressBar2.Minimum = 0
            Me.ProgressBar2.Visible = True
            Me.ProgressBar2.Value = 0
            Me.ProgressBar2.Maximum = 7
            Do While Fecha <= Me.DTPFechaFin.Value
                '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                '//////////////////////////////////////////////////BUSCO LAS RECEPCIONES DE LECHE/////////////////////////////////////////////
                '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                SqlString = "SELECT  * FROM Detalle_Compras INNER JOIN Compras ON Detalle_Compras.Numero_Compra = Compras.Numero_Compra AND Detalle_Compras.Fecha_Compra = Compras.Fecha_Compra AND Detalle_Compras.Tipo_Compra = Compras.Tipo_Compra  " & _
                            "WHERE (Detalle_Compras.Tipo_Compra = 'Recepcion') AND (Compras.Cod_Proveedor = '" & CodProductor & "') AND (Compras.TipoProductor = 'Productor') AND (Compras.Fecha_Compra = CONVERT(DATETIME, '" & Format(Fecha, "yyyy-MM-dd") & "', 102))"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "Recepciones")
                iPosicion2 = 0
                Cantidad = 0
                NCompra = 0
                Registros2 = DataSet.Tables("Recepciones").Rows.Count

                Do While iPosicion2 < Registros2
                    Cantidad = Cantidad + DataSet.Tables("Recepciones").Rows(iPosicion2)("Cantidad")
                    NCompra = DataSet.Tables("Recepciones").Rows(iPosicion2)("Numero_Compra")
                    iPosicion2 = iPosicion2 + 1
                Loop
                DataSet.Tables("Recepciones").Clear()


                CantidadTotal = CantidadTotal + Cantidad

                Select Case Contador
                    Case 1
                        MontoLunes = Cantidad
                        ROC1 = NCompra
                    Case 2
                        MontoMartes = Cantidad
                        ROC2 = NCompra
                    Case 3
                        MontoMiercoles = Cantidad
                        ROC3 = NCompra
                    Case 4
                        MontoJueves = Cantidad
                        ROC4 = NCompra
                    Case 5
                        MontoViernes = Cantidad
                        ROC5 = NCompra
                    Case 6
                        MontoSabado = Cantidad
                        ROC6 = NCompra
                    Case 7
                        MontoDomingo = Cantidad
                        ROC7 = NCompra
                End Select

                IngresoBruto = CantidadTotal * PrecioUnitario
                MontoIr = PorcientoIr * IngresoBruto
                MontoPolicia = PorcientoPolicia * CantidadTotal


                '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                '/////////////////////////////////////////////////BUSCO LAS DEDUCCIONES/////////////////////////////////////////////////
                '////////////////////////////////////////////////777777777777777777777777777777777777777777777777777777777777777777777
                Anticipo = 0
                Transporte = 0
                Pulperia = 0
                Inseminacion = 0
                Trazabilidad = 0
                Otros = 0
                SqlString = "SELECT  * FROM Deducciones_Planilla WHERE (NumNomina = '" & Me.TxtNumNomina.Text & "') AND (CodProductor = '" & CodProductor & "') AND (TipoProductor = 'Productor')"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "DeduccionPlanilla")
                If DataSet.Tables("DeduccionPlanilla").Rows.Count <> 0 Then
                    Anticipo = DataSet.Tables("DeduccionPlanilla").Rows(0)("Anticipo")
                    Transporte = DataSet.Tables("DeduccionPlanilla").Rows(0)("Transporte")
                    Pulperia = DataSet.Tables("DeduccionPlanilla").Rows(0)("Pulperia")
                    Inseminacion = DataSet.Tables("DeduccionPlanilla").Rows(0)("Inseminacion")
                    Trazabilidad = DataSet.Tables("DeduccionPlanilla").Rows(0)("Trazabilidad")
                    Otros = DataSet.Tables("DeduccionPlanilla").Rows(0)("OtrasDeducciones")
                End If
                DataSet.Tables("DeduccionPlanilla").Clear()



                '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                '/////////////////////////////////////////////////BUSCO LOS PRODUCTOS VETERINARIOS/////////////////////////////////////////////////
                '////////////////////////////////////////////////777777777777777777777777777777777777777777777777777777777777777777777
                SqlString = "SELECT DISTINCT MAX(Facturas.Numero_Factura) AS Numero_Factura, MAX(Facturas.Fecha_Factura) AS Fecha_Factura, SUM(Facturas.MontoCredito) AS MontoCredito, SUM(DetalleRecibo.MontoPagado - DetalleRecibo.MontoPagado) AS MontoPagado, SUM(Facturas.MontoCredito) AS Saldo FROM  Facturas LEFT OUTER JOIN DetalleRecibo ON Facturas.Numero_Factura = DetalleRecibo.Numero_Factura " & _
                            "WHERE (Facturas.Cod_Cliente = '" & CodProductor & "') AND (Facturas.Tipo_Factura = 'Factura') AND (Facturas.Fecha_Vencimiento <= CONVERT(DATETIME,'" & Format(Me.DTPFechaFin.Value, "yyyy-MM-dd") & "', 102)) HAVING (SUM(Facturas.MontoCredito) <> 0) AND (MAX(Facturas.TipoProductor) = 'Productor') ORDER BY MAX(Facturas.Numero_Factura) DESC"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "Veterinario")
                If DataSet.Tables("Veterinario").Rows.Count <> 0 Then
                    If Not IsDBNull(DataSet.Tables("Veterinario").Rows(0)("MontoCredito")) Then
                        MontoVeterinario = DataSet.Tables("Veterinario").Rows(0)("MontoCredito")
                    End If
                Else
                    MontoVeterinario = 0
                End If

                DataSet.Tables("Veterinario").Clear()


                Contador = Contador + 1
                Fecha = DateAdd(DateInterval.Day, 1, Fecha)
                Me.ProgressBar2.Value = Me.ProgressBar2.Value + 1
            Loop





            SqlString = "SELECT  * FROM Detalle_Nomina WHERE (NumNomina = '" & Me.TxtNumNomina.Text & "') AND (CodProductor = '" & CodProductor & "') AND (TipoProductor = 'Productor')"
            DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
            DataAdapter.Fill(DataSet, "DetalleNomina")
            If DataSet.Tables("DetalleNomina").Rows.Count = 0 Then
                StrSqlUpdate = "INSERT INTO [Detalle_Nomina] ([NumNomina],[CodProductor],[TipoProductor],[Roc1],[Lunes],[Roc2],[Martes],[Roc3],[Miercoles],[Roc4],[Jueves],[Roc5],[Viernes],[Roc6],[Sabado],[Roc7],[Domingo],[Total],[PrecioVenta],[TotalIngresos],[IR],[DeduccionPolicia],[Anticipo],[DeduccionTransporte],[Pulperia],[Inseminacion],[Trazabilidad][ProductosVeterinarios],[OtrasDeducciones]) " & _
                               "VALUES ('" & Me.TxtNumNomina.Text & "','" & CodProductor & "','Productor','" & ROC1 & "'," & MontoLunes & ",'" & ROC2 & "'," & MontoMartes & ",'" & ROC3 & "'," & MontoMiercoles & ",'" & ROC4 & "'," & MontoJueves & ",'" & ROC5 & "'," & MontoViernes & ",'" & ROC6 & "'," & MontoSabado & ",'" & ROC7 & "'," & MontoDomingo & "," & CantidadTotal & " ," & PrecioUnitario & "," & IngresoBruto & "," & MontoIr & "," & MontoPolicia & "," & Anticipo & "," & Transporte & "," & Pulperia & "," & Inseminacion & "," & Trazabilidad & " ," & MontoVeterinario & " ," & Otros & ")"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(StrSqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()

            Else

                If Not IsDBNull(DataSet.Tables("DetalleNomina").Rows(0)("PrecioVenta")) Then
                    If DataSet.Tables("DetalleNomina").Rows(0)("PrecioVenta") <= 0 Then
                        PrecioUnitario = Me.TxtPrecioUnitario.Text
                    Else
                        PrecioUnitario = DataSet.Tables("DetalleNomina").Rows(0)("PrecioVenta")
                    End If
                Else
                    PrecioUnitario = Me.TxtPrecioUnitario.Text
                End If


                StrSqlUpdate = "UPDATE [Detalle_Nomina] SET [Roc1] = '" & ROC1 & "',[Lunes] = " & MontoLunes & ",[Roc2] = '" & ROC2 & "',[Martes] = " & MontoMartes & ",[Roc3] = '" & ROC3 & "',[Miercoles] = '" & MontoMiercoles & "',[Roc4] = '" & ROC4 & "',[Jueves] = " & MontoJueves & ",[Roc5] = '" & ROC5 & "',[Viernes] =" & MontoViernes & ",[Roc6] = '" & ROC6 & "',[Sabado] = " & MontoSabado & ",[Roc7] = '" & ROC7 & "',[Domingo] = " & MontoDomingo & ",[Total] = " & CantidadTotal & ",[PrecioVenta] = " & PrecioUnitario & ",[TotalIngresos] = " & IngresoBruto & ",[IR] = " & MontoIr & ",[DeduccionPolicia] = " & MontoPolicia & ",[Anticipo] = " & Anticipo & ",[DeduccionTransporte] = " & Transporte & " ,[Pulperia] = " & Pulperia & ",[Inseminacion] = " & Inseminacion & ",[ProductosVeterinarios] = " & MontoVeterinario & " ,[Trazabilidad] = " & Trazabilidad & ",[OtrasDeducciones] = " & Otros & " " & _
                               "WHERE (NumNomina = '" & Me.TxtNumNomina.Text & "') AND (CodProductor = '" & CodProductor & "') AND (TipoProductor = 'Productor')"
                MiConexion.Open()
                ComandoUpdate = New SqlClient.SqlCommand(StrSqlUpdate, MiConexion)
                iResultado = ComandoUpdate.ExecuteNonQuery
                MiConexion.Close()
                End If
                DataSet.Tables("DetalleNomina").Clear()

                iPosicion = iPosicion + 1
                Me.ProgressBar.Value = iPosicion
        Loop


        SqlString = "SELECT Detalle_Nomina.CodProductor, Productor.NombreProductor + ' ' + Productor.ApellidoProductor AS Nombres, Detalle_Nomina.Lunes,Detalle_Nomina.Martes, Detalle_Nomina.Miercoles, Detalle_Nomina.Jueves, Detalle_Nomina.Viernes, Detalle_Nomina.Sabado,Detalle_Nomina.Domingo, Detalle_Nomina.Total, Detalle_Nomina.PrecioVenta, Detalle_Nomina.TotalIngresos FROM Detalle_Nomina INNER JOIN Productor ON Detalle_Nomina.CodProductor = Productor.CodProductor AND Detalle_Nomina.TipoProductor = Productor.TipoProductor WHERE (Detalle_Nomina.NumNomina = '" & Me.TxtNumNomina.Text & "') AND (Detalle_Nomina.TipoProductor = 'Productor')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleIngresos")
        Me.TDGridIngresos.DataSource = DataSet.Tables("DetalleIngresos")
        Me.TDGridIngresos.Splits(0).DisplayColumns(0).Width = 70
        Me.TDGridIngresos.Columns(0).Caption = "Codigo"
        Me.TDGridIngresos.Splits(0).DisplayColumns(0).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(1).Width = 190
        Me.TDGridIngresos.Splits(0).DisplayColumns(1).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(2).Width = 61
        Me.TDGridIngresos.Splits(0).DisplayColumns(2).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(3).Width = 61
        Me.TDGridIngresos.Splits(0).DisplayColumns(3).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(4).Width = 61
        Me.TDGridIngresos.Splits(0).DisplayColumns(4).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(5).Width = 61
        Me.TDGridIngresos.Splits(0).DisplayColumns(5).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(6).Width = 61
        Me.TDGridIngresos.Splits(0).DisplayColumns(6).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(7).Width = 61
        Me.TDGridIngresos.Splits(0).DisplayColumns(7).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(8).Width = 61
        Me.TDGridIngresos.Splits(0).DisplayColumns(8).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(9).Width = 61
        Me.TDGridIngresos.Splits(0).DisplayColumns(9).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(10).Width = 70
        Me.TDGridIngresos.Splits(0).DisplayColumns(10).Locked = False
        Me.TDGridIngresos.Columns(9).Caption = "Total Litros"
        Me.TDGridIngresos.Columns(10).NumberFormat = "##,##0.00"
        Me.TDGridIngresos.Columns(10).Caption = "PrecioUnit"
        Me.TDGridIngresos.Splits(0).DisplayColumns(11).Width = 80
        Me.TDGridIngresos.Splits(0).DisplayColumns(11).Locked = True
        Me.TDGridIngresos.Columns(11).NumberFormat = "##,##0.00"


        SqlString = "SELECT Detalle_Nomina.CodProductor, Productor.NombreProductor + ' ' + Productor.ApellidoProductor AS Nombres, Detalle_Nomina.IR,Detalle_Nomina.DeduccionPolicia, Detalle_Nomina.Anticipo, Detalle_Nomina.DeduccionTransporte, Detalle_Nomina.Pulperia,Detalle_Nomina.Inseminacion, Detalle_Nomina.Trazabilidad, Detalle_Nomina.ProductosVeterinarios,Detalle_Nomina.OtrasDeducciones, Detalle_Nomina.IR + Detalle_Nomina.DeduccionPolicia + Detalle_Nomina.Anticipo + Detalle_Nomina.DeduccionTransporte + Detalle_Nomina.Pulperia + Detalle_Nomina.Inseminacion + Detalle_Nomina.ProductosVeterinarios + Detalle_Nomina.OtrasDeducciones + Detalle_Nomina.Trazabilidad AS TotalEgresos, Detalle_Nomina.TotalIngresos - (Detalle_Nomina.IR + Detalle_Nomina.DeduccionPolicia + Detalle_Nomina.Anticipo + Detalle_Nomina.DeduccionTransporte + Detalle_Nomina.Pulperia + Detalle_Nomina.Inseminacion + Detalle_Nomina.ProductosVeterinarios + Detalle_Nomina.OtrasDeducciones + Detalle_Nomina.Trazabilidad) AS NetoPagar FROM  Detalle_Nomina INNER JOIN Productor ON Detalle_Nomina.CodProductor = Productor.CodProductor AND Detalle_Nomina.TipoProductor = Productor.TipoProductor " & _
            "WHERE (Detalle_Nomina.NumNomina = '" & Me.TxtNumNomina.Text & "') AND (Detalle_Nomina.TipoProductor = 'Productor')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleEgresos")
        Me.TDGridDeducciones.DataSource = DataSet.Tables("DetalleEgresos")
        Me.TDGridDeducciones.Splits(0).DisplayColumns(0).Width = 50
        Me.TDGridDeducciones.Columns(0).Caption = "Codigo"
        Me.TDGridDeducciones.Splits(0).DisplayColumns(0).Locked = True
        Me.TDGridDeducciones.Splits(0).DisplayColumns(1).Width = 180
        Me.TDGridDeducciones.Splits(0).DisplayColumns(1).Locked = True
        Me.TDGridDeducciones.Splits(0).DisplayColumns(2).Width = 60
        Me.TDGridDeducciones.Splits(0).DisplayColumns(2).Locked = True
        Me.TDGridDeducciones.Columns(2).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Splits(0).DisplayColumns(3).Width = 60
        Me.TDGridDeducciones.Splits(0).DisplayColumns(3).Locked = True
        Me.TDGridDeducciones.Splits(0).DisplayColumns("DeduccionPolicia").Visible = False
        Me.TDGridDeducciones.Columns(3).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Columns(3).Caption = "Policia"
        Me.TDGridDeducciones.Columns(4).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Splits(0).DisplayColumns(4).Width = 60
        Me.TDGridDeducciones.Splits(0).DisplayColumns(4).Locked = True
        Me.TDGridDeducciones.Columns(5).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Splits(0).DisplayColumns(5).Width = 70
        Me.TDGridDeducciones.Splits(0).DisplayColumns(5).Locked = True
        Me.TDGridDeducciones.Columns(5).Caption = "Transporte"
        Me.TDGridDeducciones.Columns(6).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Splits(0).DisplayColumns(6).Width = 70
        Me.TDGridDeducciones.Splits(0).DisplayColumns(6).Locked = True
        Me.TDGridDeducciones.Columns(7).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Splits(0).DisplayColumns(7).Width = 70
        Me.TDGridDeducciones.Splits(0).DisplayColumns(7).Locked = True
        Me.TDGridDeducciones.Columns("Trazabilidad").NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Splits(0).DisplayColumns("Trazabilidad").Width = 70
        Me.TDGridDeducciones.Splits(0).DisplayColumns("Trazabilidad").Locked = True
        Me.TDGridDeducciones.Columns("ProductosVeterinarios").NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Splits(0).DisplayColumns("ProductosVeterinarios").Width = 70
        Me.TDGridDeducciones.Splits(0).DisplayColumns("ProductosVeterinarios").Locked = True
        Me.TDGridDeducciones.Columns("ProductosVeterinarios").Caption = "Veterinario"

        Me.TDGridDeducciones.Columns("OtrasDeducciones").Caption = "Otras"
        Me.TDGridDeducciones.Columns("OtrasDeducciones").NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Splits(0).DisplayColumns("OtrasDeducciones").Width = 80
        Me.TDGridDeducciones.Splits(0).DisplayColumns("OtrasDeducciones").Locked = True
        Me.TDGridDeducciones.Columns("TotalEgresos").NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Splits(0).DisplayColumns("TotalEgresos").Width = 80
        Me.TDGridDeducciones.Splits(0).DisplayColumns("TotalEgresos").Locked = True

        Me.TDGridDeducciones.Columns("NetoPagar").NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Splits(0).DisplayColumns("NetoPagar").Width = 80
        Me.TDGridDeducciones.Splits(0).DisplayColumns("NetoPagar").Locked = True

        'SqlString = "SELECT Detalle_Nomina.CodProductor, Productor.NombreProductor + ' ' + Productor.ApellidoProductor AS Nombres, Detalle_Nomina.IR,Detalle_Nomina.DeduccionPolicia, Detalle_Nomina.Anticipo, Detalle_Nomina.DeduccionTransporte, Detalle_Nomina.Pulperia,Detalle_Nomina.Inseminacion, Detalle_Nomina.ProductosVeterinarios,Detalle_Nomina.IR + Detalle_Nomina.DeduccionPolicia + Detalle_Nomina.Anticipo + Detalle_Nomina.DeduccionTransporte + Detalle_Nomina.Pulperia + Detalle_Nomina.Inseminacion + Detalle_Nomina.ProductosVeterinarios AS TotalEgresos, Detalle_Nomina.TotalIngresos - (Detalle_Nomina.IR + Detalle_Nomina.DeduccionPolicia + Detalle_Nomina.Anticipo + Detalle_Nomina.DeduccionTransporte + Detalle_Nomina.Pulperia + Detalle_Nomina.Inseminacion + Detalle_Nomina.ProductosVeterinarios) AS NetoPagar FROM  Detalle_Nomina INNER JOIN Productor ON Detalle_Nomina.CodProductor = Productor.CodProductor AND Detalle_Nomina.TipoProductor = Productor.TipoProductor " & _
        '            "WHERE (Detalle_Nomina.NumNomina = '" & Me.TxtNumNomina.Text & "') AND (Detalle_Nomina.TipoProductor = 'Productor')"
        'DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        'DataAdapter.Fill(DataSet, "DetalleEgresos")
        'Me.TDGridDeducciones.DataSource = DataSet.Tables("DetalleEgresos")
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(0).Width = 70
        'Me.TDGridDeducciones.Columns(0).Caption = "Codigo"
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(0).Locked = True
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(1).Width = 180
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(1).Locked = True
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(2).Width = 70
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(2).Locked = True
        'Me.TDGridDeducciones.Columns(2).NumberFormat = "##,##0.00"
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(3).Width = 70
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(3).Locked = True
        'Me.TDGridDeducciones.Columns(3).NumberFormat = "##,##0.00"
        'Me.TDGridDeducciones.Columns(3).Caption = "Policia"
        'Me.TDGridDeducciones.Columns(4).NumberFormat = "##,##0.00"
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(4).Width = 70
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(4).Locked = True
        'Me.TDGridDeducciones.Columns(5).NumberFormat = "##,##0.00"
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(5).Width = 70
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(5).Locked = True
        'Me.TDGridDeducciones.Columns(5).Caption = "Transporte"
        'Me.TDGridDeducciones.Columns(6).NumberFormat = "##,##0.00"
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(6).Width = 70
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(6).Locked = True
        'Me.TDGridDeducciones.Columns(7).NumberFormat = "##,##0.00"
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(7).Width = 70
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(7).Locked = True
        'Me.TDGridDeducciones.Columns(8).NumberFormat = "##,##0.00"
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(8).Width = 70
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(8).Locked = True
        'Me.TDGridDeducciones.Columns(8).Caption = "Veterinario"
        'Me.TDGridDeducciones.Columns(9).NumberFormat = "##,##0.00"
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(9).Width = 80
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(9).Locked = True
        'Me.TDGridDeducciones.Columns(10).NumberFormat = "##,##0.00"
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(10).Width = 80
        'Me.TDGridDeducciones.Splits(0).DisplayColumns(10).Locked = True

        Me.CmdNomina.Enabled = True
        Me.CmdColillas.Enabled = True
        Me.Button2.Enabled = True
        Me.CmdCerrar.Enabled = True



    End Sub

    Private Sub TDGridDeducciones2_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles TDGridDeducciones2.AfterUpdate
        Dim SqlString As String, CodProductor As String, NombreProductor As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim StrSqlUpdate As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Anticipo As Double, Transporte As Double, Pulperia As Double, Inseminacion As Double, Trazabilidad As Double, Otros As Double


        CodProductor = Me.TDGridDeducciones2.Columns(2).Text
        NombreProductor = Me.TDGridDeducciones2.Columns(4).Text
        If IsNumeric(Me.TDGridDeducciones2.Columns(6).Text) Then
            Anticipo = Me.TDGridDeducciones2.Columns(6).Text
        Else
            Anticipo = 0
        End If

        If IsNumeric(Me.TDGridDeducciones2.Columns(7).Text) Then
            Transporte = Me.TDGridDeducciones2.Columns(7).Text
        Else
            Transporte = 0
        End If

        If IsNumeric(Me.TDGridDeducciones2.Columns(8).Text) Then
            Pulperia = Me.TDGridDeducciones2.Columns(8).Text
        Else
            Pulperia = 0
        End If

        If IsNumeric(Me.TDGridDeducciones2.Columns(9).Text) Then
            Inseminacion = Me.TDGridDeducciones2.Columns(9).Text
        Else
            Inseminacion = 0
        End If

        If IsNumeric(Me.TDGridDeducciones2.Columns("Trazabilidad").Text) Then
            Trazabilidad = Me.TDGridDeducciones2.Columns("Trazabilidad").Text
        Else
            Trazabilidad = 0
        End If

        If IsNumeric(Me.TDGridDeducciones2.Columns("OtrasDeducciones").Text) Then
            Otros = Me.TDGridDeducciones2.Columns("OtrasDeducciones").Text
        Else
            Otros = 0
        End If


        SqlString = "SELECT  * FROM Deducciones_Planilla WHERE (NumNomina = '" & Me.TxtNumNomina.Text & "') AND (CodProductor = '" & CodProductor & "') AND (TipoProductor = 'Productor')ORDER BY CodProductor"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleDeduccion")
        If DataSet.Tables("DetalleDeduccion").Rows.Count = 0 Then
            '///////////SI EXISTE EL LO ACTUALIZO////////////////
            StrSqlUpdate = "INSERT INTO [Deducciones_Planilla] ([NumNomina],[CodProductor],[TipoProductor],[NombreProductor],[NoAnticipo],[Anticipo],[Transporte],[Pulperia],[Inseminacion],[Trazabilidad],[OtrasDeducciones]) " & _
                           "VALUES ('" & Me.TxtNumNomina.Text & "','" & CodProductor & "','Productor','" & NombreProductor & "','" & Me.TDGridDeducciones2.Columns(5).Text & "'," & Anticipo & "," & Transporte & "," & Pulperia & " ," & Inseminacion & "," & Trazabilidad & "," & Otros & ")"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(StrSqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            StrSqlUpdate = "UPDATE [Deducciones_Planilla] SET [NoAnticipo] = '" & Me.TDGridDeducciones2.Columns(5).Text & "' ,[Anticipo] = " & Anticipo & ",[Transporte] = " & Transporte & ",[Pulperia] = " & Pulperia & ",[Inseminacion] = " & Inseminacion & ", [Trazabilidad] = " & Trazabilidad & ", [OtrasDeducciones] = " & Otros & "  WHERE (NumNomina = '" & Me.TxtNumNomina.Text & "') AND (CodProductor = '" & CodProductor & "') AND (TipoProductor = 'Productor')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(StrSqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()
        End If




        Me.TDGridDeducciones2.Col = 2
    End Sub

    Private Sub TDGridDeducciones2_BeforeColEdit(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.BeforeColEditEventArgs) Handles TDGridDeducciones2.BeforeColEdit


    End Sub

    Private Sub TDGridDeducciones2_BeforeColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs) Handles TDGridDeducciones2.BeforeColUpdate
        Dim Cols As Double, CodProductor As String, SqlString As String
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter

        Cols = e.ColIndex

        Select Case Cols
            Case 2

                CodProductor = Me.TDGridDeducciones2.Columns(2).Text
                SqlString = "SELECT *  FROM Productor WHERE (CodProductor = '" & CodProductor & "') AND (TipoProductor = 'Productor')"
                DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
                DataAdapter.Fill(DataSet, "Productor")
                If DataSet.Tables("Productor").Rows.Count = 0 Then
                    MsgBox("El Producto Seleccionado no Existe", MsgBoxStyle.Critical, "Sistema Facturacion")
                    Exit Sub
                Else
                    Me.TDGridDeducciones2.Columns(2).Text = DataSet.Tables("Productor").Rows(0)("CodProductor")
                    Me.TDGridDeducciones2.Columns(4).Text = DataSet.Tables("Productor").Rows(0)("NombreProductor")
                End If

        End Select
    End Sub

    Private Sub TDGridDeducciones2_ButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles TDGridDeducciones2.ButtonClick
        Dim Nombres As String
        Quien = "CodigoProductor"
        My.Forms.FrmConsultas.ShowDialog()
        Me.TDGridDeducciones2.Columns(2).Text = My.Forms.FrmConsultas.Codigo
        Nombres = My.Forms.FrmConsultas.Nombres + " " + My.Forms.FrmConsultas.Apellidos
        Me.TDGridDeducciones2.Columns(4).Text = Nombres

    End Sub

    Private Sub TDGridDeducciones2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TDGridDeducciones2.Click

    End Sub

    Private Sub TrueDBGridComponentes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TDGridIngresos.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, idDeduccion As Double, NumNomina As String, CodProductor As String
        Dim StrSqlUpdate As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim Respuesta As Integer, SqlString As String


        Respuesta = MsgBox("Desea Elimnar el Registro", MsgBoxStyle.YesNo, "Zeus Acopio")
        If Respuesta <> 6 Then
            Exit Sub
        End If


        idDeduccion = Me.TDGridDeducciones2.Columns(0).Text
        NumNomina = Me.TxtNumNomina.Text
        CodProductor = Me.TDGridDeducciones2.Columns(2).Text


        '///////////SI EXISTE EL LO ACTUALIZO////////////////
        StrSqlUpdate = "DELETE FROM [Deducciones_Planilla] WHERE (IdDeduccion = " & idDeduccion & ") AND (NumNomina = '" & NumNomina & "') AND (CodProductor = '" & CodProductor & "') AND (TipoProductor = 'Productor')"
        MiConexion.Open()
        ComandoUpdate = New SqlClient.SqlCommand(StrSqlUpdate, MiConexion)
        iResultado = ComandoUpdate.ExecuteNonQuery
        MiConexion.Close()

        SqlString = "SELECT * FROM Deducciones_Planilla WHERE (NumNomina = '" & NumNomina & "')ORDER BY CodProductor"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Deducciones")
        Me.BindingDeducciones2.DataSource = DataSet.Tables("Deducciones")
        Me.TDGridDeducciones2.DataSource = Me.BindingDeducciones2
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(0).Visible = False
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(1).Visible = False
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(2).Button = True
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(2).Width = 100
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(3).Visible = False
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(4).Width = 156
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(5).Width = 78
        Me.TDGridDeducciones2.Columns(5).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(6).Width = 78
        Me.TDGridDeducciones2.Columns(6).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(7).Width = 78
        Me.TDGridDeducciones2.Columns(7).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(8).Width = 78
        Me.TDGridDeducciones2.Columns(8).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(9).Width = 78
        Me.TDGridDeducciones2.Columns(9).NumberFormat = "##,##0.00"
    End Sub

    Private Sub CmdNomina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdNomina.Click
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim ArepPlanilla As New ArepPlanilla, SqlDatos As String
        Dim RutaLogo As String, SqlDetalle As String
        Dim SQL As New DataDynamics.ActiveReports.DataSources.SqlDBDataSource


        SqlDatos = "SELECT * FROM DatosEmpresa"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlDatos, MiConexion)
        DataAdapter.Fill(DataSet, "DatosEmpresa")

        If Not DataSet.Tables("DatosEmpresa").Rows.Count = 0 Then


            ArepPlanilla.LblEncabezado.Text = DataSet.Tables("DatosEmpresa").Rows(0)("Nombre_Empresa")
            ArepPlanilla.LblDireccion.Text = DataSet.Tables("DatosEmpresa").Rows(0)("Direccion_Empresa")

            If Not IsDBNull(DataSet.Tables("DatosEmpresa").Rows(0)("Numero_Ruc")) Then
                ArepPlanilla.LblRuc.Text = "Numero RUC " & DataSet.Tables("DatosEmpresa").Rows(0)("Numero_Ruc")
            End If
            If Not IsDBNull(DataSet.Tables("DatosEmpresa").Rows(0)("Ruta_Logo")) Then
                RutaLogo = DataSet.Tables("DatosEmpresa").Rows(0)("Ruta_Logo")
                If Dir(RutaLogo) <> "" Then
                    ArepPlanilla.ImgLogo.Image = New System.Drawing.Bitmap(RutaLogo)
                End If

            End If
        End If


        ArepPlanilla.LblOrden.Text = Me.TxtNumNomina.Text
        ArepPlanilla.LblFechaOrden.Text = Me.DTPFechaFin.Value

        '///////////////////////////////////////BUSCO EL DETALLE DE LA COMPRA///////////////////////////////////////////////////////
        SqlDetalle = "SELECT Detalle_Nomina.NumNomina,Detalle_Nomina.CodProductor, Productor.NombreProductor + ' ' + Productor.ApellidoProductor AS Nombres, Detalle_Nomina.Lunes,Detalle_Nomina.Martes, Detalle_Nomina.Miercoles, Detalle_Nomina.Jueves, Detalle_Nomina.Viernes, Detalle_Nomina.Sabado,Detalle_Nomina.Domingo, Detalle_Nomina.Total, Detalle_Nomina.PrecioVenta, Detalle_Nomina.TotalIngresos, Detalle_Nomina.IR,Detalle_Nomina.DeduccionPolicia, Detalle_Nomina.Anticipo, Detalle_Nomina.DeduccionTransporte, Detalle_Nomina.Pulperia,Detalle_Nomina.Inseminacion, Detalle_Nomina.ProductosVeterinarios,Detalle_Nomina.IR + Detalle_Nomina.DeduccionPolicia + Detalle_Nomina.Anticipo + Detalle_Nomina.DeduccionTransporte + Detalle_Nomina.Pulperia + Detalle_Nomina.Inseminacion + Detalle_Nomina.ProductosVeterinarios AS TotalEgresos,Detalle_Nomina.TotalIngresos - (Detalle_Nomina.IR + Detalle_Nomina.DeduccionPolicia + Detalle_Nomina.Anticipo + Detalle_Nomina.DeduccionTransporte + Detalle_Nomina.Pulperia + Detalle_Nomina.Inseminacion + Detalle_Nomina.ProductosVeterinarios) AS NetoPagar FROM  Detalle_Nomina INNER JOIN Productor ON Detalle_Nomina.CodProductor = Productor.CodProductor AND Detalle_Nomina.TipoProductor = Productor.TipoProductor  " & _
                     "WHERE (Detalle_Nomina.NumNomina = '" & Me.TxtNumNomina.Text & "') AND (Detalle_Nomina.TipoProductor = 'Productor')"
        Sql.ConnectionString = Conexion
        Sql.SQL = SQlDetalle
        ArepPlanilla.DataSource = SQL
        ArepPlanilla.Document.Name = "Reporte de Planilla"
        Dim ViewerForm As New FrmViewer()
        ViewerForm.arvMain.Document = ArepPlanilla.Document
        ViewerForm.Show()
        ArepPlanilla.Run(False)
    End Sub

    Private Sub CmdColillas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdColillas.Click
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter, Mes As String = ""
        Dim ArepColillas As New ArepListaRecepcion, SqlDatos As String, FechaInicio As Date, FechaFin As Date
        Dim RutaLogo As String, SqlDetalle As String
        Dim SQL As New DataDynamics.ActiveReports.DataSources.SqlDBDataSource


        SqlDatos = "SELECT * FROM DatosEmpresa"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlDatos, MiConexion)
        DataAdapter.Fill(DataSet, "DatosEmpresa")

        If Not DataSet.Tables("DatosEmpresa").Rows.Count = 0 Then


            ArepColillas.LblEncabezado.Text = DataSet.Tables("DatosEmpresa").Rows(0)("Nombre_Empresa")
            ArepColillas.LblDireccion.Text = DataSet.Tables("DatosEmpresa").Rows(0)("Direccion_Empresa")

            If Not IsDBNull(DataSet.Tables("DatosEmpresa").Rows(0)("Numero_Ruc")) Then
                ArepColillas.LblRuc.Text = "Numero RUC " & DataSet.Tables("DatosEmpresa").Rows(0)("Numero_Ruc")
            End If
            If Not IsDBNull(DataSet.Tables("DatosEmpresa").Rows(0)("Ruta_Logo")) Then
                RutaLogo = DataSet.Tables("DatosEmpresa").Rows(0)("Ruta_Logo")
                If Dir(RutaLogo) <> "" Then
                    ArepColillas.ImgLogo.Image = New System.Drawing.Bitmap(RutaLogo)
                End If

            End If
        End If

        Select Case FechaFin.Month
            Case 1 : Mes = "Enero"
            Case 2 : Mes = "Febrero"
            Case 3 : Mes = "Marzo"
            Case 4 : Mes = "Abril"
            Case 5 : Mes = "Mayo"
            Case 6 : Mes = "Junio"
            Case 7 : Mes = "Julio"
            Case 8 : Mes = "Agosto"
            Case 9 : Mes = "Septiembre"
            Case 10 : Mes = "Octubre"
            Case 11 : Mes = "Noviembre"
            Case 12 : Mes = "Diciembre"
        End Select


        FechaInicio = Me.DTPFechaIni.Value
        FechaFin = Me.DTPFechaFin.Value
        ArepColillas.LblImpreso.Text = Format(Now, "dd/MM/yyyy")
        ArepColillas.LblRecepcion.Text = "Recepcion de Leche de la Semana del " & FechaInicio.Day & " al " & FechaFin.Day & " de " & Mes & " " & FechaFin.Year

        '///////////////////////////////////////BUSCO EL DETALLE DE LA COMPRA///////////////////////////////////////////////////////
        'SqlDetalle = "SELECT  Nomina.NumPlanilla, Nomina.FechaInicial, Nomina.FechaFinal, Productor.CodProductor,Productor.NombreProductor + ' ' + Productor.ApellidoProductor AS Nombres, Detalle_Nomina.Roc1, Detalle_Nomina.Lunes, Detalle_Nomina.Roc2, Detalle_Nomina.Martes, Detalle_Nomina.Roc3, Detalle_Nomina.Miercoles, Detalle_Nomina.Roc4, Detalle_Nomina.Jueves, Detalle_Nomina.Roc5, Detalle_Nomina.Viernes, Detalle_Nomina.Roc6, Detalle_Nomina.Sabado, Detalle_Nomina.Roc7, Detalle_Nomina.Domingo, Detalle_Nomina.Total, Ruta.Cod_Ruta, Ruta.Nombre_Ruta FROM  Detalle_Nomina INNER JOIN Nomina ON Detalle_Nomina.NumNomina = Nomina.NumPlanilla INNER JOIN Productor ON Detalle_Nomina.CodProductor = Productor.CodProductor AND Detalle_Nomina.TipoProductor = Productor.TipoProductor INNER JOIN Ruta ON Productor.CodRuta = Ruta.Cod_Ruta  " & _
        '             "WHERE (Nomina.NumPlanilla = '" & Me.TxtNumNomina.Text & "') ORDER BY Ruta.Cod_Ruta, Productor.CodProductor "

        SqlDetalle = "SELECT  Nomina.NumPlanilla, Nomina.FechaInicial, Nomina.FechaFinal, Productor.CodProductor, Productor.NombreProductor + ' ' + Productor.ApellidoProductor AS Nombres, Detalle_Nomina.Roc1, Detalle_Nomina.Lunes, Detalle_Nomina.Roc2, Detalle_Nomina.Martes, Detalle_Nomina.Roc3, Detalle_Nomina.Miercoles, Detalle_Nomina.Roc4, Detalle_Nomina.Jueves, Detalle_Nomina.Roc5, Detalle_Nomina.Viernes, Detalle_Nomina.Roc6, Detalle_Nomina.Sabado, Detalle_Nomina.Roc7, Detalle_Nomina.Domingo, Detalle_Nomina.Total, Ruta_Distribucion.CodRuta, Ruta_Distribucion.Nombre_Ruta FROM  Detalle_Nomina INNER JOIN Nomina ON Detalle_Nomina.NumNomina = Nomina.NumPlanilla INNER JOIN  Productor ON Detalle_Nomina.CodProductor = Productor.CodProductor AND Detalle_Nomina.TipoProductor = Productor.TipoProductor INNER JOIN  Ruta_Distribucion ON Productor.CodRuta = Ruta_Distribucion.CodRuta  " & _
                     "WHERE  (Nomina.NumPlanilla = '" & Me.TxtNumNomina.Text & "') ORDER BY Ruta_Distribucion.CodRuta, Productor.CodProductor"
        SQL.ConnectionString = Conexion
        SQL.SQL = SqlDetalle
        ArepColillas.DataSource = SQL
        ArepColillas.Document.Name = "Reporte de Planilla"
        Dim ViewerForm As New FrmViewer()
        ViewerForm.arvMain.Document = ArepColillas.Document
        ViewerForm.Show()
        ArepColillas.Run(False)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim ArepColilla As New ArepColillas, SqlDatos As String
        Dim SqlDetalle As String
        Dim SQL As New DataDynamics.ActiveReports.DataSources.SqlDBDataSource


        SqlDatos = "SELECT * FROM DatosEmpresa"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlDatos, MiConexion)
        DataAdapter.Fill(DataSet, "DatosEmpresa")

        If Not DataSet.Tables("DatosEmpresa").Rows.Count = 0 Then


            ArepColilla.LblEncabezado.Text = DataSet.Tables("DatosEmpresa").Rows(0)("Nombre_Empresa")


            'If Not IsDBNull(DataSet.Tables("DatosEmpresa").Rows(0)("Numero_Ruc")) Then
            '    ArepColilla.LblRuc.Text = "Numero RUC " & DataSet.Tables("DatosEmpresa").Rows(0)("Numero_Ruc")
            'End If
            'If Not IsDBNull(DataSet.Tables("DatosEmpresa").Rows(0)("Ruta_Logo")) Then
            '    RutaLogo = DataSet.Tables("DatosEmpresa").Rows(0)("Ruta_Logo")
            '    If Dir(RutaLogo) <> "" Then
            '        ArepPlanilla.ImgLogo.Image = New System.Drawing.Bitmap(RutaLogo)
            '    End If

            'End If
        End If

        ArepColilla.LblPeriodo.Text = "Desde     " & Format(Me.DTPFechaIni.Value, "dd/MM/yyyy") & "    Hasta     " & Format(Me.DTPFechaFin.Value, "dd/MM/yyyy")

        '///////////////////////////////////////BUSCO EL DETALLE DE LA COMPRA///////////////////////////////////////////////////////
        SqlDetalle = "SELECT  Detalle_Nomina.CodProductor, Productor.NombreProductor + ' ' + Productor.ApellidoProductor AS Nombres, Detalle_Nomina.Lunes,Detalle_Nomina.Martes, Detalle_Nomina.Miercoles, Detalle_Nomina.Jueves, Detalle_Nomina.Viernes, Detalle_Nomina.Sabado,Detalle_Nomina.Domingo, Detalle_Nomina.Total, Detalle_Nomina.PrecioVenta, Detalle_Nomina.TotalIngresos, Detalle_Nomina.IR,Detalle_Nomina.DeduccionPolicia, Detalle_Nomina.Anticipo, Detalle_Nomina.DeduccionTransporte, Detalle_Nomina.Pulperia,Detalle_Nomina.Inseminacion, Detalle_Nomina.ProductosVeterinarios,Detalle_Nomina.IR + Detalle_Nomina.DeduccionPolicia + Detalle_Nomina.Anticipo + Detalle_Nomina.DeduccionTransporte + Detalle_Nomina.Pulperia + Detalle_Nomina.Inseminacion + Detalle_Nomina.ProductosVeterinarios AS TotalEgresos,Detalle_Nomina.TotalIngresos - (Detalle_Nomina.IR + Detalle_Nomina.DeduccionPolicia + Detalle_Nomina.Anticipo + Detalle_Nomina.DeduccionTransporte + Detalle_Nomina.Pulperia + Detalle_Nomina.Inseminacion + Detalle_Nomina.ProductosVeterinarios) AS NetoPagar, Nomina.NumPlanilla,Nomina.FechaInicial, Nomina.FechaFinal FROM  Detalle_Nomina INNER JOIN Productor ON Detalle_Nomina.CodProductor = Productor.CodProductor AND Detalle_Nomina.TipoProductor = Productor.TipoProductor INNER JOIN Nomina ON Detalle_Nomina.NumNomina = Nomina.NumPlanilla  " & _
                     "WHERE (Detalle_Nomina.NumNomina = '" & Me.TxtNumNomina.Text & "') AND (Detalle_Nomina.TipoProductor = 'Productor')"
        SQL.ConnectionString = Conexion
        SQL.SQL = SqlDetalle
        ArepColilla.DataSource = SQL
        ArepColilla.Document.Name = "Reporte de Planilla"
        Dim ViewerForm As New FrmViewer()
        ViewerForm.arvMain.Document = ArepColilla.Document
        ViewerForm.Show()
        ArepColilla.Run(False)
    End Sub

    Private Sub CmdCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdCerrar.Click
        Dim Respuesta As Integer, FechaInicio As String
        Dim StrSqlUpdate As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim SqlString As String

        Respuesta = MsgBox("Desea Cerrar la Nomina", MsgBoxStyle.YesNo, "Zeus Acopio")
        If Respuesta = 7 Then
            Exit Sub
        End If

        FechaInicio = Format(Me.DTPFechaIni.Value, "yyyy-MM-dd")

        '///////////////////////////////////////////////////////////////////////////////////////////////////
        '/////////////////////////GENERO UNA COMPRA POR PROVEEDOR ////////////////////////////////////////
        ''///////////////////////////////////////////////////////////////////////////////////////////////////////
        'Dim iPosicion As Double, Registros As Double, CodProductor As String, Fecha As Date, Nombres As String, CantidadTotal As Double, Contador As Double

        'iPosicion = 0
        'Me.ProgressBar.Visible = True
        'Me.ProgressBar.Minimum = 0
        'Me.ProgressBar.Visible = True
        'Me.ProgressBar.Value = 0
        'Registros = DataSet.Tables("Productor").Rows.Count
        'Me.ProgressBar.Maximum = Registros
        'Do While iPosicion < Registros
        '    My.Application.DoEvents()
        '    CodProductor = DataSet.Tables("Productor").Rows(iPosicion)("CodProductor")
        '    Fecha = Me.DTPFechaIni.Value
        '    Nombres = DataSet.Tables("Productor").Rows(iPosicion)("NombreProductor") + " " + DataSet.Tables("Productor").Rows(iPosicion)("ApellidoProductor")
        '    Me.LblProcesando.Text = "PROCESANDO PRODUCTOR: " & CodProductor & " " & Nombres
        '    CantidadTotal = 0
        '    Contador = 1



        '    iPosicion = iPosicion + 1
        'Loop





        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////////////////////////ACTUALIZO LOS ENCABEZADOS DEl TIPO NOMINA//////////////////////////////////
        '//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        StrSqlUpdate = "UPDATE [TipoNomina] SET [Activa] = 0 WHERE (CodTipoNomina = '" & Me.CboTipoPlanilla.Text & "')"
        MiConexion.Open()
        ComandoUpdate = New SqlClient.SqlCommand(StrSqlUpdate, MiConexion)
        iResultado = ComandoUpdate.ExecuteNonQuery
        MiConexion.Close()

        StrSqlUpdate = "UPDATE [Periodos] SET [Actual] = 0,[Calculada] = 1  WHERE (TipoNomina = '" & Me.CboTipoPlanilla.Columns(1).Text & "') AND (Inicio = CONVERT(DATETIME, '" & FechaInicio & "', 102) AND [NumNomina] = '" & Me.TxtNumNomina.Text & "')"
        MiConexion.Open()
        ComandoUpdate = New SqlClient.SqlCommand(StrSqlUpdate, MiConexion)
        iResultado = ComandoUpdate.ExecuteNonQuery
        MiConexion.Close()

        SqlString = "SELECT TipoNomina.CodTipoNomina, TipoNomina.TipoNomina, Periodos.Actual, Periodos.Periodo, Periodos.Inicio, Periodos.Final FROM  TipoNomina INNER JOIN Periodos ON TipoNomina.TipoNomina = Periodos.TipoNomina  WHERE(Periodos.Actual = 1)"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "TipoNomina")
        Me.CboTipoPlanilla.DataSource = DataSet.Tables("TipoNomina")

        Me.TxtNumNomina.Text = ""



        SqlString = "SELECT * FROM Deducciones_Planilla WHERE (NumNomina = '-1')ORDER BY CodProductor"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Deducciones")
        Me.BindingDeducciones2.DataSource = DataSet.Tables("Deducciones")
        Me.TDGridDeducciones2.DataSource = Me.BindingDeducciones2
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(0).Visible = False
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(1).Visible = False
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(2).Button = True
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(2).Width = 100
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(3).Visible = False
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(4).Width = 156
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(5).Width = 78
        Me.TDGridDeducciones2.Columns(5).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(6).Width = 78
        Me.TDGridDeducciones2.Columns(6).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(7).Width = 78
        Me.TDGridDeducciones2.Columns(7).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(8).Width = 78
        Me.TDGridDeducciones2.Columns(8).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones2.Splits.Item(0).DisplayColumns(9).Width = 78
        Me.TDGridDeducciones2.Columns(9).NumberFormat = "##,##0.00"

        SqlString = "SELECT Detalle_Nomina.CodProductor, Productor.NombreProductor + ' ' + Productor.ApellidoProductor AS Nombres, Detalle_Nomina.Lunes,Detalle_Nomina.Martes, Detalle_Nomina.Miercoles, Detalle_Nomina.Jueves, Detalle_Nomina.Viernes, Detalle_Nomina.Sabado,Detalle_Nomina.Domingo, Detalle_Nomina.Total, Detalle_Nomina.PrecioVenta, Detalle_Nomina.TotalIngresos FROM Detalle_Nomina INNER JOIN Productor ON Detalle_Nomina.CodProductor = Productor.CodProductor AND Detalle_Nomina.TipoProductor = Productor.TipoProductor WHERE (Detalle_Nomina.NumNomina = '-1') AND (Detalle_Nomina.TipoProductor = 'Productor')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleIngresos")
        Me.TDGridIngresos.DataSource = DataSet.Tables("DetalleIngresos")
        Me.TDGridIngresos.Splits(0).DisplayColumns(0).Width = 70
        Me.TDGridIngresos.Columns(0).Caption = "Codigo"
        Me.TDGridIngresos.Splits(0).DisplayColumns(0).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(1).Width = 190
        Me.TDGridIngresos.Splits(0).DisplayColumns(1).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(2).Width = 61
        Me.TDGridIngresos.Splits(0).DisplayColumns(2).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(3).Width = 61
        Me.TDGridIngresos.Splits(0).DisplayColumns(3).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(4).Width = 61
        Me.TDGridIngresos.Splits(0).DisplayColumns(4).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(5).Width = 61
        Me.TDGridIngresos.Splits(0).DisplayColumns(5).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(6).Width = 61
        Me.TDGridIngresos.Splits(0).DisplayColumns(6).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(7).Width = 61
        Me.TDGridIngresos.Splits(0).DisplayColumns(7).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(8).Width = 61
        Me.TDGridIngresos.Splits(0).DisplayColumns(8).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(9).Width = 61
        Me.TDGridIngresos.Splits(0).DisplayColumns(9).Locked = True
        Me.TDGridIngresos.Splits(0).DisplayColumns(10).Width = 70
        Me.TDGridIngresos.Splits(0).DisplayColumns(10).Locked = True
        Me.TDGridIngresos.Columns(9).Caption = "Total Litros"
        Me.TDGridIngresos.Columns(10).NumberFormat = "##,##0.00"
        Me.TDGridIngresos.Columns(10).Caption = "PrecioUnit"
        Me.TDGridIngresos.Splits(0).DisplayColumns(11).Width = 80
        Me.TDGridIngresos.Splits(0).DisplayColumns(11).Locked = True
        Me.TDGridIngresos.Columns(11).NumberFormat = "##,##0.00"



        SqlString = "SELECT Detalle_Nomina.CodProductor, Productor.NombreProductor + ' ' + Productor.ApellidoProductor AS Nombres, Detalle_Nomina.IR,Detalle_Nomina.DeduccionPolicia, Detalle_Nomina.Anticipo, Detalle_Nomina.DeduccionTransporte, Detalle_Nomina.Pulperia,Detalle_Nomina.Inseminacion, Detalle_Nomina.ProductosVeterinarios,Detalle_Nomina.IR + Detalle_Nomina.DeduccionPolicia + Detalle_Nomina.Anticipo + Detalle_Nomina.DeduccionTransporte + Detalle_Nomina.Pulperia + Detalle_Nomina.Inseminacion + Detalle_Nomina.ProductosVeterinarios AS TotalEgresos, Detalle_Nomina.TotalIngresos - (Detalle_Nomina.IR + Detalle_Nomina.DeduccionPolicia + Detalle_Nomina.Anticipo + Detalle_Nomina.DeduccionTransporte + Detalle_Nomina.Pulperia + Detalle_Nomina.Inseminacion + Detalle_Nomina.ProductosVeterinarios) AS NetoPagar FROM  Detalle_Nomina INNER JOIN Productor ON Detalle_Nomina.CodProductor = Productor.CodProductor AND Detalle_Nomina.TipoProductor = Productor.TipoProductor " & _
                    "WHERE (Detalle_Nomina.NumNomina = '-1') AND (Detalle_Nomina.TipoProductor = 'Productor')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "DetalleEgresos")
        Me.TDGridDeducciones.DataSource = DataSet.Tables("DetalleEgresos")
        Me.TDGridDeducciones.Splits(0).DisplayColumns(0).Width = 70
        Me.TDGridDeducciones.Columns(0).Caption = "Codigo"
        Me.TDGridDeducciones.Splits(0).DisplayColumns(0).Locked = True
        Me.TDGridDeducciones.Splits(0).DisplayColumns(1).Width = 180
        Me.TDGridDeducciones.Splits(0).DisplayColumns(1).Locked = True
        Me.TDGridDeducciones.Splits(0).DisplayColumns(2).Width = 70
        Me.TDGridDeducciones.Splits(0).DisplayColumns(2).Locked = True
        Me.TDGridDeducciones.Columns(2).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Splits(0).DisplayColumns(3).Width = 70
        Me.TDGridDeducciones.Splits(0).DisplayColumns(3).Locked = True
        Me.TDGridDeducciones.Columns(3).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Columns(3).Caption = "Policia"
        Me.TDGridDeducciones.Columns(4).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Splits(0).DisplayColumns(4).Width = 70
        Me.TDGridDeducciones.Splits(0).DisplayColumns(4).Locked = True
        Me.TDGridDeducciones.Columns(5).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Splits(0).DisplayColumns(5).Width = 70
        Me.TDGridDeducciones.Splits(0).DisplayColumns(5).Locked = True
        Me.TDGridDeducciones.Columns(5).Caption = "Transporte"
        Me.TDGridDeducciones.Columns(6).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Splits(0).DisplayColumns(6).Width = 70
        Me.TDGridDeducciones.Splits(0).DisplayColumns(6).Locked = True
        Me.TDGridDeducciones.Columns(7).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Splits(0).DisplayColumns(7).Width = 70
        Me.TDGridDeducciones.Splits(0).DisplayColumns(7).Locked = True
        Me.TDGridDeducciones.Columns(8).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Splits(0).DisplayColumns(8).Width = 70
        Me.TDGridDeducciones.Splits(0).DisplayColumns(8).Locked = True
        Me.TDGridDeducciones.Columns(8).Caption = "Veterinario"
        Me.TDGridDeducciones.Columns(9).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Splits(0).DisplayColumns(9).Width = 80
        Me.TDGridDeducciones.Splits(0).DisplayColumns(9).Locked = True
        Me.TDGridDeducciones.Columns(10).NumberFormat = "##,##0.00"
        Me.TDGridDeducciones.Splits(0).DisplayColumns(10).Width = 80
        Me.TDGridDeducciones.Splits(0).DisplayColumns(10).Locked = True

        Me.CmdNomina.Enabled = False
        Me.CmdColillas.Enabled = False
        Me.Button2.Enabled = False
        Me.CmdCerrar.Enabled = False
        Me.CboTipoPlanilla.Text = ""

    End Sub
End Class