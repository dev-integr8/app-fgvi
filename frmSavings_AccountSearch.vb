Public Class frmSavings_AccountSearch

    Dim SQL As New SQLControl
    Public strVCECode As String = ""
    Public strVCEName As String = ""
    Public strDepositType As String = ""
    Public strAccountNumber As String = ""
    Private Sub frmSavings_AccountSearch_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadList()
    End Sub

    Private Sub LoadList()
        Dim selectSQL As String = "SELECT  tblSavings_Account.*, VCEName FROM tblSavings_Account INNER JOIN viewVCE_Master ON tblSavings_Account.VCECode = viewVCE_Master.VCECode WHERE VCEName LIKE '%" & txtVCEName.Text & "%'"
        SQL.ReadQuery(selectSQL)
        dgvSavings.Rows.Clear()
        While SQL.SQLDR.Read
            dgvSavings.Rows.Add(SQL.SQLDR("VCECode").ToString, SQL.SQLDR("VCEName").ToString, SQL.SQLDR("DepositType").ToString, SQL.SQLDR("AccountNumber").ToString)
        End While
    End Sub

    Private Sub dgvSavings_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSavings.CellDoubleClick
        If dgvSavings.SelectedCells.Count > 0 Then
            strVCECode = dgvSavings.Rows(dgvSavings.SelectedCells(0).RowIndex).Cells(colVCECode.Index).Value
            strVCEName = dgvSavings.Rows(dgvSavings.SelectedCells(0).RowIndex).Cells(colVCEName.Index).Value
            strDepositType = dgvSavings.Rows(dgvSavings.SelectedCells(0).RowIndex).Cells(colDepositType.Index).Value
            strAccountNumber = dgvSavings.Rows(dgvSavings.SelectedCells(0).RowIndex).Cells(colAccountNumber.Index).Value
            Me.Dispose()
        End If
    End Sub

    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged
        LoadList()
    End Sub
End Class