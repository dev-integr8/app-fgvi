<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSavings_AccountSearch
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
        Me.dgvSavings = New System.Windows.Forms.DataGridView()
        Me.colVCECode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colVCEName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDepositType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAccountNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvSavings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 1200
        Me.Label1.Text = "VCE Name :"
        '
        'txtVCEName
        '
        Me.txtVCEName.BackColor = System.Drawing.Color.White
        Me.txtVCEName.Location = New System.Drawing.Point(89, 13)
        Me.txtVCEName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtVCEName.Name = "txtVCEName"
        Me.txtVCEName.Size = New System.Drawing.Size(406, 20)
        Me.txtVCEName.TabIndex = 1201
        '
        'dgvSavings
        '
        Me.dgvSavings.AllowUserToAddRows = False
        Me.dgvSavings.AllowUserToDeleteRows = False
        Me.dgvSavings.AllowUserToResizeColumns = False
        Me.dgvSavings.AllowUserToResizeRows = False
        Me.dgvSavings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSavings.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colVCECode, Me.colVCEName, Me.colDepositType, Me.colAccountNumber})
        Me.dgvSavings.Location = New System.Drawing.Point(12, 40)
        Me.dgvSavings.Name = "dgvSavings"
        Me.dgvSavings.ReadOnly = True
        Me.dgvSavings.RowHeadersVisible = False
        Me.dgvSavings.Size = New System.Drawing.Size(483, 280)
        Me.dgvSavings.TabIndex = 1202
        '
        'colVCECode
        '
        Me.colVCECode.HeaderText = "VCECode"
        Me.colVCECode.Name = "colVCECode"
        Me.colVCECode.ReadOnly = True
        Me.colVCECode.Width = 80
        '
        'colVCEName
        '
        Me.colVCEName.HeaderText = "VCEName"
        Me.colVCEName.Name = "colVCEName"
        Me.colVCEName.ReadOnly = True
        Me.colVCEName.Width = 120
        '
        'colDepositType
        '
        Me.colDepositType.HeaderText = "Deposit Type"
        Me.colDepositType.Name = "colDepositType"
        Me.colDepositType.ReadOnly = True
        Me.colDepositType.Width = 150
        '
        'colAccountNumber
        '
        Me.colAccountNumber.HeaderText = "Account Number"
        Me.colAccountNumber.Name = "colAccountNumber"
        Me.colAccountNumber.ReadOnly = True
        Me.colAccountNumber.Width = 120
        '
        'frmSavings_AccountSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(507, 332)
        Me.Controls.Add(Me.dgvSavings)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtVCEName)
        Me.Name = "frmSavings_AccountSearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Search"
        CType(Me.dgvSavings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtVCEName As System.Windows.Forms.TextBox
    Friend WithEvents dgvSavings As System.Windows.Forms.DataGridView
    Friend WithEvents colVCECode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colVCEName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDepositType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAccountNumber As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
