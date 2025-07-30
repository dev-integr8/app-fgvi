<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBOME
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBOME))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkForPR = New System.Windows.Forms.CheckBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.txtLotSize = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtJORef = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnExpload = New System.Windows.Forms.Button()
        Me.txtUOM = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtQTY = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cbBOM = New System.Windows.Forms.ComboBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.btnSearchVCE = New System.Windows.Forms.Button()
        Me.txtVCEName = New System.Windows.Forms.TextBox()
        Me.txtVCECode = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpDocDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTransNum = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbSearch = New System.Windows.Forms.ToolStripButton()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbCopy = New System.Windows.Forms.ToolStripDropDownButton()
        Me.tsbCopyPR = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsbPrint = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbSettings = New System.Windows.Forms.ToolStripDropDownButton()
        Me.tsmiBOMlist = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsbReports = New System.Windows.Forms.ToolStripDropDownButton()
        Me.TestToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrevious = New System.Windows.Forms.ToolStripButton()
        Me.tsbNext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.dgvItemList = New System.Windows.Forms.DataGridView()
        Me.dgcBOMgroup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBOM_ItemCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBOM_ItemDesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcUOM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcQTY_Per_BOM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcPR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcUC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcTC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBOMCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtVAT = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtGross = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtDiscount = New System.Windows.Forms.TextBox()
        Me.txtTotalCost = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtFOcost = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtDLcost = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtDMcost = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
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
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.dgcHeader_ItemList = New System.Windows.Forms.DataGridView()
        Me.chHeader_ItemCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chHeader_ItemName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chHeader_ItemUOM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chHeader_QTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chHeader_Size = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chHeader_LotSize = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chHeader_BOMCode = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.btnMultipleExplode = New System.Windows.Forms.Button()
        Me.contextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ViewItemInformationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvLabor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.dgvOverhead, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl2.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.dgcHeader_ItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.contextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.chkForPR)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.txtLotSize)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.txtJORef)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.btnExpload)
        Me.GroupBox1.Controls.Add(Me.txtUOM)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.txtQTY)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.cbBOM)
        Me.GroupBox1.Controls.Add(Me.txtDescription)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtItemCode)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.txtStatus)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.btnSearchVCE)
        Me.GroupBox1.Controls.Add(Me.txtVCEName)
        Me.GroupBox1.Controls.Add(Me.txtVCECode)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.dtpDocDate)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtTransNum)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 40)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(1182, 163)
        Me.GroupBox1.TabIndex = 1302
        Me.GroupBox1.TabStop = False
        '
        'chkForPR
        '
        Me.chkForPR.AutoSize = True
        Me.chkForPR.Checked = True
        Me.chkForPR.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkForPR.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.chkForPR.Location = New System.Drawing.Point(822, 65)
        Me.chkForPR.Name = "chkForPR"
        Me.chkForPR.Size = New System.Drawing.Size(68, 20)
        Me.chkForPR.TabIndex = 1342
        Me.chkForPR.Text = "For PR"
        Me.chkForPR.UseVisualStyleBackColor = True
        Me.chkForPR.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(823, 106)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(81, 22)
        Me.TextBox1.TabIndex = 1341
        Me.TextBox1.Visible = False
        '
        'txtLotSize
        '
        Me.txtLotSize.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLotSize.Location = New System.Drawing.Point(626, 109)
        Me.txtLotSize.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLotSize.Name = "txtLotSize"
        Me.txtLotSize.ReadOnly = True
        Me.txtLotSize.Size = New System.Drawing.Size(189, 22)
        Me.txtLotSize.TabIndex = 1340
        Me.txtLotSize.Visible = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(555, 112)
        Me.Label19.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(64, 16)
        Me.Label19.TabIndex = 1339
        Me.Label19.Text = "Lot Size :"
        Me.Label19.Visible = False
        '
        'txtJORef
        '
        Me.txtJORef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtJORef.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJORef.Location = New System.Drawing.Point(1044, 84)
        Me.txtJORef.Margin = New System.Windows.Forms.Padding(4)
        Me.txtJORef.Name = "txtJORef"
        Me.txtJORef.ReadOnly = True
        Me.txtJORef.Size = New System.Drawing.Size(132, 22)
        Me.txtJORef.TabIndex = 1335
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(981, 89)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(59, 16)
        Me.Label14.TabIndex = 1334
        Me.Label14.Text = "JO Ref. :"
        '
        'btnExpload
        '
        Me.btnExpload.Location = New System.Drawing.Point(987, 113)
        Me.btnExpload.Name = "btnExpload"
        Me.btnExpload.Size = New System.Drawing.Size(189, 28)
        Me.btnExpload.TabIndex = 1338
        Me.btnExpload.Text = "Explode BOM"
        Me.btnExpload.UseVisualStyleBackColor = True
        Me.btnExpload.Visible = False
        '
        'txtUOM
        '
        Me.txtUOM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUOM.Location = New System.Drawing.Point(626, 63)
        Me.txtUOM.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUOM.Name = "txtUOM"
        Me.txtUOM.ReadOnly = True
        Me.txtUOM.Size = New System.Drawing.Size(189, 22)
        Me.txtUOM.TabIndex = 1333
        Me.txtUOM.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(574, 65)
        Me.Label6.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 16)
        Me.Label6.TabIndex = 1332
        Me.Label6.Text = "UOM :"
        Me.Label6.Visible = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(538, 135)
        Me.Label18.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(80, 16)
        Me.Label18.TabIndex = 1337
        Me.Label18.Text = "BOM Code :"
        Me.Label18.Visible = False
        '
        'txtQTY
        '
        Me.txtQTY.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQTY.Location = New System.Drawing.Point(626, 86)
        Me.txtQTY.Margin = New System.Windows.Forms.Padding(4)
        Me.txtQTY.Name = "txtQTY"
        Me.txtQTY.ReadOnly = True
        Me.txtQTY.Size = New System.Drawing.Size(189, 22)
        Me.txtQTY.TabIndex = 1331
        Me.txtQTY.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(528, 88)
        Me.Label11.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(92, 16)
        Me.Label11.TabIndex = 1330
        Me.Label11.Text = "Ordered QTY :"
        Me.Label11.Visible = False
        '
        'cbBOM
        '
        Me.cbBOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBOM.FormattingEnabled = True
        Me.cbBOM.Location = New System.Drawing.Point(626, 132)
        Me.cbBOM.Name = "cbBOM"
        Me.cbBOM.Size = New System.Drawing.Size(309, 24)
        Me.cbBOM.TabIndex = 1336
        Me.cbBOM.Visible = False
        '
        'txtDescription
        '
        Me.txtDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(626, 39)
        Me.txtDescription.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReadOnly = True
        Me.txtDescription.Size = New System.Drawing.Size(309, 22)
        Me.txtDescription.TabIndex = 1329
        Me.txtDescription.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(510, 42)
        Me.Label12.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(110, 16)
        Me.Label12.TabIndex = 1328
        Me.Label12.Text = "Item Description :"
        Me.Label12.Visible = False
        '
        'txtItemCode
        '
        Me.txtItemCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.Location = New System.Drawing.Point(626, 15)
        Me.txtItemCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.ReadOnly = True
        Me.txtItemCode.Size = New System.Drawing.Size(191, 22)
        Me.txtItemCode.TabIndex = 1327
        Me.txtItemCode.Visible = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(545, 19)
        Me.Label15.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(75, 16)
        Me.Label15.TabIndex = 1326
        Me.Label15.Text = "Item Code :"
        Me.Label15.Visible = False
        '
        'txtStatus
        '
        Me.txtStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtStatus.Enabled = False
        Me.txtStatus.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(1044, 60)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(132, 22)
        Me.txtStatus.TabIndex = 1311
        Me.txtStatus.Text = "Open"
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(986, 63)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(54, 16)
        Me.Label13.TabIndex = 1310
        Me.Label13.Text = "Status :"
        '
        'btnSearchVCE
        '
        Me.btnSearchVCE.BackgroundImage = Global.jade.My.Resources.Resources.view
        Me.btnSearchVCE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchVCE.Location = New System.Drawing.Point(456, 12)
        Me.btnSearchVCE.Name = "btnSearchVCE"
        Me.btnSearchVCE.Size = New System.Drawing.Size(25, 25)
        Me.btnSearchVCE.TabIndex = 1309
        Me.btnSearchVCE.UseVisualStyleBackColor = True
        '
        'txtVCEName
        '
        Me.txtVCEName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVCEName.Location = New System.Drawing.Point(112, 38)
        Me.txtVCEName.Name = "txtVCEName"
        Me.txtVCEName.Size = New System.Drawing.Size(369, 22)
        Me.txtVCEName.TabIndex = 1308
        '
        'txtVCECode
        '
        Me.txtVCECode.Enabled = False
        Me.txtVCECode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVCECode.Location = New System.Drawing.Point(112, 13)
        Me.txtVCECode.Name = "txtVCECode"
        Me.txtVCECode.Size = New System.Drawing.Size(338, 22)
        Me.txtVCECode.TabIndex = 1307
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(112, 64)
        Me.txtRemarks.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(369, 75)
        Me.txtRemarks.TabIndex = 1300
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(45, 67)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 16)
        Me.Label2.TabIndex = 1294
        Me.Label2.Text = "Remarks :"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(963, 39)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 16)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "BOM Date :"
        '
        'dtpDocDate
        '
        Me.dtpDocDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpDocDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDocDate.Location = New System.Drawing.Point(1044, 36)
        Me.dtpDocDate.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.Size = New System.Drawing.Size(132, 22)
        Me.dtpDocDate.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 40)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Customer Name :"
        '
        'txtTransNum
        '
        Me.txtTransNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTransNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransNum.Location = New System.Drawing.Point(1044, 12)
        Me.txtTransNum.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTransNum.Name = "txtTransNum"
        Me.txtTransNum.Size = New System.Drawing.Size(132, 22)
        Me.txtTransNum.TabIndex = 44
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(7, 15)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Costumer Code :"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(970, 15)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 16)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "BOM No. :"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSearch, Me.tsbNew, Me.tsbEdit, Me.tsbSave, Me.tsbCancel, Me.ToolStripSeparator1, Me.tsbCopy, Me.tsbPrint, Me.ToolStripSeparator4, Me.tsbSettings, Me.tsbReports, Me.ToolStripSeparator2, Me.tsbPrevious, Me.tsbNext, Me.ToolStripSeparator3, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1195, 40)
        Me.ToolStrip1.TabIndex = 1345
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
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 40)
        '
        'tsbCopy
        '
        Me.tsbCopy.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbCopyPR})
        Me.tsbCopy.ForeColor = System.Drawing.Color.White
        Me.tsbCopy.Image = Global.jade.My.Resources.Resources.finance_report_infographic_512
        Me.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbCopy.Name = "tsbCopy"
        Me.tsbCopy.Size = New System.Drawing.Size(48, 37)
        Me.tsbCopy.Text = "Copy"
        Me.tsbCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbCopyPR
        '
        Me.tsbCopyPR.Name = "tsbCopyPR"
        Me.tsbCopyPR.Size = New System.Drawing.Size(156, 22)
        Me.tsbCopyPR.Text = "From Job Order"
        '
        'tsbPrint
        '
        Me.tsbPrint.AutoSize = False
        Me.tsbPrint.ForeColor = System.Drawing.Color.White
        Me.tsbPrint.Image = Global.jade.My.Resources.Resources.printer_circle_blue_512
        Me.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPrint.Name = "tsbPrint"
        Me.tsbPrint.Size = New System.Drawing.Size(50, 35)
        Me.tsbPrint.Text = "Print"
        Me.tsbPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 40)
        '
        'tsbSettings
        '
        Me.tsbSettings.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiBOMlist})
        Me.tsbSettings.ForeColor = System.Drawing.Color.White
        Me.tsbSettings.Image = Global.jade.My.Resources.Resources.finance_report_infographic_512
        Me.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSettings.Name = "tsbSettings"
        Me.tsbSettings.Size = New System.Drawing.Size(62, 37)
        Me.tsbSettings.Text = "Settings"
        Me.tsbSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsmiBOMlist
        '
        Me.tsmiBOMlist.Name = "tsmiBOMlist"
        Me.tsmiBOMlist.Size = New System.Drawing.Size(173, 22)
        Me.tsmiBOMlist.Text = "BOM Maintenance"
        '
        'tsbReports
        '
        Me.tsbReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TestToolStripMenuItem1})
        Me.tsbReports.ForeColor = System.Drawing.Color.White
        Me.tsbReports.Image = Global.jade.My.Resources.Resources.finance_report_infographic_512
        Me.tsbReports.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbReports.Name = "tsbReports"
        Me.tsbReports.Size = New System.Drawing.Size(60, 37)
        Me.tsbReports.Text = "Reports"
        Me.tsbReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbReports.Visible = False
        '
        'TestToolStripMenuItem1
        '
        Me.TestToolStripMenuItem1.Name = "TestToolStripMenuItem1"
        Me.TestToolStripMenuItem1.Size = New System.Drawing.Size(143, 22)
        Me.TestToolStripMenuItem1.Text = "SO Summary"
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
        'dgvItemList
        '
        Me.dgvItemList.AllowUserToAddRows = False
        Me.dgvItemList.AllowUserToDeleteRows = False
        Me.dgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvItemList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcBOMgroup, Me.dgcBOM_ItemCode, Me.dgcBOM_ItemDesc, Me.dgcUOM, Me.dgcQTY_Per_BOM, Me.dgcPR, Me.dgcUC, Me.dgcTC, Me.dgcBOMCode})
        Me.dgvItemList.ContextMenuStrip = Me.contextMenuStrip1
        Me.dgvItemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvItemList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvItemList.Location = New System.Drawing.Point(3, 3)
        Me.dgvItemList.Name = "dgvItemList"
        Me.dgvItemList.RowHeadersWidth = 25
        Me.dgvItemList.Size = New System.Drawing.Size(673, 365)
        Me.dgvItemList.TabIndex = 1346
        '
        'dgcBOMgroup
        '
        Me.dgcBOMgroup.HeaderText = "BOM Group"
        Me.dgcBOMgroup.Name = "dgcBOMgroup"
        Me.dgcBOMgroup.Visible = False
        Me.dgcBOMgroup.Width = 230
        '
        'dgcBOM_ItemCode
        '
        Me.dgcBOM_ItemCode.HeaderText = "Item Code"
        Me.dgcBOM_ItemCode.Name = "dgcBOM_ItemCode"
        '
        'dgcBOM_ItemDesc
        '
        Me.dgcBOM_ItemDesc.HeaderText = "Description"
        Me.dgcBOM_ItemDesc.Name = "dgcBOM_ItemDesc"
        Me.dgcBOM_ItemDesc.Width = 250
        '
        'dgcUOM
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.dgcUOM.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgcUOM.HeaderText = "UOM"
        Me.dgcUOM.Name = "dgcUOM"
        Me.dgcUOM.Width = 80
        '
        'dgcQTY_Per_BOM
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = "0"
        Me.dgcQTY_Per_BOM.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgcQTY_Per_BOM.HeaderText = "QTY per BOM"
        Me.dgcQTY_Per_BOM.Name = "dgcQTY_Per_BOM"
        '
        'dgcPR
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N4"
        DataGridViewCellStyle3.NullValue = "0"
        Me.dgcPR.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgcPR.HeaderText = "Gross Requirement"
        Me.dgcPR.Name = "dgcPR"
        Me.dgcPR.Width = 150
        '
        'dgcUC
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = "0"
        Me.dgcUC.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgcUC.HeaderText = "Standard Cost"
        Me.dgcUC.Name = "dgcUC"
        '
        'dgcTC
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = "0"
        Me.dgcTC.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgcTC.HeaderText = "Total Cost"
        Me.dgcTC.Name = "dgcTC"
        Me.dgcTC.ReadOnly = True
        Me.dgcTC.Width = 150
        '
        'dgcBOMCode
        '
        Me.dgcBOMCode.HeaderText = "BOMCode"
        Me.dgcBOMCode.Name = "dgcBOMCode"
        Me.dgcBOMCode.Visible = False
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(141, 322)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 16)
        Me.Label4.TabIndex = 1359
        Me.Label4.Text = "% Yield :"
        Me.Label4.Visible = False
        '
        'txtVAT
        '
        Me.txtVAT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtVAT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVAT.Location = New System.Drawing.Point(221, 318)
        Me.txtVAT.Name = "txtVAT"
        Me.txtVAT.ReadOnly = True
        Me.txtVAT.Size = New System.Drawing.Size(177, 22)
        Me.txtVAT.TabIndex = 1358
        Me.txtVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtVAT.Visible = False
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(97, 266)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(95, 16)
        Me.Label10.TabIndex = 1357
        Me.Label10.Text = "Batch Weight :"
        Me.Label10.Visible = False
        '
        'txtGross
        '
        Me.txtGross.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGross.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGross.Location = New System.Drawing.Point(221, 262)
        Me.txtGross.Name = "txtGross"
        Me.txtGross.ReadOnly = True
        Me.txtGross.Size = New System.Drawing.Size(177, 22)
        Me.txtGross.TabIndex = 1356
        Me.txtGross.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGross.Visible = False
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(111, 294)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(86, 16)
        Me.Label9.TabIndex = 1355
        Me.Label9.Text = "Output in kgs"
        Me.Label9.Visible = False
        '
        'txtDiscount
        '
        Me.txtDiscount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscount.Location = New System.Drawing.Point(221, 290)
        Me.txtDiscount.Name = "txtDiscount"
        Me.txtDiscount.ReadOnly = True
        Me.txtDiscount.Size = New System.Drawing.Size(177, 22)
        Me.txtDiscount.TabIndex = 1354
        Me.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDiscount.Visible = False
        '
        'txtTotalCost
        '
        Me.txtTotalCost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTotalCost.BackColor = System.Drawing.Color.White
        Me.txtTotalCost.Enabled = False
        Me.txtTotalCost.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtTotalCost.ForeColor = System.Drawing.Color.Black
        Me.txtTotalCost.Location = New System.Drawing.Point(144, 587)
        Me.txtTotalCost.Name = "txtTotalCost"
        Me.txtTotalCost.ReadOnly = True
        Me.txtTotalCost.Size = New System.Drawing.Size(267, 22)
        Me.txtTotalCost.TabIndex = 8155
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(63, 590)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 16)
        Me.Label8.TabIndex = 8156
        Me.Label8.Text = "Total Cost :"
        '
        'txtFOcost
        '
        Me.txtFOcost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtFOcost.BackColor = System.Drawing.Color.White
        Me.txtFOcost.Enabled = False
        Me.txtFOcost.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtFOcost.ForeColor = System.Drawing.Color.Black
        Me.txtFOcost.Location = New System.Drawing.Point(144, 564)
        Me.txtFOcost.Name = "txtFOcost"
        Me.txtFOcost.ReadOnly = True
        Me.txtFOcost.Size = New System.Drawing.Size(267, 22)
        Me.txtFOcost.TabIndex = 8153
        '
        'Label16
        '
        Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(34, 567)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(104, 16)
        Me.Label16.TabIndex = 8154
        Me.Label16.Text = "Overhead Cost :"
        '
        'txtDLcost
        '
        Me.txtDLcost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtDLcost.BackColor = System.Drawing.Color.White
        Me.txtDLcost.Enabled = False
        Me.txtDLcost.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtDLcost.ForeColor = System.Drawing.Color.Black
        Me.txtDLcost.Location = New System.Drawing.Point(144, 541)
        Me.txtDLcost.Name = "txtDLcost"
        Me.txtDLcost.ReadOnly = True
        Me.txtDLcost.Size = New System.Drawing.Size(267, 22)
        Me.txtDLcost.TabIndex = 8151
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(14, 544)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(124, 16)
        Me.Label17.TabIndex = 8152
        Me.Label17.Text = "Direct Labour Cost :"
        '
        'txtDMcost
        '
        Me.txtDMcost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtDMcost.BackColor = System.Drawing.Color.White
        Me.txtDMcost.Enabled = False
        Me.txtDMcost.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtDMcost.ForeColor = System.Drawing.Color.Black
        Me.txtDMcost.Location = New System.Drawing.Point(144, 518)
        Me.txtDMcost.Name = "txtDMcost"
        Me.txtDMcost.ReadOnly = True
        Me.txtDMcost.Size = New System.Drawing.Size(267, 22)
        Me.txtDMcost.TabIndex = 8149
        '
        'Label20
        '
        Me.Label20.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(46, 521)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(92, 16)
        Me.Label20.TabIndex = 8150
        Me.Label20.Text = "Material Cost :"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(503, 210)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(687, 400)
        Me.TabControl1.TabIndex = 8157
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgvItemList)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(679, 371)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Materials"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.dgvLabor)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(679, 371)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Labor"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'dgvLabor
        '
        Me.dgvLabor.AllowUserToAddRows = False
        Me.dgvLabor.AllowUserToDeleteRows = False
        Me.dgvLabor.BackgroundColor = System.Drawing.Color.White
        Me.dgvLabor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLabor.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcDLactivity, Me.dgcDLrate, Me.dgcDLcrewNo, Me.dgcDLtime, Me.dgcDLTotalMins, Me.dgcDLtotalCost})
        Me.dgvLabor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLabor.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvLabor.Location = New System.Drawing.Point(3, 3)
        Me.dgvLabor.Name = "dgvLabor"
        Me.dgvLabor.Size = New System.Drawing.Size(673, 365)
        Me.dgvLabor.TabIndex = 8140
        '
        'dgcDLactivity
        '
        Me.dgcDLactivity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.dgcDLactivity.HeaderText = "Activity"
        Me.dgcDLactivity.Name = "dgcDLactivity"
        '
        'dgcDLrate
        '
        Me.dgcDLrate.HeaderText = "Rate/Day"
        Me.dgcDLrate.Name = "dgcDLrate"
        '
        'dgcDLcrewNo
        '
        Me.dgcDLcrewNo.HeaderText = "No. of Crew"
        Me.dgcDLcrewNo.Name = "dgcDLcrewNo"
        '
        'dgcDLtime
        '
        Me.dgcDLtime.HeaderText = "Days"
        Me.dgcDLtime.Name = "dgcDLtime"
        '
        'dgcDLTotalMins
        '
        Me.dgcDLTotalMins.HeaderText = "Total Mins"
        Me.dgcDLTotalMins.Name = "dgcDLTotalMins"
        Me.dgcDLTotalMins.Visible = False
        '
        'dgcDLtotalCost
        '
        Me.dgcDLtotalCost.HeaderText = "Total Cost"
        Me.dgcDLtotalCost.Name = "dgcDLtotalCost"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.dgvOverhead)
        Me.TabPage3.Location = New System.Drawing.Point(4, 25)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(679, 371)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Overhead"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'dgvOverhead
        '
        Me.dgvOverhead.AllowUserToAddRows = False
        Me.dgvOverhead.AllowUserToDeleteRows = False
        Me.dgvOverhead.BackgroundColor = System.Drawing.Color.White
        Me.dgvOverhead.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOverhead.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcFOactivity, Me.dgcFOmachine, Me.dgcFOrate, Me.dgcFOKW, Me.dgcFOhrs, Me.dgcFOtotalKWH, Me.dgcFOcost})
        Me.dgvOverhead.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvOverhead.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvOverhead.Location = New System.Drawing.Point(3, 3)
        Me.dgvOverhead.Name = "dgvOverhead"
        Me.dgvOverhead.Size = New System.Drawing.Size(673, 365)
        Me.dgvOverhead.TabIndex = 8141
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
        Me.dgcFOmachine.Visible = False
        Me.dgcFOmachine.Width = 150
        '
        'dgcFOrate
        '
        Me.dgcFOrate.HeaderText = "Rate/Min"
        Me.dgcFOrate.Name = "dgcFOrate"
        '
        'dgcFOKW
        '
        Me.dgcFOKW.HeaderText = "KW"
        Me.dgcFOKW.Name = "dgcFOKW"
        Me.dgcFOKW.Visible = False
        '
        'dgcFOhrs
        '
        Me.dgcFOhrs.HeaderText = "No. of Mins"
        Me.dgcFOhrs.Name = "dgcFOhrs"
        '
        'dgcFOtotalKWH
        '
        Me.dgcFOtotalKWH.HeaderText = "Total KWH"
        Me.dgcFOtotalKWH.Name = "dgcFOtotalKWH"
        Me.dgcFOtotalKWH.Visible = False
        '
        'dgcFOcost
        '
        Me.dgcFOcost.HeaderText = "Total Cost"
        Me.dgcFOcost.Name = "dgcFOcost"
        '
        'TabControl2
        '
        Me.TabControl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TabControl2.Controls.Add(Me.TabPage4)
        Me.TabControl2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl2.Location = New System.Drawing.Point(8, 210)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(489, 272)
        Me.TabControl2.TabIndex = 8158
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.dgcHeader_ItemList)
        Me.TabPage4.Location = New System.Drawing.Point(4, 24)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(481, 244)
        Me.TabPage4.TabIndex = 0
        Me.TabPage4.Text = "Materials"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'dgcHeader_ItemList
        '
        Me.dgcHeader_ItemList.AllowUserToAddRows = False
        Me.dgcHeader_ItemList.AllowUserToDeleteRows = False
        Me.dgcHeader_ItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgcHeader_ItemList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chHeader_ItemCode, Me.chHeader_ItemName, Me.chHeader_ItemUOM, Me.chHeader_QTY, Me.chHeader_Size, Me.chHeader_LotSize, Me.chHeader_BOMCode})
        Me.dgcHeader_ItemList.ContextMenuStrip = Me.contextMenuStrip1
        Me.dgcHeader_ItemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgcHeader_ItemList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgcHeader_ItemList.Location = New System.Drawing.Point(3, 3)
        Me.dgcHeader_ItemList.Name = "dgcHeader_ItemList"
        Me.dgcHeader_ItemList.RowHeadersWidth = 25
        Me.dgcHeader_ItemList.Size = New System.Drawing.Size(475, 238)
        Me.dgcHeader_ItemList.TabIndex = 1346
        '
        'chHeader_ItemCode
        '
        Me.chHeader_ItemCode.HeaderText = "Item Code"
        Me.chHeader_ItemCode.Name = "chHeader_ItemCode"
        '
        'chHeader_ItemName
        '
        Me.chHeader_ItemName.HeaderText = "Description"
        Me.chHeader_ItemName.Name = "chHeader_ItemName"
        Me.chHeader_ItemName.Width = 250
        '
        'chHeader_ItemUOM
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.chHeader_ItemUOM.DefaultCellStyle = DataGridViewCellStyle6
        Me.chHeader_ItemUOM.HeaderText = "UOM"
        Me.chHeader_ItemUOM.Name = "chHeader_ItemUOM"
        Me.chHeader_ItemUOM.Width = 80
        '
        'chHeader_QTY
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N2"
        DataGridViewCellStyle7.NullValue = "0"
        Me.chHeader_QTY.DefaultCellStyle = DataGridViewCellStyle7
        Me.chHeader_QTY.HeaderText = "QTY"
        Me.chHeader_QTY.Name = "chHeader_QTY"
        '
        'chHeader_Size
        '
        Me.chHeader_Size.HeaderText = "Size"
        Me.chHeader_Size.Name = "chHeader_Size"
        '
        'chHeader_LotSize
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N2"
        DataGridViewCellStyle8.NullValue = "0"
        Me.chHeader_LotSize.DefaultCellStyle = DataGridViewCellStyle8
        Me.chHeader_LotSize.HeaderText = "Lot Size"
        Me.chHeader_LotSize.Name = "chHeader_LotSize"
        '
        'chHeader_BOMCode
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.Format = "N2"
        DataGridViewCellStyle9.NullValue = "0"
        Me.chHeader_BOMCode.DefaultCellStyle = DataGridViewCellStyle9
        Me.chHeader_BOMCode.HeaderText = "BOM Code"
        Me.chHeader_BOMCode.Name = "chHeader_BOMCode"
        Me.chHeader_BOMCode.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chHeader_BOMCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.chHeader_BOMCode.Width = 150
        '
        'btnMultipleExplode
        '
        Me.btnMultipleExplode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnMultipleExplode.Location = New System.Drawing.Point(144, 485)
        Me.btnMultipleExplode.Name = "btnMultipleExplode"
        Me.btnMultipleExplode.Size = New System.Drawing.Size(189, 28)
        Me.btnMultipleExplode.TabIndex = 8159
        Me.btnMultipleExplode.Text = "Explode BOM"
        Me.btnMultipleExplode.UseVisualStyleBackColor = True
        '
        'contextMenuStrip1
        '
        Me.contextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewItemInformationToolStripMenuItem})
        Me.contextMenuStrip1.Name = "contextMenuStrip1"
        Me.contextMenuStrip1.Size = New System.Drawing.Size(193, 26)
        '
        'ViewItemInformationToolStripMenuItem
        '
        Me.ViewItemInformationToolStripMenuItem.Name = "ViewItemInformationToolStripMenuItem"
        Me.ViewItemInformationToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
        Me.ViewItemInformationToolStripMenuItem.Text = "View Item Information"
        '
        'frmBOME
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1195, 622)
        Me.Controls.Add(Me.btnMultipleExplode)
        Me.Controls.Add(Me.TabControl2)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.txtTotalCost)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtFOcost)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtDLcost)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txtDMcost)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtVAT)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtGross)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtDiscount)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmBOME"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BOM Explosion"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dgvLabor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.dgvOverhead, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        CType(Me.dgcHeader_ItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.contextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpDocDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTransNum As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbCopy As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents tsbCopyPR As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbReports As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents TestToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbPrevious As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNext As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSearchVCE As System.Windows.Forms.Button
    Friend WithEvents txtVCEName As System.Windows.Forms.TextBox
    Friend WithEvents txtVCECode As System.Windows.Forms.TextBox
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents dgvItemList As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtVAT As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtGross As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtDiscount As System.Windows.Forms.TextBox
    Friend WithEvents txtUOM As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtQTY As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtItemCode As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtJORef As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cbBOM As System.Windows.Forms.ComboBox
    Friend WithEvents btnExpload As System.Windows.Forms.Button
    Friend WithEvents txtLotSize As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtFOcost As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtDLcost As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtDMcost As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents dgvLabor As System.Windows.Forms.DataGridView
    Friend WithEvents dgvOverhead As System.Windows.Forms.DataGridView
    Friend WithEvents tsbSettings As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents tsmiBOMlist As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents chkForPR As System.Windows.Forms.CheckBox
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents dgcHeader_ItemList As System.Windows.Forms.DataGridView
    Friend WithEvents btnMultipleExplode As System.Windows.Forms.Button
    Friend WithEvents dgcBOMgroup As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcBOM_ItemCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcBOM_ItemDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcUOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcQTY_Per_BOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcPR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcUC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcTC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcBOMCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcDLactivity As DataGridViewTextBoxColumn
    Friend WithEvents dgcDLrate As DataGridViewTextBoxColumn
    Friend WithEvents dgcDLcrewNo As DataGridViewTextBoxColumn
    Friend WithEvents dgcDLtime As DataGridViewTextBoxColumn
    Friend WithEvents dgcDLTotalMins As DataGridViewTextBoxColumn
    Friend WithEvents dgcDLtotalCost As DataGridViewTextBoxColumn
    Friend WithEvents dgcFOactivity As DataGridViewTextBoxColumn
    Friend WithEvents dgcFOmachine As DataGridViewTextBoxColumn
    Friend WithEvents dgcFOrate As DataGridViewTextBoxColumn
    Friend WithEvents dgcFOKW As DataGridViewTextBoxColumn
    Friend WithEvents dgcFOhrs As DataGridViewTextBoxColumn
    Friend WithEvents dgcFOtotalKWH As DataGridViewTextBoxColumn
    Friend WithEvents dgcFOcost As DataGridViewTextBoxColumn
    Friend WithEvents chHeader_ItemCode As DataGridViewTextBoxColumn
    Friend WithEvents chHeader_ItemName As DataGridViewTextBoxColumn
    Friend WithEvents chHeader_ItemUOM As DataGridViewTextBoxColumn
    Friend WithEvents chHeader_QTY As DataGridViewTextBoxColumn
    Friend WithEvents chHeader_Size As DataGridViewTextBoxColumn
    Friend WithEvents chHeader_LotSize As DataGridViewTextBoxColumn
    Friend WithEvents chHeader_BOMCode As DataGridViewComboBoxColumn
    Friend WithEvents contextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ViewItemInformationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
