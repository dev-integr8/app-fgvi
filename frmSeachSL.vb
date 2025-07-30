Public Class frmSeachSL

    Dim SQL As New SQLControl
    Public strVCECode As String = ""
    Public strVCEName As String = ""
    Public strAccntCode As String = ""
    Public strAccntTitle As String = ""
    Public strRefNo As String = ""
    Public strAmount As String = ""
    Dim disableEvent As Boolean = True

    Private Sub frmSeachSL_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        txtVCEName.Select()
        txtVCEName.Focus()
        LoadMember()
    End Sub

    Private Sub txtVCEName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
        If e.KeyCode = Keys.Up Then
            e.SuppressKeyPress = True
            If dgvSL.SelectedRows(0).Index > 0 Then
                dgvSL.CurrentCell = dgvSL.Rows(dgvSL.SelectedRows(0).Index - 1).Cells(0)
            End If
        ElseIf e.KeyCode = Keys.Down Then
            e.SuppressKeyPress = True
            If dgvSL.SelectedRows(0).Index < dgvSL.Rows.Count - 1 Then
                dgvSL.CurrentCell = dgvSL.Rows(dgvSL.SelectedRows(0).Index + 1).Cells(0)
            End If
        End If
        disableEvent = False
    End Sub

    Private Sub txtVCEName_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyUp
        If e.KeyCode = Keys.Enter Then
            If disableEvent = False Then
                Dim intRowIndex As Integer = dgvSL.SelectedRows(0).Index
                strVCECode = dgvSL.Rows(intRowIndex).Cells(colVCECode.Index).Value
                strVCEName = dgvSL.Rows(intRowIndex).Cells(colVCEName.Index).Value
                strAccntCode = dgvSL.Rows(intRowIndex).Cells(colAccntCode.Index).Value
                strAccntTitle = dgvSL.Rows(intRowIndex).Cells(colAccntTitle.Index).Value
                strRefNo = dgvSL.Rows(intRowIndex).Cells(colRefNo.Index).Value
                strAmount = dgvSL.Rows(intRowIndex).Cells(colBalance.Index).Value
                Me.Dispose()
            End If
        End If
    End Sub

    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged
        LoadMember()
    End Sub

    Private Sub LoadMember()
        Dim selectSQL As String = ""
        selectSQL = " SELECT TOP 50 * FROM View_SearchSL WHERE VCEName LIKE '%" & txtVCEName.Text & "%' ORDER BY VCEName, AccntCode "
        SQL.ReadQuery(selectSQL)
        dgvSL.Rows.Clear()
        While SQL.SQLDR.Read
            dgvSL.Rows.Add(SQL.SQLDR("VCECode").ToString, SQL.SQLDR("VCEName").ToString, SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccntTitle").ToString, SQL.SQLDR("RefNo").ToString, SQL.SQLDR("Balance").ToString)
        End While
    End Sub

    Private Sub dgvSL_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSL.CellContentClick
        
    End Sub

    Private Sub dgvSL_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSL.CellDoubleClick
        If e.RowIndex >= 0 Then
            strVCECode = dgvSL.Rows(e.RowIndex).Cells(colVCECode.Index).Value
            strVCEName = dgvSL.Rows(e.RowIndex).Cells(colVCEName.Index).Value
            strAccntCode = dgvSL.Rows(e.RowIndex).Cells(colAccntCode.Index).Value
            strAccntTitle = dgvSL.Rows(e.RowIndex).Cells(colAccntTitle.Index).Value
            strRefNo = dgvSL.Rows(e.RowIndex).Cells(colRefNo.Index).Value
            strAmount = dgvSL.Rows(e.RowIndex).Cells(colBalance.Index).Value
            Me.Dispose()
        End If
    End Sub

    Private Sub dgvSL_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles dgvSL.KeyDown
        If e.KeyCode = Keys.Enter Then
            If dgvSL.SelectedCells(0).RowIndex >= 0 Then
                strVCECode = dgvSL.Rows(dgvSL.SelectedCells(0).RowIndex).Cells(colVCECode.Index).Value
                strVCEName = dgvSL.Rows(dgvSL.SelectedCells(0).RowIndex).Cells(colVCEName.Index).Value
                strAccntCode = dgvSL.Rows(dgvSL.SelectedCells(0).RowIndex).Cells(colAccntCode.Index).Value
                strAccntTitle = dgvSL.Rows(dgvSL.SelectedCells(0).RowIndex).Cells(colAccntTitle.Index).Value
                strRefNo = dgvSL.Rows(dgvSL.SelectedCells(0).RowIndex).Cells(colRefNo.Index).Value
                strAmount = dgvSL.Rows(dgvSL.SelectedCells(0).RowIndex).Cells(colBalance.Index).Value
                Me.Dispose()
            End If
        End If
    End Sub
End Class