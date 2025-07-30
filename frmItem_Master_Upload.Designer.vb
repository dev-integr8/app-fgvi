<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmItem_Master_Upload
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmItem_Master_Upload))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbOpen = New System.Windows.Forms.ToolStripButton()
        Me.tsbDownload = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.tcUpload = New System.Windows.Forms.TabControl()
        Me.tpItemMaster = New System.Windows.Forms.TabPage()
        Me.dgvItemMaster = New System.Windows.Forms.DataGridView()
        Me.tpItemBarcode = New System.Windows.Forms.TabPage()
        Me.dgvItemBarcode = New System.Windows.Forms.DataGridView()
        Me.tpItemPrice = New System.Windows.Forms.TabPage()
        Me.dgvItemPrice = New System.Windows.Forms.DataGridView()
        Me.tpItemConversion = New System.Windows.Forms.TabPage()
        Me.dgvItemConversion = New System.Windows.Forms.DataGridView()
        Me.pbItem = New System.Windows.Forms.ProgressBar()
        Me.bgwUpload = New System.ComponentModel.BackgroundWorker()
        Me.ToolStrip1.SuspendLayout()
        Me.tcUpload.SuspendLayout()
        Me.tpItemMaster.SuspendLayout()
        CType(Me.dgvItemMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpItemBarcode.SuspendLayout()
        CType(Me.dgvItemBarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpItemPrice.SuspendLayout()
        CType(Me.dgvItemPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpItemConversion.SuspendLayout()
        CType(Me.dgvItemConversion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSave, Me.ToolStripSeparator1, Me.tsbOpen, Me.tsbDownload, Me.ToolStripSeparator3, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(800, 40)
        Me.ToolStrip1.TabIndex = 1205
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbSave
        '
        Me.tsbSave.AutoSize = False
        Me.tsbSave.Enabled = False
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
        'tsbOpen
        '
        Me.tsbOpen.AutoSize = False
        Me.tsbOpen.ForeColor = System.Drawing.Color.White
        Me.tsbOpen.Image = Global.jade.My.Resources.Resources.folder_icon
        Me.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbOpen.Name = "tsbOpen"
        Me.tsbOpen.Size = New System.Drawing.Size(50, 35)
        Me.tsbOpen.Text = "Open"
        Me.tsbOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbDownload
        '
        Me.tsbDownload.AutoSize = False
        Me.tsbDownload.ForeColor = System.Drawing.Color.White
        Me.tsbDownload.Image = Global.jade.My.Resources.Resources.arrows_down
        Me.tsbDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbDownload.Name = "tsbDownload"
        Me.tsbDownload.Size = New System.Drawing.Size(70, 35)
        Me.tsbDownload.Text = "Download"
        Me.tsbDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        'tcUpload
        '
        Me.tcUpload.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcUpload.Controls.Add(Me.tpItemMaster)
        Me.tcUpload.Controls.Add(Me.tpItemBarcode)
        Me.tcUpload.Controls.Add(Me.tpItemPrice)
        Me.tcUpload.Controls.Add(Me.tpItemConversion)
        Me.tcUpload.Location = New System.Drawing.Point(0, 43)
        Me.tcUpload.Name = "tcUpload"
        Me.tcUpload.SelectedIndex = 0
        Me.tcUpload.Size = New System.Drawing.Size(800, 378)
        Me.tcUpload.TabIndex = 1206
        '
        'tpItemMaster
        '
        Me.tpItemMaster.Controls.Add(Me.dgvItemMaster)
        Me.tpItemMaster.Location = New System.Drawing.Point(4, 22)
        Me.tpItemMaster.Name = "tpItemMaster"
        Me.tpItemMaster.Padding = New System.Windows.Forms.Padding(3)
        Me.tpItemMaster.Size = New System.Drawing.Size(792, 352)
        Me.tpItemMaster.TabIndex = 0
        Me.tpItemMaster.Text = "Item Master"
        Me.tpItemMaster.UseVisualStyleBackColor = True
        '
        'dgvItemMaster
        '
        Me.dgvItemMaster.AllowUserToAddRows = False
        Me.dgvItemMaster.AllowUserToDeleteRows = False
        Me.dgvItemMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvItemMaster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvItemMaster.Location = New System.Drawing.Point(3, 3)
        Me.dgvItemMaster.Name = "dgvItemMaster"
        Me.dgvItemMaster.ReadOnly = True
        Me.dgvItemMaster.RowHeadersVisible = False
        Me.dgvItemMaster.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvItemMaster.Size = New System.Drawing.Size(786, 346)
        Me.dgvItemMaster.TabIndex = 0
        '
        'tpItemBarcode
        '
        Me.tpItemBarcode.Controls.Add(Me.dgvItemBarcode)
        Me.tpItemBarcode.Location = New System.Drawing.Point(4, 22)
        Me.tpItemBarcode.Name = "tpItemBarcode"
        Me.tpItemBarcode.Padding = New System.Windows.Forms.Padding(3)
        Me.tpItemBarcode.Size = New System.Drawing.Size(792, 352)
        Me.tpItemBarcode.TabIndex = 1
        Me.tpItemBarcode.Text = "Item Barcode"
        Me.tpItemBarcode.UseVisualStyleBackColor = True
        '
        'dgvItemBarcode
        '
        Me.dgvItemBarcode.AllowUserToAddRows = False
        Me.dgvItemBarcode.AllowUserToDeleteRows = False
        Me.dgvItemBarcode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvItemBarcode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvItemBarcode.Location = New System.Drawing.Point(3, 3)
        Me.dgvItemBarcode.Name = "dgvItemBarcode"
        Me.dgvItemBarcode.ReadOnly = True
        Me.dgvItemBarcode.RowHeadersVisible = False
        Me.dgvItemBarcode.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvItemBarcode.Size = New System.Drawing.Size(786, 346)
        Me.dgvItemBarcode.TabIndex = 1
        '
        'tpItemPrice
        '
        Me.tpItemPrice.Controls.Add(Me.dgvItemPrice)
        Me.tpItemPrice.Location = New System.Drawing.Point(4, 22)
        Me.tpItemPrice.Name = "tpItemPrice"
        Me.tpItemPrice.Size = New System.Drawing.Size(792, 352)
        Me.tpItemPrice.TabIndex = 2
        Me.tpItemPrice.Text = "Item Price"
        Me.tpItemPrice.UseVisualStyleBackColor = True
        '
        'dgvItemPrice
        '
        Me.dgvItemPrice.AllowUserToAddRows = False
        Me.dgvItemPrice.AllowUserToDeleteRows = False
        Me.dgvItemPrice.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvItemPrice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvItemPrice.Location = New System.Drawing.Point(3, 3)
        Me.dgvItemPrice.Name = "dgvItemPrice"
        Me.dgvItemPrice.ReadOnly = True
        Me.dgvItemPrice.RowHeadersVisible = False
        Me.dgvItemPrice.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvItemPrice.Size = New System.Drawing.Size(786, 346)
        Me.dgvItemPrice.TabIndex = 3
        '
        'tpItemConversion
        '
        Me.tpItemConversion.Controls.Add(Me.dgvItemConversion)
        Me.tpItemConversion.Location = New System.Drawing.Point(4, 22)
        Me.tpItemConversion.Name = "tpItemConversion"
        Me.tpItemConversion.Padding = New System.Windows.Forms.Padding(3)
        Me.tpItemConversion.Size = New System.Drawing.Size(792, 352)
        Me.tpItemConversion.TabIndex = 3
        Me.tpItemConversion.Text = "Item Conversion"
        Me.tpItemConversion.UseVisualStyleBackColor = True
        '
        'dgvItemConversion
        '
        Me.dgvItemConversion.AllowUserToAddRows = False
        Me.dgvItemConversion.AllowUserToDeleteRows = False
        Me.dgvItemConversion.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvItemConversion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvItemConversion.Location = New System.Drawing.Point(3, 3)
        Me.dgvItemConversion.Name = "dgvItemConversion"
        Me.dgvItemConversion.ReadOnly = True
        Me.dgvItemConversion.RowHeadersVisible = False
        Me.dgvItemConversion.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvItemConversion.Size = New System.Drawing.Size(786, 346)
        Me.dgvItemConversion.TabIndex = 4
        '
        'pbItem
        '
        Me.pbItem.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pbItem.Location = New System.Drawing.Point(0, 427)
        Me.pbItem.Name = "pbItem"
        Me.pbItem.Size = New System.Drawing.Size(800, 23)
        Me.pbItem.TabIndex = 1207
        '
        'bgwUpload
        '
        Me.bgwUpload.WorkerReportsProgress = True
        '
        'frmItem_Master_Upload
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pbItem)
        Me.Controls.Add(Me.tcUpload)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmItem_Master_Upload"
        Me.Text = "Item Master Uploader"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.tcUpload.ResumeLayout(False)
        Me.tpItemMaster.ResumeLayout(False)
        CType(Me.dgvItemMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpItemBarcode.ResumeLayout(False)
        CType(Me.dgvItemBarcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpItemPrice.ResumeLayout(False)
        CType(Me.dgvItemPrice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpItemConversion.ResumeLayout(False)
        CType(Me.dgvItemConversion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents tsbSave As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents tsbDownload As ToolStripButton
    Friend WithEvents tsbOpen As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents tsbClose As ToolStripButton
    Friend WithEvents tsbExit As ToolStripButton
    Friend WithEvents tcUpload As TabControl
    Friend WithEvents tpItemMaster As TabPage
    Friend WithEvents dgvItemMaster As DataGridView
    Friend WithEvents tpItemBarcode As TabPage
    Friend WithEvents dgvItemBarcode As DataGridView
    Friend WithEvents pbItem As ProgressBar
    Friend WithEvents bgwUpload As System.ComponentModel.BackgroundWorker
    Friend WithEvents tpItemPrice As TabPage
    Friend WithEvents dgvItemPrice As DataGridView
    Friend WithEvents tpItemConversion As System.Windows.Forms.TabPage
    Friend WithEvents dgvItemConversion As System.Windows.Forms.DataGridView
End Class
