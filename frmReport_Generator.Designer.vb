<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReport_Generator
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReport_Generator))
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lvFilter = New System.Windows.Forms.ListView()
        Me.chFilter = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.rbSpecific = New System.Windows.Forms.RadioButton()
        Me.rbNone = New System.Windows.Forms.RadioButton()
        Me.rbAll = New System.Windows.Forms.RadioButton()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.cbCostCenter = New System.Windows.Forms.ComboBox()
        Me.pgBar = New System.Windows.Forms.ProgressBar()
        Me.cbReportType = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbPeriod = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cbReportCategory = New System.Windows.Forms.ComboBox()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.gbPeriodYM = New System.Windows.Forms.GroupBox()
        Me.chkYTD = New System.Windows.Forms.CheckBox()
        Me.nupYear = New System.Windows.Forms.NumericUpDown()
        Me.lblMonth = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbMonth = New System.Windows.Forms.ComboBox()
        Me.gbPeriodFT = New System.Windows.Forms.GroupBox()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.chkInclude = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbVCEFilter = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbFilter = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbSpecific2 = New System.Windows.Forms.RadioButton()
        Me.rbNone2 = New System.Windows.Forms.RadioButton()
        Me.rbAll2 = New System.Windows.Forms.RadioButton()
        Me.lvFilter2 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblType = New System.Windows.Forms.Label()
        Me.cmbLoanType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbBranch = New System.Windows.Forms.ComboBox()
        Me.gbType = New System.Windows.Forms.GroupBox()
        Me.rbDetailed = New System.Windows.Forms.RadioButton()
        Me.rbSummary = New System.Windows.Forms.RadioButton()
        Me.dgvCPR = New System.Windows.Forms.DataGridView()
        Me.dgcSort = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBank = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.bgwIS = New System.ComponentModel.BackgroundWorker()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.gbPeriodYM.SuspendLayout()
        CType(Me.nupYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbPeriodFT.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.gbType.SuspendLayout()
        CType(Me.dgvCPR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.Controls.Add(Me.Label6)
        Me.GroupBox6.Controls.Add(Me.GroupBox1)
        Me.GroupBox6.Controls.Add(Me.btnExport)
        Me.GroupBox6.Controls.Add(Me.cbCostCenter)
        Me.GroupBox6.Controls.Add(Me.pgBar)
        Me.GroupBox6.Controls.Add(Me.cbReportType)
        Me.GroupBox6.Controls.Add(Me.Label2)
        Me.GroupBox6.Controls.Add(Me.cbPeriod)
        Me.GroupBox6.Controls.Add(Me.Label14)
        Me.GroupBox6.Controls.Add(Me.cbReportCategory)
        Me.GroupBox6.Controls.Add(Me.btnPreview)
        Me.GroupBox6.Controls.Add(Me.gbPeriodYM)
        Me.GroupBox6.Controls.Add(Me.gbPeriodFT)
        Me.GroupBox6.Controls.Add(Me.chkInclude)
        Me.GroupBox6.Controls.Add(Me.Label5)
        Me.GroupBox6.Controls.Add(Me.cbVCEFilter)
        Me.GroupBox6.Controls.Add(Me.Label3)
        Me.GroupBox6.Controls.Add(Me.cbFilter)
        Me.GroupBox6.Controls.Add(Me.GroupBox2)
        Me.GroupBox6.Location = New System.Drawing.Point(8, 1)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.GroupBox6.Size = New System.Drawing.Size(442, 665)
        Me.GroupBox6.TabIndex = 580
        Me.GroupBox6.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(2, 504)
        Me.Label6.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 15)
        Me.Label6.TabIndex = 881
        Me.Label6.Text = "Res Center :"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lvFilter)
        Me.GroupBox1.Controls.Add(Me.rbSpecific)
        Me.GroupBox1.Controls.Add(Me.rbNone)
        Me.GroupBox1.Controls.Add(Me.rbAll)
        Me.GroupBox1.Location = New System.Drawing.Point(82, 197)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(323, 115)
        Me.GroupBox1.TabIndex = 866
        Me.GroupBox1.TabStop = False
        '
        'lvFilter
        '
        Me.lvFilter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvFilter.CheckBoxes = True
        Me.lvFilter.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chFilter})
        Me.lvFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvFilter.HideSelection = False
        Me.lvFilter.Location = New System.Drawing.Point(1, 35)
        Me.lvFilter.MultiSelect = False
        Me.lvFilter.Name = "lvFilter"
        Me.lvFilter.Size = New System.Drawing.Size(316, 75)
        Me.lvFilter.TabIndex = 598
        Me.lvFilter.UseCompatibleStateImageBehavior = False
        Me.lvFilter.View = System.Windows.Forms.View.Details
        '
        'chFilter
        '
        Me.chFilter.Text = "Filter"
        Me.chFilter.Width = 308
        '
        'rbSpecific
        '
        Me.rbSpecific.AutoSize = True
        Me.rbSpecific.Location = New System.Drawing.Point(73, 12)
        Me.rbSpecific.Name = "rbSpecific"
        Me.rbSpecific.Size = New System.Drawing.Size(74, 20)
        Me.rbSpecific.TabIndex = 865
        Me.rbSpecific.TabStop = True
        Me.rbSpecific.Text = "Specific"
        Me.rbSpecific.UseVisualStyleBackColor = True
        '
        'rbNone
        '
        Me.rbNone.AutoSize = True
        Me.rbNone.Location = New System.Drawing.Point(155, 12)
        Me.rbNone.Name = "rbNone"
        Me.rbNone.Size = New System.Drawing.Size(88, 20)
        Me.rbNone.TabIndex = 864
        Me.rbNone.TabStop = True
        Me.rbNone.Text = "Pick None"
        Me.rbNone.UseVisualStyleBackColor = True
        '
        'rbAll
        '
        Me.rbAll.AutoSize = True
        Me.rbAll.Location = New System.Drawing.Point(21, 12)
        Me.rbAll.Name = "rbAll"
        Me.rbAll.Size = New System.Drawing.Size(41, 20)
        Me.rbAll.TabIndex = 863
        Me.rbAll.TabStop = True
        Me.rbAll.Text = "All"
        Me.rbAll.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(236, 556)
        Me.btnExport.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(165, 32)
        Me.btnExport.TabIndex = 876
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'cbCostCenter
        '
        Me.cbCostCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCostCenter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCostCenter.FormattingEnabled = True
        Me.cbCostCenter.Location = New System.Drawing.Point(81, 501)
        Me.cbCostCenter.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.cbCostCenter.Name = "cbCostCenter"
        Me.cbCostCenter.Size = New System.Drawing.Size(324, 24)
        Me.cbCostCenter.TabIndex = 880
        '
        'pgBar
        '
        Me.pgBar.Location = New System.Drawing.Point(83, 595)
        Me.pgBar.Name = "pgBar"
        Me.pgBar.Size = New System.Drawing.Size(323, 19)
        Me.pgBar.TabIndex = 875
        '
        'cbReportType
        '
        Me.cbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbReportType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbReportType.FormattingEnabled = True
        Me.cbReportType.Location = New System.Drawing.Point(82, 51)
        Me.cbReportType.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.cbReportType.Name = "cbReportType"
        Me.cbReportType.Size = New System.Drawing.Size(324, 24)
        Me.cbReportType.TabIndex = 871
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(22, 79)
        Me.Label2.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 32)
        Me.Label2.TabIndex = 601
        Me.Label2.Text = "Period " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   Type :"
        '
        'cbPeriod
        '
        Me.cbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPeriod.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPeriod.FormattingEnabled = True
        Me.cbPeriod.Items.AddRange(New Object() {"Yearly", "Quarterly", "Monthly", "Daily", "Date Range", "As Of"})
        Me.cbPeriod.Location = New System.Drawing.Point(82, 84)
        Me.cbPeriod.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.cbPeriod.Name = "cbPeriod"
        Me.cbPeriod.Size = New System.Drawing.Size(324, 24)
        Me.cbPeriod.TabIndex = 600
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(20, 26)
        Me.Label14.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(55, 16)
        Me.Label14.TabIndex = 593
        Me.Label14.Text = "Report :"
        '
        'cbReportCategory
        '
        Me.cbReportCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbReportCategory.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbReportCategory.FormattingEnabled = True
        Me.cbReportCategory.Location = New System.Drawing.Point(82, 23)
        Me.cbReportCategory.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.cbReportCategory.Name = "cbReportCategory"
        Me.cbReportCategory.Size = New System.Drawing.Size(324, 24)
        Me.cbReportCategory.TabIndex = 592
        '
        'btnPreview
        '
        Me.btnPreview.Location = New System.Drawing.Point(78, 556)
        Me.btnPreview.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(158, 32)
        Me.btnPreview.TabIndex = 591
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'gbPeriodYM
        '
        Me.gbPeriodYM.Controls.Add(Me.chkYTD)
        Me.gbPeriodYM.Controls.Add(Me.nupYear)
        Me.gbPeriodYM.Controls.Add(Me.lblMonth)
        Me.gbPeriodYM.Controls.Add(Me.Label4)
        Me.gbPeriodYM.Controls.Add(Me.cbMonth)
        Me.gbPeriodYM.Location = New System.Drawing.Point(82, 114)
        Me.gbPeriodYM.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.gbPeriodYM.Name = "gbPeriodYM"
        Me.gbPeriodYM.Padding = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.gbPeriodYM.Size = New System.Drawing.Size(324, 75)
        Me.gbPeriodYM.TabIndex = 599
        Me.gbPeriodYM.TabStop = False
        Me.gbPeriodYM.Text = "Period"
        '
        'chkYTD
        '
        Me.chkYTD.AutoSize = True
        Me.chkYTD.Location = New System.Drawing.Point(169, 15)
        Me.chkYTD.Name = "chkYTD"
        Me.chkYTD.Size = New System.Drawing.Size(55, 20)
        Me.chkYTD.TabIndex = 600
        Me.chkYTD.Text = "YTD"
        Me.chkYTD.UseVisualStyleBackColor = True
        Me.chkYTD.Visible = False
        '
        'nupYear
        '
        Me.nupYear.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nupYear.Location = New System.Drawing.Point(63, 16)
        Me.nupYear.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nupYear.Name = "nupYear"
        Me.nupYear.Size = New System.Drawing.Size(68, 22)
        Me.nupYear.TabIndex = 599
        Me.nupYear.Value = New Decimal(New Integer() {2017, 0, 0, 0})
        '
        'lblMonth
        '
        Me.lblMonth.AutoSize = True
        Me.lblMonth.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonth.Location = New System.Drawing.Point(4, 44)
        Me.lblMonth.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.Size = New System.Drawing.Size(50, 16)
        Me.lblMonth.TabIndex = 597
        Me.lblMonth.Text = "Month :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 19)
        Me.Label4.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 16)
        Me.Label4.TabIndex = 596
        Me.Label4.Text = "Year :"
        '
        'cbMonth
        '
        Me.cbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMonth.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMonth.FormattingEnabled = True
        Me.cbMonth.Items.AddRange(New Object() {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"})
        Me.cbMonth.Location = New System.Drawing.Point(63, 40)
        Me.cbMonth.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.cbMonth.Name = "cbMonth"
        Me.cbMonth.Size = New System.Drawing.Size(161, 24)
        Me.cbMonth.TabIndex = 595
        '
        'gbPeriodFT
        '
        Me.gbPeriodFT.Controls.Add(Me.dtpFrom)
        Me.gbPeriodFT.Controls.Add(Me.lblFrom)
        Me.gbPeriodFT.Controls.Add(Me.lblTo)
        Me.gbPeriodFT.Controls.Add(Me.dtpTo)
        Me.gbPeriodFT.Location = New System.Drawing.Point(82, 114)
        Me.gbPeriodFT.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.gbPeriodFT.Name = "gbPeriodFT"
        Me.gbPeriodFT.Padding = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.gbPeriodFT.Size = New System.Drawing.Size(245, 75)
        Me.gbPeriodFT.TabIndex = 599
        Me.gbPeriodFT.TabStop = False
        Me.gbPeriodFT.Text = "Period"
        '
        'dtpFrom
        '
        Me.dtpFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(71, 19)
        Me.dtpFrom.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(107, 22)
        Me.dtpFrom.TabIndex = 592
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.Location = New System.Drawing.Point(25, 22)
        Me.lblFrom.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(45, 16)
        Me.lblFrom.TabIndex = 575
        Me.lblFrom.Text = "From :"
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.Location = New System.Drawing.Point(39, 44)
        Me.lblTo.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(31, 16)
        Me.lblTo.TabIndex = 576
        Me.lblTo.Text = "To :"
        '
        'dtpTo
        '
        Me.dtpTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(71, 43)
        Me.dtpTo.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(107, 22)
        Me.dtpTo.TabIndex = 593
        '
        'chkInclude
        '
        Me.chkInclude.AutoSize = True
        Me.chkInclude.Location = New System.Drawing.Point(82, 530)
        Me.chkInclude.Name = "chkInclude"
        Me.chkInclude.Size = New System.Drawing.Size(134, 20)
        Me.chkInclude.TabIndex = 874
        Me.chkInclude.Text = "Include Cancelled"
        Me.chkInclude.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(21, 475)
        Me.Label5.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 16)
        Me.Label5.TabIndex = 878
        Me.Label5.Text = "VCE :"
        '
        'cbVCEFilter
        '
        Me.cbVCEFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbVCEFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbVCEFilter.FormattingEnabled = True
        Me.cbVCEFilter.Items.AddRange(New Object() {"Yearly", "Quarterly", "Monthly", "Daily", "Date Range"})
        Me.cbVCEFilter.Location = New System.Drawing.Point(81, 472)
        Me.cbVCEFilter.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.cbVCEFilter.Name = "cbVCEFilter"
        Me.cbVCEFilter.Size = New System.Drawing.Size(323, 24)
        Me.cbVCEFilter.TabIndex = 877
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(21, 444)
        Me.Label3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 16)
        Me.Label3.TabIndex = 873
        Me.Label3.Text = "Filter :"
        '
        'cbFilter
        '
        Me.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFilter.FormattingEnabled = True
        Me.cbFilter.Items.AddRange(New Object() {"Yearly", "Quarterly", "Monthly", "Daily", "Date Range"})
        Me.cbFilter.Location = New System.Drawing.Point(82, 441)
        Me.cbFilter.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.cbFilter.Name = "cbFilter"
        Me.cbFilter.Size = New System.Drawing.Size(323, 24)
        Me.cbFilter.TabIndex = 872
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.rbSpecific2)
        Me.GroupBox2.Controls.Add(Me.rbNone2)
        Me.GroupBox2.Controls.Add(Me.rbAll2)
        Me.GroupBox2.Controls.Add(Me.lvFilter2)
        Me.GroupBox2.Location = New System.Drawing.Point(82, 318)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(323, 115)
        Me.GroupBox2.TabIndex = 879
        Me.GroupBox2.TabStop = False
        '
        'rbSpecific2
        '
        Me.rbSpecific2.AutoSize = True
        Me.rbSpecific2.Location = New System.Drawing.Point(73, 12)
        Me.rbSpecific2.Name = "rbSpecific2"
        Me.rbSpecific2.Size = New System.Drawing.Size(74, 20)
        Me.rbSpecific2.TabIndex = 865
        Me.rbSpecific2.TabStop = True
        Me.rbSpecific2.Text = "Specific"
        Me.rbSpecific2.UseVisualStyleBackColor = True
        '
        'rbNone2
        '
        Me.rbNone2.AutoSize = True
        Me.rbNone2.Location = New System.Drawing.Point(155, 12)
        Me.rbNone2.Name = "rbNone2"
        Me.rbNone2.Size = New System.Drawing.Size(88, 20)
        Me.rbNone2.TabIndex = 864
        Me.rbNone2.TabStop = True
        Me.rbNone2.Text = "Pick None"
        Me.rbNone2.UseVisualStyleBackColor = True
        '
        'rbAll2
        '
        Me.rbAll2.AutoSize = True
        Me.rbAll2.Location = New System.Drawing.Point(21, 12)
        Me.rbAll2.Name = "rbAll2"
        Me.rbAll2.Size = New System.Drawing.Size(41, 20)
        Me.rbAll2.TabIndex = 863
        Me.rbAll2.TabStop = True
        Me.rbAll2.Text = "All"
        Me.rbAll2.UseVisualStyleBackColor = True
        '
        'lvFilter2
        '
        Me.lvFilter2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvFilter2.CheckBoxes = True
        Me.lvFilter2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.lvFilter2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvFilter2.HideSelection = False
        Me.lvFilter2.Location = New System.Drawing.Point(1, 35)
        Me.lvFilter2.MultiSelect = False
        Me.lvFilter2.Name = "lvFilter2"
        Me.lvFilter2.Size = New System.Drawing.Size(316, 74)
        Me.lvFilter2.TabIndex = 598
        Me.lvFilter2.UseCompatibleStateImageBehavior = False
        Me.lvFilter2.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Filter"
        Me.ColumnHeader1.Width = 308
        '
        'lblType
        '
        Me.lblType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblType.AutoSize = True
        Me.lblType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.Location = New System.Drawing.Point(482, 359)
        Me.lblType.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(79, 16)
        Me.lblType.TabIndex = 870
        Me.lblType.Text = "Loan Type :"
        Me.lblType.Visible = False
        '
        'cmbLoanType
        '
        Me.cmbLoanType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbLoanType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLoanType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLoanType.FormattingEnabled = True
        Me.cmbLoanType.Items.AddRange(New Object() {"Yearly", "Monthly", "Daily", "Date Range"})
        Me.cmbLoanType.Location = New System.Drawing.Point(562, 356)
        Me.cmbLoanType.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.cmbLoanType.Name = "cmbLoanType"
        Me.cmbLoanType.Size = New System.Drawing.Size(242, 24)
        Me.cmbLoanType.TabIndex = 869
        Me.cmbLoanType.Visible = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(486, 287)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 868
        Me.Label1.Text = "Branch :"
        '
        'cbBranch
        '
        Me.cbBranch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBranch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbBranch.FormattingEnabled = True
        Me.cbBranch.Items.AddRange(New Object() {"Yearly", "Monthly", "Daily", "Date Range"})
        Me.cbBranch.Location = New System.Drawing.Point(546, 284)
        Me.cbBranch.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.cbBranch.Name = "cbBranch"
        Me.cbBranch.Size = New System.Drawing.Size(245, 24)
        Me.cbBranch.TabIndex = 867
        '
        'gbType
        '
        Me.gbType.Controls.Add(Me.rbDetailed)
        Me.gbType.Controls.Add(Me.rbSummary)
        Me.gbType.Location = New System.Drawing.Point(486, 136)
        Me.gbType.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.gbType.Name = "gbType"
        Me.gbType.Padding = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.gbType.Size = New System.Drawing.Size(245, 50)
        Me.gbType.TabIndex = 578
        Me.gbType.TabStop = False
        Me.gbType.Text = "Report Type"
        Me.gbType.Visible = False
        '
        'rbDetailed
        '
        Me.rbDetailed.AutoSize = True
        Me.rbDetailed.Location = New System.Drawing.Point(103, 22)
        Me.rbDetailed.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.rbDetailed.Name = "rbDetailed"
        Me.rbDetailed.Size = New System.Drawing.Size(77, 20)
        Me.rbDetailed.TabIndex = 1
        Me.rbDetailed.Text = "Detailed"
        Me.rbDetailed.UseVisualStyleBackColor = True
        '
        'rbSummary
        '
        Me.rbSummary.AutoSize = True
        Me.rbSummary.Checked = True
        Me.rbSummary.Location = New System.Drawing.Point(10, 22)
        Me.rbSummary.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.rbSummary.Name = "rbSummary"
        Me.rbSummary.Size = New System.Drawing.Size(83, 20)
        Me.rbSummary.TabIndex = 0
        Me.rbSummary.TabStop = True
        Me.rbSummary.Text = "Summary"
        Me.rbSummary.UseVisualStyleBackColor = True
        '
        'dgvCPR
        '
        Me.dgvCPR.AllowUserToAddRows = False
        Me.dgvCPR.AllowUserToDeleteRows = False
        Me.dgvCPR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCPR.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcSort, Me.dgcBank, Me.dgcAmount})
        Me.dgvCPR.Location = New System.Drawing.Point(597, 427)
        Me.dgvCPR.Name = "dgvCPR"
        Me.dgvCPR.Size = New System.Drawing.Size(361, 339)
        Me.dgvCPR.TabIndex = 581
        Me.dgvCPR.Visible = False
        '
        'dgcSort
        '
        Me.dgcSort.HeaderText = "SortID"
        Me.dgcSort.Name = "dgcSort"
        '
        'dgcBank
        '
        Me.dgcBank.HeaderText = "Bank"
        Me.dgcBank.Name = "dgcBank"
        '
        'dgcAmount
        '
        Me.dgcAmount.HeaderText = "Amount"
        Me.dgcAmount.Name = "dgcAmount"
        '
        'bgwIS
        '
        Me.bgwIS.WorkerReportsProgress = True
        '
        'frmReport_Generator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(464, 672)
        Me.Controls.Add(Me.dgvCPR)
        Me.Controls.Add(Me.lblType)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.cmbLoanType)
        Me.Controls.Add(Me.gbType)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbBranch)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmReport_Generator"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report Generator"
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gbPeriodYM.ResumeLayout(False)
        Me.gbPeriodYM.PerformLayout()
        CType(Me.nupYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbPeriodFT.ResumeLayout(False)
        Me.gbPeriodFT.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.gbType.ResumeLayout(False)
        Me.gbType.PerformLayout()
        CType(Me.dgvCPR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents gbType As System.Windows.Forms.GroupBox
    Friend WithEvents rbDetailed As System.Windows.Forms.RadioButton
    Friend WithEvents rbSummary As System.Windows.Forms.RadioButton
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cbReportCategory As System.Windows.Forms.ComboBox
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents gbPeriodYM As System.Windows.Forms.GroupBox
    Friend WithEvents chkYTD As System.Windows.Forms.CheckBox
    Friend WithEvents nupYear As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMonth As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbMonth As System.Windows.Forms.ComboBox
    Friend WithEvents gbPeriodFT As System.Windows.Forms.GroupBox
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbBranch As System.Windows.Forms.ComboBox
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents cmbLoanType As System.Windows.Forms.ComboBox
    Friend WithEvents dgvCPR As System.Windows.Forms.DataGridView
    Friend WithEvents dgcSort As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcBank As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cbReportType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbFilter As System.Windows.Forms.ComboBox
    Friend WithEvents chkInclude As System.Windows.Forms.CheckBox
    Friend WithEvents bgwIS As System.ComponentModel.BackgroundWorker
    Friend WithEvents pgBar As System.Windows.Forms.ProgressBar
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbVCEFilter As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lvFilter As System.Windows.Forms.ListView
    Friend WithEvents chFilter As System.Windows.Forms.ColumnHeader
    Friend WithEvents rbSpecific As System.Windows.Forms.RadioButton
    Friend WithEvents rbNone As System.Windows.Forms.RadioButton
    Friend WithEvents rbAll As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lvFilter2 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents rbSpecific2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbNone2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbAll2 As System.Windows.Forms.RadioButton
    Friend WithEvents Label6 As Label
    Friend WithEvents cbCostCenter As ComboBox
End Class
