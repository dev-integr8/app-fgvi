<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBooking
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBooking))
        Me.txtTransNum = New System.Windows.Forms.TextBox()
        Me.dtpDocDate = New System.Windows.Forms.DateTimePicker()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.gbPayee = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtClientName = New System.Windows.Forms.TextBox()
        Me.txtClientCode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbLocation = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
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
        Me.tsbCancel = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbOption = New System.Windows.Forms.ToolStripDropDownButton()
        Me.TruckingRatesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrint = New System.Windows.Forms.ToolStripButton()
        Me.tsbReports = New System.Windows.Forms.ToolStripDropDownButton()
        Me.CVListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnbilledBookingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnpaidBookingsToSupplierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TripSummaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrevious = New System.Windows.Forms.ToolStripButton()
        Me.tsbNext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.bgwUpload = New System.ComponentModel.BackgroundWorker()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.dgcDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcRefNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcCustomer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDestination = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcVehicleType = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.dgcPlate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcAdditional = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcVATAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcVAT = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgcVATinc = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtVATAmount = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtTotalAmount = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tpBooking = New System.Windows.Forms.TabPage()
        Me.tpEntries = New System.Windows.Forms.TabPage()
        Me.txtTotalCredit = New System.Windows.Forms.TextBox()
        Me.txtTotalDebit = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dgvEntry = New System.Windows.Forms.DataGridView()
        Me.dgcAccntCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcAccntTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDebit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcCredit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chVATType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chATCCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chCompute = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.dgcVCECode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcVCEName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcParticulars = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcRef = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcCC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chCost_Center = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chProfit_Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chProfit_Center = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcCIP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chCIP_Description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chRecordID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gbPayee.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.tpBooking.SuspendLayout()
        Me.tpEntries.SuspendLayout()
        CType(Me.dgvEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTransNum
        '
        Me.txtTransNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTransNum.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransNum.Location = New System.Drawing.Point(911, 13)
        Me.txtTransNum.Name = "txtTransNum"
        Me.txtTransNum.Size = New System.Drawing.Size(132, 22)
        Me.txtTransNum.TabIndex = 16
        '
        'dtpDocDate
        '
        Me.dtpDocDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpDocDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDocDate.Location = New System.Drawing.Point(911, 38)
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.Size = New System.Drawing.Size(132, 22)
        Me.dtpDocDate.TabIndex = 17
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(516, 42)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtRemarks.Size = New System.Drawing.Size(277, 94)
        Me.txtRemarks.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label10.Location = New System.Drawing.Point(513, 22)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 16)
        Me.Label10.TabIndex = 108
        Me.Label10.Text = "Remarks :"
        '
        'gbPayee
        '
        Me.gbPayee.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbPayee.Controls.Add(Me.Button1)
        Me.gbPayee.Controls.Add(Me.Label5)
        Me.gbPayee.Controls.Add(Me.txtClientName)
        Me.gbPayee.Controls.Add(Me.txtClientCode)
        Me.gbPayee.Controls.Add(Me.Label6)
        Me.gbPayee.Controls.Add(Me.Label7)
        Me.gbPayee.Controls.Add(Me.Label1)
        Me.gbPayee.Controls.Add(Me.cbLocation)
        Me.gbPayee.Controls.Add(Me.Label16)
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
        Me.gbPayee.Controls.Add(Me.dtpDocDate)
        Me.gbPayee.Controls.Add(Me.txtTransNum)
        Me.gbPayee.Location = New System.Drawing.Point(8, 40)
        Me.gbPayee.Name = "gbPayee"
        Me.gbPayee.Size = New System.Drawing.Size(1049, 150)
        Me.gbPayee.TabIndex = 47
        Me.gbPayee.TabStop = False
        '
        'Button1
        '
        Me.Button1.BackgroundImage = Global.jade.My.Resources.Resources.view
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Location = New System.Drawing.Point(454, 64)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(25, 25)
        Me.Button1.TabIndex = 1376
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label5.Location = New System.Drawing.Point(38, 69)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 16)
        Me.Label5.TabIndex = 1375
        Me.Label5.Text = "Client Code :"
        '
        'txtClientName
        '
        Me.txtClientName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClientName.Location = New System.Drawing.Point(129, 91)
        Me.txtClientName.Name = "txtClientName"
        Me.txtClientName.Size = New System.Drawing.Size(319, 22)
        Me.txtClientName.TabIndex = 1372
        '
        'txtClientCode
        '
        Me.txtClientCode.Enabled = False
        Me.txtClientCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClientCode.Location = New System.Drawing.Point(129, 66)
        Me.txtClientCode.Name = "txtClientCode"
        Me.txtClientCode.Size = New System.Drawing.Size(319, 22)
        Me.txtClientCode.TabIndex = 1371
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label6.Location = New System.Drawing.Point(34, 94)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 16)
        Me.Label6.TabIndex = 1373
        Me.Label6.Text = "Client Name :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label7.Location = New System.Drawing.Point(109, 69)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(16, 16)
        Me.Label7.TabIndex = 1374
        Me.Label7.Text = "  "
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label1.Location = New System.Drawing.Point(60, 122)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 16)
        Me.Label1.TabIndex = 1370
        Me.Label1.Text = "Location :"
        '
        'cbLocation
        '
        Me.cbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLocation.FormattingEnabled = True
        Me.cbLocation.Location = New System.Drawing.Point(129, 119)
        Me.cbLocation.Name = "cbLocation"
        Me.cbLocation.Size = New System.Drawing.Size(319, 23)
        Me.cbLocation.TabIndex = 1369
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label16.Location = New System.Drawing.Point(24, 17)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(97, 16)
        Me.Label16.TabIndex = 1368
        Me.Label16.Text = "Supplier Code :"
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
        Me.Label2.Location = New System.Drawing.Point(20, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 16)
        Me.Label2.TabIndex = 1360
        Me.Label2.Text = "Supplier Name :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label3.Location = New System.Drawing.Point(109, 17)
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
        Me.txtStatus.Location = New System.Drawing.Point(911, 63)
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
        Me.Label15.Location = New System.Drawing.Point(855, 66)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(54, 16)
        Me.Label15.TabIndex = 1355
        Me.Label15.Text = "Status :"
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label13.Location = New System.Drawing.Point(815, 42)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(94, 16)
        Me.Label13.TabIndex = 1346
        Me.Label13.Text = "Booking Date :"
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label9.Location = New System.Drawing.Point(822, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(87, 16)
        Me.Label9.TabIndex = 1345
        Me.Label9.Text = "Booking No. :"
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
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSearch, Me.tsbNew, Me.tsbEdit, Me.tsbCancel, Me.tsbSave, Me.ToolStripSeparator1, Me.tsbOption, Me.ToolStripSeparator4, Me.tsbPrint, Me.tsbReports, Me.ToolStripSeparator2, Me.tsbPrevious, Me.tsbNext, Me.ToolStripSeparator3, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1063, 40)
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
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 40)
        '
        'tsbOption
        '
        Me.tsbOption.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TruckingRatesToolStripMenuItem})
        Me.tsbOption.ForeColor = System.Drawing.Color.White
        Me.tsbOption.Image = Global.jade.My.Resources.Resources.finance_report_infographic_512
        Me.tsbOption.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbOption.Name = "tsbOption"
        Me.tsbOption.Size = New System.Drawing.Size(57, 37)
        Me.tsbOption.Text = "Option"
        Me.tsbOption.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'TruckingRatesToolStripMenuItem
        '
        Me.TruckingRatesToolStripMenuItem.Name = "TruckingRatesToolStripMenuItem"
        Me.TruckingRatesToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.TruckingRatesToolStripMenuItem.Text = "Trucking Rates"
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
        Me.tsbReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CVListToolStripMenuItem, Me.UnbilledBookingsToolStripMenuItem, Me.UnpaidBookingsToSupplierToolStripMenuItem, Me.TripSummaryToolStripMenuItem})
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
        Me.CVListToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
        Me.CVListToolStripMenuItem.Text = "Booking List"
        '
        'UnbilledBookingsToolStripMenuItem
        '
        Me.UnbilledBookingsToolStripMenuItem.Name = "UnbilledBookingsToolStripMenuItem"
        Me.UnbilledBookingsToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
        Me.UnbilledBookingsToolStripMenuItem.Text = "Unbilled Bookings to Client"
        '
        'UnpaidBookingsToSupplierToolStripMenuItem
        '
        Me.UnpaidBookingsToSupplierToolStripMenuItem.Name = "UnpaidBookingsToSupplierToolStripMenuItem"
        Me.UnpaidBookingsToSupplierToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
        Me.UnpaidBookingsToSupplierToolStripMenuItem.Text = "Unpaid Bookings to Supplier"
        '
        'TripSummaryToolStripMenuItem
        '
        Me.TripSummaryToolStripMenuItem.Name = "TripSummaryToolStripMenuItem"
        Me.TripSummaryToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
        Me.TripSummaryToolStripMenuItem.Text = "Trip Summary"
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
        'bgwUpload
        '
        Me.bgwUpload.WorkerReportsProgress = True
        Me.bgwUpload.WorkerSupportsCancellation = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'dgvList
        '
        Me.dgvList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcDate, Me.dgcRefNo, Me.dgcCustomer, Me.dgcDestination, Me.dgcVehicleType, Me.dgcPlate, Me.dgcRate, Me.dgcAdditional, Me.dgcVATAmount, Me.dgcTotal, Me.dgcVAT, Me.dgcVATinc})
        Me.dgvList.Location = New System.Drawing.Point(3, 6)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.RowHeadersWidth = 25
        Me.dgvList.Size = New System.Drawing.Size(1029, 261)
        Me.dgvList.TabIndex = 1369
        '
        'dgcDate
        '
        Me.dgcDate.HeaderText = "Delivery Date (mm/dd/yyyy)"
        Me.dgcDate.Name = "dgcDate"
        Me.dgcDate.Width = 80
        '
        'dgcRefNo
        '
        Me.dgcRefNo.HeaderText = "Ref. No."
        Me.dgcRefNo.Name = "dgcRefNo"
        '
        'dgcCustomer
        '
        Me.dgcCustomer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.dgcCustomer.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgcCustomer.HeaderText = "Customer"
        Me.dgcCustomer.Name = "dgcCustomer"
        '
        'dgcDestination
        '
        Me.dgcDestination.HeaderText = "Destination"
        Me.dgcDestination.Name = "dgcDestination"
        Me.dgcDestination.Width = 200
        '
        'dgcVehicleType
        '
        Me.dgcVehicleType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.dgcVehicleType.HeaderText = "Vehicle Type"
        Me.dgcVehicleType.Name = "dgcVehicleType"
        Me.dgcVehicleType.ReadOnly = True
        Me.dgcVehicleType.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgcVehicleType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.dgcVehicleType.Width = 150
        '
        'dgcPlate
        '
        Me.dgcPlate.HeaderText = "Plate #"
        Me.dgcPlate.Name = "dgcPlate"
        '
        'dgcRate
        '
        Me.dgcRate.FillWeight = 150.0!
        Me.dgcRate.HeaderText = "Rate"
        Me.dgcRate.Name = "dgcRate"
        '
        'dgcAdditional
        '
        Me.dgcAdditional.HeaderText = "Additional"
        Me.dgcAdditional.Name = "dgcAdditional"
        '
        'dgcVATAmount
        '
        Me.dgcVATAmount.HeaderText = "VAT Amount"
        Me.dgcVATAmount.Name = "dgcVATAmount"
        Me.dgcVATAmount.Visible = False
        '
        'dgcTotal
        '
        Me.dgcTotal.HeaderText = "Total Amount"
        Me.dgcTotal.Name = "dgcTotal"
        Me.dgcTotal.Visible = False
        '
        'dgcVAT
        '
        Me.dgcVAT.HeaderText = "VATable"
        Me.dgcVAT.Name = "dgcVAT"
        Me.dgcVAT.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgcVAT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.dgcVAT.Width = 60
        '
        'dgcVATinc
        '
        Me.dgcVATinc.HeaderText = "VAT Inclusive"
        Me.dgcVATinc.Name = "dgcVATinc"
        Me.dgcVATinc.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgcVATinc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.dgcVATinc.Width = 60
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label8.Location = New System.Drawing.Point(794, 276)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(87, 16)
        Me.Label8.TabIndex = 1373
        Me.Label8.Text = "VAT Amount :"
        '
        'txtVATAmount
        '
        Me.txtVATAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtVATAmount.Location = New System.Drawing.Point(890, 273)
        Me.txtVATAmount.Name = "txtVATAmount"
        Me.txtVATAmount.ReadOnly = True
        Me.txtVATAmount.Size = New System.Drawing.Size(132, 21)
        Me.txtVATAmount.TabIndex = 1372
        Me.txtVATAmount.Text = "0.00"
        Me.txtVATAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label11.Location = New System.Drawing.Point(790, 299)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(91, 16)
        Me.Label11.TabIndex = 1375
        Me.Label11.Text = "Total Amount :"
        '
        'txtTotalAmount
        '
        Me.txtTotalAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTotalAmount.Location = New System.Drawing.Point(890, 296)
        Me.txtTotalAmount.Name = "txtTotalAmount"
        Me.txtTotalAmount.ReadOnly = True
        Me.txtTotalAmount.Size = New System.Drawing.Size(132, 21)
        Me.txtTotalAmount.TabIndex = 1374
        Me.txtTotalAmount.Text = "0.00"
        Me.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tpBooking)
        Me.TabControl1.Controls.Add(Me.tpEntries)
        Me.TabControl1.Location = New System.Drawing.Point(8, 194)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1043, 353)
        Me.TabControl1.TabIndex = 1376
        '
        'tpBooking
        '
        Me.tpBooking.Controls.Add(Me.Label11)
        Me.tpBooking.Controls.Add(Me.dgvList)
        Me.tpBooking.Controls.Add(Me.txtTotalAmount)
        Me.tpBooking.Controls.Add(Me.txtVATAmount)
        Me.tpBooking.Controls.Add(Me.Label8)
        Me.tpBooking.Location = New System.Drawing.Point(4, 24)
        Me.tpBooking.Name = "tpBooking"
        Me.tpBooking.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBooking.Size = New System.Drawing.Size(1035, 325)
        Me.tpBooking.TabIndex = 1
        Me.tpBooking.Text = "Booking"
        Me.tpBooking.UseVisualStyleBackColor = True
        '
        'tpEntries
        '
        Me.tpEntries.Controls.Add(Me.txtTotalCredit)
        Me.tpEntries.Controls.Add(Me.txtTotalDebit)
        Me.tpEntries.Controls.Add(Me.Label4)
        Me.tpEntries.Controls.Add(Me.dgvEntry)
        Me.tpEntries.Location = New System.Drawing.Point(4, 24)
        Me.tpEntries.Name = "tpEntries"
        Me.tpEntries.Padding = New System.Windows.Forms.Padding(3)
        Me.tpEntries.Size = New System.Drawing.Size(1035, 325)
        Me.tpEntries.TabIndex = 0
        Me.tpEntries.Text = "Accounting Entries"
        Me.tpEntries.UseVisualStyleBackColor = True
        '
        'txtTotalCredit
        '
        Me.txtTotalCredit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTotalCredit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCredit.Location = New System.Drawing.Point(468, 300)
        Me.txtTotalCredit.Name = "txtTotalCredit"
        Me.txtTotalCredit.Size = New System.Drawing.Size(100, 22)
        Me.txtTotalCredit.TabIndex = 99
        '
        'txtTotalDebit
        '
        Me.txtTotalDebit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTotalDebit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalDebit.Location = New System.Drawing.Point(364, 300)
        Me.txtTotalDebit.Name = "txtTotalDebit"
        Me.txtTotalDebit.Size = New System.Drawing.Size(100, 22)
        Me.txtTotalDebit.TabIndex = 100
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(319, 303)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 16)
        Me.Label4.TabIndex = 101
        Me.Label4.Text = "Total:"
        '
        'dgvEntry
        '
        Me.dgvEntry.AllowUserToAddRows = False
        Me.dgvEntry.AllowUserToDeleteRows = False
        Me.dgvEntry.AllowUserToOrderColumns = True
        Me.dgvEntry.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEntry.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcAccntCode, Me.dgcAccntTitle, Me.dgcDebit, Me.dgcCredit, Me.chVATType, Me.chATCCode, Me.chCompute, Me.dgcVCECode, Me.dgcVCEName, Me.dgcParticulars, Me.dgcRef, Me.dgcCC, Me.chCost_Center, Me.chProfit_Code, Me.chProfit_Center, Me.dgcCIP, Me.chCIP_Description, Me.chRecordID})
        Me.dgvEntry.Location = New System.Drawing.Point(0, 3)
        Me.dgvEntry.Name = "dgvEntry"
        Me.dgvEntry.ReadOnly = True
        Me.dgvEntry.RowHeadersWidth = 30
        Me.dgvEntry.Size = New System.Drawing.Size(1029, 291)
        Me.dgvEntry.TabIndex = 77
        '
        'dgcAccntCode
        '
        Me.dgcAccntCode.HeaderText = "Account Code"
        Me.dgcAccntCode.Name = "dgcAccntCode"
        Me.dgcAccntCode.ReadOnly = True
        Me.dgcAccntCode.Width = 80
        '
        'dgcAccntTitle
        '
        Me.dgcAccntTitle.HeaderText = "Account Title"
        Me.dgcAccntTitle.Name = "dgcAccntTitle"
        Me.dgcAccntTitle.ReadOnly = True
        Me.dgcAccntTitle.Width = 250
        '
        'dgcDebit
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.dgcDebit.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgcDebit.HeaderText = "Debit"
        Me.dgcDebit.Name = "dgcDebit"
        Me.dgcDebit.ReadOnly = True
        '
        'dgcCredit
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.dgcCredit.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgcCredit.HeaderText = "Credit"
        Me.dgcCredit.Name = "dgcCredit"
        Me.dgcCredit.ReadOnly = True
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
        Me.chCompute.ReadOnly = True
        Me.chCompute.Visible = False
        Me.chCompute.Width = 30
        '
        'dgcVCECode
        '
        Me.dgcVCECode.HeaderText = "VCECode"
        Me.dgcVCECode.Name = "dgcVCECode"
        Me.dgcVCECode.ReadOnly = True
        Me.dgcVCECode.Visible = False
        '
        'dgcVCEName
        '
        Me.dgcVCEName.HeaderText = "VCEName"
        Me.dgcVCEName.Name = "dgcVCEName"
        Me.dgcVCEName.ReadOnly = True
        Me.dgcVCEName.Visible = False
        Me.dgcVCEName.Width = 200
        '
        'dgcParticulars
        '
        Me.dgcParticulars.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.dgcParticulars.HeaderText = "Particulars"
        Me.dgcParticulars.Name = "dgcParticulars"
        Me.dgcParticulars.ReadOnly = True
        '
        'dgcRef
        '
        Me.dgcRef.HeaderText = "Ref. No."
        Me.dgcRef.Name = "dgcRef"
        Me.dgcRef.ReadOnly = True
        '
        'dgcCC
        '
        Me.dgcCC.HeaderText = "Cost ID"
        Me.dgcCC.Name = "dgcCC"
        Me.dgcCC.ReadOnly = True
        Me.dgcCC.Visible = False
        '
        'chCost_Center
        '
        Me.chCost_Center.HeaderText = "Cost Center"
        Me.chCost_Center.Name = "chCost_Center"
        Me.chCost_Center.ReadOnly = True
        Me.chCost_Center.Visible = False
        Me.chCost_Center.Width = 120
        '
        'chProfit_Code
        '
        Me.chProfit_Code.HeaderText = "Profit Code"
        Me.chProfit_Code.Name = "chProfit_Code"
        Me.chProfit_Code.ReadOnly = True
        Me.chProfit_Code.Visible = False
        '
        'chProfit_Center
        '
        Me.chProfit_Center.HeaderText = "Profit Center"
        Me.chProfit_Center.Name = "chProfit_Center"
        Me.chProfit_Center.ReadOnly = True
        Me.chProfit_Center.Visible = False
        Me.chProfit_Center.Width = 120
        '
        'dgcCIP
        '
        Me.dgcCIP.HeaderText = "CIP Code"
        Me.dgcCIP.Name = "dgcCIP"
        Me.dgcCIP.ReadOnly = True
        Me.dgcCIP.Visible = False
        '
        'chCIP_Description
        '
        Me.chCIP_Description.HeaderText = "CIP Description"
        Me.chCIP_Description.Name = "chCIP_Description"
        Me.chCIP_Description.ReadOnly = True
        Me.chCIP_Description.Visible = False
        Me.chCIP_Description.Width = 120
        '
        'chRecordID
        '
        Me.chRecordID.HeaderText = "Record ID"
        Me.chRecordID.Name = "chRecordID"
        Me.chRecordID.ReadOnly = True
        Me.chRecordID.Visible = False
        '
        'frmBooking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(1063, 561)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.gbPayee)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmBooking"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Booking Module"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.gbPayee.ResumeLayout(False)
        Me.gbPayee.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.tpBooking.ResumeLayout(False)
        Me.tpBooking.PerformLayout()
        Me.tpEntries.ResumeLayout(False)
        Me.tpEntries.PerformLayout()
        CType(Me.dgvEntry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtTransNum As System.Windows.Forms.TextBox
    Friend WithEvents dtpDocDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
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
    Friend WithEvents tsbOption As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents bgwUpload As System.ComponentModel.BackgroundWorker
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents CVListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvList As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents cbLocation As ComboBox
    Friend WithEvents TruckingRatesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsbPrint As ToolStripButton
    Friend WithEvents Button1 As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents txtClientName As TextBox
    Friend WithEvents txtClientCode As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents UnbilledBookingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UnpaidBookingsToSupplierToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TripSummaryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label11 As Label
    Friend WithEvents txtTotalAmount As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtVATAmount As TextBox
    Friend WithEvents dgcDate As DataGridViewTextBoxColumn
    Friend WithEvents dgcRefNo As DataGridViewTextBoxColumn
    Friend WithEvents dgcCustomer As DataGridViewTextBoxColumn
    Friend WithEvents dgcDestination As DataGridViewTextBoxColumn
    Friend WithEvents dgcVehicleType As DataGridViewComboBoxColumn
    Friend WithEvents dgcPlate As DataGridViewTextBoxColumn
    Friend WithEvents dgcRate As DataGridViewTextBoxColumn
    Friend WithEvents dgcAdditional As DataGridViewTextBoxColumn
    Friend WithEvents dgcVATAmount As DataGridViewTextBoxColumn
    Friend WithEvents dgcTotal As DataGridViewTextBoxColumn
    Friend WithEvents dgcVAT As DataGridViewCheckBoxColumn
    Friend WithEvents dgcVATinc As DataGridViewCheckBoxColumn
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents tpBooking As TabPage
    Friend WithEvents tpEntries As TabPage
    Friend WithEvents dgvEntry As DataGridView
    Friend WithEvents txtTotalCredit As TextBox
    Friend WithEvents txtTotalDebit As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents dgcAccntCode As DataGridViewTextBoxColumn
    Friend WithEvents dgcAccntTitle As DataGridViewTextBoxColumn
    Friend WithEvents dgcDebit As DataGridViewTextBoxColumn
    Friend WithEvents dgcCredit As DataGridViewTextBoxColumn
    Friend WithEvents chVATType As DataGridViewTextBoxColumn
    Friend WithEvents chATCCode As DataGridViewTextBoxColumn
    Friend WithEvents chCompute As DataGridViewButtonColumn
    Friend WithEvents dgcVCECode As DataGridViewTextBoxColumn
    Friend WithEvents dgcVCEName As DataGridViewTextBoxColumn
    Friend WithEvents dgcParticulars As DataGridViewTextBoxColumn
    Friend WithEvents dgcRef As DataGridViewTextBoxColumn
    Friend WithEvents dgcCC As DataGridViewTextBoxColumn
    Friend WithEvents chCost_Center As DataGridViewTextBoxColumn
    Friend WithEvents chProfit_Code As DataGridViewTextBoxColumn
    Friend WithEvents chProfit_Center As DataGridViewTextBoxColumn
    Friend WithEvents dgcCIP As DataGridViewTextBoxColumn
    Friend WithEvents chCIP_Description As DataGridViewTextBoxColumn
    Friend WithEvents chRecordID As DataGridViewTextBoxColumn
End Class
