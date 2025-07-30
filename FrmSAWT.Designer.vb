<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSAWT
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
        Me.lvSAWT = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chReportMonth = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chTIN = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chBranchCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chCompanyName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chSurname = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chFirstName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chMiddleName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chATC = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chIncPayment = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chEWTrate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chTax = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.dgvData = New System.Windows.Forms.DataGridView()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.lblTitle = New System.Windows.Forms.Label()
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lvSAWT
        '
        Me.lvSAWT.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvSAWT.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.chReportMonth, Me.chTIN, Me.chBranchCode, Me.chCompanyName, Me.chSurname, Me.chFirstName, Me.chMiddleName, Me.chATC, Me.chIncPayment, Me.chEWTrate, Me.chTax})
        Me.lvSAWT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvSAWT.FullRowSelect = True
        Me.lvSAWT.GridLines = True
        Me.lvSAWT.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvSAWT.Location = New System.Drawing.Point(7, 278)
        Me.lvSAWT.MultiSelect = False
        Me.lvSAWT.Name = "lvSAWT"
        Me.lvSAWT.Size = New System.Drawing.Size(1099, 0)
        Me.lvSAWT.TabIndex = 1345
        Me.lvSAWT.UseCompatibleStateImageBehavior = False
        Me.lvSAWT.View = System.Windows.Forms.View.Details
        Me.lvSAWT.Visible = False
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Width = 0
        '
        'chReportMonth
        '
        Me.chReportMonth.Text = "Reporting Month"
        Me.chReportMonth.Width = 110
        '
        'chTIN
        '
        Me.chTIN.Text = "TIN No"
        Me.chTIN.Width = 80
        '
        'chBranchCode
        '
        Me.chBranchCode.Text = "Branch Code"
        Me.chBranchCode.Width = 80
        '
        'chCompanyName
        '
        Me.chCompanyName.Text = "Company Name"
        Me.chCompanyName.Width = 180
        '
        'chSurname
        '
        Me.chSurname.Text = "Surname"
        Me.chSurname.Width = 100
        '
        'chFirstName
        '
        Me.chFirstName.Text = "First Name"
        Me.chFirstName.Width = 100
        '
        'chMiddleName
        '
        Me.chMiddleName.Text = "Middle Name"
        Me.chMiddleName.Width = 100
        '
        'chATC
        '
        Me.chATC.Text = "ATC"
        '
        'chIncPayment
        '
        Me.chIncPayment.Text = "Income Payment"
        Me.chIncPayment.Width = 80
        '
        'chEWTrate
        '
        Me.chEWTrate.Text = "EWT Rate"
        '
        'chTax
        '
        Me.chTax.Text = "Tax Amount"
        Me.chTax.Width = 90
        '
        'dgvData
        '
        Me.dgvData.AllowUserToAddRows = False
        Me.dgvData.AllowUserToDeleteRows = False
        Me.dgvData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvData.Location = New System.Drawing.Point(7, 35)
        Me.dgvData.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvData.Name = "dgvData"
        Me.dgvData.ReadOnly = True
        Me.dgvData.RowHeadersVisible = False
        Me.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvData.Size = New System.Drawing.Size(1104, 144)
        Me.dgvData.TabIndex = 1347
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Location = New System.Drawing.Point(946, 187)
        Me.btnExport.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(165, 32)
        Me.btnExport.TabIndex = 1348
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(4, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(337, 18)
        Me.lblTitle.TabIndex = 1349
        Me.lblTitle.Text = "Summary Alphalist of Withholding Taxes(SAWT)"
        '
        'FrmSAWT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1118, 222)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.dgvData)
        Me.Controls.Add(Me.lvSAWT)
        Me.Name = "FrmSAWT"
        Me.Text = "Tax Reports"
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lvSAWT As System.Windows.Forms.ListView
    Friend WithEvents chReportMonth As System.Windows.Forms.ColumnHeader
    Friend WithEvents chTIN As System.Windows.Forms.ColumnHeader
    Friend WithEvents chBranchCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents chCompanyName As System.Windows.Forms.ColumnHeader
    Friend WithEvents chSurname As System.Windows.Forms.ColumnHeader
    Friend WithEvents chFirstName As System.Windows.Forms.ColumnHeader
    Friend WithEvents chMiddleName As System.Windows.Forms.ColumnHeader
    Friend WithEvents chATC As System.Windows.Forms.ColumnHeader
    Friend WithEvents chIncPayment As System.Windows.Forms.ColumnHeader
    Friend WithEvents chEWTrate As System.Windows.Forms.ColumnHeader
    Friend WithEvents chTax As System.Windows.Forms.ColumnHeader
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents dgvData As System.Windows.Forms.DataGridView
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents lblTitle As System.Windows.Forms.Label
End Class
