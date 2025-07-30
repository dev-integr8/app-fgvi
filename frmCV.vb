Imports Excel = Microsoft.Office.Interop.Excel
Public Class frmCV
    Dim TransID, RefID, RR_RefID, JETransiD As String
    Dim CVNo As String
    Dim disableEvent As Boolean = False
    Dim bankEvent As Boolean = False
    Dim ModuleID As String = "CV"
    Dim ColumnPK As String = "CV_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblCV"
    Dim TransAuto As Boolean
    Dim ForApproval As Boolean = False
    Dim AccntCode As String
    Dim APV_ID, ADV_ID, RFP_ID, LOAN_ID, PCV_ID, CA_ID, BM_ID, TP_ID As Integer
    Dim bankID As Integer
    Dim PCV, TP_Ref As String
    Dim tpHidden As New Dictionary(Of String, System.Windows.Forms.TabPage)
    Dim tpOrder As New List(Of String)
    Dim accntInputVAT, accntTitleInputVAT As String
    Dim isClearingEnabled As Boolean = False
    Dim isReversalEntry As Boolean = False


    Dim transactionCleared As Boolean = False
    Dim accountCleared As String = ""
    Dim dateCleared As String = ""


    Dim Valid As Boolean = True
    Dim InvalidTemplate As Boolean = False
    Dim path As String
    Dim templateName As String = "TEMPLATE"
    Public excelPW As String = "@dm1nEvo"


    ' SETUP VARIABLES
    Dim pendingAPsetup As Boolean
    Dim accntAPpending As String

    'Check Convertion to Text File
    Dim Dir As String
    Dim App_Path As String
    Dim BnkAcctNo As String


    Public Overloads Function ShowDialog(ByVal DocNumber As String) As Boolean
        TransID = DocNumber
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub Disbursement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Text = "(" & database & ") - Check Voucher "
            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            isReversalEntry = GetTransisReversal(ModuleID)
            LoadBankList()
            LoadMultipleBank()
            LoadDisbursementType()
            LoadSetup()
            LoadCostCenter()

            'QRCode Printing
            App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName




            If cbPaymentType.Items.Count > 0 Then
                cbPaymentType.SelectedIndex = 0
            End If

            If TransID <> "" Then
                If Not AllowAccess("CV_VIEW") Then
                    msgRestricted()
                    dtpDocDate.Value = Date.Today.Date
                    tsbOption.Enabled = False
                    tsbSearch.Enabled = True
                    tsbNew.Enabled = True
                    tsbEdit.Enabled = False
                    tsbSave.Enabled = False
                    tsbCancel.Enabled = False
                    tsbDelete.Enabled = False
                    tsbClose.Enabled = False
                    tsbPrevious.Enabled = False
                    tsbNext.Enabled = False
                    tsbExit.Enabled = True
                    tsbPrint.Enabled = False
                    tsbCopy.Enabled = False
                    EnableControl(False)
                Else
                    LoadCV(TransID)
                End If
            Else
                dtpDocDate.Value = Date.Today.Date
                tsbOption.Enabled = False
                tsbSearch.Enabled = True
                tsbNew.Enabled = True
                tsbEdit.Enabled = False
                tsbSave.Enabled = False
                tsbCancel.Enabled = False
                tsbDelete.Enabled = False
                tsbClose.Enabled = False
                tsbPrevious.Enabled = False
                tsbNext.Enabled = False
                tsbExit.Enabled = True
                tsbPrint.Enabled = False
                tsbCopy.Enabled = False
                EnableControl(False)
            End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadSetup()
        Dim query As String
        query = " SELECT  ISNULL(AP_SetupPendingAP,0) AS AP_SetupPendingAP, AP_Pending, TAX_IV, AccountTitle, ISNULL(CIB_Clearing ,0) AS CIB_Clearing  FROM tblSystemSetup " &
                " INNER JOIN tblCOA_Master ON " &
                " tblCOA_Master.AccountCode = tblSystemSetup.TAX_IV"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            accntInputVAT = SQL.SQLDR("TAX_IV").ToString
            accntTitleInputVAT = SQL.SQLDR("AccountTitle").ToString
            pendingAPsetup = SQL.SQLDR("AP_SetupPendingAP")
            accntAPpending = SQL.SQLDR("AP_Pending").ToString
            isClearingEnabled = SQL.SQLDR("CIB_Clearing").ToString
        End If
    End Sub

    Private Sub LoadDisbursementType()
        Dim query As String
        query = " SELECT DISTINCT Expense_Description FROM tblCV_ExpenseType WHERE Status ='Active' "
        SQL.ReadQuery(query)
        cbDisburseType.Items.Clear()
        While SQL.SQLDR.Read
            cbDisburseType.Items.Add(SQL.SQLDR("Expense_Description").ToString)
        End While
    End Sub

    Private Sub LoadMultipleBank()
        Try
            Dim dgvMultiBank As New DataGridViewComboBoxColumn
            dgvMultiBank = dgvMultipleCheck.Columns(dgcBank.Index)
            dgvMultiBank.Items.Clear()
            ' ADD ALL BranchCode
            Dim query As String
            query = " SELECT  CAST(BankID AS nvarchar) + '-' + Bank + ' ' + Branch + " & _
                 "         CASE WHEN AccountNo <> '' THEN ' (' + AccountNo  +  ')' ELSE '' END AS Bank " & _
                 " FROM    tblBank_Master " & _
                 " WHERE   Status ='Active'"
            SQL.ReadQuery(query)
            dgvMultiBank.Items.Clear()
            While SQL.SQLDR.Read
                dgvMultiBank.Items.Add(SQL.SQLDR("Bank").ToString)
            End While
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)

        dtpDocDate.Enabled = Value
        txtVCEName.Enabled = Value
        btnSearchVCE.Enabled = Value
        'dgvEntry.Enabled = Value
        dgvEntry.AllowUserToAddRows = Value
        dgvEntry.AllowUserToDeleteRows = Value
        If Value = True Then
            dgvEntry.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
            LoadBranch()
        Else
            dgvEntry.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        txtRemarks.Enabled = Value
        txtAmount.Enabled = Value
        cbCurrency.Enabled = Value
        txtConversion.Enabled = Value
        txtORNo.Enabled = Value
        cbPaymentType.Enabled = Value
        cbDisburseType.Enabled = Value
        cbCostCenter.Enabled = Value
        tcPayment.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Private Sub dgvEntry_CellBeginEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvEntry.CellBeginEdit
        eColIndex = e.ColumnIndex
    End Sub

    Private Sub dgvEntry_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dgvEntry.CurrentCellDirtyStateChanged
        If eColIndex = dgcBranchCode.Index And TypeOf (dgvEntry.CurrentRow.Cells(dgcBranchCode.Index)) Is DataGridViewComboBoxCell Then
            dgvEntry.EndEdit()
        End If
    End Sub

    Private Sub dgvEntry_DataError(sender As System.Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvEntry.DataError
        Try

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadCV(ByVal ID As String)
        If dgvEntry.ColumnCount >= 11 AndAlso dgvEntry.Columns(10).HeaderText = "Balance" Then
            dgvEntry.Columns.Remove("Balance")
        End If
        Dim query, payment_type, Currency, cc As String
        query = " SELECT  TransID, CV_No, PaymentType, tblCV.VCECode, VCEName, DateCV, TotalAmount, Remarks, " &
                "         ISNULL(APV_Ref,0) as APV_Ref, OR_Ref, ISNULL(LN_Ref,0) as LN_Ref, ISNULL(CA_Ref,0) as CA_Ref, tblCV.Status, Currency, ISNULL(Exchange_Rate,0) AS Exchange_Rate, CostCenter  " &
                " FROM    tblCV LEFT JOIN viewVCE_Master " &
                " ON      tblCV.VCECode = viewVCE_Master.VCECode " &
                " WHERE   TransID = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            CVNo = SQL.SQLDR("CV_No").ToString
            LOAN_ID = SQL.SQLDR("LN_Ref").ToString
            APV_ID = SQL.SQLDR("APV_Ref").ToString
            CA_ID = SQL.SQLDR("CA_Ref").ToString
            txtTransNum.Text = CVNo
            payment_type = SQL.SQLDR("PaymentType").ToString
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
            disableEvent = True
            dtpDocDate.Value = SQL.SQLDR("DateCV")
            cc = SQL.SQLDR("CostCenter").ToString
            disableEvent = False
            txtAmount.Text = CDec(SQL.SQLDR("TotalAmount")).ToString("N2")
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtORNo.Text = SQL.SQLDR("OR_Ref").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            Currency = SQL.SQLDR("Currency").ToString
            txtConversion.Text = CDec(SQL.SQLDR("Exchange_Rate")).ToString("N4")
            txtLoanRef.Text = GetLoanNo(LOAN_ID)
            txtAPVRef.Text = GetAPVNo(APV_ID)
            txtCARef.Text = GetCANo(CA_ID)
            cbPaymentType.SelectedItem = payment_type
            cbCostCenter.SelectedItem = cc
            If payment_type = "Check" Then
                LoadCVRef(TransID)
            ElseIf payment_type = "Multiple Check" Then
                LoadCVRefMulti(TransID)
            ElseIf payment_type = "Bank Transfer" Then
                LoadCVRef_BankTransfer(TransID)
            ElseIf payment_type = "Debit Memo" Then
                LoadCVRef_DebitMemo(TransID)
            End If
            If cbPaymentType.Text <> "Cash" Then
                BnkAcctNo = GetBankAccountNo(cbBank.SelectedItem)
            End If
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
                LoadEntry(TransID)

                ' TOOLSTRIP BUTTONS
                tsbCancel.Text = "Cancel"
                If txtStatus.Text = "Cancelled" Then
                    tsbEdit.Enabled = False
                    tsbCancel.Enabled = True
                    tsbCancel.Text = "Un-Can"
                    tsbDelete.Enabled = True
                ElseIf txtStatus.Text = "Closed" Then
                    tsbEdit.Enabled = False
                    tsbCancel.Enabled = False
                    tsbDelete.Enabled = False
                Else
                    tsbEdit.Enabled = True
                    tsbCancel.Enabled = True
                    tsbDelete.Enabled = True
                End If
                tsbPrint.Enabled = True
                tsbClose.Enabled = True
                tsbPrevious.Enabled = True
                tsbNext.Enabled = True
                tsbPrint.Enabled = True
                tsbSave.Enabled = False
                tsbNew.Enabled = True
                tsbSearch.Enabled = True
                tsbExit.Enabled = True
                tsbCopy.Enabled = False
                tsbOption.Enabled = True
                If dtpDocDate.Value <= GetMaxPEC() Then
                    tsbEdit.Enabled = False
                    tsbCancel.Enabled = False
                    tsbDelete.Enabled = False
                End If
                EnableControl(False)
            Else
                ClearText()
        End If
    End Sub

    Private Sub LoadCVRef(ByVal CVNo As Integer)
        Dim query As String
        query = " SELECT CAST(tblBank_Master.BankID AS nvarchar) + '-' + Bank + ' ' + Branch + CASE WHEN AccountNo <> '' THEN ' (' + AccountNo  +  ')' ELSE '' END AS Bank, " & _
                " RefNo, RefDate, RefAmount, viewCV_BankRef_Status.Status, RefName " & _
                " FROM viewCV_BankRef_Status INNER JOIN tblBank_Master " & _
                " ON viewCV_BankRef_Status.BankID = tblBank_Master.BankID " & _
                " WHERE CV_No ='" & CVNo & "' AND viewCV_BankRef_Status.Status <> 'Cancelled' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            disableEvent = True
            gbBank.Visible = True
            txtBankRef.Text = SQL.SQLDR("RefNo").ToString
            dtpBankRefDate.Value = SQL.SQLDR("RefDate")
            txtBankRefAmount.Text = CDec(SQL.SQLDR("RefAmount")).ToString("N2")
            cbBank.SelectedItem = SQL.SQLDR("Bank").ToString
            txtRefStatus.Text = SQL.SQLDR("Status").ToString
            txtBankRefName.Text = SQL.SQLDR("RefName").ToString
            bankID = GetBankID(cbBank.SelectedItem)
            disableEvent = False
        ElseIf cbPaymentType.SelectedItem = "Check" Then
            gbBank.Visible = True
            txtBankRef.Text = ""
            dtpBankRefDate.Value = dtpDocDate.Value.Date
            txtBankRefAmount.Text = CDec(txtAmount.Text)
            cbBank.SelectedIndex = -1
        Else
            gbBank.Visible = False
        End If
    End Sub

    Private Sub LoadCVRefMulti(ByVal CVNo As Integer)
        Dim query As String
        query = " SELECT tblBank_Master.BankID, CAST(tblBank_Master.BankID AS nvarchar) + '-' + Bank + ' ' + Branch + CASE WHEN AccountNo <> '' THEN ' (' + AccountNo  +  ')' ELSE '' END AS Bank, " & _
                " RefNo, RefDate, RefAmount, viewCV_BankRef_Status.Status, RefName , ISNULL(RefVCECode,'') as RefVCECode" & _
                " FROM viewCV_BankRef_Status INNER JOIN tblBank_Master " & _
                " ON viewCV_BankRef_Status.BankID = tblBank_Master.BankID " & _
                " WHERE CV_No ='" & CVNo & "' AND viewCV_BankRef_Status.Status <> 'Cancelled' "
        SQL.ReadQuery(query)

        dgvMultipleCheck.Rows.Clear()
        While SQL.SQLDR.Read
            dgvMultipleCheck.Rows.Add(SQL.SQLDR("BankID").ToString, SQL.SQLDR("Bank").ToString, SQL.SQLDR("RefNo").ToString, SQL.SQLDR("RefDate").ToString, CDec(SQL.SQLDR("RefAmount")).ToString("N2"), _
                               SQL.SQLDR("RefVCECode").ToString, SQL.SQLDR("RefName").ToString, SQL.SQLDR("Status").ToString)
        End While
    End Sub

    Private Sub LoadCVRef_BankTransfer(ByVal CVNo As Integer)
        Dim query As String
        query = " SELECT CAST(tblBank_Master.BankID AS nvarchar) + '-' + Bank + ' ' + Branch + CASE WHEN AccountNo <> '' THEN ' (' + AccountNo  +  ')' ELSE '' END AS Bank, " & _
                " RefNo, RefDate, RefAmount, tblCV_BankRef.Status, RefName, DestinationBank, DestinationBankAccount " & _
                " FROM tblCV_BankRef INNER JOIN tblBank_Master " & _
                " ON tblCV_BankRef.BankID = tblBank_Master.BankID " & _
                " WHERE CV_No ='" & CVNo & "' AND tblCV_BankRef.Status <> 'Cancelled' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            disableEvent = True
            gbBank.Visible = True
            txtBankTransfer_Ref.Text = SQL.SQLDR("RefNo").ToString
            dtpBankTransfer_Date.Value = SQL.SQLDR("RefDate")
            txtBankTransfer_Amount.Text = CDec(SQL.SQLDR("RefAmount")).ToString("N2")
            txtBankTransfer_VCEBankName.Text = SQL.SQLDR("DestinationBank").ToString
            txtBankTransfer_VCEBankAccount.Text = SQL.SQLDR("DestinationBankAccount").ToString
            cbBankTransfer_Bank.SelectedItem = SQL.SQLDR("Bank").ToString
            bankID = GetBankID(cbBankTransfer_Bank.SelectedItem)
            disableEvent = False
        End If
    End Sub

    Private Sub LoadCVRef_DebitMemo(ByVal CVNo As Integer)
        Dim query As String
        query = " SELECT CAST(tblBank_Master.BankID AS nvarchar) + '-' + Bank + ' ' + Branch + CASE WHEN AccountNo <> '' THEN ' (' + AccountNo  +  ')' ELSE '' END AS Bank, " & _
                " RefNo " & _
                " FROM tblCV_BankRef INNER JOIN tblBank_Master " & _
                " ON tblCV_BankRef.BankID = tblBank_Master.BankID " & _
                " WHERE CV_No ='" & CVNo & "' AND tblCV_BankRef.Status <> 'Cancelled' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            disableEvent = True
            gbBank.Visible = True
            txtDMRef.Text = SQL.SQLDR("RefNo").ToString
            cbBankDM.SelectedItem = SQL.SQLDR("Bank").ToString
            bankID = GetBankID(cbBankDM.SelectedItem)
            disableEvent = False
        End If
    End Sub

    Private Sub LoadEntry(ByVal CVNo As Integer)
        Dim query As String

        dateCleared = ""
        transactionCleared = False
        accountCleared = ""

        query = " SELECT ID, JE_No, View_GL_Transaction.BranchCode, View_GL_Transaction.AccntCode, AccountTitle, View_GL_Transaction.VCECode, View_GL_Transaction.VCEName, Debit, Credit, Particulars, RefNo, CostCenter , ProfitCenter, VATType, dateCleared, ATC_Code  " & _
                " FROM   View_GL_Transaction INNER JOIN tblCOA_Master " & _
                " ON     View_GL_Transaction.AccntCode = tblCOA_Master.AccountCode " & _
                " WHERE JE_No = (SELECT  JE_No FROM tblJE_Header WHERE RefType = 'CV' AND RefTransID = " & CVNo & " AND isUpload <> 1) " & _
                " ORDER BY LineNumber "
        SQL.ReadQuery(query)
        dgvEntry.Rows.Clear()

        Dim rowsCount As Integer = 0
        If SQL.SQLDR.HasRows Then
            While SQL.SQLDR.Read
                JETransiD = SQL.SQLDR("JE_No").ToString
                dgvEntry.Rows.Add(SQL.SQLDR("AccntCode").ToString)
                dgvEntry.Rows(rowsCount).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                dgvEntry.Rows(rowsCount).Cells(chDebit.Index).Value = IIf(CDec(SQL.SQLDR("Debit")).ToString("N2") = 0, "", CDec(SQL.SQLDR("Debit")).ToString("N2"))
                dgvEntry.Rows(rowsCount).Cells(chCredit.Index).Value = IIf(CDec(SQL.SQLDR("Credit")).ToString("N2") = 0, "", CDec(SQL.SQLDR("Credit")).ToString("N2"))
                dgvEntry.Rows(rowsCount).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                dgvEntry.Rows(rowsCount).Cells(chVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                dgvEntry.Rows(rowsCount).Cells(chParticulars.Index).Value = SQL.SQLDR("Particulars").ToString
                dgvEntry.Rows(rowsCount).Cells(chRef.Index).Value = SQL.SQLDR("RefNo").ToString
                dgvEntry.Rows(rowsCount).Cells(chVATType.Index).Value = SQL.SQLDR("VATType").ToString
                dgvEntry.Rows(rowsCount).Cells(chATCCode.Index).Value = SQL.SQLDR("ATC_Code").ToString
                dgvEntry.Rows(rowsCount).Cells(dgcBranchCode.Index).Value = SQL.SQLDR("BranchCode").ToString
                dgvEntry.Rows(rowsCount).Cells(chCostID.Index).Value = SQL.SQLDR("CostCenter").ToString
                dgvEntry.Rows(rowsCount).Cells(chCost_Center.Index).Value = GetCCName(SQL.SQLDR("CostCenter").ToString)
                dgvEntry.Rows(rowsCount).Cells(chProfit_Code.Index).Value = SQL.SQLDR("ProfitCenter").ToString
                dgvEntry.Rows(rowsCount).Cells(chProfit_Center.Index).Value = GetPCName(SQL.SQLDR("ProfitCenter").ToString)

                 

                If SQL.SQLDR("dateCleared").ToString <> "" Then
                    dateCleared = SQL.SQLDR("dateCleared").ToString
                    transactionCleared = True
                    accountCleared = SQL.SQLDR("AccntCode").ToString
                End If


                    rowsCount += 1
            End While

            LoadBranch()
            TotalDBCR()
        Else
            JETransiD = 0
            dgvEntry.Rows.Clear()
        End If
    End Sub

    Private Sub LoadBankList()
        Dim query As String
        query = " SELECT  CAST(BankID AS nvarchar) + '-' + Bank + ' ' + Branch + " & _
                "         CASE WHEN AccountNo <> '' THEN ' (' + AccountNo  +  ')' ELSE '' END AS Bank " & _
                " FROM    tblBank_Master " & _
                " WHERE   Status ='Active'"
        SQL.ReadQuery(query)
        cbBank.Items.Clear()
        While SQL.SQLDR.Read
            cbBank.Items.Add(SQL.SQLDR("Bank").ToString)
            cbBankTransfer_Bank.Items.Add(SQL.SQLDR("Bank").ToString)
            cbBankDM.Items.Add(SQL.SQLDR("Bank").ToString)
            cbBankMC.Items.Add(SQL.SQLDR("Bank").ToString)
        End While
    End Sub

    Private Sub LoadDisburseType()
        Dim query As String
        query = " SELECT  DISTINCT ISNULL(Expense_Description,'') AS Expense_Description " & _
                " FROM    tblCV_ExpenseType " & _
                " WHERE   Status ='Active' "
        SQL.ReadQuery(query)
        cbDisburseType.Items.Clear()
        While SQL.SQLDR.Read
            cbDisburseType.Items.Add(SQL.SQLDR("Expense_Description").ToString)
        End While
    End Sub


    Private Sub cbPaymentType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbPaymentType.SelectedIndexChanged
        If disableEvent = False Then
            tcPayment.Visible = True
            HideTabPageAll()
            ShowTabPage(cbPaymentType.SelectedItem)
            If cbPaymentType.SelectedItem = "Check" Then
                If cbBank.SelectedIndex <> -1 Then bankID = GetBankID(cbBank.SelectedItem)
                tcPayment.SelectedTab = tpCheck
                bankEvent = True
            ElseIf cbPaymentType.SelectedItem = "Multiple Check" Then
                tcPayment.SelectedTab = tpMultipleCheck
                bankEvent = True
            ElseIf cbPaymentType.SelectedItem = "Manager's Check" Then
                If cbBankMC.SelectedIndex <> -1 Then bankID = GetBankID(cbBankMC.SelectedItem)
                tcPayment.SelectedTab = tpMC
                bankEvent = False
            ElseIf cbPaymentType.SelectedItem = "Debit Memo" Then
                If cbBankDM.SelectedIndex <> -1 Then bankID = GetBankID(cbBankDM.SelectedItem)
                tcPayment.SelectedTab = tpDebitMemo
                bankEvent = False
            ElseIf cbPaymentType.SelectedItem = "Bank Transfer" Then
                If cbBankTransfer_Bank.SelectedIndex <> -1 Then bankID = GetBankID(cbBankTransfer_Bank.SelectedItem)
                tcPayment.SelectedTab = tpBankTransfer
                bankEvent = True
            ElseIf cbPaymentType.SelectedItem = "(Multiple Payment Method)" Then
                tcPayment.SelectedTab = tpCheck
                bankEvent = False
                ShowTabPageAll()
            End If
            'bankID = 0
            'LoadBankDetails()
            'If cbBank.Items.Count = 1 Then
            '    cbBank.SelectedIndex = 0
            '    bankID = GetBankID(cbBank.SelectedItem)
            '    If cbPaymentType.SelectedItem = "Check" Then
            '        txtBankRef.Text = GetCheckNo(bankID)
            '    End If
            'End If
        End If
    End Sub



    Private Sub LoadBankDetails()
        txtBankRef.Text = ""
        Dim query, prefix As String
        query = " SELECT  Payment_Type, WithBank, WithCheck, RefNo_Prefix, Account_Code " & _
                " FROM    tblCV_PaymentType " & _
                " WHERE   Payment_Type = @Payment_Type "
        SQL.FlushParams()
        SQL.AddParam("Payment_Type", cbPaymentType.SelectedItem)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            AccntCode = SQL.SQLDR("Account_Code").ToString
            gbBank.Visible = SQL.SQLDR("WithBank")
            If SQL.SQLDR("WithBank") Then
                prefix = SQL.SQLDR("RefNo_Prefix").ToString
                If Not SQL.SQLDR("WithCheck") Then
                    txtBankRef.Text = GetBankRef(prefix)
                End If
            End If
        End If
    End Sub

    Private Sub cbCategory_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        Try
            LoadDisburseType()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function GetBankRef(ByVal Prefix As String) As String
        Dim query As String
        query = " SELECT  (ISNULL(MAX(CAST(REPLACE(RefNo,'" & Prefix & "','') AS int)),0) + 1) AS RefNo " & _
                " FROM    tblCV_Bankref " & _
                " WHERE   RefNo LIKE '" & Prefix & "%' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return Prefix + SQL.SQLDR("RefNo").ToString
        Else
            Return Prefix + "1"
        End If
    End Function

    Private Sub cbBank_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbBank.SelectedIndexChanged
        Try
            If disableEvent = False Then
                If cbBank.SelectedIndex <> -1 Then
                    bankID = GetBankID(cbBank.SelectedItem)
                    'BnkAcctNo = GetBankAccountNo(cbBank.SelectedItem)

                    If cbPaymentType.SelectedItem = "Check" Then
                        txtBankRef.Text = GetCheckNo(bankID)
                    End If
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function GetCheckNo(ByVal Bank_ID As String) As String
        Dim query As String
        query = " SELECT  RIGHT('000000000000' + CAST((CAST(MAX(RefNo) AS bigint) + 1) AS nvarchar), LEN(SeriesFrom)) AS RefNo, SeriesFrom " & _
                " FROM    tblCV_BankRef RIGHT JOIN tblBank_Master " & _
                " ON      tblCV_BankRef.BankID = tblBank_Master.BankID " & _
                " AND     tblCV_BankRef.RefNo BETWEEN SeriesFrom AND SeriesTo " & _
                " WHERE   tblBank_Master.BankID = '" & Bank_ID & "'   " & _
                " GROUP BY LEN(SeriesFrom), SeriesFrom  "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read AndAlso Not IsDBNull(SQL.SQLDR("RefNo")) Then
            Return SQL.SQLDR("RefNo").ToString
        Else
            Return SQL.SQLDR("SeriesFrom").ToString
        End If
    End Function

    Private Function GetDMNo(ByVal Bank_ID As String) As String
        Dim query As String
        query = " SELECT  ISNULL(MAX(RefNo),0) + 1 AS RefNo " & _
                " FROM    tblCV INNER JOIN tblCV_BankRef  " & _
                " ON      tblCV_BankRef.CV_No = tblCV.TransID " & _
                " WHERE   PaymentType = 'Debit Memo' AND BankID = '" & Bank_ID & "'   "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read AndAlso Not IsDBNull(SQL.SQLDR("RefNo")) Then
            Return SQL.SQLDR("RefNo").ToString
        Else
            Return 1
        End If
    End Function


    Private Function GetBankAccountNo(ByVal BankAccnt As String) As String
        Dim query As String
        query = " SELECT AccountNo FROM tblBank_Master WHERE BankID = LEFT('" & BankAccnt & "',CHARINDEX('-','" & BankAccnt & "',1)-1) "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("AccountNo")
        Else
            Return ""
        End If
    End Function
    Private Function GetBankID(ByVal Bank As String) As Integer
        Dim query As String
        query = " SELECT BankID FROM tblBank_Master WHERE BankID = LEFT('" & Bank & "',CHARINDEX('-','" & Bank & "',1)-1) "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("BankID")
        Else
            Return 0
        End If
    End Function

    Private Function GetLoanNo(ByVal ID As Integer) As String
        Dim query As String
        query = " SELECT Loan_No FROM tblLoan WHERE TransID = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("Loan_No")
        Else
            Return ""
        End If
    End Function

    Private Function GetAPVNo(ByVal ID As Integer) As String
        Dim query As String
        query = " SELECT APV_No FROM tblAPV WHERE TransID = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("APV_No")
        Else
            Return 0
        End If
    End Function


    Private Function GetCANo(ByVal ID As Integer) As String
        Dim query As String
        query = " SELECT CA_No FROM tblCA WHERE TransID = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("CA_No")
        Else
            Return 0
        End If
    End Function


    Public Sub TotalDBCR()
        Try
            'debit compute & print in textbox
            Dim a As Double = 0
            For i As Integer = 0 To dgvEntry.Rows.Count - 1
                If dgvEntry.Item(chAccntCode.Index, i).Value <> "" AndAlso Val(dgvEntry.Item(chDebit.Index, i).Value) <> 0 Then
                    a = a + Double.Parse(dgvEntry.Item(chDebit.Index, i).Value).ToString("N2")
                End If
            Next
            txtTotalDebit.Text = a.ToString("N2")
            'credit compute & print in textbox
            Dim b As Double = 0
            For i As Integer = 0 To dgvEntry.Rows.Count - 1
                If dgvEntry.Item(chAccntCode.Index, i).Value <> "" AndAlso Val(dgvEntry.Item(chCredit.Index, i).Value) <> 0 Then
                    b = b + Double.Parse(dgvEntry.Item(chCredit.Index, i).Value).ToString("N2")
                End If
            Next
            txtTotalCredit.Text = b.ToString("N2")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub dtpCVDate_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpDocDate.ValueChanged
        dtpBankRefDate.Value = dtpDocDate.Value
        If disableEvent = False Then
            If TransID = "" Then
                txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
            End If
        End If
    End Sub

    Private Sub txtAmount_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtAmount.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If txtAmount.Text = "" Then
                    MsgBox("Please enter an amount", MsgBoxStyle.Exclamation)
                Else
                    txtAmount.Text = CDec(txtAmount.Text).ToString("N2")
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub dgvManual_RowsRemoved(sender As System.Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgvEntry.RowsRemoved
        Try
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub ClearText()
        If dgvEntry.ColumnCount >= 11 AndAlso dgvEntry.Columns(10).HeaderText = "Balance" Then
            dgvEntry.Columns.Remove("Balance")
        End If
        APV_ID = 0
        ADV_ID = 0
        CA_ID = 0
        RFP_ID = 0
        LOAN_ID = 0
        PCV_ID = 0
        RR_RefID = 0
        txtRRRef.Text = ""
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        txtAmount.Text = ""
        txtRemarks.Text = ""
        cbPaymentType.SelectedIndex = 0
        cbBank.SelectedIndex = -1
        txtBankRef.Text = ""
        txtBankRefAmount.Text = ""
        txtBankRefName.Text = ""
        txtTransNum.Text = ""
        txtAPVRef.Text = ""
        txtCARef.Text = ""
        txtLoanRef.Text = ""
        txtORNo.Text = ""
        txtStatus.Text = ""
        dgvEntry.Rows.Clear()
        dgvMultipleCheck.Rows.Clear()
        txtTotalDebit.Text = "0.00"
        txtTotalCredit.Text = "0.00"
        dtpDocDate.MinDate = GetMaxPEC().AddDays(1)
        dtpDocDate.Value = Date.Today.Date
        cbCostCenter.SelectedIndex = 0

        cbBankTransfer_Bank.SelectedIndex = -1
        txtBankTransfer_Amount.Text = ""
        txtBankTransfer_Ref.Text = ""
        txtBankTransfer_VCEBankAccount.Text = ""
        txtBankTransfer_VCEBankName.Text = ""
        cbCurrency.Items.Clear()
        txtConversion.Text = ""
    End Sub

    Private Sub SaveCV()
        Dim SQL1 As New SQLControl
        SQL1.BeginTransaction()
        Try
            Dim insertSQL As String
            activityStatus = True
            insertSQL = " INSERT INTO " &
                        " tblCV (TransID, CV_No, PaymentType, VCECode, DateCV, TotalAmount, Currency, Exchange_Rate, Remarks, " &
                        "       APV_Ref, ADV_Ref, PCV_Ref, LN_Ref, OR_Ref, CA_Ref, RR_Ref, WhoCreated, TransAuto, BranchCode, BusinessCode, Status, CostCenter ) " &
                        " VALUES (@TransID, @CV_No, @PaymentType, @VCECode, @DateCV, @TotalAmount, @Currency, @Exchange_Rate, @Remarks, " &
                        "       @APV_Ref, @ADV_Ref, @PCV_Ref, @LN_Ref, @OR_Ref, @CA_Ref, @RR_Ref, @WhoCreated, @TransAuto, @BranchCode, @BusinessCode, @Status, @CostCenter)"
            SQL1.FlushParams()
            SQL1.AddParam("@TransID", TransID)
            SQL1.AddParam("@CV_No", CVNo)
            SQL1.AddParam("@PaymentType", cbPaymentType.SelectedItem)
            SQL1.AddParam("@VCECode", txtVCECode.Text)
            SQL1.AddParam("@DateCV", dtpDocDate.Value.Date)
            SQL1.AddParam("@TotalAmount", CDec(txtAmount.Text))
            SQL1.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL1.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)))
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@APV_Ref", IIf(txtAPVRef.Text = "", DBNull.Value, APV_ID))
            SQL1.AddParam("@ADV_Ref", IIf(txtADVRef.Text = "", DBNull.Value, ADV_ID))
            SQL1.AddParam("@CA_Ref", IIf(txtCARef.Text = "", DBNull.Value, CA_ID))
            SQL1.AddParam("@RR_Ref", IIf(txtRRRef.Text = "", DBNull.Value, RR_RefID))
            SQL1.AddParam("@PCV_Ref", PCV_ID)
            SQL1.AddParam("@LN_Ref", LOAN_ID)
            SQL1.AddParam("@OR_Ref", txtORNo.Text)
            SQL1.AddParam("@TransAuto", TransAuto)
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@CostCenter", cbCostCenter.SelectedItem)
            If ForApproval = True Then SQL1.AddParam("@Status", "Draft") Else SQL1.AddParam("@Status", "Active")
            SQL1.ExecNonQuery(insertSQL)

            If ADV_ID > 0 Then
                Dim updateSQL As String
                updateSQL = " UPDATE tblADV SET Status ='Closed' WHERE TransID = '" & ADV_ID & "'"
                SQL1.ExecNonQuery(updateSQL)
            End If

            If LOAN_ID > 0 Then
                Dim updateSQL As String
                updateSQL = " UPDATE tblLoan SET Status ='Released', DateRelease = '" & dtpDocDate.Value.Date & "', RefType = 'CV', RefTransID = '" & TransID & "' WHERE TransID = '" & LOAN_ID & "'"
                SQL1.ExecNonQuery(updateSQL)
            End If

            If RFP_ID > 0 Then
                Dim updateSQL As String
                updateSQL = " UPDATE tblRFP SET Status ='Closed' WHERE TransID = '" & RFP_ID & "'"
                SQL1.ExecNonQuery(updateSQL)
            End If

            If CA_ID > 0 Then
                Dim updateSQL As String
                updateSQL = " UPDATE tblCA SET Status ='Released' WHERE TransID = '" & CA_ID & "'"
                SQL1.ExecNonQuery(updateSQL)
            End If
            If bankEvent = True Then
                If cbPaymentType.SelectedItem = "Check" Then
                    SaveCVRef(TransID, SQL1)
                ElseIf cbPaymentType.SelectedItem = "Multiple Check" Then
                    SaveCVMultiRef(TransID, SQL1)
                ElseIf cbPaymentType.SelectedItem = "Bank Transfer" Then
                    SaveCVRef_BankTransfer(TransID, SQL1)
                ElseIf cbPaymentType.SelectedItem = "Debit Memo" Then
                    SaveDMRef(TransID, SQL1)
                End If
            End If

            JETransiD = GenerateTransID("JE_No", "tblJE_Header")

            insertSQL = " INSERT INTO " &
                        " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, " &
                        "               TotalDBCR,  Currency, Exchange_Rate, Remarks, WhoCreated, Status, CostCenter) " &
                        " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, " &
                        "       @TotalDBCR, @Currency, @Exchange_Rate, @Remarks, @WhoCreated, @Status, @CostCenter)"
            SQL1.FlushParams()
            SQL1.AddParam("@JE_No", JETransiD)
            SQL1.AddParam("@AppDate", dtpDocDate.Value.Date)
            SQL1.AddParam("@RefType", "CV")
            SQL1.AddParam("@RefTransID", TransID)
            SQL1.AddParam("@Book", "Cash Disbursements")
            SQL1.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
            SQL1.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL1.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)))
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@CostCenter", cbCostCenter.SelectedItem)
            If ForApproval = True Then SQL1.AddParam("@Status", "Draft") Else SQL1.AddParam("@Status", "Saved")
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.ExecNonQuery(insertSQL)

            ' JETransiD = LoadJE("CV", TransID)

            Dim strRefNo As String = ""
            Dim HeaderCC As String = cbCostCenter.SelectedItem

            Dim line As Integer = 1
            For Each item As DataGridViewRow In dgvEntry.Rows
                If item.Cells(chAccntCode.Index).Value <> Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, CostCenter, BranchCode, ProfitCenter, VATType, ATC_Code) " & _
                                " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @CostCenter, @BranchCode, @ProfitCenter, @VATType, @ATC_Code)"
                    SQL1.FlushParams()
                    SQL1.AddParam("@JE_No", JETransiD)
                    SQL1.AddParam("@AccntCode", item.Cells(chAccntCode.Index).Value.ToString)
                    If item.Cells(chVCECode.Index).Value <> Nothing AndAlso item.Cells(chVCECode.Index).Value <> "" Then
                        SQL1.AddParam("@VCECode", item.Cells(chVCECode.Index).Value.ToString)
                    Else
                        SQL1.AddParam("@VCECode", txtVCECode.Text)
                    End If
                    If item.Cells(chDebit.Index).Value <> Nothing AndAlso IsNumeric(item.Cells(chDebit.Index).Value) Then
                        SQL1.AddParam("@Debit", CDec(item.Cells(chDebit.Index).Value))
                    Else
                        SQL1.AddParam("@Debit", 0)
                    End If
                    If item.Cells(chCredit.Index).Value <> Nothing AndAlso IsNumeric(item.Cells(chCredit.Index).Value) Then
                        SQL1.AddParam("@Credit", CDec(item.Cells(chCredit.Index).Value))
                    Else
                        SQL1.AddParam("@Credit", 0)
                    End If
                    If item.Cells(chParticulars.Index).Value <> Nothing AndAlso item.Cells(chParticulars.Index).Value <> "" Then
                        SQL1.AddParam("@Particulars", item.Cells(chParticulars.Index).Value.ToString)
                    Else
                        SQL1.AddParam("@Particulars", txtRemarks.Text)
                    End If
                    If cbCostCenter.SelectedItem = "" Then
                        If item.Cells(chCostID.Index).Value <> Nothing AndAlso item.Cells(chCostID.Index).Value <> "" Then
                            SQL1.AddParam("@CostCenter", item.Cells(chCostID.Index).Value.ToString)
                        Else
                            SQL1.AddParam("@CostCenter", "")
                        End If
                    Else
                        SQL1.AddParam("@CostCenter", HeaderCC)
                    End If
                    If item.Cells(chProfit_Code.Index).Value <> Nothing AndAlso item.Cells(chProfit_Code.Index).Value <> "" Then
                            SQL1.AddParam("@ProfitCenter", item.Cells(chProfit_Code.Index).Value.ToString)
                        Else
                            SQL1.AddParam("@ProfitCenter", "")
                        End If
                        If item.Cells(chRef.Index).Value <> Nothing AndAlso item.Cells(chRef.Index).Value <> "" Then
                            SQL1.AddParam("@RefNo", item.Cells(chRef.Index).Value.ToString)
                            If strRefNo.Length = 0 Then
                                strRefNo = item.Cells(chRef.Index).Value.ToString
                            Else
                                strRefNo = strRefNo & "-" & item.Cells(chRef.Index).Value.ToString
                            End If
                        Else
                            SQL1.AddParam("@RefNo", "")
                        End If
                        SQL1.AddParam("@LineNumber", line)
                        If item.Cells(dgcBranchCode.Index).Value <> Nothing AndAlso item.Cells(dgcBranchCode.Index).Value <> "" Then
                            SQL1.AddParam("@BranchCode", item.Cells(dgcBranchCode.Index).Value.ToString)
                        Else
                            SQL1.AddParam("@BranchCode", BranchCode)
                        End If
                        If item.Cells(chVATType.Index).Value <> Nothing AndAlso item.Cells(chVATType.Index).Value <> "" Then
                            SQL1.AddParam("@VATType", item.Cells(chVATType.Index).Value.ToString)
                        Else
                            SQL1.AddParam("@VATType", "")
                        End If
                        If item.Cells(chATCCode.Index).Value <> Nothing AndAlso item.Cells(chATCCode.Index).Value <> "" Then
                            SQL1.AddParam("@ATC_Code", item.Cells(chATCCode.Index).Value.ToString)
                        Else
                            SQL1.AddParam("@ATC_Code", "")
                        End If
                        SQL1.ExecNonQuery(insertSQL)


                        Dim update As String
                        If item.Cells(chRecordID.Index).Value <> Nothing Then
                            update = " UPDATE tblPCV_Details Set Status = 'Closed' " &
                             " WHERE RecordID = '" & item.Cells(chRecordID.Index).Value & "'"
                            SQL1.ExecNonQuery(update)
                        End If

                        If item.Cells(chRef.Index).Value <> Nothing Then
                            If item.Cells(chRef.Index).Value.Contains("PCV:") Then
                                PCV = dgvEntry.Item(chRef.Index, line).Value.ToString.Replace("PCV:", "")
                                update = " UPDATE tblPCV SET Status ='Closed' WHERE PCV_No = '" & PCV & "'"
                                SQL1.ExecNonQuery(update)
                            End If
                        End If
                        line += 1
                    End If
            Next

            If strRefNo.Contains("LN:") Then
                strRefNo = strRefNo.Replace("LN:", "")
                Dim count As Integer = strRefNo.Split("-").Length - 1
                For i As Integer = 0 To count
                    Dim updateSQL As String
                    updateSQL = " UPDATE tblLoan SET Status ='Released', DateRelease = '" & dtpDocDate.Value.Date & "', RefType = 'CV', RefTransID = '" & TransID & "' WHERE Loan_No = '" & strRefNo.Split("-")(i) & "'"
                    SQL1.ExecNonQuery(updateSQL)
                Next
            End If
            SQL1.Commit()
        Catch ex As Exception
            SQL1.Rollback()
            TransID = ""
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "CV_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL1.FlushParams()
        End Try
    End Sub


    Private Sub UpdateCV()
        Dim SQL As New SQLControl
        SQL.BeginTransaction()
        Try
            Dim insertSQL, updateSQL, deleteSQL As String
            activityStatus = True
            updateSQL = " UPDATE tblCV  " &
                        " SET    CV_No = @CV_No, PaymentType = @PaymentType, VCECode = @VCECode, DateCV = @DateCV, RR_Ref = @RR_Ref," &
                        "        TotalAmount = @TotalAmount, Currency = @Currency, Exchange_Rate = @Exchange_Rate, Remarks = @Remarks, APV_Ref = @APV_Ref, ADV_Ref = @ADV_Ref, CA_Ref = @CA_Ref, OR_Ref = @OR_Ref, WhoModified = @WhoModified, DateModified = GETDATE(), " &
                        "        BranchCode = @BranchCode, BusinessCode = @BusinessCode, CostCenter = @CostCenter" &
                        " WHERE TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@CV_No", CVNo)
            SQL.AddParam("@PaymentType", cbPaymentType.SelectedItem)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateCV", dtpDocDate.Value.Date)
            SQL.AddParam("@TotalAmount", CDec(txtAmount.Text))
            SQL.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@APV_Ref", IIf(txtAPVRef.Text = "", DBNull.Value, APV_ID))
            SQL.AddParam("@ADV_Ref", IIf(txtADVRef.Text = "", DBNull.Value, ADV_ID))
            SQL.AddParam("@CA_Ref", IIf(txtCARef.Text = "", DBNull.Value, CA_ID))
            SQL.AddParam("@RR_Ref", IIf(RR_RefID = Nothing, 0, RR_RefID))
            SQL.AddParam("@OR_Ref", txtORNo.Text)
            SQL.AddParam("@WhoModified", UserID)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@CostCenter", cbCostCenter.SelectedItem)
            SQL.ExecNonQuery(updateSQL)

            If LOAN_ID > 0 Then
                updateSQL = " UPDATE tblLoan SET Status ='Released', DateRelease = '" & dtpDocDate.Value.Date & "', RefType = 'CV', RefTransID = '" & TransID & "' WHERE TransID = '" & LOAN_ID & "'"
                SQL.ExecNonQuery(updateSQL)
            End If

            If bankEvent = True Then
                If cbPaymentType.SelectedItem = "Check" Then
                    SaveCVRef(TransID, SQL)
                ElseIf cbPaymentType.SelectedItem = "Multiple Check" Then
                    SaveCVMultiRef(TransID, SQL)
                ElseIf cbPaymentType.SelectedItem = "Bank Transfer" Then
                    SaveCVRef_BankTransfer(TransID, SQL)
                ElseIf cbPaymentType.SelectedItem = "Debit Memo" Then
                    SaveDMRef(TransID, SQL)
                End If
            End If


            JETransiD = LoadJE("CV", TransID)
            If JETransiD = 0 Then

                JETransiD = GenerateTransID("JE_No", "tblJE_Header")

                insertSQL = " INSERT INTO " &
                       " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Currency, Exchange_Rate, Remarks, WhoCreated, CostCenter) " &
                       " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Currency, @Exchange_Rate, @Remarks, @WhoCreated, @CostCenter)"
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransiD)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "CV")
                SQL.AddParam("@RefTransID", TransID)
                SQL.AddParam("@Book", "Cash Disbursements")
                SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@WhoCreated", UserID)
                SQL.AddParam("@CostCenter", cbCostCenter.SelectedItem)
                SQL.ExecNonQuery(insertSQL)

                'JETransiD = LoadJE("CV", TransID)
            Else
                updateSQL = " UPDATE tblJE_Header " &
                           " SET    AppDate = @AppDate, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " &
                           "        RefType = @RefType, RefTransID = @RefTransID, Book = @Book, TotalDBCR = @TotalDBCR, Currency = @Currency, Exchange_Rate = @Exchange_Rate,  " &
                           "        Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE(), CostCenter = @CostCenter " &
                           " WHERE  JE_No = @JE_No "
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransiD)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "CV")
                SQL.AddParam("@RefTransID", TransID)
                SQL.AddParam("@Book", "Cash Disbursements")
                SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.00", txtConversion.Text)))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@CostCenter", cbCostCenter.SelectedItem)
                SQL.AddParam("@WhoModified", UserID)
                SQL.ExecNonQuery(updateSQL)
            End If


            ' DELETE ACCOUNTING ENTRIES
            deleteSQL = " DELETE FROM tblJE_Details  WHERE  JE_No = @JE_No "
            SQL.FlushParams()
            SQL.AddParam("@JE_No", JETransiD)
            SQL.ExecNonQuery(deleteSQL)

            Dim strRefNo As String = ""
            Dim HeaderCC As String = cbCostCenter.SelectedItem

            Dim line As Integer = 1
            For Each item As DataGridViewRow In dgvEntry.Rows
                If item.Cells(chAccntCode.Index).Value <> Nothing Then
                    insertSQL = " INSERT INTO " &
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, CostCenter, BranchCode , ProfitCenter, VATType, ATC_Code ) " &
                                " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @CostCenter, @BranchCode, @ProfitCenter, @VATType, @ATC_Code)"
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransiD)
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
                        SQL.AddParam("@Particulars", txtRemarks.Text)
                    End If
                    If cbCostCenter.SelectedItem = "" Then
                        If item.Cells(chCostID.Index).Value <> Nothing AndAlso item.Cells(chCostID.Index).Value <> "" Then
                            SQL.AddParam("@CostCenter", item.Cells(chCostID.Index).Value.ToString)
                        Else
                            SQL.AddParam("@CostCenter", "")
                        End If
                    Else
                        SQL.AddParam("@CostCenter", HeaderCC)
                    End If
                    If item.Cells(chProfit_Code.Index).Value <> Nothing AndAlso item.Cells(chProfit_Code.Index).Value <> "" Then
                        SQL.AddParam("@ProfitCenter", item.Cells(chProfit_Code.Index).Value.ToString)
                    Else
                        SQL.AddParam("@ProfitCenter", "")
                    End If
                    If item.Cells(chRef.Index).Value <> Nothing AndAlso item.Cells(chRef.Index).Value <> "" Then
                        SQL.AddParam("@RefNo", item.Cells(chRef.Index).Value.ToString)
                        If strRefNo.Length = 0 Then
                            strRefNo = item.Cells(chRef.Index).Value.ToString
                        Else
                            strRefNo = strRefNo & "-" & item.Cells(chRef.Index).Value.ToString
                        End If
                    Else
                        SQL.AddParam("@RefNo", "")
                    End If
                    SQL.AddParam("@LineNumber", line)
                    If item.Cells(dgcBranchCode.Index).Value <> Nothing AndAlso item.Cells(dgcBranchCode.Index).Value <> "" Then
                        SQL.AddParam("@BranchCode", item.Cells(dgcBranchCode.Index).Value.ToString)
                    Else
                        SQL.AddParam("@BranchCode", BranchCode)
                    End If
                    If item.Cells(chVATType.Index).Value <> Nothing AndAlso item.Cells(chVATType.Index).Value <> "" Then
                        SQL.AddParam("@VATType", item.Cells(chVATType.Index).Value.ToString)
                    Else
                        SQL.AddParam("@VATType", "")
                    End If
                    If item.Cells(chATCCode.Index).Value <> Nothing AndAlso item.Cells(chATCCode.Index).Value <> "" Then
                        SQL.AddParam("@ATC_Code", item.Cells(chATCCode.Index).Value.ToString)
                    Else
                        SQL.AddParam("@ATC_Code", "")
                    End If
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next

            If strRefNo.Contains("LN:") Then
                strRefNo = strRefNo.Replace("LN:", "")
                Dim count As Integer = strRefNo.Split("-").Length - 1
                For i As Integer = 0 To count
                    Dim updateSQL1 As String
                    updateSQL1 = " UPDATE tblLoan SET Status ='Released', DateRelease = '" & dtpDocDate.Value.Date & "', RefType = 'CV', RefTransID = '" & TransID & "' WHERE Loan_No = '" & strRefNo.Split("-")(i) & "'"
                    SQL.ExecNonQuery(updateSQL1)
                Next
            End If
            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "CV_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub LoadBankAccount_BankTransfer(ByVal VCECode As String)
        Dim insertSQL As String
        insertSQL = " SELECT  BankName, BankAccount " & _
                            " FROM    tblVCE_Master " & _
                            " WHERE   VCECode = @VCECode "
        SQL.FlushParams()
        SQL.AddParam("@VCECode", VCECode)
        SQL.ReadQuery(insertSQL)
        If SQL.SQLDR.Read Then
            txtBankTransfer_VCEBankName.Text = SQL.SQLDR("BankName").ToString()
            txtBankTransfer_VCEBankAccount.Text = SQL.SQLDR("BankAccount").ToString
        End If

    End Sub


    Private Sub SaveCVRef(ByVal CVNo As Integer, SQL1 As SQLControl)
        Dim deleteSQL As String
        deleteSQL = "DELETE FROM tblCV_BankRef WHERE CV_No ='" & CVNo & "'"
        SQL1.ExecNonQuery(deleteSQL)
        Dim insertSQL As String
        insertSQL = " INSERT INTO " & _
                    " tblCV_BankRef (CV_No, BankID, RefNo, RefDate, RefAmount, RefName) " & _
                    " VALUES(@CV_No, @BankID, @RefNo, @RefDate, @RefAmount, @RefName)"
        SQL1.FlushParams()
        SQL1.AddParam("@CV_No", CVNo)
        SQL1.AddParam("@BankID", bankID)
        SQL1.AddParam("@RefNo", txtBankRef.Text)
        SQL1.AddParam("@RefDate", dtpBankRefDate.Value.Date)
        SQL1.AddParam("@RefAmount", CDec(txtAmount.Text))
        SQL1.AddParam("@RefName", txtBankRefName.Text)
        SQL1.ExecNonQuery(insertSQL)
    End Sub

    Private Sub SaveDMRef(ByVal CVNo As Integer, SQL1 As SQLControl)
        Dim deleteSQL As String
        deleteSQL = "DELETE FROM tblCV_BankRef WHERE CV_No ='" & CVNo & "'"
        SQL1.ExecNonQuery(deleteSQL)
        Dim insertSQL As String
        insertSQL = " INSERT INTO " & _
                    " tblCV_BankRef (CV_No, BankID, RefNo, RefDate, RefAmount, RefName) " & _
                    " VALUES(@CV_No, @BankID, @RefNo, @RefDate, @RefAmount, @RefName)"
        SQL1.FlushParams()
        SQL1.AddParam("@CV_No", CVNo)
        SQL1.AddParam("@BankID", bankID)
        SQL1.AddParam("@RefNo", txtDMRef.Text)
        SQL1.AddParam("@RefDate", dtpDocDate.Value)
        SQL1.AddParam("@RefAmount", CDec(txtAmount.Text))
        SQL1.AddParam("@RefName", "")
        SQL1.ExecNonQuery(insertSQL)
    End Sub

    Private Sub SaveCVMultiRef(ByVal CVNo As Integer, SQL1 As SQLControl)
        Dim deleteSQL As String
        Dim insertSQL As String
        Dim bankIDMulti As Integer
        deleteSQL = "DELETE FROM tblCV_BankRef WHERE CV_No ='" & CVNo & "'"
        SQL1.ExecNonQuery(deleteSQL)

        For Each item As DataGridViewRow In dgvMultipleCheck.Rows
            If item.Cells(dgcBankID.Index).Value <> Nothing Then
                bankIDMulti = item.Cells(dgcBankID.Index).Value
                insertSQL = " INSERT INTO " & _
                  " tblCV_BankRef (CV_No, BankID, RefNo, RefDate, RefAmount, RefVCECode, RefName) " & _
                  " VALUES(@CV_No, @BankID, @RefNo, @RefDate, @RefAmount, @RefVCECode, @RefName)"
                SQL1.FlushParams()
                SQL1.AddParam("@CV_No", CVNo)
                SQL1.AddParam("@BankID", bankIDMulti)
                If item.Cells(dgcCheckNo.Index).Value <> Nothing Then
                    SQL1.AddParam("@RefNo", item.Cells(dgcCheckNo.Index).Value)
                Else
                    SQL1.AddParam("@RefNo", "")
                End If
                SQL1.AddParam("@RefDate", item.Cells(dgcCheckDate.Index).Value)
                If IsNumeric(item.Cells(dgcAmount.Index).Value) Then
                    SQL1.AddParam("@RefAmount", CDec(item.Cells(dgcAmount.Index).Value))
                Else
                    SQL1.AddParam("@RefAmount", 0)
                End If
                If item.Cells(dgcCheckVCECode.Index).Value <> "" Then
                    SQL1.AddParam("@RefVCECode", item.Cells(dgcCheckVCECode.Index).Value)
                Else
                    SQL1.AddParam("@RefVCECode", "")
                End If
                If item.Cells(dgcCheckName.Index).Value <> "" Then
                    SQL1.AddParam("@RefName", item.Cells(dgcCheckName.Index).Value)
                Else
                    SQL1.AddParam("@RefName", txtVCEName.Text)
                End If
                SQL1.ExecNonQuery(insertSQL)
            End If
        Next
    End Sub

    Private Sub SaveCVRef_BankTransfer(ByVal CVNo As Integer, SQL1 As SQLControl)
        Dim deleteSQL As String
        deleteSQL = "DELETE FROM tblCV_BankRef WHERE CV_No ='" & CVNo & "'"
        SQL1.ExecNonQuery(deleteSQL)
        Dim insertSQL As String
        insertSQL = " INSERT INTO " & _
                    " tblCV_BankRef (CV_No, BankID, RefNo, RefDate, RefAmount,  DestinationBank, DestinationBankAccount) " & _
                    " VALUES(@CV_No, @BankID, @RefNo, @RefDate, @RefAmount,  @DestinationBank, @DestinationBankAccount)"
        SQL1.FlushParams()
        SQL1.AddParam("@CV_No", CVNo)
        SQL1.AddParam("@BankID", bankID)
        SQL1.AddParam("@RefNo", txtBankTransfer_Ref.Text)
        SQL1.AddParam("@RefDate", dtpBankTransfer_Date.Value.Date)
        SQL1.AddParam("@RefAmount", CDec(txtBankTransfer_Amount.Text))
        SQL1.AddParam("@DestinationBank", txtBankTransfer_VCEBankName.Text)
        SQL1.AddParam("@DestinationBankAccount", txtBankTransfer_VCEBankAccount.Text)
        SQL1.ExecNonQuery(insertSQL)
    End Sub

    Private Sub cbDisburseType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbDisburseType.SelectedIndexChanged
        Try
            If disableEvent = False Then
                If cbDisburseType.SelectedIndex <> -1 Then
                    Dim query As String
                    Dim amount As Decimal
                    query = " SELECT  Account_Code, AccountTitle, Amount  " & _
                            " FROM    tblCV_ExpenseType INNER JOIN tblCOA_Master " & _
                            " ON      tblCV_ExpenseType.Account_Code = tblCOA_Master.AccountCode " & _
                            " WHERE   tblCV_ExpenseType.Status ='Active' AND Expense_Description = @Expense_Description "
                    SQL.FlushParams()
                    SQL.AddParam("@Expense_Description", cbDisburseType.SelectedItem)
                    SQL.ReadQuery(query)
                    If SQL.SQLDR.Read Then
                        If txtAmount.Text = "" Then
                            amount = CDec(SQL.SQLDR("Amount"))
                        Else
                            amount = CDec(txtAmount.Text) - IIf(txtTotalDebit.Text = "", 0, txtTotalDebit.Text)
                        End If

                        dgvEntry.Rows.Add("")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("Account_Code").ToString
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(amount).ToString("N2")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = cbDisburseType.SelectedItem
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode


                        TotalDBCR()
                    End If

                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub dgvEntry_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntry.CellContentClick
        Try
            If e.ColumnIndex = Column12.Index Then
                If txtTotalDebit.Text <> txtTotalCredit.Text Then
                    Dim query As String
                    Dim bankIDMulti As Integer
                    query = " SELECT  WithBank, Account_Code " & _
                            " FROM    tblCV_PaymentType LEFT JOIN tblCOA_Master " & _
                            " ON      tblCV_PaymentType.Account_Code = tblCOA_Master.AccountCode " & _
                            " WHERE   Payment_Type = @Payment_Type "
                    SQL.FlushParams()
                    SQL.AddParam("@Payment_Type", cbPaymentType.SelectedItem)
                    SQL.ReadQuery(query)
                    If SQL.SQLDR.Read Then
                        If SQL.SQLDR("WithBank") Then
                            If cbPaymentType.SelectedItem = "Check" Or cbPaymentType.SelectedItem = "Manager's Check" Then
                                If cbBank.SelectedIndex <> -1 Then
                                    If isClearingEnabled Then
                                        query = " SELECT    tblBank_Master.AccountCode_Clearing AS AccountCode, AccountTitle" &
                                               " FROM      tblBank_Master INNER JOIN tblCOA_Master " &
                                               " ON        tblBank_Master.AccountCode_Clearing = tblCOA_Master.AccountCode " &
                                               " WHERE     BankID ='" & bankID & "' "
                                    Else
                                        query = " SELECT    tblBank_Master.AccountCode, AccountTitle" &
                                               " FROM      tblBank_Master INNER JOIN tblCOA_Master " &
                                               " ON        tblBank_Master.AccountCode = tblCOA_Master.AccountCode " &
                                               " WHERE     BankID ='" & bankID & "' "
                                    End If

                                    SQL.ReadQuery(query)
                                    If SQL.SQLDR.Read Then
                                        dgvEntry.Rows.Add("")
                                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("AccountCode").ToString
                                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(CDec(txtTotalDebit.Text) - CDec(txtTotalCredit.Text)).ToString("N2")
                                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode


                                        LoadBranch()
                                    End If
                                    txtAmount.Text = CDec(CDec(txtTotalDebit.Text) - CDec(txtTotalCredit.Text)).ToString("N2")
                                    txtBankRefAmount.Text = txtAmount.Text
                                    TotalDBCR()
                                End If
                            ElseIf cbPaymentType.SelectedItem = "Debit Memo" Then
                                If cbBankDM.SelectedIndex <> -1 Then
                                    If isClearingEnabled Then
                                        query = " SELECT    tblBank_Master.AccountCode_Clearing AS AccountCode, AccountTitle" &
                                               " FROM      tblBank_Master INNER JOIN tblCOA_Master " &
                                               " ON        tblBank_Master.AccountCode_Clearing = tblCOA_Master.AccountCode " &
                                               " WHERE     BankID ='" & bankID & "' "
                                    Else
                                        query = " SELECT    tblBank_Master.AccountCode, AccountTitle" &
                                            " FROM      tblBank_Master INNER JOIN tblCOA_Master " &
                                            " ON        tblBank_Master.AccountCode = tblCOA_Master.AccountCode " &
                                            " WHERE     BankID ='" & bankID & "' "
                                    End If


                                    SQL.ReadQuery(query)
                                    If SQL.SQLDR.Read Then
                                        dgvEntry.Rows.Add("")
                                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("AccountCode").ToString
                                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(CDec(txtTotalDebit.Text) - CDec(txtTotalCredit.Text)).ToString("N2")
                                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode


                                        LoadBranch()
                                    End If
                                    txtAmount.Text = CDec(CDec(txtTotalDebit.Text) - CDec(txtTotalCredit.Text)).ToString("N2")
                                    TotalDBCR()
                                End If
                            ElseIf cbPaymentType.SelectedItem = "Multiple Check" Then
                                Dim Amount As Decimal = 0
                                For i As Integer = 0 To dgvMultipleCheck.Rows.Count - 1
                                    If dgvMultipleCheck.Item(dgcBankID.Index, i).Value <> "" Then
                                        bankIDMulti = dgvMultipleCheck.Item(dgcBankID.Index, i).Value
                                        If isClearingEnabled Then
                                            query = " SELECT    tblBank_Master.AccountCode_Clearing AS AccountCode, AccountTitle" &
                                             " FROM      tblBank_Master INNER JOIN tblCOA_Master " &
                                             " ON        tblBank_Master.AccountCode_Clearing = tblCOA_Master.AccountCode " &
                                             " WHERE     BankID ='" & bankIDMulti & "' "
                                        Else
                                            query = " SELECT    tblBank_Master.AccountCode, AccountTitle" &
                                             " FROM      tblBank_Master INNER JOIN tblCOA_Master " &
                                             " ON        tblBank_Master.AccountCode = tblCOA_Master.AccountCode " &
                                             " WHERE     BankID ='" & bankIDMulti & "' "
                                        End If


                                        SQL.ReadQuery(query)
                                        If SQL.SQLDR.Read Then
                                            dgvEntry.Rows.Add("")
                                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("AccountCode").ToString
                                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(dgvMultipleCheck.Item(dgcAmount.Index, i).Value).ToString("N2")
                                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = dgvMultipleCheck.Item(dgcCheckVCECode.Index, i).Value
                                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = dgvMultipleCheck.Item(dgcCheckName.Index, i).Value
                                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode

                                          
                                            LoadBranch()
                                            Amount = Amount + dgvMultipleCheck.Item(dgcAmount.Index, i).Value
                                        End If
                                        TotalDBCR()
                                    End If
                                Next
                                txtAmount.Text = CDec(Amount).ToString("N2")

                            ElseIf cbPaymentType.SelectedItem = "Bank Transfer" Then
                                If cbBankTransfer_Bank.SelectedIndex <> -1 Then
                                    query = " SELECT    tblBank_Master.AccountCode, AccountTitle" & _
                                            " FROM      tblBank_Master INNER JOIN tblCOA_Master " & _
                                            " ON        tblBank_Master.AccountCode = tblCOA_Master.AccountCode " & _
                                            " WHERE     BankID ='" & bankID & "' "
                                    SQL.ReadQuery(query)
                                    If SQL.SQLDR.Read Then
                                        dgvEntry.Rows.Add("")
                                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("AccountCode").ToString
                                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(CDec(txtTotalDebit.Text) - CDec(txtTotalCredit.Text)).ToString("N2")
                                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode

                                    
                                        LoadBranch()
                                    End If
                                    txtAmount.Text = CDec(CDec(txtTotalDebit.Text) - CDec(txtTotalCredit.Text)).ToString("N2")
                                    txtBankTransfer_Amount.Text = txtAmount.Text
                                    TotalDBCR()
                                End If
                            End If
                    Else
                            Dim code As String = GetCashAccountCode()
                        dgvEntry.Rows.Add("")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = code
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(code)
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(CDec(txtTotalDebit.Text) - CDec(txtTotalCredit.Text)).ToString("N2")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode

                       
                        LoadBranch()
                        txtAmount.Text = CDec(CDec(txtTotalDebit.Text) - CDec(txtTotalCredit.Text)).ToString("N2")
                        txtBankRefAmount.Text = txtAmount.Text
                        TotalDBCR()
                    End If

                        TotalDBCR()
                    End If
                End If
            ElseIf e.ColumnIndex = chCompute.Index Then
                Dim f As New frmTaxComputation
                Dim VATamount As Decimal
                Dim VCECode As String
                If dgvEntry.Item(chVCECode.Index, e.RowIndex).Value = "" Then
                    VCECode = txtVCECode.Text
                Else
                    VCECode = dgvEntry.Item(chVCECode.Index, e.RowIndex).Value
                End If

                VATamount = CDec(IIf(IIf(IsNothing(dgvEntry.Item(chDebit.Index, e.RowIndex).Value), "0.00", dgvEntry.Item(chDebit.Index, e.RowIndex).Value).ToString = "0.00", IIf(IsNothing(dgvEntry.Item(chCredit.Index, e.RowIndex).Value), "0.00", dgvEntry.Item(chCredit.Index, e.RowIndex).Value).ToString, IIf(IsNothing(dgvEntry.Item(chDebit.Index, e.RowIndex).Value), "0.00", dgvEntry.Item(chDebit.Index, e.RowIndex).Value).ToString)).ToString("N2")
                f.ShowDialog(VATamount, "PJ", "", VCECode)
                If IsNothing(dgvEntry.Item(chDebit.Index, e.RowIndex).Value) Then
                    dgvEntry.Item(chCredit.Index, e.RowIndex).Value = CDec(f.GrossAmount).ToString("N2")
                    dgvEntry.Item(chVATType.Index, e.RowIndex).Value = f.VATType
                    dgvEntry.Item(chVCECode.Index, e.RowIndex).Value = f.VCECode
                    dgvEntry.Item(chVCEName.Index, e.RowIndex).Value = GetVCEName(f.VCECode)
                Else
                    dgvEntry.Item(chDebit.Index, e.RowIndex).Value = CDec(f.GrossAmount).ToString("N2")
                    dgvEntry.Item(chVATType.Index, e.RowIndex).Value = f.VATType
                    dgvEntry.Item(chVCECode.Index, e.RowIndex).Value = f.VCECode
                    dgvEntry.Item(chVCEName.Index, e.RowIndex).Value = GetVCEName(f.VCECode)
                End If
                If f.VATAmount <> 0 Then
                    dgvEntry.Rows.Add("")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = f.TAX_IV
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(f.TAX_IV)
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(f.VATAmount).ToString("N2")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = "Input VAT"
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = f.VCECode
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(f.VCECode)

                 
                End If
                If f.EWTAmount <> 0 Then
                    dgvEntry.Rows.Add("")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = f.TAX_EWT
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(f.TAX_EWT)
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(f.EWTAmount).ToString("N2")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = "EWT"
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = f.VCECode
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chATCCode.Index).Value = f.ATCCode
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(f.VCECode)

                   
                End If
                TotalDBCR()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Function GetCashAccount(ByVal PaymentType As String, BankID As Integer)
        Dim cashAccount As String = ""
        Dim query As String
        query = " SELECT  WithBank, Account_Code " & _
                " FROM    tblCV_PaymentType LEFT JOIN tblCOA_Master " & _
                " ON      tblCV_PaymentType.Account_Code = tblCOA_Master.AccountCode " & _
                " WHERE   Payment_Type ='" & PaymentType & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            If SQL.SQLDR("WithBank") Then
                If cbBank.SelectedIndex <> -1 Then
                    query = " SELECT    tblBank_Master.AccountCode, AccountTitle " & _
                            " FROM      tblBank_Master INNER JOIN tblCOA_Master " & _
                            " ON        tblBank_Master.AccountCode = tblCOA_Master.AccountCode " & _
                            " WHERE     BankID ='" & BankID & "' "
                    SQL.ReadQuery(query)
                    If SQL.SQLDR.Read Then
                        cashAccount = SQL.SQLDR("AccountCode").ToString
                    End If
                End If
            Else
                cashAccount = SQL.SQLDR("Account_Code").ToString
            End If
        End If
        Return cashAccount
    End Function



    Private Sub dgvManual_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntry.CellEndEdit
        If e.ColumnIndex = chDebit.Index Or e.ColumnIndex = chCredit.Index Then
            If IsNumeric(dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value) Then
                dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value = CDec(dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value).ToString("N2")
            End If
            TotalDBCR()
        ElseIf e.ColumnIndex = chAccntCode.Index Or e.ColumnIndex = chAccntTitle.Index Then
            If dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value <> Nothing Then
                Dim f As New frmCOA_Search
                Dim filter As String
                If e.ColumnIndex = 0 Then
                    filter = "AccntCode"
                Else
                    filter = "AccntTitle"
                End If
                f.ShowDialog(filter, dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value.ToString)
                If f.accountcode <> "" And f.accttile <> "" Then
                    dgvEntry.Item(chAccntCode.Index, e.RowIndex).Value = f.accountcode
                    dgvEntry.Item(chAccntTitle.Index, e.RowIndex).Value = f.accttile
                    dgvEntry.SelectedCells(0).Selected = False
                    dgvEntry.Item(chDebit.Index, e.RowIndex).Selected = True
                End If
                f.Dispose()

                TotalDBCR()
            End If
        ElseIf e.ColumnIndex = chVCECode.Index Or e.ColumnIndex = chVCEName.Index Then
            Dim f As New frmVCE_Search
            f.txtFilter.Text = dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value.ToString
            f.ShowDialog()
            dgvEntry.Item(chVCECode.Index, e.RowIndex).Value = f.VCECode
            dgvEntry.Item(chVCEName.Index, e.RowIndex).Value = f.VCEName
            f.Dispose()

        ElseIf e.ColumnIndex = chCost_Center.Index Then
            If dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value <> Nothing Then
                Dim f1 As New frmCC_PC_Search
                f1.Text = "Search of Cost Center"
                f1.Type = "Cost Center"
                f1.txtSearch.Text = dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                f1.FilterText = dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                f1.ShowDialog()
                If f1.Code <> "" And f1.Description <> "" Then
                    dgvEntry.Item(chCostID.Index, e.RowIndex).Value = f1.Code
                    dgvEntry.Item(chCost_Center.Index, e.RowIndex).Value = f1.Description
                End If
                f1.Dispose()
            End If
        ElseIf e.ColumnIndex = chProfit_Center.Index Then
            If dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value <> Nothing Then
                Dim f1 As New frmCC_PC_Search
                f1.Text = "Search of Profit Center"
                f1.Type = "Profit Center"
                f1.txtSearch.Text = dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                f1.FilterText = dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                f1.ShowDialog()
                If f1.Code <> "" And f1.Description <> "" Then
                    dgvEntry.Item(chProfit_Code.Index, e.RowIndex).Value = f1.Code
                    dgvEntry.Item(chProfit_Center.Index, e.RowIndex).Value = f1.Description
                End If
                f1.Dispose()
            End If
        End If
    End Sub



    'Start of Cost Center insert to Table
    Dim strDefaultGridView As String = ""
    Dim strDefaultGridCode As String = ""
    Public Function LoadCostCenterGridView()

        Dim selectSQL As String = " SELECT Code, Description FROM tblCC  WHERE Status <> 'Inactive'"
        SQL.ReadQuery(selectSQL, 2)

        Dim cbvGridviewCell As New DataGridViewComboBoxCell

        Dim count As Integer = 1
        cbvGridviewCell.Items.Add("")

        While SQL.SQLDR2.Read
            If count = 1 Then
                strDefaultGridCode = SQL.SQLDR2("Code").ToString
                strDefaultGridView = SQL.SQLDR2("Description").ToString
            End If
            cbvGridviewCell.Items.Add(SQL.SQLDR2("Description").ToString)
            count += 1
        End While
        strDefaultGridView = ""
        Return cbvGridviewCell

    End Function

    Public Sub LoadCostCenterCode(ByVal CostCenter As String, ByVal RowIndex As Integer, ByVal CodeIndex As Integer, ByVal CostIndex As Integer)

        Dim selectSQL As String
        selectSQL = " SELECT Code, Description FROM tblCC WHERE Description = '" & CostCenter & "' AND Status <> 'Inactive'"
        SQL.ReadQuery(selectSQL, 2)

        strDefaultGridView = ""
        strDefaultGridCode = ""

        While SQL.SQLDR2.Read
            strDefaultGridView = SQL.SQLDR2("Description").ToString
            strDefaultGridCode = SQL.SQLDR2("Code").ToString
        End While
        dgvEntry.Rows(RowIndex).Cells(CodeIndex).Value = strDefaultGridView
        dgvEntry.Rows(RowIndex).Cells(CostIndex).Value = strDefaultGridCode

    End Sub

    Public Function LoadProfitCenterGridView()

        Dim selectSQL As String = " SELECT Code, Description FROM tblPC  WHERE Status <> 'Inactive'"
        SQL.ReadQuery(selectSQL, 2)

        Dim cbvGridviewCell As New DataGridViewComboBoxCell

        Dim count As Integer = 1
        cbvGridviewCell.Items.Add("")

        While SQL.SQLDR2.Read
            If count = 1 Then
                strDefaultGridCode = SQL.SQLDR2("Code").ToString
                strDefaultGridView = SQL.SQLDR2("Description").ToString
            End If
            cbvGridviewCell.Items.Add(SQL.SQLDR2("Description").ToString)
            count += 1
        End While
        strDefaultGridView = ""
        Return cbvGridviewCell

    End Function

    Public Sub LoadProfitCenterCode(ByVal ProfitCenter As String, ByVal RowIndex As Integer, ByVal CodeIndex As Integer, ByVal CostIndex As Integer)

        Dim selectSQL As String
        selectSQL = " SELECT Code, Description FROM tblPC WHERE Description = '" & ProfitCenter & "'  AND Status <> 'Inactive'"
        SQL.ReadQuery(selectSQL, 2)

        strDefaultGridView = ""
        strDefaultGridCode = ""

        While SQL.SQLDR2.Read
            strDefaultGridView = SQL.SQLDR2("Description").ToString
            strDefaultGridCode = SQL.SQLDR2("Code").ToString
        End While
        dgvEntry.Rows(RowIndex).Cells(CodeIndex).Value = strDefaultGridView
        dgvEntry.Rows(RowIndex).Cells(CostIndex).Value = strDefaultGridCode

    End Sub

    Public Function GetPCName(ByVal PCCode As String) As String
        Dim query As String
        query = " SELECT Description FROM tblPC WHERE Code ='" & PCCode & "'  AND Status <> 'Inactive' "
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("Description").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetCCName(ByVal CCCode As String) As String
        Dim query As String
        query = " SELECT Description FROM tblCC WHERE Code ='" & CCCode & "'  AND Status <> 'Inactive' "
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("Description").ToString
        Else
            Return ""
        End If
    End Function


    Private Sub LoadBranch()
        Try
            Dim dgvCB As New DataGridViewComboBoxColumn
            dgvCB = dgvEntry.Columns(dgcBranchCode.Index)
            dgvCB.Items.Clear()
            ' ADD ALL BranchCode
            Dim query As String
            query = " SELECT BranchCode FROM tblBranch "
            SQL.ReadQuery(query)
            dgvCB.Items.Clear()
            While SQL.SQLDR.Read
                dgvCB.Items.Add(SQL.SQLDR("BranchCode").ToString)
            End While
            dgvCB.Items.Add("")
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub btnPrintCheck_Click(sender As System.Object, e As System.EventArgs)
        If txtBankRef.Text <> "" Then
            frmCheckPrinting.CVTransID = txtTransNum.Text
            frmCheckPrinting.ShowDialog()
            frmCheckPrinting.Dispose()
        Else
            MsgBox("No Check No. to print!", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub LoadRFP(ByVal RFPID As String)
        Try
            Dim query As String
            query = " SELECT TransID, RFP_No, BranchCode, tblRFP.VCECode, VCEName " & _
                    " FROM tblRFP LEFT JOIN tblVCE_Master " & _
                    " ON tblRFP.VCECode = tblVCE_Master.VCECode " & _
                    " WHERE TransID ='" & RFPID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                txtVCEName.Text = SQL.SQLDR("VCEName").ToString
                RFP_ID = SQL.SQLDR("TransID")
                txtRFPRef.Text = SQL.SQLDR("RFP_No")
                query = " SELECT tblRFP.TransID, tblRFP.BranchCode, AccountCode, AccountTitle, CodePayee, RecordPayee, tblRFP_Details.Type, tblRFP_Details.BaseAmount, tblRFP_Details.InputVAT " & _
                        " FROM tblRFP INNER JOIN tblRFP_Details " & _
                        " ON tblRFP.TransID = tblRFP_Details.TransID " & _
                        " LEFT JOIN tblRFP_Type " & _
                        " ON tblRFP_Details.Type = tblRFP_Type.Type " & _
                        " LEFT JOIN tblCOA_Master " & _
                        " ON tblCOA_Master.AccountCode = tblRFP_Type.DefaultAccount " & _
                        " WHERE tblRFP.TransID = '" & RFPID & "' "
                SQL.ReadQuery(query)
                While SQL.SQLDR.Read
                    dgvEntry.Rows.Add("")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("AccountCode").ToString
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(SQL.SQLDR("BaseAmount")).ToString("N2")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = SQL.SQLDR("CodePayee").ToString
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = SQL.SQLDR("RecordPayee").ToString
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = SQL.SQLDR("BranchCode").ToString
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = SQL.SQLDR("Type").ToString
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "RFP:" & txtRFPRef.Text

                 


                    If SQL.SQLDR("InputVAT") <> 0 Then
                        dgvEntry.Rows.Add("")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = "18060"
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = " Vat Input"
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(SQL.SQLDR("InputVAT")).ToString("N2")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = SQL.SQLDR("CodePayee").ToString
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = SQL.SQLDR("RecordPayee").ToString
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = SQL.SQLDR("BranchCode").ToString
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = SQL.SQLDR("Type").ToString
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "RFP:" & txtRFPRef.Text

                    End If

                End While
                LoadBranch()
            End If
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadAPV(ByVal APV As String)
        Try
            Dim query As String
            query = " SELECT TransID AS TransID, APV_No, VCECode, Supplier AS  VCEName, Date AS Date_APV, Amount AS Net_Purchase, Amount/1.12 * 0.12 AS Input_VAT, Amount/1.12 * 0.02 AS EWT, Remarks, Reference AS Reference_Other, AccntCode, AccountTitle, Remarks, CostCenter " &
                    " FROM View_APV_Balance " &
                    " WHERE  TransID ='" & APV & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                txtVCEName.Text = SQL.SQLDR("VCEName").ToString
                txtBankRefName.Text = SQL.SQLDR("VCEName").ToString
                txtRemarks.Text = SQL.SQLDR("Remarks").ToString
                APV_ID = SQL.SQLDR("TransID")
                txtAPVRef.Text = SQL.SQLDR("APV_No")
                cbCostCenter.Text = SQL.SQLDR("CostCenter").ToString
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("AccntCode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(SQL.SQLDR("Net_Purchase")).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "APV:" & txtAPVRef.Text
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCostID.Index).Value = SQL.SQLDR("CostCenter").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCost_Center.Index).Value = GetCCName(SQL.SQLDR("CostCenter").ToString)

                LoadBranch()
            End If
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadADV(ByVal ADV As String)
        Try
            Dim query As String
            query = " SELECT TransID, ADV_No,  tblADV.VCECode, VCEName, DateADV AS DateADV, ADV_Amount AS Net_Purchase, Remarks,  AccntCode, AccountTitle " & _
                    " FROM   tblADV INNER JOIN tblVCE_Master " & _
                    " ON     tblADV.VCECode = tblVCE_Master.VCECode " & _
                    " INNER JOIN tblCOA_Master " & _
                    " ON     tblADV.AccntCode = tblCOA_Master.AccountCode " & _
                    " WHERE  TransID ='" & ADV & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                txtVCEName.Text = SQL.SQLDR("VCEName").ToString
                txtADVRef.Text = SQL.SQLDR("ADV_No")
                ADV_ID = SQL.SQLDR("TransID")

                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("AccntCode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(SQL.SQLDR("Net_Purchase")).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "ADV:" & txtADVRef.Text
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode

              

            End If
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadCA(ByVal CA As String)

        Try
            Dim query As String
            query = " SELECT TransID, CA_No,  tblCA.VCECode, VCEName, DateCA AS DateCA, Amount AS Net_Purchase, Remarks,  AccntCode, AccountTitle,  CostID  " &
                    " FROM   tblCA INNER JOIN viewVCE_Master " &
                    " ON     tblCA.VCECode = viewVCE_Master.VCECode " &
                    " INNER JOIN tblCOA_Master " &
                    " ON     tblCA.AccntCode = tblCOA_Master.AccountCode " &
                    " WHERE  TransID ='" & CA & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                CA_ID = SQL.SQLDR("TransID")
                txtCARef.Text = SQL.SQLDR("CA_No")
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                txtVCEName.Text = SQL.SQLDR("VCEName").ToString
                txtBankRefName.Text = SQL.SQLDR("VCEName").ToString
                txtRemarks.Text = SQL.SQLDR("Remarks").ToString
                cbCostCenter.Text = SQL.SQLDR("CostID").ToString
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("AccntCode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(SQL.SQLDR("Net_Purchase")).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "CA:" & SQL.SQLDR("CA_No").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode



            End If


            LoadBranch()
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub LoadLoan(ByVal Loan As String)
        Try
            Dim LoanAccount, IntIncomeAccount, UnearnedAccount, IntRecAccount, Loan_No, VCECode, IntAmortMethod, cashAccount As String
            Dim SetupUnearned As Boolean
            Dim LoanAmount, IntAmount, cashAmount As Decimal
            Dim query As String
            query = " SELECT tblLoan.LoanCode, SetupUnearned, LoanAccount, IntIncomeAccount, UnearnedAccount, IntRecAccount, " & _
                    " LoanAmount, IntAmount, tblLoan.IntAmortMethod, Loan_No, VCECode " & _
                    " FROM tblLoan_Type INNER JOIN tblLoan " & _
                    " ON tblLoan_Type.LoanCode = tblLoan.LoanCode " & _
                    " WHERE TransID = '" & Loan & "' "
            SQL.GetQuery(query)
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                With SQL.SQLDS.Tables(0).Rows(0)
                    LOAN_ID = Loan
                    SetupUnearned = .Item(1)
                    LoanAccount = .Item(2)
                    IntIncomeAccount = .Item(3)
                    UnearnedAccount = .Item(4)
                    IntRecAccount = .Item(5)
                    LoanAmount = .Item(6)
                    IntAmount = .Item(7)
                    IntAmortMethod = .Item(8)
                    Loan_No = .Item(9)
                    VCECode = .Item(10)
                    txtLoanRef.Text = Loan_No
                    If cbPaymentType.SelectedItem = "Check" Then
                        cashAccount = GetCashAccount(cbPaymentType.SelectedItem, bankID)
                    End If
                    If IntAmortMethod = "Less to Proceeds" Then    ' IF INTEREST IS LESS TO PROCEED 
                        ' LOAN RECEIVABLE ENTRY
                        dgvEntry.Rows.Add("")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = LoanAccount
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(LoanAccount)
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(LoanAmount).ToString("N2")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(VCECode)
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "LN:" & Loan_No
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode

                   

                        ' INTEREST INCOME ENTRY
                        dgvEntry.Rows.Add("")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = IntIncomeAccount
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(IntIncomeAccount)
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(IntAmount).ToString("N2")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(VCECode)
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Loan_No
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode

                        
                        cashAmount = LoanAmount - IntAmount
                    ElseIf IntAmortMethod = "Add On" Then    ' IF INTEREST IS Add On
                        ' LOAN RECEIVABLE ENTRY
                        dgvEntry.Rows.Add("")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = LoanAccount
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(LoanAccount)
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(LoanAmount).ToString("N2")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(VCECode)
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "LN:" & Loan_No
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode

                    

                        ' INTEREST INCOME ENTRY
                        dgvEntry.Rows.Add("")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = IntIncomeAccount
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(IntIncomeAccount)
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(IntAmount).ToString("N2")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(VCECode)
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Loan_No
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode

                       

                        cashAmount = LoanAmount - IntAmount
                    ElseIf IntAmortMethod = "Amortize" Then
                        If SetupUnearned = False Then  ' IF WITHOUT SETUP OF UNEARNED INCOME
                            ' LOAN RECEIVABLE ENTRY
                            dgvEntry.Rows.Add("")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = LoanAccount
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(LoanAccount)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(LoanAmount).ToString("N2")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(VCECode)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "LN:" & Loan_No
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode

                           

                        ElseIf SetupUnearned = True AndAlso LoanAccount = IntIncomeAccount Then  ' IF WITH SETUP OF UNEARNED INCOME AND INT. REC IS SAME AS LOAN REC. ACCOUNT
                            ' LOAN RECEIVABLE ENTRY
                            dgvEntry.Rows.Add("")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = LoanAccount
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(LoanAccount)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(LoanAmount + IntAmount).ToString("N2")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(VCECode)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "LN:" & Loan_No
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode

                         

                            ' UNEARNED INCOME ENTRY
                            dgvEntry.Rows.Add("")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = UnearnedAccount
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(UnearnedAccount)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(IntAmount).ToString("N2")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(VCECode)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Loan_No
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode

                        ElseIf SetupUnearned = True AndAlso LoanAccount <> IntIncomeAccount Then  ' IF WITH SETUP OF UNEARNED INCOME AND INT. REC IS SAME AS LOAN REC. ACCOUNT
                            ' LOAN RECEIVABLE ENTRY
                            dgvEntry.Rows.Add("")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = LoanAccount
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(LoanAccount)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(LoanAmount).ToString("N2")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(VCECode)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "LN:" & Loan_No
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode

                           
                            ' INTEREST RECEIVABLE ENTRY
                            dgvEntry.Rows.Add("")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = IntIncomeAccount
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(IntIncomeAccount)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(IntAmount).ToString("N2")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(VCECode)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Loan_No
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode

                            
                            ' UNEARNED INCOME ENTRY
                            dgvEntry.Rows.Add("")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = UnearnedAccount
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(UnearnedAccount)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(IntAmount).ToString("N2")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(VCECode)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Loan_No
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode

                           
                        End If
                        cashAmount = LoanAmount
                    End If
                End With


                query = " SELECT TransID, AccountCode, Amount, Description, VCECode, " & _
                        " CASE WHEN SetupCharges = 1  " & _
                        " THEN '' ELSE  RefID END AS RefID    " & _
                        " FROM tblLoan_Details " & _
                        " WHERE  tblLoan_Details.TransID = '" & Loan & "' AND tblLoan_Details.AmortMethod = 'Less to Proceeds'  "

                'query = " SELECT TransID,  " & _
                ' " CASE WHEN tblLoan_Charges.RecordID IS NULL THEN tblLoan_Details.AccountCode ELSE tblLoan_Charges.DefaultAccount END AS AccountCode,  " & _
                ' "  Amount, Description, VCECode, CASE WHEN SetupCharges = 1 THEN '' ELSE  'LN:' + RefID END AS RefID  " & _
                ' " FROM  tblLoan_Details LEFT JOIN tblLoan_Charges " & _
                ' " on tblLoan_Details.RefID = tblLoan_Charges.ChargeID " & _
                ' " WHERE TransID = '" & Loan & "' AND tblLoan_Details.AmortMethod = 'Less to Proceeds' "
                SQL.GetQuery(query)
                If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                        If row(2) > 0 Then
                            dgvEntry.Rows.Add("")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = row(1).ToString
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(row(1).ToString)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(row(2)).ToString("N2")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = row(4).ToString
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(row(4).ToString)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = row(3).ToString
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode

                           
                            cashAmount -= row(2)
                        End If
                    Next
                End If
                If cashAccount <> "" Then
                    ' CASH ENTRY
                    dgvEntry.Rows.Add(BranchCode, cashAccount, GetAccntTitle(cashAccount), "0.00", CDec(cashAmount).ToString("N2"), "", "", "", "")
                    dgvEntry.Rows.Add("")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = cashAccount
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(cashAccount)
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(cashAmount).ToString("N2")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode

                    
                    txtBankRefAmount.Text = CDec(cashAmount).ToString("N2")
                End If
                LoadBranch()
            End If
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("CV_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("CV")
            TransID = f.transID
            LoadCV(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("CV_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            CVNo = ""

            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbCancel.Enabled = False
            tsbDelete.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
            tsbPrint.Enabled = False
            tsbCopy.Enabled = True
            tsbOption.Enabled = False
            txtStatus.Text = "Open"
            EnableControl(True)

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
            txtVCEName.Select()
        End If
    End Sub

    'Private Function GenerateTransNum(ByRef Auto As Boolean, ModuleID As String, ColumnPK As String, Table As String) As String
    '    Dim TransNum As String = ""
    '    If Auto = True Then
    '        ' GENERATE TRANS ID 
    '        Dim query As String
    '        Do
    '            query = " SELECT	GlobalSeries, ISNULL(BranchCode,'All') AS BranchCode, ISNULL(BusinessCode,'All') AS BusinessCode,  " & _
    '                                " 		    ISNULL(TransGroup,0) AS TransGroup, ISNULL(Prefix,'') AS Prefix, ISNULL(Digits,6) AS Digits, " & _
    '                                "           ISNULL(StartRecord,1) AS StartRecord, LEN(ISNULL(Prefix,'')) + ISNULL(Digits,6) AS ID_Length " & _
    '                                " FROM	    tblTransactionSetup LEFT JOIN tblTransactionDetails " & _
    '                                " ON		tblTransactionSetup.TransType = tblTransactionDetails.TransType " & _
    '                                " WHERE	    tblTransactionSetup.TransType ='" & ModuleID & "' "
    '            SQL.ReadQuery(query)
    '            If SQL.SQLDR.Read Then
    '                If SQL.SQLDR("GlobalSeries") = True Then
    '                    If SQL.SQLDR("BranchCode") = "All" AndAlso SQL.SQLDR("BusinessCode") = "All" Then
    '                        Dim digits As Integer = SQL.SQLDR("Digits")
    '                        Dim prefix As String = Month(dtpDocDate.Value).ToString("MM") & Year(dtpDocDate.Value).ToString("YYYY")
    '                        Dim startrecord As Integer = SQL.SQLDR("StartRecord")
    '                        query = " SELECT    ISNULL(MAX(SUBSTRING(" & ColumnPK & "," & prefix.Length + 1 & "," & digits & "))+ 1,1) AS TransID  " & _
    '                                " FROM      " & Table & "  " & _
    '                                " WHERE     " & ColumnPK & " LIKE '" & prefix & "%' AND LEN(" & ColumnPK & ") = '" & digits & "'  AND TransAuto = 1 "
    '                        SQL.ReadQuery(query)
    '                        If SQL.SQLDR.Read Then
    '                            TransNum = SQL.SQLDR("TransID")
    '                            For i As Integer = 1 To digits
    '                                TransNum = "0" & TransNum
    '                            Next
    '                            TransNum = prefix & Strings.Right(TransNum, digits)

    '                            ' CHECK IF GENERATED TRANSNUM ALREADY EXIST
    '                            query = " SELECT    " & ColumnPK & " AS TransID  " & _
    '                                    " FROM      " & Table & "  " & _
    '                                    " WHERE     " & ColumnPK & " = '" & TransNum & "' "
    '                            SQL.ReadQuery(query)
    '                            If SQL.SQLDR.Read Then
    '                                Dim updateSQL As String
    '                                updateSQL = " UPDATE  " & Table & "  SET TransAuto = 1 WHERE " & ColumnPK & " = '" & TransNum & "' "
    '                                SQL.ExecNonQuery(updateSQL)
    '                                TransNum = -1
    '                            End If
    '                        End If

    '                    End If
    '                Else

    '                    Dim digits As Integer = SQL.SQLDR("Digits")
    '                    ' Dim prefix As String = Year(Date.Today) & DateTime.Now.ToString("MM")
    '                    Dim prefix As String = dtpDocDate.Value.ToString("MM") & Year(dtpDocDate.Value)
    '                    Dim startrecord As Integer = SQL.SQLDR("StartRecord")
    '                    query = " SELECT    ISNULL(MAX(SUBSTRING(" & ColumnPK & "," & prefix.Length + 1 & "," & digits & "))+ 1,1) AS TransID  " & _
    '                            " FROM      " & Table & "  " & _
    '                            " WHERE     " & ColumnPK & " LIKE '" & prefix & "%'   AND TransAuto = 1 AND BranchCode = '" & BranchCode & "'"
    '                    SQL.ReadQuery(query)
    '                    If SQL.SQLDR.Read Then
    '                        TransNum = SQL.SQLDR("TransID")
    '                        For i As Integer = 1 To digits
    '                            TransNum = "0" & TransNum
    '                        Next
    '                        TransNum = prefix & Strings.Right(TransNum, digits)

    '                        ' CHECK IF GENERATED TRANSNUM ALREADY EXIST
    '                        query = " SELECT    " & ColumnPK & " AS TransID  " & _
    '                                " FROM      " & Table & "  " & _
    '                                " WHERE     " & ColumnPK & " = '" & TransNum & "' AND BranchCode = '" & BranchCode & "'"
    '                        SQL.ReadQuery(query)
    '                        If SQL.SQLDR.Read Then
    '                            Dim updateSQL As String
    '                            updateSQL = " UPDATE  " & Table & "  SET TransAuto = 1 WHERE " & ColumnPK & " = '" & TransNum & "' "
    '                            SQL.ExecNonQuery(updateSQL)
    '                            TransNum = -1
    '                        End If
    '                    End If
    '                End If
    '            End If
    '            If TransNum <> -1 Then Exit Do
    '        Loop

    '        Return TransNum
    '    Else
    '        Return ""
    '    End If
    'End Function

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("CV_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)

            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbCancel.Enabled = False
            tsbDelete.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
            tsbPrint.Enabled = False
            tsbCopy.Enabled = False
            tsbOption.Enabled = True
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        Try
            If txtVCECode.Text = "" Then
                Msg("Please enter VCECode!", MsgBoxStyle.Exclamation)
            ElseIf txtTotalDebit.Text <> txtTotalCredit.Text Then
                MsgBox("Please check the Debit and Credit Amount!", MsgBoxStyle.Exclamation)
            ElseIf txtAmount.Text = "" Then
                MsgBox("Please check amount!", MsgBoxStyle.Exclamation)
            ElseIf txtConversion.Visible = True And txtConversion.Text = "" Then
                MsgBox("Please check exchange rate!", MsgBoxStyle.Exclamation)
            ElseIf dgvEntry.Rows.Count = 0 Then
                MsgBox("No entries, Please check!", MsgBoxStyle.Exclamation)
            ElseIf bankEvent = True And cbPaymentType.SelectedItem = "Check" And cbBank.SelectedIndex = -1 Then
                MsgBox("No bank selected, Please check!", MsgBoxStyle.Exclamation)
            ElseIf bankEvent = True And cbPaymentType.SelectedItem = "Multiple Check" And dgvMultipleCheck.Rows.Count = -1 Then
                MsgBox("No bank selected, Please check!", MsgBoxStyle.Exclamation)
            ElseIf bankEvent = True And cbPaymentType.SelectedItem = "Bank Transfer" And cbBankTransfer_Bank.SelectedIndex = -1 Then
                MsgBox("No bank selected, Please check!", MsgBoxStyle.Exclamation)
            ElseIf TransAuto = False And txtTransNum.Text = "" Then
                MsgBox("Please Enter Voucher Number!", MsgBoxStyle.Exclamation)
            ElseIf TransAuto = False AndAlso IfExist(txtTransNum.Text) And TransID = "" Then
                MsgBox("CV" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
            ElseIf TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID(ColumnID, DBTable)
                    If TransAuto Then
                        CVNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    Else
                        CVNo = txtTransNum.Text
                    End If
                    txtTransNum.Text = CVNo
                    SaveCV()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    LoadCV(TransID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    If transactionCleared = True Then
                        If MsgBox("This transaction is already cleared, are you sure you want to update this record?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                            If CVNo = txtTransNum.Text Then
                                CVNo = txtTransNum.Text
                                UpdateCV()
                                UpdateBR(accountCleared)
                                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                                CVNo = txtTransNum.Text
                                LoadCV(TransID)
                            Else
                                If Not IfExist(txtTransNum.Text) Then
                                    CVNo = txtTransNum.Text
                                    UpdateCV()
                                    UpdateBR(accountCleared)
                                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                                    CVNo = txtTransNum.Text
                                    LoadCV(TransID)
                                Else
                                    MsgBox("CV" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
                                End If
                            End If
                        End If
                    Else
                        If CVNo = txtTransNum.Text Then
                            CVNo = txtTransNum.Text
                            UpdateCV()
                            Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                            CVNo = txtTransNum.Text
                            LoadCV(TransID)
                        Else
                            If Not IfExist(txtTransNum.Text) Then
                                CVNo = txtTransNum.Text
                                UpdateCV()
                                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                                CVNo = txtTransNum.Text
                                LoadCV(TransID)
                            Else
                                MsgBox("CV" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub
    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("CV_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If txtStatus.Text <> "Cancelled" AndAlso MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim updateSQL As String
                        updateSQL = " UPDATE  tblCV SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        CVNo = txtTransNum.Text
                        SQL.AddParam("@TransID", TransID)
                        SQL.ExecNonQuery(updateSQL)

                        updateSQL = " UPDATE  tblJE_Header SET Status ='Cancelled' WHERE RefTransID = @RefTransID  AND RefType ='CV' "
                        SQL.FlushParams()
                        SQL.AddParam("@RefTransID", TransID)
                        SQL.ExecNonQuery(updateSQL)

                        If isReversalEntry = True Then
                            Dim insertSQL As String
                            insertSQL = " INSERT INTO tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, OtherRef) " &
                                        " SELECT JE_No, AccntCode, VCECode, Credit, Debit, Particulars, RefNo, 0 AS LineNumber, OtherRef FROM tblJE_Details " &
                                        " WHERE JE_No IN (SELECT DISTINCT JE_No FROM tblJE_Header WHERE RefType ='CV' AND RefTransID ='" & TransID & "') "
                            SQL.ExecNonQuery(insertSQL)
                        End If

                        Msg("Record cancelled successfully", MsgBoxStyle.Information)


                        If LOAN_ID > 0 Then
                            updateSQL = " UPDATE tblLoan SET Status ='Approved', DateRelease = '', RefType = '', RefTransID = '' WHERE TransID = '" & LOAN_ID & "'"
                            SQL.ExecNonQuery(updateSQL)
                        End If

                        If CA_ID > 0 Then
                            updateSQL = " UPDATE tblCA SET Status ='Active' WHERE TransID = '" & CA_ID & "'"
                            SQL.ExecNonQuery(updateSQL)
                        End If

                        tsbSearch.Enabled = True
                        tsbNew.Enabled = True
                        tsbEdit.Enabled = False
                        tsbSave.Enabled = False
                        tsbCancel.Enabled = False
                        tsbDelete.Enabled = False
                        tsbClose.Enabled = False
                        tsbPrevious.Enabled = False
                        tsbNext.Enabled = False
                        tsbExit.Enabled = True
                        tsbPrint.Enabled = True
                        tsbCopy.Enabled = False
                        EnableControl(False)

                        CVNo = txtTransNum.Text
                        LoadCV(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "CV_No", CVNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                ElseIf txtStatus.Text = "Cancelled" AndAlso MsgBox("Are you sure you want to un-cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim updateSQL As String
                        updateSQL = " UPDATE  tblCV SET Status = @Status WHERE TransID = @TransID "
                        SQL.FlushParams()
                        CVNo = txtTransNum.Text
                        SQL.AddParam("@TransID", TransID)
                        If ForApproval Then
                            SQL.AddParam("@Status", "Draft")
                        Else
                            SQL.AddParam("@Status", "Active")
                        End If
                        SQL.ExecNonQuery(updateSQL)

                        updateSQL = " UPDATE  tblJE_Header SET Status =@Status WHERE RefTransID = @RefTransID  AND RefType ='CV' "
                        SQL.FlushParams()
                        SQL.AddParam("@RefTransID", TransID)
                        If ForApproval Then
                            SQL.AddParam("@Status", "Draft")
                        Else
                            SQL.AddParam("@Status", "Saved")
                        End If
                        SQL.ExecNonQuery(updateSQL)

                        Dim deleteSQL As String
                        deleteSQL = " DELETE FROM tblJE_Details " &
                                    " WHERE JE_No IN (SELECT DISTINCT JE_No FROM tblJE_Header WHERE RefType ='CV' AND RefTransID ='" & TransID & "') AND LineNumber = 0 "
                        SQL.ExecNonQuery(deleteSQL)
                        Msg("Record un-cancelled successfully", MsgBoxStyle.Information)


                        If LOAN_ID > 0 Then
                            updateSQL = " UPDATE tblLoan SET Status ='Released', DateRelease = '" & dtpDocDate.Text & "', RefType = 'CV', RefTransID = '" & TransID & "' WHERE TransID = '" & LOAN_ID & "'"
                            SQL.ExecNonQuery(updateSQL)
                        End If

                        tsbSearch.Enabled = True
                        tsbNew.Enabled = True
                        tsbEdit.Enabled = False
                        tsbSave.Enabled = False
                        tsbCancel.Enabled = False
                        tsbDelete.Enabled = False
                        tsbClose.Enabled = False
                        tsbPrevious.Enabled = False
                        tsbNext.Enabled = False
                        tsbExit.Enabled = True
                        tsbPrint.Enabled = True
                        tsbCopy.Enabled = False
                        EnableControl(False)

                        CVNo = txtTransNum.Text
                        LoadCV(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "CV_No", CVNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs)
        Dim f As New frmReport_Display
        f.ShowDialog("CV", TransID)
        f.Dispose()
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If CVNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblCV " & _
                    " INNER JOIN tblBranch  ON " & _
                    " tblCV.BranchCode = tblBranch.BranchCode " & _
                    " WHERE " & _
                    " ( tblCV.BranchCode IN " & _
                    " ( " & _
                    " SELECT DISTINCT tblBranch.BranchCode " & _
                    " FROM tblBranch " & _
                    " INNER JOIN tblUser_Access ON " & _
                    " tblBranch.BranchCode = tblUser_Access.Code " & _
                    " AND tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                    " AND tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                    " WHERE UserID ='" & UserID & "' " & _
                    " ) " & _
                    " ) " & _
                    " AND CV_No < '" & CVNo & "' ORDER BY CV_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadCV(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If CVNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblCV " & _
                    " INNER JOIN tblBranch  ON " & _
                    " tblCV.BranchCode = tblBranch.BranchCode " & _
                    " WHERE " & _
                    " ( tblCV.BranchCode IN " & _
                    " ( " & _
                    " SELECT DISTINCT tblBranch.BranchCode " & _
                    " FROM tblBranch " & _
                    " INNER JOIN tblUser_Access ON " & _
                    " tblBranch.BranchCode = tblUser_Access.Code " & _
                    " AND tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                    " AND tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                    " WHERE UserID ='" & UserID & "' " & _
                    " ) " & _
                    " ) " & _
                    " AND CV_No > '" & CVNo & "' ORDER BY CV_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadCV(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click

        ' Toolstrip Buttons
        If CVNo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbDelete.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadCV(TransID)
            tsbEdit.Enabled = True
            tsbCancel.Enabled = True
            tsbDelete.Enabled = False
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


    Private Sub UpdateBR(ByVal bankAccountCode As String)
        Try
            Dim bankID As Integer
            Dim query, updatesql As String
            query = " SELECT    BankID " & _
                    " FROM      tblBank_Master " & _
                    " WHERE     AccountCode ='" & bankAccountCode & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                bankID = SQL.SQLDR("BankID").ToString

                query = " SELECT TransID FROM tblBR " & _
                            " WHERE DateBR >= @DateBR AND BankID = @BankID "
                SQL.AddParam("@BankID", bankID)
                SQL.AddParam("@DateBR", dateCleared)
                SQL.ReadQuery(query)

                While SQL.SQLDR.Read
                    Dim BR_TransID As String
                    BR_TransID = SQL.SQLDR("TransID").ToString
                    updatesql = " UPDATE tblBR SET Status = 'Cancelled',  WhoModified = @WhoModified, DateModified = GETDATE() " & _
                          " WHERE TransID = @TransID "
                    SQL.AddParam("@TransID", BR_TransID)
                    SQL.AddParam("@WhoModified", UserID)
                    SQL.ExecNonQuery(updatesql)

                    updatesql = " UPDATE tblJV SET BR_Ref = NULL " & _
                                " WHERE BR_Ref = @BR_Ref "
                    SQL.AddParam("@BR_Ref", BR_TransID)
                    SQL.ExecNonQuery(updatesql)

                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "BankID", bankID, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Function IfExist(ByVal ID As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblCV WHERE CV_No ='" & ID & "' AND Status <> 'Cancelled'  "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub TestToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub btnSearchVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchVCE.Click
        Dim f As New frmVCE_Search
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        txtVCEName.Text = f.VCEName
        txtBankRefName.Text = f.VCEName
        If bankEvent = True Then
            If cbPaymentType.SelectedItem = "Bank Transfer" Then
                LoadBankAccount_BankTransfer(txtVCECode.Text)
            End If
        End If
        LoadCurrency()
        f.Dispose()
    End Sub

    Private Sub txtVCEName_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.txtFilter.Text = txtVCEName.Text
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            txtVCEName.Text = f.VCEName
            txtBankRefName.Text = f.VCEName
            If bankEvent = True Then
                If cbPaymentType.SelectedItem = "Bank Transfer" Then
                    LoadBankAccount_BankTransfer(txtVCECode.Text)
                End If
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


    Private Sub frmCV_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                If tsbPrint.Enabled = True Then tsbPrint.ShowDropDown()
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

    Private Sub tsbCopyAPV_Click(sender As System.Object, e As System.EventArgs) Handles tsbCopyAPV.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.chkBatch.Visible = True

        f.ShowDialog("APV")

        If f.batch = True Then
            For Each row As DataGridViewRow In f.dgvList.Rows
                If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                    LoadAPV(row.Cells(0).Value)
                End If
            Next
        Else
            If f.transID <> "" Then
                LoadAPV(f.transID)
            End If
        End If
    End Sub

    Private Sub FromAdvancesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles tsbCopyADV.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.chkBatch.Visible = True

        f.ShowDialog("ADV")

        If f.batch = True Then
            For Each row As DataGridViewRow In f.dgvList.Rows
                If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                    LoadADV(row.Cells(0).Value)
                End If
            Next
        Else
            If f.transID <> "" Then
                LoadADV(f.transID)
            End If
        End If
    End Sub

    Private Sub PrintCVToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PrintCVToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("CV", TransID, "CV Printout")
        f.Dispose()
    End Sub

    Private Sub tsbPrint_ButtonClick(sender As System.Object, e As System.EventArgs) Handles tsbPrint.ButtonClick
        'Dim f As New frmReport_Display
        'f.ShowDialog("CV", TransID)
        'f.Dispose()
    End Sub

    Private Sub ChequieToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ChequieToolStripMenuItem.Click
        Dim f As New frmCheckPrinting
        f.CVTransID = TransID
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub BIR2307ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BIR2307ToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("BIR_2307", TransID, "CV")
        f.Dispose()
    End Sub

    Private Sub tsmUnreleased_Click(sender As System.Object, e As System.EventArgs)
        Dim updateSQl, CheckNo As String
        Dim bankIDMulti As Integer
        If MsgBox("Tagging cheque as released" & vbNewLine & "Click Yes to Confirm", MsgBoxStyle.YesNo + MsgBoxStyle.Information, "GR8 Message Alert") = MsgBoxResult.Yes Then
            If cbPaymentType.SelectedItem = "Check" Then

                updateSQl = " UPDATE tblCV_BankRef " &
                            " SET   Status ='Released' WHERE CV_No ='" & TransID & "' AND BankID = '" & bankID & "' AND RefNo ='" & txtBankRef.Text & "' "
                SQL.ExecNonQuery(updateSQl)
                txtRefStatus.Text = "Released"
            ElseIf cbPaymentType.SelectedItem = "Multiple Check" Then
                For Each item As DataGridViewRow In dgvMultipleCheck.Rows
                    If item.Cells(dgcBankID.Index).Value <> Nothing Then
                        bankIDMulti = item.Cells(dgcBankID.Index).Value
                        CheckNo = item.Cells(dgcCheckNo.Index).Value
                        updateSQl = " UPDATE tblCV_BankRef " &
                          " SET   Status ='Released' WHERE CV_No ='" & TransID & "' AND BankID = '" & bankIDMulti & "' AND RefNo ='" & CheckNo & "' "
                        SQL.ExecNonQuery(updateSQl)
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub CancelCheckToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CancelCheckToolStripMenuItem.Click
        Dim updateSQl, CheckNo As String
        Dim bankIDMulti As Integer
        If MsgBox("Tagging cheque as cancelled" & vbNewLine & "Click Yes to Confirm", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.Yes Then
            If cbPaymentType.SelectedItem = "Check" Then

                updateSQl = " UPDATE tblCV_BankRef " & _
                            " SET   Status ='Cancelled' WHERE CV_No ='" & TransID & "' AND BankID = '" & bankID & "' AND RefNo ='" & txtBankRef.Text & "' "
                SQL.ExecNonQuery(updateSQl)
                txtRefStatus.Text = "Cancelled"

            ElseIf cbPaymentType.SelectedItem = "Multiple Check" Then
                For Each item As DataGridViewRow In dgvMultipleCheck.Rows
                    If item.Cells(dgcBankID.Index).Value <> Nothing Then
                        bankIDMulti = item.Cells(dgcBankID.Index).Value
                        CheckNo = item.Cells(dgcCheckNo.Index).Value
                        updateSQl = " UPDATE tblCV_BankRef " & _
                          " SET   Status ='Cancelled' WHERE CV_No ='" & TransID & "' AND BankID = '" & bankIDMulti & "' AND RefNo ='" & CheckNo & "' "
                        SQL.ExecNonQuery(updateSQl)
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub dgvEntry_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles dgvEntry.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Select Case dgvEntry.SelectedCells(0).ColumnIndex
                Case chRef.Index
                    Dim f As New frmSeachSL
                    f.ShowDialog()
                    If f.strVCECode <> "" Then
                        dgvEntry.Rows.Add("")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = f.strVCECode
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = f.strVCEName
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = f.strAccntCode
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = f.strAccntTitle
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = f.strRefNo
                        Dim selectSQL As String = " SELECT * FROM tblLoan_Type WHERE LoanAccount = '" & f.strAccntCode & "' "
                        SQL.ReadQuery(selectSQL)
                        If SQL.SQLDR.Read Then
                            dgvEntry.Rows.Add("")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = f.strVCECode
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = f.strVCEName
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("IntIncomeAccount").ToString
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(SQL.SQLDR("IntIncomeAccount").ToString)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = f.strRefNo
                        End If
                    End If
            End Select
        End If
    End Sub

    Private Sub FromRFPToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromRFPToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("RFP")
        LoadRFP(f.transID)
        f.Dispose()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Dim f As New frmMasterfile_Bank
        f.ShowDialog()
        f.Dispose()
        LoadBankList()
    End Sub

    Private Sub FromLoanToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromLoanToolStripMenuItem.Click
        If Not AllowAccess("CV_LOAN") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.cbFilter.SelectedItem = "Status"
            f.txtFilter.Text = "Approved"
            f.txtFilter.Enabled = False
            f.cbFilter.Enabled = False
            f.btnSearch.Enabled = False
            f.ShowDialog("Copy Loan")
            If f.batch = True Then
                For Each row As DataGridViewRow In f.dgvList.Rows
                    If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                        LoadLoan(row.Cells(0).Value)
                    End If
                Next
            Else
                LoadLoan(f.transID)
            End If

            f.Dispose()
        End If
    End Sub

#Region "TabControlMethod"
    Public Sub HideTabPageAll()
        If tpOrder Is Nothing Then
            ' The first time the Hide method is called, save the original order of the TabPages
            For Each TabPageCurrent As TabPage In tcPayment.TabPages
                tpOrder.Add(TabPageCurrent.Name)
            Next
        End If
        For Each TabPageCurrent As TabPage In tcPayment.TabPages
            Dim TabPageToHide As TabPage

            ' Get the TabPage object
            TabPageToHide = tcPayment.TabPages(TabPageCurrent.Name)
            ' Add the TabPage to the internal List
            tpHidden.Add(TabPageCurrent.Text, TabPageToHide)
            ' Remove the TabPage from the TabPages collection of the TabControl
            tcPayment.TabPages.Remove(TabPageToHide)
        Next
    End Sub

    Public Sub ShowTabPage(ByVal TabPageName As String)
        If tpHidden.ContainsKey(TabPageName) Then
            Dim TabPageToShow As TabPage

            ' Get the TabPage object
            TabPageToShow = tpHidden(TabPageName)
            ' Add the TabPage to the TabPages collection of the TabControl
            tcPayment.TabPages.Insert(GetTabPageInsertionPoint(TabPageName), TabPageToShow)
            ' Remove the TabPage from the internal List
            tpHidden.Remove(TabPageName)
        End If
    End Sub


    Public Sub ShowTabPageAll()
        For Each kvp As KeyValuePair(Of String, System.Windows.Forms.TabPage) In tpHidden
            Dim v1 As String = kvp.Key
            Dim v2 As TabPage = kvp.Value
            tcPayment.TabPages.Insert(GetTabPageInsertionPoint(v1), v2)
        Next
        tpHidden.Clear()
        '       tpHidden = Nothing
        '       tpOrder = Nothing
    End Sub

    Private Function GetTabPageInsertionPoint(ByVal TabPageName As String) As Integer
        Dim TabPageIndex As Integer
        Dim TabPageCurrent As TabPage
        Dim TabNameIndex As Integer
        Dim TabNameCurrent As String

        For TabPageIndex = 0 To tcPayment.TabPages.Count - 1
            TabPageCurrent = tcPayment.TabPages(TabPageIndex)
            For TabNameIndex = TabPageIndex To tpOrder.Count - 1
                TabNameCurrent = tpOrder(TabNameIndex)
                If TabNameCurrent = TabPageCurrent.Name Then
                    Exit For
                End If
                If TabNameCurrent = TabPageName Then
                    Return TabPageIndex
                End If
            Next
        Next
        Return TabPageIndex
    End Function
#End Region


    Dim eColIndex As Integer = 0
    Private Sub dgvMultipleCheck_CellBeginEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvMultipleCheck.CellBeginEdit
        eColIndex = e.ColumnIndex
    End Sub


    'Start of Cost Center insert to Table
    Dim strDefaultGridBankID As String = ""
    Dim strDefaultGridCheckNo As String = ""
    Dim strDefaultGridCheckDate As String = ""
    Dim strDefaultGridBankDesc As String = ""
    Private Sub dgvMultipleCheck_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMultipleCheck.CellEndEdit
        Dim rowIndex As Integer = dgvMultipleCheck.CurrentCell.RowIndex
        Dim colIndex As Integer = dgvMultipleCheck.CurrentCell.ColumnIndex
        Dim BankIDMulti As Integer
        Try
            Select Case colIndex
                Case dgcCheckDate.Index
                    If Not IsDate(dgvMultipleCheck.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                        dgvMultipleCheck.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Date.Today.Date
                    End If
                Case dgcAmount.Index
                    If IsNumeric(dgvMultipleCheck.Item(e.ColumnIndex, e.RowIndex).Value) Then
                        dgvMultipleCheck.Item(e.ColumnIndex, e.RowIndex).Value = CDec(dgvMultipleCheck.Item(e.ColumnIndex, e.RowIndex).Value).ToString("N2")
                    End If
                Case dgcBank.Index
                    Dim BankDesc As String
                    BankDesc = dgvMultipleCheck.Rows(e.RowIndex).Cells(dgcBank.Index).Value
                    BankIDMulti = GetBankID(BankDesc)
                    LoadBankMultipleDetails(BankIDMulti, e.RowIndex, dgcBankID.Index, dgcCheckNo.Index, dgcCheckDate.Index)
                Case dgcCheckName.Index
                    Dim f As New frmVCE_Search
                    f.txtFilter.Text = dgvMultipleCheck.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                    f.ShowDialog()
                    dgvMultipleCheck.Item(dgcCheckVCECode.Index, e.RowIndex).Value = f.VCECode
                    dgvMultipleCheck.Item(dgcCheckName.Index, e.RowIndex).Value = f.VCEName
                    f.Dispose()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub LoadBankMultipleDetails(ByVal BankIDMulti As Integer, ByVal RowIndex As Integer, ByVal BankIDIndex As Integer, ByVal CheckNoIndex As Integer, ByVal CheckDateIndex As Integer)

        Dim selectSQL As String
        selectSQL = " SELECT  RIGHT('000000000000' + CAST((CAST(MAX(RefNo) AS bigint) + 1) AS nvarchar), LEN(SeriesFrom)) AS RefNo, SeriesFrom " & _
                " FROM    tblCV_BankRef RIGHT JOIN tblBank_Master " & _
                " ON      tblCV_BankRef.BankID = tblBank_Master.BankID " & _
                " AND     tblCV_BankRef.RefNo BETWEEN SeriesFrom AND SeriesTo " & _
                " WHERE   tblBank_Master.BankID = '" & BankIDMulti & "'   " & _
                " GROUP BY LEN(SeriesFrom), SeriesFrom  "
        SQL.ReadQuery(selectSQL, 2)

        strDefaultGridBankID = ""
        strDefaultGridCheckNo = ""
        strDefaultGridCheckDate = ""
        strDefaultGridBankDesc = ""

        While SQL.SQLDR2.Read
            strDefaultGridBankID = BankIDMulti
            If Not IsDBNull(SQL.SQLDR2("RefNo")) Then
                strDefaultGridCheckNo = SQL.SQLDR2("RefNo").ToString()
            Else
                strDefaultGridCheckNo = SQL.SQLDR2("SeriesFrom").ToString()
            End If
            strDefaultGridCheckDate = Date.Today.Date
        End While
        dgvMultipleCheck.Rows(RowIndex).Cells(BankIDIndex).Value = strDefaultGridBankID
        dgvMultipleCheck.Rows(RowIndex).Cells(CheckNoIndex).Value = strDefaultGridCheckNo
        dgvMultipleCheck.Rows(RowIndex).Cells(CheckDateIndex).Value = strDefaultGridCheckDate


    End Sub


    Private Sub dgvMultipleCheck_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dgvMultipleCheck.CurrentCellDirtyStateChanged
        If eColIndex = dgcBank.Index And TypeOf (dgvMultipleCheck.CurrentRow.Cells(dgcBank.Index)) Is DataGridViewComboBoxCell Then
            dgvMultipleCheck.EndEdit()
        End If
    End Sub

    Private Sub tsbDelete_Click(sender As System.Object, e As System.EventArgs) Handles tsbDelete.Click
        If Not AllowAccess("CV_DEL") Then
            msgRestricted()
        Else
            If MsgBox("Are you sure you want to delete this transaction?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Delete") = MsgBoxResult.Yes Then
                Dim updateSQL As String

                Dim deleteSQL As String = "DELETE FROM tblCV WHERE TransID = '" & TransID & "'"
                SQL.ExecNonQuery(deleteSQL)

                deleteSQL = "DELETE FROM tblJE_Header WHERE RefType = 'CV' AND RefTransID = '" & TransID & "'"
                SQL.ExecNonQuery(deleteSQL)

                If LOAN_ID > 0 Then
                    updateSQL = " UPDATE tblLoan SET Status ='Approved', DateRelease = '', RefType = '', RefTransID = '' WHERE TransID = '" & LOAN_ID & "'"
                    SQL.ExecNonQuery(updateSQL)
                End If
                MsgBox("Successfully Deleted!", MsgBoxStyle.Information, "Delete")
                tsbNew.PerformClick()
            End If
        End If
    End Sub

    Private Sub FromFundsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromFundsToolStripMenuItem.Click
        frmFund_Copy.strType = "CV"
        frmFund_Copy.ShowDialog()
    End Sub

    Private Sub txtBankRefName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtBankRefName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.txtFilter.Text = txtBankRefName.Text
            f.ShowDialog()
            txtBankRefName.Text = f.VCEName
            f.Dispose()
        End If
    End Sub

    Private Sub txtBankRefName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtBankRefName.TextChanged

    End Sub

    Private Sub FromPCVToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromPCVToolStripMenuItem.Click


        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("PCVRR-CV")
        LoadPCV(f.transID)
        f.Dispose()
    End Sub

    Public Sub LoadPCV(ByVal PCVID As String)
        Try
            Dim query As String
            query = " SELECT TransID, PCVRR_No, BranchCode, tblPCVRR.VCECode, VCEName " & _
                    " FROM tblPCVRR LEFT JOIN viewVCE_Master " & _
                    " ON tblPCVRR.VCECode = viewVCE_Master.VCECode " & _
                    " WHERE TransID ='" & PCVID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                txtVCEName.Text = SQL.SQLDR("VCEName").ToString
                PCV = SQL.SQLDR("PCVRR_No")

                query = " SELECT tblPCVRR.TransID, AccountCode, AccountTitle, CodePayee, RecordPayee, tblPCV_Details.Type, tblPCV_Details.BaseAmount, tblPCV_Details.InputVAT, " & _
                            " CostCenter, tblPCV_Details.Status, tblPCV_Details.RecordID, CIP_Code   " & _
                          " FROM tblPCVRR " & _
                          " INNER JOIN tblPCVRR_Details ON tblPCVRR.TransID = tblPCVRR_Details.TransID   " & _
                          " INNER JOIN tblPCV_Details ON tblPCVRR_Details.PCV_TransID = tblPCV_Details.TransID   " & _
                          " LEFT JOIN tblRFP_Type ON tblPCV_Details.Type = tblRFP_Type.Type  " & _
                          " LEFT JOIN tblCOA_Master ON tblCOA_Master.AccountCode = tblRFP_Type.DefaultAccount " & _
                          " WHERE tblPCVRR.TransID = '" & PCVID & "' AND tblPCVRR_Details.Status ='Active' "
                SQL.ReadQuery(query)

                Dim rowsCount As Integer = dgvEntry.Rows.Count - 1
                While SQL.SQLDR.Read


                    dgvEntry.Rows.Add(SQL.SQLDR("AccountCode").ToString)
                    dgvEntry.Rows(rowsCount).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                    dgvEntry.Rows(rowsCount).Cells(chDebit.Index).Value = CDec(SQL.SQLDR("BaseAmount")).ToString("N2")
                    dgvEntry.Rows(rowsCount).Cells(chCredit.Index).Value = "0.00"
                    dgvEntry.Rows(rowsCount).Cells(chVCECode.Index).Value = SQL.SQLDR("CodePayee").ToString
                    dgvEntry.Rows(rowsCount).Cells(chVCEName.Index).Value = SQL.SQLDR("RecordPayee").ToString
                    dgvEntry.Rows(rowsCount).Cells(chParticulars.Index).Value = SQL.SQLDR("Type").ToString
                    dgvEntry.Rows(rowsCount).Cells(chRef.Index).Value = "PCVRR:" & PCV
                    dgvEntry.Rows(rowsCount).Cells(dgcBranchCode.Index).Value = BranchCode
                    dgvEntry.Rows(rowsCount).Cells(chCostID.Index).Value = SQL.SQLDR("CostCenter").ToString
                    dgvEntry.Rows(rowsCount).Cells(chCost_Center.Index).Value = GetCCName(SQL.SQLDR("CostCenter").ToString)
                    dgvEntry.Rows(rowsCount).Cells(chRecordID.Index).Value = SQL.SQLDR("RecordID").ToString

                    If SQL.SQLDR("InputVAT") <> 0 Then
                        rowsCount += 1
                        dgvEntry.Rows.Add(accntInputVAT)
                        dgvEntry.Rows(rowsCount).Cells(chAccntTitle.Index).Value = accntTitleInputVAT
                        dgvEntry.Rows(rowsCount).Cells(chDebit.Index).Value = CDec(SQL.SQLDR("InputVAT")).ToString("N2")
                        dgvEntry.Rows(rowsCount).Cells(chCredit.Index).Value = "0.00"
                        dgvEntry.Rows(rowsCount).Cells(chVCECode.Index).Value = SQL.SQLDR("CodePayee").ToString
                        dgvEntry.Rows(rowsCount).Cells(chVCEName.Index).Value = SQL.SQLDR("RecordPayee").ToString
                        dgvEntry.Rows(rowsCount).Cells(chParticulars.Index).Value = SQL.SQLDR("Type").ToString
                        dgvEntry.Rows(rowsCount).Cells(chRef.Index).Value = "PCVRR:" & PCV
                        dgvEntry.Rows(rowsCount).Cells(dgcBranchCode.Index).Value = BranchCode
                        dgvEntry.Rows(rowsCount).Cells(chCostID.Index).Value = SQL.SQLDR("CostCenter").ToString
                        dgvEntry.Rows(rowsCount).Cells(chCost_Center.Index).Value = GetCCName(SQL.SQLDR("CostCenter").ToString)
                    End If

                    rowsCount += 1
                End While
            End If
            LoadCurrency()
            TotalDBCR()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    'Private Sub LoadPCV(ByVal PCV_NO As String)
    '    Dim query As String

    '    Try
    '        query = " SELECT Ref_TransID AS TransID, PCV_No, VCECode, Supplier AS  VCEName, Date AS Date_PCV, Amount , Remarks,  AccntCode, AccountTitle " & _
    '                " FROM View_PCV_Balance " & _
    '                " WHERE  Ref_TransID ='" & PCV_NO & "' "
    '        SQL.ReadQuery(query)
    '        If SQL.SQLDR.Read Then
    '            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
    '            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
    '            dgvEntry.Rows.Add(BranchCode, SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccountTitle").ToString, CDec(SQL.SQLDR("Amount")).ToString("N2"), "0.00", SQL.SQLDR("VCECode").ToString, SQL.SQLDR("VCEName").ToString, "", "PCV:" & SQL.SQLDR("PCV_No").ToString)
    '            LoadBranch()
    '        End If
    '        TotalDBCR()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub FromCAToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromCAToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.chkBatch.Visible = True
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("CA")

        If f.batch = True Then
            For Each row As DataGridViewRow In f.dgvList.Rows
                If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                    LoadCA(row.Cells(0).Value)
                End If
            Next
        Else
            If f.transID <> "" Then
                LoadCA(f.transID)
            End If
        End If
        f.Dispose()
    End Sub

    Private Sub btnUOMGroup_Click(sender As System.Object, e As System.EventArgs) Handles btnUOMGroup.Click
        frmDisbursement_Type.ShowDialog()
        LoadDisbursementType()
    End Sub

    Private Sub btnTax_Click(sender As System.Object, e As System.EventArgs) Handles btnTax.Click
        If txtAmount.Text <> "" Then
            Dim f As New frmTaxComputation
            Dim VATamount As Decimal
            Dim VCECode As String
            VCECode = txtVCECode.Text
            VATamount = CDec(txtAmount.Text).ToString("N2")
            f.ShowDialog(VATamount, "PJ", "", VCECode)

            dgvEntry.Rows.Add("")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(f.GrossAmount).ToString("N2")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = f.VATType
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = f.VCECode
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(f.VCECode)


            If f.VATAmount <> 0 Then
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = f.TAX_IV
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(f.TAX_IV)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(f.VATAmount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = "Input VAT"
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = f.VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(f.VCECode)

            End If
            If f.EWTAmount <> 0 Then
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = f.TAX_EWT
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(f.TAX_EWT)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(f.EWTAmount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = "EWT"
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = f.VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(f.VCECode)


            End If
            TotalDBCR()
        End If
    End Sub

    Private Sub dgvEntry_CellLeave(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntry.CellLeave

    End Sub

    Private Sub FromPTToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromPTToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False

        f.ShowDialog("PT")
        LoadPT(f.transID)
        f.Dispose()
    End Sub

    Private Sub LoadPT(ByVal ID As String)
        Try
            Dim PTAccount As String = LoadPTAccount()
            Dim PTTitle As String = GetAccntTitle(PTAccount)
            Dim query As String
            query = " SELECT ID, PeriodMonth, PeriodYear, PeriodQuarter, Due " & _
                    " FROM   tblPT " & _
                    " WHERE  ID ='" & ID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = PTAccount
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = PTTitle
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(SQL.SQLDR("Due")).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode

                LoadBranch()
            End If
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function LoadPTAccount() As String
        Dim query As String
        query = " SELECT TAX_Percentage FROM tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("TAX_Percentage").ToString()
        Else
            Return ""
        End If
    End Function

    Private Sub cbBankTransfer_Bank_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbBankTransfer_Bank.SelectedIndexChanged
        Try
            If disableEvent = False Then
                If cbBankTransfer_Bank.SelectedIndex <> -1 Then
                    bankID = GetBankID(cbBankTransfer_Bank.SelectedItem)
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub tsbDownload_Click(sender As Object, e As EventArgs) Handles tsbDownload.Click
        Dim fileName As String = "CV Uploader.xlsx"
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
            For i As Integer = 0 To 14
                If i = 0 Then
                    xlWorkSheet.Cells(1, i + 1) = templateName
                End If
                xlWorkSheet.Cells(2, i + 1) = dgvEntry.Columns(i).Name
                xlWorkSheet.Cells(3, i + 1) = dgvEntry.Columns(i).HeaderText
            Next
            xlWorkSheet.Protect(excelPW)
            Dim ctr As Integer = 1
            Do
                fileName = "CV Uploader -" & ctr.ToString & ".xlsx"
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

    Private Sub tsbUpload_Click(sender As Object, e As EventArgs) Handles tsbUpload.Click
        If tsbUpload.Text = "Upload" Then
            With (OpenFileDialog1)
                .InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                .Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx"
                .FilterIndex = 2
            End With
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                If MessageBox.Show("Uploading CV" & vbNewLine & "Are you sure you want to Contiue?", "Message Alert", MessageBoxButtons.YesNo) = MsgBoxResult.Yes Then
                    path = OpenFileDialog1.FileName
                    dgvEntry.Rows.Clear()
                    'dgvEntry.ReadOnly = True
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
                    If dgvEntry.Columns.Count > j + addlCol Then

                        If j = chAccntCode.Index Then
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

                            AddValue(ObjectText, rowCount - 1, j)
                            ' Check if AccountCode exist on Masterfile.
                            If Not validateAccountCode(ObjectText) Then
                                ' if not exist, change color.
                                ChangeCellColor(rowCount - 1, j)
                                Valid = False
                            Else
                                ' if existing  AccountCode
                                dgvEntry.Item(j + 1, rowCount - 1).Value = GetAccntTitle(ObjectText)
                            End If
                        ElseIf j = chAccntTitle.Index Then
                            ' Check if has valid AccountCode
                            If dgvEntry.Item(j - 1, rowCount - 1).Value = "" Then
                                ' if not check VCEref on excel template
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)

                            End If
                        ElseIf j = chDebit.Index Then
                            ' Debit
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chCredit.Index Then
                            ' Credit
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If

                        ElseIf j = chVCECode.Index Then
                            ' Check if has valid VCEcode
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                ' if not check VCEref on excel template
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chVCEName.Index Then
                            ' Check if has valid VCEcode
                            If ObjectText <> "" Then
                                If Not validateVCE(ObjectText) Then
                                    ' if not exist, change color.
                                    ChangeCellColor(rowCount - 1, j - 1)
                                    Valid = False
                                Else
                                    ' if existing  AccountCode
                                    dgvEntry.Item(j, rowCount - 1).Value = GetVCEName(ObjectText)
                                End If
                            End If
                        ElseIf j = chParticulars.Index Then
                            ' Particulars
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chRef.Index Then
                            ' REfno
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                ' if not check VCEref on excel template
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chCostID.Index Then
                            ' Check if has valid COST ID
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                ' if not check VCEref on excel template
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chCost_Center.Index Then
                            ' Check if has valid Cost ID
                            If ObjectText <> "" Then
                                If Not validateCostID(ObjectText) Then
                                    ' if not exist, change color.
                                    ChangeCellColor(rowCount - 1, j - 1)
                                    Valid = False
                                Else
                                    ' if existing  CostCenter
                                    dgvEntry.Item(j, rowCount - 1).Value = GetCCName(ObjectText)
                                End If
                            End If
                        ElseIf j = chProfit_Code.Index Then
                            ' Check if has valid Profit ID
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                ' if not check VCEref on excel template
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chProfit_Center.Index Then
                            ' Check if has valid Profit ID
                            If ObjectText <> "" Then
                                If Not validateProfitID(ObjectText) Then
                                    ' if not exist, change color.
                                    ChangeCellColor(rowCount - 1, j - 1)
                                    Valid = False
                                Else
                                    ' if existing  Profit Center
                                    dgvEntry.Item(j, rowCount - 1).Value = GetPCName(ObjectText)
                                End If
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
                If dgvEntry.Rows.Count > 1 Then
                    MsgBox(dgvEntry.Rows.Count - 1 & " File Data Uploaded Successfully!", vbInformation)
                Else
                    MsgBox(dgvEntry.Rows.Count & " File Data Uploaded Successfully!", vbInformation)
                End If

            Else
                MsgBox("Some data are invalid !, Please Check highlighted cells.", MsgBoxStyle.Exclamation)
            End If
            If dgvEntry.Rows.Count > 1 Then
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).ReadOnly = True
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
            dgvEntry.Rows.Add("")
        End If
    End Sub


    Private Delegate Sub AddValueInvoker(ByVal Value As String, ByVal row As Integer, ByVal col As Integer)
    Private Sub AddValue(ByVal Value As String, ByVal row As Integer, ByVal col As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New AddValueInvoker(AddressOf AddValue), Value, row, col)
        Else
            dgvEntry.Item(col, row).Value = Value
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
            dgvEntry.Rows(row).Cells(col).Style.BackColor = Color.Yellow
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

    Public Function validateAccountCode(ByVal AccntCode As String) As Boolean
        Try
            Dim query As String
            query = " SELECT    * " &
                    " FROM      tblCOA_Master " &
                    " WHERE     AccountCode = @AccountCode "
            SQL.FlushParams()
            SQL.AddParam("@AccountCode", AccntCode)
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

    Private Sub cbBankDM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbBankDM.SelectedIndexChanged
        Try
            If disableEvent = False Then
                If cbBankDM.SelectedIndex <> -1 Then
                    bankID = GetBankID(cbBankDM.SelectedItem)
                    If cbPaymentType.SelectedItem = "Debit Memo" Then
                        txtDMRef.Text = GetDMNo(bankID)
                    End If
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CVListToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CVListToolStripMenuItem.Click
        Dim f As New frmReport_Filter
        f.Report = "CV List"
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub txtAmount_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtAmount.TextChanged

    End Sub
    Private Sub LoadCALiquidation(ByVal CA As String)
        Try
            Dim query As String
            query = " SELECT  TransID, [CA No.], Date, VCECode, VCEName, CA_Amount, Balance * -1 AS Balance," & _
                    " Remarks, AccntCode, AccountTitle, Status" & _
                    " FROM   View_CA_Balance " & _
                    " WHERE  TransID ='" & CA & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                CA_ID = SQL.SQLDR("TransID")
                'txtCARef.Text = SQL.SQLDR("CV_No")
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                txtVCEName.Text = SQL.SQLDR("VCEName").ToString
                txtBankRefName.Text = SQL.SQLDR("VCEName").ToString
                txtRemarks.Text = SQL.SQLDR("Remarks").ToString
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("AccntCode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(SQL.SQLDR("Balance")).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "CA:" & SQL.SQLDR("CA No.").ToString
            End If

            LoadBranch()
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FromCALiquidationToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromCALiquidationToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        'f.txtFilter.Text = "Active"
        f.txtFilter.Text = "Released"
        f.chkBatch.Visible = True
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("CA-Liquidation")
        If f.batch = True Then
            For Each row As DataGridViewRow In f.dgvList.Rows
                If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                    LoadCALiquidation(row.Cells(0).Value)
                End If
            Next
        Else
            If f.transID <> "" Then
                LoadCALiquidation(f.transID)
            End If
        End If
        f.Dispose()
    End Sub

    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged

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

    Private Sub FromRRToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromRRToolStripMenuItem.Click

        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("RR-APV")
        LoadRR(f.transID)
        f.Dispose()
    End Sub

    Private Sub LoadRR(ByVal ID As String)
        Dim query As String
        query = "  SELECT    tblRR.TransID, tblRR.RR_No, DateRR AS Date, tblRR.VCECode, viewVCE_Master.VCEName,  " & _
                " ISNULL(ADV_Amount,0) AS Advances,  Credit AS NetPurchase,  " & _
                " tblRR.Remarks,  ADV.AccntCode  AS ADVAccount, View_GL.AccntCode as APPendingAccount  " & _
                " FROM tblRR " & _
                " INNER JOIN viewVCE_Master  ON  " & _
                " tblRR.VCECode = viewVCE_Master.VCECode " & _
                " INNER JOIN View_GL  ON " & _
                "  tblRR.RR_No = CAST(REPLACE(View_GL.RefNo,'RR:','') AS nvarchar) " & _
                " LEFT JOIN " & _
                " (  SELECT PO_Ref, AccntCode, SUM(ADV_Amount) AS ADV_Amount " & _
                "  FROM tblADV " & _
                " WHERE Status ='Closed' GROUP BY PO_Ref, AccntCode) AS ADV " & _
                " ON        tblRR.PO_Ref  = ADV.PO_Ref " & _
                " WHERE      tblRR.TransID ='" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            RR_RefID = SQL.SQLDR("TransID")
            txtRRRef.Text = SQL.SQLDR("RR_No").ToString
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
            txtAmount.Text = CDec(SQL.SQLDR("NetPurchase")).ToString("N2")
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            LoadCurrency()
            GenerateEntryRR(ID)
        End If
    End Sub

    Private Sub GenerateEntryRR(ByVal PO_ID As Integer)
        Dim query As String
        Dim amount As Decimal = 0
 
            'accntCR = GetAccntCode(cbCreditAccount.SelectedItem)
        query = " SELECT RefNo, SUM(Credit-Debit) AS Debit, View_GL.VCECode, VCEName  " & _
                " FROM View_GL  INNER JOIN tblRR ON tblRR.RR_No = CAST(REPLACE(View_GL.RefNo,'RR:','') AS nvarchar) WHERE tblRR.TransID = '" & PO_ID & "' AND AccntCode ='" & accntAPpending & "' " & _
                " GROUP BY RefNo, View_GL.VCECode, VCEName  "
            SQL.GetQuery(query)
            dgvEntry.Rows.Clear()
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                    dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = accntAPpending
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(accntAPpending)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(row.Item(1)).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = txtVCECode.Text
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = txtVCEName.Text
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = row.Item(0).ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = ""

                    amount += row.Item(1)
            Next
            TotalDBCR()
        End If

    End Sub

    Private Sub OutstandingChequeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OutstandingChequeToolStripMenuItem.Click
        Dim updateSQl, CheckNo As String
        Dim bankIDMulti As Integer
        If MsgBox("Tagging cheque as Outstanding" & vbNewLine & "Click Yes to Confirm", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.Yes Then
            If cbPaymentType.SelectedItem = "Check" Then

                updateSQl = " UPDATE tblCV_BankRef " &
                            " SET   Status ='Released' WHERE CV_No ='" & TransID & "' AND BankID = '" & bankID & "' AND RefNo ='" & txtBankRef.Text & "' "
                SQL.ExecNonQuery(updateSQl)
                txtRefStatus.Text = "Released"
            ElseIf cbPaymentType.SelectedItem = "Multiple Check" Then
                For Each item As DataGridViewRow In dgvMultipleCheck.Rows
                    If item.Cells(dgcBankID.Index).Value <> Nothing Then
                        bankIDMulti = item.Cells(dgcBankID.Index).Value
                        CheckNo = item.Cells(dgcCheckNo.Index).Value
                        updateSQl = " UPDATE tblCV_BankRef " &
                          " SET   Status ='Released' WHERE CV_No ='" & TransID & "' AND BankID = '" & bankIDMulti & "' AND RefNo ='" & CheckNo & "' "
                        SQL.ExecNonQuery(updateSQl)
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub StailedChequeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StailedChequeToolStripMenuItem.Click
        Dim updateSQl, CheckNo As String
        Dim bankIDMulti As Integer
        If MsgBox("Tagging cheque as Stailed" & vbNewLine & "Click Yes to Confirm", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.Yes Then
            If cbPaymentType.SelectedItem = "Check" Then

                updateSQl = " UPDATE tblCV_BankRef " &
                            " SET   Status ='Stailed' WHERE CV_No ='" & TransID & "' AND BankID = '" & bankID & "' AND RefNo ='" & txtBankRef.Text & "' "
                SQL.ExecNonQuery(updateSQl)

                txtRefStatus.Text = "Stailed"
            ElseIf cbPaymentType.SelectedItem = "Multiple Check" Then
                For Each item As DataGridViewRow In dgvMultipleCheck.Rows
                    If item.Cells(dgcBankID.Index).Value <> Nothing Then
                        bankIDMulti = item.Cells(dgcBankID.Index).Value
                        CheckNo = item.Cells(dgcCheckNo.Index).Value
                        updateSQl = " UPDATE tblCV_BankRef " &
                          " SET   Status ='Stailed' WHERE CV_No ='" & TransID & "' AND BankID = '" & bankIDMulti & "' AND RefNo ='" & CheckNo & "' "
                        SQL.ExecNonQuery(updateSQl)
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub FromATDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromATDToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.chkBatch.Visible = True
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False

        f.ShowDialog("ATD-CV")
        If f.batch = True Then
            For Each row As DataGridViewRow In f.dgvList.Rows
                If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                    LoadATD(row.Cells(0).Value)
                End If
            Next
        Else
            If f.transID <> "" Then
                LoadATD(f.transID)
            End If
        End If
        f.Dispose()
    End Sub

    Private Sub LoadATD(ByVal ATD As String)
        Try
            Dim query As String
            query = " SELECT TransID, ATD_No, VCECode, VCEName, DateATD, Total_Amount, Account_Code, AccountTitle " & _
                    " FROM    viewATD_Balance " & _
                    " WHERE  TransID ='" & ATD & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                txtVCEName.Text = SQL.SQLDR("VCEName").ToString
                APV_ID = SQL.SQLDR("TransID")
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("Account_Code").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(SQL.SQLDR("Total_Amount")).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "ATD:" & SQL.SQLDR("ATD_No")
                LoadBranch()
            End If
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FromRealEstateCommissionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromRealEstateCommissionToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.chkBatch.Visible = True
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("RE-COMM")

        If f.batch = True Then
            For Each row As DataGridViewRow In f.dgvList.Rows
                If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                    LoadRE_Comm(row.Cells(0).Value)
                End If
            Next
        Else
            If f.transID <> "" Then
                LoadRE_Comm(f.transID)
            End If
        End If
        f.Dispose()
    End Sub

    Private Sub LoadRE_Comm(ByVal COM As String)
        Try
            Dim query As String
            query = " 	SELECT TransID, TransNo, TransDate as Date, Code as VCECode, Name as VCEName, TotalAmount as Amount, '0.00' as Credit, Remarks, '' as Particulars,   " &
                    "    '' as CostID, TransNO as RefNo, 'Credit' AS Nature, '' as VatType, AccountCode, AccountTitle  " &
                    " 	FROM View_CommissionAmount   " &
                    " 		CROSS JOIN  " &
                    "  			( SELECT tblSystemSetup.RE_Commission as AccountCode, tblCOA_Master.AccountTitle as AccountTitle   " &
                    " 	FROM tblSystemSetup     " &
                    "  		INNER JOIN tblCOA_Master on tblSystemSetup.RE_Commission = tblCOA_Master.AccountCode  ) ComAccount  " &
                    "     WHERE  TransID ='" & COM & "'  "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                CA_ID = SQL.SQLDR("TransID")
                txtCARef.Text = SQL.SQLDR("TransNo")
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                txtVCEName.Text = SQL.SQLDR("VCEName").ToString
                txtBankRefName.Text = SQL.SQLDR("VCEName").ToString
                txtRemarks.Text = SQL.SQLDR("Remarks").ToString
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("AccountCode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(SQL.SQLDR("Amount")).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "RE_COMM:" & SQL.SQLDR("TransNo").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode
            End If
            LoadBranch()
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub dgvEntry_ColumnWidthChanged(sender As Object, e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles dgvEntry.ColumnWidthChanged

    End Sub

    Private Sub ConvertCheque_Click(sender As Object, e As EventArgs) Handles ConvertCheque.Click
        Dim TW As System.IO.TextWriter
        Dim CheckConvert As String = Nothing
        Dim CheckConvert1 As String = Nothing
        Dim currentDateTime As String = DateTime.Now.ToString("yyyyMMddHHmmss")
        Dim numberWithCommas As String = txtAmount.Text
        Dim AmountCheck As String = numberWithCommas.Replace(",", "")
        'Dim selectedFolder As String = cbCostCenter.SelectedItem.ToString()

        If cbCostCenter.SelectedItem = "" Then
            Dir = App_Path.ToString.Replace("\Debug", "") & "\CheckConverter\"
        Else
            Dir = App_Path.ToString.Replace("\Debug", "") & "\CheckConverter\" & cbCostCenter.SelectedItem & "\"
        End If
        'Dir = App_Path.ToString.Replace("\Debug", "") & "\CheckConverter\" & cbCostCenter.Text
        TW = System.IO.File.CreateText(Dir + "" & BnkAcctNo & currentDateTime & ".txt")

        Dim ctr As Integer
        'Do While ctr < nupQty.Value 
        '    Dim QRCODE As String
        '    Dim a As String()
        '    Dim weight As Double = 0
        '    If txtQRWeight.Text.Contains("/") Then mm m
        '        a = txtQRWeight.Text.Split("/")
        '        If IsNumeric(a(0)) AndAlso IsNumeric(a(1)) Then
        '            weight = CDbl(a(0) / a(1))
        '        End If
        '    Else
        '        weight = Double.Parse(txtQRWeight.Text.Trim)
        '    End If

        '    QRCODE = txtItemCode.Text.Trim & "\" & ctr

        'QR = txtItemCode.Text.Trim & "|" & txtItemName.Text.Trim & "|" & txtQRWeight.Text.Trim & " GRAM/S|" & QRCODE & "|Mfr:" & dtpManufacturingDate.Value.Day & MonthName(dtpManufacturingDate.Value.Month, True) & dtpManufacturingDate.Value.Year.ToString.Substring(4 - 2) & "|Exp:" & dtpExpirationDate.Value.Day & MonthName(dtpExpirationDate.Value.Month, True) & dtpExpirationDate.Value.Year.ToString.Substring(4 - 2) & "|LotNo:" & txtLotNo.Text
        'CheckConvert = "H|1" & txtAmount.Text.Trim &
        '    CheckConvert = "H|1" & txtAmount.Text.Trim & "|" & txtQRWeight.Text.Trim & " GRAM/S|" & QRCODE & "|Mfr:" & dtpManufacturingDate.Value.Day & MonthName(dtpManufacturingDate.Value.Month, True) & dtpManufacturingDate.Value.Year.ToString.Substring(4 - 2) & "|Exp:" & dtpExpirationDate.Value.Day & MonthName(dtpExpirationDate.Value.Month, True) & dtpExpirationDate.Value.Year.ToString.Substring(4 - 2) & "|LotNo:" & txtLotNo.Text
        '    TW.WriteLine(CheckConvert & vbCrLf & CheckConvert1)
        '    ctr = ctr + 1
        'Loop
        CheckConvert = "H|1|" & AmountCheck
        CheckConvert1 = "D|1" & txtTransNum.Text.Trim & "|" & AmountCheck & "|" & BnkAcctNo & "|" & dtpDocDate.Value & "|" & txtVCEName.Text & "||0|" & txtRemarks.Text.Trim & "|||||||||||||||||"
        TW.WriteLine(CheckConvert.Trim)
        TW.WriteLine(CheckConvert1.Trim)
        ctr = ctr + 1

        TW.Flush()
        TW.Close()
        MsgBox("Textfile Convertion Succesfully!")
    End Sub

    Private Sub cbCostCenter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCostCenter.SelectedIndexChanged
        If cbCostCenter.SelectedItem = "" Then
            For Each item As DataGridViewRow In dgvEntry.Rows
                item.Cells(chCostID.Index).Value = String.Empty
                item.Cells(chCost_Center.Index).Value = String.Empty
            Next
        End If
    End Sub

    Private Sub FromCSFeeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromCSFeeToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "VCE Name"
        f.txtFilter.Text = txtVCEName.Text
        f.chkBatch.Visible = True
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False

        f.ShowDialog("CS-CV")
        If f.batch = True Then
            For Each row As DataGridViewRow In f.dgvList.Rows
                If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                    LoadCS(row.Cells(0).Value)
                End If
            Next
        Else
            If f.transID <> "" Then
                LoadCS(f.transID)
            End If
        End If
        f.Dispose()
    End Sub


    Private Sub LoadCS(ByVal CS As String)
        Try
            Dim query As String
            query = " SELECT TransID,  ViewCS_Balance_Charges.VCECode, VCEName " & _
                    " FROM ViewCS_Balance_Charges " & _
                    " LEFT JOIN viewVCE_Master " & _
                    " ON viewVCE_Master.VCECode = ViewCS_Balance_Charges.VCECode " & _
                    " WHERE  TransID ='" & CS & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                txtVCEName.Text = SQL.SQLDR("VCEName").ToString
                LoadCSCharges(CS, txtVCECode.Text)
            End If
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub LoadCSCharges(ByVal ID As Integer, ByVal VCECode As String)
        Dim query As String


        query = " SELECT TransID, CS_No, DefaultAccount, VCECode, NetAmount " & _
                " FROM  viewCS_Amount_Charges LEFT JOIN tblCOA_Master " & _
                " ON     tblCOA_Master.AccountCode = viewCS_Amount_Charges.DefaultAccount " & _
                " WHERE TransID = " & ID & " AND VCECode =  '" & VCECode & "'"
        SQL.ReadQuery(query)
        Dim Amount As Decimal = 0
        Dim rowsCount As Integer = 0
        If SQL.SQLDR.HasRows Then
            While SQL.SQLDR.Read
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(SQL.SQLDR("VCECode").ToString)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("DefaultAccount").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(SQL.SQLDR("DefaultAccount").ToString)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(SQL.SQLDR("NetAmount")).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "CS:" & SQL.SQLDR("CS_No").ToString
            End While
        End If
    End Sub

    Private Sub FromPJToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromPJToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        If txtVCECode.Text <> "" Then
            f.cbFilter.SelectedItem = "VCE Code"
            f.txtFilter.Text = txtVCECode.Text
        Else
            f.cbFilter.SelectedItem = "Status"
            f.txtFilter.Text = "Active"
        End If
        f.chkBatch.Visible = True
        f.txtFilter.Enabled = True
        f.cbFilter.Enabled = True
        f.btnSearch.Enabled = True

        f.ShowDialog("APVCV-Payables")

        If f.batch = True Then
            For Each row As DataGridViewRow In f.dgvList.Rows
                If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                    LoadPayables(row.Cells(0).Value)
                End If
            Next
        Else
            If f.transID <> "" Then
                LoadPayables(f.transID)
            End If
        End If
        TotalDBCR()
        f.Dispose()
    End Sub

    Private Sub LoadPayables(ByVal Reference As String)
        Try
            Dim query As String
            Dim VCECode As String = ""
            Dim VCEName As String = ""
            Dim OV_Account As String = ""
            Dim DOV_Account As String = ""
            Dim AP_Account As String = ""
            Dim Balance As Decimal = 0
            Dim VatAmount As Decimal = 0
            query = " SELECT   Reference, VCECode, VCEName, AppDate, Balance , Remarks,  AccntCode, AccntTitle " & _
                    " FROM    view_AP_Balance " & _
                    " WHERE   Reference ='" & Reference & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                VCECode = SQL.SQLDR("VCECode").ToString
                VCEName = SQL.SQLDR("VCEName").ToString
                AP_Account = SQL.SQLDR("AccntCode").ToString
                Balance = SQL.SQLDR("Balance")
                txtVCECode.Text = VCECode
                txtVCEName.Text = VCEName
            End If
            Dim ref As String() = Reference.Split(":")
            query = " SELECT ISNULL(SUM(Credit-Debit),0) AS Amount FROM view_GL WHERE RefType ='" & ref(0) & "' AND TransNo = '" & ref(1) & "' AND AccntCode = (SELECT TAX_DOV FROM tblSystemSetup ) "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read AndAlso SQL.SQLDR("Amount") > 0 Then
                VatAmount = SQL.SQLDR("Amount")
                query = " SELECT ISNULL(TAX_OV,'') AS TAX_OV, ISNULL(TAX_DOV,'') AS TAX_DOV FROM tblSystemSetup  "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    OV_Account = SQL.SQLDR("TAX_OV").ToString
                    DOV_Account = SQL.SQLDR("TAX_DOV").ToString
                End If
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = DOV_Account
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(DOV_Account)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(VatAmount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = VCEName
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Reference


                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = OV_Account
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(OV_Account)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(VatAmount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = VCEName
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Reference

                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = AP_Account
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(AP_Account)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(Balance).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = VCEName
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Reference


            Else
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = AP_Account
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(AP_Account)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(Balance).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = VCEName
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Reference

            End If
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FromBookingToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromBookingToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False

        f.ShowDialog("BM-CV")
        LoadBM(f.transID)
        f.Dispose()
    End Sub

    Private Sub LoadBM(ByVal BM As String)
        Try
            Dim query As String
            query = " SELECT Ref_TransID AS TransID, BM_No, VCECode, Supplier AS  VCEName, Date AS Date_BM, Amount AS Net_Purchase, Amount/1.12 * 0.12 AS Input_VAT, Amount/1.12 * 0.02 AS EWT, Remarks,  AccntCode, AccountTitle " &
                    " FROM   viewBM_Balance " &
                    " WHERE  Ref_TransID ='" & BM & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                txtVCEName.Text = SQL.SQLDR("VCEName").ToString
                BM_ID = SQL.SQLDR("TransID")
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("AccntCode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(SQL.SQLDR("Net_Purchase")).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "BM:" & SQL.SQLDR("BM_No")


                LoadBranch()
            End If
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FromTerminalPayToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromTerminalPayToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("TP-CV")
        LoadTP(f.transID)
        f.Dispose()
    End Sub

    Private Sub LoadTP(TransID As String)
        SetPayrollDatabase()
        Dim query As String
        query = " SELECT    TransID, TP_No, DateTP, Emp_ID, EmployeeName, Clearance, " &
                "           SeparationID, Separation_Type, Separation_Date, Separation_Reason, TP.Status " &
                " FROM      " & database & ".dbo.tblTP AS TP INNER JOIN viewSeparatedEmployee " &
                " ON        TP.EmpID = viewSeparatedEmployee.Emp_ID " &
                " WHERE     TransID = @TransID"
        SQL_RUBY.FlushParams()
        SQL_RUBY.AddParam("@TransID", TransID)
        SQL_RUBY.ReadQuery(query)
        Dim amount As Decimal = 0
        If SQL_RUBY.SQLDR.Read Then
            txtVCECode.Text = SQL_RUBY.SQLDR("Emp_ID").ToString
            txtVCEName.Text = SQL_RUBY.SQLDR("EmployeeName").ToString
            TP_ID = SQL_RUBY.SQLDR("TransID")
            TP_Ref = SQL_RUBY.SQLDR("TP_No").ToString
            LoadLastPay(txtVCECode.Text)
        End If
    End Sub

    Private Sub LoadLastPay(EmpID As String)
        Dim query As String
        Dim total As Decimal = 0
        dgvEntry.Rows.Clear()
        query = " SELECT AccntCode, AccntTitle, SUM(Credit) AS Amount " &
                " FROM view_GL WHERE VCECode = @VCECode AND AccntCode IN (SELECT AccntCode FROM tblDefaultAccount WHERE Type ='TP') " &
                " GROUP BY AccntCode, AccntTitle  " &
                " HAVING SUM(Credit) <> 0 "
        SQL.FlushParams()
        SQL.AddParam("@VCECode", EmpID)
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvEntry.Rows.Add("")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("AccntCode").ToString
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccntTitle").ToString
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(SQL.SQLDR("Amount")).ToString("N2")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = txtVCECode.Text
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = txtVCEName.Text
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcBranchCode.Index).Value = BranchCode
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "TP:" & TP_Ref
        End While
        LoadBranch()
        LoadCurrency()
        TotalDBCR()
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
End Class