Public Class frmBOM_FG
    Dim Code As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "BOMFG"
    Dim TransAuto As Boolean
    Dim objExcel As New Microsoft.Office.Interop.Excel.Application
    Dim BOM_FG As String
    Dim BOM_FG_ItemCode

#Region "VERIFIED"
    Private Sub frmBOM_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        tsbSearch.Enabled = True
        tsbNew.Enabled = True
        tsbEdit.Enabled = False
        tsbSave.Enabled = False
        tsbCancel.Enabled = False
        tsbClose.Enabled = False
        tsbPrevious.Enabled = False
        tsbNext.Enabled = False
        tsbExit.Enabled = True
        tsbUpload.Enabled = False
        EnableControl(False)
    End Sub

    Private Sub ClearText()
        txtCode.Clear()
        txtItemCode.Clear()
        txtItemName.Clear()
        txtDescription.Clear()
        txtQTY.Clear()
        cbUOM.SelectedIndex = -1
        dgvItemMaster.Rows.Clear()
        dgvLabor.Rows.Clear()
        dgvOverhead.Rows.Clear()

        txtDLcost.Text = "0.00"
        txtDMcost.Text = "0.00"
        txtFOcost.Text = "0.00"
        txtTotalCost.Text = "0.00"
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        dgvItemMaster.AllowUserToAddRows = Value
        dgvLabor.AllowUserToAddRows = Value
        dgvOverhead.AllowUserToAddRows = Value
        dgvItemMaster.AllowUserToDeleteRows = Value
        dgvOverhead.AllowUserToDeleteRows = Value
        dgvLabor.AllowUserToDeleteRows = Value
        If Value = True Then
            dgvItemMaster.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
            dgvLabor.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
            dgvOverhead.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemMaster.EditMode = DataGridViewEditMode.EditProgrammatically
            dgvLabor.EditMode = DataGridViewEditMode.EditProgrammatically
            dgvOverhead.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        txtCode.Enabled = False
        txtItemName.Enabled = Value
        txtQTY.Enabled = Value
        txtDescription.Enabled = Value
        cbUOM.Enabled = Value
        dgvItemMaster.Enabled = Value
    End Sub

    Private Sub txtItemName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtItemName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmCopyFrom
            f.ShowDialog("All Item", txtItemName.Text)
            txtItemCode.Text = f.TransID
            txtItemName.Text = GetItemName(txtItemCode.Text)
            LoadUOM(txtItemCode.Text)
            txtQTY.Select()
            f.Dispose()
        End If
    End Sub


