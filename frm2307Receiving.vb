Public Class frm2307Receiving
    Public Amount As Decimal
    Public TransID As String
    Private Sub frm2307Receiving_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txt2307Amount.Text = Amount


    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        '2307 Tagging
        If txt2307Amount.Text = "" Then
            Msg("Please check amount!", MsgBoxStyle.Information)
        Else
            SaveTagging()
        Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
            Me.Close()
        End If
    End Sub

    Private Sub SaveTagging()

        Dim query As String
        query = "UPDATE tblCollection SET DateReceived_2307 = @DateReceived_2307, Amount_2307 = @Amount_2307 WHERE TransID = @TransID"
        SQL.FlushParams()
        SQL.AddParam("@DateReceived_2307", dtp2307.Value)
        SQL.AddParam("@Amount_2307", CDec(txt2307Amount.Text))
        SQL.AddParam("@TransID", TransID)
        SQL.ExecNonQuery(query)

    End Sub
End Class