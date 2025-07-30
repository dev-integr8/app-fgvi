Public Class FrmCollector_Search
    Public CollectorID, CollectorName As String


    Private Sub FrmCollector_Search_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbFilter.SelectedIndex = 1
        cbStatus.SelectedIndex = 0
        LoadList()
    End Sub


    Private Sub LoadList()

        If cbStatus.SelectedIndex <> -1 Then
            Dim query As String
            query = " SELECT     Collector_ID, Collector_Name, Status " & _
                    " FROM       tblCollector_Master " & _
                    " WHERE      " & cbFilter.SelectedItem & " LIKE '%" & txtFilter.Text & "%' AND Status ='" & cbStatus.SelectedItem & "' "

            SQL.ReadQuery(query)
            lvList.Items.Clear()
            While SQL.SQLDR.Read
                lvList.Items.Add(SQL.SQLDR("Collector_ID").ToString)
                lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Collector_Name").ToString)
                lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Status").ToString)
            End While
        End If
    End Sub

    Private Sub lvList_DoubleClick(sender As Object, e As EventArgs) Handles lvList.DoubleClick
        If lvList.SelectedItems.Count = 1 Then
            CollectorID = lvList.SelectedItems(0).SubItems(chCollectorID.Index).Text
            CollectorName = lvList.SelectedItems(0).SubItems(chCollectorName.Index).Text
            Me.Close()
        End If
    End Sub

    Private Sub lvList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvList.SelectedIndexChanged

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadList()
    End Sub

    Private Sub txtFilter_TextChanged(sender As Object, e As EventArgs) Handles txtFilter.TextChanged
        LoadList()
    End Sub
End Class