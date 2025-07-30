Public Class frmBOME
    Dim TransID, RefID As String
    Dim BOMNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "BOM"
    Dim ColumnPK As String = "BOM_No"
    Dim DBTable As String = "tblBOM"
    Dim ColumnID As String = "TransID"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim JO_ID As Integer
    Dim ForApproval As Boolean = False

    Public Overloads Function ShowDialog(ByVal DocID As String) As Boolean
        TransID = DocID
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmSO_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            If TransID <> "" Then
                LoadBOM(TransID)
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
            txtVCEName.Focus()
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        If Value = True Then
            dgcHeader_ItemList.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgcHeader_ItemList.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        txtVCEName.Enabled = Value
        btnSearchVCE.Enabled = Value
        cbBOM.Enabled = Value
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        btnExpload.Enabled = Value
        btnMultipleExplode.Enabled = Value
        chkForPR.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Private Sub LoadBOM(ByVal ID As String)
        Dim query As String
        query = " SELECT     TransID, BOM_No, VCECode, DateBOM,  Remarks, JO_Ref, " & _
                "            Status, ISNULL(ForPR,0) as ForPR " & _
                " FROM       tblBOM " & _
                " WHERE      TransId = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            BOMNo = SQL.SQLDR("BOM_No").ToString
            txtTransNum.Text = BOMNo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            dtpDocDate.Text = SQL.SQLDR("DateBOM").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            chkForPR.Checked = SQL.SQLDR("ForPR")
            JO_ID = SQL.SQLDR("JO_Ref").ToString
            txtJORef.Text = LoadJONo(JO_ID)

            LoadBOMType(txtItemCode.Text)
            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            LoadBOMJOHEADER(TransID)
            LoadBOMDetails(TransID)
            LoadLaborDetails(TransID)
            LoadOverHeadDetails(TransID)
            ComputeTotals()

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

            If dtpDocDate.Value <= GetMaxPEC() Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            End If

            EnableControl(False)
        Else
            ClearText()
        End If

    End Sub

    Private Function LoadJONo(JOID As Integer) As String
        Dim query As String
        query = " SELECT JO_No FROM tblJO WHERE TransID = '" & JOID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("JO_No")
        Else
            Return 0
        End If
    End Function

    Protected Sub LoadBOMJOHEADER(ByVal TransID As String)
        Dim query As String
        Dim ctr As Integer = 0
        query = " SELECT    TransID,  ItemCode, Description, UOM, QTY,  LotSize, BOMCode " &
                " FROM      tblBOM_JOHeader " &
                " WHERE     tblBOM_JOHeader.TransId = " & TransID & " " &
                " ORDER BY  LineNum "
        SQL.GetQuery(query)
        dgcHeader_ItemList.Rows.Clear()
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgcHeader_ItemList.Rows.Add(New String() {row(1).ToString,
                                             row(2).ToString,
                                             row(3).ToString,
                                             CDec(row(4)).ToString("N2"),
                                            CDec(row(5)).ToString("N2"),
                                            CDec(row(6)).ToString("N2"),
                                             GetBOM(row(7).ToString, "tblBOM_FG")})
                LoadBOMType(row(1).ToString, ctr)
                ctr += 1
            Next

        End If
    End Sub

    Protected Sub LoadBOMDetails(ByVal TransID As String)
        Dim query As String
        query = " SELECT    TransID, ItemGroup, ItemCode, Description, UOM, QTY_Per_BOM, GrossQTY, UnitCost, TotalCost, BOMCode " & _
                " FROM      tblBOM_Details " & _
                " WHERE     tblBOM_Details.TransId = " & TransID & " " & _
                " ORDER BY  LineNum "
        dgvItemList.Rows.Clear()
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvItemList.Rows.Add(SQL.SQLDR("ItemGroup").ToString, _
                                 SQL.SQLDR("ItemCode").ToString, _
                                 SQL.SQLDR("Description").ToString, _
                                 SQL.SQLDR("UOM").ToString, _
                                 SQL.SQLDR("QTY_Per_BOM").ToString, _
                                 CDec(SQL.SQLDR("GrossQTY")).ToString("N3"), _
                                 CDec(SQL.SQLDR("UnitCost")).ToString("N2"), _
                                 CDec(SQL.SQLDR("TotalCost")).ToString("N2"), _
                                SQL.SQLDR("BOMCode").ToString)
        End While
    End Sub

    Protected Sub LoadLaborDetails(ByVal TransID As String)
        Dim query As String
        query = " SELECT    TransID, Activity, RatePerHour, CrewNum, TimeMins, TotalMins, TotalCost " & _
                " FROM      tblBOM_DL " & _
                " WHERE     tblBOM_DL.TransId = " & TransID & " " & _
                " ORDER BY  LineNumber "
        dgvLabor.Rows.Clear()
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvLabor.Rows.Add(SQL.SQLDR("Activity").ToString, _
                                 CDec(SQL.SQLDR("RatePerHour")).ToString("N3"), _
                                 CDec(SQL.SQLDR("CrewNum")).ToString("N3"), _
                                 CDec(SQL.SQLDR("TimeMins")).ToString("N3"), _
                                 CDec(SQL.SQLDR("TotalMins")).ToString("N3"), _
                                 CDec(SQL.SQLDR("TotalCost")).ToString("N2"))
        End While
    End Sub

    Protected Sub LoadOverHeadDetails(ByVal TransID As String)
        Dim query As String
        query = " SELECT    TransID, Activity, Machine, RatePerHour, KW, NumHours, TotalKWH,TotalCost " & _
                " FROM      tblBOM_FOH " & _
                " WHERE     tblBOM_FOH.TransId = " & TransID & " " & _
                " ORDER BY  LineNumber "
        dgvOverhead.Rows.Clear()
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvOverhead.Rows.Add(SQL.SQLDR("Activity").ToString, _
                                 SQL.SQLDR("Machine").ToString, _
                                CDec(SQL.SQLDR("RatePerHour")).ToString("N3"), _
                                 CDec(SQL.SQLDR("KW")).ToString("N3"), _
                                 CDec(SQL.SQLDR("NumHours")).ToString("N3"), _
                                 CDec(SQL.SQLDR("TotalKWH")).ToString("N2"), _
                                 CDec(SQL.SQLDR("TotalCost")).ToString("N2"))
        End While
    End Sub

    Private Sub LoadJO(ByVal ID As String)
        Dim query As String
        query = " SELECT    tblJO.TransID, tblJO_Details.ItemCode, ItemName, tblJO_Details.UOM, " &
                 "          ISNULL(tblJO_Details.QTY,0) AS QTY, ISNULL(tblJO_Details.Size,0) AS Size, tblJO.VCECode, tblJO.Remarks, tblJO.JO_No " &
                 " FROM       tblJO_Details " &
                 " INNER JOIN tblJO ON " &
                 " tblJO.TransID = tblJO_Details.TransID" &
                 " LEFT JOIN tblItem_Master ON " &
                 " tblItem_Master.ItemCode = tblJO_Details.ItemCode" &
                 " WHERE      tblJO.TransID = '" & ID & "' "
        SQL.GetQuery(query)
        dgcHeader_ItemList.Rows.Clear()
        Dim ctr As Integer = 0
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                JO_ID = row(0).ToString
                txtJORef.Text = row(8).ToString
                txtVCECode.Text = row(6).ToString
                txtRemarks.Text = row(7).ToString
                txtVCEName.Text = GetVCEName(row(6).ToString)
                If row(1).ToString <> "" Then
                    dgcHeader_ItemList.Rows.Add(row(1).ToString, row(2).ToString, row(3).ToString,
                                                CDec(row(4)).ToString("N2"), CDec(row(5)).ToString("N2"))
                    LoadBOMType(row(1).ToString, ctr)
                    ctr += 1
                End If
            Next
            DisableHeaderColumn()
        End If
    End Sub


    Private Sub DisableHeaderColumn()
        dgcHeader_ItemList.Columns(chHeader_ItemCode.Index).ReadOnly = True
        dgcHeader_ItemList.Columns(chHeader_ItemName.Index).ReadOnly = True
        dgcHeader_ItemList.Columns(chHeader_ItemUOM.Index).ReadOnly = True
        dgcHeader_ItemList.Columns(chHeader_QTY.Index).ReadOnly = True
        dgcHeader_ItemList.Columns(chHeader_LotSize.Index).ReadOnly = True
    End Sub

    Private Sub LoadBOMType(ItemCode As String, ByVal SelectedIndex As Integer)
        Try
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgcHeader_ItemList.Item(chHeader_BOMCode.Index, SelectedIndex)
            dgvCB.Items.Clear()
            ' ADD ALL UOM
            Dim query As String
            query = " SELECT BOM_Code  + ' | ' + ISNULL(Remarks,'') AS Description  FROm tblBOM_FG WHERE ItemCode ='" & ItemCode & "' "
            SQL.ReadQuery(query)
            dgvCB.Items.Clear()
            While SQL.SQLDR.Read
                dgvCB.Items.Add(SQL.SQLDR("Description").ToString)
            End While
            dgvCB.Items.Add("")
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadBOMType(ByVal ItemCode As String)
        Dim query As String
        query = " SELECT BOM_Code  + ' | ' + ISNULL(Remarks,'') AS Description  FROm tblBOM_FG WHERE ItemCode ='" & ItemCode & "' "
        SQL.ReadQuery(query)
        cbBOM.Items.Clear()
        While SQL.SQLDR.Read
            cbBOM.Items.Add(SQL.SQLDR("Description").ToString)
        End While
    End Sub


    Public Function GetItemName(ByVal ItemCode As String) As String
        Dim query As String
        query = " SELECT ItemName FROM tblItem_Master WHERE ItemCode ='" & ItemCode & "'"
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("ItemName").ToString
        Else
            Return ""
        End If
    End Function

    Private Sub ClearText()
        txtTransNum.Clear()
        txtVCECode.Clear()
        txtVCEName.Clear()
        dgcHeader_ItemList.Rows.Clear()
        dgvItemList.Rows.Clear()
        dgvLabor.Rows.Clear()
        dgvOverhead.Rows.Clear()
        txtRemarks.Clear()
        txtGross.Text = "0.00"
        txtVAT.Text = "0.00"
        txtDiscount.Text = "0.00"
        txtStatus.Text = "Open"
        dtpDocDate.MinDate = GetMaxPEC().AddDays(1)
        dtpDocDate.Value = Date.Today.Date

        txtItemCode.Clear()
        txtDescription.Clear()
        txtUOM.Clear()
        txtQTY.Clear()
        txtLotSize.Clear()
        cbBOM.SelectedIndex = -1
        txtJORef.Clear()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("Are you sure you want to cancel Purchase Order No. " & txtTransNum.Text & "? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Dim updateSQL As String
            updateSQL = " UPDATE tblBOM_No SET Status ='Cancelled' WHERE TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", txtTransNum.Text)
            SQL.ExecNonQuery(updateSQL)
            MsgBox("BOM No. " & txtTransNum.Text & " has been cancelled", MsgBoxStyle.Information)
        End If
    End Sub

    Public Function GetTransID() As Integer
        Dim query As String
        query = " SELECT MAX(TransID) + 1 AS TransID FROM tblBOM_Header"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() And Not IsDBNull(SQL.SQLDR("TransID")) Then
            Return SQL.SQLDR("TransID")
        Else
            Return 1
        End If
    End Function

    Private Sub DisableColumn()
        dgvItemList.Columns("Column6").ReadOnly = True
        dgvItemList.Columns("Column7").ReadOnly = True
    End Sub

    Private Sub dgvProducts_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        'Try
        '    Dim itemCode As String
        '    Dim rowIndex As Integer = dgvItemList.CurrentCell.RowIndex
        '    Dim colindex As Integer = dgvItemList.CurrentCell.ColumnIndex
        '    Select Case colindex
        '        Case chItemCode.Index
        '            If dgvItemList.Item(chItemCode.Index, e.RowIndex).Value <> "" Then
        '                itemCode = dgvItemList.Item(chItemCode.Index, e.RowIndex).Value
        '                Dim f As New frmCopyFrom
        '                f.ShowDialog("ItemListSO", itemCode, "Selling")
        '                If f.TransID <> "" Then
        '                    itemCode = f.TransID
        '                    LoadItem(itemCode)
        '                Else
        '                    dgvItemList.Rows.RemoveAt(e.RowIndex)
        '                End If
        '                f.Dispose()
        '            End If
        '        Case chItemDesc.Index
        '            If dgvItemList.Item(chItemDesc.Index, e.RowIndex).Value <> "" Then
        '                itemCode = dgvItemList.Item(chItemDesc.Index, e.RowIndex).Value
        '                Dim f As New frmCopyFrom
        '                f.ShowDialog("ItemListSO", itemCode, "Selling")
        '                If f.TransID <> "" Then
        '                    itemCode = f.TransID
        '                    LoadItem(itemCode)
        '                End If
        '                dgvItemList.Rows.RemoveAt(e.RowIndex)
        '                f.Dispose()
        '            End If
        '        Case chQTY.Index
        '            If IsNumeric(dgvItemList.Item(chUnitPrice.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemList.Item(chQTY.Index, e.RowIndex).Value) Then
        '                Recompute(e.RowIndex, e.ColumnIndex)
        '                ComputeTotal()
        '            End If
        '        Case chDiscountRate.Index
        '            If IsNumeric(dgvItemList.Item(chGross.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemList.Item(chDiscountRate.Index, e.RowIndex).Value) Then
        '                Recompute(e.RowIndex, e.ColumnIndex)
        '                ComputeTotal()
        '            End If
        '        Case chDiscount.Index
        '            If IsNumeric(dgvItemList.Item(chGross.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemList.Item(chDiscount.Index, e.RowIndex).Value) Then
        '                Recompute(e.RowIndex, e.ColumnIndex)
        '                ComputeTotal()
        '            End If
        '    End Select
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub Recompute(ByVal RowID As Integer, ByVal ColID As Integer)
        'Dim gross, VAT, discount, net As Decimal
        'If RowID <> -1 Then
        '    If IsNumeric(dgvItemList.Item(chGross.Index, RowID).Value) Then
        '        ' GET GROSS AMOUNT (VAT INCLUSIVE)
        '        gross = CDec(dgvItemList.Item(chUnitPrice.Index, RowID).Value) * CDec(dgvItemList.Item(chQTY.Index, RowID).Value)

        '        ' COMPUTE VAT IF VATABLE
        '        If ColID = chVAT.Index Then
        '            If chkVATInc.Checked = True AndAlso dgvItemList.Item(chVAT.Index, RowID).Value = True Then
        '                dgvItemList.Item(chVAT.Index, RowID).Value = False
        '                VAT = 0
        '            Else
        '                dgvItemList.Item(chVAT.Index, RowID).Value = True
        '                VAT = CDec(gross * 0.12).ToString("N2")
        '            End If
        '        Else
        '            If chkVATInc.Checked = True AndAlso dgvItemList.Item(chVAT.Index, RowID).Value = True Then
        '                VAT = CDec(gross * 0.12).ToString("N2")
        '            Else
        '                VAT = 0
        '            End If
        '        End If


        '        ' COMPUTE DISCOUNT
        '        If IsNumeric(dgvItemList.Item(chDiscountRate.Index, RowID).Value) Then
        '            discount = CDec((gross - VAT) * (CDec(dgvItemList.Item(chDiscountRate.Index, RowID).Value) / 100.0)).ToString("N2")
        '        ElseIf IsNumeric(dgvItemList.Item(chDiscount.Index, RowID).Value) Then
        '            discount = CDec(dgvItemList.Item(chDiscount.Index, RowID).Value)
        '        Else
        '            discount = 0
        '        End If
        '        net = gross - discount + VAT
        '        dgvItemList.Item(chGross.Index, RowID).Value = Format(gross, "#,###,###,###.00").ToString()
        '        dgvItemList.Item(chDiscount.Index, RowID).Value = Format(discount, "#,###,###,###.00").ToString()
        '        dgvItemList.Item(chVATAmount.Index, RowID).Value = Format(VAT, "#,###,###,###.00").ToString()
        '        dgvItemList.Item(chNetAmount.Index, RowID).Value = Format(net, "#,###,###,###.00").ToString()
        '        ComputeTotal()
        '    End If
        'End If

    End Sub

    Public Sub LoadItem(ByVal ID As String)
        Dim query As String
        query = " SELECT  ItemCode, ItemName, PD_UOM  " & _
                " FROM    tblItem_Master " & _
                " WHERE   ItemCode ='" & ID & "'"
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvItemList.Rows.Add(New String() {"", _
                                               "", _
                                               "", _
                                 SQL.SQLDR("ItemCode").ToString, _
                                               SQL.SQLDR("ItemName").ToString})

        End While
    End Sub

    Private Sub SaveBOM()
        Try
            activityStatus = True
            Dim insertSQL, BOM_Code As String
            insertSQL = " INSERT INTO " & _
                                " tblBOM  (TransID, BOM_No, VCECode, DateBOM,  JO_Ref, Remarks, ForPR, WhoCreated, Status) " & _
                                " VALUES (@TransID, @BOM_No, @VCECode, @DateBOM,  @JO_Ref, @Remarks, @ForPR, @WhoCreated, @Status) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@BOM_No", BOMNo)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateBOM", dtpDocDate.Value.Date)
            'SQL.AddParam("@ItemCode", txtItemCode.Text)
            'SQL.AddParam("@Description", txtDescription.Text)
            'SQL.AddParam("@UOM", txtUOM.Text)
            'If IsNumeric(txtQTY.Text) Then
            '    SQL.AddParam("@QTY", CDec(txtQTY.Text))
            'Else
            '    SQL.AddParam("@QTY", 0)
            'End If
            'If IsNumeric(txtLotSize.Text) Then
            '    SQL.AddParam("@LotSize", CDec(txtLotSize.Text))
            'Else
            '    SQL.AddParam("@LotSize", 0)
            'End If
            'SQL.AddParam("@BOMCode", BOM_Code)
            SQL.AddParam("@JO_Ref", JO_ID)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.AddParam("@ForPR", IIf(chkForPR.Checked = True, True, False))
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Active")
            SQL.ExecNonQuery(insertSQL)



            Dim line As Integer = 1
            For Each row As DataGridViewRow In dgcHeader_ItemList.Rows
                If Not row.Cells(chHeader_ItemCode.Index).Value Is Nothing AndAlso Not row.Cells(chHeader_BOMCode.Index).Value Is Nothing Then
                    BOM_Code = GetBOM(row.Cells(chHeader_BOMCode.Index).Value.ToString)
                    insertSQL = " INSERT INTO " &
                                " tblBOM_JOHeader(TransId, ItemCode, Description, QTY,  UOM,  LotSize, BOMCode, LineNum   ) " &
                                " VALUES(@TransId, @ItemCode, @Description, @QTY,  @UOM,  @LotSize, @BOMCode, @LineNum   ) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", row.Cells(chHeader_ItemCode.Index).Value.ToString)
                    SQL.AddParam("@Description", row.Cells(chHeader_ItemName.Index).Value.ToString)
                    SQL.AddParam("@UOM", row.Cells(chHeader_ItemUOM.Index).Value.ToString)
                    SQL.AddParam("@QTY", CDec(row.Cells(chHeader_QTY.Index).Value))
                    SQL.AddParam("@LotSize", CDec(row.Cells(chHeader_LotSize.Index).Value))
                    SQL.AddParam("@BOMCode", BOM_Code)
                    SQL.AddParam("@LineNum", line)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next

            line = 1
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(dgcBOM_ItemCode.Index).Value Is Nothing AndAlso Not row.Cells(dgcQTY_Per_BOM.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblBOM_Details(TransId, ItemGroup, ItemCode, Description, QTY_Per_BOM, UOM, GrossQTY, UnitCost, TotalCost, LineNum, WhoCreated, BOMCode) " & _
                                " VALUES(@TransId, @ItemGroup, @ItemCode, @Description, @QTY_Per_BOM, @UOM, @GrossQTY, @UnitCost, @TotalCost, @LineNum, @WhoCreated, @BOMCode) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemGroup", row.Cells(dgcBOMgroup.Index).Value.ToString)
                    SQL.AddParam("@ItemCode", row.Cells(dgcBOM_ItemCode.Index).Value)
                    SQL.AddParam("@Description", row.Cells(dgcBOM_ItemDesc.Index).Value.ToString)
                    SQL.AddParam("@QTY_Per_BOM", CDec(row.Cells(dgcQTY_Per_BOM.Index).Value))
                    SQL.AddParam("@UOM", row.Cells(dgcUOM.Index).Value.ToString)
                    SQL.AddParam("@GrossQTY", CDec(row.Cells(dgcPR.Index).Value))
                    SQL.AddParam("@UnitCost", CDec(row.Cells(dgcUC.Index).Value))
                    SQL.AddParam("@TotalCost", CDec(row.Cells(dgcTC.Index).Value))
                    SQL.AddParam("@BOMCode", row.Cells(dgcBOMCode.Index).Value)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next


            line = 1
            For Each row As DataGridViewRow In dgvLabor.Rows
                If Not row.Cells(dgcDLactivity.Index).Value Is Nothing AndAlso Not row.Cells(dgcDLtotalCost.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblBOM_DL(TransId, Activity, RatePerHour, CrewNum, TimeMins, TotalMins, TotalCost, LineNumber, WhoCreated) " & _
                                " VALUES(@TransId, @Activity, @RatePerHour, @CrewNum, @TimeMins, @TotalMins, @TotalCost , @LineNumber, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@Activity", row.Cells(dgcDLactivity.Index).Value.ToString)
                    SQL.AddParam("@RatePerHour", CDec(row.Cells(dgcDLrate.Index).Value))
                    SQL.AddParam("@CrewNum", CDec(row.Cells(dgcDLcrewNo.Index).Value))
                    SQL.AddParam("@TimeMins", CDec(row.Cells(dgcDLtime.Index).Value))
                    SQL.AddParam("@TotalMins", CDec(row.Cells(dgcDLTotalMins.Index).Value))
                    SQL.AddParam("@TotalCost", CDec(row.Cells(dgcDLtotalCost.Index).Value))
                    SQL.AddParam("@LineNumber", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next


            line = 1
            For Each row As DataGridViewRow In dgvOverhead.Rows
                If Not row.Cells(dgcFOactivity.Index).Value Is Nothing AndAlso Not row.Cells(dgcFOcost.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblBOM_FOH(TransId, Activity, Machine, RatePerHour, KW, NumHours, TotalKWH, TotalCost, LineNumber, WhoCreated) " & _
                                " VALUES(@TransId, @Activity, @Machine, @RatePerHour, @KW, @NumHours, @TotalKWH, @TotalCost , @LineNumber, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@Activity", row.Cells(dgcFOactivity.Index).Value.ToString)
                    SQL.AddParam("@Machine", row.Cells(dgcFOmachine.Index).Value.ToString)
                    SQL.AddParam("@RatePerHour", CDec(row.Cells(dgcFOrate.Index).Value))
                    SQL.AddParam("@KW", CDec(row.Cells(dgcFOKW.Index).Value))
                    SQL.AddParam("@NumHours", CDec(row.Cells(dgcFOhrs.Index).Value))
                    SQL.AddParam("@TotalKWH", CDec(row.Cells(dgcFOtotalKWH.Index).Value))
                    SQL.AddParam("@TotalCost", CDec(row.Cells(dgcFOcost.Index).Value))
                    SQL.AddParam("@LineNumber", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "BOM_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub UpdateBOM()
        Try
            activityStatus = True
            Dim insertSQL, updateSQL, BOM_Code As String
            updateSQL = " UPDATE tblBOM " & _
                         " SET    BOM_No = @BOM_No, VCECode = @VCECode, DateBOM = @DateBOM,  Remarks = @Remarks, JO_Ref = @JO_Ref, ForPR = @ForPR, " & _
                          "        DateModified = GETDATE(), WhoModified = @WhoModified " & _
                          " WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@BOM_No", BOMNo)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateBOM", dtpDocDate.Value.Date)
            SQL.AddParam("@JO_Ref", JO_ID)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@ForPR", IIf(chkForPR.Checked = True, True, False))
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)


            Dim deleteSQL As String
            deleteSQL = " DELETE FROM tblBOM_JOHeader WHERE TransID = '" & TransID & "' "
            SQL.ExecNonQuery(deleteSQL)

            Dim line As Integer = 1
            For Each row As DataGridViewRow In dgcHeader_ItemList.Rows
                If Not row.Cells(chHeader_ItemCode.Index).Value Is Nothing AndAlso Not row.Cells(chHeader_BOMCode.Index).Value Is Nothing Then
                    BOM_Code = GetBOM(row.Cells(chHeader_BOMCode.Index).Value.ToString)
                    insertSQL = " INSERT INTO " &
                                " tblBOM_JOHeader(TransId, ItemCode, Description, QTY, Size,  UOM, LotSize, BOMCode, LineNum   ) " &
                                " VALUES(@TransId, @ItemCode, @Description, @QTY,  @Size, @UOM, @LotSize, @BOMCode, @LineNum   ) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", row.Cells(chHeader_ItemCode.Index).Value.ToString)
                    SQL.AddParam("@Description", row.Cells(chHeader_ItemName.Index).Value.ToString)
                    SQL.AddParam("@UOM", row.Cells(chHeader_ItemUOM.Index).Value.ToString)
                    SQL.AddParam("@QTY", CDec(row.Cells(chHeader_QTY.Index).Value))
                    SQL.AddParam("@Size", CDec(row.Cells(chHeader_Size.Index).Value))
                    SQL.AddParam("@LotSize", CDec(row.Cells(chHeader_LotSize.Index).Value))
                    SQL.AddParam("@BOMCode", BOM_Code)
                    SQL.AddParam("@LineNum", line)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next

            deleteSQL = " DELETE FROM tblBOM_Details WHERE TransID = '" & TransID & "' "
            SQL.ExecNonQuery(deleteSQL)

            line = 1
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(dgcBOM_ItemCode.Index).Value Is Nothing AndAlso Not row.Cells(dgcQTY_Per_BOM.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " & _
                                  " tblBOM_Details(TransId, ItemGroup, ItemCode, Description, QTY_Per_BOM, UOM, GrossQTY, UnitCost, TotalCost, LineNum, WhoCreated, BOMCode) " & _
                                  " VALUES(@TransId, @ItemGroup, @ItemCode, @Description, @QTY_Per_BOM, @UOM, @GrossQTY, @UnitCost, @TotalCost, @LineNum, @WhoCreated, @BOMCode) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemGroup", row.Cells(dgcBOMgroup.Index).Value.ToString)
                    SQL.AddParam("@ItemCode", row.Cells(dgcBOM_ItemCode.Index).Value)
                    SQL.AddParam("@Description", row.Cells(dgcBOM_ItemDesc.Index).Value.ToString)
                    SQL.AddParam("@QTY_Per_BOM", CDec(row.Cells(dgcQTY_Per_BOM.Index).Value))
                    SQL.AddParam("@UOM", row.Cells(dgcUOM.Index).Value.ToString)
                    SQL.AddParam("@GrossQTY", CDec(row.Cells(dgcPR.Index).Value))
                    SQL.AddParam("@UnitCost", CDec(row.Cells(dgcUC.Index).Value))
                    SQL.AddParam("@TotalCost", CDec(row.Cells(dgcTC.Index).Value))
                    SQL.AddParam("@BOMCode", row.Cells(dgcBOMCode.Index).Value)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next

            deleteSQL = " DELETE FROM tblBOM_DL WHERE TransID = '" & TransID & "' "
            SQL.ExecNonQuery(deleteSQL)

            line = 1
            For Each row As DataGridViewRow In dgvLabor.Rows
                If Not row.Cells(dgcDLactivity.Index).Value Is Nothing AndAlso Not row.Cells(dgcDLtotalCost.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblBOM_DL(TransId, Activity, RatePerHour, CrewNum, TimeMins, TotalMins, TotalCost, LineNumber, WhoCreated) " & _
                                " VALUES(@TransId, @Activity, @RatePerHour, @CrewNum, @TimeMins, @TotalMins, @TotalCost , @LineNumber, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@Activity", row.Cells(dgcDLactivity.Index).Value.ToString)
                    SQL.AddParam("@RatePerHour", CDec(row.Cells(dgcDLrate.Index).Value))
                    SQL.AddParam("@CrewNum", CDec(row.Cells(dgcDLcrewNo.Index).Value))
                    SQL.AddParam("@TimeMins", CDec(row.Cells(dgcDLtime.Index).Value))
                    SQL.AddParam("@TotalMins", CDec(row.Cells(dgcDLTotalMins.Index).Value))
                    SQL.AddParam("@TotalCost", CDec(row.Cells(dgcDLtotalCost.Index).Value))
                    SQL.AddParam("@LineNumber", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next


            deleteSQL = " DELETE FROM tblBOM_FOH WHERE TransID = '" & TransID & "' "
            SQL.ExecNonQuery(deleteSQL)

            line = 1
            For Each row As DataGridViewRow In dgvOverhead.Rows
                If Not row.Cells(dgcFOactivity.Index).Value Is Nothing AndAlso Not row.Cells(dgcFOcost.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblBOM_FOH(TransId, Activity, Machine, RatePerHour, KW, NumHours, TotalKWH, TotalCost, LineNumber, WhoCreated) " & _
                                " VALUES(@TransId, @Activity, @Machine, @RatePerHour, @KW, @NumHours, @TotalKWH, @TotalCost , @LineNumber, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@Activity", row.Cells(dgcFOactivity.Index).Value.ToString)
                    SQL.AddParam("@Machine", row.Cells(dgcFOmachine.Index).Value.ToString)
                    SQL.AddParam("@RatePerHour", CDec(row.Cells(dgcFOrate.Index).Value))
                    SQL.AddParam("@KW", CDec(row.Cells(dgcFOKW.Index).Value))
                    SQL.AddParam("@NumHours", CDec(row.Cells(dgcFOhrs.Index).Value))
                    SQL.AddParam("@TotalKWH", CDec(row.Cells(dgcFOtotalKWH.Index).Value))
                    SQL.AddParam("@TotalCost", CDec(row.Cells(dgcFOcost.Index).Value))
                    SQL.AddParam("@LineNumber", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "BOM_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub ComputeTotal()
        Dim b As Decimal = 0
        Dim a As Decimal = 0
        Dim c As Decimal = 0
        Dim d As Decimal = 0
        ' COMPUTE GROSS
        'For i As Integer = 0 To dgvItemList.Rows.Count - 1
        '    If Val(dgvItemList.Item(chGross.Index, i).Value) <> 0 Then
        '        If IsNumeric(dgvItemList.Item(chGross.Index, i).Value) Then
        '            a = a + Double.Parse(dgvItemList.Item(chGross.Index, i).Value).ToString
        '        End If
        '    End If
        'Next
        txtGross.Text = a.ToString("N2")

        ' COMPUTE DISCOUNT
        'For i As Integer = 0 To dgvItemList.Rows.Count - 1
        '    If Val(dgvItemList.Item(chDiscount.Index, i).Value) <> 0 Then
        '        If IsNumeric(dgvItemList.Item(chDiscount.Index, i).Value) Then
        '            b = b + Double.Parse(dgvItemList.Item(chDiscount.Index, i).Value)
        '        End If
        '    End If
        'Next
        txtDiscount.Text = b.ToString("N2")


        ' COMPUTE VAT
        'For i As Integer = 0 To dgvItemList.Rows.Count - 1
        '    If Val(dgvItemList.Item(chVATAmount.Index, i).Value) <> 0 Then
        '        If IsNumeric(dgvItemList.Item(chVATAmount.Index, i).Value) Then
        '            c = c + Double.Parse(dgvItemList.Item(chVATAmount.Index, i).Value).ToString
        '        End If
        '    End If
        'Next
        txtVAT.Text = c.ToString("N2")

    End Sub

    Private Sub dgvItemMaster_RowsRemoved(sender As System.Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
        ComputeTotal()
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("BOM_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("BOM")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadBOM(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("BOM_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            BOMNo = ""

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
        If Not AllowAccess("BOM_EDIT") Then
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
        If txtVCECode.Text = "" Then
            Msg("Please enter VCECode!", MsgBoxStyle.Exclamation)
        ElseIf dgvItemList.Rows.Count = 0 Then
            MsgBox("No BOM Selected!", MsgBoxStyle.Exclamation)
        ElseIf TransID = "" Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                TransID = GenerateTransID(ColumnID, DBTable)
                BOMNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                txtTransNum.Text = BOMNo
                SaveBOM()
                Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                BOMNo = txtTransNum.Text
                LoadBOM(TransID)
            End If
        Else
            If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                UpdateBOM()
                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                BOMNo = txtTransNum.Text
                LoadBOM(TransID)
            End If
        End If
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("BOM", TransID)
        f.Dispose()
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If BOMNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblBOM  WHERE BOM_No < '" & BOMNo & "' ORDER BY BOM_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadBOM(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If BOMNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblBOM  WHERE BOM_No > '" & BOMNo & "' ORDER BY BOM_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadBOM(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("BOM_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE  tblBOM SET Status ='Cancelled', WhoModified = @WhoModified, JO_Ref = 0 WHERE BOM_No = @BOM_No "
                        SQL.FlushParams()
                        BOMNo = txtTransNum.Text
                        SQL.AddParam("@BOM_No", BOMNo)
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

                        BOMNo = txtTransNum.Text
                        LoadBOM(BOMNo)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "BOM_No", BOMNo, BusinessType, BranchCode, "", activityStatus)
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
            End If
        ElseIf e.Alt = True Then
            If e.KeyCode = Keys.F4 Then
                If tsbPrint.Enabled = True Then
                    tsbPrint.PerformClick()
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

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        ' Toolstrip Buttons
        If BOMNo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadBOM(BOMNo)
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
        f.ShowDialog("JO-BOM")
        LoadJO(f.transID)
        f.Dispose()
    End Sub


    Private Sub txtVCEName_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
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

    Private Sub dgvItemList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellContentClick

    End Sub

    Private Sub txtCaseOrder_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs)
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) And Not e.KeyChar = "." Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCaseOrder_TextChanged(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub cbBOM_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbBOM.SelectedIndexChanged
        If cbBOM.SelectedIndex <> -1 Then
            txtLotSize.Text = GetLotSize(GetBOM(cbBOM.SelectedItem))
        End If
    End Sub

    Private Function GetLotSize(ByVal Code As String) As Decimal
        Dim query As String
        query = " SELECT QTY FROM tblBOM_FG WHERE BOM_Code ='" & Code & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("QTY").ToString
        Else
            Return 0
        End If
    End Function

    Private Sub btnExpload_Click(sender As System.Object, e As System.EventArgs) Handles btnExpload.Click
        dgvItemList.Rows.Clear()
        Dim total As Decimal = 0
        Dim query, bom_code As String


        bom_code = GetBOM(cbBOM.SelectedItem)
        'query = " SELECT BOM_Group, tblBOM_FG_Details.MaterialCode, ItemName, tblBOM_FG_Details.UOM, tblBOM_FG_Details.QTY, ISNULL(tblItem_Master.ID_SC,ISNULL(tblBOM_Group.StandardCost,0)) AS ID_SC  " & _
        ' " FROM tblBOM_FG INNER JOIN tblBOM_FG_Details " & _
        ' " ON tblBOM_FG.BOM_Code = tblBOM_FG_Details.BOM_Code " & _
        ' " LEFT JOIn tblItem_Master " & _
        ' " ON tblBOM_FG_Details.MaterialCode = tblItem_Master.ItemCode " & _
        ' " AND tblItem_Master.Status ='Active' " & _
        ' " LEFT JOIN tblBOM_Group " & _
        ' " ON tblBOM_Group.BOMGroup = tblBOM_FG_Details.BOM_Group " & _
        ' " AND tblBOM_Group.Status ='Active' " & _
        ' " WHERE tblBOM_FG.Status ='Active' " & _
        ' " AND tblBOM_FG.BOM_Code = '" & cbBOM.SelectedItem & "' " & _
        ' " ORDER BY  LineNumber "
        query = " SELECT tblBOM_FG_Details.BOM_Group,  " & _
                " ISNULL(tblBOM_SFG_Details.MaterialCode, tblBOM_FG_Details.MaterialCode) AS MaterialCode, " & _
                " CASE WHEN tblBOM_SFG_Details.MaterialCode IS NULL THEN tblitem_Master.ItemName ELSE SubItem.ItemName END AS ItemName,  " & _
                " ISNULL(tblBOM_SFG_Details.UOM, tblBOM_FG_Details.UOM) AS UOM, " & _
                " ISNULL(tblBOM_SFG_Details.QTY, tblBOM_FG_Details.QTY) AS QTY, " & _
                " CASE WHEN tblBOM_SFG_Details.MaterialCode IS NULL  " & _
                " THEN ISNULL(tblItem_Master.ID_SC,ISNULL(tblBOM_Group.StandardCost,0))  " & _
                " ELSE ISNULL(SubItem.ID_SC,0) " & _
                " END AS ID_SC " & _
                " FROM tblBOM_FG INNER JOIN tblBOM_FG_Details   " & _
                " ON tblBOM_FG.BOM_Code = tblBOM_FG_Details.BOM_Code  LEFT JOIn tblItem_Master   " & _
                " ON tblBOM_FG_Details.MaterialCode = tblItem_Master.ItemCode   " & _
                " AND tblItem_Master.Status ='Active'  LEFT JOIN tblBOM_Group   " & _
                " ON tblBOM_Group.BOMGroup = tblBOM_FG_Details.BOM_Group   " & _
                " AND tblBOM_Group.Status ='Active'   " & _
                " LEFT JOIN tblBOM_SFG " & _
                " ON tblBOM_FG_Details.MaterialCode = tblBOM_SFG.ItemCode " & _
                " LEFT JOIN tblBOM_SFG_Details " & _
                " ON tblBOM_SFG.BOM_Code = tblBOM_SFG_Details.BOM_Code " & _
                " LEFT JOINtblItem_Master  AS SubItem " & _
                " ON tblBOM_SFG_Details.MaterialCode = SubItem.ItemCode   " & _
                " AND SubItem.Status ='Active'  " & _
                " WHERE tblBOM_FG.Status ='Active'   " & _
                " AND tblBOM_FG.BOM_Code = '" & bom_code & "'  ORDER BY  tblBOM_FG_Details.LineNumber, tblBOM_SFG_Details.LineNumber "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            Dim cycle As Decimal = (CDec(txtQTY.Text) / CDec(txtLotSize.Text))
            TextBox1.Text = cycle
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                If row(1).ToString = "" Then
                    'query = " SELECT BOM_Group, tblItem_Master.ItemCode, ItemName, tblBOM_Maintenance_Details.UOM, tblBOM_Maintenance_Details.QTY, ISNULL(Stock,0) AS Stock  " & _
                    ' " FROM tblBOM_Maintenance_Header INNER JOIN tblBOM_Maintenance_Details " & _
                    ' " ON tblBOM_Maintenance_Header.BOM_Code = tblBOM_Maintenance_Details.BOM_Code " & _
                    ' " INNER JOIn tblItem_Master " & _
                    ' " ON tblBOM_Maintenance_Details.BOM_Group = tblItem_Master.ItemGroup " & _
                    ' " AND tblItem_Master.Status ='Active' " & _
                    ' "  LEFT JOIN  " & _
                    ' " ( " & _
                    ' " SELECT tblInventory.ItemCode, ISNULL(SUM(QTY),0) AS Stock " & _
                    ' " FROM tblInventory INNER JOIN tblItem_Master " & _
                    ' " ON tblInventory.ItemCode = tblItem_Master.ItemCode " & _
                    ' " GROUP BY tblInventory.ItemCode " & _
                    ' " )  AS Stock " & _
                    ' " ON tblItem_Master.ItemCode = Stock.ItemCode " & _
                    ' " WHERE tblBOM_Maintenance_Header.Status ='Active' " & _
                    ' " AND tblBOM_Maintenance_Header.BOM_Code = '" & cbBOM.SelectedItem & "' " & _
                    ' " AND tblBOM_Maintenance_Details.BOM_Group = '" & row(0).ToString & "' " & _
                    ' " ORDER BY LineNumber "
                    'SQL.ReadQuery(query)
                    'Dim OrderBalance As Decimal = 0
                    'While SQL.SQLDR.Read
                    '    Dim OrderedQTY As Decimal = SQL.SQLDR("QTY").ToString
                    '    OrderBalance = OrderedQTY
                    '    Dim Stock As Decimal = SQL.SQLDR("Stock").ToString
                    '    If Stock > OrderBalance Then
                    '        dgvItemList.Rows.Add({row(0).ToString, SQL.SQLDR("ItemCode").ToString, SQL.SQLDR("ItemName").ToString, row(3).ToString, OrderBalance, OrderBalance * txtQTY.Text})
                    '        Exit While
                    '    ElseIf Stock > 0 Then
                    '        dgvItemList.Rows.Add({row(0).ToString, SQL.SQLDR("ItemCode").ToString, SQL.SQLDR("ItemName").ToString, row(3).ToString, Stock, Stock * txtQTY.Text})
                    '        OrderBalance = OrderBalance - Stock
                    ' End If

                    'End While\
                    total = row(4) * cycle
                    dgvItemList.Rows.Add({row(0).ToString, "", "", row(3).ToString, CDec(row(4)).ToString("N3"), CDec(total).ToString("N3"), CDec(row(5)).ToString("N2"), CDec(row(5) * (row(4) * cycle)).ToString("N2")})
                Else
                    dgvItemList.Rows.Add({row(0).ToString, row(1).ToString, row(2).ToString, row(3).ToString, CDec(row(4)).ToString("N3"), CDec(row(4) * cycle).ToString("N3"), CDec(row(5)).ToString("N2"), CDec(row(5) * (row(4) * cycle)).ToString("N2")})
                End If

            Next
            LoadLabor(bom_code, cycle)
            LoadOverhead(bom_code, cycle)
            ComputeTotals()
        End If
    End Sub

    Public Sub LoadOverhead(ByVal Code As String, ByVal cycle As Integer)
        Dim query As String
        Dim ctr As Integer = 0
        query = " SELECT   Activity, Machine, RatePerHour, KW, NumHours, TotalKWH, TotalCost " & _
                " FROM     tblBOM_Overhead " & _
                " WHERE    BOMCode = '" & Code & "'" & _
                " ORDER BY LineNumber "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvOverhead.Rows.Add(New String() {row(0).ToString, _
                                             row(1).ToString, _
                                             row(2).ToString, _
                                             row(3).ToString, _
                                             row(4).ToString * cycle, row(5).ToString * cycle, row(6).ToString * cycle})
                ctr += 1
            Next

        End If
    End Sub

    Public Sub LoadLabor(ByVal Code As String, ByVal cycle As Integer)
        Dim query As String
        Dim ctr As Integer = 0
        query = " SELECT   Activity, RatePerHour, CrewNum, TimeMins, TotalMins, TotalCost " & _
                " FROM     tblBOM_Labor " & _
                " WHERE    BOMCode = '" & Code & "'" & _
                " ORDER BY LineNumber "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvLabor.Rows.Add(New String() {row(0).ToString, _
                                             row(1).ToString, _
                                             row(2).ToString, _
                                             row(3).ToString * cycle, _
                                             row(4).ToString * cycle, row(5).ToString * cycle})
                ctr += 1
            Next

        End If
    End Sub

    Private Function GetINVTY(ItemCode As String) As Decimal
        Dim query As String
        query = " SELECT ISNULL(SUM(QTY),0) AS Stock " & _
                " FROM tblInventory INNER JOIN tblItem_Master " & _
                " ON tblInventory.ItemCode = tblItem_Master.ItemCode " & _
                " WHERE tblInventory.ItemCode = '" & ItemCode & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("Stock")
        Else
            Return 0
        End If
    End Function

    Private Sub txtLotSize_TextChanged(sender As System.Object, e As System.EventArgs)
        If IsNumeric(txtLotSize.Text) Then
            ComputeTotals()
        End If
    End Sub

    Private Sub ComputeTotals()
        Dim A, B, C As Decimal
        A = 0
        For Each row As DataGridViewRow In dgvItemList.Rows
            If Not row.Cells(dgcQTY_Per_BOM.Index).Value Is Nothing AndAlso IsNumeric(row.Cells(dgcQTY_Per_BOM.Index).Value) Then
                '       row.Cells(dgcPR.Index).Value = CDec(row.Cells(dgcQTY_Per_BOM.Index).Value) / CDec(txtLotSize.Text)
            End If
            A += row.Cells(dgcTC.Index).Value
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

    Private Sub tsmiBOMlist_Click(sender As System.Object, e As System.EventArgs) Handles tsmiBOMlist.Click
        frmBOM_FG.ShowDialog()
    End Sub


    Private Sub txtDescription_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtDescription.TextChanged

    End Sub

    Dim eColIndex As Integer = 0
    Private Sub dgcHeader_ItemList_CellBeginEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgcHeader_ItemList.CellBeginEdit
        eColIndex = e.ColumnIndex
    End Sub

    Private Sub dgcHeader_ItemList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgcHeader_ItemList.CellContentClick

    End Sub

    Private Sub dgcHeader_ItemList_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgcHeader_ItemList.CellEndEdit
        Dim rowIndex As Integer = dgcHeader_ItemList.CurrentCell.RowIndex
        Dim colindex As Integer = dgcHeader_ItemList.CurrentCell.ColumnIndex
        Select Case colindex
            Case chHeader_BOMCode.Index
                If dgcHeader_ItemList.Item(chHeader_ItemCode.Index, e.RowIndex).Value <> "" Then
                    dgcHeader_ItemList.Item(chHeader_LotSize.Index, dgcHeader_ItemList.SelectedCells(0).RowIndex).Value = GetLotSize(GetBOM(dgcHeader_ItemList.Item(chHeader_BOMCode.Index, dgcHeader_ItemList.SelectedCells(0).RowIndex).Value))
                End If
        End Select
    End Sub

    Private Sub dgcHeader_ItemList_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dgcHeader_ItemList.CurrentCellDirtyStateChanged
        If eColIndex = chHeader_BOMCode.Index And TypeOf (dgcHeader_ItemList.CurrentRow.Cells(chHeader_BOMCode.Index)) Is DataGridViewComboBoxCell Then
            dgcHeader_ItemList.EndEdit()
        End If
    End Sub

    Private Sub btnMultipleExplode_Click(sender As System.Object, e As System.EventArgs) Handles btnMultipleExplode.Click
        dgvItemList.Rows.Clear()
        dgvLabor.Rows.Clear()
        dgvOverhead.Rows.Clear()
        Dim total As Decimal = 0
        Dim query, bom_code As String

        For Each row As DataGridViewRow In dgcHeader_ItemList.Rows
            If Not row.Cells(chHeader_ItemCode.Index).Value Is Nothing AndAlso Not row.Cells(chHeader_BOMCode.Index).Value Is Nothing Then
                bom_code = GetBOM(row.Cells(chHeader_BOMCode.Index).Value)
                Dim cycle As Decimal = ((CDec(row.Cells(chHeader_QTY.Index).Value) * CDec(row.Cells(chHeader_Size.Index).Value)) / CDec(row.Cells(chHeader_LotSize.Index).Value))

                query = " SELECT tblBOM_FG_Details.BOM_Group,  " & _
                        " ISNULL(tblBOM_SFG_Details.MaterialCode, tblBOM_FG_Details.MaterialCode) AS MaterialCode, " & _
                        " CASE WHEN tblBOM_SFG_Details.MaterialCode IS NULL THEN tblitem_Master.ItemName ELSE SubItem.ItemName END AS ItemName,  " & _
                        " ISNULL(tblBOM_SFG_Details.UOM, tblBOM_FG_Details.UOM) AS UOM, " & _
                        " ISNULL(tblBOM_SFG_Details.QTY, tblBOM_FG_Details.QTY) AS QTY, " & _
                        " CASE WHEN tblBOM_SFG_Details.MaterialCode IS NULL  " & _
                        " THEN ISNULL(tblItem_Master.ID_SC,ISNULL(tblBOM_Group.StandardCost,0))  " & _
                        " ELSE ISNULL(SubItem.ID_SC,0) " & _
                        " END AS ID_SC " & _
                        " FROM tblBOM_FG INNER JOIN tblBOM_FG_Details   " & _
                        " ON tblBOM_FG.BOM_Code = tblBOM_FG_Details.BOM_Code  LEFT JOIn tblItem_Master   " & _
                        " ON tblBOM_FG_Details.MaterialCode = tblItem_Master.ItemCode   " & _
                        "  AND tblItem_Master.Status ='Active'  LEFT JOIN tblBOM_Group   " & _
                        "  ON tblBOM_Group.BOMGroup = tblBOM_FG_Details.BOM_Group   " & _
                        "  AND tblBOM_Group.Status ='Active'   " & _
                        " LEFT JOIN tblBOM_SFG " & _
                        " ON tblBOM_FG_Details.MaterialCode = tblBOM_SFG.ItemCode " & _
                        " LEFT JOIN tblBOM_SFG_Details " & _
                        " ON tblBOM_SFG.BOM_Code = tblBOM_SFG_Details.BOM_Code " & _
                        " LEFT JOIn tblItem_Master  AS SubItem " & _
                        " ON tblBOM_SFG_Details.MaterialCode = SubItem.ItemCode   " & _
                        " AND SubItem.Status ='Active'  " & _
                        " WHERE tblBOM_FG.Status ='Active'   " & _
                        " AND tblBOM_FG.BOM_Code = '" & bom_code & "'  ORDER BY  tblBOM_FG_Details.LineNumber, tblBOM_SFG_Details.LineNumber "
                SQL.GetQuery(query)
                If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                    For Each row1 As DataRow In SQL.SQLDS.Tables(0).Rows
                        If row1(1).ToString = "" Then
                            total = row1(4) * cycle
                            dgvItemList.Rows.Add({row1(0).ToString, "", "", row1(3).ToString, CDec(row1(4)).ToString("N3"), CDec(total).ToString("N3"), CDec(row1(5)).ToString("N2"), CDec(row1(5) * (row1(4) * cycle)).ToString("N2"), bom_code})
                        Else
                            dgvItemList.Rows.Add({row1(0).ToString, row1(1).ToString, row1(2).ToString, row1(3).ToString, CDec(row1(4)).ToString("N3"), CDec(row1(4) * cycle).ToString("N3"), CDec(row1(5)).ToString("N2"), CDec(row1(5) * (row1(4) * cycle)).ToString("N2"), bom_code})
                        End If
                    Next
                    LoadLabor(bom_code, cycle)
                    LoadOverhead(bom_code, cycle)
                End If
            End If
        Next
        ComputeTotals()

    End Sub




    Private Sub dgvItemList_ItemList_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs)
        If e.ColumnIndex <> -1 AndAlso e.RowIndex <> -1 AndAlso e.Button = MouseButtons.Right Then
            Dim c As DataGridViewCell = sender(e.ColumnIndex, e.RowIndex)
            If Not c.Selected Then
                c.DataGridView.ClearSelection()
                c.DataGridView.CurrentCell = c
                c.Selected = True
            End If
        End If


    End Sub

    Private Sub dgvItemList_ItemList_KeyDown(sender As Object, e As KeyEventArgs)
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
                Dim code As String = dgvItemList.Rows(dgvItemList.CurrentCell.RowIndex).Cells(dgcBOM_ItemCode.Index).Value.ToString
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