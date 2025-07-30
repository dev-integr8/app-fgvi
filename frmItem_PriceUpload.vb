Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security
Imports System.Security.Principal
Imports System.Net.NetworkInformation
Imports System.Text

Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices


Imports System.Configuration

Public Class frmItem_PriceUpload

    Dim moduleID As String = "VCE"
    Dim FileName As String
    Dim templateName As String = "VCE UPLOADER"
    Dim selectedquery As String = ""

    Private Sub Panel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub tsbUpload_Click(sender As System.Object, e As System.EventArgs) Handles tsbUpload.Click
        Dim OpenFileDialog As New OpenFileDialog
        Dim ctrN As Integer
        Dim str As String
        Dim objExcel As New Microsoft.Office.Interop.Excel.Application
        dgvEntry.Rows.Clear()

        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            FileName = OpenFileDialog.FileName
            objExcel.Workbooks.Open(FileName)
            str = "a"
            ctrN = 2

            Do While str <> ""
                Dim ItemCode, ItemName, ItemPrice, UOM, QTY, Type, VATInc, VATable As String

                ItemCode = RTrim(objExcel.Range("a" & CStr(ctrN)).Value)
                ItemName = RTrim(objExcel.Range("b" & CStr(ctrN)).Value)
                ItemPrice = RTrim(objExcel.Range("c" & CStr(ctrN)).Value)
                UOM = RTrim(objExcel.Range("d" & CStr(ctrN)).Value)
                QTY = RTrim(objExcel.Range("e" & CStr(ctrN)).Value)
                Type = RTrim(objExcel.Range("f" & CStr(ctrN)).Value)
                VATInc = RTrim(objExcel.Range("g" & CStr(ctrN)).Value)
                VATable = RTrim(objExcel.Range("h" & CStr(ctrN)).Value)


                dgvEntry.Rows.Add(New String() {
                                         ItemCode.ToString,
                ItemName.ToString,
                ItemPrice.ToString,
                UOM.ToString,
                QTY.ToString,
                Type.ToString,
                VATInc.ToString,
                VATable.ToString})


                ctrN = ctrN + 1
                str = RTrim(objExcel.Range("a" & CStr(ctrN)).Value)
            Loop
            objExcel.Workbooks.Close()

            EnableControl(True)
            tsbSave.Enabled = True
            tsbClose.Enabled = True
        End If
    End Sub


    Private Sub InsertItemPrice()
        Try
            Dim i As Integer = 0
            For Each row As DataGridViewRow In dgvEntry.Rows
                If row.Cells(chItemCode.Index).Value <> "" Then
                    If RecordExist(row.Cells(chItemCode.Index).Value) Then
                        UpdatePrice(row.Cells(chItemCode.Index).Value)
                        SaveItemPrice(row.Cells(chItemCode.Index).Value, row.Cells(chPrice.Index).Value, row.Cells(chUOM.Index).Value, row.Cells(chQTY.Index).Value, row.Cells(chType.Index).Value, row.Cells(chVAT.Index).Value, row.Cells(chVATInc.Index).Value)
                    Else
                        SaveItemPrice(row.Cells(chItemCode.Index).Value, row.Cells(chPrice.Index).Value, row.Cells(chUOM.Index).Value, row.Cells(chQTY.Index).Value, row.Cells(chType.Index).Value, row.Cells(chVAT.Index).Value, row.Cells(chVATInc.Index).Value)
                    End If
                End If
                i += 1
            Next
        Catch ex As System.Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        Finally
            RecordActivity(UserID, moduleID, Me.Name.ToString, "INSERT", "Item_Price", "UPLOAD", BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub


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


    Private Function validateDGV() As Boolean
        Dim value As Boolean = True
        For Each row As DataGridViewRow In dgvEntry.Rows
            'check item code if exist
            If Not IsNothing(row.Cells(chItemCode.Index).Value) Then
                If Not RecordExist(row.Cells(chItemCode.Index).Value) Then
                    ChangeCellColor(row.Index, chItemCode.Index)
                    value = False
                End If
            End If
        Next
        If value = False Then
            MsgBox("Some data are invalid !, Please Check highlighted cells.", MsgBoxStyle.Exclamation, "GR8 Message Alert")
        End If
        Return value
    End Function

    Private Delegate Sub ChangeCellColorInvoker(ByVal row As Integer, ByVal col As Integer)
    Private Sub ChangeCellColor(ByVal row As Integer, ByVal col As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New ChangeCellColorInvoker(AddressOf ChangeCellColor), row, col)
        Else
            dgvEntry.Rows(row).Cells(col).Style.BackColor = Color.Yellow
        End If
    End Sub

    Private Function RecordExist(ByVal Record As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblItem_Master WHERE ItemCode ='" & Record & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
     
            dgvEntry.Rows.Clear()

            Dim query As String
        query = " SELECT     ItemCode, ItemName FROM tblItem_Master  " & _
                        " WHERE Status = 'Active' "
            SQL.ReadQuery(query)
            Dim rowsCount As Integer = 0

            If SQL.SQLDR.HasRows Then
                While SQL.SQLDR.Read
                    dgvEntry.Rows.Add("")
                dgvEntry.Rows(rowsCount).Cells(chItemCode.Index).Value = SQL.SQLDR("ItemCode").ToString
                dgvEntry.Rows(rowsCount).Cells(chItemName.Index).Value = SQL.SQLDR("ItemName").ToString
                dgvEntry.Rows(rowsCount).Cells(chPrice.Index).Value = ""
                dgvEntry.Rows(rowsCount).Cells(chUOM.Index).Value = ""
                dgvEntry.Rows(rowsCount).Cells(chQTY.Index).Value = ""
                dgvEntry.Rows(rowsCount).Cells(chType.Index).Value = ""
                dgvEntry.Rows(rowsCount).Cells(chVAT.Index).Value = ""
                dgvEntry.Rows(rowsCount).Cells(chVATInc.Index).Value = ""
                    rowsCount += 1
                End While

                ' Toolstrip Buttons
                tsbNew.Enabled = False
                tsbExtract.Enabled = True
                tsbSave.Enabled = True
                tsbDownload.Enabled = True
                tsbUpload.Enabled = True
                tsbClose.Enabled = True
                tsbExit.Enabled = False

                EnableControl(True)
            Else

                ' Toolstrip Buttons
                tsbNew.Enabled = True
                tsbSave.Enabled = False
                tsbExtract.Enabled = False
                tsbDownload.Enabled = False
                tsbUpload.Enabled = False
                tsbClose.Enabled = False
                tsbExit.Enabled = False

                EnableControl(False)
                Msg("No Record!", MsgBoxStyle.Exclamation)
            End If
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        dgvEntry.Enabled = Value
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        dgvEntry.Rows.Clear()
        EnableControl(False)
        ' Toolstrip Buttons
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbExtract.Enabled = False
        tsbClose.Enabled = False
        tsbDownload.Enabled = False
        tsbUpload.Enabled = True
        tsbExit.Enabled = True
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If validateDGV() Then
            If dgvEntry.Rows.Count = 0 Then
                Msg("Please upload data!", MsgBoxStyle.Exclamation)
            Else
                If MsgBox("Update Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    InsertItemPrice()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    tsbClose.PerformClick()

                End If
            End If
        End If
    End Sub

    Private Sub frmVCE_Uploader_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbDownload.Enabled = False
        tsbExtract.Enabled = False
        tsbUpload.Enabled = True
        tsbExit.Enabled = True
        EnableControl(False)
    End Sub

    Private Sub tsbDownload_Click(sender As System.Object, e As System.EventArgs) Handles tsbDownload.Click
        Dim fileName As String = "VCE UPLOADER.xlsx"
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        xlApp = New Excel.Application
        Dim App_Path As String
        App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName & "\Templates"
        If My.Computer.FileSystem.FileExists(App_Path + "\VCE UPLOADER.xlsx") Then
            xlWorkBook = xlApp.Workbooks.Open(App_Path + "\VCE UPLOADER.xlsx")
            xlWorkSheet = xlWorkBook.Worksheets("Sheet1")
            Dim ctr As Integer = 1
            Do
                fileName = "VCE UPLOADER -" & ctr.ToString & ".xlsx"
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

    Private Sub dgvEntry_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntry.CellContentClick

    End Sub

    Private Sub tsbExtract_Click(sender As System.Object, e As System.EventArgs) Handles tsbExtract.Click
        Dim Separator As String = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator

        DataGridToCSV(dgvEntry, Separator)
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

        For r As Integer = 0 To dt.Rows.Count - 2

            Dim CSVLine As StringBuilder = New StringBuilder()
            Dim s As String = ""
            For c As Integer = 0 To dt.Columns.Count - 2
                If c = 0 Then
                    'CSVLine.Append(Qualifier & gridResults.Rows(r).Cells(c).Value.ToString() & Qualifier)
                    s = s & dgvEntry.Rows(r).Cells(c).Value.ToString()
                Else
                    'CSVLine.Append("," & Qualifier & gridResults.Rows(r).Cells(c).Value.ToString() & Qualifier)
                    s = s & Qualifier & IIf(dgvEntry.Rows(r).Cells(c).Value.ToString.Replace(ControlChars.CrLf, " ").Contains(Qualifier), """" & dgvEntry.Rows(r).Cells(c).Value.ToString.Replace(ControlChars.CrLf, " ") & """", dgvEntry.Rows(r).Cells(c).Value.ToString.Replace(ControlChars.CrLf, " "))
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
End Class