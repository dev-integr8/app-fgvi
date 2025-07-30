<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm2307Receiving
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm2307Receiving))
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txt2307Amount = New System.Windows.Forms.TextBox()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.lblDateReceived = New System.Windows.Forms.Label()
        Me.dtp2307 = New System.Windows.Forms.DateTimePicker()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(228, 66)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(78, 26)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'txt2307Amount
        '
        Me.txt2307Amount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt2307Amount.Location = New System.Drawing.Point(117, 10)
        Me.txt2307Amount.Name = "txt2307Amount"
        Me.txt2307Amount.Size = New System.Drawing.Size(189, 22)
        Me.txt2307Amount.TabIndex = 1
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = True
        Me.lblAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmount.Location = New System.Drawing.Point(60, 10)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(57, 16)
        Me.lblAmount.TabIndex = 2
        Me.lblAmount.Text = "Amount:"
        '
        'lblDateReceived
        '
        Me.lblDateReceived.AutoSize = True
        Me.lblDateReceived.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateReceived.Location = New System.Drawing.Point(22, 39)
        Me.lblDateReceived.Name = "lblDateReceived"
        Me.lblDateReceived.Size = New System.Drawing.Size(95, 16)
        Me.lblDateReceived.TabIndex = 3
        Me.lblDateReceived.Text = "Date Received:"
        '
        'dtp2307
        '
        Me.dtp2307.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp2307.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp2307.Location = New System.Drawing.Point(117, 39)
        Me.dtp2307.Name = "dtp2307"
        Me.dtp2307.Size = New System.Drawing.Size(189, 21)
        Me.dtp2307.TabIndex = 4
        '
        'frm2307Receiving
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(320, 103)
        Me.Controls.Add(Me.dtp2307)
        Me.Controls.Add(Me.lblDateReceived)
        Me.Controls.Add(Me.lblAmount)
        Me.Controls.Add(Me.txt2307Amount)
        Me.Controls.Add(Me.btnSave)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm2307Receiving"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "2307 Receiving"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnSave As Button
    Friend WithEvents txt2307Amount As TextBox
    Friend WithEvents lblAmount As Label
    Friend WithEvents lblDateReceived As Label
    Friend WithEvents dtp2307 As DateTimePicker
End Class
