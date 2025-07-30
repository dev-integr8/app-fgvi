<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPO_PartialPayment
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPO_PartialPayment))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.cbType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbDownPayment_Type = New System.Windows.Forms.ComboBox()
        Me.txtDownpayment_Amount = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDownpayment_Percent = New System.Windows.Forms.TextBox()
        Me.lblDownpaymen_Percent = New System.Windows.Forms.Label()
        Me.cbMethod = New System.Windows.Forms.ComboBox()
        Me.lblMethod = New System.Windows.Forms.Label()
        Me.dgvMonthly = New System.Windows.Forms.DataGridView()
        Me.dgcM_Terms = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcT_Amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tpMain = New System.Windows.Forms.TabPage()
        Me.txtNoOfMonths = New System.Windows.Forms.TextBox()
        Me.tpDownpayment = New System.Windows.Forms.TabPage()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvMonthly, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.tpMain.SuspendLayout()
        Me.tpDownpayment.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSave, Me.ToolStripSeparator1, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(622, 46)
        Me.ToolStrip1.TabIndex = 1307
        Me.ToolStrip1.Text = "ToolStrip1"
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
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 46)
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
        'cbType
        '
        Me.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbType.FormattingEnabled = True
        Me.cbType.Items.AddRange(New Object() {"Monthly", "Progressive"})
        Me.cbType.Location = New System.Drawing.Point(97, 9)
        Me.cbType.Name = "cbType"
        Me.cbType.Size = New System.Drawing.Size(162, 23)
        Me.cbType.TabIndex = 1308
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 15)
        Me.Label1.TabIndex = 1309
        Me.Label1.Text = "Type : "
        '
        'cbDownPayment_Type
        '
        Me.cbDownPayment_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDownPayment_Type.FormattingEnabled = True
        Me.cbDownPayment_Type.Items.AddRange(New Object() {"Amount", "Percent"})
        Me.cbDownPayment_Type.Location = New System.Drawing.Point(153, 15)
        Me.cbDownPayment_Type.Name = "cbDownPayment_Type"
        Me.cbDownPayment_Type.Size = New System.Drawing.Size(119, 23)
        Me.cbDownPayment_Type.TabIndex = 1310
        '
        'txtDownpayment_Amount
        '
        Me.txtDownpayment_Amount.Location = New System.Drawing.Point(153, 41)
        Me.txtDownpayment_Amount.Name = "txtDownpayment_Amount"
        Me.txtDownpayment_Amount.Size = New System.Drawing.Size(119, 21)
        Me.txtDownpayment_Amount.TabIndex = 1311
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(30, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(121, 15)
        Me.Label2.TabIndex = 1312
        Me.Label2.Text = "Downpayment Type :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(137, 15)
        Me.Label3.TabIndex = 1313
        Me.Label3.Text = "Downpayment Amount :"
        '
        'txtDownpayment_Percent
        '
        Me.txtDownpayment_Percent.Location = New System.Drawing.Point(278, 15)
        Me.txtDownpayment_Percent.Name = "txtDownpayment_Percent"
        Me.txtDownpayment_Percent.Size = New System.Drawing.Size(74, 21)
        Me.txtDownpayment_Percent.TabIndex = 1314
        '
        'lblDownpaymen_Percent
        '
        Me.lblDownpaymen_Percent.AutoSize = True
        Me.lblDownpaymen_Percent.Location = New System.Drawing.Point(358, 18)
        Me.lblDownpaymen_Percent.Name = "lblDownpaymen_Percent"
        Me.lblDownpaymen_Percent.Size = New System.Drawing.Size(18, 15)
        Me.lblDownpaymen_Percent.TabIndex = 1315
        Me.lblDownpaymen_Percent.Text = "%"
        '
        'cbMethod
        '
        Me.cbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMethod.FormattingEnabled = True
        Me.cbMethod.Items.AddRange(New Object() {"Amount", "Percent"})
        Me.cbMethod.Location = New System.Drawing.Point(97, 38)
        Me.cbMethod.Name = "cbMethod"
        Me.cbMethod.Size = New System.Drawing.Size(162, 23)
        Me.cbMethod.TabIndex = 1318
        '
        'lblMethod
        '
        Me.lblMethod.AutoSize = True
        Me.lblMethod.Location = New System.Drawing.Point(5, 41)
        Me.lblMethod.Name = "lblMethod"
        Me.lblMethod.Size = New System.Drawing.Size(58, 15)
        Me.lblMethod.TabIndex = 1319
        Me.lblMethod.Text = "Method : "
        '
        'dgvMonthly
        '
        Me.dgvMonthly.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMonthly.BackgroundColor = System.Drawing.Color.Gainsboro
        Me.dgvMonthly.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMonthly.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcM_Terms, Me.dgcT_Amount})
        Me.dgvMonthly.Location = New System.Drawing.Point(12, 155)
        Me.dgvMonthly.Name = "dgvMonthly"
        Me.dgvMonthly.Size = New System.Drawing.Size(598, 185)
        Me.dgvMonthly.TabIndex = 1
        '
        'dgcM_Terms
        '
        Me.dgcM_Terms.HeaderText = "Terms"
        Me.dgcM_Terms.Name = "dgcM_Terms"
        Me.dgcM_Terms.Width = 200
        '
        'dgcT_Amount
        '
        Me.dgcT_Amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N2"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.dgcT_Amount.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgcT_Amount.HeaderText = "Amount"
        Me.dgcT_Amount.Name = "dgcT_Amount"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tpMain)
        Me.TabControl1.Controls.Add(Me.tpDownpayment)
        Me.TabControl1.Location = New System.Drawing.Point(12, 49)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(598, 100)
        Me.TabControl1.TabIndex = 1320
        '
        'tpMain
        '
        Me.tpMain.Controls.Add(Me.txtNoOfMonths)
        Me.tpMain.Controls.Add(Me.cbType)
        Me.tpMain.Controls.Add(Me.Label1)
        Me.tpMain.Controls.Add(Me.lblMethod)
        Me.tpMain.Controls.Add(Me.cbMethod)
        Me.tpMain.Location = New System.Drawing.Point(4, 24)
        Me.tpMain.Name = "tpMain"
        Me.tpMain.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMain.Size = New System.Drawing.Size(590, 72)
        Me.tpMain.TabIndex = 0
        Me.tpMain.Text = "Partial"
        Me.tpMain.UseVisualStyleBackColor = True
        '
        'txtNoOfMonths
        '
        Me.txtNoOfMonths.Location = New System.Drawing.Point(97, 38)
        Me.txtNoOfMonths.Name = "txtNoOfMonths"
        Me.txtNoOfMonths.Size = New System.Drawing.Size(162, 21)
        Me.txtNoOfMonths.TabIndex = 1320
        '
        'tpDownpayment
        '
        Me.tpDownpayment.Controls.Add(Me.txtDownpayment_Amount)
        Me.tpDownpayment.Controls.Add(Me.cbDownPayment_Type)
        Me.tpDownpayment.Controls.Add(Me.Label2)
        Me.tpDownpayment.Controls.Add(Me.lblDownpaymen_Percent)
        Me.tpDownpayment.Controls.Add(Me.Label3)
        Me.tpDownpayment.Controls.Add(Me.txtDownpayment_Percent)
        Me.tpDownpayment.Location = New System.Drawing.Point(4, 24)
        Me.tpDownpayment.Name = "tpDownpayment"
        Me.tpDownpayment.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDownpayment.Size = New System.Drawing.Size(590, 72)
        Me.tpDownpayment.TabIndex = 1
        Me.tpDownpayment.Text = "Downpayment"
        Me.tpDownpayment.UseVisualStyleBackColor = True
        '
        'frmPO_PartialPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(622, 343)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.dgvMonthly)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(638, 382)
        Me.MinimumSize = New System.Drawing.Size(638, 382)
        Me.Name = "frmPO_PartialPayment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Partial Payment"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvMonthly, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.tpMain.ResumeLayout(False)
        Me.tpMain.PerformLayout()
        Me.tpDownpayment.ResumeLayout(False)
        Me.tpDownpayment.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents cbType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbDownPayment_Type As System.Windows.Forms.ComboBox
    Friend WithEvents txtDownpayment_Amount As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDownpayment_Percent As System.Windows.Forms.TextBox
    Friend WithEvents lblDownpaymen_Percent As System.Windows.Forms.Label
    Friend WithEvents cbMethod As System.Windows.Forms.ComboBox
    Friend WithEvents lblMethod As System.Windows.Forms.Label
    Friend WithEvents dgvMonthly As System.Windows.Forms.DataGridView
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tpMain As System.Windows.Forms.TabPage
    Friend WithEvents tpDownpayment As System.Windows.Forms.TabPage
    Friend WithEvents txtNoOfMonths As System.Windows.Forms.TextBox
    Friend WithEvents dgcM_Terms As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcT_Amount As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
