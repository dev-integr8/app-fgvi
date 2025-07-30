Imports System.ComponentModel
Imports System.IO
Imports Microsoft.Office.Interop

Public Class frmPDC
    Dim ModuleID As String = "PDC"
    Dim disableEvent As Boolean = False
    Dim TransID As String
    Dim PDCNo As String
    Dim ColumnPK As String = "Batch_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblPDC"
    Dim TransAuto As Boolean
    Dim FileName As String
    Dim CheckCount As Integer

    Public Overloads Function ShowDialog(ByVal DocNumber As String) As Boolean
        TransID = DocNumber
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmItem_Pricelist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Text = "(" & database & ") - PDC Warehousing"
            TransAuto = GetTransSetup(ModuleID)

            If TransID <> "" Then
                If Not AllowAccess("PDC_ADD") Then
                    msgRestricted()
                    tsbSearch.Enabled = True
                    tsbNew.Enabled = True
                    tsbEdit.Enabled = False
                    tsbSave.Enabled = False
                    tsbCancel.Enabled = False
                    tsbClose.Enabled = False
                    tsbPrevious.Enabled = False
                    tsbTools.Enabled = False
                    tsbDownload.Enabled = False
                    tsbUpload.Enabled = False
                    tsbNext.Enabled = False
                    tsbExit.Enabled = True
                    EnableControl(False)
                Else
                    LoadPDC(TransID)
                End If
            Else
                tsbNew.Enabled = True
                tsbEdit.Enabled = False
                tsbSave.Enabled = False
                tsbCancel.Enabled = False
                tsbClose.Enabled = False
                tsbPrevious.Enabled = False
                tsbTools.Enabled = False
                tsbDownload.Enabled = False
                tsbUpload.Enabled = False
                tsbNext.Enabled = False
                tsbExit.Enabled = True
                EnableControl(False)
            End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub EnableControl(Value As Boolean)
        txtVCEName.Enabled = Value
        cbBank.Enabled = Value
        btnSearchVCE.Enabled = Value
        txtRemarks.Enabled = Value
        dgvList.AllowUserToAddRows = Value
        dgvList.AllowUserToDeleteRows = Value
        If Value Then
            dgvList.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvList.EditMode = DataGridViewEditMode.EditProgrammatically
        End If

        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Private Sub tsbNew_Click(sender As Object, e As EventArgs) Handles tsbNew.Click
        If Not AllowAccess("PDC_ADD") Then
            msgRestricted()
        Else
            ' CLEAR TRANSACTION RECORDS
            LoadBank()
            ClearText()
            TransID = ""
            PDCNo = ""
            ' SET TOOLSTRIP BUTTONS
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbSave.Enabled = True
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbTools.Enabled = True
            tsbPrint.Enabled = False
            tsbDownload.Enabled = True
            tsbUpload.Enabled = True
            tsbExit.Enabled = False
            EnableControl(True)

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
        End If
    End Sub

    Private Sub ClearText()
        txtVCECode.Clear()
        txtVCEName.Clear()
        txtRemarks.Clear()
        txtTransNum.Clear()
        dgvList.Rows.Clear()
        cbBank.Text = ""
    End Sub

    Private Sub tsbClose_Click(sender As Object, e As EventArgs) Handles tsbClose.Click
        ' Toolstrip Buttons
        If PDCNo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbPrint.Enabled = False
            tsbNext.Enabled = False
        Else
            LoadPDC(PDCNo)
            tsbEdit.Enabled = True
            tsbCancel.Enabled = True
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbPrint.Enabled = True
        End If
        tsbSearch.Enabled = True
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbTools.Enabled = False
        tsbDownload.Enabled = False
        tsbUpload.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
    End Sub

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub


    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        CountCheck()

        If txtVCECode.Text = "" Then
            Msg("Please input customer!", MsgBoxStyle.Exclamation)
        ElseIf CheckCount = 0 Then
            Msg("Please input atleast one check details!", MsgBoxStyle.Exclamation)
        ElseIf TransID = "" Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                TransID = GenerateTransID(ColumnID, DBTable)
                PDCNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                txtTransNum.Text = PDCNo
                SavePDC()
                Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                PDCNo = txtTransNum.Text
                LoadPDC(TransID)
            End If
        Else
            If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                UpdatePDC()
                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                PDCNo = txtTransNum.Text
                LoadPDC(TransID)
            End If
        End If
    End Sub

    Private Sub SavePDC()
        Dim SQL As New SQLControl
        SQL.BeginTransaction()
        Try
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " &
                                " tblPDC  (TransID, Batch_No, VCECode, Bank, Remarks, CheckCount,   " &
                                "          WhoCreated, Status, DateCreated) " &
                                " VALUES (@TransID, @Batch_No, @VCECode, @Bank, @Remarks, @CheckCount, " &
                                "         @WhoCreated, @Status, GETDATE()) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@Batch_No", PDCNo)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@Bank", cbBank.Text)
            SQL.AddParam("@CheckCount", CheckCount)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)
            SaveDetails(SQL)
            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
            TransID = ""
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", ColumnPK, txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub UpdatePDC()
        Dim SQL As New SQLControl
        SQL.BeginTransaction()
        Try
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " UPDATE    tblPDC " &
                        " SET       Batch_No = @Batch_No, VCECode = @VCECode, Bank = @Bank, Remarks = @Remarks, CheckCount = @CheckCount,  " &
                        "           WhoModified = @WhoModified, Status = @Status, DateModified = GETDATE() " &
                        " WHERE     TransID = @TransID  "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@Batch_No", PDCNo)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@Bank", cbBank.Text)
            SQL.AddParam("@CheckCount", CheckCount)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(insertSQL)
            SaveDetails(SQL)
            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", ColumnPK, txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub SaveDetails(SQL As SQLControl)
        Dim query As String
        query = " DELETE FROM tblPDC_Details WHERE TransID = @TransID "
        SQL.FlushParams()
        SQL.AddParam("@TransID", TransID)
        SQL.ExecNonQuery(query)

        For Each row As DataGridViewRow In dgvList.Rows
            If row.Cells(dgcCheckNo.Index).Value <> "" AndAlso IsDate(row.Cells(dgcDate.Index).Value) Then
                query = " INSERT INTO " &
               " tblPDC_Details(TransID, DatePDC, CheckNumber, Amount) " &
               " VALUES(@TransID, @DatePDC, @CheckNumber, @Amount)"
                SQL.FlushParams()
                SQL.AddParam("@TransID", TransID)
                SQL.AddParam("@DatePDC", row.Cells(dgcDate.Index).Value)
                SQL.AddParam("@CheckNumber", row.Cells(dgcCheckNo.Index).Value)
                SQL.AddParam("@Amount", CDec(row.Cells(dgcAmount.Index).Value))
                SQL.ExecNonQuery(query)
            End If
        Next
    End Sub


    Dim ReadonlyColor As Color = Color.FromArgb(234, 234, 234)
    Dim DefaultColor As Color = Color.White

    Private Sub tsbSearch_Click(sender As Object, e As EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("PDC_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("PDC")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadPDC(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub LoadPDC(ByVal ID As String)
        Dim query As String
        query = " SELECT     tblPDC.TransID, Batch_No, VCECode, Bank, Remarks, DateCreated, Status  " &
                " FROM       tblPDC  " &
                " WHERE      tblPDC.TransId = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            TransID = SQL.SQLDR("TransID").ToString
            PDCNo = SQL.SQLDR("Batch_No").ToString
            txtTransNum.Text = PDCNo
            txtStatus.Text = SQL.SQLDR("Status").ToString
            dtpDocDate.Text = SQL.SQLDR("DateCreated")
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            cbBank.Text = SQL.SQLDR("Bank").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtVCEName.Text = GetVCECode(txtVCECode.Text)
            LoadDetails(TransID)

            ' TOOLSTRIP BUTTONS
            If txtStatus.Text = "Cancelled" Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            Else
                tsbEdit.Enabled = True
                tsbCancel.Enabled = True
            End If
            tsbClose.Enabled = False
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbTools.Enabled = False
            tsbPrint.Enabled = True
            tsbSave.Enabled = False
            tsbNew.Enabled = True
            tsbSearch.Enabled = True
            tsbDownload.Enabled = False
            tsbUpload.Enabled = False
            tsbExit.Enabled = True

            EnableControl(False)
        Else
            ClearText()
        End If

    End Sub

    Private Sub LoadDetails(ID As Integer)
        Dim query As String
        dgvList.Rows.Clear()

        query = " SELECT        DatePDC, CheckNumber, Amount " &
                 " FROM         tblPDC_Details " &
                 " WHERE        TransID = @TransID " &
                 " ORDER BY     DatePDC "
        SQL.FlushParams()
        SQL.AddParam("@TransID", ID)
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvList.Rows.Add(CDate(SQL.SQLDR("DatePDC")).ToString("MM/dd/yyyy"), SQL.SQLDR("CheckNumber").ToString, CDec(SQL.SQLDR("Amount")).ToString("N2"))
        End While
        lblCounter.Text = "Record Count : " & CheckCount
    End Sub


    Private Sub frmItem_Pricelist_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub tsbNext_Click(sender As Object, e As EventArgs) Handles tsbNext.Click
        If PDCNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblPDC  WHERE Batch_No > '" & PDCNo & "' ORDER BY Batch_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadPDC(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As Object, e As EventArgs) Handles tsbPrevious.Click
        If PDCNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblPDC  WHERE Batch_No < '" & PDCNo & "' ORDER BY Batch_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadPDC(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbEdit_Click(sender As Object, e As EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("PDC_EDIT") Then
            msgRestricted()
        Else
            LoadBank()
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
            tsbTools.Enabled = False
            tsbPrint.Enabled = False
        End If
    End Sub

    Private Sub tsbDownload_Click(sender As Object, e As EventArgs) Handles tsbDownload.Click
        tsbLabel.Visible = True
        tsbLabel.Text = "Downloading Template..."
        GroupBox2.Enabled = False
        tsbBar.Visible = True
        bgwDownload.RunWorkerAsync()
    End Sub

    Private Sub bgwDownload_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDownload.DoWork
        Dim templateName As String = "PDC.xlsx"
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim ctr As Integer = 0
        Dim SQL As New SQLControl
        xlApp = New Excel.Application
        Dim fileSuffix As String = (CDate(Date.Now).ToString("MMddYYYhhmmss"))
        SetPGBmax(dgvList.Rows.Count * (dgvList.Columns.Count - 1))
        If dgvList.Rows.Count > 0 Then
            App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName & "\Templates"
            If My.Computer.FileSystem.FileExists(App_Path + "\" & templateName) Then
                xlWorkBook = xlApp.Workbooks.Open(App_Path + "\" & templateName)
                xlWorkSheet = xlWorkBook.Worksheets("Template")
                xlWorkSheet.Unprotect("@dm1nEvo")

                xlWorkSheet.Protect("@dm1nEvo")
                xlWorkBook.SaveAs(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & Replace(templateName, ".xlsx", "") & fileSuffix & ".xlsx")
                xlWorkBook.Close(Type.Missing, Type.Missing, Type.Missing)
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
        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & Replace(templateName, ".xlsx", "") & fileSuffix & ".xlsx") Then
            Dim xls As Excel.Application
            Dim workbook As Excel.Workbook
            xls = New Excel.Application
            xls.Visible = True
            workbook = xls.Workbooks.Open(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & Replace(templateName, ".xlsx", "") & fileSuffix & ".xlsx")
        End If
    End Sub

    Private Delegate Sub SetPGBmaxInvoker(ByVal Value As String)
    Private Sub SetPGBmax(ByVal Value As String)
        If Me.InvokeRequired Then
            Me.Invoke(New SetPGBmaxInvoker(AddressOf SetPGBmax), Value)
        Else
            tsbBar.Maximum = Value
            tsbBar.Value = 0
        End If
    End Sub

    Private Sub bgwDownload_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgwDownload.ProgressChanged
        tsbBar.Value = e.ProgressPercentage
    End Sub

    Private Sub bgwDownload_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwDownload.RunWorkerCompleted
        tsbBar.Visible = False
        tsbLabel.Visible = False
        tsbBar.Value = 0
        GroupBox2.Enabled = True
    End Sub

    Private Sub tsbUpload_Click(sender As Object, e As EventArgs) Handles tsbUpload.Click
        Dim OpenFileDialog As New OpenFileDialog
        tsbLabel.Visible = True
        tsbLabel.Text = "Uploading Data..."
        tsbSave.Enabled = False
        GroupBox2.Enabled = False
        tsbBar.Visible = True
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            FileName = OpenFileDialog.FileName
            dgvList.Rows.Clear()
            bgwUpload.RunWorkerAsync()
        End If

    End Sub

    Private Sub bgwUpload_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwUpload.DoWork
        Dim firstRow As Integer
        Dim ctr As Integer = 0
        Dim str As String
        Dim objExcel As New Microsoft.Office.Interop.Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim range As Excel.Range

        xlWorkBook = objExcel.Workbooks.Open(FileName)
        xlWorkSheet = xlWorkBook.Worksheets(1)
        range = xlWorkSheet.UsedRange
        str = "a"
        firstRow = 2
        disableEvent = True

        SetPGBmax(range.Rows.Count)
        Do While str <> ""
            Dim DatePDC As Date
            Dim CheckNumber As String
            Dim Amount As Decimal = 0
            If Not IsDate(RTrim(objExcel.Range("a" & CStr(firstRow)).Value)) Then
                DatePDC = CDate("01-01-1900")
            Else
                DatePDC = RTrim(objExcel.Range("a" & CStr(firstRow)).Value)
            End If

            CheckNumber = RTrim(objExcel.Range("b" & CStr(firstRow)).Value)
            If Not IsNumeric(RTrim(objExcel.Range("c" & CStr(firstRow)).Value)) Then
                Amount = 0
            Else
                Amount = RTrim(objExcel.Range("c" & CStr(firstRow)).Value)
            End If

            If CheckNumber <> "" Then
                AddRow()
                AddValue(DatePDC, ctr, 0, DbType.Date)
                AddValue(CheckNumber, ctr, 1, DbType.String)
                AddValue(Amount, ctr, 2, DbType.Decimal)
            End If


            firstRow += 1
            ctr += 1
            str = RTrim(objExcel.Range("a" & CStr(ctr)).Value)
            bgwUpload.ReportProgress(ctr)
        Loop
        disableEvent = False
        objExcel.Workbooks.Close()
        ValidateDGV()
    End Sub
    Private Delegate Sub AddValueInvoker(ByVal Value As String, ByVal row As Integer, ByVal col As Integer, DataType As DbType)
    Private Sub AddValue(ByVal Value As String, ByVal row As Integer, ByVal col As Integer, DataType As DbType)
        If Me.InvokeRequired Then
            Me.Invoke(New AddValueInvoker(AddressOf AddValue), Value, row, col, DataType)
        Else
            Select Case DataType
                Case DbType.Decimal
                    dgvList.Item(col, row).Value = CDec(Value).ToString("N2")
                Case DbType.Date
                    dgvList.Item(col, row).Value = CDate(Value).ToString("MM/dd/yyyy")
                Case Else
                    dgvList.Item(col, row).Value = Value.ToString

            End Select
        End If
    End Sub

    Private Sub AddRow()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf AddRow))
        Else
            dgvList.Rows.Add("")
        End If
    End Sub
    Private Function ValidateDGV() As Boolean
        If dgvList.Rows.Count = 0 Then
            MsgBox("There are no item on the list!", MsgBoxStyle.Exclamation)
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub bgwUpload_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgwUpload.ProgressChanged
        tsbBar.Value = e.ProgressPercentage
        CheckCount = e.ProgressPercentage
        lblCounter.Text = "Record Count : " & CheckCount
    End Sub

    Private Sub bgwUpload_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwUpload.RunWorkerCompleted
        tsbBar.Visible = False
        tsbBar.Value = 0
        tsbSave.Enabled = True
        tsbLabel.Visible = False
        tsbSave.Enabled = True
        GroupBox2.Enabled = True
        lblCounter.Text = "Record Count : " & CheckCount
    End Sub

    Private Sub dgvList_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs)
        If e.ColumnIndex = dgcAmount.Index Then
            Dim dc As Decimal
            If e.FormattedValue.ToString <> String.Empty AndAlso Not Decimal.TryParse(e.FormattedValue.ToString, dc) Then
                Msg("Invalid Amount!", vbExclamation)
                e.Cancel = True
            End If
        ElseIf e.ColumnIndex = dgcDate.Index Then
            Dim dt As Date
            If e.FormattedValue.ToString <> String.Empty AndAlso Not Date.TryParse(e.FormattedValue.ToString, dt) Then
                Msg("Invalid Amount!", vbExclamation)
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub btnSearchVCE_Click(sender As Object, e As EventArgs) Handles btnSearchVCE.Click
        Dim f As New frmVCE_Search
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        txtVCEName.Text = f.VCEName
        f.Dispose()
    End Sub

    Private Sub btnVCE_Click(sender As Object, e As EventArgs) Handles btnVCE.Click
        frmVCE_Master.ShowDialog(txtVCECode.Text)
    End Sub

    Private Sub BatchEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatchEntryToolStripMenuItem.Click
        Dim f As New frmPDC_Batch
        f.ShowDialog()
        If f.Saved Then
            GenerateChecks(f.PaymentFrequecy, f.CheckNumber, f.StartDate, f.CheckAmount, f.NumberOfChecks)
        End If
    End Sub

    Private Sub GenerateChecks(Frequency As String, FirstCheckNumber As String, FirstCheckDate As Date, CheckAmount As Decimal, NoOfChecks As Integer)
        dgvList.Rows.Clear()
        Dim datePDC As Date = FirstCheckDate
        Dim amount As Decimal = CheckAmount
        Dim CheckNum As Integer
        Dim CheckString As String = FirstCheckNumber
        Dim CheckDigits As Integer = FirstCheckNumber.Length

        For i As Integer = 1 To NoOfChecks
            dgvList.Rows.Add(datePDC.ToString("MM/dd/yyyy"), CheckString, amount.ToString("N2"))
            CheckNum = (CInt(CheckString) + 1).ToString
            CheckString = ""
            For j As Integer = 1 To CheckDigits
                CheckString = CheckString & "0"
            Next
            CheckString = Strings.Right((CheckString & CheckNum.ToString), CheckDigits)
            Select Case Frequency
                Case "Daily"
                    datePDC = DateAdd(DateInterval.Day, 1, datePDC)
                Case "Weekly"
                    datePDC = DateAdd(DateInterval.Weekday, 1, datePDC)
                Case "Monthly"
                    datePDC = DateAdd(DateInterval.Month, 1, datePDC)
                Case "Quarterly"
                    datePDC = DateAdd(DateInterval.Quarter, 1, datePDC)
                Case "Yearly"
                    datePDC = DateAdd(DateInterval.Year, 1, datePDC)
            End Select
        Next
        CheckCount = NoOfChecks
        lblCounter.Text = "Record Count : " & NoOfChecks

    End Sub

    Private Sub tsbPrint_Click(sender As Object, e As EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("PDC", TransID)
        f.Dispose()
    End Sub

    Private Sub CountCheck()
        Dim i As Integer = 0
        For Each row As DataGridViewRow In dgvList.Rows
            If row.Cells(dgcCheckNo.Index).Value <> "" AndAlso IsDate(row.Cells(dgcDate.Index).Value) Then
                i += 1
            End If
        Next
        CheckCount = i
    End Sub

    Private Sub dgvList_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellEndEdit
        If e.ColumnIndex <> -1 AndAlso e.RowIndex <> -1 Then
            If e.ColumnIndex = dgcAmount.Index Then
                If IsNumeric(dgvList.Item(e.ColumnIndex, e.RowIndex).Value) Then
                    dgvList.Item(e.ColumnIndex, e.RowIndex).Value = CDec(dgvList.Item(e.ColumnIndex, e.RowIndex).Value).ToString("N2")
                End If
            ElseIf e.ColumnIndex = dgcDate.Index Then
                If IsDate(dgvList.Item(e.ColumnIndex, e.RowIndex).Value) Then
                    dgvList.Item(e.ColumnIndex, e.RowIndex).Value = CDate(dgvList.Item(e.ColumnIndex, e.RowIndex).Value).ToString("MM/dd/yyyy")
                End If
            End If


        End If
        CountCheck()

    End Sub

    Private Sub tsbCancel_Click(sender As Object, e As EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("PDC_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim updateSQL As String
                        updateSQL = " UPDATE  tblPDC SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        PDCNo = txtTransNum.Text
                        SQL.AddParam("@TransID", TransID)
                        SQL.ExecNonQuery(updateSQL)

                        Msg("Record cancelled successfully", MsgBoxStyle.Information)

                        tsbSearch.Enabled = True
                        tsbNew.Enabled = True
                        tsbEdit.Enabled = False
                        tsbSave.Enabled = False
                        tsbCancel.Enabled = False
                        tsbClose.Enabled = False
                        tsbPrevious.Enabled = False
                        tsbNext.Enabled = False
                        tsbExit.Enabled = True
                        tsbPrint.Enabled = True
                        EnableControl(False)

                        PDCNo = txtTransNum.Text
                        LoadPDC(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", ColumnPK, PDCNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub LoadBank()
        cbBank.Items.Clear()
        Dim query As String
        query = " SELECT DISTINCT Bank FROM tblPDC ORDER BY Bank "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            cbBank.Items.Add(SQL.SQLDR("Bank").to)
        End While
    End Sub
End Class