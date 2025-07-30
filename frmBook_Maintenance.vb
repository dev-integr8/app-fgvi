Public Class frmBook_Maintenance
    Dim activityResult As Boolean = True
    Dim ID As Integer

    Private Sub frmBook_Maintenance_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadBooks()
        LoadType()
    End Sub

    Private Sub LoadBooks()
        Dim query, Book As String
        query = " SELECT BOOKS " & vbCrLf &
                " FROM rptBook_Header "
        SQL.ReadQuery(query)
        cbBooks.Items.Clear()
        While SQL.SQLDR.Read
            Book = SQL.SQLDR("BOOKS").ToString
            cbBooks.Items.Add(Book)
        End While
    End Sub
    Private Sub LoadType()
        Dim query As String
        query = " SELECT * " &
               " FROM rptBook_Maintenance"
        SQL.ReadQuery(query)
        lvType.Items.Clear()
        While SQL.SQLDR.Read
            lvType.Items.Add(SQL.SQLDR("Record_ID").ToString)
            lvType.Items(lvType.Items.Count - 1).SubItems.Add(SQL.SQLDR("Type").ToString)
            lvType.Items(lvType.Items.Count - 1).SubItems.Add(SQL.SQLDR("AccountCode").ToString)
            lvType.Items(lvType.Items.Count - 1).SubItems.Add(SQL.SQLDR("Description").ToString)
        End While
    End Sub
    Protected Sub ClearText()
        cbBooks.Text = ""
        txtAccntCode.Text = ""
        txtAccntTitle.Text = ""
        txtSearch.Text = ""
    End Sub
    Protected Sub EnableControl(ByVal Value As Boolean)
        cbBooks.Enabled = Value
        txtAccntCode.ReadOnly = Not Value
        txtAccntTitle.ReadOnly = Not Value
    End Sub

    Private Sub txtAccntTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtAccntTitle.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmCOA_Search
            f.ShowDialog("AccntTitle", txtAccntTitle.Text)
            txtAccntTitle.Text = f.accttile
            txtAccntCode.Text = f.accountcode
            f.Dispose()
        End If
    End Sub

    Private Sub txtAccntTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtAccntTitle.TextChanged

    End Sub

    Private Sub btnNew_Click(sender As System.Object, e As System.EventArgs) Handles btnNew.Click
        ClearText()
        EnableControl(True)
        btnSave.Text = "Save"
        btnSave.Enabled = True
    End Sub

    Private Sub btnEdit_Click(sender As System.Object, e As System.EventArgs) Handles btnEdit.Click
        EnableControl(True)
        btnSave.Text = "Update"
        btnSave.Enabled = True
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Dim type As String = cbBooks.SelectedItem
        Dim Query As String = "  SELECT Count(Record_ID) as Count , Type " &
                                "   FROM rptBook_Maintenance   " &
                                " WHERE TYPE = '" & cbBooks.SelectedItem & "' " &
                                " GROUP BY  type "
        SQL.ReadQuery(Query)
        If SQL.SQLDR.Read Then
            Query = SQL.SQLDR("Count")
        Else
            Query = 0
        End If
        If type = "" Then
            MsgBox("Please enter type of Books", MsgBoxStyle.Exclamation)
        ElseIf txtAccntCode.Text = "" Then
            MsgBox("Please enter credit default entry for this collection type", MsgBoxStyle.Exclamation)
        ElseIf btnSave.Text = "Save" Then
            If Query >= 5 Then
                MsgBox("You reach Maximum account", MsgBoxStyle.Exclamation)
                ClearText()
                EnableControl(False)
            Else
                Try
                    Dim insertSQL As String
                    insertSQL = " INSERT INTO " & _
                                " rptBook_Maintenance(Type, AccountCode, Description) " & _
                                " VALUES(@Type, @AccountCode, @Description) "
                    SQL.FlushParams()
                    SQL.AddParam("@Type", cbBooks.SelectedItem)
                    SQL.AddParam("@AccountCode", txtAccntCode.Text)
                    SQL.AddParam("@Description", txtAccntTitle.Text)
                    SQL.ExecNonQuery(insertSQL)
                    MsgBox("New type saved Successfully!", MsgBoxStyle.Information)
                    ClearText()
                    EnableControl(False)
                    LoadType()
                    btnSave.Enabled = False

                Catch ex As Exception
                    MsgBox(ex.Message)
                    activityResult = False
                Finally
                    '  RecordActivity(UserID, moduleID, "INSERT", type, activityResult)
                    activityResult = True
                End Try
            End If
        ElseIf btnSave.Text = "Update" Then
            Try
                Dim updateSQL As String
                updateSQL = " UPDATE rptBook_Maintenance " & _
                            " SET       Type = @Type, AccountCode = @AccountCode, " & _
                            "        Description = @Description" & _
                            " WHERE  Record_ID = @Record_ID "
                SQL.FlushParams()
                SQL.AddParam("@Record_ID", ID)
                SQL.AddParam("@Type", cbBooks.SelectedItem)
                SQL.AddParam("@AccountCode", txtAccntCode.Text)
                SQL.AddParam("@Description", txtAccntTitle.Text)
                SQL.ExecNonQuery(updateSQL)
                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                EnableControl(False)
                LoadType()
                btnSave.Enabled = False
            Catch ex As Exception
                MsgBox(ex.Message)
                activityResult = False
            Finally
                '      RecordActivity(UserID, moduleID, "UPDATE", type, activityResult)
            End Try
        End If
    End Sub

    Private Sub btnRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnRemove.Click
        Try
            Dim DeleteSQL As String
            DeleteSQL = " DELETE FROM rptBook_Maintenance " & _
                         " WHERE  Record_ID = '" & ID & "' "
            SQL.ExecNonQuery(DeleteSQL)
            MsgBox("Record removed successfully!", MsgBoxStyle.Information)
            ClearText()
            EnableControl(False)
            LoadType()
        Catch ex As Exception
            MsgBox(ex.Message)
            activityResult = False
        Finally
            '   RecordActivity(UserID, moduleID, "REMOVE", "Collection Type", ID, activityResult)
            activityResult = True
        End Try
    End Sub

    Private Sub lvType_Click(sender As Object, e As System.EventArgs) Handles lvType.Click
        If lvType.SelectedItems.Count > 0 Then
            ID = lvType.Items(lvType.SelectedItems(0).Index).SubItems(chID.Index).Text()
            cbBooks.Text = lvType.Items(lvType.SelectedItems(0).Index).SubItems(chType.Index).Text()
            txtAccntCode.Text = lvType.Items(lvType.SelectedItems(0).Index).SubItems(chAccntCode.Index).Text()
            txtAccntTitle.Text = lvType.Items(lvType.SelectedItems(0).Index).SubItems(chAccntTitle.Index).Text()
            EnableControl(False)
        End If
    End Sub

    Private Sub lvType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lvType.SelectedIndexChanged

    End Sub
End Class