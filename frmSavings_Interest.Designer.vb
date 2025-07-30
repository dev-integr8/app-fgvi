<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSavings_Interest
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSavings_Interest))
        Me.cmbSavingsType = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PostToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmbMonth = New System.Windows.Forms.ComboBox()
        Me.nudYear = New System.Windows.Forms.NumericUpDown()
        Me.dtpStart = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtpEnd = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvInterest = New System.Windows.Forms.DataGridView()
        Me.chVCECode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chVCEName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chAccountType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chAccountNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chDays = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chDailyBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chADB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chIntRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chInterest = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chPrincipal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chSavings = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnGenerateInterest = New System.Windows.Forms.Button()
        Me.txtTotalInterest = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbBranch = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.nudYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvInterest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbSavingsType
        '
        Me.cmbSavingsType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSavingsType.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cmbSavingsType.FormattingEnabled = True
        Me.cmbSavingsType.Location = New System.Drawing.Point(115, 27)
        Me.cmbSavingsType.Name = "cmbSavingsType"
        Me.cmbSavingsType.Size = New System.Drawing.Size(249, 23)
        Me.cmbSavingsType.TabIndex = 1412
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(15, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 15)
        Me.Label4.TabIndex = 1411
        Me.Label4.Text = "Savings Type :"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem})
        Me.MenuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1078, 24)
        Me.MenuStrip1.TabIndex = 1413
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem, Me.PostToolStripMenuItem})
        Me.EditToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.EditToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'PostToolStripMenuItem
        '
        Me.PostToolStripMenuItem.Name = "PostToolStripMenuItem"
        Me.PostToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.PostToolStripMenuItem.Text = "Copy to JV"
        '
        'cmbMonth
        '
        Me.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMonth.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cmbMonth.FormattingEnabled = True
        Me.cmbMonth.Items.AddRange(New Object() {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"})
        Me.cmbMonth.Location = New System.Drawing.Point(115, 52)
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(156, 23)
        Me.cmbMonth.TabIndex = 1412
        '
        'nudYear
        '
        Me.nudYear.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.nudYear.Location = New System.Drawing.Point(273, 52)
        Me.nudYear.Maximum = New Decimal(New Integer() {2999, 0, 0, 0})
        Me.nudYear.Minimum = New Decimal(New Integer() {2019, 0, 0, 0})
        Me.nudYear.Name = "nudYear"
        Me.nudYear.Size = New System.Drawing.Size(91, 23)
        Me.nudYear.TabIndex = 1414
        Me.nudYear.Value = New Decimal(New Integer() {2019, 0, 0, 0})
        '
        'dtpStart
        '
        Me.dtpStart.Enabled = False
        Me.dtpStart.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStart.Location = New System.Drawing.Point(115, 77)
        Me.dtpStart.Name = "dtpStart"
        Me.dtpStart.Size = New System.Drawing.Size(123, 23)
        Me.dtpStart.TabIndex = 1422
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(15, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 15)
        Me.Label9.TabIndex = 1421
        Me.Label9.Text = "Period :"
        '
        'dtpEnd
        '
        Me.dtpEnd.Enabled = False
        Me.dtpEnd.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEnd.Location = New System.Drawing.Point(241, 77)
        Me.dtpEnd.Name = "dtpEnd"
        Me.dtpEnd.Size = New System.Drawing.Size(123, 23)
        Me.dtpEnd.TabIndex = 1423
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 83)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 15)
        Me.Label1.TabIndex = 1421
        Me.Label1.Text = "Period Covered :"
        '
        'dgvInterest
        '
        Me.dgvInterest.AllowUserToAddRows = False
        Me.dgvInterest.AllowUserToDeleteRows = False
        Me.dgvInterest.AllowUserToResizeColumns = False
        Me.dgvInterest.AllowUserToResizeRows = False
        Me.dgvInterest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInterest.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chVCECode, Me.chVCEName, Me.chAccountType, Me.chAccountNumber, Me.chBalance, Me.chDays, Me.chDailyBalance, Me.chADB, Me.chIntRate, Me.chInterest, Me.chPrincipal, Me.chSavings})
        Me.dgvInterest.Location = New System.Drawing.Point(13, 102)
        Me.dgvInterest.MultiSelect = False
        Me.dgvInterest.Name = "dgvInterest"
        Me.dgvInterest.ReadOnly = True
        Me.dgvInterest.RowHeadersVisible = False
        Me.dgvInterest.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvInterest.Size = New System.Drawing.Size(1053, 411)
        Me.dgvInterest.TabIndex = 1424
        '
        'chVCECode
        '
        Me.chVCECode.HeaderText = "VCECode"
        Me.chVCECode.Name = "chVCECode"
        Me.chVCECode.ReadOnly = True
        Me.chVCECode.Width = 70
        '
        'chVCEName
        '
        Me.chVCEName.HeaderText = "VCEName"
        Me.chVCEName.Name = "chVCEName"
        Me.chVCEName.ReadOnly = True
        Me.chVCEName.Width = 190
        '
        'chAccountType
        '
        Me.chAccountType.HeaderText = "AccountType"
        Me.chAccountType.Name = "chAccountType"
        Me.chAccountType.ReadOnly = True
        Me.chAccountType.Visible = False
        '
        'chAccountNumber
        '
        Me.chAccountNumber.HeaderText = "Accnt No."
        Me.chAccountNumber.Name = "chAccountNumber"
        Me.chAccountNumber.ReadOnly = True
        '
        'chBalance
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.chBalance.DefaultCellStyle = DataGridViewCellStyle1
        Me.chBalance.HeaderText = "Balance"
        Me.chBalance.Name = "chBalance"
        Me.chBalance.ReadOnly = True
        '
        'chDays
        '
        Me.chDays.HeaderText = "Days"
        Me.chDays.Name = "chDays"
        Me.chDays.ReadOnly = True
        Me.chDays.Width = 50
        '
        'chDailyBalance
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.chDailyBalance.DefaultCellStyle = DataGridViewCellStyle2
        Me.chDailyBalance.HeaderText = "Daily Balance"
        Me.chDailyBalance.Name = "chDailyBalance"
        Me.chDailyBalance.ReadOnly = True
        '
        'chADB
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.chADB.DefaultCellStyle = DataGridViewCellStyle3
        Me.chADB.HeaderText = "ADB"
        Me.chADB.Name = "chADB"
        Me.chADB.ReadOnly = True
        '
        'chIntRate
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.chIntRate.DefaultCellStyle = DataGridViewCellStyle4
        Me.chIntRate.HeaderText = "Int. Rate"
        Me.chIntRate.Name = "chIntRate"
        Me.chIntRate.ReadOnly = True
        Me.chIntRate.Width = 80
        '
        'chInterest
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.chInterest.DefaultCellStyle = DataGridViewCellStyle5
        Me.chInterest.HeaderText = "Interest"
        Me.chInterest.Name = "chInterest"
        Me.chInterest.ReadOnly = True
        Me.chInterest.Width = 80
        '
        'chPrincipal
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.chPrincipal.DefaultCellStyle = DataGridViewCellStyle6
        Me.chPrincipal.HeaderText = "Principal"
        Me.chPrincipal.Name = "chPrincipal"
        Me.chPrincipal.ReadOnly = True
        '
        'chSavings
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.chSavings.DefaultCellStyle = DataGridViewCellStyle7
        Me.chSavings.HeaderText = "Savings"
        Me.chSavings.Name = "chSavings"
        Me.chSavings.ReadOnly = True
        '
        'btnGenerateInterest
        '
        Me.btnGenerateInterest.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenerateInterest.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnGenerateInterest.Location = New System.Drawing.Point(13, 519)
        Me.btnGenerateInterest.Name = "btnGenerateInterest"
        Me.btnGenerateInterest.Size = New System.Drawing.Size(150, 35)
        Me.btnGenerateInterest.TabIndex = 1425
        Me.btnGenerateInterest.Text = "Generate Interest"
        Me.btnGenerateInterest.UseVisualStyleBackColor = True
        '
        'txtTotalInterest
        '
        Me.txtTotalInterest.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtTotalInterest.Location = New System.Drawing.Point(942, 519)
        Me.txtTotalInterest.Name = "txtTotalInterest"
        Me.txtTotalInterest.ReadOnly = True
        Me.txtTotalInterest.Size = New System.Drawing.Size(124, 23)
        Me.txtTotalInterest.TabIndex = 1427
        Me.txtTotalInterest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(855, 522)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 15)
        Me.Label6.TabIndex = 1426
        Me.Label6.Text = "Total Interest :"
        '
        'cbBranch
        '
        Me.cbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBranch.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cbBranch.FormattingEnabled = True
        Me.cbBranch.Location = New System.Drawing.Point(432, 30)
        Me.cbBranch.Name = "cbBranch"
        Me.cbBranch.Size = New System.Drawing.Size(249, 23)
        Me.cbBranch.TabIndex = 1429
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(372, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 15)
        Me.Label2.TabIndex = 1428
        Me.Label2.Text = "Branch :"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Location = New System.Drawing.Point(170, 519)
        Me.btnExport.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(111, 35)
        Me.btnExport.TabIndex = 1430
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'frmSavings_Interest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1078, 561)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.cbBranch)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtTotalInterest)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnGenerateInterest)
        Me.Controls.Add(Me.dgvInterest)
        Me.Controls.Add(Me.dtpEnd)
        Me.Controls.Add(Me.dtpStart)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.nudYear)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.cmbMonth)
        Me.Controls.Add(Me.cmbSavingsType)
        Me.Controls.Add(Me.Label4)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(950, 600)
        Me.Name = "frmSavings_Interest"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generate Interest"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.nudYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvInterest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbSavingsType As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents cmbMonth As System.Windows.Forms.ComboBox
    Friend WithEvents nudYear As System.Windows.Forms.NumericUpDown
    Friend WithEvents dtpStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtpEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvInterest As System.Windows.Forms.DataGridView
    Friend WithEvents btnGenerateInterest As System.Windows.Forms.Button
    Friend WithEvents txtTotalInterest As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PostToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cbBranch As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents chVCECode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chVCEName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chAccountType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chAccountNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chBalance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chDays As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chDailyBalance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chADB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chIntRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chInterest As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chPrincipal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chSavings As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
