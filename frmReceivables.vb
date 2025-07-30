Public Class frmReceivables
    Dim SQL As New SQLControl
    Public VCECode As String = Nothing
    Public CollectDate As Date
    Dim module_type As String = "Disburse"

    Private Sub frmReceivables_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadAR()
        lvARList.Focus()
    End Sub

    Private Sub LoadAR()
        Dim query As String
        query = "   SELECT  tblLoan.TransID, tblLoan.Loan_No, VCECode, tblLoan_Type.LoanType, CAST(DateMaturity AS DATE) AS Maturity_Date,   " & vbCrLf & _
                "   		  CASE WHEN ISNULL(Scheduled.Principal_Due - ISNULL(Actual.Principal,0),0) <= 0   " & vbCrLf & _
                "  			    THEN 0   " & vbCrLf & _
                "  				ELSE ISNULL(Scheduled.Principal_Due - ISNULL(Actual.Principal,0),0)  " & vbCrLf & _
                "  		  END AS Principal_Balance,   " & vbCrLf & _
                "   		   CASE WHEN tblLoan.IntAmortMethod <> 'Less to Proceeds' THEN  " & vbCrLf & _
                "              (CASE WHEN ISNULL(Scheduled.Interest_Due - ISNULL(Actual.Interest,0),0) <= 0   " & vbCrLf & _
                "  			        THEN 0  " & vbCrLf & _
                "  			        ELSE ISNULL(Scheduled.Interest_Due - ISNULL(Actual.Interest,0),0)  " & vbCrLf & _
                "  		      END)  " & vbCrLf & _
                "               ELSE 0 END AS Interest_Balance,    " & vbCrLf & _
                "           CASE WHEN ISNULL(Scheduled.CBU - ISNULL(Actual.CBU,0),0) <= 0    " & vbCrLf & _
                "   			        THEN 0   " & vbCrLf & _
                "   			        ELSE ISNULL(Scheduled.CBU - ISNULL(Actual.CBU,0),0)   " & vbCrLf & _
                "   		      END AS CBU_Balance,  " & vbCrLf & _
                " 			   			    CASE WHEN ISNULL(Scheduled.Savings - ISNULL(Actual.Savings,0),0) <= 0    " & vbCrLf & _
                "   			        THEN 0   " & vbCrLf & _
                "   			        ELSE ISNULL(Scheduled.Savings - ISNULL(Actual.Savings,0),0)   " & vbCrLf & _
                "   		      END AS Savings_Balance,   " & vbCrLf & _
                " 			    CASE WHEN ISNULL(Scheduled.Kasimpatiya - ISNULL(Actual.Kasimpatiya,0),0) <= 0    " & vbCrLf & _
                "   			        THEN 0   " & _
                "   			        ELSE ISNULL(Scheduled.Kasimpatiya - ISNULL(Actual.Kasimpatiya,0),0)   " & vbCrLf & _
                "   		      END AS Kasimpatiya_Balance, " & vbCrLf & _
                "  		  tblLoan.Terms, IntAmount,   " & vbCrLf & _
                "   		  CASE WHEN '" & CollectDate & "' > DateMaturity AND DATEDIFF(Day,DateMaturity,'" & CollectDate & "') > 3   " & vbCrLf & _
                "   			   THEN (Scheduled.Principal_Due - ISNULL(Actual.Principal,0)) * 0.03 * DATEDIFF(DAY,DateMaturity,'" & CollectDate & "')/30   " & vbCrLf & _
                "  			   ELSE 0   " & vbCrLf & _
                "  		  END AS Penalty  " & vbCrLf & _
                "   FROM tblLoan INNER JOIN tblLoan_Type   " & vbCrLf & _
                "   ON tblLoan.LoanCode = tblLoan_Type.LoanCode   " & vbCrLf & _
                "   LEFT JOIN   " & vbCrLf & _
                "   (   " & vbCrLf & _
                "  	SELECT TransID,  MAX(Principal) AS Principal_Advance, MAX(Interest) AS Interest_Advance   , MAX(Other1) AS CBU, " & vbCrLf & _
                " MAX(Other2) AS Savings, MAX(Other3) AS Kasimpatiya " & vbCrLf & _
                "  	FROM tblLoan_Schedule " & vbCrLf & _
                "  	 GROUP BY TransID  " & vbCrLf & _
                "   ) AS Advance   " & vbCrLf & _
                "   ON tblLoan.TransID = Advance.TransID   " & vbCrLf & _
                "   LEFT JOIN  " & vbCrLf & _
                "   (   " & vbCrLf & _
                "  	SELECT TransID, SUM(Principal) AS Principal_Balance, SUM(Interest) AS Interest_Balance , SUM(Other1) AS CBU, " & vbCrLf & _
                "   SUM(Other2) AS Savings, SUM(Other3) AS Kasimpatiya" & vbCrLf & _
                " FROM tblLoan_Schedule   " & vbCrLf & _
                "  	GROUP BY TransID   " & vbCrLf & _
                "   ) AS Balance   " & vbCrLf & _
                "   ON tblLoan.TransID = Balance.TransID  " & vbCrLf & _
                "   LEFT JOIN    " & vbCrLf & _
                "   (   " & vbCrLf & _
                "  	SELECT TransID, SUM(Principal) AS Principal_Due, SUM(Interest) AS Interest_Due  , SUM(Other1) AS CBU, " & vbCrLf & _
                "   SUM(Other2) AS Savings, SUM(Other3) AS Kasimpatiya" & vbCrLf & _
                "  	FROM tblLoan_Schedule    " & vbCrLf & _
                "  	WHERE Date <= '" & CollectDate & "'   " & vbCrLf & _
                "  	GROUP BY TransID   " & vbCrLf & _
                "   ) AS Scheduled   " & vbCrLf & _
                "   ON  tblLoan.TransID = Scheduled.TransID   " & vbCrLf & _
                "   LEFT JOIN    " & vbCrLf & _
                "   (   " & vbCrLf & _
                "  	SELECT RefTransID AS TransID, SUM(LoanPayment) AS Principal, SUM(IntPayment) AS Interest  , SUM(CBU) AS CBU, " & vbCrLf & _
                "   SUM(Savings) AS Savings, SUM(Kasimpatiya) AS Kasimpatiya" & vbCrLf & _
                "  	FROM View_LoanLedger    " & vbCrLf & _
                "  	WHERE AppDate <= '" & CollectDate & "'   " & vbCrLf & _
                "  	GROUP BY RefTransID   " & vbCrLf & _
                "   ) AS Actual   " & vbCrLf & _
                "   ON  tblLoan.TransID = Actual.TransID   " & vbCrLf & _
                "   WHERE tblLoan.VCECode ='" & VCECode & "' AND tblLoan.Status='Released'   " & vbCrLf & _
                "   AND (ISNULL(Principal_Balance - ISNULL(Actual.Principal,0),0)   " & vbCrLf & _
                "   + ISNULL(Interest_Balance - ISNULL(Actual.Interest,0),0)) > 0  "
        SQL.ReadQuery(query)
        lvARList.Items.Clear()
        If SQL.SQLDR.HasRows Then
            While SQL.SQLDR.Read
                lvARList.Items.Add(SQL.SQLDR("LoanType").ToString)
                lvARList.Items(lvARList.Items.Count - 1).SubItems.Add(String.Format("{0:n2}", SQL.SQLDR("Principal_Balance")))
                lvARList.Items(lvARList.Items.Count - 1).SubItems.Add(String.Format("{0:n2}", SQL.SQLDR("Interest_Balance")))
                lvARList.Items(lvARList.Items.Count - 1).SubItems.Add(String.Format("{0:n2}", SQL.SQLDR("CBU_Balance")))
                lvARList.Items(lvARList.Items.Count - 1).SubItems.Add(String.Format("{0:n2}", SQL.SQLDR("Savings_Balance")))
                lvARList.Items(lvARList.Items.Count - 1).SubItems.Add(String.Format("{0:n2}", SQL.SQLDR("Kasimpatiya_Balance")))
                lvARList.Items(lvARList.Items.Count - 1).SubItems.Add(String.Format("{0:n2}", SQL.SQLDR("Penalty")))
                lvARList.Items(lvARList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Maturity_Date"))
                lvARList.Items(lvARList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Terms").ToString)
                lvARList.Items(lvARList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Loan_No").ToString)
                lvARList.Items(lvARList.Items.Count - 1).SubItems.Add("LN")
            End While
            If lvARList.Items.Count = 1 Then
                lvARList.Items(0).Checked = True
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub lvARList_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles lvARList.KeyDown
        If e.KeyCode = Keys.Enter Then
            CopyToCollection()
        End If
    End Sub

    Private Sub CopyToCollection()
        For Each item As ListViewItem In lvARList.Items
            If item.Checked = True Then
                If CDec(item.SubItems(chAmount.Index).Text) > 0 Then
                    With frmCollection
                        .dgvEntry.Rows.Add(GetAccntCode("Loan", item.SubItems(chType.Index).Text))
                        .dgvEntry.Item(.chAccntTitle.Index, .dgvEntry.Rows.Count - 2).Value = GetAccntTitle(GetAccntCode("Loan", item.SubItems(chType.Index).Text))
                        .dgvEntry.Item(.chCredit.Index, .dgvEntry.Rows.Count - 2).Value = CDec(item.SubItems(chAmount.Index).Text)
                        .dgvEntry.Item(.chRef.Index, .dgvEntry.Rows.Count - 2).Value = item.SubItems(chRefType.Index).Text & ":" & item.SubItems(chRefID.Index).Text
                    End With
                End If
                If CDec(item.SubItems(chInterest.Index).Text) > 0 Then
                    With frmCollection
                        .dgvEntry.Rows.Add(GetAccntCode("Interest", item.SubItems(chType.Index).Text))
                        .dgvEntry.Item(.chAccntTitle.Index, .dgvEntry.Rows.Count - 2).Value = GetAccntTitle(GetAccntCode("Interest", item.SubItems(chType.Index).Text))
                        .dgvEntry.Item(.chCredit.Index, .dgvEntry.Rows.Count - 2).Value = CDec(item.SubItems(chInterest.Index).Text)
                        .dgvEntry.Item(.chRef.Index, .dgvEntry.Rows.Count - 2).Value = item.SubItems(chRefType.Index).Text & ":" & item.SubItems(chRefID.Index).Text
                    End With
                End If

                If CDec(item.SubItems(chCBU.Index).Text) > 0 Then
                    With frmCollection
                        .dgvEntry.Rows.Add(GetAccntCode("CBU", item.SubItems(chType.Index).Text))
                        .dgvEntry.Item(.chAccntTitle.Index, .dgvEntry.Rows.Count - 2).Value = GetAccntTitle(GetAccntCode("CBU", item.SubItems(chType.Index).Text))
                        .dgvEntry.Item(.chCredit.Index, .dgvEntry.Rows.Count - 2).Value = CDec(item.SubItems(chCBU.Index).Text)
                        .dgvEntry.Item(.chRef.Index, .dgvEntry.Rows.Count - 2).Value = item.SubItems(chRefType.Index).Text & ":" & item.SubItems(chRefID.Index).Text
                    End With
                End If

                If CDec(item.SubItems(chSavings.Index).Text) > 0 Then
                    With frmCollection
                        .dgvEntry.Rows.Add(GetAccntCode("Savings", item.SubItems(chType.Index).Text))
                        .dgvEntry.Item(.chAccntTitle.Index, .dgvEntry.Rows.Count - 2).Value = GetAccntTitle(GetAccntCode("Savings", item.SubItems(chType.Index).Text))
                        .dgvEntry.Item(.chCredit.Index, .dgvEntry.Rows.Count - 2).Value = CDec(item.SubItems(chSavings.Index).Text)
                        .dgvEntry.Item(.chRef.Index, .dgvEntry.Rows.Count - 2).Value = item.SubItems(chRefType.Index).Text & ":" & item.SubItems(chRefID.Index).Text
                    End With
                End If

                If CDec(item.SubItems(chKasimpatiya.Index).Text) > 0 Then
                    With frmCollection
                        .dgvEntry.Rows.Add(GetAccntCode("Kasimpatiya", item.SubItems(chType.Index).Text))
                        .dgvEntry.Item(.chAccntTitle.Index, .dgvEntry.Rows.Count - 2).Value = GetAccntTitle(GetAccntCode("Kasimpatiya", item.SubItems(chType.Index).Text))
                        .dgvEntry.Item(.chCredit.Index, .dgvEntry.Rows.Count - 2).Value = CDec(item.SubItems(chKasimpatiya.Index).Text)
                        .dgvEntry.Item(.chRef.Index, .dgvEntry.Rows.Count - 2).Value = item.SubItems(chRefType.Index).Text & ":" & item.SubItems(chRefID.Index).Text
                    End With
                End If
                If CDec(item.SubItems(chPenalty.Index).Text) > 0 Then
                    With frmCollection
                        .dgvEntry.Rows.Add(GetAccntCode("Penalty", item.SubItems(chType.Index).Text))
                        .dgvEntry.Item(.chAccntTitle.Index, .dgvEntry.Rows.Count - 2).Value = GetAccntTitle(GetAccntCode("Penalty", item.SubItems(chType.Index).Text))
                        '.dgvEntry.Item(.chDebit.Index, .dgvEntry.Rows.Count - 2).Value = ""
                        .dgvEntry.Item(.chCredit.Index, .dgvEntry.Rows.Count - 2).Value = CDec(item.SubItems(chPenalty.Index).Text).ToString("N2")
                        .dgvEntry.Item(.chRef.Index, .dgvEntry.Rows.Count - 2).Value = item.SubItems(chRefType.Index).Text & ":" & item.SubItems(chRefID.Index).Text

                    End With
                    'Else
                    '    With frmCollection
                    '        .dgvEntry.Rows.Add(GetAccntCode("Penalty", item.SubItems(chType.Index).Text))
                    '        .dgvEntry.Item(.chAccntTitle.Index, .dgvEntry.Rows.Count - 2).Value = GetAccntTitle(GetAccntCode("Penalty", item.SubItems(chType.Index).Text))
                    '        ' .dgvEntry.Item(.chDebit.Index, .dgvEntry.Rows.Count - 2).Value = ""
                    '        .dgvEntry.Item(.chCredit.Index, .dgvEntry.Rows.Count - 2).Value = CDec(item.SubItems(chPenalty.Index).Text).ToString("N2")
                    '        .dgvEntry.Item(.chRef.Index, .dgvEntry.Rows.Count - 2).Value = item.SubItems(chRefType.Index).Text & ":" & item.SubItems(chRefID.Index).Text
                    '    End With
                End If
                'If CDec(item.SubItems(chPenalty.Index).Text) > 0 Then
                '    With frmCollection
                '        .dgvProducts.Rows.Add()
                '        .dgvProducts.Item(1, .dgvProducts.Rows.Count - 2).Value = GetAccntCode("Penalty", item.SubItems(chType.Index).Text)
                '        .dgvProducts.Item(2, .dgvProducts.Rows.Count - 2).Value = GetAccntTitle(GetAccntCode("Penalty", item.SubItems(chType.Index).Text))
                '        .dgvProducts.Item(3, .dgvProducts.Rows.Count - 2).Value = ""
                '        .dgvProducts.Item(4, .dgvProducts.Rows.Count - 2).Value = CDec(item.SubItems(chPenalty.Index).Text)
                '        .dgvProducts.Item(5, .dgvProducts.Rows.Count - 2).Value = CDec(item.SubItems(chPenalty.Index).Text)
                '        .dgvProducts.Item(6, .dgvProducts.Rows.Count - 2).Value = CDec(item.SubItems(chPenalty.Index).Text)
                '        .dgvProducts.Item(7, .dgvProducts.Rows.Count - 2).Value = 0.0
                '        .dgvProducts.Item(8, .dgvProducts.Rows.Count - 2).Value = 0.0
                '        .dgvProducts.Item(9, .dgvProducts.Rows.Count - 2).Value = item.SubItems(chRefType.Index).Text & ":" & item.SubItems(chRefID.Index).Text
                '    End With
                'End If
            End If
        Next
        If frmCollection.dgvEntry.Rows.Count > 1 Then
            frmCollection.TotalDBCR()
        End If
        Me.Close()
    End Sub

    Private Function GetAccntCode(ByVal Type As String, Category As String) As String
        Dim query As String = ""
        If Type = "Loan" Then
            query = " SELECT LoanAccount AS AccntCode FROM tblLoan_Type WHERE LoanType ='" & Category & "' "
        ElseIf Type = "Interest" Then
            query = " SELECT IntIncomeAccount AS AccntCode FROM tblLoan_Type WHERE LoanType ='" & Category & "' "
        ElseIf Type = "Penalty" Then
            query = " SELECT Penalty_AccountCode AS AccntCode FROM tblLoan_Type WHERE LoanType ='" & Category & "' "
        Else
            query = " SELECT LoanType, tblLoan_ChargesDefault.Description, tblLoan_Charges.DefaultAccount AS AccntCode FROM tblLoan_Type " & _
                    " INNER JOIN  " & _
                    " tblLoan_Charges ON " & _
                    " tblLoan_Charges.LoanCode = tblLoan_Type.LoanCode " & _
                    " INNER JOIN " & _
                    " tblLoan_ChargesDefault ON " & _
                    " tblLoan_ChargesDefault.ChargeID = tblLoan_Charges.ChargeID " & _
                    " WHERE LoanType ='" & Category & "' AND  tblLoan_ChargesDefault.Description = '" & Type & "' "
        End If
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("AccntCode")
        Else
            Return ""
        End If
    End Function

    Private Function GetAccntTitle(ByVal AccntCode As String) As String
        Dim query As String
        query = " SELECT AccountTitle FROM tblCOA_Master  WHERE AccountCode ='" & AccntCode & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("AccountTitle").ToString
        Else
            Return ""
        End If
    End Function


    Private Sub frmReceivables_Enter(sender As System.Object, e As System.EventArgs) Handles MyBase.Enter
        CopyToCollection()
    End Sub

    Private Sub lvARList_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lvARList.SelectedIndexChanged

    End Sub
End Class