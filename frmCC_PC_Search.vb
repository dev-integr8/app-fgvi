Public Class frmCC_PC_Search
    Public Type As String
    Public Code, Description As String
    Public FilterText As String

    'Public Overloads Function ShowDialog(fld As String, ByVal Desc As String) As Boolean
    '    Type = fld
    '    txtSearch.Text = Desc
    '    FilterText = Desc
    '    MyBase.ShowDialog()
    '    Return True
    'End Function


    Private Sub LoadDescription()
        dgvSearchList.Rows.Clear()
        Dim query As String = ""
        Select Case Type
            Case "Cost Center"
                query = "SELECT  Code, Description" & _
                       " FROM    tblCC " & _
                       " WHERE   Description LIKE '%' + @Filter + '%' AND Status = 'Active'" & _
                       " ORDER BY Description"
            Case "Profit Center"
                query = "SELECT  Code, Description" & _
                       " FROM    tblPC " & _
                       " WHERE   Description LIKE '%' + @Filter + '%' AND Status = 'Active'" & _
                       " ORDER BY Description"
        End Select
        SQL.FlushParams()
        SQL.AddParam("@Filter", txtSearch.Text)
        SQL.ReadQuery(query)
        If SQL.SQLDR.HasRows Then
            While SQL.SQLDR.Read
                dgvSearchList.Rows.Add(New String() {SQL.SQLDR("Code").ToString, _
                                                     SQL.SQLDR("Description").ToString})
            End While
            'If dgvSearchList.Rows.Count = 1 Then
            '    Code = dgvSearchList.Item(0, 0).Value
            '    Description = dgvSearchList.Item(1, 0).Value
            '    Me.Close()
            'End If
        Else
            If FilterText <> "" Then
                MsgBox("No Record Found!", MsgBoxStyle.Exclamation)
                Code = ""
                Description = ""
                Me.Close()
            End If
        End If
    End Sub
    Private Sub dgvSearchBPPO_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvSearchList.CellMouseDoubleClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Code = dgvSearchList.Item(0, e.RowIndex).Value
            Description = dgvSearchList.Item(1, e.RowIndex).Value
            Me.Close()
        End If
    End Sub

    Private Sub dgvSearchBPPO_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles dgvSearchList.KeyDown
        If e.KeyCode = Keys.Enter Then
            If dgvSearchList.SelectedRows.Count > 0 Then
                Code = dgvSearchList.SelectedRows(0).Cells(0).Value.ToString
                Description = dgvSearchList.SelectedRows(0).Cells(1).Value.ToString
            End If
            Me.Close()
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadDescription()
    End Sub

    Private Sub frmCC_PC_Search_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDescription()
        If FilterText = "" Then txtSearch.Select()
    End Sub
End Class