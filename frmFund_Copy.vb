Public Class frmFund_Copy
    Dim branch As String

    Private Sub btnGenerate_Click(sender As System.Object, e As System.EventArgs) Handles btnGenerate.Click
        If cbFund.SelectedIndex > -1 And IsNumeric(txtAmount.Text) And strType = "CV" Then
            Dim strCode As String = ""
            Dim strMonthsDelay As String = ""
            Dim decShareMinimum As Decimal = 0
            Dim selectSQL As String = " SELECT * FROM tblFund WHERE Description = '" & cbFund.SelectedItem & "' "

            branch = Strings.Left(cbbranch.SelectedItem, cbbranch.SelectedItem.ToString.IndexOf(" - "))
            SQL.ReadQuery(selectSQL)
            While SQL.SQLDR.Read
                strCode = SQL.SQLDR("Code").ToString
                strMonthsDelay = SQL.SQLDR("MonthsDelay").ToString
                decShareMinimum = CDec(SQL.SQLDR("ShareCapital").ToString)
            End While
            If strCode <> "" Then
                Dim decAmount As Decimal = txtAmount.Text
                Dim decMemAmount As Decimal = 0
                frmCV.dgvEntry.Rows.Clear()
                selectSQL = " SELECT AccntCode, AccountTitle, Minimum, PercentRate, Member, Debit FROM tblFund_Details " & vbCrLf & _
                            " INNER JOIN tblCOA_Master ON tblFund_Details.AccntCode = tblCOA_Master.AccountCode " & vbCrLf & _
                            " WHERE Code = '" & strCode & "' "
                SQL.ReadQuery(selectSQL)
                While SQL.SQLDR.Read
                    Dim strAccntCode As String = SQL.SQLDR("AccntCode").ToString
                    Dim strAccntTitle As String = SQL.SQLDR("AccountTitle").ToString
                    Dim decMinimum As Decimal = CDec(SQL.SQLDR("Minimum").ToString)
                    Dim decPercent As Decimal = CDec(SQL.SQLDR("PercentRate").ToString)
                    Dim boolMember As Boolean = SQL.SQLDR("Member").ToString
                    Dim boolDebit As Boolean = SQL.SQLDR("Debit").ToString
                    If boolMember Then
                        Dim decCount As Decimal = 0
                        'selectSQL = " SELECT COUNT(VCECode) AS Count FROM " & vbCrLf & _
                        '            " (SELECT VCECode, VCEName FROM View_Ledger " & vbCrLf & _
                        '            " WHERE AppDate <= DATEADD(DAY, -1, DATEADD(MONTH, -" & strMonthsDelay & ", '" & dtpDate.Value.Month & "-01-" & dtpDate.Value.Year & "')) AND AccntCode = '" & strAccntCode & "' " & vbCrLf & _
                        '            " GROUP BY VCECode, VCEName " & vbCrLf & _
                        '            " HAVING SUM(Credit - Debit) >= " & decMinimum & ") AS Counter "
                        'Dim strFilterDate As String = "WHERE AppDate <= DATEADD(DAY, -1, DATEADD(MONTH, -" & strMonthsDelay & ", '" & dtpDate.Value.Month & "-" & dtpDate.Value.Day & "-" & dtpDate.Value.Year & "'))"
                        Dim strFilterDate As String = "WHERE AppDate <=  '" & dtpDate.Value.Month & "-" & dtpDate.Value.Day & "-" & dtpDate.Value.Year & "'"
                        If strMonthsDelay = "0" Then
                            strFilterDate = "WHERE MONTH(AppDate) = MONTH('" & dtpDate.Value & "') AND YEAR(AppDate) = YEAR('" & dtpDate.Value & "')"
                        End If
                        Dim strFilterShare As String = ""
                        strFilterShare = "       SELECT VCECode FROM view_FundMinimum " & vbCrLf & _
                                         "       WHERE AppDate <= DATEADD(DAY, -1, DATEADD(MONTH, -" & strMonthsDelay & ", '" & dtpDate.Value.Month & "-" & dtpDate.Value.Day & "-" & dtpDate.Value.Year & "')) " & vbCrLf & _
                                         "       GROUP BY VCECode " & vbCrLf & _
                                         "       HAVING SUM(Credit - Debit) >= " & decShareMinimum & " "
                        If decShareMinimum = 0 Then
                            strFilterShare = "  SELECT VCECode FROM ViewVCE_Master "
                        End If
                        selectSQL = " SELECT COUNT(VCECode) AS Count FROM " & vbCrLf & _
                                    " ( " & vbCrLf & _
                                    " 	SELECT VCECode, VCEName, SUM(Credit - Debit) AS Savings,'' as  RefNo, BranchCode FROM View_GL " & vbCrLf & _
                                    " 	" & strFilterDate & " " & vbCrLf & _
                                    "   AND AccntCode = '" & strAccntCode & "' " & vbCrLf & _
                                    " 	AND VCECode IN " & vbCrLf & _
                                    "   ( " & vbCrLf & _
                                    "       " & strFilterShare & " " & vbCrLf & _
                                    "   ) " & IIf(branch = "ALL", "AND BranchCode in ( SELECT    DISTINCT  tblBranch.BranchCode   " & _
                                    " FROM      tblBranch    " & _
                                    " INNER JOIN tblUser_Access    ON   " & _
                                    " tblBranch.BranchCode = tblUser_Access.Code    " & _
                                    " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                                    " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                                    " WHERE     UserID ='" & UserID & "'  )", " AND BranchCode = '" & branch & "' ") & _
                                    "   GROUP BY VCECode, VCEName, BranchCode " & vbCrLf & _
                                    "   HAVING SUM(Credit - Debit) >= " & decMinimum & " " & vbCrLf & _
                                    " ) AS Counter "
                        SQL.ReadQuery(selectSQL, 2)
                        While SQL.SQLDR2.Read
                            decCount = CDec(SQL.SQLDR2("Count").ToString)
                        End While
                        If decCount > 0 Then
                            'selectSQL = " SELECT VCECode, VCEName, SUM(Credit - Debit) AS Balance, RefNo FROM View_Ledger " & vbCrLf & _
                            '            " WHERE AppDate <= DATEADD(MONTH, -" & strMonthsDelay & ", '" & dtpDate.Value & "') AND AccntCode = '" & strAccntCode & "' " & vbCrLf & _
                            '            " GROUP BY VCECode, VCEName, RefNo " & vbCrLf & _
                            '            " HAVING SUM(Credit - Debit) >= " & decMinimum & " " & vbCrLf & _
                            '            " ORDER BY VCECode "
                            selectSQL = " 	SELECT VCECode, VCEName, SUM(Credit - Debit) AS Savings, '' as RefNo, BranchCode FROM View_GL " & vbCrLf & _
                                        " 	" & strFilterDate & " " & vbCrLf & _
                                        "   AND AccntCode = '" & strAccntCode & "' " & vbCrLf & _
                                        " 	 AND VCECode IN " & vbCrLf & _
                                        "   ( " & vbCrLf & _
                                        "       " & strFilterShare & " " & vbCrLf & _
                                        "   ) " & IIf(branch = "ALL", "AND BranchCode in ( SELECT    DISTINCT  tblBranch.BranchCode   " & _
                                        " FROM      tblBranch    " & _
                                        " INNER JOIN tblUser_Access    ON   " & _
                                        " tblBranch.BranchCode = tblUser_Access.Code    " & _
                                        " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                                        " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                                        " WHERE     UserID ='" & UserID & "'  )", " AND BranchCode = '" & branch & "' ") & vbCrLf & _
                                        "   GROUP BY VCECode, VCEName, BranchCode " & vbCrLf & _
                                        "   HAVING SUM(Credit - Debit) >= " & decMinimum & " "
                            SQL.ReadQuery(selectSQL, 2)
                            If frmCV.dgvEntry.ColumnCount >= 11 Then
                            Else
                                frmCV.dgvEntry.Columns.Add("Balance", "Balance")
                            End If
                            While SQL.SQLDR2.Read
                                If boolDebit Then
                                    'frmCV.dgvEntry.Rows.Add("", strAccntCode, strAccntTitle, CDec(CDec((decAmount * (decPercent / 100)) / decCount).ToString("N0")).ToString("N2"), CDec(0).ToString("N2"), SQL.SQLDR2("VCECode").ToString, SQL.SQLDR2("VCEName").ToString, "", SQL.SQLDR2("RefNo").ToString, "", CDec(SQL.SQLDR2("Savings")).ToString("N2"))
                                    frmCV.dgvEntry.Rows.Add(branch, strAccntCode, strAccntTitle, CDec(decAmount).ToString("N2"), CDec(0).ToString("N2"), SQL.SQLDR2("VCECode").ToString, SQL.SQLDR2("VCEName").ToString, "", SQL.SQLDR2("RefNo").ToString, "", CDec(SQL.SQLDR2("Savings")).ToString("N2"))
                                Else
                                    ' frmCV.dgvEntry.Rows.Add("", strAccntCode, strAccntTitle, CDec(0).ToString("N2"), CDec(CDec((decAmount * (decPercent / 100)) / decCount).ToString("N0")).ToString("N2"), SQL.SQLDR2("VCECode").ToString, SQL.SQLDR2("VCEName").ToString, "", SQL.SQLDR2("RefNo").ToString, "", CDec(SQL.SQLDR2("Savings")).ToString("N2"))
                                    frmCV.dgvEntry.Rows.Add(branch, strAccntCode, strAccntTitle, CDec(0).ToString("N2"), CDec(decAmount).ToString("N2"), SQL.SQLDR2("VCECode").ToString, SQL.SQLDR2("VCEName").ToString, "", SQL.SQLDR2("RefNo").ToString, "", CDec(SQL.SQLDR2("Savings")).ToString("N2"))
                                End If
                                decMemAmount += CDec(CDec((decAmount * (decPercent / 100)) / decCount).ToString("N0"))
                            End While
                        Else
                            If boolDebit Then
                                ' frmCV.dgvEntry.Rows.Add("", strAccntCode, strAccntTitle, CDec(decMemAmount - (decAmount * (decPercent / 100))).ToString("N2"), CDec(0).ToString("N2"))
                                frmCV.dgvEntry.Rows.Add(branch, strAccntCode, strAccntTitle, CDec(decAmount).ToString("N2"), CDec(0).ToString("N2"))
                            Else
                                'frmCV.dgvEntry.Rows.Add("", strAccntCode, strAccntTitle, CDec(0).ToString("N2"), CDec(decAmount * (decPercent / 100)).ToString("N2"))
                                frmCV.dgvEntry.Rows.Add(branch, strAccntCode, strAccntTitle, CDec(0).ToString("N2"), CDec(decAmount).ToString("N2"))
                            End If
                        End If
                    Else
                        If boolDebit Then
                            ' frmCV.dgvEntry.Rows.Add("", strAccntCode, strAccntTitle, CDec((decAmount - ((decAmount * (decPercent / 100)) + decMemAmount)) + (decAmount * (decPercent / 100))).ToString("N2"), CDec(0).ToString("N2"))
                            frmCV.dgvEntry.Rows.Add(branch, strAccntCode, strAccntTitle, CDec(decAmount).ToString("N2"), CDec(0).ToString("N2"))
                        Else
                            'frmCV.dgvEntry.Rows.Add("", strAccntCode, strAccntTitle, CDec(0).ToString("N2"), CDec(decAmount * (decPercent / 100)).ToString("N2"))
                            frmCV.dgvEntry.Rows.Add(branch, strAccntCode, strAccntTitle, CDec(0).ToString("N2"), CDec(decAmount).ToString("N2"))
                        End If
                        decMemAmount = 0
                    End If
                End While
                frmCV.TotalDBCR()
            End If
        ElseIf cbFund.SelectedIndex > -1 And IsNumeric(txtAmount.Text) And strType = "JV" Then
            Dim strCode As String = ""
            Dim strMonthsDelay As String = ""
            Dim decShareMinimum As Decimal = 0
            Dim selectSQL As String = " SELECT * FROM tblFund WHERE Description = '" & cbFund.SelectedItem & "' "
            branch = Strings.Left(cbbranch.SelectedItem, cbbranch.SelectedItem.ToString.IndexOf(" - "))
            SQL.ReadQuery(selectSQL)
            While SQL.SQLDR.Read
                strCode = SQL.SQLDR("Code").ToString
                strMonthsDelay = SQL.SQLDR("MonthsDelay").ToString
                decShareMinimum = CDec(SQL.SQLDR("ShareCapital").ToString)
            End While
            If strCode <> "" Then
                Dim decAmount As Decimal = txtAmount.Text
                Dim decMemAmount As Decimal = 0
                frmJV.dgvEntry.Rows.Clear()
                selectSQL = " SELECT AccntCode, AccountTitle, Minimum, PercentRate, Member, Debit FROM tblFund_Details " & vbCrLf & _
                            " INNER JOIN tblCOA_Master ON tblFund_Details.AccntCode = tblCOA_Master.AccountCode " & vbCrLf & _
                            " WHERE Code = '" & strCode & "' "
                SQL.ReadQuery(selectSQL)
                While SQL.SQLDR.Read
                    Dim strAccntCode As String = SQL.SQLDR("AccntCode").ToString
                    Dim strAccntTitle As String = SQL.SQLDR("AccountTitle").ToString
                    Dim decMinimum As Decimal = CDec(SQL.SQLDR("Minimum").ToString)
                    Dim decPercent As Decimal = CDec(SQL.SQLDR("PercentRate").ToString)
                    Dim boolMember As Boolean = SQL.SQLDR("Member").ToString
                    Dim boolDebit As Boolean = SQL.SQLDR("Debit").ToString
                    If boolMember Then
                        Dim decCount As Decimal = 0
                        'selectSQL = " SELECT COUNT(VCECode) AS Count FROM " & vbCrLf & _
                        '            " (SELECT VCECode, VCEName FROM View_Ledger " & vbCrLf & _
                        '            " WHERE AppDate <= DATEADD(DAY, -1, DATEADD(MONTH, -" & strMonthsDelay & ", '" & dtpDate.Value.Month & "-01-" & dtpDate.Value.Year & "')) AND AccntCode = '" & strAccntCode & "' " & vbCrLf & _
                        '            " GROUP BY VCECode, VCEName " & vbCrLf & _
                        '            " HAVING SUM(Credit - Debit) >= " & decMinimum & ") AS Counter "
                        'Dim strFilterDate As String = "WHERE AppDate <= DATEADD(DAY, -1, DATEADD(MONTH, -" & strMonthsDelay & ", '" & dtpDate.Value.Month & "-" & dtpDate.Value.Day & "-" & dtpDate.Value.Year & "'))"
                        Dim strFilterDate As String = "WHERE AppDate <=  '" & dtpDate.Value.Month & "-" & dtpDate.Value.Day & "-" & dtpDate.Value.Year & "'"
                        If strMonthsDelay = "0" Then
                            strFilterDate = "WHERE MONTH(AppDate) = MONTH('" & dtpDate.Value & "') AND YEAR(AppDate) = YEAR('" & dtpDate.Value & "')"
                        End If
                        Dim strFilterShare As String = ""
                        strFilterShare = "       SELECT VCECode FROM view_FundMinimum " & vbCrLf & _
                                         "       WHERE AppDate <= DATEADD(DAY, -1, DATEADD(MONTH, -" & strMonthsDelay & ", '" & dtpDate.Value.Month & "-" & dtpDate.Value.Day & "-" & dtpDate.Value.Year & "')) " & vbCrLf & _
                                         "       GROUP BY VCECode " & vbCrLf & _
                                         "       HAVING SUM(Credit - Debit) >= " & decShareMinimum & " "
                        If decShareMinimum = 0 Then
                            strFilterShare = "  SELECT VCECode FROM ViewVCE_Master "
                        End If
                        selectSQL = " SELECT COUNT(VCECode) AS Count FROM " & vbCrLf & _
                                    " ( " & vbCrLf & _
                                    " 	SELECT VCECode, VCEName, SUM(Credit - Debit) AS Savings, '' as  RefNo, BranchCode FROM View_GL " & vbCrLf & _
                                    " 	" & strFilterDate & " " & vbCrLf & _
                                    "   AND AccntCode = '" & strAccntCode & "' " & vbCrLf & _
                                    " 	AND VCECode IN " & vbCrLf & _
                                    "   ( " & vbCrLf & _
                                    "       " & strFilterShare & " " & vbCrLf & _
                                    "   ) " & IIf(branch = "ALL", "AND BranchCode in ( SELECT    DISTINCT  tblBranch.BranchCode   " & _
                                    " FROM      tblBranch    " & _
                                    " INNER JOIN tblUser_Access    ON   " & _
                                    " tblBranch.BranchCode = tblUser_Access.Code    " & _
                                    " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                                    " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                                    " WHERE     UserID ='" & UserID & "'  )", " AND BranchCode = '" & branch & "' ") & _
                                    "   GROUP BY VCECode, VCEName,  BranchCode " & vbCrLf & _
                                    "   HAVING SUM(Credit - Debit) >= " & decMinimum & " " & vbCrLf & _
                                    " ) AS Counter "
                        SQL.ReadQuery(selectSQL, 2)
                        While SQL.SQLDR2.Read
                            decCount = CDec(SQL.SQLDR2("Count").ToString)
                        End While
                        If decCount > 0 Then
                            'selectSQL = " SELECT VCECode, VCEName, SUM(Credit - Debit) AS Balance, RefNo FROM View_Ledger " & vbCrLf & _
                            '            " WHERE AppDate <= DATEADD(MONTH, -" & strMonthsDelay & ", '" & dtpDate.Value & "') AND AccntCode = '" & strAccntCode & "' " & vbCrLf & _
                            '            " GROUP BY VCECode, VCEName, RefNo " & vbCrLf & _
                            '            " HAVING SUM(Credit - Debit) >= " & decMinimum & " " & vbCrLf & _
                            '            " ORDER BY VCECode "
                            selectSQL = " 	SELECT VCECode, VCEName, SUM(Credit - Debit) AS Savings,'' as  RefNo, BranchCode FROM View_GL " & vbCrLf & _
                                        " 	" & strFilterDate & " " & vbCrLf & _
                                        "   AND AccntCode = '" & strAccntCode & "' " & vbCrLf & _
                                        " 	 AND VCECode IN " & vbCrLf & _
                                        "   ( " & vbCrLf & _
                                        "       " & strFilterShare & " " & vbCrLf & _
                                        "   ) " & IIf(branch = "ALL", "AND BranchCode in ( SELECT    DISTINCT  tblBranch.BranchCode   " & _
                                        " FROM      tblBranch    " & _
                                        " INNER JOIN tblUser_Access    ON   " & _
                                        " tblBranch.BranchCode = tblUser_Access.Code    " & _
                                        " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                                        " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                                        " WHERE     UserID ='" & UserID & "'  )", " AND BranchCode = '" & branch & "' ") & vbCrLf & _
                                        "   GROUP BY VCECode, VCEName, BranchCode " & vbCrLf & _
                                        "   HAVING SUM(Credit - Debit) >= " & decMinimum & " "
                            SQL.ReadQuery(selectSQL, 2)
                            If frmJV.dgvEntry.ColumnCount >= 14 Then
                            Else
                                frmJV.dgvEntry.Columns.Add("Balance", "Balance")
                            End If
                            While SQL.SQLDR2.Read
                                If boolDebit Then
                                    ' frmJV.dgvEntry.Rows.Add(strAccntCode, strAccntTitle, CDec((decAmount * (decPercent / 100)) / decCount).ToString("N0"), CDec(0).ToString("N2"), "", SQL.SQLDR2("VCECode").ToString, SQL.SQLDR2("VCEName").ToString, SQL.SQLDR2("RefNo").ToString, "", "", "", "", SQL.SQLDR2("Savings").ToString)
                                    frmJV.dgvEntry.Rows.Add(strAccntCode, strAccntTitle, CDec(decAmount).ToString("N2"), CDec(0).ToString("N2"), "", SQL.SQLDR2("VCECode").ToString, SQL.SQLDR2("VCEName").ToString, SQL.SQLDR2("RefNo").ToString, "", "", "", "", SQL.SQLDR2("BranchCode").ToString, SQL.SQLDR2("Savings").ToString)
                                Else
                                    ' frmJV.dgvEntry.Rows.Add(strAccntCode, strAccntTitle, CDec(0).ToString("N2"), CDec((decAmount * (decPercent / 100)) / decCount).ToString("N0"), "", SQL.SQLDR2("VCECode").ToString, SQL.SQLDR2("VCEName").ToString, SQL.SQLDR2("RefNo").ToString, "", "", "", "", SQL.SQLDR2("Savings").ToString)
                                    frmJV.dgvEntry.Rows.Add(strAccntCode, strAccntTitle, CDec(0).ToString("N2"), CDec(decAmount).ToString("N2"), "", SQL.SQLDR2("VCECode").ToString, SQL.SQLDR2("VCEName").ToString, SQL.SQLDR2("RefNo").ToString, "", "", "", "", SQL.SQLDR2("BranchCode").ToString, SQL.SQLDR2("Savings").ToString)
                                End If
                                decMemAmount += CDec(CDec((decAmount * (decPercent / 100)) / decCount).ToString("N0"))
                            End While
                        Else
                            If boolDebit Then
                                ' frmJV.dgvEntry.Rows.Add(strAccntCode, strAccntTitle, CDec((decAmount * (decPercent / 100)) / decCount).ToString("N0"), CDec(0).ToString("N2"), "", SQL.SQLDR2("VCECode").ToString, SQL.SQLDR2("VCEName").ToString, SQL.SQLDR2("RefNo").ToString)
                                frmJV.dgvEntry.Rows.Add(strAccntCode, strAccntTitle, CDec(decAmount).ToString("N2"), CDec(0).ToString("N2"), "", SQL.SQLDR2("VCECode").ToString, SQL.SQLDR2("VCEName").ToString, SQL.SQLDR2("RefNo").ToString, "", "", "", "", SQL.SQLDR2("BranchCode").ToString)
                            Else
                                ' frmJV.dgvEntry.Rows.Add(strAccntCode, strAccntTitle, CDec(0).ToString("N2"), CDec((decAmount * (decPercent / 100)) / decCount).ToString("N0"), "", SQL.SQLDR2("VCECode").ToString, SQL.SQLDR2("VCEName").ToString, SQL.SQLDR2("RefNo").ToString)
                                frmJV.dgvEntry.Rows.Add(strAccntCode, strAccntTitle, CDec(0).ToString("N2"), CDec(decAmount).ToString("N2"), "", SQL.SQLDR2("VCECode").ToString, SQL.SQLDR2("VCEName").ToString, SQL.SQLDR2("RefNo").ToString, "", "", "", "", SQL.SQLDR2("BranchCode").ToString)
                            End If
                        End If
                    Else
                        If boolDebit Then
                            'frmJV.dgvEntry.Rows.Add(strAccntCode, strAccntTitle, CDec((decAmount - ((decAmount * (decPercent / 100)) + decMemAmount)) + (decAmount * (decPercent / 100))).ToString("N2"), CDec(0).ToString("N2"))
                            frmJV.dgvEntry.Rows.Add(strAccntCode, strAccntTitle, CDec(decAmount).ToString("N2"), CDec(0).ToString("N2"))
                        Else
                            'frmJV.dgvEntry.Rows.Add(strAccntCode, strAccntTitle, CDec(0).ToString("N2"), CDec(decAmount * (decPercent / 100)).ToString("N2"))
                            frmJV.dgvEntry.Rows.Add(strAccntCode, strAccntTitle, CDec(0).ToString("N2"), CDec(decAmount).ToString("N2"))
                        End If
                        decMemAmount = 0
                    End If
                End While
                frmJV.TotalDBCR()
            End If
        ElseIf cbFund.SelectedIndex > -1 And IsNumeric(txtAmount.Text) And strType = "Savings" Then
            Dim selectSQL As String
            Dim decAmount As Decimal = txtAmount.Text
            Dim decMemAmount As Decimal = 0
            branch = Strings.Left(cbbranch.SelectedItem, cbbranch.SelectedItem.ToString.IndexOf(" - "))
            frmJV.dgvEntry.Rows.Clear()
            selectSQL = " SELECT  AccntCode, DebitTitle, RefAccount, CreditTitle FROM tblDefaultAccount " & vbCrLf & _
                        " INNER JOIN (SELECT AccountCode, AccountTitle AS DebitTitle FROM tblCOA_Master) AS DebitAccnt  ON tblDefaultAccount.AccntCode = DebitAccnt.AccountCode " & vbCrLf & _
                        " INNER JOIN (SELECT AccountCode, AccountTitle AS CreditTitle FROM tblCOA_Master) AS CreditAccnt  ON tblDefaultAccount.RefAccount = CreditAccnt.AccountCode " & vbCrLf & _
                        " WHERE ModuleID = 'FUND' ANd Type = '" & strType & "' "
            SQL.ReadQuery(selectSQL)
            While SQL.SQLDR.Read
                Dim debitAccntCode As String = SQL.SQLDR("AccntCode").ToString
                Dim debitAccntTitle As String = SQL.SQLDR("DebitTitle").ToString
                Dim creditAccntCode As String = SQL.SQLDR("RefAccount").ToString
                Dim creditAccntTitle As String = SQL.SQLDR("CreditTitle").ToString


                selectSQL = " SELECT tblMember_Sponsor.Member_ID, tblMember_Master.Full_Name, SUM(Credit-Debit) as Savings, tblMember_Sponsor.VCECode, viewVCE_Master.VCEName, View_GL.BranchCode " & vbCrLf & _
                            " FROM tblMember_Sponsor " & vbCrLf & _
                            "	INNER JOIN tblMember_Master ON " & vbCrLf & _
                            "		tblMember_Master.Member_ID = tblMember_Sponsor.Member_ID  " & vbCrLf & _
                            "	INNER JOIN viewVCE_Master ON   " & vbCrLf & _
                            "		viewVCE_Master.VCECode = tblMember_Sponsor.VCECode " & vbCrLf & _
                            " LEFT JOIN View_GL ON" & vbCrLf & _
                            " View_GL.VCECode = tblMember_Sponsor.Member_ID AND AccntCode = '" & debitAccntCode & "'" & vbCrLf & _
                            " " & IIf(branch = "ALL", "WHERE View_GL.BranchCode in ( SELECT    DISTINCT  tblBranch.BranchCode   " & vbCrLf & _
                                        " FROM      tblBranch    " & vbCrLf & _
                                        " INNER JOIN tblUser_Access    ON   " & vbCrLf & _
                                        " tblBranch.BranchCode = tblUser_Access.Code    " & vbCrLf & _
                                        " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & vbCrLf & _
                                        " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & vbCrLf & _
                                        " WHERE     UserID ='" & UserID & "'  )", " WHERE View_GL.BranchCode = '" & branch & "' ") & " AND AppDate <= '" & dtpDate.Value & "'" & vbCrLf & _
                            " GROUP BY tblMember_Sponsor.Member_ID, tblMember_Master.Full_Name, tblMember_Sponsor.VCECode, viewVCE_Master.VCEName, View_GL.BranchCode " & vbCrLf & _
                            " HAVING(SUM(Credit - Debit) > 200) "

                SQL.ReadQuery(selectSQL, 2)
                If frmJV.dgvEntry.ColumnCount >= 14 Then
                Else
                    frmJV.dgvEntry.Columns.Add("Balance", "Balance")
                End If
                While SQL.SQLDR2.Read
                    Dim MemberID As String = SQL.SQLDR2("Member_ID").ToString
                    Dim MemberName As String = SQL.SQLDR2("Full_Name").ToString
                    Dim VCECode As String = SQL.SQLDR2("VCECode").ToString
                    Dim VCEName As String = SQL.SQLDR2("VCEName").ToString
                    Dim BranchCode As String = SQL.SQLDR2("BranchCode").ToString
                    Dim Balance As Double = SQL.SQLDR2("Savings").ToString
                    Dim Refno As String = ""

                    frmJV.dgvEntry.Rows.Add(debitAccntCode, debitAccntTitle, CDec(decAmount).ToString("N2"), CDec(0).ToString("N2"), "", MemberID, MemberName, Refno, "", "", "", "", BranchCode, SQL.SQLDR2("Savings").ToString)
                    frmJV.dgvEntry.Rows.Add(creditAccntCode, creditAccntTitle, CDec(0).ToString("N2"), CDec(decAmount).ToString("N2"), "", VCECode, VCEName, "", "", "", "", "", BranchCode)
                End While
                frmJV.TotalDBCR()
            End While
        Else
                MsgBox("Invalid Input", MsgBoxStyle.Information, "Error")
        End If
    End Sub

    Public strType As String = ""
    Private Sub frmFundCopy_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadFund()
        LoadBranches()
    End Sub

    Private Sub LoadBranches()
        Dim query As String
        query = " SELECT    DISTINCT  tblBranch.BranchCode + ' - ' + Description AS BranchCode  " & _
                " FROM      tblBranch    " & _
                " INNER JOIN tblUser_Access    ON   " & _
                " tblBranch.BranchCode = tblUser_Access.Code    " & _
                " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                " WHERE     UserID ='" & UserID & "'  "
        SQL.ReadQuery(query)
        cbbranch.Items.Clear()
        cbbranch.Items.Add("ALL - ALL BRANCHES")
        While SQL.SQLDR.Read
            cbbranch.Items.Add(SQL.SQLDR("BranchCode").ToString)
        End While
        cbbranch.SelectedItem = "ALL - ALL BRANCHES"
    End Sub

    Private Sub LoadFund()
        Dim selectSQL As String = " SELECT * FROM tblFund ORDER BY Code "
        SQL.ReadQuery(selectSQL)
        cbFund.Items.Clear()
        While SQL.SQLDR.Read
            cbFund.Items.Add(SQL.SQLDR("Description").ToString)
        End While
    End Sub

 
End Class