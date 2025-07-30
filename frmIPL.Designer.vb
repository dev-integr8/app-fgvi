<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmIPL
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbSearch = New System.Windows.Forms.ToolStripButton()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrevious = New System.Windows.Forms.ToolStripButton()
        Me.tsbNext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbDownload = New System.Windows.Forms.ToolStripButton()
        Me.tsbUpload = New System.Windows.Forms.ToolStripButton()
        Me.tsbBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.tsbLabel = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.p = New System.Windows.Forms.GroupBox()
        Me.dgvFilter = New System.Windows.Forms.DataGridView()
        Me.dgcDBField = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcField = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.dgcCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBarcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcUOM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcUnitPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcNewPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcVATInc = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgcPriceList = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cbUOM = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lbUOM = New System.Windows.Forms.ListBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbMarkupMethod = New System.Windows.Forms.ComboBox()
        Me.txtMarkupValue = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rbManual = New System.Windows.Forms.RadioButton()
        Me.rbMarkup = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbMarkupBasedOn = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dtpDocDate = New System.Windows.Forms.DateTimePicker()
        Me.rbImport = New System.Windows.Forms.RadioButton()
        Me.txtTransNum = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.chkApplyAllUOM = New System.Windows.Forms.CheckBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnUOM = New System.Windows.Forms.Button()
        Me.bgwDownload = New System.ComponentModel.BackgroundWorker()
        Me.lblCounter = New System.Windows.Forms.Label()
        Me.bgwUpload = New System.ComponentModel.BackgroundWorker()
        Me.ToolStrip1.SuspendLayout()
        Me.p.SuspendLayout()
        CType(Me.dgvFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSearch, Me.tsbNew, Me.tsbEdit, Me.tsbSave, Me.tsbCancel, Me.ToolStripSeparator1, Me.tsbPrevious, Me.tsbNext, Me.ToolStripSeparator3, Me.tsbDownload, Me.tsbUpload, Me.tsbBar, Me.tsbLabel, Me.ToolStripSeparator2, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1103, 40)
        Me.ToolStrip1.TabIndex = 1346
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
        'tsbDownload
        '
        Me.tsbDownload.AutoSize = False
        Me.tsbDownload.Enabled = False
        Me.tsbDownload.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.tsbDownload.ForeColor = System.Drawing.Color.White
        Me.tsbDownload.Image = Global.jade.My.Resources.Resources.downloadicon
        Me.tsbDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbDownload.Name = "tsbDownload"
        Me.tsbDownload.Size = New System.Drawing.Size(60, 35)
        Me.tsbDownload.Text = "Download"
        Me.tsbDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbUpload
        '
        Me.tsbUpload.AutoSize = False
        Me.tsbUpload.Enabled = False
        Me.tsbUpload.ForeColor = System.Drawing.Color.White
        Me.tsbUpload.Image = Global.jade.My.Resources.Resources.arrow_upload_icon
        Me.tsbUpload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbUpload.Name = "tsbUpload"
        Me.tsbUpload.Size = New System.Drawing.Size(50, 35)
        Me.tsbUpload.Text = "Upload"
        Me.tsbUpload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbBar
        '
        Me.tsbBar.Margin = New System.Windows.Forms.Padding(1, 6, 1, 6)
        Me.tsbBar.Name = "tsbBar"
        Me.tsbBar.Size = New System.Drawing.Size(200, 28)
        Me.tsbBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.tsbBar.Visible = False
        '
        'tsbLabel
        '
        Me.tsbLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbLabel.ForeColor = System.Drawing.Color.White
        Me.tsbLabel.Name = "tsbLabel"
        Me.tsbLabel.Size = New System.Drawing.Size(144, 37)
        Me.tsbLabel.Text = "Downloadting Template..."
        Me.tsbLabel.Visible = False
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 40)
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
        'p
        '
        Me.p.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.p.Controls.Add(Me.dgvFilter)
        Me.p.Location = New System.Drawing.Point(688, 43)
        Me.p.Name = "p"
        Me.p.Size = New System.Drawing.Size(410, 208)
        Me.p.TabIndex = 1347
        Me.p.TabStop = False
        Me.p.Text = "Filters"
        '
        'dgvFilter
        '
        Me.dgvFilter.AllowUserToAddRows = False
        Me.dgvFilter.AllowUserToDeleteRows = False
        Me.dgvFilter.AllowUserToResizeRows = False
        Me.dgvFilter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvFilter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFilter.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcDBField, Me.dgcField, Me.dgcValue})
        Me.dgvFilter.Location = New System.Drawing.Point(6, 22)
        Me.dgvFilter.Name = "dgvFilter"
        Me.dgvFilter.RowHeadersVisible = False
        Me.dgvFilter.Size = New System.Drawing.Size(397, 180)
        Me.dgvFilter.TabIndex = 1362
        '
        'dgcDBField
        '
        Me.dgcDBField.HeaderText = "DBField"
        Me.dgcDBField.Name = "dgcDBField"
        Me.dgcDBField.Visible = False
        '
        'dgcField
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.dgcField.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgcField.HeaderText = "Field"
        Me.dgcField.Name = "dgcField"
        Me.dgcField.ReadOnly = True
        Me.dgcField.Width = 200
        '
        'dgcValue
        '
        Me.dgcValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.dgcValue.HeaderText = "Value"
        Me.dgcValue.Name = "dgcValue"
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
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcCode, Me.dgcBarcode, Me.dgcName, Me.dgcUOM, Me.dgcUnitPrice, Me.dgcNewPrice, Me.dgcQTY, Me.dgcVATInc, Me.dgcPriceList})
        Me.dgvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvList.Location = New System.Drawing.Point(6, 77)
        Me.dgvList.MultiSelect = False
        Me.dgvList.Name = "dgvList"
        Me.dgvList.RowHeadersVisible = False
        Me.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvList.Size = New System.Drawing.Size(676, 491)
        Me.dgvList.TabIndex = 1348
        '
        'dgcCode
        '
        Me.dgcCode.HeaderText = "Item Code"
        Me.dgcCode.Name = "dgcCode"
        Me.dgcCode.ReadOnly = True
        Me.dgcCode.Width = 90
        '
        'dgcBarcode
        '
        Me.dgcBarcode.HeaderText = "Barcode"
        Me.dgcBarcode.Name = "dgcBarcode"
        Me.dgcBarcode.ReadOnly = True
        '
        'dgcName
        '
        Me.dgcName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.dgcName.HeaderText = "Name"
        Me.dgcName.Name = "dgcName"
        Me.dgcName.ReadOnly = True
        '
        'dgcUOM
        '
        Me.dgcUOM.HeaderText = "UOM"
        Me.dgcUOM.Name = "dgcUOM"
        Me.dgcUOM.ReadOnly = True
        Me.dgcUOM.Width = 60
        '
        'dgcUnitPrice
        '
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = "0"
        Me.dgcUnitPrice.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgcUnitPrice.HeaderText = "Unit Price"
        Me.dgcUnitPrice.Name = "dgcUnitPrice"
        Me.dgcUnitPrice.ReadOnly = True
        '
        'dgcNewPrice
        '
        Me.dgcNewPrice.HeaderText = "New Price"
        Me.dgcNewPrice.Name = "dgcNewPrice"
        Me.dgcNewPrice.ReadOnly = True
        Me.dgcNewPrice.Visible = False
        '
        'dgcQTY
        '
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = "1"
        Me.dgcQTY.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgcQTY.HeaderText = "QTY"
        Me.dgcQTY.Name = "dgcQTY"
        Me.dgcQTY.ReadOnly = True
        Me.dgcQTY.Width = 60
        '
        'dgcVATInc
        '
        Me.dgcVATInc.HeaderText = " VAT Inclusive"
        Me.dgcVATInc.Name = "dgcVATInc"
        Me.dgcVATInc.ReadOnly = True
        Me.dgcVATInc.Width = 70
        '
        'dgcPriceList
        '
        Me.dgcPriceList.HeaderText = "Price List"
        Me.dgcPriceList.Name = "dgcPriceList"
        Me.dgcPriceList.ReadOnly = True
        '
        'cbUOM
        '
        Me.cbUOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbUOM.FormattingEnabled = True
        Me.cbUOM.Items.AddRange(New Object() {"Fixed Amount", "Variable Amount", "Percentage"})
        Me.cbUOM.Location = New System.Drawing.Point(228, 126)
        Me.cbUOM.Name = "cbUOM"
        Me.cbUOM.Size = New System.Drawing.Size(127, 24)
        Me.cbUOM.TabIndex = 1349
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(178, 131)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 17)
        Me.Label6.TabIndex = 1348
        Me.Label6.Text = "UOM :"
        '
        'lbUOM
        '
        Me.lbUOM.FormattingEnabled = True
        Me.lbUOM.ItemHeight = 16
        Me.lbUOM.Location = New System.Drawing.Point(228, 153)
        Me.lbUOM.Name = "lbUOM"
        Me.lbUOM.Size = New System.Drawing.Size(127, 52)
        Me.lbUOM.TabIndex = 1347
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(14, 22)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(134, 17)
        Me.Label5.TabIndex = 1357
        Me.Label5.Text = "Pricing Description :"
        '
        'cbMarkupMethod
        '
        Me.cbMarkupMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMarkupMethod.FormattingEnabled = True
        Me.cbMarkupMethod.Items.AddRange(New Object() {"Percentage", "Fixed Amount"})
        Me.cbMarkupMethod.Location = New System.Drawing.Point(115, 17)
        Me.cbMarkupMethod.Name = "cbMarkupMethod"
        Me.cbMarkupMethod.Size = New System.Drawing.Size(237, 24)
        Me.cbMarkupMethod.TabIndex = 1356
        '
        'txtMarkupValue
        '
        Me.txtMarkupValue.Location = New System.Drawing.Point(115, 44)
        Me.txtMarkupValue.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMarkupValue.Name = "txtMarkupValue"
        Me.txtMarkupValue.Size = New System.Drawing.Size(237, 23)
        Me.txtMarkupValue.TabIndex = 1355
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(6, 48)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 17)
        Me.Label4.TabIndex = 1354
        Me.Label4.Text = "Mark up Value :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(50, 23)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 17)
        Me.Label1.TabIndex = 1353
        Me.Label1.Text = "Method :"
        '
        'rbManual
        '
        Me.rbManual.AutoSize = True
        Me.rbManual.Location = New System.Drawing.Point(17, 92)
        Me.rbManual.Name = "rbManual"
        Me.rbManual.Size = New System.Drawing.Size(119, 21)
        Me.rbManual.TabIndex = 1359
        Me.rbManual.Text = "Manual Pricing"
        Me.rbManual.UseVisualStyleBackColor = True
        '
        'rbMarkup
        '
        Me.rbMarkup.AutoSize = True
        Me.rbMarkup.Checked = True
        Me.rbMarkup.Location = New System.Drawing.Point(17, 113)
        Me.rbMarkup.Name = "rbMarkup"
        Me.rbMarkup.Size = New System.Drawing.Size(77, 21)
        Me.rbMarkup.TabIndex = 1360
        Me.rbMarkup.TabStop = True
        Me.rbMarkup.Text = "Mark up"
        Me.rbMarkup.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cbMarkupBasedOn)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtMarkupValue)
        Me.GroupBox1.Controls.Add(Me.cbMarkupMethod)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 205)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(359, 100)
        Me.GroupBox1.TabIndex = 1361
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Mark up"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(37, 73)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 17)
        Me.Label2.TabIndex = 1357
        Me.Label2.Text = "Based on :"
        '
        'cbMarkupBasedOn
        '
        Me.cbMarkupBasedOn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMarkupBasedOn.FormattingEnabled = True
        Me.cbMarkupBasedOn.Items.AddRange(New Object() {"Standard Cost", "Purchase Price"})
        Me.cbMarkupBasedOn.Location = New System.Drawing.Point(115, 70)
        Me.cbMarkupBasedOn.Name = "cbMarkupBasedOn"
        Me.cbMarkupBasedOn.Size = New System.Drawing.Size(237, 24)
        Me.cbMarkupBasedOn.TabIndex = 1358
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.txtStatus)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.dtpDocDate)
        Me.GroupBox2.Controls.Add(Me.rbImport)
        Me.GroupBox2.Controls.Add(Me.txtTransNum)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtDescription)
        Me.GroupBox2.Controls.Add(Me.chkApplyAllUOM)
        Me.GroupBox2.Controls.Add(Me.cbUOM)
        Me.GroupBox2.Controls.Add(Me.btnAdd)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.GroupBox1)
        Me.GroupBox2.Controls.Add(Me.btnDelete)
        Me.GroupBox2.Controls.Add(Me.rbMarkup)
        Me.GroupBox2.Controls.Add(Me.rbManual)
        Me.GroupBox2.Controls.Add(Me.btnUOM)
        Me.GroupBox2.Controls.Add(Me.lbUOM)
        Me.GroupBox2.Location = New System.Drawing.Point(694, 257)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(397, 311)
        Me.GroupBox2.TabIndex = 1363
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Price Setup"
        '
        'txtStatus
        '
        Me.txtStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtStatus.Enabled = False
        Me.txtStatus.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(281, 66)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(110, 22)
        Me.txtStatus.TabIndex = 1369
        Me.txtStatus.Text = "Open"
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(226, 69)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(54, 16)
        Me.Label13.TabIndex = 1368
        Me.Label13.Text = "Status :"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(178, 45)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(102, 16)
        Me.Label8.TabIndex = 1366
        Me.Label8.Text = "Effectivity Date :"
        '
        'dtpDocDate
        '
        Me.dtpDocDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpDocDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDocDate.Location = New System.Drawing.Point(281, 42)
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.Size = New System.Drawing.Size(110, 22)
        Me.dtpDocDate.TabIndex = 1367
        '
        'rbImport
        '
        Me.rbImport.AutoSize = True
        Me.rbImport.Location = New System.Drawing.Point(17, 134)
        Me.rbImport.Name = "rbImport"
        Me.rbImport.Size = New System.Drawing.Size(91, 21)
        Me.rbImport.TabIndex = 1365
        Me.rbImport.Text = "Import File"
        Me.rbImport.UseVisualStyleBackColor = True
        '
        'txtTransNum
        '
        Me.txtTransNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTransNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransNum.Location = New System.Drawing.Point(281, 18)
        Me.txtTransNum.Name = "txtTransNum"
        Me.txtTransNum.Size = New System.Drawing.Size(110, 22)
        Me.txtTransNum.TabIndex = 1350
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label7.Location = New System.Drawing.Point(191, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(89, 16)
        Me.Label7.TabIndex = 1349
        Me.Label7.Text = "Price List No :"
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(17, 44)
        Me.txtDescription.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(152, 41)
        Me.txtDescription.TabIndex = 1363
        '
        'chkApplyAllUOM
        '
        Me.chkApplyAllUOM.AutoSize = True
        Me.chkApplyAllUOM.Location = New System.Drawing.Point(228, 105)
        Me.chkApplyAllUOM.Name = "chkApplyAllUOM"
        Me.chkApplyAllUOM.Size = New System.Drawing.Size(133, 21)
        Me.chkApplyAllUOM.TabIndex = 1362
        Me.chkApplyAllUOM.Text = "Apply to All UOM"
        Me.chkApplyAllUOM.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.BackgroundImage = Global.jade.My.Resources.Resources.add_icon
        Me.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAdd.Location = New System.Drawing.Point(357, 152)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(25, 25)
        Me.btnAdd.TabIndex = 1351
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.BackgroundImage = Global.jade.My.Resources.Resources.delete1
        Me.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDelete.Location = New System.Drawing.Point(357, 178)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(25, 25)
        Me.btnDelete.TabIndex = 1352
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnUOM
        '
        Me.btnUOM.BackgroundImage = Global.jade.My.Resources.Resources.report_icon
        Me.btnUOM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnUOM.Location = New System.Drawing.Point(357, 126)
        Me.btnUOM.Name = "btnUOM"
        Me.btnUOM.Size = New System.Drawing.Size(25, 25)
        Me.btnUOM.TabIndex = 1350
        Me.btnUOM.UseVisualStyleBackColor = True
        '
        'bgwDownload
        '
        Me.bgwDownload.WorkerReportsProgress = True
        '
        'lblCounter
        '
        Me.lblCounter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCounter.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.lblCounter.Location = New System.Drawing.Point(465, 51)
        Me.lblCounter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCounter.Name = "lblCounter"
        Me.lblCounter.Size = New System.Drawing.Size(216, 21)
        Me.lblCounter.TabIndex = 1364
        Me.lblCounter.Text = "Record Count : "
        Me.lblCounter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'bgwUpload
        '
        Me.bgwUpload.WorkerReportsProgress = True
        '
        'frmIPL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1103, 580)
        Me.Controls.Add(Me.lblCounter)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.p)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmIPL"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Item Pricelist"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.p.ResumeLayout(False)
        CType(Me.dgvFilter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents p As System.Windows.Forms.GroupBox
    Friend WithEvents dgvList As DataGridView
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnUOM As Button
    Friend WithEvents cbUOM As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents lbUOM As ListBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cbMarkupMethod As ComboBox
    Friend WithEvents txtMarkupValue As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents rbMarkup As RadioButton
    Friend WithEvents rbManual As RadioButton
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents chkApplyAllUOM As CheckBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents dgvFilter As DataGridView
    Friend WithEvents txtTransNum As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents rbImport As RadioButton
    Friend WithEvents tsbSearch As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents tsbPrevious As ToolStripButton
    Friend WithEvents tsbNext As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents tsbExit As ToolStripButton
    Friend WithEvents Label8 As Label
    Friend WithEvents dtpDocDate As DateTimePicker
    Friend WithEvents dgcDBField As DataGridViewTextBoxColumn
    Friend WithEvents dgcField As DataGridViewTextBoxColumn
    Friend WithEvents dgcValue As DataGridViewTextBoxColumn
    Friend WithEvents Label2 As Label
    Friend WithEvents cbMarkupBasedOn As ComboBox
    Friend WithEvents tsbDownload As ToolStripButton
    Friend WithEvents tsbUpload As ToolStripButton
    Friend WithEvents tsbEdit As ToolStripButton
    Friend WithEvents tsbCancel As ToolStripButton
    Friend WithEvents txtStatus As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents tsbBar As ToolStripProgressBar
    Friend WithEvents bgwDownload As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblCounter As Label
    Friend WithEvents tsbLabel As ToolStripLabel
    Friend WithEvents bgwUpload As System.ComponentModel.BackgroundWorker
    Friend WithEvents dgcCode As DataGridViewTextBoxColumn
    Friend WithEvents dgcBarcode As DataGridViewTextBoxColumn
    Friend WithEvents dgcName As DataGridViewTextBoxColumn
    Friend WithEvents dgcUOM As DataGridViewTextBoxColumn
    Friend WithEvents dgcUnitPrice As DataGridViewTextBoxColumn
    Friend WithEvents dgcNewPrice As DataGridViewTextBoxColumn
    Friend WithEvents dgcQTY As DataGridViewTextBoxColumn
    Friend WithEvents dgcVATInc As DataGridViewCheckBoxColumn
    Friend WithEvents dgcPriceList As DataGridViewTextBoxColumn
End Class
