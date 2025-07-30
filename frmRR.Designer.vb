<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRR
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRR))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkFixedAsset = New System.Windows.Forms.CheckBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cbCurrency = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cbPurchaseType = New System.Windows.Forms.ComboBox()
        Me.cbWHSE = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.btnSearchVCE = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPORef = New System.Windows.Forms.TextBox()
        Me.dtpDeliveryDate = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtDRRef = New System.Windows.Forms.TextBox()
        Me.txtVCEName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtVCECode = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpDocDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTransNum = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.contextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ViewItemInformationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.dgvItemList = New System.Windows.Forms.DataGridView()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbSearch = New System.Windows.Forms.ToolStripButton()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbCopy = New System.Windows.Forms.ToolStripDropDownButton()
        Me.tsbCopyPO = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrint = New System.Windows.Forms.ToolStripButton()
        Me.tsbReports = New System.Windows.Forms.ToolStripDropDownButton()
        Me.TestToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrevious = New System.Windows.Forms.ToolStripButton()
        Me.tsbNext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.dgvGroup = New System.Windows.Forms.DataGridView()
        Me.dgcSubGroup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcSubCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcSubDesc = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.dgcSubUOM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcSubPOQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcSubActual = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcSubAdd = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tpItem = New System.Windows.Forms.TabPage()
        Me.tpEntry = New System.Windows.Forms.TabPage()
        Me.txtTotalCredit = New System.Windows.Forms.TextBox()
        Me.txtTotalDebit = New System.Windows.Forms.TextBox()
        Me.lvAccount = New System.Windows.Forms.ListView()
        Me.chAccountCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chAccountTitle = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chDebit = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chCredit = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chRef = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chQTY = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chUCost = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chiCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvVATType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnApplyRate = New System.Windows.Forms.Button()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtDiscountRate = New System.Windows.Forms.TextBox()
        Me.chkVAT = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtVAT = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtGross = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtDiscount = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtNet = New System.Windows.Forms.TextBox()
        Me.chkVATInc = New System.Windows.Forms.CheckBox()
        Me.chItemCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chBarCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chItemDesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chUOM = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.chStandardCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chPOQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chRRQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcWHSE = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.chUnitCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chGross = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDiscountRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDiscountAmt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcVATamt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcNet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcVATInc = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgcVAT1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgcAccountCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chVATType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chDateExpired = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chLotNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chSerialNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chSize = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.chColor = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.GroupBox1.SuspendLayout()
        Me.contextMenuStrip1.SuspendLayout()
        CType(Me.dgvItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.tpItem.SuspendLayout()
        Me.tpEntry.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.chkFixedAsset)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.cbCurrency)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.cbPurchaseType)
        Me.GroupBox1.Controls.Add(Me.cbWHSE)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.btnSearchVCE)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtPORef)
        Me.GroupBox1.Controls.Add(Me.dtpDeliveryDate)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtDRRef)
        Me.GroupBox1.Controls.Add(Me.txtVCEName)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtVCECode)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.dtpDocDate)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtTransNum)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtStatus)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 40)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1072, 172)
        Me.GroupBox1.TabIndex = 1302
        Me.GroupBox1.TabStop = False
        '
        'chkFixedAsset
        '
        Me.chkFixedAsset.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFixedAsset.AutoSize = True
        Me.chkFixedAsset.Location = New System.Drawing.Point(934, 112)
        Me.chkFixedAsset.Name = "chkFixedAsset"
        Me.chkFixedAsset.Size = New System.Drawing.Size(88, 19)
        Me.chkFixedAsset.TabIndex = 1389
        Me.chkFixedAsset.Text = "Fixed Asset"
        Me.chkFixedAsset.UseVisualStyleBackColor = True
        Me.chkFixedAsset.Visible = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(526, 44)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(68, 16)
        Me.Label24.TabIndex = 1381
        Me.Label24.Text = "Currency :"
        '
        'cbCurrency
        '
        Me.cbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCurrency.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCurrency.FormattingEnabled = True
        Me.cbCurrency.Location = New System.Drawing.Point(594, 41)
        Me.cbCurrency.Name = "cbCurrency"
        Me.cbCurrency.Size = New System.Drawing.Size(216, 24)
        Me.cbCurrency.TabIndex = 1380
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(492, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(102, 16)
        Me.Label10.TabIndex = 1311
        Me.Label10.Text = "Purchase Type :"
        '
        'cbPurchaseType
        '
        Me.cbPurchaseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPurchaseType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPurchaseType.FormattingEnabled = True
        Me.cbPurchaseType.Items.AddRange(New Object() {"Goods (Stock)", "Services"})
        Me.cbPurchaseType.Location = New System.Drawing.Point(594, 12)
        Me.cbPurchaseType.Name = "cbPurchaseType"
        Me.cbPurchaseType.Size = New System.Drawing.Size(216, 24)
        Me.cbPurchaseType.TabIndex = 1310
        '
        'cbWHSE
        '
        Me.cbWHSE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbWHSE.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbWHSE.FormattingEnabled = True
        Me.cbWHSE.Location = New System.Drawing.Point(112, 88)
        Me.cbWHSE.Name = "cbWHSE"
        Me.cbWHSE.Size = New System.Drawing.Size(338, 24)
        Me.cbWHSE.TabIndex = 1308
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label22.Location = New System.Drawing.Point(28, 91)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(84, 16)
        Me.Label22.TabIndex = 1309
        Me.Label22.Text = "Warehouse :"
        '
        'btnSearchVCE
        '
        Me.btnSearchVCE.BackgroundImage = Global.jade.My.Resources.Resources.view
        Me.btnSearchVCE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchVCE.Location = New System.Drawing.Point(456, 12)
        Me.btnSearchVCE.Name = "btnSearchVCE"
        Me.btnSearchVCE.Size = New System.Drawing.Size(25, 25)
        Me.btnSearchVCE.TabIndex = 1307
        Me.btnSearchVCE.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label6.Location = New System.Drawing.Point(872, 87)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 16)
        Me.Label6.TabIndex = 1300
        Me.Label6.Text = "PO Ref. :"
        '
        'txtPORef
        '
        Me.txtPORef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPORef.Enabled = False
        Me.txtPORef.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtPORef.Location = New System.Drawing.Point(934, 84)
        Me.txtPORef.Name = "txtPORef"
        Me.txtPORef.Size = New System.Drawing.Size(132, 22)
        Me.txtPORef.TabIndex = 1301
        '
        'dtpDeliveryDate
        '
        Me.dtpDeliveryDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.dtpDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDeliveryDate.Location = New System.Drawing.Point(349, 62)
        Me.dtpDeliveryDate.Name = "dtpDeliveryDate"
        Me.dtpDeliveryDate.Size = New System.Drawing.Size(101, 22)
        Me.dtpDeliveryDate.TabIndex = 1299
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label9.Location = New System.Drawing.Point(251, 66)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(96, 16)
        Me.Label9.TabIndex = 1298
        Me.Label9.Text = "Delivery Date :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label12.Location = New System.Drawing.Point(51, 66)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(61, 16)
        Me.Label12.TabIndex = 1287
        Me.Label12.Text = "DR Ref. :"
        '
        'txtDRRef
        '
        Me.txtDRRef.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDRRef.Location = New System.Drawing.Point(112, 63)
        Me.txtDRRef.Name = "txtDRRef"
        Me.txtDRRef.Size = New System.Drawing.Size(132, 22)
        Me.txtDRRef.TabIndex = 1288
        '
        'txtVCEName
        '
        Me.txtVCEName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVCEName.Location = New System.Drawing.Point(112, 38)
        Me.txtVCEName.Name = "txtVCEName"
        Me.txtVCEName.Size = New System.Drawing.Size(338, 22)
        Me.txtVCEName.TabIndex = 1290
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label5.Location = New System.Drawing.Point(825, 39)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(107, 16)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Document Date :"
        '
        'txtVCECode
        '
        Me.txtVCECode.Enabled = False
        Me.txtVCECode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVCECode.Location = New System.Drawing.Point(112, 13)
        Me.txtVCECode.Name = "txtVCECode"
        Me.txtVCECode.Size = New System.Drawing.Size(338, 22)
        Me.txtVCECode.TabIndex = 1289
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(112, 116)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(338, 50)
        Me.txtRemarks.TabIndex = 1300
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label2.Location = New System.Drawing.Point(43, 116)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 16)
        Me.Label2.TabIndex = 1294
        Me.Label2.Text = "Remarks :"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label4.Location = New System.Drawing.Point(881, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 16)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Status :"
        '
        'dtpDocDate
        '
        Me.dtpDocDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpDocDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDocDate.Location = New System.Drawing.Point(934, 36)
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.Size = New System.Drawing.Size(132, 22)
        Me.dtpDocDate.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label1.Location = New System.Drawing.Point(14, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Vendor Name :"
        '
        'txtTransNum
        '
        Me.txtTransNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTransNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransNum.Location = New System.Drawing.Point(934, 12)
        Me.txtTransNum.Name = "txtTransNum"
        Me.txtTransNum.Size = New System.Drawing.Size(132, 22)
        Me.txtTransNum.TabIndex = 44
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label3.Location = New System.Drawing.Point(18, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(94, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Vendor Code :"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label7.Location = New System.Drawing.Point(874, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 16)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "RR No. :"
        '
        'txtStatus
        '
        Me.txtStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtStatus.Enabled = False
        Me.txtStatus.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(934, 60)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(132, 22)
        Me.txtStatus.TabIndex = 8
        Me.txtStatus.Text = "Open"
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
        'dgvItemList
        '
        Me.dgvItemList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvItemList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chItemCode, Me.chBarCode, Me.chItemDesc, Me.chUOM, Me.chStandardCost, Me.chPOQTY, Me.chRRQTY, Me.dgcWHSE, Me.chUnitCost, Me.chGross, Me.dgcDiscountRate, Me.dgcDiscountAmt, Me.dgcVATamt, Me.dgcNet, Me.dgcVATInc, Me.dgcVAT1, Me.dgcAccountCode, Me.chVATType, Me.chDateExpired, Me.chLotNo, Me.chSerialNo, Me.chSize, Me.chColor})
        Me.dgvItemList.ContextMenuStrip = Me.contextMenuStrip1
        Me.dgvItemList.Location = New System.Drawing.Point(6, 6)
        Me.dgvItemList.Name = "dgvItemList"
        Me.dgvItemList.RowHeadersWidth = 25
        Me.dgvItemList.Size = New System.Drawing.Size(1060, 142)
        Me.dgvItemList.TabIndex = 1297
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(125, 70)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(136, 22)
        Me.TextBox1.TabIndex = 1301
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSearch, Me.tsbNew, Me.tsbEdit, Me.tsbSave, Me.tsbCancel, Me.ToolStripSeparator1, Me.tsbCopy, Me.ToolStripSeparator4, Me.tsbPrint, Me.tsbReports, Me.ToolStripSeparator2, Me.tsbPrevious, Me.tsbNext, Me.ToolStripSeparator3, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1085, 40)
        Me.ToolStrip1.TabIndex = 1316
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
        Me.tsbCopy.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbCopyPO})
        Me.tsbCopy.ForeColor = System.Drawing.Color.White
        Me.tsbCopy.Image = Global.jade.My.Resources.Resources.finance_report_infographic_512
        Me.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbCopy.Name = "tsbCopy"
        Me.tsbCopy.Size = New System.Drawing.Size(48, 37)
        Me.tsbCopy.Text = "Copy"
        Me.tsbCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbCopyPO
        '
        Me.tsbCopyPO.Name = "tsbCopyPO"
        Me.tsbCopyPO.Size = New System.Drawing.Size(186, 22)
        Me.tsbCopyPO.Text = "From Purchase Order"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 40)
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
        '
        'TestToolStripMenuItem1
        '
        Me.TestToolStripMenuItem1.Name = "TestToolStripMenuItem1"
        Me.TestToolStripMenuItem1.Size = New System.Drawing.Size(109, 22)
        Me.TestToolStripMenuItem1.Text = "RR List"
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
        'dgvGroup
        '
        Me.dgvGroup.AllowUserToAddRows = False
        Me.dgvGroup.AllowUserToDeleteRows = False
        Me.dgvGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvGroup.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcSubGroup, Me.dgcSubCode, Me.dgcSubDesc, Me.dgcSubUOM, Me.dgcSubPOQTY, Me.dgcSubActual, Me.dgcSubAdd})
        Me.dgvGroup.Location = New System.Drawing.Point(0, 428)
        Me.dgvGroup.Name = "dgvGroup"
        Me.dgvGroup.RowHeadersWidth = 25
        Me.dgvGroup.Size = New System.Drawing.Size(757, 163)
        Me.dgvGroup.TabIndex = 1317
        Me.dgvGroup.Visible = False
        '
        'dgcSubGroup
        '
        Me.dgcSubGroup.HeaderText = "Item Group"
        Me.dgcSubGroup.Name = "dgcSubGroup"
        Me.dgcSubGroup.Width = 150
        '
        'dgcSubCode
        '
        Me.dgcSubCode.HeaderText = "ItemCode"
        Me.dgcSubCode.Name = "dgcSubCode"
        Me.dgcSubCode.Visible = False
        Me.dgcSubCode.Width = 80
        '
        'dgcSubDesc
        '
        Me.dgcSubDesc.HeaderText = "Item Description"
        Me.dgcSubDesc.Name = "dgcSubDesc"
        Me.dgcSubDesc.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgcSubDesc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.dgcSubDesc.Width = 250
        '
        'dgcSubUOM
        '
        Me.dgcSubUOM.HeaderText = "UOM"
        Me.dgcSubUOM.Name = "dgcSubUOM"
        Me.dgcSubUOM.ReadOnly = True
        Me.dgcSubUOM.Width = 50
        '
        'dgcSubPOQTY
        '
        Me.dgcSubPOQTY.HeaderText = "PO Quantity"
        Me.dgcSubPOQTY.Name = "dgcSubPOQTY"
        Me.dgcSubPOQTY.Width = 80
        '
        'dgcSubActual
        '
        Me.dgcSubActual.HeaderText = "Actual Quantity"
        Me.dgcSubActual.Name = "dgcSubActual"
        Me.dgcSubActual.Width = 80
        '
        'dgcSubAdd
        '
        Me.dgcSubAdd.HeaderText = ""
        Me.dgcSubAdd.Name = "dgcSubAdd"
        Me.dgcSubAdd.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgcSubAdd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.dgcSubAdd.Width = 80
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tpItem)
        Me.TabControl1.Controls.Add(Me.tpEntry)
        Me.TabControl1.Location = New System.Drawing.Point(0, 218)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1080, 192)
        Me.TabControl1.TabIndex = 1310
        '
        'tpItem
        '
        Me.tpItem.Controls.Add(Me.dgvItemList)
        Me.tpItem.Location = New System.Drawing.Point(4, 24)
        Me.tpItem.Name = "tpItem"
        Me.tpItem.Padding = New System.Windows.Forms.Padding(3)
        Me.tpItem.Size = New System.Drawing.Size(1072, 164)
        Me.tpItem.TabIndex = 0
        Me.tpItem.Text = "Line Item"
        Me.tpItem.UseVisualStyleBackColor = True
        '
        'tpEntry
        '
        Me.tpEntry.Controls.Add(Me.txtTotalCredit)
        Me.tpEntry.Controls.Add(Me.txtTotalDebit)
        Me.tpEntry.Controls.Add(Me.lvAccount)
        Me.tpEntry.Location = New System.Drawing.Point(4, 24)
        Me.tpEntry.Name = "tpEntry"
        Me.tpEntry.Padding = New System.Windows.Forms.Padding(3)
        Me.tpEntry.Size = New System.Drawing.Size(1072, 164)
        Me.tpEntry.TabIndex = 1
        Me.tpEntry.Text = "Accounting Entry"
        Me.tpEntry.UseVisualStyleBackColor = True
        '
        'txtTotalCredit
        '
        Me.txtTotalCredit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTotalCredit.Location = New System.Drawing.Point(839, 139)
        Me.txtTotalCredit.Name = "txtTotalCredit"
        Me.txtTotalCredit.Size = New System.Drawing.Size(147, 21)
        Me.txtTotalCredit.TabIndex = 2
        '
        'txtTotalDebit
        '
        Me.txtTotalDebit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTotalDebit.Location = New System.Drawing.Point(688, 139)
        Me.txtTotalDebit.Name = "txtTotalDebit"
        Me.txtTotalDebit.Size = New System.Drawing.Size(147, 21)
        Me.txtTotalDebit.TabIndex = 1
        '
        'lvAccount
        '
        Me.lvAccount.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvAccount.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chAccountCode, Me.chAccountTitle, Me.chDebit, Me.chCredit, Me.chRef, Me.chQTY, Me.chUCost, Me.chiCode, Me.lvVATType})
        Me.lvAccount.GridLines = True
        Me.lvAccount.Location = New System.Drawing.Point(3, 3)
        Me.lvAccount.Name = "lvAccount"
        Me.lvAccount.Size = New System.Drawing.Size(1063, 130)
        Me.lvAccount.TabIndex = 0
        Me.lvAccount.UseCompatibleStateImageBehavior = False
        Me.lvAccount.View = System.Windows.Forms.View.Details
        '
        'chAccountCode
        '
        Me.chAccountCode.Text = "Account Code"
        Me.chAccountCode.Width = 114
        '
        'chAccountTitle
        '
        Me.chAccountTitle.Text = "Account Code"
        Me.chAccountTitle.Width = 567
        '
        'chDebit
        '
        Me.chDebit.Text = "Debit"
        Me.chDebit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.chDebit.Width = 150
        '
        'chCredit
        '
        Me.chCredit.Text = "Credit"
        Me.chCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.chCredit.Width = 150
        '
        'chRef
        '
        Me.chRef.Text = "Ref. No."
        Me.chRef.Width = 75
        '
        'chQTY
        '
        Me.chQTY.Text = "QTY"
        Me.chQTY.Width = 100
        '
        'chUCost
        '
        Me.chUCost.Text = "Unit Cost"
        Me.chUCost.Width = 100
        '
        'chiCode
        '
        Me.chiCode.Text = "Item Code"
        Me.chiCode.Width = 100
        '
        'lvVATType
        '
        Me.lvVATType.Text = "VAT Type"
        '
        'btnApplyRate
        '
        Me.btnApplyRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApplyRate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnApplyRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnApplyRate.Location = New System.Drawing.Point(1043, 468)
        Me.btnApplyRate.Name = "btnApplyRate"
        Me.btnApplyRate.Size = New System.Drawing.Size(31, 25)
        Me.btnApplyRate.TabIndex = 1386
        Me.btnApplyRate.Text = ">>"
        Me.btnApplyRate.UseVisualStyleBackColor = True
        Me.btnApplyRate.Visible = False
        '
        'Label21
        '
        Me.Label21.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(800, 473)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(91, 16)
        Me.Label21.TabIndex = 1385
        Me.Label21.Text = "Discount (%) :"
        '
        'txtDiscountRate
        '
        Me.txtDiscountRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDiscountRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscountRate.Location = New System.Drawing.Point(897, 469)
        Me.txtDiscountRate.Name = "txtDiscountRate"
        Me.txtDiscountRate.Size = New System.Drawing.Size(143, 22)
        Me.txtDiscountRate.TabIndex = 1384
        Me.txtDiscountRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkVAT
        '
        Me.chkVAT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkVAT.AutoSize = True
        Me.chkVAT.Checked = True
        Me.chkVAT.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVAT.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkVAT.Location = New System.Drawing.Point(897, 569)
        Me.chkVAT.Name = "chkVAT"
        Me.chkVAT.Size = New System.Drawing.Size(73, 20)
        Me.chkVAT.TabIndex = 1383
        Me.chkVAT.Text = "VATable"
        Me.chkVAT.UseVisualStyleBackColor = True
        Me.chkVAT.Visible = False
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(814, 519)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 16)
        Me.Label8.TabIndex = 1382
        Me.Label8.Text = "VAT (12%) :"
        '
        'txtVAT
        '
        Me.txtVAT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtVAT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVAT.Location = New System.Drawing.Point(897, 517)
        Me.txtVAT.Name = "txtVAT"
        Me.txtVAT.ReadOnly = True
        Me.txtVAT.Size = New System.Drawing.Size(177, 22)
        Me.txtVAT.TabIndex = 1381
        Me.txtVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(792, 448)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(99, 16)
        Me.Label18.TabIndex = 1380
        Me.Label18.Text = "Gross Amount :"
        '
        'txtGross
        '
        Me.txtGross.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGross.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGross.Location = New System.Drawing.Point(897, 445)
        Me.txtGross.Name = "txtGross"
        Me.txtGross.ReadOnly = True
        Me.txtGross.Size = New System.Drawing.Size(177, 22)
        Me.txtGross.TabIndex = 1379
        Me.txtGross.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(824, 496)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(67, 16)
        Me.Label19.TabIndex = 1378
        Me.Label19.Text = "Discount :"
        '
        'txtDiscount
        '
        Me.txtDiscount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscount.Location = New System.Drawing.Point(897, 493)
        Me.txtDiscount.Name = "txtDiscount"
        Me.txtDiscount.ReadOnly = True
        Me.txtDiscount.Size = New System.Drawing.Size(177, 22)
        Me.txtDiscount.TabIndex = 1377
        Me.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(807, 544)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(84, 16)
        Me.Label20.TabIndex = 1376
        Me.Label20.Text = "Net Amount :"
        '
        'txtNet
        '
        Me.txtNet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNet.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNet.Location = New System.Drawing.Point(897, 541)
        Me.txtNet.Name = "txtNet"
        Me.txtNet.ReadOnly = True
        Me.txtNet.Size = New System.Drawing.Size(177, 22)
        Me.txtNet.TabIndex = 1375
        Me.txtNet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkVATInc
        '
        Me.chkVATInc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkVATInc.AutoSize = True
        Me.chkVATInc.Checked = True
        Me.chkVATInc.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVATInc.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkVATInc.Location = New System.Drawing.Point(980, 569)
        Me.chkVATInc.Margin = New System.Windows.Forms.Padding(4)
        Me.chkVATInc.Name = "chkVATInc"
        Me.chkVATInc.Size = New System.Drawing.Size(103, 20)
        Me.chkVATInc.TabIndex = 1374
        Me.chkVATInc.Text = "VAT Inclusive"
        Me.chkVATInc.UseVisualStyleBackColor = True
        Me.chkVATInc.Visible = False
        '
        'chItemCode
        '
        Me.chItemCode.HeaderText = "Item Code"
        Me.chItemCode.Name = "chItemCode"
        Me.chItemCode.Width = 80
        '
        'chBarCode
        '
        Me.chBarCode.HeaderText = "Barcode"
        Me.chBarCode.Name = "chBarCode"
        '
        'chItemDesc
        '
        Me.chItemDesc.HeaderText = "Item Description"
        Me.chItemDesc.Name = "chItemDesc"
        Me.chItemDesc.Width = 652
        '
        'chUOM
        '
        Me.chUOM.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.chUOM.HeaderText = "UOM"
        Me.chUOM.Name = "chUOM"
        Me.chUOM.ReadOnly = True
        Me.chUOM.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chUOM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.chUOM.Width = 50
        '
        'chStandardCost
        '
        Me.chStandardCost.HeaderText = "Standard Cost"
        Me.chStandardCost.Name = "chStandardCost"
        Me.chStandardCost.ReadOnly = True
        '
        'chPOQTY
        '
        Me.chPOQTY.HeaderText = "PO QTY"
        Me.chPOQTY.Name = "chPOQTY"
        Me.chPOQTY.ReadOnly = True
        Me.chPOQTY.Width = 80
        '
        'chRRQTY
        '
        Me.chRRQTY.HeaderText = "Actual QTY"
        Me.chRRQTY.Name = "chRRQTY"
        Me.chRRQTY.Width = 80
        '
        'dgcWHSE
        '
        Me.dgcWHSE.HeaderText = "WHSE"
        Me.dgcWHSE.Name = "dgcWHSE"
        Me.dgcWHSE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgcWHSE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.dgcWHSE.Visible = False
        Me.dgcWHSE.Width = 150
        '
        'chUnitCost
        '
        Me.chUnitCost.HeaderText = "UnitCost"
        Me.chUnitCost.Name = "chUnitCost"
        '
        'chGross
        '
        Me.chGross.HeaderText = "Gross"
        Me.chGross.Name = "chGross"
        Me.chGross.Visible = False
        '
        'dgcDiscountRate
        '
        Me.dgcDiscountRate.HeaderText = "DiscountRate"
        Me.dgcDiscountRate.Name = "dgcDiscountRate"
        Me.dgcDiscountRate.Visible = False
        '
        'dgcDiscountAmt
        '
        Me.dgcDiscountAmt.HeaderText = "DiscountAmt"
        Me.dgcDiscountAmt.Name = "dgcDiscountAmt"
        Me.dgcDiscountAmt.Visible = False
        '
        'dgcVATamt
        '
        Me.dgcVATamt.HeaderText = "VAT Amount"
        Me.dgcVATamt.Name = "dgcVATamt"
        Me.dgcVATamt.Visible = False
        '
        'dgcNet
        '
        Me.dgcNet.HeaderText = "Net"
        Me.dgcNet.Name = "dgcNet"
        Me.dgcNet.ReadOnly = True
        '
        'dgcVATInc
        '
        Me.dgcVATInc.HeaderText = "VATInc"
        Me.dgcVATInc.Name = "dgcVATInc"
        Me.dgcVATInc.Visible = False
        '
        'dgcVAT1
        '
        Me.dgcVAT1.HeaderText = "VAT"
        Me.dgcVAT1.Name = "dgcVAT1"
        Me.dgcVAT1.Visible = False
        '
        'dgcAccountCode
        '
        Me.dgcAccountCode.HeaderText = "AccountCode"
        Me.dgcAccountCode.Name = "dgcAccountCode"
        Me.dgcAccountCode.ReadOnly = True
        Me.dgcAccountCode.Visible = False
        '
        'chVATType
        '
        Me.chVATType.HeaderText = "VAT Type"
        Me.chVATType.Name = "chVATType"
        '
        'chDateExpired
        '
        DataGridViewCellStyle1.Format = "d"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.chDateExpired.DefaultCellStyle = DataGridViewCellStyle1
        Me.chDateExpired.HeaderText = "Expiry Date (MM/dd/YYYY)"
        Me.chDateExpired.Name = "chDateExpired"
        '
        'chLotNo
        '
        Me.chLotNo.HeaderText = "Lot No."
        Me.chLotNo.Name = "chLotNo"
        '
        'chSerialNo
        '
        Me.chSerialNo.HeaderText = "Serial No."
        Me.chSerialNo.Name = "chSerialNo"
        '
        'chSize
        '
        Me.chSize.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.chSize.HeaderText = "Size"
        Me.chSize.Name = "chSize"
        Me.chSize.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chSize.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'chColor
        '
        Me.chColor.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.chColor.HeaderText = "Color"
        Me.chColor.Name = "chColor"
        Me.chColor.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'frmRR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1085, 603)
        Me.Controls.Add(Me.btnApplyRate)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.txtDiscountRate)
        Me.Controls.Add(Me.chkVAT)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtVAT)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtGross)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtDiscount)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.txtNet)
        Me.Controls.Add(Me.chkVATInc)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.dgvGroup)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmRR"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Receiving Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.contextMenuStrip1.ResumeLayout(False)
        CType(Me.dgvItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.tpItem.ResumeLayout(False)
        Me.tpEntry.ResumeLayout(False)
        Me.tpEntry.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtDRRef As System.Windows.Forms.TextBox
    Friend WithEvents txtVCEName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtVCECode As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpDocDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTransNum As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents contextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ViewItemInformationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvItemList As System.Windows.Forms.DataGridView
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtPORef As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbCopy As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents tsbCopyPO As System.Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents dtpDeliveryDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dgvGroup As System.Windows.Forms.DataGridView
    Friend WithEvents dgcSubGroup As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcSubCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcSubDesc As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents dgcSubUOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcSubPOQTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcSubActual As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcSubAdd As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents cbWHSE As System.Windows.Forms.ComboBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tpItem As System.Windows.Forms.TabPage
    Friend WithEvents tpEntry As System.Windows.Forms.TabPage
    Friend WithEvents lvAccount As System.Windows.Forms.ListView
    Friend WithEvents chAccountCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents chAccountTitle As System.Windows.Forms.ColumnHeader
    Friend WithEvents chDebit As System.Windows.Forms.ColumnHeader
    Friend WithEvents chCredit As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtTotalCredit As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalDebit As System.Windows.Forms.TextBox
    Friend WithEvents chRef As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnApplyRate As System.Windows.Forms.Button
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtDiscountRate As System.Windows.Forms.TextBox
    Friend WithEvents chkVAT As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtVAT As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtGross As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtDiscount As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtNet As System.Windows.Forms.TextBox
    Friend WithEvents chkVATInc As System.Windows.Forms.CheckBox
    Friend WithEvents chQTY As System.Windows.Forms.ColumnHeader
    Friend WithEvents chUCost As System.Windows.Forms.ColumnHeader
    Friend WithEvents chiCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cbPurchaseType As System.Windows.Forms.ComboBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents cbCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents lvVATType As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkFixedAsset As System.Windows.Forms.CheckBox
    Friend WithEvents chItemCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chBarCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chItemDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chUOM As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents chStandardCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chPOQTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chRRQTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcWHSE As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents chUnitCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chGross As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcDiscountRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcDiscountAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcVATamt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcNet As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcVATInc As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents dgcVAT1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents dgcAccountCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chVATType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chDateExpired As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chLotNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chSerialNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chSize As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents chColor As System.Windows.Forms.DataGridViewComboBoxColumn
End Class
