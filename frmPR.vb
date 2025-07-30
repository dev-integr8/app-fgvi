Public Class frmPR
    Dim TransID As String
    Dim PRNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "PRQ"
    Dim ColumnPK As String = "PR_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblPR"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim BOM_ID As String
    Dim SO_ID As Integer
    Dim ForApproval As Boolean = False

    'SETUP VARIABLES
    Dim PRstock As String

    Private Sub frmPR_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            LoadSetup()
            LoadDepartment()
            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            dtpDelivery.Value = Date.Today.Date
            LoadChartOfAccount()
            If PRNo <> "" Then
                LoadPR(PRNo)
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
            tsbCopy.Enabled = False
            tsbPrint.Enabled = False
            EnableControl(False)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadSetup()
        Dim query As String
        query = " SELECT  ISNULL(PR_StockLevel,0) AS PR_StockLevel FROM tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            PRstock = SQL.SQLDR("PR_StockLevel").ToString
            If PRstock <> "" Then
                LoadWHlevel(PRstock)
            End If
        End If
    End Sub

    Private Sub LoadDepartment()
        Dim query As String
        query = " SELECT Description FROM tblCC WHERE Status ='Active' "
        SQL.ReadQuery(query)
        cbDeliverTo.Items.Clear()
        While SQL.SQLDR.Read
            cbDeliverTo.Items.Add(SQL.SQLDR("Description").ToString)
        End While
    End Sub

    Private Sub LoadWHlevel(ByVal GroupID As String)
        Dim query As String
        query = " SELECT DISTINCT  " & GroupID & " AS GroupID FROM tblWarehouse WHERE Status ='Active' "
        SQL.ReadQuery(query)
        cbStock.Items.Clear()
        cbStock.Items.Add("ALL")
        While SQL.SQLDR.Read
            cbStock.Items.Add(SQL.SQLDR("GroupID").ToString)
        End While
        cbStock.SelectedItem = "ALL"
    End Sub


    Private Sub LoadChartOfAccount()
        Dim query As String
        query = " SELECT  AccountCode, AccountTitle " & _
                " FROM    tblCOA_Master " & _
                " WHERE   AccountNature = 'Debit' " & _
                " ORDER BY AccountTitle "
        SQL.ReadQuery(query)
        cbGLAccount.Items.Clear()
        While SQL.SQLDR.Read
            cbGLAccount.Items.Add(SQL.SQLDR("AccountTitle"))
        End While
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        txtVCEName.Enabled = Value
        btnSearchVCE.Enabled = Value
        cbCurrency.Enabled = Value
        cbGLAccount.Enabled = Value
        dgvItemList.AllowUserToAddRows = Value
        dgvItemList.AllowUserToDeleteRows = Value
        If Value = True Then
            dgvItemList.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemList.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        cbDeliverTo.Enabled = Value
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        dtpDelivery.Enabled = Value
        cbPurchaseType.Enabled = Value
        cbStock.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Private Sub LoadPR(ByVal TransNum As String)
        Dim query As String
        query = " SELECT   TransID, PR_No, DatePR, AccntCode, PurchaseType, Remarks, DateNeeded, RequestedBy, BOM_Ref, ISNULL(SO_Ref,0) as SO_Ref, Status, VCECode " & _
                " FROM     tblPR " & _
                " WHERE    TransID = '" & TransNum & "' " & _
                " ORDER BY TransID "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            txtTransNum.Text = SQL.SQLDR("PR_No").ToString
            PRNo = SQL.SQLDR("PR_No").ToString
            cbGLAccount.SelectedItem = SQL.SQLDR("AccntCode").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            dtpDocDate.Text = SQL.SQLDR("DatePR").ToString
            dtpDelivery.Text = SQL.SQLDR("DateNeeded").ToString
            cbDeliverTo.Text = SQL.SQLDR("RequestedBy").ToString
            BOM_ID = SQL.SQLDR("BOM_Ref").ToString
            SO_ID = SQL.SQLDR("SO_Ref").ToString
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            If Not IsDBNull(SQL.SQLDR("PurchaseType")) Then
                cbPurchaseType.SelectedItem = SQL.SQLDR("PurchaseType").ToString
            Else
                cbPurchaseType.SelectedIndex = -1
            End If
            If IsNumeric(BOM_ID) Then
                txtBOMRef.Text = LoadBOMNo(BOM_ID)
            End If

            txtSORef.Text = LoadSONo(SO_ID)
            txtVCEName.Text = GetVCEName(txtVCECode.Text)

            If BOM_ID <> "" Then
                dgvItemList.Columns(dgcBOMQTY.Index).Visible = True
                dgvItemList.Columns(dgcStock.Index).Visible = True
            Else
                dgvItemList.Columns(dgcBOMQTY.Index).Visible = False
                dgvItemList.Columns(dgcStock.Index).Visible = False
            End If

            LoadPRDetails(TransID)

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
            tsbCopy.Enabled = False
            tsbSearch.Enabled = True
            tsbExit.Enabled = True
            If dtpDocDate.Value < GetMaxPEC() Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            End If
            EnableControl(False)
        Else
            ClearText()
        End If
    End Sub

    Private Function LoadBOMNo(ID As Integer) As String
        Dim query As String
        query = " SELECT BOM_No FROM tblBOM WHERE TransID = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("BOM_No").ToString
        Else
            Return ""
        End If
    End Function


    Private Function LoadSONo(ID As Integer) As String
        Dim query As String
        query = " SELECT SO_No FROM tblSO WHERE TransID = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("SO_No").ToString
        Else
            Return ""
        End If
    End Function

    Protected Sub LoadPRDetails(ByVal TransID As String)
        Dim ctr As Integer = 0
        Dim query As String
        query = " SELECT    ItemGroup, ItemCode, Description, BOM_UOM, UOM, ISNULL(BOMQTY,0) AS BOMQTY, " & _
                "ISNULL(InStock,0) AS InStock, QTY, tblPR_Details.AccntCode,  tblCOA_Master.AccountTitle, " & _
                "ISNULL(ReserveQTY,0) AS ReserveQTY  " &
                " FROM      tblPR_Details " &
                "LEFT JOIN " &
                " tblCOA_Master ON " &
                " tblCOA_Master.AccountCode = tblPR_Details.AccntCode " &
                " WHERE     tblPR_Details.TransID = " & TransID & " " &
                " ORDER BY  LineNum "
        dgvItemList.Rows.Clear()
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvItemList.Rows.Add(row(0).ToString, row(1).ToString, "", row(2).ToString, row(3).ToString, _
                                                CDec(row(5)).ToString("N2"), CDec(row(6)).ToString("N2"), _
                                                  (CDec(row(5)) - CDec(row(6))).ToString("N2"), _
                                                row(4).ToString, CDec(row(7)).ToString("N2"), row(8).ToString, row(9).ToString, _
                                                CDec(row(10)).ToString("N2"))
                LoadUOM(row(1).ToString, ctr)
                ctr += 1
            Next
        End If
        LoadBarCode()
    End Sub

    Private Sub LoadUOM(ItemCode As String, ByVal SelectedIndex As Integer)
        Try
            Dim bool As Boolean = True
            Dim strUOM As String = dgvItemList.Item(chUOM_PR.Index, SelectedIndex).Value
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemList.Item(chUOM_PR.Index, SelectedIndex)
            dgvCB.Items.Clear()
            ' ADD ALL WHSEc
            Dim query As String

            query = " SELECT DISTINCT UOM.UnitCode FROM tblItem_Master INNER JOIN  " & _
               " ( " & _
               " SELECT GroupCode, UnitCode FROM tblUOM_Group WHERE Status ='Active' " & _
               " UNION ALL " & _
               " SELECT GroupCode, UnitCodeFrom FROM tblUOM_GroupDetails " & _
               "  UNION ALL  " & _
               "    SELECT ItemCode, ItemUOM FROM tblItem_Master WHERE Status ='Active'  " & _
               " UNION ALL " & _
               " SELECT GroupCode, UnitCodeTo FROM tblUOM_GroupDetails " & _
               " ) AS UOM " & _
               " ON tblItem_Master.ItemUOMGroup = UOM.GroupCode " & _
               " OR  tblItem_Master.ItemCode = UOM.GroupCode " & _
               " WHERE ItemCode ='" & ItemCode & "' "
            SQL.ReadQuery(query, 2)
            dgvCB.Items.Clear()
            While SQL.SQLDR2.Read
                If strUOM = SQL.SQLDR2("UnitCode").ToString Then
                    bool = False
                End If
                dgvCB.Items.Add(SQL.SQLDR2("UnitCode").ToString)
            End While
            dgvCB.Items.Add("")
            If bool = True Then
                dgvCB.Value = ""
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub ClearText()
        txtTransNum.Text = ""
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        cbGLAccount.SelectedIndex = -1
        dgvItemList.Rows.Clear()
        cbDeliverTo.Text = ""
        txtRemarks.Text = ""
        txtStatus.Text = "Open"
        txtBOMRef.Text = ""
        txtSORef.Text = ""
        cbCurrency.Items.Clear()
        dtpDocDate.MinDate = GetMaxPEC()
        dtpDelivery.Value = Date.Today.Date
        cbPurchaseType.SelectedItem = "Goods (Stock)"
        dgvItemList.Columns(dgcBOMQTY.Index).Visible = False
        dgvItemList.Columns(dgcStock.Index).Visible = False
    End Sub

    Private Sub dgvItemList_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellEndEdit
        Try

            Dim itemCode, UOM As String
            Dim rowIndex As Integer = dgvItemList.CurrentCell.RowIndex
            Dim colindex As Integer = dgvItemList.CurrentCell.ColumnIndex
            Select Case colindex
                Case chItemCode.Index
                    If dgvItemList.Item(chItemCode.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemList.Item(chItemCode.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("ItemListPR", itemCode, "Purchase", "ItemCode")
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItem(itemCode)
                        End If
                        dgvItemList.Rows.RemoveAt(e.RowIndex)
                        f.Dispose()
                    End If
                Case chBarcode.Index
                    If dgvItemList.Item(chBarcode.Index, e.RowIndex).Value <> "" Then

                        itemCode = dgvItemList.Item(chBarcode.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("ItemListPR", itemCode, "Purchase", "Barcode")
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            UOM = f.UOM
                            LoadItem(itemCode, UOM)
                        End If
                        dgvItemList.Rows.RemoveAt(e.RowIndex)
                        f.Dispose()
                    End If
                Case chItemDesc.Index
                    If dgvItemList.Item(chItemDesc.Index, e.RowIndex).Value <> "" Then

                        itemCode = dgvItemList.Item(chItemDesc.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("ItemListPR", itemCode, "Purchase", "ItemName")
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItem(itemCode)
                        End If
                        dgvItemList.Rows.RemoveAt(e.RowIndex)
                        f.Dispose()
                    End If
                Case dgcAccountTitle.Index
                    Dim f As New frmCOA_Search
                    f.ShowDialog("AccntTitle", dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value.ToString)
                    dgvItemList.Item(dgcAccountCode.Index, e.RowIndex).Value = f.accountcode
                    If IsNothing(f.accountcode) Then
                        dgvItemList.Item(dgcAccountTitle.Index, e.RowIndex).Value = ""
                    Else
                        dgvItemList.Item(dgcAccountTitle.Index, e.RowIndex).Value = f.accttile
                    End If
                    f.Dispose()
            End Select
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Public Sub LoadItem(ByVal ItemCode As String, Optional UOM As String = "")
        Dim query As String
        query = " SELECT  ItemGroup, ItemCode, ItemName, ItemUOM  " & _
                " FROM    tblItem_Master " & _
                " WHERE   ItemCode ='" & ItemCode & "'"
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            If UOM = "" Then
                UOM = SQL.SQLDR("ItemUOM").ToString
            End If
            dgvItemList.Rows.Add(New String() {SQL.SQLDR("ItemGroup").ToString, _
                                                 SQL.SQLDR("ItemCode").ToString, "", _
                                               SQL.SQLDR("ItemName").ToString, "", "0.00", "0.00", "0.00", _
                                               UOM, 1})
            LoadUOM(ItemCode, dgvItemList.Rows.Count - 2)
        End While
        LoadStock()
        LoadBarCode()
    End Sub



    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("PRQ_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            BOM_ID = ""
            PRNo = ""
            LoadSetup()

            dgvItemList.Columns(dgcBOMQTY.Index).Visible = False
            dgvItemList.Columns(dgcStock.Index).Visible = False

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


    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("PRQ_EDIT") Then
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
            tsbCopy.Enabled = False
            tsbPrint.Enabled = False
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click

        If cbPurchaseType.SelectedIndex = -1 Then
            Msg("Please select purchase type!", MsgBoxStyle.Exclamation)
        ElseIf RecordsValidated() Then
            If TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID(ColumnID, DBTable)
                    PRNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    txtTransNum.Text = PRNo
                    SavePR()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    PRNo = txtTransNum.Text
                    LoadPR(PRNo)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    UpdatePR()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    PRNo = txtTransNum.Text
                    LoadPR(PRNo)
                End If
            End If
        End If
    End Sub

    Private Function RecordsValidated() As Boolean
        Dim ctr As Integer = 0
        Dim valid As Boolean = True
        For Each row As DataGridViewRow In dgvItemList.Rows
            If cbPurchaseType.SelectedItem = "Goods (Stock)" Then
                If row.Cells(chItemCode.Index).Value <> Nothing AndAlso row.Cells(chItemCode.Index).Value <> "" _
                    AndAlso (Not IsNumeric(row.Cells(chPRQTY.Index).Value)) Then
                    Msg("Please input quantity for this item!", MsgBoxStyle.Exclamation)
                    valid = False
                    Exit For
                ElseIf row.Cells(chItemCode.Index).Value <> Nothing AndAlso Not IsNumeric(row.Cells(chPRQTY.Index).Value) AndAlso row.Cells(chPRQTY.Index).Value <> 0 Then
                    Msg("No Item Selected for this quantity!", MsgBoxStyle.Exclamation)
                    valid = False
                    Exit For
                ElseIf row.Cells(chItemCode.Index).Value <> Nothing AndAlso row.Cells(chPRQTY.Index).Value <> Nothing Then
                    ctr += 1
                End If
            ElseIf cbPurchaseType.SelectedItem = "Services" Or cbPurchaseType.SelectedItem = "Non-Stock" Then
                If row.Cells(chItemDesc.Index).Value <> Nothing AndAlso row.Cells(chPRQTY.Index).Value = Nothing Then
                    Msg("Please input quantity for this item!", MsgBoxStyle.Exclamation)
                    valid = False
                    Exit For
                ElseIf row.Cells(chItemDesc.Index).Value <> Nothing AndAlso row.Cells(chPRQTY.Index).Value = Nothing Then
                    Msg("No Item Selected for this quantity!", MsgBoxStyle.Exclamation)
                    valid = False
                    Exit For
                ElseIf row.Cells(chItemDesc.Index).Value <> Nothing AndAlso row.Cells(chPRQTY.Index).Value <> Nothing Then
                    ctr += 1
                End If
            End If
        Next
        If ctr = 0 Then
            Msg("Please enter item/services to purchase", MsgBoxStyle.Exclamation)
            valid = False
        End If
        Return valid
    End Function

    Private Sub SavePR()
        Try
            If cbGLAccount.SelectedIndex = -1 Then
                AccntCode = ""
            Else
                AccntCode = GetAccntCode(cbGLAccount.SelectedItem)
            End If
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                         " tblPR(TransID, PR_No, BranchCode, BusinessCode, DatePR, AccntCode, PurchaseType, Remarks, DateNeeded, RequestedBy, BOM_Ref, SO_Ref, WhoCreated, Status, VCECode) " & _
                         " VALUES(@TransID, @PR_No, @BranchCode, @BusinessCode, @DatePR, @AccntCode, @PurchaseType, @Remarks, @DateNeeded, @RequestedBy, @BOM_Ref, @SO_Ref, @WhoCreated, @Status, @VCECode)"
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@PR_No", PRNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DatePR", dtpDocDate.Value.Date)
            SQL.AddParam("@AccntCode", AccntCode)
            SQL.AddParam("@PurchaseType", cbPurchaseType.SelectedItem)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateNeeded", dtpDelivery.Value.Date)
            SQL.AddParam("@RequestedBy", cbDeliverTo.Text)
            SQL.AddParam("@BOM_Ref", BOM_ID)
            SQL.AddParam("@SO_Ref", SO_ID)
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            For Each row As DataGridViewRow In dgvItemList.Rows
                If cbPurchaseType.SelectedItem = "Goods (Stock)" Then
                    If Not IsNothing(row.Cells(chItemCode.Index).Value) OrElse Not IsNothing(row.Cells(dgcItemGroup.Index).Value) Then
                        If IsNothing(row.Cells(dgcAccountCode.Index).Value) Then AccntCode = "" Else AccntCode = row.Cells(dgcAccountCode.Index).Value
                        insertSQL = " INSERT INTO " &
                           " tblPR_Details(TransID, ItemGroup, ItemCode, Description, BOM_UOM, BOMQTY, InStock, QTY, ReserveQTY, UOM, AccntCode, LineNum, WhoCreated) " &
                           " VALUES(@TransID, @ItemGroup, @ItemCode, @Description, @BOM_UOM, @BOMQTY, @InStock, @QTY, @ReserveQTY, @UOM, @AccntCode, @LineNum, @WhoCreated)"
                        Dim BOMQTY, StockQTY, PRQTY, ResQTY As Decimal
                        If IsNumeric(row.Cells(dgcBOMQTY.Index).Value) Then BOMQTY = CDec(row.Cells(dgcBOMQTY.Index).Value) Else BOMQTY = 0
                        If IsNumeric(row.Cells(dgcStock.Index).Value) Then StockQTY = CDec(row.Cells(dgcStock.Index).Value) Else StockQTY = 0
                        If IsNumeric(row.Cells(chPRQTY.Index).Value) Then PRQTY = CDec(row.Cells(chPRQTY.Index).Value) Else PRQTY = 0
                        If StockQTY > BOMQTY Then ResQTY = BOMQTY Else ResQTY = StockQTY
                        SQL.FlushParams()
                        SQL.AddParam("@TransID", TransID)
                        If IsNothing(row.Cells(dgcItemGroup.Index).Value) Then
                            SQL.AddParam("@ItemGroup", "")
                        Else
                            SQL.AddParam("@ItemGroup", row.Cells(dgcItemGroup.Index).Value)
                        End If
                        If cbPurchaseType.SelectedItem = "Goods (Stock)" Then
                            SQL.AddParam("@ItemCode", row.Cells(chItemCode.Index).Value)
                        Else
                            SQL.AddParam("@ItemCode", "")
                        End If
                        SQL.AddParam("@UOM", row.Cells(chUOM_PR.Index).Value)
                        SQL.AddParam("@BOM_UOM", row.Cells(chUOM_BOM.Index).Value)
                        SQL.AddParam("@Description", row.Cells(chItemDesc.Index).Value)
                        SQL.AddParam("@BOMQTY", BOMQTY)
                        SQL.AddParam("@InStock", StockQTY)
                        SQL.AddParam("@QTY", PRQTY)
                        SQL.AddParam("@ReserveQTY", ResQTY)
                        SQL.AddParam("@AccntCode", AccntCode)
                        SQL.AddParam("@LineNum", line)
                        SQL.AddParam("@WhoCreated", UserID)
                        SQL.ExecNonQuery(insertSQL)
                    End If
                Else
                    If Not IsNothing(row.Cells(chItemDesc.Index).Value) Then
                        If IsNothing(row.Cells(dgcAccountCode.Index).Value) Then AccntCode = "" Else AccntCode = row.Cells(dgcAccountCode.Index).Value
                        insertSQL = " INSERT INTO " &
                           " tblPR_Details(TransID, ItemGroup, ItemCode, Description, BOM_UOM, BOMQTY, InStock, QTY, ReserveQTY, UOM, AccntCode, LineNum, WhoCreated) " &
                           " VALUES(@TransID, @ItemGroup, @ItemCode, @Description, @BOM_UOM, @BOMQTY, @InStock, @QTY, @ReserveQTY, @UOM, @AccntCode, @LineNum, @WhoCreated)"
                        Dim BOMQTY, StockQTY, PRQTY, ResQTY As Decimal
                        If IsNumeric(row.Cells(dgcBOMQTY.Index).Value) Then BOMQTY = CDec(row.Cells(dgcBOMQTY.Index).Value) Else BOMQTY = 0
                        If IsNumeric(row.Cells(dgcStock.Index).Value) Then StockQTY = CDec(row.Cells(dgcStock.Index).Value) Else StockQTY = 0
                        If IsNumeric(row.Cells(chPRQTY.Index).Value) Then PRQTY = CDec(row.Cells(chPRQTY.Index).Value) Else PRQTY = 0
                        If StockQTY > BOMQTY Then ResQTY = BOMQTY Else ResQTY = StockQTY
                        SQL.FlushParams()
                        SQL.AddParam("@TransID", TransID)
                        If IsNothing(row.Cells(dgcItemGroup.Index).Value) Then
                            SQL.AddParam("@ItemGroup", "")
                        Else
                            SQL.AddParam("@ItemGroup", row.Cells(dgcItemGroup.Index).Value)
                        End If
                        If cbPurchaseType.SelectedItem = "Goods (Stock)" Then
                            SQL.AddParam("@ItemCode", row.Cells(chItemCode.Index).Value)
                        Else
                            SQL.AddParam("@ItemCode", "")
                        End If
                        SQL.AddParam("@UOM", row.Cells(chUOM_PR.Index).Value)
                        SQL.AddParam("@BOM_UOM", row.Cells(chUOM_BOM.Index).Value)
                        SQL.AddParam("@Description", row.Cells(chItemDesc.Index).Value)
                        SQL.AddParam("@BOMQTY", BOMQTY)
                        SQL.AddParam("@InStock", StockQTY)
                        SQL.AddParam("@QTY", PRQTY)
                        SQL.AddParam("@ReserveQTY", ResQTY)
                        SQL.AddParam("@AccntCode", AccntCode)
                        SQL.AddParam("@LineNum", line)
                        SQL.AddParam("@WhoCreated", UserID)
                        SQL.ExecNonQuery(insertSQL)
                    End If
                End If
                line += 1
            Next

            If BOM_ID <> "" Then
                Dim updateSQL As String
                updateSQL = "UPDATE tblBOM SET PR_Ref ='" & TransID & "' WHERE TransID IN (" & BOM_ID & ") "
                SQL.ExecNonQuery(updateSQL)
            End If

        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "PR_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub UpdatePR()
        Try
            If cbGLAccount.SelectedIndex = -1 Then
                AccntCode = ""
            Else
                AccntCode = GetAccntCode(cbGLAccount.SelectedItem)
            End If
            activityStatus = True

            ' UPDATE PR HEADER
            Dim updateSQL, deleteSQL, insertSQL As String
            updateSQL = " UPDATE tblPR " &
                        " SET    PR_No = @PR_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " &
                        "        DatePR = @DatePR, AccntCode = @AccntCode, PurchaseType = @PurchaseType, Remarks = @Remarks, " &
                        "        DateNeeded = @DateNeeded, RequestedBy = @RequestedBy, BOM_Ref = @BOM_Ref, " &
                        "        DateModified = GETDATE(), WhoModified = @WhoModified, VCECode = @VCECode " &
                        " WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@PR_No", txtTransNum.Text)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DatePR", dtpDocDate.Value.Date)
            SQL.AddParam("@AccntCode", AccntCode)
            SQL.AddParam("@PurchaseType", cbPurchaseType.SelectedItem)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateNeeded", dtpDelivery.Value.Date)
            SQL.AddParam("@RequestedBy", cbDeliverTo.Text)
            SQL.AddParam("@BOM_Ref", BOM_ID)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)

            ' DELETE PR DETAILS
            deleteSQL = " DELETE FROM tblPR_Details WHERE TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.ExecNonQuery(deleteSQL)

            ' INSERT PR DETAILS
            Dim line As Integer = 1
            For Each row As DataGridViewRow In dgvItemList.Rows
                If cbPurchaseType.SelectedItem = "Goods (Stock)" Then
                    If Not IsNothing(row.Cells(chItemCode.Index).Value) OrElse Not IsNothing(row.Cells(dgcItemGroup.Index).Value) Then
                        If IsNothing(row.Cells(dgcAccountCode.Index).Value) Then AccntCode = "" Else AccntCode = row.Cells(dgcAccountCode.Index).Value
                        insertSQL = " INSERT INTO " &
                           " tblPR_Details(TransID, ItemGroup, ItemCode, Description, BOM_UOM, BOMQTY, InStock, QTY, ReserveQTY, UOM, AccntCode, LineNum, WhoCreated) " &
                           " VALUES(@TransID, @ItemGroup, @ItemCode, @Description, @BOM_UOM, @BOMQTY, @InStock, @QTY, @ReserveQTY, @UOM, @AccntCode, @LineNum, @WhoCreated)"
                        Dim BOMQTY, StockQTY, PRQTY, ResQTY As Decimal
                        If IsNumeric(row.Cells(dgcBOMQTY.Index).Value) Then BOMQTY = CDec(row.Cells(dgcBOMQTY.Index).Value) Else BOMQTY = 0
                        If IsNumeric(row.Cells(dgcStock.Index).Value) Then StockQTY = CDec(row.Cells(dgcStock.Index).Value) Else StockQTY = 0
                        If IsNumeric(row.Cells(chPRQTY.Index).Value) Then PRQTY = CDec(row.Cells(chPRQTY.Index).Value) Else PRQTY = 0
                        If StockQTY > BOMQTY Then ResQTY = BOMQTY Else ResQTY = StockQTY
                        SQL.FlushParams()
                        SQL.AddParam("@TransID", TransID)
                        If IsNothing(row.Cells(dgcItemGroup.Index).Value) Then
                            SQL.AddParam("@ItemGroup", "")
                        Else
                            SQL.AddParam("@ItemGroup", row.Cells(dgcItemGroup.Index).Value)
                        End If
                        If cbPurchaseType.SelectedItem = "Goods (Stock)" Then
                            SQL.AddParam("@ItemCode", row.Cells(chItemCode.Index).Value)
                        Else
                            SQL.AddParam("@ItemCode", "")
                        End If
                        SQL.AddParam("@UOM", row.Cells(chUOM_PR.Index).Value)
                        SQL.AddParam("@BOM_UOM", row.Cells(chUOM_BOM.Index).Value)
                        SQL.AddParam("@Description", row.Cells(chItemDesc.Index).Value)
                        SQL.AddParam("@BOMQTY", BOMQTY)
                        SQL.AddParam("@InStock", StockQTY)
                        SQL.AddParam("@QTY", PRQTY)
                        SQL.AddParam("@ReserveQTY", ResQTY)
                        SQL.AddParam("@AccntCode", AccntCode)
                        SQL.AddParam("@LineNum", line)
                        SQL.AddParam("@WhoCreated", UserID)
                        SQL.ExecNonQuery(insertSQL)

                    End If
                Else
                    If Not IsNothing(row.Cells(chItemDesc.Index).Value) Then
                        If IsNothing(row.Cells(dgcAccountCode.Index).Value) Then AccntCode = "" Else AccntCode = row.Cells(dgcAccountCode.Index).Value
                        insertSQL = " INSERT INTO " &
                           " tblPR_Details(TransID, ItemGroup, ItemCode, Description, BOM_UOM, BOMQTY, InStock, QTY, ReserveQTY, UOM, AccntCode, LineNum, WhoCreated) " &
                           " VALUES(@TransID, @ItemGroup, @ItemCode, @Description, @BOM_UOM, @BOMQTY, @InStock, @QTY, @ReserveQTY, @UOM, @AccntCode, @LineNum, @WhoCreated)"
                        Dim BOMQTY, StockQTY, PRQTY, ResQTY As Decimal
                        If IsNumeric(row.Cells(dgcBOMQTY.Index).Value) Then BOMQTY = CDec(row.Cells(dgcBOMQTY.Index).Value) Else BOMQTY = 0
                        If IsNumeric(row.Cells(dgcStock.Index).Value) Then StockQTY = CDec(row.Cells(dgcStock.Index).Value) Else StockQTY = 0
                        If IsNumeric(row.Cells(chPRQTY.Index).Value) Then PRQTY = CDec(row.Cells(chPRQTY.Index).Value) Else PRQTY = 0
                        If StockQTY > BOMQTY Then ResQTY = BOMQTY Else ResQTY = StockQTY
                        SQL.FlushParams()
                        SQL.AddParam("@TransID", TransID)
                        If IsNothing(row.Cells(dgcItemGroup.Index).Value) Then
                            SQL.AddParam("@ItemGroup", "")
                        Else
                            SQL.AddParam("@ItemGroup", row.Cells(dgcItemGroup.Index).Value)
                        End If
                        If cbPurchaseType.SelectedItem = "Goods (Stock)" Then
                            SQL.AddParam("@ItemCode", row.Cells(chItemCode.Index).Value)
                        Else
                            SQL.AddParam("@ItemCode", "")
                        End If
                        SQL.AddParam("@UOM", row.Cells(chUOM_PR.Index).Value)
                        SQL.AddParam("@BOM_UOM", row.Cells(chUOM_BOM.Index).Value)
                        SQL.AddParam("@Description", row.Cells(chItemDesc.Index).Value)
                        SQL.AddParam("@BOMQTY", BOMQTY)
                        SQL.AddParam("@InStock", StockQTY)
                        SQL.AddParam("@QTY", PRQTY)
                        SQL.AddParam("@ReserveQTY", ResQTY)
                        SQL.AddParam("@AccntCode", AccntCode)
                        SQL.AddParam("@LineNum", line)
                        SQL.AddParam("@WhoCreated", UserID)
                        SQL.ExecNonQuery(insertSQL)
                    End If
                End If
                line += 1
            Next


            If BOM_ID <> "" Then
                updateSQL = "UPDATE tblBOM SET PR_Ref ='" & TransID & "' WHERE TransID IN ('" & BOM_ID & "') "
                SQL.ExecNonQuery(updateSQL)
            End If
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "PR_No", PRNo, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub


    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("PRQ_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("PRQ")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadPR(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("PRQ_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE  tblPR SET Status ='Cancelled' WHERE PR_No = @PR_No "
                        SQL.FlushParams()
                        PRNo = txtTransNum.Text
                        SQL.AddParam("@PR_No", PRNo)
                        SQL.ExecNonQuery(deleteSQL)
                        Msg("Record cancelled successfully", MsgBoxStyle.Information)

                        tsbSearch.Enabled = True
                        tsbNew.Enabled = True
                        tsbEdit.Enabled = False
                        tsbSave.Enabled = False
                        tsbCancel.Enabled = False
                        tsbClose.Enabled = False
                        tsbCopy.Enabled = False
                        tsbPrevious.Enabled = False
                        tsbNext.Enabled = False
                        tsbExit.Enabled = True
                        tsbPrint.Enabled = True
                        EnableControl(False)

                        PRNo = txtTransNum.Text
                        LoadPR(PRNo)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "PR_No", PRNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If PRNo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadPR(PRNo)
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
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If PRNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 PR_No FROM tblPR  WHERE PR_No < '" & PRNo & "' ORDER BY PR_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                PRNo = SQL.SQLDR("PR_No").ToString
                LoadPR(PRNo)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If PRNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 PR_No FROM tblPR  WHERE PR_No > '" & PRNo & "' ORDER BY PR_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                PRNo = SQL.SQLDR("PR_No").ToString
                LoadPR(PRNo)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub


    Private Sub frmPR_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            End If
        ElseIf e.Alt = True Then
            If e.KeyCode = Keys.F4 Then
                If tsbExit.Enabled = True Then
                    tsbExit.PerformClick()
                Else
                    e.SuppressKeyPress = True
                End If
            End If
        ElseIf e.Shift = True Then
            If e.KeyCode = Keys.C Then
                If tsbCopy.Enabled = True Then tsbCopy.ShowDropDown()
            End If
        ElseIf e.KeyCode = Keys.Escape Then
            If tsbClose.Enabled = True Then tsbClose.PerformClick()
        End If
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        'If txtBOMRef.Text = "" Then
        Dim f As New frmReport_Display
        f.ShowDialog("PR", TransID)
        f.Dispose()
        'Else
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("PR_BOM", TransID)
        '    f.Dispose()
        'End If

    End Sub

    Private Sub TestToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles TestToolStripMenuItem1.Click
        Dim f As New frmReport_Filter
        f.Report = "PR List"
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub cbPurchType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbPurchaseType.SelectedIndexChanged
        If disableEvent = False Then
            If cbPurchaseType.SelectedItem = "Goods (Stock)" Then
                cbDeliverTo.SelectedIndex = -1
                cbDeliverTo.Items.Clear()
                RefreshDatagrid()
            ElseIf cbPurchaseType.SelectedItem = "Non-Stock" Then
                RefreshDatagrid()
                LoadDepartment()
            Else
                RefreshDatagrid()
                LoadDepartment()
            End If
        End If
    End Sub

    Private Sub cbPurchType_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbPurchaseType.KeyPress
        e.Handled = True
    End Sub

    Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvItemList.EditingControlShowing

        ' GET THE EDITING CONTROL
        Dim editingComboBox As ComboBox = TryCast(e.Control, ComboBox)
        If Not editingComboBox Is Nothing Then
            ' REMOVE AN EXISTING EVENT-HANDLER TO AVOID ADDING MULTIPLE HANDLERS WHEN THE EDITING CONTROL IS REUSED
            RemoveHandler editingComboBox.SelectionChangeCommitted, New EventHandler(AddressOf editingComboBox_SelectionChangeCommitted)

            ' ADD THE EVENT HANDLER
            AddHandler editingComboBox.SelectionChangeCommitted, AddressOf editingComboBox_SelectionChangeCommitted

            ' PREVENT THIS HANDLER FROM FIRING TWICE
            RemoveHandler dgvItemList.EditingControlShowing, AddressOf DataGridView1_EditingControlShowing
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
                LoadStock()
                LoadBarCode()
            End If
        End If

        'Remove the handle to this event. It will be readded each time a new combobox selection causes the EditingControlShowing Event to fire
        RemoveHandler editingComboBox.SelectionChangeCommitted, AddressOf editingComboBox_SelectionChangeCommitted
        'Re-enable the EditingControlShowing event so the above can take place.
        AddHandler dgvItemList.EditingControlShowing, AddressOf DataGridView1_EditingControlShowing
    End Sub


    Private Sub TextBox_keyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        If ControlChars.Back <> e.KeyChar Then
            If Not (Char.IsDigit(CChar(CStr(e.KeyChar))) Or e.KeyChar = ".") Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub PRWithoutPOToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PRWithoutPOToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("PR_WithoutPO", "", "Summary")
        f.Dispose()
    End Sub

    Private Sub tsbCopyPR_Click(sender As System.Object, e As System.EventArgs) Handles tsbCopyPR.Click
        Dim f As New frmLoadTransactions
        Dim transidPR As String = ""
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.chkBatch.Visible = True
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("BOM_PR")
        BOM_ID = ""
        SO_ID = 0

        If f.batch = True Then
            For Each row As DataGridViewRow In f.dgvList.Rows
                If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                    If transidPR = "" Then
                        transidPR = row.Cells(0).Value
                    Else
                        transidPR = transidPR & "," & row.Cells(0).Value
                    End If
                End If
            Next
            LoadBOM(transidPR)
        Else
            If f.transID <> "" Then
                LoadBOM(f.transID)
            End If
        End If

        f.Dispose()
    End Sub

    Private Sub LoadBOM(ByVal ID As String)
        cbPurchaseType.SelectedItem = "Goods (Stock)"
        Dim query As String
        'query = " SELECT    TransID, BOM_No, Remarks " & _
        '         " FROM     tblBOM " & _
        '         " WHERE    TransId = '" & ID & "' "
        'SQL.FlushParams()
        'SQL.ReadQuery(query)
        'If SQL.SQLDR.Read Then
        '    txtRemarks.Text = SQL.SQLDR("Remarks").ToString
        BOM_ID = ID
        Dim stockFilter As String
        If (cbStock.SelectedIndex = -1 Or cbStock.SelectedItem = "ALL") Then
            stockFilter = ""
        Else
            stockFilter = " WHERE " & PRstock & " ='" & cbStock.SelectedItem & "' "
        End If
        dgvItemList.Rows.Clear()
        query = " SELECT tblBOM_Details.ItemGroup, tblBOM_Details.ItemCode, Description,  " &
                " tblBOM_Details.UOM, SUM(ISNULL(GrossQTY,0)) AS GrossQTY, ISNULL(QTY,0) AS Stock,  " &
                " SUM(ISNULL(GrossQTY,0)) - ISNULL(QTY,0) AS Lacking, " &
                " ISNULL(PD_UOM,tblBOM_Details.UOM) AS PD_UOM, " &
                " CEILING((SUM(ISNULL(GrossQTY,0)) - ISNULL(QTY,0))/ ISNULL(dbo.fn_GetUOMQTY(tblBOM_Details.ItemCode,PD_UOM,tblBOM_Details.UOM),1)) AS PR_QTY, " &
                " AD_Inv " &
                " FROM tblBOM_Details LEFT JOIN " &
                " ( " &
                " SELECT ItemCode, SUM(QTY) AS QTY         " &
                " FROM viewItem_Stock   " & stockFilter &
                " GROUP BY ItemCode " &
                " ) AS Stocks   " &
                " ON tblBOM_Details.ItemCode  = Stocks.ItemCode   " &
                " LEFT JOIN tblItem_Master " &
                " ON tblBOM_Details.ItemCode  = tblItem_Master.ItemCode   " &
                " WHERE tblBOM_Details.TransID IN (" & ID & ") " &
                " GROUP BY tblBOM_Details.ItemGroup, tblBOM_Details.ItemCode, Description, tblBOM_Details.UOM, PD_UOM,   " &
                " dbo.fn_GetUOMQTY(tblBOM_Details.ItemCode,PD_UOM,tblBOM_Details.UOM), ISNULL(QTY,0), AD_Inv "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                Dim MOQ As Decimal = GetMOQ(row(1).ToString)
                Dim BOMQTY As Decimal = row(4)
                Dim Stock As Decimal = row(5)
                Dim Lacking As Decimal = row(6)
                Dim PR_UOM As String = row(7)
                Dim PRQTY As Decimal = row(8)

                If Lacking <= 0 Then
                    Lacking = 0
                    PRQTY = 0
                End If
                dgvItemList.Rows.Add({row(0).ToString, row(1).ToString, "", row(2).ToString, row(3).ToString, _
                                      CDec(BOMQTY).ToString("N2"), CDec(Stock).ToString("N2"), CDec(Lacking).ToString("N2"), _
                                      PR_UOM.ToString, PRQTY.ToString("N2"), row(9).ToString, GetAccntTitle(row(9).ToString)})
                LoadUOM(row(1).ToString, dgvItemList.Rows.Count - 2)
            Next
        End If
        LoadStock()
        LoadBarCode()
        dgvItemList.Columns(dgcBOMQTY.Index).ReadOnly = True
        dgvItemList.Columns(dgcBOMQTY.Index).Visible = True
        dgvItemList.Columns(chUOM_BOM.Index).Visible = True
        dgvItemList.Columns(chLacking.Index).Visible = True
        dgvItemList.Columns(dgcStock.Index).Visible = True

        'End If
    End Sub

    Private Sub LoadSO(ByVal ID As String)
        cbPurchaseType.SelectedItem = "Goods (Stock)"
        Dim query As String
        Dim BOM As Integer
        query = " SELECT    TransID, SO_No, Remarks " &
                 " FROM     tblSO " &
                 " WHERE    TransId = '" & ID & "' "
        SQL.FlushParams()
        SQL.ReadQuery(query)
        SO_ID = ID
        dgvItemList.Rows.Clear()
        If SQL.SQLDR.Read Then
            txtSORef.Text = SQL.SQLDR("SO_No").ToString
            Dim stockFilter As String
            If (cbStock.SelectedIndex = -1 Or cbStock.SelectedItem = "ALL") Then
                stockFilter = ""
            Else
                stockFilter = " WHERE " & PRstock & " ='" & cbStock.SelectedItem & "' "
            End If
            query = "  SELECT tblBOM_Details.ItemGroup, tblBOM_Details.ItemCode, Description,  " &
                    " tblBOM_Details.UOM, SUM(ISNULL(GrossQTY,0)) AS GrossQTY,  " &
                    " ISNULL(QTY,0) AS Stock, AD_Inv  FROM tblBOM_Details " &
                    "  LEFT JOIN tblItem_Master  ON        " &
                    "     tblBOM_Details.ItemCode  = tblItem_Master.ItemCode  AND       isPurchase = 1   " &
                    "  LEFT JOIN   " &
                    " (  " &
                    " SELECT ItemCode, SUM(QTY) AS QTY         " &
                    " FROM viewItem_Stock        " &
                    " GROUP BY ItemCode   " &
                    "  ) AS Stocks  ON         " &
                    " tblBOM_Details.ItemCode  = Stocks.ItemCode " &
                    " WHERE TransID IN  " &
                    " (  " &
                    " SELECT Distinct tblBOM_Details.TransID as BOM_TransID " &
                    " FROM tblBOM_Details   " &
                    " INNER JOIN  tblBOM ON   " &
                    " tblBOM.TransID = tblBOM_Details.TransID   " &
                    " LEFT JOIN  tblJO ON   " &
                    " tblJO.TransID = tblBOM.JO_Ref   " &
                    " LEFT JOIN  tblSO ON   " &
                    " tblSO.TransID = tblJO.SO_Ref  " &
                    " WHERE tblSO.TransID = '" & ID & "' AND tblBOM.TransID  " &
                    " NOT IN (SELECT BOM_Ref FROM tblPR WHERE Status in ('Active', 'Closed'))  " &
                    " ) " &
                    " GROUP BY tblBOM_Details.ItemGroup, tblBOM_Details.ItemCode, Description, tblBOM_Details.UOM, QTY, AD_Inv "
            SQL.GetQuery(query)
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                Dim PRQTY As Decimal = 0
                For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                    Dim MOQ As Decimal = GetMOQ(row(1).ToString)

                    If row(5) > row(4) Then ' IF AVAILABLE STOCK IS GREATER THAN THE BOM REQUIREMENT THEN PRQTY SHOULD BE ZERO
                        PRQTY = 0
                    Else ' IF AVAILABLE STOCK IS LESS THAN THE BOM REQUIREMENT THEN PRQTY SHOULD BE BOM QTY - STOCK
                        PRQTY = row(4) - row(5)

                        If PRQTY > MOQ Then ' IF PR QUANTITY IS GREATER THAN MOQ OF ITEM
                            PRQTY = Math.Ceiling(PRQTY)
                        Else
                            PRQTY = MOQ
                        End If
                    End If
                    dgvItemList.Rows.Add({row(0).ToString, row(1).ToString, "", row(2).ToString, _
                                          row(3).ToString, CDec(row(4)).ToString("N2"), CDec(row(5)).ToString("N2"), _
                                          PRQTY.ToString("N2"), row(6).ToString, GetAccntTitle(row(6).ToString)})
                    LoadUOM(row(1).ToString, dgvItemList.Rows.Count - 2)
                Next
            End If

            LoadStock()
            LoadBarCode()
            dgvItemList.Columns(dgcBOMQTY.Index).ReadOnly = True
            dgvItemList.Columns(dgcBOMQTY.Index).Visible = True
            dgvItemList.Columns(dgcStock.Index).Visible = True
        End If
    End Sub

    Private Sub LoadStock()
        Dim query As String
        Dim stockFilter, itemCode As String
        Dim BOMQTY As Decimal = 0
        Dim StockQTY As Decimal = 0
        Dim ResQTY As Decimal = 0
        Dim PRQTY As Decimal = 0
        If (cbStock.SelectedIndex = -1 Or cbStock.SelectedItem = "ALL") Then
            stockFilter = " "
        Else
            stockFilter = " AND " & PRstock & " ='" & cbStock.SelectedItem & "' "
        End If
        For Each row As DataGridViewRow In dgvItemList.Rows
            If Not IsNothing(row.Cells(chItemCode.Index).Value) Then
                itemCode = row.Cells(chItemCode.Index).Value.ToString

                If Not IsNumeric(row.Cells(dgcBOMQTY.Index).Value) Then BOMQTY = 0 Else BOMQTY = CDec(row.Cells(dgcBOMQTY.Index).Value)
                If Not IsNumeric(row.Cells(dgcReserveQTY.Index).Value) Then ResQTY = 0 Else ResQTY = CDec(row.Cells(dgcReserveQTY.Index).Value)
                ' QUERY STOCK
                query = "   SELECT ISNULL(SUM(QTY),0) AS QTY " &
                        "   FROM viewItem_Stock " &
                        "   WHERE ItemCode ='" & itemCode & "' " & stockFilter
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    StockQTY = SQL.SQLDR("QTY")
                    'QUERY RESERVE STOCK
                    Dim ForReserve As Decimal
                    query = " SELECT ISNULL(SUM(ReserveQTY),0) AS ReserveQTY " &
                       " FROM viewPR_Reserve " &
                       " WHERE ItemCode ='" & itemCode & "' "
                    SQL.ReadQuery(query)
                    If SQL.SQLDR.Read Then
                        ForReserve = SQL.SQLDR("ReserveQTY")
                    Else
                        ForReserve = 0
                    End If

                    StockQTY = StockQTY - ForReserve + ResQTY

                    Dim MOQ As Decimal = GetMOQ(itemCode)
                    If StockQTY >= BOMQTY Then ' IF AVAILABLE STOCK IS GREATER THAN THE BOM REQUIREMENT THEN PRQTY SHOULD BE ZERO
                        PRQTY = 0
                    Else ' IF AVAILABLE STOCK IS LESS THAN THE BOM REQUIREMENT THEN PRQTY SHOULD BE BOM QTY - STOCK
                        PRQTY = BOMQTY - IIf(StockQTY < 0, 0, StockQTY)
                        If PRQTY > MOQ Then ' IF PR QUANTITY IS GREATER THAN MOQ OF ITEM
                            PRQTY = Math.Ceiling(PRQTY)
                        Else
                            PRQTY = MOQ
                        End If
                    End If
                End If
                row.Cells(dgcStock.Index).Value = StockQTY
                row.Cells(chPRQTY.Index).Value = PRQTY

            End If

        Next
    End Sub

    Private Sub LoadBarCode()
        Dim query As String
        Dim itemCode, UOM As String
        Dim Barcode As String
        For Each row As DataGridViewRow In dgvItemList.Rows
            If Not IsNothing(row.Cells(chItemCode.Index).Value) AndAlso Not IsNothing(row.Cells(chUOM_PR.Index).Value) Then
                itemCode = row.Cells(chItemCode.Index).Value.ToString
                UOM = row.Cells(chUOM_PR.Index).Value.ToString
                ' QUERY Barcode
                query = " SELECT Barcode FROM tblItem_Barcode WHERE UOM = @UOM AND ItemCode= @ItemCode AND STATUS <> 'Inactive'"
                SQL.FlushParams()
                SQL.AddParam("@ItemCode", itemCode)
                SQL.AddParam("@UOM", UOM)
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    Barcode = SQL.SQLDR("Barcode")
                    row.Cells(chBarcode.Index).Value = Barcode
                Else
                    row.Cells(chBarcode.Index).Value = ""
                End If
            End If

        Next
    End Sub

    Private Function GetMOQ(ItemCode As String) As Decimal
        Dim query As String
        query = " SELECT ISNULL(PD_ReorderQTY,0) AS PD_ReorderQTY FROM tblItem_Master WHERE ItemCode = '" & ItemCode & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("PD_ReorderQTY")
        Else
            Return 0
        End If
    End Function

    Private Sub cbStock_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbStock.SelectedIndexChanged
        If disableEvent = False Then
            LoadStock()
        End If
    End Sub

    Private Sub RefreshDatagrid()
        If cbPurchaseType.SelectedIndex <> -1 Then
            If cbPurchaseType.SelectedItem = "Goods (Stock)" Then
                dgvItemList.Columns(dgcItemGroup.Index).Visible = True
                dgvItemList.Columns(chItemCode.Index).Visible = True
                dgvItemList.Columns(chUOM_PR.Index).Visible = True
                dgvItemList.Columns(dgcAccountTitle.Index).Visible = False
                'dgvItemList.Columns(chUOM_PR.Index).ReadOnly = True
                lblGL.Visible = False
                cbGLAccount.Visible = False
            ElseIf cbPurchaseType.SelectedItem = "Non-Stock" Then
                dgvItemList.Columns(dgcItemGroup.Index).Visible = False
                dgvItemList.Columns(chItemCode.Index).Visible = False
                dgvItemList.Columns(chUOM_PR.Index).Visible = True
                lblGL.Visible = False
                cbGLAccount.Visible = False
                dgvItemList.Columns(dgcAccountTitle.Index).Visible = True
                ' dgvItemList.Columns(chUOM_PR.Index).ReadOnly = False
            ElseIf cbPurchaseType.SelectedItem = "Services" Then
                dgvItemList.Columns(dgcItemGroup.Index).Visible = False
                dgvItemList.Columns(chItemCode.Index).Visible = False
                dgvItemList.Columns(chUOM_PR.Index).Visible = False
                lblGL.Visible = False
                cbGLAccount.Visible = False
                dgvItemList.Columns(dgcAccountTitle.Index).Visible = True
            End If
        End If
    End Sub

    Private Sub PrintCVToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PrintCVToolStripMenuItem.Click
        'If txtBOMRef.Text = "" Then
        Dim f As New frmReport_Display
        f.ShowDialog("PR", TransID)
        f.Dispose()
        'Else
        '    Dim f As New frmReport_Display
        '    f.ShowDialog("PR_BOM", TransID)
        '    f.Dispose()
        'End If
    End Sub

    Private Sub ChequieToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ChequieToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("PR_Detailed", TransID)
        f.Dispose()
    End Sub

    Private Sub ToolStripSplitButton1_ButtonClick(sender As System.Object, e As System.EventArgs) Handles ToolStripSplitButton1.ButtonClick

    End Sub

    Private Sub FromSOToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromSOToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("PR_SO")
        BOM_ID = 0
        SO_ID = 0
        If f.transID <> "" Then
            LoadSO(f.transID)
        End If

        f.Dispose()
    End Sub

    Private Sub dgvItemList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellContentClick

    End Sub

    Private Sub ToolStrip1_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub

    Private Sub FromItemMasterPreferredSupplierToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromItemMasterPreferredSupplierToolStripMenuItem.Click
        If txtVCECode.Text <> "" Then
            LoadSuppliersItem(txtVCECode.Text)
        Else
            MsgBox("Please select supplier first!", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub btnSearchVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchVCE.Click
        Dim f As New frmVCE_Search
        f.Type = "Vendor"
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        txtVCEName.Text = f.VCEName
        LoadCurrency()
        f.Dispose()
    End Sub


    Private Sub LoadCurrency()
        cbCurrency.Items.Clear()
        For Each item In LoadVCECurrency(txtVCECode.Text)
            cbCurrency.Items.Add(item)
        Next
        If cbCurrency.Items.Count = 0 Then
            cbCurrency.Items.Add(BaseCurrency)
        End If
        cbCurrency.SelectedIndex = 0

    End Sub

    Private Sub txtVCEName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.Type = "Vendor"
            f.txtFilter.Text = txtVCEName.Text
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            txtVCEName.Text = f.VCEName
            LoadCurrency()
            f.Dispose()
        End If
    End Sub

    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged

    End Sub

    Public Sub LoadSuppliersItem(VCECode As String)
        Try
            Dim query, UOM, ItemCode As String
            Dim QTY As Decimal
            query = " SELECT  ItemGroup, ItemCode,  ItemName, ISNULL(PD_UOM,ItemUOM) AS PD_UOM,  " &
                                    "         ISNULL(PD_UnitCost,ID_SC) AS Net_Price, WHSE, ISNULL(VATable,0) AS VATable, ISNULL(PD_VATinc, 0) AS PD_VATinc, AD_Inv " &
                                    " FROM    viewItem_Cost " &
                                    " WHERE   VCECode = @VCECode "
            SQL.FlushParams()
            SQL.AddParam("@VCECode", VCECode)
            SQL.ReadQuery(query)
            While SQL.SQLDR.Read
                UOM = SQL.SQLDR("PD_UOM").ToString
                ItemCode = SQL.SQLDR("ItemCode").ToString
                QTY = 0
                dgvItemList.Rows.Add(New String() {SQL.SQLDR("ItemGroup").ToString, SQL.SQLDR("ItemCode").ToString, "",
                                                  SQL.SQLDR("ItemName").ToString, "", "0.00", "0.00", "0.00",
                                                  UOM,
                                                  QTY})
                LoadUOM(ItemCode, dgvItemList.Rows.Count - 2)
            End While
            LoadStock()
            LoadBarCode()
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            SQL.FlushParams()
        End Try
    End Sub


    Private Sub dgvItemList_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs)
        If e.ColumnIndex <> -1 AndAlso e.RowIndex <> -1 AndAlso e.Button = MouseButtons.Right Then
            Dim c As DataGridViewCell = sender(e.ColumnIndex, e.RowIndex)
            If Not c.Selected Then
                c.DataGridView.ClearSelection()
                c.DataGridView.CurrentCell = c
                c.Selected = True
            End If
        End If


    End Sub

    Private Sub dgvItemList_KeyDown(sender As Object, e As KeyEventArgs)
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
        If dgvItemList.CurrentCell IsNot Nothing Then
            If dgvItemList.CurrentCell.RowIndex <> -1 Then
                Dim code As String = dgvItemList.Rows(dgvItemList.CurrentCell.RowIndex).Cells(chItemCode.Index).Value.ToString
                If code <> "" Then
                    f.ShowDialog(code)
                Else
                    MsgBox("Please select an item first!", vbExclamation)
                End If
            End If
        End If
        f.Dispose()
    End Sub

    Private Sub FromReorderingPointToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromReorderingPointToolStripMenuItem.Click
        Dim query As String
        Dim MOQ As Decimal = 0
        Dim EOQ As Decimal = 0
        Dim SOH As Decimal = 0
        Dim MIN As Decimal = 0
        Dim diff As Decimal = 0
        Dim SugPRQTY As Decimal = 0
        Dim POQTY As Decimal = 0
        Dim itemStock As Decimal = 0
        Dim VAT, VATInc As Boolean
        Dim Price, VATAmt As Decimal
        Dim UOM As String = ""
        Dim InvAccount As String = ""

        query = "  SELECT  ItemGroup, viewItem_Cost.ItemCode,  ItemName, ISNULL(PD_UOM,ItemUOM) AS PD_UOM,            " & _
                "  ISNULL(PD_UnitCost,ID_SC) AS Net_Price,  ISNULL(VATable,0) AS VATable, ISNULL(PD_VATinc, 0) AS PD_VATinc, AD_Inv,           " & _
                "  Size, Color, VATType, ISNULL(PD_ReorderQTY,0) AS PD_ReorderQTY, ISNULL(QTY,0) AS Stock, ID_Min, PD_EOQ, PD_MOQ" & _
                "   FROM    viewItem_Cost   " & _
                "    LEFT JOIN ( " & _
                "     SELECT ItemCode, UOM, SUM(QTY) AS QTY FROM viewItem_Stock GROUP BY ItemCode, UOM  " & _
                "   ) AS viewItem_Stock  " & _
                "   ON viewItem_Cost.ItemCode = viewItem_Stock.ItemCode  AND viewItem_Cost.PD_UOM = viewItem_Stock.UOM " & _
                "    WHERE ISNULL(viewItem_Stock.QTY,0) <=  ID_Min  AND ISNULL(ID_Min,0) <> 0"
        SQL.ReadQuery(query)
        SQL.GetQuery(query)
        Dim ctr As Integer = 1
        Dim ctrGroup As Integer = 0
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                SOH = CDec(row(12))
                MIN = CDec(row(13))
                EOQ = CDec(row(14))
                MOQ = CDec(row(15))
                SugPRQTY = MIN * EOQ
                diff = MIN - SOH
                POQTY = IIf(MOQ < SugPRQTY, SugPRQTY, MOQ)

                If UOM = "" Then
                    UOM = row(3).ToString
                End If
                VATInc = row(6)
                VAT = row(5)
                Price = CDec(row(4)).ToString("N2")
                If VAT = True Then
                    If VATInc = True Then
                        VATAmt = (Price) / 1.12 * 0.12
                    Else
                        VATAmt = (Price) * 0.12
                    End If
                Else
                    VATAmt = 0
                End If
                If InvAccount = "" Then InvAccount = row(7).ToString
                dgvItemList.Rows.Add(row(0).ToString, row(1).ToString, "", row(2).ToString, "", "0.00", SOH, _
                          "0.00", UOM, CDec(POQTY).ToString("N2"), InvAccount, GetAccntTitle(InvAccount), "0.00")

                LoadUOM(row(1).ToString, dgvItemList.Rows.Count - 2)
                ctr += 1
            Next
        End If
        LoadBarCode()

    End Sub
End Class