<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class ArepLiquidacion 
    Inherits DataDynamics.ActiveReports.ActiveReport3 

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
        End If
        MyBase.Dispose(disposing)
    End Sub
    
    'NOTE: The following procedure is required by the ActiveReports Designer
    'It can be modified using the ActiveReports Designer.
    'Do not modify it using the code editor.
    Private WithEvents PageHeader1 As DataDynamics.ActiveReports.PageHeader
    Private WithEvents Detail1 As DataDynamics.ActiveReports.Detail
    Private WithEvents PageFooter1 As DataDynamics.ActiveReports.PageFooter
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim OleDBDataSource1 As DataDynamics.ActiveReports.DataSources.OleDBDataSource = New DataDynamics.ActiveReports.DataSources.OleDBDataSource
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ArepLiquidacion))
        Me.PageHeader1 = New DataDynamics.ActiveReports.PageHeader
        Me.lblOrderNum = New DataDynamics.ActiveReports.Label
        Me.lblOrderDate = New DataDynamics.ActiveReports.Label
        Me.ImgLogo = New DataDynamics.ActiveReports.Picture
        Me.LblEncabezado = New DataDynamics.ActiveReports.Label
        Me.LblDireccion = New DataDynamics.ActiveReports.Label
        Me.LblRuc = New DataDynamics.ActiveReports.Label
        Me.LblOrden = New DataDynamics.ActiveReports.Label
        Me.LblFechaOrden = New DataDynamics.ActiveReports.Label
        Me.LblTipoCompra = New DataDynamics.ActiveReports.Label
        Me.Detail1 = New DataDynamics.ActiveReports.Detail
        Me.TextBox1 = New DataDynamics.ActiveReports.TextBox
        Me.TextBox2 = New DataDynamics.ActiveReports.TextBox
        Me.TextBox3 = New DataDynamics.ActiveReports.TextBox
        Me.TextBox4 = New DataDynamics.ActiveReports.TextBox
        Me.TextBox5 = New DataDynamics.ActiveReports.TextBox
        Me.TextBox6 = New DataDynamics.ActiveReports.TextBox
        Me.TextBox7 = New DataDynamics.ActiveReports.TextBox
        Me.PageFooter1 = New DataDynamics.ActiveReports.PageFooter
        Me.GroupHeader1 = New DataDynamics.ActiveReports.GroupHeader
        Me.lneBillTo = New DataDynamics.ActiveReports.Line
        Me.LblCodProveedor = New DataDynamics.ActiveReports.Label
        Me.LblNombres = New DataDynamics.ActiveReports.Label
        Me.LblApellidos = New DataDynamics.ActiveReports.Label
        Me.LblDireccionProveedor = New DataDynamics.ActiveReports.Label
        Me.LblTelefono = New DataDynamics.ActiveReports.Label
        Me.Label1 = New DataDynamics.ActiveReports.Label
        Me.Label5 = New DataDynamics.ActiveReports.Label
        Me.LblBodegas = New DataDynamics.ActiveReports.Label
        Me.Label2 = New DataDynamics.ActiveReports.Label
        Me.Label3 = New DataDynamics.ActiveReports.Label
        Me.Line1 = New DataDynamics.ActiveReports.Line
        Me.Label4 = New DataDynamics.ActiveReports.Label
        Me.Label6 = New DataDynamics.ActiveReports.Label
        Me.lblProductName = New DataDynamics.ActiveReports.Label
        Me.lblQty = New DataDynamics.ActiveReports.Label
        Me.lblDiscount = New DataDynamics.ActiveReports.Label
        Me.lblTotals = New DataDynamics.ActiveReports.Label
        Me.Label7 = New DataDynamics.ActiveReports.Label
        Me.lblUnitPrice = New DataDynamics.ActiveReports.Label
        Me.lblProductID = New DataDynamics.ActiveReports.Label
        Me.GroupFooter1 = New DataDynamics.ActiveReports.GroupFooter
        Me.Shape1 = New DataDynamics.ActiveReports.Shape
        Me.Label8 = New DataDynamics.ActiveReports.Label
        Me.Label10 = New DataDynamics.ActiveReports.Label
        Me.Label12 = New DataDynamics.ActiveReports.Label
        Me.Label20 = New DataDynamics.ActiveReports.Label
        Me.Label36 = New DataDynamics.ActiveReports.Label
        Me.Label37 = New DataDynamics.ActiveReports.Label
        Me.TextBox8 = New DataDynamics.ActiveReports.TextBox
        Me.TextBox9 = New DataDynamics.ActiveReports.TextBox
        Me.TextBox10 = New DataDynamics.ActiveReports.TextBox
        Me.TextBox11 = New DataDynamics.ActiveReports.TextBox
        Me.TextBox12 = New DataDynamics.ActiveReports.TextBox
        Me.TextBox16 = New DataDynamics.ActiveReports.TextBox
        Me.SrpDetalleImpuesto = New DataDynamics.ActiveReports.SubReport
        Me.Label9 = New DataDynamics.ActiveReports.Label
        Me.Label11 = New DataDynamics.ActiveReports.Label
        Me.LblTotal = New DataDynamics.ActiveReports.Label
        Me.Label13 = New DataDynamics.ActiveReports.Label
        Me.Label14 = New DataDynamics.ActiveReports.Label
        Me.TextBox13 = New DataDynamics.ActiveReports.TextBox
        Me.Label15 = New DataDynamics.ActiveReports.Label
        Me.TextBox14 = New DataDynamics.ActiveReports.TextBox
        Me.Label16 = New DataDynamics.ActiveReports.Label
        Me.TextBox15 = New DataDynamics.ActiveReports.TextBox
        Me.Label17 = New DataDynamics.ActiveReports.Label
        Me.TextBox17 = New DataDynamics.ActiveReports.TextBox
        Me.Label18 = New DataDynamics.ActiveReports.Label
        Me.TextBox18 = New DataDynamics.ActiveReports.TextBox
        CType(Me.lblOrderNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOrderDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblEncabezado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblDireccion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblRuc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblOrden, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblFechaOrden, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTipoCompra, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblCodProveedor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblNombres, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblApellidos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblDireccionProveedor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTelefono, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblBodegas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProductName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotals, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnitPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProductID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label36, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PageHeader1
        '
        Me.PageHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.lblOrderNum, Me.lblOrderDate, Me.ImgLogo, Me.LblEncabezado, Me.LblDireccion, Me.LblRuc, Me.LblOrden, Me.LblFechaOrden, Me.LblTipoCompra})
        Me.PageHeader1.Height = 1.510417!
        Me.PageHeader1.Name = "PageHeader1"
        '
        'lblOrderNum
        '
        Me.lblOrderNum.Border.BottomColor = System.Drawing.Color.Black
        Me.lblOrderNum.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblOrderNum.Border.LeftColor = System.Drawing.Color.Black
        Me.lblOrderNum.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblOrderNum.Border.RightColor = System.Drawing.Color.Black
        Me.lblOrderNum.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblOrderNum.Border.TopColor = System.Drawing.Color.Black
        Me.lblOrderNum.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblOrderNum.Height = 0.1875!
        Me.lblOrderNum.HyperLink = Nothing
        Me.lblOrderNum.Left = 4.75!
        Me.lblOrderNum.Name = "lblOrderNum"
        Me.lblOrderNum.Style = "text-align: right; font-weight: bold; "
        Me.lblOrderNum.Text = "Liquidacion #:"
        Me.lblOrderNum.Top = 0.9375!
        Me.lblOrderNum.Width = 1.0!
        '
        'lblOrderDate
        '
        Me.lblOrderDate.Border.BottomColor = System.Drawing.Color.Black
        Me.lblOrderDate.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblOrderDate.Border.LeftColor = System.Drawing.Color.Black
        Me.lblOrderDate.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblOrderDate.Border.RightColor = System.Drawing.Color.Black
        Me.lblOrderDate.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblOrderDate.Border.TopColor = System.Drawing.Color.Black
        Me.lblOrderDate.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblOrderDate.Height = 0.1875!
        Me.lblOrderDate.HyperLink = Nothing
        Me.lblOrderDate.Left = 4.75!
        Me.lblOrderDate.Name = "lblOrderDate"
        Me.lblOrderDate.Style = "text-align: right; font-weight: bold; "
        Me.lblOrderDate.Text = "Fecha Liq:"
        Me.lblOrderDate.Top = 1.125!
        Me.lblOrderDate.Width = 1.0!
        '
        'ImgLogo
        '
        Me.ImgLogo.Border.BottomColor = System.Drawing.Color.Black
        Me.ImgLogo.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.ImgLogo.Border.LeftColor = System.Drawing.Color.Black
        Me.ImgLogo.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.ImgLogo.Border.RightColor = System.Drawing.Color.Black
        Me.ImgLogo.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.ImgLogo.Border.TopColor = System.Drawing.Color.Black
        Me.ImgLogo.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.ImgLogo.Height = 1.0!
        Me.ImgLogo.Image = Nothing
        Me.ImgLogo.ImageData = Nothing
        Me.ImgLogo.Left = 0.0!
        Me.ImgLogo.LineWeight = 0.0!
        Me.ImgLogo.Name = "ImgLogo"
        Me.ImgLogo.SizeMode = DataDynamics.ActiveReports.SizeModes.Stretch
        Me.ImgLogo.Top = 0.0!
        Me.ImgLogo.Width = 1.625!
        '
        'LblEncabezado
        '
        Me.LblEncabezado.Border.BottomColor = System.Drawing.Color.Black
        Me.LblEncabezado.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblEncabezado.Border.LeftColor = System.Drawing.Color.Black
        Me.LblEncabezado.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblEncabezado.Border.RightColor = System.Drawing.Color.Black
        Me.LblEncabezado.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblEncabezado.Border.TopColor = System.Drawing.Color.Black
        Me.LblEncabezado.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblEncabezado.Height = 0.625!
        Me.LblEncabezado.HyperLink = Nothing
        Me.LblEncabezado.Left = 1.6875!
        Me.LblEncabezado.Name = "LblEncabezado"
        Me.LblEncabezado.Style = "color: #404040; ddo-char-set: 0; text-align: center; font-weight: bold; backgroun" & _
            "d-color: #FFFFC0; font-size: 15.75pt; "
        Me.LblEncabezado.Text = ""
        Me.LblEncabezado.Top = 0.0!
        Me.LblEncabezado.Width = 3.0!
        '
        'LblDireccion
        '
        Me.LblDireccion.Border.BottomColor = System.Drawing.Color.Black
        Me.LblDireccion.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblDireccion.Border.LeftColor = System.Drawing.Color.Black
        Me.LblDireccion.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblDireccion.Border.RightColor = System.Drawing.Color.Black
        Me.LblDireccion.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblDireccion.Border.TopColor = System.Drawing.Color.Black
        Me.LblDireccion.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblDireccion.Height = 0.1875!
        Me.LblDireccion.HyperLink = Nothing
        Me.LblDireccion.Left = 0.0!
        Me.LblDireccion.Name = "LblDireccion"
        Me.LblDireccion.Style = "ddo-char-set: 0; font-style: italic; font-size: 8.25pt; "
        Me.LblDireccion.Text = ""
        Me.LblDireccion.Top = 1.0625!
        Me.LblDireccion.Width = 4.0!
        '
        'LblRuc
        '
        Me.LblRuc.Border.BottomColor = System.Drawing.Color.Black
        Me.LblRuc.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblRuc.Border.LeftColor = System.Drawing.Color.Black
        Me.LblRuc.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblRuc.Border.RightColor = System.Drawing.Color.Black
        Me.LblRuc.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblRuc.Border.TopColor = System.Drawing.Color.Black
        Me.LblRuc.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblRuc.Height = 0.1875!
        Me.LblRuc.HyperLink = Nothing
        Me.LblRuc.Left = 0.0!
        Me.LblRuc.Name = "LblRuc"
        Me.LblRuc.Style = "ddo-char-set: 0; font-style: italic; font-size: 8.25pt; "
        Me.LblRuc.Text = ""
        Me.LblRuc.Top = 1.25!
        Me.LblRuc.Width = 2.0!
        '
        'LblOrden
        '
        Me.LblOrden.Border.BottomColor = System.Drawing.Color.Black
        Me.LblOrden.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblOrden.Border.LeftColor = System.Drawing.Color.Black
        Me.LblOrden.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblOrden.Border.RightColor = System.Drawing.Color.Black
        Me.LblOrden.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblOrden.Border.TopColor = System.Drawing.Color.Black
        Me.LblOrden.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblOrden.DataField = "Numero_Liquidacion"
        Me.LblOrden.Height = 0.1875!
        Me.LblOrden.HyperLink = Nothing
        Me.LblOrden.Left = 5.8125!
        Me.LblOrden.Name = "LblOrden"
        Me.LblOrden.Style = ""
        Me.LblOrden.Text = ""
        Me.LblOrden.Top = 0.9375!
        Me.LblOrden.Width = 0.8125!
        '
        'LblFechaOrden
        '
        Me.LblFechaOrden.Border.BottomColor = System.Drawing.Color.Black
        Me.LblFechaOrden.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblFechaOrden.Border.LeftColor = System.Drawing.Color.Black
        Me.LblFechaOrden.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblFechaOrden.Border.RightColor = System.Drawing.Color.Black
        Me.LblFechaOrden.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblFechaOrden.Border.TopColor = System.Drawing.Color.Black
        Me.LblFechaOrden.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblFechaOrden.DataField = "Fecha_Liquidacion"
        Me.LblFechaOrden.Height = 0.1875!
        Me.LblFechaOrden.HyperLink = Nothing
        Me.LblFechaOrden.Left = 5.8125!
        Me.LblFechaOrden.Name = "LblFechaOrden"
        Me.LblFechaOrden.Style = ""
        Me.LblFechaOrden.Text = ""
        Me.LblFechaOrden.Top = 1.125!
        Me.LblFechaOrden.Width = 0.8125!
        '
        'LblTipoCompra
        '
        Me.LblTipoCompra.Border.BottomColor = System.Drawing.Color.Black
        Me.LblTipoCompra.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblTipoCompra.Border.LeftColor = System.Drawing.Color.Black
        Me.LblTipoCompra.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblTipoCompra.Border.RightColor = System.Drawing.Color.Black
        Me.LblTipoCompra.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblTipoCompra.Border.TopColor = System.Drawing.Color.Black
        Me.LblTipoCompra.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblTipoCompra.Height = 0.5!
        Me.LblTipoCompra.HyperLink = Nothing
        Me.LblTipoCompra.Left = 4.9375!
        Me.LblTipoCompra.Name = "LblTipoCompra"
        Me.LblTipoCompra.Style = "color: #404040; ddo-char-set: 0; text-align: center; font-weight: bold; backgroun" & _
            "d-color: #C0C0FF; font-size: 14.25pt; "
        Me.LblTipoCompra.Text = "Liquidacion de Productos"
        Me.LblTipoCompra.Top = 0.0!
        Me.LblTipoCompra.Width = 1.5625!
        '
        'Detail1
        '
        Me.Detail1.ColumnSpacing = 0.0!
        Me.Detail1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.TextBox1, Me.TextBox2, Me.TextBox3, Me.TextBox4, Me.TextBox5, Me.TextBox6, Me.TextBox7})
        Me.Detail1.Height = 0.2083333!
        Me.Detail1.Name = "Detail1"
        '
        'TextBox1
        '
        Me.TextBox1.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox1.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox1.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox1.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox1.DataField = "Cod_Producto"
        Me.TextBox1.Height = 0.1875!
        Me.TextBox1.Left = 0.0625!
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Style = "ddo-char-set: 0; font-size: 8.25pt; "
        Me.TextBox1.Text = Nothing
        Me.TextBox1.Top = 0.0!
        Me.TextBox1.Width = 0.6875!
        '
        'TextBox2
        '
        Me.TextBox2.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox2.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox2.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox2.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox2.DataField = "Descripcion_Producto"
        Me.TextBox2.Height = 0.1875!
        Me.TextBox2.Left = 0.75!
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Style = "ddo-char-set: 0; font-size: 8.25pt; "
        Me.TextBox2.Text = Nothing
        Me.TextBox2.Top = 0.0!
        Me.TextBox2.Width = 2.6875!
        '
        'TextBox3
        '
        Me.TextBox3.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox3.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox3.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox3.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox3.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox3.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox3.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox3.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox3.DataField = "Cantidad"
        Me.TextBox3.Height = 0.1875!
        Me.TextBox3.Left = 3.4375!
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Style = "ddo-char-set: 0; text-align: center; font-size: 8.25pt; "
        Me.TextBox3.Text = Nothing
        Me.TextBox3.Top = 0.0!
        Me.TextBox3.Width = 0.5!
        '
        'TextBox4
        '
        Me.TextBox4.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox4.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox4.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox4.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox4.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox4.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox4.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox4.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox4.DataField = "Precio_Compra"
        Me.TextBox4.Height = 0.1875!
        Me.TextBox4.Left = 3.9375!
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.OutputFormat = resources.GetString("TextBox4.OutputFormat")
        Me.TextBox4.Style = "ddo-char-set: 0; text-align: right; font-size: 8.25pt; "
        Me.TextBox4.Text = Nothing
        Me.TextBox4.Top = 0.0!
        Me.TextBox4.Width = 0.8125!
        '
        'TextBox5
        '
        Me.TextBox5.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox5.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox5.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox5.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox5.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox5.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox5.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox5.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox5.DataField = "Gasto_Compra"
        Me.TextBox5.Height = 0.1875!
        Me.TextBox5.Left = 4.75!
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.OutputFormat = resources.GetString("TextBox5.OutputFormat")
        Me.TextBox5.Style = "ddo-char-set: 0; text-align: right; font-size: 8.25pt; "
        Me.TextBox5.Text = Nothing
        Me.TextBox5.Top = 0.0!
        Me.TextBox5.Width = 0.75!
        '
        'TextBox6
        '
        Me.TextBox6.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox6.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox6.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox6.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox6.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox6.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox6.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox6.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox6.DataField = "FOB"
        Me.TextBox6.Height = 0.1875!
        Me.TextBox6.Left = 5.5!
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.OutputFormat = resources.GetString("TextBox6.OutputFormat")
        Me.TextBox6.Style = "ddo-char-set: 0; text-align: right; font-size: 8.25pt; "
        Me.TextBox6.Text = Nothing
        Me.TextBox6.Top = 0.0!
        Me.TextBox6.Width = 0.6875!
        '
        'TextBox7
        '
        Me.TextBox7.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox7.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox7.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox7.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox7.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox7.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox7.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox7.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox7.DataField = "Precio_Costo"
        Me.TextBox7.Height = 0.1875!
        Me.TextBox7.Left = 6.1875!
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.OutputFormat = resources.GetString("TextBox7.OutputFormat")
        Me.TextBox7.Style = "ddo-char-set: 0; text-align: right; font-size: 8.25pt; "
        Me.TextBox7.Text = Nothing
        Me.TextBox7.Top = 0.0!
        Me.TextBox7.Width = 0.75!
        '
        'PageFooter1
        '
        Me.PageFooter1.Height = 0.25!
        Me.PageFooter1.Name = "PageFooter1"
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.lneBillTo, Me.LblCodProveedor, Me.LblNombres, Me.LblApellidos, Me.LblDireccionProveedor, Me.LblTelefono, Me.Label1, Me.Label5, Me.LblBodegas, Me.Label2, Me.Label3, Me.Line1, Me.Label4, Me.Label6, Me.lblProductName, Me.lblQty, Me.lblDiscount, Me.lblTotals, Me.Label7, Me.lblUnitPrice, Me.lblProductID})
        Me.GroupHeader1.DataField = "Numero_Liquidacion"
        Me.GroupHeader1.Height = 1.59375!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'lneBillTo
        '
        Me.lneBillTo.Border.BottomColor = System.Drawing.Color.Black
        Me.lneBillTo.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lneBillTo.Border.LeftColor = System.Drawing.Color.Black
        Me.lneBillTo.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lneBillTo.Border.RightColor = System.Drawing.Color.Black
        Me.lneBillTo.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lneBillTo.Border.TopColor = System.Drawing.Color.Black
        Me.lneBillTo.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lneBillTo.Height = 0.0!
        Me.lneBillTo.Left = 0.0!
        Me.lneBillTo.LineWeight = 2.0!
        Me.lneBillTo.Name = "lneBillTo"
        Me.lneBillTo.Top = 0.25!
        Me.lneBillTo.Width = 3.0!
        Me.lneBillTo.X1 = 0.0!
        Me.lneBillTo.X2 = 3.0!
        Me.lneBillTo.Y1 = 0.25!
        Me.lneBillTo.Y2 = 0.25!
        '
        'LblCodProveedor
        '
        Me.LblCodProveedor.Border.BottomColor = System.Drawing.Color.Black
        Me.LblCodProveedor.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblCodProveedor.Border.LeftColor = System.Drawing.Color.Black
        Me.LblCodProveedor.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblCodProveedor.Border.RightColor = System.Drawing.Color.Black
        Me.LblCodProveedor.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblCodProveedor.Border.TopColor = System.Drawing.Color.Black
        Me.LblCodProveedor.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblCodProveedor.DataField = "Nombre_Proveedor"
        Me.LblCodProveedor.Height = 0.1875!
        Me.LblCodProveedor.HyperLink = Nothing
        Me.LblCodProveedor.Left = 0.0!
        Me.LblCodProveedor.Name = "LblCodProveedor"
        Me.LblCodProveedor.Style = "ddo-char-set: 0; font-size: 8.25pt; "
        Me.LblCodProveedor.Text = ""
        Me.LblCodProveedor.Top = 0.3125!
        Me.LblCodProveedor.Width = 3.0!
        '
        'LblNombres
        '
        Me.LblNombres.Border.BottomColor = System.Drawing.Color.Black
        Me.LblNombres.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblNombres.Border.LeftColor = System.Drawing.Color.Black
        Me.LblNombres.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblNombres.Border.RightColor = System.Drawing.Color.Black
        Me.LblNombres.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblNombres.Border.TopColor = System.Drawing.Color.Black
        Me.LblNombres.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblNombres.DataField = "Apellido_Proveedor"
        Me.LblNombres.Height = 0.1875!
        Me.LblNombres.HyperLink = Nothing
        Me.LblNombres.Left = 0.0!
        Me.LblNombres.Name = "LblNombres"
        Me.LblNombres.Style = "ddo-char-set: 0; font-size: 8.25pt; "
        Me.LblNombres.Text = ""
        Me.LblNombres.Top = 0.5!
        Me.LblNombres.Width = 3.0!
        '
        'LblApellidos
        '
        Me.LblApellidos.Border.BottomColor = System.Drawing.Color.Black
        Me.LblApellidos.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblApellidos.Border.LeftColor = System.Drawing.Color.Black
        Me.LblApellidos.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblApellidos.Border.RightColor = System.Drawing.Color.Black
        Me.LblApellidos.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblApellidos.Border.TopColor = System.Drawing.Color.Black
        Me.LblApellidos.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblApellidos.Height = 0.1875!
        Me.LblApellidos.HyperLink = Nothing
        Me.LblApellidos.Left = 0.0!
        Me.LblApellidos.Name = "LblApellidos"
        Me.LblApellidos.Style = "ddo-char-set: 0; font-size: 8.25pt; "
        Me.LblApellidos.Text = ""
        Me.LblApellidos.Top = 0.6875!
        Me.LblApellidos.Width = 3.0!
        '
        'LblDireccionProveedor
        '
        Me.LblDireccionProveedor.Border.BottomColor = System.Drawing.Color.Black
        Me.LblDireccionProveedor.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblDireccionProveedor.Border.LeftColor = System.Drawing.Color.Black
        Me.LblDireccionProveedor.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblDireccionProveedor.Border.RightColor = System.Drawing.Color.Black
        Me.LblDireccionProveedor.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblDireccionProveedor.Border.TopColor = System.Drawing.Color.Black
        Me.LblDireccionProveedor.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblDireccionProveedor.Height = 0.1875!
        Me.LblDireccionProveedor.HyperLink = Nothing
        Me.LblDireccionProveedor.Left = 0.0!
        Me.LblDireccionProveedor.Name = "LblDireccionProveedor"
        Me.LblDireccionProveedor.Style = "ddo-char-set: 0; font-size: 8.25pt; "
        Me.LblDireccionProveedor.Text = ""
        Me.LblDireccionProveedor.Top = 0.875!
        Me.LblDireccionProveedor.Width = 3.0!
        '
        'LblTelefono
        '
        Me.LblTelefono.Border.BottomColor = System.Drawing.Color.Black
        Me.LblTelefono.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblTelefono.Border.LeftColor = System.Drawing.Color.Black
        Me.LblTelefono.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblTelefono.Border.RightColor = System.Drawing.Color.Black
        Me.LblTelefono.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblTelefono.Border.TopColor = System.Drawing.Color.Black
        Me.LblTelefono.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblTelefono.Height = 0.1875!
        Me.LblTelefono.HyperLink = Nothing
        Me.LblTelefono.Left = 0.0!
        Me.LblTelefono.Name = "LblTelefono"
        Me.LblTelefono.Style = "ddo-char-set: 0; font-size: 8.25pt; "
        Me.LblTelefono.Text = ""
        Me.LblTelefono.Top = 1.0625!
        Me.LblTelefono.Width = 3.0!
        '
        'Label1
        '
        Me.Label1.Border.BottomColor = System.Drawing.Color.Black
        Me.Label1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label1.Border.LeftColor = System.Drawing.Color.Black
        Me.Label1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label1.Border.RightColor = System.Drawing.Color.Black
        Me.Label1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label1.Border.TopColor = System.Drawing.Color.Black
        Me.Label1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label1.Height = 0.1979167!
        Me.Label1.HyperLink = Nothing
        Me.Label1.Left = 0.0!
        Me.Label1.Name = "Label1"
        Me.Label1.Style = "color: White; ddo-char-set: 0; text-align: center; font-weight: bold; background-" & _
            "color: #8080FF; font-size: 8.25pt; "
        Me.Label1.Text = "Proveedor"
        Me.Label1.Top = 0.0625!
        Me.Label1.Width = 1.0!
        '
        'Label5
        '
        Me.Label5.Border.BottomColor = System.Drawing.Color.Black
        Me.Label5.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label5.Border.LeftColor = System.Drawing.Color.Black
        Me.Label5.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label5.Border.RightColor = System.Drawing.Color.Black
        Me.Label5.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label5.Border.TopColor = System.Drawing.Color.Black
        Me.Label5.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label5.Height = 0.1875!
        Me.Label5.HyperLink = Nothing
        Me.Label5.Left = 5.125!
        Me.Label5.Name = "Label5"
        Me.Label5.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; "
        Me.Label5.Text = "Bodegas"
        Me.Label5.Top = 0.125!
        Me.Label5.Width = 1.4375!
        '
        'LblBodegas
        '
        Me.LblBodegas.Border.BottomColor = System.Drawing.Color.Black
        Me.LblBodegas.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblBodegas.Border.LeftColor = System.Drawing.Color.Black
        Me.LblBodegas.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblBodegas.Border.RightColor = System.Drawing.Color.Black
        Me.LblBodegas.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblBodegas.Border.TopColor = System.Drawing.Color.Black
        Me.LblBodegas.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblBodegas.Height = 0.1875!
        Me.LblBodegas.HyperLink = Nothing
        Me.LblBodegas.Left = 5.125!
        Me.LblBodegas.Name = "LblBodegas"
        Me.LblBodegas.Style = "ddo-char-set: 0; text-align: center; font-size: 8.25pt; "
        Me.LblBodegas.Text = ""
        Me.LblBodegas.Top = 0.375!
        Me.LblBodegas.Width = 1.4375!
        '
        'Label2
        '
        Me.Label2.Border.BottomColor = System.Drawing.Color.Black
        Me.Label2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label2.Border.LeftColor = System.Drawing.Color.Black
        Me.Label2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label2.Border.RightColor = System.Drawing.Color.Black
        Me.Label2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label2.Border.TopColor = System.Drawing.Color.Black
        Me.Label2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label2.Height = 0.1875!
        Me.Label2.HyperLink = Nothing
        Me.Label2.Left = 3.25!
        Me.Label2.Name = "Label2"
        Me.Label2.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; vertical-align: middle; "
        Me.Label2.Text = "Moneda Liquidacion"
        Me.Label2.Top = 0.0625!
        Me.Label2.Width = 1.4375!
        '
        'Label3
        '
        Me.Label3.Border.BottomColor = System.Drawing.Color.Black
        Me.Label3.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label3.Border.LeftColor = System.Drawing.Color.Black
        Me.Label3.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label3.Border.RightColor = System.Drawing.Color.Black
        Me.Label3.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label3.Border.TopColor = System.Drawing.Color.Black
        Me.Label3.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label3.DataField = "MonedaLiquidacion"
        Me.Label3.Height = 0.1875!
        Me.Label3.HyperLink = Nothing
        Me.Label3.Left = 3.25!
        Me.Label3.Name = "Label3"
        Me.Label3.Style = "ddo-char-set: 0; text-align: center; font-size: 8.25pt; "
        Me.Label3.Text = ""
        Me.Label3.Top = 0.3125!
        Me.Label3.Width = 1.4375!
        '
        'Line1
        '
        Me.Line1.Border.BottomColor = System.Drawing.Color.Black
        Me.Line1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line1.Border.LeftColor = System.Drawing.Color.Black
        Me.Line1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line1.Border.RightColor = System.Drawing.Color.Black
        Me.Line1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line1.Border.TopColor = System.Drawing.Color.Black
        Me.Line1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line1.Height = 0.0!
        Me.Line1.Left = 5.125!
        Me.Line1.LineWeight = 2.0!
        Me.Line1.Name = "Line1"
        Me.Line1.Top = 0.3125!
        Me.Line1.Width = 1.4375!
        Me.Line1.X1 = 5.125!
        Me.Line1.X2 = 6.5625!
        Me.Line1.Y1 = 0.3125!
        Me.Line1.Y2 = 0.3125!
        '
        'Label4
        '
        Me.Label4.Border.BottomColor = System.Drawing.Color.Black
        Me.Label4.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label4.Border.LeftColor = System.Drawing.Color.Black
        Me.Label4.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label4.Border.RightColor = System.Drawing.Color.Black
        Me.Label4.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label4.Border.TopColor = System.Drawing.Color.Black
        Me.Label4.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label4.Height = 0.1875!
        Me.Label4.HyperLink = Nothing
        Me.Label4.Left = 3.25!
        Me.Label4.Name = "Label4"
        Me.Label4.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; vertical-align: middle; "
        Me.Label4.Text = "Moneda Impuestos"
        Me.Label4.Top = 0.625!
        Me.Label4.Width = 1.4375!
        '
        'Label6
        '
        Me.Label6.Border.BottomColor = System.Drawing.Color.Black
        Me.Label6.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label6.Border.LeftColor = System.Drawing.Color.Black
        Me.Label6.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label6.Border.RightColor = System.Drawing.Color.Black
        Me.Label6.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label6.Border.TopColor = System.Drawing.Color.Black
        Me.Label6.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label6.DataField = "MonedaImpuestos"
        Me.Label6.Height = 0.1875!
        Me.Label6.HyperLink = Nothing
        Me.Label6.Left = 3.25!
        Me.Label6.Name = "Label6"
        Me.Label6.Style = "ddo-char-set: 0; text-align: center; font-size: 8.25pt; "
        Me.Label6.Text = ""
        Me.Label6.Top = 0.875!
        Me.Label6.Width = 1.4375!
        '
        'lblProductName
        '
        Me.lblProductName.Border.BottomColor = System.Drawing.Color.Black
        Me.lblProductName.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblProductName.Border.LeftColor = System.Drawing.Color.Black
        Me.lblProductName.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblProductName.Border.RightColor = System.Drawing.Color.Black
        Me.lblProductName.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblProductName.Border.TopColor = System.Drawing.Color.Black
        Me.lblProductName.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblProductName.Height = 0.1875!
        Me.lblProductName.HyperLink = Nothing
        Me.lblProductName.Left = 0.75!
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; "
        Me.lblProductName.Text = "Nombre Producto"
        Me.lblProductName.Top = 1.375!
        Me.lblProductName.Width = 2.6875!
        '
        'lblQty
        '
        Me.lblQty.Border.BottomColor = System.Drawing.Color.Black
        Me.lblQty.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblQty.Border.LeftColor = System.Drawing.Color.Black
        Me.lblQty.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblQty.Border.RightColor = System.Drawing.Color.Black
        Me.lblQty.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblQty.Border.TopColor = System.Drawing.Color.Black
        Me.lblQty.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblQty.Height = 0.1875!
        Me.lblQty.HyperLink = Nothing
        Me.lblQty.Left = 3.4375!
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Style = "color: rgb(255,255,255); ddo-char-set: 0; text-align: center; font-weight: bold; " & _
            "background-color: #8080FF; font-size: 8.25pt; "
        Me.lblQty.Text = "Qty"
        Me.lblQty.Top = 1.375!
        Me.lblQty.Width = 0.5!
        '
        'lblDiscount
        '
        Me.lblDiscount.Border.BottomColor = System.Drawing.Color.Black
        Me.lblDiscount.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblDiscount.Border.LeftColor = System.Drawing.Color.Black
        Me.lblDiscount.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblDiscount.Border.RightColor = System.Drawing.Color.Black
        Me.lblDiscount.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblDiscount.Border.TopColor = System.Drawing.Color.Black
        Me.lblDiscount.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblDiscount.Height = 0.1875!
        Me.lblDiscount.HyperLink = Nothing
        Me.lblDiscount.Left = 4.75!
        Me.lblDiscount.Name = "lblDiscount"
        Me.lblDiscount.Style = "color: rgb(255,255,255); ddo-char-set: 0; text-align: center; font-weight: bold; " & _
            "background-color: #8080FF; font-size: 8.25pt; "
        Me.lblDiscount.Text = "Gastos"
        Me.lblDiscount.Top = 1.375!
        Me.lblDiscount.Width = 0.75!
        '
        'lblTotals
        '
        Me.lblTotals.Border.BottomColor = System.Drawing.Color.Black
        Me.lblTotals.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblTotals.Border.LeftColor = System.Drawing.Color.Black
        Me.lblTotals.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblTotals.Border.RightColor = System.Drawing.Color.Black
        Me.lblTotals.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblTotals.Border.TopColor = System.Drawing.Color.Black
        Me.lblTotals.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblTotals.Height = 0.1875!
        Me.lblTotals.HyperLink = Nothing
        Me.lblTotals.Left = 6.25!
        Me.lblTotals.Name = "lblTotals"
        Me.lblTotals.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; "
        Me.lblTotals.Text = "Precio Cto"
        Me.lblTotals.Top = 1.375!
        Me.lblTotals.Width = 0.75!
        '
        'Label7
        '
        Me.Label7.Border.BottomColor = System.Drawing.Color.Black
        Me.Label7.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label7.Border.LeftColor = System.Drawing.Color.Black
        Me.Label7.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label7.Border.RightColor = System.Drawing.Color.Black
        Me.Label7.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label7.Border.TopColor = System.Drawing.Color.Black
        Me.Label7.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label7.Height = 0.1875!
        Me.Label7.HyperLink = Nothing
        Me.Label7.Left = 5.5!
        Me.Label7.Name = "Label7"
        Me.Label7.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; "
        Me.Label7.Text = "FOB"
        Me.Label7.Top = 1.375!
        Me.Label7.Width = 0.75!
        '
        'lblUnitPrice
        '
        Me.lblUnitPrice.Border.BottomColor = System.Drawing.Color.Black
        Me.lblUnitPrice.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblUnitPrice.Border.LeftColor = System.Drawing.Color.Black
        Me.lblUnitPrice.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblUnitPrice.Border.RightColor = System.Drawing.Color.Black
        Me.lblUnitPrice.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblUnitPrice.Border.TopColor = System.Drawing.Color.Black
        Me.lblUnitPrice.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblUnitPrice.Height = 0.1875!
        Me.lblUnitPrice.HyperLink = Nothing
        Me.lblUnitPrice.Left = 3.9375!
        Me.lblUnitPrice.Name = "lblUnitPrice"
        Me.lblUnitPrice.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; "
        Me.lblUnitPrice.Text = "Precio Comp"
        Me.lblUnitPrice.Top = 1.375!
        Me.lblUnitPrice.Width = 0.8125!
        '
        'lblProductID
        '
        Me.lblProductID.Border.BottomColor = System.Drawing.Color.Black
        Me.lblProductID.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblProductID.Border.LeftColor = System.Drawing.Color.Black
        Me.lblProductID.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblProductID.Border.RightColor = System.Drawing.Color.Black
        Me.lblProductID.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblProductID.Border.TopColor = System.Drawing.Color.Black
        Me.lblProductID.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblProductID.Height = 0.1875!
        Me.lblProductID.HyperLink = Nothing
        Me.lblProductID.Left = 0.0625!
        Me.lblProductID.Name = "lblProductID"
        Me.lblProductID.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; "
        Me.lblProductID.Text = "Product ID"
        Me.lblProductID.Top = 1.375!
        Me.lblProductID.Width = 0.6875!
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.Shape1, Me.Label8, Me.Label10, Me.Label12, Me.Label20, Me.Label36, Me.Label37, Me.TextBox8, Me.TextBox9, Me.TextBox10, Me.TextBox11, Me.TextBox12, Me.TextBox16, Me.SrpDetalleImpuesto, Me.Label9, Me.Label11, Me.LblTotal, Me.Label13, Me.Label14, Me.TextBox13, Me.Label15, Me.TextBox14, Me.Label16, Me.TextBox15, Me.Label17, Me.TextBox17, Me.Label18, Me.TextBox18})
        Me.GroupFooter1.Height = 1.979167!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'Shape1
        '
        Me.Shape1.Border.BottomColor = System.Drawing.Color.Black
        Me.Shape1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Shape1.Border.LeftColor = System.Drawing.Color.Black
        Me.Shape1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Shape1.Border.RightColor = System.Drawing.Color.Black
        Me.Shape1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Shape1.Border.TopColor = System.Drawing.Color.Black
        Me.Shape1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Shape1.Height = 1.3125!
        Me.Shape1.Left = 0.0625!
        Me.Shape1.Name = "Shape1"
        Me.Shape1.RoundingRadius = 9.999999!
        Me.Shape1.Top = 0.5625!
        Me.Shape1.Width = 3.625!
        '
        'Label8
        '
        Me.Label8.Border.BottomColor = System.Drawing.Color.Black
        Me.Label8.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label8.Border.LeftColor = System.Drawing.Color.Black
        Me.Label8.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label8.Border.RightColor = System.Drawing.Color.Black
        Me.Label8.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label8.Border.TopColor = System.Drawing.Color.Black
        Me.Label8.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label8.Height = 0.1875!
        Me.Label8.HyperLink = Nothing
        Me.Label8.Left = 0.125!
        Me.Label8.Name = "Label8"
        Me.Label8.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; vertical-align: middle; "
        Me.Label8.Text = "Seguro"
        Me.Label8.Top = 0.625!
        Me.Label8.Width = 0.875!
        '
        'Label10
        '
        Me.Label10.Border.BottomColor = System.Drawing.Color.Black
        Me.Label10.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label10.Border.LeftColor = System.Drawing.Color.Black
        Me.Label10.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label10.Border.RightColor = System.Drawing.Color.Black
        Me.Label10.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label10.Border.TopColor = System.Drawing.Color.Black
        Me.Label10.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label10.Height = 0.1875!
        Me.Label10.HyperLink = Nothing
        Me.Label10.Left = 0.125!
        Me.Label10.Name = "Label10"
        Me.Label10.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; vertical-align: middle; "
        Me.Label10.Text = "Transporte"
        Me.Label10.Top = 0.875!
        Me.Label10.Width = 0.875!
        '
        'Label12
        '
        Me.Label12.Border.BottomColor = System.Drawing.Color.Black
        Me.Label12.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label12.Border.LeftColor = System.Drawing.Color.Black
        Me.Label12.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label12.Border.RightColor = System.Drawing.Color.Black
        Me.Label12.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label12.Border.TopColor = System.Drawing.Color.Black
        Me.Label12.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label12.Height = 0.1875!
        Me.Label12.HyperLink = Nothing
        Me.Label12.Left = 0.125!
        Me.Label12.Name = "Label12"
        Me.Label12.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; vertical-align: middle; "
        Me.Label12.Text = "Almacen"
        Me.Label12.Top = 1.125!
        Me.Label12.Width = 0.875!
        '
        'Label20
        '
        Me.Label20.Border.BottomColor = System.Drawing.Color.Black
        Me.Label20.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label20.Border.LeftColor = System.Drawing.Color.Black
        Me.Label20.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label20.Border.RightColor = System.Drawing.Color.Black
        Me.Label20.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label20.Border.TopColor = System.Drawing.Color.Black
        Me.Label20.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label20.Height = 0.1875!
        Me.Label20.HyperLink = Nothing
        Me.Label20.Left = 0.125!
        Me.Label20.Name = "Label20"
        Me.Label20.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; vertical-align: middle; "
        Me.Label20.Text = "Fletes"
        Me.Label20.Top = 1.375!
        Me.Label20.Width = 0.875!
        '
        'Label36
        '
        Me.Label36.Border.BottomColor = System.Drawing.Color.Black
        Me.Label36.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label36.Border.LeftColor = System.Drawing.Color.Black
        Me.Label36.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label36.Border.RightColor = System.Drawing.Color.Black
        Me.Label36.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label36.Border.TopColor = System.Drawing.Color.Black
        Me.Label36.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label36.Height = 0.1875!
        Me.Label36.HyperLink = Nothing
        Me.Label36.Left = 0.0625!
        Me.Label36.Name = "Label36"
        Me.Label36.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; vertical-align: middle; "
        Me.Label36.Text = "Gastos de Liquidacion"
        Me.Label36.Top = 0.375!
        Me.Label36.Width = 3.625!
        '
        'Label37
        '
        Me.Label37.Border.BottomColor = System.Drawing.Color.Black
        Me.Label37.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label37.Border.LeftColor = System.Drawing.Color.Black
        Me.Label37.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label37.Border.RightColor = System.Drawing.Color.Black
        Me.Label37.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label37.Border.TopColor = System.Drawing.Color.Black
        Me.Label37.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label37.Height = 0.1875!
        Me.Label37.HyperLink = Nothing
        Me.Label37.Left = 4.5!
        Me.Label37.Name = "Label37"
        Me.Label37.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; vertical-align: middle; "
        Me.Label37.Text = "Total"
        Me.Label37.Top = 0.0!
        Me.Label37.Width = 0.75!
        '
        'TextBox8
        '
        Me.TextBox8.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox8.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox8.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox8.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox8.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox8.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox8.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox8.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox8.DataField = "FOB"
        Me.TextBox8.Height = 0.1875!
        Me.TextBox8.Left = 5.25!
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.OutputFormat = resources.GetString("TextBox8.OutputFormat")
        Me.TextBox8.Style = "ddo-char-set: 0; text-align: right; font-weight: bold; font-size: 8.25pt; "
        Me.TextBox8.SummaryGroup = "GroupHeader1"
        Me.TextBox8.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.Group
        Me.TextBox8.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal
        Me.TextBox8.Text = Nothing
        Me.TextBox8.Top = 0.0!
        Me.TextBox8.Width = 0.75!
        '
        'TextBox9
        '
        Me.TextBox9.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox9.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox9.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox9.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox9.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox9.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox9.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox9.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox9.DataField = "Precio_Costo"
        Me.TextBox9.Height = 0.1875!
        Me.TextBox9.Left = 6.0!
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.OutputFormat = resources.GetString("TextBox9.OutputFormat")
        Me.TextBox9.Style = "ddo-char-set: 0; text-align: right; font-weight: bold; font-size: 8.25pt; "
        Me.TextBox9.SummaryGroup = "GroupHeader1"
        Me.TextBox9.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.Group
        Me.TextBox9.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal
        Me.TextBox9.Text = Nothing
        Me.TextBox9.Top = 0.0!
        Me.TextBox9.Width = 0.75!
        '
        'TextBox10
        '
        Me.TextBox10.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox10.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox10.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox10.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox10.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox10.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox10.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox10.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox10.DataField = "Seguro"
        Me.TextBox10.Height = 0.1875!
        Me.TextBox10.Left = 1.0!
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.OutputFormat = resources.GetString("TextBox10.OutputFormat")
        Me.TextBox10.Style = "ddo-char-set: 0; text-align: right; font-size: 8.25pt; "
        Me.TextBox10.Text = Nothing
        Me.TextBox10.Top = 0.625!
        Me.TextBox10.Width = 0.6875!
        '
        'TextBox11
        '
        Me.TextBox11.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox11.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox11.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox11.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox11.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox11.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox11.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox11.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox11.DataField = "Transporte"
        Me.TextBox11.Height = 0.1875!
        Me.TextBox11.Left = 1.0!
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.OutputFormat = resources.GetString("TextBox11.OutputFormat")
        Me.TextBox11.Style = "ddo-char-set: 0; text-align: right; font-size: 8.25pt; "
        Me.TextBox11.Text = Nothing
        Me.TextBox11.Top = 0.875!
        Me.TextBox11.Width = 0.6875!
        '
        'TextBox12
        '
        Me.TextBox12.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox12.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox12.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox12.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox12.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox12.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox12.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox12.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox12.DataField = "Almacen"
        Me.TextBox12.Height = 0.1875!
        Me.TextBox12.Left = 1.0!
        Me.TextBox12.Name = "TextBox12"
        Me.TextBox12.OutputFormat = resources.GetString("TextBox12.OutputFormat")
        Me.TextBox12.Style = "ddo-char-set: 0; text-align: right; font-size: 8.25pt; "
        Me.TextBox12.Text = Nothing
        Me.TextBox12.Top = 1.125!
        Me.TextBox12.Width = 0.6875!
        '
        'TextBox16
        '
        Me.TextBox16.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox16.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox16.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox16.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox16.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox16.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox16.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox16.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox16.DataField = "Fletes"
        Me.TextBox16.Height = 0.1875!
        Me.TextBox16.Left = 1.0!
        Me.TextBox16.Name = "TextBox16"
        Me.TextBox16.OutputFormat = resources.GetString("TextBox16.OutputFormat")
        Me.TextBox16.Style = "ddo-char-set: 0; text-align: right; font-size: 8.25pt; "
        Me.TextBox16.Text = Nothing
        Me.TextBox16.Top = 1.375!
        Me.TextBox16.Width = 0.6875!
        '
        'SrpDetalleImpuesto
        '
        Me.SrpDetalleImpuesto.Border.BottomColor = System.Drawing.Color.Black
        Me.SrpDetalleImpuesto.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.SrpDetalleImpuesto.Border.LeftColor = System.Drawing.Color.Black
        Me.SrpDetalleImpuesto.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.SrpDetalleImpuesto.Border.RightColor = System.Drawing.Color.Black
        Me.SrpDetalleImpuesto.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.SrpDetalleImpuesto.Border.TopColor = System.Drawing.Color.Black
        Me.SrpDetalleImpuesto.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.SrpDetalleImpuesto.CloseBorder = False
        Me.SrpDetalleImpuesto.Height = 1.0!
        Me.SrpDetalleImpuesto.Left = 4.75!
        Me.SrpDetalleImpuesto.Name = "SrpDetalleImpuesto"
        Me.SrpDetalleImpuesto.Report = Nothing
        Me.SrpDetalleImpuesto.ReportName = "SubReport1"
        Me.SrpDetalleImpuesto.Top = 0.5625!
        Me.SrpDetalleImpuesto.Width = 1.5625!
        '
        'Label9
        '
        Me.Label9.Border.BottomColor = System.Drawing.Color.Black
        Me.Label9.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label9.Border.LeftColor = System.Drawing.Color.Black
        Me.Label9.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label9.Border.RightColor = System.Drawing.Color.Black
        Me.Label9.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label9.Border.TopColor = System.Drawing.Color.Black
        Me.Label9.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label9.Height = 0.1875!
        Me.Label9.HyperLink = Nothing
        Me.Label9.Left = 4.75!
        Me.Label9.Name = "Label9"
        Me.Label9.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; "
        Me.Label9.Text = "Impuesto"
        Me.Label9.Top = 0.375!
        Me.Label9.Width = 0.875!
        '
        'Label11
        '
        Me.Label11.Border.BottomColor = System.Drawing.Color.Black
        Me.Label11.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label11.Border.LeftColor = System.Drawing.Color.Black
        Me.Label11.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label11.Border.RightColor = System.Drawing.Color.Black
        Me.Label11.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label11.Border.TopColor = System.Drawing.Color.Black
        Me.Label11.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label11.Height = 0.1875!
        Me.Label11.HyperLink = Nothing
        Me.Label11.Left = 5.625!
        Me.Label11.Name = "Label11"
        Me.Label11.Style = "color: rgb(255,255,255); ddo-char-set: 0; text-align: center; font-weight: bold; " & _
            "background-color: #8080FF; font-size: 8.25pt; "
        Me.Label11.Text = "Monto"
        Me.Label11.Top = 0.375!
        Me.Label11.Width = 0.6875!
        '
        'LblTotal
        '
        Me.LblTotal.Border.BottomColor = System.Drawing.Color.Black
        Me.LblTotal.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblTotal.Border.LeftColor = System.Drawing.Color.Black
        Me.LblTotal.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblTotal.Border.RightColor = System.Drawing.Color.Black
        Me.LblTotal.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblTotal.Border.TopColor = System.Drawing.Color.Black
        Me.LblTotal.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.LblTotal.Height = 0.1875!
        Me.LblTotal.HyperLink = Nothing
        Me.LblTotal.Left = 5.625!
        Me.LblTotal.Name = "LblTotal"
        Me.LblTotal.Style = "ddo-char-set: 0; text-align: right; font-weight: bold; font-size: 8.25pt; "
        Me.LblTotal.Text = ""
        Me.LblTotal.Top = 1.5625!
        Me.LblTotal.Width = 0.6875!
        '
        'Label13
        '
        Me.Label13.Border.BottomColor = System.Drawing.Color.Black
        Me.Label13.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label13.Border.LeftColor = System.Drawing.Color.Black
        Me.Label13.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label13.Border.RightColor = System.Drawing.Color.Black
        Me.Label13.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label13.Border.TopColor = System.Drawing.Color.Black
        Me.Label13.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label13.Height = 0.1875!
        Me.Label13.HyperLink = Nothing
        Me.Label13.Left = 4.75!
        Me.Label13.Name = "Label13"
        Me.Label13.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; "
        Me.Label13.Text = "Total Imp"
        Me.Label13.Top = 1.5625!
        Me.Label13.Width = 0.875!
        '
        'Label14
        '
        Me.Label14.Border.BottomColor = System.Drawing.Color.Black
        Me.Label14.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label14.Border.LeftColor = System.Drawing.Color.Black
        Me.Label14.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label14.Border.RightColor = System.Drawing.Color.Black
        Me.Label14.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label14.Border.TopColor = System.Drawing.Color.Black
        Me.Label14.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label14.Height = 0.1875!
        Me.Label14.HyperLink = Nothing
        Me.Label14.Left = 1.75!
        Me.Label14.Name = "Label14"
        Me.Label14.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; vertical-align: middle; "
        Me.Label14.Text = "Agente Aduana"
        Me.Label14.Top = 0.625!
        Me.Label14.Width = 0.9375!
        '
        'TextBox13
        '
        Me.TextBox13.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox13.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox13.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox13.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox13.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox13.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox13.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox13.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox13.DataField = "GtoAgenteAduana"
        Me.TextBox13.Height = 0.1875!
        Me.TextBox13.Left = 2.6875!
        Me.TextBox13.Name = "TextBox13"
        Me.TextBox13.OutputFormat = resources.GetString("TextBox13.OutputFormat")
        Me.TextBox13.Style = "ddo-char-set: 0; text-align: right; font-size: 8.25pt; "
        Me.TextBox13.Text = Nothing
        Me.TextBox13.Top = 0.625!
        Me.TextBox13.Width = 0.75!
        '
        'Label15
        '
        Me.Label15.Border.BottomColor = System.Drawing.Color.Black
        Me.Label15.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label15.Border.LeftColor = System.Drawing.Color.Black
        Me.Label15.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label15.Border.RightColor = System.Drawing.Color.Black
        Me.Label15.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label15.Border.TopColor = System.Drawing.Color.Black
        Me.Label15.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label15.Height = 0.1875!
        Me.Label15.HyperLink = Nothing
        Me.Label15.Left = 1.75!
        Me.Label15.Name = "Label15"
        Me.Label15.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; vertical-align: middle; "
        Me.Label15.Text = "Flete Interno"
        Me.Label15.Top = 0.875!
        Me.Label15.Width = 0.9375!
        '
        'TextBox14
        '
        Me.TextBox14.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox14.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox14.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox14.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox14.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox14.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox14.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox14.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox14.DataField = "GtoFletesInternos"
        Me.TextBox14.Height = 0.1875!
        Me.TextBox14.Left = 2.6875!
        Me.TextBox14.Name = "TextBox14"
        Me.TextBox14.OutputFormat = resources.GetString("TextBox14.OutputFormat")
        Me.TextBox14.Style = "ddo-char-set: 0; text-align: right; font-size: 8.25pt; "
        Me.TextBox14.Text = Nothing
        Me.TextBox14.Top = 0.875!
        Me.TextBox14.Width = 0.75!
        '
        'Label16
        '
        Me.Label16.Border.BottomColor = System.Drawing.Color.Black
        Me.Label16.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label16.Border.LeftColor = System.Drawing.Color.Black
        Me.Label16.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label16.Border.RightColor = System.Drawing.Color.Black
        Me.Label16.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label16.Border.TopColor = System.Drawing.Color.Black
        Me.Label16.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label16.Height = 0.1875!
        Me.Label16.HyperLink = Nothing
        Me.Label16.Left = 1.75!
        Me.Label16.Name = "Label16"
        Me.Label16.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; vertical-align: middle; "
        Me.Label16.Text = "Custodio"
        Me.Label16.Top = 1.125!
        Me.Label16.Width = 0.9375!
        '
        'TextBox15
        '
        Me.TextBox15.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox15.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox15.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox15.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox15.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox15.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox15.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox15.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox15.DataField = "GtoCustodio"
        Me.TextBox15.Height = 0.1875!
        Me.TextBox15.Left = 2.6875!
        Me.TextBox15.Name = "TextBox15"
        Me.TextBox15.OutputFormat = resources.GetString("TextBox15.OutputFormat")
        Me.TextBox15.Style = "ddo-char-set: 0; text-align: right; font-size: 8.25pt; "
        Me.TextBox15.Text = Nothing
        Me.TextBox15.Top = 1.125!
        Me.TextBox15.Width = 0.75!
        '
        'Label17
        '
        Me.Label17.Border.BottomColor = System.Drawing.Color.Black
        Me.Label17.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label17.Border.LeftColor = System.Drawing.Color.Black
        Me.Label17.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label17.Border.RightColor = System.Drawing.Color.Black
        Me.Label17.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label17.Border.TopColor = System.Drawing.Color.Black
        Me.Label17.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label17.Height = 0.1875!
        Me.Label17.HyperLink = Nothing
        Me.Label17.Left = 1.75!
        Me.Label17.Name = "Label17"
        Me.Label17.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; vertical-align: middle; "
        Me.Label17.Text = "Gastos Aduana"
        Me.Label17.Top = 1.375!
        Me.Label17.Width = 0.9375!
        '
        'TextBox17
        '
        Me.TextBox17.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox17.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox17.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox17.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox17.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox17.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox17.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox17.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox17.DataField = "GtoAduana"
        Me.TextBox17.Height = 0.1875!
        Me.TextBox17.Left = 2.6875!
        Me.TextBox17.Name = "TextBox17"
        Me.TextBox17.OutputFormat = resources.GetString("TextBox17.OutputFormat")
        Me.TextBox17.Style = "ddo-char-set: 0; text-align: right; font-size: 8.25pt; "
        Me.TextBox17.Text = Nothing
        Me.TextBox17.Top = 1.375!
        Me.TextBox17.Width = 0.75!
        '
        'Label18
        '
        Me.Label18.Border.BottomColor = System.Drawing.Color.Black
        Me.Label18.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label18.Border.LeftColor = System.Drawing.Color.Black
        Me.Label18.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label18.Border.RightColor = System.Drawing.Color.Black
        Me.Label18.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label18.Border.TopColor = System.Drawing.Color.Black
        Me.Label18.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label18.Height = 0.1875!
        Me.Label18.HyperLink = Nothing
        Me.Label18.Left = 1.75!
        Me.Label18.Name = "Label18"
        Me.Label18.Style = "color: rgb(255,255,255); text-align: center; font-weight: bold; background-color:" & _
            " #8080FF; font-size: 8.5pt; vertical-align: middle; "
        Me.Label18.Text = "Otros Gastos"
        Me.Label18.Top = 1.625!
        Me.Label18.Width = 0.9375!
        '
        'TextBox18
        '
        Me.TextBox18.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox18.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox18.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox18.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox18.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox18.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox18.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox18.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox18.DataField = "GtoOtros"
        Me.TextBox18.Height = 0.1875!
        Me.TextBox18.Left = 2.6875!
        Me.TextBox18.Name = "TextBox18"
        Me.TextBox18.OutputFormat = resources.GetString("TextBox18.OutputFormat")
        Me.TextBox18.Style = "ddo-char-set: 0; text-align: right; font-size: 8.25pt; "
        Me.TextBox18.Text = Nothing
        Me.TextBox18.Top = 1.625!
        Me.TextBox18.Width = 0.75!
        '
        'ArepLiquidacion
        '
        Me.MasterReport = False
        OleDBDataSource1.ConnectionString = "Provider=SQLOLEDB.1;Password=P@ssword;Persist Security Info=True;User ID=sa;Initi" & _
            "al Catalog=SistemaFacturacionRevetsa;Data Source=JUAN\SQL2005"
        OleDBDataSource1.SQL = resources.GetString("OleDBDataSource1.SQL")
        Me.DataSource = OleDBDataSource1
        Me.PageSettings.Margins.Left = 0.7!
        Me.PageSettings.Margins.Right = 0.5!
        Me.PageSettings.PaperHeight = 11.0!
        Me.PageSettings.PaperWidth = 8.5!
        Me.PrintWidth = 7.0625!
        Me.Sections.Add(Me.PageHeader1)
        Me.Sections.Add(Me.GroupHeader1)
        Me.Sections.Add(Me.Detail1)
        Me.Sections.Add(Me.GroupFooter1)
        Me.Sections.Add(Me.PageFooter1)
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" & _
                    "l; font-size: 10pt; color: Black; ", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold; ", "Heading1", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" & _
                    "lic; ", "Heading2", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold; ", "Heading3", "Normal"))
        CType(Me.lblOrderNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOrderDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblEncabezado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblDireccion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblRuc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblOrden, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblFechaOrden, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTipoCompra, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblCodProveedor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblNombres, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblApellidos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblDireccionProveedor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTelefono, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblBodegas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProductName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotals, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnitPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProductID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label36, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Private WithEvents lblOrderNum As DataDynamics.ActiveReports.Label
    Private WithEvents lblOrderDate As DataDynamics.ActiveReports.Label
    Friend WithEvents ImgLogo As DataDynamics.ActiveReports.Picture
    Friend WithEvents LblEncabezado As DataDynamics.ActiveReports.Label
    Friend WithEvents LblDireccion As DataDynamics.ActiveReports.Label
    Friend WithEvents LblRuc As DataDynamics.ActiveReports.Label
    Friend WithEvents LblOrden As DataDynamics.ActiveReports.Label
    Friend WithEvents LblFechaOrden As DataDynamics.ActiveReports.Label
    Friend WithEvents LblTipoCompra As DataDynamics.ActiveReports.Label
    Friend WithEvents GroupHeader1 As DataDynamics.ActiveReports.GroupHeader
    Friend WithEvents GroupFooter1 As DataDynamics.ActiveReports.GroupFooter
    Private WithEvents lneBillTo As DataDynamics.ActiveReports.Line
    Friend WithEvents LblCodProveedor As DataDynamics.ActiveReports.Label
    Friend WithEvents LblNombres As DataDynamics.ActiveReports.Label
    Friend WithEvents LblApellidos As DataDynamics.ActiveReports.Label
    Friend WithEvents LblDireccionProveedor As DataDynamics.ActiveReports.Label
    Friend WithEvents LblTelefono As DataDynamics.ActiveReports.Label
    Friend WithEvents Label1 As DataDynamics.ActiveReports.Label
    Private WithEvents Label5 As DataDynamics.ActiveReports.Label
    Friend WithEvents LblBodegas As DataDynamics.ActiveReports.Label
    Private WithEvents Label2 As DataDynamics.ActiveReports.Label
    Friend WithEvents Label3 As DataDynamics.ActiveReports.Label
    Private WithEvents Line1 As DataDynamics.ActiveReports.Line
    Private WithEvents Label4 As DataDynamics.ActiveReports.Label
    Friend WithEvents Label6 As DataDynamics.ActiveReports.Label
    Private WithEvents lblProductName As DataDynamics.ActiveReports.Label
    Private WithEvents lblQty As DataDynamics.ActiveReports.Label
    Private WithEvents lblDiscount As DataDynamics.ActiveReports.Label
    Private WithEvents lblTotals As DataDynamics.ActiveReports.Label
    Private WithEvents Label7 As DataDynamics.ActiveReports.Label
    Private WithEvents lblUnitPrice As DataDynamics.ActiveReports.Label
    Private WithEvents lblProductID As DataDynamics.ActiveReports.Label
    Friend WithEvents Shape1 As DataDynamics.ActiveReports.Shape
    Private WithEvents Label8 As DataDynamics.ActiveReports.Label
    Private WithEvents Label10 As DataDynamics.ActiveReports.Label
    Private WithEvents Label12 As DataDynamics.ActiveReports.Label
    Private WithEvents Label20 As DataDynamics.ActiveReports.Label
    Private WithEvents Label36 As DataDynamics.ActiveReports.Label
    Private WithEvents Label37 As DataDynamics.ActiveReports.Label
    Friend WithEvents TextBox1 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox2 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox3 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox4 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox5 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox6 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox7 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox8 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox9 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox10 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox11 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox12 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox16 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents SrpDetalleImpuesto As DataDynamics.ActiveReports.SubReport
    Private WithEvents Label9 As DataDynamics.ActiveReports.Label
    Private WithEvents Label11 As DataDynamics.ActiveReports.Label
    Friend WithEvents LblTotal As DataDynamics.ActiveReports.Label
    Private WithEvents Label13 As DataDynamics.ActiveReports.Label
    Private WithEvents Label14 As DataDynamics.ActiveReports.Label
    Friend WithEvents TextBox13 As DataDynamics.ActiveReports.TextBox
    Private WithEvents Label15 As DataDynamics.ActiveReports.Label
    Friend WithEvents TextBox14 As DataDynamics.ActiveReports.TextBox
    Private WithEvents Label16 As DataDynamics.ActiveReports.Label
    Friend WithEvents TextBox15 As DataDynamics.ActiveReports.TextBox
    Private WithEvents Label17 As DataDynamics.ActiveReports.Label
    Friend WithEvents TextBox17 As DataDynamics.ActiveReports.TextBox
    Private WithEvents Label18 As DataDynamics.ActiveReports.Label
    Friend WithEvents TextBox18 As DataDynamics.ActiveReports.TextBox
End Class