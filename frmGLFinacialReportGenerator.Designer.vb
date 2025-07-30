<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGLFinacialReportGenerator
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGLFinacialReportGenerator))
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.nupYear = New System.Windows.Forms.NumericUpDown()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbMonth = New System.Windows.Forms.ComboBox()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cbFiscal = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkPEC = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbDetailed = New System.Windows.Forms.RadioButton()
        Me.rbSummary = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.rbRefID = New System.Windows.Forms.RadioButton()
        Me.rbAccountCode = New System.Windows.Forms.RadioButton()
        Me.rbOrderDate = New System.Windows.Forms.RadioButton()
        Me.rbOrderTransactionNumber = New System.Windows.Forms.RadioButton()
        Me.rbOrderSubType = New System.Windows.Forms.RadioButton()
        Me.rbAccountTitle = New System.Windows.Forms.RadioButton()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.cmbToDepartment = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbFromDepartment = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.cbAccountTo = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbAccountFrom = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.dgvData = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtTotalCredit = New System.Windows.Forms.TextBox()
        Me.txtTotalDebit = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnGeneratePEC = New System.Windows.Forms.Button()
        Me.btnPostPEC = New System.Windows.Forms.Button()
        Me.btnGenerateBB = New System.Windows.Forms.Button()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.dtpPECdate = New System.Windows.Forms.DateTimePicker()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dtpLastPECdate = New System.Windows.Forms.DateTimePicker()
        Me.btnPostBB = New System.Windows.Forms.Button()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.chkInventory = New System.Windows.Forms.CheckBox()
        Me.chkPayable = New System.Windows.Forms.CheckBox()
        Me.chkORNo = New System.Windows.Forms.CheckBox()
        Me.chkAR = New System.Windows.Forms.CheckBox()
        Me.chkCSI = New System.Windows.Forms.CheckBox()
        Me.chkCR = New System.Windows.Forms.CheckBox()
        Me.chSJ = New System.Windows.Forms.CheckBox()
        Me.chkPJ = New System.Windows.Forms.CheckBox()
        Me.chkBB = New System.Windows.Forms.CheckBox()
        Me.chkJV = New System.Windows.Forms.CheckBox()
        Me.chkDV = New System.Windows.Forms.CheckBox()
        Me.chkOR = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtTotalVariance = New System.Windows.Forms.TextBox()
        Me.rbBalanceSheet = New System.Windows.Forms.RadioButton()
        Me.rbTrialBalance = New System.Windows.Forms.RadioButton()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.rbIncomeStatement = New System.Windows.Forms.RadioButton()
        Me.rbARR = New System.Windows.Forms.RadioButton()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.cbStatus = New System.Windows.Forms.ComboBox()
        Me.bgwExport = New System.ComponentModel.BackgroundWorker()
        Me.pgbCounter = New System.Windows.Forms.ProgressBar()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nupYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(56, 69)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 15)
        Me.Label5.TabIndex = 575
        Me.Label5.Text = "From Period :"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.nupYear)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cbMonth)
        Me.GroupBox1.Controls.Add(Me.dtpTo)
        Me.GroupBox1.Controls.Add(Me.dtpFrom)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.cbFiscal)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 8)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(351, 120)
        Me.GroupBox1.TabIndex = 576
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Financial Period"
        '
        'nupYear
        '
        Me.nupYear.Location = New System.Drawing.Point(57, 18)
        Me.nupYear.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nupYear.Name = "nupYear"
        Me.nupYear.Size = New System.Drawing.Size(68, 21)
        Me.nupYear.TabIndex = 598
        Me.nupYear.Value = New Decimal(New Integer() {2016, 0, 0, 0})
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(8, 42)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(48, 15)
        Me.Label10.TabIndex = 597
        Me.Label10.Text = "Month :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(17, 21)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 15)
        Me.Label2.TabIndex = 596
        Me.Label2.Text = "Year :"
        '
        'cbMonth
        '
        Me.cbMonth.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cbMonth.FormattingEnabled = True
        Me.cbMonth.Items.AddRange(New Object() {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"})
        Me.cbMonth.Location = New System.Drawing.Point(58, 41)
        Me.cbMonth.Margin = New System.Windows.Forms.Padding(4)
        Me.cbMonth.Name = "cbMonth"
        Me.cbMonth.Size = New System.Drawing.Size(106, 21)
        Me.cbMonth.TabIndex = 595
        '
        'dtpTo
        '
        Me.dtpTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(145, 92)
        Me.dtpTo.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(124, 21)
        Me.dtpTo.TabIndex = 593
        '
        'dtpFrom
        '
        Me.dtpFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(145, 68)
        Me.dtpFrom.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(124, 21)
        Me.dtpFrom.TabIndex = 592
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(179, 20)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 15)
        Me.Label9.TabIndex = 580
        Me.Label9.Text = "Fiscal Option :"
        '
        'cbFiscal
        '
        Me.cbFiscal.DisplayMember = "MTD"
        Me.cbFiscal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cbFiscal.FormattingEnabled = True
        Me.cbFiscal.Items.AddRange(New Object() {"MTD", "YTD"})
        Me.cbFiscal.Location = New System.Drawing.Point(263, 18)
        Me.cbFiscal.Margin = New System.Windows.Forms.Padding(4)
        Me.cbFiscal.Name = "cbFiscal"
        Me.cbFiscal.Size = New System.Drawing.Size(80, 21)
        Me.cbFiscal.TabIndex = 578
        Me.cbFiscal.Text = "MTD"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(71, 90)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 15)
        Me.Label1.TabIndex = 576
        Me.Label1.Text = "To Period :"
        '
        'chkPEC
        '
        Me.chkPEC.AutoSize = True
        Me.chkPEC.Location = New System.Drawing.Point(252, 21)
        Me.chkPEC.Margin = New System.Windows.Forms.Padding(4)
        Me.chkPEC.Name = "chkPEC"
        Me.chkPEC.Size = New System.Drawing.Size(50, 19)
        Me.chkPEC.TabIndex = 592
        Me.chkPEC.Text = "PEC"
        Me.chkPEC.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbDetailed)
        Me.GroupBox2.Controls.Add(Me.rbSummary)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 254)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(351, 47)
        Me.GroupBox2.TabIndex = 577
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Report Type"
        '
        'rbDetailed
        '
        Me.rbDetailed.AutoSize = True
        Me.rbDetailed.Location = New System.Drawing.Point(217, 16)
        Me.rbDetailed.Margin = New System.Windows.Forms.Padding(4)
        Me.rbDetailed.Name = "rbDetailed"
        Me.rbDetailed.Size = New System.Drawing.Size(71, 19)
        Me.rbDetailed.TabIndex = 1
        Me.rbDetailed.Text = "Detailed"
        Me.rbDetailed.UseVisualStyleBackColor = True
        '
        'rbSummary
        '
        Me.rbSummary.AutoSize = True
        Me.rbSummary.Checked = True
        Me.rbSummary.Location = New System.Drawing.Point(62, 16)
        Me.rbSummary.Margin = New System.Windows.Forms.Padding(4)
        Me.rbSummary.Name = "rbSummary"
        Me.rbSummary.Size = New System.Drawing.Size(78, 19)
        Me.rbSummary.TabIndex = 0
        Me.rbSummary.TabStop = True
        Me.rbSummary.Text = "Summary"
        Me.rbSummary.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rbRefID)
        Me.GroupBox3.Controls.Add(Me.rbAccountCode)
        Me.GroupBox3.Controls.Add(Me.rbOrderDate)
        Me.GroupBox3.Controls.Add(Me.rbOrderTransactionNumber)
        Me.GroupBox3.Controls.Add(Me.rbOrderSubType)
        Me.GroupBox3.Controls.Add(Me.rbAccountTitle)
        Me.GroupBox3.Controls.Add(Me.GroupBox5)
        Me.GroupBox3.Location = New System.Drawing.Point(13, 302)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(351, 65)
        Me.GroupBox3.TabIndex = 577
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Sort by"
        '
        'rbRefID
        '
        Me.rbRefID.AutoSize = True
        Me.rbRefID.Location = New System.Drawing.Point(246, 22)
        Me.rbRefID.Margin = New System.Windows.Forms.Padding(4)
        Me.rbRefID.Name = "rbRefID"
        Me.rbRefID.Size = New System.Drawing.Size(97, 19)
        Me.rbRefID.TabIndex = 582
        Me.rbRefID.TabStop = True
        Me.rbRefID.Text = "Reference ID"
        Me.rbRefID.UseVisualStyleBackColor = True
        Me.rbRefID.Visible = False
        '
        'rbAccountCode
        '
        Me.rbAccountCode.AutoSize = True
        Me.rbAccountCode.Checked = True
        Me.rbAccountCode.Location = New System.Drawing.Point(8, 19)
        Me.rbAccountCode.Margin = New System.Windows.Forms.Padding(4)
        Me.rbAccountCode.Name = "rbAccountCode"
        Me.rbAccountCode.Size = New System.Drawing.Size(100, 19)
        Me.rbAccountCode.TabIndex = 581
        Me.rbAccountCode.TabStop = True
        Me.rbAccountCode.Text = "Account Code"
        Me.rbAccountCode.UseVisualStyleBackColor = True
        '
        'rbOrderDate
        '
        Me.rbOrderDate.AutoSize = True
        Me.rbOrderDate.Location = New System.Drawing.Point(246, 39)
        Me.rbOrderDate.Margin = New System.Windows.Forms.Padding(4)
        Me.rbOrderDate.Name = "rbOrderDate"
        Me.rbOrderDate.Size = New System.Drawing.Size(51, 19)
        Me.rbOrderDate.TabIndex = 3
        Me.rbOrderDate.TabStop = True
        Me.rbOrderDate.Text = "Date"
        Me.rbOrderDate.UseVisualStyleBackColor = True
        '
        'rbOrderTransactionNumber
        '
        Me.rbOrderTransactionNumber.AutoSize = True
        Me.rbOrderTransactionNumber.Location = New System.Drawing.Point(130, 39)
        Me.rbOrderTransactionNumber.Margin = New System.Windows.Forms.Padding(4)
        Me.rbOrderTransactionNumber.Name = "rbOrderTransactionNumber"
        Me.rbOrderTransactionNumber.Size = New System.Drawing.Size(104, 19)
        Me.rbOrderTransactionNumber.TabIndex = 5
        Me.rbOrderTransactionNumber.TabStop = True
        Me.rbOrderTransactionNumber.Text = "Trans Number"
        Me.rbOrderTransactionNumber.UseVisualStyleBackColor = True
        '
        'rbOrderSubType
        '
        Me.rbOrderSubType.AutoSize = True
        Me.rbOrderSubType.Location = New System.Drawing.Point(10, 39)
        Me.rbOrderSubType.Margin = New System.Windows.Forms.Padding(4)
        Me.rbOrderSubType.Name = "rbOrderSubType"
        Me.rbOrderSubType.Size = New System.Drawing.Size(122, 19)
        Me.rbOrderSubType.TabIndex = 4
        Me.rbOrderSubType.TabStop = True
        Me.rbOrderSubType.Text = "Sub Account Type"
        Me.rbOrderSubType.UseVisualStyleBackColor = True
        '
        'rbAccountTitle
        '
        Me.rbAccountTitle.AutoSize = True
        Me.rbAccountTitle.Location = New System.Drawing.Point(130, 19)
        Me.rbAccountTitle.Margin = New System.Windows.Forms.Padding(4)
        Me.rbAccountTitle.Name = "rbAccountTitle"
        Me.rbAccountTitle.Size = New System.Drawing.Size(94, 19)
        Me.rbAccountTitle.TabIndex = 1
        Me.rbAccountTitle.TabStop = True
        Me.rbAccountTitle.Text = "Account Title"
        Me.rbAccountTitle.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.cmbToDepartment)
        Me.GroupBox5.Controls.Add(Me.Label6)
        Me.GroupBox5.Controls.Add(Me.cmbFromDepartment)
        Me.GroupBox5.Controls.Add(Me.Label7)
        Me.GroupBox5.Location = New System.Drawing.Point(0, 86)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox5.Size = New System.Drawing.Size(351, 82)
        Me.GroupBox5.TabIndex = 580
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Department"
        '
        'cmbToDepartment
        '
        Me.cmbToDepartment.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbToDepartment.FormattingEnabled = True
        Me.cmbToDepartment.Items.AddRange(New Object() {"ADM", "FIN"})
        Me.cmbToDepartment.Location = New System.Drawing.Point(169, 49)
        Me.cmbToDepartment.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbToDepartment.Name = "cmbToDepartment"
        Me.cmbToDepartment.Size = New System.Drawing.Size(158, 24)
        Me.cmbToDepartment.TabIndex = 582
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label6.Location = New System.Drawing.Point(8, 49)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 13)
        Me.Label6.TabIndex = 583
        Me.Label6.Text = "To Department"
        '
        'cmbFromDepartment
        '
        Me.cmbFromDepartment.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFromDepartment.FormattingEnabled = True
        Me.cmbFromDepartment.Items.AddRange(New Object() {"ADM", "FIN"})
        Me.cmbFromDepartment.Location = New System.Drawing.Point(171, 14)
        Me.cmbFromDepartment.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbFromDepartment.Name = "cmbFromDepartment"
        Me.cmbFromDepartment.Size = New System.Drawing.Size(158, 24)
        Me.cmbFromDepartment.TabIndex = 580
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label7.Location = New System.Drawing.Point(7, 19)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 13)
        Me.Label7.TabIndex = 581
        Me.Label7.Text = "From Deparment"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.cbAccountTo)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.cbAccountFrom)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Location = New System.Drawing.Point(13, 375)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Size = New System.Drawing.Size(351, 80)
        Me.GroupBox4.TabIndex = 578
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Account Group"
        '
        'cbAccountTo
        '
        Me.cbAccountTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbAccountTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbAccountTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAccountTo.FormattingEnabled = True
        Me.cbAccountTo.Items.AddRange(New Object() {" "})
        Me.cbAccountTo.Location = New System.Drawing.Point(129, 42)
        Me.cbAccountTo.Margin = New System.Windows.Forms.Padding(4)
        Me.cbAccountTo.Name = "cbAccountTo"
        Me.cbAccountTo.Size = New System.Drawing.Size(215, 24)
        Me.cbAccountTo.TabIndex = 582
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 44)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(105, 15)
        Me.Label3.TabIndex = 583
        Me.Label3.Text = "To Account Code :"
        '
        'cbAccountFrom
        '
        Me.cbAccountFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbAccountFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbAccountFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAccountFrom.FormattingEnabled = True
        Me.cbAccountFrom.Items.AddRange(New Object() {" "})
        Me.cbAccountFrom.Location = New System.Drawing.Point(129, 16)
        Me.cbAccountFrom.Margin = New System.Windows.Forms.Padding(4)
        Me.cbAccountFrom.Name = "cbAccountFrom"
        Me.cbAccountFrom.Size = New System.Drawing.Size(215, 24)
        Me.cbAccountFrom.TabIndex = 580
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 19)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(120, 15)
        Me.Label4.TabIndex = 581
        Me.Label4.Text = "From Account Code :"
        '
        'btnGenerate
        '
        Me.btnGenerate.Location = New System.Drawing.Point(384, 8)
        Me.btnGenerate.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(102, 30)
        Me.btnGenerate.TabIndex = 582
        Me.btnGenerate.Text = "Generate"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'dgvData
        '
        Me.dgvData.AllowUserToAddRows = False
        Me.dgvData.AllowUserToDeleteRows = False
        Me.dgvData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvData.Location = New System.Drawing.Point(373, 46)
        Me.dgvData.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvData.Name = "dgvData"
        Me.dgvData.ReadOnly = True
        Me.dgvData.RowHeadersVisible = False
        Me.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvData.Size = New System.Drawing.Size(795, 644)
        Me.dgvData.TabIndex = 583
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(1389, 8)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(129, 30)
        Me.Button1.TabIndex = 584
        Me.Button1.Text = "Export"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtTotalCredit
        '
        Me.txtTotalCredit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTotalCredit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCredit.Location = New System.Drawing.Point(726, 706)
        Me.txtTotalCredit.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTotalCredit.Name = "txtTotalCredit"
        Me.txtTotalCredit.Size = New System.Drawing.Size(161, 22)
        Me.txtTotalCredit.TabIndex = 585
        '
        'txtTotalDebit
        '
        Me.txtTotalDebit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTotalDebit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalDebit.Location = New System.Drawing.Point(471, 706)
        Me.txtTotalDebit.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTotalDebit.Name = "txtTotalDebit"
        Me.txtTotalDebit.Size = New System.Drawing.Size(162, 22)
        Me.txtTotalDebit.TabIndex = 586
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(395, 845)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 16)
        Me.Label8.TabIndex = 587
        Me.Label8.Text = "Total :"
        '
        'btnGeneratePEC
        '
        Me.btnGeneratePEC.Location = New System.Drawing.Point(25, 74)
        Me.btnGeneratePEC.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGeneratePEC.Name = "btnGeneratePEC"
        Me.btnGeneratePEC.Size = New System.Drawing.Size(139, 30)
        Me.btnGeneratePEC.TabIndex = 588
        Me.btnGeneratePEC.Text = "Generate PEC Entry"
        Me.btnGeneratePEC.UseVisualStyleBackColor = True
        '
        'btnPostPEC
        '
        Me.btnPostPEC.Location = New System.Drawing.Point(172, 74)
        Me.btnPostPEC.Margin = New System.Windows.Forms.Padding(4)
        Me.btnPostPEC.Name = "btnPostPEC"
        Me.btnPostPEC.Size = New System.Drawing.Size(139, 30)
        Me.btnPostPEC.TabIndex = 589
        Me.btnPostPEC.Text = "Post PEC"
        Me.btnPostPEC.UseVisualStyleBackColor = True
        '
        'btnGenerateBB
        '
        Me.btnGenerateBB.Enabled = False
        Me.btnGenerateBB.Location = New System.Drawing.Point(25, 112)
        Me.btnGenerateBB.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGenerateBB.Name = "btnGenerateBB"
        Me.btnGenerateBB.Size = New System.Drawing.Size(139, 30)
        Me.btnGenerateBB.TabIndex = 590
        Me.btnGenerateBB.Text = "Generate BB"
        Me.btnGenerateBB.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.Label15)
        Me.GroupBox7.Controls.Add(Me.dtpPECdate)
        Me.GroupBox7.Controls.Add(Me.Label14)
        Me.GroupBox7.Controls.Add(Me.dtpLastPECdate)
        Me.GroupBox7.Controls.Add(Me.btnPostBB)
        Me.GroupBox7.Controls.Add(Me.btnGeneratePEC)
        Me.GroupBox7.Controls.Add(Me.btnGenerateBB)
        Me.GroupBox7.Controls.Add(Me.btnPostPEC)
        Me.GroupBox7.Location = New System.Drawing.Point(14, 514)
        Me.GroupBox7.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox7.Size = New System.Drawing.Size(351, 159)
        Me.GroupBox7.TabIndex = 591
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Period End Closing"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(22, 51)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(92, 15)
        Me.Label15.TabIndex = 597
        Me.Label15.Text = "New PEC date :"
        '
        'dtpPECdate
        '
        Me.dtpPECdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpPECdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpPECdate.Location = New System.Drawing.Point(149, 45)
        Me.dtpPECdate.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpPECdate.Name = "dtpPECdate"
        Me.dtpPECdate.Size = New System.Drawing.Size(194, 21)
        Me.dtpPECdate.TabIndex = 596
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(22, 22)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(119, 15)
        Me.Label14.TabIndex = 595
        Me.Label14.Text = "Period Closed as of :"
        '
        'dtpLastPECdate
        '
        Me.dtpLastPECdate.Enabled = False
        Me.dtpLastPECdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpLastPECdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpLastPECdate.Location = New System.Drawing.Point(149, 20)
        Me.dtpLastPECdate.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpLastPECdate.Name = "dtpLastPECdate"
        Me.dtpLastPECdate.Size = New System.Drawing.Size(194, 21)
        Me.dtpLastPECdate.TabIndex = 594
        '
        'btnPostBB
        '
        Me.btnPostBB.Enabled = False
        Me.btnPostBB.Location = New System.Drawing.Point(172, 112)
        Me.btnPostBB.Margin = New System.Windows.Forms.Padding(4)
        Me.btnPostBB.Name = "btnPostBB"
        Me.btnPostBB.Size = New System.Drawing.Size(139, 30)
        Me.btnPostBB.TabIndex = 591
        Me.btnPostBB.Text = "Post BB"
        Me.btnPostBB.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.chkInventory)
        Me.GroupBox8.Controls.Add(Me.chkPayable)
        Me.GroupBox8.Controls.Add(Me.chkORNo)
        Me.GroupBox8.Controls.Add(Me.chkAR)
        Me.GroupBox8.Controls.Add(Me.chkCSI)
        Me.GroupBox8.Controls.Add(Me.chkCR)
        Me.GroupBox8.Controls.Add(Me.chSJ)
        Me.GroupBox8.Controls.Add(Me.chkPJ)
        Me.GroupBox8.Controls.Add(Me.chkBB)
        Me.GroupBox8.Controls.Add(Me.chkJV)
        Me.GroupBox8.Controls.Add(Me.chkDV)
        Me.GroupBox8.Controls.Add(Me.chkOR)
        Me.GroupBox8.Controls.Add(Me.chkPEC)
        Me.GroupBox8.Location = New System.Drawing.Point(14, 128)
        Me.GroupBox8.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox8.Size = New System.Drawing.Size(351, 118)
        Me.GroupBox8.TabIndex = 578
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Book"
        '
        'chkInventory
        '
        Me.chkInventory.AutoSize = True
        Me.chkInventory.Checked = True
        Me.chkInventory.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkInventory.Location = New System.Drawing.Point(251, 61)
        Me.chkInventory.Margin = New System.Windows.Forms.Padding(4)
        Me.chkInventory.Name = "chkInventory"
        Me.chkInventory.Size = New System.Drawing.Size(74, 19)
        Me.chkInventory.TabIndex = 604
        Me.chkInventory.Text = "Inventory"
        Me.chkInventory.UseVisualStyleBackColor = True
        '
        'chkPayable
        '
        Me.chkPayable.AutoSize = True
        Me.chkPayable.Checked = True
        Me.chkPayable.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPayable.Location = New System.Drawing.Point(128, 61)
        Me.chkPayable.Margin = New System.Windows.Forms.Padding(4)
        Me.chkPayable.Name = "chkPayable"
        Me.chkPayable.Size = New System.Drawing.Size(116, 19)
        Me.chkPayable.TabIndex = 603
        Me.chkPayable.Text = "Account Payable"
        Me.chkPayable.UseVisualStyleBackColor = True
        '
        'chkORNo
        '
        Me.chkORNo.AutoSize = True
        Me.chkORNo.Checked = True
        Me.chkORNo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkORNo.Location = New System.Drawing.Point(129, 84)
        Me.chkORNo.Margin = New System.Windows.Forms.Padding(4)
        Me.chkORNo.Name = "chkORNo"
        Me.chkORNo.Size = New System.Drawing.Size(44, 19)
        Me.chkORNo.TabIndex = 602
        Me.chkORNo.Text = "OR"
        Me.chkORNo.UseVisualStyleBackColor = True
        Me.chkORNo.Visible = False
        '
        'chkAR
        '
        Me.chkAR.AutoSize = True
        Me.chkAR.Checked = True
        Me.chkAR.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAR.Location = New System.Drawing.Point(11, 84)
        Me.chkAR.Margin = New System.Windows.Forms.Padding(4)
        Me.chkAR.Name = "chkAR"
        Me.chkAR.Size = New System.Drawing.Size(42, 19)
        Me.chkAR.TabIndex = 601
        Me.chkAR.Text = "AR"
        Me.chkAR.UseVisualStyleBackColor = True
        Me.chkAR.Visible = False
        '
        'chkCSI
        '
        Me.chkCSI.AutoSize = True
        Me.chkCSI.Checked = True
        Me.chkCSI.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCSI.Location = New System.Drawing.Point(178, 84)
        Me.chkCSI.Margin = New System.Windows.Forms.Padding(4)
        Me.chkCSI.Name = "chkCSI"
        Me.chkCSI.Size = New System.Drawing.Size(45, 19)
        Me.chkCSI.TabIndex = 600
        Me.chkCSI.Text = "CSI"
        Me.chkCSI.UseVisualStyleBackColor = True
        Me.chkCSI.Visible = False
        '
        'chkCR
        '
        Me.chkCR.AutoSize = True
        Me.chkCR.Checked = True
        Me.chkCR.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCR.Location = New System.Drawing.Point(251, 84)
        Me.chkCR.Margin = New System.Windows.Forms.Padding(4)
        Me.chkCR.Name = "chkCR"
        Me.chkCR.Size = New System.Drawing.Size(43, 19)
        Me.chkCR.TabIndex = 599
        Me.chkCR.Text = "CR"
        Me.chkCR.UseVisualStyleBackColor = True
        Me.chkCR.Visible = False
        '
        'chSJ
        '
        Me.chSJ.AutoSize = True
        Me.chSJ.Checked = True
        Me.chSJ.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chSJ.Location = New System.Drawing.Point(12, 62)
        Me.chSJ.Margin = New System.Windows.Forms.Padding(4)
        Me.chSJ.Name = "chSJ"
        Me.chSJ.Size = New System.Drawing.Size(88, 19)
        Me.chSJ.TabIndex = 598
        Me.chSJ.Text = "Sales Book"
        Me.chSJ.UseVisualStyleBackColor = True
        '
        'chkPJ
        '
        Me.chkPJ.AutoSize = True
        Me.chkPJ.Checked = True
        Me.chkPJ.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPJ.Location = New System.Drawing.Point(129, 41)
        Me.chkPJ.Margin = New System.Windows.Forms.Padding(4)
        Me.chkPJ.Name = "chkPJ"
        Me.chkPJ.Size = New System.Drawing.Size(122, 19)
        Me.chkPJ.TabIndex = 597
        Me.chkPJ.Text = "Purchase Journal"
        Me.chkPJ.UseVisualStyleBackColor = True
        '
        'chkBB
        '
        Me.chkBB.AutoSize = True
        Me.chkBB.Checked = True
        Me.chkBB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBB.Location = New System.Drawing.Point(252, 41)
        Me.chkBB.Margin = New System.Windows.Forms.Padding(4)
        Me.chkBB.Name = "chkBB"
        Me.chkBB.Size = New System.Drawing.Size(42, 19)
        Me.chkBB.TabIndex = 596
        Me.chkBB.Text = "BB"
        Me.chkBB.UseVisualStyleBackColor = True
        '
        'chkJV
        '
        Me.chkJV.AutoSize = True
        Me.chkJV.Checked = True
        Me.chkJV.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkJV.Location = New System.Drawing.Point(129, 21)
        Me.chkJV.Margin = New System.Windows.Forms.Padding(4)
        Me.chkJV.Name = "chkJV"
        Me.chkJV.Size = New System.Drawing.Size(115, 19)
        Me.chkJV.TabIndex = 595
        Me.chkJV.Text = "Journal Voucher"
        Me.chkJV.UseVisualStyleBackColor = True
        '
        'chkDV
        '
        Me.chkDV.AutoSize = True
        Me.chkDV.Checked = True
        Me.chkDV.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDV.Location = New System.Drawing.Point(12, 41)
        Me.chkDV.Margin = New System.Windows.Forms.Padding(4)
        Me.chkDV.Name = "chkDV"
        Me.chkDV.Size = New System.Drawing.Size(103, 19)
        Me.chkDV.TabIndex = 594
        Me.chkDV.Text = "Disbursement"
        Me.chkDV.UseVisualStyleBackColor = True
        '
        'chkOR
        '
        Me.chkOR.AutoSize = True
        Me.chkOR.Checked = True
        Me.chkOR.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOR.Location = New System.Drawing.Point(13, 21)
        Me.chkOR.Margin = New System.Windows.Forms.Padding(4)
        Me.chkOR.Name = "chkOR"
        Me.chkOR.Size = New System.Drawing.Size(99, 19)
        Me.chkOR.TabIndex = 593
        Me.chkOR.Text = "Cash Receipt"
        Me.chkOR.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(392, 709)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(72, 15)
        Me.Label11.TabIndex = 592
        Me.Label11.Text = "Total Debit :"
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(640, 709)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(75, 15)
        Me.Label12.TabIndex = 593
        Me.Label12.Text = "Total Credit :"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Location = New System.Drawing.Point(1077, 8)
        Me.btnExport.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(102, 30)
        Me.btnExport.TabIndex = 594
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(906, 709)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(58, 15)
        Me.Label13.TabIndex = 596
        Me.Label13.Text = "Variance:"
        '
        'txtTotalVariance
        '
        Me.txtTotalVariance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTotalVariance.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalVariance.Location = New System.Drawing.Point(971, 706)
        Me.txtTotalVariance.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTotalVariance.Name = "txtTotalVariance"
        Me.txtTotalVariance.Size = New System.Drawing.Size(139, 22)
        Me.txtTotalVariance.TabIndex = 595
        '
        'rbBalanceSheet
        '
        Me.rbBalanceSheet.AutoSize = True
        Me.rbBalanceSheet.Location = New System.Drawing.Point(6, 22)
        Me.rbBalanceSheet.Margin = New System.Windows.Forms.Padding(4)
        Me.rbBalanceSheet.Name = "rbBalanceSheet"
        Me.rbBalanceSheet.Size = New System.Drawing.Size(105, 19)
        Me.rbBalanceSheet.TabIndex = 0
        Me.rbBalanceSheet.TabStop = True
        Me.rbBalanceSheet.Text = "Balance Sheet"
        Me.rbBalanceSheet.UseVisualStyleBackColor = True
        '
        'rbTrialBalance
        '
        Me.rbTrialBalance.AutoSize = True
        Me.rbTrialBalance.Location = New System.Drawing.Point(6, 63)
        Me.rbTrialBalance.Margin = New System.Windows.Forms.Padding(4)
        Me.rbTrialBalance.Name = "rbTrialBalance"
        Me.rbTrialBalance.Size = New System.Drawing.Size(97, 19)
        Me.rbTrialBalance.TabIndex = 6
        Me.rbTrialBalance.TabStop = True
        Me.rbTrialBalance.Text = "Trial Balance"
        Me.rbTrialBalance.UseVisualStyleBackColor = True
        '
        'CmdPreview
        '
        Me.CmdPreview.Location = New System.Drawing.Point(267, 11)
        Me.CmdPreview.Margin = New System.Windows.Forms.Padding(4)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.Size = New System.Drawing.Size(77, 30)
        Me.CmdPreview.TabIndex = 583
        Me.CmdPreview.Text = "Preview"
        Me.CmdPreview.UseVisualStyleBackColor = True
        '
        'rbIncomeStatement
        '
        Me.rbIncomeStatement.AutoSize = True
        Me.rbIncomeStatement.Location = New System.Drawing.Point(6, 42)
        Me.rbIncomeStatement.Margin = New System.Windows.Forms.Padding(4)
        Me.rbIncomeStatement.Name = "rbIncomeStatement"
        Me.rbIncomeStatement.Size = New System.Drawing.Size(125, 19)
        Me.rbIncomeStatement.TabIndex = 584
        Me.rbIncomeStatement.TabStop = True
        Me.rbIncomeStatement.Text = "Income Statement"
        Me.rbIncomeStatement.UseVisualStyleBackColor = True
        '
        'rbARR
        '
        Me.rbARR.AutoSize = True
        Me.rbARR.Location = New System.Drawing.Point(6, 85)
        Me.rbARR.Margin = New System.Windows.Forms.Padding(4)
        Me.rbARR.Name = "rbARR"
        Me.rbARR.Size = New System.Drawing.Size(185, 19)
        Me.rbARR.TabIndex = 585
        Me.rbARR.TabStop = True
        Me.rbARR.Text = "Accounts Receivable Analysis"
        Me.rbARR.UseVisualStyleBackColor = True
        Me.rbARR.Visible = False
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.Controls.Add(Me.rbARR)
        Me.GroupBox6.Controls.Add(Me.rbIncomeStatement)
        Me.GroupBox6.Controls.Add(Me.CmdPreview)
        Me.GroupBox6.Controls.Add(Me.rbTrialBalance)
        Me.GroupBox6.Controls.Add(Me.rbBalanceSheet)
        Me.GroupBox6.Location = New System.Drawing.Point(13, 718)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox6.Size = New System.Drawing.Size(351, 10)
        Me.GroupBox6.TabIndex = 578
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Financial Report"
        Me.GroupBox6.Visible = False
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.cbStatus)
        Me.GroupBox9.Location = New System.Drawing.Point(13, 460)
        Me.GroupBox9.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox9.Size = New System.Drawing.Size(351, 49)
        Me.GroupBox9.TabIndex = 584
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Branch"
        '
        'cbStatus
        '
        Me.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbStatus.FormattingEnabled = True
        Me.cbStatus.Location = New System.Drawing.Point(32, 16)
        Me.cbStatus.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.Size = New System.Drawing.Size(290, 24)
        Me.cbStatus.TabIndex = 882
        '
        'bgwExport
        '
        Me.bgwExport.WorkerReportsProgress = True
        Me.bgwExport.WorkerSupportsCancellation = True
        '
        'pgbCounter
        '
        Me.pgbCounter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgbCounter.BackColor = System.Drawing.Color.Gainsboro
        Me.pgbCounter.ForeColor = System.Drawing.Color.Red
        Me.pgbCounter.Location = New System.Drawing.Point(826, 14)
        Me.pgbCounter.Name = "pgbCounter"
        Me.pgbCounter.Size = New System.Drawing.Size(244, 18)
        Me.pgbCounter.TabIndex = 1068
        Me.pgbCounter.Visible = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(691, 8)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(102, 30)
        Me.Button2.TabIndex = 1069
        Me.Button2.Text = "Export"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'frmGLFinacialReportGenerator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1192, 733)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.pgbCounter)
        Me.Controls.Add(Me.GroupBox9)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtTotalVariance)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.GroupBox8)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.txtTotalCredit)
        Me.Controls.Add(Me.txtTotalDebit)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.dgvData)
        Me.Controls.Add(Me.btnGenerate)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmGLFinacialReportGenerator"
        Me.Text = "Query Generator"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nupYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rbSummary As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rbOrderDate As System.Windows.Forms.RadioButton
    Friend WithEvents rbAccountTitle As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cbAccountTo As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbAccountFrom As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbToDepartment As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbFromDepartment As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents rbDetailed As System.Windows.Forms.RadioButton
    Friend WithEvents rbOrderTransactionNumber As System.Windows.Forms.RadioButton
    Friend WithEvents rbOrderSubType As System.Windows.Forms.RadioButton
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents dgvData As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtTotalCredit As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalDebit As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnGeneratePEC As System.Windows.Forms.Button
    Friend WithEvents btnPostPEC As System.Windows.Forms.Button
    Friend WithEvents btnGenerateBB As System.Windows.Forms.Button
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents btnPostBB As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cbFiscal As System.Windows.Forms.ComboBox
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkPEC As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbMonth As System.Windows.Forms.ComboBox
    Friend WithEvents nupYear As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents chkJV As System.Windows.Forms.CheckBox
    Friend WithEvents chkDV As System.Windows.Forms.CheckBox
    Friend WithEvents chkOR As System.Windows.Forms.CheckBox
    Friend WithEvents rbAccountCode As System.Windows.Forms.RadioButton
    Friend WithEvents rbRefID As System.Windows.Forms.RadioButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents chkBB As System.Windows.Forms.CheckBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtTotalVariance As System.Windows.Forms.TextBox
    Friend WithEvents rbBalanceSheet As System.Windows.Forms.RadioButton
    Friend WithEvents rbTrialBalance As System.Windows.Forms.RadioButton
    Friend WithEvents CmdPreview As System.Windows.Forms.Button
    Friend WithEvents rbIncomeStatement As System.Windows.Forms.RadioButton
    Friend WithEvents rbARR As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents chkPJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkORNo As System.Windows.Forms.CheckBox
    Friend WithEvents chkAR As System.Windows.Forms.CheckBox
    Friend WithEvents chkCSI As System.Windows.Forms.CheckBox
    Friend WithEvents chkCR As System.Windows.Forms.CheckBox
    Friend WithEvents chSJ As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents cbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents dtpPECdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents dtpLastPECdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents chkPayable As System.Windows.Forms.CheckBox
    Friend WithEvents bgwExport As System.ComponentModel.BackgroundWorker
    Friend WithEvents pgbCounter As System.Windows.Forms.ProgressBar
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents chkInventory As System.Windows.Forms.CheckBox
End Class
