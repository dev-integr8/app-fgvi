<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ItemMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_ItemMaster))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbSearch = New System.Windows.Forms.ToolStripButton()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txItemType = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbUOM = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbStatus = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtSupCode = New System.Windows.Forms.TextBox()
        Me.txtSupName = New System.Windows.Forms.TextBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtInv_AccntCode = New System.Windows.Forms.TextBox()
        Me.txtInv_AccntName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtATD_Code = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtATD_Title = New System.Windows.Forms.TextBox()
        Me.txtExpense_Code = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtExpense_Title = New System.Windows.Forms.TextBox()
        Me.txtSales_AccntCode = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSales_AccntName = New System.Windows.Forms.TextBox()
        Me.txtCOS_AccntCode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCOS_AccntName = New System.Windows.Forms.TextBox()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSearch, Me.tsbNew, Me.tsbEdit, Me.tsbSave, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(773, 38)
        Me.ToolStrip1.TabIndex = 1
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
        'txtItemCode
        '
        Me.txtItemCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtItemCode.ForeColor = System.Drawing.Color.Black
        Me.txtItemCode.Location = New System.Drawing.Point(137, 63)
        Me.txtItemCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.Size = New System.Drawing.Size(328, 22)
        Me.txtItemCode.TabIndex = 1088
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(50, 94)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 16)
        Me.Label1.TabIndex = 1090
        Me.Label1.Text = "Item Name :"
        '
        'txtItemName
        '
        Me.txtItemName.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtItemName.ForeColor = System.Drawing.Color.Black
        Me.txtItemName.Location = New System.Drawing.Point(137, 90)
        Me.txtItemName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.Size = New System.Drawing.Size(328, 22)
        Me.txtItemName.TabIndex = 1089
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(54, 66)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 16)
        Me.Label4.TabIndex = 1091
        Me.Label4.Text = "Item Code :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(473, 66)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 16)
        Me.Label2.TabIndex = 1093
        Me.Label2.Text = "Item Type :"
        '
        'txItemType
        '
        Me.txItemType.BackColor = System.Drawing.SystemColors.Window
        Me.txItemType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txItemType.ForeColor = System.Drawing.Color.Black
        Me.txItemType.Location = New System.Drawing.Point(555, 63)
        Me.txItemType.Margin = New System.Windows.Forms.Padding(4)
        Me.txItemType.Name = "txItemType"
        Me.txItemType.Size = New System.Drawing.Size(183, 22)
        Me.txItemType.TabIndex = 1094
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(502, 94)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 16)
        Me.Label5.TabIndex = 1096
        Me.Label5.Text = "UOM :"
        '
        'cbUOM
        '
        Me.cbUOM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUOM.FormattingEnabled = True
        Me.cbUOM.Location = New System.Drawing.Point(555, 89)
        Me.cbUOM.Name = "cbUOM"
        Me.cbUOM.Size = New System.Drawing.Size(183, 23)
        Me.cbUOM.TabIndex = 1097
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(484, 77)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 16)
        Me.Label6.TabIndex = 1098
        Me.Label6.Text = "Status :"
        Me.Label6.Visible = False
        '
        'cbStatus
        '
        Me.cbStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbStatus.FormattingEnabled = True
        Me.cbStatus.Location = New System.Drawing.Point(543, 76)
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.Size = New System.Drawing.Size(183, 23)
        Me.cbStatus.TabIndex = 1099
        Me.cbStatus.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtSupCode)
        Me.GroupBox1.Controls.Add(Me.txtSupName)
        Me.GroupBox1.Controls.Add(Me.txtAmount)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cbStatus)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 41)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(745, 146)
        Me.GroupBox1.TabIndex = 1100
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Item Master"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(53, 106)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(64, 16)
        Me.Label12.TabIndex = 1380
        Me.Label12.Text = "Supplier :"
        '
        'txtSupCode
        '
        Me.txtSupCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupCode.Enabled = False
        Me.txtSupCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtSupCode.ForeColor = System.Drawing.Color.Black
        Me.txtSupCode.Location = New System.Drawing.Point(127, 106)
        Me.txtSupCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSupCode.Name = "txtSupCode"
        Me.txtSupCode.ReadOnly = True
        Me.txtSupCode.Size = New System.Drawing.Size(132, 22)
        Me.txtSupCode.TabIndex = 1378
        '
        'txtSupName
        '
        Me.txtSupName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtSupName.ForeColor = System.Drawing.Color.Black
        Me.txtSupName.Location = New System.Drawing.Point(267, 106)
        Me.txtSupName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSupName.Name = "txtSupName"
        Me.txtSupName.Size = New System.Drawing.Size(459, 22)
        Me.txtSupName.TabIndex = 1379
        '
        'txtAmount
        '
        Me.txtAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.Location = New System.Drawing.Point(125, 76)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmount.Size = New System.Drawing.Size(183, 22)
        Me.txtAmount.TabIndex = 1377
        Me.txtAmount.Text = "0.00"
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(55, 78)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(62, 16)
        Me.Label8.TabIndex = 1376
        Me.Label8.Text = " Amount :"
        '
        'txtInv_AccntCode
        '
        Me.txtInv_AccntCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtInv_AccntCode.Enabled = False
        Me.txtInv_AccntCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtInv_AccntCode.ForeColor = System.Drawing.Color.Black
        Me.txtInv_AccntCode.Location = New System.Drawing.Point(152, 20)
        Me.txtInv_AccntCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtInv_AccntCode.Name = "txtInv_AccntCode"
        Me.txtInv_AccntCode.ReadOnly = True
        Me.txtInv_AccntCode.Size = New System.Drawing.Size(136, 22)
        Me.txtInv_AccntCode.TabIndex = 1100
        '
        'txtInv_AccntName
        '
        Me.txtInv_AccntName.BackColor = System.Drawing.SystemColors.Window
        Me.txtInv_AccntName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtInv_AccntName.ForeColor = System.Drawing.Color.Black
        Me.txtInv_AccntName.Location = New System.Drawing.Point(292, 20)
        Me.txtInv_AccntName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtInv_AccntName.Name = "txtInv_AccntName"
        Me.txtInv_AccntName.Size = New System.Drawing.Size(434, 22)
        Me.txtInv_AccntName.TabIndex = 1101
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(32, 23)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(119, 16)
        Me.Label7.TabIndex = 1103
        Me.Label7.Text = "Inventory Account :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtATD_Code)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.txtATD_Title)
        Me.GroupBox2.Controls.Add(Me.txtExpense_Code)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.txtExpense_Title)
        Me.GroupBox2.Controls.Add(Me.txtSales_AccntCode)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtSales_AccntName)
        Me.GroupBox2.Controls.Add(Me.txtCOS_AccntCode)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtCOS_AccntName)
        Me.GroupBox2.Controls.Add(Me.txtInv_AccntCode)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtInv_AccntName)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 193)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(745, 176)
        Me.GroupBox2.TabIndex = 1101
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Default Entry"
        '
        'txtATD_Code
        '
        Me.txtATD_Code.BackColor = System.Drawing.SystemColors.Window
        Me.txtATD_Code.Enabled = False
        Me.txtATD_Code.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtATD_Code.ForeColor = System.Drawing.Color.Black
        Me.txtATD_Code.Location = New System.Drawing.Point(152, 140)
        Me.txtATD_Code.Margin = New System.Windows.Forms.Padding(4)
        Me.txtATD_Code.Name = "txtATD_Code"
        Me.txtATD_Code.ReadOnly = True
        Me.txtATD_Code.Size = New System.Drawing.Size(136, 22)
        Me.txtATD_Code.TabIndex = 1113
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(55, 140)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(93, 16)
        Me.Label11.TabIndex = 1115
        Me.Label11.Text = "ATD Account :"
        '
        'txtATD_Title
        '
        Me.txtATD_Title.BackColor = System.Drawing.SystemColors.Window
        Me.txtATD_Title.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtATD_Title.ForeColor = System.Drawing.Color.Black
        Me.txtATD_Title.Location = New System.Drawing.Point(292, 140)
        Me.txtATD_Title.Margin = New System.Windows.Forms.Padding(4)
        Me.txtATD_Title.Name = "txtATD_Title"
        Me.txtATD_Title.Size = New System.Drawing.Size(434, 22)
        Me.txtATD_Title.TabIndex = 1114
        '
        'txtExpense_Code
        '
        Me.txtExpense_Code.BackColor = System.Drawing.SystemColors.Window
        Me.txtExpense_Code.Enabled = False
        Me.txtExpense_Code.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtExpense_Code.ForeColor = System.Drawing.Color.Black
        Me.txtExpense_Code.Location = New System.Drawing.Point(152, 110)
        Me.txtExpense_Code.Margin = New System.Windows.Forms.Padding(4)
        Me.txtExpense_Code.Name = "txtExpense_Code"
        Me.txtExpense_Code.ReadOnly = True
        Me.txtExpense_Code.Size = New System.Drawing.Size(136, 22)
        Me.txtExpense_Code.TabIndex = 1110
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(33, 113)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(118, 16)
        Me.Label10.TabIndex = 1112
        Me.Label10.Text = "Expense Account :"
        '
        'txtExpense_Title
        '
        Me.txtExpense_Title.BackColor = System.Drawing.SystemColors.Window
        Me.txtExpense_Title.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtExpense_Title.ForeColor = System.Drawing.Color.Black
        Me.txtExpense_Title.Location = New System.Drawing.Point(292, 110)
        Me.txtExpense_Title.Margin = New System.Windows.Forms.Padding(4)
        Me.txtExpense_Title.Name = "txtExpense_Title"
        Me.txtExpense_Title.Size = New System.Drawing.Size(434, 22)
        Me.txtExpense_Title.TabIndex = 1111
        '
        'txtSales_AccntCode
        '
        Me.txtSales_AccntCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtSales_AccntCode.Enabled = False
        Me.txtSales_AccntCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtSales_AccntCode.ForeColor = System.Drawing.Color.Black
        Me.txtSales_AccntCode.Location = New System.Drawing.Point(152, 80)
        Me.txtSales_AccntCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSales_AccntCode.Name = "txtSales_AccntCode"
        Me.txtSales_AccntCode.ReadOnly = True
        Me.txtSales_AccntCode.Size = New System.Drawing.Size(136, 22)
        Me.txtSales_AccntCode.TabIndex = 1107
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(51, 83)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(100, 16)
        Me.Label9.TabIndex = 1109
        Me.Label9.Text = "Sales Account :"
        '
        'txtSales_AccntName
        '
        Me.txtSales_AccntName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSales_AccntName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtSales_AccntName.ForeColor = System.Drawing.Color.Black
        Me.txtSales_AccntName.Location = New System.Drawing.Point(292, 80)
        Me.txtSales_AccntName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSales_AccntName.Name = "txtSales_AccntName"
        Me.txtSales_AccntName.Size = New System.Drawing.Size(434, 22)
        Me.txtSales_AccntName.TabIndex = 1108
        '
        'txtCOS_AccntCode
        '
        Me.txtCOS_AccntCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCOS_AccntCode.Enabled = False
        Me.txtCOS_AccntCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtCOS_AccntCode.ForeColor = System.Drawing.Color.Black
        Me.txtCOS_AccntCode.Location = New System.Drawing.Point(152, 50)
        Me.txtCOS_AccntCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCOS_AccntCode.Name = "txtCOS_AccntCode"
        Me.txtCOS_AccntCode.ReadOnly = True
        Me.txtCOS_AccntCode.Size = New System.Drawing.Size(136, 22)
        Me.txtCOS_AccntCode.TabIndex = 1104
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(7, 53)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(144, 16)
        Me.Label3.TabIndex = 1106
        Me.Label3.Text = "Cost of Sales Account :"
        '
        'txtCOS_AccntName
        '
        Me.txtCOS_AccntName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCOS_AccntName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtCOS_AccntName.ForeColor = System.Drawing.Color.Black
        Me.txtCOS_AccntName.Location = New System.Drawing.Point(292, 50)
        Me.txtCOS_AccntName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCOS_AccntName.Name = "txtCOS_AccntName"
        Me.txtCOS_AccntName.Size = New System.Drawing.Size(434, 22)
        Me.txtCOS_AccntName.TabIndex = 1105
        '
        'Frm_ItemMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(773, 378)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.cbUOM)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txItemType)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtItemCode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtItemName)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Frm_ItemMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Item Masterfile"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtItemCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtItemName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txItemType As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbUOM As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtInv_AccntCode As System.Windows.Forms.TextBox
    Friend WithEvents txtInv_AccntName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSales_AccntCode As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtSales_AccntName As System.Windows.Forms.TextBox
    Friend WithEvents txtCOS_AccntCode As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCOS_AccntName As System.Windows.Forms.TextBox
    Friend WithEvents txtExpense_Code As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtExpense_Title As System.Windows.Forms.TextBox
    Friend WithEvents txtATD_Code As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtATD_Title As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtSupCode As System.Windows.Forms.TextBox
    Friend WithEvents txtSupName As System.Windows.Forms.TextBox
End Class
