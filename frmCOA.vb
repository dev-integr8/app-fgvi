Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

Public Class frmCOA
    Dim lastNodeIndex As Integer = -1
    Dim rowIndexFromMouseDown As Integer
    Dim rw As DataGridViewRow
    Dim moveItem As Boolean = False
    Private Path As String = New IO.FileInfo(Application.ExecutablePath).DirectoryName & "\Templates"

    Private Sub frmCOA_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        cbType.SelectedItem = "Balance Sheet"

        tsbEdit.Enabled = False
        tsbSave.Enabled = False
        tsbUndo.Enabled = False
        tsbUp.Enabled = False
        tsbDown.Enabled = False
        tsbDelete.Enabled = False

    End Sub

    Private Function GetHierarchy(ByVal Account As String) As Integer
        Dim query As String
        query = " SELECT Hierarchy FROM tblCOA_AccountGroup WHERE AccountGroup ='" & Account & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("Hierarchy").ToString
        Else
            Return 0
        End If
    End Function

    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs)


    End Sub

    Private Sub cbType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbType.SelectedIndexChanged
        If cbType.SelectedIndex <> -1 Then
            LoadAccounts()

            tsbEdit.Enabled = False
            tsbSave.Enabled = False
            tsbUndo.Enabled = False
            tsbUp.Enabled = False
            tsbDown.Enabled = False
            tsbDelete.Enabled = False
        End If
    End Sub

    Public Sub LoadAccounts()
        If cbType.SelectedIndex <> -1 Then
            Dim query As String
            query = " SELECT	AccountCode, AccountTitle,  AccountNature, Hierarchy, ReportAlias, tblCOA_Master.AccountGroup, OrderNo " & _
                    " FROM	    tblCOA_Master INNER JOIN tblCOA_AccountGroup " & _
                    " ON		tblCOA_Master.AccountGroup = tblCOA_AccountGroup.AccountGroup " & _
                    " WHERE     AccountType ='" & cbType.SelectedItem & "'  AND Status = 'Active'" & _
                    " ORDER BY  OrderNo "
            SQL.GetQuery(query)
            dgvPreview.Rows.Clear()
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                dgvPreview.Rows.Clear()
                Dim code, title, nature, group As String
                Dim line As Integer
                Dim tabCount As Integer = 0
                For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                    code = row.Item(0).ToString
                    title = row.Item(1).ToString
                    nature = row.Item(2).ToString
                    group = row.Item(5).ToString
                    line = row.Item(6)
                    tabCount = row.Item(3) - 1
                    If tabCount > 0 Then
                        For i As Integer = 1 To tabCount
                            title = "   " & title
                        Next
                    End If
                    If nature <> "" Then
                        If nature = "Debit" Then
                            title = title & "                                                                                                 "
                            title = Strings.Left(title, 95)
                            title = title & "####.##"
                        Else
                            title = title & "                                                                                                                "
                            title = Strings.Left(title, 105)
                            title = title & "####.##"
                        End If
                    End If
                    dgvPreview.Rows.Add({code, title, group, line})
                Next
            End If
        End If
    End Sub

    Private Sub btnUp_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub btnDown_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub MoveAccount(ByVal Type As String)
        If dgvPreview.SelectedRows.Count = 1 Then
            lastNodeIndex = -1
            Dim rowIndexOfItemUnderMouseToDrop As Integer
            lastNodeIndex = dgvPreview.SelectedRows(0).Index
            rw = dgvPreview.SelectedRows(0)
            rowIndexFromMouseDown = dgvPreview.SelectedRows(0).Index
            If Type = "UP" Then
                rowIndexOfItemUnderMouseToDrop = dgvPreview.SelectedRows(0).Index - 1
            Else
                rowIndexOfItemUnderMouseToDrop = dgvPreview.SelectedRows(0).Index + 1
            End If
            If rowIndexOfItemUnderMouseToDrop <> -1 AndAlso rowIndexOfItemUnderMouseToDrop < dgvPreview.Rows.Count Then
                dgvPreview.Rows.RemoveAt(rowIndexFromMouseDown)
                dgvPreview.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rw)
                dgvUndo.Rows.Add(rowIndexFromMouseDown, rowIndexOfItemUnderMouseToDrop)
                If lastNodeIndex <> -1 Then
                    If Type = "UP" Then
                        dgvPreview.Rows(lastNodeIndex - 1).Selected = True
                    Else
                        dgvPreview.Rows(lastNodeIndex + 1).Selected = True
                        dgvPreview.Focus()

                    End If
                Else
                    dgvPreview.SelectedRows(0).Selected = False
                End If
            End If
        End If
    End Sub

    Private Sub dgvPreview_AutoSizeColumnModeChanged(sender As Object, e As System.Windows.Forms.DataGridViewAutoSizeColumnModeEventArgs) Handles dgvPreview.AutoSizeColumnModeChanged

    End Sub

    Private Sub dgvPreview_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles dgvPreview.DragDrop
        Dim rowIndexOfItemUnderMouseToDrop As Integer
        Dim clientPoint As Point = dgvPreview.PointToClient(New Point(e.X, e.Y))
        rowIndexOfItemUnderMouseToDrop = dgvPreview.HitTest(clientPoint.X, clientPoint.Y).RowIndex
        If (e.Effect = DragDropEffects.Move) AndAlso rowIndexOfItemUnderMouseToDrop <> rowIndexFromMouseDown _
            AndAlso rowIndexOfItemUnderMouseToDrop <> -1 AndAlso rowIndexFromMouseDown <> -1 Then
            dgvPreview.Rows.RemoveAt(rowIndexFromMouseDown)
            dgvPreview.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rw)
            dgvUndo.Rows.Add(rowIndexFromMouseDown, rowIndexOfItemUnderMouseToDrop)
            dgvPreview.Rows(rowIndexOfItemUnderMouseToDrop).Selected = True
        End If
    End Sub

    Private Sub dgvPreview_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles dgvPreview.DragEnter
        If dgvPreview.SelectedRows.Count > 0 Then
            e.Effect = DragDropEffects.Move
        End If
    End Sub

    Private Sub dgvPreview_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles dgvPreview.MouseDown
        If dgvPreview.SelectedRows.Count = 1 Then
            If e.Button = MouseButtons.Left Then
                moveItem = True
            End If
        End If
    End Sub

    Private Sub dgvPreview_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles dgvPreview.MouseUp
        If moveItem = True Then
            rw = dgvPreview.SelectedRows(0)
            rowIndexFromMouseDown = dgvPreview.SelectedRows(0).Index
            dgvPreview.DoDragDrop(rw, DragDropEffects.Move)
        End If
        moveItem = False
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub btnEdit_Click(sender As System.Object, e As System.EventArgs)


    End Sub

    Private Sub btnUndo_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub undoChanges()
        If dgvUndo.Rows.Count > 0 Then
            rw = dgvPreview.Rows(dgvUndo.Rows(dgvUndo.Rows.Count - 1).Cells(1).Value.ToString())
            dgvPreview.Rows.RemoveAt(dgvUndo.Rows(dgvUndo.Rows.Count - 1).Cells(1).Value.ToString())
            dgvPreview.Rows.Insert(dgvUndo.Rows(dgvUndo.Rows.Count - 1).Cells(0).Value, rw)
            dgvPreview.Rows(dgvUndo.Rows(dgvUndo.Rows.Count - 1).Cells(0).Value).Selected = True
            dgvUndo.Rows.RemoveAt(dgvUndo.Rows.Count - 1)
        End If
    End Sub

    Private Sub dgvPreview_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles dgvPreview.KeyDown
        If e.Shift = True AndAlso (e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up) Then
            If e.KeyCode = Keys.Down Then
                tsbDown.PerformClick()
                e.SuppressKeyPress = True
            Else
                tsbUp.PerformClick()
                e.SuppressKeyPress = True
            End If
        ElseIf e.Control = True Then
            If e.KeyCode = Keys.Z Then
                tsbUndo.PerformClick()
            ElseIf e.KeyCode = Keys.S Then
                tsbSave.PerformClick()
            ElseIf e.KeyCode = Keys.A Then
                tsbNew.PerformClick()
            ElseIf e.KeyCode = Keys.E Then
                tsbEdit.PerformClick()
            End If

        End If
    End Sub

    Private Sub dgvPreview_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPreview.CellContentClick
        If dgvPreview.SelectedRows.Count = 1 Then
            tsbEdit.Enabled = True
            tsbUp.Enabled = True
            tsbDown.Enabled = True
            tsbDelete.Enabled = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Function GetSaveFileName() As String
        Dim sfdFile As New SaveFileDialog
        With sfdFile
            .InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            .Filter = "Excel Files|*.xls;*.xlsx"
            .FilterIndex = 1
            If sfdFile.ShowDialog() = DialogResult.OK Then
                Return sfdFile.FileName
            Else
                Return ""
            End If
        End With

    End Function
    Private Sub OpenUploaderFile(File As String)
        If My.Computer.FileSystem.FileExists(File) Then
            Dim ExcelApp As Excel.Application
            Dim ExcelWorkBook As Excel.Workbook
            ExcelApp = New Excel.Application
            ExcelWorkBook = ExcelApp.Workbooks.Open(File)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Function GetUploaderFile() As String
        Dim ofdUploader As New OpenFileDialog
        With ofdUploader
            .InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            .Filter = "Excel Files|*.xls;*.xlsx"
            .FilterIndex = 1
        End With
        If ofdUploader.ShowDialog() = DialogResult.OK Then
            Return ofdUploader.FileName
        Else
            Return ""
        End If
    End Function

    Private Sub Upload()
        Try

            Dim objExcel As New Microsoft.Office.Interop.Excel.Application
            Dim OpenFileDialog As New OpenFileDialog
            Dim ctrN As Integer
            Dim str As String
            OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
            OpenFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel Files (2007) (*.xls)|*.xls|All Files (*.*)|*.*"
            If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
                Dim FileName As String = OpenFileDialog.FileName
                objExcel.Workbooks.Open(FileName)
                str = "a"
                Dim rows As Integer = 0
                ctrN = 3
                Do While str <> ""
                    ctrN = ctrN + 1
                    str = RTrim(objExcel.Range("a" & CStr(ctrN)).Value)
                    rows += 1
                Loop
                ctrN = 3
                str = "a"
                pgbCounter.Maximum = rows
                pgbCounter.Value = 0
                Dim insertSQL As String
                pgbCounter.Visible = True
                SQL.FlushParams()
                SQL.ExecNonQuery("DELETE FROM tblCOA_Master")
                Dim Header As Boolean = True
                Dim OrderNo As Integer = 0
                Do While ctrN <= rows
                    OrderNo += 1
                    Dim Code, Title, Type, Category, ReportAlias, Group, Nature, withSL, Status As String
                    Code = RTrim(objExcel.Range("a" & CStr(ctrN)).Value)
                    Title = RTrim(objExcel.Range("b" & CStr(ctrN)).Value)
                    Type = RTrim(objExcel.Range("c" & CStr(ctrN)).Value)
                    Category = RTrim(objExcel.Range("d" & CStr(ctrN)).Value)
                    ReportAlias = RTrim(objExcel.Range("e" & CStr(ctrN)).Value)
                    Group = RTrim(objExcel.Range("f" & CStr(ctrN)).Value)
                    Nature = RTrim(objExcel.Range("g" & CStr(ctrN)).Value)
                    withSL = RTrim(objExcel.Range("h" & CStr(ctrN)).Value)
                    Status = RTrim(objExcel.Range("i" & CStr(ctrN)).Value)
                    insertSQL = " INSERT INTO " & _
                                " tblCOA_Master (AccountCode, AccountType, AccountTitle, AccountCategory, AccountGroup, " & _
                                "                AccountNature, ReportAlias, withSubsidiary, OrderNo, Status) " & _
                                " VALUES (@AccountCode, @AccountType, @AccountTitle, @AccountCategory, @AccountGroup, " & _
                                "                @AccountNature, @ReportAlias, @withSubsidiary, @OrderNo, @Status) "
                    SQL.FlushParams()
                    SQL.AddParam("@AccountCode", Code)
                    SQL.AddParam("@AccountTitle", Title)
                    SQL.AddParam("@AccountType", Type)
                    SQL.AddParam("@AccountCategory", Category)
                    SQL.AddParam("@AccountGroup", Group)
                    SQL.AddParam("@AccountNature", Nature)
                    SQL.AddParam("@ReportAlias", ReportAlias)
                    SQL.AddParam("@withSubsidiary", withSL)
                    SQL.AddParam("@OrderNo", OrderNo)
                    SQL.AddParam("@Status", Status)
                    SQL.ExecNonQuery(insertSQL)
                    ctrN += 1
                    pgbCounter.Value += 1
                    'pgbCounter.Refresh()
                Loop
                pgbCounter.Value = 0
                pgbCounter.Visible = False
                objExcel.Workbooks.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub tsbDownload_Click(sender As System.Object, e As System.EventArgs) Handles tsbDownload.Click
        Dim SourceFile, DestinationFile As String
        SourceFile = Path + "\COA_UPLOADER"

        ' GET AVAILABLE FILE EXTENSION
        If My.Computer.FileSystem.FileExists(SourceFile & ".xlsx") Then
            SourceFile = SourceFile & ".xlsx"
        ElseIf My.Computer.FileSystem.FileExists(SourceFile & ".xls") Then
            SourceFile = SourceFile & ".xls"
        Else
            SourceFile = ""
        End If

        If SourceFile <> "" Then
            DestinationFile = GetSaveFileName()
            My.Computer.FileSystem.CopyFile(SourceFile, DestinationFile)
            OpenUploaderFile(DestinationFile)
        Else
            MsgBox("No Template found!" & vbNewLine & "Please contact your systems administrator", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub tsbUpload_Click(sender As System.Object, e As System.EventArgs) Handles tsbUpload.Click
        Upload()
        LoadAccounts()
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        frmCOA_Add.cbType.SelectedItem = cbType.SelectedItem

        frmCOA_Add.txtAccntCode.ReadOnly = False
        If dgvPreview.SelectedRows.Count = 1 Then
            frmCOA_Add.ShowDialog("", dgvPreview.SelectedRows(0).Cells(chGroup.Index).Value.ToString, dgvPreview.SelectedRows(0).Cells(chOrderNo.Index).Value.ToString)
        Else
            frmCOA_Add.ShowDialog()
        End If
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If dgvPreview.SelectedRows.Count = 1 Then
            frmCOA_Add.txtAccntCode.ReadOnly = True
            frmCOA_Add.ShowDialog(dgvPreview.SelectedRows(0).Cells(0).Value.ToString)
            LoadAccounts()
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        Dim sortNo As Integer = 1
        Dim updateSQL As String
        For Each row As DataGridViewRow In dgvPreview.Rows
            updateSQL = " UPDATE tblCOA_Master SET OrderNo ='" & sortNo & "' WHERE AccountCode ='" & row.Cells(0).Value.ToString & "'"
            SQL.ExecNonQuery(updateSQL)
            sortNo += 1
        Next
        LoadAccounts()
        Msg("Records Saved Successfully!", MsgBoxStyle.Information)
    End Sub

    Private Sub tsbUndo_Click(sender As System.Object, e As System.EventArgs) Handles tsbUndo.Click
        undoChanges()
    End Sub

    Private Sub tsbUp_Click(sender As System.Object, e As System.EventArgs) Handles tsbUp.Click
        MoveAccount("UP")
        tsbSave.Enabled = True
        tsbUndo.Enabled = True
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbDown.Click
        MoveAccount("DOWN")
        tsbSave.Enabled = True
        tsbUndo.Enabled = True
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        If cbType.SelectedIndex <> -1 Then
            LoadAccounts()
            tsbEdit.Enabled = False
            tsbSave.Enabled = False
            tsbUndo.Enabled = False
            tsbUp.Enabled = False
            tsbDown.Enabled = False
            tsbDelete.Enabled = False
        End If

    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("COA_Master", Date.Today)
        f.Dispose()
    End Sub

    Private Sub tsbExtract_Click(sender As System.Object, e As System.EventArgs) Handles tsbExtract.Click
        Dim dt As New DataTable
        Try
            If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

                dt = CreateTable()
                PopulateSheet(dt, FolderBrowserDialog1.SelectedPath & "\" & "ExtractedCOA" & Date.Today.Month.ToString("00") & Date.Today.Day.ToString("00") & Date.Today.Year.ToString("00") & Now.Hour.ToString("00") & Now.Minute.ToString("00") & ".xlsx")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dt.Dispose()
        End Try
    End Sub

    Public Function CreateTable() As DataTable
        Dim query As String
        query = "  SELECT AccountCode, AccountTitle, withSubsidiary, AccountType, " & _
                "AccountNature, AccountCategory, AccountGroup, OrderNo " & _
                "FROM tblCOA_Master"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            SQL.GetDataTable(query)
        End If
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

    Private Sub tsbDelete_Click(sender As System.Object, e As System.EventArgs) Handles tsbDelete.Click

        Dim updateSQL As String
        If dgvPreview.SelectedRows.Count = 1 Then
            If Not IfAccntCodeUsed(dgvPreview.SelectedRows(0).Cells(0).Value.ToString) Then
                If MsgBox("Are you sure you want to inactive this account?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    updateSQL = " UPDATE tblCOA_Master  " & _
                       " SET    Status = @Status" & _
                       " WHERE AccountCode = @AccountCode "
                    SQL.FlushParams()
                    SQL.AddParam("@Status", "Inactive")
                    SQL.AddParam("@AccountCode", dgvPreview.SelectedRows(0).Cells(0).Value.ToString)
                    SQL.ExecNonQuery(updateSQL)
                    LoadAccounts()
                End If
            Else
                MsgBox("This Account is already used!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub
End Class