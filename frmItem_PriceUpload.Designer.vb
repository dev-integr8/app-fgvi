<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmItem_PriceUpload
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmItem_PriceUpload))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbExtract = New System.Windows.Forms.ToolStripButton()
        Me.tsbDownload = New System.Windows.Forms.ToolStripButton()
        Me.tsbUpload = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.pgbCounter = New System.Windows.Forms.ProgressBar()
        Me.dgvEntry = New System.Windows.Forms.DataGridView()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.chItemCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chItemName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chUOM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chVATInc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chVAT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbNew, Me.tsbSave, Me.ToolStripSeparator1, Me.tsbExtract, Me.tsbDownload, Me.tsbUpload, Me.ToolStripSeparator3, Me.tsbClose, Me.tsbExit})
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
        Me.tsbNew.Text = "Generate"
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
        'tsbExtract
        '
        Me.tsbExtract.AutoSize = False
        Me.tsbExtract.ForeColor = System.Drawing.Color.White
        Me.tsbExtract.Image = Global.jade.My.Resources.Resources.edit_pen_write_notes_document_3c679c93cb5d1fed_512x512
        Me.tsbExtract.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExtract.Name = "tsbExtract"
        Me.tsbExtract.Size = New System.Drawing.Size(50, 35)
        Me.tsbExtract.Text = "Extract"
        Me.tsbExtract.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        Me.tsbDownload.Visible = False
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
        Me.dgvEntry.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEntry.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chItemCode, Me.chItemName, Me.chPrice, Me.chUOM, Me.chQTY, Me.chType, Me.chVATInc, Me.chVAT})
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
        'chPrice
        '
        Me.chPrice.HeaderText = "Price"
        Me.chPrice.Name = "chPrice"
        Me.chPrice.ReadOnly = True
        '
        'chUOM
        '
        Me.chUOM.HeaderText = "UOM"
        Me.chUOM.Name = "chUOM"
        Me.chUOM.ReadOnly = True
        '
        'chQTY
        '
        Me.chQTY.HeaderText = "QTY"
        Me.chQTY.Name = "chQTY"
        Me.chQTY.ReadOnly = True
        '
        'chType
        '
        Me.chType.HeaderText = "Type"
        Me.chType.Name = "chType"
        Me.chType.ReadOnly = True
        '
        'chVATInc
        '
        Me.chVATInc.HeaderText = "VAT Inc."
        Me.chVATInc.Name = "chVATInc"
        Me.chVATInc.ReadOnly = True
        '
        'chVAT
        '
        Me.chVAT.HeaderText = "Vatable"
        Me.chVAT.Name = "chVAT"
        Me.chVAT.ReadOnly = True
        '
        'frmItem_PriceUpload
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
        Me.Name = "frmItem_PriceUpload"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Item Price Upload"
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
    Friend WithEvents tsbExtract As System.Windows.Forms.ToolStripButton
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents chItemCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chItemName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chUOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chQTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chVATInc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chVAT As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
