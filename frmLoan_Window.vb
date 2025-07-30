Public Class frmLoan_Window
    Dim TransID As String
    Dim LoanNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "LN"
    Dim ColumnPK As String = "Loan_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblLoan"
    Dim TransAuto As Boolean
    Dim DR_ID As Integer

    Dim proceeds As Double
    Dim loanType As String = ""
    Dim memType As String = ""
    Dim Member_No As String = ""
    Dim updateType As Integer = 0
    Dim restruct As Boolean = False
    Dim interestAmt, intRate, intValue, principal, noOfAmort, Amort, totalAmmort As Decimal
    Dim intMethod As String
    Dim intAmort As Boolean
    Dim addOnAmt As Decimal = 0

    Dim factor As Decimal
    Dim ab As Integer
    Dim Cash_Button As Boolean = False

    Dim Loan_Code As String = ""
    Dim borrower As Integer = 0
    Dim CV_Type As String = ""

    Dim strIntValue As String = ""
    Dim strNote As String = ""

#Region "FORM EVENTS"

    Public Overloads Function ShowDialog(ByVal ID As String) As Boolean
        TransID = ID
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmLoanWindow_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            LoadLoanType()
            LoadDGVAmortMethod()
            LoadComboboxAmortMethod()
            cbPaymentType.SelectedIndex = 0
            If TransID <> "" Then
                LoadLoan(TransID)
                EnableControl(False)
                TabControl1.SelectTab(tpPayment)
            End If

            tsbSearch.Enabled = True
            tsbNew.Enabled = True
            tsbEdit.Enabled = False
            tsbSave.Enabled = False
            tsbCancel.Enabled = False
            tsbExtend.Enabled = False
            tsbClose.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = True
            tsbPrint.Enabled = False
            tsbCopy.Enabled = False
            EnableControl(False)
            EnableControlNote(False)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadDGVAmortMethod()
        Dim dgvCB As DataGridViewComboBoxColumn
        dgvCB = dgvCharges.Columns(dgcAmortMethod.Index)

        dgvCB.Items.Clear()
        dgvCB.Items.Add("Less to Proceeds")
        dgvCB.Items.Add("Amortize")
        dgvCB.Items.Add("Add On")
        dgvCB.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
    End Sub

    Private Sub LoadComboboxAmortMethod()
        cbAmortMethod.Items.Clear()
        cbAmortMethod.Items.Add("Less to Proceeds")
        cbAmortMethod.Items.Add("Amortize")
        cbAmortMethod.Items.Add("Add On")
    End Sub

    Private Sub frmLoan_Window_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control = True Then
            If e.KeyCode = Keys.S Then
                If tsbSave.Enabled = True Then tsbSave.PerformClick()
            ElseIf e.KeyCode = Keys.F Then
                If tsbSearch.Enabled = True Then tsbSearch.PerformClick()
            ElseIf e.KeyCode = Keys.N Then
                If tsbNew.Enabled = True Then tsbNew.PerformClick()
            ElseIf e.KeyCode = Keys.E Then
                If tsbEdit.Enabled = True Then tsbEdit.PerformClick()
            ElseIf e.KeyCode = Keys.D Then
                If tsbCancel.Enabled = True Then tsbCancel.PerformClick()
            ElseIf e.KeyCode = Keys.Left Then
                If tsbPrevious.Enabled = True Then tsbPrevious.PerformClick()
            ElseIf e.KeyCode = Keys.Right Then
                If tsbNext.Enabled = True Then tsbNext.PerformClick()
            ElseIf e.KeyCode = Keys.P Then
                If tsbPrint.Enabled = True Then tsbPrint.PerformClick()
            ElseIf e.KeyCode = Keys.R Then
                If tsbReports.Enabled = True Then tsbReports.PerformClick()
            ElseIf e.KeyCode = Keys.C Then
                If tsbReports.Enabled = True Then tsbCopy.ShowDropDown()
            End If
        ElseIf e.Alt = True Then
            If e.KeyCode = Keys.F4 Then
                If tsbExit.Enabled = True Then
                    tsbExit.PerformClick()
                Else
                    e.SuppressKeyPress = True
                End If
            End If
        ElseIf e.KeyCode = Keys.Escape Then
            If tsbClose.Enabled = True Then tsbClose.PerformClick()
        End If
    End Sub
#End Region

