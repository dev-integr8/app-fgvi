Public Class frmItem_Master
    Public itemCode As String = ""
    Dim moduleID As String = "ITM"
    Dim disableEvent As Boolean = False
    Dim WHSE As String
    Public Overloads Function ShowDialog(ByVal Code As String) As Boolean
        itemCode = Code
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmItem_Master_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Text = "(" & database & ") - Item Masterfile "
            LoadUomGroup()
            LoadWHSE()
            loadData()
            LoadVAType()

            If itemCode <> "" Then
                If Not AllowAccess("ITM_VIEW") Then
                    msgRestricted()
                    tsbSearch.Enabled = True
                    tsbNew.Enabled = True
                    tsbEdit.Enabled = False
                    tsbSave.Enabled = False
                    tsbDelete.Enabled = False
                    tsbClose.Enabled = False
                    tsbPrevious.Enabled = False
                    tsbNext.Enabled = False
                    tsbExit.Enabled = True
                    EnableControl(False)
                Else
                    LoadItem(itemCode)
                End If
            Else
                tsbSearch.Enabled = True
                tsbNew.Enabled = True
                tsbEdit.Enabled = False
                tsbSave.Enabled = False
                tsbDelete.Enabled = False
                tsbClose.Enabled = False
                tsbPrevious.Enabled = False
                tsbNext.Enabled = False
                tsbExit.Enabled = True
                EnableControl(False)
            End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        End Try
    End Sub

    'Barcode Data'
    Private Sub LoadItem(ByVal ItemCode As String)
        Dim query As String
        query = " SELECT ItemCode, Barcode, ItemName, ItemType, ItemCategory, ItemGroup, ItemUOMGroup, Status, ItemUOM, ISNULL(ItemWeight,0) AS ItemWeight, " &
                "         ISNULL(isInventory,0) AS isInventory,  ISNULL(isSales,0) AS isSales,  ISNULL(isPurchase,0) AS isPurchase,  ISNULL(isProduce,0) AS isProduce,  ISNULL(isFixAsset,0) AS isFixAsset,  ISNULL(isOwned,0) AS isOwned, ItemOwner, " &
                "        ISNULL(PD_UpdateLatest,0) AS PD_UpdateLatest, PD_Supplier,  ISNULL(PD_UnitCost,0) AS PD_UnitCost, PD_UOM, ID_Warehouse,  ISNULL(PD_SafetyStock,0) AS PD_SafetyStock, ISNULL(PD_ReorderQTY,0) AS PD_ReorderQTY,  ISNULL(VATable,0) AS VATable,  ISNULL(PD_VATinc,0) AS PD_VATinc, " &
                "        ISNULL(ID_Max,0) AS ID_Max, ISNULL(ID_Min,0) AS ID_Min, ID_Method, ID_UOM, ID_SC, AD_Sales, AD_COS, AD_Inv, AD_Discount, AD_Expense, ItemDept, AD_AccrudDep, AD_DepExpense, AD_FixedAsset, ISNULL(isMultipleBarcode,0) AS isMultipleBarcode, ISNULL(isConsignment,0) AS isConsignment,  AD_Consignment, ISNULL(DiscountPercent, 'None') AS  DiscountPercent," &
                "        G1, G2, G3, G4, G5, VATType,  ISNULL(PD_MOQ,0) AS PD_MOQ, ISNULL(PD_EOQ,0) AS PD_EOQ" &
                " FROM tblItem_Master " &
                " WHERE ItemCode ='" & ItemCode & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            disableEvent = True
            ItemCode = SQL.SQLDR("ItemCode").ToString
            txtItemCode.Text = SQL.SQLDR("ItemCode").ToString
            txtBarcode.Text = SQL.SQLDR("Barcode").ToString
            txtItemName.Text = SQL.SQLDR("ItemName").ToString
            cbItemType.Text = SQL.SQLDR("ItemType").ToString
            If Not cbItemGroup.Items.Contains(SQL.SQLDR("ItemGroup").ToString) Then
                cbItemGroup.Items.Add(SQL.SQLDR("ItemGroup").ToString)
            End If
            cbItemGroup.SelectedItem = SQL.SQLDR("ItemGroup").ToString
            cbG1.Text = SQL.SQLDR("G1").ToString
            cbG2.Text = SQL.SQLDR("G2").ToString
            cbG3.Text = SQL.SQLDR("G3").ToString
            cbG4.Text = SQL.SQLDR("G4").ToString
            cbG5.Text = SQL.SQLDR("G5").ToString
            If SQL.SQLDR("ItemWeight") = 0 Then txtWeight.Text = "" Else txtWeight.Text = SQL.SQLDR("ItemWeight").ToString
            cbItemCategory.Text = SQL.SQLDR("ItemCategory").ToString
            If Not cbUoMGroup.Items.Contains(SQL.SQLDR("ItemUOMGroup").ToString) Then
                cbUoMGroup.Items.Add(SQL.SQLDR("ItemUOMGroup").ToString)
            End If
            cbUoMGroup.SelectedItem = SQL.SQLDR("ItemUOMGroup").ToString
            cbStatus.SelectedItem = SQL.SQLDR("Status").ToString
            txtUOMBase.Text = SQL.SQLDR("ItemUOM").ToString

            chkInv.Checked = SQL.SQLDR("isInventory")
            chkMultipleBarcode.Checked = SQL.SQLDR("isMultipleBarcode")
            chkSale.Checked = SQL.SQLDR("isSales")
            chkPurch.Checked = SQL.SQLDR("isPurchase")
            chkProd.Checked = SQL.SQLDR("isProduce")
            chkFixedAsset.Checked = SQL.SQLDR("isFixAsset")
            chkConsignment.Checked = SQL.SQLDR("isConsignment")
            If SQL.SQLDR("isOwned") = False Then
                rbIDOwned.Checked = True
            Else
                rbIDOwned.Checked = False
            End If
            cbIDTolling.Text = SQL.SQLDR("ItemOwner").ToString

            ' PURCHASE DATA 
            chkPurchUpdate.Checked = SQL.SQLDR("PD_UpdateLatest")
            txtPurchVCECode.Text = SQL.SQLDR("PD_Supplier").ToString
            txtPurchPrice.Text = CDec(SQL.SQLDR("PD_UnitCost")).ToString("N2")
            txtPurchUOM.Text = SQL.SQLDR("PD_UOM").ToString
            txtPurchMinimum.Text = CDec(SQL.SQLDR("PD_SafetyStock")).ToString("N2")
            txtPurchReorder.Text = CDec(SQL.SQLDR("PD_ReorderQTY")).ToString("N2")
            txtPD_EOQ.Text = CDec(SQL.SQLDR("PD_EOQ")).ToString("N2")
            txtPD_MOQ.Text = CDec(SQL.SQLDR("PD_MOQ")).ToString("N2")
            chkVATable.Checked = SQL.SQLDR("VATable")
            cbVATType.SelectedItem = SQL.SQLDR("VATType").ToString
            chkPurchVATInc.Checked = SQL.SQLDR("PD_VATinc")

            'SC/PWD DEFAULT DISCOUNT PERCENTAGE
            Dim DefaultDiscountPercent As String
            DefaultDiscountPercent = SQL.SQLDR("DiscountPercent")
            If DefaultDiscountPercent = "5" Then
                RdFivePercent.Checked = True
            ElseIf DefaultDiscountPercent = "20" Then
                RdTwentyPercent.Checked = True
            ElseIf DefaultDiscountPercent = "None" Then
                RdNone.Checked = True
            End If

            ' INVENTORY DATA
            If SQL.SQLDR("ID_Max") = 0 Then
                txtInvMax.Text = ""
            Else
                txtInvMax.Text = SQL.SQLDR("ID_Max")
            End If
            If SQL.SQLDR("ID_Min") = 0 Then
                txtInvMin.Text = ""
            Else
                txtInvMin.Text = SQL.SQLDR("ID_Min")
            End If
            WHSE = SQL.SQLDR("ID_Warehouse").ToString
            cbInvMethod.SelectedItem = SQL.SQLDR("ID_Method").ToString
            txtInvUOM.Text = SQL.SQLDR("ID_UOM").ToString
            txtInvStandard.Text = CDec(SQL.SQLDR("ID_SC")).ToString("N2")
            cbDept.Text = SQL.SQLDR("ItemDept").ToString
            disableEvent = False

            ' Accounting DATA
            txtSaleAccntCode.Text = SQL.SQLDR("AD_Sales").ToString
            txtCostAccntCode.Text = SQL.SQLDR("AD_COS").ToString
            txtInvAccntCode.Text = SQL.SQLDR("AD_Inv").ToString
            txtDiscAccntCode.Text = SQL.SQLDR("AD_Discount").ToString
            txtExpAccntCode.Text = SQL.SQLDR("AD_Expense").ToString
            txtAccumDepCode.Text = SQL.SQLDR("AD_AccrudDep").ToString
            txtDepExpCode.Text = SQL.SQLDR("AD_DepExpense").ToString
            txtFixedAssetAccountCode.Text = SQL.SQLDR("AD_FixedAsset").ToString
            txtConAccntCode.Text = SQL.SQLDR("AD_Consignment").ToString

            txtSaleAccntTitle.Text = GetAccntTitle(txtSaleAccntCode.Text)
            txtCostAccntTitle.Text = GetAccntTitle(txtCostAccntCode.Text)
            txtInvAccntTitle.Text = GetAccntTitle(txtInvAccntCode.Text)
            txtDiscAccntTitle.Text = GetAccntTitle(txtDiscAccntCode.Text)
            txtExpAccntTitle.Text = GetAccntTitle(txtExpAccntCode.Text)
            txtAccumDepTitle.Text = GetAccntTitle(txtAccumDepCode.Text)
            txtDepExpTitle.Text = GetAccntTitle(txtDepExpCode.Text)
            txtFixedAssetAccountTitle.Text = GetAccntTitle(txtFixedAssetAccountCode.Text)
            txtConAccntTitle.Text = GetAccntTitle(txtConAccntCode.Text)
            If GetWHSEDesc(WHSE) = "" Then
                cbInvWarehouse.SelectedIndex = -1
            Else
                cbInvWarehouse.SelectedItem = GetWHSEDesc(WHSE)
            End If

            txtPurchVCEname.Text = GetVCEName(txtPurchVCECode.Text)

            LoadPrice("Selling")
            LoadSizeColor()
            'Barcode Data'
            LoadBarcode()
            'Barcode Data'
            LoadWHSE_INVY()

            If cbUoMGroup.Text = "(Manual)" Then
                frmUOMConversion.Dispose()
                frmUOMConversion.LoadItem = True
            End If
            'LoadBOM(txtItemCode.Text)

            ' TOOLSTRIP BUTTONS
            tsbSearch.Enabled = True
            tsbNew.Enabled = True
            tsbEdit.Enabled = True
            tsbSave.Enabled = False
            tsbDelete.Enabled = True
            tsbClose.Enabled = False
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbExit.Enabled = True
            EnableControl(False)
            LoadCharges()
        End If
    End Sub
    'Barcode Data'

    Private Sub LoadPrice(Type As String)
        Dim query As String
        query = " SELECT Type, UnitPrice, UOM, UOMQTY, VATInclusive, VCEName, ISNULL(VATable,0) AS VATable " &
                " FROM   viewItem_Price " &
                " WHERE  Category='" & Type & "' " &
                " AND    ItemCode ='" & txtItemCode.Text & "'" &
                " AND    PriceStatus ='Active' "
        SQL.GetQuery(query)
        dgvSell.Rows.Clear()
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvSell.Rows.Add({row(0).ToString, CDec(row(1)).ToString("N2"), row(2),
                                         row(3), row(6), row(4)})
            Next
        End If
    End Sub


    Private Sub LoadSizeColor()
        Dim query As String
        query = " SELECT Size, Color " &
                " FROM   tblItem_SizeColor " &
                " WHERE  ItemCode ='" & txtItemCode.Text & "'"
        SQL.GetQuery(query)
        dgvSizeColor.Rows.Clear()
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvSizeColor.Rows.Add({row(0).ToString, row(1).ToString})
            Next
        End If
    End Sub

    Private Sub LoadCharges()
        'dgvItemList.Rows.Clear()
        'Dim query As String
        'query = " SELECT    DescRecordID, Description,  " &
        '        "           ISNULL(GrossAmount,0) AS GrossAmount, " &
        '        "            Type, tblItem_Charges.VCECode, VCEName " &
        '        " FROM      tblItem_Charges " &
        '        " LEFT JOIN viewVCE_Master ON " &
        '        " viewVCE_Master.VCECode = tblItem_Charges.VCECode " &
        '        " WHERE     tblItem_Charges.ItemCode = '" & txtItemCode.Text & "' " &
        '        " ORDER BY  LineNum "
        'SQL.ReadQuery(query)
        'While SQL.SQLDR.Read
        '    dgvItemList.Rows.Add("")
        '    dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chBS_RecordID.Index).Value = SQL.SQLDR("DescRecordID").ToString
        '    dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chBS_Desc.Index).Value = SQL.SQLDR("Description").ToString
        '    dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chBS_VCECode.Index).Value = SQL.SQLDR("VCECode").ToString
        '    dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chBS_VCEName.Index).Value = SQL.SQLDR("VCEName").ToString
        '    dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chBS_Amount.Index).Value = CDec(SQL.SQLDR("GrossAmount")).ToString("N2")
        '    dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chBS_Type.Index).Value = SQL.SQLDR("Type").ToString
        '    LoadType(dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chBS_RecordID.Index).Value, dgvItemList.Rows.Count - 1)
        'End While
    End Sub

    Private Sub LoadBOM(ItemCode As String)
        Dim ctr As Integer = 1
        Dim query As String
        query = " SELECT RecordID, MaterialCode, tblItem_Master.ItemName, tblBOM.UOM, tblBOM.QTY " &
                " FROM   tblBOM INNER JOIN tblItem_Master " &
                " ON     tblBOM.ItemCode = tblItem_Master.ItemCode  " &
                " WHERE  tblItem_Master.ItemCode ='" & ItemCode & "'" &
                " AND    tblBOM.Status ='Active' "
        SQL.GetQuery(query)
        dgvSell.Rows.Clear()
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvSell.Rows.Add({row(0).ToString, ctr, row(1).ToString, row(2).ToString, row(3).ToString, CDec(row(4)).ToString("N2")})
            Next
        End If
    End Sub

    Private Sub LoadUomGroup()
        Dim temp As String
        Dim query As String
        temp = cbUoMGroup.SelectedItem
        query = " SELECT GroupCode FROM tblUOM_Group WHERE Status ='Active' AND Manual = 0 "
        SQL.ReadQuery(query)
        cbUoMGroup.Items.Clear()
        cbUoMGroup.Items.Add("(Manual)")
        While SQL.SQLDR.Read
            cbUoMGroup.Items.Add(SQL.SQLDR("GroupCode").ToString)
        End While
        If temp <> "" Then
            cbUoMGroup.SelectedItem = temp
        Else
            cbUoMGroup.SelectedItem = "(Manual)"
        End If
        cbUoMGroup.Items.Add("(Add UoM Group)")
    End Sub

    Private Sub LoadBOMGroup()
        Dim temp As String
        Dim query As String
        temp = cbItemGroup.SelectedItem
        query = " SELECT BOMGroup FROM tblBOM_Group WHERE Status ='Active' "
        SQL.ReadQuery(query)
        cbItemGroup.Items.Clear()
        While SQL.SQLDR.Read
            cbItemGroup.Items.Add(SQL.SQLDR("BOMGroup").ToString)
        End While
        If temp <> "" Then
            cbItemGroup.SelectedItem = temp
        Else
            cbItemGroup.SelectedIndex = -1
        End If
    End Sub

    Private Sub LoadWHSE()
        Dim query As String
        query = " SELECT Description FROM tblWarehouse WHERE Status ='Active' "
        SQL.ReadQuery(query)
        cbInvWarehouse.Items.Clear()
        While SQL.SQLDR.Read
            cbInvWarehouse.Items.Add(SQL.SQLDR("Description").ToString)
        End While
    End Sub

    Private Sub btnUOM_Click(sender As System.Object, e As System.EventArgs) Handles btnUOM.Click
        frmItem_UOMlist.ShowDialog()
        If frmItem_UOMlist.UoM <> "" Then
            txtUOMBase.Text = frmItem_UOMlist.UoM
            If txtPurchUOM.Text = "" Then txtPurchUOM.Text = txtUOMBase.Text
            If txtSellUOM.Text = "" Then txtSellUOM.Text = txtUOMBase.Text
            If txtInvUOM.Text = "" Then txtInvUOM.Text = txtUOMBase.Text
        End If
        frmItem_UOMlist.Dispose()
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("ITM_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmItem_Search
            f.ShowDialog()
            If f.ItemCode <> "" Then
                itemCode = f.ItemCode
            End If
            LoadItem(itemCode)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("ITM_ADD") Then
            msgRestricted()
        Else
            txtItemCode.Clear()
            txtItemName.Clear()
            cbItemType.Text = ""
            cbItemCategory.Text = ""
            cbItemGroup.SelectedIndex = -1
            cbG1.Text = ""
            cbG2.Text = ""
            cbG3.Text = ""
            cbG4.Text = ""
            cbG5.Text = ""
            cbUoMGroup.SelectedIndex = -1
            cbStatus.SelectedIndex = 0
            txtUOMBase.Clear()
            txtBarcode.Clear()
            chkInv.Checked = False
            chkProd.Checked = False
            chkPurch.Checked = False
            chkSale.Checked = False
            chkConsignment.Checked = False
            dgvSizeColor.Rows.Clear()
            'Barcode Data'
            chkMultipleBarcode.Checked = False
            'Barcode Data'
            chkFixedAsset.Checked = False
            cbUoMGroup.SelectedItem = "Manual"
            txtWeight.Clear()
            itemCode = ""


            ' PURCHASE DATA
            txtPurchUOM.Clear()
            txtPurchVCECode.Clear()
            txtPurchVCEname.Clear()
            txtPurchPrice.Clear()
            txtPurchUOM.Clear()
            txtPurchMinimum.Clear()
            txtPurchReorder.Clear()
            txtPD_EOQ.Clear()
            txtPD_MOQ.Clear()
            chkPurch.Checked = True

            ' SALES DATA
            dgvSell.Rows.Clear()

            ' INVENTORY DATA
            cbInvWarehouse.SelectedIndex = -1
            cbInvMethod.SelectedIndex = 0
            txtInvMax.Clear()
            txtInvMin.Clear()
            txtInvUOM.Clear()
            dgvInv.Rows.Clear()
            txtInvStock.Clear()
            txtInvOrder.Clear()
            txtInvCommit.Clear()
            txtInvAvailable.Clear()
            cbIDTolling.SelectedIndex = -1
            cbDept.SelectedIndex = -1

            ' ACCOUNTING DATA
            txtCostAccntCode.Clear()
            txtCostAccntTitle.Clear()
            txtDiscAccntCode.Clear()
            txtDiscAccntTitle.Clear()
            txtInvAccntCode.Clear()
            txtInvAccntTitle.Clear()
            txtSaleAccntCode.Clear()
            txtSaleAccntTitle.Clear()
            txtExpAccntCode.Clear()
            txtExpAccntTitle.Clear()
            txtFixedAssetAccountCode.Clear()
            txtFixedAssetAccountTitle.Clear()
            txtDepExpCode.Clear()
            txtDepExpTitle.Clear()
            txtAccumDepCode.Clear()
            txtAccumDepTitle.Clear()
            txtConAccntCode.Clear()
            txtConAccntTitle.Clear()

            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbDelete.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
            frmUOMConversion.group = ""
            frmUOMConversion.Close()
            LoadUomGroup()
            LoadWHSE()
            LoadCategory()
            LoadType()
            LoadBOMGroup()
            EnableControl(True)
        End If
    End Sub

    Private Sub LoadCategory()
        Dim query As String
        query = " SELECT DISTINCT ItemCategory FROM tblItem_Master WHERE Status ='Active' "
        SQL.ReadQuery(query)
        cbItemCategory.Items.Clear()
        While SQL.SQLDR.Read
            cbItemCategory.Items.Add(SQL.SQLDR("ItemCategory").ToString)
        End While
    End Sub

    Private Sub LoadType()
        Dim query As String
        query = " SELECT DISTINCT ItemType FROM tblItem_Master WHERE Status ='Active' "
        SQL.ReadQuery(query)
        cbItemType.Items.Clear()
        While SQL.SQLDR.Read
            cbItemType.Items.Add(SQL.SQLDR("ItemType").ToString)
        End While
    End Sub

    Private Sub LoadDepartment()
        Dim query As String
        query = " SELECT DISTINCT Department FROM tblItem_Master WHERE Status ='Active' "
        SQL.ReadQuery(query)
        cbDept.Items.Clear()
        While SQL.SQLDR.Read
            cbDept.Items.Add(SQL.SQLDR("Department").ToString)
        End While
    End Sub


    Private Sub EnableControl(ByVal Value As Boolean)
        txtItemCode.Enabled = Value
        txtItemName.Enabled = Value
        cbStatus.Enabled = Value
        cbItemType.Enabled = Value
        cbItemCategory.Enabled = Value
        txtBarcode.Enabled = Value
        chkInv.Enabled = Value
        chkProd.Enabled = Value
        chkPurch.Enabled = Value
        chkConsignment.Enabled = Value
        GrpDefaultDiscount.Enabled = Value
        grpSizeColor.Enabled = Value
        'Barcode Data'
        chkMultipleBarcode.Enabled = Value
        'Barcode Data'

        chkSale.Enabled = Value
        chkFixedAsset.Enabled = Value
        btnUOM.Enabled = Value
        btnBOMgroup.Enabled = Value
        cbUoMGroup.Enabled = Value
        btnUOMGroup.Enabled = Value
        chkExcise.Enabled = Value
        chkVATable.Enabled = Value
        cbVATType.Enabled = Value
        cbItemGroup.Enabled = Value
        cbG1.Enabled = Value
        cbG2.Enabled = Value
        cbG3.Enabled = Value
        cbG4.Enabled = Value
        cbG5.Enabled = Value

        rbIDOwned.Enabled = Value
        rbIDTolling.Enabled = Value
        cbIDTolling.Enabled = Value
        txtWeight.Enabled = Value

        ' PURCHASE DATA
        If chkPurch.Checked Then
            txtPurchMinimum.Enabled = Value
            txtPurchPrice.Enabled = Value
            txtPurchReorder.Enabled = Value
            txtPurchVCECode.Enabled = Value
            btnPurchSupplier.Enabled = Value
            btnPurchUOM.Enabled = Value
            txtPurchVCEname.Enabled = Value
            chkPurchVATInc.Enabled = Value
            chkPurchUpdate.Enabled = Value
            txtPD_EOQ.Enabled = Value
            txtPD_MOQ.Enabled = Value
        Else
            txtPurchMinimum.Enabled = False
            txtPurchPrice.Enabled = False
            txtPurchReorder.Enabled = False
            txtPurchVCECode.Enabled = False
            btnPurchSupplier.Enabled = False
            btnPurchUOM.Enabled = False
            txtPurchVCEname.Enabled = False
            chkPurchVATInc.Enabled = False
            chkPurchUpdate.Enabled = False
            txtPD_EOQ.Enabled = False
            txtPD_MOQ.Enabled = False
        End If

        ' SALES DATA
        If chkSale.Checked Then
            cbSellType.Enabled = Value
            chkSellVAT.Enabled = Value
            chkVAT.Enabled = Value
            txtSellPrice.Enabled = Value
            txtSellQTY.Enabled = Value
            btnSellAdd.Enabled = Value
            btnSellRemove.Enabled = Value
            btnSellUOM.Enabled = Value
            btnSalesAccnt.Enabled = Value
        Else
            cbSellType.Enabled = False
            chkSellVAT.Enabled = False
            chkVAT.Enabled = False
            txtSellPrice.Enabled = False
            txtSellQTY.Enabled = False
            btnSellAdd.Enabled = False
            btnSellRemove.Enabled = False
            btnSellUOM.Enabled = False
            btnSalesAccnt.Enabled = False
        End If

        'Barcode Date'
        If chkMultipleBarcode.Checked Then
            txtBarcode_UOM.Enabled = Value
            txtBarcode_Barcode.Enabled = Value
            btnBarcodeAdd.Enabled = Value
            btnBarcodeRemove.Enabled = Value
            btnBarcodeUOM.Enabled = Value
        Else
            txtBarcode_UOM.Enabled = False
            txtBarcode_Barcode.Enabled = False
            btnBarcodeAdd.Enabled = False
            btnBarcodeRemove.Enabled = False
            btnBarcodeUOM.Enabled = False
        End If
        'Barcode Date'

        ' INVENTORY DATA 
        If chkInv.Checked Then
            cbInvMethod.Enabled = Value
            txtInvMin.Enabled = Value
            txtInvMax.Enabled = Value
            btnInvUOM.Enabled = Value
            cbInvWarehouse.Enabled = Value
            txtInvStandard.Enabled = Value
            cbDept.Enabled = Value
        Else
            cbInvMethod.Enabled = False
            txtInvMin.Enabled = False
            txtInvMax.Enabled = False
            btnInvUOM.Enabled = False
            cbInvWarehouse.Enabled = False
            txtInvStandard.Enabled = False
            cbDept.Enabled = False
        End If

        ' PRODUCTION DATA 
        If chkProd.Checked Then
            cbProd.Enabled = Value
            cbMDprodFloor.Enabled = Value
        Else
            cbProd.Enabled = False
            cbMDprodFloor.Enabled = False
        End If

        ' ENTRY DATA

        If chkInv.Checked = False Then ' IF STOCK ITEMS, SHOULD HAVE INVENTORY ACCOUNT
            txtInvAccntTitle.Enabled = False
        Else
            txtInvAccntTitle.Enabled = Value
        End If
        If chkSale.Checked = False Then ' IF SELLING ITEMS, SHOULD HAVE SALES, DISCOUNT AND COST OF SALES ACCOUNT
            txtSaleAccntTitle.Enabled = False
            txtCostAccntTitle.Enabled = False
            txtDiscAccntTitle.Enabled = False
        Else
            txtSaleAccntTitle.Enabled = Value
            txtCostAccntTitle.Enabled = Value
            txtDiscAccntTitle.Enabled = Value
        End If
        If chkInv.Checked = True Then ' IF NON-STOCK ITEMS, SHOULD HAVE DIRECT EXPENSE ACCOUNT
            txtExpAccntTitle.Enabled = False
        Else
            If chkPurch.Checked = False Then
                txtExpAccntTitle.Enabled = False
            Else
                txtExpAccntTitle.Enabled = Value
            End If
        End If
        If chkFixedAsset.Checked = False Then ' IF FIXED ASSET
            txtFixedAssetAccountTitle.Enabled = False
            txtDepExpTitle.Enabled = False
            txtAccumDepTitle.Enabled = False
        Else
            txtFixedAssetAccountTitle.Enabled = Value
            txtDepExpTitle.Enabled = Value
            txtAccumDepTitle.Enabled = Value
        End If

        ' IF CONSIGNMENT ITEM
        If chkConsignment.Checked = False Then
            txtConAccntTitle.Enabled = False
        Else
            txtConAccntTitle.Enabled = Value
        End If
        dgvItemList.AllowUserToAddRows = Value
        dgvItemList.AllowUserToDeleteRows = Value
        dgvItemList.ReadOnly = Not Value
        If Value = True Then
            dgvItemList.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemList.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
    End Sub


    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("ITM_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)

            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbDelete.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
        End If
    End Sub

    Private Sub tsbCloseClick(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        ' Toolstrip Buttons
        If itemCode = "" Then
            tsbNew.PerformClick()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbDelete.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
        Else
            LoadItem(itemCode)
            tsbEdit.Enabled = True
            tsbDelete.Enabled = True
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
        End If
        tsbSearch.Enabled = True
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
    End Sub

    Private Sub tsbDelete_Click(sender As System.Object, e As System.EventArgs) Handles tsbDelete.Click
        If Not AllowAccess("ITM_DEL") Then
            msgRestricted()
        Else
            If txtItemCode.Text <> "" Then
                If MsgBox("Are you sure you want to delete this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " DELETE FROM tblItem_Master WHERE itemCode = @ItemCode "
                        SQL.FlushParams()
                        itemCode = txtItemCode.Text
                        SQL.AddParam("@ItemCode", itemCode)
                        SQL.ExecNonQuery(deleteSQL)
                        Msg("Record deleted successfully", MsgBoxStyle.Information)

                        tsbSearch.Enabled = True
                        tsbNew.Enabled = True
                        tsbEdit.Enabled = False
                        tsbSave.Enabled = False
                        tsbDelete.Enabled = False
                        tsbClose.Enabled = False
                        tsbPrevious.Enabled = False
                        tsbNext.Enabled = False
                        tsbExit.Enabled = True
                        EnableControl(False)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
                    Finally
                        RecordActivity(UserID, moduleID, Me.Name.ToString, "DELETE", "ItemCode", itemCode, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                        tsbNew.PerformClick()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If Validated() Then
            If itemCode = "" Then
                If MsgBox("Saving New Item, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    SaveItem()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    itemCode = txtItemCode.Text
                    LoadItem(itemCode)
                End If
            Else
                If MsgBox("Updating Item Information, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    UpdateItem()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    itemCode = txtItemCode.Text
                    LoadItem(itemCode)
                End If
            End If
        End If
    End Sub

    Private Shadows Function Validated() As Boolean
        If txtItemCode.Text = "" Then
            Msg("Please enter Item Code for this Item!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf cbUoMGroup.SelectedIndex = -1 Then
            Msg("Please select default UoM Group for this item!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf cbUoMGroup.Text = "(Manual)" AndAlso txtUOMBase.Text = "" Then
            Msg("Please select Base UoM for this item!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf chkInv.Checked = True And cbInvWarehouse.SelectedIndex = -1 Then
            Msg("Please select default warehouse for this item!", MsgBoxStyle.Exclamation)
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub SaveItem()
        Try
            activityStatus = True
            ' VALIDATE VALUES
            If cbInvWarehouse.SelectedIndex = -1 Then WHSE = "" Else WHSE = GetWHSECode(cbInvWarehouse.SelectedItem)

            ' INSERT QUERY
            Dim insertSQL As String
            insertSQL = " INSERT INTO " &
                        " tblItem_Master(ItemCode, Barcode, ItemName, ItemType, ItemCategory, ItemGroup, ItemOwner, ItemUOM, " &
                        "        ItemWeight, ItemUOMGroup, isInventory, isSales, isMultipleBarcode, isPurchase, isProduce, isFixAsset, isOwned,  VATable," &
                        "        PD_UpdateLatest, PD_Supplier, PD_UnitCost, PD_UOM, PD_SafetyStock, PD_ReorderQTY,  PD_VATinc, " &
                        "        ID_Warehouse, ID_Max, ID_Min, ID_Method, ID_UOM, ID_SC, AD_Sales, AD_COS, AD_Inv, AD_Discount, AD_Expense," &
                        "        WhoCreated, ItemDept, Status, AD_AccrudDep, AD_DepExpense, AD_FixedAsset, isConsignment, AD_Consignment,DiscountPercent, " &
                        "        G1, G2, G3, G4, G5, VATType, PD_MOQ, PD_EOQ) " &
                        " VALUES(@ItemCode, @Barcode, @ItemName, @ItemType, @ItemCategory, @ItemGroup, @ItemOwner, @ItemUOM, " &
                        "        @ItemWeight, @ItemUOMGroup, @isInventory, @isSales, @isMultipleBarcode, @isPurchase, @isProduce, @isFixAsset, @isOwned, @VATable, " &
                        "        @PD_UpdateLatest, @PD_Supplier, @PD_UnitCost, @PD_UOM, @PD_SafetyStock, @PD_ReorderQTY,  @PD_VATinc, " &
                        "        @ID_Warehouse, @ID_Max, @ID_Min, @ID_Method, @ID_UOM, @ID_SC, @AD_Sales, @AD_COS, @AD_Inv, @AD_Discount, @AD_Expense, " &
                        "        @WhoCreated, @ItemDept, @Status, @AD_AccrudDep, @AD_DepExpense, @AD_FixedAsset, @isConsignment, @AD_Consignment, @DiscountPercent, " &
                        "        @G1, @G2, @G3, @G4, @G5, @VATType, @PD_MOQ, @PD_EOQ) "
            SQL.FlushParams()
            SQL.AddParam("@ItemCode", txtItemCode.Text)
            SQL.AddParam("@Barcode", txtBarcode.Text)
            SQL.AddParam("@ItemName", txtItemName.Text)
            SQL.AddParam("@ItemType", cbItemType.Text)
            SQL.AddParam("@ItemCategory", cbItemCategory.Text)
            If IsNumeric(txtWeight.Text) Then SQL.AddParam("@ItemWeight", CDec(txtWeight.Text)) Else SQL.AddParam("@ItemWeight", 0)
            SQL.AddParam("@ItemGroup", IIf(cbItemGroup.SelectedIndex = -1, "", cbItemGroup.SelectedItem))
            SQL.AddParam("@G1", cbG1.Text)
            SQL.AddParam("@G2", cbG2.Text)
            SQL.AddParam("@G3", cbG3.Text)
            SQL.AddParam("@G4", cbG4.Text)
            SQL.AddParam("@G5", cbG5.Text)
            SQL.AddParam("@ItemUOMGroup", IIf(cbUoMGroup.SelectedIndex = -1, "", cbUoMGroup.SelectedItem))
            SQL.AddParam("@Status", IIf(cbStatus.SelectedIndex = -1, "", cbStatus.SelectedItem))
            SQL.AddParam("@ItemUOM", txtUOMBase.Text)
            SQL.AddParam("@isInventory", chkInv.Checked)
            SQL.AddParam("@isSales", chkSale.Checked)
            'Barcode Data'
            SQL.AddParam("@isMultipleBarcode", chkMultipleBarcode.Checked)
            'Barcode Data'
            SQL.AddParam("@isPurchase", chkPurch.Checked)
            SQL.AddParam("@isProduce", chkProd.Checked)
            SQL.AddParam("@isFixAsset", chkFixedAsset.Checked)
            SQL.AddParam("@isConsignment", chkConsignment.Checked)
            SQL.AddParam("@VATable", chkVATable.Checked)
            SQL.AddParam("@VATType", IIf(chkVATable.Checked = False, "", IIf(cbVATType.SelectedItem = "", "", cbVATType.SelectedItem)))
            SQL.AddParam("@ItemOwner", cbIDTolling.Text)
            If rbIDOwned.Checked = True Then
                SQL.AddParam("@isOwned", True)
            Else
                SQL.AddParam("@isOwned", False)
            End If

            Dim discountPercent As String = "None"
            If RdFivePercent.Checked = True Then
                discountPercent = "5"
            ElseIf RdTwentyPercent.Checked = True Then
                discountPercent = "20"
            ElseIf RdNone.Checked = True Then
                discountPercent = "None"
            End If
            SQL.AddParam("@DiscountPercent", discountPercent)


            SQL.AddParam("@PD_UpdateLatest", chkPurchUpdate.Checked)
            SQL.AddParam("@PD_Supplier", txtPurchVCECode.Text)
            If IsNumeric(txtPurchPrice.Text) Then
                SQL.AddParam("@PD_UnitCost", CDec(txtPurchPrice.Text))
            Else
                SQL.AddParam("@PD_UnitCost", 0)
            End If
            SQL.AddParam("@PD_UOM", txtPurchUOM.Text)
            If IsNumeric(txtPurchMinimum.Text) Then
                SQL.AddParam("@PD_SafetyStock", CDec(txtPurchMinimum.Text))
            Else
                SQL.AddParam("@PD_SafetyStock", 0)
            End If
            If IsNumeric(txtPurchReorder.Text) Then
                SQL.AddParam("@PD_ReorderQTY", CDec(txtPurchReorder.Text))
            Else
                SQL.AddParam("@PD_ReorderQTY", 0)
            End If
            SQL.AddParam("@PD_VATinc", chkPurchVATInc.Checked)
            If IsNumeric(txtPD_MOQ.Text) Then
                SQL.AddParam("@PD_MOQ", CDec(txtPD_MOQ.Text))
            Else
                SQL.AddParam("@PD_MOQ", 0)
            End If
            If IsNumeric(txtPD_EOQ.Text) Then
                SQL.AddParam("@PD_EOQ", CDec(txtPD_EOQ.Text))
            Else
                SQL.AddParam("@PD_EOQ", 0)
            End If





            If IsNumeric(txtInvMax.Text) Then
                SQL.AddParam("@ID_Max", CDec(txtInvMax.Text))
            Else
                SQL.AddParam("@ID_Max", 0)
            End If

            If IsNumeric(txtInvMin.Text) Then
                SQL.AddParam("@ID_Min", CDec(txtInvMin.Text))
            Else
                SQL.AddParam("@ID_Min", 0)
            End If
            If cbInvMethod.SelectedIndex = -1 Then
                SQL.AddParam("@ID_Method", "")
            Else
                SQL.AddParam("@ID_Method", cbInvMethod.SelectedItem)
            End If
            If cbInvWarehouse.SelectedIndex = -1 Then
                SQL.AddParam("@ID_Warehouse", "")
            Else
                SQL.AddParam("@ID_Warehouse", WHSE)
            End If
            If IsNumeric(txtInvStandard.Text) Then
                SQL.AddParam("@ID_SC", CDec(txtInvStandard.Text))
            Else
                SQL.AddParam("@ID_SC", 0)
            End If
            SQL.AddParam("@ID_UOM", txtInvUOM.Text)
            SQL.AddParam("@AD_Sales", txtSaleAccntCode.Text)
            SQL.AddParam("@AD_COS", txtCostAccntCode.Text)
            SQL.AddParam("@AD_Inv", txtInvAccntCode.Text)
            SQL.AddParam("@AD_Discount", txtDiscAccntCode.Text)
            SQL.AddParam("@AD_Expense", txtExpAccntCode.Text)
            SQL.AddParam("@AD_AccrudDep", txtAccumDepCode.Text)
            SQL.AddParam("@AD_DepExpense", txtDepExpCode.Text)
            SQL.AddParam("@AD_FixedAsset", txtFixedAssetAccountCode.Text)
            SQL.AddParam("@AD_Consignment", txtConAccntCode.Text)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.AddParam("@ItemDept", cbDept.Text)
            SQL.ExecNonQuery(insertSQL)

            'UpdateCharges()
            UpdatePrice()
            'Barcode Data'
            UpdateBarcode()
            'Barcode Data'
            UpdateUOM()
            'Size and Color
            UpdateSizeColor()
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        Finally
            RecordActivity(UserID, moduleID, Me.Name.ToString, "INSERT", "ItemCode", txtItemCode.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub UpdateItem()
        Try
            activityStatus = True
            ' VALIDATE VALUES
            If cbInvWarehouse.SelectedIndex = -1 Then WHSE = "" Else WHSE = GetWHSECode(cbInvWarehouse.SelectedItem)

            ' UPDATE QUERY
            Dim updateSQL As String
            updateSQL = " UPDATE tblItem_Master " &
                        " SET    ItemCode = @ItemCodeNew, Barcode = @Barcode, ItemName = @ItemName, ItemType = @ItemType, ItemGroup = @ItemGroup, ItemWeight = @ItemWeight, " &
                        "        ItemCategory = @ItemCategory, ItemUOMGroup = @ItemUOMGroup, ItemUOM = @ItemUOM, isInventory = @isInventory, ItemOwner = @ItemOwner," &
                        "        isSales = @isSales, isMultipleBarcode = @isMultipleBarcode, isPurchase = @isPurchase, isProduce = @isProduce, isFixAsset = @isFixAsset, isOwned = @isOwned, VATable = @VATable,   " &
                        "        PD_UpdateLatest = @PD_UpdateLatest, PD_Supplier = @PD_Supplier, PD_UnitCost = @PD_UnitCost, " &
                        "        PD_UOM = @PD_UOM, PD_SafetyStock = @PD_SafetyStock, PD_ReorderQTY = @PD_ReorderQTY,  PD_VATinc = @PD_VATinc, " &
                        "        ID_Warehouse = @ID_Warehouse, ID_Min = @ID_Min, ID_Max = @ID_Max, ID_Method = @ID_Method, ID_UOM = @ID_UOM, ID_SC = @ID_SC, " &
                        "        AD_Sales = @AD_Sales, AD_COS = @AD_COS, AD_Inv = @AD_Inv, AD_Discount = @AD_Discount, AD_Expense = @AD_Expense, " &
                        "        DateModified = GETDATE(), WhoModified = @WhoModified, ItemDept = @ItemDept, Status = @Status, AD_AccrudDep = @AD_AccrudDep, AD_DepExpense = @AD_DepExpense, AD_FixedAsset = @AD_FixedAsset, " &
                        "        AD_Consignment = @AD_Consignment, isConsignment = @isConsignment, DiscountPercent= @DiscountPercent ," &
                        "        G1 = @G1, G2 = @G2, G3 = @G3, G4 = @G4, G5 = @G5, VATType = @VATType, PD_MOQ = @PD_MOQ, PD_EOQ = @PD_EOQ " &
                        " WHERE  ItemCode = @ItemCodeOld "
            SQL.FlushParams()
            SQL.AddParam("@ItemCodeOld", itemCode)
            SQL.AddParam("@ItemCodeNew", txtItemCode.Text)
            SQL.AddParam("@ItemUOMGroup", IIf(cbUoMGroup.SelectedIndex = -1, "", cbUoMGroup.SelectedItem))
            SQL.AddParam("@ItemGroup", IIf(cbItemGroup.SelectedIndex = -1, "", cbItemGroup.SelectedItem))
            SQL.AddParam("@G1", cbG1.Text)
            SQL.AddParam("@G2", cbG2.Text)
            SQL.AddParam("@G3", cbG3.Text)
            SQL.AddParam("@G4", cbG4.Text)
            SQL.AddParam("@G5", cbG5.Text)
            SQL.AddParam("@Status", IIf(cbStatus.SelectedIndex = -1, "", cbStatus.SelectedItem))
            SQL.AddParam("@Barcode", txtBarcode.Text)
            SQL.AddParam("@ItemName", txtItemName.Text)
            SQL.AddParam("@ItemType", cbItemType.Text)
            SQL.AddParam("@ItemCategory", cbItemCategory.Text)
            If IsNumeric(txtWeight.Text) Then SQL.AddParam("@ItemWeight", CDec(txtWeight.Text)) Else SQL.AddParam("@ItemWeight", 0)
            SQL.AddParam("@ItemUOM", txtUOMBase.Text)
            SQL.AddParam("@isInventory", chkInv.Checked)
            SQL.AddParam("@isSales", chkSale.Checked)
            'Barcode Data'
            SQL.AddParam("@isMultipleBarcode", chkMultipleBarcode.Checked)
            'Barcode Data'
            SQL.AddParam("@isPurchase", chkPurch.Checked)
            SQL.AddParam("@isProduce", chkProd.Checked)
            SQL.AddParam("@isFixAsset", chkFixedAsset.Checked)
            SQL.AddParam("@PD_UpdateLatest", chkPurchUpdate.Checked)
            SQL.AddParam("@isConsignment", chkConsignment.Checked)
            SQL.AddParam("@PD_Supplier", txtPurchVCECode.Text)
            SQL.AddParam("@ItemOwner", cbIDTolling.Text)

            Dim discountPercent As String = "None"
            If RdFivePercent.Checked = True Then
                discountPercent = "5"
            ElseIf RdTwentyPercent.Checked = True Then
                discountPercent = "20"
            ElseIf RdNone.Checked = True Then
                discountPercent = "None"
            End If
            SQL.AddParam("@DiscountPercent", discountPercent)



            If rbIDOwned.Checked = True Then
                SQL.AddParam("@isOwned", True)
            Else
                SQL.AddParam("@isOwned", False)
            End If

            If IsNumeric(txtPurchPrice.Text) Then
                SQL.AddParam("@PD_UnitCost", CDec(txtPurchPrice.Text))
            Else
                SQL.AddParam("@PD_UnitCost", 0)
            End If
            SQL.AddParam("@PD_UOM", txtPurchUOM.Text)
            If IsNumeric(txtPurchMinimum.Text) Then
                SQL.AddParam("@PD_SafetyStock", CDec(txtPurchMinimum.Text))
            Else
                SQL.AddParam("@PD_SafetyStock", 0)
            End If

            If IsNumeric(txtPurchReorder.Text) Then
                SQL.AddParam("@PD_ReorderQTY", CDec(txtPurchReorder.Text))
            Else
                SQL.AddParam("@PD_ReorderQTY", 0)
            End If
            SQL.AddParam("@VATable", chkVATable.Checked)
            SQL.AddParam("@VATType", IIf(chkVATable.Checked = False, "", IIf(cbVATType.SelectedItem = "", "", cbVATType.SelectedItem)))
            SQL.AddParam("@PD_VATinc", chkPurchVATInc.Checked)
            If IsNumeric(txtPD_MOQ.Text) Then
                SQL.AddParam("@PD_MOQ", CDec(txtPD_MOQ.Text))
            Else
                SQL.AddParam("@PD_MOQ", 0)
            End If
            If IsNumeric(txtPD_EOQ.Text) Then
                SQL.AddParam("@PD_EOQ", CDec(txtPD_EOQ.Text))
            Else
                SQL.AddParam("@PD_EOQ", 0)
            End If




            If IsNumeric(txtInvMax.Text) Then
                SQL.AddParam("@ID_Max", CDec(txtInvMax.Text))
            Else
                SQL.AddParam("@ID_Max", 0)
            End If

            If IsNumeric(txtInvMin.Text) Then
                SQL.AddParam("@ID_Min", CDec(txtInvMin.Text))
            Else
                SQL.AddParam("@ID_Min", 0)
            End If
            If cbInvMethod.SelectedIndex = -1 Then
                SQL.AddParam("@ID_Method", "")
            Else
                SQL.AddParam("@ID_Method", cbInvMethod.SelectedItem)
            End If
            If cbInvWarehouse.SelectedIndex = -1 Then
                SQL.AddParam("@ID_Warehouse", "")
            Else
                SQL.AddParam("@ID_Warehouse", WHSE)
            End If
            If IsNumeric(txtInvStandard.Text) Then
                SQL.AddParam("@ID_SC", CDec(txtInvStandard.Text))
            Else
                SQL.AddParam("@ID_SC", 0)
            End If
            SQL.AddParam("@ID_UOM", txtInvUOM.Text)
            SQL.AddParam("@AD_Sales", txtSaleAccntCode.Text)
            SQL.AddParam("@AD_COS", txtCostAccntCode.Text)
            SQL.AddParam("@AD_Inv", txtInvAccntCode.Text)
            SQL.AddParam("@AD_Discount", txtDiscAccntCode.Text)
            SQL.AddParam("@AD_Expense", txtExpAccntCode.Text)
            SQL.AddParam("@AD_AccrudDep", txtAccumDepCode.Text)
            SQL.AddParam("@AD_DepExpense", txtDepExpCode.Text)
            SQL.AddParam("@AD_FixedAsset", txtFixedAssetAccountCode.Text)
            SQL.AddParam("@AD_Consignment", txtConAccntCode.Text)
            SQL.AddParam("@WhoModified", UserID)
            SQL.AddParam("@ItemDept", cbDept.Text)
            SQL.ExecNonQuery(updateSQL)

            'UpdateCharges()
            UpdatePrice()
            'Barcode Data'
            UpdateBarcode()
            'Barcode Data'
            UpdateUOM()
            'Size and Color
            UpdateSizeColor()
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        Finally
            RecordActivity(UserID, moduleID, Me.Name.ToString, "UPDATE", "ItemCode", txtItemCode.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub UpdateUOM()
        Try
            If cbUoMGroup.Text = "(Manual)" Then
                With frmUOMConversion
                    Dim insertSQL, updateSQL, deleteSQL As String
                    activityStatus = True
                    Dim query As String
                    query = " SELECT * FROM tblUOM_Group WHERE GroupCode = @GroupCode "
                    SQL.FlushParams()
                    SQL.AddParam("@GroupCode", txtItemCode.Text)
                    SQL.ReadQuery(query)
                    If SQL.SQLDR.Read Then
                        updateSQL = " UPDATE tblUOM_Group " &
                                    " SET    UnitCode = @UnitCode, Manual = @Manual, WhoModified = @WhoModified " &
                                    " WHERE  GroupCode = @GroupCode "
                        SQL.FlushParams()
                        SQL.AddParam("@GroupCode", txtItemCode.Text)
                        SQL.AddParam("@UnitCode", txtUOMBase.Text)
                        If cbUoMGroup.Text = "(Manual)" Then
                            SQL.AddParam("@Manual", True)
                        Else
                            SQL.AddParam("@Manual", False)
                        End If
                        SQL.AddParam("@WhoModified", UserID)
                        SQL.ExecNonQuery(updateSQL)
                    Else
                        insertSQL = "  INSERT INTO " &
                                    "  tblUOM_Group(GroupCode, UnitCode, Manual, WhoCreated) " &
                                    "  VALUES(@GroupCode, @UnitCode, @Manual, @WhoCreated) "
                        SQL.FlushParams()
                        SQL.AddParam("@GroupCode", txtItemCode.Text)
                        SQL.AddParam("@UnitCode", txtUOMBase.Text)
                        If cbUoMGroup.Text = "(Manual)" Then
                            SQL.AddParam("@Manual", True)
                        Else
                            SQL.AddParam("@Manual", False)
                        End If
                        SQL.AddParam("@WhoCreated", UserID)
                        SQL.ExecNonQuery(insertSQL)
                    End If

                    deleteSQL = " DELETE FROM tblUOM_GroupDetails WHERE GroupCode = '" & txtItemCode.Text & "' "
                    SQL.ExecNonQuery(deleteSQL)


                    For Each row As DataGridViewRow In .dgvAltUOM.Rows
                        If Not IsNothing(row.Cells(.chFromUoM.Index).Value) AndAlso Not IsNothing(row.Cells(.dgcToUOM.Index).Value) AndAlso IsNumeric(row.Cells(.chQTY.Index).Value) Then
                            insertSQL = " INSERT INTO " &
                                        " tblUOM_GroupDetails(GroupCode, UnitCodeFrom, QTY, UnitCodeTo, WhoCreated) " &
                                        " VALUES(@GroupCode, @UnitCodeFrom, @QTY, @UnitCodeTo, @WhoCreated)"
                            SQL.FlushParams()
                            SQL.AddParam("@GroupCode", txtItemCode.Text)
                            SQL.AddParam("@WhoCreated", UserID)
                            SQL.AddParam("@UnitCodeFrom", row.Cells(.chFromUoM.Index).Value.ToString)
                            If Not row.Cells(.chQTY.Index).Value Is Nothing AndAlso IsNumeric(row.Cells(.chQTY.Index).Value) Then
                                SQL.AddParam("@QTY", row.Cells(.chQTY.Index).Value)
                            Else
                                SQL.AddParam("@QTY", "1")
                            End If
                            SQL.AddParam("@UnitCodeTo", row.Cells(.dgcToUOM.Index).Value.ToString)
                            SQL.ExecNonQuery(insertSQL)
                        End If
                    Next
                End With
            End If

        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        Finally
            RecordActivity(UserID, moduleID, Me.Name.ToString, "INSERT", "GroupCode", frmUOMConversion.cbUoMGroup.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    'Private Sub UpdateUOM()
    '    Try
    '        activityStatus = True
    '        Dim updateSQL As String
    '        updateSQL = "  UPDATE tblUOM_Group " & _
    '                    "  SET    UnitCode = @UnitCode, WhoModified = @WhoModified, Manual = @Manual " & _
    '                    "  WHERE  GroupCode = @GroupCode     "
    '        SQL.FlushParams()
    '        SQL.AddParam("@GroupCode", cbUoMGroup.Text)
    '        SQL.AddParam("@UnitCode", cbBaseUnit.SelectedItem)
    '        If group = "Manual" Then
    '            SQL.AddParam("@Manual", True)
    '        Else
    '            SQL.AddParam("@Manual", False)
    '        End If

    '        SQL.AddParam("@WhoModified", UserID)
    '        SQL.ExecNonQuery(updateSQL)

    '        Dim deleteSQL As String
    '        deleteSQL = " DELETE FROM tblUOM_GroupDetails WHERE GroupCode = @GroupCode "
    '        SQL.FlushParams()
    '        SQL.AddParam("@GroupCode", cbUoMGroup.Text)
    '        SQL.ExecNonQuery(deleteSQL)

    '        For Each row As DataGridViewRow In dgvAltUOM.Rows
    '            If row.Cells(chFromUoM.Index).Value <> Nothing Then
    '                Dim insertSQL As String
    '                insertSQL = " INSERT INTO " & _
    '                            " tblUOM_GroupDetails(GroupCode, UnitCodeFrom, QTY, UnitCodeTo, WhoCreated) " & _
    '                            " VALUES(@GroupCode, @UnitCodeFrom, @QTY, @UnitCodeTo, @WhoCreated)"
    '                SQL.FlushParams()
    '                SQL.AddParam("@GroupCode", cbUoMGroup.Text)
    '                SQL.AddParam("@WhoCreated", UserID)
    '                SQL.AddParam("@UnitCodeFrom", row.Cells(chFromUoM.Index).Value.ToString)
    '                If Not row.Cells(chQTY.Index).Value Is Nothing AndAlso IsNumeric(row.Cells(chQTY.Index).Value) Then
    '                    SQL.AddParam("@QTY", row.Cells(chQTY.Index).Value)
    '                Else
    '                    SQL.AddParam("@QTY", "1")
    '                End If
    '                SQL.AddParam("@UnitCodeTo", row.Cells(dgcToUOM.Index).Value.ToString)
    '                SQL.ExecNonQuery(insertSQL)
    '            End If
    '        Next

    '    Catch ex As Exception
    '        activityStatus = False
    '        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
    '    Finally
    '        RecordActivity(UserID, moduleID, Me.Name.ToString, "UPDATE", "GroupCode", cbUoMGroup.Text, BusinessType, BranchCode, "", activityStatus)
    '        SQL.FlushParams()
    '    End Try
    'End Sub

    Private Sub UpdateSizeColor()
        Dim insertSQL As String
        Dim updateSQL As String
        updateSQL = " DELETE tblItem_SizeColor WHERE ItemCode = @ItemCode"
        SQL.FlushParams()
        SQL.AddParam("@ItemCode", txtItemCode.Text)
        SQL.ExecNonQuery(updateSQL)

        For Each row As DataGridViewRow In dgvSizeColor.Rows
            If Not row.Cells(dcSell_Price.Index).Value Is Nothing Then
                insertSQL = " INSERT INTO " &
                        " tblItem_SizeColor (ItemCode, Size, Color) " &
                        " VALUES (@ItemCode, @Size, @Color) "
                SQL.FlushParams()
                SQL.AddParam("@ItemCode", txtItemCode.Text)
                SQL.AddParam("@Size", row.Cells(chSize.Index).Value)
                SQL.AddParam("@Color", row.Cells(chColor.Index).Value)
                SQL.ExecNonQuery(insertSQL)
            End If
        Next

    End Sub


    Private Sub UpdatePrice()
        Dim insertSQL As String
        Dim updateSQL As String
        updateSQL = " UPDATE tblItem_Price SET Status ='Inactive' WHERE ItemCode = @ItemCode"
        SQL.FlushParams()
        SQL.AddParam("@ItemCode", txtItemCode.Text)
        SQL.ExecNonQuery(updateSQL)

        For Each row As DataGridViewRow In dgvSell.Rows
            If Not row.Cells(dcSell_Price.Index).Value Is Nothing Then
                insertSQL = " INSERT INTO " &
                        " tblItem_Price (Category, Type, ItemCode, VCECode, UOM, UOMQTY, UnitPrice, VATInclusive, VAT) " &
                        " VALUES (@Category, @Type, @ItemCode, @VCECode, @UOM, @UOMQTY, @UnitPrice, @VATInclusive, @VAT ) "
                SQL.FlushParams()
                SQL.AddParam("@Category", "Selling")
                SQL.AddParam("@Type", row.Cells(dcSell_Type.Index).Value)
                SQL.AddParam("@ItemCode", txtItemCode.Text)
                SQL.AddParam("@VCECode", DBNull.Value)
                SQL.AddParam("@UOM", row.Cells(dcSell_UOM.Index).Value)
                SQL.AddParam("@UOMQTY", CDec(row.Cells(dcSell_UOMQTY.Index).Value))
                SQL.AddParam("@UnitPrice", CDec(row.Cells(dcSell_Price.Index).Value))
                SQL.AddParam("@VATInclusive", row.Cells(dcSell_VAT.Index).Value)
                SQL.AddParam("@VAT", row.Cells(dc_VAT.Index).Value)
                SQL.ExecNonQuery(insertSQL)
            End If
        Next

    End Sub

    Private Sub UpdateCharges()
        Dim insertSQL As String
        Dim deleteSQL As String
        deleteSQL = " DELETE tblItem_Charges  WHERE ItemCode = @ItemCode"
        SQL.FlushParams()
        SQL.AddParam("@ItemCode", txtItemCode.Text)
        SQL.ExecNonQuery(deleteSQL)

        Dim line As Integer = 1
        For Each row As DataGridViewRow In dgvItemList.Rows
            If Not row.Cells(chBS_Amount.Index).Value Is Nothing AndAlso Not row.Cells(chBS_Desc.Index).Value Is Nothing Then
                insertSQL = " INSERT INTO " &
                            " tblItem_Charges (ItemCode, Description, VCECode, GrossAmount, LineNum, DescRecordID, WhoCreated, Type) " &
                            " VALUES(@ItemCode,  @Description, @VCECode, @GrossAmount,  @LineNum, @DescRecordID, @WhoCreated, @Type) "
                SQL.FlushParams()
                SQL.AddParam("@ItemCode", txtItemCode.Text)
                SQL.AddParam("@Description", row.Cells(chBS_Desc.Index).Value.ToString)
                SQL.AddParam("@VCECode", row.Cells(chBS_VCECode.Index).Value.ToString)
                SQL.AddParam("@GrossAmount", CDec(row.Cells(chBS_Amount.Index).Value))
                SQL.AddParam("@DescRecordID", row.Cells(chBS_RecordID.Index).Value)
                SQL.AddParam("@LineNum", line)
                SQL.AddParam("@WhoCreated", UserID)
                SQL.AddParam("@Type", row.Cells(chBS_Type.Index).Value)
                SQL.ExecNonQuery(insertSQL)
                line += 1
            End If
        Next

    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If itemCode <> "" Then
            Dim query As String
            query = " SELECT Top 1 ItemCode FROM tblItem_Master  WHERE ItemCode < '" & itemCode & "' ORDER BY ItemCode DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                itemCode = SQL.SQLDR("ItemCode").ToString
                LoadItem(itemCode)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If

    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If itemCode <> "" Then
            Dim query As String
            query = " SELECT Top 1 ItemCode FROM tblItem_Master  WHERE ItemCode > '" & itemCode & "' ORDER BY ItemCode ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                itemCode = SQL.SQLDR("ItemCode").ToString
                LoadItem(itemCode)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub btnUOMGroup_Click(sender As System.Object, e As System.EventArgs) Handles btnUOMGroup.Click
        If cbUoMGroup.Text <> "(Manual)" Then
            Dim f As New frmUOMConversion
            f.itemCode = txtItemCode.Text
            f.group = cbUoMGroup.Text
            f.ShowDialog()
            f.Dispose()
            LoadUomGroup()
        ElseIf cbUoMGroup.Text = "(Manual)" Then
            If txtUOMBase.Text = "" Then
                Msg("Please select Base UOM first!", MsgBoxStyle.Exclamation)
            Else
                frmUOMConversion.Show()
            End If

        End If

    End Sub

    Private Sub cbUoMGroup_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbUoMGroup.SelectedIndexChanged
        If disableEvent = False Then
            If cbUoMGroup.SelectedIndex <> -1 Then
                btnUOM.Visible = True
                Dim query As String
                If cbUoMGroup.SelectedItem = "(Add UoM Group)" Then
                    Dim f As New frmUOMConversion
                    f.Type = "New Group"
                    f.ShowDialog()
                    Dim temp As String = ""
                    If f.cbUoMGroup.SelectedIndex <> -1 Then temp = f.cbUoMGroup.SelectedItem
                    f.Dispose()
                    LoadUomGroup()
                    cbUoMGroup.SelectedItem = temp
                ElseIf cbUoMGroup.SelectedItem <> "(Manual)" Then
                    query = "  SELECT UnitCode FROM tblUOM_Group WHERE GroupCode ='" & cbUoMGroup.SelectedItem & "' "
                    SQL.ReadQuery(query)
                    If SQL.SQLDR.Read Then
                        txtUOMBase.Text = SQL.SQLDR("UnitCode").ToString
                        txtPurchUOM.Text = SQL.SQLDR("UnitCode").ToString
                        txtSellUOM.Text = SQL.SQLDR("UnitCode").ToString
                        txtInvUOM.Text = SQL.SQLDR("UnitCode").ToString
                    End If
                    btnUOM.Visible = False
                End If
            End If

        End If
    End Sub

    Private Sub btnPurchSupplier_Click(sender As System.Object, e As System.EventArgs) Handles btnPurchSupplier.Click
        Dim f As New frmVCE_Search
        f.Type = "Vendor"
        f.ShowDialog()
        txtPurchVCECode.Text = f.VCECode
        txtPurchVCEname.Text = f.VCEName
    End Sub

    Private Sub chkPurch_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkPurch.CheckedChanged
        txtPurchMinimum.Enabled = sender.checked
        txtPurchPrice.Enabled = sender.checked
        txtPurchReorder.Enabled = sender.checked
        txtPurchVCECode.Enabled = sender.checked
        btnPurchSupplier.Enabled = sender.checked
        btnPurchUOM.Enabled = sender.checked
        txtPurchVCEname.Enabled = sender.checked
        chkPurchUpdate.Enabled = sender.checked
        txtPD_EOQ.Enabled = sender.checked
        txtPD_MOQ.Enabled = sender.checked
        If chkVATable.Checked = False Then
            chkPurchVATInc.Enabled = False
        Else
            chkPurchVATInc.Enabled = sender.checked
        End If
        If chkInv.Checked = True Then ' IF STOCK ITEMS, DISABLE EXPENSE ACCOUNT
            txtExpAccntTitle.Enabled = False
        Else
            txtExpAccntTitle.Enabled = sender.checked
        End If

    End Sub

    Private Sub frmItem_Master_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                If tsbDelete.Enabled = True Then tsbDelete.PerformClick()
            ElseIf e.KeyCode = Keys.Left Then
                If tsbPrevious.Enabled = True Then tsbPrevious.PerformClick()
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Right Then
                If tsbNext.Enabled = True Then tsbNext.PerformClick()
                e.SuppressKeyPress = True
            End If
        ElseIf e.Alt = True Then
            If e.KeyCode = Keys.F4 Then
                If tsbClose.Enabled = True Then
                    tsbClose.PerformClick()
                Else
                    e.SuppressKeyPress = True
                End If
            End If
        ElseIf e.KeyCode = Keys.Escape Then
            If tsbClose.Enabled = True Then tsbClose.PerformClick()
        End If
    End Sub

    Private Sub btnPurchUOM_Click(sender As System.Object, e As System.EventArgs) Handles btnPurchUOM.Click
        If cbUoMGroup.SelectedIndex <> -1 Then
            frmItem_UOMlist.ShowDialog(cbUoMGroup.SelectedItem, itemCode)
        Else
            frmItem_UOMlist.ShowDialog()
        End If
        If Not IsNothing(frmItem_UOMlist.UoM) OrElse frmItem_UOMlist.UoM <> "" Then
            txtPurchUOM.Text = frmItem_UOMlist.UoM
        End If

        frmItem_UOMlist.Dispose()
    End Sub

    Private Sub txtSellQTY_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSellQTY.KeyDown
        If e.KeyValue <> 8 And Not (e.KeyValue >= 48 And e.KeyValue <= 57) Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub btnSellUOM_Click(sender As System.Object, e As System.EventArgs) Handles btnSellUOM.Click
        If cbUoMGroup.SelectedIndex <> -1 Then
            frmItem_UOMlist.ShowDialog(cbUoMGroup.SelectedItem, itemCode)
        Else
            frmItem_UOMlist.ShowDialog()
        End If
        If Not IsNothing(frmItem_UOMlist.UoM) OrElse frmItem_UOMlist.UoM <> "" Then
            txtSellUOM.Text = frmItem_UOMlist.UoM
        End If

        frmItem_UOMlist.Dispose()
    End Sub

    Private Sub chkSale_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSale.CheckedChanged
        cbSellType.Enabled = sender.checked
        txtSellPrice.Enabled = sender.checked
        txtSellQTY.Enabled = sender.checked
        btnSellAdd.Enabled = sender.checked
        btnSellRemove.Enabled = sender.checked
        btnSellUOM.Enabled = sender.checked
        txtSaleAccntTitle.Enabled = sender.checked
        txtCostAccntTitle.Enabled = sender.checked
        txtDiscAccntTitle.Enabled = sender.checked
        If chkVATable.Checked = False Then
            chkSellVAT.Enabled = False
            chkVAT.Enabled = False
        Else
            chkSellVAT.Enabled = sender.checked
            chkVAT.Enabled = sender.checked
        End If
    End Sub

    Private Sub chkVATable_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkVATable.CheckedChanged
        If chkVATable.Checked = False Then
            chkSellVAT.Enabled = False
            chkVAT.Enabled = False
            chkPurchVATInc.Enabled = False
            chkSellVAT.Checked = False
            chkVAT.Enabled = False
            chkPurchVATInc.Checked = False
        Else
            If chkPurch.Checked = True Then
                chkPurchVATInc.Enabled = True
                chkPurchVATInc.Checked = True
            End If
            If chkSale.Checked = True Then
                chkSellVAT.Enabled = True
                chkVAT.Enabled = True
                chkSellVAT.Checked = True
                chkVAT.Checked = True
            End If
        End If
    End Sub

    Private Sub txtPurchPrice_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPurchPrice.KeyPress, txtPurchMinimum.KeyPress,
        txtPurchReorder.KeyPress
        If ControlChars.Back <> e.KeyChar Then
            If Not (Char.IsDigit(CChar(CStr(e.KeyChar))) Or e.KeyChar = ".") Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub btnSellAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnSellAdd.Click
        If cbSellType.Text = "" Then
            Msg("Enter Selling Price Type", MsgBoxStyle.Exclamation)
        ElseIf txtSellUOM.Text = "" Then
            Msg("Please select UoM for this Price Type", MsgBoxStyle.Exclamation)
        ElseIf Not IsNumeric(txtSellPrice.Text) Then
            Msg("Invalid Price!", MsgBoxStyle.Exclamation)
        ElseIf IsNumeric(txtSellPrice.Text) AndAlso CDec(txtSellPrice.Text) <= 0 Then
            Msg("Price should be greater than zero!", MsgBoxStyle.Exclamation)
        Else
            dgvSell.Rows.Add({cbSellType.Text, txtSellPrice.Text, txtSellUOM.Text, txtSellQTY.Text, chkVAT.Checked, chkSellVAT.Checked})
            cbSellType.Text = ""
            txtSellPrice.Text = ""
            txtSellUOM.Text = ""
            txtSellQTY.Text = 1
            chkSellVAT.Checked = False
            chkVAT.Checked = False
        End If
    End Sub

    Private Sub btnSellRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnSellRemove.Click
        If dgvSell.SelectedRows.Count = 1 Then
            dgvSell.Rows.RemoveAt(dgvSell.SelectedRows(0).Index)
        End If
    End Sub

    Private Sub chkProd_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkProd.CheckedChanged
        cbProd.Enabled = sender.checked
        cbMDprodFloor.Enabled = sender.checked
    End Sub

    Private Sub txtSaleAccntTitle_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSaleAccntTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtSaleAccntTitle.Text)
                txtSaleAccntCode.Text = f.accountcode
                txtSaleAccntTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        End Try
    End Sub

    Private Sub txtCostAccntTitle_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtCostAccntTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtCostAccntTitle.Text)
                txtCostAccntCode.Text = f.accountcode
                txtCostAccntTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        End Try
    End Sub

    Private Sub txtInvAccntTitle_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtInvAccntTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtInvAccntTitle.Text)
                txtInvAccntCode.Text = f.accountcode
                txtInvAccntTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        End Try
    End Sub

    Private Sub txtProdName_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)

    End Sub

    Private Sub btnInvUOM_Click(sender As System.Object, e As System.EventArgs) Handles btnInvUOM.Click
        If cbUoMGroup.SelectedIndex <> -1 Then
            frmItem_UOMlist.ShowDialog(cbUoMGroup.SelectedItem, txtItemCode.Text)
        Else
            frmItem_UOMlist.ShowDialog()
        End If
        If frmItem_UOMlist.UoM <> "" Then
            txtInvUOM.Text = frmItem_UOMlist.UoM
        End If
        frmItem_UOMlist.Dispose()
    End Sub

    Private Sub chkInv_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkInv.CheckedChanged
        txtInvMax.Enabled = sender.checked
        txtInvMin.Enabled = sender.checked
        btnInvUOM.Enabled = sender.checked
        cbInvMethod.Enabled = sender.checked
        cbInvWarehouse.Enabled = sender.checked
        txtInvStandard.Enabled = sender.checked
        txtInvAccntTitle.Enabled = sender.checked
        If chkPurch.Checked = False Then
            txtExpAccntTitle.Enabled = False
        Else
            txtExpAccntTitle.Enabled = Not sender.checked
        End If
    End Sub

    Private Sub TextBox5_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtDiscAccntTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtDiscAccntTitle.Text)
                txtDiscAccntCode.Text = f.accountcode
                txtDiscAccntTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        End Try
    End Sub

    Private Sub LoadWHSE_INVY()
        Dim InStock, Ordered, Committed As Decimal
        InStock = 0
        Ordered = 0
        Committed = 0
        Dim query As String
        query = " SELECT  pvt.WHSE, Description, " &
                " 		ISNULL(Instock,0) AS Instock, ISNULL(Ordered,0) AS Ordered, ISNULL(Committed,0) AS Committed, " &
                " 		ISNULL(Instock,0)  + ISNULL(Ordered,0) - ISNULL(Committed,0) AS Available " &
                " FROM " &
                " ( " &
                " 	SELECT   WHSE, SUM(CASE WHEN MovementType ='IN' THEN QTY ELSE QTY*-1 END) AS QTY , 'InStock' AS Type " &
                " 	FROM	 tblInventory " &
                " 	WHERE	 ItemCode ='" & txtItemCode.Text & "'" &
                " 	GROUP BY ItemCode, WHSE " &
                " UNION ALL  " &
                " 	SELECT	 WHSE, Unserved, 'Ordered' AS Type " &
                " 	FROM	 viewPO_Unserved " &
                " 	WHERE    ItemCode ='" & txtItemCode.Text & "' " &
                " UNION ALL " &
                " 	SELECT	 WHSE, Unserved, 'Committed' AS Type " &
                " 	FROM	 viewSO_Unserved " &
                " 	WHERE    ItemCode ='" & txtItemCode.Text & "' " &
                " ) " &
                " AS INVTY " &
                " PIVOT  " &
                " ( " &
                " 	SUM(QTY) FOR [Type] " &
                " 	IN ([Instock],[Ordered],[Committed]) " &
                " ) AS pvt " &
                " LEFT JOIN tblWarehouse " &
                " ON pvt.WHSE = tblWarehouse.Code AND Status ='Active'"
        SQL.ReadQuery(query)
        dgvInv.Rows.Clear()
        While SQL.SQLDR.Read
            dgvInv.Rows.Add({SQL.SQLDR("WHSE").ToString, SQL.SQLDR("Description").ToString, CDec(SQL.SQLDR("InStock")).ToString("N2"),
                             CDec(SQL.SQLDR("Ordered")).ToString("N2"), CDec(SQL.SQLDR("Committed")).ToString("N2"), CDec(SQL.SQLDR("Available")).ToString("N2")})
            InStock += SQL.SQLDR("InStock")
            Committed += SQL.SQLDR("Committed")
            Ordered += SQL.SQLDR("Ordered")
        End While
        txtInvStock.Text = InStock.ToString("N2")
        txtInvOrder.Text = Ordered.ToString("N2")
        txtInvCommit.Text = Committed.ToString("N2")
        txtInvAvailable.Text = (InStock + Ordered - Committed).ToString("N2")
    End Sub

    Private Sub btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnRefresh.Click
        LoadWHSE_INVY()
    End Sub

    Private Sub gbSales_Enter(sender As System.Object, e As System.EventArgs) Handles gbSales.Enter

    End Sub

    Private Sub rbIDTolling_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbIDTolling.CheckedChanged
        If rbIDTolling.Checked = True Then
            cbIDTolling.Visible = True
        End If
    End Sub

    Private Sub btnBOMgroup_Click(sender As System.Object, e As System.EventArgs) Handles btnBOMgroup.Click
        Dim f As New frmBOM_Group
        f.ShowDialog()
        f.Dispose()
        LoadBOMGroup()
    End Sub

    Private Sub txtExpAccntTitle_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtExpAccntTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtExpAccntTitle.Text)
                txtExpAccntCode.Text = f.accountcode
                txtExpAccntTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        End Try
    End Sub

    Private Sub btnSalesAccnt_Click(sender As System.Object, e As System.EventArgs) Handles btnSalesAccnt.Click
        If Not AllowAccess("ITM_EDIT") Then
            msgRestricted()
        Else
            frmItem_Master_AddAccount.Show()
        End If
    End Sub

    Private Sub chkSellVAT_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSellVAT.CheckedChanged
        If chkSellVAT.Checked = True Then
            chkVAT.Checked = True
        Else
            chkVAT.Checked = False
        End If
    End Sub

    Private Sub txtCostAccntTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCostAccntTitle.TextChanged

    End Sub

    Private Sub chkVAT_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkVAT.CheckedChanged
        If chkVAT.Checked = False Then
            chkSellVAT.Checked = False
        End If
    End Sub

    Private Sub dgvItemList_CellBeginEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvItemList.CellBeginEdit
        eColIndex = e.ColumnIndex
    End Sub

    Private Sub dgvItemList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellContentClick

    End Sub

    Public Sub LoadCharges(ByVal ID As String)
        Dim query, Description, RecordID As String
        Dim Amount As Decimal
        query = " SELECT  Description, Amount, RecordID FROM  tblBS_Charges " &
                " WHERE   RecordID ='" & ID & "' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            RecordID = SQL.SQLDR("RecordID")
            Description = SQL.SQLDR("Description")
            Amount = SQL.SQLDR("Amount")
            dgvItemList.Rows.Add(New String() {RecordID,
                                         Description,
                                               "",
                                               "",
                                               "",
                                                Format(0, "#,###,###,###.00").ToString})
            LoadType(ID, dgvItemList.Rows.Count - 2)
        End While
    End Sub

    Private Sub LoadType(ID As String, ByVal SelectedIndex As Integer)
        Try
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemList.Item(chBS_Type.Index, SelectedIndex)
            dgvCB.Items.Clear()
            ' ADD ALL UOM
            Dim query As String
            query = " SELECT Type " &
                 " FROM tblBS_Charges_Amount " &
                 " WHERE RecordID ='" & ID & "'"
            SQL.ReadQuery(query, 2)
            dgvCB.Items.Clear()
            While SQL.SQLDR2.Read
                dgvCB.Items.Add(SQL.SQLDR2("Type").ToString)
            End While
            dgvCB.Items.Add("")
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        End Try
    End Sub

    Dim strDefaultGridType As String = ""
    Dim strDefaultGridAmount As String = ""
    Public Sub LoadTypeAmount(ByVal RecordID As String, ByVal Type As String, ByVal RowIndex As Integer, ByVal TypeInde As Integer, ByVal AmountIndex As Integer)

        Dim selectSQL As String
        selectSQL = " SELECT Amount, Type FROM tblBS_Charges_Amount WHERE RecordID = '" & RecordID & "' AND Type = '" & Type & "' "
        SQL.ReadQuery(selectSQL, 2)

        strDefaultGridType = ""
        strDefaultGridAmount = ""

        While SQL.SQLDR2.Read
            strDefaultGridType = SQL.SQLDR2("Type").ToString
            strDefaultGridAmount = SQL.SQLDR2("Amount").ToString
        End While
        dgvItemList.Rows(RowIndex).Cells(TypeInde).Value = strDefaultGridType
        dgvItemList.Rows(RowIndex).Cells(AmountIndex).Value = strDefaultGridAmount

    End Sub

    Dim eColIndex As Integer = 0

    Private Sub dgvItemList_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellEndEdit
        Try
            Dim desc As String
            Dim rowIndex As Integer = dgvItemList.CurrentCell.RowIndex
            Dim colindex As Integer = dgvItemList.CurrentCell.ColumnIndex
            Select Case colindex

                Case chBS_Desc.Index
                    If dgvItemList.Item(chBS_Desc.Index, e.RowIndex).Value <> "" Then
                        desc = dgvItemList.Item(chBS_Desc.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("BS_Type", desc)
                        If f.TransID <> "" Then
                            desc = f.TransID
                            LoadCharges(desc)
                        End If
                        dgvItemList.Rows.RemoveAt(e.RowIndex)
                        f.Dispose()
                    End If
                Case chBS_VCEName.Index
                    Dim f As New frmVCE_Search
                    f.txtFilter.Text = dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                    f.ShowDialog()
                    dgvItemList.Item(chBS_VCECode.Index, e.RowIndex).Value = f.VCECode
                    dgvItemList.Item(chBS_VCEName.Index, e.RowIndex).Value = f.VCEName
                    f.Dispose()
                Case chBS_Type.Index
                    Dim dgvType, dgvRecordID As String
                    dgvType = dgvItemList.Rows(e.RowIndex).Cells(chBS_Type.Index).Value
                    dgvRecordID = dgvItemList.Rows(e.RowIndex).Cells(chBS_RecordID.Index).Value
                    LoadTypeAmount(dgvRecordID, dgvType, e.RowIndex, chBS_Type.Index, chBS_Amount.Index)
            End Select
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        End Try
    End Sub

    Private Sub dgvItemList_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dgvItemList.CurrentCellDirtyStateChanged

        If eColIndex = chBS_Type.Index And TypeOf (dgvItemList.CurrentRow.Cells(chBS_Type.Index)) Is DataGridViewComboBoxCell Then
            dgvItemList.CommitEdit(DataGridViewDataErrorContexts.Commit)
            dgvItemList.EndEdit()
        End If
    End Sub

    Private Sub chkFixedAsset_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkFixedAsset.CheckedChanged
        txtDepExpTitle.Enabled = sender.checked
        txtAccumDepTitle.Enabled = sender.checked
        txtFixedAssetAccountTitle.Enabled = sender.checked
    End Sub

    Private Sub txtExpAccntTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtExpAccntTitle.TextChanged

    End Sub

    Private Sub txtAccumDepTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtAccumDepTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtAccumDepTitle.Text)
                txtAccumDepCode.Text = f.accountcode
                txtAccumDepTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        End Try
    End Sub

    Private Sub txtAccumDepTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtAccumDepTitle.TextChanged

    End Sub

    Private Sub txtDepExpTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtDepExpTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtDepExpTitle.Text)
                txtDepExpCode.Text = f.accountcode
                txtDepExpTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        End Try
    End Sub

    Private Sub txtDepExpTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtDepExpTitle.TextChanged

    End Sub

    Private Sub txtFixedAssetAccountTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtFixedAssetAccountTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtFixedAssetAccountTitle.Text)
                txtFixedAssetAccountCode.Text = f.accountcode
                txtFixedAssetAccountTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        End Try
    End Sub


    'Barcode Data'
    Private Sub btnBarcodeRemove_Click(sender As Object, e As EventArgs) Handles btnBarcodeRemove.Click
        If dgvBarcode.SelectedRows.Count = 1 Then
            dgvBarcode.Rows.RemoveAt(dgvBarcode.SelectedRows(0).Index)
        End If
    End Sub

    Private Sub btnBarcodeAdd_Click(sender As Object, e As EventArgs) Handles btnBarcodeAdd.Click
        If txtBarcode_Barcode.Text = "" Then
            Msg("Enter Barcode", MsgBoxStyle.Exclamation)
        ElseIf txtBarcode_UOM.Text = "" Then
            Msg("Please select UoM for this Barcode", MsgBoxStyle.Exclamation)
        ElseIf BarCodeVAlidation() Then
            Msg("Barcode is already in use, please input another barcode.", MsgBoxStyle.Exclamation)
        Else
            dgvBarcode.Rows.Add({txtBarcode_Barcode.Text, txtBarcode_UOM.Text})
            txtBarcode_Barcode.Text = ""
            txtBarcode_UOM.Text = ""
        End If
    End Sub

    Private Function BarCodeVAlidation() As Boolean
        Dim query As String
        query = " SELECT Barcode FROM tblItem_Barcode WHERE Barcode = @Barcode AND Status <> 'Inactive' "
        SQL.FlushParams()
        SQL.AddParam("@Barcode", txtBarcode_Barcode.Text)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btnBarcodeUOM_Click(sender As Object, e As EventArgs) Handles btnBarcodeUOM.Click
        If cbUoMGroup.SelectedIndex <> -1 Then
            frmItem_UOMlist.ShowDialog(cbUoMGroup.SelectedItem, itemCode)
        Else
            frmItem_UOMlist.ShowDialog()
        End If
        If Not IsNothing(frmItem_UOMlist.UoM) OrElse frmItem_UOMlist.UoM <> "" Then
            txtBarcode_UOM.Text = frmItem_UOMlist.UoM
        End If

        frmItem_UOMlist.Dispose()
    End Sub


    Private Sub UpdateBarcode()
        Dim insertSQL As String
        Dim updateSQL As String
        updateSQL = " UPDATE tblItem_Barcode SET Status ='Inactive' WHERE ItemCode = @ItemCode"
        SQL.FlushParams()
        SQL.AddParam("@ItemCode", txtItemCode.Text)
        SQL.ExecNonQuery(updateSQL)

        For Each row As DataGridViewRow In dgvBarcode.Rows
            If Not row.Cells(dcBarcode_Barcode.Index).Value Is Nothing Then
                insertSQL = " INSERT INTO " &
                        " tblItem_Barcode (ItemCode, Barcode, UOM) " &
                        " VALUES ( @ItemCode, @Barcode, @UOM) "
                SQL.FlushParams()
                SQL.AddParam("@ItemCode", txtItemCode.Text)
                SQL.AddParam("@Barcode", row.Cells(dcBarcode_Barcode.Index).Value)
                SQL.AddParam("@UOM", row.Cells(dcBarCode_UOM.Index).Value)
                SQL.ExecNonQuery(insertSQL)
            End If
        Next

    End Sub

    Private Sub LoadBarcode()
        Dim query As String
        query = " SELECT ItemCode, Barcode, UOM" &
                " FROM   tblItem_Barcode " &
                " WHERE  ItemCode ='" & txtItemCode.Text & "'" &
                " AND    Status ='Active' "
        SQL.GetQuery(query)
        dgvBarcode.Rows.Clear()
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvBarcode.Rows.Add({row(1).ToString, row(2).ToString})
            Next
        End If
    End Sub

    Private Sub chkMultipleBarcode_CheckedChanged(sender As Object, e As EventArgs) Handles chkMultipleBarcode.CheckedChanged
        txtBarcode_Barcode.Enabled = sender.checked
        txtBarcode_UOM.Enabled = sender.checked
        btnBarcodeAdd.Enabled = sender.checked
        btnBarcodeRemove.Enabled = sender.checked
        btnBarcodeUOM.Enabled = sender.checked
    End Sub
    'Barcode Data'

    Private Sub chkConsignment_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkConsignment.CheckedChanged
        txtConAccntTitle.Enabled = sender.checked
    End Sub

    Private Sub txtConAccntTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtConAccntTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtConAccntTitle.Text)
                txtConAccntCode.Text = f.accountcode
                txtConAccntTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        End Try
    End Sub

    Private Sub txtConAccntTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtConAccntTitle.TextChanged

    End Sub

    Private Sub LoadVAType()
        cbVATType.Items.Clear()
        cbVATType.Items.Add("")
        cbVATType.Items.Add("Exempt")
        cbVATType.Items.Add("Zero-rated")
        cbVATType.Items.Add("Services")
        cbVATType.Items.Add("Capital Goods")
        cbVATType.Items.Add("Other Than Capital Goods")
    End Sub

    Private Sub loadData()
        LoadRecords() ' LOAD DATAGRIDVIEW DATA
        loadGroupField("G1", lblG1, cbG1)  ' LOAD GROUP 1 LABEL AND COMBOBOX
        loadGroupField("G2", lblG2, cbG2)  ' LOAD GROUP 2 LABEL AND COMBOBOX
        loadGroupField("G3", lblG3, cbG3)  ' LOAD GROUP 3 LABEL AND COMBOBOX
        loadGroupField("G4", lblG4, cbG4)  ' LOAD GROUP 4 LABEL AND COMBOBOX
        loadGroupField("G5", lblG5, cbG5)  ' LOAD GROUP 5 LABEL AND COMBOBOX
    End Sub

    Private Sub loadGroupField(ByVal GroupID As String, lblName As Label, cbName As ComboBox)
        lblName.Text = LoadGroupName("Item Group", GroupID)
        If lblName.Text = "" Then
            lblName.Visible = False
            cbName.Visible = False
        Else
            lblName.Visible = True
            cbName.Visible = True
            LoadGroupData(GroupID, cbName)
        End If
    End Sub

    Private Sub LoadGroupData(ByVal GroupID As String, cbName As ComboBox)
        Dim query As String
        query = " SELECT DISTINCT ISNULL(" & GroupID & ",'') AS GroupData FROM tblItem_Master WHERE  ISNULL(" & GroupID & ",'') <> ''"
        SQL.ReadQuery(query)
        cbName.Items.Clear()
        While SQL.SQLDR.Read
            cbName.Items.Add(SQL.SQLDR("GroupData").ToString)
        End While
    End Sub

    Private Sub LoadRecords() ' LOAD Item Group
        Dim dt As DataTable
        dt = LoadGroup("Item Group")
        If Not dt Is Nothing Then
            Dim groupName As String = ""
            For Each row As DataRow In dt.Rows
                groupName = groupName & ", " & row(0).ToString & " AS '" & row(1).ToString & "' "
            Next

        End If
    End Sub


    Private Function LoadGroup(ByVal Type As String) As DataTable
        ' LOAD GROUP NAME
        Dim dt As New DataTable
        Dim query As String
        query = " SELECT GroupID, Description FROM tblGroup WHERE Type ='" & Type & "' AND Status ='Active' "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            Return SQL.SQLDS.Tables(0)
        Else
            Return Nothing
        End If
    End Function

    Private Function LoadGroupName(ByVal Type As String, GroupCode As String) As String
        Dim query As String
        query = " SELECT GroupID, Description FROM tblGroup WHERE Type ='" & Type & "' AND GroupID = '" & GroupCode & "' AND Status ='Active' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("Description")
        Else
            Return ""
        End If
    End Function

    Private Sub btnSizeColor_Add_Click(sender As System.Object, e As System.EventArgs) Handles btnSizeColor_Add.Click
        If txtSize.Text = "" Then
            Msg("Please Enter Size", MsgBoxStyle.Exclamation)
        ElseIf txtColor.Text = "" Then
            Msg("Please Enter Color", MsgBoxStyle.Exclamation)
        Else
            dgvSizeColor.Rows.Add({txtSize.Text, txtColor.Text})
            txtSize.Text = ""
            txtColor.Text = ""
        End If
    End Sub

    Private Sub btnSizeColor_Remove_Click(sender As System.Object, e As System.EventArgs) Handles btnSizeColor_Remove.Click
        If dgvSizeColor.SelectedRows.Count = 1 Then
            dgvSizeColor.Rows.RemoveAt(dgvSizeColor.SelectedRows(0).Index)
        End If
    End Sub

    Private Sub cbVATType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbVATType.SelectedIndexChanged

    End Sub

    Private Sub txtPurchVCEname_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPurchVCEname.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmVCE_Search
                f.cbFilter.SelectedItem = "VCEName"
                f.txtFilter.Text = txtPurchVCEname.Text
                f.ShowDialog()
                txtPurchVCECode.Text = f.VCECode
                txtPurchVCEname.Text = f.VCEName
                f.Dispose()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class