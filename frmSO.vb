Imports Excel = Microsoft.Office.Interop.Excel
Public Class frmSO
    Dim TransID, RefID As String
    Dim SONo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "SO"
    Dim ColumnPK As String = "SO_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblSO"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim SQ_ID As Integer
    Dim POreq, DRdateReq As Boolean
    Dim ForApproval As Boolean = False

    'SETUP VARIABLES
    Dim SOstock As String
    Dim Valid As Boolean = True
    Dim InvalidTemplate As Boolean = False
    Dim path As String
    Dim templateName As String = "TEMPLATE_SO"
    Public excelPW As String = "@dm1nEvo"

    Public Overloads Function ShowDialog(ByVal DocID As String) As Boolean
        TransID = DocID
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmSO_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            LoadSetup()
            LoadSetupStock()
            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            dtpDelivery.Value = Date.Today.Date
            If TransID <> "" Then
                LoadSO(TransID)
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
        query = " SELECT    ISNULL(SO_EditPrice,0) AS SO_EditPrice, ISNULL(SO_ReqPO,0) AS SO_ReqPO, " & _
                "           ISNULL(SO_ReqDRdate,0) AS SO_ReqDRdate, ISNULL(SO_StaggeredDR,0) AS SO_StaggeredDR " & _
                " FROM      tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dgvItemList.Columns(chUnitPrice.Index).ReadOnly = Not SQL.SQLDR("SO_EditPrice")
            POreq = SQL.SQLDR("SO_ReqPO")
            DRdateReq = SQL.SQLDR("SO_ReqDRdate")
            chkStaggared.Visible = SQL.SQLDR("SO_StaggeredDR")
        End If
    End Sub

    Private Sub LoadSetupStock()
        Dim query As String
        query = " SELECT  ISNULL(PR_StockLevel,0) AS PR_StockLevel FROM tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            SOstock = SQL.SQLDR("PR_StockLevel").ToString
            If SOstock <> "" Then
                LoadWHlevel(SOstock)
            End If
        End If
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

    Private Sub EnableControl(ByVal Value As Boolean)
        txtVCEName.Enabled = Value
        btnSearchVCE.Enabled = Value
        dgvItemList.AllowUserToAddRows = Value
        dgvItemList.AllowUserToDeleteRows = Value
        txtDiscountRate.Enabled = Value
        btnApplyRate.Enabled = Value
        dgvItemList.ReadOnly = Not Value
        If Value = True Then
            dgvItemList.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemList.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        dtpDelivery.Enabled = Value
        txtRefNo.Enabled = Value
        chkStaggared.Enabled = Value



        dgvItemList.Columns(chGross.Index).ReadOnly = True
        dgvItemList.Columns(chVATAmount.Index).ReadOnly = True
        dgvItemList.Columns(chNetAmount.Index).ReadOnly = True

        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
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
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            SONo = SQL.SQLDR("SO_No").ToString
            txtTransNum.Text = SONo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            dtpDocDate.Text = SQL.SQLDR("DateSO").ToString
            dtpDelivery.Text = SQL.SQLDR("DateDeliver").ToString
            txtGross.Text = CDec(SQL.SQLDR("GrossAmount").ToString).ToString("N2")
            txtVAT.Text = CDec(SQL.SQLDR("VATAmount").ToString).ToString("N2")
            txtDiscount.Text = CDec(SQL.SQLDR("Discount").ToString).ToString("N2")
            txtNet.Text = CDec(SQL.SQLDR("NetAmount").ToString).ToString("N2")
            txtRefNo.Text = SQL.SQLDR("ReferenceNo").ToString
            chkStaggared.Checked = SQL.SQLDR("StaggardDelivery")
            chkVAT.Checked = SQL.SQLDR("VATable")
            chkVATInc.Checked = SQL.SQLDR("VATInclusive")
            SQ_ID = SQL.SQLDR("SQ_Ref").ToString
            txtSQ_Ref.Text = LoadSQNo(SQ_ID)
            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            LoadSODetails(TransID)
            ComputeTotal()

            ' TOOLSTRIP BUTTONS
            If txtStatus.Text = "Cancelled" Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
                'ElseIf JOStatus(TransID) = True Then
                '    tsbEdit.Enabled = False
                '    tsbCancel.Enabled = False
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
            If dtpDocDate.Value <= GetMaxPEC() Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            End If
            EnableControl(False)
        Else
            ClearText()
        End If

    End Sub

    Private Function JOStatus(SO_ID As Integer) As String
        Dim query As String
        query = " SELECT * FROM tblJO " & _
                " WHERE SO_Ref IN (SELECT TransID FROM tblSO WHERE TransID = '" & SO_ID & "') " & _
                " AND Status <> 'Cancelled' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function LoadSQNo(SQ_ID As Integer) As String
        Dim query As String
        query = " SELECT SQ_No FROM tblSQ WHERE TransID = '" & SQ_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("SQ_No")
        Else
            Return ""
        End If
    End Function

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
                dgvItemList.Rows.Add(row(0).ToString, row(1).ToString, row(2).ToString, _
                                     row(3).ToString, row(4).ToString, row(5).ToString, _
                                     row(6).ToString, row(7).ToString, row(8).ToString, _
                                     row(9).ToString, row(10).ToString, row(11).ToString, _
                                     row(12).ToString, row(13).ToString, "", row(14).ToString, GetVCEName(row(14)))
                LoadUOM(row(0).ToString, ctr)
                ctr += 1
            Next

        End If
    End Sub

    Private Sub LoadSQ(ByVal SQ_No As String)
        Dim query As String
        query = " SELECT    TransID, SQ_No, VCECode, Remarks, " & _
                "            ISNULL(GrossAmount,0) AS GrossAmount, ISNULL(VATAmount,0) AS VATAmount, ISNULL(Discount,0) AS Discount, ISNULL(NetAmount,0) AS NetAmount,  " & _
                "            ISNULL(VATable,1) AS VATable, VATInclusive, Status " & _
                " FROM       tblSQ " & _
                " WHERE      TransId = '" & SQ_No & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            RefID = SQL.SQLDR("TransID")
            txtRefNo.Text = SQL.SQLDR("SQ_No").ToString
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtGross.Text = CDec(SQL.SQLDR("GrossAmount")).ToString("N2")
            txtVAT.Text = CDec(SQL.SQLDR("VATAmount")).ToString("N2")
            txtDiscount.Text = CDec(SQL.SQLDR("Discount")).ToString("N2")
            txtNet.Text = CDec(SQL.SQLDR("NetAmount")).ToString("N2")
            chkVAT.Checked = SQL.SQLDR("VATable")
            chkVATInc.Checked = SQL.SQLDR("VATInclusive")
            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            query = " SELECT    ItemCode, Description, UOM, QTY, UnitPrice, GrossAmount, DiscountRate, Discount, VATAmount, NetAmount, VATable " & _
                " FROM      tblSQ_Details " & _
                " WHERE     tblSQ_Details.TransId = " & RefID & " " & _
                " ORDER BY  LineNum "
            dgvItemList.Rows.Clear()
            SQL.ReadQuery(query)
            While SQL.SQLDR.Read
                dgvItemList.Rows.Add(SQL.SQLDR("ItemCode").ToString, SQL.SQLDR("Description").ToString, SQL.SQLDR("UOM").ToString, _
                                     SQL.SQLDR("QTY").ToString, CDec(SQL.SQLDR("UnitPrice")).ToString("N2"), _
                                     CDec(SQL.SQLDR("GrossAmount")).ToString("N2"), _
                                     IIf(IsNumeric(SQL.SQLDR("DiscountRate")), SQL.SQLDR("DiscountRate"), ""), _
                                     CDec(SQL.SQLDR("Discount")).ToString("N2"), _
                                     CDec(SQL.SQLDR("VATAmount")).ToString("N2"), _
                                     CDec(SQL.SQLDR("NetAmount")).ToString("N2"), _
                                     SQL.SQLDR("VATable").ToString)
            End While
        Else
            ClearText()
        End If
    End Sub

    Private Sub ClearText()
        txtTransNum.Text = ""
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        dgvItemList.Rows.Clear()
        txtRemarks.Text = ""
        txtGross.Text = "0.00"
        txtNet.Text = "0.00"
        txtVAT.Text = "0.00"
        txtDiscount.Text = "0.00"
        txtStatus.Text = "Open"
        dtpDocDate.MinDate = GetMaxPEC().AddDays(1)
        dtpDocDate.Value = Date.Today.Date
        dtpDelivery.Value = Date.Today.Date
        txtRefNo.Clear()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("Are you sure you want to cancel Purchase Order No. " & txtTransNum.Text & "? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Dim updateSQL As String
            updateSQL = " UPDATE tblSO SET Status ='Cancelled' WHERE TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", txtTransNum.Text)
            SQL.ExecNonQuery(updateSQL)
            MsgBox("SO No. " & txtTransNum.Text & " has been cancelled", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub Recompute(ByVal RowID As Integer, ByVal ColID As Integer)
        Dim gross, VAT, discount, net, baseVAT As Decimal
        If RowID <> -1 Then
            If IsNumeric(dgvItemList.Item(chGross.Index, RowID).Value) Then
                ' GET GROSS AMOUNT (VAT INCLUSIVE)
                gross = CDec(dgvItemList.Item(chUnitPrice.Index, RowID).Value) * CDec(dgvItemList.Item(chQTY.Index, RowID).Value)
                baseVAT = gross
                ' COMPUTE VAT IF VATABLE
                If ColID = chVAT.Index Then
                    If dgvItemList.Item(chVAT.Index, RowID).Value = True Then
                        dgvItemList.Item(chVAT.Index, RowID).Value = False

                        dgvItemList.Item(chVATInc.Index, RowID).Value = False
                        VAT = 0
                        dgvItemList.Item(chVATInc.Index, RowID).ReadOnly = True
                    Else
                        dgvItemList.Item(chVAT.Index, RowID).Value = True

                        dgvItemList.Item(chVATInc.Index, RowID).ReadOnly = False
                        If dgvItemList.Item(chVATInc.Index, RowID).Value = False Then
                            VAT = CDec(baseVAT * 0.12).ToString("N2")
                        Else
                            baseVAT = (gross / 1.12)
                            VAT = CDec(baseVAT * 0.12).ToString("N2")
                        End If

                    End If
                ElseIf ColID = chVATInc.Index Then

                    If dgvItemList.Item(chVAT.Index, RowID).Value = False Then
                        VAT = 0
                    Else
                        If dgvItemList.Item(chVATInc.Index, RowID).Value = True Then
                            dgvItemList.Item(chVATInc.Index, RowID).Value = False
                            VAT = CDec(baseVAT * 0.12).ToString("N2")
                        Else
                            dgvItemList.Item(chVATInc.Index, RowID).Value = True
                            'baseVAT = (gross / 1.12)
                            'VAT = CDec(baseVAT * 0.12).ToString("N2")
                            'baseVAT = gross
                            VAT = gross - (gross / 1.12)
                            gross = (gross / 1.12)
                        End If

                    End If
                Else


                    If dgvItemList.Item(chVAT.Index, RowID).Value = False Then
                        VAT = 0
                        dgvItemList.Item(chVATInc.Index, RowID).ReadOnly = True
                    Else
                        dgvItemList.Item(chVATInc.Index, RowID).ReadOnly = False
                        If dgvItemList.Item(chVATInc.Index, RowID).Value = True Then ' IF VAT INCLUSIVE, BASE AMOUNT WILL BE GROSS / 1.12
                            'baseVAT = (gross / 1.12)

                            VAT = gross - (gross / 1.12)
                            gross = (gross / 1.12)
                        End If
                        VAT = CDec(baseVAT * 0.12).ToString("N2")
                    End If
                End If


                ' COMPUTE DISCOUNT

                If IsNumeric(dgvItemList.Item(chDiscountRate.Index, RowID).Value) Then
                    discount = CDec(baseVAT * (CDec(dgvItemList.Item(chDiscountRate.Index, RowID).Value) / 100.0)).ToString("N2")
                ElseIf IsNumeric(dgvItemList.Item(chDiscount.Index, RowID).Value) Then
                    discount = CDec(dgvItemList.Item(chDiscount.Index, RowID).Value)
                Else
                    discount = 0
                End If

                If dgvItemList.Item(chVATInc.Index, RowID).Value = False Then

                    net = baseVAT - discount + VAT
                Else
                    net = baseVAT - discount
                End If

                dgvItemList.Item(chGross.Index, RowID).Value = Format(gross, "#,###,###,###.00").ToString()
                dgvItemList.Item(chDiscount.Index, RowID).Value = Format(discount, "#,###,###,###.00").ToString()
                dgvItemList.Item(chVATAmount.Index, RowID).Value = Format(VAT, "#,###,###,###.00").ToString()
                dgvItemList.Item(chNetAmount.Index, RowID).Value = Format(net, "#,###,###,###.00").ToString()
                ComputeTotal()
            End If
        End If

    End Sub

    Public Sub LoadItem(ByVal ID As String, ByVal ItemCode As String)
        Dim query As String
        query = " SELECT  ItemCode, ItemName, UOM, 1 AS QTY, UnitPrice, VATable, VATInclusive,  " & _
                "         CASE WHEN VATable = 0 THEN 0 " & _
                "              WHEN VATInclusive = 1 THEN UnitPrice/1.12*.12 " & _
                "              ELSE UnitPrice * 0.12 " & _
                "         END AS VAT, WHSE " & _
                " FROM    viewItem_Price " & _
                " WHERE    ItemCode = '" & ItemCode & "' AND Category ='Selling' AND PriceStatus = 'Active'"
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvItemList.Rows.Add(New String() {SQL.SQLDR("ItemCode").ToString, _
                                                 SQL.SQLDR("ItemName").ToString, _
                                                 SQL.SQLDR("UOM").ToString, _
                                                 SQL.SQLDR("QTY").ToString, _
                                                 Format(SQL.SQLDR("UnitPrice"), "#,###,###,###.00").ToString, _
                                                 Format(SQL.SQLDR("UnitPrice"), "#,###,###,###.00").ToString, _
                                                 "", "0.00", _
                                                 Format(SQL.SQLDR("VAT"), "#,###,###,###.00").ToString, _
                                                 Format(SQL.SQLDR("UnitPrice"), "#,###,###,###.00").ToString, _
                                                 SQL.SQLDR("VAtable"), SQL.SQLDR("VATInclusive"), SQL.SQLDR("WHSE").ToString, dtpDelivery.Value.Date})
            LoadUOM(ItemCode, dgvItemList.SelectedCells(0).RowIndex)
        End While
        ComputeTotal()
        'LoadStock()
    End Sub


    Private Sub LoadUOM(ItemCode As String, ByVal SelectedIndex As Integer)
        Try
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemList.Item(chUOM.Index, SelectedIndex)
            dgvCB.Items.Clear()
            ' ADD ALL UOM
            Dim query As String

            query = " SELECT DISTINCT UOM.UnitCode FROM tblItem_Master INNER JOIN  " & _
               " ( " & _
               " SELECT GroupCode, UnitCode FROM tblUOM_Group WHERE Status ='Active' " & _
               "  UNION ALL  " & _
               "    SELECT ItemCode, ItemUOM FROM tblItem_Master WHERE Status ='Active'  " & _
               " UNION ALL " & _
               " SELECT GroupCode, UnitCodeFrom FROM tblUOM_GroupDetails " & _
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

    Private Sub SaveSO()
        Try
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                                " tblSO  (TransID, SO_No, BranchCode, BusinessCode, VCECode, DateSO, DateDeliver, Remarks, StaggardDelivery, " & _
                                "         GrossAmount, Discount, VATAmount, NetAmount, VATable, VATInclusive, ReferenceNo, SQ_Ref, WhoCreated, TransAuto, Status) " & _
                                " VALUES (@TransID, @SO_No, @BranchCode, @BusinessCode, @VCECode,  @DateSO, @DateDeliver, @Remarks, @StaggardDelivery, " & _
                                "         @GrossAmount, @Discount, @VATAmount, @NetAmount, @VATable, @VATInclusive,  @ReferenceNo, @SQ_Ref, @WhoCreated, @TransAuto, @Status) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@SO_No", SONo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateSO", dtpDocDate.Value.Date)
            SQL.AddParam("@DateDeliver", dtpDelivery.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@StaggardDelivery", chkStaggared.Checked)
            SQL.AddParam("@GrossAmount", CDec(txtGross.Text))
            SQL.AddParam("@Discount", CDec(txtDiscount.Text))
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@NetAmount", CDec(txtNet.Text))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInclusive", chkVATInc.Checked)
            SQL.AddParam("@ReferenceNo", txtRefNo.Text)
            SQL.AddParam("@SQ_Ref", SQ_ID)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.AddParam("@TransAuto", TransAuto)
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Active")
            SQL.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblSO_Details(TransId, ItemCode, Description, UOM, QTY, UnitPrice, GrossAmount, VATable, VATinc, VATAmount, " & _
                                "                DiscountRate, Discount, NetAmount, WHSE, DateDeliver, VCECode, LineNum, WhoCreated) " & _
                                " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @UnitPrice, @GrossAmount, @VATable, @VATinc, @VATAmount, " & _
                                "        @DiscountRate, @Discount, @NetAmount, @WHSE, @DateDeliver, @VCECode, @LineNum, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value))
                    SQL.AddParam("@Description", row.Cells(chItemDesc.Index).Value.ToString)
                    SQL.AddParam("@UOM", row.Cells(chUOM.Index).Value.ToString)
                    SQL.AddParam("@QTY", CDec(row.Cells(chQTY.Index).Value))
                    SQL.AddParam("@UnitPrice", CDec(row.Cells(chUnitPrice.Index).Value))
                    SQL.AddParam("@GrossAmount", CDec(row.Cells(chGross.Index).Value))
                    If IsNothing(row.Cells(chVAT.Index).Value) Then SQL.AddParam("@VATable", False) Else SQL.AddParam("@VATable", row.Cells(chVAT.Index).Value)
                    If IsNothing(row.Cells(chVATInc.Index).Value) Then SQL.AddParam("@VATinc", False) Else SQL.AddParam("@VATinc", row.Cells(chVATInc.Index).Value)
                    SQL.AddParam("@VATAmount", CDec(row.Cells(chVATAmount.Index).Value))
                    If IsNumeric(row.Cells(chDiscountRate.Index).Value) Then
                        SQL.AddParam("@DiscountRate", CDec(row.Cells(chDiscountRate.Index).Value))
                    Else
                        SQL.AddParam("@DiscountRate", 0)
                    End If
                    SQL.AddParam("@Discount", CDec(row.Cells(chDiscount.Index).Value))
                    SQL.AddParam("@NetAmount", CDec(row.Cells(chNetAmount.Index).Value))
                    SQL.AddParam("@WHSE", row.Cells(chWHSE.Index).Value)
                    If Not row.Cells(chDelivery.Index).Value Is Nothing AndAlso IsDate(row.Cells(chDelivery.Index).Value) Then
                        SQL.AddParam("@DateDeliver", row.Cells(chDelivery.Index).Value)
                    Else
                        SQL.AddParam("@DateDeliver", dtpDelivery.Value.Date)
                    End If
                    If Not IsNothing(row.Cells(chCustomerCode.Index).Value) Then
                        SQL.AddParam("@VCECode", row.Cells(chCustomerCode.Index).Value)
                    Else
                        SQL.AddParam("@VCECode", "")
                    End If

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
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "SO_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub UpdateSO()
        Try
            activityStatus = True
            Dim insertSQL, updateSQL As String
            updateSQL = " UPDATE  tblSO " & _
                         " SET    SO_No = @SO_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, VCECode = @VCECode, DateSO = @DateSO, " & _
                         "        DateDeliver = @DateDeliver, Remarks = @Remarks, StaggardDelivery = @StaggardDelivery, " & _
                         "        GrossAmount = @GrossAmount, Discount = @Discount, VATAmount = @VATAmount, NetAmount = @NetAmount, " & _
                         "        VATable = @VATable, VATInclusive = @VATInclusive, " & _
                         "        ReferenceNo = @ReferenceNo, SQ_Ref = @SQ_Ref, " & _
                         "        DateModified = GETDATE(), WhoModified = @WhoModified " & _
                         " WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@SO_No", SONo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateSO", dtpDocDate.Value.Date)
            SQL.AddParam("@DateDeliver", dtpDelivery.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@StaggardDelivery", chkStaggared.Checked)
            SQL.AddParam("@GrossAmount", CDec(txtGross.Text))
            SQL.AddParam("@Discount", CDec(txtDiscount.Text))
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@NetAmount", CDec(txtNet.Text))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInclusive", chkVATInc.Checked)
            SQL.AddParam("@ReferenceNo", txtRefNo.Text)
            SQL.AddParam("@SQ_Ref", SQ_ID)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)

            Dim deleteSQL As String
            deleteSQL = " DELETE FROM tblSO_Details WHERE TransID = '" & TransID & "' "
            SQL.ExecNonQuery(deleteSQL)

            Dim line As Integer = 1
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblSO_Details(TransId, ItemCode, Description, UOM, QTY, UnitPrice, GrossAmount, VATable, VATinc, VATAmount, " & _
                                "                DiscountRate, Discount, NetAmount, WHSE, DateDeliver, VCECode, LineNum, WhoCreated) " & _
                                " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @UnitPrice, @GrossAmount, @VATable, @VATinc, @VATAmount, " & _
                                "        @DiscountRate, @Discount, @NetAmount, @WHSE, @DateDeliver, @VCECode, @LineNum, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value))
                    SQL.AddParam("@Description", row.Cells(chItemDesc.Index).Value.ToString)
                    SQL.AddParam("@UOM", row.Cells(chUOM.Index).Value.ToString)
                    SQL.AddParam("@QTY", CDec(row.Cells(chQTY.Index).Value))
                    SQL.AddParam("@UnitPrice", CDec(row.Cells(chUnitPrice.Index).Value))
                    SQL.AddParam("@GrossAmount", CDec(row.Cells(chGross.Index).Value))
                    If IsNothing(row.Cells(chVAT.Index).Value) Then SQL.AddParam("@VATable", False) Else SQL.AddParam("@VATable", row.Cells(chVAT.Index).Value)
                    If IsNothing(row.Cells(chVATInc.Index).Value) Then SQL.AddParam("@VATinc", False) Else SQL.AddParam("@VATinc", row.Cells(chVATInc.Index).Value)
                    SQL.AddParam("@VATAmount", CDec(row.Cells(chVATAmount.Index).Value))
                    If IsNumeric(row.Cells(chDiscountRate.Index).Value) Then
                        SQL.AddParam("@DiscountRate", CDec(row.Cells(chDiscountRate.Index).Value))
                    Else
                        SQL.AddParam("@DiscountRate", 0)
                    End If
                    SQL.AddParam("@Discount", CDec(row.Cells(chDiscount.Index).Value))
                    SQL.AddParam("@NetAmount", CDec(row.Cells(chNetAmount.Index).Value))
                    SQL.AddParam("@WHSE", row.Cells(chWHSE.Index).Value)
                    If Not row.Cells(chDelivery.Index).Value Is Nothing AndAlso IsDate(row.Cells(chDelivery.Index).Value) Then
                        SQL.AddParam("@DateDeliver", row.Cells(chDelivery.Index).Value)
                    Else
                        SQL.AddParam("@DateDeliver", dtpDelivery.Value.Date)
                    End If
                    SQL.AddParam("@VCECode", row.Cells(chCustomerCode.Index).Value.ToString)
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
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "SO_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
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
            If Val(dgvItemList.Item(chVATAmount.Index, i).Value) <> 0 Then
                If IsNumeric(dgvItemList.Item(chVATAmount.Index, i).Value) Then
                    c = c + Double.Parse(dgvItemList.Item(chVATAmount.Index, i).Value).ToString
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

    Private Sub dgvItemMaster_RowsRemoved(sender As System.Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
        ComputeTotal()
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("SO_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("SO")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadSO(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("SO_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            SONo = ""
            LoadSetup()
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
                dgvItemList.Rows(0).Cells(chDelivery.Index).Value = dtpDelivery.Value
            End If
            If dgvItemList.SelectedCells.Count > 0 Then
                dgvItemList.SelectedCells(0).Selected = False
            End If
            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
            txtVCEName.Focus()
        End If
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("SO_EDIT") Then
            msgRestricted()
        ElseIf JOStatus(TransID) = True Then
            Msg("Cannot edit transaction, please cancel all Job Order!", MsgBoxStyle.Information)
        Else
            EnableControl(True)
            txtTransNum.Enabled = False
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
        If DataValidation() = True Then
            If TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID(ColumnID, DBTable)
                    If TransAuto Then txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable) ' IF TRANS NUM IS SYSTEM GENERATED
                    SONo = txtTransNum.Text
                    SaveSO()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    LoadSO(TransID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    UpdateSO()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    LoadSO(TransID)
                End If
            End If
        End If

    End Sub

    Private Function DataValidation() As Boolean
        Dim valid As Boolean = True
        If dgvItemList.Rows.Count = 1 Then
            Msg("No item entered!", MsgBoxStyle.Exclamation)
            valid = False
        ElseIf txtVCECode.Text = "" Then
            Msg("Please Enter Customer!", MsgBoxStyle.Exclamation)
            valid = False
        ElseIf POreq AndAlso txtRefNo.Text = "" Then
            Msg("Customer PO Required!", MsgBoxStyle.Exclamation)
            valid = False
        ElseIf TransID = "" AndAlso txtRefNo.Text <> "" AndAlso CustomerPOvalid() <> "" Then  ' CHECK PO REF. IF ALREADY EXIST IN THE SAME CUSTOMER WHEN CREATING NEW SO
            Msg("Customer PO Ref. already used for SO No. " & CustomerPOvalid() & "!", MsgBoxStyle.Exclamation)
            valid = False
        ElseIf TransID <> "" AndAlso txtRefNo.Text <> "" AndAlso CustomerPOvalid() <> "" AndAlso CustomerPOvalid() <> txtTransNum.Text Then  ' CHECK PO REF. IF ALREADY EXIST IN THE SAME CUSTOMER WHEN UPDATING EXISTING SO
            Msg("Customer PO Ref. already used for SO No. " & CustomerPOvalid() & "!", MsgBoxStyle.Exclamation)
            valid = False
        ElseIf TransAuto = False AndAlso txtTransNum.Text = "" Then ' IF TRANS. NO. IS MANUAL, SO NO. INPUT IS REQUIRED
            Msg("Please Enter SO No.!", MsgBoxStyle.Exclamation)
            valid = False
        ElseIf TransID = "" AndAlso TransAuto = False AndAlso txtTransNum.Text = TransNoValid() Then ' CHECK SO NO. IF ALREADY EXIST WHEN CREATING NEW SO
            Msg("SO No. already exist!", MsgBoxStyle.Exclamation)
            valid = False
        ElseIf validateDGV() = False Then
            valid = False
        End If
        Return valid
    End Function



    Private Function validateDGV() As Boolean
        Dim ItemCode, VCECode, UOM As String
        Dim value As Boolean = True
        For Each row As DataGridViewRow In dgvItemList.Rows
            ' If IsNothing(row.Cells(chCustomerCode.Index).Value) Then VCECode = "" Else VCECode = row.Cells(chCustomerCode.Index).Value
            If IsNothing(row.Cells(chItemCode.Index).Value) Then ItemCode = "" Else ItemCode = row.Cells(chItemCode.Index).Value
            If IsNothing(row.Cells(chUOM.Index).Value) Then UOM = "" Else UOM = row.Cells(chUOM.Index).Value
            'If VCECode = "" And ItemCode <> "" Then
            '    Msg("There are line without customer name, please check.", MsgBoxStyle.Exclamation)
            '    value = False
            '    Exit For
            'Else
            If UOM = "" And ItemCode <> "" Then
                Msg("There are line entry without Item UOM, please check.", MsgBoxStyle.Exclamation)
                value = False
                Exit For
            End If
        Next
        Return value
    End Function

    Private Function CustomerPOvalid() As String
        Dim query As String
        query = " SELECT SO_No FROM tblSO WHERE ReferenceNo = @ReferenceNo AND VCECode = @VCECode AND Status <> 'Cancelled' "
        SQL.FlushParams()
        SQL.AddParam("@ReferenceNo", txtRefNo.Text)
        SQL.AddParam("@VCECode", txtVCECode.Text)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("SO_No")
        Else
            Return ""
        End If
    End Function

    Private Function TransNoValid() As String
        Dim query As String
        query = " SELECT SO_No FROM tblSO WHERE SO_No = @SO_No  "
        SQL.FlushParams()
        SQL.AddParam("@SO_No", txtTransNum.Text)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("SO_No")
        Else
            Return ""
        End If
    End Function

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("SO", TransID)
        f.Dispose()
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If SONo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblSO  WHERE SO_No < '" & SONo & "' ORDER BY SO_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadSO(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If SONo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblSO  WHERE SO_No > '" & SONo & "' ORDER BY SO_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadSO(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("SO_DEL") Then
            msgRestricted()
        ElseIf JOStatus(TransID) = True Then
            Msg("Cannot cancel transaction, please cancel all Job Order!", MsgBoxStyle.Information)
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE  tblSO SET Status ='Cancelled', WhoModified = @WhoModified WHERE SO_No = @SO_No "
                        SQL.FlushParams()
                        SONo = txtTransNum.Text
                        SQL.AddParam("@SO_No", SONo)
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

                        SONo = txtTransNum.Text
                        LoadSO(SONo)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "SO_No", SONo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub btnSearchVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchVCE.Click
        Dim f As New frmVCE_Search
        f.Type = "Customer"
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        txtVCEName.Text = f.VCEName
    End Sub
    Private Sub frmAPV_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
                If tsbCopy.Enabled = True Then tsbCopy.ShowDropDown()
            End If
        ElseIf e.Alt = True Then
            If e.KeyCode = Keys.F4 Then
                If tsbPrint.Enabled = True Then
                    tsbPrint.PerformClick()
                Else
                    e.SuppressKeyPress = True
                End If
            End If
        ElseIf e.KeyCode = Keys.Escape Then
            If tsbClose.Enabled = True Then tsbClose.PerformClick()
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        ' Toolstrip Buttons
        If TransID = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadSO(TransID)
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

    Private Sub tsbCopySQ_Click(sender As System.Object, e As System.EventArgs) Handles tsbCopyPR.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("SQ")
        LoadSQ(f.transID)
        f.Dispose()
    End Sub

    Private Sub dgvItemList_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellClick
        Try
            If e.ColumnIndex = chUOM.Index Then
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
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try

    End Sub


    Private Sub dgvItemList_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellEndEdit
        Try
            Dim itemCode, RecordID As String
            Dim rowIndex As Integer = dgvItemList.CurrentCell.RowIndex
            Dim colindex As Integer = dgvItemList.CurrentCell.ColumnIndex
            Select Case colindex
                Case chItemCode.Index
                    If dgvItemList.Item(chItemCode.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemList.Item(chItemCode.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("ItemMaster", itemCode, "Selling")
                        If f.TransID <> "" Then
                            RecordID = f.TransID
                            itemCode = f.ItemCode
                            LoadItem(RecordID, itemCode)
                        End If
                        dgvItemList.Rows.RemoveAt(e.RowIndex)
                        f.Dispose()
                    End If
                Case chItemDesc.Index
                    If dgvItemList.Item(chItemDesc.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemList.Item(chItemDesc.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("ItemMaster", itemCode, "Selling")
                        If f.TransID <> "" Then
                            RecordID = f.TransID
                            itemCode = f.ItemCode
                            LoadItem(RecordID, itemCode)
                        End If
                        dgvItemList.Rows.RemoveAt(e.RowIndex)
                        f.Dispose()
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
                Case chDelivery.Index
                    If IsDate(dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value) Then
                        dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value = CDate(dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value)
                    End If
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

    Private Sub dgvItemList_CurrentCellDirtyStateChanged(sender As System.Object, e As System.EventArgs) Handles dgvItemList.CurrentCellDirtyStateChanged
        If dgvItemList.SelectedCells.Count > 0 AndAlso (dgvItemList.SelectedCells(0).ColumnIndex = chVAT.Index OrElse dgvItemList.SelectedCells(0).ColumnIndex = chVATInc.Index) Then
            If dgvItemList.SelectedCells(0).RowIndex <> -1 Then
                Recompute(dgvItemList.SelectedCells(0).RowIndex, dgvItemList.SelectedCells(0).ColumnIndex)
                dgvItemList.SelectedCells(0).Selected = False
                dgvItemList.EndEdit()
            End If
        End If
    End Sub

    Private Sub txtVCEName_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.Type = "ALL"
            f.txtFilter.Text = txtVCEName.Text
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            txtVCEName.Text = f.VCEName
        End If
    End Sub

    Private Sub chkStaggared_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkStaggared.CheckedChanged
        If chkStaggared.Checked = True Then
            dgvItemList.Columns(chDelivery.Index).Visible = True
            dtpDelivery.Enabled = False
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not IsNothing(row.Cells(chItemCode.Index).Value) Then
                    row.Cells(chDelivery.Index).Value = dtpDelivery.Value.Date
                End If
            Next
        Else
            dgvItemList.Columns(chDelivery.Index).Visible = False
            dtpDelivery.Enabled = True
        End If
    End Sub

    Private Sub dgvItemList_CellValidating(sender As System.Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgvItemList.CellValidating
        If e.ColumnIndex = chUnitPrice.Index Or e.ColumnIndex = chQTY.Index Then
            Dim dc As Decimal
            If e.FormattedValue.ToString <> String.Empty AndAlso Not Decimal.TryParse(e.FormattedValue.ToString, dc) Then
                Msg("Invalid Number Format!", MsgBoxStyle.Exclamation)
                e.Cancel = True
            End If

        End If
    End Sub

    Private Sub txtDiscountRate_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtDiscountRate.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso Not e.KeyChar = "." Then
            e.Handled = True
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

    Private Sub txtDiscountRate_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtDiscountRate.KeyDown
        If e.KeyCode = Keys.Enter Then
            If IsNumeric(txtDiscount.Text) Then
                For Each row As DataGridViewRow In dgvItemList.Rows
                    If Not IsNothing(row.Cells(chItemCode.Index).Value) Then
                        row.Cells(chDiscountRate.Index).Value = txtDiscountRate.Text
                        Recompute(row.Index, chDiscountRate.Index)
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub cbStock_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbStock.SelectedIndexChanged
        If disableEvent = False Then
            LoadStock()
        End If
    End Sub

    Private Sub LoadStock()
        Dim query As String
        Dim stockFilter, itemCode As String
        Dim BOMQTY As Decimal = 0
        Dim StockQTY As Decimal = 0
        Dim SOQTY As Decimal = 0
        If (cbStock.SelectedIndex = -1 Or cbStock.SelectedItem = "ALL") Then
            stockFilter = " "
        Else
            stockFilter = " AND		" & SOstock & " ='" & cbStock.SelectedItem & "' "
        End If
        For Each row As DataGridViewRow In dgvItemList.Rows
            If Not IsNothing(row.Cells(chItemCode.Index).Value) Then
                itemCode = row.Cells(chItemCode.Index).Value.ToString
                query = "   SELECT	    ISNULL(SUM(QTY),0) AS QTY " & _
                        "   FROM		viewItem_Stock " & _
                        "   WHERE       ItemCode ='" & itemCode & "' " & stockFilter
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    StockQTY = SQL.SQLDR("QTY")
                    Dim MOQ As Decimal = GetMOQ(itemCode)
                    If StockQTY >= BOMQTY Then ' IF AVAILABLE STOCK IS GREATER THAN THE BOM REQUIREMENT THEN PRQTY SHOULD BE ZERO
                        SOQTY = 0
                    Else ' IF AVAILABLE STOCK IS LESS THAN THE BOM REQUIREMENT THEN PRQTY SHOULD BE BOM QTY - STOCK
                        SOQTY = BOMQTY - StockQTY
                        If SOQTY > MOQ Then ' IF PR QUANTITY IS GREATER THAN MOQ OF ITEM
                            SOQTY = Math.Ceiling(SOQTY)
                        Else
                            SOQTY = MOQ
                        End If
                    End If
                End If
                row.Cells(chStock.Index).Value = StockQTY

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

    Private Sub dgvItemList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellContentClick

    End Sub

    Private Sub dgvItemList_DataError(sender As Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvItemList.DataError
        Try

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
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
                LoadStock()
            End If
        End If

        'Remove the handle to this event. It will be readded each time a new combobox selection causes the EditingControlShowing Event to fire
        RemoveHandler editingComboBox.SelectionChangeCommitted, AddressOf editingComboBox_SelectionChangeCommitted
        'Re-enable the EditingControlShowing event so the above can take place.
        AddHandler dgvItemList.EditingControlShowing, AddressOf dgvItemList_EditingControlShowing
    End Sub

    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged

    End Sub

    Private Sub bgwUpload_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUpload.DoWork
        Dim startIndex As Integer
        Dim excelRangeCount As Integer
        Dim rowCount As Integer
        Dim ObjectText As String = ""
        Dim ObjectText1 As String = ""
        Dim ObjectText2 As String = ""
        Dim intAmount As Decimal = 0
        Dim prinAmount As Decimal = 0
        Dim AccountID As Integer = 0
        Dim VCECode As String = ""

        ' For Addditional Interest field when autobreakdown is checked.
        Dim addlCol As Integer = 0
        ' OPENING EXCEL FILE VARIABLES
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

        Valid = True
        InvalidTemplate = False
        Dim summary As Boolean = False
        Dim rowSumCount As Integer = 0
        Dim report As String = ""

        startIndex = 4

        SetPGBmax(range.Rows.Count)  ' Set Progress bar max value.
        excelRangeCount = range.Rows.Count
        Dim maxCol As Integer = range.Columns.Count - 1
        ' Loop through rows of excel.
        For i As Integer = startIndex To range.Rows.Count
            If bgwUpload.CancellationPending Then
                e.Cancel = True
                Exit For
            End If
            ' Reset AddCol.
            addlCol = 0

            ' Loop through columns of excel.
            For j As Integer = 0 To maxCol
                Obj = CType(range.Cells(i, j + 1), Excel.Range)

                ' Exit loop if first row and first column is not equal to template name.
                If i = 1 AndAlso j = 0 Then
                    If (Obj.value Is Nothing OrElse Obj.value.ToString <> templateName) Then

                        Dim test As String = Obj.value
                        bgwUpload.CancelAsync()
                        InvalidTemplate = True

                        If bgwUpload.CancellationPending Then
                            e.Cancel = True
                        End If
                    End If
                    Exit For


                ElseIf i > 1 Then
                    ' Update cell value, exit loop after updating the last column of a row
                    If dgvItemList.Columns.Count > j + addlCol Then

                        If j = chItemCode.Index Then
                             ' On the first column of the current row, exit loop when Code, Ref and Name is blank, should have atleast one record. 
                            Obj1 = CType(range.Cells(i, j + 2), Excel.Range)
                            Obj2 = CType(range.Cells(i, j + 3), Excel.Range)
                            If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                            If IsNothing(Obj1.value) Then ObjectText1 = "" Else ObjectText1 = Obj1.value.ToString
                            If IsNothing(Obj2.value) Then ObjectText2 = "" Else ObjectText2 = Obj2.value.ToString
                            If ObjectText = "" AndAlso ObjectText1 = "" AndAlso ObjectText2 = "" Then
                                Exit For
                            End If

                            ' Add Row to Datagridview on the first column loop.
                            AddRow()
                            rowCount += 1

                            ' ItemCode No
                            If IsNothing(dgvItemList.Item(j, rowCount - 1).Value) OrElse dgvItemList.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                                LoadUOM(ObjectText, rowCount - 1)
                                AddValue("", rowCount - 1, 12)
                            End If
                        ElseIf j = chItemDesc.Index Then
                             ' Check if has valid ItemName
                            If ObjectText <> "" Then
                                If Not validateItem(ObjectText) Then
                                    ' if not exist, change color.
                                    ChangeCellColor(rowCount - 1, j)
                                    Valid = False
                                Else
                                    ' if existing  AccountCode
                                    dgvItemList.Item(j, rowCount - 1).Value = GetItemName(ObjectText)
                                End If
                            End If

                        ElseIf j = chUOM.Index Then
                            ' UOM
                            If IsNothing(dgvItemList.Item(j, rowCount - 1).Value) OrElse dgvItemList.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chQTY.Index Then
                            ' QTY
                            If IsNothing(dgvItemList.Item(j, rowCount - 1).Value) OrElse dgvItemList.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chUnitPrice.Index Then
                            ' Unit Price
                            If IsNothing(dgvItemList.Item(j, rowCount - 1).Value) OrElse dgvItemList.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chGross.Index Then
                            ' Gross
                            If IsNothing(dgvItemList.Item(j, rowCount - 1).Value) OrElse dgvItemList.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chDiscountRate.Index Then
                            ' Discount Rate
                            If IsNothing(dgvItemList.Item(j, rowCount - 1).Value) OrElse dgvItemList.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chDiscount.Index Then
                            ' Discount
                             If IsNothing(dgvItemList.Item(j, rowCount - 1).Value) OrElse dgvItemList.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chVATAmount.Index Then
                            ' Vat Amount
                             If IsNothing(dgvItemList.Item(j, rowCount - 1).Value) OrElse dgvItemList.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chNetAmount.Index Then
                            'Net
                             If IsNothing(dgvItemList.Item(j, rowCount - 1).Value) OrElse dgvItemList.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chVAT.Index Then
                            ' Check if has valid Profit ID
                            If IsNothing(dgvItemList.Item(j, rowCount - 1).Value) OrElse dgvItemList.Item(j, rowCount - 1).Value = "" Then
                                ' if not check VCEref on excel template
                                If IsNothing(Obj.value) Then ObjectText = False Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chVATInc.Index Then
                            ' Check if has valid Profit ID
                            If IsNothing(dgvItemList.Item(j, rowCount - 1).Value) OrElse dgvItemList.Item(j, rowCount - 1).Value = "" Then
                                ' if not check VCEref on excel template
                                If IsNothing(Obj.value) Then ObjectText = False Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                           
                        End If
                    Else
                        Exit For
                    End If
                End If
            Next
            ' Reset AddCol.
            addlCol = 0
            bgwUpload.ReportProgress(i)

        Next

        ComputeTotal()

        NAR(Obj)
        NAR(range)
        NAR(xlWorkSheet)
        xlWorkBook.Close(False)
        NAR(xlWorkBook)
        xlApp.Quit()
        NAR(xlApp)
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub



    Public Function validateItem(ByVal ItemCode As String) As Boolean
        Try
            Dim query As String
            query = " SELECT    * " &
                    " FROM      tblItem_Master " &
                    " WHERE     ItemCode = @ItemCode "
            SQL.FlushParams()
            SQL.AddParam("@ItemCode", ItemCode)
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

    Private Sub bgwUpload_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwUpload.ProgressChanged
        pgbCounter.Value = e.ProgressPercentage
    End Sub

    Private Sub bgwUpload_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwUpload.RunWorkerCompleted
        pgbCounter.Visible = False
        If InvalidTemplate Then
            MsgBox("Invalid Template, Please select a valid File!", MsgBoxStyle.Exclamation)
        Else
            'TotalDBCR()
            If Valid Then
                If dgvItemList.Rows.Count > 1 Then
                    MsgBox(dgvItemList.Rows.Count - 1 & " File Data Uploaded Successfully!", vbInformation)
                Else
                    MsgBox(dgvItemList.Rows.Count & " File Data Uploaded Successfully!", vbInformation)
                End If

            Else
                MsgBox("Some data are invalid !, Please Check highlighted cells.", MsgBoxStyle.Exclamation)
            End If
            If dgvItemList.Rows.Count > 1 Then
                dgvItemList.Rows(dgvItemList.Rows.Count - 1).ReadOnly = True
            End If
        End If
        tsbUpload.Text = "Upload"
    End Sub

    Private Sub NAR(ByRef o As Object)
        Try
            While (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0)
            End While
        Catch
        Finally
            o = Nothing
        End Try
    End Sub
    Private Sub AddRow()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf AddRow))
        Else
            dgvItemList.Rows.Add("")
        End If
    End Sub

    Private Delegate Sub AddValueInvoker(ByVal Value As String, ByVal row As Integer, ByVal col As Integer)
    Private Sub AddValue(ByVal Value As String, ByVal row As Integer, ByVal col As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New AddValueInvoker(AddressOf AddValue), Value, row, col)
        Else
            dgvItemList.Item(col, row).Value = Value
        End If
    End Sub
    Private Delegate Sub SetPGBmaxInvoker(ByVal Value As String)
    Private Sub SetPGBmax(ByVal Value As String)
        If Me.InvokeRequired Then
            Me.Invoke(New SetPGBmaxInvoker(AddressOf SetPGBmax), Value)
        Else
            pgbCounter.Maximum = Value
            pgbCounter.Value = 0
        End If
    End Sub

    Private Delegate Sub ChangeCellColorInvoker(ByVal row As Integer, ByVal col As Integer)
    Private Sub ChangeCellColor(ByVal row As Integer, ByVal col As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New ChangeCellColorInvoker(AddressOf ChangeCellColor), row, col)
        Else
            dgvItemList.Rows(row).Cells(col).Style.BackColor = Color.Yellow
        End If
    End Sub
    Public Function validateVCE(ByVal VCEName As String) As Boolean
        Try
            Dim query As String
            query = " SELECT    * " &
                    " FROM      viewVCE_Master " &
                    " WHERE     VCECode = @VCECode "
            SQL.FlushParams()
            SQL.AddParam("@VCECode", VCEName)
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
    Public Function validateAccountCode(ByVal ItemCode As String) As Boolean
        Try
            Dim query As String
            query = " SELECT    * " &
                    " FROM      tblItem_Master " &
                    " WHERE     ItemCode = @ItemCode "
            SQL.FlushParams()
            SQL.AddParam("@ItemCode", ItemCode)
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

    Private Sub tsbUpload_Click(sender As System.Object, e As System.EventArgs) Handles tsbUpload.Click
        If tsbUpload.Text = "Upload" Then
            With (OpenFileDialog1)
                .InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                .Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx"
                .FilterIndex = 2
            End With
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                If MessageBox.Show("Uploading " & vbNewLine & "Are you sure you want to Contiue?", "Message Alert", MessageBoxButtons.YesNo) = MsgBoxResult.Yes Then
                    path = OpenFileDialog1.FileName
                    dgvItemList.Rows.Clear()
                    'dgvItemMaster.ReadOnly = True
                    pgbCounter.Visible = True
                    bgwUpload.RunWorkerAsync()
                    tsbUpload.Text = "Stop"
                End If
            End If
        Else
            If (bgwUpload.IsBusy = True) Then
                tsbUpload.Text = "Upload"
                pgbCounter.Value = 0
                bgwUpload.CancelAsync()
            End If
        End If
    End Sub

    Private Sub tsbDownload_Click(sender As System.Object, e As System.EventArgs) Handles tsbDownload.Click
        Dim fileName As String = "SO Uploader.xlsx"
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        xlApp = New Excel.Application
        Dim App_Path As String

        App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName & "\Templates"
        If My.Computer.FileSystem.FileExists(App_Path + "\" & templateName & ".xlsx") Then
            xlWorkBook = xlApp.Workbooks.Open(App_Path + "\" & templateName & ".xlsx")
            xlWorkSheet = xlWorkBook.Worksheets("Template")
            xlWorkSheet.Unprotect(excelPW)
            For i As Integer = 0 To 16
                If i = 0 Then
                    xlWorkSheet.Cells(1, i + 1) = templateName
                End If
                xlWorkSheet.Cells(2, i + 1) = dgvItemList.Columns(i).Name
                xlWorkSheet.Cells(3, i + 1) = dgvItemList.Columns(i).HeaderText
            Next
            xlWorkSheet.Protect(excelPW)
            Dim ctr As Integer = 1
            Do
                fileName = "SO Uploader -" & ctr.ToString & ".xlsx"
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