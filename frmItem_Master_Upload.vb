Imports System.ComponentModel
Imports System.IO
Imports Microsoft.Office.Interop

Public Class frmItem_Master_Upload

    Dim SQL As New SQLControl
    Dim path As String
    Private Sub frmItem_Master_Upload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDataColumn()
    End Sub

    Private Sub LoadDataColumn()
        'Add Column Item Master
        dgvItemMaster.Columns.Clear()
        dgvItemMaster.Columns.Add("chIMItemCode", "ItemCode")
        dgvItemMaster.Columns.Add("chIMBarcode", "Barcode")
        dgvItemMaster.Columns.Add("chIMItemName", "ItemName")
        dgvItemMaster.Columns.Add("chIMItemDescription", "ItemDescription")
        Dim count As Integer = 1
        Dim query As String = " SELECT Description, GroupID FROM tblGroup where Type = 'Item Group' AND Status = 'Active' ORDER BY GroupID "
        SQL.FlushParams()
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvItemMaster.Columns.Add("chIM" & SQL.SQLDR("GroupID").ToString, SQL.SQLDR("Description").ToString)
            count += 1
        End While
        For i As Integer = count To 5
            dgvItemMaster.Columns.Add("chIMG" & i, "G" & i)
        Next
        dgvItemMaster.Columns.Add("chIMItemType", "ItemType")
        dgvItemMaster.Columns.Add("chIMItemCategory", "ItemCategory")
        dgvItemMaster.Columns.Add("chIMItemUOMGroup", "ItemUOMGroup")
        dgvItemMaster.Columns.Add("chIMItemUOM", "ItemUOM")
        dgvItemMaster.Columns.Add("chIMItemWeight", "ItemWeight")
        dgvItemMaster.Columns.Add("chIMVATable", "VATable")
        dgvItemMaster.Columns.Add("chIMLot_Size", "Lot_Size")
        dgvItemMaster.Columns.Add("chIMisInventory", "isInventory")
        dgvItemMaster.Columns.Add("chIMisSales", "isSales")
        dgvItemMaster.Columns.Add("chIMisPurchase", "isPurchase")
        dgvItemMaster.Columns.Add("chIMisProduce", "isProduce")
        dgvItemMaster.Columns.Add("chIMisOwned", "isOwned")
        dgvItemMaster.Columns.Add("chIMisFixAsset", "isFixAsset")
        dgvItemMaster.Columns.Add("chIMisConsignment", "isConsignment")
        dgvItemMaster.Columns.Add("chIMisMultipleBarcode", "isMultipleBarcode")
        dgvItemMaster.Columns.Add("chIMPurchaseData_UpdateLatest", "PurchaseData_UpdateLatest")
        dgvItemMaster.Columns.Add("chIMPurchaseData_Supplier", "PurchaseData_Supplier")
        dgvItemMaster.Columns.Add("chIMPurchaseData_UnitCost", "PurchaseData_UnitCost")
        dgvItemMaster.Columns.Add("chIMPurchaseData_UOM", "PurchaseData_UOM")
        dgvItemMaster.Columns.Add("chIMPurchaseData_SafetyStock", "PurchaseData_SafetyStock")
        dgvItemMaster.Columns.Add("chIMPurchaseData_ReorderQTY", "PurchaseData_ReorderQTY")
        dgvItemMaster.Columns.Add("chIMPurchaseData_VATinc", "PurchaseData_VATinc")
        dgvItemMaster.Columns.Add("chIMInventoryData_Max", "InventoryData_Max")
        dgvItemMaster.Columns.Add("chIMInventoryData_Min", "InventoryData_Min")
        dgvItemMaster.Columns.Add("chIMInventoryData_Method", "InventoryData_Method")
        dgvItemMaster.Columns.Add("chIMInventoryData_UOM", "InventoryData_UOM")
        dgvItemMaster.Columns.Add("chIMInventoryData_Warehouse", "InventoryData_Warehouse")
        dgvItemMaster.Columns.Add("chIMInventoryData_StandardCost", "InventoryData_StandardCost")
        dgvItemMaster.Columns.Add("chIMAccountingData_Sales", "AccountingData_Sales")
        dgvItemMaster.Columns.Add("chIMAccountingData_CostofSales", "AccountingData_CostofSales")
        dgvItemMaster.Columns.Add("chIMAcountingData_Inventory", "AcountingData_Inventory")
        dgvItemMaster.Columns.Add("chIMAccountingData_Discount", "AccountingData_Discount")
        dgvItemMaster.Columns.Add("chIMAccountingData_Expense", "AccountingData_Expense")
        dgvItemMaster.Columns.Add("chIMAccountingData_Consignment", "AccountingData_Consignment")
        dgvItemMaster.Columns.Add("chIMDiscount", "Discount")
        dgvItemMaster.Columns.Add("chIMStatus", "Status")

        'Add Column Item Barcode
        dgvItemBarcode.Columns.Clear()
        dgvItemBarcode.Columns.Add("chIBItemCode", "ItemCode")
        dgvItemBarcode.Columns.Add("chIBBarcode", "Barcode")
        dgvItemBarcode.Columns.Add("chIBUOM", "UOM")

        'Add Column Item Price
        dgvItemPrice.Columns.Clear()
        dgvItemPrice.Columns.Add("chIPType", "Type")
        dgvItemPrice.Columns.Add("chIPItemCode", "ItemCode")
        dgvItemPrice.Columns.Add("chIPUOM", "UOM")
        dgvItemPrice.Columns.Add("chIPUOMQTY", "UOMQTY")
        dgvItemPrice.Columns.Add("chIPUnitPrice", "UnitPrice")
        dgvItemPrice.Columns.Add("chIPVATInclusive", "VATInclusive")
        dgvItemPrice.Columns.Add("chIPVATable", "VATable")


        'Add Column Item Conversion
        dgvItemConversion.Columns.Clear()
        dgvItemConversion.Columns.Add("chUOMFrom", "UOMFrom")
        dgvItemConversion.Columns.Add("chConvertQTY", "Convert QTY")
        dgvItemConversion.Columns.Add("chUOMTo", "UOMTo")

    End Sub

    Private Sub tsbOpen_Click(sender As Object, e As EventArgs) Handles tsbOpen.Click
        Dim f As New OpenFileDialog
        f.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
        If f.ShowDialog = DialogResult.OK Then
            If MsgBox("Are you sure you want to upload this file?", MsgBoxStyle.YesNo, "Upload") = MsgBoxResult.Yes Then
                path = f.FileName
                LoadDataColumn()
                'dgvEntry.ReadOnly = True
                tsbSave.Enabled = True
                bgwUpload.RunWorkerAsync()
                tsbOpen.Text = "Stop"
            End If
        End If
    End Sub

    Private Sub bgwUpload_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwUpload.DoWork

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
        ' ROWS
        Dim rowCount As Integer = 0
        Dim rowSumCount As Integer = 0
        Dim strErrorMessage As String = ""
        Dim query As String = ""

        ' ROW COUNT
        rowCount = range.Rows.Count

        SetPGBmax(rowCount)  ' SET PROGRESS BAR MAX VALUE

        For i As Integer = 2 To rowCount
            If bgwUpload.CancellationPending Then
                e.Cancel = True
                Exit For
            End If

            Dim strItemCode As String = IIf(IsNothing(CType(range.Cells(i, 1), Excel.Range).Value), "", CType(range.Cells(i, 1), Excel.Range).Value)
            Dim strBarcode As String = IIf(IsNothing(CType(range.Cells(i, 2), Excel.Range).Value), "", CType(range.Cells(i, 2), Excel.Range).Value)
            Dim strItemName As String = IIf(IsNothing(CType(range.Cells(i, 3), Excel.Range).Value), "", CType(range.Cells(i, 3), Excel.Range).Value)
            Dim strItemDescription As String = IIf(IsNothing(CType(range.Cells(i, 4), Excel.Range).Value), "", CType(range.Cells(i, 4), Excel.Range).Value)
            Dim strG1 As String = IIf(IsNothing(CType(range.Cells(i, 5), Excel.Range).Value), "", CType(range.Cells(i, 5), Excel.Range).Value)
            Dim strG2 As String = IIf(IsNothing(CType(range.Cells(i, 6), Excel.Range).Value), "", CType(range.Cells(i, 6), Excel.Range).Value)
            Dim strG3 As String = IIf(IsNothing(CType(range.Cells(i, 7), Excel.Range).Value), "", CType(range.Cells(i, 7), Excel.Range).Value)
            Dim strG4 As String = IIf(IsNothing(CType(range.Cells(i, 8), Excel.Range).Value), "", CType(range.Cells(i, 8), Excel.Range).Value)
            Dim strG5 As String = IIf(IsNothing(CType(range.Cells(i, 9), Excel.Range).Value), "", CType(range.Cells(i, 9), Excel.Range).Value)
            Dim strItemType As String = IIf(IsNothing(CType(range.Cells(i, 10), Excel.Range).Value), "", CType(range.Cells(i, 10), Excel.Range).Value)
            Dim strItemCategory As String = IIf(IsNothing(CType(range.Cells(i, 11), Excel.Range).Value), "", CType(range.Cells(i, 11), Excel.Range).Value)
            Dim strItemUOMGroup As String = IIf(IsNothing(CType(range.Cells(i, 12), Excel.Range).Value), "", CType(range.Cells(i, 12), Excel.Range).Value)
            Dim strItemUOM As String = IIf(IsNothing(CType(range.Cells(i, 13), Excel.Range).Value), "", CType(range.Cells(i, 13), Excel.Range).Value)
            Dim strItemWeight As String = IIf(IsNothing(CType(range.Cells(i, 14), Excel.Range).Value), "", CType(range.Cells(i, 14), Excel.Range).Value)
            Dim strVATable As String = IIf(IsNothing(CType(range.Cells(i, 15), Excel.Range).Value), "", CType(range.Cells(i, 15), Excel.Range).Value)
            Dim strLot_Size As String = IIf(IsNothing(CType(range.Cells(i, 16), Excel.Range).Value), "", CType(range.Cells(i, 16), Excel.Range).Value)
            Dim strisInventory As String = IIf(IsNothing(CType(range.Cells(i, 17), Excel.Range).Value), "", CType(range.Cells(i, 17), Excel.Range).Value)
            Dim strisSales As String = IIf(IsNothing(CType(range.Cells(i, 18), Excel.Range).Value), "", CType(range.Cells(i, 18), Excel.Range).Value)
            Dim strisPurchase As String = IIf(IsNothing(CType(range.Cells(i, 19), Excel.Range).Value), "", CType(range.Cells(i, 19), Excel.Range).Value)
            Dim strisProduce As String = IIf(IsNothing(CType(range.Cells(i, 20), Excel.Range).Value), "", CType(range.Cells(i, 20), Excel.Range).Value)
            Dim strisOwned As String = IIf(IsNothing(CType(range.Cells(i, 21), Excel.Range).Value), "", CType(range.Cells(i, 21), Excel.Range).Value)
            Dim strisFixAsset As String = IIf(IsNothing(CType(range.Cells(i, 22), Excel.Range).Value), "", CType(range.Cells(i, 22), Excel.Range).Value)
            Dim strisConsignment As String = IIf(IsNothing(CType(range.Cells(i, 23), Excel.Range).Value), "", CType(range.Cells(i, 23), Excel.Range).Value)
            Dim strisMultipleBarcode As String = IIf(IsNothing(CType(range.Cells(i, 24), Excel.Range).Value), "", CType(range.Cells(i, 24), Excel.Range).Value)
            Dim strPurchaseData_UpdateLatest As String = IIf(IsNothing(CType(range.Cells(i, 31), Excel.Range).Value), "", CType(range.Cells(i, 31), Excel.Range).Value)
            Dim strPurchaseData_Supplier As String = IIf(IsNothing(CType(range.Cells(i, 32), Excel.Range).Value), "", CType(range.Cells(i, 32), Excel.Range).Value)
            Dim strPurchaseData_UnitCost As String = IIf(IsNothing(CType(range.Cells(i, 33), Excel.Range).Value), "", CType(range.Cells(i, 33), Excel.Range).Value)
            Dim strPurchaseData_UOM As String = IIf(IsNothing(CType(range.Cells(i, 34), Excel.Range).Value), "", CType(range.Cells(i, 34), Excel.Range).Value)
            Dim strPurchaseData_SafetyStock As String = IIf(IsNothing(CType(range.Cells(i, 35), Excel.Range).Value), "", CType(range.Cells(i, 35), Excel.Range).Value)
            Dim strPurchaseData_ReorderQTY As String = IIf(IsNothing(CType(range.Cells(i, 36), Excel.Range).Value), "", CType(range.Cells(i, 36), Excel.Range).Value)
            Dim strPurchaseData_VATinc As String = IIf(IsNothing(CType(range.Cells(i, 37), Excel.Range).Value), "", CType(range.Cells(i, 37), Excel.Range).Value)
            Dim strInventoryData_Max As String = IIf(IsNothing(CType(range.Cells(i, 38), Excel.Range).Value), "", CType(range.Cells(i, 38), Excel.Range).Value)
            Dim strInventoryData_Min As String = IIf(IsNothing(CType(range.Cells(i, 39), Excel.Range).Value), "", CType(range.Cells(i, 39), Excel.Range).Value)
            Dim strInventoryData_Method As String = IIf(IsNothing(CType(range.Cells(i, 40), Excel.Range).Value), "", CType(range.Cells(i, 40), Excel.Range).Value)
            Dim strInventoryData_UOM As String = IIf(IsNothing(CType(range.Cells(i, 41), Excel.Range).Value), "", CType(range.Cells(i, 41), Excel.Range).Value)
            Dim strInventoryData_Warehouse As String = IIf(IsNothing(CType(range.Cells(i, 42), Excel.Range).Value), "", CType(range.Cells(i, 42), Excel.Range).Value)
            Dim strInventoryData_StandardCost As String = IIf(IsNothing(CType(range.Cells(i, 43), Excel.Range).Value), "", CType(range.Cells(i, 43), Excel.Range).Value)
            Dim strAccountingData_Sales As String = IIf(IsNothing(CType(range.Cells(i, 44), Excel.Range).Value), "", CType(range.Cells(i, 44), Excel.Range).Value)
            Dim strAccountingData_CostofSales As String = IIf(IsNothing(CType(range.Cells(i, 45), Excel.Range).Value), "", CType(range.Cells(i, 45), Excel.Range).Value)
            Dim strAcountingData_Inventory As String = IIf(IsNothing(CType(range.Cells(i, 46), Excel.Range).Value), "", CType(range.Cells(i, 46), Excel.Range).Value)
            Dim strAccountingData_Discount As String = IIf(IsNothing(CType(range.Cells(i, 47), Excel.Range).Value), "", CType(range.Cells(i, 47), Excel.Range).Value)
            Dim strAccountingData_Expense As String = IIf(IsNothing(CType(range.Cells(i, 48), Excel.Range).Value), "", CType(range.Cells(i, 48), Excel.Range).Value)
            Dim strAccountingData_Consignment As String = IIf(IsNothing(CType(range.Cells(i, 49), Excel.Range).Value), "", CType(range.Cells(i, 49), Excel.Range).Value)
            Dim strDiscount As String = IIf(IsNothing(CType(range.Cells(i, 50), Excel.Range).Value), "", CType(range.Cells(i, 50), Excel.Range).Value)
            Dim strStatus As String = IIf(IsNothing(CType(range.Cells(i, 51), Excel.Range).Value), "", CType(range.Cells(i, 51), Excel.Range).Value)

            If IsNothing(strItemCode) = True Or strItemCode = "" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid ItemCode"
            Else
                query = " SELECT * FROM tblItem_Master WHERE ItemCode = @ItemCode "
                SQL.FlushParams()
                SQL.AddParam("@ItemCode", strItemCode)
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid ItemCode. Duplicate ItemCode."
                End If
            End If
            If IsNothing(strBarcode) = True Or strBarcode = "" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid Barcode"
            End If
            If IsNothing(strItemName) = True Or strItemName = "" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid ItemName"
            End If
            If IsNothing(strItemUOM) = True Or strItemUOM = "" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid ItemUOM"
            Else
                query = " SELECT * FROM tblUOM WHERE UnitCode = @UnitCode "
                SQL.FlushParams()
                SQL.AddParam("@UnitCode", strItemUOM)
                SQL.ReadQuery(query)
                If Not SQL.SQLDR.Read Then
                    strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid ItemUOM. " & strItemUOM & " is not in the list of UOM."
                End If
            End If
            If strVATable <> "0" And strVATable <> "1" And strVATable.ToLower <> "true" And strVATable.ToLower <> "false" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid VATable"
            End If
            If strisInventory <> "0" And strisInventory <> "1" And strisInventory.ToLower <> "true" And strisInventory.ToLower <> "false" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid isInventory"
            End If
            If strisSales <> "0" And strisSales <> "1" And strisSales.ToLower <> "true" And strisSales.ToLower <> "false" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid isSales"
            End If
            If strisPurchase <> "0" And strisPurchase <> "1" And strisPurchase.ToLower <> "true" And strisPurchase.ToLower <> "false" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid isPurchase"
            End If
            If strisProduce <> "0" And strisProduce <> "1" And strisProduce.ToLower <> "true" And strisProduce.ToLower <> "false" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid isProduce"
            End If
            If strisOwned <> "0" And strisOwned <> "1" And strisOwned.ToLower <> "true" And strisOwned.ToLower <> "false" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid isOwned"
            End If
            If strisFixAsset <> "0" And strisFixAsset <> "1" And strisFixAsset.ToLower <> "true" And strisFixAsset.ToLower <> "false" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid isFixAsset"
            End If
            If strisConsignment <> "0" And strisConsignment <> "1" And strisConsignment.ToLower <> "true" And strisConsignment.ToLower <> "false" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid isConsignment"
            End If
            If strisMultipleBarcode <> "0" And strisMultipleBarcode <> "1" And strisMultipleBarcode.ToLower <> "true" And strisMultipleBarcode.ToLower <> "false" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid isMultipleBarcode"
            End If
            If Not IsNothing(strPurchaseData_UnitCost) = True AndAlso Not IsNumeric(strPurchaseData_UnitCost) Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid PurchaseData_UnitCost"
            End If
            If IsNothing(strPurchaseData_UOM) = True Or strPurchaseData_UOM = "" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid PurchaseData_UOM"
            Else
                query = " SELECT * FROM tblUOM WHERE UnitCode = @UnitCode "
                SQL.FlushParams()
                SQL.AddParam("@UnitCode", strPurchaseData_UOM)
                SQL.ReadQuery(query)
                If Not SQL.SQLDR.Read Then
                    strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid PurchaseData_UOM. " & strItemUOM & " is not in the list of UOM."
                End If
            End If
            If IsNothing(strInventoryData_Method) = True Or strInventoryData_Method = "" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid InventoryData_Method"
            End If
            If IsNothing(strInventoryData_UOM) = True Or strInventoryData_UOM = "" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid PurchaseData_UOM"
            Else
                query = " SELECT * FROM tblUOM WHERE UnitCode = @UnitCode "
                SQL.FlushParams()
                SQL.AddParam("@UnitCode", strInventoryData_UOM)
                SQL.ReadQuery(query)
                If Not SQL.SQLDR.Read Then
                    strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid InventoryData_UOM. " & strItemUOM & " is not in the list of UOM."
                End If
            End If
            If Not IsNothing(strInventoryData_StandardCost) = True AndAlso Not IsNumeric(strInventoryData_StandardCost) Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid InventoryData_StandardCost"
            End If
            If IsNothing(strAccountingData_Sales) = True Or strAccountingData_Sales = "" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid AccountingData_Sales"
            Else
                query = " SELECT * FROM tblCOA_Master WHERE AccountCode = @AccountCode "
                SQL.FlushParams()
                SQL.AddParam("@AccountCode", strAccountingData_Sales)
                SQL.ReadQuery(query)
                If Not SQL.SQLDR.Read Then
                    strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid AccountingData_Sales. " & strAccountingData_Sales & " is not in the list of Chart of Account."
                End If
            End If
            If IsNothing(strAccountingData_CostofSales) = True Or strAccountingData_CostofSales = "" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid AccountingData_CostofSales"
            Else
                query = " SELECT * FROM tblCOA_Master WHERE AccountCode = @AccountCode "
                SQL.FlushParams()
                SQL.AddParam("@AccountCode", strAccountingData_CostofSales)
                SQL.ReadQuery(query)
                If Not SQL.SQLDR.Read Then
                    strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid AccountingData_CostofSales. " & strAccountingData_CostofSales & " is not in the list of Chart of Account."
                End If
            End If
            If IsNothing(strAcountingData_Inventory) = True Or strAcountingData_Inventory = "" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid AcountingData_Inventory"
            Else
                query = " SELECT * FROM tblCOA_Master WHERE AccountCode = @AccountCode "
                SQL.FlushParams()
                SQL.AddParam("@AccountCode", strAcountingData_Inventory)
                SQL.ReadQuery(query)
                If Not SQL.SQLDR.Read Then
                    strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid AcountingData_Inventory. " & strAcountingData_Inventory & " is not in the list of Chart of Account."
                End If
            End If
            If IsNothing(strAccountingData_Expense) = True Or strAccountingData_Expense = "" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid AccountingData_Expense"
            Else
                query = " SELECT * FROM tblCOA_Master WHERE AccountCode = @AccountCode "
                SQL.FlushParams()
                SQL.AddParam("@AccountCode", strAccountingData_Expense)
                SQL.ReadQuery(query)
                If Not SQL.SQLDR.Read Then
                    strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid AccountingData_Expense. " & strAccountingData_Expense & " is not in the list of Chart of Account."
                End If
            End If
            If IsNothing(strStatus) = True Or strItemCode = "" Then
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid Status"
            End If
            AddRowItemMaster(strItemCode, strBarcode, strItemName, strItemDescription, strG1, strG2, strG3, strG4, strG5, strItemType, strItemCategory, strItemUOMGroup, strItemUOM, strItemWeight, strVATable, strLot_Size, strisInventory, strisSales, strisPurchase, strisProduce, strisOwned, strisFixAsset, strisConsignment, strisMultipleBarcode, strPurchaseData_UpdateLatest, strPurchaseData_Supplier, strPurchaseData_UnitCost, strPurchaseData_UOM, strPurchaseData_SafetyStock, strPurchaseData_ReorderQTY, strPurchaseData_VATinc, strInventoryData_Max, strInventoryData_Min, strInventoryData_Method, strInventoryData_UOM, strInventoryData_Warehouse, strInventoryData_StandardCost, strAccountingData_Sales, strAccountingData_CostofSales, strAcountingData_Inventory, strAccountingData_Discount, strAccountingData_Expense, strAccountingData_Consignment, strDiscount, strStatus)

            'Item Barcode
            Dim strBarcode_1 As String = IIf(IsNothing(CType(range.Cells(i, 25), Excel.Range).Value), "", CType(range.Cells(i, 25), Excel.Range).Value)
            Dim strUOM_1 As String = IIf(IsNothing(CType(range.Cells(i, 26), Excel.Range).Value), "", CType(range.Cells(i, 26), Excel.Range).Value)
            Dim strBarcode_2 As String = IIf(IsNothing(CType(range.Cells(i, 27), Excel.Range).Value), "", CType(range.Cells(i, 27), Excel.Range).Value)
            Dim strUOM_2 As String = IIf(IsNothing(CType(range.Cells(i, 28), Excel.Range).Value), "", CType(range.Cells(i, 28), Excel.Range).Value)
            Dim strBarcode_3 As String = IIf(IsNothing(CType(range.Cells(i, 29), Excel.Range).Value), "", CType(range.Cells(i, 29), Excel.Range).Value)
            Dim strUOM_3 As String = IIf(IsNothing(CType(range.Cells(i, 30), Excel.Range).Value), "", CType(range.Cells(i, 30), Excel.Range).Value)

            If strBarcode_1 = "" And strBarcode_2 = "" And strBarcode_3 = "" And strUOM_1 = "" And strUOM_2 = "" And strUOM_3 = "" Then
                AddRowItemBarcode(strItemCode, strBarcode, strItemUOM)
            Else
                If strBarcode_1 <> "" And strUOM_1 <> "" Then
                    AddRowItemBarcode(strItemCode, strBarcode_1, strUOM_1)
                ElseIf strBarcode_1 <> "" Or strUOM_1 <> "" Then
                    strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid Barcode_1 and UOM_1"
                    AddRowItemBarcode(strItemCode, strBarcode_1, strUOM_1)
                    dgvItemBarcode.Rows(dgvItemBarcode.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
                End If
                If strBarcode_2 <> "" And strUOM_2 <> "" Then
                    AddRowItemBarcode(strItemCode, strBarcode_2, strUOM_2)
                ElseIf strBarcode_2 <> "" Or strUOM_2 <> "" Then
                    strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid Barcode_2 and UOM_2"
                    AddRowItemBarcode(strItemCode, strBarcode_1, strUOM_1)
                    dgvItemBarcode.Rows(dgvItemBarcode.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
                End If
                If strBarcode_3 <> "" And strUOM_3 <> "" Then
                    AddRowItemBarcode(strItemCode, strBarcode_3, strUOM_3)
                ElseIf strBarcode_3 <> "" Or strUOM_3 <> "" Then
                    strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid Barcode_3 and UOM_3"
                    AddRowItemBarcode(strItemCode, strBarcode_1, strUOM_1)
                    dgvItemBarcode.Rows(dgvItemBarcode.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
                End If
            End If

            'Item Price
            Dim strPriceType_1 As String = IIf(IsNothing(CType(range.Cells(i, 52), Excel.Range).Value), "", CType(range.Cells(i, 52), Excel.Range).Value)
            Dim strPriceUOM_1 As String = IIf(IsNothing(CType(range.Cells(i, 53), Excel.Range).Value), "", CType(range.Cells(i, 53), Excel.Range).Value)
            Dim strPriceUOMQTY_1 As String = IIf(IsNothing(CType(range.Cells(i, 54), Excel.Range).Value), "", CType(range.Cells(i, 54), Excel.Range).Value)
            Dim strPriceUnitPrice_1 As String = IIf(IsNothing(CType(range.Cells(i, 55), Excel.Range).Value), "", CType(range.Cells(i, 55), Excel.Range).Value)
            Dim strPriceVATInclusive_1 As String = IIf(IsNothing(CType(range.Cells(i, 56), Excel.Range).Value), "", CType(range.Cells(i, 56), Excel.Range).Value)
            Dim strVATable_1 As String = IIf(IsNothing(CType(range.Cells(i, 57), Excel.Range).Value), "", CType(range.Cells(i, 57), Excel.Range).Value)
            Dim strPriceType_2 As String = IIf(IsNothing(CType(range.Cells(i, 58), Excel.Range).Value), "", CType(range.Cells(i, 58), Excel.Range).Value)
            Dim strPriceUOM_2 As String = IIf(IsNothing(CType(range.Cells(i, 59), Excel.Range).Value), "", CType(range.Cells(i, 59), Excel.Range).Value)
            Dim strPriceUOMQTY_2 As String = IIf(IsNothing(CType(range.Cells(i, 60), Excel.Range).Value), "", CType(range.Cells(i, 60), Excel.Range).Value)
            Dim strPriceUnitPrice_2 As String = IIf(IsNothing(CType(range.Cells(i, 61), Excel.Range).Value), "", CType(range.Cells(i, 61), Excel.Range).Value)
            Dim strPriceVATInclusive_2 As String = IIf(IsNothing(CType(range.Cells(i, 62), Excel.Range).Value), "", CType(range.Cells(i, 62), Excel.Range).Value)
            Dim strVATable_2 As String = IIf(IsNothing(CType(range.Cells(i, 63), Excel.Range).Value), "", CType(range.Cells(i, 63), Excel.Range).Value)
            Dim strPriceType_3 As String = IIf(IsNothing(CType(range.Cells(i, 64), Excel.Range).Value), "", CType(range.Cells(i, 64), Excel.Range).Value)
            Dim strPriceUOM_3 As String = IIf(IsNothing(CType(range.Cells(i, 65), Excel.Range).Value), "", CType(range.Cells(i, 65), Excel.Range).Value)
            Dim strPriceUOMQTY_3 As String = IIf(IsNothing(CType(range.Cells(i, 66), Excel.Range).Value), "", CType(range.Cells(i, 66), Excel.Range).Value)
            Dim strPriceUnitPrice_3 As String = IIf(IsNothing(CType(range.Cells(i, 67), Excel.Range).Value), "", CType(range.Cells(i, 67), Excel.Range).Value)
            Dim strPriceVATInclusive_3 As String = IIf(IsNothing(CType(range.Cells(i, 68), Excel.Range).Value), "", CType(range.Cells(i, 68), Excel.Range).Value)
            Dim strVATable_3 As String = IIf(IsNothing(CType(range.Cells(i, 69), Excel.Range).Value), "", CType(range.Cells(i, 69), Excel.Range).Value)

            If strPriceType_1 <> "" And strPriceUOM_1 <> "" And IsNumeric(strPriceUOMQTY_1) = True And IsNumeric(strPriceUnitPrice_1) = True And (strPriceVATInclusive_1 = "0" Or strPriceVATInclusive_1 = "1" Or strPriceVATInclusive_1.ToLower = "true" Or strPriceVATInclusive_1.ToLower = "false") And (strVATable_1 = "0" Or strVATable_1 = "1" Or strVATable_1.ToLower = "true" Or strVATable_1.ToLower = "false") Then
                AddRowItemPrice(strPriceType_1, strItemCode, strPriceUOM_1, strPriceUOMQTY_1, strPriceUnitPrice_1, IIf(strPriceVATInclusive_1 = "0" And strPriceVATInclusive_1.ToLower = "false", "0", "1"), IIf(strVATable_1 = "0" And strVATable_1.ToLower = "false", "0", "1"))
            Else
                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid Item Price 1"
                AddRowItemPrice(strPriceType_1, strItemCode, strPriceUOM_1, strPriceUOMQTY_1, strPriceUnitPrice_1, strPriceVATInclusive_1, strVATable_1)
                dgvItemPrice.Rows(dgvItemPrice.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
            End If
            If strPriceType_2 <> "" And strPriceUOM_2 <> "" And IsNumeric(strPriceUOMQTY_2) = True And IsNumeric(strPriceUnitPrice_2) = True And (strPriceVATInclusive_2 = "0" Or strPriceVATInclusive_2 = "1" Or strPriceVATInclusive_2.ToLower = "true" Or strPriceVATInclusive_2.ToLower = "false") And (strVATable_2 = "0" Or strVATable_2 = "1" Or strVATable_2.ToLower = "true" Or strVATable_2.ToLower = "false") Then
                AddRowItemPrice(strPriceType_2, strItemCode, strPriceUOM_2, strPriceUOMQTY_2, strPriceUnitPrice_2, IIf(strPriceVATInclusive_2 = "0" And strPriceVATInclusive_2.ToLower = "false", "0", "1"), IIf(strVATable_2 = "0" And strVATable_2.ToLower = "false", "0", "1"))
            End If
            If strPriceType_3 <> "" And strPriceUOM_3 <> "" And IsNumeric(strPriceUOMQTY_3) = True And IsNumeric(strPriceUnitPrice_3) = True And (strPriceVATInclusive_3 = "0" Or strPriceVATInclusive_3 = "1" Or strPriceVATInclusive_3.ToLower = "true" Or strPriceVATInclusive_3.ToLower = "false") And (strVATable_3 = "0" Or strVATable_3 = "1" Or strVATable_3.ToLower = "true" Or strVATable_3.ToLower = "false") Then
                AddRowItemPrice(strPriceType_3, strItemCode, strPriceUOM_3, strPriceUOMQTY_3, strPriceUnitPrice_3, IIf(strPriceVATInclusive_3 = "0" And strPriceVATInclusive_3.ToLower = "false", "0", "1"), IIf(strVATable_3 = "0" And strVATable_3.ToLower = "false", "0", "1"))
            End If

            'Item Barcode
            Dim strUOMFrom As String = IIf(IsNothing(CType(range.Cells(i, 70), Excel.Range).Value), "", CType(range.Cells(i, 70), Excel.Range).Value)
            Dim strConvertQTY As String = IIf(IsNothing(CType(range.Cells(i, 71), Excel.Range).Value), "", CType(range.Cells(i, 71), Excel.Range).Value)
            Dim strUOMTo As String = IIf(IsNothing(CType(range.Cells(i, 72), Excel.Range).Value), "", CType(range.Cells(i, 72), Excel.Range).Value)

            If IsNothing(strUOMFrom) = True Or strUOMFrom = "" Then
                'strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid UOMFrom"
            Else
                query = " SELECT * FROM tblUOM WHERE UnitCode = @UnitCode "
                SQL.FlushParams()
                SQL.AddParam("@UnitCode", strUOMFrom)
                SQL.ReadQuery(query)
                If Not SQL.SQLDR.Read Then
                    strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid UOMFrom. " & strUOMFrom & " is not in the list of UOM."
                End If
            End If

            If IsNothing(strUOMTo) = True Or strUOMTo = "" Then
                'strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid UOMTo"
            Else
                query = " SELECT * FROM tblUOM WHERE UnitCode = @UnitCode "
                SQL.FlushParams()
                SQL.AddParam("@UnitCode", strUOMFrom)
                SQL.ReadQuery(query)
                If Not SQL.SQLDR.Read Then
                    strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid UOMTo. " & strUOMTo & " is not in the list of UOM."
                End If
            End If

            If strErrorMessage <> "" Then
                strErrorMessage &= vbCrLf & vbCrLf & "Row No.: " & dgvItemMaster.Rows.Count
                dgvItemMaster.Rows(dgvItemMaster.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
                MsgBox(strErrorMessage, MsgBoxStyle.Critical, "Error")
                Exit For
            Else
            End If
        Next

        '' GET ROWS
        'Dim count As Integer = 0
        'cn.Open()
        'cmd = cn.CreateCommand
        'cmd.CommandText = "SELECT * FROM [Item$]"
        'dr = cmd.ExecuteReader
        'Dim strErrorMessage As String = ""
        'If dr.HasRows Then
        '    While dr.Read
        '        Dim strItemCode As String = dr("ItemCode").ToString()
        '        Dim strBarcode As String = dr("Barcode").ToString()
        '        Dim strItemName As String = dr("ItemName").ToString()
        '        Dim strItemDescription As String = dr("ItemDescription").ToString()
        '        Dim strG1 As String = ""
        '        Dim strG2 As String = ""
        '        Dim strG3 As String = ""
        '        Dim strG4 As String = ""
        '        Dim strG5 As String = ""
        '        Dim GCount As Integer = 1
        '        Dim query As String = " SELECT Description, GroupID FROM tblGroup where Type = 'Item Group' ORDER BY GroupID "
        '        SQL.FlushParams()
        '        SQL.ReadQuery(query)
        '        While SQL.SQLDR.Read
        '            Select Case GCount
        '                Case 1
        '                    strG1 = dr(SQL.SQLDR("Description").ToString).ToString()
        '                Case 2
        '                    strG2 = dr(SQL.SQLDR("Description").ToString).ToString()
        '                Case 3
        '                    strG3 = dr(SQL.SQLDR("Description").ToString).ToString()
        '                Case 4
        '                    strG4 = dr(SQL.SQLDR("Description").ToString).ToString()
        '                Case 5
        '                    strG5 = dr(SQL.SQLDR("Description").ToString).ToString()
        '            End Select
        '            GCount += 1
        '        End While
        '        For i As Integer = GCount To 5
        '            Select Case i
        '                Case 1
        '                    strG1 = dr("G" & i).ToString()
        '                Case 2
        '                    strG2 = dr("G" & i).ToString()
        '                Case 3
        '                    strG3 = dr("G" & i).ToString()
        '                Case 4
        '                    strG4 = dr("G" & i).ToString()
        '                Case 5
        '                    strG5 = dr("G" & i).ToString()
        '            End Select
        '        Next
        '        Dim strItemType As String = dr("ItemType").ToString()
        '        Dim strItemCategory As String = dr("ItemCategory").ToString()
        '        Dim strItemUOMGroup As String = dr("ItemUOMGroup").ToString()
        '        Dim strItemUOM As String = dr("ItemUOM").ToString()
        '        Dim strItemWeight As String = dr("ItemWeight").ToString()
        '        Dim strVATable As String = dr("VATable").ToString()
        '        Dim strLot_Size As String = dr("Lot_Size").ToString()
        '        Dim strisInventory As String = dr("isInventory").ToString()
        '        Dim strisSales As String = dr("isSales").ToString()
        '        Dim strisPurchase As String = dr("isPurchase").ToString()
        '        Dim strisProduce As String = dr("isProduce").ToString()
        '        Dim strisOwned As String = dr("isOwned").ToString()
        '        Dim strisFixAsset As String = dr("isFixAsset").ToString()
        '        Dim strisConsignment As String = dr("isConsignment").ToString()
        '        Dim strisMultipleBarcode As String = dr("isMultipleBarcode").ToString()
        '        Dim strPurchaseData_UpdateLatest As String = dr("PurchaseData_UpdateLatest").ToString()
        '        Dim strPurchaseData_Supplier As String = dr("PurchaseData_Supplier").ToString()
        '        Dim strPurchaseData_UnitCost As String = dr("PurchaseData_UnitCost").ToString()
        '        Dim strPurchaseData_UOM As String = dr("PurchaseData_UOM").ToString()
        '        Dim strPurchaseData_SafetyStock As String = dr("PurchaseData_SafetyStock").ToString()
        '        Dim strPurchaseData_ReorderQTY As String = dr("PurchaseData_ReorderQTY").ToString()
        '        Dim strPurchaseData_VATinc As String = dr("PurchaseData_VATinc").ToString()
        '        Dim strInventoryData_Max As String = dr("InventoryData_Max").ToString()
        '        Dim strInventoryData_Min As String = dr("InventoryData_Min").ToString()
        '        Dim strInventoryData_Method As String = dr("InventoryData_Method").ToString()
        '        Dim strInventoryData_UOM As String = dr("InventoryData_UOM").ToString()
        '        Dim strInventoryData_Warehouse As String = dr("InventoryData_Warehouse").ToString()
        '        Dim strInventoryData_StandardCost As String = dr("InventoryData_StandardCost").ToString()
        '        Dim strAccountingData_Sales As String = dr("AccountingData_Sales").ToString()
        '        Dim strAccountingData_CostofSales As String = dr("AccountingData_CostofSales").ToString()
        '        Dim strAcountingData_Inventory As String = dr("AcountingData_Inventory").ToString()
        '        Dim strAccountingData_Discount As String = dr("AccountingData_Discount").ToString()
        '        Dim strAccountingData_Expense As String = dr("AccountingData_Expense").ToString()
        '        Dim strAccountingData_Consignment As String = dr("AccountingData_Consignment").ToString()
        '        Dim strDiscount As String = dr("Discount").ToString()
        '        Dim strStatus As String = dr("Status").ToString()

        '        If IsNothing(strItemCode) = True Or strItemCode = "" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid ItemCode"
        '        Else
        '            query = " SELECT * FROM tblItem_Master WHERE ItemCode = @ItemCode "
        '            SQL.FlushParams()
        '            SQL.AddParam("@ItemCode", strItemCode)
        '            SQL.ReadQuery(query)
        '            If SQL.SQLDR.Read Then
        '                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid ItemCode. Duplicate ItemCode."
        '            End If
        '        End If
        '        If IsNothing(strBarcode) = True Or strBarcode = "" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid Barcode"
        '        End If
        '        If IsNothing(strItemName) = True Or strItemName = "" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid ItemName"
        '        End If
        '        If IsNothing(strItemUOM) = True Or strItemUOM = "" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid ItemUOM"
        '        Else
        '            query = " SELECT * FROM tblUOM WHERE UnitCode = @UnitCode "
        '            SQL.FlushParams()
        '            SQL.AddParam("@UnitCode", strItemUOM)
        '            SQL.ReadQuery(query)
        '            If Not SQL.SQLDR.Read Then
        '                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid ItemUOM. " & strItemUOM & " is not in the list of UOM."
        '            End If
        '        End If
        '        If strVATable <> "0" And strVATable <> "1" And strVATable.ToLower <> "true" And strVATable.ToLower <> "false" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid VATable"
        '        End If
        '        If strisInventory <> "0" And strisInventory <> "1" And strisInventory.ToLower <> "true" And strisInventory.ToLower <> "false" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid isInventory"
        '        End If
        '        If strisSales <> "0" And strisSales <> "1" And strisSales.ToLower <> "true" And strisSales.ToLower <> "false" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid isSales"
        '        End If
        '        If strisPurchase <> "0" And strisPurchase <> "1" And strisPurchase.ToLower <> "true" And strisPurchase.ToLower <> "false" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid isPurchase"
        '        End If
        '        If strisProduce <> "0" And strisProduce <> "1" And strisProduce.ToLower <> "true" And strisProduce.ToLower <> "false" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid isProduce"
        '        End If
        '        If strisOwned <> "0" And strisOwned <> "1" And strisOwned.ToLower <> "true" And strisOwned.ToLower <> "false" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid isOwned"
        '        End If
        '        If strisFixAsset <> "0" And strisFixAsset <> "1" And strisFixAsset.ToLower <> "true" And strisFixAsset.ToLower <> "false" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid isFixAsset"
        '        End If
        '        If strisConsignment <> "0" And strisConsignment <> "1" And strisConsignment.ToLower <> "true" And strisConsignment.ToLower <> "false" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid isConsignment"
        '        End If
        '        If strisMultipleBarcode <> "0" And strisMultipleBarcode <> "1" And strisMultipleBarcode.ToLower <> "true" And strisMultipleBarcode.ToLower <> "false" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid isMultipleBarcode"
        '        End If
        '        If Not IsNothing(strPurchaseData_UnitCost) = True AndAlso Not IsNumeric(strPurchaseData_UnitCost) Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid PurchaseData_UnitCost"
        '        End If
        '        If IsNothing(strPurchaseData_UOM) = True Or strPurchaseData_UOM = "" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid PurchaseData_UOM"
        '        Else
        '            query = " SELECT * FROM tblUOM WHERE UnitCode = @UnitCode "
        '            SQL.FlushParams()
        '            SQL.AddParam("@UnitCode", strPurchaseData_UOM)
        '            SQL.ReadQuery(query)
        '            If Not SQL.SQLDR.Read Then
        '                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid PurchaseData_UOM. " & strItemUOM & " is not in the list of UOM."
        '            End If
        '        End If
        '        If IsNothing(strInventoryData_Method) = True Or strInventoryData_Method = "" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid InventoryData_Method"
        '        End If
        '        If IsNothing(strInventoryData_UOM) = True Or strInventoryData_UOM = "" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid PurchaseData_UOM"
        '        Else
        '            query = " SELECT * FROM tblUOM WHERE UnitCode = @UnitCode "
        '            SQL.FlushParams()
        '            SQL.AddParam("@UnitCode", strInventoryData_UOM)
        '            SQL.ReadQuery(query)
        '            If Not SQL.SQLDR.Read Then
        '                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid InventoryData_UOM. " & strItemUOM & " is not in the list of UOM."
        '            End If
        '        End If
        '        If Not IsNothing(strInventoryData_StandardCost) = True AndAlso Not IsNumeric(strInventoryData_StandardCost) Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid InventoryData_StandardCost"
        '        End If
        '        If IsNothing(strAccountingData_Sales) = True Or strAccountingData_Sales = "" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid AccountingData_Sales"
        '        Else
        '            query = " SELECT * FROM tblCOA_Master WHERE AccountCode = @AccountCode "
        '            SQL.FlushParams()
        '            SQL.AddParam("@AccountCode", strAccountingData_Sales)
        '            SQL.ReadQuery(query)
        '            If Not SQL.SQLDR.Read Then
        '                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid AccountingData_Sales. " & strAccountingData_Sales & " is not in the list of Chart of Account."
        '            End If
        '        End If
        '        If IsNothing(strAccountingData_CostofSales) = True Or strAccountingData_CostofSales = "" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid AccountingData_CostofSales"
        '        Else
        '            query = " SELECT * FROM tblCOA_Master WHERE AccountCode = @AccountCode "
        '            SQL.FlushParams()
        '            SQL.AddParam("@AccountCode", strAccountingData_CostofSales)
        '            SQL.ReadQuery(query)
        '            If Not SQL.SQLDR.Read Then
        '                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid AccountingData_CostofSales. " & strAccountingData_CostofSales & " is not in the list of Chart of Account."
        '            End If
        '        End If
        '        If IsNothing(strAcountingData_Inventory) = True Or strAcountingData_Inventory = "" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid AcountingData_Inventory"
        '        Else
        '            query = " SELECT * FROM tblCOA_Master WHERE AccountCode = @AccountCode "
        '            SQL.FlushParams()
        '            SQL.AddParam("@AccountCode", strAcountingData_Inventory)
        '            SQL.ReadQuery(query)
        '            If Not SQL.SQLDR.Read Then
        '                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid AcountingData_Inventory. " & strAcountingData_Inventory & " is not in the list of Chart of Account."
        '            End If
        '        End If
        '        If IsNothing(strAccountingData_Expense) = True Or strAccountingData_Expense = "" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid AccountingData_Expense"
        '        Else
        '            query = " SELECT * FROM tblCOA_Master WHERE AccountCode = @AccountCode "
        '            SQL.FlushParams()
        '            SQL.AddParam("@AccountCode", strAccountingData_Expense)
        '            SQL.ReadQuery(query)
        '            If Not SQL.SQLDR.Read Then
        '                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid AccountingData_Expense. " & strAccountingData_Expense & " is not in the list of Chart of Account."
        '            End If
        '        End If
        '        If IsNothing(strStatus) = True Or strItemCode = "" Then
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid Status"
        '        End If
        '        AddRowItemMaster(strItemCode, strBarcode, strItemName, strItemDescription, strG1, strG2, strG3, strG4, strG5, strItemType, strItemCategory, strItemUOMGroup, strItemUOM, strItemWeight, strVATable, strLot_Size, strisInventory, strisSales, strisPurchase, strisProduce, strisOwned, strisFixAsset, strisConsignment, strisMultipleBarcode, strPurchaseData_UpdateLatest, strPurchaseData_Supplier, strPurchaseData_UnitCost, strPurchaseData_UOM, strPurchaseData_SafetyStock, strPurchaseData_ReorderQTY, strPurchaseData_VATinc, strInventoryData_Max, strInventoryData_Min, strInventoryData_Method, strInventoryData_UOM, strInventoryData_Warehouse, strInventoryData_StandardCost, strAccountingData_Sales, strAccountingData_CostofSales, strAcountingData_Inventory, strAccountingData_Discount, strAccountingData_Expense, strAccountingData_Consignment, strDiscount, strStatus)

        '        'Item Barcode
        '        Dim strBarcode_1 As String = dr("Barcode_1").ToString()
        '        Dim strUOM_1 As String = dr("UOM_1").ToString()
        '        Dim strBarcode_2 As String = dr("Barcode_2").ToString()
        '        Dim strUOM_2 As String = dr("UOM_2").ToString()
        '        Dim strBarcode_3 As String = dr("Barcode_3").ToString()
        '        Dim strUOM_3 As String = dr("UOM_3").ToString()

        '        If strBarcode_1 = "" And strBarcode_2 = "" And strBarcode_3 = "" And strUOM_1 = "" And strUOM_2 = "" And strUOM_3 = "" Then
        '            AddRowItemBarcode(strItemCode, strBarcode, strItemUOM)
        '        Else
        '            If strBarcode_1 <> "" And strUOM_1 <> "" Then
        '                AddRowItemBarcode(strItemCode, strBarcode_1, strUOM_1)
        '            ElseIf strBarcode_1 <> "" Or strUOM_1 <> "" Then
        '                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid Barcode_1 and UOM_1"
        '                AddRowItemBarcode(strItemCode, strBarcode_1, strUOM_1)
        '                dgvItemBarcode.Rows(dgvItemBarcode.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
        '            End If
        '            If strBarcode_2 <> "" And strUOM_2 <> "" Then
        '                AddRowItemBarcode(strItemCode, strBarcode_2, strUOM_2)
        '            ElseIf strBarcode_2 <> "" Or strUOM_2 <> "" Then
        '                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid Barcode_2 and UOM_2"
        '                AddRowItemBarcode(strItemCode, strBarcode_1, strUOM_1)
        '                dgvItemBarcode.Rows(dgvItemBarcode.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
        '            End If
        '            If strBarcode_3 <> "" And strUOM_3 <> "" Then
        '                AddRowItemBarcode(strItemCode, strBarcode_3, strUOM_3)
        '            ElseIf strBarcode_3 <> "" Or strUOM_3 <> "" Then
        '                strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid Barcode_3 and UOM_3"
        '                AddRowItemBarcode(strItemCode, strBarcode_1, strUOM_1)
        '                dgvItemBarcode.Rows(dgvItemBarcode.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
        '            End If
        '        End If

        '        'Item Price
        '        Dim strPriceType_1 As String = dr("PriceType_1").ToString()
        '        Dim strPriceUOM_1 As String = dr("PriceUOM_1").ToString()
        '        Dim strPriceUOMQTY_1 As String = dr("PriceUOMQTY_1").ToString()
        '        Dim strPriceUnitPrice_1 As String = dr("PriceUnitPrice_1").ToString()
        '        Dim strPriceVATInclusive_1 As String = dr("PriceVATInclusive_1").ToString()
        '        Dim strVATable_1 As String = dr("VATable_1").ToString()
        '        Dim strPriceType_2 As String = dr("PriceType_2").ToString()
        '        Dim strPriceUOM_2 As String = dr("PriceUOM_2").ToString()
        '        Dim strPriceUOMQTY_2 As String = dr("PriceUOMQTY_2").ToString()
        '        Dim strPriceUnitPrice_2 As String = dr("PriceUnitPrice_2").ToString()
        '        Dim strPriceVATInclusive_2 As String = dr("PriceVATInclusive_2").ToString()
        '        Dim strVATable_2 As String = dr("VATable_2").ToString()
        '        Dim strPriceType_3 As String = dr("PriceType_3").ToString()
        '        Dim strPriceUOM_3 As String = dr("PriceUOM_3").ToString()
        '        Dim strPriceUOMQTY_3 As String = dr("PriceUOMQTY_3").ToString()
        '        Dim strPriceUnitPrice_3 As String = dr("PriceUnitPrice_3").ToString()
        '        Dim strPriceVATInclusive_3 As String = dr("PriceVATInclusive_3").ToString()
        '        Dim strVATable_3 As String = dr("VATable_3").ToString()

        '        If strPriceType_1 <> "" And strPriceUOM_1 <> "" And IsNumeric(strPriceUOMQTY_1) = True And IsNumeric(strPriceUnitPrice_1) = True And (strPriceVATInclusive_1 = "0" Or strPriceVATInclusive_1 = "1" Or strPriceVATInclusive_1.ToLower = "true" Or strPriceVATInclusive_1.ToLower = "false") And (strVATable_1 = "0" Or strVATable_1 = "1" Or strVATable_1.ToLower = "true" Or strVATable_1.ToLower = "false") Then
        '            AddRowItemPrice(strPriceType_1, strItemCode, strPriceUOM_1, strPriceUOMQTY_1, strPriceUnitPrice_1, IIf(strPriceVATInclusive_1 = "0" And strPriceVATInclusive_1.ToLower = "false", "0", "1"), IIf(strVATable_1 = "0" And strVATable_1.ToLower = "false", "0", "1"))
        '        Else
        '            strErrorMessage &= IIf(strErrorMessage = "", "", vbCrLf) & "Invalid Item Price 1"
        '            AddRowItemPrice(strPriceType_1, strItemCode, strPriceUOM_1, strPriceUOMQTY_1, strPriceUnitPrice_1, strPriceVATInclusive_1, strVATable_1)
        '            dgvItemPrice.Rows(dgvItemPrice.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
        '        End If
        '        If strPriceType_2 <> "" And strPriceUOM_2 <> "" And IsNumeric(strPriceUOMQTY_2) = True And IsNumeric(strPriceUnitPrice_2) = True And (strPriceVATInclusive_2 = "0" Or strPriceVATInclusive_2 = "1" Or strPriceVATInclusive_2.ToLower = "true" Or strPriceVATInclusive_2.ToLower = "false") And (strVATable_2 = "0" Or strVATable_2 = "1" Or strVATable_2.ToLower = "true" Or strVATable_2.ToLower = "false") Then
        '            AddRowItemPrice(strPriceType_2, strItemCode, strPriceUOM_2, strPriceUOMQTY_2, strPriceUnitPrice_2, IIf(strPriceVATInclusive_2 = "0" And strPriceVATInclusive_2.ToLower = "false", "0", "1"), IIf(strVATable_2 = "0" And strVATable_2.ToLower = "false", "0", "1"))
        '        End If
        '        If strPriceType_3 <> "" And strPriceUOM_3 <> "" And IsNumeric(strPriceUOMQTY_3) = True And IsNumeric(strPriceUnitPrice_3) = True And (strPriceVATInclusive_3 = "0" Or strPriceVATInclusive_3 = "1" Or strPriceVATInclusive_3.ToLower = "true" Or strPriceVATInclusive_3.ToLower = "false") And (strVATable_3 = "0" Or strVATable_3 = "1" Or strVATable_3.ToLower = "true" Or strVATable_3.ToLower = "false") Then
        '            AddRowItemPrice(strPriceType_3, strItemCode, strPriceUOM_3, strPriceUOMQTY_3, strPriceUnitPrice_3, IIf(strPriceVATInclusive_3 = "0" And strPriceVATInclusive_3.ToLower = "false", "0", "1"), IIf(strVATable_3 = "0" And strVATable_3.ToLower = "false", "0", "1"))
        '        End If

        '        If strErrorMessage <> "" Then
        '            strErrorMessage &= vbCrLf & vbCrLf & "Row No.: " & dgvItemMaster.Rows.Count
        '            dgvItemMaster.Rows(dgvItemMaster.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
        '            MsgBox(strErrorMessage, MsgBoxStyle.Critical, "Error")
        '            Exit While
        '        Else
        '        End If
        '    End While
        'End If
    End Sub

    Private Delegate Sub SetPGBmaxInvoker(ByVal Value As String)
    Private Sub SetPGBmax(ByVal Value As String)
        If Me.InvokeRequired Then
            Me.Invoke(New SetPGBmaxInvoker(AddressOf SetPGBmax), Value)
        Else
            pbItem.Maximum = Value
            pbItem.Value = 0
        End If
    End Sub

    Private Delegate Sub AddRowItemMasterInvoker(ByVal Data1 As String, ByVal Data2 As String, ByVal Data3 As String, ByVal Data4 As String, ByVal Data5 As String, ByVal Data6 As String, ByVal Data7 As String, ByVal Data8 As String, ByVal Data9 As String, ByVal Data10 As String, ByVal Data11 As String, ByVal Data12 As String, ByVal Data13 As String, ByVal Data14 As String, ByVal Data15 As String, ByVal Data16 As String, ByVal Data17 As String, ByVal Data18 As String, ByVal Data19 As String, ByVal Data20 As String, ByVal Data21 As String, ByVal Data22 As String, ByVal Data23 As String, ByVal Data24 As String, ByVal Data25 As String, ByVal Data26 As String, ByVal Data27 As String, ByVal Data28 As String, ByVal Data29 As String, ByVal Data30 As String, ByVal Data31 As String, ByVal Data32 As String, ByVal Data33 As String, ByVal Data34 As String, ByVal Data35 As String, ByVal Data36 As String, ByVal Data37 As String, ByVal Data38 As String, ByVal Data39 As String, ByVal Data40 As String, ByVal Data41 As String, ByVal Data42 As String, ByVal Data43 As String, ByVal Data44 As String, ByVal Data45 As String)
    Private Sub AddRowItemMaster(ByVal Data1 As String, ByVal Data2 As String, ByVal Data3 As String, ByVal Data4 As String, ByVal Data5 As String, ByVal Data6 As String, ByVal Data7 As String, ByVal Data8 As String, ByVal Data9 As String, ByVal Data10 As String, ByVal Data11 As String, ByVal Data12 As String, ByVal Data13 As String, ByVal Data14 As String, ByVal Data15 As String, ByVal Data16 As String, ByVal Data17 As String, ByVal Data18 As String, ByVal Data19 As String, ByVal Data20 As String, ByVal Data21 As String, ByVal Data22 As String, ByVal Data23 As String, ByVal Data24 As String, ByVal Data25 As String, ByVal Data26 As String, ByVal Data27 As String, ByVal Data28 As String, ByVal Data29 As String, ByVal Data30 As String, ByVal Data31 As String, ByVal Data32 As String, ByVal Data33 As String, ByVal Data34 As String, ByVal Data35 As String, ByVal Data36 As String, ByVal Data37 As String, ByVal Data38 As String, ByVal Data39 As String, ByVal Data40 As String, ByVal Data41 As String, ByVal Data42 As String, ByVal Data43 As String, ByVal Data44 As String, ByVal Data45 As String)
        If Me.InvokeRequired Then
            Me.Invoke(New AddRowItemMasterInvoker(AddressOf AddRowItemMaster), Data1, Data2, Data3, Data4, Data5, Data6, Data7, Data8, Data9, Data10, Data11, Data12, Data13, Data14, Data15, Data16, Data17, Data18, Data19, Data20, Data21, Data22, Data23, Data24, Data25, Data26, Data27, Data28, Data29, Data30, Data31, Data32, Data33, Data34, Data35, Data36, Data37, Data38, Data39, Data40, Data41, Data42, Data43, Data44, Data45)
        Else
            dgvItemMaster.Rows.Add(Data1, Data2, Data3, Data4, Data5, Data6, Data7, Data8, Data9, Data10, Data11, Data12, Data13, Data14, Data15, Data16, Data17, Data18, Data19, Data20, Data21, Data22, Data23, Data24, Data25, Data26, Data27, Data28, Data29, Data30, Data31, Data32, Data33, Data34, Data35, Data36, Data37, Data38, Data39, Data40, Data41, Data42, Data43, Data44, Data45)
        End If
    End Sub

    Private Delegate Sub AddRowItemBarcodeInvoker(ByVal Data1 As String, ByVal Data2 As String, ByVal Data3 As String)
    Private Sub AddRowItemBarcode(ByVal Data1 As String, ByVal Data2 As String, ByVal Data3 As String)
        If Me.InvokeRequired Then
            Me.Invoke(New AddRowItemBarcodeInvoker(AddressOf AddRowItemBarcode), Data1, Data2, Data3)
        Else
            dgvItemBarcode.Rows.Add(Data1, Data2, Data3)
        End If
    End Sub

    Private Delegate Sub AddRowItemPriceInvoker(ByVal Data1 As String, ByVal Data2 As String, ByVal Data3 As String, ByVal Data4 As String, ByVal Data5 As String, ByVal Data6 As String, ByVal Data7 As String)
    Private Sub AddRowItemPrice(ByVal Data1 As String, ByVal Data2 As String, ByVal Data3 As String, ByVal Data4 As String, ByVal Data5 As String, ByVal Data6 As String, ByVal Data7 As String)
        If Me.InvokeRequired Then
            Me.Invoke(New AddRowItemPriceInvoker(AddressOf AddRowItemPrice), Data1, Data2, Data3, Data4, Data5, Data6, Data7)
        Else
            dgvItemPrice.Rows.Add(Data1, Data2, Data3, Data4, Data5, Data6, Data7)
        End If
    End Sub

    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        Dim bool As Boolean = True
        For Each row As DataGridViewRow In dgvItemMaster.Rows
            If row.DefaultCellStyle.BackColor = Color.Yellow Then
                bool = False
                Exit For
            End If
        Next
        If bool = True Then
            If MsgBox("Are you sure you want to save this list?", MsgBoxStyle.YesNo, "Save") = MsgBoxResult.Yes Then
                'tblItem_Master
                For Each row As DataGridViewRow In dgvItemMaster.Rows
                    Dim strItemCode As String = row.Cells(0).Value
                    Dim strBarcode As String = row.Cells(1).Value
                    Dim strItemName As String = row.Cells(2).Value
                    Dim strItemDescription As String = row.Cells(3).Value
                    Dim strG1 As String = row.Cells(4).Value
                    Dim strG2 As String = row.Cells(5).Value
                    Dim strG3 As String = row.Cells(6).Value
                    Dim strG4 As String = row.Cells(7).Value
                    Dim strG5 As String = row.Cells(8).Value
                    Dim strItemType As String = row.Cells(9).Value
                    Dim strItemCategory As String = row.Cells(10).Value
                    Dim strItemUOMGroup As String = row.Cells(11).Value
                    Dim strItemUOM As String = row.Cells(12).Value
                    Dim strItemWeight As String = row.Cells(13).Value
                    Dim strVATable As String = row.Cells(14).Value
                    Dim strLot_Size As String = row.Cells(15).Value
                    Dim strisInventory As String = row.Cells(16).Value
                    Dim strisSales As String = row.Cells(17).Value
                    Dim strisPurchase As String = row.Cells(18).Value
                    Dim strisProduce As String = row.Cells(19).Value
                    Dim strisOwned As String = row.Cells(20).Value
                    Dim strisFixAsset As String = row.Cells(21).Value
                    Dim strisConsignment As String = row.Cells(22).Value
                    Dim strisMultipleBarcode As String = row.Cells(23).Value
                    Dim strPurchaseData_UpdateLatest As String = row.Cells(24).Value
                    Dim strPurchaseData_Supplier As String = row.Cells(25).Value
                    Dim strPurchaseData_UnitCost As String = row.Cells(26).Value
                    Dim strPurchaseData_UOM As String = row.Cells(27).Value
                    Dim strPurchaseData_SafetyStock As String = row.Cells(28).Value
                    Dim strPurchaseData_ReorderQTY As String = row.Cells(29).Value
                    Dim strPurchaseData_VATinc As String = row.Cells(30).Value
                    Dim strInventoryData_Max As String = row.Cells(31).Value
                    Dim strInventoryData_Min As String = row.Cells(32).Value
                    Dim strInventoryData_Method As String = row.Cells(33).Value
                    Dim strInventoryData_UOM As String = row.Cells(34).Value
                    Dim strInventoryData_Warehouse As String = row.Cells(35).Value
                    Dim strInventoryData_StandardCost As String = row.Cells(36).Value
                    Dim strAccountingData_Sales As String = row.Cells(37).Value
                    Dim strAccountingData_CostofSales As String = row.Cells(38).Value
                    Dim strAcountingData_Inventory As String = row.Cells(39).Value
                    Dim strAccountingData_Discount As String = row.Cells(40).Value
                    Dim strAccountingData_Expense As String = row.Cells(41).Value
                    Dim strAccountingData_Consignment As String = row.Cells(42).Value
                    Dim strDiscount As String = row.Cells(43).Value
                    Dim strStatus As String = row.Cells(44).Value

                    Dim query As String = " INSERT INTO tblItem_Master(ItemCode, Barcode, ItemName, ItemDescription, G1, G2, G3, G4, G5, ItemType, ItemCategory, ItemUOMGroup, ItemUOM, ItemWeight, VATable, Lot_Size, isInventory, isSales, isPurchase, isProduce, isOwned, isFixAsset, isConsignment, isMultipleBarcode, PD_UpdateLatest, PD_Supplier, PD_UnitCost, PD_UOM, PD_SafetyStock, PD_ReorderQTY, PD_VATinc, ID_Max, ID_Min, ID_Method, ID_UOM, ID_Warehouse, ID_SC, AD_Sales, AD_COS, AD_Inv, AD_Discount, AD_Expense, AD_Consignment, DiscountPercent, Status) " &
                                          " VALUES(@ItemCode, @Barcode, @ItemName, @ItemDescription, @G1, @G2, @G3, @G4, @G5, @ItemType, @ItemCategory, @ItemUOMGroup, @ItemUOM, @ItemWeight, @VATable, @Lot_Size, @isInventory, @isSales, @isPurchase, @isProduce, @isOwned, @isFixAsset, @isConsignment, @isMultipleBarcode, @PD_UpdateLatest, @PD_Supplier, @PD_UnitCost, @PD_UOM, @PD_SafetyStock, @PD_ReorderQTY, @PD_VATinc, @ID_Max, @ID_Min, @ID_Method, @ID_UOM, @ID_Warehouse, @ID_SC, @AD_Sales, @AD_COS, @AD_Inv, @AD_Discount, @AD_Expense, @AD_Consignment, @DiscountPercent, @Status) "
                    SQL.FlushParams()
                    SQL.AddParam("@ItemCode", strItemCode)
                    SQL.AddParam("@Barcode", strBarcode)
                    SQL.AddParam("@ItemName", strItemName)
                    SQL.AddParam("@ItemDescription", strItemDescription)
                    SQL.AddParam("@G1", strG1)
                    SQL.AddParam("@G2", strG2)
                    SQL.AddParam("@G3", strG3)
                    SQL.AddParam("@G4", strG4)
                    SQL.AddParam("@G5", strG5)
                    SQL.AddParam("@ItemType", strItemType)
                    SQL.AddParam("@ItemCategory", strItemCategory)
                    SQL.AddParam("@ItemUOMGroup", strItemUOMGroup)
                    SQL.AddParam("@ItemUOM", strItemUOM)
                    SQL.AddParam("@ItemWeight", IIf(IsNumeric(strItemWeight), strItemWeight, DBNull.Value))
                    SQL.AddParam("@VATable", IIf(strVATable = "0" Or strVATable.ToLower = "false", "0", "1"))
                    SQL.AddParam("@Lot_Size", IIf(IsNumeric(strLot_Size), strLot_Size, DBNull.Value))
                    SQL.AddParam("@isInventory", IIf(strisInventory = "0" Or strisInventory.ToLower = "false", "0", "1"))
                    SQL.AddParam("@isSales", IIf(strisInventory = "0" Or strisInventory.ToLower = "false", "0", "1"))
                    SQL.AddParam("@isPurchase", IIf(strisSales = "0" Or strisSales.ToLower = "false", "0", "1"))
                    SQL.AddParam("@isProduce", IIf(strisProduce = "0" Or strisProduce.ToLower = "false", "0", "1"))
                    SQL.AddParam("@isOwned", IIf(strisOwned = "0" Or strisOwned.ToLower = "false", "0", "1"))
                    SQL.AddParam("@isFixAsset", IIf(strisFixAsset = "0" Or strisFixAsset.ToLower = "false", "0", "1"))
                    SQL.AddParam("@isConsignment", IIf(strisConsignment = "0" Or strisConsignment.ToLower = "false", "0", "1"))
                    SQL.AddParam("@isMultipleBarcode", IIf(strisMultipleBarcode = "0" Or strisMultipleBarcode.ToLower = "false", "0", "1"))
                    SQL.AddParam("@PD_UpdateLatest", strPurchaseData_UpdateLatest)
                    SQL.AddParam("@PD_Supplier", strPurchaseData_Supplier)
                    SQL.AddParam("@PD_UnitCost", IIf(IsNumeric(strPurchaseData_UnitCost), strPurchaseData_UnitCost, DBNull.Value))
                    SQL.AddParam("@PD_UOM", strPurchaseData_UOM)
                    SQL.AddParam("@PD_SafetyStock", IIf(IsNumeric(strPurchaseData_SafetyStock), strPurchaseData_SafetyStock, DBNull.Value))
                    SQL.AddParam("@PD_ReorderQTY", IIf(IsNumeric(strPurchaseData_ReorderQTY), strPurchaseData_ReorderQTY, DBNull.Value))
                    SQL.AddParam("@PD_VATinc", IIf(strPurchaseData_VATinc = "0" Or strPurchaseData_VATinc.ToLower = "false", "0", "1"))
                    SQL.AddParam("@ID_Max", IIf(IsNumeric(strInventoryData_Max), strInventoryData_Max, DBNull.Value))
                    SQL.AddParam("@ID_Min", IIf(IsNumeric(strInventoryData_Min), strInventoryData_Min, DBNull.Value))
                    SQL.AddParam("@ID_Method", strInventoryData_Method)
                    SQL.AddParam("@ID_UOM", strInventoryData_UOM)
                    SQL.AddParam("@ID_Warehouse", strInventoryData_Warehouse)
                    SQL.AddParam("@ID_SC", IIf(IsNumeric(strInventoryData_StandardCost), strInventoryData_StandardCost, DBNull.Value))
                    SQL.AddParam("@AD_Sales", strAccountingData_Sales)
                    SQL.AddParam("@AD_COS", strAccountingData_CostofSales)
                    SQL.AddParam("@AD_Inv", strAcountingData_Inventory)
                    SQL.AddParam("@AD_Discount", strAccountingData_Discount)
                    SQL.AddParam("@AD_Expense", strAccountingData_Expense)
                    SQL.AddParam("@AD_Consignment", strAccountingData_Consignment)
                    SQL.AddParam("@DiscountPercent", strDiscount)
                    SQL.AddParam("@Status", strStatus)
                    SQL.ExecNonQuery(query)
                Next

                'tblItem_Barcode
                For Each row As DataGridViewRow In dgvItemBarcode.Rows
                    Dim strItemCode As String = row.Cells(0).Value
                    Dim strBarcode As String = row.Cells(1).Value
                    Dim strUOM As String = row.Cells(2).Value

                    Dim query As String = " INSERT INTO tblItem_Barcode(ItemCode, Barcode, UOM, Status) " &
                                          " VALUES(@ItemCode, @Barcode, @UOM, @Status) "
                    SQL.FlushParams()
                    SQL.AddParam("@ItemCode", strItemCode)
                    SQL.AddParam("@Barcode", strBarcode)
                    SQL.AddParam("@UOM", strUOM)
                    SQL.AddParam("@Status", "Active")
                    SQL.ExecNonQuery(query)
                Next

                'tblItem_Price
                For Each row As DataGridViewRow In dgvItemPrice.Rows
                    Dim strPriceType As String = row.Cells(0).Value
                    Dim strItemCode As String = row.Cells(1).Value
                    Dim strUOM As String = row.Cells(2).Value
                    Dim strUOMQTY As String = row.Cells(3).Value
                    Dim strUnitPrice As String = row.Cells(4).Value
                    Dim strVATInc As String = row.Cells(5).Value
                    Dim strVATable As String = row.Cells(6).Value

                    Dim query As String = " INSERT INTO tblItem_Price(Category, Type, ItemCode, UOM, UOMQTY, UnitPrice, VATInclusive, VAT, Status) " &
                                          " VALUES(@Category, @Type, @ItemCode, @UOM, @UOMQTY, @UnitPrice, @VATInclusive, @VAT, @Status) "
                    SQL.FlushParams()
                    SQL.AddParam("@Category", "Selling")
                    SQL.AddParam("@Type", strPriceType)
                    SQL.AddParam("@ItemCode", strItemCode)
                    SQL.AddParam("@UOM", strUOM)
                    SQL.AddParam("@UOMQTY", strUOMQTY)
                    SQL.AddParam("@UnitPrice", CDec(strUnitPrice))
                    SQL.AddParam("@VATInclusive", IIf(strVATable = "0" Or strVATable.ToLower = "false", "0", "1"))
                    SQL.AddParam("@VAT", IIf(strVATable = "0" Or strVATable.ToLower = "false", "0", "1"))
                    SQL.AddParam("@Status", "Active")
                    SQL.ExecNonQuery(query)
                Next

                'tblItem_Barcode
                For Each row As DataGridViewRow In dgvItemConversion.Rows
                    Dim strItemCode As String = row.Cells(0).Value
                    Dim strUOMFrom As String = row.Cells(1).Value
                    Dim strQTY As String = row.Cells(2).Value
                    Dim strUOMTo As String = row.Cells(3).Value

                    Dim query As String = "  INSERT INTO " &
                                        "  tblUOM_Group(GroupCode, UnitCode, Manual, WhoCreated) " &
                                        "  VALUES(@GroupCode, @UnitCode, @Manual, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@GroupCode", strItemCode)
                    SQL.AddParam("@UnitCode", strUOMFrom)
                    SQL.AddParam("@Manual", True)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(query)

                    If strQTY <> "" And strUOMTo <> "" Then
                        Dim query1 As String = " INSERT INTO " &
                                       " tblUOM_GroupDetails(GroupCode, UnitCodeFrom, QTY, UnitCodeTo, WhoCreated) " &
                                       " VALUES(@GroupCode, @UnitCodeFrom, @QTY, @UnitCodeTo, @WhoCreated)"
                        SQL.FlushParams()
                        SQL.AddParam("@GroupCode", strItemCode)
                        SQL.AddParam("@UnitCodeFrom", strUOMFrom)
                        SQL.AddParam("@QTY", CDec(strQTY))
                        SQL.AddParam("@UnitCodeTo", strUOMTo)
                        SQL.AddParam("@WhoCreated", UserID)
                        SQL.ExecNonQuery(query1)
                    End If
                Next


                MsgBox("Successfully Saved.", MsgBoxStyle.Information, "Save")
                tsbSave.Enabled = True
            End If
        Else
            MsgBox("Please check highlighted rows.")
        End If
    End Sub

    Private Sub bgwUpload_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwUpload.RunWorkerCompleted
        tsbOpen.Text = "Upload"
        If dgvItemMaster.Rows(dgvItemMaster.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow Then
            tsbSave.Enabled = False
        Else
            tsbSave.Enabled = True
        End If
    End Sub

    Private Sub tsbDownload_Click(sender As Object, e As EventArgs) Handles tsbDownload.Click
        Dim fd As New SaveFileDialog

        fd.Filter = "Excel Files|*.xlsx"
        fd.InitialDirectory = "Desktop"
        fd.ShowDialog()
        If fd.FileName <> "" Then
            Dim App_Path As String = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
            App_Path &= "\Templates\Item Master Template.xlsx"
            If File.Exists(App_Path) Then
                File.Copy(App_Path, fd.FileName)
                Process.Start("explorer.exe", fd.FileName)
            End If
        End If
    End Sub
End Class