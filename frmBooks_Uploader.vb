Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmBooks_Uploader
    Dim Book As String
    Dim Book_ID As String
    Dim JETransID As String
    Dim ModuleID As String = "UB"
    Dim ColumnPK As String = "UploadID"
    Dim DBTable As String = "tblJE_Header"


    ' UPLOAD VARIABLES
    Dim Valid As Boolean = True
    Dim Cancelled As Boolean = False
    Dim InvalidTemplate As Boolean = False
    Dim path As String
    Dim templateName As String = "TEMPLATE_BOOK"
    Public excelPW As String = "@dm1nEvo"


    Private Sub cbBook_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbBook.SelectedIndexChanged
        If cbBook.SelectedItem = "Cash Disbursements Book" Then
            Book = "Cash Disbursements"
        ElseIf cbBook.SelectedItem = "Cash Receipts Book" Then
            Book = "Cash Receipts"
        ElseIf cbBook.SelectedItem = "General Journal" Then
            Book = "General Journal"
        ElseIf cbBook.SelectedItem = "Sales Book" Then
            Book = "Sales"
        ElseIf cbBook.SelectedItem = "Purchase Book" Then
            Book = "Purchase Journal"
        ElseIf cbBook.SelectedItem = "Accounts Payable Book" Then
            Book = "Accounts Payable"
        End If
    End Sub


    Private Sub bgwUpload_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUpload.DoWork
        Dim startIndex As Integer
        Dim excelRangeCount As Integer
        Dim rowCount As Integer
        Dim ObjectText As String = ""
        Dim ObjectText1 As String = ""
        Dim ObjectText2 As String = ""
        Dim intAmount As Decimal = 0
        Dim prinAmount As Decimal = 0
        Dim AccountID As Integer = 0
        Dim VCECode As String = ""

        Dim total As Decimal
        Dim GrandTotal As Decimal = 0

        ' For Addditional Interest field when autobreakdown is checked.
        Dim addlCol As Integer = 0
        ' OPENING EXCEL FILE VARIABLES
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim Obj As Object
        Dim Obj1 As Object
        Dim Obj2 As Object
        Dim sheetName As String
        Dim range As Excel.Range

        ' OPEN EXCEL FILE
        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Open(path)
        sheetName = xlWorkBook.Worksheets(1).Name.ToString
        xlWorkSheet = xlWorkBook.Worksheets(sheetName)
        range = xlWorkSheet.UsedRange

        Valid = True
        InvalidTemplate = False
        Dim summary As Boolean = False
        Dim rowSumCount As Integer = 0
        Dim report As String = ""

        startIndex = 4

        SetPGBmax(range.Rows.Count)  ' Set Progress bar max value.
        excelRangeCount = range.Rows.Count
        Dim maxCol As Integer = range.Columns.Count - 1
        ' Loop through rows of excel.
        For i As Integer = startIndex To range.Rows.Count
            If bgwUpload.CancellationPending Then
                e.Cancel = True
                Exit For
            End If

            ' Reset AddCol.
            addlCol = 0

            ' Loop through columns of excel.
            For j As Integer = 0 To maxCol
                Obj = CType(range.Cells(i, j + 1), Excel.Range)

                ' Exit loop if first row and first column is not equal to template name.
                If i = 1 AndAlso j = 0 Then
                    If (Obj.value Is Nothing OrElse Obj.value.ToString <> templateName) Then

                        Dim test As String = Obj.value
                        bgwUpload.CancelAsync()
                        InvalidTemplate = True

                        If bgwUpload.CancellationPending Then
                            e.Cancel = True
                        End If
                    End If
                    Exit For


                    'ElseIf i = 2 Then
                    '    ' Add column ID, exit loop on the first blank cell of column name
                    '    If Not Obj.value Is Nothing AndAlso Obj.value.ToString <> "" Then

                    '        AddColumn(Obj.value)
                    '        If j > 2 Then
                    '            dgvEntry.Columns(j + addlCol).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


                    '        End If
                    '    Else
                    '        Exit For
                    '    End If


                    'ElseIf i = 3 Then
                    '    ' Update column header, exit loop after updating the last column of datagridview
                    '    If dgvEntry.Columns.Count > j + addlCol Then
                    '        UpdateColumn(j + addlCol, Obj.value)
                    '        If j > 2 Then

                    '        End If

                    '    Else
                    '        Exit For
                    '    End If


                ElseIf i > 1 Then
                    ' Update cell value, exit loop after updating the last column of a row
                    If dgvEntry.Columns.Count > j + addlCol Then

                        If j = 0 Then
                            ' On the first column of the current row, exit loop when Code, Ref and Name is blank, should have atleast one record. 
                            Obj1 = CType(range.Cells(i, j + 2), Excel.Range)
                            Obj2 = CType(range.Cells(i, j + 3), Excel.Range)
                            If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                            If IsNothing(Obj1.value) Then ObjectText1 = "" Else ObjectText1 = Obj1.value.ToString
                            If IsNothing(Obj2.value) Then ObjectText2 = "" Else ObjectText2 = Obj2.value.ToString
                            If ObjectText = "" AndAlso ObjectText1 = "" AndAlso ObjectText2 = "" Then
                                Exit For
                            End If

                            ' Add Row to Datagridview on the first column loop.
                            AddRow()
                            rowCount += 1
                            SetCounterValue(rowCount)

                            AddValue(ObjectText, rowCount - 1, j)
                            ' Check if first column is date
                            If Not IsDate(ObjectText) Then
                                ' if not date, change color.
                                ChangeCellColor(rowCount - 1, j)
                                Valid = False
                            End If
                        ElseIf j = 1 Then
                            ' TransID
                            If dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = 2 Then
                            ' RefType
                            If dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = 3 Then
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                ' if not check AccountCode on excel template
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = 4 Then

                            If Not validateAccountCode(ObjectText) Then
                                ' if not exist, change color.
                                ChangeCellColor(rowCount - 1, j)
                                Valid = False
                            Else
                                ' if existing  AccountCode
                                dgvEntry.Item(j, rowCount - 1).Value = GetAccntTitle(ObjectText)
                            End If
                        ElseIf j = 5 Then
                            ' Debit
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = 6 Then
                            ' Credit
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = 7 Then
                            ' Check if has valid VCEcode
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                ' if not check VCEref on excel template
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = 8 Then
                            ' Check if has valid VCEcode
                            If ObjectText <> "" Then
                                If Not validateVCE(ObjectText) Then
                                    ' if not exist, change color.
                                    ChangeCellColor(rowCount - 1, j - 1)
                                    Valid = False
                                Else
                                    ' if existing  AccountCode
                                    dgvEntry.Item(j, rowCount - 1).Value = GetVCEName(ObjectText)
                                End If
                            End If

                        ElseIf j = 9 Then
                            ' Particulars
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If

                        ElseIf j = 10 Then
                            ' RefNo
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                ' if not check VCEref on excel template
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = 11 Then
                            ' RefNo
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                ' if not check VCEref on excel template
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = 12 Then
                            ' Currency
                            If Not validateCurrency(Obj.value.ToString) Then
                                ' if not exist, change color.
                                ChangeCellColor(rowCount - 1, j)
                                Valid = False
                            Else
                                ' if existing  AccountCode
                                dgvEntry.Item(j, rowCount - 1).Value = Obj.value.ToString
                            End If
                            'If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                            '    ' if not check VCEref on excel template
                            '    If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                            '    AddValue(ObjectText, rowCount - 1, j)
                            'End If
                        ElseIf j = 13 Then
                            ' Exchange Rate
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                ' if not check VCEref on excel template
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If

                        End If
                    Else
                        Exit For
                    End If
                End If

            Next

            ' Reset AddCol.
            addlCol = 0
            bgwUpload.ReportProgress(i)
        Next


        NAR(Obj)
        NAR(range)
        NAR(xlWorkSheet)
        xlWorkBook.Close(False)
        NAR(xlWorkBook)
        xlApp.Quit()
        NAR(xlApp)
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Private Sub NAR(ByRef o As Object)
        Try
            While (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0)
            End While
        Catch
        Finally
            o = Nothing
        End Try
    End Sub

    Public Function validateVCE(ByVal VCEName As String) As Boolean
        Try
            Dim query As String
            query = " SELECT    * " &
                    " FROM      viewVCE_Master " &
                    " WHERE     VCECode = @VCECode "
            SQL.FlushParams()
            SQL.AddParam("@VCECode", VCEName)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SQL.FlushParams()
        End Try

    End Function

    Public Function validateAccountCode(ByVal AccntCode As String) As Boolean
        Try
            Dim query As String
            query = " SELECT    * " &
                    " FROM      tblCOA_Master " &
                    " WHERE     AccountCode = @AccountCode "
            SQL.FlushParams()
            SQL.AddParam("@AccountCode", AccntCode)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SQL.FlushParams()
        End Try

    End Function
    Public Function validateCurrency(ByVal Currency As String) As Boolean
        Try
            Dim query As String
            query = " SELECT    * " &
                    " FROM      tblCurrency " &
                    " WHERE     Code = @PHP "
            SQL.FlushParams()
            SQL.AddParam("@PHP", Currency)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SQL.FlushParams()
        End Try

    End Function
    Public Function getVCECode(ByVal VCERef As String, ByVal VCEName As String) As String
        Try
            Dim query As String
            query = " SELECT    VCECode " &
                    " FROM      viewVCE_Master " &
                    " WHERE     (VCERef = @VCERef AND VCERef <> '') OR (VCEName = @VCEName AND VCEName <> '') "
            SQL.FlushParams()
            SQL.AddParam("@VCERef", VCERef)
            SQL.AddParam("@VCEName", VCEName)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Return SQL.SQLDR("VCECode").ToString
            Else
                Return ""
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SQL.FlushParams()
        End Try
    End Function


    Private Sub AddRow()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf AddRow))
        Else
            dgvEntry.Rows.Add("")
        End If
    End Sub

    Private Delegate Sub AddValueInvoker(ByVal Value As String, ByVal row As Integer, ByVal col As Integer)
    Private Sub AddValue(ByVal Value As String, ByVal row As Integer, ByVal col As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New AddValueInvoker(AddressOf AddValue), Value, row, col)
        Else
            dgvEntry.Item(col, row).Value = Value
        End If
    End Sub

    Private Delegate Sub SetCounterValueInvoker(ByVal Value As String)
    Private Sub SetCounterValue(ByVal Value As String)
        If Me.InvokeRequired Then
            Me.Invoke(New SetCounterValueInvoker(AddressOf SetCounterValue), Value)
        Else
            lblCount.Text = "Record Count : " & Value
        End If
    End Sub


    Private Delegate Sub ChangeCellColorInvoker(ByVal row As Integer, ByVal col As Integer)
    Private Sub ChangeCellColor(ByVal row As Integer, ByVal col As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New ChangeCellColorInvoker(AddressOf ChangeCellColor), row, col)
        Else
            dgvEntry.Rows(row).Cells(col).Style.BackColor = Color.Yellow
        End If
    End Sub

    Private Delegate Sub DefaultCellColorInvoker(ByVal row As Integer, ByVal col As Integer)
    Private Sub DefaultCellColor(ByVal row As Integer, ByVal col As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New DefaultCellColorInvoker(AddressOf DefaultCellColor), row, col)
        Else
            dgvEntry.Rows(row).Cells(col).Style.BackColor = Color.White
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

    Private Delegate Sub AddColumnInvoker(ByVal ColName As String)
    Private Sub AddColumn(ByVal ColName As String)
        If Me.InvokeRequired Then
            Me.Invoke(New AddColumnInvoker(AddressOf AddColumn), ColName)
        Else
            dgvEntry.Columns.Add(ColName, "")
            dgvEntry.Columns(dgvEntry.Columns.Count - 1).SortMode = DataGridViewColumnSortMode.NotSortable
        End If
    End Sub

    Private Delegate Sub UpdateColumnInvoker(ByVal ColID As Integer, ByVal ColText As String)
    Private Sub UpdateColumn(ByVal ColID As Integer, ByVal ColText As String)
        If Me.InvokeRequired Then
            Me.Invoke(New UpdateColumnInvoker(AddressOf UpdateColumn), ColID, ColText)
        Else
            For Each col As DataGridViewColumn In dgvEntry.Columns
                If col.Index = ColID Then
                    col.HeaderText = ColText
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub frmBooks_Uploader_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            tsbSearch.Enabled = True
            tsbNew.Enabled = True
            tsbSave.Enabled = False
            tsbCancel.Enabled = False
            tsbClose.Enabled = False
            tsbPrevious.Enabled = False
            tsbUpload.Enabled = False
            tsbDownload.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = True
            EnableControl(False)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try

    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        cbBook.Enabled = Value
        txtRemarks.Enabled = Value
    End Sub

    Private Sub bgwUpload_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwUpload.ProgressChanged
        pgbCounter.Value = e.ProgressPercentage
    End Sub

    Private Sub bgwUpload_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwUpload.RunWorkerCompleted
        pgbCounter.Visible = False
        lblCount.Text = "Record Count : " & dgvEntry.Rows.Count
        If InvalidTemplate Then
            MsgBox("Invalid Template, Please select a valid File!", MsgBoxStyle.Exclamation)
        Else
            'TotalDBCR()
            If Valid Then
                If dgvEntry.Rows.Count > 1 Then
                    MsgBox(dgvEntry.Rows.Count & " File Data Uploaded Successfully!", vbInformation, "JADE Message Alert")
                Else
                    MsgBox(dgvEntry.Rows.Count & " File Data Uploaded Successfully!", vbInformation, "JADE Message Alert")
                End If

            Else
                MsgBox("Some data are invalid !, Please Check highlighted cells.", MsgBoxStyle.Exclamation, "JADE Message Alert")
            End If
            If dgvEntry.Rows.Count > 1 Then
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).ReadOnly = True
            End If
        End If
        tsbUpload.Text = "Upload"
        TotalDBCR()
    End Sub


    Private Sub UpdateEntry(ByVal JEID As Integer, TotalAmount As Decimal)
        Dim updateSQL As String

        updateSQL = " UPDATE tblJE_Header " & _
                    " SET    TotalDBCR = TotalDBCR + @TotalDBCR  " & _
                    " WHERE  JE_No = @JE_No "
        SQL.FlushParams()
        SQL.AddParam("@JE_No", JEID)
        SQL.AddParam("@TotalDBCR", TotalAmount)
        SQL.ExecNonQuery(updateSQL)
    End Sub

    Public Function LoadJE(ByVal Ref_Type As String, Ref_TransID As String, Book As String, isUpload As Boolean) As String
        Dim query As String
        query = " SELECT JE_No FROM tblJE_Header WHERE RefType='" & Ref_Type & "' AND RefTransID ='" & Ref_TransID & "' AND Book ='" & Book & "'  AND isUpload ='" & isUpload & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read AndAlso Not IsDBNull(SQL.SQLDR("JE_No")) Then
            Return SQL.SQLDR("JE_No")
        Else
            Return 0
        End If
    End Function

    Private Function TransIDexist(ByVal TransID As String, ByVal Book As String, ByVal Type As String, ByVal IsUpload As Boolean) As Boolean
        Try
            Dim query As String
            query = " SELECT RefTransiD FROM tblJE_Header " & _
                " WHERE RefTransiD = @RefTransiD  AND Book = @Book AND Reftype = @Reftype " & _
                " AND IsUpload = @IsUpload AND Status <> 'Cancelled' "
            SQL.FlushParams()
            SQL.AddParam("@RefTransiD", TransID)
            SQL.AddParam("@Book", Book)
            SQL.AddParam("@Reftype", Type)
            SQL.AddParam("@IsUpload", IsUpload)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        Finally
            SQL.FlushParams()
        End Try
    End Function

    Private Sub SaveEntry(ByVal TransID As Integer, DateSI As Date, TotalAmount As Decimal, Type As String, Book As String, book_ID As String, Remarks As String, Currency As String, Exchange_Rate As String)
        Dim insertSQL As String

        JETransID = GenerateTransID("JE_No", "tblJE_Header")
        insertSQL = " INSERT INTO " & _
                    " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, WhoCreated, UploadID, isUpload, Currency, Exchange_Rate) " & _
                    " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @WhoCreated, @UploadID, @isUpload, @Currency, @Exchange_Rate)"
        SQL.FlushParams()
        SQL.AddParam("@JE_No", JETransID)
        SQL.AddParam("@AppDate", DateSI)
        SQL.AddParam("@RefType", Type)
        SQL.AddParam("@RefTransID", TransID)
        SQL.AddParam("@Book", Book)
        SQL.AddParam("@TotalDBCR", TotalAmount)
        SQL.AddParam("@Remarks", Remarks)
        SQL.AddParam("@BranchCode", BranchCode)
        SQL.AddParam("@BusinessCode", BusinessType)
        SQL.AddParam("@WhoCreated", UserID)
        SQL.AddParam("@UploadID", book_ID)
        SQL.AddParam("@isUpload", True)
        SQL.AddParam("@Currency", Currency)
        SQL.AddParam("@Exchange_Rate", Exchange_Rate)
        SQL.ExecNonQuery(insertSQL)

    End Sub

    Private Sub SaveEntryDetails(JE As Integer, AccntCode As String, VCE As String, Debit As Decimal, Credit As Decimal, Particulars As String, Optional RefNo As String = "", Optional VATType As String = "")
        Dim insertSQL As String
        insertSQL = " INSERT INTO " & _
                    " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, OtherRef, LineNumber, VATType) " & _
                    " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @OtherRef, @LineNumber, @VATType)"
        SQL.FlushParams()
        SQL.AddParam("@JE_No", JE)
        SQL.AddParam("@AccntCode", AccntCode)
        SQL.AddParam("@VCECode", VCE)
        SQL.AddParam("@Debit", Debit)
        SQL.AddParam("@Credit", Credit)
        SQL.AddParam("@Particulars", Particulars)
        SQL.AddParam("@OtherRef", RefNo)
        SQL.AddParam("@LineNumber", 0)
        SQL.AddParam("@VATType", VATType)
        SQL.ExecNonQuery(insertSQL)
    End Sub

    Private Sub bgwSave_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwSave.ProgressChanged
        pgbCounter.Value = e.ProgressPercentage
    End Sub

    Private Sub bgwSave_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwSave.RunWorkerCompleted
        pgbCounter.Visible = False
        MsgBox(dgvEntry.Rows.Count & " Records Saved Successfully!", vbInformation, "JADE Message Alert")
        dgvEntry.Rows.Clear()
        tsbSave.Enabled = True
        LoadBooks(txtUpload_ID.Text)
    End Sub

    Private Sub tsbNew_Click_1(sender As Object, e As EventArgs) Handles tsbNew.Click



        'If Not AllowAccess("UB_ADD") Then
        '    msgRestricted()
        'Else
        ClearText()

        ' Toolstrip Buttons
        tsbSearch.Enabled = False
        tsbNew.Enabled = False
        tsbSave.Enabled = True
        tsbCancel.Enabled = False
        tsbClose.Enabled = True
        tsbUpload.Enabled = True
        tsbDownload.Enabled = True
        tsbPrevious.Enabled = False
        tsbNext.Enabled = False
        tsbExit.Enabled = False
        EnableControl(True)

        txtUpload_ID.Text = GenerateTransID("UploadID", DBTable)
        ' End If
    End Sub

    Private Sub ClearText()
        txtRemarks.Text = ""
        txtStatus.Text = ""
        txtCredit.Text = "0.00"
        txtDebit.Text = "0.00"
        txtDifference.Text = "0.00"
        cbBook.SelectedIndex = 0
        txtUpload_ID.Text = ""
        cbBook.SelectedText = ""
        dgvEntry.Rows.Clear()
    End Sub

    Public Function GenerateTransID(ColID As String, Table As String) As String
        Dim TransID As String = ""
        ' GENERATE TRANS ID 
        Dim query As String
        query = " SELECT    ISNULL(MAX(" & ColID & ")+ 1,1) AS TransID  " & _
                " FROM      " & Table & "  "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            TransID = SQL.SQLDR("TransID")
        Else
            TransID = 0
        End If
        Return TransID
    End Function

    Public Sub TotalDBCR()
        ' COMPUTE TOTAL DEBIT
        Dim db As Decimal = 0

        For i As Integer = 0 To dgvEntry.Rows.Count - 1
            If Val(dgvEntry.Item(chDebit.Index, i).Value) <> 0 Then
                db = db + Double.Parse(dgvEntry.Item(chDebit.Index, i).Value).ToString("N2")
            End If
        Next
        txtDebit.Text = db.ToString("N2")

        ' COMPUTE TOTAL CREDIT
        Dim b As Decimal = 0

        For i As Integer = 0 To dgvEntry.Rows.Count - 1
            If Val(dgvEntry.Item(chCredit.Index, i).Value) <> 0 Then
                b = b + Double.Parse(dgvEntry.Item(chCredit.Index, i).Value).ToString("N2")
            End If
        Next
        txtCredit.Text = b.ToString("N2")


        txtDifference.Text = CDec(txtDebit.Text - txtCredit.Text).ToString("N2")
    End Sub

    Private Sub bgwSave_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwSave.DoWork
        Dim start As Boolean = False

        SetPGBmax(dgvEntry.Rows.Count)
        Dim i As Integer = 0
        For Each row As DataGridViewRow In dgvEntry.Rows
            Dim JETransID As Integer

            If row.Cells(3).Value <> "" Then
                If Not TransIDexist(row.Cells(1).Value, Book, row.Cells(2).Value, 1) Then
                    SaveEntry(row.Cells(1).Value, row.Cells(0).Value, 0, row.Cells(2).Value, Book, txtUpload_ID.Text, txtRemarks.Text, row.Cells(chCurrency.Index).Value, row.Cells(chExchange_Rate.Index).Value)
                    JETransID = LoadJE(row.Cells(2).Value, row.Cells(1).Value, Book, 1)
                    SaveEntryDetails(JETransID, row.Cells(3).Value, row.Cells(7).Value, row.Cells(5).Value, row.Cells(6).Value, row.Cells(9).Value, row.Cells(10).Value, row.Cells(11).Value) ' ENTRY SUNDRIES
                Else
                    JETransID = LoadJE(row.Cells(2).Value, row.Cells(1).Value, Book, 1)
                    UpdateEntry(JETransID, 0)
                    SaveEntryDetails(JETransID, row.Cells(3).Value, row.Cells(7).Value, row.Cells(5).Value, row.Cells(6).Value, row.Cells(9).Value, row.Cells(10).Value, row.Cells(11).Value) ' ENTRY SUNDRIES
                End If
            End If
            bgwSave.ReportProgress(i)
            i += 1

        Next
    End Sub


    Private Sub tsbSave_Click_1(sender As Object, e As EventArgs) Handles tsbSave.Click
        If cbBook.SelectedIndex = -1 Then
            MsgBox("Please Select Book first!", MsgBoxStyle.Exclamation)
        ElseIf CDec(txtDebit.Text) <> CDec(txtCredit.Text) Then
            MsgBox("Please check the Debit and Credit Amount!", MsgBoxStyle.Exclamation)
        ElseIf Not Valid Then
            MsgBox("Some data are invalid !, Please Check highlighted cells.", MsgBoxStyle.Exclamation)
        Else
            pgbCounter.Visible = True
            bgwSave.RunWorkerAsync()
            tsbSave.Enabled = False
        End If
    End Sub

    Private Sub tsbSearch_Click(sender As Object, e As EventArgs) Handles tsbSearch.Click
        'If Not AllowAccess("UB_VIEW") Then
        '    msgRestricted()
        'Else
        Dim f As New frmLoadTransactions
        f.ShowDialog("BOOK Upload")
        If f.transID <> "" Then
            Book_ID = f.transID
        End If
        LoadBooks(Book_ID)
        f.Dispose()
        ' End If
    End Sub

    Private Sub LoadBooks(ByVal Code As String)
        Dim query, JE_No As String
        Dim Book, Reftype As String
        query = " SELECT  JE_No, UploadID, RefType, Book, TotalDBCR, Remarks, Status  " & _
                " FROM tblJE_Header " & _
                " WHERE UploadID = '" & Code & "' "
        SQL.ReadQuery(query)
        dgvEntry.Rows.Clear()
        While SQL.SQLDR.Read
            JE_No = SQL.SQLDR("JE_No").ToString
            txtUpload_ID.Text = SQL.SQLDR("UploadID").ToString
            Reftype = SQL.SQLDR("RefType").ToString
            Book = SQL.SQLDR("Book").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString

            If Book = "Cash Disbursements" Then
                cbBook.SelectedItem = "Cash Disbursements Book"
            ElseIf Book = "Cash Receipts" Then
                cbBook.SelectedItem = "Cash Receipts Book"
            ElseIf Book = "General Journal" Then
                cbBook.SelectedItem = "General Journal"
            ElseIf Book = "Sales" Then
                cbBook.SelectedItem = "Sales Book"
            ElseIf Book = "Purchase Journal" Then
                cbBook.SelectedItem = "Purchase Book"
            End If
            LoadAccountingEntry(JE_No)

            If txtStatus.Text = "Cancelled" Then
                tsbCancel.Enabled = False
                tsbUpload.Enabled = False
            Else
                tsbCancel.Enabled = True
                tsbUpload.Enabled = True
            End If

            tsbClose.Enabled = False
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbSave.Enabled = False
            tsbNew.Enabled = True
            tsbSearch.Enabled = True
            tsbExit.Enabled = True
            EnableControl(False)
        End While
        TotalDBCR()
    End Sub

    Private Sub LoadAccountingEntry(ByVal TransID As Integer)
        Try
            Dim query As String
            query = " SELECT AppDate,RefTransID,RefType, AccntCode, AccountTitle, Debit, Credit,  " & _
                    " 		tblJE_Details.VCECode, VCEName, Particulars, RefNo, Currency, ISNULL(Exchange_Rate,0) as Exchange_Rate  " & _
                    " FROM tblJE_Details " & _
                    " INNER JOIN tblJE_Header ON " & _
                    " tblJE_Header.JE_No = tblJE_Details.JE_No " & _
                    " INNER JOIN  tblCOA_Master ON " & _
                    " tblCOA_Master.AccountCode = tblJE_Details.AccntCode " & _
                    " LEFT JOIN viewVCE_Master ON " & _
                    " viewVCE_Master.VCECode = tblJE_Details.VCECode " & _
                    " WHERE  tblJE_Details.JE_No ='" & TransID & "' "
            SQL.ReadQuery(query, 2)
            Dim rowsCount As Integer = dgvEntry.Rows.Count
            If SQL.SQLDR2.HasRows Then
                While SQL.SQLDR2.Read

                    dgvEntry.Rows.Add(SQL.SQLDR2("AppDate").ToString)
                    dgvEntry.Rows(rowsCount).Cells(chRefTransID.Index).Value = SQL.SQLDR2("RefTransID").ToString
                    dgvEntry.Rows(rowsCount).Cells(chRefType.Index).Value = SQL.SQLDR2("RefType").ToString
                    dgvEntry.Rows(rowsCount).Cells(chAccntCode.Index).Value = SQL.SQLDR2("AccntCode").ToString
                    dgvEntry.Rows(rowsCount).Cells(chAccntTitle.Index).Value = SQL.SQLDR2("AccountTitle").ToString
                    dgvEntry.Rows(rowsCount).Cells(chDebit.Index).Value = CDec(SQL.SQLDR2("Debit")).ToString("N2")
                    dgvEntry.Rows(rowsCount).Cells(chCredit.Index).Value = CDec(SQL.SQLDR2("Credit")).ToString("N2")
                    dgvEntry.Rows(rowsCount).Cells(chVCECode.Index).Value = SQL.SQLDR2("VCECode").ToString
                    dgvEntry.Rows(rowsCount).Cells(chVCEName.Index).Value = SQL.SQLDR2("VCEName").ToString
                    dgvEntry.Rows(rowsCount).Cells(chParticulars.Index).Value = SQL.SQLDR2("Particulars").ToString
                    dgvEntry.Rows(rowsCount).Cells(chRefNo.Index).Value = SQL.SQLDR2("RefNo").ToString
                    dgvEntry.Rows(rowsCount).Cells(chCurrency.Index).Value = SQL.SQLDR2("Currency").ToString
                    dgvEntry.Rows(rowsCount).Cells(chExchange_Rate.Index).Value = SQL.SQLDR2("Exchange_Rate").ToString

                    rowsCount += 1
                End While

            Else
                JETransID = 0
                dgvEntry.Rows.Clear()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtTotalAmt_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If txtUpload_ID.Text <> "" Then
            Dim query As String
            query = "   SELECT Top 1 UploadID FROM tblJE_Header  " & _
                    "    WHERE UploadID > '" & txtUpload_ID.Text & "' AND isUpload = 1 ORDER BY UploadID ASC  "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Book_ID = SQL.SQLDR("UploadID").ToString
                LoadBooks(Book_ID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub cbReftype_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub dgvEntry_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntry.CellContentClick

    End Sub

    Private Sub tsbUpload_Click(sender As System.Object, e As System.EventArgs) Handles tsbUpload.Click
        If tsbUpload.Text = "Upload" Then
            With (OpenFileDialog1)
                .InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                .Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx"
                .FilterIndex = 2
            End With
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                If MessageBox.Show("Uploading Transaction" & vbNewLine & "Are you sure you want to Contiue?", "Message Alert", MessageBoxButtons.YesNo) = MsgBoxResult.Yes Then
                    path = OpenFileDialog1.FileName
                    txtCredit.Text = "0.00"
                    txtDebit.Text = "0.00"
                    txtDifference.Text = "0.00"
                    dgvEntry.Rows.Clear()
                    dgvEntry.ReadOnly = True
                    pgbCounter.Visible = True
                    bgwUpload.RunWorkerAsync()
                    tsbUpload.Text = "Stop"
                End If
            End If
        Else
            If (bgwUpload.IsBusy = True) Then
                tsbUpload.Text = "Upload"
                pgbCounter.Value = 0
                bgwUpload.CancelAsync()
            End If
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If txtUpload_ID.Text <> "" Then
            Dim query As String
            query = "   SELECT Top 1 UploadID FROM tblJE_Header  " & _
                    "    WHERE UploadID < '" & txtUpload_ID.Text & "' AND isUpload = 1 ORDER BY UploadID DESC  "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Book_ID = SQL.SQLDR("UploadID").ToString
                LoadBooks(Book_ID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbDownload_Click(sender As System.Object, e As System.EventArgs) Handles tsbDownload.Click
        Dim fileName As String = "Books Uploader.xlsx"
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        xlApp = New Excel.Application

        Dim App_Path As String

        App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName & "\Templates"
        If My.Computer.FileSystem.FileExists(App_Path + "\" & templateName & ".xlsx") Then
            xlWorkBook = xlApp.Workbooks.Open(App_Path + "\" & templateName & ".xlsx")
            xlWorkSheet = xlWorkBook.Worksheets("Template")
            xlWorkSheet.Unprotect(excelPW)
            For i As Integer = 0 To dgvEntry.Columns.Count - 1
                If i = 0 Then
                    xlWorkSheet.Cells(1, i + 1) = templateName
                End If
                xlWorkSheet.Cells(2, i + 1) = dgvEntry.Columns(i).Name
                xlWorkSheet.Cells(3, i + 1) = dgvEntry.Columns(i).HeaderText
            Next
            xlWorkSheet.Protect(excelPW)
            Dim ctr As Integer = 1
            Do
                fileName = "Books Uploader -" & ctr.ToString & ".xlsx"
                If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & fileName) = False Then
                    Exit Do
                End If
                ctr += 1
            Loop

            xlWorkBook.SaveAs(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & fileName)
            xlWorkBook.Close(Type.Missing, Type.Missing, Type.Missing)
            xlApp.Quit()

            ' CLEAN UP. (CLOSE INSTANCES OF EXCEL OBJECTS.)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet) : xlWorkSheet = Nothing
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook) : xlWorkBook = Nothing
        Else
            MsgBox("No Template found!" & vbNewLine & "Please contact your systems administrator", MsgBoxStyle.Exclamation)
        End If

        ' CLEAN UP. (CLOSE INSTANCES OF EXCEL OBJECTS.)
        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp) : xlApp = Nothing
        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & fileName) Then
            Dim xls As Excel.Application
            Dim workbook As Excel.Workbook
            xls = New Excel.Application
            xls.Visible = True
            workbook = xls.Workbooks.Open(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & fileName)
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        ' Toolstrip Buttons
        If Book_ID = "" Then
            ClearText()
            EnableControl(False)
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
        Else
            LoadBooks(Book_ID)
            tsbCancel.Enabled = True
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
        End If
        tsbSearch.Enabled = True
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        'If Not AllowAccess("UB_DEL") Then
        '    msgRestricted()
        'Else
        If txtUpload_ID.Text <> "" Then
            If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                Try
                    activityStatus = True
                    Dim updateSQL As String
                    updateSQL = " UPDATE  tblJE_Header SET Status ='Cancelled' WHERE UploadID = @UploadID AND Book = @Book "
                    SQL.FlushParams()
                    SQL.AddParam("@UploadID", txtUpload_ID.Text)
                    SQL.AddParam("@Book", Book)
                    SQL.ExecNonQuery(updateSQL)

                    tsbSearch.Enabled = True
                    tsbNew.Enabled = True
                    tsbSave.Enabled = False
                    tsbCancel.Enabled = False
                    tsbClose.Enabled = False
                    tsbPrevious.Enabled = False
                    tsbNext.Enabled = False
                    tsbExit.Enabled = True
                    EnableControl(False)

                    Msg("Record cancelled successfully", MsgBoxStyle.Information)
                    LoadBooks(txtUpload_ID.Text)
                Catch ex As Exception
                    activityStatus = True
                    SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                End Try
            End If
        End If
        ' End If
    End Sub
End Class