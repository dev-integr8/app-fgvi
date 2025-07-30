<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplashScreen
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SplashScreen))
        Me.Version = New System.Windows.Forms.Label()
        Me.Copyright = New System.Windows.Forms.Label()
        Me.picLoading = New System.Windows.Forms.PictureBox()
        Me.lblforWindows = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.picLoading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Version
        '
        Me.Version.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Version.BackColor = System.Drawing.Color.Transparent
        Me.Version.Font = New System.Drawing.Font("Candara", 9.5!)
        Me.Version.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Version.Location = New System.Drawing.Point(174, 60)
        Me.Version.Name = "Version"
        Me.Version.Size = New System.Drawing.Size(241, 24)
        Me.Version.TabIndex = 3
        Me.Version.Text = "Version {0}.{1:00}"
        Me.Version.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'Copyright
        '
        Me.Copyright.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Copyright.BackColor = System.Drawing.Color.Transparent
        Me.Copyright.Font = New System.Drawing.Font("Candara", 9.5!)
        Me.Copyright.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Copyright.Location = New System.Drawing.Point(281, 326)
        Me.Copyright.Name = "Copyright"
        Me.Copyright.Size = New System.Drawing.Size(241, 20)
        Me.Copyright.TabIndex = 4
        Me.Copyright.Text = "Copyright"
        Me.Copyright.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'picLoading
        '
        Me.picLoading.BackColor = System.Drawing.Color.Transparent
        Me.picLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picLoading.Image = Global.jade.My.Resources.Resources.loading
        Me.picLoading.ImageLocation = ""
        Me.picLoading.InitialImage = Nothing
        Me.picLoading.Location = New System.Drawing.Point(234, 215)
        Me.picLoading.Name = "picLoading"
        Me.picLoading.Size = New System.Drawing.Size(74, 61)
        Me.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picLoading.TabIndex = 5
        Me.picLoading.TabStop = False
        '
        'lblforWindows
        '
        Me.lblforWindows.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblforWindows.BackColor = System.Drawing.Color.Transparent
        Me.lblforWindows.Font = New System.Drawing.Font("Candara", 9.5!)
        Me.lblforWindows.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblforWindows.Location = New System.Drawing.Point(174, 161)
        Me.lblforWindows.Name = "lblforWindows"
        Me.lblforWindows.Size = New System.Drawing.Size(241, 24)
        Me.lblforWindows.TabIndex = 6
        Me.lblforWindows.Text = "for Windows"
        Me.lblforWindows.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SplashScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(534, 355)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblforWindows)
        Me.Controls.Add(Me.picLoading)
        Me.Controls.Add(Me.Version)
        Me.Controls.Add(Me.Copyright)
        Me.Font = New System.Drawing.Font("Candara", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SplashScreen"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.picLoading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Version As Label
    Friend WithEvents Copyright As Label
    Friend WithEvents picLoading As PictureBox
    Friend WithEvents lblforWindows As Label
    Friend WithEvents Timer1 As Timer
End Class
