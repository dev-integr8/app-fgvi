Public Class frmLoan_WindowExtend

    Dim SQL As New SQLControl
    Public strTransID As String = ""
    Public strNote As String = ""
    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Dispose()
    End Sub

    Private Sub cbMode_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbMode.SelectedIndexChanged
        ComputeNoOfAmort()
    End Sub

    Private Sub frmLoan_WindowExtend_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        cbMode.SelectedItem = "Monthly"
    End Sub

    Private Sub txtTerms_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtTerms.TextChanged
        If IsNumeric(txtTerms.Text) Then
            ComputeNoOfAmort()
        Else
            txtTerms.Text = txtTerms.Text.Substring(0, txtTerms.Text.Length - 2)
        End If
    End Sub

    Private Sub ComputeNoOfAmort()
        Dim decAmort As Decimal = 0
        If IsNumeric(txtTerms.Text) Then
            Select Case cbMode.SelectedItem
                Case "Monthly"
                    decAmort = CDec(txtTerms.Text) * 1
                Case "Semi-Monthly"
                    decAmort = CDec(txtTerms.Text) * 2
                Case "Lumpsum"
                    decAmort = 1
                Case "Daily"
                    decAmort = CDec(txtTerms.Text) * 30
                Case "Weekly"
                    decAmort = CDec(txtTerms.Text) * 4
            End Select
        End If
        txtNoOfAmmort.Text = decAmort
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click

    End Sub

    Private Sub cmbInterest_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbInterest.SelectedIndexChanged

    End Sub
End Class