<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmItemBarcodePrinting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmItemBarcodePrinting))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbPrint = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.BtnBarcodeSearch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CbSearchBy = New System.Windows.Forms.ComboBox()
        Me.TxtSearch = New System.Windows.Forms.TextBox()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.txtBarcode = New System.Windows.Forms.TextBox()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtQty = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtUOM = New System.Windows.Forms.TextBox()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lvItem = New System.Windows.Forms.ListView()
        Me.chItemCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chBarcode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chItemName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chUOM = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chPrice = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbPrint, Me.ToolStripSeparator2, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(797, 46)
        Me.ToolStrip1.TabIndex = 1186
        Me.ToolStrip1.Text = "ToolStrip1"
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
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 46)
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
        'BtnBarcodeSearch
        '
        Me.BtnBarcodeSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnBarcodeSearch.BackColor = System.Drawing.Color.White
        Me.BtnBarcodeSearch.BackgroundImage = Global.jade.My.Resources.Resources.view
        Me.BtnBarcodeSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnBarcodeSearch.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.BtnBarcodeSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnBarcodeSearch.Location = New System.Drawing.Point(756, 60)
        Me.BtnBarcodeSearch.Name = "BtnBarcodeSearch"
        Me.BtnBarcodeSearch.Size = New System.Drawing.Size(29, 23)
        Me.BtnBarcodeSearch.TabIndex = 1190
        Me.BtnBarcodeSearch.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(161, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 15)
        Me.Label1.TabIndex = 1189
        Me.Label1.Text = "Search"
        '
        'CbSearchBy
        '
        Me.CbSearchBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbSearchBy.FormattingEnabled = True
        Me.CbSearchBy.Items.AddRange(New Object() {"Item Name", "Item Code", "Barcode"})
        Me.CbSearchBy.Location = New System.Drawing.Point(12, 54)
        Me.CbSearchBy.Name = "CbSearchBy"
        Me.CbSearchBy.Size = New System.Drawing.Size(143, 23)
        Me.CbSearchBy.TabIndex = 1188
        '
        'TxtSearch
        '
        Me.TxtSearch.Location = New System.Drawing.Point(209, 54)
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(549, 23)
        Me.TxtSearch.TabIndex = 1187
        '
        'txtItemCode
        '
        Me.txtItemCode.Location = New System.Drawing.Point(90, 22)
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.ReadOnly = True
        Me.txtItemCode.Size = New System.Drawing.Size(273, 23)
        Me.txtItemCode.TabIndex = 1192
        '
        'txtBarcode
        '
        Me.txtBarcode.Location = New System.Drawing.Point(90, 47)
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.ReadOnly = True
        Me.txtBarcode.Size = New System.Drawing.Size(273, 23)
        Me.txtBarcode.TabIndex = 1193
        '
        'txtItemName
        '
        Me.txtItemName.Location = New System.Drawing.Point(90, 73)
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.ReadOnly = True
        Me.txtItemName.Size = New System.Drawing.Size(273, 23)
        Me.txtItemName.TabIndex = 1194
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 15)
        Me.Label2.TabIndex = 1195
        Me.Label2.Text = "ItemCode"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 15)
        Me.Label3.TabIndex = 1196
        Me.Label3.Text = "Barcode"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 15)
        Me.Label4.TabIndex = 1197
        Me.Label4.Text = "Item Name"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.txtQty)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtUOM)
        Me.GroupBox1.Controls.Add(Me.txtPrice)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtItemCode)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtBarcode)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtItemName)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 340)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(773, 111)
        Me.GroupBox1.TabIndex = 1198
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Item Information"
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(441, 73)
        Me.txtQty.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(273, 23)
        Me.txtQty.TabIndex = 1205
        Me.txtQty.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(369, 76)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 15)
        Me.Label5.TabIndex = 1204
        Me.Label5.Text = "Quantity"
        '
        'txtUOM
        '
        Me.txtUOM.Location = New System.Drawing.Point(441, 22)
        Me.txtUOM.Name = "txtUOM"
        Me.txtUOM.ReadOnly = True
        Me.txtUOM.Size = New System.Drawing.Size(273, 23)
        Me.txtUOM.TabIndex = 1198
        '
        'txtPrice
        '
        Me.txtPrice.Location = New System.Drawing.Point(441, 47)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.ReadOnly = True
        Me.txtPrice.Size = New System.Drawing.Size(273, 23)
        Me.txtPrice.TabIndex = 1199
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(369, 25)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 15)
        Me.Label6.TabIndex = 1202
        Me.Label6.Text = "UOM"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(369, 50)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 15)
        Me.Label7.TabIndex = 1201
        Me.Label7.Text = "Price"
        '
        'lvItem
        '
        Me.lvItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvItem.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chItemCode, Me.chBarcode, Me.chItemName, Me.chUOM, Me.chPrice})
        Me.lvItem.FullRowSelect = True
        Me.lvItem.GridLines = True
        Me.lvItem.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvItem.HideSelection = False
        Me.lvItem.Location = New System.Drawing.Point(13, 84)
        Me.lvItem.Margin = New System.Windows.Forms.Padding(4)
        Me.lvItem.MultiSelect = False
        Me.lvItem.Name = "lvItem"
        Me.lvItem.Size = New System.Drawing.Size(771, 251)
        Me.lvItem.TabIndex = 1328
        Me.lvItem.UseCompatibleStateImageBehavior = False
        Me.lvItem.View = System.Windows.Forms.View.Details
        '
        'chItemCode
        '
        Me.chItemCode.Text = "Item Code"
        Me.chItemCode.Width = 103
        '
        'chBarcode
        '
        Me.chBarcode.Text = "Barcode"
        Me.chBarcode.Width = 110
        '
        'chItemName
        '
        Me.chItemName.Text = "Item Name"
        Me.chItemName.Width = 381
        '
        'chUOM
        '
        Me.chUOM.Text = "UOM"
        Me.chUOM.Width = 64
        '
        'chPrice
        '
        Me.chPrice.Text = "Price"
        Me.chPrice.Width = 79
        '
        'frmItemBarcodePrinting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(797, 467)
        Me.Controls.Add(Me.lvItem)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BtnBarcodeSearch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CbSearchBy)
        Me.Controls.Add(Me.TxtSearch)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmItemBarcodePrinting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Barcode Printing"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents tsbExit As ToolStripButton
    Friend WithEvents tsbPrint As ToolStripButton
    Friend WithEvents BtnBarcodeSearch As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents CbSearchBy As ComboBox
    Friend WithEvents TxtSearch As TextBox
    Friend WithEvents txtItemCode As TextBox
    Friend WithEvents txtBarcode As TextBox
    Friend WithEvents txtItemName As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtQty As NumericUpDown
    Friend WithEvents Label5 As Label
    Friend WithEvents txtUOM As TextBox
    Friend WithEvents txtPrice As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents lvItem As ListView
    Friend WithEvents chItemCode As ColumnHeader
    Friend WithEvents chBarcode As ColumnHeader
    Friend WithEvents chItemName As ColumnHeader
    Friend WithEvents chUOM As ColumnHeader
    Friend WithEvents chPrice As ColumnHeader
End Class
