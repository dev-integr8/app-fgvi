Public Class frmSearchList
    Public rowInd As Integer
    Public modFrom As String = ""
    Public field As String = ""
    Public fieldDesc As String = ""
    Public result As String = ""
    Dim disableEvent As Boolean = False
    Public Overloads Function ShowDialog(ByVal ModID As String, FieldName As String, FieldDescription As String, Optional Value As String = "") As Boolean
        modFrom = ModID
        field = FieldName
        fieldDesc = FieldDescription
        txtFilter.Text = Value
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmSearchVendor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadList()
    End Sub

    Private Sub LoadList()
        dgvList.DataSource = Nothing
        SQL.FlushParams()
        Dim filter As String = ""
        Dim param As String = "@" & field
        If txtFilter.Text <> "" Then
            filter = " AND " & field & " LIKE '%' + " & param & " + '%' "
            SQL.AddParam(param, txtFilter.Text)
        End If
        Dim query As String = ""
        Select Case modFrom
            Case "IPL"
                query = " SELECT DISTINCT " & field & " AS Data  " &
                        " FROM viewItem_SellingPrice " &
                        " WHERE " & field & " <> '' " & filter &
                        " ORDER BY Data  "
        End Select
        If query <> "" Then
            SQL.GetQuery(query)
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                dgvList.DataSource = SQL.SQLDS.Tables(0)
                dgvList.Columns(0).HeaderCell.Value = fieldDesc
                dgvList.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If
        End If
    End Sub

    Private Sub dgvList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellDoubleClick
        If dgvList.CurrentRow.Index <> -1 Then
            result = dgvList.CurrentRow.Cells(0).Value.ToString
            Me.Close()
        End If
    End Sub

    Private Sub dgvList_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvList.KeyDown
        If e.KeyCode = Keys.Enter Then
            If dgvList.CurrentRow.Index <> -1 Then
                result = dgvList.CurrentRow.Cells(0).Value.ToString
                Me.Close()
            End If
        End If
    End Sub

    Private Sub txtFilter_TextChanged(sender As Object, e As EventArgs) Handles txtFilter.TextChanged
        LoadList()
    End Sub

    Private Sub frmSearchList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Up OrElse e.KeyCode = Keys.Down Then
            If Me.ActiveControl IsNot dgvList Then
                dgvList.Select()
                If dgvList.Rows.Count > 0 Then
                    If e.KeyCode = Keys.Up Then
                        If dgvList.CurrentCell.RowIndex > 0 Then
                            Dim rowIndex As Integer = dgvList.CurrentCell.RowIndex - 1
                            dgvList.CurrentCell = Nothing
                            dgvList.CurrentCell = dgvList.Item(0, rowIndex)
                            e.SuppressKeyPress = True
                        End If
                    ElseIf e.KeyCode = Keys.Down Then
                        If dgvList.CurrentCell.RowIndex + 1 < dgvList.Rows.Count Then
                            Dim rowIndex As Integer = dgvList.CurrentCell.RowIndex + 1
                            dgvList.CurrentCell = Nothing
                            dgvList.CurrentCell = dgvList.Item(0, rowIndex)
                            e.SuppressKeyPress = True
                        End If
                    End If
                End If

            End If
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            If dgvList.CurrentRow.Index <> -1 Then
                result = dgvList.CurrentRow.Cells(0).Value.ToString
                Me.Close()
            End If
        ElseIf e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z Then

            If Me.ActiveControl IsNot txtFilter Then
                txtFilter.Select()
                txtFilter.Text = Chr(e.KeyCode)
                txtFilter.SelectionStart = txtFilter.TextLength
            End If
        End If
    End Sub
End Class
