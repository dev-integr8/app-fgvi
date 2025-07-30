Public Class frmBS
    Dim TransID, JETransID As String
    Dim BSNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "BS"
    Dim ColumnPK As String = "BS_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblBS"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim LC_ID, BM_ID As Integer
    Dim LC_No, BM_No As String
    Public TAX_DOV, TAX_EWT, TAX_VatPayable, TAX_CWT, AR_OutputVAT As String
    Dim CostCenter As String
    Dim ForApproval As Boolean = False


    Public Overloads Function ShowDialog(ByVal docID As String) As Boolean
        TransID = docID
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmBS_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            LoadARAccount()
            LoadSetup()
            LoadTerms()
            LoadCostCenter()

            If TransID <> "" Then
                LoadBS(TransID)
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
    Private Sub LoadCostCenter()
        Dim query As String
        query = " SELECT Code FROM tblCC WHERE Status = 'Active' "
        'SQL.FlushParams()
        SQL.ReadQuery(query)
        cbCostCenter.Items.Clear()
        cbCostCenter.Items.Add("")
        While SQL.SQLDR.Read
            cbCostCenter.Items.Add(SQL.SQLDR("Code").ToString)
        End While
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
            dgvCB = dgvItemList.Columns(chDesc.Index)
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
        dgvItemList.AllowUserToAddRows = Value
        dgvItemList.AllowUserToDeleteRows = Value
        dgvItemList.ReadOnly = Not Value
        cbCostCenter.Enabled = Value
        If Value = True Then
            dgvItemList.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemList.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        cbTerms.Enabled = Value
        cbDefaultAcc.Enabled = Value
        txtPayPeriod.Enabled = Value
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

    Private Sub LoadARAccount()
        Dim query As String
        query = " SELECT  AccountCode, AccountTitle " & _
                " FROM    tblCOA_Master " & _
                " WHERE   AccountCode IN (SELECT AccntCode FROM tblDefaultAccount WHERE Type ='AR') " & _
                " ORDER BY AccountTitle "
        SQL.ReadQuery(query)
        cbDefaultAcc.Items.Clear()
        While SQL.SQLDR.Read
            cbDefaultAcc.Items.Add(SQL.SQLDR("AccountTitle"))
        End While
    End Sub

    Private Sub LoadBS(ByVal TransID As String)
        Dim query As String
        query = " SELECT   TransID, BS_No, VCECode, DateBS, Remarks, " &
                "          ISNULL(GrossAmount,0) AS GrossAmount, ISNULL(VATAmount,0) AS VATAmount, ISNULL(EWTAmount,0) AS EWTAmount,  ISNULL(NetAmount,0) AS NetAmount, " &
                "          ISNULL(VATable,1) AS VATable, ISNULL(VATInclusive,1) AS VATInclusive,  ISNULL(LC_ID,0) AS LC_ID, CostCenter," &
                "          Status, DebitAccount " &
                " FROM     tblBS " &
                " WHERE    TransId = '" & TransID & "' " &
                " ORDER BY TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            EnableControl(False)
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            BSNo = SQL.SQLDR("BS_No").ToString
            txtTransNum.Text = BSNo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            dtpDocDate.Text = SQL.SQLDR("DateBS").ToString
            txtGross.Text = CDec(SQL.SQLDR("GrossAmount").ToString).ToString("N2")
            txtVAT.Text = CDec(SQL.SQLDR("VATAmount").ToString).ToString("N2")
            txtEWTAmount.Text = CDec(SQL.SQLDR("EWTAmount").ToString).ToString("N2")
            txtNet.Text = CDec(SQL.SQLDR("NetAmount").ToString).ToString("N2")
            disableEvent = True
            chkVAT.Checked = SQL.SQLDR("VATable")
            chkVATInc.Checked = SQL.SQLDR("VATInclusive")
            cbDefaultAcc.SelectedItem = SQL.SQLDR("DebitAccount").ToString
            cbCostCenter.SelectedValue = SQL.SQLDR("CostCenter").ToString

            disableEvent = False
            LC_ID = SQL.SQLDR("LC_ID").ToString
            LC_No = LoadLCNo(LC_ID)
            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            LoadBSDetails(TransID)
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
            If dtpDocDate.Value < GetMaxPEC() Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            End If
        Else
            ClearText()
        End If
    End Sub
    Private Function LoadLCNo(LC_ID As Integer) As String
        Dim query As String
        query = " SELECT LC_No FROM tblLC WHERE TransID = '" & LC_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("LC_No").ToString
        Else
            Return ""
        End If
    End Function
    Private Sub LoadAccountingEntry(ByVal TransID As Integer)
        Try
            Dim query As String
            query = " SELECT JE_No, VCECode, VCEName, AccntCode, AccntTitle, Particulars, RefNo, CostCenter, " &
                    "        ISNULL(Debit,0) AS Debit, ISNULL(Credit,0) AS Credit " &
                    " FROM   View_GL_Transaction  " &
                    " WHERE  RefType ='SJ' AND RefTransID IN (SELECT TransID FROM tblSJ WHERE BSID = '" & TransID & "') "
            SQL.ReadQuery(query)
            dgvEntry.Rows.Clear()
            If SQL.SQLDR.HasRows Then
                While SQL.SQLDR.Read
                    JETransID = SQL.SQLDR("JE_No").ToString
                    dgvEntry.Rows.Add(SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccntTitle").ToString,
                                      IIf(CDec(SQL.SQLDR("Debit")) = 0, "", CDec(SQL.SQLDR("Debit")).ToString("N2")), If(CDec(SQL.SQLDR("Credit")) = 0, "", CDec(SQL.SQLDR("Credit")).ToString("N2")),
                                      SQL.SQLDR("Particulars").ToString, SQL.SQLDR("VCECode").ToString, SQL.SQLDR("VCEName").ToString, SQL.SQLDR("RefNo").ToString, SQL.SQLDR("CostCenter"))
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

    Protected Sub LoadBSDetails(ByVal TransID As String)
        dgvItemList.Rows.Clear()
        Dim query As String
        query = " SELECT    DescRecordID, Description,  " &
                "           ISNULL(GrossAmount,0) AS GrossAmount, " &
                "           ISNULL(VATAmount,0) AS VATAmount, ISNULL(NetAmount,0) AS NetAmount, " &
                "           ISNULL(VATable,1) AS VATable, ISNULL(VATInc,1) AS VATInc, ATCCode, ISNULL(EWTAmount,0) AS EWTAmount, CostCenter " &
                " FROM      tblBS_Details " &
                " WHERE     tblBS_Details.TransId = " & TransID & " " &
                " ORDER BY  LineNum "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvItemList.Rows.Add("")
            dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chChargeID.Index).Value = SQL.SQLDR("DescRecordID").ToString
            dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chDesc.Index).Value = SQL.SQLDR("Description").ToString
            dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chAmount.Index).Value = CDec(SQL.SQLDR("GrossAmount")).ToString("N2")
            dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chVATAmount.Index).Value = CDec(SQL.SQLDR("VATAmount")).ToString("N2")
            dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chTotal.Index).Value = CDec(SQL.SQLDR("NetAmount")).ToString("N2")
            dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chEWTAmount.Index).Value = CDec(SQL.SQLDR("EWTAmount")).ToString("N2")
            dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chATCCode.Index).Value = SQL.SQLDR("ATCCode").ToString
            dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(dgcVAT.Index).Value = SQL.SQLDR("VATable")
            dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(dgcVATInc.Index).Value = SQL.SQLDR("VATInc")
            dgvItemList.Rows(dgvItemList.Rows.Count - 1).Cells(chCost_Center.Index).Value = SQL.SQLDR("CostCenter")
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
        dgvEntry.Rows.Clear()
        dgvItemList.Rows.Clear()
        txtRemarks.Text = ""
        txtPayPeriod.Text = ""
        txtGross.Text = "0.00"
        txtNet.Text = "0.00"
        txtVAT.Text = "0.00"
        dtpDocDate.MinDate = GetMaxPEC()
        dtpDocDate.Value = Date.Today.Date
        txtStatus.Text = "Open"
    End Sub


    Public Sub LoadItem(ByVal ID As String)
        Dim query, Description, RecordID As String
        Dim VAT, VATInc As Boolean
        Dim Amount As Decimal
        query = " SELECT  Description, Amount, RecordID, ISNULL(VAT,0) AS VAT, ISNULL(VATInc,0) AS VATInc FROM  tblBS_Charges " & _
                " WHERE   RecordID ='" & ID & "' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            RecordID = SQL.SQLDR("RecordID")
            Description = SQL.SQLDR("Description")
            Amount = SQL.SQLDR("Amount")
            VAT = SQL.SQLDR("VAT")
            VATInc = SQL.SQLDR("VATInc")
            dgvItemList.Rows.Add(New String() {RecordID, _
                                         Description, _
                                              Format(Amount, "#,###,###,###.00").ToString, _
                                                Format(0, "#,###,###,###.00").ToString, _
                                                Format(0, "#,###,###,###.00").ToString, _
                                               "", _
                                                Format(0, "#,###,###,###.00").ToString, _
                                               "", _
                                               VAT,
                                               VATInc,
                                                Format(0, "#,###,###,###.00").ToString})
        End While
        ComputeTotal()
    End Sub


    Private Sub SaveBS()
        Dim SQL As New SQLControl
        Try
            SQL.BeginTransaction()
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " &
                        " tblBS  (TransID, BS_No, BranchCode, BusinessCode, VCECode, DateBS, DateDue, Remarks, DebitAccount, " &
                        "         GrossAmount, VATAmount, EWTAmount, NetAmount, VATable, VATInclusive,  PayPeriod, LC_ID, CostCenter,  WhoCreated, Status) " &
                        " VALUES (@TransID, @BS_No, @BranchCode, @BusinessCode, @VCECode,  @DateBS, @DateDue, @Remarks, @DebitAccount, " &
                        "         @GrossAmount,  @VATAmount, @EWTAmount, @NetAmount, @VATable, @VATInclusive, @PayPeriod, @LC_ID, @CostCenter, @WhoCreated, @Status) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@BS_No", BSNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateBS", dtpDocDate.Value.Date)
            SQL.AddParam("@DateDue", dtpDue.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@DebitAccount", IIf(cbDefaultAcc.SelectedIndex = -1, "", cbDefaultAcc.SelectedItem))
            SQL.AddParam("@GrossAmount", CDec(txtGross.Text))
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@EWTAmount", CDec(txtEWTAmount.Text))
            SQL.AddParam("@NetAmount", CDec(txtNet.Text))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInclusive", chkVATInc.Checked)
            SQL.AddParam("@PayPeriod", txtPayPeriod.Text)
            SQL.AddParam("@LC_ID", LC_ID)
            If cbCostCenter.SelectedItem <> Nothing Then
                SQL.AddParam("@CostCenter", cbCostCenter.SelectedItem)
            Else
                SQL.AddParam("@CostCenter", "")
            End If
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chAmount.Index).Value Is Nothing AndAlso Not row.Cells(chDesc.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " &
                                " tblBS_Details(TransId, Description, GrossAmount, VATable, VATInc, VATAmount, " &
                                "                NetAmount, LineNum, DescRecordID, WhoCreated, EWTAmount, ATCCode, CostCenter) " &
                                " VALUES(@TransId,  @Description, @GrossAmount, @VATable, @VATInc, @VATAmount, " &
                                "        @NetAmount, @LineNum, @DescRecordID, @WhoCreated, @EWTAmount, @ATCCode, @CostCenter) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@Description", row.Cells(chDesc.Index).Value.ToString)
                    SQL.AddParam("@GrossAmount", CDec(row.Cells(chAmount.Index).Value))
                    SQL.AddParam("@VATable", row.Cells(dgcVAT.Index).Value)
                    SQL.AddParam("@VATAmount", CDec(row.Cells(chVATAmount.Index).Value))
                    SQL.AddParam("@VATInc", row.Cells(dgcVATInc.Index).Value)
                    SQL.AddParam("@NetAmount", CDec(row.Cells(chTotal.Index).Value))
                    SQL.AddParam("@DescRecordID", row.Cells(chChargeID.Index).Value)
                    If row.Cells(chEWTAmount.Index).Value <> Nothing AndAlso IsNumeric(row.Cells(chEWTAmount.Index).Value) Then
                        SQL.AddParam("@EWTAmount", CDec(row.Cells(chEWTAmount.Index).Value))
                    Else
                        SQL.AddParam("@EWTAmount", 0)
                    End If
                    If row.Cells(chATCCode.Index).Value <> Nothing AndAlso row.Cells(chATCCode.Index).Value <> "" Then
                        SQL.AddParam("@ATCCode", row.Cells(chATCCode.Index).Value.ToString)
                    Else
                        SQL.AddParam("@ATCCode", "")
                    End If
                    'If row.Cells(chCost_Center.Index).Value <> Nothing AndAlso row.Cells(chCost_Center.Index).Value <> "" Then
                    '    SQL.AddParam("@CostCenter", row.Cells(chCost_Center.Index).Value.ToString)
                    'Else
                    '    SQL.AddParam("@CostCenter", "")
                    'End If
                    If cbCostCenter.SelectedItem <> Nothing Then
                        SQL.AddParam("@CostCenter", cbCostCenter.SelectedItem)
                    Else
                        SQL.AddParam("@CostCenter", row.Cells(chCost_Center.Index).Value.ToString)
                    End If
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
            insertSQL = " INSERT INTO " &
                        " tblSJ (TransID, SJ_No, VCECode, BranchCode, BusinessCode, DateSJ, TotalAmount, Remarks, TransAuto, WhoCreated, Terms, DueDate, BSID, Status, CostCenter) " &
                        " VALUES(@TransID, @SJ_No, @VCECode, @BranchCode, @BusinessCode, @DateSJ, @TotalAmount, @Remarks, @TransAuto, @WhoCreated,  @Terms, @DueDate, @BSID, @Status, @CostCenter)"
            SQL.FlushParams()
            SQL.AddParam("@TransID", SJID)
            SQL.AddParam("@SJ_No", SJNo)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateSJ", dtpDocDate.Value.Date)
            SQL.AddParam("@DueDate", dtpDue.Value.Date)
            SQL.AddParam("@TotalAmount", CDec(txtGross.Text))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@TransAuto", TransAuto)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.AddParam("@Terms", cbTerms.Text)
            SQL.AddParam("@BSID", TransID)
            If cbCostCenter.SelectedItem <> Nothing Then
                SQL.AddParam("@CostCenter", cbCostCenter.SelectedItem)
            Else
                SQL.AddParam("@CostCenter", "")
            End If
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Active")
            SQL.ExecNonQuery(insertSQL)

            JETransID = GenerateTransID("JE_No", "tblJE_Header")
            insertSQL = " INSERT INTO " &
                        " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, WhoCreated, Status) " &
                        " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @WhoCreated, @Status)"
            SQL.FlushParams()
            SQL.AddParam("@JE_No", JETransID)
            SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
            SQL.AddParam("@RefType", "SJ")
            SQL.AddParam("@RefTransID", SJID)
            SQL.AddParam("@Book", "Sales")
            SQL.AddParam("@TotalDBCR", CDec(txtGross.Text))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Saved")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            line = 1
            For Each item As DataGridViewRow In dgvEntry.Rows
                If item.Cells(chAccntCode.Index).Value <> Nothing Then
                    insertSQL = " INSERT INTO " &
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, CostCenter, LineNumber) " &
                                " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @CostCenter, @LineNumber)"
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransID)
                    SQL.AddParam("@AccntCode", item.Cells(chAccntCode.Index).Value.ToString)
                    If item.Cells(chVCECode.Index).Value <> Nothing AndAlso item.Cells(chVCECode.Index).Value <> "" Then
                        SQL.AddParam("@VCECode", item.Cells(chVCECode.Index).Value.ToString)
                    Else
                        SQL.AddParam("@VCECode", txtVCECode.Text)
                    End If
                    If item.Cells(chDebit.Index).Value <> Nothing AndAlso IsNumeric(item.Cells(chDebit.Index).Value) Then
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
                    'If item.Cells(chCostDescription.Index).Value <> Nothing AndAlso item.Cells(chCostDescription.Index).Value <> "" Then
                    '    SQL.AddParam("@CostCenter", item.Cells(chCostDescription.Index).Value.ToString)
                    'Else
                    '    SQL.AddParam("@CostCenter", "")
                    'End If
                    If cbCostCenter.SelectedItem <> Nothing Then
                        SQL.AddParam("@CostCenter", cbCostCenter.SelectedItem)
                    ElseIf item.Cells(chCostDescription.Index).Value <> Nothing Then
                        SQL.AddParam("@CostCenter", item.Cells(chCostDescription.Index).Value)
                    Else
                        SQL.AddParam("@CostCenter", "")
                    End If
                    SQL.AddParam("@LineNumber", line)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "BS_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub UpDateBS()
        Dim SQL As New SQLControl
        SQL.BeginTransaction()
        Try
            activityStatus = True
            Dim insertSQL, updateSQL, deleteSQL As String
            insertSQL = " UPDATE tblBS " &
                        " SET    BS_No = @BS_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, VCECode = @VCECode, DateBS=@DateBS, " &
                        "        DateDue = @DateDue, Remarks = @Remarks, DebitAccount = @DebitAccount, " &
                        "        GrossAmount = @GrossAmount, VATAmount = @VATAmount,  EWTAmount = @EWTAmount, NetAmount = @NetAmount, " &
                        "        VATable = @VATable, VATInclusive = @VATInclusive, WhoModified = @WhoModified " &
                        " WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@BS_No", BSNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateBS", dtpDocDate.Value.Date)
            SQL.AddParam("@DateDue", dtpDue.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@DebitAccount", IIf(cbDefaultAcc.SelectedIndex = -1, "", cbDefaultAcc.SelectedItem))
            SQL.AddParam("@GrossAmount", CDec(txtGross.Text))
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@EWTAmount", CDec(txtEWTAmount.Text))
            SQL.AddParam("@NetAmount", CDec(txtNet.Text))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInclusive", chkVATInc.Checked)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(insertSQL)

            deleteSQL = " DELETE FROM tblBS_Details WHERE TransID = '" & TransID & "' "
            SQL.ExecNonQuery(deleteSQL)

            Dim line As Integer = 1
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chAmount.Index).Value Is Nothing AndAlso Not row.Cells(chDesc.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " &
                                " tblBS_Details(TransId, Description, GrossAmount, VATable, VATInc, VATAmount, " &
                                "                NetAmount, LineNum, DescRecordID, WhoCreated, EWTAmount, ATCCode) " &
                                " VALUES(@TransId,  @Description, @GrossAmount, @VATable, @VATInc, @VATAmount, " &
                                "        @NetAmount, @LineNum, @DescRecordID, @WhoCreated, @EWTAmount, @ATCCode) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@Description", row.Cells(chDesc.Index).Value.ToString)
                    SQL.AddParam("@GrossAmount", CDec(row.Cells(chAmount.Index).Value))
                    SQL.AddParam("@VATable", row.Cells(dgcVAT.Index).Value)
                    SQL.AddParam("@VATAmount", CDec(row.Cells(chVATAmount.Index).Value))
                    SQL.AddParam("@VATInc", row.Cells(dgcVATInc.Index).Value)
                    SQL.AddParam("@NetAmount", CDec(row.Cells(chTotal.Index).Value))
                    SQL.AddParam("@DescRecordID", row.Cells(chChargeID.Index).Value)
                    If row.Cells(chEWTAmount.Index).Value <> Nothing AndAlso IsNumeric(row.Cells(chEWTAmount.Index).Value) Then
                        SQL.AddParam("@EWTAmount", CDec(row.Cells(chEWTAmount.Index).Value))
                    Else
                        SQL.AddParam("@EWTAmount", 0)
                    End If
                    If row.Cells(chATCCode.Index).Value <> Nothing AndAlso row.Cells(chATCCode.Index).Value <> "" Then
                        SQL.AddParam("@ATCCode", row.Cells(chATCCode.Index).Value.ToString)
                    Else
                        SQL.AddParam("@ATCCode", "")
                    End If
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next

            Dim SJID As Integer
            SJID = LoadSJID(TransID)
            Dim SJNo As String
            SJNo = LoadSJNo(TransID)
            insertSQL = " UPDATE tblSJ " & _
                        " SET    SJ_No = @SJ_No, VCECode  = @VCECode, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " & _
                        "        DateSJ = @DateSJ, TotalAmount = @TotalAmount, Remarks = @Remarks, TransAuto = @TransAuto, WhoModified = @WhoModified, " & _
                        "        Terms = @Terms, DueDate= @DueDate, BSRef = @BSRef, DateModified = GETDATE() " & _
                        " WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", SJID)
            SQL.AddParam("@SJ_No", SJNo)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateSJ", dtpDocDate.Value.Date)
            SQL.AddParam("@DueDate", dtpDue.Value.Date)
            SQL.AddParam("@TotalAmount", CDec(txtGross.Text))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@TransAuto", TransAuto)
            SQL.AddParam("@WhoModified", UserID)
            SQL.AddParam("@Terms", cbTerms.Text)
            SQL.AddParam("@BSRef", txtTransNum.Text)
            SQL.ExecNonQuery(insertSQL)

            JETransID = LoadJE("SJ", SJID)

            If JETransID = 0 Then
                insertSQL = " INSERT INTO " & _
                            " tblJE_Header (AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, WhoCreated) " & _
                            " VALUES(@AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @WhoCreated)"
                SQL.FlushParams()
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "SJ")
                SQL.AddParam("@RefTransID", SJID)
                SQL.AddParam("@Book", "Sales")
                SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@WhoCreated", UserID)
                SQL.ExecNonQuery(insertSQL)

                JETransID = LoadJE("SJ", TransID)
            Else
                updateSQL = " UPDATE tblJE_Header " & _
                            " SET    AppDate = @AppDate, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " & _
                            "        RefType = @RefType, RefTransID = @RefTransID, Book = @Book, TotalDBCR = @TotalDBCR, " & _
                            "        Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE() " & _
                            " WHERE  JE_No = @JE_No "
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransID)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "SJ")
                SQL.AddParam("@RefTransID", SJID)
                SQL.AddParam("@Book", "Sales")
                SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
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
                    If item.Cells(chDebit.Index).Value <> Nothing AndAlso IsNumeric(item.Cells(chDebit.Index).Value) Then
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
            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "BS_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
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
        Dim dataEntry As New DataTable
        dgvEntry.Rows.Clear()
        Dim query As String


        dgvEntry.Rows.Add({GetAccntCode(cbDefaultAcc.SelectedItem), cbDefaultAcc.SelectedItem, CDec(txtNet.Text).ToString("N2"), "", "", "", "", "BS:" & txtTransNum.Text})

        If txtVAT.Text <> "0.00" Then
            dgvEntry.Rows.Add({TAX_DOV, GetAccntTitle(TAX_DOV), "", CDec(txtVAT.Text).ToString, "", "", "", "BS:" & txtTransNum.Text})
        End If

        If txtEWTAmount.Text <> "0.00" Then
            dgvEntry.Rows.Add({TAX_EWT, GetAccntTitle(TAX_EWT), CDec(txtEWTAmount.Text).ToString, "", "", "", "", "BS:" & txtTransNum.Text})
        End If
        Dim ref As String = ""
        If LC_No <> "" Then
            ref = "LC:" & LC_No
        End If
        If BM_No <> "" Then
            ref = "BM:" & BM_No
        End If

        For Each row As DataGridViewRow In dgvItemList.Rows
            If row.Cells(chDesc.Index).Value <> Nothing AndAlso row.Cells(chTotal.Index).Value <> 0 Then
                query = " SELECT DefaultAccount, AccountTitle " &
                        " FROM   viewBS_Charges INNER JOIN tblCOA_Master " &
                        " ON     viewBS_Charges.DefaultAccount = tblCOA_Master.AccountCode " &
                        " WHERE  ID ='" & row.Cells(chChargeID.Index).Value & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read() Then
                    Dim SubRef As String = ref
                    If IsNumeric(row.Cells(chSubRef.Index).Value) Then
                        If row.Cells(chSubRef.Index).Value <> 0 Then SubRef = SubRef & "-" & row.Cells(chSubRef.Index).Value
                    End If

                    If row.Cells(chTotal.Index).Value > 0 Then
                        dgvEntry.Rows.Add({SQL.SQLDR("DefaultAccount").ToString, SQL.SQLDR("AccountTitle").ToString, "", (CDec(row.Cells(chTotal.Index).Value) - CDec(row.Cells(chVATAmount.Index).Value)).ToString("N2"), row.Cells(chDesc.Index).Value, txtVCECode.Text, txtVCEName.Text, SubRef,
                        IIf(cbCostCenter.SelectedItem <> "", cbCostCenter.SelectedItem, row.Cells(chCost_Center.Index).Value)})
                    Else
                        dgvEntry.Rows.Add({SQL.SQLDR("DefaultAccount").ToString, SQL.SQLDR("AccountTitle").ToString, ((CDec(row.Cells(chTotal.Index).Value) - CDec(row.Cells(chVATAmount.Index).Value)) * -1).ToString("N2"), row.Cells(chDesc.Index).Value, txtVCECode.Text, txtVCEName.Text, SubRef,
                        IIf(cbCostCenter.SelectedItem <> "", cbCostCenter.SelectedItem, row.Cells(chCost_Center.Index).Value)})
                    End If
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
        If Not AllowAccess("BS_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("BS")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadBS(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("BS_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            BSNo = ""

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

            dgvEntry.Rows.Add()
            If cbDefaultAcc.Items.Count > 0 Then
                cbDefaultAcc.SelectedIndex = 0
            End If
            dgvEntry.Rows(0).ReadOnly = True
            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
        End If
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("BS_EDIT") Then
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
                        deleteSQL = " UPDATE  tblSI SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        BSNo = txtTransNum.Text
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

                        BSNo = txtTransNum.Text
                        LoadBS(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "BS_No", BSNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("BSStorage", TransID)
        f.Dispose()
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If BSNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblBS  WHERE BS_No < '" & BSNo & "' ORDER BY BS_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadBS(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If BSNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblBS  WHERE BS_No > '" & BSNo & "' ORDER BY BS_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadBS(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If BSNo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadBS(TransID)
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
                f.Dispose()
                LoadCurrency()
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
    Private Sub dgvItemList_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellEndEdit
        Try
            Dim desc As String
            Dim rowIndex As Integer = dgvItemList.CurrentCell.RowIndex
            Dim colindex As Integer = dgvItemList.CurrentCell.ColumnIndex
            Select Case colindex

                Case chDesc.Index
                    If dgvItemList.Item(chDesc.Index, e.RowIndex).Value <> "" Then
                        desc = dgvItemList.Item(chDesc.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("BS_Type", desc)
                        If f.TransID <> "" Then
                            desc = f.TransID
                            LoadItem(desc)
                        End If
                        dgvItemList.Rows.RemoveAt(e.RowIndex)
                        f.Dispose()
                    End If
                Case chAmount.Index
                    If IsNumeric(dgvItemList.Item(chAmount.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex)
                        ComputeTotal()
                    End If
                Case chCost_Center.Index
                    cbCostCenter.SelectedItem = ""

                    If dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value <> Nothing Then
                        Dim f1 As New frmCC_PC_Search
                        f1.Text = "Search of Cost Center"
                        f1.Type = "Cost Center"
                        f1.txtSearch.Text = dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                        f1.FilterText = dgvItemList.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                        f1.ShowDialog()
                        If f1.Code <> "" And f1.Description <> "" Then
                            'dgvItemList.Item(chCostID.Index, e.RowIndex).Value = f1.Code
                            dgvItemList.Item(chCost_Center.Index, e.RowIndex).Value = f1.Description
                        End If
                        f1.Dispose()
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
                If Val(dgvItemList.Item(chAmount.Index, i).Value) <> 0 Then
                    If IsNumeric(dgvItemList.Item(chAmount.Index, i).Value) Then
                        a = a + Double.Parse(dgvItemList.Item(chAmount.Index, i).Value).ToString
                    End If
                End If
            Next
            txtGross.Text = a.ToString("N2")
            ' COMPUTE VAT
            For i As Integer = 0 To dgvItemList.Rows.Count - 1
                If Val(dgvItemList.Item(chVATAmount.Index, i).Value) <> 0 Then
                    If IsNumeric(dgvItemList.Item(chVATAmount.Index, i).Value) Then
                        b = b + Double.Parse(dgvItemList.Item(chVATAmount.Index, i).Value).ToString
                    End If
                End If
            Next
            txtVAT.Text = b.ToString("N2")

            ' COMPUTE VAT
            For i As Integer = 0 To dgvItemList.Rows.Count - 1
                If Val(dgvItemList.Item(chEWTAmount.Index, i).Value) <> 0 Then
                    If IsNumeric(dgvItemList.Item(chEWTAmount.Index, i).Value) Then
                        c = c + Double.Parse(dgvItemList.Item(chEWTAmount.Index, i).Value).ToString
                    End If
                End If
            Next
            txtEWTAmount.Text = c.ToString("N2")

            ' COMPUTE NET
            For i As Integer = 0 To dgvItemList.Rows.Count - 1
                If Val(dgvItemList.Item(chTotal.Index, i).Value) <> 0 Then
                    If IsNumeric(dgvItemList.Item(chTotal.Index, i).Value) Then
                        d = d + Double.Parse(dgvItemList.Item(chTotal.Index, i).Value).ToString
                    End If
                End If
            Next
            txtNet.Text = d.ToString("N2")

            '    ComputeVaTEWTAmount()
        End If


    End Sub

    'Private Sub Recompute(ByVal RowID As Integer, ByVal ColID As Integer)
    '    Dim Amount, VAT, Total As Decimal
    '    If RowID <> -1 Then
    '        If IsNumeric(dgvItemList.Item(chAmount.Index, RowID).Value) Then
    '            Amount = CDec(dgvItemList.Item(chAmount.Index, RowID).Value)

    '            Total = Amount + VAT
    '            dgvItemList.Item(chAmount.Index, RowID).Value = Format(Amount, "#,###,###,###.00").ToString()
    '            dgvItemList.Item(chTotal.Index, RowID).Value = Format(Total, "#,###,###,###.00").ToString()
    '            ComputeTotal()
    '        End If
    '    End If

    'End Sub

    Private Sub Recompute(ByVal RowID As Integer, ByVal ColID As Integer)
        Dim gross, VAT, discount, net, baseVAT, EWT As Decimal
        If RowID <> -1 Then
            ' GET GROSS AMOUNT (VAT INCLUSIVE)
            If IsNumeric(dgvItemList.Item(chAmount.Index, RowID).Value) Then
                gross = CDec(dgvItemList.Item(chAmount.Index, RowID).Value)
            Else
                gross = 0
            End If
            baseVAT = gross


            ' COMPUTE VAT IF VATABLE
            If ColID = dgcVAT.Index Then
                If dgvItemList.Item(dgcVAT.Index, RowID).Value = True Then
                    dgvItemList.Item(dgcVAT.Index, RowID).Value = False
                    dgvItemList.Item(dgcVATInc.Index, RowID).Value = False
                    VAT = 0
                    dgvItemList.Item(dgcVATInc.Index, RowID).ReadOnly = True
                Else
                    dgvItemList.Item(dgcVAT.Index, RowID).Value = True
                    dgvItemList.Item(dgcVATInc.Index, RowID).ReadOnly = False
                    If dgvItemList.Item(dgcVATInc.Index, RowID).Value = False Then
                        VAT = CDec((gross - discount) * 0.12).ToString("N2")
                    Else
                        VAT = (gross - discount) / 1.12 * 0.12
                    End If

                End If
            ElseIf ColID = dgcVATInc.Index Then
                If dgvItemList.Item(dgcVAT.Index, RowID).Value = False Then
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
                If dgvItemList.Item(dgcVAT.Index, RowID).Value = False Then
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

            'EWT
            If IsNumeric(dgvItemList.Item(chEWTAmount.Index, RowID).Value) Then
                EWT = CDec(dgvItemList.Item(chEWTAmount.Index, RowID).Value)
            Else
                EWT = 0
            End If

            If dgvItemList.Item(dgcVATInc.Index, RowID).Value = False Then

                net = gross - discount + VAT - EWT
            Else
                net = gross - discount - EWT
            End If

            dgvItemList.Item(chAmount.Index, RowID).Value = Format(gross, "#,###,###,###.00").ToString()
            dgvItemList.Item(chVATAmount.Index, RowID).Value = Format(VAT, "#,###,###,###.00").ToString()
            dgvItemList.Item(chTotal.Index, RowID).Value = Format(net, "#,###,###,###.00").ToString()
            ComputeTotal()

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
            ElseIf dgvItemList.Rows.Count = 1 Then
                MsgBox("Please enter an item/services to purchase!", MsgBoxStyle.Exclamation)
            ElseIf txtTransNum.Text = "" Then
                MsgBox("Please enter BS No!", MsgBoxStyle.Exclamation)
            ElseIf cbDefaultAcc.SelectedIndex = -1 Then
                MsgBox("Please enter Default Debit AR Account!")
            ElseIf TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID(ColumnID, DBTable)


                    If TransAuto Then
                        BSNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    Else
                        BSNo = txtTransNum.Text
                    End If
                    txtTransNum.Text = BSNo

                    GenerateEntry()
                    SaveBS()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    BSNo = txtTransNum.Text
                    LoadBS(TransID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    BSNo = txtTransNum.Text
                    GenerateEntry()
                    UpDateBS()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    BSNo = txtTransNum.Text
                    LoadBS(TransID)
                End If
            End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub cbDefaultAcc_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbDefaultAcc.KeyPress
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

    Private Sub cbDefaultAcc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDefaultAcc.SelectedIndexChanged
        If disableEvent = False Then
            If cbDefaultAcc.SelectedIndex <> -1 Then
                dgvEntry.Item(chAccntTitle.Index, 0).Value = cbDefaultAcc.SelectedItem
                dgvEntry.Item(chAccntCode.Index, 0).Value = GetAccntCode(cbDefaultAcc.SelectedItem)
                TotalDBCR()
            End If
        End If
    End Sub

    Private Function Tru() As Boolean
        Throw New NotImplementedException
    End Function

    Private Sub tsbCopyLC_Click(sender As Object, e As EventArgs) Handles tsbCopyLC.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("LC-BS")
        LC_ID = f.transID
        LoadLC(LC_ID)
        f.Dispose()
    End Sub
    Private Sub LoadLC(ByVal LC_ID As String)
        Dim query As String
        query = " SELECT  TransID, LC_No, DateLC AS Date, tblLC.VCECode, VCEName, RatePerMonth, VATInc  " &
                " FROM    tblLC INNER JOIN viewVCE_Master " &
                " ON      tblLC.VCECode = viewVCE_Master.VCECode " &
                " WHERE   tblLC.Status ='Active' AND TransID ='" & LC_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            LC_ID = SQL.SQLDR("TransID")
            LC_No = SQL.SQLDR("LC_No")
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
            LoadLMCharges(LC_ID)
            ComputeTotal()
            GenerateEntry()
        End If
    End Sub

    Private Sub LoadLMCharges(LC_ID As Integer)
        dgvEntry.Rows.Clear()
        dgvItemList.Rows.Clear()
        Dim f As New frmLM_List
        f.ShowDialog(LC_ID)
        If f.isCancel = False Then
            dgvEntry.Rows.Add()
            For Each item As ListViewItem In f.lvARList.Items
                If item.Checked = True Then
                    LoadLM_Charge(LC_ID, item.Text)
                End If
            Next

        End If
        f.Dispose()
    End Sub
    Private Function GetChargeID(Type As String) As String
        Dim SQL As New SQLControl
        Dim query As String
        query = " SELECT ID FROM viewBS_Charges WHERE Description = @Type "
        SQL.FlushParams()
        SQL.AddParam("@Type", Type)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("ID")
        Else
            Return ""
        End If
    End Function
    Private Sub LoadLM_Charge(LC_ID As Integer, ChargeID As String)

        Dim query As String
        Dim particulars As String = ""
        query = " SELECT Type, LC_No, PeriodFrom, PeriodTo, Amount, Account, Months, VATAble, VATInc, PaymentNo FROM viewLM_Charges WHERE TransID = @TransID AND ChargeID = @ChargeID "
        SQL.FlushParams()
        SQL.AddParam("@TransID", LC_ID)
        SQL.AddParam("@ChargeID", ChargeID)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            If SQL.SQLDR("Type") = "Rental" Then
                particulars = SQL.SQLDR("Type") & " for the Period " & GetPeriod(SQL.SQLDR("PeriodFrom").ToString, SQL.SQLDR("PeriodTo").ToString)
            ElseIf SQL.SQLDR("Type") = "Advance Rental" Then
                particulars = SQL.SQLDR("Months").ToString & " Months " & SQL.SQLDR("Type") & " for the Period " & GetPeriod(SQL.SQLDR("PeriodFrom").ToString, SQL.SQLDR("PeriodTo").ToString)
            ElseIf SQL.SQLDR("Type") = "Rental Deposit" Then
                particulars = SQL.SQLDR("Months").ToString & " Months " & SQL.SQLDR("Type")
            Else
                particulars = SQL.SQLDR("Type")
            End If

            Dim VAtAmount As Decimal = 0
            Dim Gross As Decimal = CDec(SQL.SQLDR("Amount"))
            Dim Total As Decimal = 0
            Dim Amount As Decimal = 0
            If SQL.SQLDR("VATAble") Then
                If SQL.SQLDR("VATInc") Then
                    VAtAmount = (Gross / 1.12) * 0.12
                    Total = Gross
                    Amount = Total - VAtAmount
                Else
                    VAtAmount = Gross * 0.12
                    Total = Gross + VAtAmount
                    Amount = Gross
                End If
            Else
                Total = Gross
                Amount = Total
            End If
            dgvItemList.Rows.Add({GetChargeID(SQL.SQLDR("Type").ToString), particulars, Gross.ToString("N2"), VAtAmount.ToString("N2"), "0.00", "", Total.ToString("N2"), "", SQL.SQLDR("VATAble"), SQL.SQLDR("VATInc"), SQL.SQLDR("PaymentNo").ToString})
            '      dgvEntry.Rows.Add({SQL.SQLDR("Account").ToString, GetAccntTitle(SQL.SQLDR("Account").ToString), "", Amount.ToString("N2"), particulars, txtVCECode.Text, txtVCEName.Text, "LC:" & SQL.SQLDR("LC_No").ToString})
        End If
    End Sub


    Private Function GetPeriod(BegDate As Date, EndDate As Date) As String
        Return BegDate.ToString("MMM d - ") & EndDate.ToString("MMM d, yyyy")
    End Function


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

    Private Sub txtPayPeriod_TextChanged(sender As Object, e As EventArgs) Handles txtPayPeriod.TextChanged

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

    Private Sub FromBookingToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromBookingToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "For Billing"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("BM-BS")
        If f.transID <> "" Then
            BM_ID = f.transID
            LoadBM(BM_ID)
        End If

        f.Dispose()
    End Sub

    Private Sub LoadBM(ByVal BM_ID As String)
        Dim query As String
        query = " SELECT  TransID, BM_No, DateBM AS Date, tblBM.ClientCode, VCEName, Location, TotalAmount  " &
                " FROM    tblBM LEFT JOIN viewVCE_Master " &
                " ON      tblBM.ClientCode = viewVCE_Master.VCECode " &
                " WHERE   TransID ='" & BM_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            BM_ID = SQL.SQLDR("TransID")
            BM_No = SQL.SQLDR("BM_No")
            txtVCECode.Text = SQL.SQLDR("ClientCode").ToString
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
            LoadBMCharges(BM_ID)
            ComputeTotal()
            GenerateEntry()
        End If
    End Sub

    Private Sub LoadBMCharges(BM_ID As Integer)

        Dim query As String
        Dim particulars As String = ""
        query = " SELECT Description, Rate AS Amount, VATable, VATInc FROM viewBM_BSCharges WHERE TransID = @TransID  "
        SQL.FlushParams()
        SQL.AddParam("@TransID", BM_ID)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            particulars = " Booking Rate for " & SQL.SQLDR("Description").ToString
            Dim VAtAmount As Decimal = 0
            Dim Gross As Decimal = CDec(SQL.SQLDR("Amount"))
            Dim Total As Decimal = 0
            Dim Amount As Decimal = 0
            If SQL.SQLDR("VATAble") Then
                If SQL.SQLDR("VATInc") Then
                    VAtAmount = (Gross / 1.12) * 0.12
                    Total = Gross
                    Amount = Total - VAtAmount
                Else
                    VAtAmount = Gross * 0.12
                    Total = Gross + VAtAmount
                    Amount = Gross
                End If
            Else
                Total = Gross
                Amount = Total
            End If
            dgvItemList.Rows.Add({GetChargeID("Booking Fee"), particulars, Gross.ToString("N2"), VAtAmount.ToString("N2"), "0.00", "", Total.ToString("N2"), "", SQL.SQLDR("VATAble"), SQL.SQLDR("VATInc"), ""})
        End If
    End Sub

    Private Sub dgvItemList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellContentClick
        If e.ColumnIndex = chTaxMaintenance.Index AndAlso dgvItemList.Item(chAmount.Index, e.RowIndex).Value <> "" Then
            Dim f As New frmTaxComputation
            Dim VATamount As Decimal

            VATamount = CDec(dgvItemList.Item(chAmount.Index, e.RowIndex).Value).ToString("N2")
            f.ShowDialog(VATamount, "BS", "", "", dgvItemList.Item(dgcVAT.Index, e.RowIndex).Value, dgvItemList.Item(dgcVATInc.Index, e.RowIndex).Value)
            If IsNothing(dgvItemList.Item(chAmount.Index, e.RowIndex).Value) Then
                dgvItemList.Item(chAmount.Index, e.RowIndex).Value = CDec(f.GrossAmount).ToString("N2")
            End If

            If f.EWTAmount <> 0 Then
                dgvItemList.Item(chEWTAmount.Index, e.RowIndex).Value = CDec(f.EWTAmount).ToString("N2")
                dgvItemList.Item(chATCCode.Index, e.RowIndex).Value = f.ATCCode
            Else
                dgvItemList.Item(chEWTAmount.Index, e.RowIndex).Value = CDec("0.00").ToString("N2")
                dgvItemList.Item(chATCCode.Index, e.RowIndex).Value = ""
            End If
            dgvItemList.Item(dgcVAT.Index, e.RowIndex).Value = f.vat
            dgvItemList.Item(dgcVATInc.Index, e.RowIndex).Value = f.VATInc
            Recompute(dgvItemList.SelectedCells(0).RowIndex, dgvItemList.SelectedCells(0).ColumnIndex)
            ComputeTotal()
        End If
    End Sub

    Private Sub dgvItemList_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dgvItemList.CurrentCellDirtyStateChanged
        If dgvItemList.SelectedCells.Count > 0 AndAlso (dgvItemList.SelectedCells(0).ColumnIndex = dgcVAT.Index OrElse dgvItemList.SelectedCells(0).ColumnIndex = dgcVATInc.Index) Then
            If dgvItemList.SelectedCells(0).RowIndex <> -1 Then
                Recompute(dgvItemList.SelectedCells(0).RowIndex, dgvItemList.SelectedCells(0).ColumnIndex)
                dgvItemList.SelectedCells(0).Selected = False
                dgvItemList.EndEdit()
            End If
        End If
    End Sub

    Private Sub cbCostCenter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCostCenter.SelectedIndexChanged
        Dim columnName As String = "chCost_Center"
        If dgvItemList.Columns.Contains(columnName) Then
            Dim columnIndex As Integer = dgvItemList.Columns(columnName).Index

            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.IsNewRow Then
                    row.Cells(columnIndex).Value = String.Empty
                End If
            Next
        End If
    End Sub
End Class