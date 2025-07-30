<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFund_Maintenance
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.dgvFunds = New System.Windows.Forms.DataGridView()
        Me.pnlSavings = New System.Windows.Forms.Panel()
        Me.nudShare = New System.Windows.Forms.NumericUpDown()
        Me.nudMonthsDelay = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dgvPercent = New System.Windows.Forms.DataGridView()
        Me.colAccntCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAccntTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMinimum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPercent = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMember = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colDebit = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.colCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMonthsDelay = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colShareCapital = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvFunds, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSavings.SuspendLayout()
        CType(Me.nudShare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudMonthsDelay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvPercent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.DimGray
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbNew, Me.tsbEdit, Me.tsbSave, Me.tsbDelete, Me.ToolStripSeparator1, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(845, 38)
        Me.ToolStrip1.TabIndex = 1407
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
        'tsbEdit
        '
        Me.tsbEdit.AutoSize = False
        Me.tsbEdit.Enabled = False
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
        'tsbDelete
        '
        Me.tsbDelete.AutoSize = False
        Me.tsbDelete.Enabled = False
        Me.tsbDelete.ForeColor = System.Drawing.Color.White
        Me.tsbDelete.Image = Global.jade.My.Resources.Resources.close_icon
        Me.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbDelete.Name = "tsbDelete"
        Me.tsbDelete.Size = New System.Drawing.Size(50, 35)
        Me.tsbDelete.Text = "Delete"
        Me.tsbDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 38)
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
        'dgvFunds
        '
        Me.dgvFunds.AllowUserToAddRows = False
        Me.dgvFunds.AllowUserToDeleteRows = False
        Me.dgvFunds.AllowUserToResizeColumns = False
        Me.dgvFunds.AllowUserToResizeRows = False
        Me.dgvFunds.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgvFunds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFunds.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCode, Me.colDescription, Me.colMonthsDelay, Me.colShareCapital})
        Me.dgvFunds.Location = New System.Drawing.Point(5, 42)
        Me.dgvFunds.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgvFunds.Name = "dgvFunds"
        Me.dgvFunds.ReadOnly = True
        Me.dgvFunds.RowHeadersVisible = False
        Me.dgvFunds.Size = New System.Drawing.Size(262, 284)
        Me.dgvFunds.TabIndex = 1408
        '
        'pnlSavings
        '
        Me.pnlSavings.Controls.Add(Me.nudShare)
        Me.pnlSavings.Controls.Add(Me.nudMonthsDelay)
        Me.pnlSavings.Controls.Add(Me.Label2)
        Me.pnlSavings.Controls.Add(Me.dgvPercent)
        Me.pnlSavings.Controls.Add(Me.Label1)
        Me.pnlSavings.Controls.Add(Me.Label4)
        Me.pnlSavings.Controls.Add(Me.txtDesc)
        Me.pnlSavings.Enabled = False
        Me.pnlSavings.Location = New System.Drawing.Point(273, 42)
        Me.pnlSavings.Name = "pnlSavings"
        Me.pnlSavings.Size = New System.Drawing.Size(568, 284)
        Me.pnlSavings.TabIndex = 1409
        '
        'nudShare
        '
        Me.nudShare.Location = New System.Drawing.Point(88, 48)
        Me.nudShare.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nudShare.Name = "nudShare"
        Me.nudShare.Size = New System.Drawing.Size(67, 20)
        Me.nudShare.TabIndex = 1201
        Me.nudShare.Value = New Decimal(New Integer() {2000, 0, 0, 0})
        '
        'nudMonthsDelay
        '
        Me.nudMonthsDelay.Location = New System.Drawing.Point(88, 26)
        Me.nudMonthsDelay.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        Me.nudMonthsDelay.Name = "nudMonthsDelay"
        Me.nudMonthsDelay.Size = New System.Drawing.Size(67, 20)
        Me.nudMonthsDelay.TabIndex = 1201
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 1198
        Me.Label2.Text = "Share Capital :"
        '
        'dgvPercent
        '
        Me.dgvPercent.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPercent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPercent.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colAccntCode, Me.colAccntTitle, Me.colMinimum, Me.colPercent, Me.colMember, Me.colDebit})
        Me.dgvPercent.Location = New System.Drawing.Point(3, 71)
        Me.dgvPercent.Name = "dgvPercent"
        Me.dgvPercent.Size = New System.Drawing.Size(562, 210)
        Me.dgvPercent.TabIndex = 1200
        '
        'colAccntCode
        '
        Me.colAccntCode.HeaderText = "AccntCode"
        Me.colAccntCode.Name = "colAccntCode"
        Me.colAccntCode.ReadOnly = True
        Me.colAccntCode.Width = 50
        '
        'colAccntTitle
        '
        Me.colAccntTitle.HeaderText = "AccntTitle"
        Me.colAccntTitle.Name = "colAccntTitle"
        Me.colAccntTitle.Width = 200
        '
        'colMinimum
        '
        Me.colMinimum.HeaderText = "Minimum"
        Me.colMinimum.Name = "colMinimum"
        '
        'colPercent
        '
        Me.colPercent.HeaderText = "Percent"
        Me.colPercent.Name = "colPercent"
        Me.colPercent.Width = 50
        '
        'colMember
        '
        Me.colMember.HeaderText = "Member"
        Me.colMember.Name = "colMember"
        Me.colMember.Width = 50
        '
        'colDebit
        '
        Me.colDebit.HeaderText = "Debit"
        Me.colDebit.Name = "colDebit"
        Me.colDebit.Width = 50
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 1198
        Me.Label1.Text = "Months Delay :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 1198
        Me.Label4.Text = "Description :"
        '
        'txtDesc
        '
        Me.txtDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDesc.BackColor = System.Drawing.Color.White
        Me.txtDesc.Location = New System.Drawing.Point(88, 4)
        Me.txtDesc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(477, 20)
        Me.txtDesc.TabIndex = 1199
        '
        'colCode
        '
        Me.colCode.HeaderText = "Code"
        Me.colCode.Name = "colCode"
        Me.colCode.ReadOnly = True
        Me.colCode.Visible = False
        '
        'colDescription
        '
        Me.colDescription.HeaderText = "Description"
        Me.colDescription.Name = "colDescription"
        Me.colDescription.ReadOnly = True
        Me.colDescription.Width = 256
        '
        'colMonthsDelay
        '
        Me.colMonthsDelay.HeaderText = "MonthsDelay"
        Me.colMonthsDelay.Name = "colMonthsDelay"
        Me.colMonthsDelay.ReadOnly = True
        Me.colMonthsDelay.Visible = False
        '
        'colShareCapital
        '
        Me.colShareCapital.HeaderText = "ShareCapital"
        Me.colShareCapital.Name = "colShareCapital"
        Me.colShareCapital.ReadOnly = True
        Me.colShareCapital.Visible = False
        '
        'frmFund_Maintenance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(845, 331)
        Me.Controls.Add(Me.pnlSavings)
        Me.Controls.Add(Me.dgvFunds)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frmFund_Maintenance"
        Me.Text = "Fund Maintenance"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvFunds, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSavings.ResumeLayout(False)
        Me.pnlSavings.PerformLayout()
        CType(Me.nudShare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudMonthsDelay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvPercent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgvFunds As System.Windows.Forms.DataGridView
    Friend WithEvents pnlSavings As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents dgvPercent As System.Windows.Forms.DataGridView
    Friend WithEvents nudMonthsDelay As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents colAccntCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAccntTitle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMinimum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPercent As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMember As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colDebit As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents nudShare As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents colCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMonthsDelay As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colShareCapital As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
