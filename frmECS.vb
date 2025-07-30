Public Class frmECS
    Dim TransID, RefID, JETransID As String
    Dim ECSNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "ECS"
    Dim ColumnPK As String = "ECS_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblECS"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim SO_ID, PL_ID, DR_ID As Integer
    Dim accntDR, accntCR, accntVAT, accntDiscount As String
    Dim Amount As Double


    Public Overloads Function ShowDialog(ByVal docID As String) As Boolean
        TransID = docID
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmSI_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Text = "(" & database & ") - ECS "
            TransAuto = GetTransSetup(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            LoadChartOfAccount()
            If TransID <> "" Then
                LoadECS(TransID)
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
        dtpDue.Enabled = Value
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
                " WHERE ModuleID = '" & ModuleID & "' AND Type = 'Debit' " & _
                " ORDER BY AccountTitle "
        SQL.ReadQuery(query)
        cbDefaultAcc.Items.Clear()
        While SQL.SQLDR.Read
            cbDefaultAcc.Items.Add(SQL.SQLDR("AccountTitle"))
        End While
    End Sub

    Private Sub LoadECS(ByVal TransID As String)
        Dim query As String
        query = " SELECT   TransID, ECS_No, VCECode, DateECS, Remarks, " & _
                "          ISNULL(GrossAmount,0) AS GrossAmount, ISNULL(VATAmount,0) AS VATAmount, ISNULL(Discount,0) AS Discount, ISNULL(NetAmount,0) AS NetAmount, " & _
                "          ISNULL(VATable,1) AS VATable, ISNULL(VATInc,1) AS VATInc, " & _
                "          Status, ISNULL(SO_Ref,0) AS SO_Ref,ISNULL(PL_Ref,0) AS PL_Ref, ISNULL(DR_Ref,0) AS DR_Ref, DebitAccnt " & _
                " FROM     tblECS " & _
                " WHERE    TransId = '" & TransID & "' " & _
                " ORDER BY TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            TransID = SQL.SQLDR("TransID").ToString
            ECSNo = SQL.SQLDR("ECS_No").ToString
            txtTransNum.Text = ECSNo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            dtpDocDate.Text = SQL.SQLDR("DateECS").ToString
            txtGross.Text = CDec(SQL.SQLDR("GrossAmount").ToString).ToString("N2")
            txtVAT.Text = CDec(SQL.SQLDR("VATAmount").ToString).ToString("N2")
            txtDiscount.Text = CDec(SQL.SQLDR("Discount").ToString).ToString("N2")
            txtNet.Text = CDec(SQL.SQLDR("NetAmount").ToString).ToString("N2")
            chkVAT.Checked = SQL.SQLDR("VATable")
            chkVATInc.Checked = SQL.SQLDR("VATInc")
            cbDefaultAcc.SelectedItem = SQL.SQLDR("DebitAccnt").ToString
            SO_ID = SQL.SQLDR("SO_Ref").ToString
            PL_ID = SQL.SQLDR("PL_Ref").ToString
            DR_ID = SQL.SQLDR("DR_Ref").ToString
            txtDRRef.Text = LoadDRNo(DR_ID)
            txtVCEName.Text = GetEmpName(txtVCECode.Text)
            LoadECSDetails(TransID)
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

    Public Function GetEmpName(ByVal VCECode As String) As String
        Dim query As String
        SQL.CloseCon()
        SetPayrollDatabase()
        query = " SELECT Emp_Name FROM viewEmployee_Info WHERE Emp_ID ='" & VCECode & "'"
        SQL_RUBY.ReadQuery(query)
        If SQL_RUBY.SQLDR.Read Then
            Return SQL_RUBY.SQLDR("Emp_Name").ToString
        Else
            Return ""
        End If
        SQL_RUBY.CloseCon()
        SetDatabase()
    End Function

    Private Sub LoadAccountingEntry(ByVal TransID As Integer)
        Try
            Dim query As String
            query = " SELECT JE_No, VCECode, ISNULL(VCEName,'') AS VCEName, AccntCode, AccntTitle, Particulars, " & _
                    "        ISNULL(Debit,0) AS Debit, ISNULL(Credit,0) AS Credit " & _
                    " FROM   View_GL  " & _
                    " WHERE  RefType ='ECS' AND RefTransID ='" & TransID & "' "
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

    Protected Sub LoadECSDetails(ByVal TransID As String)
        dgvItemList.Rows.Clear()
        Dim query As String
        query = " SELECT    ItemCode, Description, UOM, QTY, ISNULL(UnitPrice,0) AS UnitPrice, " & _
                "           ISNULL(GrossAmount,0) AS GrossAmount, ISNULL(DiscountRate,0) AS DiscountRate, ISNULL(Discount,0) AS Discount, " & _
                "           ISNULL(VATAmount,0) AS VATAmount, ISNULL(NetAmount,0) AS NetAmount, ISNULL(VATable,1) AS VATable, ISNULL(VATInc,1) AS VATInc " & _
                " FROM      tblECS_Details " & _
                " WHERE     tblECS_Details.TransId = " & TransID & " " & _
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
            DR_ID = SQL.SQLDR("TransID")
            txtDRRef.Text = LoadDRNo(DR_ID)
            query = "  SELECT tblDR_Details.ItemCode, Description, UOM, Unserved AS QTY,  " & _
                    "       viewDR_Unserved.WHSE, ID_SC as Unit_Price, 0 AS Amount   " & _
                    "  FROM   tblDR_Details  " & _
                    "  INNER JOIN tblItem_Master ON " & _
                    "  tblItem_Master.ItemCode = tblDR_Details.ItemCode " & _
                    "  INNER JOIN viewDR_Unserved  ON     " & _
                    "  tblDR_Details.RecordID = viewDR_Unserved.RecordID  WHERE  tblDR_Details.TransID ='" & DR_No & "'  "
            SQL.ReadQuery(query)
            dgvItemList.Rows.Clear()
            While SQL.SQLDR.Read
                dgvItemList.Rows.Add({SQL.SQLDR("ItemCode").ToString, SQL.SQLDR("Description").ToString, SQL.SQLDR("UOM").ToString,
                                      SQL.SQLDR("QTY"), CDec(SQL.SQLDR("Unit_Price")).ToString("N2"), _
                                      CDec(SQL.SQLDR("Unit_Price")).ToString("N2"), "", "", "0.00", CDec(SQL.SQLDR("Unit_Price")).ToString("N2"), True, True})
            End While
            ComputeTotal()
        Else
            ClearText()
        End If
    End Sub

    Private Sub ClearText()
        DR_ID = 0
        PL_ID = 0
        SO_ID = 0
        txtTransNum.Text = ""
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        txtDRRef.Clear()
        dgvItemList.Rows.Clear()
        dgvEntry.Rows.Clear()
        cbDefaultAcc.SelectedIndex = -1
        txtRemarks.Text = ""
        txtGross.Text = "0.00"
        txtNet.Text = "0.00"
        txtDiscount.Text = "0.00"
        txtVAT.Text = "0.00"
        dtpDocDate.Value = Date.Today.Date
        dtpDue.Value = Date.Today.Date
        txtStatus.Text = "Open"
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("Are you sure you want to cancel Sales Invoice No. " & txtTransNum.Text & "? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Dim updateSQL As String
            updateSQL = " UPDATE tblECS_Header SET Status ='Cancelled' WHERE TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", txtTransNum.Text)
            SQL.ExecNonQuery(updateSQL)
            MsgBox("SI No. " & txtTransNum.Text & " has been cancelled", MsgBoxStyle.Information)
        End If
    End Sub

    Public Function GetTransID() As Integer
        Dim query As String
        query = " SELECT MAX(TransID) + 1 AS TransID FROM tblECS_Header"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() And Not IsDBNull(SQL.SQLDR("TransID")) Then
            Return SQL.SQLDR("TransID")
        Else
            Return 1
        End If
    End Function

    Private Sub dgvProducts_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Try
            Dim itemCode As String
            Dim rowIndex As Integer = dgvItemList.CurrentCell.RowIndex
            Dim colindex As Integer = dgvItemList.CurrentCell.ColumnIndex
            Select Case colindex
                Case chItemCode.Index
                    If dgvItemList.Item(chItemCode.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemList.Item(chItemCode.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("ItemMaster", itemCode)
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
                        f.ShowDialog("ItemMaster", itemCode, "Selling")
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItem(itemCode)
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
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub LoadItem(ByVal ID As String)
        Dim query As String
        query = " SELECT  ItemCode, ItemName, ItemUOM, 1 AS QTY, CONVERT(bit, 0)  AS VATable, " & _
                "         ID_SC, 0 AS VAT " & _
                " FROM    tblItem_Master " & _
                " WHERE   ItemCode ='" & ID & "' AND ItemType ='FINISHED GOODS' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvItemList.Rows.Add(New String() {SQL.SQLDR("ItemCode").ToString, _
                                                 SQL.SQLDR("ItemName").ToString, _
                                                 SQL.SQLDR("ItemUOM").ToString, _
                                                 SQL.SQLDR("QTY").ToString, _
                                                 Format(SQL.SQLDR("ID_SC"), "#,###,###,###.00").ToString, _
                                                 Format(SQL.SQLDR("ID_SC"), "#,###,###,###.00").ToString, _
                                                 "", "0.00", _
                                                 Format(SQL.SQLDR("VAT"), "#,###,###,###.00").ToString, _
                                                 Format(SQL.SQLDR("ID_SC"), "#,###,###,###.00").ToString, _
                                                 SQL.SQLDR("VAtable")})
        End While
        ComputeTotal()
    End Sub

    Private Sub SaveECSDetails(ByVal TransID As Integer)
        Dim insertSQL As String
        Dim line As Integer = 1
        For Each row As DataGridViewRow In dgvItemList.Rows
            If row.Cells(chGross.Index).Value <> Nothing Then
                insertSQL = " INSERT INTO " & _
                            " tblECS_Details(TransID, ItemCode, Description, UOM, QTY, Unit_Price, Amount, LineNum) " & _
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

    Private Sub SaveECS()
        Try
            activityStatus = True
            Dim insertSQL As String
            Dim accntCode As String
            If cbDefaultAcc.SelectedIndex = -1 Then accntCode = "" Else accntCode = GetAccntCode(cbDefaultAcc.SelectedItem)
            insertSQL = " INSERT INTO " & _
                        " tblECS  (TransID, ECS_No, BranchCode, BusinessCode, VCECode, DateECS, DateDue, Remarks, DebitAccnt, " & _
                        "         GrossAmount, Discount, VATAmount, NetAmount, VATable, VATInc, SO_Ref, PL_Ref, DR_Ref,  WhoCreated) " & _
                        " VALUES (@TransID, @ECS_No, @BranchCode, @BusinessCode, @VCECode,  @DateECS, @DateDue, @Remarks, @DebitAccnt, " & _
                        "         @GrossAmount, @Discount, @VATAmount, @NetAmount, @VATable, @VATInc,  @SO_Ref, @PL_Ref, @DR_Ref, @WhoCreated) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@ECS_No", ECSNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateECS", dtpDocDate.Value.Date)
            SQL.AddParam("@DateDue", dtpDue.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@DebitAccnt", accntCode)
            SQL.AddParam("@GrossAmount", CDec(txtGross.Text))
            SQL.AddParam("@Discount", CDec(txtDiscount.Text))
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@NetAmount", CDec(txtNet.Text))
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
                                " tblECS_Details(TransId, ItemCode, Description, UOM, QTY, UnitPrice, GrossAmount, VATable, VATAmount, " & _
                                "                DiscountRate, Discount, NetAmount, LineNum, WhoCreated) " & _
                                " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @UnitPrice, @GrossAmount, @VATable, @VATAmount, " & _
                                "        @DiscountRate, @Discount, @NetAmount, @LineNum, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value))
                    SQL.AddParam("@Description", row.Cells(chItemDesc.Index).Value.ToString)
                    SQL.AddParam("@UOM", row.Cells(chUOM.Index).Value.ToString)
                    SQL.AddParam("@QTY", CDec(row.Cells(chQTY.Index).Value))
                    SQL.AddParam("@UnitPrice", CDec(row.Cells(chUnitPrice.Index).Value))
                    SQL.AddParam("@GrossAmount", CDec(row.Cells(chGross.Index).Value))
                    SQL.AddParam("@VATable", row.Cells(chVAT.Index).Value)
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

            SalaryDeduct(txtVCECode.Text)

            insertSQL = " INSERT INTO " & _
                        " tblJE_Header (AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, WhoCreated) " & _
                        " VALUES(@AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @WhoCreated)"
            SQL.FlushParams()
            SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
            SQL.AddParam("@RefType", "ECS")
            SQL.AddParam("@RefTransID", TransID)
            SQL.AddParam("@Book", "Sales")
            SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            JETransID = LoadJE("ECS", TransID)
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
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "ECS_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub SalaryDeduct(ByVal Emp_ID As String)
        Try

            SQL.CloseCon()
            SetPayrollDatabase()

            Dim insertSQl As String
            insertSQl = " INSERT INTO " & _
                        " tblEmployee_Ledger(Emp_ID, Ledger_Code, Calc_Method, Cutoff, Amount_1st, Amount_2nd, Total_Amount, No_of_Payday, Start_Date, Recurring, Status, ECS_Ref) " & _
                        " SELECT " & Emp_ID & " , 'D022',  'Fixed Amount',  'All', " & Amount & ",  '0.00', '" & Amount & "', '1', '" & dtpDue.Value & "', '0', 'Active', " & TransID & ""
            SQL_RUBY.ExecNonQuery(insertSQl)
            SQL_RUBY.CloseCon()
            SetDatabase()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub UpdateSalaryDeduct(ByVal TransID As String, ByVal Emp_ID As String)
        Try

            SQL.CloseCon()
            SetPayrollDatabase()

            Dim updateSQL As String
            updateSQL = " UPDATE tblEmployee_Ledger " & _
                        " SET    Emp_ID = @Emp_ID, Ledger_Code = @Ledger_Code, Calc_Method = @Calc_Method, Cutoff = @Cutoff, Amount_1st = @Amount_1st, " & _
                        "        Amount_2nd = @Amount_2nd, Total_Amount = @Total_Amount, No_of_Payday = @No_of_Payday, Start_Date = @Start_Date, Recurring = @Recurring, " & _
                        "        Status = @Status, ECS_Ref = @ECS_Ref, Who_Modified = @Who_Modified " & _
                        " WHERE  ECS_Ref = '" & TransID & "' "
            SQL_RUBY.FlushParams()
            SQL_RUBY.AddParam("@Emp_ID", Emp_ID)
            SQL_RUBY.AddParam("@Ledger_Code", "D022")
            SQL_RUBY.AddParam("@Calc_Method", "Fixed Amount")
            SQL_RUBY.AddParam("@Cutoff", "All")
            SQL_RUBY.AddParam("@Amount_1st", Amount)
            SQL_RUBY.AddParam("@Amount_2nd", "0.00")
            SQL_RUBY.AddParam("@Total_Amount", Amount)
            SQL_RUBY.AddParam("@No_of_Payday", "1")
            SQL_RUBY.AddParam("@Start_Date", dtpDue.Value)
            SQL_RUBY.AddParam("@Recurring", "0")
            SQL_RUBY.AddParam("@Status", "Active")
            SQL_RUBY.AddParam("@ECS_Ref", TransID)
            SQL_RUBY.AddParam("@Who_Modified", UserID)

            SQL_RUBY.ExecNonQuery(updateSQL)
            SQL_RUBY.CloseCon()
            SetDatabase()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub UpdateECS()
        Try
            activityStatus = True
            Dim accntCode As String
            If cbDefaultAcc.SelectedIndex = -1 Then accntCode = "" Else accntCode = GetAccntCode(cbDefaultAcc.SelectedItem)
            Dim insertSQL, updateSQL, deleteSQL As String
            insertSQL = " UPDATE tblECS " & _
                        " SET    ECS_No = @ECS_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, VCECode = @VCECode, DateECS = @DateECS, " & _
                        "        DateDue = @DateDue, Remarks = @Remarks, DebitAccnt = @DebitAccnt, VATable = @VATable, VATInc = @VATInc, " & _
                        "        GrossAmount = @GrossAmount, Discount = @Discount, VATAmount = @VATAmount, NetAmount = @NetAmount, " & _
                        "        SO_Ref = @SO_Ref, PL_Ref = @PL_Ref, DR_Ref = @DR_Ref, WhoModified = @WhoModified " & _
                        " WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@ECS_No", ECSNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateECS", dtpDocDate.Value.Date)
            SQL.AddParam("@DateDue", dtpDue.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@DebitAccnt", accntCode)
            SQL.AddParam("@GrossAmount", CDec(txtGross.Text))
            SQL.AddParam("@Discount", CDec(txtDiscount.Text))
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@NetAmount", CDec(txtNet.Text))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInc", chkVATInc.Checked)
            SQL.AddParam("@SO_Ref", SO_ID)
            SQL.AddParam("@PL_Ref", PL_ID)
            SQL.AddParam("@DR_Ref", DR_ID)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(insertSQL)

            deleteSQL = " DELETE FROM tblECS_Details WHERE TransID = '" & TransID & "' "
            SQL.ExecNonQuery(deleteSQL)

            Dim line As Integer = 1
            For Each row As DataGridViewRow In dgvItemList.Rows
                If Not row.Cells(chQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblECS_Details(TransId, ItemCode, Description, UOM, QTY, UnitPrice, GrossAmount, VATable, VATAmount, " & _
                                "                DiscountRate, Discount, NetAmount, LineNum, WhoCreated) " & _
                                " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @UnitPrice, @GrossAmount, @VATable, @VATAmount, " & _
                                "        @DiscountRate, @Discount, @NetAmount, @LineNum, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value))
                    SQL.AddParam("@Description", row.Cells(chItemDesc.Index).Value.ToString)
                    SQL.AddParam("@UOM", row.Cells(chUOM.Index).Value.ToString)
                    SQL.AddParam("@QTY", CDec(row.Cells(chQTY.Index).Value))
                    SQL.AddParam("@UnitPrice", CDec(row.Cells(chUnitPrice.Index).Value))
                    SQL.AddParam("@GrossAmount", CDec(row.Cells(chGross.Index).Value))
                    SQL.AddParam("@VATable", row.Cells(chVAT.Index).Value)
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

            UpdateSalaryDeduct(TransID, txtVCECode.Text)

            JETransID = LoadJE("ECS", TransID)

            If JETransID = 0 Then
                insertSQL = " INSERT INTO " & _
                            " tblJE_Header (AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, WhoCreated) " & _
                            " VALUES(@AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @WhoCreated)"
                SQL.FlushParams()
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "ECS")
                SQL.AddParam("@RefTransID", TransID)
                SQL.AddParam("@Book", "Sales")
                SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@WhoCreated", UserID)
                SQL.ExecNonQuery(insertSQL)

                JETransID = LoadJE("ECS", TransID)
            Else
                updateSQL = " UPDATE tblJE_Header " & _
                            " SET    AppDate = @AppDate, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " & _
                            "        RefType = @RefType, RefTransID = @RefTransID, Book = @Book, TotalDBCR = @TotalDBCR, " & _
                            "        Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE() " & _
                            " WHERE  JE_No = @JE_No "
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransID)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "ECS")
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
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "ECS_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub GenerateEntry()
        txtTotalDebit.Text = "0.00"
        txtTotalCredit.Text = "0.00"
        Dim dataEntry As New DataTable
        Dim Item_Cost As Decimal
        accntDR = GetAccntCode(cbDefaultAcc.SelectedItem)
        accntVAT = "1550-1030"
        dgvEntry.Rows.Clear()
        dgvEntry.Rows.Add({accntDR, GetAccntTitle(accntDR), CDec(txtNet.Text).ToString("N2"), "0.00", "ECS:" & txtTransNum.Text})
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
                query = " SELECT AD_COS, AccountTitle, ISNULL(ID_SC,0) AS ID_SC " & _
                        " FROM   tblItem_Master INNER JOIN tblCOA_Master " & _
                        " ON     tblItem_Master.AD_COS = tblCOA_Master.AccountCode " & _
                        " WHERE  ItemCode ='" & row.Cells(chItemCode.Index).Value & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read() Then
                    Item_Cost = SQL.SQLDR("ID_SC").ToString * row.Cells(chQTY.Index).Value
                    dgvEntry.Rows.Add({SQL.SQLDR("AD_COS").ToString, SQL.SQLDR("AccountTitle").ToString, "0.00", CDec(Item_Cost).ToString("N2"), ""})
                End If
            End If
        Next

        TotalDBCR()

        Dim Variance As Decimal
        If txtTotalDebit.Text < txtTotalCredit.Text Then
            query = " SELECT AccntCode, AccountTitle FROM tblDefaultAccount " & _
                    " INNER JOIN " & _
                    " tblCOA_Master ON " & _
                    " tblCOA_Master.AccountCode = tblDefaultAccount.AccntCode " & _
                    " WHERE ModuleID = '" & ModuleID & "' AND Type = 'Variance' " & _
                    " ORDER BY AccountTitle "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read() Then
                Variance = txtTotalCredit.Text - txtTotalDebit.Text
                If Variance < 0 Then
                    Variance = txtTotalDebit.Text - txtTotalCredit.Text
                    dgvEntry.Rows.Add({SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccountTitle").ToString, "0.00", CDec(Variance).ToString("N2"), ""})
                Else
                    dgvEntry.Rows.Add({SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccountTitle").ToString, CDec(Variance).ToString("N2"), "0.00", ""})
                End If
            End If
        ElseIf txtTotalDebit.Text > txtTotalCredit.Text Then
            query = " SELECT AccntCode, AccountTitle FROM tblDefaultAccount " & _
                   " INNER JOIN " & _
                   " tblCOA_Master ON " & _
                   " tblCOA_Master.AccountCode = tblDefaultAccount.AccntCode " & _
                   " WHERE ModuleID = '" & ModuleID & "' AND Type = 'Variance' " & _
                   " ORDER BY AccountTitle "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read() Then
                Variance = txtTotalDebit.Text - txtTotalCredit.Text
                If Variance < 0 Then
                    Variance = txtTotalCredit.Text - txtTotalDebit.Text
                    dgvEntry.Rows.Add({SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccountTitle").ToString, CDec(Variance).ToString("N2"), "0.00", ""})
                Else
                    dgvEntry.Rows.Add({SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccountTitle").ToString, "0.00", CDec(Variance).ToString("N2"), ""})
                End If
            End If
        End If
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

        ' COMPUTE TOTAL CREDIT
        Dim c As Decimal = 0

        For i As Integer = 0 To dgvItemList.Rows.Count - 1
            If Val(dgvItemList.Item(chNetAmount.Index, i).Value) <> 0 Then
                c = c + Double.Parse(dgvItemList.Item(chNetAmount.Index, i).Value).ToString("N2")
            End If
        Next
        Amount = c.ToString("N2")
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
        If Not AllowAccess("ECS_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("ECS")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadECS(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("ECS_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            ECSNo = ""


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
        If Not AllowAccess("ECS_EDIT") Then
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
        If Not AllowAccess("ECS_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE  tblECS SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        ECSNo = txtTransNum.Text
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

                        ECSNo = txtTransNum.Text
                        LoadECS(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "ECS_No", ECSNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("ECS", TransID)
        f.Dispose()
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If ECSNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblECS  WHERE ECS_No < '" & ECSNo & "' ORDER BY ECS_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadECS(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If ECSNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblECS  WHERE ECS_No > '" & ECSNo & "' ORDER BY ECS_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadECS(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If ECSNo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadECS(TransID)
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
            f.cbFilter.SelectedItem = "Emp_Name"
            f.Type = "RUBY Employee"
            f.txtFilter.Text = txtVCEName.Text
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            txtVCEName.Text = f.VCEName
        End If
    End Sub


    Private Sub dgvItemList_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellEndEdit
        Try
            Dim itemCode As String
            Dim rowIndex As Integer = dgvItemList.CurrentCell.RowIndex
            Dim colindex As Integer = dgvItemList.CurrentCell.ColumnIndex
            Select Case colindex

                Case chItemCode.Index
                    If dgvItemList.Item(chItemCode.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemList.Item(chItemCode.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("FinishedGood", itemCode, "FINISHED GOODS")
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
                        f.ShowDialog("FinishedGood", itemCode, "FINISHED GOODS")
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
        ElseIf dgvItemList.Rows.Count = 1 Then
            MsgBox("Please enter an item/services to purchase!", MsgBoxStyle.Exclamation)
        ElseIf cbDefaultAcc.SelectedIndex = -1 Then
            MsgBox("Please select default DR account!", MsgBoxStyle.Exclamation)
        ElseIf TransID = "" Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                TransID = GenerateTransID(ColumnID, DBTable)
                ECSNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                txtTransNum.Text = ECSNo
                GenerateEntry()
                SaveECS()
                Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                ECSNo = txtTransNum.Text
                LoadECS(TransID)
            End If
        Else
            If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                GenerateEntry()
                UpdateECS()
                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                ECSNo = txtTransNum.Text
                LoadECS(TransID)
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
            f.ShowDialog("DR-ECS")
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

    Private Sub dgvItemList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemList.CellContentClick

    End Sub

    Private Sub btnSearchVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchVCE.Click

    End Sub

    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged

    End Sub

    Private Sub dgvEntry_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntry.CellContentClick

    End Sub
End Class