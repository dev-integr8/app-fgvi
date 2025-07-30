<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPCV
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPCV))
        Me.txtTransNum = New System.Windows.Forms.TextBox()
        Me.dtpDocDate = New System.Windows.Forms.DateTimePicker()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cbPaymentType = New System.Windows.Forms.ComboBox()
        Me.cbDisburseType = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.gbPayee = New System.Windows.Forms.GroupBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnUOMGroup = New System.Windows.Forms.Button()
        Me.btnSearchVCE = New System.Windows.Forms.Button()
        Me.txtVCEName = New System.Windows.Forms.TextBox()
        Me.txtVCECode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditEntriesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbSearch = New System.Windows.Forms.ToolStripButton()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbOption = New System.Windows.Forms.ToolStripDropDownButton()
        Me.CancelCheckToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmUnreleased = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsbCopy = New System.Windows.Forms.ToolStripDropDownButton()
        Me.tsbCopyAPV = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsbCopyADV = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsbPrint = New System.Windows.Forms.ToolStripSplitButton()
        Me.PrintPCVToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsbReports = New System.Windows.Forms.ToolStripDropDownButton()
        Me.PCVListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrevious = New System.Windows.Forms.ToolStripButton()
        Me.tsbNext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.dgvRecords = New System.Windows.Forms.DataGridView()
        Me.txtInputVAT = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtBaseAmount = New System.Windows.Forms.TextBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dgcBranch = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcVCECode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcPayee = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcOR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcTIN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcVAT = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgcType = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.dgcCategory = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcParticulars = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcInputVAT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBase = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chCostID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chCostCenter = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.chCIP_Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chCIP_Desc = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.chProfit_Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chProfit_Desc = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.gbPayee.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTransNum
        '
        Me.txtTransNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTransNum.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransNum.Location = New System.Drawing.Point(939, 13)
        Me.txtTransNum.Name = "txtTransNum"
        Me.txtTransNum.Size = New System.Drawing.Size(132, 22)
        Me.txtTransNum.TabIndex = 16
        '
        'dtpDocDate
        '
        Me.dtpDocDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpDocDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDocDate.Location = New System.Drawing.Point(939, 38)
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.Size = New System.Drawing.Size(132, 22)
        Me.dtpDocDate.TabIndex = 17
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(561, 17)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(285, 91)
        Me.txtRemarks.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(487, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 16)
        Me.Label10.TabIndex = 108
        Me.Label10.Text = "Remarks :"
        '
        'cbPaymentType
        '
        Me.cbPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPaymentType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPaymentType.FormattingEnabled = True
        Me.cbPaymentType.Items.AddRange(New Object() {"Check", "Cash", "Managers Check", "Debit Memo", "Revolving Fund"})
        Me.cbPaymentType.Location = New System.Drawing.Point(110, 68)
        Me.cbPaymentType.Name = "cbPaymentType"
        Me.cbPaymentType.Size = New System.Drawing.Size(186, 24)
        Me.cbPaymentType.TabIndex = 4
        Me.cbPaymentType.Visible = False
        '
        'cbDisburseType
        '
        Me.cbDisburseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDisburseType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbDisburseType.FormattingEnabled = True
        Me.cbDisburseType.Location = New System.Drawing.Point(114, 89)
        Me.cbDisburseType.Name = "cbDisburseType"
        Me.cbDisburseType.Size = New System.Drawing.Size(186, 24)
        Me.cbDisburseType.TabIndex = 6
        Me.cbDisburseType.Visible = False
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(14, 92)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(98, 16)
        Me.Label22.TabIndex = 140
        Me.Label22.Text = "Expense Type :"
        Me.Label22.Visible = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(9, 72)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(99, 16)
        Me.Label25.TabIndex = 1340
        Me.Label25.Text = "Payment Type :"
        Me.Label25.Visible = False
        '
        'gbPayee
        '
        Me.gbPayee.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbPayee.Controls.Add(Me.Label16)
        Me.gbPayee.Controls.Add(Me.btnUOMGroup)
        Me.gbPayee.Controls.Add(Me.btnSearchVCE)
        Me.gbPayee.Controls.Add(Me.txtVCEName)
        Me.gbPayee.Controls.Add(Me.txtVCECode)
        Me.gbPayee.Controls.Add(Me.Label2)
        Me.gbPayee.Controls.Add(Me.Label3)
        Me.gbPayee.Controls.Add(Me.txtStatus)
        Me.gbPayee.Controls.Add(Me.Label15)
        Me.gbPayee.Controls.Add(Me.Label13)
        Me.gbPayee.Controls.Add(Me.Label9)
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
        Me.gbPayee.Size = New System.Drawing.Size(1077, 119)
        Me.gbPayee.TabIndex = 47
        Me.gbPayee.TabStop = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label16.Location = New System.Drawing.Point(31, 17)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(77, 16)
        Me.Label16.TabIndex = 1368
        Me.Label16.Text = "VCE Code :"
        '
        'btnUOMGroup
        '
        Me.btnUOMGroup.BackgroundImage = Global.jade.My.Resources.Resources._New
        Me.btnUOMGroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnUOMGroup.Location = New System.Drawing.Point(306, 89)
        Me.btnUOMGroup.Name = "btnUOMGroup"
        Me.btnUOMGroup.Size = New System.Drawing.Size(25, 25)
        Me.btnUOMGroup.TabIndex = 7
        Me.btnUOMGroup.UseVisualStyleBackColor = True
        Me.btnUOMGroup.Visible = False
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
        Me.txtVCEName.Location = New System.Drawing.Point(110, 41)
        Me.txtVCEName.Name = "txtVCEName"
        Me.txtVCEName.Size = New System.Drawing.Size(338, 22)
        Me.txtVCEName.TabIndex = 3
        '
        'txtVCECode
        '
        Me.txtVCECode.Enabled = False
        Me.txtVCECode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVCECode.Location = New System.Drawing.Point(110, 14)
        Me.txtVCECode.Name = "txtVCECode"
        Me.txtVCECode.Size = New System.Drawing.Size(338, 22)
        Me.txtVCECode.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(27, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 16)
        Me.Label2.TabIndex = 1360
        Me.Label2.Text = "VCE Name :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.txtStatus.Location = New System.Drawing.Point(939, 63)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(132, 22)
        Me.txtStatus.TabIndex = 18
        Me.txtStatus.Text = "Open"
        '
        'Label15
        '
        Me.Label15.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(879, 66)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(54, 16)
        Me.Label15.TabIndex = 1355
        Me.Label15.Text = "Status :"
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(859, 43)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(74, 16)
        Me.Label13.TabIndex = 1346
        Me.Label13.Text = "PCV Date :"
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(866, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(67, 16)
        Me.Label9.TabIndex = 1345
        Me.Label9.Text = "PCV No. :"
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
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSearch, Me.tsbNew, Me.tsbEdit, Me.tsbSave, Me.tsbCancel, Me.ToolStripSeparator1, Me.tsbOption, Me.tsbCopy, Me.ToolStripSeparator4, Me.ToolStripDropDownButton1, Me.tsbPrint, Me.tsbReports, Me.ToolStripSeparator2, Me.tsbPrevious, Me.tsbNext, Me.ToolStripSeparator3, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1091, 40)
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
        'tsbOption
        '
        Me.tsbOption.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CancelCheckToolStripMenuItem, Me.tsmUnreleased})
        Me.tsbOption.ForeColor = System.Drawing.Color.White
        Me.tsbOption.Image = Global.jade.My.Resources.Resources.finance_report_infographic_512
        Me.tsbOption.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbOption.Name = "tsbOption"
        Me.tsbOption.Size = New System.Drawing.Size(57, 37)
        Me.tsbOption.Text = "Option"
        Me.tsbOption.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbOption.Visible = False
        '
        'CancelCheckToolStripMenuItem
        '
        Me.CancelCheckToolStripMenuItem.Name = "CancelCheckToolStripMenuItem"
        Me.CancelCheckToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.CancelCheckToolStripMenuItem.Text = "Cancel Cheque"
        '
        'tsmUnreleased
        '
        Me.tsmUnreleased.Enabled = False
        Me.tsmUnreleased.Name = "tsmUnreleased"
        Me.tsmUnreleased.Size = New System.Drawing.Size(157, 22)
        Me.tsmUnreleased.Text = "Release Cheque"
        '
        'tsbCopy
        '
        Me.tsbCopy.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbCopyAPV, Me.tsbCopyADV})
        Me.tsbCopy.ForeColor = System.Drawing.Color.White
        Me.tsbCopy.Image = Global.jade.My.Resources.Resources.finance_report_infographic_512
        Me.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbCopy.Name = "tsbCopy"
        Me.tsbCopy.Size = New System.Drawing.Size(48, 37)
        Me.tsbCopy.Text = "Copy"
        Me.tsbCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbCopy.Visible = False
        '
        'tsbCopyAPV
        '
        Me.tsbCopyAPV.Name = "tsbCopyAPV"
        Me.tsbCopyAPV.Size = New System.Drawing.Size(156, 22)
        Me.tsbCopyAPV.Text = "From APV"
        '
        'tsbCopyADV
        '
        Me.tsbCopyADV.Name = "tsbCopyADV"
        Me.tsbCopyADV.Size = New System.Drawing.Size(156, 22)
        Me.tsbCopyADV.Text = "From Advances"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 40)
        Me.ToolStripSeparator4.Visible = False
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.ToolStripDropDownButton1.ForeColor = System.Drawing.Color.White
        Me.ToolStripDropDownButton1.Image = Global.jade.My.Resources.Resources.finance_report_infographic_512
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(62, 37)
        Me.ToolStripDropDownButton1.Text = "Settings"
        Me.ToolStripDropDownButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(123, 22)
        Me.ToolStripMenuItem1.Text = "PCV Type"
        '
        'tsbPrint
        '
        Me.tsbPrint.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintPCVToolStripMenuItem})
        Me.tsbPrint.ForeColor = System.Drawing.Color.White
        Me.tsbPrint.Image = Global.jade.My.Resources.Resources.printer_circle_blue_512
        Me.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPrint.Name = "tsbPrint"
        Me.tsbPrint.Size = New System.Drawing.Size(48, 37)
        Me.tsbPrint.Text = "Print"
        Me.tsbPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'PrintPCVToolStripMenuItem
        '
        Me.PrintPCVToolStripMenuItem.Name = "PrintPCVToolStripMenuItem"
        Me.PrintPCVToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.PrintPCVToolStripMenuItem.Text = "Petty Cash Voucher"
        '
        'tsbReports
        '
        Me.tsbReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PCVListToolStripMenuItem})
        Me.tsbReports.ForeColor = System.Drawing.Color.White
        Me.tsbReports.Image = Global.jade.My.Resources.Resources.finance_report_infographic_512
        Me.tsbReports.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbReports.Name = "tsbReports"
        Me.tsbReports.Size = New System.Drawing.Size(60, 37)
        Me.tsbReports.Text = "Reports"
        Me.tsbReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'PCVListToolStripMenuItem
        '
        Me.PCVListToolStripMenuItem.Name = "PCVListToolStripMenuItem"
        Me.PCVListToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.PCVListToolStripMenuItem.Text = "PCV List"
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
        'dgvRecords
        '
        Me.dgvRecords.AllowUserToAddRows = False
        Me.dgvRecords.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRecords.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcBranch, Me.dgcDate, Me.dgcVCECode, Me.dgcPayee, Me.dgcOR, Me.dgcTIN, Me.dgcAmount, Me.dgcVAT, Me.dgcType, Me.dgcCategory, Me.dgcParticulars, Me.dgcInputVAT, Me.dgcBase, Me.chCostID, Me.chCostCenter, Me.chCIP_Code, Me.chCIP_Desc, Me.chProfit_Code, Me.chProfit_Desc})
        Me.dgvRecords.Location = New System.Drawing.Point(8, 165)
        Me.dgvRecords.MultiSelect = False
        Me.dgvRecords.Name = "dgvRecords"
        Me.dgvRecords.RowHeadersWidth = 25
        Me.dgvRecords.Size = New System.Drawing.Size(1077, 285)
        Me.dgvRecords.TabIndex = 1345
        '
        'txtInputVAT
        '
        Me.txtInputVAT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInputVAT.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInputVAT.Location = New System.Drawing.Point(909, 495)
        Me.txtInputVAT.Name = "txtInputVAT"
        Me.txtInputVAT.ReadOnly = True
        Me.txtInputVAT.Size = New System.Drawing.Size(170, 22)
        Me.txtInputVAT.TabIndex = 1351
        Me.txtInputVAT.TabStop = False
        Me.txtInputVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(804, 498)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(102, 16)
        Me.Label8.TabIndex = 1352
        Me.Label8.Text = "Total Input VAT :"
        '
        'Label20
        '
        Me.Label20.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(781, 524)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(125, 16)
        Me.Label20.TabIndex = 1353
        Me.Label20.Text = "Total Base Amount :"
        '
        'txtBaseAmount
        '
        Me.txtBaseAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBaseAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBaseAmount.Location = New System.Drawing.Point(909, 521)
        Me.txtBaseAmount.Name = "txtBaseAmount"
        Me.txtBaseAmount.ReadOnly = True
        Me.txtBaseAmount.Size = New System.Drawing.Size(170, 22)
        Me.txtBaseAmount.TabIndex = 1350
        Me.txtBaseAmount.TabStop = False
        Me.txtBaseAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAmount
        '
        Me.txtAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.Location = New System.Drawing.Point(909, 469)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.ReadOnly = True
        Me.txtAmount.Size = New System.Drawing.Size(170, 22)
        Me.txtAmount.TabIndex = 1349
        Me.txtAmount.TabStop = False
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(815, 472)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 16)
        Me.Label5.TabIndex = 1354
        Me.Label5.Text = "Total Amount :"
        '
        'dgcBranch
        '
        Me.dgcBranch.HeaderText = "Branch Code"
        Me.dgcBranch.Name = "dgcBranch"
        Me.dgcBranch.Visible = False
        Me.dgcBranch.Width = 60
        '
        'dgcDate
        '
        DataGridViewCellStyle1.Format = "d"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.dgcDate.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgcDate.HeaderText = "Date (mm/dd/yyyy)"
        Me.dgcDate.Name = "dgcDate"
        Me.dgcDate.Width = 80
        '
        'dgcVCECode
        '
        Me.dgcVCECode.HeaderText = "VCECode"
        Me.dgcVCECode.Name = "dgcVCECode"
        Me.dgcVCECode.Visible = False
        '
        'dgcPayee
        '
        Me.dgcPayee.HeaderText = "Payee"
        Me.dgcPayee.Name = "dgcPayee"
        Me.dgcPayee.Width = 300
        '
        'dgcOR
        '
        Me.dgcOR.HeaderText = "O.R.#"
        Me.dgcOR.Name = "dgcOR"
        '
        'dgcTIN
        '
        Me.dgcTIN.HeaderText = "TIN"
        Me.dgcTIN.Name = "dgcTIN"
        Me.dgcTIN.Width = 120
        '
        'dgcAmount
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = "0"
        Me.dgcAmount.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgcAmount.HeaderText = "Amount"
        Me.dgcAmount.Name = "dgcAmount"
        Me.dgcAmount.Width = 80
        '
        'dgcVAT
        '
        Me.dgcVAT.HeaderText = "VAT"
        Me.dgcVAT.Name = "dgcVAT"
        Me.dgcVAT.Width = 40
        '
        'dgcType
        '
        Me.dgcType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.dgcType.HeaderText = "Type"
        Me.dgcType.Name = "dgcType"
        Me.dgcType.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgcType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.dgcType.Width = 180
        '
        'dgcCategory
        '
        Me.dgcCategory.HeaderText = "Cat."
        Me.dgcCategory.Name = "dgcCategory"
        Me.dgcCategory.Width = 50
        '
        'dgcParticulars
        '
        Me.dgcParticulars.HeaderText = "Particulars"
        Me.dgcParticulars.Name = "dgcParticulars"
        Me.dgcParticulars.Width = 150
        '
        'dgcInputVAT
        '
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = "0"
        Me.dgcInputVAT.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgcInputVAT.HeaderText = "InputVAT"
        Me.dgcInputVAT.Name = "dgcInputVAT"
        Me.dgcInputVAT.ReadOnly = True
        Me.dgcInputVAT.Width = 80
        '
        'dgcBase
        '
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = "0"
        Me.dgcBase.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgcBase.HeaderText = "Base Amount"
        Me.dgcBase.Name = "dgcBase"
        Me.dgcBase.ReadOnly = True
        Me.dgcBase.Width = 80
        '
        'chCostID
        '
        Me.chCostID.HeaderText = "Cost Center ID"
        Me.chCostID.Name = "chCostID"
        Me.chCostID.ReadOnly = True
        Me.chCostID.Visible = False
        Me.chCostID.Width = 60
        '
        'chCostCenter
        '
        Me.chCostCenter.HeaderText = "Cost Center"
        Me.chCostCenter.Name = "chCostCenter"
        Me.chCostCenter.Width = 150
        '
        'chCIP_Code
        '
        Me.chCIP_Code.HeaderText = "CIP Code"
        Me.chCIP_Code.Name = "chCIP_Code"
        Me.chCIP_Code.Visible = False
        '
        'chCIP_Desc
        '
        Me.chCIP_Desc.HeaderText = "CIP Desc"
        Me.chCIP_Desc.Name = "chCIP_Desc"
        Me.chCIP_Desc.Visible = False
        Me.chCIP_Desc.Width = 150
        '
        'chProfit_Code
        '
        Me.chProfit_Code.HeaderText = "Profit Code"
        Me.chProfit_Code.Name = "chProfit_Code"
        Me.chProfit_Code.Visible = False
        '
        'chProfit_Desc
        '
        Me.chProfit_Desc.HeaderText = "Profit Center"
        Me.chProfit_Desc.Name = "chProfit_Desc"
        '
        'frmPCV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(1091, 561)
        Me.Controls.Add(Me.txtInputVAT)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.txtBaseAmount)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.dgvRecords)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.gbPayee)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmPCV"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Petty Cash"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.gbPayee.ResumeLayout(False)
        Me.gbPayee.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvRecords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtTransNum As System.Windows.Forms.TextBox
    Friend WithEvents dtpDocDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cbPaymentType As System.Windows.Forms.ComboBox
    Friend WithEvents cbDisburseType As System.Windows.Forms.ComboBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents gbPayee As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
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
    Friend WithEvents PrintPCVToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbOption As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents tsmUnreleased As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CancelCheckToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnUOMGroup As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents dgvRecords As System.Windows.Forms.DataGridView
    Friend WithEvents txtInputVAT As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtBaseAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents PCVListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgcBranch As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcVCECode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcPayee As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcOR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcTIN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcVAT As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents dgcType As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents dgcCategory As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcParticulars As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcInputVAT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcBase As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chCostID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chCostCenter As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents chCIP_Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chCIP_Desc As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents chProfit_Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chProfit_Desc As System.Windows.Forms.DataGridViewComboBoxColumn
End Class
