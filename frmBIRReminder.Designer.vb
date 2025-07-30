<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBIRReminder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBIRReminder))
        Me.lvReminders = New System.Windows.Forms.ListView()
        Me.CHBirfrm = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.CHPeriod = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.CHDeadline = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnOkay = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvReminders
        '
        Me.lvReminders.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.CHBirfrm, Me.CHPeriod, Me.CHDeadline})
        Me.lvReminders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvReminders.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvReminders.GridLines = True
        Me.lvReminders.Location = New System.Drawing.Point(2, 15)
        Me.lvReminders.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.lvReminders.Name = "lvReminders"
        Me.lvReminders.Size = New System.Drawing.Size(610, 298)
        Me.lvReminders.TabIndex = 4
        Me.lvReminders.UseCompatibleStateImageBehavior = False
        Me.lvReminders.View = System.Windows.Forms.View.Details
        '
        'CHBirfrm
        '
        Me.CHBirfrm.Text = "BIR REMINDERS"
        Me.CHBirfrm.Width = 320
        '
        'CHPeriod
        '
        Me.CHPeriod.DisplayIndex = 2
        Me.CHPeriod.Text = "Period"
        Me.CHPeriod.Width = 150
        '
        'CHDeadline
        '
        Me.CHDeadline.DisplayIndex = 1
        Me.CHDeadline.Text = "Deadline"
        Me.CHDeadline.Width = 150
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lvReminders)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 10)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(614, 315)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        '
        'btnOkay
        '
        Me.btnOkay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOkay.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.btnOkay.Location = New System.Drawing.Point(213, 344)
        Me.btnOkay.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnOkay.Name = "btnOkay"
        Me.btnOkay.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnOkay.Size = New System.Drawing.Size(164, 29)
        Me.btnOkay.TabIndex = 200
        Me.btnOkay.Text = "Ok"
        Me.btnOkay.UseVisualStyleBackColor = True
        '
        'frmBIRReminder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(616, 397)
        Me.Controls.Add(Me.btnOkay)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "frmBIRReminder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "REMINDERS"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lvReminders As System.Windows.Forms.ListView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnOkay As System.Windows.Forms.Button
    Friend WithEvents CHBirfrm As System.Windows.Forms.ColumnHeader
    Friend WithEvents CHPeriod As System.Windows.Forms.ColumnHeader
    Friend WithEvents CHDeadline As System.Windows.Forms.ColumnHeader
End Class
