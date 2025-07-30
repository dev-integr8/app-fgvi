<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGI
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGI))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbSearch = New System.Windows.Forms.ToolStripButton()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbDownload = New System.Windows.Forms.ToolStripButton()
        Me.tsbUpload = New System.Windows.Forms.ToolStripButton()
        Me.pgbCounter = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbCopy = New System.Windows.Forms.ToolStripDropDownButton()
        Me.tsbCopyPR = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromJOToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromRRToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromInventoryCountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromRRToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrint = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsbReports = New System.Windows.Forms.ToolStripDropDownButton()
        Me.TestToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PRWithoutPOToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrevious = New System.Windows.Forms.ToolStripButton()
        Me.tsbNext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtICRef = New System.Windows.Forms.TextBox()
        Me.txtConversion = New System.Windows.Forms.TextBox()
        Me.lblConversion = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtATDNo = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cbCurrency = New System.Windows.Forms.ComboBox()
        Me.lblCostCenter = New System.Windows.Forms.Label()
        Me.cbCostCenter = New System.Windows.Forms.ComboBox()
        Me.lblJORef = New System.Windows.Forms.Label()
        Me.txtJORef = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtMRRef = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cbIssuerFrom = New System.Windows.Forms.ComboBox()
        Me.cbWHfrom = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cbIssueTo = New System.Windows.Forms.ComboBox()
        Me.txtVCEName = New System.Windows.Forms.TextBox()
        Me.txtVCECode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dtpDocDate = New System.Windows.Forms.DateTimePicker()
        Me.txtTransNum = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.btnSearchVCE = New System.Windows.Forms.Button()
        Me.cbTransType = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbWHto = New System.Windows.Forms.ComboBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.dgvItemMaster = New System.Windows.Forms.DataGridView()
        Me.contextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ViewItemInformationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtGrandTotal = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.txtTotalCredit = New System.Windows.Forms.TextBox()
        Me.txtTotalDebit = New System.Windows.Forms.TextBox()
        Me.lvAccount = New System.Windows.Forms.ListView()
        Me.chAccountCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chAccountTitle = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chDebit = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chCredit = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chRef = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.bgwSave = New System.ComponentModel.BackgroundWorker()
        Me.bgwUpload = New System.ComponentModel.BackgroundWorker()
        Me.chItemCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chBarcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chItemName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chUOM = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.dgcReqQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcStockQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcIssueQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chWHSE = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.dgcIssuedQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chAvgCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcAccntCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chkFree = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.chGrossAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chDiscountRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chDiscountAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chVATAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chNetAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chVAT = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.chVATInc = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.chDateExpired = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chLotNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chSerialNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chSize = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.chColor = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgvItemMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.contextMenuStrip1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSearch, Me.tsbNew, Me.tsbEdit, Me.tsbSave, Me.tsbCancel, Me.ToolStripSeparator1, Me.tsbDownload, Me.tsbUpload, Me.pgbCounter, Me.ToolStripSeparator5, Me.tsbCopy, Me.ToolStripSeparator4, Me.tsbPrint, Me.ToolStripDropDownButton1, Me.tsbReports, Me.ToolStripSeparator2, Me.tsbPrevious, Me.tsbNext, Me.ToolStripSeparator3, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1220, 40)
        Me.ToolStrip1.TabIndex = 1306
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
        'tsbDownload
        '
        Me.tsbDownload.AutoSize = False
        Me.tsbDownload.Font = New System.Drawing.Font("Segoe UI", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbDownload.ForeColor = System.Drawing.Color.White
        Me.tsbDownload.Image = Global.jade.My.Resources.Resources.downloadicon
        Me.tsbDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbDownload.Name = "tsbDownload"
        Me.tsbDownload.Size = New System.Drawing.Size(50, 35)
        Me.tsbDownload.Text = "Download"
        Me.tsbDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbUpload
        '
        Me.tsbUpload.AutoSize = False
        Me.tsbUpload.ForeColor = System.Drawing.Color.White
        Me.tsbUpload.Image = Global.jade.My.Resources.Resources.arrow_upload_icon
        Me.tsbUpload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbUpload.Name = "tsbUpload"
        Me.tsbUpload.Size = New System.Drawing.Size(50, 35)
        Me.tsbUpload.Text = "Upload"
        Me.tsbUpload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pgbCounter
        '
        Me.pgbCounter.AutoSize = False
        Me.pgbCounter.Name = "pgbCounter"
        Me.pgbCounter.Size = New System.Drawing.Size(100, 25)
        Me.pgbCounter.Visible = False
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 40)
        '
        'tsbCopy
        '
        Me.tsbCopy.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbCopyPR, Me.FromJOToolStripMenuItem, Me.FromRRToolStripMenuItem, Me.FromInventoryCountToolStripMenuItem, Me.FromRRToolStripMenuItem1})
        Me.tsbCopy.ForeColor = System.Drawing.Color.White
        Me.tsbCopy.Image = Global.jade.My.Resources.Resources.finance_report_infographic_512
        Me.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbCopy.Name = "tsbCopy"
        Me.tsbCopy.Size = New System.Drawing.Size(48, 37)
        Me.tsbCopy.Text = "Copy"
        Me.tsbCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbCopyPR
        '
        Me.tsbCopyPR.Name = "tsbCopyPR"
        Me.tsbCopyPR.Size = New System.Drawing.Size(194, 22)
        Me.tsbCopyPR.Text = "From Material Request"
        '
        'FromJOToolStripMenuItem
        '
        Me.FromJOToolStripMenuItem.Name = "FromJOToolStripMenuItem"
        Me.FromJOToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.FromJOToolStripMenuItem.Text = "From Job Order"
        Me.FromJOToolStripMenuItem.Visible = False
        '
        'FromRRToolStripMenuItem
        '
        Me.FromRRToolStripMenuItem.Name = "FromRRToolStripMenuItem"
        Me.FromRRToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.FromRRToolStripMenuItem.Text = "From Purchase Return"
        '
        'FromInventoryCountToolStripMenuItem
        '
        Me.FromInventoryCountToolStripMenuItem.Name = "FromInventoryCountToolStripMenuItem"
        Me.FromInventoryCountToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.FromInventoryCountToolStripMenuItem.Text = "From Inventory Count"
        '
        'FromRRToolStripMenuItem1
        '
        Me.FromRRToolStripMenuItem1.Name = "FromRRToolStripMenuItem1"
        Me.FromRRToolStripMenuItem1.Size = New System.Drawing.Size(194, 22)
        Me.FromRRToolStripMenuItem1.Text = "From Receiving Report"
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
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(112, 22)
        Me.ToolStripMenuItem1.Text = "GI Type"
        '
        'tsbReports
        '
        Me.tsbReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TestToolStripMenuItem1, Me.PRWithoutPOToolStripMenuItem})
        Me.tsbReports.ForeColor = System.Drawing.Color.White
        Me.tsbReports.Image = Global.jade.My.Resources.Resources.finance_report_infographic_512
        Me.tsbReports.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbReports.Name = "tsbReports"
        Me.tsbReports.Size = New System.Drawing.Size(60, 37)
        Me.tsbReports.Text = "Reports"
        Me.tsbReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbReports.Visible = False
        '
        'TestToolStripMenuItem1
        '
        Me.TestToolStripMenuItem1.Name = "TestToolStripMenuItem1"
        Me.TestToolStripMenuItem1.Size = New System.Drawing.Size(142, 22)
        Me.TestToolStripMenuItem1.Text = "PO List"
        '
        'PRWithoutPOToolStripMenuItem
        '
        Me.PRWithoutPOToolStripMenuItem.Name = "PRWithoutPOToolStripMenuItem"
        Me.PRWithoutPOToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.PRWithoutPOToolStripMenuItem.Text = "Unserved PO"
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
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtICRef)
        Me.GroupBox1.Controls.Add(Me.txtConversion)
        Me.GroupBox1.Controls.Add(Me.lblConversion)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtATDNo)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.cbCurrency)
        Me.GroupBox1.Controls.Add(Me.lblCostCenter)
        Me.GroupBox1.Controls.Add(Me.cbCostCenter)
        Me.GroupBox1.Controls.Add(Me.lblJORef)
        Me.GroupBox1.Controls.Add(Me.txtJORef)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtMRRef)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.cbIssuerFrom)
        Me.GroupBox1.Controls.Add(Me.cbWHfrom)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.cbIssueTo)
        Me.GroupBox1.Controls.Add(Me.txtVCEName)
        Me.GroupBox1.Controls.Add(Me.txtVCECode)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.dtpDocDate)
        Me.GroupBox1.Controls.Add(Me.txtTransNum)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtStatus)
        Me.GroupBox1.Controls.Add(Me.btnSearchVCE)
        Me.GroupBox1.Controls.Add(Me.cbTransType)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cbWHto)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(8, 40)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(1204, 231)
        Me.GroupBox1.TabIndex = 1430
        Me.GroupBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label3.Location = New System.Drawing.Point(1003, 98)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 16)
        Me.Label3.TabIndex = 1490
        Me.Label3.Text = "IC Ref.:"
        '
        'txtICRef
        '
        Me.txtICRef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtICRef.Enabled = False
        Me.txtICRef.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtICRef.Location = New System.Drawing.Point(1065, 95)
        Me.txtICRef.Name = "txtICRef"
        Me.txtICRef.Size = New System.Drawing.Size(132, 22)
        Me.txtICRef.TabIndex = 1491
        '
        'txtConversion
        '
        Me.txtConversion.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConversion.Location = New System.Drawing.Point(321, 149)
        Me.txtConversion.Name = "txtConversion"
        Me.txtConversion.Size = New System.Drawing.Size(96, 22)
        Me.txtConversion.TabIndex = 1482
        Me.txtConversion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtConversion.Visible = False
        '
        'lblConversion
        '
        Me.lblConversion.AutoSize = True
        Me.lblConversion.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lblConversion.Location = New System.Drawing.Point(222, 152)
        Me.lblConversion.Name = "lblConversion"
        Me.lblConversion.Size = New System.Drawing.Size(96, 15)
        Me.lblConversion.TabIndex = 1483
        Me.lblConversion.Text = "Exchange Rate :"
        Me.lblConversion.Visible = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label1.Location = New System.Drawing.Point(999, 128)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 16)
        Me.Label1.TabIndex = 1480
        Me.Label1.Text = "ATD No.:"
        Me.Label1.Visible = False
        '
        'txtATDNo
        '
        Me.txtATDNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtATDNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtATDNo.Location = New System.Drawing.Point(1065, 125)
        Me.txtATDNo.Name = "txtATDNo"
        Me.txtATDNo.Size = New System.Drawing.Size(132, 22)
        Me.txtATDNo.TabIndex = 1481
        Me.txtATDNo.Visible = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(35, 152)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(68, 16)
        Me.Label24.TabIndex = 1479
        Me.Label24.Text = "Currency :"
        Me.Label24.Visible = False
        '
        'cbCurrency
        '
        Me.cbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCurrency.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCurrency.FormattingEnabled = True
        Me.cbCurrency.Location = New System.Drawing.Point(103, 149)
        Me.cbCurrency.Name = "cbCurrency"
        Me.cbCurrency.Size = New System.Drawing.Size(114, 24)
        Me.cbCurrency.TabIndex = 1478
        Me.cbCurrency.Visible = False
        '
        'lblCostCenter
        '
        Me.lblCostCenter.AutoSize = True
        Me.lblCostCenter.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostCenter.Location = New System.Drawing.Point(25, 101)
        Me.lblCostCenter.Name = "lblCostCenter"
        Me.lblCostCenter.Size = New System.Drawing.Size(77, 16)
        Me.lblCostCenter.TabIndex = 1477
        Me.lblCostCenter.Text = "Cost Center"
        Me.lblCostCenter.Visible = False
        '
        'cbCostCenter
        '
        Me.cbCostCenter.BackColor = System.Drawing.Color.White
        Me.cbCostCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCostCenter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCostCenter.FormattingEnabled = True
        Me.cbCostCenter.Location = New System.Drawing.Point(103, 96)
        Me.cbCostCenter.Margin = New System.Windows.Forms.Padding(4)
        Me.cbCostCenter.Name = "cbCostCenter"
        Me.cbCostCenter.Size = New System.Drawing.Size(314, 24)
        Me.cbCostCenter.TabIndex = 1476
        Me.cbCostCenter.Visible = False
        '
        'lblJORef
        '
        Me.lblJORef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJORef.AutoSize = True
        Me.lblJORef.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.lblJORef.Location = New System.Drawing.Point(1003, 176)
        Me.lblJORef.Name = "lblJORef"
        Me.lblJORef.Size = New System.Drawing.Size(55, 16)
        Me.lblJORef.TabIndex = 1474
        Me.lblJORef.Text = "JO Ref.:"
        Me.lblJORef.Visible = False
        '
        'txtJORef
        '
        Me.txtJORef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtJORef.Enabled = False
        Me.txtJORef.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtJORef.Location = New System.Drawing.Point(1065, 173)
        Me.txtJORef.Name = "txtJORef"
        Me.txtJORef.Size = New System.Drawing.Size(132, 22)
        Me.txtJORef.TabIndex = 1475
        Me.txtJORef.Visible = False
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label6.Location = New System.Drawing.Point(1003, 152)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 16)
        Me.Label6.TabIndex = 1472
        Me.Label6.Text = "MR Ref.:"
        Me.Label6.Visible = False
        '
        'txtMRRef
        '
        Me.txtMRRef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMRRef.Enabled = False
        Me.txtMRRef.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtMRRef.Location = New System.Drawing.Point(1065, 149)
        Me.txtMRRef.Name = "txtMRRef"
        Me.txtMRRef.Size = New System.Drawing.Size(132, 22)
        Me.txtMRRef.TabIndex = 1473
        Me.txtMRRef.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(45, 21)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(57, 16)
        Me.Label12.TabIndex = 1471
        Me.Label12.Text = "Source :"
        '
        'cbIssuerFrom
        '
        Me.cbIssuerFrom.BackColor = System.Drawing.Color.White
        Me.cbIssuerFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbIssuerFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbIssuerFrom.FormattingEnabled = True
        Me.cbIssuerFrom.Items.AddRange(New Object() {"Warehouse", "Production"})
        Me.cbIssuerFrom.Location = New System.Drawing.Point(103, 17)
        Me.cbIssuerFrom.Margin = New System.Windows.Forms.Padding(4)
        Me.cbIssuerFrom.Name = "cbIssuerFrom"
        Me.cbIssuerFrom.Size = New System.Drawing.Size(127, 24)
        Me.cbIssuerFrom.TabIndex = 1470
        '
        'cbWHfrom
        '
        Me.cbWHfrom.BackColor = System.Drawing.Color.White
        Me.cbWHfrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbWHfrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbWHfrom.FormattingEnabled = True
        Me.cbWHfrom.Items.AddRange(New Object() {"Store"})
        Me.cbWHfrom.Location = New System.Drawing.Point(232, 17)
        Me.cbWHfrom.Margin = New System.Windows.Forms.Padding(4)
        Me.cbWHfrom.Name = "cbWHfrom"
        Me.cbWHfrom.Size = New System.Drawing.Size(314, 24)
        Me.cbWHfrom.TabIndex = 1469
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(40, 47)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(62, 16)
        Me.Label15.TabIndex = 1468
        Me.Label15.Text = "Issue to :"
        '
        'cbIssueTo
        '
        Me.cbIssueTo.BackColor = System.Drawing.Color.White
        Me.cbIssueTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbIssueTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbIssueTo.FormattingEnabled = True
        Me.cbIssueTo.Items.AddRange(New Object() {"Warehouse", "Production"})
        Me.cbIssueTo.Location = New System.Drawing.Point(103, 43)
        Me.cbIssueTo.Margin = New System.Windows.Forms.Padding(4)
        Me.cbIssueTo.Name = "cbIssueTo"
        Me.cbIssueTo.Size = New System.Drawing.Size(127, 24)
        Me.cbIssueTo.TabIndex = 1467
        '
        'txtVCEName
        '
        Me.txtVCEName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVCEName.Location = New System.Drawing.Point(103, 124)
        Me.txtVCEName.Name = "txtVCEName"
        Me.txtVCEName.Size = New System.Drawing.Size(314, 22)
        Me.txtVCEName.TabIndex = 1465
        Me.txtVCEName.Visible = False
        '
        'txtVCECode
        '
        Me.txtVCECode.Enabled = False
        Me.txtVCECode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVCECode.Location = New System.Drawing.Point(103, 95)
        Me.txtVCECode.Name = "txtVCECode"
        Me.txtVCECode.Size = New System.Drawing.Size(314, 22)
        Me.txtVCECode.TabIndex = 1464
        Me.txtVCECode.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(21, 128)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 16)
        Me.Label4.TabIndex = 1462
        Me.Label4.Text = "VCE Name :"
        Me.Label4.Visible = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(25, 99)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(77, 16)
        Me.Label14.TabIndex = 1463
        Me.Label14.Text = "VCE Code :"
        Me.Label14.Visible = False
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(956, 51)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(106, 16)
        Me.Label5.TabIndex = 1458
        Me.Label5.Text = "Document Date :"
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(1008, 75)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(54, 16)
        Me.Label11.TabIndex = 1456
        Me.Label11.Text = "Status :"
        '
        'dtpDocDate
        '
        Me.dtpDocDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpDocDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDocDate.Location = New System.Drawing.Point(1065, 48)
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.Size = New System.Drawing.Size(132, 22)
        Me.dtpDocDate.TabIndex = 1459
        '
        'txtTransNum
        '
        Me.txtTransNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTransNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransNum.Location = New System.Drawing.Point(1065, 24)
        Me.txtTransNum.Name = "txtTransNum"
        Me.txtTransNum.Size = New System.Drawing.Size(132, 22)
        Me.txtTransNum.TabIndex = 1461
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(1009, 27)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 16)
        Me.Label13.TabIndex = 1460
        Me.Label13.Text = "GI No. :"
        '
        'txtStatus
        '
        Me.txtStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtStatus.Enabled = False
        Me.txtStatus.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(1065, 72)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(132, 22)
        Me.txtStatus.TabIndex = 1457
        Me.txtStatus.Text = "Open"
        '
        'btnSearchVCE
        '
        Me.btnSearchVCE.BackgroundImage = Global.jade.My.Resources.Resources.view
        Me.btnSearchVCE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchVCE.Location = New System.Drawing.Point(423, 95)
        Me.btnSearchVCE.Name = "btnSearchVCE"
        Me.btnSearchVCE.Size = New System.Drawing.Size(25, 25)
        Me.btnSearchVCE.TabIndex = 1455
        Me.btnSearchVCE.UseVisualStyleBackColor = True
        Me.btnSearchVCE.Visible = False
        '
        'cbTransType
        '
        Me.cbTransType.BackColor = System.Drawing.Color.White
        Me.cbTransType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTransType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTransType.FormattingEnabled = True
        Me.cbTransType.Items.AddRange(New Object() {"Material Transfer", "Issuance to Job", "Disposal", "Promotional", "Material Return", "Issuance to Department"})
        Me.cbTransType.Location = New System.Drawing.Point(103, 69)
        Me.cbTransType.Margin = New System.Windows.Forms.Padding(4)
        Me.cbTransType.Name = "cbTransType"
        Me.cbTransType.Size = New System.Drawing.Size(314, 24)
        Me.cbTransType.TabIndex = 1429
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(15, 74)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(87, 16)
        Me.Label16.TabIndex = 1430
        Me.Label16.Text = "Trans. Type :"
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(103, 177)
        Me.txtRemarks.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(314, 46)
        Me.txtRemarks.TabIndex = 55
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(33, 173)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 16)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Remarks :"
        '
        'cbWHto
        '
        Me.cbWHto.BackColor = System.Drawing.Color.White
        Me.cbWHto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbWHto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbWHto.FormattingEnabled = True
        Me.cbWHto.Items.AddRange(New Object() {"Store"})
        Me.cbWHto.Location = New System.Drawing.Point(232, 43)
        Me.cbWHto.Margin = New System.Windows.Forms.Padding(4)
        Me.cbWHto.Name = "cbWHto"
        Me.cbWHto.Size = New System.Drawing.Size(314, 24)
        Me.cbWHto.TabIndex = 1417
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(8, 279)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1212, 296)
        Me.TabControl1.TabIndex = 1431
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgvItemMaster)
        Me.TabPage1.Controls.Add(Me.txtGrandTotal)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Size = New System.Drawing.Size(1204, 268)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Transaction"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'dgvItemMaster
        '
        Me.dgvItemMaster.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvItemMaster.BackgroundColor = System.Drawing.Color.White
        Me.dgvItemMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvItemMaster.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chItemCode, Me.chBarcode, Me.chItemName, Me.chUOM, Me.dgcReqQTY, Me.dgcStockQTY, Me.dgcIssueQTY, Me.chWHSE, Me.dgcIssuedQTY, Me.chAvgCost, Me.dgcAccntCode, Me.chAmount, Me.chkFree, Me.chGrossAmount, Me.chDiscountRate, Me.chDiscountAmount, Me.chVATAmount, Me.chNetAmount, Me.chVAT, Me.chVATInc, Me.chDateExpired, Me.chLotNo, Me.chSerialNo, Me.chSize, Me.chColor})
        Me.dgvItemMaster.ContextMenuStrip = Me.contextMenuStrip1
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvItemMaster.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgvItemMaster.Location = New System.Drawing.Point(4, 4)
        Me.dgvItemMaster.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvItemMaster.Name = "dgvItemMaster"
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.dgvItemMaster.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvItemMaster.Size = New System.Drawing.Size(1196, 260)
        Me.dgvItemMaster.TabIndex = 45
        '
        'contextMenuStrip1
        '
        Me.contextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewItemInformationToolStripMenuItem})
        Me.contextMenuStrip1.Name = "contextMenuStrip1"
        Me.contextMenuStrip1.Size = New System.Drawing.Size(193, 26)
        '
        'ViewItemInformationToolStripMenuItem
        '
        Me.ViewItemInformationToolStripMenuItem.Name = "ViewItemInformationToolStripMenuItem"
        Me.ViewItemInformationToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
        Me.ViewItemInformationToolStripMenuItem.Text = "View Item Information"
        '
        'txtGrandTotal
        '
        Me.txtGrandTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGrandTotal.BackColor = System.Drawing.Color.White
        Me.txtGrandTotal.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGrandTotal.Location = New System.Drawing.Point(475, 100)
        Me.txtGrandTotal.Margin = New System.Windows.Forms.Padding(4)
        Me.txtGrandTotal.Name = "txtGrandTotal"
        Me.txtGrandTotal.ReadOnly = True
        Me.txtGrandTotal.Size = New System.Drawing.Size(181, 22)
        Me.txtGrandTotal.TabIndex = 46
        Me.txtGrandTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGrandTotal.Visible = False
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(389, 103)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(82, 16)
        Me.Label8.TabIndex = 47
        Me.Label8.Text = "Grand Total:"
        Me.Label8.Visible = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.txtTotalCredit)
        Me.TabPage2.Controls.Add(Me.txtTotalDebit)
        Me.TabPage2.Controls.Add(Me.lvAccount)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1204, 268)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Entries"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'txtTotalCredit
        '
        Me.txtTotalCredit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTotalCredit.Location = New System.Drawing.Point(841, 240)
        Me.txtTotalCredit.Name = "txtTotalCredit"
        Me.txtTotalCredit.Size = New System.Drawing.Size(147, 21)
        Me.txtTotalCredit.TabIndex = 4
        '
        'txtTotalDebit
        '
        Me.txtTotalDebit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTotalDebit.Location = New System.Drawing.Point(690, 240)
        Me.txtTotalDebit.Name = "txtTotalDebit"
        Me.txtTotalDebit.Size = New System.Drawing.Size(147, 21)
        Me.txtTotalDebit.TabIndex = 3
        '
        'lvAccount
        '
        Me.lvAccount.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvAccount.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chAccountCode, Me.chAccountTitle, Me.chDebit, Me.chCredit, Me.chRef})
        Me.lvAccount.GridLines = True
        Me.lvAccount.Location = New System.Drawing.Point(3, 6)
        Me.lvAccount.Name = "lvAccount"
        Me.lvAccount.Size = New System.Drawing.Size(1195, 228)
        Me.lvAccount.TabIndex = 1
        Me.lvAccount.UseCompatibleStateImageBehavior = False
        Me.lvAccount.View = System.Windows.Forms.View.Details
        '
        'chAccountCode
        '
        Me.chAccountCode.Text = "Account Code"
        Me.chAccountCode.Width = 114
        '
        'chAccountTitle
        '
        Me.chAccountTitle.Text = "Account Code"
        Me.chAccountTitle.Width = 567
        '
        'chDebit
        '
        Me.chDebit.Text = "Debit"
        Me.chDebit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.chDebit.Width = 150
        '
        'chCredit
        '
        Me.chCredit.Text = "Credit"
        Me.chCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.chCredit.Width = 150
        '
        'chRef
        '
        Me.chRef.Text = "Ref. No."
        Me.chRef.Width = 75
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'bgwSave
        '
        Me.bgwSave.WorkerReportsProgress = True
        Me.bgwSave.WorkerSupportsCancellation = True
        '
        'bgwUpload
        '
        Me.bgwUpload.WorkerReportsProgress = True
        Me.bgwUpload.WorkerSupportsCancellation = True
        '
        'chItemCode
        '
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White
        Me.chItemCode.DefaultCellStyle = DataGridViewCellStyle1
        Me.chItemCode.HeaderText = "Item Code"
        Me.chItemCode.Name = "chItemCode"
        Me.chItemCode.Width = 120
        '
        'chBarcode
        '
        Me.chBarcode.HeaderText = "Barcode"
        Me.chBarcode.Name = "chBarcode"
        '
        'chItemName
        '
        Me.chItemName.HeaderText = "Item Name"
        Me.chItemName.Name = "chItemName"
        Me.chItemName.Width = 300
        '
        'chUOM
        '
        Me.chUOM.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.chUOM.HeaderText = "UOM"
        Me.chUOM.Name = "chUOM"
        Me.chUOM.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chUOM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.chUOM.Width = 120
        '
        'dgcReqQTY
        '
        Me.dgcReqQTY.HeaderText = "Request QTY"
        Me.dgcReqQTY.Name = "dgcReqQTY"
        '
        'dgcStockQTY
        '
        Me.dgcStockQTY.HeaderText = "Stock QTY"
        Me.dgcStockQTY.Name = "dgcStockQTY"
        Me.dgcStockQTY.ReadOnly = True
        '
        'dgcIssueQTY
        '
        Me.dgcIssueQTY.HeaderText = "Issue QTY"
        Me.dgcIssueQTY.Name = "dgcIssueQTY"
        Me.dgcIssueQTY.Width = 120
        '
        'chWHSE
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.chWHSE.DefaultCellStyle = DataGridViewCellStyle2
        Me.chWHSE.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.chWHSE.HeaderText = "Warehouse"
        Me.chWHSE.Name = "chWHSE"
        Me.chWHSE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chWHSE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.chWHSE.Visible = False
        Me.chWHSE.Width = 150
        '
        'dgcIssuedQTY
        '
        Me.dgcIssuedQTY.HeaderText = "Issued QTY"
        Me.dgcIssuedQTY.Name = "dgcIssuedQTY"
        Me.dgcIssuedQTY.Visible = False
        '
        'chAvgCost
        '
        Me.chAvgCost.HeaderText = "Unit Cost"
        Me.chAvgCost.Name = "chAvgCost"
        Me.chAvgCost.ReadOnly = True
        '
        'dgcAccntCode
        '
        Me.dgcAccntCode.HeaderText = "AccountCode"
        Me.dgcAccntCode.Name = "dgcAccntCode"
        Me.dgcAccntCode.Visible = False
        '
        'chAmount
        '
        Me.chAmount.HeaderText = "Amount"
        Me.chAmount.Name = "chAmount"
        '
        'chkFree
        '
        Me.chkFree.HeaderText = "Free"
        Me.chkFree.Name = "chkFree"
        Me.chkFree.Visible = False
        Me.chkFree.Width = 50
        '
        'chGrossAmount
        '
        Me.chGrossAmount.HeaderText = "Gross Amount"
        Me.chGrossAmount.Name = "chGrossAmount"
        Me.chGrossAmount.Visible = False
        '
        'chDiscountRate
        '
        Me.chDiscountRate.HeaderText = "Discount Rate"
        Me.chDiscountRate.Name = "chDiscountRate"
        Me.chDiscountRate.Visible = False
        '
        'chDiscountAmount
        '
        Me.chDiscountAmount.HeaderText = "Discount Amount"
        Me.chDiscountAmount.Name = "chDiscountAmount"
        Me.chDiscountAmount.Visible = False
        '
        'chVATAmount
        '
        Me.chVATAmount.HeaderText = "VAT Amount"
        Me.chVATAmount.Name = "chVATAmount"
        Me.chVATAmount.Visible = False
        '
        'chNetAmount
        '
        Me.chNetAmount.HeaderText = "Net Amount"
        Me.chNetAmount.Name = "chNetAmount"
        Me.chNetAmount.Visible = False
        '
        'chVAT
        '
        Me.chVAT.HeaderText = "VAT"
        Me.chVAT.Name = "chVAT"
        Me.chVAT.Visible = False
        '
        'chVATInc
        '
        Me.chVATInc.HeaderText = "VAT Inc"
        Me.chVATInc.Name = "chVATInc"
        Me.chVATInc.Visible = False
        '
        'chDateExpired
        '
        DataGridViewCellStyle3.Format = "d"
        Me.chDateExpired.DefaultCellStyle = DataGridViewCellStyle3
        Me.chDateExpired.HeaderText = "Expiry Date (MM/dd/YYYY)"
        Me.chDateExpired.Name = "chDateExpired"
        '
        'chLotNo
        '
        Me.chLotNo.HeaderText = "Lot No."
        Me.chLotNo.Name = "chLotNo"
        '
        'chSerialNo
        '
        Me.chSerialNo.HeaderText = "Serial No."
        Me.chSerialNo.Name = "chSerialNo"
        '
        'chSize
        '
        Me.chSize.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.chSize.HeaderText = "Size"
        Me.chSize.Name = "chSize"
        Me.chSize.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chSize.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'chColor
        '
        Me.chColor.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.chColor.HeaderText = "Color"
        Me.chColor.Name = "chColor"
        Me.chColor.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'frmGI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1220, 579)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmGI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Emerald - Goods Issue"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.dgvItemMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.contextMenuStrip1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbCopy As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents tsbCopyPR As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbReports As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents TestToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PRWithoutPOToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbPrevious As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNext As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cbTransType As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbWHto As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearchVCE As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents dgvItemMaster As System.Windows.Forms.DataGridView
    Friend WithEvents txtGrandTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtpDocDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtTransNum As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtVCEName As System.Windows.Forms.TextBox
    Friend WithEvents txtVCECode As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cbIssueTo As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cbIssuerFrom As System.Windows.Forms.ComboBox
    Friend WithEvents cbWHfrom As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtMRRef As System.Windows.Forms.TextBox
    Friend WithEvents lblJORef As System.Windows.Forms.Label
    Friend WithEvents txtJORef As System.Windows.Forms.TextBox
    Friend WithEvents FromJOToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblCostCenter As System.Windows.Forms.Label
    Friend WithEvents cbCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents lvAccount As System.Windows.Forms.ListView
    Friend WithEvents chAccountCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents chAccountTitle As System.Windows.Forms.ColumnHeader
    Friend WithEvents chDebit As System.Windows.Forms.ColumnHeader
    Friend WithEvents chCredit As System.Windows.Forms.ColumnHeader
    Friend WithEvents chRef As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtTotalCredit As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalDebit As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents cbCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtATDNo As System.Windows.Forms.TextBox
    Friend WithEvents txtConversion As System.Windows.Forms.TextBox
    Friend WithEvents lblConversion As System.Windows.Forms.Label
    Friend WithEvents FromRRToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FromInventoryCountToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtICRef As System.Windows.Forms.TextBox
    Friend WithEvents tsbDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbUpload As System.Windows.Forms.ToolStripButton
    Friend WithEvents pgbCounter As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents bgwSave As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwUpload As System.ComponentModel.BackgroundWorker
    Friend WithEvents FromRRToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents contextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ViewItemInformationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chItemCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chBarcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chItemName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chUOM As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents dgcReqQTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcStockQTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcIssueQTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chWHSE As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents dgcIssuedQTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chAvgCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcAccntCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkFree As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents chGrossAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chDiscountRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chDiscountAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chVATAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chNetAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chVAT As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents chVATInc As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents chDateExpired As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chLotNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chSerialNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chSize As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents chColor As System.Windows.Forms.DataGridViewComboBoxColumn
End Class
