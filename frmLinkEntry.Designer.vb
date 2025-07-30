<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLinkEntry
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
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lbType = New System.Windows.Forms.ListBox()
        Me.lbNo = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFilterType = New System.Windows.Forms.TextBox()
        Me.txtFilterNo = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnSelect
        '
        Me.btnSelect.Location = New System.Drawing.Point(169, 300)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(75, 32)
        Me.btnSelect.TabIndex = 4
        Me.btnSelect.Text = "Select"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(250, 300)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 32)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Cancel"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'lbType
        '
        Me.lbType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbType.FormattingEnabled = True
        Me.lbType.ItemHeight = 16
        Me.lbType.Location = New System.Drawing.Point(4, 49)
        Me.lbType.Margin = New System.Windows.Forms.Padding(4)
        Me.lbType.Name = "lbType"
        Me.lbType.Size = New System.Drawing.Size(157, 244)
        Me.lbType.TabIndex = 0
        '
        'lbNo
        '
        Me.lbNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbNo.FormattingEnabled = True
        Me.lbNo.ItemHeight = 16
        Me.lbNo.Location = New System.Drawing.Point(169, 49)
        Me.lbNo.Margin = New System.Windows.Forms.Padding(4)
        Me.lbNo.Name = "lbNo"
        Me.lbNo.Size = New System.Drawing.Size(157, 244)
        Me.lbNo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Ref. Type"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(166, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 17)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Ref. No."
        '
        'txtFilterType
        '
        Me.txtFilterType.Location = New System.Drawing.Point(4, 28)
        Me.txtFilterType.Name = "txtFilterType"
        Me.txtFilterType.Size = New System.Drawing.Size(157, 23)
        Me.txtFilterType.TabIndex = 6
        '
        'txtFilterNo
        '
        Me.txtFilterNo.Location = New System.Drawing.Point(169, 28)
        Me.txtFilterNo.Name = "txtFilterNo"
        Me.txtFilterNo.Size = New System.Drawing.Size(157, 23)
        Me.txtFilterNo.TabIndex = 7
        '
        'frmLinkEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 335)
        Me.Controls.Add(Me.txtFilterNo)
        Me.Controls.Add(Me.txtFilterType)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbNo)
        Me.Controls.Add(Me.lbType)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmLinkEntry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Link Entry"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents lbType As System.Windows.Forms.ListBox
    Friend WithEvents lbNo As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFilterType As System.Windows.Forms.TextBox
    Friend WithEvents txtFilterNo As System.Windows.Forms.TextBox
End Class
