<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmITEM_Uploader
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmITEM_Uploader))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbDownload = New System.Windows.Forms.ToolStripButton()
        Me.tsbUpload = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.pgbCounter = New System.Windows.Forms.ProgressBar()
        Me.dgvEntry = New System.Windows.Forms.DataGridView()
        Me.chItemCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chItemName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chItemType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chUnitCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chInvCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chInvTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chCOSCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chCOSTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chSales = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chSalesTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chVCECode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chVCEName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chUOM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chConvertQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chUOMTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbNew, Me.tsbSave, Me.ToolStripSeparator1, Me.tsbDownload, Me.tsbUpload, Me.ToolStripSeparator3, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1002, 40)
        Me.ToolStrip1.TabIndex = 1204
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
        'lblTime
        '
        Me.lblTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTime.AutoSize = True
        Me.lblTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lblTime.Location = New System.Drawing.Point(272, 457)
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
        Me.pgbCounter.Location = New System.Drawing.Point(12, 454)
        Me.pgbCounter.Name = "pgbCounter"
        Me.pgbCounter.Size = New System.Drawing.Size(244, 18)
        Me.pgbCounter.TabIndex = 1067
        '
        'dgvEntry
        '
        Me.dgvEntry.AllowUserToAddRows = False
        Me.dgvEntry.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEntry.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chItemCode, Me.chItemName, Me.chItemType, Me.chUnitCost, Me.chInvCode, Me.chInvTitle, Me.chCOSCode, Me.chCOSTitle, Me.chSales, Me.chSalesTitle, Me.chVCECode, Me.chVCEName, Me.chUOM, Me.chConvertQTY, Me.chUOMTo})
        Me.dgvEntry.Location = New System.Drawing.Point(13, 44)
        Me.dgvEntry.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvEntry.Name = "dgvEntry"
        Me.dgvEntry.ReadOnly = True
        Me.dgvEntry.Size = New System.Drawing.Size(977, 403)
        Me.dgvEntry.TabIndex = 1306
        '
        'chItemCode
        '
        Me.chItemCode.HeaderText = "Item Code"
        Me.chItemCode.Name = "chItemCode"
        Me.chItemCode.ReadOnly = True
        '
        'chItemName
        '
        Me.chItemName.HeaderText = "Item Name"
        Me.chItemName.Name = "chItemName"
        Me.chItemName.ReadOnly = True
        '
        'chItemType
        '
        Me.chItemType.HeaderText = "Item Type"
        Me.chItemType.Name = "chItemType"
        Me.chItemType.ReadOnly = True
        '
        'chUnitCost
        '
        Me.chUnitCost.HeaderText = "Unit Cost"
        Me.chUnitCost.Name = "chUnitCost"
        Me.chUnitCost.ReadOnly = True
        '
        'chInvCode
        '
        Me.chInvCode.HeaderText = "Inventory Code"
        Me.chInvCode.Name = "chInvCode"
        Me.chInvCode.ReadOnly = True
        '
        'chInvTitle
        '
        Me.chInvTitle.HeaderText = "Inventory Title"
        Me.chInvTitle.Name = "chInvTitle"
        Me.chInvTitle.ReadOnly = True
        '
        'chCOSCode
        '
        Me.chCOSCode.HeaderText = "Cost of Sales Code"
        Me.chCOSCode.Name = "chCOSCode"
        Me.chCOSCode.ReadOnly = True
        '
        'chCOSTitle
        '
        Me.chCOSTitle.HeaderText = "Cost of Sales Title"
        Me.chCOSTitle.Name = "chCOSTitle"
        Me.chCOSTitle.ReadOnly = True
        '
        'chSales
        '
        Me.chSales.HeaderText = "Sales Code"
        Me.chSales.Name = "chSales"
        Me.chSales.ReadOnly = True
        '
        'chSalesTitle
        '
        Me.chSalesTitle.HeaderText = "Sales Title"
        Me.chSalesTitle.Name = "chSalesTitle"
        Me.chSalesTitle.ReadOnly = True
        '
        'chVCECode
        '
        Me.chVCECode.HeaderText = "VCECode"
        Me.chVCECode.Name = "chVCECode"
        Me.chVCECode.ReadOnly = True
        '
        'chVCEName
        '
        Me.chVCEName.HeaderText = "Supplier"
        Me.chVCEName.Name = "chVCEName"
        Me.chVCEName.ReadOnly = True
        Me.chVCEName.Width = 250
        '
        'chUOM
        '
        Me.chUOM.HeaderText = "UOM From"
        Me.chUOM.Name = "chUOM"
        Me.chUOM.ReadOnly = True
        '
        'chConvertQTY
        '
        Me.chConvertQTY.HeaderText = "Convert QTY"
        Me.chConvertQTY.Name = "chConvertQTY"
        Me.chConvertQTY.ReadOnly = True
        '
        'chUOMTo
        '
        Me.chUOMTo.HeaderText = "UOM To"
        Me.chUOMTo.Name = "chUOMTo"
        Me.chUOMTo.ReadOnly = True
        '
        'frmITEM_Uploader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1002, 478)
        Me.Controls.Add(Me.dgvEntry)
        Me.Controls.Add(Me.lblTime)
        Me.Controls.Add(Me.pgbCounter)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmITEM_Uploader"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Item Uploader"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvEntry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents pgbCounter As System.Windows.Forms.ProgressBar
    Friend WithEvents dgvEntry As System.Windows.Forms.DataGridView
    Friend WithEvents tsbDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbUpload As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents chItemCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chItemName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chItemType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chUnitCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chInvCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chInvTitle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chCOSCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chCOSTitle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chSales As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chSalesTitle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chVCECode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chVCEName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chUOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chConvertQTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chUOMTo As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