#Region "MENU EVENTS"
    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("LN_ADD") Then
            msgRestricted()
        Else
            ' CLEAR TRANSACTION RECORDS
            ClearText()
            TransID = ""
            LoanNo = ""

            cbMode.SelectedItem = "Semi-Monthly"

            ' LOAD MAINTENANCE VALUES
            LoadLoanType()

            ' SET TOOLSTRIP BUTTONS
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbCancel.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
            tsbPrint.Enabled = False
            tsbCopy.Enabled = True
            EnableControl(True)

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
        End If
    End Sub

    'Public Function GenerateTransNum(ByRef Auto As Boolean, ModuleID As String, ColumnPK As String, Table As String, _
    '                                 Optional ByRef Branch As String = "", Optional ByRef Business As String = "", Optional ByRef WHSE As String = "") As String
    '    Dim TransNum As String = ""
    '    If Auto = True Then
    '        ' GENERATE TRANS ID 
    '        Dim query As String
    '        Do
    '            query = " SELECT	GlobalSeries, ISNULL(BranchCode,'All') AS BranchCode, ISNULL(BusinessCode,'All') AS BusinessCode,  " & _
    '                    " 		    ISNULL(Prefix,'') AS Prefix, ISNULL(Digits,6) AS Digits, " & _
    '                    "           ISNULL(StartRecord,1) AS StartRecord, LEN(ISNULL(Prefix,'')) + ISNULL(Digits,6) AS ID_Length, " & _
    '                    "           ISNULL(BranchCode,0) AS BranchCode, ISNULL(BusinessCode,0) AS BusinessCode, ISNULL(WHSECode,0) AS WHSECode " & _
    '                    " FROM	    tblTransactionSetup LEFT JOIN tblTransactionDetails " & _
    '                    " ON		tblTransactionSetup.TransType = tblTransactionDetails.TransType " & _
    '                    " WHERE	    tblTransactionSetup.TransType ='" & ModuleID & "' AND LoanCode = '" & Loan_Code & "'" & _
    '                    " AND		CASE WHEN byBranch = 1 THEN ISNULL(BranchCode,0) ELSE '' END = CASE WHEN byBranch = 1 THEN @Branch ELSE '' END " & _
    '                    " AND       CASE WHEN byBusiness = 1 THEN ISNULL(BusinessCode,0) ELSE '' END = CASE WHEN byBusiness = 1 THEN @Business ELSE '' END " & _
    '                    " AND		CASE WHEN byWHSE = 1 THEN ISNULL(WHSECode,0) ELSE '' END = CASE WHEN byWHSE = 1 THEN @WHSE  ELSE '' END  "
    '            SQL.FlushParams()
    '            SQL.AddParam("@Branch", Branch)
    '            SQL.AddParam("@Business", Business)
    '            SQL.AddParam("@WHSE", WHSE)
    '            SQL.ReadQuery(query)
    '            If SQL.SQLDR.Read Then
    '                Dim digits As Integer = SQL.SQLDR("Digits")
    '                Dim prefix As String = SQL.SQLDR("Prefix")
    '                query = " SELECT    ISNULL(MAX(REPLACE(" & ColumnPK & ",'" & prefix & "','') * 1)+ 1,1) AS TransID  " & _
    '                        " FROM      " & Table & "  " & _
    '                        " WHERE     " & ColumnPK & " LIKE '" & prefix & "%'  AND TransAuto = 1 "
    '                SQL.ReadQuery(query)
    '                If SQL.SQLDR.Read Then
    '                    TransNum = SQL.SQLDR("TransID")
    '                    'For i As Integer = 1 To digits
    '                    '    TransNum = "0" & TransNum
    '                    'Next
    '                    TransNum = prefix & TransNum

    '                    ' CHECK IF GENERATED TRANSNUM ALREADY EXIST
    '                    query = " SELECT    " & ColumnPK & " AS TransID  " & _
    '                            " FROM      " & Table & "  " & _
    '                            " WHERE     " & ColumnPK & " = '" & TransNum & "' "
    '                    SQL.ReadQuery(query)
    '                    If SQL.SQLDR.Read Then
    '                        Dim updateSQL As String
    '                        updateSQL = " UPDATE  " & Table & "  SET TransAuto = 1 WHERE " & ColumnPK & " = '" & TransNum & "' "
    '                        SQL.ExecNonQuery(updateSQL)
    '                        TransNum = "-1"
    '                    End If
    '                End If
    '            End If
    '            If TransNum <> "-1" Then Exit Do
    '        Loop

    '        Return TransNum
    '    Else
    '        Return ""
    '    End If
    'End Function

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("LN_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)

            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbCancel.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
            tsbPrint.Enabled = False
            tsbCopy.Enabled = False
            Try
                If disableEvent = True Then
                    ComputeLoan()
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        ' VALIDATE DATA INPUTS
        If DataValidated() Then
            If TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID(ColumnID, DBTable)
                    If TransAuto Then
                        LoanNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    Else
                        LoanNo = txtTransNum.Text
                    End If
                    txtTransNum.Text = LoanNo
                    SaveLoan()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    LoanNo = txtTransNum.Text
                    LoadLoan(TransID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    If LoanNo = txtTransNum.Text Then
                        LoanNo = txtTransNum.Text
                        UpdateLoan()
                        Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                        LoanNo = txtTransNum.Text
                        LoadLoan(TransID)
                    Else
                        If Not IfExist(txtTransNum.Text) Then
                            LoanNo = txtTransNum.Text
                            UpdateLoan()
                            Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                            LoanNo = txtTransNum.Text
                            LoadLoan(TransID)
                        Else
                            MsgBox("Loan" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Function IfExist(ByVal ID As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblLoan WHERE Loan_No ='" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        ' Toolstrip Buttons
        If TransID = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadLoan(TransID)
            tsbEdit.Enabled = True
            tsbCancel.Enabled = True
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbPrint.Enabled = True
        End If
        tsbSearch.Enabled = True
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
        tsbCopy.Enabled = False
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

#End Region

#Region "CONTROL EVENTS"

#Region "REGENERATE SCHEDULE"

    Private Sub chkdays_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkMon.CheckedChanged, chkTue.CheckedChanged, chkWed.CheckedChanged, _
        chkThurs.CheckedChanged, chkFri.CheckedChanged, chkSat.CheckedChanged, chkSun.CheckedChanged
        If disableEvent = False Then
            Dim chkCount As Integer = 0

            If chkMon.Checked = True Then
                chkCount += 1
            ElseIf chkTue.Checked = True Then
                chkCount += 1
            ElseIf chkWed.Checked = True Then
                chkCount += 1
            ElseIf chkThurs.Checked = True Then
                chkCount += 1
            ElseIf chkFri.Checked = True Then
                chkCount += 1
            ElseIf chkSat.Checked = True Then
                chkCount += 1
            ElseIf chkSun.Checked = True Then
                chkCount += 1
            End If
            If chkCount = 0 Then
                MsgBox("Should have atleast 1 day included for the schedule!", MsgBoxStyle.Exclamation)
                sender.checked = True
            Else
                noOfAmort = 0
                GenerateSchedule(lblMethod.Text)
            End If

        End If
    End Sub

    Private Sub cbWeekly_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbWeekly.SelectedIndexChanged
        If disableEvent = False Then
            GenerateSchedule(lblMethod.Text)
        End If
    End Sub

    Private Sub cbBiMonthly1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbBiMonthly1.SelectedIndexChanged
        If disableEvent = False Then
            cbBiMonthly2.SelectedItem = Days(cbBiMonthly1.SelectedItem).ToString
            GenerateSchedule(lblMethod.Text)
        End If
    End Sub

    Private Sub cbBiMonthly2_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbBiMonthly2.SelectedIndexChanged
        If disableEvent = False Then
            disableEvent = True
            cbBiMonthly1.SelectedItem = Days(cbBiMonthly2.SelectedItem).ToString
            disableEvent = False
            GenerateSchedule(lblMethod.Text)
        End If
    End Sub

    Private Sub txtNoOfAmmort_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtNoOfAmmort.TextChanged
        Try
            If disableEvent = False Then
                If IsNumeric(txtLoanAmount.Text) Or IsNumeric(txtTerms.Text) Then
                    If lblMethod.Text = "Straight Line" Then
                        If IsNumeric(txtNoOfAmmort.Text) AndAlso CDec(txtNoOfAmmort.Text) >= CDec(lblTotalAmount.Text) Then
                            MsgBox("No. of ammortization is too big!")
                            txtNoOfAmmort.Text = Math.Ceiling(CDec(lblTotalAmount.Text))
                            txtNoOfAmmort.SelectAll()
                        Else
                            GenerateSchedule(lblMethod.Text)
                        End If
                    ElseIf lblMethod.Text = "Diminishing Balance" Then
                        If txtNoOfAmmort.Text <> "" Then
                            GenerateSchedule(lblMethod.Text)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub cbMode_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbMode.SelectedIndexChanged
        Try
            If disableEvent = False Then
                If cbMode.SelectedItem = "Semi-Monthly" Then
                    ShowDaysChkbox(False)
                    disableEvent = True
                    If lblMethod.Text = "Diminishing Balance" Then
                        cbBiMonthly1.SelectedItem = "1"
                        cbBiMonthly2.SelectedItem = "31"
                    Else
                        cbBiMonthly1.SelectedIndex = Date.Today.Day - 1
                        cbBiMonthly2.SelectedItem = Days(cbBiMonthly1.SelectedItem).ToString
                    End If
                    cbWeekly.Visible = False
                    cbBiMonthly1.Visible = True
                    cbBiMonthly2.Visible = True
                    lblAnd.Visible = True
                    disableEvent = False
                    If IsNumeric(txtTerms.Text) Then
                        noOfAmort = GetAmortCount(cbMode.SelectedItem, txtTerms.Text)
                        txtNoOfAmmort.Text = noOfAmort
                    End If
                ElseIf cbMode.SelectedItem = "Daily" Then
                    ShowDaysChkbox(True)
                    disableEvent = True
                    cbWeekly.Visible = False
                    cbBiMonthly1.Visible = False
                    cbBiMonthly2.Visible = False
                    lblAnd.Visible = False
                    txtNoOfAmmort.ReadOnly = Not True
                    noOfAmort = 0
                    txtNoOfAmmort.Text = ""
                    disableEvent = False
                ElseIf cbMode.SelectedItem = "Weekly" Then
                    ShowDaysChkbox(False)
                    disableEvent = True
                    cbWeekly.SelectedIndex = Date.Today.DayOfWeek
                    cbWeekly.Visible = True
                    cbBiMonthly1.Visible = False
                    cbBiMonthly2.Visible = False
                    lblAnd.Visible = False
                    txtNoOfAmmort.ReadOnly = Not True
                    noOfAmort = GetAmortCount(cbMode.SelectedItem, txtTerms.Text)
                    txtNoOfAmmort.Text = noOfAmort
                    disableEvent = False
                ElseIf cbMode.SelectedItem = "Monthly" Then
                    ShowDaysChkbox(False)
                    disableEvent = True
                    cbWeekly.Visible = False
                    cbBiMonthly1.Visible = False
                    cbBiMonthly2.Visible = False
                    lblAnd.Visible = False
                    noOfAmort = GetAmortCount(cbMode.SelectedItem, txtTerms.Text)
                    txtNoOfAmmort.Text = noOfAmort
                    disableEvent = False
                End If
                disableEvent = False
                ComputeLoan()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


#End Region

#Region "SUBS"

    Private Sub ShowDaysChkbox(ByVal Value As Boolean)
        disableEvent = True
        chkMon.Checked = True
        chkTue.Checked = True
        chkWed.Checked = True
        chkThurs.Checked = True
        chkFri.Checked = True
        chkSat.Checked = True
        chkSun.Checked = True
        disableEvent = False
        chkMon.Visible = Value
        chkTue.Visible = Value
        chkWed.Visible = Value
        chkThurs.Visible = Value
        chkFri.Visible = Value
        chkSat.Visible = Value
        chkSun.Visible = Value
    End Sub

    Private Sub SaveLoanDeductions(ByVal ID As String)
        Dim deleteSQL, insertSQL As String
        deleteSQL = " DELETE FROM tblLoan_Details WHERE TransID = '" & ID & "' "
        SQL.ExecNonQuery(deleteSQL)

        For Each row As DataGridViewRow In dgvCharges.Rows
            If IsNumeric(row.Cells(2).Value) Then
                Dim account As String = GetAccount(row.Cells(0).Value)
                insertSQL = " INSERT INTO " & _
                            " tblLoan_Details(TransID, Description, Amount, AccountCode, VCECode, RefID, SetupCharges, AmortMethod) " & _
                            " VALUES (@TransID, @Description, @Amount, @AccountCode, @VCECode, @RefID, @SetupCharges, @AmortMethod)"
                SQL.FlushParams()
                SQL.AddParam("@TransID", ID)
                SQL.AddParam("@Description", IIf(row.Cells(1).Value = Nothing, "", row.Cells(1).Value))
                SQL.AddParam("@Amount", CDec(row.Cells(2).Value))
                If row.Cells(0).Value = 10 Then
                    SQL.AddParam("@AccountCode", IIf(memType = "Regular", "3013000", account))
                Else
                    SQL.AddParam("@AccountCode", account)
                End If

                SQL.AddParam("@VCECode", txtMemNo.Text)
                SQL.AddParam("@RefID", row.Cells(0).Value.ToString)
                SQL.AddParam("@AmortMethod", row.Cells(3).Value)
                SQL.AddParam("@SetupCharges", True)
                SQL.ExecNonQuery(insertSQL)
            End If
        Next
        For Each row As DataGridViewRow In dgvOtherDeductions.Rows
            If IsNumeric(row.Cells(1).Value) And row.Cells(3).Value <> "" Then
                insertSQL = " INSERT INTO " & _
                            " tblLoan_Details(TransID, Description, Amount, AccountCode, VCECode, RefID, SetupCharges, AmortMethod) " & _
                            " VALUES (@TransID, @Description, @Amount, @AccountCode, @VCECode, @RefID, @SetupCharges, @AmortMethod)"
                SQL.AddParam("@TransID", ID)
                SQL.AddParam("@Description", IIf(row.Cells(0).Value = Nothing, "", row.Cells(0).Value))
                SQL.AddParam("@Amount", CDec(row.Cells(1).Value))
                SQL.AddParam("@AccountCode", row.Cells(3).Value)
                If Not IsNothing(row.Cells(4).Value) AndAlso row.Cells(4).Value.ToString <> "" Then
                    SQL.AddParam("@VCECode", row.Cells(4).Value)
                Else
                    SQL.AddParam("@VCECode", txtMemNo.Text)
                End If
                SQL.AddParam("@RefID", IIf(IsNothing(row.Cells(RefID.Index).Value), "", row.Cells(RefID.Index).Value))
                SQL.AddParam("@AmortMethod", "Less to Proceeds")
                SQL.AddParam("@SetupCharges", False)
                SQL.ExecNonQuery(insertSQL)
            End If
        Next
    End Sub

    Private Sub SaveAmmortSched(ByVal ID As String)
        If boolExtend = True Then
            Dim insertSQL As String = ""
            insertSQL = " INSERT INTO " & _
                        " tblLoan_ScheduleExtend(AmortNum, TransID, Date, BegBalance, Amort, Principal, Interest, " & _
                        "               Other1, Other2, Other3, Other4, Other5, EndBalance, WhoCreated, DateCreated)   " & _
                        " SELECT AmortNum, TransID, Date, BegBalance, Amort, Principal, Interest, " & _
                        "               Other1, Other2, Other3, Other4, Other5, EndBalance, WhoCreated, DateCreated " & _
                        " FROM tblLoan_Schedule WHERE TransID  = @TransID "
            SQL.FlushParams()
            SQL.CloseCon()
            SQL.AddParam("@TransID", ID)
            SQL.ExecNonQuery(insertSQL)
        End If
        Dim deleteSQL As String
        deleteSQL = " DELETE FROM tblLoan_Schedule WHERE TransID = @TransID "
        SQL.FlushParams()
        SQL.CloseCon()
        SQL.AddParam("@TransID", ID)
        SQL.ExecNonQuery(deleteSQL)
        For Each item As ListViewItem In lvAmmort.Items
            Dim insertSQL As String
            Dim count As Integer = lvAmmort.Columns.Count
            insertSQL = " INSERT INTO " & _
                        " tblLoan_Schedule(AmortNum, TransID, Date, BegBalance, Amort, Principal, Interest, " & _
                        "               Other1, Other2, Other3, Other4, Other5, EndBalance, WhoCreated, DateCreated)   " & _
                        " VALUES       (@AmortNum, @TransID, @Date, @BegBalance, @Amort, @Principal, @Interest, " & _
                        "               @Other1, @Other2, @Other3, @Other4, @Other5, @EndBalance, @WhoCreated, GETDATE()) "
            SQL.FlushParams()
            SQL.CloseCon()
            SQL.AddParam("@AmortNum", item.SubItems(0).Text)
            SQL.AddParam("@TransID", ID)
            SQL.AddParam("@Date", item.SubItems(1).Text)
            SQL.AddParam("@BegBalance", CDec(item.SubItems(2).Text))
            SQL.AddParam("@Principal", CDec(item.SubItems(3).Text))
            SQL.AddParam("@Interest", CDec(item.SubItems(4).Text))
            If lvAmmort.Columns.Count > 7 Then SQL.AddParam("@Other1", CDec(item.SubItems(5).Text)) Else SQL.AddParam("@Other1", 0)
            If lvAmmort.Columns.Count > 8 Then SQL.AddParam("@Other2", CDec(item.SubItems(6).Text)) Else SQL.AddParam("@Other2", 0)
            If lvAmmort.Columns.Count > 9 Then SQL.AddParam("@Other3", CDec(item.SubItems(7).Text)) Else SQL.AddParam("@Other3", 0)
            If lvAmmort.Columns.Count > 10 Then SQL.AddParam("@Other4", CDec(item.SubItems(8).Text)) Else SQL.AddParam("@Other4", 0)
            If lvAmmort.Columns.Count > 11 Then SQL.AddParam("@Other5", CDec(item.SubItems(9).Text)) Else SQL.AddParam("@Other5", 0)
            SQL.AddParam("@Amort", CDec(item.SubItems(lvAmmort.Columns.Count - 2).Text))
            SQL.AddParam("@EndBalance", CDec(item.SubItems(lvAmmort.Columns.Count - 1).Text))
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)
        Next
    End Sub

    Private Sub SaveTitle(ByVal ID As String)
        Dim deleteSQL As String
        deleteSQL = " DELETE FROM tblLoan_Title WHERE TransID = @TransID "
        SQL.FlushParams()
        SQL.AddParam("@TransID", ID)
        SQL.ExecNonQuery(deleteSQL)
        For Each item As ListViewItem In lvTitle.Items
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblLoan_Title(TransID, TitleNo, Name_Collateral, Married_To, Location, Area, " & _
                        "               Classification, Appraised_Value, WhoCreated, DateCreated)   " & _
                        " VALUES       (@TransID, @TitleNo, @Name_Collateral, @Married_To, @Location, @Area, " & _
                        "               @Classification, @Appraised_Value,  @WhoCreated, GETDATE()) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", ID)
            SQL.AddParam("@TitleNo", item.SubItems(1).Text)
            SQL.AddParam("@Name_Collateral", item.SubItems(2).Text)
            SQL.AddParam("@Married_To", item.SubItems(3).Text)
            SQL.AddParam("@Location", item.SubItems(4).Text)
            SQL.AddParam("@Area", CDec(item.SubItems(5).Text))
            SQL.AddParam("@Classification", item.SubItems(6).Text)
            SQL.AddParam("@Appraised_Value", CDec(item.SubItems(7).Text))
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)
        Next
    End Sub
#End Region

    Private Sub btnSearchVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchVCE.Click
        Try
            Dim f As New frmVCE_Search
            f.Type = "Member"
            f.ShowDialog()
            txtMemNo.Text = f.VCECode
            txtMemName.Text = f.VCEName
            memType = f.TIN
            txtVCEType.Text = memType
            f.Dispose()
            LoadVCEData()
            LoadLoanBalance(TransID)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtMemName_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtMemName.KeyDown

        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmVCE_Search
                f.cbFilter.SelectedItem = "VCEName"
                f.Type = "VCEName"
                f.ModType = "VCEName"
                f.txtFilter.Text = txtMemName.Text
                f.ShowDialog()
                txtMemNo.Text = f.VCECode
                txtMemName.Text = f.VCEName
                memType = f.TIN
                f.Dispose()
                LoadVCEData()
            End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtLoanAmount_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtLoanAmount.TextChanged
        Try
            If disableEvent = False Then
                ComputeLoan()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cbLoanType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbLoanType.SelectedIndexChanged
        Try
            If disableEvent = False Then
                LoadLoanData()
                LoadVCELoanData()
                LoadInterestRate()
                ComputeLoan()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtTerms_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtTerms.TextChanged
        Try
            If disableEvent = False Then
                If IsNumeric(txtTerms.Text) Then
                    disableEvent = True
                    txtNoOfAmmort.Text = ""
                    disableEvent = False
                    LoadLoanAmount()
                    ComputeLoan()
                Else
                    lvAmmort.Items.Clear()
                End If

            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub GenerateSchedule(ByVal Type As String)
        Try
            If IsNumeric(txtLoanAmount.Text) Then
                If cbMode.SelectedIndex <> -1 Then
                    addOnAmt = 0
                    Dim terms As Integer
                    Dim temp(20) As Decimal
                    Dim a As Integer = 0
                    Dim index As Integer = 5
                    dtpMaturityDate.Value = DateAdd(DateInterval.Month, CDec(txtTerms.Text), dtpStartDate.Value.Date) ' GET MATURITY DATE BASED ON TERMS AND STARTING DATE

                    ' ADD COLUMNS
                    lvAmmort.Items.Clear()
                    lvAmmort.Columns.Clear()
                    lvAmmort.Columns.Add("No.")
                    lvAmmort.Columns.Add("Date")
                    lvAmmort.Columns.Add("Beg. Bal.")
                    lvAmmort.Columns.Add("Principal")
                    lvAmmort.Columns.Add("Interest")
                    ' IF THERE ARE LOAN CHARGES OTHER THAN INTEREST THAT IS AMORTIZED, ADD COLUMN FOR THAT CHARGES
                    For Each item As DataGridViewRow In dgvCharges.Rows
                        If item.Cells(dgcAmortMethod.Index).Value = "Amortize" Then
                            temp(a) = CDec(item.Cells(dgcChargeAmt.Index).Value)
                            lvAmmort.Columns.Add(item.Cells(dgcChargeDesc.Index).Value.ToString)
                            lvAmmort.Columns(index).Width = 100
                            lvAmmort.Columns(index).TextAlign = HorizontalAlignment.Right
                            index += 1
                            a += 1
                        ElseIf item.Cells(dgcAmortMethod.Index).Value = "Add On" Then
                            addOnAmt += item.Cells(dgcChargeAmt.Index).Value
                        End If
                    Next
                    lvAmmort.Columns.Add("Amortization")
                    lvAmmort.Columns.Add("Rem. Bal.")
                    lvAmmort.Columns(0).Width = 35
                    lvAmmort.Columns(1).Width = 80
                    lvAmmort.Columns(2).Width = 100
                    lvAmmort.Columns(3).Width = 100
                    lvAmmort.Columns(4).Width = 100
                    lvAmmort.Columns(2).TextAlign = HorizontalAlignment.Right
                    lvAmmort.Columns(3).TextAlign = HorizontalAlignment.Right
                    lvAmmort.Columns(4).TextAlign = HorizontalAlignment.Right
                    lvAmmort.Columns(index).TextAlign = HorizontalAlignment.Right
                    lvAmmort.Columns(index + 1).Width = 100
                    lvAmmort.Columns(index).Width = 100
                    lvAmmort.Columns(index + 1).TextAlign = HorizontalAlignment.Right

                    terms = LoadPaymentDate()
                    disableEvent = True
                    txtNoOfAmmort.Text = terms
                    disableEvent = False
                    ' GET AMORTIZATION BREAKDOWN BASED ON AMORT COUNT
                    If lvAmmort.Items.Count > 0 Then
                        If Type = "Straight Line" Then
                            GenerateSchedSL(terms)
                        ElseIf Type = "Straight Line (Disminish A Year)" Then
                            GenerateSchedSLD(terms)
                        ElseIf Type = "Diminishing Balance" Then
                            GenerateSchedDB(terms)
                        ElseIf Type = "Diminishing Balance (Straight Principal)" Then
                            GenerateSchedDBSL(terms)
                        End If
                    End If

                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub GenerateSchedSL(ByVal terms As Integer)
        Dim principalBalance, interestBalance, totalBalance As Double
        Dim principalAmort, interestAmort, amortTotal As Decimal
        Dim a As Integer
        Dim temp(20) As Decimal
        Dim addOnAmort As Decimal = addOnAmt / terms

        ' SET REMAINING BALANCES VARIABLE

        If cbAmortMethod.SelectedItem = "Amortize" Then
            interestBalance = interestAmt
        ElseIf cbAmortMethod.SelectedItem = "Add On" Then
            addOnAmt += interestAmt
            interestBalance = 0
        ElseIf cbAmortMethod.SelectedItem = "Less to Proceeds" Then
            interestBalance = 0
        End If
        principalBalance = CDec(txtLoanAmount.Text) + addOnAmt

        totalBalance = CDec(principalBalance + interestBalance)

        ' SET AMORT. VALUES
        principalAmort = CDbl(Math.Round(((principal + addOnAmt) / terms), 2))
        interestAmort = CDbl(Math.Round((interestAmt / terms), 2))
        amortTotal = 0
        Dim BB, Prin, Int, Amort As Decimal
        For i As Integer = 0 To terms - 1
            BB = principalBalance

            ' IF PRINCIPAL BALANCE IS LESS THAN PRICIPAL AMORT OR  PRINCIPAL BALANCE - PRINCIPAL AMORT IS LESS THAN 1
            If (principalBalance < principalAmort) Or ((principalBalance - principalAmort) < 1) Then
                Prin = principalBalance
                principalBalance -= principalBalance
            Else
                Prin = principalAmort
                principalBalance -= principalAmort
            End If

            ' ADD INTEREST AMOUNT TO LIST VIEW
            ' IF 

            If interestBalance < interestAmort Or ((interestBalance - interestAmort) < 1) Then
                Int = interestBalance
                interestBalance -= interestBalance
            Else
                Int = interestAmort
                interestBalance -= interestAmort
            End If

            ' ADD TO LIST VIEW
            lvAmmort.Items(i).SubItems.Add(CDec(Math.Round(BB, 2)).ToString("N2")) '  <-- Beg Bal
            lvAmmort.Items(i).SubItems.Add(CDec(Math.Round(Prin, 2)).ToString("N2")) '  <-- Principal
            lvAmmort.Items(i).SubItems.Add(CDec(Math.Round(Int, 2)).ToString("N2")) '  <-- Interest
            ' ADD OTHER VALUES TO LIST VIEW
            Amort = 0
            a = 0
            For Each item As DataGridViewRow In dgvCharges.Rows
                If item.Cells(3).Value = "Amortize" Then
                    If temp(a) = 0 Then
                        temp(a) = CDec(item.Cells(2).Value)
                    End If
                    ' -- Others --
                    If temp(a) < CDec(Math.Round(CDec(item.Cells(2).Value) / noOfAmort, 2)) Then
                        lvAmmort.Items(i).SubItems.Add(CDec(temp(a)).ToString("N2"))
                        Amort += CDec(temp(a)).ToString("N2")
                    Else
                        If temp(a) - CDec(Math.Round((CDec(item.Cells(2).Value) / noOfAmort), 2)) < 1 Then
                            lvAmmort.Items(i).SubItems.Add(CDec(temp(a)).ToString("N2"))
                            Amort += CDec(temp(a)).ToString("N2")
                        Else
                            lvAmmort.Items(i).SubItems.Add(CDec(Math.Round(CDec(item.Cells(2).Value) / noOfAmort, 2)).ToString("N2"))
                            temp(a) -= CDec(Math.Round(CDec(item.Cells(2).Value) / noOfAmort, 2))
                            Amort += CDec(Math.Round(CDec(item.Cells(2).Value) / noOfAmort, 2)).ToString("N2")
                        End If
                    End If
                    a += 1
                End If
            Next
            Amort = Prin + Int + Amort
            amortTotal = Amort + amortTotal
            lvAmmort.Items(i).SubItems.Add(CDec(Math.Round(Amort, 2)).ToString("N2")) ' ADD REMAINING BALANCE TO LIST VIEW
            lvAmmort.Items(i).SubItems.Add(CDec(Math.Round(principalBalance, 2)).ToString("N2")) ' ADD REMAINING BALANCE TO LIST VIEW
        Next
        lblTotalAmount.Text = CDec(amortTotal).ToString("N")
        lblAmort.Text = ((CDec(totalBalance)) / CDec(terms)).ToString("N")
        lblAmort.Text = (Math.Ceiling(CDec(lblAmort.Text) * 100) / 100).ToString("N")
    End Sub

    Private Sub GenerateSchedSLD(ByVal terms As Integer)
        Dim principalBalance, interestBalance, totalBalance As Double
        Dim principalAmort, interestAmort, amortTotal As Decimal
        Dim a As Integer
        Dim temp(20) As Decimal
        Dim addOnAmort As Decimal = addOnAmt / terms
        Dim InterestAmount, interestNo As Decimal

        ' SET REMAINING BALANCES VARIABLE

        If cbAmortMethod.SelectedItem = "Amortize" Then
            interestBalance = interestAmt
        ElseIf cbAmortMethod.SelectedItem = "Add On" Then
            addOnAmt += interestAmt
            interestBalance = 0
        ElseIf cbAmortMethod.SelectedItem = "Less to Proceeds" Then
            interestBalance = 0
        End If
        principalBalance = CDec(txtLoanAmount.Text) + addOnAmt

        totalBalance = CDec(principalBalance + interestBalance)

        ' SET AMORT. VALUES
        principalAmort = CDbl(Math.Round(((principal + addOnAmt) / terms), 2))
        interestAmort = CDbl(Math.Round((interestAmt / terms), 2))
        amortTotal = 0
        Dim BB, Prin, Int, Amort As Decimal
        For i As Integer = 0 To terms - 1
            BB = principalBalance

            ' IF PRINCIPAL BALANCE IS LESS THAN PRICIPAL AMORT OR  PRINCIPAL BALANCE - PRINCIPAL AMORT IS LESS THAN 1
            If (principalBalance < principalAmort) Or ((principalBalance - principalAmort) < 1) Then
                Prin = principalBalance
                principalBalance -= principalBalance
            Else
                Prin = principalAmort
                principalBalance -= principalAmort
            End If

            ' ADD INTEREST AMOUNT TO LIST VIEW
            ' IF 
            'If i > 11 Then
            '    If interestBalance < interestAmort Or ((interestBalance - interestAmort) < 1) Then
            '        Int = interestBalance / 2
            '        interestBalance -= interestBalance
            '    Else
            '        Int = interestAmort / 2
            '        interestBalance -= interestAmort
            '    End If
            'Else
            '    If interestBalance < interestAmort Or ((interestBalance - interestAmort) < 1) Then
            '        Int = interestBalance
            '        interestBalance -= interestBalance
            '    Else
            '        Int = interestAmort
            '        interestBalance -= interestAmort
            '    End If
            'End If

            If i Mod 12 = 0 Or i = 0 Then
                Int = Math.Round(BB * (CDec(lblInterest.Text.Replace("%", "")) / 100), 2)
            End If

            ' ADD TO LIST VIEW
            lvAmmort.Items(i).SubItems.Add(CDec(Math.Round(BB, 2)).ToString("N2")) '  <-- Beg Bal
            lvAmmort.Items(i).SubItems.Add(CDec(Math.Round(Prin, 2)).ToString("N2")) '  <-- Principal
            lvAmmort.Items(i).SubItems.Add(CDec(Math.Round(Int, 2)).ToString("N2")) '  <-- Interest
            ' ADD OTHER VALUES TO LIST VIEW
            Amort = 0
            a = 0
            For Each item As DataGridViewRow In dgvCharges.Rows
                If item.Cells(3).Value = "Amortize" Then
                    If temp(a) = 0 Then
                        temp(a) = CDec(item.Cells(2).Value)
                    End If
                    ' -- Others --
                    If temp(a) < CDec(Math.Round(CDec(item.Cells(2).Value) / noOfAmort, 2)) Then
                        lvAmmort.Items(i).SubItems.Add(CDec(temp(a)).ToString("N2"))
                        Amort += CDec(temp(a)).ToString("N2")
                    Else
                        If temp(a) - CDec(Math.Round((CDec(item.Cells(2).Value) / noOfAmort), 2)) < 1 Then
                            lvAmmort.Items(i).SubItems.Add(CDec(temp(a)).ToString("N2"))
                            Amort += CDec(temp(a)).ToString("N2")
                        Else
                            lvAmmort.Items(i).SubItems.Add(CDec(Math.Round(CDec(item.Cells(2).Value) / noOfAmort, 2)).ToString("N2"))
                            temp(a) -= CDec(Math.Round(CDec(item.Cells(2).Value) / noOfAmort, 2))
                            Amort += CDec(Math.Round(CDec(item.Cells(2).Value) / noOfAmort, 2)).ToString("N2")
                        End If
                    End If
                    a += 1
                End If
            Next
            Amort = Prin + Int + Amort
            amortTotal = Amort + amortTotal
            lvAmmort.Items(i).SubItems.Add(CDec(Math.Round(Amort, 2)).ToString("N2")) ' ADD REMAINING BALANCE TO LIST VIEW
            lvAmmort.Items(i).SubItems.Add(CDec(Math.Round(principalBalance, 2)).ToString("N2")) ' ADD REMAINING BALANCE TO LIST VIEW
        Next

        For i As Integer = 0 To lvAmmort.Items.Count - 1
            If Decimal.TryParse(lvAmmort.Items(i).SubItems(4).Text, interestNo) Then
                InterestAmount += interestNo
            End If
        Next

        txtIntAmount.Text = CDec(InterestAmount).ToString("N")
        lblTotalAmount.Text = CDec(amortTotal).ToString("N")
        lblAmort.Text = ((CDec(totalBalance)) / CDec(terms)).ToString("N")
        lblAmort.Text = (Math.Ceiling(CDec(lblAmort.Text) * 100) / 100).ToString("N")
    End Sub

    Private Sub GenerateSchedDB(ByVal terms As Integer)
        Dim principalBalance As Double
        Dim TotalAmount As Double = principal
        Dim addOnAmort As Decimal = addOnAmt / terms
        Dim ammortPI = CDec(lblDimBalAmmort.Text)
        Dim temp_P As Decimal
        temp_P = TotalAmount
        Dim temp(10) As Decimal
        Dim a As Integer = 0
        Dim index As Integer = 6
        Dim interest, principal1, ammort, totalInterest As Decimal
        totalInterest = 0

        Dim f As String
        f = CDec(txtNoOfAmmort.Text) / CDec(txtTerms.Text)
        For i As Integer = 0 To (terms) - 1
            ammort = lblDimBalAmmort.Text
            interest = ((intRate / f) * TotalAmount) * 100
            Dim d As Decimal = interest - Math.Floor(interest)
            If d <> 0 Then
                Dim b = d.ToString.Substring(2, 1)
                If b >= 5 Then
                    interest = Math.Ceiling(interest) / 100
                Else
                    interest = Math.Floor(interest) / 100
                End If
            Else
                interest = interest / 100
            End If
            principal1 = (ammortPI - interest)
            If temp_P < principal1 Then
                principal1 = temp_P
                interest = ammortPI - principal1
            Else
                If temp_P - CDec(principal1) < 1 Then
                    principal1 = temp_P
                    interest = ammortPI - principal1
                Else
                    temp_P -= CDec(principal1)
                End If
            End If

            Dim Others As Decimal
            totalInterest += interest
            If cbAmortMethod.SelectedItem = "Less to Proceeds" Then

                interest = 0
            End If

            principalBalance = TotalAmount - (principal1)
            ' -- Principal Beginning --
            lvAmmort.Items(CInt(i)).SubItems.Add(CDec(TotalAmount + (addOnAmt - (i * addOnAmort))).ToString("N2"))
            ' -- Principal --
            lvAmmort.Items(CInt(i)).SubItems.Add(CDec(principal1 + addOnAmort).ToString("N2"))
            ' -- Interest --
            lvAmmort.Items(CInt(i)).SubItems.Add(CDec(interest).ToString("N2"))

            Dim c As Integer = 0
            For Each item As DataGridViewRow In dgvCharges.Rows
                If item.Cells(3).Value = "Amortize" Then
                    ' -- Others --
                    lvAmmort.Items(CInt(i)).SubItems.Add(CDec(item.Cells(2).Value / terms).ToString("N2"))
                    Others = CDec(item.Cells(2).Value / terms).ToString("N2")
                End If
            Next
            ammort = principal1 + interest + Others
            ' -- Ammortization --
            lvAmmort.Items(CInt(i)).SubItems.Add(CDec(ammort + addOnAmort).ToString("N2"))
            ' -- Balance --
            If Math.Abs(principalBalance) < 1 Then
                lvAmmort.Items(CInt(i)).SubItems.Add(CDec(0).ToString("N2"))
            Else
                lvAmmort.Items(CInt(i)).SubItems.Add(CDec(principalBalance + (addOnAmt - ((i + 1) * addOnAmort))).ToString("N2"))
            End If
            lvAmmort.Items(CInt(i)).SubItems.Add(terms)
            principalBalance = TotalAmount - (principal1)
            TotalAmount = principalBalance
        Next
        For Each item As DataGridViewRow In dgvCharges.Rows
            txtIntAmount.Text = totalInterest.ToString("N2")
        Next

        '
        lblAmort.Text = CDec(ammort + addOnAmort).ToString("N2")
        lblTotalAmount.Text = principal + addOnAmt + totalInterest
    End Sub

    Private Sub GenerateSchedDBSL(ByVal terms As Integer)
        Dim principalBalance As Double
        Dim TotalAmount As Double = principal
        Dim addOnAmort As Decimal = 0
        If addOnAmt > 0 Then
            addOnAmort = addOnAmt / terms
        End If
        Dim ammortPI = CDec(lblDimBalAmmort.Text)
        Dim temp_P As Decimal
        temp_P = TotalAmount
        Dim temp(10) As Decimal
        Dim a As Integer = 0
        Dim index As Integer = 6
        Dim interest, principal1, ammort, totalInterest As Decimal
        totalInterest = 0

        principal1 = Math.Round(CDec(txtLoanAmount.Text) / CDec(txtNoOfAmmort.Text), 0)
        Dim f As String
        f = CDec(txtNoOfAmmort.Text) / CDec(txtTerms.Text)
        For i As Integer = 0 To (terms) - 1
            ammort = lblDimBalAmmort.Text
            interest = ((intRate / f) * TotalAmount) * 100
            Dim d As Decimal = interest - Math.Floor(interest)
            If d <> 0 Then
                Dim b = d.ToString.Substring(2, 1)
                If b >= 5 Then
                    interest = Math.Ceiling(interest) / 100
                Else
                    interest = Math.Floor(interest) / 100
                End If
            Else
                interest = interest / 100
            End If
            interest = Math.Round(interest, 0)
            ammortPI = principal1 + interest
            'principal1 = (ammortPI - interest)
            If i = (terms) - 1 Then
                principal1 = temp_P
            Else
                If temp_P - CDec(principal1) < 1 Then
                    principal1 = temp_P
                    interest = ammortPI - principal1
                Else
                    temp_P -= CDec(principal1)
                End If
            End If

            Dim Others As Decimal
            totalInterest += interest
            If cbAmortMethod.SelectedItem = "Less to Proceeds" Then

                interest = 0
            End If

            principalBalance = TotalAmount - (principal1)
            ' -- Principal Beginning --
            lvAmmort.Items(CInt(i)).SubItems.Add(CDec(TotalAmount + (addOnAmt - (i * addOnAmort))).ToString("N2"))
            ' -- Principal --
            lvAmmort.Items(CInt(i)).SubItems.Add(CDec(principal1 + addOnAmort).ToString("N2"))
            ' -- Interest --
            lvAmmort.Items(CInt(i)).SubItems.Add(CDec(interest).ToString("N2"))

            Dim c As Integer = 0
            For Each item As DataGridViewRow In dgvCharges.Rows
                If item.Cells(3).Value = "Amortize" Then
                    ' -- Others --
                    lvAmmort.Items(CInt(i)).SubItems.Add(CDec(item.Cells(2).Value / terms).ToString("N2"))
                    Others = CDec(item.Cells(2).Value / terms).ToString("N2")
                End If
            Next
            ammort = principal1 + interest + Others
            ' -- Ammortization --
            lvAmmort.Items(CInt(i)).SubItems.Add(CDec(ammort + addOnAmort).ToString("N2"))
            ' -- Balance --
            If Math.Abs(principalBalance) < 1 Then
                lvAmmort.Items(CInt(i)).SubItems.Add(CDec(0).ToString("N2"))
            Else
                lvAmmort.Items(CInt(i)).SubItems.Add(CDec(principalBalance + (addOnAmt - ((i + 1) * addOnAmort))).ToString("N2"))
            End If
            lvAmmort.Items(CInt(i)).SubItems.Add(terms)
            principalBalance = TotalAmount - (principal1)
            TotalAmount = principalBalance
        Next
        For Each item As DataGridViewRow In dgvCharges.Rows
            txtIntAmount.Text = totalInterest.ToString("N2")
        Next
        txtIntAmount.Text = totalInterest.ToString("N2")
        lblAmort.Text = CDec(lblDimBalAmmort.Text).ToString("N2")
        lblTotalAmount.Text = CDec(CDec(txtLoanAmount.Text) + IIf(cbAmortMethod.SelectedItem = "Less to Proceeds", 0, (txtIntAmount.Text))).ToString("N2")
    End Sub

    Private Function LoadPaymentDate() As Integer
        Dim terms As Integer
        Select Case cbMode.SelectedItem
            Case "Lumpsum"
                ' FOR LUMPSUM PAYMENT, NUMBER OF AMORTIZATION IS ALWAYS ONE
                terms = 0
                lvAmmort.Items.Add(terms + 1)
                lvAmmort.Items(lvAmmort.Items.Count - 1).SubItems.Add(CDate(DateAdd(DateInterval.Month, terms, dtpStartDate.Value.Date)).ToString("MM/dd/yyyy"))
                terms += 1
                If terms = CDec(noOfAmort) Then
                    dtpMaturityDate.Value = DateAdd(DateInterval.Month, terms - 1, dtpStartDate.Value.Date)
                End If
            Case "Daily"
                ' FOR DAILY PAYMENT, GET ACTUAL NUMBER OF AMORTIZATION BY ADDING DATE FROM STARTING DATE TO MATURITY DATE 
                Dim a As Integer = 0
                Dim include As Boolean = False
                If IsNumeric(txtNoOfAmmort.Text) AndAlso noOfAmort > 0 Then
                    ' IF NO. OF AMORT IS DEFINED, COMPUTE MATURITY DATE WHEN NO. OF AMORT IS REACHED
                    Do Until terms = CDec(noOfAmort)
                        include = False
                        If DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date).DayOfWeek = DayOfWeek.Monday AndAlso chkMon.Checked = True Then
                            include = True
                        ElseIf DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date).DayOfWeek = DayOfWeek.Tuesday AndAlso chkTue.Checked = True Then
                            include = True
                        ElseIf DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date).DayOfWeek = DayOfWeek.Wednesday AndAlso chkWed.Checked = True Then
                            include = True
                        ElseIf DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date).DayOfWeek = DayOfWeek.Thursday AndAlso chkThurs.Checked = True Then
                            include = True
                        ElseIf DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date).DayOfWeek = DayOfWeek.Friday AndAlso chkFri.Checked = True Then
                            include = True
                        ElseIf DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date).DayOfWeek = DayOfWeek.Saturday AndAlso chkSat.Checked = True Then
                            include = True
                        ElseIf DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date).DayOfWeek = DayOfWeek.Sunday AndAlso chkSun.Checked = True Then
                            include = True
                        End If
                        If include Then
                            lvAmmort.Items.Add(terms + 1) ' ADD PAYMENT NO.
                            lvAmmort.Items(lvAmmort.Items.Count - 1).SubItems.Add(DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date))  ' ADD PAYMENT DATE.
                            terms += 1
                        End If
                        If terms = CDec(txtNoOfAmmort.Text) Then
                            dtpMaturityDate.Value = DateAdd(DateInterval.Day, terms - 1, dtpStartDate.Value.Date)
                        End If
                        a += 1
                    Loop
                Else
                    ' IF NO. OF AMORT IS NOT DEFINED, COMPUTE NO. OF AMORT BASED ON MATURITY DATE
                    a = DateDiff(DateInterval.Day, dtpStartDate.Value.Date, CDate(dtpMaturityDate.Value.Date))

                    For i As Integer = 0 To a - 1
                        include = False
                        ' CHECK IF DAYS IS INCLUDED
                        If DateAdd(DateInterval.Day, i, dtpStartDate.Value.Date).DayOfWeek = DayOfWeek.Monday AndAlso chkMon.Checked = True Then
                            include = True
                        ElseIf DateAdd(DateInterval.Day, i, dtpStartDate.Value.Date).DayOfWeek = DayOfWeek.Tuesday AndAlso chkTue.Checked = True Then
                            include = True
                        ElseIf DateAdd(DateInterval.Day, i, dtpStartDate.Value.Date).DayOfWeek = DayOfWeek.Wednesday AndAlso chkWed.Checked = True Then
                            include = True
                        ElseIf DateAdd(DateInterval.Day, i, dtpStartDate.Value.Date).DayOfWeek = DayOfWeek.Thursday AndAlso chkThurs.Checked = True Then
                            include = True
                        ElseIf DateAdd(DateInterval.Day, i, dtpStartDate.Value.Date).DayOfWeek = DayOfWeek.Friday AndAlso chkFri.Checked = True Then
                            include = True
                        ElseIf DateAdd(DateInterval.Day, i, dtpStartDate.Value.Date).DayOfWeek = DayOfWeek.Saturday AndAlso chkSat.Checked = True Then
                            include = True
                        ElseIf DateAdd(DateInterval.Day, i, dtpStartDate.Value.Date).DayOfWeek = DayOfWeek.Sunday AndAlso chkSun.Checked = True Then
                            include = True
                        End If
                        If include Then
                            lvAmmort.Items.Add(terms + 1)  ' ADD PAYMENT NO.
                            lvAmmort.Items(lvAmmort.Items.Count - 1).SubItems.Add(DateAdd(DateInterval.Day, i, dtpStartDate.Value.Date)) ' ADD PAYMENT DATE.
                            terms += 1
                        End If
                    Next
                    disableEvent = True
                    txtNoOfAmmort.Text = terms
                    noOfAmort = terms
                    If txtNoOfAmmort.Focused = True Then
                        txtNoOfAmmort.SelectAll()
                    End If
                    disableEvent = False
                End If
            Case "Weekly"
                ' FOR WEEKLY PAYMENT, GET ACTUAL NUMBER OF AMORTIZATION BY ADDING SELECTED DAY OF WEEK FROM STARTING DATE TO MATURITY DATE 
                Dim daydiff As Integer
                daydiff = cbWeekly.SelectedIndex - Date.Today.DayOfWeek
                If daydiff < 0 Then
                    daydiff += 7
                End If
                terms = 0
                Dim a As Integer
                If IsNumeric(txtNoOfAmmort.Text) AndAlso txtNoOfAmmort.Text > 0 Then
                    Do Until terms = CDec(txtNoOfAmmort.Text)
                        If DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date).DayOfWeek = cbWeekly.SelectedIndex Then
                            lvAmmort.Items.Add(terms + 1)
                            lvAmmort.Items(lvAmmort.Items.Count - 1).SubItems.Add(DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date))
                            terms += 1
                            If terms = CDec(txtNoOfAmmort.Text) Then
                                dtpMaturityDate.Value = DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date)
                            End If
                        End If
                        a += 1
                    Loop
                Else
                    a = DateDiff(DateInterval.Day, dtpStartDate.Value.Date, CDate(dtpMaturityDate.Value.Date))
                    For i As Integer = 0 To a - 1
                        If DateAdd(DateInterval.Day, i, dtpStartDate.Value.Date).DayOfWeek = cbWeekly.SelectedIndex Then
                            lvAmmort.Items.Add(terms + 1)
                            lvAmmort.Items(lvAmmort.Items.Count - 1).SubItems.Add(DateAdd(DateInterval.Day, i, dtpStartDate.Value.Date))
                            terms += 1
                        End If
                    Next
                    disableEvent = True
                    txtNoOfAmmort.Text = terms
                    If txtNoOfAmmort.Focused = True Then
                        txtNoOfAmmort.SelectAll()
                    End If
                    disableEvent = False
                End If
            Case "Semi-Monthly"
                ' FOR SEMI-MONTHLY PAYMENT, GET ACTUAL NUMBER OF AMORTIZATION BY ADDING SELECTED DAYS OF THE MONTH FROM STARTING DATE TO MATURITY DATE 
                Dim a As Integer

                terms = 0
                If IsNumeric(txtNoOfAmmort.Text) AndAlso txtNoOfAmmort.Text > 0 Then
                    noOfAmort = txtNoOfAmmort.Text
                Else
                    noOfAmort = txtTerms.Text * 2
                End If
                Do Until terms = noOfAmort
                    Dim AddDate As Boolean = False
                    If cbBiMonthly1.SelectedItem = Nothing Or cbBiMonthly2.SelectedItem = Nothing Then
                        disableEvent = True
                        cbBiMonthly1.SelectedItem = 15
                        cbBiMonthly2.SelectedItem = 31
                        disableEvent = False
                    End If

                    If cbBiMonthly1.SelectedItem = 31 Or cbBiMonthly2.SelectedItem = 31 Or cbBiMonthly1.SelectedItem = 30 Or cbBiMonthly2.SelectedItem = 30 Then
                        If DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date).Day = 15 Or DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date).Day = Date.DaysInMonth(DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date).Year, DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date).Month) Then
                            AddDate = True
                        End If
                    Else
                        If DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date).Day = cbBiMonthly1.SelectedItem Or DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date).Day = cbBiMonthly2.SelectedItem Then
                            AddDate = True
                        End If
                    End If
                    If AddDate = True Then
                        lvAmmort.Items.Add(terms + 1)
                        lvAmmort.Items(lvAmmort.Items.Count - 1).SubItems.Add(CDate(DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date)).ToString("MM/dd/yyyy"))
                        terms += 1
                    End If
                    If terms = CDec(noOfAmort) Then
                        dtpMaturityDate.Value = DateAdd(DateInterval.Day, a, dtpStartDate.Value.Date)
                    End If
                    a += 1
                Loop
            Case "Monthly"
                ' FOR MONTHLY PAYMENT, GET ACTUAL NUMBER OF AMORTIZATION BY ADDING SELECTED DAY OF THE MONTH FROM STARTING DATE TO MATURITY DATE 
                terms = 0
                If IsNumeric(txtNoOfAmmort.Text) AndAlso txtNoOfAmmort.Text > 0 Then
                    noOfAmort = txtNoOfAmmort.Text
                Else
                    noOfAmort = txtTerms.Text
                End If
                Do Until terms = CDec(noOfAmort)
                    lvAmmort.Items.Add(terms + 1)
                    lvAmmort.Items(lvAmmort.Items.Count - 1).SubItems.Add(CDate(DateAdd(DateInterval.Month, terms, dtpStartDate.Value.Date)).ToString("MM/dd/yyyy"))
                    terms += 1
                    If terms = CDec(noOfAmort) Then
                        dtpMaturityDate.Value = DateAdd(DateInterval.Month, terms - 1, dtpStartDate.Value.Date)
                    End If
                Loop
        End Select
        Return terms
    End Function
