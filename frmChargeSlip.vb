Public Class frmChargeSlip
    Dim TransID, RefID, JETransID As String
    Dim CSNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "CS"
    Dim ColumnPK As String = "CS_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblCS"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim SO_ID As Integer
    Public TAX_DOV, TAX_EWT, TAX_VatPayable, TAX_CWT, AR_OutputVAT As String


    Public Overloads Function ShowDialog(ByVal docID As String) As Boolean
        TransID = docID
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmChargeSlip_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            LoadSetup()
            LoadTerms()
            If TransID <> "" Then
                LoadCS(TransID)
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
        query = " SELECT  TAX_DOV, TAX_EWT, TAX_VatPayable, TAX_CWT, TAX_OV  FROM tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            TAX_DOV = SQL.SQLDR("TAX_DOV").ToString
            TAX_EWT = SQL.SQLDR("TAX_EWT").ToString
            TAX_VatPayable = SQL.SQLDR("TAX_VatPayable").ToString
            TAX_CWT = SQL.SQLDR("TAX_CWT").ToString
            AR_OutputVAT = SQL.SQLDR("TAX_OV").ToString
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

    Private Sub LoadCharges()
        Try
            Dim dgvCB As New DataGridViewComboBoxColumn
            dgvCB = dgvItemList.Columns(chBS_Desc.Index)
            dgvCB.Items.Clear()
            Dim query As String
            query = " SELECT Description FROM tblBS_Charges WHERE Status ='Active' ORDER BY Description "
            SQL.ReadQuery(query)
            dgvCB.Items.Clear()
            While SQL.SQLDR.Read
                dgvCB.Items.Add(SQL.SQLDR("Description").ToString)
            End While
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        txtVCEName.Enabled = Value
        btnSearchVCE.Enabled = Value

        dgvItemMaster.Columns(chItem_Gross.Index).ReadOnly = True
        dgvItemMaster.Columns(chItem_VATAmount.Index).ReadOnly = True
        dgvItemMaster.Columns(chItem_NetAmount.Index).ReadOnly = True
        dgvItemMaster.AllowUserToAddRows = Value
        dgvItemMaster.AllowUserToDeleteRows = Value
        dgvItemMaster.ReadOnly = Not Value
        If Value = True Then
            dgvItemMaster.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemMaster.EditMode = DataGridViewEditMode.EditProgrammatically
        End If

        dgvItemList.AllowUserToAddRows = Value
        dgvItemList.AllowUserToDeleteRows = Value
        dgvItemList.ReadOnly = Not Value
        If Value = True Then
            dgvItemList.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemList.EditMode = DataGridViewEditMode.EditProgrammatically
        End If

        cbTerms.Enabled = Value
        txtPayPeriod.Enabled = Value
        dtpPayDate.Enabled = Value
        dtpDue.Enabled = Value
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        chkVAT.Enabled = Value
        chkEWT.Enabled = Value


        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

  

    Private Sub LoadCS(ByVal TransID As String)
        Dim query As String
        query = " SELECT   tblCS.TransID, tblCS.CS_No, VCECode, DateCS, Remarks, " & _
                "          ISNULL(GrossAmount,0) AS GrossAmount, ISNULL(VATAmount,0) AS VATAmount, ISNULL(EWTAmount,0) AS EWTAmount,  ISNULL(NetAmount,0) AS NetAmount, " & _
                "          ISNULL(VATable,1) AS VATable, ISNULL(VATInclusive,1) AS VATInclusive, " & _
                "          viewCS_Status.Status, DebitAccount " & _
                " FROM     tblCS " & _
                " LEFT JOIN   viewCS_Status " & _
                " ON   viewCS_Status.TransID = tblCS.TransID " & _
                " WHERE    tblCS.TransId = '" & TransID & "' " & _
                " ORDER BY tblCS.TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            CSNo = SQL.SQLDR("CS_No").ToString
            txtTransNum.Text = CSNo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            dtpDocDate.Text = SQL.SQLDR("DateCS").ToString
            txtGross.Text = CDec(SQL.SQLDR("GrossAmount").ToString).ToString("N2")
            txtVAT.Text = CDec(SQL.SQLDR("VATAmount").ToString).ToString("N2")
            txtEWTAmount.Text = CDec(SQL.SQLDR("EWTAmount").ToString).ToString("N2")
            txtNet.Text = CDec(SQL.SQLDR("NetAmount").ToString).ToString("N2")
            disableEvent = True
            chkVAT.Checked = SQL.SQLDR("VATable")
            disableEvent = False
            chkVATInc.Checked = SQL.SQLDR("VATInclusive")
            txtVCEName.Text = GetVCEName(txtVCECode.Text)


            EnableControl(False)
            LoadCSItem(TransID)
            LoadCSCharges(TransID)
            LoadAccountingEntry(TransID)

            ' TOOLSTRIP BUTTONS
            If txtStatus.Text = "Cancelled" Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            ElseIf txtStatus.Text = "Closed" Then
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
            If dtpDocDate.Value < GetMaxPEC() Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            End If
        End If
    End Sub

    Private Sub LoadAccountingEntry(ByVal TransID As Integer)
        Try
            Dim query As String
            query = " SELECT JE_No, VCECode, VCEName, AccntCode, AccntTitle, Particulars, RefNo, " & _
                    "        ISNULL(Debit,0) AS Debit, ISNULL(Credit,0) AS Credit " & _
                    " FROM   View_GL  " & _
                    " WHERE  RefType ='SJ' AND RefTransID IN (SELECT TransID FROM tblSJ WHERE BSID = '" & TransID & "') "
            SQL.ReadQuery(query)
            dgvEntry.Rows.Clear()
            If SQL.SQLDR.HasRows Then
                While SQL.SQLDR.Read
                    JETransID = SQL.SQLDR("JE_No").ToString
                    dgvEntry.Rows.Add(SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccntTitle").ToString, _
                                      IIf(CDec(SQL.SQLDR("Debit")) = 0, "", CDec(SQL.SQLDR("Debit")).ToString("N2")), If(CDec(SQL.SQLDR("Credit")) = 0, "", CDec(SQL.SQLDR("Credit")).ToString("N2")), _
                                      SQL.SQLDR("Particulars").ToString, SQL.SQLDR("VCECode").ToString, SQL.SQLDR("VCEName").ToString, SQL.SQLDR("RefNo").ToString)
                End While
                TotalDBCR()

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

    Protected Sub LoadCSCharges(ByVal TransID As String)
        dgvItemList.Rows.Clear()
        Dim query As String
        query = " SELECT    DescRecordID, Description,  " & _
                "           ISNULL(GrossAmount,0) AS GrossAmount, " & _
                "           ISNULL(VATAmount,0) AS VATAmount, ISNULL(NetAmount,0) AS NetAmount, ISNULL(VATable,1) AS VATable, " & _
                "            Type, tblCS_Details_Charges.VCECode, VCEName " & _
                " FROM      tblCS_Details_Charges " & _
                " LEFT JOIN viewVCE_Master ON " & _
                " viewVCE_Master.VCECode = tblCS_Details_Charges.VCECode " & _
                " WHERE     tblCS_Details_Charges.TransId = " & TransID & " " & _
                " ORDER BY  LineNum "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvItemList.Rows.Add("")
            dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chBS_RecordID.Index).Value = SQL.SQLDR("DescRecordID").ToString
            dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chBS_Desc.Index).Value = SQL.SQLDR("Description").ToString
            dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chBS_VCECode.Index).Value = SQL.SQLDR("VCECode").ToString
            dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chBS_VCEName.Index).Value = SQL.SQLDR("VCEName").ToString
            dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chBS_Amount.Index).Value = CDec(SQL.SQLDR("GrossAmount")).ToString("N2")
            dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chBS_VATAmount.Index).Value = CDec(SQL.SQLDR("VATAmount")).ToString("N2")
            dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chBS_Total.Index).Value = CDec(SQL.SQLDR("NetAmount")).ToString("N2")
            dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chBS_Type.Index).Value = SQL.SQLDR("Type").ToString
            LoadType(dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chBS_RecordID.Index).Value, dgvItemList.Rows.Count - 1)
        End While
    End Sub

    Protected Sub LoadCSItem(ByVal TransID As String)
        'dgvItemMaster.Rows.Clear()
        Dim query As String
        query = " SELECT    ItemCode, Description, VCECode, UOM, QTY, ISNULL(UnitPrice,0) AS UnitPrice, " & _
                "           ISNULL(GrossAmount,0) AS GrossAmount, ISNULL(DiscountRate,0) AS DiscountRate, ISNULL(Discount,0) AS Discount, " & _
                "           ISNULL(VATAmount,0) AS VATAmount, ISNULL(NetAmount,0) AS NetAmount, ISNULL(VATable,1) AS VATable, ISNULL(VATInc,1) AS VATInc " & _
                " FROM      tblCS_Details_Item " & _
                " WHERE     tblCS_Details_Item.TransId = " & TransID & " " & _
                " ORDER BY  LineNum "
        dgvItemMaster.Rows.Clear()
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvItemMaster.Rows.Add(SQL.SQLDR("ItemCode").ToString, SQL.SQLDR("Description").ToString, SQL.SQLDR("VCECode").ToString, GetVCEName(SQL.SQLDR("VCECode").ToString), SQL.SQLDR("UOM").ToString, _
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

    Private Sub ClearText()
        txtTransNum.Text = ""
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        dgvItemList.Rows.Clear()
        dgvEntry.Rows.Clear()
        dgvItemMaster.Rows.Clear()
        txtRemarks.Text = ""
        txtPayPeriod.Text = ""
        txtGross.Text = "0.00"
        txtNet.Text = "0.00"
        txtVAT.Text = "0.00"
        dtpDocDate.MinDate = GetMaxPEC()
        dtpDocDate.Value = Date.Today.Date
        txtStatus.Text = "Open"
    End Sub

    Public Sub LoadItemMaster(ByVal ID As String, ByVal ItemCode As String)
        Dim query As String
        query = " SELECT  ItemCode, ItemName, UOM, 1 AS QTY, VATable, " & _
                "         CASE WHEN VATable = 0 THEN UnitPrice " & _
                "              WHEN VATInclusive = 1 THEN UnitPrice/1.12 " & _
                "              ELSE UnitPrice  " & _
                "         END AS UnitPrice, " & _
                "         CASE WHEN VATable = 0 THEN 0 " & _
                "              WHEN VATInclusive = 1 THEN UnitPrice/1.12*.12 " & _
                "              ELSE UnitPrice * 0.12 " & _
                "         END AS VAT, VatInclusive " & _
                " FROM    viewItem_Price " & _
                " WHERE   ItemCode = '" & ItemCode & "'  AND Category ='Selling' AND PriceStatus = 'Active' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvItemMaster.Rows.Add(New String() {SQL.SQLDR("ItemCode").ToString, _
                                                 SQL.SQLDR("ItemName").ToString, _
                                                 "", _
                                                 "", _
                                                 SQL.SQLDR("UOM").ToString, _
                                                 SQL.SQLDR("QTY").ToString, _
                                                 Format(SQL.SQLDR("UnitPrice"), "#,###,###,###.00").ToString, _
                                                 Format(SQL.SQLDR("UnitPrice"), "#,###,###,###.00").ToString, _
                                                 "", "0.00", _
                                                 Format(SQL.SQLDR("VAT"), "#,###,###,###.00").ToString, _
                                                 Format(SQL.SQLDR("UnitPrice") + SQL.SQLDR("VAT"), "#,###,###,###.00").ToString, _
                                                 SQL.SQLDR("VAtable"), _
                                                SQL.SQLDR("VatInclusive")})
            LoadItemCharges(ItemCode)

        End While
        ComputeTotal()
    End Sub

    Public Sub LoadItemCharges(ByVal ItemCode As String)
        Dim query As String

        query = " SELECT    DescRecordID, Description,  " & _
                "           ISNULL(GrossAmount,0) AS GrossAmount, " & _
                "           0 AS VATAmount, 0 AS NetAmount, 1 AS VATable, " & _
                "            Type, tblItem_Charges.VCECode, VCEName " & _
                " FROM      tblItem_Charges " & _
                " LEFT JOIN viewVCE_Master ON " & _
                " viewVCE_Master.VCECode = tblItem_Charges.VCECode " & _
                " WHERE     tblItem_Charges.ItemCode = '" & ItemCode & "' " & _
                " ORDER BY  LineNum "
        SQL.ReadQuery(query, 3)
        While SQL.SQLDR3.Read

            dgvItemList.Rows.Add(New String() {SQL.SQLDR3("DescRecordID").ToString, _
                                        SQL.SQLDR3("Description").ToString, _
                                              SQL.SQLDR3("VCECode").ToString, _
                                              SQL.SQLDR3("VCEName").ToString, _
                                              SQL.SQLDR3("Type").ToString, _
                                               CDec(SQL.SQLDR3("GrossAmount")).ToString("N2"), _
                                               Format(0, "#,###,###,###.00").ToString, _
                                              CDec(SQL.SQLDR3("GrossAmount")).ToString("N2"), _
                                              False,
                                              False,
                                               Format(0, "#,###,###,###.00").ToString})
            LoadType(dgvItemList.Rows(dgvItemList.Rows.Count - 2).Cells(chBS_RecordID.Index).Value, dgvItemList.Rows.Count - 2)
        End While

    End Sub
    Public Sub LoadItem(ByVal ID As String)
        Dim query, Description, RecordID As String
        Dim Amount As Decimal
        query = " SELECT  Description, Amount, RecordID FROM  tblBS_Charges " & _
                " WHERE   RecordID ='" & ID & "' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            RecordID = SQL.SQLDR("RecordID")
            Description = SQL.SQLDR("Description")
            Amount = SQL.SQLDR("Amount")
            dgvItemList.Rows.Add(New String() {RecordID, _
                                         Description, _
                                               "", _
                                               "", _
                                               "", _
                                                Format(0, "#,###,###,###.00").ToString, _
                                                Format(0, "#,###,###,###.00").ToString, _
                                                Format(0, "#,###,###,###.00").ToString, _
                                               False,
                                               False,
                                                Format(0, "#,###,###,###.00").ToString})
            LoadType(ID, dgvItemList.SelectedCells(0).RowIndex)
        End While
        ComputeTotal()
    End Sub

    Private Sub LoadType(ID As String, ByVal SelectedIndex As Integer)
        Try
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemList.Item(chBS_Type.Index, SelectedIndex)
            dgvCB.Items.Clear()
            ' ADD ALL UOM
            Dim query As String
            query = " SELECT Type " & _
                 " FROM tblBS_Charges_Amount " & _
                 " WHERE RecordID ='" & ID & "'"
            SQL.ReadQuery(query, 2)
            dgvCB.Items.Clear()
            While SQL.SQLDR2.Read
                dgvCB.Items.Add(SQL.SQLDR2("Type").ToString)
            End While
            dgvCB.Items.Add("")
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub SaveBS()
        Dim SQL As New SQLControl
        Try
            SQL.BeginTransaction()
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblCS  (TransID, CS_No, BranchCode, BusinessCode, VCECode, DateCS, DateDue, Remarks,  " & _
                        "         GrossAmount, VATAmount, EWTAmount, NetAmount, VATable, VATInclusive,   WhoCreated) " & _
                        " VALUES (@TransID, @CS_No, @BranchCode, @BusinessCode, @VCECode,  @DateCS, @DateDue, @Remarks,  " & _
                        "         @GrossAmount,  @VATAmount, @EWTAmount, @NetAmount, @VATable, @VATInclusive, @WhoCreated) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@CS_No", CSNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateCS", dtpDocDate.Value.Date)
            SQL.AddParam("@DateDue", dtpDue.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@GrossAmount", CDec(txtGross.Text))
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@EWTAmount", CDec(txtEWTAmount.Text))
            SQL.AddParam("@NetAmount", CDec(txtNet.Text))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInclusive", chkVATInc.Checked)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chBS_Amount.Index).Value Is Nothing AndAlso Not row.Cells(chBS_Desc.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblCS_Details_Charges (TransId, Description, VCECode, GrossAmount, VATable, VATAmount, " & _
                                "                NetAmount, LineNum, DescRecordID, WhoCreated, Type) " & _
                                " VALUES(@TransId,  @Description, @VCECode, @GrossAmount, @VATable, @VATAmount,  " & _
                                "        @NetAmount, @LineNum, @DescRecordID, @WhoCreated, @Type) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@Description", row.Cells(chBS_Desc.Index).Value.ToString)
                    SQL.AddParam("@VCECode", row.Cells(chBS_VCECode.Index).Value.ToString)
                    SQL.AddParam("@GrossAmount", CDec(row.Cells(chBS_Amount.Index).Value))
                    SQL.AddParam("@VATable", 1)
                    SQL.AddParam("@VATAmount", CDec(row.Cells(chBS_VATAmount.Index).Value))
                    SQL.AddParam("@NetAmount", CDec(row.Cells(chBS_Total.Index).Value))
                    SQL.AddParam("@DescRecordID", row.Cells(chBS_RecordID.Index).Value)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.AddParam("@Type", row.Cells(chBS_Type.Index).Value)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next

            line = 1
            For Each row As DataGridViewRow In dgvItemMaster.Rows
                If Not row.Cells(chItem_QTY.Index).Value Is Nothing AndAlso Not row.Cells(chItem_ItemCode.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblCS_Details_Item (TransId, ItemCode, Description, VCECode, UOM, QTY, UnitPrice, GrossAmount, VATable, VATInc, VATAmount, " & _
                                "                DiscountRate, Discount, NetAmount, LineNum, WhoCreated) " & _
                                " VALUES(@TransId, @ItemCode, @Description, @VCECode, @UOM, @QTY, @UnitPrice, @GrossAmount, @VATable, @VATInc, @VATAmount, " & _
                                "        @DiscountRate, @Discount, @NetAmount, @LineNum, @WhoCreated) "
                    'insertSQL = " INSERT INTO " & _
                    '            " tblSI_Details(TransId, ItemCode, Description, UOM, QTY, UnitPrice, GrossAmount ) " & _
                    '            " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @UnitPrice, @GrossAmount) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", IIf(row.Cells(chItem_ItemCode.Index).Value = Nothing, "", row.Cells(chItem_ItemCode.Index).Value))
                    SQL.AddParam("@Description", row.Cells(chItem_ItemDesc.Index).Value.ToString)
                    SQL.AddParam("@VCECode", row.Cells(chItem_VCECode.Index).Value.ToString)
                    SQL.AddParam("@UOM", row.Cells(chItem_UOM.Index).Value.ToString)
                    SQL.AddParam("@QTY", CDec(row.Cells(chItem_QTY.Index).Value))
                    SQL.AddParam("@UnitPrice", CDec(row.Cells(chItem_UnitPrice.Index).Value))
                    SQL.AddParam("@GrossAmount", CDec(row.Cells(chItem_Gross.Index).Value))
                    SQL.AddParam("@VATable", row.Cells(chItem_VAT.Index).Value)
                    SQL.AddParam("@VATInc", row.Cells(chItem_VATInc.Index).Value)
                    SQL.AddParam("@VATAmount", CDec(row.Cells(chItem_VATAmount.Index).Value))
                    If IsNumeric(row.Cells(chItem_DiscountRate.Index).Value) Then
                        SQL.AddParam("@DiscountRate", CDec(row.Cells(chItem_DiscountRate.Index).Value))
                    Else
                        SQL.AddParam("@DiscountRate", 0)
                    End If
                    SQL.AddParam("@Discount", CDec(row.Cells(chItem_Discount.Index).Value))
                    SQL.AddParam("@NetAmount", CDec(row.Cells(chItem_NetAmount.Index).Value))
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next

            If dgvEntry.Rows.Count > 1 Then
                JETransID = GenerateTransID("JE_No", "tblJE_Header")
                insertSQL = " INSERT INTO " & _
                            " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, WhoCreated) " & _
                            " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @WhoCreated)"
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransID)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "CS")
                SQL.AddParam("@RefTransID", TransID)
                SQL.AddParam("@Book", "Inventory")
                SQL.AddParam("@TotalDBCR", CDec(txtGross.Text))
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
                        If item.Cells(chRefNo.Index).Value <> Nothing AndAlso item.Cells(chRefNo.Index).Value <> "" Then
                            SQL.AddParam("@RefNo", item.Cells(chRefNo.Index).Value.ToString)
                        Else
                            SQL.AddParam("@RefNo", "")
                        End If
                        SQL.AddParam("@LineNumber", line)
                        SQL.ExecNonQuery(insertSQL)
                        line += 1
                    End If
                Next
            End If
            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "CS_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub UpDateBS()
        Dim SQL As New SQLControl
        SQL.BeginTransaction()
        Try
            activityStatus = True
            Dim insertSQL, updateSQL, deleteSQL As String
            insertSQL = " UPDATE tblBS " & _
                        " SET    BS_No = @BS_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, VCECode = @VCECode, DateBS=@DateBS, " & _
                        "        DateDue = @DateDue, Remarks = @Remarks, " & _
                        "        GrossAmount = @GrossAmount, VATAmount = @VATAmount,  EWTAmount = @EWTAmount, NetAmount = @NetAmount, " & _
                        "        VATable = @VATable, VATInclusive = @VATInclusive,  WhoModified = @WhoModified " & _
                        " WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@BS_No", CSNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateBS", dtpDocDate.Value.Date)
            SQL.AddParam("@DateDue", dtpDue.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@GrossAmount", CDec(txtGross.Text))
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@EWTAmount", CDec(txtEWTAmount.Text))
            SQL.AddParam("@NetAmount", CDec(txtNet.Text))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInclusive", chkVATInc.Checked)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(insertSQL)

            deleteSQL = " DELETE FROM tblCS_Details_Charges WHERE TransID = '" & TransID & "' "
            SQL.ExecNonQuery(deleteSQL)

            Dim line As Integer = 1
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chBS_Amount.Index).Value Is Nothing AndAlso Not row.Cells(chBS_Desc.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblCS_Details_Charges (TransId, Description, VCECode, GrossAmount, VATable, VATAmount, " & _
                                "                NetAmount, LineNum, DescRecordID, WhoCreated, Type) " & _
                                " VALUES(@TransId,  @Description, @VCECode, @GrossAmount, @VATable, @VATAmount,  " & _
                                "        @NetAmount, @LineNum, @DescRecordID, @WhoCreated, @Type) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@Description", row.Cells(chBS_Desc.Index).Value.ToString)
                    SQL.AddParam("@VCECode", row.Cells(chBS_VCECode.Index).Value.ToString)
                    SQL.AddParam("@GrossAmount", CDec(row.Cells(chBS_Amount.Index).Value))
                    SQL.AddParam("@VATable", 1)
                    SQL.AddParam("@VATAmount", CDec(row.Cells(chBS_VATAmount.Index).Value))
                    SQL.AddParam("@NetAmount", CDec(row.Cells(chBS_Total.Index).Value))
                    SQL.AddParam("@DescRecordID", row.Cells(chBS_RecordID.Index).Value)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.AddParam("@Type", row.Cells(chBS_Type.Index).Value)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next

            deleteSQL = " DELETE FROM tblCS_Details_Item WHERE TransID = '" & TransID & "' "
            SQL.ExecNonQuery(deleteSQL)

            line = 1
            For Each row As DataGridViewRow In dgvItemMaster.Rows
                If Not row.Cells(chItem_QTY.Index).Value Is Nothing AndAlso Not row.Cells(chItem_ItemCode.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblCS_Details_Item (TransId, ItemCode, Description, VCECode, UOM, QTY, UnitPrice, GrossAmount, VATable, VATInc, VATAmount, " & _
                                "                DiscountRate, Discount, NetAmount, LineNum, WhoCreated) " & _
                                " VALUES(@TransId, @ItemCode, @Description, @VCECode, @UOM, @QTY, @UnitPrice, @GrossAmount, @VATable, @VATInc, @VATAmount, " & _
                                "        @DiscountRate, @Discount, @NetAmount, @LineNum, @WhoCreated) "
                    'insertSQL = " INSERT INTO " & _
                    '            " tblSI_Details(TransId, ItemCode, Description, UOM, QTY, UnitPrice, GrossAmount ) " & _
                    '            " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @UnitPrice, @GrossAmount) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", IIf(row.Cells(chItem_ItemCode.Index).Value = Nothing, "", row.Cells(chItem_ItemCode.Index).Value))
                    SQL.AddParam("@Description", row.Cells(chItem_ItemDesc.Index).Value.ToString)
                    SQL.AddParam("@VCECode", row.Cells(chItem_VCECode.Index).Value.ToString)
                    SQL.AddParam("@UOM", row.Cells(chItem_UOM.Index).Value.ToString)
                    SQL.AddParam("@QTY", CDec(row.Cells(chItem_QTY.Index).Value))
                    SQL.AddParam("@UnitPrice", CDec(row.Cells(chItem_UnitPrice.Index).Value))
                    SQL.AddParam("@GrossAmount", CDec(row.Cells(chItem_Gross.Index).Value))
                    SQL.AddParam("@VATable", row.Cells(chItem_VAT.Index).Value)
                    SQL.AddParam("@VATInc", row.Cells(chItem_VATInc.Index).Value)
                    SQL.AddParam("@VATAmount", CDec(row.Cells(chItem_VATAmount.Index).Value))
                    If IsNumeric(row.Cells(chItem_DiscountRate.Index).Value) Then
                        SQL.AddParam("@DiscountRate", CDec(row.Cells(chItem_DiscountRate.Index).Value))
                    Else
                        SQL.AddParam("@DiscountRate", 0)
                    End If
                    SQL.AddParam("@Discount", CDec(row.Cells(chItem_Discount.Index).Value))
                    SQL.AddParam("@NetAmount", CDec(row.Cells(chItem_NetAmount.Index).Value))
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next

            'Dim SJID As Integer
            'SJID = LoadSJID(TransID)
            'Dim SJNo As String
            'SJNo = LoadSJNo(TransID)
            'insertSQL = " UPDATE tblSJ " & _
            '            " SET    SJ_No = @SJ_No, VCECode  = @VCECode, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " & _
            '            "        DateSJ = @DateSJ, TotalAmount = @TotalAmount, Remarks = @Remarks, TransAuto = @TransAuto, WhoModified = @WhoModified, " & _
            '            "        Terms = @Terms, DueDate= @DueDate, BSRef = @BSRef, DateModified = GETDATE() " & _
            '            " WHERE  TransID = @TransID "
            'SQL.FlushParams()
            'SQL.AddParam("@TransID", SJID)
            'SQL.AddParam("@SJ_No", SJNo)
            'SQL.AddParam("@VCECode", txtVCECode.Text)
            'SQL.AddParam("@BranchCode", BranchCode)
            'SQL.AddParam("@BusinessCode", BusinessType)
            'SQL.AddParam("@DateSJ", dtpDocDate.Value.Date)
            'SQL.AddParam("@DueDate", dtpDue.Value.Date)
            'SQL.AddParam("@TotalAmount", CDec(txtGross.Text))
            'SQL.AddParam("@Remarks", txtRemarks.Text)
            'SQL.AddParam("@TransAuto", TransAuto)
            'SQL.AddParam("@WhoModified", UserID)
            'SQL.AddParam("@Terms", cbTerms.Text)
            'SQL.AddParam("@BSRef", txtTransNum.Text)
            'SQL.ExecNonQuery(insertSQL)

            'JETransID = LoadJE("SJ", SJID)

            'If JETransID = 0 Then
            '    insertSQL = " INSERT INTO " & _
            '                " tblJE_Header (AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, WhoCreated) " & _
            '                " VALUES(@AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @WhoCreated)"
            '    SQL.FlushParams()
            '    SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
            '    SQL.AddParam("@RefType", "SJ")
            '    SQL.AddParam("@RefTransID", SJID)
            '    SQL.AddParam("@Book", "Sales")
            '    SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
            '    SQL.AddParam("@Remarks", txtRemarks.Text)
            '    SQL.AddParam("@BranchCode", BranchCode)
            '    SQL.AddParam("@BusinessCode", BusinessType)
            '    SQL.AddParam("@WhoCreated", UserID)
            '    SQL.ExecNonQuery(insertSQL)

            '    JETransID = LoadJE("SJ", TransID)
            'Else
            '    updateSQL = " UPDATE tblJE_Header " & _
            '                " SET    AppDate = @AppDate, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " & _
            '                "        RefType = @RefType, RefTransID = @RefTransID, Book = @Book, TotalDBCR = @TotalDBCR, " & _
            '                "        Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE() " & _
            '                " WHERE  JE_No = @JE_No "
            '    SQL.FlushParams()
            '    SQL.AddParam("@JE_No", JETransID)
            '    SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
            '    SQL.AddParam("@RefType", "SJ")
            '    SQL.AddParam("@RefTransID", SJID)
            '    SQL.AddParam("@Book", "Sales")
            '    SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
            '    SQL.AddParam("@Remarks", txtRemarks.Text)
            '    SQL.AddParam("@BranchCode", BranchCode)
            '    SQL.AddParam("@BusinessCode", BusinessType)
            '    SQL.AddParam("@WhoModified", UserID)
            '    SQL.ExecNonQuery(updateSQL)
            'End If

            'line = 1

            '' DELETE ACCOUNTING ENTRIES
            'deleteSQL = " DELETE FROM tblJE_Details  WHERE  JE_No = @JE_No "
            'SQL.FlushParams()
            'SQL.AddParam("@JE_No", JETransID)
            'SQL.ExecNonQuery(deleteSQL)

            '' INSERT NEW ENTRIES
            'For Each item As DataGridViewRow In dgvEntry.Rows
            '    If item.Cells(chAccntCode.Index).Value <> Nothing Then
            '        insertSQL = " INSERT INTO " & _
            '                    " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber) " & _
            '                    " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber)"
            '        SQL.FlushParams()
            '        SQL.AddParam("@JE_No", JETransID)
            '        SQL.AddParam("@AccntCode", item.Cells(chAccntCode.Index).Value.ToString)
            '        If item.Cells(chVCECode.Index).Value <> Nothing AndAlso item.Cells(chVCECode.Index).Value <> "" Then
            '            SQL.AddParam("@VCECode", item.Cells(chVCECode.Index).Value.ToString)
            '        Else
            '            SQL.AddParam("@VCECode", txtVCECode.Text)
            '        End If
            '        If item.Cells(chDebit.Index).Value AndAlso IsNumeric(item.Cells(chDebit.Index).Value) Then
            '            SQL.AddParam("@Debit", CDec(item.Cells(chDebit.Index).Value))
            '        Else
            '            SQL.AddParam("@Debit", 0)
            '        End If
            '        If item.Cells(chCredit.Index).Value <> Nothing AndAlso IsNumeric(item.Cells(chCredit.Index).Value) Then
            '            SQL.AddParam("@Credit", CDec(item.Cells(chCredit.Index).Value))
            '        Else
            '            SQL.AddParam("@Credit", 0)
            '        End If
            '        If item.Cells(chParticulars.Index).Value <> Nothing AndAlso item.Cells(chParticulars.Index).Value <> "" Then
            '            SQL.AddParam("@Particulars", item.Cells(chParticulars.Index).Value.ToString)
            '        Else
            '            SQL.AddParam("@Particulars", "")
            '        End If
            '        If item.Cells(chRefNo.Index).Value <> Nothing AndAlso item.Cells(chRefNo.Index).Value <> "" Then
            '            SQL.AddParam("@RefNo", item.Cells(chRefNo.Index).Value.ToString)
            '        Else
            '            SQL.AddParam("@RefNo", "")
            '        End If
            '        SQL.AddParam("@LineNumber", line)
            '        SQL.ExecNonQuery(insertSQL)
            '        line += 1
            '    End If
            'Next
            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "CS_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub
    Public Function LoadSJID(ByVal BSNo As String) As String
        Dim query As String
        query = "SELECT TransID FROM tblSJ WHERE BSID = @BSID "
        SQL.FlushParams()
        SQL.AddParam("@BSID", BSNo)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read AndAlso Not IsDBNull(SQL.SQLDR("TransID")) Then
            Return SQL.SQLDR("TransID")
        Else
            Return 1
        End If
    End Function
    Public Function LoadSJNo(ByVal BSNo As String) As String
        Dim query As String
        query = "SELECT SJ_No AS TransID FROM tblSJ WHERE BSID = @BSID "
        SQL.FlushParams()
        SQL.AddParam("@BSID", BSNo)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read AndAlso Not IsDBNull(SQL.SQLDR("TransID")) Then
            Return SQL.SQLDR("TransID")
        Else
            Return 1
        End If
    End Function
    Private Sub GenerateEntry()
        'Dim dataEntry As New DataTable
        'dgvEntry.Rows.Clear()
        'Dim query As String



        'If txtVAT.Text <> "0.00" Then
        '    dgvEntry.Rows.Add({TAX_DOV, GetAccntTitle(TAX_DOV), "0.00", CDec(txtVAT.Text).ToString, "", "", "", "BS:" & txtTransNum.Text})
        'End If

        'If txtEWTAmount.Text <> "0.00" Then
        '    dgvEntry.Rows.Add({TAX_EWT, GetAccntTitle(TAX_EWT), CDec(txtEWTAmount.Text).ToString, "0.00", "", "", "", "BS:" & txtTransNum.Text})
        'End If

        'For Each row As DataGridViewRow In dgvItemList.Rows
        '    If row.Cells(chBS_Desc.Index).Value <> Nothing AndAlso row.Cells(chBS_Total.Index).Value <> 0 Then
        '        query = " SELECT DefaultAccount, AccountTitle " & _
        '                " FROM   tblBS_Charges INNER JOIN tblCOA_Master " & _
        '                " ON     tblBS_Charges.DefaultAccount = tblCOA_Master.AccountCode " & _
        '                " WHERE  RecordID ='" & row.Cells(chBS_RecordID.Index).Value & "' "
        '        SQL.ReadQuery(query)
        '        If SQL.SQLDR.Read() Then
        '            If row.Cells(chBS_Total.Index).Value > 0 Then
        '                dgvEntry.Rows.Add({SQL.SQLDR("DefaultAccount").ToString, SQL.SQLDR("AccountTitle").ToString, "0.00", CDec(row.Cells(chBS_Total.Index).Value).ToString("N2"), "", "", ""})
        '            Else
        '                dgvEntry.Rows.Add({SQL.SQLDR("DefaultAccount").ToString, SQL.SQLDR("AccountTitle").ToString, CDec(row.Cells(chBS_Total.Index).Value * -1).ToString("N2"), "0.00", "", "", ""})
        '            End If
        '        End If
        '    End If
        'Next

        'TotalDBCR()
    End Sub

    Private Sub TotalDBCR()
        ' COMPUTE TOTAL DEBIT
        Dim db As Decimal = 0

        For i As Integer = 0 To dgvEntry.Rows.Count - 1
            If Val(dgvEntry.Item(2, i).Value) <> 0 Then
                db = db + CDec((dgvEntry.Item(2, i).Value)).ToString("N2")
            End If
        Next
        txtTotalDebit.Text = db.ToString("N2")

        ' COMPUTE TOTAL CREDIT
        Dim b As Decimal = 0

        For i As Integer = 0 To dgvEntry.Rows.Count - 1
            If Val(dgvEntry.Item(3, i).Value) <> 0 Then
                b = b + CDec((dgvEntry.Item(3, i).Value)).ToString("N2")
            End If
        Next
        txtTotalCredit.Text = b.ToString("N2")
    End Sub



    Private Sub dgvItemMaster_RowsRemoved(sender As System.Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
        ComputeTotal()
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("CS_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("CS")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadCS(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("CS_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            CSNo = ""

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
        If Not AllowAccess("CS_EDIT") Then
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
        If Not AllowAccess("BS_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE  tblCS SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        CSNo = txtTransNum.Text
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

                        CSNo = txtTransNum.Text
                        LoadCS(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "CS_No", CSNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click

    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If CSNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblCS  WHERE CS_No < '" & CSNo & "' ORDER BY CS_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadCS(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If CSNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblCS  WHERE CS_No > '" & CSNo & "' ORDER BY CS_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadCS(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If CSNo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadCS(TransID)
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
        Dim VCENameText As String = ""
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.txtFilter.Text = txtVCEName.Text
            VCENameText = txtVCEName.Text
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
                f.Dispose()
                LoadCurrency()
            Else
                If MsgBox("Add New Customer, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    frmVCE_Add.ShowDialog("Customer", VCENameText)

                    txtVCECode.Text = frmVCE_Add.VCECode
                    txtVCEName.Text = GetVCEName(frmVCE_Add.VCECode)
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
                    Else
                        txtVCEName.Text = ""
                    End If
                End If
            End If
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

    Private Sub dgvItemList_CellBeginEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvItemList.CellBeginEdit
        eColIndex = e.ColumnIndex
    End Sub

    Private Sub dgvItemList_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellClick
        Try
            If e.ColumnIndex = chBS_Type.Index Then
                If e.RowIndex <> -1 AndAlso dgvItemList.Rows.Count > 0 AndAlso dgvItemList.Item(chBS_RecordID.Index, e.RowIndex).Value <> Nothing Then

                    LoadType(dgvItemList.Item(chBS_RecordID.Index, e.RowIndex).Value.ToString, e.RowIndex)
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
                            LoadItem(desc)
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
                Case chBS_Amount.Index
                    If IsNumeric(dgvItemList.Item(chBS_Amount.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex, dgvItemList)
                        ComputeTotal()
                    End If
                Case chBS_Type.Index
                    Dim dgvType, dgvRecordID As String
                    dgvType = dgvItemList.Rows(e.RowIndex).Cells(chBS_Type.Index).Value
                    dgvRecordID = dgvItemList.Rows(e.RowIndex).Cells(chBS_RecordID.Index).Value
                    LoadTypeAmount(dgvRecordID, dgvType, e.RowIndex, chBS_Type.Index, chBS_Amount.Index)

                    If IsNumeric(dgvItemList.Item(chBS_Amount.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex, dgvItemList)
                        ComputeTotal()
                    End If
            End Select
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
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

    Private Sub ComputeTotal()
        If dgvItemList.Rows.Count > 0 Then
            Dim b As Decimal = 0
            Dim a As Decimal = 0
            Dim c As Decimal = 0
            Dim d As Decimal = 0
            ' COMPUTE GROSS
            For i As Integer = 0 To dgvItemList.Rows.Count - 1
                If Val(dgvItemList.Item(chBS_Amount.Index, i).Value) <> 0 Then
                    If IsNumeric(dgvItemList.Item(chBS_Amount.Index, i).Value) Then
                        a = a + Double.Parse(dgvItemList.Item(chBS_Amount.Index, i).Value).ToString
                    End If
                End If
            Next
            txtGross.Text = a.ToString("N2")

            txtVAT.Text = "0.00"
            txtEWTAmount.Text = "0.00"

            ' COMPUTE NET
            For i As Integer = 0 To dgvItemList.Rows.Count - 1
                If Val(dgvItemList.Item(chBS_Total.Index, i).Value) <> 0 Then
                    If IsNumeric(dgvItemList.Item(chBS_Total.Index, i).Value) Then
                        d = d + Double.Parse(dgvItemList.Item(chBS_Total.Index, i).Value).ToString
                    End If
                End If
            Next
            txtNet.Text = d.ToString("N2")

            '  ComputeVaTEWTAmount()
        End If

        If dgvItemMaster.Rows.Count > 0 Then
            Dim b As Decimal = 0
            Dim a As Decimal = 0
            Dim c As Decimal = 0
            Dim d As Decimal = 0
            ' COMPUTE GROSS
            For i As Integer = 0 To dgvItemMaster.Rows.Count - 1
                If Val(dgvItemMaster.Item(chItem_Gross.Index, i).Value) <> 0 Then
                    If IsNumeric(dgvItemMaster.Item(chItem_Gross.Index, i).Value) Then
                        a = a + Double.Parse(dgvItemMaster.Item(chItem_Gross.Index, i).Value).ToString
                    End If
                End If
            Next
            txtGross.Text = CDec(txtGross.Text) + a

            ' COMPUTE DISCOUNT
            For i As Integer = 0 To dgvItemMaster.Rows.Count - 1
                If Val(dgvItemMaster.Item(chItem_Discount.Index, i).Value) <> 0 Then
                    If IsNumeric(dgvItemMaster.Item(chItem_Discount.Index, i).Value) Then
                        b = b + Double.Parse(dgvItemMaster.Item(chItem_Discount.Index, i).Value)
                    End If
                End If
            Next
            txtEWTAmount.Text = CDec(txtEWTAmount.Text) + b


            ' COMPUTE VAT
            For i As Integer = 0 To dgvItemMaster.Rows.Count - 1
                If Val(dgvItemMaster.Item(chItem_VATAmount.Index, i).Value) <> 0 Then
                    If IsNumeric(dgvItemMaster.Item(chItem_VATAmount.Index, i).Value) Then
                        c = c + Double.Parse(dgvItemMaster.Item(chItem_VATAmount.Index, i).Value).ToString
                    End If
                End If
            Next
            txtVAT.Text = CDec(txtVAT.Text) + c

            ' COMPUTE NET
            For i As Integer = 0 To dgvItemMaster.Rows.Count - 1
                If Val(dgvItemMaster.Item(chItem_NetAmount.Index, i).Value) <> 0 Then
                    If IsNumeric(dgvItemMaster.Item(chItem_NetAmount.Index, i).Value) Then
                        d = d + Double.Parse(dgvItemMaster.Item(chItem_NetAmount.Index, i).Value).ToString
                    End If
                End If
            Next
            txtNet.Text = CDec(txtNet.Text) + d
        End If
    End Sub

    Private Sub Recompute(ByVal RowID As Integer, ByVal ColID As Integer, dgv As DataGridView)
        'Dim Amount, VAT, Total As Decimal
        'If RowID <> -1 Thenf
        '    If IsNumeric(dgvItemList.Item(chAmount.Index, RowID).Value) Then
        '        Amount = CDec(dgvItemList.Item(chAmount.Index, RowID).Value)

        '        Total = Amount + VAT
        '        dgvItemList.Item(chAmount.Index, RowID).Value = Format(Amount, "#,###,###,###.00").ToString()
        '        dgvItemList.Item(chTotal.Index, RowID).Value = Format(Total, "#,###,###,###.00").ToString()
        '        ComputeTotal()
        '    End If
        'End If
        Dim gross, VAT, discount, net, baseVAT As Decimal
        If dgv.Name = dgvItemList.Name Then
            If RowID <> -1 Then
                If dgv.Rows.Count > 1 Then
                    If IsNumeric(dgv.Item(chBS_Amount.Index, RowID).Value) Then
                        ' GET GROSS AMOUNT (VAT INCLUSIVE)
                        gross = CDec(dgv.Item(chBS_Amount.Index, RowID).Value)
                        baseVAT = gross
                        ' COMPUTE VAT IF VATABLE
                        If ColID = chBS_VAT.Index Then
                            If dgv.Item(chBS_VAT.Index, RowID).Value = True Then
                                dgv.Item(chBS_VAT.Index, RowID).Value = False

                                dgv.Item(chBS_VATInc.Index, RowID).Value = False
                                VAT = 0
                                dgv.Item(chBS_VATInc.Index, RowID).ReadOnly = True
                            Else
                                dgv.Item(chBS_VAT.Index, RowID).Value = True

                                dgv.Item(chBS_VATInc.Index, RowID).ReadOnly = False
                                If dgv.Item(chBS_VATInc.Index, RowID).Value = False Then
                                    VAT = CDec(baseVAT * 0.12).ToString("N2")
                                Else
                                    baseVAT = (gross / 1.12)
                                    VAT = CDec(baseVAT * 0.12).ToString("N2")
                                End If
                            End If
                        ElseIf ColID = chBS_VATInc.Index Then
                            If dgv.Item(chBS_VAT.Index, RowID).Value = False Then
                                VAT = 0
                            Else
                                If dgv.Item(chBS_VATInc.Index, RowID).Value = True Then
                                    dgv.Item(chBS_VATInc.Index, RowID).Value = False
                                    VAT = CDec(baseVAT * 0.12).ToString("N2")
                                Else
                                    dgv.Item(chBS_VATInc.Index, RowID).Value = True
                                    baseVAT = (gross / 1.12)
                                    VAT = CDec(baseVAT * 0.12).ToString("N2")
                                End If

                            End If
                        Else
                            If dgv.Item(chBS_VAT.Index, RowID).Value = False Then
                                VAT = 0
                                dgv.Item(chBS_VATInc.Index, RowID).ReadOnly = True
                            Else
                                dgv.Item(chBS_VATInc.Index, RowID).ReadOnly = False
                                If dgv.Item(chBS_VATInc.Index, RowID).Value = True Then ' IF VAT INCLUSIVE, BASE AMOUNT WILL BE GROSS / 1.12
                                    baseVAT = (gross / 1.12)
                                End If
                                VAT = CDec(baseVAT * 0.12).ToString("N2")
                            End If
                        End If

                        ' COMPUTE DISCOUNT

                        'If IsNumeric(dgv.Item(chDiscountRate.Index, RowID).Value) Then
                        '    discount = CDec(baseVAT * (CDec(dgv.Item(chDiscountRate.Index, RowID).Value) / 100.0)).ToString("N2")
                        'ElseIf IsNumeric(dgv.Item(chDiscount.Index, RowID).Value) Then
                        '    discount = CDec(dgv.Item(chDiscount.Index, RowID).Value)
                        'Else
                        discount = 0
                        'End If

                        If dgv.Item(chBS_VATInc.Index, RowID).Value = False Then

                            net = baseVAT - discount + VAT
                        Else
                            net = baseVAT - discount
                        End If
                        'net = baseVAT - discount + VAT
                        dgv.Item(chBS_Amount.Index, RowID).Value = Format(gross, "#,###,###,###.00").ToString()
                        'dgv.Item(chDiscount.Index, RowID).Value = Format(discount, "#,###,###,###.00").ToString()
                        dgv.Item(chBS_VATAmount.Index, RowID).Value = Format(VAT, "#,###,###,###.00").ToString()
                        dgv.Item(chBS_Total.Index, RowID).Value = Format(net, "#,###,###,###.00").ToString()
                        ComputeTotal()

                    End If
                End If
            End If
        Else
            gross = 0
            VAT = 0
            discount = 0
            net = 0
            baseVAT = 0
            If RowID <> -1 Then
                If dgv.Rows.Count > 1 Then
                    If IsNumeric(dgv.Item(chItem_Gross.Index, RowID).Value) Then
                        ' GET GROSS AMOUNT (VAT INCLUSIVE)
                        gross = CDec(dgv.Item(chItem_UnitPrice.Index, RowID).Value) * CDec(dgv.Item(chItem_QTY.Index, RowID).Value)
                        baseVAT = gross
                        ' COMPUTE VAT IF VATABLE
                        If ColID = chItem_VAT.Index Then
                            If dgv.Item(chItem_VAT.Index, RowID).Value = True Then
                                dgv.Item(chItem_VAT.Index, RowID).Value = False

                                dgv.Item(chItem_VATInc.Index, RowID).Value = False
                                VAT = 0
                                dgv.Item(chItem_VATInc.Index, RowID).ReadOnly = True
                            Else
                                dgv.Item(chItem_VAT.Index, RowID).Value = True

                                dgv.Item(chItem_VATInc.Index, RowID).ReadOnly = False
                                If dgv.Item(chItem_VATInc.Index, RowID).Value = False Then
                                    VAT = CDec(baseVAT * 0.12).ToString("N2")
                                Else
                                    baseVAT = (gross / 1.12)
                                    VAT = CDec(baseVAT * 0.12).ToString("N2")
                                End If
                            End If
                        ElseIf ColID = chItem_VATInc.Index Then
                            If dgv.Item(chItem_VAT.Index, RowID).Value = False Then
                                VAT = 0
                            Else
                                If dgv.Item(chItem_VATInc.Index, RowID).Value = True Then
                                    dgv.Item(chItem_VATInc.Index, RowID).Value = False
                                    VAT = CDec(baseVAT * 0.12).ToString("N2")
                                Else
                                    dgv.Item(chItem_VATInc.Index, RowID).Value = True
                                    'baseVAT = (gross / 1.12)
                                    'VAT = CDec(baseVAT * 0.12).ToString("N2")
                                    'baseVAT = gross
                                    VAT = gross - (gross / 1.12)
                                    gross = (gross / 1.12)
                                End If

                            End If
                        Else
                            If dgv.Item(chItem_VAT.Index, RowID).Value = False Then
                                VAT = 0
                                dgv.Item(chItem_VATInc.Index, RowID).ReadOnly = True
                            Else
                                dgv.Item(chItem_VATInc.Index, RowID).ReadOnly = False
                                If dgv.Item(chItem_VATInc.Index, RowID).Value = True Then ' IF VAT INCLUSIVE, BASE AMOUNT WILL BE GROSS / 1.12
                                    'baseVAT = (gross / 1.12)

                                    VAT = gross - (gross / 1.12)
                                    gross = (gross / 1.12)
                                Else

                                    VAT = CDec(baseVAT * 0.12).ToString("N2")
                                End If
                            End If
                        End If

                        ' COMPUTE DISCOUNT

                        If IsNumeric(dgv.Item(chItem_DiscountRate.Index, RowID).Value) Then
                            discount = CDec(baseVAT * (CDec(dgv.Item(chItem_DiscountRate.Index, RowID).Value) / 100.0)).ToString("N2")
                        ElseIf IsNumeric(dgv.Item(chItem_Discount.Index, RowID).Value) Then
                            discount = CDec(dgv.Item(chItem_Discount.Index, RowID).Value)
                        Else
                            discount = 0
                        End If

                        If dgv.Item(chItem_VATInc.Index, RowID).Value = False Then

                            net = baseVAT - discount + VAT
                        Else
                            net = baseVAT - discount
                        End If
                        'net = baseVAT - discount + VAT
                        dgv.Item(chItem_Gross.Index, RowID).Value = Format(gross, "#,###,###,###.00").ToString()
                        dgv.Item(chItem_Discount.Index, RowID).Value = Format(discount, "#,###,###,###.00").ToString()
                        dgv.Item(chItem_VATAmount.Index, RowID).Value = Format(VAT, "#,###,###,###.00").ToString()
                        dgv.Item(chItem_NetAmount.Index, RowID).Value = Format(net, "#,###,###,###.00").ToString()
                        ComputeTotal()

                    End If
                End If
            End If
        End If

    End Sub

    Private Sub ComputeVaTEWTAmount()
        Dim gross, VATRate, VATAmount, EWTAmount, EWTRate As Decimal
        gross = CDec(IIf(txtGross.Text = "", "0.00", txtGross.Text)).ToString("N2")
        VATRate = CDec(12 / 100)
        If chkVAT.Checked Then
            VATAmount = CDec(gross * VATRate).ToString("N2")
        Else
            VATAmount = 0
        End If
        txtVAT.Text = CDec(VATAmount).ToString("N2")

        EWTRate = CDec(2 / 100)
        If chkEWT.Checked Then
            EWTAmount = CDec(gross * EWTRate).ToString("N2")
        Else
            EWTAmount = 0
        End If
        txtEWTAmount.Text = CDec(EWTAmount).ToString("N2")


        txtNet.Text = CDec((gross + VATAmount) - EWTAmount).ToString("N2")
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        Try
            If txtVCECode.Text = "" Then
                Msg("Please enter VCECode!", MsgBoxStyle.Exclamation)
            ElseIf dgvItemList.Rows.Count = 1 And dgvItemMaster.Rows.Count = 1 Then
                MsgBox("Please enter an item/services to purchase!", MsgBoxStyle.Exclamation)
            ElseIf TransAuto = False And txtTransNum.Text = "" Then
                MsgBox("Please enter CS No!", MsgBoxStyle.Exclamation)
            ElseIf TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID(ColumnID, DBTable)
                    If TransAuto Then
                        CSNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    Else
                        CSNo = txtTransNum.Text
                    End If
                    txtTransNum.Text = CSNo

                    'GenerateEntry()
                    SaveBS()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    CSNo = txtTransNum.Text
                    LoadCS(TransID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    CSNo = txtTransNum.Text
                    'GenerateEntry()
                    UpDateBS()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    CSNo = txtTransNum.Text
                    LoadCS(TransID)
                End If
            End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub cbDefaultAcc_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs)
        e.Handled = True
    End Sub

    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs)
        frmUploader.ModID = "SI"
        frmUploader.Show()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem1.Click
        frmBS_Charges.Show()
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
            f.Dispose()
        End If
    End Sub

    Private Sub cbDefaultAcc_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Function Tru() As Boolean
        Throw New NotImplementedException
    End Function

    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged

    End Sub

    Private Sub dgvItemList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellContentClick

    End Sub

    Private Sub chkVAT_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkVAT.CheckedChanged, chkEWT.CheckedChanged
        If disableEvent = False Then
            ComputeTotal()
        End If
    End Sub

    Private Sub dgvItemList_DataError(sender As Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvItemList.DataError
        Try

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
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
            End If
        End If

        'Remove the handle to this event. It will be readded each time a new combobox selection causes the EditingControlShowing Event to fire
        RemoveHandler editingComboBox.SelectionChangeCommitted, AddressOf editingComboBox_SelectionChangeCommitted
        'Re-enable the EditingControlShowing event so the above can take place.
        AddHandler dgvItemList.EditingControlShowing, AddressOf dgvItemList_EditingControlShowing
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemMaster.CellContentClick

    End Sub

    Private Sub dgvItemMaster_CellContentDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemMaster.CellContentDoubleClick

    End Sub

    Private Sub dgvItemMaster_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemMaster.CellEndEdit
        Try
            Dim itemCode, RecordID As String
            Dim rowIndex As Integer = dgvItemMaster.CurrentCell.RowIndex
            Dim colindex As Integer = dgvItemMaster.CurrentCell.ColumnIndex
            Select Case colindex
                Case chItem_ItemCode.Index
                    If dgvItemMaster.Item(chItem_ItemCode.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemMaster.Item(chItem_ItemCode.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("ItemMaster", itemCode, "Sales")
                        If f.TransID <> "" Then
                            itemCode = f.ItemCode
                            RecordID = f.TransID
                            LoadItemMaster(RecordID, itemCode)
                        End If
                        Recompute(e.RowIndex, e.ColumnIndex, dgvItemMaster)
                        ComputeTotal()
                        f.Dispose()
                    End If
                Case chItem_ItemDesc.Index
                    If dgvItemMaster.Item(chItem_ItemDesc.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemMaster.Item(chItem_ItemDesc.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("ItemMaster", itemCode, "Sales")
                        If f.TransID <> "" Then
                            itemCode = f.ItemCode
                            RecordID = f.TransID
                            LoadItemMaster(RecordID, itemCode)
                        End If
                        dgvItemMaster.Rows.RemoveAt(e.RowIndex)
                        Recompute(e.RowIndex, e.ColumnIndex, dgvItemMaster)
                        ComputeTotal()
                        f.Dispose()
                    End If
                Case chItem_VCEName.Index
                    Dim f As New frmVCE_Search
                    f.txtFilter.Text = dgvItemMaster.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                    f.ShowDialog()
                    dgvItemMaster.Item(chItem_VCECode.Index, e.RowIndex).Value = f.VCECode
                    dgvItemMaster.Item(chItem_VCEName.Index, e.RowIndex).Value = f.VCEName
                    f.Dispose()
                Case chItem_QTY.Index
                    If dgvItemMaster.Item(e.ColumnIndex, e.RowIndex).Value = "" OrElse IsNothing(dgvItemMaster.Item(e.ColumnIndex, e.RowIndex).Value) Then
                        dgvItemMaster.Item(e.ColumnIndex, e.RowIndex).Value = 0
                    ElseIf IsNumeric(dgvItemMaster.Item(chItem_UnitPrice.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemMaster.Item(chItem_QTY.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex, dgvItemMaster)
                        ComputeTotal()
                    End If
                Case chItem_UnitPrice.Index
                    If dgvItemMaster.Item(e.ColumnIndex, e.RowIndex).Value = "" OrElse IsNothing(dgvItemMaster.Item(e.ColumnIndex, e.RowIndex).Value) Then
                        dgvItemMaster.Item(e.ColumnIndex, e.RowIndex).Value = 0
                    ElseIf IsNumeric(dgvItemList.Item(chItem_UnitPrice.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemMaster.Item(chItem_QTY.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex, dgvItemMaster)
                        ComputeTotal()
                        dgvItemMaster.Item(chItem_UnitPrice.Index, e.RowIndex).Value = CDec(dgvItemMaster.Item(chItem_UnitPrice.Index, e.RowIndex).Value).ToString("N2")
                    End If
                Case chItem_DiscountRate.Index
                    If IsNumeric(dgvItemMaster.Item(chItem_Gross.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemMaster.Item(chItem_DiscountRate.Index, e.RowIndex).Value) Then
                        ' txtDiscountRate.Text = ""
                        Recompute(e.RowIndex, e.ColumnIndex, dgvItemMaster)
                        ComputeTotal()
                    End If
                Case chItem_Discount.Index
                    dgvItemMaster.Item(chItem_DiscountRate.Index, e.RowIndex).Value = Nothing
                    If IsNumeric(dgvItemMaster.Item(chItem_Gross.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemMaster.Item(chItem_Discount.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex, dgvItemMaster)
                        ComputeTotal()
                    End If
            End Select
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub dgvItemMaster_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dgvItemMaster.CurrentCellDirtyStateChanged
        If dgvItemMaster.SelectedCells.Count > 0 AndAlso (dgvItemMaster.SelectedCells(0).ColumnIndex = chItem_VAT.Index OrElse dgvItemMaster.SelectedCells(0).ColumnIndex = chItem_VATInc.Index) Then
            If dgvItemMaster.SelectedCells(0).RowIndex <> -1 Then
                Recompute(dgvItemMaster.SelectedCells(0).RowIndex, dgvItemMaster.SelectedCells(0).ColumnIndex, dgvItemMaster)
                dgvItemMaster.SelectedCells(0).Selected = False
                dgvItemMaster.EndEdit()
            End If
        End If
    End Sub

    Dim eColIndex As Integer = 0
    Private Sub dgvItemList_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dgvItemList.CurrentCellDirtyStateChanged
        If dgvItemList.SelectedCells.Count > 0 AndAlso (dgvItemList.SelectedCells(0).ColumnIndex = chBS_VAT.Index OrElse dgvItemList.SelectedCells(0).ColumnIndex = chBS_VATInc.Index) Then
            If dgvItemList.SelectedCells(0).RowIndex <> -1 Then
                Recompute(dgvItemList.SelectedCells(0).RowIndex, dgvItemList.SelectedCells(0).ColumnIndex, dgvItemList)
                dgvItemList.SelectedCells(0).Selected = False
                dgvItemList.EndEdit()
            End If
        End If


        If eColIndex = chBS_Type.Index And TypeOf (dgvItemList.CurrentRow.Cells(chBS_Type.Index)) Is DataGridViewComboBoxCell Then
            dgvItemList.CommitEdit(DataGridViewDataErrorContexts.Commit)
            dgvItemList.EndEdit()
        End If
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Dim f As New frmReport_Display
        f.ShowDialog("CS", TransID)
        f.Dispose()
    End Sub

    Private Sub CSSummaryToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CSSummaryToolStripMenuItem.Click
        Dim f As New frmReport_Filter
        f.Report = "CS Summary"
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub dgvItemList_CellEnter(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellEnter

    End Sub

    Private Sub dgvItemMaster_RowsRemoved1(sender As Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgvItemMaster.RowsRemoved
        Try
            ComputeTotal()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub dgvItemList_RowsRemoved(sender As Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgvItemList.RowsRemoved
        Try
            ComputeTotal()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class