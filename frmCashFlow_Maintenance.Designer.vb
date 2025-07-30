<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCashFlow_Maintenance
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCashFlow_Maintenance))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbUndo = New System.Windows.Forms.ToolStripButton()
        Me.tsbDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbDownload = New System.Windows.Forms.ToolStripButton()
        Me.tsbUpload = New System.Windows.Forms.ToolStripButton()
        Me.pgbCounter = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrint = New System.Windows.Forms.ToolStripButton()
        Me.tsbExtract = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbUp = New System.Windows.Forms.ToolStripButton()
        Me.tsbDown = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgvCashFlow = New System.Windows.Forms.DataGridView()
        Me.chNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chSubCategory = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chCashFlowGroup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvUndo = New System.Windows.Forms.DataGridView()
        Me.chFrom = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cbType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvCashFlow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvUndo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbNew, Me.tsbEdit, Me.tsbSave, Me.tsbUndo, Me.tsbDelete, Me.ToolStripSeparator1, Me.tsbDownload, Me.tsbUpload, Me.pgbCounter, Me.ToolStripSeparator4, Me.tsbPrint, Me.tsbExtract, Me.ToolStripSeparator2, Me.tsbUp, Me.tsbDown, Me.ToolStripSeparator3, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(948, 40)
        Me.ToolStrip1.TabIndex = 1359
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbNew
        '
        Me.tsbNew.AutoSize = False
        Me.tsbNew.Enabled = False
        Me.tsbNew.ForeColor = System.Drawing.Color.White
        Me.tsbNew.Image = Global.jade.My.Resources.Resources.circle_document_documents_extension_file_page_sheet_icon_7
        Me.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbNew.Name = "tsbNew"
        Me.tsbNew.Size = New System.Drawing.Size(50, 35)
        Me.tsbNew.Text = "Add"
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
        Me.tsbSave.Enabled = False
        Me.tsbSave.ForeColor = System.Drawing.Color.White
        Me.tsbSave.Image = Global.jade.My.Resources.Resources.Save_Icon
        Me.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSave.Name = "tsbSave"
        Me.tsbSave.Size = New System.Drawing.Size(50, 35)
        Me.tsbSave.Text = "Save"
        Me.tsbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbUndo
        '
        Me.tsbUndo.AutoSize = False
        Me.tsbUndo.Enabled = False
        Me.tsbUndo.ForeColor = System.Drawing.Color.White
        Me.tsbUndo.Image = Global.jade.My.Resources.Resources.arrows_147746_960_720
        Me.tsbUndo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbUndo.Name = "tsbUndo"
        Me.tsbUndo.Size = New System.Drawing.Size(50, 35)
        Me.tsbUndo.Text = "Undo"
        Me.tsbUndo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbUndo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbDelete
        '
        Me.tsbDelete.AutoSize = False
        Me.tsbDelete.Enabled = False
        Me.tsbDelete.ForeColor = System.Drawing.Color.White
        Me.tsbDelete.Image = Global.jade.My.Resources.Resources.close_icon
        Me.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbDelete.Name = "tsbDelete"
        Me.tsbDelete.Size = New System.Drawing.Size(50, 35)
        Me.tsbDelete.Text = "Inactive"
        Me.tsbDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 40)
        '
        'tsbDownload
        '
        Me.tsbDownload.AutoSize = False
        Me.tsbDownload.Enabled = False
        Me.tsbDownload.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.tsbDownload.ForeColor = System.Drawing.Color.White
        Me.tsbDownload.Image = Global.jade.My.Resources.Resources.downloadicon
        Me.tsbDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbDownload.Name = "tsbDownload"
        Me.tsbDownload.Size = New System.Drawing.Size(65, 35)
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
        'pgbCounter
        '
        Me.pgbCounter.AutoSize = False
        Me.pgbCounter.Name = "pgbCounter"
        Me.pgbCounter.Size = New System.Drawing.Size(100, 25)
        Me.pgbCounter.Visible = False
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 40)
        '
        'tsbPrint
        '
        Me.tsbPrint.AutoSize = False
        Me.tsbPrint.Enabled = False
        Me.tsbPrint.ForeColor = System.Drawing.Color.White
        Me.tsbPrint.Image = Global.jade.My.Resources.Resources.printer_circle_blue_512
        Me.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPrint.Name = "tsbPrint"
        Me.tsbPrint.Size = New System.Drawing.Size(50, 35)
        Me.tsbPrint.Text = "Print"
        Me.tsbPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbExtract
        '
        Me.tsbExtract.AutoSize = False
        Me.tsbExtract.Enabled = False
        Me.tsbExtract.ForeColor = System.Drawing.Color.White
        Me.tsbExtract.Image = Global.jade.My.Resources.Resources.edit_pen_write_notes_document_3c679c93cb5d1fed_512x512
        Me.tsbExtract.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExtract.Name = "tsbExtract"
        Me.tsbExtract.Size = New System.Drawing.Size(50, 35)
        Me.tsbExtract.Text = "Extract"
        Me.tsbExtract.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 40)
        '
        'tsbUp
        '
        Me.tsbUp.AutoSize = False
        Me.tsbUp.Enabled = False
        Me.tsbUp.ForeColor = System.Drawing.Color.White
        Me.tsbUp.Image = Global.jade.My.Resources.Resources.arrow_up
        Me.tsbUp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbUp.Name = "tsbUp"
        Me.tsbUp.Size = New System.Drawing.Size(50, 35)
        Me.tsbUp.Text = "Up"
        Me.tsbUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbDown
        '
        Me.tsbDown.AutoSize = False
        Me.tsbDown.Enabled = False
        Me.tsbDown.ForeColor = System.Drawing.Color.White
        Me.tsbDown.Image = Global.jade.My.Resources.Resources.arrows_down
        Me.tsbDown.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbDown.Name = "tsbDown"
        Me.tsbDown.Size = New System.Drawing.Size(50, 35)
        Me.tsbDown.Text = "Down"
        Me.tsbDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 40)
        '
        'tsbClose
        '
        Me.tsbClose.AutoSize = False
        Me.tsbClose.Enabled = False
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
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.dgvCashFlow)
        Me.GroupBox1.Controls.Add(Me.dgvUndo)
        Me.GroupBox1.Controls.Add(Me.cbType)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 43)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(923, 541)
        Me.GroupBox1.TabIndex = 1360
        Me.GroupBox1.TabStop = False
        '
        'dgvCashFlow
        '
        Me.dgvCashFlow.AllowDrop = True
        Me.dgvCashFlow.AllowUserToAddRows = False
        Me.dgvCashFlow.AllowUserToDeleteRows = False
        Me.dgvCashFlow.AllowUserToResizeColumns = False
        Me.dgvCashFlow.AllowUserToResizeRows = False
        Me.dgvCashFlow.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvCashFlow.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCashFlow.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvCashFlow.ColumnHeadersHeight = 22
        Me.dgvCashFlow.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chNo, Me.chCode, Me.chDescription, Me.chSubCategory, Me.chCashFlowGroup})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvCashFlow.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvCashFlow.GridColor = System.Drawing.Color.White
        Me.dgvCashFlow.Location = New System.Drawing.Point(7, 57)
        Me.dgvCashFlow.MultiSelect = False
        Me.dgvCashFlow.Name = "dgvCashFlow"
        Me.dgvCashFlow.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCashFlow.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvCashFlow.RowHeadersVisible = False
        Me.dgvCashFlow.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.dgvCashFlow.RowTemplate.Height = 16
        Me.dgvCashFlow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCashFlow.ShowCellToolTips = False
        Me.dgvCashFlow.Size = New System.Drawing.Size(910, 478)
        Me.dgvCashFlow.TabIndex = 1355
        '
        'chNo
        '
        Me.chNo.DataPropertyName = "No"
        Me.chNo.HeaderText = "No"
        Me.chNo.Name = "chNo"
        Me.chNo.ReadOnly = True
        '
        'chCode
        '
        Me.chCode.DataPropertyName = "Code"
        Me.chCode.HeaderText = "Code"
        Me.chCode.Name = "chCode"
        Me.chCode.ReadOnly = True
        '
        'chDescription
        '
        Me.chDescription.DataPropertyName = "AccountTitle"
        Me.chDescription.HeaderText = "Description"
        Me.chDescription.Name = "chDescription"
        Me.chDescription.ReadOnly = True
        Me.chDescription.Width = 300
        '
        'chSubCategory
        '
        Me.chSubCategory.DataPropertyName = "SubCategory"
        Me.chSubCategory.HeaderText = "SubCategory"
        Me.chSubCategory.Name = "chSubCategory"
        Me.chSubCategory.ReadOnly = True
        Me.chSubCategory.Width = 200
        '
        'chCashFlowGroup
        '
        Me.chCashFlowGroup.DataPropertyName = "SCF_Group"
        Me.chCashFlowGroup.HeaderText = "CashFlowGroup"
        Me.chCashFlowGroup.Name = "chCashFlowGroup"
        Me.chCashFlowGroup.ReadOnly = True
        Me.chCashFlowGroup.Width = 200
        '
        'dgvUndo
        '
        Me.dgvUndo.AllowUserToAddRows = False
        Me.dgvUndo.AllowUserToDeleteRows = False
        Me.dgvUndo.AllowUserToOrderColumns = True
        Me.dgvUndo.AllowUserToResizeColumns = False
        Me.dgvUndo.AllowUserToResizeRows = False
        Me.dgvUndo.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvUndo.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvUndo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUndo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chFrom, Me.chTo})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvUndo.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgvUndo.Location = New System.Drawing.Point(21, 57)
        Me.dgvUndo.Name = "dgvUndo"
        Me.dgvUndo.ReadOnly = True
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvUndo.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvUndo.RowHeadersVisible = False
        Me.dgvUndo.Size = New System.Drawing.Size(469, 299)
        Me.dgvUndo.TabIndex = 1343
        Me.dgvUndo.Visible = False
        '
        'chFrom
        '
        Me.chFrom.HeaderText = "From"
        Me.chFrom.Name = "chFrom"
        Me.chFrom.ReadOnly = True
        '
        'chTo
        '
        Me.chTo.HeaderText = "To"
        Me.chTo.Name = "chTo"
        Me.chTo.ReadOnly = True
        '
        'cbType
        '
        Me.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.cbType.FormattingEnabled = True
        Me.cbType.Location = New System.Drawing.Point(57, 19)
        Me.cbType.Name = "cbType"
        Me.cbType.Size = New System.Drawing.Size(244, 23)
        Me.cbType.TabIndex = 1350
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(12, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 15)
        Me.Label1.TabIndex = 1349
        Me.Label1.Text = "Type :"
        '
        'frmCashFlow_Maintenance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(948, 594)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCashFlow_Maintenance"
        Me.Text = "frmCashFlow_Maintenance"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvCashFlow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvUndo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents tsbNew As ToolStripButton
    Friend WithEvents tsbEdit As ToolStripButton
    Friend WithEvents tsbSave As ToolStripButton
    Friend WithEvents tsbUndo As ToolStripButton
    Friend WithEvents tsbDelete As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents tsbDownload As ToolStripButton
    Friend WithEvents tsbUpload As ToolStripButton
    Friend WithEvents pgbCounter As ToolStripProgressBar
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents tsbPrint As ToolStripButton
    Friend WithEvents tsbExtract As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents tsbUp As ToolStripButton
    Friend WithEvents tsbDown As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents tsbClose As ToolStripButton
    Friend WithEvents tsbExit As ToolStripButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents dgvCashFlow As DataGridView
    Friend WithEvents dgvUndo As DataGridView
    Friend WithEvents chFrom As DataGridViewTextBoxColumn
    Friend WithEvents chTo As DataGridViewTextBoxColumn
    Friend WithEvents cbType As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents chNo As DataGridViewTextBoxColumn
    Friend WithEvents chCode As DataGridViewTextBoxColumn
    Friend WithEvents chDescription As DataGridViewTextBoxColumn
    Friend WithEvents chSubCategory As DataGridViewTextBoxColumn
    Friend WithEvents chCashFlowGroup As DataGridViewTextBoxColumn
End Class
