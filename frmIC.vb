Imports System.IO
Imports Microsoft.Office.Interop
Public Class frmIC
    Dim TransID, RefID As String
    Dim ICNo As String
    Dim disableEvent As Boolean = False
    Dim FileName As String
    Dim ModuleID As String = "IC"
    Dim ColumnPK As String = "IC_No"
    Dim DBTable As String = "tblIC"
    Dim ColumnID As String = "TransID"
    Dim TransAuto As Boolean
    Dim tempWHSE As String = ""
    Dim ForApproval As Boolean = False

    Public Overloads Function ShowDialog(ByVal docnumber As String) As Boolean
        TransID = docnumber
        MyBase.ShowDialog()
        Return True
    End Function


    Private Sub frmIC_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            disableEvent = True
            dtpDocDate.Value = Date.Today.Date
            LoadWHSE()
            LoadItemCategory()
            LoadItemGroup()
            LoadItemType()
            disableEvent = False

            If TransID <> "" Then
                LoadIC(TransID)
            End If
            tsbSearch.Enabled = True
            tsbNew.Enabled = True
            tsbEdit.Enabled = False
            tsbSave.Enabled = False
            tsbCancel.Enabled = False
            tsbClose.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = True
            tsbDownload.Enabled = False
            tsbUpload.Enabled = False
            tsbPrint.Enabled = False
            EnableControl(False)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub



    Private Sub EnableControl(ByVal Value As Boolean)
        dgvItemList.AllowUserToAddRows = Value
        dgvItemList.AllowUserToDeleteRows = Value
        dgvItemList.ReadOnly = Not Value
        cbItemType.Enabled = Value
        cbItemGroup.Enabled = Value
        cbItemCategory.Enabled = Value
        cbWHSE.Enabled = Value
        If Value = True Then
            dgvItemList.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemList.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub


    Private Sub LoadIC(ByVal ID As String)
        Dim query As String
        query = " SELECT   TransID, IC_No, DateIC, ItemGroup, ItemCategory, ItemType, Remarks, tblIC.Status, " &
                "         CASE WHEN tblWarehouse.Description IS NOT NULL " &
                "              THEN tblIC.WHSE + ' | ' + tblWarehouse.Description " &
                "              ELSE 'Multiple Warehouse' " &
                "         END AS WHSE " &
                " FROM     tblIC LEFT JOIN tblWarehouse " &
                " ON       tblIC.WHSE = tblWarehouse.Code " &
                " AND      tblWarehouse.Status ='Active' " &
                " WHERE    TransId = '" & ID & "' " &
                " ORDER BY TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            ICNo = SQL.SQLDR("IC_No").ToString
            txtTransNum.Text = ICNo
            dtpDocDate.Text = SQL.SQLDR("DateIC").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            cbWHSE.SelectedItem = SQL.SQLDR("WHSE").ToString
            cbItemCategory.SelectedItem = SQL.SQLDR("ItemCategory").ToString
            cbItemType.SelectedItem = SQL.SQLDR("ItemType").ToString
            cbItemGroup.SelectedItem = SQL.SQLDR("ItemGroup").ToString

            LoadICDetails(TransID)
            dgvItemList.Visible = True
            dgvTemplate.Visible = False
            ' TOOLSTRIP BUTTONS
            If txtStatus.Text = "Cancelled" Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            Else
                tsbEdit.Enabled = True
                tsbCancel.Enabled = True
            End If
            tsbPrint.Enabled = True
            tsbClose.Enabled = False
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbPrint.Enabled = True
            tsbSave.Enabled = False
            tsbNew.Enabled = True
            tsbSearch.Enabled = True
            tsbExit.Enabled = True
            tsbDownload.Enabled = False
            tsbUpload.Enabled = False

            EnableControl(False)
        Else
            ClearText()
        End If
    End Sub


    Private Sub LoadICDetails(ByVal TransID As String)
        Dim ctr As Integer = 0
        Dim query As String
        query = " SELECT	tblIC_Details.ItemCode, tblIC_Details.Description, tblIC_Details.UOM, " &
                "           ISNULL(tblIC_Details.StockQTY,0) AS StockQTY,  PhysicalQTY, VarianceQTY " &
                " FROM	    tblIC INNER JOIN tblIC_Details " &
                " ON		tblIC.TransID = tblIC_Details.TransID " &
                " WHERE     tblIC_Details.TransId = " & TransID & " " &
                " ORDER BY  tblIC_Details.LineNum "
        disableEvent = True
        dgvItemList.Rows.Clear()
        disableEvent = False
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvItemList.Rows.Add(row(0).ToString, row(1).ToString, row(2).ToString,
                                                row(3).ToString, row(4).ToString, row(5).ToString)
                ctr += 1
            Next
        End If
    End Sub


    Private Sub txtVCEName_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.ShowDialog()
            f.Dispose()
        End If
    End Sub


    Private Sub ClearText()
        txtTransNum.Clear()
        dgvItemList.Rows.Clear()
        txtRemarks.Clear()
        If cbWHSE.Items.Count > 0 Then
            cbWHSE.SelectedIndex = 0
        End If
        cbItemType.SelectedIndex = 0
        cbItemGroup.SelectedIndex = 0
        cbItemCategory.SelectedIndex = 0
        txtStatus.Text = "Open"
        dtpDocDate.Value = Date.Today.Date
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("IC_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("IC")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadIC(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("IC_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            ICNo = ""
            disableEvent = True
            LoadItemCategory()
            LoadItemGroup()
            LoadItemType()
            disableEvent = False
            LoadWHSE()

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
            tsbPrint.Enabled = False
            tsbDownload.Enabled = True
            tsbUpload.Enabled = True
            EnableControl(True)

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
        End If
    End Sub

    Private Sub LoadItemGroup()
        Try
            cbItemGroup.Items.Clear()
            Dim query As String
            query = " SELECT DISTINCT ISNULL(ItemGroup,'') AS ItemGroup FROM tblItem_master  WHERE Status ='Active' AND ISNULL(ItemGroup,'')  <> '' "
            SQL.ReadQuery(query)
            SQL.FlushParams()
            cbItemGroup.Items.Add("ALL")
            While SQL.SQLDR.Read
                cbItemGroup.Items.Add(SQL.SQLDR("ItemGroup").ToString)
            End While
            cbItemGroup.SelectedItem = "ALL"
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub
    Private Sub LoadItemCategory()
        Try
            cbItemCategory.Items.Clear()
            Dim query As String
            query = " SELECT DISTINCT ISNULL(ItemCategory,'') AS ItemCategory FROM tblItem_master  WHERE Status ='Active' AND ISNULL(ItemCategory,'')  <> '' "
            SQL.ReadQuery(query)
            SQL.FlushParams()
            cbItemCategory.Items.Add("ALL")
            While SQL.SQLDR.Read
                cbItemCategory.Items.Add(SQL.SQLDR("ItemCategory").ToString)
            End While
            cbItemCategory.SelectedItem = "ALL"
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub
    Private Sub LoadItemType()
        Try
            cbItemType.Items.Clear()
            Dim query As String
            query = " SELECT DISTINCT ISNULL(ItemType,'') AS ItemType FROM tblItem_master  WHERE Status ='Active' AND ISNULL(ItemType,'')  <> '' "
            SQL.ReadQuery(query)
            SQL.FlushParams()
            cbItemType.Items.Add("ALL")
            While SQL.SQLDR.Read
                cbItemType.Items.Add(SQL.SQLDR("ItemType").ToString)
            End While
            cbItemType.SelectedItem = "ALL"
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub
    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("IC_EDIT") Then
            msgRestricted()
        Else
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
            tsbPrint.Enabled = False
            tsbDownload.Enabled = True
            tsbUpload.Enabled = True
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If ValidateDGV() Then
            If TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID(ColumnID, DBTable)
                    ICNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    txtTransNum.Text = ICNo
                    SaveIC()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    ICNo = txtTransNum.Text
                    LoadIC(TransID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    UpdateIC()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    ICNo = txtTransNum.Text
                    LoadIC(TransID)
                End If
            End If
        End If
    End Sub

    Private Function ValidateDGV() As Boolean
        If dgvItemList.Rows.Count = 0 Then
            MsgBox("There are no item on the list!", MsgBoxStyle.Exclamation)
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub SaveIC()
        Try
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " &
                        " tblIC(TransID, IC_No, BranchCode, BusinessCode, DateIC, WHSE, ItemType, ItemCategory,  " &
                        "       ItemGroup, Remarks, WhoCreated, DateCreated, Status) " &
                        " VALUES (@TransID, @IC_No, @BranchCode, @BusinessCode, @DateIC, @WHSE, @ItemType, @ItemCategory,  " &
                        "           @ItemGroup, @Remarks, @WhoCreated, GETDATE(), @Status) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@IC_No", ICNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateIC", dtpDocDate.Value.Date)
            SQL.AddParam("@WHSE", tempWHSE)
            SQL.AddParam("@ItemType", cbItemType.SelectedItem)
            SQL.AddParam("@ItemCategory", cbItemCategory.SelectedItem)
            SQL.AddParam("@ItemGroup", cbItemGroup.SelectedItem)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            Dim ItemCode, Description, UOM As String
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chPhysicalCount.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                    Description = IIf(row.Cells(chItemDesc.Index).Value = Nothing, "", row.Cells(chItemDesc.Index).Value)
                    UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                    insertSQL = " INSERT INTO " &
                         " tblIC_Details(TransId, ItemCode, Description, UOM, StockQTY, PhysicalQTY, VarianceQTY, LineNum, WhoCreated, DateCreated) " &
                         " VALUES(@TransId, @ItemCode, @Description, @UOM, @StockQTY, @PhysicalQTY, @VarianceQTY, @LineNum, @WhoCreated, GETDATE()) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", ItemCode)
                    SQL.AddParam("@Description", Description)
                    SQL.AddParam("@UOM", UOM)
                    If IsNumeric(row.Cells(chStockQTY.Index).Value) Then SQL.AddParam("@StockQTY", CDec(row.Cells(chStockQTY.Index).Value)) Else SQL.AddParam("@StockQTY", 0)
                    If IsNumeric(row.Cells(chPhysicalCount.Index).Value) Then SQL.AddParam("@PhysicalQTY", CDec(row.Cells(chPhysicalCount.Index).Value)) Else SQL.AddParam("@PhysicalQTY", 0)
                    If IsNumeric(row.Cells(chVariance.Index).Value) Then SQL.AddParam("@VarianceQTY", CDec(row.Cells(chVariance.Index).Value)) Else SQL.AddParam("@VarianceQTY", 0)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next

        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "IC_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub UpdateIC()
        Try

            activityStatus = True
            Dim insertSQL, updateSQL, deleteSQL As String
            updateSQL = " UPDATE    tblIC " &
                        " SET       IC_No = @IC_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, DateIC = @DateIC, " &
                        "           WHSE = @WHSE, ItemType = @ItemType, ItemCategory = @ItemCategory, ItemGroup = @ItemGroup, " &
                        "           Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE() " &
                        " WHERE     TransID = @TransID  "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@IC_No", ICNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateIC", dtpDocDate.Value.Date)
            SQL.AddParam("@WHSE", tempWHSE)
            SQL.AddParam("@ItemType", cbItemType.SelectedItem)
            SQL.AddParam("@ItemCategory", cbItemCategory.SelectedItem)
            SQL.AddParam("@ItemGroup", cbItemGroup.SelectedItem)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)

            deleteSQL = " DELETE FROM tblIC_Details WHERE TransID =@TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.ExecNonQuery(deleteSQL)

            Dim line As Integer = 1
            Dim ItemCode, Description, UOM As String
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chPhysicalCount.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                    Description = IIf(row.Cells(chItemDesc.Index).Value = Nothing, "", row.Cells(chItemDesc.Index).Value)
                    UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                    insertSQL = " INSERT INTO " &
                         " tblIC_Details(TransId, ItemCode, Description, UOM, StockQTY, PhysicalQTY, VarianceQTY, LineNum, WhoCreated, DateCreated) " &
                         " VALUES(@TransId, @ItemCode, @Description, @UOM, @StockQTY, @PhysicalQTY, @VarianceQTY, @LineNum, @WhoCreated, GETDATE()) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", ItemCode)
                    SQL.AddParam("@Description", Description)
                    SQL.AddParam("@UOM", UOM)
                    If IsNumeric(row.Cells(chStockQTY.Index).Value) Then SQL.AddParam("@StockQTY", CDec(row.Cells(chStockQTY.Index).Value)) Else SQL.AddParam("@StockQTY", 0)
                    If IsNumeric(row.Cells(chPhysicalCount.Index).Value) Then SQL.AddParam("@PhysicalQTY", CDec(row.Cells(chPhysicalCount.Index).Value)) Else SQL.AddParam("@PhysicalQTY", 0)
                    If IsNumeric(row.Cells(chVariance.Index).Value) Then SQL.AddParam("@VarianceQTY", CDec(row.Cells(chVariance.Index).Value)) Else SQL.AddParam("@VarianceQTY", 0)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "RR_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("IC_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL, updateSQL As String
                        deleteSQL = " UPDATE  tblIC SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        ICNo = txtTransNum.Text
                        SQL.AddParam("@TransID", TransID)
                        SQL.ExecNonQuery(deleteSQL)

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

                        Msg("Record cancelled successfully", MsgBoxStyle.Information)
                        ICNo = txtTransNum.Text
                        LoadIC(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "IC_No", ICNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If ICNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblIC  " &
                    " LEFT JOIN tblWarehouse  ON	           " &
                    " tblIC.WHSE = tblWarehouse.Code      " &
                    " WHERE            " &
                    " WHSE IN  " &
                    " (SELECT    DISTINCT tblWarehouse.Code    " &
                    " FROM      tblWarehouse     " &
                    " INNER JOIN tblUser_Access    ON           " &
                    " tblWarehouse.Code = tblUser_Access.Code      " &
                    " AND       tblUser_Access.Status ='Active'  " &
                    " AND tblWarehouse.Status ='Active'      " &
                    " AND       tblUser_Access.Type = 'Warehouse'  " &
                    " AND isAllowed = 1    WHERE     UserID ='" & UserID & "')   " &
                    " AND IC_No < '" & ICNo & "' ORDER BY IC_No DESC  "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadIC(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If ICNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblIC  " &
                    " LEFT JOIN tblWarehouse  ON	           " &
                    " tblIC.WHSE = tblWarehouse.Code      " &
                    " WHERE            " &
                    " ( WHSE IN  " &
                    " (SELECT    DISTINCT tblWarehouse.Code    " &
                    " FROM      tblWarehouse     " &
                    " INNER JOIN tblUser_Access    ON           " &
                    " tblWarehouse.Code = tblUser_Access.Code      " &
                    " AND       tblUser_Access.Status ='Active'  " &
                    " AND tblWarehouse.Status ='Active'      " &
                    " AND       tblUser_Access.Type = 'Warehouse'  " &
                    " AND isAllowed = 1    WHERE     UserID ='" & UserID & "')                    " &
                    " OR     " &
                    " WHSE = 'MW')  AND IC_No > '" & ICNo & "' ORDER BY IC_No ASC  "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadIC(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        ' Toolstrip Buttons
        If ICNo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadIC(TransID)
            tsbEdit.Enabled = True
            tsbCancel.Enabled = True
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbPrint.Enabled = True
        End If
        tsbSearch.Enabled = True
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub


    Private Sub frmRR_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Right Then
                If tsbNext.Enabled = True Then tsbNext.PerformClick()
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.P Then
                If tsbPrint.Enabled = True Then tsbPrint.PerformClick()
            ElseIf e.KeyCode = Keys.R Then
                If tsbReports.Enabled = True Then tsbReports.ShowDropDown()
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

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("IC", TransID)
        f.Dispose()
    End Sub

    Private Sub dgvItemList_EditingControlShowing(sender As System.Object, e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvItemList.EditingControlShowing
        ' GET THE EDITING CONTROL
        Dim editingComboBox As ComboBox = TryCast(e.Control, ComboBox)

        If Not editingComboBox Is Nothing Then
            ' REMOVE AN EXISTING EVENT-HANDLER TO AVOID ADDING MULTIPLE HANDLERS WHEN THE EDITING CONTROL IS REUSED
            RemoveHandler editingComboBox.SelectionChangeCommitted, New EventHandler(AddressOf editingComboBox_SelectionChangeCommitted)

            ' ADD THE EVENT HANDLER
            AddHandler editingComboBox.SelectionChangeCommitted, AddressOf editingComboBox_SelectionChangeCommitted

            ' PREVENT THIS HANDLER FROM FIRING TWICE
            RemoveHandler dgvItemList.EditingControlShowing, AddressOf dgvItemList_EditingControlShowing
        End If
    End Sub

    Private Sub editingComboBox_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rowIndex, columnIndex As Integer
        'Get the editing control
        Dim editingComboBox As ComboBox = TryCast(sender, ComboBox)
        If editingComboBox Is Nothing Then Exit Sub
        'Show your Message Boxes
        If editingComboBox.SelectedIndex <> -1 Then
            rowIndex = dgvItemList.SelectedCells(0).RowIndex
            columnIndex = dgvItemList.SelectedCells(0).ColumnIndex
            If dgvItemList.SelectedCells.Count > 0 Then
                Dim tempCell As DataGridViewComboBoxCell = dgvItemList.Item(columnIndex, rowIndex)
                Dim temp As String = editingComboBox.Text
                dgvItemList.Item(columnIndex, rowIndex).Selected = False
                dgvItemList.EndEdit(True)
                tempCell.Value = temp
            End If
        End If

        'Remove the handle to this event. It will be readded each time a new combobox selection causes the EditingControlShowing Event to fire
        RemoveHandler editingComboBox.SelectionChangeCommitted, AddressOf editingComboBox_SelectionChangeCommitted
        'Re-enable the EditingControlShowing event so the above can take place.
        AddHandler dgvItemList.EditingControlShowing, AddressOf dgvItemList_EditingControlShowing
    End Sub

    Private Sub dgvItemList_DataError(sender As System.Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvItemList.DataError
        Try

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub TestToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles TestToolStripMenuItem1.Click
        Dim f As New frmReport_Filter
        f.Report = "RR List"
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub dgvGroup_DataError(sender As System.Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs)
        Try

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub LoadWHSE(Optional ByVal SelectedIndex As Integer = -1)
        Try
            Dim query As String
            query = " SELECT tblWarehouse.Code + ' | ' + Description AS WHSECode " &
                    " FROM tblWarehouse INNER JOIN tblUser_Access " &
                    " ON tblWarehouse.Code = tblUser_Access.Code " &
                    " AND tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active' " &
                    " AND Type = 'Warehouse' AND isAllowed = 1 " &
                    " WHERE UserID ='" & UserID & "' "
            SQL.ReadQuery(query)
            cbWHSE.Items.Clear()
            While SQL.SQLDR.Read
                cbWHSE.Items.Add(SQL.SQLDR("WHSECode").ToString)
            End While
            If cbWHSE.Items.Count > 0 Then
                cbWHSE.SelectedIndex = 0
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub tsbDownload_Click(sender As Object, e As EventArgs) Handles tsbDownload.Click
        Dim templateName As String = "INVENTORY_COUNT.xlsx"
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim firstRow As Integer = 7
        Dim ctr As Integer = 0
        Dim SQL As New SQLControl
        Dim query As String
        query = "spInventoryCount"
        xlApp = New Excel.Application
        Dim fileSuffix As String = (CDate(Date.Now).ToString("MMddYYYhhmmss"))
        SQL.FlushParams()
        SQL.AddParam("@WHSE", tempWHSE)
        SQL.AddParam("@ItemGroup", cbItemGroup.SelectedItem)
        SQL.AddParam("@ItemCategory", cbItemCategory.SelectedItem)
        SQL.AddParam("@ItemType", cbItemType.SelectedItem)
        SQL.AddParam("@AsOfDate", dtpDocDate.Value.Date)
        SQL.GetQuery(query)
        Dim dt As DataTable = SQL.SQLDS.Tables(0)
        If dt.Rows.Count > 0 Then
            App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName & "\Templates"
            If My.Computer.FileSystem.FileExists(App_Path + "\" & templateName) Then
                xlWorkBook = xlApp.Workbooks.Open(App_Path + "\" & templateName)
                xlWorkSheet = xlWorkBook.Worksheets("Template")
                xlWorkSheet.Unprotect("@dm1nEvo")
                xlWorkSheet.Cells(1, 2) = cbWHSE.SelectedItem
                xlWorkSheet.Cells(2, 2) = cbItemType.SelectedItem
                xlWorkSheet.Cells(3, 2) = cbItemCategory.SelectedItem
                xlWorkSheet.Cells(4, 2) = cbItemGroup.SelectedItem
                xlWorkSheet.Cells(5, 2) = dtpDocDate.Value.Date
                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        xlWorkSheet.Cells(i + firstRow, j + 1) = dt.Rows(i).Item(j)
                    Next
                    ctr += 1
                    xlWorkSheet.Rows(firstRow + 1 + ctr).Insert
                Next
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

    Private Sub cbWHSE_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbWHSE.SelectedIndexChanged
        If disableEvent = False Then
            If cbWHSE.SelectedIndex <> -1 Then
                tempWHSE = Strings.Left(cbWHSE.SelectedItem, cbWHSE.SelectedItem.ToString.IndexOf(" | "))
                LoadItems()
            End If
        End If
    End Sub

    Private Sub cbItemType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbItemType.SelectedIndexChanged
        If disableEvent = False Then
            LoadItems()
        End If
    End Sub

    Private Sub cbItemCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbItemCategory.SelectedIndexChanged
        If disableEvent = False Then
            LoadItems()
        End If
    End Sub

    Private Sub cbItemGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbItemGroup.SelectedIndexChanged
        If disableEvent = False Then
            LoadItems()
        End If
    End Sub

    Private Sub tsbUpload_Click(sender As Object, e As EventArgs) Handles tsbUpload.Click
        Dim OpenFileDialog As New OpenFileDialog
        Dim firstRow As Integer
        Dim ctr As Integer = 1
        Dim str As String
        Dim objExcel As New Microsoft.Office.Interop.Excel.Application
        dgvTemplate.Visible = False
        dgvItemList.Visible = True
        dgvItemList.Rows.Clear()

        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            FileName = OpenFileDialog.FileName
            objExcel.Workbooks.Open(FileName)
            str = "a"
            firstRow = 7
            disableEvent = True

            Do While str <> ""
                Dim ItemCode, ItemName, UOM As String
                Dim StockQTY As Decimal = 0
                Dim PhysicalQTY As Decimal = 0
                If ctr = 1 Then
                    cbWHSE.SelectedItem = RTrim(objExcel.Range("b" & CStr(ctr)).Value)
                ElseIf ctr = 2 Then
                    cbItemType.SelectedItem = RTrim(objExcel.Range("b" & CStr(ctr)).Value)
                ElseIf ctr = 3 Then
                    cbItemCategory.SelectedItem = RTrim(objExcel.Range("b" & CStr(ctr)).Value)
                ElseIf ctr = 4 Then
                    cbItemGroup.SelectedItem = RTrim(objExcel.Range("b" & CStr(ctr)).Value)
                ElseIf ctr = 5 Then
                    dtpDocDate.Value = RTrim(objExcel.Range("b" & CStr(ctr)).Value)
                ElseIf ctr = 6 Then
                Else
                    ItemCode = RTrim(objExcel.Range("a" & CStr(firstRow)).Value)
                    ItemName = RTrim(objExcel.Range("b" & CStr(firstRow)).Value)
                    UOM = RTrim(objExcel.Range("c" & CStr(firstRow)).Value)
                    StockQTY = RTrim(objExcel.Range("d" & CStr(firstRow)).Value)
                    If Not IsNumeric(RTrim(objExcel.Range("e" & CStr(firstRow)).Value)) Then
                        PhysicalQTY = 0
                    Else
                        PhysicalQTY = RTrim(objExcel.Range("e" & CStr(firstRow)).Value)
                    End If

                    dgvItemList.Rows.Add(New String() {
                                         ItemCode.ToString,
                                        ItemName.ToString,
                                        UOM.ToString,
                                        StockQTY.ToString,
                                        PhysicalQTY.ToString, PhysicalQTY - StockQTY})
                    firstRow += 1
                End If
                ctr += 1
                str = RTrim(objExcel.Range("a" & CStr(ctr)).Value)
            Loop
            disableEvent = False
            objExcel.Workbooks.Close()
            ValidateDGV()
            tsbSave.Enabled = True
        End If
    End Sub

    Private Sub dtpDocDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpDocDate.ValueChanged
        If disableEvent = False Then
            LoadItems()
        End If
    End Sub

    Private Sub LoadItems()
        dgvTemplate.DataSource = Nothing
        Dim SQL As New SQLControl
        Dim query As String
        query = "spInventoryCount"
        SQL.FlushParams()
        SQL.AddParam("@WHSE", tempWHSE)
        SQL.AddParam("@ItemType", cbItemType.SelectedItem)
        SQL.AddParam("@ItemCategory", cbItemCategory.SelectedItem)
        SQL.AddParam("@ItemGroup", cbItemGroup.SelectedItem)
        SQL.AddParam("@AsOfDate", dtpDocDate.Value)
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvTemplate.DataSource = SQL.SQLDS.Tables(0)
            dgvTemplate.Visible = True
            dgvItemList.Visible = False
            dgvTemplate.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            tsbSave.Enabled = False
        Else
            MsgBox("No Record Found!", MsgBoxStyle.Exclamation)
        End If
    End Sub

End Class