#End Region

#Region "FUNCTIONS"
    Private Function GetLoanCode(Type As String) As Integer
        Dim query As String
        query = " SELECT LoanCode FROM tblLoan_Type WHERE LoanType ='" & Type & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("LoanCode")
        Else
            Return 1
        End If
    End Function

    Private Function DataValidated() As Boolean
        If txtMemNo.Text = "" Then
            Msg("No Member Selected!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf cbLoanType.SelectedIndex = -1 Then
            Msg("Please select Loan Type!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf txtLoanAmount.Text = "" Then
            Msg("Please input Loan Amount!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf chkwCollateral.Checked = True AndAlso lvTitle.Items.Count = 0 Then
            Msg("Please input Title!", MsgBoxStyle.Exclamation)
            TabControl2.SelectedTab = tpTitle
            Return False
        ElseIf TransAuto = False AndAlso IfExist(txtTransNum.Text) And TransID = "" Then
            MsgBox("Loan " & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
            Return False
        Else
            Return True
        End If
    End Function


    Private Function ComputeAmmort(Terms As Integer, Principal As Double, Interest As Double) As Double
        Dim ammort As Double
        disableEvent = True
        txtNoOfAmmort.Text = GetAmortCount(cbMode.SelectedItem, txtTerms.Text)
        disableEvent = False
        factor = CDec(txtNoOfAmmort.Text) / CDec(txtTerms.Text)
        If lblMethod.Text.Contains("Straight Principal") Then
            ammort = (Principal / Terms) + (Principal * Interest)
        Else
            ammort = Principal * (Interest / factor) / (1 - (1 + (Interest / factor)) ^ -(Terms * factor))
        End If
        Return ammort
    End Function

    Private Function Days(ByVal Day As Integer) As Integer
        Dim day2 As Integer
        If Day = 31 Then
            day2 = 15
        ElseIf Day <= 15 Then
            day2 = Day + 15
        Else
            day2 = Day - 15
        End If
        Return day2
    End Function

    Private Function GetAmortCount(ByVal Period As String, Terms As Decimal) As Decimal
        Dim amortCount As Decimal = 0
        If Period = "Monthly" Then
            amortCount = CDec(Terms)
        ElseIf Period = "Semi-Monthly" Then
            amortCount = 2 * CDec(Terms)
        ElseIf Period = "Weekly" Then
            amortCount = 4 * CDec(Terms)
        ElseIf Period = "Daily" Then
            amortCount = 30 * CDec(Terms)
        ElseIf Period = "Lumpsum" Then
            amortCount = 1
        End If
        Return amortCount
    End Function

    Private Function ValidateFormula(Formula As String) As String
        If Formula.Contains("%") Then
            Formula = Formula.Replace("%", "/100.00")
        End If
        If Formula.Contains("MOD") Then
            Formula = Formula.Replace("MOD", "%")
        End If
        If Formula.Contains("#Principal") Then
            Formula = Formula.Replace("#Principal", principal)
        End If
        If Formula.Contains("#Interest") Then
            Formula = Formula.Replace("#Interest", interestAmt)
        End If
        If Formula.Contains("#Intrate") Then
            Formula = Formula.Replace("#Intrate", intRate)
        End If
        If Formula.Contains("#MemberID") Then
            Formula = Formula.Replace("#MemberID", IIf(txtMemNo.Text = "", "NULL", txtMemNo.Text))
        End If
        If Formula.Contains("#NumberOfAmmort") Then
            Formula = Formula.Replace("#NumberOfAmmort", noOfAmort)
        End If
        If Formula.Contains("#WCollateral") Then
            Formula = Formula.Replace("#WCollateral", chkwCollateral.Checked)
        End If
        If Formula.Contains("#Ammort") Then
            Formula = Formula.Replace("#Ammort", Amort)
        End If
        If Formula.Contains("#Mode") Then
            Formula = Formula.Replace("#Mode", cbMode.SelectedItem)
        End If
        If Formula.Contains("[") Then
            Formula = Formula.Replace("[", "CAST(")
            Formula = Formula.Replace("]", " AS Numeric(18,6))")
        End If
        If Formula.Contains("#IF") Then
            Formula = Formula.Replace("#IF", "CASE WHEN")
            Formula = Formula.Replace("ELSEIF", " WHEN")
            Formula = Formula & " END "
        End If
        If Formula.Contains("#T") Then
            Formula = Formula.Replace("#T", txtTerms.Text)
        End If
        If Formula.Contains("#MemberType") Then
            Formula = Formula.Replace("#MemberType", "(SELECT Member_Type FROM tblMember_Master WHERE Member_ID ='" & txtMemNo.Text & "')")
        End If
        If Formula.Contains("#Age") Then
            Formula = Formula.Replace("#Age", "( SELECT CASE WHEN DATEADD(YEAR,DATEDIFF(YEAR,Birth_Date,GETDATE()),Birth_Date) <= GETDATE() " & _
                                        " 		    THEN DATEDIFF(YEAR,Birth_Date,GETDATE())  " & _
                                        " 			ELSE DATEDIFF(YEAR,Birth_Date,GETDATE()) -1 " & _
                                        " 		END AS AGE " & _
                                        " FROM   tblMember_Master WHERE Member_ID ='" & txtMemNo.Text & "' )")
        End If
        If Formula.Contains("#Lumpsum") Then
            If cbMode.SelectedItem = "Lumpsum" Then
                Formula = Formula.Replace("#Lumpsum", "True")
            End If
        End If


        Return Formula
    End Function
#End Region

#Region "SUBS"
    Private Sub LoadSchedule(ByVal LoanID As String)
        Try
            lvAmmort.Columns.Clear()
            lvAmmort.Columns.Add("No.")
            lvAmmort.Columns.Add("Date")
            lvAmmort.Columns(0).Width = 35
            lvAmmort.Columns(1).Width = 80
            lvAmmort.Columns.Add("Beg. Bal.")
            lvAmmort.Columns.Add("Principal")
            lvAmmort.Columns.Add("Interest")
            lvAmmort.Columns(2).Width = 100
            lvAmmort.Columns(3).Width = 100
            lvAmmort.Columns(4).Width = 100
            lvAmmort.Columns(2).TextAlign = HorizontalAlignment.Right
            lvAmmort.Columns(3).TextAlign = HorizontalAlignment.Right
            lvAmmort.Columns(4).TextAlign = HorizontalAlignment.Right
            Dim a As Integer = 0
            Dim index As Integer = 5
            For Each item As DataGridViewRow In dgvCharges.Rows
                If item.Cells(3).Value = "Amortize" Then
                    lvAmmort.Columns.Add(item.Cells(1).Value.ToString)
                    lvAmmort.Columns(index).Width = 100
                    lvAmmort.Columns(index).TextAlign = HorizontalAlignment.Right
                    index += 1
                    a += 1
                End If
            Next
            lvAmmort.Columns.Add("Ammortization")
            lvAmmort.Columns(index).Width = 100
            lvAmmort.Columns(index).TextAlign = HorizontalAlignment.Right
            lvAmmort.Columns.Add("Rem. Bal.")
            lvAmmort.Columns(index + 1).Width = 100
            lvAmmort.Columns(index + 1).TextAlign = HorizontalAlignment.Right
            Dim query As String
            query = " SELECT  AmortNum, Date, BegBalance, Principal, Interest, Other1, Other2, Other3, Other4, Other5, Amort, EndBalance " & _
                    " FROM    tblLoan_Schedule " & _
                    " WHERE   TransID = '" & LoanID & "' "
            SQL.CloseCon()
            SQL.ReadQuery(query)
            lvAmmort.Items.Clear()
            While SQL.SQLDR.Read
                lvAmmort.Items.Add(SQL.SQLDR("AmortNum"))
                With lvAmmort.Items(lvAmmort.Items.Count - 1).SubItems
                    .Add(SQL.SQLDR("Date"))
                    .Add(CDec(SQL.SQLDR("BegBalance")).ToString("N2"))
                    .Add(CDec(SQL.SQLDR("Principal")).ToString("N2"))
                    .Add(CDec(SQL.SQLDR("Interest")).ToString("N2"))
                    For i As Integer = 1 To a
                        .Add(CDec(SQL.SQLDR("Other" & i)).ToString("N2"))
                    Next
                    .Add(CDec(SQL.SQLDR("Amort")).ToString("N2"))
                    .Add(CDec(SQL.SQLDR("EndBalance")).ToString("N2"))
                End With
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadTitle(ByVal LoanID As String)
        Try
            Dim query As String
            query = " SELECT	RecordID, TitleNo, Name_Collateral, Married_To, Location, Area, Classification, Appraised_Value  " & _
                    " FROM	    tblLoan_Title " & _
                    " WHERE     TransID = '" & LoanID & "' " & _
                    " ORDER BY RecordID "
            SQL.CloseCon()
            SQL.ReadQuery(query)
            lvTitle.Items.Clear()
            While SQL.SQLDR.Read
                lvTitle.Items.Add(SQL.SQLDR("RecordID"))
                lvTitle.Items(lvTitle.Items.Count - 1).SubItems.Add(SQL.SQLDR("TitleNo"))
                lvTitle.Items(lvTitle.Items.Count - 1).SubItems.Add(SQL.SQLDR("Name_Collateral"))
                lvTitle.Items(lvTitle.Items.Count - 1).SubItems.Add(SQL.SQLDR("Married_To"))
                lvTitle.Items(lvTitle.Items.Count - 1).SubItems.Add(SQL.SQLDR("Location"))
                lvTitle.Items(lvTitle.Items.Count - 1).SubItems.Add(CDec(SQL.SQLDR("Area")))
                lvTitle.Items(lvTitle.Items.Count - 1).SubItems.Add(SQL.SQLDR("Classification"))
                lvTitle.Items(lvTitle.Items.Count - 1).SubItems.Add(CDec(SQL.SQLDR("Appraised_Value")))
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadLoanDeduction(ByVal Loan_ID As Integer)
        Dim query As String
        query = " SELECT	Description, Amount, AccountTitle, tblLoan_Details.AccountCode, VCEName, tblLoan_Details.VCECode, RefID, AmortMethod " & _
                " FROM      tblLoan_Details LEFT JOIN tblCOA_Master" & _
                " ON        tblLoan_Details.AccountCode = tblCOA_Master.AccountCode " & _
                " LEFT JOIN viewVCE_Master " & _
                " ON        tblLoan_Details.VCECode = viewVCE_Master.VCECode " & _
                " WHERE     TransID = " & Loan_ID & _
                " AND       SetupCharges = 1 ORDER by CAST(RefID AS INT) ASC"
        SQL.CloseCon()
        SQL.ReadQuery(query)
        dgvCharges.Rows.Clear()
        While SQL.SQLDR.Read
            dgvCharges.Rows.Add({SQL.SQLDR("RefID"), SQL.SQLDR("Description").ToString, SQL.SQLDR("Amount"), SQL.SQLDR("AmortMethod").ToString})
        End While
        query = " SELECT	Description, Amount, AccountTitle, tblLoan_Details.AccountCode, VCEName, tblLoan_Details.VCECode, RefID " & _
                " FROM      tblLoan_Details INNER JOIN tblCOA_Master" & _
                " ON        tblLoan_Details.AccountCode = tblCOA_Master.AccountCode " & _
                " LEFT JOIN viewVCE_Master " & _
                " ON        tblLoan_Details.VCECode = viewVCE_Master.VCECode " & _
                " WHERE     TransID = " & Loan_ID & _
                " AND       SetupCharges = 0 "
        SQL.CloseCon()
        SQL.GetQuery(query)
        dgvOtherDeductions.Columns.Clear()
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvOtherDeductions.DataSource = SQL.SQLDS.Tables(0)
        End If
        ComputeProceeds()
    End Sub

    Private Sub SaveLoan()
        Try
            Dim LoanCode As Integer = GetLoanCode(cbLoanType.SelectedItem)
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblLoan(TransID, Loan_No, VCECode, LoanCode, DateLoan, Method, LoanAmount, IntAmount, IntValue, IntMethod, IntAmortMethod, LoanProceeds, LoanPayable, Terms,  " & _
                        "         PaymentMode, PaymentType, PaymentSpecific, DateStart, DateMaturity, AmortAmount, NoOfAmort, Borrower, Status, DR_Ref, wCollateral, ForJV, Notes, WhoCreated, DateRenew, BranchCode, Wishlist, Amount) " & _
                        " VALUES(@TransID, @Loan_No, @VCECode, @LoanCode, @DateLoan, @Method, @LoanAmount, @IntAmount, @IntValue, @IntMethod, @IntAmortMethod, @LoanProceeds, @LoanPayable, @Terms,  " & _
                        "        @PaymentMode, @PaymentType, @PaymentSpecific, @DateStart, @DateMaturity, @AmortAmount,  @NoOfAmort, @Borrower, @Status, @DR_Ref, @wCollateral, @ForJV, @Notes, @WhoCreated, @DateRenew, @BranchCode, @Wishlist, @Amount) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@Loan_No", LoanNo)
            SQL.AddParam("@VCECode", txtMemNo.Text)
            SQL.AddParam("@LoanCode", LoanCode)
            SQL.AddParam("@DateLoan", dtpDocDate.Value)
            SQL.AddParam("@Method", lblMethod.Text)
            If IsNumeric(txtLoanAmount.Text) Then SQL.AddParam("@LoanAmount", CDec(txtLoanAmount.Text)) Else SQL.AddParam("@LoanAmount", 0)
            If IsNumeric(txtIntAmount.Text) Then SQL.AddParam("@IntAmount", CDec(txtIntAmount.Text)) Else SQL.AddParam("@IntAmount", 0)
            SQL.AddParam("@IntValue", intValue)
            SQL.AddParam("@IntMethod", intMethod)
            SQL.AddParam("@IntAmortMethod", cbAmortMethod.SelectedItem)
            If IsNumeric(lblProceeds.Text) Then SQL.AddParam("@LoanProceeds", CDec(lblProceeds.Text)) Else SQL.AddParam("@LoanProceeds", 0)
            If IsNumeric(lblTotalAmount.Text) Then SQL.AddParam("@LoanPayable", CDec(lblTotalAmount.Text)) Else SQL.AddParam("@LoanPayable", 0)
            If IsNumeric(txtTerms.Text) Then SQL.AddParam("@Terms", CDec(txtTerms.Text)) Else SQL.AddParam("@Terms", 0)
            SQL.AddParam("@PaymentMode", cbMode.SelectedItem)
            SQL.AddParam("@PaymentType", cbPaymentType.SelectedItem)
            If cbMode.SelectedItem = "Daily" Then
                SQL.AddParam("@PaymentSpecific", "")
            ElseIf cbMode.SelectedItem = "Weekly" Then
                SQL.AddParam("@PaymentSpecific", cbWeekly.SelectedItem)
            ElseIf cbMode.SelectedItem = "Semi-Monthly" Then
                If cbBiMonthly1.SelectedItem > cbBiMonthly2.SelectedItem Then
                    SQL.AddParam("@PaymentSpecific", cbBiMonthly2.SelectedItem & "-" & cbBiMonthly1.SelectedItem)
                Else
                    SQL.AddParam("@PaymentSpecific", cbBiMonthly1.SelectedItem & "-" & cbBiMonthly2.SelectedItem)
                End If
            Else
                SQL.AddParam("@PaymentSpecific", dtpStartDate.Value.Month.ToString)
            End If
            SQL.AddParam("@DateStart", dtpStartDate.Value.Date)
            SQL.AddParam("@DateMaturity", dtpMaturityDate.Value.Date)
            If IsNumeric(lblAmort.Text) Then SQL.AddParam("@AmortAmount", CDec(lblAmort.Text)) Else SQL.AddParam("@AmortAmount", 0)
            If IsNumeric(txtNoOfAmmort.Text) Then SQL.AddParam("@NoofAmort", CDec(txtNoOfAmmort.Text)) Else SQL.AddParam("@NoofAmort", 0)
            SQL.AddParam("@Borrower", borrower)
            SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.AddParam("@DR_Ref", DR_ID)
            SQL.AddParam("@wCollateral", chkwCollateral.Checked)
            SQL.AddParam("@ForJV", chkForJV.Checked)
            SQL.AddParam("@Notes", txtNotes.Text)
            SQL.AddParam("@BranchCode", BranchCode)
            If boolExtend = True Then
                SQL.AddParam("@DateRenew", dtpRenew.Value.Date)
            Else
                SQL.AddParam("@DateRenew", DBNull.Value)
            End If
            SQL.AddParam("@Wishlist", cbWishlist.SelectedItem)
            SQL.AddParam("@Amount", IIf(IsNumeric(txtAmount.Text), CDec(txtAmount.Text), 0))
            SQL.ExecNonQuery(insertSQL)

            SaveLoanDeductions(TransID)
            SaveAmmortSched(TransID)
            SaveLoanHistory(txtMemNo.Text)
            SaveCoMaker()


            If chkwCollateral.Checked = True Then
                SaveTitle(TransID)
            End If
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "Loan_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub UpdateLoan()
        Try
            Dim LoanCode As Integer = GetLoanCode(cbLoanType.SelectedItem)
            Dim updateSQL As String
            updateSQL = " UPDATE    tblLoan " & _
                        " SET       Loan_No = @Loan_No, VCECode = @VCECode, LoanCode = @LoanCode, DateLoan = @DateLoan, Method = @Method, " & _
                        "           LoanAmount = @LoanAmount, IntValue = @IntValue, IntMethod = @IntMethod,  IntAmortMethod = @IntAmortMethod, " & _
                        "           LoanProceeds = @LoanProceeds, IntAmount = @IntAmount, LoanPayable = @LoanPayable, Terms = @Terms,  " & _
                        "           PaymentMode = @PaymentMode, PaymentType = @PaymentType, PaymentSpecific = @PaymentSpecific, " & _
                        "           DateStart = @DateStart, DateMaturity = @DateMaturity, AmortAmount = @AmortAmount, NoOfAmort = @NoOfAmort, " & _
                        "           Borrower = @Borrower, DR_Ref = @DR_Ref, wCollateral = @wCollateral,  ForJV = @ForJV,  Notes = @Notes, WhoModified = @WhoModified, DateModified = GETDATE(),  DateRenew = @DateRenew, BranchCode = @BranchCode " & _
                        " WHERE     TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@Loan_No", LoanNo)
            SQL.AddParam("@VCECode", txtMemNo.Text)
            SQL.AddParam("@LoanCode", LoanCode)
            SQL.AddParam("@DateLoan", dtpDocDate.Value)
            SQL.AddParam("@Method", lblMethod.Text)
            If IsNumeric(txtLoanAmount.Text) Then SQL.AddParam("@LoanAmount", CDec(txtLoanAmount.Text)) Else SQL.AddParam("@LoanAmount", 0)
            If IsNumeric(txtIntAmount.Text) Then SQL.AddParam("@IntAmount", CDec(txtIntAmount.Text)) Else SQL.AddParam("@IntAmount", 0)
            SQL.AddParam("@IntValue", intValue)
            SQL.AddParam("@IntMethod", intMethod)
            SQL.AddParam("@IntAmortMethod", cbAmortMethod.SelectedItem)
            If IsNumeric(lblProceeds.Text) Then SQL.AddParam("@LoanProceeds", CDec(lblProceeds.Text)) Else SQL.AddParam("@LoanProceeds", 0)
            If IsNumeric(lblTotalAmount.Text) Then SQL.AddParam("@LoanPayable", CDec(lblTotalAmount.Text)) Else SQL.AddParam("@LoanPayable", 0)
            If IsNumeric(txtTerms.Text) Then SQL.AddParam("@Terms", CDec(txtTerms.Text)) Else SQL.AddParam("@Terms", 0)
            SQL.AddParam("@PaymentMode", cbMode.SelectedItem)
            SQL.AddParam("@PaymentType", cbPaymentType.SelectedItem)
            If cbMode.SelectedItem = "Daily" Then
                SQL.AddParam("@PaymentSpecific", "")
            ElseIf cbMode.SelectedItem = "Weekly" Then
                SQL.AddParam("@PaymentSpecific", cbWeekly.SelectedItem)
            ElseIf cbMode.SelectedItem = "Semi-Monthly" Then
                If cbBiMonthly1.SelectedItem > cbBiMonthly2.SelectedItem Then
                    SQL.AddParam("@PaymentSpecific", cbBiMonthly2.SelectedItem & "-" & cbBiMonthly1.SelectedItem)
                Else
                    SQL.AddParam("@PaymentSpecific", cbBiMonthly1.SelectedItem & "-" & cbBiMonthly2.SelectedItem)
                End If
            Else
                SQL.AddParam("@PaymentSpecific", dtpStartDate.Value.Month.ToString)
            End If
            SQL.AddParam("@DateStart", dtpStartDate.Value.Date)
            SQL.AddParam("@DateMaturity", dtpMaturityDate.Value.Date)
            If IsNumeric(lblAmort.Text) Then SQL.AddParam("@AmortAmount", CDec(lblAmort.Text)) Else SQL.AddParam("@AmortAmount", 0)
            If IsNumeric(txtNoOfAmmort.Text) Then SQL.AddParam("@NoofAmort", CDec(txtNoOfAmmort.Text)) Else SQL.AddParam("@NoofAmort", 0)
            SQL.AddParam("@Borrower", borrower)
            SQL.AddParam("@WhoModified", UserID)
            SQL.AddParam("@DR_Ref", DR_ID)
            SQL.AddParam("@wCollateral", chkwCollateral.Checked)
            SQL.AddParam("@ForJV", chkForJV.Checked)
            SQL.AddParam("@Notes", txtNotes.Text)
            SQL.AddParam("@BranchCode", BranchCode)
            If boolExtend = True Then
                SQL.AddParam("@DateRenew", dtpRenew.Value.Date)
            Else
                SQL.AddParam("@DateRenew", DBNull.Value)
            End If
            SQL.ExecNonQuery(updateSQL)

            SaveLoanDeductions(TransID)
            SaveAmmortSched(TransID)
            SaveLoanHistory(txtMemNo.Text)
            SaveCoMaker()

            If chkwCollateral.Checked = True Then
                SaveTitle(TransID)
            End If
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "Loan_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)

        boolExtend = Not Value
        txtMemNo.ReadOnly = Not Value
        txtMemName.ReadOnly = Not Value
        dgvCharges.Enabled = Value
        dgvOtherDeductions.Enabled = Value
        If Value = True Then
            dgvOtherDeductions.Columns.Clear()
            dgvOtherDeductions.Columns.Add("Description", "Description")
            dgvOtherDeductions.Columns.Add("Amount", "Amount")
            dgvOtherDeductions.Columns.Add("Account_Title", "Account Title")
            dgvOtherDeductions.Columns.Add("Account_Code", "Account Code")
            dgvOtherDeductions.Columns.Add("VCECode", "VCECode")
            dgvOtherDeductions.Columns.Add("VCEName", "VCEName")
            dgvOtherDeductions.Columns.Add("RefID", "RefID")
            dgvOtherDeductions.Columns(3).Visible = False
            dgvOtherDeductions.Columns(4).Visible = False
        End If
        btnRestruct.Enabled = Value

        lblHistory.Visible = False
        cbHistory.Visible = False
        Label36.Visible = False
        dtpRenew.Visible = False

        dtpRenew.Enabled = Value
        txtVCEName.Enabled = Value
        txtAmount.Enabled = Value
        cbLoanType.Enabled = Value
        cbWishlist.Enabled = Value
        txtLoanAmount.Enabled = Value
        txtTerms.Enabled = Value
        cbAmortMethod.Enabled = Value
        cbMode.Enabled = Value
        cbWeekly.Enabled = Value
        cbBiMonthly1.Enabled = Value
        cbBiMonthly2.Enabled = Value
        dtpStartDate.Enabled = Value
        cbPaymentType.Enabled = Value
        dgvOtherDeductions.Enabled = Value
        dtpDocDate.Enabled = Value
        btnSearchVCE.Enabled = Value
        chkwCollateral.Enabled = Value
        chkForJV.Enabled = Value
        cmbInterest.Enabled = Value
        txtNotes.Enabled = Value
        If chkwCollateral.Checked = True Then
            grpTitle.Enabled = Value
        Else
            grpTitle.Enabled = False
        End If


        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub



    Private Sub EnableControlNote(ByVal Value As Boolean)
        txtNotes.Enabled = Value
        If Value = True Then
            btnSaveNote.Enabled = True
            btnClose.Enabled = True
            btnEditNote.Enabled = False
        Else
            btnSaveNote.Enabled = False
            btnClose.Enabled = False
            btnEditNote.Enabled = True
        End If
    End Sub

    Private Sub LoadLoanType()
        Dim query As String
        query = " SELECT DISTINCT LoanType FROM tblLoan_Type WHERE Status ='Active' "
        SQL.ReadQuery(query)
        cbLoanType.Items.Clear()
        While SQL.SQLDR.Read
            cbLoanType.Items.Add(SQL.SQLDR("LoanType").ToString)
        End While
    End Sub

    Private Sub LoadShareCapital(ByVal VCECode As String)
        Dim query As String
        If VCECode <> "" Then
            query = " SELECT SUM(Credit) - SUM(Debit) AS Balance FROM View_GL " & _
                    " WHERE AccntCode IN " & _
                    " ( " & _
                    "       SELECT	Coop_PUC_Common FROM tblSystemSetup " & _
                    "    UNION ALL  " & _
                    "       SELECT	Coop_PUC_Preferred  FROM tblSystemSetup " & _
                    " ) AND VCECode = '" & VCECode & "' "
            SQL.ReadQuery(query)
            While SQL.SQLDR.Read
                txtShareCapital.Text = IIf(SQL.SQLDR("Balance").ToString.Length = 0, "0.00", SQL.SQLDR("Balance").ToString)
            End While
        End If
    End Sub

    Private Sub LoadLoanData()
        If cbLoanType.SelectedIndex <> -1 Then
            Loan_Code = ""
            Dim query As String
            query = " SELECT LoanCode, Method, InterestPeriod, ISNULL(Terms,1) AS Terms, PaymentType, CashVoucher, APR_Method, APR_Value, IntAmortMethod FROM tblLoan_Type WHERE LoanType = '" & cbLoanType.SelectedItem & "' AND Status ='Active' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read() Then
                disableEvent = True
                Loan_Code = SQL.SQLDR("LoanCode")
                txtTerms.Text = SQL.SQLDR("Terms")
                lblMethod.Text = SQL.SQLDR("Method").ToString
                cbPaymentType.SelectedItem = SQL.SQLDR("PaymentType").ToString
                Cash_Button = IIf(SQL.SQLDR("CashVoucher").ToString = "True", True, False)
                intMethod = SQL.SQLDR("APR_Method").ToString
                'intValue = SQL.SQLDR("APR_Value")
                strIntValue = SQL.SQLDR("APR_Value")
                intMethod = SQL.SQLDR("APR_Method")
                cbAmortMethod.SelectedItem = SQL.SQLDR("IntAmortMethod").ToString
                If cbPaymentType.SelectedItem = "RUBY PAYROLL SD" Or cbPaymentType.SelectedItem = "SALARY DEDUCTION" Then
                    cbMode.SelectedItem = "Semi-Monthly"
                    txtNoOfAmmort.Text = CDec(txtTerms.Text) * 2
                Else
                    cbMode.SelectedItem = "Monthly"
                    ShowDaysChkbox(False)
                    disableEvent = True
                    cbWeekly.Visible = False
                    cbBiMonthly1.Visible = False
                    cbBiMonthly2.Visible = False
                    lblAnd.Visible = False
                    noOfAmort = GetAmortCount(cbMode.SelectedItem, txtTerms.Text)
                    txtNoOfAmmort.Text = noOfAmort
                    disableEvent = False
                End If
                disableEvent = False
            End If

            'If TransID = "" Then
            '    txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
            'End If
        End If
    End Sub

    Private Sub LoadInterestRate()
        If intMethod = "Percentage" Then
            intRate = intValue / 100.0

            'FOULEN
            If Main_JADE.Label4.Text.ToLower.Contains("coop") Then
                lblInterest.Text = intValue.ToString("N3") & "%"
            Else
                lblInterest.Text = intValue.ToString("N2") & "%"
            End If

            'EDSCO
        ElseIf intMethod = "Amount" Then
            intRate = intValue

            'FOULEN
            If Main_JADE.Label4.Text.ToLower.Contains("coop") Then
                lblInterest.Text = intValue.ToString("N3") & "%"
            Else
                lblInterest.Text = intValue.ToString("N2") & "%"
            End If

        End If
        cmbInterest.Items.Clear()
        If strIntValue.Contains(",") Then
            For i As Integer = 0 To CStr(strIntValue).Split(",").Length - 1
                cmbInterest.Items.Add(CStr(strIntValue).Split(",")(i) & " % Annually")
            Next
            cmbInterest.SelectedIndex = 0
        Else

            'FOULEN
            If Main_JADE.Label4.Text.ToLower.Contains("coop") Then
                cmbInterest.Items.Add(CDec((strIntValue) * 100).ToString("N3") & " % Annually")
            Else
                cmbInterest.Items.Add(CDec((strIntValue) * 100).ToString("N2") & " % Annually")
            End If

            cmbInterest.SelectedIndex = 0
        End If
    End Sub

    Private Sub LoadVCEData() ' LOAD DATA THAT ONLY REQUIRES VCECode PARAMTER
        If txtMemNo.Text <> "" Then
            LoadShareCapital(txtMemNo.Text)
        End If
    End Sub

    Private Sub LoadVCELoanData() ' LOAD DATA THAT  REQUIRES VCECode AND Loan Type PARAMTER
        If txtMemNo.Text <> "" And cbLoanType.SelectedIndex <> -1 Then
            LoadLoanBalance(txtMemNo.Text)
            LoadLoanHistory(txtMemNo.Text)
        End If
    End Sub

    Private Sub ComputeInterest()
        If lblMethod.Text = "Straight Line" Then
            ' STRAIGHT LINE FORMULA  i = P * I * T
            interestAmt = principal * (intRate) * CDec(txtTerms.Text)
        End If
        txtIntAmount.Text = interestAmt.ToString("N2")
    End Sub


    Dim decPercent As Decimal = 0
    Dim decFix As Decimal = 0
    Private Sub ComputeLoanDeductions(ByVal Type As String)
        Dim amount As Decimal
        If IsNumeric(txtLoanAmount.Text) And IsNumeric(txtTerms.Text) Then
            noOfAmort = GetAmortCount(cbMode.SelectedItem, txtTerms.Text)
            decPercent = 0
            decFix = 0
            ' SET TotalAmmort VALUE BASE ON METHOD USED
            If lblMethod.Text = "Straight Line" Then
                totalAmmort = principal + interestAmt
            ElseIf lblMethod.Text = "Diminishing Balance" Then
                totalAmmort = CDec(lblDimBalAmmort.Text) * noOfAmort
            End If

            Dim query As String
            query = "  SELECT		tblLoan_ChargesDefault.ChargeID, tblLoan_ChargesDefault.Description, " & _
                    " 		        tblLoan_Charges.Value, tblLoan_Charges.ValueMethod, tblLoan_Charges.AmortMethod " & _
                    "  FROM         tblLoan_Type INNER JOIN tblLoan_Charges " & _
                    "  ON           tblLoan_Type.LoanCode = tblLoan_Charges.LoanCode " & _
                    "  AND          tblLoan_Charges.Status ='Active' " & _
                    "  INNER JOIN   tblLoan_ChargesDefault " & _
                    "  ON           tblLoan_Charges.ChargeID = tblLoan_ChargesDefault.ChargeID " & _
                    "  WHERE        LoanType = '" & Type & "' AND tblLoan_Type.Status ='Active'  " & _
                    "  ORDER BY     SortNum "
            SQL.CloseCon()
            SQL.GetQuery(query)
            dgvCharges.Rows.Clear()
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                Dim chargeID As Integer
                Dim chargeType, method, value, amortMethod As String


                For Each row As DataRow In SQL.SQLDS.Tables(0).Rows

                    ' SET DATAROW VALUES TO VARIABLES
                    chargeID = row(0)
                    chargeType = row(1)
                    value = row(2)
                    method = row(3)
                    amortMethod = row(4)

                    If method = "Percentage" Then
                        amount = principal * (CDec(value) / 100.0)

                        decPercent = decPercent + (CDec(value))
                    ElseIf method = "Amount" Then
                        amount = CDec(value)
                        decFix = decFix + amount
                    ElseIf method = "Formula" Then
                        Dim formula As String = value
                        Try
                            query = " SELECT " & ValidateFormula(formula).Replace("#ChargeID", chargeID) & " AS Value "
                            SQL.CloseCon()
                            SQL.ReadQuery(query)
                            If SQL.SQLDR.Read Then
                                value = SQL.SQLDR("Value")
                            Else
                                value = 0
                            End If

                            amount = principal * (CDec(value) / 100.0)
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try
                        
                        decPercent = decPercent + value
                    End If
                    'If amortize = True Then
                    '    totalAmmort += amount
                    'End If
                    If amount >= 0 Then
                        dgvCharges.Rows.Add({chargeID, chargeType, amount.ToString("N2"), IIf(DR_ID <> 0, "Add On", amortMethod)})
                    End If
                Next
                Amort = totalAmmort / noOfAmort
                lblTotalAmount.Text = totalAmmort.ToString("N2")
                lblAmort.Text = Amort.ToString("N2")
            End If
        End If
    End Sub

    Private Sub ComputeProceeds()
        If IsNumeric(txtLoanAmount.Text) Then
            proceeds = Convert.ToDouble(txtLoanAmount.Text)

            ' PROCEEDS 
            Dim tempProceed As Decimal = proceeds
            If cbAmortMethod.SelectedItem = "Less to Proceeds" Then
                tempProceed -= CDec(txtIntAmount.Text)
            End If
            If dgvCharges.Rows.Count > 0 Then

                For Each item As DataGridViewRow In dgvCharges.Rows
                    If item.Cells(3).Value = "Less to Proceeds" Then
                        tempProceed -= CDec(item.Cells(2).Value)
                    End If
                Next
            End If
            If dgvOtherDeductions.Rows.Count > 0 Then
                For Each item As DataGridViewRow In dgvOtherDeductions.Rows
                    If Not IsNothing(item.Cells(3).Value) Then
                        tempProceed -= CDec(item.Cells(1).Value)
                    End If
                Next
            End If
            lblProceeds.Text = tempProceed.ToString("N")
        End If
    End Sub

    Private Sub ComputeLoan()
        Dim interest As Decimal = 0
        If IsNumeric(txtTerms.Text) AndAlso CDec(txtTerms.Text) > 0 And IsNumeric(txtLoanAmount.Text) And cbLoanType.SelectedIndex <> -1 Then
            If lblMethod.Text = "Straight Line" Then
                ComputeSL()
            ElseIf lblMethod.Text = "Straight Line (Disminish A Year)" Then
                ComputeSLD()
            ElseIf lblMethod.Text.Contains("Diminishing Balance") Then
                ComputeDB()
            End If

        End If
    End Sub

    Private Sub ComputeSL()
        principal = txtLoanAmount.Text
        interestAmt = principal * (intRate) * CDec(txtTerms.Text)
        txtIntAmount.Text = interestAmt.ToString("N2")
        ComputeLoanDeductions(cbLoanType.Text)
        ComputeProceeds()
        GenerateSchedule(lblMethod.Text)
    End Sub

    Private Sub ComputeSLD()
        principal = txtLoanAmount.Text
        interestAmt = principal * (intRate) * CDec(txtTerms.Text)
        txtIntAmount.Text = interestAmt.ToString("N2")
        ComputeLoanDeductions(cbLoanType.Text)
        ComputeProceeds()
        GenerateSchedule(lblMethod.Text)
    End Sub

    Private Sub ComputeDB()
        lblDimBalAmmort.Text = Math.Round(ComputeAmmort(txtTerms.Text, txtLoanAmount.Text, intRate), 2)
        principal = txtLoanAmount.Text
        ComputeLoanDeductions(cbLoanType.Text)
        ComputeProceeds()
        GenerateSchedule(lblMethod.Text)
    End Sub
#End Region



    Private Function GetLoanID(ByVal LoanType As String, ByVal MemNo As String) As Integer
        Dim query As String
        query = " SELECT Loan_ID FROM LOAN_Header WHERE MemNo ='" & MemNo & "' AND Loan_Type ='" & LoanType & "' AND Status ='Released' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() Then
            Return SQL.SQLDR("Loan_ID")
        Else
            Return 0
        End If
    End Function


    Private Sub LoadLoanBalance(ByVal TransID As String)
        If disableEvent = False Then
            Dim selectSQL As String = " SELECT TOP 1 LoanBalance FROM View_LoanLedger " & _
                                      " WHERE VCECode = '" & txtMemNo.Text & "'  " & _
                                      " ORDER BY No Desc "

            SQL.CloseCon()
            SQL.ReadQuery(selectSQL, 2)
            If SQL.SQLDR2.Read AndAlso Not IsDBNull(SQL.SQLDR2("LoanBalance")) Then
                txtLoanBalance.Text = SQL.SQLDR2("LoanBalance")
            Else
                txtLoanBalance.Text = 0
            End If
        End If
    End Sub


    Private Function HasLoanBalance(ByVal MemNo As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblLoan WHERE MemNo = '" & MemNo & "' AND Status ='Approved' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub LoadLoan(ByVal LoanID As String)
        Try
            cbLoanType.Enabled = True
            boolExtend = False
            strNote = ""
            Dim query As String
            query = " SELECT  TransID, Loan_No, View_LoanReleased.VCECode, VCEName, LoanType, DateLoan, View_LoanReleased.Method, LoanAmount, IntValue, IntMethod, View_LoanReleased.IntAmortMethod, " & _
                    "         ISNULL(IntAmount,0) AS IntAmount, LoanProceeds, View_LoanReleased.Terms, PaymentMode, View_LoanReleased.PaymentType, PaymentSpecific, DateStart, " & _
                    "         DateMaturity, AmortAmount, View_LoanReleased.Status, DateRelease, LoanPayable, NoOfAmort, Restructured, CV_Ref, JV_Ref, Borrower, ISNULL(DR_Ref,0) as DR_Ref, wCollateral, ISNULL(ForJV,0) as ForJV, ISNULL(Notes, '') AS Notes, TransNum, RefType, View_LoanReleased.DateRenew, Wishlist, ISNULL(Amount,0) AS Amount " & _
                    " FROM    View_LoanReleased INNER JOIN viewVCE_Master " & _
                    " ON      View_LoanReleased.VCECode = viewVCE_Master.VCECode " & _
                    " INNER JOIN tblLoan_Type " & _
                    " ON      View_LoanReleased.LoanCode = tblLoan_Type.LoanCode" & _
                    " WHERE   TransID = " & LoanID & ""
            SQL.CloseCon()
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                disableEvent = True
                TransID = SQL.SQLDR("TransID").ToString
                LoanNo = SQL.SQLDR("Loan_No").ToString
                txtTransNum.Text = LoanNo
                txtMemNo.Text = SQL.SQLDR("VCECode").ToString
                txtMemName.Text = SQL.SQLDR("VCEName").ToString
                cbLoanType.SelectedItem = SQL.SQLDR("LoanType").ToString
                dtpDocDate.Value = SQL.SQLDR("DateLoan")
                txtLoanAmount.Text = SQL.SQLDR("LoanAmount")
                txtIntAmount.Text = SQL.SQLDR("IntAmount")
                lblProceeds.Text = SQL.SQLDR("LoanProceeds")
                txtTerms.Text = SQL.SQLDR("Terms")
                lblAmort.Text = SQL.SQLDR("AmortAmount")
                lblDimBalAmmort.Text = SQL.SQLDR("AmortAmount")
                Label22.Text = SQL.SQLDR("RefType") & " Ref :"
                txtCVNo.Text = SQL.SQLDR("TransNum").ToString

                If Not cbMode.Items.Contains(SQL.SQLDR("PaymentMode")) Then
                    cbMode.Items.Add(SQL.SQLDR("PaymentMode").ToString)
                End If
                If SQL.SQLDR("Borrower") = 1 Then
                    rbNewBorrower.Checked = True
                ElseIf SQL.SQLDR("Borrower") = 2 Then
                    rbRepeatBorrower.Checked = True
                ElseIf SQL.SQLDR("Borrower") = 3 Then
                    rbReturningBorrower.Checked = True
                End If
                intValue = SQL.SQLDR("intValue")
                intMethod = SQL.SQLDR("IntMethod")
                'strNote = SQL.SQLDR("Note").ToString

                'FOULEN
                If Main_JADE.Label4.Text.ToLower.Contains("coop") Then
                    strIntValue = Math.Round(CDec(SQL.SQLDR("intValue")) * 12, 4)
                Else
                    strIntValue = SQL.SQLDR("intValue") * 12
                End If

                'EDSCO


                cbAmortMethod.SelectedItem = SQL.SQLDR("IntAmortMethod").ToString
                lblMethod.Text = SQL.SQLDR("Method").ToString
                cbMode.SelectedItem = SQL.SQLDR("PaymentMode").ToString
                txtNoOfAmmort.Text = SQL.SQLDR("NoOfAmort").ToString
                cbPaymentType.SelectedItem = SQL.SQLDR("PaymentType").ToString
                If cbMode.SelectedItem = "Semi-Monthly" Then
                    Dim delimiterIndex As String
                    delimiterIndex = SQL.SQLDR("PaymentSpecific").ToString.IndexOf("-")
                    cbBiMonthly1.SelectedItem = SQL.SQLDR("PaymentSpecific").ToString.Substring(0, delimiterIndex)
                    cbBiMonthly2.SelectedItem = SQL.SQLDR("PaymentSpecific").ToString.Remove(0, delimiterIndex)
                    cbWeekly.Visible = False
                    cbBiMonthly1.Visible = True
                    cbBiMonthly2.Visible = True
                ElseIf cbMode.SelectedItem = "Weekly" Then
                    cbWeekly.SelectedItem = SQL.SQLDR("PaymentSpecific")
                    cbWeekly.Visible = True
                    cbBiMonthly1.Visible = False
                    cbBiMonthly2.Visible = False
                Else
                    cbWeekly.Visible = False
                    cbBiMonthly1.Visible = False
                    cbBiMonthly2.Visible = False
                End If
                dtpStartDate.Text = SQL.SQLDR("DateStart").ToString
                dtpMaturityDate.Text = SQL.SQLDR("DateMaturity").ToString
                txtStatus.Text = SQL.SQLDR("Status").ToString
                'Approved Loan
                If Not IsDBNull(SQL.SQLDR("DateRelease")) Then
                    dtpReleased.Value = SQL.SQLDR("DateRelease")
                End If

                If Not IsDBNull(SQL.SQLDR("DateRenew")) Then
                    dtpRenew.Value = SQL.SQLDR("DateRenew")
                    dtpRenew.Visible = True
                    Label36.Visible = True
                End If

                lblTotalAmount.Text = IIf(IsDBNull(SQL.SQLDR("LoanPayable")), 0, SQL.SQLDR("LoanPayable"))
                DR_ID = SQL.SQLDR("DR_Ref")
                chkForJV.Checked = IIf(SQL.SQLDR("ForJV").ToString = "True", True, False)
                txtNotes.Text = SQL.SQLDR("Notes").ToString
                chkwCollateral.Checked = IIf(SQL.SQLDR("wCollateral").ToString = "True", True, False)
                cbWishlist.SelectedItem = SQL.SQLDR("Wishlist").ToString
                txtAmount.Text = SQL.SQLDR("Amount").ToString

                txtDRNo.Text = LoadDRNo(DR_ID)
                memType = LoadMemType(txtMemNo.Text)
                disableEvent = False
                LoadInterestRate()
                LoadCoMaker(TransID)
                LoadLoanDeduction(TransID)
                LoadSchedule(TransID)
                LoadLoanPayment(txtTransNum.Text, CDec(txtLoanAmount.Text), IIf(cbAmortMethod.SelectedItem = "Less to Proceeds", 0, CDec(txtIntAmount.Text)))
                LoadLoanBalance(TransID)

                If chkwCollateral.Checked = True Then
                    LoadTitle(TransID)
                Else
                    txtTitleNo.Clear()
                    txtNameCollateral.Clear()
                    txtMarriedTo.Clear()
                    txtLocation.Clear()
                    txtArea.Clear()
                    cbClassification.SelectedIndex = -1
                    txtAppraisedValue.Clear()
                    lvTitle.Items.Clear()
                End If

                If lvAmmort.Items.Count = 0 Then
                    ComputeLoan()
                    SaveAmmortSched(TransID)
                End If
                ' TOOLSTRIP BUTTONS
                If txtStatus.Text = "Cancelled" Then
                    tsbEdit.Enabled = False
                    tsbCancel.Enabled = False
                    tsbExtend.Enabled = False
                ElseIf txtStatus.Text = "Closed" Then
                    tsbEdit.Enabled = False
                    tsbCancel.Enabled = False
                    tsbExtend.Enabled = False
                ElseIf txtStatus.Text = "Approved" Then
                    tsbEdit.Enabled = False
                    tsbCancel.Enabled = False
                    tsbExtend.Enabled = False
                ElseIf txtStatus.Text = "Released" Then
                    tsbEdit.Enabled = False
                    tsbCancel.Enabled = False
                    tsbExtend.Enabled = True
                Else
                    tsbEdit.Enabled = True
                    tsbCancel.Enabled = True
                    tsbExtend.Enabled = False
                End If
                tsbPrint.Enabled = True
                tsbClose.Enabled = False
                tsbPrevious.Enabled = True
                tsbNext.Enabled = True
                tsbPrint.Enabled = True
                tsbSave.Enabled = False
                tsbNew.Enabled = True
                tsbSearch.Enabled = True
                tsbExit.Enabled = True
                tsbCopy.Enabled = False
                EnableControl(False)
            Else
                ClearText()
            End If
            LoadHistory(TransID)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub LoadHistory(ByVal LoanID As String)
        Dim selectSQL As String = " SELECT TransID, CONVERT(VARCHAR, DateCreated, 120) AS Date FROM tblLoan_ScheduleExtend WHERE TransID = @TransID " & vbCrLf & _
                                  " GROUP BY TransID, CONVERT(VARCHAR, DateCreated, 120) "
        SQL.FlushParams()
        SQL.CloseCon()
        SQL.AddParam("@TransID", LoanID)
        SQL.ReadQuery(selectSQL)
        lblHistory.Visible = False
        cbHistory.Visible = False
        Label36.Visible = False
        dtpRenew.Visible = False
        cbHistory.Items.Clear()
        While SQL.SQLDR.Read
            If lblHistory.Visible = False Then
                lblHistory.Visible = True
                cbHistory.Visible = True
                Label36.Visible = True
                dtpRenew.Visible = True
            End If
            cbHistory.Items.Add(SQL.SQLDR("Date").ToString)
        End While
        If lblHistory.Visible = True Then
            selectSQL = " SELECT TransID, CONVERT(VARCHAR, DateCreated, 120) AS Date FROM tblLoan_Schedule WHERE TransID = @TransID " & vbCrLf & _
                        " GROUP BY TransID, CONVERT(VARCHAR, DateCreated, 120) "
            SQL.FlushParams()
            SQL.CloseCon()
            SQL.AddParam("@TransID", LoanID)
            SQL.ReadQuery(selectSQL)
            While SQL.SQLDR.Read
                cbHistory.Items.Add(SQL.SQLDR("Date").ToString)
            End While

        End If
    End Sub

    Private Function GETCVNo(ByVal LoanID As String) As String
        Dim query As String
        query = " SELECT CVTransId FROM ACV WHERE Ref_ID ='" & LoanID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read AndAlso Not IsDBNull(SQL.SQLDR("CVTransID")) Then
            Return SQL.SQLDR("CVTransID")
        Else
            Return ""

        End If
    End Function

    Private Sub LoadLoanPayment(ByVal LoanID As String, ByVal Principal As Decimal, ByVal Interest As String)
        Dim query As String
        Dim counter As Integer = 0

        query = " SELECT AppDate, RefType, RefTransID, ISNULL(LoanPayment,0) as LoanPayment, ISNULL(IntPayment,0) as IntPayment, " & _
                " ISNULL(LoanBalance,0) as LoanBalance " & _
                " FROM View_LoanLedger WHERE Loan_No = '" & LoanID & "' AND RefType = 'OR'"
        SQL.CloseCon()
        SQL.ReadQuery(query)
        lvPayment.Items.Clear()
        While SQL.SQLDR.Read()
            counter += 1
            lvPayment.Items.Add(SQL.SQLDR("AppDate"))
            lvPayment.Items(lvPayment.Items.Count - 1).SubItems.Add(SQL.SQLDR("RefType") & ":" & SQL.SQLDR("RefTransID"))
            lvPayment.Items(lvPayment.Items.Count - 1).SubItems.Add(SQL.SQLDR("LoanPayment"))
            lvPayment.Items(lvPayment.Items.Count - 1).SubItems.Add(SQL.SQLDR("IntPayment"))
            lvPayment.Items(lvPayment.Items.Count - 1).SubItems.Add(SQL.SQLDR("LoanBalance"))
            lvPayment.Items(lvPayment.Items.Count - 1).SubItems.Add(counter)
        End While
    End Sub



    Private Function GetAccount(ByVal Charge_ID As Integer) As String
        Dim query As String
        query = " SELECT  tblLoan_Charges.DefaultAccount " & _
                " FROM    tblLoan_Type INNER JOIN tblLoan_Charges " & _
                " ON      tblLoan_Type.LoanCode = tblLoan_Charges.LoanCode " & _
                " WHERE   LoanType ='" & cbLoanType.SelectedItem & "' AND ChargeID = " & Charge_ID & " "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("DefaultAccount").ToString
        Else
            Return ""
        End If
    End Function



    Private Sub txtLoanAmount_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtLoanAmount.KeyPress, txtNoOfAmmort.KeyPress, txtTerms.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso e.KeyChar <> "." Then
            e.Handled = True
        Else
            If e.KeyChar = "." AndAlso sender.Text.Contains(".") Then
                e.Handled = True
            End If
            If sender.name = txtTerms.Name Then
                If sender.text.contains(".") Then
                    If txtTerms.SelectionStart - txtTerms.Text.LastIndexOf(".") = 1 Then
                        If e.KeyChar <> "5" AndAlso e.KeyChar <> "0" AndAlso Not Char.IsControl(e.KeyChar) Then
                            e.Handled = True
                        End If
                    ElseIf txtTerms.SelectionStart - txtTerms.Text.LastIndexOf(".") > 1 Then
                        If e.KeyChar <> "0" AndAlso Not Char.IsControl(e.KeyChar) Then
                            e.Handled = True
                        End If

                    End If


                End If
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvOtherDeductions.CellEndEdit
        Try
            If e.ColumnIndex = 2 Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", dgvOtherDeductions.Item(e.ColumnIndex, e.RowIndex).Value.ToString)
                'DataGridView1.Rows.RemoveAt(e.RowIndex) 'delete active row
                dgvOtherDeductions.Item(3, e.RowIndex).Value = f.accountcode
                dgvOtherDeductions.Item(2, e.RowIndex).Value = f.accttile
                'If IsLoanBalance(f.accountcode) Then
                '    Dim query As String
                '    query = " SELECT  Loan_ID, Loan_Type, MemNo, MemName, Principal_Balance, Interest_Balance, Loan_AccountCode, APR_AccountCode, InterestTitle " & _
                '            " FROM    Loan_Balance WHERE MemNo ='" & txtMemNo.Text & "' " & _
                '            " AND     Loan_AccountCode ='" & f.accountcode & "' "
                '    SQL.ReadQuery(query)
                '    If SQL.SQLDR.Read Then
                '        dgvOtherDeductions.Item(1, e.RowIndex).Value = SQL.SQLDR("Principal_Balance")
                '        dgvOtherDeductions.Item(4, e.RowIndex).Value = SQL.SQLDR("MemName")
                '        dgvOtherDeductions.Item(5, e.RowIndex).Value = SQL.SQLDR("MemNo")
                '        dgvOtherDeductions.Item(0, e.RowIndex).Value = SQL.SQLDR("Loan_Type")
                '        dgvOtherDeductions.Item(6, e.RowIndex).Value = SQL.SQLDR("Loan_ID")

                '        If SQL.SQLDR("Interest_Balance") > 0 Then
                '            dgvOtherDeductions.Rows.Add(SQL.SQLDR("Loan_Type") + " Interest", SQL.SQLDR("Interest_Balance"), SQL.SQLDR("InterestTitle"), SQL.SQLDR("APR_AccountCode"), SQL.SQLDR("MemName"), SQL.SQLDR("MemNo"), SQL.SQLDR("Loan_ID"))
                '        End If
                '    End If
                'End If
                Try
                    ComputeProceeds()
                Catch ex As Exception
                End Try
            ElseIf e.ColumnIndex = 1 Then
                Try
                    If Not dgvOtherDeductions.Item(3, e.RowIndex).Value = Nothing AndAlso dgvOtherDeductions.Item(3, e.RowIndex).Value.ToString <> "" Then
                        LoadLoanAmount()
                        ComputeProceeds()
                    End If
                Catch ex As Exception
                    ComputeProceeds()
                End Try

            ElseIf e.ColumnIndex = 5 Then
                'Dim f As New frmVCE_Search
                'f.VCEName = dgvOtherDeductions.Item(4, e.RowIndex).Value.ToString
                'f.ShowDialog()
                'dgvOtherDeductions.Item(5, e.RowIndex).Value = f.VCECode
                'dgvOtherDeductions.Item(4, e.RowIndex).Value = f.VCEName
                'f.Dispose()


                Dim f As New frmVCE_Search
                f.txtFilter.Text = dgvOtherDeductions.Item(e.ColumnIndex, e.RowIndex).Value.ToString

                f.ShowDialog()
                dgvOtherDeductions.Item(4, e.RowIndex).Value = f.VCECode
                dgvOtherDeductions.Item(5, e.RowIndex).Value = f.VCEName
                f.Dispose()
            End If
        Catch ex As Exception

            MsgBox(ex.Message)
        End Try

    End Sub

    Private Function IsLoanBalance(ByVal AccountCode As String) As Boolean
        Dim query As String
        query = "SELECT * FROM tblLoan_Type WHERE Status ='Active' AND LoanAccount ='" & AccountCode & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub dtpStartDate_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpStartDate.ValueChanged
        If disableEvent = False Then
            If cbMode.SelectedItem = "Semi-Monthly" Then
                factor = 0.5
            Else
                factor = 1
            End If
            GenerateSchedule(lblMethod.Text)
        End If
    End Sub


    Private Sub btnSaveSched_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveSched.Click
        SaveAmmortSched(TransID)
        btnSaveSched.Visible = False
    End Sub



    Private Sub SaveAmmortSchedMonthly(ByVal LoanID As String)
        Dim deleteSQL As String
        deleteSQL = " DELETE FROM tblLoan_Schedule WHERE Loan_ID = @Loan_ID "
        SQL.FlushParams()
        SQL.AddParam("@Loan_ID", LoanID)
        SQL.ExecNonQuery(deleteSQL)
        For Each item As ListViewItem In lvAmmort.Items
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblLoan_Schedule_Daily(Ammort_No, Loan_ID, Date, Starting_Balance, Ammort, Principal, Interest, " & _
                        "               Other1, Other2, Other3, Other4, Other5, Remaining_Balance)   " & _
                        " VALUES       (@Ammort_No, @Loan_ID, @Date, @Starting_Balance, @Ammort, @Principal, @Interest, " & _
                        "               @Other1, @Other2, @Other3, @Other4, @Other5, @Remaining_Balance) "
            SQL.FlushParams()
            SQL.AddParam("@Ammort_No", item.SubItems(0).Text)
            SQL.AddParam("@Loan_ID", LoanID)
            SQL.AddParam("@Date", item.SubItems(1).Text)
            SQL.AddParam("@Starting_Balance", CDec(item.SubItems(2).Text))
            SQL.AddParam("@Ammort", CDec(item.SubItems(3).Text))
            SQL.AddParam("@Principal", CDec(item.SubItems(4).Text))
            SQL.AddParam("@Interest", CDec(item.SubItems(5).Text))
            If lvAmmort.Columns.Count > 7 Then SQL.AddParam("@Other1", CDec(item.SubItems(6).Text)) Else SQL.AddParam("@Other1", 0)
            If lvAmmort.Columns.Count > 8 Then SQL.AddParam("@Other2", CDec(item.SubItems(7).Text)) Else SQL.AddParam("@Other2", 0)
            If lvAmmort.Columns.Count > 9 Then SQL.AddParam("@Other3", CDec(item.SubItems(8).Text)) Else SQL.AddParam("@Other3", 0)
            If lvAmmort.Columns.Count > 10 Then SQL.AddParam("@Other4", CDec(item.SubItems(9).Text)) Else SQL.AddParam("@Other4", 0)
            If lvAmmort.Columns.Count > 11 Then SQL.AddParam("@Other5", CDec(item.SubItems(10).Text)) Else SQL.AddParam("@Other5", 0)
            SQL.AddParam("@Remaining_Balance", CDec(item.SubItems(lvAmmort.Columns.Count - 1).Text))
            SQL.ExecNonQuery(insertSQL)
        Next

    End Sub




    Private Sub btnRestruct_Click(sender As System.Object, e As System.EventArgs) Handles btnRestruct.Click
        'btnSave.Text = "Save"
        'btnSave.Enabled = True
        'restruct = True
        'txtLoanAmount.Text = txtLoanBalance.Text
        'cbLoanType.Text = "Restructure Loan"
        'cbLoanType.Enabled = False
        dgvOtherDeductions.Columns.Clear()
        dgvOtherDeductions.Columns.Add("Description", "Description")
        dgvOtherDeductions.Columns.Add("Amount", "Amount")
        dgvOtherDeductions.Columns.Add("Account_Title", "Account Title")
        dgvOtherDeductions.Columns.Add("Account_Code", "Account Code")
        dgvOtherDeductions.Columns.Add("VCECode", "VCECode")
        dgvOtherDeductions.Columns.Add("VCEName", "VCEName")
        'EnableText(True)

        If CDec(txtLoanBalance.Text) > 0 Then
            Dim rowInd As Integer
            Dim rowcount As Integer = dgvOtherDeductions.Rows.Count
            Dim NewRow As Boolean = True
            For Each row As DataGridViewRow In dgvOtherDeductions.Rows
                If row.Cells(1).Value = Nothing Then
                    rowInd = row.Index
                    NewRow = False
                    Exit For
                End If
            Next
            If NewRow = True Then
                dgvOtherDeductions.Rows.Add()
                rowInd = rowcount
            End If
            Dim rowIndex As Integer = rowInd
            dgvOtherDeductions.Rows.Add()
            dgvOtherDeductions.Item(1, rowInd).Value = CDec(txtLoanBalance.Text)
            dgvOtherDeductions.Item(2, rowInd).Value = GetAccntTitle(GetDeffaultAccount(cbLoanType.SelectedItem))
            dgvOtherDeductions.Item(3, rowInd).Value = GetDeffaultAccount(cbLoanType.SelectedItem)
            ComputeProceeds()
        End If
    End Sub

    Private Sub txtInterest_TextChanged(sender As System.Object, e As System.EventArgs)
        If disableEvent = False Then

        End If
    End Sub

    Private Sub Button1_Click_2(sender As System.Object, e As System.EventArgs)
        Try
            Dim f As New frmSelectRecipients
            f.ShowDialog()
            txtVCECode.Text = f.MemNo
            txtVCEName.Text = f.MemName
            f.Close()
            f.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub lvCoMaker_Click(sender As Object, e As System.EventArgs) Handles lvCoMaker.Click
        If lvCoMaker.SelectedItems.Count > 0 Then
            txtVCECode.Text = lvCoMaker.Items(lvCoMaker.SelectedItems(0).Index).SubItems(chMemNo.Index).Text()
            txtVCEName.Text = lvCoMaker.Items(lvCoMaker.SelectedItems(0).Index).SubItems(chMemName.Index).Text()
        End If
    End Sub

    Private Sub lvCoMaker_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lvCoMaker.MouseClick
        If lvCoMaker.Items.Count > 0 Then
            If e.Button = MouseButtons.Right Then
                If lvCoMaker.FocusedItem.Bounds.Contains(e.Location) = True Then
                    ab = lvCoMaker.SelectedIndices(0).ToString
                    ContextMenuStrip1.Show(Cursor.Position)
                End If
            End If
        End If
    End Sub

    Private Sub lvCoMaker_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lvCoMaker.SelectedIndexChanged

    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Dim deleteSQL As String
        If lvCoMaker.SelectedItems(0).SubItems(chLoanID.Index).Text <> "" Then
            deleteSQL = "Delete from  tblLoan_CoMaker where MemNo = @MemNo and LoanID ='" & lvCoMaker.SelectedItems(0).SubItems(chLoanID.Index).Text & "'  "
            SQL.AddParam("@MemNo", txtVCECode.Text)
            SQL.ExecNonQuery(deleteSQL)
        End If
        Dim listItem As ListViewItem
        listItem = lvCoMaker.Items(ab)
        lvCoMaker.Items.Remove(listItem)
    End Sub

    Public Sub SaveCoMaker()
        Dim deleteSQL As String
        deleteSQL = "Delete from tblLoan_CoMaker where TransID = '" & TransID & "' "
        SQL.ExecNonQuery(deleteSQL)
        For Each item As ListViewItem In lvCoMaker.Items
            Dim insertSQL
            insertSQL = " INSERT INTO " & _
                           " tblLoan_CoMaker(TransID, VCECode, WhoCreated)" & _
                           " VALUES('" & TransID & "','" & item.SubItems(2).Text & "', '" & UserID & "')"
            SQL.ExecNonQuery(insertSQL)
        Next

    End Sub

    Private Sub txtVCEName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmVCE_Search
                f.VCEName = txtVCEName.Text
                f.ShowDialog()
                txtVCECode.Text = f.VCECode
                txtVCEName.Text = f.VCEName
                f.Dispose()

                If txtVCECode.Text <> "" Then
                    Dim lv As New ListViewItem("")
                    lv.SubItems.Add(TransID)
                    lv.SubItems.Add(txtVCECode.Text)
                    lv.SubItems.Add(txtVCEName.Text)
                    lvCoMaker.Items.Add(lv)
                End If

                txtVCECode.Text = ""
                txtVCEName.Text = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged

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

    Private Sub Button1_Click_3(sender As System.Object, e As System.EventArgs)
        If txtVCECode.Text <> "" Then
            For Each item As ListViewItem In lvCoMaker.Items
                If item.SubItems(0).Text = txtVCECode.Text Then
                    MsgBox("Comaker already exist!", MsgBoxStyle.Exclamation)
                Else
                    lvCoMaker.Items.Add(txtVCECode.Text)
                    lvCoMaker.Items(lvCoMaker.Items.Count - 1).SubItems.Add(txtVCEName.Text)
                End If
            Next
        End If
    End Sub

    Private Sub btnRemove_Click(sender As System.Object, e As System.EventArgs)
        If lvCoMaker.SelectedItems.Count = 1 Then
            lvCoMaker.SelectedItems(0).Remove()
        End If
    End Sub

    Private Sub btnPrintReport_Click(sender As System.Object, e As System.EventArgs) Handles btnPrintReport.Click
        If cbReportType.SelectedItem = "Application for Express Loan" Then
            Dim f As New frmReport_Display
            f.ShowDialog(TransID, "APPEXL", "")
            f.Dispose()
        ElseIf cbReportType.SelectedItem = "Application for IOU Loan" Then
            Dim f As New frmReport_Display
            f.ShowDialog(TransID, "APPIOU", "")
            f.Dispose()
        ElseIf cbReportType.SelectedItem = "Application for Emergency Loan" Then
            Dim f As New frmReport_Display
            f.ShowDialog(TransID, "APPEML", "")
            f.Dispose()
        ElseIf cbReportType.SelectedItem = "Application for Basic Commodity Loan" Then
            Dim f As New frmReport_Display
            f.ShowDialog(TransID, "APPBCL", "")
            f.Dispose()
        ElseIf cbReportType.SelectedItem = "Application for Pensioner Loan" Then
            Dim f As New frmReport_Display
            f.ShowDialog(TransID, "APPPSL", "")
            f.Dispose()
        ElseIf cbReportType.SelectedItem = "Promissory Note with Notary" Then
            Dim f As New frmReport_Display
            f.ShowDialog(TransID, "PNWN", "")
            f.Dispose()
        ElseIf cbReportType.SelectedItem = "Promissory Note without Notary" Then
            Dim f As New frmReport_Display
            f.ShowDialog(TransID, "PNWON", "")
            f.Dispose()
        ElseIf cbReportType.SelectedItem = "Disclosure" Then
            Dim f As New frmReport_Display
            f.ShowDialog(TransID, "Disclosure", "")
            f.Dispose()
        ElseIf cbReportType.SelectedItem = "Promisorry Note" Then
            Dim f As New frmReport_Display
            f.ShowDialog(TransID, "Promisorry Note", "")
            f.Dispose()
        ElseIf cbReportType.SelectedItem = "Statement of Account" Then
            Dim f As New frmReport_Display
            f.ShowDialog(TransID, "SOA", "")
            f.Dispose()
        ElseIf cbReportType.SelectedItem = "Loan Approval Sheet" Then
            Dim f As New frmReport_Display
            f.ShowDialog(TransID, "LAS", "")
            f.Dispose()
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles btnPrintReport2.Click
        If txtVCECode.Text <> "" Then
            Dim f As New frmReport_Display
            f.ShowDialog(TransID, "NOTICE_COMAKER", dtpReportDate2.Value.Date, txtVCECode.Text)
            f.Dispose()
        Else
            MsgBox("Please select Co Maker!", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Function GetDeffaultAccount(ByVal Type As String) As String
        Dim query As String
        query = " SELECT LoanAccount from tblLoan_Type" & _
                " WHERE   Loan_Type ='" & cbLoanType.SelectedItem & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("LoanAccount").ToString
        Else
            Return ""
        End If
    End Function

    Private Function GetAccntTitle(ByVal Code As String) As String
        Dim query As String
        query = " SELECT AccntTitle FROM ChartOfAccount WHERE AccntCode = '" & Code & "' "
        SQL.FlushParams()
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("AccntTitle").ToString
        Else
            Return ""
        End If
    End Function

    Private Sub Label10_Click(sender As System.Object, e As System.EventArgs) Handles Label10.Click

    End Sub

    Public Sub UpdateLoanChargesValue(ByVal value As String, ByVal description As String)
        Dim UpdateSQL As String
        UpdateSQL = "UPDATE tblLoan_Charges SET Value = '" & value & "' " & _
                            "WHERE Loan_Code = '" & Loan_Code & "' AND Charge_ID = '" & GetChargeID(description) & "'"
        SQL.FlushParams()
        SQL.ExecNonQuery(UpdateSQL)
    End Sub

    Private Sub dgvCharges_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCharges.CellEndEdit
        ComputeProceeds()
        GenerateSchedule(lblMethod.Text)
    End Sub

    Private Function GetChargeID(ByVal Description As String) As String
        Dim query As String
        query = "SELECT ChargeID FROM tblloan_Chargesdefault WHERE Description ='" & Description & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("ChargeID").ToString
        Else
            Return ""
        End If
    End Function

    Private Sub rbNewBorrower_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbNewBorrower.CheckedChanged, rbRepeatBorrower.CheckedChanged, rbReturningBorrower.CheckedChanged
        If rbNewBorrower.Checked = True Then
            borrower = 1
        ElseIf rbRepeatBorrower.Checked = True Then
            borrower = 2
        ElseIf rbReturningBorrower.Checked = True Then
            borrower = 3
        End If
    End Sub

    Private Sub SaveLoanHistory(ByVal MemNo As String)
        'Dim insertSQL As String
        'Dim deleteSQL As String
        'deleteSQL = " DELETE FROM tblLoan_History WHERE MemNo = @MemNo "
        'SQL.AddParam("@MemNo", MemNo)
        'SQL.ExecNonQuery(deleteSQL)

        'deleteSQL = " DELETE FROM tblLoan_FDSD WHERE MemNo = @MemNo "
        'SQL.AddParam("@MemNo", MemNo)
        'SQL.ExecNonQuery(deleteSQL)

        'For Each row As DataGridViewRow In dgvLoanHistory.Rows
        '    If IsNumeric(row.Cells(1).Value) Then

        '        insertSQL = " INSERT INTO " & _
        '                        " tblLoan_History(MemNo, Loan_Type, Loan_Amount, Starting_Date, Maturity_Date, Terms, Offsetting, FD, SD) " & _
        '                        " VALUES (@MemNo, @Loan_Type, @Loan_Amount, @Starting_Date, @Maturity_Date, @Terms, @Offsetting, @FD, @SD)"
        '        SQL.FlushParams()
        '        SQL.AddParam("@MemNo", MemNo)
        '        SQL.AddParam("@Loan_Type", IIf(row.Cells(0).Value = Nothing, "", row.Cells(0).Value))
        '        SQL.AddParam("@Loan_Amount", IIf(row.Cells(0).Value = Nothing, "0", CDec(row.Cells(1).Value)))
        '        SQL.AddParam("@Starting_Date", IIf(row.Cells(2).Value = Nothing, Date.Now, row.Cells(2).Value))
        '        SQL.AddParam("@Maturity_Date", IIf(row.Cells(3).Value = Nothing, Date.Now, row.Cells(3).Value))
        '        SQL.AddParam("@Terms", IIf(row.Cells(4).Value = Nothing, "0", row.Cells(4).Value))
        '        SQL.AddParam("@Offsetting", IIf(row.Cells(5).Value = Nothing, "0", row.Cells(5).Value))
        '        SQL.AddParam("@FD", IIf(txtFD.Text = "", 0, txtFD.Text))
        '        SQL.AddParam("@SD", IIf(txtSD.Text = "", 0, txtSD.Text))
        '        SQL.ExecNonQuery(insertSQL)
        '    End If
        'Next

        'insertSQL = " INSERT INTO " & _
        '            " tblLoan_FDSD(MemNo, FD, SD) " & _
        '            " VALUES (@MemNo, @FD, @SD)"
        'SQL.FlushParams()
        'SQL.AddParam("@MemNo", MemNo)
        'SQL.AddParam("@FD", IIf(txtFD.Text = "", 0, txtFD.Text))
        'SQL.AddParam("@SD", IIf(txtSD.Text = "", 0, txtSD.Text))
        'SQL.ExecNonQuery(insertSQL)

        'MsgBox("Loan History Saved Successfully!", MsgBoxStyle.Information)
        'LoadLoanHistory(txtMemNo.Text)
    End Sub

    Private Sub LoadLoanHistory(ByVal MemNo As String)
        'Dim query As String
        'query = "SELECT * FROM tblLoan_History Where MemNo = '" & MemNo & "' "
        'SQL.ReadQuery(query)
        'dgvLoanHistory.Rows.Clear()
        'While SQL.SQLDR.Read
        '    dgvLoanHistory.Rows.Add({SQL.SQLDR("Loan_Type"), SQL.SQLDR("Loan_Amount").ToString, SQL.SQLDR("Starting_Date"), SQL.SQLDR("Maturity_Date").ToString, SQL.SQLDR("Offsetting").ToString, SQL.SQLDR("Terms").ToString})
        'End While

        'query = "SELECT * FROM tblLoan_FDSD Where MemNo = '" & MemNo & "' "
        'SQL.ReadQuery(query)
        'While SQL.SQLDR.Read
        '    txtFD.Text = CDec(SQL.SQLDR("FD"))
        '    txtSD.Text = CDec(SQL.SQLDR("SD"))
        'End While

    End Sub


    Private Sub btnSaveLoanHistory_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveLoanHistory.Click
        If txtMemNo.Text <> "" Then
            If MsgBox("Are you sure you want to Save Loan History?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                SaveLoanHistory(txtMemNo.Text)
            End If
        Else
            MsgBox("Please select Member Name?", vbInformation)
        End If

    End Sub

    Private Sub ClearText()
        strNote = ""
        CV_Type = ""
        borrower = 0
        DR_ID = 0
        rbNewBorrower.Checked = True
        txtLoanBalance.Text = 0
        txtStatus.Clear()
        txtCVNo.Clear()
        btnRestruct.Enabled = True
        disableEvent = False
        txtLoanAmount.Text = ""
        lblMethod.Text = ""
        txtMemNo.Clear()
        txtMemName.Clear()
        TransID = ""
        memType = ""
        dgvLoanHistory.Rows.Clear()
        dgvCharges.Rows.Clear()
        lvAmmort.Items.Clear()
        txtTransNum.Clear()

        txtFD.Clear()
        txtSD.Clear()
        dgvOtherDeductions.Columns.Clear()
        dgvOtherDeductions.Columns.Add("Description", "Description")
        dgvOtherDeductions.Columns.Add("Amount", "Amount")
        dgvOtherDeductions.Columns.Add("Account_Title", "Account Title")
        dgvOtherDeductions.Columns.Add("Account_Code", "Account Code")
        dgvOtherDeductions.Columns.Add("VCECode", "VCECode")
        dgvOtherDeductions.Columns.Add("VCEName", "VCEName")
        dgvOtherDeductions.Columns.Add("RefID", "RefID")
        dgvOtherDeductions.Columns(3).Visible = False
        dgvOtherDeductions.Columns(4).Visible = False

        txtVCECode.Text = ""
        txtVCEName.Text = ""
        txtDRNo.Text = ""
        txtIntAmount.Text = ""
        lvCoMaker.Items.Clear()
        cbPaymentType.SelectedIndex = 0
        chkwCollateral.Checked = False


        txtTitleNo.Clear()
        txtNameCollateral.Clear()
        txtMarriedTo.Clear()
        txtLocation.Clear()
        txtArea.Clear()
        cbClassification.SelectedIndex = -1
        txtAppraisedValue.Clear()
        lvTitle.Items.Clear()

        disableEvent = True
        txtLoanAmount.Text = ""
        lblProceeds.Text = ""
        lblAmort.Text = ""
        lvAmmort.Items.Clear()
        lblCapital.Text = ""
        txtNoOfAmmort.Text = ""
        txtTerms.Text = ""
        lblTotalAmount.Text = ""
        cbLoanType.SelectedIndex = -1
        cbWishlist.SelectedIndex = -1
        txtAmount.Text = ""
        disableEvent = False
        txtNotes.Text = ""
        txtVCEType.Text = ""
    End Sub





    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If LoanNo <> "" Then
            If MsgBox("Are you sure you want to delete this loan transaction?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim deleteSQl As String
                deleteSQl = " DELETE FROM tblLoan WHERE TransID ='" & TransID & "' "
                SQL.ExecNonQuery(deleteSQl)
                deleteSQl = " DELETE FROM tblLoan_Schedule WHERE Loan_ID ='" & LoanNo & "' "
                SQL.ExecNonQuery(deleteSQl)
                deleteSQl = " DELETE FROM tblLoan_Details WHERE Loan_ID ='" & LoanNo & "' "
                SQL.ExecNonQuery(deleteSQl)
                MsgBox("Record deleted successfully", MsgBoxStyle.Information)
            End If
        End If
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("LN_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("LN")
            If f.transID <> "" Then
                TransID = f.transID
                LoadLoan(TransID)
            End If
            f.Dispose()
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If LoanNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblLoan  WHERE Loan_No < '" & LoanNo & "' ORDER BY TransID DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadLoan(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If LoanNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblLoan  WHERE Loan_No > '" & LoanNo & "' ORDER BY TransID ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadLoan(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub PrintLoanScheduleToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles tsbSchedule.Click
        Dim f As New frmReport_Display
        If cbHistory.Visible = True AndAlso (cbHistory.SelectedIndex > -1 And cbHistory.SelectedIndex < cbHistory.Items.Count - 1) Then
            f.ShowDialog("LN_ScheduleHistory", TransID, cbHistory.SelectedItem)
            f.Dispose()
        Else
            f.ShowDialog("LN_Schedule", TransID, UserName)
            f.Dispose()
        End If
    End Sub

    Private Sub CVToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        Dim f As New frmCV
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub ApproveToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ApproveToolStripMenuItem.Click
        'If MsgBox("Are you sure you want to approve this loan?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '    ApproveLoan(TransID)
        '    Msg("Loans Approveds Succesfully!", MsgBoxStyle.Information)
        'End If
    End Sub

    Private Sub BatchApproveToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BatchApproveToolStripMenuItem.Click
        Dim f As New frmLoadList
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("Loan Approval")
        Dim ctr As Integer = 0
        For Each row As DataGridViewRow In f.dgvList.Rows
            If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                ApproveLoan(row.Cells(0).Value)
                ctr += 1
            End If
        Next
        If ctr > 0 Then
            Msg("Loans Approveds Succesfully!", MsgBoxStyle.Information)
        End If
        If TransID <> "" Then
            LoadLoan(TransID)
        End If
        f.Dispose()
    End Sub

    Private Sub ApproveLoan(ByVal LoanID As String)
        Try
            Dim query As String
            query = " UPDATE tblLoan " & _
                    " SET    Status ='Approved' " & _
                    " WHERE   TransID = " & LoanID
            SQL.ExecNonQuery(query)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub releaseLoan(ByVal LoanID As String)
        Try
            Dim query As String
            query = " UPDATE tblLoan " & _
                    " SET    Status ='Released' " & _
                    " WHERE   TransID = @TransID"
            SQL.FlushParams()
            SQL.AddParam("@TransID", LoanID)
            SQL.ExecNonQuery(query)
            query = " SELECT VCECode, AmortAmount,  LoanPayable, NoOfAmort, DateStart FROM tblLoan WHERE TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", LoanID)
            SQL.ReadQuery(query)
            Dim code As String
            Dim LoanPayable, Amort, NoofAmort As Decimal
            Dim dateStart As Date
            If SQL.SQLDR.Read Then
                code = SQL.SQLDR("VCECode").ToString
                LoanPayable = SQL.SQLDR("LoanPayable").ToString
                Amort = SQL.SQLDR("AmortAmount").ToString
                NoofAmort = SQL.SQLDR("NoOfAmort").ToString
                dateStart = SQL.SQLDR("DateStart").ToString

                'SQL.CloseCon()
                'SetPayrollDatabase()

                'Dim insertSQl As String
                'insertSQl = " INSERT INTO " & _
                '            " tblEmployee_Ledger(Employee_ID, Type, Code, Calc_Method, Rate, Days_Hrs, Amount, Cutoff, Total_Amount, No_of_Payday, Start_Date, Recurring, Status) " & _
                '            " SELECT " & code & " , 'Deductions', 'Loan', 'Fixed Amount', 0.00, 0.00, " & Amort & ", 'All', " & LoanPayable & ",  " & NoofAmort & ", '" & dateStart & "', 0, 'Active' "
                'SQL_RUBY.ExecNonQuery(insertSQl)
                'SQL_RUBY.CloseCon()
                'SetDatabase()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BatchRreleaseToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BatchRreleaseToolStripMenuItem.Click
        Dim f As New frmLoadList
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("Loan Release")
        Dim ctr As Integer = 0
        For Each row As DataGridViewRow In f.dgvList.Rows
            If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                releaseLoan(row.Cells(0).Value)
                ctr += 1
            End If
        Next
        If ctr > 0 Then
            Msg("Loans Approveds Succesfully!", MsgBoxStyle.Information)
        End If
        f.Dispose()
    End Sub

    Private Sub ReleasedToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ReleasedToolStripMenuItem.Click
        If MsgBox("Are you sure you want to approve this loan?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            releaseLoan(TransID)
            Msg("Loans Approveds Succesfully!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub cbPaymentType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbPaymentType.SelectedIndexChanged
        If cbPaymentType.SelectedIndex <> -1 Then
            If cbPaymentType.SelectedItem = "RUBY PAYROLL SD" Then

            End If
        End If
    End Sub

    Private Sub dgvCharges_DataError(sender As System.Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvCharges.DataError
        Try
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub cbAmortMethod_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbAmortMethod.SelectedIndexChanged
        If disableEvent = False Then
            ComputeProceeds()
            GenerateSchedule(lblMethod.Text)
        End If

    End Sub

    Private Sub DisclosureToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DisclosureToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("LN_Disclosure", TransID, UserName)
        f.Dispose()
    End Sub

    Private Sub PromisorryNoteToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PromisorryNoteToolStripMenuItem.Click
        Dim query, companyname As String

        query = "SELECT Company_Logo, Employer_Name FROM tblcompany"
        SQL.FlushParams()
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() Then
            companyname = SQL.SQLDR("Employer_Name").ToString
            Dim f As New frmReport_Display
            f.ShowDialog("LN_PN", TransID, companyname)
            f.Dispose()
        End If

    End Sub

    Private Sub LoanApprovalSheetToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LoanApprovalSheetToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("LN_LAS", TransID, UserName)
        f.Dispose()
    End Sub

    Private Sub cmbInterest_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbInterest.SelectedIndexChanged
        If cmbInterest.SelectedIndex >= 0 Then
            If disableEvent = False Then
                'FOULEN
                If Main_JADE.Label4.Text.ToLower.Contains("coop") Then
                    lblInterest.Text = Math.Round(CDec(cmbInterest.SelectedItem.ToString.Replace(" % Annually", "")) / 12, 3) & "%"
                Else
                    lblInterest.Text = Math.Round(CDec(cmbInterest.SelectedItem.ToString.Replace(" % Annually", "")) / 12, 2) & "%"
                End If

                'EDSCO

                intRate = Math.Round(CDec(CDec(cmbInterest.SelectedItem.ToString.Replace(" % Annually", "")) / 12) / 100, 6)
                intValue = intRate
                ComputeLoan()

            End If
        End If
    End Sub

    Private Sub dgvCharges_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCharges.CellContentClick

    End Sub

    Private Sub FromDRToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromDRToolStripMenuItem.Click
        If cbLoanType.SelectedIndex = -1 Then
            Msg("Please select loan type!", MsgBoxStyle.Exclamation)
        ElseIf GetLoanDR(cbLoanType.SelectedItem) <> True Then
            Msg("Please select loan type with For Delivery Receipt!", MsgBoxStyle.Exclamation)
        Else
            Dim f As New frmLoadTransactions
            f.cbFilter.SelectedItem = "Status"
            f.txtFilter.Text = "Active"
            f.txtFilter.Enabled = False
            f.cbFilter.Enabled = False
            f.btnSearch.Enabled = False
            f.ShowDialog("DR-LN")
            LoadDR(f.transID)
            f.Dispose()
        End If
    End Sub

    Public Function GetLoanDR(ByVal LoanType As String) As String
        Dim query As String
        query = " SELECT ForDR FROM tblLoan_Type " & _
                " WHERE LoanType = '" & LoanType & "' AND Status ='Active'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() Then
            Return SQL.SQLDR("ForDR")
        Else
            Return False
        End If
    End Function

    Private Sub txtMemName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtMemName.TextChanged

    End Sub

    Private Sub LoadDR(ByVal DR_No As String)
        Dim query As String
        query = "  SELECT   tblDR.TransID, tblDR.VCECode, VCEName, Remarks,  Unserved, viewDR_LN_Status.Status   " & _
                "  FROM     tblDR LEFT JOIN tblVCE_Master  " & _
                "  ON	   tblDR.VCECode = tblVCE_Master.VCECode  " & _
                "  LEFT JOIN	 viewDR_LN_Status  " & _
                "  ON		 tblDR.TransID = viewDR_LN_Status.TransID " & _
                "  LEFT JOIN viewDR_LN_UNserved " & _
                "  ON viewDR_LN_UNserved.TransID = tblDR.TransID " & _
                " WHERE   viewDR_LN_Status.Status ='Active' AND tblDR.TransID ='" & DR_No & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            DR_ID = SQL.SQLDR("TransID")
            txtMemNo.Text = SQL.SQLDR("VCECode")
            txtMemName.Text = SQL.SQLDR("VCEName")
            txtLoanAmount.Text = CDec(SQL.SQLDR("Unserved"))
            txtDRNo.Text = LoadDRNo(DR_ID)
            LoadVCEData()
        Else
            ClearText()
        End If
    End Sub

    Private Function LoadDRNo(DR_ID As Integer) As String
        Dim query As String
        query = " SELECT DR_No FROM tblDR WHERE TransID = '" & DR_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("DR_No")
        Else
            Return ""
        End If
    End Function


    Private Function LoadMemType(MemNo As String) As String
        Dim query As String
        query = " SELECT Member_Type FROM tblMember_Master WHERE Member_ID = '" & MemNo & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("Member_Type")
        Else
            Return ""
        End If
    End Function

    Private Sub btnAddTitle_Click(sender As System.Object, e As System.EventArgs) Handles btnAddTitle.Click
        If DataValidatedTitle() Then
            lvTitle.Items.Add("")
            lvTitle.Items(lvTitle.Items.Count - 1).SubItems.Add(txtTitleNo.Text)
            lvTitle.Items(lvTitle.Items.Count - 1).SubItems.Add(txtNameCollateral.Text)
            lvTitle.Items(lvTitle.Items.Count - 1).SubItems.Add(txtMarriedTo.Text)
            lvTitle.Items(lvTitle.Items.Count - 1).SubItems.Add(txtLocation.Text)
            lvTitle.Items(lvTitle.Items.Count - 1).SubItems.Add(CDec(txtArea.Text))
            lvTitle.Items(lvTitle.Items.Count - 1).SubItems.Add(cbClassification.SelectedItem)
            lvTitle.Items(lvTitle.Items.Count - 1).SubItems.Add(CDec(txtAppraisedValue.Text))


            txtTitleNo.Clear()
            txtNameCollateral.Clear()
            txtMarriedTo.Clear()
            txtLocation.Clear()
            txtArea.Clear()
            cbClassification.SelectedIndex = -1
            txtAppraisedValue.Clear()
        End If
    End Sub

    Private Function DataValidatedTitle() As Boolean
        If txtTitleNo.Text = "" Then
            Msg("Please enter Title Number!", MsgBoxStyle.Exclamation)
            txtTitleNo.Focus()
            Return False
        ElseIf txtNameCollateral.Text = "" Then
            Msg("Please enter Name of Collateral.", MsgBoxStyle.Exclamation)
            txtNameCollateral.Focus()
            Return False
        ElseIf txtLocation.Text = "" Then
            Msg("Please enter Location.", MsgBoxStyle.Exclamation)
            txtLocation.Focus()
            Return False
        ElseIf txtArea.Text = "" Then
            Msg("Please enter Area.", MsgBoxStyle.Exclamation)
            txtArea.Focus()
            Return False
        ElseIf txtAppraisedValue.Text = "" Then
            Msg("Please enter Appraised Value.", MsgBoxStyle.Exclamation)
            txtAppraisedValue.Focus()
            Return False
        ElseIf cbClassification.SelectedIndex = -1 Then
            Msg("Please select classification", MsgBoxStyle.Exclamation)
            cbClassification.Focus()
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub btnRemoveTitle_Click(sender As Object, e As System.EventArgs) Handles btnRemoveTitle.Click
        If lvTitle.SelectedItems.Count = 1 Then
            'Remove single item
            lvTitle.Items.RemoveAt(lvTitle.SelectedIndices(0))
        ElseIf lvTitle.SelectedItems.Count > 1 Then
            'Remove Multiple Selected Items 
            For Each item As ListViewItem In lvTitle.SelectedItems
                item.Remove()
            Next
        Else
            Msg("Please select title", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub txtArea_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtArea.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso e.KeyChar <> "." Then
            e.Handled = True
        Else
            If e.KeyChar = "." AndAlso sender.Text.Contains(".") Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtAppraisedValue_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtAppraisedValue.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso e.KeyChar <> "." Then
            e.Handled = True
        Else
            If e.KeyChar = "." AndAlso sender.Text.Contains(".") Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub chwCollateral_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkwCollateral.CheckedChanged
        If chkwCollateral.Checked = True Then
            grpTitle.Enabled = True
        Else
            grpTitle.Enabled = False
        End If
        ComputeLoan()
    End Sub

    Private Sub txtArea_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtArea.TextChanged

    End Sub

    Private Sub txtAppraisedValue_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtAppraisedValue.TextChanged

    End Sub

    Private Sub dgvCharges_CellEnter(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCharges.CellEnter

    End Sub

    Private Sub dgvOtherDeductions_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvOtherDeductions.CellContentClick

    End Sub

    Private Sub LoanReleaseToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub tsbReports_Click(sender As System.Object, e As System.EventArgs) Handles tsbReports.Click

    End Sub

    Private Sub TitleListToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub


    Private Sub BatchActiveToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BatchActiveToolStripMenuItem.Click
        If Not AllowAccess("LN_ACT") Then
            msgRestricted()
        Else
            Dim f As New frmLoadList
            f.cbFilter.SelectedItem = "Status"
            f.txtFilter.Text = "Approved"
            f.txtFilter.Enabled = False
            f.cbFilter.Enabled = False
            f.btnSearch.Enabled = False
            f.ShowDialog("Loan Active")
            Dim ctr As Integer = 0
            For Each row As DataGridViewRow In f.dgvList.Rows
                If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                    ActiveLoan(row.Cells(0).Value)
                    ctr += 1
                End If
            Next
            If ctr > 0 Then
                Msg("Loans Active Succesfully!", MsgBoxStyle.Information)
            End If
            If TransID <> "" Then
                LoadLoan(TransID)
            End If
            f.Dispose()
        End If
    End Sub

    Private Sub ActiveLoan(ByVal LoanID As String)
        Try
            Dim query As String
            query = " UPDATE tblLoan " & _
                    " SET    Status ='Active' " & _
                    " WHERE   TransID = " & LoanID
            SQL.ExecNonQuery(query)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Dim boolExtend As Boolean = False
    Private Sub tsbRenew_Click(sender As System.Object, e As System.EventArgs) Handles tsbExtend.Click
        'If MsgBox("Are you sure you want to renew this loan?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Renew") = MsgBoxResult.Yes Then
        '    Dim strVCECode As String = txtVCECode.Text
        '    Dim strVCEName As String = txtVCEName.Text
        '    Dim strLoanID As String = txtTransNum.Text
        '    Dim strTerms As String = txtTerms.Text
        '    Dim selectSQL As String = " SELECT TOP 1 * FROM View_LoanLedger WHERE Loan_No = '" & strLoanID & "' ORDER BY No DESC "
        '    SQL.ReadQuery(selectSQL, 2)
        '    While SQL.SQLDR2.Read
        '        tsbClose.PerformClick()
        '        tsbNew.PerformClick()
        '        txtVCECode.Text = strVCECode
        '        txtVCEName.Text = strVCEName
        '        cbLoanType.SelectedItem = "Renewed Loan"
        '        txtLoanAmount.Text = CDec(SQL.SQLDR2("LoanBalance").ToString)
        '        dgvOtherDeductions.Rows.Add()
        '    End While
        'End If
        'frmLoan_WindowExtend.strNote = strNote
        'frmLoan_WindowExtend.strTransID = TransID
        'frmLoan_WindowExtend.ShowDialog()
        If Not AllowAccess("LN_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)
            Label36.Visible = True
            dtpRenew.Visible = True
            boolExtend = True
            ' Toolstrip Buttons

            Dim query As String = ""
            query = " SELECT LoanCode, Method, InterestPeriod, ISNULL(Terms,1) AS Terms, PaymentType, CashVoucher, APR_Method, APR_Value, IntAmortMethod FROM tblLoan_Type WHERE LoanType = '" & cbLoanType.SelectedItem & "' AND Status ='Active' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read() Then
                strIntValue = SQL.SQLDR("APR_Value")
            End If
            Dim strInterestRate As String = CDec(cmbInterest.SelectedItem.ToString.Split("%")(0)).ToString.TrimEnd("0").TrimEnd(".")
            Dim strInterest As String = txtIntAmount.Text
            LoadInterestRate()
            cmbInterest.SelectedItem = strInterestRate & " % Annually"
            txtIntAmount.Text = strInterest
            'cbLoanType_SelectedIndexChanged(cbLoanType, EventArgs.Empty)
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbCancel.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
            tsbPrint.Enabled = False
            tsbCopy.Enabled = False
            ComputeLoan()
            Try
                If disableEvent = True Then
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            'Disable Member
            cbLoanType.Enabled = False
            txtMemNo.Enabled = False
            txtMemName.Enabled = False
            txtTransNum.Enabled = False
            dtpDocDate.Enabled = False
        End If
    End Sub

    Private Sub cbHistory_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbHistory.SelectedIndexChanged
        If cbHistory.Items.Count - 1 = cbHistory.SelectedIndex Then
            LoadSchedule(TransID)
        Else
            LoadScheduleExtend(TransID)
        End If
    End Sub

    Private Sub LoadScheduleExtend(ByVal LoanID As String)
        Try
            lvAmmort.Columns.Clear()
            lvAmmort.Columns.Add("No.")
            lvAmmort.Columns.Add("Date")
            lvAmmort.Columns(0).Width = 35
            lvAmmort.Columns(1).Width = 80
            lvAmmort.Columns.Add("Beg. Bal.")
            lvAmmort.Columns.Add("Ammortization")
            lvAmmort.Columns.Add("Principal")
            lvAmmort.Columns.Add("Interest")
            lvAmmort.Columns(2).Width = 100
            lvAmmort.Columns(3).Width = 100
            lvAmmort.Columns(4).Width = 100
            lvAmmort.Columns(5).Width = 100
            lvAmmort.Columns(2).TextAlign = HorizontalAlignment.Right
            lvAmmort.Columns(3).TextAlign = HorizontalAlignment.Right
            lvAmmort.Columns(4).TextAlign = HorizontalAlignment.Right
            lvAmmort.Columns(5).TextAlign = HorizontalAlignment.Right
            Dim a As Integer = 0
            Dim index As Integer = 6
            For Each item As DataGridViewRow In dgvCharges.Rows
                If item.Cells(3).Value = "Amortize" Then
                    lvAmmort.Columns.Add(item.Cells(1).Value.ToString)
                    lvAmmort.Columns(index).Width = 100
                    lvAmmort.Columns(index).TextAlign = HorizontalAlignment.Right
                    index += 1
                    a += 1
                End If
            Next
            lvAmmort.Columns.Add("Rem. Bal.")
            lvAmmort.Columns(index).Width = 100
            lvAmmort.Columns(index).TextAlign = HorizontalAlignment.Right
            Dim query As String
            query = " SELECT  AmortNum, Date, BegBalance, Amort, Principal, Interest, Other1, Other2, Other3, Other4, Other5, EndBalance " & _
                    " FROM    tblLoan_ScheduleExtend " & _
                    " WHERE   TransID = @TransID AND CONVERT(VARCHAR, DateCreated, 120) = @Date "
            SQL.FlushParams()
            SQL.AddParam("@TransID", LoanID)
            SQL.AddParam("@Date", cbHistory.SelectedItem)
            SQL.ReadQuery(query)
            lvAmmort.Items.Clear()
            While SQL.SQLDR.Read
                lvAmmort.Items.Add(SQL.SQLDR("AmortNum"))
                With lvAmmort.Items(lvAmmort.Items.Count - 1).SubItems
                    .Add(SQL.SQLDR("Date"))
                    .Add(CDec(SQL.SQLDR("BegBalance")).ToString("N2"))
                    .Add(CDec(SQL.SQLDR("Amort")).ToString("N2"))
                    .Add(CDec(SQL.SQLDR("Principal")).ToString("N2"))
                    .Add(CDec(SQL.SQLDR("Interest")).ToString("N2"))
                    For i As Integer = 1 To a
                        .Add(CDec(SQL.SQLDR("Other" & i)).ToString("N2"))
                    Next
                    .Add(CDec(SQL.SQLDR("EndBalance")).ToString("N2"))
                End With
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnEditNote_Click(sender As System.Object, e As System.EventArgs) Handles btnEditNote.Click
        If Not AllowAccess("LN_EDIT") Then
            msgRestricted()
        Else
            EnableControlNote(True)
        End If
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        ' Toolstrip Buttons
        If TransID = "" Then
            ClearText()
            EnableControlNote(False)
            btnEditNote.Enabled = False
        Else
            LoadLoan(TransID)
            btnEditNote.Enabled = True
        End If
        btnSaveNote.Enabled = False
        btnClose.Enabled = False
    End Sub

    Private Sub btnSaveNote_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveNote.Click
        If TransID <> "" Then
            If MsgBox("Update Notes?, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                Dim updateSQL As String
                updateSQL = " UPDATE    tblLoan " & _
                            " SET    Notes = @Notes, WhoModified = @WhoModified, DateModified = GETDATE() " & _
                            " WHERE     TransID = @TransID "
                SQL.FlushParams()
                SQL.AddParam("@TransID", TransID)
                SQL.AddParam("@WhoModified", UserID)
                SQL.AddParam("@Notes", txtNotes.Text)
                SQL.ExecNonQuery(updateSQL)

                Msg("Note Update Succesfully!", MsgBoxStyle.Information)
                btnClose.PerformClick()
            End If
        Else
            Msg("Please create loan!", MsgBoxStyle.Information)
        End If

    End Sub

    Private Sub ActiveToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ActiveToolStripMenuItem.Click

    End Sub

    Private Sub cbCountRenew_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub btnRenew_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton1.Click
        For i As Integer = 86 To 1342
            LoadLoan(i)
        Next
    End Sub

    Private Sub GroupBox_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox.Enter

    End Sub

    Private Sub txtMemNo_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtMemNo.TextChanged

    End Sub



    Private Sub LoadLoanAmount()
        If DisableEvent = False Then
            If IsNumeric(txtAmount.Text) And cbWishlist.SelectedIndex > -1 And IsNumeric(txtTerms.Text) Then
                If cbWishlist.SelectedItem = "Net Take Home" Then
                    txtLoanAmount.Text = txtAmount.Text
                    ComputeLoanDeductions(cbLoanType.Text)

                    For i As Integer = 0 To dgvOtherDeductions.Rows.Count - 1
                        If IsNumeric(dgvOtherDeductions.Rows(i).Cells(1).Value) Then
                            decFix += (CDec(dgvOtherDeductions.Rows(i).Cells(1).Value))
                        End If
                    Next

                    Dim a As Decimal = Math.Round((CDec(txtAmount.Text) + decFix))
                    Dim c As Decimal = (CDec(lblInterest.Text.Replace("%", "")) / 100) * txtTerms.Text
                    Dim b As Decimal = (1 - (decPercent / 100))
                    ' Dim s As Decimal = Math.Round((CDec(txtAmount.Text) + 10) / (1 - amount), 2)
                    If IsNumeric(txtLoanAmount.Text) AndAlso txtLoanAmount.Text = Math.Round((a) / ((b) - (c)), 2) Then
                        txtLoanAmount_TextChanged(txtLoanAmount, EventArgs.Empty)
                    Else
                        txtLoanAmount.Text = CDec(Math.Round((a) / ((b) - (c)), 2)).ToString("N2")
                    End If
                  Else
                    If IsNumeric(txtLoanAmount.Text) AndAlso txtLoanAmount.Text = CDec(txtAmount.Text) Then
                        txtLoanAmount_TextChanged(txtLoanAmount, EventArgs.Empty)
                    Else
                        txtLoanAmount.Text = CDec(txtAmount.Text)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtAmount_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtAmount.KeyUp
        LoadLoanAmount()
    End Sub

    Private Sub txtAmount_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtAmount.TextChanged

    End Sub

    Private Sub cbWishlist_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbWishlist.SelectedIndexChanged
        LoadLoanAmount()
    End Sub
End Class
