Imports System.Net.Mail
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmPO
    Dim TransID As String
    Dim PONo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "PO"
    Dim ColumnPK As String = "PO_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblPO"
    Dim TransAuto, TransAutoPO As Boolean
    Dim AccntCode As String
    Dim CF_ID As Integer
    Dim SO_ID As Integer
    Dim SONo As String
    Dim PR_ID As String
    Dim PO_App As Boolean = False
    Public sendmail
    Dim EmailAddress, EmailPassword As String
    Dim ForApproval As Boolean = False

    Public Overloads Function ShowDialog(ByVal docnumber As String) As Boolean
        TransID = docnumber
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmPO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LoadSetup()
            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            dtpDelivery.Value = Date.Today.Date
            If TransID <> "" Then
                LoadPO(TransID)
            End If

            LoadCostCenter()
            LoadChartOfAccount()
            LoadTerms()

            tsbSearch.Enabled = True
            tsbNew.Enabled = True
            tsbEdit.Enabled = False
            tsbSave.Enabled = False
            tsbCancel.Enabled = False
            tsbApprove.Enabled = False
            tsbClose.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = True
            tsbEmail.Enabled = False
            tsbPrint.Enabled = False
            tsbCopy.Enabled = False
            EnableControl(False)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub LoadSetup()
        Dim query As String
        query = " SELECT  ISNULL(PO_Approval,0) AS PO_Approval, ISNULL(PO_DiffSeries,0) AS PO_DiffSeries, " & _
                " EmailUsername,  EmailPassword " & _
                " FROM tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            PO_App = SQL.SQLDR("PO_Approval")
            TransAutoPO = SQL.SQLDR("PO_DiffSeries")
            EmailAddress = SQL.SQLDR("EmailUsername")
            EmailPassword = SQL.SQLDR("EmailPassword")
        End If
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        txtVCEName.Enabled = Value
        btnSearchVCE.Enabled = Value
        dgvItemList.AllowUserToAddRows = Value
        dgvItemList.AllowUserToDeleteRows = Value
        If Value = True Then
            dgvItemList.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemList.EditMode = DataGridViewEditMode.EditProgrammatically
        End If

        dgvItemList.Columns(chStockQTY.Index).Visible = Value
        dgvItemList.Columns(chStockQTY.Index).ReadOnly = True
        dgvItemList.Columns(chReorderingPoint.Index).ReadOnly = True
        cbCostCenter.Enabled = Value
        cbPurchaseType.Enabled = Value
        cbGLAccount.Enabled = Value
        cbTerms.Enabled = Value
        cbDeliverTo.Enabled = Value
        txtRemarks.Enabled = Value
        txtDiscountRate.Enabled = Value
        btnApplyRate.Enabled = Value
        dtpDocDate.Enabled = Value
        dtpDelivery.Enabled = Value
        chkVAT.Enabled = Value
        cbCurrency.Enabled = Value
        txtEmail.Enabled = Value
        chkFixedAsset.Enabled = Value
        chkPartialPayment.Enabled = Value
        lnkPartialPayment.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Private Sub LoadTerms()
        Dim query As String
        query = " SELECT  Description " & _
                " FROM    tblTerms " & _
                " WHERE   tblTerms.Status = 'Active'" & _
                " ORDER BY Days "
        SQL.ReadQuery(query)
        cbTerms.Items.Clear()
        While SQL.SQLDR.Read
            cbTerms.Items.Add(SQL.SQLDR("Description"))
        End While
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

    Private Sub LoadPO(ByVal ID As String)
        Dim query, CC, Currency As String
        query = " SELECT   tblPO.TransID, tblPO.PO_No, VCECode, PurchaseType, DatePO, DateDeliver, Remarks, CostCenter, " & _
                "          GrossAmount, Discount, VATAmount, NetAmount, VATable, ISNULL(VATInclusive,'True') AS VATInclusive, " & _
                "          viewPO_Status.Status, AccntCode, DeliverTo, PR_Ref, CF_Ref, SO_Ref, Terms, Currency, EmailAddress, ISNULL(isPartial,0) as isPartial , ISNULL(isFixedAsset,0) as isFixedAsset " & _
                " FROM     tblPO INNER JOIN viewPO_Status " & _
                " ON       tblPO.TransID = viewPO_Status.TransID " & _
                " WHERE    tblPO.TransID = '" & ID & "' " & _
                " ORDER BY TransID "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            disableEvent = True
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            PONo = SQL.SQLDR("PO_No").ToString
            txtTransNum.Text = PONo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            If IsDBNull(SQL.SQLDR("PurchaseType")) Then
                cbPurchaseType.SelectedIndex = -1
            Else
                cbPurchaseType.SelectedItem = SQL.SQLDR("PurchaseType").ToString
            End If
            If IsDBNull(SQL.SQLDR("Terms")) Then
                cbTerms.SelectedIndex = -1
            Else
                cbTerms.Text = SQL.SQLDR("Terms").ToString
            End If
            CC = SQL.SQLDR("CostCenter").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            dtpDocDate.Text = SQL.SQLDR("DatePO").ToString
            dtpDelivery.Text = SQL.SQLDR("DateDeliver").ToString
            txtGross.Text = SQL.SQLDR("GrossAmount").ToString
            txtDiscount.Text = SQL.SQLDR("Discount").ToString
            txtVAT.Text = SQL.SQLDR("VATAmount").ToString
            txtNet.Text = SQL.SQLDR("NetAmount").ToString
            cbDeliverTo.Text = SQL.SQLDR("DeliverTo").ToString
            Dim code As String = SQL.SQLDR("AccntCode").ToString
            chkVAT.Checked = SQL.SQLDR("VATable")
            chkVATInc.Checked = SQL.SQLDR("VATInclusive")
            txtEmail.Text = SQL.SQLDR("EmailAddress").ToString
            PR_ID = SQL.SQLDR("PR_Ref").ToString
            CF_ID = SQL.SQLDR("CF_Ref").ToString
            SO_ID = SQL.SQLDR("SO_Ref").ToString
            Currency = SQL.SQLDR("Currency").ToString
            chkPartialPayment.Checked = SQL.SQLDR("isPartial").ToString
            chkFixedAsset.Checked = SQL.SQLDR("isFixedAsset").ToString
            disableEvent = False
            cbCostCenter.SelectedItem = GetCCName(CC)
            'txtPRNo.Text = LoadPRNo(PR_ID)
            txtCFNo.Text = LoadCFNo(CF_ID)
            txtSONo.Text = LoadSONo(SO_ID)
            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            cbGLAccount.Text = GetAccntTitle(code)
            LoadVCE_Info(txtVCECode.Text)
            LoadPODetails(TransID)
            ComputeTotal()
            RefreshDatagrid()

            cbCurrency.Items.Clear()
            For Each item In LoadVCECurrency(txtVCECode.Text)
                cbCurrency.Items.Add(item)
            Next
            If cbCurrency.Items.Count = 0 Then
                cbCurrency.Items.Add(BaseCurrency)
            End If
            cbCurrency.SelectedItem = Currency

            If chkPartialPayment.Checked = True Then
                frmPO_PartialPayment.TransID = TransID
                frmPO_PartialPayment.Show()
                frmPO_PartialPayment.Hide()
            End If

            ' TOOLSTRIP BUTTONS
            If txtStatus.Text = "Cancelled" Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
                tsbApprove.Enabled = False
            ElseIf txtStatus.Text = "Closed" Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
                tsbApprove.Enabled = False
            ElseIf txtStatus.Text = "For Approval" Then
                tsbEdit.Enabled = True
                tsbCancel.Enabled = True
                tsbApprove.Enabled = True
            Else
                tsbEdit.Enabled = True
                tsbCancel.Enabled = True
                tsbApprove.Enabled = False
            End If
            tsbPrint.Enabled = True
            tsbClose.Enabled = False
            tsbPrevious.Enabled = True
            tsbEmail.Enabled = True
            tsbNext.Enabled = True
            tsbPrint.Enabled = True
            tsbSave.Enabled = False
            tsbNew.Enabled = True
            tsbSearch.Enabled = True
            tsbExit.Enabled = True
            tsbCopy.Enabled = False
            If dtpDocDate.Value <= GetMaxPEC() Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            End If
            EnableControl(False)
        Else
            ClearText()
        End If
    End Sub

    Private Function LoadPRNo(ID As Integer) As String
        Dim query As String
        query = " SELECT PR_No FROM tblPR WHERE TransID = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("PR_No")
        Else
            Return ""
        End If
    End Function

    Private Function LoadSONo(ID As Integer) As String
        Dim query As String
        query = " SELECT SO_No FROM tblSO WHERE TransID = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("SO_No")
        Else
            Return ""
        End If
    End Function

    Private Function LoadCFNo(ID As Integer) As String
        Dim query As String
        query = " SELECT CF_No FROM tblCF WHERE TransID = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("CF_No")
        Else
            Return ""
        End If
    End Function

    Protected Sub LoadPODetails(ByVal TransID As String)
        Dim ctr As Integer = 0
        Dim query As String
        query = " SELECT    ItemGroup, ItemCode, Description, UOM, QTY, UnitPrice, GrossAmount, VATAmount, DiscountRate, Discount, NetAmount, VATable,  " & _
                "           VATInc, WHSE, AccntCode, AccountTitle, tblPO_Details.VCECode, VCEName, VATType, ISNULL(tblPO_Details.Size,'') AS Size,  " & _
                "           ISNULL(tblPO_Details.Color,'') AS Color, ISNULL(tblPO_Details.Reorder_Point,0) AS Reorder_Point " & _
                " FROM      tblPO_Details LEFT JOIN tblCOA_Master " & _
                " ON        tblPO_Details.AccntCode = tblCOA_Master.AccountCode " & _
                " LEFT JOIN tblVCE_Master " & _
                " ON        tblVCE_Master.VCECode = tblPO_Details.VCECode " & _
                " WHERE     tblPO_Details.TransId = " & TransID & " " & _
                " ORDER BY  LineNum "
        disableEvent = True
        dgvItemList.Rows.Clear()
        disableEvent = False

        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvItemList.Rows.Add(row(0).ToString, row(1).ToString, "", row(2).ToString,
                                                row(3).ToString, 0, row(4).ToString, CDec(row(5)).ToString("N2"),
                                              CDec(row(6)).ToString("N2"),
                                              CDec(row(8)).ToString("N2"),
                                              CDec(row(9)).ToString("N2"),
                                              CDec(row(7)).ToString("N2"),
                                              CDec(row(10)).ToString("N2"),
                                                row(11).ToString, row(12).ToString, row(13).ToString, row(14).ToString, row(15).ToString,
                                                row(16).ToString, row(17).ToString, row(18).ToString, row(19).ToString, row(20).ToString, CDec(row(21)).ToString("N2"))

                LoadVATType(ctr)
                LoadUOM(row(1).ToString, ctr)
                LoadColor(row(1).ToString, ctr)
                LoadSize(row(1).ToString, ctr)
                ctr += 1
                dgvItemList.Columns(chStockQTY.Index).Visible = False
            Next
            LoadBarCode()
        End If
    End Sub

    Private Sub ClearText()
        txtTransNum.Text = ""
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        txtAddress.Text = ""
        txtTIN.Text = ""
        txtContact.Text = ""
        txtEmail.Text = ""
        txtPerson.Text = ""
        cbCurrency.Items.Clear()
        dgvItemList.Rows.Clear()
        cbDeliverTo.Text = ""
        cbPurchaseType.SelectedItem = "Goods (Stock)"
        cbTerms.SelectedItem = -1
        cbGLAccount.SelectedIndex = -1
        txtRemarks.Text = ""
        txtGross.Text = "0.00"
        txtNet.Text = "0.00"
        txtDiscount.Text = "0.00"
        txtVAT.Text = "0.00"
        txtStatus.Text = "Open"
        dtpDocDate.MinDate = GetMaxPEC().AddDays(1)
        dtpDocDate.Value = Date.Today.Date
        dtpDelivery.Value = Date.Today.Date
        txtPRNo.Clear()
        txtCFNo.Clear()
        txtSONo.Clear()
        chkPartialPayment.Checked = False
        chkFixedAsset.Checked = False

        LoadChartOfAccount()
        LoadTerms()
    End Sub

    Private Sub dgvItemList_CellBeginEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvItemList.CellBeginEdit
        eColIndex = e.ColumnIndex
    End Sub


    Private Sub dgvProducts_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellEndEdit
        Try
            Dim itemCode, UOM As String
            Dim rowIndex As Integer = dgvItemList.CurrentCell.RowIndex
            Dim colindex As Integer = dgvItemList.CurrentCell.ColumnIndex
            Select Case colindex
                Case chItemCode.Index
                    If dgvItemList.Item(chItemCode.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemList.Item(chItemCode.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("ItemListPO", itemCode, "Purchase", chkFixedAsset.Checked, "ItemCode")
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItem("", itemCode)
                        End If
                        dgvItemList.Rows.RemoveAt(e.RowIndex)
                        f.Dispose()
                    End If
                Case chItemDesc.Index
                    If cbPurchaseType.SelectedItem = "Goods (Stock)" Or cbPurchaseType.SelectedItem = "Non-Stock" Then
                        If dgvItemList.Item(chItemDesc.Index, e.RowIndex).Value <> "" Then
                            itemCode = dgvItemList.Item(chItemDesc.Index, e.RowIndex).Value
                            Dim f As New frmCopyFrom
                            f.ShowDialog("ItemListPO", itemCode, "Purchase", chkFixedAsset.Checked, "ItemName")
                            If f.TransID <> "" Then
                                itemCode = f.TransID
                                LoadItem("", itemCode)
                            End If
                            dgvItemList.Rows.RemoveAt(e.RowIndex)
                            f.Dispose()
                        End If
                    End If
                Case chBarcode.Index
                    If cbPurchaseType.SelectedItem = "Goods (Stock)" Or cbPurchaseType.SelectedItem = "Non-Stock" Then
                        If dgvItemList.Item(chBarcode.Index, e.RowIndex).Value <> "" Then
                            itemCode = dgvItemList.Item(chBarcode.Index, e.RowIndex).Value
                            Dim f As New frmCopyFrom
                            f.ShowDialog("ItemListPO", itemCode, "Purchase", chkFixedAsset.Checked, "Barcode")
                            If f.TransID <> "" Then
                                itemCode = f.TransID
                                UOM = f.UOM
                                LoadItem("", itemCode, UOM)
                            End If
                            dgvItemList.Rows.RemoveAt(e.RowIndex)
                            f.Dispose()
                        End If
                    End If

                Case chQTY.Index
                    If dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value = "" OrElse IsNothing(dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value) Then
                        dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value = 0
                    ElseIf IsNumeric(dgvItemList.Item(chUnitPrice.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemList.Item(chQTY.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex)
                        ComputeTotal()
                    End If
                Case chUnitPrice.Index
                    If dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value = "" OrElse IsNothing(dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value) Then
                        dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value = 0
                    ElseIf IsNumeric(dgvItemList.Item(chUnitPrice.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemList.Item(chQTY.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex)
                        ComputeTotal()
                        dgvItemList.Item(chUnitPrice.Index, e.RowIndex).Value = CDec(dgvItemList.Item(chUnitPrice.Index, e.RowIndex).Value).ToString("N2")
                    End If
                Case chGross.Index
                    If dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value = "" OrElse IsNothing(dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value) Then
                        dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value = 0
                    End If
                    Recompute(e.RowIndex, e.ColumnIndex)
                    ComputeTotal()
                    dgvItemList.Item(chUnitPrice.Index, e.RowIndex).Value = CDec(dgvItemList.Item(chUnitPrice.Index, e.RowIndex).Value).ToString("N2")
                Case chDiscountRate.Index
                    If IsNumeric(dgvItemList.Item(chGross.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemList.Item(chDiscountRate.Index, e.RowIndex).Value) Then
                        txtDiscountRate.Text = ""
                        Recompute(e.RowIndex, e.ColumnIndex)
                        ComputeTotal()
                    End If
                Case chDiscount.Index
                    dgvItemList.Item(chDiscountRate.Index, e.RowIndex).Value = Nothing
                    If IsNumeric(dgvItemList.Item(chGross.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemList.Item(chDiscount.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex)
                        ComputeTotal()
                    End If
                Case chUnitPrice.Index
                    If IsNumeric(dgvItemList.Item(chGross.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemList.Item(chDiscount.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex)
                        ComputeTotal()
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
                Case chCustomerName.Index
                    Dim f As New frmVCE_Search
                    f.txtFilter.Text = dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                    f.Type = "Member"
                    f.ShowDialog()
                    dgvItemList.Item(chCustomerCode.Index, e.RowIndex).Value = f.VCECode
                    dgvItemList.Item(chCustomerName.Index, e.RowIndex).Value = f.VCEName
                    f.Dispose()
            End Select
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadStock()
        Dim query As String
        Dim itemCode, UOM As String
        Dim BOMQTY As Decimal = 0
        Dim StockQTY As Decimal = 0
        Dim ResQTY As Decimal = 0
        Dim PRQTY As Decimal = 0
        For Each row As DataGridViewRow In dgvItemList.Rows
            If Not IsNothing(row.Cells(chItemCode.Index).Value) AndAlso Not IsNothing(row.Cells(chUOM.Index).Value) Then
                itemCode = row.Cells(chItemCode.Index).Value.ToString

                UOM = row.Cells(chUOM.Index).Value.ToString
                ' QUERY STOCK
                query = " SELECT ISNULL(SUM(BaseQTY),0) AS QTY " &
                        " FROM viewItemStock_BaseUnit " &
                        " WHERE ItemCode = @ItemCode"
                SQL.FlushParams()
                SQL.AddParam("@ItemCode", itemCode)
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    StockQTY = SQL.SQLDR("QTY")

                    query = " SELECT ISNULL(SUM(QTY),1) AS QTY " &
                        " FROM viewItem_UOM " &
                        " WHERE GroupCode = @ItemCode AND UnitCode  = @UOM "
                    SQL.FlushParams()
                    SQL.AddParam("@ItemCode", itemCode)
                    SQL.AddParam("@UOM", UOM)
                    SQL.ReadQuery(query)
                    If SQL.SQLDR.Read Then
                        StockQTY = StockQTY / SQL.SQLDR("QTY")
                    End If

                    row.Cells(chStockQTY.Index).Value = StockQTY
                End If
            End If

        Next
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
                query = " SELECT Barcode FROM tblItem_Barcode WHERE UOM = @UOM AND ItemCode= @ItemCode  AND STATUS <> 'Inactive'"
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

    'Private Sub Recompute(ByVal RowID As Integer, ByVal ColID As Integer)
    '    Dim gross, VAT, discount, net, baseVAT As Decimal
    '    If RowID <> -1 Then
    '        ' GET GROSS AMOUNT (VAT INCLUSIVE)
    '        If IsNumeric(dgvItemList.Item(chUnitPrice.Index, RowID).Value) AndAlso IsNumeric(dgvItemList.Item(chQTY.Index, RowID).Value) AndAlso dgvItemList.Columns(chQTY.Index).Visible = True Then
    '            gross = CDec(dgvItemList.Item(chUnitPrice.Index, RowID).Value) * CDec(dgvItemList.Item(chQTY.Index, RowID).Value)

    '        ElseIf IsNumeric(dgvItemList.Item(chGross.Index, RowID).Value) Then
    '            gross = CDec(dgvItemList.Item(chGross.Index, RowID).Value)
    '        Else
    '            gross = 0
    '        End If
    '        baseVAT = gross
    '        ' COMPUTE VAT IF VATABLE
    '        If ColID = chVAT.Index Then
    '            If dgvItemList.Item(chVAT.Index, RowID).Value = True Then
    '                dgvItemList.Item(chVAT.Index, RowID).Value = False

    '                dgvItemList.Item(chVATinc.Index, RowID).Value = False
    '                VAT = 0
    '                dgvItemList.Item(chVATinc.Index, RowID).ReadOnly = True
    '            Else
    '                dgvItemList.Item(chVAT.Index, RowID).Value = True

    '                dgvItemList.Item(chVATinc.Index, RowID).ReadOnly = False
    '                If dgvItemList.Item(chVATinc.Index, RowID).Value = False Then
    '                    VAT = CDec(baseVAT * 0.12).ToString("N2")
    '                Else
    '                    baseVAT = (gross / 1.12)
    '                    VAT = CDec(baseVAT * 0.12).ToString("N2")
    '                End If

    '            End If
    '        ElseIf ColID = chVATinc.Index Then
    '            If dgvItemList.Item(chVAT.Index, RowID).Value = False Then
    '                VAT = 0
    '            Else
    '                If dgvItemList.Item(chVATinc.Index, RowID).Value = True Then
    '                    dgvItemList.Item(chVATinc.Index, RowID).Value = False
    '                    VAT = CDec(baseVAT * 0.12).ToString("N2")
    '                Else
    '                    dgvItemList.Item(chVATinc.Index, RowID).Value = True
    '                    'baseVAT = (gross / 1.12)
    '                    'VAT = CDec(baseVAT * 0.12).ToString("N2")
    '                    'baseVAT = gross
    '                    VAT = gross - (gross / 1.12)
    '                    gross = (gross / 1.12)
    '                End If

    '            End If
    '        Else
    '            If dgvItemList.Item(chVAT.Index, RowID).Value = False Then
    '                VAT = 0
    '                dgvItemList.Item(chVATinc.Index, RowID).ReadOnly = True
    '            Else
    '                dgvItemList.Item(chVATinc.Index, RowID).ReadOnly = False
    '                If dgvItemList.Item(chVATinc.Index, RowID).Value = True Then ' IF VAT INCLUSIVE, BASE AMOUNT WILL BE GROSS / 1.12
    '                    'baseVAT = (gross / 1.12)

    '                    VAT = gross - (gross / 1.12)
    '                    gross = (gross / 1.12)
    '                End If
    '                VAT = CDec(baseVAT * 0.12).ToString("N2")
    '            End If
    '        End If


    '        ' COMPUTE DISCOUNT
    '        If IsNumeric(dgvItemList.Item(chDiscountRate.Index, RowID).Value) Then
    '            discount = CDec(baseVAT * (CDec(dgvItemList.Item(chDiscountRate.Index, RowID).Value) / 100.0)).ToString("N2")
    '        ElseIf IsNumeric(dgvItemList.Item(chDiscount.Index, RowID).Value) Then
    '            discount = CDec(dgvItemList.Item(chDiscount.Index, RowID).Value)
    '        Else
    '            discount = 0
    '        End If

    '        If dgvItemList.Item(chVATinc.Index, RowID).Value = False Then

    '            net = baseVAT - discount + VAT
    '        Else
    '            net = baseVAT - discount
    '        End If

    '        'net = baseVAT - discount + VAT
    '        dgvItemList.Item(chGross.Index, RowID).Value = Format(gross, "#,###,###,###.00").ToString()
    '        dgvItemList.Item(chDiscount.Index, RowID).Value = Format(discount, "#,###,###,###.00").ToString()
    '        dgvItemList.Item(chVATAmt.Index, RowID).Value = Format(VAT, "#,###,###,###.00").ToString()
    '        dgvItemList.Item(chNetAmount.Index, RowID).Value = Format(net, "#,###,###,###.00").ToString()
    '        ComputeTotal()

    '    End If

    'End Sub

    Private Sub Recompute(ByVal RowID As Integer, ByVal ColID As Integer)
        Dim gross, VAT, discount, net, baseVAT As Decimal
        If RowID <> -1 Then
            ' GET GROSS AMOUNT (VAT INCLUSIVE)
            If IsNumeric(dgvItemList.Item(chUnitPrice.Index, RowID).Value) AndAlso IsNumeric(dgvItemList.Item(chQTY.Index, RowID).Value) AndAlso dgvItemList.Columns(chQTY.Index).Visible = True Then
                gross = CDec(dgvItemList.Item(chUnitPrice.Index, RowID).Value) * CDec(dgvItemList.Item(chQTY.Index, RowID).Value)

            ElseIf IsNumeric(dgvItemList.Item(chGross.Index, RowID).Value) Then
                gross = CDec(dgvItemList.Item(chGross.Index, RowID).Value)
            Else
                gross = 0
            End If
            baseVAT = gross


            ' COMPUTE DISCOUNT
            If IsNumeric(dgvItemList.Item(chDiscountRate.Index, RowID).Value) Then
                discount = CDec(gross * (CDec(dgvItemList.Item(chDiscountRate.Index, RowID).Value) / 100.0)).ToString("N2")
            ElseIf IsNumeric(dgvItemList.Item(chDiscount.Index, RowID).Value) Then
                discount = CDec(dgvItemList.Item(chDiscount.Index, RowID).Value)
            Else
                discount = 0
            End If
            ' COMPUTE VAT IF VATABLE
            If ColID = chVAT.Index Then
                If dgvItemList.Item(chVAT.Index, RowID).Value = True Then
                    dgvItemList.Item(chVAT.Index, RowID).Value = False
                    dgvItemList.Item(chVATinc.Index, RowID).Value = False
                    VAT = 0
                    dgvItemList.Item(chVATinc.Index, RowID).ReadOnly = True
                Else
                    dgvItemList.Item(chVAT.Index, RowID).Value = True
                    dgvItemList.Item(chVATinc.Index, RowID).ReadOnly = False
                    If dgvItemList.Item(chVATinc.Index, RowID).Value = False Then
                        VAT = CDec((gross - discount) * 0.12).ToString("N2")
                    Else
                        VAT = (gross - discount) / 1.12 * 0.12
                    End If

                End If
            ElseIf ColID = chVATinc.Index Then
                If dgvItemList.Item(chVAT.Index, RowID).Value = False Then
                    VAT = 0
                Else
                    If dgvItemList.Item(chVATinc.Index, RowID).Value = True Then
                        dgvItemList.Item(chVATinc.Index, RowID).Value = False
                        VAT = CDec((gross - discount) * 0.12).ToString("N2")
                    Else
                        dgvItemList.Item(chVATinc.Index, RowID).Value = True
                        VAT = (gross - discount) / 1.12 * 0.12
                    End If

                End If
            Else
                If dgvItemList.Item(chVAT.Index, RowID).Value = False Then
                    VAT = 0
                    dgvItemList.Item(chVATinc.Index, RowID).ReadOnly = True
                Else
                    dgvItemList.Item(chVATinc.Index, RowID).ReadOnly = False
                    If dgvItemList.Item(chVATinc.Index, RowID).Value = True Then ' IF VAT INCLUSIVE, BASE AMOUNT WILL BE GROSS / 1.12

                        VAT = (gross - discount) / 1.12 * 0.12
                    Else
                        VAT = CDec((gross - discount) * 0.12).ToString("N2")
                    End If
                End If
            End If


            If dgvItemList.Item(chVATinc.Index, RowID).Value = False Then

                net = gross - discount + VAT
            Else
                net = gross - discount
            End If

            dgvItemList.Item(chGross.Index, RowID).Value = Format(gross, "#,###,###,###.00").ToString()
            dgvItemList.Item(chDiscount.Index, RowID).Value = Format(discount, "#,###,###,###.00").ToString()
            dgvItemList.Item(chVATAmt.Index, RowID).Value = Format(VAT, "#,###,###,###.00").ToString()
            dgvItemList.Item(chNetAmount.Index, RowID).Value = Format(net, "#,###,###,###.00").ToString()
            ComputeTotal()

        End If

    End Sub

    Public Sub LoadItem(ByVal ItemGroup As String, ByVal ItemCode As String, Optional UOM As String = "", Optional QTY As Integer = 1, Optional InvAccount As String = "")
        Try
            Dim query As String
            Dim VAT, VATInc As Boolean
            Dim Price, VATAmt As Decimal
            If ItemCode = "" Then
                query = " SELECT  BOMGroup AS ItemGroup, '' AS ItemCode,  '' AS ItemName, UOM AS PD_UOM,  " & _
                        "         StandardCost AS Net_Price, '' AS WHSE, CAST(1 AS bit) AS VATable, CAST(1 AS bit) AS PD_VATinc, '' AS AD_Inv " & _
                        " FROM    tblBOM_Group " & _
                        " WHERE   BOMGroup = @BOMGroup "
                SQL.FlushParams()
                SQL.AddParam("@BOMGroup", ItemGroup)
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    If UOM = "" Then
                        UOM = SQL.SQLDR("PD_UOM").ToString
                    End If
                    VAT = SQL.SQLDR("VATable").ToString
                    Price = SQL.SQLDR("Net_Price")
                    If VAT = True Then
                        VATAmt = (Price / 1.12) * 0.12
                    Else
                        VATAmt = 0
                    End If
                    If InvAccount = "" Then InvAccount = SQL.SQLDR("AD_Inv").ToString
                    dgvItemList.Rows.Add(New String() {SQL.SQLDR("ItemGroup").ToString, SQL.SQLDR("ItemCode").ToString,
                                                  SQL.SQLDR("ItemName").ToString,
                                                  UOM,
                                                  0, QTY,
                                                  Format(Price, "#,###,###,###.00").ToString,
                                                  Format(Price * QTY, "#,###,###,###.00").ToString,
                                                  "",
                                                  "0.00",
                                                  Format(VATAmt * QTY, "#,###,###,###.00").ToString,
                                                  Format((Price) * QTY, "#,###,###,###.00").ToString,
                                                  SQL.SQLDR("VATable").ToString,
                                                  SQL.SQLDR("PD_VATinc").ToString,
                                                  SQL.SQLDR("WHSE").ToString,
                                                  InvAccount, ""})
                    LoadVATType(dgvItemList.Rows.Count - 2)
                End If
            Else
                query = " SELECT  ItemGroup, ItemCode,  ItemName, ISNULL(PD_UOM,ItemUOM) AS PD_UOM,  " & _
                                    "         ISNULL(PD_UnitCost,ID_SC) AS Net_Price, WHSE, ISNULL(VATable,0) AS VATable, ISNULL(PD_VATinc, 0) AS PD_VATinc, AD_Inv, " & _
                                    "         Size, Color, VATType, PD_ReorderQTY " & _
                                    " FROM    viewItem_Cost " & _
                                    " WHERE   ItemCode = '" & ItemCode & "' "
                ' SQL.FlushParams()
                'SQL.AddParam("@ItemCode", ItemCode)
                SQL.ReadQuery(query)
                SQL.GetQuery(query)
                Dim ctr As Integer = 1
            Dim ctrGroup As Integer = 0
                If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In SQL.SQLDS.Tables(0).Rows

                        If UOM = "" Then
                            UOM = row(3).ToString
                        End If
                        VATInc = row(7)
                        VAT = row(6)
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
                        If InvAccount = "" Then InvAccount = row(8).ToString
                        dgvItemList.Rows.Add(row(0).ToString, row(1).ToString, "", row(2).ToString, UOM, 0, QTY, _
                                  CDec(Price).ToString("N2"), CDec(Price * QTY).ToString("N2"), "", "0.00", CDec(VATAmt * QTY).ToString("N2"), _
                                 CDec((Price) * QTY).ToString("N2"), VAT, VATInc, row(5).ToString, InvAccount, GetAccntTitle(InvAccount), _
                                 "", "", row(11).ToString, row(9).ToString, row(10).ToString, row(12).ToString)

                        LoadVATType(dgvItemList.Rows.Count - 2)
                        LoadUOM(row(1).ToString, dgvItemList.Rows.Count - 2)
                        LoadColor(row(1).ToString, dgvItemList.Rows.Count - 2)
                        LoadSize(row(1).ToString, dgvItemList.Rows.Count - 2)
                        ctr += 1
                    Next
                End If
            End If
            LoadStock()
            LoadBarCode()
            Recompute(dgvItemList.RowCount - 2, chQTY.Index)




            'SQL.ReadQuery(query)
            'If SQL.SQLDR.Read Then

            '    If InvAccount = "" Then InvAccount = SQL.SQLDR("AD_Inv").ToString
            '    dgvItemList.Rows.Add(New String() {SQL.SQLDR("ItemGroup").ToString, SQL.SQLDR("ItemCode").ToString,
            '                                  SQL.SQLDR("ItemName").ToString,
            '                                  UOM,
            '                                  0, QTY,
            '                                  Format(Price, "#,###,###,###.00").ToString,
            '                                  Format(Price * QTY, "#,###,###,###.00").ToString,
            '                                  "",
            '                                  "0.00",
            '                                  Format(VATAmt * QTY, "#,###,###,###.00").ToString,
            '                                  Format((Price) * QTY, "#,###,###,###.00").ToString,
            '                                  SQL.SQLDR("VATable").ToString,
            '                                  SQL.SQLDR("PD_VATinc").ToString,
            '                                  SQL.SQLDR("WHSE").ToString,
            '                                  InvAccount, "", SQL.SQLDR("Size").ToString, SQL.SQLDR("Color").ToString})

            '    LoadVATType(dgvItemList.Rows.Count - 2)

            'End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub LoadUOM(ItemCode As String, ByVal SelectedIndex As Integer)
        Try
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
                dgvCB.Items.Add(SQL.SQLDR("UnitCode").ToString)
            End While
            dgvCB.Items.Add("")
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
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


    Public Sub LoadSuppliersItem(VCECode As String)
        Try
            Dim query, UOM, InvAccount, ItemCode As String
            Dim VAT As Boolean
            Dim Price, VATAmt, QTY As Decimal
            query = " SELECT  ItemGroup, ItemCode,  ItemName, ISNULL(PD_UOM,ItemUOM) AS PD_UOM,  " &
                                    "         ISNULL(PD_UnitCost,ID_SC) AS Net_Price, WHSE, ISNULL(VATable,0) AS VATable, ISNULL(PD_VATinc, 0) AS PD_VATinc, AD_Inv " &
                                    " FROM    viewItem_Cost " &
                                    " WHERE   VCECode = @VCECode "
            SQL.FlushParams()
            SQL.AddParam("@VCECode", VCECode)
            SQL.ReadQuery(query)
            While SQL.SQLDR.Read
                UOM = SQL.SQLDR("PD_UOM").ToString
                VAT = SQL.SQLDR("VATable").ToString
                Price = SQL.SQLDR("Net_Price")
                If VAT = True Then
                    VATAmt = (Price / 1.12) * 0.12
                Else
                    VATAmt = 0
                End If
                QTY = 0
                InvAccount = SQL.SQLDR("AD_Inv").ToString
                ItemCode = SQL.SQLDR("ItemCode").ToString
                dgvItemList.Rows.Add(New String() {SQL.SQLDR("ItemGroup").ToString, ItemCode, "",
                                                  SQL.SQLDR("ItemName").ToString,
                                                  UOM,
                                                  0, QTY,
                                                  Format(Price, "#,###,###,###.00").ToString,
                                                  Format(Price * QTY, "#,###,###,###.00").ToString,
                                                  "",
                                                  "0.00",
                                                  Format(VATAmt * QTY, "#,###,###,###.00").ToString,
                                                  Format(Price * QTY, "#,###,###,###.00").ToString,
                                                  SQL.SQLDR("VATable").ToString,
                                                  SQL.SQLDR("PD_VATinc").ToString,
                                                  SQL.SQLDR("WHSE").ToString,
                                                  InvAccount, ""})

                LoadVATType(dgvItemList.Rows.Count - 2)
                LoadUOM(ItemCode, dgvItemList.Rows.Count - 2)
                LoadColor(ItemCode, dgvItemList.Rows.Count - 2)
                LoadSize(ItemCode, dgvItemList.Rows.Count - 2)
            End While
            LoadStock()
            LoadBarCode()
            Recompute(dgvItemList.RowCount - 2, chQTY.Index)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub LoadVATType(Optional ByVal SelectedIndex As Integer = -1)
        Try
            ' If SelectedIndex <> -1 Then
            'Dim temp As String
            'Dim dgvCB As New DataGridViewComboBoxCell
            'dgvCB = dgvItemList.Item(chVATType.Index, SelectedIndex)
            'If IsNothing(dgvCB.Value) Then
            '    temp = ""
            'Else
            '    temp = dgvCB.Value.ToString
            'End If
            'dgvCB.Value = Nothing
            '' ADD ALL WHSEc
            'dgvCB.Items.Clear()
            'dgvCB.Items.Add("")
            'dgvCB.Items.Add("Exempt")
            'dgvCB.Items.Add("Zero-rated")
            'dgvCB.Items.Add("Services")
            'dgvCB.Items.Add("Capital Goods")
            'dgvCB.Items.Add("Other Than Capital Goods")
            'dgvCB.Value = temp



            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemList.Item(chVATType.Index, SelectedIndex)

            dgvCB.Items.Clear()
            dgvCB.Items.Add("")
            dgvCB.Items.Add("Exempt")
            dgvCB.Items.Add("Zero-rated")
            dgvCB.Items.Add("Services")
            dgvCB.Items.Add("Capital Goods")
            dgvCB.Items.Add("Other Than Capital Goods")
            '  End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
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
            If Val(dgvItemList.Item(chDiscount.Index, i).Value) <> 0 Then
                If IsNumeric(dgvItemList.Item(chDiscount.Index, i).Value) Then
                    b = b + Double.Parse(dgvItemList.Item(chDiscount.Index, i).Value)
                End If
            End If
        Next
        txtDiscount.Text = b.ToString("N2")


        ' COMPUTE VAT
        For i As Integer = 0 To dgvItemList.Rows.Count - 1
            If Val(dgvItemList.Item(chVATAmt.Index, i).Value) <> 0 Then
                If IsNumeric(dgvItemList.Item(chVATAmt.Index, i).Value) Then
                    c = c + Double.Parse(dgvItemList.Item(chVATAmt.Index, i).Value).ToString
                End If
            End If
        Next
        txtVAT.Text = c.ToString("N2")

        ' COMPUTE NET
        For i As Integer = 0 To dgvItemList.Rows.Count - 1
            If Val(dgvItemList.Item(chNetAmount.Index, i).Value) <> 0 Then
                If IsNumeric(dgvItemList.Item(chNetAmount.Index, i).Value) Then
                    d = d + Double.Parse(dgvItemList.Item(chNetAmount.Index, i).Value).ToString
                End If
            End If
        Next
        txtNet.Text = d.ToString("N2")
    End Sub

    Private Sub dgvItemMaster_RowsRemoved(sender As System.Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgvItemList.RowsRemoved
        If disableEvent = False Then
            ComputeTotal()
        End If
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("PO_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("PO")
            If f.transID <> "" Then
                TransID = f.transID
            End If

            LoadPO(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("PO_ADD") Then
            msgRestricted()
        Else
            ' CLEAR TRANSACTION RECORDS
            ClearText()
            TransID = ""
            PONo = ""

            ' SET REFERENCE ID TO 0
            CF_ID = 0
            PR_ID = 0
            SO_ID = 0

            ' LOAD MAINTENANCE VALUES
            LoadCostCenter()
            LoadChartOfAccount()
            LoadTerms()

            ' SET TOOLSTRIP BUTTONS
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbCancel.Enabled = False
            tsbApprove.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
            tsbPrint.Enabled = False
            tsbCopy.Enabled = True
            EnableControl(True)

            If TransAutoPO Then
                If cbPurchaseType.SelectedIndex = -1 Then
                    disableEvent = True
                    cbPurchaseType.SelectedIndex = 0
                    disableEvent = False
                End If
                txtTransNum.Text = GenerateTransNumPO()

            Else
                txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
            End If

        End If
    End Sub

    Public Function GenerateTransNumPO() As String
        Dim TransNum As String = ""
        ' GENERATE TRANS ID 
        Dim query As String
        query = " SELECT GlobalSeries, ISNULL(BranchCode,'All') AS BranchCode, ISNULL(BusinessCode,'All') AS BusinessCode,  " & _
                " ISNULL(TransGroup,0) AS TransGroup, ISNULL(Prefix,'') AS Prefix, ISNULL(Digits,6) AS Digits, " & _
                " ISNULL(StartRecord,1) AS StartRecord, LEN(ISNULL(Prefix,'')) + ISNULL(Digits,6) AS ID_Length " & _
                " FROM tblTransactionSetup LEFT JOIN tblTransactionDetails " & _
                " ON tblTransactionSetup.TransType = tblTransactionDetails.TransType " & _
                " WHERE tblTransactionSetup.TransType ='" & ModuleID & "' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            If SQL.SQLDR("GlobalSeries") = True Then
                If SQL.SQLDR("BranchCode") = "All" AndAlso SQL.SQLDR("BusinessCode") = "All" Then
                    Dim digits As Integer = SQL.SQLDR("Digits")
                    Dim prefix As String = SQL.SQLDR("Prefix")
                    Dim addPrefix As String = Strings.Left(cbPurchaseType.SelectedItem, 1)
                    query = " SELECT    ISNULL(MAX(SUBSTRING(" & ColumnPK & "," & prefix.Length + addPrefix.Length + 1 & "," & digits & "))+ 1,1) AS TransID  " & _
                            " FROM      tblPO  " & _
                            " WHERE     " & ColumnPK & " LIKE '" & prefix & "%' AND LEN(" & ColumnPK & ") = '" & digits + 1 & "'  AND PurchaseType = '" & cbPurchaseType.SelectedItem & "' "
                    SQL.ReadQuery(query)
                    If SQL.SQLDR.Read Then
                        TransNum = SQL.SQLDR("TransID")
                        For i As Integer = 1 To digits
                            TransNum = "0" & TransNum
                        Next
                        TransNum = prefix & addPrefix & Strings.Right(TransNum, digits)
                    End If
                End If
            End If
        End While
        Return TransNum
    End Function

    Private Sub btnSearchVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchVCE.Click
        Dim f As New frmVCE_Search
        f.Type = "Vendor"
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        LoadVCE_Info(f.VCECode)
        If txtVCECode.Text <> "" Then
            Dim query As String
            query = " SELECT  Terms" & _
                " FROM     tblVCE_Master " & _
                " WHERE    VCECode = '" & txtVCECode.Text & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                cbTerms.Text = SQL.SQLDR("Terms").ToString
            End If
        End If
        LoadCurrency()
        f.Dispose()
    End Sub

    Private Sub LoadVCE_Info(ByVal VCECode As String)
        Dim query As String
        query = " SELECT    VCECode, VCEName, TIN_No, Address, ContactNo, Contact_Person, Terms, Contact_Email " & _
                " FROM      ViewVCE_Details " & _
                " WHERE     VCECode ='" & VCECode & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
            txtTIN.Text = SQL.SQLDR("TIN_No").ToString
            txtAddress.Text = SQL.SQLDR("Address").ToString
            txtContact.Text = SQL.SQLDR("ContactNo").ToString
            txtPerson.Text = SQL.SQLDR("Contact_Person").ToString
            If txtEmail.Text = "" Then
                txtEmail.Text = SQL.SQLDR("Contact_Email").ToString
            End If
            cbTerms.SelectedItem = SQL.SQLDR("Terms").ToString
        End If
        LoadCurrency()
    End Sub

    Private Sub txtVCEName_Invalidated(sender As Object, e As System.Windows.Forms.InvalidateEventArgs) Handles txtVCEName.Invalidated

    End Sub

    Private Sub txtVCEName_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.Type = "Vendor"
            f.txtFilter.Text = txtVCEName.Text
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            LoadVCE_Info(f.VCECode)
            If txtVCECode.Text <> "" Then
                Dim query As String
                query = " SELECT  Terms" & _
                    " FROM     tblVCE_Master " & _
                    " WHERE    VCECode = '" & txtVCECode.Text & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    cbTerms.Text = SQL.SQLDR("Terms").ToString
                End If
            End If
            LoadCurrency()
            f.Dispose()
        End If
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("PO_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)

            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbCancel.Enabled = False
            tsbApprove.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
            tsbPrint.Enabled = False
            tsbCopy.Enabled = False
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        ' VALIDATE DATA INPUTS
        If DataValidated() Then
            If TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID(ColumnID, DBTable)
                    If TransAutoPO Then
                        If cbPurchaseType.SelectedIndex = -1 Then
                            disableEvent = True
                            cbPurchaseType.SelectedIndex = 0
                            disableEvent = False
                        End If
                        PONo = GenerateTransNumPO()

                    Else
                        PONo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    End If
                    txtTransNum.Text = PONo
                    SavePO()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    PONo = txtTransNum.Text
                    LoadPO(TransID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    UpdatePO()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    PONo = txtTransNum.Text
                    LoadPO(TransID)
                End If
            End If
        End If
    End Sub

    Private Function DataValidated() As Boolean
        If txtVCECode.Text = "" Then
            Msg("Please enter VCECode!", MsgBoxStyle.Exclamation)
            Return False
            'ElseIf cbPurchaseType.SelectedItem <> "Goods (Stock)" AndAlso cbGLAccount.SelectedIndex = -1 Then
            '    Msg("Please select default GL Account", MsgBoxStyle.Exclamation)
            '    Return False
        ElseIf dgvItemList.Rows.Count <= 1 Then
            Msg("There are no items on the list!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf validateDGV() = False Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function validateDGV() As Boolean
        Dim desc, account As String
        Dim net As Decimal
        Dim value As Boolean = True
        For Each row As DataGridViewRow In dgvItemList.Rows
            If IsNothing(row.Cells(chItemDesc.Index).Value) Then desc = "" Else desc = row.Cells(chItemDesc.Index).Value
            If Not IsNumeric(row.Cells(chNetAmount.Index).Value) Then net = 0 Else net = row.Cells(chNetAmount.Index).Value
            'If IsNothing(row.Cells(dgcAccountCode.Index).Value) Then account = "" Else account = row.Cells(dgcAccountCode.Index).Value

            If desc = "" And net > 0 Then
                Msg("There are line entry without item description, please check.", MsgBoxStyle.Exclamation)
                value = False
                Exit For
            ElseIf desc <> "" And net = 0 And cbPurchaseType.SelectedItem <> "Goods (Stock)" Then
                Msg("There are line entry without price, please check.", MsgBoxStyle.Exclamation)
                value = False
                Exit For
                'ElseIf desc <> "" And net <> 0 And account = "" Then
                '    Msg("There are line entry without account title, please check.", MsgBoxStyle.Exclamation)
                '    value = False
                '    Exit For
            End If
        Next
        Return value
    End Function

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("PO_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE  tblPO SET Status ='Cancelled' WHERE PO_No = @PO_No "
                        SQL.FlushParams()
                        PONo = txtTransNum.Text
                        SQL.AddParam("@PO_No", PONo)
                        SQL.ExecNonQuery(deleteSQL)


                        deleteSQL = " UPDATE  tblPR SET Status ='Active' WHERE TransID IN (" & PR_ID & ") "
                        SQL.FlushParams()
                        SQL.ExecNonQuery(deleteSQL)


                        Msg("Record cancelled successfully", MsgBoxStyle.Information)

                        tsbSearch.Enabled = True
                        tsbNew.Enabled = True
                        tsbEdit.Enabled = False
                        tsbSave.Enabled = False
                        tsbCancel.Enabled = False
                        tsbApprove.Enabled = False
                        tsbClose.Enabled = False
                        tsbPrevious.Enabled = False
                        tsbNext.Enabled = False
                        tsbExit.Enabled = True
                        tsbPrint.Enabled = True
                        tsbCopy.Enabled = False
                        EnableControl(False)

                        PONo = txtTransNum.Text
                        LoadPO(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "PO_No", PONo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub SavePO()
        Try
            If cbGLAccount.SelectedIndex = -1 Then
                AccntCode = ""
            Else
                AccntCode = GetAccntCode(cbGLAccount.SelectedItem)
            End If
            Dim CC As String = ""
            If cbCostCenter.SelectedIndex <> -1 Then
                CC = GetCCCode(cbCostCenter.SelectedItem)
            End If
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblPO(TransID, PO_No, BranchCode, BusinessCode, DatePO, VCECode, PurchaseType, DateDeliver, Terms, Remarks, " & _
                        "           CostCenter, GrossAmount, Discount, VATAmount, NetAmount, VATable, VATInclusive, PR_Ref, CF_Ref, SO_Ref, " & _
                        "           AccntCode, Currency, DeliverTo, WhoCreated, Status, EmailAddress, isPartial, isFixedAsset) " & _
                        " VALUES (@TransID, @PO_No, @BranchCode, @BusinessCode, @DatePO,  @VCECode, @PurchaseType, @DateDeliver, @Terms, @Remarks, " & _
                        "           @CostCenter, @GrossAmount, @Discount, @VATAmount, @NetAmount, @Vatable, @VATInclusive, @PR_Ref, @CF_Ref, @SO_Ref, " & _
                        "           @AccntCode, @Currency, @DeliverTo, @WhoCreated, @Status, @EmailAddress, @isPartial, @isFixedAsset) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@PO_No", PONo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DatePO", dtpDocDate.Value.Date)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@CostCenter", CC)
            SQL.AddParam("@Terms", IIf(cbTerms.SelectedIndex = -1, DBNull.Value, cbTerms.SelectedItem))
            SQL.AddParam("@PurchaseType", IIf(cbPurchaseType.SelectedIndex = -1, DBNull.Value, cbPurchaseType.SelectedItem))
            SQL.AddParam("@DateDeliver", dtpDelivery.Value.Date)
            SQL.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@GrossAmount", CDec(txtGross.Text))
            SQL.AddParam("@Discount", CDec(txtDiscount.Text))
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@NetAmount", CDec(txtNet.Text))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInclusive", chkVATInc.Checked)
            SQL.AddParam("@AccntCode", AccntCode)
            SQL.AddParam("@DeliverTo", cbDeliverTo.Text)
            SQL.AddParam("@PR_Ref", PR_ID)
            SQL.AddParam("@CF_Ref", CF_ID)
            SQL.AddParam("@SO_Ref", SO_ID)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.AddParam("@EmailAddress", txtEmail.Text)
            SQL.AddParam("@isPartial", chkPartialPayment.Checked)
            SQL.AddParam("@isFixedAsset", chkFixedAsset.Checked)
            ' If PO_App = True Then SQL.AddParam("@Status", "For Approval") Else SQL.AddParam("@Status", "Active")
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Active")
            SQL.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            Dim WHSE, VATType, VCECode, Size, Color As String
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chNetAmount.Index).Value Is Nothing AndAlso Not row.Cells(chItemDesc.Index).Value Is Nothing Then
                    If IsNothing(row.Cells(chWHSE.Index).Value) Then WHSE = "" Else WHSE = GetWHSECode(row.Cells(chWHSE.Index).Value.ToString)
                    If IsNothing(row.Cells(dgcAccountCode.Index).Value) Then AccntCode = "" Else AccntCode = row.Cells(dgcAccountCode.Index).Value
                    If IsNothing(row.Cells(chCustomerCode.Index).Value) Then VCECode = "" Else VCECode = row.Cells(chCustomerCode.Index).Value
                    If IsNothing(row.Cells(chVATType.Index).Value) Then VATType = "" Else VATType = row.Cells(chVATType.Index).Value
                    If IsNothing(row.Cells(chSize.Index).Value) Then Size = "" Else Size = row.Cells(chSize.Index).Value
                    If IsNothing(row.Cells(chColor.Index).Value) Then Color = "" Else Color = row.Cells(chColor.Index).Value

                    ' INSERT QUERY
                    insertSQL = " INSERT INTO " & _
                                " tblPO_Details(TransId, ItemGroup, ItemCode, Description, UOM, QTY, UnitPrice, AccntCode, " & _
                                "               GrossAmount, VATAmount, DiscountRate, Discount, NetAmount, VATable, VATinc, WHSE, VCECode,LineNum, WhoCreated, VATType, " & _
                                "               Size, Color, Reorder_Point) " & _
                                " VALUES(@TransId, @ItemGroup, @ItemCode, @Description, @UOM, @QTY, @UnitPrice,  @AccntCode, " & _
                                "          @GrossAmount, @VATAmount, @DiscountRate, @Discount, @NetAmount, @VATable, @VATinc, @WHSE, @VCECode, @LineNum, @WhoCreated, @VATType, " & _
                                "          @Size, @Color, @Reorder_Point) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    If cbPurchaseType.SelectedItem = "Goods (Stock)" Or cbPurchaseType.SelectedItem = "Non-Stock" Then
                        SQL.AddParam("@ItemGroup", row.Cells(chItemGroup.Index).Value)
                        SQL.AddParam("@ItemCode", row.Cells(chItemCode.Index).Value)
                        SQL.AddParam("@UOM", row.Cells(chUOM.Index).Value)
                        If IsNumeric(row.Cells(chUnitPrice.Index).Value) Then
                            SQL.AddParam("@UnitPrice", CDec(row.Cells(chUnitPrice.Index).Value))
                        Else
                            SQL.AddParam("@UnitPrice", 0)
                        End If
                    Else
                        SQL.AddParam("@ItemGroup", "")
                        SQL.AddParam("@ItemCode", "")
                        SQL.AddParam("@UOM", "")
                        If IsNumeric(row.Cells(chGross.Index).Value) Then
                            SQL.AddParam("@UnitPrice", CDec(row.Cells(chGross.Index).Value))
                        Else
                            SQL.AddParam("@UnitPrice", 0)
                        End If
                    End If
                    SQL.AddParam("@Description", row.Cells(chItemDesc.Index).Value.ToString)
                    SQL.AddParam("@QTY", CDec(row.Cells(chQTY.Index).Value))
                    If IsNumeric(row.Cells(chGross.Index).Value) Then SQL.AddParam("@GrossAmount", CDec(row.Cells(chGross.Index).Value)) Else SQL.AddParam("@GrossAmount", 0)
                    If IsNumeric(row.Cells(chDiscountRate.Index).Value) Then SQL.AddParam("@DiscountRate", CDec(row.Cells(chDiscountRate.Index).Value)) Else SQL.AddParam("@DiscountRate", 0)
                    If IsNumeric(row.Cells(chDiscount.Index).Value) Then SQL.AddParam("@Discount", CDec(row.Cells(chDiscount.Index).Value)) Else SQL.AddParam("@Discount", 0)
                    If IsNumeric(row.Cells(chVATAmt.Index).Value) Then SQL.AddParam("@VATAmount", CDec(row.Cells(chVATAmt.Index).Value)) Else SQL.AddParam("@VATAmount", 0)
                    If IsNumeric(row.Cells(chNetAmount.Index).Value) Then SQL.AddParam("@NetAmount", CDec(row.Cells(chNetAmount.Index).Value)) Else SQL.AddParam("@NetAmount", 0)
                    If IsNothing(row.Cells(chVAT.Index).Value) Then SQL.AddParam("@VATable", False) Else SQL.AddParam("@VATable", row.Cells(chVAT.Index).Value)
                    If IsNothing(row.Cells(chVATinc.Index).Value) Then SQL.AddParam("@VATinc", False) Else SQL.AddParam("@VATinc", row.Cells(chVATinc.Index).Value)
                    SQL.AddParam("@WHSE", WHSE)
                    SQL.AddParam("@AccntCode", AccntCode)
                    SQL.AddParam("@VCECode", VCECode)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.AddParam("@VATType", VATType)
                    SQL.AddParam("@Size", Size)
                    SQL.AddParam("@Color", Color)
                    If IsNumeric(row.Cells(chReorderingPoint.Index).Value) Then SQL.AddParam("@Reorder_Point", CDec(row.Cells(chReorderingPoint.Index).Value)) Else SQL.AddParam("@Reorder_Point", 0)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next

            If chkPartialPayment.Checked = True Then
                insertSQL = " INSERT INTO " & _
                         " tblPO_PartialPayment_Header(TransID, PaymentType, Method, DP_Type, DP_Terms_Percent, DP_Amount, PO_TotalAmount, NoOfMonths) " & _
                         " VALUES (@TransID, @PaymentType, @Method, @DP_Type, @DP_Terms_Percent, @DP_Amount, @PO_TotalAmount, @NoOfMonths) "
                SQL.FlushParams()
                SQL.AddParam("@TransID", TransID)
                SQL.AddParam("@PaymentType", IIf(frmPO_PartialPayment.cbType.SelectedItem = Nothing, "", frmPO_PartialPayment.cbType.SelectedItem))
                SQL.AddParam("@Method", IIf(frmPO_PartialPayment.cbType.SelectedItem = "Progressive", frmPO_PartialPayment.cbMethod.SelectedItem, ""))
                SQL.AddParam("@DP_Type", frmPO_PartialPayment.cbDownPayment_Type.SelectedItem)
                SQL.AddParam("@DP_Terms_Percent", IIf(frmPO_PartialPayment.cbDownPayment_Type.SelectedItem = "Percent", IIf(frmPO_PartialPayment.txtDownpayment_Percent.Text = "", 0, CDec(frmPO_PartialPayment.txtDownpayment_Percent.Text)), 0))
                SQL.AddParam("@DP_Amount", IIf(frmPO_PartialPayment.txtDownpayment_Amount.Text = "", 0, CDec(frmPO_PartialPayment.txtDownpayment_Amount.Text)))
                SQL.AddParam("@NoOfMonths", IIf(frmPO_PartialPayment.cbType.SelectedItem = Nothing, 0, IIf(frmPO_PartialPayment.txtNoOfMonths.Text = "", 0, frmPO_PartialPayment.txtNoOfMonths.Text)))
                SQL.AddParam("@PO_TotalAmount", CDec(txtNet.Text))
                SQL.ExecNonQuery(insertSQL)

                For Each row As DataGridViewRow In frmPO_PartialPayment.dgvMonthly.Rows
                    If Not row.Cells(frmPO_PartialPayment.dgcM_Terms.Index).Value Is Nothing AndAlso Not row.Cells(frmPO_PartialPayment.dgcT_Amount.Index).Value Is Nothing Then
                        ' INSERT QUERY
                        insertSQL = " INSERT INTO " & _
                                    " tblPO_PartialPayment_Details(TransId, Des_Terms, Value) " & _
                                    " VALUES(@TransId, @Des_Terms, @Value) "
                        SQL.FlushParams()
                        SQL.AddParam("@TransID", TransID)
                        SQL.AddParam("@Des_Terms", row.Cells(frmPO_PartialPayment.dgcM_Terms.Index).Value)
                        SQL.AddParam("@Value", CDec(row.Cells(frmPO_PartialPayment.dgcT_Amount.Index).Value).ToString())
                        SQL.ExecNonQuery(insertSQL)
                    End If
                Next
                frmPO_PartialPayment.Close()
                frmPO_PartialPayment.Dispose()
            End If

            UpdatePR(PR_ID)  ' UPDATE PR STATUS
            UpdateCF(CF_ID, txtVCECode.Text)  ' UPDATE CF DETAILS STATUS
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "PO_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub UpdatePR(ByVal ID As String)
        If ID <> "" Then
            Dim updateSQL As String
            updateSQL = "UPDATE tblPR SET Status ='Closed' WHERE TransID IN (" & ID & ") "
            SQL.ExecNonQuery(updateSQL)
        End If
    End Sub

    Private Sub UpdateCF(ByVal ID As Integer, ByVal Code As String)
        If ID <> 0 Then
            Dim updateSQL As String
            updateSQL = " UPDATE tblCF_Details SET Status = 'Closed'  " & _
                        " WHERE TransID = '" & ID & "' " & _
                        " AND     (CASE WHEN ApproveSP = 'Supplier 1' THEN S1code " & _
                        "              WHEN ApproveSP = 'Supplier 2' THEN S2code " & _
                        "              WHEN ApproveSP = 'Supplier 3' THEN S3code " & _
                        "              WHEN ApproveSP = 'Supplier 4' THEN S4code " & _
                        "         END)  ='" & Code & "' "
            SQL.ExecNonQuery(updateSQL)
        End If
    End Sub


    Private Sub UpdatePO()
        Try
            activityStatus = True
            If cbGLAccount.SelectedIndex = -1 Then
                AccntCode = ""
            Else
                AccntCode = GetAccntCode(cbGLAccount.SelectedItem)
            End If
            Dim CC As String = ""
            If cbCostCenter.SelectedIndex <> -1 Then
                CC = GetCCCode(cbCostCenter.SelectedItem)
            End If
            ' UPDATE PR HEADER
            Dim updateSQL, deleteSQL, insertSQL As String
            updateSQL = " UPDATE tblPO " & _
                        " SET    PO_No = @PO_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, DatePO = @DatePO, Terms = @Terms, " & _
                        "        VCECode = @VCECode, PurchaseType = @PurchaseType, DateDeliver = @DateDeliver, Remarks = @Remarks, CostCenter = @CostCenter, " & _
                        "        GrossAmount = @GrossAmount, Discount = @Discount, VATAmount = @VATAmount, NetAmount = @NetAmount, " & _
                        "        VATable = @VATable, VATInclusive = @VATInclusive, AccntCode = @AccntCode, DeliverTo = @DeliverTo, Currency = @Currency, isFixedAsset = @isFixedAsset," & _
                        "        PR_Ref = @PR_Ref, CF_Ref = @CF_Ref, SO_Ref = @SO_Ref, WhoModified = @WhoModified, DateModified = GETDATE() , EmailAddress = @EmailAddress, isPartial = @isPartial " & _
                        " WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@PO_No", PONo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DatePO", dtpDocDate.Value.Date)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@CostCenter", CC)
            SQL.AddParam("@Terms", IIf(cbTerms.Text = "", DBNull.Value, cbTerms.Text))
            SQL.AddParam("@PurchaseType", IIf(cbPurchaseType.SelectedIndex = -1, DBNull.Value, cbPurchaseType.SelectedItem))
            SQL.AddParam("@DateDeliver", dtpDelivery.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@GrossAmount", CDec(txtGross.Text))
            SQL.AddParam("@Discount", CDec(txtDiscount.Text))
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@NetAmount", CDec(txtNet.Text))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInclusive", chkVATInc.Checked)
            SQL.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL.AddParam("@AccntCode", AccntCode)
            SQL.AddParam("@DeliverTo", cbDeliverTo.Text)
            SQL.AddParam("@PR_Ref", PR_ID)
            SQL.AddParam("@CF_Ref", CF_ID)
            SQL.AddParam("@SO_Ref", SO_ID)
            SQL.AddParam("@WhoModified", UserID)
            SQL.AddParam("@EmailAddress", txtEmail.Text)
            SQL.AddParam("@isPartial", chkPartialPayment.Checked)
            SQL.AddParam("@isFixedAsset", chkFixedAsset.Checked)
            SQL.ExecNonQuery(updateSQL)


            ' DELETE PR DETAILS
            deleteSQL = " DELETE FROM tblPO_Details WHERE TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.ExecNonQuery(deleteSQL)

            ' INSERT PR DETAILS
            Dim line As Integer = 1
            Dim WHSE, VATType, VCECode, Size, Color As String
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chNetAmount.Index).Value Is Nothing AndAlso Not row.Cells(chItemDesc.Index).Value Is Nothing Then
                    If IsNothing(row.Cells(chWHSE.Index).Value) Then WHSE = "" Else WHSE = GetWHSECode(row.Cells(chWHSE.Index).Value.ToString)
                    If IsNothing(row.Cells(dgcAccountCode.Index).Value) Then AccntCode = "" Else AccntCode = row.Cells(dgcAccountCode.Index).Value
                    If IsNothing(row.Cells(chCustomerCode.Index).Value) Then VCECode = "" Else VCECode = row.Cells(chCustomerCode.Index).Value
                    If IsNothing(row.Cells(chVATType.Index).Value) Then VATType = "" Else VATType = row.Cells(chVATType.Index).Value
                    If IsNothing(row.Cells(chSize.Index).Value) Then Size = "" Else Size = row.Cells(chSize.Index).Value
                    If IsNothing(row.Cells(chColor.Index).Value) Then Color = "" Else Color = row.Cells(chColor.Index).Value

                    ' INSERT QUERY
                    insertSQL = " INSERT INTO " & _
                                " tblPO_Details(TransId, ItemGroup, ItemCode, Description, UOM, QTY, UnitPrice, AccntCode, " & _
                                "               GrossAmount, VATAmount, DiscountRate, Discount, NetAmount, VATable, VATinc, WHSE, VCECode,LineNum, WhoCreated, VATType, " & _
                                "               Size, Color, Reorder_Point) " & _
                                " VALUES(@TransId, @ItemGroup, @ItemCode, @Description, @UOM, @QTY, @UnitPrice,  @AccntCode, " & _
                                "          @GrossAmount, @VATAmount, @DiscountRate, @Discount, @NetAmount, @VATable, @VATinc, @WHSE, @VCECode, @LineNum, @WhoCreated, @VATType, " & _
                                "          @Size, @Color, @Reorder_Point) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    If cbPurchaseType.SelectedItem = "Goods (Stock)" Or cbPurchaseType.SelectedItem = "Non-Stock" Then
                        SQL.AddParam("@ItemGroup", row.Cells(chItemGroup.Index).Value)
                        SQL.AddParam("@ItemCode", row.Cells(chItemCode.Index).Value)
                        SQL.AddParam("@UOM", row.Cells(chUOM.Index).Value)
                        If IsNumeric(row.Cells(chUnitPrice.Index).Value) Then
                            SQL.AddParam("@UnitPrice", CDec(row.Cells(chUnitPrice.Index).Value))
                        Else
                            SQL.AddParam("@UnitPrice", 0)
                        End If
                    Else
                        SQL.AddParam("@ItemGroup", "")
                        SQL.AddParam("@ItemCode", "")
                        SQL.AddParam("@UOM", "")
                        If IsNumeric(row.Cells(chGross.Index).Value) Then
                            SQL.AddParam("@UnitPrice", CDec(row.Cells(chGross.Index).Value))
                        Else
                            SQL.AddParam("@UnitPrice", 0)
                        End If
                    End If
                    SQL.AddParam("@Description", row.Cells(chItemDesc.Index).Value.ToString)
                    SQL.AddParam("@QTY", CDec(row.Cells(chQTY.Index).Value))
                    If IsNumeric(row.Cells(chGross.Index).Value) Then SQL.AddParam("@GrossAmount", CDec(row.Cells(chGross.Index).Value)) Else SQL.AddParam("@GrossAmount", 0)
                    If IsNumeric(row.Cells(chDiscountRate.Index).Value) Then SQL.AddParam("@DiscountRate", CDec(row.Cells(chDiscountRate.Index).Value)) Else SQL.AddParam("@DiscountRate", 0)
                    If IsNumeric(row.Cells(chDiscount.Index).Value) Then SQL.AddParam("@Discount", CDec(row.Cells(chDiscount.Index).Value)) Else SQL.AddParam("@Discount", 0)
                    If IsNumeric(row.Cells(chVATAmt.Index).Value) Then SQL.AddParam("@VATAmount", CDec(row.Cells(chVATAmt.Index).Value)) Else SQL.AddParam("@VATAmount", 0)
                    If IsNumeric(row.Cells(chNetAmount.Index).Value) Then SQL.AddParam("@NetAmount", CDec(row.Cells(chNetAmount.Index).Value)) Else SQL.AddParam("@NetAmount", 0)
                    If IsNothing(row.Cells(chVAT.Index).Value) Then SQL.AddParam("@VATable", False) Else SQL.AddParam("@VATable", row.Cells(chVAT.Index).Value)
                    If IsNothing(row.Cells(chVATinc.Index).Value) Then SQL.AddParam("@VATinc", False) Else SQL.AddParam("@VATinc", row.Cells(chVATinc.Index).Value)
                    SQL.AddParam("@WHSE", WHSE)
                    SQL.AddParam("@AccntCode", AccntCode)
                    SQL.AddParam("@VCECode", VCECode)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.AddParam("@VATType", VATType)
                    SQL.AddParam("@Size", Size)
                    SQL.AddParam("@Color", Color)
                    If IsNumeric(row.Cells(chReorderingPoint.Index).Value) Then SQL.AddParam("@Reorder_Point", CDec(row.Cells(chReorderingPoint.Index).Value)) Else SQL.AddParam("@Reorder_Point", 0)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next

            If chkPartialPayment.Checked = True Then
                ' DELETE PR DETAILS
                deleteSQL = " DELETE FROM tblPO_PartialPayment_Header WHERE TransID = @TransID "
                SQL.FlushParams()
                SQL.AddParam("@TransID", TransID)
                SQL.ExecNonQuery(deleteSQL)

                insertSQL = " INSERT INTO " & _
                          " tblPO_PartialPayment_Header(TransID, PaymentType, Method, DP_Type, DP_Terms_Percent, DP_Amount, PO_TotalAmount, NoOfMonths) " & _
                          " VALUES (@TransID, @PaymentType, @Method, @DP_Type, @DP_Terms_Percent, @DP_Amount, @PO_TotalAmount, @NoOfMonths) "
                SQL.FlushParams()
                SQL.AddParam("@TransID", TransID)
                SQL.AddParam("@PaymentType", IIf(frmPO_PartialPayment.cbType.SelectedItem = Nothing, "", frmPO_PartialPayment.cbType.SelectedItem))
                SQL.AddParam("@Method", IIf(frmPO_PartialPayment.cbType.SelectedItem = "Progressive", frmPO_PartialPayment.cbMethod.SelectedItem, ""))
                SQL.AddParam("@DP_Type", frmPO_PartialPayment.cbDownPayment_Type.SelectedItem)
                SQL.AddParam("@DP_Terms_Percent", IIf(frmPO_PartialPayment.cbDownPayment_Type.SelectedItem = "Percent", IIf(frmPO_PartialPayment.txtDownpayment_Percent.Text = "", 0, CDec(frmPO_PartialPayment.txtDownpayment_Percent.Text)), 0))
                SQL.AddParam("@DP_Amount", IIf(frmPO_PartialPayment.txtDownpayment_Amount.Text = "", 0, CDec(frmPO_PartialPayment.txtDownpayment_Amount.Text)))
                SQL.AddParam("@NoOfMonths", IIf(frmPO_PartialPayment.cbType.SelectedItem = Nothing, 0, IIf(frmPO_PartialPayment.txtNoOfMonths.Text = "", 0, frmPO_PartialPayment.txtNoOfMonths.Text)))
                SQL.AddParam("@PO_TotalAmount", CDec(txtNet.Text))
                SQL.ExecNonQuery(insertSQL)


                ' DELETE PR DETAILS
                deleteSQL = " DELETE FROM tblPO_PartialPayment_Details WHERE TransID = @TransID "
                SQL.FlushParams()
                SQL.AddParam("@TransID", TransID)
                SQL.ExecNonQuery(deleteSQL)

                For Each row As DataGridViewRow In frmPO_PartialPayment.dgvMonthly.Rows
                    If Not row.Cells(frmPO_PartialPayment.dgcM_Terms.Index).Value Is Nothing AndAlso Not row.Cells(frmPO_PartialPayment.dgcT_Amount.Index).Value Is Nothing Then
                        ' INSERT QUERY
                        insertSQL = " INSERT INTO " & _
                                    " tblPO_PartialPayment_Details(TransId, Des_Terms, Value) " & _
                                    " VALUES(@TransId, @Des_Terms, @Value) "
                        SQL.FlushParams()
                        SQL.AddParam("@TransID", TransID)
                        SQL.AddParam("@Des_Terms", row.Cells(frmPO_PartialPayment.dgcM_Terms.Index).Value)
                        SQL.AddParam("@Value", CDec(row.Cells(frmPO_PartialPayment.dgcT_Amount.Index).Value).ToString())
                        SQL.ExecNonQuery(insertSQL)
                    End If
                Next
                frmPO_PartialPayment.Close()
                frmPO_PartialPayment.Dispose()
            End If

            UpdatePR(PR_ID)  ' UPDATE PR STATUS
            UpdateCF(CF_ID, txtVCECode.Text)  ' UPDATE CF DETAILS STATUS
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "PO_No", PONo, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("PO", TransID, "PO Printout")
        f.Dispose()
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If PONo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblPO  WHERE PO_No < '" & PONo & "' ORDER BY PO_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadPO(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If PONo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblPO  WHERE PO_No > '" & PONo & "' ORDER BY PO_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadPO(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        ' Toolstrip Buttons
        If TransID = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbApprove.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            tsbEdit.Enabled = True
            tsbCancel.Enabled = True
            tsbApprove.Enabled = True
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbPrint.Enabled = True
            LoadPO(TransID)
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

    Private Sub tsbCopyPR_Click(sender As System.Object, e As System.EventArgs) Handles tsbCopyPR.Click
        Dim f As New frmLoadTransactions
        Dim transidPR As String = ""
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.chkBatch.Visible = True
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("PRQ")
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
            LoadPR(transidPR)
        Else
            If f.transID <> "" Then
                LoadPR(f.transID)
            End If
        End If

        f.Dispose()
    End Sub

    Private Sub LoadPR(ByVal TransNum As String)
        Dim query, Currency As String
        query = " SELECT   TransID, PR_No, DatePR, VCECode, PurchaseType,  " & _
                "          Remarks, DateNeeded, RequestedBy, Status, AccntCode " & _
                " FROM     tblPR " & _
                " WHERE    TransID = '" & TransNum & "' " & _
                " ORDER BY TransID "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            PR_ID = SQL.SQLDR("TransID")
            PR_ID = TransNum
            txtPRNo.Text = SQL.SQLDR("PR_No").ToString
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            cbPurchaseType.SelectedItem = SQL.SQLDR("PurchaseType").ToString
            txtStatus.Text = "Open"
            'dtpDocDate.Text = SQL.SQLDR("DatePR").ToString
            'cbDeliverTo.Text = SQL.SQLDR("RequestedBy").ToString
            'cbGLAccount.SelectedItem = GetAccntTitle(SQL.SQLDR("AccntCode").ToString)
            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            Currency = "PHP"
            cbCurrency.Items.Clear()
            For Each item In LoadVCECurrency(txtVCECode.Text)
                cbCurrency.Items.Add(item)
            Next
            If cbCurrency.Items.Count = 0 Then
                cbCurrency.Items.Add(BaseCurrency)
            End If
            cbCurrency.SelectedItem = Currency

            query = " SELECT    ItemGroup, '', ItemCode, Description, UOM,SUM(QTY) AS QTY, tblPR_Details.AccntCode, tblCOA_Master.AccountTitle  " & _
                    " FROM      tblPR_Details " & _
                    " LEFT JOIN " & _
                    " tblCOA_Master ON " & _
                    " tblCOA_Master.AccountCode = tblPR_Details.AccntCode " & _
                    " WHERE     tblPR_Details.TransID IN (" & PR_ID & ") AND QTY > 0 " & _
                    "  GROUP BY  ItemGroup,  ItemCode, Description, UOM, tblPR_Details.AccntCode, tblCOA_Master.AccountTitle "
            dgvItemList.Rows.Clear()
            SQL.GetQuery(query)
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                If cbPurchaseType.SelectedItem = "Goods (Stock)" Then
                    For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                        LoadItem(row.Item(0), row.Item(2), row.Item(4), row.Item(5))
                    Next
                Else
                    For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                        dgvItemList.Rows.Add(New String() {row.Item(0), row.Item(1),
                                                      row.Item(2),
                                                       row.Item(3),
                                                       0, row.Item(4),
                                                     "0.00",
                                                      "0.00",
                                                      "",
                                                      "0.00",
                                                     "0.00",
                                                      "0.00",
                                                      "True",
                                                      "True",
                                                      "",
                                                       row.Item(5), row.Item(6)})
                        LoadVATType(dgvItemList.Rows.Count - 2)
                        LoadUOM(row(2).ToString, dgvItemList.Rows.Count - 2)
                        LoadColor(row(2).ToString, dgvItemList.Rows.Count - 2)
                        LoadSize(row(2).ToString, dgvItemList.Rows.Count - 2)
                    Next
                End If
            End If
            LoadStock()
            LoadBarCode()
            ComputeTotal()

        End If
    End Sub

    Private Sub LoadCostCenter()
        Dim query As String
        query = " SELECT Description FROM tblCC "
        SQL.ReadQuery(query)
        cbCostCenter.Items.Clear()
        While SQL.SQLDR.Read
            cbCostCenter.Items.Add(SQL.SQLDR("Description").ToString)
        End While
    End Sub

    Private Sub LoadCF(ByVal TransNum As String, ByVal SupplierCode As String)
        Dim query, itemcode As String
        query = " SELECT   tblCF.TransID, CF_No, PurchaseType, CostCenter, Requestedby, tblCF.Remarks,  tblCF.Status  " & _
                " FROM     tblCF " & _
                " WHERE     TransID = @TransID  "
        SQL.FlushParams()
        SQL.AddParam("@TransID", TransNum)
        SQL.ReadQuery(query)
        SQL.FlushParams()
        If SQL.SQLDR.Read Then
            CF_ID = SQL.SQLDR("TransID")
            txtCFNo.Text = SQL.SQLDR("CF_No").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            cbPurchaseType.SelectedItem = SQL.SQLDR("PurchaseType").ToString
            txtStatus.Text = "Open"
            cbDeliverTo.Text = SQL.SQLDR("RequestedBy").ToString
            cbCostCenter.SelectedItem = GetCCName(SQL.SQLDR("CostCenter").ToString)
            query = " SELECT TransID, CFdetails.ItemGroup, CFdetails.ItemCode, Description, UOM, QTY, SupplierCode, UnitPrice, VAT, VATInc, GrossAmount, VATAmount, TotalAmount, AD_Inv " & _
                    " FROM " & _
                    " ( " & _
                    "   SELECT    TransID, ItemGroup, ItemCode, Description, UOM, QTY, " & _
                    "           CASE WHEN ApproveSP ='Supplier 1' THEN S1code " & _
                    "                           WHEN ApproveSP ='Supplier 2' THEN S2code " & _
                    "                           WHEN ApproveSP ='Supplier 3' THEN S3code " & _
                    "                           WHEN ApproveSP ='Supplier 4' THEN S4code " & _
                    "                      END AS SupplierCode, " & _
                    "               CASE WHEN ApproveSP ='Supplier 1' THEN S1price " & _
                    "                           WHEN ApproveSP ='Supplier 2' THEN S2price " & _
                    "                           WHEN ApproveSP ='Supplier 3' THEN S3price " & _
                    "                           WHEN ApproveSP ='Supplier 4' THEN S4price " & _
                    "               END AS UnitPrice, " & _
                    "               CASE WHEN ApproveSP ='Supplier 1' THEN S1vat " & _
                    "                           WHEN ApproveSP ='Supplier 2' THEN S2vat " & _
                    "                           WHEN ApproveSP ='Supplier 3' THEN S3vat " & _
                    "                           WHEN ApproveSP ='Supplier 4' THEN S4vat " & _
                    "               END AS VAT, " & _
                    "               CASE WHEN ApproveSP ='Supplier 1' THEN S1vatInc " & _
                    "                           WHEN ApproveSP ='Supplier 2' THEN S2vatInc " & _
                    "                           WHEN ApproveSP ='Supplier 3' THEN S3vatInc " & _
                    "                           WHEN ApproveSP ='Supplier 4' THEN S4vatInc " & _
                    "               END AS VATInc, GrossAmount, VATAmount, TotalAmount " & _
                    "   FROM tblCF_Details " & _
                    " ) AS CFdetails " & _
                    " LEFT JOIN tblVCE_Master " & _
                    " ON        tblVCE_Master.VCECode = CFdetails.SupplierCode " & _
                    " LEFT JOIN tblItem_Master " & _
                    " ON        tblItem_Master.ItemCode = CFdetails.ItemCode " & _
                    " WHERE     CFdetails.TransID = @TransID AND SupplierCode = @SupplierCode AND tblVCE_Master.Status ='Active' "
            dgvItemList.Rows.Clear()
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransNum)
            SQL.AddParam("@SupplierCode", SupplierCode)
            SQL.ReadQuery(query)
            While SQL.SQLDR.Read
                itemcode = SQL.SQLDR("ItemCode").ToString
                dgvItemList.Rows.Add(New String() {SQL.SQLDR("ItemGroup").ToString, SQL.SQLDR("ItemCode").ToString, "",
                                                            SQL.SQLDR("Description").ToString, SQL.SQLDR("UOM").ToString, "0", SQL.SQLDR("QTY").ToString,
                                                             CDec(SQL.SQLDR("UnitPrice")).ToString("N2"), CDec(SQL.SQLDR("GrossAmount")).ToString("N2"), "0", ".00",
                                                            CDec(SQL.SQLDR("VATAmount")).ToString("N2"), CDec(SQL.SQLDR("TotalAmount")).ToString("N2"), SQL.SQLDR("VAT").ToString,
                                                            SQL.SQLDR("VATInc").ToString, "", SQL.SQLDR("AD_Inv").ToString, ""})
                LoadVATType(dgvItemList.Rows.Count - 2)
                LoadUOM(itemcode, dgvItemList.Rows.Count - 2)
                LoadColor(itemcode, dgvItemList.Rows.Count - 2)
                LoadSize(itemcode, dgvItemList.Rows.Count - 2)
            End While
            SQL.FlushParams()
            LoadStock()
            LoadBarCode()
            ComputeTotal()
        End If
    End Sub

    Private Sub frmPO_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            ElseIf e.KeyCode = Keys.Right Then
                If tsbNext.Enabled = True Then tsbNext.PerformClick()
            ElseIf e.KeyCode = Keys.P Then
                If tsbPrint.Enabled = True Then tsbPrint.PerformClick()
            ElseIf e.KeyCode = Keys.R Then
                If tsbReports.Enabled = True Then tsbReports.PerformClick()
            ElseIf e.KeyCode = Keys.C Then
                If tsbReports.Enabled = True Then tsbCopy.ShowDropDown()
            ElseIf e.KeyCode = Keys.A Then
                If tsbApprove.Enabled = True Then tsbApprove.PerformClick()
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

    Private Sub TestToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles TestToolStripMenuItem1.Click
        Dim f As New frmReport_Filter
        f.Report = "PO List"
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub cbPurchaseType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbPurchaseType.SelectedIndexChanged
        If disableEvent = False Then
            RefreshDatagrid()
            If TransAutoPO Then
                txtTransNum.Text = GenerateTransNumPO()
            End If

        End If
    End Sub

    Private Sub RefreshDatagrid()
        If cbPurchaseType.SelectedIndex <> -1 Then
            If cbPurchaseType.SelectedItem = "Goods (Stock)" Then
                dgvItemList.Columns(chItemGroup.Index).Visible = False
                dgvItemList.Columns(chItemCode.Index).Visible = True
                dgvItemList.Columns(chUOM.Index).Visible = True
                dgvItemList.Columns(chUnitPrice.Index).Visible = True
                dgvItemList.Columns(chQTY.Index).Visible = True
                dgvItemList.Columns(dgcAccountTitle.Index).Visible = False
                dgvItemList.Columns(chGross.Index).ReadOnly = True
                ' dgvItemList.Columns(chUOM.Index).ReadOnly = True
                lblGL.Visible = False
                cbGLAccount.Visible = False
            ElseIf cbPurchaseType.SelectedItem = "Non-Stock" Then
                dgvItemList.Columns(chItemGroup.Index).Visible = False
                dgvItemList.Columns(chItemCode.Index).Visible = True
                dgvItemList.Columns(chUOM.Index).Visible = True
                dgvItemList.Columns(chUnitPrice.Index).Visible = True
                dgvItemList.Columns(chQTY.Index).Visible = True
                lblGL.Visible = False
                cbGLAccount.Visible = False
                dgvItemList.Columns(dgcAccountTitle.Index).Visible = True
                dgvItemList.Columns(chGross.Index).ReadOnly = False
                '  dgvItemList.Columns(chUOM.Index).ReadOnly = False
            ElseIf cbPurchaseType.SelectedItem = "Services" Then
                dgvItemList.Columns(chItemGroup.Index).Visible = False
                dgvItemList.Columns(chItemCode.Index).Visible = False
                dgvItemList.Columns(chUOM.Index).Visible = False
                dgvItemList.Columns(chUnitPrice.Index).Visible = False
                dgvItemList.Columns(chQTY.Index).Visible = False
                lblGL.Visible = False
                cbGLAccount.Visible = False
                dgvItemList.Columns(dgcAccountTitle.Index).Visible = True
                dgvItemList.Columns(chGross.Index).ReadOnly = False
            End If

        End If
    End Sub

    Private Control As Boolean = False

    Private Sub cb_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles cbGLAccount.KeyDown, cbPurchaseType.KeyDown
        Control = False
        If e.KeyCode = Keys.ControlKey Then
            Control = True
        End If
    End Sub

    Private Sub cbGLAccount_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbGLAccount.KeyPress, cbPurchaseType.KeyPress
        If Control = True Then
            e.Handled = True
        End If
    End Sub

    Private Sub chkVAT_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkVAT.CheckedChanged
        For Each row As DataGridViewRow In dgvItemList.Rows
            If Not row.Cells(chGross.Index).Value Is Nothing Then
                If chkVAT.CheckState = CheckState.Checked Then
                    row.Cells(chVAT.Index).Value = True
                Else
                    row.Cells(chVAT.Index).Value = False
                End If
            End If


            Recompute(row.Index, chQTY.Index)
        Next
        ComputeTotal()
    End Sub

    Private Sub PRWithoutPOToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PRWithoutPOToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("PO_Unserved", "", "Summary")
        f.Dispose()
    End Sub

    Private Sub tsbApprove_Click(sender As System.Object, e As System.EventArgs) Handles tsbApprove.Click
        If Not AllowAccess("PO_APPROVAL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to approve this transaction?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim updateSQL As String
                        updateSQL = " UPDATE  tblPO SET Status ='Active' WHERE PO_No = @PO_No "
                        SQL.FlushParams()
                        PONo = txtTransNum.Text
                        SQL.AddParam("@PO_No", PONo)
                        SQL.ExecNonQuery(updateSQL)
                        Msg("Record Approved successfully", MsgBoxStyle.Information)

                        tsbSearch.Enabled = True
                        tsbNew.Enabled = True
                        tsbEdit.Enabled = False
                        tsbSave.Enabled = False
                        tsbCancel.Enabled = False
                        tsbApprove.Enabled = False
                        tsbClose.Enabled = False
                        tsbPrevious.Enabled = False
                        tsbNext.Enabled = False
                        tsbExit.Enabled = True
                        tsbPrint.Enabled = True
                        tsbCopy.Enabled = False
                        EnableControl(False)

                        PONo = txtTransNum.Text
                        LoadPO(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "APPROVE", "PO_No", PONo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbCopyCF_Click(sender As System.Object, e As System.EventArgs) Handles tsbCopyCF.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("Sub CF")
        LoadCF(f.transID, f.itemCode)
        LoadVCE_Info(f.itemCode)
        LoadCurrency()
        f.Dispose()
    End Sub

    Dim eColIndex As Integer = 0
    Private Sub dgvItemList_CurrentCellDirtyStateChanged(sender As System.Object, e As System.EventArgs) Handles dgvItemList.CurrentCellDirtyStateChanged
        If dgvItemList.SelectedCells.Count > 0 AndAlso (dgvItemList.SelectedCells(0).ColumnIndex = chVAT.Index OrElse dgvItemList.SelectedCells(0).ColumnIndex = chVATinc.Index) Then
            If dgvItemList.SelectedCells(0).RowIndex <> -1 Then
                Recompute(dgvItemList.SelectedCells(0).RowIndex, dgvItemList.SelectedCells(0).ColumnIndex)
                dgvItemList.SelectedCells(0).Selected = False
                dgvItemList.EndEdit()
            End If
        ElseIf eColIndex = chVATType.Index And TypeOf (dgvItemList.CurrentRow.Cells(chVATType.Index)) Is DataGridViewComboBoxCell Then
            dgvItemList.EndEdit()
        End If
    End Sub

    Private Sub btnApplyRate_Click(sender As System.Object, e As System.EventArgs) Handles btnApplyRate.Click
        If IsNumeric(txtDiscount.Text) Then
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not IsNothing(row.Cells(chItemCode.Index).Value) Then
                    row.Cells(chDiscountRate.Index).Value = txtDiscountRate.Text
                    Recompute(row.Index, chDiscountRate.Index)
                End If
            Next
        End If
    End Sub

    Private Sub txtVCEName_Resize(sender As Object, e As System.EventArgs) Handles txtVCEName.Resize

    End Sub

    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged

    End Sub

    Private Sub dgvItemList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellContentClick

    End Sub

    Private Sub FromSOToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromSOToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("PO_SO")
        SO_ID = 0
        If f.transID <> "" Then
            LoadSO(f.transID)
        End If

        f.Dispose()
    End Sub


    Private Sub LoadSO(ByVal ID As String)
        Dim query As String
        query = " SELECT     TransID, SO_No, VCECode, DateSO, DateDeliver, Remarks, StaggardDelivery, " & _
                "            ISNULL(GrossAmount,0) AS GrossAmount, ISNULL(VATAmount,0) AS VATAmount, ISNULL(Discount,0) AS Discount, ISNULL(NetAmount,0) AS NetAmount,  " & _
                "            VATable, VATInclusive, Status, ReferenceNo, ISNULL(SQ_Ref,0) AS SQ_Ref " & _
                " FROM       tblSO " & _
                " WHERE      TransId = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            SO_ID = SQL.SQLDR("TransID").ToString
            SONo = SQL.SQLDR("SO_No").ToString
            txtSONo.Text = SONo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            dtpDocDate.Text = SQL.SQLDR("DateSO").ToString
            dtpDelivery.Text = SQL.SQLDR("DateDeliver").ToString
            txtGross.Text = CDec(SQL.SQLDR("GrossAmount").ToString).ToString("N2")
            txtVAT.Text = CDec(SQL.SQLDR("VATAmount").ToString).ToString("N2")
            txtDiscount.Text = CDec(SQL.SQLDR("Discount").ToString).ToString("N2")
            txtNet.Text = CDec(SQL.SQLDR("NetAmount").ToString).ToString("N2")
            chkVAT.Checked = SQL.SQLDR("VATable")
            chkVATInc.Checked = SQL.SQLDR("VATInclusive")
            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            LoadVCE_Info(txtVCECode.Text)
            LoadSODetails(SO_ID)
            ComputeTotal()

        Else
            ClearText()
        End If

    End Sub

    Protected Sub LoadSODetails(ByVal TransID As String)
        Dim ctr As Integer = 0
        Dim query As String
        query = " SELECT    ItemCode, Description, UOM, QTY, ISNULL(UnitPrice,0) AS UnitPrice, " & _
                "           ISNULL(GrossAmount,0) AS GrossAmount, ISNULL(DiscountRate,0) AS DiscountRate, ISNULL(Discount,0) AS Discount, " & _
                "           ISNULL(VATAmount,0) AS VATAmount, ISNULL(NetAmount,0) AS NetAmount, ISNULL(VATable,1) AS VATable, ISNULL(VATinc,1) AS VATinc, " & _
                "           WHSE, DateDeliver, VCECode " & _
                " FROM      tblSO_Details " & _
                " WHERE     tblSO_Details.TransId = " & TransID & " " & _
                " ORDER BY  LineNum "
        dgvItemList.Rows.Clear()
        SQL.FlushParams()
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvItemList.Rows.Add("", row(0).ToString, "", row(1).ToString, row(2).ToString, "0",
                                     row(3).ToString, row(4).ToString, row(5).ToString,
                                     row(6).ToString, row(7).ToString, row(8).ToString,
                                     row(9).ToString, row(10).ToString, row(11).ToString,
                                     row(12).ToString, "", "", row(14).ToString, GetVCEName(row(14)))
                LoadVATType(ctr)
                LoadUOM(row(0).ToString, ctr)
                LoadColor(row(0).ToString, ctr)
                LoadSize(row(0).ToString, ctr)
                ctr += 1
            Next

        End If
        LoadStock()
        LoadBarCode()
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

    Private Sub chkVATInc_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkVATInc.CheckedChanged

    End Sub

    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs) Handles tsbEmail.Click
        Try

            If IsValidEmailFormat(txtEmail.Text) Then
                Dim attachfilepath As String
                Dim cryRpt As New ReportDocument
                Dim Report_Path As String
                Dim App_Path As String = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
                Report_Path = App_Path & "\CR_Reports\" & database
                Report_Path = Report_Path & "\PO_Printout.rpt"
                cryRpt.Load(Report_Path)
                cryRpt.SetDatabaseLogon(DBUser, DBPassword)
                cryRpt.SetParameterValue("@TransID", TransID)
                cryRpt.SetParameterValue("@Username", UserName)
                CrystalReportViewer1.ReportSource = cryRpt
                CrystalReportViewer1.Refresh()


                Dim App_Path2 As String = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
                Dim CrExportOptions As ExportOptions
                Dim CrDiskFileDestinationOptions As New  _
                DiskFileDestinationOptions()
                Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()
                CrDiskFileDestinationOptions.DiskFileName = App_Path2 & "\Export\PO" & TransID & ".pdf"
                attachfilepath = App_Path2 & "\Export\PO" & TransID & ".pdf"
                CrExportOptions = cryRpt.ExportOptions
                With CrExportOptions
                    .ExportDestinationType = ExportDestinationType.DiskFile
                    .ExportFormatType = ExportFormatType.PortableDocFormat
                    .DestinationOptions = CrDiskFileDestinationOptions
                    .FormatOptions = CrFormatTypeOptions
                End With
                cryRpt.Export()

                SendEmail(attachfilepath)
            Else
                Msg("Please enter a valid email address.", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Function IsValidEmailFormat(ByVal email As String) As Boolean
        Try
            Dim a As New System.Net.Mail.MailAddress(email)
        Catch
            Return False
        End Try
        Return True
    End Function

    Private Sub SendEmail(ByVal filepath As String)
        Try
            Dim mail As New MailMessage()
            Dim SmtpServer As New SmtpClient("smtp.gmail.com")
            mail.From = New MailAddress(EmailAddress)
            mail.[To].Add(txtEmail.Text)
            mail.Subject = "Test Mail - 1"
            mail.Body = "mail with attachment"

            Dim attachment As System.Net.Mail.Attachment
            attachment = New System.Net.Mail.Attachment(filepath)
            mail.Attachments.Add(attachment)

            SmtpServer.Port = 587
            SmtpServer.Credentials = New System.Net.NetworkCredential(EmailAddress, EmailPassword)
            SmtpServer.EnableSsl = True

            SmtpServer.Send(mail)
            Msg("Email Sent.", MsgBoxStyle.Information)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

   
    Private Sub lnkPartialPayment_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkPartialPayment.LinkClicked
        If disableEvent = False Then
            If txtNet.Text <> "0.00" Then
                If chkPartialPayment.Checked = False Then
                    chkPartialPayment.Checked = True
                Else
                    If Not frmPO_PartialPayment.IsDisposed Then
                        frmPO_PartialPayment.Close()
                        frmPO_PartialPayment.Dispose()
                    End If
                    frmPO_PartialPayment.TotalAmount = txtNet.Text
                    frmPO_PartialPayment.TransID = TransID
                    frmPO_PartialPayment.Show()
                End If
            Else
                Msg("Please check net amount.", MsgBoxStyle.Information)
            End If
        End If
    End Sub

    Private Sub chkPartialPayment_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkPartialPayment.CheckedChanged
        If disableEvent = False Then
            If chkPartialPayment.Checked = True Then
                If txtNet.Text <> "0.00" Then
                    If Not frmPO_PartialPayment.IsDisposed Then
                        frmPO_PartialPayment.Close()
                        frmPO_PartialPayment.Dispose()
                    End If
                    frmPO_PartialPayment.TotalAmount = txtNet.Text
                    frmPO_PartialPayment.TransID = TransID
                    frmPO_PartialPayment.Show()
                Else
                    Msg("Please check net amount.", MsgBoxStyle.Information)
                    chkPartialPayment.Checked = False
                End If
            End If
        End If
    End Sub

    Private Sub FromItemMasterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromItemMasterToolStripMenuItem.Click
        If txtVCECode.Text <> "" Then
            LoadSuppliersItem(txtVCECode.Text)
        Else
            MsgBox("Please select supplier first!", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub ToolStrip1_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub

    Private Sub txtDiscountRate_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtDiscountRate.TextChanged

    End Sub

    Private Sub chkFixedAsset_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkFixedAsset.CheckedChanged

    End Sub

    Private Sub GroupBox1_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox1.Enter

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
                dgvItemList.Rows.Add(row(0).ToString, row(1).ToString, "", row(2).ToString, UOM, SOH, POQTY, _
                          CDec(Price).ToString("N2"), CDec(Price * POQTY).ToString("N2"), "", "0.00", CDec(VATAmt * POQTY).ToString("N2"), _
                         CDec((Price) * POQTY).ToString("N2"), VAT, VATInc, "", InvAccount, GetAccntTitle(InvAccount), _
                         "", "", row(10).ToString, row(8).ToString, row(9).ToString, CDec(MIN).ToString("N2"))

                LoadVATType(dgvItemList.Rows.Count - 2)
                LoadUOM(row(1).ToString, dgvItemList.Rows.Count - 2)
                LoadColor(row(1).ToString, dgvItemList.Rows.Count - 2)
                LoadSize(row(1).ToString, dgvItemList.Rows.Count - 2)


                ctr += 1
            Next
        End If
        LoadStock()
        LoadBarCode()
        Recompute(dgvItemList.RowCount - 2, chQTY.Index)

    End Sub

    Private Sub DimFAsNewFrmReportDisplayToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DimFAsNewFrmReportDisplayToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("Reordering", "Reordering")
        f.Dispose()
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

    Private Sub contextMenuStrip1_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles contextMenuStrip1.Opening

    End Sub

    Private Sub dgvItemList_EditingControlShowing(sender As Object, e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvItemList.EditingControlShowing
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
                LoadStock()
                LoadBarCode()
            End If
        End If

        'Remove the handle to this event. It will be readded each time a new combobox selection causes the EditingControlShowing Event to fire
        RemoveHandler editingComboBox.SelectionChangeCommitted, AddressOf editingComboBox_SelectionChangeCommitted
        'Re-enable the EditingControlShowing event so the above can take place.
        AddHandler dgvItemList.EditingControlShowing, AddressOf dgvItemList_EditingControlShowing
    End Sub
End Class
