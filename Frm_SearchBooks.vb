Public Class Frm_SearchBooks
    Public UploadID, BookType As String

    Private Sub Frm_SearchBooks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbFilter.SelectedItem = "UploadID"
        LoadBooks()
    End Sub

    Private Sub LoadBooks()

        If cbFilter.SelectedIndex <> 1 Then
            Dim query As String
            query = " SELECT UploadID, RefType, Book, TotalDBCR, Remarks  " & _
                    " FROM tblJE_Header " & _
                    " WHERE      " & cbFilter.SelectedItem & " LIKE '%" & txtFilter.Text & "%' AND UploadID is not null"

            SQL.ReadQuery(query)
            lvList.Items.Clear()
            While SQL.SQLDR.Read
                lvList.Items.Add(SQL.SQLDR("UploadID").ToString)
                lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("RefType").ToString)
                lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Book").ToString)
                lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("TotalDBCR").ToString)
                lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Remarks").ToString)

            End While
        End If
    End Sub

    Private Sub lvList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvList.SelectedIndexChanged

    End Sub

    Private Sub lvList_DoubleClick(sender As Object, e As EventArgs) Handles lvList.DoubleClick
        If lvList.SelectedItems.Count = 1 Then
            UploadID = lvList.SelectedItems(0).SubItems(chID.Index).Text
            BookType = lvList.SelectedItems(0).SubItems(chType.Index).Text
            Me.Close()
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadBooks()
    End Sub

    Private Sub txtFilter_TextChanged(sender As Object, e As EventArgs) Handles txtFilter.TextChanged
        LoadBooks()
    End Sub
End Class