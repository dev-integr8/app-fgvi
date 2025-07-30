Public Class frmActivateSubscription

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        frmInit.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        Try
            Dim validUntil As Date
            Dim SQL1 As New SQLControl("LICENSE SERVER")
            Dim query As String
            query = " SELECT subscription_status, valid_to " & _
                    " FROM users INNER JOIN user_subscriptions " & _
                    " ON users.ID = user_subscriptions.user_id " & _
                    " WHERE txn_id = @txn_id AND user_ID = @user_ID AND Status ='Completed' "
            SQL1.FlushParams()
            SQL1.AddParam("@user_ID", txtSubsID.Text)
            SQL1.AddParam("@txn_id", txtActivationKey.Text)
            SQL1.ReadQuery(query)
            If SQL1.SQLDR.Read Then
                validUntil = SQL.SQLDR("valid_to")
                If SQL1.SQLDR("subscription_status") = "Active" Then
                    MsgBox("Activation key already used!", MsgBoxStyle.Exclamation)
                Else
                    Dim updateSQL As String
                    updateSQL = " UPDATE    users " & _
                                " SET       subscription_status = 'ACtive' " & _
                                " WHERE     ID = @ID "
                    SQL1.FlushParams()
                    SQL1.AddParam("@ID", txtSubsID.Text)
                    SQL1.ExecNonQuery(updateSQL)

                    updateSQL = " UPDATE tblLicenseInfo " & _
                                " SET    IsInit= 2, SubscriptionValidty = '" & validUntil & "'"
                    SQL.ExecNonQuery(updateSQL)

                    MsgBox("Your product subscription has been activated successfully! ", MsgBoxStyle.Information)
                    Me.Close()
                    isRegistered = True
                    SplashScreen.Show()
                    SplashScreen.Timer1.Start()
                End If
            Else
                MsgBox("Invalid Key!, please enter the correct subscription ID and activation key.", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class