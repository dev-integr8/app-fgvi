Public Class frmPL
    Dim RecordID, RefID As String
    Dim PLNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "PL"
    Dim ColumnPK As String = "PL_No"
    Dim DBTable As String = "tblPL"
    Dim ColumnID As String = "TransID"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim whoCreated, dateCreated As String
    Dim updateCTR As Integer = 0
    Dim SO_ID As Integer

    Private Property chRefRecID As Object
    Dim itemCode As String
    Dim line As Integer = 0

    Public Overloads Function ShowDialog(ByVal docnumber As String) As Boolean
        RecordID = docnumber
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmPL_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            dtpActual.Value = Date.Today.Date
            If RecordID <> "" Then
                LoadPL(RecordID)
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
            tsbPrint.Enabled = False
            tsbCopy.Enabled = False
            EnableControl(False)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        If Value = True Then
            dgvPick.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvPick.EditMode = DataGridViewEditMode.EditProgrammatically
        End If

        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub


    Private Sub LoadPL(ByVal ID As String)
        Dim query As String
        query = " SELECT   TransID, PL_No, VCECode, DatePL, DateDeliver, Remarks, Status, SO_Ref, DateCreated, WhoCreated " & _
                " FROM     tblPL " & _
                " WHERE    TransID = '" & ID & "' " & _
                " ORDER BY TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            RecordID = SQL.SQLDR("TransID").ToString
            PLNo = SQL.SQLDR("PL_No").ToString
            txtTransNum.Text = PLNo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            dtpDocDate.Text = SQL.SQLDR("DatePL").ToString
            dtpActual.Value = IIf(IsDate(SQL.SQLDR("DateDeliver")), SQL.SQLDR("DateDeliver"), Date.Today)
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            SO_ID = SQL.SQLDR("SO_Ref").ToString
            dateCreated = SQL.SQLDR("DateCreated").ToString
            whoCreated = SQL.SQLDR("WhoCreated").ToString
            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            txtSORef.Text = LoadSONo(SO_ID)
            LoadPLDetails(RecordID)

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
            tsbCopy.Enabled = False
            EnableControl(False)
        Else
            ClearText()
        End If
    End Sub

    Private Function LoadSONo(SO_ID As Integer) As String
        Dim query As String
        query = " SELECT SO_No FROM tblSO WHERE TransID = '" & SO_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("SO_No")
        Else
            Return ""
        End If
    End Function

    Protected Sub LoadPLDetails(ByVal RecordID As String)
        Dim itemExist As Boolean
        Dim ctr As Integer = 0
        Dim reqQTY, stockQTY, pickQTY, allocQTY, forAlloc As Decimal
        Dim ItemCode, ItemDesc, UOM, WHSE As String
        Dim lineSO As Integer = 0
        Dim linePL As Integer = 0
        Dim query As String
        'query = " SELECT	tblPL_Details.ItemCode, tblPL_Details.Description, tblPL_Details.UOM,  " & _
        '        "           ISNULL(tblPL_Details.ReqQTY,0) AS ReqQTY, " & _
        '        "           ISNULL(tblPL_Details.StockQTY,0) AS StockQTY, " & _
        '        "           ISNULL(tblPL_Details.PickQTY,0) AS PickQTY, " & _
        '        "           ISNULL(tblPL_Details.WHSE,'') AS WHSE, SO_Line, LineNum " & _
        '        " FROM	    tblPL_Details " & _
        '        " WHERE     tblPL_Details.RecordID = " & RecordID & " " & _
        '        " ORDER BY  tblPL_Details.LineNum "
        query = " SELECT	tblPL_Details.ItemCode, tblPL_Details.Description, tblPL_Details.UOM,   " & _
                "            ISNULL(tblPL_Details.ReqQTY,0) AS ReqQTY,  " & _
                "            ISNULL(tblPL_Details.StockQTY,0) AS StockQTY,  " & _
                "            ISNULL(tblPL_Details.PickQTY,0) AS PickQTY,  " & _
                "            ISNULL(Allocated.AllocatedQTY,0) AS AllocatedQTY,  " & _
                "            ISNULL(tblPL_Details.WHSE,'') AS WHSE, ISNULL(SO_Line,0) as SO_Line, LineNum  " & _
                "  FROM	    tblPL_Details LEFT JOIN " & _
                "  ( " & _
                " 		 SELECT	ItemCode, SUM(PickQTY) AS AllocatedQTY " & _
                " 		 FROM	tblPL INNER JOIN tblPL_Details " & _
                " 		 ON		tblPL.TransID = tblPL_Details.TransID " & _
                " 		 WHERE  tblPL.Status <> 'Modified' AND PickQTY > 0 AND SO_Ref ='" & SO_ID & "' AND tblPL.TransID <> " & RecordID & " " & _
                " 		 GROUP BY ItemCode " & _
                " ) AS Allocated " & _
                " ON		   tblPL_Details.ItemCode = Allocated.ItemCode " & _
                " WHERE     tblPL_Details.TransID = " & RecordID & " " & _
                " ORDER BY  tblPL_Details.LineNum  "
        dgvItemList.Rows.Clear()
        dgvPickAll.Rows.Clear()
        dgvPick.Rows.Clear()
        dgvPickList.Rows.Clear()
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count Then
            dgvItemList.Columns(dgcAllocatedQTY.Index).Visible = False
            dgvItemList.Columns(dgcForAllocation.Index).Visible = False
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                ItemCode = row(0)
                ItemDesc = row(1)
                UOM = row(2)
                reqQTY = CDec(row(3))
                stockQTY = CDec(row(4))
                pickQTY = CDec(row(5))
                allocQTY = CDec(row(6))
                forAlloc = reqQTY - allocQTY
                WHSE = row(7)
                lineSO = row(8)
                linePL = row(9)

                If allocQTY > 0 Then
                    dgvItemList.Columns(dgcAllocatedQTY.Index).Visible = False
                    dgvItemList.Columns(dgcForAllocation.Index).Visible = False
                End If

                ' POPULATE ITEMLIST DGV
                itemExist = False
                For Each item As DataGridViewRow In dgvItemList.Rows
                    If item.Cells(dgclineSO.Index).Value = lineSO Then
                        item.Cells(dgcPickTotal.Index).Value = CDec(item.Cells(dgcPickTotal.Index).Value) + pickQTY
                        itemExist = True
                    End If
                Next
                If itemExist = False Then
                    dgvItemList.Rows.Add({linePL, ItemCode, ItemDesc, UOM, reqQTY, allocQTY, forAlloc, pickQTY, lineSO})
                End If

                ' POPULATE STOCK DGV
                itemExist = False
                For Each item As DataGridViewRow In dgvPickAll.Rows
                    If item.Cells(dgcItemCodeAll.Index).Value = ItemCode AndAlso item.Cells(dgcWHSEall.Index).Value = WHSE Then
                        itemExist = True
                    End If
                Next
                If itemExist = False And stockQTY > 0 Then
                    dgvPickAll.Rows.Add({ItemCode, WHSE, stockQTY})
                End If

                ' POPULATE PICKLIST DGV
                If pickQTY > 0 Then
                    dgvPickList.Rows.Add({ItemCode, WHSE, pickQTY, lineSO})
                End If
            Next
        End If
    End Sub

    Private Sub txtVCEName_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.txtFilter.Text = txtVCEName.Text
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            txtVCEName.Text = f.VCEName
        End If
    End Sub

    Private Sub ClearText()
        txtTransNum.Clear()
        txtVCECode.Clear()
        txtVCEName.Clear()
        dgvItemList.Rows.Clear()
        dgvPickAll.Rows.Clear()
        dgvPick.Rows.Clear()
        dgvPickList.Rows.Clear()
        txtRemarks.Clear()
        txtSORef.Clear()
        txtStatus.Text = "Open"
        dtpActual.Value = Date.Today.Date
        dtpDocDate.Value = Date.Today.Date
    End Sub


    Private Sub dgvItemList_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellEndEdit
        Try
            Dim itemCode As String
            Dim rowIndex As Integer = dgvItemList.CurrentCell.RowIndex
            Dim colindex As Integer = dgvItemList.CurrentCell.ColumnIndex
            Select Case colindex
                Case dgcItemCode.Index
                    If dgvItemList.Item(dgcItemCode.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemList.Item(dgcItemCode.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("All Item", itemCode)
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItem(itemCode)
                        End If
                        f.Dispose()
                    End If
                Case dgcItemDesc.Index
                    If dgvItemList.Item(dgcItemDesc.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemList.Item(dgcItemDesc.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("All Item", itemCode)
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItem(itemCode)
                        End If
                        dgvItemList.Rows.RemoveAt(e.RowIndex)
                        f.Dispose()
                    End If
            End Select
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Public Sub LoadItem(ByVal ItemCode As String, Optional UOM As String = "", Optional QTY As Integer = 1)
        Try
            Dim query As String
            Dim netPrice As Decimal
            query = " SELECT  ItemCode,  ItemName, UOM,  " & _
                    "         CASE WHEN VATable = 1 " & _
                    "              THEN (CASE WHEN VATInclusive = 1 THEN UnitPrice ELSE UnitPrice*1.12 END)  " & _
                    "              ELSE UnitPrice " & _
                    "         END AS Net_Price, WHSE " & _
                    " FROM    viewItem_Price " & _
                    " WHERE   ItemCode = @ItemCode "
            SQL.FlushParams()
            SQL.AddParam("@ItemCode", ItemCode)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                If UOM = "" Then
                    UOM = SQL.SQLDR("UOM").ToString
                End If
                netPrice = SQL.SQLDR("Net_Price")
                dgvItemList.Rows.Add(New String() {SQL.SQLDR("ItemCode").ToString, _
                                              SQL.SQLDR("ItemName").ToString, _
                                             UOM, _
                                              QTY, _
                                              GetWHSEDesc(SQL.SQLDR("WHSE").ToString), _
                                              Format(netPrice, "#,###,###,###.00").ToString})
            End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub SaveRecord()
        Try
            activityStatus = True
            RecordID = GenerateTransID(ColumnID, DBTable)
            If TransAuto Then txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable) ' IF TRANS NUM IS SYSTEM GENERATED
            PLNo = txtTransNum.Text
            SavePL()
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "PL_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub SavePL()
        whoCreated = UserID
        dateCreated = GetDate()

        Dim insertSQL As String
        insertSQL = " INSERT INTO " & _
                    " tblPL(TransID, PL_No, BranchCode, BusinessCode, VCECode, DatePL, DateDeliver, Remarks, Status, SO_Ref, WhoCreated, DateCreated, TransAuto) " & _
                    " VALUES (@TransID, @PL_No, @BranchCode, @BusinessCode, @VCECode, @DatePL, @DateDeliver, @Remarks, @Status, @SO_Ref, @WhoCreated, @DateCreated, @TransAuto) "
        SQL.FlushParams()
        SQL.AddParam("@TransID", RecordID)
        SQL.AddParam("@PL_No", PLNo)
        SQL.AddParam("@BranchCode", BranchCode)
        SQL.AddParam("@BusinessCode", BusinessType)
        SQL.AddParam("@VCECode", txtVCECode.Text)
        SQL.AddParam("@DatePL", dtpDocDate.Value.Date)
        SQL.AddParam("@DateDeliver", dtpActual.Value.Date)
        SQL.AddParam("@Remarks", txtRemarks.Text)
        SQL.AddParam("@SO_Ref", SO_ID)
        SQL.AddParam("@Status", "Active")
        SQL.AddParam("@WhoCreated", whoCreated)
        SQL.AddParam("@DateCreated", dateCreated)
        SQL.AddParam("@TransAuto", TransAuto)
        SQL.ExecNonQuery(insertSQL)
        SavePLDetails()
    End Sub

    Private Sub SavePLDetails()
        Dim insertSQL As String
        Dim ItemCode, Description, UOM, WHSE As String
        Dim PickQTY, StockQTY, ReqQTY As Decimal
        Dim lineSO, linePL As Integer

        

        For Each row As DataGridViewRow In dgvItemList.Rows ' REQ QTY
            If Not row.Cells(dgcReqQTY.Index).Value Is Nothing AndAlso Not row.Cells(dgcItemCode.Index).Value Is Nothing Then
                ItemCode = IIf(row.Cells(dgcItemCode.Index).Value = Nothing, "", row.Cells(dgcItemCode.Index).Value)
                Description = IIf(row.Cells(dgcItemDesc.Index).Value = Nothing, "", row.Cells(dgcItemDesc.Index).Value)
                UOM = IIf(row.Cells(dgcUOM.Index).Value = Nothing, "", row.Cells(dgcUOM.Index).Value)
                If IsNumeric(row.Cells(dgclineSO.Index).Value) Then lineSO = row.Cells(dgclineSO.Index).Value Else lineSO = 0
                If IsNumeric(row.Cells(dgcLine.Index).Value) Then linePL = row.Cells(dgcLine.Index).Value Else linePL = 0
                If IsNumeric(row.Cells(dgcReqQTY.Index).Value) Then ReqQTY = CDec(row.Cells(dgcReqQTY.Index).Value) Else ReqQTY = 0

                Dim itemExistList As Boolean = False       ' CHECK IF ITEM EXIST IN DGVPICKALL DATAGRID

                For Each row2 As DataGridViewRow In dgvPickAll.Rows ' STOCK QTY
                    If row2.Cells(dgcItemCodeAll.Index).Value = ItemCode Then
                        WHSE = IIf(row2.Cells(dgcWHSEall.Index).Value = Nothing, "", row2.Cells(dgcWHSEall.Index).Value)
                        If IsNumeric(row2.Cells(dgcStockAll.Index).Value) Then StockQTY = CDec(row2.Cells(dgcStockAll.Index).Value) Else StockQTY = 0

                        For Each row3 As DataGridViewRow In dgvPickList.Rows ' PICK QTY
                            If row3.Cells(dgcItemCodeList.Index).Value = ItemCode _
                                AndAlso row3.Cells(dgcWHSEList.Index).Value = WHSE _
                                AndAlso row3.Cells(dgcLineList.Index).Value = lineSO Then
                                If IsNumeric(row3.Cells(dgcPickList.Index).Value) Then PickQTY = CDec(row3.Cells(dgcPickList.Index).Value) Else PickQTY = 0

                                ' IF ITEM DOES EXIST ON DGVPICKALL DATAGRID, SAVE PICK QUANTITY
                                If PickQTY > 0 Then
                                    itemExistList = True
                                    insertSQL = " INSERT INTO " & _
                                                " tblPL_Details(TransID, SO_Line, ItemCode, Description, UOM, ReqQTY, StockQTY, PickQTY, WHSE, LineNum, Status) " & _
                                                " VALUES(@TransID, @SO_Line, @ItemCode, @Description, @UOM, @ReqQTY, @StockQTY, @PickQTY, @WHSE, @LineNum, @Status) "
                                    SQL.FlushParams()
                                    SQL.AddParam("@TransID", RecordID)
                                    SQL.AddParam("@ItemCode", ItemCode)
                                    SQL.AddParam("@Description", Description)
                                    SQL.AddParam("@UOM", UOM)
                                    SQL.AddParam("@ReqQTY", ReqQTY)
                                    SQL.AddParam("@StockQTY", StockQTY)
                                    SQL.AddParam("@PickQTY", PickQTY)
                                    SQL.AddParam("@WHSE", WHSE)
                                    SQL.AddParam("@SO_Line", lineSO)
                                    SQL.AddParam("@LineNum", linePL)
                                    SQL.AddParam("@Status", "Active")
                                    SQL.ExecNonQuery(insertSQL)
                                    line += 1
                                End If
                            End If
                        Next
                    End If
                Next

                If itemExistList = False Then ' IF ITEM DOES NOT EXIST ON DGVPICKALL DATAGRID, SAVE REQ QUANTITY
                    insertSQL = " INSERT INTO " & _
                                " tblPL_Details(TransID, SO_Line, ItemCode, Description, UOM, ReqQTY, StockQTY, PickQTY, WHSE, LineNum, Status) " & _
                                " VALUES(@TransID, @SO_Line, @ItemCode, @Description, @UOM, @ReqQTY, @StockQTY, @PickQTY, @WHSE, @LineNum, @Status) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", RecordID)
                    SQL.AddParam("@ItemCode", ItemCode)
                    SQL.AddParam("@Description", Description)
                    SQL.AddParam("@UOM", UOM)
                    SQL.AddParam("@ReqQTY", ReqQTY)
                    SQL.AddParam("@StockQTY", 0)
                    SQL.AddParam("@PickQTY", 0)
                    SQL.AddParam("@WHSE", DBNull.Value)
                    SQL.AddParam("@SO_Line", lineSO)
                    SQL.AddParam("@LineNum", linePL)
                    SQL.AddParam("@Status", "Active")
                    SQL.ExecNonQuery(insertSQL)
                End If
            End If
        Next
    End Sub

    Private Sub UpdateRecord()
        Try
            activityStatus = True
            Dim insertSQL, updateSQL, deleteSQL As String
            updateSQL = " UPDATE    tblPL " & _
                        " SET       PL_No = @PL_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, DatePL = @DatePL, " & _
                        "           VCECode = @VCECode, DateDeliver = @DateDeliver, Remarks = @Remarks, " & _
                        "           SO_Ref = @SO_Ref, WhoModified = @WhoModified, DateModified = GETDATE() " & _
                        " WHERE     TransID = @TransID  "
            SQL.FlushParams()
            SQL.AddParam("@TransID", RecordID)
            SQL.AddParam("@PL_No", PLNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DatePL", dtpDocDate.Value.Date)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateDeliver", dtpActual.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@SO_Ref", SO_ID)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)

            deleteSQL = " DELETE FROM tblSO_Details WHERE TransID = '" & RecordID & "' "
            SQL.ExecNonQuery(deleteSQL)

            SavePLDetails()
            'SavePL()
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "PL_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus, updateCTR)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub CloseSO()
        Dim query, updateSQL As String
        query = " SELECT SUM(Unserved) AS Unserved  FROM viewSO_Unserved WHERE RecordID ='" & RecordID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            If SQL.SQLDR("Unserved") = 0 Then
                updateSQL = " UPDATE tblSO SET Status='Closed' "
            End If
        End If
    End Sub

    Private Sub btnSearchVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchVCE.Click
        Dim f As New frmVCE_Search
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        txtVCEName.Text = f.VCEName
    End Sub

    Private Sub LoadSO(ByVal SO As String)
        Dim query As String
        query = " SELECT  TransID, SO_No, DateSO AS Date, tblSO.VCECode, VCEName, Remarks " & _
                " FROM    tblSO INNER JOIN tblVCE_Master " & _
                " ON      tblSO.VCECode = tblVCE_Master.VCECode " & _
                " WHERE   tblSO.Status ='Active' AND TransID ='" & SO & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            SO_ID = SQL.SQLDR("TransID")
            txtSORef.Text = SQL.SQLDR("SO_No")
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString

            Dim allocQTY As Decimal = 0
            Dim reqQTY As Decimal = 0
            Dim forAlloc As Decimal = 0
            query = " SELECT tblSO_Details.LineNum, tblSO_Details.ItemCode, Description, UOM,  QTY " & _
                    " FROM   tblSO_Details " & _
                    " WHERE  tblSO_Details.TransID ='" & SO & "' " & _
                    " ORDER BY LineNum"
            dgvItemList.Rows.Clear()
            dgvPickAll.Rows.Clear()
            SQL.GetQuery(query)

            dgvItemList.Columns(dgcAllocatedQTY.Index).Visible = False
            dgvItemList.Columns(dgcForAllocation.Index).Visible = False
            Dim ctr As Integer = 0
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                    reqQTY = row(4)
                    allocQTY = LoadAllocated(row(1).ToString, SO, row(0))
                    forAlloc = reqQTY - allocQTY
                    dgvItemList.Rows.Add("", row(1).ToString, row(2).ToString, row(3).ToString, _
                                         CDec(reqQTY).ToString("N2"), CDec(allocQTY).ToString("N2"), CDec(forAlloc).ToString("N2"), "0.00", row(0).ToString)
                    If allocQTY > 0 Then
                        dgvItemList.Columns(dgcAllocatedQTY.Index).Visible = True
                        dgvItemList.Columns(dgcForAllocation.Index).Visible = True
                    End If
                    ' ADD ONLY SPECIFIC ITEMS INTO DGV PICK ALL
                    Dim itemExist As Boolean = False
                    For Each row2 As DataGridViewRow In dgvPickAll.Rows
                        If row2.Cells(dgcItemCodeAll.Index).Value = row(1).ToString Then
                            itemExist = True
                            Exit For
                        End If
                    Next
                    If itemExist = False Then
                        LoadStock(row(1).ToString, row(3).ToString)
                    End If

                    ctr += 1
                Next
            End If
            RefreshLine()
        Else
            ClearText()
        End If
    End Sub

    Private Sub RefreshLine()
        Dim i As Integer = 1
        For Each row As DataGridViewRow In dgvItemList.Rows
            row.Cells(dgcLine.Index).Value = i
            i += 1
        Next
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("PL_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("PL")
            RecordID = f.transID
            LoadPL(RecordID)
            f.Dispose()
        End If
    End Sub

    Private Sub LoadStock(ByVal ItemCode As String, ByVal UOM As String)
        Dim query As String
        query = " SELECT Stock.ItemCode, Stock.WHSE, ISNULL(Stock.QTY,0) - ISNULL(Allocated.QTY,0)  AS QTY" & _
                " FROM " & _
                "     (  SELECT ItemCode, WHSE, QTY FROM viewItem_Stock WHERE ItemCode = @ItemCode AND UOM = @UOM) AS Stock " & _
                " LEFT JOIN " & _
                "     ( SELECT ItemCode, WHSE, SUM(QTY) AS QTY FROM viewPL_AllocatedItem WHERE TransID <> @TransID AND ItemCode = @ItemCode AND UOM = @UOM GROUP BY ItemCode, WHSE) AS Allocated " & _
                " ON    Stock.ItemCode = Allocated.ItemCode " & _
                " AND	Stock.WHSE = Allocated.WHSE "
        SQL.FlushParams()
        SQL.AddParam("@ItemCode", ItemCode)
        SQL.AddParam("@TransID", RecordID)
        SQL.AddParam("@UOM", UOM)
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvPickAll.Rows.Add({SQL.SQLDR("ItemCode").ToString, SQL.SQLDR("WHSE").ToString, SQL.SQLDR("QTY").ToString})
        End While
        SQL.FlushParams()
    End Sub


    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("PL_ADD") Then
            msgRestricted()
        Else
            ClearText()
            RecordID = ""
            PLNo = ""

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
            tsbCopy.Enabled = True
            EnableControl(True)

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
        End If
    End Sub

    Private Sub dgvItemList_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellClick
        ' LOAD ITEM STOCK FROM PICKALL TO PICK DGV
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            If dgvItemList.Item(dgcItemCode.Index, e.RowIndex).Value <> "" Then
                dgvPick.Rows.Clear()

                ' SET SELECTED ITEM CODE AND LINE TO VARIABLE
                itemCode = dgvItemList.Item(dgcItemCode.Index, e.RowIndex).Value
                line = dgvItemList.Item(dgclineSO.Index, e.RowIndex).Value

                ' LOOP THROUGH DGV PICKALL THE ITEM AVILABLE STOCK PER WHSE 
                For Each row As DataGridViewRow In dgvPickAll.Rows
                    If row.Cells(dgcItemCodeAll.Index).Value = itemCode Then

                        ' LOOP THROUGH DGV PICKLIST ALL PICKED QTY
                        Dim pickQTY As Decimal = 0
                        Dim allocQTY As Decimal = 0
                        Dim stockQTY As Decimal = row.Cells(dgcStockAll.Index).Value
                        Dim pick As Boolean = False
                        For Each rowPick As DataGridViewRow In dgvPickList.Rows

                            ' IF SELECTED LINE HAS ITEMS IN PICK LIST DGV GET TOTAL PICK QTY
                            If rowPick.Cells(dgcItemCodeList.Index).Value = itemCode _
                                AndAlso row.Cells(dgcWHSEall.Index).Value = rowPick.Cells(dgcWHSEList.Index).Value Then
                                If rowPick.Cells(dgcLineList.Index).Value = line Then
                                    pickQTY += rowPick.Cells(dgcPickList.Index).Value
                                Else
                                    allocQTY += rowPick.Cells(dgcPickList.Index).Value
                                End If
                            End If
                        Next
                        If pickQTY = 0 Then
                            pick = False
                        Else
                            pick = True
                        End If
                        dgvPick.Rows.Add({pick, row.Cells(dgcWHSEall.Index).Value, CDec(stockQTY - allocQTY).ToString("N2"), pickQTY})
                    End If
                Next
            End If
        End If
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


    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("PL_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)

            ' RELOAD ITEM STOCK
            dgvPickAll.Rows.Clear()
            Dim itemExist As Boolean = False

            For Each row As DataGridViewRow In dgvItemList.Rows
                For Each row2 As DataGridViewRow In dgvPickAll.Rows
                    If row2.Cells(dgcItemCodeAll.Index).Value = row.Cells(1).Value.ToString Then
                        itemExist = True
                        Exit For
                    End If
                Next
                If itemExist = False Then
                    LoadStock(row.Cells(1).Value.ToString, row.Cells(3).Value.ToString)
                End If
            Next




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
            tsbCopy.Enabled = False
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If PLNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblPL  WHERE PL_No < '" & PLNo & "' ORDER BY PL_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                RecordID = SQL.SQLDR("TransID").ToString
                LoadPL(RecordID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If PLNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblPL  WHERE PL_No > '" & PLNo & "' ORDER BY PL_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                RecordID = SQL.SQLDR("TransID").ToString
                LoadPL(RecordID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub


    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        If MsgBox("Your changes will not be saved" & vbNewLine & "Are you sure you want to close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            If RecordID = "" Then
                ClearText()
                EnableControl(False)
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
                tsbPrevious.Enabled = False
                tsbNext.Enabled = False
                tsbPrint.Enabled = False
            Else
                LoadPL(RecordID)
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
            tsbCopy.Enabled = False
        End If
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbCopyPO_Click(sender As System.Object, e As System.EventArgs) Handles tsbCopyPO.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("SO-PL")
        If f.transID <> "" Then
            LoadSO(f.transID)
        End If
        f.Dispose()
    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("PL_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE  tblPL SET Status ='Cancelled', WhoModified = @WhoModified, DateModified = GETDATE() WHERE TransID = @TransID "
                        SQL.FlushParams()
                        PLNo = txtTransNum.Text
                        SQL.AddParam("@TransID", RecordID)
                        SQL.AddParam("@WhoModified", UserID)
                        SQL.ExecNonQuery(deleteSQL)
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
                        tsbCopy.Enabled = False
                        EnableControl(False)

                        PLNo = txtTransNum.Text
                        LoadPL(RecordID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "PL_No", PLNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub frmPL_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                If tsbReports.Enabled = True Then tsbReports.PerformClick()
            ElseIf e.KeyCode = Keys.C Then
                If tsbCopy.Enabled = True Then tsbCopy.ShowDropDown()
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

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If DataValidation() = True Then
            If RecordID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    SaveRecord()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    LoadPL(RecordID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    UpdateRecord()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    LoadPL(RecordID)
                End If
            End If
        End If
    End Sub

    Private Function DataValidation() As Boolean
        Dim valid As Boolean = True
        If txtVCECode.Text = "" Then
            Msg("Please enter VCECode!", MsgBoxStyle.Exclamation)
            valid = False
        ElseIf dgvItemList.Rows.Count = 0 Then
            Msg("No item entered!", MsgBoxStyle.Exclamation)
            valid = False
        ElseIf TransAuto = False AndAlso txtTransNum.Text = "" Then ' IF TRANS. NO. IS MANUAL, PL NO. INPUT IS REQUIRED
            Msg("Please Enter PL No.!", MsgBoxStyle.Exclamation)
            valid = False
        ElseIf RecordID = "" AndAlso TransAuto = False AndAlso txtTransNum.Text = TransNoValid() Then ' CHECK PL NO. IF ALREADY EXIST WHEN CREATING NEW PL
            Msg("PL No. already exist!", MsgBoxStyle.Exclamation)
            valid = False
        End If
        Return valid
    End Function

    Private Function TransNoValid() As String
        Dim query As String
        query = " SELECT PL_No FROM tblPL WHERE PL_No = @PL_No  "
        SQL.FlushParams()
        SQL.AddParam("@PL_No", txtTransNum.Text)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("PL_No")
        Else
            Return ""
        End If
    End Function

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("PL", RecordID)
        f.Dispose()
    End Sub

    Private Sub dgvItemList_DataError(sender As System.Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvItemList.DataError
        Try

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub dgvPickList_CurrentCellDirtyStateChanged(sender As System.Object, e As System.EventArgs) Handles dgvPick.CurrentCellDirtyStateChanged

        If disableEvent = False Then
            Dim WHSE As String
            Dim QTY As Decimal = 0
            Dim Required As Decimal = 0
            Dim Allocated As Decimal = 0
            Dim ForAllocation As Decimal = 0
            Dim PickQTY As Decimal = 0
            ' IF  PICK CHECKBOX  IS FALSE SET VALUE TO 0 ELSE SET PICK QTY TO FOR ALLOCATION
            If dgvPick.SelectedCells.Count > 0 AndAlso dgvPick.SelectedCells(0).ColumnIndex = dgcPick.Index Then
                If dgvPick.SelectedCells(0).RowIndex <> -1 Then
                    WHSE = dgvPick.Rows(dgvPick.SelectedCells(0).RowIndex).Cells(dgcWHSE.Index).Value.ToString
                    If dgvPick.SelectedCells(0).Value = True Then
                        dgvPick.Item(dgcPick.Index, dgvPick.SelectedCells(0).RowIndex).Value = False
                        QTY = 0
                        dgvPick.Rows(dgvPick.SelectedCells(0).RowIndex).Cells(dgcPickQTY.Index).Value = QTY
                        dgvPick.Rows(dgvPick.SelectedCells(0).RowIndex).Cells(dgcPickActual.Index).Value = QTY
                    Else
                        dgvPick.Item(dgcPick.Index, dgvPick.SelectedCells(0).RowIndex).Value = True
                        QTY = dgvPick.Rows(dgvPick.SelectedCells(0).RowIndex).Cells(dgcStockQTY.Index).Value.ToString

                        Required = dgvItemList.Rows(dgvItemList.SelectedRows(0).Index).Cells(dgcForAllocation.Index).Value  ' REQUIRED QTY IS ORDERED LESS ALLOCATION FROM OTHER PL TRANSACTION
                        Allocated = dgvItemList.Rows(dgvItemList.SelectedRows(0).Index).Cells(dgcPickTotal.Index).Value
                        ForAllocation = Required - Allocated

                        If QTY > ForAllocation Then
                            PickQTY = ForAllocation
                        Else
                            PickQTY = QTY
                        End If
                        dgvPick.Rows(dgvPick.SelectedCells(0).RowIndex).Cells(dgcPickQTY.Index).Value = PickQTY
                        dgvPick.Rows(dgvPick.SelectedCells(0).RowIndex).Cells(dgcPickActual.Index).Value = PickQTY
                    End If

                    If PickQTY = 0 Then
                        disableEvent = True
                        dgvPick.Item(dgcPick.Index, dgvPick.SelectedCells(0).RowIndex).Value = False
                        disableEvent = False
                    End If
                    UpdatePickList(itemCode, WHSE, PickQTY, line)
                    dgvPick.SelectedCells(0).Selected = False
                    dgvPick.EndEdit()

                End If
            End If
        End If
    End Sub

    Private Function GetPickTotalQTY(ByVal ItemCode As String, WHSE As String, ByVal lineNo As Integer) As Decimal
        Dim pickQTY As Decimal = 0
        For Each row As DataGridViewRow In dgvPickList.Rows
            If row.Cells(dgcItemCodeList.Index).Value = ItemCode AndAlso row.Cells(dgcWHSEList.Index).Value = WHSE Then
                If row.Cells(dgcLineList.Index).Value <> lineNo Then
                    pickQTY += row.Cells(dgcPickList.Index).Value
                End If

            End If
        Next
        Return pickQTY
    End Function

    Private Sub UpdatePickList(ByVal ItemCode As String, WHSE As String, ByVal QTY As Decimal, ByVal LineSo As Integer)
        Dim pickQTY As Decimal = 0
        Dim hasRecord As Boolean = False
        For Each row As DataGridViewRow In dgvPickList.Rows
            If Not IsNothing(row.Cells(dgcItemCodeList.Index).Value) AndAlso Not IsNothing(row.Cells(dgcWHSEList.Index).Value) Then
                If row.Cells(dgcItemCodeList.Index).Value = ItemCode AndAlso row.Cells(dgcWHSEList.Index).Value = WHSE AndAlso row.Cells(dgcLineList.Index).Value = LineSo Then
                    row.Cells(dgcPickList.Index).Value = QTY
                    hasRecord = True
                    Exit For
                End If
            End If
        Next
        If Not hasRecord Then
            dgvPickList.Rows.Add({ItemCode, WHSE, QTY, LineSo})
        End If
        UpdatePickTotal(ItemCode, LineSo)
    End Sub

    Private Sub dgvPickList_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPick.CellEndEdit
        If dgvPick.SelectedCells.Count > 0 Then
            If e.ColumnIndex = dgcPickQTY.Index Then
                Dim WHSE As String
                Dim QTY As Decimal = 0
                Dim Required As Decimal = 0
                Dim Picked As Decimal = 0
                Dim ForAllocation As Decimal = 0
                Dim PickQTY As Decimal = 0
                Dim ActualQTY As Decimal = 0
                Dim IncDec As Decimal = 0
                Dim StockQTY As Decimal = 0
                WHSE = dgvPick.Rows(e.RowIndex).Cells(dgcWHSE.Index).Value.ToString
                QTY = dgvPick.Rows(e.RowIndex).Cells(dgcPickQTY.Index).Value
                ActualQTY = dgvPick.Rows(e.RowIndex).Cells(dgcPickActual.Index).Value
                IncDec = QTY - ActualQTY
                disableEvent = True
                dgvPick.Item(dgcPick.Index, e.RowIndex).Value = True
                disableEvent = False
                Required = dgvItemList.Rows(dgvItemList.SelectedRows(0).Index).Cells(dgcForAllocation.Index).Value
                Picked = dgvItemList.Rows(dgvItemList.SelectedRows(0).Index).Cells(dgcPickTotal.Index).Value
                ForAllocation = Required - Picked
                StockQTY = dgvPick.Rows(dgvPick.SelectedCells(0).RowIndex).Cells(dgcStockQTY.Index).Value.ToString
                If QTY >= StockQTY Then
                    QTY = StockQTY
                End If

                '  IF (TOTAL PICK QTY + INCDEC) IS GREATER THAN REQUIRED QTY
                If (Picked + IncDec) >= Required Then

                    If ForAllocation <= 0 Then

                        PickQTY = ActualQTY
                    Else
                        If ForAllocation > QTY Then
                            PickQTY = QTY
                        Else
                            PickQTY = ForAllocation + ActualQTY
                        End If

                    End If

                    dgvPick.Item(dgcPickQTY.Index, e.RowIndex).Value = PickQTY
                    dgvPick.Item(dgcPickActual.Index, e.RowIndex).Value = PickQTY
                Else
                    PickQTY = QTY
                    dgvPick.Item(dgcPickQTY.Index, e.RowIndex).Value = PickQTY
                    dgvPick.Item(dgcPickActual.Index, e.RowIndex).Value = PickQTY
                End If

                If dgvPick.Item(dgcPickQTY.Index, e.RowIndex).Value = 0 Then
                    dgvPick.Item(dgcPick.Index, e.RowIndex).Value = False
                End If
              
                UpdatePickList(itemCode, WHSE, PickQTY, line)

            End If
        End If
    End Sub

    Private Sub UpdatePickTotal(ByVal ItemCode As String, ByVal lineSo As Integer)
        Dim total As Decimal = 0
        For Each row As DataGridViewRow In dgvPickList.Rows
            If row.Cells(dgcItemCodeList.Index).Value = ItemCode AndAlso row.Cells(dgcLineList.Index).Value = lineSo Then
                If row.Cells(dgcPickList.Index).Value >= 0 Then
                    total += row.Cells(dgcPickList.Index).Value
                End If
            End If
        Next
        For Each row As DataGridViewRow In dgvItemList.Rows
            If row.Cells(dgcItemCode.Index).Value = ItemCode AndAlso row.Cells(dgclineSO.Index).Value = lineSo Then
                row.Cells(dgcPickTotal.Index).Value = total
            End If
        Next
    End Sub

    Private Sub dgvItemList_Sorted(sender As System.Object, e As System.EventArgs) Handles dgvItemList.Sorted
        RefreshLine()
    End Sub

    Private Function LoadAllocated(ByVal ItemCode As String, ByVal SORef As Integer, ByVal SOLine As Integer) As Decimal
        Dim query As String
        query = " 	 SELECT	    ItemCode, SO_Line, ISNULL(SUM(PickQTY),0) AS AllocatedQTY " & _
                " 	 FROM	    tblPL INNER JOIN tblPL_Details " & _
                " 	 ON		    tblPL.TransID = tblPL_Details.TransID " & _
                " 	 WHERE      ItemCode = @ItemCode AND tblPL.Status NOT IN ('Cancelled', 'Modified') AND PickQTY > 0 AND SO_Ref =@SORef AND tblPL_Details.SO_Line = @SOLine " & _
                " 	 GROUP BY   ItemCode, SO_Line "
        SQL.FlushParams()
        SQL.AddParam("@ItemCode", ItemCode)
        SQL.AddParam("@SORef", SORef)
        SQL.AddParam("@SOLine", SOLine)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("AllocatedQTY").ToString
        Else
            Return 0
        End If
    End Function

    Private Sub dgvPick_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPick.CellContentClick

    End Sub

    Private Sub dgvItemList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellContentClick

    End Sub

    Private Sub dgvItemList_CellMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvItemList.CellMouseClick

    End Sub
End Class