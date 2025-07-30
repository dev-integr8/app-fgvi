<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmItemPrice_Uploader
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmItemPrice_Uploader))
        Me.lblTime = New System.Windows.Forms.Label()
        Me.pgbCounter = New System.Windows.Forms.ProgressBar()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.dgvEntry = New System.Windows.Forms.DataGridView()
        Me.chD_ItemCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chD_ItemName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chD_UnitPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chD_UOM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chD_QTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chD_Type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chD_VATInc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chD_VATable = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.bgwUpload = New System.ComponentModel.BackgroundWorker()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.bgwSave = New System.ComponentModel.BackgroundWorker()
        Me.txtDifference = New System.Windows.Forms.TextBox()
        Me.txtCredit = New System.Windows.Forms.TextBox()
        Me.txtDebit = New System.Windows.Forms.TextBox()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtUpload_ID = New System.Windows.Forms.TextBox()
        Me.tsbSearch = New System.Windows.Forms.ToolStripButton()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbCancel = New System.Windows.Forms.ToolStripButton()
        Me.tsbDownload = New System.Windows.Forms.ToolStripButton()
        Me.tsbUpload = New System.Windows.Forms.ToolStripButton()
        Me.tsbPrevious = New System.Windows.Forms.ToolStripButton()
        Me.tsbNext = New System.Windows.Forms.ToolStripButton()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        CType(Me.dgvEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTime
        '
        Me.lblTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTime.AutoSize = True
        Me.lblTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lblTime.Location = New System.Drawing.Point(775, 75)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(0, 15)
        Me.lblTime.TabIndex = 1097
        Me.lblTime.Visible = False
        '
        'pgbCounter
        '
        Me.pgbCounter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgbCounter.BackColor = System.Drawing.Color.Gainsboro
        Me.pgbCounter.ForeColor = System.Drawing.Color.Red
        Me.pgbCounter.Location = New System.Drawing.Point(770, 53)
        Me.pgbCounter.Name = "pgbCounter"
        Me.pgbCounter.Size = New System.Drawing.Size(476, 18)
        Me.pgbCounter.TabIndex = 1067
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCount.Location = New System.Drawing.Point(18, 75)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(96, 16)
        Me.lblCount.TabIndex = 1082
        Me.lblCount.Text = "Record Count :"
        Me.lblCount.Visible = False
        '
        'dgvEntry
        '
        Me.dgvEntry.AllowUserToAddRows = False
        Me.dgvEntry.AllowUserToDeleteRows = False
        Me.dgvEntry.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEntry.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chD_ItemCode, Me.chD_ItemName, Me.chD_UnitPrice, Me.chD_UOM, Me.chD_QTY, Me.chD_Type, Me.chD_VATInc, Me.chD_VATable})
        Me.dgvEntry.Location = New System.Drawing.Point(12, 107)
        Me.dgvEntry.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvEntry.Name = "dgvEntry"
        Me.dgvEntry.ReadOnly = True
        Me.dgvEntry.Size = New System.Drawing.Size(1253, 512)
        Me.dgvEntry.TabIndex = 1305
        '
        'chD_ItemCode
        '
        Me.chD_ItemCode.HeaderText = "Item Code"
        Me.chD_ItemCode.Name = "chD_ItemCode"
        Me.chD_ItemCode.ReadOnly = True
        '
        'chD_ItemName
        '
        Me.chD_ItemName.HeaderText = "Item Name"
        Me.chD_ItemName.Name = "chD_ItemName"
        Me.chD_ItemName.ReadOnly = True
        Me.chD_ItemName.Width = 200
        '
        'chD_UnitPrice
        '
        Me.chD_UnitPrice.HeaderText = "Unit Price"
        Me.chD_UnitPrice.Name = "chD_UnitPrice"
        Me.chD_UnitPrice.ReadOnly = True
        '
        'chD_UOM
        '
        Me.chD_UOM.HeaderText = "UOM"
        Me.chD_UOM.Name = "chD_UOM"
        Me.chD_UOM.ReadOnly = True
        '
        'chD_QTY
        '
        Me.chD_QTY.HeaderText = "QTY"
        Me.chD_QTY.Name = "chD_QTY"
        Me.chD_QTY.ReadOnly = True
        '
        'chD_Type
        '
        Me.chD_Type.HeaderText = "Type"
        Me.chD_Type.Name = "chD_Type"
        Me.chD_Type.ReadOnly = True
        '
        'chD_VATInc
        '
        Me.chD_VATInc.HeaderText = "VAT Inc"
        Me.chD_VATInc.Name = "chD_VATInc"
        Me.chD_VATInc.ReadOnly = True
        '
        'chD_VATable
        '
        Me.chD_VATable.HeaderText = "Vatable"
        Me.chD_VATable.Name = "chD_VATable"
        Me.chD_VATable.ReadOnly = True
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
        'bgwSave
        '
        Me.bgwSave.WorkerReportsProgress = True
        Me.bgwSave.WorkerSupportsCancellation = True
        '
        'txtDifference
        '
        Me.txtDifference.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtDifference.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDifference.Location = New System.Drawing.Point(809, 623)
        Me.txtDifference.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDifference.Name = "txtDifference"
        Me.txtDifference.ReadOnly = True
        Me.txtDifference.Size = New System.Drawing.Size(96, 22)
        Me.txtDifference.TabIndex = 1309
        '
        'txtCredit
        '
        Me.txtCredit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCredit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCredit.Location = New System.Drawing.Point(707, 623)
        Me.txtCredit.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCredit.Name = "txtCredit"
        Me.txtCredit.ReadOnly = True
        Me.txtCredit.Size = New System.Drawing.Size(96, 22)
        Me.txtCredit.TabIndex = 1306
        '
        'txtDebit
        '
        Me.txtDebit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtDebit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDebit.Location = New System.Drawing.Point(605, 623)
        Me.txtDebit.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDebit.Name = "txtDebit"
        Me.txtDebit.ReadOnly = True
        Me.txtDebit.Size = New System.Drawing.Size(96, 22)
        Me.txtDebit.TabIndex = 1307
        '
        'lblTotal
        '
        Me.lblTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(558, 623)
        Me.lblTotal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(39, 16)
        Me.lblTotal.TabIndex = 1308
        Me.lblTotal.Text = "Total:"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSearch, Me.tsbNew, Me.tsbSave, Me.tsbCancel, Me.ToolStripSeparator4, Me.tsbDownload, Me.tsbUpload, Me.ToolStripSeparator2, Me.tsbPrevious, Me.tsbNext, Me.ToolStripSeparator1, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1278, 38)
        Me.ToolStrip1.TabIndex = 1310
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 38)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 38)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 38)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(39, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 16)
        Me.Label2.TabIndex = 1311
        Me.Label2.Text = "Upload ID :"
        Me.Label2.Visible = False
        '
        'txtUpload_ID
        '
        Me.txtUpload_ID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtUpload_ID.Location = New System.Drawing.Point(120, 49)
        Me.txtUpload_ID.Name = "txtUpload_ID"
        Me.txtUpload_ID.ReadOnly = True
        Me.txtUpload_ID.Size = New System.Drawing.Size(100, 22)
        Me.txtUpload_ID.TabIndex = 1312
        Me.txtUpload_ID.Visible = False
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
        Me.tsbSearch.Visible = False
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
        'frmItemPrice_Uploader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1278, 649)
        Me.Controls.Add(Me.lblTime)
        Me.Controls.Add(Me.pgbCounter)
        Me.Controls.Add(Me.txtUpload_ID)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.txtDifference)
        Me.Controls.Add(Me.txtCredit)
        Me.Controls.Add(Me.txtDebit)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.dgvEntry)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmItemPrice_Uploader"
        Me.Text = "Item Price Uploader"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgvEntry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents pgbCounter As System.Windows.Forms.ProgressBar
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents dgvEntry As System.Windows.Forms.DataGridView
    Friend WithEvents bgwUpload As System.ComponentModel.BackgroundWorker
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents bgwSave As System.ComponentModel.BackgroundWorker
    Friend WithEvents txtDifference As System.Windows.Forms.TextBox
    Friend WithEvents txtCredit As System.Windows.Forms.TextBox
    Friend WithEvents txtDebit As System.Windows.Forms.TextBox
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbPrevious As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNext As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtUpload_ID As System.Windows.Forms.TextBox
    Friend WithEvents tsbCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbUpload As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents chD_ItemCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chD_ItemName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chD_UnitPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chD_UOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chD_QTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chD_Type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chD_VATInc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chD_VATable As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
