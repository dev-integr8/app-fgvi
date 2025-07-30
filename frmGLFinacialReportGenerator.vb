Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Security
Imports System.Security.Principal
Imports System.Net.NetworkInformation
Imports System.Text

Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Runtime.InteropServices


Imports ClosedXML.Excel
Imports System.Configuration

Public Class frmGLFinacialReportGenerator
    Public Period
    Public TextFile As IO.StreamWriter
    Public TextFile1 As IO.StreamWriter
    Public FROMPERIOD, TOPERIOD As String
    Dim SQL As New SQLControl
    Dim branch As String
    Dim exportpath As String
    Dim selectedquery As String = ""

    Private Sub frmGLFinacialReportGenerator_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        nupYear.Value = Date.Today.Year
        cbMonth.SelectedIndex = Date.Today.Month - 1
        LoadAccountCode()
        LoadBranch()
        dtpLastPECdate.Value = GetMaxPEC()
        dtpPECdate.MinDate = dtpLastPECdate.Value
        dtpPECdate.Value = dtpLastPECdate.Value
        btnPostPEC.Enabled = False
    End Sub

    Private Sub LoadBranch()
        Dim query As String
        query = " SELECT    DISTINCT  tblBranch.BranchCode + ' - ' + Description AS BranchCode  " & _
                " FROM      tblBranch    " & _
                " INNER JOIN tblUser_Access    ON   " & _
                " tblBranch.BranchCode = tblUser_Access.Code    " & _
                " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                " WHERE     UserID ='" & UserID & "'  "
        SQL.ReadQuery(query)
        cbStatus.Items.Clear()
        cbStatus.Items.Add("ALL - ALL BRANCHES")
        While SQL.SQLDR.Read
            cbStatus.Items.Add(SQL.SQLDR("BranchCode").ToString)
        End While
        cbStatus.SelectedItem = "ALL - ALL BRANCHES"
    End Sub

    Private Sub LoadPeriod()
        Dim period As String = (cbMonth.SelectedIndex + 1).ToString & "-1-" & nupYear.Value.ToString
        If cbFiscal.Text = "MTD" Then
            dtpFrom.Value = Date.Parse(period)
            dtpTo.Value = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, Date.Parse(period)))
        ElseIf cbFiscal.Text = "YTD" Then
            dtpFrom.Value = Date.Parse("1-1-" & nupYear.Value.ToString)
            dtpTo.Value = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, Date.Parse(period)))
        End If
    End Sub

    Private Sub LoadAccountCode()
        Dim query As String
        query = "SELECT  AccountTitle FROM tblCOA_Master WHERE AccountGroup = 'SubAccount' ORDER BY AccountCode"
        SQL.ReadQuery(query)
        cbAccountFrom.Items.Clear()
        cbAccountTo.Items.Clear()
        While SQL.SQLDR.Read
            cbAccountFrom.Items.Add(SQL.SQLDR("AccountTitle").ToString)
            cbAccountTo.Items.Add(SQL.SQLDR("AccountTitle").ToString)
        End While
    End Sub

    Private Sub LoadTBDetailed()
        Dim query As String
        If cbFiscal.SelectedItem = "MTD" AndAlso chkBB.Checked = True Then
            query = "  SELECT  tblCOA_Master.AccountCode, tblCOA_Master.AccountTitle,   " & _
                    "           Debit, Credit, JE.VCECode, JE.VCEName, Particulars, RefNo, Check_No, Book, RefType, CASE WHEN RefType ='BB' THEN CAST(JE.JE_No AS nvarchar) ELSE CAST(ISNULL(JE.TransNo,RefTransID) AS nvarchar) END AS TransNo, RefTransID as RefTransID,   " & _
                    "           tblCOA_Master.AccountType, JE.BranchCode, AppDate,ISNULL(Client_Name,'') AS Client, ISNULL(viewVCE_Master.Status,'') AS VCEStatus, ISNULL(ProfitCenter,'') AS ProfitCode, ISNULL(PCDEsc,'') AS ProfitCenter, ISNULL(CostCenter,'') AS CostCode, ISNULL(CCDesc,'') AS CostCenter , viewVCE_Master.TIN_No  " & _
                    "  FROM      " & _
                    "  (      SELECT  JE_No, VCECode, VCEName, TransNo, RefTransID, RefType,  AccntCode, AccntTitle,   " & _
                    "  		        Debit, Credit, AppDate, Book , BranchCode, Particulars, RefNo, Status, ProfitCenter, PCDEsc, CostCenter, CCDesc, Check_No " & _
                    "         FROM	view_GL WHERE Status <> 'Cancelled' ) AS JE   " & _
                    "  INNER JOIN tblCOA_Master   " & _
                    "  ON	    CAST(JE.AccntCode AS nvarchar) =  CAST(tblCOA_Master.AccountCode AS nvarchar)     " & _
                    "  LEFT JOIN viewVCE_Master   " & _
                    "  ON	   CAST(JE.VCECode AS nvarchar) =  CAST(viewVCE_Master.VCECode AS nvarchar)   " & _
                    " INNER JOIN tblUser_Access    ON   " & _
                    " tblUser_Access.UserID= 1  AND   tblUser_Access.Type = 'BranchCode' AND       tblUser_Access.Status ='Active'  " & _
                    " AND JE.branchcode = tblUser_Access.Code AND isAllowed = 1  " & _
                    "  WHERE    JE.AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'   AND	   tblCOA_Master.AccountCode >= N'" & GetAccntCode(cbAccountFrom.SelectedItem) & "'  " & _
                    "  AND      tblCOA_Master.AccountCode <= N'" & GetAccntCode(cbAccountTo.SelectedItem) & "' AND JE.Status <> 'Cancelled'  " & _
                    "  AND	   Book IN ('" & IIf(chkPayable.Checked = True, "Accounts Payable", "") & "','" & IIf(chSJ.Checked = True, "Sales", "") & "','" & IIf(chkOR.Checked = True, "Cash Receipts", "") & "','" & IIf(chkDV.Checked = True, "Cash Disbursements", "") & "','" & IIf(chkJV.Checked = True, "General Journal", "") & "','" & IIf(chkPJ.Checked = True, "Purchase Journal", "") & "','" & IIf(chkPEC.Checked = True, "Period End Closing", "") & "','" & IIf(chkBB.Checked = True, "BB", "") & "','" & IIf(chkInventory.Checked = True, "Inventory", "") & "')  " & OrderBy()
        Else
            query = "  SELECT   tblCOA_Master.AccountCode, tblCOA_Master.AccountTitle,   " & _
                    "           Debit AS Debit, Credit AS Credit, view_GL.VCECode, view_GL.VCEName, Particulars, RefNo, Check_No, Book, RefType, CASE WHEN RefType ='BB' THEN CAST(view_GL.JE_No AS nvarchar) ELSE CAST(ISNULL(TransNo,RefTransID) AS nvarchar) END AS TransNo, RefTransID as RefTransID,   " & _
                    "           tblCOA_Master.AccountType, view_GL.BranchCode, Appdate, ISNULL(Client_Name,'') AS Client, ISNULL(viewVCE_Master.Status,'') AS VCEStatus, ISNULL(ProfitCenter,'') AS ProfitCode, ISNULL(PCDEsc,'') AS ProfitCenter , ISNULL(CostCenter,'') AS CostCode, ISNULL(CCDesc,'') AS CostCenter , viewVCE_Master.TIN_No " & _
                    "  FROM     view_GL INNER JOIN tblCOA_Master   " & _
                    "   ON	    CAST(view_GL.AccntCode AS nvarchar) =  CAST(tblCOA_Master.AccountCode AS nvarchar)  " & _
                    " LEFT JOIN viewVCE_Master   " & _
                    "  ON	   CAST(view_GL.VCECode AS nvarchar) =  CAST(viewVCE_Master.VCECode AS nvarchar)   " & _
                    " INNER JOIN tblUser_Access    ON   " & _
                    " tblUser_Access.UserID= 1  AND   tblUser_Access.Type = 'BranchCode' AND       tblUser_Access.Status ='Active'  " & _
                    " AND view_GL.branchcode = tblUser_Access.Code AND isAllowed = 1  " & _
                    "  WHERE    view_GL.AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  AND	   tblCOA_Master.AccountCode >= N'" & GetAccntCode(cbAccountFrom.SelectedItem) & "'  " & _
                    "  AND      tblCOA_Master.AccountCode <= N'" & GetAccntCode(cbAccountTo.SelectedItem) & "'  AND view_GL.Status <> 'Cancelled'  " & _
                     "  AND	   Book IN ('" & IIf(chkPayable.Checked = True, "Accounts Payable", "") & "','" & IIf(chSJ.Checked = True, "Sales", "") & "','" & IIf(chkOR.Checked = True, "Cash Receipts", "") & "','" & IIf(chkDV.Checked = True, "Cash Disbursements", "") & "','" & IIf(chkJV.Checked = True, "General Journal", "") & "','" & IIf(chkPJ.Checked = True, "Purchase Journal", "") & "','" & IIf(chkPEC.Checked = True, "Period End Closing", "") & "','" & IIf(chkBB.Checked = True, "BB", "") & "', '" & IIf(chkInventory.Checked = True, "Inventory", "") & "')  " ')  " & OrderBy()
        End If
        selectedquery = query
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvData.DataSource = SQL.SQLDS.Tables(0)
            dgvData.Columns(2).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(3).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(2).Frozen = True
            dgvData.Columns(3).Frozen = True
            dgvData.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            TotalRR()
        Else
            dgvData.DataSource = Nothing
        End If
    End Sub

    Private Sub LoadTBSummary()
        Dim query As String
        If cbFiscal.SelectedItem = "MTD" AndAlso chkBB.Checked = True Then
            query = "  SELECT   tblCOA_Master.AccountCode, tblCOA_Master.AccountTitle,   " & _
                    "           CASE WHEN SUM(Debit) > SUM(Credit) THEN SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit,  " & _
                    "           CASE WHEN SUM(Credit) > SUM(Debit) THEN SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit,  " & _
                    "           tblCOA_Master.AccountType, BranchCode  " & _
                    "  FROM      " & _
                    "  (      SELECT  JE_No, VCECode, RefTransID, RefType,  AccntCode, AccntTitle,   " & _
                    "  		        Debit, Credit, AppDate, BranchCode, Book , Status  " & _
                    "         FROM	view_GL WHERE Status <> 'Cancelled' ) AS JE   " & _
                    "  INNER JOIN tblCOA_Master   " & _
                    "  ON	   JE.AccntCode = tblCOA_Master.AccountCode   " & _
                    "  WHERE    JE.AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'   " & IIf(branch = "ALL", "AND BranchCode in ( SELECT    DISTINCT  tblBranch.BranchCode   " & _
                    " FROM      tblBranch    " & _
                    " INNER JOIN tblUser_Access    ON   " & _
                    " tblBranch.BranchCode = tblUser_Access.Code    " & _
                    " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                    " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                    " WHERE     UserID ='" & UserID & "'  )", " AND BranchCode = '" & branch & "' ") & _
                    "  AND	   tblCOA_Master.AccountCode  >= N'" & GetAccntCode(cbAccountFrom.SelectedItem) & "'  " & _
                    "  AND      tblCOA_Master.AccountCode  <= N'" & GetAccntCode(cbAccountTo.SelectedItem) & "'   AND JE.Status <> 'Cancelled'" & _
                                "  AND	   Book IN ('" & IIf(chkPayable.Checked = True, "Accounts Payable", "") & "','" & IIf(chSJ.Checked = True, "Sales", "") & "','" & IIf(chkOR.Checked = True, "Cash Receipts", "") & "','" & IIf(chkDV.Checked = True, "Cash Disbursements", "") & "','" & IIf(chkJV.Checked = True, "General Journal", "") & "','" & IIf(chkPJ.Checked = True, "Purchase Journal", "") & "','" & IIf(chkPEC.Checked = True, "Period End Closing", "") & "','" & IIf(chkBB.Checked = True, "BB", "") & "','" & IIf(chkInventory.Checked = True, "Inventory", "") & "')  " & _
        "  GROUP BY tblCOA_Master.AccountCode, tblCOA_Master.AccountTitle, tblCOA_Master.AccountType,  BranchCode "
        Else
            query = "  SELECT   tblCOA_Master.AccountCode, tblCOA_Master.AccountTitle,   " & _
                    "           CASE WHEN SUM(Debit) > SUM(Credit) THEN SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit,  " & _
                    "           CASE WHEN SUM(Credit) > SUM(Debit) THEN SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit,  " & _
                    "           tblCOA_Master.AccountType, BranchCode " & _
                    "  FROM     view_GL INNER JOIN tblCOA_Master   " & _
                    "  ON	   view_GL.AccntCode = tblCOA_Master.AccountCode   " & _
                    "  WHERE    view_GL.AppDate BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'   " & IIf(branch = "ALL", "AND BranchCode in ( SELECT    DISTINCT  tblBranch.BranchCode   " & _
                    " FROM      tblBranch    " & _
                    " INNER JOIN tblUser_Access    ON   " & _
                    " tblBranch.BranchCode = tblUser_Access.Code    " & _
                    " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                    " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                    " WHERE     UserID ='" & UserID & "'  )", " AND BranchCode = '" & branch & "' ") & _
                    "  AND	   tblCOA_Master.AccountCode >= N'" & GetAccntCode(cbAccountFrom.SelectedItem) & "'  " & _
                    "  AND      tblCOA_Master.AccountCode <= N'" & GetAccntCode(cbAccountTo.SelectedItem) & "' AND view_GL.Status <> 'Cancelled' " & _
                                "  AND	   Book IN ('" & IIf(chkPayable.Checked = True, "Accounts Payable", "") & "','" & IIf(chSJ.Checked = True, "Sales", "") & "','" & IIf(chkOR.Checked = True, "Cash Receipts", "") & "','" & IIf(chkDV.Checked = True, "Cash Disbursements", "") & "','" & IIf(chkJV.Checked = True, "General Journal", "") & "','" & IIf(chkPJ.Checked = True, "Purchase Journal", "") & "','" & IIf(chkPEC.Checked = True, "Period End Closing", "") & "','" & IIf(chkBB.Checked = True, "BB", "") & "','" & IIf(chkInventory.Checked = True, "Inventory", "") & "')  " & _
        "  GROUP BY tblCOA_Master.AccountCode, tblCOA_Master.AccountTitle, tblCOA_Master.AccountType,  BranchCode "
        End If
        selectedquery = query
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvData.DataSource = SQL.SQLDS.Tables(0)
            dgvData.Columns(2).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(3).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Else
            dgvData.DataSource = Nothing
            dgvData.Refresh()
            MsgBox("No Record Found", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Function OrderBy() As String
        If rbOrderDate.Checked = True Then
            Return " ORDER BY JE.AppDate"
        ElseIf rbOrderSubType.Checked = True Then
            Return " ORDER BY Account_Sub_Type "
        ElseIf rbOrderTransactionNumber.Checked = True Then
            Return " ORDER BY Sub_Category "
        ElseIf rbAccountCode.Checked = True Then
            Return " ORDER BY AccountCode "
        ElseIf rbAccountTitle.Checked = True Then
            Return " ORDER BY AccountTitle "
        ElseIf rbRefID.Checked = True Then
            Return " ORDER BY RefTransID "
        Else
            Return ""
        End If
    End Function

    Private Function GetAccntCode(ByVal AccntTitle As String) As String
        Dim query As String
        query = " SELECT AccountCode FROM tblCOA_Master WHERE AccountTitle = '" & AccntTitle & "' "
        SQL.FlushParams()
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("AccountCode").ToString
        Else
            Return ""
        End If
    End Function

    Private Sub btnGenerate_Click(sender As System.Object, e As System.EventArgs) Handles btnGenerate.Click
        Try
            branch = Strings.Left(cbStatus.SelectedItem, cbStatus.SelectedItem.ToString.IndexOf(" - "))
            GenerateReport()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub GenerateReport()
        If cbAccountFrom.SelectedIndex = -1 Or cbAccountTo.SelectedIndex = -1 Then
            MsgBox("Please select account code!", MsgBoxStyle.Exclamation)
        Else
            dgvData.DataSource = Nothing
            If dgvData.Rows.Count > 0 Then
                dgvData.Rows.Clear()
            End If
            If dgvData.Columns.Count > 0 Then
                dgvData.Columns.Clear()
            End If

            If rbDetailed.Checked = True Then
                LoadTBDetailed()
            Else
                LoadTBSummary()
            End If
        End If
        TotalRR()
    End Sub

    Private Sub cmbToDepartment_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles cmbToDepartment.KeyDown
        'Try
        '    If e.KeyCode = Keys.Enter Then
        '        Dim cmd As New SqlCommand
        '        Dim dr As SqlDataReader

        '        cmd = conn.CreateCommand
        '        cmd.CommandText = " SELECT        dbo.ChartOfAccount.Account_Type, dbo.ChartOfAccount.Account_Sub_Type, dbo.ChartOfAccount.Sub_Category, dbo.ChartOfAccount.Segment, SUM(je.Debit) AS Debit, SUM(je.Credit) AS Credit" & _
        '                        " FROM            dbo.view_GL AS je INNER JOIN     dbo.ChartOfAccount ON je.AccntCode = dbo.ChartOfAccount.AccntCode" & _
        '                        " GROUP BY je.FromPeriod, je.ToPeriod, dbo.ChartOfAccount.Account_Type, dbo.ChartOfAccount.Account_Sub_Type, dbo.ChartOfAccount.Sub_Category, dbo.ChartOfAccount.Segment" & _
        '                        " HAVING        (je.FromPeriod >= '" & FROMPERIOD & "') AND (je.ToPeriod <= '" & TOPERIOD & "') AND (dbo.ChartOfAccount.Segment >= N'" & cbAccountFrom.Text & "' AND dbo.ChartOfAccount.Segment <= N'" & cmbToDepartment.Text & "')"

        '        dr = cmd.ExecuteReader
        '        If dr.HasRows Then
        '            While dr.Read
        '                dgvData.Rows.Add(New String() {dr("Account_Type").ToString, _
        '                                                   dr("Account_Sub_Type").ToString, _
        '                                                    dr("Sub_Category").ToString, _
        '                                                    dr("Segment").ToString, _
        '                                                       "", _
        '                                                      "", _
        '                                                      dr("Debit").ToString, _
        '                                                     dr("Credit").ToString, ""})
        '            End While
        '            dgvData.Columns(0).Visible = True
        '            dgvData.Columns(1).Visible = True
        '            dgvData.Columns(2).Visible = True
        '            dgvData.Columns(3).Visible = True
        '            dgvData.Columns(4).Visible = False
        '            dgvData.Columns(5).Visible = False
        '            dgvData.Columns(6).Visible = True
        '            dgvData.Columns(7).Visible = True
        '        End If
        '        dr.Close()
        '        conn.Close()

        '    End If
        'Catch exs As SqlException

        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub cmbPeriod_Covered_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)

        Period = Split(dtpFrom.Text, " To ")
        If UBound(Period) > 0 Then
            FROMPERIOD = Period(0)
            TOPERIOD = Period(1)
        Else
            FROMPERIOD = dtpFrom.Text
            TOPERIOD = dtpTo.Text
        End If

    End Sub


    Private Sub cmbToDepartment_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbToDepartment.SelectedIndexChanged

    End Sub

    Private Sub cmbToAccountCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles cbAccountTo.KeyDown
        'Try

        '    Period = Split(dtpFrom.Text, " To ")
        '    FROMPERIOD = Period(0)
        '    TOPERIOD = Period(1)
        '    If e.KeyCode = Keys.Enter Then
        '        Dim cmd As New SqlCommand
        '        Dim dr As SqlDataReader
        '        cmd = conn.CreateCommand
        '        cmd.CommandText = " SELECT        dbo.ChartOfAccount.Account_Type, dbo.ChartOfAccount.Account_Sub_Type, dbo.ChartOfAccount.Sub_Category, dbo.ChartOfAccount.Segment, SUM(je.Debit) AS Debit, SUM(je.Credit) AS Credit" & _
        '                        " FROM            dbo.view_GL AS je INNER JOIN     dbo.ChartOfAccount ON je.AccntCode = dbo.ChartOfAccount.AccntCode" & _
        '                        " where        (je.FromPeriod >= '" & FROMPERIOD & "') AND (je.ToPeriod <= '" & TOPERIOD & "') AND (dbo.ChartOfAccount.AccntCode >= N'" & cbAccountFrom.Text & "' AND dbo.ChartOfAccount.AccntCode <= N'" & cbAccountTo.Text & "')" & _
        '                        " GROUP BY je.FromPeriod, je.ToPeriod, dbo.ChartOfAccount.Account_Type, dbo.ChartOfAccount.Account_Sub_Type, dbo.ChartOfAccount.Sub_Category, dbo.ChartOfAccount.Segment"

        '        dr = cmd.ExecuteReader
        '        If dr.HasRows Then
        '            While dr.Read
        '                dgvData.Rows.Add(New String() {dr("Account_Type").ToString, _
        '                                                   dr("Account_Sub_Type").ToString, _
        '                                                    dr("Sub_Category").ToString, _
        '                                                    dr("Segment").ToString, _
        '                                                       "", _
        '                                                      "", _
        '                                                      dr("Debit").ToString, _
        '                                                     dr("Credit").ToString, ""})
        '            End While
        '            dgvData.Columns(0).Visible = True
        '            dgvData.Columns(1).Visible = True
        '            dgvData.Columns(2).Visible = True
        '            dgvData.Columns(3).Visible = True
        '            dgvData.Columns(4).Visible = False
        '            dgvData.Columns(5).Visible = False
        '            dgvData.Columns(6).Visible = True
        '            dgvData.Columns(7).Visible = True
        '        End If
        '        dr.Close()
        '        conn.Close()

        '    End If
        'Catch exs As SqlException

        'Catch ex As Exception

        'End Try

    End Sub


    Private Sub CmdPreview_Click(sender As System.Object, e As System.EventArgs) Handles CmdPreview.Click
        Period = Split(dtpFrom.Text, " To ")
        If UBound(Period) > 0 Then
            FROMPERIOD = Period(0)
            TOPERIOD = Period(1)
        Else
            FROMPERIOD = dtpFrom.Text
            TOPERIOD = dtpTo.Text
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        printtotxt()
    End Sub


    Private Sub printtotxt()
        'Try
        '    Dim maxtransid As String
        '    Dim App_Path As String
        '    App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName


        '    maxtransid = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
        '    App_Path = App_Path & "\DownloadedFiles\ReportGenerator"
        '    TextFile = New StreamWriter(App_Path & maxtransid & ".txt")


        '    TextFile.WriteLine(Date.Now)


        '    If dgvData.RowCount > 0 Then
        '    End If

        '    'debit compute & print in textbox
        '    Dim a As Double = 0
        '    For x As Integer = 0 To dgvData.ColumnCount - 2
        '        '  maxtransid = dgvCVRR.Columns(x).HeaderText
        '        TextFile.Write(dgvData.Columns(x).HeaderText)        '((dgvCVRR.Columns(0))  '.Item(1).Value.Columns))
        '        TextFile.Write("|")
        '    Next
        '    TextFile.WriteLine()
        '    For y As Integer = 0 To dgvData.Rows.Count - 1
        '        For x As Integer = 0 To dgvData.ColumnCount - 2
        '            TextFile.Write((dgvData.Item(x, y).Value).ToString)
        '            TextFile.Write("|")
        '        Next
        '        TextFile.WriteLine()
        '    Next


        '    TextFile.Close()
        '    'MessageBox("Txt file created : " & App_Path & maxtransid & ".txt", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)

        'Catch exs As SqlException
        '    MessageBox.Show(exs.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
        '    CN.Close()
        '    CN2.Close()
        '    CN3.Close()
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
        '    CN.Close()
        '    CN2.Close()
        '    CN3.Close()
        'End Try
    End Sub
    Private Sub TotalRR()
        Try
            'debit compute & print in textbox
            Dim a As Double = 0
            For i As Integer = 0 To dgvData.Rows.Count - 1

                If Val(dgvData.Item(2, i).Value) <> 0 Then
                    a = a + Double.Parse(dgvData.Item(2, i).Value).ToString("N2")
                End If
            Next
            txtTotalDebit.Text = a.ToString("N2")

            'credit compute & print in textbox
            Dim b As Double = 0

            For i As Integer = 0 To dgvData.Rows.Count - 1
                If Val(dgvData.Item(3, i).Value) <> 0 Then
                    b = b + Double.Parse(dgvData.Item(3, i).Value).ToString("N2")
                End If
            Next
            txtTotalCredit.Text = b.ToString("N2")
            txtTotalVariance.Text = Math.Abs(CDec(txtTotalDebit.Text) - CDec(txtTotalCredit.Text))
        Catch exs As SqlException
            MessageBox.Show(exs.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
        End Try
    End Sub

    Private Sub TotalRRPEC()
        Try
            'debit compute & print in textbox
            Dim a As Double = 0
            For i As Integer = 0 To dgvData.Rows.Count - 1

                If Val(dgvData.Item(2, i).Value) <> 0 Then
                    a = a + Double.Parse(dgvData.Item(2, i).Value).ToString("N2")
                End If
            Next
            txtTotalDebit.Text = a.ToString("N2")

            'credit compute & print in textbox
            Dim b As Double = 0

            For i As Integer = 0 To dgvData.Rows.Count - 1
                If Val(dgvData.Item(3, i).Value) <> 0 Then
                    b = b + Double.Parse(dgvData.Item(3, i).Value).ToString("N2")
                End If
            Next
            txtTotalCredit.Text = b.ToString("N2")
            txtTotalVariance.Text = Math.Abs(CDec(txtTotalDebit.Text) - CDec(txtTotalCredit.Text))
        Catch exs As SqlException
            MessageBox.Show(exs.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
        End Try
    End Sub

    Private Sub btnGeneratePEC_Click(sender As System.Object, e As System.EventArgs) Handles btnGeneratePEC.Click
        If dtpPECdate.Value = dtpLastPECdate.Value Then
            LoadPEC()
            MsgBox("This Period is already posted!", MsgBoxStyle.Information)
        Else
            GenerateEntry()
        End If
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles btnPostPEC.Click
        Try
            If dgvData.RowCount <> 0 Then
                If MsgBox("Are you sure you want to Post this Period End Closing Entry for the Year: " & Year(dtpPECdate.Value) & " and Month : " & Month(dtpPECdate.Value) & " ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = vbYes Then
                    SavePECEntry()
                    MsgBox("Period Posted Successfully!", MsgBoxStyle.Information)
                    dtpLastPECdate.Value = GetMaxPEC()
                    dtpPECdate.MinDate = GetMaxPEC()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function LoadJE_PEC(ByVal Ref_Type As String, AppDate As Date) As String
        Dim query As String
        query = " SELECT JE_No FROM tblJE_Header WHERE RefType='" & Ref_Type & "' AND AppDate ='" & AppDate & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read AndAlso Not IsDBNull(SQL.SQLDR("JE_No")) Then
            Return SQL.SQLDR("JE_No")
        Else
            Return 0
        End If
    End Function
    Private Sub SavePECEntry()
        Dim insertSQL, JETransiD As String
        Try
            Dim AccntTitle, AccntCode As String
            Dim DebitAmount, CreditAmount As Double

            Dim transID As String = GenerateTransID("RecordID", "tblPEC")
            insertSQL = " INSERT INTO tblPEC (RecordID, datePEC, Amount, Status, DateCreated, WhoCreated) " & _
                     " VALUES (@RecordID, @datePEC, @Amount, @Status, GETDATE(), @WhoCreated) "
            SQL.FlushParams()
            SQL.AddParam("@RecordID", transID)
            SQL.AddParam("@datePEC", dtpPECdate.Value)
            SQL.AddParam("@Amount", CDec(txtTotalDebit.Text))
            SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)


            JETransiD = GenerateTransID("JE_No", "tblJE_Header")

            insertSQL = " INSERT INTO " & _
                       " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, WhoCreated) " & _
                       " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @WhoCreated)"
            SQL.FlushParams()
            SQL.AddParam("@JE_No", JETransiD)
            SQL.AddParam("@AppDate", dtpPECdate.Value)
            SQL.AddParam("@RefType", "PEC")
            SQL.AddParam("@RefTransID", transID)
            SQL.AddParam("@Book", "PEC")
            SQL.AddParam("@TotalDBCR", CDec(txtTotalDebit.Text))
            SQL.AddParam("@Remarks", "Period End Closing")
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)
            'JETransiD = LoadJE("PEC", transID)
            For i As Integer = 0 To dgvData.RowCount - 1
                AccntCode = dgvData.Item(0, i).Value
                AccntTitle = dgvData.Item(1, i).Value
                If IsNumeric(dgvData.Item(2, i).Value) Then
                    DebitAmount = dgvData.Item(2, i).Value.Replace(",", "")
                Else
                    DebitAmount = 0
                End If
                If IsNumeric(dgvData.Item(3, i).Value) Then
                    CreditAmount = dgvData.Item(3, i).Value.Replace(",", "")
                Else
                    CreditAmount = 0
                End If
                insertSQL = " INSERT INTO " & _
                            " tblJE_Details (LineNumber, JE_No, BranchCode, AccntCode, VCECode, " & _
                            "             Debit, Credit, Particulars)" & _
                            " VALUES (@LineNumber, @JE_No, @BranchCode, @AccntCode, @VCECode, " & _
                            "         @Debit, @Credit, @Particulars)"
                SQL.AddParam("@LineNumber", i + 1)
                SQL.AddParam("@JE_No", JETransiD)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@AccntCode", AccntCode)
                SQL.AddParam("@VCECode", "")
                SQL.AddParam("@Debit", DebitAmount)
                SQL.AddParam("@Credit", CreditAmount)
                SQL.AddParam("@Particulars", "Period End Closing")
                SQL.ExecNonQuery(insertSQL)
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub GenerateEntry()
        Dim query As String
        Dim PEC_Amount As Decimal = 0

        query = " SELECT	AccntCode, AccntTitle,  " & _
                " 		CASE WHEN SUM(Credit) > SUM(Debit) THEN SUM(Credit) - SUM(Debit) ELSE 0 END AS Debit, " & _
                " 		CASE WHEN SUM(Debit) > SUM(Credit) THEN SUM(Debit) - SUM(Credit) ELSE 0 END AS Credit " & _
                " FROM view_GL WHERE AppDate <= '" & dtpPECdate.Value.Date & "'  AND Status <> 'Cancelled' " & _
                " AND  AccntCode IN (SELECT AccountCode FROM tblCOA_Master WHERE AccountType IN ('Income Statement')) " & _
                " GROUP BY AccntCode, AccntTitle " & _
                " ORDER BY AccntCode "
        SQL.ReadQuery(query)
        dgvData.ClearSelection()
        dgvData.Rows.Clear()
        If SQL.SQLDR.HasRows Then
            dgvData.Columns.Clear()
            dgvData.Columns.Add("AccountCode", "Account Code")
            dgvData.Columns.Add("AccountTitle", "Account Title")
            dgvData.Columns.Add("Debit", "Debit")
            dgvData.Columns.Add("Credit", "Credit")
            While SQL.SQLDR.Read
                PEC_Amount = PEC_Amount + (CDec(SQL.SQLDR("Debit")) - CDec(SQL.SQLDR("Credit")))
                dgvData.Rows.Add(New String() {SQL.SQLDR("AccntCode").ToString, _
                                               SQL.SQLDR("AccntTitle").ToString, _
                                        Format(SQL.SQLDR("Debit"), "#,###,###,###.00").ToString, _
                                        Format(SQL.SQLDR("Credit"), "#,###,###,###.00").ToString})
            End While
            dgvData.Columns(0).Visible = True
            dgvData.Columns(1).Visible = True
            dgvData.Columns(2).Visible = True
            dgvData.Columns(3).Visible = True
            AddPECAmount(PEC_Amount)
            btnGeneratePEC.Enabled = False
            btnPostPEC.Enabled = True
        End If
        TotalRRPEC()
    End Sub

    Private Sub LoadPEC()
        Dim query As String
        Dim PEC_Amount As Decimal = 0
        query = " SELECT	AccntCode, AccntTitle,  " & _
                " 		CASE WHEN SUM(Debit) > SUM(Credit) THEN SUM(Debit) - SUM(Credit) ELSE 0 END AS Debit, " & _
                " 		CASE WHEN SUM(Credit) > SUM(Debit) THEN SUM(Credit) - SUM(Debit) ELSE 0 END AS Credit " & _
                " FROM  view_GL WHERE AppDate = '" & dtpLastPECdate.Value.Date & "' AND Book = 'PEC' " & _
                " GROUP BY AccntCode, AccntTitle " & _
                " ORDER BY AccntCode "
        SQL.ReadQuery(query)
        dgvData.ClearSelection()
        dgvData.Rows.Clear()
        If SQL.SQLDR.HasRows Then
            dgvData.Columns.Clear()
            dgvData.Columns.Add("AccountCode", "Account Code")
            dgvData.Columns.Add("AccountTitle", "Account Title")
            dgvData.Columns.Add("Debit", "Debit")
            dgvData.Columns.Add("Credit", "Credit")
            While SQL.SQLDR.Read
                PEC_Amount = PEC_Amount + (CDec(SQL.SQLDR("Debit")) - CDec(SQL.SQLDR("Credit")))
                dgvData.Rows.Add(New String() {SQL.SQLDR("AccntCode").ToString, _
                                               SQL.SQLDR("AccntTitle").ToString, _
                                        Format(SQL.SQLDR("Debit"), "#,###,###,###.00").ToString, _
                                        Format(SQL.SQLDR("Credit"), "#,###,###,###.00").ToString})
            End While
            dgvData.Columns(0).Visible = True
            dgvData.Columns(1).Visible = True
            dgvData.Columns(2).Visible = True
            dgvData.Columns(3).Visible = True
        End If
        TotalRRPEC()
    End Sub
    Private Sub AddPECAmount(Amount As Decimal)
        Dim PECaccount As String = LoadPECAccount()
        dgvData.Rows.Add(New String() {PECaccount, GetAccntTitle(PECaccount), IIf(Amount > 0, 0, Amount), IIf(Amount > 0, Math.Abs(Amount), 0)})
    End Sub
    Private Function LoadPECAccount() As String
        Dim query As String
        query = " SELECT PEC_Account FROM tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("PEC_Account").ToString
        Else
            Return ""
        End If
    End Function

    Private Sub btnTBPEC_Click(sender As System.Object, e As System.EventArgs) Handles btnGenerateBB.Click
        Try
            GenerateBB()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
        End Try
    End Sub

    Private Sub GenerateBB()
        If dgvData.Rows.Count > 0 Then
            dgvData.Rows.Clear()
        End If

        dgvData.Columns.Clear()
        Dim query As String
        query = " SELECT  view_GL.AccntCode, AccntTitle, " & _
                "         CASE WHEN (SUM(view_GL.Debit) - SUM(view_GL.Credit)) > 0 THEN SUM(view_GL.Debit) - SUM(view_GL.Credit) ELSE 0 END AS Debit,  " & _
                "         CASE WHEN (SUM(view_GL.Credit) - SUM(view_GL.Debit)) > 0 THEN SUM(view_GL.Credit) - SUM(view_GL.Debit) ELSE 0 END AS Credit " & _
                " FROM    view_GL " & _
                " WHERE   AppDate >= '01-01-" & dtpLastPECdate.Value.Year & "' AND AppDate <= '" & dtpLastPECdate.Value & "' AND Status <> 'Cancelled'  " & _
                " GROUP BY view_GL.AccntCode, AccntTitle " & _
                " HAVING   ABS(SUM(view_GL.Debit) - SUM(view_GL.Credit)) >1 " & _
                " ORDER BY AccntCode "
        SQL.FlushParams()
        SQL.ReadQuery(query)
        If SQL.SQLDR.HasRows Then
            dgvData.Columns.Clear()
            dgvData.Columns.Add("AccntCode", "Account Code")
            dgvData.Columns.Add("AccntTitle", "Account Title")
            dgvData.Columns.Add("Debit", "Debit")
            dgvData.Columns.Add("Credit", "Credit")
            While SQL.SQLDR.Read
                dgvData.Rows.Add(New String() {SQL.SQLDR("AccntCode").ToString, _
                                               SQL.SQLDR("AccntTitle").ToString, _
                                               Format(SQL.SQLDR("Debit"), "#,###,###,###.00").ToString, _
                                               Format(SQL.SQLDR("Credit"), "#,###,###,###.00").ToString})
            End While
            dgvData.Columns(0).Visible = True
            dgvData.Columns(1).Visible = True
            dgvData.Columns(2).Visible = True
            dgvData.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(3).Visible = True
            dgvData.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            btnGenerateBB.Enabled = False
            btnPostBB.Enabled = True
        End If
        TotalRRPEC()
    End Sub
    Private Sub btnPostBB_Click(sender As System.Object, e As System.EventArgs) Handles btnPostBB.Click
        Try
            Dim applydate As Date
            If MsgBox("Are you sure you want to Post the " & vbNewLine & "Beginning Balance for the Period Year : " & Year(dtpLastPECdate.Value) + 1 & " ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = vbYes Then
                applydate = DateAdd(DateInterval.Day, 1, dtpLastPECdate.Value)
                SaveBB(applydate)
                MsgBox("BB has been posted for the year " & applydate.Year, MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadBBPEC()
        cbFiscal.SelectedItem = "MTD"
        If cbMonth.SelectedIndex = 11 Then
            cbMonth.SelectedIndex = 0
            nupYear.Value = nupYear.Value + 1
        Else
            cbMonth.SelectedIndex = cbMonth.SelectedIndex + 1
        End If
        cbAccountFrom.SelectedIndex = 0
        cbAccountTo.SelectedIndex = cbAccountTo.Items.Count - 1
        rbSummary.Checked = True
        chkBB.Checked = True
        chkDV.Checked = False
        chkOR.Checked = False
        chkPEC.Checked = False
        chkJV.Checked = False
        rbAccountCode.Checked = True
        GenerateReport()
    End Sub

    Private Sub SaveBB(PostDate As Date)
        Dim transiD, JETransiD As Integer
        Dim BBNo As String
        Dim AccntTitle, AccntCode As String
        Dim Debit, Credit As Double

        transiD = GenerateTransID("TransID", "tblBB")
        BBNo = GenerateTransNum(True, "BB", "BB_No", "tblBB")

        Dim SQL1 As New SQLControl
        SQL1.BeginTransaction()
        Try
            Dim insertSQL As String
            activityStatus = True
            insertSQL = " INSERT INTO " & _
                        " tblBB (TransID, BB_No, BranchCode, BusinessCode, DateBB, TotalAmount, Remarks, TransAuto, WhoCreated) " & _
                        " VALUES(@TransID, @BB_No, @BranchCode, @BusinessCode, @DateBB, @TotalAmount, @Remarks, @TransAuto, @WhoCreated)"
            SQL1.FlushParams()
            SQL1.AddParam("@TransID", TransID)
            SQL1.AddParam("@BB_No", BBNo)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@DateBB", PostDate)
            If IsNumeric(txtTotalDebit.Text) Then
                SQL1.AddParam("@TotalAmount", CDec(txtTotalDebit.Text))
            Else
                SQL1.AddParam("@TotalAmount", 0)
            End If
            SQL1.AddParam("@Remarks", "Forwarded Balance for the Year" & PostDate.Year)
            SQL1.AddParam("@TransAuto", True)
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.ExecNonQuery(insertSQL)


            JETransiD = GenerateTransID("JE_No", "tblJE_Header")

            insertSQL = " INSERT INTO " & _
                        " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, WhoCreated) " & _
                        " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @WhoCreated)"
            SQL1.FlushParams()
            SQL1.AddParam("@JE_No", JETransiD)
            SQL1.AddParam("@AppDate", PostDate)
            SQL1.AddParam("@RefType", "BB")
            SQL1.AddParam("@RefTransID", TransID)
            SQL1.AddParam("@Book", "BB")
            SQL1.AddParam("@TotalDBCR", CDec(txtTotalDebit.Text))
            SQL1.AddParam("@Remarks", "Forwarded Balance for the Year" & PostDate.Year)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.ExecNonQuery(insertSQL)

            Dim strRefNo As String = ""
            Dim VCECode As String = ""

            Dim line As Integer = 1
            For Each item As DataGridViewRow In dgvData.Rows
                If item.Cells(0).Value <> Nothing Then
                    AccntCode = item.Cells(0).Value
                    AccntTitle = item.Cells(1).Value
                    If IsNumeric(item.Cells(2).Value) Then
                        Debit = CDec(item.Cells(2).Value)
                    Else
                        Debit = 0
                    End If
                    If IsNumeric(item.Cells(3).Value) Then
                        Credit = CDec(item.Cells(3).Value)
                    Else
                        Credit = 0
                    End If
                    insertSQL = " INSERT INTO " & _
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, CostCenter, CIP_Code, BranchCode) " & _
                                " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @CostCenter, @CIP_Code, @BranchCode)"
                    SQL1.FlushParams()
                    SQL1.AddParam("@JE_No", JETransiD)
                    SQL1.AddParam("@AccntCode", AccntCode)
                    SQL1.AddParam("@VCECode", "")
                    SQL1.AddParam("@Debit", Debit)
                    SQL1.AddParam("@Credit", Credit)
                    SQL1.AddParam("@Particulars", "")
                    SQL1.AddParam("@CostCenter", "")
                    SQL1.AddParam("@CIP_Code", "")
                    SQL1.AddParam("@RefNo", "")
                    SQL1.AddParam("@LineNumber", line)
                    SQL1.AddParam("@BranchCode", BranchCode)

                    SQL1.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
            SQL1.Commit()
        Catch ex As Exception
            SQL1.Rollback()
            TransID = ""
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, "BB", Me.Name.ToString, "INSERT", "BB_No", JETransiD, BusinessType, BranchCode, "", activityStatus)
            SQL1.FlushParams()
        End Try
    End Sub
    Private Sub cmbFiscalOption(sender As System.Object, e As System.EventArgs) Handles cbFiscal.SelectedIndexChanged, cbMonth.SelectedIndexChanged
        If cbMonth.SelectedIndex <> -1 Then
            LoadPeriod()
        End If
    End Sub

    Private Sub rbDetailed_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbDetailed.CheckedChanged
        If rbDetailed.Checked = True Then
            rbRefID.Visible = True
            rbOrderDate.Visible = True
        Else
            If rbRefID.Checked = True Then
                rbAccountCode.Checked = True
                rbOrderDate.Visible = True
            End If
            rbRefID.Visible = False
            rbOrderDate.Visible = False
        End If
    End Sub

    Private Sub btnExport_Click(sender As System.Object, e As System.EventArgs) Handles btnExport.Click
        'Try
        '    If ((dgvData.Columns.Count = 0) Or (dgvData.Rows.Count = 0)) Then
        '        MsgBox("No Records to Export!", MsgBoxStyle.Exclamation, "Message Alert")
        '    Else
        '        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
        '            Export(FolderBrowserDialog1.SelectedPath)
        '        End If
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

        Dim Separator As String = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator

        DataGridToCSV(dgvData, Separator)

        'If btnExport.Text = "Export" Then
        '    If ((dgvData.Columns.Count = 0) Or (dgvData.Rows.Count = 0)) Then
        '        MsgBox("No Records to Export!", MsgBoxStyle.Exclamation, "Message Alert")
        '    Else
        '        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
        '            ' Export(FolderBrowserDialog1.SelectedPath)
        '            exportpath = FolderBrowserDialog1.SelectedPath
        '            pgbCounter.Visible = True
        '            bgwExport.RunWorkerAsync()
        '            btnExport.Text = "Stop"
        '        End If
        '    End If
        'Else
        '    If (bgwExport.IsBusy = True) Then
        '        btnExport.Text = "Export"
        '        pgbCounter.Value = 0
        '        bgwExport.CancelAsync()
        '    End If
        'End If
    End Sub

    Private Sub Export(ByVal Path As String)
        Dim DSet As New DataSet
        DSet.Tables.Add()
        For i As Integer = 0 To dgvData.ColumnCount - 1
            DSet.Tables(0).Columns.Add(dgvData.Columns(i).HeaderText)
        Next
        'add rows to the table
        Dim row As DataRow
        For i As Integer = 0 To dgvData.RowCount - 1
            row = DSet.Tables(0).NewRow
            For j As Integer = 0 To dgvData.Columns.Count - 1
                row(j) = dgvData.Rows(i).Cells(j).Value
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

    Private Sub dgvData_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvData.CellDoubleClick
        Try
            If rbDetailed.Checked = True Then
                Dim RefType As String = dgvData.CurrentRow.Cells(8).Value.ToString
                Dim RefID As String = dgvData.CurrentRow.Cells(10).Value.ToString
                Select Case RefType
                    Case "CV"
                        Dim f As New frmCV
                        f.ShowDialog(RefID)
                        f.Dispose()
                    Case "APV"
                        Dim f As New frmAPV
                        f.ShowDialog(RefID)
                        f.Dispose()
                    Case "JV"
                        Dim f As New frmJV
                        f.ShowDialog(RefID)
                        f.Dispose()
                    Case "OR"
                        Dim f As New frmCollection
                        f.TransType = "OR"
                        f.ShowDialog(RefID)
                        f.Dispose()
                    Case "AR"
                        Dim f As New frmCollection
                        f.TransType = "AR"
                        f.ShowDialog(RefID)
                        f.Dispose()
                End Select
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub dtpPECdate_ValueChanged(sender As Object, e As EventArgs) Handles dtpPECdate.ValueChanged
        btnGeneratePEC.Enabled = True
        btnPostPEC.Enabled = False
        dgvData.Rows.Clear()
        dgvData.Columns.Clear()
    End Sub

    Private Sub dtpLastPECdate_ValueChanged(sender As Object, e As EventArgs) Handles dtpLastPECdate.ValueChanged
        If Month(dtpLastPECdate.Value) = 12 And (dtpLastPECdate.Value.Day) = 31 Then
            btnGenerateBB.Enabled = True
        Else
            btnGenerateBB.Enabled = False
        End If
    End Sub

    Private Delegate Sub SetPGBmaxInvoker(ByVal Value As String)
    Private Sub SetPGBmax(ByVal Value As String)
        If Me.InvokeRequired Then
            Me.Invoke(New SetPGBmaxInvoker(AddressOf SetPGBmax), Value)
        Else
            pgbCounter.Maximum = Value
            pgbCounter.Value = 0
        End If
    End Sub

    Private Sub bgwExport_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwExport.DoWork

        SetPGBmax(dgvData.Rows.Count)  ' Set Progress bar max value.

        Dim DSet As New DataSet
        DSet.Tables.Add()
        For i As Integer = 0 To dgvData.ColumnCount - 1
            DSet.Tables(0).Columns.Add(dgvData.Columns(i).HeaderText)
        Next
        'add rows to the table
        Dim row As DataRow
        For i As Integer = 0 To dgvData.RowCount - 1
            row = DSet.Tables(0).NewRow
            For j As Integer = 0 To dgvData.Columns.Count - 1
                row(j) = dgvData.Rows(i).Cells(j).Value
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
                bgwExport.ReportProgress(rowIndex)
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
    End Sub

    Private Sub bgwExport_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwExport.ProgressChanged
        pgbCounter.Value = e.ProgressPercentage
    End Sub

    Private Sub bgwExport_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwExport.RunWorkerCompleted

        MsgBox("Exported Succesfully", MsgBoxStyle.Information, "Message Alert")
        btnExport.Text = "Export"
    End Sub


    Public Function CreateTable() As DataTable
        SQL.GetDataTable(selectedquery)
        Return SQL.SQLDT
    End Function

    Public Shared Sub PopulateSheet(ByVal dt As DataTable, ByVal File As String)
        'Dim oXL As Excel.Application = CType(CreateObject("Excel.Application"), Excel.Application)
        'Dim oWB As Excel.Workbook
        'Dim oSheet As Excel.Worksheet
        Dim oRng As Excel.Range
        ' oXL.Visible = True

        Dim excel As New Microsoft.Office.Interop.Excel.Application
        Dim wBook As Microsoft.Office.Interop.Excel.Workbook
        Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet

        wBook = excel.Workbooks.Add()
        wSheet = wBook.ActiveSheet()

        'oWB = oXL.Workbooks.Add
        'oSheet = CType(wBook.ActiveSheet, Excel.Worksheet)

        Dim dc As DataColumn
        Dim dr As DataRow
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

        wSheet.Cells.Select()
        wSheet.Columns.AutoFit()
        wSheet.Rows.AutoFit()

        excel.Visible = True
        excel.UserControl = True

        wBook.SaveAs(File)
        oRng = Nothing
        excel.Quit()

        ExcelCleanUp(excel, wBook, wSheet)
    End Sub

    Private Shared Sub ExcelCleanUp( _
    ByVal oXL As Excel.Application, _
    ByVal oWB As Excel.Workbook, _
    ByVal oSheet As Excel.Worksheet)

        GC.Collect()
        GC.WaitForPendingFinalizers()

        Marshal.FinalReleaseComObject(oXL)
        Marshal.FinalReleaseComObject(oSheet)
        Marshal.FinalReleaseComObject(oWB)

        oSheet = Nothing
        oWB = Nothing
        oXL = Nothing

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim dt As New DataTable
        Try
            If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

                dt = CreateTable()
                '    PopulateSheet(dt, FolderBrowserDialog1.SelectedPath & "\" & "ExtractedReport" & Date.Today.Month.ToString("00") & Date.Today.Day.ToString("00") & Date.Today.Year.ToString("00") & Now.Hour.ToString("00") & Now.Minute.ToString("00") & ".xlsx")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dt.Dispose()
        End Try
    End Sub


    Private Sub DataGridToCSV(ByRef dt As DataGridView, Qualifier As String)
        Dim TempDirectory As String = "Temp"
        System.IO.Directory.CreateDirectory(TempDirectory)
        Dim file As String = System.IO.Path.GetRandomFileName & ".csv"
        Dim oWrite = New StreamWriter(TempDirectory & "\" & file, False, Encoding.UTF8)

        Dim CSV As StringBuilder = New StringBuilder()

        Dim i As Integer = 1
        Dim CSVHeader As StringBuilder = New StringBuilder()
        For Each c As DataGridViewColumn In dt.Columns
            If i = 1 Then
                CSVHeader.Append(c.HeaderText.ToString())
            Else
                CSVHeader.Append(Qualifier & c.HeaderText.ToString())
            End If
            i += 1
        Next

        'CSV.AppendLine(CSVHeader.ToString())
        oWrite.WriteLine(CSVHeader.ToString())
        oWrite.Flush()

        For r As Integer = 0 To dt.Rows.Count - 1

            Dim CSVLine As StringBuilder = New StringBuilder()
            Dim s As String = ""
            For c As Integer = 0 To dt.Columns.Count - 1
                If c = 0 Then
                    'CSVLine.Append(Qualifier & gridResults.Rows(r).Cells(c).Value.ToString() & Qualifier)
                    s = s & dgvData.Rows(r).Cells(c).Value.ToString()
                Else
                    'CSVLine.Append("," & Qualifier & gridResults.Rows(r).Cells(c).Value.ToString() & Qualifier)
                    s = s & Qualifier & IIf(dgvData.Rows(r).Cells(c).Value.ToString.Replace(ControlChars.CrLf, " ").Contains(Qualifier), """" & dgvData.Rows(r).Cells(c).Value.ToString.Replace(ControlChars.CrLf, " ") & """", dgvData.Rows(r).Cells(c).Value.ToString.Replace(ControlChars.CrLf, " "))
                End If

            Next
            oWrite.WriteLine(s)
            oWrite.Flush()
            'CSV.AppendLine(CSVLine.ToString())
            'CSVLine.Clear()
        Next

        'oWrite.Write(CSV.ToString())

        oWrite.Close()
        oWrite = Nothing

        System.Diagnostics.Process.Start(TempDirectory & "\" & file)

        GC.Collect()

    End Sub

    Private Sub dgvData_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvData.CellContentClick

    End Sub

    Private Sub cbAccountFrom_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbAccountFrom.SelectedIndexChanged

    End Sub
End Class