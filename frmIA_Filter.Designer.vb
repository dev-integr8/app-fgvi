<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIA_Filter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIA_Filter))
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbPeriod = New System.Windows.Forms.ComboBox()
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
        Me.tcFilter = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.rbSpecific = New System.Windows.Forms.RadioButton()
        Me.rbNone = New System.Windows.Forms.RadioButton()
        Me.rbAll = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbWHSEFilter = New System.Windows.Forms.ComboBox()
        Me.cbWHSECategory = New System.Windows.Forms.ComboBox()
        Me.lvWHSE = New System.Windows.Forms.ListView()
        Me.chCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chStoreLoc = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.rbPWHSEspecific = New System.Windows.Forms.RadioButton()
        Me.rbPWHSEnone = New System.Windows.Forms.RadioButton()
        Me.rbPWHSEall = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cbPWHSEfilter = New System.Windows.Forms.ComboBox()
        Me.cbPWHSECategory = New System.Windows.Forms.ComboBox()
        Me.lvPWHSE = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cbItemOwner = New System.Windows.Forms.ComboBox()
        Me.rbItemSpecific = New System.Windows.Forms.RadioButton()
        Me.rbItemNone = New System.Windows.Forms.RadioButton()
        Me.rbItemAll = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbItemCategory = New System.Windows.Forms.ComboBox()
        Me.cbItemType = New System.Windows.Forms.ComboBox()
        Me.lvItem = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.gbPeriodYM.SuspendLayout()
        CType(Me.nupYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbPeriodFT.SuspendLayout()
        Me.tcFilter.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(324, 376)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 30)
        Me.btnOK.TabIndex = 17
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(405, 376)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 30)
        Me.btnCancel.TabIndex = 18
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 16)
        Me.Label2.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 32)
        Me.Label2.TabIndex = 605
        Me.Label2.Text = "Period " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   Type :"
        '
        'cbPeriod
        '
        Me.cbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPeriod.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPeriod.FormattingEnabled = True
        Me.cbPeriod.Items.AddRange(New Object() {"Yearly", "Monthly", "Daily", "Date Range"})
        Me.cbPeriod.Location = New System.Drawing.Point(71, 21)
        Me.cbPeriod.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.cbPeriod.Name = "cbPeriod"
        Me.cbPeriod.Size = New System.Drawing.Size(333, 24)
        Me.cbPeriod.TabIndex = 604
        '
        'gbPeriodYM
        '
        Me.gbPeriodYM.Controls.Add(Me.chkYTD)
        Me.gbPeriodYM.Controls.Add(Me.nupYear)
        Me.gbPeriodYM.Controls.Add(Me.lblMonth)
        Me.gbPeriodYM.Controls.Add(Me.Label4)
        Me.gbPeriodYM.Controls.Add(Me.cbMonth)
        Me.gbPeriodYM.Location = New System.Drawing.Point(71, 51)
        Me.gbPeriodYM.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.gbPeriodYM.Name = "gbPeriodYM"
        Me.gbPeriodYM.Padding = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.gbPeriodYM.Size = New System.Drawing.Size(300, 75)
        Me.gbPeriodYM.TabIndex = 602
        Me.gbPeriodYM.TabStop = False
        Me.gbPeriodYM.Text = "Period"
        '
        'chkYTD
        '
        Me.chkYTD.AutoSize = True
        Me.chkYTD.Location = New System.Drawing.Point(169, 15)
        Me.chkYTD.Name = "chkYTD"
        Me.chkYTD.Size = New System.Drawing.Size(49, 19)
        Me.chkYTD.TabIndex = 600
        Me.chkYTD.Text = "YTD"
        Me.chkYTD.UseVisualStyleBackColor = True
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
        Me.lblMonth.Location = New System.Drawing.Point(13, 44)
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
        Me.Label4.Location = New System.Drawing.Point(20, 19)
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
        Me.gbPeriodFT.Location = New System.Drawing.Point(71, 51)
        Me.gbPeriodFT.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.gbPeriodFT.Name = "gbPeriodFT"
        Me.gbPeriodFT.Padding = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.gbPeriodFT.Size = New System.Drawing.Size(300, 75)
        Me.gbPeriodFT.TabIndex = 603
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
        Me.dtpFrom.Size = New System.Drawing.Size(192, 22)
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
        Me.dtpTo.Size = New System.Drawing.Size(192, 22)
        Me.dtpTo.TabIndex = 593
        '
        'tcFilter
        '
        Me.tcFilter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcFilter.Controls.Add(Me.TabPage1)
        Me.tcFilter.Controls.Add(Me.TabPage2)
        Me.tcFilter.Controls.Add(Me.TabPage3)
        Me.tcFilter.Controls.Add(Me.TabPage4)
        Me.tcFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.tcFilter.Location = New System.Drawing.Point(12, 12)
        Me.tcFilter.Name = "tcFilter"
        Me.tcFilter.SelectedIndex = 0
        Me.tcFilter.Size = New System.Drawing.Size(467, 358)
        Me.tcFilter.TabIndex = 607
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.cbPeriod)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.gbPeriodYM)
        Me.TabPage1.Controls.Add(Me.gbPeriodFT)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(459, 330)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Period"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.rbSpecific)
        Me.TabPage2.Controls.Add(Me.rbNone)
        Me.TabPage2.Controls.Add(Me.rbAll)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.cbWHSEFilter)
        Me.TabPage2.Controls.Add(Me.cbWHSECategory)
        Me.TabPage2.Controls.Add(Me.lvWHSE)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(459, 330)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Warehouse"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'rbSpecific
        '
        Me.rbSpecific.AutoSize = True
        Me.rbSpecific.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.rbSpecific.Location = New System.Drawing.Point(136, 70)
        Me.rbSpecific.Name = "rbSpecific"
        Me.rbSpecific.Size = New System.Drawing.Size(75, 21)
        Me.rbSpecific.TabIndex = 868
        Me.rbSpecific.TabStop = True
        Me.rbSpecific.Text = "Specific"
        Me.rbSpecific.UseVisualStyleBackColor = True
        '
        'rbNone
        '
        Me.rbNone.AutoSize = True
        Me.rbNone.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.rbNone.Location = New System.Drawing.Point(218, 70)
        Me.rbNone.Name = "rbNone"
        Me.rbNone.Size = New System.Drawing.Size(90, 21)
        Me.rbNone.TabIndex = 867
        Me.rbNone.TabStop = True
        Me.rbNone.Text = "Pick None"
        Me.rbNone.UseVisualStyleBackColor = True
        '
        'rbAll
        '
        Me.rbAll.AutoSize = True
        Me.rbAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.rbAll.Location = New System.Drawing.Point(84, 70)
        Me.rbAll.Name = "rbAll"
        Me.rbAll.Size = New System.Drawing.Size(41, 21)
        Me.rbAll.TabIndex = 866
        Me.rbAll.TabStop = True
        Me.rbAll.Text = "All"
        Me.rbAll.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(35, 41)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 15)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Filter :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 15)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Category ;"
        '
        'cbWHSEFilter
        '
        Me.cbWHSEFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbWHSEFilter.FormattingEnabled = True
        Me.cbWHSEFilter.Location = New System.Drawing.Point(81, 38)
        Me.cbWHSEFilter.Name = "cbWHSEFilter"
        Me.cbWHSEFilter.Size = New System.Drawing.Size(325, 23)
        Me.cbWHSEFilter.TabIndex = 4
        '
        'cbWHSECategory
        '
        Me.cbWHSECategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbWHSECategory.FormattingEnabled = True
        Me.cbWHSECategory.Location = New System.Drawing.Point(81, 11)
        Me.cbWHSECategory.Name = "cbWHSECategory"
        Me.cbWHSECategory.Size = New System.Drawing.Size(325, 23)
        Me.cbWHSECategory.TabIndex = 3
        '
        'lvWHSE
        '
        Me.lvWHSE.CheckBoxes = True
        Me.lvWHSE.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chCode, Me.chStoreLoc})
        Me.lvWHSE.FullRowSelect = True
        Me.lvWHSE.Location = New System.Drawing.Point(6, 97)
        Me.lvWHSE.MultiSelect = False
        Me.lvWHSE.Name = "lvWHSE"
        Me.lvWHSE.Size = New System.Drawing.Size(447, 222)
        Me.lvWHSE.TabIndex = 0
        Me.lvWHSE.UseCompatibleStateImageBehavior = False
        Me.lvWHSE.View = System.Windows.Forms.View.Details
        '
        'chCode
        '
        Me.chCode.Text = "Code"
        Me.chCode.Width = 110
        '
        'chStoreLoc
        '
        Me.chStoreLoc.Text = "Store Locations"
        Me.chStoreLoc.Width = 250
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.rbPWHSEspecific)
        Me.TabPage3.Controls.Add(Me.rbPWHSEnone)
        Me.TabPage3.Controls.Add(Me.rbPWHSEall)
        Me.TabPage3.Controls.Add(Me.Label7)
        Me.TabPage3.Controls.Add(Me.Label8)
        Me.TabPage3.Controls.Add(Me.cbPWHSEfilter)
        Me.TabPage3.Controls.Add(Me.cbPWHSECategory)
        Me.TabPage3.Controls.Add(Me.lvPWHSE)
        Me.TabPage3.Location = New System.Drawing.Point(4, 24)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(459, 330)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Production Location"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'rbPWHSEspecific
        '
        Me.rbPWHSEspecific.AutoSize = True
        Me.rbPWHSEspecific.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.rbPWHSEspecific.Location = New System.Drawing.Point(136, 70)
        Me.rbPWHSEspecific.Name = "rbPWHSEspecific"
        Me.rbPWHSEspecific.Size = New System.Drawing.Size(75, 21)
        Me.rbPWHSEspecific.TabIndex = 876
        Me.rbPWHSEspecific.TabStop = True
        Me.rbPWHSEspecific.Text = "Specific"
        Me.rbPWHSEspecific.UseVisualStyleBackColor = True
        '
        'rbPWHSEnone
        '
        Me.rbPWHSEnone.AutoSize = True
        Me.rbPWHSEnone.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.rbPWHSEnone.Location = New System.Drawing.Point(218, 70)
        Me.rbPWHSEnone.Name = "rbPWHSEnone"
        Me.rbPWHSEnone.Size = New System.Drawing.Size(90, 21)
        Me.rbPWHSEnone.TabIndex = 875
        Me.rbPWHSEnone.TabStop = True
        Me.rbPWHSEnone.Text = "Pick None"
        Me.rbPWHSEnone.UseVisualStyleBackColor = True
        '
        'rbPWHSEall
        '
        Me.rbPWHSEall.AutoSize = True
        Me.rbPWHSEall.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.rbPWHSEall.Location = New System.Drawing.Point(84, 70)
        Me.rbPWHSEall.Name = "rbPWHSEall"
        Me.rbPWHSEall.Size = New System.Drawing.Size(41, 21)
        Me.rbPWHSEall.TabIndex = 874
        Me.rbPWHSEall.TabStop = True
        Me.rbPWHSEall.Text = "All"
        Me.rbPWHSEall.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(35, 41)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 15)
        Me.Label7.TabIndex = 873
        Me.Label7.Text = "Filter :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(14, 14)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(61, 15)
        Me.Label8.TabIndex = 872
        Me.Label8.Text = "Category ;"
        '
        'cbPWHSEfilter
        '
        Me.cbPWHSEfilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPWHSEfilter.FormattingEnabled = True
        Me.cbPWHSEfilter.Location = New System.Drawing.Point(81, 38)
        Me.cbPWHSEfilter.Name = "cbPWHSEfilter"
        Me.cbPWHSEfilter.Size = New System.Drawing.Size(326, 23)
        Me.cbPWHSEfilter.TabIndex = 871
        '
        'cbPWHSECategory
        '
        Me.cbPWHSECategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPWHSECategory.FormattingEnabled = True
        Me.cbPWHSECategory.Location = New System.Drawing.Point(81, 11)
        Me.cbPWHSECategory.Name = "cbPWHSECategory"
        Me.cbPWHSECategory.Size = New System.Drawing.Size(326, 23)
        Me.cbPWHSECategory.TabIndex = 870
        '
        'lvPWHSE
        '
        Me.lvPWHSE.CheckBoxes = True
        Me.lvPWHSE.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lvPWHSE.FullRowSelect = True
        Me.lvPWHSE.Location = New System.Drawing.Point(6, 97)
        Me.lvPWHSE.MultiSelect = False
        Me.lvPWHSE.Name = "lvPWHSE"
        Me.lvPWHSE.Size = New System.Drawing.Size(447, 222)
        Me.lvPWHSE.TabIndex = 869
        Me.lvPWHSE.UseCompatibleStateImageBehavior = False
        Me.lvPWHSE.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Code"
        Me.ColumnHeader1.Width = 110
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Store Locations"
        Me.ColumnHeader2.Width = 250
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Label9)
        Me.TabPage4.Controls.Add(Me.cbItemOwner)
        Me.TabPage4.Controls.Add(Me.rbItemSpecific)
        Me.TabPage4.Controls.Add(Me.rbItemNone)
        Me.TabPage4.Controls.Add(Me.rbItemAll)
        Me.TabPage4.Controls.Add(Me.Label1)
        Me.TabPage4.Controls.Add(Me.Label3)
        Me.TabPage4.Controls.Add(Me.cbItemCategory)
        Me.TabPage4.Controls.Add(Me.cbItemType)
        Me.TabPage4.Controls.Add(Me.lvItem)
        Me.TabPage4.Location = New System.Drawing.Point(4, 24)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(459, 330)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Item"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(11, 66)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(76, 15)
        Me.Label9.TabIndex = 886
        Me.Label9.Text = "Item Owner :"
        '
        'cbItemOwner
        '
        Me.cbItemOwner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbItemOwner.FormattingEnabled = True
        Me.cbItemOwner.Location = New System.Drawing.Point(90, 63)
        Me.cbItemOwner.Name = "cbItemOwner"
        Me.cbItemOwner.Size = New System.Drawing.Size(333, 23)
        Me.cbItemOwner.TabIndex = 885
        '
        'rbItemSpecific
        '
        Me.rbItemSpecific.AutoSize = True
        Me.rbItemSpecific.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.rbItemSpecific.Location = New System.Drawing.Point(147, 94)
        Me.rbItemSpecific.Name = "rbItemSpecific"
        Me.rbItemSpecific.Size = New System.Drawing.Size(75, 21)
        Me.rbItemSpecific.TabIndex = 884
        Me.rbItemSpecific.TabStop = True
        Me.rbItemSpecific.Text = "Specific"
        Me.rbItemSpecific.UseVisualStyleBackColor = True
        '
        'rbItemNone
        '
        Me.rbItemNone.AutoSize = True
        Me.rbItemNone.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.rbItemNone.Location = New System.Drawing.Point(229, 94)
        Me.rbItemNone.Name = "rbItemNone"
        Me.rbItemNone.Size = New System.Drawing.Size(90, 21)
        Me.rbItemNone.TabIndex = 883
        Me.rbItemNone.TabStop = True
        Me.rbItemNone.Text = "Pick None"
        Me.rbItemNone.UseVisualStyleBackColor = True
        '
        'rbItemAll
        '
        Me.rbItemAll.AutoSize = True
        Me.rbItemAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.rbItemAll.Location = New System.Drawing.Point(95, 94)
        Me.rbItemAll.Name = "rbItemAll"
        Me.rbItemAll.Size = New System.Drawing.Size(41, 21)
        Me.rbItemAll.TabIndex = 882
        Me.rbItemAll.TabStop = True
        Me.rbItemAll.Text = "All"
        Me.rbItemAll.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 15)
        Me.Label1.TabIndex = 881
        Me.Label1.Text = "Category :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(48, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 15)
        Me.Label3.TabIndex = 880
        Me.Label3.Text = "Type :"
        '
        'cbItemCategory
        '
        Me.cbItemCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbItemCategory.FormattingEnabled = True
        Me.cbItemCategory.Location = New System.Drawing.Point(90, 37)
        Me.cbItemCategory.Name = "cbItemCategory"
        Me.cbItemCategory.Size = New System.Drawing.Size(333, 23)
        Me.cbItemCategory.TabIndex = 879
        '
        'cbItemType
        '
        Me.cbItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbItemType.FormattingEnabled = True
        Me.cbItemType.Location = New System.Drawing.Point(90, 11)
        Me.cbItemType.Name = "cbItemType"
        Me.cbItemType.Size = New System.Drawing.Size(333, 23)
        Me.cbItemType.TabIndex = 878
        '
        'lvItem
        '
        Me.lvItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvItem.CheckBoxes = True
        Me.lvItem.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader4})
        Me.lvItem.FullRowSelect = True
        Me.lvItem.Location = New System.Drawing.Point(6, 122)
        Me.lvItem.MultiSelect = False
        Me.lvItem.Name = "lvItem"
        Me.lvItem.Size = New System.Drawing.Size(447, 197)
        Me.lvItem.TabIndex = 877
        Me.lvItem.UseCompatibleStateImageBehavior = False
        Me.lvItem.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Code"
        Me.ColumnHeader3.Width = 110
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Description"
        Me.ColumnHeader4.Width = 337
        '
        'frmIA_Filter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(486, 414)
        Me.Controls.Add(Me.tcFilter)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmIA_Filter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Filter"
        Me.gbPeriodYM.ResumeLayout(False)
        Me.gbPeriodYM.PerformLayout()
        CType(Me.nupYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbPeriodFT.ResumeLayout(False)
        Me.gbPeriodFT.PerformLayout()
        Me.tcFilter.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents gbPeriodYM As System.Windows.Forms.GroupBox
    Friend WithEvents nupYear As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMonth As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbMonth As System.Windows.Forms.ComboBox
    Friend WithEvents gbPeriodFT As System.Windows.Forms.GroupBox
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkYTD As System.Windows.Forms.CheckBox
    Friend WithEvents tcFilter As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents lvWHSE As System.Windows.Forms.ListView
    Friend WithEvents chCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents chStoreLoc As System.Windows.Forms.ColumnHeader
    Friend WithEvents cbWHSEFilter As System.Windows.Forms.ComboBox
    Friend WithEvents cbWHSECategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents rbSpecific As System.Windows.Forms.RadioButton
    Friend WithEvents rbNone As System.Windows.Forms.RadioButton
    Friend WithEvents rbAll As System.Windows.Forms.RadioButton
    Friend WithEvents rbPWHSEspecific As System.Windows.Forms.RadioButton
    Friend WithEvents rbPWHSEnone As System.Windows.Forms.RadioButton
    Friend WithEvents rbPWHSEall As System.Windows.Forms.RadioButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cbPWHSEfilter As System.Windows.Forms.ComboBox
    Friend WithEvents cbPWHSECategory As System.Windows.Forms.ComboBox
    Friend WithEvents lvPWHSE As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cbItemOwner As System.Windows.Forms.ComboBox
    Friend WithEvents rbItemSpecific As System.Windows.Forms.RadioButton
    Friend WithEvents rbItemNone As System.Windows.Forms.RadioButton
    Friend WithEvents rbItemAll As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbItemCategory As System.Windows.Forms.ComboBox
    Friend WithEvents cbItemType As System.Windows.Forms.ComboBox
    Friend WithEvents lvItem As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
End Class
