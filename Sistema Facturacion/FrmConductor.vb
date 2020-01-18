Public Class FrmConductor
    Public MiConexion As New SqlClient.SqlConnection(Conexion)
    Public Sub LimpiaConductor()
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim sql As String, ComandoUpdate As New SqlClient.SqlCommand 'iResultado As Integer
        Dim SqlProductos As String, SqlString As String, Ruta As String, LeeArchivo As String

        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////CARGO LOS CONDUCTORES////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlString = "SELECT Codigo, Nombre, Cedula, Licencia, Activo, ListaNegra, RazonListaNegra FROM Conductor "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Conductor")
        Me.CboCodigoConductor.DataSource = DataSet.Tables("Conductor")
        'If Not DataSet.Tables("Conductor").Rows.Count = 0 Then
        '    Me.CboCodigoConductor.Text = DataSet.Tables("Conductor").Rows(0)("Nombre")
        'End If
        'Me.CboCodigoConductor.Columns(0).Caption = "Codigo"

        Me.TxtCedula.Text = ""
        Me.TxtLicencia.Text = ""
        Me.TxtMotivo.Text = ""
        Me.TxtNombre.Text = ""
        Me.CboCodigoConductor.Text = ""
        Me.CboLstaNegra.Text = "No"
        Me.CboActivo.Text = "Activo"

    End Sub


    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Me.Close()
    End Sub



    Private Sub FrmConductor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim sql As String, ComandoUpdate As New SqlClient.SqlCommand 'iResultado As Integer
        Dim SqlProductos As String, SqlString As String, Ruta As String, LeeArchivo As String

        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////CARGO LOS CONDUCTORES////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SqlString = "SELECT Codigo, Nombre, Cedula, Licencia, Activo, ListaNegra, RazonListaNegra FROM Conductor "
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Conductor")
        Me.CboCodigoConductor.DataSource = DataSet.Tables("Conductor")
        'If Not DataSet.Tables("Conductor").Rows.Count = 0 Then
        '    Me.CboCodigoConductor.Text = DataSet.Tables("Conductor").Rows(0)("Nombre")
        'End If
        'Me.CboCodigoConductor.Columns(0).Caption = "Codigo"
    End Sub

    Private Sub C1Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Button2.Click
        Quien = "Conductor"
        My.Forms.FrmConsultas.ShowDialog()
        Me.CboCodigoConductor.Text = My.Forms.FrmConsultas.Codigo
    End Sub

    Private Sub CboCodigoConductor_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboCodigoConductor.TextChanged

        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim sql As String, ComandoUpdate As New SqlClient.SqlCommand 'iResultado As Integer
        Dim SqlProductos As String, SqlString As String, Codigo As String
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        '//////////////////////////CARGO LOS CONDUCTORES////////////////////////////////////////////////////////////////////
        '////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Codigo = Me.CboCodigoConductor.Columns("Codigo").Text
        SqlString = "SELECT Codigo, Nombre, Cedula, Licencia, Activo, ListaNegra, RazonListaNegra FROM Conductor WHERE (Codigo = '" & Codigo & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SqlString, MiConexion)
        DataAdapter.Fill(DataSet, "Conductor")
        If Not DataSet.Tables("Conductor").Rows.Count = 0 Then
            Me.TxtNombre.Text = DataSet.Tables("Conductor").Rows(0)("Nombre")
            Me.TxtCedula.Text = DataSet.Tables("Conductor").Rows(0)("Cedula")
            Me.TxtLicencia.Text = DataSet.Tables("Conductor").Rows(0)("Licencia")
            If Not IsDBNull(DataSet.Tables("Conductor").Rows(0)("RazonListaNegra")) Then
                Me.TxtMotivo.Text = DataSet.Tables("Conductor").Rows(0)("RazonListaNegra")
            End If

            If DataSet.Tables("Conductor").Rows(0)("Activo") = True Then
                Me.CboActivo.Text = "Activo"
            Else
                Me.CboActivo.Text = "Inactivo"
            End If

            If DataSet.Tables("Conductor").Rows(0)("ListaNegra") = True Then
                Me.CboLstaNegra.Text = "Activo"
            Else
                Me.CboLstaNegra.Text = "Inactivo"
            End If

        End If
        Me.CboCodigoConductor.Columns(0).Caption = "Codigo"

        MiConexion.Close()

    End Sub

    Private Sub ButtonNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNuevo.Click
        LimpiaConductor()
    End Sub

    Private Sub ButtonGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonGrabar.Click
        Dim SQLString As String, Activo As Double
        Dim DataSet As New DataSet, DataAdapter As New SqlClient.SqlDataAdapter
        Dim StrSqlUpdate As String, ComandoUpdate As New SqlClient.SqlCommand, iResultado As Integer
        Dim ListaNegra As Double


        If Me.CboActivo.Text = "Activo" Then
            Activo = 1
        Else
            Activo = 0
        End If

        If Me.CboLstaNegra.Text = "Activo" Then
            ListaNegra = 1
        Else
            ListaNegra = 0
        End If

        MiConexion.Close()

        SQLString = "SELECT  Codigo, Nombre, Cedula, Activo FROM Conductor WHERE (Activo = 1) AND (Codigo = '" & Me.CboCodigoConductor.Columns(0).Text & "')"
        DataAdapter = New SqlClient.SqlDataAdapter(SQLString, MiConexion)
        DataAdapter.Fill(DataSet, "Clientes")
        If Not DataSet.Tables("Clientes").Rows.Count = 0 Then

            '///////////SI EXISTE EL USUARIO LO ACTUALIZO////////////////
            StrSqlUpdate = "UPDATE [Conductor]  SET [Nombre] = '" & Me.TxtNombre.Text & "',[Cedula] = '" & Me.TxtCedula.Text & "',[Licencia] = '" & Me.TxtLicencia.Text & "'  ,[Activo] = " & Activo & ",[ListaNegra] = " & ListaNegra & ",[RazonListaNegra]= '" & Me.TxtMotivo.Text & "'  WHERE (Activo = 1) AND (Codigo = '" & Me.CboCodigoConductor.Text & "')"
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(StrSqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        Else
            '/////////SI NO EXISTE LO AGREGO COMO NUEVO/////////////////
            StrSqlUpdate = "INSERT INTO [Conductor] ([Codigo],[Nombre],[Cedula],[Licencia],[ListaNegra],[RazonListaNegra],[Activo]) VALUES ('" & Me.CboCodigoConductor.Columns(0).Text & "' ,'" & Me.TxtNombre.Text & "' ,'" & Me.TxtCedula.Text & "', '" & Me.TxtLicencia.Text & "', " & ListaNegra & ", '" & Me.TxtMotivo.Text & "', " & Activo & ") "
            MiConexion.Open()
            ComandoUpdate = New SqlClient.SqlCommand(StrSqlUpdate, MiConexion)
            iResultado = ComandoUpdate.ExecuteNonQuery
            MiConexion.Close()

        End If


        LimpiaConductor()

    End Sub

    Private Sub ButtonBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBorrar.Click

    End Sub
End Class