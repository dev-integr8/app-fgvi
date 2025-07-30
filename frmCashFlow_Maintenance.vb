Public Class frmCashFlow_Maintenance
    Private Sub frmCashFlow_Maintenance_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LoadAccountGroup()

        cbType.SelectedItem = "Group"

        LoadList()

    End Sub

    Private Sub LoadAccountGroup()
        Dim query As String = "SELECT AccountGroup FROM tblCOA_AccountGroup ORDER BY Hierarchy"

        cbType.Items.Clear()

        SQL.FlushParams()
        SQL.ReadQuery(query)

        While SQL.SQLDR.Read()
            cbType.Items.Add(SQL.SQLDR("AccountGroup").ToString())
        End While
    End Sub

    Private Sub LoadList()
        Dim query As String = "SELECT 1 AS No, AccountCode AS Code, AccountTitle, SubCategory, SCF_Group FROM tblCOA_Master " &
                              "WHERE AccountType = 'Balance Sheet' AND AccountGroup = @AccountGroup AND Status = 'Active' " &
                              "ORDER BY OrderNo"

        SQL.FlushParams()
        SQL.AddParam("@AccountGroup", cbType.SelectedItem)

        SQL.GetQuery(query)

        dgvCashFlow.DataSource = SQL.SQLDS.Tables(0)
    End Sub

    Private Sub dgvCashFlow_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvCashFlow.RowPostPaint
        Dim rowNumber As Integer = e.RowIndex + 1
        dgvCashFlow.Rows(e.RowIndex).Cells("chNo").Value = rowNumber.ToString()
    End Sub

    Private Sub cbType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbType.SelectedIndexChanged
        If cbType.SelectedIndex <> -1 Then
            LoadList()
        End If
    End Sub

    Private Sub tsbEdit_Click(sender As Object, e As EventArgs) Handles tsbEdit.Click
        If dgvCashFlow.SelectedRows.Count = 1 Then
            frmCashFlow_Add.txtCode.ReadOnly = True
            frmCashFlow_Add.txtDescription.ReadOnly = True
            frmCashFlow_Add.ShowDialog(dgvCashFlow.SelectedRows(0).Cells(1).Value.ToString)
        End If
    End Sub
End Class