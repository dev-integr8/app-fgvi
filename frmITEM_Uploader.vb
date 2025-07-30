Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmITEM_Uploader

    Dim moduleID As String = "ITM"
    Dim FileName As String
    Dim templateName As String = "ITEM UPLOADER"

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
                Dim ItemCode, ItemName, UOM, ItemType, UnitCost, InvCode, InvTitle,
                    COSCode, COSTitle, SalesCode, SalesTitle, ConvertQTY, UOMTo, VCECode As String

                ItemCode = RTrim(objExcel.Range("a" & CStr(ctrN)).Value)
                ItemName = RTrim(objExcel.Range("b" & CStr(ctrN)).Value)
                ItemType = RTrim(objExcel.Range("c" & CStr(ctrN)).Value)
                UnitCost = RTrim(objExcel.Range("d" & CStr(ctrN)).Value)
                InvCode = RTrim(objExcel.Range("e" & CStr(ctrN)).Value)
                InvTitle = RTrim(objExcel.Range("f" & CStr(ctrN)).Value)
                COSCode = RTrim(objExcel.Range("g" & CStr(ctrN)).Value)
                COSTitle = RTrim(objExcel.Range("h" & CStr(ctrN)).Value)
                SalesCode = RTrim(objExcel.Range("i" & CStr(ctrN)).Value)
                SalesTitle = RTrim(objExcel.Range("j" & CStr(ctrN)).Value)
                VCECode = RTrim(objExcel.Range("k" & CStr(ctrN)).Value)
                UOM = RTrim(objExcel.Range("m" & CStr(ctrN)).Value)
                ConvertQTY = RTrim(objExcel.Range("n" & CStr(ctrN)).Value)
                UOMTo = RTrim(objExcel.Range("o" & CStr(ctrN)).Value)

                dgvEntry.Rows.Add(New String() {
                                         ItemCode.ToString,
                                        ItemName.ToString,
                                        ItemType.ToString,
                                        UnitCost.ToString,
                                        InvCode.ToString,
                                        InvTitle.ToString,
                                        COSCode.ToString,
                                        COSTitle.ToString,
                                        SalesCode.ToString,
                                        SalesTitle.ToString,
                                        VCECode.ToString,
                                        GetVCEName(VCECode.ToString),
                                        UOM.ToString,
                                        ConvertQTY.ToString,
                                         UOMTo})


                ctrN = ctrN + 1
                str = RTrim(objExcel.Range("a" & CStr(ctrN)).Value)
            Loop
            objExcel.Workbooks.Close()
            validateDGV()
        End If
    End Sub


    Private Sub SaveVCE_upload()
        Try
            activityStatus = True
            Dim insertSQL, insertSQL2, insertSQL3 As String
            Dim i As Integer = 0
            For Each row As DataGridViewRow In dgvEntry.Rows
                If Not row.Cells(0).Value Is Nothing Then
                    insertSQL = " INSERT INTO " &
                     " tblItem_Master   (ItemCode, ItemName, ItemDescription, ItemType, ItemUOM, ID_SC, AD_Inv, AD_COS, AD_Sales, isInventory, PD_Supplier)  " &
                     " VALUES                (@ItemCode, @ItemName, @ItemDescription, @ItemType, @ItemUOM, @ID_SC, @AD_Inv, @AD_COS, @AD_Sales, @isInventory, @PD_Supplier) "
                    SQL.FlushParams()
                    SQL.AddParam("@ItemCode", row.Cells(chItemCode.Index).Value.ToString)
                    SQL.AddParam("@ItemName", row.Cells(chItemName.Index).Value.ToString)
                    SQL.AddParam("@ItemDescription", row.Cells(chItemName.Index).Value.ToString)
                    SQL.AddParam("@ItemType", row.Cells(chItemType.Index).Value.ToString)
                    SQL.AddParam("@ItemUOM", row.Cells(chUOM.Index).Value.ToString)
                    SQL.AddParam("@ID_SC", CDec(row.Cells(chUnitCost.Index).Value))
                    SQL.AddParam("@AD_Inv", row.Cells(chInvCode.Index).Value.ToString)
                    SQL.AddParam("@AD_COS", row.Cells(chCOSCode.Index).Value.ToString)
                    SQL.AddParam("@AD_Sales", row.Cells(chSales.Index).Value.ToString)
                    SQL.AddParam("@isInventory", True)
                    SQL.AddParam("@PD_Supplier", row.Cells(chVCECode.Index).Value.ToString)
                    SQL.ExecNonQuery(insertSQL)

                    insertSQL2 = "  INSERT INTO " &
                                    "  tblUOM_Group(GroupCode, UnitCode, Manual, WhoCreated) " &
                                    "  VALUES(@GroupCode, @UnitCode, @Manual, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@GroupCode", row.Cells(chItemCode.Index).Value.ToString)
                    SQL.AddParam("@UnitCode", row.Cells(chUOM.Index).Value.ToString)
                    SQL.AddParam("@Manual", True)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL2)

                    insertSQL3 = " INSERT INTO " &
                                        " tblUOM_GroupDetails(GroupCode, UnitCodeFrom, QTY, UnitCodeTo, WhoCreated) " &
                                        " VALUES(@GroupCode, @UnitCodeFrom, @QTY, @UnitCodeTo, @WhoCreated)"
                    SQL.FlushParams()
                    SQL.AddParam("@GroupCode", row.Cells(chItemCode.Index).Value.ToString)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.AddParam("@UnitCodeFrom", row.Cells(chUOM.Index).Value.ToString)
                    SQL.AddParam("@QTY", CDec(row.Cells(chConvertQTY.Index).Value))
                    SQL.AddParam("@UnitCodeTo", row.Cells(chUOMTo.Index).Value.ToString)
                    SQL.ExecNonQuery(insertSQL3)

                    i += 1
                End If
            Next
        Catch ex As System.Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        Finally
            RecordActivity(UserID, moduleID, Me.Name.ToString, "INSERT", "ItemCode", "UPLOAD", BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
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
        If Not AllowAccess("ITM_ADD") Then
            msgRestricted()
        Else

            dgvEntry.Rows.Clear()
            ' Toolstrip Buttons
            tsbNew.Enabled = False
            tsbSave.Enabled = True
            tsbDownload.Enabled = True
            tsbUpload.Enabled = True
            tsbClose.Enabled = True
            tsbExit.Enabled = False


            EnableControl(True)
        End If
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        dgvEntry.Enabled = Value
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbDownload.Enabled = False
        tsbUpload.Enabled = False
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
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    SaveVCE_upload()
                    Msg("Date upload successfully!", MsgBoxStyle.Exclamation)
                End If
            End If
        End If
    End Sub

    Private Function validateDGV() As Boolean
        Dim value As Boolean = True
        For Each row As DataGridViewRow In dgvEntry.Rows
            'check item code if exist
            If RecordExist(row.Cells(chItemCode.Index).Value) Then
                ChangeCellColor(row.Index, chItemCode.Index)
                value = False
            End If

            'check item name
            If row.Cells(chItemName.Index).Value = "" Then
                ChangeCellColor(row.Index, chItemName.Index)
                value = False
            End If

            'check item UOM
            If row.Cells(chUOM.Index).Value = "" Then
                ChangeCellColor(row.Index, chUOM.Index)
                value = False
            End If

            'check item unit Cost
            If Not IsNumeric(row.Cells(chUnitCost.Index).Value) Then
                ChangeCellColor(row.Index, chUnitCost.Index)
                value = False
            End If

            'check Inv Code
            If Not validateAccountCode(row.Cells(chInvCode.Index).Value) Then
                ' if not exist, change color.
                ChangeCellColor(row.Index, chInvCode.Index)
                value = False
            Else
                ' if existing  AccountCode
                dgvEntry.Item(chInvTitle.Index, row.Index).Value = GetAccntTitle(row.Cells(chInvCode.Index).Value)
            End If

            'check COS Code
            If Not validateAccountCode(row.Cells(chCOSCode.Index).Value) Then
                ' if not exist, change color.
                ChangeCellColor(row.Index, chCOSCode.Index)
                value = False
            Else
                ' if existing  AccountCode
                dgvEntry.Item(chCOSTitle.Index, row.Index).Value = GetAccntTitle(row.Cells(chCOSCode.Index).Value)
            End If

            'check COS Code
            If Not validateAccountCode(row.Cells(chSales.Index).Value) Then
                ' if not exist, change color.
                ChangeCellColor(row.Index, chSales.Index)
                value = False
            Else
                ' if existing  AccountCode
                dgvEntry.Item(chSalesTitle.Index, row.Index).Value = GetAccntTitle(row.Cells(chSales.Index).Value)
            End If
        Next
        If value = False Then
            MsgBox("Some data are invalid !, Please Check highlighted cells.", MsgBoxStyle.Exclamation, "GR8 Message Alert")
        End If
        Return value
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

    Private Sub frmVCE_Uploader_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbDownload.Enabled = False
        tsbUpload.Enabled = False
        tsbExit.Enabled = True
        EnableControl(False)
    End Sub

    Private Sub tsbDownload_Click(sender As System.Object, e As System.EventArgs) Handles tsbDownload.Click
        Dim fileName As String = "ITEM UPLOADER.xlsx"
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        xlApp = New Excel.Application
        Dim App_Path As String
        App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName & "\Templates"
        If My.Computer.FileSystem.FileExists(App_Path + "\ITEM UPLOADER.xlsx") Then
            xlWorkBook = xlApp.Workbooks.Open(App_Path + "\ITEM UPLOADER.xlsx")
            xlWorkSheet = xlWorkBook.Worksheets("Sheet1")
            Dim ctr As Integer = 1
            Do
                fileName = "ITEM UPLOADER -" & ctr.ToString & ".xlsx"
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

    Private Delegate Sub ChangeCellColorInvoker(ByVal row As Integer, ByVal col As Integer)
    Private Sub ChangeCellColor(ByVal row As Integer, ByVal col As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New ChangeCellColorInvoker(AddressOf ChangeCellColor), row, col)
        Else
            dgvEntry.Rows(row).Cells(col).Style.BackColor = Color.Yellow
        End If
    End Sub
End Class