#End Region
   

    
    Private Sub LoadUOM(ByVal ItemCode As String)
        Dim query As String
        query = " SELECT viewItem_UOM.UnitCode " & _
                " FROM   tblItem_Master INNER JOIN viewItem_UOM " & _
                " ON     tblItem_Master.ItemUOMGroup = viewItem_UOM.GroupCode OR tblItem_Master.ItemCode = viewItem_UOM.GroupCode   " & _
                " WHERE  ItemCode ='" & ItemCode & "' "
        SQL.ReadQuery(query)
        cbUOM.Items.Clear()
        While SQL.SQLDR.Read
            cbUOM.Items.Add(SQL.SQLDR("UnitCode").ToString)
        End While
        If cbUOM.Items.Count > 0 Then
            cbUOM.SelectedIndex = 0
        End If
    End Sub

    Private Sub LoadItemDetails(ByVal ItemCode As String, Optional ByVal upload As Boolean = False)
        Dim query As String
        query = " SELECT    ItemCategory, ItemGroup, ItemCode, ItemName, ItemUOM, 1, ISNULL(ID_SC,0) AS ID_SC " & _
                " FROM      tblItem_Master " & _
                " WHERE     ItemCode = @ItemCode"
        SQL.FlushParams()
        SQL.AddParam("@ItemCode", IIf(ItemCode = Nothing, "", ItemCode))
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            If upload = False Then
                If dgvItemMaster.SelectedCells.Count > 0 Then
                    dgvItemMaster.Item(0, dgvItemMaster.SelectedCells(0).RowIndex).Value = ""
                    dgvItemMaster.Item(1, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemCategory").ToString
                    dgvItemMaster.Item(2, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemGroup").ToString
                    dgvItemMaster.Item(3, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemCode").ToString
                    dgvItemMaster.Item(4, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemName").ToString
                    dgvItemMaster.Item(5, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemUOM").ToString
                    dgvItemMaster.Item(6, dgvItemMaster.SelectedCells(0).RowIndex).Value = 1
                    dgvItemMaster.Item(7, dgvItemMaster.SelectedCells(0).RowIndex).Value = CDec(SQL.SQLDR("ID_SC").ToString).ToString("N2")
                    LoadUOM(SQL.SQLDR("ItemCode").ToString, dgvItemMaster.SelectedCells(0).RowIndex)
                End If
            Else
                dgvItemMaster.Rows.Add({"", SQL.SQLDR("ItemCategory").ToString, SQL.SQLDR("ItemGroup").ToString, SQL.SQLDR("ItemCode").ToString, SQL.SQLDR("ItemName").ToString, "", 1})
                LoadUOM(SQL.SQLDR("ItemCode").ToString, dgvItemMaster.Rows.Count - 2)
            End If
      
        End If
    End Sub

    Private Sub dgvItemMaster_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemMaster.CellEndEdit
        Dim itemCode As String
        Dim rowIndex As Integer = dgvItemMaster.CurrentCell.RowIndex
        Dim colindex As Integer = dgvItemMaster.CurrentCell.ColumnIndex
        Select Case colindex
            Case chBOMItemCode.Index
                If dgvItemMaster.Item(chBOMItemCode.Index, e.RowIndex).Value <> "" Then
                    itemCode = dgvItemMaster.Item(chBOMItemCode.Index, e.RowIndex).Value
                    Dim f As New frmCopyFrom
                    f.ShowDialog("All Item", itemCode)
                    If f.TransID <> "" Then
                        itemCode = f.TransID
                        LoadItemDetails(itemCode)
                        ComputeTotals()
                    Else
                        dgvItemMaster.Rows.RemoveAt(e.RowIndex)
                    End If
                    f.Dispose()
                End If
            Case chBOMItemName.Index
                If dgvItemMaster.Item(chBOMItemName.Index, e.RowIndex).Value <> "" Then
                    itemCode = dgvItemMaster.Item(chBOMItemName.Index, e.RowIndex).Value
                    Dim f As New frmCopyFrom
                    f.ShowDialog("All Item", itemCode)
                    If f.TransID <> "" Then
                        itemCode = f.TransID
                        LoadItemDetails(itemCode)
                        ComputeTotals()
                    Else
                        dgvItemMaster.Rows.RemoveAt(e.RowIndex)
                    End If
                    f.Dispose()
                End If
            Case dgcItemGroup.Index
                If dgvItemMaster.Item(dgcItemGroup.Index, e.RowIndex).Value <> "" Then
                    itemCode = dgvItemMaster.Item(dgcItemGroup.Index, e.RowIndex).Value
                    Dim f As New frmCopyFrom
                    f.ShowDialog("ItemGroup", itemCode)
                    If f.TransID <> "" Then
                        itemCode = f.TransID
                        dgvItemMaster.Item(0, dgvItemMaster.SelectedCells(0).RowIndex).Value = ""
                        dgvItemMaster.Item(1, dgvItemMaster.SelectedCells(0).RowIndex).Value = ""
                        dgvItemMaster.Item(2, dgvItemMaster.SelectedCells(0).RowIndex).Value = itemCode
                        dgvItemMaster.Item(3, dgvItemMaster.SelectedCells(0).RowIndex).Value = ""
                        dgvItemMaster.Item(4, dgvItemMaster.SelectedCells(0).RowIndex).Value = ""
                        dgvItemMaster.Item(5, dgvItemMaster.SelectedCells(0).RowIndex).Value = f.ItemCode
                        dgvItemMaster.Item(6, dgvItemMaster.SelectedCells(0).RowIndex).Value = 1
                        LoadUOM("", dgvItemMaster.SelectedCells(0).RowIndex, itemCode)
                    Else
                        dgvItemMaster.Rows.RemoveAt(e.RowIndex)
                    End If
                    f.Dispose()
                End If
            Case chBOMQTY.Index
                ComputeTotals()
        End Select
    End Sub

    Public Sub SaveHeader(ByVal Code As String)
        Try
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblBOM_FG(BOM_Code, ItemCode, UOM, QTY, Remarks, WhoCreated, " & _
                        " TotalMaterialCost, TotalDLCost, TotalFOCost, TotalCost) " & _
                        " VALUES (@BOM_Code,  @ItemCode,  @UOM, @QTY, @Remarks, @WhoCreated, " & _
                        " @TotalMaterialCost, @TotalDLCost, @TotalFOCost, @TotalCost) "
            SQL.FlushParams()
            SQL.AddParam("@BOM_Code", Code)
            SQL.AddParam("@ItemCode", txtItemCode.Text)
            SQL.AddParam("@UOM", cbUOM.SelectedItem)
            SQL.AddParam("@QTY", IIf(Not IsNumeric(txtQTY.Text), 0, CDec(txtQTY.Text)))
            SQL.AddParam("@Remarks", txtDescription.Text)
            SQL.AddParam("@TotalMaterialCost", CDec(txtDMcost.Text))
            SQL.AddParam("@TotalDLCost", CDec(txtDLcost.Text))
            SQL.AddParam("@TotalFOCost", CDec(txtFOcost.Text))
            SQL.AddParam("@TotalCost", CDec(txtTotalCost.Text))
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)
            SaveDetails(Code)
            SaveLabor(Code)
            SaveOverhead(Code)
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "BOM_Code", Code, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Public Sub UpdateHeader(ByVal Code As String)
        Try
            Dim insertSQL As String
            insertSQL = " UPDATE tblBOM_FG " & _
                        " SET    ItemCode = @ItemCode, UOM = @UOM, QTY = @QTY, Remarks = @Remarks, WhoModified = @WhoModified, " & _
                        "   TotalMaterialCost = @TotalMaterialCost, TotalDLCost = @TotalDLCost, TotalFOCost = @TotalFOCost, TotalCost = @TotalCost" & _
                        " WHERE  BOM_Code = @BOM_Code "
            SQL.FlushParams()
            SQL.AddParam("@BOM_Code", Code)
            SQL.AddParam("@ItemCode", txtItemCode.Text)
            SQL.AddParam("@UOM", cbUOM.SelectedItem)
            SQL.AddParam("@QTY", IIf(Not IsNumeric(txtQTY.Text), 0, CDec(txtQTY.Text)))
            SQL.AddParam("@Remarks", txtDescription.Text)
            SQL.AddParam("@TotalMaterialCost", CDec(txtDMcost.Text))
            SQL.AddParam("@TotalDLCost", CDec(txtDLcost.Text))
            SQL.AddParam("@TotalFOCost", CDec(txtFOcost.Text))
            SQL.AddParam("@TotalCost", CDec(txtTotalCost.Text))
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(insertSQL)

            SaveDetails(Code)
            SaveLabor(Code)
            SaveOverhead(Code)
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "BOM_Code", Code, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub SaveDetails(ByVal Code As String)
        Dim deleteSQL As String
        deleteSQL = " DELETE FROM tblBOM_FG_Details WHERE BOM_Code = '" & Code & "' "
        SQL.ExecNonQuery(deleteSQL)

        Dim insertSQL As String
        Dim line As Integer = 1
        For Each row As DataGridViewRow In dgvItemMaster.Rows
            If Not row.Cells(chBOMItemCode.Index).Value = Nothing Or Not row.Cells(dgcItemGroup.Index).Value = Nothing Then

                insertSQL = " INSERT INTO " & _
                            " tblBOM_FG_Details(BOM_Code, BOM_Group, MaterialCode, UOM, QTY, UnitCost, TotalCost, LineNumber) " & _
                            " VALUES (@BOM_Code, @BOM_Group, @MaterialCode, @UOM, @QTY, @UnitCost, @TotalCost, @LineNumber) "
                SQL.FlushParams()
                SQL.AddParam("@BOM_Code", Code)
                SQL.AddParam("@BOM_Group", IIf(row.Cells(dgcItemGroup.Index).Value = Nothing, "", row.Cells(dgcItemGroup.Index).Value))
                SQL.AddParam("@MaterialCode", IIf(row.Cells(chBOMItemCode.Index).Value = Nothing, "", row.Cells(chBOMItemCode.Index).Value))
                SQL.AddParam("@UOM", IIf(row.Cells(chBOMUOM.Index).Value = Nothing, "", row.Cells(chBOMUOM.Index).Value))
                SQL.AddParam("@QTY", IIf(row.Cells(chBOMQTY.Index).Value = Nothing, 0, CDec(row.Cells(chBOMQTY.Index).Value)))
                SQL.AddParam("@UnitCost", IIf(row.Cells(chCost.Index).Value = Nothing, 0, CDec(row.Cells(chCost.Index).Value)))
                SQL.AddParam("@TotalCost", IIf(row.Cells(chTotalCost.Index).Value = Nothing, 0, CDec(row.Cells(chTotalCost.Index).Value)))
                SQL.AddParam("@LineNumber", line)
                SQL.ExecNonQuery(insertSQL)
                line += 1
            End If
        Next
    End Sub

    Private Sub SaveLabor(ByVal Code As String)
        Dim deleteSQL As String
        deleteSQL = " DELETE FROM tblBOM_Labor WHERE BOMCode = '" & Code & "' "
        SQL.ExecNonQuery(deleteSQL)

        Dim insertSQL As String
        Dim line As Integer = 1
        For Each row As DataGridViewRow In dgvLabor.Rows
            If Not row.Cells(dgcDLactivity.Index).Value = Nothing Then

                insertSQL = " INSERT INTO " & _
                            " tblBOM_Labor(BOMCode, Activity, RatePerHour, CrewNum, TimeMins, TotalMins, TotalCost, WhoCreated, LineNumber) " & _
                            " VALUES (@BOMCode, @Activity, @RatePerHour, @CrewNum, @TimeMins, @TotalMins, @TotalCost, @WhoCreated, @LineNumber) "
                SQL.FlushParams()
                SQL.AddParam("@BOMCode", Code)
                SQL.AddParam("@Activity", IIf(row.Cells(dgcDLactivity.Index).Value = Nothing, "", row.Cells(dgcDLactivity.Index).Value))
                SQL.AddParam("@RatePerHour", IIf(row.Cells(dgcDLrate.Index).Value = Nothing, 0, row.Cells(dgcDLrate.Index).Value))
                SQL.AddParam("@CrewNum", IIf(row.Cells(dgcDLcrewNo.Index).Value = Nothing, 0, row.Cells(dgcDLcrewNo.Index).Value))
                SQL.AddParam("@TimeMins", IIf(row.Cells(dgcDLtime.Index).Value = Nothing, 0, row.Cells(dgcDLtime.Index).Value))
                SQL.AddParam("@TotalMins", IIf(row.Cells(dgcDLTotalMins.Index).Value = Nothing, 0, row.Cells(dgcDLTotalMins.Index).Value))
                SQL.AddParam("@TotalCost", IIf(row.Cells(dgcDLtotalCost.Index).Value = Nothing, 0, row.Cells(dgcDLtotalCost.Index).Value))
                SQL.AddParam("@WhoCreated", UserID)
                SQL.AddParam("@LineNumber", line)
                SQL.ExecNonQuery(insertSQL)
                line += 1
            End If
        Next
    End Sub

    Private Sub SaveOverhead(ByVal Code As String)
        Dim deleteSQL As String
        deleteSQL = " DELETE FROM tblBOM_Overhead WHERE BOMCode = '" & Code & "' "
        SQL.ExecNonQuery(deleteSQL)

        Dim insertSQL As String
        Dim line As Integer = 1
        For Each row As DataGridViewRow In dgvOverhead.Rows
            If Not row.Cells(dgcFOactivity.Index).Value = Nothing Then
                insertSQL = " INSERT INTO " & _
                            " tblBOM_Overhead(BOMCode, Activity, Machine, RatePerHour, KW, NumHours, TotalKWH, TotalCost, WhoCreated, LineNumber) " & _
                            " VALUES (@BOMCode, @Activity, @Machine, @RatePerHour, @KW, @NumHours, @TotalKWH, @TotalCost, @WhoCreated, @LineNumber) "
                SQL.FlushParams()
                SQL.AddParam("@BOMCode", Code)
                SQL.AddParam("@Activity", IIf(row.Cells(dgcFOactivity.Index).Value = Nothing, "", row.Cells(dgcFOactivity.Index).Value))
                SQL.AddParam("@Machine", IIf(row.Cells(dgcFOmachine.Index).Value = Nothing, "", row.Cells(dgcFOmachine.Index).Value))
                SQL.AddParam("@RatePerHour", IIf(row.Cells(dgcFOrate.Index).Value = Nothing, 0, row.Cells(dgcFOrate.Index).Value))
                SQL.AddParam("@KW", IIf(row.Cells(dgcFOKW.Index).Value = Nothing, 0, row.Cells(dgcFOKW.Index).Value))
                SQL.AddParam("@NumHours", IIf(row.Cells(dgcFOhrs.Index).Value = Nothing, 0, row.Cells(dgcFOhrs.Index).Value))
                SQL.AddParam("@TotalKWH", IIf(row.Cells(dgcFOtotalKWH.Index).Value = Nothing, 0, row.Cells(dgcFOtotalKWH.Index).Value))
                SQL.AddParam("@TotalCost", IIf(row.Cells(dgcFOcost.Index).Value = Nothing, 0, row.Cells(dgcFOcost.Index).Value))
                SQL.AddParam("@WhoCreated", UserID)
                SQL.AddParam("@LineNumber", line)
                SQL.ExecNonQuery(insertSQL)
                line += 1
            End If
        Next
    End Sub

    Private Function GenerateCode() As String
        Dim query As String
        query = " SELECT   LEFT('BOM',3) + Cast(Max(Cast(REPLACE(BOM_Code, LEFT(BOM_Code,3), '')as int)+1) as nvarchar) AS BOM_Code " & _
                " FROM     tblBOM_FG "
        SQL.FlushParams()
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read AndAlso Not IsDBNull(SQL.SQLDR("BOM_Code")) Then
            Return SQL.SQLDR("BOM_Code").ToString
        Else
            Return "BOM1"
        End If
    End Function




    Public Sub LoadHeader(ByVal Code As String)
        Dim query As String
        query = " SELECT BOM_Code, tblBOM_FG.ItemCode, ItemName, tblBOM_FG.UOM, tblBOM_FG.QTY, tblBOM_FG.Remarks, " & _
                "   ISNULL(TotalMaterialCost,0) AS TotalMaterialCost, ISNULL(TotalDLCost,0) AS  TotalDLCost, ISNULL(TotalFOCost,0) AS TotalFOCost,ISNULL(TotalCost,0) AS  TotalCost" & _
                " FROM   tblBOM_FG INNER JOIN tblItem_Master " & _
                " ON     tblBOM_FG.ItemCode = tblItem_Master.ItemCode " & _
                " AND    tblItem_Master.Status ='Active' " & _
                " WHERE  tblBOM_FG.Status ='Active'" & _
                " AND    BOM_Code = '" & Code & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() Then
            Dim uom As String
            txtCode.Text = SQL.SQLDR("BOM_Code").ToString
            txtItemCode.Text = SQL.SQLDR("ItemCode").ToString
            txtItemName.Text = SQL.SQLDR("ItemName").ToString
            uom = SQL.SQLDR("UOM").ToString
            txtQTY.Text = SQL.SQLDR("QTY").ToString
            txtDescription.Text = SQL.SQLDR("Remarks").ToString
            txtDMcost.Text = CDec(SQL.SQLDR("TotalMaterialCost").ToString).ToString("N2")
            txtDLcost.Text = CDec(SQL.SQLDR("TotalDLCost").ToString).ToString("N2")
            txtFOcost.Text = CDec(SQL.SQLDR("TotalFOCost").ToString).ToString("N2")
            txtTotalCost.Text = CDec(SQL.SQLDR("TotalCost").ToString).ToString("N2")
            LoadUOM(txtItemCode.Text)
            cbUOM.SelectedItem = uom

            tsbClose.Enabled = False
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbEdit.Enabled = True
            tsbSave.Enabled = False
            tsbNew.Enabled = True
            tsbSearch.Enabled = True
            tsbExit.Enabled = True
            tsbUpload.Enabled = False
            EnableControl(False)
        Else
            ClearText()
        End If
    End Sub

    Public Sub LoadDetails(ByVal Code As String)
        Dim query As String
        Dim ctr As Integer = 0
        query = " SELECT   RecordID, ItemCategory, BOM_Group, MaterialCode, ItemName, UOM, QTY, UnitCost, TotalCost" & _
                " FROM     tblBOM_FG_Details LEFT JOIN tblItem_Master " & _
                " ON       tblBOM_FG_Details.MaterialCode = tblItem_Master.ItemCode " & _
                " AND      tblItem_Master.Status ='Active' " & _
                " WHERE    BOM_Code = '" & Code & "'" & _
                " ORDER BY LineNumber "
        SQL.GetQuery(query)
        dgvItemMaster.Rows.Clear()
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvItemMaster.Rows.Add(New String() {row(0).ToString, _
                                             row(1).ToString, _
                                             row(2).ToString, _
                                             row(3).ToString, _
                                             row(4).ToString, row(5).ToString, CDec(row(6)).ToString("N3"), CDec(row(7)).ToString("N3"), CDec(row(8)).ToString("N3")})
                LoadUOM(row(3).ToString, ctr, row(2).ToString)
                ctr += 1
            Next

        End If
    End Sub

    Public Sub LoadOverhead(ByVal Code As String)
        Dim query As String
        Dim ctr As Integer = 0
        query = " SELECT   Activity, Machine, RatePerHour, KW, NumHours, TotalKWH, TotalCost " & _
                " FROM     tblBOM_Overhead " & _
                " WHERE    BOMCode = '" & Code & "'" & _
                " ORDER BY LineNumber "
        SQL.GetQuery(query)
        dgvOverhead.Rows.Clear()
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvOverhead.Rows.Add(New String() {row(0).ToString, _
                                             row(1).ToString, _
                                             row(2).ToString, _
                                             row(3).ToString, _
                                             row(4).ToString, row(5).ToString, row(6).ToString})
                ctr += 1
            Next

        End If
    End Sub

    Public Sub LoadLabor(ByVal Code As String)
        Dim query As String
        Dim ctr As Integer = 0
        query = " SELECT   Activity, RatePerHour, CrewNum, TimeMins, TotalMins, TotalCost " & _
                " FROM     tblBOM_Labor " & _
                " WHERE    BOMCode = '" & Code & "'" & _
                " ORDER BY LineNumber "
        SQL.GetQuery(query)
        dgvLabor.Rows.Clear()
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvLabor.Rows.Add(New String() {row(0).ToString, _
                                             row(1).ToString, _
                                             row(2).ToString, _
                                             row(3).ToString, _
                                             row(4).ToString, row(5).ToString})
                ctr += 1
            Next

        End If
    End Sub

    Private Sub dgvItemMaster_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemMaster.CellClick
        If e.ColumnIndex = chBOMUOM.Index Then
            If e.RowIndex <> -1 AndAlso dgvItemMaster.Rows.Count > 0 Then
                If dgvItemMaster.Item(chBOMItemCode.Index, e.RowIndex).Value <> Nothing Then
                    LoadUOM(dgvItemMaster.Item(chBOMItemCode.Index, e.RowIndex).Value.ToString, e.RowIndex)
                ElseIf dgvItemMaster.Item(dgcItemGroup.Index, e.RowIndex).Value <> Nothing Then
                    LoadUOM("", e.RowIndex, dgvItemMaster.Item(dgcItemGroup.Index, e.RowIndex).Value.ToString)
                End If

                Dim dgvCol As DataGridViewComboBoxColumn
                dgvCol = dgvItemMaster.Columns.Item(e.ColumnIndex)
                dgvItemMaster.BeginEdit(True)
                dgvCol.Selected = True
                DirectCast(dgvItemMaster.EditingControl, DataGridViewComboBoxEditingControl).DroppedDown = True
                Dim editingComboBox As ComboBox = TryCast(sender, ComboBox)

            End If
        End If
    End Sub

    Private Sub dgvItemMaster_EditingControlShowing(sender As System.Object, e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvItemMaster.EditingControlShowing
        'Get the Editing Control. I personally prefer Trycast for this as it will not throw an error
        Dim editingComboBox As ComboBox = TryCast(e.Control, ComboBox)
        If Not editingComboBox Is Nothing Then
            ' Remove an existing event-handler, if present, to avoid 
            ' adding multiple handlers when the editing control is reused.
            RemoveHandler editingComboBox.SelectionChangeCommitted, New EventHandler(AddressOf editingComboBox_SelectionChangeCommitted)

            ' Add the event handler. 
            AddHandler editingComboBox.SelectionChangeCommitted, AddressOf editingComboBox_SelectionChangeCommitted
        End If

        'Prevent this event from firing twice, as is normally the case.
        RemoveHandler dgvItemMaster.EditingControlShowing, AddressOf dgvItemMaster_EditingControlShowing
    End Sub

    Private Sub editingComboBox_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rowIndex, columnIndex As Integer
        'Get the editing control
        Dim editingComboBox As ComboBox = TryCast(sender, ComboBox)
        If editingComboBox Is Nothing Then Exit Sub
        'Show your Message Boxes
        If editingComboBox.SelectedIndex <> -1 Then
            rowIndex = dgvItemMaster.SelectedCells(0).RowIndex
            columnIndex = dgvItemMaster.SelectedCells(0).ColumnIndex
            If dgvItemMaster.SelectedCells.Count > 0 Then
                Dim tempCell As DataGridViewComboBoxCell = dgvItemMaster.Item(columnIndex, rowIndex)
                Dim temp As String = editingComboBox.Text
                dgvItemMaster.Item(columnIndex, rowIndex).Selected = False
                dgvItemMaster.EndEdit(True)
                tempCell.Value = temp
            End If
        End If

        'Remove the handle to this event. It will be readded each time a new combobox selection causes the EditingControlShowing Event to fire
        RemoveHandler editingComboBox.SelectionChangeCommitted, AddressOf editingComboBox_SelectionChangeCommitted
        'Re-enable the EditingControlShowing event so the above can take place.
        AddHandler dgvItemMaster.EditingControlShowing, AddressOf dgvItemMaster_EditingControlShowing
    End Sub

    Private Sub LoadUOM(ItemCode As String, ByVal SelectedIndex As Integer, Optional ByVal Group As String = "")
        Try
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemMaster.Item(chBOMUOM.Index, SelectedIndex)
            dgvCB.Items.Clear()
            ' ADD ITEM UOM
            Dim query As String
            If ItemCode <> "" Then
                query = " SELECT    DISTINCT viewItem_UOM.UnitCode " & _
                  " FROM      tblItem_Master INNER JOIN viewItem_UOM " & _
                  " ON        tblItem_Master.ItemUOMGroup = viewItem_UOM.GroupCode " & _
                  " OR        tblItem_Master.ItemCode = viewItem_UOM.GroupCode " & _
                  " WHERE     ItemCode ='" & ItemCode & "'  "
            Else
                query = " SELECT    UOM AS UnitCode " & _
                        " FROM      tblBOM_Group " & _
                        " WHERE     BOMGroup ='" & Group & "'  "
            End If
            SQL.ReadQuery(query)
            dgvCB.Items.Clear()
            While SQL.SQLDR.Read
                dgvCB.Items.Add(SQL.SQLDR("UnitCode").ToString)
            End While
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Function UOMexist(ItemCode As String, ByVal SelectedIndex As Integer, Optional ByVal Group As String = "", Optional ByVal UOM As String = "") As Boolean
        Try
            Dim value As Boolean = False
            ' ADD ITEM UOM
            Dim query As String
            If ItemCode <> "" Then
                query = " SELECT    DISTINCT viewItem_UOM.UnitCode " & _
                  " FROM      tblItem_Master INNER JOIN viewItem_UOM " & _
                  " ON        tblItem_Master.ItemUOMGroup = viewItem_UOM.GroupCode " & _
                  " OR        tblItem_Master.ItemCode = viewItem_UOM.GroupCode " & _
                  " WHERE     ItemCode ='" & ItemCode & "'  "
            Else
                query = " SELECT    UOM AS UnitCode " & _
                        " FROM      tblBOM_Group " & _
                        " WHERE     BOMGroup ='" & Group & "'  "
            End If
            SQL.ReadQuery(query)
            While SQL.SQLDR.Read
                If UOM = SQL.SQLDR("UnitCode").ToString Then
                    value = True
                    Exit While
                End If
            End While
            Return value
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Function

    Private Sub dgvItemMaster_DataError(sender As System.Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvItemMaster.DataError
        Try

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub dgvLabor_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLabor.CellEndEdit
        Dim rowIndex As Integer = dgvLabor.CurrentCell.RowIndex
        ComputeTotalmins(rowIndex)
        ComputeDLCost(rowIndex)
        ComputeTotals()
    End Sub

    Private Sub ComputeTotalmins(ByVal rowID As Integer)
        If rowID >= 0 Then
            With dgvLabor.Rows(rowID)
                If IsNumeric(.Cells(dgcDLcrewNo.Index).Value) AndAlso IsNumeric(.Cells(dgcDLtime.Index).Value) Then
                    .Cells(dgcDLTotalMins.Index).Value = CDec(.Cells(dgcDLcrewNo.Index).Value) * CDec(.Cells(dgcDLtime.Index).Value)
                End If
            End With
        End If
    End Sub

    Private Sub ComputeDLCost(ByVal rowID As Integer)
        If rowID >= 0 Then
            With dgvLabor.Rows(rowID)
                If IsNumeric(.Cells(dgcDLrate.Index).Value) AndAlso IsNumeric(.Cells(dgcDLTotalMins.Index).Value) Then
                    .Cells(dgcDLtotalCost.Index).Value = CDec(.Cells(dgcDLrate.Index).Value) * CDec(.Cells(dgcDLTotalMins.Index).Value / 60)
                End If
            End With
        End If
    End Sub

    Private Sub ComputeKWH(ByVal rowID As Integer)
        If rowID >= 0 Then
            With dgvOverhead.Rows(rowID)
                If IsNumeric(.Cells(dgcFOhrs.Index).Value) AndAlso IsNumeric(.Cells(dgcFOKW.Index).Value) Then
                    .Cells(dgcFOtotalKWH.Index).Value = CDec(.Cells(dgcFOhrs.Index).Value) * CDec(.Cells(dgcFOKW.Index).Value)
                End If
            End With
        End If
    End Sub

    Private Sub ComputeFOCost(ByVal rowID As Integer)
        If rowID >= 0 Then
            With dgvOverhead.Rows(rowID)
                If IsNumeric(.Cells(dgcFOtotalKWH.Index).Value) AndAlso IsNumeric(.Cells(dgcFOrate.Index).Value) Then
                    .Cells(dgcFOcost.Index).Value = CDec(.Cells(dgcFOtotalKWH.Index).Value) * CDec(.Cells(dgcFOrate.Index).Value)
                End If
            End With
        End If
    End Sub

    Private Sub dgvOverhead_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvOverhead.CellEndEdit
        Dim rowIndex As Integer = dgvOverhead.CurrentCell.RowIndex
        ComputeKWH(rowIndex)
        ComputeFOCost(rowIndex)
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("BOMFG_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("BOM-FG")
            If f.transID <> "" Then
                Code = f.transID
            End If
            LoadBOM(Code)
            f.Dispose()
        End If
    End Sub

    Private Sub LoadBOM(ByVal Code As String)
        LoadHeader(Code)
        LoadDetails(Code)
        LoadLabor(Code)
        LoadOverhead(Code)
        LoadProfitMargin(txtItemCode.Text)
    End Sub

    Public Sub LoadProfitMargin(ByVal ItemCode As String)
        Dim query As String
        Dim SellingPrice, Revenue As Decimal
        Dim VATInclusive As Boolean
        Dim ctr As Integer = 0
        query = " SELECT Type, UnitPrice, UOM, UOMQTY, VATInclusive, VCEName " & _
                " FROM   viewItem_Price " & _
                " WHERE  Category='Selling' " & _
                " AND    ItemCode ='" & ItemCode & "'" & _
                " AND    PriceStatus ='Active' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            SellingPrice = CDec(SQL.SQLDR("UnitPrice")).ToString("N2")
            VATInclusive = SQL.SQLDR("VATInclusive").ToString

            If VATInclusive = True Then
                Revenue = SellingPrice - (SellingPrice * 0.12)
            Else
                Revenue = SellingPrice
            End If
            txtRevenue.Text = CDec(Revenue).ToString("N2")
            txtSetAside.Text = CDec(Revenue * 0.55).ToString("N2")
            txtCOGS.Text = CDec(Revenue * 0.45).ToString("N2")
            txtCOS.Text = CDec(txtDMcost.Text / txtQTY.Text).ToString("N2")
            txtProfitMargin.Text = CDec(txtCOGS.Text - txtCOS.Text).ToString("N2")
        End If
    End Sub

    Private Function RecordExist(ByVal Record As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblBOM_FG WHERE BOM_Code ='" & Record & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If BOMValidated() Then
            If txtItemCode.Text = "" Or txtItemName.Text = "" Or txtQTY.Text = "" Or txtCode.Text = "" Then
                MsgBox("Fill up required fieilds!", MsgBoxStyle.Information)
            ElseIf Code = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    If RecordExist(txtCode.Text) Then
                        Msg("BOM Code already in used! Please change VCECode", MsgBoxStyle.Exclamation)
                    Else
                        SaveHeader(txtCode.Text)
                        Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                        Code = txtCode.Text
                        LoadBOM(Code)
                    End If
                End If
            Else
                ' IF VCECODE IS CHANGED VALIDATE IF NEW CODE EXIST
                Dim Validated As Boolean = True
                If Code <> txtCode.Text Then
                    If RecordExist(txtCode.Text) Then
                        Validated = False
                    Else
                        Validated = True
                    End If
                End If

                If Validated Then
                    If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                        UpdateHeader(Code)
                        Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                        Code = txtCode.Text
                        LoadBOM(Code)
                    End If
                Else
                    Msg("BOM Code is already in used! Please change BOM Code", MsgBoxStyle.Exclamation)
                    txtCode.Text = Code
                    txtCode.SelectAll()
                End If

            End If
        End If
    End Sub

    Private Sub tsbUpload_Click(sender As System.Object, e As System.EventArgs) Handles tsbUpload.Click
        dgvItemMaster.Rows.Clear()
        Dim invalidUOM As Boolean = False
        Dim OpenFileDialog As New OpenFileDialog
        Dim ctrN As Integer
        Dim str As String
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        OpenFileDialog.Filter = "All Files (*.*)|*.*"
        OpenFileDialog.FileName = "Upload"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            objExcel.Workbooks.Open(FileName)
            str = "a"
            ctrN = 2
            Dim Category, Group, ItemCode, ItemDesc, UOM, QTY, FG_Code, FG_ItemCode, FG_UOM, FG_QTY As String
            Dim ctr As Integer = 0

            Do While str <> ""
                Category = RTrim(objExcel.Range("a" & CStr(ctrN)).Value)
                Group = RTrim(objExcel.Range("b" & CStr(ctrN)).Value)
                ItemCode = RTrim(objExcel.Range("c" & CStr(ctrN)).Value)
                ItemDesc = RTrim(objExcel.Range("d" & CStr(ctrN)).Value)
                UOM = RTrim(objExcel.Range("e" & CStr(ctrN)).Value)
                QTY = RTrim(objExcel.Range("f" & CStr(ctrN)).Value)
                FG_Code = RTrim(objExcel.Range("g" & CStr(ctrN)).Value)
                FG_ItemCode = RTrim(objExcel.Range("h" & CStr(ctrN)).Value)
                FG_UOM = RTrim(objExcel.Range("j" & CStr(ctrN)).Value)
                FG_QTY = RTrim(objExcel.Range("k" & CStr(ctrN)).Value)

                BOM_FG = FG_Code
                BOM_FG_ItemCode = FG_ItemCode
                If DataValidated() Then
                    txtCode.Text = FG_Code
                    txtItemCode.Text = FG_ItemCode
                    txtItemName.Text = GetItemName(FG_ItemCode)
                    LoadUOM(txtItemCode.Text)
                    txtQTY.Text = FG_QTY
                    If ItemCode <> "" Then
                        LoadItemDetails(ItemCode, True)
                    Else
                        dgvItemMaster.Rows.Add(New String() {
                                                                    "", _
                                                                    Category.ToString, _
                                                                    Group.ToString, _
                                                                   ItemCode.ToString, _
                                                                   GetItemName(ItemCode).ToString, _
                                                                   UOM.ToString, _
                                                                    QTY.ToString})
                        LoadUOM(ItemCode.ToString, dgvItemMaster.Rows.Count - 2, Group)
                    End If

                    If UOMexist(ItemCode, ctr, Group, UOM) Then
                        dgvItemMaster.Item(chBOMUOM.Index, ctr).Value = UOM
                    Else
                        invalidUOM = True
                    End If

                    dgvItemMaster.Item(chBOMQTY.Index, ctr).Value = QTY
                Else
                    Exit Do
                End If
                ctrN = ctrN + 1
                str = RTrim(objExcel.Range("e" & CStr(ctrN)).Value)
                ctr += 1

            Loop
            objExcel.Workbooks.Close()
            EnableControl(True)
            If invalidUOM Then
                Msg("There are UOMs that are not valid as Alternative UOM in some Item!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Function DataValidated() As Boolean
        If validateSFG_ItemCode() = False Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function BOMValidated() As Boolean
        If validateSFG_Code() = False Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function validateSFG_Code() As Boolean
        Dim query As String
        Dim value As Boolean = True
        query = "SELECT BOM_Code FROM tblBOM_SFG WHERE BOM_Code = '" & BOM_FG & "'"
        SQL.ReadQuery(query)

        If SQL.SQLDR.Read Then
            Msg("SFG Code already exist, please check.", MsgBoxStyle.Exclamation)
            value = False
        End If
        Return value
    End Function

    Private Function validateSFG_ItemCode() As Boolean
        Dim query As String
        Dim value As Boolean = True
        query = "SELECT ItemCode FROM tblItem_Master WHERE ItemCode = '" & BOM_FG_ItemCode & "'"
        SQL.ReadQuery(query)

        If Not SQL.SQLDR.Read Then
            Msg("SFG Item Code not exist in item master, please check.", MsgBoxStyle.Exclamation)
            value = False
        End If
        Return value
    End Function


    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("BOMFG_ADD") Then
            msgRestricted()
        Else
            ClearText()
            Code = ""

            txtCode.Select()
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
            tsbUpload.Enabled = True

            EnableControl(True)

            txtCode.Text = GenerateBOM("FG")
        End If
    End Sub
    Private Function GenerateBOM(ByVal Type As String) As String
        Dim query As String
        query = " SELECT '" & Type & "' + RIGHT('000' +  Cast(ISNULL(Max(Cast(REPLACE(BOM_Code, LEFT(BOM_Code,2),'')as int)+1),1) AS nvarchar),3) AS BOM_Code " & _
                    " FROM     tblBOM_FG "
        SQL.ReadQuery(query)
        SQL.SQLDR.Read()
        Return SQL.SQLDR("BOM_Code").ToString
    End Function

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("BOMFG_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)
            ' Toolstrip Buttons
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbCancel.Enabled = False
            tsbClose.Enabled = True
            tsbUpload.Enabled = True
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        ' Toolstrip Buttons
        If Code = "" Then
            tsbNew.PerformClick()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
        Else
            LoadBOM(Code)
            tsbEdit.Enabled = True
            tsbCancel.Enabled = True
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
        End If
        tsbSearch.Enabled = True
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
        tsbUpload.Enabled = False
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If Code <> "" Then
            Dim query As String
            query = " SELECT Top 1 BOM_Code FROM tblBOM_FG  WHERE BOM_Code < '" & Code & "' ORDER BY BOM_Code DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Code = SQL.SQLDR("BOM_Code").ToString
                LoadBOM(Code)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If Code <> "" Then
            Dim query As String
            query = " SELECT Top 1 BOM_Code FROM tblBOM_FG  WHERE BOM_Code > '" & Code & "' ORDER BY BOM_Code ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Code = SQL.SQLDR("BOM_Code").ToString
                LoadBOM(Code)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("BOMFG_DEL") Then
            msgRestricted()
        Else
            If txtItemCode.Text <> "" Then
                If MsgBox("Are you sure you want to delete this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE tblBOM_FG SET Status ='Inactive' WHERE BOM_Code = @BOM_Code "
                        SQL.FlushParams()
                        Code = txtCode.Text
                        SQL.AddParam("@BOM_Code", Code)
                        SQL.ExecNonQuery(deleteSQL)
                        Msg("Record deleted successfully", MsgBoxStyle.Information)

                        tsbSearch.Enabled = True
                        tsbNew.Enabled = True
                        tsbEdit.Enabled = False
                        tsbSave.Enabled = False
                        tsbCancel.Enabled = False
                        tsbClose.Enabled = False
                        tsbPrevious.Enabled = False
                        tsbNext.Enabled = False
                        tsbExit.Enabled = True
                        EnableControl(False)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "DELETE", "BOM_Code", Code, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                        tsbNew.PerformClick()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub txtItemCode_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtItemCode.TextChanged

    End Sub

    Private Sub txtItemName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtItemName.TextChanged

    End Sub

    Private Sub dgvItemMaster_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemMaster.CellContentClick

    End Sub

    Private Sub dgvLabor_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLabor.CellContentClick

    End Sub

    Private Sub ComputeTotals()
        Dim A, B, C As Decimal
        A = 0
        For Each row As DataGridViewRow In dgvItemMaster.Rows
            If Not row.Cells(chBOMQTY.Index).Value Is Nothing AndAlso IsNumeric(row.Cells(chCost.Index).Value) Then
                row.Cells(chTotalCost.Index).Value = CDec(row.Cells(chBOMQTY.Index).Value * row.Cells(chCost.Index).Value).ToString("N2")
                A += row.Cells(chTotalCost.Index).Value
            End If
        Next
        txtDMcost.Text = A.ToString("N2")

        B = 0
        For Each row As DataGridViewRow In dgvLabor.Rows
            B += row.Cells(dgcDLtotalCost.Index).Value
        Next
        txtDLcost.Text = B.ToString("N2")

        C = 0
        For Each row As DataGridViewRow In dgvOverhead.Rows
            C += row.Cells(dgcFOcost.Index).Value
        Next
        txtFOcost.Text = C.ToString("N2")

        txtTotalCost.Text = (A + B + C).ToString("N2")
    End Sub


    Private Sub dgvItemMaster_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs)
        If e.ColumnIndex <> -1 AndAlso e.RowIndex <> -1 AndAlso e.Button = MouseButtons.Right Then
            Dim c As DataGridViewCell = sender(e.ColumnIndex, e.RowIndex)
            If Not c.Selected Then
                c.DataGridView.ClearSelection()
                c.DataGridView.CurrentCell = c
                c.Selected = True
            End If
        End If


    End Sub

    Private Sub dgvItemMaster_KeyDown(sender As Object, e As KeyEventArgs)
        If (e.KeyCode = Keys.F10 AndAlso e.Shift) OrElse e.KeyCode = Keys.Apps Then
            e.SuppressKeyPress = True
            Dim currentCell As DataGridViewCell = sender.CurrentCell
            If currentCell Is Nothing Then
                Dim cms As ContextMenuStrip = currentCell.ContextMenuStrip
                If cms IsNot Nothing Then
                    Dim r As Rectangle = currentCell.DataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, False)
                    Dim p As Point = New Point(r.X + r.Width, r.Y + r.Height)
                    cms.Show(currentCell.DataGridView, p)
                End If
            End If
        End If
    End Sub


    Private Sub ViewItemInformationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewItemInformationToolStripMenuItem.Click

        Dim f As New frmItem_Master
        If dgvItemMaster.CurrentCell IsNot Nothing Then
            If dgvItemMaster.CurrentCell.RowIndex <> -1 Then
                Dim code As String = dgvItemMaster.Rows(dgvItemMaster.CurrentCell.RowIndex).Cells(chBOMItemCode.Index).Value.ToString
                If code <> "" Then
                    f.ShowDialog(code)
                Else
                    MsgBox("Please select an item first!", vbExclamation)
                End If
            End If
        End If
        f.Dispose()
    End Sub
  
End Class