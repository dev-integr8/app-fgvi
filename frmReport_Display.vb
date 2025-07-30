Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmReport_Display
    Public docnum As String
    Public docutype As String
    Public BillingPeriod As String
    Public company As String
    Public Accnttitle As String
    'Dim rpt, p1, p2, p3, p4, p5 As String
    Public rpt, p1, p2, p3, p4, p5, p6 As String
    Dim crtableLogoninfos As New TableLogOnInfos()
    Dim crtableLogoninfo As New TableLogOnInfo()
    Dim crConnectionInfo As New ConnectionInfo()
    Dim CrTables As Tables
    Dim CrTable As Table
    Dim crReportDocument As New ReportDocument()

    Public Overloads Function ShowDialog(ByVal reportType As String, ByVal param1 As String, Optional ByVal param2 As String = "",
                                         Optional param3 As String = "", Optional param4 As String = "", Optional param5 As String = "", Optional param6 As String = "") As Boolean
        rpt = reportType
        p1 = param1
        p2 = param2
        p3 = param3
        p4 = param4
        p5 = param5
        p6 = param6
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmCRDisplay_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub frmCRDisplay_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        CrystalReportViewer1.ReportSource.Close()
        Me.Dispose()
    End Sub


    Private Sub frmCRDisplay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Report_Path As String
        Report_Path = App_Path & "\CR_Reports\JADE_01"
        Dim CR As New ReportDocument
        Dim CR1 As New ReportDocument
        Dim CR2 As New ReportDocument
        Dim Username As String
        Username = GetUserName(UserID)



        '  CR.Load(Application.StartupPath & "\Reports\crInv.rpt")



        Try
            If Strings.Left(rpt, 5) = "TRANS" Then
                Report_Path = Report_Path & "\" & rpt & ".rpt"
                CR.Load(Report_Path)
                CR.SetDatabaseLogon(DBUser, DBPassword)
                CR.SetParameterValue("TransID", p1)
            ElseIf Strings.Right(rpt, 5) = "_List" Then
                Report_Path = Report_Path & "\" & rpt & ".rpt"
                CR.Load(Report_Path)
                CR.SetDatabaseLogon(DBUser, DBPassword)
                If rpt <> "FA_List" Then
                    CR.SetParameterValue("User", p1)
                    CR.SetParameterValue("Type", p2)
                    CR.SetParameterValue("DateFrom", p3)
                    CR.SetParameterValue("DateTo", p4)
                    CR.SetParameterValue("Status", p5)
                    If rpt = "Collection_List" Then
                        CR.SetParameterValue("Reftype", p6)
                    End If
                Else
                    CR.SetParameterValue("@AsOfDate", p1)
                End If
            ElseIf Strings.Right(rpt, 9) = "_Register" Then
                Report_Path = Report_Path & "\" & rpt & ".rpt"
                CR.Load(Report_Path)
                CR.SetDatabaseLogon(DBUser, DBPassword)
                CR.SetParameterValue("User", Username)
                CR.SetParameterValue("FilterBy", p1)
                CR.SetParameterValue("DateFrom", p2)
                CR.SetParameterValue("DateTo", p3)
                CR.SetParameterValue("IncludeCancelled", p4)
            Else
                Select Case rpt
                    Case "RE_Ledger"
                        Report_Path = Report_Path & "\RE_LedgerPrintout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                    Case "Journal"
                        Report_Path = Report_Path & "\Journal.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", frmUserLogin.txtUsername.Text)
                        CR.SetParameterValue("Book", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                        CR.SetParameterValue("IncludeCancelled", p4)
                        CR.SetParameterValue("CostCenter", p5)
                    Case "VCEList"
                        Report_Path = Report_Path & "\VCE_List.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                    Case "AOE"
                        Report_Path = Report_Path & "\AOE.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("ClientName", p2)
                        CR.SetParameterValue("AccntTitle", p3)
                    Case "Lapsing_Schedule"
                        Report_Path = Report_Path & "\Lapsing_Schedule.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@AsOfDate", p1)
                    Case "Lapsing_Schedule_Calendarized"
                        Report_Path = Report_Path & "\Lapsing_Schedule_Calendarized.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@AsOfDate", p1)
                    Case "CA_Unliquidated"
                        Report_Path = Report_Path & "\CA_Unliquidated.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateTo", p1)
                        CR.SetParameterValue("IncludeCancelled", p2)
                    Case "CA_Summary"
                        Report_Path = Report_Path & "\CA_Summary.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("VCEName", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                    Case "Reordering"
                        Report_Path = Report_Path & "\PO_Reordering.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                    Case "Books_CRB"
                        Report_Path = Report_Path & "\Books_CRB.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("Book", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                        CR.SetParameterValue("IncludeCancelled", p4)
                        CR.SetParameterValue("User", p5)
                        CR.SetParameterValue("CostCenter", p6)
                    Case "Books_CDB"
                        Report_Path = Report_Path & "\Books_CDB.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("Book", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                        CR.SetParameterValue("IncludeCancelled", p4)
                        CR.SetParameterValue("User", p5)
                        CR.SetParameterValue("CostCenter", p6)
                    Case "Books_APV"
                        Report_Path = Report_Path & "\Books_APV.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("Book", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                        CR.SetParameterValue("IncludeCancelled", p4)
                        CR.SetParameterValue("User", p5)
                        CR.SetParameterValue("CostCenter", p6)
                    Case "Books_GJ"
                        Report_Path = Report_Path & "\Books_GJ.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("Book", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                        CR.SetParameterValue("IncludeCancelled", p4)
                        CR.SetParameterValue("User", p5)
                        CR.SetParameterValue("CostCenter", p6)
                    Case "Books_SB"
                        Report_Path = Report_Path & "\Books_SB.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("Book", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                        CR.SetParameterValue("IncludeCancelled", p4)
                        CR.SetParameterValue("User", p5)
                        CR.SetParameterValue("CostCenter", p6)
                    Case "Books_Inventory"
                        Report_Path = Report_Path & "\Books_Inventory.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("Book", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                        CR.SetParameterValue("IncludeCancelled", p4)
                        CR.SetParameterValue("User", p5)
                    Case "GL"
                        Report_Path = Report_Path & "\GL.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", frmUserLogin.txtUsername.Text)
                        CR.SetParameterValue("Account", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                    Case "GL_COSTCENTER"
                        Report_Path = Report_Path & "\GL_COSTCENTER.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", frmUserLogin.txtUsername.Text)
                        CR.SetParameterValue("Account", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                        CR.SetParameterValue("CostCenter", p4)
                    Case "SL"
                        Report_Path = Report_Path & "\SL.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", frmUserLogin.txtUsername.Text)
                        CR.SetParameterValue("Account", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                        CR.SetParameterValue("IncludeCancelled", p4)
                        CR.SetParameterValue("VCE", p5)
                    Case "SL_COSTCENTER"
                        Report_Path = Report_Path & "\SL_COSTCENTER.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", frmUserLogin.txtUsername.Text)
                        CR.SetParameterValue("Account", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                        CR.SetParameterValue("IncludeCancelled", p4)
                        CR.SetParameterValue("VCE", p5)
                        CR.SetParameterValue("CostCenter", p6)
                    Case "COA_Master"
                        Report_Path = Report_Path & "\COA_Master.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("Date", p1)
                        CR.SetParameterValue("User", Username)
                    Case "AR_Aging"
                        Report_Path = Report_Path & "\AR_Aging.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@ASOFDate", p2)
                        CR.SetParameterValue("@AccntCode", p3)
                        CR.SetParameterValue("@CostCenter", p4)
                    Case "AR_Aging_CostCenter"
                        Report_Path = Report_Path & "\AR_Aging_CostCenter.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@ASOFDate", p2)
                        CR.SetParameterValue("@AccntCode", p3)
                        CR.SetParameterValue("@CostCenter", p4)
                    Case "AR_Aging_Detailed"
                        Report_Path = Report_Path & "\AR_Aging_Detailed.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@ASOFDate", p2)
                        CR.SetParameterValue("@AccntCode", p3)
                    Case "AR_Aging_Summary"
                        Report_Path = Report_Path & "\AR_Aging_Summary.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@ASOFDate", p2)
                        CR.SetParameterValue("@AccntCode", p3)
                    Case "LN_AGING"
                        Report_Path = Report_Path & "\LN_Aging.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@AsOfDate", p2)
                        CR.SetParameterValue("@BranchCode", p3)
                        CR.SetParameterValue("@LoanType", p4)
                        CR.SetParameterValue("User", Username)
                    Case "Schedule"
                        Report_Path = Report_Path & "\Schedule.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@AsOfDate", p1)
                        CR.SetParameterValue("@VCECode", p3)
                        CR.SetParameterValue("@Account", p2)

                    Case "Schedule_COSTCENTER"
                        Report_Path = Report_Path & "\Schedule_COSTCENTER.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@AsOfDate", p1)
                        CR.SetParameterValue("@VCECode", p3)
                        CR.SetParameterValue("@Account", p2)
                        CR.SetParameterValue("@CostCenter", p4)
                    Case "Loyalty"
                        Report_Path = Report_Path & "\Loyalty.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("MemberCode", p1)
                    Case "Membership"
                        Report_Path = Report_Path & "\Loyalty.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("MemberCode", p1)
                    Case "Schedule_IS"
                        Report_Path = Report_Path & "\Schedule_IS.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@DateFrom", p1)
                        CR.SetParameterValue("@DateTo", p2)
                        CR.SetParameterValue("@Account", p3)
                        CR.SetParameterValue("@VCECode", p4)
                        CR.SetParameterValue("@CostCenter", p5)

                        'LOAN REPORTS
                    Case "LN_Schedule"
                        Report_Path = Report_Path & "\LN_Schedule.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                        CR.SetParameterValue("User", p2)
                    Case "LN_Disclosure"
                        Report_Path = Report_Path & "\LN_Disclosure.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                        CR.SetParameterValue("User", p2)
                    Case "LN_PN"
                        Report_Path = Report_Path & "\LN_PN.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                    Case "LN_LAS"
                        Report_Path = Report_Path & "\LN_LAS.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                        CR.SetParameterValue("User", p2)


                    Case "LN_Aging_PerLoanType"
                        Report_Path = Report_Path & "\LN_Aging_PerLoanType.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("AppDate", p2)
                        CR.SetParameterValue("LoanType", p3)
                        CR.SetParameterValue("Filter", p4)
                    Case "LN_Release"
                        Report_Path = Report_Path & "\LN_Release.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", frmUserLogin.txtUsername.Text)
                        CR.SetParameterValue("Status", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                        CR.SetParameterValue("Branch", p4)
                    Case "Loan_Title"
                        Report_Path = Report_Path & "\LN_TitleList.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("DateTo", p2)
                    Case "LN_CoMakerList"
                        Report_Path = Report_Path & "\LN_CoMakerList.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("Filter", p1)
                    Case "CV_Transaction"
                        Report_Path = Report_Path & "\CV_Transaction.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("DateTo", p2)
                    'Case "CV_Register"
                    '    Report_Path = Report_Path & "\CV_Regiester.rpt"
                    '    CR.Load(Report_Path)
                    '    CR.SetDatabaseLogon(DBUser, DBPassword)
                    '    CR.SetParameterValue("DateFrom", p1)
                    '    CR.SetParameterValue("DateTo", p2)
                    Case "Check_Issuance"
                        Report_Path = Report_Path & "\Check_Issuance.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("DateTo", p2)
                    Case "Account_Balances"
                        Report_Path = Report_Path & "\AccountBalances.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("DateTo", p2)
                        CR.SetParameterValue("AccntTitle", p3)
                    Case "Loan_Balances"
                        Report_Path = Report_Path & "\LoanBalances.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateTo", p1)
                        CR.SetParameterValue("AccountTitle", p2)
                    Case "OR_Transaction"
                        Report_Path = Report_Path & "\OR_Transaction.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("DateTo", p2)
                    'Case "SOA"
                    '    Report_Path = Report_Path & "\SOA.rpt"
                    '    CR.Load(Report_Path)
                    '    CR.SetDatabaseLogon(DBUser, DBPassword)
                    '    CR.SetParameterValue("@AsOfDate", p2)
                    '    CR.SetParameterValue("@VCECode", p3)
                    Case "SOA"
                        Report_Path = Report_Path & "\SOA.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@AsOfDate", p2)
                        CR.SetParameterValue("@VCECode", p3)
                    Case "SOA_AllVCE"
                        Report_Path = Report_Path & "\SOA_AllVCE.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@AsOfDate", p2)
                    Case "SOA_Detailed"
                        Report_Path = Report_Path & "\SOA_Detailed.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("VCECode", p1)
                        CR.SetParameterValue("Date", CDate(p2).ToShortDateString)
                        CR.SetParameterValue("DateFrom", CDate(p3).ToShortDateString)
                    Case "SOA_Loan_Detailed"
                        Report_Path = Report_Path & "\SOA_Loan_Detailed.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("VCECode", p1)
                        CR.SetParameterValue("Date", CDate(p2).ToShortDateString)
                        CR.SetParameterValue("DateFrom", CDate(p3).ToShortDateString)
                    Case "SOA_Credit_Detailed"
                        Report_Path = Report_Path & "\SOA_Credit_Detailed.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("VCECode", p1)
                        CR.SetParameterValue("Date", CDate(p2).ToShortDateString)
                        CR.SetParameterValue("DateFrom", CDate(p3).ToShortDateString)
                    Case "Verifier"
                        Report_Path = Report_Path & "\Verifier.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("VCECode", p1)
                        CR.SetParameterValue("AccntCode", p2)
                    Case "SP_AnnexA"
                        Report_Path = Report_Path & "\" & rpt & ".rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                    Case "SP_AnnexC"
                        Report_Path = Report_Path & "\" & rpt & ".rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                    Case "SP_AnnexD"
                        Report_Path = Report_Path & "\" & rpt & ".rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                    Case "SP_AnnexMOA"
                        Report_Path = Report_Path & "\" & rpt & ".rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                    Case "PO_Unserved"
                        Report_Path = Report_Path & "\" & rpt & ".rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("Type", p2)
                    Case "TL-CC"
                        Report_Path = Report_Path & "\TL_CostCenter.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@CostCenter", p1)
                    Case "PR_WithoutPO"
                        Report_Path = Report_Path & "\" & rpt & ".rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("Type", p2)
                        'Book of Accounts Summary
                    Case "BOASUMY"
                        Report_Path = Report_Path & "\BOASUMY.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("Year", p2)
                        CR.SetParameterValue("Filter", p3)
                        CR.SetParameterValue("Branch", p4)
                    Case "BOASUMM"
                        Report_Path = Report_Path & "\BOASUMM.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("Month", p2)
                        CR.SetParameterValue("Year", p3)
                        CR.SetParameterValue("Filter", p4)
                        CR.SetParameterValue("Branch", p5)
                    Case "BOASUMD"
                        Report_Path = Report_Path & "\BOASUMD.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("Date", p2)
                        CR.SetParameterValue("Filter", p3)
                        CR.SetParameterValue("Branch", p4)
                    Case "BOASUMR"
                        Report_Path = Report_Path & "\BOASUMR.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                        CR.SetParameterValue("Filter", p4)
                        CR.SetParameterValue("Branch", p5)
                        'Detailed
                    Case "BOADEMR"
                        Report_Path = Report_Path & "\BOADEMR.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", Username)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                        CR.SetParameterValue("Filter", p4)
                        CR.SetParameterValue("Branch", p5)

                        'SUMMARY
                    Case "GENLGRDS"
                        Report_Path = Report_Path & "\GENLGRDS.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("Date", p2)
                    Case "GENLGRMS"
                        Report_Path = Report_Path & "\GENLGRMS.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("Year", p2)
                        CR.SetParameterValue("Month", p3)
                    Case "GENLGRYS"
                        Report_Path = Report_Path & "\GENLGRYS.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("Year", p2)
                    Case "GENLGRRS"
                        Report_Path = Report_Path & "\GENLGRRS.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)

                        'DETAILED
                    Case "GENLGRDD"
                        Report_Path = Report_Path & "\GENLGRDD.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("Date", p2)
                    Case "GENLGRMD"
                        Report_Path = Report_Path & "\GENLGRMD.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("Year", p2)
                        CR.SetParameterValue("Month", p3)
                    Case "GENLGRYD"
                        Report_Path = Report_Path & "\GENLGRYD.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("Year", p2)
                    Case "GENLGRRD"
                        Report_Path = Report_Path & "\GENLGRRD.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                    Case "SUBLGRS"
                        Report_Path = Report_Path & "\SUBLGRS.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                    Case "SUBLGRS_Insu"
                        Report_Path = Report_Path & "\SUBLGRS_Insu.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                    Case "SUBLGRSC"
                        Report_Path = Report_Path & "\SUBLGRSC.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                    Case "FSBS"
                        Report_Path = Report_Path & "\FSBS.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("DateTo", p2)
                        CR.SetParameterValue("CostCenter", p4)
                    Case "FSBS_Audited"
                        Report_Path = Report_Path & "\FSBS_Audited.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("DateTo", p2)
                        CR.SetParameterValue("CostCenter", p4)
                    Case "FSBS_PerGroup"
                        Report_Path = Report_Path & "\FSBS_PerGroup.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("DateTo", p2)
                        CR.SetParameterValue("CostCenter", p4)
                    Case "FSBS2"
                        Report_Path = Report_Path & "\FSBS2.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("DateTo", p2)
                    Case "FSINCS"
                        Report_Path = Report_Path & "\FSINCS.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("DateTo", p3)
                    Case "FS_TB_Summary"
                        Report_Path = Report_Path & "\FS_TB_Summary.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("USER", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("Branch", p3)
                    Case "FS_TB_Detailed"
                        Report_Path = Report_Path & "\FS_TB_Detailed.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("USER", p1)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("Branch", p3)
                        CR.SetParameterValue("Project", p4)
                    Case "CV Summary"
                        Report_Path = Report_Path & "\CV_Summary.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@FromDate", p1)
                        CR.SetParameterValue("@ToDate", p2)
                    Case "CPR"
                        Report_Path = Report_Path & "\CPR.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@DateFrom", p1)
                        CR.SetParameterValue("@DateTo", p2)
                    Case "BIR_2307"
                        Report_Path = Report_Path & "\CV_2307.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                        CR.SetParameterValue("Type", p2)
                    Case ("Billing-Reimburse")
                        Report_Path = Report_Path & "\Billing_Reimbursement.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                    Case "AR"
                        Report_Path = Report_Path & "\AR_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                    Case "OR"
                        Report_Path = Report_Path & "\OR_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                    Case "Collection"
                        Report_Path = Report_Path & "\Collection_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                        CR.SetParameterValue("TransType", p2)
                        'CR.SetParameterValue("User", Username)
                    Case "Billing-Service"
                        Report_Path = Report_Path & "\Billing_Services.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                    Case "CV"
                        Report_Path = Report_Path & "\CV_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                        CR.SetParameterValue("@FormName", p2)
                    Case "PCV"
                        Report_Path = Report_Path & "\PCV_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("User", Username)
                    Case "PCVRR"
                        Report_Path = Report_Path & "\PCVRR_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                        CR.SetParameterValue("Username", Username)
                    Case "Check"
                        Report_Path = Report_Path & "\Check.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Name", p2)
                        CR.SetParameterValue("@Date", p3)
                        CR.SetParameterValue("@Amount", p4)
                    Case "Aging"
                        Report_Path = Report_Path & "\Aging_Report.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("AccntTitle", Accnttitle)
                        CR.SetParameterValue("RefDate", BillingPeriod)
                    Case "Aging1"
                        Report_Path = Report_Path & "\Aging_Report_Water.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("AccntTitle", Accnttitle)
                        CR.SetParameterValue("RefDate", BillingPeriod)
                    Case "Aging2"
                        Report_Path = Report_Path & "\Aging_Report_EMU.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("AccntTitle", Accnttitle)
                        CR.SetParameterValue("RefDate", BillingPeriod)

                    Case "Bank Recon"
                        Report_Path = Report_Path & "\BankRecon.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", docnum)

                    Case "Billing Rental"
                        Report_Path = Report_Path & "\Billing_Rental.rpt"
                        '  MsgBox(Report_Path, vbInformation)
                        CR.Load(Report_Path)  ' CR.Load(Report_Path & "\CR_Reports\Billing Statement072015.rpt")
                        'CR.SetDatabaseLogon(  DBUser  , DBPassword)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@BSNO", docnum)
                    Case "Billing PEZA"
                        Report_Path = Report_Path & "\Billing_PEZA.rpt"
                        '  MsgBox(Report_Path, vbInformation)
                        CR.Load(Report_Path)  ' CR.Load(Report_Path & "\CR_Reports\Billing Statement072015.rpt")
                        'CR.SetDatabaseLogon(  DBUser  , DBPassword)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@BSNO", docnum)
                        ' INVENTORY REPORTS
                    Case "IA_Count"
                        Report_Path = Report_Path & "\" & rpt & ".rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("DateTo", p2)
                        CR.SetParameterValue("ItemCode", p3)
                    Case "DepositSlip"
                        Report_Path = Report_Path & "\DepositSlipver2.rpt"
                        '  MsgBox(Report_Path, vbInformation)
                        CR.Load(Report_Path)  ' CR.Load(Report_Path & "\CR_Reports\Billing Statement072015.rpt")
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", docnum)
                    Case "Daily Sales Tally Report"
                        Report_Path = Report_Path & "\Daily Sales Tally Report.rpt"
                        '  MsgBox(Report_Path, vbInformation)
                        CR.Load(Report_Path)  ' CR.Load(Report_Path & "\CR_Reports\Billing Statement072015.rpt")
                        CR.SetDatabaseLogon("sa", "Evosolution")
                        ' CR.SetParameterValue("DepositDate", docnum)
                        '     CR.SetParameterValue("Billing_Period", "July 2015")
                    Case "Sales_Summary_Report"
                        Report_Path = Report_Path & "\Sales_Summary_Report.rpt"
                        '  MsgBox(Report_Path, vbInformation)
                        CR.Load(Report_Path)  ' CR.Load(Report_Path & "\CR_Reports\Billing Statement072015.rpt")
                        CR.SetDatabaseLogon("sa", "Evosolution")
                        Dim datedocnum As Date
                        datedocnum = docnum
                    Case "IDBack"
                        Report_Path = Report_Path & "\ID_BACK.rpt"
                        '  MsgBox(Report_Path, vbInformation)
                        CR.Load(Report_Path)  ' CR.Load(Report_Path & "\CR_Reports\Billing Statement072015.rpt")
                        CR.SetDatabaseLogon("sa", "Evosolution")
                        CR.SetParameterValue("@MemberID2", docnum)
                    Case "IDFront"
                        Report_Path = Report_Path & "\ID_Front.rpt"
                        '  MsgBox(Report_Path, vbInformation)
                        CR.Load(Report_Path)  ' CR.Load(Report_Path & "\CR_Reports\Billing Statement072015.rpt")
                        CR.SetDatabaseLogon("sa", "Evosolution")
                        CR.SetParameterValue("@MemberID2", docnum)
                    Case "Journal Entry"
                        Report_Path = Report_Path & "\JournalEntry.rpt"
                        '  MsgBox(Report_Path, vbInformation)
                        CR.Load(Report_Path)  ' CR.Load(Report_Path & "\CR_Reports\Billing Statement072015.rpt")
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", docnum)
                    Case "Period End Adjusment"
                        Report_Path = Report_Path & "\JournalVoucherPeriodEndAdjustment.rpt"
                        '  MsgBox(Report_Path, vbInformation)
                        CR.Load(Report_Path)  ' CR.Load(Report_Path & "\CR_Reports\Billing Statement072015.rpt")
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", docnum)
                    Case "JV"
                        Report_Path = Report_Path & "\JV_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("User", Username)
                    Case "PJ"
                        Report_Path = Report_Path & "\PJ_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("User", Username)
                    Case "SJ"
                        Report_Path = Report_Path & "\SJ_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("User", Username)
                    Case "BI"
                        Report_Path = Report_Path & "\BI_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                    Case "JO"
                        Report_Path = Report_Path & "\JO_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                    Case "Disbursement"
                        Report_Path = Report_Path & "\CheckVoucher.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", docnum)
                    Case "2307"
                        Report_Path = Report_Path & "\Tax2307.rpt"
                        '  MsgBox(Report_Path, vbInformation)
                        CR.Load(Report_Path)  ' CR.Load(Report_Path & "\CR_Reports\Billing Statement072015.rpt")
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", docnum)
                    Case "BillingStatement"
                        'CR.Load("C:\perpetual\evo\HCG\HCG\bin\Debug\CR_Reports\Billing_Statement073015abc.rpt")  ' CR.Load(Report_Path & "\CR_Reports\Billing Statement072015.rpt")
                        '   CR.Load("C:\perpetual\evo\HCG\HCG\bin\Debug\CR_Reports\Billing_Statement073015abc.rpt")  ' CR.Load(Report_Path & "\CR_Reports\Billing Statement072015.rpt")
                        Report_Path = Report_Path & "\BillingStatementV2ConsoBP.rpt"
                        '  MsgBox(Report_Path, vbInformation)
                        CR.Load(Report_Path)  ' CR.Load(Report_Path & "\CR_Reports\Billing Statement072015.rpt")
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@BSNO", docnum)
                        '   CR.SetParameterValue("@BillingPeriod", BillingPeriod)
                    Case "Goods Return"
                        CR.Load(Report_Path & "\Billing Statement073015a.rpt")
                        CR.SetDatabaseLogon("sa", "hochengtest")
                        CR.SetParameterValue("ConsolidatingBP", docnum)
                        '  CR.SetParameterValue("DocNumber", docnum)
                    Case "Deposit"
                        'CR.Load("C:\perpetual\evo\HCG\HCG\bin\Debug\CR_Reports\Billing_Statement073015abc.rpt")  ' CR.Load(Report_Path & "\CR_Reports\Billing Statement072015.rpt")
                        '   CR.Load("C:\perpetual\evo\HCG\HCG\bin\Debug\CR_Reports\Billing_Statement073015abc.rpt")  ' CR.Load(Report_Path & "\CR_Reports\Billing Statement072015.rpt")
                        Report_Path = Report_Path & "\Deposit_Report.rpt"
                        '  MsgBox(Report_Path, vbInformation)
                        CR.Load(Report_Path)  ' CR.Load(Report_Path & "\CR_Reports\Billing Statement072015.rpt")
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@DepositDate", docnum)
                    Case "Deposit Slip"
                        Report_Path = Report_Path & "\ORReport.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TRNASID", docnum)
                    Case "PO"
                        Report_Path = Report_Path & "\PO_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                        ' CR.SetParameterValue("@FormName", p2)
                    Case "BSStorage"
                        Report_Path = Report_Path & "\BSStorage_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)

                    Case "CS_Summary"
                        Report_Path = Report_Path & "\CS_Summary.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("DateTo", p2)
                    Case "CS"
                        Report_Path = Report_Path & "\CS_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                    Case "PB_Savings"
                        Report_Path = Report_Path & "\PB_Savings.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("VCECode", p1)
                        CR.SetParameterValue("LineNo", p2)
                        CR.SetParameterValue("Page", p3)
                        CR.SetParameterValue("RefNo", p4)
                    Case "PR"
                        Report_Path = Report_Path & "\PR_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                    Case "PR_Detailed"
                        Report_Path = Report_Path & "\PR_Detailed_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                    Case "SO"
                        Report_Path = Report_Path & "\SO_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                    Case "DR"
                        Report_Path = Report_Path & "\DR_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                    Case "APV"
                        Report_Path = Report_Path & "\APV_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                        CR.SetParameterValue("@FormName", p2)
                    Case "CA"
                        Report_Path = Report_Path & "\CA_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                    Case "RR"
                        Report_Path = Report_Path & "\RR_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                        'CR.SetParameterValue("@FormName", p2)
                    Case "IC"
                        Report_Path = Report_Path & "\IC_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                        'CR.SetParameterValue("@FormName", p2)
                    Case "PL"
                        Report_Path = Report_Path & "\PL_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                    Case "GI"
                        Report_Path = Report_Path & "\GI_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                    Case "MRIS"
                        Report_Path = Report_Path & "\MRIS_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                    Case "GR"
                        Report_Path = Report_Path & "\GR_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                    Case "MR"
                        Report_Path = Report_Path & "\MR_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                    Case "ITI"
                        Report_Path = Report_Path & "\ITI_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                    Case "ITR"
                        Report_Path = Report_Path & "\ITR_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                    Case "GR"
                        Report_Path = Report_Path & "\GR_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                    Case "SQ"
                        Report_Path = Report_Path & "\SQ_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                    Case "ATD"
                        Report_Path = Report_Path & "\ATD_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                    Case "CSI"
                        Report_Path = Report_Path & "\CSI_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                    Case "SI"
                        Report_Path = Report_Path & "\SI_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                    Case "ECS"
                        Report_Path = Report_Path & "\ECS_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                    Case "SIBS"
                        Report_Path = Report_Path & "\SIBS_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                    Case "SIF"
                        Report_Path = Report_Path & "\SI_F_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                    Case "BOMC"
                        Report_Path = Report_Path & "\BOMC_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                    Case "BOM"
                        Report_Path = Report_Path & "\BOM_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                    Case "CF-C"
                        Report_Path = Report_Path & "\CF_Comparison_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                    Case "CF-A"
                        Report_Path = Report_Path & "\CF_Approved_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                    Case "DS"
                        Report_Path = Report_Path & "\DS_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                    Case "DS"
                        Report_Path = Report_Path & "\BR_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                    Case "RFP"
                        Report_Path = Report_Path & "\RFP.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                    Case "RFP_VAT"
                        Report_Path = Report_Path & "\RFP_VAT.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                    Case "RFP_Summary"
                        Report_Path = Report_Path & "\RFP_Summary.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                    Case "DCPR"
                        Report_Path = Report_Path & "\DCPR.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateTo", p1)
                    Case "MemberReport"
                        Report_Path = Report_Path & "\MemberList.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("DateTo", p2)
                        CR.SetParameterValue("Filter", p3)
                        CR.SetParameterValue("MemberType", p4)

                        'Case "FSBS"
                        '    Report_Path = Report_Path & "\FSBS.rpt"
                        '    CR.Load(Report_Path)
                        '    CR.SetDatabaseLogon(  DBUser  , DBPassword)
                        '    CR.SetParameterValue("User", p1)
                        '    CR.SetParameterValue("DateTo", p2)
                        '    CR.SetParameterValue("Branch", p3)
                    Case "FSIS"
                        Report_Path = Report_Path & "\FSIS.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("DateTo", p2)
                        CR.SetParameterValue("CostCenter", p4)
                    Case "FSIS_PerGroup"
                        Report_Path = Report_Path & "\FSIS_PerGroup.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("DateTo", p2)
                        CR.SetParameterValue("CostCenter", p4)
                    Case "FSIS_Audited"
                        Report_Path = Report_Path & "\FSIS_Audited.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("DateTo", p2)
                        CR.SetParameterValue("CostCenter", p4)
                    Case "FSIS2"
                        Report_Path = Report_Path & "\FSIS2.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("DateTo", p2)
                    Case "BR"
                        Report_Path = Report_Path & "\BR_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                        CR.SetParameterValue("User", Username)
                    Case "Collection_Daily"
                        Report_Path = Report_Path & "\Collection_List.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("DateTo", p2)
                        CR.SetParameterValue("Filter", p3)
                        CR.SetParameterValue("TransType", p4)
                        CR.SetParameterValue("Username", Username)
                    Case "BIR_2551Q"
                        Report_Path = Report_Path & "\2551Q.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("Year", p1)
                        CR.SetParameterValue("Quarter", p2)
                    Case "Purchase_Journal"
                        Report_Path = Report_Path & "\PJ.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("DateTo", p2)
                    Case "Sales_Journal"
                        Report_Path = Report_Path & "\SJ.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("DateTo", p2)
                    Case "CashDisbursement_Journal"
                        Report_Path = Report_Path & "\CDJ.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("DateTo", p2)
                    Case "CashReceipt_Journal"
                        Report_Path = Report_Path & "\CRJ.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("DateTo", p2)
                    Case "AccountsPayable_Journal"
                        Report_Path = Report_Path & "\APJ.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("DateTo", p2)
                    Case "SAWT"
                        Report_Path = Report_Path & "\SAWT.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("Date", p1)
                    Case "GPR"
                        Report_Path = Report_Path & "\GPR.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("DateTo", p2)
                    Case "SCHED_SR"
                        Report_Path = Report_Path & "\SCHED_SR.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("DateTo", p2)
                    Case "SCHED_COS"
                        Report_Path = Report_Path & "\SCHED_COS.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("DateTo", p2)
                    Case "BS_ForReceiving"
                        Report_Path = Report_Path & "\BS_ForReceiving.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@User", Username)
                        CR.SetParameterValue("@AsOfDate", Date.Today)
                    Case "LC_PaymentDetails"
                        Report_Path = Report_Path & "\LC_PaymentDetails.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                    Case "LC_ContractHistory"
                        Report_Path = Report_Path & "\LC_ContractHistory.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("PropCode", p1)
                    Case "LC_ExpiredContracts"
                        Report_Path = Report_Path & "\LC_ExpiredContracts.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("AsOfDate", p1)
                    Case "LC_PastDue"
                        Report_Path = Report_Path & "\LC_PastDue.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("AsOfDate", p1)
                    Case "LC_UnpaidCharges"
                        Report_Path = Report_Path & "\LC_UnpaidCharges.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("AsOfDate", p1)
                    Case "RE_Reservation"
                        Report_Path = Report_Path & "\RE_Reservation.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                    Case "RE_NDC"
                        Report_Path = Report_Path & "\RE_NDC.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                    Case "RE_CTS"
                        Report_Path = Report_Path & "\RE_CTS.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                    Case "RE_Waiver"
                        Report_Path = Report_Path & "\RE_Waiver.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                    Case "RE_NC"
                        Report_Path = Report_Path & "\RE_NC.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                    Case "RE_LOR"
                        Report_Path = Report_Path & "\RE_LOR.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                    Case "RE_BIS"
                        Report_Path = Report_Path & "\RE_BIS.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                    Case "RE_CAM"
                        Report_Path = Report_Path & "\RE_CAM.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                    Case "RE_AMR"
                        Report_Path = Report_Path & "\RE_AMR.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("AsOfDate", Date.Today.Date)
                    Case "RE_AU"
                        Report_Path = Report_Path & "\RE_AU.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("AsOfDate", Date.Today.Date)
                    Case "RE_NUE"
                        Report_Path = Report_Path & "\RE_NUE.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("TransID", p1)
                    Case "RE_TotalSales"
                        Report_Path = Report_Path & "\RE_TotalSales.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@Status", p1)
                        CR.SetParameterValue("@CostCenter", p2)
                        CR.SetParameterValue("@DateFrom", p3)
                        CR.SetParameterValue("@DateTo", p4)
                    Case "RE_ComExp"
                        Report_Path = Report_Path & "\RE_ComExp.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p1)
                        CR.SetParameterValue("DateTo", p2)
                        CR.SetParameterValue("Type", p3)
                        CR.SetParameterValue("CostCenter", p4)
                        CR.SetParameterValue("VCE", p5)

                    Case "BM"
                        Report_Path = Report_Path & "\BM_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                        CR.SetParameterValue("@Username", Username)
                    Case "BM_TripSummary"
                        Report_Path = Report_Path & "\" & rpt & ".rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("DateFrom", p3)
                        CR.SetParameterValue("DateTo", p4)
                    Case "BM_Unpaid"
                        Report_Path = Report_Path & "\" & rpt & ".rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("Type", p2)
                    Case "BM_Unbilled"
                        Report_Path = Report_Path & "\" & rpt & ".rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("Type", p2)
                    Case "PDC"
                        Report_Path = Report_Path & "\PDC_Printout.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@TransID", p1)
                    Case "FSBSCC"
                        Report_Path = Report_Path & "\FSBSCC.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("DateTo", p2)
                        CR.SetParameterValue("Filter", p4)
                    Case "FSIS_CC"
                        Report_Path = Report_Path & "\FSIS_CC.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("User", p1)
                        CR.SetParameterValue("DateTo", p2)
                        CR.SetParameterValue("Filter", p4)
                    Case "AP_Aging"
                        Report_Path = Report_Path & "\AP_Aging.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@ASOFDate", p2)
                        CR.SetParameterValue("@AccntCode", p3)
                        CR.SetParameterValue("VCE", p4)
                    Case "AP_Aging_CostCenter"
                        Report_Path = Report_Path & "\AP_Aging_CostCenter.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@ASOFDate", p2)
                        CR.SetParameterValue("@AccntCode", p3)
                        CR.SetParameterValue("VCE", p4)
                        CR.SetParameterValue("@CostCenter", p5)
                    Case "AP_Aging_Detailed"
                        Report_Path = Report_Path & "\AP_Aging_Detailed.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@ASOFDate", p2)
                        CR.SetParameterValue("@AccntCode", p3)
                        CR.SetParameterValue("VCE", p4)
                    Case "AP_Aging_Detailed_CostCenter"
                        Report_Path = Report_Path & "\AP_Aging_Detailed_CostCenter.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@ASOFDate", p2)
                        CR.SetParameterValue("@AccntCode", p3)
                        CR.SetParameterValue("VCE", p4)
                        CR.SetParameterValue("@CostCenter", p5)
                    Case "AP_Aging_Summary"
                        Report_Path = Report_Path & "\AP_Aging_Summary.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@ASOFDate", p2)
                        CR.SetParameterValue("@AccntCode", p3)
                        CR.SetParameterValue("VCE", p4)

                    Case "AP_Aging_Summary_CostCenter"
                        Report_Path = Report_Path & "\AP_Aging_Summary_CostCenter.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@ASOFDate", p2)
                        CR.SetParameterValue("@AccntCode", p3)
                        CR.SetParameterValue("VCE", p4)
                        CR.SetParameterValue("@CostCenter", p5)

                    Case "Inventory_Property"
                        Report_Path = Report_Path & "\Inventory_Property.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("Status", p2)
                        CR.SetParameterValue("AsOfDate", Date.Today.Date)

                    Case "Cash_Flow"
                        Report_Path = Report_Path & "\SCF1.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("@Year", DateTime.Parse(p2).Year)
                        CR.SetParameterValue("@Month", DateTime.Parse(p2).Month)
                        CR.SetParameterValue("@Year", DateTime.Parse(p2).Year, "SCF_Operating.rpt")
                        CR.SetParameterValue("@Month", DateTime.Parse(p2).Month, "SCF_Operating.rpt")
                        CR.SetParameterValue("@Year", DateTime.Parse(p2).Year, "SCF_Investing.rpt")
                        CR.SetParameterValue("@Month", DateTime.Parse(p2).Month, "SCF_Investing.rpt")
                        CR.SetParameterValue("@Year", DateTime.Parse(p2).Year, "SCF_Financing.rpt")
                        CR.SetParameterValue("@Month", DateTime.Parse(p2).Month, "SCF_Financing.rpt")

                    Case "FSSCE"
                        Report_Path = Report_Path & "\FSSCE.rpt"
                        CR.Load(Report_Path)
                        CR.SetDatabaseLogon(DBUser, DBPassword)
                        CR.SetParameterValue("DateFrom", p2)
                        CR.SetParameterValue("User", p1)
                End Select
            End If


            'CrTables = CR.Database.Tables


            'For Each CrTable In CrTables

            '    crtableLogoninfo = CrTable.LogOnInfo
            '    crConnectionInfo.ServerName = "(local)"
            '    crConnectionInfo.DatabaseName = "CAS_GA"
            '    crConnectionInfo.UserID = "eVoSolution"
            '    crConnectionInfo.Password = "eVoSolutiontest"
            '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
            '    CrTable.Location.Substring(CrTable.Location.LastIndexOf(".") + 1)

            'Next

            ''    crReportDocument.ReportOptions.EnableSaveDataWithReport = False
            ''Refresh the ReportViewer Object
            'CrystalReportViewer1.RefreshReport()
            'Bind the ReportDocument to ReportViewer Object
            ' CrystalReportViewer1.ReportSource = CR

            With crConnectionInfo
                .ServerName = database
                'If you are connecting to Oracle there is no DatabaseName. Use an empty string.
                'For example, .DatabaseName = ""
                .DatabaseName = database
                .UserID = DBUser
                .Password = DBPassword
            End With

            CrTables = CR.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next
            GC.Collect()
            GC.WaitForPendingFinalizers()
            CrystalReportViewer1.ReportSource = CR

        Catch exs As SqlException
            MessageBox.Show(exs.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
        End Try

    End Sub

End Class