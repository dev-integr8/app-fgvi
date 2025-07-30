<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVerifier
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVerifier))
        Me.cbFilter = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFilter = New System.Windows.Forms.TextBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.dgvSL = New System.Windows.Forms.DataGridView()
        Me.colVCECode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colVCEName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAccntCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAccntTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDebit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCredit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colButton = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.lbAutoComplete = New System.Windows.Forms.ListBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        CType(Me.dgvSL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cbFilter
        '
        Me.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilter.FormattingEnabled = True
        Me.cbFilter.Items.AddRange(New Object() {"VCEName", "VCECode"})
        Me.cbFilter.Location = New System.Drawing.Point(67, 30)
        Me.cbFilter.Name = "cbFilter"
        Me.cbFilter.Size = New System.Drawing.Size(129, 21)
        Me.cbFilter.TabIndex = 1188
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 1187
        Me.Label1.Text = "Filter by :"
        '
        'txtFilter
        '
        Me.txtFilter.Location = New System.Drawing.Point(199, 31)
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(266, 20)
        Me.txtFilter.TabIndex = 1186
        '
        'btnSearch
        '
        Me.btnSearch.BackgroundImage = Global.jade.My.Resources.Resources.view
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearch.Location = New System.Drawing.Point(467, 28)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(25, 25)
        Me.btnSearch.TabIndex = 1189
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'dgvSL
        '
        Me.dgvSL.AllowUserToAddRows = False
        Me.dgvSL.AllowUserToDeleteRows = False
        Me.dgvSL.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvSL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvSL.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colVCECode, Me.colVCEName, Me.colAccntCode, Me.colAccntTitle, Me.colDebit, Me.colCredit, Me.colButton})
        Me.dgvSL.Location = New System.Drawing.Point(12, 58)
        Me.dgvSL.Name = "dgvSL"
        Me.dgvSL.ReadOnly = True
        Me.dgvSL.RowHeadersVisible = False
        Me.dgvSL.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvSL.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSL.Size = New System.Drawing.Size(960, 491)
        Me.dgvSL.TabIndex = 1190
        '
        'colVCECode
        '
        Me.colVCECode.HeaderText = "VCECode"
        Me.colVCECode.Name = "colVCECode"
        Me.colVCECode.ReadOnly = True
        '
        'colVCEName
        '
        Me.colVCEName.HeaderText = "VCEName"
        Me.colVCEName.Name = "colVCEName"
        Me.colVCEName.ReadOnly = True
        Me.colVCEName.Width = 300
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
        Me.colAccntTitle.Width = 200
        '
        'colDebit
        '
        Me.colDebit.HeaderText = "Debit"
        Me.colDebit.Name = "colDebit"
        Me.colDebit.ReadOnly = True
        '
        'colCredit
        '
        Me.colCredit.HeaderText = "Credit"
        Me.colCredit.Name = "colCredit"
        Me.colCredit.ReadOnly = True
        '
        'colButton
        '
        Me.colButton.HeaderText = ""
        Me.colButton.Name = "colButton"
        Me.colButton.ReadOnly = True
        Me.colButton.Width = 40
        '
        'lbAutoComplete
        '
        Me.lbAutoComplete.FormattingEnabled = True
        Me.lbAutoComplete.Location = New System.Drawing.Point(498, 30)
        Me.lbAutoComplete.Name = "lbAutoComplete"
        Me.lbAutoComplete.Size = New System.Drawing.Size(191, 95)
        Me.lbAutoComplete.TabIndex = 1192
        Me.lbAutoComplete.Visible = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(984, 24)
        Me.MenuStrip1.TabIndex = 1193
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'frmVerifier
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 561)
        Me.Controls.Add(Me.lbAutoComplete)
        Me.Controls.Add(Me.dgvSL)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.cbFilter)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtFilter)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmVerifier"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Verifier"
        CType(Me.dgvSL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents cbFilter As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFilter As System.Windows.Forms.TextBox
    Friend WithEvents dgvSL As System.Windows.Forms.DataGridView
    Friend WithEvents lbAutoComplete As System.Windows.Forms.ListBox
    Friend WithEvents colVCECode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colVCEName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAccntCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAccntTitle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDebit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCredit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colButton As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
End Class
