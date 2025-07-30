Public Class frmCheckTemplates

    Private Sub frmCheckTemplates_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTemplates()
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim f As New frmCheck_Designer
        f.ShowDialog()
        f.Dispose()
        LoadTemplates()
    End Sub
    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If lvTemplates.SelectedItems.Count = 1 Then
            If MsgBox("You're about to delete this template, please confirm", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim updateSQL As String
                updateSQL = " UPDATE tblCheck_Template " & _
                            " SET    STatus ='Inactive' " & _
                            " WHERE  ID = @ID"
                SQL.FlushParams()
                SQL.AddParam("@ID", lvTemplates.SelectedItems(0).Text)
                SQL.ExecNonQuery(updateSQL)
                LoadTemplates()
            End If
        End If
    End Sub

    Private Sub LoadTemplates()
        Dim query As String
        lvTemplates.Items.Clear()
        query = " SELECT ID, TemplateName FROM tblCheck_Template  WHERE Status ='Active'"
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            lvTemplates.Items.Add(SQL.SQLDR("ID").ToString)
            lvTemplates.Items(lvTemplates.Items.Count - 1).SubItems.Add(SQL.SQLDR("TemplateName").ToString)
        End While
    End Sub

    Private Sub btnModify_Click(sender As Object, e As EventArgs) Handles btnModify.Click
        If lvTemplates.SelectedItems.Count = 1 Then
            Dim f As New frmCheck_Designer
            f.ShowDialog(lvTemplates.SelectedItems(0).Text)
            f.Dispose()
            LoadTemplates()
        Else
            MsgBox("Please select template to edit", MsgBoxStyle.Exclamation)
        End If
    End Sub
End Class