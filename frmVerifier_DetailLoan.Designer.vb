<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVerifier_DetailLoan
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVerifier_DetailLoan))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SOAToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtVCECode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtVCEName = New System.Windows.Forms.TextBox()
        Me.cmbLoan = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.colLoanID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPrincipal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colInterest = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chMaturityDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lvCoMaker = New System.Windows.Forms.ListView()
        Me.chCoMakerID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chLoanID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chMemNo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chMemName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtInterestRate = New System.Windows.Forms.TextBox()
        Me.btnNotes = New System.Windows.Forms.Button()
        Me.dgvLedger = New System.Windows.Forms.DataGridView()
        Me.colNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRefType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRefTransID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRefID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colLoanAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colLoanPayment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colLoanBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIntPayment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colServiceFee = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTotalPayment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colWhoCreated = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnPrintTransaction = New System.Windows.Forms.Button()
        Me.txtLine = New System.Windows.Forms.TextBox()
        Me.btnMin = New System.Windows.Forms.Button()
        Me.txtPage = New System.Windows.Forms.TextBox()
        Me.btnMax = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblBalance = New System.Windows.Forms.Label()
        Me.txtDateReleased = New System.Windows.Forms.TextBox()
        Me.txtInterestAmount = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtDateMaturity = New System.Windows.Forms.TextBox()
        Me.txtLoanAmount = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDateStart = New System.Windows.Forms.TextBox()
        Me.txtLoanDate = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmsMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvLedger, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.cmsMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1190, 24)
        Me.MenuStrip1.TabIndex = 1194
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SOAToolStripMenuItem})
        Me.PrintToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PrintToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.PrintToolStripMenuItem.Text = "Print"
        '
        'SOAToolStripMenuItem
        '
        Me.SOAToolStripMenuItem.Name = "SOAToolStripMenuItem"
        Me.SOAToolStripMenuItem.Size = New System.Drawing.Size(98, 22)
        Me.SOAToolStripMenuItem.Text = "SOA"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 1195
        Me.Label1.Text = "VCE Code:"
        '
        'txtVCECode
        '
        Me.txtVCECode.BackColor = System.Drawing.Color.White
        Me.txtVCECode.Location = New System.Drawing.Point(71, 2)
        Me.txtVCECode.Name = "txtVCECode"
        Me.txtVCECode.ReadOnly = True
        Me.txtVCECode.Size = New System.Drawing.Size(224, 20)
        Me.txtVCECode.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 1195
        Me.Label2.Text = "VCE Name:"
        '
        'txtVCEName
        '
        Me.txtVCEName.BackColor = System.Drawing.Color.White
        Me.txtVCEName.Location = New System.Drawing.Point(71, 24)
        Me.txtVCEName.Name = "txtVCEName"
        Me.txtVCEName.ReadOnly = True
        Me.txtVCEName.Size = New System.Drawing.Size(224, 20)
        Me.txtVCEName.TabIndex = 1
        '
        'cmbLoan
        '
        Me.cmbLoan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLoan.FormattingEnabled = True
        Me.cmbLoan.Location = New System.Drawing.Point(71, 46)
        Me.cmbLoan.Name = "cmbLoan"
        Me.cmbLoan.Size = New System.Drawing.Size(224, 21)
        Me.cmbLoan.TabIndex = 1196
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 1195
        Me.Label3.Text = "Loan:"
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.AllowUserToResizeColumns = False
        Me.dgvList.AllowUserToResizeRows = False
        Me.dgvList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvList.BackgroundColor = System.Drawing.Color.White
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colLoanID, Me.colPrincipal, Me.colInterest, Me.chMaturityDate})
        Me.dgvList.Location = New System.Drawing.Point(2, 69)
        Me.dgvList.MultiSelect = False
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.RowHeadersVisible = False
        Me.dgvList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvList.Size = New System.Drawing.Size(293, 458)
        Me.dgvList.TabIndex = 1197
        '
        'colLoanID
        '
        Me.colLoanID.HeaderText = "Loan ID"
        Me.colLoanID.Name = "colLoanID"
        Me.colLoanID.ReadOnly = True
        Me.colLoanID.Width = 70
        '
        'colPrincipal
        '
        Me.colPrincipal.HeaderText = "Principal"
        Me.colPrincipal.Name = "colPrincipal"
        Me.colPrincipal.ReadOnly = True
        Me.colPrincipal.Width = 120
        '
        'colInterest
        '
        Me.colInterest.HeaderText = "Interest"
        Me.colInterest.Name = "colInterest"
        Me.colInterest.ReadOnly = True
        Me.colInterest.Width = 80
        '
        'chMaturityDate
        '
        Me.chMaturityDate.HeaderText = "Maturity Date"
        Me.chMaturityDate.Name = "chMaturityDate"
        Me.chMaturityDate.ReadOnly = True
        Me.chMaturityDate.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.lvCoMaker)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.txtInterestRate)
        Me.Panel1.Controls.Add(Me.btnNotes)
        Me.Panel1.Controls.Add(Me.dgvLedger)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.btnPrintTransaction)
        Me.Panel1.Controls.Add(Me.txtLine)
        Me.Panel1.Controls.Add(Me.btnMin)
        Me.Panel1.Controls.Add(Me.txtPage)
        Me.Panel1.Controls.Add(Me.btnMax)
        Me.Panel1.Controls.Add(Me.btnNext)
        Me.Panel1.Controls.Add(Me.btnPrev)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.lblBalance)
        Me.Panel1.Controls.Add(Me.txtDateReleased)
        Me.Panel1.Controls.Add(Me.txtInterestAmount)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtDateMaturity)
        Me.Panel1.Controls.Add(Me.txtLoanAmount)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtDateStart)
        Me.Panel1.Controls.Add(Me.txtLoanDate)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Location = New System.Drawing.Point(304, 27)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(883, 576)
        Me.Panel1.TabIndex = 1198
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(536, 5)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(59, 13)
        Me.Label13.TabIndex = 1432
        Me.Label13.Text = "Co-Maker :"
        '
        'lvCoMaker
        '
        Me.lvCoMaker.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvCoMaker.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chCoMakerID, Me.chLoanID, Me.chMemNo, Me.chMemName})
        Me.lvCoMaker.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lvCoMaker.FullRowSelect = True
        Me.lvCoMaker.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvCoMaker.Location = New System.Drawing.Point(534, 23)
        Me.lvCoMaker.MultiSelect = False
        Me.lvCoMaker.Name = "lvCoMaker"
        Me.lvCoMaker.Size = New System.Drawing.Size(344, 88)
        Me.lvCoMaker.TabIndex = 1431
        Me.lvCoMaker.UseCompatibleStateImageBehavior = False
        Me.lvCoMaker.View = System.Windows.Forms.View.Details
        '
        'chCoMakerID
        '
        Me.chCoMakerID.Width = 0
        '
        'chLoanID
        '
        Me.chLoanID.Text = "Code"
        Me.chLoanID.Width = 0
        '
        'chMemNo
        '
        Me.chMemNo.Text = "Mem No"
        Me.chMemNo.Width = 80
        '
        'chMemName
        '
        Me.chMemName.Text = "Mem Name"
        Me.chMemName.Width = 230
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(245, 26)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(51, 13)
        Me.Label12.TabIndex = 1429
        Me.Label12.Text = "Int. Rate:"
        '
        'txtInterestRate
        '
        Me.txtInterestRate.BackColor = System.Drawing.Color.White
        Me.txtInterestRate.Location = New System.Drawing.Point(324, 23)
        Me.txtInterestRate.Name = "txtInterestRate"
        Me.txtInterestRate.ReadOnly = True
        Me.txtInterestRate.Size = New System.Drawing.Size(149, 20)
        Me.txtInterestRate.TabIndex = 1430
        '
        'btnNotes
        '
        Me.btnNotes.Image = CType(resources.GetObject("btnNotes.Image"), System.Drawing.Image)
        Me.btnNotes.Location = New System.Drawing.Point(479, 1)
        Me.btnNotes.Name = "btnNotes"
        Me.btnNotes.Size = New System.Drawing.Size(51, 64)
        Me.btnNotes.TabIndex = 1428
        Me.btnNotes.Text = "Notes"
        Me.btnNotes.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnNotes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnNotes.UseVisualStyleBackColor = True
        '
        'dgvLedger
        '
        Me.dgvLedger.AllowUserToAddRows = False
        Me.dgvLedger.AllowUserToDeleteRows = False
        Me.dgvLedger.AllowUserToResizeColumns = False
        Me.dgvLedger.AllowUserToResizeRows = False
        Me.dgvLedger.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvLedger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvLedger.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colNum, Me.colRefType, Me.colRefTransID, Me.colRefID, Me.colDate, Me.colLoanAmount, Me.colLoanPayment, Me.colLoanBalance, Me.colIntPayment, Me.colServiceFee, Me.colTotalPayment, Me.colWhoCreated})
        Me.dgvLedger.Location = New System.Drawing.Point(2, 114)
        Me.dgvLedger.MultiSelect = False
        Me.dgvLedger.Name = "dgvLedger"
        Me.dgvLedger.ReadOnly = True
        Me.dgvLedger.RowHeadersVisible = False
        Me.dgvLedger.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvLedger.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLedger.Size = New System.Drawing.Size(876, 428)
        Me.dgvLedger.TabIndex = 0
        '
        'colNum
        '
        Me.colNum.HeaderText = "No."
        Me.colNum.Name = "colNum"
        Me.colNum.ReadOnly = True
        Me.colNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colNum.Width = 30
        '
        'colRefType
        '
        Me.colRefType.HeaderText = "RefType"
        Me.colRefType.Name = "colRefType"
        Me.colRefType.ReadOnly = True
        Me.colRefType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colRefType.Width = 50
        '
        'colRefTransID
        '
        Me.colRefTransID.HeaderText = "RefTransID"
        Me.colRefTransID.Name = "colRefTransID"
        Me.colRefTransID.ReadOnly = True
        Me.colRefTransID.Visible = False
        '
        'colRefID
        '
        Me.colRefID.HeaderText = "RefID"
        Me.colRefID.Name = "colRefID"
        Me.colRefID.ReadOnly = True
        Me.colRefID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colRefID.Width = 60
        '
        'colDate
        '
        Me.colDate.HeaderText = "Date"
        Me.colDate.Name = "colDate"
        Me.colDate.ReadOnly = True
        Me.colDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colDate.Width = 70
        '
        'colLoanAmount
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colLoanAmount.DefaultCellStyle = DataGridViewCellStyle1
        Me.colLoanAmount.HeaderText = "Loan Amount"
        Me.colLoanAmount.Name = "colLoanAmount"
        Me.colLoanAmount.ReadOnly = True
        Me.colLoanAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colLoanPayment
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colLoanPayment.DefaultCellStyle = DataGridViewCellStyle2
        Me.colLoanPayment.HeaderText = "Loan Payment"
        Me.colLoanPayment.Name = "colLoanPayment"
        Me.colLoanPayment.ReadOnly = True
        Me.colLoanPayment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colLoanBalance
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colLoanBalance.DefaultCellStyle = DataGridViewCellStyle3
        Me.colLoanBalance.HeaderText = "Loan Balance"
        Me.colLoanBalance.Name = "colLoanBalance"
        Me.colLoanBalance.ReadOnly = True
        Me.colLoanBalance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colIntPayment
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colIntPayment.DefaultCellStyle = DataGridViewCellStyle4
        Me.colIntPayment.HeaderText = "Int. Payment"
        Me.colIntPayment.Name = "colIntPayment"
        Me.colIntPayment.ReadOnly = True
        Me.colIntPayment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colIntPayment.Width = 80
        '
        'colServiceFee
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colServiceFee.DefaultCellStyle = DataGridViewCellStyle5
        Me.colServiceFee.HeaderText = "Service Fee"
        Me.colServiceFee.Name = "colServiceFee"
        Me.colServiceFee.ReadOnly = True
        Me.colServiceFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colServiceFee.Width = 80
        '
        'colTotalPayment
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colTotalPayment.DefaultCellStyle = DataGridViewCellStyle6
        Me.colTotalPayment.HeaderText = "Total Payment"
        Me.colTotalPayment.Name = "colTotalPayment"
        Me.colTotalPayment.ReadOnly = True
        Me.colTotalPayment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colWhoCreated
        '
        Me.colWhoCreated.HeaderText = "Who Created"
        Me.colWhoCreated.Name = "colWhoCreated"
        Me.colWhoCreated.ReadOnly = True
        Me.colWhoCreated.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label11.Location = New System.Drawing.Point(760, 553)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 13)
        Me.Label11.TabIndex = 1427
        Me.Label11.Text = "Line No. :"
        '
        'btnPrintTransaction
        '
        Me.btnPrintTransaction.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrintTransaction.Font = New System.Drawing.Font("Wingdings 2", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnPrintTransaction.Location = New System.Drawing.Point(850, 543)
        Me.btnPrintTransaction.Name = "btnPrintTransaction"
        Me.btnPrintTransaction.Size = New System.Drawing.Size(30, 30)
        Me.btnPrintTransaction.TabIndex = 1426
        Me.btnPrintTransaction.Text = ""
        Me.btnPrintTransaction.UseVisualStyleBackColor = True
        '
        'txtLine
        '
        Me.txtLine.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLine.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtLine.Location = New System.Drawing.Point(819, 544)
        Me.txtLine.MinimumSize = New System.Drawing.Size(30, 28)
        Me.txtLine.Name = "txtLine"
        Me.txtLine.ReadOnly = True
        Me.txtLine.Size = New System.Drawing.Size(30, 20)
        Me.txtLine.TabIndex = 1425
        Me.txtLine.Text = "0"
        Me.txtLine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnMin
        '
        Me.btnMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnMin.Font = New System.Drawing.Font("Webdings", 12.0!)
        Me.btnMin.Location = New System.Drawing.Point(1, 543)
        Me.btnMin.Name = "btnMin"
        Me.btnMin.Size = New System.Drawing.Size(30, 30)
        Me.btnMin.TabIndex = 1424
        Me.btnMin.Text = ""
        Me.btnMin.UseVisualStyleBackColor = True
        '
        'txtPage
        '
        Me.txtPage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtPage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPage.Location = New System.Drawing.Point(62, 544)
        Me.txtPage.MinimumSize = New System.Drawing.Size(30, 28)
        Me.txtPage.Name = "txtPage"
        Me.txtPage.ReadOnly = True
        Me.txtPage.Size = New System.Drawing.Size(30, 20)
        Me.txtPage.TabIndex = 1423
        Me.txtPage.Text = "1"
        Me.txtPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnMax
        '
        Me.btnMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnMax.Font = New System.Drawing.Font("Webdings", 12.0!)
        Me.btnMax.Location = New System.Drawing.Point(123, 543)
        Me.btnMax.Name = "btnMax"
        Me.btnMax.Size = New System.Drawing.Size(30, 30)
        Me.btnMax.TabIndex = 1422
        Me.btnMax.Text = ""
        Me.btnMax.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Font = New System.Drawing.Font("Webdings", 12.0!)
        Me.btnNext.Location = New System.Drawing.Point(93, 543)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(30, 30)
        Me.btnNext.TabIndex = 1421
        Me.btnNext.Text = "4"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnPrev
        '
        Me.btnPrev.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrev.Font = New System.Drawing.Font("Webdings", 12.0!)
        Me.btnPrev.Location = New System.Drawing.Point(31, 543)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(30, 30)
        Me.btnPrev.TabIndex = 1420
        Me.btnPrev.Text = ""
        Me.btnPrev.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Black
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(241, 50)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(49, 13)
        Me.Label10.TabIndex = 1198
        Me.Label10.Text = "Balance:"
        '
        'lblBalance
        '
        Me.lblBalance.BackColor = System.Drawing.Color.Black
        Me.lblBalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!)
        Me.lblBalance.ForeColor = System.Drawing.Color.White
        Me.lblBalance.Location = New System.Drawing.Point(238, 47)
        Me.lblBalance.Name = "lblBalance"
        Me.lblBalance.Size = New System.Drawing.Size(236, 64)
        Me.lblBalance.TabIndex = 1199
        Me.lblBalance.Text = "0.00"
        Me.lblBalance.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'txtDateReleased
        '
        Me.txtDateReleased.BackColor = System.Drawing.Color.White
        Me.txtDateReleased.Location = New System.Drawing.Point(82, 91)
        Me.txtDateReleased.Name = "txtDateReleased"
        Me.txtDateReleased.ReadOnly = True
        Me.txtDateReleased.Size = New System.Drawing.Size(149, 20)
        Me.txtDateReleased.TabIndex = 1197
        '
        'txtInterestAmount
        '
        Me.txtInterestAmount.BackColor = System.Drawing.Color.White
        Me.txtInterestAmount.Location = New System.Drawing.Point(324, 2)
        Me.txtInterestAmount.Name = "txtInterestAmount"
        Me.txtInterestAmount.ReadOnly = True
        Me.txtInterestAmount.Size = New System.Drawing.Size(149, 20)
        Me.txtInterestAmount.TabIndex = 1197
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(3, 94)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(81, 13)
        Me.Label9.TabIndex = 1196
        Me.Label9.Text = "Released Date:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 13)
        Me.Label6.TabIndex = 1196
        Me.Label6.Text = "Loan Date:"
        '
        'txtDateMaturity
        '
        Me.txtDateMaturity.BackColor = System.Drawing.Color.White
        Me.txtDateMaturity.Location = New System.Drawing.Point(82, 69)
        Me.txtDateMaturity.Name = "txtDateMaturity"
        Me.txtDateMaturity.ReadOnly = True
        Me.txtDateMaturity.Size = New System.Drawing.Size(149, 20)
        Me.txtDateMaturity.TabIndex = 1197
        '
        'txtLoanAmount
        '
        Me.txtLoanAmount.BackColor = System.Drawing.Color.White
        Me.txtLoanAmount.Location = New System.Drawing.Point(82, 2)
        Me.txtLoanAmount.Name = "txtLoanAmount"
        Me.txtLoanAmount.ReadOnly = True
        Me.txtLoanAmount.Size = New System.Drawing.Size(149, 20)
        Me.txtLoanAmount.TabIndex = 1197
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 72)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 13)
        Me.Label8.TabIndex = 1196
        Me.Label8.Text = "Maturity Date:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(245, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 13)
        Me.Label5.TabIndex = 1196
        Me.Label5.Text = "Int. Amount:"
        '
        'txtDateStart
        '
        Me.txtDateStart.BackColor = System.Drawing.Color.White
        Me.txtDateStart.Location = New System.Drawing.Point(82, 46)
        Me.txtDateStart.Name = "txtDateStart"
        Me.txtDateStart.ReadOnly = True
        Me.txtDateStart.Size = New System.Drawing.Size(149, 20)
        Me.txtDateStart.TabIndex = 1197
        '
        'txtLoanDate
        '
        Me.txtLoanDate.BackColor = System.Drawing.Color.White
        Me.txtLoanDate.Location = New System.Drawing.Point(82, 24)
        Me.txtLoanDate.Name = "txtLoanDate"
        Me.txtLoanDate.ReadOnly = True
        Me.txtLoanDate.Size = New System.Drawing.Size(149, 20)
        Me.txtLoanDate.TabIndex = 1197
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 49)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 13)
        Me.Label7.TabIndex = 1196
        Me.Label7.Text = "Start Date:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 1196
        Me.Label4.Text = "Loan Amount:"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.txtVCECode)
        Me.Panel2.Controls.Add(Me.dgvList)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.cmbLoan)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.txtVCEName)
        Me.Panel2.Location = New System.Drawing.Point(3, 27)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(299, 531)
        Me.Panel2.TabIndex = 1199
        '
        'cmsMenu
        '
        Me.cmsMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewToolStripMenuItem})
        Me.cmsMenu.Name = "cmsMenu"
        Me.cmsMenu.Size = New System.Drawing.Size(100, 26)
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(99, 22)
        Me.ViewToolStripMenuItem.Text = "View"
        '
        'frmVerifier_DetailLoan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1190, 606)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmVerifier_DetailLoan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Loan"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgvLedger, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.cmsMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtVCECode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtVCEName As System.Windows.Forms.TextBox
    Friend WithEvents cmbLoan As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents dgvLedger As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtLoanDate As System.Windows.Forms.TextBox
    Friend WithEvents txtLoanAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtInterestAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtDateReleased As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtDateMaturity As System.Windows.Forms.TextBox
    Friend WithEvents txtDateStart As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblBalance As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnMin As System.Windows.Forms.Button
    Friend WithEvents txtPage As System.Windows.Forms.TextBox
    Friend WithEvents btnMax As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnPrev As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnPrintTransaction As System.Windows.Forms.Button
    Friend WithEvents txtLine As System.Windows.Forms.TextBox
    Friend WithEvents cmsMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents colNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRefType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRefTransID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRefID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLoanAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLoanPayment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLoanBalance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIntPayment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colServiceFee As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTotalPayment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colWhoCreated As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnNotes As System.Windows.Forms.Button
    Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SOAToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtInterestRate As System.Windows.Forms.TextBox
    Friend WithEvents colLoanID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPrincipal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colInterest As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chMaturityDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lvCoMaker As System.Windows.Forms.ListView
    Friend WithEvents chCoMakerID As System.Windows.Forms.ColumnHeader
    Friend WithEvents chLoanID As System.Windows.Forms.ColumnHeader
    Friend WithEvents chMemNo As System.Windows.Forms.ColumnHeader
    Friend WithEvents chMemName As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label13 As System.Windows.Forms.Label
End Class
