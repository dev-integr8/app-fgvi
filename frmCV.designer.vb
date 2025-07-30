<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCV
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCV))
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtBankRef = New System.Windows.Forms.TextBox()
        Me.txtTransNum = New System.Windows.Forms.TextBox()
        Me.txtBankRefAmount = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtTotalDebit = New System.Windows.Forms.TextBox()
        Me.txtTotalCredit = New System.Windows.Forms.TextBox()
        Me.dtpDocDate = New System.Windows.Forms.DateTimePicker()
        Me.dgvEntry = New System.Windows.Forms.DataGridView()
        Me.chAccntCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chAccntTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chDebit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chCredit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chVATType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chATCCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chCompute = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.chVCECode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chVCEName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chParticulars = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chRef = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chCostID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chCost_Center = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chProfit_Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chProfit_Center = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBranchCode = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Column12 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.chRecordID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dtpBankRefDate = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cbPaymentType = New System.Windows.Forms.ComboBox()
        Me.cbBank = New System.Windows.Forms.ComboBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cbDisburseType = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtORNo = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.gbPayee = New System.Windows.Forms.GroupBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.cbCostCenter = New System.Windows.Forms.ComboBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtRRRef = New System.Windows.Forms.TextBox()
        Me.txtConversion = New System.Windows.Forms.TextBox()
        Me.lblConversion = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.cbCurrency = New System.Windows.Forms.ComboBox()
        Me.btnTax = New System.Windows.Forms.Button()
        Me.txtCARef = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtLoanRef = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.tcPayment = New System.Windows.Forms.TabControl()
        Me.tpCheck = New System.Windows.Forms.TabPage()
        Me.gbBank = New System.Windows.Forms.GroupBox()
        Me.txtBankRefName = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtRefStatus = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tpMultipleCheck = New System.Windows.Forms.TabPage()
        Me.dgvMultipleCheck = New System.Windows.Forms.DataGridView()
        Me.dgcBankID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBank = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.dgcCheckNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcCheckDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcCheckVCECode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcCheckName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tpBankTransfer = New System.Windows.Forms.TabPage()
        Me.txtBankTransfer_VCEBankAccount = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtBankTransfer_VCEBankName = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.txtBankTransfer_Ref = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.dtpBankTransfer_Date = New System.Windows.Forms.DateTimePicker()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtBankTransfer_Amount = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cbBankTransfer_Bank = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.tpMC = New System.Windows.Forms.TabPage()
        Me.cbBankMC = New System.Windows.Forms.ComboBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.tpDebitMemo = New System.Windows.Forms.TabPage()
        Me.cbBankDM = New System.Windows.Forms.ComboBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtDMRef = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtRFPRef = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnUOMGroup = New System.Windows.Forms.Button()
        Me.txtADVRef = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnSearchVCE = New System.Windows.Forms.Button()
        Me.txtVCEName = New System.Windows.Forms.TextBox()
        Me.txtVCECode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtAPVRef = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditEntriesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbSearch = New System.Windows.Forms.ToolStripButton()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbCancel = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbOption = New System.Windows.Forms.ToolStripDropDownButton()
        Me.OutstandingChequeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StailedChequeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CancelCheckToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsbCopy = New System.Windows.Forms.ToolStripDropDownButton()
        Me.tsbCopyAPV = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsbCopyADV = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromRFPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromLoanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromFundsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromCAToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromPCVToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromPTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromCALiquidationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromRRToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromATDToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromCSFeeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromPJToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromBookingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromTerminalPayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromRealEstateCommissionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsbDownload = New System.Windows.Forms.ToolStripButton()
        Me.tsbUpload = New System.Windows.Forms.ToolStripButton()
        Me.pgbCounter = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrint = New System.Windows.Forms.ToolStripSplitButton()
        Me.PrintCVToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChequieToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BIR2307ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConvertCheque = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsbReports = New System.Windows.Forms.ToolStripDropDownButton()
        Me.CVListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrevious = New System.Windows.Forms.ToolStripButton()
        Me.tsbNext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.bgwUpload = New System.ComponentModel.BackgroundWorker()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        CType(Me.dgvEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbPayee.SuspendLayout()
        Me.tcPayment.SuspendLayout()
        Me.tpCheck.SuspendLayout()
        Me.gbBank.SuspendLayout()
        Me.tpMultipleCheck.SuspendLayout()
        CType(Me.dgvMultipleCheck, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpBankTransfer.SuspendLayout()
        Me.tpMC.SuspendLayout()
        Me.tpDebitMemo.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(15, 72)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 16)
        Me.Label6.TabIndex = 91
        Me.Label6.Text = "Check No. :"
        '
        'txtBankRef
        '
        Me.txtBankRef.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankRef.Location = New System.Drawing.Point(93, 69)
        Me.txtBankRef.Name = "txtBankRef"
        Me.txtBankRef.Size = New System.Drawing.Size(210, 22)
        Me.txtBankRef.TabIndex = 12
        '
        'txtTransNum
        '
        Me.txtTransNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTransNum.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransNum.Location = New System.Drawing.Point(1138, 13)
        Me.txtTransNum.Name = "txtTransNum"
        Me.txtTransNum.Size = New System.Drawing.Size(132, 22)
        Me.txtTransNum.TabIndex = 16
        '
        'txtBankRefAmount
        '
        Me.txtBankRefAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankRefAmount.Location = New System.Drawing.Point(93, 119)
        Me.txtBankRefAmount.Name = "txtBankRefAmount"
        Me.txtBankRefAmount.ReadOnly = True
        Me.txtBankRefAmount.Size = New System.Drawing.Size(210, 22)
        Me.txtBankRefAmount.TabIndex = 14
        Me.txtBankRefAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBankRefAmount.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(31, 121)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(61, 16)
        Me.Label7.TabIndex = 92
        Me.Label7.Text = "Amount :"
        Me.Label7.Visible = False
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(292, 536)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 16)
        Me.Label8.TabIndex = 98
        Me.Label8.Text = "Total Debit:"
        '
        'txtTotalDebit
        '
        Me.txtTotalDebit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTotalDebit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalDebit.Location = New System.Drawing.Point(371, 536)
        Me.txtTotalDebit.Name = "txtTotalDebit"
        Me.txtTotalDebit.ReadOnly = True
        Me.txtTotalDebit.Size = New System.Drawing.Size(161, 22)
        Me.txtTotalDebit.TabIndex = 97
        Me.txtTotalDebit.TabStop = False
        '
        'txtTotalCredit
        '
        Me.txtTotalCredit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTotalCredit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCredit.Location = New System.Drawing.Point(616, 536)
        Me.txtTotalCredit.Name = "txtTotalCredit"
        Me.txtTotalCredit.ReadOnly = True
        Me.txtTotalCredit.Size = New System.Drawing.Size(161, 22)
        Me.txtTotalCredit.TabIndex = 96
        Me.txtTotalCredit.TabStop = False
        '
        'dtpDocDate
        '
        Me.dtpDocDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpDocDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDocDate.Location = New System.Drawing.Point(1138, 38)
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.Size = New System.Drawing.Size(132, 22)
        Me.dtpDocDate.TabIndex = 17
        '
        'dgvEntry
        '
        Me.dgvEntry.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEntry.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chAccntCode, Me.chAccntTitle, Me.chDebit, Me.chCredit, Me.chVATType, Me.chATCCode, Me.chCompute, Me.chVCECode, Me.chVCEName, Me.chParticulars, Me.chRef, Me.chCostID, Me.chCost_Center, Me.chProfit_Code, Me.chProfit_Center, Me.dgcBranchCode, Me.Column12, Me.chRecordID})
        Me.dgvEntry.Location = New System.Drawing.Point(3, 2)
        Me.dgvEntry.Name = "dgvEntry"
        Me.dgvEntry.RowHeadersWidth = 25
        Me.dgvEntry.Size = New System.Drawing.Size(1262, 168)
        Me.dgvEntry.TabIndex = 21
        '
        'chAccntCode
        '
        Me.chAccntCode.HeaderText = "Account Code"
        Me.chAccntCode.Name = "chAccntCode"
        '
        'chAccntTitle
        '
        Me.chAccntTitle.HeaderText = "Account Title"
        Me.chAccntTitle.Name = "chAccntTitle"
        Me.chAccntTitle.Width = 250
        '
        'chDebit
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.chDebit.DefaultCellStyle = DataGridViewCellStyle1
        Me.chDebit.HeaderText = "Debit"
        Me.chDebit.Name = "chDebit"
        '
        'chCredit
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.chCredit.DefaultCellStyle = DataGridViewCellStyle2
        Me.chCredit.HeaderText = "Credit"
        Me.chCredit.Name = "chCredit"
        '
        'chVATType
        '
        Me.chVATType.HeaderText = "VAT Type"
        Me.chVATType.Name = "chVATType"
        Me.chVATType.ReadOnly = True
        '
        'chATCCode
        '
        Me.chATCCode.HeaderText = "ATC Code"
        Me.chATCCode.Name = "chATCCode"
        Me.chATCCode.ReadOnly = True
        Me.chATCCode.Width = 60
        '
        'chCompute
        '
        Me.chCompute.HeaderText = "Tax"
        Me.chCompute.MinimumWidth = 30
        Me.chCompute.Name = "chCompute"
        Me.chCompute.Text = "%"
        Me.chCompute.Width = 30
        '
        'chVCECode
        '
        Me.chVCECode.HeaderText = "VCECode"
        Me.chVCECode.Name = "chVCECode"
        Me.chVCECode.Width = 80
        '
        'chVCEName
        '
        Me.chVCEName.HeaderText = "VCEName"
        Me.chVCEName.Name = "chVCEName"
        Me.chVCEName.Width = 160
        '
        'chParticulars
        '
        Me.chParticulars.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomLeft
        Me.chParticulars.DefaultCellStyle = DataGridViewCellStyle3
        Me.chParticulars.HeaderText = "Particulars"
        Me.chParticulars.Name = "chParticulars"
        Me.chParticulars.Width = 74
        '
        'chRef
        '
        Me.chRef.HeaderText = "RefNo"
        Me.chRef.Name = "chRef"
        Me.chRef.ReadOnly = True
        '
        'chCostID
        '
        Me.chCostID.HeaderText = "Cost ID"
        Me.chCostID.Name = "chCostID"
        Me.chCostID.ReadOnly = True
        '
        'chCost_Center
        '
        Me.chCost_Center.HeaderText = "Cost Center"
        Me.chCost_Center.Name = "chCost_Center"
        '
        'chProfit_Code
        '
        Me.chProfit_Code.HeaderText = "Profit Code"
        Me.chProfit_Code.Name = "chProfit_Code"
        Me.chProfit_Code.Visible = False
        '
        'chProfit_Center
        '
        Me.chProfit_Center.HeaderText = "Profit Center"
        Me.chProfit_Center.Name = "chProfit_Center"
        '
        'dgcBranchCode
        '
        Me.dgcBranchCode.HeaderText = "Branch Code"
        Me.dgcBranchCode.Name = "dgcBranchCode"
        Me.dgcBranchCode.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgcBranchCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.dgcBranchCode.Width = 50
        '
        'Column12
        '
        Me.Column12.HeaderText = ">>"
        Me.Column12.Name = "Column12"
        Me.Column12.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column12.Width = 50
        '
        'chRecordID
        '
        Me.chRecordID.HeaderText = "RecordID"
        Me.chRecordID.Name = "chRecordID"
        Me.chRecordID.Visible = False
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(129, 227)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtRemarks.Size = New System.Drawing.Size(316, 51)
        Me.txtRemarks.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label10.Location = New System.Drawing.Point(47, 227)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(78, 16)
        Me.Label10.TabIndex = 108
        Me.Label10.Text = "Particulars :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(8, 98)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(84, 16)
        Me.Label11.TabIndex = 113
        Me.Label11.Text = "Check Date :"
        '
        'dtpBankRefDate
        '
        Me.dtpBankRefDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpBankRefDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpBankRefDate.Location = New System.Drawing.Point(93, 94)
        Me.dtpBankRefDate.Name = "dtpBankRefDate"
        Me.dtpBankRefDate.Size = New System.Drawing.Size(210, 22)
        Me.dtpBankRefDate.TabIndex = 13
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(46, 22)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(46, 16)
        Me.Label12.TabIndex = 116
        Me.Label12.Text = "Bank :"
        '
        'cbPaymentType
        '
        Me.cbPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPaymentType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPaymentType.FormattingEnabled = True
        Me.cbPaymentType.Items.AddRange(New Object() {"Check", "Cash", "Bank Transfer", "Debit Memo", "Manager's Check"})
        Me.cbPaymentType.Location = New System.Drawing.Point(129, 64)
        Me.cbPaymentType.Name = "cbPaymentType"
        Me.cbPaymentType.Size = New System.Drawing.Size(319, 24)
        Me.cbPaymentType.TabIndex = 4
        '
        'cbBank
        '
        Me.cbBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBank.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbBank.FormattingEnabled = True
        Me.cbBank.Location = New System.Drawing.Point(93, 18)
        Me.cbBank.Name = "cbBank"
        Me.cbBank.Size = New System.Drawing.Size(210, 24)
        Me.cbBank.TabIndex = 10
        '
        'Button3
        '
        Me.Button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(308, 16)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(33, 28)
        Me.Button3.TabIndex = 11
        Me.Button3.Text = ">>"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(541, 536)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(77, 16)
        Me.Label20.TabIndex = 129
        Me.Label20.Text = "Total Credit:"
        '
        'cbDisburseType
        '
        Me.cbDisburseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDisburseType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbDisburseType.FormattingEnabled = True
        Me.cbDisburseType.Location = New System.Drawing.Point(129, 145)
        Me.cbDisburseType.Name = "cbDisburseType"
        Me.cbDisburseType.Size = New System.Drawing.Size(167, 24)
        Me.cbDisburseType.TabIndex = 6
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label22.Location = New System.Drawing.Point(30, 146)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(98, 16)
        Me.Label22.TabIndex = 140
        Me.Label22.Text = "Expense Type :"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label23.Location = New System.Drawing.Point(9, 171)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(119, 16)
        Me.Label23.TabIndex = 142
        Me.Label23.Text = "Costumer OR No. :"
        '
        'txtORNo
        '
        Me.txtORNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtORNo.Location = New System.Drawing.Point(129, 172)
        Me.txtORNo.Name = "txtORNo"
        Me.txtORNo.Size = New System.Drawing.Size(167, 22)
        Me.txtORNo.TabIndex = 8
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label25.Location = New System.Drawing.Point(13, 67)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(115, 16)
        Me.Label25.TabIndex = 1340
        Me.Label25.Text = "Payment Method :"
        '
        'gbPayee
        '
        Me.gbPayee.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbPayee.Controls.Add(Me.Label37)
        Me.gbPayee.Controls.Add(Me.cbCostCenter)
        Me.gbPayee.Controls.Add(Me.Label36)
        Me.gbPayee.Controls.Add(Me.txtRRRef)
        Me.gbPayee.Controls.Add(Me.txtConversion)
        Me.gbPayee.Controls.Add(Me.lblConversion)
        Me.gbPayee.Controls.Add(Me.Label35)
        Me.gbPayee.Controls.Add(Me.cbCurrency)
        Me.gbPayee.Controls.Add(Me.btnTax)
        Me.gbPayee.Controls.Add(Me.txtCARef)
        Me.gbPayee.Controls.Add(Me.Label32)
        Me.gbPayee.Controls.Add(Me.txtLoanRef)
        Me.gbPayee.Controls.Add(Me.Label31)
        Me.gbPayee.Controls.Add(Me.tcPayment)
        Me.gbPayee.Controls.Add(Me.txtRFPRef)
        Me.gbPayee.Controls.Add(Me.Label17)
        Me.gbPayee.Controls.Add(Me.Label16)
        Me.gbPayee.Controls.Add(Me.btnUOMGroup)
        Me.gbPayee.Controls.Add(Me.txtADVRef)
        Me.gbPayee.Controls.Add(Me.Label14)
        Me.gbPayee.Controls.Add(Me.btnSearchVCE)
        Me.gbPayee.Controls.Add(Me.txtVCEName)
        Me.gbPayee.Controls.Add(Me.txtVCECode)
        Me.gbPayee.Controls.Add(Me.Label2)
        Me.gbPayee.Controls.Add(Me.Label3)
        Me.gbPayee.Controls.Add(Me.txtStatus)
        Me.gbPayee.Controls.Add(Me.Label15)
        Me.gbPayee.Controls.Add(Me.txtAPVRef)
        Me.gbPayee.Controls.Add(Me.Label4)
        Me.gbPayee.Controls.Add(Me.txtAmount)
        Me.gbPayee.Controls.Add(Me.Label1)
        Me.gbPayee.Controls.Add(Me.Label13)
        Me.gbPayee.Controls.Add(Me.Label9)
        Me.gbPayee.Controls.Add(Me.txtORNo)
        Me.gbPayee.Controls.Add(Me.Label23)
        Me.gbPayee.Controls.Add(Me.Label10)
        Me.gbPayee.Controls.Add(Me.txtRemarks)
        Me.gbPayee.Controls.Add(Me.cbPaymentType)
        Me.gbPayee.Controls.Add(Me.cbDisburseType)
        Me.gbPayee.Controls.Add(Me.Label22)
        Me.gbPayee.Controls.Add(Me.dtpDocDate)
        Me.gbPayee.Controls.Add(Me.Label25)
        Me.gbPayee.Controls.Add(Me.txtTransNum)
        Me.gbPayee.Location = New System.Drawing.Point(8, 40)
        Me.gbPayee.Name = "gbPayee"
        Me.gbPayee.Size = New System.Drawing.Size(1276, 288)
        Me.gbPayee.TabIndex = 47
        Me.gbPayee.TabStop = False
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label37.Location = New System.Drawing.Point(42, 199)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(85, 16)
        Me.Label37.TabIndex = 1384
        Me.Label37.Text = "Cost Center :"
        '
        'cbCostCenter
        '
        Me.cbCostCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCostCenter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCostCenter.FormattingEnabled = True
        Me.cbCostCenter.Location = New System.Drawing.Point(129, 198)
        Me.cbCostCenter.Name = "cbCostCenter"
        Me.cbCostCenter.Size = New System.Drawing.Size(167, 24)
        Me.cbCostCenter.TabIndex = 1383
        '
        'Label36
        '
        Me.Label36.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label36.Location = New System.Drawing.Point(1074, 115)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(61, 16)
        Me.Label36.TabIndex = 1381
        Me.Label36.Text = "RR Ref. :"
        '
        'txtRRRef
        '
        Me.txtRRRef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRRRef.Enabled = False
        Me.txtRRRef.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtRRRef.Location = New System.Drawing.Point(1138, 112)
        Me.txtRRRef.Name = "txtRRRef"
        Me.txtRRRef.ReadOnly = True
        Me.txtRRRef.Size = New System.Drawing.Size(132, 22)
        Me.txtRRRef.TabIndex = 1382
        '
        'txtConversion
        '
        Me.txtConversion.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConversion.Location = New System.Drawing.Point(352, 118)
        Me.txtConversion.Name = "txtConversion"
        Me.txtConversion.Size = New System.Drawing.Size(93, 22)
        Me.txtConversion.TabIndex = 1379
        Me.txtConversion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtConversion.Visible = False
        '
        'lblConversion
        '
        Me.lblConversion.AutoSize = True
        Me.lblConversion.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lblConversion.Location = New System.Drawing.Point(253, 121)
        Me.lblConversion.Name = "lblConversion"
        Me.lblConversion.Size = New System.Drawing.Size(96, 15)
        Me.lblConversion.TabIndex = 1380
        Me.lblConversion.Text = "Exchange Rate :"
        Me.lblConversion.Visible = False
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label35.Location = New System.Drawing.Point(60, 118)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(68, 16)
        Me.Label35.TabIndex = 1378
        Me.Label35.Text = "Currency :"
        '
        'cbCurrency
        '
        Me.cbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCurrency.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCurrency.FormattingEnabled = True
        Me.cbCurrency.Location = New System.Drawing.Point(129, 117)
        Me.cbCurrency.Name = "cbCurrency"
        Me.cbCurrency.Size = New System.Drawing.Size(118, 24)
        Me.cbCurrency.TabIndex = 1377
        '
        'btnTax
        '
        Me.btnTax.Location = New System.Drawing.Point(299, 90)
        Me.btnTax.Name = "btnTax"
        Me.btnTax.Size = New System.Drawing.Size(25, 23)
        Me.btnTax.TabIndex = 1376
        Me.btnTax.Text = "%"
        Me.btnTax.UseVisualStyleBackColor = True
        '
        'txtCARef
        '
        Me.txtCARef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCARef.Enabled = False
        Me.txtCARef.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCARef.Location = New System.Drawing.Point(1138, 88)
        Me.txtCARef.Name = "txtCARef"
        Me.txtCARef.ReadOnly = True
        Me.txtCARef.Size = New System.Drawing.Size(132, 22)
        Me.txtCARef.TabIndex = 1374
        Me.txtCARef.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label32
        '
        Me.Label32.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label32.Location = New System.Drawing.Point(1076, 90)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(60, 16)
        Me.Label32.TabIndex = 1375
        Me.Label32.Text = "CA Ref. :"
        '
        'txtLoanRef
        '
        Me.txtLoanRef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLoanRef.Enabled = False
        Me.txtLoanRef.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoanRef.Location = New System.Drawing.Point(1139, 190)
        Me.txtLoanRef.Name = "txtLoanRef"
        Me.txtLoanRef.Size = New System.Drawing.Size(132, 22)
        Me.txtLoanRef.TabIndex = 1372
        Me.txtLoanRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLoanRef.Visible = False
        '
        'Label31
        '
        Me.Label31.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label31.Location = New System.Drawing.Point(1066, 193)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(71, 16)
        Me.Label31.TabIndex = 1373
        Me.Label31.Text = "Loan Ref. :"
        Me.Label31.Visible = False
        '
        'tcPayment
        '
        Me.tcPayment.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcPayment.Controls.Add(Me.tpCheck)
        Me.tcPayment.Controls.Add(Me.tpMultipleCheck)
        Me.tcPayment.Controls.Add(Me.tpBankTransfer)
        Me.tcPayment.Controls.Add(Me.tpMC)
        Me.tcPayment.Controls.Add(Me.tpDebitMemo)
        Me.tcPayment.ItemSize = New System.Drawing.Size(41, 25)
        Me.tcPayment.Location = New System.Drawing.Point(485, 14)
        Me.tcPayment.Name = "tcPayment"
        Me.tcPayment.SelectedIndex = 0
        Me.tcPayment.Size = New System.Drawing.Size(571, 216)
        Me.tcPayment.TabIndex = 1371
        '
        'tpCheck
        '
        Me.tpCheck.Controls.Add(Me.gbBank)
        Me.tpCheck.Location = New System.Drawing.Point(4, 29)
        Me.tpCheck.Name = "tpCheck"
        Me.tpCheck.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCheck.Size = New System.Drawing.Size(563, 183)
        Me.tpCheck.TabIndex = 1
        Me.tpCheck.Text = "Check"
        Me.tpCheck.UseVisualStyleBackColor = True
        '
        'gbBank
        '
        Me.gbBank.Controls.Add(Me.txtBankRefName)
        Me.gbBank.Controls.Add(Me.Label30)
        Me.gbBank.Controls.Add(Me.txtRefStatus)
        Me.gbBank.Controls.Add(Me.Label5)
        Me.gbBank.Controls.Add(Me.cbBank)
        Me.gbBank.Controls.Add(Me.Label12)
        Me.gbBank.Controls.Add(Me.dtpBankRefDate)
        Me.gbBank.Controls.Add(Me.Button3)
        Me.gbBank.Controls.Add(Me.Label11)
        Me.gbBank.Controls.Add(Me.txtBankRefAmount)
        Me.gbBank.Controls.Add(Me.Label7)
        Me.gbBank.Controls.Add(Me.txtBankRef)
        Me.gbBank.Controls.Add(Me.Label6)
        Me.gbBank.Location = New System.Drawing.Point(3, 7)
        Me.gbBank.Name = "gbBank"
        Me.gbBank.Size = New System.Drawing.Size(398, 176)
        Me.gbBank.TabIndex = 10
        Me.gbBank.TabStop = False
        Me.gbBank.Text = "Bank Details"
        '
        'txtBankRefName
        '
        Me.txtBankRefName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankRefName.Location = New System.Drawing.Point(93, 45)
        Me.txtBankRefName.Name = "txtBankRefName"
        Me.txtBankRefName.Size = New System.Drawing.Size(210, 22)
        Me.txtBankRefName.TabIndex = 131
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(42, 48)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(50, 16)
        Me.Label30.TabIndex = 132
        Me.Label30.Text = "Name :"
        '
        'txtRefStatus
        '
        Me.txtRefStatus.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefStatus.Location = New System.Drawing.Point(93, 143)
        Me.txtRefStatus.Name = "txtRefStatus"
        Me.txtRefStatus.ReadOnly = True
        Me.txtRefStatus.Size = New System.Drawing.Size(210, 22)
        Me.txtRefStatus.TabIndex = 15
        Me.txtRefStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(31, 145)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 16)
        Me.Label5.TabIndex = 130
        Me.Label5.Text = "Status :"
        '
        'tpMultipleCheck
        '
        Me.tpMultipleCheck.Controls.Add(Me.dgvMultipleCheck)
        Me.tpMultipleCheck.Location = New System.Drawing.Point(4, 29)
        Me.tpMultipleCheck.Name = "tpMultipleCheck"
        Me.tpMultipleCheck.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMultipleCheck.Size = New System.Drawing.Size(563, 183)
        Me.tpMultipleCheck.TabIndex = 2
        Me.tpMultipleCheck.Text = "Multiple Check"
        Me.tpMultipleCheck.UseVisualStyleBackColor = True
        '
        'dgvMultipleCheck
        '
        Me.dgvMultipleCheck.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMultipleCheck.BackgroundColor = System.Drawing.Color.White
        Me.dgvMultipleCheck.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMultipleCheck.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcBankID, Me.dgcBank, Me.dgcCheckNo, Me.dgcCheckDate, Me.dgcAmount, Me.dgcCheckVCECode, Me.dgcCheckName, Me.dgcStatus})
        Me.dgvMultipleCheck.Location = New System.Drawing.Point(6, 2)
        Me.dgvMultipleCheck.Name = "dgvMultipleCheck"
        Me.dgvMultipleCheck.RowHeadersWidth = 25
        Me.dgvMultipleCheck.Size = New System.Drawing.Size(554, 173)
        Me.dgvMultipleCheck.TabIndex = 22
        '
        'dgcBankID
        '
        Me.dgcBankID.HeaderText = "BankID"
        Me.dgcBankID.Name = "dgcBankID"
        Me.dgcBankID.Visible = False
        '
        'dgcBank
        '
        Me.dgcBank.HeaderText = "Bank"
        Me.dgcBank.Name = "dgcBank"
        Me.dgcBank.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgcBank.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'dgcCheckNo
        '
        Me.dgcCheckNo.HeaderText = "Check No."
        Me.dgcCheckNo.Name = "dgcCheckNo"
        '
        'dgcCheckDate
        '
        DataGridViewCellStyle4.Format = "MM/dd/yyyy"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.dgcCheckDate.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgcCheckDate.HeaderText = "Check Date"
        Me.dgcCheckDate.Name = "dgcCheckDate"
        '
        'dgcAmount
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "999,999,999.00"
        DataGridViewCellStyle5.NullValue = "0.00"
        Me.dgcAmount.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgcAmount.HeaderText = "Amount"
        Me.dgcAmount.Name = "dgcAmount"
        '
        'dgcCheckVCECode
        '
        Me.dgcCheckVCECode.HeaderText = "VCE Code"
        Me.dgcCheckVCECode.Name = "dgcCheckVCECode"
        Me.dgcCheckVCECode.Visible = False
        '
        'dgcCheckName
        '
        Me.dgcCheckName.HeaderText = "Check Name"
        Me.dgcCheckName.Name = "dgcCheckName"
        Me.dgcCheckName.Width = 200
        '
        'dgcStatus
        '
        Me.dgcStatus.HeaderText = "Status"
        Me.dgcStatus.Name = "dgcStatus"
        '
        'tpBankTransfer
        '
        Me.tpBankTransfer.Controls.Add(Me.txtBankTransfer_VCEBankAccount)
        Me.tpBankTransfer.Controls.Add(Me.Label18)
        Me.tpBankTransfer.Controls.Add(Me.txtBankTransfer_VCEBankName)
        Me.tpBankTransfer.Controls.Add(Me.Label33)
        Me.tpBankTransfer.Controls.Add(Me.txtBankTransfer_Ref)
        Me.tpBankTransfer.Controls.Add(Me.Label26)
        Me.tpBankTransfer.Controls.Add(Me.dtpBankTransfer_Date)
        Me.tpBankTransfer.Controls.Add(Me.Label21)
        Me.tpBankTransfer.Controls.Add(Me.txtBankTransfer_Amount)
        Me.tpBankTransfer.Controls.Add(Me.Label24)
        Me.tpBankTransfer.Controls.Add(Me.cbBankTransfer_Bank)
        Me.tpBankTransfer.Controls.Add(Me.Label19)
        Me.tpBankTransfer.Location = New System.Drawing.Point(4, 29)
        Me.tpBankTransfer.Name = "tpBankTransfer"
        Me.tpBankTransfer.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBankTransfer.Size = New System.Drawing.Size(563, 183)
        Me.tpBankTransfer.TabIndex = 3
        Me.tpBankTransfer.Text = "Bank Transfer"
        Me.tpBankTransfer.UseVisualStyleBackColor = True
        '
        'txtBankTransfer_VCEBankAccount
        '
        Me.txtBankTransfer_VCEBankAccount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankTransfer_VCEBankAccount.Location = New System.Drawing.Point(422, 40)
        Me.txtBankTransfer_VCEBankAccount.Name = "txtBankTransfer_VCEBankAccount"
        Me.txtBankTransfer_VCEBankAccount.Size = New System.Drawing.Size(210, 22)
        Me.txtBankTransfer_VCEBankAccount.TabIndex = 128
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(319, 44)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(97, 16)
        Me.Label18.TabIndex = 129
        Me.Label18.Text = "Bank Account :"
        '
        'txtBankTransfer_VCEBankName
        '
        Me.txtBankTransfer_VCEBankName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankTransfer_VCEBankName.Location = New System.Drawing.Point(422, 15)
        Me.txtBankTransfer_VCEBankName.Name = "txtBankTransfer_VCEBankName"
        Me.txtBankTransfer_VCEBankName.Size = New System.Drawing.Size(210, 22)
        Me.txtBankTransfer_VCEBankName.TabIndex = 126
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(332, 18)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(84, 16)
        Me.Label33.TabIndex = 127
        Me.Label33.Text = "Bank Name :"
        '
        'txtBankTransfer_Ref
        '
        Me.txtBankTransfer_Ref.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankTransfer_Ref.Location = New System.Drawing.Point(94, 71)
        Me.txtBankTransfer_Ref.Name = "txtBankTransfer_Ref"
        Me.txtBankTransfer_Ref.Size = New System.Drawing.Size(210, 22)
        Me.txtBankTransfer_Ref.TabIndex = 124
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(16, 74)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(74, 16)
        Me.Label26.TabIndex = 125
        Me.Label26.Text = "Reference :"
        '
        'dtpBankTransfer_Date
        '
        Me.dtpBankTransfer_Date.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpBankTransfer_Date.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpBankTransfer_Date.Location = New System.Drawing.Point(94, 43)
        Me.dtpBankTransfer_Date.Name = "dtpBankTransfer_Date"
        Me.dtpBankTransfer_Date.Size = New System.Drawing.Size(210, 22)
        Me.dtpBankTransfer_Date.TabIndex = 120
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(9, 46)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(84, 16)
        Me.Label21.TabIndex = 123
        Me.Label21.Text = "Check Date :"
        '
        'txtBankTransfer_Amount
        '
        Me.txtBankTransfer_Amount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankTransfer_Amount.Location = New System.Drawing.Point(94, 99)
        Me.txtBankTransfer_Amount.Name = "txtBankTransfer_Amount"
        Me.txtBankTransfer_Amount.Size = New System.Drawing.Size(210, 22)
        Me.txtBankTransfer_Amount.TabIndex = 121
        Me.txtBankTransfer_Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(32, 101)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(61, 16)
        Me.Label24.TabIndex = 122
        Me.Label24.Text = "Amount :"
        '
        'cbBankTransfer_Bank
        '
        Me.cbBankTransfer_Bank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBankTransfer_Bank.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbBankTransfer_Bank.FormattingEnabled = True
        Me.cbBankTransfer_Bank.Location = New System.Drawing.Point(94, 15)
        Me.cbBankTransfer_Bank.Name = "cbBankTransfer_Bank"
        Me.cbBankTransfer_Bank.Size = New System.Drawing.Size(210, 24)
        Me.cbBankTransfer_Bank.TabIndex = 117
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(47, 19)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(46, 16)
        Me.Label19.TabIndex = 119
        Me.Label19.Text = "Bank :"
        '
        'tpMC
        '
        Me.tpMC.Controls.Add(Me.cbBankMC)
        Me.tpMC.Controls.Add(Me.Label34)
        Me.tpMC.Controls.Add(Me.TextBox5)
        Me.tpMC.Controls.Add(Me.Label29)
        Me.tpMC.Location = New System.Drawing.Point(4, 29)
        Me.tpMC.Name = "tpMC"
        Me.tpMC.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMC.Size = New System.Drawing.Size(563, 183)
        Me.tpMC.TabIndex = 5
        Me.tpMC.Text = "Manager's Check"
        Me.tpMC.UseVisualStyleBackColor = True
        '
        'cbBankMC
        '
        Me.cbBankMC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBankMC.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbBankMC.FormattingEnabled = True
        Me.cbBankMC.Location = New System.Drawing.Point(94, 15)
        Me.cbBankMC.Name = "cbBankMC"
        Me.cbBankMC.Size = New System.Drawing.Size(210, 24)
        Me.cbBankMC.TabIndex = 128
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(47, 19)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(46, 16)
        Me.Label34.TabIndex = 130
        Me.Label34.Text = "Bank :"
        '
        'TextBox5
        '
        Me.TextBox5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox5.Location = New System.Drawing.Point(141, 133)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.ReadOnly = True
        Me.TextBox5.Size = New System.Drawing.Size(210, 22)
        Me.TextBox5.TabIndex = 123
        Me.TextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox5.Visible = False
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(51, 137)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(84, 16)
        Me.Label29.TabIndex = 124
        Me.Label29.Text = "MC Amount :"
        Me.Label29.Visible = False
        '
        'tpDebitMemo
        '
        Me.tpDebitMemo.Controls.Add(Me.cbBankDM)
        Me.tpDebitMemo.Controls.Add(Me.Label27)
        Me.tpDebitMemo.Controls.Add(Me.txtDMRef)
        Me.tpDebitMemo.Controls.Add(Me.Label28)
        Me.tpDebitMemo.Location = New System.Drawing.Point(4, 29)
        Me.tpDebitMemo.Name = "tpDebitMemo"
        Me.tpDebitMemo.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDebitMemo.Size = New System.Drawing.Size(563, 183)
        Me.tpDebitMemo.TabIndex = 4
        Me.tpDebitMemo.Text = "Debit Memo"
        Me.tpDebitMemo.UseVisualStyleBackColor = True
        '
        'cbBankDM
        '
        Me.cbBankDM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBankDM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbBankDM.FormattingEnabled = True
        Me.cbBankDM.Location = New System.Drawing.Point(94, 15)
        Me.cbBankDM.Name = "cbBankDM"
        Me.cbBankDM.Size = New System.Drawing.Size(210, 24)
        Me.cbBankDM.TabIndex = 125
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(47, 19)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(46, 16)
        Me.Label27.TabIndex = 127
        Me.Label27.Text = "Bank :"
        '
        'txtDMRef
        '
        Me.txtDMRef.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDMRef.Location = New System.Drawing.Point(94, 44)
        Me.txtDMRef.Name = "txtDMRef"
        Me.txtDMRef.ReadOnly = True
        Me.txtDMRef.Size = New System.Drawing.Size(210, 22)
        Me.txtDMRef.TabIndex = 123
        Me.txtDMRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(32, 46)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(60, 16)
        Me.Label28.TabIndex = 124
        Me.Label28.Text = "DM No. :"
        '
        'txtRFPRef
        '
        Me.txtRFPRef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRFPRef.Enabled = False
        Me.txtRFPRef.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRFPRef.Location = New System.Drawing.Point(1139, 164)
        Me.txtRFPRef.Name = "txtRFPRef"
        Me.txtRFPRef.Size = New System.Drawing.Size(132, 22)
        Me.txtRFPRef.TabIndex = 1369
        Me.txtRFPRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRFPRef.Visible = False
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label17.Location = New System.Drawing.Point(1068, 166)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(69, 16)
        Me.Label17.TabIndex = 1370
        Me.Label17.Text = "RFP Ref. :"
        Me.Label17.Visible = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label16.Location = New System.Drawing.Point(51, 17)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(77, 16)
        Me.Label16.TabIndex = 1368
        Me.Label16.Text = "VCE Code :"
        '
        'btnUOMGroup
        '
        Me.btnUOMGroup.BackgroundImage = Global.jade.My.Resources.Resources._New
        Me.btnUOMGroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnUOMGroup.Location = New System.Drawing.Point(299, 143)
        Me.btnUOMGroup.Name = "btnUOMGroup"
        Me.btnUOMGroup.Size = New System.Drawing.Size(25, 25)
        Me.btnUOMGroup.TabIndex = 7
        Me.btnUOMGroup.UseVisualStyleBackColor = True
        '
        'txtADVRef
        '
        Me.txtADVRef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtADVRef.Enabled = False
        Me.txtADVRef.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtADVRef.Location = New System.Drawing.Point(1139, 218)
        Me.txtADVRef.Name = "txtADVRef"
        Me.txtADVRef.Size = New System.Drawing.Size(132, 22)
        Me.txtADVRef.TabIndex = 20
        Me.txtADVRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtADVRef.Visible = False
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label14.Location = New System.Drawing.Point(1067, 220)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(70, 16)
        Me.Label14.TabIndex = 1366
        Me.Label14.Text = "ADV Ref. :"
        Me.Label14.Visible = False
        '
        'btnSearchVCE
        '
        Me.btnSearchVCE.BackgroundImage = Global.jade.My.Resources.Resources.view
        Me.btnSearchVCE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchVCE.Location = New System.Drawing.Point(454, 13)
        Me.btnSearchVCE.Name = "btnSearchVCE"
        Me.btnSearchVCE.Size = New System.Drawing.Size(25, 25)
        Me.btnSearchVCE.TabIndex = 2
        Me.btnSearchVCE.UseVisualStyleBackColor = True
        '
        'txtVCEName
        '
        Me.txtVCEName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVCEName.Location = New System.Drawing.Point(129, 39)
        Me.txtVCEName.Name = "txtVCEName"
        Me.txtVCEName.Size = New System.Drawing.Size(319, 22)
        Me.txtVCEName.TabIndex = 3
        '
        'txtVCECode
        '
        Me.txtVCECode.Enabled = False
        Me.txtVCECode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVCECode.Location = New System.Drawing.Point(129, 14)
        Me.txtVCECode.Name = "txtVCECode"
        Me.txtVCECode.Size = New System.Drawing.Size(319, 22)
        Me.txtVCECode.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label2.Location = New System.Drawing.Point(47, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 16)
        Me.Label2.TabIndex = 1360
        Me.Label2.Text = "VCE Name :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label3.Location = New System.Drawing.Point(31, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(16, 16)
        Me.Label3.TabIndex = 1361
        Me.Label3.Text = "  "
        '
        'txtStatus
        '
        Me.txtStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtStatus.Enabled = False
        Me.txtStatus.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(1138, 63)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(132, 22)
        Me.txtStatus.TabIndex = 18
        Me.txtStatus.Text = "Open"
        '
        'Label15
        '
        Me.Label15.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label15.Location = New System.Drawing.Point(1082, 66)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(54, 16)
        Me.Label15.TabIndex = 1355
        Me.Label15.Text = "Status :"
        '
        'txtAPVRef
        '
        Me.txtAPVRef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAPVRef.Enabled = False
        Me.txtAPVRef.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAPVRef.Location = New System.Drawing.Point(1138, 140)
        Me.txtAPVRef.Name = "txtAPVRef"
        Me.txtAPVRef.Size = New System.Drawing.Size(132, 22)
        Me.txtAPVRef.TabIndex = 19
        Me.txtAPVRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAPVRef.Visible = False
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label4.Location = New System.Drawing.Point(1066, 142)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 16)
        Me.Label4.TabIndex = 1350
        Me.Label4.Text = "APV Ref. :"
        Me.Label4.Visible = False
        '
        'txtAmount
        '
        Me.txtAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.Location = New System.Drawing.Point(129, 91)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(167, 22)
        Me.txtAmount.TabIndex = 5
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label1.Location = New System.Drawing.Point(67, 92)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 16)
        Me.Label1.TabIndex = 1348
        Me.Label1.Text = "Amount :"
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label13.Location = New System.Drawing.Point(1071, 42)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(65, 16)
        Me.Label13.TabIndex = 1346
        Me.Label13.Text = "CV Date :"
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label9.Location = New System.Drawing.Point(1078, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(58, 16)
        Me.Label9.TabIndex = 1345
        Me.Label9.Text = "CV No. :"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditEntriesToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(125, 26)
        '
        'EditEntriesToolStripMenuItem
        '
        Me.EditEntriesToolStripMenuItem.Name = "EditEntriesToolStripMenuItem"
        Me.EditEntriesToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.EditEntriesToolStripMenuItem.Text = "Edit Entry"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSearch, Me.tsbNew, Me.tsbEdit, Me.tsbCancel, Me.tsbSave, Me.tsbDelete, Me.ToolStripSeparator1, Me.tsbOption, Me.tsbCopy, Me.tsbDownload, Me.tsbUpload, Me.pgbCounter, Me.ToolStripSeparator4, Me.tsbPrint, Me.tsbReports, Me.ToolStripSeparator2, Me.tsbPrevious, Me.tsbNext, Me.ToolStripSeparator3, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1290, 40)
        Me.ToolStrip1.TabIndex = 1344
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
        'tsbOption
        '
        Me.tsbOption.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OutstandingChequeToolStripMenuItem, Me.StailedChequeToolStripMenuItem, Me.CancelCheckToolStripMenuItem})
        Me.tsbOption.ForeColor = System.Drawing.Color.White
        Me.tsbOption.Image = Global.jade.My.Resources.Resources.finance_report_infographic_512
        Me.tsbOption.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbOption.Name = "tsbOption"
        Me.tsbOption.Size = New System.Drawing.Size(57, 37)
        Me.tsbOption.Text = "Option"
        Me.tsbOption.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'OutstandingChequeToolStripMenuItem
        '
        Me.OutstandingChequeToolStripMenuItem.Name = "OutstandingChequeToolStripMenuItem"
        Me.OutstandingChequeToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.OutstandingChequeToolStripMenuItem.Text = "Released Cheque"
        '
        'StailedChequeToolStripMenuItem
        '
        Me.StailedChequeToolStripMenuItem.Name = "StailedChequeToolStripMenuItem"
        Me.StailedChequeToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.StailedChequeToolStripMenuItem.Text = "Stailed Cheque"
        Me.StailedChequeToolStripMenuItem.Visible = False
        '
        'CancelCheckToolStripMenuItem
        '
        Me.CancelCheckToolStripMenuItem.Name = "CancelCheckToolStripMenuItem"
        Me.CancelCheckToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.CancelCheckToolStripMenuItem.Text = "Cancel Cheque"
        '
        'tsbCopy
        '
        Me.tsbCopy.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbCopyAPV, Me.tsbCopyADV, Me.FromRFPToolStripMenuItem, Me.FromLoanToolStripMenuItem, Me.FromFundsToolStripMenuItem, Me.FromCAToolStripMenuItem, Me.FromPCVToolStripMenuItem, Me.FromPTToolStripMenuItem, Me.FromCALiquidationToolStripMenuItem, Me.FromRRToolStripMenuItem, Me.FromATDToolStripMenuItem, Me.FromCSFeeToolStripMenuItem, Me.FromPJToolStripMenuItem, Me.FromBookingToolStripMenuItem, Me.FromTerminalPayToolStripMenuItem, Me.FromRealEstateCommissionToolStripMenuItem})
        Me.tsbCopy.ForeColor = System.Drawing.Color.White
        Me.tsbCopy.Image = Global.jade.My.Resources.Resources.finance_report_infographic_512
        Me.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbCopy.Name = "tsbCopy"
        Me.tsbCopy.Size = New System.Drawing.Size(48, 37)
        Me.tsbCopy.Text = "Copy"
        Me.tsbCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbCopyAPV
        '
        Me.tsbCopyAPV.Name = "tsbCopyAPV"
        Me.tsbCopyAPV.Size = New System.Drawing.Size(231, 22)
        Me.tsbCopyAPV.Text = "From Account Payable"
        '
        'tsbCopyADV
        '
        Me.tsbCopyADV.Name = "tsbCopyADV"
        Me.tsbCopyADV.Size = New System.Drawing.Size(231, 22)
        Me.tsbCopyADV.Text = "From Advances"
        '
        'FromRFPToolStripMenuItem
        '
        Me.FromRFPToolStripMenuItem.Name = "FromRFPToolStripMenuItem"
        Me.FromRFPToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.FromRFPToolStripMenuItem.Text = "From RFP"
        Me.FromRFPToolStripMenuItem.Visible = False
        '
        'FromLoanToolStripMenuItem
        '
        Me.FromLoanToolStripMenuItem.Name = "FromLoanToolStripMenuItem"
        Me.FromLoanToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.FromLoanToolStripMenuItem.Text = "From Loans"
        '
        'FromFundsToolStripMenuItem
        '
        Me.FromFundsToolStripMenuItem.Name = "FromFundsToolStripMenuItem"
        Me.FromFundsToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.FromFundsToolStripMenuItem.Text = "From Funds"
        Me.FromFundsToolStripMenuItem.Visible = False
        '
        'FromCAToolStripMenuItem
        '
        Me.FromCAToolStripMenuItem.Name = "FromCAToolStripMenuItem"
        Me.FromCAToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.FromCAToolStripMenuItem.Text = "From Cash Advance"
        '
        'FromPCVToolStripMenuItem
        '
        Me.FromPCVToolStripMenuItem.Name = "FromPCVToolStripMenuItem"
        Me.FromPCVToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.FromPCVToolStripMenuItem.Text = "From PCFRR"
        '
        'FromPTToolStripMenuItem
        '
        Me.FromPTToolStripMenuItem.Name = "FromPTToolStripMenuItem"
        Me.FromPTToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.FromPTToolStripMenuItem.Text = "From PT"
        Me.FromPTToolStripMenuItem.Visible = False
        '
        'FromCALiquidationToolStripMenuItem
        '
        Me.FromCALiquidationToolStripMenuItem.Name = "FromCALiquidationToolStripMenuItem"
        Me.FromCALiquidationToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.FromCALiquidationToolStripMenuItem.Text = "From CA-Liquidation"
        '
        'FromRRToolStripMenuItem
        '
        Me.FromRRToolStripMenuItem.Name = "FromRRToolStripMenuItem"
        Me.FromRRToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.FromRRToolStripMenuItem.Text = "From Receiving Report"
        '
        'FromATDToolStripMenuItem
        '
        Me.FromATDToolStripMenuItem.Name = "FromATDToolStripMenuItem"
        Me.FromATDToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.FromATDToolStripMenuItem.Text = "From ATD"
        '
        'FromCSFeeToolStripMenuItem
        '
        Me.FromCSFeeToolStripMenuItem.Name = "FromCSFeeToolStripMenuItem"
        Me.FromCSFeeToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.FromCSFeeToolStripMenuItem.Text = "From CS Fee"
        '
        'FromPJToolStripMenuItem
        '
        Me.FromPJToolStripMenuItem.Name = "FromPJToolStripMenuItem"
        Me.FromPJToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.FromPJToolStripMenuItem.Text = "From Purchase Journal"
        '
        'FromBookingToolStripMenuItem
        '
        Me.FromBookingToolStripMenuItem.Name = "FromBookingToolStripMenuItem"
        Me.FromBookingToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.FromBookingToolStripMenuItem.Text = "From Booking"
        '
        'FromTerminalPayToolStripMenuItem
        '
        Me.FromTerminalPayToolStripMenuItem.Name = "FromTerminalPayToolStripMenuItem"
        Me.FromTerminalPayToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.FromTerminalPayToolStripMenuItem.Text = "From Terminal Pay"
        '
        'FromRealEstateCommissionToolStripMenuItem
        '
        Me.FromRealEstateCommissionToolStripMenuItem.Name = "FromRealEstateCommissionToolStripMenuItem"
        Me.FromRealEstateCommissionToolStripMenuItem.Size = New System.Drawing.Size(231, 22)
        Me.FromRealEstateCommissionToolStripMenuItem.Text = "From Real Estate Commission"
        '
        'tsbDownload
        '
        Me.tsbDownload.AutoSize = False
        Me.tsbDownload.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.tsbDownload.ForeColor = System.Drawing.Color.White
        Me.tsbDownload.Image = Global.jade.My.Resources.Resources.arrow_upload_icon
        Me.tsbDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbDownload.Name = "tsbDownload"
        Me.tsbDownload.Size = New System.Drawing.Size(65, 35)
        Me.tsbDownload.Text = "Download"
        Me.tsbDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbUpload
        '
        Me.tsbUpload.AutoSize = False
        Me.tsbUpload.ForeColor = System.Drawing.Color.White
        Me.tsbUpload.Image = Global.jade.My.Resources.Resources.arrow_upload_icon
        Me.tsbUpload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbUpload.Name = "tsbUpload"
        Me.tsbUpload.Size = New System.Drawing.Size(50, 35)
        Me.tsbUpload.Text = "Upload"
        Me.tsbUpload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pgbCounter
        '
        Me.pgbCounter.AutoSize = False
        Me.pgbCounter.Name = "pgbCounter"
        Me.pgbCounter.Size = New System.Drawing.Size(100, 25)
        Me.pgbCounter.Visible = False
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 40)
        '
        'tsbPrint
        '
        Me.tsbPrint.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintCVToolStripMenuItem, Me.ChequieToolStripMenuItem, Me.BIR2307ToolStripMenuItem, Me.ConvertCheque})
        Me.tsbPrint.ForeColor = System.Drawing.Color.White
        Me.tsbPrint.Image = Global.jade.My.Resources.Resources.printer_circle_blue_512
        Me.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPrint.Name = "tsbPrint"
        Me.tsbPrint.Size = New System.Drawing.Size(48, 37)
        Me.tsbPrint.Text = "Print"
        Me.tsbPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'PrintCVToolStripMenuItem
        '
        Me.PrintCVToolStripMenuItem.Name = "PrintCVToolStripMenuItem"
        Me.PrintCVToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.PrintCVToolStripMenuItem.Text = "Check Voucher"
        '
        'ChequieToolStripMenuItem
        '
        Me.ChequieToolStripMenuItem.Name = "ChequieToolStripMenuItem"
        Me.ChequieToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ChequieToolStripMenuItem.Text = "Check"
        '
        'BIR2307ToolStripMenuItem
        '
        Me.BIR2307ToolStripMenuItem.Name = "BIR2307ToolStripMenuItem"
        Me.BIR2307ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.BIR2307ToolStripMenuItem.Text = "BIR 2307"
        '
        'ConvertCheque
        '
        Me.ConvertCheque.Name = "ConvertCheque"
        Me.ConvertCheque.Size = New System.Drawing.Size(180, 22)
        Me.ConvertCheque.Text = "Convert Check"
        '
        'tsbReports
        '
        Me.tsbReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CVListToolStripMenuItem})
        Me.tsbReports.ForeColor = System.Drawing.Color.White
        Me.tsbReports.Image = Global.jade.My.Resources.Resources.finance_report_infographic_512
        Me.tsbReports.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbReports.Name = "tsbReports"
        Me.tsbReports.Size = New System.Drawing.Size(60, 37)
        Me.tsbReports.Text = "Reports"
        Me.tsbReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'CVListToolStripMenuItem
        '
        Me.CVListToolStripMenuItem.Name = "CVListToolStripMenuItem"
        Me.CVListToolStripMenuItem.Size = New System.Drawing.Size(110, 22)
        Me.CVListToolStripMenuItem.Text = "CV List"
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
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(8, 328)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1276, 207)
        Me.TabControl1.TabIndex = 1345
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgvEntry)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Size = New System.Drawing.Size(1268, 179)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Entries"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'bgwUpload
        '
        Me.bgwUpload.WorkerReportsProgress = True
        Me.bgwUpload.WorkerSupportsCancellation = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmCV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(1290, 561)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.txtTotalDebit)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtTotalCredit)
        Me.Controls.Add(Me.gbPayee)
        Me.Controls.Add(Me.Label20)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmCV"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " Check Voucher"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgvEntry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbPayee.ResumeLayout(False)
        Me.gbPayee.PerformLayout()
        Me.tcPayment.ResumeLayout(False)
        Me.tpCheck.ResumeLayout(False)
        Me.gbBank.ResumeLayout(False)
        Me.gbBank.PerformLayout()
        Me.tpMultipleCheck.ResumeLayout(False)
        CType(Me.dgvMultipleCheck, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpBankTransfer.ResumeLayout(False)
        Me.tpBankTransfer.PerformLayout()
        Me.tpMC.ResumeLayout(False)
        Me.tpMC.PerformLayout()
        Me.tpDebitMemo.ResumeLayout(False)
        Me.tpDebitMemo.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtBankRef As System.Windows.Forms.TextBox
    Friend WithEvents txtTransNum As System.Windows.Forms.TextBox
    Friend WithEvents txtBankRefAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtTotalDebit As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalCredit As System.Windows.Forms.TextBox
    Friend WithEvents dtpDocDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtpBankRefDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cbPaymentType As System.Windows.Forms.ComboBox
    Friend WithEvents dgvEntry As System.Windows.Forms.DataGridView
    Friend WithEvents cbBank As System.Windows.Forms.ComboBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cbDisburseType As System.Windows.Forms.ComboBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtORNo As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents gbPayee As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents gbBank As System.Windows.Forms.GroupBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAPVRef As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EditEntriesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbCopy As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents tsbCopyAPV As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbReports As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbPrevious As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNext As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSearchVCE As System.Windows.Forms.Button
    Friend WithEvents txtVCEName As System.Windows.Forms.TextBox
    Friend WithEvents txtVCECode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tsbCopyADV As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbPrint As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents PrintCVToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChequieToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BIR2307ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtRefStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tsbOption As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents CancelCheckToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtADVRef As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnUOMGroup As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents FromRFPToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtRFPRef As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents FromLoanToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tcPayment As System.Windows.Forms.TabControl
    Friend WithEvents tpCheck As System.Windows.Forms.TabPage
    Friend WithEvents tpMultipleCheck As System.Windows.Forms.TabPage
    Friend WithEvents dgvMultipleCheck As System.Windows.Forms.DataGridView
    Friend WithEvents tpBankTransfer As System.Windows.Forms.TabPage
    Friend WithEvents tpDebitMemo As System.Windows.Forms.TabPage
    Friend WithEvents tpMC As System.Windows.Forms.TabPage
    Friend WithEvents txtBankTransfer_Ref As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents dtpBankTransfer_Date As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtBankTransfer_Amount As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents cbBankTransfer_Bank As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtDMRef As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtBankRefName As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtLoanRef As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents tsbDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents FromFundsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgcBankID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcBank As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents dgcCheckNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcCheckDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcCheckVCECode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcCheckName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcStatus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents FromPCVToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FromCAToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtCARef As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents btnTax As System.Windows.Forms.Button
    Friend WithEvents FromPTToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtBankTransfer_VCEBankAccount As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtBankTransfer_VCEBankName As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents cbBankDM As System.Windows.Forms.ComboBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents tsbUpload As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents bgwUpload As System.ComponentModel.BackgroundWorker
    Friend WithEvents pgbCounter As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cbBankMC As System.Windows.Forms.ComboBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents CVListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FromCALiquidationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cbCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents txtConversion As System.Windows.Forms.TextBox
    Friend WithEvents lblConversion As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents FromRRToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtRRRef As System.Windows.Forms.TextBox
    Friend WithEvents FromATDToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chAccntCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chAccntTitle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chDebit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chCredit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chVATType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chATCCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chCompute As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents chVCECode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chVCEName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chParticulars As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chRef As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chCostID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chCost_Center As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chProfit_Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chProfit_Center As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcBranchCode As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Column12 As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents chRecordID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FromCSFeeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FromPJToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OutstandingChequeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StailedChequeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FromBookingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FromTerminalPayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FromRealEstateCommissionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label37 As Label
    Friend WithEvents cbCostCenter As ComboBox
    Friend WithEvents ConvertCheque As ToolStripMenuItem
End Class
