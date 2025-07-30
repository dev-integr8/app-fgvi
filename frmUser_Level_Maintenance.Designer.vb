<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUser_Level_Maintenance
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUser_Level_Maintenance))
        Me.lvModule = New System.Windows.Forms.ListView()
        Me.chModID = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.chModName = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.cmsUpdate = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiAllow = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiDisallow = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblUserLevel = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblContent = New System.Windows.Forms.Label()
        Me.lvOthers = New System.Windows.Forms.ListView()
        Me.chCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chDesc = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chContentAllowed = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lvAccess = New System.Windows.Forms.ListView()
        Me.chAccessID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chAccessType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chAllowed = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbSearch = New System.Windows.Forms.ToolStripButton()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.gbSystem = New System.Windows.Forms.GroupBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.chkPearl = New System.Windows.Forms.CheckBox()
        Me.chkSapphire = New System.Windows.Forms.CheckBox()
        Me.chkEmerald = New System.Windows.Forms.CheckBox()
        Me.chkOnyx = New System.Windows.Forms.CheckBox()
        Me.chkRuby = New System.Windows.Forms.CheckBox()
        Me.chkJade = New System.Windows.Forms.CheckBox()
        Me.cbSystem = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lvModuleList = New System.Windows.Forms.ListView()
        Me.chAllModID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chAllModName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chAllModSort = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chAllModCategory = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lvModAllowed = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnAddSpecific = New System.Windows.Forms.Button()
        Me.btnAddAll = New System.Windows.Forms.Button()
        Me.btnRemAll = New System.Windows.Forms.Button()
        Me.btnRemSpecific = New System.Windows.Forms.Button()
        Me.lvModAllowedAll = New System.Windows.Forms.ListView()
        Me.chAllowedAllID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chAllowedAllDesc = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chAllowedAllValue = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblModule = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cbType = New System.Windows.Forms.ComboBox()
        Me.lvAccessAll = New System.Windows.Forms.ListView()
        Me.chAccessAllID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chAccessAllCategory = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chAccessAllValue = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmsUpdate.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.gbSystem.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvModule
        '
        resources.ApplyResources(Me.lvModule, "lvModule")
        Me.lvModule.BackColor = System.Drawing.Color.White
        Me.lvModule.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvModule.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chModID, Me.chModName})
        Me.lvModule.ForeColor = System.Drawing.Color.Black
        Me.lvModule.FullRowSelect = True
        Me.lvModule.GridLines = True
        Me.lvModule.HideSelection = False
        Me.lvModule.MultiSelect = False
        Me.lvModule.Name = "lvModule"
        Me.lvModule.UseCompatibleStateImageBehavior = False
        Me.lvModule.View = System.Windows.Forms.View.Details
        '
        'chModID
        '
        resources.ApplyResources(Me.chModID, "chModID")
        '
        'chModName
        '
        resources.ApplyResources(Me.chModName, "chModName")
        '
        'cmsUpdate
        '
        Me.cmsUpdate.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiAllow, Me.tsmiDisallow})
        Me.cmsUpdate.Name = "ContextMenuStrip1"
        resources.ApplyResources(Me.cmsUpdate, "cmsUpdate")
        '
        'tsmiAllow
        '
        Me.tsmiAllow.Name = "tsmiAllow"
        resources.ApplyResources(Me.tsmiAllow, "tsmiAllow")
        '
        'tsmiDisallow
        '
        Me.tsmiDisallow.Name = "tsmiDisallow"
        resources.ApplyResources(Me.tsmiDisallow, "tsmiDisallow")
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Name = "Label1"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblUserLevel)
        Me.GroupBox1.Controls.Add(Me.Label10)
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'lblUserLevel
        '
        resources.ApplyResources(Me.lblUserLevel, "lblUserLevel")
        Me.lblUserLevel.Name = "lblUserLevel"
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.Name = "Label10"
        '
        'lblContent
        '
        Me.lblContent.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        resources.ApplyResources(Me.lblContent, "lblContent")
        Me.lblContent.ForeColor = System.Drawing.Color.White
        Me.lblContent.Name = "lblContent"
        '
        'lvOthers
        '
        resources.ApplyResources(Me.lvOthers, "lvOthers")
        Me.lvOthers.BackColor = System.Drawing.Color.White
        Me.lvOthers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvOthers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chCode, Me.chDesc, Me.chContentAllowed})
        Me.lvOthers.ForeColor = System.Drawing.Color.Black
        Me.lvOthers.FullRowSelect = True
        Me.lvOthers.GridLines = True
        Me.lvOthers.MultiSelect = False
        Me.lvOthers.Name = "lvOthers"
        Me.lvOthers.UseCompatibleStateImageBehavior = False
        Me.lvOthers.View = System.Windows.Forms.View.Details
        '
        'chCode
        '
        resources.ApplyResources(Me.chCode, "chCode")
        '
        'chDesc
        '
        resources.ApplyResources(Me.chDesc, "chDesc")
        '
        'chContentAllowed
        '
        resources.ApplyResources(Me.chContentAllowed, "chContentAllowed")
        '
        'Panel1
        '
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Panel1.Name = "Panel1"
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Name = "Label7"
        '
        'lvAccess
        '
        resources.ApplyResources(Me.lvAccess, "lvAccess")
        Me.lvAccess.BackColor = System.Drawing.Color.White
        Me.lvAccess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvAccess.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chAccessID, Me.chAccessType, Me.chAllowed})
        Me.lvAccess.ForeColor = System.Drawing.Color.Black
        Me.lvAccess.FullRowSelect = True
        Me.lvAccess.GridLines = True
        Me.lvAccess.MultiSelect = False
        Me.lvAccess.Name = "lvAccess"
        Me.lvAccess.UseCompatibleStateImageBehavior = False
        Me.lvAccess.View = System.Windows.Forms.View.Details
        '
        'chAccessID
        '
        resources.ApplyResources(Me.chAccessID, "chAccessID")
        '
        'chAccessType
        '
        resources.ApplyResources(Me.chAccessType, "chAccessType")
        '
        'chAllowed
        '
        resources.ApplyResources(Me.chAllowed, "chAllowed")
        '
        'ToolStrip1
        '
        resources.ApplyResources(Me.ToolStrip1, "ToolStrip1")
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSearch, Me.tsbNew, Me.tsbEdit, Me.tsbSave, Me.tsbDelete, Me.ToolStripSeparator1, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Name = "ToolStrip1"
        '
        'tsbSearch
        '
        resources.ApplyResources(Me.tsbSearch, "tsbSearch")
        Me.tsbSearch.ForeColor = System.Drawing.Color.White
        Me.tsbSearch.Image = Global.jade.My.Resources.Resources.view
        Me.tsbSearch.Name = "tsbSearch"
        '
        'tsbNew
        '
        resources.ApplyResources(Me.tsbNew, "tsbNew")
        Me.tsbNew.ForeColor = System.Drawing.Color.White
        Me.tsbNew.Image = Global.jade.My.Resources.Resources.circle_document_documents_extension_file_page_sheet_icon_7
        Me.tsbNew.Name = "tsbNew"
        '
        'tsbEdit
        '
        resources.ApplyResources(Me.tsbEdit, "tsbEdit")
        Me.tsbEdit.ForeColor = System.Drawing.Color.White
        Me.tsbEdit.Image = Global.jade.My.Resources.Resources.edit_pen_write_notes_document_3c679c93cb5d1fed_512x512
        Me.tsbEdit.Name = "tsbEdit"
        '
        'tsbSave
        '
        resources.ApplyResources(Me.tsbSave, "tsbSave")
        Me.tsbSave.ForeColor = System.Drawing.Color.White
        Me.tsbSave.Image = Global.jade.My.Resources.Resources.Save_Icon
        Me.tsbSave.Name = "tsbSave"
        '
        'tsbDelete
        '
        resources.ApplyResources(Me.tsbDelete, "tsbDelete")
        Me.tsbDelete.ForeColor = System.Drawing.Color.White
        Me.tsbDelete.Image = Global.jade.My.Resources.Resources.close_icon
        Me.tsbDelete.Name = "tsbDelete"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        '
        'tsbClose
        '
        resources.ApplyResources(Me.tsbClose, "tsbClose")
        Me.tsbClose.ForeColor = System.Drawing.Color.White
        Me.tsbClose.Image = Global.jade.My.Resources.Resources.close_button_icon_transparent_background_247604
        Me.tsbClose.Name = "tsbClose"
        '
        'tsbExit
        '
        resources.ApplyResources(Me.tsbExit, "tsbExit")
        Me.tsbExit.ForeColor = System.Drawing.Color.White
        Me.tsbExit.Image = Global.jade.My.Resources.Resources.exit_button_icon_18
        Me.tsbExit.Name = "tsbExit"
        '
        'gbSystem
        '
        Me.gbSystem.Controls.Add(Me.chkAll)
        Me.gbSystem.Controls.Add(Me.chkPearl)
        Me.gbSystem.Controls.Add(Me.chkSapphire)
        Me.gbSystem.Controls.Add(Me.chkEmerald)
        Me.gbSystem.Controls.Add(Me.chkOnyx)
        Me.gbSystem.Controls.Add(Me.chkRuby)
        Me.gbSystem.Controls.Add(Me.chkJade)
        resources.ApplyResources(Me.gbSystem, "gbSystem")
        Me.gbSystem.Name = "gbSystem"
        Me.gbSystem.TabStop = False
        '
        'chkAll
        '
        resources.ApplyResources(Me.chkAll, "chkAll")
        Me.chkAll.Name = "chkAll"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'chkPearl
        '
        resources.ApplyResources(Me.chkPearl, "chkPearl")
        Me.chkPearl.Name = "chkPearl"
        Me.chkPearl.UseVisualStyleBackColor = True
        '
        'chkSapphire
        '
        resources.ApplyResources(Me.chkSapphire, "chkSapphire")
        Me.chkSapphire.Name = "chkSapphire"
        Me.chkSapphire.UseVisualStyleBackColor = True
        '
        'chkEmerald
        '
        resources.ApplyResources(Me.chkEmerald, "chkEmerald")
        Me.chkEmerald.Name = "chkEmerald"
        Me.chkEmerald.UseVisualStyleBackColor = True
        '
        'chkOnyx
        '
        resources.ApplyResources(Me.chkOnyx, "chkOnyx")
        Me.chkOnyx.Name = "chkOnyx"
        Me.chkOnyx.UseVisualStyleBackColor = True
        '
        'chkRuby
        '
        resources.ApplyResources(Me.chkRuby, "chkRuby")
        Me.chkRuby.Name = "chkRuby"
        Me.chkRuby.UseVisualStyleBackColor = True
        '
        'chkJade
        '
        resources.ApplyResources(Me.chkJade, "chkJade")
        Me.chkJade.Name = "chkJade"
        Me.chkJade.UseVisualStyleBackColor = True
        '
        'cbSystem
        '
        Me.cbSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSystem.FormattingEnabled = True
        Me.cbSystem.Items.AddRange(New Object() {resources.GetString("cbSystem.Items")})
        resources.ApplyResources(Me.cbSystem, "cbSystem")
        Me.cbSystem.Name = "cbSystem"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'lvModuleList
        '
        resources.ApplyResources(Me.lvModuleList, "lvModuleList")
        Me.lvModuleList.BackColor = System.Drawing.Color.White
        Me.lvModuleList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvModuleList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chAllModID, Me.chAllModName, Me.chAllModSort, Me.chAllModCategory})
        Me.lvModuleList.ForeColor = System.Drawing.Color.Black
        Me.lvModuleList.FullRowSelect = True
        Me.lvModuleList.GridLines = True
        Me.lvModuleList.HideSelection = False
        Me.lvModuleList.MultiSelect = False
        Me.lvModuleList.Name = "lvModuleList"
        Me.lvModuleList.UseCompatibleStateImageBehavior = False
        Me.lvModuleList.View = System.Windows.Forms.View.Details
        '
        'chAllModID
        '
        resources.ApplyResources(Me.chAllModID, "chAllModID")
        '
        'chAllModName
        '
        resources.ApplyResources(Me.chAllModName, "chAllModName")
        '
        'chAllModSort
        '
        resources.ApplyResources(Me.chAllModSort, "chAllModSort")
        '
        'chAllModCategory
        '
        resources.ApplyResources(Me.chAllModCategory, "chAllModCategory")
        '
        'Panel2
        '
        resources.ApplyResources(Me.Panel2, "Panel2")
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Panel2.Name = "Panel2"
        '
        'lvModAllowed
        '
        resources.ApplyResources(Me.lvModAllowed, "lvModAllowed")
        Me.lvModAllowed.BackColor = System.Drawing.Color.White
        Me.lvModAllowed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvModAllowed.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lvModAllowed.ForeColor = System.Drawing.Color.Black
        Me.lvModAllowed.FullRowSelect = True
        Me.lvModAllowed.GridLines = True
        Me.lvModAllowed.HideSelection = False
        Me.lvModAllowed.MultiSelect = False
        Me.lvModAllowed.Name = "lvModAllowed"
        Me.lvModAllowed.UseCompatibleStateImageBehavior = False
        Me.lvModAllowed.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        resources.ApplyResources(Me.ColumnHeader1, "ColumnHeader1")
        '
        'ColumnHeader2
        '
        resources.ApplyResources(Me.ColumnHeader2, "ColumnHeader2")
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Name = "Label9"
        '
        'btnAddSpecific
        '
        resources.ApplyResources(Me.btnAddSpecific, "btnAddSpecific")
        Me.btnAddSpecific.Name = "btnAddSpecific"
        Me.btnAddSpecific.UseVisualStyleBackColor = True
        '
        'btnAddAll
        '
        resources.ApplyResources(Me.btnAddAll, "btnAddAll")
        Me.btnAddAll.Name = "btnAddAll"
        Me.btnAddAll.UseVisualStyleBackColor = True
        '
        'btnRemAll
        '
        resources.ApplyResources(Me.btnRemAll, "btnRemAll")
        Me.btnRemAll.Name = "btnRemAll"
        Me.btnRemAll.UseVisualStyleBackColor = True
        '
        'btnRemSpecific
        '
        resources.ApplyResources(Me.btnRemSpecific, "btnRemSpecific")
        Me.btnRemSpecific.Name = "btnRemSpecific"
        Me.btnRemSpecific.UseVisualStyleBackColor = True
        '
        'lvModAllowedAll
        '
        resources.ApplyResources(Me.lvModAllowedAll, "lvModAllowedAll")
        Me.lvModAllowedAll.BackColor = System.Drawing.Color.White
        Me.lvModAllowedAll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvModAllowedAll.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chAllowedAllID, Me.chAllowedAllDesc, Me.chAllowedAllValue})
        Me.lvModAllowedAll.ForeColor = System.Drawing.Color.Black
        Me.lvModAllowedAll.FullRowSelect = True
        Me.lvModAllowedAll.GridLines = True
        Me.lvModAllowedAll.HideSelection = False
        Me.lvModAllowedAll.MultiSelect = False
        Me.lvModAllowedAll.Name = "lvModAllowedAll"
        Me.lvModAllowedAll.UseCompatibleStateImageBehavior = False
        Me.lvModAllowedAll.View = System.Windows.Forms.View.Details
        '
        'chAllowedAllID
        '
        resources.ApplyResources(Me.chAllowedAllID, "chAllowedAllID")
        '
        'chAllowedAllDesc
        '
        resources.ApplyResources(Me.chAllowedAllDesc, "chAllowedAllDesc")
        '
        'lblModule
        '
        resources.ApplyResources(Me.lblModule, "lblModule")
        Me.lblModule.Name = "lblModule"
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.Name = "Label11"
        '
        'cbType
        '
        Me.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbType.FormattingEnabled = True
        Me.cbType.Items.AddRange(New Object() {resources.GetString("cbType.Items"), resources.GetString("cbType.Items1"), resources.GetString("cbType.Items2")})
        resources.ApplyResources(Me.cbType, "cbType")
        Me.cbType.Name = "cbType"
        '
        'lvAccessAll
        '
        Me.lvAccessAll.BackColor = System.Drawing.Color.White
        Me.lvAccessAll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvAccessAll.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chAccessAllID, Me.chAccessAllCategory, Me.chAccessAllValue})
        Me.lvAccessAll.ForeColor = System.Drawing.Color.Black
        Me.lvAccessAll.FullRowSelect = True
        Me.lvAccessAll.GridLines = True
        resources.ApplyResources(Me.lvAccessAll, "lvAccessAll")
        Me.lvAccessAll.MultiSelect = False
        Me.lvAccessAll.Name = "lvAccessAll"
        Me.lvAccessAll.UseCompatibleStateImageBehavior = False
        Me.lvAccessAll.View = System.Windows.Forms.View.Details
        '
        'frmUser_Level_Maintenance
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cbType)
        Me.Controls.Add(Me.lblModule)
        Me.Controls.Add(Me.btnAddSpecific)
        Me.Controls.Add(Me.btnAddAll)
        Me.Controls.Add(Me.btnRemAll)
        Me.Controls.Add(Me.btnRemSpecific)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lvModAllowed)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cbSystem)
        Me.Controls.Add(Me.gbSystem)
        Me.Controls.Add(Me.lblContent)
        Me.Controls.Add(Me.lvOthers)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lvModule)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lvAccess)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lvModuleList)
        Me.Controls.Add(Me.lvModAllowedAll)
        Me.Controls.Add(Me.lvAccessAll)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmUser_Level_Maintenance"
        Me.cmsUpdate.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout
        Me.gbSystem.ResumeLayout(false)
        Me.gbSystem.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents lvModule As System.Windows.Forms.ListView
    Friend WithEvents cmsUpdate As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsmiAllow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chModID As System.Windows.Forms.ColumnHeader
    Friend WithEvents chModName As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lvAccess As System.Windows.Forms.ListView
    Friend WithEvents chAccessType As System.Windows.Forms.ColumnHeader
    Friend WithEvents chAccessID As System.Windows.Forms.ColumnHeader
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblUserLevel As System.Windows.Forms.Label
    Friend WithEvents lblContent As System.Windows.Forms.Label
    Friend WithEvents lvOthers As System.Windows.Forms.ListView
    Friend WithEvents chCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents chDesc As System.Windows.Forms.ColumnHeader
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents gbSystem As System.Windows.Forms.GroupBox
    Friend WithEvents chkPearl As System.Windows.Forms.CheckBox
    Friend WithEvents chkSapphire As System.Windows.Forms.CheckBox
    Friend WithEvents chkEmerald As System.Windows.Forms.CheckBox
    Friend WithEvents chkOnyx As System.Windows.Forms.CheckBox
    Friend WithEvents chkRuby As System.Windows.Forms.CheckBox
    Friend WithEvents chkJade As System.Windows.Forms.CheckBox
    Friend WithEvents chkAll As System.Windows.Forms.CheckBox
    Friend WithEvents cbSystem As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lvModuleList As System.Windows.Forms.ListView
    Friend WithEvents chAllModID As System.Windows.Forms.ColumnHeader
    Friend WithEvents chAllModName As System.Windows.Forms.ColumnHeader
    Friend WithEvents chAllModCategory As System.Windows.Forms.ColumnHeader
    Friend WithEvents chAllModSort As System.Windows.Forms.ColumnHeader
    Friend WithEvents chAllowed As System.Windows.Forms.ColumnHeader
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lvModAllowed As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnAddSpecific As System.Windows.Forms.Button
    Friend WithEvents btnAddAll As System.Windows.Forms.Button
    Friend WithEvents btnRemAll As System.Windows.Forms.Button
    Friend WithEvents btnRemSpecific As System.Windows.Forms.Button
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents lvModAllowedAll As System.Windows.Forms.ListView
    Friend WithEvents chAllowedAllID As System.Windows.Forms.ColumnHeader
    Friend WithEvents chAllowedAllDesc As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblModule As System.Windows.Forms.Label
    Friend WithEvents chContentAllowed As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cbType As System.Windows.Forms.ComboBox
    Friend WithEvents tsmiDisallow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lvAccessAll As System.Windows.Forms.ListView
    Friend WithEvents chAccessAllID As System.Windows.Forms.ColumnHeader
    Friend WithEvents chAccessAllCategory As System.Windows.Forms.ColumnHeader
    Friend WithEvents chAccessAllValue As System.Windows.Forms.ColumnHeader
    Friend WithEvents chAllowedAllValue As System.Windows.Forms.ColumnHeader
    Friend WithEvents tsbClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
End Class
