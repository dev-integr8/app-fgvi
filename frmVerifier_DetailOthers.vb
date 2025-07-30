Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmVerifier_DetailOthers

    Dim SQL As New SQLControl
    Public strType As String = ""
    Public strAccntCode As String = ""
    Public strVCECode As String = ""
    Private Sub frmVerifier_DetailOthers_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadMember()
        LoadAccount()
        LoadTransactions()
        LoadBalance()
    End Sub

    Private Sub LoadMember()
        Dim selectSQL As String = "SELECT VCEName FROM viewVCE_Master WHERE VCECode = '" & strVCECode & "'"
        SQL.ReadQuery(selectSQL)
        If SQL.SQLDR.Read Then
            txtVCECode.Text = strVCECode
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
        End If
    End Sub

    Private Sub LoadAccount()
        Dim selectSQL As String = "SELECT AccountTitle FROM tblCOA_Master WHERE AccountCode = '" & strAccntCode & "'"
        SQL.ReadQuery(selectSQL)
        If SQL.SQLDR.Read Then
            txtAccount.Text = SQL.SQLDR("AccountTitle").ToString
        End If
    End Sub

    Private Sub LoadTransactions()
        Dim selectSQL As String = ""
        selectSQL = "SELECT RefType, ReftransID, TransNo, AppDate, Debit, Credit, Balance, Remarks FROM view_Ledger WHERE AccntCode = '" & strAccntCode & "' AND VCECode = '" & strVCECode & "' ORDER BY Appdate "
        SQL.ReadQuery(selectSQL)
        dgvLedger.Rows.Clear()
        Dim count As Integer = 1
        While SQL.SQLDR.Read
            If strAccntCode.Substring(0, 1) = "2" Or strAccntCode.Substring(0, 1) = "3" Or strAccntCode.Substring(0, 1) = "5" Then
                dgvLedger.Columns(colDeposit.Index).HeaderText = "Debit"
                dgvLedger.Columns(colWithdrawal.Index).HeaderText = "Credit"
                dgvLedger.Rows.Add(count, _
                                   SQL.SQLDR("RefType").ToString, _
                                   SQL.SQLDR("ReftransID").ToString, _
                                   SQL.SQLDR("TransNo").ToString, _
                                   CDate(SQL.SQLDR("AppDate").ToString).ToShortDateString, _
                                   CDec(SQL.SQLDR("Debit").ToString).ToString("N2"), _
                                   CDec(SQL.SQLDR("Credit").ToString).ToString("N2"), _
                                   CDec(SQL.SQLDR("Balance").ToString).ToString("N2"), _
                                   SQL.SQLDR("Remarks").ToString)
            Else
                dgvLedger.Columns(colDeposit.Index).HeaderText = "Debit"
                dgvLedger.Columns(colWithdrawal.Index).HeaderText = "Credit"
                dgvLedger.Rows.Add(count, _
                                   SQL.SQLDR("RefType").ToString, _
                                   SQL.SQLDR("ReftransID").ToString, _
                                   SQL.SQLDR("TransNo").ToString, _
                                   CDate(SQL.SQLDR("AppDate").ToString).ToShortDateString, _
                                   CDec(SQL.SQLDR("Debit").ToString).ToString("N2"), _
                                   CDec(SQL.SQLDR("Credit").ToString).ToString("N2"), _
                                   CDec(SQL.SQLDR("Balance").ToString).ToString("N2"), _
                                   SQL.SQLDR("Remarks").ToString)
            End If

            count += 1
        End While
    End Sub

    Private Sub LoadBalance()
        Dim selectSQL As String = " SELECT CASE WHEN SUM(Debit - Credit) > 0 THEN ISNULL(SUM(Debit - Credit),0) ELSE ISNULL(SUM(Credit - Debit),0) END AS Balance FROM view_Ledger WHERE AccntCode = '" & strAccntCode & "' AND VCECode = '" & strVCECode & "' "
        SQL.ReadQuery(selectSQL)
        While SQL.SQLDR.Read
            lblBalance.Text = CDec(SQL.SQLDR("Balance").ToString).ToString("N2")
        End While
    End Sub

    Private Sub dgvLedger_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLedger.CellContentClick

    End Sub

    Private Sub dgvLedger_CellMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvLedger.CellMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If e.ColumnIndex > -1 And e.RowIndex > -1 Then
                dgvLedger.CurrentCell = dgvLedger.Rows(e.RowIndex).Cells(e.ColumnIndex)
                cmsMenu.Show(New Point(MousePosition.X, MousePosition.Y))
            End If
        End If
    End Sub

    Private Sub ViewToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ViewToolStripMenuItem.Click
        Dim strRefType As String = dgvLedger.SelectedRows(0).Cells(colRefType.Index).Value
        Dim strRefTransID As String = dgvLedger.SelectedRows(0).Cells(colRefID.Index).Value
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
            Case "APV"
                Dim f As New frmAPV
                f.ShowDialog(strRefTransID)
            Case "SJ"
                Dim f As New frmSJ
                f.ShowDialog(strRefTransID)
            Case "PJ"
                Dim f As New frmPJ
                f.ShowDialog(strRefTransID)
            Case "GI"
                Dim f As New frmGI
                f.ShowDialog(strRefTransID)
            Case "MRIS"
                Dim f As New frmMRISATD
                f.ShowDialog(strRefTransID)
        End Select
    End Sub

    Private Sub lblBalance_Click(sender As System.Object, e As System.EventArgs) Handles lblBalance.Click

    End Sub

    Private Sub PrintToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        'Dim f As New frmReport_Display
        'f.ShowDialog("Verifier", strVCECode, strAccntCode)
        'f.Dispose()


        Dim attachfilepath As String
        Dim cryRpt As New ReportDocument
        Dim Report_Path As String
        Dim App_Path As String = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
        Report_Path = App_Path & "\CR_Reports\" & database
        Report_Path = Report_Path & "\Verifier.rpt"
        cryRpt.Load(Report_Path)
        cryRpt.SetDatabaseLogon(DBUser, DBPassword)
        cryRpt.SetParameterValue("VCECode", strVCECode)
        cryRpt.SetParameterValue("AccntCode", strAccntCode)
        CrystalReportViewer1.Refresh()


        Dim App_Path2 As String = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
        Dim CrExportOptions As ExportOptions
        Dim CrDiskFileDestinationOptions As New  _
        DiskFileDestinationOptions()
        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()
        CrDiskFileDestinationOptions.DiskFileName = App_Path2 & "\Export\Verifier" & strVCECode & strAccntCode & ".pdf"
        attachfilepath = App_Path2 & "\Export\Verifier" & strVCECode & strAccntCode & ".pdf"
        CrExportOptions = cryRpt.ExportOptions
        With CrExportOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
            .DestinationOptions = CrDiskFileDestinationOptions
            .FormatOptions = CrFormatTypeOptions
        End With
        cryRpt.Export()

        System.Diagnostics.Process.Start(attachfilepath)
    End Sub
End Class