<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBOMSelect
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
        Me.gbBOM = New System.Windows.Forms.GroupBox()
        Me.btnAddMats = New System.Windows.Forms.Button()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtQTY = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.nupQty = New System.Windows.Forms.NumericUpDown()
        Me.cbBOM = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtUOM = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbItemName = New System.Windows.Forms.ComboBox()
        Me.gbBOM.SuspendLayout()
        CType(Me.nupQty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gbBOM
        '
        Me.gbBOM.Controls.Add(Me.cbItemName)
        Me.gbBOM.Controls.Add(Me.Label6)
        Me.gbBOM.Controls.Add(Me.Label3)
        Me.gbBOM.Controls.Add(Me.btnAddMats)
        Me.gbBOM.Controls.Add(Me.txtItemCode)
        Me.gbBOM.Controls.Add(Me.Label10)
        Me.gbBOM.Controls.Add(Me.txtQTY)
        Me.gbBOM.Controls.Add(Me.Label9)
        Me.gbBOM.Controls.Add(Me.nupQty)
        Me.gbBOM.Controls.Add(Me.cbBOM)
        Me.gbBOM.Controls.Add(Me.Label7)
        Me.gbBOM.Controls.Add(Me.Label1)
        Me.gbBOM.Controls.Add(Me.txtUOM)
        Me.gbBOM.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.gbBOM.Location = New System.Drawing.Point(12, 12)
        Me.gbBOM.Name = "gbBOM"
        Me.gbBOM.Size = New System.Drawing.Size(453, 165)
        Me.gbBOM.TabIndex = 1451
        Me.gbBOM.TabStop = False
        Me.gbBOM.Text = "Bill of Materials"
        '
        'btnAddMats
        '
        Me.btnAddMats.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnAddMats.Location = New System.Drawing.Point(98, 127)
        Me.btnAddMats.Name = "btnAddMats"
        Me.btnAddMats.Size = New System.Drawing.Size(348, 30)
        Me.btnAddMats.TabIndex = 1450
        Me.btnAddMats.Text = "Add Materials"
        Me.btnAddMats.UseVisualStyleBackColor = True
        '
        'txtItemCode
        '
        Me.txtItemCode.AcceptsReturn = True
        Me.txtItemCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemCode.Enabled = False
        Me.txtItemCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.Location = New System.Drawing.Point(98, 50)
        Me.txtItemCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.Size = New System.Drawing.Size(348, 22)
        Me.txtItemCode.TabIndex = 1444
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(17, 53)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 16)
        Me.Label10.TabIndex = 1449
        Me.Label10.Text = "Item Code :"
        '
        'txtQTY
        '
        Me.txtQTY.AcceptsReturn = True
        Me.txtQTY.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtQTY.Enabled = False
        Me.txtQTY.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQTY.Location = New System.Drawing.Point(98, 102)
        Me.txtQTY.Margin = New System.Windows.Forms.Padding(4)
        Me.txtQTY.Name = "txtQTY"
        Me.txtQTY.Size = New System.Drawing.Size(205, 22)
        Me.txtQTY.TabIndex = 1442
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(13, 78)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(79, 16)
        Me.Label9.TabIndex = 1448
        Me.Label9.Text = "Item Name :"
        '
        'nupQty
        '
        Me.nupQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nupQty.Location = New System.Drawing.Point(387, 25)
        Me.nupQty.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.nupQty.Name = "nupQty"
        Me.nupQty.Size = New System.Drawing.Size(59, 22)
        Me.nupQty.TabIndex = 1438
        Me.nupQty.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cbBOM
        '
        Me.cbBOM.BackColor = System.Drawing.Color.White
        Me.cbBOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBOM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbBOM.FormattingEnabled = True
        Me.cbBOM.Location = New System.Drawing.Point(98, 24)
        Me.cbBOM.Margin = New System.Windows.Forms.Padding(4)
        Me.cbBOM.Name = "cbBOM"
        Me.cbBOM.Size = New System.Drawing.Size(216, 24)
        Me.cbBOM.TabIndex = 1436
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(311, 105)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 16)
        Me.Label7.TabIndex = 1447
        Me.Label7.Text = "UOM :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(48, 27)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 16)
        Me.Label1.TabIndex = 1437
        Me.Label1.Text = "BOM :"
        '
        'txtUOM
        '
        Me.txtUOM.AcceptsReturn = True
        Me.txtUOM.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUOM.Enabled = False
        Me.txtUOM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUOM.Location = New System.Drawing.Point(364, 102)
        Me.txtUOM.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUOM.Name = "txtUOM"
        Me.txtUOM.Size = New System.Drawing.Size(82, 22)
        Me.txtUOM.TabIndex = 1446
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(316, 28)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 16)
        Me.Label3.TabIndex = 1451
        Me.Label3.Text = "Batch Qty:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 104)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 16)
        Me.Label6.TabIndex = 1452
        Me.Label6.Text = "Standard Qty:"
        '
        'cbItemName
        '
        Me.cbItemName.BackColor = System.Drawing.Color.White
        Me.cbItemName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbItemName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbItemName.FormattingEnabled = True
        Me.cbItemName.Location = New System.Drawing.Point(98, 75)
        Me.cbItemName.Margin = New System.Windows.Forms.Padding(4)
        Me.cbItemName.Name = "cbItemName"
        Me.cbItemName.Size = New System.Drawing.Size(348, 24)
        Me.cbItemName.TabIndex = 1453
        '
        'frmBOMSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(477, 189)
        Me.Controls.Add(Me.gbBOM)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBOMSelect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BOM Selection"
        Me.gbBOM.ResumeLayout(False)
        Me.gbBOM.PerformLayout()
        CType(Me.nupQty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbBOM As System.Windows.Forms.GroupBox
    Friend WithEvents btnAddMats As System.Windows.Forms.Button
    Friend WithEvents txtItemCode As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtQTY As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents nupQty As System.Windows.Forms.NumericUpDown
    Friend WithEvents cbBOM As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtUOM As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbItemName As System.Windows.Forms.ComboBox
End Class
