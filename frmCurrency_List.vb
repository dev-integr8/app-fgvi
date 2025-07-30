Public Class frmCurrency_List
    Public SelectedCodes As New List(Of String)
    Public Overloads Function ShowDialog(ByVal Codes As List(Of String)) As Boolean
        SelectedCodes = Codes
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmCurrency_List_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadList()
        If SelectedCodes.Count > 0 Then
            For Each item As ListViewItem In lvList.Items
                If SelectedCodes.Contains(item.Text) Then
                    item.Checked = True
                End If
            Next
        End If
    End Sub

    Private Sub LoadList()
        lvList.Items.Clear()
        Dim query As String
        query = " SELECT Code, Description FROM tblCurrency WHERE Status ='Active' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            lvList.Items.Add(SQL.SQLDR("Code").ToString)
            lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Description").ToString)
        End While
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        SelectedCodes.Clear()
        For Each item As ListViewItem In lvList.Items
            If item.Checked = True Then
                SelectedCodes.Add(item.Text)
            End If
        Next
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class