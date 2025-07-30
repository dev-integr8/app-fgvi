<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmProperty_Search
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProperty_Search))
        Me.cbFilter = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chPropCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.txtFilter = New System.Windows.Forms.TextBox()
        Me.lvList = New System.Windows.Forms.ListView()
        Me.cbProject = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbUnitType = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.cbModel = New System.Windows.Forms.ComboBox()
        Me.lblModel = New System.Windows.Forms.Label()
        Me.chBlk = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chLot = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chPhase = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chModel = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chFloorArea = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chLotArea = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chContractPrice = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chProject = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chDesc = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chStatus = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'cbFilter
        '
        Me.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilter.FormattingEnabled = True
        Me.cbFilter.Items.AddRange(New Object() {"PropCode", "Description"})
        Me.cbFilter.Location = New System.Drawing.Point(71, 59)
        Me.cbFilter.Name = "cbFilter"
        Me.cbFilter.Size = New System.Drawing.Size(129, 23)
        Me.cbFilter.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 15)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Filter by :"
        '
        'chPropCode
        '
        Me.chPropCode.Text = "Property Code"
        Me.chPropCode.Width = 92
        '
        'txtFilter
        '
        Me.txtFilter.Location = New System.Drawing.Point(203, 59)
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(266, 21)
        Me.txtFilter.TabIndex = 8
        '
        'lvList
        '
        Me.lvList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chPropCode, Me.chProject, Me.chModel, Me.chBlk, Me.chLot, Me.chPhase, Me.chFloorArea, Me.chLotArea, Me.chContractPrice, Me.chDesc, Me.chType, Me.chStatus})
        Me.lvList.Cursor = System.Windows.Forms.Cursors.Default
        Me.lvList.FullRowSelect = True
        Me.lvList.HideSelection = False
        Me.lvList.Location = New System.Drawing.Point(0, 88)
        Me.lvList.MultiSelect = False
        Me.lvList.Name = "lvList"
        Me.lvList.Size = New System.Drawing.Size(774, 366)
        Me.lvList.TabIndex = 7
        Me.lvList.UseCompatibleStateImageBehavior = False
        Me.lvList.View = System.Windows.Forms.View.Details
        '
        'cbProject
        '
        Me.cbProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbProject.FormattingEnabled = True
        Me.cbProject.Location = New System.Drawing.Point(71, 6)
        Me.cbProject.Name = "cbProject"
        Me.cbProject.Size = New System.Drawing.Size(398, 23)
        Me.cbProject.TabIndex = 1187
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 15)
        Me.Label3.TabIndex = 1186
        Me.Label3.Text = "Project :"
        '
        'cbUnitType
        '
        Me.cbUnitType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbUnitType.FormattingEnabled = True
        Me.cbUnitType.Location = New System.Drawing.Point(71, 32)
        Me.cbUnitType.Name = "cbUnitType"
        Me.cbUnitType.Size = New System.Drawing.Size(187, 23)
        Me.cbUnitType.TabIndex = 1189
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 36)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 15)
        Me.Label4.TabIndex = 1188
        Me.Label4.Text = "Unit Type :"
        '
        'btnSearch
        '
        Me.btnSearch.BackgroundImage = CType(resources.GetObject("btnSearch.BackgroundImage"), System.Drawing.Image)
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearch.Location = New System.Drawing.Point(473, 57)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(25, 25)
        Me.btnSearch.TabIndex = 1185
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'cbModel
        '
        Me.cbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbModel.FormattingEnabled = True
        Me.cbModel.Location = New System.Drawing.Point(315, 32)
        Me.cbModel.Name = "cbModel"
        Me.cbModel.Size = New System.Drawing.Size(154, 23)
        Me.cbModel.TabIndex = 1191
        '
        'lblModel
        '
        Me.lblModel.AutoSize = True
        Me.lblModel.Location = New System.Drawing.Point(266, 36)
        Me.lblModel.Name = "lblModel"
        Me.lblModel.Size = New System.Drawing.Size(48, 15)
        Me.lblModel.TabIndex = 1190
        Me.lblModel.Text = "Model :"
        '
        'chBlk
        '
        Me.chBlk.Text = "Blk"
        Me.chBlk.Width = 45
        '
        'chLot
        '
        Me.chLot.Text = "Lot"
        Me.chLot.Width = 43
        '
        'chPhase
        '
        Me.chPhase.Text = "Phase"
        Me.chPhase.Width = 73
        '
        'chModel
        '
        Me.chModel.Text = "Model"
        Me.chModel.Width = 102
        '
        'chFloorArea
        '
        Me.chFloorArea.Text = "Floor Area"
        Me.chFloorArea.Width = 71
        '
        'chLotArea
        '
        Me.chLotArea.Text = "Lot Area"
        Me.chLotArea.Width = 68
        '
        'chContractPrice
        '
        Me.chContractPrice.Text = "Total Contract Price"
        Me.chContractPrice.Width = 121
        '
        'chProject
        '
        Me.chProject.Text = "Project"
        Me.chProject.Width = 150
        '
        'chDesc
        '
        Me.chDesc.Text = "Description"
        Me.chDesc.Width = 0
        '
        'chType
        '
        Me.chType.Text = "Type"
        '
        'chStatus
        '
        Me.chStatus.Text = "Status"
        Me.chStatus.Width = 80
        '
        'frmProperty_Search
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(774, 454)
        Me.Controls.Add(Me.cbModel)
        Me.Controls.Add(Me.lblModel)
        Me.Controls.Add(Me.cbUnitType)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cbProject)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.cbFilter)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtFilter)
        Me.Controls.Add(Me.lvList)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Name = "frmProperty_Search"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Property Listing"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbFilter As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chPropCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtFilter As System.Windows.Forms.TextBox
    Friend WithEvents lvList As System.Windows.Forms.ListView
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents cbProject As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cbUnitType As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cbModel As ComboBox
    Friend WithEvents lblModel As Label
    Friend WithEvents chProject As ColumnHeader
    Friend WithEvents chModel As ColumnHeader
    Friend WithEvents chBlk As ColumnHeader
    Friend WithEvents chLot As ColumnHeader
    Friend WithEvents chPhase As ColumnHeader
    Friend WithEvents chFloorArea As ColumnHeader
    Friend WithEvents chLotArea As ColumnHeader
    Friend WithEvents chContractPrice As ColumnHeader
    Friend WithEvents chDesc As ColumnHeader
    Friend WithEvents chType As ColumnHeader
    Friend WithEvents chStatus As ColumnHeader
End Class
