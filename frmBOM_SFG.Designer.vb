<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBOM_SFG
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBOM_SFG))
        Me.cbUOM = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.txtQTY = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgvItemMaster = New System.Windows.Forms.DataGridView()
        Me.chIDX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcItemCategory = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcItemGroup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chBOMItemCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chBOMItemName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chBOMUOM = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.chBOMQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chTotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtProfitMargin = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtCOS = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCOGS = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSetAside = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtRevenue = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtTotalCost = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtFOcost = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtDLcost = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtDMcost = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.dgvLabor = New System.Windows.Forms.DataGridView()
        Me.dgcDLactivity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDLrate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDLcrewNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDLtime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDLTotalMins = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDLtotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.dgvOverhead = New System.Windows.Forms.DataGridView()
        Me.dgcFOactivity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcFOmachine = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcFOrate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcFOKW = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcFOhrs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcFOtotalKWH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcFOcost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbSearch = New System.Windows.Forms.ToolStripButton()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbUpload = New System.Windows.Forms.ToolStripButton()
        Me.tsbReports = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrevious = New System.Windows.Forms.ToolStripButton()
        Me.tsbNext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        CType(Me.dgvItemMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvLabor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.dgvOverhead, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cbUOM
        '
        Me.cbUOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbUOM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.cbUOM.FormattingEnabled = True
        Me.cbUOM.Location = New System.Drawing.Point(105, 91)
        Me.cbUOM.Name = "cbUOM"
        Me.cbUOM.Size = New System.Drawing.Size(93, 24)
        Me.cbUOM.TabIndex = 8134
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(24, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 16)
        Me.Label4.TabIndex = 8132
        Me.Label4.Text = "Item Code :"
        '
        'txtItemCode
        '
        Me.txtItemCode.BackColor = System.Drawing.Color.White
        Me.txtItemCode.Enabled = False
        Me.txtItemCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtItemCode.ForeColor = System.Drawing.Color.Black
        Me.txtItemCode.Location = New System.Drawing.Point(105, 43)
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.Size = New System.Drawing.Size(238, 22)
        Me.txtItemCode.TabIndex = 8127
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(20, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 16)
        Me.Label1.TabIndex = 8130
        Me.Label1.Text = "Item Name :"
        '
        'txtItemName
        '
        Me.txtItemName.BackColor = System.Drawing.Color.White
        Me.txtItemName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtItemName.ForeColor = System.Drawing.Color.Black
        Me.txtItemName.Location = New System.Drawing.Point(105, 67)
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.Size = New System.Drawing.Size(238, 22)
        Me.txtItemName.TabIndex = 8128
        '
        'txtQTY
        '
        Me.txtQTY.BackColor = System.Drawing.Color.White
        Me.txtQTY.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtQTY.ForeColor = System.Drawing.Color.Black
        Me.txtQTY.Location = New System.Drawing.Point(252, 91)
        Me.txtQTY.Name = "txtQTY"
        Me.txtQTY.Size = New System.Drawing.Size(91, 22)
        Me.txtQTY.TabIndex = 8129
        Me.txtQTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(54, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 16)
        Me.Label2.TabIndex = 8135
        Me.Label2.Text = "UOM :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(204, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 16)
        Me.Label3.TabIndex = 8136
        Me.Label3.Text = "QTY :"
        '
        'dgvItemMaster
        '
        Me.dgvItemMaster.BackgroundColor = System.Drawing.Color.White
        Me.dgvItemMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvItemMaster.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chIDX, Me.dgcItemCategory, Me.dgcItemGroup, Me.chBOMItemCode, Me.chBOMItemName, Me.chBOMUOM, Me.chBOMQTY, Me.chCost, Me.chTotalCost})
        Me.dgvItemMaster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvItemMaster.Location = New System.Drawing.Point(3, 3)
        Me.dgvItemMaster.Name = "dgvItemMaster"
        Me.dgvItemMaster.Size = New System.Drawing.Size(1271, 378)
        Me.dgvItemMaster.TabIndex = 8138
        '
        'chIDX
        '
        Me.chIDX.HeaderText = "idx"
        Me.chIDX.Name = "chIDX"
        Me.chIDX.Visible = False
        '
        'dgcItemCategory
        '
        Me.dgcItemCategory.HeaderText = "Category"
        Me.dgcItemCategory.Name = "dgcItemCategory"
        Me.dgcItemCategory.Visible = False
        '
        'dgcItemGroup
        '
        Me.dgcItemGroup.HeaderText = "Group"
        Me.dgcItemGroup.Name = "dgcItemGroup"
        Me.dgcItemGroup.Visible = False
        Me.dgcItemGroup.Width = 150
        '
        'chBOMItemCode
        '
        Me.chBOMItemCode.HeaderText = "Item Code"
        Me.chBOMItemCode.Name = "chBOMItemCode"
        '
        'chBOMItemName
        '
        Me.chBOMItemName.HeaderText = "Item Description"
        Me.chBOMItemName.Name = "chBOMItemName"
        Me.chBOMItemName.Width = 300
        '
        'chBOMUOM
        '
        Me.chBOMUOM.HeaderText = "UOM"
        Me.chBOMUOM.Name = "chBOMUOM"
        Me.chBOMUOM.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chBOMUOM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'chBOMQTY
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.chBOMQTY.DefaultCellStyle = DataGridViewCellStyle1
        Me.chBOMQTY.HeaderText = "Quantity"
        Me.chBOMQTY.Name = "chBOMQTY"
        '
        'chCost
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.chCost.DefaultCellStyle = DataGridViewCellStyle2
        Me.chCost.HeaderText = "Standard Cost"
        Me.chCost.Name = "chCost"
        Me.chCost.ReadOnly = True
        '
        'chTotalCost
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.chTotalCost.DefaultCellStyle = DataGridViewCellStyle3
        Me.chTotalCost.HeaderText = "Total Cost"
        Me.chTotalCost.Name = "chTotalCost"
        Me.chTotalCost.ReadOnly = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtProfitMargin)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtCOS)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtCOGS)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtSetAside)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtRevenue)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtTotalCost)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtFOcost)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtDLcost)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.txtDMcost)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtDescription)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtCode)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtItemCode)
        Me.GroupBox1.Controls.Add(Me.txtQTY)
        Me.GroupBox1.Controls.Add(Me.txtItemName)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cbUOM)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 43)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1281, 155)
        Me.GroupBox1.TabIndex = 8139
        Me.GroupBox1.TabStop = False
        '
        'txtProfitMargin
        '
        Me.txtProfitMargin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtProfitMargin.BackColor = System.Drawing.Color.White
        Me.txtProfitMargin.Enabled = False
        Me.txtProfitMargin.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtProfitMargin.ForeColor = System.Drawing.Color.Black
        Me.txtProfitMargin.Location = New System.Drawing.Point(810, 112)
        Me.txtProfitMargin.Name = "txtProfitMargin"
        Me.txtProfitMargin.ReadOnly = True
        Me.txtProfitMargin.Size = New System.Drawing.Size(166, 22)
        Me.txtProfitMargin.TabIndex = 8173
        Me.txtProfitMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(716, 115)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(88, 16)
        Me.Label12.TabIndex = 8174
        Me.Label12.Text = "Profit Margin :"
        '
        'txtCOS
        '
        Me.txtCOS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCOS.BackColor = System.Drawing.Color.White
        Me.txtCOS.Enabled = False
        Me.txtCOS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtCOS.ForeColor = System.Drawing.Color.Black
        Me.txtCOS.Location = New System.Drawing.Point(810, 88)
        Me.txtCOS.Name = "txtCOS"
        Me.txtCOS.ReadOnly = True
        Me.txtCOS.Size = New System.Drawing.Size(166, 22)
        Me.txtCOS.TabIndex = 8171
        Me.txtCOS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(762, 91)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 16)
        Me.Label6.TabIndex = 8172
        Me.Label6.Text = "COS :"
        '
        'txtCOGS
        '
        Me.txtCOGS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCOGS.BackColor = System.Drawing.Color.White
        Me.txtCOGS.Enabled = False
        Me.txtCOGS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtCOGS.ForeColor = System.Drawing.Color.Black
        Me.txtCOGS.Location = New System.Drawing.Point(810, 65)
        Me.txtCOGS.Name = "txtCOGS"
        Me.txtCOGS.ReadOnly = True
        Me.txtCOGS.Size = New System.Drawing.Size(166, 22)
        Me.txtCOGS.TabIndex = 8169
        Me.txtCOGS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(752, 68)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(52, 16)
        Me.Label9.TabIndex = 8170
        Me.Label9.Text = "COGS :"
        '
        'txtSetAside
        '
        Me.txtSetAside.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSetAside.BackColor = System.Drawing.Color.White
        Me.txtSetAside.Enabled = False
        Me.txtSetAside.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtSetAside.ForeColor = System.Drawing.Color.Black
        Me.txtSetAside.Location = New System.Drawing.Point(810, 42)
        Me.txtSetAside.Name = "txtSetAside"
        Me.txtSetAside.ReadOnly = True
        Me.txtSetAside.Size = New System.Drawing.Size(166, 22)
        Me.txtSetAside.TabIndex = 8167
        Me.txtSetAside.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(732, 45)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 16)
        Me.Label10.TabIndex = 8168
        Me.Label10.Text = "Set Aside :"
        '
        'txtRevenue
        '
        Me.txtRevenue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRevenue.BackColor = System.Drawing.Color.White
        Me.txtRevenue.Enabled = False
        Me.txtRevenue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtRevenue.ForeColor = System.Drawing.Color.Black
        Me.txtRevenue.Location = New System.Drawing.Point(810, 19)
        Me.txtRevenue.Name = "txtRevenue"
        Me.txtRevenue.ReadOnly = True
        Me.txtRevenue.Size = New System.Drawing.Size(166, 22)
        Me.txtRevenue.TabIndex = 8165
        Me.txtRevenue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(735, 22)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(69, 16)
        Me.Label11.TabIndex = 8166
        Me.Label11.Text = "Revenue :"
        '
        'txtTotalCost
        '
        Me.txtTotalCost.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTotalCost.BackColor = System.Drawing.Color.White
        Me.txtTotalCost.Enabled = False
        Me.txtTotalCost.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtTotalCost.Location = New System.Drawing.Point(1109, 88)
        Me.txtTotalCost.Name = "txtTotalCost"
        Me.txtTotalCost.ReadOnly = True
        Me.txtTotalCost.Size = New System.Drawing.Size(166, 22)
        Me.txtTotalCost.TabIndex = 8163
        Me.txtTotalCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(1028, 91)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 16)
        Me.Label8.TabIndex = 8164
        Me.Label8.Text = "Total Cost :"
        '
        'txtFOcost
        '
        Me.txtFOcost.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFOcost.BackColor = System.Drawing.Color.White
        Me.txtFOcost.Enabled = False
        Me.txtFOcost.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtFOcost.ForeColor = System.Drawing.Color.Black
        Me.txtFOcost.Location = New System.Drawing.Point(1109, 65)
        Me.txtFOcost.Name = "txtFOcost"
        Me.txtFOcost.ReadOnly = True
        Me.txtFOcost.Size = New System.Drawing.Size(166, 22)
        Me.txtFOcost.TabIndex = 8161
        Me.txtFOcost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(999, 68)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(104, 16)
        Me.Label16.TabIndex = 8162
        Me.Label16.Text = "Overhead Cost :"
        '
        'txtDLcost
        '
        Me.txtDLcost.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDLcost.BackColor = System.Drawing.Color.White
        Me.txtDLcost.Enabled = False
        Me.txtDLcost.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtDLcost.ForeColor = System.Drawing.Color.Black
        Me.txtDLcost.Location = New System.Drawing.Point(1109, 42)
        Me.txtDLcost.Name = "txtDLcost"
        Me.txtDLcost.ReadOnly = True
        Me.txtDLcost.Size = New System.Drawing.Size(166, 22)
        Me.txtDLcost.TabIndex = 8159
        Me.txtDLcost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(979, 45)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(124, 16)
        Me.Label17.TabIndex = 8160
        Me.Label17.Text = "Direct Labour Cost :"
        '
        'txtDMcost
        '
        Me.txtDMcost.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDMcost.BackColor = System.Drawing.Color.White
        Me.txtDMcost.Enabled = False
        Me.txtDMcost.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtDMcost.ForeColor = System.Drawing.Color.Black
        Me.txtDMcost.Location = New System.Drawing.Point(1109, 19)
        Me.txtDMcost.Name = "txtDMcost"
        Me.txtDMcost.ReadOnly = True
        Me.txtDMcost.Size = New System.Drawing.Size(166, 22)
        Me.txtDMcost.TabIndex = 8157
        Me.txtDMcost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(1011, 22)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(92, 16)
        Me.Label20.TabIndex = 8158
        Me.Label20.Text = "Material Cost :"
        '
        'txtDescription
        '
        Me.txtDescription.BackColor = System.Drawing.Color.White
        Me.txtDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(437, 19)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(237, 88)
        Me.txtDescription.TabIndex = 8139
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(349, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 16)
        Me.Label7.TabIndex = 8140
        Me.Label7.Text = "Description :"
        '
        'txtCode
        '
        Me.txtCode.BackColor = System.Drawing.Color.White
        Me.txtCode.Enabled = False
        Me.txtCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtCode.ForeColor = System.Drawing.Color.Black
        Me.txtCode.Location = New System.Drawing.Point(105, 19)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(238, 22)
        Me.txtCode.TabIndex = 8137
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(17, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 16)
        Me.Label5.TabIndex = 8138
        Me.Label5.Text = "BOM Code :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.TabControl1)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(-1, 204)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1291, 432)
        Me.GroupBox2.TabIndex = 8140
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "BOM Details"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(3, 17)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1285, 412)
        Me.TabControl1.TabIndex = 8139
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgvItemMaster)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1277, 384)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Materials"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.dgvLabor)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1277, 384)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Direct Labour"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'dgvLabor
        '
        Me.dgvLabor.BackgroundColor = System.Drawing.Color.White
        Me.dgvLabor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLabor.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcDLactivity, Me.dgcDLrate, Me.dgcDLcrewNo, Me.dgcDLtime, Me.dgcDLTotalMins, Me.dgcDLtotalCost})
        Me.dgvLabor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLabor.Location = New System.Drawing.Point(3, 3)
        Me.dgvLabor.Name = "dgvLabor"
        Me.dgvLabor.Size = New System.Drawing.Size(1271, 378)
        Me.dgvLabor.TabIndex = 8139
        '
        'dgcDLactivity
        '
        Me.dgcDLactivity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.dgcDLactivity.HeaderText = "Activity"
        Me.dgcDLactivity.Name = "dgcDLactivity"
        '
        'dgcDLrate
        '
        Me.dgcDLrate.HeaderText = "Rate/Hour"
        Me.dgcDLrate.Name = "dgcDLrate"
        '
        'dgcDLcrewNo
        '
        Me.dgcDLcrewNo.HeaderText = "No. of Crew"
        Me.dgcDLcrewNo.Name = "dgcDLcrewNo"
        '
        'dgcDLtime
        '
        Me.dgcDLtime.HeaderText = "Time (in mins)"
        Me.dgcDLtime.Name = "dgcDLtime"
        '
        'dgcDLTotalMins
        '
        Me.dgcDLTotalMins.HeaderText = "Total Mins"
        Me.dgcDLTotalMins.Name = "dgcDLTotalMins"
        Me.dgcDLTotalMins.ReadOnly = True
        '
        'dgcDLtotalCost
        '
        Me.dgcDLtotalCost.HeaderText = "Total Cost"
        Me.dgcDLtotalCost.Name = "dgcDLtotalCost"
        Me.dgcDLtotalCost.ReadOnly = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.dgvOverhead)
        Me.TabPage3.Location = New System.Drawing.Point(4, 24)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(1277, 384)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Overhead"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'dgvOverhead
        '
        Me.dgvOverhead.BackgroundColor = System.Drawing.Color.White
        Me.dgvOverhead.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOverhead.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcFOactivity, Me.dgcFOmachine, Me.dgcFOrate, Me.dgcFOKW, Me.dgcFOhrs, Me.dgcFOtotalKWH, Me.dgcFOcost})
        Me.dgvOverhead.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvOverhead.Location = New System.Drawing.Point(3, 3)
        Me.dgvOverhead.Name = "dgvOverhead"
        Me.dgvOverhead.Size = New System.Drawing.Size(1271, 378)
        Me.dgvOverhead.TabIndex = 8140
        '
        'dgcFOactivity
        '
        Me.dgcFOactivity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.dgcFOactivity.HeaderText = "Activity"
        Me.dgcFOactivity.Name = "dgcFOactivity"
        '
        'dgcFOmachine
        '
        Me.dgcFOmachine.HeaderText = "Machine"
        Me.dgcFOmachine.Name = "dgcFOmachine"
        Me.dgcFOmachine.Width = 150
        '
        'dgcFOrate
        '
        Me.dgcFOrate.HeaderText = "Rate/Hour"
        Me.dgcFOrate.Name = "dgcFOrate"
        '
        'dgcFOKW
        '
        Me.dgcFOKW.HeaderText = "KW"
        Me.dgcFOKW.Name = "dgcFOKW"
        '
        'dgcFOhrs
        '
        Me.dgcFOhrs.HeaderText = "No. of Hrs"
        Me.dgcFOhrs.Name = "dgcFOhrs"
        '
        'dgcFOtotalKWH
        '
        Me.dgcFOtotalKWH.HeaderText = "Total KWH"
        Me.dgcFOtotalKWH.Name = "dgcFOtotalKWH"
        '
        'dgcFOcost
        '
        Me.dgcFOcost.HeaderText = "Total Cost"
        Me.dgcFOcost.Name = "dgcFOcost"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSearch, Me.tsbNew, Me.tsbEdit, Me.tsbSave, Me.tsbCancel, Me.ToolStripSeparator4, Me.tsbUpload, Me.tsbReports, Me.ToolStripSeparator2, Me.tsbPrevious, Me.tsbNext, Me.ToolStripSeparator3, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1290, 40)
        Me.ToolStrip1.TabIndex = 8141
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbSearch
        '
        Me.tsbSearch.AutoSize = False
        Me.tsbSearch.ForeColor = System.Drawing.Color.White
        Me.tsbSearch.Image = Global.jade.My.Resources.Resources.view
        Me.tsbSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.tsbSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSearch.Name = "tsbSearch"
        Me.tsbSearch.Size = New System.Drawing.Size(50, 35)
        Me.tsbSearch.Text = "Search"
        Me.tsbSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        Me.tsbSave.ForeColor = System.Drawing.Color.White
        Me.tsbSave.Image = Global.jade.My.Resources.Resources.Save_Icon
        Me.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSave.Name = "tsbSave"
        Me.tsbSave.Size = New System.Drawing.Size(50, 35)
        Me.tsbSave.Text = "Save"
        Me.tsbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbCancel
        '
        Me.tsbCancel.AutoSize = False
        Me.tsbCancel.ForeColor = System.Drawing.Color.White
        Me.tsbCancel.Image = Global.jade.My.Resources.Resources.close_icon
        Me.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbCancel.Name = "tsbCancel"
        Me.tsbCancel.Size = New System.Drawing.Size(50, 35)
        Me.tsbCancel.Text = "Cancel"
        Me.tsbCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 40)
        '
        'tsbUpload
        '
        Me.tsbUpload.AutoSize = False
        Me.tsbUpload.ForeColor = System.Drawing.Color.White
        Me.tsbUpload.Image = CType(resources.GetObject("tsbUpload.Image"), System.Drawing.Image)
        Me.tsbUpload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbUpload.Name = "tsbUpload"
        Me.tsbUpload.Size = New System.Drawing.Size(50, 35)
        Me.tsbUpload.Text = "Upload"
        Me.tsbUpload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbReports
        '
        Me.tsbReports.ForeColor = System.Drawing.Color.White
        Me.tsbReports.Image = Global.jade.My.Resources.Resources.finance_report_infographic_512
        Me.tsbReports.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbReports.Name = "tsbReports"
        Me.tsbReports.Size = New System.Drawing.Size(60, 37)
        Me.tsbReports.Text = "Reports"
        Me.tsbReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbReports.Visible = False
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 40)
        '
        'tsbPrevious
        '
        Me.tsbPrevious.AutoSize = False
        Me.tsbPrevious.ForeColor = System.Drawing.Color.White
        Me.tsbPrevious.Image = Global.jade.My.Resources.Resources.arrows_147746_960_720
        Me.tsbPrevious.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPrevious.Name = "tsbPrevious"
        Me.tsbPrevious.Size = New System.Drawing.Size(50, 35)
        Me.tsbPrevious.Text = "Previous"
        Me.tsbPrevious.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbNext
        '
        Me.tsbNext.AutoSize = False
        Me.tsbNext.ForeColor = System.Drawing.Color.White
        Me.tsbNext.Image = Global.jade.My.Resources.Resources.red_arrow_png_15
        Me.tsbNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbNext.Name = "tsbNext"
        Me.tsbNext.Size = New System.Drawing.Size(50, 35)
        Me.tsbNext.Text = "Next"
        Me.tsbNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 40)
        '
        'tsbClose
        '
        Me.tsbClose.AutoSize = False
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
        'frmBOM_SFG
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1290, 636)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmBOM_SFG"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BOM - Semi Finished Goods"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgvItemMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dgvLabor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.dgvOverhead, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cbUOM As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtItemCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtItemName As System.Windows.Forms.TextBox
    Friend WithEvents txtQTY As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dgvItemMaster As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents dgvLabor As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents dgvOverhead As System.Windows.Forms.DataGridView
    Friend WithEvents dgcFOactivity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcFOmachine As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcFOrate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcFOKW As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcFOhrs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcFOtotalKWH As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcFOcost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbReports As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbPrevious As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNext As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbUpload As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtFOcost As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtDLcost As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtDMcost As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents dgcDLactivity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcDLrate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcDLcrewNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcDLtime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcDLTotalMins As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcDLtotalCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chIDX As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcItemCategory As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcItemGroup As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chBOMItemCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chBOMItemName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chBOMUOM As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents chBOMQTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chTotalCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtProfitMargin As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtCOS As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCOGS As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtSetAside As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtRevenue As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
End Class
