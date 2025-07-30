Public Class frmActivateTrial

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        frmInit.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        Try
            Dim SQL1 As New SQLControl("LICENSE SERVER")
            Dim query As String
            query = " SELECT trial_used AS isActivated  FROM users WHERE email = @Email AND trial_code = @ActivationKey "
            SQL1.FlushParams()
            SQL1.AddParam("@Email", txtEmail.Text)
            SQL1.AddParam("@ActivationKey", txtActivationKey.Text)
            SQL1.ReadQuery(query)
            If SQL1.SQLDR.Read Then
                If SQL1.SQLDR("isActivated") = "Active" Then
                    MsgBox("Activation key already used!", MsgBoxStyle.Exclamation)
                Else
                    Dim updateSQL As String
                    updateSQL = " UPDATE    users " & _
                                " SET       trial_used = 'Active', trial_startdate = GETDATE() " & _
                                " WHERE     email = @Email AND trial_code = @ActivationKey "
                    SQL1.FlushParams()
                    SQL1.AddParam("@Email", txtEmail.Text)
                    SQL1.AddParam("@ActivationKey", txtActivationKey.Text)
                    SQL1.ExecNonQuery(updateSQL)

                    Dim insertSQL As String
                    insertSQL = " UPDATE tblLicenseInfo " & _
                                " SET    IsInit=1, TrialDays = 15, TrialStart = '" & Date.Today.Date & "'"
                    If SQL.ExecNonQuery(insertSQL) = 0 Then

                        insertSQL = " INSERT INTO  tblLicenseInfo(IsInit, TrialDays, TrialStart) " & _
                                    " VALUES(1, 15, '" & Date.Today.Date & "')"
                        SQL.ExecNonQuery(insertSQL)
                    End If

                    MsgBox("Trial Version Remaining Days: 15 days!", MsgBoxStyle.Information)
                    Me.Close()
                    isTrial = True
                    SplashScreen.Show()
                    SplashScreen.Timer1.Start()
                End If
            Else
                MsgBox("Invalid Key!, please enter the correct email and activation key.", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SetDatabase()
        End Try
    End Sub
End Class