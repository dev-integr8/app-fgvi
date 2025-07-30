<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSeachSL
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtVCEName = New System.Windows.Forms.TextBox()
        Me.dgvSL = New System.Windows.Forms.DataGridView()
        Me.colVCECode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colVCEName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAccntCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAccntTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRefNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvSL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 1202
        Me.Label1.Text = "VCE Name :"
        '
        'txtVCEName
        '
        Me.txtVCEName.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtVCEName.BackColor = System.Drawing.Color.White
        Me.txtVCEName.Location = New System.Drawing.Point(83, 4)
        Me.txtVCEName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtVCEName.Name = "txtVCEName"
        Me.txtVCEName.Size = New System.Drawing.Size(575, 20)
        Me.txtVCEName.TabIndex = 1203
        '
        'dgvSL
        '
        Me.dgvSL.AllowUserToAddRows = False
        Me.dgvSL.AllowUserToDeleteRows = False
        Me.dgvSL.AllowUserToResizeColumns = False
        Me.dgvSL.AllowUserToResizeRows = False
        Me.dgvSL.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvSL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSL.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colVCECode, Me.colVCEName, Me.colAccntCode, Me.colAccntTitle, Me.colRefNo, Me.colBalance})
        Me.dgvSL.Location = New System.Drawing.Point(4, 27)
        Me.dgvSL.MultiSelect = False
        Me.dgvSL.Name = "dgvSL"
        Me.dgvSL.ReadOnly = True
        Me.dgvSL.RowHeadersVisible = False
        Me.dgvSL.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSL.Size = New System.Drawing.Size(654, 278)
        Me.dgvSL.TabIndex = 1204
        '
        'colVCECode
        '
        Me.colVCECode.HeaderText = "VCECode"
        Me.colVCECode.Name = "colVCECode"
        Me.colVCECode.ReadOnly = True
        Me.colVCECode.Width = 60
        '
        'colVCEName
        '
        Me.colVCEName.HeaderText = "VCEName"
        Me.colVCEName.Name = "colVCEName"
        Me.colVCEName.ReadOnly = True
        Me.colVCEName.Width = 150
        '
        'colAccntCode
        '
        Me.colAccntCode.HeaderText = "AccntCode"
        Me.colAccntCode.Name = "colAccntCode"
        Me.colAccntCode.ReadOnly = True
        Me.colAccntCode.Width = 80
        '
        'colAccntTitle
        '
        Me.colAccntTitle.HeaderText = "AccntTitle"
        Me.colAccntTitle.Name = "colAccntTitle"
        Me.colAccntTitle.ReadOnly = True
        Me.colAccntTitle.Width = 150
        '
        'colRefNo
        '
        Me.colRefNo.HeaderText = "RefNo"
        Me.colRefNo.Name = "colRefNo"
        Me.colRefNo.ReadOnly = True
        Me.colRefNo.Width = 80
        '
        'colBalance
        '
        Me.colBalance.HeaderText = "Balance"
        Me.colBalance.Name = "colBalance"
        Me.colBalance.ReadOnly = True
        '
        'frmSeachSL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(661, 308)
        Me.Controls.Add(Me.dgvSL)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtVCEName)
        Me.Name = "frmSeachSL"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Search SL"
        CType(Me.dgvSL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtVCEName As System.Windows.Forms.TextBox
    Friend WithEvents dgvSL As System.Windows.Forms.DataGridView
    Friend WithEvents colVCECode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colVCEName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAccntCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAccntTitle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRefNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBalance As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
