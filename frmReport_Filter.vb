

Public Class frmReport_Filter
    Public Report As String
    Dim branch As String
    Dim rpt, p1 As String

    Public Overloads Function ShowDialog(Optional param4 As String = "", Optional param5 As String = "") As Boolean

        rpt = param4
        p1 = param5
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmReport_Filter_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If rpt = "FA" Then
            cbPeriod.SelectedItem = "As Of"
            nupYear.Value = Date.Today.Year
            cbMonth.SelectedIndex = Date.Today.Month - 1
            cbPeriod.Enabled = False
            cbStatus.Enabled = False
            gbType.Enabled = False
            LoadStatus()
        Else
            cbPeriod.SelectedItem = "Monthly"
            nupYear.Value = Date.Today.Year
            cbMonth.SelectedIndex = Date.Today.Month - 1
            LoadStatus()
        End If
    End Sub



    Private Sub LoadStatus()
        Select Case Report
            Case "PR List"
                Dim query As String
                query = " SELECT DISTINCT Status  FROM tblPR	 "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                cbStatus.Items.Add("All")
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0
            Case "PO List"
                Dim query As String
                query = " SELECT DISTINCT Status  FROM viewPO_Status	 "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                cbStatus.Items.Add("All")
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0
            Case "RFP List"
                Dim query As String
                query = " SELECT DISTINCT Status  FROM tblRFP	 "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                cbStatus.Items.Add("All")
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0
            Case ("CA List")
                Dim query As String
                query = " SELECT DISTINCT Status  FROM tblCA	 "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                cbStatus.Items.Add("All")
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0
            Case "PCV List"
                Dim query As String
                query = " SELECT DISTINCT Status  FROM tblPCV	 "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                cbStatus.Items.Add("All")
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0
            Case "APV List"
                Dim query As String
                query = " SELECT DISTINCT Status  FROM tblAPV	 "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                cbStatus.Items.Add("All")
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0
            Case "RR List"
                Dim query As String
                query = " SELECT DISTINCT Status  FROM tblRR "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                cbStatus.Items.Add("All")
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0
            Case "MRIS List"
                Dim query As String
                query = " SELECT DISTINCT Status  FROM tblMRIS "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                cbStatus.Items.Add("All")
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0
            Case "SO List"
                Dim query As String
                query = " SELECT DISTINCT Status  FROM tblSO "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                cbStatus.Items.Add("All")
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0
            Case "Collection List"
               Dim query As String
                query = " SELECT DISTINCT Status  FROM tblCollection WHERE TransType ='" & rpt & "' "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                cbStatus.Items.Add("All")
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0
            Case "CV List"
                Dim query As String
                query = " SELECT DISTINCT Status  FROM tblCV "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                cbStatus.Items.Add("All")
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0
            Case "SJ List"
                Dim query As String
                query = " SELECT DISTINCT Status  FROM tblSJ "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                cbStatus.Items.Add("All")
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0

            Case "PJ List"
                Dim query As String
                query = " SELECT DISTINCT Status  FROM tblPJ "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                cbStatus.Items.Add("All")
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0
            Case "JV List"
                Dim query As String
                query = " SELECT DISTINCT Status  FROM tblJV "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                cbStatus.Items.Add("All")
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0
            Case "SI List"
                Dim query As String
                query = " SELECT DISTINCT Status  FROM tblSI	 "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                cbStatus.Items.Add("All")
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0
            Case "CSI List"
                Dim query As String
                query = " SELECT DISTINCT Status  FROM tblCSI	 "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                cbStatus.Items.Add("All")
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0
            Case "CS Summary"
                cbStatus.Visible = False
                Label1.Visible = False
                gbType.Visible = False
            Case "BS Receiving"
                Dim query As String
                query = " SELECT DISTINCT Status  FROM tblSJ	 "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0

            Case "Fixed Asset List"
                Dim query As String
                query = " SELECT DISTINCT Status  FROM tblDepreciation	 "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0
            Case "BM List"
                Dim query As String
                query = " SELECT DISTINCT Status  FROM viewBM_Status "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                cbStatus.Items.Add("All")
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0
            Case "BM Trip Summary"
                Dim query As String
                query = " SELECT DISTINCT Status  FROM viewBM_Status "
                SQL.ReadQuery(query)
                cbStatus.Items.Clear()
                cbStatus.Items.Add("All")
                While SQL.SQLDR.Read
                    cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
                End While
                cbStatus.SelectedIndex = 0
        End Select

    End Sub

    Private Sub nupYear_ValueChanged(sender As System.Object, e As System.EventArgs) Handles nupYear.ValueChanged, cbMonth.SelectedIndexChanged, chkYTD.CheckedChanged
        LoadPeriod()
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
            ElseIf cbPeriod.SelectedItem = "Daily" Then
                dtpTo.Value = dtpFrom.Value
            ElseIf cbPeriod.SelectedItem = "Yearly" Then
                dtpFrom.Value = Date.Parse("1-1-" & nupYear.Value.ToString)
                dtpTo.Value = Date.Parse("12-31-" & nupYear.Value.ToString)
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

    Private Sub btnPreview_Click(sender As System.Object, e As System.EventArgs) Handles btnPreview.Click
        If Report = "PR List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("PR_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()
        ElseIf Report = "PO List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("PO_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()
        ElseIf Report = "RFP List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("RFP_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()
        ElseIf Report = "PCV List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("PCV_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()
        ElseIf Report = "CA List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("CA_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()
        ElseIf Report = "APV List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("APV_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()
        ElseIf Report = "RR List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("RR_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()
        ElseIf Report = "MRIS List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("MRIS_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()
        ElseIf Report = "SO List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("SO_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()
        ElseIf Report = "Collection List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("Collection_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem, rpt)
            f.Dispose()
        ElseIf Report = "CV List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("CV_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()
        ElseIf Report = "SJ List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("SJ_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()
        ElseIf Report = "PJ List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("PJ_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()
        ElseIf Report = "APV List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("APV_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()
        ElseIf Report = "JV List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("JV_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()
        ElseIf Report = "SI List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("SI_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()
        ElseIf Report = "CSI List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("CSI_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()
        ElseIf Report = "CS Summary" Then
            Dim f As New frmReport_Display
            f.ShowDialog("CS_Summary", dtpFrom.Value.Date, dtpTo.Value.Date)
            f.Dispose()
        ElseIf Report = "BS Receiving" Then
            Dim f As New frmReport_Display
            f.ShowDialog("BS_Receiving_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()
        ElseIf Report = "Fixed Asset List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("FA_List", dtpFrom.Value.Date)
            f.Dispose()
        ElseIf Report = "BM List" Then
            Dim f As New frmReport_Display
            f.ShowDialog("BM_List", "", IIf(rbSummary.Checked, "Summary", "Detailed"), dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()

        ElseIf Report = "BM Trip Summary" Then
            Dim f As New frmReport_Display
            f.ShowDialog("BM_TripSummary", "", "", dtpFrom.Value.Date, dtpTo.Value.Date, cbStatus.SelectedItem)
            f.Dispose()
        End If
    End Sub

    Private Sub cbPeriod_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbPeriod.SelectedIndexChanged
        If cbPeriod.SelectedItem = "Monthly" Then
            gbPeriodYM.Visible = True
            gbPeriodFT.Visible = False
            chkYTD.Visible = False
            cbMonth.Enabled = True
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
            dtpTo.Value = dtpFrom.Value
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
            cbMonth.Enabled = False
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
        LoadPeriod()
    End Sub


    Private Sub dtpFrom_ValueChanged(sender As Object, e As System.EventArgs) Handles dtpFrom.ValueChanged
        If cbPeriod.SelectedItem = "Daily" Then
            dtpTo.Value = dtpFrom.Value
        End If
    End Sub
End Class