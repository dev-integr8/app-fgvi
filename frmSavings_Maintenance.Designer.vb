<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSavings_Maintenance
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dgvSavings = New System.Windows.Forms.DataGridView()
        Me.colSavingsCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.txtAPR = New System.Windows.Forms.TextBox()
        Me.cmbMethod = New System.Windows.Forms.ComboBox()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.nudNoDaysMonth = New System.Windows.Forms.NumericUpDown()
        Me.nudNoDaysYear = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtSavAccntCode = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSavAccntTitle = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtExpAccntCode = New System.Windows.Forms.TextBox()
        Me.txtExpAccntTitle = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtMinimum = New System.Windows.Forms.TextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.pnlSavings = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtPrefix = New System.Windows.Forms.TextBox()
        Me.nudSav2 = New System.Windows.Forms.NumericUpDown()
        Me.nudSav = New System.Windows.Forms.NumericUpDown()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtSavAccntTitle2 = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtSavAccntCode2 = New System.Windows.Forms.TextBox()
        CType(Me.dgvSavings, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudNoDaysMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudNoDaysYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlSavings.SuspendLayout()
        CType(Me.nudSav2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudSav, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvSavings
        '
        Me.dgvSavings.AllowUserToAddRows = False
        Me.dgvSavings.AllowUserToDeleteRows = False
        Me.dgvSavings.AllowUserToResizeColumns = False
        Me.dgvSavings.AllowUserToResizeRows = False
        Me.dgvSavings.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgvSavings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSavings.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSavingsCode, Me.colDescription})
        Me.dgvSavings.Location = New System.Drawing.Point(3, 39)
        Me.dgvSavings.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgvSavings.Name = "dgvSavings"
        Me.dgvSavings.ReadOnly = True
        Me.dgvSavings.RowHeadersVisible = False
        Me.dgvSavings.Size = New System.Drawing.Size(262, 351)
        Me.dgvSavings.TabIndex = 0
        '
        'colSavingsCode
        '
        Me.colSavingsCode.HeaderText = "Code"
        Me.colSavingsCode.Name = "colSavingsCode"
        Me.colSavingsCode.ReadOnly = True
        Me.colSavingsCode.Visible = False
        '
        'colDescription
        '
        Me.colDescription.HeaderText = "Description"
        Me.colDescription.Name = "colDescription"
        Me.colDescription.ReadOnly = True
        Me.colDescription.Width = 256
        '
        'txtDesc
        '
        Me.txtDesc.BackColor = System.Drawing.Color.White
        Me.txtDesc.Location = New System.Drawing.Point(151, 27)
        Me.txtDesc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(259, 21)
        Me.txtDesc.TabIndex = 1199
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 15)
        Me.Label4.TabIndex = 1198
        Me.Label4.Text = "Description :"
        '
        'cmbType
        '
        Me.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.Items.AddRange(New Object() {"Simple Interest", "Compounding Interest"})
        Me.cmbType.Location = New System.Drawing.Point(151, 50)
        Me.cmbType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(259, 23)
        Me.cmbType.TabIndex = 1200
        '
        'txtAPR
        '
        Me.txtAPR.BackColor = System.Drawing.Color.White
        Me.txtAPR.Location = New System.Drawing.Point(151, 74)
        Me.txtAPR.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtAPR.Name = "txtAPR"
        Me.txtAPR.Size = New System.Drawing.Size(91, 21)
        Me.txtAPR.TabIndex = 1199
        '
        'cmbMethod
        '
        Me.cmbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMethod.FormattingEnabled = True
        Me.cmbMethod.Items.AddRange(New Object() {"Total Monthly Balance"})
        Me.cmbMethod.Location = New System.Drawing.Point(151, 97)
        Me.cmbMethod.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbMethod.Name = "cmbMethod"
        Me.cmbMethod.Size = New System.Drawing.Size(259, 23)
        Me.cmbMethod.TabIndex = 1200
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Monthly", "Quarterly", "Yearly"})
        Me.cmbPeriod.Location = New System.Drawing.Point(151, 121)
        Me.cmbPeriod.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(259, 23)
        Me.cmbPeriod.TabIndex = 1200
        '
        'nudNoDaysMonth
        '
        Me.nudNoDaysMonth.Location = New System.Drawing.Point(151, 145)
        Me.nudNoDaysMonth.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.nudNoDaysMonth.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.nudNoDaysMonth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudNoDaysMonth.Name = "nudNoDaysMonth"
        Me.nudNoDaysMonth.Size = New System.Drawing.Size(92, 21)
        Me.nudNoDaysMonth.TabIndex = 1201
        Me.nudNoDaysMonth.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'nudNoDaysYear
        '
        Me.nudNoDaysYear.Location = New System.Drawing.Point(151, 168)
        Me.nudNoDaysYear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.nudNoDaysYear.Maximum = New Decimal(New Integer() {365, 0, 0, 0})
        Me.nudNoDaysYear.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudNoDaysYear.Name = "nudNoDaysYear"
        Me.nudNoDaysYear.Size = New System.Drawing.Size(92, 21)
        Me.nudNoDaysYear.TabIndex = 1201
        Me.nudNoDaysYear.Value = New Decimal(New Integer() {365, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 15)
        Me.Label1.TabIndex = 1198
        Me.Label1.Text = "Type :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 15)
        Me.Label2.TabIndex = 1198
        Me.Label2.Text = "APR :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 15)
        Me.Label3.TabIndex = 1198
        Me.Label3.Text = "Method :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(4, 125)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 15)
        Me.Label5.TabIndex = 1198
        Me.Label5.Text = "Period :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(4, 147)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(126, 15)
        Me.Label6.TabIndex = 1198
        Me.Label6.Text = "No. of Days in Month :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(4, 170)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(116, 15)
        Me.Label7.TabIndex = 1198
        Me.Label7.Text = "No. of Days in Year :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(4, 217)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 15)
        Me.Label8.TabIndex = 1198
        Me.Label8.Text = "Account Code :"
        '
        'txtSavAccntCode
        '
        Me.txtSavAccntCode.BackColor = System.Drawing.Color.White
        Me.txtSavAccntCode.Location = New System.Drawing.Point(151, 213)
        Me.txtSavAccntCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSavAccntCode.Name = "txtSavAccntCode"
        Me.txtSavAccntCode.ReadOnly = True
        Me.txtSavAccntCode.Size = New System.Drawing.Size(91, 21)
        Me.txtSavAccntCode.TabIndex = 1202
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(4, 239)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(128, 15)
        Me.Label9.TabIndex = 1198
        Me.Label9.Text = "Savings Account Title :"
        '
        'txtSavAccntTitle
        '
        Me.txtSavAccntTitle.BackColor = System.Drawing.Color.White
        Me.txtSavAccntTitle.Location = New System.Drawing.Point(151, 235)
        Me.txtSavAccntTitle.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSavAccntTitle.Name = "txtSavAccntTitle"
        Me.txtSavAccntTitle.Size = New System.Drawing.Size(259, 21)
        Me.txtSavAccntTitle.TabIndex = 1203
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(4, 305)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(88, 15)
        Me.Label10.TabIndex = 1198
        Me.Label10.Text = "Account Code :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(4, 327)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(133, 15)
        Me.Label11.TabIndex = 1198
        Me.Label11.Text = "Expense Account Title :"
        '
        'txtExpAccntCode
        '
        Me.txtExpAccntCode.BackColor = System.Drawing.Color.White
        Me.txtExpAccntCode.Location = New System.Drawing.Point(151, 301)
        Me.txtExpAccntCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtExpAccntCode.Name = "txtExpAccntCode"
        Me.txtExpAccntCode.ReadOnly = True
        Me.txtExpAccntCode.Size = New System.Drawing.Size(91, 21)
        Me.txtExpAccntCode.TabIndex = 1204
        '
        'txtExpAccntTitle
        '
        Me.txtExpAccntTitle.BackColor = System.Drawing.Color.White
        Me.txtExpAccntTitle.Location = New System.Drawing.Point(151, 324)
        Me.txtExpAccntTitle.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtExpAccntTitle.Name = "txtExpAccntTitle"
        Me.txtExpAccntTitle.Size = New System.Drawing.Size(259, 21)
        Me.txtExpAccntTitle.TabIndex = 1205
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(4, 194)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(66, 15)
        Me.Label12.TabIndex = 1198
        Me.Label12.Text = "Minimum :"
        '
        'txtMinimum
        '
        Me.txtMinimum.BackColor = System.Drawing.Color.White
        Me.txtMinimum.Location = New System.Drawing.Point(151, 191)
        Me.txtMinimum.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtMinimum.Name = "txtMinimum"
        Me.txtMinimum.Size = New System.Drawing.Size(91, 21)
        Me.txtMinimum.TabIndex = 1202
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.DimGray
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbNew, Me.tsbEdit, Me.tsbSave, Me.tsbDelete, Me.ToolStripSeparator1, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(688, 38)
        Me.ToolStrip1.TabIndex = 1406
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbNew
        '
        Me.tsbNew.AutoSize = False
        Me.tsbNew.ForeColor = System.Drawing.Color.White
        Me.tsbNew.Image = Global.jade.My.Resources.Resources.circle_document_documents_extension_file_page_sheet_icon_7
        Me.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbNew.Name = "tsbNew"
        Me.tsbNew.Size = New System.Drawing.Size(50, 35)
        Me.tsbNew.Text = "New"
        Me.tsbNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbEdit
        '
        Me.tsbEdit.AutoSize = False
        Me.tsbEdit.Enabled = False
        Me.tsbEdit.ForeColor = System.Drawing.Color.White
        Me.tsbEdit.Image = Global.jade.My.Resources.Resources.edit_pen_write_notes_document_3c679c93cb5d1fed_512x512
        Me.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbEdit.Name = "tsbEdit"
        Me.tsbEdit.Size = New System.Drawing.Size(50, 35)
        Me.tsbEdit.Text = "Edit"
        Me.tsbEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbSave
        '
        Me.tsbSave.AutoSize = False
        Me.tsbSave.Enabled = False
        Me.tsbSave.ForeColor = System.Drawing.Color.White
        Me.tsbSave.Image = Global.jade.My.Resources.Resources.Save_Icon
        Me.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSave.Name = "tsbSave"
        Me.tsbSave.Size = New System.Drawing.Size(50, 35)
        Me.tsbSave.Text = "Save"
        Me.tsbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbDelete
        '
        Me.tsbDelete.AutoSize = False
        Me.tsbDelete.Enabled = False
        Me.tsbDelete.ForeColor = System.Drawing.Color.White
        Me.tsbDelete.Image = Global.jade.My.Resources.Resources.close_icon
        Me.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbDelete.Name = "tsbDelete"
        Me.tsbDelete.Size = New System.Drawing.Size(50, 35)
        Me.tsbDelete.Text = "Delete"
        Me.tsbDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 38)
        '
        'tsbClose
        '
        Me.tsbClose.AutoSize = False
        Me.tsbClose.Enabled = False
        Me.tsbClose.ForeColor = System.Drawing.Color.White
        Me.tsbClose.Image = Global.jade.My.Resources.Resources.close_button_icon_transparent_background_247604
        Me.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbClose.Name = "tsbClose"
        Me.tsbClose.Size = New System.Drawing.Size(50, 35)
        Me.tsbClose.Text = "Close"
        Me.tsbClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbExit
        '
        Me.tsbExit.AutoSize = False
        Me.tsbExit.ForeColor = System.Drawing.Color.White
        Me.tsbExit.Image = Global.jade.My.Resources.Resources.exit_button_icon_18
        Me.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExit.Name = "tsbExit"
        Me.tsbExit.Size = New System.Drawing.Size(50, 35)
        Me.tsbExit.Text = "Exit"
        Me.tsbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlSavings
        '
        Me.pnlSavings.Controls.Add(Me.Label13)
        Me.pnlSavings.Controls.Add(Me.txtPrefix)
        Me.pnlSavings.Controls.Add(Me.Label4)
        Me.pnlSavings.Controls.Add(Me.Label1)
        Me.pnlSavings.Controls.Add(Me.nudSav2)
        Me.pnlSavings.Controls.Add(Me.nudSav)
        Me.pnlSavings.Controls.Add(Me.nudNoDaysYear)
        Me.pnlSavings.Controls.Add(Me.Label2)
        Me.pnlSavings.Controls.Add(Me.nudNoDaysMonth)
        Me.pnlSavings.Controls.Add(Me.Label3)
        Me.pnlSavings.Controls.Add(Me.cmbPeriod)
        Me.pnlSavings.Controls.Add(Me.Label17)
        Me.pnlSavings.Controls.Add(Me.Label8)
        Me.pnlSavings.Controls.Add(Me.cmbMethod)
        Me.pnlSavings.Controls.Add(Me.Label5)
        Me.pnlSavings.Controls.Add(Me.cmbType)
        Me.pnlSavings.Controls.Add(Me.Label16)
        Me.pnlSavings.Controls.Add(Me.Label14)
        Me.pnlSavings.Controls.Add(Me.Label12)
        Me.pnlSavings.Controls.Add(Me.txtExpAccntTitle)
        Me.pnlSavings.Controls.Add(Me.Label10)
        Me.pnlSavings.Controls.Add(Me.txtSavAccntTitle2)
        Me.pnlSavings.Controls.Add(Me.txtSavAccntTitle)
        Me.pnlSavings.Controls.Add(Me.Label15)
        Me.pnlSavings.Controls.Add(Me.Label9)
        Me.pnlSavings.Controls.Add(Me.txtExpAccntCode)
        Me.pnlSavings.Controls.Add(Me.Label6)
        Me.pnlSavings.Controls.Add(Me.txtMinimum)
        Me.pnlSavings.Controls.Add(Me.Label11)
        Me.pnlSavings.Controls.Add(Me.txtSavAccntCode2)
        Me.pnlSavings.Controls.Add(Me.txtSavAccntCode)
        Me.pnlSavings.Controls.Add(Me.Label7)
        Me.pnlSavings.Controls.Add(Me.txtAPR)
        Me.pnlSavings.Controls.Add(Me.txtDesc)
        Me.pnlSavings.Enabled = False
        Me.pnlSavings.Location = New System.Drawing.Point(271, 39)
        Me.pnlSavings.Name = "pnlSavings"
        Me.pnlSavings.Size = New System.Drawing.Size(413, 350)
        Me.pnlSavings.TabIndex = 1407
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(4, 7)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(44, 15)
        Me.Label13.TabIndex = 1206
        Me.Label13.Text = "Prefix :"
        '
        'txtPrefix
        '
        Me.txtPrefix.BackColor = System.Drawing.Color.White
        Me.txtPrefix.Location = New System.Drawing.Point(151, 4)
        Me.txtPrefix.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtPrefix.Name = "txtPrefix"
        Me.txtPrefix.Size = New System.Drawing.Size(259, 21)
        Me.txtPrefix.TabIndex = 1207
        '
        'nudSav2
        '
        Me.nudSav2.Location = New System.Drawing.Point(313, 257)
        Me.nudSav2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.nudSav2.Name = "nudSav2"
        Me.nudSav2.Size = New System.Drawing.Size(92, 21)
        Me.nudSav2.TabIndex = 1201
        '
        'nudSav
        '
        Me.nudSav.Location = New System.Drawing.Point(313, 213)
        Me.nudSav.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.nudSav.Name = "nudSav"
        Me.nudSav.Size = New System.Drawing.Size(92, 21)
        Me.nudSav.TabIndex = 1201
        Me.nudSav.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(4, 261)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(88, 15)
        Me.Label17.TabIndex = 1198
        Me.Label17.Text = "Account Code :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(248, 261)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(55, 15)
        Me.Label16.TabIndex = 1198
        Me.Label16.Text = "Percent :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(248, 217)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(55, 15)
        Me.Label14.TabIndex = 1198
        Me.Label14.Text = "Percent :"
        '
        'txtSavAccntTitle2
        '
        Me.txtSavAccntTitle2.BackColor = System.Drawing.Color.White
        Me.txtSavAccntTitle2.Location = New System.Drawing.Point(151, 279)
        Me.txtSavAccntTitle2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSavAccntTitle2.Name = "txtSavAccntTitle2"
        Me.txtSavAccntTitle2.Size = New System.Drawing.Size(259, 21)
        Me.txtSavAccntTitle2.TabIndex = 1203
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(4, 283)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(128, 15)
        Me.Label15.TabIndex = 1198
        Me.Label15.Text = "Savings Account Title :"
        '
        'txtSavAccntCode2
        '
        Me.txtSavAccntCode2.BackColor = System.Drawing.Color.White
        Me.txtSavAccntCode2.Location = New System.Drawing.Point(151, 257)
        Me.txtSavAccntCode2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSavAccntCode2.Name = "txtSavAccntCode2"
        Me.txtSavAccntCode2.ReadOnly = True
        Me.txtSavAccntCode2.Size = New System.Drawing.Size(91, 21)
        Me.txtSavAccntCode2.TabIndex = 1202
        '
        'frmSavings_Maintenance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(688, 393)
        Me.Controls.Add(Me.pnlSavings)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.dgvSavings)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmSavings_Maintenance"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Savings Maintenance"
        CType(Me.dgvSavings, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudNoDaysMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudNoDaysYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlSavings.ResumeLayout(False)
        Me.pnlSavings.PerformLayout()
        CType(Me.nudSav2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudSav, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvSavings As System.Windows.Forms.DataGridView
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents txtAPR As System.Windows.Forms.TextBox
    Friend WithEvents cmbMethod As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents nudNoDaysMonth As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudNoDaysYear As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtSavAccntCode As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtSavAccntTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtExpAccntCode As System.Windows.Forms.TextBox
    Friend WithEvents txtExpAccntTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtMinimum As System.Windows.Forms.TextBox
    Friend WithEvents colSavingsCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlSavings As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtPrefix As System.Windows.Forms.TextBox
    Friend WithEvents nudSav2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudSav As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtSavAccntTitle2 As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtSavAccntCode2 As System.Windows.Forms.TextBox
End Class
