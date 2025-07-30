Public Class frmAbout
    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not isRegistered Then
            If TrialExpired Then
                lblTitle.Text = "Gr8Books (Trial Expired)"
            Else
                lblTitle.Text = "Gr8Books (Trial " & DaysLeft & " Days Left)"
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub
End Class