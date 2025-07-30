Public Class frmSavings_Interest

    Dim SQL As New SQLControl
    Dim decIntRate As Decimal = 0
    Dim branch As String

    Private Sub frmSavingsAccountInterest_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp

    End Sub
    Private Sub frmSavingsAccountInterest_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDepositType()
        LoadBranches()
        nudYear.Value = Date.Now.Year
        cmbMonth.SelectedIndex = 0
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
        cbBranch.Items.Clear()
        While SQL.SQLDR.Read
            cbBranch.Items.Add(SQL.SQLDR("BranchCode").ToString)
        End While
        If cbBranch.Items.Count > 1 Then
            cbBranch.SelectedIndex = 0
        End If
    End Sub

    Private Sub LoadDepositType()
        Dim selectSQL As String = " SELECT Description FROM tblSavings_Type "
        SQL.ReadQuery(selectSQL)
        cmbSavingsType.Items.Clear()
        While SQL.SQLDR.Read
            cmbSavingsType.Items.Add(SQL.SQLDR("Description").ToString)
        End While
        If cmbSavingsType.Items.Count > 1 Then
            cmbSavingsType.SelectedIndex = 0
        End If
    End Sub

    Private Sub cmbMonth_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbMonth.SelectedIndexChanged
        If cmbMonth.Items.Count = 4 Then
            If cmbMonth.SelectedItem = "1st Quarter" Then
                dtpStart.Value = CDate("01-01-" & nudYear.Value)
                dtpEnd.Value = CDate("03-31-" & nudYear.Value)
            ElseIf cmbMonth.SelectedItem = "2nd Quarter" Then
                dtpStart.Value = CDate("04-01-" & nudYear.Value)
                dtpEnd.Value = CDate("06-30-" & nudYear.Value)
            ElseIf cmbMonth.SelectedItem = "3rd Quarter" Then
                dtpStart.Value = CDate("07-01-" & nudYear.Value)
                dtpEnd.Value = CDate("09-30-" & nudYear.Value)
            ElseIf cmbMonth.SelectedItem = "4th Quarter" Then
                dtpStart.Value = CDate("10-01-" & nudYear.Value)
                dtpEnd.Value = CDate("12-31-" & nudYear.Value)
            End If
        Else
            dtpStart.Value = CDate((cmbMonth.SelectedIndex + 1) & "-1-" & nudYear.Value)
            dtpEnd.Value = dtpStart.Value.AddMonths(1).AddDays(-1)
        End If
        LoadInterest()
    End Sub

    Private Sub LoadInterest()
        Dim selectSQL As String = ""
        selectSQL = " SELECT * FROM tblSavings_Interest WHERE SavingsType = '" & cmbSavingsType.SelectedItem & "' AND Period = '" & cmbMonth.SelectedItem & "' AND Year = '" & nudYear.Value & "' AND Branch = '" & branch & "'"
        SQL.ReadQuery(selectSQL)
        dgvInterest.Rows.Clear()
        Dim boolInterest As Boolean = False
        Dim decTotalInterest As Decimal = 0
        While SQL.SQLDR.Read
            boolInterest = True
            dgvInterest.Rows.Add(
                SQL.SQLDR("VCECode").ToString, _
                SQL.SQLDR("VCEName").ToString, _
                SQL.SQLDR("SavingsType").ToString, _
                SQL.SQLDR("AccountNumber").ToString, _
                SQL.SQLDR("FirstMonth").ToString, _
                SQL.SQLDR("SecondMonth").ToString, _
                SQL.SQLDR("ThirdMonth").ToString, _
                SQL.SQLDR("TotalAverage").ToString, _
                SQL.SQLDR("InterestRate").ToString, _
                SQL.SQLDR("Interest").ToString, _
                SQL.SQLDR("Principal").ToString, _
                SQL.SQLDR("Savings").ToString _
                            )
            decTotalInterest = decTotalInterest + Math.Round(CDec(SQL.SQLDR("Interest").ToString))
        End While

        txtTotalInterest.Text = decTotalInterest.ToString("N2")
    End Sub

    Private Sub btnGenerateInterest_Click(sender As System.Object, e As System.EventArgs) Handles btnGenerateInterest.Click
        Dim selectSQL As String = ""
        branch = Strings.Left(cbBranch.SelectedItem, cbBranch.SelectedItem.ToString.IndexOf(" - "))
        selectSQL = " SELECT Minimum, APR, ISNULL(NoDaysYear, 365) AS NoDaysYear, ISNULL(SavPercent, 100) AS SavPercent, ISNULL(SavPercent2, 0) AS SavPercent2 FROM tblSavings_Type WHERE Description = '" & cmbSavingsType.SelectedItem & "' "
        SQL.ReadQuery(selectSQL)
        Dim strMinimum As String = ""
        Dim decDaysinYear As Decimal = 0
        Dim decRatePrincipal As Decimal = 0
        Dim decRateSavings As Decimal = 0
        While SQL.SQLDR.Read
            strMinimum = SQL.SQLDR("Minimum").ToString
            decIntRate = CDec(SQL.SQLDR("APR").ToString)
            decDaysinYear = CDec(SQL.SQLDR("NoDaysYear").ToString)
            decRateSavings = CDec(SQL.SQLDR("SavPercent").ToString) / 100
            decRatePrincipal = CDec(SQL.SQLDR("SavPercent2").ToString) / 100
        End While
        'selectSQL = " SELECT *, (FirstBalance+SecondBalance+ThirdBalance)/3 AS ADB FROM " & vbCrLf & _
        '            " ( " & vbCrLf & _
        '            " 	SELECT Name.*, ISNULL(FirstBalance, 0) AS FirstBalance, ISNULL(SecondBalance, 0) AS SecondBalance, ISNULL(ThirdBalance, 0) AS ThirdBalance FROM " & vbCrLf & _
        '            " 	( " & vbCrLf & _
        '            " 		SELECT viewSavings.VCECode, VCEName, AccntCode, RefNo FROM viewSavings " & vbCrLf & _
        '            " 		INNER JOIN viewVCE_Master ON viewSavings.VCECode = viewVCE_Master.VCECode " & vbCrLf & _
        '            " 		GROUP BY viewSavings.VCECode, VCEName, AccntCode, RefNo " & vbCrLf & _
        '            " 	) AS Name " & vbCrLf & _
        '            " 	LEFT JOIN " & vbCrLf & _
        '            " 	( " & vbCrLf & _
        '            " 		SELECT  " & vbCrLf & _
        '            " 			VCECode, AccntCode, RefNo, SUM(Deposit + Interest - Withdrawal) AS FirstBalance " & vbCrLf & _
        '            " 		FROM viewSavings " & vbCrLf & _
        '            " 		WHERE AppDate <= DATEADD(DAY,-1, DATEADD(MONTH,1,'" & dtpStart.Value & "')) " & vbCrLf & _
        '            " 		GROUP BY VCECode, AccntCode, RefNo " & vbCrLf & _
        '            " 	) AS FirstMonth " & vbCrLf & _
        '            " 	ON Name.VCECode = FirstMonth.VCECode AND Name.AccntCode = FirstMonth.AccntCode AND Name.RefNo = FirstMonth.RefNo " & vbCrLf & _
        '            " 	LEFT JOIN " & vbCrLf & _
        '            " 	( " & vbCrLf & _
        '            " 		SELECT  " & vbCrLf & _
        '            " 			VCECode, AccntCode, RefNo, SUM(Deposit + Interest - Withdrawal) AS SecondBalance " & vbCrLf & _
        '            " 		FROM viewSavings " & vbCrLf & _
        '            " 		WHERE AppDate <= DATEADD(DAY,-1, DATEADD(MONTH,2,'" & dtpStart.Value & "')) " & vbCrLf & _
        '            " 		GROUP BY VCECode, AccntCode, RefNo " & vbCrLf & _
        '            " 	) AS SecondMonth " & vbCrLf & _
        '            " 	ON Name.VCECode = SecondMonth.VCECode AND Name.AccntCode = SecondMonth.AccntCode AND Name.RefNo = SecondMonth.RefNo " & vbCrLf & _
        '            " 	LEFT JOIN " & vbCrLf & _
        '            " 	( " & vbCrLf & _
        '            " 		SELECT  " & vbCrLf & _
        '            " 			VCECode, AccntCode, RefNo, SUM(Deposit + Interest - Withdrawal) AS ThirdBalance " & vbCrLf & _
        '            " 		FROM viewSavings " & vbCrLf & _
        '            " 		WHERE AppDate <= DATEADD(DAY,-1, DATEADD(MONTH,3,'" & dtpStart.Value & "')) " & vbCrLf & _
        '            " 		GROUP BY VCECode, AccntCode, RefNo " & vbCrLf & _
        '            " 	) AS ThirdMonth " & vbCrLf & _
        '            " 	ON Name.VCECode = ThirdMonth.VCECode AND Name.AccntCode = ThirdMonth.AccntCode AND Name.RefNo = ThirdMonth.RefNo " & vbCrLf & _
        '            " ) AS Final ORDER BY VCEName "
        'selectSQL = " SELECT *, (FirstBalance+SecondBalance+ThirdBalance)/3 AS ADB FROM " & vbCrLf & _
        '            " ( " & vbCrLf & _
        '            " 	SELECT Name.*, ISNULL(FirstBalance, 0) AS FirstBalance, ISNULL(SecondBalance, 0) AS SecondBalance, ISNULL(ThirdBalance, 0) AS ThirdBalance FROM " & vbCrLf & _
        '            " 	( " & vbCrLf & _
        '            " 		SELECT VCECode, VCEName, AccntCode, RefNo FROM view_GL  " & vbCrLf & _
        '            " 		WHERE AppDate <= DATEADD(DAY,-1, DATEADD(MONTH,3,'" & dtpStart.Value & "')) " & vbCrLf & _
        '            " 		GROUP BY VCECode, VCEName, AccntCode, RefNo " & vbCrLf & _
        '            " 	) AS Name " & vbCrLf & _
        '            " 	LEFT JOIN " & vbCrLf & _
        '            " 	( " & vbCrLf & _
        '            " 		SELECT  " & vbCrLf & _
        '            " 			VCECode, AccntCode, RefNo, SUM(Credit - Debit) AS FirstBalance " & vbCrLf & _
        '            " 		FROM tblJE_Details " & vbCrLf & _
        '            " 		WHERE JE_No IN (SELECT JE_No FROM tblJE_Header WHERE AppDate <= DATEADD(DAY,-1, DATEADD(MONTH,1,'" & dtpStart.Value & "')) AND Status <> 'Cancelled') " & vbCrLf & _
        '            " 		AND VCECode IN (SELECT VCECode FROM viewVCE_Master) " & vbCrLf & _
        '            " 		GROUP BY VCECode, AccntCode, RefNo " & vbCrLf & _
        '            " 	) AS FirstMonth " & vbCrLf & _
        '            " 	ON Name.VCECode = FirstMonth.VCECode AND Name.AccntCode = FirstMonth.AccntCode AND Name.RefNo = FirstMonth.RefNo " & vbCrLf & _
        '            " 	LEFT JOIN " & vbCrLf & _
        '            " 	( " & vbCrLf & _
        '            " 		SELECT  " & vbCrLf & _
        '            " 			VCECode, AccntCode, RefNo, SUM(Credit - Debit) AS SecondBalance " & vbCrLf & _
        '            " 		FROM tblJE_Details " & vbCrLf & _
        '            " 		WHERE JE_No IN (SELECT JE_No FROM tblJE_Header WHERE AppDate <= DATEADD(DAY,-1, DATEADD(MONTH,2,'" & dtpStart.Value & "')) AND Status <> 'Cancelled') " & vbCrLf & _
        '            " 		AND VCECode IN (SELECT VCECode FROM viewVCE_Master) " & vbCrLf & _
        '            " 		GROUP BY VCECode, AccntCode, RefNo " & vbCrLf & _
        '            " 	) AS SecondMonth " & vbCrLf & _
        '            " 	ON Name.VCECode = SecondMonth.VCECode AND Name.AccntCode = SecondMonth.AccntCode AND Name.RefNo = SecondMonth.RefNo " & vbCrLf & _
        '            " 	LEFT JOIN " & vbCrLf & _
        '            " 	( " & vbCrLf & _
        '            " 		SELECT  " & vbCrLf & _
        '            " 			VCECode, AccntCode, RefNo, SUM(Credit - Debit) AS ThirdBalance " & vbCrLf & _
        '            " 		FROM tblJE_Details " & vbCrLf & _
        '            " 		WHERE JE_No IN (SELECT JE_No FROM tblJE_Header WHERE AppDate <= DATEADD(DAY,-1, DATEADD(MONTH,3,'" & dtpStart.Value & "')) AND Status <> 'Cancelled') " & vbCrLf & _
        '            " 		AND VCECode IN (SELECT VCECode FROM viewVCE_Master) " & vbCrLf & _
        '            " 		GROUP BY VCECode, AccntCode, RefNo " & vbCrLf & _
        '            " 	) AS ThirdMonth " & vbCrLf & _
        '            " 	ON Name.VCECode = ThirdMonth.VCECode AND Name.AccntCode = ThirdMonth.AccntCode AND Name.RefNo = ThirdMonth.RefNo " & vbCrLf & _
        '            " ) AS Final ORDER BY VCEName "
        'selectSQL = " SELECT *, (FirstBalance+SecondBalance+ThirdBalance)/3 AS ADB FROM " & vbCrLf & _
        '            " ( " & vbCrLf & _
        '            " 	SELECT Name.*, ISNULL(FirstBalance, 0) AS FirstBalance, ISNULL(SecondBalance, 0) AS SecondBalance, ISNULL(ThirdBalance, 0) AS ThirdBalance FROM " & vbCrLf & _
        '            " 	( " & vbCrLf & _
        '            " 		SELECT VCECode, VCEName, AccntCode, RefNo FROM view_GL  " & vbCrLf & _
        '            " 		WHERE AppDate <= DATEADD(DAY,-1, DATEADD(MONTH,3,'" & dtpStart.Value & "')) " & vbCrLf & _
        '            " 		AND AccntCode IN (SELECT SavAccountCode FROM tblSavings_Type WHERE Description = '" & cmbSavingsType.SelectedItem & "') " & vbCrLf & _
        '            " 		GROUP BY VCECode, VCEName, AccntCode, RefNo " & vbCrLf & _
        '            " 	) AS Name " & vbCrLf & _
        '            " 	LEFT JOIN " & vbCrLf & _
        '            " 	( " & vbCrLf & _
        '            " 		SELECT  " & vbCrLf & _
        '            " 			VCECode, AccntCode, RefNo, SUM(Credit - Debit) AS FirstBalance " & vbCrLf & _
        '            " 		FROM " & vbCrLf & _
        '            " 		( " & vbCrLf & _
        '            " 		    SELECT " & vbCrLf & _
        '            " 		        JE_No, " & vbCrLf & _
        '            " 		        VCECode, " & vbCrLf & _
        '            " 		        AccntCode, " & vbCrLf & _
        '            " 		        RefNo, " & vbCrLf & _
        '            " 		        CASE WHEN (SELECT Type FROM tblSavings_Type WHERE Description = '" & cmbSavingsType.SelectedItem & "') = 'Compounding Interest' AND Particulars = 'Interest' AND YEAR(AppDate) = YEAR('" & dtpStart.Value & "') THEN 0 ELSE Credit END AS Credit, " & vbCrLf & _
        '            " 		        Debit " & vbCrLf & _
        '            " 		    FROM View_GL " & vbCrLf & _
        '            " 		    WHERE AppDate <= DATEADD(DAY,-1, DATEADD(MONTH,1,'" & dtpStart.Value & "')) AND Status <> 'Cancelled' " & vbCrLf & _
        '            " 		    AND AccntCode IN (SELECT SavAccountCode FROM tblSavings_Type WHERE Description = '" & cmbSavingsType.SelectedItem & "') " & vbCrLf & _
        '            " 		) AS tblJE_Details " & vbCrLf & _
        '            " 		WHERE VCECode IN (SELECT VCECode FROM viewVCE_Master) " & vbCrLf & _
        '            " 		GROUP BY VCECode, AccntCode, RefNo " & vbCrLf & _
        '            " 	) AS FirstMonth " & vbCrLf & _
        '            " 	ON Name.VCECode = FirstMonth.VCECode AND Name.AccntCode = FirstMonth.AccntCode AND Name.RefNo = FirstMonth.RefNo " & vbCrLf & _
        '            " 	LEFT JOIN " & vbCrLf & _
        '            " 	( " & vbCrLf & _
        '            " 		SELECT  " & vbCrLf & _
        '            " 			VCECode, AccntCode, RefNo, SUM(Credit - Debit) AS SecondBalance " & vbCrLf & _
        '            " 		FROM " & vbCrLf & _
        '            " 		( " & vbCrLf & _
        '            " 		    SELECT " & vbCrLf & _
        '            " 		        JE_No, " & vbCrLf & _
        '            " 		        VCECode, " & vbCrLf & _
        '            " 		        AccntCode, " & vbCrLf & _
        '            " 		        RefNo, " & vbCrLf & _
        '            " 		        CASE WHEN (SELECT Type FROM tblSavings_Type WHERE Description = '" & cmbSavingsType.SelectedItem & "') = 'Compounding Interest' AND Particulars = 'Interest' AND YEAR(AppDate) = YEAR('" & dtpStart.Value & "') THEN 0 ELSE Credit END AS Credit, " & vbCrLf & _
        '            " 		        Debit " & vbCrLf & _
        '            " 		    FROM View_GL " & vbCrLf & _
        '            " 		    WHERE AppDate <= DATEADD(DAY,-1, DATEADD(MONTH,2,'" & dtpStart.Value & "')) AND Status <> 'Cancelled' " & vbCrLf & _
        '            " 		    AND AccntCode IN (SELECT SavAccountCode FROM tblSavings_Type WHERE Description = '" & cmbSavingsType.SelectedItem & "') " & vbCrLf & _
        '            " 		) AS tblJE_Details " & vbCrLf & _
        '            " 		WHERE VCECode IN (SELECT VCECode FROM viewVCE_Master) " & vbCrLf & _
        '            " 		GROUP BY VCECode, AccntCode, RefNo " & vbCrLf & _
        '            " 	) AS SecondMonth " & vbCrLf & _
        '            " 	ON Name.VCECode = SecondMonth.VCECode AND Name.AccntCode = SecondMonth.AccntCode AND Name.RefNo = SecondMonth.RefNo " & vbCrLf & _
        '            " 	LEFT JOIN " & vbCrLf & _
        '            " 	( " & vbCrLf & _
        '            " 		SELECT  " & vbCrLf & _
        '            " 			VCECode, AccntCode, RefNo, SUM(Credit - Debit) AS ThirdBalance " & vbCrLf & _
        '            " 		FROM " & vbCrLf & _
        '            " 		( " & vbCrLf & _
        '            " 		    SELECT " & vbCrLf & _
        '            " 		        JE_No, " & vbCrLf & _
        '            " 		        VCECode, " & vbCrLf & _
        '            " 		        AccntCode, " & vbCrLf & _
        '            " 		        RefNo, " & vbCrLf & _
        '            " 		        CASE WHEN (SELECT Type FROM tblSavings_Type WHERE Description = '" & cmbSavingsType.SelectedItem & "') = 'Compounding Interest' AND Particulars = 'Interest' AND YEAR(AppDate) = YEAR('" & dtpStart.Value & "') THEN 0 ELSE Credit END AS Credit, " & vbCrLf & _
        '            " 		        Debit " & vbCrLf & _
        '            " 		    FROM View_GL " & vbCrLf & _
        '            " 		    WHERE AppDate <= DATEADD(DAY,-1, DATEADD(MONTH,3,'" & dtpStart.Value & "')) AND Status <> 'Cancelled' " & vbCrLf & _
        '            " 		    AND AccntCode IN (SELECT SavAccountCode FROM tblSavings_Type WHERE Description = '" & cmbSavingsType.SelectedItem & "') " & vbCrLf & _
        '            " 		) AS tblJE_Details " & vbCrLf & _
        '            " 		WHERE VCECode IN (SELECT VCECode FROM viewVCE_Master) " & vbCrLf & _
        '            " 		GROUP BY VCECode, AccntCode, RefNo " & vbCrLf & _
        '            " 	) AS ThirdMonth " & vbCrLf & _
        '            " 	ON Name.VCECode = ThirdMonth.VCECode AND Name.AccntCode = ThirdMonth.AccntCode AND Name.RefNo = ThirdMonth.RefNo " & vbCrLf & _
        '            " ) AS Final ORDER BY VCEName "
        selectSQL = " SELECT " & vbCrLf & _
                    " 	ADB.VCECode, " & vbCrLf & _
                    " 	VCEName, " & vbCrLf & _
                    " 	ISNULL(AccountNumber, '') AS AccountNumber, " & vbCrLf & _
                    " 	AccntCode, " & vbCrLf & _
                    " 	AccntTitle, " & vbCrLf & _
                    " 	SUM(Credit - Debit) AS Balance, " & vbCrLf & _
                    " 	SUM(DayDiff) AS DayDiff, " & vbCrLf & _
                    " 	SUM(DailyBalance) AS DailyBalance, " & vbCrLf & _
                    " 	SUM(DailyBalance) / SUM(DayDiff) AS ADB " & vbCrLf & _
                    " FROM " & vbCrLf & _
                    " ( " & vbCrLf & _
                    " 	SELECT " & vbCrLf & _
                    " 		ID, " & vbCrLf & _
                    " 		RefType, " & vbCrLf & _
                    " 		RefTransID, " & vbCrLf & _
                    " 		VCECode, " & vbCrLf & _
                    " 		VCEName, " & vbCrLf & _
                    " 		AccntCode, " & vbCrLf & _
                    " 		AccntTitle, " & vbCrLf & _
                    " 		Debit, " & vbCrLf & _
                    " 		Credit, " & vbCrLf & _
                    " 		ISNULL(SUM(Credit - Debit) OVER(PARTITION BY VCECode, AccntCode ORDER BY AppDate, ID), 0) AS Balance, " & vbCrLf & _
                    " 		AppDate, " & vbCrLf & _
                    " 		ISNULL(LEAD(AppDate) OVER(PARTITION BY VCECode, AccntCode ORDER BY AppDate, ID), '" & dtpEnd.Value & "') AS NextDate, " & vbCrLf & _
                    " 		DATEDIFF(DAY, AppDate, ISNULL(LEAD(AppDate) OVER(PARTITION BY VCECode, AccntCode ORDER BY AppDate, ID), '" & dtpEnd.Value & "')) AS DayDiff, " & vbCrLf & _
                    " 		(ISNULL(SUM(Credit - Debit) OVER(PARTITION BY VCECode, AccntCode ORDER BY AppDate, ID), 0) * DATEDIFF(DAY, AppDate, ISNULL(LEAD(AppDate) OVER(PARTITION BY VCECode, AccntCode ORDER BY AppDate, ID), '" & dtpEnd.Value & "'))) AS DailyBalance " & vbCrLf & _
                    " 	FROM " & vbCrLf & _
                    " 	( " & vbCrLf & _
                    " 		SELECT " & vbCrLf & _
                    " 			'0' AS ID, " & vbCrLf & _
                    " 			'BB' AS RefType, " & vbCrLf & _
                    " 			'1' AS RefTransID, " & vbCrLf & _
                    " 			VCECode, " & vbCrLf & _
                    " 			VCEName, " & vbCrLf & _
                    " 			AccntCode, " & vbCrLf & _
                    " 			AccntTitle, " & vbCrLf & _
                    " 			0 AS Debit, " & vbCrLf & _
                    " 			SUM(Credit - Debit) AS Credit, " & vbCrLf & _
                    " 			'" & dtpStart.Value & "' AS AppDate " & vbCrLf & _
                    " 		FROM view_GL " & vbCrLf & _
                    " 		WHERE Status <> 'Cancelled' " & vbCrLf & _
                    " 		AND AccntCode IN (SELECT SavAccountCode FROM tblSavings_Type WHERE Description = '" & cmbSavingsType.SelectedItem & "') " & vbCrLf & _
                    " 		AND (AppDate < '" & dtpStart.Value & "' OR RefType = 'BB') " & vbCrLf & _
                    " 		AND VCECode IN (SELECT VCECode FROM viewVCE_Master) AND BranchCode = '" & branch & "' " & vbCrLf & _
                    " 		GROUP BY VCECode, VCEName, AccntCode, AccntTitle " & vbCrLf & _
                    " 		UNION ALL " & vbCrLf & _
                    " 		SELECT " & vbCrLf & _
                    " 			ID, " & vbCrLf & _
                    " 			RefType, " & vbCrLf & _
                    " 			RefTransID, " & vbCrLf & _
                    " 			VCECode, " & vbCrLf & _
                    " 			VCEName, " & vbCrLf & _
                    " 			AccntCode, " & vbCrLf & _
                    " 			AccntTitle, " & vbCrLf & _
                    " 			Debit, " & vbCrLf & _
                    " 			Credit, " & vbCrLf & _
                    " 			AppDate " & vbCrLf & _
                    " 		FROM view_GL " & vbCrLf & _
                    " 		WHERE Status <> 'Cancelled' AND AccntCode IN (SELECT SavAccountCode FROM tblSavings_Type WHERE Description = '" & cmbSavingsType.SelectedItem & "') " & vbCrLf & _
                    " 		AND AppDate BETWEEN '" & dtpStart.Value & "' AND '" & dtpEnd.Value & "' AND RefType <> 'BB' " & vbCrLf & _
                    " 		AND VCECode IN (SELECT VCECode FROM viewVCE_Master) AND BranchCode = '" & branch & "'" & vbCrLf & _
                    " 	) AS DB " & vbCrLf & _
                    " ) AS ADB " & vbCrLf & _
                    " LEFT JOIN tblSavings_Account " & vbCrLf & _
                    " ON tblSavings_Account.VCECode = ADB.VCECode AND tblSavings_Account.DepositType = ADB.AccntTitle " & vbCrLf & _
                    " GROUP BY ADB.VCECode, VCEName, ISNULL(AccountNumber, ''), AccntCode, AccntTitle " & vbCrLf & _
                    " HAVING SUM(DayDiff) > 0 "
        SQL.CloseCon()
        SQL.ReadQuery(selectSQL)
        dgvInterest.Rows.Clear()
        Dim decTotalInterest As Decimal = 0
        While SQL.SQLDR.Read
            If CDec(SQL.SQLDR("ADB").ToString) >= CDec(strMinimum) Then
                Dim decRate As Decimal = 0
                Dim decRatePrinc As Decimal = decRatePrincipal
                Dim decRateSav As Decimal = decRateSavings
                decRate = (decIntRate / 100) / decDaysinYear * CDec(SQL.SQLDR("DayDiff").ToString)
                If CDec(SQL.SQLDR("ADB")) < 10000 And decRatePrincipal > 0 Then
                    decRatePrinc = 1
                    decRateSav = 0
                End If
                dgvInterest.Rows.Add(SQL.SQLDR("VCECode").ToString, _
                                     SQL.SQLDR("VCEName").ToString, _
                                     cmbSavingsType.SelectedItem, _
                                     SQL.SQLDR("AccountNumber").ToString, _
                                     CDec(SQL.SQLDR("Balance")).ToString("N2"), _
                                     CDec(SQL.SQLDR("DayDiff")).ToString("N2"), _
                                     CDec(SQL.SQLDR("DailyBalance")).ToString("N2"), _
                                     CDec(SQL.SQLDR("ADB")).ToString("N2"), _
                                     Math.Round(decRate, 4), _
                                     CDec(Math.Round(CDec(SQL.SQLDR("ADB").ToString) * decRate, 2)).ToString("N2"), _
                                     CDec(Math.Round(Math.Round(CDec(SQL.SQLDR("ADB").ToString) * decRate, 2) * decRatePrinc, 2)).ToString("N2"), _
                                     CDec(Math.Round(CDec(SQL.SQLDR("ADB").ToString) * decRate, 2) - Math.Round(Math.Round(CDec(SQL.SQLDR("ADB").ToString) * decRate, 2) * decRatePrinc, 2)).ToString("N2")
                                     )
                decTotalInterest = decTotalInterest + Math.Round(CDec(SQL.SQLDR("ADB").ToString) * decRate, 2)
            End If
        End While
        txtTotalInterest.Text = decTotalInterest.ToString("N2")
    End Sub

    Private Function GetMinDateTime(ByVal strVCECode As String, ByVal strAccntCode As String) As Integer

        Dim selectSQL As String = " SELECT ISNULL(DATEDIFF(MONTH, MIN(AppDate), '" & dtpEnd.Value & "'), 0) AS Date FROM view_GL " & vbCrLf & _
                                  " WHERE VCECode = '" & strVCECode & "' AND AccntCode = '" & strAccntCode & "' "
        SQL.ReadQuery(selectSQL, 2)
        If SQL.SQLDR2.Read Then
            Return CInt(SQL.SQLDR2("Date").ToString)
        Else
            Return 0
        End If

    End Function

    Private Sub cmbSavingsType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbSavingsType.SelectedIndexChanged
        'Dim selectSQL As String = "SELECT ((APR/100)/Days_In_Year)*Days_In_Month AS IntRate FROM Savings_Type WHERE AccntTitle = '" & cmbSavingsType.SelectedItem & "'"
        'SQL.ReadQuery(selectSQL)
        'While SQL.SQLDR.Read
        '    decIntRate = CDec(SQL.SQLDR("IntRate").ToString)
        'End While
        Dim selectSQL As String = "SELECT * FROM tblSavings_Type WHERE Description = '" & cmbSavingsType.SelectedItem & "'"
        SQL.ReadQuery(selectSQL)
        cmbMonth.Items.Clear()
        While SQL.SQLDR.Read
            If SQL.SQLDR("Period").ToString = "Quarterly" Then
                cmbMonth.Items.Add("1st Quarter")
                cmbMonth.Items.Add("2nd Quarter")
                cmbMonth.Items.Add("3rd Quarter")
                cmbMonth.Items.Add("4th Quarter")
            Else
                cmbMonth.Items.Add("January")
                cmbMonth.Items.Add("February")
                cmbMonth.Items.Add("March")
                cmbMonth.Items.Add("April")
                cmbMonth.Items.Add("May")
                cmbMonth.Items.Add("June")
                cmbMonth.Items.Add("July")
                cmbMonth.Items.Add("August")
                cmbMonth.Items.Add("September")
                cmbMonth.Items.Add("October")
                cmbMonth.Items.Add("November")
                cmbMonth.Items.Add("December")
            End If
            decIntRate = CDec(SQL.SQLDR("APR").ToString)
        End While
    End Sub
    Dim strName As String = ""
    Private Sub dgvInterest_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles dgvInterest.KeyUp
        If e.Modifiers = Keys.Control And e.KeyCode = Keys.F Then
            strName = InputBox("Name:", "Search", strName)
            If strName <> "" Then
                Dim rowIndex As Integer = 0
                If dgvInterest.SelectedCells.Count > 0 Then
                    rowIndex = dgvInterest.SelectedCells(0).RowIndex + 1
                End If
                Dim boolSearch As Boolean = False
                For i As Integer = rowIndex To dgvInterest.Rows.Count - 1
                    If dgvInterest.Rows(i).Cells(chVCEName.Index).Value.ToString.ToLower.Contains(strName.ToLower) Then
                        dgvInterest.Rows(i).Cells(chVCEName.Index).Selected = True
                        boolSearch = True
                        Exit For
                    End If
                Next
                If boolSearch = False Then
                    MsgBox("No Result")
                    If dgvInterest.Rows.Count > 1 Then
                        dgvInterest.Rows(0).Cells(chVCEName.Index).Selected = True
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        If MsgBox("Are you sure you want to save this Interest?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
            Dim selectSQL As String = ""
            Dim deleteSQL As String = ""
            Dim insertSQL As String = ""

            deleteSQL = "DELETE FROM tblSavings_Interest WHERE SavingsType = '" & cmbSavingsType.SelectedItem & "' AND Period = '" & cmbMonth.SelectedItem & "' AND Year = '" & nudYear.Value & "'  AND Branch = '" & branch & "' "
            SQL.CloseCon()
            SQL.ExecNonQuery(deleteSQL)

            For i As Integer = 0 To dgvInterest.Rows.Count - 1
                insertSQL = " INSERT INTO tblSavings_Interest(VCECode, VCEName, AccountNumber, FirstMonth, SecondMonth, ThirdMonth, TotalAverage, InterestRate, Interest, Principal, Savings, SavingsType, Period, Year, Branch) " & _
                            " VALUES " & vbCrLf & _
                            " ( " & vbCrLf & _
                            "       '" & dgvInterest.Rows(i).Cells(chVCECode.Index).Value & "', " & vbCrLf & _
                            "       '" & dgvInterest.Rows(i).Cells(chVCEName.Index).Value.ToString.Replace("'", "''") & "', " & vbCrLf & _
                            "       '" & dgvInterest.Rows(i).Cells(chAccountNumber.Index).Value & "', " & vbCrLf & _
                            "       '" & CDec(dgvInterest.Rows(i).Cells(chBalance.Index).Value) & "', " & vbCrLf & _
                            "       '" & CDec(dgvInterest.Rows(i).Cells(chDays.Index).Value) & "', " & vbCrLf & _
                            "       '" & CDec(dgvInterest.Rows(i).Cells(chDailyBalance.Index).Value) & "', " & vbCrLf & _
                            "       '" & CDec(dgvInterest.Rows(i).Cells(chADB.Index).Value) & "', " & vbCrLf & _
                            "       '" & CDec(dgvInterest.Rows(i).Cells(chIntRate.Index).Value) & "', " & vbCrLf & _
                            "       '" & CDec(dgvInterest.Rows(i).Cells(chInterest.Index).Value) & "', " & vbCrLf & _
                            "       '" & CDec(dgvInterest.Rows(i).Cells(chPrincipal.Index).Value) & "', " & vbCrLf & _
                            "       '" & CDec(dgvInterest.Rows(i).Cells(chSavings.Index).Value) & "', " & vbCrLf & _
                            "       '" & cmbSavingsType.SelectedItem & "', " & vbCrLf & _
                            "       '" & cmbMonth.SelectedItem & "', " & vbCrLf & _
                            "       '" & nudYear.Value & "', " & vbCrLf & _
                             "       '" & branch & "' " & vbCrLf & _
                            " ) "
                SQL.ExecNonQuery(insertSQL)
            Next
            MsgBox("Successfully Saved!", MsgBoxStyle.Information, "Save")
        End If
    End Sub

    Private Sub PostToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PostToolStripMenuItem.Click
        If cmbSavingsType.SelectedIndex > -1 And cmbMonth.SelectedIndex > -1 Then
            If MsgBox("Are you sure you want to post " & cmbSavingsType.SelectedItem & " (" & nudYear.Value & ")", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Post") = MsgBoxResult.Yes Then
                Dim selectSQL As String = ""
                selectSQL = " SELECT " & vbCrLf & _
                            "   SavAccountCode, " & vbCrLf & _
                            "   VCECode, " & vbCrLf & _
                            "   VCEName, " & vbCrLf & _
                            "   AccountNumber, " & vbCrLf & _
                            "   Interest, " & vbCrLf & _
                            "   Principal, " & vbCrLf & _
                            "   Savings, " & vbCrLf & _
                            "   ExpAccountCode " & vbCrLf & _
                            " FROM tblSavings_Interest " & vbCrLf & _
                            " INNER JOIN tblSavings_Type " & vbCrLf & _
                            " ON SavingsType = Description " & vbCrLf & _
                            " WHERE SavingsType = '" & cmbSavingsType.SelectedItem & "' AND tblSavings_Interest.Period = '" & cmbMonth.SelectedItem & "' AND Year = '" & nudYear.Value & "' AND Branch = '" & branch & "' "
                SQL.CloseCon()
                SQL.ReadQuery(selectSQL)
                Dim f As New frmJV
                f.tsbNew.PerformClick()
                Dim count As Integer = 1
                Dim strAccntCode As String = ""
                Dim totalAmount As Decimal = 0
                While SQL.SQLDR.Read
                    If count = 1 Then
                        strAccntCode = SQL.SQLDR("ExpAccountCode").ToString
                    End If
                    Dim decPrincipal As Decimal = CDec(SQL.SQLDR("Principal").ToString)
                    Dim decSavings As Decimal = CDec(SQL.SQLDR("Savings").ToString)
                    If decPrincipal > 0 Then
                        selectSQL = "SELECT TOP 1 AccntCode FROM view_SL WHERE AccountTitle LIKE '%paid%up%' ORDER BY AccntCode"
                        SQL.ReadQuery(selectSQL, 2)
                        If SQL.SQLDR2.Read Then
                            f.dgvEntry.Rows.Add("")
                            f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chAccntCode.Index).Value = SQL.SQLDR2("AccntCode").ToString
                            f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chAccntTitle.Index).Value = GetAccntTitle(SQL.SQLDR2("AccntCode").ToString)
                            f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chDebit.Index).Value = ""
                            f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chCredit.Index).Value = CDec(SQL.SQLDR("Principal").ToString).ToString("N2")
                            f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                            f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                            f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chParticulars.Index).Value = "Interest"
                            f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chBranchCode.Index).Value = branch
                            decPrincipal = 0
                        End If
                    End If
                    If decSavings > 0 Then
                        f.dgvEntry.Rows.Add("")
                        f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chAccntCode.Index).Value = SQL.SQLDR("SavAccountCode").ToString
                        f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chAccntTitle.Index).Value = GetAccntTitle(SQL.SQLDR("SavAccountCode").ToString)
                        f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chDebit.Index).Value = ""
                        f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chCredit.Index).Value = CDec(CDec(SQL.SQLDR("Savings").ToString) + decPrincipal).ToString("N2")
                        f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                        f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                        f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chRefNo.Index).Value = SQL.SQLDR("AccountNumber").ToString
                        f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chParticulars.Index).Value = "Interest"
                        f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chBranchCode.Index).Value = branch
                       
                    End If
                    totalAmount += CDec(SQL.SQLDR("Interest").ToString)
                    count += 1
                End While

                f.dgvEntry.Rows.Add("")
                f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chAccntCode.Index).Value = strAccntCode
                f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chAccntTitle.Index).Value = GetAccntTitle(strAccntCode)
                f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chDebit.Index).Value = totalAmount.ToString("N2")
                f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chCredit.Index).Value = ""
                f.dgvEntry.Rows(f.dgvEntry.Rows.Count - 2).Cells(f.chBranchCode.Index).Value = branch

                f.interestgen = True
                f.interestBranchCode = branch
                f.Show()
                f.TotalDBCR()
            End If
        End If
    End Sub

    Private Sub cbBranch_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbBranch.SelectedIndexChanged
        branch = Strings.Left(cbBranch.SelectedItem, cbBranch.SelectedItem.ToString.IndexOf(" - "))
        LoadInterest()
    End Sub

    Private Sub btnExport_Click(sender As System.Object, e As System.EventArgs) Handles btnExport.Click
        Try
            If ((dgvInterest.Columns.Count = 0) Or (dgvInterest.Rows.Count = 0)) Then
                MsgBox("No Records to Export!", MsgBoxStyle.Exclamation, "Message Alert")
            Else
                If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Export(FolderBrowserDialog1.SelectedPath)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Export(ByVal Path As String)
        Dim DSet As New DataSet
        DSet.Tables.Add()
        For i As Integer = 0 To dgvInterest.ColumnCount - 1
            DSet.Tables(0).Columns.Add(dgvInterest.Columns(i).HeaderText)
        Next
        'add rows to the table
        Dim row As DataRow
        For i As Integer = 0 To dgvInterest.RowCount - 1
            row = DSet.Tables(0).NewRow
            For j As Integer = 0 To dgvInterest.Columns.Count - 1
                row(j) = dgvInterest.Rows(i).Cells(j).Value
            Next
            DSet.Tables(0).Rows.Add(row)
        Next

        Dim excel As New Microsoft.Office.Interop.Excel.Application
        Dim wBook As Microsoft.Office.Interop.Excel.Workbook
        Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet

        wBook = excel.Workbooks.Add()
        wSheet = wBook.ActiveSheet()

        Dim dt As System.Data.DataTable = DSet.Tables(0)
        Dim dc As System.Data.DataColumn
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        For Each dc In dt.Columns
            colIndex = colIndex + 1
            excel.Cells(1, colIndex) = dc.ColumnName
        Next

        For Each dr In dt.Rows
            rowIndex = rowIndex + 1
            colIndex = 0
            For Each dc In dt.Columns
                colIndex = colIndex + 1
                excel.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
            Next
        Next
        wSheet.Columns.AutoFit()
        Dim strFileName As String
        strFileName = FolderBrowserDialog1.SelectedPath & "\" & "ExtractedReport" & Date.Today.Month.ToString("00") & Date.Today.Day.ToString("00") & Date.Today.Year.ToString("00") & Now.Hour.ToString("00") & Now.Minute.ToString("00") & ".xls"

        Dim blnFileOpen As Boolean = False

        If System.IO.File.Exists(strFileName) Then
            System.IO.File.Delete(strFileName)
        End If

        wBook.SaveAs(strFileName)
        excel.Workbooks.Open(strFileName)
        excel.Visible = True
        MsgBox("Exported Succesfully", MsgBoxStyle.Information, "Message Alert")
    End Sub
End Class