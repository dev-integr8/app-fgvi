Public Class frmSI_Inv
    Dim TransID, RefID, JETransID As String
    Dim SINo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "SI"
    Dim ColumnPK As String = "SI_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblSI"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim SO_ID, PL_ID, DR_ID As Integer
    Dim accntDR, accntCR, accntVAT, accntDiscount As String


    Public Overloads Function ShowDialog(ByVal docID As String) As Boolean
        TransID = docID
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmSI_Inv_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Text = "(" & database & ") - Sales Invoice "
            TransAuto = GetTransSetup(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            LoadSetup()
            LoadTerms()
            LoadChartOfAccount()
            If TransID <> "" Then
                LoadSI(TransID)
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

    Private Sub LoadSetup()
        Dim query As String
        query = " SELECT    TAX_IV" & _
                " FROM      tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            accntVAT = SQL.SQLDR("TAX_IV")
        End If
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        txtVCEName.Enabled = Value
        btnSearchVCE.Enabled = Value
        cbCurrency.Enabled = Value
        txtConversion.Enabled = Value
        cbDefaultAcc.Enabled = Value
        dgvItemList.AllowUserToAddRows = Value
        dgvItemList.AllowUserToDeleteRows = Value
        dgvItemList.ReadOnly = Not Value
        If Value = True Then
            dgvItemList.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemList.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        cbTerms.Enabled = Value
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Private Sub LoadChartOfAccount()
        Dim query As String
        query = " SELECT AccntCode, AccountTitle FROM tblDefaultAccount " & _
                " INNER JOIN " & _
                " tblCOA_Master ON " & _
                " tblCOA_Master.AccountCode = tblDefaultAccount.AccntCode " & _
                " WHERE Type = 'AR'  AND tblDefaultAccount.Status = 'Active'" & _
                " ORDER BY AccountTitle "

        'query = " SELECT AccountCode, AccountTitle FROM tblCOA_Master ORDER BY AccountTitle "
        SQL.ReadQuery(query)
        cbDefaultAcc.Items.Clear()
        While SQL.SQLDR.Read
            cbDefaultAcc.Items.Add(SQL.SQLDR("AccountTitle"))
        End While
    End Sub

    Private Sub LoadSI(ByVal TransID As String)
        Dim query, terms, Currency As String
        query = " SELECT   TransID, SI_No, VCECode, DateSI, Remarks,  Terms, " & _
                "          ISNULL(GrossAmount,0) AS GrossAmount, ISNULL(VATAmount,0) AS VATAmount, ISNULL(Discount,0) AS Discount, ISNULL(NetAmount,0) AS NetAmount, " & _
                "          ISNULL(VATable,1) AS VATable, ISNULL(VATInc,1) AS VATInc, Currency, ISNULL(Exchange_Rate,0) AS Exchange_Rate, " & _
                "          Status, ISNULL(SO_Ref,0) AS SO_Ref,ISNULL(PL_Ref,0) AS PL_Ref, ISNULL(DR_Ref,0) AS DR_Ref, DebitAccnt " & _
                " FROM     tblSI " & _
                " WHERE    TransId = '" & TransID & "' " & _
                " ORDER BY TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            TransID = SQL.SQLDR("TransID").ToString
            SINo = SQL.SQLDR("SI_No").ToString
            txtTransNum.Text = SINo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            disableEvent = True
            dtpDocDate.Text = SQL.SQLDR("DateSI").ToString
            terms = SQL.SQLDR("Terms").ToString
            txtGross.Text = CDec(SQL.SQLDR("GrossAmount").ToString).ToString("N2")
            txtVAT.Text = CDec(SQL.SQLDR("VATAmount").ToString).ToString("N2")
            txtDiscount.Text = CDec(SQL.SQLDR("Discount").ToString).ToString("N2")
            txtNet.Text = CDec(SQL.SQLDR("NetAmount").ToString).ToString("N2")
            Currency = SQL.SQLDR("Currency").ToString
            txtConversion.Text = CDec(SQL.SQLDR("Exchange_Rate")).ToString("N4")
            chkVAT.Checked = SQL.SQLDR("VATable")
            chkVATInc.Checked = SQL.SQLDR("VATInc")
            SO_ID = SQL.SQLDR("SO_Ref").ToString
            PL_ID = SQL.SQLDR("PL_Ref").ToString
            DR_ID = SQL.SQLDR("DR_Ref").ToString
            cbDefaultAcc.SelectedItem = GetAccntTitle(SQL.SQLDR("DebitAccnt").ToString)

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
            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            txtSORef.Text = LoadSONo(SO_ID)
            txtPLRef.Text = LoadPLNo(PL_ID)
            txtDRRef.Text = LoadDRNo(DR_ID)
            cbTerms.Text = terms
            LoadSIDetails(TransID)
            ComputeTotal()
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

    Private Sub LoadAccountingEntry(ByVal TransID As Integer)
        Try
            Dim query As String
            query = " SELECT JE_No, VCECode, VCEName, AccntCode, AccntTitle, Particulars, RefNo, " & _
                     "        ISNULL(Debit,0) AS Debit, ISNULL(Credit,0) AS Credit " & _
                     " FROM   View_GL  " & _
                     " WHERE  RefType ='SJ' AND RefTransID IN (SELECT TransID FROM tblSJ WHERE SIRef =(SELECT SI_No FROM tblSI WHERE TransID = '" & TransID & "')) "
            SQL.ReadQuery(query)
            dgvEntry.Rows.Clear()
            If SQL.SQLDR.HasRows Then
                While SQL.SQLDR.Read
                    JETransiD = SQL.SQLDR("JE_No").ToString
                    dgvEntry.Rows.Add(SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccntTitle").ToString, _
                                      IIf(CDec(SQL.SQLDR("Debit")) = 0, "", CDec(SQL.SQLDR("Debit")).ToString("N2")), If(CDec(SQL.SQLDR("Credit")) = 0, "", CDec(SQL.SQLDR("Credit")).ToString("N2")), _
                                      SQL.SQLDR("Particulars").ToString, SQL.SQLDR("VCECode").ToString, SQL.SQLDR("VCEName").ToString)
                End While
                TotalDBCR()
            Else
                JETransiD = 0
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

    Private Function LoadDRNo(DR_ID As Integer) As String
        Dim query As String
        query = " SELECT DR_No FROM tblDR WHERE TransID = '" & DR_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("DR_No")
        Else
            Return ""
        End If
    End Function

    Protected Sub LoadSIDetails(ByVal TransID As String)
        dgvItemList.Rows.Clear()
        Dim query As String
        query = " SELECT    ItemCode, Description, UOM, QTY, ISNULL(UnitPrice,0) AS UnitPrice, " & _
                "           ISNULL(GrossAmount,0) AS GrossAmount, ISNULL(DiscountRate,0) AS DiscountRate, ISNULL(Discount,0) AS Discount, " & _
                "           ISNULL(VATAmount,0) AS VATAmount, ISNULL(NetAmount,0) AS NetAmount, ISNULL(VATable,1) AS VATable, ISNULL(VATInc,1) AS VATInc " & _
                " FROM      tblSI_Details " & _
                " WHERE     tblSI_Details.TransId = " & TransID & " " & _
                " ORDER BY  LineNum "
        dgvItemList.Rows.Clear()
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvItemList.Rows.Add(SQL.SQLDR("ItemCode").ToString, SQL.SQLDR("Description").ToString, SQL.SQLDR("UOM").ToString, _
                                 SQL.SQLDR("QTY").ToString, CDec(SQL.SQLDR("UnitPrice")).ToString("N2"), _
                                 CDec(SQL.SQLDR("GrossAmount")).ToString("N2"), _
                                 IIf((SQL.SQLDR("DiscountRate") <> 0), SQL.SQLDR("DiscountRate"), ""), _
                                 CDec(SQL.SQLDR("Discount")).ToString("N2"), _
                                 CDec(SQL.SQLDR("VATAmount")).ToString("N2"), _
                                 CDec(SQL.SQLDR("NetAmount")).ToString("N2"), _
                                 SQL.SQLDR("VATable").ToString, _
                                 SQL.SQLDR("VATInc").ToString)
        End While
    End Sub

    Private Sub txtVCEName_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.txtFilter.Text = txtVCEName.Text
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            txtVCEName.Text = f.VCEName
            f.Dispose()
        End If
    End Sub

    Private Sub LoadDR(ByVal DR_No As String)
        Dim query As String
        query = " SELECT  TransID, DateDR AS Date, tblDR.VCECode, VCEName  " & _
                " FROM    tblDR INNER JOIN tblVCE_Master " & _
                " ON      tblDR.VCECode = tblVCE_Master.VCECode " & _
                " WHERE   tblDR.Status ='Active' AND TransID ='" & DR_No & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtDRRef.Text = SQL.SQLDR("TransID")
            DR_ID = txtDRRef.Text
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
            query = "  SELECT tblDR_Details.ItemCode, tblDR_Details.Description, tblDR_Details.UOM,  " & _
                    " 		  tblDR_Details.QTY, tblDR_Details.UnitPrice, tblDR_Details.GrossAmount,  " & _
                    " 		  ISNULL(tblDR_Details.VATAmount,0) AS VATAmount, tblDR_Details.NetAmount, VATable, VATInc" & _
                    "  FROM	  tblDR   INNER JOIN tblDR_Details " & _
                    "  ON	  tblDR.TransID = tblDR_Details.TransID " & _
                    "  WHERE  tblDR.TransID = '" & DR_No & "' "
            SQL.ReadQuery(query)
            While SQL.SQLDR.Read
                dgvItemList.Rows.Add("")
                dgvItemList.Rows(dgvItemList.Rows.Count - 2).Cells(chItemCode.Index).Value = SQL.SQLDR("ItemCode").ToString
                dgvItemList.Rows(dgvItemList.Rows.Count - 2).Cells(chItemDesc.Index).Value = SQL.SQLDR("Description").ToString
                dgvItemList.Rows(dgvItemList.Rows.Count - 2).Cells(chUOM.Index).Value = SQL.SQLDR("UOM").ToString
                dgvItemList.Rows(dgvItemList.Rows.Count - 2).Cells(chQTY.Index).Value = SQL.SQLDR("QTY")
                dgvItemList.Rows(dgvItemList.Rows.Count - 2).Cells(chUnitPrice.Index).Value = CDec(SQL.SQLDR("UnitPrice")).ToString("N2")
                dgvItemList.Rows(dgvItemList.Rows.Count - 2).Cells(chGross.Index).Value = CDec(SQL.SQLDR("GrossAmount")).ToString("N2")
                dgvItemList.Rows(dgvItemList.Rows.Count - 2).Cells(chVATAmount.Index).Value = CDec(SQL.SQLDR("VATAmount")).ToString("N2")
                dgvItemList.Rows(dgvItemList.Rows.Count - 2).Cells(chNetAmount.Index).Value = CDec(SQL.SQLDR("NetAmount")).ToString("N2")
                dgvItemList.Rows(dgvItemList.Rows.Count - 2).Cells(chVAT.Index).Value = SQL.SQLDR("VATable")
                dgvItemList.Rows(dgvItemList.Rows.Count - 2).Cells(dgcVATInc.Index).Value = SQL.SQLDR("VATInc")

            End While
            If txtVCECode.Text <> "" Then
                query = " SELECT  Terms" & _
                    " FROM     tblVCE_Master " & _
                    " WHERE    VCECode = '" & txtVCECode.Text & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    cbTerms.Text = SQL.SQLDR("Terms").ToString
                End If
            End If
            LoadCurrency()
            ComputeTotal()
        End If
    End Sub

    Private Sub ClearText()
        DR_ID = 0
        PL_ID = 0
        SO_ID = 0
        txtTransNum.Text = ""
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        txtSORef.Clear()
        txtDRRef.Clear()
        txtPLRef.Clear()
        dgvItemList.Rows.Clear()
        dgvEntry.Rows.Clear()
        txtRemarks.Text = ""
        txtGross.Text = "0.00"
        txtNet.Text = "0.00"
        txtDiscount.Text = "0.00"
        txtVAT.Text = "0.00"
        dtpDocDate.Value = Date.Today.Date
        txtStatus.Text = "Open"
        cbDefaultAcc.SelectedItem = ""
        cbTerms.SelectedIndex = -1
        cbDefaultAcc.SelectedIndex = -1
        cbCurrency.Items.Clear()
        txtConversion.Text = ""

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("Are you sure you want to cancel Sales Invoice No. " & txtTransNum.Text & "? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Dim updateSQL As String
            updateSQL = " UPDATE tblSI_Header SET Status ='Cancelled' WHERE TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", txtTransNum.Text)
            SQL.ExecNonQuery(updateSQL)
            MsgBox("SI No. " & txtTransNum.Text & " has been cancelled", MsgBoxStyle.Information)
        End If
    End Sub

    Public Function GetTransID() As Integer
        Dim query As String
        query = " SELECT MAX(TransID) + 1 AS TransID FROM tblSI_Header"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() And Not IsDBNull(SQL.SQLDR("TransID")) Then
            Return SQL.SQLDR("TransID")
        Else
            Return 1
        End If
    End Function

    Private Sub dgvProducts_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Try
            Dim itemCode, RecordID As String
            Dim rowIndex As Integer = dgvItemList.CurrentCell.RowIndex
            Dim colindex As Integer = dgvItemList.CurrentCell.ColumnIndex
            Select Case colindex
                Case chItemCode.Index
                    If dgvItemList.Item(chItemCode.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemList.Item(chItemCode.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("ItemMaster", itemCode)
                        If f.TransID <> "" Then
                            itemCode = f.ItemCode
                            RecordID = f.TransID
                            LoadItem(RecordID, itemCode)
                            LoadStock()
                        End If
                        f.Dispose()
                    End If
                Case chItemDesc.Index
                    If dgvItemList.Item(chItemDesc.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemList.Item(chItemDesc.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("ItemMaster", itemCode, "Selling")
                        If f.TransID <> "" Then
                            itemCode = f.ItemCode
                            RecordID = f.TransID
                            LoadItem(RecordID, itemCode)
                            LoadStock()
                        End If
                        dgvItemList.Rows.RemoveAt(e.RowIndex)
                        f.Dispose()
                    End If
                Case chQTY.Index
                    If IsNumeric(dgvItemList.Item(chUnitPrice.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemList.Item(chQTY.Index, e.RowIndex).Value) Then
                        Dim totoalprice = CDec(dgvItemList.Item(chUnitPrice.Index, e.RowIndex).Value) * CDec(dgvItemList.Item(chQTY.Index, e.RowIndex).Value)
                        dgvItemList.Item(chGross.Index, e.RowIndex).Value = Format(totoalprice, "#,###,###,###.00").ToString()
                        ComputeTotal()
                    End If
                Case chWarehouse.Index
                    If dgvItemList.Item(chItemCode.Index, e.RowIndex).Value <> "" Then
                        LoadStock()
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub LoadItem(ByVal ID As String, ByVal ItemCode As String)
        Dim query As String
        query = " SELECT  ItemCode, ItemName, UOM, 1 AS QTY, VATable, " & _
                "         CASE WHEN VATable = 0 THEN UnitPrice " & _
                "              WHEN VATInclusive = 1 THEN UnitPrice/1.12 " & _
                "              ELSE UnitPrice  " & _
                "         END AS UnitPrice, " & _
                "         CASE WHEN VATable = 0 THEN 0 " & _
                "              WHEN VATInclusive = 1 THEN UnitPrice/1.12*.12 " & _
                "              ELSE UnitPrice * 0.12 " & _
                "         END AS VAT " & _
                " FROM    viewItem_Price " & _
                " WHERE   ItemCode = '" & ItemCode & "'  AND Category ='Selling' "
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
                                                 SQL.SQLDR("VAtable")})
            LoadWHSE(dgvItemList.Rows.Count - 2)
        End While
        ComputeTotal()
    End Sub

    Private Sub LoadWHSE(Optional ByVal SelectedIndex As Integer = -1)
        Try
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemList.Item(chWarehouse.Index, SelectedIndex)
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

    Private Sub LoadStock()


        Dim query As String
        Dim itemCode, UOM As String
        Dim StockQTY As Decimal = 0
        Dim IssueQTY As Decimal = 0
        Dim IssuedQTY As Decimal = 0
        Dim ReqQTY As Decimal = 0
        For Each row As DataGridViewRow In dgvItemList.Rows
            Dim WHSE As String
            If row.Cells(chWarehouse.Index).Value = "" Then
                WHSE = ""
            Else
                WHSE = GetWHSE(row.Cells(chWarehouse.Index).Value)
            End If
            If Not IsNothing(row.Cells(chItemCode.Index).Value) Then
                itemCode = row.Cells(chItemCode.Index).Value.ToString
                If Not IsNothing(row.Cells(chUOM.Index).Value) Then UOM = row.Cells(chUOM.Index).Value Else UOM = ""
                If Not IsNumeric(row.Cells(chQTY.Index).Value) Then ReqQTY = 0 Else ReqQTY = CDec(row.Cells(chQTY.Index).Value)

                query = "   SELECT	    ISNULL(SUM(QTY),0) AS QTY " & _
                        "   FROM		viewItem_Stock " & _
                        "   WHERE       ItemCode ='" & itemCode & "' " & _
                        "   AND         Description = '" & WHSE & "' " & _
                        "   AND         UOM = '" & UOM & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    StockQTY = SQL.SQLDR("QTY") + ConvertToAltUOM(itemCode, UOM, IssuedQTY)
                    If StockQTY >= ReqQTY Then ' IF AVAILABLE STOCK IS GREATER THAN THE BOM REQUIREMENT THEN ISSUE QTY SHOULD BE THE SAME AS BOM QTY
                        IssueQTY = ReqQTY
                    Else ' IF AVAILABLE STOCK IS LESS THAN THE BOM REQUIREMENT THEN ISSUE QTY SHOULD BE ONLY THE STOCK QTY
                        IssueQTY = StockQTY
                    End If
                End If
                row.Cells(chStocks.Index).Value = CDec(StockQTY).ToString("N2")

            End If

        Next
        dgvItemList.Columns(chStocks.Index).Visible = True
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


    Private Sub SaveSIDetails(ByVal TransID As Integer)
        Dim insertSQL As String
        Dim line As Integer = 1
        For Each row As DataGridViewRow In dgvItemList.Rows
            If row.Cells(chGross.Index).Value <> Nothing Then
                insertSQL = " INSERT INTO " & _
                            " tblSI_Details(TransID, ItemCode, Description, UOM, QTY, Unit_Price, Amount, LineNum) " & _
                            " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @Unit_Price, @Amount, @LineNum) "
                SQL.FlushParams()
                SQL.AddParam("TransID", TransID)
                SQL.AddParam("ItemCode", IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value))
                SQL.AddParam("Description", row.Cells(chItemDesc.Index).Value.ToString)
                SQL.AddParam("UOM", row.Cells(chUOM.Index).Value.ToString)
                SQL.AddParam("QTY", CDec(row.Cells(chQTY.Index).Value))
                SQL.AddParam("Unit_Price", CDec(row.Cells(chUnitPrice.Index).Value))
                SQL.AddParam("Amount", CDec(row.Cells(chGross.Index).Value))
                SQL.AddParam("LineNum", line)
                SQL.ExecNonQuery(insertSQL)
                line += 1
            End If
        Next
    End Sub

    Private Sub SaveSI()
        Try
            activityStatus = True
            Dim insertSQL As String
            Dim accntCode As String
            If cbDefaultAcc.SelectedIndex = -1 Then accntCode = "" Else accntCode = GetAccntCode(cbDefaultAcc.SelectedItem)
            insertSQL = " INSERT INTO " & _
                        " tblSI  (TransID, SI_No, BranchCode, BusinessCode, VCECode, DateSI, Terms, DateDue, Remarks, DebitAccnt, " & _
                        "         GrossAmount, Discount, VATAmount, NetAmount, Currency, Exchange_Rate, VATable, VATInc, SO_Ref, PL_Ref, DR_Ref,  WhoCreated) " & _
                        " VALUES (@TransID, @SI_No, @BranchCode, @BusinessCode, @VCECode, @DateSI, @Terms,  @DateDue, @Remarks, @DebitAccnt, " & _
                        "         @GrossAmount, @Discount, @VATAmount, @NetAmount,  @Currency, @Exchange_Rate, @VATable, @VATInc,  @SO_Ref, @PL_Ref, @DR_Ref, @WhoCreated) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@SI_No", SINo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateSI", dtpDocDate.Value.Date)
            SQL.AddParam("@Terms", cbTerms.Text)
            SQL.AddParam("@DateDue", dtpDue.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@DebitAccnt", accntCode)
            SQL.AddParam("@GrossAmount", CDec(txtGross.Text))
            SQL.AddParam("@Discount", CDec(txtDiscount.Text))
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@NetAmount", CDec(txtNet.Text))
            SQL.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInc", chkVATInc.Checked)
            SQL.AddParam("@SO_Ref", SO_ID)
            SQL.AddParam("@PL_Ref", PL_ID)
            SQL.AddParam("@DR_Ref", DR_ID)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblSI_Details(TransId, ItemCode, Description, UOM, QTY, UnitPrice, GrossAmount, VATable, VATInc, VATAmount, " & _
                                "                DiscountRate, Discount, NetAmount, LineNum, WhoCreated) " & _
                                " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @UnitPrice, @GrossAmount, @VATable, @VATInc, @VATAmount, " & _
                                "        @DiscountRate, @Discount, @NetAmount, @LineNum, @WhoCreated) "
                    'insertSQL = " INSERT INTO " & _
                    '            " tblSI_Details(TransId, ItemCode, Description, UOM, QTY, UnitPrice, GrossAmount ) " & _
                    '            " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @UnitPrice, @GrossAmount) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value))
                    SQL.AddParam("@Description", row.Cells(chItemDesc.Index).Value.ToString)
                    SQL.AddParam("@UOM", row.Cells(chUOM.Index).Value.ToString)
                    SQL.AddParam("@QTY", CDec(row.Cells(chQTY.Index).Value))
                    SQL.AddParam("@UnitPrice", CDec(row.Cells(chUnitPrice.Index).Value))
                    SQL.AddParam("@GrossAmount", CDec(row.Cells(chGross.Index).Value))
                    SQL.AddParam("@VATable", row.Cells(chVAT.Index).Value)
                    SQL.AddParam("@VATInc", row.Cells(dgcVATInc.Index).Value)
                    SQL.AddParam("@VATAmount", CDec(row.Cells(chVATAmount.Index).Value))
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
            Dim SJID As Integer
            SJID = GenerateTransID("TransID", "tblSJ")
            Dim SJNo As String
            SJNo = GenerateTransNum(True, "SJ", "SJ_No", "tblSJ")
            insertSQL = " INSERT INTO " & _
                        " tblSJ (TransID, SJ_No, VCECode, BranchCode, BusinessCode, DateSJ, TotalAmount,  Currency, Exchange_Rate, Remarks, TransAuto, WhoCreated, Terms, DueDate, SIRef) " & _
                        " VALUES(@TransID, @SJ_No, @VCECode, @BranchCode, @BusinessCode, @DateSJ, @TotalAmount, @Currency, @Exchange_Rate, @Remarks, @TransAuto, @WhoCreated,  @Terms, @DueDate, @SIRef)"
            SQL.FlushParams()
            SQL.AddParam("@TransID", SJID)
            SQL.AddParam("@SJ_No", SJNo)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateSJ", dtpDocDate.Value.Date)
            SQL.AddParam("@DueDate", dtpDue.Value.Date)
            SQL.AddParam("@TotalAmount", CDec(txtGross.Text))
            SQL.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@TransAuto", TransAuto)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.AddParam("@Terms", cbTerms.Text)
            SQL.AddParam("@SIRef", txtTransNum.Text)
            SQL.ExecNonQuery(insertSQL)

            JETransID = GenerateTransID("JE_No", "tblJE_Header")

            insertSQL = " INSERT INTO " & _
                        " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR,  Currency, Exchange_Rate, Remarks, WhoCreated) " & _
                        " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Currency, @Exchange_Rate, @Remarks, @WhoCreated)"
            SQL.FlushParams()
            SQL.AddParam("@JE_No", JETransID)
            SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
            SQL.AddParam("@RefType", "SJ")
            SQL.AddParam("@RefTransID", SJID)
            SQL.AddParam("@Book", "Sales")
            SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
            SQL.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

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
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "SI_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub UpdateSI()
        Try
            activityStatus = True
            Dim SJTransID As String
            Dim accntCode As String
            If cbDefaultAcc.SelectedIndex = -1 Then accntCode = "" Else accntCode = GetAccntCode(cbDefaultAcc.SelectedItem)
            Dim insertSQL, updateSQL, deleteSQL As String
            insertSQL = " UPDATE tblSI " & _
                        " SET    SI_No = @SI_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, VCECode = @VCECode, DateSI = @DateSI, " & _
                        "        Terms = @Terms, DateDue = @DateDue, Remarks = @Remarks, DebitAccnt = @DebitAccnt, VATable = @VATable, VATInc = @VATInc, " & _
                        "        GrossAmount = @GrossAmount, Discount = @Discount, VATAmount = @VATAmount, NetAmount = @NetAmount,  Currency = @Currency, Exchange_Rate = @Exchange_Rate," & _
                        "        SO_Ref = @SO_Ref, PL_Ref = @PL_Ref, DR_Ref = @DR_Ref, WhoModified = @WhoModified " & _
                        " WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@SI_No", SINo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateSI", dtpDocDate.Value.Date)
            SQL.AddParam("@Terms", cbTerms.Text)
            SQL.AddParam("@DateDue", dtpDue.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@DebitAccnt", accntCode)
            SQL.AddParam("@GrossAmount", CDec(txtGross.Text))
            SQL.AddParam("@Discount", CDec(txtDiscount.Text))
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@NetAmount", CDec(txtNet.Text))
            SQL.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInc", chkVATInc.Checked)
            SQL.AddParam("@SO_Ref", SO_ID)
            SQL.AddParam("@PL_Ref", PL_ID)
            SQL.AddParam("@DR_Ref", DR_ID)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(insertSQL)

            deleteSQL = " DELETE FROM tblSI_Details WHERE TransID = '" & TransID & "' "
            SQL.ExecNonQuery(deleteSQL)

            Dim line As Integer = 1
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblSI_Details(TransId, ItemCode, Description, UOM, QTY, UnitPrice, GrossAmount, VATable, VATInc, VATAmount, " & _
                                "                DiscountRate, Discount, NetAmount, LineNum, WhoCreated) " & _
                                " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @UnitPrice, @GrossAmount, @VATable, @VATInc, @VATAmount, " & _
                                "        @DiscountRate, @Discount, @NetAmount, @LineNum, @WhoCreated) "
                    'insertSQL = " INSERT INTO " & _
                    '            " tblSI_Details(TransId, ItemCode, Description, UOM, QTY, UnitPrice, GrossAmount ) " & _
                    '            " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @UnitPrice, @GrossAmount) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value))
                    SQL.AddParam("@Description", row.Cells(chItemDesc.Index).Value.ToString)
                    SQL.AddParam("@UOM", row.Cells(chUOM.Index).Value.ToString)
                    SQL.AddParam("@QTY", CDec(row.Cells(chQTY.Index).Value))
                    SQL.AddParam("@UnitPrice", CDec(row.Cells(chUnitPrice.Index).Value))
                    SQL.AddParam("@GrossAmount", CDec(row.Cells(chGross.Index).Value))
                    SQL.AddParam("@VATable", row.Cells(chVAT.Index).Value)
                    SQL.AddParam("@VATInc", row.Cells(dgcVATInc.Index).Value)
                    SQL.AddParam("@VATAmount", CDec(row.Cells(chVATAmount.Index).Value))
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

            updateSQL = " UPDATE tblSJ " & _
                       " SET     BranchCode = @BranchCode, BusinessCode = @BusinessCode,  VCECode = @VCECode, DateSJ = @DateSJ, " & _
                       "        TotalAmount = @TotalAmount, Currency = @Currency, Exchange_Rate = @Exchange_Rate, Remarks = @Remarks,  WhoModified = @WhoModified, DateModified = GETDATE(), " & _
                       "        Terms = @Terms, DueDate = @DueDate, SIRef = @SIRef " & _
                       " WHERE  SIRef =(SELECT SI_No FROM tblSI WHERE TransID = '" & TransID & "') "
            SQL.FlushParams()
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateSJ", dtpDocDate.Value.Date)
            SQL.AddParam("@DueDate", dtpDue.Value.Date)
            SQL.AddParam("@TotalAmount", CDec(txtGross.Text))
            SQL.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@TransAuto", TransAuto)
            SQL.AddParam("@WhoModified", UserID)
            SQL.AddParam("@Terms", cbTerms.Text)
            SQL.AddParam("@SIRef", txtTransNum.Text)
            SQL.ExecNonQuery(updateSQL)

            Dim selectsql As String
            selectsql = " SELECT  TransID FROM tblSJ WHERE  SIRef =(SELECT SI_No FROM tblSI WHERE TransID = '" & TransID & "')"
            SQL.ReadQuery(selectsql)
            If SQL.SQLDR.Read Then
                SJTransID = SQL.SQLDR("TransID").ToString
                JETransID = LoadJE("SJ", SJTransID)
            End If


            If JETransID = 0 Then
                JETransID = GenerateTransID("JE_No", "tblJE_Header")
                insertSQL = " INSERT INTO " & _
                            " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Currency, Exchange_Rate, Remarks, WhoCreated) " & _
                            " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Currency, @Exchange_Rate,  @Remarks, @WhoCreated)"
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransID)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "SJ")
                SQL.AddParam("@RefTransID", SJTransID)
                SQL.AddParam("@Book", "Sales")
                SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@WhoCreated", UserID)
                SQL.ExecNonQuery(insertSQL)

                'JETransID = LoadJE("APV", TransID)
            Else
                updateSQL = " UPDATE tblJE_Header " & _
                            " SET    AppDate = @AppDate, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " & _
                            "        RefType = @RefType, RefTransID = @RefTransID, Book = @Book, TotalDBCR = @TotalDBCR,  Currency = @Currency, Exchange_Rate = @Exchange_Rate," & _
                            "        Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE() " & _
                            " WHERE  JE_No = @JE_No "
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransID)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "SJ")
                SQL.AddParam("@RefTransID", SJTransID)
                SQL.AddParam("@Book", "Sales")
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
                        SQL.AddParam("@RefNo", txtTransNum.Text)
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
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "SI_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub GenerateEntry()
        Dim dataEntry As New DataTable
        accntDR = GetAccntCode(cbDefaultAcc.SelectedItem)
        dgvEntry.Rows.Clear()
        dgvEntry.Rows.Add({accntDR, GetAccntTitle(accntDR), CDec(txtNet.Text).ToString("N2"), "0.00", "SI:" & txtTransNum.Text})
        Dim query As String
        For Each row As DataGridViewRow In dgvItemList.Rows
            If row.Cells(chItemCode.Index).Value <> Nothing AndAlso row.Cells(chDiscount.Index).Value > 0 Then
                query = " SELECT AD_Discount, AccountTitle " & _
                        " FROM   tblItem_Master INNER JOIN tblCOA_Master " & _
                        " ON     tblItem_Master.AD_Discount = tblCOA_Master.AccountCode " & _
                        " WHERE  ItemCode ='" & row.Cells(chItemCode.Index).Value & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read() Then
                    dgvEntry.Rows.Add({SQL.SQLDR("AD_Discount").ToString, SQL.SQLDR("AccountTitle").ToString, CDec(row.Cells(chDiscount.Index).Value).ToString("N2"), "0.00", ""})
                End If
            End If
        Next
        If txtGross.Text <> txtNet.Text Then
            dgvEntry.Rows.Add({accntVAT, GetAccntTitle(accntVAT), "0.00", CDec(txtVAT.Text).ToString("N2"), ""})
        End If


        For Each row As DataGridViewRow In dgvItemList.Rows
            If row.Cells(chItemCode.Index).Value <> Nothing AndAlso row.Cells(chGross.Index).Value > 0 Then
                query = " SELECT AD_Sales, AccountTitle " & _
                        " FROM   tblItem_Master INNER JOIN tblCOA_Master " & _
                        " ON     tblItem_Master.AD_Sales = tblCOA_Master.AccountCode " & _
                        " WHERE  ItemCode ='" & row.Cells(chItemCode.Index).Value & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read() Then
                    dgvEntry.Rows.Add({SQL.SQLDR("AD_Sales").ToString, SQL.SQLDR("AccountTitle").ToString, "0.00", CDec(row.Cells(chGross.Index).Value).ToString("N2"), ""})
                End If
            End If
        Next
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
        End If



    End Sub

    Private Sub dgvItemMaster_RowsRemoved(sender As System.Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
        ComputeTotal()
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("SI_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("SI")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadSI(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("SI_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            SINo = ""


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
        If Not AllowAccess("SI_EDIT") Then
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

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("SI_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE  tblSI SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        SINo = txtTransNum.Text
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
                        tsbCopy.Enabled = False
                        EnableControl(False)

                        SINo = txtTransNum.Text
                        LoadSI(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "SI_No", SINo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("SI", TransID)
        f.Dispose()
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If SINo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblSI  WHERE SI_No < '" & SINo & "' ORDER BY SI_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadSI(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If SINo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblSI  WHERE SI_No > '" & SINo & "' ORDER BY SI_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadSI(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If SINo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadSI(TransID)
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

    Private Sub frmSI_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub txtVCEName_KeyDown_1(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.txtFilter.Text = txtVCEName.Text
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            txtVCEName.Text = f.VCEName
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

    Private Sub tsbCopyPO_Click(sender As System.Object, e As System.EventArgs) Handles tsbCopyPO.Click
        If cbDefaultAcc.SelectedIndex = -1 Then
            Msg("Please select default Debit account first!", MsgBoxStyle.Exclamation)
        Else
            Dim f As New frmLoadTransactions
            f.cbFilter.SelectedItem = "Status"
            f.txtFilter.Text = "Closed"
            f.txtFilter.Enabled = False
            f.cbFilter.Enabled = False
            f.btnSearch.Enabled = False
            f.ShowDialog("SO")
            LoadSO(f.transID)
            f.Dispose()
        End If
    End Sub

    Private Sub LoadSO(ByVal SO_No As String)
        Dim query As String
        query = " SELECT    TransID, SO_No, VCECode, Remarks, " & _
                "            ISNULL(GrossAmount,0) AS GrossAmount, ISNULL(VATAmount,0) AS VATAmount, ISNULL(Discount,0) AS Discount, ISNULL(NetAmount,0) AS NetAmount,  " & _
                "            ISNULL(VATable,1) AS VATable, VATInclusive, Status " & _
                " FROM       tblSO " & _
                " WHERE      TransId = '" & SO_No & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            RefID = SQL.SQLDR("TransID")
            txtSORef.Text = SQL.SQLDR("SO_No").ToString
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtGross.Text = CDec(SQL.SQLDR("GrossAmount")).ToString("N2")
            txtVAT.Text = CDec(SQL.SQLDR("VATAmount")).ToString("N2")
            txtDiscount.Text = CDec(SQL.SQLDR("Discount")).ToString("N2")
            txtNet.Text = CDec(SQL.SQLDR("NetAmount")).ToString("N2")
            chkVAT.Checked = SQL.SQLDR("VATable")
            chkVATInc.Checked = SQL.SQLDR("VATInclusive")
            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            query = " SELECT    ItemCode, Description, UOM, QTY, UnitPrice, GrossAmount, DiscountRate, Discount, VATAmount, NetAmount, VATable, VATInc " & _
                " FROM      tblSO_Details " & _
                " WHERE     tblSO_Details.TransId = " & RefID & " " & _
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
                                     SQL.SQLDR("VATable"), SQL.SQLDR("VATInc"))
            End While
            LoadCurrency()
        End If
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
                        f.ShowDialog("ItemMaster", itemCode, "Sales")
                        If f.TransID <> "" Then
                            itemCode = f.ItemCode
                            RecordID = f.TransID
                            LoadItem(RecordID, itemCode)
                            LoadStock()
                        End If
                        f.Dispose()
                    End If
                Case chItemDesc.Index
                    If dgvItemList.Item(chItemDesc.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemList.Item(chItemDesc.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("ItemMaster", itemCode, "Sales")
                        If f.TransID <> "" Then
                            itemCode = f.ItemCode
                            RecordID = f.TransID
                            LoadItem(RecordID, itemCode)
                            LoadStock()
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
                Case chWarehouse.Index
                    If dgvItemList.Item(chItemCode.Index, e.RowIndex).Value <> "" Then
                        LoadStock()
                    End If
            End Select
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
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

                        dgvItemList.Item(dgcVATInc.Index, RowID).Value = False
                        VAT = 0
                        dgvItemList.Item(dgcVATInc.Index, RowID).ReadOnly = True
                    Else
                        dgvItemList.Item(chVAT.Index, RowID).Value = True

                        dgvItemList.Item(dgcVATInc.Index, RowID).ReadOnly = False
                        If dgvItemList.Item(dgcVATInc.Index, RowID).Value = False Then
                            VAT = CDec(baseVAT * 0.12).ToString("N2")
                        Else
                            baseVAT = (gross / 1.12)
                            VAT = CDec(baseVAT * 0.12).ToString("N2")
                        End If
                    End If
                ElseIf ColID = dgcVATInc.Index Then
                    If dgvItemList.Item(chVAT.Index, RowID).Value = False Then
                        VAT = 0
                    Else
                        If dgvItemList.Item(dgcVATInc.Index, RowID).Value = True Then
                            dgvItemList.Item(dgcVATInc.Index, RowID).Value = False
                            VAT = CDec(baseVAT * 0.12).ToString("N2")
                        Else
                            dgvItemList.Item(dgcVATInc.Index, RowID).Value = True
                            baseVAT = (gross / 1.12)
                            VAT = CDec(baseVAT * 0.12).ToString("N2")
                        End If

                    End If
                Else
                    If dgvItemList.Item(chVAT.Index, RowID).Value = False Then
                        VAT = 0
                        dgvItemList.Item(dgcVATInc.Index, RowID).ReadOnly = True
                    Else
                        dgvItemList.Item(dgcVATInc.Index, RowID).ReadOnly = False
                        If dgvItemList.Item(dgcVATInc.Index, RowID).Value = True Then ' IF VAT INCLUSIVE, BASE AMOUNT WILL BE GROSS / 1.12
                            baseVAT = (gross / 1.12)
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
                net = baseVAT - discount + VAT
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
        ElseIf cbDefaultAcc.SelectedItem = "" Then
            Msg("Please Select DR Account!", MsgBoxStyle.Exclamation)
        ElseIf txtConversion.Visible = True And txtConversion.Text = "" Then
            MsgBox("Please check exchange rate!", MsgBoxStyle.Exclamation)
        ElseIf dgvItemList.Rows.Count = 1 Then
            MsgBox("Please enter an item/services to purchase!", MsgBoxStyle.Exclamation)
        ElseIf TransID = "" Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                TransID = GenerateTransID(ColumnID, DBTable)
                SINo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                txtTransNum.Text = SINo
                GenerateEntry()
                SaveSI()
                Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                SINo = txtTransNum.Text
                LoadSI(TransID)
            End If
        Else
            If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                GenerateEntry()
                UpdateSI()
                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                SINo = txtTransNum.Text
                LoadSI(TransID)
            End If
        End If
    End Sub

    Private Sub cbDefaultAcc_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbDefaultAcc.KeyPress
        e.Handled = True
    End Sub

    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs)
        frmUploader.ModID = "SI"
        frmUploader.Show()
    End Sub

    Private Sub FromDRToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromDRToolStripMenuItem.Click
        If cbDefaultAcc.SelectedIndex = -1 Then
            Msg("Please select default account!", MsgBoxStyle.Exclamation)
        Else
            Dim f As New frmLoadTransactions
            f.cbFilter.SelectedItem = "Status"
            f.txtFilter.Text = "Active"
            f.txtFilter.Enabled = False
            f.cbFilter.Enabled = False
            f.btnSearch.Enabled = False
            f.ShowDialog("DR-SI")
            LoadDR(f.transID)
            f.Dispose()
        End If
    End Sub

    Private Sub dgvItemList_CurrentCellDirtyStateChanged(sender As System.Object, e As System.EventArgs) Handles dgvItemList.CurrentCellDirtyStateChanged
        If dgvItemList.SelectedCells.Count > 0 AndAlso (dgvItemList.SelectedCells(0).ColumnIndex = chVAT.Index OrElse dgvItemList.SelectedCells(0).ColumnIndex = dgcVATInc.Index) Then
            If dgvItemList.SelectedCells(0).RowIndex <> -1 Then
                Recompute(dgvItemList.SelectedCells(0).RowIndex, dgvItemList.SelectedCells(0).ColumnIndex)
                dgvItemList.SelectedCells(0).Selected = False
                dgvItemList.EndEdit()
            End If
        End If
    End Sub

    Private Sub TestToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles TestToolStripMenuItem1.Click
        Dim f As New frmReport_Filter
        f.Report = "SI List"
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged

    End Sub

    Private Sub dgvItemList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellContentClick

    End Sub

    Private Sub BIR2307ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SIPrintOutToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("SI", TransID)
        f.Dispose()
    End Sub

    Private Sub BSMofelsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BSMofelsToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("SIBS", TransID)
        f.Dispose()
    End Sub

    Private Sub PrintFMToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PrintFMToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("SIF", TransID)
        f.Dispose()
    End Sub

    Private Sub btnSearchVCE_Click(sender As Object, e As EventArgs) Handles btnSearchVCE.Click
        Dim f As New frmVCE_Search
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        txtVCEName.Text = f.VCEName
        If txtVCECode.Text <> "" Then
            Dim query As String
            query = " SELECT  Terms" & _
                " FROM     tblVCE_Master " & _
                " WHERE    VCECode = '" & txtVCECode.Text & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                cbTerms.Text = SQL.SQLDR("Terms").ToString
            End If
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

    Private Sub cbTerms_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbTerms.SelectedIndexChanged
        Dim query, DateMode As String
        Dim Days As Integer = 0
        query = " SELECT  Description, Days, DateMode " & _
                " FROM    tblTerms " & _
                " WHERE   tblTerms.Status = 'Active' AND Description = '" & cbTerms.SelectedItem & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            DateMode = SQL.SQLDR("DateMode")
            Days = SQL.SQLDR("Days")
            If DateMode = "Year" Then
                dtpDue.Value = DateAdd(DateInterval.Year, CDec(Days), dtpDocDate.Value.Date) ' GET MATURITY DATE BASED ON TERMS AND STARTING DATE
            ElseIf DateMode = "Month" Then
                dtpDue.Value = DateAdd(DateInterval.Month, CDec(Days), dtpDocDate.Value.Date) ' GET MATURITY DATE BASED ON TERMS AND STARTING DATE
            Else
                dtpDue.Value = DateAdd(DateInterval.Day, CDec(Days), dtpDocDate.Value.Date) ' GET MATURITY DATE BASED ON TERMS AND STARTING DATE
            End If
        End If
    End Sub

    Private Sub dtpDocDate_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpDocDate.ValueChanged
        If disableEvent = False Then
            If TransID = "" Then
                txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
            End If
            Dim query, DateMode As String
            Dim Days As Integer = 0
            query = " SELECT  Description, Days, DateMode " & _
                    " FROM    tblTerms " & _
                    " WHERE   tblTerms.Status = 'Active' AND Description = '" & cbTerms.SelectedItem & "'"
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                DateMode = SQL.SQLDR("DateMode")
                Days = SQL.SQLDR("Days")
                If DateMode = "Year" Then
                    dtpDue.Value = DateAdd(DateInterval.Year, CDec(Days), dtpDocDate.Value.Date) ' GET MATURITY DATE BASED ON TERMS AND STARTING DATE
                ElseIf DateMode = "Month" Then
                    dtpDue.Value = DateAdd(DateInterval.Month, CDec(Days), dtpDocDate.Value.Date) ' GET MATURITY DATE BASED ON TERMS AND STARTING DATE
                Else
                    dtpDue.Value = DateAdd(DateInterval.Day, CDec(Days), dtpDocDate.Value.Date) ' GET MATURITY DATE BASED ON TERMS AND STARTING DATE
                End If
            End If
        End If
    End Sub

    Private Sub dgvEntry_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntry.CellContentClick

    End Sub

    Private Sub txtTotalCredit_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtTotalCredit.TextChanged

    End Sub

    Private Sub txtTotalDebit_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtTotalDebit.TextChanged

    End Sub

    Private Sub Label15_Click(sender As System.Object, e As System.EventArgs) Handles Label15.Click

    End Sub
End Class