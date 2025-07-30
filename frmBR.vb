Public Class FrmBR
    Dim TransID, RefID, JETransiD As String
    Dim BRNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "BR"
    Dim ColumnPK As String = "BR_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblBR"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim bankID As Integer
    Dim bank As String

    Dim JVNo, JVJETransiD As String
    Dim JVTransID As String
    Dim JVTransAuto As Boolean
    Dim clearingAccount As String
    Dim isClearingEnabled As Boolean = False

    Public Overloads Function ShowDialog(ByVal DocNumber As String) As Boolean
        TransID = DocNumber
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub FrmBank_Recon_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Text = "(" & database & ") - Bank Reconciliation "
            TransAuto = GetTransSetup(ModuleID)
            JVTransAuto = GetTransSetup("JV")
            LoadBank()
            LoadSetup()
            RefreshBRType()
            dtpDocDate.Value = Date.Today.Date
            If TransID <> "" Then
                LoadBR(TransID)
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
            tsbRefresh.Enabled = False
            EnableControl(False)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadSetup()
        Dim query As String
        query = " SELECT   ISNULL(CIB_Clearing ,0) AS CIB_Clearing  FROM tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            isClearingEnabled = SQL.SQLDR("CIB_Clearing").ToString
        End If
        If isClearingEnabled Then
            TabControl1.TabPages.Remove(TabPage1)
        End If
    End Sub
    Private Function GetMinDateForBR(BankID As String) As Date
        Dim query As String
        query = " SELECT DATEADD(DAY,1,ISNULL(MAX(DateBR),'01-01-1900')) AS DateBR FROM tblBR WHERE Status <> 'Cancelled' AND BankID ='" & BankID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("DateBR")
        Else
            Return "01-01-1900"
        End If
    End Function

    Private Sub LoadBR(ByVal TransID As String)
        Dim query As String
        query = " SELECT   TransID, BR_No, DateBR, BankID, BookEndBal, BookAdjBal, BankEndBal, BankAdjBal, OC, DIT, Status, Remarks" & _
                " FROM     tblBR " & _
                " WHERE    TransId = '" & TransID & "' " & _
                " ORDER BY TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            disableEvent = True
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            BRNo = SQL.SQLDR("BR_No").ToString
            txtTransNum.Text = BRNo
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            dtpDocDate.Text = SQL.SQLDR("DateBR").ToString
            txtBookBal.Text = CDec(SQL.SQLDR("BookEndBal").ToString).ToString("N2")
            txtAdjBookBal.Text = CDec(SQL.SQLDR("BookAdjBal").ToString).ToString("N2")
            txtBankBal.Text = CDec(SQL.SQLDR("BankEndBal").ToString).ToString("N2")
            txtAdjBankBal.Text = CDec(SQL.SQLDR("BankAdjBal").ToString).ToString("N2")
            txtOC.Text = CDec(SQL.SQLDR("OC").ToString).ToString("N2")
            txtDIT.Text = CDec(SQL.SQLDR("DIT").ToString).ToString("N2")
            disableEvent = False
            SelectBank(SQL.SQLDR("BankID").ToString)
            ' TOOLSTRIP BUTTONS

            tsbCancel.Text = "Cancel"
            ' TOOLSTRIP BUTTONS
            If txtStatus.Text = "Cancelled" Then
                tsbEdit.Enabled = False
                tsbCancel.Text = "Un-Can"
                tsbCancel.Enabled = True
            Else
                tsbEdit.Enabled = True
                tsbCancel.Enabled = True
            End If

            tsbPrint.Enabled = True
            tsbClose.Enabled = False
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbPrint.Enabled = True
            tsbRefresh.Enabled = False
            tsbSave.Enabled = False
            tsbNew.Enabled = True
            tsbSearch.Enabled = True
            tsbExit.Enabled = True
            EnableControl(False)
            LoadOC()
            LoadDIT()
            LoadBRJV(TransID, txtAccountCode.Text)
            LoadAdjustedBankBalance()
            LoadAdjustedBookBalance()
            LoadClearedDIT()
            ComputeVariance()
        Else
            ClearText()
        End If
    End Sub

    Private Sub LoadBRJV(ByVal ID As String, BankAccount As String)
        Dim query As String
        query = " SELECT    TransID, JV_No, JE_No, AccntCode, AccntTitle, view_GL.VCECode, VCEName, Credit-Debit AS Amount, Particulars, tblJV.Remarks " & _
                " FROM      tblJV INNER JOIN view_GL  " & _
                " ON        tblJV.TransID = view_GL.RefTransID  " & _
                " AND       view_GL.RefType ='JV' " & _
                " WHERE     BR_Ref = '" & TransID & "' And AccntCode <> '" & BankAccount & "' " & _
                " AND       tblJV.Status <> 'Cancelled' "
        SQL.ReadQuery(query)
        disableEvent = True
        dgvJV.Rows.Clear()
        disableEvent = False
        While SQL.SQLDR.Read
            dgvJV.Rows.Add(SQL.SQLDR("Particulars").ToString, CDec(SQL.SQLDR("Amount")).ToString("N2"), _
                           SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccntTitle").ToString, _
                           SQL.SQLDR("VCECode").ToString, SQL.SQLDR("VCEName").ToString)
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            JVJETransiD = SQL.SQLDR("JE_No").ToString
            JVNo = SQL.SQLDR("JV_No").ToString
            JVTransID = SQL.SQLDR("TransID").ToString
            txtJVNO.Text = SQL.SQLDR("JV_No").ToString
        End While
        LoadJVAdj()
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        dtpDocDate.Enabled = Value
        cbBank.Enabled = Value
        txtBankBal.ReadOnly = Not Value
        txtRemarks.ReadOnly = Not Value
        dgvJV.AllowUserToAddRows = Value
        dgvJV.AllowUserToDeleteRows = Value
        If Value = True Then
            dgvJV.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvJV.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        dgvOC.AllowUserToAddRows = False
        dgvOC.AllowUserToDeleteRows = False
        If Value = True Then
            dgvOC.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvOC.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        dgvDIT.AllowUserToAddRows = False
        dgvDIT.AllowUserToDeleteRows = False
        If Value = True Then
            dgvDIT.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvDIT.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        dgvCleared.AllowUserToAddRows = False
        dgvCleared.AllowUserToDeleteRows = False
        If Value = True Then
            dgvCleared.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvCleared.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        chkDITcheckAll.Enabled = Value
        chkOCcheckAll.Enabled = Value
        dgvDIT.Enabled = Value
        dgvOC.Enabled = Value
        dgvCleared.Enabled = Value
        btnAdjType.Enabled = Value
        cbBankReconType.Enabled = Value
        btnUpdateDeposit.Enabled = Value
        btnUpdateChecks.Enabled = Value
        txtJVRemarks.ReadOnly = Not Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
        If JVTransAuto Then
            txtJVNO.Visible = False
            txtJVNO.Enabled = False
        Else
            txtJVNO.Visible = True
            txtJVNO.Enabled = Value
        End If
    End Sub

    Private Sub ClearText()
        txtTransNum.Text = ""
        txtBankBal.Text = ""
        txtBookBal.Text = ""
        txtAdjBankBal.Text = ""
        txtAdjBookBal.Text = ""
        txtDIT.Text = ""
        txtOC.Text = ""
        txtDITSubTotal.Text = ""
        txtOCsubtotal.Text = ""
        txtClearedAmount.Text = ""
        dgvDIT.Rows.Clear()
        dgvCleared.Rows.Clear()
        dgvOC.Rows.Clear()
        dgvJV.Rows.Clear()
        txtRemarks.Text = ""

        dtpDocDate.MinDate = GetMinDateForBR(txtBankID.Text)
        txtStatus.Text = "Open"
    End Sub

    Private Sub SelectBank(ByVal bankID As String)
        Dim query As String
        query = " SELECT    CAST(BankID AS nvarchar) + '-' + Bank + ' ' + Branch + ' ' + AccountNo AS BankDetails,  BankID, Bank, Branch, tblBank_Master.AccountCode, AccountTitle,  AccountNo" & _
                " FROM      tblBank_Master LEFT JOIN tblCOA_Master " & _
                " ON        tblBank_Master.AccountCode = tblCOA_Master.AccountCode " & _
                " WHERE     BankID = '" & bankID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            If Not cbBank.Items.Contains(SQL.SQLDR("BankDetails").ToString) Then
                cbBank.Items.Add(SQL.SQLDR("BankDetails").ToString)
            End If
            disableEvent = True
            cbBank.SelectedItem = SQL.SQLDR("BankDetails").ToString
            txtBankID.Text = SQL.SQLDR("BankID")
            txtAccountCode.Text = SQL.SQLDR("AccountCode")
            txtAccountTitle.Text = SQL.SQLDR("AccountTitle")
            txtBankAccountNo.Text = SQL.SQLDR("AccountNo")
            disableEvent = False
        End If
    End Sub
    Private Sub LoadBank()
        Dim query As String
        query = " SELECT    CAST(BankID AS nvarchar) + '-' + Bank + ' ' + Branch + ' ' + AccountNo AS BankDetails " & _
                " FROM      tblBank_Master INNER JOIN tblCOA_Master  " & _
                " ON        tblBank_Master.AccountCode = tblCOA_Master.AccountCode " & _
                " AND       tblCOA_Master.Status ='Active' " & _
                " WHERE     tblBank_Master.Status ='Active' " & _
                " ORDER BY  Bank, Branch "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            bank = SQL.SQLDR("BankDetails").ToString
            cbBank.Items.Add(bank)
        End While
    End Sub

    Private Sub LoadBankDetail(bank As String)
        Dim query As String
        Dim bank1() As String = Strings.Split(bank, "-")
        bankID = bank1(0)
        query = " SELECT   BankID, Bank, Branch, tblBank_Master.AccountCode, AccountTitle, AccountNo, ISNULL(AccountCode_Clearing,'') AS AccountCode_Clearing " &
                " FROM     tblBank_Master LEFT JOIN tblCOA_Master " &
                " ON       tblBank_Master.AccountCode = tblCOA_Master.AccountCode " &
                " WHERE    BankID = '" & bankID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtBankID.Text = SQL.SQLDR("BankID")
            txtAccountCode.Text = SQL.SQLDR("AccountCode")
            txtAccountTitle.Text = SQL.SQLDR("AccountTitle")
            txtBankAccountNo.Text = SQL.SQLDR("AccountNo")
            clearingAccount = SQL.SQLDR("AccountCode_Clearing")
        End If
    End Sub

    Private Sub LoadBookBalance()
        Dim query As String
        query = " SELECT   ISNULL(SUM(Debit-Credit),0) AS BookBalance " & _
                " FROM     view_GL " & _
                " WHERE    AppDate <=  '" & dtpDocDate.Value.Date & "'  AND YEAR(Appdate) = '" & dtpDocDate.Value.Year & "'  " & _
                " AND      AccntCode = '" & txtAccountCode.Text & "' AND Status <> 'Cancelled' " & _
                " AND      JE_No NOT IN (SELECT JE_No FROM tblJE_Header WHERE RefTransID IN (SELECT TransID FROM tblJV WHERE BR_Ref = '" & TransID & "') AND RefType='JV' )"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtBookBal.Text = CDec(SQL.SQLDR("BookBalance")).ToString("N2")
        Else
            txtBookBal.Text = 0
        End If
    End Sub

    Private Sub txtBank_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbBank.SelectedIndexChanged
        If disableEvent = False Then
            If cbBank.SelectedIndex <> -1 Then
                LoadBankDetail(cbBank.SelectedItem)
                dtpDocDate.MinDate = GetMinDateForBR(txtBankID.Text)
                If dtpDocDate.MinDate <= Date.Today.Date Then
                    disableEvent = True
                    dtpDocDate.Value = Date.Today.Date
                    disableEvent = False
                End If
                RefreshData()
            End If
        End If
    End Sub

    Private Sub RefreshData()
        LoadBookBalance()
        LoadDIT()
        LoadOC()
        LoadAdjustedBookBalance()
        LoadAdjustedBankBalance()
        ComputeVariance()
        LoadClearedDIT()
    End Sub

    Private Sub ComputeVariance()
        Dim BankBal, BookBal, Variance As Decimal
        If IsNumeric(txtAdjBookBal.Text) Then BookBal = CDec(txtAdjBookBal.Text) Else BookBal = 0
        If IsNumeric(txtAdjBankBal.Text) Then BankBal = CDec(txtAdjBankBal.Text) Else BankBal = 0
        Variance = (BookBal - BankBal)
        txtVariance.Text = Variance.ToString("N2")
    End Sub


    Private Sub LoadDIT()
        Try
            Dim query As String
            If isClearingEnabled Then
                query = " SELECT ID, AppDate, RefTransID, RefType, TransNo,  Debit.Amount -  ISNULL(Credit.Amount,0) AS Amount   " &
                        " FROM " &
                        " ( " &
                        "  SELECT    ID, AppDate, RefTransID, RefType, TransNo, RefType + ':' + TransNo AS Ref,  SUM(Debit - Credit) AS Amount   " &
                        "  FROM      view_GL  " &
                        "  WHERE     AccntCode = '" & clearingAccount & "' AND AppDate <='" & dtpDocDate.Value.Date & "' " &
                        "  AND       Book='Cash Receipts' AND Status <> 'Cancelled'  " &
                        "  GROUP BY  ID, AppDate, RefTransID, RefType, TransNo " &
                        "  ) AS Debit " &
                        "  LEFT JOIN  " &
                        "  ( " &
                        "    SELECT    RefNo, SUM(Credit-Debit) AS Amount   " &
                        "  FROM      view_GL  " &
                        "  WHERE     AccntCode = '" & clearingAccount & "' AND AppDate <= '" & dtpDocDate.Value.Date & "' " &
                        "  AND       Book <> 'Cash Receipts' AND Status <> 'Cancelled'  " &
                        "  GROUP BY  RefNO " &
                        "  ) AS Credit " &
                        "  ON Debit.Ref = Credit.RefNo " &
                        " WHERE Debit.Amount - ISNULL(Credit.Amount,0) > 0 "
            Else
                query = " SELECT    ID, AppDate, RefTransID, RefType, TransNo,  SUM(Debit - Credit) AS Amount  " &
                       " FROM      view_GL " &
                       " WHERE     AccntCode = '" & txtAccountCode.Text & "' AND AppDate <='" & dtpDocDate.Value.Date & "' " &
                       " AND       Book='Cash Receipts' AND Status <> 'Cancelled' " &
                       " AND      (dateCleared IS NULL OR dateCleared  >'" & dtpDocDate.Value.Date & "') " &
                       " GROUP BY  ID, AppDate, RefTransID, RefType, TransNo" &
                       " UNION ALL " &
                       " SELECT ID, AppDate, TransID, RefType, TransNo, Amount  FROM tblOCDIT_details " &
                       " LEFT JOIN " &
                       " viewVCE_Master ON " &
                       " viewVCE_Master.VCECode = tblOCDIT_details.VCECode " &
                       " WHERE     AccntCode = '" & txtAccountCode.Text & "' AND AppDate <='" & dtpDocDate.Value.Date & "' " &
                       " AND       Book='Cash Receipts' AND tblOCDIT_details.Status <> 'Cancelled' " &
                       " AND      (dateCleared IS NULL OR dateCleared  >'" & dtpDocDate.Value.Date & "') "
            End If

            SQL.ReadQuery(query)
            dgvDIT.Rows.Clear()
            Dim total As Decimal = 0
            If SQL.SQLDR.HasRows Then
                While SQL.SQLDR.Read
                    dgvDIT.Rows.Add(False, SQL.SQLDR("ID"), SQL.SQLDR("AppDate"), SQL.SQLDR("RefTransID").ToString, SQL.SQLDR("RefType").ToString, _
                                   SQL.SQLDR("TransNo"), CDec(SQL.SQLDR("Amount")).ToString("N2"))
                    total += SQL.SQLDR("Amount")
                End While
                TxtTotalDIT.Text = total.ToString("N2")
                txtDIT.Text = total.ToString("N2")
            Else
                dgvDIT.Rows.Clear()
                TxtTotalDIT.Text = 0
                txtDIT.Text = 0
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadOC()
        Try
            Dim query As String
            If isClearingEnabled Then
                query = " SELECT ID, AppDate, RefTransID, RefType, VCEName, TransNo, Check_No, Credit.Amount - ISNULL(Debit.Amount,0) AS Amount   " &
                        " FROM " &
                        " ( " &
                        "  SELECT    ID, AppDate, RefTransID, RefType, VCEName, TransNo, Check_No,  RefType + ':' + TransNo AS Ref,  SUM(Credit - Debit) AS Amount   " &
                        "  FROM      view_GL  " &
                        "  WHERE     AccntCode = '" & clearingAccount & "' AND AppDate <='" & dtpDocDate.Value.Date & "' " &
                        "  AND       Book='Cash Disbursements' AND Status <> 'Cancelled'  " &
                        "  GROUP BY  ID, AppDate, RefTransID, RefType, VCEName, TransNo, Check_No " &
                        "  ) AS Credit " &
                        "  LEFT JOIN  " &
                        "  ( " &
                        "    SELECT    RefNo, SUM(Debit-Credit) AS Amount   " &
                        "  FROM      view_GL  " &
                        "  WHERE     AccntCode = '" & clearingAccount & "' AND AppDate <= '" & dtpDocDate.Value.Date & "' " &
                        "  AND       Book <> 'Cash Receipts' AND Status <> 'Cancelled'  " &
                        "  GROUP BY  RefNO " &
                        "  ) AS Debit " &
                        "  ON Credit.Ref = Debit.RefNo " &
                        " WHERE Credit.Amount - ISNULL(Debit.Amount,0) > 0 "
            Else
                query = " SELECT    ID, AppDate, RefTransID, RefType, TransNo, Check_No, VCEName, Credit - Debit AS Amount  " &
                        " FROM      view_GL " &
                        " WHERE     AccntCode = '" & txtAccountCode.Text & "' AND AppDate <='" & dtpDocDate.Value.Date & "' " &
                        " AND       Book='Cash Disbursements' AND Status <> 'Cancelled' " &
                        " AND      (dateCleared IS NULL OR dateCleared  >'" & dtpDocDate.Value.Date & "') " &
                        " UNION ALL " &
                        " SELECT ID, AppDate, TransID, RefType, TransNo, Check_No, viewVCE_Master.VCEName, Amount  FROM tblOCDIT_details " &
                        " LEFT JOIN " &
                        " viewVCE_Master ON " &
                        " viewVCE_Master.VCECode = tblOCDIT_details.VCECode " &
                        " WHERE     AccntCode = '" & txtAccountCode.Text & "' AND AppDate <='" & dtpDocDate.Value.Date & "' " &
                        " AND       Book='Cash Disbursements' AND tblOCDIT_details.Status <> 'Cancelled' " &
                        " AND      (dateCleared IS NULL OR dateCleared  >'" & dtpDocDate.Value.Date & "') "
            End If

            SQL.ReadQuery(query)
            dgvOC.Rows.Clear()
            Dim total As Decimal = 0
            If SQL.SQLDR.HasRows Then
                While SQL.SQLDR.Read
                    dgvOC.Rows.Add(False, SQL.SQLDR("ID"), CDate(SQL.SQLDR("AppDate")).Date, SQL.SQLDR("RefTransID").ToString, SQL.SQLDR("RefType").ToString, SQL.SQLDR("TransNo"), _
                                   CDec(SQL.SQLDR("Amount")).ToString("N2"), SQL.SQLDR("Check_No").ToString, SQL.SQLDR("VCEName").ToString)
                    total += SQL.SQLDR("Amount")
                End While
                txtOC.Text = total.ToString("N2")
                TxtTotalOutstanding.Text = total.ToString("N2")
            Else
                dgvOC.Rows.Clear()
                txtOC.Text = 0
                TxtTotalOutstanding.Text = 0
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try

    End Sub

    Private Sub LoadBankRecon(ByVal ID As Integer)
        Dim query As String
        query = " SELECT Bank, BankReconDate, Trial_Balace_Amount, Cash_In_Bank, Deposit_In_Transit, OutstandingCheck " & _
                " FROM   ABank_Recon WHERE TransID ='" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() Then
            disableEvent = True
            cbBank.Text = SQL.SQLDR("Bank").ToString
            txtBookBal.Text = SQL.SQLDR("Trial_Balace_Amount")
            txtBankBal.Text = SQL.SQLDR("Cash_In_Bank")
            If dtpDocDate.Value <> SQL.SQLDR("BankReconDate") Then
                dtpDocDate.Value = SQL.SQLDR("BankReconDate")
            End If
            disableEvent = False
        End If
        LoadBankDetail(cbBank.Text)
    End Sub

    Private Sub LoadAdjustedBookBalance()
        Dim bookBal, adjCash As Decimal
        If IsNumeric(txtBookBal.Text) Then bookBal = CDec(txtBookBal.Text) Else bookBal = 0
        If IsNumeric(txtJVadjustment.Text) Then adjCash = CDec(txtJVadjustment.Text) Else adjCash = 0
        txtAdjBookBal.Text = CDec(bookBal + adjCash).ToString("N2")
    End Sub


    Private Sub dgvJV_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvJV.CellEndEdit
        Dim rowIndex As Integer = dgvJV.CurrentCell.RowIndex
        Dim colIndex As Integer = dgvJV.CurrentCell.ColumnIndex
        Dim AccountTitle As String
        Try
            Select Case colIndex
                Case dgcJVamount.Index
                    Dim Amount = Double.Parse(dgvJV.Item(dgcJVamount.Index, e.RowIndex).Value)
                    dgvJV.Item(dgcJVamount.Index, e.RowIndex).Value = CDec(Amount).ToString("N2")
                Case dgcJVCode.Index
                    dgvJV.Item(dgcJVtitle.Index, e.RowIndex).Value = GetAccntTitle((dgvJV.Item(dgcJVCode.Index, e.RowIndex).Value))
                Case dgcJVtitle.Index
                    AccountTitle = dgvJV.Item(dgcJVtitle.Index, e.RowIndex).Value
                    Dim f As New frmCOA_Search
                    f.ShowDialog("AccntTitle", AccountTitle)
                    dgvJV.Rows.RemoveAt(rowIndex) 'delete active row
                    dgvJV.Item(dgcJVCode.Index, e.RowIndex).Value = f.accountcode
                    dgvJV.Item(dgcJVtitle.Index, e.RowIndex).Value = f.accttile
                    f.Dispose()
                Case dgcJVvceCode.Index
                    If dgvJV.Item(dgcJVvceCode.Index, e.RowIndex).Value <> "" Then
                        If GetVCEName(dgvJV.Item(dgcJVvceCode.Index, e.RowIndex).Value) = "" Then
                            Msg("VCEName doesn't exist!", MsgBoxStyle.Exclamation)
                            dgvJV.Item(dgcJVvceCode.Index, e.RowIndex).Value = ""
                            dgvJV.Item(dgcJVvceName.Index, e.RowIndex).Value = ""
                        Else
                            dgvJV.Item(dgcJVvceName.Index, e.RowIndex).Value = GetVCEName(dgvJV.Item(dgcJVvceCode.Index, e.RowIndex).Value)
                        End If
                    End If
                Case dgcJVvceName.Index
                    If dgvJV.Item(dgcJVvceName.Index, e.RowIndex).Value <> "" Then
                        Dim f As New frmVCE_Search
                        f.txtFilter.Text = dgvJV.Item(dgcJVvceName.Index, e.RowIndex).Value
                        f.ShowDialog()
                        dgvJV.Item(dgcJVvceCode.Index, e.RowIndex).Value = f.VCECode
                        dgvJV.Item(dgcJVvceName.Index, e.RowIndex).Value = f.VCEName
                    End If
            End Select
            LoadJVAdj()
            LoadAdjustedBookBalance()
            ComputeVariance()
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub chkOCcheckAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkOCcheckAll.CheckedChanged

        Try
            Dim a As Double = 0
            For i As Integer = 0 To dgvOC.Rows.Count - 1
                dgvOC.Item(0, i).Value = chkOCcheckAll.Checked
            Next
            computeOCsubtotal()
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadAdjustedBankBalance()
        Dim DIT, CIB, OC, ADJ As Double
        If IsNumeric(txtBankBal.Text) Then CIB = CDec(txtBankBal.Text) Else CIB = 0
        If IsNumeric(txtDIT.Text) Then DIT = CDec(txtDIT.Text) Else DIT = 0
        If IsNumeric(txtOC.Text) Then OC = CDec(txtOC.Text) Else OC = 0
        ADJ = CIB + DIT - OC
        txtAdjBankBal.Text = CDec(ADJ).ToString("N2")
    End Sub

    Private Sub dgridOC_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvOC.CellContentClick
        Try
            If e.ColumnIndex = dgcOCsearch.Index Then
                Dim TransID As String = dgvOC.Item(dgcOCrefID.Index, e.RowIndex).Value
                Dim f As New frmCV
                f.ShowDialog(TransID)
                f.Dispose()
            Else
                Dim rowIndex As Integer = dgvOC.CurrentCell.RowIndex
                Dim colIndex As Integer = dgvOC.CurrentCell.ColumnIndex
                If dgvOC.Item(0, rowIndex).Value Then
                    dgvOC.Item(0, rowIndex).Value = 0
                Else
                    dgvOC.Item(0, rowIndex).Value = 1
                End If
                computeOCsubtotal()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub dgvDIT_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDIT.CellContentClick
        Try
            If e.ColumnIndex = dgcDITsearch.Index Then
                Dim DocID As Integer = dgvDIT.Item(dgcDITrefID.Index, e.RowIndex).Value
                Dim DocType As String = dgvDIT.Item(dgcDITtype.Index, e.RowIndex).Value
                Dim f As New frmBD  'LoadingScreen 'ParentMenu '
                'f.ShowDialog(DocID)
                f.Dispose()
            Else
                Dim rowIndex As Integer = dgvDIT.CurrentCell.RowIndex
                Dim colIndex As Integer = dgvDIT.CurrentCell.ColumnIndex
                If dgvDIT.Item(0, rowIndex).Value Then
                    dgvDIT.Item(0, rowIndex).Value = False
                Else
                    dgvDIT.Item(0, rowIndex).Value = True
                End If
                computeDITsubtotal()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub computeDITsubtotal()
        Dim subtotal As Decimal
        Try
            subtotal = 0
            For i As Integer = 0 To dgvDIT.Rows.Count - 1
                Dim checked As Boolean
                checked = dgvDIT.Item(0, i).Value
                If checked AndAlso IsNumeric(dgvDIT.Item(dgcDITamount.Index, i).Value) Then
                    subtotal = CDec(dgvDIT.Item(dgcDITamount.Index, i).Value) + subtotal
                End If
            Next
            txtDITSubTotal.Text = Format(subtotal, "#,###,###,###.00").ToString()
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub computeOCsubtotal()
        Dim subtotal As Decimal
        Try
            subtotal = 0
            For i As Integer = 0 To dgvOC.Rows.Count - 1
                Dim checked As Boolean
                checked = dgvOC.Item(0, i).Value
                If checked AndAlso IsNumeric(dgvOC.Item(dgcOCamount.Index, i).Value) Then
                    subtotal = CDec(dgvOC.Item(dgcOCamount.Index, i).Value) + subtotal
                End If
            Next
            txtOCsubtotal.Text = Format(subtotal, "#,###,###,###.00").ToString()
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtAdjustedBookBal_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtAdjBookBal.TextChanged, txtAdjBankBal.TextChanged

    End Sub


    Private Sub btnUpdateDeposit_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdateDeposit.Click
        Dim updateSQL As String
        Dim withcheck As Boolean = False
        Try
            For i As Integer = 0 To dgvDIT.Rows.Count - 1
                Dim Post As Boolean
                Post = dgvDIT.Item(0, i).Value
                If Post Then
                    withcheck = True
                    Exit For
                End If
            Next
            If withcheck = False Then
                Msg("No selected transactions to be cleared!  " & vbNewLine & "Please mark check for those items to be cleared", MsgBoxStyle.Exclamation)
            Else
                If isClearingEnabled Then
                    SaveDITClearingJV()
                Else
                    For i As Integer = 0 To dgvDIT.Rows.Count - 1
                        Dim Post As Boolean
                        Dim RefType As String
                        Post = dgvDIT.Item(0, i).Value
                        RefType = dgvDIT.Item(dgcDITtype.Index, i).Value
                        If Post Then

                            If RefType <> "DIT" Then
                                updateSQL = " UPDATE tblJE_Details " &
                                                    " SET    dateCleared = '" & dtpDocDate.Value.Date & "' " &
                                                    " WHERE  ID = '" & dgvDIT.Item(dgcDITID.Index, i).Value & "'  "
                                SQL.ExecNonQuery(updateSQL)
                            Else
                                updateSQL = " UPDATE tblOCDIT_Details " &
                                                    " SET    dateCleared = '" & dtpDocDate.Value.Date & "' " &
                                                    " WHERE  ID = '" & dgvDIT.Item(dgcDITID.Index, i).Value & "' AND RefType = 'DIT' "
                                SQL.ExecNonQuery(updateSQL)
                            End If

                        End If
                    Next
                End If

            End If
            RefreshData()
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub btnUpdateChecks_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdateChecks.Click
        Dim updateSQL As String
        Dim withcheck As Boolean = False
        Try
            For i As Integer = 0 To dgvOC.Rows.Count - 1
                Dim Post As Boolean
                Post = dgvOC.Item(0, i).Value
                If Post Then
                    withcheck = True
                    Exit For
                End If
            Next
            If withcheck = False Then
                Msg("No selected transactions to be cleared!  " & vbNewLine & "Please mark check for those items to be cleared", MsgBoxStyle.Exclamation)
            Else
                If isClearingEnabled Then
                    SaveOCClearingJV()
                Else
                    For i As Integer = 0 To dgvOC.Rows.Count - 1
                        Dim Post As Boolean
                        Dim RefType As String
                        Post = dgvOC.Item(0, i).Value
                        RefType = dgvOC.Item(dgcDITtype.Index, i).Value
                        If Post Then
                            If RefType <> "OC" Then
                                updateSQL = " UPDATE tblJE_Details " &
                                            " SET    dateCleared = '" & dtpDocDate.Value.Date & "' " &
                                            " WHERE  ID = '" & dgvOC.Item(dgcDITID.Index, i).Value & "'  "
                                SQL.ExecNonQuery(updateSQL)

                                updateSQL = " UPDATE tblCV_BankRef " &
                                       " SET   Status ='Cleared' WHERE CV_No ='" & dgvOC.Item(dgcOCrefID.Index, i).Value & "' AND BankID = '" & bankID & "' AND RefNo ='" & dgvOC.Item(chOC_CheckNo.Index, i).Value & "' "
                                SQL.ExecNonQuery(updateSQL)
                            Else
                                updateSQL = " UPDATE tblOCDIT_Details " &
                                            " SET    dateCleared = '" & dtpDocDate.Value.Date & "' " &
                                            " WHERE  ID = '" & dgvOC.Item(dgcDITID.Index, i).Value & "' AND Reftype = 'OC' "
                                SQL.ExecNonQuery(updateSQL)
                            End If
                        End If
                    Next
                End If

            End If
            RefreshData()
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub chkDITcheckAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDITcheckAll.CheckedChanged
        Try
            Dim a As Double = 0
            For i As Integer = 0 To dgvDIT.Rows.Count - 1
                dgvDIT.Item(0, i).Value = chkDITcheckAll.Checked
            Next
            computeDITsubtotal()
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("BR_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            BRNo = ""

            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbRefresh.Enabled = True
            tsbCancel.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
            tsbPrint.Enabled = False
            EnableControl(True)

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
            txtJVNO.Text = GenerateTransNum(JVTransAuto, "JV", "JV_No", "tblJV")
        End If
    End Sub

    Private Function GenerateTransNumJV(ByRef Auto As Boolean, ModuleID As String, ColumnPK As String, Table As String) As String
        Dim TransNum As String = ""
        If Auto = True Then
            ' GENERATE TRANS ID 
            Dim query As String
            Do
                query = " SELECT	GlobalSeries, ISNULL(BranchCode,'All') AS BranchCode, ISNULL(BusinessCode,'All') AS BusinessCode,  " & _
                                    " 		    ISNULL(TransGroup,0) AS TransGroup, ISNULL(Prefix,'') AS Prefix, ISNULL(Digits,6) AS Digits, " & _
                                    "           ISNULL(StartRecord,1) AS StartRecord, LEN(ISNULL(Prefix,'')) + ISNULL(Digits,6) AS ID_Length " & _
                                    " FROM	    tblTransactionSetup LEFT JOIN tblTransactionDetails " & _
                                    " ON		tblTransactionSetup.TransType = tblTransactionDetails.TransType " & _
                                    " WHERE	    tblTransactionSetup.TransType ='" & ModuleID & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    If SQL.SQLDR("GlobalSeries") = False Then
                        If SQL.SQLDR("BranchCode") = "All" AndAlso SQL.SQLDR("BusinessCode") = "All" Then
                            Dim digits As Integer = 8
                            Dim prefix As String = SQL.SQLDR("Prefix")
                            Dim startrecord As Integer = SQL.SQLDR("StartRecord")
                            query = " SELECT    ISNULL(MAX(SUBSTRING(" & ColumnPK & "," & prefix.Length + 1 & "," & digits & "))+ 1,1) AS TransID  " & _
                                    " FROM      " & Table & "  " & _
                                    " WHERE     " & ColumnPK & " LIKE '" & prefix & "%' AND LEN(" & ColumnPK & ") = '" & digits & "'  AND TransAuto = 1 "
                            SQL.ReadQuery(query)
                            If SQL.SQLDR.Read Then
                                TransNum = SQL.SQLDR("TransID")
                                For i As Integer = 1 To digits
                                    TransNum = "0" & TransNum
                                Next
                                TransNum = prefix & Strings.Right(TransNum, digits)

                                ' CHECK IF GENERATED TRANSNUM ALREADY EXIST
                                query = " SELECT    " & ColumnPK & " AS TransID  " & _
                                        " FROM      " & Table & "  " & _
                                        " WHERE     " & ColumnPK & " = '" & TransNum & "' "
                                SQL.ReadQuery(query)
                                If SQL.SQLDR.Read Then
                                    Dim updateSQL As String
                                    updateSQL = " UPDATE  " & Table & "  SET TransAuto = 1 WHERE " & ColumnPK & " = '" & TransNum & "' "
                                    SQL.ExecNonQuery(updateSQL)
                                    TransNum = -1
                                End If
                            End If
                        End If
                    End If
                End If
                If TransNum <> -1 Then Exit Do
            Loop

            Return TransNum
        Else
            Return ""
        End If
    End Function

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        ' Toolstrip Buttons
        If BRNo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadBR(TransID)
            tsbEdit.Enabled = True
            tsbCancel.Enabled = True
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbPrint.Enabled = True
        End If
        tsbSearch.Enabled = True
        tsbNew.Enabled = True
        tsbRefresh.Enabled = False
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If BRNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblBR  WHERE BR_No > '" & BRNo & "' ORDER BY BR_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadBR(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If BRNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblBR WHERE BR_No < '" & BRNo & "' ORDER BY BR_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadBR(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        Try
            Dim variance As Decimal
            If IsNumeric(txtVariance.Text) Then
                variance = txtVariance.Text
            Else
                variance = 0
            End If
            If variance <> 0 Then
                Msg("You can't save bank recon. if there is a variance!", MsgBoxStyle.Exclamation)
            ElseIf cbBank.SelectedIndex = -1 Then
                MsgBox("Please select bank!", MsgBoxStyle.Exclamation)
            ElseIf Not IsNumeric(txtBankBal.Text) Then
                MsgBox("Invalid Bank Balance!", MsgBoxStyle.Exclamation)
            ElseIf TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID(ColumnID, DBTable)
                    If TransAuto Then
                        BRNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    Else
                        BRNo = txtTransNum.Text
                    End If
                    txtTransNum.Text = BRNo
                    SaveBR()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    BRNo = txtTransNum.Text
                    LoadBR(TransID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    If BRNo = txtTransNum.Text Then
                        BRNo = txtTransNum.Text
                        UpdateBR()
                        Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                        BRNo = txtTransNum.Text
                        LoadBR(TransID)
                    Else
                        If Not IfExist(txtTransNum.Text) Then
                            BRNo = txtTransNum.Text
                            UpdateBR()
                            Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                            BRNo = txtTransNum.Text
                            LoadBR(TransID)
                        Else
                            MsgBox("BR" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Function IfExist(ByVal ID As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblBR WHERE BR_No ='" & ID & "'  "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub SaveBR()
        Dim SQL1 As New SQLControl
        SQL1.BeginTransaction()
        Try
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblBR  (TransID, BR_No, BranchCode, BusinessCode, DateBR, BankID, BookEndBal, BookAdjBal, BankEndBal, BankAdjBal, " & _
                        "         OC, DIT, Status, Remarks, DateCreated, WhoCreated) " & _
                        " VALUES (@TransID, @BR_No, @BranchCode, @BusinessCode, @DateBR,  @BankID, @BookEndBal, @BookAdjBal, @BankEndBal, @BankAdjBal, " & _
                        "         @OC, @DIT, @Status, @Remarks, GETDATE(), @WhoCreated) "
            SQL1.FlushParams()
            SQL1.AddParam("@TransID", TransID)
            SQL1.AddParam("@BR_No", BRNo)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@DateBR", dtpDocDate.Value.Date)
            SQL1.AddParam("@BankID", txtBankID.Text)
            If IsNumeric(txtBookBal.Text) Then SQL1.AddParam("@BookEndBal", CDec(txtBookBal.Text)) Else SQL1.AddParam("@BookEndBal", 0)
            If IsNumeric(txtAdjBookBal.Text) Then SQL1.AddParam("@BookAdjBal", CDec(txtAdjBookBal.Text)) Else SQL1.AddParam("@BookAdjBal", 0)
            If IsNumeric(txtBankBal.Text) Then SQL1.AddParam("@BankEndBal", CDec(txtBankBal.Text)) Else SQL1.AddParam("@BankEndBal", 0)
            If IsNumeric(txtAdjBankBal.Text) Then SQL1.AddParam("@BankAdjBal", CDec(txtAdjBankBal.Text)) Else SQL1.AddParam("@BankAdjBal", 0)
            If IsNumeric(txtOC.Text) Then SQL1.AddParam("@OC", CDec(txtOC.Text)) Else SQL1.AddParam("@OC", 0)
            If IsNumeric(txtDIT.Text) Then SQL1.AddParam("@DIT", CDec(txtDIT.Text)) Else SQL1.AddParam("@DIT", 0)
            SQL1.AddParam("@Status", "Active")
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.ExecNonQuery(insertSQL)

            If dgvJV.Rows.Count > 0 Then
                SaveJV(SQL1)
            End If
            SQL1.Commit()
        Catch ex As Exception
            SQL1.Rollback()
            TransID = ""
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "BR_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL1.FlushParams()
        End Try
    End Sub

    Private Sub SaveJV(SQL1 As SQLControl)
        JVTransID = GenerateTransID("TransID", "tblJV")
        JVNo = GenerateTransNum(JVTransAuto, "JV", "JV_No", "tblJV")
        txtJVNO.Text = JVNo
        Try
            Dim insertSQL As String
            activityStatus = True
            insertSQL = " INSERT INTO " & _
                        " tblJV (TransID, JV_No, BranchCode, BusinessCode, DateJV, TotalAmount, BR_Ref, Remarks, WhoCreated) " & _
                        " VALUES(@TransID, @JV_No, @BranchCode, @BusinessCode, @DateJV, @TotalAmount, @BR_Ref, @Remarks, @WhoCreated)"
            SQL1.FlushParams()
            SQL1.AddParam("@TransID", JVTransID)
            SQL1.AddParam("@JV_No", JVNo)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@DateJV", dtpDocDate.Value.Date)
            If IsNumeric(txtJVadjustment.Text) Then
                SQL1.AddParam("@TotalAmount", Math.Abs(CDec(txtJVadjustment.Text)))
            Else
                SQL1.AddParam("@TotalAmount", 0)
            End If
            SQL1.AddParam("@BR_Ref", TransID)
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.ExecNonQuery(insertSQL)

            JETransiD = GenerateTransID("JE_No", "tblJE_Header")

            insertSQL = " INSERT INTO " & _
                        " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, WhoCreated) " & _
                        " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @WhoCreated)"
            SQL1.FlushParams()
            SQL1.AddParam("@JE_No", JETransiD)
            SQL1.AddParam("@AppDate", dtpDocDate.Value.Date)
            SQL1.AddParam("@RefType", "JV")
            SQL1.AddParam("@RefTransID", JVTransID)
            SQL1.AddParam("@Book", "General Journal")
            SQL1.AddParam("@TotalDBCR", Math.Abs(CDec(txtJVadjustment.Text)))
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.ExecNonQuery(insertSQL)

            ' JVJETransiD = LoadJE("JV", JVTransID)

            Dim line As Integer = 1
            For Each item As DataGridViewRow In dgvJV.Rows
                If item.Cells(dgcJVCode.Index).Value <> Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber) " & _
                                " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber)"
                    SQL1.FlushParams()
                    SQL1.AddParam("@JE_No", JETransiD)
                    SQL1.AddParam("@AccntCode", item.Cells(dgcJVCode.Index).Value.ToString)
                    If item.Cells(dgcJVvceCode.Index).Value <> Nothing AndAlso item.Cells(dgcJVvceCode.Index).Value <> "" Then
                        SQL1.AddParam("@VCECode", item.Cells(dgcJVvceCode.Index).Value.ToString)
                    Else
                        SQL1.AddParam("@VCECode", "")
                    End If
                    If Not IsNothing(item.Cells(dgcJVamount.Index).Value) AndAlso IsNumeric(item.Cells(dgcJVamount.Index).Value) AndAlso IsNumeric(item.Cells(dgcJVamount.Index).Value) < 0 Then
                        SQL1.AddParam("@Debit", Math.Abs(CDec(item.Cells(dgcJVamount.Index).Value)))
                    Else
                        SQL1.AddParam("@Debit", 0)
                    End If
                    If Not IsNothing(item.Cells(dgcJVamount.Index).Value) AndAlso IsNumeric(item.Cells(dgcJVamount.Index).Value) AndAlso IsNumeric(item.Cells(dgcJVamount.Index).Value) > 0 Then
                        SQL1.AddParam("@Credit", CDec(item.Cells(dgcJVamount.Index).Value))
                    Else
                        SQL1.AddParam("@Credit", 0)
                    End If
                    If item.Cells(dgcJVparticulars.Index).Value <> Nothing AndAlso item.Cells(dgcJVparticulars.Index).Value <> "" Then
                        SQL1.AddParam("@Particulars", item.Cells(dgcJVparticulars.Index).Value.ToString)
                    Else
                        SQL1.AddParam("@Particulars", "")
                    End If
                    SQL1.AddParam("@RefNo", "")
                    SQL1.AddParam("@LineNumber", line)
                    SQL1.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
            insertSQL = " INSERT INTO " & _
                               " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, dateCleared, LineNumber) " & _
                               " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @dateCleared, @LineNumber)"
            SQL1.FlushParams()
            SQL1.AddParam("@JE_No", JETransiD)
            SQL1.AddParam("@AccntCode", txtAccountCode.Text)
            SQL1.AddParam("@VCECode", "")
            If IsNumeric(txtJVadjustment.Text) AndAlso CDec(txtJVadjustment.Text) > 0 Then
                SQL1.AddParam("@Debit", CDec(txtJVadjustment.Text))
            Else
                SQL1.AddParam("@Debit", 0)
            End If
            If IsNumeric(txtJVadjustment.Text) AndAlso CDec(txtJVadjustment.Text) < 0 Then
                SQL1.AddParam("@Credit", Math.Abs(CDec(txtJVadjustment.Text)))
            Else
                SQL1.AddParam("@Credit", 0)
            End If
            SQL1.AddParam("@Particulars", "Bank Recon. Item")
            SQL1.AddParam("@RefNo", "BR:" & txtTransNum.Text)
            SQL1.AddParam("@dateCleared", dtpDocDate.Value.Date)
            SQL1.AddParam("@LineNumber", line)
            SQL1.ExecNonQuery(insertSQL)
        Catch ex As Exception
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "JV_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL1.FlushParams()
        End Try
    End Sub

    Private Sub SaveDITClearingJV()
        Dim SQL1 As New SQLControl

        SQL1.BeginTransaction()
        JVTransID = GenerateTransID("TransID", "tblJV")
        JVNo = GenerateTransNum(JVTransAuto, "JV", "JV_No", "tblJV")
        Try
            Dim insertSQL As String
            activityStatus = True
            insertSQL = " INSERT INTO " &
                        " tblJV (TransID, JV_No, BranchCode, BusinessCode, DateJV, TotalAmount, isClearingEntry , Remarks, WhoCreated) " &
                        " VALUES(@TransID, @JV_No, @BranchCode, @BusinessCode, @DateJV, @TotalAmount, @isClearingEntry , @Remarks, @WhoCreated)"
            SQL1.FlushParams()
            SQL1.AddParam("@TransID", JVTransID)
            SQL1.AddParam("@JV_No", JVNo)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@DateJV", Date.Today.Date)
            If IsNumeric(txtJVadjustment.Text) Then
                SQL1.AddParam("@TotalAmount", Math.Abs(CDec(txtDITSubTotal.Text)))
            Else
                SQL1.AddParam("@TotalAmount", 0)
            End If
            SQL1.AddParam("@isClearingEntry ", True)
            SQL1.AddParam("@Remarks", "DIT Clearing")
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.ExecNonQuery(insertSQL)

            JETransiD = GenerateTransID("JE_No", "tblJE_Header")

            insertSQL = " INSERT INTO " &
                        " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, WhoCreated) " &
                        " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @WhoCreated)"
            SQL1.FlushParams()
            SQL1.AddParam("@JE_No", JETransiD)
            SQL1.AddParam("@AppDate", dtpDocDate.Value.Date)
            SQL1.AddParam("@RefType", "JV")
            SQL1.AddParam("@RefTransID", JVTransID)
            SQL1.AddParam("@Book", "General Journal")
            SQL1.AddParam("@TotalDBCR", Math.Abs(CDec(txtDITSubTotal.Text)))
            SQL1.AddParam("@Remarks", "DIT Clearing")
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.ExecNonQuery(insertSQL)


            insertSQL = " INSERT INTO " &
                               " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, dateCleared, LineNumber) " &
                               " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @dateCleared, @LineNumber)"
            SQL1.FlushParams()
            SQL1.AddParam("@JE_No", JETransiD)
            SQL1.AddParam("@AccntCode", txtAccountCode.Text)
            SQL1.AddParam("@VCECode", "")
            SQL1.AddParam("@Debit", CDec(txtDITSubTotal.Text))
            SQL1.AddParam("@Credit", 0)
            SQL1.AddParam("@Particulars", "DIT Clearing")
            SQL1.AddParam("@dateCleared", Date.Today.Date)
            SQL1.AddParam("@LineNumber", 1)
            SQL1.ExecNonQuery(insertSQL)

            Dim line As Integer = 2
            For Each row As DataGridViewRow In dgvDIT.Rows
                If row.Cells(0).Value = True Then
                    insertSQL = " INSERT INTO " &
                               " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber) " &
                               " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber)"
                    SQL1.FlushParams()
                    SQL1.AddParam("@JE_No", JETransiD)
                    SQL1.AddParam("@AccntCode", clearingAccount)
                    SQL1.AddParam("@VCECode", "")
                    SQL1.AddParam("@Debit", 0)
                    SQL1.AddParam("@Credit", CDec(row.Cells(dgcDITamount.Index).Value))
                    SQL1.AddParam("@Particulars", "DIT Clearing")
                    SQL1.AddParam("@RefNo", row.Cells(dgcDITtype.Index).Value & ":" & row.Cells(dgcDITrefNo.Index).Value)
                    SQL1.AddParam("@LineNumber", line)
                    SQL1.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
            SQL1.Commit()
        Catch ex As Exception
            SQL1.Rollback()
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "JV_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL1.FlushParams()
        End Try
    End Sub

    Private Sub SaveOCClearingJV()
        Dim SQL1 As New SQLControl

        SQL1.BeginTransaction()
        JVTransID = GenerateTransID("TransID", "tblJV")
        JVNo = GenerateTransNum(JVTransAuto, "JV", "JV_No", "tblJV")
        Try
            Dim insertSQL, updateSQL As String
            activityStatus = True
            insertSQL = " INSERT INTO " &
                        " tblJV (TransID, JV_No, BranchCode, BusinessCode, DateJV, TotalAmount, isClearingEntry , Remarks, WhoCreated) " &
                        " VALUES(@TransID, @JV_No, @BranchCode, @BusinessCode, @DateJV, @TotalAmount, @isClearingEntry , @Remarks, @WhoCreated)"
            SQL1.FlushParams()
            SQL1.AddParam("@TransID", JVTransID)
            SQL1.AddParam("@JV_No", JVNo)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@DateJV", Date.Today.Date)
            If IsNumeric(txtJVadjustment.Text) Then
                SQL1.AddParam("@TotalAmount", Math.Abs(CDec(txtOCsubtotal.Text)))
            Else
                SQL1.AddParam("@TotalAmount", 0)
            End If
            SQL1.AddParam("@isClearingEntry ", True)
            SQL1.AddParam("@Remarks", "OC Clearing")
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.ExecNonQuery(insertSQL)

            JETransiD = GenerateTransID("JE_No", "tblJE_Header")

            insertSQL = " INSERT INTO " &
                        " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, WhoCreated) " &
                        " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @WhoCreated)"
            SQL1.FlushParams()
            SQL1.AddParam("@JE_No", JETransiD)
            SQL1.AddParam("@AppDate", dtpDocDate.Value.Date)
            SQL1.AddParam("@RefType", "JV")
            SQL1.AddParam("@RefTransID", JVTransID)
            SQL1.AddParam("@Book", "General Journal")
            SQL1.AddParam("@TotalDBCR", Math.Abs(CDec(txtOCsubtotal.Text)))
            SQL1.AddParam("@Remarks", "OC Clearing")
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            For Each row As DataGridViewRow In dgvOC.Rows
                If row.Cells(dgcOCinclude.Index).Value = 1 Then
                    insertSQL = " INSERT INTO " &
                               " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber) " &
                               " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber)"
                    SQL1.FlushParams()
                    SQL1.AddParam("@JE_No", JETransiD)
                    SQL1.AddParam("@AccntCode", clearingAccount)
                    SQL1.AddParam("@VCECode", "")
                    SQL1.AddParam("@Debit", CDec(row.Cells(dgcOCamount.Index).Value))
                    SQL1.AddParam("@Credit", 0)
                    SQL1.AddParam("@Particulars", "OC Clearing")
                    SQL1.AddParam("@RefNo", row.Cells(dgcOCrefType.Index).Value & ":" & row.Cells(dgcOCrefNo.Index).Value)
                    SQL1.AddParam("@LineNumber", line)
                    SQL1.ExecNonQuery(insertSQL)
                    line += 1
                    updateSQL = " UPDATE tblCV_BankRef " &
                                        " SET   Status ='Cleared' WHERE CV_No ='" & row.Cells(dgcOCrefID.Index).Value & "' AND BankID = '" & bankID & "' AND RefNo ='" & row.Cells(chOC_CheckNo.Index).Value & "' "
                    SQL.ExecNonQuery(updateSQL)
                End If
            Next


            insertSQL = " INSERT INTO " &
                               " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, dateCleared, LineNumber) " &
                               " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @dateCleared, @LineNumber)"
            SQL1.FlushParams()
            SQL1.AddParam("@JE_No", JETransiD)
            SQL1.AddParam("@AccntCode", txtAccountCode.Text)
            SQL1.AddParam("@VCECode", "")
            SQL1.AddParam("@Debit", 0)
            SQL1.AddParam("@Credit", CDec(txtOCsubtotal.Text))
            SQL1.AddParam("@Particulars", "OC Clearing")
            SQL1.AddParam("@dateCleared", Date.Today.Date)
            SQL1.AddParam("@LineNumber", line)
            SQL1.ExecNonQuery(insertSQL)

            SQL1.Commit()
        Catch ex As Exception
            SQL1.Rollback()
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "JV_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL1.FlushParams()
        End Try
    End Sub
    Private Sub UpdateJV(ByVal CancelTrans As Boolean)
        Dim SQL As New SQLControl
        SQL.BeginTransaction()
        Try
            If CancelTrans Then
                Dim updateSQL As String
                updateSQL = " UPDATE  tblJV SET Status ='Cancelled' WHERE TransID = @TransID "
                SQL.FlushParams()
                SQL.AddParam("@TransID", JVTransID)
                SQL.ExecNonQuery(updateSQL)

                updateSQL = " UPDATE  tblJE_Header SET Status ='Cancelled' WHERE RefTransID = @RefTransID  AND RefType ='JV' "
                SQL.FlushParams()
                SQL.AddParam("@RefTransID", JVTransID)
                SQL.ExecNonQuery(updateSQL)

                Dim insertSQL As String
                insertSQL = " INSERT INTO tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, OtherRef, dateCleared) " & _
                            " SELECT    JE_No, AccntCode, VCECode, Credit, Debit, Particulars, RefNo, LineNumber, OtherRef, dateCleared " & _
                            " FROM      tblJE_Details " & _
                            " WHERE     JE_No IN (SELECT DISTINCT JE_No FROM tblJE_Header WHERE RefType ='JV' AND RefTransID ='" & JVTransID & "') "
                SQL.ExecNonQuery(insertSQL)

            Else
                Dim insertSQL As String
                activityStatus = True
                insertSQL = " UPDATE    tblJV " & _
                            " SET       JV_No = @JV_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, DateJV  = @DateJV, TotalAmount = @TotalAmount, " & _
                            "           BR_Ref = @BR_Ref, Remarks = @Remarks, Status = @Status, WhoModified = @WhoModified, DateModified = GETDATE() " & _
                            " WHERE     TransID = @TransID "
                SQL.FlushParams()
                SQL.AddParam("@TransID", JVTransID)
                SQL.AddParam("@JV_No", JVNo)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@DateJV", dtpDocDate.Value.Date)
                If IsNumeric(txtJVadjustment.Text) Then
                    SQL.AddParam("@TotalAmount", Math.Abs(CDec(txtJVadjustment.Text)))
                Else
                    SQL.AddParam("@TotalAmount", 0)
                End If
                SQL.AddParam("@BR_Ref", TransID)
                SQL.AddParam("@Status", "Saved")
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@WhoModified", UserID)
                SQL.ExecNonQuery(insertSQL)

                JVJETransiD = LoadJE("JV", JVTransID)

                insertSQL = " UPDATE    tblJE_Header " & _
                            " SET       AppDate = @AppDate, BranchCode = @BranchCode, BusinessCode = @BusinessCode, RefType = @RefType, RefTransID = @RefTransID,  " & _
                            "           Book = @Book, TotalDBCR = @TotalDBCR, Remarks = @Remarks, WhoModified = @WhoModified, Status = @Status, DateModified = GETDATE() " & _
                            " WHERE     JE_No = @JE_No "
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JVJETransiD)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "JV")
                SQL.AddParam("@RefTransID", JVTransID)
                SQL.AddParam("@Book", "General Journal")
                SQL.AddParam("@TotalDBCR", Math.Abs(CDec(txtJVadjustment.Text)))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@Status", "Saved")
                SQL.AddParam("@WhoModified", UserID)
                SQL.ExecNonQuery(insertSQL)


                ' DELETE ACCOUNTING ENTRIES
                Dim deleteSQL As String
                deleteSQL = " DELETE FROM tblJE_Details  WHERE  JE_No = @JE_No "
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JVJETransiD)
                SQL.ExecNonQuery(deleteSQL)

                Dim line As Integer = 1
                For Each item As DataGridViewRow In dgvJV.Rows
                    If item.Cells(dgcJVCode.Index).Value <> Nothing Then
                        insertSQL = " INSERT INTO " & _
                                    " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber) " & _
                                    " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber)"
                        SQL.FlushParams()
                        SQL.AddParam("@JE_No", JVJETransiD)
                        SQL.AddParam("@AccntCode", item.Cells(dgcJVCode.Index).Value.ToString)
                        If item.Cells(dgcJVvceCode.Index).Value <> Nothing AndAlso item.Cells(dgcJVvceCode.Index).Value <> "" Then
                            SQL.AddParam("@VCECode", item.Cells(dgcJVvceCode.Index).Value.ToString)
                        Else
                            SQL.AddParam("@VCECode", "")
                        End If
                        If Not IsNothing(item.Cells(dgcJVamount.Index).Value) AndAlso IsNumeric(item.Cells(dgcJVamount.Index).Value) AndAlso IsNumeric(item.Cells(dgcJVamount.Index).Value) < 0 Then
                            SQL.AddParam("@Debit", Math.Abs(CDec(item.Cells(dgcJVamount.Index).Value)))
                        Else
                            SQL.AddParam("@Debit", 0)
                        End If
                        If Not IsNothing(item.Cells(dgcJVamount.Index).Value) AndAlso IsNumeric(item.Cells(dgcJVamount.Index).Value) AndAlso IsNumeric(item.Cells(dgcJVamount.Index).Value) > 0 Then
                            SQL.AddParam("@Credit", CDec(item.Cells(dgcJVamount.Index).Value))
                        Else
                            SQL.AddParam("@Credit", 0)
                        End If
                        If item.Cells(dgcJVparticulars.Index).Value <> Nothing AndAlso item.Cells(dgcJVparticulars.Index).Value <> "" Then
                            SQL.AddParam("@Particulars", item.Cells(dgcJVparticulars.Index).Value.ToString)
                        Else
                            SQL.AddParam("@Particulars", "")
                        End If
                        SQL.AddParam("@RefNo", "")
                        SQL.AddParam("@LineNumber", line)
                        SQL.ExecNonQuery(insertSQL)
                        line += 1
                    End If
                Next
                insertSQL = " INSERT INTO " & _
                                   " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, dateCleared, LineNumber) " & _
                                   " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @dateCleared, @LineNumber)"
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JVJETransiD)
                SQL.AddParam("@AccntCode", txtAccountCode.Text)
                SQL.AddParam("@VCECode", "")
                If IsNumeric(txtJVadjustment.Text) AndAlso CDec(txtJVadjustment.Text) > 0 Then
                    SQL.AddParam("@Debit", CDec(txtJVadjustment.Text))
                Else
                    SQL.AddParam("@Debit", 0)
                End If
                If IsNumeric(txtJVadjustment.Text) AndAlso CDec(txtJVadjustment.Text) < 0 Then
                    SQL.AddParam("@Credit", Math.Abs(CDec(txtJVadjustment.Text)))
                Else
                    SQL.AddParam("@Credit", 0)
                End If
                SQL.AddParam("@Particulars", "Bank Recon. Item")
                SQL.AddParam("@RefNo", "BR:" & txtTransNum.Text)
                SQL.AddParam("@dateCleared", dtpDocDate.Value.Date)
                SQL.AddParam("@LineNumber", line)
                SQL.ExecNonQuery(insertSQL)
            End If

            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE FROM BANK RECON", "JV_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub UpdateBR()
        Dim SQL As New SQLControl
        SQL.BeginTransaction()
        Try
            activityStatus = True
            Dim updateSQL As String
            updateSQL = " UPDATE    tblBR  " & _
                        " SET       TransID = @TransID, BR_No = @BR_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, DateBR = @DateBR, " & _
                        "           BankID = @BankID, BookEndBal = @BookEndBal, BookAdjBal = @BookAdjBal, BankEndBal = @BankEndBal, BankAdjBal = @BankAdjBal, " & _
                        "           OC = @OC, DIT = @DIT, Remarks = @Remarks, DateModified = GETDATE(), WhoModified = @WhoModified " & _
                        " WHERE     TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@BR_No", BRNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateBR", dtpDocDate.Value.Date)
            SQL.AddParam("@BankID", txtBankID.Text)
            If IsNumeric(txtBookBal.Text) Then SQL.AddParam("@BookEndBal", CDec(txtBookBal.Text)) Else SQL.AddParam("@BookEndBal", 0)
            If IsNumeric(txtAdjBookBal.Text) Then SQL.AddParam("@BookAdjBal", CDec(txtAdjBookBal.Text)) Else SQL.AddParam("@BookAdjBal", 0)
            If IsNumeric(txtBankBal.Text) Then SQL.AddParam("@BankEndBal", CDec(txtBankBal.Text)) Else SQL.AddParam("@BankEndBal", 0)
            If IsNumeric(txtAdjBankBal.Text) Then SQL.AddParam("@BankAdjBal", CDec(txtAdjBankBal.Text)) Else SQL.AddParam("@BankAdjBal", 0)
            If IsNumeric(txtOC.Text) Then SQL.AddParam("@OC", CDec(txtOC.Text)) Else SQL.AddParam("@OC", 0)
            If IsNumeric(txtDIT.Text) Then SQL.AddParam("@DIT", CDec(txtDIT.Text)) Else SQL.AddParam("@DIT", 0)
            SQL.AddParam("@Status", "Active")
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)

            If JVJETransiD <> "" Then
                Dim adjCIB As Decimal
                If IsNumeric(txtJVadjustment.Text) Then adjCIB = CDec(txtJVadjustment.Text) Else adjCIB = 0

                If dgvJV.Rows.Count <= 1 And adjCIB = 0 Then
                    UpdateJV(True)
                Else
                    UpdateJV(False)
                End If
            End If

            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "BR_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("BR_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)
            dtpDocDate.Enabled = False
            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbRefresh.Enabled = True
            tsbCancel.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
            tsbPrint.Enabled = False
        End If
    End Sub

    Private Sub txtBankBal_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtBankBal.TextChanged
        LoadAdjustedBankBalance()
        ComputeVariance()
    End Sub

    Private Sub btnAdjType_Click(sender As System.Object, e As System.EventArgs) Handles btnAdjType.Click
        If Not AllowAccess("BR_MNT") Then
            msgRestricted()
        Else
            Dim f As New frmBR_Type
            f.ShowDialog()
            f.Dispose()
            RefreshBRType()
        End If
    End Sub

    Private Sub RefreshBRType()
        Dim query As String
        query = " SELECT Type FROM tblBR_Type WHERE Status ='Active' ORDER BY SortID "
        SQL.ReadQuery(query)
        cbBankReconType.Items.Clear()
        While SQL.SQLDR.Read
            cbBankReconType.Items.Add(SQL.SQLDR("Type").ToString)
        End While
    End Sub

    Private Sub cbBankReconType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbBankReconType.SelectedIndexChanged
        If cbBankReconType.SelectedIndex <> -1 Then
            Dim query As String
            query = " SELECT    DefaultAccount, accountTitle, " & _
                    "           CASE WHEN Nature ='Add' THEN DefaultAmount ELSE DefaultAmount * -1 END AS Amount, Type " & _
                    " FROM      tblBR_Type INNER JOIN tblCOA_Master " & _
                    " ON        tblBR_Type.DefaultAccount = tblCOA_Master.accountCode " & _
                    " WHERE     Type = @Type AND tblBR_Type.Status ='Active' AND tblCOA_Master.Status ='Active' "
            SQL.FlushParams()
            SQL.AddParam("@Type", cbBankReconType.SelectedItem)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                dgvJV.Rows.Add(SQL.SQLDR("Type").ToString,
                              CDec(SQL.SQLDR("Amount")).ToString("N2"),
                                SQL.SQLDR("DefaultAccount").ToString, SQL.SQLDR("accountTitle").ToString, "", "")
            End If
            SQL.FlushParams()
            LoadJVAdj()
            LoadAdjustedBookBalance()
            ComputeVariance()
        End If
    End Sub

    Private Sub LoadJVAdj()
        Dim total, amount As Decimal
        For Each row As DataGridViewRow In dgvJV.Rows
            If IsNumeric(row.Cells(dgcJVamount.Index).Value) Then amount = CDec(row.Cells(dgcJVamount.Index).Value) Else amount = 0
            total += amount
        Next
        txtJVadjustment.Text = total.ToString("N2")
    End Sub

    Private Sub dtpDocDate_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpDocDate.ValueChanged
        If disableEvent = False Then
            RefreshData()
        End If
    End Sub


    Private Sub frmBR_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        If Not AllowAccess("BR_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("BR")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadBR(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub dgvJV_RowsRemoved(sender As System.Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgvJV.RowsRemoved
        If disableEvent = False Then
            LoadJVAdj()
            LoadAdjustedBookBalance()
            ComputeVariance()
        End If

    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("BR_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If txtStatus.Text <> "Cancelled" AndAlso MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim updateSQL As String
                        updateSQL = " UPDATE  tblBR SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        SQL.AddParam("@TransID", TransID)
                        SQL.ExecNonQuery(updateSQL)

                        updateSQL = " UPDATE  tblJE_Header SET Status ='Cancelled' WHERE RefTransID = @RefTransID  AND RefType ='BR' "
                        SQL.FlushParams()
                        SQL.AddParam("@RefTransID", TransID)
                        SQL.ExecNonQuery(updateSQL)

                        Dim insertSQL As String
                        insertSQL = " INSERT INTO tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, OtherRef) " & _
                                    " SELECT    JE_No, AccntCode, VCECode, Credit, Debit, Particulars, RefNo, LineNumber, OtherRef " & _
                                    " FROM      tblJE_Details " & _
                                    " WHERE     JE_No IN (SELECT DISTINCT JE_No FROM tblJE_Header WHERE RefType ='BR' AND RefTransID ='" & TransID & "') "
                        SQL.ExecNonQuery(insertSQL)
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

                        BRNo = txtTransNum.Text
                        LoadBR(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "BR_No", BRNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                ElseIf txtStatus.Text = "Cancelled" AndAlso MsgBox("Are you sure you want to un-cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim updateSQL As String
                        updateSQL = " UPDATE  tblBR SET Status ='Active' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        SQL.AddParam("@TransID", TransID)
                        SQL.ExecNonQuery(updateSQL)

                        updateSQL = " UPDATE  tblJE_Header SET Status ='Saved' WHERE RefTransID = @RefTransID  AND RefType ='BR' "
                        SQL.FlushParams()
                        SQL.AddParam("@RefTransID", TransID)
                        SQL.ExecNonQuery(updateSQL)

                        Dim insertSQL As String
                        insertSQL = " INSERT INTO tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, OtherRef) " & _
                                    " SELECT    JE_No, AccntCode, VCECode, Credit, Debit, Particulars, RefNo, LineNumber, OtherRef " & _
                                    " FROM      tblJE_Details " & _
                                    " WHERE     JE_No IN (SELECT DISTINCT JE_No FROM tblJE_Header WHERE RefType ='BR' AND RefTransID ='" & TransID & "') "
                        SQL.ExecNonQuery(insertSQL)
                        Msg("Record un-cancelled successfully", MsgBoxStyle.Information)

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

                        BRNo = txtTransNum.Text
                        LoadBR(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "UN-CANCEL", "BR_No", BRNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("BR", TransID)
        f.Dispose()
    End Sub

    Private Sub LoadClearedDIT()
        Try
            'If dtpDocDate.Value.Date >= dtpFrom.Value.Date Then
            '    Dim query As String
            '    query = " SELECT    ID, AppDate, RefTransID, RefType, TransNo, Check_No, VCEName, Credit - Debit AS Amount , BusinessType " &
            '            " FROM      View_BankRecon " &
            '            " WHERE     dateCleared is not NULL " &
            '            " AND      (dateCleared IS NOT NULL OR dateCleared  >'" & dtpDocDate.Value.Date & "') " &
            '            " ORDER BY AppDate"
            '    SQL.ReadQuery(query)
            'Else
            Dim query As String
            If isClearingEnabled Then
                query = " SELECT    ID, AppDate, RefTransID, RefType, TransNo, Check_No, VCEName, CASE WHEN Book = 'Cash Disbursements' THEN Credit - Debit ELSE Debit - Credit END AS Amount , BusinessCode, 'GL' AS Type " & vbCrLf &
                        " FROM      view_GL" & vbCrLf &
                        " WHERE     AccntCode = '" & txtAccountCode.Text & "' AND dateCleared is not NULL  " & vbCrLf &
                        " AND       dateCleared = '" & dtpDocDate.Value.Date & "' " & vbCrLf &
                        " UNION ALL " & vbCrLf &
                        " SELECT    ID, AppDate, TransID, RefType, TransNo, Check_No, VCECode,  Amount , BusinessType, RefType  " & vbCrLf &
                        " FROM      tblOCDIT_Details " & vbCrLf &
                        " WHERE     AccntCode = '" & txtAccountCode.Text & "' AND dateCleared is not NULL " & vbCrLf &
                        " AND       dateCleared = '" & dtpDocDate.Value.Date & "' "
            Else
                query = " SELECT    ID, AppDate, RefTransID, RefType, TransNo, Check_No, VCEName, CASE WHEN Book = 'Cash Disbursements' THEN Credit - Debit ELSE Debit - Credit END AS Amount , BusinessCode, 'GL' AS Type " & vbCrLf &
                        " FROM      view_GL " & vbCrLf &
                        " WHERE     AccntCode = '" & txtAccountCode.Text & "' AND dateCleared is not NULL  " & vbCrLf &
                        " AND       dateCleared = '" & dtpDocDate.Value.Date & "' " & vbCrLf &
                        " UNION ALL " & vbCrLf &
                        " SELECT    ID, AppDate, TransID, RefType, TransNo, Check_No, VCECode,  Amount , BusinessType, RefType  " & vbCrLf &
                        " FROM      tblOCDIT_Details " & vbCrLf &
                        " WHERE     AccntCode = '" & txtAccountCode.Text & "' AND dateCleared is not NULL " & vbCrLf &
                        " AND       dateCleared = '" & dtpDocDate.Value.Date & "' "
            End If

            SQL.ReadQuery(query)

            dgvCleared.Rows.Clear()
            Dim total As Decimal = 0
            If SQL.SQLDR.HasRows Then
                While SQL.SQLDR.Read
                    dgvCleared.Rows.Add(False, SQL.SQLDR("ID"), CDate(SQL.SQLDR("AppDate")).Date, SQL.SQLDR("RefTransID").ToString, SQL.SQLDR("RefType").ToString, SQL.SQLDR("TransNo"),
                                   CDec(SQL.SQLDR("Amount")).ToString("N2"), SQL.SQLDR("Check_No").ToString, SQL.SQLDR("VCEName").ToString, SQL.SQLDR("Type").ToString)
                    total += SQL.SQLDR("Amount")
                End While
            Else
                dgvCleared.Rows.Clear()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub btnReturnCheck_Click(sender As System.Object, e As System.EventArgs) Handles btnReturnCheck.Click
        Dim updateSQL As String
        Dim withcheck As Boolean = False
        Try
            For i As Integer = 0 To dgvCleared.Rows.Count - 1
                Dim Post As Boolean
                Post = dgvCleared.Item(0, i).Value
                If Post Then
                    withcheck = True
                    Exit For
                End If
            Next
            If withcheck = False Then
                Msg("No selected transactions to be cleared!  " & vbNewLine & "Please mark check for those items to be cleared", MsgBoxStyle.Exclamation)
            Else
                For i As Integer = 0 To dgvCleared.Rows.Count - 1
                    Dim Post As Boolean
                    Post = dgvCleared.Item(0, i).Value
                    If Post Then

                        If dgvCleared.Item(dgcClearType.Index, i).Value = "OC" Then
                            updateSQL = " UPDATE tblOCDIT_Details " &
                                       " SET    dateCleared = NULL " &
                                       " WHERE  ID = '" & dgvCleared.Item(dgcClearID.Index, i).Value & "'  AND RefType = 'OC'   "
                            SQL.ExecNonQuery(updateSQL)
                        ElseIf dgvCleared.Item(dgcClearType.Index, i).Value = "DIT" Then
                            updateSQL = " UPDATE tblOCDIT_Details " &
                                           " SET    dateCleared = NULL " &
                                           " WHERE  ID = '" & dgvCleared.Item(dgcClearID.Index, i).Value & "' AND RefType = 'DIT' "
                            SQL.ExecNonQuery(updateSQL)
                        Else
                            updateSQL = " UPDATE view_GL " &
                                         " SET    dateCleared = NULL " &
                                         " WHERE  ID = '" & dgvCleared.Item(dgcClearID.Index, i).Value & "'  "
                            SQL.ExecNonQuery(updateSQL)
                        End If
                    End If
                Next
            End If
            RefreshData()
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub dgvCleared_CellContentClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCleared.CellContentClick
        Try

            Dim rowIndex As Integer = dgvCleared.CurrentCell.RowIndex
            Dim colIndex As Integer = dgvCleared.CurrentCell.ColumnIndex
            If dgvCleared.Item(0, rowIndex).Value Then
                dgvCleared.Item(0, rowIndex).Value = False
            Else
                dgvCleared.Item(0, rowIndex).Value = True
            End If
            computeClearedsubtotal()
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub computeClearedsubtotal()
        Dim subtotal As Decimal
        Try
            subtotal = 0
            For i As Integer = 0 To dgvCleared.Rows.Count - 1
                Dim checked As Boolean
                checked = dgvCleared.Item(0, i).Value
                If checked AndAlso IsNumeric(dgvCleared.Item(dgcClearAmount.Index, i).Value) Then
                    subtotal = CDec(dgvCleared.Item(dgcClearAmount.Index, i).Value) + subtotal
                End If
            Next
            txtClearedAmount.Text = Format(subtotal, "#,###,###,###.00").ToString()
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub chkClear_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkClear.CheckedChanged
        Try
            Dim a As Double = 0
            For i As Integer = 0 To dgvCleared.Rows.Count - 1
                dgvCleared.Item(0, i).Value = chkClear.Checked
            Next
            computeClearedsubtotal()
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub tsbRefresh_Click(sender As System.Object, e As System.EventArgs) Handles tsbRefresh.Click
        If disableEvent = False Then
            If cbBank.SelectedIndex <> -1 Then
                RefreshData()
            Else
                Msg("Please select bank.", MsgBoxStyle.Information)
            End If
        End If
    End Sub
End Class
