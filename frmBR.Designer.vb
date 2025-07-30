<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBR
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBR))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBookBal = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBankBal = New System.Windows.Forms.TextBox()
        Me.txtOC = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtVariance = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtAdjBookBal = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtAdjBankBal = New System.Windows.Forms.TextBox()
        Me.txtDIT = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbBankReconType = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cbBank = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tpDIT = New System.Windows.Forms.TabPage()
        Me.chkDITcheckAll = New System.Windows.Forms.CheckBox()
        Me.btnUpdateDeposit = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtDITSubTotal = New System.Windows.Forms.TextBox()
        Me.dgvDIT = New System.Windows.Forms.DataGridView()
        Me.dgcDITinclude = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgcDITID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDITdate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDITrefID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDITtype = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDITrefNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDITamount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDITsearch = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TxtTotalDIT = New System.Windows.Forms.TextBox()
        Me.tpOC = New System.Windows.Forms.TabPage()
        Me.txtOCsubtotal = New System.Windows.Forms.TextBox()
        Me.btnUpdateChecks = New System.Windows.Forms.Button()
        Me.chkOCcheckAll = New System.Windows.Forms.CheckBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.dgvOC = New System.Windows.Forms.DataGridView()
        Me.dgcOCinclude = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgcOCID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcOCdate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcOCrefID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcOCrefType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcOCrefNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcOCamount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chOC_CheckNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcOCvceName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcOCsearch = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.TxtTotalOutstanding = New System.Windows.Forms.TextBox()
        Me.tpJV = New System.Windows.Forms.TabPage()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtJVNO = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dgvJV = New System.Windows.Forms.DataGridView()
        Me.dgcJVparticulars = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcJVamount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcJVCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcJVtitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcJVvceCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcJVvceName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtJVRemarks = New System.Windows.Forms.TextBox()
        Me.btnAdjType = New System.Windows.Forms.Button()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.dgvCleared = New System.Windows.Forms.DataGridView()
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgcClearID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcClearDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcClearReID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcClearRefType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcClearRefNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcClearAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcClearCheckNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcClearName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcClearType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chkClear = New System.Windows.Forms.CheckBox()
        Me.btnReturnCheck = New System.Windows.Forms.Button()
        Me.txtClearedAmount = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtJVadjustment = New System.Windows.Forms.TextBox()
        Me.txtAccountTitle = New System.Windows.Forms.TextBox()
        Me.txtAccountCode = New System.Windows.Forms.TextBox()
        Me.txtBankAccountNo = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtBankID = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtTransNum = New System.Windows.Forms.TextBox()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.dtpDocDate = New System.Windows.Forms.DateTimePicker()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbSearch = New System.Windows.Forms.ToolStripButton()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrint = New System.Windows.Forms.ToolStripButton()
        Me.tsbReports = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrevious = New System.Windows.Forms.ToolStripButton()
        Me.tsbNext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.TabControl1.SuspendLayout()
        Me.tpDIT.SuspendLayout()
        CType(Me.dgvDIT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpOC.SuspendLayout()
        CType(Me.dgvOC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpJV.SuspendLayout()
        CType(Me.dgvJV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgvCleared, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(381, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 17)
        Me.Label3.TabIndex = 1352
        Me.Label3.Text = "Book Balance"
        '
        'txtBookBal
        '
        Me.txtBookBal.Location = New System.Drawing.Point(380, 38)
        Me.txtBookBal.Name = "txtBookBal"
        Me.txtBookBal.ReadOnly = True
        Me.txtBookBal.Size = New System.Drawing.Size(181, 21)
        Me.txtBookBal.TabIndex = 1351
        Me.txtBookBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(561, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 17)
        Me.Label1.TabIndex = 1357
        Me.Label1.Text = "Bank Balance"
        '
        'txtBankBal
        '
        Me.txtBankBal.Location = New System.Drawing.Point(564, 38)
        Me.txtBankBal.Name = "txtBankBal"
        Me.txtBankBal.Size = New System.Drawing.Size(182, 21)
        Me.txtBankBal.TabIndex = 1358
        Me.txtBankBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOC
        '
        Me.txtOC.Location = New System.Drawing.Point(564, 68)
        Me.txtOC.Name = "txtOC"
        Me.txtOC.ReadOnly = True
        Me.txtOC.Size = New System.Drawing.Size(182, 21)
        Me.txtOC.TabIndex = 1360
        Me.txtOC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label15.Location = New System.Drawing.Point(751, 149)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(64, 17)
        Me.Label15.TabIndex = 1388
        Me.Label15.Text = "Variance"
        '
        'txtVariance
        '
        Me.txtVariance.Location = New System.Drawing.Point(749, 169)
        Me.txtVariance.Name = "txtVariance"
        Me.txtVariance.ReadOnly = True
        Me.txtVariance.Size = New System.Drawing.Size(133, 21)
        Me.txtVariance.TabIndex = 1387
        Me.txtVariance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(381, 149)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(154, 17)
        Me.Label6.TabIndex = 1386
        Me.Label6.Text = "Adjusted Book Balance"
        '
        'txtAdjBookBal
        '
        Me.txtAdjBookBal.Location = New System.Drawing.Point(380, 169)
        Me.txtAdjBookBal.Name = "txtAdjBookBal"
        Me.txtAdjBookBal.ReadOnly = True
        Me.txtAdjBookBal.Size = New System.Drawing.Size(181, 21)
        Me.txtAdjBookBal.TabIndex = 1385
        Me.txtAdjBookBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label9.Location = New System.Drawing.Point(561, 149)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(154, 17)
        Me.Label9.TabIndex = 1364
        Me.Label9.Text = "Adjusted Bank Balance"
        '
        'txtAdjBankBal
        '
        Me.txtAdjBankBal.Location = New System.Drawing.Point(564, 169)
        Me.txtAdjBankBal.Name = "txtAdjBankBal"
        Me.txtAdjBankBal.ReadOnly = True
        Me.txtAdjBankBal.Size = New System.Drawing.Size(181, 21)
        Me.txtAdjBankBal.TabIndex = 1363
        Me.txtAdjBankBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDIT
        '
        Me.txtDIT.Location = New System.Drawing.Point(564, 92)
        Me.txtDIT.Name = "txtDIT"
        Me.txtDIT.ReadOnly = True
        Me.txtDIT.Size = New System.Drawing.Size(182, 21)
        Me.txtDIT.TabIndex = 1359
        Me.txtDIT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(425, 71)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(136, 17)
        Me.Label4.TabIndex = 1362
        Me.Label4.Text = "Outstanding Check :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(434, 93)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(127, 17)
        Me.Label2.TabIndex = 1361
        Me.Label2.Text = "Deposit in Transit :"
        '
        'cbBankReconType
        '
        Me.cbBankReconType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBankReconType.FormattingEnabled = True
        Me.cbBankReconType.Location = New System.Drawing.Point(163, 7)
        Me.cbBankReconType.Name = "cbBankReconType"
        Me.cbBankReconType.Size = New System.Drawing.Size(236, 24)
        Me.cbBankReconType.TabIndex = 1381
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label7.Location = New System.Drawing.Point(8, 10)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(149, 17)
        Me.Label7.TabIndex = 1359
        Me.Label7.Text = "Bank Recon Adj. Type"
        '
        'cbBank
        '
        Me.cbBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBank.FormattingEnabled = True
        Me.cbBank.Location = New System.Drawing.Point(109, 23)
        Me.cbBank.Name = "cbBank"
        Me.cbBank.Size = New System.Drawing.Size(249, 23)
        Me.cbBank.TabIndex = 1367
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(61, 27)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(46, 16)
        Me.Label12.TabIndex = 1366
        Me.Label12.Text = "Bank :"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tpDIT)
        Me.TabControl1.Controls.Add(Me.tpOC)
        Me.TabControl1.Controls.Add(Me.tpJV)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(8, 244)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1154, 281)
        Me.TabControl1.TabIndex = 1376
        '
        'tpDIT
        '
        Me.tpDIT.Controls.Add(Me.chkDITcheckAll)
        Me.tpDIT.Controls.Add(Me.btnUpdateDeposit)
        Me.tpDIT.Controls.Add(Me.Label18)
        Me.tpDIT.Controls.Add(Me.txtDITSubTotal)
        Me.tpDIT.Controls.Add(Me.dgvDIT)
        Me.tpDIT.Controls.Add(Me.Label11)
        Me.tpDIT.Controls.Add(Me.TxtTotalDIT)
        Me.tpDIT.Location = New System.Drawing.Point(4, 25)
        Me.tpDIT.Name = "tpDIT"
        Me.tpDIT.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDIT.Size = New System.Drawing.Size(1146, 252)
        Me.tpDIT.TabIndex = 0
        Me.tpDIT.Text = "Deposit In Transit"
        Me.tpDIT.UseVisualStyleBackColor = True
        '
        'chkDITcheckAll
        '
        Me.chkDITcheckAll.AutoSize = True
        Me.chkDITcheckAll.Location = New System.Drawing.Point(15, 14)
        Me.chkDITcheckAll.Name = "chkDITcheckAll"
        Me.chkDITcheckAll.Size = New System.Drawing.Size(83, 20)
        Me.chkDITcheckAll.TabIndex = 1385
        Me.chkDITcheckAll.Text = "Check All"
        Me.chkDITcheckAll.UseVisualStyleBackColor = True
        '
        'btnUpdateDeposit
        '
        Me.btnUpdateDeposit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateDeposit.Location = New System.Drawing.Point(6, 220)
        Me.btnUpdateDeposit.Name = "btnUpdateDeposit"
        Me.btnUpdateDeposit.Size = New System.Drawing.Size(171, 26)
        Me.btnUpdateDeposit.TabIndex = 1383
        Me.btnUpdateDeposit.Text = "Update Cleared Deposits"
        Me.btnUpdateDeposit.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label18.Location = New System.Drawing.Point(450, 365)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(69, 17)
        Me.Label18.TabIndex = 1382
        Me.Label18.Text = "Sub Total"
        '
        'txtDITSubTotal
        '
        Me.txtDITSubTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtDITSubTotal.Location = New System.Drawing.Point(406, 220)
        Me.txtDITSubTotal.Name = "txtDITSubTotal"
        Me.txtDITSubTotal.ReadOnly = True
        Me.txtDITSubTotal.Size = New System.Drawing.Size(149, 22)
        Me.txtDITSubTotal.TabIndex = 1381
        Me.txtDITSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'dgvDIT
        '
        Me.dgvDIT.AllowUserToAddRows = False
        Me.dgvDIT.AllowUserToDeleteRows = False
        Me.dgvDIT.AllowUserToOrderColumns = True
        Me.dgvDIT.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDIT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvDIT.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcDITinclude, Me.dgcDITID, Me.dgcDITdate, Me.dgcDITrefID, Me.dgcDITtype, Me.dgcDITrefNo, Me.dgcDITamount, Me.dgcDITsearch})
        Me.dgvDIT.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.dgvDIT.Location = New System.Drawing.Point(3, 40)
        Me.dgvDIT.Name = "dgvDIT"
        Me.dgvDIT.ReadOnly = True
        Me.dgvDIT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDIT.Size = New System.Drawing.Size(1140, 174)
        Me.dgvDIT.TabIndex = 1378
        '
        'dgcDITinclude
        '
        Me.dgcDITinclude.Frozen = True
        Me.dgcDITinclude.HeaderText = "Include"
        Me.dgcDITinclude.Name = "dgcDITinclude"
        Me.dgcDITinclude.ReadOnly = True
        Me.dgcDITinclude.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgcDITinclude.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.dgcDITinclude.Width = 60
        '
        'dgcDITID
        '
        Me.dgcDITID.HeaderText = "ID"
        Me.dgcDITID.Name = "dgcDITID"
        Me.dgcDITID.ReadOnly = True
        Me.dgcDITID.Visible = False
        '
        'dgcDITdate
        '
        DataGridViewCellStyle1.Format = "d"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.dgcDITdate.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgcDITdate.HeaderText = "Deposit Date"
        Me.dgcDITdate.Name = "dgcDITdate"
        Me.dgcDITdate.ReadOnly = True
        '
        'dgcDITrefID
        '
        Me.dgcDITrefID.HeaderText = "Ref. ID"
        Me.dgcDITrefID.Name = "dgcDITrefID"
        Me.dgcDITrefID.ReadOnly = True
        Me.dgcDITrefID.Visible = False
        '
        'dgcDITtype
        '
        Me.dgcDITtype.HeaderText = "Ref. Type"
        Me.dgcDITtype.Name = "dgcDITtype"
        Me.dgcDITtype.ReadOnly = True
        '
        'dgcDITrefNo
        '
        Me.dgcDITrefNo.HeaderText = "Ref. No."
        Me.dgcDITrefNo.Name = "dgcDITrefNo"
        Me.dgcDITrefNo.ReadOnly = True
        '
        'dgcDITamount
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.dgcDITamount.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgcDITamount.HeaderText = "Amount"
        Me.dgcDITamount.Name = "dgcDITamount"
        Me.dgcDITamount.ReadOnly = True
        Me.dgcDITamount.Width = 150
        '
        'dgcDITsearch
        '
        Me.dgcDITsearch.HeaderText = ">>"
        Me.dgcDITsearch.Name = "dgcDITsearch"
        Me.dgcDITsearch.ReadOnly = True
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label11.Location = New System.Drawing.Point(911, 366)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(66, 17)
        Me.Label11.TabIndex = 1371
        Me.Label11.Text = "Total DIT"
        '
        'TxtTotalDIT
        '
        Me.TxtTotalDIT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtTotalDIT.Location = New System.Drawing.Point(995, 362)
        Me.TxtTotalDIT.Name = "TxtTotalDIT"
        Me.TxtTotalDIT.Size = New System.Drawing.Size(186, 22)
        Me.TxtTotalDIT.TabIndex = 1370
        Me.TxtTotalDIT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpOC
        '
        Me.tpOC.Controls.Add(Me.txtOCsubtotal)
        Me.tpOC.Controls.Add(Me.btnUpdateChecks)
        Me.tpOC.Controls.Add(Me.chkOCcheckAll)
        Me.tpOC.Controls.Add(Me.Label13)
        Me.tpOC.Controls.Add(Me.dgvOC)
        Me.tpOC.Controls.Add(Me.TxtTotalOutstanding)
        Me.tpOC.Location = New System.Drawing.Point(4, 25)
        Me.tpOC.Name = "tpOC"
        Me.tpOC.Padding = New System.Windows.Forms.Padding(3)
        Me.tpOC.Size = New System.Drawing.Size(1146, 252)
        Me.tpOC.TabIndex = 1
        Me.tpOC.Text = "Outstanding Checks"
        Me.tpOC.UseVisualStyleBackColor = True
        '
        'txtOCsubtotal
        '
        Me.txtOCsubtotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtOCsubtotal.Location = New System.Drawing.Point(406, 220)
        Me.txtOCsubtotal.Name = "txtOCsubtotal"
        Me.txtOCsubtotal.ReadOnly = True
        Me.txtOCsubtotal.Size = New System.Drawing.Size(149, 22)
        Me.txtOCsubtotal.TabIndex = 1385
        Me.txtOCsubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnUpdateChecks
        '
        Me.btnUpdateChecks.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateChecks.Location = New System.Drawing.Point(6, 220)
        Me.btnUpdateChecks.Name = "btnUpdateChecks"
        Me.btnUpdateChecks.Size = New System.Drawing.Size(171, 26)
        Me.btnUpdateChecks.TabIndex = 1384
        Me.btnUpdateChecks.Text = "Update Cleared Checks"
        Me.btnUpdateChecks.UseVisualStyleBackColor = True
        '
        'chkOCcheckAll
        '
        Me.chkOCcheckAll.AutoSize = True
        Me.chkOCcheckAll.Location = New System.Drawing.Point(15, 14)
        Me.chkOCcheckAll.Name = "chkOCcheckAll"
        Me.chkOCcheckAll.Size = New System.Drawing.Size(83, 20)
        Me.chkOCcheckAll.TabIndex = 1374
        Me.chkOCcheckAll.Text = "Check All"
        Me.chkOCcheckAll.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label13.Location = New System.Drawing.Point(769, 477)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(121, 17)
        Me.Label13.TabIndex = 1373
        Me.Label13.Text = "Total Outstanding"
        '
        'dgvOC
        '
        Me.dgvOC.AllowUserToAddRows = False
        Me.dgvOC.AllowUserToDeleteRows = False
        Me.dgvOC.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvOC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvOC.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcOCinclude, Me.dgcOCID, Me.dgcOCdate, Me.dgcOCrefID, Me.dgcOCrefType, Me.dgcOCrefNo, Me.dgcOCamount, Me.chOC_CheckNo, Me.dgcOCvceName, Me.dgcOCsearch})
        Me.dgvOC.Location = New System.Drawing.Point(3, 40)
        Me.dgvOC.Name = "dgvOC"
        Me.dgvOC.ReadOnly = True
        Me.dgvOC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvOC.Size = New System.Drawing.Size(1140, 174)
        Me.dgvOC.TabIndex = 1370
        '
        'dgcOCinclude
        '
        Me.dgcOCinclude.HeaderText = "Include"
        Me.dgcOCinclude.Name = "dgcOCinclude"
        Me.dgcOCinclude.ReadOnly = True
        Me.dgcOCinclude.Width = 60
        '
        'dgcOCID
        '
        Me.dgcOCID.HeaderText = "ID"
        Me.dgcOCID.Name = "dgcOCID"
        Me.dgcOCID.ReadOnly = True
        Me.dgcOCID.Visible = False
        '
        'dgcOCdate
        '
        DataGridViewCellStyle3.Format = "d"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.dgcOCdate.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgcOCdate.HeaderText = "Check Date"
        Me.dgcOCdate.Name = "dgcOCdate"
        Me.dgcOCdate.ReadOnly = True
        '
        'dgcOCrefID
        '
        Me.dgcOCrefID.HeaderText = "Ref. ID"
        Me.dgcOCrefID.Name = "dgcOCrefID"
        Me.dgcOCrefID.ReadOnly = True
        Me.dgcOCrefID.Visible = False
        '
        'dgcOCrefType
        '
        Me.dgcOCrefType.HeaderText = "Ref. Type"
        Me.dgcOCrefType.Name = "dgcOCrefType"
        Me.dgcOCrefType.ReadOnly = True
        '
        'dgcOCrefNo
        '
        Me.dgcOCrefNo.HeaderText = "Ref. No."
        Me.dgcOCrefNo.Name = "dgcOCrefNo"
        Me.dgcOCrefNo.ReadOnly = True
        '
        'dgcOCamount
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.dgcOCamount.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgcOCamount.HeaderText = "Amount"
        Me.dgcOCamount.Name = "dgcOCamount"
        Me.dgcOCamount.ReadOnly = True
        Me.dgcOCamount.Width = 150
        '
        'chOC_CheckNo
        '
        Me.chOC_CheckNo.HeaderText = "CheckNo"
        Me.chOC_CheckNo.Name = "chOC_CheckNo"
        Me.chOC_CheckNo.ReadOnly = True
        '
        'dgcOCvceName
        '
        Me.dgcOCvceName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.dgcOCvceName.HeaderText = "Payee Name"
        Me.dgcOCvceName.Name = "dgcOCvceName"
        Me.dgcOCvceName.ReadOnly = True
        '
        'dgcOCsearch
        '
        Me.dgcOCsearch.HeaderText = ""
        Me.dgcOCsearch.Name = "dgcOCsearch"
        Me.dgcOCsearch.ReadOnly = True
        Me.dgcOCsearch.Text = ">>"
        Me.dgcOCsearch.Width = 50
        '
        'TxtTotalOutstanding
        '
        Me.TxtTotalOutstanding.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtTotalOutstanding.Location = New System.Drawing.Point(927, 279)
        Me.TxtTotalOutstanding.Name = "TxtTotalOutstanding"
        Me.TxtTotalOutstanding.Size = New System.Drawing.Size(194, 22)
        Me.TxtTotalOutstanding.TabIndex = 1372
        Me.TxtTotalOutstanding.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tpJV
        '
        Me.tpJV.Controls.Add(Me.Label8)
        Me.tpJV.Controls.Add(Me.txtJVNO)
        Me.tpJV.Controls.Add(Me.cbBankReconType)
        Me.tpJV.Controls.Add(Me.Label5)
        Me.tpJV.Controls.Add(Me.dgvJV)
        Me.tpJV.Controls.Add(Me.Label7)
        Me.tpJV.Controls.Add(Me.txtJVRemarks)
        Me.tpJV.Controls.Add(Me.btnAdjType)
        Me.tpJV.Location = New System.Drawing.Point(4, 25)
        Me.tpJV.Name = "tpJV"
        Me.tpJV.Padding = New System.Windows.Forms.Padding(3)
        Me.tpJV.Size = New System.Drawing.Size(1146, 252)
        Me.tpJV.TabIndex = 2
        Me.tpJV.Text = "JV"
        Me.tpJV.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label8.Location = New System.Drawing.Point(934, 10)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(62, 17)
        Me.Label8.TabIndex = 1389
        Me.Label8.Text = "JV Ref. :"
        '
        'txtJVNO
        '
        Me.txtJVNO.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtJVNO.Location = New System.Drawing.Point(1002, 7)
        Me.txtJVNO.Name = "txtJVNO"
        Me.txtJVNO.ReadOnly = True
        Me.txtJVNO.Size = New System.Drawing.Size(135, 22)
        Me.txtJVNO.TabIndex = 1385
        Me.txtJVNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 174)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 16)
        Me.Label5.TabIndex = 1380
        Me.Label5.Text = "Remarks :"
        '
        'dgvJV
        '
        Me.dgvJV.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvJV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvJV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcJVparticulars, Me.dgcJVamount, Me.dgcJVCode, Me.dgcJVtitle, Me.dgcJVvceCode, Me.dgcJVvceName})
        Me.dgvJV.Location = New System.Drawing.Point(3, 38)
        Me.dgvJV.Name = "dgvJV"
        Me.dgvJV.Size = New System.Drawing.Size(1134, 127)
        Me.dgvJV.TabIndex = 77
        '
        'dgcJVparticulars
        '
        Me.dgcJVparticulars.HeaderText = "Particulars"
        Me.dgcJVparticulars.Name = "dgcJVparticulars"
        Me.dgcJVparticulars.Width = 230
        '
        'dgcJVamount
        '
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.dgcJVamount.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgcJVamount.HeaderText = "Add (Deduct)"
        Me.dgcJVamount.Name = "dgcJVamount"
        '
        'dgcJVCode
        '
        Me.dgcJVCode.HeaderText = "Account Code"
        Me.dgcJVCode.Name = "dgcJVCode"
        '
        'dgcJVtitle
        '
        Me.dgcJVtitle.HeaderText = "Account Title"
        Me.dgcJVtitle.Name = "dgcJVtitle"
        Me.dgcJVtitle.Width = 280
        '
        'dgcJVvceCode
        '
        Me.dgcJVvceCode.HeaderText = "VCECode"
        Me.dgcJVvceCode.Name = "dgcJVvceCode"
        Me.dgcJVvceCode.Width = 80
        '
        'dgcJVvceName
        '
        Me.dgcJVvceName.HeaderText = "VCEName"
        Me.dgcJVvceName.Name = "dgcJVvceName"
        Me.dgcJVvceName.Width = 200
        '
        'txtJVRemarks
        '
        Me.txtJVRemarks.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtJVRemarks.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJVRemarks.Location = New System.Drawing.Point(80, 171)
        Me.txtJVRemarks.Multiline = True
        Me.txtJVRemarks.Name = "txtJVRemarks"
        Me.txtJVRemarks.Size = New System.Drawing.Size(327, 75)
        Me.txtJVRemarks.TabIndex = 1381
        '
        'btnAdjType
        '
        Me.btnAdjType.BackgroundImage = Global.jade.My.Resources.Resources._New
        Me.btnAdjType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAdjType.Location = New System.Drawing.Point(405, 7)
        Me.btnAdjType.Name = "btnAdjType"
        Me.btnAdjType.Size = New System.Drawing.Size(25, 25)
        Me.btnAdjType.TabIndex = 1388
        Me.btnAdjType.UseVisualStyleBackColor = True
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgvCleared)
        Me.TabPage1.Controls.Add(Me.chkClear)
        Me.TabPage1.Controls.Add(Me.btnReturnCheck)
        Me.TabPage1.Controls.Add(Me.txtClearedAmount)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1146, 252)
        Me.TabPage1.TabIndex = 3
        Me.TabPage1.Text = "Cleared Transaction"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'dgvCleared
        '
        Me.dgvCleared.AllowUserToAddRows = False
        Me.dgvCleared.AllowUserToDeleteRows = False
        Me.dgvCleared.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvCleared.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvCleared.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewCheckBoxColumn1, Me.dgcClearID, Me.dgcClearDate, Me.dgcClearReID, Me.dgcClearRefType, Me.dgcClearRefNo, Me.dgcClearAmount, Me.dgcClearCheckNo, Me.dgcClearName, Me.dgcClearType})
        Me.dgvCleared.Location = New System.Drawing.Point(3, 39)
        Me.dgvCleared.Name = "dgvCleared"
        Me.dgvCleared.ReadOnly = True
        Me.dgvCleared.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCleared.Size = New System.Drawing.Size(1140, 174)
        Me.dgvCleared.TabIndex = 1390
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.HeaderText = "Include"
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.ReadOnly = True
        Me.DataGridViewCheckBoxColumn1.Width = 60
        '
        'dgcClearID
        '
        Me.dgcClearID.HeaderText = "ID"
        Me.dgcClearID.Name = "dgcClearID"
        Me.dgcClearID.ReadOnly = True
        Me.dgcClearID.Visible = False
        '
        'dgcClearDate
        '
        DataGridViewCellStyle6.Format = "d"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.dgcClearDate.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgcClearDate.HeaderText = "Date"
        Me.dgcClearDate.Name = "dgcClearDate"
        Me.dgcClearDate.ReadOnly = True
        '
        'dgcClearReID
        '
        Me.dgcClearReID.HeaderText = "Ref. ID"
        Me.dgcClearReID.Name = "dgcClearReID"
        Me.dgcClearReID.ReadOnly = True
        Me.dgcClearReID.Visible = False
        '
        'dgcClearRefType
        '
        Me.dgcClearRefType.HeaderText = "Ref. Type"
        Me.dgcClearRefType.Name = "dgcClearRefType"
        Me.dgcClearRefType.ReadOnly = True
        '
        'dgcClearRefNo
        '
        Me.dgcClearRefNo.HeaderText = "Ref. No."
        Me.dgcClearRefNo.Name = "dgcClearRefNo"
        Me.dgcClearRefNo.ReadOnly = True
        '
        'dgcClearAmount
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N2"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.dgcClearAmount.DefaultCellStyle = DataGridViewCellStyle7
        Me.dgcClearAmount.HeaderText = "Amount"
        Me.dgcClearAmount.Name = "dgcClearAmount"
        Me.dgcClearAmount.ReadOnly = True
        Me.dgcClearAmount.Width = 150
        '
        'dgcClearCheckNo
        '
        Me.dgcClearCheckNo.HeaderText = "CheckNo"
        Me.dgcClearCheckNo.Name = "dgcClearCheckNo"
        Me.dgcClearCheckNo.ReadOnly = True
        '
        'dgcClearName
        '
        Me.dgcClearName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.dgcClearName.HeaderText = "Payee Name"
        Me.dgcClearName.Name = "dgcClearName"
        Me.dgcClearName.ReadOnly = True
        '
        'dgcClearType
        '
        Me.dgcClearType.HeaderText = "Type"
        Me.dgcClearType.Name = "dgcClearType"
        Me.dgcClearType.ReadOnly = True
        Me.dgcClearType.Visible = False
        '
        'chkClear
        '
        Me.chkClear.AutoSize = True
        Me.chkClear.Location = New System.Drawing.Point(15, 10)
        Me.chkClear.Name = "chkClear"
        Me.chkClear.Size = New System.Drawing.Size(83, 20)
        Me.chkClear.TabIndex = 1389
        Me.chkClear.Text = "Check All"
        Me.chkClear.UseVisualStyleBackColor = True
        '
        'btnReturnCheck
        '
        Me.btnReturnCheck.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReturnCheck.Location = New System.Drawing.Point(6, 216)
        Me.btnReturnCheck.Name = "btnReturnCheck"
        Me.btnReturnCheck.Size = New System.Drawing.Size(171, 26)
        Me.btnReturnCheck.TabIndex = 1388
        Me.btnReturnCheck.Text = "Unclear Transaction"
        Me.btnReturnCheck.UseVisualStyleBackColor = True
        '
        'txtClearedAmount
        '
        Me.txtClearedAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtClearedAmount.Location = New System.Drawing.Point(406, 216)
        Me.txtClearedAmount.Name = "txtClearedAmount"
        Me.txtClearedAmount.ReadOnly = True
        Me.txtClearedAmount.Size = New System.Drawing.Size(149, 22)
        Me.txtClearedAmount.TabIndex = 1387
        Me.txtClearedAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label19.Location = New System.Drawing.Point(564, 120)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(106, 17)
        Me.Label19.TabIndex = 1390
        Me.Label19.Text = ": JV Adjustment"
        '
        'txtJVadjustment
        '
        Me.txtJVadjustment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtJVadjustment.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJVadjustment.Location = New System.Drawing.Point(380, 118)
        Me.txtJVadjustment.Name = "txtJVadjustment"
        Me.txtJVadjustment.ReadOnly = True
        Me.txtJVadjustment.Size = New System.Drawing.Size(181, 22)
        Me.txtJVadjustment.TabIndex = 1383
        Me.txtJVadjustment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAccountTitle
        '
        Me.txtAccountTitle.Location = New System.Drawing.Point(109, 73)
        Me.txtAccountTitle.Name = "txtAccountTitle"
        Me.txtAccountTitle.ReadOnly = True
        Me.txtAccountTitle.Size = New System.Drawing.Size(249, 21)
        Me.txtAccountTitle.TabIndex = 1377
        Me.txtAccountTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAccountCode
        '
        Me.txtAccountCode.Location = New System.Drawing.Point(109, 49)
        Me.txtAccountCode.Name = "txtAccountCode"
        Me.txtAccountCode.ReadOnly = True
        Me.txtAccountCode.Size = New System.Drawing.Size(180, 21)
        Me.txtAccountCode.TabIndex = 1378
        Me.txtAccountCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBankAccountNo
        '
        Me.txtBankAccountNo.Location = New System.Drawing.Point(109, 97)
        Me.txtBankAccountNo.Name = "txtBankAccountNo"
        Me.txtBankAccountNo.ReadOnly = True
        Me.txtBankAccountNo.Size = New System.Drawing.Size(249, 21)
        Me.txtBankAccountNo.TabIndex = 1379
        Me.txtBankAccountNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Panel3)
        Me.GroupBox2.Controls.Add(Me.Panel2)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.txtJVadjustment)
        Me.GroupBox2.Controls.Add(Me.txtRemarks)
        Me.GroupBox2.Controls.Add(Me.txtBankID)
        Me.GroupBox2.Controls.Add(Me.Panel1)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.txtBankAccountNo)
        Me.GroupBox2.Controls.Add(Me.txtVariance)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtAccountTitle)
        Me.GroupBox2.Controls.Add(Me.txtAdjBookBal)
        Me.GroupBox2.Controls.Add(Me.txtAccountCode)
        Me.GroupBox2.Controls.Add(Me.cbBank)
        Me.GroupBox2.Controls.Add(Me.txtTransNum)
        Me.GroupBox2.Controls.Add(Me.txtStatus)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.dtpDocDate)
        Me.GroupBox2.Controls.Add(Me.txtAdjBankBal)
        Me.GroupBox2.Controls.Add(Me.Label23)
        Me.GroupBox2.Controls.Add(Me.txtDIT)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label22)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtOC)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtBankBal)
        Me.GroupBox2.Controls.Add(Me.txtBookBal)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 40)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1154, 198)
        Me.GroupBox2.TabIndex = 1379
        Me.GroupBox2.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Panel3.Location = New System.Drawing.Point(380, 146)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(502, 5)
        Me.Panel3.TabIndex = 1410
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Panel2.Location = New System.Drawing.Point(380, 61)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(365, 5)
        Me.Panel2.TabIndex = 1409
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(19, 100)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(88, 16)
        Me.Label17.TabIndex = 1408
        Me.Label17.Text = "Account No. :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(15, 76)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(92, 16)
        Me.Label16.TabIndex = 1407
        Me.Label16.Text = "Account Title :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(9, 52)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(98, 16)
        Me.Label14.TabIndex = 1406
        Me.Label14.Text = "Account Code :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label10.Location = New System.Drawing.Point(35, 124)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 17)
        Me.Label10.TabIndex = 1404
        Me.Label10.Text = "Remarks :"
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(109, 121)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(249, 63)
        Me.txtRemarks.TabIndex = 1405
        Me.txtRemarks.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBankID
        '
        Me.txtBankID.Location = New System.Drawing.Point(293, 49)
        Me.txtBankID.Name = "txtBankID"
        Me.txtBankID.ReadOnly = True
        Me.txtBankID.Size = New System.Drawing.Size(65, 21)
        Me.txtBankID.TabIndex = 1403
        Me.txtBankID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBankID.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(364, 22)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(5, 165)
        Me.Panel1.TabIndex = 1402
        '
        'txtTransNum
        '
        Me.txtTransNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTransNum.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransNum.Location = New System.Drawing.Point(1014, 16)
        Me.txtTransNum.Name = "txtTransNum"
        Me.txtTransNum.Size = New System.Drawing.Size(132, 22)
        Me.txtTransNum.TabIndex = 1393
        '
        'txtStatus
        '
        Me.txtStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtStatus.Enabled = False
        Me.txtStatus.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(1014, 64)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(132, 22)
        Me.txtStatus.TabIndex = 1395
        Me.txtStatus.Text = "Open"
        '
        'dtpDocDate
        '
        Me.dtpDocDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpDocDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDocDate.Location = New System.Drawing.Point(1014, 40)
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.Size = New System.Drawing.Size(132, 22)
        Me.dtpDocDate.TabIndex = 1394
        '
        'Label23
        '
        Me.Label23.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label23.Location = New System.Drawing.Point(957, 19)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(58, 16)
        Me.Label23.TabIndex = 1398
        Me.Label23.Text = "BR No. :"
        '
        'Label20
        '
        Me.Label20.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label20.Location = New System.Drawing.Point(961, 67)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(54, 16)
        Me.Label20.TabIndex = 1401
        Me.Label20.Text = "Status :"
        '
        'Label22
        '
        Me.Label22.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label22.Location = New System.Drawing.Point(893, 43)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(122, 16)
        Me.Label22.TabIndex = 1399
        Me.Label22.Text = "Bank Recon. Date :"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSearch, Me.tsbNew, Me.tsbEdit, Me.tsbSave, Me.tsbCancel, Me.ToolStripSeparator4, Me.tsbPrint, Me.tsbReports, Me.ToolStripSeparator2, Me.tsbPrevious, Me.tsbNext, Me.ToolStripSeparator3, Me.tsbRefresh, Me.ToolStripSeparator1, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1167, 40)
        Me.ToolStrip1.TabIndex = 1385
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
        'tsbRefresh
        '
        Me.tsbRefresh.AutoSize = False
        Me.tsbRefresh.ForeColor = System.Drawing.Color.White
        Me.tsbRefresh.Image = Global.jade.My.Resources.Resources.refresh
        Me.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbRefresh.Name = "tsbRefresh"
        Me.tsbRefresh.Size = New System.Drawing.Size(50, 35)
        Me.tsbRefresh.Text = "Refresh"
        Me.tsbRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 40)
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
        'FrmBR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1167, 540)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "FrmBR"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bank Recon "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabControl1.ResumeLayout(False)
        Me.tpDIT.ResumeLayout(False)
        Me.tpDIT.PerformLayout()
        CType(Me.dgvDIT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpOC.ResumeLayout(False)
        Me.tpOC.PerformLayout()
        CType(Me.dgvOC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpJV.ResumeLayout(False)
        Me.tpJV.PerformLayout()
        CType(Me.dgvJV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.dgvCleared, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBookBal As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBankBal As System.Windows.Forms.TextBox
    Friend WithEvents txtOC As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cbBank As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtDIT As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tpDIT As System.Windows.Forms.TabPage
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TxtTotalDIT As System.Windows.Forms.TextBox
    Friend WithEvents tpOC As System.Windows.Forms.TabPage
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TxtTotalOutstanding As System.Windows.Forms.TextBox
    Friend WithEvents dgvOC As System.Windows.Forms.DataGridView
    Friend WithEvents txtAccountTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtBankAccountNo As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents tpJV As System.Windows.Forms.TabPage
    Friend WithEvents dgvJV As System.Windows.Forms.DataGridView
    Friend WithEvents cbBankReconType As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtJVRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtJVadjustment As System.Windows.Forms.TextBox
    Friend WithEvents chkOCcheckAll As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtAdjBankBal As System.Windows.Forms.TextBox
    Friend WithEvents txtJVNO As System.Windows.Forms.TextBox
    Friend WithEvents dgvDIT As System.Windows.Forms.DataGridView
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtDITSubTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtVariance As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAdjBookBal As System.Windows.Forms.TextBox
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
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
    Friend WithEvents txtTransNum As System.Windows.Forms.TextBox
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents dtpDocDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtBankID As System.Windows.Forms.TextBox
    Friend WithEvents btnUpdateDeposit As System.Windows.Forms.Button
    Friend WithEvents btnUpdateChecks As System.Windows.Forms.Button
    Friend WithEvents chkDITcheckAll As System.Windows.Forms.CheckBox
    Friend WithEvents txtOCsubtotal As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnAdjType As System.Windows.Forms.Button
    Friend WithEvents dgcDITinclude As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents dgcDITID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcDITdate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcDITrefID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcDITtype As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcDITrefNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcDITamount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcDITsearch As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents dgcOCinclude As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents dgcOCID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcOCdate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcOCrefID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcOCrefType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcOCrefNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcOCamount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chOC_CheckNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcOCvceName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcOCsearch As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents dgcJVparticulars As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcJVamount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcJVCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcJVtitle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcJVvceCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcJVvceName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tsbPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents chkClear As System.Windows.Forms.CheckBox
    Friend WithEvents btnReturnCheck As System.Windows.Forms.Button
    Friend WithEvents txtClearedAmount As System.Windows.Forms.TextBox
    Friend WithEvents dgvCleared As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents dgcClearID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcClearDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcClearReID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcClearRefType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcClearRefNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcClearAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcClearCheckNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcClearName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcClearType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tsbRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
End Class
