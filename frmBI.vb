Public Class frmBI
    Dim TransID, RefID, JETransID As String
    Dim BINo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "BI"
    Dim ColumnPK As String = "BI_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblBI"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim SO_ID As Integer
    Dim accntDR, accntCR, accntVAT, accntDiscount As String
    Dim RecordID As Integer


    Public Overloads Function ShowDialog(ByVal docID As String) As Boolean
        TransID = docID
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmBI_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            LoadChartOfAccount()
            If TransID <> "" Then
                LoadBI(TransID)
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
        dtpDocDate.Enabled = Value
        txtSORef.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Private Sub LoadChartOfAccount()
        Dim query As String
        query = " SELECT  AccountCode, AccountTitle " & _
                " FROM    tblCOA_Master " & _
                " WHERE   AccountNature = 'Debit' " & _
                " ORDER BY AccountTitle "
        SQL.ReadQuery(query)
        cbDefaultAcc.Items.Clear()
        While SQL.SQLDR.Read
            cbDefaultAcc.Items.Add(SQL.SQLDR("AccountTitle"))
        End While
    End Sub

    Private Sub LoadBI(ByVal TransID As String)
        Dim query As String
        query = " SELECT   TransID, BI_No, VCECode, DateBI, Remarks, " & _
                "          ISNULL(GrossAmount,0) AS GrossAmount, ISNULL(VATAmount,0) AS VATAmount, ISNULL(Discount,0) AS Discount, ISNULL(NetAmount,0) AS NetAmount, " & _
                "          ISNULL(VATable,1) AS VATable, ISNULL(VATInclusive,1) AS VATInclusive, " & _
                "          Status, ISNULL(SO_Ref,0) AS SO_Ref, DebitAccnt " & _
                " FROM     tblBI " & _
                " WHERE    TransId = '" & TransID & "' " & _
                " ORDER BY TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            TransID = SQL.SQLDR("TransID").ToString
            BINo = SQL.SQLDR("BI_No").ToString
            txtTransNum.Text = BINo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            dtpDocDate.Text = SQL.SQLDR("DateBI").ToString
            txtGross.Text = CDec(SQL.SQLDR("GrossAmount").ToString).ToString("N2")
            txtVAT.Text = CDec(SQL.SQLDR("VATAmount").ToString).ToString("N2")
            txtDiscount.Text = CDec(SQL.SQLDR("Discount").ToString).ToString("N2")
            txtNet.Text = CDec(SQL.SQLDR("NetAmount").ToString).ToString("N2")
            chkVAT.Checked = SQL.SQLDR("VATable")
            chkVATInc.Checked = SQL.SQLDR("VATInclusive")
            cbDefaultAcc.SelectedItem = SQL.SQLDR("DebitAccnt").ToString
            SO_ID = SQL.SQLDR("SO_Ref").ToString
            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            txtSORef.Text = LoadSONo(SO_ID)
            LoadBIDetails(TransID)
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
            query = " SELECT JE_No, VCECode, VCEName, AccntCode, AccntTitle, Particulars, " & _
                    "        ISNULL(Debit,0) AS Debit, ISNULL(Credit,0) AS Credit " & _
                    " FROM   View_GL  " & _
                    " WHERE  RefType ='BI' AND RefTransID ='" & TransID & "' "
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

    Protected Sub LoadBIDetails(ByVal TransID As String)
        dgvItemList.Rows.Clear()
        Dim query As String
        query = " SELECT     DescRecordID, Description, " & _
                "           ISNULL(GrossAmount,0) AS GrossAmount, ISNULL(DiscountRate,0) AS DiscountRate, ISNULL(Discount,0) AS Discount, " & _
                "           ISNULL(VATAmount,0) AS VATAmount, ISNULL(NetAmount,0) AS NetAmount, ISNULL(VATable,1) AS VATable " & _
                " FROM      tblBI_Details " & _
                " WHERE     tblBI_Details.TransId = " & TransID & " " & _
                " ORDER BY  LineNum "
        dgvItemList.Rows.Clear()
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvItemList.Rows.Add(SQL.SQLDR("DescRecordID").ToString, _
                                SQL.SQLDR("Description").ToString, _
                                 CDec(SQL.SQLDR("GrossAmount")).ToString("N2"), _
                                 IIf((SQL.SQLDR("DiscountRate") <> 0), SQL.SQLDR("DiscountRate"), ""), _
                                 CDec(SQL.SQLDR("Discount")).ToString("N2"), _
                                  CDec(SQL.SQLDR("VATAmount")).ToString("N2"), _
                                 CDec(SQL.SQLDR("NetAmount")).ToString("N2"), _
                                 "True", _
                                 SQL.SQLDR("VATable").ToString)
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
        txtRemarks.Text = ""
        txtGross.Text = "0.00"
        txtNet.Text = "0.00"
        txtDiscount.Text = "0.00"
        txtVAT.Text = "0.00"
        dtpDocDate.MinDate = GetMaxPEC()
        txtSORef.Text = ""
        txtStatus.Text = "Open"
    End Sub


    Public Sub LoadItem(ByVal ID As String)
        Dim query As String
        query = " SELECT  Description, Amount, RecordID FROM    tblBI_Type " & _
                " WHERE   RecordID ='" & ID & "' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvItemList.Rows.Add(New String() {SQL.SQLDR("RecordID").ToString, SQL.SQLDR("Description").ToString, _
                                                 Format(SQL.SQLDR("Amount"), "#,###,###,###.00").ToString, _
                                                 "", "0.00", "0.00", _
                                                 Format(SQL.SQLDR("Amount"), "#,###,###,###.00").ToString})
        End While
        ComputeTotal()
    End Sub


    Private Sub SaveBI()
        Try
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblBI  (TransID, BI_No, BranchCode, BusinessCode, VCECode, DateBI, DateDue, Remarks, DebitAccnt, " & _
                        "         GrossAmount, Discount, VATAmount, NetAmount, VATable, VATInclusive, SO_Ref,  WhoCreated) " & _
                        " VALUES (@TransID, @BI_No, @BranchCode, @BusinessCode, @VCECode,  @DateBI, @DateDue, @Remarks, @DebitAccnt, " & _
                        "         @GrossAmount, @Discount, @VATAmount, @NetAmount, @VATable, @VATInclusive,  @SO_Ref, @WhoCreated) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@BI_No", BINo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateBI", dtpDocDate.Value.Date)
            SQL.AddParam("@DateDue", dtpDue.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@DebitAccnt", IIf(cbDefaultAcc.SelectedIndex = -1, "", cbDefaultAcc.SelectedItem))
            SQL.AddParam("@GrossAmount", CDec(txtGross.Text))
            SQL.AddParam("@Discount", CDec(txtDiscount.Text))
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@NetAmount", CDec(txtNet.Text))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInclusive", chkVATInc.Checked)
            SQL.AddParam("@SO_Ref", txtSORef.Text)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chGross.Index).Value Is Nothing AndAlso Not row.Cells(chItemDesc.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblBI_Details(TransId,  Description,  GrossAmount, VATable, VATAmount, " & _
                                "                DiscountRate, Discount, NetAmount, LineNum, DescRecordID, WhoCreated) " & _
                                " VALUES(@TransId,  @Description,  @GrossAmount, @VATable, @VATAmount, " & _
                                "        @DiscountRate, @Discount, @NetAmount, @LineNum, @DescRecordID , @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@Description", row.Cells(chItemDesc.Index).Value.ToString)
                    SQL.AddParam("@GrossAmount", CDec(row.Cells(chGross.Index).Value))
                    SQL.AddParam("@VATable", IIf(row.Cells(chVATInc.Index).Value = True, 1, 0))
                    SQL.AddParam("@VATAmount", CDec(row.Cells(chVATAmt.Index).Value))
                    If IsNumeric(row.Cells(chDiscountRate.Index).Value) Then
                        SQL.AddParam("@DiscountRate", CDec(row.Cells(chDiscountRate.Index).Value))
                    Else
                        SQL.AddParam("@DiscountRate", 0)
                    End If
                    SQL.AddParam("@Discount", CDec(row.Cells(chDiscount.Index).Value))
                    SQL.AddParam("@NetAmount", CDec(row.Cells(chNetAmount.Index).Value))
                    SQL.AddParam("@DescRecordID", row.Cells(chRecordID.Index).Value)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next


            insertSQL = " INSERT INTO " & _
                        " tblJE_Header (AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, WhoCreated) " & _
                        " VALUES(@AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @WhoCreated)"
            SQL.FlushParams()
            SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
            SQL.AddParam("@RefType", "BI")
            SQL.AddParam("@RefTransID", TransID)
            SQL.AddParam("@Book", "Sales")
            SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            JETransID = LoadJE("BI", TransID)
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

    Private Sub UpdateBI()
        Try
            activityStatus = True
            BINo = txtTransNum.Text
            Dim insertSQL, updateSQL, deleteSQL As String
            insertSQL = " UPDATE tblBI " & _
                        " SET    BI_No = @BI_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, VCECode = @VCECode, DateBI=@DateBI, " & _
                        "        DateDue = @DateDue, Remarks = @Remarks, DebitAccnt = @DebitAccnt, " & _
                        "        GrossAmount = @GrossAmount, Discount = @Discount, VATAmount = @VATAmount, NetAmount = @NetAmount, " & _
                        "        VATable = @VATable, VATInclusive = @VATInclusive, SO_Ref = @SO_Ref,  WhoModified = @WhoModified " & _
                        " WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@BI_No", BINo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateBI", dtpDocDate.Value.Date)
            SQL.AddParam("@DateDue", dtpDue.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@DebitAccnt", IIf(cbDefaultAcc.SelectedIndex = -1, "", cbDefaultAcc.SelectedItem))
            SQL.AddParam("@GrossAmount", CDec(txtGross.Text))
            SQL.AddParam("@Discount", CDec(txtDiscount.Text))
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@NetAmount", CDec(txtNet.Text))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInclusive", chkVATInc.Checked)
            SQL.AddParam("@SO_Ref", txtSORef.Text)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(insertSQL)

            deleteSQL = " DELETE FROM tblBI_Details WHERE TransID = '" & TransID & "' "
            SQL.ExecNonQuery(deleteSQL)

            Dim line As Integer = 1
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chItemDesc.Index).Value Is Nothing AndAlso Not row.Cells(chGross.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblBI_Details(TransId,  Description,  GrossAmount, VATable, VATAmount, " & _
                                "                DiscountRate, Discount, NetAmount, LineNum, DescRecordID, WhoCreated) " & _
                                " VALUES (@TransId,  @Description,  @GrossAmount, @VATable, @VATAmount, " & _
                                "        @DiscountRate, @Discount, @NetAmount, @LineNum, @DescRecordID, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@Description", row.Cells(chItemDesc.Index).Value.ToString)
                    SQL.AddParam("@GrossAmount", CDec(row.Cells(chGross.Index).Value))
                    SQL.AddParam("@VATable", IIf(row.Cells(chVATInc.Index).Value = True, 1, 0))
                    SQL.AddParam("@VATAmount", CDec(row.Cells(chVATAmt.Index).Value))
                    If IsNumeric(row.Cells(chDiscountRate.Index).Value) Then
                        SQL.AddParam("@DiscountRate", CDec(row.Cells(chDiscountRate.Index).Value))
                    Else
                        SQL.AddParam("@DiscountRate", 0)
                    End If
                    SQL.AddParam("@Discount", CDec(row.Cells(chDiscount.Index).Value))
                    SQL.AddParam("@NetAmount", CDec(row.Cells(chNetAmount.Index).Value))
                    SQL.AddParam("@DescRecordID", row.Cells(chRecordID.Index).Value)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next

            JETransID = LoadJE("BI", TransID)

            If JETransID = 0 Then
                insertSQL = " INSERT INTO " & _
                            " tblJE_Header (AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, WhoCreated) " & _
                            " VALUES(@AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @WhoCreated)"
                SQL.FlushParams()
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "BI")
                SQL.AddParam("@RefTransID", TransID)
                SQL.AddParam("@Book", "Sales")
                SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@WhoCreated", UserID)
                SQL.ExecNonQuery(insertSQL)

                JETransID = LoadJE("BI", TransID)
            Else
                updateSQL = " UPDATE tblJE_Header " & _
                            " SET    AppDate = @AppDate, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " & _
                            "        RefType = @RefType, RefTransID = @RefTransID, Book = @Book, TotalDBCR = @TotalDBCR, " & _
                            "        Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE() " & _
                            " WHERE  JE_No = @JE_No "
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransID)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "BI")
                SQL.AddParam("@RefTransID", TransID)
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
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "BI_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub GenerateEntry()
        Dim dataEntry As New DataTable
        dgvEntry.Rows.Clear()
        'dgvEntry.Rows.Add({accntDR, GetAccntTitle(accntDR), CDec(txtNet.Text).ToString("N2"), "0.00", "SI:" & txtTransNum.Text})
        Dim query As String
        For Each row As DataGridViewRow In dgvItemList.Rows
            If row.Cells(chItemDesc.Index).Value <> Nothing AndAlso row.Cells(chNetAmount.Index).Value > 0 Then
                query = " SELECT DefaultAccountDR, AccountTitle " & _
                        " FROM   tblBI_Type INNER JOIN tblCOA_Master " & _
                        " ON     tblBI_Type.DefaultAccountDR = tblCOA_Master.AccountCode " & _
                        " WHERE  RecordID ='" & row.Cells(chRecordID.Index).Value & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read() Then
                    dgvEntry.Rows.Add({SQL.SQLDR("DefaultAccountDR").ToString, SQL.SQLDR("AccountTitle").ToString, CDec(row.Cells(chNetAmount.Index).Value).ToString("N2") - CDec(row.Cells(chDiscount.Index).Value).ToString("N2"), "0.00", ""})
                End If
            End If
        Next

        For Each row As DataGridViewRow In dgvItemList.Rows
            If row.Cells(chItemDesc.Index).Value <> Nothing AndAlso row.Cells(chDiscount.Index).Value > 0 Then
                query = " SELECT DefaultAccountDiscount, AccountTitle " & _
                        " FROM   tblBI_Type INNER JOIN tblCOA_Master " & _
                        " ON     tblBI_Type.DefaultAccountDiscount = tblCOA_Master.AccountCode " & _
                        " WHERE  RecordID ='" & row.Cells(chRecordID.Index).Value & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read() Then
                    dgvEntry.Rows.Add({SQL.SQLDR("DefaultAccountDiscount").ToString, SQL.SQLDR("AccountTitle").ToString, CDec(row.Cells(chDiscount.Index).Value).ToString("N2"), "0.00", ""})
                End If
            End If
        Next

        For Each row As DataGridViewRow In dgvItemList.Rows
            If row.Cells(chItemDesc.Index).Value <> Nothing AndAlso row.Cells(chVATInc.Index).Value = True Then
                query = " SELECT AR_OutputVAT, AccountTitle " & _
                        " FROM   tblSystemSetup INNER JOIN tblCOA_Master " & _
                        " ON     tblSystemSetup.AR_OutputVAT = tblCOA_Master.AccountCode "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read() Then
                    dgvEntry.Rows.Add({SQL.SQLDR("AR_OutputVAT").ToString, SQL.SQLDR("AccountTitle").ToString, "0.00", CDec(row.Cells(chVATAmt.Index).Value).ToString("N2"), ""})
                End If
            End If
        Next

        For Each row As DataGridViewRow In dgvItemList.Rows
            If row.Cells(chItemDesc.Index).Value <> Nothing AndAlso row.Cells(chGross.Index).Value > 0 Then
                query = " SELECT DefaultAccountCR, AccountTitle " & _
                        " FROM   tblBI_Type INNER JOIN tblCOA_Master " & _
                        " ON     tblBI_Type.DefaultAccountCR = tblCOA_Master.AccountCode " & _
                        " WHERE  RecordID ='" & row.Cells(chRecordID.Index).Value & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read() Then
                    dgvEntry.Rows.Add({SQL.SQLDR("DefaultAccountCR").ToString, SQL.SQLDR("AccountTitle").ToString, "0.00", CDec(row.Cells(chNetAmount.Index).Value).ToString("N2") - CDec(row.Cells(chVATAmt.Index).Value).ToString("N2"), ""})
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
        End If
       


    End Sub

    Private Sub dgvItemMaster_RowsRemoved(sender As System.Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
        ComputeTotal()
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("BI_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("BI")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadBI(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("BI_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            BINo = ""

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
        If Not AllowAccess("BI_EDIT") Then
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
        If Not AllowAccess("BI_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE  tblSI SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        BINo = txtTransNum.Text
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

                        BINo = txtTransNum.Text
                        LoadBI(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "SI_No", BINo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("BI", TransID)
        f.Dispose()
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If BINo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblBI  WHERE BI_No < '" & BINo & "' ORDER BY BI_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadBI(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If BINo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblBI  WHERE BI_No > '" & BINo & "' ORDER BY BI_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadBI(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If BINo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadBI(TransID)
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
            query = " SELECT    ItemCode, Description, UOM, QTY, UnitPrice, GrossAmount, DiscountRate, Discount, VATAmount, NetAmount, VATable " & _
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
                                     SQL.SQLDR("VATable").ToString)
            End While
        Else
            ClearText()
        End If
    End Sub

    Private Sub dgvItemList_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellEndEdit
        Try
            Dim desc As String
            Dim rowIndex As Integer = dgvItemList.CurrentCell.RowIndex
            Dim colindex As Integer = dgvItemList.CurrentCell.ColumnIndex
            Select Case colindex

                Case chItemDesc.Index
                    If dgvItemList.Item(chItemDesc.Index, e.RowIndex).Value <> "" Then
                        desc = dgvItemList.Item(chItemDesc.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("BI_Type", desc)
                        If f.TransID <> "" Then
                            desc = f.TransID
                            LoadItem(desc)
                        End If
                        dgvItemList.Rows.RemoveAt(e.RowIndex)
                        f.Dispose()
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

                Case chGross.Index
                    If IsNumeric(dgvItemList.Item(chGross.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemList.Item(chDiscount.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex)
                        ComputeTotal()
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
                gross = CDec(dgvItemList.Item(chGross.Index, RowID).Value)
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
                dgvItemList.Item(chVATAmt.Index, RowID).Value = Format(VAT, "#,###,###,###.00").ToString()
                dgvItemList.Item(chNetAmount.Index, RowID).Value = Format(net, "#,###,###,###.00").ToString()
                ComputeTotal()
            End If
        End If


    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If txtVCECode.Text = "" Then
            Msg("Please enter VCECode!", MsgBoxStyle.Exclamation)
        ElseIf dgvItemList.Rows.Count = 1 Then
            MsgBox("Please enter an item/services to purchase!", MsgBoxStyle.Exclamation)
        ElseIf txtTransNum.Text = "" Then
            MsgBox("Please enter BI No.!", MsgBoxStyle.Exclamation)
        ElseIf TransID = "" Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                TransID = GenerateTransID(ColumnID, DBTable)
                If TransAuto = True Then
                    BINo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    txtTransNum.Text = BINo
                Else
                    BINo = txtTransNum.Text
                End If

                GenerateEntry()
                SaveBI()
                Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                BINo = txtTransNum.Text
                LoadBI(TransID)
            End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    GenerateEntry()
                    UpdateBI()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    BINo = txtTransNum.Text
                    LoadBI(TransID)
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



    Private Sub ToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem1.Click
        frmBI_Type.Show()
    End Sub

    Private Sub dgvItemList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellContentClick

    End Sub

    Private Sub chkVAT_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkVAT.CheckedChanged
        For Each row As DataGridViewRow In dgvItemList.Rows
            If Not row.Cells(chGross.Index).Value Is Nothing Then
                If chkVAT.CheckState = CheckState.Checked Then
                    row.Cells(chVAT.Index).Value = True
                Else
                    row.Cells(chVAT.Index).Value = False
                End If
            End If


            Recompute(row.Index, chGross.Index)
        Next
        ComputeTotal()
    End Sub

    Private Sub dgvItemList_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dgvItemList.CurrentCellDirtyStateChanged
        If dgvItemList.SelectedCells.Count > 0 AndAlso (dgvItemList.SelectedCells(0).ColumnIndex = chVAT.Index OrElse dgvItemList.SelectedCells(0).ColumnIndex = chVATInc.Index) Then
            If dgvItemList.SelectedCells(0).RowIndex <> -1 Then
                Recompute(dgvItemList.SelectedCells(0).RowIndex, dgvItemList.SelectedCells(0).ColumnIndex)
                dgvItemList.SelectedCells(0).Selected = False
                dgvItemList.EndEdit()
            End If
        End If
    End Sub

End Class