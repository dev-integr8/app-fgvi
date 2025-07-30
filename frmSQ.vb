Public Class frmSQ
    Dim TransID As String
    Dim SQNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "SQ"
    Dim ColumnPK As String = "SQ_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblSQ"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim ShowBOM As Boolean = True
    Dim ForApproval As Boolean = False

    Public Overloads Function ShowDialog(ByVal docnumber As String) As Boolean
        TransID = docnumber
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmSQ_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            dgvItemList.Columns(chBOM.Index).Visible = ShowBOM
            dgvItemList.Columns(chTotalCost.Index).Visible = ShowBOM
            dgvItemList.Columns(chMargin.Index).Visible = ShowBOM
            dgvItemList.Columns(chUnitPrice.Index).ReadOnly = ShowBOM ' IF BOM IS USED, PRICE IS READONLY AND DETERMINED BY BOM COST * (MARGIN/100)


            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            dtpUntil.Value = Date.Today.Date
            If TransID <> "" Then
                LoadSQ(TransID)
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
            EnableControl(False)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        txtVCEName.Enabled = Value
        btnSearchVCE.Enabled = Value
        dgvItemList.AllowUserToAddRows = Value
        dgvItemList.AllowUserToDeleteRows = Value
        dgvItemList.ReadOnly = Not Value
        dgvItemList.Columns(chGross.Index).ReadOnly = True
        dgvItemList.Columns(chVATAmount.Index).ReadOnly = True
        dgvItemList.Columns(chUOM.Index).ReadOnly = True
        dgvItemList.Columns(chNetAmount.Index).ReadOnly = True
        If Value = True Then
            dgvItemList.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemList.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        dtpUntil.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Private Sub LoadSQ(ByVal ID As String)
        Dim query As String
        Dim discount As Decimal = 0
        query = " SELECT   TransID, SQ_No, VCECode, DateSQ, DateValidUntil, " & _
                "          ISNULL(GrossAmount,0) AS GrossAmount, ISNULL(Discount,0) AS Discount, " & _
                "          ISNULL(VATAmount,0) AS VATAmount, ISNULL(NetAmount,0) AS NetAmount, " & _
                "          Remarks, VATable, VATInclusive, Status " & _
                " FROM     tblSQ " & _
                " WHERE    TransId = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            TransID = SQL.SQLDR("TransID").ToString
            SQNo = SQL.SQLDR("SQ_No").ToString
            txtTransNum.Text = SQNo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            dtpDocDate.Value = SQL.SQLDR("DateSQ").ToString
            dtpUntil.Value = SQL.SQLDR("DateValidUntil").ToString
            txtGross.Text = CDec(SQL.SQLDR("GrossAmount")).ToString("N2")
            txtDiscount.Text = CDec(SQL.SQLDR("Discount")).ToString("N2")
            txtVAT.Text = CDec(SQL.SQLDR("VATAmount")).ToString("N2")
            txtNet.Text = CDec(SQL.SQLDR("NetAmount")).ToString("N2")
            chkVATinc.Checked = SQL.SQLDR("VATInclusive")
            chkVAT.Checked = SQL.SQLDR("VATable")
            txtStatus.Text = SQL.SQLDR("Status").ToString
            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            LoadSQDetails(TransID)
            ComputeTotal()
            txtDiscount.Text = discount.ToString("N2")
            txtNet.Text = CDec(CDec(txtGross.Text) - discount).ToString("N2")

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
            EnableControl(False)
        Else
            ClearText()
        End If
    End Sub

    Private Sub LoadSQDetails(ByVal TransID As String)
        Dim query As String
        query = " SELECT    ItemCode, Description, Particulars, UOM, QTY, UnitPrice, GrossAmount, " &
                "           DiscountRate, Discount, VATAmount, NetAmount, VATable, ISNULL(VAT_Inc,0) AS VAT_Inc, " &
                "           ISNULL(BOM,'') AS BOM, ISNULL(TotalCost,0) AS TotalCost, ISNULL(Margin,0) AS Margin " &
                " FROM      tblSQ_Details " &
                " WHERE     tblSQ_Details.TransId = " & TransID & " " &
                " ORDER BY  LineNum "
        dgvItemList.Rows.Clear()
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvItemList.Rows.Add(SQL.SQLDR("ItemCode").ToString, SQL.SQLDR("Description").ToString, SQL.SQLDR("Particulars").ToString, SQL.SQLDR("BOM").ToString,
                                 SQL.SQLDR("UOM").ToString, SQL.SQLDR("TotalCost").ToString, SQL.SQLDR("Margin").ToString,
                                 CDec(SQL.SQLDR("UnitPrice")).ToString("N2"), SQL.SQLDR("QTY").ToString,
                                 CDec(SQL.SQLDR("GrossAmount")).ToString("N2"),
                                 IIf(IsNumeric(SQL.SQLDR("DiscountRate")), SQL.SQLDR("DiscountRate"), ""),
                                 CDec(SQL.SQLDR("Discount")).ToString("N2"),
                                 CDec(SQL.SQLDR("VATAmount")).ToString("N2"),
                                 CDec(SQL.SQLDR("NetAmount")).ToString("N2"),
                                 SQL.SQLDR("VATable").ToString, SQL.SQLDR("VAT_Inc").ToString)
            LoadBOM(SQL.SQLDR("ItemCode").ToString, dgvItemList.Rows.Count - 1)
        End While
    End Sub
    Private Sub LoadBOM(ByVal ItemCode As String)

    End Sub
    Private Sub txtVCEName_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.txtFilter.Text = txtVCEName.Text
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            txtVCEName.Text = f.VCEName
            f.Dispose()
        Else
            If txtVCEName.SelectionStart <> Len(txtVCEName.Text) Then
                txtVCEName.SelectAll()
                txtVCECode.Clear()
            End If
        End If
    End Sub

    Private Sub ClearText()
        txtTransNum.Text = ""
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        dgvItemList.Rows.Clear()
        txtRemarks.Text = ""
        txtNet.Text = "0.00"
        txtGross.Text = "0.00"
        txtVAT.Text = "0.00"
        txtDiscount.Text = "0.00"
        dtpDocDate.Value = Date.Today.Date
        txtStatus.Text = "Open"
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
                        f.ShowDialog("SalesItem", itemCode)
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItem(itemCode)
                        End If
                        f.Dispose()
                    End If
                Case chItemDesc.Index
                    If dgvItemList.Item(chItemDesc.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemList.Item(chItemDesc.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("SalesItem", itemCode)
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItem(itemCode)
                        End If
                        dgvItemList.Rows.RemoveAt(e.RowIndex)
                        f.Dispose()
                    End If
                Case chMargin.Index
                    If IsNumeric(dgvItemList.Item(chMargin.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemList.Item(chTotalCost.Index, e.RowIndex).Value) Then
                        dgvItemList.Item(chUnitPrice.Index, e.RowIndex).Value = dgvItemList.Item(chTotalCost.Index, e.RowIndex).Value * (1 + (dgvItemList.Item(chMargin.Index, e.RowIndex).Value / 100))
                        Recompute(e.RowIndex, e.ColumnIndex)
                        ComputeTotal()
                    End If
                Case chQTY.Index
                    If IsNumeric(dgvItemList.Item(chUnitPrice.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemList.Item(chQTY.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex)
                        ComputeTotal()
                    End If
                Case chUnitPrice.Index
                    If IsNumeric(dgvItemList.Item(chUnitPrice.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemList.Item(chQTY.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex)
                        ComputeTotal()
                    End If
                Case chDiscountRate.Index
                    If IsNumeric(dgvItemList.Item(chGross.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemList.Item(chDiscountRate.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex)
                        ComputeTotal()
                    End If
                Case chDiscount.Index
                    If IsNumeric(dgvItemList.Item(chGross.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemList.Item(chDiscount.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex)
                        ComputeTotal()
                    End If
            End Select
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Public Sub LoadItem(ByVal ID As String)
        Dim query As String
        query = " SELECT  ItemCode, ItemName, UOM, 1 AS QTY, VATable, VATInclusive, " &
                "         CASE WHEN VATable = 0 THEN UnitPrice " &
                "              WHEN VATInclusive = 1 THEN UnitPrice/1.12 " &
                "              ELSE UnitPrice  " &
                "         END AS UnitPrice, " &
                "         CASE WHEN VATable = 0 THEN 0 " &
                "              WHEN VATInclusive = 1 THEN UnitPrice/1.12*.12 " &
                "              ELSE UnitPrice * 0.12 " &
                "         END AS VAT " &
                " FROM    viewItem_Price " &
                " WHERE   ItemCode = '" & ID & "'  AND Category ='Selling' AND PriceStatus = 'Active' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvItemList.Rows.Add(New String() {SQL.SQLDR("ItemCode").ToString,
                                                 SQL.SQLDR("ItemName").ToString,
                                                 "", "",
                                                 SQL.SQLDR("UOM").ToString, 0, 0,
                                                 Format(SQL.SQLDR("UnitPrice"), "#,###,###,###.00").ToString,
                                                 SQL.SQLDR("QTY").ToString,
                                                 Format(SQL.SQLDR("UnitPrice"), "#,###,###,###.00").ToString,
                                                 "", "0.00",
                                                 Format(SQL.SQLDR("VAT"), "#,###,###,###.00").ToString,
                                                 Format(SQL.SQLDR("UnitPrice"), "#,###,###,###.00").ToString,
                                                 SQL.SQLDR("VAtable"),
                                                 SQL.SQLDR("VATInclusive")})
            LoadBOM(SQL.SQLDR("ItemCode").ToString, dgvItemList.Rows.Count - 2)
            LoadBomData(dgvItemList.Rows(dgvItemList.Rows.Count - 2).Cells(chBOM.Index).Value, dgvItemList.Rows.Count - 2)
        End While
        ComputeTotal()
    End Sub
    Private Sub LoadBOM(ItemCode As String, SelectedIndex As Integer)
        Dim SQL As New SQLControl
        Try
            Dim selectedValue As String = dgvItemList.Item(chBOM.Index, SelectedIndex).Value
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemList.Item(chBOM.Index, SelectedIndex)
            dgvCB.Items.Clear()
            Dim query As String
            query = " SELECT  DISTINCT BOM_Code FROM tblBOM_FG WHERE ItemCode = @ItemCode "
            SQL.FlushParams()
            SQL.AddParam("@ItemCode", ItemCode)
            SQL.ReadQuery(query)
            dgvCB.Items.Clear()
            While SQL.SQLDR.Read
                dgvCB.Items.Add(SQL.SQLDR("BOM_Code").ToString)
            End While
            If selectedValue = "" Then
                If dgvCB.Items.Count > 0 Then
                    dgvCB.Value = dgvCB.Items(0)
                End If
            Else
                dgvCB.Value = selectedValue
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            SaveError(ex.Message, ex.StackTrace, Me.Name, ModuleID)
        Finally
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


    Private Sub dgvItemMaster_RowsRemoved(sender As System.Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgvItemList.RowsRemoved
        ComputeTotal()
    End Sub



    Private Sub frmSQ_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        ElseIf e.KeyCode = Keys.Escape Then
            If tsbClose.Enabled = True Then tsbClose.PerformClick()
        End If
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("SQ_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("SQ")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadSQ(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("SQ_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            SQNo = ""

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
            EnableControl(True)

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)


        End If
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("SQ_EDIT") Then
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
        End If
    End Sub

    Private Sub SaveSQ()
        Try
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblSQ(TransID, SQ_No, BranchCode, BusinessCode, DateSQ, VCECode, DateValidUntil, Remarks,  " & _
                        "            GrossAmount, Discount, VATAmount, NetAmount, VATable, VATInclusive, WhoCreated, Status) " & _
                        " VALUES (@TransID, @SQ_No, @BranchCode, @BusinessCode, @DateSQ, @VCECode, @DateValidUntil, @Remarks,  " & _
                        "           @GrossAmount, @Discount, @VATAmount, @NetAmount, @VATable, @VATInclusive, @WhoCreated, @Status) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@SQ_No", SQNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateSQ", dtpDocDate.Value.Date)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateValidUntil", dtpUntil.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@GrossAmount", CDec(txtGross.Text))
            SQL.AddParam("@Discount", CDec(txtDiscount.Text))
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@NetAmount", CDec(txtNet.Text))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInclusive", chkVATinc.Checked)
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " &
                                " tblSQ_Details(TransId, ItemCode, Description, Particulars, UOM, QTY, UnitPrice, GrossAmount, " &
                                "               VATable, VAT_Inc, VATAmount, BOM, TotalCost, Margin, " &
                                "                DiscountRate, Discount, NetAmount, LineNum, WhoCreated) " &
                                " VALUES(@TransId, @ItemCode, @Description, @Particulars, @UOM, @QTY, @UnitPrice, @GrossAmount, " &
                                "               @VATable, @VAT_Inc,  @VATAmount, @BOM, @TotalCost, @Margin, " &
                                "        @DiscountRate, @Discount, @NetAmount, @LineNum, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value))
                    SQL.AddParam("@Description", row.Cells(chItemDesc.Index).Value.ToString)
                    SQL.AddParam("@Particulars", row.Cells(chParticulars.Index).Value.ToString)
                    SQL.AddParam("@UOM", row.Cells(chUOM.Index).Value.ToString)
                    SQL.AddParam("@QTY", CDec(row.Cells(chQTY.Index).Value))
                    SQL.AddParam("@UnitPrice", CDec(row.Cells(chUnitPrice.Index).Value))
                    SQL.AddParam("@GrossAmount", CDec(row.Cells(chGross.Index).Value))
                    SQL.AddParam("@VATable", row.Cells(chVAT.Index).Value)
                    SQL.AddParam("@VAT_Inc", row.Cells(chVATInc.Index).Value)
                    SQL.AddParam("@VATAmount", CDec(row.Cells(chVATAmount.Index).Value))
                    SQL.AddParam("@BOM", row.Cells(chBOM.Index).Value)
                    SQL.AddParam("@TotalCost", CDec(row.Cells(chTotalCost.Index).Value))
                    SQL.AddParam("@Margin", CDec(row.Cells(chMargin.Index).Value))
                    If IsNumeric(row.Cells(chDiscountRate.Index).Value) Then
                        SQL.AddParam("@DiscountRate", CDec(row.Cells(chDiscountRate.Index).Value))
                    Else
                        SQL.AddParam("@DiscountRate", 0)
                    End If
                    SQL.AddParam("@Discount", CDec(row.Cells(chDiscount.Index).Value))
                    SQL.AddParam("@NetAmount", CDec(row.Cells(chNetAmount.Index).Value))
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
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "SQ_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub UpdateSQ()
        Try
            activityStatus = True
            Dim insertSQL, deleteSQL As String
            insertSQL = " UPDATE  tblSQ" & _
                        " SET     SQ_No = @SQ_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, DateSQ = @DateSQ, " & _
                        "         VCECode = @VCECode, DateValidUntil = @DateValidUntil, Remarks = @Remarks,  " & _
                        "         GrossAmount = @GrossAmount, Discount = @Discount, VATAmount = @VATAmount, NetAmount = @NetAmount, " & _
                        "         VATable = @VATable, VATInclusive = @VATInclusive, WhoCreated = @WhoCreated " & _
                        " WHERE   TransID =  @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@SQ_No", SQNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateSQ", dtpDocDate.Value.Date)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateValidUntil", dtpUntil.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@GrossAmount", CDec(txtGross.Text))
            SQL.AddParam("@Discount", CDec(txtDiscount.Text))
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@NetAmount", CDec(txtNet.Text))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInclusive", chkVATinc.Checked)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            deleteSQL = " DELETE FROM tblSQ_Details WHERE TransID =@TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.ExecNonQuery(deleteSQL)

            Dim line As Integer = 1
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " &
                                " tblSQ_Details(TransId, ItemCode, Description, Particulars, UOM, QTY, UnitPrice, GrossAmount, " &
                                "               VATable, VAT_Inc, VATAmount, BOM, TotalCost, Margin, " &
                                "               DiscountRate, Discount, NetAmount, LineNum, WhoCreated) " &
                                " VALUES(@TransId, @ItemCode, @Description, @Particulars, @UOM, @QTY, @UnitPrice, @GrossAmount, " &
                                "               @VATable, @VAT_Inc, @VATAmount, @BOM, @TotalCost, @Margin, " &
                                "        @DiscountRate, @Discount, @NetAmount, @LineNum, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value))
                    SQL.AddParam("@Description", row.Cells(chItemDesc.Index).Value.ToString)
                    SQL.AddParam("@Particulars", row.Cells(chParticulars.Index).Value.ToString)
                    SQL.AddParam("@UOM", row.Cells(chUOM.Index).Value.ToString)
                    SQL.AddParam("@QTY", CDec(row.Cells(chQTY.Index).Value))
                    SQL.AddParam("@UnitPrice", CDec(row.Cells(chUnitPrice.Index).Value))
                    SQL.AddParam("@GrossAmount", CDec(row.Cells(chGross.Index).Value))
                    SQL.AddParam("@VATable", row.Cells(chVAT.Index).Value)
                    SQL.AddParam("@VAT_Inc", row.Cells(chVATInc.Index).Value)
                    SQL.AddParam("@VATAmount", CDec(row.Cells(chVATAmount.Index).Value))
                    SQL.AddParam("@BOM", row.Cells(chBOM.Index).Value)
                    SQL.AddParam("@TotalCost", CDec(row.Cells(chTotalCost.Index).Value))
                    SQL.AddParam("@Margin", CDec(row.Cells(chMargin.Index).Value))
                    If IsNumeric(row.Cells(chDiscountRate.Index).Value) Then
                        SQL.AddParam("@DiscountRate", CDec(row.Cells(chDiscountRate.Index).Value))
                    Else
                        SQL.AddParam("@DiscountRate", 0)
                    End If
                    SQL.AddParam("@Discount", CDec(row.Cells(chDiscount.Index).Value))
                    SQL.AddParam("@NetAmount", CDec(row.Cells(chNetAmount.Index).Value))
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
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "SQ_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("SQ_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE  tblSQ SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        SQNo = txtTransNum.Text
                        SQL.AddParam("@TransID", TransID)
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
                        EnableControl(False)

                        SQNo = txtTransNum.Text
                        LoadSQ(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "SQ_No", SQNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If SQNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblSQ  WHERE SQ_No < '" & SQNo & "' ORDER BY SQ_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadSQ(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If SQNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblSQ  WHERE SQ_No > '" & SQNo & "' ORDER BY SQ_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadSQ(TransID)
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
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadSQ(TransID)
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

    Private Sub txtVCEName_KeyDown_1(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.Type = "Customer"
            f.txtFilter.Text = txtVCEName.Text
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            txtVCEName.Text = f.VCEName
        End If
    End Sub

    Private Sub btnSearchVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchVCE.Click
        Dim f As New frmVCE_Search
        f.Type = "Customer"
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        txtVCEName.Text = f.VCEName
    End Sub

    Private Sub dgvItemList_CurrentCellDirtyStateChanged(sender As System.Object, e As System.EventArgs) Handles dgvItemList.CurrentCellDirtyStateChanged
        If dgvItemList.SelectedCells.Count > 0 AndAlso dgvItemList.SelectedCells(0).ColumnIndex = chVAT.Index Then
            If dgvItemList.SelectedCells(0).RowIndex <> -1 Then
                Recompute(dgvItemList.SelectedCells(0).RowIndex, dgvItemList.SelectedCells(0).ColumnIndex)
                dgvItemList.SelectedCells(0).Selected = False
                dgvItemList.EndEdit()
            End If
        End If
    End Sub


    Private Sub Recompute(ByVal RowID As Integer, ByVal ColID As Integer)
        Dim totalCost, UnitPrice, gross, VAT, discount, net As Decimal
        totalCost = CDec(dgvItemList.Item(chTotalCost.Index, RowID).Value)
        UnitPrice = CDec(dgvItemList.Item(chUnitPrice.Index, RowID).Value)
        If RowID <> -1 Then
            If IsNumeric(dgvItemList.Item(chGross.Index, RowID).Value) Then
                ' GET GROSS AMOUNT (VAT INCLUSIVE)
                gross = CDec(dgvItemList.Item(chUnitPrice.Index, RowID).Value) * CDec(dgvItemList.Item(chQTY.Index, RowID).Value)

                ' COMPUTE VAT IF VATABLE
                If ColID = chVAT.Index Then
                    If chkVAT.Checked = True AndAlso dgvItemList.Item(chVAT.Index, RowID).Value = True Then
                        dgvItemList.Item(chVAT.Index, RowID).Value = False
                        VAT = 0
                    Else
                        dgvItemList.Item(chVAT.Index, RowID).Value = True
                        VAT = CDec(gross * 0.12).ToString("N2")
                    End If
                Else
                    If chkVAT.Checked = True AndAlso dgvItemList.Item(chVAT.Index, RowID).Value = True Then
                        VAT = CDec(gross * 0.12).ToString("N2")
                    Else
                        VAT = 0
                    End If
                End If


                ' COMPUTE DISCOUNT
                If IsNumeric(dgvItemList.Item(chDiscountRate.Index, RowID).Value) Then
                    discount = CDec((gross) * (CDec(dgvItemList.Item(chDiscountRate.Index, RowID).Value) / 100.0)).ToString("N2")
                ElseIf IsNumeric(dgvItemList.Item(chDiscount.Index, RowID).Value) Then
                    discount = CDec(dgvItemList.Item(chDiscount.Index, RowID).Value)
                Else
                    discount = 0
                End If
                net = gross - discount + VAT
                dgvItemList.Item(chTotalCost.Index, RowID).Value = Format(totalCost, "#,###,###,###.00").ToString()
                dgvItemList.Item(chUnitPrice.Index, RowID).Value = Format(UnitPrice, "#,###,###,###.00").ToString()
                dgvItemList.Item(chGross.Index, RowID).Value = Format(gross, "#,###,###,###.00").ToString()
                dgvItemList.Item(chDiscount.Index, RowID).Value = Format(discount, "#,###,###,###.00").ToString()
                dgvItemList.Item(chVATAmount.Index, RowID).Value = Format(VAT, "#,###,###,###.00").ToString()
                dgvItemList.Item(chNetAmount.Index, RowID).Value = Format(net, "#,###,###,###.00").ToString()
                ComputeTotal()
            End If
        End If

    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If txtVCECode.Text = "" Then
            Msg("Please enter VCECode!", MsgBoxStyle.Exclamation)
        ElseIf TransID = "" Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                TransID = GenerateTransID(ColumnID, DBTable)
                SQNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                txtTransNum.Text = SQNo
                SaveSQ()
                Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                SQNo = txtTransNum.Text
                LoadSQ(TransID)
            End If
        Else
            If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                UpdateSQ()
                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                SQNo = txtTransNum.Text
                LoadSQ(TransID)
            End If
        End If
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("SQ", TransID)
        f.Dispose()
    End Sub

    Private Sub dgvItemList_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvItemList.DataError
        Try

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub dgvItemList_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgvItemList.EditingControlShowing
        Dim ColumnIndex As Integer = dgvItemList.CurrentCell.ColumnIndex
        If ColumnIndex = chBOM.Index Then
            Dim cb As ComboBox = e.Control
            If cb IsNot Nothing Then
                RemoveHandler cb.SelectedIndexChanged, AddressOf Cb_SelectedIndexChanged
                AddHandler cb.SelectedIndexChanged, AddressOf Cb_SelectedIndexChanged
            End If
        End If
    End Sub

    Private Sub Cb_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim rowIndex As Integer = dgvItemList.CurrentCell.RowIndex
        Dim colIndex As Integer = dgvItemList.CurrentCell.ColumnIndex
        Dim cb As ComboBox = sender

        dgvItemList.Rows(rowIndex).Cells(colIndex).Value = cb.SelectedItem
        If colIndex = chBOM.Index Then
            LoadBomData(dgvItemList.Rows(rowIndex).Cells(colIndex).Value, rowIndex)
        End If
    End Sub

    Private Sub LoadBomData(BOM_Code As String, Selectedindex As Integer)
        Dim SQL As New SQLControl
        Dim query As String
        query = " SELECT UOM, TotalCost FROM tblBOM_FG WHERE BOM_Code =@BOM_Code "
        SQL.FlushParams()
        SQL.AddParam("@BOM_Code", BOM_Code)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dgvItemList.Rows(Selectedindex).Cells(chUOM.Index).Value = SQL.SQLDR("UOM").ToString
            dgvItemList.Rows(Selectedindex).Cells(chTotalCost.Index).Value = SQL.SQLDR("TotalCost").ToString
            If IsNumeric(dgvItemList.Item(chMargin.Index, Selectedindex).Value) AndAlso IsNumeric(dgvItemList.Item(chTotalCost.Index, Selectedindex).Value) Then
                dgvItemList.Item(chUnitPrice.Index, Selectedindex).Value = dgvItemList.Item(chTotalCost.Index, Selectedindex).Value * (1 + (dgvItemList.Item(chMargin.Index, Selectedindex).Value / 100))
                Recompute(Selectedindex, chUnitPrice.Index)
                ComputeTotal()
            End If
        End If
    End Sub

    Private Sub dgvItemList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellContentClick

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