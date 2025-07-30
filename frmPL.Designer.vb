<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPL
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPL))
        Me.dgvItemList = New System.Windows.Forms.DataGridView()
        Me.dgcLine = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcItemCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcItemDesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcUOM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcReqQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcAllocatedQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcForAllocation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcPickTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgclineSO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtSORef = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpActual = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnSearchVCE = New System.Windows.Forms.Button()
        Me.txtVCEName = New System.Windows.Forms.TextBox()
        Me.txtVCECode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpDocDate = New System.Windows.Forms.DateTimePicker()
        Me.txtTransNum = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbSearch = New System.Windows.Forms.ToolStripButton()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbCopy = New System.Windows.Forms.ToolStripDropDownButton()
        Me.tsbCopyPO = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrint = New System.Windows.Forms.ToolStripButton()
        Me.tsbReports = New System.Windows.Forms.ToolStripDropDownButton()
        Me.TestToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrevious = New System.Windows.Forms.ToolStripButton()
        Me.tsbNext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.dgvPick = New System.Windows.Forms.DataGridView()
        Me.dgvPickAll = New System.Windows.Forms.DataGridView()
        Me.dgcItemCodeAll = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcWHSEall = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcStockAll = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvPickList = New System.Windows.Forms.DataGridView()
        Me.dgcItemCodeList = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcWHSEList = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcPickList = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcLineList = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcPick = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgcWHSE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcStockQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcPickQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcPickActual = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvPick, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvPickAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvPickList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvItemList
        '
        Me.dgvItemList.AllowUserToAddRows = False
        Me.dgvItemList.AllowUserToDeleteRows = False
        Me.dgvItemList.AllowUserToResizeRows = False
        Me.dgvItemList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvItemList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcLine, Me.dgcItemCode, Me.dgcItemDesc, Me.dgcUOM, Me.dgcReqQTY, Me.dgcAllocatedQTY, Me.dgcForAllocation, Me.dgcPickTotal, Me.dgclineSO})
        Me.dgvItemList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvItemList.Location = New System.Drawing.Point(3, 199)
        Me.dgvItemList.MultiSelect = False
        Me.dgvItemList.Name = "dgvItemList"
        Me.dgvItemList.RowHeadersWidth = 25
        Me.dgvItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvItemList.Size = New System.Drawing.Size(629, 361)
        Me.dgvItemList.TabIndex = 1297
        '
        'dgcLine
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        Me.dgcLine.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgcLine.Frozen = True
        Me.dgcLine.HeaderText = "#"
        Me.dgcLine.Name = "dgcLine"
        Me.dgcLine.ReadOnly = True
        Me.dgcLine.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.dgcLine.Width = 30
        '
        'dgcItemCode
        '
        Me.dgcItemCode.HeaderText = "ItemCode"
        Me.dgcItemCode.Name = "dgcItemCode"
        '
        'dgcItemDesc
        '
        Me.dgcItemDesc.HeaderText = "Item Description"
        Me.dgcItemDesc.Name = "dgcItemDesc"
        Me.dgcItemDesc.Width = 250
        '
        'dgcUOM
        '
        Me.dgcUOM.HeaderText = "UOM"
        Me.dgcUOM.Name = "dgcUOM"
        Me.dgcUOM.ReadOnly = True
        Me.dgcUOM.Width = 60
        '
        'dgcReqQTY
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = "0.00"
        Me.dgcReqQTY.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgcReqQTY.HeaderText = "Req. QTY"
        Me.dgcReqQTY.Name = "dgcReqQTY"
        Me.dgcReqQTY.ReadOnly = True
        Me.dgcReqQTY.Width = 90
        '
        'dgcAllocatedQTY
        '
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = "0.00"
        Me.dgcAllocatedQTY.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgcAllocatedQTY.HeaderText = "Allocated QTY"
        Me.dgcAllocatedQTY.Name = "dgcAllocatedQTY"
        Me.dgcAllocatedQTY.ReadOnly = True
        Me.dgcAllocatedQTY.Visible = False
        '
        'dgcForAllocation
        '
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = "0.00"
        Me.dgcForAllocation.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgcForAllocation.HeaderText = "For Allocation"
        Me.dgcForAllocation.Name = "dgcForAllocation"
        Me.dgcForAllocation.ReadOnly = True
        Me.dgcForAllocation.Visible = False
        '
        'dgcPickTotal
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = "0.00"
        Me.dgcPickTotal.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgcPickTotal.HeaderText = "Pick QTY"
        Me.dgcPickTotal.Name = "dgcPickTotal"
        Me.dgcPickTotal.ReadOnly = True
        '
        'dgclineSO
        '
        Me.dgclineSO.HeaderText = "LineSO"
        Me.dgclineSO.Name = "dgclineSO"
        Me.dgclineSO.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtSORef)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtStatus)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpActual)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.btnSearchVCE)
        Me.GroupBox1.Controls.Add(Me.txtVCEName)
        Me.GroupBox1.Controls.Add(Me.txtVCECode)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.dtpDocDate)
        Me.GroupBox1.Controls.Add(Me.txtTransNum)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 43)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(960, 153)
        Me.GroupBox1.TabIndex = 1302
        Me.GroupBox1.TabStop = False
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(757, 87)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 16)
        Me.Label6.TabIndex = 1319
        Me.Label6.Text = "SO Ref. :"
        '
        'txtSORef
        '
        Me.txtSORef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSORef.Enabled = False
        Me.txtSORef.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtSORef.Location = New System.Drawing.Point(822, 84)
        Me.txtSORef.Name = "txtSORef"
        Me.txtSORef.Size = New System.Drawing.Size(132, 22)
        Me.txtSORef.TabIndex = 1320
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(765, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 16)
        Me.Label4.TabIndex = 1317
        Me.Label4.Text = "Status :"
        '
        'txtStatus
        '
        Me.txtStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtStatus.Enabled = False
        Me.txtStatus.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(822, 60)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(132, 22)
        Me.txtStatus.TabIndex = 1318
        Me.txtStatus.Text = "Open"
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(112, 89)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(338, 51)
        Me.txtRemarks.TabIndex = 1316
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(38, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 16)
        Me.Label2.TabIndex = 1315
        Me.Label2.Text = "Remarks :"
        '
        'dtpActual
        '
        Me.dtpActual.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.dtpActual.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpActual.Location = New System.Drawing.Point(112, 63)
        Me.dtpActual.Name = "dtpActual"
        Me.dtpActual.Size = New System.Drawing.Size(132, 22)
        Me.dtpActual.TabIndex = 1314
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(14, 67)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(92, 16)
        Me.Label9.TabIndex = 1313
        Me.Label9.Text = "Delivery Date :"
        '
        'btnSearchVCE
        '
        Me.btnSearchVCE.BackgroundImage = Global.jade.My.Resources.Resources.view
        Me.btnSearchVCE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchVCE.Location = New System.Drawing.Point(456, 12)
        Me.btnSearchVCE.Name = "btnSearchVCE"
        Me.btnSearchVCE.Size = New System.Drawing.Size(25, 25)
        Me.btnSearchVCE.TabIndex = 1312
        Me.btnSearchVCE.UseVisualStyleBackColor = True
        '
        'txtVCEName
        '
        Me.txtVCEName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVCEName.Location = New System.Drawing.Point(112, 38)
        Me.txtVCEName.Name = "txtVCEName"
        Me.txtVCEName.Size = New System.Drawing.Size(338, 22)
        Me.txtVCEName.TabIndex = 1311
        '
        'txtVCECode
        '
        Me.txtVCECode.Enabled = False
        Me.txtVCECode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVCECode.Location = New System.Drawing.Point(112, 13)
        Me.txtVCECode.Name = "txtVCECode"
        Me.txtVCECode.Size = New System.Drawing.Size(338, 22)
        Me.txtVCECode.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(2, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 16)
        Me.Label1.TabIndex = 1308
        Me.Label1.Text = "Customer Name :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 16)
        Me.Label3.TabIndex = 1309
        Me.Label3.Text = "Customer Code :"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(756, 39)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 16)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "PL Date :"
        '
        'dtpDocDate
        '
        Me.dtpDocDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpDocDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDocDate.Location = New System.Drawing.Point(822, 36)
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.Size = New System.Drawing.Size(132, 22)
        Me.dtpDocDate.TabIndex = 16
        '
        'txtTransNum
        '
        Me.txtTransNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTransNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransNum.Location = New System.Drawing.Point(822, 12)
        Me.txtTransNum.Name = "txtTransNum"
        Me.txtTransNum.Size = New System.Drawing.Size(132, 22)
        Me.txtTransNum.TabIndex = 44
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(763, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 16)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "PL No. :"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.SeaGreen
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSearch, Me.tsbNew, Me.tsbEdit, Me.tsbSave, Me.tsbCancel, Me.ToolStripSeparator1, Me.tsbCopy, Me.ToolStripSeparator4, Me.tsbPrint, Me.tsbReports, Me.ToolStripSeparator2, Me.tsbPrevious, Me.tsbNext, Me.ToolStripSeparator3, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(969, 40)
        Me.ToolStrip1.TabIndex = 1317
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbSearch
        '
        Me.tsbSearch.AutoSize = False
        Me.tsbSearch.ForeColor = System.Drawing.Color.White
        Me.tsbSearch.Image = Global.jade.My.Resources.Resources.view
        Me.tsbSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.tsbSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSearch.Name = "tsbSearch"
        Me.tsbSearch.Size = New System.Drawing.Size(50, 35)
        Me.tsbSearch.Text = "Search"
        Me.tsbSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        'tsbEdit
        '
        Me.tsbEdit.AutoSize = False
        Me.tsbEdit.ForeColor = System.Drawing.Color.White
        Me.tsbEdit.Image = Global.jade.My.Resources.Resources.edit_pen_write_notes_document_3c679c93cb5d1fed_512x512
        Me.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbEdit.Name = "tsbEdit"
        Me.tsbEdit.Size = New System.Drawing.Size(50, 35)
        Me.tsbEdit.Text = "Edit"
        Me.tsbEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        'tsbCancel
        '
        Me.tsbCancel.AutoSize = False
        Me.tsbCancel.ForeColor = System.Drawing.Color.White
        Me.tsbCancel.Image = Global.jade.My.Resources.Resources.close_icon
        Me.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbCancel.Name = "tsbCancel"
        Me.tsbCancel.Size = New System.Drawing.Size(50, 35)
        Me.tsbCancel.Text = "Cancel"
        Me.tsbCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 40)
        '
        'tsbCopy
        '
        Me.tsbCopy.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbCopyPO})
        Me.tsbCopy.ForeColor = System.Drawing.Color.White
        Me.tsbCopy.Image = Global.jade.My.Resources.Resources.finance_report_infographic_512
        Me.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbCopy.Name = "tsbCopy"
        Me.tsbCopy.Size = New System.Drawing.Size(48, 37)
        Me.tsbCopy.Text = "Copy"
        Me.tsbCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbCopyPO
        '
        Me.tsbCopyPO.Name = "tsbCopyPO"
        Me.tsbCopyPO.Size = New System.Drawing.Size(152, 22)
        Me.tsbCopyPO.Text = "From SO"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 40)
        '
        'tsbPrint
        '
        Me.tsbPrint.AutoSize = False
        Me.tsbPrint.ForeColor = System.Drawing.Color.White
        Me.tsbPrint.Image = Global.jade.My.Resources.Resources.printer_circle_blue_512
        Me.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPrint.Name = "tsbPrint"
        Me.tsbPrint.Size = New System.Drawing.Size(50, 35)
        Me.tsbPrint.Text = "Print"
        Me.tsbPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbReports
        '
        Me.tsbReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TestToolStripMenuItem1})
        Me.tsbReports.ForeColor = System.Drawing.Color.White
        Me.tsbReports.Image = Global.jade.My.Resources.Resources.finance_report_infographic_512
        Me.tsbReports.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbReports.Name = "tsbReports"
        Me.tsbReports.Size = New System.Drawing.Size(60, 37)
        Me.tsbReports.Text = "Reports"
        Me.tsbReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbReports.Visible = False
        '
        'TestToolStripMenuItem1
        '
        Me.TestToolStripMenuItem1.Name = "TestToolStripMenuItem1"
        Me.TestToolStripMenuItem1.Size = New System.Drawing.Size(110, 22)
        Me.TestToolStripMenuItem1.Text = "DR List"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 40)
        '
        'tsbPrevious
        '
        Me.tsbPrevious.AutoSize = False
        Me.tsbPrevious.ForeColor = System.Drawing.Color.White
        Me.tsbPrevious.Image = Global.jade.My.Resources.Resources.arrows_147746_960_720
        Me.tsbPrevious.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPrevious.Name = "tsbPrevious"
        Me.tsbPrevious.Size = New System.Drawing.Size(50, 35)
        Me.tsbPrevious.Text = "Previous"
        Me.tsbPrevious.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbNext
        '
        Me.tsbNext.AutoSize = False
        Me.tsbNext.ForeColor = System.Drawing.Color.White
        Me.tsbNext.Image = Global.jade.My.Resources.Resources.red_arrow_png_15
        Me.tsbNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbNext.Name = "tsbNext"
        Me.tsbNext.Size = New System.Drawing.Size(50, 35)
        Me.tsbNext.Text = "Next"
        Me.tsbNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 40)
        '
        'tsbClose
        '
        Me.tsbClose.AutoSize = False
        Me.tsbClose.ForeColor = System.Drawing.Color.White
        Me.tsbClose.Image = Global.jade.My.Resources.Resources.close_button_icon_transparent_background_247604
        Me.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbClose.Name = "tsbClose"
        Me.tsbClose.Size = New System.Drawing.Size(50, 35)
        Me.tsbClose.Text = "Close"
        Me.tsbClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        'dgvPick
        '
        Me.dgvPick.AllowUserToAddRows = False
        Me.dgvPick.AllowUserToDeleteRows = False
        Me.dgvPick.AllowUserToResizeRows = False
        Me.dgvPick.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPick.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPick.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcPick, Me.dgcWHSE, Me.dgcStockQTY, Me.dgcPickQTY, Me.dgcPickActual})
        Me.dgvPick.Location = New System.Drawing.Point(635, 199)
        Me.dgvPick.Name = "dgvPick"
        Me.dgvPick.RowHeadersVisible = False
        Me.dgvPick.Size = New System.Drawing.Size(328, 361)
        Me.dgvPick.TabIndex = 1318
        '
        'dgvPickAll
        '
        Me.dgvPickAll.AllowUserToAddRows = False
        Me.dgvPickAll.AllowUserToDeleteRows = False
        Me.dgvPickAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPickAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPickAll.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcItemCodeAll, Me.dgcWHSEall, Me.dgcStockAll})
        Me.dgvPickAll.Location = New System.Drawing.Point(635, 317)
        Me.dgvPickAll.Name = "dgvPickAll"
        Me.dgvPickAll.RowHeadersVisible = False
        Me.dgvPickAll.Size = New System.Drawing.Size(328, 125)
        Me.dgvPickAll.TabIndex = 1319
        '
        'dgcItemCodeAll
        '
        Me.dgcItemCodeAll.HeaderText = "ItemCode"
        Me.dgcItemCodeAll.Name = "dgcItemCodeAll"
        '
        'dgcWHSEall
        '
        Me.dgcWHSEall.HeaderText = "Warehouse"
        Me.dgcWHSEall.Name = "dgcWHSEall"
        '
        'dgcStockAll
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.dgcStockAll.DefaultCellStyle = DataGridViewCellStyle8
        Me.dgcStockAll.HeaderText = "Stock QTY"
        Me.dgcStockAll.Name = "dgcStockAll"
        Me.dgcStockAll.Width = 90
        '
        'dgvPickList
        '
        Me.dgvPickList.AllowUserToAddRows = False
        Me.dgvPickList.AllowUserToDeleteRows = False
        Me.dgvPickList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPickList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPickList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcItemCodeList, Me.dgcWHSEList, Me.dgcPickList, Me.dgcLineList})
        Me.dgvPickList.Location = New System.Drawing.Point(635, 448)
        Me.dgvPickList.Name = "dgvPickList"
        Me.dgvPickList.RowHeadersVisible = False
        Me.dgvPickList.Size = New System.Drawing.Size(328, 109)
        Me.dgvPickList.TabIndex = 1320
        '
        'dgcItemCodeList
        '
        Me.dgcItemCodeList.HeaderText = "ItemCode"
        Me.dgcItemCodeList.Name = "dgcItemCodeList"
        '
        'dgcWHSEList
        '
        Me.dgcWHSEList.HeaderText = "Warehouse"
        Me.dgcWHSEList.Name = "dgcWHSEList"
        '
        'dgcPickList
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.dgcPickList.DefaultCellStyle = DataGridViewCellStyle9
        Me.dgcPickList.HeaderText = "Pick QTY"
        Me.dgcPickList.Name = "dgcPickList"
        Me.dgcPickList.Width = 70
        '
        'dgcLineList
        '
        Me.dgcLineList.HeaderText = "Line"
        Me.dgcLineList.Name = "dgcLineList"
        '
        'dgcPick
        '
        Me.dgcPick.HeaderText = "Pick"
        Me.dgcPick.Name = "dgcPick"
        Me.dgcPick.Width = 40
        '
        'dgcWHSE
        '
        Me.dgcWHSE.HeaderText = "Warehouse"
        Me.dgcWHSE.Name = "dgcWHSE"
        Me.dgcWHSE.ReadOnly = True
        '
        'dgcStockQTY
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.dgcStockQTY.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgcStockQTY.HeaderText = "Stock QTY"
        Me.dgcStockQTY.Name = "dgcStockQTY"
        Me.dgcStockQTY.ReadOnly = True
        Me.dgcStockQTY.Width = 90
        '
        'dgcPickQTY
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.dgcPickQTY.DefaultCellStyle = DataGridViewCellStyle7
        Me.dgcPickQTY.HeaderText = "Pick QTY"
        Me.dgcPickQTY.Name = "dgcPickQTY"
        Me.dgcPickQTY.Width = 85
        '
        'dgcPickActual
        '
        Me.dgcPickActual.HeaderText = "Pick"
        Me.dgcPickActual.Name = "dgcPickActual"
        Me.dgcPickActual.Visible = False
        '
        'frmPL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(969, 561)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.dgvItemList)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvPick)
        Me.Controls.Add(Me.dgvPickList)
        Me.Controls.Add(Me.dgvPickAll)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmPL"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pick List"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgvItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvPick, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvPickAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvPickList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvItemList As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTransNum As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbCopy As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents tsbCopyPO As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbReports As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents TestToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbPrevious As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNext As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSearchVCE As System.Windows.Forms.Button
    Friend WithEvents txtVCEName As System.Windows.Forms.TextBox
    Friend WithEvents txtVCECode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpDocDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpActual As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtSORef As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents dgvPick As System.Windows.Forms.DataGridView
    Friend WithEvents dgvPickAll As System.Windows.Forms.DataGridView
    Friend WithEvents dgvPickList As System.Windows.Forms.DataGridView
    Friend WithEvents dgcItemCodeAll As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcWHSEall As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcStockAll As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcItemCodeList As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcWHSEList As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcPickList As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcLineList As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcLine As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcItemCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcItemDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcUOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcReqQTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcAllocatedQTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcForAllocation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcPickTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgclineSO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcPick As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents dgcWHSE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcStockQTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcPickQTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcPickActual As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
