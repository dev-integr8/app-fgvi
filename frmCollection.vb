Public Class frmCollection
    Public TransType, Book As String
    Dim TransID, RefID, JETransiD, DBAccount As String
    Dim TransNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "OR"
    Dim ColumnID As String = "TransID"
    Dim ColumnPK As String = "TransNo"
    Dim DBTable As String = "tblCollection"
    Dim TransAuto As Boolean
    Dim ForApproval As Boolean = False
    Dim AccntCode As String
    Dim VATType As String
    Dim bankID As Integer = 0
    Dim RE_ID As Integer
    Dim RE_SchedNum As Integer
    Dim VATAmount As Decimal = 0
    Dim SI_ID, CA_ID As Integer
    Public TAX_DOV, TAX_EWT, TAX_VatPayable, TAX_CWT, TAX_OV As String
    Dim isClearingEnabled As Boolean = False
    Dim isReversalEntry As Boolean = False


    'Dim DBAccount, DBTitle As String
    'Public VCECode, VCEName, CashPayment, CheckNO, BankName, Amount, DocNum, BSNO, PRNO, ORNO, WithholdingTax As String
    'Public CheckDate, DocDate, ApplicationDate, TaxDate As Date
    'Public billing_Period As String
    'Public BankAccountNo, Bank, BankBranch, BankAccountCode, BankAccountTitle As String
    'Dim EnableEvent As Boolean = True
    'Dim a As String
    'Dim activityResult As Boolean = True
    'Dim allowEdit As Boolean = True
    'Dim allowEvent As Boolean = True

    Dim transactionCleared As Boolean = False
    Dim accountCleared As String = ""
    Dim dateCleared As String = ""

    Public Overloads Function ShowDialog(ByVal DocNumber As String) As Boolean
        TransID = DocNumber
        MyBase.ShowDialog()
        Return True
    End Function
    Protected Sub LoadCollector()
        Dim query As String
        query = "SELECT  TOP (100) PERCENT Collector_ID, Collector_Name FROM  tblCollector_Master WHERE Status ='Active' ORDER BY Collector_Name"
        SQL.ReadQuery(query)
        cbCollector.Items.Clear()
        While SQL.SQLDR.Read
            cbCollector.Items.Add(SQL.SQLDR("Collector_Name"))
        End While
    End Sub

    Private Sub frmCollection_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Text = "Collection - " & TransType
            Label5.Text = TransType & " No. :"
            TransAuto = GetTransSetup(TransType)
            ForApproval = GetTransApproval(TransType)
            isReversalEntry = GetTransisReversal(ModuleID)
            LoadBankList()
            LoadSetup()
            LoadCollector()
            LoadPaymentType()
            LoadCollectionType()
            LoadCollectionCompany()
            LoadCostCenter()

            If cbPaymentType.Items.Count > 0 Then
                cbPaymentType.SelectedIndex = 0
            End If
            If cbCostCenter.Items.Count > 0 Then
                cbCostCenter.SelectedIndex = 0
            End If
            dtpDate.Value = Date.Today.Date

            If TransID <> "" Then
                If Not AllowAccess(TransType & "_VIEW") Then
                    msgRestricted()
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
                    LoadCollection(TransID)
                End If
            Else
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
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, TransType)
        End Try
    End Sub


    Private Sub LoadSetup()
        Dim query As String
        query = " SELECT  TAX_DOV, TAX_EWT, TAX_VatPayable, TAX_CWT, TAX_OV, ISNULL(CIB_Clearing,0) AS CIB_Clearing  FROM tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            TAX_DOV = SQL.SQLDR("TAX_DOV").ToString
            TAX_EWT = SQL.SQLDR("TAX_EWT").ToString
            TAX_VatPayable = SQL.SQLDR("TAX_VatPayable").ToString
            TAX_CWT = SQL.SQLDR("TAX_CWT").ToString
            TAX_OV = SQL.SQLDR("TAX_OV").ToString
            isClearingEnabled = SQL.SQLDR("CIB_Clearing").ToString
        End If
    End Sub

    Private Function LoadBankID(ByVal Bank As String) As String
        Dim query As String
        query = " SELECT  CAST(BankID AS nvarchar) + '-' + Bank + ' ' + Branch + " & _
                "         CASE WHEN AccountNo <> '' THEN ' (' + AccountNo  +  ')' ELSE '' END AS Bank " & _
                " FROM    tblBank_Master " & _
                " WHERE   Status ='Active' AND BankID = @BankID "
        SQL.FlushParams()
        SQL.AddParam("@BankID", Bank)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("Bank")
        Else
            query = " SELECT CASH_ACCOUNT + '-' + AccountTitle AS Bank FROM tblSystemSetup " & _
                    " INNER JOIN tblCOA_Master ON" & _
                    " tblCOA_Master.AccountCode = tblSystemSetup.CASH_ACCOUNT " & _
                    " WHERE  CASH_ACCOUNT = @BankID "
            SQL.FlushParams()
            SQL.AddParam("@BankID", Bank)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Return SQL.SQLDR("Bank")
            Else
                cbBankTo.SelectedIndex = -1
                Return ""
            End If
            
        End If
    End Function

    Private Sub LoadBankList()
        Dim query As String
        query = " SELECT  CAST(BankID AS nvarchar) + '-' + Bank + ' ' + Branch + " & _
                "         CASE WHEN AccountNo <> '' THEN ' (' + AccountNo  +  ')' ELSE '' END AS Bank " & _
                " FROM    tblBank_Master " & _
                " WHERE   Status ='Active'" & _
                " UNION ALL " & _
                "  SELECT CASH_ACCOUNT + '-' + AccountTitle FROM tblSystemSetup " & _
                "  INNER JOIN tblCOA_Master ON " & _
                "   tblCOA_Master.AccountCode = tblSystemSetup.CASH_ACCOUNT"
        SQL.ReadQuery(query)
        cbBank.Items.Clear()
        While SQL.SQLDR.Read
            cbBankTo.Items.Add(SQL.SQLDR("Bank").ToString)
        End While
    End Sub

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

    Private Sub EnableControl(ByVal Value As Boolean)
        txtVCECode.Enabled = Value
        txtVCEName.Enabled = Value
        btnSearchVCE.Enabled = Value
        dgvEntry.AllowUserToAddRows = Value
        dgvEntry.AllowUserToDeleteRows = Value
        gbBank.Enabled = Value
        dtpDate.Enabled = Value
        cbCurrency.Enabled = Value
        txtConversion.Enabled = Value
        cbCostCenter.Enabled = Value

        If Value = True Then
            dgvEntry.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvEntry.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        txtRemarks.Enabled = Value
        txtAmount.Enabled = Value
        txtAPVRef.Enabled = Value
        txtBSRef.Enabled = Value
        cbPaymentType.Enabled = Value
        cbCollector.Enabled = Value
        cbCollectionCompany.Enabled = Value
        cbCollectionType.Enabled = Value
        cbBankTo.Enabled = Value
        btnTypeMaintenance.Enabled = Value
        txtRemarks.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Private Sub LoadCollection(ByVal ID As String)
        Dim query, PaymentType, CollectionComapany, Currency, CC As String
        query = " SELECT  TransID, TransNo, DateTrans, PaymentType, tblCollection.VCECode, VCEName, Amount, CheckRef, BankRef, CheckDate, Remarks, tblCollection.Status, " &
                " ISNULL(Collection_Company, 'KASAMA MPC') as Collection_Company, ISNULL(bankID ,0) as bankID, BSRef, SIRef, Currency, ISNULL(Exchange_Rate,0) AS Exchange_Rate, CostCenter " &
                " FROM    tblCollection LEFT JOIN viewVCE_Master " &
                " ON      tblCollection.VCECode = viewVCE_Master.VCECode " &
                " WHERE   TransID = '" & ID & "' AND TransType = '" & TransType & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            TransNo = SQL.SQLDR("TransNo").ToString
            txtTransNum.Text = TransNo
            PaymentType = SQL.SQLDR("PaymentType").ToString
            CollectionComapany = SQL.SQLDR("Collection_Company").ToString
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
            dtpDate.Value = SQL.SQLDR("DateTrans")
            CC = SQL.SQLDR("CostCenter").ToString
            txtAmount.Text = CDec(SQL.SQLDR("Amount")).ToString("N2")
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            Currency = SQL.SQLDR("Currency").ToString
            txtConversion.Text = CDec(SQL.SQLDR("Exchange_Rate")).ToString("N4")
            txtBankRef.Text = SQL.SQLDR("CheckRef").ToString
            txtAPVRef.Text = SQL.SQLDR("SIRef").ToString
            txtBSRef.Text = SQL.SQLDR("BSRef").ToString
            cbBank.Text = SQL.SQLDR("BankRef").ToString
            If IsDate(SQL.SQLDR("CheckDate")) Then
                dtpBankRefDate.Value = SQL.SQLDR("CheckDate")
            End If
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            bankID = SQL.SQLDR("bankID").ToString
            disableEvent = True
            cbBankTo.SelectedItem = LoadBankID(bankID)
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
            cbPaymentType.SelectedItem = PaymentType
            cbCollectionCompany.SelectedItem = CollectionComapany
            cbCostCenter.SelectedItem = CC
            disableEvent = False
            LoadEntry(TransID)

            tsbCancel.Text = "Cancel"
            ' TOOLSTRIP BUTTONS
            If txtStatus.Text = "Cancelled" Then
                tsbCancel.Text = "Un-Can"
                tsbEdit.Enabled = False
                tsbCancel.Enabled = True
                tsbDelete.Enabled = True
            Else
                tsbEdit.Enabled = True
                tsbCancel.Enabled = True
                tsbDelete.Enabled = True
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
            If dtpDate.Value <= GetMaxPEC() Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
                tsbDelete.Enabled = False
            End If
            EnableControl(False)
        Else
            ClearText()
        End If
    End Sub

    Private Sub LoadEntry(ByVal CollectionID As Integer)
        Dim query As String


        dateCleared = ""
        transactionCleared = False
        accountCleared = ""

        query = " SELECT ID, JE_No, tblJE_Details.AccntCode, AccountTitle, tblJE_Details.VCECode, VCEName, Debit, Credit, Particulars, RefNo, dateCleared , VATType, CostCenter" &
                " FROM  tblJE_Details LEFT JOIN viewVCE_Master " &
                " ON     tblJE_Details.VCECode = viewVCE_Master.VCECode " &
                " INNER JOIN tblCOA_Master " &
                " ON     tblJE_Details.AccntCode = tblCOA_Master.AccountCode " &
                " WHERE JE_No = (SELECT  JE_No FROM tblJE_Header WHERE RefType = '" & TransType & "' AND RefTransID = " & CollectionID & " AND isUpload<> 1) " &
                " ORDER BY Debit DESC, Credit ASC "
        SQL.ReadQuery(query)
        dgvEntry.Rows.Clear()
        Dim rowsCount As Integer = 0
        If SQL.SQLDR.HasRows Then
            While SQL.SQLDR.Read
                JETransiD = SQL.SQLDR("JE_No").ToString
                dgvEntry.Rows.Add(SQL.SQLDR("AccntCode").ToString)
                dgvEntry.Rows(rowsCount).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                dgvEntry.Rows(rowsCount).Cells(chDebit.Index).Value = IIf(SQL.SQLDR("Debit") = 0, "", CDec(SQL.SQLDR("Debit")).ToString("N2"))
                dgvEntry.Rows(rowsCount).Cells(chCredit.Index).Value = IIf(SQL.SQLDR("Credit") = 0, "", CDec(SQL.SQLDR("Credit")).ToString("N2"))
                dgvEntry.Rows(rowsCount).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                dgvEntry.Rows(rowsCount).Cells(chVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                dgvEntry.Rows(rowsCount).Cells(chParticulars.Index).Value = SQL.SQLDR("Particulars").ToString
                dgvEntry.Rows(rowsCount).Cells(chRef.Index).Value = SQL.SQLDR("RefNo").ToString
                dgvEntry.Rows(rowsCount).Cells(chVATType.Index).Value = SQL.SQLDR("VATType").ToString
                dgvEntry.Rows(rowsCount).Cells(chProfit_Code.Index).Value = SQL.SQLDR("CostCenter").ToString
                dgvEntry.Rows(rowsCount).Cells(chProfit_Center.Index).Value = GetCCName(SQL.SQLDR("CostCenter").ToString)

                If SQL.SQLDR("dateCleared").ToString <> "" Then
                    dateCleared = SQL.SQLDR("dateCleared").ToString
                    transactionCleared = True
                    accountCleared = SQL.SQLDR("AccntCode").ToString
                End If


                rowsCount += 1
            End While
            TotalDBCR()
        Else
            JETransiD = 0
            dgvEntry.Rows.Clear()
        End If
    End Sub

    Private Sub LoadPaymentType()
        Dim query As String
        query = " SELECT DISTINCT PaymentType FROM tblCollection_PaymentType WHERE Status ='Active' "
        SQL.ReadQuery(query)
        cbPaymentType.Items.Clear()
        While SQL.SQLDR.Read
            cbPaymentType.Items.Add(SQL.SQLDR("PaymentType").ToString)
        End While
        If cbPaymentType.Items.Count > 0 Then
            cbPaymentType.SelectedIndex = 0
        End If
    End Sub

    Private Sub cbPaymentType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbPaymentType.SelectedIndexChanged
        'Dim query As String
        'query = " SELECT WithBank, Account_Code " & _
        '        " FROM   tblCollection_PaymentType " & _
        '        " WHERE  Status ='Active' AND PaymentType ='" & cbPaymentType.SelectedItem & "' "
        'SQL.ReadQuery(query)
        'cbCollectionType.Items.Clear()
        'If SQL.SQLDR.Read Then
        '    gbBank.Visible = SQL.SQLDR("WithBank")
        '    DBAccount = SQL.SQLDR("Account_Code").ToString
        'End If


        'If CheckBox1.Checked = False Then
        '    SelectPaymentType()
        'End If

        If disableEvent = False Then
            If CheckBox1.Checked Then
                SelectBank()
            Else
                SelectPaymentType()
                bankID = 0
            End If
            If cbPaymentType.SelectedItem = "Cash" Then
                gbBank.Visible = False
            Else
                gbBank.Visible = True
            End If
        End If

        'If disableEvent = False Then
        '    tcPayment.Visible = True
        '    HideTabPageAll()
        '    ShowTabPage(cbPaymentType.SelectedItem)
        '    If cbPaymentType.SelectedItem = "Check" Then
        '        If cbBank.SelectedIndex <> -1 Then bankID = GetBankID(cbBank.SelectedItem)
        '        tcPayment.SelectedTab = tpCheck
        '    ElseIf cbPaymentType.SelectedItem = "Multiple Check" Then
        '        tcPayment.SelectedTab = tpMultipleCheck
        '    End If
        'End If


    End Sub

    Private Sub SelectPaymentType()
        'Dim query As String
        'query = " SELECT WithBank, Account_Code " &
        '        " FROM   tblCollection_PaymentType " &
        '        " WHERE  Status ='Active' AND PaymentType ='" & cbPaymentType.Text & "' "
        'SQL.ReadQuery(query)
        'cbCollectionType.Items.Clear()
        'If SQL.SQLDR.Read Then
        '    gbBank.Visible = SQL.SQLDR("WithBank")
        '    DBAccount = SQL.SQLDR("Account_Code").ToString

        '    dgvEntry.Item(chAccntCode.Index, 0).Value = DBAccount
        '    dgvEntry.Item(chAccntTitle.Index, 0).Value = GetAccntTitle(DBAccount)
        'End If

        Dim query As String
        query = " SELECT WithBank, Account_Code " &
                " FROM   tblCollection_PaymentType " &
                " WHERE  Status ='Active' AND PaymentType ='" & cbPaymentType.SelectedItem & "' "
        SQL.ReadQuery(query)
        cbCollectionType.Items.Clear()
        If SQL.SQLDR.Read Then
            gbBank.Visible = SQL.SQLDR("WithBank")
            DBAccount = SQL.SQLDR("Account_Code").ToString

            If cbPaymentType.SelectedItem <> "Cash" Then
                If dgvEntry.Rows.Count > 0 Then
                    dgvEntry.Item(chAccntCode.Index, 0).Value = DBAccount
                    dgvEntry.Item(chAccntTitle.Index, 0).Value = GetAccntTitle(DBAccount)
                End If
            End If
        End If
    End Sub

    Private Sub LoadCollectionType()
        Dim query As String
        query = " SELECT DISTINCT Collection_Type FROM tblCollection_Type WHERE Status ='Active' "
        SQL.ReadQuery(query)
        cbCollectionType.Items.Clear()
        While SQL.SQLDR.Read
            cbCollectionType.Items.Add(SQL.SQLDR("Collection_Type").ToString)
        End While
    End Sub

    Private Sub LoadCollectionCompany()
        Dim query As String
        query = " SELECT DISTINCT Collection_Company FROM tblCollection_Company "
        SQL.ReadQuery(query)
        cbCollectionCompany.Items.Clear()
        While SQL.SQLDR.Read
            cbCollectionCompany.Items.Add(SQL.SQLDR("Collection_Company").ToString)
        End While
        cbCollectionCompany.SelectedIndex = 0
    End Sub

    Private Sub btnTypeMaintenance_Click(sender As System.Object, e As System.EventArgs) Handles btnTypeMaintenance.Click
        frmCollection_Type.ShowDialog()
        LoadCollectionType()
    End Sub

    Public Function getduplicateOR(orno As String) As Boolean
        Dim query As String
        query = "SELECT * FROM Collection WHERE (ORNO = N'" & orno & "')"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub cbCollectionType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbCollectionType.SelectedIndexChanged
        Try
            If disableEvent = False Then
                Dim query As String
                Dim amount As Decimal
                query = " SELECT  Account_Code, AccountTitle, Amount, Collection_Type  " & _
                        " FROM    tblCollection_Type INNER JOIN tblCOA_Master " & _
                        " ON      tblCollection_Type.Account_Code = tblCOA_Master.AccountCode " & _
                        " WHERE   tblCollection_Type.Status ='Active' AND Collection_Type = @Collection_Type "
                SQL.FlushParams()
                SQL.AddParam("@Collection_Type", cbCollectionType.SelectedItem)
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
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(amount).ToString("N2")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = ""
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = ""
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = cbCollectionType.SelectedItem
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = IIf(cbRef.Text = "", txtRef.Text, cbRef.Text & ":" & txtRef.Text)


                    TotalDBCR()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SQL.FlushParams()
        End Try

    End Sub
    Dim eColIndex As Integer = 0
    Private Sub dgvEntry_CellBeginEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvEntry.CellBeginEdit
        eColIndex = e.ColumnIndex
    End Sub

    Private Sub dgvProducts_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntry.CellEndEdit
        Try
            If e.ColumnIndex = chDebit.Index Or e.ColumnIndex = chCredit.Index Then
                If IsNumeric(dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value) Then
                    dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value = CDec(dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value).ToString("N2")
                End If
                TotalDBCR()
            ElseIf e.ColumnIndex = chAccntCode.Index Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntCode", dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value.ToString)
                dgvEntry.Item(chAccntCode.Index, e.RowIndex).Value = f.accountcode
                dgvEntry.Item(chAccntTitle.Index, e.RowIndex).Value = f.accttile
                f.Dispose()
                dgvEntry.Item(chDebit.Index, e.RowIndex).Selected = True
                TotalDBCR()
            ElseIf e.ColumnIndex = chAccntTitle.Index Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value.ToString)
                dgvEntry.Item(chAccntCode.Index, e.RowIndex).Value = f.accountcode
                dgvEntry.Item(chAccntTitle.Index, e.RowIndex).Value = f.accttile
                f.Dispose()


                ''Auto Entry Ref No
                'Dim strVCECode As String = ""
                'Dim strAccntCode As String = ""
                'strVCECode = txtVCECode.Text
                'strAccntCode = dgvEntry.Item(chAccntCode.Index, e.RowIndex).Value
                'If dgvEntry.Item(chVCECode.Index, e.RowIndex).Value <> "" Then
                '    strVCECode = dgvEntry.Item(chVCECode.Index, e.RowIndex).Value
                'End If
                'dgvEntry.Item(chRef.Index, e.RowIndex).Value = GetRefNo(strVCECode, strAccntCode)
                'Dim strRefNo As String = ""
                'Dim strRefNoLoan As String = ""
                'strRefNo = dgvEntry.Item(chRef.Index, e.RowIndex).Value
                'strRefNoLoan = GetRefNoLoan(strRefNo)
                'If strRefNoLoan <> "" Then
                '    dgvEntry.Rows.Add("")
                '    dgvEntry.Item(chVCECode.Index, e.RowIndex + 1).Value = GetRefNoVCECode(strRefNo)
                '    dgvEntry.Item(chVCEName.Index, e.RowIndex + 1).Value = GetVCEName(dgvEntry.Item(chVCECode.Index, e.RowIndex + 1).Value)
                '    dgvEntry.Item(chAccntCode.Index, e.RowIndex + 1).Value = strRefNoLoan
                '    dgvEntry.Item(chAccntTitle.Index, e.RowIndex + 1).Value = GetAccntTitle(dgvEntry.Item(chAccntCode.Index, e.RowIndex + 1).Value)
                '    dgvEntry.Item(chRef.Index, e.RowIndex + 1).Value = strRefNo
                'End If
            ElseIf e.ColumnIndex = chVCECode.Index Or e.ColumnIndex = chVCEName.Index Then
                Dim f As New frmVCE_Search

                f.cbFilter.SelectedItem = "VCEName"
                f.txtFilter.Text = dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                f.ShowDialog()
                dgvEntry.Item(chVCECode.Index, e.RowIndex).Value = f.VCECode
                dgvEntry.Item(chVCEName.Index, e.RowIndex).Value = f.VCEName
                f.Dispose()

            ElseIf e.ColumnIndex = chProfit_Center.Index Then
                If dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value <> Nothing Then
                    Dim f1 As New frmCC_PC_Search
                    f1.Text = "Search of Cost Center"
                    f1.Type = "Cost Center"
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
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Start of Cost Center insert to Table
    Dim strDefaultGridView As String = ""
    Dim strDefaultGridCode As String = ""
    Public Function LoadProfitCenterGridView()

        Dim selectSQL As String = " SELECT Code, Description FROM tblPC WHERE Status <> 'Inactive'"
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
        query = " SELECT Description FROM tblCC WHERE Code ='" & PCCode & "'  AND Status <> 'Inactive' "
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("Description").ToString
        Else
            Return ""
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

    Private Sub txtVCEName_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmVCE_Search
                f.cbFilter.SelectedItem = "VCEName"
                f.txtFilter.Text = txtVCEName.Text
                f.ShowDialog()
                txtVCECode.Text = f.VCECode
                txtVCEName.Text = f.VCEName
                LoadCurrency()
                f.Dispose()
                If TransType = "OR" Then
                    If txtVCECode.Text <> "" Then
                        f.Dispose()
                        Dim r As New frmReceivables
                        r.VCECode = txtVCECode.Text
                        r.CollectDate = dtpDate.Value.Date
                        r.Show()
                        'TotalCashAmount()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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

    Private Sub txtBankRefAmount_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtAmount.KeyDown
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

    Private Function IfExist(ByVal ID As String, Type As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblCollection WHERE TransNo ='" & ID & "' AND TransType ='" & Type & "' AND BranchCode = '" & BranchCode & "' AND Status <> 'Cancelled' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub SaveCollection()
        Dim SQL1 As New SQLControl
        SQL1.BeginTransaction()
        Try
            Dim insertSQL As String
            activityStatus = True
            insertSQL = " INSERT INTO " &
                        " tblCollection (TransID, TransType, TransNo, DateTrans, VCECode, PaymentType, Amount, " &
                        "               Currency, Exchange_Rate,  CheckRef, BankRef, CheckDate, Remarks, TransAuto, " &
                        "               WhoCreated, BranchCode, BusinessCode, Collection_Company, Collector_Name, bankID, SIRef, BSRef, Status, CostCenter) " &
                        " VALUES(@TransID, @TransType, @TransNo, @DateTrans, @VCECode, @PaymentType, @Amount, " &
                        "               @Currency, @Exchange_Rate, @CheckRef, @BankRef, @CheckDate, @Remarks, @TransAuto, " &
                        "               @WhoCreated, @BranchCode, @BusinessCode, @Collection_Company, @Collector_Name, @bankID, @SIRef, @BSRef, @Status, @CostCenter)"
            SQL1.FlushParams()
            SQL1.AddParam("@TransID", TransID)
            SQL1.AddParam("@TransType", TransType)
            SQL1.AddParam("@TransNo", TransNo)
            SQL1.AddParam("@DateTrans", dtpDate.Value.Date)
            SQL1.AddParam("@PaymentType", cbPaymentType.SelectedItem)
            SQL1.AddParam("@Collection_Company", cbCollectionCompany.SelectedItem)
            SQL1.AddParam("@VCECode", txtVCECode.Text)
            If IsNumeric(txtAmount.Text) Then
                SQL1.AddParam("@Amount", CDec(txtAmount.Text))
            Else
                SQL1.AddParam("@Amount", 0)
            End If
            SQL1.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL1.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL1.AddParam("@CheckRef", txtBankRef.Text)
            SQL1.AddParam("@BankRef", cbBank.Text)
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@TransAuto", TransAuto)
            SQL1.AddParam("@CheckDate", IIf(gbBank.Visible = True, dtpBankRefDate.Value.Date, DBNull.Value))
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@Collector_Name", cbCollector.Text)
            SQL1.AddParam("@SIRef", txtAPVRef.Text)
            SQL1.AddParam("@BSRef", txtBSRef.Text)
            SQL1.AddParam("@bankID", bankID)
            SQL1.AddParam("@CostCenter", cbCostCenter.SelectedItem)
            If ForApproval = True Then SQL1.AddParam("@Status", "Draft") Else SQL1.AddParam("@Status", "Saved")
            SQL1.ExecNonQuery(insertSQL)

            JETransiD = GenerateTransID("JE_No", "tblJE_Header")

            insertSQL = " INSERT INTO " &
                        " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, " &
                        "               TotalDBCR, Currency, Exchange_Rate, Remarks, WhoCreated, Status, CostCenter) " &
                        " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, " &
                        "               @TotalDBCR, @Currency, @Exchange_Rate, @Remarks, @WhoCreated, @Status, @CostCenter)"
            SQL1.FlushParams()
            SQL1.AddParam("@JE_No", JETransiD)
            SQL1.AddParam("@AppDate", dtpDate.Value.Date)
            SQL1.AddParam("@RefType", TransType)
            SQL1.AddParam("@RefTransID", TransID)
            SQL1.AddParam("@Book", Book)
            SQL1.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
            SQL1.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL1.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.AddParam("@CostCenter", cbCostCenter.SelectedItem)
            If ForApproval = True Then SQL1.AddParam("@Status", "Draft") Else SQL1.AddParam("@Status", "Saved")
            SQL1.ExecNonQuery(insertSQL)

            'JETransiD = LoadJE(TransType, TransID)
            Dim HeaderCC As String = cbCostCenter.SelectedItem

            Dim line As Integer = 1
            For Each item As DataGridViewRow In dgvEntry.Rows
                If item.Cells(chAccntCode.Index).Value <> Nothing Then
                    insertSQL = " INSERT INTO " &
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, BranchCode, CostCenter) " &
                                " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @BranchCode, @CostCenter)"
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
                    If item.Cells(chRef.Index).Value <> Nothing AndAlso item.Cells(chRef.Index).Value <> "" Then
                        SQL1.AddParam("@RefNo", item.Cells(chRef.Index).Value.ToString)
                    Else
                        SQL1.AddParam("@RefNo", "")
                    End If
                    If cbCostCenter.SelectedItem = "" Then
                        If item.Cells(chProfit_Code.Index).Value <> Nothing AndAlso item.Cells(chProfit_Code.Index).Value <> "" Then
                            SQL1.AddParam("@CostCenter", item.Cells(chProfit_Code.Index).Value.ToString)
                        Else
                            SQL1.AddParam("@CostCenter", "")
                        End If
                    Else
                        SQL1.AddParam("@CostCenter", HeaderCC)
                    End If
                    SQL1.AddParam("@LineNumber", line)
                        SQL1.AddParam("@BranchCode", BranchCode)
                        SQL1.ExecNonQuery(insertSQL)
                        line += 1
                    End If
            Next
            'If CA_ID > 0 Then
            '    Dim updateSQL As String
            '    updateSQL = " UPDATE tblCA SET Status ='Closed' WHERE TransID = '" & CA_ID & "'"
            '    SQL1.ExecNonQuery(updateSQL)
            'End If
            SQL1.Commit()
        Catch ex As Exception
            SQL1.Rollback()
            TransID = ""
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, TransType, Me.Name.ToString, "INSERT", "TransNo", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL1.FlushParams()
        End Try
    End Sub

    Private Sub UpdateCollection()
        Dim SQL As New SQLControl
        SQL.BeginTransaction()
        Try
            Dim insertSQL, UpdateSQL, deleteSQL As String
            activityStatus = True
            UpdateSQL = " UPDATE tblCollection  " &
                        " SET    TransType = @TransType, TransNo = @TransNo, DateTrans = @DateTrans, PaymentType = @PaymentType, " &
                        "        VCECode = @VCECode, Amount = @Amount, CheckRef = @CheckRef, BankRef = @BankRef, CheckDate = @CheckDate, Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE(), " &
                        "       Collection_Company = @Collection_Company, bankID = @bankID , SIRef = @SIRef, BSRef = @BSRef, Currency = @Currency, Exchange_Rate = @Exchange_Rate, CostCenter = @CostCenter" &
                        " WHERE TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@TransType", TransType)
            SQL.AddParam("@TransNo", TransNo)
            SQL.AddParam("@DateTrans", dtpDate.Value.Date)
            SQL.AddParam("@PaymentType", cbPaymentType.SelectedItem)
            SQL.AddParam("@Collection_Company", cbCollectionCompany.SelectedItem)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            If IsNumeric(txtAmount.Text) Then
                SQL.AddParam("@Amount", CDec(txtAmount.Text))
            Else
                SQL.AddParam("@Amount", 0)
            End If
            SQL.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL.AddParam("@CheckRef", txtBankRef.Text)
            SQL.AddParam("@BankRef", cbBank.Text)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@CheckDate", IIf(gbBank.Visible = True, dtpBankRefDate.Value.Date, DBNull.Value))
            SQL.AddParam("@WhoModified", UserID)
            SQL.AddParam("@SIRef", txtAPVRef.Text)
            SQL.AddParam("@BSRef", txtBSRef.Text)
            SQL.AddParam("@CostCenter", cbCostCenter.SelectedItem)
            SQL.AddParam("@bankID", bankID)
            SQL.ExecNonQuery(UpdateSQL)

            JETransiD = LoadJE(TransType, TransID)
            If JETransiD = 0 Then

                JETransiD = GenerateTransID("JE_No", "tblJE_Header")

                insertSQL = " INSERT INTO " &
                       " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Currency, Exchange_Rate,  Remarks, WhoCreated, CostCenter) " &
                       " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Currency, @Exchange_Rate, @Remarks, @WhoCreated, @CostCenter)"
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransiD)
                SQL.AddParam("@AppDate", dtpDate.Value.Date)
                SQL.AddParam("@RefType", TransType)
                SQL.AddParam("@RefTransID", TransID)
                SQL.AddParam("@Book", Book)
                SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@WhoCreated", "")
                SQL.AddParam("@CostCenter", cbCostCenter.SelectedItem)
                SQL.ExecNonQuery(insertSQL)

                'JETransiD = LoadJE(TransType, TransID)
            Else
                UpdateSQL = " UPDATE tblJE_Header " &
                           " SET    AppDate = @AppDate, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " &
                           "        RefType = @RefType, RefTransID = @RefTransID, Book = @Book, TotalDBCR = @TotalDBCR, Currency = @Currency, Exchange_Rate = @Exchange_Rate, " &
                           "        Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE(), CostCenter = @CostCenter " &
                           " WHERE  JE_No = @JE_No "
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransiD)
                SQL.AddParam("@AppDate", dtpDate.Value.Date)
                SQL.AddParam("@RefType", TransType)
                SQL.AddParam("@RefTransID", TransID)
                SQL.AddParam("@Book", Book)
                SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@CostCenter", cbCostCenter.SelectedItem)
                SQL.AddParam("@WhoModified", UserID)
                SQL.ExecNonQuery(UpdateSQL)
            End If


            ' DELETE ACCOUNTING ENTRIES
            deleteSQL = " DELETE FROM tblJE_Details  WHERE  JE_No = @JE_No "
            SQL.FlushParams()
            SQL.AddParam("@JE_No", JETransiD)
            SQL.ExecNonQuery(deleteSQL)

            Dim HeaderCC As String = cbCostCenter.SelectedItem

            Dim line As Integer = 1
            For Each item As DataGridViewRow In dgvEntry.Rows
                If item.Cells(chAccntCode.Index).Value <> Nothing Then
                    insertSQL = " INSERT INTO " &
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, BranchCode, CostCenter) " &
                                " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @BranchCode, @CostCenter)"
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
                    If item.Cells(chRef.Index).Value <> Nothing AndAlso item.Cells(chRef.Index).Value <> "" Then
                        SQL.AddParam("@RefNo", item.Cells(chRef.Index).Value.ToString)
                    Else
                        SQL.AddParam("@RefNo", "")
                    End If
                    If cbCostCenter.SelectedItem = "" Then
                        If item.Cells(chProfit_Code.Index).Value <> Nothing AndAlso item.Cells(chProfit_Code.Index).Value <> "" Then
                            SQL.AddParam("@CostCenter", item.Cells(chProfit_Code.Index).Value.ToString)
                        Else
                            SQL.AddParam("@CostCenter", "")
                        End If
                    Else
                        SQL.AddParam("@CostCenter", HeaderCC)
                    End If
                    SQL.AddParam("@LineNumber", line)
                    SQL.AddParam("@BranchCode", BranchCode)
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
            RecordActivity(UserID, TransType, Me.Name.ToString, "UPDATE", "TransNo", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub ClearText()
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        txtAmount.Text = "0.00"
        txtRemarks.Text = ""
        cbBank.Items.Clear()
        txtBankRef.Text = ""
        txtStatus.Text = ""
        cbCollector.Text = ""
        dgvEntry.Rows.Clear()
        tcCollection.SelectedTab = tpCollection
        cbCollectionCompany.SelectedIndex = 0
        cbCostCenter.SelectedIndex = 0
        cbBankTo.SelectedIndex = -1
        txtTotalDebit.Text = "0.00"
        txtTotalCredit.Text = "0.00"
        txtAPVRef.Clear()
        txtBSRef.Clear()
        cbCurrency.Items.Clear()
        txtConversion.Text = ""
        dtpDate.MinDate = GetMaxPEC().AddDays(1)
        dtpDate.Value = Date.Today.Date
    End Sub

    Private Sub dgvManual_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntry.CellContentClick
        Try
            If e.ColumnIndex = chCompute.Index Then
                Dim f As New frmTaxComputation
                Dim VATamount As Decimal
                Dim VCECode As String
                If dgvEntry.Item(chVCECode.Index, e.RowIndex).Value = "" Then
                    VCECode = txtVCECode.Text
                Else
                    VCECode = dgvEntry.Item(chVCECode.Index, e.RowIndex).Value
                End If

                VATamount = CDec(IIf(IIf(IsNothing(dgvEntry.Item(chDebit.Index, e.RowIndex).Value), "0.00", dgvEntry.Item(chDebit.Index, e.RowIndex).Value).ToString = "0.00", IIf(IsNothing(dgvEntry.Item(chCredit.Index, e.RowIndex).Value), "0.00", dgvEntry.Item(chCredit.Index, e.RowIndex).Value).ToString, IIf(IsNothing(dgvEntry.Item(chDebit.Index, e.RowIndex).Value), "0.00", dgvEntry.Item(chDebit.Index, e.RowIndex).Value).ToString)).ToString("N2")
                f.ShowDialog(VATamount, "", "", VCECode)
                If IsNothing(dgvEntry.Item(chDebit.Index, e.RowIndex).Value) Then
                    dgvEntry.Item(chCredit.Index, e.RowIndex).Value = CDec(f.GrossAmount).ToString("N2")
                    dgvEntry.Item(chVCECode.Index, e.RowIndex).Value = f.VCECode
                    dgvEntry.Item(chVCEName.Index, e.RowIndex).Value = GetVCEName(f.VCECode)
                    txtAmount.Text = CDec(f.GrossAmount).ToString("N2")
                Else
                    dgvEntry.Item(chDebit.Index, e.RowIndex).Value = CDec(f.GrossAmount).ToString("N2")
                    dgvEntry.Item(chVCECode.Index, e.RowIndex).Value = f.VCECode
                    dgvEntry.Item(chVCEName.Index, e.RowIndex).Value = GetVCEName(f.VCECode)
                    txtAmount.Text = CDec(f.GrossAmount).ToString("N2")
                End If
                If f.VATAmount <> 0 Then
                    dgvEntry.Rows.Add("")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = f.AR_OutputVAT
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(f.AR_OutputVAT)
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(f.VATAmount).ToString("N2")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = "Output VAT"
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = f.VCECode
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(f.VCECode)

                   
                End If
                If f.EWTAmount <> 0 Then
                    dgvEntry.Rows.Add("")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = f.TAX_CWT
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(f.TAX_CWT)
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(f.EWTAmount).ToString("N2")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = "CWT"
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = f.VCECode
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(f.VCECode)

                End If

                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(VATamount - f.EWTAmount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = f.VATType
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = f.VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(f.VCECode)

                TotalDBCR()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub dgvEntry_RowsRemoved(sender As System.Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgvEntry.RowsRemoved
        Try
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess(TransType & "_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog(TransType)
            TransID = f.transID
            LoadCollection(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess(TransType & "_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            TransNo = ""

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
            txtStatus.Text = "Open"
            dgvEntry.Rows.Add()
            EnableControl(True)
            cbBankTo.SelectedIndex = 0
            dgvEntry.Rows(0).ReadOnly = True
            txtTransNum.Text = GenerateTransNum(TransAuto, TransType, ColumnPK, DBTable)
        End If
    End Sub


    Public Function GenerateTransNum(ByRef Auto As Boolean, ModuleID As String, ColumnPK As String, Table As String) As String
        Dim TransNum As String = ""
        If Auto = True Then
            ' GENERATE TRANS ID 
            Dim query As String
            query = " SELECT	GlobalSeries, ISNULL(BranchCode,'All') AS BranchCode, ISNULL(BusinessCode,'All') AS BusinessCode,  " & _
                                " 		    ISNULL(TransGroup,0) AS TransGroup, ISNULL(Prefix,'') AS Prefix, ISNULL(Digits,6) AS Digits, " & _
                                "           ISNULL(StartRecord,1) AS StartRecord, LEN(ISNULL(Prefix,'')) + ISNULL(Digits,6) AS ID_Length " & _
                                " FROM	    tblTransactionSetup LEFT JOIN tblTransactionDetails " & _
                                " ON		tblTransactionSetup.TransType = tblTransactionDetails.TransType " & _
                                " WHERE	    tblTransactionSetup.TransType ='" & ModuleID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                If SQL.SQLDR("GlobalSeries") = True Then
                    If SQL.SQLDR("BranchCode") = "All" AndAlso SQL.SQLDR("BusinessCode") = "All" Then
                        Dim digits As Integer = SQL.SQLDR("Digits")
                        Dim prefix As String = SQL.SQLDR("Prefix")
                        Dim startrecord As Integer = SQL.SQLDR("StartRecord")
                        query = " SELECT    ISNULL(MAX(SUBSTRING(" & ColumnPK & "," & prefix.Length + 1 & "," & digits & "))+ 1," & startrecord & ") AS TransID  " & _
                                " FROM      " & Table & "  " & _
                                " WHERE     " & ColumnPK & " LIKE '" & prefix & "%' AND LEN(" & ColumnPK & ") = '" & digits & "'  AND TransAuto = 1 AND TransType  ='" & ModuleID & "' "
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
                Else

                    query = " SELECT	GlobalSeries, ISNULL(BranchCode,'All') AS BranchCode, ISNULL(BusinessCode,'All') AS BusinessCode,  " & _
                                " 		    ISNULL(TransGroup,0) AS TransGroup, ISNULL(Prefix,'') AS Prefix, ISNULL(Digits,6) AS Digits, " & _
                                "           ISNULL(StartRecord,1) AS StartRecord, LEN(ISNULL(Prefix,'')) + ISNULL(Digits,6) AS ID_Length " & _
                                " FROM	    tblTransactionSetup LEFT JOIN tblTransactionDetails " & _
                                " ON		tblTransactionSetup.TransType = tblTransactionDetails.TransType " & _
                                " WHERE	    tblTransactionSetup.TransType ='" & ModuleID & "' AND BranchCode =  '" & BranchCode & "' "
                    SQL.ReadQuery(query)
                    If SQL.SQLDR.Read Then
                        Dim digits As Integer = SQL.SQLDR("Digits")
                        Dim prefix As String = SQL.SQLDR("Prefix")
                        Dim startrecord As Integer = SQL.SQLDR("StartRecord")
                        query = " SELECT    ISNULL(MAX(SUBSTRING(" & ColumnPK & "," & prefix.Length + 1 & "," & digits & "))+ 1,1) AS TransID  " & _
                                " FROM      " & Table & "  " & _
                                " WHERE     " & ColumnPK & " LIKE '" & prefix & "%'   AND TransAuto = 1 AND BranchCode = '" & BranchCode & "'"
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
                                    " WHERE     " & ColumnPK & " = '" & TransNum & "' AND BranchCode = '" & BranchCode & "'"
                            SQL.ReadQuery(query)
                            If SQL.SQLDR.Read Then
                                Dim updateSQL As String
                                updateSQL = " UPDATE  " & Table & "  SET TransAuto = 1 WHERE " & ColumnPK & " = '" & TransNum & "' "
                                SQL.ExecNonQuery(updateSQL)
                                TransNum = "-1"
                            End If
                        End If
                    End If
                End If
            End If
            If TransNum <> "-1" Then

            End If

            Return TransNum
        Else
            Return ""
        End If
    End Function

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess(TransType & "_EDIT") Then
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
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        Try
            If txtVCECode.Text = "" Then
                Msg("Please enter VCECode!", MsgBoxStyle.Exclamation)
            ElseIf cbBankTo.SelectedIndex = -1 Then
                MsgBox("Please select bank deposit!", MsgBoxStyle.Exclamation)
            ElseIf txtTotalDebit.Text <> txtTotalCredit.Text Then
                MsgBox("Please check the Debit and Credit Amount!", MsgBoxStyle.Exclamation)
            ElseIf txtAmount.Text = "" Then
                MsgBox("Please check amount!", MsgBoxStyle.Exclamation)
            ElseIf txtConversion.Visible = True And txtConversion.Text = "" Then
                MsgBox("Please check exchange rate!", MsgBoxStyle.Exclamation)
            ElseIf dgvEntry.Rows.Count = 1 Then
                MsgBox("No entries, Please check!", MsgBoxStyle.Exclamation)
            ElseIf gbBank.Visible = True And cbBank.Text = "" Then
                MsgBox("Please enter bank of the received cheque", MsgBoxStyle.Exclamation)
            ElseIf TransAuto = False AndAlso txtTransNum.Text = "" Then
                MsgBox("Please enter " & TransType & " Number", MsgBoxStyle.Exclamation)
            ElseIf TransAuto = False AndAlso IfExist(txtTransNum.Text, TransType) AndAlso TransID = "" Then
                MsgBox(TransType & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
            ElseIf TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID(ColumnID, DBTable)
                    If TransAuto Then
                        TransNo = GenerateTransNum(TransAuto, TransType, ColumnPK, DBTable)
                    Else
                        TransNo = txtTransNum.Text
                    End If
                    txtTransNum.Text = TransNo
                    SaveCollection()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    LoadCollection(TransID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    If transactionCleared = True Then
                        If MsgBox("This transaction is already cleared, are you sure you want to update this record?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                            If TransNo = txtTransNum.Text Then
                                TransNo = txtTransNum.Text
                                UpdateCollection()
                                UpdateBR(accountCleared)
                                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                                TransNo = txtTransNum.Text
                                LoadCollection(TransID)
                            Else
                                If Not IfExist(txtTransNum.Text, TransType) Then
                                    TransNo = txtTransNum.Text
                                    UpdateCollection()
                                    UpdateBR(accountCleared)
                                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                                    TransNo = txtTransNum.Text
                                    LoadCollection(TransID)
                                Else
                                    MsgBox(TransType & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
                                End If
                            End If
                        End If
                    Else
                        If TransNo = txtTransNum.Text Then
                            TransNo = txtTransNum.Text
                            UpdateCollection()
                            Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                            TransNo = txtTransNum.Text
                            LoadCollection(TransID)
                        Else
                            If Not IfExist(txtTransNum.Text, TransType) Then
                                TransNo = txtTransNum.Text
                                UpdateCollection()
                                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                                TransNo = txtTransNum.Text
                                LoadCollection(TransID)
                            Else
                                MsgBox(TransType & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
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

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess(TransType & "_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If txtStatus.Text <> "Cancelled" AndAlso MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim updateSQL As String
                        updateSQL = " UPDATE  tblCollection SET Status ='Cancelled' WHERE TransID = @TransID AND TransType = @TransType"
                        SQL.FlushParams()
                        SQL.AddParam("@TransID", TransID)
                        SQL.AddParam("@TransType", TransType)
                        SQL.ExecNonQuery(updateSQL)

                        updateSQL = " UPDATE  tblJE_Header SET Status ='Cancelled' WHERE RefTransID = @RefTransID  AND RefType = @RefType "
                        SQL.FlushParams()
                        SQL.AddParam("@RefTransID", TransID)
                        SQL.AddParam("@RefType", TransType)
                        SQL.ExecNonQuery(updateSQL)

                        If isReversalEntry = True Then
                            Dim insertSQL As String
                            insertSQL = " INSERT INTO tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, OtherRef) " & _
                                        " SELECT JE_No, AccntCode, VCECode, Credit, Debit, Particulars, RefNo, 0 AS LineNumber, OtherRef FROM tblJE_Details " & _
                                        " WHERE JE_No IN (SELECT DISTINCT JE_No FROM tblJE_Header WHERE RefType ='" & TransType & "' AND RefTransID ='" & TransID & "') "
                            SQL.ExecNonQuery(insertSQL)

                        End If
                        Msg("Record Cancelled successfully", MsgBoxStyle.Information)

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

                        LoadCollection(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, TransType)
                    Finally
                        RecordActivity(UserID, TransType, Me.Name.ToString, "CANCEL", "TransNo", TransNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                ElseIf txtStatus.Text = "Cancelled" AndAlso MsgBox("Are you sure you want to un-cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim updateSQL As String
                        updateSQL = " UPDATE  tblCollection SET Status = @Status WHERE TransID = @TransID AND TransType = @TransType"
                        SQL.FlushParams()
                        SQL.AddParam("@TransID", TransID)
                        SQL.AddParam("@TransType", TransType)
                        If ForApproval Then
                            SQL.AddParam("@Status", "Draft")
                        Else
                            SQL.AddParam("@Status", "Active")
                        End If
                        SQL.ExecNonQuery(updateSQL)

                        updateSQL = " UPDATE  tblJE_Header SET Status = @Status WHERE RefTransID = @RefTransID  AND RefType = @RefType "
                        SQL.FlushParams()
                        SQL.AddParam("@RefTransID", TransID)
                        SQL.AddParam("@RefType", TransType)
                        If ForApproval Then
                            SQL.AddParam("@Status", "Draft")
                        Else
                            SQL.AddParam("@Status", "Saved")
                        End If
                        SQL.ExecNonQuery(updateSQL)

                        If isReversalEntry = True Then
                            Dim insertSQL As String
                            insertSQL = " DELETE FROM tblJE_Details " & _
                                        " WHERE JE_No IN (SELECT DISTINCT JE_No FROM tblJE_Header WHERE RefType ='" & TransType & "' AND RefTransID ='" & TransID & "') AND LineNumber = 0 "
                            SQL.ExecNonQuery(insertSQL)
                        End If
                        Msg("Record un-cancelled successfully", MsgBoxStyle.Information)

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

                        LoadCollection(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, TransType)
                    Finally
                        RecordActivity(UserID, TransType, Me.Name.ToString, "CANCEL", "TransNo", TransNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub


    Private Sub frmCollection_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub tsbCopyPO_Click(sender As System.Object, e As System.EventArgs) Handles tsbCopyPO.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False

        f.ShowDialog("SI")
        LoadSI(f.transID)
        f.Dispose()
    End Sub

    Private Sub LoadSI(ByVal APV As String)
        Try
            Dim query As String
            query = " SELECT Ref_TransID AS TransID, SI_No, VCECode, Supplier AS  VCEName, Date AS Date_SI, Amount AS Net_Purchase, Amount/1.12 * 0.12 AS Input_VAT, Amount/1.12 * 0.02 AS EWT, Remarks, Reference AS Reference_Other, AccntCode, AccountTitle " & _
                    " FROM View_SI_Balance " & _
                    " WHERE  Ref_TransID ='" & APV & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                txtVCEName.Text = SQL.SQLDR("VCEName").ToString
                SI_ID = SQL.SQLDR("TransID")
                txtAPVRef.Text = SQL.SQLDR("SI_No")
                dgvEntry.Rows.Add(SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccountTitle").ToString, "0.00", CDec(SQL.SQLDR("Net_Purchase")).ToString("N2"), "", "", "", "SI:" & txtAPVRef.Text)
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("AccntCode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "SI:" & txtAPVRef.Text
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(SQL.SQLDR("Net_Purchase")).ToString("N2")

            End If
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton1.Click
        
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If TransNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblCollection " & _
                    "   INNER JOIN tblBranch  ON	          " & _
                    "   tblCollection.BranchCode = tblBranch.BranchCode    " & _
                    "     WHERE          " & _
                    " 	( tblCollection.BranchCode IN  " & _
                    " 	  ( " & _
                    "       SELECT    DISTINCT tblBranch.BranchCode  " & _
                    " 	    FROM      tblBranch   " & _
                    " 	    INNER JOIN tblUser_Access    ON          " & _
                    " 	    tblBranch.BranchCode = tblUser_Access.Code   " & _
                    " 	    AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                    " 	    AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                    " 	    WHERE     UserID ='" & UserID & "' " & _
                    " 	   ) " & _
                    "    )   " & _
                    " AND  TransNo > '" & TransNo & "' AND TransType = '" & TransType & "'  ORDER BY TransNo ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadCollection(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If TransNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblCollection " & _
                    "   INNER JOIN tblBranch  ON	          " & _
                    "   tblCollection.BranchCode = tblBranch.BranchCode    " & _
                    "     WHERE          " & _
                    " 	( tblCollection.BranchCode IN  " & _
                    " 	  ( " & _
                    "       SELECT    DISTINCT tblBranch.BranchCode  " & _
                    " 	    FROM      tblBranch   " & _
                    " 	    INNER JOIN tblUser_Access    ON          " & _
                    " 	    tblBranch.BranchCode = tblUser_Access.Code   " & _
                    " 	    AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                    " 	    AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                    " 	    WHERE     UserID ='" & UserID & "' " & _
                    " 	   ) " & _
                    "    )   " & _
                    " AND  TransNo < '" & TransNo & "'  AND TransType = '" & TransType & "' ORDER BY TransNo DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadCollection(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
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
            tsbDelete.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadCollection(TransID)
            tsbEdit.Enabled = True
            tsbCancel.Enabled = True
            tsbDelete.Enabled = True
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


    Private Sub btnSearchVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchVCE.Click
        Dim f As New frmVCE_Search
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        txtVCEName.Text = f.VCEName
        LoadCurrency()
        If TransType = "OR" Then
            If txtVCECode.Text <> "" Then
                f.Dispose()
                Dim r As New frmReceivables
                r.VCECode = txtVCECode.Text
                r.CollectDate = dtpDate.Value.Date
                r.Show()
                'TotalCashAmount()
            End If
        End If
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbDelete_Click(sender As System.Object, e As System.EventArgs) Handles tsbDelete.Click
        If Not AllowAccess(TransType & "_DEL") Then
            msgRestricted()
        Else
            If MsgBox("Are you sure you want to delete this transaction?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Delete") = MsgBoxResult.Yes Then
                Dim deleteSQL As String = "DELETE FROM tblCollection WHERE TransID = '" & TransID & "'"
                SQL.ExecNonQuery(deleteSQL)
                deleteSQL = "DELETE FROM tblJE_Header WHERE RefType = 'OR' AND RefTransID = '" & TransID & "'"
                SQL.ExecNonQuery(deleteSQL)
                MsgBox("Successfully Deleted!", MsgBoxStyle.Information, "Delete")
                tsbNew.PerformClick()
            End If
        End If
    End Sub

    Private Sub TestToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles TestToolStripMenuItem1.Click
        Dim f As New frmReport_Filter
        f.Report = "Collection List"
        f.ShowDialog(TransType)
        f.Dispose()
    End Sub
    Private Sub dgvEntry_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles dgvEntry.KeyDown
        If e.KeyCode = Keys.Enter Then
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
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = f.strAmount


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

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("Collection", TransID, TransType)
        f.Dispose()
    End Sub

    Private Sub cbBankTo_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbBankTo.SelectedIndexChanged
        'If disableEvent = False Then
        '    If cbBankTo.SelectedIndex <> -1 Then
        '        bankID = GetBankID(cbBankTo.SelectedItem)

        '        Dim query As String
        '        If isClearingEnabled Then
        '            query = " SELECT    tblBank_Master.AccountCode_Clearing AS AccountCode, AccountTitle" &
        '         " FROM      tblBank_Master INNER JOIN tblCOA_Master " &
        '         " ON        tblBank_Master.AccountCode_Clearing = tblCOA_Master.AccountCode " &
        '         " WHERE     BankID ='" & bankID & "' "
        '        Else
        '            query = " SELECT    tblBank_Master.AccountCode, AccountTitle" &
        '         " FROM      tblBank_Master INNER JOIN tblCOA_Master " &
        '         " ON        tblBank_Master.AccountCode = tblCOA_Master.AccountCode " &
        '         " WHERE     BankID ='" & bankID & "' "
        '        End If

        '        SQL.ReadQuery(query)
        '        If SQL.SQLDR.Read Then
        '            dgvEntry.Item(chAccntCode.Index, 0).Value = SQL.SQLDR("AccountCode").ToString
        '            dgvEntry.Item(chAccntTitle.Index, 0).Value = SQL.SQLDR("AccountTitle").ToString
        '        Else
        '            query = " SELECT AccountCode, AccountTitle FROM tblCOA_Master WHERE AccountCode = LEFT('" & cbBankTo.SelectedItem & "',CHARINDEX('-','" & cbBankTo.SelectedItem & "',1)-1) "
        '            SQL.ReadQuery(query)
        '            If SQL.SQLDR.Read Then
        '                bankID = SQL.SQLDR("AccountCode").ToString
        '                dgvEntry.Item(chAccntCode.Index, 0).Value = SQL.SQLDR("AccountCode").ToString
        '                dgvEntry.Item(chAccntTitle.Index, 0).Value = SQL.SQLDR("AccountTitle").ToString
        '            End If
        '        End If
        '        TotalDBCR()
        '    End If
        'End If

        If disableEvent = False Then
            If cbBankTo.SelectedIndex <> -1 Then
                SelectBank()
                TotalDBCR()
            End If
        End If
    End Sub

    Private Sub FromSJToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromSJToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = True
        f.cbFilter.Enabled = True
        f.btnSearch.Enabled = True

        f.ShowDialog("OR-SJ")
        LoadSJ(f.transID)
        f.Dispose()
    End Sub

    Private Sub LoadSJ(ByVal SJNo As String)
        Try
            Dim query As String
            query = " SELECT TransID, SJ_No, VCECode, VCEName, DateSJ, Amount , Remarks,  AccntCode, AccntTitle " & _
                    " FROM View_SJ_Balance " & _
                    " WHERE  TransID ='" & SJNo & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                txtVCEName.Text = SQL.SQLDR("VCEName").ToString
                txtAmount.Text = CDec(SQL.SQLDR("Amount")).ToString("N2")
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("AccntCode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccntTitle").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(SQL.SQLDR("Amount")).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = SQL.SQLDR("SJ_No").ToString


            End If
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadReceivables(ByVal Reference As String)
        Try
            Dim query As String
            Dim VCECode As String = ""
            Dim VCEName As String = ""
            Dim OV_Account As String = ""
            Dim DOV_Account As String = ""
            Dim AR_Account As String = ""
            Dim Balance As Decimal = 0
            Dim VatAmount As Decimal = 0
            query = " SELECT   Reference, VCECode, VCEName, AppDate, Balance , Remarks,  AccntCode, AccntTitle " & _
                    " FROM    view_AR_Balance " & _
                    " WHERE   Reference ='" & Reference & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                VCECode = SQL.SQLDR("VCECode").ToString
                VCEName = SQL.SQLDR("VCEName").ToString
                AR_Account = SQL.SQLDR("AccntCode").ToString
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
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = AR_Account
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(AR_Account)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(Balance).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = VCEName
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Reference


            Else
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = AR_Account
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(AR_Account)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(Balance).ToString("N2")
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

    Private Sub FromCAToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromCAToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        'f.txtFilter.Text = "Active"
        f.txtFilter.Text = "Released"
        f.chkBatch.Visible = True
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("CA-JV")



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

    Private Sub LoadCA(ByVal CA As String)
        Try
            Dim query As String
            query = " SELECT  TransID, [CA No.], Date, VCECode, VCEName, CA_Amount, Balance, Remarks,  AccntCode, AccountTitle, Status" & _
                    " FROM   View_CA_Balance " & _
                    " WHERE  TransID ='" & CA & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                CA_ID = SQL.SQLDR("TransID")
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                txtVCEName.Text = SQL.SQLDR("VCEName").ToString
                txtRemarks.Text = SQL.SQLDR("Remarks").ToString
                txtAmount.Text = SQL.SQLDR("Balance").ToString
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("AccntCode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(SQL.SQLDR("Balance")).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "CA:" & SQL.SQLDR("CA No.").ToString

            End If
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FromARToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromARToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        If txtVCECode.Text <> "" Then
            f.cbFilter.SelectedItem = "VCE Code"
            f.txtFilter.Text = txtVCECode.Text
        Else
            f.cbFilter.SelectedItem = "Status"
            f.txtFilter.Text = "Saved"
        End If
        f.chkBatch.Visible = True
        f.txtFilter.Enabled = True
        f.cbFilter.Enabled = True
        f.btnSearch.Enabled = True

        f.ShowDialog("OR-Receivables")

        If f.batch = True Then
            For Each row As DataGridViewRow In f.dgvList.Rows
                If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                    LoadReceivables(row.Cells(0).Value)
                End If
            Next
        Else
            If f.transID <> "" Then
                LoadReceivables(f.transID)
            End If
        End If
        txtAmount.Text = CDec(txtTotalCredit.Text) - CDec(txtTotalDebit.Text)
        TotalDBCR()
        f.Dispose()
    End Sub


    Private Sub txtAmount_Leave(sender As Object, e As System.EventArgs) Handles txtAmount.Leave

    End Sub

    Private Sub txtAmount_LostFocus(sender As Object, e As EventArgs) Handles txtAmount.LostFocus
        If IsNumeric(txtAmount.Text) Then
            txtAmount.Text = CDec(txtAmount.Text).ToString("N2")
        End If
    End Sub

    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged
        If IsNumeric(txtAmount.Text) AndAlso dgvEntry.Rows.Count > 0 Then
            dgvEntry.Item(chDebit.Index, 0).Value = CDec(txtAmount.Text).ToString("N2")
            TotalDBCR()
        End If
    End Sub

    Private Sub dgvEntry_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dgvEntry.CurrentCellDirtyStateChanged

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

    Private Sub txtVCECode_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCECode.TextChanged

    End Sub

    Private Sub FromCSToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromCSToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.chkBatch.Visible = True
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("CS")



        If f.batch = True Then
            For Each row As DataGridViewRow In f.dgvList.Rows
                If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                    LoadCS(f.transID)
                    TotalDBCR()
                    txtAmount.Text = CDec(txtTotalCredit.Text - txtTotalDebit.Text)
                End If
            Next
        Else
            If f.transID <> "" Then
                LoadCS(f.transID)
                TotalDBCR()
                txtAmount.Text = CDec(txtTotalCredit.Text - txtTotalDebit.Text)
            End If
        End If
        f.Dispose()
    End Sub


    Private Sub LoadCS(ByVal CS As String)
        Try
            Dim query As String
            query = " SELECT TransID,  tblCS.VCECode, VCEName " & _
                    " FROM tblCS " & _
                    " LEFT JOIN viewVCE_Master " & _
                    " ON viewVCE_Master.VCECode = tblCS.VCECode " & _
                    " WHERE  TransID ='" & CS & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                txtVCEName.Text = SQL.SQLDR("VCEName").ToString
                LoadCSCharges(CS)
            End If
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub LoadCSCharges(ByVal ID As Integer)
        Dim query As String


        query = " SELECT TransID, CS_No, DefaultAccount, Type, VCECode, NetAmount, AD_Discount, Discount,  VATAmount " & _
                " FROM  viewCS_Amount LEFT JOIN tblCOA_Master " & _
                " ON     tblCOA_Master.AccountCode = viewCS_Amount.DefaultAccount " & _
                " WHERE TransID = " & ID & " "
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
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(SQL.SQLDR("NetAmount")).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "CS:" & SQL.SQLDR("CS_No").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = SQL.SQLDR("Type").ToString

                If SQL.SQLDR("Discount").ToString <> 0 Then
                    dgvEntry.Rows.Add("")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(SQL.SQLDR("VCECode").ToString)
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("AD_Discount").ToString
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(SQL.SQLDR("AD_Discount").ToString)
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(SQL.SQLDR("Discount")).ToString("N2")
                End If

                If SQL.SQLDR("VATAmount").ToString <> 0 Then
                    dgvEntry.Rows.Add("")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(SQL.SQLDR("VCECode").ToString)
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = TAX_OV
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(TAX_OV)
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(SQL.SQLDR("VATAmount")).ToString("N2")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = "CS:" & SQL.SQLDR("CS_No").ToString
                End If
            End While
        Else
            JETransiD = 0
            dgvEntry.Rows.Clear()
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem1.Click

        If TransID <> "" Then
            frm2307Receiving.Amount = txtAmount.Text
            frm2307Receiving.TransID = TransID
            frm2307Receiving.Show()
        Else
            Msg("Please select transaction.", MsgBoxStyle.Information)
        End If
    End Sub



    Private Sub FromLCToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromLCToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        If txtVCECode.Text <> "" Then
            f.cbFilter.SelectedItem = "VCE Code"
            f.txtFilter.Text = txtVCECode.Text
        Else
            f.cbFilter.SelectedItem = "Status"
            f.txtFilter.Text = "Saved"
        End If
        f.chkBatch.Visible = False
        f.txtFilter.Enabled = True
        f.cbFilter.Enabled = True
        f.btnSearch.Enabled = True

        f.ShowDialog("LC-OR")

        If f.transID <> "" Then
            LoadLC_OR(f.transID)
        End If
        txtAmount.Text = CDec(txtTotalCredit.Text) - CDec(txtTotalDebit.Text)
        TotalDBCR()
        f.Dispose()
    End Sub

    Private Sub FromRealEstateLoanApprovedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromRealEstateLoanApprovedToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = True
        f.cbFilter.Enabled = True
        f.btnSearch.Enabled = True
        f.ShowDialog("RE-Loan")
        If f.transID <> "" Then
            RE_ID = f.transID
            LoadRE_Loan(RE_ID)

            txtAmount.Text = CDec(txtTotalCredit.Text) - CDec(txtTotalDebit.Text)
            TotalDBCR()
        End If

        f.Dispose()
    End Sub

    Private Sub LoadLC_OR(ByVal ID As String)
        Try
            Dim query As String
            Dim VCECode As String = ""
            Dim VCEName As String = ""
            Dim AdvanceRent As String = ""
            Dim RentIncome As String = ""
            Dim OV_Account As String = ""
            Dim DOV_Account As String = ""
            Dim Amount As Decimal = 0
            Dim Reference As String = ""
            Dim CashAccount As String = ""
            Dim PaymentNo As Integer
            query = " SELECT LM_RentalIncome, LM_AdvanceRent, TAX_DOV, TAX_OV FROM tblSystemSetup "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                RentIncome = SQL.SQLDR("LM_RentalIncome").ToString
                AdvanceRent = SQL.SQLDR("LM_AdvanceRent").ToString
                DOV_Account = SQL.SQLDR("TAX_DOV").ToString
                OV_Account = SQL.SQLDR("TAX_OV").ToString
            End If
            query = " SELECT  RowID, LC_No, VCECode, VCEName, PeriodFrom, PeriodTo, " &
                    "         CASE WHEN VATable = 1 AND VATInc = 1 THEN Amount/1.12 ELSE Amount END AS Amount, " &
                    "         CASE WHEN VATable = 1 AND VATInc = 1 THEN (Amount/1.12) * .12 " &
                    "              WHEN VATable = 1 AND VATInc = 0 THEN Amount * .12 " &
                    "              ELSE 0 " &
                    "         END AS VATAmount, " &
                    "         VATable, VATInc, CashAccount, PaymentNo  " &
                    " FROM    viewLC_ForOR " &
                    " WHERE   RowID ='" & ID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Reference = "LC:" & SQL.SQLDR("LC_No").ToString
                VCECode = SQL.SQLDR("VCECode").ToString
                VCEName = SQL.SQLDR("VCEName").ToString
                Amount = SQL.SQLDR("Amount")
                VATAmount = SQL.SQLDR("VATAmount")
                CashAccount = SQL.SQLDR("CashAccount").ToString
                PaymentNo = SQL.SQLDR("PaymentNo").ToString
                txtVCECode.Text = VCECode
                txtVCEName.Text = VCEName
            End If

            dgvEntry.Rows.Add("")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = AdvanceRent
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(AdvanceRent)
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(Amount).ToString("N2")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = VCEName
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Reference & "-" & PaymentNo

            If VATAmount > 0 Then
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = DOV_Account
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(DOV_Account)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(VATAmount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = VCEName
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Reference & "-" & PaymentNo


                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = OV_Account
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(OV_Account)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(VATAmount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = VCEName
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Reference & "-" & PaymentNo
            End If

            dgvEntry.Rows.Add("")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = RentIncome
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(RentIncome)
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(Amount).ToString("N2")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = VCEName
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Reference & "-" & PaymentNo


            dgvEntry.Rows.Add("")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = CashAccount
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(CashAccount)
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(Amount + VATAmount).ToString("N2")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = VCEName
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Reference
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FromREToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromREToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        If txtVCECode.Text <> "" Then
            f.cbFilter.SelectedItem = "VCE Code"
            f.txtFilter.Text = txtVCECode.Text
        Else
            f.cbFilter.SelectedItem = "Status"
            f.txtFilter.Text = "Saved"
        End If
        f.chkBatch.Visible = True
        f.txtFilter.Enabled = True
        f.cbFilter.Enabled = True
        f.btnSearch.Enabled = True
        f.ShowDialog("RE-Collection", dtpDate.Value.Date)

        If f.batch = True Then
            For Each row As DataGridViewRow In f.dgvList.Rows
                If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                    LoadRE_Equity(row.Cells(0).Value, "", "")
                End If
            Next
        Else
            If f.transID <> "" Then
                RE_ID = f.transID
                RE_SchedNum = f.Re_schednum
                LoadRE_Equity(RE_ID, RE_SchedNum, f.itemCode)
            End If
        End If
        txtAmount.Text = CDec(txtTotalCredit.Text) - CDec(txtTotalDebit.Text)
        TotalDBCR()
        f.Dispose()
    End Sub

    Private Sub LoadRE_Equity(ByVal ID As String, ByVal RENum As String, ByVal TransNum As String)
        Try
            Dim query As String
            Dim VCECode As String = ""
            Dim VCEName As String = ""
            Dim Equity As String = ""
            Dim Penalty As String = ""
            Dim OV_Account As String = ""
            Dim Amount As Decimal = 0
            Dim PenaltyAmount As Decimal = 0
            Dim Reference As String = ""
            Dim PaymentNo As Integer
            'query = " SELECT RE_Equity, RE_Penalty, TAX_OV FROM tblSystemSetup "
            query = " SELECT RE_AR, RE_Penalty, TAX_OV FROM tblSystemSetup "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Equity = SQL.SQLDR("RE_AR").ToString
                OV_Account = SQL.SQLDR("TAX_OV").ToString
                Penalty = SQL.SQLDR("RE_Penalty").ToString
            End If
            'query = " SELECT  ID, RE_No, VCECode, VCEName, DueDate, " &
            '        "         CASE WHEN VATable = 1 AND VATInc = 1 THEN Principal/1.12 ELSE Principal END AS Amount, " &
            '        "         CASE WHEN VATable = 1 AND VATInc = 1 THEN (Principal/1.12) * .12 " &
            '        "              WHEN VATable = 1 AND VATInc = 0 THEN Principal * .12 " &
            '        "              ELSE 0 " &
            '        "         END AS VATAmount, " &
            '        "         VATable, VATInc, Penalty, PaymentNo  " &
            '        " FROM    viewRE_PaymentDue " &
            '        " WHERE   ID ='" & ID & "' "
            query = "spRE_PaymentDue"
            SQL.FlushParams()
            SQL.AddParam("@AsOfDate", dtpDate.Value)
            SQL.AddParam("@ID", RENum)
            SQL.AddParam("@RE_No", TransNum)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Reference = "RE:" & SQL.SQLDR("RE_No").ToString
                VCECode = SQL.SQLDR("VCECode").ToString
                VCEName = SQL.SQLDR("VCEName").ToString
                Amount = SQL.SQLDR("Amount")
                PenaltyAmount = SQL.SQLDR("Penalty")
                cbCostCenter.SelectedItem = SQL.SQLDR("Project")
                VATAmount = SQL.SQLDR("VATAmount")
                PaymentNo = SQL.SQLDR("PaymentNo").ToString
                txtVCECode.Text = VCECode
                txtVCEName.Text = VCEName
            End If
            'If PenaltyAmount > 0 Then
            '    PenaltyAmount = 0
            '    dgvEntry.Rows.Add("")
            '    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = Penalty
            '    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(Penalty)
            '    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
            '    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(PenaltyAmount).ToString("N2")
            '    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
            '    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = VCEName
            '    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = "Penalty"
            '    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = ""
            '    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Reference & "-" & PaymentNo
            'End If
            If VATAmount > 0 And TransType = "OR" Then

                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = OV_Account
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(OV_Account)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(VATAmount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = VCEName
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = "Output VAT"
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Reference & "-" & PaymentNo
                VATType = "VAT (12%)"
            End If



            dgvEntry.Rows.Add("")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = Equity
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(Equity)
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(Amount).ToString("N2")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = VCEName
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = VATType
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Reference & "-" & PaymentNo
            LoadCurrency()
            TotalDBCR()
         Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FromREReservationToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromREReservationToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = True
        f.cbFilter.Enabled = True
        f.btnSearch.Enabled = True
        f.ShowDialog("RE-Reservation")

        If f.batch = True Then
            For Each row As DataGridViewRow In f.dgvList.Rows
                If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                    LoadRE(row.Cells(0).Value)
                End If
            Next
        Else
            If f.transID <> "" Then
                RE_ID = f.transID
                LoadRE(RE_ID)
            End If
        End If
        txtAmount.Text = CDec(txtTotalCredit.Text) - CDec(txtTotalDebit.Text)
        TotalDBCR()
        f.Dispose()

        'If f.transID <> "" Then
        '    RE_ID = f.transID
        '    LoadRE(RE_ID)

        '    txtAmount.Text = CDec(txtTotalCredit.Text) - CDec(txtTotalDebit.Text)
        '    TotalDBCR()
        'End If

        'f.Dispose()
    End Sub

    Private Sub LoadRE_Loan(ByVal RE_ID As String)
        Try
            Dim query As String
            Dim VCECode As String = ""
            Dim VCEName As String = ""
            Dim Account As String = ""
            Dim Amount As Decimal = 0
            Dim Reference As String = ""
            Dim paymenttype As String
            Dim PaymentNo As Integer
            'query = " SELECT RE_Reserve FROM tblSystemSetup "
            query = " SELECT RE_AR FROM tblSystemSetup "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Account = SQL.SQLDR("RE_AR").ToString
            End If
            query = "  SELECT PaymentType " &
                    "  FROM tblRE " &
                    "  WHERE   tblRE.Status ='Active' AND TransID = '" & RE_ID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                paymenttype = SQL.SQLDR("PaymentType").ToString
            End If
            query = " SELECT COUNT(TransID) + 1 as No " &
                    " FROM tblRE_Schedule " &
                    " WHERE TransID = '" & RE_ID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                PaymentNo = SQL.SQLDR("No").ToString
            End If

            query = " SELECT  TransID, RE_No, DateRE AS Date, tblRE.VCECode, VCEName, Balance, tblSaleProperty.Project   " &
                        " FROM    tblRE LEFT JOIN viewVCE_Master " &
                        " ON      tblRE.VCECode = viewVCE_Master.VCECode " &
                        " LEFT JOIN tblSaleProperty " &
                        " ON tblRE.PropCode = tblSaleProperty.UnitCode " &
                        " WHERE   tblRE.Status ='Active' AND TransID ='" & RE_ID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Reference = "RE:" & SQL.SQLDR("RE_No").ToString
                VCECode = SQL.SQLDR("VCECode").ToString
                VCEName = SQL.SQLDR("VCEName").ToString
                cbCostCenter.SelectedItem = SQL.SQLDR("Project").ToString
                Amount = SQL.SQLDR("Balance").ToString

                'If paymenttype = "Cash Sales" Then
                '    Amount = SQL.SQLDR("TCP_Reservation")
                'ElseIf paymenttype = "Deferred Cash" Then
                '    Amount = SQL.SQLDR("DC_Reservation")
                'ElseIf paymenttype = "Financing" Then
                '    Amount = SQL.SQLDR("ReservationAmount")
                'End If
                txtVCECode.Text = VCECode
                txtVCEName.Text = VCEName
            End If
            dgvEntry.Rows.Add("")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = Account
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(Account)
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(Amount).ToString("N2")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = VCEName
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = "Loanable Amount"
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Reference & "-" & PaymentNo
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub LoadRE(ByVal RE_ID As String)
        Try
            Dim query As String
            Dim VCECode As String = ""
            Dim VCEName As String = ""
            Dim Account As String = ""
            Dim Amount As Decimal = 0
            Dim Reference As String = ""
            Dim paymenttype As String
            Dim PaymentNo As Integer
            'query = " SELECT RE_Reserve FROM tblSystemSetup "
            query = " SELECT RE_AR FROM tblSystemSetup "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Account = SQL.SQLDR("RE_AR").ToString
            End If
            query = "  SELECT PaymentType " &
                    "  FROM tblRE " &
                    "  WHERE   tblRE.Status ='Active' AND TransID = '" & RE_ID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                paymenttype = SQL.SQLDR("PaymentType").ToString
            End If
            'query = " SELECT  TransID, RE_No, DateRE AS Date, tblRE.VCECode, VCEName, TCP_Reservation, DC_Reservation, ReservationAmount, tblSaleProperty.Project   " &
            '            " FROM    tblRE LEFT JOIN viewVCE_Master " &
            '            " ON      tblRE.VCECode = viewVCE_Master.VCECode " &
            '            " LEFT JOIN tblSaleProperty " &
            '            " ON tblRE.PropCode = tblSaleProperty.UnitCode " &
            '            " WHERE   tblRE.Status ='Active' AND TransID ='" & RE_ID & "' "
            query = "   SELECT  tblRE.TransID, tblRE.RE_No, DateRE AS Date, tblRE.VCECode, VCEName, TCP_Reservation, DC_Reservation, viewRE_Reservation.Balance AS  [ReservationAmount], tblSaleProperty.Project     " &
                    "  FROM    tblRE " &
                    "  INNER JOIN viewRE_Reservation  ON	   tblRE.TransID = viewRE_Reservation.TransID  " &
                    "  LEFT JOIN viewVCE_Master  ON      tblRE.VCECode = viewVCE_Master.VCECode   " &
                    "  LEFT JOIN tblSaleProperty  ON tblRE.PropCode = tblSaleProperty.UnitCode   " &
                    "  WHERE   tblRE.Status ='Active' AND tblRE.TransID = '" & RE_ID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Reference = "RE:" & SQL.SQLDR("RE_No").ToString
                VCECode = SQL.SQLDR("VCECode").ToString
                VCEName = SQL.SQLDR("VCEName").ToString
                cbCostCenter.SelectedItem = SQL.SQLDR("Project").ToString
                Amount = SQL.SQLDR("ReservationAmount")

                'If paymenttype = "Cash Sales" Then
                '    Amount = SQL.SQLDR("TCP_Reservation")
                'ElseIf paymenttype = "Deferred Cash" Then
                '    Amount = SQL.SQLDR("DC_Reservation")
                'ElseIf paymenttype = "Financing" Then
                '    Amount = SQL.SQLDR("ReservationAmount")
                'End If
                txtVCECode.Text = VCECode
                txtVCEName.Text = VCEName
            End If
            dgvEntry.Rows.Add("")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = Account
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(Account)
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(Amount).ToString("N2")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = VCEName
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chParticulars.Index).Value = "Reservation Fee"
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRef.Index).Value = Reference
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FromBillingStatementToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromBillingStatementToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        If txtVCECode.Text <> "" Then
            f.cbFilter.SelectedItem = "VCE Code"
            f.txtFilter.Text = txtVCECode.Text
        Else
            f.cbFilter.SelectedItem = "Status"
            f.txtFilter.Text = "Saved"
        End If
        f.chkBatch.Visible = True
        f.txtFilter.Enabled = True
        f.cbFilter.Enabled = True
        f.btnSearch.Enabled = True

        f.ShowDialog("BS-Receivables")

        If f.batch = True Then
            For Each row As DataGridViewRow In f.dgvList.Rows
                If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                    LoadReceivables(row.Cells(0).Value)
                End If
            Next
        Else
            If f.transID <> "" Then
                LoadReceivables(f.transID)
            End If
        End If
        txtAmount.Text = CDec(txtTotalCredit.Text) - CDec(txtTotalDebit.Text)
        TotalDBCR()
        f.Dispose()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            SelectBank()
        Else
            SelectPaymentType()
            bankID = 0
        End If
        cbBankTo.Enabled = CheckBox1.Checked
    End Sub

    Private Sub SelectBank()
        If cbBankTo.Items.Count > 0 Then
            If cbBankTo.SelectedIndex = -1 Then
                cbBankTo.SelectedIndex = 0
            End If
            bankID = GetBankID(cbBankTo.SelectedItem)

            Dim query As String
            query = " SELECT    tblBank_Master.AccountCode, AccountTitle" &
                    " FROM      tblBank_Master INNER JOIN tblCOA_Master " &
                    " ON        tblBank_Master.AccountCode = tblCOA_Master.AccountCode " &
                    " WHERE     BankID ='" & bankID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                dgvEntry.Item(chAccntCode.Index, 0).Value = SQL.SQLDR("AccountCode").ToString
                dgvEntry.Item(chAccntTitle.Index, 0).Value = SQL.SQLDR("AccountTitle").ToString

            End If
        End If

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