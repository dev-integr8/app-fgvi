<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReceivables
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
        Me.lvARList = New System.Windows.Forms.ListView()
        Me.chType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chAmount = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chInterest = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chCBU = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chSavings = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chKasimpatiya = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chPenalty = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chMaturityDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chTerms = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chRefID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chRefType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'lvARList
        '
        Me.lvARList.CheckBoxes = True
        Me.lvARList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chType, Me.chAmount, Me.chInterest, Me.chCBU, Me.chSavings, Me.chKasimpatiya, Me.chPenalty, Me.chMaturityDate, Me.chTerms, Me.chRefID, Me.chRefType})
        Me.lvARList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvARList.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvARList.FullRowSelect = True
        Me.lvARList.GridLines = True
        Me.lvARList.Location = New System.Drawing.Point(0, 0)
        Me.lvARList.Name = "lvARList"
        Me.lvARList.Size = New System.Drawing.Size(745, 335)
        Me.lvARList.TabIndex = 1377
        Me.lvARList.UseCompatibleStateImageBehavior = False
        Me.lvARList.View = System.Windows.Forms.View.Details
        '
        'chType
        '
        Me.chType.Text = "Type"
        Me.chType.Width = 141
        '
        'chAmount
        '
        Me.chAmount.Text = "Amount"
        Me.chAmount.Width = 94
        '
        'chInterest
        '
        Me.chInterest.Text = "Interest"
        Me.chInterest.Width = 94
        '
        'chCBU
        '
        Me.chCBU.Text = "CBU"
        Me.chCBU.Width = 94
        '
        'chSavings
        '
        Me.chSavings.Text = "Savings"
        Me.chSavings.Width = 94
        '
        'chKasimpatiya
        '
        Me.chKasimpatiya.Text = "Kasimpatiya"
        Me.chKasimpatiya.Width = 94
        '
        'chPenalty
        '
        Me.chPenalty.Text = "Penalty"
        Me.chPenalty.Width = 80
        '
        'chMaturityDate
        '
        Me.chMaturityDate.Text = "MaturityDate"
        Me.chMaturityDate.Width = 87
        '
        'chTerms
        '
        Me.chTerms.Text = "Terms"
        Me.chTerms.Width = 73
        '
        'chRefID
        '
        Me.chRefID.Text = "Ref ID"
        Me.chRefID.Width = 58
        '
        'chRefType
        '
        Me.chRefType.Text = "RefType"
        Me.chRefType.Width = 79
        '
        'frmReceivables
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(745, 335)
        Me.Controls.Add(Me.lvARList)
        Me.Name = "frmReceivables"
        Me.Text = "AR List"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lvARList As System.Windows.Forms.ListView
    Friend WithEvents chType As System.Windows.Forms.ColumnHeader
    Friend WithEvents chAmount As System.Windows.Forms.ColumnHeader
    Friend WithEvents chInterest As System.Windows.Forms.ColumnHeader
    Friend WithEvents chPenalty As System.Windows.Forms.ColumnHeader
    Friend WithEvents chMaturityDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents chTerms As System.Windows.Forms.ColumnHeader
    Friend WithEvents chRefID As System.Windows.Forms.ColumnHeader
    Friend WithEvents chRefType As System.Windows.Forms.ColumnHeader
    Friend WithEvents chCBU As System.Windows.Forms.ColumnHeader
    Friend WithEvents chSavings As System.Windows.Forms.ColumnHeader
    Friend WithEvents chKasimpatiya As System.Windows.Forms.ColumnHeader
End Class
