<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProductor
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmProductor))
        Me.LblTitulo = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.CmdGrabar = New System.Windows.Forms.Button
        Me.ButtonBorrar = New System.Windows.Forms.Button
        Me.CmdNuevo = New System.Windows.Forms.Button
        Me.Button8 = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TxtNumeroCedula = New System.Windows.Forms.MaskedTextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.CboCodigoProductor = New C1.Win.C1List.C1Combo
        Me.ChkCausaIVA = New System.Windows.Forms.CheckBox
        Me.CboRuta = New C1.Win.C1List.C1Combo
        Me.CboCooperativa = New C1.Win.C1List.C1Combo
        Me.CboDepartamentos = New C1.Win.C1List.C1Combo
        Me.CboEscolaridad = New C1.Win.C1List.C1Combo
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label28 = New System.Windows.Forms.Label
        Me.TxtCtaxPagar = New System.Windows.Forms.TextBox
        Me.Lbl = New System.Windows.Forms.Label
        Me.TxtCtaxCobrar = New System.Windows.Forms.TextBox
        Me.CboSexo = New System.Windows.Forms.ComboBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.DTFechaNacimientos = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.DTFechaAdmision = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.ChkActivo = New System.Windows.Forms.CheckBox
        Me.TxtApellidos = New System.Windows.Forms.TextBox
        Me.LblApellido = New System.Windows.Forms.Label
        Me.TxtDireccion = New System.Windows.Forms.TextBox
        Me.LblDireccion = New System.Windows.Forms.Label
        Me.TxtNombre = New System.Windows.Forms.TextBox
        Me.LblNombre = New System.Windows.Forms.Label
        Me.Button6 = New System.Windows.Forms.Button
        Me.LblCodigo = New System.Windows.Forms.Label
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.CmdAgregar = New System.Windows.Forms.Button
        Me.CmdBorrarFoto = New System.Windows.Forms.Button
        Me.ImgFoto = New System.Windows.Forms.PictureBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.TxtEstadoCivil = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.TxtEdad = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.TextBox11 = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.TxtComunidad = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.TxtCorreoElectronico = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.TxtFax = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.TxtTelefono = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TxtNumeroHijos = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TxtNombreConyugue = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.C1TrueDBGrid1 = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.OptTPrestada = New System.Windows.Forms.RadioButton
        Me.OptTAlquilada = New System.Windows.Forms.RadioButton
        Me.OptTReforma = New System.Windows.Forms.RadioButton
        Me.OptTComprada = New System.Windows.Forms.RadioButton
        Me.OptTHerencia = New System.Windows.Forms.RadioButton
        Me.OptTPropia = New System.Windows.Forms.RadioButton
        Me.Label18 = New System.Windows.Forms.Label
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.TxtVivienda = New System.Windows.Forms.TextBox
        Me.OptViviendaNo = New System.Windows.Forms.RadioButton
        Me.OptViviendaSi = New System.Windows.Forms.RadioButton
        Me.Label13 = New System.Windows.Forms.Label
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.OptFuenteNo = New System.Windows.Forms.RadioButton
        Me.OptFuenteSi = New System.Windows.Forms.RadioButton
        Me.Label12 = New System.Windows.Forms.Label
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.C1TrueDBGrid2 = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.TxtCualEmpresa = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.OptContratoNO = New System.Windows.Forms.RadioButton
        Me.OptContratoSi = New System.Windows.Forms.RadioButton
        Me.Label24 = New System.Windows.Forms.Label
        Me.TxtCantidadTerreno = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.TxtCantidadBueyes = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.TxtCantidadVaquillas = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.TxtCantidadVacas = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.TxtAreaPasto = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.CboCodigoProductor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboRuta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboCooperativa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboDepartamentos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboEscolaridad, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        CType(Me.ImgFoto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.C1TrueDBGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.C1TrueDBGrid2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.SuspendLayout()
        '
        'LblTitulo
        '
        Me.LblTitulo.AutoSize = True
        Me.LblTitulo.BackColor = System.Drawing.Color.FromArgb(CType(CType(161, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.LblTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblTitulo.Location = New System.Drawing.Point(242, 18)
        Me.LblTitulo.Name = "LblTitulo"
        Me.LblTitulo.Size = New System.Drawing.Size(194, 13)
        Me.LblTitulo.TabIndex = 114
        Me.LblTitulo.Text = "REGISTRO DE  PRODUCTORES"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(161, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(8, -1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(83, 60)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 113
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(161, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.PictureBox1.Location = New System.Drawing.Point(-3, -1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(677, 60)
        Me.PictureBox1.TabIndex = 112
        Me.PictureBox1.TabStop = False
        '
        'CmdGrabar
        '
        Me.CmdGrabar.Image = CType(resources.GetObject("CmdGrabar.Image"), System.Drawing.Image)
        Me.CmdGrabar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdGrabar.Location = New System.Drawing.Point(90, 392)
        Me.CmdGrabar.Name = "CmdGrabar"
        Me.CmdGrabar.Size = New System.Drawing.Size(78, 68)
        Me.CmdGrabar.TabIndex = 116
        Me.CmdGrabar.Text = "Grabar"
        Me.CmdGrabar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdGrabar.UseVisualStyleBackColor = True
        '
        'ButtonBorrar
        '
        Me.ButtonBorrar.Image = CType(resources.GetObject("ButtonBorrar.Image"), System.Drawing.Image)
        Me.ButtonBorrar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ButtonBorrar.Location = New System.Drawing.Point(174, 392)
        Me.ButtonBorrar.Name = "ButtonBorrar"
        Me.ButtonBorrar.Size = New System.Drawing.Size(75, 67)
        Me.ButtonBorrar.TabIndex = 117
        Me.ButtonBorrar.Text = "Eliminar"
        Me.ButtonBorrar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ButtonBorrar.UseVisualStyleBackColor = True
        '
        'CmdNuevo
        '
        Me.CmdNuevo.Image = CType(resources.GetObject("CmdNuevo.Image"), System.Drawing.Image)
        Me.CmdNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdNuevo.Location = New System.Drawing.Point(9, 392)
        Me.CmdNuevo.Name = "CmdNuevo"
        Me.CmdNuevo.Size = New System.Drawing.Size(75, 67)
        Me.CmdNuevo.TabIndex = 115
        Me.CmdNuevo.Text = "Nuevo"
        Me.CmdNuevo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdNuevo.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button8.Image = CType(resources.GetObject("Button8.Image"), System.Drawing.Image)
        Me.Button8.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button8.Location = New System.Drawing.Point(588, 394)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(75, 66)
        Me.Button8.TabIndex = 118
        Me.Button8.Text = "Salir"
        Me.Button8.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button8.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(8, 65)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(655, 323)
        Me.TabControl1.TabIndex = 119
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TxtNumeroCedula)
        Me.TabPage1.Controls.Add(Me.Label29)
        Me.TabPage1.Controls.Add(Me.CboCodigoProductor)
        Me.TabPage1.Controls.Add(Me.ChkCausaIVA)
        Me.TabPage1.Controls.Add(Me.CboRuta)
        Me.TabPage1.Controls.Add(Me.CboCooperativa)
        Me.TabPage1.Controls.Add(Me.CboDepartamentos)
        Me.TabPage1.Controls.Add(Me.CboEscolaridad)
        Me.TabPage1.Controls.Add(Me.Button2)
        Me.TabPage1.Controls.Add(Me.Button1)
        Me.TabPage1.Controls.Add(Me.Label28)
        Me.TabPage1.Controls.Add(Me.TxtCtaxPagar)
        Me.TabPage1.Controls.Add(Me.Lbl)
        Me.TabPage1.Controls.Add(Me.TxtCtaxCobrar)
        Me.TabPage1.Controls.Add(Me.CboSexo)
        Me.TabPage1.Controls.Add(Me.Label27)
        Me.TabPage1.Controls.Add(Me.Label26)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.DTFechaNacimientos)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.DTFechaAdmision)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.ChkActivo)
        Me.TabPage1.Controls.Add(Me.TxtApellidos)
        Me.TabPage1.Controls.Add(Me.LblApellido)
        Me.TabPage1.Controls.Add(Me.TxtDireccion)
        Me.TabPage1.Controls.Add(Me.LblDireccion)
        Me.TabPage1.Controls.Add(Me.TxtNombre)
        Me.TabPage1.Controls.Add(Me.LblNombre)
        Me.TabPage1.Controls.Add(Me.Button6)
        Me.TabPage1.Controls.Add(Me.LblCodigo)
        Me.TabPage1.Controls.Add(Me.GroupBox6)
        Me.TabPage1.Controls.Add(Me.ImgFoto)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(647, 297)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Datos Generales"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TxtNumeroCedula
        '
        Me.TxtNumeroCedula.Location = New System.Drawing.Point(485, 81)
        Me.TxtNumeroCedula.Mask = "0000000000000>A"
        Me.TxtNumeroCedula.Name = "TxtNumeroCedula"
        Me.TxtNumeroCedula.Size = New System.Drawing.Size(135, 20)
        Me.TxtNumeroCedula.TabIndex = 168
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(391, 86)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(40, 13)
        Me.Label29.TabIndex = 167
        Me.Label29.Text = "Cedula"
        '
        'CboCodigoProductor
        '
        Me.CboCodigoProductor.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.CboCodigoProductor.Caption = ""
        Me.CboCodigoProductor.CaptionHeight = 17
        Me.CboCodigoProductor.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CboCodigoProductor.ColumnCaptionHeight = 17
        Me.CboCodigoProductor.ColumnFooterHeight = 17
        Me.CboCodigoProductor.ContentHeight = 15
        Me.CboCodigoProductor.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.CboCodigoProductor.EditorBackColor = System.Drawing.SystemColors.Window
        Me.CboCodigoProductor.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboCodigoProductor.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.CboCodigoProductor.EditorHeight = 15
        Me.CboCodigoProductor.Images.Add(CType(resources.GetObject("CboCodigoProductor.Images"), System.Drawing.Image))
        Me.CboCodigoProductor.ItemHeight = 15
        Me.CboCodigoProductor.Location = New System.Drawing.Point(125, 139)
        Me.CboCodigoProductor.MatchEntryTimeout = CType(2000, Long)
        Me.CboCodigoProductor.MaxDropDownItems = CType(5, Short)
        Me.CboCodigoProductor.MaxLength = 32767
        Me.CboCodigoProductor.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.CboCodigoProductor.Name = "CboCodigoProductor"
        Me.CboCodigoProductor.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.CboCodigoProductor.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.CboCodigoProductor.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.CboCodigoProductor.Size = New System.Drawing.Size(208, 21)
        Me.CboCodigoProductor.TabIndex = 166
        Me.CboCodigoProductor.PropBag = resources.GetString("CboCodigoProductor.PropBag")
        '
        'ChkCausaIVA
        '
        Me.ChkCausaIVA.AutoSize = True
        Me.ChkCausaIVA.Checked = True
        Me.ChkCausaIVA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkCausaIVA.Location = New System.Drawing.Point(485, 273)
        Me.ChkCausaIVA.Name = "ChkCausaIVA"
        Me.ChkCausaIVA.Size = New System.Drawing.Size(76, 17)
        Me.ChkCausaIVA.TabIndex = 165
        Me.ChkCausaIVA.Text = "Causa IVA"
        Me.ChkCausaIVA.UseVisualStyleBackColor = True
        '
        'CboRuta
        '
        Me.CboRuta.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.CboRuta.Caption = ""
        Me.CboRuta.CaptionHeight = 17
        Me.CboRuta.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CboRuta.ColumnCaptionHeight = 17
        Me.CboRuta.ColumnFooterHeight = 17
        Me.CboRuta.ContentHeight = 15
        Me.CboRuta.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.CboRuta.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.CboRuta.DropDownWidth = 300
        Me.CboRuta.EditorBackColor = System.Drawing.SystemColors.Window
        Me.CboRuta.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboRuta.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.CboRuta.EditorHeight = 15
        Me.CboRuta.Images.Add(CType(resources.GetObject("CboRuta.Images"), System.Drawing.Image))
        Me.CboRuta.ItemHeight = 15
        Me.CboRuta.Location = New System.Drawing.Point(485, 186)
        Me.CboRuta.MatchEntryTimeout = CType(2000, Long)
        Me.CboRuta.MaxDropDownItems = CType(5, Short)
        Me.CboRuta.MaxLength = 32767
        Me.CboRuta.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.CboRuta.Name = "CboRuta"
        Me.CboRuta.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.CboRuta.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.CboRuta.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.CboRuta.Size = New System.Drawing.Size(135, 21)
        Me.CboRuta.TabIndex = 11
        Me.CboRuta.PropBag = resources.GetString("CboRuta.PropBag")
        '
        'CboCooperativa
        '
        Me.CboCooperativa.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.CboCooperativa.Caption = ""
        Me.CboCooperativa.CaptionHeight = 17
        Me.CboCooperativa.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CboCooperativa.ColumnCaptionHeight = 17
        Me.CboCooperativa.ColumnFooterHeight = 17
        Me.CboCooperativa.ContentHeight = 15
        Me.CboCooperativa.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.CboCooperativa.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.CboCooperativa.DropDownWidth = 300
        Me.CboCooperativa.EditorBackColor = System.Drawing.SystemColors.Window
        Me.CboCooperativa.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboCooperativa.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.CboCooperativa.EditorHeight = 15
        Me.CboCooperativa.Images.Add(CType(resources.GetObject("CboCooperativa.Images"), System.Drawing.Image))
        Me.CboCooperativa.ItemHeight = 15
        Me.CboCooperativa.Location = New System.Drawing.Point(485, 161)
        Me.CboCooperativa.MatchEntryTimeout = CType(2000, Long)
        Me.CboCooperativa.MaxDropDownItems = CType(5, Short)
        Me.CboCooperativa.MaxLength = 32767
        Me.CboCooperativa.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.CboCooperativa.Name = "CboCooperativa"
        Me.CboCooperativa.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.CboCooperativa.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.CboCooperativa.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.CboCooperativa.Size = New System.Drawing.Size(135, 21)
        Me.CboCooperativa.TabIndex = 10
        Me.CboCooperativa.PropBag = resources.GetString("CboCooperativa.PropBag")
        '
        'CboDepartamentos
        '
        Me.CboDepartamentos.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.CboDepartamentos.Caption = ""
        Me.CboDepartamentos.CaptionHeight = 17
        Me.CboDepartamentos.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CboDepartamentos.ColumnCaptionHeight = 17
        Me.CboDepartamentos.ColumnFooterHeight = 17
        Me.CboDepartamentos.ContentHeight = 15
        Me.CboDepartamentos.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.CboDepartamentos.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.CboDepartamentos.DropDownWidth = 300
        Me.CboDepartamentos.EditorBackColor = System.Drawing.SystemColors.Window
        Me.CboDepartamentos.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboDepartamentos.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.CboDepartamentos.EditorHeight = 15
        Me.CboDepartamentos.Images.Add(CType(resources.GetObject("CboDepartamentos.Images"), System.Drawing.Image))
        Me.CboDepartamentos.ItemHeight = 15
        Me.CboDepartamentos.Location = New System.Drawing.Point(485, 134)
        Me.CboDepartamentos.MatchEntryTimeout = CType(2000, Long)
        Me.CboDepartamentos.MaxDropDownItems = CType(5, Short)
        Me.CboDepartamentos.MaxLength = 32767
        Me.CboDepartamentos.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.CboDepartamentos.Name = "CboDepartamentos"
        Me.CboDepartamentos.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.CboDepartamentos.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.CboDepartamentos.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.CboDepartamentos.Size = New System.Drawing.Size(135, 21)
        Me.CboDepartamentos.TabIndex = 9
        Me.CboDepartamentos.PropBag = resources.GetString("CboDepartamentos.PropBag")
        '
        'CboEscolaridad
        '
        Me.CboEscolaridad.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.CboEscolaridad.Caption = ""
        Me.CboEscolaridad.CaptionHeight = 17
        Me.CboEscolaridad.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CboEscolaridad.ColumnCaptionHeight = 17
        Me.CboEscolaridad.ColumnFooterHeight = 17
        Me.CboEscolaridad.ContentHeight = 15
        Me.CboEscolaridad.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.CboEscolaridad.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.CboEscolaridad.DropDownWidth = 300
        Me.CboEscolaridad.EditorBackColor = System.Drawing.SystemColors.Window
        Me.CboEscolaridad.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboEscolaridad.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.CboEscolaridad.EditorHeight = 15
        Me.CboEscolaridad.Images.Add(CType(resources.GetObject("CboEscolaridad.Images"), System.Drawing.Image))
        Me.CboEscolaridad.ItemHeight = 15
        Me.CboEscolaridad.Location = New System.Drawing.Point(485, 106)
        Me.CboEscolaridad.MatchEntryTimeout = CType(2000, Long)
        Me.CboEscolaridad.MaxDropDownItems = CType(5, Short)
        Me.CboEscolaridad.MaxLength = 32767
        Me.CboEscolaridad.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.CboEscolaridad.Name = "CboEscolaridad"
        Me.CboEscolaridad.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.CboEscolaridad.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.CboEscolaridad.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.CboEscolaridad.Size = New System.Drawing.Size(135, 21)
        Me.CboEscolaridad.TabIndex = 8
        Me.CboEscolaridad.PropBag = resources.GetString("CboEscolaridad.PropBag")
        '
        'Button2
        '
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(591, 242)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(29, 30)
        Me.Button2.TabIndex = 164
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(591, 210)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(29, 30)
        Me.Button1.TabIndex = 163
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(393, 249)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(67, 13)
        Me.Label28.TabIndex = 162
        Me.Label28.Text = "Ctas x Pagar"
        '
        'TxtCtaxPagar
        '
        Me.TxtCtaxPagar.AcceptsReturn = True
        Me.TxtCtaxPagar.Location = New System.Drawing.Point(485, 246)
        Me.TxtCtaxPagar.Name = "TxtCtaxPagar"
        Me.TxtCtaxPagar.Size = New System.Drawing.Size(100, 20)
        Me.TxtCtaxPagar.TabIndex = 13
        '
        'Lbl
        '
        Me.Lbl.AutoSize = True
        Me.Lbl.Location = New System.Drawing.Point(394, 218)
        Me.Lbl.Name = "Lbl"
        Me.Lbl.Size = New System.Drawing.Size(70, 13)
        Me.Lbl.TabIndex = 160
        Me.Lbl.Text = "Ctas x Cobrar"
        '
        'TxtCtaxCobrar
        '
        Me.TxtCtaxCobrar.Location = New System.Drawing.Point(485, 215)
        Me.TxtCtaxCobrar.Name = "TxtCtaxCobrar"
        Me.TxtCtaxCobrar.Size = New System.Drawing.Size(100, 20)
        Me.TxtCtaxCobrar.TabIndex = 12
        '
        'CboSexo
        '
        Me.CboSexo.FormattingEnabled = True
        Me.CboSexo.Items.AddRange(New Object() {"Masculino", "Femenino"})
        Me.CboSexo.Location = New System.Drawing.Point(485, 56)
        Me.CboSexo.Name = "CboSexo"
        Me.CboSexo.Size = New System.Drawing.Size(135, 21)
        Me.CboSexo.TabIndex = 7
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(390, 62)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(31, 13)
        Me.Label27.TabIndex = 156
        Me.Label27.Text = "Sexo"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(390, 193)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(30, 13)
        Me.Label26.TabIndex = 154
        Me.Label26.Text = "Ruta"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(390, 166)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 13)
        Me.Label5.TabIndex = 152
        Me.Label5.Text = "Cooperativa"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(390, 139)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 13)
        Me.Label4.TabIndex = 150
        Me.Label4.Text = "Departamentos"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(390, 112)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 148
        Me.Label3.Text = "Escolaridad"
        '
        'DTFechaNacimientos
        '
        Me.DTFechaNacimientos.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTFechaNacimientos.Location = New System.Drawing.Point(485, 32)
        Me.DTFechaNacimientos.Name = "DTFechaNacimientos"
        Me.DTFechaNacimientos.Size = New System.Drawing.Size(135, 20)
        Me.DTFechaNacimientos.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(388, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 13)
        Me.Label2.TabIndex = 146
        Me.Label2.Text = "Fecha Nacimiento"
        '
        'DTFechaAdmision
        '
        Me.DTFechaAdmision.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTFechaAdmision.Location = New System.Drawing.Point(485, 6)
        Me.DTFechaAdmision.Name = "DTFechaAdmision"
        Me.DTFechaAdmision.Size = New System.Drawing.Size(135, 20)
        Me.DTFechaAdmision.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(388, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 144
        Me.Label1.Text = "Fecha Admision"
        '
        'ChkActivo
        '
        Me.ChkActivo.AutoSize = True
        Me.ChkActivo.Checked = True
        Me.ChkActivo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkActivo.Location = New System.Drawing.Point(399, 273)
        Me.ChkActivo.Name = "ChkActivo"
        Me.ChkActivo.Size = New System.Drawing.Size(56, 17)
        Me.ChkActivo.TabIndex = 143
        Me.ChkActivo.Text = "Activo"
        Me.ChkActivo.UseVisualStyleBackColor = True
        '
        'TxtApellidos
        '
        Me.TxtApellidos.Location = New System.Drawing.Point(127, 192)
        Me.TxtApellidos.Name = "TxtApellidos"
        Me.TxtApellidos.Size = New System.Drawing.Size(249, 20)
        Me.TxtApellidos.TabIndex = 3
        '
        'LblApellido
        '
        Me.LblApellido.AutoSize = True
        Me.LblApellido.Location = New System.Drawing.Point(21, 192)
        Me.LblApellido.Name = "LblApellido"
        Me.LblApellido.Size = New System.Drawing.Size(93, 13)
        Me.LblApellido.TabIndex = 141
        Me.LblApellido.Text = "Apellido Productor"
        '
        'TxtDireccion
        '
        Me.TxtDireccion.Location = New System.Drawing.Point(125, 215)
        Me.TxtDireccion.Multiline = True
        Me.TxtDireccion.Name = "TxtDireccion"
        Me.TxtDireccion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtDireccion.Size = New System.Drawing.Size(251, 59)
        Me.TxtDireccion.TabIndex = 4
        '
        'LblDireccion
        '
        Me.LblDireccion.AutoSize = True
        Me.LblDireccion.Location = New System.Drawing.Point(22, 215)
        Me.LblDireccion.Name = "LblDireccion"
        Me.LblDireccion.Size = New System.Drawing.Size(101, 13)
        Me.LblDireccion.TabIndex = 140
        Me.LblDireccion.Text = "Direccion Productor"
        '
        'TxtNombre
        '
        Me.TxtNombre.Location = New System.Drawing.Point(127, 168)
        Me.TxtNombre.Name = "TxtNombre"
        Me.TxtNombre.Size = New System.Drawing.Size(249, 20)
        Me.TxtNombre.TabIndex = 1
        '
        'LblNombre
        '
        Me.LblNombre.AutoSize = True
        Me.LblNombre.Location = New System.Drawing.Point(21, 168)
        Me.LblNombre.Name = "LblNombre"
        Me.LblNombre.Size = New System.Drawing.Size(93, 13)
        Me.LblNombre.TabIndex = 139
        Me.LblNombre.Text = "Nombre Productor"
        '
        'Button6
        '
        Me.Button6.Image = CType(resources.GetObject("Button6.Image"), System.Drawing.Image)
        Me.Button6.Location = New System.Drawing.Point(339, 126)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(37, 38)
        Me.Button6.TabIndex = 137
        Me.Button6.UseVisualStyleBackColor = True
        '
        'LblCodigo
        '
        Me.LblCodigo.AutoSize = True
        Me.LblCodigo.Location = New System.Drawing.Point(21, 139)
        Me.LblCodigo.Name = "LblCodigo"
        Me.LblCodigo.Size = New System.Drawing.Size(89, 13)
        Me.LblCodigo.TabIndex = 138
        Me.LblCodigo.Text = "Codigo Productor"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.CmdAgregar)
        Me.GroupBox6.Controls.Add(Me.CmdBorrarFoto)
        Me.GroupBox6.Location = New System.Drawing.Point(176, 17)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(148, 105)
        Me.GroupBox6.TabIndex = 3
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Foto"
        '
        'CmdAgregar
        '
        Me.CmdAgregar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmdAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.CmdAgregar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAgregar.Image = CType(resources.GetObject("CmdAgregar.Image"), System.Drawing.Image)
        Me.CmdAgregar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdAgregar.Location = New System.Drawing.Point(6, 19)
        Me.CmdAgregar.Name = "CmdAgregar"
        Me.CmdAgregar.Size = New System.Drawing.Size(65, 67)
        Me.CmdAgregar.TabIndex = 47
        Me.CmdAgregar.Text = "Agregar"
        Me.CmdAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdAgregar.UseVisualStyleBackColor = True
        '
        'CmdBorrarFoto
        '
        Me.CmdBorrarFoto.Image = CType(resources.GetObject("CmdBorrarFoto.Image"), System.Drawing.Image)
        Me.CmdBorrarFoto.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdBorrarFoto.Location = New System.Drawing.Point(77, 19)
        Me.CmdBorrarFoto.Name = "CmdBorrarFoto"
        Me.CmdBorrarFoto.Size = New System.Drawing.Size(65, 67)
        Me.CmdBorrarFoto.TabIndex = 22
        Me.CmdBorrarFoto.Text = "Borrar"
        Me.CmdBorrarFoto.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdBorrarFoto.UseVisualStyleBackColor = True
        '
        'ImgFoto
        '
        Me.ImgFoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ImgFoto.Image = CType(resources.GetObject("ImgFoto.Image"), System.Drawing.Image)
        Me.ImgFoto.Location = New System.Drawing.Point(16, 17)
        Me.ImgFoto.Name = "ImgFoto"
        Me.ImgFoto.Size = New System.Drawing.Size(154, 105)
        Me.ImgFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ImgFoto.TabIndex = 2
        Me.ImgFoto.TabStop = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(647, 297)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Informacion Adicional"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TxtEstadoCivil)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.TxtEdad)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        Me.GroupBox3.Controls.Add(Me.TxtComunidad)
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Location = New System.Drawing.Point(327, 44)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(315, 170)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        '
        'TxtEstadoCivil
        '
        Me.TxtEstadoCivil.Location = New System.Drawing.Point(112, 68)
        Me.TxtEstadoCivil.Name = "TxtEstadoCivil"
        Me.TxtEstadoCivil.Size = New System.Drawing.Size(143, 20)
        Me.TxtEstadoCivil.TabIndex = 21
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 68)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(62, 13)
        Me.Label14.TabIndex = 145
        Me.Label14.Text = "Estado Civil"
        '
        'TxtEdad
        '
        Me.TxtEdad.Location = New System.Drawing.Point(112, 42)
        Me.TxtEdad.Name = "TxtEdad"
        Me.TxtEdad.Size = New System.Drawing.Size(143, 20)
        Me.TxtEdad.TabIndex = 20
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 42)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(32, 13)
        Me.Label15.TabIndex = 143
        Me.Label15.Text = "Edad"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.TextBox11)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Location = New System.Drawing.Point(315, 0)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(315, 170)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "GroupBox4"
        '
        'TextBox11
        '
        Me.TextBox11.Location = New System.Drawing.Point(112, 16)
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.Size = New System.Drawing.Size(197, 20)
        Me.TextBox11.TabIndex = 140
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 16)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(93, 13)
        Me.Label16.TabIndex = 141
        Me.Label16.Text = "Nombre Productor"
        '
        'TxtComunidad
        '
        Me.TxtComunidad.Location = New System.Drawing.Point(112, 16)
        Me.TxtComunidad.Name = "TxtComunidad"
        Me.TxtComunidad.Size = New System.Drawing.Size(197, 20)
        Me.TxtComunidad.TabIndex = 19
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(6, 16)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(60, 13)
        Me.Label17.TabIndex = 141
        Me.Label17.Text = "Comunidad"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TxtCorreoElectronico)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.TxtFax)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.TxtTelefono)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.TxtNumeroHijos)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.TxtNombreConyugue)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 43)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(315, 170)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'TxtCorreoElectronico
        '
        Me.TxtCorreoElectronico.Location = New System.Drawing.Point(112, 120)
        Me.TxtCorreoElectronico.Name = "TxtCorreoElectronico"
        Me.TxtCorreoElectronico.Size = New System.Drawing.Size(143, 20)
        Me.TxtCorreoElectronico.TabIndex = 18
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 120)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(94, 13)
        Me.Label11.TabIndex = 149
        Me.Label11.Text = "Correo Electronico"
        '
        'TxtFax
        '
        Me.TxtFax.Location = New System.Drawing.Point(112, 94)
        Me.TxtFax.Name = "TxtFax"
        Me.TxtFax.Size = New System.Drawing.Size(143, 20)
        Me.TxtFax.TabIndex = 17
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 94)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(24, 13)
        Me.Label10.TabIndex = 147
        Me.Label10.Text = "Fax"
        '
        'TxtTelefono
        '
        Me.TxtTelefono.Location = New System.Drawing.Point(112, 68)
        Me.TxtTelefono.Name = "TxtTelefono"
        Me.TxtTelefono.Size = New System.Drawing.Size(143, 20)
        Me.TxtTelefono.TabIndex = 16
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 68)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(49, 13)
        Me.Label9.TabIndex = 145
        Me.Label9.Text = "Telefono"
        '
        'TxtNumeroHijos
        '
        Me.TxtNumeroHijos.Location = New System.Drawing.Point(112, 42)
        Me.TxtNumeroHijos.Name = "TxtNumeroHijos"
        Me.TxtNumeroHijos.Size = New System.Drawing.Size(143, 20)
        Me.TxtNumeroHijos.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 42)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(85, 13)
        Me.Label8.TabIndex = 143
        Me.Label8.Text = "Numero de Hijos"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TextBox2)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Location = New System.Drawing.Point(315, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(315, 170)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "GroupBox2"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(112, 16)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(197, 20)
        Me.TextBox2.TabIndex = 140
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(93, 13)
        Me.Label7.TabIndex = 141
        Me.Label7.Text = "Nombre Productor"
        '
        'TxtNombreConyugue
        '
        Me.TxtNombreConyugue.Location = New System.Drawing.Point(112, 16)
        Me.TxtNombreConyugue.Name = "TxtNombreConyugue"
        Me.TxtNombreConyugue.Size = New System.Drawing.Size(197, 20)
        Me.TxtNombreConyugue.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(95, 13)
        Me.Label6.TabIndex = 141
        Me.Label6.Text = "Nombre Conyugue" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.C1TrueDBGrid1)
        Me.TabPage3.Controls.Add(Me.GroupBox8)
        Me.TabPage3.Controls.Add(Me.GroupBox7)
        Me.TabPage3.Controls.Add(Me.GroupBox5)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(647, 297)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Tenencia de Tierra"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'C1TrueDBGrid1
        '
        Me.C1TrueDBGrid1.GroupByCaption = "Drag a column header here to group by that column"
        Me.C1TrueDBGrid1.Images.Add(CType(resources.GetObject("C1TrueDBGrid1.Images"), System.Drawing.Image))
        Me.C1TrueDBGrid1.Location = New System.Drawing.Point(16, 115)
        Me.C1TrueDBGrid1.Name = "C1TrueDBGrid1"
        Me.C1TrueDBGrid1.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.C1TrueDBGrid1.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.C1TrueDBGrid1.PreviewInfo.ZoomFactor = 75
        Me.C1TrueDBGrid1.PrintInfo.PageSettings = CType(resources.GetObject("C1TrueDBGrid1.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.C1TrueDBGrid1.Size = New System.Drawing.Size(610, 135)
        Me.C1TrueDBGrid1.TabIndex = 3
        Me.C1TrueDBGrid1.Text = "C1TrueDBGrid1"
        Me.C1TrueDBGrid1.PropBag = resources.GetString("C1TrueDBGrid1.PropBag")
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.OptTPrestada)
        Me.GroupBox8.Controls.Add(Me.OptTAlquilada)
        Me.GroupBox8.Controls.Add(Me.OptTReforma)
        Me.GroupBox8.Controls.Add(Me.OptTComprada)
        Me.GroupBox8.Controls.Add(Me.OptTHerencia)
        Me.GroupBox8.Controls.Add(Me.OptTPropia)
        Me.GroupBox8.Controls.Add(Me.Label18)
        Me.GroupBox8.Location = New System.Drawing.Point(16, 66)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(616, 43)
        Me.GroupBox8.TabIndex = 2
        Me.GroupBox8.TabStop = False
        '
        'OptTPrestada
        '
        Me.OptTPrestada.AutoSize = True
        Me.OptTPrestada.Location = New System.Drawing.Point(522, 14)
        Me.OptTPrestada.Name = "OptTPrestada"
        Me.OptTPrestada.Size = New System.Drawing.Size(67, 17)
        Me.OptTPrestada.TabIndex = 32
        Me.OptTPrestada.Text = "Prestada"
        Me.OptTPrestada.UseVisualStyleBackColor = True
        '
        'OptTAlquilada
        '
        Me.OptTAlquilada.AutoSize = True
        Me.OptTAlquilada.Location = New System.Drawing.Point(446, 14)
        Me.OptTAlquilada.Name = "OptTAlquilada"
        Me.OptTAlquilada.Size = New System.Drawing.Size(68, 17)
        Me.OptTAlquilada.TabIndex = 31
        Me.OptTAlquilada.Text = "Alquilada"
        Me.OptTAlquilada.UseVisualStyleBackColor = True
        '
        'OptTReforma
        '
        Me.OptTReforma.AutoSize = True
        Me.OptTReforma.Location = New System.Drawing.Point(335, 15)
        Me.OptTReforma.Name = "OptTReforma"
        Me.OptTReforma.Size = New System.Drawing.Size(104, 17)
        Me.OptTReforma.TabIndex = 30
        Me.OptTReforma.Text = "Reforma Agracia"
        Me.OptTReforma.UseVisualStyleBackColor = True
        '
        'OptTComprada
        '
        Me.OptTComprada.AutoSize = True
        Me.OptTComprada.Location = New System.Drawing.Point(249, 15)
        Me.OptTComprada.Name = "OptTComprada"
        Me.OptTComprada.Size = New System.Drawing.Size(73, 17)
        Me.OptTComprada.TabIndex = 29
        Me.OptTComprada.Text = "Comprada"
        Me.OptTComprada.UseVisualStyleBackColor = True
        '
        'OptTHerencia
        '
        Me.OptTHerencia.AutoSize = True
        Me.OptTHerencia.Location = New System.Drawing.Point(176, 14)
        Me.OptTHerencia.Name = "OptTHerencia"
        Me.OptTHerencia.Size = New System.Drawing.Size(68, 17)
        Me.OptTHerencia.TabIndex = 28
        Me.OptTHerencia.Text = "Herencia"
        Me.OptTHerencia.UseVisualStyleBackColor = True
        '
        'OptTPropia
        '
        Me.OptTPropia.AutoSize = True
        Me.OptTPropia.Checked = True
        Me.OptTPropia.Location = New System.Drawing.Point(115, 14)
        Me.OptTPropia.Name = "OptTPropia"
        Me.OptTPropia.Size = New System.Drawing.Size(55, 17)
        Me.OptTPropia.TabIndex = 27
        Me.OptTPropia.TabStop = True
        Me.OptTPropia.Text = "Propia"
        Me.OptTPropia.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 16)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(100, 13)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Tenencia de Tierra "
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.TxtVivienda)
        Me.GroupBox7.Controls.Add(Me.OptViviendaNo)
        Me.GroupBox7.Controls.Add(Me.OptViviendaSi)
        Me.GroupBox7.Controls.Add(Me.Label13)
        Me.GroupBox7.Location = New System.Drawing.Point(252, 17)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(380, 43)
        Me.GroupBox7.TabIndex = 1
        Me.GroupBox7.TabStop = False
        '
        'TxtVivienda
        '
        Me.TxtVivienda.Location = New System.Drawing.Point(210, 16)
        Me.TxtVivienda.Name = "TxtVivienda"
        Me.TxtVivienda.Size = New System.Drawing.Size(164, 20)
        Me.TxtVivienda.TabIndex = 26
        '
        'OptViviendaNo
        '
        Me.OptViviendaNo.AutoSize = True
        Me.OptViviendaNo.Location = New System.Drawing.Point(164, 14)
        Me.OptViviendaNo.Name = "OptViviendaNo"
        Me.OptViviendaNo.Size = New System.Drawing.Size(39, 17)
        Me.OptViviendaNo.TabIndex = 25
        Me.OptViviendaNo.Text = "No"
        Me.OptViviendaNo.UseVisualStyleBackColor = True
        '
        'OptViviendaSi
        '
        Me.OptViviendaSi.AutoSize = True
        Me.OptViviendaSi.Checked = True
        Me.OptViviendaSi.Location = New System.Drawing.Point(115, 14)
        Me.OptViviendaSi.Name = "OptViviendaSi"
        Me.OptViviendaSi.Size = New System.Drawing.Size(34, 17)
        Me.OptViviendaSi.TabIndex = 24
        Me.OptViviendaSi.TabStop = True
        Me.OptViviendaSi.Text = "Si"
        Me.OptViviendaSi.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 16)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(81, 13)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Vivienda Propia"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.OptFuenteNo)
        Me.GroupBox5.Controls.Add(Me.OptFuenteSi)
        Me.GroupBox5.Controls.Add(Me.Label12)
        Me.GroupBox5.Location = New System.Drawing.Point(16, 17)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(230, 43)
        Me.GroupBox5.TabIndex = 0
        Me.GroupBox5.TabStop = False
        '
        'OptFuenteNo
        '
        Me.OptFuenteNo.AutoSize = True
        Me.OptFuenteNo.Location = New System.Drawing.Point(164, 14)
        Me.OptFuenteNo.Name = "OptFuenteNo"
        Me.OptFuenteNo.Size = New System.Drawing.Size(39, 17)
        Me.OptFuenteNo.TabIndex = 23
        Me.OptFuenteNo.TabStop = True
        Me.OptFuenteNo.Text = "No"
        Me.OptFuenteNo.UseVisualStyleBackColor = True
        '
        'OptFuenteSi
        '
        Me.OptFuenteSi.AutoSize = True
        Me.OptFuenteSi.Checked = True
        Me.OptFuenteSi.Location = New System.Drawing.Point(115, 14)
        Me.OptFuenteSi.Name = "OptFuenteSi"
        Me.OptFuenteSi.Size = New System.Drawing.Size(34, 17)
        Me.OptFuenteSi.TabIndex = 22
        Me.OptFuenteSi.TabStop = True
        Me.OptFuenteSi.Text = "Si"
        Me.OptFuenteSi.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(88, 13)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Fuentes de Agua" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.C1TrueDBGrid2)
        Me.TabPage4.Controls.Add(Me.GroupBox9)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(647, 297)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Ganado"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'C1TrueDBGrid2
        '
        Me.C1TrueDBGrid2.GroupByCaption = "Drag a column header here to group by that column"
        Me.C1TrueDBGrid2.Images.Add(CType(resources.GetObject("C1TrueDBGrid2.Images"), System.Drawing.Image))
        Me.C1TrueDBGrid2.Location = New System.Drawing.Point(12, 139)
        Me.C1TrueDBGrid2.Name = "C1TrueDBGrid2"
        Me.C1TrueDBGrid2.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.C1TrueDBGrid2.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.C1TrueDBGrid2.PreviewInfo.ZoomFactor = 75
        Me.C1TrueDBGrid2.PrintInfo.PageSettings = CType(resources.GetObject("C1TrueDBGrid2.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.C1TrueDBGrid2.Size = New System.Drawing.Size(620, 117)
        Me.C1TrueDBGrid2.TabIndex = 2
        Me.C1TrueDBGrid2.Text = "C1TrueDBGrid2"
        Me.C1TrueDBGrid2.PropBag = resources.GetString("C1TrueDBGrid2.PropBag")
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.TxtCualEmpresa)
        Me.GroupBox9.Controls.Add(Me.Label25)
        Me.GroupBox9.Controls.Add(Me.GroupBox10)
        Me.GroupBox9.Controls.Add(Me.TxtCantidadTerreno)
        Me.GroupBox9.Controls.Add(Me.Label23)
        Me.GroupBox9.Controls.Add(Me.TxtCantidadBueyes)
        Me.GroupBox9.Controls.Add(Me.Label22)
        Me.GroupBox9.Controls.Add(Me.TxtCantidadVaquillas)
        Me.GroupBox9.Controls.Add(Me.Label21)
        Me.GroupBox9.Controls.Add(Me.TxtCantidadVacas)
        Me.GroupBox9.Controls.Add(Me.Label20)
        Me.GroupBox9.Controls.Add(Me.TxtAreaPasto)
        Me.GroupBox9.Controls.Add(Me.Label19)
        Me.GroupBox9.Location = New System.Drawing.Point(7, 3)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(625, 130)
        Me.GroupBox9.TabIndex = 1
        Me.GroupBox9.TabStop = False
        '
        'TxtCualEmpresa
        '
        Me.TxtCualEmpresa.Location = New System.Drawing.Point(335, 83)
        Me.TxtCualEmpresa.Name = "TxtCualEmpresa"
        Me.TxtCualEmpresa.Size = New System.Drawing.Size(282, 20)
        Me.TxtCualEmpresa.TabIndex = 40
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(235, 86)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(94, 13)
        Me.Label25.TabIndex = 11
        Me.Label25.Text = "Con Cual Empresa"
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.OptContratoNO)
        Me.GroupBox10.Controls.Add(Me.OptContratoSi)
        Me.GroupBox10.Controls.Add(Me.Label24)
        Me.GroupBox10.Location = New System.Drawing.Point(12, 72)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(200, 40)
        Me.GroupBox10.TabIndex = 10
        Me.GroupBox10.TabStop = False
        '
        'OptContratoNO
        '
        Me.OptContratoNO.AutoSize = True
        Me.OptContratoNO.Location = New System.Drawing.Point(143, 14)
        Me.OptContratoNO.Name = "OptContratoNO"
        Me.OptContratoNO.Size = New System.Drawing.Size(39, 17)
        Me.OptContratoNO.TabIndex = 39
        Me.OptContratoNO.Text = "No"
        Me.OptContratoNO.UseVisualStyleBackColor = True
        '
        'OptContratoSi
        '
        Me.OptContratoSi.AutoSize = True
        Me.OptContratoSi.Checked = True
        Me.OptContratoSi.Location = New System.Drawing.Point(94, 14)
        Me.OptContratoSi.Name = "OptContratoSi"
        Me.OptContratoSi.Size = New System.Drawing.Size(34, 17)
        Me.OptContratoSi.TabIndex = 38
        Me.OptContratoSi.TabStop = True
        Me.OptContratoSi.Text = "Si"
        Me.OptContratoSi.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(8, 16)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(77, 13)
        Me.Label24.TabIndex = 0
        Me.Label24.Text = "Tiene Contrato"
        '
        'TxtCantidadTerreno
        '
        Me.TxtCantidadTerreno.Location = New System.Drawing.Point(315, 46)
        Me.TxtCantidadTerreno.Name = "TxtCantidadTerreno"
        Me.TxtCantidadTerreno.Size = New System.Drawing.Size(78, 20)
        Me.TxtCantidadTerreno.TabIndex = 37
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(202, 49)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(109, 13)
        Me.Label23.TabIndex = 8
        Me.Label23.Text = "Cantidad de Terrenos"
        '
        'TxtCantidadBueyes
        '
        Me.TxtCantidadBueyes.Location = New System.Drawing.Point(115, 42)
        Me.TxtCantidadBueyes.Name = "TxtCantidadBueyes"
        Me.TxtCantidadBueyes.Size = New System.Drawing.Size(78, 20)
        Me.TxtCantidadBueyes.TabIndex = 36
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(9, 45)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(102, 13)
        Me.Label22.TabIndex = 6
        Me.Label22.Text = "Cantidad de Bueyes"
        '
        'TxtCantidadVaquillas
        '
        Me.TxtCantidadVaquillas.Location = New System.Drawing.Point(539, 19)
        Me.TxtCantidadVaquillas.Name = "TxtCantidadVaquillas"
        Me.TxtCantidadVaquillas.Size = New System.Drawing.Size(78, 20)
        Me.TxtCantidadVaquillas.TabIndex = 35
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(426, 19)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(109, 13)
        Me.Label21.TabIndex = 4
        Me.Label21.Text = "Cantidad de Vaquillas"
        '
        'TxtCantidadVacas
        '
        Me.TxtCantidadVacas.Location = New System.Drawing.Point(339, 16)
        Me.TxtCantidadVacas.Name = "TxtCantidadVacas"
        Me.TxtCantidadVacas.Size = New System.Drawing.Size(78, 20)
        Me.TxtCantidadVacas.TabIndex = 34
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(236, 16)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(97, 13)
        Me.Label20.TabIndex = 2
        Me.Label20.Text = "Cantidad de Vacas"
        '
        'TxtAreaPasto
        '
        Me.TxtAreaPasto.Location = New System.Drawing.Point(86, 16)
        Me.TxtAreaPasto.Name = "TxtAreaPasto"
        Me.TxtAreaPasto.Size = New System.Drawing.Size(144, 20)
        Me.TxtAreaPasto.TabIndex = 33
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(6, 16)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(74, 13)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "Area de Pasto"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'FrmProductor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(675, 463)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.CmdGrabar)
        Me.Controls.Add(Me.ButtonBorrar)
        Me.Controls.Add(Me.CmdNuevo)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.LblTitulo)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmProductor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Registro de Productor"
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.CboCodigoProductor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboRuta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboCooperativa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboDepartamentos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboEscolaridad, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        CType(Me.ImgFoto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.C1TrueDBGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        CType(Me.C1TrueDBGrid2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblTitulo As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents CmdGrabar As System.Windows.Forms.Button
    Friend WithEvents ButtonBorrar As System.Windows.Forms.Button
    Friend WithEvents CmdNuevo As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents CmdAgregar As System.Windows.Forms.Button
    Friend WithEvents CmdBorrarFoto As System.Windows.Forms.Button
    Friend WithEvents ImgFoto As System.Windows.Forms.PictureBox
    Friend WithEvents TxtApellidos As System.Windows.Forms.TextBox
    Friend WithEvents LblApellido As System.Windows.Forms.Label
    Friend WithEvents TxtDireccion As System.Windows.Forms.TextBox
    Friend WithEvents LblDireccion As System.Windows.Forms.Label
    Friend WithEvents TxtNombre As System.Windows.Forms.TextBox
    Friend WithEvents LblNombre As System.Windows.Forms.Label
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents LblCodigo As System.Windows.Forms.Label
    Friend WithEvents ChkActivo As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DTFechaAdmision As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTFechaNacimientos As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TxtNombreConyugue As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtNumeroHijos As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TxtCorreoElectronico As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TxtFax As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TxtTelefono As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtEstadoCivil As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TxtEdad As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox11 As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TxtComunidad As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents OptFuenteNo As System.Windows.Forms.RadioButton
    Friend WithEvents OptFuenteSi As System.Windows.Forms.RadioButton
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtVivienda As System.Windows.Forms.TextBox
    Friend WithEvents OptViviendaNo As System.Windows.Forms.RadioButton
    Friend WithEvents OptViviendaSi As System.Windows.Forms.RadioButton
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents OptTPrestada As System.Windows.Forms.RadioButton
    Friend WithEvents OptTAlquilada As System.Windows.Forms.RadioButton
    Friend WithEvents OptTReforma As System.Windows.Forms.RadioButton
    Friend WithEvents OptTComprada As System.Windows.Forms.RadioButton
    Friend WithEvents OptTHerencia As System.Windows.Forms.RadioButton
    Friend WithEvents OptTPropia As System.Windows.Forms.RadioButton
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents C1TrueDBGrid1 As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtCantidadVacas As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents TxtAreaPasto As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents TxtCantidadVaquillas As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents TxtCantidadBueyes As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents TxtCantidadTerreno As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents OptContratoNO As System.Windows.Forms.RadioButton
    Friend WithEvents OptContratoSi As System.Windows.Forms.RadioButton
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents TxtCualEmpresa As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents C1TrueDBGrid2 As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents CboSexo As System.Windows.Forms.ComboBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Lbl As System.Windows.Forms.Label
    Friend WithEvents TxtCtaxCobrar As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents TxtCtaxPagar As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents CboEscolaridad As C1.Win.C1List.C1Combo
    Friend WithEvents CboDepartamentos As C1.Win.C1List.C1Combo
    Friend WithEvents CboCooperativa As C1.Win.C1List.C1Combo
    Friend WithEvents CboRuta As C1.Win.C1List.C1Combo
    Friend WithEvents ChkCausaIVA As System.Windows.Forms.CheckBox
    Friend WithEvents CboCodigoProductor As C1.Win.C1List.C1Combo
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents TxtNumeroCedula As System.Windows.Forms.MaskedTextBox
End Class
