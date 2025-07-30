<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBranch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBranch))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvBranch = New System.Windows.Forms.DataGridView()
        Me.dgcBranch = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBranchCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgvBusinessType = New System.Windows.Forms.DataGridView()
        Me.chActivity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chTransaction = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.dtpStartEffectDate = New System.Windows.Forms.DateTimePicker()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtCTENo = New System.Windows.Forms.TextBox()
        Me.chkwCTE = New System.Windows.Forms.CheckBox()
        Me.dtpEndEffectDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpDateIssue = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtTinNo = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dtpLastDateAmendment = New System.Windows.Forms.DateTimePicker()
        Me.dtpRegDate = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtLatestAmendmentNo = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCOR_No = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtProvince = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtRegion = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBranchCode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtBranchName = New System.Windows.Forms.TextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        CType(Me.dgvBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvBusinessType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(264, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(141, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Registered Name :"
        '
        'dgvBranch
        '
        Me.dgvBranch.AllowUserToAddRows = False
        Me.dgvBranch.AllowUserToDeleteRows = False
        Me.dgvBranch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgvBranch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBranch.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcBranch, Me.dgcBranchCode})
        Me.dgvBranch.Location = New System.Drawing.Point(12, 55)
        Me.dgvBranch.Name = "dgvBranch"
        Me.dgvBranch.ReadOnly = True
        Me.dgvBranch.RowHeadersVisible = False
        Me.dgvBranch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvBranch.Size = New System.Drawing.Size(235, 527)
        Me.dgvBranch.TabIndex = 1
        '
        'dgcBranch
        '
        Me.dgcBranch.HeaderText = "Branch"
        Me.dgcBranch.Name = "dgcBranch"
        Me.dgcBranch.ReadOnly = True
        Me.dgcBranch.Width = 70
        '
        'dgcBranchCode
        '
        Me.dgcBranchCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.dgcBranchCode.HeaderText = "Branch Code"
        Me.dgcBranchCode.Name = "dgcBranchCode"
        Me.dgcBranchCode.ReadOnly = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.dgvBusinessType)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtTinNo)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtAddress)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtProvince)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtRegion)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtBranchCode)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtBranchName)
        Me.GroupBox1.Location = New System.Drawing.Point(262, 86)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(793, 496)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Branch Details"
        '
        'dgvBusinessType
        '
        Me.dgvBusinessType.AllowUserToAddRows = False
        Me.dgvBusinessType.AllowUserToDeleteRows = False
        Me.dgvBusinessType.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvBusinessType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBusinessType.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chActivity, Me.chTransaction})
        Me.dgvBusinessType.Location = New System.Drawing.Point(6, 345)
        Me.dgvBusinessType.Name = "dgvBusinessType"
        Me.dgvBusinessType.ReadOnly = True
        Me.dgvBusinessType.RowHeadersWidth = 25
        Me.dgvBusinessType.Size = New System.Drawing.Size(781, 145)
        Me.dgvBusinessType.TabIndex = 20
        '
        'chActivity
        '
        Me.chActivity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.chActivity.HeaderText = "Business Activity"
        Me.chActivity.Name = "chActivity"
        Me.chActivity.ReadOnly = True
        '
        'chTransaction
        '
        Me.chTransaction.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.chTransaction.HeaderText = "Transaction Type"
        Me.chTransaction.Items.AddRange(New Object() {"Members Only", "Members and Non Members"})
        Me.chTransaction.Name = "chTransaction"
        Me.chTransaction.ReadOnly = True
        Me.chTransaction.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chTransaction.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.chTransaction.Width = 250
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.dtpStartEffectDate)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.txtCTENo)
        Me.GroupBox3.Controls.Add(Me.chkwCTE)
        Me.GroupBox3.Controls.Add(Me.dtpEndEffectDate)
        Me.GroupBox3.Controls.Add(Me.dtpDateIssue)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Location = New System.Drawing.Point(380, 164)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(407, 175)
        Me.GroupBox3.TabIndex = 13
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Certificate of Tax Exemption"
        '
        'dtpStartEffectDate
        '
        Me.dtpStartEffectDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStartEffectDate.Location = New System.Drawing.Point(231, 94)
        Me.dtpStartEffectDate.Name = "dtpStartEffectDate"
        Me.dtpStartEffectDate.Size = New System.Drawing.Size(149, 22)
        Me.dtpStartEffectDate.TabIndex = 15
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(14, 46)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(65, 16)
        Me.Label15.TabIndex = 14
        Me.Label15.Text = "CTE No. :"
        '
        'txtCTENo
        '
        Me.txtCTENo.Location = New System.Drawing.Point(231, 44)
        Me.txtCTENo.Name = "txtCTENo"
        Me.txtCTENo.Size = New System.Drawing.Size(149, 22)
        Me.txtCTENo.TabIndex = 13
        '
        'chkwCTE
        '
        Me.chkwCTE.AutoSize = True
        Me.chkwCTE.Location = New System.Drawing.Point(17, 21)
        Me.chkwCTE.Name = "chkwCTE"
        Me.chkwCTE.Size = New System.Drawing.Size(221, 20)
        Me.chkwCTE.TabIndex = 12
        Me.chkwCTE.Text = "With Certificate of Tax Exemption"
        Me.chkwCTE.UseVisualStyleBackColor = True
        '
        'dtpEndEffectDate
        '
        Me.dtpEndEffectDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEndEffectDate.Location = New System.Drawing.Point(231, 132)
        Me.dtpEndEffectDate.Name = "dtpEndEffectDate"
        Me.dtpEndEffectDate.Size = New System.Drawing.Size(149, 22)
        Me.dtpEndEffectDate.TabIndex = 11
        '
        'dtpDateIssue
        '
        Me.dtpDateIssue.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateIssue.Location = New System.Drawing.Point(231, 69)
        Me.dtpDateIssue.Name = "dtpDateIssue"
        Me.dtpDateIssue.Size = New System.Drawing.Size(149, 22)
        Me.dtpDateIssue.TabIndex = 10
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(14, 132)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(191, 32)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "End of Effectivity Date of Tax " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Exemption (Latest CTE issued)"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(14, 94)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(186, 32)
        Me.Label13.TabIndex = 7
        Me.Label13.Text = "Start of Effectivity Date of Tax " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Exemption (First Issued CTE) :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(14, 72)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(86, 16)
        Me.Label14.TabIndex = 5
        Me.Label14.Text = "Date Issued :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(14, 81)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 16)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "TIN No. :"
        '
        'txtTinNo
        '
        Me.txtTinNo.Enabled = False
        Me.txtTinNo.Location = New System.Drawing.Point(116, 79)
        Me.txtTinNo.Name = "txtTinNo"
        Me.txtTinNo.Size = New System.Drawing.Size(243, 22)
        Me.txtTinNo.TabIndex = 11
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dtpLastDateAmendment)
        Me.GroupBox2.Controls.Add(Me.dtpRegDate)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtLatestAmendmentNo)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtCOR_No)
        Me.GroupBox2.Location = New System.Drawing.Point(380, 21)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(407, 137)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "CDA Certificate of Registration"
        '
        'dtpLastDateAmendment
        '
        Me.dtpLastDateAmendment.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpLastDateAmendment.Location = New System.Drawing.Point(163, 101)
        Me.dtpLastDateAmendment.Name = "dtpLastDateAmendment"
        Me.dtpLastDateAmendment.Size = New System.Drawing.Size(217, 22)
        Me.dtpLastDateAmendment.TabIndex = 11
        '
        'dtpRegDate
        '
        Me.dtpRegDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpRegDate.Location = New System.Drawing.Point(163, 49)
        Me.dtpRegDate.Name = "dtpRegDate"
        Me.dtpRegDate.Size = New System.Drawing.Size(217, 22)
        Me.dtpRegDate.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(14, 99)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(93, 32)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Latest Date of " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Amendment :" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(14, 77)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(149, 16)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Latest Amendment No. :"
        '
        'txtLatestAmendmentNo
        '
        Me.txtLatestAmendmentNo.Location = New System.Drawing.Point(163, 75)
        Me.txtLatestAmendmentNo.Name = "txtLatestAmendmentNo"
        Me.txtLatestAmendmentNo.Size = New System.Drawing.Size(217, 22)
        Me.txtLatestAmendmentNo.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(14, 52)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(118, 16)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Registration Date :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 27)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 16)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "COR No. :"
        '
        'txtCOR_No
        '
        Me.txtCOR_No.Location = New System.Drawing.Point(163, 25)
        Me.txtCOR_No.Name = "txtCOR_No"
        Me.txtCOR_No.Size = New System.Drawing.Size(217, 22)
        Me.txtCOR_No.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 157)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 48)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Branch " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Registered " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Address :"
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(116, 155)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(243, 94)
        Me.txtAddress.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 131)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 16)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Province :"
        '
        'txtProvince
        '
        Me.txtProvince.Location = New System.Drawing.Point(116, 129)
        Me.txtProvince.Name = "txtProvince"
        Me.txtProvince.Size = New System.Drawing.Size(243, 22)
        Me.txtProvince.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 106)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 16)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Region :"
        '
        'txtRegion
        '
        Me.txtRegion.Location = New System.Drawing.Point(116, 104)
        Me.txtRegion.Name = "txtRegion"
        Me.txtRegion.Size = New System.Drawing.Size(243, 22)
        Me.txtRegion.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 16)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Branch Code :"
        '
        'txtBranchCode
        '
        Me.txtBranchCode.Location = New System.Drawing.Point(116, 54)
        Me.txtBranchCode.Name = "txtBranchCode"
        Me.txtBranchCode.Size = New System.Drawing.Size(243, 22)
        Me.txtBranchCode.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Branch Name :"
        '
        'txtBranchName
        '
        Me.txtBranchName.Location = New System.Drawing.Point(116, 29)
        Me.txtBranchName.Name = "txtBranchName"
        Me.txtBranchName.Size = New System.Drawing.Size(243, 22)
        Me.txtBranchName.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbNew, Me.tsbEdit, Me.tsbSave, Me.tsbDelete, Me.ToolStripSeparator1, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1067, 40)
        Me.ToolStrip1.TabIndex = 1186
        Me.ToolStrip1.Text = "ToolStrip1"
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
        Me.tsbDelete.Size = New System.Drawing.Size(70, 35)
        Me.tsbDelete.Text = "Deactivate"
        Me.tsbDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        Me.tsbExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmBranch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1067, 594)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvBranch)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmBranch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Branch Settings"
        CType(Me.dgvBranch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvBusinessType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents dgvBranch As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents dtpLastDateAmendment As DateTimePicker
    Friend WithEvents dtpRegDate As DateTimePicker
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents txtLatestAmendmentNo As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents txtCOR_No As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtProvince As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtRegion As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtBranchCode As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtBranchName As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtTinNo As TextBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents dtpStartEffectDate As DateTimePicker
    Friend WithEvents Label15 As Label
    Friend WithEvents txtCTENo As TextBox
    Friend WithEvents chkwCTE As CheckBox
    Friend WithEvents dtpEndEffectDate As DateTimePicker
    Friend WithEvents dtpDateIssue As DateTimePicker
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents dgvBusinessType As DataGridView
    Friend WithEvents chActivity As DataGridViewTextBoxColumn
    Friend WithEvents chTransaction As DataGridViewComboBoxColumn
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents tsbNew As ToolStripButton
    Friend WithEvents tsbEdit As ToolStripButton
    Friend WithEvents tsbSave As ToolStripButton
    Friend WithEvents tsbDelete As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents tsbClose As ToolStripButton
    Friend WithEvents tsbExit As ToolStripButton
    Friend WithEvents dgcBranch As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcBranchCode As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
