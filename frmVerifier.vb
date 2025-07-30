Public Class frmVerifier

    Dim SQL As New SQLControl
    Private Sub frmVerifier_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        cbFilter.SelectedIndex = 0
    End Sub

    Dim boolFilter As Boolean = False
    Private Sub txtFilter_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtFilter.KeyUp
        Select Case e.KeyCode
            Case Keys.Up
                If lbAutoComplete.SelectedIndex > 0 Then
                    lbAutoComplete.SelectedIndex = lbAutoComplete.SelectedIndex - 1
                Else
                    lbAutoComplete.SelectedIndex = lbAutoComplete.Items.Count - 1
                End If
            Case Keys.Down
                If lbAutoComplete.SelectedIndex < lbAutoComplete.Items.Count - 1 And lbAutoComplete.SelectedIndex >= 0 Then
                    lbAutoComplete.SelectedIndex = lbAutoComplete.SelectedIndex + 1
                Else
                    lbAutoComplete.SelectedIndex = 0
                End If
            Case Keys.Enter
                If lbAutoComplete.Items.Count > 0 Then
                    boolFilter = True
                    If lbAutoComplete.SelectedIndex = -1 Then
                        txtFilter.Text = lbAutoComplete.Items(0).ToString
                    Else
                        txtFilter.Text = lbAutoComplete.SelectedItem
                    End If
                End If
            Case Keys.Escape
                lbAutoComplete.Visible = False
        End Select
    End Sub

    Private Sub txtFilter_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtFilter.TextChanged
        If boolFilter = False And txtFilter.Text.Length > 0 Then
            SQL.CloseCon()
            Dim selectSQL As String = "SELECT TOP 10 * FROM viewVCE_Master WHERE " & cbFilter.SelectedItem & " LIKE '" & txtFilter.Text.Replace("'", "''") & "%' ORDER BY VCEName "
            SQL.ReadQuery(selectSQL)
            lbAutoComplete.Items.Clear()
            While SQL.SQLDR.Read
                lbAutoComplete.Items.Add(SQL.SQLDR(cbFilter.SelectedItem).ToString)
            End While
            If lbAutoComplete.Items.Count > 0 Then
                lbAutoComplete.Width = txtFilter.Width
                lbAutoComplete.Height = IIf(lbAutoComplete.Items.Count = 1, 17, lbAutoComplete.Items.Count * 15)
                lbAutoComplete.Location = New Point(txtFilter.Location.X, txtFilter.Location.Y + txtFilter.Height - 1)
                lbAutoComplete.Visible = True
                txtFilter.Select()
                txtFilter.Focus()
            Else
                lbAutoComplete.Visible = False
            End If
        Else
            boolFilter = False
            lbAutoComplete.Visible = False
        End If
        loadSL()
    End Sub

    Private Sub loadSL()
        Dim selectSQL As String = " SELECT TOP 50 View_SL.*  FROM View_SL " & _
                                    " INNER JOIN  tblCOA_Master ON " & _
                                    " tblCOA_Master.AccountCode = View_SL.AccntCode AND withSubsidiary = 1 " & _
                                    " WHERE " & cbFilter.SelectedItem & " LIKE '%" & txtFilter.Text.Replace("'", "''") & "%' ORDER BY VCECode, VCEName, AccntCode "
        SQL.ReadQuery(selectSQL)
        dgvSL.Rows.Clear()
        While SQL.SQLDR.Read
            dgvSL.Rows.Add(SQL.SQLDR("VCECode").ToString, SQL.SQLDR("VCEName").ToString, SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccountTitle").ToString, CDec(SQL.SQLDR("Debit")).ToString("N2"), CDec(SQL.SQLDR("Credit")).ToString("N2"))
            Dim dgvButton As New DataGridViewButtonCell
            dgvButton.Value = "L"
            dgvButton.Style.Font = New Font("Webdings", 12)
            dgvSL.Rows(dgvSL.Rows.Count - 1).Cells(colButton.Index) = dgvButton
        End While
    End Sub

    Private Sub dgvSL_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSL.CellContentClick
        Dim intCol As Integer = e.ColumnIndex
        Dim intRow As Integer = e.RowIndex
        lbAutoComplete.Visible = False
        Select Case intCol
            Case colButton.Index
                Dim strAccntCode As String = dgvSL.SelectedRows(0).Cells(colAccntCode.Index).Value
                Select Case SearchTypeSL(strAccntCode)
                    Case "Loan"
                        Dim f As New frmVerifier_DetailLoan
                        f.strVCECode = dgvSL.SelectedRows(0).Cells(colVCECode.Index).Value
                        f.strAccntCode = dgvSL.SelectedRows(0).Cells(colAccntCode.Index).Value
                        f.Show()
                    Case "Savings"
                        Dim f As New frmVerifier_DetailSavings
                        f.strVCECode = dgvSL.SelectedRows(0).Cells(colVCECode.Index).Value
                        f.strAccntCode = dgvSL.SelectedRows(0).Cells(colAccntCode.Index).Value
                        f.Show()
                    Case "AR"
                        Dim f As New frmVerifier_DetailAR
                        f.strVCECode = dgvSL.SelectedRows(0).Cells(colVCECode.Index).Value
                        f.strAccntCode = dgvSL.SelectedRows(0).Cells(colAccntCode.Index).Value
                        f.Show()
                    Case Else
                        Dim f As New frmVerifier_DetailOthers
                        f.strVCECode = dgvSL.SelectedRows(0).Cells(colVCECode.Index).Value
                        f.strAccntCode = dgvSL.SelectedRows(0).Cells(colAccntCode.Index).Value
                        f.Show()
                End Select
        End Select
    End Sub

    Private Function SearchTypeSL(ByVal strAccntCode As String) As String
        Dim selectSQL As String = ""
        Dim strType As String = ""
        'Search if Loan
        selectSQL = " SELECT * FROM tblLoan_Type WHERE LoanAccount = '" & strAccntCode & "' "
        SQL.ReadQuery(selectSQL)
        If SQL.SQLDR.Read Then
            strType = "Loan"
        End If
        'Search if Savings
        selectSQL = " SELECT TOP 1 VCECode FROM viewSavings WHERE AccntCode = '" & strAccntCode & "' AND VCECode = '" & dgvSL.Rows(dgvSL.SelectedCells(0).RowIndex).Cells(colVCECode.Index).Value & "' "
        SQL.ReadQuery(selectSQL)
        If SQL.SQLDR.Read Then
            strType = "Savings"
        End If

        'Search if AR
        selectSQL = " SELECT TOP 1 VCECode FROM viewBS_Ledger WHERE AccntCode = '" & strAccntCode & "' AND VCECode = '" & dgvSL.Rows(dgvSL.SelectedCells(0).RowIndex).Cells(colVCECode.Index).Value & "' "
        SQL.ReadQuery(selectSQL)
        If SQL.SQLDR.Read Then
            strType = "AR"
        End If
        Return strType
    End Function

    Private Sub lbAutoComplete_Click(sender As Object, e As System.EventArgs) Handles lbAutoComplete.Click
        If lbAutoComplete.SelectedItems.Count > 0 Then
            txtFilter.Text = lbAutoComplete.SelectedItem.ToString
            lbAutoComplete.Visible = False
        End If
    End Sub
End Class