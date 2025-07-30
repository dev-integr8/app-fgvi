<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVCE_Updater
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVCE_Updater))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbDownload = New System.Windows.Forms.ToolStripButton()
        Me.tsbUpload = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.pgbCounter = New System.Windows.Forms.ProgressBar()
        Me.dgvEntry = New System.Windows.Forms.DataGridView()
        Me.tsbExtract = New System.Windows.Forms.ToolStripButton()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.chVCECode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chVCEName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chLastName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chFirstName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chMiddleName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chTinNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chTerms = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chVCEType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chAddressUnit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chAddressLotBlk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chAddressStreet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chAddressSubd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chAddressBrgy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chAddressCity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chAddressProvince = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chAddressZipCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chContact_Tel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chContactCel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chContactFax = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chContactEmail = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chContactWeb = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chContactPerson = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chPEZA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chCompany = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chBankName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chBankAccount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbNew, Me.tsbSave, Me.ToolStripSeparator1, Me.tsbExtract, Me.tsbDownload, Me.tsbUpload, Me.ToolStripSeparator3, Me.tsbClose, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1002, 40)
        Me.ToolStrip1.TabIndex = 1204
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
        Me.tsbNew.Text = "Generate"
        Me.tsbNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 40)
        '
        'tsbDownload
        '
        Me.tsbDownload.AutoSize = False
        Me.tsbDownload.Font = New System.Drawing.Font("Segoe UI", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbDownload.ForeColor = System.Drawing.Color.White
        Me.tsbDownload.Image = Global.jade.My.Resources.Resources.downloadicon
        Me.tsbDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbDownload.Name = "tsbDownload"
        Me.tsbDownload.Size = New System.Drawing.Size(50, 35)
        Me.tsbDownload.Text = "Download"
        Me.tsbDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbDownload.Visible = False
        '
        'tsbUpload
        '
        Me.tsbUpload.AutoSize = False
        Me.tsbUpload.ForeColor = System.Drawing.Color.White
        Me.tsbUpload.Image = Global.jade.My.Resources.Resources.arrow_upload_icon
        Me.tsbUpload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbUpload.Name = "tsbUpload"
        Me.tsbUpload.Size = New System.Drawing.Size(50, 35)
        Me.tsbUpload.Text = "Upload"
        Me.tsbUpload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        'lblTime
        '
        Me.lblTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTime.AutoSize = True
        Me.lblTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lblTime.Location = New System.Drawing.Point(272, 457)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(0, 15)
        Me.lblTime.TabIndex = 1097
        Me.lblTime.Visible = False
        '
        'pgbCounter
        '
        Me.pgbCounter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgbCounter.BackColor = System.Drawing.Color.Gainsboro
        Me.pgbCounter.ForeColor = System.Drawing.Color.Red
        Me.pgbCounter.Location = New System.Drawing.Point(12, 454)
        Me.pgbCounter.Name = "pgbCounter"
        Me.pgbCounter.Size = New System.Drawing.Size(244, 18)
        Me.pgbCounter.TabIndex = 1067
        '
        'dgvEntry
        '
        Me.dgvEntry.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEntry.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chVCECode, Me.chVCEName, Me.chLastName, Me.chFirstName, Me.chMiddleName, Me.chTinNo, Me.chTerms, Me.chVCEType, Me.chAddressUnit, Me.chAddressLotBlk, Me.chAddressStreet, Me.chAddressSubd, Me.chAddressBrgy, Me.chAddressCity, Me.chAddressProvince, Me.chAddressZipCode, Me.chContact_Tel, Me.chContactCel, Me.chContactFax, Me.chContactEmail, Me.chContactWeb, Me.chContactPerson, Me.chPEZA, Me.chCompany, Me.chBankName, Me.chBankAccount})
        Me.dgvEntry.Location = New System.Drawing.Point(13, 44)
        Me.dgvEntry.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvEntry.Name = "dgvEntry"
        Me.dgvEntry.ReadOnly = True
        Me.dgvEntry.Size = New System.Drawing.Size(977, 403)
        Me.dgvEntry.TabIndex = 1306
        '
        'tsbExtract
        '
        Me.tsbExtract.AutoSize = False
        Me.tsbExtract.ForeColor = System.Drawing.Color.White
        Me.tsbExtract.Image = Global.jade.My.Resources.Resources.edit_pen_write_notes_document_3c679c93cb5d1fed_512x512
        Me.tsbExtract.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExtract.Name = "tsbExtract"
        Me.tsbExtract.Size = New System.Drawing.Size(50, 35)
        Me.tsbExtract.Text = "Extract"
        Me.tsbExtract.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'chVCECode
        '
        Me.chVCECode.HeaderText = "VCE Code"
        Me.chVCECode.Name = "chVCECode"
        Me.chVCECode.ReadOnly = True
        '
        'chVCEName
        '
        Me.chVCEName.HeaderText = "VCE Name"
        Me.chVCEName.Name = "chVCEName"
        Me.chVCEName.ReadOnly = True
        '
        'chLastName
        '
        Me.chLastName.HeaderText = "Last Name"
        Me.chLastName.Name = "chLastName"
        Me.chLastName.ReadOnly = True
        '
        'chFirstName
        '
        Me.chFirstName.HeaderText = "First Name"
        Me.chFirstName.Name = "chFirstName"
        Me.chFirstName.ReadOnly = True
        '
        'chMiddleName
        '
        Me.chMiddleName.HeaderText = "Middle Name"
        Me.chMiddleName.Name = "chMiddleName"
        Me.chMiddleName.ReadOnly = True
        '
        'chTinNo
        '
        Me.chTinNo.HeaderText = "Tin No."
        Me.chTinNo.Name = "chTinNo"
        Me.chTinNo.ReadOnly = True
        '
        'chTerms
        '
        Me.chTerms.HeaderText = "Terms"
        Me.chTerms.Name = "chTerms"
        Me.chTerms.ReadOnly = True
        '
        'chVCEType
        '
        Me.chVCEType.HeaderText = "VCE Type"
        Me.chVCEType.Name = "chVCEType"
        Me.chVCEType.ReadOnly = True
        '
        'chAddressUnit
        '
        Me.chAddressUnit.HeaderText = "Address Unit"
        Me.chAddressUnit.Name = "chAddressUnit"
        Me.chAddressUnit.ReadOnly = True
        '
        'chAddressLotBlk
        '
        Me.chAddressLotBlk.HeaderText = "Address Lot/Blk"
        Me.chAddressLotBlk.Name = "chAddressLotBlk"
        Me.chAddressLotBlk.ReadOnly = True
        '
        'chAddressStreet
        '
        Me.chAddressStreet.HeaderText = "Address Street"
        Me.chAddressStreet.Name = "chAddressStreet"
        Me.chAddressStreet.ReadOnly = True
        '
        'chAddressSubd
        '
        Me.chAddressSubd.HeaderText = "Address Subdivision"
        Me.chAddressSubd.Name = "chAddressSubd"
        Me.chAddressSubd.ReadOnly = True
        '
        'chAddressBrgy
        '
        Me.chAddressBrgy.HeaderText = "Address Brgy"
        Me.chAddressBrgy.Name = "chAddressBrgy"
        Me.chAddressBrgy.ReadOnly = True
        '
        'chAddressCity
        '
        Me.chAddressCity.HeaderText = "Address City"
        Me.chAddressCity.Name = "chAddressCity"
        Me.chAddressCity.ReadOnly = True
        '
        'chAddressProvince
        '
        Me.chAddressProvince.HeaderText = "Address Province"
        Me.chAddressProvince.Name = "chAddressProvince"
        Me.chAddressProvince.ReadOnly = True
        '
        'chAddressZipCode
        '
        Me.chAddressZipCode.HeaderText = "Address ZipCode"
        Me.chAddressZipCode.Name = "chAddressZipCode"
        Me.chAddressZipCode.ReadOnly = True
        '
        'chContact_Tel
        '
        Me.chContact_Tel.HeaderText = "Contact Telephone"
        Me.chContact_Tel.Name = "chContact_Tel"
        Me.chContact_Tel.ReadOnly = True
        '
        'chContactCel
        '
        Me.chContactCel.HeaderText = "Contact Cellphone"
        Me.chContactCel.Name = "chContactCel"
        Me.chContactCel.ReadOnly = True
        '
        'chContactFax
        '
        Me.chContactFax.HeaderText = "Contact Fax"
        Me.chContactFax.Name = "chContactFax"
        Me.chContactFax.ReadOnly = True
        '
        'chContactEmail
        '
        Me.chContactEmail.HeaderText = "Contact Email"
        Me.chContactEmail.Name = "chContactEmail"
        Me.chContactEmail.ReadOnly = True
        '
        'chContactWeb
        '
        Me.chContactWeb.HeaderText = "Contact Web Site"
        Me.chContactWeb.Name = "chContactWeb"
        Me.chContactWeb.ReadOnly = True
        '
        'chContactPerson
        '
        Me.chContactPerson.HeaderText = "Contact Person"
        Me.chContactPerson.Name = "chContactPerson"
        Me.chContactPerson.ReadOnly = True
        '
        'chPEZA
        '
        Me.chPEZA.HeaderText = "PEZA"
        Me.chPEZA.Name = "chPEZA"
        Me.chPEZA.ReadOnly = True
        '
        'chCompany
        '
        Me.chCompany.HeaderText = "Company"
        Me.chCompany.Name = "chCompany"
        Me.chCompany.ReadOnly = True
        '
        'chBankName
        '
        Me.chBankName.HeaderText = "Bank Name"
        Me.chBankName.Name = "chBankName"
        Me.chBankName.ReadOnly = True
        '
        'chBankAccount
        '
        Me.chBankAccount.HeaderText = "Bank Account"
        Me.chBankAccount.Name = "chBankAccount"
        Me.chBankAccount.ReadOnly = True
        '
        'frmVCE_Updater
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 15!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1002, 478)
        Me.Controls.Add(Me.dgvEntry)
        Me.Controls.Add(Me.lblTime)
        Me.Controls.Add(Me.pgbCounter)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmVCE_Updater"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "VCE Uploader"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ToolStrip1.ResumeLayout(false)
        Me.ToolStrip1.PerformLayout
        CType(Me.dgvEntry,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents pgbCounter As System.Windows.Forms.ProgressBar
    Friend WithEvents dgvEntry As System.Windows.Forms.DataGridView
    Friend WithEvents tsbDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbUpload As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbExtract As System.Windows.Forms.ToolStripButton
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents chVCECode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chVCEName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chLastName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chFirstName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chMiddleName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chTinNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chTerms As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chVCEType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chAddressUnit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chAddressLotBlk As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chAddressStreet As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chAddressSubd As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chAddressBrgy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chAddressCity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chAddressProvince As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chAddressZipCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chContact_Tel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chContactCel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chContactFax As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chContactEmail As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chContactWeb As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chContactPerson As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chPEZA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chCompany As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chBankName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chBankAccount As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
