Public Class frmCOA_Search
    Public COA_Group As String
    Public accountcode, accttile As String
    Dim FilterText As String

    Public Overloads Function ShowDialog(fld As String, ByVal Accttle As String) As Boolean
        If fld = "AccntCode" Then
            cbFilter.SelectedItem = "Account Code"
        Else
            cbFilter.SelectedItem = "Account Title"
        End If
        txtSearch.Text = Accttle
        FilterText = Accttle
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub FrmChartOfAccountSearch_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LoadAccounts()
        If FilterText = "" Then txtSearch.Select()

        If cbFilter.SelectedItem = "Account Code" AndAlso dgvSearchList.Rows.Count = 1 Then
            accountcode = dgvSearchList.Item(0, 0).Value
            accttile = dgvSearchList.Item(1, 0).Value
            COA_Group = dgvSearchList.Item(2, 0).Value
            Me.Close()
        End If
    End Sub

    Private Sub LoadAccounts()
        dgvSearchList.Rows.Clear()
        Dim query As String = ""
        Select Case cbFilter.SelectedItem
            Case "Account Code"
                query = "SELECT  AccountCode, AccountTitle, COA_Group " & _
                       " FROM    tblCOA_Master " & _
                       " WHERE   AccountNature <> '' AND AccountCode LIKE '%' + @Filter + '%' AND AccountGroup = 'SubAccount' AND Status = 'Active'" & _
                       " ORDER BY AccountCode"
            Case "Account Title"
                query = "SELECT  AccountCode, AccountTitle, COA_Group " & _
                       " FROM    tblCOA_Master" & _
                       " WHERE   AccountNature <> '' AND AccountTitle LIKE '%' + @Filter + '%' AND AccountGroup = 'SubAccount' AND Status = 'Active'" & _
                       " ORDER BY AccountTitle"
        End Select
        SQL.FlushParams()
        SQL.AddParam("@Filter", txtSearch.Text)
        SQL.ReadQuery(query)
        If SQL.SQLDR.HasRows Then
            While SQL.SQLDR.Read
                dgvSearchList.Rows.Add(New String() {SQL.SQLDR("AccountCode").ToString, _
                                                     SQL.SQLDR("AccountTitle").ToString, _
                                                     SQL.SQLDR("COA_Group").ToString})
            End While
        Else
            If FilterText <> "" Then
                MsgBox("No Record Found!", MsgBoxStyle.Exclamation)
                accountcode = ""
                accttile = ""
                Me.Close()
            End If
        End If
    End Sub
    Private Sub dgvSearchBPPO_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvSearchList.CellMouseDoubleClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            accountcode = dgvSearchList.Item(0, e.RowIndex).Value
            accttile = dgvSearchList.Item(1, e.RowIndex).Value
            COA_Group = dgvSearchList.Item(2, e.RowIndex).Value
            Me.Close()
        End If
    End Sub

    Private Sub dgvSearchBPPO_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles dgvSearchList.KeyDown
        If e.KeyCode = Keys.Enter Then
            If dgvSearchList.SelectedRows.Count > 0 Then
                accountcode = dgvSearchList.SelectedRows(0).Cells(0).Value.ToString
                accttile = dgvSearchList.SelectedRows(0).Cells(1).Value.ToString
                COA_Group = dgvSearchList.SelectedRows(0).Cells(2).Value.ToString
            End If
            Me.Close()
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadAccounts()
    End Sub
End Class