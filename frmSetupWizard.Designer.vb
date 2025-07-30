<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetupWizard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetupWizard))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tpCompany = New System.Windows.Forms.TabPage()
        Me.tpCOAUploader = New System.Windows.Forms.TabPage()
        Me.tpVCEUploader = New System.Windows.Forms.TabPage()
        Me.tpBankMasterfile = New System.Windows.Forms.TabPage()
        Me.tpSettings = New System.Windows.Forms.TabPage()
        Me.tpWelcome = New System.Windows.Forms.TabPage()
        Me.tpFinished = New System.Windows.Forms.TabPage()
        Me.tsbPrevious = New System.Windows.Forms.ToolStripButton()
        Me.tsbNext = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.tpItemMasterUploader = New System.Windows.Forms.TabPage()
        Me.ToolStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbPrevious, Me.tsbNext, Me.ToolStripSeparator3, Me.tsbExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 553)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1068, 40)
        Me.ToolStrip1.TabIndex = 1317
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 40)
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tpWelcome)
        Me.TabControl1.Controls.Add(Me.tpCompany)
        Me.TabControl1.Controls.Add(Me.tpCOAUploader)
        Me.TabControl1.Controls.Add(Me.tpVCEUploader)
        Me.TabControl1.Controls.Add(Me.tpItemMasterUploader)
        Me.TabControl1.Controls.Add(Me.tpBankMasterfile)
        Me.TabControl1.Controls.Add(Me.tpSettings)
        Me.TabControl1.Controls.Add(Me.tpFinished)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1068, 553)
        Me.TabControl1.TabIndex = 1318
        '
        'tpCompany
        '
        Me.tpCompany.AutoScroll = True
        Me.tpCompany.Location = New System.Drawing.Point(4, 22)
        Me.tpCompany.Name = "tpCompany"
        Me.tpCompany.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCompany.Size = New System.Drawing.Size(1060, 527)
        Me.tpCompany.TabIndex = 0
        Me.tpCompany.Text = "Company Info"
        Me.tpCompany.UseVisualStyleBackColor = True
        '
        'tpCOAUploader
        '
        Me.tpCOAUploader.AutoScroll = True
        Me.tpCOAUploader.Location = New System.Drawing.Point(4, 22)
        Me.tpCOAUploader.Name = "tpCOAUploader"
        Me.tpCOAUploader.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCOAUploader.Size = New System.Drawing.Size(1060, 527)
        Me.tpCOAUploader.TabIndex = 1
        Me.tpCOAUploader.Text = "Chart of Account"
        Me.tpCOAUploader.UseVisualStyleBackColor = True
        '
        'tpVCEUploader
        '
        Me.tpVCEUploader.AutoScroll = True
        Me.tpVCEUploader.Location = New System.Drawing.Point(4, 22)
        Me.tpVCEUploader.Name = "tpVCEUploader"
        Me.tpVCEUploader.Padding = New System.Windows.Forms.Padding(3)
        Me.tpVCEUploader.Size = New System.Drawing.Size(1060, 527)
        Me.tpVCEUploader.TabIndex = 2
        Me.tpVCEUploader.Text = "VCE Uploader"
        Me.tpVCEUploader.UseVisualStyleBackColor = True
        '
        'tpBankMasterfile
        '
        Me.tpBankMasterfile.AutoScroll = True
        Me.tpBankMasterfile.Location = New System.Drawing.Point(4, 22)
        Me.tpBankMasterfile.Name = "tpBankMasterfile"
        Me.tpBankMasterfile.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBankMasterfile.Size = New System.Drawing.Size(1060, 527)
        Me.tpBankMasterfile.TabIndex = 3
        Me.tpBankMasterfile.Text = "Bank Masterfile"
        Me.tpBankMasterfile.UseVisualStyleBackColor = True
        '
        'tpSettings
        '
        Me.tpSettings.AutoScroll = True
        Me.tpSettings.Location = New System.Drawing.Point(4, 22)
        Me.tpSettings.Name = "tpSettings"
        Me.tpSettings.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSettings.Size = New System.Drawing.Size(1060, 527)
        Me.tpSettings.TabIndex = 4
        Me.tpSettings.Text = "Settings"
        Me.tpSettings.UseVisualStyleBackColor = True
        '
        'tpWelcome
        '
        Me.tpWelcome.BackgroundImage = Global.jade.My.Resources.Resources.Gr8_Wizard_Windows_start
        Me.tpWelcome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tpWelcome.Location = New System.Drawing.Point(4, 22)
        Me.tpWelcome.Name = "tpWelcome"
        Me.tpWelcome.Padding = New System.Windows.Forms.Padding(3)
        Me.tpWelcome.Size = New System.Drawing.Size(1060, 527)
        Me.tpWelcome.TabIndex = 5
        Me.tpWelcome.Text = "Welcome"
        Me.tpWelcome.UseVisualStyleBackColor = True
        '
        'tpFinished
        '
        Me.tpFinished.BackgroundImage = Global.jade.My.Resources.Resources.ADS_1_2
        Me.tpFinished.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tpFinished.Location = New System.Drawing.Point(4, 22)
        Me.tpFinished.Name = "tpFinished"
        Me.tpFinished.Padding = New System.Windows.Forms.Padding(3)
        Me.tpFinished.Size = New System.Drawing.Size(1060, 527)
        Me.tpFinished.TabIndex = 6
        Me.tpFinished.Text = "Finished"
        Me.tpFinished.UseVisualStyleBackColor = True
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
        'tpItemMasterUploader
        '
        Me.tpItemMasterUploader.Location = New System.Drawing.Point(4, 22)
        Me.tpItemMasterUploader.Name = "tpItemMasterUploader"
        Me.tpItemMasterUploader.Padding = New System.Windows.Forms.Padding(3)
        Me.tpItemMasterUploader.Size = New System.Drawing.Size(1060, 527)
        Me.tpItemMasterUploader.TabIndex = 7
        Me.tpItemMasterUploader.Text = "Item Master Uploader"
        Me.tpItemMasterUploader.UseVisualStyleBackColor = True
        '
        'frmSetupWizard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1068, 593)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSetupWizard"
        Me.Text = "Setup Wizard"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbPrevious As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNext As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tpCompany As System.Windows.Forms.TabPage
    Friend WithEvents tpCOAUploader As System.Windows.Forms.TabPage
    Friend WithEvents tpVCEUploader As System.Windows.Forms.TabPage
    Friend WithEvents tpBankMasterfile As System.Windows.Forms.TabPage
    Friend WithEvents tpSettings As System.Windows.Forms.TabPage
    Friend WithEvents tpWelcome As System.Windows.Forms.TabPage
    Friend WithEvents tpFinished As System.Windows.Forms.TabPage
    Friend WithEvents tpItemMasterUploader As System.Windows.Forms.TabPage
End Class
