Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmItemPrice_Uploader
    Dim Book As String
    Dim PO_ID As String
    Dim ModuleID As String = "UB"
    Dim ColumnPK As String = "UploadID"
    Dim DBTable As String = "tblPO"
    Dim PONo As String
    Dim TransAuto, TransAutoPO As Boolean
    Dim PO_App As Boolean = False


    ' UPLOAD VARIABLES
    Dim Valid As Boolean = True
    Dim Cancelled As Boolean = False
    Dim InvalidTemplate As Boolean = False
    Dim path As String
    Dim templateName As String = "TEMPLATE_ITEMPRICE"
    Public excelPW As String = "@dm1nEvo"



    Private Sub cbBook_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)

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

                            ' ItemCode No
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If

                        ElseIf j = chD_ItemName.Index Then
                            ' Check if has valid ItemName
                            If ObjectText <> "" Then
                                If Not validateItem(ObjectText) Then
                                    ' if not exist, change color.
                                    ChangeCellColor(rowCount - 1, j - 1)
                                    Valid = False
                                Else
                                    ' if existing  AccountCode
                                    dgvEntry.Item(j, rowCount - 1).Value = GetItemName(ObjectText)
                                End If
                            End If
                        ElseIf j = chD_UnitPrice.Index Then
                            ' Unit Price
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chD_UOM.Index Then
                            ' UOM
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chD_QTY.Index Then
                            ' QTY
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chD_Type.Index Then
                            ' VAT Type
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chD_VATable.Index Then
                            ' VATable
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chD_VATInc.Index Then
                            ' VATInc
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
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

    Public Function validateItem(ByVal ItemCode As String) As Boolean
        Try
            Dim query As String
            query = " SELECT    * " &
                    " FROM      tblItem_Master " &
                    " WHERE     ItemCode = @ItemCode "
            SQL.FlushParams()
            SQL.AddParam("@ItemCode", ItemCode)
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

            LoadSetup()
            TransAuto = GetTransSetup(ModuleID)
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

    Private Sub LoadSetup()
        Dim query As String
        query = " SELECT  ISNULL(PO_Approval,0) AS PO_Approval, ISNULL(PO_DiffSeries,0) AS PO_DiffSeries, " & _
                " EmailUsername,  EmailPassword " & _
                " FROM tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            PO_App = SQL.SQLDR("PO_Approval")
            TransAutoPO = SQL.SQLDR("PO_DiffSeries")
        End If
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)

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



    Public Function LoadPOTransID(ByVal PORef As String, UploadiD As String) As String
        Dim query As String
        query = " SELECT TransID FROM tblPO WHERE PORef='" & PORef & "' AND UploadID ='" & UploadiD & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read AndAlso Not IsDBNull(SQL.SQLDR("TransID")) Then
            Return SQL.SQLDR("TransID")
        Else
            Return 0
        End If
    End Function

    Private Function ItemPriceExist(ByVal ItemCode As String) As Boolean
        Try
            Dim query As String
            query = " SELECT * FROM tblItem_Price " & _
                " WHERE ItemCode = @ItemCode AND Status = 'Active' "
            SQL.FlushParams()
            SQL.AddParam("@ItemCode", ItemCode)
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

    Private Sub UpdatePrice(ByVal ItemCode As String)
        Dim updateSQL As String
        updateSQL = " UPDATE tblItem_Price SET Status ='Inactive' WHERE ItemCode = @ItemCode"
        SQL.FlushParams()
        SQL.AddParam("@ItemCode", ItemCode)
        SQL.ExecNonQuery(updateSQL)

    End Sub

    Private Sub SaveItemPrice(ByVal ItemCode As String, UnitPrice As Decimal, UOM As String, QTY As Decimal, Type As String, VATable As String, VATInc As String)
        Dim insertSQL As String

        insertSQL = " INSERT INTO " & _
                          " tblItem_Price (Category, Type, ItemCode, VCECode, UOM, UOMQTY, UnitPrice, VATInclusive, VAT) " & _
                          " VALUES (@Category, @Type, @ItemCode, @VCECode, @UOM, @UOMQTY, @UnitPrice, @VATInclusive, @VAT ) "
        SQL.FlushParams()
        SQL.AddParam("@Category", "Selling")
        SQL.AddParam("@Type", Type)
        SQL.AddParam("@ItemCode", ItemCode)
        SQL.AddParam("@VCECode", DBNull.Value)
        SQL.AddParam("@UOM", UOM)
        SQL.AddParam("@UOMQTY", CDec(QTY))
        SQL.AddParam("@UnitPrice", CDec(UnitPrice))
        SQL.AddParam("@VATInclusive", IIf(VATInc = "", False, VATInc))
        SQL.AddParam("@VAT", IIf(VATable = "", False, VATable))
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
        LoadPO(txtUpload_ID.Text)
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
        txtCredit.Text = "0.00"
        txtDebit.Text = "0.00"
        txtDifference.Text = "0.00"
        txtUpload_ID.Text = ""
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

    End Sub

    Private Sub bgwSave_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwSave.DoWork
        Dim start As Boolean = False

        SetPGBmax(dgvEntry.Rows.Count)
        Dim i As Integer = 0
        For Each row As DataGridViewRow In dgvEntry.Rows
            If row.Cells(chD_ItemCode.Index).Value <> "" Then
                If ItemPriceExist(row.Cells(chD_ItemCode.Index).Value) Then
                    UpdatePrice(row.Cells(chD_ItemCode.Index).Value)
                    SaveItemPrice(row.Cells(chD_ItemCode.Index).Value, row.Cells(chD_UnitPrice.Index).Value, row.Cells(chD_UOM.Index).Value, row.Cells(chD_QTY.Index).Value, row.Cells(chD_Type.Index).Value, row.Cells(chD_VATable.Index).Value, row.Cells(chD_VATInc.Index).Value)
                Else
                    SaveItemPrice(row.Cells(chD_ItemCode.Index).Value, row.Cells(chD_UnitPrice.Index).Value, row.Cells(chD_UOM.Index).Value, row.Cells(chD_QTY.Index).Value, row.Cells(chD_Type.Index).Value, row.Cells(chD_VATable.Index).Value, row.Cells(chD_VATInc.Index).Value)
                End If
            End If
            bgwSave.ReportProgress(i)
            i += 1

        Next
    End Sub


    Private Sub tsbSave_Click_1(sender As Object, e As EventArgs) Handles tsbSave.Click
        If Not Valid Then
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
            PO_ID = f.transID
        End If
        LoadPO(PO_ID)
        f.Dispose()
        ' End If
    End Sub

    Private Sub LoadPO(ByVal TransID As String)
        Dim query, JE_No As String
        Dim Book, Reftype As String
        query = " SELECT  JE_No, UploadID, RefType, Book, TotalDBCR, Remarks, Status  " & _
                " FROM tblJE_Header " & _
                " WHERE UploadID = '" & TransID & "' "
        SQL.ReadQuery(query)
        dgvEntry.Rows.Clear()
        While SQL.SQLDR.Read
            JE_No = SQL.SQLDR("JE_No").ToString
            txtUpload_ID.Text = SQL.SQLDR("UploadID").ToString
            Reftype = SQL.SQLDR("RefType").ToString
            Book = SQL.SQLDR("Book").ToString

            LoadAccountingEntry(JE_No)

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



                    rowsCount += 1
                End While

            Else
                TransID = 0
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
                PO_ID = SQL.SQLDR("UploadID").ToString
                LoadPO(PO_ID)
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
                PO_ID = SQL.SQLDR("UploadID").ToString
                LoadPO(PO_ID)
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
        Dim fileName As String = "Item Price Uploader.xlsx"
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
                fileName = "Item Price Uploader -" & ctr.ToString & ".xlsx"
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
        If PO_ID = "" Then
            ClearText()
            EnableControl(False)
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
        Else
            LoadPO(PO_ID)
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
                    LoadPO(txtUpload_ID.Text)
                Catch ex As Exception
                    activityStatus = True
                    SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                End Try
            End If
        End If
        ' End If
    End Sub
End Class