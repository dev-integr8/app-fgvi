Public Class frmRR
    Dim TransID, RefID, JETransID As String
    Dim RRNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "RR"
    Dim ColumnPK As String = "RR_No"
    Dim DBTable As String = "tblRR"
    Dim ColumnID As String = "TransID"
    Dim TransAuto As Boolean
    Dim AccntCode, accntAdvance As String
    Dim PO_ID As Integer
    Dim tempWHSE As String = ""
    Dim Adv_Amount As Decimal
    Dim ForApproval As Boolean = False

    ' SETUP VARIABLES
    Dim pendingAPsetup As Boolean
    Dim accntInputVAT, accntAPpending, RR_Book, Inv_VarianceAccnt As String

    Public Overloads Function ShowDialog(ByVal docnumber As String) As Boolean
        TransID = docnumber
        MyBase.ShowDialog()
        Return True
    End Function


    Private Sub frmRR_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            dtpDeliveryDate.Value = Date.Today.Date
            LoadWHSE()
            LoadSetup()
            Loaddatagridcolumn()
            If TransID <> "" Then
                LoadRR(TransID)
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

    Private Sub LoadSetup()
        Dim query As String
        query = " SELECT  ISNULL(AP_SetupPendingAP,0) AS AP_SetupPendingAP, AP_Pending, TAX_IV, RR_Book, Inv_VarianceAccnt  FROM tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            pendingAPsetup = SQL.SQLDR("AP_SetupPendingAP")
            accntAPpending = SQL.SQLDR("AP_Pending").ToString
            accntInputVAT = SQL.SQLDR("TAX_IV").ToString
            RR_Book = SQL.SQLDR("RR_Book").ToString
            Inv_VarianceAccnt = SQL.SQLDR("Inv_VarianceAccnt").ToString
        End If
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        txtVCEName.Enabled = Value
        btnSearchVCE.Enabled = Value
        dgvItemList.AllowUserToAddRows = Value
        dgvItemList.AllowUserToDeleteRows = Value
        dgvItemList.ReadOnly = Not Value
        cbWHSE.Enabled = Value
        dgvItemList.Columns(chPOQTY.Index).ReadOnly = True
        If Value = True Then
            dgvItemList.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemList.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        dtpDeliveryDate.Enabled = Value
        cbPurchaseType.Enabled = Value
        txtPORef.Enabled = Value
        txtDRRef.Enabled = Value
        chkFixedAsset.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub


    Private Sub LoadRR(ByVal ID As String)
        Dim query, Currency As String
        query = " SELECT   TransID, RR_No, VCECode, DateRR, DateDeliver, Remarks, tblRR.Status, PurchaseType, Currency, " & _
                "         CASE WHEN tblWarehouse.Description IS NOT NULL " & _
                "              THEN tblRR.WHSE + ' | ' + tblWarehouse.Description " & _
                "              ELSE 'Multiple Warehouse' " & _
                "         END AS WHSE, " & _
                "          WHSE, SI_Ref, SI_Date, DR_Ref, PO_Ref, ISNULL(isFixedAsset, 0) AS isFixedAsset " & _
                " FROM     tblRR LEFT JOIN tblWarehouse " & _
                " ON       tblRR.WHSE = tblWarehouse.Code " & _
                " AND      tblWarehouse.Status ='Active' " & _
                " WHERE    TransId = '" & ID & "' " & _
                " ORDER BY TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            RRNo = SQL.SQLDR("RR_No").ToString
            txtTransNum.Text = RRNo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            dtpDocDate.Text = SQL.SQLDR("DateRR").ToString
            dtpDeliveryDate.Value = IIf(IsDate(SQL.SQLDR("DateDeliver")), SQL.SQLDR("DateDeliver"), Date.Today)
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            txtDRRef.Text = SQL.SQLDR("DR_Ref").ToString
            PO_ID = SQL.SQLDR("PO_Ref").ToString
            Currency = SQL.SQLDR("Currency").ToString
            cbWHSE.SelectedItem = SQL.SQLDR("WHSE").ToString
            chkFixedAsset.Checked = SQL.SQLDR("isFixedAsset").ToString
            If IsDBNull(SQL.SQLDR("PurchaseType")) Then
                cbPurchaseType.SelectedIndex = -1
            Else
                cbPurchaseType.SelectedItem = SQL.SQLDR("PurchaseType").ToString
            End If
            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            txtPORef.Text = LoadPONo(PO_ID)

            cbCurrency.Items.Clear()
            For Each item In LoadVCECurrency(txtVCECode.Text)
                cbCurrency.Items.Add(item)
            Next
            If cbCurrency.Items.Count = 0 Then
                cbCurrency.Items.Add(BaseCurrency)
            End If
            cbCurrency.SelectedItem = Currency

            LoadRRDetails(TransID)
            LoadEntry(TransID)
            If txtPORef.Text <> "" Then
                dgvItemList.Columns(chPOQTY.Index).Visible = True
            Else
                dgvItemList.Columns(chPOQTY.Index).Visible = False
            End If
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

            If Inv_ComputationMethod <> "SC" Then
                If dtpDocDate.Value < GetMaxInventoryDate() Then
                    tsbEdit.Enabled = False
                    tsbCancel.Enabled = False
                Else
                    If GetLast_InventoryOUT(TransID, ModuleID) Then
                        dtpDocDate.MinDate = GetMaxInventoryDate()
                    Else
                        tsbEdit.Enabled = False
                        tsbCancel.Enabled = False
                    End If
                End If
            End If
            If dtpDocDate.Value <= GetMaxPEC() Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            End If

            disableEvent = True
            EnableControl(False)
            disableEvent = False
        Else
            ClearText()
        End If
    End Sub

    Private Function LoadPONo(PO_ID As Integer) As String
        Dim query As String
        query = " SELECT PO_No FROM tblPO WHERE TransID = '" & PO_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("PO_No").ToString
        Else
            Return ""
        End If
    End Function

    Private Sub LoadRRDetails(ByVal TransID As String)
        Dim ctr As Integer = 0
        Dim query As String
        query = " SELECT tblRR_Details.ItemCode, tblRR_Details.Description, tblRR_Details.UOM, " & _
                " ISNULL(tblRR_Details.QTY,0) AS POQTY, tblRR_Details.QTY  AS RRQTY, tblRR_Details.WHSE, ISNULL(tblRR_Details.UnitCost,0) AS UnitPrice,  " & _
                " tblRR_Details.GrossAmount,  tblRR_Details.VATAmount,  tblRR_Details.DiscountRate,  tblRR_Details.Discount,  tblRR_Details.NetAmount,  tblRR_Details.VATable,  " & _
                " tblRR_Details.VATInc,  tblRR_Details.AccntCode, VATType, ISNULL(StandardCost,0) AS StandardCost, ISNULL(SerialNo,'') AS SerialNo, ISNULL(LotNo,'') AS LotNo, " & _
                " ISNULL(DateExpired,'') AS DateExpired, ISNULL(tblRR_Details.Size,'') AS Size, ISNULL(tblRR_Details.Color,'') AS Color " & _
                " FROM tblRR INNER JOIN tblRR_Details " & _
                " ON tblRR.TransID = tblRR_Details.TransID " & _
                " WHERE tblRR_Details.TransId = " & TransID & " " & _
                " ORDER BY  tblRR_Details.LineNum "
        disableEvent = True
        dgvItemList.Rows.Clear()
        disableEvent = False
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvItemList.Rows.Add(row(0).ToString, "", row(1).ToString, row(2).ToString, row(16).ToString, _
                                                row(3).ToString, row(4).ToString, row(5).ToString, row(6).ToString, row(7).ToString, _
                                                row(10).ToString, row(8).ToString, row(9).ToString, row(11).ToString, row(12).ToString, _
                                                row(13).ToString, row(14).ToString, row(15).ToString, CDate(row(19)).ToString, _
                                                row(18).ToString, row(17).ToString, row(20).ToString, row(21).ToString)
                LoadWHSE(ctr)
                LoadUOM(row(0).ToString, ctr)
                LoadColor(row(0).ToString, ctr)
                LoadSize(row(0).ToString, ctr)
                ctr += 1
            Next
            LoadBarCode()
            ComputeTotal()
        End If

    End Sub

    Private Sub LoadEntry(ByVal RRNo As Integer)
        Dim query As String
        If RR_Book = "Inventory" Then
            query = " SELECT ID, JE_No, View_GL_Transaction.BranchCode, View_GL_Transaction.AccntCode, AccountTitle, View_GL_Transaction.VCECode, View_GL_Transaction.VCEName, Debit, Credit, Particulars, RefNo , VATType  " & _
                    " FROM   View_GL_Transaction INNER JOIN tblCOA_Master " & _
                    " ON     View_GL_Transaction.AccntCode = tblCOA_Master.AccountCode " & _
                    " WHERE JE_No = (SELECT  JE_No FROM tblJE_Header WHERE RefType = 'RR' AND RefTransID = " & RRNo & ") " & _
                    " ORDER BY LineNumber "
        Else
            query = " SELECT ID, JE_No, View_GL_Transaction.BranchCode, View_GL_Transaction.AccntCode, AccountTitle, View_GL_Transaction.VCECode, View_GL_Transaction.VCEName, Debit, Credit, Particulars, RefNo , VATType  " & _
                    " FROM   View_GL_Transaction INNER JOIN tblCOA_Master " & _
                    " ON     View_GL_Transaction.AccntCode = tblCOA_Master.AccountCode " & _
                    " WHERE RefType ='PJ' AND RefTransID IN (SELECT TransID FROM tblPJ WHERE Reftype = 'RR' AND  RefID = '" & TransID & "') " & _
                    " ORDER BY LineNumber "
        End If
        SQL.ReadQuery(query)
        lvAccount.Items.Clear()
        While SQL.SQLDR.Read
            JETransID = SQL.SQLDR("JE_No")
            lvAccount.Items.Add(SQL.SQLDR("AccntCode").ToString)
            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(SQL.SQLDR("AccountTitle").ToString)
            If SQL.SQLDR("Debit").ToString = 0 Then lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("") Else lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(SQL.SQLDR("Debit")).ToString("N2"))
            If SQL.SQLDR("Credit").ToString = 0 Then lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("") Else lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(SQL.SQLDR("Credit")).ToString("N2"))
            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(SQL.SQLDR("RefNo").ToString)
            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(SQL.SQLDR("VATType").ToString)

        End While

        TotalDBCR()
    End Sub

    Private Sub txtVCEName_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.txtFilter.Text = txtVCEName.Text
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            txtVCEName.Text = f.VCEName
            LoadCurrency()
            f.Dispose()
        End If
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

    Private Sub ClearText()
        txtTransNum.Clear()
        txtVCECode.Clear()
        txtVCEName.Clear()
        dgvItemList.Rows.Clear()
        lvAccount.Items.Clear()
        dgvGroup.Rows.Clear()
        txtRemarks.Clear()
        txtPORef.Clear()
        txtDRRef.Clear()
        chkFixedAsset.Checked = False
        cbPurchaseType.SelectedItem = "Goods (Stock)"
        txtStatus.Text = "Open"
        dtpDeliveryDate.Value = Date.Today.Date
        dtpDocDate.MinDate = GetMaxPEC().AddDays(1)
        If Inv_ComputationMethod <> "SC" Then
            dtpDocDate.MinDate = GetMaxInventoryDate()
        End If
        dtpDocDate.Value = Date.Today.Date
        cbCurrency.Items.Clear()
        txtGross.Text = "0.00"
        txtNet.Text = "0.00"
        txtDiscount.Text = "0.00"
        txtVAT.Text = "0.00"
        txtDiscountRate.Text = ""
        Loaddatagridcolumn()
    End Sub

    Private Sub Loaddatagridcolumn()
        If Inv_ComputationMethod = "SC" Then
            dgvItemList.Columns(chStandardCost.Index).Visible = True
            dgvItemList.Columns(chStandardCost.Index).ReadOnly = True
        Else
            dgvItemList.Columns(chStandardCost.Index).Visible = False
            dgvItemList.Columns(chStandardCost.Index).ReadOnly = False

        End If
    End Sub

    Private Sub dgvProducts_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellEndEdit
        Try
            Dim itemCode As String
            Dim rowIndex As Integer = dgvItemList.CurrentCell.RowIndex
            Dim colindex As Integer = dgvItemList.CurrentCell.ColumnIndex
            Select Case colindex
                Case chItemCode.Index
                    If dgvItemList.Item(chItemCode.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemList.Item(chItemCode.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("ItemListPO", itemCode, "", chkFixedAsset.Checked, "ItemCode")
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItem(itemCode)
                        End If
                        dgvItemList.Rows.RemoveAt(e.RowIndex)
                        f.Dispose()
                        GenerateEntry()
                    End If

                Case chBarCode.Index
                    If dgvItemList.Item(chBarCode.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemList.Item(chBarCode.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("ItemListPO", itemCode, "", chkFixedAsset.Checked, "Barcode")
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItem(itemCode)
                        End If
                        dgvItemList.Rows.RemoveAt(e.RowIndex)
                        f.Dispose()
                        GenerateEntry()
                    End If
                Case chItemDesc.Index
                    If dgvItemList.Item(chItemDesc.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemList.Item(chItemDesc.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("ItemListPO", itemCode, "", chkFixedAsset.Checked, "ItemName")
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItem(itemCode)
                        End If
                        dgvItemList.Rows.RemoveAt(e.RowIndex)
                        f.Dispose()
                        GenerateEntry()
                    End If
                Case chPOQTY.Index
                    GenerateEntry()
                Case chRRQTY.Index
                    If dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value = "" OrElse IsNothing(dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value) Then
                        dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value = 0
                    ElseIf IsNumeric(dgvItemList.Item(chUnitCost.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemList.Item(chRRQTY.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex)
                        ComputeTotal()
                        GenerateEntry()
                        dgvItemList.Item(chUnitCost.Index, e.RowIndex).Value = CDec(dgvItemList.Item(chUnitCost.Index, e.RowIndex).Value).ToString("N2")
                    End If
                Case chUnitCost.Index
                    If dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value = "" OrElse IsNothing(dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value) Then
                        dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value = 0
                    ElseIf IsNumeric(dgvItemList.Item(chUnitCost.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemList.Item(chRRQTY.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex)
                        ComputeTotal()
                        GenerateEntry()
                        dgvItemList.Item(chUnitCost.Index, e.RowIndex).Value = CDec(dgvItemList.Item(chUnitCost.Index, e.RowIndex).Value).ToString("N2")
                    End If
                Case dgcAccountCode.Index
                    Dim f As New frmCOA_Search
                    f.ShowDialog("AccntTitle", dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value.ToString)
                    dgvItemList.Item(dgcAccountCode.Index, e.RowIndex).Value = f.accountcode
                    If IsNothing(f.accountcode) Then
                        dgvItemList.Item(dgcAccountCode.Index, e.RowIndex).Value = ""
                    Else
                        dgvItemList.Item(dgcAccountCode.Index, e.RowIndex).Value = f.accountcode
                    End If
                    f.Dispose()
                    GenerateEntry()

                Case chDateExpired.Index
                    If IsDate(dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value) Then
                        dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value = CDate(dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value)
                    End If
                    LoadPeriod()

            End Select
            dgcNet.ReadOnly = True
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadPeriod()
        Dim minPeriod, maxPeriod, tempDate As Date
        For Each row As DataGridViewRow In dgvItemList.Rows
            If IsDate(row.Cells(chDateExpired.Index).Value) Then
                tempDate = row.Cells(chDateExpired.Index).Value
                If minPeriod = #12:00:00 AM# OrElse tempDate <= minPeriod Then
                    minPeriod = tempDate
                End If
                If maxPeriod = #12:00:00 AM# OrElse tempDate >= maxPeriod Then
                    maxPeriod = tempDate
                End If
            End If
        Next
    End Sub

    'Private Sub Recompute(ByVal RowID As Integer, ByVal ColID As Integer)
    '    Dim gross, VAT, discount, net, baseVAT As Decimal
    '    If RowID <> -1 Then
    '        ' GET GROSS AMOUNT (VAT INCLUSIVE)
    '        If IsNumeric(dgvItemList.Item(chUnitCost.Index, RowID).Value) AndAlso IsNumeric(dgvItemList.Item(chRRQTY.Index, RowID).Value) AndAlso dgvItemList.Columns(chRRQTY.Index).Visible = True Then
    '            gross = CDec(dgvItemList.Item(chRRQTY.Index, RowID).Value) * CDec(dgvItemList.Item(chUnitCost.Index, RowID).Value)

    '        ElseIf IsNumeric(dgvItemList.Item(chGross.Index, RowID).Value) Then
    '            gross = CDec(dgvItemList.Item(chGross.Index, RowID).Value)
    '        Else
    '            gross = 0
    '        End If
    '        baseVAT = gross
    '        ' COMPUTE VAT IF VATABLE
    '        If ColID = dgcVAT1.Index Then
    '            If dgvItemList.Item(dgcVAT1.Index, RowID).Value = True Then
    '                dgvItemList.Item(dgcVAT1.Index, RowID).Value = False

    '                dgvItemList.Item(dgcVATInc.Index, RowID).Value = False
    '                VAT = 0
    '                dgvItemList.Item(dgcVATInc.Index, RowID).ReadOnly = True
    '            Else
    '                dgvItemList.Item(dgcVAT1.Index, RowID).Value = True

    '                dgvItemList.Item(dgcVATInc.Index, RowID).ReadOnly = False
    '                If dgvItemList.Item(dgcVATInc.Index, RowID).Value = False Then
    '                    VAT = CDec(baseVAT * 0.12).ToString("N2")
    '                Else
    '                    baseVAT = (gross / 1.12)
    '                    VAT = CDec(baseVAT * 0.12).ToString("N2")
    '                End If

    '            End If
    '        ElseIf ColID = dgcVATInc.Index Then
    '            If dgvItemList.Item(dgcVAT1.Index, RowID).Value = False Then
    '                VAT = 0
    '            Else
    '                If dgvItemList.Item(dgcVATInc.Index, RowID).Value = True Then
    '                    dgvItemList.Item(dgcVATInc.Index, RowID).Value = False
    '                    VAT = CDec(baseVAT * 0.12).ToString("N2")
    '                Else
    '                    dgvItemList.Item(dgcVATInc.Index, RowID).Value = True
    '                    baseVAT = (gross / 1.12)
    '                    VAT = CDec(baseVAT * 0.12).ToString("N2")
    '                End If

    '            End If
    '        Else
    '            If dgvItemList.Item(dgcVAT1.Index, RowID).Value = False Then
    '                VAT = 0
    '                dgvItemList.Item(dgcVATInc.Index, RowID).ReadOnly = True
    '            Else
    '                dgvItemList.Item(dgcVATInc.Index, RowID).ReadOnly = False
    '                If dgvItemList.Item(dgcVATInc.Index, RowID).Value = True Then ' IF VAT INCLUSIVE, BASE AMOUNT WILL BE GROSS / 1.12
    '                    baseVAT = (gross / 1.12)
    '                End If
    '                VAT = CDec(baseVAT * 0.12).ToString("N2")
    '            End If
    '        End If


    '        ' COMPUTE DISCOUNT
    '        If IsNumeric(dgvItemList.Item(dgcDiscountRate.Index, RowID).Value) Then
    '            discount = CDec(baseVAT * (CDec(dgvItemList.Item(dgcDiscountRate.Index, RowID).Value) / 100.0)).ToString("N2")
    '        ElseIf IsNumeric(dgvItemList.Item(dgcDiscountAmt.Index, RowID).Value) Then
    '            discount = CDec(dgvItemList.Item(dgcDiscountAmt.Index, RowID).Value)
    '        Else
    '            discount = 0
    '        End If

    '        net = baseVAT - discount + VAT
    '        dgvItemList.Item(chGross.Index, RowID).Value = Format(gross, "#,###,###,###.00").ToString()
    '        dgvItemList.Item(dgcDiscountAmt.Index, RowID).Value = Format(discount, "#,###,###,###.00").ToString()
    '        dgvItemList.Item(dgcVATamt.Index, RowID).Value = Format(VAT, "#,###,###,###.00").ToString()
    '        dgvItemList.Item(dgcNet.Index, RowID).Value = Format(net, "#,###,###,###.00").ToString()
    '        ComputeTotal()
    '    End If
    'End Sub

    Private Sub Recompute(ByVal RowID As Integer, ByVal ColID As Integer)
        Dim gross, VAT, discount, net, baseVAT As Decimal
        If RowID <> -1 Then
            ' GET GROSS AMOUNT (VAT INCLUSIVE)
            If IsNumeric(dgvItemList.Item(chUnitCost.Index, RowID).Value) AndAlso IsNumeric(dgvItemList.Item(chRRQTY.Index, RowID).Value) AndAlso dgvItemList.Columns(chRRQTY.Index).Visible = True Then
                gross = CDec(dgvItemList.Item(chRRQTY.Index, RowID).Value) * CDec(dgvItemList.Item(chUnitCost.Index, RowID).Value)

            ElseIf IsNumeric(dgvItemList.Item(chGross.Index, RowID).Value) Then
                gross = CDec(dgvItemList.Item(chGross.Index, RowID).Value)
            Else
                gross = 0
            End If
            baseVAT = gross


            ' COMPUTE DISCOUNT
            If IsNumeric(dgvItemList.Item(dgcDiscountRate.Index, RowID).Value) Then
                discount = CDec(gross * (CDec(dgvItemList.Item(dgcDiscountRate.Index, RowID).Value) / 100.0)).ToString("N2")
            ElseIf IsNumeric(dgvItemList.Item(dgcDiscountAmt.Index, RowID).Value) Then
                discount = CDec(dgvItemList.Item(dgcDiscountAmt.Index, RowID).Value)
            Else
                discount = 0
            End If

            ' COMPUTE VAT IF VATABLE
            If ColID = dgcVAT1.Index Then
                If dgvItemList.Item(dgcVAT1.Index, RowID).Value = True Then
                    dgvItemList.Item(dgcVAT1.Index, RowID).Value = False
                    dgvItemList.Item(dgcVATInc.Index, RowID).Value = False
                    VAT = 0
                    dgvItemList.Item(dgcVATInc.Index, RowID).ReadOnly = True
                Else
                    dgvItemList.Item(dgcVAT1.Index, RowID).Value = True
                    dgvItemList.Item(dgcVATInc.Index, RowID).ReadOnly = False
                    If dgvItemList.Item(dgcVATInc.Index, RowID).Value = False Then
                        VAT = CDec((gross - discount) * 0.12).ToString("N2")
                    Else
                        VAT = (gross - discount) / 1.12 * 0.12
                    End If

                End If
            ElseIf ColID = dgcVATInc.Index Then
                If dgvItemList.Item(dgcVAT1.Index, RowID).Value = False Then
                    VAT = 0
                Else
                    If dgvItemList.Item(dgcVATInc.Index, RowID).Value = True Then
                        dgvItemList.Item(dgcVATInc.Index, RowID).Value = False
                        VAT = CDec((gross - discount) * 0.12).ToString("N2")
                    Else
                        dgvItemList.Item(dgcVATInc.Index, RowID).Value = True
                        VAT = (gross - discount) / 1.12 * 0.12
                    End If

                End If
            Else
                If dgvItemList.Item(dgcVAT1.Index, RowID).Value = False Then
                    VAT = 0
                    dgvItemList.Item(dgcVATInc.Index, RowID).ReadOnly = True
                Else
                    dgvItemList.Item(dgcVATInc.Index, RowID).ReadOnly = False
                    If dgvItemList.Item(dgcVATInc.Index, RowID).Value = True Then ' IF VAT INCLUSIVE, BASE AMOUNT WILL BE GROSS / 1.12

                        VAT = (gross - discount) / 1.12 * 0.12
                    Else
                        VAT = CDec((gross - discount) * 0.12).ToString("N2")
                    End If
                End If
            End If


            If dgvItemList.Item(dgcVATInc.Index, RowID).Value = False Then

                net = gross - discount + VAT
            Else
                net = gross - discount
            End If

            dgvItemList.Item(chGross.Index, RowID).Value = Format(gross, "#,###,###,###.00").ToString()
            dgvItemList.Item(dgcDiscountAmt.Index, RowID).Value = Format(discount, "#,###,###,###.00").ToString()
            dgvItemList.Item(dgcVATamt.Index, RowID).Value = Format(VAT, "#,###,###,###.00").ToString()
            dgvItemList.Item(dgcNet.Index, RowID).Value = Format(net, "#,###,###,###.00").ToString()
            ComputeTotal()
        End If
    End Sub

    Private Sub ComputeTotal()
        Dim b As Decimal = 0
        Dim a As Decimal = 0
        Dim c As Decimal = 0
        Dim d As Decimal = 0
        ' COMPUTE GROSS
        For i As Integer = 0 To dgvItemList.Rows.Count - 1
            If Val(dgvItemList.Item(chGross.Index, i).Value) <> 0 Then
                If IsNumeric(dgvItemList.Item(chGross.Index, i).Value) Then
                    a = a + Double.Parse(dgvItemList.Item(chGross.Index, i).Value).ToString
                End If
            End If
        Next
        txtGross.Text = a.ToString("N2")

        ' COMPUTE DISCOUNT
        For i As Integer = 0 To dgvItemList.Rows.Count - 1
            If Val(dgvItemList.Item(dgcDiscountAmt.Index, i).Value) <> 0 Then
                If IsNumeric(dgvItemList.Item(dgcDiscountAmt.Index, i).Value) Then
                    b = b + Double.Parse(dgvItemList.Item(dgcDiscountAmt.Index, i).Value)
                End If
            End If
        Next
        txtDiscount.Text = b.ToString("N2")


        ' COMPUTE VAT
        For i As Integer = 0 To dgvItemList.Rows.Count - 1
            If Val(dgvItemList.Item(dgcVATamt.Index, i).Value) <> 0 Then
                If IsNumeric(dgvItemList.Item(dgcVATamt.Index, i).Value) Then
                    c = c + Double.Parse(dgvItemList.Item(dgcVATamt.Index, i).Value).ToString
                End If
            End If
        Next
        txtVAT.Text = c.ToString("N2")

        ' COMPUTE NET
        For i As Integer = 0 To dgvItemList.Rows.Count - 1
            If Val(dgvItemList.Item(dgcNet.Index, i).Value) <> 0 Then
                If IsNumeric(dgvItemList.Item(dgcNet.Index, i).Value) Then
                    d = d + Double.Parse(dgvItemList.Item(dgcNet.Index, i).Value).ToString
                End If
            End If
        Next
        txtNet.Text = d.ToString("N2")

    End Sub

    Private Sub GenerateEntry()
        Dim vat, pendingAP, amount, clearingAmount, InvAmount As Decimal
        Dim accountExist As Boolean = False
        vat = 0
        pendingAP = 0
        amount = 0
        If pendingAPsetup Then
            If Inv_ComputationMethod = "SC" Then
                ' Variance Entry
                lvAccount.Items.Clear()
                For Each rowitem As DataGridViewRow In dgvItemList.Rows
                    If IsNumeric(rowitem.Cells(dgcNet.Index).Value) Then
                        If IfFixedAssetItem(rowitem.Cells(chItemCode.Index).Value) Then
                            ' Fixed Asset Entry
                            Dim query As String
                            query = " SELECT AD_FixedAsset, AccountTitle " & _
                                      " FROM   tblItem_Master INNER JOIN tblCOA_Master " & _
                                      " ON     tblItem_Master.AD_FixedAsset = tblCOA_Master.AccountCode " & _
                                      " WHERE  ItemCode ='" & rowitem.Cells(chItemCode.Index).Value & "' "
                            SQL.ReadQuery(query)
                            If SQL.SQLDR.Read() Then
                                pendingAP += rowitem.Cells(dgcNet.Index).Value
                                vat += rowitem.Cells(dgcVATamt.Index).Value
                                amount = rowitem.Cells(dgcNet.Index).Value - rowitem.Cells(dgcVATamt.Index).Value

                                If accountExist = False Then
                                    lvAccount.Items.Add(SQL.SQLDR("AD_FixedAsset").ToString)
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(SQL.SQLDR("AD_FixedAsset").ToString))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(amount.ToString("N2"))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(rowitem.Cells(chRRQTY.Index).Value)
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(rowitem.Cells(chUnitCost.Index).Value)
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(rowitem.Cells(chItemCode.Index).Value)
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(IIf(rowitem.Cells(chVATType.Index).Value = Nothing, "", rowitem.Cells(chVATType.Index).Value))
                                End If
                            End If
                        Else
                            pendingAP += rowitem.Cells(dgcNet.Index).Value
                            vat += rowitem.Cells(dgcVATamt.Index).Value
                            amount = rowitem.Cells(dgcNet.Index).Value - rowitem.Cells(dgcVATamt.Index).Value
                            InvAmount = rowitem.Cells(chStandardCost.Index).Value * rowitem.Cells(chQTY.Index).Value
                            clearingAmount += rowitem.Cells(chStandardCost.Index).Value * rowitem.Cells(chQTY.Index).Value
                            If IsNumeric(rowitem.Cells(dgcNet.Index).Value) AndAlso IfInventoriableItem(rowitem.Cells(chItemCode.Index).Value) Then
                                If accountExist = False Then
                                    'lvAccount.Items.Add(Inv_VarianceAccnt)
                                    'lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(Inv_VarianceAccnt))
                                    'lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(amount.ToString("N2"))
                                    'lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    'lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    'lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(rowitem.Cells(chRRQTY.Index).Value)
                                    'lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(rowitem.Cells(chUnitCost.Index).Value)
                                    'lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(rowitem.Cells(chItemCode.Index).Value)
                                    'lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(IIf(rowitem.Cells(chVATType.Index).Value = Nothing, "", rowitem.Cells(chVATType.Index).Value))


                                    lvAccount.Items.Add(GetItem_InvAccnt(rowitem.Cells(chItemCode.Index).Value))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(GetItem_InvAccnt(rowitem.Cells(chItemCode.Index).Value)))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(InvAmount.ToString("N2"))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(rowitem.Cells(chRRQTY.Index).Value)
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(rowitem.Cells(chUnitCost.Index).Value)
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(rowitem.Cells(chItemCode.Index).Value)
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(IIf(rowitem.Cells(chVATType.Index).Value = Nothing, "", rowitem.Cells(chVATType.Index).Value))

                                End If
                            Else
                                If accountExist = False Then
                                    lvAccount.Items.Add(GetItem_InvAccnt(rowitem.Cells(chItemCode.Index).Value))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(GetItem_InvAccnt(rowitem.Cells(chItemCode.Index).Value)))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(amount.ToString("N2"))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(rowitem.Cells(chRRQTY.Index).Value)
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(rowitem.Cells(chUnitCost.Index).Value)
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(rowitem.Cells(chItemCode.Index).Value)
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(IIf(rowitem.Cells(chVATType.Index).Value = Nothing, "", rowitem.Cells(chVATType.Index).Value))
                                End If

                            End If
                        End If
                    End If
                Next

                ' INPUT VAT ENTRY
                If vat <> 0 Then
                    lvAccount.Items.Add(accntInputVAT)
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(accntInputVAT))
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(vat.ToString("N2"))
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("Input VAT")
                End If

                ' Advances ENTRY
                If Adv_Amount <> 0 Then
                    lvAccount.Items.Add(accntAdvance)
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("     " & GetAccntTitle(accntAdvance))
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(Adv_Amount.ToString("N2"))
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                End If

                ' Variance Entry
                Dim varianceAmount As Decimal = 0
                varianceAmount = clearingAmount - (pendingAP - vat)
                If varianceAmount <> 0 Then
                    lvAccount.Items.Add(Inv_VarianceAccnt)
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("     " & GetAccntTitle(Inv_VarianceAccnt))
                    If varianceAmount < 0 Then
                        varianceAmount = varianceAmount * -1
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(varianceAmount.ToString("N2"))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    Else
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(varianceAmount.ToString("N2"))
                    End If
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                End If

                    ' PENDING AP ENTRY
                    If (pendingAP - Adv_Amount) <> 0 Then
                        lvAccount.Items.Add(accntAPpending)
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("     " & GetAccntTitle(accntAPpending))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add((pendingAP - Adv_Amount).ToString("N2"))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("RR:" & txtTransNum.Text)
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    End If


                Else
                    lvAccount.Items.Clear()
                    For Each row As DataGridViewRow In dgvItemList.Rows
                        accountExist = False
                        If IsNumeric(row.Cells(dgcNet.Index).Value) Then
                            If IfFixedAssetItem(row.Cells(chItemCode.Index).Value) Then
                                Dim query As String
                                query = " SELECT AD_FixedAsset, AccountTitle " & _
                                          " FROM   tblItem_Master INNER JOIN tblCOA_Master " & _
                                          " ON     tblItem_Master.AD_FixedAsset = tblCOA_Master.AccountCode " & _
                                          " WHERE  ItemCode ='" & row.Cells(chItemCode.Index).Value & "' "
                                SQL.ReadQuery(query)
                                If SQL.SQLDR.Read() Then
                                    pendingAP += row.Cells(dgcNet.Index).Value
                                    vat += row.Cells(dgcVATamt.Index).Value
                                    amount = row.Cells(dgcNet.Index).Value - row.Cells(dgcVATamt.Index).Value

                                    If accountExist = False Then
                                        lvAccount.Items.Add(SQL.SQLDR("AD_FixedAsset").ToString)
                                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(SQL.SQLDR("AD_FixedAsset").ToString))
                                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(amount.ToString("N2"))
                                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chRRQTY.Index).Value)
                                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chUnitCost.Index).Value)
                                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chItemCode.Index).Value)
                                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(IIf(row.Cells(chVATType.Index).Value = Nothing, "", row.Cells(chVATType.Index).Value))
                                    End If
                                End If
                            Else
                                pendingAP += row.Cells(dgcNet.Index).Value
                                vat += row.Cells(dgcVATamt.Index).Value
                                amount = row.Cells(dgcNet.Index).Value - row.Cells(dgcVATamt.Index).Value

                                If accountExist = False Then
                                    lvAccount.Items.Add(GetItem_InvAccnt(row.Cells(chItemCode.Index).Value))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(GetItem_InvAccnt(row.Cells(chItemCode.Index).Value)))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(amount.ToString("N2"))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chRRQTY.Index).Value)
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chUnitCost.Index).Value)
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chItemCode.Index).Value)
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(IIf(row.Cells(chVATType.Index).Value = Nothing, "", row.Cells(chVATType.Index).Value))
                                End If
                            End If
                        End If
                    Next
                    ' INPUT VAT ENTRY
                    If vat <> 0 Then
                        lvAccount.Items.Add(accntInputVAT)
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(accntInputVAT))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(vat.ToString("N2"))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("Input VAT")
                    End If

                    ' Advances ENTRY
                    If Adv_Amount <> 0 Then
                        lvAccount.Items.Add(accntAdvance)
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("     " & GetAccntTitle(accntAdvance))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(Adv_Amount.ToString("N2"))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    End If


                    ' PENDING AP ENTRY
                    lvAccount.Items.Add(accntAPpending)
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("     " & GetAccntTitle(accntAPpending))
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(pendingAP.ToString("N2"))
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("RR:" & txtTransNum.Text)
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                End If

                TotalDBCR()
            End If

    End Sub

    Public Sub LoadItem(ByVal ItemCode As String, Optional UOM As String = "", Optional QTY As Integer = 1)
        Try
            Dim query, AccntCode, Size, Color As String
            Dim netPrice, StandardCost, VATAmt As Decimal
            Dim VAT, VATInc As Boolean
            query = " SELECT  ItemCode,  ItemName, PD_UOM,  " & _
                    "         ISNULL(PD_UnitCost,0) AS Net_Price, ISNULL(ID_SC,0) AS ID_SC, WHSE, AD_Inv,  " & _
                    "         ISNULL(VATable,0) AS VATable, ISNULL(PD_VATinc, 0) AS PD_VATinc, Size, Color " & _
                    " FROM    viewItem_Cost " & _
                    " WHERE   ItemCode = @ItemCode "
            SQL.FlushParams()
            SQL.AddParam("@ItemCode", IIf(ItemCode = Nothing, "", ItemCode))
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                If UOM = "" Then
                    UOM = SQL.SQLDR("PD_UOM").ToString
                End If
                AccntCode = SQL.SQLDR("AD_Inv")
                StandardCost = SQL.SQLDR("ID_SC")
                netPrice = SQL.SQLDR("Net_Price")
                VAT = SQL.SQLDR("VATable").ToString
                VATInc = SQL.SQLDR("PD_VATinc").ToString
                If VAT = True Then
                    If VATInc = True Then
                        VATAmt = (netPrice) / 1.12 * 0.12
                    Else
                        VATAmt = (netPrice) * 0.12
                    End If
                Else
                    VATAmt = 0
                End If
                Size = SQL.SQLDR("Size").ToString
                Color = SQL.SQLDR("Color").ToString
                dgvItemList.Rows.Add(New String() {SQL.SQLDR("ItemCode").ToString, "", _
                                              SQL.SQLDR("ItemName").ToString, _
                                             UOM, _
                                             StandardCost, _
                                              QTY, _
                                              QTY, _
                                              GetWHSEDesc(SQL.SQLDR("WHSE").ToString), _
                                              Format(netPrice, "#,###,###,###.00").ToString,
                                              Format(netPrice * QTY, "#,###,###,###.00").ToString,
                                              Format(0, "#,###,###,###.00").ToString,
                                              Format(0, "#,###,###,###.00").ToString,
                                              Format(VATAmt, "#,###,###,###.00").ToString,
                                              Format(netPrice * QTY, "#,###,###,###.00").ToString,
                                            SQL.SQLDR("VATable").ToString, _
                                            SQL.SQLDR("PD_VATinc").ToString, _
                                                  AccntCode, "", "", "", "", Size, Color})
                LoadWHSE(dgvItemList.Rows.Count - 2)
                LoadUOM(ItemCode, dgvItemList.Rows.Count - 2)
                LoadColor(ItemCode, dgvItemList.Rows.Count - 2)
                LoadSize(ItemCode, dgvItemList.Rows.Count - 2)
            End If
            LoadBarCode()
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub LoadColor(ItemCode As String, ByVal SelectedIndex As Integer)
        Try
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemList.Item(chColor.Index, SelectedIndex)
            dgvCB.Items.Clear()
            ' ADD ALL WHSEc
            Dim query As String

            query = " SELECT DISTINCT Color FROM tblitem_SIzecolor " & _
                    " WHERE ItemCode ='" & ItemCode & "' "
            SQL.ReadQuery(query)
            dgvCB.Items.Clear()
            While SQL.SQLDR.Read
                dgvCB.Items.Add(SQL.SQLDR("Color").ToString)
            End While
            dgvCB.Items.Add("")
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub LoadSize(ItemCode As String, ByVal SelectedIndex As Integer)
        Try
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemList.Item(chSize.Index, SelectedIndex)
            dgvCB.Items.Clear()
            ' ADD ALL WHSEc
            Dim query As String

            query = " SELECT DISTINCT Size FROM tblitem_SIzecolor " & _
                    " WHERE ItemCode ='" & ItemCode & "' "
            SQL.ReadQuery(query)
            dgvCB.Items.Clear()
            While SQL.SQLDR.Read
                dgvCB.Items.Add(SQL.SQLDR("Size").ToString)
            End While
            dgvCB.Items.Add("")
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadUOM(ItemCode As String, ByVal SelectedIndex As Integer)
        Try
            Dim bool As Boolean = True
            Dim strUOM As String = dgvItemList.Item(chUOM.Index, SelectedIndex).Value
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemList.Item(chUOM.Index, SelectedIndex)
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
            SQL.ReadQuery(query)
            dgvCB.Items.Clear()
            While SQL.SQLDR.Read
                If strUOM = SQL.SQLDR("UnitCode").ToString Then
                    bool = False
                End If
                dgvCB.Items.Add(SQL.SQLDR("UnitCode").ToString)
            End While
            dgvCB.Items.Add("")
            If bool = True Then
                dgvCB.Value = ""
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadPO(ByVal PO As String)
        Try
            Dim query, Currency As String
            Dim purchaseType As String = ""
            query = " SELECT TransID, PO_No, tblPO.VCECode, VCEName, DatePO, DateDeliver, Remarks, PurchaseType, Currency,   " & _
                    " ADV.AccntCode  AS ADVAccount, ISNULL(ADV_Amount,0) AS Advances, ISNULL(isFixedAsset,0) AS isFixedAsset" & _
                    " FROM   tblPO INNER JOIN tblVCE_Master " & _
                    " ON     tblPO.VCECode = tblVCE_Master.VCECode " & _
                    "LEFT JOIN " & _
                      " (  SELECT PO_Ref, AccntCode, SUM(ADV_Amount) AS ADV_Amount " & _
                    "  FROM View_ADV_Balance " & _
                    " WHERE Status ='Closed' GROUP BY PO_Ref, AccntCode) AS ADV " & _
                    " ON        tblPO.TransID  = ADV.PO_Ref " & _
                    " WHERE  TransID ='" & PO & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                PO_ID = SQL.SQLDR("TransID")
                txtPORef.Text = SQL.SQLDR("PO_No").ToString
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                txtVCEName.Text = SQL.SQLDR("VCEName").ToString
                purchaseType = SQL.SQLDR("PurchaseType").ToString
                Currency = SQL.SQLDR("Currency").ToString
                Adv_Amount = SQL.SQLDR("Advances").ToString
                accntAdvance = SQL.SQLDR("ADVAccount").ToString
                chkFixedAsset.Checked = SQL.SQLDR("isFixedAsset").ToString
                If IsDBNull(SQL.SQLDR("PurchaseType")) Then
                    cbPurchaseType.SelectedIndex = -1
                Else
                    cbPurchaseType.SelectedItem = SQL.SQLDR("PurchaseType").ToString
                End If
                cbCurrency.Items.Clear()
                For Each item In LoadVCECurrency(txtVCECode.Text)
                    cbCurrency.Items.Add(item)
                Next
                If cbCurrency.Items.Count = 0 Then
                    cbCurrency.Items.Add(BaseCurrency)
                End If
                cbCurrency.SelectedItem = Currency
            End If



            query = " SELECT tblPO_Details.ItemGroup, tblPO_Details.ItemCode, Description, UOM, Unserved AS QTY, viewPO_Unserved.WHSE,  " & _
                    "        ISNULL(UnitPrice,0) AS UnitPrice, GrossAmount, DiscountRate, Discount, VATAmount, NetAmount, VATinc, VATable, AccntCodE, VATType, Size, Color " & _
                    " FROM   tblPO_Details INNER JOIN viewPO_Unserved " & _
                    " ON     tblPO_Details.RecordID = viewPO_Unserved.RecordID " & _
                    " WHERE  tblPO_Details.TransID ='" & PO & "' "
            SQL.GetQuery(query)
            dgvItemList.Columns(chPOQTY.Index).Visible = True
            dgvItemList.Rows.Clear()
            dgvGroup.Rows.Clear()
            Dim ctr As Integer = 0
            Dim ctrGroup As Integer = 0
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                    If row(1).ToString = "" AndAlso purchaseType = "Goods (Stock)" Then
                        dgvGroup.Rows.Add(row(0).ToString, row(1).ToString, row(2).ToString, row(3).ToString, _
                                     CDec(row(4)).ToString("N2"), CDec(row(4)).ToString("N2"), "ADD")
                        LoadItemGroup(row(0).ToString, ctrGroup)
                        LoadUOM(row(0).ToString, ctr)
                        ctrGroup += 1
                    Else
                        dgvItemList.Rows.Add(row(1).ToString, "", row(2).ToString, row(3).ToString, CDec(GetStandardCost(row(1).ToString)),
                                 CDec(row(4)).ToString("N2"), CDec(row(4)).ToString("N2"), GetWHSEDesc(row(5).ToString),
                                   row(6).ToString, row(7).ToString, row(8).ToString, row(9).ToString, row(10).ToString, row(11).ToString, row(12).ToString,
                                  row(13).ToString, row(14).ToString, row(15).ToString, "", "", "", row(16).ToString, row(17).ToString)
                        LoadUOM(row(1).ToString, ctr)
                        LoadColor(row(1).ToString, ctr)
                        LoadSize(row(1).ToString, ctr)
                        LoadWHSE(ctr)
                        ctr += 1
                    End If
                Next
            End If
            If purchaseType = "Goods (Stocks)" Then
                dgvItemList.Columns(dgcWHSE.Index).Visible = True
                dgvItemList.Columns(chPOQTY.Index).ReadOnly = True
                cbWHSE.Enabled = True
                'Else
                'cbWHSE.Enabled = False
                'dgvItemList.Columns(dgcWHSE.Index).Visible = False
            End If
            LoadBarCode()
            ComputeTotal()
            GenerateEntry()
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub TotalDBCR()
        Try
            'debit compute & print in textbox
            Dim a As Double = 0
            For i As Integer = 0 To lvAccount.Items.Count - 1
                If (lvAccount.Items(i).SubItems(chDebit.Index).Text) <> "" Then
                    a = a + Double.Parse((lvAccount.Items(i).SubItems(chDebit.Index).Text))
                End If
            Next
            txtTotalDebit.Text = a.ToString("N2")
            'credit compute & print in textbox
            Dim b As Double = 0
            For i As Integer = 0 To lvAccount.Items.Count - 1
                If (lvAccount.Items(i).SubItems(chCredit.Index).Text) <> "" Then
                    b = b + Double.Parse((lvAccount.Items(i).SubItems(chCredit.Index).Text))
                End If
            Next
            txtTotalCredit.Text = b.ToString("N2")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnSearchVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchVCE.Click
        Dim f As New frmVCE_Search
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        txtVCEName.Text = f.VCEName
        LoadCurrency()
        f.Dispose()
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("RR_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("RR")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadRR(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("RR_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            RRNo = ""
            PO_ID = 0
            LoadWHSE()
            dgvItemList.Columns(chPOQTY.Index).Visible = False
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
            If dgvItemList.Rows.Count > 0 Then
                dgvItemList.Rows(0).Cells(chDateExpired.Index).Value = dtpDocDate.Value
            End If
            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
        End If
    End Sub

    Private Sub LoadItemGroup(ByVal Group As String, Optional ByVal SelectedIndex As Integer = -1)
        Try
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvGroup.Item(dgcSubDesc.Index, SelectedIndex)
            Dim temp As String = dgvCB.Value.ToString
            dgvCB.Value = Nothing
            ' ADD ALL WHSEc
            Dim query As String
            query = " SELECT ItemName FROM tblItem_Master WHERE ItemGroup = @ItemGroup AND Status ='Active' "
            SQL.FlushParams()
            SQL.AddParam("@ItemGroup", Group)
            SQL.ReadQuery(query)
            SQL.FlushParams()
            dgvCB.Items.Clear()
            While SQL.SQLDR.Read
                dgvCB.Items.Add(SQL.SQLDR("ItemName").ToString)
            End While
            dgvCB.Value = temp
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("RR_EDIT") Then
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
            tsbCopy.Enabled = False
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If validateDGV() Then
            If txtVCECode.Text = "" Then
                Msg("Please enter VCECode!", MsgBoxStyle.Exclamation)
            ElseIf TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID(ColumnID, DBTable)
                    RRNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    txtTransNum.Text = RRNo
                    SaveRR()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    RRNo = txtTransNum.Text
                    LoadRR(TransID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    UpdateRR()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    RRNo = txtTransNum.Text
                    LoadRR(TransID)
                End If
            End If
        End If
    End Sub

    Private Function validateDGV() As Boolean
        Dim desc, WHSE As String
        Dim value As Boolean = True
        If cbPurchaseType.SelectedItem = "Goods (Stock)" Then
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chRRQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    If IsNothing(row.Cells(chUOM.Index).Value) Then desc = "" Else desc = row.Cells(chUOM.Index).Value
                    'If IsNothing(row.Cells(chDateExpired.Index).Value) Then desc = "" Else desc = row.Cells(chDateExpired.Index).Value
                    'If IsNothing(row.Cells(chLotNo.Index).Value) Then desc = "" Else desc = row.Cells(chLotNo.Index).Value
                    'If IsNothing(row.Cells(chSerialNo.Index).Value) Then desc = "" Else desc = row.Cells(chSerialNo.Index).Value
                    If cbWHSE.SelectedItem = "Multiple Warehouse" Then
                        If IsNothing(row.Cells(dgcWHSE.Index).Value) Or row.Cells(dgcWHSE.Index).Value = "Multiple Warehouse" Then WHSE = "" Else WHSE = row.Cells(dgcWHSE.Index).Value
                    Else
                        WHSE = "MW"
                    End If
                    If desc = "" Or WHSE = "" Then
                        Msg("There are line entry without Item UOM or Warehouse, please check.", MsgBoxStyle.Exclamation)
                        value = False
                        Exit For

                    End If
                End If
            Next
            Return value
        Else
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chRRQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then

                    If cbWHSE.SelectedItem = "Multiple Warehouse" Then
                        If IsNothing(row.Cells(dgcWHSE.Index).Value) Then WHSE = "" Else WHSE = row.Cells(dgcWHSE.Index).Value
                    Else
                        WHSE = "MW"
                    End If
                    If WHSE = "" Then
                        Msg("There are line entry without Item UOM or Warehouse, please check.", MsgBoxStyle.Exclamation)
                        value = False
                        Exit For

                    End If
                End If
            Next
            Return value
        End If

    End Function

    Private Sub SaveRR()
        Try
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblRR(TransID, RR_No, BranchCode, BusinessCode, DateRR, VCECode, DateDeliver, Remarks,  " & _
                        "           WHSE, SI_Date, SI_Ref, DR_Ref, PO_Ref, WhoCreated, PurchaseType, Currency, isFixedAsset, Status) " & _
                        " VALUES (@TransID, @RR_No, @BranchCode, @BusinessCode, @DateRR, @VCECode, @DateDeliver, @Remarks,  " & _
                        "           @WHSE, @SI_Date, @SI_Ref, @DR_Ref, @PO_Ref, @WhoCreated, @PurchaseType, @Currency, @isFixedAsset, @Status) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            If cbWHSE.SelectedItem = "Multiple Warehouse" Then
                SQL.AddParam("@WHSE", "MW")
            Else
                SQL.AddParam("@WHSE", tempWHSE)
            End If
            SQL.AddParam("@RR_No", RRNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateRR", dtpDocDate.Value.Date)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateDeliver", dtpDeliveryDate.Value.Date)
            SQL.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@SI_Date", dtpDeliveryDate.Value.Date)
            SQL.AddParam("@SI_Ref", "")
            SQL.AddParam("@DR_Ref", txtDRRef.Text)
            SQL.AddParam("@PO_Ref", PO_ID)
            SQL.AddParam("@isFixedAsset", chkFixedAsset.Checked)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.AddParam("@PurchaseType", cbPurchaseType.SelectedItem)
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Active")
            SQL.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            Dim ItemCode, Description, UOM, WHSE, AccntCode, VATType, DateExpired, LotNo, SerialNo, Size, Color As String
            Dim QTY, UnitCost As Decimal
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chRRQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                    Description = IIf(row.Cells(chItemDesc.Index).Value = Nothing, "", row.Cells(chItemDesc.Index).Value)
                    UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                    If Inv_ComputationMethod = "SC" Then
                        UnitCost = IIf(row.Cells(chStandardCost.Index).Value = Nothing, 0, row.Cells(chStandardCost.Index).Value)
                    Else
                        UnitCost = IIf(row.Cells(chUnitCost.Index).Value = Nothing, 0, row.Cells(chUnitCost.Index).Value)
                    End If
                    If IsNumeric(row.Cells(chRRQTY.Index).Value) Then
                        QTY = CDec(row.Cells(chRRQTY.Index).Value)
                    Else
                        QTY = 1
                    End If
                    WHSE = row.Cells(dgcWHSE.Index).Value
                    AccntCode = IIf(row.Cells(dgcAccountCode.Index).Value = Nothing, "", row.Cells(dgcAccountCode.Index).Value)
                    VATType = IIf(row.Cells(chVATType.Index).Value = Nothing, "", row.Cells(chVATType.Index).Value)
                    DateExpired = IIf(row.Cells(chDateExpired.Index).Value = Nothing, "", row.Cells(chDateExpired.Index).Value)
                    LotNo = IIf(row.Cells(chLotNo.Index).Value = Nothing, "", row.Cells(chLotNo.Index).Value)
                    SerialNo = IIf(row.Cells(chSerialNo.Index).Value = Nothing, "", row.Cells(chSerialNo.Index).Value)
                    Size = IIf(row.Cells(chSize.Index).Value = Nothing, "", row.Cells(chSize.Index).Value)
                    Color = IIf(row.Cells(chColor.Index).Value = Nothing, "", row.Cells(chColor.Index).Value)
                    insertSQL = " INSERT INTO " & _
                         " tblRR_Details(TransId, ItemCode, Description, UOM, QTY, WHSE, AccntCode, LineNum, WhoCreated, " & _
                         "  GrossAmount, VATAmount, DiscountRate, Discount, NetAmount, VATable, VATinc, UnitCost, VATType, StandardCost, " & _
                         "  SerialNo, LotNo, DateExpired, Size, Color) " & _
                         " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @WHSE, @AccntCode, @LineNum, @WhoCreated, " & _
                         "  @GrossAmount, @VATAmount, @DiscountRate, @Discount, @NetAmount, @VATable, @VATinc, @UnitCost, @VATType, @StandardCost, " & _
                         "  @SerialNo, @LotNo, @DateExpired, @Size, @Color) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", ItemCode)
                    SQL.AddParam("@Description", Description)
                    SQL.AddParam("@UOM", UOM)
                    SQL.AddParam("@QTY", QTY)
                    If cbWHSE.SelectedItem = "Multiple Warehouse" Then
                        SQL.AddParam("@WHSE", WHSE)
                    Else
                        SQL.AddParam("@WHSE", tempWHSE)
                        WHSE = tempWHSE
                    End If
                    If IsNumeric(row.Cells(chStandardCost.Index).Value) Then SQL.AddParam("@StandardCost", CDec(row.Cells(chStandardCost.Index).Value)) Else SQL.AddParam("@StandardCost", 0)
                    If IsNumeric(row.Cells(chUnitCost.Index).Value) Then SQL.AddParam("@UnitCost", CDec(row.Cells(chUnitCost.Index).Value)) Else SQL.AddParam("@UnitCost", 0)
                    If IsNumeric(row.Cells(chGross.Index).Value) Then SQL.AddParam("@GrossAmount", CDec(row.Cells(chGross.Index).Value)) Else SQL.AddParam("@GrossAmount", 0)
                    If IsNumeric(row.Cells(dgcDiscountRate.Index).Value) Then SQL.AddParam("@DiscountRate", CDec(row.Cells(dgcDiscountRate.Index).Value)) Else SQL.AddParam("@DiscountRate", 0)
                    If IsNumeric(row.Cells(dgcDiscountAmt.Index).Value) Then SQL.AddParam("@Discount", CDec(row.Cells(dgcDiscountAmt.Index).Value)) Else SQL.AddParam("@Discount", 0)
                    If IsNumeric(row.Cells(dgcVATamt.Index).Value) Then SQL.AddParam("@VATAmount", CDec(row.Cells(dgcVATamt.Index).Value)) Else SQL.AddParam("@VATAmount", 0)
                    If IsNumeric(row.Cells(dgcNet.Index).Value) Then SQL.AddParam("@NetAmount", CDec(row.Cells(dgcNet.Index).Value)) Else SQL.AddParam("@NetAmount", 0)
                    If IsNothing(row.Cells(dgcVAT1.Index).Value) Then SQL.AddParam("@VATable", False) Else SQL.AddParam("@VATable", row.Cells(dgcVAT1.Index).Value)
                    If IsNothing(row.Cells(dgcVATInc.Index).Value) Then SQL.AddParam("@VATinc", False) Else SQL.AddParam("@VATinc", row.Cells(dgcVATInc.Index).Value)
                    SQL.AddParam("@AccntCode", AccntCode)
                    SQL.AddParam("@VATType", VATType)
                    SQL.AddParam("@SerialNo", SerialNo)
                    SQL.AddParam("@LotNo", LotNo)
                    SQL.AddParam("@DateExpired", DateExpired)
                    SQL.AddParam("@Size", Size)
                    SQL.AddParam("@Color", Color)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1

                    If cbPurchaseType.SelectedItem = "Goods (Stock)" Then
                        SaveINVTY("IN", ModuleID, "RR", TransID, dtpDocDate.Value.Date, ItemCode, WHSE, QTY, UOM, UnitCost, "Active", SerialNo, LotNo, DateExpired, Size, Color)
                    End If
                End If
            Next

            ComputeWAUC("RR", TransID)

            If RR_Book = "Inventory" Then
                JETransID = GenerateTransID("JE_No", "tblJE_Header")
                insertSQL = " INSERT INTO " & _
                           " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, Currency, WhoCreated, Status) " & _
                           " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @Currency, @WhoCreated, @Status)"
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransID)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "RR")
                SQL.AddParam("@RefTransID", TransID)
                SQL.AddParam("@Book", RR_Book)
                SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@WhoCreated", "")
                If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Saved")
                SQL.ExecNonQuery(insertSQL)

                'JETransID = LoadJE("RR", TransID)

                line = 1
                For Each item As ListViewItem In lvAccount.Items
                    If item.SubItems(chAccountCode.Index).Text <> "" Then
                        insertSQL = " INSERT INTO " & _
                                    " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, BranchCode, VATType) " & _
                                    " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @BranchCode, @VATType)"
                        SQL.FlushParams()
                        SQL.AddParam("@JE_No", JETransID)
                        SQL.AddParam("@AccntCode", item.SubItems(chAccountCode.Index).Text)
                        SQL.AddParam("@VCECode", txtVCECode.Text)
                        If IsNumeric(item.SubItems(chDebit.Index).Text) Then
                            SQL.AddParam("@Debit", CDec(item.SubItems(chDebit.Index).Text))
                        Else
                            SQL.AddParam("@Debit", 0)
                        End If
                        If IsNumeric(item.SubItems(chCredit.Index).Text) Then
                            SQL.AddParam("@Credit", CDec(item.SubItems(chCredit.Index).Text))
                        Else
                            SQL.AddParam("@Credit", 0)
                        End If
                        SQL.AddParam("@Particulars", "")
                        SQL.AddParam("@RefNo", item.SubItems(chRef.Index).Text)
                        If item.SubItems(lvVATType.Index).Text <> "" Then
                            SQL.AddParam("@VATType", item.SubItems(lvVATType.Index).Text)
                        Else
                            SQL.AddParam("@VATType", "")
                        End If
                        SQL.AddParam("@LineNumber", line)
                        SQL.AddParam("@BranchCode", BranchCode)
                        SQL.ExecNonQuery(insertSQL)
                        line += 1
                    End If
                Next
            Else

                Dim PJID As Integer
                PJID = GenerateTransID("TransID", "tblPJ")
                Dim PJNo As String
                PJNo = GenerateTransNum(True, "PJ", "PJ_No", "tblPJ")
                insertSQL = " INSERT INTO " & _
                            " tblPJ (TransID, PJ_No, VCECode, BranchCode, BusinessCode, DatePJ, TotalAmount,  Currency, Exchange_Rate, Remarks, TransAuto, WhoCreated,  SIRef, RefID, RefType, Status) " & _
                            " VALUES(@TransID, @PJ_No, @VCECode, @BranchCode, @BusinessCode, @DatePJ, @TotalAmount, @Currency, @Exchange_Rate, @Remarks, @TransAuto, @WhoCreated,  @SIRef, @RefID, @RefType, @Status)"
                SQL.FlushParams()
                SQL.AddParam("@TransID", PJID)
                SQL.AddParam("@PJ_No", PJNo)
                SQL.AddParam("@VCECode", txtVCECode.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@DatePJ", dtpDocDate.Value.Date)
                SQL.AddParam("@TotalAmount", CDec(txtGross.Text))
                SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                SQL.AddParam("@Exchange_Rate", "0.0000")
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@TransAuto", TransAuto)
                SQL.AddParam("@WhoCreated", UserID)
                SQL.AddParam("@SIRef", txtTransNum.Text)
                SQL.AddParam("@RefID", TransID)
                SQL.AddParam("@Reftype", "RR")
                If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Active")
                SQL.ExecNonQuery(insertSQL)


                JETransID = GenerateTransID("JE_No", "tblJE_Header")
                insertSQL = " INSERT INTO " & _
                           " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, Currency, WhoCreated, Status) " & _
                           " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @Currency, @WhoCreated, @Status)"
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransID)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "PJ")
                SQL.AddParam("@RefTransID", PJID)
                SQL.AddParam("@Book", RR_Book)
                SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@WhoCreated", "")
                If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Saved")
                SQL.ExecNonQuery(insertSQL)

                'JETransID = LoadJE("RR", TransID)

                line = 1
                For Each item As ListViewItem In lvAccount.Items
                    If item.SubItems(chAccountCode.Index).Text <> "" Then
                        insertSQL = " INSERT INTO " & _
                                    " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, BranchCode, VATType) " & _
                                    " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @BranchCode, @VATType)"
                        SQL.FlushParams()
                        SQL.AddParam("@JE_No", JETransID)
                        SQL.AddParam("@AccntCode", item.SubItems(chAccountCode.Index).Text)
                        SQL.AddParam("@VCECode", txtVCECode.Text)
                        If IsNumeric(item.SubItems(chDebit.Index).Text) Then
                            SQL.AddParam("@Debit", CDec(item.SubItems(chDebit.Index).Text))
                        Else
                            SQL.AddParam("@Debit", 0)
                        End If
                        If IsNumeric(item.SubItems(chCredit.Index).Text) Then
                            SQL.AddParam("@Credit", CDec(item.SubItems(chCredit.Index).Text))
                        Else
                            SQL.AddParam("@Credit", 0)
                        End If
                        SQL.AddParam("@Particulars", "")
                        SQL.AddParam("@RefNo", item.SubItems(chRef.Index).Text)
                        If item.SubItems(lvVATType.Index).Text <> "" Then
                            SQL.AddParam("@VATType", item.SubItems(lvVATType.Index).Text)
                        Else
                            SQL.AddParam("@VATType", "")
                        End If
                        SQL.AddParam("@LineNumber", line)
                        SQL.AddParam("@BranchCode", BranchCode)
                        SQL.ExecNonQuery(insertSQL)
                        line += 1
                    End If
                Next
            End If
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "RR_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub UpdateRR()
        Try

            activityStatus = True
            Dim insertSQL, updateSQL, deleteSQL As String
            Dim PJTransID As String
            updateSQL = " UPDATE    tblRR " & _
                        " SET       RR_No = @RR_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, DateRR = @DateRR, " & _
                        "           VCECode = @VCECode, DateDeliver = @DateDeliver, Remarks = @Remarks, SI_Date = @SI_Date, WHSE = @WHSE, PurchaseType = @PurchaseType, " & _
                        "           SI_Ref = @SI_Ref, DR_Ref = @DR_Ref, PO_Ref = @PO_Ref, WhoModified = @WhoModified, DateModified = GETDATE(), Currency = @Currency, isFixedAsset = @isFixedAsset " & _
                        " WHERE     TransID = @TransID  "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            If cbWHSE.SelectedItem = "Multiple Warehouse" Then
                SQL.AddParam("@WHSE", "MW")
            Else
                SQL.AddParam("@WHSE", tempWHSE)
            End If
            SQL.AddParam("@RR_No", RRNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateRR", dtpDocDate.Value.Date)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateDeliver", dtpDeliveryDate.Value.Date)
            SQL.AddParam("@PurchaseType", cbPurchaseType.SelectedItem)
            SQL.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@SI_Date", dtpDeliveryDate.Value.Date)
            SQL.AddParam("@SI_Ref", "")
            SQL.AddParam("@DR_Ref", txtDRRef.Text)
            SQL.AddParam("@isFixedAsset", chkFixedAsset.Checked)
            SQL.AddParam("@PO_Ref", PO_ID)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)

            deleteSQL = " DELETE FROM tblRR_Details WHERE TransID =@TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.ExecNonQuery(deleteSQL)


            DELINVTY(ModuleID, "RR", TransID)

            Dim line As Integer = 1
            Dim ItemCode, Description, UOM, WHSE, AccntCode, VATType, DateExpired, LotNo, SerialNo, Size, Color As String
            Dim QTY, UnitCost As Decimal
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chRRQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                    Description = IIf(row.Cells(chItemDesc.Index).Value = Nothing, "", row.Cells(chItemDesc.Index).Value)
                    UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                    If Inv_ComputationMethod = "SC" Then
                        UnitCost = IIf(row.Cells(chStandardCost.Index).Value = Nothing, 0, row.Cells(chStandardCost.Index).Value)
                    Else
                        UnitCost = IIf(row.Cells(chUnitCost.Index).Value = Nothing, 0, row.Cells(chUnitCost.Index).Value)
                    End If
                    If IsNumeric(row.Cells(chRRQTY.Index).Value) Then
                        QTY = CDec(row.Cells(chRRQTY.Index).Value)
                    Else
                        QTY = 1
                    End If
                    WHSE = row.Cells(dgcWHSE.Index).Value
                    AccntCode = IIf(row.Cells(dgcAccountCode.Index).Value = Nothing, "", row.Cells(dgcAccountCode.Index).Value)
                    VATType = IIf(row.Cells(chVATType.Index).Value = Nothing, "", row.Cells(chVATType.Index).Value)
                    DateExpired = IIf(row.Cells(chDateExpired.Index).Value = Nothing, "", row.Cells(chDateExpired.Index).Value)
                    LotNo = IIf(row.Cells(chLotNo.Index).Value = Nothing, "", row.Cells(chLotNo.Index).Value)
                    SerialNo = IIf(row.Cells(chSerialNo.Index).Value = Nothing, "", row.Cells(chSerialNo.Index).Value)
                    Size = IIf(row.Cells(chSize.Index).Value = Nothing, "", row.Cells(chSize.Index).Value)
                    Color = IIf(row.Cells(chColor.Index).Value = Nothing, "", row.Cells(chColor.Index).Value)
                    insertSQL = " INSERT INTO " & _
                         " tblRR_Details(TransId, ItemCode, Description, UOM, QTY, WHSE, AccntCode, LineNum, WhoCreated, " & _
                         "  GrossAmount, VATAmount, DiscountRate, Discount, NetAmount, VATable, VATinc, UnitCost, VATType, StandardCost, " & _
                         "  SerialNo, LotNo, DateExpired, Size, Color) " & _
                         " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @WHSE, @AccntCode, @LineNum, @WhoCreated, " & _
                         "  @GrossAmount, @VATAmount, @DiscountRate, @Discount, @NetAmount, @VATable, @VATinc, @UnitCost, @VATType, @StandardCost, " & _
                         "  @SerialNo, @LotNo, @DateExpired, @Size, @Color) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", ItemCode)
                    SQL.AddParam("@Description", Description)
                    SQL.AddParam("@UOM", UOM)
                    SQL.AddParam("@QTY", QTY)
                    If cbWHSE.SelectedItem = "Multiple Warehouse" Then
                        SQL.AddParam("@WHSE", WHSE)
                    Else
                        SQL.AddParam("@WHSE", tempWHSE)
                        WHSE = tempWHSE
                    End If
                    If IsNumeric(row.Cells(chStandardCost.Index).Value) Then SQL.AddParam("@StandardCost", CDec(row.Cells(chStandardCost.Index).Value)) Else SQL.AddParam("@StandardCost", 0)
                    If IsNumeric(row.Cells(chUnitCost.Index).Value) Then SQL.AddParam("@UnitCost", CDec(row.Cells(chUnitCost.Index).Value)) Else SQL.AddParam("@UnitCost", 0)
                    If IsNumeric(row.Cells(chGross.Index).Value) Then SQL.AddParam("@GrossAmount", CDec(row.Cells(chGross.Index).Value)) Else SQL.AddParam("@GrossAmount", 0)
                    If IsNumeric(row.Cells(dgcDiscountRate.Index).Value) Then SQL.AddParam("@DiscountRate", CDec(row.Cells(dgcDiscountRate.Index).Value)) Else SQL.AddParam("@DiscountRate", 0)
                    If IsNumeric(row.Cells(dgcDiscountAmt.Index).Value) Then SQL.AddParam("@Discount", CDec(row.Cells(dgcDiscountAmt.Index).Value)) Else SQL.AddParam("@Discount", 0)
                    If IsNumeric(row.Cells(dgcVATamt.Index).Value) Then SQL.AddParam("@VATAmount", CDec(row.Cells(dgcVATamt.Index).Value)) Else SQL.AddParam("@VATAmount", 0)
                    If IsNumeric(row.Cells(dgcNet.Index).Value) Then SQL.AddParam("@NetAmount", CDec(row.Cells(dgcNet.Index).Value)) Else SQL.AddParam("@NetAmount", 0)
                    If IsNothing(row.Cells(dgcVAT1.Index).Value) Then SQL.AddParam("@VATable", False) Else SQL.AddParam("@VATable", row.Cells(dgcVAT1.Index).Value)
                    If IsNothing(row.Cells(dgcVATInc.Index).Value) Then SQL.AddParam("@VATinc", False) Else SQL.AddParam("@VATinc", row.Cells(dgcVATInc.Index).Value)
                    SQL.AddParam("@AccntCode", AccntCode)
                    SQL.AddParam("@VATType", VATType)
                    SQL.AddParam("@SerialNo", SerialNo)
                    SQL.AddParam("@LotNo", LotNo)
                    SQL.AddParam("@DateExpired", DateExpired)
                    SQL.AddParam("@Size", Size)
                    SQL.AddParam("@Color", Color)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1

                    If cbPurchaseType.SelectedItem = "Goods (Stock)" Then
                        SaveINVTY("IN", ModuleID, "RR", TransID, dtpDocDate.Value.Date, ItemCode, WHSE, QTY, UOM, UnitCost, "Active", SerialNo, LotNo, DateExpired, Size, Color)
                    End If
                End If
            Next

            ComputeWAUC("RR", TransID)
            If RR_Book = "Inventory" Then
                JETransID = LoadJE("RR", TransID)

                ' UPDATE ENTRIES
                If JETransID = 0 Then
                    JETransID = GenerateTransID("JE_No", "tblJE_Header")

                    insertSQL = " INSERT INTO " & _
                        " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, Currency, WhoCreated) " & _
                        " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @Currency, @WhoCreated)"
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransID)
                    SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                    SQL.AddParam("@RefType", "RR")
                    SQL.AddParam("@RefTransID", TransID)
                    SQL.AddParam("@Book", RR_Book)
                    SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                    SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                    SQL.AddParam("@Remarks", txtRemarks.Text)
                    SQL.AddParam("@BranchCode", BranchCode)
                    SQL.AddParam("@BusinessCode", BusinessType)
                    SQL.AddParam("@WhoCreated", "")
                    SQL.ExecNonQuery(insertSQL)
                Else
                    updateSQL = " UPDATE tblJE_Header " & _
                               " SET    AppDate = @AppDate, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " & _
                               "        RefType = @RefType, RefTransID = @RefTransID, Book = @Book, TotalDBCR = @TotalDBCR, " & _
                               "        Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE(), Currency = @Currency " & _
                               " WHERE  JE_No = @JE_No "
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransID)
                    SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                    SQL.AddParam("@RefType", "RR")
                    SQL.AddParam("@RefTransID", TransID)
                    SQL.AddParam("@Book", RR_Book)
                    SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                    SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                    SQL.AddParam("@Remarks", txtRemarks.Text)
                    SQL.AddParam("@BranchCode", BranchCode)
                    SQL.AddParam("@BusinessCode", BusinessType)
                    SQL.AddParam("@WhoModified", UserID)
                    SQL.ExecNonQuery(updateSQL)
                End If


                ' DELETE ACCOUNTING ENTRIES
                deleteSQL = " DELETE FROM tblJE_Details  WHERE  JE_No = @JE_No "
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransID)
                SQL.ExecNonQuery(deleteSQL)

                line = 1
                For Each item As ListViewItem In lvAccount.Items
                    If item.SubItems(chAccountCode.Index).Text <> "" Then
                        insertSQL = " INSERT INTO " & _
                                    " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, BranchCode, VATType) " & _
                                    " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @BranchCode, @VATType)"
                        SQL.FlushParams()
                        SQL.AddParam("@JE_No", JETransID)
                        SQL.AddParam("@AccntCode", item.SubItems(chAccountCode.Index).Text)
                        SQL.AddParam("@VCECode", txtVCECode.Text)
                        If IsNumeric(item.SubItems(chDebit.Index).Text) Then
                            SQL.AddParam("@Debit", CDec(item.SubItems(chDebit.Index).Text))
                        Else
                            SQL.AddParam("@Debit", 0)
                        End If
                        If IsNumeric(item.SubItems(chCredit.Index).Text) Then
                            SQL.AddParam("@Credit", CDec(item.SubItems(chCredit.Index).Text))
                        Else
                            SQL.AddParam("@Credit", 0)
                        End If
                        SQL.AddParam("@Particulars", "")
                        If item.SubItems(chRef.Index).Text <> "" Then
                            SQL.AddParam("@RefNo", item.SubItems(chRef.Index).Text)
                        Else
                            SQL.AddParam("@RefNo", "")
                        End If
                        SQL.AddParam("@LineNumber", line)
                        If item.SubItems(lvVATType.Index).Text <> "" Then
                            SQL.AddParam("@VATType", item.SubItems(lvVATType.Index).Text)
                        Else
                            SQL.AddParam("@VATType", "")
                        End If
                        SQL.AddParam("@BranchCode", BranchCode)
                        SQL.ExecNonQuery(insertSQL)
                        line += 1
                    End If
                Next
            Else
                updateSQL = " UPDATE tblPJ " & _
                      " SET     BranchCode = @BranchCode, BusinessCode = @BusinessCode,  VCECode = @VCECode, DatePJ = @DatePJ, " & _
                      "        TotalAmount = @TotalAmount, Currency = @Currency, Exchange_Rate = @Exchange_Rate, Remarks = @Remarks,  WhoModified = @WhoModified, DateModified = GETDATE(), " & _
                      "         SIRef = @SIRef " & _
                      " WHERE  RefID = '" & TransID & "' AND Reftype ='RR' "
                SQL.FlushParams()
                SQL.AddParam("@VCECode", txtVCECode.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@DatePJ", dtpDocDate.Value.Date)
                SQL.AddParam("@TotalAmount", CDec(txtGross.Text))
                SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                SQL.AddParam("@Exchange_Rate", "0.0000")
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@TransAuto", TransAuto)
                SQL.AddParam("@WhoModified", UserID)
                SQL.AddParam("@SIRef", txtTransNum.Text)
                SQL.ExecNonQuery(updateSQL)

                Dim selectsql As String
                selectsql = " SELECT  TransID FROM tblPJ WHERE  RefID = '" & TransID & "' AND Reftype ='RR'"
                SQL.ReadQuery(selectsql)
                If SQL.SQLDR.Read Then
                    PJTransID = SQL.SQLDR("TransID").ToString
                    JETransID = LoadJE("PJ", PJTransID)
                End If

                ' UPDATE ENTRIES
                If JETransID = 0 Then
                    JETransID = GenerateTransID("JE_No", "tblJE_Header")

                    insertSQL = " INSERT INTO " & _
                        " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, Currency, WhoCreated) " & _
                        " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @Currency, @WhoCreated)"
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransID)
                    SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                    SQL.AddParam("@RefType", "PJ")
                    SQL.AddParam("@RefTransID", PJTransID)
                    SQL.AddParam("@Book", RR_Book)
                    SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                    SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                    SQL.AddParam("@Remarks", txtRemarks.Text)
                    SQL.AddParam("@BranchCode", BranchCode)
                    SQL.AddParam("@BusinessCode", BusinessType)
                    SQL.AddParam("@WhoCreated", "")
                    SQL.ExecNonQuery(insertSQL)
                Else
                    updateSQL = " UPDATE tblJE_Header " & _
                               " SET    AppDate = @AppDate, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " & _
                               "        RefType = @RefType, RefTransID = @RefTransID, Book = @Book, TotalDBCR = @TotalDBCR, " & _
                               "        Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE(), Currency = @Currency " & _
                               " WHERE  JE_No = @JE_No "
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransID)
                    SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                    SQL.AddParam("@RefType", "PJ")
                    SQL.AddParam("@RefTransID", PJTransID)
                    SQL.AddParam("@Book", RR_Book)
                    SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                    SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                    SQL.AddParam("@Remarks", txtRemarks.Text)
                    SQL.AddParam("@BranchCode", BranchCode)
                    SQL.AddParam("@BusinessCode", BusinessType)
                    SQL.AddParam("@WhoModified", UserID)
                    SQL.ExecNonQuery(updateSQL)
                End If


                ' DELETE ACCOUNTING ENTRIES
                deleteSQL = " DELETE FROM tblJE_Details  WHERE  JE_No = @JE_No "
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransID)
                SQL.ExecNonQuery(deleteSQL)

                line = 1
                For Each item As ListViewItem In lvAccount.Items
                    If item.SubItems(chAccountCode.Index).Text <> "" Then
                        insertSQL = " INSERT INTO " & _
                                    " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, BranchCode, VATType) " & _
                                    " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @BranchCode, @VATType)"
                        SQL.FlushParams()
                        SQL.AddParam("@JE_No", JETransID)
                        SQL.AddParam("@AccntCode", item.SubItems(chAccountCode.Index).Text)
                        SQL.AddParam("@VCECode", txtVCECode.Text)
                        If IsNumeric(item.SubItems(chDebit.Index).Text) Then
                            SQL.AddParam("@Debit", CDec(item.SubItems(chDebit.Index).Text))
                        Else
                            SQL.AddParam("@Debit", 0)
                        End If
                        If IsNumeric(item.SubItems(chCredit.Index).Text) Then
                            SQL.AddParam("@Credit", CDec(item.SubItems(chCredit.Index).Text))
                        Else
                            SQL.AddParam("@Credit", 0)
                        End If
                        SQL.AddParam("@Particulars", "")
                        If item.SubItems(chRef.Index).Text <> "" Then
                            SQL.AddParam("@RefNo", item.SubItems(chRef.Index).Text)
                        Else
                            SQL.AddParam("@RefNo", "")
                        End If
                        If item.SubItems(lvVATType.Index).Text <> "" Then
                            SQL.AddParam("@VATType", item.SubItems(lvVATType.Index).Text)
                        Else
                            SQL.AddParam("@VATType", "")
                        End If
                        SQL.AddParam("@BranchCode", BranchCode)
                        SQL.AddParam("@LineNumber", line)
                        SQL.ExecNonQuery(insertSQL)
                        line += 1
                    End If
                Next
            End If
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "RR_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("RR_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL, updateSQL As String
                        deleteSQL = " UPDATE  tblRR SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        RRNo = txtTransNum.Text
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
                        tsbCopy.Enabled = False
                        EnableControl(False)

                        'DELINVTY(ModuleID, "RR", TransID)

                        Dim line As Integer = 1
                        Dim ItemCode, Description, UOM, WHSE As String
                        Dim QTY, UnitCost As Decimal
                        For Each row As DataGridViewRow In dgvItemList.Rows
                            If Not row.Cells(chRRQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                                ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                                Description = IIf(row.Cells(chItemDesc.Index).Value = Nothing, "", row.Cells(chItemDesc.Index).Value)
                                UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                                If Inv_ComputationMethod = "SC" Then
                                    UnitCost = IIf(row.Cells(chStandardCost.Index).Value = Nothing, "", row.Cells(chStandardCost.Index).Value)
                                Else
                                    UnitCost = IIf(row.Cells(chUnitCost.Index).Value = Nothing, "", row.Cells(chUnitCost.Index).Value)
                                End If
                                If IsNumeric(row.Cells(chRRQTY.Index).Value) Then
                                    QTY = CDec(row.Cells(chRRQTY.Index).Value)
                                Else
                                    QTY = 1
                                End If
                                WHSE = row.Cells(dgcWHSE.Index).Value
                                line += 1

                                SaveINVTY("OUT", ModuleID, "RR", TransID, Date.Today, ItemCode, WHSE, QTY, UOM, UnitCost, "Active")
                            End If
                        Next
                        ComputeWAUC("RR", TransID)

                        If RR_Book = "Inventory" Then
                            JETransID = LoadJE("RR", TransID)
                            updateSQL = " UPDATE tblJE_Header " & _
                              " SET    Status = @Status,  WhoModified = @WhoModified, DateModified = GETDATE()" & _
                              " WHERE  JE_No = @JE_No "
                            SQL.FlushParams()
                            SQL.AddParam("@JE_No", JETransID)
                            SQL.AddParam("@Status", "Cancelled")
                            SQL.AddParam("@WhoModified", UserID)
                            SQL.ExecNonQuery(updateSQL)
                        Else
                            Dim selectsql, PJTransID As String

                            selectsql = " SELECT  TransID FROM tblPJ WHERE  RefID = '" & TransID & "' AND Reftype ='RR'"
                            SQL.ReadQuery(selectsql)
                            If SQL.SQLDR.Read Then
                                PJTransID = SQL.SQLDR("TransID").ToString
                                JETransID = LoadJE("PJ", PJTransID)
                            End If

                            updateSQL = " UPDATE tblPJ " & _
                                     " SET     Status = 'Cancelled' " & _
                                     " WHERE  RefID = '" & TransID & "' AND Reftype ='RR' "
                            SQL.FlushParams()
                            SQL.ExecNonQuery(updateSQL)


                            updateSQL = " UPDATE tblJE_Header " & _
                            " SET    Status = @Status,  WhoModified = @WhoModified, DateModified = GETDATE()" & _
                            " WHERE  JE_No = @JE_No "
                            SQL.FlushParams()
                            SQL.AddParam("@JE_No", JETransID)
                            SQL.AddParam("@Status", "Cancelled")
                            SQL.AddParam("@WhoModified", UserID)
                            SQL.ExecNonQuery(updateSQL)
                        End If

                        Msg("Record cancelled successfully", MsgBoxStyle.Information)
                        RRNo = txtTransNum.Text
                        LoadRR(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "RR_No", RRNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If RRNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblRR  " & _
                    " LEFT JOIN tblVCE_Master ON " & _
                    " tblRR.VCECode = tblVCE_Master.VCECode   " & _
                    " LEFT JOIN tblWarehouse ON " & _
                    " tblRR.WHSE = tblWarehouse.Code " & _
                    " WHERE " & _
                    " ( WHSE IN  " & _
                    " (SELECT    DISTINCT tblWarehouse.Code    " & _
                    " FROM tblWarehouse     " & _
                    " INNER JOIN tblUser_Access ON " & _
                    " tblWarehouse.Code = tblUser_Access.Code      " & _
                    " AND tblUser_Access.Status ='Active'  " & _
                    " AND tblWarehouse.Status ='Active'      " & _
                    " AND tblUser_Access.Type = 'Warehouse'  " & _
                    " AND isAllowed = 1    WHERE     UserID ='" & UserID & "')                    " & _
                    " OR " & _
                    " WHSE = 'MW')  AND RR_No < '" & RRNo & "' ORDER BY RR_No DESC  "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadRR(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If RRNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblRR  " & _
                    " LEFT JOIN tblVCE_Master ON " & _
                    " tblRR.VCECode = tblVCE_Master.VCECode   " & _
                    " LEFT JOIN tblWarehouse ON  " & _
                    " tblRR.WHSE = tblWarehouse.Code " & _
                    " WHERE " & _
                    " ( WHSE IN  " & _
                    " (SELECT DISTINCT tblWarehouse.Code    " & _
                    " FROM tblWarehouse " & _
                    " INNER JOIN tblUser_Access ON " & _
                    " tblWarehouse.Code = tblUser_Access.Code " & _
                    " AND tblUser_Access.Status ='Active'  " & _
                    " AND tblWarehouse.Status ='Active'      " & _
                    " AND tblUser_Access.Type = 'Warehouse'  " & _
                    " AND isAllowed = 1    WHERE     UserID ='" & UserID & "') " & _
                    " OR " & _
                    " WHSE = 'MW')  AND RR_No > '" & RRNo & "' ORDER BY RR_No ASC  "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadRR(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If RRNo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadRR(TransID)
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

    Private Sub tsbCopyPO_Click(sender As System.Object, e As System.EventArgs) Handles tsbCopyPO.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("PO")
        LoadPO(f.transID)
        f.Dispose()
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("RR", TransID, "RR Printout")
        f.Dispose()
    End Sub

    Private Sub dgvItemList_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellClick
        Try
            If e.ColumnIndex = dgcWHSE.Index Then
                If e.RowIndex <> -1 AndAlso dgvItemList.Rows.Count > 0 Then
                    LoadWHSE(e.RowIndex)
                    Dim dgvCol As DataGridViewComboBoxColumn
                    dgvCol = dgvItemList.Columns.Item(e.ColumnIndex)
                    dgvItemList.BeginEdit(True)
                    dgvCol.Selected = True
                    DirectCast(dgvItemList.EditingControl, DataGridViewComboBoxEditingControl).DroppedDown = True
                    Dim editingComboBox As ComboBox = TryCast(sender, ComboBox)
                End If
            ElseIf e.ColumnIndex = chUOM.Index Then
                If e.RowIndex <> -1 AndAlso dgvItemList.Rows.Count > 0 AndAlso dgvItemList.Item(chItemCode.Index, e.RowIndex).Value <> Nothing Then
                    LoadUOM(dgvItemList.Item(chItemCode.Index, e.RowIndex).Value.ToString, e.RowIndex)
                    Dim dgvCol As New DataGridViewComboBoxColumn
                    dgvCol = dgvItemList.Columns.Item(e.ColumnIndex)
                    dgvItemList.BeginEdit(True)
                    dgvCol.Selected = True
                    DirectCast(dgvItemList.EditingControl, DataGridViewComboBoxEditingControl).DroppedDown = True
                    Dim editingComboBox As ComboBox = TryCast(sender, ComboBox)

                End If
            End If
        Catch ex As NullReferenceException
            If dgvItemList.ReadOnly = False Then
                SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
            End If

        End Try
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
                LoadBarCode()
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

    Private Sub dgvGroup_DataError(sender As System.Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvGroup.DataError
        Try

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub dgvGroup_EditingControlShowing(sender As Object, e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvGroup.EditingControlShowing
        Dim editingGroupComboBox As ComboBox = TryCast(e.Control, ComboBox)
        If Not editingGroupComboBox Is Nothing Then
            ' REMOVE AN EXISTING EVENT-HANDLER TO AVOID ADDING MULTIPLE HANDLERS WHEN THE EDITING CONTROL IS REUSED
            RemoveHandler editingGroupComboBox.SelectionChangeCommitted, New EventHandler(AddressOf editingGroupComboBox_SelectionChangeCommitted)

            ' ADD THE EVENT HANDLER
            AddHandler editingGroupComboBox.SelectionChangeCommitted, AddressOf editingGroupComboBox_SelectionChangeCommitted

            ' PREVENT THIS HANDLER FROM FIRING TWICE
            RemoveHandler dgvGroup.EditingControlShowing, AddressOf dgvGroup_EditingControlShowing
        End If
    End Sub

    Private Sub editingGroupComboBox_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rowIndex, columnIndex As Integer
        'Get the editing control
        Dim editingGroupComboBox As ComboBox = TryCast(sender, ComboBox)
        If editingGroupComboBox Is Nothing Then Exit Sub
        'Show your Message Boxes
        If editingGroupComboBox.SelectedIndex <> -1 Then
            rowIndex = dgvGroup.SelectedCells(0).RowIndex
            columnIndex = dgvGroup.SelectedCells(0).ColumnIndex
            If dgvItemList.SelectedCells.Count > 0 Then
                Dim tempCell As DataGridViewComboBoxCell = dgvGroup.Item(columnIndex, rowIndex)
                Dim temp As String = editingGroupComboBox.Text
                dgvGroup.Item(columnIndex, rowIndex).Selected = False
                dgvGroup.EndEdit(True)
                tempCell.Value = temp
                dgvGroup.Item(dgcSubCode.Index, rowIndex).Value = GetItemCode(temp)
            End If
        End If

        'Remove the handle to this event. It will be readded each time a new combobox selection causes the EditingControlShowing Event to fire
        RemoveHandler editingGroupComboBox.SelectionChangeCommitted, AddressOf editingGroupComboBox_SelectionChangeCommitted
        'Re-enable the EditingControlShowing event so the above can take place.
        AddHandler dgvGroup.EditingControlShowing, AddressOf dgvGroup_EditingControlShowing
    End Sub

    Private Sub LoadBarCode()
        Dim query As String
        Dim itemCode, UOM As String
        Dim Barcode As String
        For Each row As DataGridViewRow In dgvItemList.Rows
            If Not IsNothing(row.Cells(chItemCode.Index).Value) AndAlso Not IsNothing(row.Cells(chUOM.Index).Value) Then
                itemCode = row.Cells(chItemCode.Index).Value.ToString
                UOM = row.Cells(chUOM.Index).Value.ToString
                ' QUERY Barcode
                query = " SELECT Barcode FROM tblItem_Barcode WHERE UOM = @UOM AND ItemCode= @ItemCode AND STATUS <> 'Inactive'"
                SQL.FlushParams()
                SQL.AddParam("@ItemCode", itemCode)
                SQL.AddParam("@UOM", UOM)
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    Barcode = SQL.SQLDR("Barcode")
                    row.Cells(chBarCode.Index).Value = Barcode
                Else
                    row.Cells(chBarCode.Index).Value = ""
                End If
            End If

        Next
    End Sub

    Private Sub dgvGroup_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvGroup.CellContentClick
        Try
            If e.ColumnIndex = dgcSubAdd.Index Then
                With dgvGroup.SelectedRows(0)
                    dgvItemList.Rows.Add(.Cells(dgcSubCode.Index).Value.ToString, .Cells(dgcSubDesc.Index).Value.ToString, .Cells(dgcSubUOM.Index).Value.ToString, _
                                         .Cells(dgcSubPOQTY.Index).Value, .Cells(dgcSubActual.Index).Value)
                End With
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadWHSE(Optional ByVal SelectedIndex As Integer = -1)
        Try
            If SelectedIndex = -1 Then
                Dim query As String
                query = " SELECT tblWarehouse.Code + ' | ' + Description AS WHSECode " & _
                        " FROM tblWarehouse INNER JOIN tblUser_Access " & _
                        " ON tblWarehouse.Code = tblUser_Access.Code " & _
                        " AND tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active' " & _
                        " AND Type = 'Warehouse' AND isAllowed = 1 " & _
                        " WHERE UserID ='" & UserID & "' "
                SQL.ReadQuery(query)
                cbWHSE.Items.Clear()
                cbWHSE.Items.Add("Multiple Warehouse")
                Dim ctr As Integer = 0
                While SQL.SQLDR.Read
                    cbWHSE.Items.Add(SQL.SQLDR("WHSECode").ToString)
                    ctr += 1
                End While
                If ctr <= 1 Then
                    cbWHSE.Items.Remove("Multiple Warehouse")
                End If
                If cbWHSE.Items.Count > 0 Then
                    cbWHSE.SelectedIndex = 0
                End If
            Else
                Dim dgvCB As New DataGridViewComboBoxCell
                dgvCB = dgvItemList.Item(dgcWHSE.Index, SelectedIndex)
                Dim temp As String = dgvCB.Value.ToString
                dgvCB.Value = Nothing
                ' ADD ALL WHSEc
                Dim query As String
                query = " SELECT tblWarehouse.Code " & _
                         " FROM tblWarehouse INNER JOIN tblUser_Access " & _
                         " ON tblWarehouse.Code = tblUser_Access.Code " & _
                         " AND tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active' " & _
                         " AND Type = 'Warehouse' AND isAllowed = 1 " & _
                         " WHERE UserID ='" & UserID & "' "
                SQL.ReadQuery(query)
                dgvCB.Items.Clear()
                dgvCB.Items.Add("Multiple Warehouse")
                Dim ctr As Integer = 0
                While SQL.SQLDR.Read
                    dgvCB.Items.Add(SQL.SQLDR("Code").ToString)
                    ctr += 1
                End While
                If ctr <= 1 Then
                    dgvCB.Items.Remove("Multiple Warehouse")
                End If
                dgvCB.Value = temp
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub cbWHSE_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbWHSE.SelectedIndexChanged
        If disableEvent = False Then
            If cbWHSE.SelectedIndex <> -1 Then
                If cbWHSE.SelectedItem = "Multiple Warehouse" Then
                    If tempWHSE <> "" Then
                        For Each row As DataGridViewRow In dgvItemList.Rows
                            row.Cells(dgcWHSE.Index).Value = tempWHSE
                        Next
                    End If
                    dgvItemList.Columns(dgcWHSE.Index).Visible = True
                Else
                    dgvItemList.Columns(dgcWHSE.Index).Visible = False
                    tempWHSE = Strings.Left(cbWHSE.SelectedItem, cbWHSE.SelectedItem.ToString.IndexOf(" | "))
                End If
            End If
        End If
    End Sub

    Private Sub dgvItemList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellContentClick

    End Sub

    Private Sub tsbReports_Click(sender As System.Object, e As System.EventArgs) Handles tsbReports.Click

    End Sub

    Private Sub lvAccount_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lvAccount.SelectedIndexChanged

    End Sub

    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged

    End Sub

    Private Sub dgvItemList_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles dgvItemList.DragEnter

    End Sub

    Private Sub dgvItemList_CellErrorTextNeeded(sender As Object, e As System.Windows.Forms.DataGridViewCellErrorTextNeededEventArgs) Handles dgvItemList.CellErrorTextNeeded

    End Sub

    Private Sub dgvItemList_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgvItemList.CellValidating
        If e.ColumnIndex = chDateExpired.Index Then
            Dim dt As DateTime
            If e.FormattedValue.ToString <> String.Empty AndAlso Not DateTime.TryParse(e.FormattedValue.ToString, dt) Then
                MessageBox.Show("Enter correct Date")
                e.Cancel = True
            End If

        End If
    End Sub

    Private Sub cbPurchaseType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbPurchaseType.SelectedIndexChanged

    End Sub

    Private Sub dgvItemList_RowsRemoved(sender As Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgvItemList.RowsRemoved
        Try
            If disableEvent = False Then
                ComputeTotal()
                GenerateEntry()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
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
End Class