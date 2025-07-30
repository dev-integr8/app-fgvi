Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmDR
    Dim TransID, RefID, JETransID As String
    Dim DRNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "DR"
    Dim ColumnPK As String = "DR_No"
    Dim DBTable As String = "tblDR"
    Dim ColumnID As String = "TransID"
    Dim TransAuto As Boolean
    Dim SO_ID, PL_ID, PO_ID As Integer
    Dim ForApproval As Boolean = False

    Dim Valid As Boolean = True
    Dim InvalidTemplate As Boolean = False
    Dim path As String
    Dim templateName As String = "TEMPLATE_DR"
    Public excelPW As String = "@dm1nEvo"

    Public Overloads Function ShowDialog(ByVal docnumber As String) As Boolean
        TransID = docnumber
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmDR_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            dtpActual.Value = Date.Today.Date
            If TransID <> "" Then
                LoadDR(TransID)
            End If
            tsbSearch.Enabled = True
            tsbNew.Enabled = True
            tsbEdit.Enabled = False
            tsbSave.Enabled = False
            tsbCancel.Enabled = False
            tsbClose.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbUpload.Enabled = False
            tsbDownload.Enabled = False
            tsbExit.Enabled = True
            tsbPrint.Enabled = False
            tsbCopy.Enabled = False
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
        If Value = True Then
            dgvItemList.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemList.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        txtRemarks.Enabled = Value
        chkForECS.Enabled = Value
        dtpDocDate.Enabled = Value
        dtpActual.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub


    Private Sub LoadDR(ByVal ID As String)
        Dim query, Currency As String
        query = " SELECT   TransID, DR_No, VCECode, DateDR, DateDeliver, Remarks, Status, SO_Ref, PL_Ref, PO_Ref, ForECS, Currency, ISNULL(Exchange_Rate,0) AS Exchange_Rate " & _
                " FROM     tblDR " & _
                " WHERE    TransId = '" & ID & "' " & _
                " ORDER BY TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            DRNo = SQL.SQLDR("DR_No").ToString
            txtTransNum.Text = DRNo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            dtpDocDate.Text = SQL.SQLDR("DateDR").ToString
            dtpActual.Value = IIf(IsDate(SQL.SQLDR("DateDeliver")), SQL.SQLDR("DateDeliver"), Date.Today)
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            Currency = SQL.SQLDR("Currency").ToString
            txtConversion.Text = CDec(SQL.SQLDR("Exchange_Rate")).ToString("N4")
            txtStatus.Text = SQL.SQLDR("Status").ToString
            chkForECS.Checked = SQL.SQLDR("ForECS").ToString
            SO_ID = SQL.SQLDR("SO_Ref").ToString
            PL_ID = SQL.SQLDR("PL_Ref").ToString
            PO_ID = SQL.SQLDR("PO_Ref").ToString
            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            txtSORef.Text = LoadSONo(SO_ID)
            txtPLRef.Text = LoadPLNo(PL_ID)
            txtPORef.Text = LoadPONo(PO_ID)

            disableEvent = True
            cbCurrency.Items.Clear()
            For Each item In LoadVCECurrency(txtVCECode.Text)
                cbCurrency.Items.Add(item)
            Next
            If cbCurrency.Items.Count = 0 Then
                cbCurrency.Items.Add(BaseCurrency)
            End If
            cbCurrency.SelectedItem = Currency

            If cbCurrency.SelectedItem <> BaseCurrency Then
                lblConversion.Visible = True
                txtConversion.Visible = True
            Else
                lblConversion.Visible = False
                txtConversion.Visible = False
            End If
            disableEvent = False
            LoadDRDetails(TransID)
            LoadAccountingEntry(TransID)

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
            tsbUpload.Enabled = False
            tsbDownload.Enabled = False
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
            EnableControl(False)
        Else
            ClearText()
        End If
    End Sub

    Private Sub LoadAccountingEntry(ByVal TransID As Integer)
        Try
            Dim query As String
            query = " SELECT JE_No, VCECode, ISNULL(VCEName,'') AS VCEName, AccntCode, AccntTitle, Particulars, " & _
                    "        ISNULL(Debit,0) AS Debit, ISNULL(Credit,0) AS Credit " & _
                    " FROM   View_GL_Transaction  " & _
                    " WHERE  RefType ='DR' AND RefTransID ='" & TransID & "' "
            SQL.ReadQuery(query)
            dgvEntry.Rows.Clear()
            If SQL.SQLDR.HasRows Then
                While SQL.SQLDR.Read
                    JETransID = SQL.SQLDR("JE_No").ToString
                    dgvEntry.Rows.Add(SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccntTitle").ToString, _
                                      IIf(CDec(SQL.SQLDR("Debit")) = 0, "", CDec(SQL.SQLDR("Debit")).ToString("N2")), If(CDec(SQL.SQLDR("Credit")) = 0, "", CDec(SQL.SQLDR("Credit")).ToString("N2")), _
                                      SQL.SQLDR("Particulars").ToString, SQL.SQLDR("VCECode").ToString, SQL.SQLDR("VCEName").ToString)
                End While
                TotalDBCR()
            Else
                JETransID = 0
                dgvEntry.Rows.Clear()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
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

    Private Function LoadPLNo(PL_ID As Integer) As String
        Dim query As String
        query = " SELECT PL_No FROM tblPL WHERE TransID = '" & PL_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("PL_No")
        Else
            Return ""
        End If
    End Function

    Private Function LoadPONo(PO_ID As Integer) As String
        Dim query As String
        query = " SELECT PO_No FROM tblPO WHERE TransID = '" & PO_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("PO_No")
        Else
            Return ""
        End If
    End Function

    Protected Sub LoadDRDetails(ByVal TransID As String)
        dgvItemList.Rows.Clear()
        Dim ctr As Integer = 0
        Dim query As String
        'query = " SELECT tblDR_Details.ItemCode, tblDR_Details.Description, tblDR_Details.UOM,  tblDR_Details.QTY AS QTY, " & _
        '        " tblDR_Details.WHSE, ISNULL(tblSO_Details.UnitPrice,0) AS UnitPrice, ISNULL(Ref_SO_RecID,0) AS Ref_SO_RecID " & _
        '        " FROM tblDR INNER JOIN tblDR_Details " & _
        '        " ON tblDR.TransID = tblDR_Details.TransID " & _
        '        " LEFT JOIN tblSO " & _
        '        " ON tblSO.TransID = tblDR.SO_Ref  " & _
        '        " LEFT JOIN tblSO_Details " & _
        '        " ON tblSO.TransID = tblSO_Details.TransID " & _
        '        " AND tblSO_Details.ItemCode = tblDR_Details.ItemCode " & _
        '        " AND tblSO_Details.RecordID = tblDR_Details.Ref_SO_RecID " & _
        '        " WHERE tblDR_Details.TransId = " & TransID & " " & _
        '        " ORDER BY  tblDR_Details.LineNum "
        query = " SELECT tblDR_Details.ItemCode, tblDR_Details.Description, tblDR_Details.UOM,  tblDR_Details.QTY AS QTY, " & _
               " tblDR_Details.WHSE, ISNULL(tblDR_Details.UnitPrice,0) AS UnitPrice, ISNULL(tblDR_Details.GrossAmount,0) AS GrossAmount, " & _
               " ISNULL(tblDR_Details.VATAmount,0) AS VATAmount, ISNULL(tblDR_Details.NetAmount,0) AS NetAmount, ISNULL(Ref_SO_RecID,0) AS Ref_SO_RecID, tblDR_Details.AccntCode, " & _
               " ISNULL(tblDR_Details.DRRate,0) AS DRRate, ISNULL(tblDR_Details.DRPrice,0) AS DRPrice, ISNULL(tblDR_Details.VATable,0) AS VATable, " & _
               " ISNULL(tblDR_Details.VATInc,0) AS VATInc, ISNULL(tblDR_Details.StockQTY,0) AS StockQTY,  ISNULL(SerialNo,'') AS SerialNo, ISNULL(LotNo,'') AS LotNo, " & _
               " ISNULL(tblDR_Details.DateExpired,'') AS DateExpired, ISNULL(tblDR_Details.Size,'') AS Size, ISNULL(tblDR_Details.Color,'') AS Color " & _
               " FROM tblDR INNER JOIN tblDR_Details " & _
               " ON tblDR.TransID = tblDR_Details.TransID " & _
               " LEFT JOIN tblSO " & _
               " ON tblSO.TransID = tblDR.SO_Ref  " & _
               " LEFT JOIN tblSO_Details " & _
               " ON tblSO.TransID = tblSO_Details.TransID " & _
               " AND tblSO_Details.ItemCode = tblDR_Details.ItemCode " & _
               " AND tblSO_Details.RecordID = tblDR_Details.Ref_SO_RecID " & _
               " WHERE tblDR_Details.TransId = " & TransID & " " & _
               " ORDER BY  tblDR_Details.LineNum "
        dgvItemList.Rows.Clear()
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvItemList.Rows.Add(row(0).ToString, "", row(1).ToString, row(2).ToString, _
                                      row(3).ToString, GetWHSEDesc(row(4).ToString), row(5).ToString, _
                                      row(6).ToString, row(7).ToString, row(8).ToString, row(9).ToString, _
                                      row(13).ToString, row(14).ToString, row(10).ToString, row(11).ToString, _
                                      row(12).ToString, row(15).ToString, row(18).ToString, row(17).ToString, _
                                       row(16).ToString, row(19).ToString, row(20).ToString)
                LoadWHSE(ctr)
                LoadUOM(row(0).ToString, ctr)
                LoadColor(row(0).ToString, ctr)
                LoadSize(row(0).ToString, ctr)
                ctr += 1
            Next
            LoadBarCode()
        End If
    End Sub

    Private Sub txtVCEName_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.txtFilter.Text = txtVCEName.Text
            f.Type = "ALL"
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
        PL_ID = 0
        SO_ID = 0
        PO_ID = 0
        Valid = True
        txtTransNum.Clear()
        txtVCECode.Clear()
        txtVCEName.Clear()
        chkForECS.Checked = False
        dgvItemList.Rows.Clear()
        dgvEntry.Rows.Clear()
        txtRemarks.Clear()
        txtSORef.Clear()
        txtPLRef.Clear()
        txtPORef.Clear()
        txtStatus.Text = "Open"
        dtpActual.Value = Date.Today.Date
        dtpDocDate.MinDate = GetMaxPEC().AddDays(1)
        If Inv_ComputationMethod <> "SC" Then
            dtpDocDate.MinDate = GetMaxInventoryDate()
        End If
        dtpDocDate.Value = Date.Today.Date
        cbCurrency.Items.Clear()
        txtConversion.Text = ""
    End Sub


    Private Sub dgvItemList_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellEndEdit
        Try
            Dim itemCode, UOM As String
            Dim rowIndex As Integer = dgvItemList.CurrentCell.RowIndex
            Dim colindex As Integer = dgvItemList.CurrentCell.ColumnIndex
            Select Case colindex
                Case chItemCode.Index
                    If dgvItemList.Item(chItemCode.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemList.Item(chItemCode.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        ' f.ShowDialog("All Item", itemCode)
                        f.ShowDialog("SerialItem", itemCode, "ItemCode")
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItem(itemCode)
                        End If
                        dgvItemList.Rows.RemoveAt(e.RowIndex)
                        f.Dispose()
                    End If
                Case chBarCode.Index
                    If dgvItemList.Item(chBarCode.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemList.Item(chBarCode.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        ' f.ShowDialog("All Item", itemCode)
                        f.ShowDialog("SerialItem", itemCode, "Barcode")
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
                        ' f.ShowDialog("All Item", itemCode)
                        f.ShowDialog("SerialItem", itemCode, "ItemName")
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItem(itemCode)
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
                Case chDRRate.Index
                    If dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value = "" OrElse IsNothing(dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value) Then
                        dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value = 0
                    ElseIf IsNumeric(dgvItemList.Item(chDRRate.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemList.Item(chQTY.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex)
                        ComputeTotal()
                    End If
                Case chWHSE.Index
                    If dgvItemList.Item(chItemCode.Index, e.RowIndex).Value <> "" Then
                        LoadStock()
                    End If

                Case chDateExpired.Index
                    If IsDate(dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value) Then
                        dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value = CDate(dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value)
                    End If
                    LoadPeriod()

            End Select
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


    Private Sub LoadStock()


        Dim query As String
        Dim itemCode, UOM As String
        Dim StockQTY As Decimal = 0
        Dim IssueQTY As Decimal = 0
        Dim IssuedQTY As Decimal = 0
        Dim ReqQTY As Decimal = 0
        For Each row As DataGridViewRow In dgvItemList.Rows
            Dim WHSE As String
            If row.Cells(chWHSE.Index).Value = "" Then
                WHSE = ""
            Else
                WHSE = GetWHSE(row.Cells(chWHSE.Index).Value)
            End If
            If Not IsNothing(row.Cells(chItemCode.Index).Value) Then
                itemCode = row.Cells(chItemCode.Index).Value.ToString
                If Not IsNothing(row.Cells(chUOM.Index).Value) Then UOM = row.Cells(chUOM.Index).Value Else UOM = ""
                If Not IsNumeric(row.Cells(chQTY.Index).Value) Then ReqQTY = 0 Else ReqQTY = CDec(row.Cells(chQTY.Index).Value)

                query = " SELECT ISNULL(SUM(QTY),0) AS QTY " & _
                        " FROM viewItem_Stock " & _
                        " WHERE ItemCode ='" & itemCode & "' " & _
                        " AND Description = '" & WHSE & "' " & _
                        " AND UOM = '" & UOM & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    StockQTY = SQL.SQLDR("QTY") + ConvertToAltUOM(itemCode, UOM, IssuedQTY)
                    If StockQTY >= ReqQTY Then ' IF AVAILABLE STOCK IS GREATER THAN THE BOM REQUIREMENT THEN ISSUE QTY SHOULD BE THE SAME AS BOM QTY
                        IssueQTY = ReqQTY
                    Else ' IF AVAILABLE STOCK IS LESS THAN THE BOM REQUIREMENT THEN ISSUE QTY SHOULD BE ONLY THE STOCK QTY
                        IssueQTY = StockQTY
                    End If
                End If
                row.Cells(dgcStockQTY.Index).Value = CDec(StockQTY).ToString("N2")

            End If

        Next
        dgvItemList.Columns(dgcStockQTY.Index).Visible = True
    End Sub

    Private Function ConvertToAltUOM(ByVal itemCode As String, UOM As String, QTY As Decimal)
        Dim query As String
        Dim ConvertQTY As Decimal = 0
        query = " SELECT ISNULL(QTY,0) as QTY  FROM viewItem_UOM WHERE GroupCode ='" & itemCode & "' AND UnitCode ='" & UOM & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            ConvertQTY = QTY / CDec(SQL.SQLDR("QTY"))
        End If
        Return ConvertQTY
    End Function

    Private Sub Recompute(ByVal RowID As Integer, Optional ColID As Integer = 1)
        Dim gross, VAT, discount, net, baseVAT, DRPrice As Decimal
        If RowID <> -1 Then
            If IsNumeric(dgvItemList.Item(chGross.Index, RowID).Value) Then
                ' GET GROSS AMOUNT (VAT INCLUSIVE)
                gross = CDec(dgvItemList.Item(chUnitPrice.Index, RowID).Value) * CDec(dgvItemList.Item(chQTY.Index, RowID).Value)
                DRPrice = CDec(dgvItemList.Item(chDRRate.Index, RowID).Value) * CDec(dgvItemList.Item(chQTY.Index, RowID).Value)
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
                            baseVAT = (gross / 1.12)
                            VAT = CDec(baseVAT * 0.12).ToString("N2")
                        End If

                    End If
                Else
                    If dgvItemList.Item(chVAT.Index, RowID).Value = False Then
                        VAT = 0
                        dgvItemList.Item(chVATInc.Index, RowID).ReadOnly = True
                    Else
                        dgvItemList.Item(chVATInc.Index, RowID).ReadOnly = False
                        If dgvItemList.Item(chVATInc.Index, RowID).Value = True Then ' IF VAT INCLUSIVE, BASE AMOUNT WILL BE GROSS / 1.12
                            baseVAT = (gross / 1.12)
                        End If
                        VAT = CDec(baseVAT * 0.12).ToString("N2")
                    End If
                End If

                net = baseVAT - discount + VAT + DRPrice
                dgvItemList.Item(chDRPrice.Index, RowID).Value = Format(DRPrice, "#,###,###,###.00").ToString()
                dgvItemList.Item(chGross.Index, RowID).Value = Format(gross, "#,###,###,###.00").ToString()
                dgvItemList.Item(chVATAmount.Index, RowID).Value = Format(VAT, "#,###,###,###.00").ToString()
                dgvItemList.Item(chNetAmount.Index, RowID).Value = Format(net, "#,###,###,###.00").ToString()
                ComputeTotal()

            End If
        End If

    End Sub

    Private Sub ComputeTotal()
        If dgvItemList.Rows.Count > 0 Then
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

            ' COMPUTE NET
            For i As Integer = 0 To dgvItemList.Rows.Count - 1
                If Val(dgvItemList.Item(chNetAmount.Index, i).Value) <> 0 Then
                    If IsNumeric(dgvItemList.Item(chNetAmount.Index, i).Value) Then
                        d = d + Double.Parse(dgvItemList.Item(chNetAmount.Index, i).Value).ToString
                    End If
                End If
            Next
        End If



    End Sub


    Public Sub LoadItem(ByVal ID As String, Optional UOM As String = "", Optional QTY As Integer = 1)
        Try
            Dim query, AccntCode, ItemCode As String
            Dim netPrice As Decimal
            query = " SELECT  ItemCode,  ItemName, UOM AS ItemUOM,  " & _
                    "         ISNULL(ID_SC,0) AS ID_SC, WHSE AS  ID_Warehouse, AD_Cos, DateExpired, LotNo, SerialNo, EB, Size, Color " & _
                    " FROM    viewItem_StockSerial " & _
                    " WHERE   TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", ID)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                If UOM = "" Then
                    UOM = SQL.SQLDR("ItemUOM").ToString
                End If
                ItemCode = SQL.SQLDR("ItemCode").ToString
                netPrice = SQL.SQLDR("ID_SC")
                AccntCode = SQL.SQLDR("AD_Cos").ToString
                AccntCode = SQL.SQLDR("AD_Cos").ToString
                dgvItemList.Rows.Add(New String() {ItemCode, "", _
                                              SQL.SQLDR("ItemName").ToString, _
                                             UOM, _
                                              QTY, _
                                              GetWHSEDesc(SQL.SQLDR("ID_Warehouse").ToString), _
                                              Format(netPrice, "#,###,###,###.00").ToString, _
                                                   Format(netPrice, "#,###,###,###.00").ToString, _
                                                   "0.00", _
                                                Format(netPrice, "#,###,###,###.00").ToString, _
                                                   "", _
                                                  False, _
                                                  False, _
                                                  AccntCode, _
                                                   "0.00", _
                                                    "0.00", _
                                                    SQL.SQLDR("EB").ToString, _
                                                   SQL.SQLDR("DateExpired").ToString, _
                                                   SQL.SQLDR("LotNo").ToString, _
                                                   SQL.SQLDR("SerialNo").ToString, _
                                                   SQL.SQLDR("Size").ToString, _
                                                   SQL.SQLDR("Color").ToString})
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


    Private Sub SaveDR()
        Try
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                                " tblDR(TransID, DR_No, BranchCode, BusinessCode, VCECode, DateDR, DateDeliver, " & _
                                "       Remarks, Currency, Exchange_Rate, SO_Ref, PL_Ref, PO_Ref, CusSONo, ForECS,  PlateNumber, DriverName, WhoCreated, Status) " & _
                                " VALUES (@TransID, @DR_No, @BranchCode, @BusinessCode, @VCECode,  @DateDR, @DateDeliver, " & _
                                "       @Remarks, @Currency, @Exchange_Rate, @SO_Ref, @PL_Ref, @PO_Ref, @CusSONo, @ForECS, @PlateNumber, @DriverName, @WhoCreated, @Status) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@DR_No", DRNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateDR", dtpDocDate.Value.Date)
            SQL.AddParam("@DateDeliver", dtpActual.Value.Date)
            SQL.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@SO_Ref", SO_ID)
            SQL.AddParam("@PL_Ref", PL_ID)
            SQL.AddParam("@PO_Ref", PO_ID)
            SQL.AddParam("@CusSONo", txtCustSO.Text)
            SQL.AddParam("@PlateNumber", txtPlateNumber.Text)
            SQL.AddParam("@DriverName", txtDriverName.Text)
            SQL.AddParam("@ForECS", chkForECS.Checked)
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            Dim ItemCode, Description, UOM, WHSE, Ref_SO_RecID, AccntCode, DateExpired, LotNo, SerialNo, Size, Color As String
            Dim QTY, UnitCost, UnitPrice, GrossAmount, NetAmount, DRRate, DRPrice, VATAmount, StockQTY As Decimal
            Dim VAT, VATInc As Boolean
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                    Description = IIf(row.Cells(chItemDesc.Index).Value = Nothing, "", row.Cells(chItemDesc.Index).Value)
                    UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                    Ref_SO_RecID = IIf(row.Cells(chRefRecID.Index).Value = Nothing, "", row.Cells(chRefRecID.Index).Value)
                    UnitPrice = IIf(row.Cells(chUnitPrice.Index).Value = Nothing, "0.00", CDec(row.Cells(chUnitPrice.Index).Value).ToString("N2"))
                    GrossAmount = IIf(row.Cells(chGross.Index).Value = Nothing, "0.00", CDec(row.Cells(chGross.Index).Value).ToString("N2"))
                    NetAmount = IIf(row.Cells(chNetAmount.Index).Value = Nothing, "0.00", CDec(row.Cells(chNetAmount.Index).Value).ToString("N2"))
                    AccntCode = IIf(row.Cells(chAccnt.Index).Value = Nothing, "", row.Cells(chAccnt.Index).Value)
                    DRRate = IIf(row.Cells(chDRRate.Index).Value = Nothing, "0.00", CDec(row.Cells(chDRRate.Index).Value).ToString("N2"))
                    DRPrice = IIf(row.Cells(chDRPrice.Index).Value = Nothing, "0.00", CDec(row.Cells(chDRPrice.Index).Value).ToString("N2"))
                    VATAmount = IIf(row.Cells(chVATAmount.Index).Value = Nothing, "0.00", CDec(row.Cells(chVATAmount.Index).Value).ToString("N2"))
                    VAT = IIf(row.Cells(chVAT.Index).Value = Nothing, False, row.Cells(chVAT.Index).Value)
                    VATInc = IIf(row.Cells(chVATInc.Index).Value = Nothing, False, row.Cells(chVATInc.Index).Value)
                    DateExpired = IIf(row.Cells(chDateExpired.Index).Value = Nothing, "", row.Cells(chDateExpired.Index).Value)
                    LotNo = IIf(row.Cells(chLotNo.Index).Value = Nothing, "", row.Cells(chLotNo.Index).Value)
                    SerialNo = IIf(row.Cells(chSerialNo.Index).Value = Nothing, "", row.Cells(chSerialNo.Index).Value)
                    Size = IIf(row.Cells(chSize.Index).Value = Nothing, "", row.Cells(chSize.Index).Value)
                    Color = IIf(row.Cells(chColor.Index).Value = Nothing, "", row.Cells(chColor.Index).Value)
                    If Inv_ComputationMethod = "SC" Then
                        UnitCost = GetStandardCost(ItemCode)
                    Else
                        UnitCost = GetAverageCost(ItemCode)
                    End If
                    StockQTY = CDec(row.Cells(dgcStockQTY.Index).Value)
                    If IsNumeric(row.Cells(chQTY.Index).Value) Then
                        QTY = CDec(row.Cells(chQTY.Index).Value)
                    Else
                        QTY = 1
                    End If
                    WHSE = IIf(row.Cells(chWHSE.Index).Value = Nothing, "", GetWHSECode(row.Cells(chWHSE.Index).Value))
                    insertSQL = " INSERT INTO " & _
                         " tblDR_Details(TransId, ItemCode, Description, UOM, QTY, UnitPrice, GrossAmount, VATAmount, NetAmount, " & _
                         "  VATable, VATinc, WHSE, Ref_SO_RecID, AccntCode, DRRate, DRPrice, StockQTY, LineNum, WhoCreated, " & _
                         "  SerialNo, LotNo, DateExpired, Size, Color) " & _
                         " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @UnitPrice, @GrossAmount, @VATAmount, @NetAmount, " & _
                         "  @VATable, @VATinc,  @WHSE, @Ref_SO_RecID, @AccntCode,  @DRRate, @DRPrice, @StockQTY, @LineNum, @WhoCreated, " & _
                         "  @SerialNo, @LotNo, @DateExpired, @Size, @Color) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", ItemCode)
                    SQL.AddParam("@Description", Description)
                    SQL.AddParam("@UOM", UOM)
                    SQL.AddParam("@QTY", QTY)
                    SQL.AddParam("@UnitPrice", UnitPrice)
                    SQL.AddParam("@GrossAmount", GrossAmount)
                    SQL.AddParam("@NetAmount", NetAmount)
                    SQL.AddParam("@VATable", VAT)
                    SQL.AddParam("@VATinc", VATInc)
                    SQL.AddParam("@VATAmount", VATAmount)
                    SQL.AddParam("@WHSE", WHSE)
                    SQL.AddParam("@Ref_SO_RecID", Ref_SO_RecID)
                    SQL.AddParam("@AccntCode", AccntCode)
                    SQL.AddParam("@DRRate", DRRate)
                    SQL.AddParam("@DRPrice", DRPrice)
                    SQL.AddParam("@StockQTY", StockQTY)
                    SQL.AddParam("@SerialNo", SerialNo)
                    SQL.AddParam("@LotNo", LotNo)
                    SQL.AddParam("@DateExpired", DateExpired)
                    SQL.AddParam("@Size", Size)
                    SQL.AddParam("@Color", Color)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1

                    SaveINVTY("OUT", ModuleID, "DR", TransID, dtpDocDate.Value.Date, ItemCode, WHSE, QTY, UOM, UnitCost, "Active", SerialNo, LotNo, DateExpired, Size, Color)
                End If
            Next
            'UpdatePO(PO_ID, txtVCECode.Text)  ' UPDATE PO DETAILS STATUS
            ComputeWAUC("DR", TransID)


            JETransID = GenerateTransID("JE_No", "tblJE_Header")
            insertSQL = " INSERT INTO " & _
                        " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Currency, Exchange_Rate, Remarks, WhoCreated, Status) " & _
                        " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Currency, @Exchange_Rate, @Remarks, @WhoCreated, @Status)"
            SQL.FlushParams()
            SQL.AddParam("@JE_No", JETransID)
            SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
            SQL.AddParam("@RefType", "DR")
            SQL.AddParam("@RefTransID", TransID)
            SQL.AddParam("@Book", "Inventory")
            SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
            SQL.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Saved")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            '  JETransID = LoadJE("DR", TransID)
            line = 1
            For Each item As DataGridViewRow In dgvEntry.Rows
                If item.Cells(chAccntCode.Index).Value <> Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber) " & _
                                " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber)"
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransID)
                    SQL.AddParam("@AccntCode", item.Cells(chAccntCode.Index).Value.ToString)
                    If item.Cells(chVCECode.Index).Value <> Nothing AndAlso item.Cells(chVCECode.Index).Value <> "" Then
                        SQL.AddParam("@VCECode", item.Cells(chVCECode.Index).Value.ToString)
                    Else
                        SQL.AddParam("@VCECode", txtVCECode.Text)
                    End If
                    If item.Cells(chDebit.Index).Value AndAlso IsNumeric(item.Cells(chDebit.Index).Value) Then
                        SQL.AddParam("@Debit", CDec(item.Cells(chDebit.Index).Value))
                    Else
                        SQL.AddParam("@Debit", 0)
                    End If
                    If item.Cells(chCredit.Index).Value <> Nothing AndAlso IsNumeric(item.Cells(chCredit.Index).Value) Then
                        SQL.AddParam("@Credit", CDec(item.Cells(chCredit.Index).Value))
                    Else
                        SQL.AddParam("@Credit", 0)
                    End If
                    If item.Cells(chParticulars.Index).Value <> Nothing AndAlso item.Cells(chParticulars.Index).Value <> "" Then
                        SQL.AddParam("@Particulars", item.Cells(chParticulars.Index).Value.ToString)
                    Else
                        SQL.AddParam("@Particulars", "")
                    End If
                    If item.Cells(chParticulars.Index).Value <> Nothing AndAlso item.Cells(chParticulars.Index).Value <> "" Then
                        SQL.AddParam("@RefNo", item.Cells(chParticulars.Index).Value.ToString)
                    Else
                        SQL.AddParam("@RefNo", "")
                    End If
                    SQL.AddParam("@LineNumber", line)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next

        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "DR_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub GenerateEntry()
        Dim dataEntry As New DataTable
        dgvEntry.Rows.Clear()
        Dim query As String
        For Each row As DataGridViewRow In dgvItemList.Rows
            Dim AvgCost As Decimal = 0
            If row.Cells(chItemCode.Index).Value <> Nothing AndAlso row.Cells(chNetAmount.Index).Value > 0 Then
                If Inv_ComputationMethod = "SC" Then
                    AvgCost = GetStandardCost(row.Cells(chItemCode.Index).Value)
                Else
                    AvgCost = GetAverageCost(row.Cells(chItemCode.Index).Value)
                End If
                query = " SELECT AD_COS, AccountTitle " & _
                        " FROM   tblItem_Master INNER JOIN tblCOA_Master " & _
                        " ON     tblItem_Master.AD_COS = tblCOA_Master.AccountCode " & _
                        " WHERE  ItemCode ='" & row.Cells(chItemCode.Index).Value & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read() Then
                    dgvEntry.Rows.Add({SQL.SQLDR("AD_COS").ToString, GetAccntTitle(SQL.SQLDR("AD_COS").ToString), CDec(row.Cells(chQTY.Index).Value * AvgCost), "0.00", "DR:" & txtTransNum.Text})
                End If
            End If
        Next

        For Each row As DataGridViewRow In dgvItemList.Rows
            Dim AvgCost As Decimal = 0
            If row.Cells(chItemCode.Index).Value <> Nothing AndAlso row.Cells(chNetAmount.Index).Value > 0 Then
                If Inv_ComputationMethod = "SC" Then
                    AvgCost = GetStandardCost(row.Cells(chItemCode.Index).Value)
                Else
                    AvgCost = GetAverageCost(row.Cells(chItemCode.Index).Value)
                End If
                query = " SELECT AD_Inv, AccountTitle " & _
                        " FROM   tblItem_Master INNER JOIN tblCOA_Master " & _
                        " ON     tblItem_Master.AD_Inv = tblCOA_Master.AccountCode " & _
                        " WHERE  ItemCode ='" & row.Cells(chItemCode.Index).Value & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read() Then
                    dgvEntry.Rows.Add({SQL.SQLDR("AD_Inv").ToString, GetAccntTitle(SQL.SQLDR("AD_Inv").ToString), "0.00", CDec(row.Cells(chQTY.Index).Value * AvgCost), "DR:" & txtTransNum.Text})
                End If
            End If
        Next
        'If txtTotalDebit.Text <> 0 Then
        '    query = " SELECT AccntCode, AccountTitle FROM tblDefaultAccount " & _
        '       " INNER JOIN " & _
        '       " tblCOA_Master ON " & _
        '       " tblCOA_Master.AccountCode = tblDefaultAccount.AccntCode " & _
        '       " WHERE ModuleID = '" & ModuleID & "' AND Type = 'Credit' " & _
        '       " ORDER BY AccountTitle "
        '    SQL.ReadQuery(query)
        '    If SQL.SQLDR.Read() Then
        '        dgvEntry.Rows.Add({SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccountTitle").ToString, "0.00", CDec(txtTotalDebit.Text).ToString("N2"), ""})
        '    End If
        'End If

        TotalDBCR()
    End Sub

    Private Sub TotalDBCR()
        ' COMPUTE TOTAL DEBIT
        Dim db As Decimal = 0

        For i As Integer = 0 To dgvEntry.Rows.Count - 1
            If Val(dgvEntry.Item(2, i).Value) <> 0 Then
                db = db + Double.Parse(dgvEntry.Item(2, i).Value).ToString("N2")
            End If
        Next
        txtTotalDebit.Text = db.ToString("N2")

        ' COMPUTE TOTAL CREDIT
        Dim b As Decimal = 0

        For i As Integer = 0 To dgvEntry.Rows.Count - 1
            If Val(dgvEntry.Item(3, i).Value) <> 0 Then
                b = b + Double.Parse(dgvEntry.Item(3, i).Value).ToString("N2")
            End If
        Next
        txtTotalCredit.Text = b.ToString("N2")
    End Sub

    Private Sub UpdateDR()
        Try

            activityStatus = True
            Dim insertSQL, updateSQL, deleteSQL As String
            updateSQL = " UPDATE    tblDR " & _
                        " SET       DR_No = @DR_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, DateDR = @DateDR, " & _
                        "           VCECode = @VCECode, DateDeliver = @DateDeliver, Remarks = @Remarks, ForECS = @ForECS, Currency = @Currency, Exchange_Rate = @Exchange_Rate,  " & _
                        "           SO_Ref = @SO_Ref, PL_Ref = @PL_Ref, PO_Ref = @PO_Ref, CusSONo = @CusSONo, PlateNumber = @PlateNumber, DriverName = @DriverName, WhoModified = @WhoModified, DateModified = GETDATE() " & _
                        " WHERE     TransID = @TransID  "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@DR_No", DRNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateDR", dtpDocDate.Value.Date)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateDeliver", dtpActual.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL.AddParam("@SO_Ref", SO_ID)
            SQL.AddParam("@PL_Ref", PL_ID)
            SQL.AddParam("@PO_Ref", PO_ID)
            SQL.AddParam("@CusSONo", txtCustSO.Text)
            SQL.AddParam("@PlateNumber", txtPlateNumber.Text)
            SQL.AddParam("@DriverName", txtDriverName.Text)
            SQL.AddParam("@ForECS", chkForECS.Checked)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)

            deleteSQL = " DELETE FROM tblDR_Details WHERE TransID =@TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.ExecNonQuery(deleteSQL)


            DELINVTY(ModuleID, "DR", TransID)

            Dim line As Integer = 1
            Dim ItemCode, Description, UOM, WHSE, Ref_SO_RecID, AccntCode, DateExpired, LotNo, SerialNo, Size, Color As String
            Dim QTY, UnitCost, UnitPrice, GrossAmount, NetAmount, DRRate, DRPrice, VATAmount, StockQTY As Decimal
            Dim VAT, VATInc As Boolean
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                    Description = IIf(row.Cells(chItemDesc.Index).Value = Nothing, "", row.Cells(chItemDesc.Index).Value)
                    UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                    Ref_SO_RecID = IIf(row.Cells(chRefRecID.Index).Value = Nothing, "", row.Cells(chRefRecID.Index).Value)
                    UnitPrice = IIf(row.Cells(chUnitPrice.Index).Value = Nothing, "0.00", CDec(row.Cells(chUnitPrice.Index).Value).ToString("N2"))
                    GrossAmount = IIf(row.Cells(chGross.Index).Value = Nothing, "0.00", CDec(row.Cells(chGross.Index).Value).ToString("N2"))
                    NetAmount = IIf(row.Cells(chNetAmount.Index).Value = Nothing, "0.00", CDec(row.Cells(chNetAmount.Index).Value).ToString("N2"))
                    AccntCode = IIf(row.Cells(chAccnt.Index).Value = Nothing, "", row.Cells(chAccnt.Index).Value)
                    DRRate = IIf(row.Cells(chDRRate.Index).Value = Nothing, "0.00", CDec(row.Cells(chDRRate.Index).Value).ToString("N2"))
                    DRPrice = IIf(row.Cells(chDRPrice.Index).Value = Nothing, "0.00", CDec(row.Cells(chDRPrice.Index).Value).ToString("N2"))
                    VATAmount = IIf(row.Cells(chVATAmount.Index).Value = Nothing, "0.00", CDec(row.Cells(chVATAmount.Index).Value).ToString("N2"))
                    VAT = IIf(row.Cells(chVAT.Index).Value = Nothing, False, row.Cells(chVAT.Index).Value)
                    VATInc = IIf(row.Cells(chVATInc.Index).Value = Nothing, False, row.Cells(chVATInc.Index).Value)
                    DateExpired = IIf(row.Cells(chDateExpired.Index).Value = Nothing, "", row.Cells(chDateExpired.Index).Value)
                    LotNo = IIf(row.Cells(chLotNo.Index).Value = Nothing, "", row.Cells(chLotNo.Index).Value)
                    SerialNo = IIf(row.Cells(chSerialNo.Index).Value = Nothing, "", row.Cells(chSerialNo.Index).Value)
                    Size = IIf(row.Cells(chSize.Index).Value = Nothing, "", row.Cells(chSize.Index).Value)
                    Color = IIf(row.Cells(chColor.Index).Value = Nothing, "", row.Cells(chColor.Index).Value)
                    If Inv_ComputationMethod = "SC" Then
                        UnitCost = GetStandardCost(ItemCode)
                    Else
                        UnitCost = GetAverageCost(ItemCode)
                    End If
                    StockQTY = CDec(row.Cells(dgcStockQTY.Index).Value)
                    If IsNumeric(row.Cells(chQTY.Index).Value) Then
                        QTY = CDec(row.Cells(chQTY.Index).Value)
                    Else
                        QTY = 1
                    End If
                    WHSE = IIf(row.Cells(chWHSE.Index).Value = Nothing, "", GetWHSECode(row.Cells(chWHSE.Index).Value))
                    insertSQL = " INSERT INTO " & _
                         " tblDR_Details(TransId, ItemCode, Description, UOM, QTY, UnitPrice, GrossAmount, VATAmount, NetAmount, " & _
                         "  VATable, VATinc, WHSE, Ref_SO_RecID, AccntCode, DRRate, DRPrice, StockQTY, LineNum, WhoCreated, " & _
                         "  SerialNo, LotNo, DateExpired, Size, Color) " & _
                         " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @UnitPrice, @GrossAmount, @VATAmount, @NetAmount, " & _
                         "  @VATable, @VATinc,  @WHSE, @Ref_SO_RecID, @AccntCode,  @DRRate, @DRPrice, @StockQTY, @LineNum, @WhoCreated, " & _
                         "  @SerialNo, @LotNo, @DateExpired, @Size, @Color) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", ItemCode)
                    SQL.AddParam("@Description", Description)
                    SQL.AddParam("@UOM", UOM)
                    SQL.AddParam("@QTY", QTY)
                    SQL.AddParam("@UnitPrice", UnitPrice)
                    SQL.AddParam("@GrossAmount", GrossAmount)
                    SQL.AddParam("@NetAmount", NetAmount)
                    SQL.AddParam("@VATable", VAT)
                    SQL.AddParam("@VATinc", VATInc)
                    SQL.AddParam("@VATAmount", VATAmount)
                    SQL.AddParam("@WHSE", WHSE)
                    SQL.AddParam("@Ref_SO_RecID", Ref_SO_RecID)
                    SQL.AddParam("@AccntCode", AccntCode)
                    SQL.AddParam("@DRRate", DRRate)
                    SQL.AddParam("@DRPrice", DRPrice)
                    SQL.AddParam("@StockQTY", StockQTY)
                    SQL.AddParam("@SerialNo", SerialNo)
                    SQL.AddParam("@LotNo", LotNo)
                    SQL.AddParam("@DateExpired", DateExpired)
                    SQL.AddParam("@Size", Size)
                    SQL.AddParam("@Color", Color)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1

                    SaveINVTY("OUT", ModuleID, "DR", TransID, dtpDocDate.Value.Date, ItemCode, WHSE, QTY, UOM, UnitCost, "Active", SerialNo, LotNo, DateExpired, Size, Color)
                End If
            Next

            ComputeWAUC("DR", TransID)

            JETransID = LoadJE("DR", TransID)

            If JETransID = 0 Then

                JETransID = GenerateTransID("JE_No", "tblJE_Header")
                insertSQL = " INSERT INTO " & _
                            " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Currency, Remarks, WhoCreated) " & _
                            " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Currency, @Remarks, @WhoCreated)"
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransID)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "DR")
                SQL.AddParam("@RefTransID", TransID)
                SQL.AddParam("@Book", "Inventory")
                SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@WhoCreated", UserID)
                SQL.ExecNonQuery(insertSQL)

                'JETransID = LoadJE("DR", TransID)
            Else
                updateSQL = " UPDATE tblJE_Header " & _
                            " SET    AppDate = @AppDate, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " & _
                            "        RefType = @RefType, RefTransID = @RefTransID, Book = @Book, TotalDBCR = @TotalDBCR, Currency = @Currency, Exchange_Rate = @Exchange_Rate,  " & _
                            "        Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE() " & _
                            " WHERE  JE_No = @JE_No "
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransID)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "DR")
                SQL.AddParam("@RefTransID", TransID)
                SQL.AddParam("@Book", "Inventory")
                SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@WhoModified", UserID)
                SQL.ExecNonQuery(updateSQL)
            End If

            line = 1

            ' DELETE ACCOUNTING ENTRIES
            deleteSQL = " DELETE FROM tblJE_Details  WHERE  JE_No = @JE_No "
            SQL.FlushParams()
            SQL.AddParam("@JE_No", JETransID)
            SQL.ExecNonQuery(deleteSQL)

            ' INSERT NEW ENTRIES
            For Each item As DataGridViewRow In dgvEntry.Rows
                If item.Cells(chAccntCode.Index).Value <> Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber) " & _
                                " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber)"
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransID)
                    SQL.AddParam("@AccntCode", item.Cells(chAccntCode.Index).Value.ToString)
                    If item.Cells(chVCECode.Index).Value <> Nothing AndAlso item.Cells(chVCECode.Index).Value <> "" Then
                        SQL.AddParam("@VCECode", item.Cells(chVCECode.Index).Value.ToString)
                    Else
                        SQL.AddParam("@VCECode", txtVCECode.Text)
                    End If
                    If item.Cells(chDebit.Index).Value AndAlso IsNumeric(item.Cells(chDebit.Index).Value) Then
                        SQL.AddParam("@Debit", CDec(item.Cells(chDebit.Index).Value))
                    Else
                        SQL.AddParam("@Debit", 0)
                    End If
                    If item.Cells(chCredit.Index).Value <> Nothing AndAlso IsNumeric(item.Cells(chCredit.Index).Value) Then
                        SQL.AddParam("@Credit", CDec(item.Cells(chCredit.Index).Value))
                    Else
                        SQL.AddParam("@Credit", 0)
                    End If
                    If item.Cells(chParticulars.Index).Value <> Nothing AndAlso item.Cells(chParticulars.Index).Value <> "" Then
                        SQL.AddParam("@Particulars", item.Cells(chParticulars.Index).Value.ToString)
                    Else
                        SQL.AddParam("@Particulars", "")
                    End If
                    If item.Cells(chParticulars.Index).Value <> Nothing AndAlso item.Cells(chParticulars.Index).Value <> "" Then
                        SQL.AddParam("@RefNo", item.Cells(chParticulars.Index).Value.ToString)
                    Else
                        SQL.AddParam("@RefNo", "")
                    End If
                    SQL.AddParam("@LineNumber", line)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "DR_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
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

    Private Sub LoadSO(ByVal SO As String)
        Dim query As String
        query = " SELECT  TransID, SO_No, DateSO AS Date, tblSO.VCECode, VCEName, Remarks " & _
                " FROM   tblSO INNER JOIN viewVCE_MAster " & _
                " ON     tblSO.VCECode = viewVCE_MAster.VCECode " & _
                " WHERE  tblSO.Status ='Active' AND TransID ='" & SO & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            SO_ID = SQL.SQLDR("TransID")
            txtSORef.Text = SQL.SQLDR("SO_No")
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            LoadCurrency()

            query = " SELECT tblSO_Details.ItemCode, Description, UOM, Unserved AS QTY , ISNULL(viewSO_Unserved.UnitPrice,0) AS UnitPrice, viewSO_Unserved.WHSE,  tblSO_Details.GrossAmount, tblSO_Details.NetAmount, tblSO_Details.RecordID, VATable, VATinc, VATAmount " & _
                    " FROM   tblSO_Details INNER JOIN viewSO_Unserved " & _
                    " ON     tblSO_Details.TransID = viewSO_Unserved.TransID " & _
                    " AND    tblSO_Details.ItemCode = viewSO_Unserved.ItemCode " & _
                    " AND    tblSO_Details.RecordID = viewSO_Unserved.RecordID " & _
                    " WHERE  tblSO_Details.TransID ='" & SO & "' "
            dgvItemList.Rows.Clear()
            SQL.GetQuery(query)
            Dim ctr As Integer = 0
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                    dgvItemList.Rows.Add(row(0).ToString, "", row(1).ToString, row(2).ToString, _
                                         CDec(row(3)).ToString("N2"), GetWHSEDesc(row(5).ToString), CDec(row(4)).ToString("N2"), CDec(row(6)).ToString("N2"), CDec(row(11)).ToString("N2"), CDec(row(7)).ToString("N2"), row(8).ToString, row(9).ToString, row(10).ToString)
                    LoadWHSE(ctr)
                    LoadUOM(row(0).ToString, ctr)
                    LoadColor(row(0).ToString, ctr)
                    LoadSize(row(0).ToString, ctr)
                    Recompute(ctr)
                    ctr += 1
                Next
            End If
            LoadBarCode()
            ComputeTotal()
        End If
    End Sub


    Private Sub LoadPL(ByVal ID As String, ByVal WHSE As String)
        Dim query As String
        query = " SELECT    TransID, PL_No, SO_Ref, DatePL AS Date, tblPL.VCECode, VCEName, Remarks " & _
                " FROM      tblPL INNER JOIN tblVCE_Master " & _
                " ON        tblPL.VCECode = tblVCE_Master.VCECode " & _
                " WHERE     tblPL.Status ='Active' AND TransID ='" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            PL_ID = SQL.SQLDR("TransID")
            SO_ID = SQL.SQLDR("SO_Ref")
            txtPLRef.Text = SQL.SQLDR("PL_No")
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtSORef.Text = LoadSONo(SO_ID)

            query = " SELECT tblPL_Details.ItemCode, Description, UOM, tblPL_Details.PickQTY AS QTY , " & _
                    " ISNULL(ID_SC,0) AS UnitPrice, tblPL_Details.WHSE, viewSO_Unserved.RecordID, AD_COS  " & _
                    " FROM   tblPL_Details " & _
                    " INNER JOIN viewSO_Unserved " & _
                    " ON     viewSO_Unserved.TransID = '" & SO_ID & "'" & _
                    " AND    tblPL_Details.ItemCode = viewSO_Unserved.ItemCode " & _
                    " INNER JOIN " & _
                    " tblItem_Master ON " & _
                    " tblItem_Master.ItemCode = tblPL_Details.ItemCode " & _
                    " WHERE  tblPL_Details.TransID ='" & ID & "' "
            dgvItemList.Rows.Clear()
            SQL.GetQuery(query)
            Dim ctr As Integer = 0
            Dim netamount As Decimal
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                    netamount = row(3) * row(4)
                    dgvItemList.Rows.Add(row(0).ToString, row(1).ToString, row(2).ToString, _
                                         CDec(row(3)).ToString("N2"), GetWHSEDesc(row(5).ToString), row(4).ToString, row(4).ToString, CDec(netamount).ToString("N2"), row(6).ToString, row(7).ToString)
                    LoadWHSE(ctr)
                    LoadUOM(row(0).ToString, ctr)
                    LoadColor(row(0).ToString, ctr)
                    LoadSize(row(0).ToString, ctr)
                    ctr += 1
                Next
            End If
        Else
            ClearText()
        End If
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("DR_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("DR")
            TransID = f.transID
            LoadDR(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("DR_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            DRNo = ""

            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbCancel.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbUpload.Enabled = True
            tsbDownload.Enabled = True
            tsbExit.Enabled = False
            tsbPrint.Enabled = False
            tsbCopy.Enabled = True
            EnableControl(True)

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
        End If
    End Sub

    Private Sub dgvItemList_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellClick
        If e.ColumnIndex = chWHSE.Index Then
            If e.RowIndex <> -1 AndAlso dgvItemList.Rows.Count > 0 Then
                Try
                    LoadWHSE(e.RowIndex)
                    Dim dgvCol As DataGridViewComboBoxColumn
                    dgvCol = dgvItemList.Columns.Item(e.ColumnIndex)
                    dgvItemList.BeginEdit(True)
                    dgvCol.Selected = True
                    DirectCast(dgvItemList.EditingControl, DataGridViewComboBoxEditingControl).DroppedDown = True
                    Dim editingComboBox As ComboBox = TryCast(sender, ComboBox)
                Catch ex As NullReferenceException
                Catch ex As Exception
                    SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                End Try

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
                LoadBarcode()
            End If
        End If

        'Remove the handle to this event. It will be readded each time a new combobox selection causes the EditingControlShowing Event to fire
        RemoveHandler editingComboBox.SelectionChangeCommitted, AddressOf editingComboBox_SelectionChangeCommitted
        'Re-enable the EditingControlShowing event so the above can take place.
        AddHandler dgvItemList.EditingControlShowing, AddressOf dgvItemList_EditingControlShowing
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

    Private Sub LoadWHSE(Optional ByVal SelectedIndex As Integer = -1)
        Try
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemList.Item(chWHSE.Index, SelectedIndex)
            dgvCB.Items.Clear()
            ' ADD ALL WHSEc
            Dim query As String
            query = " SELECT Description FROM tblWarehouse WHERE Status ='Active' "
            SQL.ReadQuery(query)
            dgvCB.Items.Clear()
            While SQL.SQLDR.Read
                dgvCB.Items.Add(SQL.SQLDR("Description").ToString)
            End While
            If dgvCB.Value <> Nothing AndAlso Not dgvCB.Items.Contains(dgvCB.Value) Then
                dgvCB.Items.Add(dgvCB.Value)
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("DR_EDIT") Then
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

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If DRNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblDR  WHERE DR_No < '" & DRNo & "' ORDER BY DR_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadDR(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If DRNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblDR  WHERE DR_No > '" & DRNo & "' ORDER BY DR_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadDR(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub


    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If DRNo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadDR(TransID)
            tsbEdit.Enabled = True
            tsbCancel.Enabled = True
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbPrint.Enabled = True
        End If
        tsbSearch.Enabled = True
        tsbUpload.Enabled = False
        tsbDownload.Enabled = False
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

    Private Sub tsbCopyPO_Click(sender As System.Object, e As System.EventArgs) Handles tsbCopySO.Click

        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("SO")
        LoadSO(f.transID)
        f.Dispose()
    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("DR_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL, updateSQL As String
                        deleteSQL = " UPDATE  tblDR SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        DRNo = txtTransNum.Text
                        SQL.AddParam("@TransID", TransID)
                        SQL.ExecNonQuery(deleteSQL)

                        '    DELINVTY(ModuleID, "DR", TransID)

                        Dim line As Integer = 1
                        Dim ItemCode, Description, UOM, WHSE, Ref_SO_RecID, AccntCode As String
                        Dim QTY, UnitCost, UnitPrice, GrossAmount, NetAmount, DRRate, DRPrice, VATAmount, StockQTY As Decimal
                        Dim VAT, VATInc As Boolean
                        For Each row As DataGridViewRow In dgvItemList.Rows
                            If Not row.Cells(chQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                                ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                                Description = IIf(row.Cells(chItemDesc.Index).Value = Nothing, "", row.Cells(chItemDesc.Index).Value)
                                UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                                Ref_SO_RecID = IIf(row.Cells(chRefRecID.Index).Value = Nothing, "", row.Cells(chRefRecID.Index).Value)
                                UnitPrice = IIf(row.Cells(chUnitPrice.Index).Value = Nothing, "0.00", CDec(row.Cells(chUnitPrice.Index).Value).ToString("N2"))
                                GrossAmount = IIf(row.Cells(chGross.Index).Value = Nothing, "0.00", CDec(row.Cells(chGross.Index).Value).ToString("N2"))
                                NetAmount = IIf(row.Cells(chNetAmount.Index).Value = Nothing, "0.00", CDec(row.Cells(chNetAmount.Index).Value).ToString("N2"))
                                AccntCode = IIf(row.Cells(chAccnt.Index).Value = Nothing, "", row.Cells(chAccnt.Index).Value)
                                DRRate = IIf(row.Cells(chDRRate.Index).Value = Nothing, "0.00", CDec(row.Cells(chDRRate.Index).Value).ToString("N2"))
                                DRPrice = IIf(row.Cells(chDRPrice.Index).Value = Nothing, "0.00", CDec(row.Cells(chDRPrice.Index).Value).ToString("N2"))
                                VATAmount = IIf(row.Cells(chVATAmount.Index).Value = Nothing, "0.00", CDec(row.Cells(chVATAmount.Index).Value).ToString("N2"))
                                VAT = IIf(row.Cells(chVAT.Index).Value = Nothing, False, row.Cells(chVAT.Index).Value)
                                VATInc = IIf(row.Cells(chVATInc.Index).Value = Nothing, False, row.Cells(chVATInc.Index).Value)
                                If Inv_ComputationMethod = "SC" Then
                                    UnitCost = GetStandardCost(ItemCode)
                                Else
                                    UnitCost = GetAverageCost(ItemCode)
                                End If
                                StockQTY = CDec(row.Cells(dgcStockQTY.Index).Value)
                                If IsNumeric(row.Cells(chQTY.Index).Value) Then
                                    QTY = CDec(row.Cells(chQTY.Index).Value)
                                Else
                                    QTY = 1
                                End If
                                WHSE = IIf(row.Cells(chWHSE.Index).Value = Nothing, "", GetWHSECode(row.Cells(chWHSE.Index).Value))

                                SaveINVTY("IN", ModuleID, "DR", TransID, Date.Today, ItemCode, WHSE, QTY, UOM, UnitCost, "Active")
                            End If
                        Next

                        ComputeWAUC("DR", TransID)

                        JETransID = LoadJE("DR", TransID)
                        updateSQL = " UPDATE tblJE_Header " & _
                          " SET    Status = @Status,  WhoModified = @WhoModified, DateModified = GETDATE()" & _
                          " WHERE  JE_No = @JE_No "
                        SQL.FlushParams()
                        SQL.AddParam("@JE_No", JETransID)
                        SQL.AddParam("@Status", "Cancelled")
                        SQL.AddParam("@WhoModified", UserID)
                        SQL.ExecNonQuery(updateSQL)

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

                        DRNo = txtTransNum.Text
                        LoadDR(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "DR_No", DRNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub frmDR_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        If DataValidated() Then
            If txtVCECode.Text = "" Then
                Msg("Please enter VCECode!", MsgBoxStyle.Exclamation)
            ElseIf TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID(ColumnID, DBTable)
                    If TransAuto Then
                        DRNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    Else
                        DRNo = txtTransNum.Text
                    End If
                    txtTransNum.Text = DRNo
                    GenerateEntry()
                    SaveDR()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    DRNo = txtTransNum.Text
                    LoadDR(TransID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    If DRNo = txtTransNum.Text Then
                        DRNo = txtTransNum.Text
                        GenerateEntry()
                        UpdateDR()
                        Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                        DRNo = txtTransNum.Text
                        LoadDR(TransID)
                    Else
                        If Not IfExist(txtTransNum.Text) Then
                            DRNo = txtTransNum.Text
                            GenerateEntry()
                            UpdateDR()
                            Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                            DRNo = txtTransNum.Text
                            LoadDR(TransID)
                        Else
                            MsgBox("DR" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
                        End If
                    End If

                End If
            End If
        End If
    End Sub

    Private Function IfExist(ByVal ID As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblDR WHERE DR_No ='" & ID & "'  "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function DataValidated() As Boolean
        If txtVCECode.Text = "" Then
            Msg("Please select Customer!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf txtConversion.Visible = True And txtConversion.Text = "" Then
            MsgBox("Please check exchange rate!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf dgvItemList.Rows.Count <= 1 Then
            Msg("There are no items on the list!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf Valid = False Then
            Msg("Please check uploaded items!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf validateDGV() = False Then
            Return False
        Else
            Return True
        End If
        Return True

    End Function

    Private Function validateDGV() As Boolean
        Dim WHSE As String
        Dim stockQTY, issueQTY As Decimal
        Dim value As Boolean = True
        For Each row As DataGridViewRow In dgvItemList.Rows
            If row.Cells(dgcStockQTY.Index).Value <> "" Then
                If Not IsNumeric(row.Cells(dgcStockQTY.Index).Value) Then stockQTY = 0 Else stockQTY = row.Cells(dgcStockQTY.Index).Value
                If Not IsNumeric(row.Cells(chQTY.Index).Value) Then issueQTY = 0 Else issueQTY = row.Cells(chQTY.Index).Value
                If IsNothing(row.Cells(chWHSE.Index).Value) Or row.Cells(chWHSE.Index).Value = "Multiple Warehouse" Then WHSE = "" Else WHSE = row.Cells(chWHSE.Index).Value
                If WHSE = "" Then
                    Msg("There are line entry without  Warehouse, please check.", MsgBoxStyle.Exclamation)
                    value = False
                    Exit For

                End If

                'If issueQTY > stockQTY Then
                '    Msg("Quantity should not be greater than Stock Quantity", MsgBoxStyle.Exclamation)
                '    value = False
                '    Exit For
                'End If
            End If
        Next
        Return value
    End Function

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("DR", TransID)
        f.Dispose()
    End Sub

    Private Sub dgvItemList_DataError(sender As System.Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvItemList.DataError
        Try

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub FromPickListToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromPickListToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("PL-DR")
        LoadPL(f.transID, f.itemCode)
        f.Dispose()
    End Sub


    Private Sub dgvEntry_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntry.CellContentClick

    End Sub

    Private Sub dgvItemList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellContentClick

    End Sub

    Private Sub FromPOToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromPOToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("Sub PO")
        LoadPO(f.transID, f.itemCode)
        f.Dispose()
    End Sub

    Private Sub LoadPO(ByVal TransNum As String, ByVal SupplierCode As String)
        Dim query, ItemCode As String
        query = "  SELECT   tblPO.TransID, PO_No, PurchaseType, DateDeliver, tblPO.Remarks,  tblPO.Status  " & _
                " FROM     tblPO " & _
                " WHERE     TransID = @TransID  "
        SQL.FlushParams()
        SQL.AddParam("@TransID", TransNum)
        SQL.ReadQuery(query)
        SQL.FlushParams()
        If SQL.SQLDR.Read Then
            PO_ID = SQL.SQLDR("TransID")
            txtPORef.Text = SQL.SQLDR("PO_No").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = "Open"
            query = "  SELECT TransID, POdetails.ItemCode, Description, UOM, QTY, POdetails.VCECode, UnitPrice, POdetails.VATable, VATInc, GrossAmount, VATAmount, NetAmount, AD_Inv , RecordID " & _
                    "  FROM  " & _
                    "  (  " & _
                    " SELECT TransID, ItemCode, Description, UOM, QTY,  " & _
                    " VCECode,  UnitPrice, VATable, VATInc, GrossAmount, VATAmount, NetAmount, RecordID  " & _
                    " FROM tblPO_Details  " & _
                    "  ) AS POdetails  " & _
                    "  LEFT JOIN tblVCE_Master  " & _
                    "  ON        tblVCE_Master.VCECode = POdetails.VCECode  " & _
                    "  LEFT JOIN tblItem_Master  " & _
                    "  ON        tblItem_Master.ItemCode = POdetails.ItemCode  " & _
                    "  WHERE     POdetails.TransID = @TransID AND POdetails.VCECode = @SupplierCode AND tblVCE_Master.Status ='Active'  "
            dgvItemList.Rows.Clear()
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransNum)
            SQL.AddParam("@SupplierCode", SupplierCode)
            SQL.ReadQuery(query)

            While SQL.SQLDR.Read
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                ItemCode = SQL.SQLDR("ItemCode").ToString
                dgvItemList.Rows.Add(New String() {SQL.SQLDR("ItemCode").ToString, "", SQL.SQLDR("Description").ToString, _
                                                            SQL.SQLDR("UOM").ToString, SQL.SQLDR("QTY").ToString, "", _
                                                             CDec(SQL.SQLDR("UnitPrice")).ToString("N2"), CDec(SQL.SQLDR("GrossAmount")).ToString("N2"), _
                                                        CDec(SQL.SQLDR("NetAmount")).ToString("N2"), SQL.SQLDR("RecordID").ToString, SQL.SQLDR("AD_Inv").ToString, "0.00", "0.00"})
                LoadWHSE(dgvItemList.Rows.Count - 2)
                LoadUOM(ItemCode, dgvItemList.Rows.Count - 2)
                LoadColor(ItemCode, dgvItemList.Rows.Count - 2)
                LoadSize(ItemCode, dgvItemList.Rows.Count - 2)
            End While
            LoadBarCode()
            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            SQL.FlushParams()
            ComputeTotal()
        Else
            ClearText()
        End If
    End Sub

    Private Sub UpdatePO(ByVal ID As Integer, ByVal Code As String)
        If ID <> 0 Then
            Dim updateSQL As String
            updateSQL = " UPDATE tblPO_Details SET Status = 'Closed'  " & _
                        " WHERE TransID = '" & ID & "' " & _
                        " AND     VCECode  ='" & Code & "' "
            SQL.ExecNonQuery(updateSQL)
        End If
    End Sub

    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged

    End Sub

    Private Sub ToolStrip1_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub

    Private Sub cbCurrency_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbCurrency.SelectedIndexChanged
        If disableEvent = False Then
            If cbCurrency.SelectedItem <> BaseCurrency Then
                lblConversion.Visible = True
                txtConversion.Visible = True
                MsgBox("Please input " & cbCurrency.SelectedItem & " amount in Debit and Credit.", MsgBoxStyle.Exclamation)
            Else
                lblConversion.Visible = False
                txtConversion.Visible = False
            End If
        End If
    End Sub

    Dim eColIndex As Integer = 0
    Private Sub dgvItemList_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dgvItemList.CurrentCellDirtyStateChanged

        If eColIndex = chWHSE.Index And TypeOf (dgvItemList.CurrentRow.Cells(chWHSE.Index)) Is DataGridViewComboBoxCell Then
            dgvItemList.CommitEdit(DataGridViewDataErrorContexts.Commit)
            dgvItemList.EndEdit()
        End If
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

    Private Sub FromGIConsignmentToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromGIConsignmentToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("GI-DR-Consignment")
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        txtConversion.Text = ""
        cbCurrency.Items.Clear()
        LoadGI(f.transID)
        f.Dispose()
    End Sub

    Private Sub LoadGI(ByVal ID As String)
        Try
            Dim WHSEfrom, WHSEto As String
            Dim query As String
            query = " SELECT TransID, GI_No, IssueFrom, IssueTo, WHSE_From, WHSE_To, DateGI, Remarks " & _
                    " FROM   tblGI " & _
                    " WHERE  TransID ='" & ID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then


                query = "  SELECT tblGI_Details.ItemCode, Description, UOM, IssueQTY, WHSE, AvgCost, GrossAmount, VATAmount, NetAmount, 0, VATable, VATInc  , AccntCode, 0, " & _
                        "           0,   ISNULL(DateExpired,'') AS DateExpired, ISNULL(LotNo,'') AS LotNo ,  ISNULL(SerialNo,'') AS SerialNo " & _
                        "  FROM   tblGI_Details   " & _
                        " WHERE  tblGI_Details.TransID ='" & ID & "' "
                SQL.GetQuery(query)
                dgvItemMaster.Rows.Clear()
                Dim ctr As Integer = 0
                dgvItemList.Rows.Clear()
                SQL.GetQuery(query)
                If SQL.SQLDS.Tables(0).Rows.Count Then
                    For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                        dgvItemList.Rows.Add(row(0).ToString, "", row(1).ToString, row(2).ToString, _
                                              row(3).ToString, GetWHSEDesc(row(4).ToString), row(5).ToString, _
                                              row(6).ToString, row(7).ToString, row(8).ToString, row(9).ToString, _
                                              row(10).ToString, row(11).ToString, row(12).ToString, row(13).ToString, _
                                              row(14).ToString, row(15).ToString, row(16).ToString, row(17).ToString)
                        LoadWHSE(ctr)
                        LoadUOM(row(0).ToString, ctr)
                        LoadColor(row(0).ToString, ctr)
                        LoadSize(row(0).ToString, ctr)
                        ctr += 1
                    Next
                    LoadBarCode()
                End If
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub tsbDownload_Click(sender As System.Object, e As System.EventArgs) Handles tsbDownload.Click
        Dim fileName As String = "DR Uploader.xlsx"
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
            For i As Integer = 0 To 18
                If i = 0 Then
                    xlWorkSheet.Cells(1, i + 1) = templateName
                End If
                xlWorkSheet.Cells(2, i + 1) = dgvItemList.Columns(i).Name
                xlWorkSheet.Cells(3, i + 1) = dgvItemList.Columns(i).HeaderText
            Next
            xlWorkSheet.Protect(excelPW)
            Dim ctr As Integer = 1
            Do
                fileName = "DR Uploader -" & ctr.ToString & ".xlsx"
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

                        If j = 0 Then
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
                        ElseIf j = chWHSE.Index Then
                            ' REfno
                            If IsNothing(dgvItemList.Item(j, rowCount - 1).Value) OrElse dgvItemList.Item(j, rowCount - 1).Value = "" Then
                                ' if not check VCEref on excel template
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                                LoadWHSE(rowCount - 1)
                            End If
                        ElseIf j = dgcStockQTY.Index Then
                             ' QTY
                            If IsNothing(dgvItemList.Item(j, rowCount - 1).Value) OrElse dgvItemList.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chDateExpired.Index Then
                            If Not IsDate(ObjectText) Then
                                ' if not date, change color.
                                ChangeCellColor(rowCount - 1, j)
                                    Valid = False
                                Else
                                    ' if existing  AccountCode
                                    AddValue(ObjectText, rowCount - 1, j)
                                End If
                        ElseIf j = chLotNo.Index Then
                            ' LotNo
                            If IsNothing(dgvItemList.Item(j, rowCount - 1).Value) OrElse dgvItemList.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chSerialNo.Index Then
                            ' Serial
                            If IsNothing(dgvItemList.Item(j, rowCount - 1).Value) OrElse dgvItemList.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
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

    Private Sub bgwUpload_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwUpload.ProgressChanged
        pgbCounter.Value = e.ProgressPercentage
    End Sub

    Private Sub bgwUpload_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwUpload.RunWorkerCompleted
        pgbCounter.Visible = False
        If InvalidTemplate Then
            MsgBox("Invalid Template, Please select a valid File!", MsgBoxStyle.Exclamation)
        Else
            TotalDBCR()
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


    Private Delegate Sub AddValueInvoker(ByVal Value As String, ByVal row As Integer, ByVal col As Integer)
    Private Delegate Sub SetPGBmaxInvoker(ByVal Value As String)

    Private Delegate Sub ChangeCellColorInvoker(ByVal row As Integer, ByVal col As Integer)

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
    Private Sub AddValue(ByVal Value As String, ByVal row As Integer, ByVal col As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New AddValueInvoker(AddressOf AddValue), Value, row, col)
        Else
            dgvItemList.Item(col, row).Value = Value
        End If
    End Sub
    Private Sub SetPGBmax(ByVal Value As String)
        If Me.InvokeRequired Then
            Me.Invoke(New SetPGBmaxInvoker(AddressOf SetPGBmax), Value)
        Else
            pgbCounter.Maximum = Value
            pgbCounter.Value = 0
        End If
    End Sub

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

    Private Sub dgvItemList_EditModeChanged(sender As Object, e As System.EventArgs) Handles dgvItemList.EditModeChanged

    End Sub
End Class