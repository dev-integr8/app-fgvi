<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmItem_Master
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmItem_Master))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.tcItemDetails = New System.Windows.Forms.TabControl()
        Me.tpBarcode = New System.Windows.Forms.TabPage()
        Me.btnBarcodeUOM = New System.Windows.Forms.Button()
        Me.btnBarcodeRemove = New System.Windows.Forms.Button()
        Me.btnBarcodeAdd = New System.Windows.Forms.Button()
        Me.txtBarcode_Barcode = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtBarcode_UOM = New System.Windows.Forms.TextBox()
        Me.dgvBarcode = New System.Windows.Forms.DataGridView()
        Me.dcBarcode_Barcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dcBarCode_UOM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tpPD = New System.Windows.Forms.TabPage()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.txtPD_EOQ = New System.Windows.Forms.TextBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.txtPD_MOQ = New System.Windows.Forms.TextBox()
        Me.chkSerial = New System.Windows.Forms.CheckBox()
        Me.btnPurchSupplier = New System.Windows.Forms.Button()
        Me.txtPurchUOM = New System.Windows.Forms.TextBox()
        Me.btnPurchUOM = New System.Windows.Forms.Button()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtPurchReorder = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtPurchMinimum = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.chkPurchVATInc = New System.Windows.Forms.CheckBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtPurchPrice = New System.Windows.Forms.TextBox()
        Me.txtPurchVCECode = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtPurchVCEname = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.chkPurchUpdate = New System.Windows.Forms.CheckBox()
        Me.tpSD = New System.Windows.Forms.TabPage()
        Me.gbSales = New System.Windows.Forms.GroupBox()
        Me.GrpDefaultDiscount = New System.Windows.Forms.GroupBox()
        Me.RdNone = New System.Windows.Forms.RadioButton()
        Me.RdTwentyPercent = New System.Windows.Forms.RadioButton()
        Me.RdFivePercent = New System.Windows.Forms.RadioButton()
        Me.chkVAT = New System.Windows.Forms.CheckBox()
        Me.btnSellUOM = New System.Windows.Forms.Button()
        Me.dgvSell = New System.Windows.Forms.DataGridView()
        Me.dcSell_Type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dcSell_Price = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dcSell_UOM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dcSell_UOMQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dc_VAT = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dcSell_VAT = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.btnSellRemove = New System.Windows.Forms.Button()
        Me.cbSellType = New System.Windows.Forms.ComboBox()
        Me.btnSellAdd = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtSellPrice = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.chkSellVAT = New System.Windows.Forms.CheckBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtSellUOM = New System.Windows.Forms.TextBox()
        Me.txtSellQTY = New System.Windows.Forms.TextBox()
        Me.tpID = New System.Windows.Forms.TabPage()
        Me.cbDept = New System.Windows.Forms.ComboBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtInvStandard = New System.Windows.Forms.TextBox()
        Me.txtInvUOM = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cbInvWarehouse = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtInvMax = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtInvMin = New System.Windows.Forms.TextBox()
        Me.cbInvMethod = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtInvAvailable = New System.Windows.Forms.TextBox()
        Me.txtInvCommit = New System.Windows.Forms.TextBox()
        Me.txtInvOrder = New System.Windows.Forms.TextBox()
        Me.txtInvStock = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cbIDTolling = New System.Windows.Forms.ComboBox()
        Me.rbIDTolling = New System.Windows.Forms.RadioButton()
        Me.rbIDOwned = New System.Windows.Forms.RadioButton()
        Me.dgvInv = New System.Windows.Forms.DataGridView()
        Me.chInvWHSECode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chInvWHSEName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chInvStock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chInvOrdered = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chInvCommit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chInvAvail = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnInvUOM = New System.Windows.Forms.Button()
        Me.tpDefaultEntry = New System.Windows.Forms.TabPage()
        Me.txtConAccntTitle = New System.Windows.Forms.TextBox()
        Me.txtConAccntCode = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtFixedAssetAccountTitle = New System.Windows.Forms.TextBox()
        Me.txtFixedAssetAccountCode = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtDepExpTitle = New System.Windows.Forms.TextBox()
        Me.txtDepExpCode = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtAccumDepTitle = New System.Windows.Forms.TextBox()
        Me.txtAccumDepCode = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.btnSalesAccnt = New System.Windows.Forms.Button()
        Me.txtExpAccntTitle = New System.Windows.Forms.TextBox()
        Me.txtExpAccntCode = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.txtDiscAccntTitle = New System.Windows.Forms.TextBox()
        Me.txtDiscAccntCode = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtInvAccntTitle = New System.Windows.Forms.TextBox()
        Me.txtInvAccntCode = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtCostAccntTitle = New System.Windows.Forms.TextBox()
        Me.txtCostAccntCode = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSaleAccntTitle = New System.Windows.Forms.TextBox()
        Me.txtSaleAccntCode = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.tpMD = New System.Windows.Forms.TabPage()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cbMDprodFloor = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cbProd = New System.Windows.Forms.ComboBox()
        Me.tpBSCharges = New System.Windows.Forms.TabPage()
        Me.dgvItemList = New System.Windows.Forms.DataGridView()
        Me.chBS_RecordID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chBS_Desc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chBS_VCECode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chBS_VCEName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chBS_Type = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.chBS_Amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.grpSizeColor = New System.Windows.Forms.GroupBox()
        Me.txtSize = New System.Windows.Forms.TextBox()
        Me.dgvSizeColor = New System.Windows.Forms.DataGridView()
        Me.chSize = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chColor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtColor = New System.Windows.Forms.TextBox()
        Me.btnSizeColor_Remove = New System.Windows.Forms.Button()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.btnSizeColor_Add = New System.Windows.Forms.Button()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.chkVATable = New System.Windows.Forms.CheckBox()
        Me.chkInv = New System.Windows.Forms.CheckBox()
        Me.chkSale = New System.Windows.Forms.CheckBox()
        Me.chkPurch = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbItemType = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbItemCategory = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtBarcode = New System.Windows.Forms.TextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbSearch = New System.Windows.Forms.ToolStripButton()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrevious = New System.Windows.Forms.ToolStripButton()
        Me.tsbNext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.cbUoMGroup = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtUOMBase = New System.Windows.Forms.TextBox()
        Me.chkExcise = New System.Windows.Forms.CheckBox()
        Me.cbItemGroup = New System.Windows.Forms.ComboBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.chkProd = New System.Windows.Forms.CheckBox()
        Me.cbStatus = New System.Windows.Forms.ComboBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.btnBOMgroup = New System.Windows.Forms.Button()
        Me.btnUOMGroup = New System.Windows.Forms.Button()
        Me.btnUOM = New System.Windows.Forms.Button()
        Me.chkFixedAsset = New System.Windows.Forms.CheckBox()
        Me.chkMultipleBarcode = New System.Windows.Forms.CheckBox()
        Me.chkConsignment = New System.Windows.Forms.CheckBox()
        Me.lblG5 = New System.Windows.Forms.Label()
        Me.lblG4 = New System.Windows.Forms.Label()
        Me.lblG3 = New System.Windows.Forms.Label()
        Me.lblG2 = New System.Windows.Forms.Label()
        Me.lblG1 = New System.Windows.Forms.Label()
        Me.cbG5 = New System.Windows.Forms.ComboBox()
        Me.cbG4 = New System.Windows.Forms.ComboBox()
        Me.cbG3 = New System.Windows.Forms.ComboBox()
        Me.cbG2 = New System.Windows.Forms.ComboBox()
        Me.cbG1 = New System.Windows.Forms.ComboBox()
        Me.cbVATType = New System.Windows.Forms.ComboBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.tcItemDetails.SuspendLayout()
        Me.tpBarcode.SuspendLayout()
        CType(Me.dgvBarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpPD.SuspendLayout()
        Me.tpSD.SuspendLayout()
        Me.gbSales.SuspendLayout()
        Me.GrpDefaultDiscount.SuspendLayout()
        CType(Me.dgvSell, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpID.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvInv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpDefaultEntry.SuspendLayout()
        Me.tpMD.SuspendLayout()
        Me.tpBSCharges.SuspendLayout()
        CType(Me.dgvItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage1.SuspendLayout()
        Me.grpSizeColor.SuspendLayout()
        CType(Me.dgvSizeColor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(26, 86)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 16)
        Me.Label1.TabIndex = 1077
        Me.Label1.Text = "Item Name :"
        '
        'txtItemName
        '
        Me.txtItemName.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemName.ForeColor = System.Drawing.Color.Black
        Me.txtItemName.Location = New System.Drawing.Point(110, 82)
        Me.txtItemName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.Size = New System.Drawing.Size(468, 22)
        Me.txtItemName.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(30, 60)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 16)
        Me.Label4.TabIndex = 1087
        Me.Label4.Text = "Item Code :"
        '
        'txtItemCode
        '
        Me.txtItemCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtItemCode.ForeColor = System.Drawing.Color.Black
        Me.txtItemCode.Location = New System.Drawing.Point(110, 57)
        Me.txtItemCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.Size = New System.Drawing.Size(468, 22)
        Me.txtItemCode.TabIndex = 1
        '
        'tcItemDetails
        '
        Me.tcItemDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcItemDetails.Controls.Add(Me.tpBarcode)
        Me.tcItemDetails.Controls.Add(Me.tpPD)
        Me.tcItemDetails.Controls.Add(Me.tpSD)
        Me.tcItemDetails.Controls.Add(Me.tpID)
        Me.tcItemDetails.Controls.Add(Me.tpDefaultEntry)
        Me.tcItemDetails.Controls.Add(Me.tpMD)
        Me.tcItemDetails.Controls.Add(Me.tpBSCharges)
        Me.tcItemDetails.Controls.Add(Me.TabPage1)
        Me.tcItemDetails.Location = New System.Drawing.Point(13, 243)
        Me.tcItemDetails.Name = "tcItemDetails"
        Me.tcItemDetails.SelectedIndex = 0
        Me.tcItemDetails.Size = New System.Drawing.Size(1238, 441)
        Me.tcItemDetails.TabIndex = 1167
        '
        'tpBarcode
        '
        Me.tpBarcode.Controls.Add(Me.btnBarcodeUOM)
        Me.tpBarcode.Controls.Add(Me.btnBarcodeRemove)
        Me.tpBarcode.Controls.Add(Me.btnBarcodeAdd)
        Me.tpBarcode.Controls.Add(Me.txtBarcode_Barcode)
        Me.tpBarcode.Controls.Add(Me.Label37)
        Me.tpBarcode.Controls.Add(Me.Label38)
        Me.tpBarcode.Controls.Add(Me.txtBarcode_UOM)
        Me.tpBarcode.Controls.Add(Me.dgvBarcode)
        Me.tpBarcode.Location = New System.Drawing.Point(4, 25)
        Me.tpBarcode.Name = "tpBarcode"
        Me.tpBarcode.Size = New System.Drawing.Size(1230, 412)
        Me.tpBarcode.TabIndex = 9
        Me.tpBarcode.Text = "Barcode Data"
        Me.tpBarcode.UseVisualStyleBackColor = True
        '
        'btnBarcodeUOM
        '
        Me.btnBarcodeUOM.BackgroundImage = Global.jade.My.Resources.Resources.view
        Me.btnBarcodeUOM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBarcodeUOM.Location = New System.Drawing.Point(575, 36)
        Me.btnBarcodeUOM.Name = "btnBarcodeUOM"
        Me.btnBarcodeUOM.Size = New System.Drawing.Size(25, 25)
        Me.btnBarcodeUOM.TabIndex = 1219
        Me.btnBarcodeUOM.UseVisualStyleBackColor = True
        '
        'btnBarcodeRemove
        '
        Me.btnBarcodeRemove.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnBarcodeRemove.Enabled = False
        Me.btnBarcodeRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnBarcodeRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBarcodeRemove.ForeColor = System.Drawing.Color.White
        Me.btnBarcodeRemove.Location = New System.Drawing.Point(448, 118)
        Me.btnBarcodeRemove.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBarcodeRemove.Name = "btnBarcodeRemove"
        Me.btnBarcodeRemove.Size = New System.Drawing.Size(75, 24)
        Me.btnBarcodeRemove.TabIndex = 1218
        Me.btnBarcodeRemove.Text = "Remove"
        Me.btnBarcodeRemove.UseVisualStyleBackColor = False
        '
        'btnBarcodeAdd
        '
        Me.btnBarcodeAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnBarcodeAdd.Enabled = False
        Me.btnBarcodeAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnBarcodeAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBarcodeAdd.ForeColor = System.Drawing.Color.White
        Me.btnBarcodeAdd.Location = New System.Drawing.Point(448, 93)
        Me.btnBarcodeAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBarcodeAdd.Name = "btnBarcodeAdd"
        Me.btnBarcodeAdd.Size = New System.Drawing.Size(75, 24)
        Me.btnBarcodeAdd.TabIndex = 1217
        Me.btnBarcodeAdd.Text = "Add"
        Me.btnBarcodeAdd.UseVisualStyleBackColor = False
        '
        'txtBarcode_Barcode
        '
        Me.txtBarcode_Barcode.BackColor = System.Drawing.SystemColors.Window
        Me.txtBarcode_Barcode.ForeColor = System.Drawing.Color.Black
        Me.txtBarcode_Barcode.Location = New System.Drawing.Point(500, 12)
        Me.txtBarcode_Barcode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtBarcode_Barcode.Name = "txtBarcode_Barcode"
        Me.txtBarcode_Barcode.Size = New System.Drawing.Size(357, 22)
        Me.txtBarcode_Barcode.TabIndex = 1213
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.ForeColor = System.Drawing.Color.Black
        Me.Label37.Location = New System.Drawing.Point(454, 37)
        Me.Label37.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(45, 16)
        Me.Label37.TabIndex = 1216
        Me.Label37.Text = "UOM :"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.ForeColor = System.Drawing.Color.Black
        Me.Label38.Location = New System.Drawing.Point(434, 14)
        Me.Label38.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(66, 16)
        Me.Label38.TabIndex = 1214
        Me.Label38.Text = "Barcode :"
        '
        'txtBarcode_UOM
        '
        Me.txtBarcode_UOM.BackColor = System.Drawing.SystemColors.Window
        Me.txtBarcode_UOM.ForeColor = System.Drawing.Color.Black
        Me.txtBarcode_UOM.Location = New System.Drawing.Point(500, 37)
        Me.txtBarcode_UOM.Margin = New System.Windows.Forms.Padding(4)
        Me.txtBarcode_UOM.Name = "txtBarcode_UOM"
        Me.txtBarcode_UOM.Size = New System.Drawing.Size(68, 22)
        Me.txtBarcode_UOM.TabIndex = 1215
        '
        'dgvBarcode
        '
        Me.dgvBarcode.AllowUserToAddRows = False
        Me.dgvBarcode.AllowUserToDeleteRows = False
        Me.dgvBarcode.AllowUserToResizeColumns = False
        Me.dgvBarcode.AllowUserToResizeRows = False
        Me.dgvBarcode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgvBarcode.BackgroundColor = System.Drawing.Color.White
        Me.dgvBarcode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBarcode.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dcBarcode_Barcode, Me.dcBarCode_UOM})
        Me.dgvBarcode.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvBarcode.Location = New System.Drawing.Point(12, 12)
        Me.dgvBarcode.MultiSelect = False
        Me.dgvBarcode.Name = "dgvBarcode"
        Me.dgvBarcode.ReadOnly = True
        Me.dgvBarcode.RowHeadersVisible = False
        Me.dgvBarcode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvBarcode.Size = New System.Drawing.Size(392, 211)
        Me.dgvBarcode.TabIndex = 1159
        '
        'dcBarcode_Barcode
        '
        Me.dcBarcode_Barcode.HeaderText = "Barcode"
        Me.dcBarcode_Barcode.Name = "dcBarcode_Barcode"
        Me.dcBarcode_Barcode.ReadOnly = True
        Me.dcBarcode_Barcode.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dcBarcode_Barcode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.dcBarcode_Barcode.Width = 300
        '
        'dcBarCode_UOM
        '
        Me.dcBarCode_UOM.HeaderText = "UOM"
        Me.dcBarCode_UOM.Name = "dcBarCode_UOM"
        Me.dcBarCode_UOM.ReadOnly = True
        Me.dcBarCode_UOM.Width = 60
        '
        'tpPD
        '
        Me.tpPD.BackColor = System.Drawing.Color.White
        Me.tpPD.Controls.Add(Me.Label43)
        Me.tpPD.Controls.Add(Me.txtPD_EOQ)
        Me.tpPD.Controls.Add(Me.Label44)
        Me.tpPD.Controls.Add(Me.txtPD_MOQ)
        Me.tpPD.Controls.Add(Me.chkSerial)
        Me.tpPD.Controls.Add(Me.btnPurchSupplier)
        Me.tpPD.Controls.Add(Me.txtPurchUOM)
        Me.tpPD.Controls.Add(Me.btnPurchUOM)
        Me.tpPD.Controls.Add(Me.Label23)
        Me.tpPD.Controls.Add(Me.txtPurchReorder)
        Me.tpPD.Controls.Add(Me.Label24)
        Me.tpPD.Controls.Add(Me.txtPurchMinimum)
        Me.tpPD.Controls.Add(Me.Button1)
        Me.tpPD.Controls.Add(Me.chkPurchVATInc)
        Me.tpPD.Controls.Add(Me.Label17)
        Me.tpPD.Controls.Add(Me.txtPurchPrice)
        Me.tpPD.Controls.Add(Me.txtPurchVCECode)
        Me.tpPD.Controls.Add(Me.Label13)
        Me.tpPD.Controls.Add(Me.txtPurchVCEname)
        Me.tpPD.Controls.Add(Me.Label11)
        Me.tpPD.Controls.Add(Me.chkPurchUpdate)
        Me.tpPD.Location = New System.Drawing.Point(4, 25)
        Me.tpPD.Name = "tpPD"
        Me.tpPD.Padding = New System.Windows.Forms.Padding(3)
        Me.tpPD.Size = New System.Drawing.Size(1230, 412)
        Me.tpPD.TabIndex = 0
        Me.tpPD.Text = "Purchase Data"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.ForeColor = System.Drawing.Color.Black
        Me.Label43.Location = New System.Drawing.Point(64, 202)
        Me.Label43.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(162, 16)
        Me.Label43.TabIndex = 1217
        Me.Label43.Text = "Economic Order Quantity :"
        '
        'txtPD_EOQ
        '
        Me.txtPD_EOQ.BackColor = System.Drawing.SystemColors.Window
        Me.txtPD_EOQ.ForeColor = System.Drawing.Color.Black
        Me.txtPD_EOQ.Location = New System.Drawing.Point(227, 199)
        Me.txtPD_EOQ.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPD_EOQ.Name = "txtPD_EOQ"
        Me.txtPD_EOQ.Size = New System.Drawing.Size(157, 22)
        Me.txtPD_EOQ.TabIndex = 1216
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.ForeColor = System.Drawing.Color.Black
        Me.Label44.Location = New System.Drawing.Point(71, 177)
        Me.Label44.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(155, 16)
        Me.Label44.TabIndex = 1215
        Me.Label44.Text = "Minimum Order Quantity :"
        '
        'txtPD_MOQ
        '
        Me.txtPD_MOQ.BackColor = System.Drawing.SystemColors.Window
        Me.txtPD_MOQ.ForeColor = System.Drawing.Color.Black
        Me.txtPD_MOQ.Location = New System.Drawing.Point(227, 173)
        Me.txtPD_MOQ.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPD_MOQ.Name = "txtPD_MOQ"
        Me.txtPD_MOQ.Size = New System.Drawing.Size(157, 22)
        Me.txtPD_MOQ.TabIndex = 1214
        '
        'chkSerial
        '
        Me.chkSerial.AutoSize = True
        Me.chkSerial.Location = New System.Drawing.Point(227, 149)
        Me.chkSerial.Name = "chkSerial"
        Me.chkSerial.Size = New System.Drawing.Size(131, 20)
        Me.chkSerial.TabIndex = 1210
        Me.chkSerial.Text = "Serial # Required"
        Me.chkSerial.UseVisualStyleBackColor = True
        Me.chkSerial.Visible = False
        '
        'btnPurchSupplier
        '
        Me.btnPurchSupplier.BackgroundImage = Global.jade.My.Resources.Resources.view
        Me.btnPurchSupplier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPurchSupplier.Location = New System.Drawing.Point(647, 42)
        Me.btnPurchSupplier.Name = "btnPurchSupplier"
        Me.btnPurchSupplier.Size = New System.Drawing.Size(25, 25)
        Me.btnPurchSupplier.TabIndex = 1213
        Me.btnPurchSupplier.UseVisualStyleBackColor = True
        '
        'txtPurchUOM
        '
        Me.txtPurchUOM.BackColor = System.Drawing.SystemColors.Window
        Me.txtPurchUOM.Enabled = False
        Me.txtPurchUOM.ForeColor = System.Drawing.Color.Black
        Me.txtPurchUOM.Location = New System.Drawing.Point(227, 70)
        Me.txtPurchUOM.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPurchUOM.Name = "txtPurchUOM"
        Me.txtPurchUOM.Size = New System.Drawing.Size(111, 22)
        Me.txtPurchUOM.TabIndex = 1212
        '
        'btnPurchUOM
        '
        Me.btnPurchUOM.BackgroundImage = Global.jade.My.Resources.Resources.view
        Me.btnPurchUOM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPurchUOM.Location = New System.Drawing.Point(340, 69)
        Me.btnPurchUOM.Name = "btnPurchUOM"
        Me.btnPurchUOM.Size = New System.Drawing.Size(25, 25)
        Me.btnPurchUOM.TabIndex = 1211
        Me.btnPurchUOM.UseVisualStyleBackColor = True
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(520, 201)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(115, 16)
        Me.Label23.TabIndex = 1206
        Me.Label23.Text = "Reorder Quantity :"
        Me.Label23.Visible = False
        '
        'txtPurchReorder
        '
        Me.txtPurchReorder.BackColor = System.Drawing.SystemColors.Window
        Me.txtPurchReorder.ForeColor = System.Drawing.Color.Black
        Me.txtPurchReorder.Location = New System.Drawing.Point(638, 198)
        Me.txtPurchReorder.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPurchReorder.Name = "txtPurchReorder"
        Me.txtPurchReorder.Size = New System.Drawing.Size(157, 22)
        Me.txtPurchReorder.TabIndex = 1205
        Me.txtPurchReorder.Visible = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(472, 176)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(163, 16)
        Me.Label24.TabIndex = 1204
        Me.Label24.Text = "Minimum Before Reorder :"
        Me.Label24.Visible = False
        '
        'txtPurchMinimum
        '
        Me.txtPurchMinimum.BackColor = System.Drawing.SystemColors.Window
        Me.txtPurchMinimum.ForeColor = System.Drawing.Color.Black
        Me.txtPurchMinimum.Location = New System.Drawing.Point(638, 172)
        Me.txtPurchMinimum.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPurchMinimum.Name = "txtPurchMinimum"
        Me.txtPurchMinimum.Size = New System.Drawing.Size(157, 22)
        Me.txtPurchMinimum.TabIndex = 1203
        Me.txtPurchMinimum.Visible = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button1.Enabled = False
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(223, 231)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(142, 45)
        Me.Button1.TabIndex = 1202
        Me.Button1.Text = "Purchase History"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'chkPurchVATInc
        '
        Me.chkPurchVATInc.AutoSize = True
        Me.chkPurchVATInc.Checked = True
        Me.chkPurchVATInc.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPurchVATInc.Location = New System.Drawing.Point(227, 125)
        Me.chkPurchVATInc.Name = "chkPurchVATInc"
        Me.chkPurchVATInc.Size = New System.Drawing.Size(77, 20)
        Me.chkPurchVATInc.TabIndex = 1195
        Me.chkPurchVATInc.Text = "VAT Inc."
        Me.chkPurchVATInc.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(155, 98)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(71, 16)
        Me.Label17.TabIndex = 1187
        Me.Label17.Text = "Unit Price :"
        '
        'txtPurchPrice
        '
        Me.txtPurchPrice.BackColor = System.Drawing.SystemColors.Window
        Me.txtPurchPrice.ForeColor = System.Drawing.Color.Black
        Me.txtPurchPrice.Location = New System.Drawing.Point(227, 96)
        Me.txtPurchPrice.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPurchPrice.Name = "txtPurchPrice"
        Me.txtPurchPrice.Size = New System.Drawing.Size(111, 22)
        Me.txtPurchPrice.TabIndex = 1186
        '
        'txtPurchVCECode
        '
        Me.txtPurchVCECode.BackColor = System.Drawing.SystemColors.Window
        Me.txtPurchVCECode.ForeColor = System.Drawing.Color.Black
        Me.txtPurchVCECode.Location = New System.Drawing.Point(227, 44)
        Me.txtPurchVCECode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPurchVCECode.Name = "txtPurchVCECode"
        Me.txtPurchVCECode.Size = New System.Drawing.Size(111, 22)
        Me.txtPurchVCECode.TabIndex = 1183
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(101, 46)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(123, 16)
        Me.Label13.TabIndex = 1182
        Me.Label13.Text = "Preferred Supplier :"
        '
        'txtPurchVCEname
        '
        Me.txtPurchVCEname.BackColor = System.Drawing.SystemColors.Window
        Me.txtPurchVCEname.ForeColor = System.Drawing.Color.Black
        Me.txtPurchVCEname.Location = New System.Drawing.Point(339, 44)
        Me.txtPurchVCEname.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPurchVCEname.Name = "txtPurchVCEname"
        Me.txtPurchVCEname.Size = New System.Drawing.Size(305, 22)
        Me.txtPurchVCEname.TabIndex = 1181
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(121, 73)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(103, 16)
        Me.Label11.TabIndex = 1177
        Me.Label11.Text = "Purchase UoM :"
        '
        'chkPurchUpdate
        '
        Me.chkPurchUpdate.AutoSize = True
        Me.chkPurchUpdate.Location = New System.Drawing.Point(227, 21)
        Me.chkPurchUpdate.Name = "chkPurchUpdate"
        Me.chkPurchUpdate.Size = New System.Drawing.Size(219, 20)
        Me.chkPurchUpdate.TabIndex = 1172
        Me.chkPurchUpdate.Text = "Update based on Last purchase"
        Me.chkPurchUpdate.UseVisualStyleBackColor = True
        '
        'tpSD
        '
        Me.tpSD.Controls.Add(Me.gbSales)
        Me.tpSD.Location = New System.Drawing.Point(4, 25)
        Me.tpSD.Name = "tpSD"
        Me.tpSD.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSD.Size = New System.Drawing.Size(1230, 412)
        Me.tpSD.TabIndex = 6
        Me.tpSD.Text = "Sales Data"
        Me.tpSD.UseVisualStyleBackColor = True
        '
        'gbSales
        '
        Me.gbSales.Controls.Add(Me.GrpDefaultDiscount)
        Me.gbSales.Controls.Add(Me.chkVAT)
        Me.gbSales.Controls.Add(Me.btnSellUOM)
        Me.gbSales.Controls.Add(Me.dgvSell)
        Me.gbSales.Controls.Add(Me.btnSellRemove)
        Me.gbSales.Controls.Add(Me.cbSellType)
        Me.gbSales.Controls.Add(Me.btnSellAdd)
        Me.gbSales.Controls.Add(Me.Label20)
        Me.gbSales.Controls.Add(Me.txtSellPrice)
        Me.gbSales.Controls.Add(Me.Label15)
        Me.gbSales.Controls.Add(Me.chkSellVAT)
        Me.gbSales.Controls.Add(Me.Label18)
        Me.gbSales.Controls.Add(Me.Label16)
        Me.gbSales.Controls.Add(Me.txtSellUOM)
        Me.gbSales.Controls.Add(Me.txtSellQTY)
        Me.gbSales.Location = New System.Drawing.Point(8, 9)
        Me.gbSales.Name = "gbSales"
        Me.gbSales.Size = New System.Drawing.Size(1089, 397)
        Me.gbSales.TabIndex = 1172
        Me.gbSales.TabStop = False
        Me.gbSales.Text = "Prices"
        '
        'GrpDefaultDiscount
        '
        Me.GrpDefaultDiscount.Controls.Add(Me.RdNone)
        Me.GrpDefaultDiscount.Controls.Add(Me.RdTwentyPercent)
        Me.GrpDefaultDiscount.Controls.Add(Me.RdFivePercent)
        Me.GrpDefaultDiscount.Location = New System.Drawing.Point(816, 14)
        Me.GrpDefaultDiscount.Name = "GrpDefaultDiscount"
        Me.GrpDefaultDiscount.Size = New System.Drawing.Size(240, 71)
        Me.GrpDefaultDiscount.TabIndex = 1251
        Me.GrpDefaultDiscount.TabStop = False
        Me.GrpDefaultDiscount.Text = "SC/PWD Default Discount Percentage"
        '
        'RdNone
        '
        Me.RdNone.AutoSize = True
        Me.RdNone.Checked = True
        Me.RdNone.Location = New System.Drawing.Point(170, 32)
        Me.RdNone.Name = "RdNone"
        Me.RdNone.Size = New System.Drawing.Size(59, 20)
        Me.RdNone.TabIndex = 0
        Me.RdNone.TabStop = True
        Me.RdNone.Text = "None"
        Me.RdNone.UseVisualStyleBackColor = True
        '
        'RdTwentyPercent
        '
        Me.RdTwentyPercent.AutoSize = True
        Me.RdTwentyPercent.Location = New System.Drawing.Point(94, 32)
        Me.RdTwentyPercent.Name = "RdTwentyPercent"
        Me.RdTwentyPercent.Size = New System.Drawing.Size(52, 20)
        Me.RdTwentyPercent.TabIndex = 0
        Me.RdTwentyPercent.Text = "20%"
        Me.RdTwentyPercent.UseVisualStyleBackColor = True
        '
        'RdFivePercent
        '
        Me.RdFivePercent.AutoSize = True
        Me.RdFivePercent.Location = New System.Drawing.Point(18, 32)
        Me.RdFivePercent.Name = "RdFivePercent"
        Me.RdFivePercent.Size = New System.Drawing.Size(45, 20)
        Me.RdFivePercent.TabIndex = 0
        Me.RdFivePercent.Text = "5%"
        Me.RdFivePercent.UseVisualStyleBackColor = True
        '
        'chkVAT
        '
        Me.chkVAT.AutoSize = True
        Me.chkVAT.Checked = True
        Me.chkVAT.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVAT.Location = New System.Drawing.Point(733, 22)
        Me.chkVAT.Name = "chkVAT"
        Me.chkVAT.Size = New System.Drawing.Size(54, 20)
        Me.chkVAT.TabIndex = 1213
        Me.chkVAT.Text = "VAT"
        Me.chkVAT.UseVisualStyleBackColor = True
        '
        'btnSellUOM
        '
        Me.btnSellUOM.BackgroundImage = Global.jade.My.Resources.Resources.view
        Me.btnSellUOM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSellUOM.Location = New System.Drawing.Point(649, 68)
        Me.btnSellUOM.Name = "btnSellUOM"
        Me.btnSellUOM.Size = New System.Drawing.Size(25, 25)
        Me.btnSellUOM.TabIndex = 1212
        Me.btnSellUOM.UseVisualStyleBackColor = True
        '
        'dgvSell
        '
        Me.dgvSell.AllowUserToAddRows = False
        Me.dgvSell.AllowUserToDeleteRows = False
        Me.dgvSell.AllowUserToResizeColumns = False
        Me.dgvSell.AllowUserToResizeRows = False
        Me.dgvSell.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgvSell.BackgroundColor = System.Drawing.Color.White
        Me.dgvSell.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSell.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dcSell_Type, Me.dcSell_Price, Me.dcSell_UOM, Me.dcSell_UOMQTY, Me.dc_VAT, Me.dcSell_VAT})
        Me.dgvSell.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvSell.Location = New System.Drawing.Point(8, 21)
        Me.dgvSell.MultiSelect = False
        Me.dgvSell.Name = "dgvSell"
        Me.dgvSell.ReadOnly = True
        Me.dgvSell.RowHeadersVisible = False
        Me.dgvSell.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSell.Size = New System.Drawing.Size(499, 302)
        Me.dgvSell.TabIndex = 1158
        '
        'dcSell_Type
        '
        Me.dcSell_Type.HeaderText = "Type"
        Me.dcSell_Type.Name = "dcSell_Type"
        Me.dcSell_Type.ReadOnly = True
        Me.dcSell_Type.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dcSell_Type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.dcSell_Type.Width = 150
        '
        'dcSell_Price
        '
        Me.dcSell_Price.HeaderText = "Price"
        Me.dcSell_Price.Name = "dcSell_Price"
        Me.dcSell_Price.ReadOnly = True
        Me.dcSell_Price.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'dcSell_UOM
        '
        Me.dcSell_UOM.HeaderText = "UOM"
        Me.dcSell_UOM.Name = "dcSell_UOM"
        Me.dcSell_UOM.ReadOnly = True
        Me.dcSell_UOM.Width = 60
        '
        'dcSell_UOMQTY
        '
        Me.dcSell_UOMQTY.HeaderText = "QTY"
        Me.dcSell_UOMQTY.Name = "dcSell_UOMQTY"
        Me.dcSell_UOMQTY.ReadOnly = True
        Me.dcSell_UOMQTY.Width = 60
        '
        'dc_VAT
        '
        Me.dc_VAT.HeaderText = "VAT"
        Me.dc_VAT.Name = "dc_VAT"
        Me.dc_VAT.ReadOnly = True
        Me.dc_VAT.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dc_VAT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.dc_VAT.Width = 60
        '
        'dcSell_VAT
        '
        Me.dcSell_VAT.HeaderText = " VAT Inc"
        Me.dcSell_VAT.Name = "dcSell_VAT"
        Me.dcSell_VAT.ReadOnly = True
        Me.dcSell_VAT.Width = 60
        '
        'btnSellRemove
        '
        Me.btnSellRemove.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnSellRemove.Enabled = False
        Me.btnSellRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSellRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSellRemove.ForeColor = System.Drawing.Color.White
        Me.btnSellRemove.Location = New System.Drawing.Point(521, 158)
        Me.btnSellRemove.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSellRemove.Name = "btnSellRemove"
        Me.btnSellRemove.Size = New System.Drawing.Size(75, 24)
        Me.btnSellRemove.TabIndex = 1171
        Me.btnSellRemove.Text = "Remove"
        Me.btnSellRemove.UseVisualStyleBackColor = False
        '
        'cbSellType
        '
        Me.cbSellType.FormattingEnabled = True
        Me.cbSellType.Location = New System.Drawing.Point(574, 18)
        Me.cbSellType.Name = "cbSellType"
        Me.cbSellType.Size = New System.Drawing.Size(153, 24)
        Me.cbSellType.TabIndex = 1160
        '
        'btnSellAdd
        '
        Me.btnSellAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnSellAdd.Enabled = False
        Me.btnSellAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSellAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSellAdd.ForeColor = System.Drawing.Color.White
        Me.btnSellAdd.Location = New System.Drawing.Point(521, 133)
        Me.btnSellAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSellAdd.Name = "btnSellAdd"
        Me.btnSellAdd.Size = New System.Drawing.Size(75, 24)
        Me.btnSellAdd.TabIndex = 1170
        Me.btnSellAdd.Text = "Add"
        Me.btnSellAdd.UseVisualStyleBackColor = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(526, 21)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(46, 16)
        Me.Label20.TabIndex = 1161
        Me.Label20.Text = "Type :"
        '
        'txtSellPrice
        '
        Me.txtSellPrice.BackColor = System.Drawing.SystemColors.Window
        Me.txtSellPrice.ForeColor = System.Drawing.Color.Black
        Me.txtSellPrice.Location = New System.Drawing.Point(574, 44)
        Me.txtSellPrice.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSellPrice.Name = "txtSellPrice"
        Me.txtSellPrice.Size = New System.Drawing.Size(153, 22)
        Me.txtSellPrice.TabIndex = 1162
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(531, 94)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(42, 16)
        Me.Label15.TabIndex = 1167
        Me.Label15.Text = "QTY :"
        '
        'chkSellVAT
        '
        Me.chkSellVAT.AutoSize = True
        Me.chkSellVAT.Checked = True
        Me.chkSellVAT.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSellVAT.Location = New System.Drawing.Point(733, 44)
        Me.chkSellVAT.Name = "chkSellVAT"
        Me.chkSellVAT.Size = New System.Drawing.Size(77, 20)
        Me.chkSellVAT.TabIndex = 1168
        Me.chkSellVAT.Text = "VAT Inc."
        Me.chkSellVAT.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(528, 69)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(45, 16)
        Me.Label18.TabIndex = 1165
        Me.Label18.Text = "UOM :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(528, 47)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(45, 16)
        Me.Label16.TabIndex = 1163
        Me.Label16.Text = "Price :"
        '
        'txtSellUOM
        '
        Me.txtSellUOM.BackColor = System.Drawing.SystemColors.Window
        Me.txtSellUOM.ForeColor = System.Drawing.Color.Black
        Me.txtSellUOM.Location = New System.Drawing.Point(574, 69)
        Me.txtSellUOM.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSellUOM.Name = "txtSellUOM"
        Me.txtSellUOM.Size = New System.Drawing.Size(68, 22)
        Me.txtSellUOM.TabIndex = 1164
        '
        'txtSellQTY
        '
        Me.txtSellQTY.BackColor = System.Drawing.SystemColors.Window
        Me.txtSellQTY.ForeColor = System.Drawing.Color.Black
        Me.txtSellQTY.Location = New System.Drawing.Point(574, 94)
        Me.txtSellQTY.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSellQTY.Name = "txtSellQTY"
        Me.txtSellQTY.Size = New System.Drawing.Size(68, 22)
        Me.txtSellQTY.TabIndex = 1166
        Me.txtSellQTY.Text = "1"
        '
        'tpID
        '
        Me.tpID.Controls.Add(Me.cbDept)
        Me.tpID.Controls.Add(Me.Label27)
        Me.tpID.Controls.Add(Me.Label30)
        Me.tpID.Controls.Add(Me.txtInvStandard)
        Me.tpID.Controls.Add(Me.txtInvUOM)
        Me.tpID.Controls.Add(Me.Label8)
        Me.tpID.Controls.Add(Me.cbInvWarehouse)
        Me.tpID.Controls.Add(Me.Label12)
        Me.tpID.Controls.Add(Me.txtInvMax)
        Me.tpID.Controls.Add(Me.Label7)
        Me.tpID.Controls.Add(Me.txtInvMin)
        Me.tpID.Controls.Add(Me.cbInvMethod)
        Me.tpID.Controls.Add(Me.Label6)
        Me.tpID.Controls.Add(Me.Label5)
        Me.tpID.Controls.Add(Me.txtInvAvailable)
        Me.tpID.Controls.Add(Me.txtInvCommit)
        Me.tpID.Controls.Add(Me.txtInvOrder)
        Me.tpID.Controls.Add(Me.txtInvStock)
        Me.tpID.Controls.Add(Me.GroupBox1)
        Me.tpID.Controls.Add(Me.dgvInv)
        Me.tpID.Controls.Add(Me.btnRefresh)
        Me.tpID.Controls.Add(Me.btnInvUOM)
        Me.tpID.Location = New System.Drawing.Point(4, 25)
        Me.tpID.Name = "tpID"
        Me.tpID.Size = New System.Drawing.Size(1230, 412)
        Me.tpID.TabIndex = 2
        Me.tpID.Text = "Inventory Data"
        Me.tpID.UseVisualStyleBackColor = True
        '
        'cbDept
        '
        Me.cbDept.FormattingEnabled = True
        Me.cbDept.Location = New System.Drawing.Point(147, 120)
        Me.cbDept.Name = "cbDept"
        Me.cbDept.Size = New System.Drawing.Size(278, 24)
        Me.cbDept.TabIndex = 1216
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(56, 123)
        Me.Label27.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(84, 16)
        Me.Label27.TabIndex = 1215
        Me.Label27.Text = "Department :"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(47, 96)
        Me.Label30.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(99, 16)
        Me.Label30.TabIndex = 1213
        Me.Label30.Text = "Standard Cost :"
        '
        'txtInvStandard
        '
        Me.txtInvStandard.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvStandard.ForeColor = System.Drawing.Color.Black
        Me.txtInvStandard.Location = New System.Drawing.Point(146, 93)
        Me.txtInvStandard.Margin = New System.Windows.Forms.Padding(4)
        Me.txtInvStandard.Name = "txtInvStandard"
        Me.txtInvStandard.Size = New System.Drawing.Size(279, 22)
        Me.txtInvStandard.TabIndex = 1212
        '
        'txtInvUOM
        '
        Me.txtInvUOM.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvUOM.Enabled = False
        Me.txtInvUOM.ForeColor = System.Drawing.Color.Black
        Me.txtInvUOM.Location = New System.Drawing.Point(146, 13)
        Me.txtInvUOM.Margin = New System.Windows.Forms.Padding(4)
        Me.txtInvUOM.Name = "txtInvUOM"
        Me.txtInvUOM.Size = New System.Drawing.Size(279, 22)
        Me.txtInvUOM.TabIndex = 1187
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(482, 39)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 16)
        Me.Label8.TabIndex = 1182
        Me.Label8.Text = "Maximum :"
        '
        'cbInvWarehouse
        '
        Me.cbInvWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbInvWarehouse.DropDownWidth = 400
        Me.cbInvWarehouse.FormattingEnabled = True
        Me.cbInvWarehouse.Location = New System.Drawing.Point(146, 65)
        Me.cbInvWarehouse.Name = "cbInvWarehouse"
        Me.cbInvWarehouse.Size = New System.Drawing.Size(279, 24)
        Me.cbInvWarehouse.TabIndex = 1210
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(17, 69)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(129, 16)
        Me.Label12.TabIndex = 1209
        Me.Label12.Text = "Default Warehouse :"
        '
        'txtInvMax
        '
        Me.txtInvMax.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvMax.ForeColor = System.Drawing.Color.Black
        Me.txtInvMax.Location = New System.Drawing.Point(559, 36)
        Me.txtInvMax.Margin = New System.Windows.Forms.Padding(4)
        Me.txtInvMax.Name = "txtInvMax"
        Me.txtInvMax.Size = New System.Drawing.Size(179, 22)
        Me.txtInvMax.TabIndex = 1181
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(486, 15)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 16)
        Me.Label7.TabIndex = 1180
        Me.Label7.Text = "Minimum :"
        '
        'txtInvMin
        '
        Me.txtInvMin.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvMin.ForeColor = System.Drawing.Color.Black
        Me.txtInvMin.Location = New System.Drawing.Point(559, 12)
        Me.txtInvMin.Margin = New System.Windows.Forms.Padding(4)
        Me.txtInvMin.Name = "txtInvMin"
        Me.txtInvMin.Size = New System.Drawing.Size(179, 22)
        Me.txtInvMin.TabIndex = 1179
        '
        'cbInvMethod
        '
        Me.cbInvMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbInvMethod.FormattingEnabled = True
        Me.cbInvMethod.Items.AddRange(New Object() {"Weighted Average Unit Cost"})
        Me.cbInvMethod.Location = New System.Drawing.Point(146, 38)
        Me.cbInvMethod.Name = "cbInvMethod"
        Me.cbInvMethod.Size = New System.Drawing.Size(279, 24)
        Me.cbInvMethod.TabIndex = 1178
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(33, 41)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 16)
        Me.Label6.TabIndex = 1177
        Me.Label6.Text = "Costing Method :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(33, 16)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 16)
        Me.Label5.TabIndex = 1175
        Me.Label5.Text = "Inventory UoM"
        '
        'txtInvAvailable
        '
        Me.txtInvAvailable.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInvAvailable.Location = New System.Drawing.Point(859, 384)
        Me.txtInvAvailable.Name = "txtInvAvailable"
        Me.txtInvAvailable.ReadOnly = True
        Me.txtInvAvailable.Size = New System.Drawing.Size(103, 22)
        Me.txtInvAvailable.TabIndex = 4
        Me.txtInvAvailable.TabStop = False
        '
        'txtInvCommit
        '
        Me.txtInvCommit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInvCommit.Location = New System.Drawing.Point(759, 384)
        Me.txtInvCommit.Name = "txtInvCommit"
        Me.txtInvCommit.ReadOnly = True
        Me.txtInvCommit.Size = New System.Drawing.Size(101, 22)
        Me.txtInvCommit.TabIndex = 3
        Me.txtInvCommit.TabStop = False
        '
        'txtInvOrder
        '
        Me.txtInvOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInvOrder.Location = New System.Drawing.Point(659, 384)
        Me.txtInvOrder.Name = "txtInvOrder"
        Me.txtInvOrder.ReadOnly = True
        Me.txtInvOrder.Size = New System.Drawing.Size(101, 22)
        Me.txtInvOrder.TabIndex = 2
        Me.txtInvOrder.TabStop = False
        '
        'txtInvStock
        '
        Me.txtInvStock.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInvStock.Location = New System.Drawing.Point(559, 384)
        Me.txtInvStock.Name = "txtInvStock"
        Me.txtInvStock.ReadOnly = True
        Me.txtInvStock.Size = New System.Drawing.Size(101, 22)
        Me.txtInvStock.TabIndex = 1
        Me.txtInvStock.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbIDTolling)
        Me.GroupBox1.Controls.Add(Me.rbIDTolling)
        Me.GroupBox1.Controls.Add(Me.rbIDOwned)
        Me.GroupBox1.Location = New System.Drawing.Point(983, 304)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(244, 102)
        Me.GroupBox1.TabIndex = 1214
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Stock"
        Me.GroupBox1.Visible = False
        '
        'cbIDTolling
        '
        Me.cbIDTolling.FormattingEnabled = True
        Me.cbIDTolling.Location = New System.Drawing.Point(17, 66)
        Me.cbIDTolling.Name = "cbIDTolling"
        Me.cbIDTolling.Size = New System.Drawing.Size(212, 24)
        Me.cbIDTolling.TabIndex = 1175
        '
        'rbIDTolling
        '
        Me.rbIDTolling.AutoSize = True
        Me.rbIDTolling.Location = New System.Drawing.Point(17, 45)
        Me.rbIDTolling.Name = "rbIDTolling"
        Me.rbIDTolling.Size = New System.Drawing.Size(140, 20)
        Me.rbIDTolling.TabIndex = 1
        Me.rbIDTolling.Text = "Customer Supplied"
        Me.rbIDTolling.UseVisualStyleBackColor = True
        '
        'rbIDOwned
        '
        Me.rbIDOwned.AutoSize = True
        Me.rbIDOwned.Checked = True
        Me.rbIDOwned.Location = New System.Drawing.Point(17, 21)
        Me.rbIDOwned.Name = "rbIDOwned"
        Me.rbIDOwned.Size = New System.Drawing.Size(68, 20)
        Me.rbIDOwned.TabIndex = 0
        Me.rbIDOwned.TabStop = True
        Me.rbIDOwned.Text = "Owned"
        Me.rbIDOwned.UseVisualStyleBackColor = True
        '
        'dgvInv
        '
        Me.dgvInv.AllowUserToAddRows = False
        Me.dgvInv.AllowUserToDeleteRows = False
        Me.dgvInv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvInv.BackgroundColor = System.Drawing.Color.White
        Me.dgvInv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chInvWHSECode, Me.chInvWHSEName, Me.chInvStock, Me.chInvOrdered, Me.chInvCommit, Me.chInvAvail})
        Me.dgvInv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvInv.Location = New System.Drawing.Point(12, 157)
        Me.dgvInv.Name = "dgvInv"
        Me.dgvInv.ReadOnly = True
        Me.dgvInv.RowHeadersWidth = 25
        Me.dgvInv.Size = New System.Drawing.Size(950, 227)
        Me.dgvInv.TabIndex = 0
        '
        'chInvWHSECode
        '
        Me.chInvWHSECode.HeaderText = "Code"
        Me.chInvWHSECode.Name = "chInvWHSECode"
        Me.chInvWHSECode.ReadOnly = True
        '
        'chInvWHSEName
        '
        Me.chInvWHSEName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.chInvWHSEName.HeaderText = "Warehouse Name"
        Me.chInvWHSEName.Name = "chInvWHSEName"
        Me.chInvWHSEName.ReadOnly = True
        '
        'chInvStock
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.chInvStock.DefaultCellStyle = DataGridViewCellStyle1
        Me.chInvStock.HeaderText = "In Stock"
        Me.chInvStock.Name = "chInvStock"
        Me.chInvStock.ReadOnly = True
        '
        'chInvOrdered
        '
        Me.chInvOrdered.HeaderText = "Ordered"
        Me.chInvOrdered.Name = "chInvOrdered"
        Me.chInvOrdered.ReadOnly = True
        '
        'chInvCommit
        '
        Me.chInvCommit.HeaderText = "Committed"
        Me.chInvCommit.Name = "chInvCommit"
        Me.chInvCommit.ReadOnly = True
        '
        'chInvAvail
        '
        Me.chInvAvail.HeaderText = "Available"
        Me.chInvAvail.Name = "chInvAvail"
        Me.chInvAvail.ReadOnly = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackgroundImage = Global.jade.My.Resources.Resources.view
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefresh.Location = New System.Drawing.Point(535, 384)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(25, 23)
        Me.btnRefresh.TabIndex = 1211
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnInvUOM
        '
        Me.btnInvUOM.BackgroundImage = Global.jade.My.Resources.Resources.view
        Me.btnInvUOM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnInvUOM.Location = New System.Drawing.Point(426, 12)
        Me.btnInvUOM.Name = "btnInvUOM"
        Me.btnInvUOM.Size = New System.Drawing.Size(25, 25)
        Me.btnInvUOM.TabIndex = 1188
        Me.btnInvUOM.UseVisualStyleBackColor = True
        '
        'tpDefaultEntry
        '
        Me.tpDefaultEntry.BackColor = System.Drawing.Color.White
        Me.tpDefaultEntry.Controls.Add(Me.txtConAccntTitle)
        Me.tpDefaultEntry.Controls.Add(Me.txtConAccntCode)
        Me.tpDefaultEntry.Controls.Add(Me.Label39)
        Me.tpDefaultEntry.Controls.Add(Me.txtFixedAssetAccountTitle)
        Me.tpDefaultEntry.Controls.Add(Me.txtFixedAssetAccountCode)
        Me.tpDefaultEntry.Controls.Add(Me.Label36)
        Me.tpDefaultEntry.Controls.Add(Me.txtDepExpTitle)
        Me.tpDefaultEntry.Controls.Add(Me.txtDepExpCode)
        Me.tpDefaultEntry.Controls.Add(Me.Label34)
        Me.tpDefaultEntry.Controls.Add(Me.txtAccumDepTitle)
        Me.tpDefaultEntry.Controls.Add(Me.txtAccumDepCode)
        Me.tpDefaultEntry.Controls.Add(Me.Label35)
        Me.tpDefaultEntry.Controls.Add(Me.btnSalesAccnt)
        Me.tpDefaultEntry.Controls.Add(Me.txtExpAccntTitle)
        Me.tpDefaultEntry.Controls.Add(Me.txtExpAccntCode)
        Me.tpDefaultEntry.Controls.Add(Me.Label33)
        Me.tpDefaultEntry.Controls.Add(Me.txtDiscAccntTitle)
        Me.tpDefaultEntry.Controls.Add(Me.txtDiscAccntCode)
        Me.tpDefaultEntry.Controls.Add(Me.Label29)
        Me.tpDefaultEntry.Controls.Add(Me.txtInvAccntTitle)
        Me.tpDefaultEntry.Controls.Add(Me.txtInvAccntCode)
        Me.tpDefaultEntry.Controls.Add(Me.Label19)
        Me.tpDefaultEntry.Controls.Add(Me.txtCostAccntTitle)
        Me.tpDefaultEntry.Controls.Add(Me.txtCostAccntCode)
        Me.tpDefaultEntry.Controls.Add(Me.Label9)
        Me.tpDefaultEntry.Controls.Add(Me.txtSaleAccntTitle)
        Me.tpDefaultEntry.Controls.Add(Me.txtSaleAccntCode)
        Me.tpDefaultEntry.Controls.Add(Me.Label14)
        Me.tpDefaultEntry.Location = New System.Drawing.Point(4, 25)
        Me.tpDefaultEntry.Name = "tpDefaultEntry"
        Me.tpDefaultEntry.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDefaultEntry.Size = New System.Drawing.Size(1230, 412)
        Me.tpDefaultEntry.TabIndex = 5
        Me.tpDefaultEntry.Text = "Default Accounting Entry"
        '
        'txtConAccntTitle
        '
        Me.txtConAccntTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtConAccntTitle.ForeColor = System.Drawing.Color.Black
        Me.txtConAccntTitle.Location = New System.Drawing.Point(352, 249)
        Me.txtConAccntTitle.Margin = New System.Windows.Forms.Padding(4)
        Me.txtConAccntTitle.Name = "txtConAccntTitle"
        Me.txtConAccntTitle.Size = New System.Drawing.Size(305, 22)
        Me.txtConAccntTitle.TabIndex = 1243
        '
        'txtConAccntCode
        '
        Me.txtConAccntCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtConAccntCode.Enabled = False
        Me.txtConAccntCode.ForeColor = System.Drawing.Color.Black
        Me.txtConAccntCode.Location = New System.Drawing.Point(240, 249)
        Me.txtConAccntCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtConAccntCode.Name = "txtConAccntCode"
        Me.txtConAccntCode.Size = New System.Drawing.Size(111, 22)
        Me.txtConAccntCode.TabIndex = 1244
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.ForeColor = System.Drawing.Color.Black
        Me.Label39.Location = New System.Drawing.Point(45, 253)
        Me.Label39.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(191, 16)
        Me.Label39.TabIndex = 1242
        Me.Label39.Text = "Default Consignment Account : "
        '
        'txtFixedAssetAccountTitle
        '
        Me.txtFixedAssetAccountTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtFixedAssetAccountTitle.ForeColor = System.Drawing.Color.Black
        Me.txtFixedAssetAccountTitle.Location = New System.Drawing.Point(352, 169)
        Me.txtFixedAssetAccountTitle.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFixedAssetAccountTitle.Name = "txtFixedAssetAccountTitle"
        Me.txtFixedAssetAccountTitle.Size = New System.Drawing.Size(305, 22)
        Me.txtFixedAssetAccountTitle.TabIndex = 1237
        '
        'txtFixedAssetAccountCode
        '
        Me.txtFixedAssetAccountCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtFixedAssetAccountCode.Enabled = False
        Me.txtFixedAssetAccountCode.ForeColor = System.Drawing.Color.Black
        Me.txtFixedAssetAccountCode.Location = New System.Drawing.Point(240, 169)
        Me.txtFixedAssetAccountCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFixedAssetAccountCode.Name = "txtFixedAssetAccountCode"
        Me.txtFixedAssetAccountCode.Size = New System.Drawing.Size(111, 22)
        Me.txtFixedAssetAccountCode.TabIndex = 1238
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.ForeColor = System.Drawing.Color.Black
        Me.Label36.Location = New System.Drawing.Point(101, 173)
        Me.Label36.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(135, 16)
        Me.Label36.TabIndex = 1236
        Me.Label36.Text = "Fixed Asset Account :"
        '
        'txtDepExpTitle
        '
        Me.txtDepExpTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtDepExpTitle.ForeColor = System.Drawing.Color.Black
        Me.txtDepExpTitle.Location = New System.Drawing.Point(352, 222)
        Me.txtDepExpTitle.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDepExpTitle.Name = "txtDepExpTitle"
        Me.txtDepExpTitle.Size = New System.Drawing.Size(305, 22)
        Me.txtDepExpTitle.TabIndex = 1234
        '
        'txtDepExpCode
        '
        Me.txtDepExpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtDepExpCode.Enabled = False
        Me.txtDepExpCode.ForeColor = System.Drawing.Color.Black
        Me.txtDepExpCode.Location = New System.Drawing.Point(240, 222)
        Me.txtDepExpCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDepExpCode.Name = "txtDepExpCode"
        Me.txtDepExpCode.Size = New System.Drawing.Size(111, 22)
        Me.txtDepExpCode.TabIndex = 1235
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.ForeColor = System.Drawing.Color.Black
        Me.Label34.Location = New System.Drawing.Point(26, 226)
        Me.Label34.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(210, 16)
        Me.Label34.TabIndex = 1233
        Me.Label34.Text = "Default Depre. Expense Account : "
        '
        'txtAccumDepTitle
        '
        Me.txtAccumDepTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtAccumDepTitle.ForeColor = System.Drawing.Color.Black
        Me.txtAccumDepTitle.Location = New System.Drawing.Point(352, 195)
        Me.txtAccumDepTitle.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAccumDepTitle.Name = "txtAccumDepTitle"
        Me.txtAccumDepTitle.Size = New System.Drawing.Size(305, 22)
        Me.txtAccumDepTitle.TabIndex = 1231
        '
        'txtAccumDepCode
        '
        Me.txtAccumDepCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtAccumDepCode.Enabled = False
        Me.txtAccumDepCode.ForeColor = System.Drawing.Color.Black
        Me.txtAccumDepCode.Location = New System.Drawing.Point(240, 195)
        Me.txtAccumDepCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAccumDepCode.Name = "txtAccumDepCode"
        Me.txtAccumDepCode.Size = New System.Drawing.Size(111, 22)
        Me.txtAccumDepCode.TabIndex = 1232
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.ForeColor = System.Drawing.Color.Black
        Me.Label35.Location = New System.Drawing.Point(38, 199)
        Me.Label35.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(198, 16)
        Me.Label35.TabIndex = 1230
        Me.Label35.Text = "Default Accum. Depre. Account :"
        '
        'btnSalesAccnt
        '
        Me.btnSalesAccnt.Location = New System.Drawing.Point(664, 88)
        Me.btnSalesAccnt.Name = "btnSalesAccnt"
        Me.btnSalesAccnt.Size = New System.Drawing.Size(240, 23)
        Me.btnSalesAccnt.TabIndex = 1229
        Me.btnSalesAccnt.Text = "Add Another Sales Account"
        Me.btnSalesAccnt.UseVisualStyleBackColor = True
        Me.btnSalesAccnt.Visible = False
        '
        'txtExpAccntTitle
        '
        Me.txtExpAccntTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtExpAccntTitle.ForeColor = System.Drawing.Color.Black
        Me.txtExpAccntTitle.Location = New System.Drawing.Point(352, 143)
        Me.txtExpAccntTitle.Margin = New System.Windows.Forms.Padding(4)
        Me.txtExpAccntTitle.Name = "txtExpAccntTitle"
        Me.txtExpAccntTitle.Size = New System.Drawing.Size(305, 22)
        Me.txtExpAccntTitle.TabIndex = 1227
        '
        'txtExpAccntCode
        '
        Me.txtExpAccntCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtExpAccntCode.Enabled = False
        Me.txtExpAccntCode.ForeColor = System.Drawing.Color.Black
        Me.txtExpAccntCode.Location = New System.Drawing.Point(240, 143)
        Me.txtExpAccntCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtExpAccntCode.Name = "txtExpAccntCode"
        Me.txtExpAccntCode.Size = New System.Drawing.Size(111, 22)
        Me.txtExpAccntCode.TabIndex = 1228
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.ForeColor = System.Drawing.Color.Black
        Me.Label33.Location = New System.Drawing.Point(70, 147)
        Me.Label33.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(166, 16)
        Me.Label33.TabIndex = 1226
        Me.Label33.Text = "Default Expense Account : "
        '
        'txtDiscAccntTitle
        '
        Me.txtDiscAccntTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtDiscAccntTitle.ForeColor = System.Drawing.Color.Black
        Me.txtDiscAccntTitle.Location = New System.Drawing.Point(352, 89)
        Me.txtDiscAccntTitle.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDiscAccntTitle.Name = "txtDiscAccntTitle"
        Me.txtDiscAccntTitle.Size = New System.Drawing.Size(305, 22)
        Me.txtDiscAccntTitle.TabIndex = 1224
        '
        'txtDiscAccntCode
        '
        Me.txtDiscAccntCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtDiscAccntCode.Enabled = False
        Me.txtDiscAccntCode.ForeColor = System.Drawing.Color.Black
        Me.txtDiscAccntCode.Location = New System.Drawing.Point(240, 89)
        Me.txtDiscAccntCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDiscAccntCode.Name = "txtDiscAccntCode"
        Me.txtDiscAccntCode.Size = New System.Drawing.Size(111, 22)
        Me.txtDiscAccntCode.TabIndex = 1225
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.ForeColor = System.Drawing.Color.Black
        Me.Label29.Location = New System.Drawing.Point(87, 93)
        Me.Label29.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(149, 16)
        Me.Label29.TabIndex = 1223
        Me.Label29.Text = "Default Sales Discount :"
        '
        'txtInvAccntTitle
        '
        Me.txtInvAccntTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvAccntTitle.ForeColor = System.Drawing.Color.Black
        Me.txtInvAccntTitle.Location = New System.Drawing.Point(352, 116)
        Me.txtInvAccntTitle.Margin = New System.Windows.Forms.Padding(4)
        Me.txtInvAccntTitle.Name = "txtInvAccntTitle"
        Me.txtInvAccntTitle.Size = New System.Drawing.Size(305, 22)
        Me.txtInvAccntTitle.TabIndex = 1221
        '
        'txtInvAccntCode
        '
        Me.txtInvAccntCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvAccntCode.Enabled = False
        Me.txtInvAccntCode.ForeColor = System.Drawing.Color.Black
        Me.txtInvAccntCode.Location = New System.Drawing.Point(240, 116)
        Me.txtInvAccntCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtInvAccntCode.Name = "txtInvAccntCode"
        Me.txtInvAccntCode.Size = New System.Drawing.Size(111, 22)
        Me.txtInvAccntCode.TabIndex = 1222
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(72, 120)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(164, 16)
        Me.Label19.TabIndex = 1220
        Me.Label19.Text = "Default Inventory Account :"
        '
        'txtCostAccntTitle
        '
        Me.txtCostAccntTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtCostAccntTitle.ForeColor = System.Drawing.Color.Black
        Me.txtCostAccntTitle.Location = New System.Drawing.Point(352, 63)
        Me.txtCostAccntTitle.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCostAccntTitle.Name = "txtCostAccntTitle"
        Me.txtCostAccntTitle.Size = New System.Drawing.Size(305, 22)
        Me.txtCostAccntTitle.TabIndex = 1218
        '
        'txtCostAccntCode
        '
        Me.txtCostAccntCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCostAccntCode.Enabled = False
        Me.txtCostAccntCode.ForeColor = System.Drawing.Color.Black
        Me.txtCostAccntCode.Location = New System.Drawing.Point(240, 63)
        Me.txtCostAccntCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCostAccntCode.Name = "txtCostAccntCode"
        Me.txtCostAccntCode.Size = New System.Drawing.Size(111, 22)
        Me.txtCostAccntCode.TabIndex = 1219
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(91, 40)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(145, 16)
        Me.Label9.TabIndex = 1217
        Me.Label9.Text = "Default Sales Account :"
        '
        'txtSaleAccntTitle
        '
        Me.txtSaleAccntTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaleAccntTitle.ForeColor = System.Drawing.Color.Black
        Me.txtSaleAccntTitle.Location = New System.Drawing.Point(352, 37)
        Me.txtSaleAccntTitle.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSaleAccntTitle.Name = "txtSaleAccntTitle"
        Me.txtSaleAccntTitle.Size = New System.Drawing.Size(305, 22)
        Me.txtSaleAccntTitle.TabIndex = 1215
        '
        'txtSaleAccntCode
        '
        Me.txtSaleAccntCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaleAccntCode.Enabled = False
        Me.txtSaleAccntCode.ForeColor = System.Drawing.Color.Black
        Me.txtSaleAccntCode.Location = New System.Drawing.Point(240, 37)
        Me.txtSaleAccntCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSaleAccntCode.Name = "txtSaleAccntCode"
        Me.txtSaleAccntCode.Size = New System.Drawing.Size(111, 22)
        Me.txtSaleAccntCode.TabIndex = 1216
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(47, 66)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(189, 16)
        Me.Label14.TabIndex = 1214
        Me.Label14.Text = "Default Cost of Sales Account :"
        '
        'tpMD
        '
        Me.tpMD.Controls.Add(Me.Label22)
        Me.tpMD.Controls.Add(Me.cbMDprodFloor)
        Me.tpMD.Controls.Add(Me.Label21)
        Me.tpMD.Controls.Add(Me.cbProd)
        Me.tpMD.Location = New System.Drawing.Point(4, 25)
        Me.tpMD.Name = "tpMD"
        Me.tpMD.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMD.Size = New System.Drawing.Size(1230, 412)
        Me.tpMD.TabIndex = 7
        Me.tpMD.Text = "Production Data"
        Me.tpMD.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(28, 45)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(125, 16)
        Me.Label22.TabIndex = 1221
        Me.Label22.Text = "Default Shop Floor :"
        '
        'cbMDprodFloor
        '
        Me.cbMDprodFloor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMDprodFloor.FormattingEnabled = True
        Me.cbMDprodFloor.Items.AddRange(New Object() {"Semi-Finished Goods", "Finished Goods"})
        Me.cbMDprodFloor.Location = New System.Drawing.Point(155, 42)
        Me.cbMDprodFloor.Name = "cbMDprodFloor"
        Me.cbMDprodFloor.Size = New System.Drawing.Size(214, 24)
        Me.cbMDprodFloor.TabIndex = 1220
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(107, 20)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(46, 16)
        Me.Label21.TabIndex = 1219
        Me.Label21.Text = "Type :"
        '
        'cbProd
        '
        Me.cbProd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbProd.FormattingEnabled = True
        Me.cbProd.Items.AddRange(New Object() {"Semi-Finished Goods", "Finished Goods"})
        Me.cbProd.Location = New System.Drawing.Point(155, 16)
        Me.cbProd.Name = "cbProd"
        Me.cbProd.Size = New System.Drawing.Size(214, 24)
        Me.cbProd.TabIndex = 1218
        '
        'tpBSCharges
        '
        Me.tpBSCharges.Controls.Add(Me.dgvItemList)
        Me.tpBSCharges.Location = New System.Drawing.Point(4, 25)
        Me.tpBSCharges.Name = "tpBSCharges"
        Me.tpBSCharges.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBSCharges.Size = New System.Drawing.Size(1230, 412)
        Me.tpBSCharges.TabIndex = 8
        Me.tpBSCharges.Text = "Default Charges"
        Me.tpBSCharges.UseVisualStyleBackColor = True
        '
        'dgvItemList
        '
        Me.dgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvItemList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chBS_RecordID, Me.chBS_Desc, Me.chBS_VCECode, Me.chBS_VCEName, Me.chBS_Type, Me.chBS_Amount})
        Me.dgvItemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvItemList.Location = New System.Drawing.Point(3, 3)
        Me.dgvItemList.Name = "dgvItemList"
        Me.dgvItemList.RowHeadersWidth = 25
        Me.dgvItemList.Size = New System.Drawing.Size(1224, 406)
        Me.dgvItemList.TabIndex = 1312
        '
        'chBS_RecordID
        '
        Me.chBS_RecordID.HeaderText = "RecordID"
        Me.chBS_RecordID.Name = "chBS_RecordID"
        Me.chBS_RecordID.Visible = False
        '
        'chBS_Desc
        '
        Me.chBS_Desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.chBS_Desc.HeaderText = "Description"
        Me.chBS_Desc.Name = "chBS_Desc"
        Me.chBS_Desc.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'chBS_VCECode
        '
        Me.chBS_VCECode.HeaderText = "VCE Code"
        Me.chBS_VCECode.Name = "chBS_VCECode"
        Me.chBS_VCECode.Visible = False
        '
        'chBS_VCEName
        '
        Me.chBS_VCEName.HeaderText = "VCEName"
        Me.chBS_VCEName.Name = "chBS_VCEName"
        Me.chBS_VCEName.Width = 200
        '
        'chBS_Type
        '
        Me.chBS_Type.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.chBS_Type.HeaderText = "Type"
        Me.chBS_Type.Name = "chBS_Type"
        '
        'chBS_Amount
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomRight
        Me.chBS_Amount.DefaultCellStyle = DataGridViewCellStyle2
        Me.chBS_Amount.HeaderText = "Amount"
        Me.chBS_Amount.Name = "chBS_Amount"
        Me.chBS_Amount.Width = 150
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.grpSizeColor)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1230, 412)
        Me.TabPage1.TabIndex = 10
        Me.TabPage1.Text = "Size And Color"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'grpSizeColor
        '
        Me.grpSizeColor.Controls.Add(Me.txtSize)
        Me.grpSizeColor.Controls.Add(Me.dgvSizeColor)
        Me.grpSizeColor.Controls.Add(Me.txtColor)
        Me.grpSizeColor.Controls.Add(Me.btnSizeColor_Remove)
        Me.grpSizeColor.Controls.Add(Me.Label42)
        Me.grpSizeColor.Controls.Add(Me.btnSizeColor_Add)
        Me.grpSizeColor.Controls.Add(Me.Label41)
        Me.grpSizeColor.Location = New System.Drawing.Point(6, 6)
        Me.grpSizeColor.Name = "grpSizeColor"
        Me.grpSizeColor.Size = New System.Drawing.Size(1218, 400)
        Me.grpSizeColor.TabIndex = 0
        Me.grpSizeColor.TabStop = False
        '
        'txtSize
        '
        Me.txtSize.BackColor = System.Drawing.SystemColors.Window
        Me.txtSize.ForeColor = System.Drawing.Color.Black
        Me.txtSize.Location = New System.Drawing.Point(566, 22)
        Me.txtSize.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSize.Name = "txtSize"
        Me.txtSize.Size = New System.Drawing.Size(153, 22)
        Me.txtSize.TabIndex = 1174
        '
        'dgvSizeColor
        '
        Me.dgvSizeColor.AllowUserToAddRows = False
        Me.dgvSizeColor.AllowUserToDeleteRows = False
        Me.dgvSizeColor.AllowUserToResizeColumns = False
        Me.dgvSizeColor.AllowUserToResizeRows = False
        Me.dgvSizeColor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgvSizeColor.BackgroundColor = System.Drawing.Color.White
        Me.dgvSizeColor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSizeColor.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chSize, Me.chColor})
        Me.dgvSizeColor.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvSizeColor.Location = New System.Drawing.Point(10, 13)
        Me.dgvSizeColor.MultiSelect = False
        Me.dgvSizeColor.Name = "dgvSizeColor"
        Me.dgvSizeColor.ReadOnly = True
        Me.dgvSizeColor.RowHeadersVisible = False
        Me.dgvSizeColor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSizeColor.Size = New System.Drawing.Size(499, 365)
        Me.dgvSizeColor.TabIndex = 1172
        '
        'chSize
        '
        Me.chSize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.chSize.HeaderText = "Size"
        Me.chSize.Name = "chSize"
        Me.chSize.ReadOnly = True
        Me.chSize.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chSize.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'chColor
        '
        Me.chColor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.chColor.HeaderText = "Color"
        Me.chColor.Name = "chColor"
        Me.chColor.ReadOnly = True
        Me.chColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'txtColor
        '
        Me.txtColor.BackColor = System.Drawing.SystemColors.Window
        Me.txtColor.ForeColor = System.Drawing.Color.Black
        Me.txtColor.Location = New System.Drawing.Point(566, 47)
        Me.txtColor.Margin = New System.Windows.Forms.Padding(4)
        Me.txtColor.Name = "txtColor"
        Me.txtColor.Size = New System.Drawing.Size(153, 22)
        Me.txtColor.TabIndex = 1176
        '
        'btnSizeColor_Remove
        '
        Me.btnSizeColor_Remove.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnSizeColor_Remove.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSizeColor_Remove.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSizeColor_Remove.ForeColor = System.Drawing.Color.White
        Me.btnSizeColor_Remove.Location = New System.Drawing.Point(566, 102)
        Me.btnSizeColor_Remove.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSizeColor_Remove.Name = "btnSizeColor_Remove"
        Me.btnSizeColor_Remove.Size = New System.Drawing.Size(75, 24)
        Me.btnSizeColor_Remove.TabIndex = 1181
        Me.btnSizeColor_Remove.Text = "Remove"
        Me.btnSizeColor_Remove.UseVisualStyleBackColor = False
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.ForeColor = System.Drawing.Color.Black
        Me.Label42.Location = New System.Drawing.Point(520, 25)
        Me.Label42.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(40, 16)
        Me.Label42.TabIndex = 1175
        Me.Label42.Text = "Size :"
        '
        'btnSizeColor_Add
        '
        Me.btnSizeColor_Add.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnSizeColor_Add.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSizeColor_Add.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSizeColor_Add.ForeColor = System.Drawing.Color.White
        Me.btnSizeColor_Add.Location = New System.Drawing.Point(566, 77)
        Me.btnSizeColor_Add.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSizeColor_Add.Name = "btnSizeColor_Add"
        Me.btnSizeColor_Add.Size = New System.Drawing.Size(75, 24)
        Me.btnSizeColor_Add.TabIndex = 1180
        Me.btnSizeColor_Add.Text = "Add"
        Me.btnSizeColor_Add.UseVisualStyleBackColor = False
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.ForeColor = System.Drawing.Color.Black
        Me.Label41.Location = New System.Drawing.Point(520, 47)
        Me.Label41.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(46, 16)
        Me.Label41.TabIndex = 1177
        Me.Label41.Text = "Color :"
        '
        'chkVATable
        '
        Me.chkVATable.AutoSize = True
        Me.chkVATable.Checked = True
        Me.chkVATable.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVATable.Location = New System.Drawing.Point(451, 138)
        Me.chkVATable.Name = "chkVATable"
        Me.chkVATable.Size = New System.Drawing.Size(81, 20)
        Me.chkVATable.TabIndex = 1208
        Me.chkVATable.Text = "VATable"
        Me.chkVATable.UseVisualStyleBackColor = True
        '
        'chkInv
        '
        Me.chkInv.AutoSize = True
        Me.chkInv.Location = New System.Drawing.Point(600, 59)
        Me.chkInv.Name = "chkInv"
        Me.chkInv.Size = New System.Drawing.Size(109, 20)
        Me.chkInv.TabIndex = 1168
        Me.chkInv.Text = "Inventory Item"
        Me.chkInv.UseVisualStyleBackColor = True
        '
        'chkSale
        '
        Me.chkSale.AutoSize = True
        Me.chkSale.Location = New System.Drawing.Point(600, 79)
        Me.chkSale.Name = "chkSale"
        Me.chkSale.Size = New System.Drawing.Size(90, 20)
        Me.chkSale.TabIndex = 1169
        Me.chkSale.Text = "Sales Item"
        Me.chkSale.UseVisualStyleBackColor = True
        '
        'chkPurch
        '
        Me.chkPurch.AutoSize = True
        Me.chkPurch.Location = New System.Drawing.Point(600, 99)
        Me.chkPurch.Name = "chkPurch"
        Me.chkPurch.Size = New System.Drawing.Size(112, 20)
        Me.chkPurch.TabIndex = 1170
        Me.chkPurch.Text = "Purchase Item"
        Me.chkPurch.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(37, 112)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 16)
        Me.Label2.TabIndex = 1173
        Me.Label2.Text = "Item type :"
        '
        'cbItemType
        '
        Me.cbItemType.FormattingEnabled = True
        Me.cbItemType.Location = New System.Drawing.Point(110, 107)
        Me.cbItemType.Name = "cbItemType"
        Me.cbItemType.Size = New System.Drawing.Size(137, 24)
        Me.cbItemType.TabIndex = 1174
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(254, 110)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 16)
        Me.Label3.TabIndex = 1175
        Me.Label3.Text = "Item Category :"
        '
        'cbItemCategory
        '
        Me.cbItemCategory.FormattingEnabled = True
        Me.cbItemCategory.Location = New System.Drawing.Point(358, 106)
        Me.cbItemCategory.Name = "cbItemCategory"
        Me.cbItemCategory.Size = New System.Drawing.Size(219, 24)
        Me.cbItemCategory.TabIndex = 1176
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(27, 192)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(78, 16)
        Me.Label10.TabIndex = 1177
        Me.Label10.Text = "Base UoM :"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(39, 219)
        Me.Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(66, 16)
        Me.Label25.TabIndex = 1181
        Me.Label25.Text = "Barcode :"
        Me.Label25.Visible = False
        '
        'txtBarcode
        '
        Me.txtBarcode.BackColor = System.Drawing.SystemColors.Window
        Me.txtBarcode.ForeColor = System.Drawing.Color.Black
        Me.txtBarcode.Location = New System.Drawing.Point(110, 214)
        Me.txtBarcode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.Size = New System.Drawing.Size(210, 22)
        Me.txtBarcode.TabIndex = 1180
        Me.txtBarcode.Visible = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSearch, Me.tsbNew, Me.tsbEdit, Me.tsbSave, Me.tsbDelete, Me.ToolStripSeparator1, Me.tsbPrevious, Me.tsbNext, Me.ToolStripSeparator2, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1263, 40)
        Me.ToolStrip1.TabIndex = 1185
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
        'tsbDelete
        '
        Me.tsbDelete.AutoSize = False
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
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 40)
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
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 40)
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
        Me.tsbExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'cbUoMGroup
        '
        Me.cbUoMGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbUoMGroup.FormattingEnabled = True
        Me.cbUoMGroup.Location = New System.Drawing.Point(110, 161)
        Me.cbUoMGroup.Name = "cbUoMGroup"
        Me.cbUoMGroup.Size = New System.Drawing.Size(210, 24)
        Me.cbUoMGroup.TabIndex = 1188
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(22, 164)
        Me.Label26.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(83, 16)
        Me.Label26.TabIndex = 1187
        Me.Label26.Text = "UoM Group :"
        '
        'txtUOMBase
        '
        Me.txtUOMBase.BackColor = System.Drawing.SystemColors.Window
        Me.txtUOMBase.Enabled = False
        Me.txtUOMBase.ForeColor = System.Drawing.Color.Black
        Me.txtUOMBase.Location = New System.Drawing.Point(110, 189)
        Me.txtUOMBase.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUOMBase.Name = "txtUOMBase"
        Me.txtUOMBase.Size = New System.Drawing.Size(210, 22)
        Me.txtUOMBase.TabIndex = 1186
        '
        'chkExcise
        '
        Me.chkExcise.AutoSize = True
        Me.chkExcise.Checked = True
        Me.chkExcise.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkExcise.Location = New System.Drawing.Point(597, 216)
        Me.chkExcise.Name = "chkExcise"
        Me.chkExcise.Size = New System.Drawing.Size(93, 20)
        Me.chkExcise.TabIndex = 1209
        Me.chkExcise.Text = "Excise Tax"
        Me.chkExcise.UseVisualStyleBackColor = True
        Me.chkExcise.Visible = False
        '
        'cbItemGroup
        '
        Me.cbItemGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbItemGroup.FormattingEnabled = True
        Me.cbItemGroup.Location = New System.Drawing.Point(110, 134)
        Me.cbItemGroup.Name = "cbItemGroup"
        Me.cbItemGroup.Size = New System.Drawing.Size(210, 24)
        Me.cbItemGroup.TabIndex = 1211
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.ForeColor = System.Drawing.Color.Black
        Me.Label31.Location = New System.Drawing.Point(24, 138)
        Me.Label31.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(79, 16)
        Me.Label31.TabIndex = 1210
        Me.Label31.Text = "Item Group :"
        '
        'txtWeight
        '
        Me.txtWeight.BackColor = System.Drawing.SystemColors.Window
        Me.txtWeight.Enabled = False
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(451, 190)
        Me.txtWeight.Margin = New System.Windows.Forms.Padding(4)
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(127, 22)
        Me.txtWeight.TabIndex = 1216
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.ForeColor = System.Drawing.Color.Black
        Me.Label32.Location = New System.Drawing.Point(364, 193)
        Me.Label32.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(85, 16)
        Me.Label32.TabIndex = 1217
        Me.Label32.Text = "Weight (KG) :"
        '
        'chkProd
        '
        Me.chkProd.AutoSize = True
        Me.chkProd.Location = New System.Drawing.Point(600, 119)
        Me.chkProd.Name = "chkProd"
        Me.chkProd.Size = New System.Drawing.Size(114, 20)
        Me.chkProd.TabIndex = 1171
        Me.chkProd.Text = "Produced Item"
        Me.chkProd.UseVisualStyleBackColor = True
        '
        'cbStatus
        '
        Me.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbStatus.FormattingEnabled = True
        Me.cbStatus.Items.AddRange(New Object() {"Active", "Inactive"})
        Me.cbStatus.Location = New System.Drawing.Point(451, 216)
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.Size = New System.Drawing.Size(126, 24)
        Me.cbStatus.TabIndex = 1219
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.ForeColor = System.Drawing.Color.Black
        Me.Label28.Location = New System.Drawing.Point(393, 219)
        Me.Label28.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(51, 16)
        Me.Label28.TabIndex = 1218
        Me.Label28.Text = "Status :"
        '
        'btnBOMgroup
        '
        Me.btnBOMgroup.BackgroundImage = Global.jade.My.Resources.Resources._New
        Me.btnBOMgroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBOMgroup.Location = New System.Drawing.Point(326, 133)
        Me.btnBOMgroup.Name = "btnBOMgroup"
        Me.btnBOMgroup.Size = New System.Drawing.Size(25, 25)
        Me.btnBOMgroup.TabIndex = 1215
        Me.btnBOMgroup.UseVisualStyleBackColor = True
        '
        'btnUOMGroup
        '
        Me.btnUOMGroup.BackgroundImage = Global.jade.My.Resources.Resources._New
        Me.btnUOMGroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnUOMGroup.Location = New System.Drawing.Point(326, 161)
        Me.btnUOMGroup.Name = "btnUOMGroup"
        Me.btnUOMGroup.Size = New System.Drawing.Size(25, 25)
        Me.btnUOMGroup.TabIndex = 1189
        Me.btnUOMGroup.UseVisualStyleBackColor = True
        '
        'btnUOM
        '
        Me.btnUOM.BackgroundImage = Global.jade.My.Resources.Resources.view
        Me.btnUOM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnUOM.Location = New System.Drawing.Point(326, 189)
        Me.btnUOM.Name = "btnUOM"
        Me.btnUOM.Size = New System.Drawing.Size(25, 25)
        Me.btnUOM.TabIndex = 1183
        Me.btnUOM.UseVisualStyleBackColor = True
        '
        'chkFixedAsset
        '
        Me.chkFixedAsset.AutoSize = True
        Me.chkFixedAsset.Location = New System.Drawing.Point(600, 138)
        Me.chkFixedAsset.Name = "chkFixedAsset"
        Me.chkFixedAsset.Size = New System.Drawing.Size(125, 20)
        Me.chkFixedAsset.TabIndex = 1220
        Me.chkFixedAsset.Text = "Fixed Asset Item"
        Me.chkFixedAsset.UseVisualStyleBackColor = True
        '
        'chkMultipleBarcode
        '
        Me.chkMultipleBarcode.AutoSize = True
        Me.chkMultipleBarcode.Location = New System.Drawing.Point(600, 160)
        Me.chkMultipleBarcode.Name = "chkMultipleBarcode"
        Me.chkMultipleBarcode.Size = New System.Drawing.Size(156, 20)
        Me.chkMultipleBarcode.TabIndex = 1221
        Me.chkMultipleBarcode.Text = "Multiple Barcode Item"
        Me.chkMultipleBarcode.UseVisualStyleBackColor = True
        '
        'chkConsignment
        '
        Me.chkConsignment.AutoSize = True
        Me.chkConsignment.Location = New System.Drawing.Point(600, 184)
        Me.chkConsignment.Name = "chkConsignment"
        Me.chkConsignment.Size = New System.Drawing.Size(133, 20)
        Me.chkConsignment.TabIndex = 1222
        Me.chkConsignment.Text = "Consignment Item"
        Me.chkConsignment.UseVisualStyleBackColor = True
        '
        'lblG5
        '
        Me.lblG5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.lblG5.Location = New System.Drawing.Point(799, 167)
        Me.lblG5.Name = "lblG5"
        Me.lblG5.Size = New System.Drawing.Size(132, 17)
        Me.lblG5.TabIndex = 1336
        Me.lblG5.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblG5.Visible = False
        '
        'lblG4
        '
        Me.lblG4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.lblG4.Location = New System.Drawing.Point(799, 141)
        Me.lblG4.Name = "lblG4"
        Me.lblG4.Size = New System.Drawing.Size(132, 17)
        Me.lblG4.TabIndex = 1335
        Me.lblG4.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblG4.Visible = False
        '
        'lblG3
        '
        Me.lblG3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.lblG3.Location = New System.Drawing.Point(799, 115)
        Me.lblG3.Name = "lblG3"
        Me.lblG3.Size = New System.Drawing.Size(132, 17)
        Me.lblG3.TabIndex = 1334
        Me.lblG3.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblG3.Visible = False
        '
        'lblG2
        '
        Me.lblG2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.lblG2.Location = New System.Drawing.Point(799, 89)
        Me.lblG2.Name = "lblG2"
        Me.lblG2.Size = New System.Drawing.Size(132, 17)
        Me.lblG2.TabIndex = 1333
        Me.lblG2.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblG2.Visible = False
        '
        'lblG1
        '
        Me.lblG1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.lblG1.Location = New System.Drawing.Point(799, 63)
        Me.lblG1.Name = "lblG1"
        Me.lblG1.Size = New System.Drawing.Size(132, 23)
        Me.lblG1.TabIndex = 1332
        Me.lblG1.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblG1.Visible = False
        '
        'cbG5
        '
        Me.cbG5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.cbG5.FormattingEnabled = True
        Me.cbG5.Location = New System.Drawing.Point(939, 164)
        Me.cbG5.Name = "cbG5"
        Me.cbG5.Size = New System.Drawing.Size(312, 24)
        Me.cbG5.TabIndex = 1331
        Me.cbG5.Visible = False
        '
        'cbG4
        '
        Me.cbG4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.cbG4.FormattingEnabled = True
        Me.cbG4.Location = New System.Drawing.Point(939, 138)
        Me.cbG4.Name = "cbG4"
        Me.cbG4.Size = New System.Drawing.Size(312, 24)
        Me.cbG4.TabIndex = 1330
        Me.cbG4.Visible = False
        '
        'cbG3
        '
        Me.cbG3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.cbG3.FormattingEnabled = True
        Me.cbG3.Location = New System.Drawing.Point(939, 112)
        Me.cbG3.Name = "cbG3"
        Me.cbG3.Size = New System.Drawing.Size(312, 24)
        Me.cbG3.TabIndex = 1329
        Me.cbG3.Visible = False
        '
        'cbG2
        '
        Me.cbG2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.cbG2.FormattingEnabled = True
        Me.cbG2.Location = New System.Drawing.Point(939, 86)
        Me.cbG2.Name = "cbG2"
        Me.cbG2.Size = New System.Drawing.Size(312, 24)
        Me.cbG2.TabIndex = 1328
        Me.cbG2.Visible = False
        '
        'cbG1
        '
        Me.cbG1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.cbG1.FormattingEnabled = True
        Me.cbG1.Location = New System.Drawing.Point(939, 60)
        Me.cbG1.Name = "cbG1"
        Me.cbG1.Size = New System.Drawing.Size(312, 24)
        Me.cbG1.TabIndex = 1327
        Me.cbG1.Visible = False
        '
        'cbVATType
        '
        Me.cbVATType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbVATType.FormattingEnabled = True
        Me.cbVATType.Items.AddRange(New Object() {"Active", "Inactive"})
        Me.cbVATType.Location = New System.Drawing.Point(451, 161)
        Me.cbVATType.Name = "cbVATType"
        Me.cbVATType.Size = New System.Drawing.Size(126, 24)
        Me.cbVATType.TabIndex = 1338
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.ForeColor = System.Drawing.Color.Black
        Me.Label40.Location = New System.Drawing.Point(373, 165)
        Me.Label40.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(76, 16)
        Me.Label40.TabIndex = 1337
        Me.Label40.Text = "VAT Type :"
        '
        'frmItem_Master
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1263, 691)
        Me.Controls.Add(Me.cbVATType)
        Me.Controls.Add(Me.Label40)
        Me.Controls.Add(Me.lblG5)
        Me.Controls.Add(Me.lblG4)
        Me.Controls.Add(Me.lblG3)
        Me.Controls.Add(Me.lblG2)
        Me.Controls.Add(Me.lblG1)
        Me.Controls.Add(Me.cbG5)
        Me.Controls.Add(Me.cbG4)
        Me.Controls.Add(Me.cbG3)
        Me.Controls.Add(Me.cbG2)
        Me.Controls.Add(Me.cbG1)
        Me.Controls.Add(Me.chkConsignment)
        Me.Controls.Add(Me.chkMultipleBarcode)
        Me.Controls.Add(Me.chkFixedAsset)
        Me.Controls.Add(Me.cbStatus)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.Label32)
        Me.Controls.Add(Me.txtWeight)
        Me.Controls.Add(Me.btnBOMgroup)
        Me.Controls.Add(Me.cbItemGroup)
        Me.Controls.Add(Me.Label31)
        Me.Controls.Add(Me.chkExcise)
        Me.Controls.Add(Me.btnUOMGroup)
        Me.Controls.Add(Me.cbUoMGroup)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.txtUOMBase)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.chkVATable)
        Me.Controls.Add(Me.btnUOM)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.txtBarcode)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cbItemCategory)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cbItemType)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.chkProd)
        Me.Controls.Add(Me.chkPurch)
        Me.Controls.Add(Me.chkSale)
        Me.Controls.Add(Me.chkInv)
        Me.Controls.Add(Me.tcItemDetails)
        Me.Controls.Add(Me.txtItemCode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtItemName)
        Me.Controls.Add(Me.Label4)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmItem_Master"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Item Master"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tcItemDetails.ResumeLayout(False)
        Me.tpBarcode.ResumeLayout(False)
        Me.tpBarcode.PerformLayout()
        CType(Me.dgvBarcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpPD.ResumeLayout(False)
        Me.tpPD.PerformLayout()
        Me.tpSD.ResumeLayout(False)
        Me.gbSales.ResumeLayout(False)
        Me.gbSales.PerformLayout()
        Me.GrpDefaultDiscount.ResumeLayout(False)
        Me.GrpDefaultDiscount.PerformLayout()
        CType(Me.dgvSell, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpID.ResumeLayout(False)
        Me.tpID.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvInv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpDefaultEntry.ResumeLayout(False)
        Me.tpDefaultEntry.PerformLayout()
        Me.tpMD.ResumeLayout(False)
        Me.tpMD.PerformLayout()
        Me.tpBSCharges.ResumeLayout(False)
        CType(Me.dgvItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage1.ResumeLayout(False)
        Me.grpSizeColor.ResumeLayout(False)
        Me.grpSizeColor.PerformLayout()
        CType(Me.dgvSizeColor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtItemName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtItemCode As System.Windows.Forms.TextBox
    Friend WithEvents tcItemDetails As System.Windows.Forms.TabControl
    Friend WithEvents tpPD As System.Windows.Forms.TabPage
    Friend WithEvents tpID As System.Windows.Forms.TabPage
    Friend WithEvents chkInv As System.Windows.Forms.CheckBox
    Friend WithEvents chkSale As System.Windows.Forms.CheckBox
    Friend WithEvents chkPurch As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbItemType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbItemCategory As System.Windows.Forms.ComboBox
    Friend WithEvents dgvInv As System.Windows.Forms.DataGridView
    Friend WithEvents txtInvStock As System.Windows.Forms.TextBox
    Friend WithEvents txtInvCommit As System.Windows.Forms.TextBox
    Friend WithEvents txtInvOrder As System.Windows.Forms.TextBox
    Friend WithEvents txtInvAvailable As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtInvMin As System.Windows.Forms.TextBox
    Friend WithEvents cbInvMethod As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtInvMax As System.Windows.Forms.TextBox
    Friend WithEvents chkPurchUpdate As System.Windows.Forms.CheckBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtPurchVCECode As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtPurchVCEname As System.Windows.Forms.TextBox
    Friend WithEvents chkPurchVATInc As System.Windows.Forms.CheckBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtPurchPrice As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtSaleAccntCode As System.Windows.Forms.TextBox
    Friend WithEvents txtSaleAccntTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents tpDefaultEntry As System.Windows.Forms.TabPage
    Friend WithEvents txtInvAccntTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtInvAccntCode As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtCostAccntTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtCostAccntCode As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtPurchReorder As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtPurchMinimum As System.Windows.Forms.TextBox
    Friend WithEvents chkVATable As System.Windows.Forms.CheckBox
    Friend WithEvents cbInvWarehouse As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtBarcode As System.Windows.Forms.TextBox
    Friend WithEvents btnUOM As System.Windows.Forms.Button
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbPrevious As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbNext As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPurchUOM As System.Windows.Forms.Button
    Friend WithEvents cbUoMGroup As System.Windows.Forms.ComboBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents btnUOMGroup As System.Windows.Forms.Button
    Friend WithEvents txtUOMBase As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchUOM As System.Windows.Forms.TextBox
    Friend WithEvents btnPurchSupplier As System.Windows.Forms.Button
    Friend WithEvents tpSD As System.Windows.Forms.TabPage
    Friend WithEvents btnSellRemove As System.Windows.Forms.Button
    Friend WithEvents btnSellAdd As System.Windows.Forms.Button
    Friend WithEvents chkSellVAT As System.Windows.Forms.CheckBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtSellQTY As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtSellUOM As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtSellPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cbSellType As System.Windows.Forms.ComboBox
    Friend WithEvents dgvSell As System.Windows.Forms.DataGridView
    Friend WithEvents gbSales As System.Windows.Forms.GroupBox
    Friend WithEvents chkExcise As System.Windows.Forms.CheckBox
    Private WithEvents btnSellUOM As System.Windows.Forms.Button
    Friend WithEvents btnInvUOM As System.Windows.Forms.Button
    Friend WithEvents txtInvUOM As System.Windows.Forms.TextBox
    Friend WithEvents txtDiscAccntTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtDiscAccntCode As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtInvStandard As System.Windows.Forms.TextBox
    Friend WithEvents chInvWHSECode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chInvWHSEName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chInvStock As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chInvOrdered As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chInvCommit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chInvAvail As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkSerial As System.Windows.Forms.CheckBox
    Friend WithEvents cbItemGroup As System.Windows.Forms.ComboBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cbIDTolling As System.Windows.Forms.ComboBox
    Friend WithEvents rbIDTolling As System.Windows.Forms.RadioButton
    Friend WithEvents rbIDOwned As System.Windows.Forms.RadioButton
    Friend WithEvents btnBOMgroup As System.Windows.Forms.Button
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtExpAccntTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtExpAccntCode As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents chkProd As System.Windows.Forms.CheckBox
    Friend WithEvents cbProd As System.Windows.Forms.ComboBox
    Friend WithEvents tpMD As System.Windows.Forms.TabPage
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cbMDprodFloor As System.Windows.Forms.ComboBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents btnSalesAccnt As System.Windows.Forms.Button
    Friend WithEvents cbDept As System.Windows.Forms.ComboBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents cbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents chkVAT As System.Windows.Forms.CheckBox
    Friend WithEvents dcSell_Type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dcSell_Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dcSell_UOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dcSell_UOMQTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dc_VAT As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents dcSell_VAT As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents tpBSCharges As System.Windows.Forms.TabPage
    Friend WithEvents dgvItemList As System.Windows.Forms.DataGridView
    Friend WithEvents chBS_RecordID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chBS_Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chBS_VCECode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chBS_VCEName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chBS_Type As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents chBS_Amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkFixedAsset As System.Windows.Forms.CheckBox
    Friend WithEvents txtDepExpTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtDepExpCode As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtAccumDepTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtAccumDepCode As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtFixedAssetAccountTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtFixedAssetAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents tpBarcode As TabPage
    Friend WithEvents dgvBarcode As DataGridView
    Friend WithEvents dcBarcode_Barcode As DataGridViewTextBoxColumn
    Friend WithEvents dcBarCode_UOM As DataGridViewTextBoxColumn
    Private WithEvents btnBarcodeUOM As Button
    Friend WithEvents btnBarcodeRemove As Button
    Friend WithEvents btnBarcodeAdd As Button
    Friend WithEvents txtBarcode_Barcode As TextBox
    Friend WithEvents Label37 As Label
    Friend WithEvents Label38 As Label
    Friend WithEvents txtBarcode_UOM As TextBox
    Friend WithEvents chkMultipleBarcode As CheckBox
    Friend WithEvents chkConsignment As System.Windows.Forms.CheckBox
    Friend WithEvents txtConAccntTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtConAccntCode As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents GrpDefaultDiscount As GroupBox
    Friend WithEvents RdNone As RadioButton
    Friend WithEvents RdTwentyPercent As RadioButton
    Friend WithEvents RdFivePercent As RadioButton
    Friend WithEvents lblG5 As System.Windows.Forms.Label
    Friend WithEvents lblG4 As System.Windows.Forms.Label
    Friend WithEvents lblG3 As System.Windows.Forms.Label
    Friend WithEvents lblG2 As System.Windows.Forms.Label
    Friend WithEvents lblG1 As System.Windows.Forms.Label
    Friend WithEvents cbG5 As System.Windows.Forms.ComboBox
    Friend WithEvents cbG4 As System.Windows.Forms.ComboBox
    Friend WithEvents cbG3 As System.Windows.Forms.ComboBox
    Friend WithEvents cbG2 As System.Windows.Forms.ComboBox
    Friend WithEvents cbG1 As System.Windows.Forms.ComboBox
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents dgvSizeColor As System.Windows.Forms.DataGridView
    Friend WithEvents btnSizeColor_Remove As System.Windows.Forms.Button
    Friend WithEvents btnSizeColor_Add As System.Windows.Forms.Button
    Friend WithEvents txtSize As System.Windows.Forms.TextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents txtColor As System.Windows.Forms.TextBox
    Friend WithEvents chSize As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chColor As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grpSizeColor As System.Windows.Forms.GroupBox
    Friend WithEvents cbVATType As System.Windows.Forms.ComboBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents txtPD_EOQ As System.Windows.Forms.TextBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents txtPD_MOQ As System.Windows.Forms.TextBox
End Class
