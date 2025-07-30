<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRE_Ledger
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRE_Ledger))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.dgvLedger = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ToolStripSplitButton1 = New System.Windows.Forms.ToolStripSplitButton()
        Me.dgcNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDateDue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcPrincipal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcInterest = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcPenalty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBalancePrev = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcTotalDue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcAmountPaid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcAppliedPrincipal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcAppliedInterest = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcAppliedPenalty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcPrincipalBalanace = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcInterestBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcPenaltyBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcDatePaid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcRef = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvLedger, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnPreview)
        Me.GroupBox1.Controls.Add(Me.dgvLedger)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1191, 653)
        Me.GroupBox1.TabIndex = 1319
        Me.GroupBox1.TabStop = False
        '
        'btnPreview
        '
        Me.btnPreview.Location = New System.Drawing.Point(1066, 20)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(119, 42)
        Me.btnPreview.TabIndex = 1565
        Me.btnPreview.Text = "Report Preview"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'dgvLedger
        '
        Me.dgvLedger.AllowUserToAddRows = False
        Me.dgvLedger.AllowUserToDeleteRows = False
        Me.dgvLedger.AllowUserToResizeRows = False
        Me.dgvLedger.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvLedger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLedger.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcNum, Me.dgcDesc, Me.dgcDateDue, Me.dgcPrincipal, Me.dgcInterest, Me.dgcPenalty, Me.dgcBalancePrev, Me.dgcTotalDue, Me.dgcAmountPaid, Me.dgcAppliedPrincipal, Me.dgcAppliedInterest, Me.dgcAppliedPenalty, Me.dgcBalance, Me.dgcPrincipalBalanace, Me.dgcInterestBalance, Me.dgcPenaltyBalance, Me.dgcDatePaid, Me.dgcRef})
        Me.dgvLedger.Location = New System.Drawing.Point(6, 77)
        Me.dgvLedger.Name = "dgvLedger"
        Me.dgvLedger.ReadOnly = True
        Me.dgvLedger.RowHeadersVisible = False
        Me.dgvLedger.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLedger.Size = New System.Drawing.Size(1179, 570)
        Me.dgvLedger.TabIndex = 1564
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label2.Location = New System.Drawing.Point(135, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 16)
        Me.Label2.TabIndex = 1521
        Me.Label2.Text = "Customer Name :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label4.Location = New System.Drawing.Point(22, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 16)
        Me.Label4.TabIndex = 1522
        Me.Label4.Text = "Customer Code :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label1.Location = New System.Drawing.Point(135, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 16)
        Me.Label1.TabIndex = 1308
        Me.Label1.Text = "Description :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label3.Location = New System.Drawing.Point(28, 43)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 16)
        Me.Label3.TabIndex = 1309
        Me.Label3.Text = "Property Code :"
        '
        'ToolStripSplitButton1
        '
        Me.ToolStripSplitButton1.Name = "ToolStripSplitButton1"
        Me.ToolStripSplitButton1.Size = New System.Drawing.Size(23, 23)
        '
        'dgcNum
        '
        Me.dgcNum.Frozen = True
        Me.dgcNum.HeaderText = "#"
        Me.dgcNum.Name = "dgcNum"
        Me.dgcNum.ReadOnly = True
        Me.dgcNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.dgcNum.Width = 50
        '
        'dgcDesc
        '
        Me.dgcDesc.Frozen = True
        Me.dgcDesc.HeaderText = "Description"
        Me.dgcDesc.Name = "dgcDesc"
        Me.dgcDesc.ReadOnly = True
        Me.dgcDesc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.dgcDesc.Width = 120
        '
        'dgcDateDue
        '
        Me.dgcDateDue.Frozen = True
        Me.dgcDateDue.HeaderText = "Date Due"
        Me.dgcDateDue.Name = "dgcDateDue"
        Me.dgcDateDue.ReadOnly = True
        Me.dgcDateDue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'dgcPrincipal
        '
        Me.dgcPrincipal.Frozen = True
        Me.dgcPrincipal.HeaderText = "Principal"
        Me.dgcPrincipal.Name = "dgcPrincipal"
        Me.dgcPrincipal.ReadOnly = True
        Me.dgcPrincipal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'dgcInterest
        '
        Me.dgcInterest.Frozen = True
        Me.dgcInterest.HeaderText = "Interest"
        Me.dgcInterest.Name = "dgcInterest"
        Me.dgcInterest.ReadOnly = True
        Me.dgcInterest.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'dgcPenalty
        '
        Me.dgcPenalty.Frozen = True
        Me.dgcPenalty.HeaderText = "Penalty"
        Me.dgcPenalty.Name = "dgcPenalty"
        Me.dgcPenalty.ReadOnly = True
        Me.dgcPenalty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'dgcBalancePrev
        '
        Me.dgcBalancePrev.Frozen = True
        Me.dgcBalancePrev.HeaderText = "Balance (Prev. Months)"
        Me.dgcBalancePrev.Name = "dgcBalancePrev"
        Me.dgcBalancePrev.ReadOnly = True
        Me.dgcBalancePrev.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'dgcTotalDue
        '
        Me.dgcTotalDue.Frozen = True
        Me.dgcTotalDue.HeaderText = "Total Due"
        Me.dgcTotalDue.Name = "dgcTotalDue"
        Me.dgcTotalDue.ReadOnly = True
        Me.dgcTotalDue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'dgcAmountPaid
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgcAmountPaid.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgcAmountPaid.HeaderText = "Amount Paid"
        Me.dgcAmountPaid.Name = "dgcAmountPaid"
        Me.dgcAmountPaid.ReadOnly = True
        Me.dgcAmountPaid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'dgcAppliedPrincipal
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgcAppliedPrincipal.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgcAppliedPrincipal.HeaderText = "Applied Principal"
        Me.dgcAppliedPrincipal.Name = "dgcAppliedPrincipal"
        Me.dgcAppliedPrincipal.ReadOnly = True
        Me.dgcAppliedPrincipal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'dgcAppliedInterest
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgcAppliedInterest.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgcAppliedInterest.HeaderText = "Applied Interest"
        Me.dgcAppliedInterest.Name = "dgcAppliedInterest"
        Me.dgcAppliedInterest.ReadOnly = True
        Me.dgcAppliedInterest.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'dgcAppliedPenalty
        '
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgcAppliedPenalty.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgcAppliedPenalty.HeaderText = "Applied Penalty"
        Me.dgcAppliedPenalty.Name = "dgcAppliedPenalty"
        Me.dgcAppliedPenalty.ReadOnly = True
        Me.dgcAppliedPenalty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'dgcBalance
        '
        Me.dgcBalance.HeaderText = "Balance"
        Me.dgcBalance.Name = "dgcBalance"
        Me.dgcBalance.ReadOnly = True
        Me.dgcBalance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'dgcPrincipalBalanace
        '
        Me.dgcPrincipalBalanace.HeaderText = "Principal Balance"
        Me.dgcPrincipalBalanace.Name = "dgcPrincipalBalanace"
        Me.dgcPrincipalBalanace.ReadOnly = True
        Me.dgcPrincipalBalanace.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'dgcInterestBalance
        '
        Me.dgcInterestBalance.HeaderText = "Interest Balance"
        Me.dgcInterestBalance.Name = "dgcInterestBalance"
        Me.dgcInterestBalance.ReadOnly = True
        Me.dgcInterestBalance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'dgcPenaltyBalance
        '
        Me.dgcPenaltyBalance.HeaderText = "Penalty Balance"
        Me.dgcPenaltyBalance.Name = "dgcPenaltyBalance"
        Me.dgcPenaltyBalance.ReadOnly = True
        Me.dgcPenaltyBalance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'dgcDatePaid
        '
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgcDatePaid.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgcDatePaid.HeaderText = "DatePaid"
        Me.dgcDatePaid.Name = "dgcDatePaid"
        Me.dgcDatePaid.ReadOnly = True
        Me.dgcDatePaid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'dgcRef
        '
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgcRef.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgcRef.HeaderText = "Reference"
        Me.dgcRef.Name = "dgcRef"
        Me.dgcRef.ReadOnly = True
        Me.dgcRef.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.dgcRef.Width = 125
        '
        'frmRE_Ledger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1217, 678)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmRE_Ledger"
        Me.Text = "Buyer's Ledger"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvLedger, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents ToolStripSplitButton1 As ToolStripSplitButton
    Friend WithEvents dgvLedger As DataGridView
    Friend WithEvents btnPreview As Button
    Friend WithEvents dgcNum As DataGridViewTextBoxColumn
    Friend WithEvents dgcDesc As DataGridViewTextBoxColumn
    Friend WithEvents dgcDateDue As DataGridViewTextBoxColumn
    Friend WithEvents dgcPrincipal As DataGridViewTextBoxColumn
    Friend WithEvents dgcInterest As DataGridViewTextBoxColumn
    Friend WithEvents dgcPenalty As DataGridViewTextBoxColumn
    Friend WithEvents dgcBalancePrev As DataGridViewTextBoxColumn
    Friend WithEvents dgcTotalDue As DataGridViewTextBoxColumn
    Friend WithEvents dgcAmountPaid As DataGridViewTextBoxColumn
    Friend WithEvents dgcAppliedPrincipal As DataGridViewTextBoxColumn
    Friend WithEvents dgcAppliedInterest As DataGridViewTextBoxColumn
    Friend WithEvents dgcAppliedPenalty As DataGridViewTextBoxColumn
    Friend WithEvents dgcBalance As DataGridViewTextBoxColumn
    Friend WithEvents dgcPrincipalBalanace As DataGridViewTextBoxColumn
    Friend WithEvents dgcInterestBalance As DataGridViewTextBoxColumn
    Friend WithEvents dgcPenaltyBalance As DataGridViewTextBoxColumn
    Friend WithEvents dgcDatePaid As DataGridViewTextBoxColumn
    Friend WithEvents dgcRef As DataGridViewTextBoxColumn
End Class
