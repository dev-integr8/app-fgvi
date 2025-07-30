Public Class frmVerifier_DetailSavings

    Public strVCECode As String = ""
    Public strAccntCode As String = ""
    Dim RefNo As String = ""
    Dim strMaxPage As String = ""
    Dim decMaxLine As Decimal = 20
    Private Sub frmVerifier_DetailLoan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadVCE()
        LoadSavingsType()
        LoadSavingsList()
    End Sub

    Private Sub LoadVCE()
        Dim selectSQL As String = " SELECT * FROM viewVCE_Master WHERE VCECode = '" & strVCECode & "' "
        SQL.ReadQuery(selectSQL)
        While SQL.SQLDR.Read
            txtVCECode.Text = strVCECode
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
        End While
    End Sub

    Private Sub LoadSavingsList()
        Dim selectSQL As String = " SELECT RefNo, SUM(Deposit + Interest - Withdrawal) AS Balance FROM viewSavings " & vbCrLf & _
                                  " WHERE VCECode = '" & strVCECode & "' " & vbCrLf & _
                                  " AND AccntCode = '" & LoadAccountCode(cmbLoan.SelectedItem) & "' " & vbCrLf & _
                                  " GROUP BY RefNo " & vbCrLf & _
                                  " ORDER BY RefNo "
        SQL.ReadQuery(selectSQL)
        dgvList.Rows.Clear()
        While SQL.SQLDR.Read
            dgvList.Rows.Add(SQL.SQLDR("RefNo").ToString, CDec(SQL.SQLDR("Balance").ToString).ToString("N2"))
        End While
    End Sub

    Private Sub LoadSavingsType()
        Dim selectSQL As String = " SELECT DISTINCT DepositType FROM tblSavings_Account " & vbCrLf & _
                                  " WHERE VCECode = '" & strVCECode & "' " & vbCrLf & _
                                  " ORDER BY DepositType "
        SQL.ReadQuery(selectSQL)
        cmbLoan.Items.Clear()
        Dim boolCode As Boolean = False
        While SQL.SQLDR.Read
            cmbLoan.Items.Add(SQL.SQLDR("DepositType").ToString)
        End While
        If cmbLoan.Items.Count > 0 And boolCode = False Then
            cmbLoan.SelectedIndex = 0
        End If
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

    Private Sub LoadSavings()
        If dgvList.SelectedRows.Count > 0 Then
            Dim strID As String = dgvList.SelectedRows(0).Cells(colAccountNumber.Index).Value
            RefNo = strID
            Dim selectSQL As String = " SELECT * FROM tblSavings_Account WHERE AccountNumber = '" & strID & "' AND VCECode = '" & strVCECode & "' "
            SQL.ReadQuery(selectSQL)
            If SQL.SQLDR.Read Then
                LoadSavingsBalance(strID)
                LoadSavingsLedger()
                EnableButton()
            End If
        End If
    End Sub

    Private Sub LoadLoanMaxPage(ByVal LoanID As String)
        Dim selectSQL As String = " SELECT CEILING(CAST(MAX(No) AS decimal(18,2))/" & decMaxLine & ") AS MaxPage FROM View_LoanLedger " & vbCrLf & _
                                  " WHERE Loan_No = '" & LoanID & "' "
        SQL.ReadQuery(selectSQL)
        While SQL.SQLDR.Read
            strMaxPage = SQL.SQLDR("MaxPage").ToString
        End While
    End Sub

    Private Sub LoadSavingsLedger()
        Dim strID As String = dgvList.SelectedRows(0).Cells(colAccountNumber.Index).Value
        Dim selectSQL = " SELECT * FROM viewSavings " & vbCrLf & _
                        " WHERE RefNo = '" & strID & "' AND VCECode = '" & strVCECode & "' " & vbCrLf & _
                        " AND No BETWEEN " & (decMaxLine * CDec(IIf(IsNumeric(txtPage.Text), txtPage.Text, 1))) - (decMaxLine - 1) & " AND " & (decMaxLine * CDec(IIf(IsNumeric(txtPage.Text), txtPage.Text, 1))) & " " & vbCrLf & _
                        " ORDER BY No "
        SQL.ReadQuery(selectSQL)
        dgvLedger.Rows.Clear()
        Dim count As Integer = 1
        While SQL.SQLDR.Read
            dgvLedger.Rows.Add(count, _
                               SQL.SQLDR("RefType").ToString, _
                               SQL.SQLDR("RefTransID").ToString, _
                               CDate(SQL.SQLDR("AppDate").ToString).ToShortDateString, _
                               CDec(SQL.SQLDR("Deposit").ToString).ToString("N2"), _
                               CDec(SQL.SQLDR("Withdrawal").ToString).ToString("N2"), _
                               CDec(SQL.SQLDR("Interest").ToString).ToString("N2"), _
                               CDec(SQL.SQLDR("Balance").ToString).ToString("N2"), _
                               SQL.SQLDR("TransID").ToString)
            txtLine.Text = "1"
            count += 1
        End While
    End Sub

    Private Sub LoadSavingsBalance(ByVal SavingsID As String)
        Dim selectSQL = " SELECT TOP 1 * FROM viewSavings WHERE RefNo = '" & SavingsID & "' AND VCECode = '" & txtVCECode.Text & "' ORDER BY No DESC "
        SQL.ReadQuery(selectSQL)
        If SQL.SQLDR.Read Then
            lblBalance.Text = CDec(SQL.SQLDR("Balance").ToString).ToString("N2")
        End If
    End Sub

    Private Sub dgvList_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList.CellClick
        LoadSavings()
    End Sub

    Private Sub dgvLedger_CellMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvLedger.CellMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If e.ColumnIndex > -1 And e.RowIndex > -1 Then
                dgvLedger.CurrentCell = dgvLedger.Rows(e.RowIndex).Cells(e.ColumnIndex)
                cmsMenu.Show(New Point(MousePosition.X, MousePosition.Y))
            End If
        End If
    End Sub

    Private Sub dgvLedger_SelectionChanged(sender As Object, e As System.EventArgs) Handles dgvLedger.SelectionChanged
        If dgvLedger.SelectedRows.Count > 0 Then
            txtLine.Text = dgvLedger.SelectedRows(0).Index + 1
        End If
    End Sub

    Private Sub EnableButton()
        If txtPage.Text = "1" Or strMaxPage = "" Then
            btnPrev.Enabled = True
            btnMin.Enabled = True
        Else
            btnPrev.Enabled = True
            btnMin.Enabled = True
        End If
        If txtPage.Text = strMaxPage Or strMaxPage = "" Then
            btnNext.Enabled = True
            btnMax.Enabled = True
        Else
            btnNext.Enabled = True
            btnMax.Enabled = True
        End If
    End Sub

    Private Sub btnNext_Click(sender As System.Object, e As System.EventArgs) Handles btnNext.Click
        txtPage.Text = CInt(txtPage.Text) + 1
        LoadSavingsLedger()
        EnableButton()
    End Sub

    Private Sub btnPrev_Click(sender As System.Object, e As System.EventArgs) Handles btnPrev.Click
        txtPage.Text = CInt(txtPage.Text) - 1
        LoadSavingsLedger()
        EnableButton()
    End Sub

    Private Sub btnMax_Click(sender As System.Object, e As System.EventArgs) Handles btnMax.Click
        txtPage.Text = CInt(strMaxPage)
        LoadSavingsLedger()
        EnableButton()
    End Sub

    Private Sub btnMin_Click(sender As System.Object, e As System.EventArgs) Handles btnMin.Click
        txtPage.Text = "1"
        LoadSavingsLedger()
        EnableButton()
    End Sub

    Private Sub cmbLoan_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbLoan.SelectedIndexChanged
        LoadSavingsList()
        txtPage.Text = "1"
        txtLine.Text = "0"
        dgvLedger.Rows.Clear()
        lblBalance.Text = "0.00"
    End Sub

    Private Sub btnPrintTransaction_Click(sender As System.Object, e As System.EventArgs) Handles btnPrintTransaction.Click
        If txtLine.Text <> "0" Then
            Dim f As New frmReport_Display
            f.ShowDialog("PB_Savings", txtVCECode.Text, txtLine.Text, txtPage.Text, RefNo)
            f.Dispose()
        End If
    End Sub

    Private Sub ViewToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ViewToolStripMenuItem.Click
        Dim strRefType As String = dgvLedger.SelectedRows(0).Cells(colRefType.Index).Value
        Dim strRefTransID As String = dgvLedger.SelectedRows(0).Cells(colTransID.Index).Value
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
        End Select
    End Sub

    Private Sub SOAToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SOAToolStripMenuItem.Click
        Dim f As New frmVerifier_DetailSOA
        f.Type = "Savings"
        f.txtVCECode.Text = txtVCECode.Text
        f.txtVCEName.Text = txtVCEName.Text
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub dgvList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList.CellContentClick

    End Sub

    Private Sub dgvLedger_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvLedger.CellContentClick

    End Sub
End Class