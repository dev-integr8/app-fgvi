<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTaxComputation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTaxComputation))
        Me.btnCompute = New System.Windows.Forms.Button()
        Me.txtGrossAmount = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNetAmount = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbVATCode = New System.Windows.Forms.ComboBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtVATRate = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtVATAmount = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtEWTAmount = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtEWTRate = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbEWTCode = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.chkVATInc = New System.Windows.Forms.CheckBox()
        Me.chkVAT = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'btnCompute
        '
        Me.btnCompute.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.btnCompute.Location = New System.Drawing.Point(58, 240)
        Me.btnCompute.Name = "btnCompute"
        Me.btnCompute.Size = New System.Drawing.Size(291, 38)
        Me.btnCompute.TabIndex = 0
        Me.btnCompute.Text = "Compute"
        Me.btnCompute.UseVisualStyleBackColor = True
        '
        'txtGrossAmount
        '
        Me.txtGrossAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGrossAmount.Location = New System.Drawing.Point(112, 12)
        Me.txtGrossAmount.Name = "txtGrossAmount"
        Me.txtGrossAmount.ReadOnly = True
        Me.txtGrossAmount.Size = New System.Drawing.Size(291, 22)
        Me.txtGrossAmount.TabIndex = 1361
        Me.txtGrossAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label1.Location = New System.Drawing.Point(7, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 16)
        Me.Label1.TabIndex = 1362
        Me.Label1.Text = "Gross Amount :"
        '
        'txtNetAmount
        '
        Me.txtNetAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetAmount.Location = New System.Drawing.Point(112, 40)
        Me.txtNetAmount.Name = "txtNetAmount"
        Me.txtNetAmount.ReadOnly = True
        Me.txtNetAmount.Size = New System.Drawing.Size(291, 22)
        Me.txtNetAmount.TabIndex = 1363
        Me.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label2.Location = New System.Drawing.Point(22, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 16)
        Me.Label2.TabIndex = 1364
        Me.Label2.Text = "Net Amount :"
        '
        'cbVATCode
        '
        Me.cbVATCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbVATCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbVATCode.FormattingEnabled = True
        Me.cbVATCode.Items.AddRange(New Object() {"Cash", "Check", "Multiple Check", "Manager's Check", "Bank Transfer", "Credit Card", "(Multiple Payment Method)"})
        Me.cbVATCode.Location = New System.Drawing.Point(112, 68)
        Me.cbVATCode.Name = "cbVATCode"
        Me.cbVATCode.Size = New System.Drawing.Size(291, 24)
        Me.cbVATCode.TabIndex = 1365
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label25.Location = New System.Drawing.Point(33, 71)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(73, 16)
        Me.Label25.TabIndex = 1366
        Me.Label25.Text = "VAT Code :"
        '
        'txtVATRate
        '
        Me.txtVATRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVATRate.Location = New System.Drawing.Point(112, 98)
        Me.txtVATRate.Name = "txtVATRate"
        Me.txtVATRate.ReadOnly = True
        Me.txtVATRate.Size = New System.Drawing.Size(273, 22)
        Me.txtVATRate.TabIndex = 1367
        Me.txtVATRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label3.Location = New System.Drawing.Point(36, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 16)
        Me.Label3.TabIndex = 1368
        Me.Label3.Text = "VAT Rate :"
        '
        'txtVATAmount
        '
        Me.txtVATAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVATAmount.Location = New System.Drawing.Point(112, 126)
        Me.txtVATAmount.Name = "txtVATAmount"
        Me.txtVATAmount.ReadOnly = True
        Me.txtVATAmount.Size = New System.Drawing.Size(291, 22)
        Me.txtVATAmount.TabIndex = 1369
        Me.txtVATAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label4.Location = New System.Drawing.Point(19, 129)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 16)
        Me.Label4.TabIndex = 1370
        Me.Label4.Text = "VAT Amount :"
        '
        'txtEWTAmount
        '
        Me.txtEWTAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEWTAmount.Location = New System.Drawing.Point(112, 212)
        Me.txtEWTAmount.Name = "txtEWTAmount"
        Me.txtEWTAmount.ReadOnly = True
        Me.txtEWTAmount.Size = New System.Drawing.Size(291, 22)
        Me.txtEWTAmount.TabIndex = 1375
        Me.txtEWTAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label5.Location = New System.Drawing.Point(13, 215)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 16)
        Me.Label5.TabIndex = 1376
        Me.Label5.Text = "EWT Amount :"
        '
        'txtEWTRate
        '
        Me.txtEWTRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEWTRate.Location = New System.Drawing.Point(112, 184)
        Me.txtEWTRate.Name = "txtEWTRate"
        Me.txtEWTRate.ReadOnly = True
        Me.txtEWTRate.Size = New System.Drawing.Size(273, 22)
        Me.txtEWTRate.TabIndex = 1373
        Me.txtEWTRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label6.Location = New System.Drawing.Point(30, 187)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 16)
        Me.Label6.TabIndex = 1374
        Me.Label6.Text = "EWT Rate :"
        '
        'cbEWTCode
        '
        Me.cbEWTCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbEWTCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbEWTCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbEWTCode.FormattingEnabled = True
        Me.cbEWTCode.Items.AddRange(New Object() {"Cash", "Check", "Multiple Check", "Manager's Check", "Bank Transfer", "Credit Card", "(Multiple Payment Method)"})
        Me.cbEWTCode.Location = New System.Drawing.Point(112, 154)
        Me.cbEWTCode.Name = "cbEWTCode"
        Me.cbEWTCode.Size = New System.Drawing.Size(291, 24)
        Me.cbEWTCode.TabIndex = 1371
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label7.Location = New System.Drawing.Point(27, 157)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 16)
        Me.Label7.TabIndex = 1372
        Me.Label7.Text = "EWT Code :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label8.Location = New System.Drawing.Point(388, 101)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(20, 16)
        Me.Label8.TabIndex = 1377
        Me.Label8.Text = "%"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label9.Location = New System.Drawing.Point(388, 187)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(20, 16)
        Me.Label9.TabIndex = 1378
        Me.Label9.Text = "%"
        '
        'chkVATInc
        '
        Me.chkVATInc.AutoSize = True
        Me.chkVATInc.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.chkVATInc.Location = New System.Drawing.Point(409, 15)
        Me.chkVATInc.Name = "chkVATInc"
        Me.chkVATInc.Size = New System.Drawing.Size(71, 20)
        Me.chkVATInc.TabIndex = 1379
        Me.chkVATInc.Text = "VAT Inc"
        Me.chkVATInc.UseVisualStyleBackColor = True
        '
        'chkVAT
        '
        Me.chkVAT.AutoSize = True
        Me.chkVAT.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.chkVAT.Location = New System.Drawing.Point(409, 40)
        Me.chkVAT.Name = "chkVAT"
        Me.chkVAT.Size = New System.Drawing.Size(50, 20)
        Me.chkVAT.TabIndex = 1380
        Me.chkVAT.Text = "VAT"
        Me.chkVAT.UseVisualStyleBackColor = True
        Me.chkVAT.Visible = False
        '
        'frmTaxComputation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(495, 296)
        Me.Controls.Add(Me.chkVAT)
        Me.Controls.Add(Me.chkVATInc)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtEWTAmount)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtEWTRate)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cbEWTCode)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtVATAmount)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtVATRate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cbVATCode)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.txtNetAmount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtGrossAmount)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCompute)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmTaxComputation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tax Computation"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCompute As System.Windows.Forms.Button
    Friend WithEvents txtGrossAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNetAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbVATCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtVATRate As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtVATAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtEWTAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtEWTRate As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbEWTCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents chkVATInc As System.Windows.Forms.CheckBox
    Friend WithEvents chkVAT As System.Windows.Forms.CheckBox
End Class
