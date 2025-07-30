<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPDC_List
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.bgwDownload = New System.ComponentModel.BackgroundWorker()
        Me.bgwUpload = New System.ComponentModel.BackgroundWorker()
        Me.lblCounter = New System.Windows.Forms.Label()
        Me.dgcID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBank = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcCheckNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcVCecODE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcVCEName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcRemarks = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbNew, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(944, 40)
        Me.ToolStrip1.TabIndex = 1346
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbNew
        '
        Me.tsbNew.AutoSize = False
        Me.tsbNew.ForeColor = System.Drawing.Color.White
        Me.tsbNew.Image = Global.jade.My.Resources.Resources.circle_document_documents_extension_file_page_sheet_icon_7
        Me.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbNew.Name = "tsbNew"
        Me.tsbNew.Size = New System.Drawing.Size(50, 35)
        Me.tsbNew.Text = "New"
        Me.tsbNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        Me.tsbExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.AllowUserToResizeColumns = False
        Me.dgvList.AllowUserToResizeRows = False
        Me.dgvList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvList.BackgroundColor = System.Drawing.Color.White
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcID, Me.dgcCode, Me.dgcBank, Me.dgcCheckNo, Me.dgcAmount, Me.dgcVCecODE, Me.dgcVCEName, Me.dgcRemarks})
        Me.dgvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvList.Location = New System.Drawing.Point(6, 67)
        Me.dgvList.MultiSelect = False
        Me.dgvList.Name = "dgvList"
        Me.dgvList.RowHeadersVisible = False
        Me.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvList.Size = New System.Drawing.Size(926, 501)
        Me.dgvList.TabIndex = 1348
        '
        'bgwDownload
        '
        Me.bgwDownload.WorkerReportsProgress = True
        '
        'bgwUpload
        '
        Me.bgwUpload.WorkerReportsProgress = True
        '
        'lblCounter
        '
        Me.lblCounter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCounter.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.lblCounter.Location = New System.Drawing.Point(716, 43)
        Me.lblCounter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCounter.Name = "lblCounter"
        Me.lblCounter.Size = New System.Drawing.Size(216, 21)
        Me.lblCounter.TabIndex = 1364
        Me.lblCounter.Text = "Record Count : "
        Me.lblCounter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgcID
        '
        Me.dgcID.HeaderText = "ID"
        Me.dgcID.Name = "dgcID"
        Me.dgcID.Visible = False
        '
        'dgcCode
        '
        Me.dgcCode.HeaderText = "Date"
        Me.dgcCode.Name = "dgcCode"
        Me.dgcCode.ReadOnly = True
        Me.dgcCode.Width = 90
        '
        'dgcBank
        '
        Me.dgcBank.HeaderText = "Bank"
        Me.dgcBank.Name = "dgcBank"
        Me.dgcBank.ReadOnly = True
        Me.dgcBank.Width = 120
        '
        'dgcCheckNo
        '
        Me.dgcCheckNo.HeaderText = "Check Number"
        Me.dgcCheckNo.Name = "dgcCheckNo"
        Me.dgcCheckNo.ReadOnly = True
        Me.dgcCheckNo.Width = 150
        '
        'dgcAmount
        '
        DataGridViewCellStyle1.Format = "N2"
        DataGridViewCellStyle1.NullValue = "0"
        Me.dgcAmount.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgcAmount.HeaderText = "Amount"
        Me.dgcAmount.Name = "dgcAmount"
        Me.dgcAmount.ReadOnly = True
        Me.dgcAmount.Width = 120
        '
        'dgcVCecODE
        '
        Me.dgcVCecODE.HeaderText = "Client Code"
        Me.dgcVCecODE.Name = "dgcVCecODE"
        Me.dgcVCecODE.ReadOnly = True
        Me.dgcVCecODE.Visible = False
        '
        'dgcVCEName
        '
        Me.dgcVCEName.HeaderText = "Client Name"
        Me.dgcVCEName.Name = "dgcVCEName"
        Me.dgcVCEName.ReadOnly = True
        Me.dgcVCEName.Width = 250
        '
        'dgcRemarks
        '
        Me.dgcRemarks.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.dgcRemarks.HeaderText = "Remarks"
        Me.dgcRemarks.Name = "dgcRemarks"
        '
        'frmPDC_List
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(944, 580)
        Me.Controls.Add(Me.lblCounter)
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmPDC_List"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PDC List"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgvList As DataGridView
    Friend WithEvents tsbExit As ToolStripButton
    Friend WithEvents bgwDownload As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwUpload As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblCounter As Label
    Friend WithEvents dgcID As DataGridViewTextBoxColumn
    Friend WithEvents dgcCode As DataGridViewTextBoxColumn
    Friend WithEvents dgcBank As DataGridViewTextBoxColumn
    Friend WithEvents dgcCheckNo As DataGridViewTextBoxColumn
    Friend WithEvents dgcAmount As DataGridViewTextBoxColumn
    Friend WithEvents dgcVCecODE As DataGridViewTextBoxColumn
    Friend WithEvents dgcVCEName As DataGridViewTextBoxColumn
    Friend WithEvents dgcRemarks As DataGridViewTextBoxColumn
End Class
