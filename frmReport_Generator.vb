Imports System.Linq
Imports Microsoft.Office.Interop

Public Class frmReport_Generator
    Dim SQL As New SQLControl
    Public Type As String
    Dim branch As String
    Private isSelecting As Boolean = False
    Private originalItems As List(Of String)


    Const Tab As String = "   "
    Dim CostCenterFilter As String
    Private Sub frmReports_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        cbReportCategory.Items.Clear()
        cbReportCategory.Items.Add("Financial Reports")
        cbReportCategory.Items.Add("Trial Balance")
        cbReportCategory.Items.Add("Books of Accounts")
        cbReportCategory.Items.Add("Journals")
        cbReportCategory.Items.Add("Ledgers")
        cbReportCategory.Items.Add("Schedule")
        cbReportCategory.Items.Add("Transaction Registers")
        cbReportCategory.Items.Add("Cash Position Report")
        cbReportCategory.Items.Add("Tax Reports")
        'cbReportCategory.Items.Add("AOE Reports")
        'cbReportCategory.Items.Add("CA Unliquidated")
        cbReportCategory.Items.Add("Cash Advance Reports")
        'cbReportCategory.Items.Add("Gross Margin Reports")
        'cbReportCategory.Items.Add("Fixed Asset Schedule")
        cbReportCategory.Items.Add("Accounts Receivable")
        cbReportCategory.Items.Add("Accounts Payable")
        cbReportCategory.Items.Add("Real Estate Sales")
        cbReportCategory.Items.Add("Commission Report")
        cbReportCategory.SelectedIndex = 0
        cbPeriod.SelectedItem = "Monthly"
        nupYear.Value = Date.Today.Year
        cbMonth.SelectedIndex = Date.Today.Month - 1
        LoadCostCenter(cbCostCenter)

        originalItems = New List(Of String)()
    End Sub



    Private Sub LoadCostCenter(cb As ComboBox)
        Dim query As String
        query = " SELECT DISTINCT Code FROM tblCC WHERE Status = 'Active'  ORDER BY Code "
        SQL.ReadQuery(query)
        cb.Items.Clear()
        If cb Is cbFilter Then
            cb.Items.Add("ALL")
        End If
        cb.Items.Add("ALL")
        While SQL.SQLDR.Read
            cb.Items.Add(SQL.SQLDR("Code").ToString)
        End While
        If cb.Items.Count > 0 Then
            cb.SelectedIndex = 0
        End If
        cb.Enabled = True
    End Sub

    Private Sub LoadBranches()
        Dim query As String
        query = " SELECT    DISTINCT  tblBranch.BranchCode + ' - ' + Description AS BranchCode  " &
                " FROM      tblBranch    " &
                " INNER JOIN tblUser_Access    ON   " &
                " tblBranch.BranchCode = tblUser_Access.Code    " &
                " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " &
                " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " &
                " WHERE     UserID ='" & UserID & "'  "
        SQL.ReadQuery(query)
        cbBranch.Items.Clear()
        cbBranch.Items.Add("ALL - ALL BRANCHES")
        While SQL.SQLDR.Read
            cbBranch.Items.Add(SQL.SQLDR("BranchCode").ToString)
        End While
        cbBranch.SelectedItem = "ALL - ALL BRANCHES"
    End Sub
    Private Sub cbReport_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbReportCategory.SelectedIndexChanged
        If cbReportCategory.SelectedItem = "AOE Reports" Then
            Me.Size = New Size(470, 701)
            GroupBox1.Visible = True
            GroupBox2.Visible = True
            Label3.Visible = False
            cbFilter.Visible = False
            cbCostCenter.Visible = False
            Label5.Visible = False
            Label6.Visible = False
            cbVCEFilter.Visible = False
            chkInclude.Visible = False
            btnPreview.Location = New Point(79, 599)
            btnExport.Location = New Point(237, 599)
            pgBar.Location = New Point(79, 633)

        Else
            Me.Size = New Size(445, 443)
            GroupBox1.Visible = False
            GroupBox2.Visible = False
            Label3.Visible = True
            cbFilter.Visible = True
            cbCostCenter.Visible = True
            Label6.Visible = True
            Label5.Visible = True
            cbVCEFilter.Visible = True
            chkInclude.Visible = True
            Label3.Location = New Point(22, 202)
            cbFilter.Location = New Point(83, 199)
            Label5.Location = New Point(22, 233)
            cbVCEFilter.Location = New Point(82, 230)
            Label6.Location = New Point(4, 264)
            cbCostCenter.Location = New Point(82, 261)
            chkInclude.Location = New Point(83, 302)
            btnPreview.Location = New Point(80, 327)
            btnExport.Location = New Point(238, 327)
            pgBar.Location = New Point(80, 361)
        End If

        cbFilter.Enabled = False
        cbVCEFilter.Enabled = False
        cbFilter.Items.Clear()
        cbVCEFilter.Items.Clear()
        cbVCEFilter.Text = ""
        btnExport.Visible = False
        cbReportType.Visible = True
        cbPeriod.Enabled = True
        If cbReportCategory.SelectedItem = "Financial Reports" Then
            cbReportType.Items.Clear()
            cbReportType.Items.Add("Income Statement")
            cbReportType.Items.Add("Income Statement - Audited")
            cbReportType.Items.Add("Income Statement - Per Group")
            cbReportType.Items.Add("Calendarized Income Statement")
            cbReportType.Items.Add("Balance Sheet")
            cbReportType.Items.Add("Balance Sheet - Audited")
            cbReportType.Items.Add("Balance Sheet - Per Group")
            cbReportType.Items.Add("Statement of Cash Flow")
            cbReportType.Items.Add("Statement of Changes in Equity")
            'cbReportType.Items.Add("Income Statement Cost Center")
            'cbReportType.Items.Add("Balance Sheet Cost Center")
            'cbReportType.Items.Add("Balance Sheet 2")
        ElseIf cbReportCategory.SelectedItem = "Trial Balance" Then
            cbReportType.Items.Clear()
            cbReportType.Items.Add("Worksheet")
            'cbReportType.Items.Add("Preliminary Trial Balance")
            'cbReportType.Items.Add("Post Closing Trial Balance")
        ElseIf cbReportCategory.SelectedItem = "Commission Report" Then
            cbReportType.Items.Clear()
            cbReportType.Items.Add("Commission Ledger")
            cbReportType.Items.Add("Summary of Commission")
            'cbReportType.Items.Add("Preliminary Trial Balance")
            'cbReportType.Items.Add("Post Closing Trial Balance")
        ElseIf cbReportCategory.SelectedItem = "Books of Accounts" Then
            cbReportType.Items.Clear()
            cbReportType.Items.Add("Cash Receipts Book")
            cbReportType.Items.Add("Cash Disbursements Book")
            cbReportType.Items.Add("Accounts Payable")
            'cbReportType.Items.Add("Purchase Book")
            cbReportType.Items.Add("Sales Book")
            cbReportType.Items.Add("General Journal")
            'cbReportType.Items.Add("Inventory Ledger")
            btnExport.Visible = True
        ElseIf cbReportCategory.SelectedItem = "Journals" Then
            cbReportType.Items.Clear()
            cbReportType.Items.Add("Cash Receipts Journal")
            cbReportType.Items.Add("Cash Disbursements Journal")
            cbReportType.Items.Add("Purchase Journal")
            cbReportType.Items.Add("Sales Journal")
            cbReportType.Items.Add("General Journal")
            'cbReportType.Items.Add("Inventory Journal")
        ElseIf cbReportCategory.SelectedItem = "Ledgers" Then
            cbReportType.Items.Clear()
            cbReportType.Items.Add("General Ledger")
            cbReportType.Items.Add("Subsidiary Ledger")
        ElseIf cbReportCategory.SelectedItem = "Transaction Registers" Then
            cbReportType.Items.Clear()
            cbReportType.Items.Add("CV Register")
            cbReportType.Items.Add("Check Register")
            cbReportType.Items.Add("Petty Cash Register")
            cbReportType.Items.Add("Cash Advance Register")
            cbReportType.Items.Add("Cash Receipts Register")
            cbReportType.Items.Add("Sales Register")
            cbReportType.Items.Add("Purchases Register")
            cbReportType.Items.Add("Purchase Requisition Register")
            cbReportType.Items.Add("Purchase Order Register")
            cbReportType.Items.Add("Receiving Report Register")
            cbReportType.Items.Add("Goods Issue Register")
            cbReportType.Items.Add("Goods Receipt Register")
            cbReportType.Items.Add("Sales Order Register")
            cbReportType.Items.Add("Delivery Receipt Register")
        ElseIf cbReportCategory.SelectedItem = "Cash Position Report" Then
            cbReportType.Visible = False
            cbReportType.Items.Clear()
        ElseIf cbReportCategory.SelectedItem = "Schedule" Then
            cbReportType.Items.Clear()
            cbReportType.Items.Add("Balance Sheet Accounts")
            cbReportType.Items.Add("Income Statement Accounts")
        ElseIf cbReportCategory.SelectedItem = "Accounts Receivable" Then
            cbReportType.Items.Clear()
            cbReportType.Items.Add("Aging of Accounts Receivable")
            'cbReportType.Items.Add("Aging of Accounts Receivable - Per Cost Center")
            'cbReportType.Items.Add("Aging of Accounts Receivable - Summary")
            'cbReportType.Items.Add("Aging of Accounts Receivable - Detailed")
            cbReportType.Items.Add("Statement of Account")
        ElseIf cbReportCategory.SelectedItem = "Accounts Payable" Then
            cbReportType.Items.Clear()
            cbReportType.Items.Add("Aging of Accounts Payable")
            'cbReportType.Items.Add("Aging of Accounts Payable - Per Cost Center")
            'cbReportType.Items.Add("Aging of Accounts Payable - Summary")
            'cbReportType.Items.Add("Aging of Accounts Payable - Detailed")
        ElseIf cbReportCategory.SelectedItem = "Tax Reports" Then
            cbReportType.Items.Clear()
            cbReportType.Items.Add("MAP")
            cbReportType.Items.Add("QAP")
            cbReportType.Items.Add("SAWT")
            cbReportType.Items.Add("SLS")
            cbReportType.Items.Add("SLP")
            cbReportType.Items.Add("Percentage Tax")
        ElseIf cbReportCategory.SelectedItem = "AOE Reports" Then
            cbReportType.Visible = False
            cbReportType.Items.Clear()
            LoadInsCompany()
            LoadAOE()
            cbPeriod.SelectedItem = "As Of"
            cbFilter.Enabled = True
        ElseIf cbReportCategory.SelectedItem = "Cash Advance Reports" Then
            cbReportType.Items.Clear()
            cbReportType.Items.Add("Cash Advance Unliquidated")
            cbReportType.Items.Add("Cash Advance Summary")
            cbPeriod.SelectedItem = "As Of"
            cbFilter.Enabled = True
        ElseIf cbReportCategory.SelectedItem = "Gross Margin Reports" Then
            cbReportType.Items.Clear()
            cbReportType.Items.Add("Gross Margin Report")
            cbReportType.Items.Add("Schedule of Cost Of Services")
            cbReportType.Items.Add("Schedule of Service Revenue")
        ElseIf cbReportCategory.SelectedItem = "Fixed Asset Schedule" Then
            cbReportType.Items.Clear()
            cbReportType.Items.Add("Lapsing Schedule")
            cbReportType.Items.Add("Lapsing Schedule - Calendarized")
        ElseIf cbReportCategory.SelectedItem = "Real Estate Sales" Then
            cbReportType.Items.Clear()
            cbReportType.Items.Add("Real Estate Sales - Draft")
            cbReportType.Items.Add("Real Estate Sales - Approved")
        End If


        If cbReportType.Items.Count > 0 Then
            cbReportType.SelectedIndex = 0
        End If
    End Sub

    Private Sub cbReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbReportType.SelectedIndexChanged
        DisableFilter()
        If cbReportType.SelectedItem = "General Ledger" Then
            LoadAccounts(cbFilter)
            cbPeriod.Enabled = True
            cbVCEFilter.Enabled = False
            cbVCEFilter.SelectedItem = ""
            'ElseIf cbReportType.SelectedItem = "Worksheet" Then
            '    cbPeriod.SelectedItem = "As Of"
            '    cbPeriod.Enabled = False
        ElseIf cbReportType.SelectedItem = "Commission Ledger" Then
            'LoadAccounts(cbFilter)
            LoadAPV_Buyer()
            cbPeriod.Enabled = True
            cbVCEFilter.Enabled = True
            cbVCEFilter.SelectedItem = "ALL"
            'cbPeriod.Enabled = True
            'cbFilter.Enabled = True
            cbFilter.Enabled = False
        ElseIf cbReportType.SelectedItem = "Summary of Commission" Then
            'LoadAccounts(cbFilter)
            LoadAPV_Buyer()
            cbPeriod.Enabled = True
            cbVCEFilter.Enabled = False
            cbVCEFilter.SelectedItem = ""
            'cbFilter.Enabled = False
            cbFilter.Enabled = False
        ElseIf cbReportType.SelectedItem = "Income Statement - Per Group" Then
            LoadAccounts(cbFilter)
            cbPeriod.Enabled = True
        ElseIf cbReportType.SelectedItem = "Balance Sheet - Per Group" Then
            LoadAccounts(cbFilter)
            cbPeriod.Enabled = True
        ElseIf cbReportType.SelectedItem = "Subsidiary Ledger" Then
            LoadAccounts(cbFilter)
            LoadVCE()
            cbPeriod.Enabled = True
            cbVCEFilter.Enabled = True
        ElseIf cbReportType.SelectedItem = "CV Register" Then
            LoadBank()
            cbPeriod.Enabled = True
        ElseIf cbReportType.SelectedItem = "Check Register" Then
            LoadBank()
            cbPeriod.Enabled = True
        ElseIf cbReportType.SelectedItem = "Sales Register" Then
            LoadSalesReftype()
            cbPeriod.Enabled = True
        ElseIf cbReportType.SelectedItem = "Cash Receipts Register" Then
            LoadCRBRefType()
            cbPeriod.Enabled = True
        ElseIf cbReportType.SelectedItem = "Statement of Account" Then
            LoadCustomer()
            cbPeriod.SelectedItem = "As Of"
            cbPeriod.Enabled = True
            cbFilter.Enabled = False
            cbVCEFilter.Enabled = True

        ElseIf cbReportType.SelectedItem = "Aging of Accounts Receivable" Then
            LoadARAccount()
            cbPeriod.SelectedItem = "As Of"
            cbPeriod.Enabled = False
            cbFilter.Enabled = True
            cbVCEFilter.Text = ""
            cbVCEFilter.Enabled = False

        ElseIf cbReportType.SelectedItem = "Aging of Accounts Receivable - Per Profit Center" Then
            LoadARAccount()
            cbPeriod.SelectedItem = "As Of"
            cbPeriod.Enabled = True
            cbFilter.Enabled = True
            LoadCostCenter(cbCostCenter)
        ElseIf cbReportType.SelectedItem = "Aging of Accounts Receivable - Summary" Then
            LoadARAccount()
            cbPeriod.SelectedItem = "As Of"
            cbPeriod.Enabled = True
            cbFilter.Enabled = True
        ElseIf cbReportType.SelectedItem = "Aging of Accounts Receivable - Detailed" Then
            LoadARAccount()
            cbPeriod.SelectedItem = "As Of"
            cbPeriod.Enabled = True
            cbFilter.Enabled = True
        ElseIf cbReportType.SelectedItem = "Lapsing Schedule" Then
            cbPeriod.SelectedItem = "As Of"
            cbPeriod.Enabled = True
            cbFilter.Enabled = True
        ElseIf cbReportType.SelectedItem = "Lapsing Schedule - Calendarized" Then
            cbPeriod.SelectedItem = "As Of"
            cbPeriod.Enabled = True
            cbFilter.Enabled = True
        ElseIf cbReportType.SelectedItem = "Balance Sheet Accounts" Then
            cbPeriod.SelectedItem = "As Of"
            cbPeriod.Enabled = False
            cbFilter.Enabled = True
            cbVCEFilter.Enabled = True
            LoadAccountsPerType(cbFilter, cbReportType.SelectedItem.Replace(" Accounts", ""))
        ElseIf cbReportType.SelectedItem = "Income Statement Accounts" Then
            cbPeriod.SelectedItem = "Date Range"
            cbPeriod.Enabled = True
            cbFilter.Enabled = True
            cbVCEFilter.Enabled = True
            LoadAccountsPerType(cbFilter, cbReportType.SelectedItem.Replace(" Accounts", ""))
        ElseIf cbReportType.SelectedItem = "Cash Advance Summary" Then
            cbPeriod.SelectedItem = "Date Range"
            cbPeriod.Enabled = True
            'cbFilter.Enabled = True
            cbVCEFilter.Enabled = True
            LoadVCEName(cbVCEFilter)

        ElseIf cbReportType.SelectedItem = "Income Statement Cost Center" Then
            cbPeriod.Enabled = True
            cbFilter.Enabled = True
            cbVCEFilter.Enabled = False
            cbVCEFilter.SelectedItem = ""
            LoadCostCenter(cbCostCenter)

        ElseIf cbReportType.SelectedItem = "Balance Sheet Cost Center" Then
            cbPeriod.Enabled = True
            cbFilter.Enabled = True
            cbVCEFilter.Enabled = False
            cbVCEFilter.SelectedItem = ""
            LoadCostCenter(cbCostCenter)
        ElseIf cbReportType.SelectedItem = "Aging of Accounts Payable" Then
            LoadAPAccount()
            cbPeriod.SelectedItem = "As Of"
            cbPeriod.Enabled = False
            cbFilter.Enabled = True
            LoadVCE()
            cbVCEFilter.Enabled = True
        ElseIf cbReportType.SelectedItem = "Aging of Accounts Payable - Per Cost Center" Then
            LoadAPAccount()
            cbPeriod.SelectedItem = "As Of"
            cbPeriod.Enabled = True
            cbFilter.Enabled = True
            LoadVCE()
            LoadCostCenter(cbCostCenter)
            cbVCEFilter.Enabled = True
        ElseIf cbReportType.SelectedItem = "Aging of Accounts Payable - Summary" Then
            LoadAPAccount()
            cbPeriod.SelectedItem = "As Of"
            cbPeriod.Enabled = True
            cbFilter.Enabled = True
            LoadVCE()
            cbVCEFilter.Enabled = True
        ElseIf cbReportType.SelectedItem = "Aging of Accounts Payable - Detailed" Then
            LoadAPAccount()
            cbPeriod.SelectedItem = "As Of"
            cbPeriod.Enabled = True
            cbFilter.Enabled = True
            LoadVCE()
            cbVCEFilter.Enabled = True
        ElseIf cbReportType.SelectedItem = "Statement of Cash Flow" Then
            cbPeriod.SelectedItem = "Monthly"
            cbPeriod.Enabled = False
            cbFilter.Enabled = False
            cbVCEFilter.Enabled = True
        ElseIf cbReportType.SelectedItem = "Statement of Changes in Equity" Then
            cbPeriod.SelectedItem = "Monthly"
            cbPeriod.Enabled = True
            cbFilter.Enabled = False
            cbVCEFilter.Enabled = False
            cbVCEFilter.SelectedItem = ""
            cbCostCenter.Enabled = False
        End If

    End Sub

    Private Sub LoadAPAccount()
        Dim query As String
        query = " SELECT AccntCode, AccountTitle FROM tblDefaultAccount " &
                " INNER JOIN " &
                " tblCOA_Master ON " &
                " tblCOA_Master.AccountCode = tblDefaultAccount.AccntCode " &
                " WHERE Type = 'AP'  AND tblDefaultAccount.Status = 'Active'" &
                " ORDER BY AccountTitle "
        SQL.ReadQuery(query)
        cbFilter.Items.Clear()
        While SQL.SQLDR.Read
            cbFilter.Items.Add(SQL.SQLDR("AccountTitle"))
        End While
        cbFilter.SelectedIndex = 0
        cbFilter.Enabled = True
    End Sub

    Private Sub LoadARAccount()
        Dim query As String
        query = " SELECT AccntCode, AccountTitle FROM tblDefaultAccount " &
                " INNER JOIN " &
                " tblCOA_Master ON " &
                " tblCOA_Master.AccountCode = tblDefaultAccount.AccntCode " &
                " WHERE Type = 'AR'  AND tblDefaultAccount.Status = 'Active'" &
                " ORDER BY AccountTitle "
        SQL.ReadQuery(query)
        cbFilter.Items.Clear()

        While SQL.SQLDR.Read
            cbFilter.Items.Add(SQL.SQLDR("AccountTitle"))
        End While
        cbFilter.SelectedIndex = 0
        cbFilter.Enabled = True
    End Sub

    Private Sub LoadScheduleVCE(Account As String)
        cbVCEFilter.Items.Clear()
        Dim query As String
        query = " SELECT DISTINCT  VCEName FROM view_GL WHERE  AccntTitle =@Account  AND Status <> 'Cancelled' "
        SQL.FlushParams()
        SQL.AddParam("@Account", Account)
        SQL.ReadQuery(query)
        cbVCEFilter.Items.Add("ALL")
        While SQL.SQLDR.Read
            cbVCEFilter.Items.Add(SQL.SQLDR("VCEName").ToString)
        End While
        cbVCEFilter.SelectedIndex = 0
        cbVCEFilter.Enabled = True
    End Sub

    Private Sub LoadInsCompany()
        Dim query As String
        query = " SELECT DISTINCT VCECode, VCEName  " &
                " FROM view_GL WHERE  VCECode IN (SELECT DISTINCT Ins_Company FROM viewVCE_Master) " &
                " ORDER BY VCEName "
        SQL.ReadQuery(query)
        lvFilter.Items.Clear()
        While SQL.SQLDR.Read
            lvFilter.Items.Add(SQL.SQLDR("VCEName").ToString)
        End While
    End Sub

    Private Sub LoadCustomer()
        Dim query As String
        query = " SELECT DISTINCT VCECode, VCEName  " &
                " FROM view_GL WHERE AccntCode IN (SELECT AccntCode FROM tblDefaultAccount WHERE Type ='AR') AND VCEName <> '' " &
                " ORDER BY VCEName "
        SQL.ReadQuery(query)
        cbVCEFilter.Items.Clear()
        'cbVCEFilter.Items.Add("ALL")
        cbFilter.Items.Clear()
        While SQL.SQLDR.Read
            Dim item As String = SQL.SQLDR("VCEName").ToString()
            cbVCEFilter.Items.Add(item)
            originalItems.Add(item)
        End While
    End Sub

    Private Sub DisableFilter()
        cbFilter.Items.Clear()
        cbFilter.Enabled = False
    End Sub
    Private Sub LoadSubsidiary()
        Dim query As String
        query = " SELECT   DISTINCT tblCOA_Master.AccountTitle " &
                " FROM     tblCOA_Master " &
                " WHERE    withSubsidiary = 1 AND AccountGroup  = 'SubAccount'" &
                " ORDER BY AccountTitle "
        SQL.ReadQuery(query)
        lvFilter.Items.Clear()
        While SQL.SQLDR.Read
            lvFilter.Items.Add(SQL.SQLDR("AccountTitle").ToString)
        End While
    End Sub
    Private Sub LoadPeriod()
        If cbMonth.SelectedIndex <> -1 Then
            If cbPeriod.SelectedItem = "Quarterly" Then
                Select Case cbMonth.SelectedItem
                    Case "1st Quarter"
                        dtpFrom.Value = Date.Parse("1-1-" & nupYear.Value.ToString)
                        dtpTo.Value = Date.Parse("3-31-" & nupYear.Value.ToString)
                    Case "2nd Quarter"
                        dtpFrom.Value = Date.Parse("4-1-" & nupYear.Value.ToString)
                        dtpTo.Value = Date.Parse("6-30-" & nupYear.Value.ToString)
                    Case "3rd Quarter"
                        dtpFrom.Value = Date.Parse("7-1-" & nupYear.Value.ToString)
                        dtpTo.Value = Date.Parse("9-30-" & nupYear.Value.ToString)
                    Case "4th Quarter"
                        dtpFrom.Value = Date.Parse("10-1-" & nupYear.Value.ToString)
                        dtpTo.Value = Date.Parse("12-31-" & nupYear.Value.ToString)
                End Select
            Else
                Dim period As String = (cbMonth.SelectedIndex + 1).ToString & "-1-" & nupYear.Value.ToString
                If chkYTD.Checked Then
                    dtpFrom.Value = Date.Parse("1-1-" & nupYear.Value.ToString)
                    dtpTo.Value = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, Date.Parse(period)))
                Else
                    dtpFrom.Value = Date.Parse(period)
                    dtpTo.Value = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, Date.Parse(period)))
                End If

            End If
        End If
    End Sub

    Private Sub Reset()
        cbMonth.Enabled = True
        lvFilter.Items.Clear()
        gbPeriodYM.Visible = True
        gbPeriodFT.Visible = False
        cbPeriod.Enabled = False
        lblFrom.Text = "From :"
        lblTo.Visible = True
        dtpTo.Visible = True
        cbPeriod.Items.Clear()
        cbPeriod.Items.Add("Yearly")
        cbPeriod.Items.Add("Monthly")
        cbPeriod.Items.Add("Daily")
        cbPeriod.Items.Add("Date Range")
        cbPeriod.SelectedIndex = -1

        cbMonth.Items.Clear()
        cbMonth.Items.Add("January")
        cbMonth.Items.Add("February")
        cbMonth.Items.Add("March")
        cbMonth.Items.Add("April")
        cbMonth.Items.Add("May")
        cbMonth.Items.Add("June")
        cbMonth.Items.Add("July")
        cbMonth.Items.Add("August")
        cbMonth.Items.Add("September")
        cbMonth.Items.Add("October")
        cbMonth.Items.Add("November")
        cbMonth.Items.Add("December")
        lblMonth.Text = "Month :"
    End Sub

    Dim reportType As String

    Private Sub btnPreview_Click(sender As System.Object, e As System.EventArgs) Handles btnPreview.Click
        branch = "ALL"
        If cbCostCenter.SelectedItem = "ALL" Then
            CostCenterFilter = ""
        Else
            CostCenterFilter = "AND CostCenter = '" & cbCostCenter.SelectedItem & "'"
        End If
        Select Case cbReportCategory.SelectedItem
            Case "Financial Reports"
                Dim f As New frmReport_Display
                Select Case cbReportType.SelectedItem

                    Case "Income Statement"
                        GenerateNewIS()
                        f.ShowDialog("FSIS", UserName, dtpTo.Value.Date, cbBranch.SelectedItem, cbCostCenter.SelectedItem)
                    Case "Income Statement - Audited"
                        GenerateNewIS()
                        f.ShowDialog("FSIS_Audited", UserName, dtpTo.Value.Date, cbBranch.SelectedItem, cbCostCenter.SelectedItem)
                    Case "Income Statement - Per Group"
                        GenerateBS()
                        f.ShowDialog("FSIS_PerGroup", UserName, dtpTo.Value.Date, cbBranch.SelectedItem, cbCostCenter.SelectedItem)
                    Case "Calendarized Income Statement"
                        GroupBox6.Enabled = False
                        pgBar.Maximum = 12
                        pgBar.Value = 0
                        pgBar.Visible = True
                        bgwIS.RunWorkerAsync()
                    Case "Balance Sheet"
                        GenerateBS()
                        f.ShowDialog("FSBS", UserName, dtpTo.Value.Date, cbBranch.SelectedItem, cbCostCenter.SelectedItem)
                    Case "Balance Sheet - Audited"
                        GenerateBS()
                        f.ShowDialog("FSBS_Audited", UserName, dtpTo.Value.Date, cbBranch.SelectedItem, cbCostCenter.SelectedItem)
                    Case "Balance Sheet - Per Group"
                        GenerateBS()
                        f.ShowDialog("FSBS_PerGroup", UserName, dtpTo.Value.Date, cbBranch.SelectedItem, cbCostCenter.SelectedItem)
                    Case "Balance Sheet 2"
                        GenerateBS()
                        f.ShowDialog("FSBS2", UserName, dtpTo.Value.Date, cbBranch.SelectedItem)
                    Case "Income Statement Cost Center"
                        reportType = cbReportType.SelectedItem
                        GenerateNewIS()
                        f.ShowDialog("FSIS_CC", UserName, dtpTo.Value.Date, cbBranch.SelectedItem, cbFilter.SelectedItem)
                    Case "Balance Sheet Cost Center"
                        GenerateBS()
                        f.ShowDialog("FSBSCC", UserName, dtpTo.Value.Date, cbBranch.SelectedItem, cbFilter.SelectedItem)
                    Case "Statement of Cash Flow"
                        f.ShowDialog("Cash_Flow", UserName, dtpTo.Value.Date, cbBranch.SelectedItem, cbFilter.SelectedItem)
                    Case "Statement of Changes in Equity"
                        GenerateSCE(dtpFrom.Value.Date, dtpTo.Value.Date)
                End Select
                f.Dispose()
            Case "Trial Balance"
                Select Case cbReportType.SelectedItem
                    Case "Worksheet"
                        GenerateTB("Detailed", dtpFrom.Value.Date, dtpTo.Value.Date)
                    Case "Preliminary Trial Balance"
                    Case "Post Closing Trial Balance"
                End Select
            Case "Journals"
                Dim f As New frmReport_Display
                Select Case cbReportType.SelectedItem
                    Case "Cash Receipts Journal"
                        f.ShowDialog("Journal", "Cash Receipts", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, cbCostCenter.SelectedItem)
                    Case "Cash Disbursements Journal"
                        f.ShowDialog("Journal", "Cash Disbursements", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, cbCostCenter.SelectedItem)
                    Case "Purchase Journal"
                        f.ShowDialog("Journal", "Purchase Journal", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, cbCostCenter.SelectedItem)
                    Case "Sales Journal"
                        f.ShowDialog("Journal", "Sales", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, cbCostCenter.SelectedItem)
                    Case "General Journal"
                        f.ShowDialog("Journal", "General Journal", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, cbCostCenter.SelectedItem)
                    Case "Inventory Journal"
                        f.ShowDialog("Journal", "Inventory", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, cbCostCenter.SelectedItem)
                End Select
                f.Dispose()
            Case "Ledgers"
                Dim f As New frmReport_Display
                Select Case cbReportType.SelectedItem
                    Case "General Ledger"
                        If cbCostCenter.SelectedItem = "ALL" Then
                            f.ShowDialog("GL", cbFilter.SelectedItem, dtpFrom.Value.Date, dtpTo.Value.Date)
                        Else
                            f.ShowDialog("GL_COSTCENTER", cbFilter.SelectedItem, dtpFrom.Value.Date, dtpTo.Value.Date, cbCostCenter.SelectedItem)
                        End If
                    Case "Subsidiary Ledger"
                        If cbCostCenter.SelectedItem = "ALL" Then
                            f.ShowDialog("SL", cbFilter.SelectedItem, dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, cbVCEFilter.Text)
                        Else
                            f.ShowDialog("SL_COSTCENTER", cbFilter.SelectedItem, dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, cbVCEFilter.Text, cbCostCenter.SelectedItem)
                        End If
                End Select
                f.Dispose()
            Case "Schedule"
                Dim f As New frmReport_Display
                Select Case cbReportType.SelectedItem
                    Case "Balance Sheet Accounts"
                        If cbCostCenter.SelectedItem = "ALL" Then
                            f.ShowDialog("Schedule", dtpFrom.Value.Date, GetAccntCode(cbFilter.SelectedItem), GetVCECode(cbVCEFilter.SelectedItem))
                        Else
                            f.ShowDialog("Schedule_COSTCENTER", dtpFrom.Value.Date, GetAccntCode(cbFilter.SelectedItem), GetVCECode(cbVCEFilter.SelectedItem), cbCostCenter.SelectedItem)
                        End If

                        f.Dispose()
                    Case "Income Statement Accounts"
                        f.ShowDialog("Schedule_IS", dtpFrom.Value.Date, dtpTo.Value.Date, GetAccntCode(cbFilter.SelectedItem), IIf(cbVCEFilter.Text = "ALL", "ALL", GetVCECode(cbVCEFilter.Text)), cbCostCenter.Text)
                End Select



            Case "Transaction Registers"
                Dim f As New frmReport_Display
                Select Case cbReportType.SelectedItem
                    Case "CV Register"
                        f.ShowDialog("CV_Register", cbFilter.SelectedItem, dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, cbCostCenter.SelectedItem)
                    Case "Check Register"
                        f.ShowDialog("Check_Register", cbFilter.SelectedItem, dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked)
                    Case "Petty Cash Register"
                        f.ShowDialog("PCV_Register", "", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked)
                    Case "Cash Advance Register"
                        f.ShowDialog("CA_Register", "", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked)
                    Case "Cash Receipts Register"
                        f.ShowDialog("CR_Register", cbFilter.SelectedItem, dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked)
                    Case "Sales Register"
                        f.ShowDialog("Sales_Register", cbFilter.SelectedItem, dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked)
                    Case "Purchases Register"
                        f.ShowDialog("Purchases_Register", "", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked)
                    Case "Purchase Requisition Register"
                        f.ShowDialog("PR_Register", "", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked)
                    Case "Purchase Order Register"
                        f.ShowDialog("PO_Register", "", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked)
                    Case "Receiving Report Register"
                        f.ShowDialog("RR_Register", "", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked)
                    Case "Goods Issue Register"
                        f.ShowDialog("GI_Register", "", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked)
                    Case "Goods Receipt Register"
                        f.ShowDialog("GR_Register", "", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked)
                    Case "Sales Order Register"
                        f.ShowDialog("SO_Register", "", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked)
                    Case "Delivery Receipt Register"
                        f.ShowDialog("DR_Register", "", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked)
                End Select
                f.Dispose()
            Case "Cash Position Report"
                GenerateCPR()
            Case "Accounts Receivable"
                Select Case cbReportType.SelectedItem
                    Case "Aging of Accounts Receivable"
                        Dim f As New frmReport_Display
                        f.ShowDialog("AR_Aging", "", dtpFrom.Value.Date, GetAccntCode(cbFilter.SelectedItem), cbCostCenter.SelectedItem)

                        'If cbCostCenter.SelectedItem = "ALL" Then
                        '    f.ShowDialog("AR_Aging", "", dtpFrom.Value.Date, GetAccntCode(cbFilter.SelectedItem))
                        'Else
                        '    f.ShowDialog("AR_Aging_CostCenter", "", dtpFrom.Value.Date, GetAccntCode(cbFilter.SelectedItem), cbCostCenter.SelectedItem)
                        'End If
                        f.Dispose()
                    Case "Aging of Accounts Receivable - Summary"
                        Dim f As New frmReport_Display
                        f.ShowDialog("AR_Aging_Summary", "", dtpFrom.Value.Date, GetAccntCode(cbFilter.SelectedItem), "")
                        f.Dispose()
                    Case "Aging of Accounts Receivable - Detailed"
                        Dim f As New frmReport_Display
                        f.ShowDialog("AR_Aging_Detailed", "", dtpFrom.Value.Date, GetAccntCode(cbFilter.SelectedItem), "")
                        f.Dispose()
                        'Case "Statement of Account"
                        '    Dim VCECode As String
                        '    VCECode = GetVCECode(cbFilter.SelectedItem)
                        '    Dim f As New frmReport_Display
                        '    f.ShowDialog("SOA", "", dtpFrom.Value.Date, VCECode, "")
                        '    f.Dispose()
                    Case "Statement of Account"
                        Dim VCECode As String
                        If cbVCEFilter.SelectedItem = "All" Then
                            VCECode = "All"
                        Else
                            VCECode = GetVCECode(cbVCEFilter.SelectedItem)
                        End If
                        Dim f As New frmReport_Display
                        If VCECode = "All" Then
                            f.ShowDialog("SOA_AllVCE", "", dtpFrom.Value.Date, "", "")
                            f.Dispose()
                        Else
                            f.ShowDialog("SOA", "", dtpFrom.Value.Date, VCECode, "")
                            f.Dispose()
                        End If
                End Select
            Case "Tax Reports"
                If cbReportType.SelectedItem = "Percentage Tax" Then
                Else
                    Dim f As New FrmSAWT
                    f.ReportType = cbReportType.SelectedItem
                    f.DateFrom = dtpFrom.Value.Date
                    f.DateTo = dtpTo.Value.Date
                    f.CostCenter = cbCostCenter.Text
                    f.Show()
                End If
            Case "Books of Accounts"
                Dim f As New frmReport_Display
                Select Case cbReportType.SelectedItem
                    Case "Cash Receipts Book"
                        GenerateBooks("Cash Receipts")
                        f.ShowDialog("Books_CRB", "Cash Receipts", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, UserID, cbCostCenter.Text)
                    Case "Cash Disbursements Book"
                        GenerateBooks("Cash Disbursements")
                        f.ShowDialog("Books_CDB", "Cash Disbursements", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, UserID, cbCostCenter.Text)
                    Case "Accounts Payable"
                        GenerateBooks("Accounts Payable")
                        f.ShowDialog("Books_APV", "Accounts Payable", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, UserID, cbCostCenter.Text)
                    Case "Purchase Book"
                        GenerateBooks("Purchases")
                    Case "Sales Book"
                        GenerateBooks("Sales")
                        f.ShowDialog("Books_SB", "Sales", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, UserID, cbCostCenter.Text)
                    Case "General Journal"
                        GenerateBooks("General Journal")
                        f.ShowDialog("Books_GJ", "General Journal", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, UserID, cbCostCenter.Text)
                    Case "Inventory Ledger"
                        GenerateBooks("Inventory")
                        f.ShowDialog("Books_Inventory", "Inventory", dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, UserID)
                End Select
            Case "AOE Reports"
                If cbReportCategory.SelectedItem = "AOE Reports" Then
                    Dim f As New frmReport_Display
                    'Client
                    Dim MyFilterx As String = ""
                    If rbAll.Checked = True Then
                        MyFilterx = "All"
                    Else
                        For Each item As ListViewItem In lvFilter.Items
                            If item.Checked = True Then
                                If MyFilterx = "" Then
                                    MyFilterx = item.Text
                                Else
                                    MyFilterx = MyFilterx + ","
                                    MyFilterx = MyFilterx + " " + item.Text
                                End If

                            End If
                        Next
                    End If


                    'AccountCode
                    Dim MyFilterx2 As String = ""
                    If rbAll2.Checked = True Then
                        MyFilterx2 = "All"
                    Else
                        For Each item2 As ListViewItem In lvFilter2.Items
                            If item2.Checked = True Then
                                If MyFilterx2 = "" Then
                                    MyFilterx2 = item2.Text
                                Else
                                    MyFilterx2 = MyFilterx + ","
                                    MyFilterx2 = MyFilterx + " " + item2.Text
                                End If

                            End If
                        Next
                    End If

                    f.ShowDialog("AOE", dtpFrom.Value.Date, MyFilterx, MyFilterx2)
                    f.Dispose()
                End If
            Case "Cash Advance Reports"

                Dim f As New frmReport_Display
                Select Case cbReportType.SelectedItem
                    Case "Cash Advance Unliquidated"
                        f.ShowDialog("CA_Unliquidated", dtpTo.Value.Date, chkInclude.Checked)
                    Case "Cash Advance Summary"
                        f.ShowDialog("CA_Summary", cbVCEFilter.SelectedItem, dtpFrom.Value.Date, dtpTo.Value.Date)
                End Select

            Case "Gross Margin Reports"
                Dim f As New frmReport_Display
                Select Case cbReportType.SelectedItem
                    Case "Gross Margin Report"
                        f.ShowDialog("GPR", dtpFrom.Value.Date, dtpTo.Value.Date)
                    Case "Schedule of Cost Of Services"
                        f.ShowDialog("SCHED_COS", dtpFrom.Value.Date, dtpTo.Value.Date)
                    Case "Schedule of Service Revenue"
                        f.ShowDialog("SCHED_SR", dtpFrom.Value.Date, dtpTo.Value.Date)
                End Select

            Case "Fixed Asset Schedule"

                Dim f As New frmReport_Display
                Select Case cbReportType.SelectedItem
                    Case "Lapsing Schedule"
                        f.ShowDialog("Lapsing_Schedule", dtpFrom.Value.Date)
                        f.Dispose()
                    Case "Lapsing Schedule - Calendarized"
                        f.ShowDialog("Lapsing_Schedule_Calendarized", dtpFrom.Value.Date)
                        f.Dispose()
                End Select

            'Case "Real Estate Sales"

            '    Dim f As New frmReport_Display
            '    Select Case cbReportType.SelectedItem
            '        Case "Real Estate Sales - Draft"
            '            Dim Code As String
            '            Code = GetCostCenter(cbCostCenter.SelectedItem)
            '            If cbPeriod.SelectedItem = "Daily" Then
            '                f.ShowDialog("RE_TotalSales", "Draft", Code, dtpFrom.Value.Date, dtpFrom.Value.Date)
            '            Else
            '                f.ShowDialog("RE_TotalSales", "Draft", Code, dtpFrom.Value.Date, dtpTo.Value.Date)
            '            End If
            '            f.Dispose()
            '        Case "Real Estate Sales - Approved"
            '            f.ShowDialog("RE_TotalSales", "Active", cbCostCenter.SelectedItem, dtpFrom.Value.Date, dtpTo.Value.Date)
            '            f.Dispose()
            '    End Select

            Case "Real Estate Sales"

                Dim f As New frmReport_Display
                Dim Code As String
                If cbCostCenter.SelectedItem = "All" Then
                    Code = "All"
                Else
                    Code = cbCostCenter.SelectedItem
                End If
                GetCostCenter(Code)
                If cbCostCenter.SelectedItem = "ALL" Then
                    Select Case cbReportType.SelectedItem
                        Case "Real Estate Sales - Draft"
                            If cbPeriod.SelectedItem = "Daily" Then
                                f.ShowDialog("RE_TotalSales", "Draft", Code, dtpFrom.Value.Date, dtpFrom.Value.Date)
                            Else
                                f.ShowDialog("RE_TotalSales", "Draft", Code, dtpFrom.Value.Date, dtpTo.Value.Date)
                            End If

                        Case "Real Estate Sales - Approved"
                            If cbPeriod.SelectedItem = "Daily" Then
                                f.ShowDialog("RE_TotalSales", "Active", Code, dtpFrom.Value.Date, dtpFrom.Value.Date)
                            Else
                                f.ShowDialog("RE_TotalSales", "Active", Code, dtpFrom.Value.Date, dtpTo.Value.Date)
                            End If
                    End Select
                Else
                    Select Case cbReportType.SelectedItem
                        Case "Real Estate Sales - Draft"
                            If cbPeriod.SelectedItem = "Daily" Then
                                f.ShowDialog("RE_TotalSales", "Draft", Code, dtpFrom.Value.Date, dtpFrom.Value.Date)
                            Else
                                f.ShowDialog("RE_TotalSales", "Draft", Code, dtpFrom.Value.Date, dtpTo.Value.Date)
                            End If

                        Case "Real Estate Sales - Approved"
                            If cbPeriod.SelectedItem = "Daily" Then
                                f.ShowDialog("RE_TotalSales", "Active", Code, dtpFrom.Value.Date, dtpFrom.Value.Date)
                            Else
                                f.ShowDialog("RE_TotalSales", "Active", Code, dtpFrom.Value.Date, dtpTo.Value.Date)
                            End If
                    End Select
                End If
                f.Dispose()

            Case "Commission Report"
                Dim f As New frmReport_Display
                Select Case cbReportType.SelectedItem
                    Case "Commission Ledger"
                        f.ShowDialog("RE_ComExp", dtpFrom.Value.Date, dtpTo.Value.Date, "Detailed", cbCostCenter.SelectedItem, IIf(cbVCEFilter.SelectedItem Is Nothing, cbVCEFilter.Text, cbVCEFilter.SelectedItem))
                    Case "Summary of Commission"
                        f.ShowDialog("RE_ComExp", dtpFrom.Value.Date, dtpTo.Value.Date, "Summary", cbCostCenter.SelectedItem, "ALL")
                End Select
                f.Dispose()


            Case "Accounts Payable"
                Select Case cbReportType.SelectedItem
                    Case "Aging of Accounts Payable"
                        Dim f As New frmReport_Display
                        If cbCostCenter.SelectedItem = "ALL" Then
                            f.ShowDialog("AP_Aging", "", dtpFrom.Value.Date, GetAccntCode(cbFilter.SelectedItem), GetVCECode(cbVCEFilter.SelectedItem))
                        Else
                            f.ShowDialog("AP_Aging_CostCenter", "", dtpFrom.Value.Date, GetAccntCode(cbFilter.SelectedItem), GetVCECode(cbVCEFilter.SelectedItem), cbCostCenter.Text)
                        End If
                        f.Dispose()
                    Case "Aging of Accounts Payable - Per Cost Center"
                        Dim f As New frmReport_Display
                        f.ShowDialog("AP_Aging_CostCenter", "", dtpFrom.Value.Date, GetAccntCode(cbFilter.SelectedItem), cbVCEFilter.SelectedItem, cbCostCenter.SelectedItem)
                        f.Dispose()
                    Case "Aging of Accounts Payable - Summary"
                        Dim f As New frmReport_Display
                        If cbCostCenter.SelectedItem = "ALL" Then
                            f.ShowDialog("AP_Aging_Summary", "", dtpFrom.Value.Date, GetAccntCode(cbFilter.SelectedItem), cbVCEFilter.SelectedItem)
                        Else
                            f.ShowDialog("AP_Aging_Summary_CostCenter", "", dtpFrom.Value.Date, GetAccntCode(cbFilter.SelectedItem), cbVCEFilter.SelectedItem, cbCostCenter.SelectedItem)
                        End If
                        f.Dispose()
                    Case "Aging of Accounts Payable - Detailed"
                        Dim f As New frmReport_Display
                        If cbCostCenter.SelectedItem = "ALL" Then
                            f.ShowDialog("AP_Aging_Detailed", "", dtpFrom.Value.Date, GetAccntCode(cbFilter.SelectedItem), cbVCEFilter.SelectedItem)
                        Else
                            f.ShowDialog("AP_Aging_Detailed_CostCenter", "", dtpFrom.Value.Date, GetAccntCode(cbFilter.SelectedItem), cbVCEFilter.SelectedItem, cbCostCenter.SelectedItem)
                        End If
                        f.Dispose()
                End Select

        End Select


        'If cbReportCategory.SelectedItem = "Trial Balance" Then
        '    GenerateTB(IIf(rbSummary.Checked = True, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date)
        'ElseIf cbReportCategory.SelectedItem = "Book of Accounts" Then
        '    Dim index As Integer = -1
        '    For Each item As ListViewItem In lvFilter.Items
        '        If item.Checked = True Then
        '            index = item.Index
        '        End If
        '    Next
        '    If index <> -1 Then
        '        If cbPeriod.SelectedItem = "Daily" Then
        '            GenerateTB("By Book", dtpFrom.Value.Date, dtpFrom.Value.Date, lvFilter.Items(index).SubItems(0).Text)
        '        ElseIf cbPeriod.SelectedItem = "Monthly" Then
        '            Dim DateFrom, DateTo As Date
        '            DateFrom = CDate((cbMonth.SelectedIndex + 1) & "-1-" & nupYear.Value)
        '            DateTo = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, CDate((cbMonth.SelectedIndex + 1) & "-1-" & nupYear.Value)))
        '            GenerateTB("By Book", DateFrom, DateTo, lvFilter.Items(index).SubItems(0).Text)
        '        ElseIf cbPeriod.SelectedItem = "Yearly" Then
        '            GenerateTB("By Book", "01-01-" & nupYear.Value, "12-31-" & nupYear.Value, lvFilter.Items(index).SubItems(0).Text)
        '        Else
        '            GenerateTB("By Book", dtpFrom.Value.Date, dtpTo.Value.Date, lvFilter.Items(index).SubItems(0).Text)
        '        End If
        '    Else
        '        MsgBox("Please select book first!", MsgBoxStyle.Exclamation)
        '    End If
        'ElseIf cbReportCategory.SelectedItem = "Balance Sheet" Then
        '    GenerateBS()
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("FSBS", UserName, dtpTo.Value.Date, cbBranch.SelectedItem)
        '    f.Dispose()
        'ElseIf cbReportCategory.SelectedItem = "Balance Sheet 2" Then
        '    GenerateBS()
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("FSBS2", UserName, dtpTo.Value.Date, cbBranch.SelectedItem)
        '    f.Dispose()
        'ElseIf cbReportCategory.SelectedItem = "Income Statement" Then

        '    GenerateIS("By Branch")
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("FSIS", UserName, dtpTo.Value.Date, cbBranch.SelectedItem)
        '    f.Dispose()

        '    'ElseIf cbReport.SelectedItem = "Income Statement" Then
        '    '    Dim f As New frmReport_Display

        '    '    f.ShowDialog("FSINCS", "", dtpFrom.Value.Date, dtpTo.Value.Date)
        '    '    f.Dispose()
        'ElseIf cbReportCategory.SelectedItem = "Subsidiary Ledger Schedule" Then
        '    GenerateSL()
        'ElseIf cbReportCategory.SelectedItem = "Subsidiary Ledger Column" Then
        '    GenerateSLC()
        'ElseIf cbReportCategory.SelectedItem = "Insurance Premium Payable" Then
        '    GenerateSLC()
        'ElseIf cbReportCategory.SelectedItem = "Daily Cash Position" Then
        '    GenerateDCPR("Daily", dtpFrom.Value.Date, dtpFrom.Value.Date)
        'ElseIf cbReportCategory.SelectedItem = "Account Schedule" Then
        '    GenerateGL()
        'ElseIf cbReportCategory.SelectedItem = "Aging Report" Then
        '    'GenerateAging(dtpFrom.Value.Date)
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("AR_AGING", "", dtpFrom.Value.Date, IIf(branch = "ALL", "", branch), "")
        '    f.Dispose()
        'ElseIf cbReportCategory.SelectedItem = "Loan Aging Report" Then
        '    'GenerateAging(dtpFrom.Value.Date)
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("LN_AGING", "", dtpFrom.Value.Date, IIf(branch = "ALL", "", branch), "")
        '    f.Dispose()
        '    'Threading.Thread.Sleep(2000)
        '    'Dim f As New frmReport_Display
        '    'f.rpt = "LN_AGING"
        '    'f.p1 = ""
        '    'f.p2 = dtpFrom.Value.Date
        '    'f.p3 = branch
        '    'f.Show()

        'ElseIf cbReportCategory.SelectedItem = "Loan Aging Report Per Loan Type" Then
        '    If cmbLoanType.SelectedIndex <> -1 Then
        '        ' GenerateAging(dtpFrom.Value.Date)
        '        Dim f As New frmReport_Display
        '        f.ShowDialog("LN_AGING", "", dtpFrom.Value.Date, IIf(branch = "ALL", "", branch), cmbLoanType.Text)
        '        'f.ShowDialog("LN_Aging_PerLoanType", "", dtpFrom.Value.Date, cmbLoanType.Text, branch)
        '        f.Dispose()
        '    Else
        '        MsgBox("Please select loan type!", MsgBoxStyle.Exclamation)
        '    End If
        'ElseIf cbReportCategory.SelectedItem = "Loan Release" Then
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("LN_Release", "Released", dtpFrom.Value.Date, dtpTo.Value.Date, branch)
        '    f.Dispose()
        'ElseIf cbReportCategory.SelectedItem = "CV Transaction" Then
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("CV_Transaction", dtpFrom.Value.Date, dtpTo.Value.Date)
        '    f.Dispose()
        'ElseIf cbReportCategory.SelectedItem = "OR Transaction" Then
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("OR_Transaction", dtpFrom.Value.Date, dtpTo.Value.Date)
        '    f.Dispose()
        'ElseIf cbReportCategory.SelectedItem = "Check Issuance" Then
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("Check_Issuance", dtpFrom.Value.Date, dtpTo.Value.Date)
        '    f.Dispose()
        'ElseIf cbReportCategory.SelectedItem = "Account Balances" Then
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("Account_Balances", dtpFrom.Value.Date, dtpTo.Value.Date, cmbLoanType.Text)
        '    f.Dispose()
        'ElseIf cbReportCategory.SelectedItem = "Loan Balances" Then
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("Loan_Balances", dtpFrom.Value.Date, cmbLoanType.Text)
        '    f.Dispose()
        'ElseIf cbReportCategory.SelectedItem = "Loan Title" Then
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("Loan_Title", dtpFrom.Value.Date, dtpTo.Value.Date)
        '    f.Dispose()

        'ElseIf cbReportCategory.SelectedItem = "Member List" Then
        '    Dim f As New frmReport_Display

        '    Dim index As Integer = -1
        '    For Each item As ListViewItem In lvFilter.Items
        '        If item.Checked = True Then
        '            index = item.Index
        '        End If
        '    Next
        '    If index <> -1 Then
        '        f.ShowDialog("MemberReport", dtpFrom.Value.Date, dtpTo.Value.Date, branch, lvFilter.Items(index).SubItems(0).Text)
        '    Else
        '        MsgBox("Please type first!", MsgBoxStyle.Exclamation)
        '    End If
        '    f.Dispose()
        'ElseIf cbReportCategory.SelectedItem = "Chart of Account" Then
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("COA_Master", Date.Today)
        '    f.Dispose()
        'ElseIf cbReportCategory.SelectedItem = "Loan Co-Maker List" Then
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("LN_CoMakerList", branch)
        '    f.Dispose()
        'ElseIf cbReportCategory.SelectedItem = "MAP" Then
        '    Dim f As New FrmSAWT
        '    f.ReportType = "MAP"
        '    f.DateFrom = dtpFrom.Value.Date
        '    f.DateTo = dtpTo.Value.Date
        '    f.Show()
        'ElseIf cbReportCategory.SelectedItem = "QAP" Then
        '    Dim f As New FrmSAWT
        '    f.ReportType = "QAP"
        '    f.DateFrom = dtpFrom.Value.Date
        '    f.DateTo = dtpTo.Value.Date
        '    f.Show()
        'ElseIf cbReportCategory.SelectedItem = "Cash Position Report" Then
        '    GenerateCPR()
        'ElseIf cbReportCategory.SelectedItem = "SAWT" Then
        '    Dim f As New FrmSAWT
        '    f.ReportType = "SAWT"
        '    f.DateFrom = dtpFrom.Value.Date
        '    f.DateTo = dtpTo.Value.Date
        '    f.Show()
        'ElseIf cbReportCategory.SelectedItem = "SLS" Then
        '    Dim f As New FrmSAWT
        '    f.ReportType = "SLS"
        '    f.DateFrom = dtpFrom.Value.Date
        '    f.DateTo = dtpTo.Value.Date
        '    f.Show()
        'ElseIf cbReportCategory.SelectedItem = "SLP" Then
        '    Dim f As New FrmSAWT
        '    f.ReportType = "SLP"
        '    f.DateFrom = dtpFrom.Value.Date
        '    f.DateTo = dtpTo.Value.Date
        '    f.Show()
        'ElseIf cbReportCategory.SelectedItem = "Purchase Journal" Then
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("Purchase_Journal", dtpFrom.Value.Date, dtpTo.Value.Date)
        '    f.Dispose()

        'ElseIf cbReportCategory.SelectedItem = "Sales Journal" Then
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("Sales_Journal", dtpFrom.Value.Date, dtpTo.Value.Date)
        '    f.Dispose()

        'ElseIf cbReportCategory.SelectedItem = "Cash Disbursement Journal" Then
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("CashDisbursement_Journal", dtpFrom.Value.Date, dtpTo.Value.Date)
        '    f.Dispose()

        'ElseIf cbReportCategory.SelectedItem = "Cash Receipts Journal" Then
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("CashReceipt_Journal", dtpFrom.Value.Date, dtpTo.Value.Date)
        '    f.Dispose()

        'ElseIf cbReportCategory.SelectedItem = "Accounts Payable Journal" Then
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("AccountsPayable_Journal", dtpFrom.Value.Date, dtpTo.Value.Date)
        '    f.Dispose()

        'End If


    End Sub


    Private Sub ClearCPRTable()
        dgvCPR.Rows.Clear()
        Dim deleteSQL As String
        deleteSQL = "DELETE FROM tblCPR "
        SQL.FlushParams()
        SQL.ExecNonQuery(deleteSQL)
    End Sub
    Private Sub InsertCPRtoTable()
        Dim insertSQL As String
        For Each row As DataGridViewRow In dgvCPR.Rows
            insertSQL = " INSERT INTO tblCPR(SortID, Bank, Amount) " &
                        " VALUES(@SortID, @Bank, @Amount)"
            SQL.FlushParams()
            SQL.AddParam("@SortID", row.Cells(0).Value)
            SQL.AddParam("@Bank", row.Cells(1).Value)
            SQL.AddParam("@Amount", row.Cells(2).Value)
            SQL.ExecNonQuery(insertSQL)
        Next
    End Sub

    Private Function GetBanks() As List(Of String)
        Dim banks As New List(Of String)
        Dim query As String
        query = "SELECT Bank FROM tblBank_Master WHERE Status ='Active' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            banks.Add(SQL.SQLDR("Bank").ToString)
        End While
        Return banks
    End Function


    Private Function CPR_CRB(DateFrom As Date, DateTo As Date, Bank As String) As Decimal
        Dim query As String
        query = " SELECT	 ISNULL(SUM(Debit-Credit),0) AS Amount " &
                " FROM	     view_GL INNER JOIN tblBank_Master " &
                " ON		 view_GL.accntCode = tblBank_Master.AccountCode " &
                " WHERE	     AppDate BETWEEN '" & DateFrom & "' AND '" & DateTo & "' AND Bank ='" & Bank & "' " &
                " AND        Book ='Cash Receipts' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() Then
            dgvCPR.Rows.Add({"2", Bank, CDec(SQL.SQLDR("Amount"))})
            Return CDec(SQL.SQLDR("Amount"))
        Else
            Return 0
        End If
    End Function
    Private Function CPR_JVD(DateFrom As Date, DateTo As Date, Bank As String) As Decimal
        Dim query As String
        query = " SELECT	 ISNULL(SUM(Debit-Credit),0) AS Amount " &
                " FROM	     view_GL INNER JOIN tblBank_Master " &
                " ON		 view_GL.accntCode = tblBank_Master.AccountCode " &
                " WHERE	     AppDate BETWEEN '" & DateFrom & "' AND '" & DateTo & "' AND RefType <>'BB' AND Bank ='" & Bank & "' " &
                " AND        Book NOT IN ('Cash Receipts','Cash Disbursements') AND Debit > 0 "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() Then
            dgvCPR.Rows.Add({"3", Bank, CDec(SQL.SQLDR("Amount"))})
            Return CDec(SQL.SQLDR("Amount"))
        Else
            Return 0
        End If
    End Function
    Private Function CPR_CDB(DateFrom As Date, DateTo As Date, Bank As String) As Decimal
        Dim query As String
        query = " SELECT	 ISNULL(SUM(Credit-Debit),0) AS Amount " &
                " FROM	     view_GL INNER JOIN tblBank_Master " &
                " ON		 view_GL.accntCode = tblBank_Master.AccountCode " &
                " WHERE	     AppDate BETWEEN '" & DateFrom & "' AND '" & DateTo & "' AND Bank ='" & Bank & "' " &
                " AND        Book ='Cash Disbursements' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() Then
            dgvCPR.Rows.Add({"5", Bank, CDec(SQL.SQLDR("Amount"))})
            Return CDec(SQL.SQLDR("Amount"))
        Else
            Return 0
        End If
    End Function


    Private Sub GenerateAging(ByVal DateFrom As String)
        Dim insertSQL As String
        insertSQL = " DELETE FROM tblFilter_LNAging " &
                    " INSERT INTO tblFilter_LNAging (Loan_No) " &
                    " SELECT Loan_No FROM tblLoan  " &
                    " WHERE DateLoan <= '" & DateFrom & "' " &
                    " AND Loan_No IN  " &
                    " ( " &
                    " SELECT REPLACE(RefNo, 'LN:', '')  " &
                    " FROM view_GL " &
                    " WHERE AccntCode IN (SELECT LoanAccount FROM tblLoan_Type WHERE LoanAccount <> '')  " &
                    " AND RefNo <> '' AND AppDate <= '" & DateFrom & "'  " & IIf(branch = "ALL", "AND BranchCode in ( SELECT    DISTINCT  tblBranch.BranchCode   " &
                    " FROM      tblBranch    " &
                    " INNER JOIN tblUser_Access    ON   " &
                    " tblBranch.BranchCode = tblUser_Access.Code    " &
                    " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " &
                    " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " &
                    " WHERE     UserID ='" & UserID & "'  )", " AND BranchCode = '" & branch & "' ") &
                    " GROUP BY REPLACE(RefNo, 'LN:', '')  " &
                    " HAVING SUM(Debit - Credit) > 0 " &
                    " ) "
        SQL.ExecNonQuery(insertSQL)

        'insertSQL = " DELETE tblview_LoanLedger " & _
        '    " INSERT INTO tblview_LoanLedger " & _
        '    " SELECT * FROM view_LoanLedger  " & _
        '    " WHERE Loan_NO IN (SELECT * FROM tblFilter_LNAging) "
        'SQL.ExecNonQuery(insertSQL)
    End Sub

    Private Sub SaveFilter()
        SQL.ExecNonQuery("DELETE FROM tblFilter")
        Dim insertSQL As String
        For Each item In lvFilter.Items
            insertSQL = " INSERT INTO tblFilter (Filter) " &
                        " VALUES(@Filter)"
            SQL.FlushParams()
            SQL.AddParam("@Filter", item.ToString)
            SQL.ExecNonQuery(insertSQL)
        Next
    End Sub
    Enum TotalLevel As Integer
        NotTotal = 0
        SubTotal = 1
        Total = 2
        GrandTotal = 3
    End Enum

    Private Sub GenerateNewIS()
        Dim dtRecord As New DataTable
        Dim Filter As String
        dtRecord.Columns.Add("Code", GetType(String))
        dtRecord.Columns.Add("Description", GetType(String))
        dtRecord.Columns.Add("Class", GetType(String))
        dtRecord.Columns.Add("Nature", GetType(String))
        dtRecord.Columns.Add("ShowTitle", GetType(Boolean))
        dtRecord.Columns.Add("ShowTotal", GetType(Boolean))
        dtRecord.Columns.Add("ContraAccount", GetType(Boolean))
        dtRecord.Columns.Add("Debit", GetType(Decimal))
        dtRecord.Columns.Add("Credit", GetType(Decimal))
        dtRecord.Columns.Add("CostCenter", GetType(String))
        Dim bool As Boolean = False
        Dim prevAccnt As String = ""
        Dim currentLevel As Integer = 1
        Dim isGroupBeforePosting As Boolean = False
        SQL.FlushParams()
        SQL.ExecNonQuery("DELETE FROM rptIS")
        ' GET ROOT ACCOUNT, THERE SHOULD ONLY BE ONE ROOT ACCOUNT PER ACCOUNT TYPE
        prevAccnt = GetRootAccount(currentLevel, dtRecord)

        'YEAR TO DATE
        SearchChildAccounts(prevAccnt, currentLevel, dtRecord, isGroupBeforePosting)
        InsertTotal(1, dtRecord, TotalLevel.GrandTotal)

        'FOR THE MONTH
        currentLevel = 1
        Filter = " AND AppDate BETWEEN CAST('" & dtpTo.Value.Month & "-01-" & dtpTo.Value.Year & "' AS DATE) AND '" & dtpTo.Value.Date & "' "
        SearchChildAccounts(prevAccnt, currentLevel, dtRecord, isGroupBeforePosting, False, Filter, "G1")
        UpdateTotal(1, dtRecord, "G1")

        SQL.FlushParams()
    End Sub

    Private Sub GenerateComparativeIS()
        Dim dtRecord As New DataTable
        Dim Filter As String
        dtRecord.Columns.Add("Code", GetType(String))
        dtRecord.Columns.Add("Description", GetType(String))
        dtRecord.Columns.Add("Class", GetType(String))
        dtRecord.Columns.Add("Nature", GetType(String))
        dtRecord.Columns.Add("ShowTitle", GetType(Boolean))
        dtRecord.Columns.Add("ShowTotal", GetType(Boolean))
        dtRecord.Columns.Add("ContraAccount", GetType(Boolean))
        dtRecord.Columns.Add("Debit", GetType(Decimal))
        dtRecord.Columns.Add("Credit", GetType(Decimal))
        Dim bool As Boolean = False
        Dim prevAccnt As String = ""
        Dim currentLevel As Integer = 1
        Dim isGroupBeforePosting As Boolean = False
        SQL.FlushParams()
        SQL.ExecNonQuery("DELETE FROM rptIS")
        ' GET ROOT ACCOUNT, THERE SHOULD ONLY BE ONE ROOT ACCOUNT PER ACCOUNT TYPE
        prevAccnt = GetRootAccount(currentLevel, dtRecord)

        'YEAR TO DATE
        SearchChildAccounts(prevAccnt, currentLevel, dtRecord, isGroupBeforePosting)
        InsertTotal(1, dtRecord, TotalLevel.GrandTotal)

        'FOR THE MONTH 
        For i As Integer = 1 To 12
            bgwIS.ReportProgress(i)
            dtRecord.Rows.Clear()
            currentLevel = 1
            Filter = " AND YEAR(AppDate) = '" & dtpTo.Value.Year & "'  AND MONTH(AppDate) = " & i & " "
            prevAccnt = GetRootAccount(currentLevel, dtRecord)
            SearchChildAccounts(prevAccnt, currentLevel, dtRecord, isGroupBeforePosting, False, Filter, "G" & i)
            UpdateTotal(1, dtRecord, "G" & i)
        Next

        SQL.FlushParams()
    End Sub

    Private Sub SearchChildAccounts(ByVal Parent As String, ByRef CurrentLevel As Integer, dt As DataTable, ByRef isGroupBeforePosting As Boolean,
                                    Optional ByVal isInsert As Boolean = True, Optional ByVal Filter As String = "", Optional ByVal Field As String = "")
        Dim query As String
        Dim dtList As New DataTable

        query = " SELECT    AccountCode, ReportAlias, Class, AccountNature, showTotal, showTitle, contraAccount, ChildID " &
                " FROM      tblCOA_Master  " &
                " WHERE     AccountType = 'Income Statement' AND Parent = @Parent  AND Status <>'Inactive' " &
                " ORDER BY  ChildID "
        SQL.FlushParams()
        SQL.AddParam("@Parent", Parent)
        SQL.GetQuery(query)
        dtList = SQL.SQLDS.Tables(0)
        If dtList.Rows.Count > 0 Then
            CurrentLevel += 1
            For Each row As DataRow In dtList.Rows
                Do While dt.Rows.Count < CurrentLevel
                    dt.Rows.Add()
                Loop
                dt.Rows(CurrentLevel - 1).Item("Code") = row.Item("AccountCode").ToString
                dt.Rows(CurrentLevel - 1).Item("Description") = row.Item("ReportAlias").ToString
                dt.Rows(CurrentLevel - 1).Item("Class") = row.Item("Class").ToString
                dt.Rows(CurrentLevel - 1).Item("Nature") = row.Item("AccountNature").ToString
                dt.Rows(CurrentLevel - 1).Item("ShowTitle") = row.Item("showTitle")
                dt.Rows(CurrentLevel - 1).Item("ShowTotal") = row.Item("showTotal")
                dt.Rows(CurrentLevel - 1).Item("ContraAccount") = row.Item("contraAccount")
                dt.Rows(CurrentLevel - 1).Item("Debit") = 0
                dt.Rows(CurrentLevel - 1).Item("Credit") = 0

                If dt.Rows(CurrentLevel - 1).Item("Class") = "Posting" Then
                    If isInsert Then
                        InsertPosting(CurrentLevel, dt)
                        isGroupBeforePosting = True
                    Else
                        UpdatePosting(CurrentLevel, dt, Filter, Field)
                    End If

                ElseIf dt.Rows(CurrentLevel - 1).Item("Class") = "Grouping" AndAlso dt.Rows(CurrentLevel - 1).Item("ShowTitle") = True Then
                    With dt.Rows(CurrentLevel - 1)
                        If isInsert Then
                            InsertToTable(.Item("Code"), .Item("Description"), 0, .Item("Class"), CurrentLevel, False)
                        End If
                    End With
                End If
                SearchChildAccounts(row.Item("AccountCode").ToString, CurrentLevel, dt, isGroupBeforePosting, isInsert, Filter, Field)
                If dt.Rows(CurrentLevel - 1).Item("Class") = "Grouping" AndAlso dt.Rows(CurrentLevel - 1).Item("ShowTotal") = True Then
                    Dim Total As TotalLevel
                    If isGroupBeforePosting Then
                        Total = TotalLevel.SubTotal
                        isGroupBeforePosting = False
                    Else
                        Total = TotalLevel.Total
                    End If
                    If isInsert Then
                        InsertTotal(CurrentLevel, dt, Total)
                    Else
                        UpdateTotal(CurrentLevel, dt, Field)
                    End If
                End If
            Next
            CurrentLevel -= 1
        End If
    End Sub
    Private Sub InsertTotal(ByRef Level As String, ByRef dt As DataTable, TotalLevel As TotalLevel, Optional CostCenter As String = "")
        Dim amount As Decimal = 0
        Dim code As String = dt.Rows(Level - 1).Item("Code")
        Dim desc As String = dt.Rows(Level - 1).Item("Description")
        If dt.Rows(Level - 1).Item("Nature") = "Debit" Then
            If Not IsDBNull(dt.Rows(Level - 1).Item("Debit")) AndAlso Not IsDBNull(dt.Rows(Level - 1).Item("Credit")) Then
                amount = dt.Rows(Level - 1).Item("Debit") - dt.Rows(Level - 1).Item("Credit")
            Else
                amount = 0
            End If

        Else
            If Not IsDBNull(dt.Rows(Level - 1).Item("Debit")) AndAlso Not IsDBNull(dt.Rows(Level - 1).Item("Credit")) Then
                amount = dt.Rows(Level - 1).Item("Credit") - dt.Rows(Level - 1).Item("Debit")
            Else
                amount = 0
            End If
        End If
        If dt.Rows(Level - 1).Item("ShowTitle") = True Then
            desc = "TOTAL " & desc
        End If

        InsertToTable(code, desc, amount, "Total", Level, TotalLevel, CostCenter)
        dt.Rows(Level - 1).Item("Debit") = 0
        dt.Rows(Level - 1).Item("Credit") = 0
        'Dim amount As Decimal = 0
        'Dim code As String = dt.Rows(Level - 1).Item("Code")
        'Dim desc As String = dt.Rows(Level - 1).Item("Description")
        'If dt.Rows(Level - 1).Item("Nature") = "Debit" Then
        '    amount = dt.Rows(Level - 1).Item("Debit") - dt.Rows(Level - 1).Item("Credit")
        'Else
        '    amount = dt.Rows(Level - 1).Item("Credit") - dt.Rows(Level - 1).Item("Debit")
        'End If
        'If dt.Rows(Level - 1).Item("ShowTitle") = True Then
        '    desc = "TOTAL " & desc
        'End If

        'InsertToTable(code, desc, amount, "Total", Level, TotalLevel)
        'dt.Rows(Level - 1).Item("Debit") = 0
        'dt.Rows(Level - 1).Item("Credit") = 0
    End Sub
    Private Sub UpdateTotal(ByRef Level As String, ByRef dt As DataTable, Column As String)
        Dim amount As Decimal = 0
        Dim code As String = dt.Rows(Level - 1).Item("Code")
        If dt.Rows(Level - 1).Item("Nature") = "Debit" Then
            amount = dt.Rows(Level - 1).Item("Debit") - dt.Rows(Level - 1).Item("Credit")
        Else
            amount = dt.Rows(Level - 1).Item("Credit") - dt.Rows(Level - 1).Item("Debit")
        End If
        UpdateTable(code, amount, Column)
        dt.Rows(Level - 1).Item("Debit") = 0
        dt.Rows(Level - 1).Item("Credit") = 0
    End Sub
    Private Sub UpdateTable(Code As String, Amt As Decimal, Column As String)
        Dim updateSQL As String
        updateSQL = " UPDATE rptIS " &
                    " SET    " & Column & " = @Amount " &
                    " WHERE AccntCode = @AccntCode AND ( Class ='Posting' OR  Class ='Total') "
        SQL.FlushParams()
        SQL.AddParam("@AccntCode", Code)
        SQL.AddParam("@Amount", Amt)
        SQL.ExecNonQuery(updateSQL)
    End Sub
    Private Sub InsertToTable(Code As String, Desc As String, Amt As Decimal, AccountClass As String, Level As String, TotalLevel As TotalLevel, Optional CostCenter As String = "")
        Dim insertSQl As String
        For i As Integer = 1 To Level
            Desc = Tab & Desc
        Next
        insertSQl = " INSERT INTO " &
                        " rptIS(AccntCode, Description, Amount, Class, TotalLevel, CostCenter) " &
                        " VALUES(@AccntCode, @Description, @Amount, @Class, @TotalLevel, @CostCenter)"
        SQL.FlushParams()
        SQL.AddParam("@AccntCode", Code)
        SQL.AddParam("@Description", Desc)
        SQL.AddParam("@Amount", Amt)
        SQL.AddParam("@Class", AccountClass)
        SQL.AddParam("@TotalLevel", CInt(TotalLevel))
        SQL.AddParam("@CostCenter", CostCenter)
        SQL.ExecNonQuery(insertSQl)
    End Sub
    Private Sub InsertPosting(ByRef Level As String, ByRef dt As DataTable)
        Dim query As String
        Dim debit As Decimal = 0
        Dim credit As Decimal = 0
        Dim code As String = dt.Rows(Level - 1).Item("Code")
        Dim desc As String = dt.Rows(Level - 1).Item("Description")




        query = " 	SELECT    AccntCode, AccntTitle, " & vbCrLf &
                "             CASE WHEN SUM(Debit) > SUM(Credit) THEN SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit, " & vbCrLf &
                "             CASE WHEN SUM(Credit) > SUM(Debit) THEN SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit " & vbCrLf &
                " 	FROM      view_GL  " & vbCrLf &
                " 	WHERE     AccntCode = @AccntCode AND Status <> 'Cancelled' AND Book <> 'PEC'" & vbCrLf &
                "   AND       AppDate BETWEEN CAST('01-01-" & dtpFrom.Value.Year & "' AS DATE) AND '" & dtpTo.Value.Date & "' " & CostCenterFilter & vbCrLf &
                " 	GROUP BY  AccntCode, AccntTitle " & vbCrLf &
                "   HAVING    SUM(Debit) <> SUM(Credit) "
        SQL.FlushParams()
        SQL.AddParam("@AccntCode", code)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            If dt.Rows(Level - 1).Item("Nature") = "Debit" Then
                debit = CDec(SQL.SQLDR("Debit")) - CDec(SQL.SQLDR("Credit"))
            Else
                credit = CDec(SQL.SQLDR("Credit")) - CDec(SQL.SQLDR("Debit"))
            End If
            InsertToTable(code, code & "-" & desc, debit + credit, dt.Rows(Level - 1).Item("Class"), Level, TotalLevel.NotTotal)

            For i As Integer = Level - 1 To 1 Step -1
                If IsDBNull(dt.Rows(i - 1).Item("Debit")) Then
                    dt.Rows(i - 1).Item("Debit") = debit
                Else
                    dt.Rows(i - 1).Item("Debit") += debit
                End If
                If IsDBNull(dt.Rows(i - 1).Item("Credit")) Then
                    dt.Rows(i - 1).Item("Credit") = credit
                Else
                    dt.Rows(i - 1).Item("Credit") += credit
                End If
            Next
        End If

    End Sub
    Private Sub UpdatePosting(ByRef Level As String, ByRef dt As DataTable, filter As String, column As String)
        Dim query As String
        Dim debit As Decimal = 0
        Dim credit As Decimal = 0
        Dim code As String = dt.Rows(Level - 1).Item("Code")
        Dim desc As String = dt.Rows(Level - 1).Item("Description")

        query = " 	SELECT    AccntCode, AccntTitle, " & vbCrLf &
                "             CASE WHEN SUM(Debit) > SUM(Credit) THEN SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit, " & vbCrLf &
                "             CASE WHEN SUM(Credit) > SUM(Debit) THEN SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit " & vbCrLf &
                " 	FROM      view_GL  " & vbCrLf &
                " 	WHERE     AccntCode = @AccntCode AND Status <> 'Cancelled'  AND Book <> 'PEC'" & filter & CostCenterFilter & vbCrLf &
                " 	GROUP BY  AccntCode, AccntTitle " & vbCrLf &
                "   HAVING    SUM(Debit) <> SUM(Credit) "
        SQL.FlushParams()
        SQL.AddParam("@AccntCode", code)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            If dt.Rows(Level - 1).Item("Nature") = "Debit" Then
                debit = CDec(SQL.SQLDR("Debit")) - CDec(SQL.SQLDR("Credit"))
            Else
                credit = CDec(SQL.SQLDR("Credit")) - CDec(SQL.SQLDR("Debit"))
            End If
            UpdateTable(code, debit + credit, column)

            For i As Integer = Level - 1 To 1 Step -1
                If IsDBNull(dt.Rows(i - 1).Item("Debit")) Then
                    dt.Rows(i - 1).Item("Debit") = debit
                Else
                    dt.Rows(i - 1).Item("Debit") += debit
                End If
                If IsDBNull(dt.Rows(i - 1).Item("Credit")) Then
                    dt.Rows(i - 1).Item("Credit") = credit
                Else
                    dt.Rows(i - 1).Item("Credit") += credit
                End If
            Next
        End If

    End Sub


    Private Function GetRootAccount(ByRef Level As Integer, ByRef dt As DataTable) As String
        Dim query As String
        query = " SELECT    AccountCode, ReportAlias, Class, AccountNature, showTotal, showTitle, contraAccount " &
                " FROM      tblCOA_Master  " &
                " WHERE     AccountType = 'Income Statement' AND ISNULL(Parent,'') ='' AND Status <>'Inactive' "
        SQL.FlushParams()
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            ' FIRST ROW
            Do While dt.Rows.Count < Level
                dt.Rows.Add()
            Loop
            dt.Rows(Level - 1).Item("Code") = SQL.SQLDR("AccountCode").ToString
            dt.Rows(Level - 1).Item("Description") = SQL.SQLDR("ReportAlias").ToString
            dt.Rows(Level - 1).Item("Class") = SQL.SQLDR("Class").ToString
            dt.Rows(Level - 1).Item("Nature") = SQL.SQLDR("AccountNature").ToString
            dt.Rows(Level - 1).Item("ShowTitle") = SQL.SQLDR("showTitle")
            dt.Rows(Level - 1).Item("ShowTotal") = SQL.SQLDR("showTotal")
            dt.Rows(Level - 1).Item("ContraAccount") = SQL.SQLDR("contraAccount")
            Return SQL.SQLDR("AccountCode").ToString
        Else
            Return ""
        End If
    End Function


    Private Sub GenerateIS(ByVal Type As String)
        Dim deleteSQl As String
        deleteSQl = " DELETE FROM rptBS "
        SQL.ExecNonQuery(deleteSQl)
        Dim dt As New DataTable
        Dim query As String
        Dim desc As String
        Dim AccountCode As String
        Dim groupID As Integer = 0
        Dim debit As Decimal = 0
        Dim credit As Decimal = 0
        Dim valueMonth As Decimal = 0
        Dim valueBudget As Decimal = 0
        Dim nature As String = ""
        Dim Parent As String
        Dim totalDesc(7) As String
        Dim TotalParent(7) As String
        Dim totalCR(7) As Decimal
        Dim totalDR(7) As Decimal
        Dim TotalNature(7) As String
        Dim insertSQl As String
        Dim prevID As Integer = 0
        Dim recID As Integer = 1
        Dim incre As Integer = 1
        Dim Filter As String = ""

        query = " SELECT  CASE WHEN AccountGroup ='SubAccount' THEN tblCOA_Master.AccountCode  + ' - ' + CASE WHEN ISNULL(ReportAlias,'') ='' THEN AccountTitle ELSE ReportAlias END " & vbCrLf &
                " 			 ELSE CASE WHEN ReportAlias ='' THEN AccountTitle ELSE ReportAlias END " & vbCrLf &
                " 		END  AS Descrition," & vbCrLf &
                "  CASE	WHEN AccountGroup = 'Group' THEN 'G1'		WHEN AccountGroup = 'SubGroup' THEN 'G2' " & vbCrLf &
                " 		WHEN AccountGroup = 'Category' THEN 'G3'	WHEN AccountGroup = 'SubCategory' THEN 'G4' " & vbCrLf &
                " 		WHEN AccountGroup = 'CostCenter' THEN 'G5'	WHEN AccountGroup = 'MainAccount' THEN 'G6' " & vbCrLf &
                " 		WHEN AccountGroup = 'SubAccount' THEN 'G7' " & vbCrLf &
                "  ELSE ''	END AS AccountGroup, " & vbCrLf &
                " 	   CASE WHEN SUM(ISNULL(Debit,0))  > SUM(ISNULL(Credit,0))   " & vbCrLf &
                " 	        THEN SUM(ISNULL(Debit,0))  - SUM(ISNULL(Credit,0)) " & vbCrLf &
                " 			ELSE 0 " & vbCrLf &
                " 	   END AS Debit," & vbCrLf &
                " 	   CASE WHEN SUM(ISNULL(Credit,0))  > SUM(ISNULL(Debit,0))  " & vbCrLf &
                " 	        THEN SUM(ISNULL(Credit,0))  - SUM(ISNULL(Debit,0)) " & vbCrLf &
                " 			ELSE 0 " & vbCrLf &
                " 	   END AS Credit," & vbCrLf &
                "      showTotal, contraAccount, " & vbCrLf &
                " 	   CASE WHEN AccountNature = 'Debit' THEN SUM(ISNULL(MonthDebit,0) - ISNULL(MonthCredit,0))  " & vbCrLf &
                " 	        WHEN AccountNature = 'Credit' THEN SUM(ISNULL(MonthCredit,0) - ISNULL(MonthDebit,0)) " & vbCrLf &
                " 			ELSE 0 " & vbCrLf &
                " 	   END AS MonthAmount," & vbCrLf &
                " 	   tblCOA_Master.AccountCode," & vbCrLf &
                "      ISNULL(Budget.Amount,0) as BudgetAmount, Parent, AccountNature, showTitle " & vbCrLf &
                " FROM tblCOA_Master " & vbCrLf &
                " LEFT JOIN " & vbCrLf &
                " ( " & vbCrLf &
                " 	SELECT    AccntCode, AccntTitle, SUM(Debit) AS Debit, SUM(Credit)  AS Credit " & vbCrLf &
                " 	FROM      view_GL  " & vbCrLf &
                " 	WHERE     AppDate BETWEEN CAST('01-01-" & dtpFrom.Value.Year & "' AS DATE) AND '" & dtpTo.Value.Date & "' " &
                "   AND       Status <> 'Cancelled' " & Filter & vbCrLf &
                " 	GROUP BY  AccntCode, AccntTitle " & vbCrLf &
                " ) AS TB " & vbCrLf &
                " ON  tblCOA_Master.AccountCode = TB.AccntCode " & vbCrLf &
                " LEFT JOIN " & vbCrLf &
                " ( " & vbCrLf &
                " 	SELECT    AccntCode, AccntTitle, SUM(Debit) AS MonthDebit, SUM(Credit)  AS MonthCredit " & vbCrLf &
                " 	FROM      view_GL  " & vbCrLf &
                " 	WHERE     AppDate BETWEEN CAST('" & dtpTo.Value.Month & "-01-" & dtpFrom.Value.Year & "' AS DATE) AND '" & dtpTo.Value.Date & "' " &
                "   AND       Status <> 'Cancelled' " & Filter & vbCrLf &
                " 	GROUP BY  AccntCode, AccntTitle " & vbCrLf &
                " ) AS MB " & vbCrLf &
                " ON  tblCOA_Master.AccountCode = MB.AccntCode " & vbCrLf &
                " LEFT JOIN  " & vbCrLf &
                "  (  " & vbCrLf &
                "  	SELECT    AccountCode, Amount " & vbCrLf &
                "  	FROM      tblBudget   " & vbCrLf &
                "  	WHERE     YEAR(AppDate) = '" & dtpFrom.Value.Year & "'  " & vbCrLf &
                "  ) AS Budget  " & vbCrLf &
                "  ON  tblCOA_Master.AccountCode = Budget.AccountCode " & vbCrLf &
                " WHERE  AccountType ='Income Statement' " & vbCrLf &
                " GROUP BY tblCOA_Master.AccountCode, AccountTitle, ReportAlias, AccountGroup, AccountNature, " &
                "         showTotal, OrderNo, contraAccount, Budget.Amount, Parent, showTitle " & vbCrLf &
                " HAVING  (AccountGroup <> 'SubAccount' OR " & vbCrLf &
                "        (AccountGroup = 'SubAccount'  AND " & vbCrLf &
                "        (CASE WHEN AccountNature = 'Debit' THEN SUM(ISNULL(Debit,0) - ISNULL(Credit,0))  " & vbCrLf &
                " 	        WHEN AccountNature = 'Credit' THEN SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) " & vbCrLf &
                " 			ELSE 0 " & vbCrLf &
                " 	   END) <> 0)) " & vbCrLf &
                " ORDER BY OrderNo "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                desc = row(0).ToString
                groupID = CInt(row(1).ToString.Replace("G", ""))
                debit = row(2)
                credit = row(3)
                valueMonth = row(6)
                Parent = row(9).ToString
                nature = row(10).ToString
                If row(4) = True Then
                    totalDesc(groupID) = desc
                End If
                If row(5) = True Then
                    debit = debit * -1
                    credit = credit * -1
                    valueMonth = valueMonth * -1
                End If
                AccountCode = row(6).ToString
                valueBudget = row(7)
                If groupID <> prevID Or groupID = 7 Then

                    If prevID > groupID Then
                        If prevID <> 7 Then
                            For i As Integer = incre - 1 To 0 Step -1
                                deleteSQl = " DELETE FROM rptBS WHERE RecordID = '" & recID - 1 & "' "
                                SQL.ExecNonQuery(deleteSQl)
                                recID -= 1
                            Next

                            incre = 0
                        Else
                            incre = 0
                        End If
                        For i As Integer = 7 To 1 Step -1
                            If groupID <= i Then
                                If Not IsNothing(totalDesc(i)) AndAlso totalDesc(i) <> "" Then
                                    If totalCR(i) <> 0 Then
                                        insertSQl = " INSERT INTO " &
                                               " rptBS(RecordID, Description, Amount, AmountMonth, GroupID, AccountCode, AmountBudget) " &
                                               " VALUES(@RecordID, @Description, @Amount, @AmountMonth, @GroupID, @AccountCode, @AmountBudget)"
                                        SQL.FlushParams()
                                        SQL.AddParam("@RecordID", recID)
                                        SQL.AddParam("@Description", "TOTAL " & totalDesc(i))
                                        SQL.AddParam("@Amount", totalCR(i))
                                        SQL.AddParam("@AmountMonth", totalCR(i))
                                        SQL.AddParam("@GroupID", i)
                                        SQL.AddParam("@AccountCode", totalCR(i))
                                        SQL.AddParam("@AmountBudget", totalCR(i))
                                        SQL.ExecNonQuery(insertSQl)
                                        incre = 0
                                        totalDesc(i) = Nothing
                                        totalDR(i) = Nothing
                                        totalCR(i) = Nothing
                                        recID += 1
                                    End If

                                End If
                            End If

                        Next
                    End If
                    insertSQl = " INSERT INTO " &
                        " rptBS(RecordID, Description, Amount, AmountMonth, GroupID, AccountCode, AmountBudget) " &
                        " VALUES(@RecordID, @Description, @Amount, @AmountMonth, @GroupID, @AccountCode, @AmountBudget)"
                    SQL.FlushParams()
                    SQL.AddParam("@RecordID", recID)
                    SQL.AddParam("@Description", desc)
                    SQL.AddParam("@Amount", debit)
                    SQL.AddParam("@AmountMonth", valueMonth)
                    SQL.AddParam("@GroupID", groupID)
                    SQL.AddParam("@AccountCode", AccountCode)
                    SQL.AddParam("@AmountBudget", valueBudget)
                    SQL.ExecNonQuery(insertSQl)
                    prevID = groupID
                    recID += 1
                    incre += 1
                    If debit <> 0 Or credit <> 0 Then
                        For i As Integer = 1 To 7
                            If Not IsNothing(totalDesc(i)) AndAlso totalDesc(i) <> "" Then
                                If TotalNature(i) = "Debit" Then
                                    totalDR(i) += debit
                                ElseIf TotalNature(i) = "Credit" Then
                                    totalCR(i) += credit
                                End If
                            End If
                        Next
                    End If

                End If
                TotalParent(groupID) = Parent
                TotalNature(groupID) = nature
            Next

            If prevID <> 7 Then
                For i As Integer = incre - 1 To 0 Step -1
                    deleteSQl = " DELETE FROM rptBS WHERE RecordID = '" & recID - 1 & "' "
                    SQL.ExecNonQuery(deleteSQl)
                    recID -= 1
                Next
            End If
            For i As Integer = 7 To 1 Step -1
                If Not IsNothing(totalDesc(i)) AndAlso totalDesc(i) <> "" Then
                    If totalCR(i) <> 0 Then
                        insertSQl = " INSERT INTO " &
                               " rptBS(RecordID, Description, Amount, GroupID, AccountCode, AmountBudget) " &
                               " VALUES(@RecordID, @Description, @Amount, @GroupID, @AccountCode, @AmountBudget)"
                        SQL.FlushParams()
                        SQL.AddParam("@RecordID", recID)
                        SQL.AddParam("@Description", "TOTAL " & totalDesc(i))
                        SQL.AddParam("@Amount", totalCR(i))
                        SQL.AddParam("@GroupID", i)
                        SQL.AddParam("@AccountCode", totalCR(i))
                        SQL.AddParam("@AmountBudget", totalCR(i))
                        SQL.ExecNonQuery(insertSQl)
                        incre = 0
                        totalDesc(i) = Nothing
                        totalCR(i) = Nothing
                        recID += 1
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub GenerateISbyBranch()
        Dim deleteSQl As String
        deleteSQl = " DELETE FROM rptIS_Header "
        SQL.ExecNonQuery(deleteSQl)


        Dim Code As String
        Dim desc As String
        Dim nature As String
        Dim groupID As Integer = 0
        Dim incre As Integer = 1
        Dim prevID As Integer = 0
        Dim prevTotalLabel As String
        Dim prevNature As String
        Dim recID As Integer = 1
        Dim showTitle As Boolean = True
        Dim sortID As Integer = 0
        Dim totalCR(7) As Decimal
        Dim totalDB(7) As Decimal
        Dim totalDBCR(7) As String
        Dim totalDesc(7) As String
        Dim totalCode(7) As String
        Dim value As Decimal = 0
        Dim valueCR As Decimal = 0
        Dim valueDB As Decimal = 0
        Dim dt As DataTable

        Dim insertSQL As String
        insertSQL = "  INSERT INTO  " &
                "  rptIS_Header(Type, Description, Param1) " &
                "  SELECT 'G' +  CAST(ROW_NUMBER() OVER (ORDER BY SortID) AS nvarchar) AS Type, Description, BranchCode " &
                "  FROM   tblBranch " &
                "  WHERE  BranchCode IN  " &
                "  (  " &
                "  	SELECT    BranchCode  " &
                "  	FROM      view_GL   " &
                " 	WHERE     AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " &
                "  )  "
        SQL.ExecNonQuery(insertSQL)

        Dim query As String
        query = " SELECT Type, Description, Param1 FROM rptIS_Header "
        SQL.GetQuery(query)
        dt = SQL.SQLDS.Tables(0)
        If dt.Rows.Count > 0 Then
            For Each row1 As DataRow In dt.Rows
                prevID = 0
                incre = 1
                groupID = 0
                recID = 1
                sortID = 1
                query = " SELECT  CASE WHEN AccountGroup ='SubAccount' THEN AccountCode  + ' - ' + CASE WHEN ISNULL(ReportAlias,'') ='' THEN AccountTitle ELSE ReportAlias END " &
                         " 			 ELSE CASE WHEN ReportAlias ='' THEN AccountTitle ELSE ReportAlias END " &
                         " 		END  AS Descrition,  " &
                         "  CASE	WHEN AccountGroup = 'Group' THEN 'G1'		WHEN AccountGroup = 'SubGroup' THEN 'G2' " &
                         " 		WHEN AccountGroup = 'Category' THEN 'G3'	WHEN AccountGroup = 'SubCategory' THEN 'G4' " &
                         " 		WHEN AccountGroup = 'CostCenter' THEN 'G5'	WHEN AccountGroup = 'MainAccount' THEN 'G6' " &
                         " 		WHEN AccountGroup = 'SubAccount' THEN 'G7' " &
                         "  ELSE ''	END AS AccountGroup, " &
                         " 	   CASE WHEN AccountNature = 'Debit' THEN SUM(ISNULL(Debit,0) - ISNULL(Credit,0))  " &
                         " 	        WHEN AccountNature = 'Credit' THEN SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) " &
                         " 			ELSE 0 " &
                         " 	   END AS Amount, showTotal, contraAccount, " &
                         "       SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) AS TotalDebit, " &
                         "       SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) AS TotalCredit, " &
                         "       AccountNature, showTitle, ISNULL(OrderNo,0) AS OrderNo, AccountCode " &
                         " FROM tblCOA_Master " &
                         " LEFT JOIN " &
                         " ( " &
                         " 	SELECT    AccntCode, AccntTitle, SUM(Debit) AS Debit, SUM(Credit)  AS Credit " &
                         " 	FROM      view_GL  " &
                         " 	WHERE     AppDate BETWEEN'" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " &
                         "  AND       BranchCode = '" & row1(2).ToString & "' " &
                         " 	GROUP BY  AccntCode, AccntTitle " &
                         " ) AS TB " &
                         " ON  tblCOA_Master.AccountCode = TB.AccntCode " &
                         " WHERE  AccountType ='Income Statement' " &
                         " GROUP BY AccountCode, AccountTitle, ReportAlias, AccountGroup, AccountNature, showTotal, OrderNo, contraAccount, showTitle " &
                         " HAVING  (AccountGroup <> 'SubAccount' OR " &
                         "        (AccountGroup = 'SubAccount'  AND " &
                         "        (CASE WHEN AccountNature = 'Debit' THEN SUM(ISNULL(Debit,0) - ISNULL(Credit,0))  " &
                         " 	        WHEN AccountNature = 'Credit' THEN SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) " &
                         " 			ELSE 0 " &
                         " 	   END) <> 0)) " &
                         " ORDER BY OrderNo "
                SQL.GetQuery(query)
                If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In SQL.SQLDS.Tables(0).Rows

                        desc = row(0).ToString
                        groupID = CInt(row(1).ToString.Replace("G", ""))
                        value = row(2)
                        valueDB = row(5)
                        valueCR = row(6)
                        showTitle = row(8)
                        sortID = row(9)
                        nature = row(7)
                        Code = row(10)

                        If groupID <> prevID Or groupID = 7 Then
                            If prevID > groupID Then
                                If prevID <> 7 Then
                                    For i As Integer = incre - 1 To 0 Step -1
                                        recID -= 1
                                    Next

                                    incre = 0
                                    For i As Integer = groupID + 1 To 7
                                        totalDesc(i) = Nothing
                                    Next
                                Else
                                    incre = 0
                                End If

                                For i As Integer = 6 To 1 Step -1
                                    If groupID <= i Then
                                        If Not IsNothing(totalDesc(i)) AndAlso totalDesc(i) <> "" Then
                                            If totalCR(i) <> 0 Then

                                                insertSQL = " UPDATE rptIS  " &
                                                              " SET    " & row1(0).ToString & " = @Amount " &
                                                              " WHERE  RecordID = @RecordID AND Description = @Description AND GroupID = @GroupID "
                                                SQL.FlushParams()
                                                SQL.AddParam("@Description", "TOTAL " & totalDesc(i))
                                                If totalDBCR(i) = "Credit" Then
                                                    SQL.AddParam("@Amount", totalCR(i))
                                                Else
                                                    SQL.AddParam("@Amount", totalDB(i))
                                                End If
                                                SQL.AddParam("@GroupID", i)
                                                SQL.AddParam("@RecordID", totalCode(i))
                                                SQL.ExecNonQuery(insertSQL)
                                                incre = 0
                                                totalDesc(i) = Nothing
                                                totalCR(i) = Nothing
                                                totalDB(i) = Nothing
                                                totalDBCR(i) = Nothing
                                                totalCode(i) = Nothing
                                                If i = groupID Then
                                                    totalDesc(i) = desc
                                                    totalCode(i) = Code
                                                End If
                                                recID += 1

                                                'If groupID = i Then
                                                '    insertSQL = " UPDATE rptIS " & _
                                                '                " SET    " & row1(0).ToString & " = @Amount " & _
                                                '                " WHERE  SortID = @SortID AND Description = @Description AND GroupID = @GroupID "
                                                '    SQL.FlushParams()
                                                '    If IsNothing(prevTotalLabel) Then SQL.AddParam("@Description", "TOTAL " & totalDesc(i)) Else SQL.AddParam("@Description", "TOTAL " & prevTotalLabel)
                                                '    If totalDBCR(i) = "Credit" Then
                                                '        SQL.AddParam("@Amount", totalCR(i))
                                                '    Else
                                                '        SQL.AddParam("@Amount", totalDB(i))
                                                '    End If
                                                '    SQL.AddParam("@GroupID", i)
                                                '    SQL.AddParam("@SortID", sortID)
                                                '    SQL.ExecNonQuery(insertSQL)
                                                '    incre = 0
                                                '    If IsNothing(prevTotalLabel) Then totalDesc(i) = Nothing Else prevTotalLabel = Nothing
                                                '    totalCR(i) = Nothing
                                                '    totalDB(i) = Nothing
                                                '    totalDBCR(i) = Nothing
                                                '    recID += 1
                                                'Else
                                                '    insertSQL = " UPDATE rptIS  " & _
                                                '                " SET    " & row1(0).ToString & " = @Amount " & _
                                                '                " WHERE  SortID = @SortID AND Description = @Description AND GroupID = @GroupID "
                                                '    SQL.FlushParams()
                                                '    SQL.AddParam("@Description", "TOTAL " & totalDesc(i))
                                                '    If totalDBCR(i) = "Credit" Then
                                                '        SQL.AddParam("@Amount", totalCR(i))
                                                '    Else
                                                '        SQL.AddParam("@Amount", totalDB(i))
                                                '    End If
                                                '    SQL.AddParam("@GroupID", i)
                                                '    SQL.AddParam("@SortID", sortID)
                                                '    SQL.ExecNonQuery(insertSQL)
                                                '    incre = 0
                                                '    totalDesc(i) = Nothing
                                                '    totalCR(i) = Nothing
                                                '    totalDB(i) = Nothing
                                                '    totalDBCR(i) = Nothing
                                                '    recID += 1
                                                'End If

                                            End If

                                        End If
                                    End If

                                Next
                            Else
                                If row(3) = True Then
                                    If Not IsNothing(totalDesc(groupID)) AndAlso totalDesc(groupID) <> desc Then
                                        prevTotalLabel = totalDesc(groupID)
                                    End If
                                    totalDesc(groupID) = desc
                                    totalCode(groupID) = Code
                                    If Not IsNothing(totalDBCR(groupID)) AndAlso totalDBCR(groupID) <> nature Then
                                        prevNature = totalDBCR(groupID)
                                    End If
                                    totalDBCR(groupID) = nature
                                End If
                            End If
                        End If
                        If showTitle = True Then
                            If groupID = "7" And value <> 0 Then
                                insertSQL = " UPDATE rptIS  " &
                                       " SET    " & row1(0).ToString & " = @Amount " &
                                       " WHERE  RecordID = @RecordID AND Description = @Description AND GroupID = @GroupID "
                                SQL.FlushParams()
                                SQL.AddParam("@Description", desc)
                                SQL.AddParam("@Amount", value)
                                SQL.AddParam("@GroupID", groupID)
                                SQL.AddParam("@RecordID", Code)
                                SQL.ExecNonQuery(insertSQL)
                            End If

                        End If

                        prevID = groupID
                        recID += 1
                        incre += 1
                        If value <> 0 Then
                            For i As Integer = 1 To 7
                                If Not IsNothing(totalDesc(i)) AndAlso totalDesc(i) <> "" Then
                                    totalCR(i) += valueCR
                                    totalDB(i) += valueDB

                                    'If IsNothing(totalDBCR(i)) Then
                                    '    totalDBCR(i) = row(7)
                                    'End If
                                End If
                            Next
                        End If

                    Next
                    If prevID <> 7 Then
                        For i As Integer = incre - 1 To 0 Step -1

                            recID -= 1
                        Next
                    End If

                    For i As Integer = 6 To 1 Step -1
                        If Not IsNothing(totalDesc(i)) AndAlso totalDesc(i) <> "" Then
                            If totalCR(i) <> 0 Then
                                insertSQL = " UPDATE rptIS  " &
                                            " SET    " & row1(0).ToString & " = @Amount " &
                                            " WHERE  RecordID = @RecordID AND Description = @Description AND GroupID = @GroupID "
                                SQL.FlushParams()
                                SQL.AddParam("@RecordID", totalCode(i))
                                SQL.AddParam("@Description", "TOTAL " & totalDesc(i))
                                If totalDBCR(i) = "Credit" Then
                                    SQL.AddParam("@Amount", totalCR(i))
                                Else
                                    SQL.AddParam("@Amount", totalDB(i))
                                End If
                                SQL.AddParam("@GroupID", i)
                                SQL.ExecNonQuery(insertSQL)
                                incre = 0
                                totalDesc(i) = Nothing
                                totalCR(i) = Nothing
                                totalDB(i) = Nothing
                                totalDBCR(i) = Nothing
                                recID += 1
                            End If
                        End If
                    Next
                End If

            Next
        End If
    End Sub


    Private Sub GenerateSL()
        Dim i As Integer = 1
        Dim insertSQl As String
        Dim deleteSQL As String
        deleteSQL = " DELETE FROM tblSL_PrintH  "
        SQL.ExecNonQuery(deleteSQL)
        deleteSQL = " DELETE FROM tblSL_Print "
        SQL.ExecNonQuery(deleteSQL)
        insertSQl = " INSERT INTO tblSL_Print(VCECode, RefID) " &
                    " SELECT VCECode,  REPLACE(RefNo, 'LN:', '') as RefNo" &
                    " FROM   view_GL INNER JOIN tblCOA_Master " &
                    " ON     view_GL.AccntCode = tblCOA_Master.AccountCode " &
                    " WHERE  AppDate BETWEEN CAST('01-01-" & dtpFrom.Value.Year & "' AS DATE) AND '" & dtpTo.Value.Date & "'  " &
                    " AND    WithSubsidiary = 1 AND VCECode <> '' " & IIf(branch = "ALL", "AND BranchCode in ( SELECT    DISTINCT  tblBranch.BranchCode   " &
                    " FROM      tblBranch    " &
                    " INNER JOIN tblUser_Access    ON   " &
                    " tblBranch.BranchCode = tblUser_Access.Code    " &
                    " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " &
                    " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " &
                    " WHERE     UserID ='" & UserID & "'  )", " AND BranchCode = '" & branch & "' ") &
                    " GROUP BY VCECode,  REPLACE(RefNo, 'LN:', '')" &
                    " ORDER BY VCECode "
        SQL.ExecNonQuery(insertSQl)
        For Each item As ListViewItem In lvFilter.Items
            If item.Checked = True And i < 10 Then
                insertSQl = " INSERT INTO " &
                            " tblSL_PrintH(Type, Description) " &
                            " VALUES ('C" & i & "',@Description)"
                SQL.FlushParams()
                SQL.AddParam("@Description", item.SubItems(0).Text)
                SQL.ExecNonQuery(insertSQl)
                Dim updateSQL As String
                updateSQL = " UPDATE tblSL_Print " &
                            " SET   Type = @Description, C" & i & " = ISNULL(Balance,0) " &
                            " FROM  " &
                            " ( " &
                            "      SELECT VCECode, REPLACE(RefNo, 'LN:', '') AS RefNo, SUM(ISNULL(CASE WHEN AccountNature ='Debit' " &
                            " 		     THEN Debit -Credit                           " &
                            " 			ELSE Credit - Debit  " &
                            " 	   END,0)) AS Balance " &
                            " FROM view_GL INNER JOIN tblCOA_Master " &
                            " ON view_GL.AccntCode = tblCOA_Master.AccountCode " &
                            " WHERE AppDate BETWEEN CAST('01-01-" & dtpFrom.Value.Year & "' AS DATE) AND '" & dtpTo.Value.Date & "' " &
                            " AND WithSubsidiary = 1 AND tblCOA_Master.AccountTitle = @Description AND AccountNature IN ('Debit','Credit') " & IIf(branch = "ALL", "AND BranchCode in ( SELECT    DISTINCT  tblBranch.BranchCode   " &
                            " FROM      tblBranch    " &
                            " INNER JOIN tblUser_Access    ON   " &
                            " tblBranch.BranchCode = tblUser_Access.Code    " &
                            " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " &
                            " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " &
                            " WHERE     UserID ='" & UserID & "'  )", " AND BranchCode = '" & branch & "' ") &
                            " GROUP BY VCECode, REPLACE(RefNo, 'LN:', '') " &
                            " ) AS A " &
                            " WHERE  tblSL_Print.VCECode = A.VCECode AND tblSL_Print.RefID = A.RefNo"
                SQL.FlushParams()
                SQL.AddParam("@Description", item.SubItems(0).Text)
                SQL.ExecNonQuery(updateSQL)
                i += 1
            End If
        Next

        Dim f As New frmReport_Display
        f.ShowDialog("SUBLGRS", UserName, dtpFrom.Value.Date, dtpTo.Value.Date)
        f.Dispose()
    End Sub

    Private Sub GenerateSLC()
        Dim i As Integer = 1
        Dim insertSQl As String
        Dim deleteSQL As String
        deleteSQL = " DELETE FROM tblSL_PrintH  "
        SQL.ExecNonQuery(deleteSQL)
        deleteSQL = " DELETE FROM tblSL_Print "
        SQL.ExecNonQuery(deleteSQL)
        insertSQl = " INSERT INTO tblSL_Print(VCECode) " &
                    " SELECT DISTINCT VCECode " &
                    " FROM   view_GL INNER JOIN tblCOA_Master " &
                    " ON     view_GL.AccntCode = tblCOA_Master.AccountCode " &
                    " WHERE  AppDate BETWEEN CAST('01-01-" & dtpFrom.Value.Year & "' AS DATE) AND '" & dtpTo.Value.Date & "'  " &
                    " AND    WithSubsidiary = 1 " & IIf(branch = "ALL", "AND BranchCode in ( SELECT    DISTINCT  tblBranch.BranchCode   " &
                    " FROM      tblBranch    " &
                    " INNER JOIN tblUser_Access    ON   " &
                    " tblBranch.BranchCode = tblUser_Access.Code    " &
                    " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " &
                    " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " &
                    " WHERE     UserID ='" & UserID & "'  )", " AND BranchCode = '" & branch & "' ") &
                    " ORDER BY VCECode "
        SQL.ExecNonQuery(insertSQl)
        For Each item As ListViewItem In lvFilter.Items
            If item.Checked = True And i < 10 Then
                insertSQl = " INSERT INTO " &
                            " tblSL_PrintH(Type, Description) " &
                            " VALUES ('C" & i & "',@Description)"
                SQL.FlushParams()
                SQL.AddParam("@Description", item.SubItems(0).Text)
                SQL.ExecNonQuery(insertSQl)
                Dim updateSQL As String
                updateSQL = " UPDATE tblSL_Print " &
                            " SET    C" & i & " = ISNULL(Balance,0) " &
                            " FROM  " &
                            " ( " &
                            "      SELECT VCECode, SUM(ISNULL(CASE WHEN AccountNature ='Debit' " &
                            " 		     THEN Debit -Credit                           " &
                            " 			ELSE Credit - Debit  " &
                            " 	   END,0)) AS Balance " &
                            " FROM view_GL INNER JOIN tblCOA_Master " &
                            " ON view_GL.AccntCode = tblCOA_Master.AccountCode " &
                            " WHERE AppDate BETWEEN CAST('01-01-" & dtpFrom.Value.Year & "' AS DATE) AND '" & dtpTo.Value.Date & "' " & IIf(branch = "ALL", "AND BranchCode in ( SELECT    DISTINCT  tblBranch.BranchCode   " &
                            " FROM      tblBranch    " &
                            " INNER JOIN tblUser_Access    ON   " &
                            " tblBranch.BranchCode = tblUser_Access.Code    " &
                            " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " &
                            " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " &
                            " WHERE     UserID ='" & UserID & "'  )", " AND BranchCode = '" & branch & "' ") &
                            " AND WithSubsidiary = 1 AND tblCOA_Master.AccountTitle = @Description AND AccountNature IN ('Debit','Credit') " &
                            " GROUP BY VCECode " &
                            " ) AS A " &
                            " WHERE  tblSL_Print.VCECode = A.VCECode "
                SQL.FlushParams()
                SQL.AddParam("@Description", item.SubItems(0).Text)
                SQL.ExecNonQuery(updateSQL)
                i += 1
            End If
        Next

        Dim f As New frmReport_Display
        If cbReportCategory.SelectedItem = "Subsidiary Ledger Column" Then
            f.ShowDialog("SUBLGRSC", UserName, dtpFrom.Value.Date, dtpTo.Value.Date)
            f.Dispose()
        Else
            f.ShowDialog("SUBLGRS_Insu", UserName, dtpFrom.Value.Date, dtpTo.Value.Date)
            f.Dispose()
        End If
    End Sub
    Private Sub GenerateBooks(ByVal Book As String)
        'Dim f As New frmReport_Display
        Dim Query, deleteSQL As String
        Dim Codes As String()
        Dim CodeList As New List(Of String)
        deleteSQL = "DELETE FROM RptBooks_Header"
        SQL.ExecNonQuery(deleteSQL)


        Query = " INSERT INTO rptBooks_Header(Type,  AccntCode, Description) " & vbCrLf &
              " SELECT  'C' + CAST(ROW_NUMBER() OVER (ORDER BY Record_ID) AS nvarchar) AS Type , AccountCode, Description  " &
              " FROM  " &
              " ( " &
              " SELECT  TOP 5 AccountCode, Description, Record_ID FROM rptBook_Maintenance  " &
              " WHERE Type LIKE '" & cbReportType.Text.Trim & "%' " &
              " ) AS TopAccounts "
        SQL.ExecNonQuery(Query)


        Query = " SELECT Accntcode FROM rptBooks_Header "
        SQL.ReadQuery(Query)

        While SQL.SQLDR.Read()
            CodeList.Add(SQL.SQLDR("Accntcode").ToString)
        End While

        Codes = CodeList.ToArray()

        deleteSQL = "DELETE FROM rptBooks"
        SQL.ExecNonQuery(deleteSQL)
        Select Case cbReportType.SelectedItem
            Case "Cash Receipts Book"
                If cbCostCenter.Text <> "ALL" Then
                    Query = " INSERT rptBooks(Date, RefNo, CheckNo, VCECode, VCEName, Particulars, Account, Amount, C1, C2, C3, C4, C5, AccntTitle, Debit, Credit, Reference.Status, CostCenter)" & vbCrLf &
                             " SELECT  AppDate, Reference.RefType + ':' + Reference.TransNo AS Ref, Reference.Check_No,  " & vbCrLf &
                             " 		ISNULL(CIB.VCECode, Reference.VCECode) AS VCECode, viewVCE_Master.VCEName, Particulars, " & vbCrLf &
                             " 		CIB.AccntTitle  AS Account,  " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  CIB.Amount ELSE 0 END AS Amount, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C1 ELSE 0 END AS C1, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C2 ELSE 0 END AS C2, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C3 ELSE 0 END AS C3, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C4 ELSE 0 END AS C4, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C5 ELSE 0 END AS C5, " & vbCrLf &
                             "       Sundries.AccntTitle, Debit, Credit, Reference.Status , CostCenter" & vbCrLf &
                             " FROM " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT 	 AppDate, RefType, RefTransID, TransNo, Check_No, MAX(VCECode) AS VCECode, ISNULL(Remarks, MAX(Particulars)) AS Particulars, Status, CostCenter " & vbCrLf &
                             " 	FROM	 view_GL   " & vbCrLf &
                             " 	WHERE	 (Book ='" & Book & "' ) " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED' AND CostCenter = '" & cbCostCenter.Text & "' ") & vbCrLf &
                             " 	GROUP BY AppDate, RefType, RefTransID, Check_No, Remarks, TransNo, Status, CostCenter " & vbCrLf &
                             " ) AS Reference " & vbCrLf &
                             " LEFT JOIN " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT	 RefType, RefTransID, VCECode, view_GL.AccntTitle, CASE WHEN (Book ='" & Book & "' )  THEN  SUM(Debit-Credit) ELSE SUM(Credit-Debit) END  AS Amount  " & vbCrLf &
                             " 	FROM	 view_GL  INNER JOIN (SELECT AccountCode FROM tblBank_Master  " & vbCrLf &
                             " 	UNION ALL SELECT CASH_ACCOUNT FROM tblSystemSetup) AS tblBank_Master  " & vbCrLf &
                             " 	ON		 view_GL.AccntCode = tblBank_Master.AccountCode " & vbCrLf &
                             " 	WHERE	 (Book ='" & Book & "' ) " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED' AND CostCenter = '" & cbCostCenter.Text & "' ") & vbCrLf &
                             " 	AND      Debit + Credit <> 0 " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID, view_GL.AccntTitle, VCECode, Book, SJReftype " & vbCrLf &
                             " ) AS CIB " & vbCrLf &
                             " ON	Reference.RefType = CIB.RefType " & vbCrLf &
                             " AND	Reference.RefTransID = CIB.RefTransID " & vbCrLf &
                             " LEFT JOIN " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT RefType, RefTransID, " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(0) & "],0)) AS C1,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(1) & "],0)) AS C2,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(2) & "],0)) AS C3,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(3) & "],0)) AS C4,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(4) & "],0)) AS C5 " & vbCrLf &
                             " 	FROM  " & vbCrLf &
                             " 	( " & vbCrLf &
                             " 		SELECT	 RefType, RefTransID, AccntCode, SUM(Credit-Debit) AS Amount " & vbCrLf &
                             " 		FROM	 view_GL  " & vbCrLf &
                             " 		WHERE	 AccntCode IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                             " 		AND		 (Book ='" & Book & "' ) " & vbCrLf &
                             " 		AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' AND Debit + Credit <> 0  " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED' AND CostCenter = '" & cbCostCenter.Text & "' ") & vbCrLf &
                             " 		GROUP BY RefType, RefTransID, AccntCode " & vbCrLf &
                             " 	) AS TopAccount " & vbCrLf &
                             " 	PIVOT (SUM(Amount) FOR [AccntCode]  " & vbCrLf &
                             " 				IN ([" & Codes(0) & "], [" & Codes(1) & "], [" & Codes(2) & "], [" & Codes(3) & "], [" & Codes(4) & "]) " & vbCrLf &
                             " 			)  AS pvt " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID " & vbCrLf &
                             " ) AS TopAccountColumns " & vbCrLf &
                             " ON	Reference.RefType = TopAccountColumns.RefType " & vbCrLf &
                             " AND	Reference.RefTransID = TopAccountColumns.RefTransID " & vbCrLf &
                             " LEFT JOIN  " & vbCrLf &
                             " ( " & vbCrLf &
                             " SELECT RefType, RefTransID, AccntCode, AccntTitle, Debit, Credit, ROW_NUMBER() OVER (PARTITION BY RefType, RefTransID ORDER BY Debit DESC, Credit Asc) AS RowID FROM  " & vbCrLf &
                             " ( " & vbCrLf &
                             " 		SELECT	 RefType, RefTransID, AccntCode, AccntTitle,    " & vbCrLf &
                             "   CASE WHEN SUM(Debit) > SUM(Credit) THEN  SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit," & vbCrLf &
                             "   CASE WHEN SUM(Credit) > SUM(Debit) THEN  SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit" & vbCrLf &
                             " 	FROM	 view_GL  " & vbCrLf &
                             " 	WHERE	 AccntCode NOT IN (SELECT AccountCode FROM tblBank_Master UNION ALL SELECT CASH_ACCOUNT FROM tblSystemSetup)  AND AccntCode NOT IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                             " 	AND		 (Book ='" & Book & "' ) " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & vbCrLf &
                             " 	AND     Debit + Credit <> 0  " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED' AND CostCenter = '" & cbCostCenter.Text & "'") & vbCrLf &
                             "   GROUP BY RefType, RefTransID, AccntCode, AccntTitle " & vbCrLf &
                               " ) AS SundryAccounts " & vbCrLf &
                             " ) AS Sundries " & vbCrLf &
                             " ON	Reference.RefType = Sundries.RefType " & vbCrLf &
                             " AND	Reference.RefTransID = Sundries.RefTransID " & vbCrLf &
                             " LEFT JOIN viewVCE_Master " & vbCrLf &
                             " ON  ISNULL(CIB.VCECode, Reference.VCECode) = viewVCE_Master.VCECode "
                    SQL.ExecNonQuery(Query)
                    ' f.ShowDialog("Books_CRB", Book, dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, UserID)
                Else
                    Query = " INSERT rptBooks(Date, RefNo, CheckNo, VCECode, VCEName, Particulars, Account, Amount, C1, C2, C3, C4, C5, AccntTitle, Debit, Credit, Reference.Status, CostCenter)" & vbCrLf &
                             " SELECT  AppDate, Reference.RefType + ':' + Reference.TransNo AS Ref, Reference.Check_No,  " & vbCrLf &
                             " 		ISNULL(CIB.VCECode, Reference.VCECode) AS VCECode, viewVCE_Master.VCEName, Particulars, " & vbCrLf &
                             " 		CIB.AccntTitle  AS Account,  " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  CIB.Amount ELSE 0 END AS Amount, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C1 ELSE 0 END AS C1, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C2 ELSE 0 END AS C2, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C3 ELSE 0 END AS C3, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C4 ELSE 0 END AS C4, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C5 ELSE 0 END AS C5, " & vbCrLf &
                             "       Sundries.AccntTitle, Debit, Credit, Reference.Status , CostCenter" & vbCrLf &
                             " FROM " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT 	 AppDate, RefType, RefTransID, TransNo, Check_No, MAX(VCECode) AS VCECode, ISNULL(Remarks, MAX(Particulars)) AS Particulars, Status, CostCenter " & vbCrLf &
                             " 	FROM	 view_GL   " & vbCrLf &
                             " 	WHERE	 (Book ='" & Book & "' ) " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                             " 	GROUP BY AppDate, RefType, RefTransID, Check_No, Remarks, TransNo, Status, CostCenter " & vbCrLf &
                             " ) AS Reference " & vbCrLf &
                             " LEFT JOIN " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT	 RefType, RefTransID, VCECode, view_GL.AccntTitle, CASE WHEN (Book ='" & Book & "' )  THEN  SUM(Debit-Credit) ELSE SUM(Credit-Debit) END  AS Amount  " & vbCrLf &
                             " 	FROM	 view_GL  INNER JOIN (SELECT AccountCode FROM tblBank_Master  " & vbCrLf &
                             " 	UNION ALL SELECT CASH_ACCOUNT FROM tblSystemSetup) AS tblBank_Master  " & vbCrLf &
                             " 	ON		 view_GL.AccntCode = tblBank_Master.AccountCode " & vbCrLf &
                             " 	WHERE	 (Book ='" & Book & "' ) " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED'") & vbCrLf &
                             " 	AND      Debit + Credit <> 0 " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID, view_GL.AccntTitle, VCECode, Book, SJReftype " & vbCrLf &
                             " ) AS CIB " & vbCrLf &
                             " ON	Reference.RefType = CIB.RefType " & vbCrLf &
                             " AND	Reference.RefTransID = CIB.RefTransID " & vbCrLf &
                             " LEFT JOIN " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT RefType, RefTransID, " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(0) & "],0)) AS C1,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(1) & "],0)) AS C2,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(2) & "],0)) AS C3,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(3) & "],0)) AS C4,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(4) & "],0)) AS C5 " & vbCrLf &
                             " 	FROM  " & vbCrLf &
                             " 	( " & vbCrLf &
                             " 		SELECT	 RefType, RefTransID, AccntCode, SUM(Credit-Debit) AS Amount " & vbCrLf &
                             " 		FROM	 view_GL  " & vbCrLf &
                             " 		WHERE	 AccntCode IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                             " 		AND		 (Book ='" & Book & "' ) " & vbCrLf &
                             " 		AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' AND Debit + Credit <> 0  " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                             " 		GROUP BY RefType, RefTransID, AccntCode " & vbCrLf &
                             " 	) AS TopAccount " & vbCrLf &
                             " 	PIVOT (SUM(Amount) FOR [AccntCode]  " & vbCrLf &
                             " 				IN ([" & Codes(0) & "], [" & Codes(1) & "], [" & Codes(2) & "], [" & Codes(3) & "], [" & Codes(4) & "]) " & vbCrLf &
                             " 			)  AS pvt " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID " & vbCrLf &
                             " ) AS TopAccountColumns " & vbCrLf &
                             " ON	Reference.RefType = TopAccountColumns.RefType " & vbCrLf &
                             " AND	Reference.RefTransID = TopAccountColumns.RefTransID " & vbCrLf &
                             " LEFT JOIN  " & vbCrLf &
                             " ( " & vbCrLf &
                             " SELECT RefType, RefTransID, AccntCode, AccntTitle, Debit, Credit, ROW_NUMBER() OVER (PARTITION BY RefType, RefTransID ORDER BY Debit DESC, Credit Asc) AS RowID FROM  " & vbCrLf &
                             " ( " & vbCrLf &
                             " 		SELECT	 RefType, RefTransID, AccntCode, AccntTitle,    " & vbCrLf &
                             "   CASE WHEN SUM(Debit) > SUM(Credit) THEN  SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit," & vbCrLf &
                             "   CASE WHEN SUM(Credit) > SUM(Debit) THEN  SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit" & vbCrLf &
                             " 	FROM	 view_GL  " & vbCrLf &
                             " 	WHERE	 AccntCode NOT IN (SELECT AccountCode FROM tblBank_Master UNION ALL SELECT CASH_ACCOUNT FROM tblSystemSetup)  AND AccntCode NOT IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                             " 	AND		 (Book ='" & Book & "' ) " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & vbCrLf &
                             " 	AND     Debit + Credit <> 0  " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                             "   GROUP BY RefType, RefTransID, AccntCode, AccntTitle " & vbCrLf &
                               " ) AS SundryAccounts " & vbCrLf &
                             " ) AS Sundries " & vbCrLf &
                             " ON	Reference.RefType = Sundries.RefType " & vbCrLf &
                             " AND	Reference.RefTransID = Sundries.RefTransID " & vbCrLf &
                             " LEFT JOIN viewVCE_Master " & vbCrLf &
                             " ON  ISNULL(CIB.VCECode, Reference.VCECode) = viewVCE_Master.VCECode "
                    SQL.ExecNonQuery(Query)
                End If

            Case "Cash Disbursements Book"
                If cbCostCenter.Text <> "ALL" Then
                    Query = " INSERT rptBooks(Date, RefNo, CheckNo, VCECode, VCEName, Particulars, Account, Amount, C1, C2, C3, C4, C5, AccntTitle, Debit, Credit, Reference.Status, CostCenter)" & vbCrLf &
                             " SELECT  AppDate, Reference.RefType + ':' + Reference.TransNo AS Ref, Reference.Check_No,  " & vbCrLf &
                             " 		ISNULL(CIB.VCECode, Reference.VCECode) AS VCECode, viewVCE_Master.VCEName, Particulars, " & vbCrLf &
                             " 		CIB.AccntTitle  AS Account,  " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  CIB.Amount ELSE 0 END AS Amount, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C1 ELSE 0 END AS C1, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C2 ELSE 0 END AS C2, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C3 ELSE 0 END AS C3, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C4 ELSE 0 END AS C4, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C5 ELSE 0 END AS C5, " & vbCrLf &
                             "       Sundries.AccntTitle, Debit, Credit, Reference.Status, CostCenter " & vbCrLf &
                             " FROM " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT 	 AppDate, RefType, RefTransID, TransNo, MAX(Check_No) AS Check_No, MAX(VCECode) AS VCECode, ISNULL(Remarks, MAX(Particulars)) AS Particulars, Status, CostCenter " & vbCrLf &
                             " 	FROM	 view_GL   " & vbCrLf &
                             " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED' AND CostCenter = '" & cbCostCenter.Text & "' ") & vbCrLf &
                             " 	GROUP BY AppDate, RefType, RefTransID, Remarks, TransNo, Status, CostCenter " & vbCrLf &
                             " ) AS Reference " & vbCrLf &
                             " LEFT JOIN " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT	 RefType, RefTransID, VCECode, view_GL.AccntTitle, SUM(Credit-Debit) AS Amount   " & vbCrLf &
                             " 	FROM	 view_GL INNER JOIN tblCV_BankRef " & vbCrLf &
                             "  ON		 view_GL.RefTransID = tblCV_BankRef.CV_No " & vbCrLf &
                             "  INNER JOIN tblBank_Master  " & vbCrLf &
                             " 	ON		  CAST(tblCV_BankRef.BankID AS INT) = CAST(tblBank_Master.BankID  AS INT) " & vbCrLf &
                             "  AND		 view_GL.AccntCode = tblBank_Master.AccountCode " & vbCrLf &
                             " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED' AND CostCenter = '" & cbCostCenter.Text & "'") & vbCrLf &
                             " 	AND      Debit + Credit <> 0   AND isUpload = 0 " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID, view_GL.AccntTitle, VCECode, Book " & vbCrLf &
                             "      UNION ALL " & vbCrLf &
                             " 	SELECT	 RefType, RefTransID, VCECode, view_GL.AccntTitle, SUM(Credit-Debit) AS Amount  " & vbCrLf &
                             " 	FROM	 view_GL  INNER JOIN tblBank_Master  " & vbCrLf &
                             " 	ON		 view_GL.AccntCode = tblBank_Master.AccountCode " & vbCrLf &
                             " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED' AND CostCenter = '" & cbCostCenter.Text & "'") & vbCrLf &
                             " 	AND      Debit + Credit <> 0  AND isUpload = 1 " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID, view_GL.AccntTitle, VCECode, Book " & vbCrLf &
                             " ) AS CIB " & vbCrLf &
                             " ON	CAST(Reference.RefType AS nvarchar) = CAST(CIB.RefType  AS nvarchar) " & vbCrLf &
                             " AND	CAST(Reference.RefTransID AS INT) = CAST(CIB.RefTransID  AS INT) " & vbCrLf &
                             " LEFT JOIN " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT RefType, RefTransID, " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(0) & "],0)) AS C1,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(1) & "],0)) AS C2,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(2) & "],0)) AS C3,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(3) & "],0)) AS C4,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(4) & "],0)) AS C5 " & vbCrLf &
                             " 	FROM  " & vbCrLf &
                             " 	( " & vbCrLf &
                             " 		SELECT	 RefType, RefTransID, AccntCode, SUM(Debit-Credit) AS Amount " & vbCrLf &
                             " 		FROM	 view_GL  " & vbCrLf &
                             " 		WHERE	 AccntCode IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                             " 		AND		 Book ='" & Book & "'  " & vbCrLf &
                             " 		AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' AND Debit + Credit <> 0  " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED' AND CostCenter = '" & cbCostCenter.Text & "'") & vbCrLf &
                             " 		GROUP BY RefType, RefTransID, AccntCode " & vbCrLf &
                             " 	) AS TopAccount " & vbCrLf &
                             " 	PIVOT (SUM(Amount) FOR [AccntCode]  " & vbCrLf &
                             " 				IN ([" & Codes(0) & "], [" & Codes(1) & "], [" & Codes(2) & "], [" & Codes(3) & "], [" & Codes(4) & "]) " & vbCrLf &
                             " 			)  AS pvt " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID " & vbCrLf &
                             " ) AS TopAccountColumns " & vbCrLf &
                             " ON	CAST(Reference.RefType AS nvarchar)  = CAST(TopAccountColumns.RefType  AS nvarchar) " & vbCrLf &
                             " AND	CAST(Reference.RefTransID AS INT) = CAST(TopAccountColumns.RefTransID AS INT)  " & vbCrLf &
                             " LEFT JOIN  " & vbCrLf &
                             " ( " & vbCrLf &
                             " SELECT RefType, RefTransID, AccntCode, AccntTitle, Debit, Credit, ROW_NUMBER() OVER (PARTITION BY RefType, RefTransID ORDER BY Debit DESC, Credit Asc) AS RowID FROM  " & vbCrLf &
                             " ( " & vbCrLf &
                             " 		SELECT	 RefType, RefTransID, AccntCode, AccntTitle,    " & vbCrLf &
                             "   CASE WHEN SUM(Debit) > SUM(Credit) THEN  SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit," & vbCrLf &
                             "   CASE WHEN SUM(Credit) > SUM(Debit) THEN  SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit" & vbCrLf &
                             " 	FROM	 view_GL  " & vbCrLf &
                             " 	WHERE	 AccntCode NOT IN (SELECT AccountCode FROM tblBank_Master)  AND AccntCode NOT IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                             " 	AND		 Book ='" & Book & "'  " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & vbCrLf &
                             " 	AND     Debit + Credit <> 0  AND IsUpload = 1 " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                             "   GROUP BY RefType, RefTransID, AccntCode, AccntTitle " & vbCrLf &
                             " UNION ALL " & vbCrLf &
                             "     SELECT	 RefType, RefTransID, VCECode, view_GL.AccntTitle, " & vbCrLf &
                            " 	   CASE WHEN SUM(Debit) > SUM(Credit) THEN  SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit, " & vbCrLf &
                            " 	   CASE WHEN SUM(Credit) > SUM(Debit) THEN  SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit " & vbCrLf &
                            "  	FROM	 view_GL  LEFT JOIN tblCV_BankRef  " & vbCrLf &
                            " 	ON		 view_GL.RefTransID = tblCV_BankRef.CV_No " & vbCrLf &
                            " 	LEFT JOIN tblBank_Master " & vbCrLf &
                            " 	ON		 CAST(tblCV_BankRef.BankID AS INT) = CAST(tblBank_Master.BankID AS INT)  " & vbCrLf &
                            " 	AND		 view_GL.AccntCode = tblBank_Master.AccountCode " & vbCrLf &
                            "  	WHERE	 Book ='" & Book & "' " &
                            "  	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & vbCrLf &
                            "  	AND      Debit + Credit <> 0 AND isUpload = 0  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED'") & vbCrLf &
                            " 	AND		tblBank_Master.BankID IS NULL  AND AccntCode NOT IN (SELECT AccntCode FROM rptBooks_Header)  " & vbCrLf &
                            "  	GROUP BY RefType, RefTransID, view_GL.AccntTitle, VCECode, Book  " & vbCrLf &
                               " ) AS SundryAccounts " & vbCrLf &
                             " ) AS Sundries " & vbCrLf &
                             " ON	Reference.RefType = Sundries.RefType " & vbCrLf &
                             " AND	CAST(Reference.RefTransID AS INT) = CAST(Sundries.RefTransID  AS INT) " & vbCrLf &
                             " LEFT JOIN viewVCE_Master " & vbCrLf &
                             " ON  CAST(ISNULL(CIB.VCECode, Reference.VCECode) AS nvarchar)  = CAST(viewVCE_Master.VCECode AS nvarchar) "
                    SQL.ExecNonQuery(Query)
                    ' f.ShowDialog("Books_CDB", Book, dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, UserID)
                Else
                    Query = " INSERT rptBooks(Date, RefNo, CheckNo, VCECode, VCEName, Particulars, Account, Amount, C1, C2, C3, C4, C5, AccntTitle, Debit, Credit, Reference.Status)" & vbCrLf &
                            " SELECT  AppDate, Reference.RefType + ':' + Reference.TransNo AS Ref, Reference.Check_No,  " & vbCrLf &
                            " 		ISNULL(CIB.VCECode, Reference.VCECode) AS VCECode, viewVCE_Master.VCEName, Particulars, " & vbCrLf &
                            " 		CIB.AccntTitle  AS Account,  " & vbCrLf &
                            " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  CIB.Amount ELSE 0 END AS Amount, " & vbCrLf &
                            " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C1 ELSE 0 END AS C1, " & vbCrLf &
                            " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C2 ELSE 0 END AS C2, " & vbCrLf &
                            " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C3 ELSE 0 END AS C3, " & vbCrLf &
                            " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C4 ELSE 0 END AS C4, " & vbCrLf &
                            " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C5 ELSE 0 END AS C5, " & vbCrLf &
                            "       Sundries.AccntTitle, Debit, Credit, Reference.Status " & vbCrLf &
                            " FROM " & vbCrLf &
                            " ( " & vbCrLf &
                            " 	SELECT 	 AppDate, RefType, RefTransID, TransNo, MAX(Check_No) AS Check_No, MAX(VCECode) AS VCECode, ISNULL(Remarks, MAX(Particulars)) AS Particulars, Status " & vbCrLf &
                            " 	FROM	 view_GL   " & vbCrLf &
                            " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                            " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                            " 	GROUP BY AppDate, RefType, RefTransID, Remarks, TransNo, Status " & vbCrLf &
                            " ) AS Reference " & vbCrLf &
                            " LEFT JOIN " & vbCrLf &
                            " ( " & vbCrLf &
                            " 	SELECT	 RefType, RefTransID, VCECode, view_GL.AccntTitle, SUM(Credit-Debit) AS Amount   " & vbCrLf &
                            " 	FROM	 view_GL INNER JOIN tblCV_BankRef " & vbCrLf &
                            "  ON		 view_GL.RefTransID = tblCV_BankRef.CV_No " & vbCrLf &
                            "  INNER JOIN tblBank_Master  " & vbCrLf &
                            " 	ON		  CAST(tblCV_BankRef.BankID AS INT) = CAST(tblBank_Master.BankID  AS INT) " & vbCrLf &
                            "  AND		 view_GL.AccntCode = tblBank_Master.AccountCode " & vbCrLf &
                            " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                            " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED'") & vbCrLf &
                            " 	AND      Debit + Credit <> 0   AND isUpload = 0 " & vbCrLf &
                            " 	GROUP BY RefType, RefTransID, view_GL.AccntTitle, VCECode, Book " & vbCrLf &
                            "      UNION ALL " & vbCrLf &
                            " 	SELECT	 RefType, RefTransID, VCECode, view_GL.AccntTitle, SUM(Credit-Debit) AS Amount  " & vbCrLf &
                            " 	FROM	 view_GL  INNER JOIN tblBank_Master  " & vbCrLf &
                            " 	ON		 view_GL.AccntCode = tblBank_Master.AccountCode " & vbCrLf &
                            " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                            " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED'") & vbCrLf &
                            " 	AND      Debit + Credit <> 0  AND isUpload = 1 " & vbCrLf &
                            " 	GROUP BY RefType, RefTransID, view_GL.AccntTitle, VCECode, Book " & vbCrLf &
                            " ) AS CIB " & vbCrLf &
                            " ON	CAST(Reference.RefType AS nvarchar) = CAST(CIB.RefType  AS nvarchar) " & vbCrLf &
                            " AND	CAST(Reference.RefTransID AS INT) = CAST(CIB.RefTransID  AS INT) " & vbCrLf &
                            " LEFT JOIN " & vbCrLf &
                            " ( " & vbCrLf &
                            " 	SELECT RefType, RefTransID, " & vbCrLf &
                            " 			SUM(ISNULL([" & Codes(0) & "],0)) AS C1,  " & vbCrLf &
                            " 			SUM(ISNULL([" & Codes(1) & "],0)) AS C2,  " & vbCrLf &
                            " 			SUM(ISNULL([" & Codes(2) & "],0)) AS C3,  " & vbCrLf &
                            " 			SUM(ISNULL([" & Codes(3) & "],0)) AS C4,  " & vbCrLf &
                            " 			SUM(ISNULL([" & Codes(4) & "],0)) AS C5 " & vbCrLf &
                            " 	FROM  " & vbCrLf &
                            " 	( " & vbCrLf &
                            " 		SELECT	 RefType, RefTransID, AccntCode, SUM(Debit-Credit) AS Amount " & vbCrLf &
                            " 		FROM	 view_GL  " & vbCrLf &
                            " 		WHERE	 AccntCode IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                            " 		AND		 Book ='" & Book & "'  " & vbCrLf &
                            " 		AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' AND Debit + Credit <> 0  " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                            " 		GROUP BY RefType, RefTransID, AccntCode " & vbCrLf &
                            " 	) AS TopAccount " & vbCrLf &
                            " 	PIVOT (SUM(Amount) FOR [AccntCode]  " & vbCrLf &
                            " 				IN ([" & Codes(0) & "], [" & Codes(1) & "], [" & Codes(2) & "], [" & Codes(3) & "], [" & Codes(4) & "]) " & vbCrLf &
                            " 			)  AS pvt " & vbCrLf &
                            " 	GROUP BY RefType, RefTransID " & vbCrLf &
                            " ) AS TopAccountColumns " & vbCrLf &
                            " ON	CAST(Reference.RefType AS nvarchar)  = CAST(TopAccountColumns.RefType  AS nvarchar) " & vbCrLf &
                            " AND	CAST(Reference.RefTransID AS INT) = CAST(TopAccountColumns.RefTransID AS INT)  " & vbCrLf &
                            " LEFT JOIN  " & vbCrLf &
                            " ( " & vbCrLf &
                            " SELECT RefType, RefTransID, AccntCode, AccntTitle, Debit, Credit, ROW_NUMBER() OVER (PARTITION BY RefType, RefTransID ORDER BY Debit DESC, Credit Asc) AS RowID FROM  " & vbCrLf &
                            " ( " & vbCrLf &
                            " 		SELECT	 RefType, RefTransID, AccntCode, AccntTitle,    " & vbCrLf &
                            "   CASE WHEN SUM(Debit) > SUM(Credit) THEN  SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit," & vbCrLf &
                            "   CASE WHEN SUM(Credit) > SUM(Debit) THEN  SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit" & vbCrLf &
                            " 	FROM	 view_GL  " & vbCrLf &
                            " 	WHERE	 AccntCode NOT IN (SELECT AccountCode FROM tblBank_Master)  AND AccntCode NOT IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                            " 	AND		 Book ='" & Book & "'  " & vbCrLf &
                            " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & vbCrLf &
                            " 	AND     Debit + Credit <> 0  AND IsUpload = 1 " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                            "   GROUP BY RefType, RefTransID, AccntCode, AccntTitle " & vbCrLf &
                            " UNION ALL " & vbCrLf &
                            "     SELECT	 RefType, RefTransID, VCECode, view_GL.AccntTitle, " & vbCrLf &
                           " 	   CASE WHEN SUM(Debit) > SUM(Credit) THEN  SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit, " & vbCrLf &
                           " 	   CASE WHEN SUM(Credit) > SUM(Debit) THEN  SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit " & vbCrLf &
                           "  	FROM	 view_GL  LEFT JOIN tblCV_BankRef  " & vbCrLf &
                           " 	ON		 view_GL.RefTransID = tblCV_BankRef.CV_No " & vbCrLf &
                           " 	LEFT JOIN tblBank_Master " & vbCrLf &
                           " 	ON		 CAST(tblCV_BankRef.BankID AS INT) = CAST(tblBank_Master.BankID AS INT)  " & vbCrLf &
                           " 	AND		 view_GL.AccntCode = tblBank_Master.AccountCode " & vbCrLf &
                           "  	WHERE	 Book ='" & Book & "' " &
                           "  	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & vbCrLf &
                           "  	AND      Debit + Credit <> 0 AND isUpload = 0  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED'") & vbCrLf &
                           " 	AND		tblBank_Master.BankID IS NULL  AND AccntCode NOT IN (SELECT AccntCode FROM rptBooks_Header)  " & vbCrLf &
                           "  	GROUP BY RefType, RefTransID, view_GL.AccntTitle, VCECode, Book  " & vbCrLf &
                              " ) AS SundryAccounts " & vbCrLf &
                            " ) AS Sundries " & vbCrLf &
                            " ON	Reference.RefType = Sundries.RefType " & vbCrLf &
                            " AND	CAST(Reference.RefTransID AS INT) = CAST(Sundries.RefTransID  AS INT) " & vbCrLf &
                            " LEFT JOIN viewVCE_Master " & vbCrLf &
                            " ON  CAST(ISNULL(CIB.VCECode, Reference.VCECode) AS nvarchar)  = CAST(viewVCE_Master.VCECode AS nvarchar) "
                    SQL.ExecNonQuery(Query)
                End If
            Case "Accounts Payable"
                If cbCostCenter.Text <> "ALL" Then
                    Query = " INSERT rptBooks(Date, RefNo,  VCECode, VCEName, Particulars, Account, Amount, C1, C2, C3, C4, C5, AccntTitle, Debit, Credit, Reference.Status, CostCenter)" & vbCrLf &
                             " SELECT  AppDate, Reference.RefType + ':' + Reference.TransNo AS Ref, " & vbCrLf &
                             " 		 Reference.VCECode AS VCECode, viewVCE_Master.VCEName, Particulars, " & vbCrLf &
                             " 		CIB.AccntTitle  AS Account,  " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  CIB.Amount ELSE 0 END AS Amount, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C1 ELSE 0 END AS C1, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C2 ELSE 0 END AS C2, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C3 ELSE 0 END AS C3, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C4 ELSE 0 END AS C4, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C5 ELSE 0 END AS C5, " & vbCrLf &
                             "       Sundries.AccntTitle, Debit, Credit, Reference.Status, CostCenter " & vbCrLf &
                             " FROM " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT 	 AppDate, RefType, RefTransID, TransNo, Check_No, MAX(VCECode) AS VCECode, ISNULL(Remarks, MAX(Particulars)) AS Particulars, Status, CostCenter " & vbCrLf &
                             " 	FROM	 view_GL   " & vbCrLf &
                             " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED' AND CostCenter = '" & cbCostCenter.Text & "'") & vbCrLf &
                             " 	GROUP BY AppDate, RefType, RefTransID, Check_No, Remarks, TransNo, Status, CostCenter " & vbCrLf &
                             " ) AS Reference " & vbCrLf &
                             " LEFT JOIN " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT	 RefType, RefTransID,  view_GL.AccntTitle, SUM(Credit-Debit) AS Amount  " & vbCrLf &
                             " 	FROM	 view_GL INNER JOIN tblAPV  " & vbCrLf &
                             "  ON       view_GL.RefTransiD = tblAPV.TransID " & vbCrLf &
                             "  AND      view_GL.RefType ='APV' " & vbCrLf &
                             "  AND      tblAPV.AccntCode = view_GL.AccntCode " & vbCrLf &
                             " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED' AND tblAPV.CostCenter = '" & cbCostCenter.Text & "'") & vbCrLf &
                             "  AND      Debit + Credit <> 0 AND isUpload = 0 " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID, view_GL.AccntTitle,  Book " & vbCrLf &
                             " UNION ALL " & vbCrLf &
                             " 	SELECT	 RefType, RefTransID,  view_GL.AccntTitle, SUM(Credit-Debit) AS Amount  " & vbCrLf &
                             " 	FROM	 view_GL " &
                             " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED' AND CostCenter = '" & cbCostCenter.Text & "'") & vbCrLf &
                             " 	AND      AccntCode IN ( SELECT AccntCode FROM tblDefaultAccount WHERE Type ='AP')  " &
                             "  AND      Debit + Credit <> 0 AND isUpload = 1 " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID, view_GL.AccntTitle,  Book " & vbCrLf &
                             " ) AS CIB " & vbCrLf &
                             " ON	Reference.RefType = CIB.RefType " & vbCrLf &
                             " AND	Reference.RefTransID = CIB.RefTransID " & vbCrLf &
                             " LEFT JOIN " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT RefType, RefTransID, " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(0) & "],0)) AS C1,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(1) & "],0)) AS C2,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(2) & "],0)) AS C3,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(3) & "],0)) AS C4,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(4) & "],0)) AS C5 " & vbCrLf &
                             " 	FROM  " & vbCrLf &
                             " 	( " & vbCrLf &
                             " 		SELECT	 RefType, RefTransID, AccntCode, SUM(Debit-Credit) AS Amount " & vbCrLf &
                             " 		FROM	 view_GL  " & vbCrLf &
                             " 		WHERE	 AccntCode IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                             " 		AND		 Book ='" & Book & "'  " & vbCrLf &
                             " 		AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' AND Debit + Credit <> 0  " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED' AND CostCenter = '" & cbCostCenter.Text & "'") & vbCrLf &
                             " 		GROUP BY RefType, RefTransID, AccntCode " & vbCrLf &
                             " 	) AS TopAccount " & vbCrLf &
                             " 	PIVOT (SUM(Amount) FOR [AccntCode]  " & vbCrLf &
                             " 				IN ([" & Codes(0) & "], [" & Codes(1) & "], [" & Codes(2) & "], [" & Codes(3) & "], [" & Codes(4) & "]) " & vbCrLf &
                             " 			)  AS pvt " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID " & vbCrLf &
                             " ) AS TopAccountColumns " & vbCrLf &
                             " ON	Reference.RefType = TopAccountColumns.RefType " & vbCrLf &
                             " AND	Reference.RefTransID = TopAccountColumns.RefTransID " & vbCrLf &
                             " LEFT JOIN  " & vbCrLf &
                             " ( " & vbCrLf &
                             " SELECT RefType, RefTransID, AccntCode, AccntTitle, Debit, Credit, ROW_NUMBER() OVER (PARTITION BY RefType, RefTransID ORDER BY Debit DESC, Credit Asc) AS RowID FROM  " & vbCrLf &
                             " ( " & vbCrLf &
                             " 		SELECT	 RefType, RefTransID, AccntCode, AccntTitle,    " & vbCrLf &
                             "   CASE WHEN SUM(Debit) > SUM(Credit) THEN  SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit," & vbCrLf &
                             "   CASE WHEN SUM(Credit) > SUM(Debit) THEN  SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit" & vbCrLf &
                             " 	FROM	 view_GL  " & vbCrLf &
                             " 	WHERE	 AccntCode NOT  IN ( SELECT AccntCode FROM tblDefaultAccount WHERE Type ='AP')   AND AccntCode NOT IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                             " 	AND		 Book ='" & Book & "'  " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & vbCrLf &
                             " 	AND     Debit + Credit <> 0   AND isUpload = 1 " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                             "   GROUP BY RefType, RefTransID, AccntCode, AccntTitle " & vbCrLf &
                             " UNION ALL " & vbCrLf &
                             " 	SELECT	 RefType, RefTransID, view_GL.AccntCode,  view_GL.AccntTitle,   " & vbCrLf &
                             "   CASE WHEN SUM(Debit) > SUM(Credit) THEN  SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit," & vbCrLf &
                             "   CASE WHEN SUM(Credit) > SUM(Debit) THEN  SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit" & vbCrLf &
                             " 	FROM	 view_GL LEFT JOIN tblAPV  " & vbCrLf &
                             "  ON       view_GL.RefTransiD = tblAPV.TransID " & vbCrLf &
                             "  AND      view_GL.RefType ='APV' " & vbCrLf &
                             "  AND      tblAPV.AccntCode = view_GL.AccntCode " & vbCrLf &
                             " 	WHERE	 tblAPV.AccntCode IS NULL AND Book ='" & Book & "'   AND view_GL.AccntCode NOT IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED'") & vbCrLf &
                             "  AND      Debit + Credit <> 0 AND isUpload = 0 " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID, view_GL.AccntCode,  view_GL.AccntTitle,  Book " & vbCrLf &
                               " ) AS SundryAccounts " & vbCrLf &
                             " ) AS Sundries " & vbCrLf &
                             " ON	Reference.RefType = Sundries.RefType " & vbCrLf &
                             " AND	Reference.RefTransID = Sundries.RefTransID " & vbCrLf &
                             " LEFT JOIN viewVCE_Master " & vbCrLf &
                             " ON   Reference.VCECode = viewVCE_Master.VCECode "
                    SQL.ExecNonQuery(Query)
                    '  f.ShowDialog("Books_APV", Book, dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, UserID)
                Else
                    Query = " INSERT rptBooks(Date, RefNo,  VCECode, VCEName, Particulars, Account, Amount, C1, C2, C3, C4, C5, AccntTitle, Debit, Credit, Reference.Status)" & vbCrLf &
                             " SELECT  AppDate, Reference.RefType + ':' + Reference.TransNo AS Ref, " & vbCrLf &
                             " 		 Reference.VCECode AS VCECode, viewVCE_Master.VCEName, Particulars, " & vbCrLf &
                             " 		CIB.AccntTitle  AS Account,  " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  CIB.Amount ELSE 0 END AS Amount, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C1 ELSE 0 END AS C1, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C2 ELSE 0 END AS C2, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C3 ELSE 0 END AS C3, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C4 ELSE 0 END AS C4, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C5 ELSE 0 END AS C5, " & vbCrLf &
                             "       Sundries.AccntTitle, Debit, Credit, Reference.Status " & vbCrLf &
                             " FROM " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT 	 AppDate, RefType, RefTransID, TransNo, Check_No, MAX(VCECode) AS VCECode, ISNULL(Remarks, MAX(Particulars)) AS Particulars, Status " & vbCrLf &
                             " 	FROM	 view_GL   " & vbCrLf &
                             " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                             " 	GROUP BY AppDate, RefType, RefTransID, Check_No, Remarks, TransNo, Status " & vbCrLf &
                             " ) AS Reference " & vbCrLf &
                             " LEFT JOIN " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT	 RefType, RefTransID,  view_GL.AccntTitle, SUM(Credit-Debit) AS Amount  " & vbCrLf &
                             " 	FROM	 view_GL INNER JOIN tblAPV  " & vbCrLf &
                             "  ON       view_GL.RefTransiD = tblAPV.TransID " & vbCrLf &
                             "  AND      view_GL.RefType ='APV' " & vbCrLf &
                             "  AND      tblAPV.AccntCode = view_GL.AccntCode " & vbCrLf &
                             " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED'") & vbCrLf &
                             "  AND      Debit + Credit <> 0 AND isUpload = 0 " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID, view_GL.AccntTitle,  Book " & vbCrLf &
                             " UNION ALL " & vbCrLf &
                             " 	SELECT	 RefType, RefTransID,  view_GL.AccntTitle, SUM(Credit-Debit) AS Amount  " & vbCrLf &
                             " 	FROM	 view_GL " &
                             " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED'") & vbCrLf &
                             " 	AND      AccntCode IN ( SELECT AccntCode FROM tblDefaultAccount WHERE Type ='AP')  " &
                             "  AND      Debit + Credit <> 0 AND isUpload = 1 " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID, view_GL.AccntTitle,  Book " & vbCrLf &
                             " ) AS CIB " & vbCrLf &
                             " ON	Reference.RefType = CIB.RefType " & vbCrLf &
                             " AND	Reference.RefTransID = CIB.RefTransID " & vbCrLf &
                             " LEFT JOIN " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT RefType, RefTransID, " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(0) & "],0)) AS C1,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(1) & "],0)) AS C2,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(2) & "],0)) AS C3,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(3) & "],0)) AS C4,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(4) & "],0)) AS C5 " & vbCrLf &
                             " 	FROM  " & vbCrLf &
                             " 	( " & vbCrLf &
                             " 		SELECT	 RefType, RefTransID, AccntCode, SUM(Debit-Credit) AS Amount " & vbCrLf &
                             " 		FROM	 view_GL  " & vbCrLf &
                             " 		WHERE	 AccntCode IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                             " 		AND		 Book ='" & Book & "'  " & vbCrLf &
                             " 		AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' AND Debit + Credit <> 0  " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                             " 		GROUP BY RefType, RefTransID, AccntCode " & vbCrLf &
                             " 	) AS TopAccount " & vbCrLf &
                             " 	PIVOT (SUM(Amount) FOR [AccntCode]  " & vbCrLf &
                             " 				IN ([" & Codes(0) & "], [" & Codes(1) & "], [" & Codes(2) & "], [" & Codes(3) & "], [" & Codes(4) & "]) " & vbCrLf &
                             " 			)  AS pvt " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID " & vbCrLf &
                             " ) AS TopAccountColumns " & vbCrLf &
                             " ON	Reference.RefType = TopAccountColumns.RefType " & vbCrLf &
                             " AND	Reference.RefTransID = TopAccountColumns.RefTransID " & vbCrLf &
                             " LEFT JOIN  " & vbCrLf &
                             " ( " & vbCrLf &
                             " SELECT RefType, RefTransID, AccntCode, AccntTitle, Debit, Credit, ROW_NUMBER() OVER (PARTITION BY RefType, RefTransID ORDER BY Debit DESC, Credit Asc) AS RowID FROM  " & vbCrLf &
                             " ( " & vbCrLf &
                             " 		SELECT	 RefType, RefTransID, AccntCode, AccntTitle,    " & vbCrLf &
                             "   CASE WHEN SUM(Debit) > SUM(Credit) THEN  SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit," & vbCrLf &
                             "   CASE WHEN SUM(Credit) > SUM(Debit) THEN  SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit" & vbCrLf &
                             " 	FROM	 view_GL  " & vbCrLf &
                             " 	WHERE	 AccntCode NOT  IN ( SELECT AccntCode FROM tblDefaultAccount WHERE Type ='AP')   AND AccntCode NOT IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                             " 	AND		 Book ='" & Book & "'  " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & vbCrLf &
                             " 	AND     Debit + Credit <> 0   AND isUpload = 1 " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                             "   GROUP BY RefType, RefTransID, AccntCode, AccntTitle " & vbCrLf &
                             " UNION ALL " & vbCrLf &
                             " 	SELECT	 RefType, RefTransID, view_GL.AccntCode,  view_GL.AccntTitle,   " & vbCrLf &
                             "   CASE WHEN SUM(Debit) > SUM(Credit) THEN  SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit," & vbCrLf &
                             "   CASE WHEN SUM(Credit) > SUM(Debit) THEN  SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit" & vbCrLf &
                             " 	FROM	 view_GL LEFT JOIN tblAPV  " & vbCrLf &
                             "  ON       view_GL.RefTransiD = tblAPV.TransID " & vbCrLf &
                             "  AND      view_GL.RefType ='APV' " & vbCrLf &
                             "  AND      tblAPV.AccntCode = view_GL.AccntCode " & vbCrLf &
                             " 	WHERE	 tblAPV.AccntCode IS NULL AND Book ='" & Book & "'   AND view_GL.AccntCode NOT IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED'") & vbCrLf &
                             "  AND      Debit + Credit <> 0 AND isUpload = 0 " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID, view_GL.AccntCode,  view_GL.AccntTitle,  Book " & vbCrLf &
                               " ) AS SundryAccounts " & vbCrLf &
                             " ) AS Sundries " & vbCrLf &
                             " ON	Reference.RefType = Sundries.RefType " & vbCrLf &
                             " AND	Reference.RefTransID = Sundries.RefTransID " & vbCrLf &
                             " LEFT JOIN viewVCE_Master " & vbCrLf &
                             " ON   Reference.VCECode = viewVCE_Master.VCECode "
                    SQL.ExecNonQuery(Query)
                End If
            Case "General Journal"
                If cbCostCenter.Text <> "ALL" Then
                    Query = " INSERT rptBooks(Date, RefNo,  VCECode, VCEName, Particulars, Account, Amount, C1, C2, C3, C4, C5, AccntTitle, Debit, Credit, Reference.Status, CostCenter)" & vbCrLf &
                            " SELECT  AppDate, Reference.RefType + ':' + Reference.TransNo AS Ref, " & vbCrLf &
                            " 		ISNULL(CIB.VCECode, Reference.VCECode) AS VCECode, viewVCE_Master.VCEName, Particulars, " & vbCrLf &
                            " 		CIB.AccntTitle  AS Account,  " & vbCrLf &
                            " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  CIB.Amount ELSE 0 END AS Amount, " & vbCrLf &
                            " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C1 ELSE 0 END AS C1, " & vbCrLf &
                            " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C2 ELSE 0 END AS C2, " & vbCrLf &
                            " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C3 ELSE 0 END AS C3, " & vbCrLf &
                            " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C4 ELSE 0 END AS C4, " & vbCrLf &
                            " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C5 ELSE 0 END AS C5, " & vbCrLf &
                            "       Sundries.AccntTitle, Debit, Credit, Reference.Status, CostCenter " & vbCrLf &
                            " FROM " & vbCrLf &
                            " ( " & vbCrLf &
                            " 	SELECT 	 AppDate, RefType, RefTransID, TransNo, Check_No, MAX(VCECode) AS VCECode, ISNULL(Remarks, MAX(Particulars)) AS Particulars, Status, CostCenter " & vbCrLf &
                            " 	FROM	 view_GL   " & vbCrLf &
                            " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                            " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED' AND CostCenter = '" & cbCostCenter.Text & "'") & vbCrLf &
                            " 	GROUP BY AppDate, RefType, RefTransID, Check_No, Remarks, TransNo, Status, CostCenter " & vbCrLf &
                            " ) AS Reference " & vbCrLf &
                            " LEFT JOIN " & vbCrLf &
                            " ( " & vbCrLf &
                            " 	SELECT	 RefType, RefTransID, VCECode, view_GL.AccntTitle, SUM(Credit-Debit) AS Amount  " & vbCrLf &
                            " 	FROM	 view_GL " &
                            " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                            " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED' AND CostCenter = '" & cbCostCenter.Text & "'") & vbCrLf &
                            " 	AND      AccntCode IN ( '')  " &
                            "  AND      Debit + Credit <> 0 " & vbCrLf &
                            " 	GROUP BY RefType, RefTransID, view_GL.AccntTitle, VCECode, Book " & vbCrLf &
                            " ) AS CIB " & vbCrLf &
                            " ON	Reference.RefType = CIB.RefType " & vbCrLf &
                            " AND	Reference.RefTransID = CIB.RefTransID " & vbCrLf &
                            " LEFT JOIN " & vbCrLf &
                            " ( " & vbCrLf &
                            " 	SELECT RefType, RefTransID, " & vbCrLf &
                            " 			0 AS C1,  " & vbCrLf &
                            " 			0 AS C2,  " & vbCrLf &
                            " 			0 AS C3,  " & vbCrLf &
                            " 			0 AS C4,  " & vbCrLf &
                            " 			0 AS C5 " & vbCrLf &
                            " 	FROM  " & vbCrLf &
                            " 	( " & vbCrLf &
                            " 		SELECT	 RefType, RefTransID, AccntCode, SUM(Debit-Credit) AS Amount " & vbCrLf &
                            " 		FROM	 view_GL  " & vbCrLf &
                            " 		WHERE	 AccntCode IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                            " 		AND		 Book ='" & Book & "'  " & vbCrLf &
                            " 		AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' AND Debit + Credit <> 0  " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED' AND CostCenter = '" & cbCostCenter.Text & "'") & vbCrLf &
                            " 		GROUP BY RefType, RefTransID, AccntCode " & vbCrLf &
                            " 	) AS TopAccount " & vbCrLf &
                            " 	PIVOT (SUM(Amount) FOR [AccntCode]  " & vbCrLf &
                            " 				IN ([C1], [C2], [C3], [C4], [C5]) " & vbCrLf &
                            " 			)  AS pvt " & vbCrLf &
                            " 	GROUP BY RefType, RefTransID " & vbCrLf &
                            " ) AS TopAccountColumns " & vbCrLf &
                            " ON	Reference.RefType = TopAccountColumns.RefType " & vbCrLf &
                            " AND	Reference.RefTransID = TopAccountColumns.RefTransID " & vbCrLf &
                            " INNER JOIN  --Changed to inner join to optimize query speed" & vbCrLf &
                            " ( " & vbCrLf &
                            " SELECT RefType, RefTransID, AccntCode, AccntTitle, Debit, Credit, ROW_NUMBER() OVER (PARTITION BY RefType, RefTransID ORDER BY Debit DESC, Credit Asc) AS RowID FROM  " & vbCrLf &
                            " ( " & vbCrLf &
                            " 		SELECT	 RefType, RefTransID, AccntCode, AccntTitle,    " & vbCrLf &
                            "   CASE WHEN SUM(Debit) > SUM(Credit) THEN  SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit," & vbCrLf &
                            "   CASE WHEN SUM(Credit) > SUM(Debit) THEN  SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit" & vbCrLf &
                            " 	FROM	 view_GL  " & vbCrLf &
                            " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                            " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & vbCrLf &
                            " 	AND     Debit + Credit <> 0  " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                            "   GROUP BY RefType, RefTransID, AccntCode, AccntTitle " & vbCrLf &
                              " ) AS SundryAccounts " & vbCrLf &
                            " ) AS Sundries " & vbCrLf &
                            " ON	Reference.RefType = Sundries.RefType " & vbCrLf &
                            " AND	Reference.RefTransID = Sundries.RefTransID " & vbCrLf &
                            " LEFT JOIN viewVCE_Master " & vbCrLf &
                            " ON  ISNULL(CIB.VCECode, Reference.VCECode) = viewVCE_Master.VCECode "
                    SQL.ExecNonQuery(Query)
                    '  f.ShowDialog("Books_GJ", Book, dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, UserID)
                Else
                    Query = " INSERT rptBooks(Date, RefNo,  VCECode, VCEName, Particulars, Account, Amount, C1, C2, C3, C4, C5, AccntTitle, Debit, Credit, Reference.Status)" & vbCrLf &
                            " SELECT AppDate, Reference.RefType + ':' + Reference.TransNo AS Ref, " & vbCrLf &
                            " 		ISNULL(CIB.VCECode, Reference.VCECode) AS VCECode, viewVCE_Master.VCEName, Particulars, " & vbCrLf &
                            " 		CIB.AccntTitle  AS Account,  " & vbCrLf &
                            " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  CIB.Amount ELSE 0 END AS Amount, " & vbCrLf &
                            " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C1 ELSE 0 END AS C1, " & vbCrLf &
                            " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C2 ELSE 0 END AS C2, " & vbCrLf &
                            " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C3 ELSE 0 END AS C3, " & vbCrLf &
                            " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C4 ELSE 0 END AS C4, " & vbCrLf &
                            " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C5 ELSE 0 END AS C5, " & vbCrLf &
                            "       Sundries.AccntTitle, Debit, Credit, Reference.Status " & vbCrLf &
                            " FROM " & vbCrLf &
                            " ( " & vbCrLf &
                            " 	SELECT 	 AppDate, RefType, RefTransID, TransNo, Check_No, MAX(VCECode) AS VCECode, ISNULL(Remarks, MAX(Particulars)) AS Particulars, Status " & vbCrLf &
                            " 	FROM	 view_GL   " & vbCrLf &
                            " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                            " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                            " 	GROUP BY AppDate, RefType, RefTransID, Check_No, Remarks, TransNo, Status " & vbCrLf &
                            " ) AS Reference " & vbCrLf &
                            " LEFT JOIN " & vbCrLf &
                            " ( " & vbCrLf &
                            " 	SELECT	 RefType, RefTransID, VCECode, view_GL.AccntTitle, SUM(Credit-Debit) AS Amount  " & vbCrLf &
                            " 	FROM	 view_GL " &
                            " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                            " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED'") & vbCrLf &
                            " 	AND      AccntCode IN ( '')  " &
                            "  AND      Debit + Credit <> 0 " & vbCrLf &
                            " 	GROUP BY RefType, RefTransID, view_GL.AccntTitle, VCECode, Book " & vbCrLf &
                            " ) AS CIB " & vbCrLf &
                            " ON	Reference.RefType = CIB.RefType " & vbCrLf &
                            " AND	Reference.RefTransID = CIB.RefTransID " & vbCrLf &
                            " LEFT JOIN " & vbCrLf &
                            " ( " & vbCrLf &
                            " 	SELECT RefType, RefTransID, " & vbCrLf &
                            " 			0 AS C1,  " & vbCrLf &
                            " 			0 AS C2,  " & vbCrLf &
                            " 			0 AS C3,  " & vbCrLf &
                            " 			0 AS C4,  " & vbCrLf &
                            " 			0 AS C5 " & vbCrLf &
                            " 	FROM  " & vbCrLf &
                            " 	( " & vbCrLf &
                            " 		SELECT	 RefType, RefTransID, AccntCode, SUM(Debit-Credit) AS Amount " & vbCrLf &
                            " 		FROM	 view_GL  " & vbCrLf &
                            " 		WHERE	 AccntCode IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                            " 		AND		 Book ='" & Book & "'  " & vbCrLf &
                            " 		AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' AND Debit + Credit <> 0  " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                            " 		GROUP BY RefType, RefTransID, AccntCode " & vbCrLf &
                            " 	) AS TopAccount " & vbCrLf &
                            " 	PIVOT (SUM(Amount) FOR [AccntCode]  " & vbCrLf &
                            " 				IN ([C1], [C2], [C3], [C4], [C5]) " & vbCrLf &
                            " 			)  AS pvt " & vbCrLf &
                            " 	GROUP BY RefType, RefTransID " & vbCrLf &
                            " ) AS TopAccountColumns " & vbCrLf &
                            " ON	Reference.RefType = TopAccountColumns.RefType " & vbCrLf &
                            " AND	Reference.RefTransID = TopAccountColumns.RefTransID " & vbCrLf &
                            "  INNER JOIN  --Changed to inner join to optimize query speed  " & vbCrLf &
                            " ( " & vbCrLf &
                            " SELECT RefType, RefTransID, AccntCode, AccntTitle, Debit, Credit, ROW_NUMBER() OVER (PARTITION BY RefType, RefTransID ORDER BY Debit DESC, Credit Asc) AS RowID FROM  " & vbCrLf &
                            " ( " & vbCrLf &
                            " 		SELECT	 RefType, RefTransID, AccntCode, AccntTitle,    " & vbCrLf &
                            "   CASE WHEN SUM(Debit) > SUM(Credit) THEN  SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit," & vbCrLf &
                            "   CASE WHEN SUM(Credit) > SUM(Debit) THEN  SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit" & vbCrLf &
                            " 	FROM	 view_GL  " & vbCrLf &
                            " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                            " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & vbCrLf &
                            " 	AND     Debit + Credit <> 0  " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                            "   GROUP BY RefType, RefTransID, AccntCode, AccntTitle " & vbCrLf &
                              " ) AS SundryAccounts " & vbCrLf &
                            " ) AS Sundries " & vbCrLf &
                            " ON	Reference.RefType = Sundries.RefType " & vbCrLf &
                            " AND	Reference.RefTransID = Sundries.RefTransID " & vbCrLf &
                            " LEFT JOIN viewVCE_Master " & vbCrLf &
                            " ON  ISNULL(CIB.VCECode, Reference.VCECode) = viewVCE_Master.VCECode "
                    SQL.ExecNonQuery(Query)
                End If
            Case "Sales Book"
                If cbCostCenter.Text <> "ALL" Then
                    Query = " INSERT rptBooks(Date, RefNo,  VCECode, VCEName, Particulars, Account, Amount, C1, C2, C3, C4, C5, AccntTitle, Debit, Credit, Reference.Status, Costcenter)" & vbCrLf &
                             " SELECT  AppDate, Reference.RefType + ':' + Reference.TransNo AS Ref, " & vbCrLf &
                             " 		ISNULL(CIB.VCECode, Reference.VCECode) AS VCECode, viewVCE_Master.VCEName, Particulars, " & vbCrLf &
                             " 		CIB.AccntTitle  AS Account,  " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  CIB.Amount ELSE 0 END AS Amount, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C1 ELSE 0 END AS C1, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C2 ELSE 0 END AS C2, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C3 ELSE 0 END AS C3, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C4 ELSE 0 END AS C4, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C5 ELSE 0 END AS C5, " & vbCrLf &
                             "       Sundries.AccntTitle, Debit, Credit, Reference.Status, CostCenter " & vbCrLf &
                             " FROM " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT 	 AppDate, RefType, RefTransID, TransNo, Check_No, MAX(VCECode) AS VCECode, ISNULL(Remarks, MAX(Particulars)) AS Particulars, Status, CostCenter " & vbCrLf &
                             " 	FROM	 view_GL   " & vbCrLf &
                             " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED' AND CostCenter = '" & cbCostCenter.Text & "'") & vbCrLf &
                             " 	GROUP BY AppDate, RefType, RefTransID, Check_No, Remarks, TransNo, Status , CostCenter" & vbCrLf &
                             " ) AS Reference " & vbCrLf &
                             " LEFT JOIN " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT	 RefType, RefTransID, VCECode, view_GL.AccntTitle, SUM(Credit-Debit) AS Amount  " & vbCrLf &
                             " 	FROM	 view_GL " &
                             " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED' AND CostCenter = '" & cbCostCenter.Text & "'") & vbCrLf &
                             " 	AND      AccntCode IN ('4010000')  " &
                             "  AND      Debit + Credit <> 0 " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID, view_GL.AccntTitle, VCECode, Book " & vbCrLf &
                             " ) AS CIB " & vbCrLf &
                             " ON	Reference.RefType = CIB.RefType " & vbCrLf &
                             " AND	Reference.RefTransID = CIB.RefTransID " & vbCrLf &
                             " LEFT JOIN " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT RefType, RefTransID, " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(0) & "],0)) AS C1,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(1) & "],0)) AS C2,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(2) & "],0)) AS C3,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(3) & "],0)) AS C4,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(4) & "],0)) AS C5 " & vbCrLf &
                             " 	FROM  " & vbCrLf &
                             " 	( " & vbCrLf &
                             " 		SELECT	 RefType, RefTransID, AccntCode, SUM(Debit-Credit) AS Amount " & vbCrLf &
                             " 		FROM	 view_GL  " & vbCrLf &
                             " 		WHERE	 AccntCode IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                             " 		AND		 Book ='" & Book & "'  " & vbCrLf &
                             " 		AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' AND Debit + Credit <> 0  " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED' AND CostCenter = '" & cbCostCenter.Text & "'") & vbCrLf &
                             " 		GROUP BY RefType, RefTransID, AccntCode " & vbCrLf &
                             " 	) AS TopAccount " & vbCrLf &
                             " 	PIVOT (SUM(Amount) FOR [AccntCode]  " & vbCrLf &
                             " 				IN ([" & Codes(0) & "], [" & Codes(1) & "], [" & Codes(2) & "], [" & Codes(3) & "], [" & Codes(4) & "]) " & vbCrLf &
                             " 			)  AS pvt " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID " & vbCrLf &
                             " ) AS TopAccountColumns " & vbCrLf &
                             " ON	Reference.RefType = TopAccountColumns.RefType " & vbCrLf &
                             " AND	Reference.RefTransID = TopAccountColumns.RefTransID " & vbCrLf &
                             " LEFT JOIN  " & vbCrLf &
                             " ( " & vbCrLf &
                             " SELECT RefType, RefTransID, AccntCode, AccntTitle, Debit, Credit, ROW_NUMBER() OVER (PARTITION BY RefType, RefTransID ORDER BY Debit DESC, Credit Asc) AS RowID FROM  " & vbCrLf &
                             " ( " & vbCrLf &
                             " 		SELECT	 RefType, RefTransID, AccntCode, AccntTitle,    " & vbCrLf &
                             "   CASE WHEN SUM(Debit) > SUM(Credit) THEN  SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit," & vbCrLf &
                             "   CASE WHEN SUM(Credit) > SUM(Debit) THEN  SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit" & vbCrLf &
                             " 	FROM	 view_GL  " & vbCrLf &
                             " 	WHERE	 AccntCode NOT  IN ( '4010000')   AND AccntCode NOT IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                             " 	AND		 Book ='" & Book & "'  " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & vbCrLf &
                             " 	AND     Debit + Credit <> 0  " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED' AND CostCenter = '" & cbCostCenter.Text & "'") & vbCrLf &
                             "   GROUP BY RefType, RefTransID, AccntCode, AccntTitle " & vbCrLf &
                               " ) AS SundryAccounts " & vbCrLf &
                             " ) AS Sundries " & vbCrLf &
                             " ON	Reference.RefType = Sundries.RefType " & vbCrLf &
                             " AND	Reference.RefTransID = Sundries.RefTransID " & vbCrLf &
                             " LEFT JOIN viewVCE_Master " & vbCrLf &
                             " ON  ISNULL(CIB.VCECode, Reference.VCECode) = viewVCE_Master.VCECode "
                    SQL.ExecNonQuery(Query)
                    '  f.ShowDialog("Books_SB", Book, dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, UserID)
                Else
                    Query = " INSERT rptBooks(Date, RefNo,  VCECode, VCEName, Particulars, Account, Amount, C1, C2, C3, C4, C5, AccntTitle, Debit, Credit, Reference.Status)" & vbCrLf &
                             " SELECT  AppDate, Reference.RefType + ':' + Reference.TransNo AS Ref, " & vbCrLf &
                             " 		ISNULL(CIB.VCECode, Reference.VCECode) AS VCECode, viewVCE_Master.VCEName, Particulars, " & vbCrLf &
                             " 		CIB.AccntTitle  AS Account,  " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  CIB.Amount ELSE 0 END AS Amount, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C1 ELSE 0 END AS C1, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C2 ELSE 0 END AS C2, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C3 ELSE 0 END AS C3, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C4 ELSE 0 END AS C4, " & vbCrLf &
                             " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C5 ELSE 0 END AS C5, " & vbCrLf &
                             "       Sundries.AccntTitle, Debit, Credit, Reference.Status " & vbCrLf &
                             " FROM " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT 	 AppDate, RefType, RefTransID, TransNo, Check_No, MAX(VCECode) AS VCECode, ISNULL(Remarks, MAX(Particulars)) AS Particulars, Status " & vbCrLf &
                             " 	FROM	 view_GL   " & vbCrLf &
                             " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                             " 	GROUP BY AppDate, RefType, RefTransID, Check_No, Remarks, TransNo, Status " & vbCrLf &
                             " ) AS Reference " & vbCrLf &
                             " LEFT JOIN " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT	 RefType, RefTransID, VCECode, view_GL.AccntTitle, SUM(Credit-Debit) AS Amount  " & vbCrLf &
                             " 	FROM	 view_GL " &
                             " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED'") & vbCrLf &
                             " 	AND      AccntCode IN ('4010000')  " &
                             "  AND      Debit + Credit <> 0 " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID, view_GL.AccntTitle, VCECode, Book " & vbCrLf &
                             " ) AS CIB " & vbCrLf &
                             " ON	Reference.RefType = CIB.RefType " & vbCrLf &
                             " AND	Reference.RefTransID = CIB.RefTransID " & vbCrLf &
                             " LEFT JOIN " & vbCrLf &
                             " ( " & vbCrLf &
                             " 	SELECT RefType, RefTransID, " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(0) & "],0)) AS C1,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(1) & "],0)) AS C2,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(2) & "],0)) AS C3,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(3) & "],0)) AS C4,  " & vbCrLf &
                             " 			SUM(ISNULL([" & Codes(4) & "],0)) AS C5 " & vbCrLf &
                             " 	FROM  " & vbCrLf &
                             " 	( " & vbCrLf &
                             " 		SELECT	 RefType, RefTransID, AccntCode, SUM(Debit-Credit) AS Amount " & vbCrLf &
                             " 		FROM	 view_GL  " & vbCrLf &
                             " 		WHERE	 AccntCode IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                             " 		AND		 Book ='" & Book & "'  " & vbCrLf &
                             " 		AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' AND Debit + Credit <> 0  " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                             " 		GROUP BY RefType, RefTransID, AccntCode " & vbCrLf &
                             " 	) AS TopAccount " & vbCrLf &
                             " 	PIVOT (SUM(Amount) FOR [AccntCode]  " & vbCrLf &
                             " 				IN ([" & Codes(0) & "], [" & Codes(1) & "], [" & Codes(2) & "], [" & Codes(3) & "], [" & Codes(4) & "]) " & vbCrLf &
                             " 			)  AS pvt " & vbCrLf &
                             " 	GROUP BY RefType, RefTransID " & vbCrLf &
                             " ) AS TopAccountColumns " & vbCrLf &
                             " ON	Reference.RefType = TopAccountColumns.RefType " & vbCrLf &
                             " AND	Reference.RefTransID = TopAccountColumns.RefTransID " & vbCrLf &
                             " LEFT JOIN  " & vbCrLf &
                             " ( " & vbCrLf &
                             " SELECT RefType, RefTransID, AccntCode, AccntTitle, Debit, Credit, ROW_NUMBER() OVER (PARTITION BY RefType, RefTransID ORDER BY Debit DESC, Credit Asc) AS RowID FROM  " & vbCrLf &
                             " ( " & vbCrLf &
                             " 		SELECT	 RefType, RefTransID, AccntCode, AccntTitle,    " & vbCrLf &
                             "   CASE WHEN SUM(Debit) > SUM(Credit) THEN  SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit," & vbCrLf &
                             "   CASE WHEN SUM(Credit) > SUM(Debit) THEN  SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit" & vbCrLf &
                             " 	FROM	 view_GL  " & vbCrLf &
                             " 	WHERE	 AccntCode NOT  IN ( '4010000')   AND AccntCode NOT IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                             " 	AND		 Book ='" & Book & "'  " & vbCrLf &
                             " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & vbCrLf &
                             " 	AND     Debit + Credit <> 0  " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                             "   GROUP BY RefType, RefTransID, AccntCode, AccntTitle " & vbCrLf &
                               " ) AS SundryAccounts " & vbCrLf &
                             " ) AS Sundries " & vbCrLf &
                             " ON	Reference.RefType = Sundries.RefType " & vbCrLf &
                             " AND	Reference.RefTransID = Sundries.RefTransID " & vbCrLf &
                             " LEFT JOIN viewVCE_Master " & vbCrLf &
                             " ON  ISNULL(CIB.VCECode, Reference.VCECode) = viewVCE_Master.VCECode "
                    SQL.ExecNonQuery(Query)
                End If
            Case "Inventory Ledger"
                Query = " INSERT rptBooks(Date, RefNo,  VCECode, VCEName, Particulars, Account, Amount, C1, C2, C3, C4, C5, AccntTitle, Debit, Credit, Reference.Status)" & vbCrLf &
                " SELECT  AppDate, Reference.RefType + ':' + Reference.TransNo AS Ref, " & vbCrLf &
                " 		ISNULL(CIB.VCECode, Reference.VCECode) AS VCECode, viewVCE_Master.VCEName, Particulars, " & vbCrLf &
                " 		CIB.AccntTitle  AS Account,  " & vbCrLf &
                " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  CIB.Amount ELSE 0 END AS Amount, " & vbCrLf &
                " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C1 ELSE 0 END AS C1, " & vbCrLf &
                " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C2 ELSE 0 END AS C2, " & vbCrLf &
                " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C3 ELSE 0 END AS C3, " & vbCrLf &
                " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C4 ELSE 0 END AS C4, " & vbCrLf &
                " 		CASE WHEN ISNULL(Sundries.RowID,1) = 1 THEN  C5 ELSE 0 END AS C5, " & vbCrLf &
                "       Sundries.AccntTitle, Debit, Credit, Reference.Status " & vbCrLf &
                " FROM " & vbCrLf &
                " ( " & vbCrLf &
                " 	SELECT 	 AppDate, RefType, RefTransID, TransNo, Check_No, MAX(VCECode) AS VCECode, ISNULL(Remarks, MAX(Particulars)) AS Particulars, Status " & vbCrLf &
                " 	FROM	 view_GL   " & vbCrLf &
                " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                " 	GROUP BY AppDate, RefType, RefTransID, Check_No, Remarks, TransNo, Status " & vbCrLf &
                " ) AS Reference " & vbCrLf &
                " LEFT JOIN " & vbCrLf &
                " ( " & vbCrLf &
                " 	SELECT	 RefType, RefTransID, VCECode, view_GL.AccntTitle, SUM(Credit-Debit) AS Amount  " & vbCrLf &
                " 	FROM	 view_GL " &
                " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(chkInclude.Checked = True, "", " AND view_GL.STATUS <> 'CANCELLED'") & vbCrLf &
                " 	AND      AccntCode IN ( '')  " &
                "  AND      Debit + Credit <> 0 " & vbCrLf &
                " 	GROUP BY RefType, RefTransID, view_GL.AccntTitle, VCECode, Book " & vbCrLf &
                " ) AS CIB " & vbCrLf &
                " ON	Reference.RefType = CIB.RefType " & vbCrLf &
                " AND	Reference.RefTransID = CIB.RefTransID " & vbCrLf &
                " LEFT JOIN " & vbCrLf &
                " ( " & vbCrLf &
                " 	SELECT RefType, RefTransID, " & vbCrLf &
                " 			0 AS C1,  " & vbCrLf &
                " 			0 AS C2,  " & vbCrLf &
                " 			0 AS C3,  " & vbCrLf &
                " 			0 AS C4,  " & vbCrLf &
                " 			0 AS C5 " & vbCrLf &
                " 	FROM  " & vbCrLf &
                " 	( " & vbCrLf &
                " 		SELECT	 RefType, RefTransID, AccntCode, SUM(Debit-Credit) AS Amount " & vbCrLf &
                " 		FROM	 view_GL  " & vbCrLf &
                " 		WHERE	 AccntCode IN (SELECT AccntCode FROM rptBooks_Header) " & vbCrLf &
                " 		AND		 Book ='" & Book & "'  " & vbCrLf &
                " 		AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' AND Debit + Credit <> 0  " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                " 		GROUP BY RefType, RefTransID, AccntCode " & vbCrLf &
                " 	) AS TopAccount " & vbCrLf &
                " 	PIVOT (SUM(Amount) FOR [AccntCode]  " & vbCrLf &
                " 				IN ([C1], [C2], [C3], [C4], [C5]) " & vbCrLf &
                " 			)  AS pvt " & vbCrLf &
                " 	GROUP BY RefType, RefTransID " & vbCrLf &
                " ) AS TopAccountColumns " & vbCrLf &
                " ON	Reference.RefType = TopAccountColumns.RefType " & vbCrLf &
                " AND	Reference.RefTransID = TopAccountColumns.RefTransID " & vbCrLf &
                " LEFT JOIN  " & vbCrLf &
                " ( " & vbCrLf &
                " SELECT RefType, RefTransID, AccntCode, AccntTitle, Debit, Credit, ROW_NUMBER() OVER (PARTITION BY RefType, RefTransID ORDER BY Debit DESC, Credit Asc) AS RowID FROM  " & vbCrLf &
                " ( " & vbCrLf &
                " 		SELECT	 RefType, RefTransID, AccntCode, AccntTitle,    " & vbCrLf &
                "   CASE WHEN SUM(Debit) > SUM(Credit) THEN  SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit," & vbCrLf &
                "   CASE WHEN SUM(Credit) > SUM(Debit) THEN  SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit" & vbCrLf &
                " 	FROM	 view_GL  " & vbCrLf &
                " 	WHERE	 Book ='" & Book & "'  " & vbCrLf &
                " 	AND		 AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "' " & vbCrLf &
                " 	AND     Debit + Credit <> 0  " & IIf(chkInclude.Checked = True, "", " AND STATUS <> 'CANCELLED'") & vbCrLf &
                "   GROUP BY RefType, RefTransID, AccntCode, AccntTitle " & vbCrLf &
                  " ) AS SundryAccounts " & vbCrLf &
                " ) AS Sundries " & vbCrLf &
                " ON	Reference.RefType = Sundries.RefType " & vbCrLf &
                " AND	Reference.RefTransID = Sundries.RefTransID " & vbCrLf &
                " LEFT JOIN viewVCE_Master " & vbCrLf &
                " ON  ISNULL(CIB.VCECode, Reference.VCECode) = viewVCE_Master.VCECode "
                SQL.ExecNonQuery(Query)
                '  f.ShowDialog("Books_Inventory", Book, dtpFrom.Value.Date, dtpTo.Value.Date, chkInclude.Checked, UserID)
        End Select


    End Sub

    Private Sub LoadAccountsPerType(cb As ComboBox, Filter As String)
        Dim query As String
        query = " SELECT DISTINCT AccountTitle FROM tblCOA_Master WHERE AccountGroup = 'SubAccount' AND AccountType = '" & Filter & "' ORDER BY AccountTitle "
        SQL.ReadQuery(query)
        cb.Items.Clear()
        'If cb Is cbFilter Then
        '    cb.Items.Add("ALL")
        'End If
        While SQL.SQLDR.Read
            cb.Items.Add(SQL.SQLDR("AccountTitle").ToString)
        End While
        If cb.Items.Count > 0 Then
            cb.SelectedIndex = 0
        End If
        cb.Enabled = True
    End Sub
    Private Sub LoadVCEName(cb As ComboBox)
        Dim query As String
        query = " SELECT VCEName  FROM viewVCE_master WHERE Status <> 'Inactive' ORDER BY VCEName "
        SQL.ReadQuery(query)
        cb.Items.Clear()
        If cb Is cbFilter Then
            cb.Items.Add("ALL")
        End If
        cb.Items.Add("ALL")
        While SQL.SQLDR.Read
            cb.Items.Add(SQL.SQLDR("VCEName").ToString)
        End While
        If cb.Items.Count > 0 Then
            cb.SelectedIndex = 0
        End If
        cb.Enabled = True
    End Sub

    Private Sub LoadAccounts(cb As ComboBox)
        If cbReportType.SelectedItem = "Income Statement - Per Group" Then
            Dim query As String
            query = " SELECT DISTINCT AccountTitle FROM tblCOA_Master WHERE AccountGroup = 'MainAccount' AND AccountType = 'Income Statement' ORDER BY AccountTitle "
            SQL.ReadQuery(query)
            cb.Items.Clear()
            While SQL.SQLDR.Read
                cb.Items.Add(SQL.SQLDR("AccountTitle").ToString)
            End While
            If cb.Items.Count > 0 Then
                cb.SelectedIndex = 0
            End If
            cb.Enabled = True
        ElseIf cbReportType.SelectedItem = "Balance Sheet - Per Group" Then
            Dim query As String
            query = " SELECT DISTINCT AccountTitle FROM tblCOA_Master WHERE AccountGroup = 'MainAccount' AND AccountType = 'Balance Sheet' ORDER BY AccountTitle "
            SQL.ReadQuery(query)
            cb.Items.Clear()
            While SQL.SQLDR.Read
                cb.Items.Add(SQL.SQLDR("AccountTitle").ToString)
            End While
            If cb.Items.Count > 0 Then
                cb.SelectedIndex = 0
            End If
            cb.Enabled = True
        Else
            Dim query As String
            query = " SELECT DISTINCT AccountTitle FROM tblCOA_Master WHERE AccountGroup = 'SubAccount'  ORDER BY AccountTitle "
            SQL.ReadQuery(query)
            cb.Items.Clear()
            If cb Is cbFilter Then
                cb.Items.Add("ALL")
            End If
            While SQL.SQLDR.Read
                cb.Items.Add(SQL.SQLDR("AccountTitle").ToString)
            End While
            If cb.Items.Count > 0 Then
                cb.SelectedIndex = 0
            End If
            cb.Enabled = True

        End If

    End Sub

    Private Sub LoadMembType()
        lvFilter.Items.Clear()
        lvFilter.Items.Add("All")
        lvFilter.Items.Add("Member")
        lvFilter.Items.Add("Vendor")
        lvFilter.Items.Add("Customer")
        lvFilter.Items.Add("Employee")
        lvFilter.Items.Add("Others")
    End Sub

    Private Sub LoadAOE()
        Dim query As String
        query = " SELECT  AccountTitle FROM tblCOA_Master  WHERE AccountCode between '1130301' AND '1130400'  "
        SQL.ReadQuery(query)
        lvFilter2.Items.Clear()
        While SQL.SQLDR.Read
            lvFilter2.Items.Add(SQL.SQLDR("AccountTitle").ToString)
        End While
    End Sub

    Private Sub LoadLoanType()
        Dim query As String
        query = " SELECT LoanType FROM tblLoan_Type WHERE Status = 'Active'  ORDER BY LoanType "
        SQL.ReadQuery(query)
        cmbLoanType.Items.Clear()
        While SQL.SQLDR.Read
            cmbLoanType.Items.Add(SQL.SQLDR("LoanType").ToString)
        End While
    End Sub

    Private Sub LoadSavingsType()
        Dim query As String
        query = " SELECT DISTINCT DepositType FROM tblSavings_Account  ORDER BY DepositType "
        SQL.ReadQuery(query)
        cmbLoanType.Items.Clear()
        While SQL.SQLDR.Read
            cmbLoanType.Items.Add(SQL.SQLDR("DepositType").ToString)
        End While
    End Sub

    Private Sub LoadLoanAccount()
        Dim query As String
        query = " SELECT  DISTINCT LoanAccount, AccountTitle from tblLoan_Type " &
                " INNER JOIN " &
                " tblCOA_Master ON " &
                " tblCOA_Master.AccountCode = tblLoan_Type.LoanAccount " &
                " WHERE tblLoan_Type.Status = 'Active' " &
                " ORDER BY AccountTitle"
        SQL.ReadQuery(query)
        cmbLoanType.Items.Clear()
        While SQL.SQLDR.Read
            cmbLoanType.Items.Add(SQL.SQLDR("AccountTitle").ToString)
        End While
    End Sub

    Private Sub GenerateGL()
        Dim i As Integer = 1
        Dim insertSQl As String
        Dim deleteSQL As String
        deleteSQL = " DELETE FROM tblPrint_TB  "
        SQL.ExecNonQuery(deleteSQL)
        For Each item As ListViewItem In lvFilter.Items
            If item.Checked = True Then
                insertSQl = " INSERT INTO tblPrint_TB(Code) " &
                            " VALUES (@Code) "
                SQL.FlushParams()
                SQL.AddParam("@Code", GetAccntCode(item.SubItems(0).Text))
                SQL.ExecNonQuery(insertSQl)
            End If
        Next
        If rbSummary.Checked = True Then
            If cbPeriod.SelectedItem = "Yearly" Then
                Dim f As New frmReport_Display
                f.ShowDialog("GENLGRYS", "", nupYear.Value)
                f.Dispose()
            ElseIf cbPeriod.SelectedItem = "Monthly" Then
                Dim f As New frmReport_Display
                f.ShowDialog("GENLGRMS", "", nupYear.Value, cbMonth.SelectedIndex + 1)
                f.Dispose()
            ElseIf cbPeriod.SelectedItem = "Daily" Then
                Dim f As New frmReport_Display
                f.ShowDialog("GENLGRDS", "", dtpFrom.Value.Date)
                f.Dispose()
            ElseIf cbPeriod.SelectedItem = "Date Range" Then
                Dim f As New frmReport_Display
                f.ShowDialog("GENLGRRS", "", dtpFrom.Value.Date, dtpTo.Value.Date)
                f.Dispose()
            End If
        Else
            If cbPeriod.SelectedItem = "Yearly" Then
                Dim f As New frmReport_Display
                f.ShowDialog("GENLGRYD", "", nupYear.Value)
                f.Dispose()
            ElseIf cbPeriod.SelectedItem = "Monthly" Then
                Dim f As New frmReport_Display
                f.ShowDialog("GENLGRMD", "", nupYear.Value, cbMonth.SelectedIndex + 1)
                f.Dispose()
            ElseIf cbPeriod.SelectedItem = "Daily" Then
                Dim f As New frmReport_Display
                f.ShowDialog("GENLGRDD", "", dtpFrom.Value.Date)
                f.Dispose()
            ElseIf cbPeriod.SelectedItem = "Date Range" Then
                Dim f As New frmReport_Display
                f.ShowDialog("GENLGRRD", "", dtpFrom.Value.Date, dtpTo.Value.Date)
                f.Dispose()
            End If
        End If
    End Sub

    Private Sub GenerateTB(ByVal Type As String, ByVal DateFrom As Date, ByVal DateTo As Date, Optional ByVal Filter As String = "")
        Dim insertSQL, deleteSQL As String
        deleteSQL = " DELETE FROM tblPrint_TB "
        SQL.ExecNonQuery(deleteSQL)
        If Type = "Detailed" Then
            If cbCostCenter.SelectedItem = "ALL" Then
                insertSQL = " INSERT INTO tblPRint_TB(Code, Title, BBDR, BBCR, CRDR, CRCR, CDDR, CDCR, SBDR, SBCR, PBDR, PBCR, JVDR, JVCR, IBDR, IBCR, APDR, APCR, TBDR, TBCR) " & vbCrLf &
                        " SELECT  AccountCode, AccountTitle, " & vbCrLf &
                        "       CASE WHEN SUM(BBDR) > SUM(BBCR) THEN SUM(BBDR) - SUM(BBCR) ELSE 0 END AS BBDR, " & vbCrLf &
                        "       CASE WHEN SUM(BBCR) > SUM(BBDR) THEN SUM(BBCR) - SUM(BBDR) ELSE 0 END AS BBCR, " & vbCrLf &
                        "       SUM(CRDR) AS CRDR, " & vbCrLf &
                        "       SUM(CRCR) AS CRCR, " & vbCrLf &
                        "       SUM(CDDR) AS CDDR, " & vbCrLf &
                        "       SUM(CDCR) AS CDCR, " & vbCrLf &
                        "       SUM(SBDR) AS SBDR, " & vbCrLf &
                        "       SUM(SBCR) AS SBCR, " & vbCrLf &
                        "       SUM(PBDR) AS PBDR, " & vbCrLf &
                        "       SUM(PBCR) AS PBCR, " & vbCrLf &
                        "       SUM(JVDR) AS JVDR, " & vbCrLf &
                        "       SUM(JVCR) AS JVCR, " & vbCrLf &
                        "       SUM(IBDR) AS IBDR, " & vbCrLf &
                        "       SUM(IBCR) AS IBCR, " & vbCrLf &
                        "       SUM(APDR) AS APDR, " & vbCrLf &
                        "       SUM(APCR) AS APCR, " & vbCrLf &
                        "       CASE WHEN SUM(TBDR) > SUM(TBCR) THEN SUM(TBDR) - SUM(TBCR) ELSE 0 END AS TBDR, " & vbCrLf &
                        "       CASE WHEN SUM(TBCR) > SUM(TBDR) THEN SUM(TBCR) - SUM(TBDR) ELSE 0 END AS TBCR " & vbCrLf &
                        "FROM " & vbCrLf &
                        "( " & vbCrLf &
                        "   SELECT tblCOA_Master.AccountCode, tblCOA_Master.AccountTitle, " & vbCrLf &
                        "          CASE WHEN AppDate <'" & DateFrom & "' OR Book ='BB' THEN Debit ELSE 0 END AS BBDR, " & vbCrLf &
                        "          CASE WHEN AppDate < '" & DateFrom & "' OR Book ='BB' THEN Credit ELSE 0 END AS BBCR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Receipts' THEN Debit ELSE 0 END AS CRDR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Receipts' THEN Credit ELSE 0 END AS CRCR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Disbursements' THEN Debit ELSE 0 END AS CDDR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Disbursements' THEN Credit ELSE 0 END AS CDCR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Sales' THEN Debit ELSE 0 END AS SBDR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Sales' THEN Credit ELSE 0 END AS SBCR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Purchase Journal' THEN Debit ELSE 0 END AS PBDR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Purchase Journal' THEN Credit ELSE 0 END AS PBCR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='General Journal' THEN Debit ELSE 0 END AS JVDR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='General Journal' THEN Credit ELSE 0 END AS JVCR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Inventory' THEN Debit ELSE 0 END AS IBDR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Inventory' THEN Credit ELSE 0 END AS IBCR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Accounts Payable' THEN Debit ELSE 0 END AS APDR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Accounts Payable' THEN Credit ELSE 0 END AS APCR, " & vbCrLf &
                        "          Debit AS TBDR, " & vbCrLf &
                        "          Credit AS TBCR, view_GL.Status " & vbCrLf &
                        "   FROM view_GL INNER JOIN tblCOA_Master " & vbCrLf &
                        "   ON view_GL.AccntCode = tblCOA_Master.AccountCode " & vbCrLf &
                        "   WHERE AppDate BETWEEN '01-01-" & DateFrom.Year & "' AND '" & DateTo & "' AND View_GL.Status <> 'Cancelled' " & CostCenterFilter & IIf(branch = "ALL", "AND BranchCode in ( SELECT DISTINCT tblBranch.BranchCode " & vbCrLf &
                        "FROM tblBranch " & vbCrLf &
                        "INNER JOIN tblUser_Access ON tblBranch.BranchCode = tblUser_Access.Code " & vbCrLf &
                        "AND tblUser_Access.Status ='Active' AND tblBranch.Status ='Active' " & vbCrLf &
                        "AND tblUser_Access.Type = 'BranchCode' AND isAllowed = 1 " & vbCrLf &
                        "WHERE UserID ='" & UserID & "' )", " AND BranchCode = '" & branch & "' ") & vbCrLf &
                        ") AS A " & vbCrLf &
                        "GROUP BY AccountCode, AccountTitle "
            Else

                insertSQL = " INSERT INTO tblPRint_TB(Code, Title, BBDR, BBCR, CRDR, CRCR, CDDR, CDCR, SBDR, SBCR, PBDR, PBCR, JVDR, JVCR, IBDR, IBCR, APDR, APCR, TBDR, TBCR, CostCenter) " & vbCrLf &
                        " SELECT  AccountCode, AccountTitle, " & vbCrLf &
                        "       CASE WHEN SUM(BBDR) > SUM(BBCR) THEN SUM(BBDR) - SUM(BBCR) ELSE 0 END AS BBDR, " & vbCrLf &
                        "       CASE WHEN SUM(BBCR) > SUM(BBDR) THEN SUM(BBCR) - SUM(BBDR) ELSE 0 END AS BBCR, " & vbCrLf &
                        "       SUM(CRDR) AS CRDR, " & vbCrLf &
                        "       SUM(CRCR) AS CRCR, " & vbCrLf &
                        "       SUM(CDDR) AS CDDR, " & vbCrLf &
                        "       SUM(CDCR) AS CDCR, " & vbCrLf &
                        "       SUM(SBDR) AS SBDR, " & vbCrLf &
                        "       SUM(SBCR) AS SBCR, " & vbCrLf &
                        "       SUM(PBDR) AS PBDR, " & vbCrLf &
                        "       SUM(PBCR) AS PBCR, " & vbCrLf &
                        "       SUM(JVDR) AS JVDR, " & vbCrLf &
                        "       SUM(JVCR) AS JVCR, " & vbCrLf &
                        "       SUM(IBDR) AS IBDR, " & vbCrLf &
                        "       SUM(IBCR) AS IBCR, " & vbCrLf &
                        "       SUM(APDR) AS APDR, " & vbCrLf &
                        "       SUM(APCR) AS APCR, " & vbCrLf &
                        "       CASE WHEN SUM(TBDR) > SUM(TBCR) THEN SUM(TBDR) - SUM(TBCR) ELSE 0 END AS TBDR, " & vbCrLf &
                        "       CASE WHEN SUM(TBCR) > SUM(TBDR) THEN SUM(TBCR) - SUM(TBDR) ELSE 0 END AS TBCR, CostCenter " & vbCrLf &
                        "FROM " & vbCrLf &
                        "( " & vbCrLf &
                        "   SELECT tblCOA_Master.AccountCode, tblCOA_Master.AccountTitle, " & vbCrLf &
                        "          CASE WHEN AppDate <'" & DateFrom & "' OR Book ='BB' THEN Debit ELSE 0 END AS BBDR, " & vbCrLf &
                        "          CASE WHEN AppDate < '" & DateFrom & "' OR Book ='BB' THEN Credit ELSE 0 END AS BBCR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Receipts' THEN Debit ELSE 0 END AS CRDR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Receipts' THEN Credit ELSE 0 END AS CRCR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Disbursements' THEN Debit ELSE 0 END AS CDDR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Disbursements' THEN Credit ELSE 0 END AS CDCR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Sales' THEN Debit ELSE 0 END AS SBDR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Sales' THEN Credit ELSE 0 END AS SBCR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Purchase Journal' THEN Debit ELSE 0 END AS PBDR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Purchase Journal' THEN Credit ELSE 0 END AS PBCR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='General Journal' THEN Debit ELSE 0 END AS JVDR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='General Journal' THEN Credit ELSE 0 END AS JVCR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Inventory' THEN Debit ELSE 0 END AS IBDR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Inventory' THEN Credit ELSE 0 END AS IBCR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Accounts Payable' THEN Debit ELSE 0 END AS APDR, " & vbCrLf &
                        "          CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Accounts Payable' THEN Credit ELSE 0 END AS APCR, " & vbCrLf &
                        "          Debit AS TBDR, " & vbCrLf &
                        "          Credit AS TBCR, view_GL.Status, CostCenter " & vbCrLf &
                        "   FROM view_GL INNER JOIN tblCOA_Master " & vbCrLf &
                        "   ON view_GL.AccntCode = tblCOA_Master.AccountCode " & vbCrLf &
                        "   WHERE AppDate BETWEEN '01-01-" & DateFrom.Year & "' AND '" & DateTo & "' " & CostCenterFilter & IIf(branch = "ALL", " AND View_GL.Status <> 'Cancelled' AND BranchCode in ( SELECT DISTINCT tblBranch.BranchCode " & vbCrLf &
                        "FROM tblBranch " & vbCrLf &
                        "INNER JOIN tblUser_Access ON tblBranch.BranchCode = tblUser_Access.Code " & vbCrLf &
                        "AND tblUser_Access.Status ='Active' AND tblBranch.Status ='Active' " & vbCrLf &
                        "AND tblUser_Access.Type = 'BranchCode' AND isAllowed = 1 " & vbCrLf &
                        "WHERE UserID ='" & UserID & "' )", " AND BranchCode = '" & branch & "' ") & vbCrLf &
                        ") AS A " & vbCrLf &
                        "GROUP BY AccountCode, AccountTitle, CostCenter "
            End If
            '" WHERE A.Status ='Saved'  " & _
            SQL.ExecNonQuery(insertSQL)
            Dim f As New frmReport_Display
            f.ShowDialog("FS_TB_Detailed", "", DateTo, branch, cbCostCenter.Text)
            f.Dispose()
        ElseIf Type = "Summary" Then
            insertSQL = " INSERT INTO tblPRint_TB(Code, Title, BBDR, BBCR, CRDR, CRCR, TBDR, TBCR) " &
                 " SELECT  AccountCode, AccountTitle,  " &
                 " 		  CASE WHEN SUM(BBDR) > SUM(BBCR) THEN SUM(BBDR) - SUM(BBCR) ELSE 0 END AS BBDR, " &
                 " 		  CASE WHEN SUM(BBCR) > SUM(BBDR) THEN SUM(BBCR) - SUM(BBDR) ELSE 0 END AS BBDR, " &
                 " 		  CASE WHEN SUM(CRDR + CDDR + JVDR + PBDR + SBDR + IBDR + APCDR) > SUM(CRCR + CDCR + JVCR + PBCR + SBCR + IBCR + APCR) THEN SUM(CRDR + CDDR + JVDR + PBDR + SBDR + IBDR + APDR) - SUM(CRCR + CDCR + JVCR + PBCR + SBCR + IBCR + APCR) ELSE 0 END AS CRDR, " &
                 " 		  CASE WHEN SUM(CRCR + CDCR + JVCR + PBCR + SBCR + IBCR + APCR) > SUM(CRDR + CDDR + JVDR + PBDR + SBDR + IBDR + APDR) THEN SUM(CRCR + CDCR + JVCR + PBCR + SBCR + IBCR + APCR) - SUM(CRDR + CDDR + JVDR + PBDR + SBDR + IBDR + APDR) ELSE 0 END AS CRCR, " &
                 " 		  CASE WHEN SUM(TBDR) > SUM(TBCR) THEN SUM(TBDR) - SUM(TBCR) ELSE 0 END AS TBDR, " &
                 " 		  CASE WHEN SUM(TBCR) > SUM(TBDR) THEN SUM(TBCR) - SUM(TBDR) ELSE 0 END AS TBCR " &
                 " FROM " &
                 " ( " &
                 " 	SELECT tblCOA_Master.AccountCode, tblCOA_Master.AccountTitle,  " &
                 " 		   CASE WHEN AppDate <'" & DateFrom & "' OR Book ='BB' THEN Debit ELSE 0 END AS BBDR, " &
                 " 		   CASE WHEN AppDate < '" & DateFrom & "' OR Book ='BB' THEN Credit ELSE 0 END AS BBCR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Receipts' THEN Debit ELSE 0 END AS CRDR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Receipts' THEN Credit ELSE 0 END AS CRCR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Disbursements' THEN Debit ELSE 0 END AS CDDR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Disbursements' THEN Credit ELSE 0 END AS CDCR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='General Journal' THEN Debit ELSE 0 END AS JVDR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='General Journal' THEN Credit ELSE 0 END AS JVCR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Purchase Journal' THEN Debit ELSE 0 END AS PBDR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Purchase Journal' THEN Credit ELSE 0 END AS PBCR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Sales' THEN Debit ELSE 0 END AS SBDR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Sales' THEN Credit ELSE 0 END AS SBCR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Inventory' THEN Debit ELSE 0 END AS IBDR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Inventory' THEN Credit ELSE 0 END AS IBCR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Accounts Payable' THEN Debit ELSE 0 END AS APDR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Accounts Payable' THEN Credit ELSE 0 END AS APCR, " &
                 " 		   Debit AS TBDR, " &
                 " 		   Credit AS TBCR, view_GL.Status " &
                 " 	FROM View_GL JOIN tblCOA_Master " &
                 " 	ON View_GL.AccntCode = tblCOA_Master.AccountCode  " &
                 " 	WHERE AppDate BETWEEN '01-01-" & DateFrom.Year & "' AND '" & DateTo & "' " & CostCenterFilter & IIf(branch = "ALL", "AND BranchCode in ( SELECT    DISTINCT  tblBranch.BranchCode   " &
                    " FROM      tblBranch    " &
                    " INNER JOIN tblUser_Access    ON   " &
                    " tblBranch.BranchCode = tblUser_Access.Code    " &
                    " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " &
                    " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " &
                    " WHERE     UserID ='" & UserID & "'  )", " AND BranchCode = '" & branch & "' ") &
                 " ) AS A " &
                 " GROUP BY AccountCode, AccountTitle "

            '" WHERE A.Status ='Saved'  " & _
            SQL.ExecNonQuery(insertSQL)
            Dim f As New frmReport_Display
            f.ShowDialog("FS_TB_Summary", "", DateTo, branch)
            f.Dispose()
        ElseIf Type = "By Book" Then
            If rbSummary.Checked = True Then
                insertSQL = " INSERT INTO tblPRint_TB(Code, Title, BBDR, BBCR, CRDR, CRCR, CDDR, CDCR, JVDR, JVCR, PBDR, PBCR, SBDR, SBCR, IBDR, IBCR, TBDR, TBCR) " &
             " SELECT  AccountCode, AccountTitle,  " &
             " 		  SUM(BBDR) AS BBDR, " &
             " 		  SUM(BBDR) AS BBDR, " &
             " 		  SUM(CRDR) AS CRDR, " &
             " 		  SUM(CRCR) AS CRCR, " &
             " 		  SUM(CDDR) AS CDDR, " &
             " 		  SUM(CDCR) AS CDCR, " &
             " 		  SUM(JVDR) AS JVDR, " &
             " 		  SUM(JVCR) AS JVCR, " &
             " 		  SUM(PBDR) AS PBDR, " &
             " 		  SUM(PBCR) AS PBCR, " &
             " 		  SUM(SBDR) AS SBDR, " &
             " 		  SUM(SBCR) AS SBCR, " &
             " 		  SUM(IBDR) AS IBDR, " &
             " 		  SUM(IBCR) AS IBCR, " &
             " 		  CASE WHEN SUM(TBDR) > SUM(TBCR) THEN SUM(TBDR) - SUM(TBCR) ELSE 0 END AS TBDR, " &
             " 		  CASE WHEN SUM(TBCR) > SUM(TBDR) THEN SUM(TBCR) - SUM(TBDR) ELSE 0 END AS TBCR " &
             " FROM " &
             " ( " &
             " 	SELECT tblCOA_Master.AccountCode, tblCOA_Master.AccountTitle,  " &
             " 		   CASE WHEN AppDate <'" & DateFrom & "' OR Book ='BB' THEN Debit ELSE 0 END AS BBDR, " &
             " 		   CASE WHEN AppDate < '" & DateFrom & "' OR Book ='BB' THEN Credit ELSE 0 END AS BBCR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Receipts' THEN Debit ELSE 0 END AS CRDR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Receipts' THEN Credit ELSE 0 END AS CRCR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Disbursements' THEN Debit ELSE 0 END AS CDDR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Disbursements' THEN Credit ELSE 0 END AS CDCR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='General Journal' THEN Debit ELSE 0 END AS JVDR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='General Journal' THEN Credit ELSE 0 END AS JVCR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Accounts Payable' THEN Debit ELSE 0 END AS PBDR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Accounts Payable' THEN Credit ELSE 0 END AS PBCR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Sales' THEN Debit ELSE 0 END AS SBDR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Sales' THEN Credit ELSE 0 END AS SBCR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Inventory' THEN Debit ELSE 0 END AS IBDR, " &
                 " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Inventory' THEN Credit ELSE 0 END AS IBCR, " &
             " 		   Debit AS TBDR, " &
             " 		   Credit AS TBCR " &
             " 	FROM View_GL INNER JOIN tblCOA_Master " &
             " 	ON View_GL.AccntCode = tblCOA_Master.AccountCode " &
             " 	WHERE view_GL.Status <>'Cancelled' AND AppDate BETWEEN '" & DateFrom & "' AND '" & DateTo & "' " & CostCenterFilter & IIf(branch = "ALL", "AND BranchCode in ( SELECT    DISTINCT  tblBranch.BranchCode   " &
                    " FROM      tblBranch    " &
                    " INNER JOIN tblUser_Access    ON   " &
                    " tblBranch.BranchCode = tblUser_Access.Code    " &
                    " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " &
                    " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " &
                    " WHERE     UserID ='" & UserID & "'  )", " AND BranchCode = '" & branch & "' ") &
             " ) AS A " &
             " GROUP BY AccountCode, AccountTitle "
                SQL.ExecNonQuery(insertSQL)
                If cbPeriod.SelectedItem = "Yearly" Then
                    Dim f As New frmReport_Display
                    f.ShowDialog("BOASUMY", "", nupYear.Value, Filter, branch)
                    f.Dispose()
                ElseIf cbPeriod.SelectedItem = "Monthly" Then
                    Dim f As New frmReport_Display
                    f.ShowDialog("BOASUMM", "", cbMonth.SelectedIndex + 1, nupYear.Value, Filter, branch)
                    f.Dispose()
                ElseIf cbPeriod.SelectedItem = "Daily" Then
                    Dim f As New frmReport_Display
                    f.ShowDialog("BOASUMD", "", dtpFrom.Value.Date, Filter, branch)
                    f.Dispose()
                ElseIf cbPeriod.SelectedItem = "Date Range" Then
                    Dim f As New frmReport_Display
                    f.ShowDialog("BOASUMR", "", dtpFrom.Value.Date, dtpTo.Value.Date, Filter, branch)
                    f.Dispose()
                End If
            ElseIf rbDetailed.Checked = True Then
                If Filter = "General Ledger" Then
                    If cbPeriod.SelectedItem = "Yearly" Then
                        dtpFrom.Value = CDate("1-1-" + nupYear.Value.ToString)
                        dtpTo.Value = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Year, 1, CDate("1-1-" + nupYear.Value.ToString)))
                    ElseIf cbPeriod.SelectedItem = "Monthly" Then
                        dtpFrom.Value = CDate((cbMonth.SelectedIndex + 1).ToString + "-1-" + nupYear.Value.ToString)
                        dtpTo.Value = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, CDate((cbMonth.SelectedIndex + 1).ToString + "-1-" + nupYear.Value.ToString)))
                    ElseIf cbPeriod.SelectedItem = "Daily" Then
                        dtpTo.Value = dtpFrom.Value.Date
                    End If
                    Dim f As New frmReport_Display
                    f.ShowDialog("Book_GL", "", dtpFrom.Value.Date, dtpTo.Value.Date)
                    f.Dispose()
                Else
                    If cbPeriod.SelectedItem = "Yearly" Then
                        Dim f As New frmReport_Display
                        f.ShowDialog("BOADEMR", "", "01-01-" & nupYear.Value, "12-31-" & nupYear.Value, Filter, branch)
                        f.Dispose()
                    ElseIf cbPeriod.SelectedItem = "Monthly" Then
                        Dim f As New frmReport_Display
                        f.ShowDialog("BOADEMR", "", CDate((cbMonth.SelectedIndex + 1) & "-1-" & nupYear.Value), DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, CDate((cbMonth.SelectedIndex + 1) & "-1-" & nupYear.Value))), Filter, branch)
                        f.Dispose()
                    ElseIf cbPeriod.SelectedItem = "Daily" Then
                        Dim f As New frmReport_Display
                        f.ShowDialog("BOADEMR", "", dtpFrom.Value.Date, dtpTo.Value.Date, Filter, branch)
                        f.Dispose()
                    ElseIf cbPeriod.SelectedItem = "Date Range" Then
                        Dim f As New frmReport_Display
                        f.ShowDialog("BOADEMR", "", dtpFrom.Value.Date, dtpTo.Value.Date, Filter, branch)
                        f.Dispose()
                    End If
                End If
            End If

        End If
    End Sub

    Private Sub GenerateBS()
        Dim deleteSQl As String
        deleteSQl = " DELETE FROM rptBS "
        SQL.ExecNonQuery(deleteSQl)
        Dim dt As New DataTable
        Dim query As String
        Dim desc As String
        Dim groupID As Integer = 0
        Dim value As Decimal = 0
        Dim accntCategory As String
        Dim totalDesc(7) As String
        Dim totalCR(7) As Decimal
        Dim insertSQl As String
        Dim prevID As Integer = 0
        Dim recID As Integer = 1
        Dim incre As Integer = 1
        Dim Filter As String

        Dim MainAccountCode As String





        'Filter = IIf(branch = "ALL", "AND BranchCode in ( SELECT    DISTINCT  tblBranch.BranchCode   " & _
        '        " FROM      tblBranch    " & _
        '        " INNER JOIN tblUser_Access    ON   " & _
        '        " tblBranch.BranchCode = tblUser_Access.Code    " & _
        '        " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
        '        " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
        '        " WHERE     UserID ='" & UserID & "'  )", " AND BranchCode = '" & branch & "' ")
        If cbReportType.SelectedItem = "Balance Sheet" Or cbReportType.SelectedItem = "Balance Sheet - Audited" Then
            query = " SELECT  CASE WHEN AccountGroup ='SubAccount' THEN AccountCode  + ' - ' + CASE WHEN ISNULL(ReportAlias,'') ='' THEN AccountTitle ELSE ReportAlias END " & vbCrLf &
                " 			 ELSE CASE WHEN ReportAlias ='' THEN AccountTitle ELSE ReportAlias END " & vbCrLf &
                " 		END  AS Descrition," & vbCrLf &
                "  CASE	WHEN AccountGroup = 'Group' THEN 'G1'		WHEN AccountGroup = 'SubGroup' THEN 'G2' " & vbCrLf &
                " 		WHEN AccountGroup = 'Category' THEN 'G3'	WHEN AccountGroup = 'SubCategory' THEN 'G4' " & vbCrLf &
                " 		WHEN AccountGroup = 'CostCenter' THEN 'G5'	WHEN AccountGroup = 'MainAccount' THEN 'G6' " & vbCrLf &
                " 		WHEN AccountGroup = 'SubAccount' THEN 'G7' " & vbCrLf &
                "  ELSE ''	END AS AccountGroup, " & vbCrLf &
                " 	   CASE WHEN AccountNature = 'Debit' THEN " & vbCrLf &
                "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) ELSE " & vbCrLf &
                "               SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) END " & vbCrLf &
                " 	        WHEN AccountNature = 'Credit' THEN " & vbCrLf &
                "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Credit,0) - ISNULL(Debit,0))  ELSE " & vbCrLf &
                 "              SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) END " & vbCrLf &
                " 			ELSE 0 " & vbCrLf &
                " 	   END AS Amount, showTotal, contraAccount, OrderNo, AccountCategory " & vbCrLf &
                " FROM tblCOA_Master " & vbCrLf &
                " LEFT JOIN " & vbCrLf &
                " ( " & vbCrLf &
                " 	SELECT    AccntCode, AccntTitle, SUM(Debit) AS Debit, SUM(Credit)  AS Credit " & vbCrLf &
                " 	FROM      view_GL  " & vbCrLf &
                " 	WHERE     Status <> 'Cancelled' AND AppDate BETWEEN CAST('01-01-" & dtpFrom.Value.Year & "' AS DATE) AND '" & dtpTo.Value.Date & "' " & CostCenterFilter & Filter & vbCrLf &
                " 	GROUP BY  AccntCode, AccntTitle " & vbCrLf &
                " ) AS TB " & vbCrLf &
                " ON  tblCOA_Master.AccountCode = TB.AccntCode " & vbCrLf &
                " WHERE  AccountType ='Balance Sheet' " & vbCrLf &
                " GROUP BY AccountCode, AccountTitle, ReportAlias, AccountGroup, AccountNature, showTotal, OrderNo, contraAccount, AccountCategory " & vbCrLf &
                " HAVING  (AccountGroup <> 'SubAccount' OR " & vbCrLf &
                "        (AccountGroup = 'SubAccount'  AND " & vbCrLf &
                "        ( CASE WHEN AccountNature = 'Debit' THEN " & vbCrLf &
                "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) ELSE " & vbCrLf &
                "               SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) END " & vbCrLf &
                " 	        WHEN AccountNature = 'Credit' THEN " & vbCrLf &
                "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Credit,0) - ISNULL(Debit,0))  ELSE " & vbCrLf &
                 "              SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) END " & vbCrLf &
                " 			ELSE 0  " & vbCrLf &
                " 	   END) <> 999999999)) " & vbCrLf &
                " UNION ALL " & vbCrLf &
                " ( " & vbCrLf &
                "	  SELECT CASE WHEN AccountGroup ='SubAccount' THEN AccountCode  + ' - ' + CASE WHEN ISNULL(ReportAlias,'') ='' THEN AccountTitle ELSE ReportAlias END " & vbCrLf &
                " 				 ELSE CASE WHEN ReportAlias ='' THEN AccountTitle ELSE ReportAlias END " & vbCrLf &
                " 			END  AS Descrition," & vbCrLf &
                "	  CASE	WHEN AccountGroup = 'Group' THEN 'G1'		WHEN AccountGroup = 'SubGroup' THEN 'G2' " & vbCrLf &
                " 			WHEN AccountGroup = 'Category' THEN 'G3'	WHEN AccountGroup = 'SubCategory' THEN 'G4' " & vbCrLf &
                " 			WHEN AccountGroup = 'CostCenter' THEN 'G5'	WHEN AccountGroup = 'MainAccount' THEN 'G6' " & vbCrLf &
                " 			WHEN AccountGroup = 'SubAccount' THEN 'G7' " & vbCrLf &
                "	  ELSE ''	END AS AccountGroup," & vbCrLf &
                "        Amount," & vbCrLf &
                "        showTotal, contraAccount, OrderNo, AccountCategory" & vbCrLf &
                "        FROM tblCOA_Master" & vbCrLf &
                "        LEFT JOIN" & vbCrLf &
                "	  (" & vbCrLf &
                "		SELECT (SELECT PEC_Account FROM tblSystemSetup) AS AccntCode, ISNULL(SUM(Credit - Debit),0) AS Amount FROM" & vbCrLf &
                "		View_GL WHERE  Status <> 'Cancelled' AND AppDate BETWEEN CAST('01-01-" & dtpFrom.Value.Year & "' AS DATE) AND '" & dtpTo.Value.Date & "' AND AccntCode IN (SELECT AccountCode FROM tblCOA_Master WHERE AccountType = 'Income Statement')" & CostCenterFilter & vbCrLf &
                "	  ) AS Bal ON AccountCode = AccntCode" & vbCrLf &
                "	  WHERE AccntCode IN (SELECT PEC_Account FROM tblSystemSetup) AND AccountGroup = 'SubAccount'" & vbCrLf &
                " )" & vbCrLf &
                " ORDER BY OrderNo "

        ElseIf cbReportType.SelectedItem = "Balance Sheet - Per Group" Then
            MainAccountCode = GetAccntCode_MainAccount(cbFilter.SelectedItem)
            query = " SELECT  CASE WHEN AccountGroup ='SubAccount' THEN AccountCode  + ' - ' + CASE WHEN ISNULL(ReportAlias,'') ='' THEN AccountTitle ELSE ReportAlias END " & vbCrLf &
                    " 			 ELSE CASE WHEN ReportAlias ='' THEN AccountTitle ELSE ReportAlias END " & vbCrLf &
                    " 		END  AS Descrition," & vbCrLf &
                    "  CASE	WHEN AccountGroup = 'Group' THEN 'G1'		WHEN AccountGroup = 'SubGroup' THEN 'G2' " & vbCrLf &
                    " 		WHEN AccountGroup = 'Category' THEN 'G3'	WHEN AccountGroup = 'SubCategory' THEN 'G4' " & vbCrLf &
                    " 		WHEN AccountGroup = 'CostCenter' THEN 'G5'	WHEN AccountGroup = 'MainAccount' THEN 'G6' " & vbCrLf &
                    " 		WHEN AccountGroup = 'SubAccount' THEN 'G7' " & vbCrLf &
                    "  ELSE ''	END AS AccountGroup, " & vbCrLf &
                    " 	   CASE WHEN AccountNature = 'Debit' THEN " & vbCrLf &
                    "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) ELSE " & vbCrLf &
                    "               SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) END " & vbCrLf &
                    " 	        WHEN AccountNature = 'Credit' THEN " & vbCrLf &
                    "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Credit,0) - ISNULL(Debit,0))  ELSE " & vbCrLf &
                     "              SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) END " & vbCrLf &
                    " 			ELSE 0 " & vbCrLf &
                    " 	   END AS Amount, showTotal, contraAccount, OrderNo, AccountCategory " & vbCrLf &
                    " FROM tblCOA_Master " & vbCrLf &
                    " LEFT JOIN " & vbCrLf &
                    " ( " & vbCrLf &
                    " 	SELECT    AccntCode, AccntTitle, SUM(Debit) AS Debit, SUM(Credit)  AS Credit " & vbCrLf &
                    " 	FROM      view_GL  " & vbCrLf &
                    " 	WHERE     Status <> 'Cancelled' AND AppDate BETWEEN CAST('01-01-" & dtpFrom.Value.Year & "' AS DATE) AND '" & dtpTo.Value.Date & "' " & CostCenterFilter & Filter & vbCrLf &
                    " 	GROUP BY  AccntCode, AccntTitle " & vbCrLf &
                    " ) AS TB " & vbCrLf &
                    " ON  tblCOA_Master.AccountCode = TB.AccntCode " & vbCrLf &
                    " WHERE  AccountType ='Balance Sheet' AND Parent = '" & MainAccountCode & "' " & vbCrLf &
                    " GROUP BY AccountCode, AccountTitle, ReportAlias, AccountGroup, AccountNature, showTotal, OrderNo, contraAccount, AccountCategory " & vbCrLf &
                    " HAVING  (AccountGroup <> 'SubAccount' OR " & vbCrLf &
                    "        (AccountGroup = 'SubAccount'  AND " & vbCrLf &
                    "        ( CASE WHEN AccountNature = 'Debit' THEN " & vbCrLf &
                    "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) ELSE " & vbCrLf &
                    "               SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) END " & vbCrLf &
                    " 	        WHEN AccountNature = 'Credit' THEN " & vbCrLf &
                    "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Credit,0) - ISNULL(Debit,0))  ELSE " & vbCrLf &
                     "              SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) END " & vbCrLf &
                    " 			ELSE 0  " & vbCrLf &
                    " 	   END) <> 0)) " & vbCrLf &
                    " ORDER BY OrderNo "

        ElseIf cbReportType.SelectedItem = "Income Statement - Per Group" Then
            MainAccountCode = GetAccntCode_MainAccount(cbFilter.SelectedItem)
            query = " SELECT  CASE WHEN AccountGroup ='SubAccount' THEN AccountCode  + ' - ' + CASE WHEN ISNULL(ReportAlias,'') ='' THEN AccountTitle ELSE ReportAlias END " & vbCrLf &
                    " 			 ELSE CASE WHEN ReportAlias ='' THEN AccountTitle ELSE ReportAlias END " & vbCrLf &
                    " 		END  AS Descrition," & vbCrLf &
                    "  CASE	WHEN AccountGroup = 'Group' THEN 'G1'		WHEN AccountGroup = 'SubGroup' THEN 'G2' " & vbCrLf &
                    " 		WHEN AccountGroup = 'Category' THEN 'G3'	WHEN AccountGroup = 'SubCategory' THEN 'G4' " & vbCrLf &
                    " 		WHEN AccountGroup = 'CostCenter' THEN 'G5'	WHEN AccountGroup = 'MainAccount' THEN 'G6' " & vbCrLf &
                    " 		WHEN AccountGroup = 'SubAccount' THEN 'G7' " & vbCrLf &
                    "  ELSE ''	END AS AccountGroup, " & vbCrLf &
                    " 	   CASE WHEN AccountNature = 'Debit' THEN " & vbCrLf &
                    "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) ELSE " & vbCrLf &
                    "               SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) END " & vbCrLf &
                    " 	        WHEN AccountNature = 'Credit' THEN " & vbCrLf &
                    "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Credit,0) - ISNULL(Debit,0))  ELSE " & vbCrLf &
                     "              SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) END " & vbCrLf &
                    " 			ELSE 0 " & vbCrLf &
                    " 	   END AS Amount, showTotal, contraAccount, OrderNo, AccountCategory " & vbCrLf &
                    " FROM tblCOA_Master " & vbCrLf &
                    " LEFT JOIN " & vbCrLf &
                    " ( " & vbCrLf &
                    " 	SELECT    AccntCode, AccntTitle, SUM(Debit) AS Debit, SUM(Credit)  AS Credit " & vbCrLf &
                    " 	FROM      view_GL  " & vbCrLf &
                    " 	WHERE     Status <> 'Cancelled' AND AppDate BETWEEN CAST('01-01-" & dtpFrom.Value.Year & "' AS DATE) AND '" & dtpTo.Value.Date & "' " & CostCenterFilter & Filter & vbCrLf &
                    " 	GROUP BY  AccntCode, AccntTitle " & vbCrLf &
                    " ) AS TB " & vbCrLf &
                    " ON  tblCOA_Master.AccountCode = TB.AccntCode " & vbCrLf &
                    " WHERE  AccountType ='Income Statement' AND ( Parent = '" & MainAccountCode & "' OR tblCOA_Master.AccountCode = '" & MainAccountCode & "' )  " & vbCrLf &
                    " GROUP BY AccountCode, AccountTitle, ReportAlias, AccountGroup, AccountNature, showTotal, OrderNo, contraAccount, AccountCategory " & vbCrLf &
                    " HAVING  (AccountGroup <> 'SubAccount' OR " & vbCrLf &
                    "        (AccountGroup = 'SubAccount'  AND " & vbCrLf &
                    "        ( CASE WHEN AccountNature = 'Debit' THEN " & vbCrLf &
                    "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) ELSE " & vbCrLf &
                    "               SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) END " & vbCrLf &
                    " 	        WHEN AccountNature = 'Credit' THEN " & vbCrLf &
                    "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Credit,0) - ISNULL(Debit,0))  ELSE " & vbCrLf &
                     "              SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) END " & vbCrLf &
                    " 			ELSE 0  " & vbCrLf &
                    " 	   END) <> 0)) " & vbCrLf &
                    " ORDER BY OrderNo "

        ElseIf cbReportType.SelectedItem = "Balance Sheet Cost Center" Then

            query = " SELECT  CASE WHEN AccountGroup ='SubAccount' THEN AccountCode  + ' - ' + CASE WHEN ISNULL(ReportAlias,'') ='' THEN AccountTitle ELSE ReportAlias END " & vbCrLf &
                " 			 ELSE CASE WHEN ReportAlias ='' THEN AccountTitle ELSE ReportAlias END " & vbCrLf &
                " 		END  AS Descrition," & vbCrLf &
                "  CASE	WHEN AccountGroup = 'Group' THEN 'G1'		WHEN AccountGroup = 'SubGroup' THEN 'G2' " & vbCrLf &
                " 		WHEN AccountGroup = 'Category' THEN 'G3'	WHEN AccountGroup = 'SubCategory' THEN 'G4' " & vbCrLf &
                " 		WHEN AccountGroup = 'CostCenter' THEN 'G5'	WHEN AccountGroup = 'MainAccount' THEN 'G6' " & vbCrLf &
                " 		WHEN AccountGroup = 'SubAccount' THEN 'G7' " & vbCrLf &
                "  ELSE ''	END AS AccountGroup, " & vbCrLf &
                " 	   CASE WHEN AccountNature = 'Debit' THEN " & vbCrLf &
                "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) ELSE " & vbCrLf &
                "               SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) END " & vbCrLf &
                " 	        WHEN AccountNature = 'Credit' THEN " & vbCrLf &
                "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Credit,0) - ISNULL(Debit,0))  ELSE " & vbCrLf &
                 "              SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) END " & vbCrLf &
                " 			ELSE 0 " & vbCrLf &
                " 	   END AS Amount, showTotal, contraAccount, OrderNo, AccountCategory " & vbCrLf &
                " FROM tblCOA_Master " & vbCrLf &
                " LEFT JOIN " & vbCrLf &
                " ( " & vbCrLf &
                " 	SELECT    AccntCode, AccntTitle, SUM(Debit) AS Debit, SUM(Credit)  AS Credit " & vbCrLf &
                " 	FROM      view_GL  " & vbCrLf &
                " 	WHERE     Status <> 'Cancelled' AND AppDate BETWEEN CAST('01-01-" & dtpFrom.Value.Year & "' AS DATE) AND '" & dtpTo.Value.Date & "' AND CostCenter = '" & cbCostCenter.SelectedItem & "' " & Filter & vbCrLf &
                " 	GROUP BY  AccntCode, AccntTitle " & vbCrLf &
                " ) AS TB " & vbCrLf &
                " ON  tblCOA_Master.AccountCode = TB.AccntCode " & vbCrLf &
                " WHERE  AccountType ='Balance Sheet' " & vbCrLf &
                " GROUP BY AccountCode, AccountTitle, ReportAlias, AccountGroup, AccountNature, showTotal, OrderNo, contraAccount, AccountCategory " & vbCrLf &
                " HAVING  (AccountGroup <> 'SubAccount' OR " & vbCrLf &
                "        (AccountGroup = 'SubAccount'  AND " & vbCrLf &
                "        ( CASE WHEN AccountNature = 'Debit' THEN " & vbCrLf &
                "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) ELSE " & vbCrLf &
                "               SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) END " & vbCrLf &
                " 	        WHEN AccountNature = 'Credit' THEN " & vbCrLf &
                "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Credit,0) - ISNULL(Debit,0))  ELSE " & vbCrLf &
                 "              SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) END " & vbCrLf &
                " 			ELSE 0  " & vbCrLf &
                " 	   END) <> 0)) " & vbCrLf &
                " UNION ALL " & vbCrLf &
                " ( " & vbCrLf &
                "	  SELECT CASE WHEN AccountGroup ='SubAccount' THEN AccountCode  + ' - ' + CASE WHEN ISNULL(ReportAlias,'') ='' THEN AccountTitle ELSE ReportAlias END " & vbCrLf &
                " 				 ELSE CASE WHEN ReportAlias ='' THEN AccountTitle ELSE ReportAlias END " & vbCrLf &
                " 			END  AS Descrition," & vbCrLf &
                "	  CASE	WHEN AccountGroup = 'Group' THEN 'G1'		WHEN AccountGroup = 'SubGroup' THEN 'G2' " & vbCrLf &
                " 			WHEN AccountGroup = 'Category' THEN 'G3'	WHEN AccountGroup = 'SubCategory' THEN 'G4' " & vbCrLf &
                " 			WHEN AccountGroup = 'CostCenter' THEN 'G5'	WHEN AccountGroup = 'MainAccount' THEN 'G6' " & vbCrLf &
                " 			WHEN AccountGroup = 'SubAccount' THEN 'G7' " & vbCrLf &
                "	  ELSE ''	END AS AccountGroup," & vbCrLf &
                "        Amount," & vbCrLf &
                "        showTotal, contraAccount, OrderNo, AccountCategory" & vbCrLf &
                "        FROM tblCOA_Master" & vbCrLf &
                "        LEFT JOIN" & vbCrLf &
                "	  (" & vbCrLf &
                "		SELECT (SELECT AccountCode FROM tblCOA_Master WHERE AccountTitle LIKE 'NET%' AND AccountGroup = 'SubAccount') AS AccntCode, ISNULL(SUM(Credit - Debit),0) AS Amount FROM" & vbCrLf &
                "		View_GL WHERE  Status <> 'Cancelled' AND AppDate BETWEEN CAST('01-01-" & dtpFrom.Value.Year & "' AS DATE) AND '" & dtpTo.Value.Date & "' AND AccntCode IN (SELECT AccountCode FROM tblCOA_Master WHERE AccountType = 'Income Statement') AND CostCenter = '" & cbCostCenter.SelectedItem & "' " & vbCrLf &
                "	  ) AS Bal ON AccountCode = AccntCode" & vbCrLf &
                "	  WHERE AccountTitle LIKE 'NET%' OR AccountTitle LIKE 'Retained%' AND AccountGroup = 'SubAccount'" & vbCrLf &
                " )" & vbCrLf &
                " ORDER BY OrderNo "
        Else
            query = " SELECT  CASE WHEN AccountGroup ='SubAccount' THEN AccountCode  + ' - ' + CASE WHEN ISNULL(ReportAlias,'') ='' THEN AccountTitle ELSE ReportAlias END " & vbCrLf &
               " 			 ELSE CASE WHEN ReportAlias ='' THEN AccountTitle ELSE ReportAlias END " & vbCrLf &
               " 		END  AS Descrition," & vbCrLf &
               "  CASE	WHEN AccountGroup = 'Group' THEN 'G1'		WHEN AccountGroup = 'SubGroup' THEN 'G2' " & vbCrLf &
               " 		WHEN AccountGroup = 'Category' THEN 'G3'	WHEN AccountGroup = 'SubCategory' THEN 'G4' " & vbCrLf &
               " 		WHEN AccountGroup = 'CostCenter' THEN 'G5'	WHEN AccountGroup = 'MainAccount' THEN 'G6' " & vbCrLf &
               " 		WHEN AccountGroup = 'SubAccount' THEN 'G7' " & vbCrLf &
               "  ELSE ''	END AS AccountGroup, " & vbCrLf &
               " 	   CASE WHEN AccountNature = 'Debit' THEN " & vbCrLf &
               "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) ELSE " & vbCrLf &
               "               SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) END " & vbCrLf &
               " 	        WHEN AccountNature = 'Credit' THEN " & vbCrLf &
               "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Credit,0) - ISNULL(Debit,0))  ELSE " & vbCrLf &
                "              SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) END " & vbCrLf &
               " 			ELSE 0 " & vbCrLf &
               " 	   END AS Amount, showTotal, contraAccount, OrderNo, AccountCategory " & vbCrLf &
               " FROM tblCOA_Master " & vbCrLf &
               " LEFT JOIN " & vbCrLf &
               " ( " & vbCrLf &
               " 	SELECT    AccntCode, AccntTitle, SUM(Debit) AS Debit, SUM(Credit)  AS Credit " & vbCrLf &
               " 	FROM      view_GL  " & vbCrLf &
               " 	WHERE     Status <> 'Cancelled' AND AppDate BETWEEN CAST('01-01-" & dtpFrom.Value.Year & "' AS DATE) AND '" & dtpTo.Value.Date & "' AND CostCenter = '" & cbFilter.SelectedItem & "' " & Filter & vbCrLf &
               " 	GROUP BY  AccntCode, AccntTitle " & vbCrLf &
               " ) AS TB " & vbCrLf &
               " ON  tblCOA_Master.AccountCode = TB.AccntCode " & vbCrLf &
               " WHERE  AccountType ='Balance Sheet' " & vbCrLf &
               " GROUP BY AccountCode, AccountTitle, ReportAlias, AccountGroup, AccountNature, showTotal, OrderNo, contraAccount, AccountCategory " & vbCrLf &
               " HAVING  (AccountGroup <> 'SubAccount' OR " & vbCrLf &
               "        (AccountGroup = 'SubAccount'  AND " & vbCrLf &
               "        ( CASE WHEN AccountNature = 'Debit' THEN " & vbCrLf &
               "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) ELSE " & vbCrLf &
               "               SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) END " & vbCrLf &
               " 	        WHEN AccountNature = 'Credit' THEN " & vbCrLf &
               "               CASE WHEN contraAccount = 0 THEN SUM(ISNULL(Credit,0) - ISNULL(Debit,0))  ELSE " & vbCrLf &
                "              SUM(ISNULL(Debit,0) - ISNULL(Credit,0)) END " & vbCrLf &
               " 			ELSE 0  " & vbCrLf &
               " 	   END) <> 0)) " & vbCrLf &
               " UNION ALL " & vbCrLf &
               " ( " & vbCrLf &
               "	  SELECT CASE WHEN AccountGroup ='SubAccount' THEN AccountCode  + ' - ' + CASE WHEN ISNULL(ReportAlias,'') ='' THEN AccountTitle ELSE ReportAlias END " & vbCrLf &
               " 				 ELSE CASE WHEN ReportAlias ='' THEN AccountTitle ELSE ReportAlias END " & vbCrLf &
               " 			END  AS Descrition," & vbCrLf &
               "	  CASE	WHEN AccountGroup = 'Group' THEN 'G1'		WHEN AccountGroup = 'SubGroup' THEN 'G2' " & vbCrLf &
               " 			WHEN AccountGroup = 'Category' THEN 'G3'	WHEN AccountGroup = 'SubCategory' THEN 'G4' " & vbCrLf &
               " 			WHEN AccountGroup = 'CostCenter' THEN 'G5'	WHEN AccountGroup = 'MainAccount' THEN 'G6' " & vbCrLf &
               " 			WHEN AccountGroup = 'SubAccount' THEN 'G7' " & vbCrLf &
               "	  ELSE ''	END AS AccountGroup," & vbCrLf &
               "        Amount," & vbCrLf &
               "        showTotal, contraAccount, OrderNo, AccountCategory" & vbCrLf &
               "        FROM tblCOA_Master" & vbCrLf &
               "        LEFT JOIN" & vbCrLf &
               "	  (" & vbCrLf &
               "		SELECT (SELECT AccountCode FROM tblCOA_Master WHERE AccountTitle LIKE 'NET%' AND AccountGroup = 'SubAccount') AS AccntCode, ISNULL(SUM(Credit - Debit),0) AS Amount FROM" & vbCrLf &
               "		View_GL WHERE  Status <> 'Cancelled' AND AppDate BETWEEN CAST('01-01-" & dtpFrom.Value.Year & "' AS DATE) AND '" & dtpTo.Value.Date & "' AND AccntCode IN (SELECT AccountCode FROM tblCOA_Master WHERE AccountType = 'Income Statement') AND CostCenter = '" & cbFilter.SelectedItem & "'" & vbCrLf &
               "	  ) AS Bal ON AccountCode = AccntCode" & vbCrLf &
               "	  WHERE AccountTitle LIKE 'NET%' AND AccountGroup = 'SubAccount'" & vbCrLf &
               " )" & vbCrLf &
               " ORDER BY OrderNo "
        End If

        'query = " SELECT  CASE WHEN AccountGroup ='SubAccount' THEN AccountCode  + ' - ' + CASE WHEN ISNULL(ReportAlias,'') ='' THEN AccountTitle ELSE ReportAlias END " & _
        '        " 			 ELSE CASE WHEN ReportAlias ='' THEN AccountTitle ELSE ReportAlias END " & _
        '        " 		END  AS Descrition," & _
        '        "  CASE	WHEN AccountGroup = 'Group' THEN 'G1'		WHEN AccountGroup = 'SubGroup' THEN 'G2' " & _
        '        " 		WHEN AccountGroup = 'Category' THEN 'G3'	WHEN AccountGroup = 'SubCategory' THEN 'G4' " & _
        '        " 		WHEN AccountGroup = 'CostCenter' THEN 'G5'	WHEN AccountGroup = 'MainAccount' THEN 'G6' " & _
        '        " 		WHEN AccountGroup = 'SubAccount' THEN 'G7' " & _
        '        "  ELSE ''	END AS AccountGroup, " & _
        '        " 	   CASE WHEN AccountNature = 'Debit' THEN SUM(ISNULL(Debit,0) - ISNULL(Credit,0))  " & _
        '        " 	        WHEN AccountNature = 'Credit' THEN SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) " & _
        '        " 			ELSE 0 " & _
        '        " 	   END AS Amount, showTotal, contraAccount " & _
        '        " FROM tblCOA_Master " & _
        '        " LEFT JOIN " & _
        '        " ( " & _
        '        " 	SELECT    AccntCode, AccntTitle, SUM(Debit) AS Debit, SUM(Credit)  AS Credit " & _
        '        " 	FROM      view_GL  " & _
        '        " 	WHERE     AppDate BETWEEN CAST('01-01-" & dtpFrom.Value.Year & "' AS DATE) AND '" & dtpTo.Value.Date & "' " & Filter & _
        '        " 	GROUP BY  AccntCode, AccntTitle " & _
        '        " ) AS TB " & _
        '        " ON  tblCOA_Master.AccountCode = TB.AccntCode " & _
        '        " WHERE  AccountType ='Balance Sheet' " & _
        '        " GROUP BY AccountCode, AccountTitle, ReportAlias, AccountGroup, AccountNature, showTotal, OrderNo, contraAccount " & _
        '        " HAVING  (AccountGroup <> 'SubAccount' OR " & _
        '        "        (AccountGroup = 'SubAccount'  AND " & _
        '        "        (CASE WHEN AccountNature = 'Debit' THEN SUM(ISNULL(Debit,0) - ISNULL(Credit,0))  " & _
        '        " 	        WHEN AccountNature = 'Credit' THEN SUM(ISNULL(Credit,0) - ISNULL(Debit,0)) " & _
        '        " 			ELSE 0 " & _
        '        " 	   END) <> 0)) " & _
        '        " ORDER BY OrderNo "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                desc = row(0).ToString
                groupID = CInt(row(1).ToString.Replace("G", ""))
                value = row(2)
                If row(3) = True Then
                    totalDesc(groupID) = desc
                End If
                'If row(4) = True Then
                '    value = value * -1
                'End If
                accntCategory = row(6)
                If groupID <> prevID Or groupID = 7 Then

                    If prevID > groupID Then
                        If prevID <> 7 Then
                            For i As Integer = incre - 1 To 0 Step -1
                                deleteSQl = " DELETE FROM rptBS WHERE RecordID = '" & recID - 1 & "' "
                                SQL.ExecNonQuery(deleteSQl)
                                recID -= 1
                            Next

                            incre = 0
                        Else
                            incre = 0
                        End If
                        For i As Integer = 6 To 1 Step -1
                            If groupID <= i Then
                                If Not IsNothing(totalDesc(i)) AndAlso totalDesc(i) <> "" Then
                                    If totalCR(i) <> 0 Then
                                        insertSQl = " INSERT INTO " &
                                               " rptBS(RecordID, Description, Amount, GroupID) " &
                                               " VALUES(@RecordID, @Description, @Amount, @GroupID)"
                                        SQL.FlushParams()
                                        SQL.AddParam("@RecordID", recID)
                                        SQL.AddParam("@Description", "TOTAL " & totalDesc(i))
                                        SQL.AddParam("@Amount", totalCR(i))
                                        SQL.AddParam("@GroupID", i)
                                        SQL.ExecNonQuery(insertSQl)
                                        incre = 0
                                        totalDesc(i) = Nothing
                                        totalCR(i) = Nothing
                                        recID += 1
                                    End If

                                End If
                            End If

                        Next
                    End If
                    insertSQl = " INSERT INTO " &
                        " rptBS(RecordID, Description, Amount, GroupID, AccountCategory) " &
                        " VALUES(@RecordID, @Description, @Amount, @GroupID, @AccountCategory)"
                    SQL.FlushParams()
                    SQL.AddParam("@RecordID", recID)
                    SQL.AddParam("@Description", desc)
                    SQL.AddParam("@Amount", value)
                    SQL.AddParam("@GroupID", groupID)
                    SQL.AddParam("@AccountCategory", accntCategory)
                    SQL.ExecNonQuery(insertSQl)
                    prevID = groupID
                    recID += 1
                    incre += 1
                    If value <> 0 Then
                        For i As Integer = 1 To 7
                            If Not IsNothing(totalDesc(i)) AndAlso totalDesc(i) <> "" Then
                                totalCR(i) += value
                            End If
                        Next
                    End If

                End If
            Next
            If prevID <> 7 Then
                For i As Integer = incre - 1 To 0 Step -1
                    deleteSQl = " DELETE FROM rptBS WHERE RecordID = '" & recID - 1 & "' "
                    SQL.ExecNonQuery(deleteSQl)
                    recID -= 1
                Next
            End If
            'For i As Integer = 6 To 1 Step -1
            '    If Not IsNothing(totalDesc(i)) AndAlso totalDesc(i) <> "" Then
            '        If totalCR(i) <> 0 Then
            '            insertSQl = " INSERT INTO " &
            '                   " rptBS(RecordID, Description, Amount, GroupID) " &
            '                   " VALUES(@RecordID, @Description, @Amount, @GroupID)"
            '            SQL.FlushParams()
            '            SQL.AddParam("@RecordID", recID)
            '            SQL.AddParam("@Description", "TOTAL " & totalDesc(i))
            '            SQL.AddParam("@Amount", totalCR(i))
            '            SQL.AddParam("@GroupID", i)
            '            SQL.ExecNonQuery(insertSQl)
            '            incre = 0
            '            totalDesc(i) = Nothing
            '            totalCR(i) = Nothing
            '            recID += 1
            '        End If
            '    End If
            'Next
        End If
    End Sub

    Private Sub GenerateSCE(ByVal DateFrom As Date, ByVal DateTo As Date)
        Dim insertSQL, deleteSQL As String
        deleteSQL = " DELETE FROM tblPRint_TB "
        SQL.ExecNonQuery(deleteSQL)
        insertSQL = " INSERT INTO tblPRint_TB(Code, Title, BBDR, BBCR, CRDR, CRCR, CDDR, CDCR, JVDR, JVCR,TBDR, TBCR) " &
                    " SELECT  AccountCode, AccountTitle,  " &
                    " 		  CASE WHEN SUM(BBDR) > SUM(BBCR) THEN SUM(BBDR) - SUM(BBCR) ELSE 0 END AS BBDR, " &
                    " 		  CASE WHEN SUM(BBCR) > SUM(BBDR) THEN SUM(BBCR) - SUM(BBDR) ELSE 0 END AS BBDR, " &
                    " 		  SUM(CRDR) AS CRDR, " &
                    " 		  SUM(CRCR) AS CRCR, " &
                    " 		  SUM(CDDR) AS CDDR, " &
                    " 		  SUM(CDCR) AS CDCR, " &
                    " 		  SUM(JVDR) AS JVDR, " &
                    " 		  SUM(JVCR) AS JVCR, " &
                    " 		  CASE WHEN SUM(TBDR) > SUM(TBCR) THEN SUM(TBDR) - SUM(TBCR) ELSE 0 END AS TBDR, " &
                    " 		  CASE WHEN SUM(TBCR) > SUM(TBDR) THEN SUM(TBCR) - SUM(TBDR) ELSE 0 END AS TBCR " &
                    " FROM " &
                    " ( " &
                    " 	SELECT tblCOA_Master.AccountCode, tblCOA_Master.AccountTitle,  " &
                    " 		   CASE WHEN AppDate <'" & DateFrom & "' OR Book ='BB' THEN Debit ELSE 0 END AS BBDR, " &
                    " 		   CASE WHEN AppDate < '" & DateFrom & "' OR Book ='BB' THEN Credit ELSE 0 END AS BBCR, " &
                    " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='CASH RECEIPTS' THEN Debit ELSE 0 END AS CRDR, " &
                    " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='CASH RECEIPTS' THEN Credit ELSE 0 END AS CRCR, " &
                    " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Disbursements' THEN Debit ELSE 0 END AS CDDR, " &
                    " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Disbursements' THEN Credit ELSE 0 END AS CDCR, " &
                    " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='General Journal' THEN Debit ELSE 0 END AS JVDR, " &
                    " 		   CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='General Journal' THEN Credit ELSE 0 END AS JVCR, " &
                    " 		   Debit AS TBDR, " &
                    " 		   Credit AS TBCR " &
                    " 	FROM View_GL INNER JOIN tblCOA_Master " &
                    " 	ON View_GL.AccntCode = tblCOA_Master.AccountCode " &
                    " 	WHERE Book <> 'Period End Closing' AND View_GL.Status <> 'Cancelled' AND AccntCode LIKE '3%' AND AppDate  BETWEEN '01-01-" & DateFrom.Year & "' AND '" & DateTo & "' " &
                    " ) AS A " &
                    " GROUP BY AccountCode, AccountTitle "
        SQL.ExecNonQuery(insertSQL)
        Dim f As New frmReport_Display
        f.ShowDialog("FSSCE", UserName, dtpTo.Value.Date)
        f.Dispose()
    End Sub

    Private Sub GenerateDCPR(ByVal Type As String, ByVal DateFrom As Date, ByVal DateTo As Date, Optional ByVal Filter As String = "")
        Dim insertSQL, deleteSQL As String
        deleteSQL = " DELETE FROM tblPrint_DCPR "
        SQL.ExecNonQuery(deleteSQL)
        If Type = "Daily" Then
            insertSQL = " INSERT INTO tblPRint_DCPR(Code, Title, BB, CR, DEP, CD, JV, Row_ID) " &
                        "  SELECT  AccountCode, AccountTitle,   		   " &
                        " 		SUM(BB) AS BB,  		   " &
                        " 		SUM(CR) AS CR,  		   " &
                        " 		SUM(DEP) AS DEP,  		   " &
                        " 		SUM(CD) AS CD,  		   " &
                        " 		SUM(JV) AS JV,           " &
                        " 		ROW_NUMBER() OVER (ORDER BY AccountCode) AS Row_ID   " &
                        " FROM   " &
                        " (  	 " &
                        " 		SELECT  AccountCode, AccountTitle,   		    " &
                        " 				CASE WHEN AppDate <'" & DateFrom & "' OR Book ='BB' THEN Debit - Credit ELSE 0 END AS BB,  		    " &
                        " 				CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Receipts' AND RefType <> 'DS' THEN Debit - Credit ELSE 0 END AS CR,   " &
                        " 			    CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Receipts' AND RefType ='DS' THEN Debit - Credit ELSE 0 END AS DEP,   " &
                        " 				CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='Cash Disbursements' THEN Credit - Debit ELSE 0 END AS CD,  		   " &
                        " 				CASE WHEN AppDate >= '" & DateFrom & "' AND Book ='General Journal' THEN Debit - Credit ELSE 0 END AS JV  	 " &
                        " 		FROM view_GL RIGHT JOIN tblCOA_Master  	 " &
                        " 		ON   view_GL.AccntCode = tblCOA_Master.AccountCode  	 " &
                        " 		WHERE (AppDate BETWEEN '01-01-" & DateFrom.Year & "' AND '" & DateFrom & "'  OR view_GL.AccntCode IS NULL)    " &
                        " 		AND   (tblCOA_Master.AccountCode IN (SELECT AccountCode FROM tblBank_Master))   " & IIf(branch = "ALL", "AND BranchCode in ( SELECT    DISTINCT  tblBranch.BranchCode   " &
                        " FROM      tblBranch    " &
                        " INNER JOIN tblUser_Access    ON   " &
                        " tblBranch.BranchCode = tblUser_Access.Code    " &
                        " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " &
                        " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " &
                        " WHERE     UserID ='" & UserID & "'  )", " AND BranchCode = '" & branch & "' ") &
                        " ) AS A  GROUP BY AccountCode, AccountTitle "
            SQL.ExecNonQuery(insertSQL)
            Dim f As New frmReport_Display
            f.ShowDialog("DCPR", DateTo)
            f.Dispose()
        End If
    End Sub



    Private Sub nupYear_ValueChanged(sender As System.Object, e As System.EventArgs) Handles nupYear.ValueChanged, cbMonth.SelectedIndexChanged, chkYTD.CheckedChanged
        LoadPeriod()
    End Sub

    Private Sub lvFilter_Click(sender As Object, e As System.EventArgs) Handles lvFilter.Click
        If rbNone.Checked = True Then
            For Each item As ListViewItem In lvFilter.Items
                item.Checked = False
            Next
        End If
    End Sub



    Private Sub lvFilter_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lvFilter.SelectedIndexChanged
        If cbReportCategory.SelectedItem = "Book of Accounts" Then
            For Each item As ListViewItem In lvFilter.Items
                item.Checked = False
            Next
        End If
        If lvFilter.SelectedItems.Count = 1 Then
            If lvFilter.SelectedItems(0).Checked = False Then
                lvFilter.SelectedItems(0).Checked = True
            Else
                lvFilter.SelectedItems(0).Checked = False
            End If
        End If
        If cbReportCategory.SelectedItem <> "Member List" Then
            rbSpecific.Checked = True
        End If
    End Sub

    Private Sub cbPeriod_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbPeriod.SelectedIndexChanged
        If cbPeriod.SelectedItem = "Monthly" Then
            gbPeriodYM.Visible = True
            gbPeriodFT.Visible = False
            chkYTD.Visible = False
            cbMonth.Enabled = True
            chkYTD.Checked = False
            cbMonth.Items.Clear()
            cbMonth.Items.Add("January")
            cbMonth.Items.Add("February")
            cbMonth.Items.Add("March")
            cbMonth.Items.Add("April")
            cbMonth.Items.Add("May")
            cbMonth.Items.Add("June")
            cbMonth.Items.Add("July")
            cbMonth.Items.Add("August")
            cbMonth.Items.Add("September")
            cbMonth.Items.Add("October")
            cbMonth.Items.Add("November")
            cbMonth.Items.Add("December")
            lblMonth.Text = "Month :"
        ElseIf cbPeriod.SelectedItem = "Daily" Then
            gbPeriodYM.Visible = False
            gbPeriodFT.Visible = True
            lblTo.Visible = False
            dtpTo.Visible = False
            lblFrom.Text = "Date :"
        ElseIf cbPeriod.SelectedItem = "Date Range" Then
            lblFrom.Text = "From :"
            lblTo.Visible = True
            dtpTo.Visible = True
            gbPeriodYM.Visible = False
            gbPeriodFT.Visible = True '
        ElseIf cbPeriod.SelectedItem = "Yearly" Then
            chkYTD.Checked = True
            LoadPeriod()
            lblFrom.Text = "From :"
            lblTo.Visible = True
            dtpTo.Visible = True
            gbPeriodYM.Visible = True
            gbPeriodFT.Visible = False
            cbMonth.Enabled = True
        ElseIf cbPeriod.SelectedItem = "Quarterly" Then
            chkYTD.Checked = True
            LoadPeriod()
            cbMonth.Items.Clear()
            cbMonth.Items.Add("1st Quarter")
            cbMonth.Items.Add("2nd Quarter")
            cbMonth.Items.Add("3rd Quarter")
            cbMonth.Items.Add("4th Quarter")
            lblMonth.Text = "Quarter :"
            lblFrom.Text = "From :"
            lblTo.Visible = True
            dtpTo.Visible = True
            gbPeriodYM.Visible = True
            gbPeriodFT.Visible = False
            cbMonth.Enabled = True
        ElseIf cbPeriod.SelectedItem = "As Of" Then
            gbPeriodYM.Visible = False
            gbPeriodFT.Visible = True
            lblTo.Visible = False
            dtpTo.Visible = False
            lblFrom.Text = "As Of :"
        End If
    End Sub


    Private Sub rbAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbAll.CheckedChanged, rbNone.CheckedChanged, rbSpecific.CheckedChanged
        If rbAll.Checked = True Then
            For Each item As ListViewItem In lvFilter.Items
                item.Checked = True
            Next
        ElseIf rbNone.Checked = True Then
            For Each item As ListViewItem In lvFilter.Items
                item.Checked = False
            Next
        End If
    End Sub

    Private Sub LoadBank()
        Dim query As String
        query = " SELECT Bank FROM tblBank_Master ORDER BY Bank "
        SQL.ReadQuery(query)
        cbFilter.Items.Clear()
        cbFilter.Items.Add("ALL")
        While SQL.SQLDR.Read
            cbFilter.Items.Add(SQL.SQLDR("Bank").ToString)
        End While
        cbFilter.SelectedIndex = 0
        cbFilter.Enabled = True
    End Sub

    Private Sub LoadVCE()

        Dim query As String
        query = " SELECT VCEName FROM viewVCE_Master WHERE Status = 'Active' ORDER BY VCEName "
        SQL.ReadQuery(query)
        cbVCEFilter.Items.Clear()
        cbVCEFilter.Items.Add("ALL")
        originalItems.Add("ALL")
        While SQL.SQLDR.Read
            Dim item As String = SQL.SQLDR("VCEName").ToString()
            cbVCEFilter.Items.Add(item)
            originalItems.Add(item)
        End While
        cbVCEFilter.SelectedIndex = 0
        cbVCEFilter.Enabled = True
    End Sub



    Private Sub LoadAPV_Buyer()
        Dim query As String
        query = " SELECT DISTINCT VCEname " &
                " FROM View_GL " &
                " WHERE AccntCode = '5310001' AND Status <> 'Cancelled' "
        SQL.ReadQuery(query)
        cbVCEFilter.Items.Clear()
        cbVCEFilter.Items.Add("ALL")
        'originalItems.Add("ALL")
        While SQL.SQLDR.Read
            Dim item As String = SQL.SQLDR("VCEName").ToString()
            cbVCEFilter.Items.Add(item)
            originalItems.Add(item)
        End While

        'cbVCEFilter.SelectedIndex = 0
        cbVCEFilter.Enabled = True
    End Sub

    Private Sub LoadSalesReftype()
        cbFilter.Items.Clear()
        cbFilter.Items.Add("ALL")
        cbFilter.Items.Add("SI")
        cbFilter.Items.Add("BS")
        cbFilter.SelectedIndex = 0
        cbFilter.Enabled = True
    End Sub
    Private Sub LoadCRBRefType()
        cbFilter.Items.Clear()
        cbFilter.Items.Add("ALL")
        cbFilter.Items.Add("OR")
        cbFilter.Items.Add("AR")
        cbFilter.Items.Add("CR")
        cbFilter.Items.Add("PR")
        cbFilter.SelectedIndex = 0
        cbFilter.Enabled = True
    End Sub
    Private Sub bgwIS_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwIS.DoWork
        GenerateComparativeIS()
    End Sub

    Private Sub bgwIS_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwIS.ProgressChanged
        pgBar.Value = e.ProgressPercentage
    End Sub

    Private Sub bgwIS_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwIS.RunWorkerCompleted
        Dim f As New frmReport_Display
        f.ShowDialog("FSIS2", UserName, dtpTo.Value.Date, cbBranch.SelectedItem)
        f.Dispose()
        pgBar.Visible = False
        GroupBox6.Enabled = True
    End Sub

    Private Function CPR_DIT(DateFrom As Date, DateTo As Date, Bank As String) As Decimal
        Dim query As String
        query = " SELECT	 ISNULL(SUM(Debit-Credit),0) AS Amount " &
                " FROM	     view_GL INNER JOIN tblBank_Master " &
                " ON		 view_GL.accntCode = tblBank_Master.AccountCode " &
                " WHERE	     YEAR(AppDate) = " & DateFrom.Year & " AND view_GL.Status <> 'Cancelled' AND AppDate  <= '" & DateTo & "'  AND Bank ='" & Bank & "' " &
                " AND        Book ='Cash Receipts' AND dateCleared IS NULL "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() Then
            dgvCPR.Rows.Add({"9", Bank, CDec(SQL.SQLDR("Amount"))})
            Return CDec(SQL.SQLDR("Amount"))
        Else
            Return 0
        End If
    End Function

    Private Function CPR_OC(DateFrom As Date, DateTo As Date, Bank As String) As Decimal
        Dim query As String
        query = " SELECT	 ISNULL(SUM(Credit-Debit),0) AS Amount " &
                " FROM	     view_GL INNER JOIN tblBank_Master " &
                " ON		 view_GL.accntCode = tblBank_Master.AccountCode " &
                " WHERE	     YEAR(AppDate) = " & DateFrom.Year & " AND view_GL.Status <> 'Cancelled' AND AppDate  <= '" & DateTo & "' AND Bank ='" & Bank & "' " &
                " AND        Book ='Cash Disbursements' AND dateCleared IS NULL "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() Then
            dgvCPR.Rows.Add({"8", Bank, CDec(SQL.SQLDR("Amount"))})
            Return CDec(SQL.SQLDR("Amount"))
        Else
            Return 0
        End If
    End Function

    Private Sub GenerateCPR()
        ClearCPRTable()
        Dim dateFrom, dateTo As Date
        dateFrom = dtpFrom.Value.Date
        dateTo = dtpTo.Value.Date
        Dim total As Decimal = 0

        Dim banks As List(Of String) = GetBanks()
        For Each bank As String In banks
            total = 0
            total += CPR_BB(dateFrom, bank)
            total += CPR_CRB(dateFrom, dateTo, bank)
            total += CPR_JVD(dateFrom, dateTo, bank)
            dgvCPR.Rows.Add({"4", bank, total})
            total -= CPR_CDB(dateFrom, dateTo, bank)
            total -= CPR_JVC(dateFrom, dateTo, bank)
            dgvCPR.Rows.Add({"7", bank, total})
            total += CPR_OC(dateFrom, dateTo, bank)
            total -= CPR_DIT(dateFrom, dateTo, bank)
            dgvCPR.Rows.Add({"10", bank, total})
        Next
        InsertCPRtoTable()

        Dim f As New frmReport_Display
        f.ShowDialog("CPR", dtpFrom.Value.Date, dtpTo.Value.Date)
        f.Dispose()
    End Sub

    Private Function CPR_JVC(DateFrom As Date, DateTo As Date, Bank As String) As Decimal
        Dim query As String
        query = " SELECT	 ISNULL(SUM(Credit-Debit),0) AS Amount " &
                " FROM	     view_GL INNER JOIN tblBank_Master " &
                " ON		 view_GL.accntCode = tblBank_Master.AccountCode " &
                " WHERE	     YEAR(AppDate) = " & DateFrom.Year & " AND view_GL.Status <> 'Cancelled' AND AppDate BETWEEN '" & DateFrom & "' AND '" & DateTo & "' AND RefType <>'BB' AND Bank ='" & Bank & "' " &
                " AND        Book NOT IN ('Cash Receipts','Cash Disbursements') AND Credit > 0 "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() Then
            dgvCPR.Rows.Add({"6", Bank, CDec(SQL.SQLDR("Amount"))})
            Return CDec(SQL.SQLDR("Amount"))
        Else
            Return 0
        End If
    End Function

    Private Function CPR_BB(DateFrom As Date, Bank As String) As Decimal
        Dim query As String
        query = " SELECT	 ISNULL(SUM(Debit-Credit),0) AS Amount " &
                " FROM	     view_GL INNER JOIN tblBank_Master " &
                " ON		 view_GL.accntCode = tblBank_Master.AccountCode " &
                " WHERE	     YEAR(AppDate) = " & DateFrom.Year & " AND view_GL.Status <> 'Cancelled' AND (AppDate < '" & DateFrom & "' OR (AppDate = '" & DateFrom & "' AND RefType ='BB')) AND Bank ='" & Bank & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() Then
            dgvCPR.Rows.Add({"1", Bank, CDec(SQL.SQLDR("Amount"))})
            Return CDec(SQL.SQLDR("Amount"))
        Else
            Return 0
        End If
    End Function

    Private Sub btnExport_Click(sender As System.Object, e As System.EventArgs) Handles btnExport.Click
        branch = "ALL"
        Dim templateName As String = ""
        Dim sfd As New SaveFileDialog
        sfd.Filter = "CSV Files (.csv)|.csv"
        sfd.DefaultExt = "csv"
        Dim colStart As Integer = 1
        Dim query As String = ""
        Dim filename As String
        If sfd.ShowDialog = DialogResult.OK Then
            filename = sfd.FileName
            Dim Username As String
            Username = GetUserName(UserID)
            Select Case cbReportCategory.SelectedItem
                Case "Books of Accounts"
                    Select Case cbReportType.SelectedItem
                        Case "Cash Receipts Book"
                            GenerateBooks("Cash Receipts")
                            templateName = "CRB.csv"
                            colStart = 7
                            query = "SELECT Date, RefNo, VCECode, VCEName, Account, Amount, C1, C2, C3, C4, C5, AccntTitle, Debit, Credit FROM rptBooks"
                        Case "Cash Disbursements Book"
                            GenerateBooks("Cash Disbursements")
                            templateName = "CDB.csv"
                            colStart = 7
                            query = "SELECT Date, RefNo, VCECode, VCEName, Account, Amount, C1, C2, C3, C4, C5, AccntTitle, Debit, Credit FROM rptBooks"
                        Case "Accounts Payable"
                            GenerateBooks("Accounts Payable")
                            templateName = "APB.csv"
                            colStart = 7
                            query = "SELECT Date, RefNo,  VCEName, Particulars, Account, Amount, C1, C2, C3, C4, C5, AccntTitle, Debit, Credit FROM rptBooks"
                        Case "Purchase Book"
                            GenerateBooks("Purchases")
                        Case "Sales Book"
                            GenerateBooks("Sales")
                            templateName = "SB.csv"
                            colStart = 6
                            query = "SELECT Date, RefNo,  VCEName, Account, Amount, C1, C2, C3, C4, C5, AccntTitle, Debit, Credit FROM rptBooks"
                        Case "General Journal"
                            GenerateBooks("General Journal")
                        Case "Inventory Ledger"
                            GenerateBooks("Inventory")
                    End Select
            End Select
            Dim query2 As String = " SELECT Description FROM rptBooks_header ORDER BY Type "
            Dim SQL2 As New SQLControl
            SQL2.GetQuery(query2)
            Dim xlApp As Excel.Application
            Dim xlWorkBook As Excel.Workbook
            Dim xlWorkSheet As Excel.Worksheet
            xlApp = New Excel.Application
            Dim firstRow As Integer = 2
            Dim App_Path As String
            Dim ctr As Integer = 0
            Dim SQL As New SQLControl
            SQL.FlushParams()
            SQL.GetQuery(query)
            Dim dt As DataTable = SQL.SQLDS.Tables(0)
            Dim dt2 As DataTable = SQL2.SQLDS.Tables(0)
            If dt.Rows.Count > 0 Then
                App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName & "\Templates"
                If My.Computer.FileSystem.FileExists(App_Path + "\" & templateName) Then
                    xlWorkBook = xlApp.Workbooks.Open(App_Path + "\" & templateName)
                    xlWorkSheet = xlWorkBook.Worksheets(1)
                    For i As Integer = 0 To dt.Rows.Count - 1
                        For j As Integer = 0 To dt.Columns.Count - 1
                            xlWorkSheet.Cells(i + firstRow, j + 1) = dt.Rows(i).Item(j)
                        Next
                        ctr += 1
                    Next


                    If dt2.Rows.Count > 0 Then
                        For j As Integer = 0 To dt2.Rows.Count - 1
                            xlWorkSheet.Cells(1, j + colStart) = dt2.Rows(j).Item(0)
                        Next
                    End If

                    xlWorkBook.SaveAs(filename)
                    xlWorkBook.Close(System.Type.Missing, System.Type.Missing, System.Type.Missing)
                    xlApp.Quit()

                    ' CLEAN UP. (CLOSE INSTANCES OF EXCEL OBJECTS.)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet) : xlWorkSheet = Nothing
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook) : xlWorkBook = Nothing
                Else
                    MsgBox("No Template found!" & vbNewLine & "Please contact your systems administrator", MsgBoxStyle.Exclamation)
                End If
            End If


            ' CLEAN UP. (CLOSE INSTANCES OF EXCEL OBJECTS.)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp) : xlApp = Nothing
            If My.Computer.FileSystem.FileExists(filename) Then
                Dim xls As Excel.Application
                Dim workbook As Excel.Workbook
                xls = New Excel.Application
                xls.Visible = True
                workbook = xls.Workbooks.Open(filename)
            End If
        End If
    End Sub

    Private Sub cbFilter_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbFilter.SelectedIndexChanged
        If cbReportCategory.SelectedItem = "Schedule" Then
            LoadScheduleVCE(cbFilter.SelectedItem)
        End If
    End Sub

    Private Sub rbNone2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbNone2.CheckedChanged
        If rbAll2.Checked = True Then
            For Each item As ListViewItem In lvFilter2.Items
                item.Checked = True
            Next
        ElseIf rbNone2.Checked = True Then
            For Each item As ListViewItem In lvFilter2.Items
                item.Checked = False
            Next
        End If
    End Sub

    Private Sub rbSpecific2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbSpecific2.CheckedChanged
        If rbAll2.Checked = True Then
            For Each item As ListViewItem In lvFilter2.Items
                item.Checked = True
            Next
        ElseIf rbNone2.Checked = True Then
            For Each item As ListViewItem In lvFilter2.Items
                item.Checked = False
            Next
        End If
    End Sub

    Private Sub rbAll2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbAll2.CheckedChanged
        If rbAll2.Checked = True Then
            For Each item As ListViewItem In lvFilter2.Items
                item.Checked = True
            Next
        ElseIf rbNone2.Checked = True Then
            For Each item As ListViewItem In lvFilter2.Items
                item.Checked = False
            Next
        End If
    End Sub


    Private Sub cbVCEFilter_TextChanged(sender As Object, e As EventArgs) Handles cbVCEFilter.TextChanged
        If isSelecting Then Return
        ' Get the current text
        Dim keyword As String = cbVCEFilter.Text.ToLower()

        Dim filteredItems = originalItems.Where(Function(item) item.ToLower().Contains(keyword)).ToList()

        cbVCEFilter.Items.Clear()
        cbVCEFilter.Items.AddRange(filteredItems.ToArray())

        cbVCEFilter.Items.Remove("ALL")

        If cbReportType.SelectedItem <> "Statement of Account" Then
            cbVCEFilter.Items.Insert(0, "ALL")
        End If



        cbVCEFilter.SelectionStart = cbVCEFilter.Text.Length
            cbVCEFilter.SelectionLength = 0

    End Sub

    Private Sub cbVCEFilter_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbVCEFilter.SelectionChangeCommitted
        isSelecting = True

        cbVCEFilter.Text = cbVCEFilter.SelectedItem.ToString()

        Me.BeginInvoke(New Action(Sub() isSelecting = False))
    End Sub
End Class