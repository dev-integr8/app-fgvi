Public Class frmVerifier_DetailAR

    Dim SQL As New SQLControl
    Public strType As String = ""
    Public strAccntCode As String = ""
    Public strVCECode As String = ""
    Private Sub frmVerifier_DetailAR_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadMember()
        LoadAccount()
    End Sub

    Private Sub LoadMember()
        Dim selectSQL As String = "SELECT VCEName FROM viewVCE_Master WHERE VCECode = '" & strVCECode & "'"
        SQL.ReadQuery(selectSQL)
        If SQL.SQLDR.Read Then
            txtVCECode.Text = strVCECode
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
        End If
    End Sub

    Private Sub LoadAccount()
        Dim selectSQL As String = " SELECT DISTINCT AccntCode, AccountTitle FROM viewBS_Ledger " & vbCrLf & _
                                 " INNER JOIN tblCOA_Master ON tblCOA_Master.AccountCode = viewBS_Ledger.AccntCode " & vbCrLf & _
                                 " WHERE VCECode = '" & strVCECode & "' " & vbCrLf & _
                                 " ORDER BY AccountTitle "
        SQL.ReadQuery(selectSQL)
        cmbLoan.Items.Clear()
        Dim boolCode As Boolean = False
        While SQL.SQLDR.Read
            cmbLoan.Items.Add(SQL.SQLDR("AccountTitle").ToString)
        End While
        If cmbLoan.Items.Count > 0 And boolCode = False Then
            cmbLoan.SelectedIndex = 0
        End If
    End Sub

    Private Sub LoadTransactions()
        Dim strID As String = dgvList.SelectedRows(0).Cells(colAccountNumber.Index).Value
        Dim selectSQL As String = ""
        'selectSQL = "SELECT RefType, ReftransID, TransNo, AppDate, Debit, Credit, Balance, Remarks FROM viewBS_Ledger WHERE AccntCode = '" & strAccntCode & "' AND VCECode = '" & strVCECode & "' AND RefNo = '" & strID & "' ORDER BY No "
        selectSQL = "SELECT RefType, ReftransID, TransNo, AppDate, Debit, Credit, Balance, Remarks FROM viewBS_Ledger WHERE AccntCode = '" & strAccntCode & "' AND VCECode = '" & strVCECode & "' AND RefNo LIKE '" & strID & "%' ORDER BY No "
        SQL.ReadQuery(selectSQL)
        dgvLedger.Rows.Clear()
        Dim count As Integer = 1
        While SQL.SQLDR.Read
            If strAccntCode.Substring(0, 1) = "2" Or strAccntCode.Substring(0, 1) = "3" Or strAccntCode.Substring(0, 1) = "5" Then
                dgvLedger.Columns(colDeposit.Index).HeaderText = "Debit"
                dgvLedger.Columns(colWithdrawal.Index).HeaderText = "Credit"
                dgvLedger.Rows.Add(count, _
                                   SQL.SQLDR("RefType").ToString, _
                                   SQL.SQLDR("ReftransID").ToString, _
                                   SQL.SQLDR("TransNo").ToString, _
                                   CDate(SQL.SQLDR("AppDate").ToString).ToShortDateString, _
                                   CDec(SQL.SQLDR("Debit").ToString).ToString("N2"), _
                                   CDec(SQL.SQLDR("Credit").ToString).ToString("N2"), _
                                   CDec(SQL.SQLDR("Balance").ToString).ToString("N2"), _
                                   SQL.SQLDR("Remarks").ToString)
            Else
                dgvLedger.Columns(colDeposit.Index).HeaderText = "Debit"
                dgvLedger.Columns(colWithdrawal.Index).HeaderText = "Credit"
                dgvLedger.Rows.Add(count, _
                                   SQL.SQLDR("RefType").ToString, _
                                   SQL.SQLDR("ReftransID").ToString, _
                                   SQL.SQLDR("TransNo").ToString, _
                                   CDate(SQL.SQLDR("AppDate").ToString).ToShortDateString, _
                                   CDec(SQL.SQLDR("Debit").ToString).ToString("N2"), _
                                   CDec(SQL.SQLDR("Credit").ToString).ToString("N2"), _
                                   CDec(SQL.SQLDR("Balance").ToString).ToString("N2"), _
                                   SQL.SQLDR("Remarks").ToString)
            End If

            count += 1
        End While
        LoadBalance(strID)
    End Sub

    Private Sub LoadBalance(ByVal Refno As String)
        'Dim selectSQL As String = " SELECT CASE WHEN SUM(Debit - Credit) > 0 THEN SUM(Debit - Credit) ELSE SUM(Credit - Debit) END AS Balance FROM view_Ledger WHERE AccntCode = '" & strAccntCode & "' AND VCECode = '" & strVCECode & "' "
        'SQL.ReadQuery(selectSQL)
        'While SQL.SQLDR.Read
        '    lblBalance.Text = CDec(SQL.SQLDR("Balance").ToString).ToString("N2")
        'End While

        'Dim selectSQL = " SELECT TOP 1 * FROM viewBS_Ledger WHERE RefNo = '" & Refno & "' AND VCECode = '" & txtVCECode.Text & "' ORDER BY No DESC "
        Dim selectSQL = " SELECT CASE WHEN SUM(Debit - Credit) > 0 THEN SUM(Debit - Credit) ELSE SUM(Credit - Debit) END AS Balance FROM viewBS_Ledger WHERE RefNo LIKE '" & Refno & "%' AND VCECode = '" & txtVCECode.Text & "' AND AccntCode = '" & strAccntCode & "' "
        SQL.ReadQuery(selectSQL)
        If SQL.SQLDR.Read Then
            lblBalance.Text = CDec(SQL.SQLDR("Balance").ToString).ToString("N2")
        End If
    End Sub

    Private Sub dgvLedger_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLedger.CellContentClick

    End Sub

    Private Sub dgvLedger_CellMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvLedger.CellMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If e.ColumnIndex > -1 And e.RowIndex > -1 Then
                dgvLedger.CurrentCell = dgvLedger.Rows(e.RowIndex).Cells(e.ColumnIndex)
                cmsMenu.Show(New Point(MousePosition.X, MousePosition.Y))
            End If
        End If
    End Sub

    Private Sub ViewToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ViewToolStripMenuItem.Click
        Dim strRefType As String = dgvLedger.SelectedRows(0).Cells(colRefType.Index).Value
        Dim strRefTransID As String = dgvLedger.SelectedRows(0).Cells(colRefID.Index).Value
        Select Case strRefType
            Case "CV"
                Dim f As New frmCV
                f.ShowDialog(strRefTransID)
            Case "JV"
                Dim f As New frmJV
                f.ShowDialog(strRefTransID)
            Case "OR"
                Dim f As New frmCollection
                f.TransType = "OR"
                f.ShowDialog(strRefTransID)
            Case "APV"
                Dim f As New frmAPV
                f.ShowDialog(strRefTransID)
            Case "SJ"
                Dim f As New frmSJ
                f.ShowDialog(strRefTransID)
            Case "PJ"
                Dim f As New frmPJ
                f.ShowDialog(strRefTransID)
        End Select
    End Sub

    Private Sub lblBalance_Click(sender As System.Object, e As System.EventArgs) Handles lblBalance.Click

    End Sub

    Private Sub cmbLoan_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbLoan.SelectedIndexChanged
        LoadARList()
        lblBalance.Text = "0.00"
        dgvLedger.Rows.Clear()
    End Sub

    Private Sub LoadARList()
        'Dim selectSQL As String = " SELECT RefNo, SUM(Debit - Credit) AS Balance FROM viewBS_Ledger " & vbCrLf &
        '                          " WHERE VCECode = '" & strVCECode & "' " & vbCrLf &
        '                          " AND AccntCode = '" & LoadAccountCode(cmbLoan.SelectedItem) & "' " & vbCrLf &
        '                          " GROUP BY RefNo " & vbCrLf &
        '                          " ORDER BY RefNo "
        Dim selectSQL As String = " SELECT LEFT(RefNo, CHARINDEX('-', RefNo + '-') - 1) AS RefNo_Base , SUM(Debit - Credit) AS Balance FROM viewBS_Ledger " & vbCrLf &
                                  " WHERE VCECode = '" & strVCECode & "' " & vbCrLf &
                                  " AND AccntCode = '" & LoadAccountCode(cmbLoan.SelectedItem) & "' " & vbCrLf &
                                  " GROUP BY LEFT(RefNo, CHARINDEX('-', RefNo + '-') - 1) " & vbCrLf &
                                  " ORDER BY RefNo_Base "
        SQL.ReadQuery(selectSQL)
        dgvList.Rows.Clear()
        While SQL.SQLDR.Read
            dgvList.Rows.Add(SQL.SQLDR("RefNo_Base").ToString, CDec(SQL.SQLDR("Balance").ToString).ToString("N2"))
        End While
    End Sub

    Private Function LoadAccountCode(ByVal AccntTitle As String) As String
        Dim selectSQL As String = " SELECT AccountCode FROM tblCOA_Master " & vbCrLf & _
                                  " WHERE AccountTitle = '" & AccntTitle & "' "
        Dim strAccountCode As String = ""
        SQL.ReadQuery(selectSQL)
        While SQL.SQLDR.Read
            strAccountCode = SQL.SQLDR("AccountCode").ToString
        End While
        Return strAccountCode
    End Function

    Private Sub dgvList_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList.CellClick
        LoadTransactions()
    End Sub

    Private Sub dgvList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList.CellContentClick

    End Sub

    Private Sub SOAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SOAToolStripMenuItem.Click
        Dim f As New frmVerifier_DetailSOA
        f.Type = "AR"
        f.txtVCECode.Text = txtVCECode.Text
        f.txtVCEName.Text = txtVCEName.Text
        f.ShowDialog()
        f.Dispose()
    End Sub
End Class