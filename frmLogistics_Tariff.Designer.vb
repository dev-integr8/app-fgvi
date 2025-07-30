<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLogistics_Tariff
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogistics_Tariff))
        Me.dgvRecords = New System.Windows.Forms.DataGridView()
        Me.dgcFrom = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcVehicle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcVCECode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcVCEName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        CType(Me.dgvRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvRecords
        '
        Me.dgvRecords.AllowUserToAddRows = False
        Me.dgvRecords.AllowUserToDeleteRows = False
        Me.dgvRecords.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvRecords.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dgvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRecords.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcFrom, Me.dgcTo, Me.dgcVehicle, Me.dgcRate, Me.dgcVCECode, Me.dgcVCEName})
        Me.dgvRecords.Location = New System.Drawing.Point(0, 43)
        Me.dgvRecords.MultiSelect = False
        Me.dgvRecords.Name = "dgvRecords"
        Me.dgvRecords.RowHeadersWidth = 25
        Me.dgvRecords.Size = New System.Drawing.Size(645, 376)
        Me.dgvRecords.TabIndex = 0
        '
        'dgcFrom
        '
        Me.dgcFrom.HeaderText = "From"
        Me.dgcFrom.Name = "dgcFrom"
        Me.dgcFrom.Width = 120
        '
        'dgcTo
        '
        Me.dgcTo.HeaderText = "To"
        Me.dgcTo.Name = "dgcTo"
        Me.dgcTo.Width = 120
        '
        'dgcVehicle
        '
        Me.dgcVehicle.HeaderText = "Vehicle"
        Me.dgcVehicle.Name = "dgcVehicle"
        '
        'dgcRate
        '
        Me.dgcRate.HeaderText = "Rate"
        Me.dgcRate.Name = "dgcRate"
        Me.dgcRate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgcRate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.dgcRate.Width = 80
        '
        'dgcVCECode
        '
        Me.dgcVCECode.HeaderText = "VCECode"
        Me.dgcVCECode.Name = "dgcVCECode"
        Me.dgcVCECode.Visible = False
        '
        'dgcVCEName
        '
        Me.dgcVCEName.HeaderText = "Supplier"
        Me.dgcVCEName.Name = "dgcVCEName"
        Me.dgcVCEName.Width = 250
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbNew, Me.tsbSave, Me.ToolStripSeparator1, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(645, 40)
        Me.ToolStrip1.TabIndex = 1406
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
        Me.tsbNew.Text = "Upload"
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
        'frmLogistics_Tariff
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(645, 419)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.dgvRecords)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmLogistics_Tariff"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rate Maintenance"
        CType(Me.dgvRecords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvRecords As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgcFrom As DataGridViewTextBoxColumn
    Friend WithEvents dgcTo As DataGridViewTextBoxColumn
    Friend WithEvents dgcVehicle As DataGridViewTextBoxColumn
    Friend WithEvents dgcRate As DataGridViewTextBoxColumn
    Friend WithEvents dgcVCECode As DataGridViewTextBoxColumn
    Friend WithEvents dgcVCEName As DataGridViewTextBoxColumn
End Class
