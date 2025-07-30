Public Class frmPDC_Batch
    Public StartDate As Date
    Public CheckNumber As String
    Public CheckAmount As Decimal
    Public NumberOfChecks As Integer
    Public PaymentFrequecy As String
    Public Saved As Boolean = False


    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        If txtCheckNo.Text = "" Then
            MsgBox("Please input the first check number", MsgBoxStyle.Exclamation)
        ElseIf Not IsNumeric(txtCheckNo.Text) Then
            MsgBox("Please input valid check number", MsgBoxStyle.Exclamation)
        ElseIf Not IsNumeric(txtCheckAmount.Text) OrElse CDec(txtCheckAmount.Text) <= 0 Then
            MsgBox("Please input valid check amount", MsgBoxStyle.Exclamation)
        Else
            CheckNumber = txtCheckNo.Text
            CheckAmount = CDec(txtCheckAmount.Text)
            NumberOfChecks = CInt(nupNumberOfChecks.Value)
            PaymentFrequecy = cbFrequency.SelectedItem
            StartDate = dtpDocDate.Value
            Saved = True
            Me.Close()
        End If
    End Sub

    Private Sub frmPDC_Batch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbFrequency.SelectedItem = "Monthly"
        dtpDocDate.Value = Date.Today.Date
    End Sub

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
    End Sub
End Class