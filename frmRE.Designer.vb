<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRE
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRE))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.txtSJNo = New System.Windows.Forms.TextBox()
        Me.btnViewLedger = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtNetSelling = New System.Windows.Forms.TextBox()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.txtSalesName = New System.Windows.Forms.TextBox()
        Me.txtSalesCode = New System.Windows.Forms.TextBox()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.txtCommission = New System.Windows.Forms.TextBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.nupCommission = New System.Windows.Forms.NumericUpDown()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.tcPayment = New System.Windows.Forms.TabControl()
        Me.tpCash = New System.Windows.Forms.TabPage()
        Me.nupWithInDays = New System.Windows.Forms.NumericUpDown()
        Me.lblCashPayableUntil = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.rbTCPWithin = New System.Windows.Forms.RadioButton()
        Me.rbTCPSpot = New System.Windows.Forms.RadioButton()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtTCPReserve = New System.Windows.Forms.TextBox()
        Me.nupTCPDiscount = New System.Windows.Forms.NumericUpDown()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtTCPBalance = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtTCPDiscount = New System.Windows.Forms.TextBox()
        Me.tpDeferred = New System.Windows.Forms.TabPage()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.dgvDeferredSched = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.dtpDeferredEnd = New System.Windows.Forms.DateTimePicker()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtDeferredMonthly = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtDeferredReserve = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.nupDeferredTerms = New System.Windows.Forms.NumericUpDown()
        Me.dtpDeferredStart = New System.Windows.Forms.DateTimePicker()
        Me.tpFinancing = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblDPPayableUntil = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.nupDPDays = New System.Windows.Forms.NumericUpDown()
        Me.rbTerm = New System.Windows.Forms.RadioButton()
        Me.rbCashWithin = New System.Windows.Forms.RadioButton()
        Me.rbSpotDp = New System.Windows.Forms.RadioButton()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.nupTerm = New System.Windows.Forms.NumericUpDown()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.dgvDPSchedule = New System.Windows.Forms.DataGridView()
        Me.chCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chDateDue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtBalanceDP = New System.Windows.Forms.TextBox()
        Me.dtpStart = New System.Windows.Forms.DateTimePicker()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.nupDiscountRate = New System.Windows.Forms.NumericUpDown()
        Me.txtMonthly = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtDiscount = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtReserve = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tcFinancing = New System.Windows.Forms.TabControl()
        Me.tpInhouse = New System.Windows.Forms.TabPage()
        Me.lblInhouseUntil = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.dtpInhouseStart = New System.Windows.Forms.DateTimePicker()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.dgvInhouse = New System.Windows.Forms.DataGridView()
        Me.dgcInHouseNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcInHouseDueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcInhouseInterest = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcInHousePrincipal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcInHouseTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nupInhouseTotalMonths = New System.Windows.Forms.NumericUpDown()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtInhouseMonthly = New System.Windows.Forms.TextBox()
        Me.nupInhousePaymentPerYear = New System.Windows.Forms.NumericUpDown()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.nupInhouseYear = New System.Windows.Forms.NumericUpDown()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.nupInhouseRate = New System.Windows.Forms.NumericUpDown()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.tpPagibig = New System.Windows.Forms.TabPage()
        Me.lblPagibigUntil = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.dgvPagibig = New System.Windows.Forms.DataGridView()
        Me.dgcPagibigPaymentNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcPagibigDuedate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcPagibigAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.lvPagibig = New System.Windows.Forms.ListView()
        Me.chPagibigTerms = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chPagibigAmort = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chPagibigNDI = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.nupPagibigRate = New System.Windows.Forms.NumericUpDown()
        Me.nupPagibigTerms = New System.Windows.Forms.NumericUpDown()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.dtpPagibigStart = New System.Windows.Forms.DateTimePicker()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.txtPagibigMonthly = New System.Windows.Forms.TextBox()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.txtPagibiLoanDiff = New System.Windows.Forms.TextBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.txtPagibigLoanable = New System.Windows.Forms.TextBox()
        Me.tpBank = New System.Windows.Forms.TabPage()
        Me.lblBankUntil = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.dgvBank = New System.Windows.Forms.DataGridView()
        Me.dgcBankPaymentNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBankDuedate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBankAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nupBankTerms = New System.Windows.Forms.NumericUpDown()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.dtpBankStart = New System.Windows.Forms.DateTimePicker()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.txtBankMonthly = New System.Windows.Forms.TextBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.txtBankLoanDiff = New System.Windows.Forms.TextBox()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.txtBankLoanable = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.lvBankComputation = New System.Windows.Forms.ListView()
        Me.chTerm = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chAmortization = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chNDI = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.nupBankRate = New System.Windows.Forms.NumericUpDown()
        Me.miscFeeRate = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbFinancingMode = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtLoanable = New System.Windows.Forms.TextBox()
        Me.nupDPRate = New System.Windows.Forms.NumericUpDown()
        Me.txtTotalDP = New System.Windows.Forms.TextBox()
        Me.tpAccountingEntries = New System.Windows.Forms.TabPage()
        Me.dgvEntry = New System.Windows.Forms.DataGridView()
        Me.chAccntCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chAccntTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chDebit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chCredit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chParticulars = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chVCECode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chVCEName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chRefNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtTotalCredit = New System.Windows.Forms.TextBox()
        Me.txtTotalDebit = New System.Windows.Forms.TextBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.txtSellingCommission = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtSelling = New System.Windows.Forms.TextBox()
        Me.cbPaymentType = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtMisc = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtVAT = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtTCP = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtAddl = New System.Windows.Forms.TextBox()
        Me.chkVATInc = New System.Windows.Forms.CheckBox()
        Me.chkVATable = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.btnSearchProp = New System.Windows.Forms.Button()
        Me.btnLPM = New System.Windows.Forms.Button()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.dtpDocDate = New System.Windows.Forms.DateTimePicker()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btnVCE = New System.Windows.Forms.Button()
        Me.btnSearchVCE = New System.Windows.Forms.Button()
        Me.txtVCEName = New System.Windows.Forms.TextBox()
        Me.txtVCECode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.txtTransNum = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtPropName = New System.Windows.Forms.TextBox()
        Me.txtPropCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbSearch = New System.Windows.Forms.ToolStripButton()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrint = New System.Windows.Forms.ToolStripSplitButton()
        Me.ReservationAgreementToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContractToSellToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.WaiverToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuyersInformationSheetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreditApprovalMemorandumFormToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ListOfRequirementsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.NoticeOfDisapprovalForCAMToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NoticeOfUnpaidEquityToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NoticeOfNonComplianceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NoticeOfCancellationForDocumentationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsbReports = New System.Windows.Forms.ToolStripDropDownButton()
        Me.AccountMonitoringReservedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ListOfAvailableUnitsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InventoryPropertyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrevious = New System.Windows.Forms.ToolStripButton()
        Me.tsbNext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSplitButton1 = New System.Windows.Forms.ToolStripSplitButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.nupCommission, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tcPayment.SuspendLayout()
        Me.tpCash.SuspendLayout()
        CType(Me.nupWithInDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nupTCPDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpDeferred.SuspendLayout()
        CType(Me.dgvDeferredSched, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nupDeferredTerms, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpFinancing.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.nupDPDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nupTerm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDPSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nupDiscountRate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tcFinancing.SuspendLayout()
        Me.tpInhouse.SuspendLayout()
        CType(Me.dgvInhouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nupInhouseTotalMonths, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nupInhousePaymentPerYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nupInhouseYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nupInhouseRate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpPagibig.SuspendLayout()
        CType(Me.dgvPagibig, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.nupPagibigRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nupPagibigTerms, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpBank.SuspendLayout()
        CType(Me.dgvBank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nupBankTerms, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.nupBankRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nupDPRate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpAccountingEntries.SuspendLayout()
        CType(Me.dgvEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label67)
        Me.GroupBox1.Controls.Add(Me.txtSJNo)
        Me.GroupBox1.Controls.Add(Me.btnViewLedger)
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.tcPayment)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.btnSearchProp)
        Me.GroupBox1.Controls.Add(Me.btnLPM)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.dtpDocDate)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.btnVCE)
        Me.GroupBox1.Controls.Add(Me.btnSearchVCE)
        Me.GroupBox1.Controls.Add(Me.txtVCEName)
        Me.GroupBox1.Controls.Add(Me.txtVCECode)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtStatus)
        Me.GroupBox1.Controls.Add(Me.txtTransNum)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtPropName)
        Me.GroupBox1.Controls.Add(Me.txtPropCode)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 43)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1295, 634)
        Me.GroupBox1.TabIndex = 1319
        Me.GroupBox1.TabStop = False
        '
        'Label67
        '
        Me.Label67.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label67.AutoSize = True
        Me.Label67.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.Location = New System.Drawing.Point(1087, 70)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(55, 16)
        Me.Label67.TabIndex = 1597
        Me.Label67.Text = "SJ No. :"
        '
        'txtSJNo
        '
        Me.txtSJNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSJNo.Enabled = False
        Me.txtSJNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSJNo.Location = New System.Drawing.Point(1144, 68)
        Me.txtSJNo.Name = "txtSJNo"
        Me.txtSJNo.Size = New System.Drawing.Size(132, 22)
        Me.txtSJNo.TabIndex = 1596
        Me.txtSJNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnViewLedger
        '
        Me.btnViewLedger.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewLedger.Location = New System.Drawing.Point(472, 97)
        Me.btnViewLedger.Name = "btnViewLedger"
        Me.btnViewLedger.Size = New System.Drawing.Size(482, 28)
        Me.btnViewLedger.TabIndex = 1595
        Me.btnViewLedger.Text = "View Ledger"
        Me.btnViewLedger.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtNetSelling)
        Me.GroupBox5.Controls.Add(Me.Label52)
        Me.GroupBox5.Controls.Add(Me.Label51)
        Me.GroupBox5.Controls.Add(Me.Label49)
        Me.GroupBox5.Controls.Add(Me.Button1)
        Me.GroupBox5.Controls.Add(Me.Button2)
        Me.GroupBox5.Controls.Add(Me.txtSalesName)
        Me.GroupBox5.Controls.Add(Me.txtSalesCode)
        Me.GroupBox5.Controls.Add(Me.Label50)
        Me.GroupBox5.Controls.Add(Me.txtCommission)
        Me.GroupBox5.Controls.Add(Me.Label48)
        Me.GroupBox5.Controls.Add(Me.nupCommission)
        Me.GroupBox5.Controls.Add(Me.Label47)
        Me.GroupBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(16, 371)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(331, 237)
        Me.GroupBox5.TabIndex = 1594
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Commission"
        Me.GroupBox5.Visible = False
        '
        'txtNetSelling
        '
        Me.txtNetSelling.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetSelling.Location = New System.Drawing.Point(158, 126)
        Me.txtNetSelling.Name = "txtNetSelling"
        Me.txtNetSelling.ReadOnly = True
        Me.txtNetSelling.Size = New System.Drawing.Size(151, 22)
        Me.txtNetSelling.TabIndex = 1622
        Me.txtNetSelling.Text = "0.00"
        Me.txtNetSelling.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label52.Location = New System.Drawing.Point(39, 129)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(113, 16)
        Me.Label52.TabIndex = 1621
        Me.Label52.Text = "Net Selling Price :"
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label51.Location = New System.Drawing.Point(6, 70)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(51, 16)
        Me.Label51.TabIndex = 1620
        Me.Label51.Text = "Name :"
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label49.Location = New System.Drawing.Point(10, 45)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(47, 16)
        Me.Label49.TabIndex = 1619
        Me.Label49.Text = "Code :"
        '
        'Button1
        '
        Me.Button1.BackgroundImage = Global.jade.My.Resources.Resources.report_icon
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Location = New System.Drawing.Point(284, 66)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(25, 25)
        Me.Button1.TabIndex = 1618
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.BackgroundImage = Global.jade.My.Resources.Resources.view
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button2.Location = New System.Drawing.Point(284, 41)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(25, 25)
        Me.Button2.TabIndex = 1617
        Me.Button2.UseVisualStyleBackColor = True
        '
        'txtSalesName
        '
        Me.txtSalesName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesName.Location = New System.Drawing.Point(59, 68)
        Me.txtSalesName.Name = "txtSalesName"
        Me.txtSalesName.Size = New System.Drawing.Size(220, 22)
        Me.txtSalesName.TabIndex = 1616
        '
        'txtSalesCode
        '
        Me.txtSalesCode.Enabled = False
        Me.txtSalesCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesCode.Location = New System.Drawing.Point(59, 43)
        Me.txtSalesCode.Name = "txtSalesCode"
        Me.txtSalesCode.Size = New System.Drawing.Size(220, 22)
        Me.txtSalesCode.TabIndex = 1615
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label50.Location = New System.Drawing.Point(16, 24)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(125, 16)
        Me.Label50.TabIndex = 1614
        Me.Label50.Text = "Sales Agent/Broker"
        '
        'txtCommission
        '
        Me.txtCommission.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCommission.Location = New System.Drawing.Point(158, 175)
        Me.txtCommission.Name = "txtCommission"
        Me.txtCommission.ReadOnly = True
        Me.txtCommission.Size = New System.Drawing.Size(151, 22)
        Me.txtCommission.TabIndex = 1612
        Me.txtCommission.Text = "0.00"
        Me.txtCommission.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label48.Location = New System.Drawing.Point(30, 178)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(122, 16)
        Me.Label48.TabIndex = 1611
        Me.Label48.Text = "Total Commission :"
        '
        'nupCommission
        '
        Me.nupCommission.DecimalPlaces = 2
        Me.nupCommission.InterceptArrowKeys = False
        Me.nupCommission.Location = New System.Drawing.Point(158, 151)
        Me.nupCommission.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.nupCommission.Name = "nupCommission"
        Me.nupCommission.Size = New System.Drawing.Size(101, 22)
        Me.nupCommission.TabIndex = 1597
        Me.nupCommission.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nupCommission.ThousandsSeparator = True
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label47.Location = New System.Drawing.Point(9, 153)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(143, 16)
        Me.Label47.TabIndex = 1596
        Me.Label47.Text = "Commission Rate (%) :"
        '
        'tcPayment
        '
        Me.tcPayment.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcPayment.Controls.Add(Me.tpCash)
        Me.tcPayment.Controls.Add(Me.tpDeferred)
        Me.tcPayment.Controls.Add(Me.tpFinancing)
        Me.tcPayment.Controls.Add(Me.tpAccountingEntries)
        Me.tcPayment.Location = New System.Drawing.Point(358, 131)
        Me.tcPayment.Name = "tcPayment"
        Me.tcPayment.SelectedIndex = 0
        Me.tcPayment.Size = New System.Drawing.Size(931, 504)
        Me.tcPayment.TabIndex = 1593
        '
        'tpCash
        '
        Me.tpCash.Controls.Add(Me.nupWithInDays)
        Me.tpCash.Controls.Add(Me.lblCashPayableUntil)
        Me.tpCash.Controls.Add(Me.Label35)
        Me.tpCash.Controls.Add(Me.Label33)
        Me.tpCash.Controls.Add(Me.rbTCPWithin)
        Me.tpCash.Controls.Add(Me.rbTCPSpot)
        Me.tpCash.Controls.Add(Me.Label34)
        Me.tpCash.Controls.Add(Me.txtTCPReserve)
        Me.tpCash.Controls.Add(Me.nupTCPDiscount)
        Me.tpCash.Controls.Add(Me.Label30)
        Me.tpCash.Controls.Add(Me.Label31)
        Me.tpCash.Controls.Add(Me.txtTCPBalance)
        Me.tpCash.Controls.Add(Me.Label32)
        Me.tpCash.Controls.Add(Me.txtTCPDiscount)
        Me.tpCash.Location = New System.Drawing.Point(4, 24)
        Me.tpCash.Name = "tpCash"
        Me.tpCash.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCash.Size = New System.Drawing.Size(923, 476)
        Me.tpCash.TabIndex = 3
        Me.tpCash.Text = "Cash Sales"
        Me.tpCash.UseVisualStyleBackColor = True
        '
        'nupWithInDays
        '
        Me.nupWithInDays.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.nupWithInDays.Location = New System.Drawing.Point(199, 14)
        Me.nupWithInDays.Name = "nupWithInDays"
        Me.nupWithInDays.Size = New System.Drawing.Size(55, 22)
        Me.nupWithInDays.TabIndex = 1605
        Me.nupWithInDays.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblCashPayableUntil
        '
        Me.lblCashPayableUntil.AutoSize = True
        Me.lblCashPayableUntil.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCashPayableUntil.Location = New System.Drawing.Point(139, 151)
        Me.lblCashPayableUntil.Name = "lblCashPayableUntil"
        Me.lblCashPayableUntil.Size = New System.Drawing.Size(93, 16)
        Me.lblCashPayableUntil.TabIndex = 1604
        Me.lblCashPayableUntil.Text = "Payable Until :"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(40, 151)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(93, 16)
        Me.Label35.TabIndex = 1603
        Me.Label35.Text = "Payable Until :"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label33.Location = New System.Drawing.Point(260, 18)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(40, 16)
        Me.Label33.TabIndex = 1602
        Me.Label33.Text = "Days"
        '
        'rbTCPWithin
        '
        Me.rbTCPWithin.AutoSize = True
        Me.rbTCPWithin.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.rbTCPWithin.Location = New System.Drawing.Point(131, 16)
        Me.rbTCPWithin.Name = "rbTCPWithin"
        Me.rbTCPWithin.Size = New System.Drawing.Size(62, 20)
        Me.rbTCPWithin.TabIndex = 1600
        Me.rbTCPWithin.Text = "Within"
        Me.rbTCPWithin.UseVisualStyleBackColor = True
        '
        'rbTCPSpot
        '
        Me.rbTCPSpot.AutoSize = True
        Me.rbTCPSpot.Checked = True
        Me.rbTCPSpot.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.rbTCPSpot.Location = New System.Drawing.Point(33, 16)
        Me.rbTCPSpot.Name = "rbTCPSpot"
        Me.rbTCPSpot.Size = New System.Drawing.Size(84, 20)
        Me.rbTCPSpot.TabIndex = 1599
        Me.rbTCPSpot.TabStop = True
        Me.rbTCPSpot.Text = "Spot TCP"
        Me.rbTCPSpot.UseVisualStyleBackColor = True
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label34.Location = New System.Drawing.Point(47, 52)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(87, 16)
        Me.Label34.TabIndex = 1597
        Me.Label34.Text = "Reservation :"
        '
        'txtTCPReserve
        '
        Me.txtTCPReserve.Enabled = False
        Me.txtTCPReserve.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTCPReserve.Location = New System.Drawing.Point(139, 49)
        Me.txtTCPReserve.Name = "txtTCPReserve"
        Me.txtTCPReserve.Size = New System.Drawing.Size(151, 22)
        Me.txtTCPReserve.TabIndex = 1598
        Me.txtTCPReserve.Text = "0.00"
        Me.txtTCPReserve.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'nupTCPDiscount
        '
        Me.nupTCPDiscount.DecimalPlaces = 2
        Me.nupTCPDiscount.InterceptArrowKeys = False
        Me.nupTCPDiscount.Location = New System.Drawing.Point(139, 73)
        Me.nupTCPDiscount.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.nupTCPDiscount.Name = "nupTCPDiscount"
        Me.nupTCPDiscount.Size = New System.Drawing.Size(151, 21)
        Me.nupTCPDiscount.TabIndex = 1594
        Me.nupTCPDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nupTCPDiscount.ThousandsSeparator = True
        Me.nupTCPDiscount.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label30.Location = New System.Drawing.Point(44, 75)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(89, 16)
        Me.Label30.TabIndex = 1593
        Me.Label30.Text = "Discount (%) :"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label31.Location = New System.Drawing.Point(70, 127)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(64, 16)
        Me.Label31.TabIndex = 1591
        Me.Label31.Text = "Balance :"
        '
        'txtTCPBalance
        '
        Me.txtTCPBalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTCPBalance.Location = New System.Drawing.Point(139, 122)
        Me.txtTCPBalance.Name = "txtTCPBalance"
        Me.txtTCPBalance.ReadOnly = True
        Me.txtTCPBalance.Size = New System.Drawing.Size(151, 22)
        Me.txtTCPBalance.TabIndex = 1592
        Me.txtTCPBalance.Text = "0.00"
        Me.txtTCPBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label32.Location = New System.Drawing.Point(68, 100)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(66, 16)
        Me.Label32.TabIndex = 1589
        Me.Label32.Text = "Discount :"
        '
        'txtTCPDiscount
        '
        Me.txtTCPDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTCPDiscount.Location = New System.Drawing.Point(139, 97)
        Me.txtTCPDiscount.Name = "txtTCPDiscount"
        Me.txtTCPDiscount.Size = New System.Drawing.Size(151, 22)
        Me.txtTCPDiscount.TabIndex = 1590
        Me.txtTCPDiscount.Text = "0.00"
        Me.txtTCPDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpDeferred
        '
        Me.tpDeferred.Controls.Add(Me.Label43)
        Me.tpDeferred.Controls.Add(Me.dgvDeferredSched)
        Me.tpDeferred.Controls.Add(Me.Label39)
        Me.tpDeferred.Controls.Add(Me.dtpDeferredEnd)
        Me.tpDeferred.Controls.Add(Me.Label38)
        Me.tpDeferred.Controls.Add(Me.txtDeferredMonthly)
        Me.tpDeferred.Controls.Add(Me.Label20)
        Me.tpDeferred.Controls.Add(Me.txtDeferredReserve)
        Me.tpDeferred.Controls.Add(Me.Label36)
        Me.tpDeferred.Controls.Add(Me.Label37)
        Me.tpDeferred.Controls.Add(Me.nupDeferredTerms)
        Me.tpDeferred.Controls.Add(Me.dtpDeferredStart)
        Me.tpDeferred.Location = New System.Drawing.Point(4, 24)
        Me.tpDeferred.Name = "tpDeferred"
        Me.tpDeferred.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDeferred.Size = New System.Drawing.Size(923, 476)
        Me.tpDeferred.TabIndex = 4
        Me.tpDeferred.Text = "Deferred Cash"
        Me.tpDeferred.UseVisualStyleBackColor = True
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label43.Location = New System.Drawing.Point(290, 14)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(127, 16)
        Me.Label43.TabIndex = 1581
        Me.Label43.Text = "Payment Schedule :"
        '
        'dgvDeferredSched
        '
        Me.dgvDeferredSched.AllowUserToAddRows = False
        Me.dgvDeferredSched.AllowUserToDeleteRows = False
        Me.dgvDeferredSched.AllowUserToResizeColumns = False
        Me.dgvDeferredSched.AllowUserToResizeRows = False
        Me.dgvDeferredSched.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDeferredSched.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDeferredSched.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3})
        Me.dgvDeferredSched.Location = New System.Drawing.Point(293, 37)
        Me.dgvDeferredSched.Name = "dgvDeferredSched"
        Me.dgvDeferredSched.ReadOnly = True
        Me.dgvDeferredSched.RowHeadersVisible = False
        Me.dgvDeferredSched.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDeferredSched.Size = New System.Drawing.Size(621, 416)
        Me.dgvDeferredSched.TabIndex = 1580
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "No."
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 50
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Date Due"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 200
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Amount"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 200
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(24, 140)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(94, 16)
        Me.Label39.TabIndex = 1579
        Me.Label39.Text = "Payment End :"
        '
        'dtpDeferredEnd
        '
        Me.dtpDeferredEnd.Enabled = False
        Me.dtpDeferredEnd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDeferredEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDeferredEnd.Location = New System.Drawing.Point(125, 136)
        Me.dtpDeferredEnd.Name = "dtpDeferredEnd"
        Me.dtpDeferredEnd.Size = New System.Drawing.Size(151, 22)
        Me.dtpDeferredEnd.TabIndex = 1578
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label38.Location = New System.Drawing.Point(22, 90)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(100, 16)
        Me.Label38.TabIndex = 1576
        Me.Label38.Text = "Monthly Equity :"
        '
        'txtDeferredMonthly
        '
        Me.txtDeferredMonthly.Enabled = False
        Me.txtDeferredMonthly.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeferredMonthly.Location = New System.Drawing.Point(125, 87)
        Me.txtDeferredMonthly.Name = "txtDeferredMonthly"
        Me.txtDeferredMonthly.ReadOnly = True
        Me.txtDeferredMonthly.Size = New System.Drawing.Size(151, 22)
        Me.txtDeferredMonthly.TabIndex = 1577
        Me.txtDeferredMonthly.Text = "0.00"
        Me.txtDeferredMonthly.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label20.Location = New System.Drawing.Point(35, 40)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(87, 16)
        Me.Label20.TabIndex = 1574
        Me.Label20.Text = "Reservation :"
        '
        'txtDeferredReserve
        '
        Me.txtDeferredReserve.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeferredReserve.Location = New System.Drawing.Point(125, 37)
        Me.txtDeferredReserve.Name = "txtDeferredReserve"
        Me.txtDeferredReserve.Size = New System.Drawing.Size(151, 22)
        Me.txtDeferredReserve.TabIndex = 1575
        Me.txtDeferredReserve.Text = "0.00"
        Me.txtDeferredReserve.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(22, 116)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(97, 16)
        Me.Label36.TabIndex = 1573
        Me.Label36.Text = "Payment Start :"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(15, 65)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(107, 16)
        Me.Label37.TabIndex = 1572
        Me.Label37.Text = "Terms (Months) :"
        '
        'nupDeferredTerms
        '
        Me.nupDeferredTerms.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.nupDeferredTerms.Location = New System.Drawing.Point(125, 63)
        Me.nupDeferredTerms.Name = "nupDeferredTerms"
        Me.nupDeferredTerms.Size = New System.Drawing.Size(55, 22)
        Me.nupDeferredTerms.TabIndex = 1571
        Me.nupDeferredTerms.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'dtpDeferredStart
        '
        Me.dtpDeferredStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDeferredStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDeferredStart.Location = New System.Drawing.Point(125, 112)
        Me.dtpDeferredStart.Name = "dtpDeferredStart"
        Me.dtpDeferredStart.Size = New System.Drawing.Size(151, 22)
        Me.dtpDeferredStart.TabIndex = 1570
        '
        'tpFinancing
        '
        Me.tpFinancing.Controls.Add(Me.GroupBox3)
        Me.tpFinancing.Controls.Add(Me.txtReserve)
        Me.tpFinancing.Controls.Add(Me.Label9)
        Me.tpFinancing.Controls.Add(Me.tcFinancing)
        Me.tpFinancing.Controls.Add(Me.miscFeeRate)
        Me.tpFinancing.Controls.Add(Me.Label5)
        Me.tpFinancing.Controls.Add(Me.cbFinancingMode)
        Me.tpFinancing.Controls.Add(Me.Label23)
        Me.tpFinancing.Controls.Add(Me.Label17)
        Me.tpFinancing.Controls.Add(Me.txtLoanable)
        Me.tpFinancing.Controls.Add(Me.nupDPRate)
        Me.tpFinancing.Controls.Add(Me.txtTotalDP)
        Me.tpFinancing.Location = New System.Drawing.Point(4, 24)
        Me.tpFinancing.Name = "tpFinancing"
        Me.tpFinancing.Padding = New System.Windows.Forms.Padding(3)
        Me.tpFinancing.Size = New System.Drawing.Size(923, 476)
        Me.tpFinancing.TabIndex = 5
        Me.tpFinancing.Text = "Financing"
        Me.tpFinancing.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.lblDPPayableUntil)
        Me.GroupBox3.Controls.Add(Me.Label45)
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        Me.GroupBox3.Controls.Add(Me.nupTerm)
        Me.GroupBox3.Controls.Add(Me.Label21)
        Me.GroupBox3.Controls.Add(Me.dgvDPSchedule)
        Me.GroupBox3.Controls.Add(Me.txtBalanceDP)
        Me.GroupBox3.Controls.Add(Me.dtpStart)
        Me.GroupBox3.Controls.Add(Me.Label41)
        Me.GroupBox3.Controls.Add(Me.Label22)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.nupDiscountRate)
        Me.GroupBox3.Controls.Add(Me.txtMonthly)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.txtDiscount)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.GroupBox3.Location = New System.Drawing.Point(16, 40)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(901, 187)
        Me.GroupBox3.TabIndex = 1611
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Downpayment"
        '
        'lblDPPayableUntil
        '
        Me.lblDPPayableUntil.AutoSize = True
        Me.lblDPPayableUntil.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDPPayableUntil.Location = New System.Drawing.Point(120, 161)
        Me.lblDPPayableUntil.Name = "lblDPPayableUntil"
        Me.lblDPPayableUntil.Size = New System.Drawing.Size(93, 16)
        Me.lblDPPayableUntil.TabIndex = 1613
        Me.lblDPPayableUntil.Text = "Payable Until :"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(21, 161)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(93, 16)
        Me.Label45.TabIndex = 1612
        Me.Label45.Text = "Payable Until :"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.nupDPDays)
        Me.GroupBox4.Controls.Add(Me.rbTerm)
        Me.GroupBox4.Controls.Add(Me.rbCashWithin)
        Me.GroupBox4.Controls.Add(Me.rbSpotDp)
        Me.GroupBox4.Controls.Add(Me.Label40)
        Me.GroupBox4.Location = New System.Drawing.Point(15, 19)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(551, 48)
        Me.GroupBox4.TabIndex = 1611
        Me.GroupBox4.TabStop = False
        '
        'nupDPDays
        '
        Me.nupDPDays.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.nupDPDays.Location = New System.Drawing.Point(251, 16)
        Me.nupDPDays.Name = "nupDPDays"
        Me.nupDPDays.Size = New System.Drawing.Size(55, 22)
        Me.nupDPDays.TabIndex = 1608
        Me.nupDPDays.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'rbTerm
        '
        Me.rbTerm.AutoSize = True
        Me.rbTerm.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.rbTerm.Location = New System.Drawing.Point(390, 17)
        Me.rbTerm.Name = "rbTerm"
        Me.rbTerm.Size = New System.Drawing.Size(133, 20)
        Me.rbTerm.TabIndex = 1607
        Me.rbTerm.Text = "Termed Payment "
        Me.rbTerm.UseVisualStyleBackColor = True
        '
        'rbCashWithin
        '
        Me.rbCashWithin.AutoSize = True
        Me.rbCashWithin.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.rbCashWithin.Location = New System.Drawing.Point(141, 18)
        Me.rbCashWithin.Name = "rbCashWithin"
        Me.rbCashWithin.Size = New System.Drawing.Size(96, 20)
        Me.rbCashWithin.TabIndex = 1604
        Me.rbCashWithin.Text = "Cash Within"
        Me.rbCashWithin.UseVisualStyleBackColor = True
        '
        'rbSpotDp
        '
        Me.rbSpotDp.AutoSize = True
        Me.rbSpotDp.Checked = True
        Me.rbSpotDp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.rbSpotDp.Location = New System.Drawing.Point(15, 20)
        Me.rbSpotDp.Name = "rbSpotDp"
        Me.rbSpotDp.Size = New System.Drawing.Size(76, 20)
        Me.rbSpotDp.TabIndex = 1603
        Me.rbSpotDp.TabStop = True
        Me.rbSpotDp.Text = "Spot DP"
        Me.rbSpotDp.UseVisualStyleBackColor = True
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label40.Location = New System.Drawing.Point(308, 20)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(40, 16)
        Me.Label40.TabIndex = 1606
        Me.Label40.Text = "Days"
        '
        'nupTerm
        '
        Me.nupTerm.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.nupTerm.Location = New System.Drawing.Point(407, 81)
        Me.nupTerm.Name = "nupTerm"
        Me.nupTerm.Size = New System.Drawing.Size(55, 22)
        Me.nupTerm.TabIndex = 1566
        Me.nupTerm.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(294, 85)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(107, 16)
        Me.Label21.TabIndex = 1567
        Me.Label21.Text = "Terms (Months) :"
        '
        'dgvDPSchedule
        '
        Me.dgvDPSchedule.AllowUserToAddRows = False
        Me.dgvDPSchedule.AllowUserToDeleteRows = False
        Me.dgvDPSchedule.AllowUserToResizeColumns = False
        Me.dgvDPSchedule.AllowUserToResizeRows = False
        Me.dgvDPSchedule.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDPSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDPSchedule.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chCount, Me.chDateDue, Me.chAmount})
        Me.dgvDPSchedule.Location = New System.Drawing.Point(574, 18)
        Me.dgvDPSchedule.Name = "dgvDPSchedule"
        Me.dgvDPSchedule.ReadOnly = True
        Me.dgvDPSchedule.RowHeadersVisible = False
        Me.dgvDPSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDPSchedule.Size = New System.Drawing.Size(321, 159)
        Me.dgvDPSchedule.TabIndex = 1564
        '
        'chCount
        '
        Me.chCount.HeaderText = "No."
        Me.chCount.Name = "chCount"
        Me.chCount.ReadOnly = True
        Me.chCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.chCount.Width = 50
        '
        'chDateDue
        '
        Me.chDateDue.HeaderText = "Date Due"
        Me.chDateDue.Name = "chDateDue"
        Me.chDateDue.ReadOnly = True
        Me.chDateDue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.chDateDue.Width = 120
        '
        'chAmount
        '
        Me.chAmount.HeaderText = "Amount"
        Me.chAmount.Name = "chAmount"
        Me.chAmount.ReadOnly = True
        Me.chAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.chAmount.Width = 150
        '
        'txtBalanceDP
        '
        Me.txtBalanceDP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalanceDP.Location = New System.Drawing.Point(120, 132)
        Me.txtBalanceDP.Name = "txtBalanceDP"
        Me.txtBalanceDP.ReadOnly = True
        Me.txtBalanceDP.Size = New System.Drawing.Size(151, 22)
        Me.txtBalanceDP.TabIndex = 1610
        Me.txtBalanceDP.Text = "0.00"
        Me.txtBalanceDP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'dtpStart
        '
        Me.dtpStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStart.Location = New System.Drawing.Point(407, 107)
        Me.dtpStart.Name = "dtpStart"
        Me.dtpStart.Size = New System.Drawing.Size(151, 22)
        Me.dtpStart.TabIndex = 1528
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label41.Location = New System.Drawing.Point(28, 135)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(86, 16)
        Me.Label41.TabIndex = 1609
        Me.Label41.Text = "Balance DP :"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(320, 110)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(81, 16)
        Me.Label22.TabIndex = 1569
        Me.Label22.Text = "Equity Start :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label11.Location = New System.Drawing.Point(301, 135)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(100, 16)
        Me.Label11.TabIndex = 1574
        Me.Label11.Text = "Monthly Equity :"
        '
        'nupDiscountRate
        '
        Me.nupDiscountRate.DecimalPlaces = 2
        Me.nupDiscountRate.InterceptArrowKeys = False
        Me.nupDiscountRate.Location = New System.Drawing.Point(120, 83)
        Me.nupDiscountRate.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.nupDiscountRate.Name = "nupDiscountRate"
        Me.nupDiscountRate.Size = New System.Drawing.Size(151, 22)
        Me.nupDiscountRate.TabIndex = 1588
        Me.nupDiscountRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nupDiscountRate.ThousandsSeparator = True
        Me.nupDiscountRate.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'txtMonthly
        '
        Me.txtMonthly.Enabled = False
        Me.txtMonthly.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonthly.Location = New System.Drawing.Point(407, 132)
        Me.txtMonthly.Name = "txtMonthly"
        Me.txtMonthly.ReadOnly = True
        Me.txtMonthly.Size = New System.Drawing.Size(151, 22)
        Me.txtMonthly.TabIndex = 1575
        Me.txtMonthly.Text = "0.00"
        Me.txtMonthly.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label16.Location = New System.Drawing.Point(25, 85)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(89, 16)
        Me.Label16.TabIndex = 1587
        Me.Label16.Text = "Discount (%) :"
        '
        'txtDiscount
        '
        Me.txtDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscount.Location = New System.Drawing.Point(120, 107)
        Me.txtDiscount.Name = "txtDiscount"
        Me.txtDiscount.Size = New System.Drawing.Size(151, 22)
        Me.txtDiscount.TabIndex = 1577
        Me.txtDiscount.TabStop = False
        Me.txtDiscount.Text = "0.00"
        Me.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label12.Location = New System.Drawing.Point(48, 110)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(66, 16)
        Me.Label12.TabIndex = 1576
        Me.Label12.Text = "Discount :"
        '
        'txtReserve
        '
        Me.txtReserve.Enabled = False
        Me.txtReserve.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReserve.Location = New System.Drawing.Point(122, 10)
        Me.txtReserve.Name = "txtReserve"
        Me.txtReserve.Size = New System.Drawing.Size(151, 22)
        Me.txtReserve.TabIndex = 1571
        Me.txtReserve.Text = "0.00"
        Me.txtReserve.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label9.Location = New System.Drawing.Point(335, 236)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(179, 16)
        Me.Label9.TabIndex = 1579
        Me.Label9.Text = "Balance / Loanable Amount :"
        '
        'tcFinancing
        '
        Me.tcFinancing.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcFinancing.Controls.Add(Me.tpInhouse)
        Me.tcFinancing.Controls.Add(Me.tpPagibig)
        Me.tcFinancing.Controls.Add(Me.tpBank)
        Me.tcFinancing.Location = New System.Drawing.Point(6, 266)
        Me.tcFinancing.Name = "tcFinancing"
        Me.tcFinancing.SelectedIndex = 0
        Me.tcFinancing.Size = New System.Drawing.Size(914, 207)
        Me.tcFinancing.TabIndex = 1591
        '
        'tpInhouse
        '
        Me.tpInhouse.Controls.Add(Me.lblInhouseUntil)
        Me.tpInhouse.Controls.Add(Me.Label66)
        Me.tpInhouse.Controls.Add(Me.dtpInhouseStart)
        Me.tpInhouse.Controls.Add(Me.Label46)
        Me.tpInhouse.Controls.Add(Me.dgvInhouse)
        Me.tpInhouse.Controls.Add(Me.nupInhouseTotalMonths)
        Me.tpInhouse.Controls.Add(Me.Label28)
        Me.tpInhouse.Controls.Add(Me.Label27)
        Me.tpInhouse.Controls.Add(Me.txtInhouseMonthly)
        Me.tpInhouse.Controls.Add(Me.nupInhousePaymentPerYear)
        Me.tpInhouse.Controls.Add(Me.Label26)
        Me.tpInhouse.Controls.Add(Me.nupInhouseYear)
        Me.tpInhouse.Controls.Add(Me.Label25)
        Me.tpInhouse.Controls.Add(Me.nupInhouseRate)
        Me.tpInhouse.Controls.Add(Me.Label24)
        Me.tpInhouse.Location = New System.Drawing.Point(4, 24)
        Me.tpInhouse.Name = "tpInhouse"
        Me.tpInhouse.Padding = New System.Windows.Forms.Padding(3)
        Me.tpInhouse.Size = New System.Drawing.Size(906, 179)
        Me.tpInhouse.TabIndex = 0
        Me.tpInhouse.Text = "In-House"
        Me.tpInhouse.UseVisualStyleBackColor = True
        '
        'lblInhouseUntil
        '
        Me.lblInhouseUntil.AutoSize = True
        Me.lblInhouseUntil.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInhouseUntil.Location = New System.Drawing.Point(182, 152)
        Me.lblInhouseUntil.Name = "lblInhouseUntil"
        Me.lblInhouseUntil.Size = New System.Drawing.Size(93, 16)
        Me.lblInhouseUntil.TabIndex = 1617
        Me.lblInhouseUntil.Text = "Payable Until :"
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.Location = New System.Drawing.Point(83, 152)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(93, 16)
        Me.Label66.TabIndex = 1616
        Me.Label66.Text = "Payable Until :"
        '
        'dtpInhouseStart
        '
        Me.dtpInhouseStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpInhouseStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpInhouseStart.Location = New System.Drawing.Point(182, 126)
        Me.dtpInhouseStart.Name = "dtpInhouseStart"
        Me.dtpInhouseStart.Size = New System.Drawing.Size(151, 22)
        Me.dtpInhouseStart.TabIndex = 1601
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(79, 129)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(97, 16)
        Me.Label46.TabIndex = 1602
        Me.Label46.Text = "Payment Start :"
        '
        'dgvInhouse
        '
        Me.dgvInhouse.AllowUserToAddRows = False
        Me.dgvInhouse.AllowUserToDeleteRows = False
        Me.dgvInhouse.AllowUserToResizeColumns = False
        Me.dgvInhouse.AllowUserToResizeRows = False
        Me.dgvInhouse.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvInhouse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInhouse.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcInHouseNo, Me.dgcInHouseDueDate, Me.dgcInhouseInterest, Me.dgcInHousePrincipal, Me.dgcInHouseTotal})
        Me.dgvInhouse.Location = New System.Drawing.Point(339, 10)
        Me.dgvInhouse.Name = "dgvInhouse"
        Me.dgvInhouse.ReadOnly = True
        Me.dgvInhouse.RowHeadersVisible = False
        Me.dgvInhouse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvInhouse.Size = New System.Drawing.Size(561, 201)
        Me.dgvInhouse.TabIndex = 1599
        '
        'dgcInHouseNo
        '
        Me.dgcInHouseNo.HeaderText = "No."
        Me.dgcInHouseNo.Name = "dgcInHouseNo"
        Me.dgcInHouseNo.ReadOnly = True
        Me.dgcInHouseNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.dgcInHouseNo.Width = 50
        '
        'dgcInHouseDueDate
        '
        Me.dgcInHouseDueDate.HeaderText = "Date Due"
        Me.dgcInHouseDueDate.Name = "dgcInHouseDueDate"
        Me.dgcInHouseDueDate.ReadOnly = True
        Me.dgcInHouseDueDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.dgcInHouseDueDate.Width = 120
        '
        'dgcInhouseInterest
        '
        Me.dgcInhouseInterest.HeaderText = "Interest"
        Me.dgcInhouseInterest.Name = "dgcInhouseInterest"
        Me.dgcInhouseInterest.ReadOnly = True
        Me.dgcInhouseInterest.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.dgcInhouseInterest.Width = 150
        '
        'dgcInHousePrincipal
        '
        Me.dgcInHousePrincipal.HeaderText = "Principal"
        Me.dgcInHousePrincipal.Name = "dgcInHousePrincipal"
        Me.dgcInHousePrincipal.ReadOnly = True
        '
        'dgcInHouseTotal
        '
        Me.dgcInHouseTotal.HeaderText = "Total"
        Me.dgcInHouseTotal.Name = "dgcInHouseTotal"
        Me.dgcInHouseTotal.ReadOnly = True
        '
        'nupInhouseTotalMonths
        '
        Me.nupInhouseTotalMonths.InterceptArrowKeys = False
        Me.nupInhouseTotalMonths.Location = New System.Drawing.Point(182, 79)
        Me.nupInhouseTotalMonths.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.nupInhouseTotalMonths.Name = "nupInhouseTotalMonths"
        Me.nupInhouseTotalMonths.Size = New System.Drawing.Size(151, 21)
        Me.nupInhouseTotalMonths.TabIndex = 1598
        Me.nupInhouseTotalMonths.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nupInhouseTotalMonths.ThousandsSeparator = True
        Me.nupInhouseTotalMonths.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label28.Location = New System.Drawing.Point(25, 81)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(153, 16)
        Me.Label28.TabIndex = 1597
        Me.Label28.Text = "Months to be Amortized :"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label27.Location = New System.Drawing.Point(40, 105)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(136, 16)
        Me.Label27.TabIndex = 1595
        Me.Label27.Text = "Monthly Amortization :"
        '
        'txtInhouseMonthly
        '
        Me.txtInhouseMonthly.Enabled = False
        Me.txtInhouseMonthly.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInhouseMonthly.Location = New System.Drawing.Point(182, 102)
        Me.txtInhouseMonthly.Name = "txtInhouseMonthly"
        Me.txtInhouseMonthly.ReadOnly = True
        Me.txtInhouseMonthly.Size = New System.Drawing.Size(151, 22)
        Me.txtInhouseMonthly.TabIndex = 1596
        Me.txtInhouseMonthly.Text = "0.00"
        Me.txtInhouseMonthly.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'nupInhousePaymentPerYear
        '
        Me.nupInhousePaymentPerYear.InterceptArrowKeys = False
        Me.nupInhousePaymentPerYear.Location = New System.Drawing.Point(182, 56)
        Me.nupInhousePaymentPerYear.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.nupInhousePaymentPerYear.Name = "nupInhousePaymentPerYear"
        Me.nupInhousePaymentPerYear.Size = New System.Drawing.Size(151, 21)
        Me.nupInhousePaymentPerYear.TabIndex = 1594
        Me.nupInhousePaymentPerYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nupInhousePaymentPerYear.ThousandsSeparator = True
        Me.nupInhousePaymentPerYear.Value = New Decimal(New Integer() {12, 0, 0, 0})
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label26.Location = New System.Drawing.Point(54, 58)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(122, 16)
        Me.Label26.TabIndex = 1593
        Me.Label26.Text = "Payment per Year :"
        '
        'nupInhouseYear
        '
        Me.nupInhouseYear.InterceptArrowKeys = False
        Me.nupInhouseYear.Location = New System.Drawing.Point(182, 34)
        Me.nupInhouseYear.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.nupInhouseYear.Name = "nupInhouseYear"
        Me.nupInhouseYear.Size = New System.Drawing.Size(151, 21)
        Me.nupInhouseYear.TabIndex = 1592
        Me.nupInhouseYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nupInhouseYear.ThousandsSeparator = True
        Me.nupInhouseYear.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label25.Location = New System.Drawing.Point(83, 36)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(93, 16)
        Me.Label25.TabIndex = 1591
        Me.Label25.Text = "Terms (Year) :"
        '
        'nupInhouseRate
        '
        Me.nupInhouseRate.DecimalPlaces = 2
        Me.nupInhouseRate.InterceptArrowKeys = False
        Me.nupInhouseRate.Location = New System.Drawing.Point(182, 11)
        Me.nupInhouseRate.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.nupInhouseRate.Name = "nupInhouseRate"
        Me.nupInhouseRate.Size = New System.Drawing.Size(151, 21)
        Me.nupInhouseRate.TabIndex = 1590
        Me.nupInhouseRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nupInhouseRate.ThousandsSeparator = True
        Me.nupInhouseRate.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label24.Location = New System.Drawing.Point(20, 13)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(156, 16)
        Me.Label24.TabIndex = 1589
        Me.Label24.Text = "Annual Interest Rate (%) :"
        '
        'tpPagibig
        '
        Me.tpPagibig.Controls.Add(Me.lblPagibigUntil)
        Me.tpPagibig.Controls.Add(Me.Label64)
        Me.tpPagibig.Controls.Add(Me.dgvPagibig)
        Me.tpPagibig.Controls.Add(Me.Panel1)
        Me.tpPagibig.Controls.Add(Me.nupPagibigTerms)
        Me.tpPagibig.Controls.Add(Me.Label55)
        Me.tpPagibig.Controls.Add(Me.dtpPagibigStart)
        Me.tpPagibig.Controls.Add(Me.Label56)
        Me.tpPagibig.Controls.Add(Me.Label57)
        Me.tpPagibig.Controls.Add(Me.txtPagibigMonthly)
        Me.tpPagibig.Controls.Add(Me.Label54)
        Me.tpPagibig.Controls.Add(Me.txtPagibiLoanDiff)
        Me.tpPagibig.Controls.Add(Me.Label53)
        Me.tpPagibig.Controls.Add(Me.txtPagibigLoanable)
        Me.tpPagibig.Location = New System.Drawing.Point(4, 24)
        Me.tpPagibig.Name = "tpPagibig"
        Me.tpPagibig.Padding = New System.Windows.Forms.Padding(3)
        Me.tpPagibig.Size = New System.Drawing.Size(906, 179)
        Me.tpPagibig.TabIndex = 1
        Me.tpPagibig.Text = "Pagibig"
        Me.tpPagibig.UseVisualStyleBackColor = True
        '
        'lblPagibigUntil
        '
        Me.lblPagibigUntil.AutoSize = True
        Me.lblPagibigUntil.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPagibigUntil.Location = New System.Drawing.Point(197, 140)
        Me.lblPagibigUntil.Name = "lblPagibigUntil"
        Me.lblPagibigUntil.Size = New System.Drawing.Size(93, 16)
        Me.lblPagibigUntil.TabIndex = 1615
        Me.lblPagibigUntil.Text = "Payable Until :"
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.Location = New System.Drawing.Point(98, 140)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(93, 16)
        Me.Label64.TabIndex = 1614
        Me.Label64.Text = "Payable Until :"
        '
        'dgvPagibig
        '
        Me.dgvPagibig.AllowUserToAddRows = False
        Me.dgvPagibig.AllowUserToDeleteRows = False
        Me.dgvPagibig.AllowUserToResizeColumns = False
        Me.dgvPagibig.AllowUserToResizeRows = False
        Me.dgvPagibig.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPagibig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPagibig.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcPagibigPaymentNo, Me.dgcPagibigDuedate, Me.dgcPagibigAmount})
        Me.dgvPagibig.Location = New System.Drawing.Point(451, 4)
        Me.dgvPagibig.Name = "dgvPagibig"
        Me.dgvPagibig.ReadOnly = True
        Me.dgvPagibig.RowHeadersVisible = False
        Me.dgvPagibig.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPagibig.Size = New System.Drawing.Size(443, 215)
        Me.dgvPagibig.TabIndex = 1607
        '
        'dgcPagibigPaymentNo
        '
        Me.dgcPagibigPaymentNo.HeaderText = "No."
        Me.dgcPagibigPaymentNo.Name = "dgcPagibigPaymentNo"
        Me.dgcPagibigPaymentNo.ReadOnly = True
        Me.dgcPagibigPaymentNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.dgcPagibigPaymentNo.Width = 50
        '
        'dgcPagibigDuedate
        '
        Me.dgcPagibigDuedate.HeaderText = "Date Due"
        Me.dgcPagibigDuedate.Name = "dgcPagibigDuedate"
        Me.dgcPagibigDuedate.ReadOnly = True
        Me.dgcPagibigDuedate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.dgcPagibigDuedate.Width = 120
        '
        'dgcPagibigAmount
        '
        Me.dgcPagibigAmount.HeaderText = "Amount"
        Me.dgcPagibigAmount.Name = "dgcPagibigAmount"
        Me.dgcPagibigAmount.ReadOnly = True
        Me.dgcPagibigAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.dgcPagibigAmount.Width = 150
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label44)
        Me.Panel1.Controls.Add(Me.lvPagibig)
        Me.Panel1.Controls.Add(Me.nupPagibigRate)
        Me.Panel1.Location = New System.Drawing.Point(777, 153)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(20, 10)
        Me.Panel1.TabIndex = 1606
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label44.Location = New System.Drawing.Point(20, 10)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(156, 16)
        Me.Label44.TabIndex = 1594
        Me.Label44.Text = "Annual Interest Rate (%) :"
        '
        'lvPagibig
        '
        Me.lvPagibig.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chPagibigTerms, Me.chPagibigAmort, Me.chPagibigNDI})
        Me.lvPagibig.FullRowSelect = True
        Me.lvPagibig.HideSelection = False
        Me.lvPagibig.Location = New System.Drawing.Point(1, 32)
        Me.lvPagibig.MultiSelect = False
        Me.lvPagibig.Name = "lvPagibig"
        Me.lvPagibig.Size = New System.Drawing.Size(327, 121)
        Me.lvPagibig.TabIndex = 1593
        Me.lvPagibig.UseCompatibleStateImageBehavior = False
        Me.lvPagibig.View = System.Windows.Forms.View.Details
        '
        'chPagibigTerms
        '
        Me.chPagibigTerms.Text = "Terms"
        Me.chPagibigTerms.Width = 77
        '
        'chPagibigAmort
        '
        Me.chPagibigAmort.Text = "Amortization"
        Me.chPagibigAmort.Width = 111
        '
        'chPagibigNDI
        '
        Me.chPagibigNDI.Text = "Required NDI"
        Me.chPagibigNDI.Width = 127
        '
        'nupPagibigRate
        '
        Me.nupPagibigRate.DecimalPlaces = 2
        Me.nupPagibigRate.InterceptArrowKeys = False
        Me.nupPagibigRate.Location = New System.Drawing.Point(182, 8)
        Me.nupPagibigRate.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.nupPagibigRate.Name = "nupPagibigRate"
        Me.nupPagibigRate.Size = New System.Drawing.Size(71, 21)
        Me.nupPagibigRate.TabIndex = 1595
        Me.nupPagibigRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nupPagibigRate.ThousandsSeparator = True
        Me.nupPagibigRate.Value = New Decimal(New Integer() {65, 0, 0, 65536})
        '
        'nupPagibigTerms
        '
        Me.nupPagibigTerms.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.nupPagibigTerms.Location = New System.Drawing.Point(197, 63)
        Me.nupPagibigTerms.Name = "nupPagibigTerms"
        Me.nupPagibigTerms.Size = New System.Drawing.Size(55, 22)
        Me.nupPagibigTerms.TabIndex = 1601
        Me.nupPagibigTerms.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.Location = New System.Drawing.Point(84, 67)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(107, 16)
        Me.Label55.TabIndex = 1602
        Me.Label55.Text = "Terms (Months) :"
        '
        'dtpPagibigStart
        '
        Me.dtpPagibigStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpPagibigStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpPagibigStart.Location = New System.Drawing.Point(197, 88)
        Me.dtpPagibigStart.Name = "dtpPagibigStart"
        Me.dtpPagibigStart.Size = New System.Drawing.Size(151, 22)
        Me.dtpPagibigStart.TabIndex = 1600
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.Location = New System.Drawing.Point(94, 91)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(97, 16)
        Me.Label56.TabIndex = 1603
        Me.Label56.Text = "Payment Start :"
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label57.Location = New System.Drawing.Point(75, 116)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(116, 16)
        Me.Label57.TabIndex = 1604
        Me.Label57.Text = "Monthly Payment :"
        '
        'txtPagibigMonthly
        '
        Me.txtPagibigMonthly.Enabled = False
        Me.txtPagibigMonthly.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPagibigMonthly.Location = New System.Drawing.Point(197, 113)
        Me.txtPagibigMonthly.Name = "txtPagibigMonthly"
        Me.txtPagibigMonthly.ReadOnly = True
        Me.txtPagibigMonthly.Size = New System.Drawing.Size(151, 22)
        Me.txtPagibigMonthly.TabIndex = 1605
        Me.txtPagibigMonthly.Text = "0.00"
        Me.txtPagibigMonthly.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label54.Location = New System.Drawing.Point(86, 42)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(108, 16)
        Me.Label54.TabIndex = 1598
        Me.Label54.Text = "Loan Difference :"
        '
        'txtPagibiLoanDiff
        '
        Me.txtPagibiLoanDiff.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtPagibiLoanDiff.Location = New System.Drawing.Point(197, 39)
        Me.txtPagibiLoanDiff.Name = "txtPagibiLoanDiff"
        Me.txtPagibiLoanDiff.ReadOnly = True
        Me.txtPagibiLoanDiff.Size = New System.Drawing.Size(156, 22)
        Me.txtPagibiLoanDiff.TabIndex = 1599
        Me.txtPagibiLoanDiff.Text = "0.00"
        Me.txtPagibiLoanDiff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label53.Location = New System.Drawing.Point(12, 18)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(182, 16)
        Me.Label53.TabIndex = 1596
        Me.Label53.Text = "Approved Loanable Amount :"
        '
        'txtPagibigLoanable
        '
        Me.txtPagibigLoanable.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtPagibigLoanable.Location = New System.Drawing.Point(197, 15)
        Me.txtPagibigLoanable.Name = "txtPagibigLoanable"
        Me.txtPagibigLoanable.Size = New System.Drawing.Size(156, 22)
        Me.txtPagibigLoanable.TabIndex = 1597
        Me.txtPagibigLoanable.Text = "0.00"
        Me.txtPagibigLoanable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpBank
        '
        Me.tpBank.Controls.Add(Me.lblBankUntil)
        Me.tpBank.Controls.Add(Me.Label65)
        Me.tpBank.Controls.Add(Me.dgvBank)
        Me.tpBank.Controls.Add(Me.nupBankTerms)
        Me.tpBank.Controls.Add(Me.Label58)
        Me.tpBank.Controls.Add(Me.dtpBankStart)
        Me.tpBank.Controls.Add(Me.Label59)
        Me.tpBank.Controls.Add(Me.Label60)
        Me.tpBank.Controls.Add(Me.txtBankMonthly)
        Me.tpBank.Controls.Add(Me.Label61)
        Me.tpBank.Controls.Add(Me.txtBankLoanDiff)
        Me.tpBank.Controls.Add(Me.Label62)
        Me.tpBank.Controls.Add(Me.txtBankLoanable)
        Me.tpBank.Controls.Add(Me.Panel2)
        Me.tpBank.Location = New System.Drawing.Point(4, 24)
        Me.tpBank.Name = "tpBank"
        Me.tpBank.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBank.Size = New System.Drawing.Size(906, 179)
        Me.tpBank.TabIndex = 2
        Me.tpBank.Text = "Bank"
        Me.tpBank.UseVisualStyleBackColor = True
        '
        'lblBankUntil
        '
        Me.lblBankUntil.AutoSize = True
        Me.lblBankUntil.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankUntil.Location = New System.Drawing.Point(197, 140)
        Me.lblBankUntil.Name = "lblBankUntil"
        Me.lblBankUntil.Size = New System.Drawing.Size(93, 16)
        Me.lblBankUntil.TabIndex = 1629
        Me.lblBankUntil.Text = "Payable Until :"
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.Location = New System.Drawing.Point(98, 140)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(93, 16)
        Me.Label65.TabIndex = 1628
        Me.Label65.Text = "Payable Until :"
        '
        'dgvBank
        '
        Me.dgvBank.AllowUserToAddRows = False
        Me.dgvBank.AllowUserToDeleteRows = False
        Me.dgvBank.AllowUserToResizeColumns = False
        Me.dgvBank.AllowUserToResizeRows = False
        Me.dgvBank.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvBank.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBank.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcBankPaymentNo, Me.dgcBankDuedate, Me.dgcBankAmount})
        Me.dgvBank.Location = New System.Drawing.Point(451, 4)
        Me.dgvBank.Name = "dgvBank"
        Me.dgvBank.ReadOnly = True
        Me.dgvBank.RowHeadersVisible = False
        Me.dgvBank.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvBank.Size = New System.Drawing.Size(443, 215)
        Me.dgvBank.TabIndex = 1627
        '
        'dgcBankPaymentNo
        '
        Me.dgcBankPaymentNo.HeaderText = "No."
        Me.dgcBankPaymentNo.Name = "dgcBankPaymentNo"
        Me.dgcBankPaymentNo.ReadOnly = True
        Me.dgcBankPaymentNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.dgcBankPaymentNo.Width = 50
        '
        'dgcBankDuedate
        '
        Me.dgcBankDuedate.HeaderText = "Date Due"
        Me.dgcBankDuedate.Name = "dgcBankDuedate"
        Me.dgcBankDuedate.ReadOnly = True
        Me.dgcBankDuedate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.dgcBankDuedate.Width = 120
        '
        'dgcBankAmount
        '
        Me.dgcBankAmount.HeaderText = "Amount"
        Me.dgcBankAmount.Name = "dgcBankAmount"
        Me.dgcBankAmount.ReadOnly = True
        Me.dgcBankAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.dgcBankAmount.Width = 150
        '
        'nupBankTerms
        '
        Me.nupBankTerms.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.nupBankTerms.Location = New System.Drawing.Point(197, 63)
        Me.nupBankTerms.Name = "nupBankTerms"
        Me.nupBankTerms.Size = New System.Drawing.Size(55, 22)
        Me.nupBankTerms.TabIndex = 1622
        Me.nupBankTerms.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.Location = New System.Drawing.Point(84, 67)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(107, 16)
        Me.Label58.TabIndex = 1623
        Me.Label58.Text = "Terms (Months) :"
        '
        'dtpBankStart
        '
        Me.dtpBankStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpBankStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpBankStart.Location = New System.Drawing.Point(197, 88)
        Me.dtpBankStart.Name = "dtpBankStart"
        Me.dtpBankStart.Size = New System.Drawing.Size(151, 22)
        Me.dtpBankStart.TabIndex = 1621
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.Location = New System.Drawing.Point(94, 91)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(97, 16)
        Me.Label59.TabIndex = 1624
        Me.Label59.Text = "Payment Start :"
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label60.Location = New System.Drawing.Point(75, 116)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(116, 16)
        Me.Label60.TabIndex = 1625
        Me.Label60.Text = "Monthly Payment :"
        '
        'txtBankMonthly
        '
        Me.txtBankMonthly.Enabled = False
        Me.txtBankMonthly.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankMonthly.Location = New System.Drawing.Point(197, 113)
        Me.txtBankMonthly.Name = "txtBankMonthly"
        Me.txtBankMonthly.ReadOnly = True
        Me.txtBankMonthly.Size = New System.Drawing.Size(151, 22)
        Me.txtBankMonthly.TabIndex = 1626
        Me.txtBankMonthly.Text = "0.00"
        Me.txtBankMonthly.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label61.Location = New System.Drawing.Point(86, 42)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(108, 16)
        Me.Label61.TabIndex = 1619
        Me.Label61.Text = "Loan Difference :"
        '
        'txtBankLoanDiff
        '
        Me.txtBankLoanDiff.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtBankLoanDiff.Location = New System.Drawing.Point(197, 39)
        Me.txtBankLoanDiff.Name = "txtBankLoanDiff"
        Me.txtBankLoanDiff.ReadOnly = True
        Me.txtBankLoanDiff.Size = New System.Drawing.Size(156, 22)
        Me.txtBankLoanDiff.TabIndex = 1620
        Me.txtBankLoanDiff.Text = "0.00"
        Me.txtBankLoanDiff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label62.Location = New System.Drawing.Point(12, 18)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(182, 16)
        Me.Label62.TabIndex = 1617
        Me.Label62.Text = "Approved Loanable Amount :"
        '
        'txtBankLoanable
        '
        Me.txtBankLoanable.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtBankLoanable.Location = New System.Drawing.Point(197, 15)
        Me.txtBankLoanable.Name = "txtBankLoanable"
        Me.txtBankLoanable.Size = New System.Drawing.Size(156, 22)
        Me.txtBankLoanable.TabIndex = 1618
        Me.txtBankLoanable.Text = "0.00"
        Me.txtBankLoanable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label42)
        Me.Panel2.Controls.Add(Me.lvBankComputation)
        Me.Panel2.Controls.Add(Me.nupBankRate)
        Me.Panel2.Location = New System.Drawing.Point(768, 151)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(14, 10)
        Me.Panel2.TabIndex = 1616
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label42.Location = New System.Drawing.Point(21, 21)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(156, 16)
        Me.Label42.TabIndex = 1591
        Me.Label42.Text = "Annual Interest Rate (%) :"
        '
        'lvBankComputation
        '
        Me.lvBankComputation.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chTerm, Me.chAmortization, Me.chNDI})
        Me.lvBankComputation.FullRowSelect = True
        Me.lvBankComputation.HideSelection = False
        Me.lvBankComputation.Location = New System.Drawing.Point(2, 43)
        Me.lvBankComputation.MultiSelect = False
        Me.lvBankComputation.Name = "lvBankComputation"
        Me.lvBankComputation.Size = New System.Drawing.Size(327, 121)
        Me.lvBankComputation.TabIndex = 0
        Me.lvBankComputation.UseCompatibleStateImageBehavior = False
        Me.lvBankComputation.View = System.Windows.Forms.View.Details
        '
        'chTerm
        '
        Me.chTerm.Text = "Terms"
        Me.chTerm.Width = 77
        '
        'chAmortization
        '
        Me.chAmortization.Text = "Amortization"
        Me.chAmortization.Width = 111
        '
        'chNDI
        '
        Me.chNDI.Text = "Required NDI"
        Me.chNDI.Width = 127
        '
        'nupBankRate
        '
        Me.nupBankRate.DecimalPlaces = 2
        Me.nupBankRate.InterceptArrowKeys = False
        Me.nupBankRate.Location = New System.Drawing.Point(183, 19)
        Me.nupBankRate.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.nupBankRate.Name = "nupBankRate"
        Me.nupBankRate.Size = New System.Drawing.Size(71, 21)
        Me.nupBankRate.TabIndex = 1592
        Me.nupBankRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nupBankRate.ThousandsSeparator = True
        Me.nupBankRate.Value = New Decimal(New Integer() {65, 0, 0, 65536})
        '
        'miscFeeRate
        '
        Me.miscFeeRate.AutoSize = True
        Me.miscFeeRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.miscFeeRate.Location = New System.Drawing.Point(504, 13)
        Me.miscFeeRate.Name = "miscFeeRate"
        Me.miscFeeRate.Size = New System.Drawing.Size(20, 16)
        Me.miscFeeRate.TabIndex = 1585
        Me.miscFeeRate.Text = "%"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label5.Location = New System.Drawing.Point(30, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 16)
        Me.Label5.TabIndex = 1570
        Me.Label5.Text = "Reservation :"
        '
        'cbFinancingMode
        '
        Me.cbFinancingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFinancingMode.FormattingEnabled = True
        Me.cbFinancingMode.Items.AddRange(New Object() {"In-House", "Pagibig", "Bank"})
        Me.cbFinancingMode.Location = New System.Drawing.Point(136, 236)
        Me.cbFinancingMode.Name = "cbFinancingMode"
        Me.cbFinancingMode.Size = New System.Drawing.Size(151, 23)
        Me.cbFinancingMode.TabIndex = 1589
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label23.Location = New System.Drawing.Point(346, 13)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(92, 16)
        Me.Label23.TabIndex = 1572
        Me.Label23.Text = "Required DP :"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label17.Location = New System.Drawing.Point(20, 239)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(110, 16)
        Me.Label17.TabIndex = 1590
        Me.Label17.Text = "Financing Mode :"
        '
        'txtLoanable
        '
        Me.txtLoanable.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtLoanable.Location = New System.Drawing.Point(520, 233)
        Me.txtLoanable.Name = "txtLoanable"
        Me.txtLoanable.ReadOnly = True
        Me.txtLoanable.Size = New System.Drawing.Size(156, 22)
        Me.txtLoanable.TabIndex = 1580
        Me.txtLoanable.Text = "0.00"
        Me.txtLoanable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'nupDPRate
        '
        Me.nupDPRate.DecimalPlaces = 2
        Me.nupDPRate.InterceptArrowKeys = False
        Me.nupDPRate.Location = New System.Drawing.Point(441, 11)
        Me.nupDPRate.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.nupDPRate.Name = "nupDPRate"
        Me.nupDPRate.Size = New System.Drawing.Size(60, 21)
        Me.nupDPRate.TabIndex = 1586
        Me.nupDPRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nupDPRate.ThousandsSeparator = True
        Me.nupDPRate.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'txtTotalDP
        '
        Me.txtTotalDP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalDP.Location = New System.Drawing.Point(525, 10)
        Me.txtTotalDP.Name = "txtTotalDP"
        Me.txtTotalDP.Size = New System.Drawing.Size(151, 22)
        Me.txtTotalDP.TabIndex = 1573
        Me.txtTotalDP.Text = "0.00"
        Me.txtTotalDP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpAccountingEntries
        '
        Me.tpAccountingEntries.AutoScroll = True
        Me.tpAccountingEntries.Controls.Add(Me.dgvEntry)
        Me.tpAccountingEntries.Controls.Add(Me.txtTotalCredit)
        Me.tpAccountingEntries.Controls.Add(Me.txtTotalDebit)
        Me.tpAccountingEntries.Controls.Add(Me.Label63)
        Me.tpAccountingEntries.Location = New System.Drawing.Point(4, 24)
        Me.tpAccountingEntries.Name = "tpAccountingEntries"
        Me.tpAccountingEntries.Padding = New System.Windows.Forms.Padding(3)
        Me.tpAccountingEntries.Size = New System.Drawing.Size(923, 476)
        Me.tpAccountingEntries.TabIndex = 6
        Me.tpAccountingEntries.Text = "Accounting Entries"
        Me.tpAccountingEntries.UseVisualStyleBackColor = True
        '
        'dgvEntry
        '
        Me.dgvEntry.AllowUserToAddRows = False
        Me.dgvEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEntry.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chAccntCode, Me.chAccntTitle, Me.chDebit, Me.chCredit, Me.chParticulars, Me.chVCECode, Me.chVCEName, Me.chRefNo})
        Me.dgvEntry.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgvEntry.Location = New System.Drawing.Point(3, 3)
        Me.dgvEntry.Name = "dgvEntry"
        Me.dgvEntry.ReadOnly = True
        Me.dgvEntry.RowHeadersWidth = 25
        Me.dgvEntry.Size = New System.Drawing.Size(917, 255)
        Me.dgvEntry.TabIndex = 99
        '
        'chAccntCode
        '
        Me.chAccntCode.HeaderText = "Code"
        Me.chAccntCode.Name = "chAccntCode"
        Me.chAccntCode.ReadOnly = True
        '
        'chAccntTitle
        '
        Me.chAccntTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.chAccntTitle.HeaderText = "Account Title"
        Me.chAccntTitle.Name = "chAccntTitle"
        Me.chAccntTitle.ReadOnly = True
        '
        'chDebit
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N2"
        DataGridViewCellStyle1.NullValue = "0"
        Me.chDebit.DefaultCellStyle = DataGridViewCellStyle1
        Me.chDebit.HeaderText = "Debit"
        Me.chDebit.Name = "chDebit"
        Me.chDebit.ReadOnly = True
        '
        'chCredit
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = "0"
        Me.chCredit.DefaultCellStyle = DataGridViewCellStyle2
        Me.chCredit.HeaderText = "Credit"
        Me.chCredit.Name = "chCredit"
        Me.chCredit.ReadOnly = True
        '
        'chParticulars
        '
        Me.chParticulars.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.chParticulars.HeaderText = "Particulars"
        Me.chParticulars.Name = "chParticulars"
        Me.chParticulars.ReadOnly = True
        '
        'chVCECode
        '
        Me.chVCECode.HeaderText = "VCECode"
        Me.chVCECode.Name = "chVCECode"
        Me.chVCECode.ReadOnly = True
        Me.chVCECode.Visible = False
        '
        'chVCEName
        '
        Me.chVCEName.HeaderText = "VCEName"
        Me.chVCEName.Name = "chVCEName"
        Me.chVCEName.ReadOnly = True
        Me.chVCEName.Visible = False
        Me.chVCEName.Width = 200
        '
        'chRefNo
        '
        Me.chRefNo.HeaderText = "Ref. No."
        Me.chRefNo.Name = "chRefNo"
        Me.chRefNo.ReadOnly = True
        '
        'txtTotalCredit
        '
        Me.txtTotalCredit.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTotalCredit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCredit.Location = New System.Drawing.Point(478, 264)
        Me.txtTotalCredit.Name = "txtTotalCredit"
        Me.txtTotalCredit.Size = New System.Drawing.Size(100, 22)
        Me.txtTotalCredit.TabIndex = 100
        '
        'txtTotalDebit
        '
        Me.txtTotalDebit.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtTotalDebit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalDebit.Location = New System.Drawing.Point(379, 264)
        Me.txtTotalDebit.Name = "txtTotalDebit"
        Me.txtTotalDebit.Size = New System.Drawing.Size(100, 22)
        Me.txtTotalDebit.TabIndex = 101
        '
        'Label63
        '
        Me.Label63.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label63.AutoSize = True
        Me.Label63.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.Location = New System.Drawing.Point(330, 267)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(39, 16)
        Me.Label63.TabIndex = 102
        Me.Label63.Text = "Total:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label68)
        Me.GroupBox2.Controls.Add(Me.txtSellingCommission)
        Me.GroupBox2.Controls.Add(Me.Label29)
        Me.GroupBox2.Controls.Add(Me.txtSelling)
        Me.GroupBox2.Controls.Add(Me.cbPaymentType)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.txtMisc)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.txtVAT)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.txtTCP)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.txtAddl)
        Me.GroupBox2.Controls.Add(Me.chkVATInc)
        Me.GroupBox2.Controls.Add(Me.chkVATable)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 124)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(331, 245)
        Me.GroupBox2.TabIndex = 1592
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Contract Price"
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label68.Location = New System.Drawing.Point(49, 159)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(88, 16)
        Me.Label68.TabIndex = 1595
        Me.Label68.Text = "Commission :"
        '
        'txtSellingCommission
        '
        Me.txtSellingCommission.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSellingCommission.Location = New System.Drawing.Point(142, 155)
        Me.txtSellingCommission.Name = "txtSellingCommission"
        Me.txtSellingCommission.Size = New System.Drawing.Size(151, 22)
        Me.txtSellingCommission.TabIndex = 1596
        Me.txtSellingCommission.Text = "0.00"
        Me.txtSellingCommission.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label29.Location = New System.Drawing.Point(34, 210)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(102, 16)
        Me.Label29.TabIndex = 1594
        Me.Label29.Text = "Payment Type :"
        '
        'txtSelling
        '
        Me.txtSelling.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSelling.Location = New System.Drawing.Point(142, 29)
        Me.txtSelling.Name = "txtSelling"
        Me.txtSelling.ReadOnly = True
        Me.txtSelling.Size = New System.Drawing.Size(151, 22)
        Me.txtSelling.TabIndex = 1539
        Me.txtSelling.Text = "0.00"
        Me.txtSelling.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cbPaymentType
        '
        Me.cbPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPaymentType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.cbPaymentType.FormattingEnabled = True
        Me.cbPaymentType.Items.AddRange(New Object() {"Cash Sales", "Deferred Cash", "Financing"})
        Me.cbPaymentType.Location = New System.Drawing.Point(142, 207)
        Me.cbPaymentType.Name = "cbPaymentType"
        Me.cbPaymentType.Size = New System.Drawing.Size(151, 24)
        Me.cbPaymentType.TabIndex = 1593
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label8.Location = New System.Drawing.Point(48, 32)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(89, 16)
        Me.Label8.TabIndex = 1538
        Me.Label8.Text = "Selling Price :"
        '
        'txtMisc
        '
        Me.txtMisc.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMisc.Location = New System.Drawing.Point(142, 104)
        Me.txtMisc.Name = "txtMisc"
        Me.txtMisc.ReadOnly = True
        Me.txtMisc.Size = New System.Drawing.Size(151, 22)
        Me.txtMisc.TabIndex = 1541
        Me.txtMisc.Text = "0.00"
        Me.txtMisc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label10.Location = New System.Drawing.Point(65, 107)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 16)
        Me.Label10.TabIndex = 1540
        Me.Label10.Text = "Misc. Fee :"
        '
        'txtVAT
        '
        Me.txtVAT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVAT.Location = New System.Drawing.Point(142, 54)
        Me.txtVAT.Name = "txtVAT"
        Me.txtVAT.ReadOnly = True
        Me.txtVAT.Size = New System.Drawing.Size(151, 22)
        Me.txtVAT.TabIndex = 1547
        Me.txtVAT.Text = "0.00"
        Me.txtVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label15.Location = New System.Drawing.Point(67, 57)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(70, 16)
        Me.Label15.TabIndex = 1546
        Me.Label15.Text = "VAT 12% :"
        '
        'txtTCP
        '
        Me.txtTCP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTCP.Location = New System.Drawing.Point(142, 180)
        Me.txtTCP.Name = "txtTCP"
        Me.txtTCP.ReadOnly = True
        Me.txtTCP.Size = New System.Drawing.Size(151, 22)
        Me.txtTCP.TabIndex = 1549
        Me.txtTCP.Text = "0.00"
        Me.txtTCP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label7.Location = New System.Drawing.Point(62, 133)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 16)
        Me.Label7.TabIndex = 1583
        Me.Label7.Text = "Addl. Cost :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label14.Location = New System.Drawing.Point(43, 180)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(93, 16)
        Me.Label14.TabIndex = 1548
        Me.Label14.Text = "Total Amount :"
        '
        'txtAddl
        '
        Me.txtAddl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddl.Location = New System.Drawing.Point(142, 129)
        Me.txtAddl.Name = "txtAddl"
        Me.txtAddl.ReadOnly = True
        Me.txtAddl.Size = New System.Drawing.Size(151, 22)
        Me.txtAddl.TabIndex = 1584
        Me.txtAddl.Text = "0.00"
        Me.txtAddl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkVATInc
        '
        Me.chkVATInc.AutoSize = True
        Me.chkVATInc.Enabled = False
        Me.chkVATInc.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkVATInc.Location = New System.Drawing.Point(200, 80)
        Me.chkVATInc.Name = "chkVATInc"
        Me.chkVATInc.Size = New System.Drawing.Size(109, 20)
        Me.chkVATInc.TabIndex = 1582
        Me.chkVATInc.Text = "VAT Inclusive"
        Me.chkVATInc.UseVisualStyleBackColor = True
        '
        'chkVATable
        '
        Me.chkVATable.AutoSize = True
        Me.chkVATable.Checked = True
        Me.chkVATable.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVATable.Enabled = False
        Me.chkVATable.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkVATable.Location = New System.Drawing.Point(122, 80)
        Me.chkVATable.Name = "chkVATable"
        Me.chkVATable.Size = New System.Drawing.Size(81, 20)
        Me.chkVATable.TabIndex = 1581
        Me.chkVATable.Text = "VATable"
        Me.chkVATable.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label6.Location = New System.Drawing.Point(469, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 16)
        Me.Label6.TabIndex = 1563
        Me.Label6.Text = "Remarks :"
        '
        'txtRemarks
        '
        Me.txtRemarks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRemarks.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(472, 39)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(586, 55)
        Me.txtRemarks.TabIndex = 1562
        '
        'btnSearchProp
        '
        Me.btnSearchProp.BackgroundImage = Global.jade.My.Resources.Resources.view
        Me.btnSearchProp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchProp.Location = New System.Drawing.Point(437, 69)
        Me.btnSearchProp.Name = "btnSearchProp"
        Me.btnSearchProp.Size = New System.Drawing.Size(25, 25)
        Me.btnSearchProp.TabIndex = 1561
        Me.btnSearchProp.UseVisualStyleBackColor = True
        '
        'btnLPM
        '
        Me.btnLPM.BackgroundImage = Global.jade.My.Resources.Resources.report_icon
        Me.btnLPM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLPM.Location = New System.Drawing.Point(437, 95)
        Me.btnLPM.Name = "btnLPM"
        Me.btnLPM.Size = New System.Drawing.Size(25, 25)
        Me.btnLPM.TabIndex = 1560
        Me.btnLPM.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(1069, 47)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(74, 16)
        Me.Label19.TabIndex = 1558
        Me.Label19.Text = "Doc. Date :"
        '
        'dtpDocDate
        '
        Me.dtpDocDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpDocDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDocDate.Location = New System.Drawing.Point(1144, 44)
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.Size = New System.Drawing.Size(132, 22)
        Me.dtpDocDate.TabIndex = 1559
        '
        'Label18
        '
        Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(1089, 96)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(54, 16)
        Me.Label18.TabIndex = 1557
        Me.Label18.Text = "Status :"
        '
        'btnVCE
        '
        Me.btnVCE.BackgroundImage = Global.jade.My.Resources.Resources.report_icon
        Me.btnVCE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnVCE.Location = New System.Drawing.Point(437, 45)
        Me.btnVCE.Name = "btnVCE"
        Me.btnVCE.Size = New System.Drawing.Size(25, 25)
        Me.btnVCE.TabIndex = 1526
        Me.btnVCE.UseVisualStyleBackColor = True
        '
        'btnSearchVCE
        '
        Me.btnSearchVCE.BackgroundImage = Global.jade.My.Resources.Resources.view
        Me.btnSearchVCE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchVCE.Location = New System.Drawing.Point(437, 20)
        Me.btnSearchVCE.Name = "btnSearchVCE"
        Me.btnSearchVCE.Size = New System.Drawing.Size(25, 25)
        Me.btnSearchVCE.TabIndex = 1525
        Me.btnSearchVCE.UseVisualStyleBackColor = True
        '
        'txtVCEName
        '
        Me.txtVCEName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVCEName.Location = New System.Drawing.Point(145, 46)
        Me.txtVCEName.Name = "txtVCEName"
        Me.txtVCEName.Size = New System.Drawing.Size(286, 22)
        Me.txtVCEName.TabIndex = 1524
        '
        'txtVCECode
        '
        Me.txtVCECode.Enabled = False
        Me.txtVCECode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVCECode.Location = New System.Drawing.Point(145, 21)
        Me.txtVCECode.Name = "txtVCECode"
        Me.txtVCECode.Size = New System.Drawing.Size(286, 22)
        Me.txtVCECode.TabIndex = 1523
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label2.Location = New System.Drawing.Point(28, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 16)
        Me.Label2.TabIndex = 1521
        Me.Label2.Text = "Customer Name :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label4.Location = New System.Drawing.Point(32, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 16)
        Me.Label4.TabIndex = 1522
        Me.Label4.Text = "Customer Code :"
        '
        'txtStatus
        '
        Me.txtStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtStatus.Enabled = False
        Me.txtStatus.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(1144, 94)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(132, 22)
        Me.txtStatus.TabIndex = 1520
        Me.txtStatus.Text = "Open"
        '
        'txtTransNum
        '
        Me.txtTransNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTransNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransNum.Location = New System.Drawing.Point(1144, 18)
        Me.txtTransNum.Name = "txtTransNum"
        Me.txtTransNum.Size = New System.Drawing.Size(132, 22)
        Me.txtTransNum.TabIndex = 1518
        Me.txtTransNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(1085, 21)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(58, 16)
        Me.Label13.TabIndex = 1517
        Me.Label13.Text = "RE No. :"
        '
        'txtPropName
        '
        Me.txtPropName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPropName.Location = New System.Drawing.Point(145, 97)
        Me.txtPropName.Name = "txtPropName"
        Me.txtPropName.Size = New System.Drawing.Size(286, 22)
        Me.txtPropName.TabIndex = 1311
        '
        'txtPropCode
        '
        Me.txtPropCode.Enabled = False
        Me.txtPropCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPropCode.Location = New System.Drawing.Point(145, 71)
        Me.txtPropCode.Name = "txtPropCode"
        Me.txtPropCode.Size = New System.Drawing.Size(286, 22)
        Me.txtPropCode.TabIndex = 1310
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label1.Location = New System.Drawing.Point(57, 100)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 16)
        Me.Label1.TabIndex = 1308
        Me.Label1.Text = "Description :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label3.Location = New System.Drawing.Point(38, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 16)
        Me.Label3.TabIndex = 1309
        Me.Label3.Text = "Property Code :"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSearch, Me.tsbNew, Me.tsbEdit, Me.tsbSave, Me.tsbCancel, Me.ToolStripSeparator1, Me.tsbPrint, Me.tsbReports, Me.ToolStripSeparator2, Me.tsbPrevious, Me.tsbNext, Me.ToolStripSeparator3, Me.tsbClose, Me.tsbExit, Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1321, 40)
        Me.ToolStrip1.TabIndex = 1320
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
        Me.tsbCancel.Text = "Inactive"
        Me.tsbCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 40)
        '
        'tsbPrint
        '
        Me.tsbPrint.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReservationAgreementToolStripMenuItem, Me.ContractToSellToolStripMenuItem, Me.ToolStripSeparator5, Me.WaiverToolStripMenuItem, Me.BuyersInformationSheetToolStripMenuItem, Me.CreditApprovalMemorandumFormToolStripMenuItem, Me.ListOfRequirementsToolStripMenuItem, Me.ToolStripSeparator4, Me.NoticeOfDisapprovalForCAMToolStripMenuItem, Me.NoticeOfUnpaidEquityToolStripMenuItem, Me.NoticeOfNonComplianceToolStripMenuItem, Me.NoticeOfCancellationForDocumentationToolStripMenuItem})
        Me.tsbPrint.ForeColor = System.Drawing.Color.White
        Me.tsbPrint.Image = Global.jade.My.Resources.Resources.printer_circle_blue_512
        Me.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPrint.Name = "tsbPrint"
        Me.tsbPrint.Size = New System.Drawing.Size(48, 37)
        Me.tsbPrint.Text = "Print"
        Me.tsbPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ReservationAgreementToolStripMenuItem
        '
        Me.ReservationAgreementToolStripMenuItem.Name = "ReservationAgreementToolStripMenuItem"
        Me.ReservationAgreementToolStripMenuItem.Size = New System.Drawing.Size(296, 22)
        Me.ReservationAgreementToolStripMenuItem.Text = "Reservation Agreement"
        '
        'ContractToSellToolStripMenuItem
        '
        Me.ContractToSellToolStripMenuItem.Name = "ContractToSellToolStripMenuItem"
        Me.ContractToSellToolStripMenuItem.Size = New System.Drawing.Size(296, 22)
        Me.ContractToSellToolStripMenuItem.Text = "Contract to Sell"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(293, 6)
        '
        'WaiverToolStripMenuItem
        '
        Me.WaiverToolStripMenuItem.Name = "WaiverToolStripMenuItem"
        Me.WaiverToolStripMenuItem.Size = New System.Drawing.Size(296, 22)
        Me.WaiverToolStripMenuItem.Text = "Waiver"
        '
        'BuyersInformationSheetToolStripMenuItem
        '
        Me.BuyersInformationSheetToolStripMenuItem.Name = "BuyersInformationSheetToolStripMenuItem"
        Me.BuyersInformationSheetToolStripMenuItem.Size = New System.Drawing.Size(296, 22)
        Me.BuyersInformationSheetToolStripMenuItem.Text = "Buyers Information Sheet"
        '
        'CreditApprovalMemorandumFormToolStripMenuItem
        '
        Me.CreditApprovalMemorandumFormToolStripMenuItem.Name = "CreditApprovalMemorandumFormToolStripMenuItem"
        Me.CreditApprovalMemorandumFormToolStripMenuItem.Size = New System.Drawing.Size(296, 22)
        Me.CreditApprovalMemorandumFormToolStripMenuItem.Text = "Credit Approval Memorandum Form"
        '
        'ListOfRequirementsToolStripMenuItem
        '
        Me.ListOfRequirementsToolStripMenuItem.Name = "ListOfRequirementsToolStripMenuItem"
        Me.ListOfRequirementsToolStripMenuItem.Size = New System.Drawing.Size(296, 22)
        Me.ListOfRequirementsToolStripMenuItem.Text = "List of Requirements"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(293, 6)
        '
        'NoticeOfDisapprovalForCAMToolStripMenuItem
        '
        Me.NoticeOfDisapprovalForCAMToolStripMenuItem.Name = "NoticeOfDisapprovalForCAMToolStripMenuItem"
        Me.NoticeOfDisapprovalForCAMToolStripMenuItem.Size = New System.Drawing.Size(296, 22)
        Me.NoticeOfDisapprovalForCAMToolStripMenuItem.Text = "Notice of Disapproval for CAM"
        '
        'NoticeOfUnpaidEquityToolStripMenuItem
        '
        Me.NoticeOfUnpaidEquityToolStripMenuItem.Name = "NoticeOfUnpaidEquityToolStripMenuItem"
        Me.NoticeOfUnpaidEquityToolStripMenuItem.Size = New System.Drawing.Size(296, 22)
        Me.NoticeOfUnpaidEquityToolStripMenuItem.Text = "Notice of Unpaid Equity"
        '
        'NoticeOfNonComplianceToolStripMenuItem
        '
        Me.NoticeOfNonComplianceToolStripMenuItem.Name = "NoticeOfNonComplianceToolStripMenuItem"
        Me.NoticeOfNonComplianceToolStripMenuItem.Size = New System.Drawing.Size(296, 22)
        Me.NoticeOfNonComplianceToolStripMenuItem.Text = "Notice of Non-Compliance"
        '
        'NoticeOfCancellationForDocumentationToolStripMenuItem
        '
        Me.NoticeOfCancellationForDocumentationToolStripMenuItem.Name = "NoticeOfCancellationForDocumentationToolStripMenuItem"
        Me.NoticeOfCancellationForDocumentationToolStripMenuItem.Size = New System.Drawing.Size(296, 22)
        Me.NoticeOfCancellationForDocumentationToolStripMenuItem.Text = "Notice of Cancellation for Documentation"
        '
        'tsbReports
        '
        Me.tsbReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AccountMonitoringReservedToolStripMenuItem, Me.ListOfAvailableUnitsToolStripMenuItem, Me.InventoryPropertyToolStripMenuItem})
        Me.tsbReports.ForeColor = System.Drawing.Color.White
        Me.tsbReports.Image = Global.jade.My.Resources.Resources.finance_report_infographic_512
        Me.tsbReports.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbReports.Name = "tsbReports"
        Me.tsbReports.Size = New System.Drawing.Size(60, 37)
        Me.tsbReports.Text = "Reports"
        Me.tsbReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'AccountMonitoringReservedToolStripMenuItem
        '
        Me.AccountMonitoringReservedToolStripMenuItem.Name = "AccountMonitoringReservedToolStripMenuItem"
        Me.AccountMonitoringReservedToolStripMenuItem.Size = New System.Drawing.Size(240, 22)
        Me.AccountMonitoringReservedToolStripMenuItem.Text = "Account Monitoring (Reserved)"
        '
        'ListOfAvailableUnitsToolStripMenuItem
        '
        Me.ListOfAvailableUnitsToolStripMenuItem.Name = "ListOfAvailableUnitsToolStripMenuItem"
        Me.ListOfAvailableUnitsToolStripMenuItem.Size = New System.Drawing.Size(240, 22)
        Me.ListOfAvailableUnitsToolStripMenuItem.Text = "List of Available Units"
        '
        'InventoryPropertyToolStripMenuItem
        '
        Me.InventoryPropertyToolStripMenuItem.Name = "InventoryPropertyToolStripMenuItem"
        Me.InventoryPropertyToolStripMenuItem.Size = New System.Drawing.Size(240, 22)
        Me.InventoryPropertyToolStripMenuItem.Text = "Inventory Property"
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
        'ToolStripSplitButton1
        '
        Me.ToolStripSplitButton1.Name = "ToolStripSplitButton1"
        Me.ToolStripSplitButton1.Size = New System.Drawing.Size(23, 23)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.AutoSize = False
        Me.ToolStripButton1.ForeColor = System.Drawing.Color.White
        Me.ToolStripButton1.Image = Global.jade.My.Resources.Resources.exit_button_icon_18
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(50, 35)
        Me.ToolStripButton1.Text = "Generate"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton1.ToolTipText = "Generate"
        '
        'frmRE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1321, 690)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmRE"
        Me.Text = "Real Estate"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.nupCommission, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tcPayment.ResumeLayout(False)
        Me.tpCash.ResumeLayout(False)
        Me.tpCash.PerformLayout()
        CType(Me.nupWithInDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nupTCPDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpDeferred.ResumeLayout(False)
        Me.tpDeferred.PerformLayout()
        CType(Me.dgvDeferredSched, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nupDeferredTerms, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpFinancing.ResumeLayout(False)
        Me.tpFinancing.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.nupDPDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nupTerm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDPSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nupDiscountRate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tcFinancing.ResumeLayout(False)
        Me.tpInhouse.ResumeLayout(False)
        Me.tpInhouse.PerformLayout()
        CType(Me.dgvInhouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nupInhouseTotalMonths, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nupInhousePaymentPerYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nupInhouseYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nupInhouseRate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpPagibig.ResumeLayout(False)
        Me.tpPagibig.PerformLayout()
        CType(Me.dgvPagibig, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.nupPagibigRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nupPagibigTerms, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpBank.ResumeLayout(False)
        Me.tpBank.PerformLayout()
        CType(Me.dgvBank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nupBankTerms, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.nupBankRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nupDPRate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpAccountingEntries.ResumeLayout(False)
        Me.tpAccountingEntries.PerformLayout()
        CType(Me.dgvEntry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtPropName As TextBox
    Friend WithEvents txtPropCode As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents tsbSearch As ToolStripButton
    Friend WithEvents tsbNew As ToolStripButton
    Friend WithEvents tsbEdit As ToolStripButton
    Friend WithEvents tsbSave As ToolStripButton
    Friend WithEvents tsbCancel As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents tsbPrevious As ToolStripButton
    Friend WithEvents tsbNext As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents tsbClose As ToolStripButton
    Friend WithEvents tsbExit As ToolStripButton
    Friend WithEvents txtTransNum As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents btnVCE As Button
    Friend WithEvents btnSearchVCE As Button
    Friend WithEvents txtVCEName As TextBox
    Friend WithEvents txtVCECode As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents dtpStart As DateTimePicker
    Friend WithEvents txtStatus As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents txtTCP As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents txtVAT As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtMisc As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtSelling As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents dtpDocDate As DateTimePicker
    Friend WithEvents btnLPM As Button
    Friend WithEvents btnSearchProp As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents txtRemarks As TextBox
    Friend WithEvents dgvDPSchedule As DataGridView
    Friend WithEvents Label21 As Label
    Friend WithEvents nupTerm As NumericUpDown
    Friend WithEvents tsbPrint As ToolStripSplitButton
    Friend WithEvents ToolStripSplitButton1 As ToolStripSplitButton
    Friend WithEvents tsbReports As ToolStripDropDownButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents Label22 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents txtTotalDP As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtReserve As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtLoanable As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txtDiscount As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtMonthly As TextBox
    Friend WithEvents chkVATInc As CheckBox
    Friend WithEvents chkVATable As CheckBox
    Friend WithEvents ReservationAgreementToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContractToSellToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NoticeOfUnpaidEquityToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NoticeOfDisapprovalForCAMToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents WaiverToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label7 As Label
    Friend WithEvents txtAddl As TextBox
    Friend WithEvents nupDPRate As NumericUpDown
    Friend WithEvents miscFeeRate As Label
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents NoticeOfNonComplianceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NoticeOfCancellationForDocumentationToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents BuyersInformationSheetToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CreditApprovalMemorandumFormToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ListOfRequirementsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AccountMonitoringReservedToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents nupDiscountRate As NumericUpDown
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents cbFinancingMode As ComboBox
    Friend WithEvents tcFinancing As TabControl
    Friend WithEvents tpInhouse As TabPage
    Friend WithEvents nupInhouseTotalMonths As NumericUpDown
    Friend WithEvents Label28 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents txtInhouseMonthly As TextBox
    Friend WithEvents nupInhousePaymentPerYear As NumericUpDown
    Friend WithEvents Label26 As Label
    Friend WithEvents nupInhouseYear As NumericUpDown
    Friend WithEvents Label25 As Label
    Friend WithEvents nupInhouseRate As NumericUpDown
    Friend WithEvents Label24 As Label
    Friend WithEvents tpPagibig As TabPage
    Friend WithEvents tpBank As TabPage
    Friend WithEvents tcPayment As TabControl
    Friend WithEvents tpCash As TabPage
    Friend WithEvents tpDeferred As TabPage
    Friend WithEvents tpFinancing As TabPage
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label29 As Label
    Friend WithEvents cbPaymentType As ComboBox
    Friend WithEvents lvBankComputation As ListView
    Friend WithEvents Label33 As Label
    Friend WithEvents rbTCPWithin As RadioButton
    Friend WithEvents rbTCPSpot As RadioButton
    Friend WithEvents Label34 As Label
    Friend WithEvents txtTCPReserve As TextBox
    Friend WithEvents nupTCPDiscount As NumericUpDown
    Friend WithEvents Label30 As Label
    Friend WithEvents Label31 As Label
    Friend WithEvents txtTCPBalance As TextBox
    Friend WithEvents Label32 As Label
    Friend WithEvents txtTCPDiscount As TextBox
    Friend WithEvents Label35 As Label
    Friend WithEvents dgvDeferredSched As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents Label39 As Label
    Friend WithEvents dtpDeferredEnd As DateTimePicker
    Friend WithEvents Label38 As Label
    Friend WithEvents txtDeferredMonthly As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents txtDeferredReserve As TextBox
    Friend WithEvents Label36 As Label
    Friend WithEvents Label37 As Label
    Friend WithEvents nupDeferredTerms As NumericUpDown
    Friend WithEvents dtpDeferredStart As DateTimePicker
    Friend WithEvents Label40 As Label
    Friend WithEvents rbCashWithin As RadioButton
    Friend WithEvents rbSpotDp As RadioButton
    Friend WithEvents txtBalanceDP As TextBox
    Friend WithEvents Label41 As Label
    Friend WithEvents rbTerm As RadioButton
    Friend WithEvents nupBankRate As NumericUpDown
    Friend WithEvents Label42 As Label
    Friend WithEvents chTerm As ColumnHeader
    Friend WithEvents chAmortization As ColumnHeader
    Friend WithEvents chNDI As ColumnHeader
    Friend WithEvents lblCashPayableUntil As Label
    Friend WithEvents Label43 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents lblDPPayableUntil As Label
    Friend WithEvents Label45 As Label
    Friend WithEvents chCount As DataGridViewTextBoxColumn
    Friend WithEvents chDateDue As DataGridViewTextBoxColumn
    Friend WithEvents chAmount As DataGridViewTextBoxColumn
    Friend WithEvents dgvInhouse As DataGridView
    Friend WithEvents nupPagibigRate As NumericUpDown
    Friend WithEvents Label44 As Label
    Friend WithEvents lvPagibig As ListView
    Friend WithEvents chPagibigTerms As ColumnHeader
    Friend WithEvents chPagibigAmort As ColumnHeader
    Friend WithEvents chPagibigNDI As ColumnHeader
    Friend WithEvents dgcInHouseNo As DataGridViewTextBoxColumn
    Friend WithEvents dgcInHouseDueDate As DataGridViewTextBoxColumn
    Friend WithEvents dgcInhouseInterest As DataGridViewTextBoxColumn
    Friend WithEvents dgcInHousePrincipal As DataGridViewTextBoxColumn
    Friend WithEvents dgcInHouseTotal As DataGridViewTextBoxColumn
    Friend WithEvents dtpInhouseStart As DateTimePicker
    Friend WithEvents Label46 As Label
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents Label51 As Label
    Friend WithEvents Label49 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents txtSalesName As TextBox
    Friend WithEvents txtSalesCode As TextBox
    Friend WithEvents Label50 As Label
    Friend WithEvents txtCommission As TextBox
    Friend WithEvents Label48 As Label
    Friend WithEvents nupCommission As NumericUpDown
    Friend WithEvents Label47 As Label
    Friend WithEvents txtNetSelling As TextBox
    Friend WithEvents Label52 As Label
    Friend WithEvents ListOfAvailableUnitsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label54 As Label
    Friend WithEvents txtPagibiLoanDiff As TextBox
    Friend WithEvents Label53 As Label
    Friend WithEvents txtPagibigLoanable As TextBox
    Friend WithEvents nupPagibigTerms As NumericUpDown
    Friend WithEvents Label55 As Label
    Friend WithEvents dtpPagibigStart As DateTimePicker
    Friend WithEvents Label56 As Label
    Friend WithEvents Label57 As Label
    Friend WithEvents txtPagibigMonthly As TextBox
    Friend WithEvents dgvPagibig As DataGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents dgvBank As DataGridView
    Friend WithEvents nupBankTerms As NumericUpDown
    Friend WithEvents Label58 As Label
    Friend WithEvents dtpBankStart As DateTimePicker
    Friend WithEvents Label59 As Label
    Friend WithEvents Label60 As Label
    Friend WithEvents txtBankMonthly As TextBox
    Friend WithEvents Label61 As Label
    Friend WithEvents txtBankLoanDiff As TextBox
    Friend WithEvents Label62 As Label
    Friend WithEvents txtBankLoanable As TextBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lblPagibigUntil As Label
    Friend WithEvents Label64 As Label
    Friend WithEvents lblBankUntil As Label
    Friend WithEvents Label65 As Label
    Friend WithEvents btnViewLedger As Button
    Friend WithEvents dgcPagibigPaymentNo As DataGridViewTextBoxColumn
    Friend WithEvents dgcPagibigDuedate As DataGridViewTextBoxColumn
    Friend WithEvents dgcPagibigAmount As DataGridViewTextBoxColumn
    Friend WithEvents dgcBankPaymentNo As DataGridViewTextBoxColumn
    Friend WithEvents dgcBankDuedate As DataGridViewTextBoxColumn
    Friend WithEvents dgcBankAmount As DataGridViewTextBoxColumn
    Friend WithEvents lblInhouseUntil As Label
    Friend WithEvents Label66 As Label
    Friend WithEvents nupWithInDays As NumericUpDown
    Friend WithEvents nupDPDays As NumericUpDown
    Friend WithEvents tpAccountingEntries As TabPage
    Friend WithEvents dgvEntry As DataGridView
    Friend WithEvents chAccntCode As DataGridViewTextBoxColumn
    Friend WithEvents chAccntTitle As DataGridViewTextBoxColumn
    Friend WithEvents chDebit As DataGridViewTextBoxColumn
    Friend WithEvents chCredit As DataGridViewTextBoxColumn
    Friend WithEvents chParticulars As DataGridViewTextBoxColumn
    Friend WithEvents chVCECode As DataGridViewTextBoxColumn
    Friend WithEvents chVCEName As DataGridViewTextBoxColumn
    Friend WithEvents chRefNo As DataGridViewTextBoxColumn
    Friend WithEvents txtTotalCredit As TextBox
    Friend WithEvents txtTotalDebit As TextBox
    Friend WithEvents Label63 As Label
    Friend WithEvents Label67 As Label
    Friend WithEvents txtSJNo As TextBox
    Friend WithEvents Label68 As Label
    Friend WithEvents txtSellingCommission As TextBox
    Friend WithEvents InventoryPropertyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripButton1 As ToolStripButton
End Class
