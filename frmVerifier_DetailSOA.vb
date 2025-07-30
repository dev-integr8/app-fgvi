Public Class frmVerifier_DetailSOA
    Public Type As String

    Private Sub btnPreview_Click(sender As System.Object, e As System.EventArgs) Handles btnPreview.Click
        If Type = "Savings" Then
            Dim f As New frmReport_Display
            f.ShowDialog("SOA_Credit_Detailed", txtVCECode.Text, dtpDateTo.Value, dtpDateFrom.Value)
            f.Dispose()
        ElseIf Type = "Loan" Then
            Dim f As New frmReport_Display
            f.ShowDialog("SOA_Loan_Detailed", txtVCECode.Text, dtpDateTo.Value, dtpDateFrom.Value)
            f.Dispose()
        Else
            Dim f As New frmReport_Display
            f.ShowDialog("SOA_Detailed", txtVCECode.Text, dtpDateTo.Value, dtpDateFrom.Value)
            f.Dispose()
        End If
    End Sub

End Class