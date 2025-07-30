Public Class frmInit
    Dim SQL As New SQLControl


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Application.Exit()
    End Sub

    Private Sub btnPurchase_Click(sender As Object, e As EventArgs) Handles btnPurchase.Click
        If btnPurchase.Text = "Purchase" Then
            Dim url As String = "http://122.52.201.210:8083/gr8books/login.php"
            Process.Start(url)
            frmActivateSubscription.Show()
            Me.Hide()
        Else
            Try
                Dim validUntil As Date
                Dim SQL1 As New SQLControl("LICENSE SERVER")
                Dim query As String
                query = " SELECT MAX(valid_to) AS valid_to " & _
                        " FROM user_subscriptions " & _
                        " WHERE user_ID = @user_ID AND Status ='Completed' "
                SQL1.FlushParams()
                SQL1.AddParam("@user_ID", SubscriptionID)
                SQL1.ReadQuery(query)
                If SQL1.SQLDR.Read Then
                    validUntil = SQL.SQLDR("valid_to")
                    Dim updateSQL As String

                    updateSQL = " UPDATE tblLicenseInfo " & _
                                " SET    SubscriptionValidty = '" & validUntil & "'"
                    SQL.ExecNonQuery(updateSQL)

                    MsgBox("Your product subscription has been activated successfully! ", MsgBoxStyle.Information)
                    Me.Close()
                    isRegistered = True
                    SplashScreen.Show()
                    SplashScreen.Timer1.Start()
                Else
                    MsgBox("Invalid Key!, please enter the correct subscription ID and activation key.", MsgBoxStyle.Exclamation)
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
       
    End Sub

    Private Sub frmInit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If TrialExpired Then
            btnPurchase.Text = "Purchase"
            btnTry.Visible = True
            btnRegister.Text = "Register"
            btnTry.Enabled = False
            lblHeader.Text = "Gr8Books Windows Trial (Expired)"
            lblHeading.Text = "Your Gr8 Offline Trial has expired."
            lblDetails.Text = "Please purchase an activation key to continue using the Gr8Books system."
        ElseIf SubsExpired Then
            btnRegister.Text = "Renew Subscription"
            btnPurchase.Text = "Done"
            btnTry.Visible = False
            btnRegister.Text = ""
            btnTry.Enabled = False
            lblHeader.Text = "Gr8Books Windows Subscription (Expired)"
            lblHeading.Text = "Your Gr8Books Windows Subscription has expired."
            lblDetails.Text = "Please renew your subscription to continue using the Gr8Books system."
        End If
    End Sub

    Private Sub btnTry_Click(sender As Object, e As EventArgs) Handles btnTry.Click
        Dim insertSQL As String
        insertSQL = " UPDATE tblLicenseInfo " & _
                    " SET    IsInit=1, TrialStart = '" & Date.Today.Date & "'"
        SQL.ExecNonQuery(insertSQL)
        lsJADE.Show()
        lsJADE.Timer1.Start()
        Me.Close()
    End Sub

    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        If btnRegister.Text = "Register" Then
            Me.Close()
            frmActivateSubscription.Show()
        Else
            Dim url As String = "http://122.52.201.210:8083/gr8books/login.php"
            Process.Start(url)
        End If
    End Sub
End Class