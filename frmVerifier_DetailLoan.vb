Public Class frmVerifier_DetailLoan

    Public strVCECode As String = ""
    Public strAccntCode As String = ""
    Dim strMaxPage As String = ""
    Dim decMaxLine As Decimal = 24
    Dim strNote As String = ""
    Private Sub frmVerifier_DetailLoan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadVCE()
        LoadLoanType()
        LoadLoanList()

    End Sub

    Private Sub LoadVCE()
        Dim selectSQL As String = " SELECT * FROM viewVCE_Master WHERE VCECode = '" & strVCECode & "' "
        SQL.ReadQuery(selectSQL)
        While SQL.SQLDR.Read
            txtVCECode.Text = strVCECode
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
        End While
    End Sub

    Private Sub LoadLoanList()
        Dim selectSQL As String = " SELECT Loan_No, LoanAmount, IntAmount, DateMaturity FROM tblLoan " & vbCrLf & _
                                  " INNER JOIN tblLoan_Type ON tblLoan.LoanCode = tblLoan_Type.LoanCode " & vbCrLf & _
                                  " WHERE tblLoan.Status = 'Released' AND tblLoan_Type.Status = 'Active' " & vbCrLf & _
                                  " AND LoanAccount = '" & LoadLoanAccount(cmbLoan.SelectedItem) & "' " & vbCrLf & _
                                  " AND VCECode = '" & strVCECode & "' " & vbCrLf & _
                                  " ORDER BY Loan_No, DateLoan "
        SQL.ReadQuery(selectSQL)
        dgvList.Rows.Clear()
        While SQL.SQLDR.Read
            dgvList.Rows.Add(SQL.SQLDR("Loan_No").ToString, CDec(SQL.SQLDR("LoanAmount").ToString).ToString("N2"), CDec(SQL.SQLDR("IntAmount").ToString).ToString("N2"), CDate(SQL.SQLDR("DateMaturity").ToString))
        End While
        RedHighlight()
    End Sub

    Private Sub RedHighlight()

        For i As Integer = 0 To dgvList.Rows.Count - 1
            If dgvList.Item(chMaturityDate.Index, i).Value <= CDate(Date.Today) Then
                dgvList.Rows(i).DefaultCellStyle.BackColor = Color.Red
            End If
        Next
    End Sub

    Private Sub LoadLoanType()
        Dim selectSQL As String = " SELECT DISTINCT LoanType, LoanAccount FROM tblLoan " & vbCrLf & _
                                  " INNER JOIN tblLoan_Type ON tblLoan.LoanCode = tblLoan_Type.LoanCode " & vbCrLf & _
                                  " WHERE tblLoan.Status = 'Released' AND tblLoan_Type.Status = 'Active' " & vbCrLf & _
                                  " AND VCECode = '" & strVCECode & "' " & vbCrLf & _
                                  " ORDER BY LoanType "
        SQL.ReadQuery(selectSQL)
        cmbLoan.Items.Clear()
        Dim boolCode As Boolean = False
        While SQL.SQLDR.Read
            cmbLoan.Items.Add(SQL.SQLDR("LoanType").ToString)
            If strAccntCode = SQL.SQLDR("LoanAccount").ToString Then
                boolCode = True
                cmbLoan.SelectedIndex = cmbLoan.Items.Count - 1
            End If
        End While
        If cmbLoan.Items.Count > 0 And boolCode = False Then
            cmbLoan.SelectedIndex = 0
        End If
    End Sub

    Private Function LoadLoanAccount(ByVal AccntCode As String) As String
        Dim selectSQL As String = " SELECT LoanAccount FROM tblLoan_Type " & vbCrLf & _
                                  " WHERE LoanType = '" & AccntCode & "' AND Status = 'Active' "
        Dim strAccountCode As String = ""
        SQL.ReadQuery(selectSQL)
        While SQL.SQLDR.Read
            strAccountCode = SQL.SQLDR("LoanAccount").ToString
        End While
        Return strAccountCode
    End Function

    Private Sub LoadLoan()
        If dgvList.SelectedRows.Count > 0 Then
            Dim strLoanID As String = dgvList.SelectedRows(0).Cells(colLoanID.Index).Value
            Dim selectSQL As String = " SELECT * FROM tblLoan WHERE Loan_No = '" & strLoanID & "' AND VCECode = '" & strVCECode & "' "
            SQL.ReadQuery(selectSQL)
            If SQL.SQLDR.Read Then
                txtLoanAmount.Text = CDec(SQL.SQLDR("LoanAmount").ToString).ToString("N2")
                txtInterestAmount.Text = CDec(SQL.SQLDR("IntAmount").ToString).ToString("N2")
                txtInterestRate.Text = CDec(SQL.SQLDR("IntValue").ToString * 100 * 12).ToString("N2") & "%"
                txtLoanDate.Text = CDate(SQL.SQLDR("DateLoan").ToString).ToShortDateString
                txtDateStart.Text = CDate(SQL.SQLDR("DateStart").ToString).ToShortDateString
                txtDateMaturity.Text = CDate(SQL.SQLDR("DateMaturity").ToString).ToShortDateString
                txtDateReleased.Text = CDate(SQL.SQLDR("DateRelease").ToString).ToShortDateString
                strNote = SQL.SQLDR("Notes").ToString
                LoadLoanBalance(strLoanID)
                LoadCoMaker(strLoanID)
                LoadLoanLedger()
                EnableButton()
            End If
        End If
    End Sub

    Public Sub LoadCoMaker(ByVal LoanID As Integer)
        Dim query As String
        query = " SELECT    TransID, viewVCE_Master.VCECode, VCEName " & _
                " FROM      tblLoan_CoMaker INNER JOIN viewVCE_Master " & _
                " ON        tblLoan_CoMaker.VCECode = viewVCE_Master.VCECode " & _
                " WHERE     TransID = '" & LoanID & "' "
        SQL.CloseCon()
        SQL.ReadQuery(query)
        lvCoMaker.Items.Clear()
        While SQL.SQLDR.Read
            lvCoMaker.Items.Add("")
            lvCoMaker.Items(lvCoMaker.Items.Count - 1).SubItems.Add(SQL.SQLDR("TransID").ToString)
            lvCoMaker.Items(lvCoMaker.Items.Count - 1).SubItems.Add(SQL.SQLDR("VCECode").ToString)
            lvCoMaker.Items(lvCoMaker.Items.Count - 1).SubItems.Add(SQL.SQLDR("VCEName").ToString)
        End While
    End Sub

    Private Sub LoadLoanMaxPage(ByVal LoanID As String)
        Dim selectSQL As String = " SELECT CEILING(CAST(MAX(No) AS decimal(18,2))/" & decMaxLine & ") AS MaxPage FROM View_LoanLedger " & vbCrLf & _
                                  " WHERE Loan_No = '" & LoanID & "' "
        SQL.ReadQuery(selectSQL)
        While SQL.SQLDR.Read
            strMaxPage = SQL.SQLDR("MaxPage").ToString
        End While
    End Sub
    Private Sub LoadLoanLedger()
        Dim strLoanID As String = dgvList.SelectedRows(0).Cells(colLoanID.Index).Value
        LoadLoanMaxPage(strLoanID)
        Dim selectSQL = " SELECT * FROM view_LoanLedger " & vbCrLf & _
                        " WHERE Loan_No = '" & strLoanID & "' " & vbCrLf & _
                        " AND No BETWEEN " & (decMaxLine * CDec(IIf(IsNumeric(txtPage.Text), txtPage.Text, 1))) - (decMaxLine - 1) & " AND " & (decMaxLine * CDec(IIf(IsNumeric(txtPage.Text), txtPage.Text, 1))) & " " & vbCrLf & _
                        " ORDER BY No "
        SQL.ReadQuery(selectSQL)
        dgvLedger.Rows.Clear()
        Dim count As Integer = 1
        While SQL.SQLDR.Read
            dgvLedger.Rows.Add(count, _
                               SQL.SQLDR("RefType").ToString, _
                               SQL.SQLDR("RefTransID").ToString, _
                               SQL.SQLDR("TransNo").ToString, _
                               CDate(SQL.SQLDR("AppDate").ToString).ToShortDateString, _
                               IIf(IsNumeric(SQL.SQLDR("LoanAmount").ToString), CDec(SQL.SQLDR("LoanAmount")).ToString("N2"), "0.00"), _
                               IIf(IsNumeric(SQL.SQLDR("LoanPayment").ToString), CDec(SQL.SQLDR("LoanPayment")).ToString("N2"), "0.00"), _
                               IIf(IsNumeric(SQL.SQLDR("LoanBalance").ToString), CDec(SQL.SQLDR("LoanBalance")).ToString("N2"), "0.00"), _
                               IIf(IsNumeric(SQL.SQLDR("IntPayment").ToString), CDec(SQL.SQLDR("IntPayment")).ToString("N2"), "0.00"), _
                               IIf(IsNumeric(SQL.SQLDR("CBU").ToString), CDec(SQL.SQLDR("CBU")).ToString("N2"), "0.00"), _
                               IIf(IsNumeric(SQL.SQLDR("Payment").ToString), CDec(SQL.SQLDR("Payment")).ToString("N2"), "0.00"), _
                               SQL.SQLDR("LoginName").ToString)
            txtLine.Text = "1"
            count += 1
        End While
        If txtLine.Text = strMaxPage Then
            selectSQL = " SELECT " & vbCrLf & _
                        "   SUM(LoanAmount) AS LoanAmount, " & vbCrLf & _
                        "   SUM(LoanPayment) AS LoanPayment, " & vbCrLf & _
                        "   SUM(LoanAmount - LoanPayment) AS LoanBalance, " & vbCrLf & _
                        "   SUM(IntPayment) AS IntPayment, " & vbCrLf & _
                        "   SUM(CBU) AS CBU, " & vbCrLf & _
                        "   SUM(Payment) AS Payment " & vbCrLf & _
                        " FROM view_LoanLedger " & vbCrLf & _
                        " WHERE Loan_No = '" & strLoanID & "' " & vbCrLf & _
                        " GROUP BY Loan_No "
            SQL.ReadQuery(selectSQL)
            While SQL.SQLDR.Read
                dgvLedger.Rows.Add("", _
                               "", _
                               "", _
                               "", _
                               "Total", _
                               IIf(IsNumeric(SQL.SQLDR("LoanAmount").ToString), CDec(SQL.SQLDR("LoanAmount")).ToString("N2"), "0.00"), _
                               IIf(IsNumeric(SQL.SQLDR("LoanPayment").ToString), CDec(SQL.SQLDR("LoanPayment")).ToString("N2"), "0.00"), _
                               IIf(IsNumeric(SQL.SQLDR("LoanBalance").ToString), CDec(SQL.SQLDR("LoanBalance")).ToString("N2"), "0.00"), _
                               IIf(IsNumeric(SQL.SQLDR("IntPayment").ToString), CDec(SQL.SQLDR("IntPayment")).ToString("N2"), "0.00"), _
                               IIf(IsNumeric(SQL.SQLDR("CBU").ToString), CDec(SQL.SQLDR("CBU")).ToString("N2"), "0.00"), _
                               IIf(IsNumeric(SQL.SQLDR("Payment").ToString), CDec(SQL.SQLDR("Payment")).ToString("N2"), "0.00"), _
                               "")
                dgvLedger.Rows(dgvLedger.Rows.Count - 1).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8.25, FontStyle.Bold)
            End While
        End If
    End Sub

    Private Sub LoadLoanBalance(ByVal LoanID As String)
        Dim selectSQL = " SELECT TOP 1 * FROM view_LoanLedger WHERE Loan_No = '" & LoanID & "' ORDER BY No DESC "
        SQL.ReadQuery(selectSQL)
        If SQL.SQLDR.Read Then
            lblBalance.Text = CDec(SQL.SQLDR("LoanBalance").ToString).ToString("N2")
        End If
    End Sub

    Private Sub dgvList_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList.CellClick
        LoadLoan()
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
        If dgvLedger.SelectedRows.Count > 0 AndAlso dgvLedger.SelectedRows(0).Cells(colNum.Index).Value.ToString <> "" Then
            txtLine.Text = dgvLedger.SelectedRows(0).Index + 1
        End If
    End Sub

    Private Sub EnableButton()
        If txtPage.Text = "1" Or strMaxPage = "" Then
            btnPrev.Enabled = False
            btnMin.Enabled = False
        Else
            btnPrev.Enabled = True
            btnMin.Enabled = True
        End If
        If txtPage.Text = strMaxPage Or strMaxPage = "" Then
            btnNext.Enabled = False
            btnMax.Enabled = False
        Else
            btnNext.Enabled = True
            btnMax.Enabled = True
        End If
    End Sub

    Private Sub btnNext_Click(sender As System.Object, e As System.EventArgs) Handles btnNext.Click
        txtPage.Text = CInt(txtPage.Text) + 1
        LoadLoanLedger()
        EnableButton()
    End Sub

    Private Sub btnPrev_Click(sender As System.Object, e As System.EventArgs) Handles btnPrev.Click
        txtPage.Text = CInt(txtPage.Text) - 1
        LoadLoanLedger()
        EnableButton()
    End Sub

    Private Sub btnMax_Click(sender As System.Object, e As System.EventArgs) Handles btnMax.Click
        txtPage.Text = CInt(strMaxPage)
        LoadLoanLedger()
        EnableButton()
    End Sub

    Private Sub btnMin_Click(sender As System.Object, e As System.EventArgs) Handles btnMin.Click
        txtPage.Text = "1"
        LoadLoanLedger()
        EnableButton()
    End Sub

    Private Sub cmbLoan_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbLoan.SelectedIndexChanged
        LoadLoanList()
        txtPage.Text = "1"
        txtLine.Text = "0"
        dgvLedger.Rows.Clear()
        txtLoanAmount.Clear()
        txtInterestAmount.Clear()
        txtLoanDate.Clear()
        txtDateStart.Clear()
        txtInterestRate.Clear()
        txtDateMaturity.Clear()
        txtDateReleased.Clear()
        lblBalance.Text = "0.00"
    End Sub

    Private Sub btnPrintTransaction_Click(sender As System.Object, e As System.EventArgs) Handles btnPrintTransaction.Click
        If txtLine.Text <> "0" Then
            Dim strReport As String = "PSBSAVINGS"
        End If
    End Sub

    Private Sub ViewToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ViewToolStripMenuItem.Click
        Dim strRefType As String = dgvLedger.SelectedRows(0).Cells(colRefType.Index).Value
        Dim strRefTransID As String = dgvLedger.SelectedRows(0).Cells(colRefTransID.Index).Value
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
            Case "PCV"
                Dim f As New frmPCV
                f.ShowDialog(strRefTransID)
        End Select
    End Sub

    Private Sub dgvList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList.CellContentClick

    End Sub

    Private Sub btnNotes_Click(sender As System.Object, e As System.EventArgs) Handles btnNotes.Click
        MsgBox(strNote, MsgBoxStyle.Information, "Notes")
    End Sub

    Private Sub SOAToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SOAToolStripMenuItem.Click
        Dim f As New frmVerifier_DetailSOA
        f.Type = "Loan"
        f.txtVCECode.Text = txtVCECode.Text
        f.txtVCEName.Text = txtVCEName.Text
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub PrintToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PrintToolStripMenuItem.Click

    End Sub

    Private Sub dgvLedger_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvLedger.CellContentClick

    End Sub
End Class