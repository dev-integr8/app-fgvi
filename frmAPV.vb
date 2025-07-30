Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmAPV
    Dim TransID, RefID, RR_RefID, JETransiD As String
    Dim APVNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "APV"
    Dim ColumnID As String = "TransID"
    Dim ColumnPK As String = "APV_No"
    Dim DBTable As String = "tblAPV"
    Dim TransAuto As Boolean
    Dim ForApproval As Boolean = False
    Dim AccntCode As String
    Dim PO_ID As Integer
    Dim PCV As String
    Dim accntDR, accntCR, accntVAT, accntAdvance As String
    Dim Adv_Amount As Decimal
    Dim PayPeriod As String = ""
    Dim Payroll_OrgID As Integer = 0
    Dim isReversalEntry As Boolean = False

    Dim Valid As Boolean = True
    Dim InvalidTemplate As Boolean = False
    Dim path As String
    Dim templateName As String = "TEMPLATE"
    Public excelPW As String = "@dm1nEvo"


    ' SETUP VARIABLES
    Dim pendingAPsetup As Boolean
    Dim accntInputVAT, accntAPpending As String

    Public Overloads Function ShowDialog(ByVal DocID As String) As Boolean
        TransID = DocID
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub APV_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            LoadSetup()
            ForApproval = GetTransApproval(ModuleID)
            isReversalEntry = GetTransisReversal(ModuleID)
            LoadChartOfAccount()
            LoadCostCenter()

            If TransID <> "" Then
                If Not AllowAccess("APV_VIEW") Then
                    msgRestricted()
                    dtpDocDate.Value = Date.Today.Date
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
                Else
                    LoadAPV(TransID)
                End If
            Else
                tsbSearch.Enabled = True
                tsbNew.Enabled = True
                tsbEdit.Enabled = False
                tsbSave.Enabled = False
                tsbCancel.Enabled = False
                tsbClose.Enabled = False
                tsbUpload.Enabled = False
                tsbPrevious.Enabled = False
                tsbNext.Enabled = False
                tsbExit.Enabled = True
                tsbPrint.Enabled = False
                tsbCopy.Enabled = False
                EnableControl(False)
                If dtpDocDate.Value < GetMaxPEC() Then
                    tsbEdit.Enabled = False
                    tsbCancel.Enabled = False
                End If
            End If
            
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadSetup()
        Dim query As String
        query = " SELECT  ISNULL(AP_SetupPendingAP,0) AS AP_SetupPendingAP, AP_Pending, AP_InputVAT  FROM tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            pendingAPsetup = SQL.SQLDR("AP_SetupPendingAP")
            accntAPpending = SQL.SQLDR("AP_Pending").ToString
            accntInputVAT = SQL.SQLDR("AP_InputVAT").ToString
        End If
    End Sub


    Private Sub LoadChartOfAccount()
        Dim query As String
        query = " SELECT    AccountTitle " & _
                " FROM      tblDefaultAccount INNER JOIN tblCOA_Master " & _
                " ON        tblDefaultAccount.AccntCode = tblCOA_Master.AccountCode " & _
                " WHERE     tblDefaultAccount.Type = 'AP' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            cbCreditAccount.Items.Add(SQL.SQLDR("AccountTitle"))
        End While
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        dtpDocDate.Enabled = Value
        txtVCEName.Enabled = Value
        btnSearchVCE.Enabled = Value
        cbCurrency.Enabled = Value
        txtConversion.Enabled = Value
        dgvEntry.AllowUserToAddRows = Value
        dgvEntry.AllowUserToDeleteRows = Value
        cbCostCenter.Enabled = Value
        If Value = True Then
            dgvEntry.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvEntry.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        txtPORef.Enabled = Value
        txtSINo.Enabled = Value
        txtAmount.Enabled = Value
        txtDiscount.Enabled = Value
        txtVAT.Enabled = Value
        txtNet.Enabled = Value
        cbCreditAccount.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Private Sub LoadAPV(ID As String)
        Dim query, Currency, cc As String
        query = " SELECT  TransID, APV_No, tblAPV.VCECOde, VCEName, DateAPV, ISNULL(Amount,0) AS Amount, ISNULL(Discount,0) AS Discount, " &
                "         ISNULL(InputVAT,0) AS InputVAT, ISNULL(NetPurchase,0) AS NetPurchase, Remarks, PO_Ref, SI_Ref, RR_Ref, tblAPV.Status, AccntCode, Currency, ISNULL(Exchange_Rate,0) AS Exchange_Rate, CostCenter  " &
                " FROM    tblAPV INNER JOIN viewVCE_Master " &
                " ON      tblAPV.VCECode = viewVCE_Master.VCECode " &
                " WHERE   TransID ='" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            APVNo = SQL.SQLDR("APV_No").ToString
            txtTransNum.Text = APVNo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
            dtpDocDate.Value = SQL.SQLDR("DateAPV")
            cc = SQL.SQLDR("CostCenter").ToString
            txtAmount.Text = CDec(SQL.SQLDR("Amount").ToString).ToString("N2")
            txtDiscount.Text = CDec(SQL.SQLDR("Discount")).ToString("N2")
            txtVAT.Text = CDec(SQL.SQLDR("InputVAT")).ToString("N2")
            txtNet.Text = CDec(SQL.SQLDR("NetPurchase")).ToString("N2")
            Currency = SQL.SQLDR("Currency").ToString
            txtConversion.Text = CDec(SQL.SQLDR("Exchange_Rate")).ToString("N4")
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtPORef.Text = SQL.SQLDR("PO_Ref").ToString
            txtRRRef.Text = SQL.SQLDR("RR_Ref").ToString
            txtSINo.Text = SQL.SQLDR("SI_Ref").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            disableEvent = True
            cbCostCenter.SelectedItem = cc
            cbCreditAccount.SelectedItem = GetAccntTitle(SQL.SQLDR("AccntCode").ToString)
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
            LoadAccountingEntry(TransID)
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
            tsbSave.Enabled = False
            tsbNew.Enabled = True
            tsbSearch.Enabled = True
            tsbExit.Enabled = True
            tsbCopy.Enabled = False
            If dtpDocDate.Value <= GetMaxPEC() Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            End If
            EnableControl(False)
        Else
            ClearText()
        End If
    End Sub

    Private Function LoadPONo(PO_ID As Integer) As String
        Dim query As String
        query = " SELECT PO_No FROM tblPO WHERE TransID = '" & PO_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("PO_No")
        Else
            Return 0
        End If
    End Function

    Private Sub LoadAccountingEntry(ByVal TransID As Integer)
        Try
            Dim query As String
            query = " SELECT ID, JE_No, View_GL_Transaction.BranchCode, View_GL_Transaction.AccntCode, AccntTitle, View_GL_Transaction.VCECode, View_GL_Transaction.VCEName, Particulars, " & _
                    "        ISNULL(Debit,0) AS Debit, ISNULL(Credit,0) AS Credit, RefNo, CostCenter, SL_Code, CIP_Code , VATType, ProfitCenter, ATC_Code " & _
                    " FROM   View_GL_Transaction  " & _
                    " WHERE  RefType ='APV' AND RefTransID ='" & TransID & "' AND isUpload <> 1" & _
                    " ORDER BY LineNumber "
            SQL.ReadQuery(query)
            dgvEntry.Rows.Clear()
            Dim rowsCount As Integer = 0
            If SQL.SQLDR.HasRows Then
                While SQL.SQLDR.Read

                    JETransiD = SQL.SQLDR("JE_No").ToString
                    dgvEntry.Rows.Add(SQL.SQLDR("AccntCode").ToString)
                    dgvEntry.Rows(rowsCount).Cells(dgcAccntTitle.Index).Value = SQL.SQLDR("AccntTitle").ToString
                    dgvEntry.Rows(rowsCount).Cells(dgcDebit.Index).Value = IIf(SQL.SQLDR("Debit") = 0, "", CDec(SQL.SQLDR("Debit")).ToString("N2"))
                    dgvEntry.Rows(rowsCount).Cells(dgcCredit.Index).Value = IIf(SQL.SQLDR("Credit") = 0, "", CDec(SQL.SQLDR("Credit")).ToString("N2"))
                    dgvEntry.Rows(rowsCount).Cells(dgcVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                    dgvEntry.Rows(rowsCount).Cells(dgcVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                    dgvEntry.Rows(rowsCount).Cells(dgcParticulars.Index).Value = SQL.SQLDR("Particulars").ToString
                    dgvEntry.Rows(rowsCount).Cells(dgcRefNo.Index).Value = SQL.SQLDR("RefNo").ToString
                    dgvEntry.Rows(rowsCount).Cells(chVATType.Index).Value = SQL.SQLDR("VATType").ToString
                    dgvEntry.Rows(rowsCount).Cells(chATCCode.Index).Value = SQL.SQLDR("ATC_Code").ToString
                    dgvEntry.Rows(rowsCount).Cells(dgcCC.Index).Value = SQL.SQLDR("CostCenter").ToString
                    dgvEntry.Rows(rowsCount).Cells(chCost_Center.Index).Value = GetCCName(SQL.SQLDR("CostCenter").ToString)
                    dgvEntry.Rows(rowsCount).Cells(chProfit_Code.Index).Value = SQL.SQLDR("ProfitCenter").ToString
                    dgvEntry.Rows(rowsCount).Cells(chProfit_Center.Index).Value = GetPCName(SQL.SQLDR("ProfitCenter").ToString)
                    dgvEntry.Rows(rowsCount).Cells(dgcCIP.Index).Value = SQL.SQLDR("CIP_Code").ToString
                    dgvEntry.Rows(rowsCount).Cells(chCIP_Description.Index).Value = GetCIPName(SQL.SQLDR("CIP_Code").ToString)

                    rowsCount += 1
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

    Private Sub TotalDBCR()
        ' COMPUTE TOTAL DEBIT
        Dim db As Decimal = 0

        For i As Integer = 0 To dgvEntry.Rows.Count - 1
            If dgvEntry.Item(dgcAccntCode.Index, i).Value <> "" AndAlso Val(dgvEntry.Item(dgcDebit.Index, i).Value) <> 0 Then
                db = db + Double.Parse(dgvEntry.Item(dgcDebit.Index, i).Value).ToString("N2")
            End If
        Next
        txtTotalDebit.Text = db.ToString("N2")

        ' COMPUTE TOTAL CREDIT
        Dim b As Decimal = 0

        For i As Integer = 0 To dgvEntry.Rows.Count - 1
            If dgvEntry.Item(dgcAccntCode.Index, i).Value <> "" AndAlso Val(dgvEntry.Item(dgcCredit.Index, i).Value) <> 0 Then
                b = b + Double.Parse(dgvEntry.Item(dgcCredit.Index, i).Value).ToString("N2")
            End If
        Next
        txtTotalCredit.Text = b.ToString("N2")
    End Sub

    Private Sub txtVCEName_KeyDown_1(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.txtFilter.Text = txtVCEName.Text
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            txtVCEName.Text = f.VCEName
            LoadCurrency()
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
        txtTransNum.Clear()
        txtVCECode.Clear()
        txtVCEName.Clear()
        dgvEntry.Rows.Clear()
        txtRemarks.Clear()
        txtPORef.Clear()
        txtRRRef.Clear()
        txtStatus.Text = "Open"
        txtAmount.Text = "0.00"
        txtDiscount.Text = "0.00"
        txtVAT.Text = "0.00"
        txtNet.Text = "0.00"
        txtTotalDebit.Text = "0.00"
        txtTotalCredit.Text = "0.00"
        txtSINo.Clear()
        dgvEntry.Rows.Clear()
        cbCreditAccount.SelectedIndex = -1
        cbCurrency.Items.Clear()
        txtConversion.Text = ""
        dtpDocDate.MinDate = GetMaxPEC().AddDays(1)
        dtpDocDate.Value = Date.Today.Date
        PayPeriod = ""
        cbCostCenter.SelectedItem = ""
        Payroll_OrgID = 0
    End Sub

    Private Sub LoadPO(ByVal ID As String)

        Dim query As String
        query = " SELECT tblPO.TransID, tblPO.PO_No, DatePO AS Date, tblPO.VCECode, VCEName,  " & _
                " ISNULL(ADV_Amount,0) AS Advances, GrossAmount AS Amount, " & _
                " VATAmount AS VAT, " & _
                " NetAmount AS NetPurchase, " & _
                " Remarks, tblPO.AccntCode, ADV.AccntCode  AS ADVAccount " & _
                " FROM tblPO INNER JOIN tblVCE_Master " & _
                " ON tblPO.VCECode = tblVCE_Master.VCECode " & _
                " INNER JOIN viewPO_Status " & _
                " ON tblPO.TransID = viewPO_Status.TransID " & _
                " LEFT JOIN " & _
                " ( SELECT PO_Ref, AccntCode, SUM(ADV_Amount) AS ADV_Amount  FROM tblADV WHERE Status ='Closed' GROUP BY PO_Ref, AccntCode) AS ADV " & _
                " ON tblPO.TransID  = ADV.PO_Ref " & _
                " WHERE viewPO_Status.Status ='Closed' AND tblPO.TransID ='" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            RefID = SQL.SQLDR("TransID")
            txtPORef.Text = SQL.SQLDR("PO_No").ToString
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
            txtAmount.Text = CDec(SQL.SQLDR("Amount")).ToString("N2")
            txtDiscount.Text = "0.00"
            txtVAT.Text = CDec(SQL.SQLDR("VAT")).ToString("N2")
            txtNet.Text = CDec(SQL.SQLDR("NetPurchase")).ToString("N2")
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            accntDR = SQL.SQLDR("AccntCode").ToString
            Adv_Amount = SQL.SQLDR("Advances").ToString
            accntAdvance = SQL.SQLDR("ADVAccount").ToString
            GenerateEntry(ID)
        Else
            ClearText()
        End If
    End Sub

    Private Sub GenerateEntry(ByVal PO_ID As Integer)
        Dim query As String
        Dim amount As Decimal = 0
        If pendingAPsetup = False Then
            Dim baseAmt As Decimal = 0
            query = " SELECT BaseAmount, AD_Inv FROM viewAPV_InvEntry WHERE TransID = '" & PO_ID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                accntDR = SQL.SQLDR("AD_Inv").ToString
                baseAmt = SQL.SQLDR("BaseAmount").ToString
            End If
            accntCR = GetAccntCode(cbCreditAccount.SelectedItem)
            dgvEntry.Rows.Clear()
            dgvEntry.Rows.Add("")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = accntDR
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(accntDR)
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = CDec(txtNet.Text) - CDec(txtVAT.Text).ToString("N2")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = ""


            If CDec(txtVAT.Text) <> 0 Then
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = accntInputVAT
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(accntInputVAT)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = CDec(txtVAT.Text).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = ""
            End If

            If Adv_Amount <> 0 Then
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = accntAdvance
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(accntAdvance)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = CDec(Adv_Amount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = ""
            End If

            If CDec(txtNet.Text) - Adv_Amount <> 0 Then
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = accntCR
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(accntCR)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = CDec(txtNet.Text - Adv_Amount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = ""
            End If

            TotalDBCR()
        Else
            accntCR = GetAccntCode(cbCreditAccount.SelectedItem)
            query = " SELECT RefNo, SUM(Credit-Debit) AS Debit, VCECode, VCEName  " & _
                    " FROM View_GL WHERE RefType='RR' AND RefTransID IN (SELECT TransID FROM tblRR WHERE PO_Ref = '" & PO_ID & "') AND AccntCode ='" & accntAPpending & "' " & _
                    " GROUP BY RefNo, VCECode, VCEName  "
            SQL.GetQuery(query)
            dgvEntry.Rows.Clear()
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                    dgvEntry.Rows.Add("")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = accntAPpending
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(accntAPpending)
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = CDec(row.Item(1)).ToString("N2")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = ""
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = txtVCECode.Text
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = txtVCEName.Text
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcRefNo.Index).Value = row.Item(0).ToString
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = ""
                    amount += row.Item(1)
                Next

                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = accntCR
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(accntCR)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = CDec(amount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = txtVCECode.Text
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = txtVCEName.Text
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcRefNo.Index).Value = "APV:" & txtTransNum.Text
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = ""
            End If
        End If

    End Sub



    Private Sub dgvCVRR_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntry.CellEndEdit
        Dim rowIndex As Integer = dgvEntry.CurrentCell.RowIndex
        Dim colIndex As Integer = dgvEntry.CurrentCell.ColumnIndex
        Dim Accountcode As String
        Dim COA_Group As String
        Try
            Select Case colIndex
                Case dgcAccntCode.Index
                    'dgvEntry.Item(dgcAccntTitle.Index, e.RowIndex).Value = GetAccntTitle((dgvEntry.Item(dgcAccntCode.Index, e.RowIndex).Value))
                    Accountcode = dgvEntry.Item(dgcAccntCode.Index, e.RowIndex).Value
                    Dim f As New frmCOA_Search
                    f.accttile = Accountcode
                    f.ShowDialog("AccntCode", Accountcode)
                    dgvEntry.Item(dgcAccntCode.Index, e.RowIndex).Value = f.accountcode
                    dgvEntry.Item(dgcAccntTitle.Index, e.RowIndex).Value = f.accttile
                    COA_Group = f.COA_Group
                    f.Dispose()
                    dgvEntry.Item(dgcParticulars.Index, e.RowIndex).Value = txtRemarks.Text
                    dgvEntry.Item(dgcVCECode.Index, e.RowIndex).Value = txtVCECode.Text
                    dgvEntry.Item(dgcVCEName.Index, e.RowIndex).Value = txtVCEName.Text


                Case dgcAccntTitle.Index
                    Accountcode = dgvEntry.Item(dgcAccntTitle.Index, e.RowIndex).Value
                    Dim f As New frmCOA_Search
                    f.accttile = Accountcode
                    f.ShowDialog("AccntTitle", Accountcode)
                    dgvEntry.Item(dgcAccntCode.Index, e.RowIndex).Value = f.accountcode
                    dgvEntry.Item(dgcAccntTitle.Index, e.RowIndex).Value = f.accttile
                    COA_Group = f.COA_Group
                    f.Dispose()
                    dgvEntry.Item(dgcParticulars.Index, e.RowIndex).Value = txtRemarks.Text
                    dgvEntry.Item(dgcVCECode.Index, e.RowIndex).Value = txtVCECode.Text
                    dgvEntry.Item(dgcVCEName.Index, e.RowIndex).Value = txtVCEName.Text

                Case dgcDebit.Index
                    If IsNumeric(dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value) Then
                        dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value = CDec(dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value).ToString("N2")
                    End If
                    TotalDBCR()

                Case dgcCredit.Index
                    If IsNumeric(dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value) Then
                        dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value = CDec(dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value).ToString("N2")
                    End If
                    TotalDBCR()

                Case dgcVCEName.Index
                    Dim CC, PC As String
                    Dim f As New frmVCE_Search
                    f.txtFilter.Text = dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                    f.ShowDialog()
                    dgvEntry.Item(dgcVCECode.Index, e.RowIndex).Value = f.VCECode
                    dgvEntry.Item(dgcVCEName.Index, e.RowIndex).Value = f.VCEName
                    CC = GetCC(f.VCECode)
                    PC = GetPC(f.VCECode)
                    f.Dispose()

                    
                Case chCost_Center.Index
                    If dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value <> Nothing Then
                        Dim f1 As New frmCC_PC_Search
                        f1.Text = "Search of Cost Center"
                        f1.Type = "Cost Center"
                        f1.txtSearch.Text = dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                        f1.FilterText = dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                        f1.ShowDialog()
                        If f1.Code <> "" And f1.Description <> "" Then
                            dgvEntry.Item(dgcCC.Index, e.RowIndex).Value = f1.Code
                            dgvEntry.Item(chCost_Center.Index, e.RowIndex).Value = f1.Description
                        End If
                        f1.Dispose()
                    End If
                Case chProfit_Center.Index
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
            End Select
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function GetTransID() As Integer
        Dim query As String
        query = " SELECT MAX(TransID) + 1 AS TransID FROM tblAPV"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() And Not IsDBNull(SQL.SQLDR("TransID")) Then
            Return SQL.SQLDR("TransID")
        Else
            Return 1
        End If
    End Function

    Private Sub SaveAPV()
        Dim SQL1 As New SQLControl
        SQL1.BeginTransaction()
        Try
            If cbCreditAccount.SelectedIndex = -1 Then
                AccntCode = ""
            Else
                AccntCode = GetAccntCode(cbCreditAccount.SelectedItem)
            End If
            Dim insertSQL As String
            activityStatus = True
            insertSQL = " INSERT INTO " &
                        " tblAPV    (TransID, APV_No, BranchCode, BusinessCode, VCECode, DateAPV, SI_Ref, AccntCode, Amount, " &
                        "           Discount, InputVAT, NetPurchase, Currency, Exchange_Rate, Remarks, PO_Ref, WhoCreated, RR_Ref, PayPeriod, OrgID, Status, CostCenter) " &
                        " VALUES (@TransID, @APV_No, @BranchCode, @BusinessCode, @VCECode, @DateAPV, @SI_Ref, @AccntCode, @Amount, " &
                        "           @Discount, @InputVAT, @NetPurchase, @Currency, @Exchange_Rate, @Remarks, @PO_Ref, @WhoCreated, @RR_Ref, @PayPeriod, @OrgID, @Status, @CostCenter) "
            SQL1.FlushParams()
            SQL1.AddParam("@TransID", TransID)
            SQL1.AddParam("@APV_No", APVNo)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@VCECode", txtVCECode.Text)
            SQL1.AddParam("@DateAPV", dtpDocDate.Value.Date)
            SQL1.AddParam("@AccntCode", AccntCode)
            SQL1.AddParam("@Amount", IIf(txtAmount.Text = "", 0, CDec(txtAmount.Text)))
            SQL1.AddParam("@Discount", IIf(txtDiscount.Text = "", 0, CDec(txtDiscount.Text)))
            SQL1.AddParam("@InputVAT", IIf(txtVAT.Text = "", 0, CDec(txtVAT.Text)))
            SQL1.AddParam("@NetPurchase", IIf(txtNet.Text = "", 0, CDec(txtNet.Text)))
            SQL1.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL1.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@PO_Ref", IIf(RefID = Nothing, 0, RefID))
            SQL1.AddParam("@RR_Ref", IIf(txtRRRef.Text = Nothing, 0, txtRRRef.Text))
            SQL1.AddParam("@SI_Ref", txtSINo.Text)
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.AddParam("@PayPeriod", PayPeriod)
            SQL1.AddParam("@OrgID", Payroll_OrgID)
            SQL1.AddParam("@CostCenter", cbCostCenter.SelectedItem)
            If ForApproval = True Then SQL1.AddParam("@Status", "Draft") Else SQL1.AddParam("@Status", "Active")
            SQL1.ExecNonQuery(insertSQL)

            Dim updateSQl As String
            updateSQl = " UPDATE tblPO SET Status ='Posted' WHERE TransID ='" & RefID & "' "
            SQL1.ExecNonQuery(updateSQl)

            JETransiD = GenerateTransID("JE_No", "tblJE_Header")

            insertSQL = " INSERT INTO " &
                        " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, " &
                        "               Book, TotalDBCR, Currency, Exchange_Rate, Remarks, WhoCreated, Status, CostCenter) " &
                        " VALUES(@JE_No,@AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, " &
                        "               @Book, @TotalDBCR, @Currency, @Exchange_Rate, @Remarks, @WhoCreated, @Status, @CostCenter)"
            SQL1.FlushParams()
            SQL1.AddParam("@JE_No", JETransiD)
            SQL1.AddParam("@AppDate", dtpDocDate.Value.Date)
            SQL1.AddParam("@RefType", "APV")
            SQL1.AddParam("@RefTransID", TransID)
            SQL1.AddParam("@Book", "Accounts Payable")
            SQL1.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
            SQL1.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL1.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@CostCenter", cbCostCenter.SelectedItem)
            If ForApproval = True Then SQL1.AddParam("@Status", "Draft") Else SQL1.AddParam("@Status", "Saved")
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.ExecNonQuery(insertSQL)

            ' JETransiD = LoadJE("APV", TransID)
            Dim HeaderCC As String = cbCostCenter.SelectedItem

            Dim line As Integer = 1
            For Each item As DataGridViewRow In dgvEntry.Rows
                If item.Cells(dgcAccntCode.Index).Value <> Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, CIP_Code, CostCenter, VATType, ProfitCenter, ATC_Code) " & _
                                " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @CIP_Code, @CostCenter, @VATType, @ProfitCenter, @ATC_Code)"
                    SQL1.FlushParams()
                    SQL1.AddParam("@JE_No", JETransiD)
                    SQL1.AddParam("@AccntCode", item.Cells(dgcAccntCode.Index).Value.ToString)
                    If item.Cells(dgcVCECode.Index).Value <> Nothing AndAlso item.Cells(dgcVCECode.Index).Value <> "" Then
                        SQL1.AddParam("@VCECode", item.Cells(dgcVCECode.Index).Value.ToString)
                    Else
                        SQL1.AddParam("@VCECode", txtVCECode.Text)
                    End If
                    If item.Cells(dgcDebit.Index).Value <> Nothing AndAlso IsNumeric(item.Cells(dgcDebit.Index).Value) Then
                        SQL1.AddParam("@Debit", CDec(item.Cells(dgcDebit.Index).Value))
                    Else
                        SQL1.AddParam("@Debit", 0)
                    End If
                    If item.Cells(dgcCredit.Index).Value <> Nothing AndAlso IsNumeric(item.Cells(dgcCredit.Index).Value) Then
                        SQL1.AddParam("@Credit", CDec(item.Cells(dgcCredit.Index).Value))
                    Else
                        SQL1.AddParam("@Credit", 0)
                    End If
                    If item.Cells(dgcParticulars.Index).Value <> Nothing AndAlso item.Cells(dgcParticulars.Index).Value <> "" Then
                        SQL1.AddParam("@Particulars", item.Cells(dgcParticulars.Index).Value.ToString)
                    Else
                        SQL1.AddParam("@Particulars", txtRemarks.Text)
                    End If
                    If cbCostCenter.SelectedItem = "" Then
                        If item.Cells(dgcCC.Index).Value <> Nothing AndAlso item.Cells(dgcCC.Index).Value <> "" Then
                            SQL1.AddParam("@CostCenter", item.Cells(dgcCC.Index).Value.ToString)
                        Else
                            SQL1.AddParam("@CostCenter", "")
                        End If
                    Else
                        SQL1.AddParam("@CostCenter", HeaderCC)
                    End If
                    If item.Cells(dgcCIP.Index).Value <> Nothing AndAlso item.Cells(dgcCIP.Index).Value <> "" Then
                            SQL1.AddParam("@CIP_Code", item.Cells(dgcCIP.Index).Value.ToString)
                        Else
                            SQL1.AddParam("@CIP_Code", "")
                        End If
                        If item.Cells(chProfit_Code.Index).Value <> Nothing AndAlso item.Cells(chProfit_Code.Index).Value <> "" Then
                            SQL1.AddParam("@ProfitCenter", item.Cells(chProfit_Code.Index).Value.ToString)
                        Else
                            SQL1.AddParam("@ProfitCenter", "")
                        End If
                        If item.Cells(dgcRefNo.Index).Value <> Nothing AndAlso item.Cells(dgcRefNo.Index).Value <> "" Then
                            SQL1.AddParam("@RefNo", item.Cells(dgcRefNo.Index).Value.ToString)
                        Else
                            SQL1.AddParam("@RefNo", "")
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
                        SQL1.AddParam("@LineNumber", line)
                        SQL1.ExecNonQuery(insertSQL)

                        Dim update As String
                        If item.Cells(chRecordID.Index).Value <> Nothing Then
                            update = " UPDATE tblPCV_Details Set Status = 'Closed' " &
                                 " WHERE RecordID = '" & item.Cells(chRecordID.Index).Value & "'"
                            SQL1.ExecNonQuery(update)
                        End If

                        If item.Cells(dgcRefNo.Index).Value <> Nothing Then
                            If item.Cells(dgcRefNo.Index).Value.Contains("PCV:") Then
                                PCV = dgvEntry.Item(dgcRefNo.Index, line).Value.ToString.Replace("PCV:", "")
                                update = " UPDATE tblPCV SET Status ='Closed' WHERE PCV_No = '" & PCV & "'"
                                SQL1.ExecNonQuery(update)
                                'ElseIf item.Cells(dgcRefNo.Index).Value.Contains("ATD:") Then
                                '    Dim ATD As String
                                '    ATD = dgvEntry.Item(dgcRefNo.Index, line).Value.ToString.Replace("ATD:", "")
                                '    update = " UPDATE tblATD SET Status ='Posted' WHERE ATD_No = '" & ATD & "'"
                                '    SQL1.ExecNonQuery(update)
                            End If
                        End If
                        line += 1
                    End If
            Next

            SQL1.Commit()
            If PayPeriod <> "" Then
                SetPayrollDatabase()
                insertSQL = " INSERT INTO tblPayroll_EntryPosting(Payroll_Period, OrgCost_ID, Status, DateCreated, WhoCreated) " & _
                            " VALUES(@Payroll_Period, @OrgCost_ID, 'Posted', GETDATE(), @WhoCreated) "
                SQL_RUBY.FlushParams()
                SQL_RUBY.AddParam("@Payroll_Period", PayPeriod)
                SQL_RUBY.AddParam("@OrgCost_ID", Payroll_OrgID)
                SQL_RUBY.AddParam("@WhoCreated", UserID)
                SQL_RUBY.ExecNonQuery(insertSQL)
                PayPeriod = ""
                Payroll_OrgID = 0
            End If
        Catch ex As Exception
            SQL1.Rollback()
            TransID = ""
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "APV_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL1.FlushParams()
        End Try
    End Sub

    Private Sub UpdateAPV()
        Dim SQL As New SQLControl
        SQL.BeginTransaction()
        Try
            If cbCreditAccount.SelectedIndex = -1 Then
                AccntCode = ""
            Else
                AccntCode = GetAccntCode(cbCreditAccount.SelectedItem)
            End If
            Dim insertSQL, updateSQL, deleteSQL As String
            activityStatus = True
            updateSQL = " UPDATE tblAPV " &
                        " SET    APV_No = @APV_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " &
                        "        VCECode = @VCECode, DateAPV = @DateAPV, SI_Ref = @SI_Ref, AccntCode = @AccntCode, " &
                        "        Amount = @Amount, Discount = @Discount, InputVAT = @InputVAT, NetPurchase = @NetPurchase, Currency = @Currency, Exchange_Rate = @Exchange_Rate,  " &
                        "        Remarks = @Remarks, PO_Ref = @PO_Ref, RR_Ref = @RR_Ref, WhoModified = @WhoModified, DateModified = GETDATE(), CostCenter = @CostCenter " &
                        " WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@APV_No", APVNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateAPV", dtpDocDate.Value.Date)
            SQL.AddParam("@AccntCode", AccntCode)
            SQL.AddParam("@Amount", IIf(txtAmount.Text = "", 0, CDec(txtAmount.Text)))
            SQL.AddParam("@Discount", IIf(txtDiscount.Text = "", 0, CDec(txtDiscount.Text)))
            SQL.AddParam("@InputVAT", IIf(txtVAT.Text = "", 0, CDec(txtVAT.Text)))
            SQL.AddParam("@NetPurchase", IIf(txtNet.Text = "", 0, CDec(txtNet.Text)))
            SQL.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@PO_Ref", txtPORef.Text)
            SQL.AddParam("@RR_Ref", txtRRRef.Text)
            SQL.AddParam("@SI_Ref", txtSINo.Text)
            SQL.AddParam("@WhoModified", UserID)
            SQL.AddParam("@CostCenter", cbCostCenter.SelectedItem)
            SQL.ExecNonQuery(updateSQL)

            JETransiD = LoadJE("APV", TransID)

            If JETransiD = 0 Then

                JETransiD = GenerateTransID("JE_No", "tblJE_Header")

                insertSQL = " INSERT INTO " & _
                            " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Currency, Exchange_Rate, Remarks, WhoCreated) " & _
                            " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Currency, @Exchange_Rate, @Remarks, @WhoCreated)"
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransiD)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "APV")
                SQL.AddParam("@RefTransID", TransID)
                SQL.AddParam("@Book", "Accounts Payable")
                SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@WhoCreated", UserID)
                SQL.ExecNonQuery(insertSQL)

            Else
                updateSQL = " UPDATE tblJE_Header " &
                            " SET    AppDate = @AppDate, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " &
                            "        RefType = @RefType, RefTransID = @RefTransID, Book = @Book, TotalDBCR = @TotalDBCR, Currency = @Currency, Exchange_Rate = @Exchange_Rate, " &
                            "        Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE(), CostCenter = @CostCenter " &
                            " WHERE  JE_No = @JE_No "
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransiD)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "APV")
                SQL.AddParam("@RefTransID", TransID)
                SQL.AddParam("@Book", "Accounts Payable")
                SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@WhoModified", UserID)
                SQL.AddParam("@CostCenter", cbCostCenter.SelectedItem)
                SQL.ExecNonQuery(updateSQL)
            End If

            Dim line As Integer = 1

            ' DELETE ACCOUNTING ENTRIES
            deleteSQL = " DELETE FROM tblJE_Details  WHERE  JE_No = @JE_No "
            SQL.FlushParams()
            SQL.AddParam("@JE_No", JETransiD)
            SQL.ExecNonQuery(deleteSQL)

            Dim HeaderCC As String = cbCostCenter.SelectedItem
            ' INSERT NEW ENTRIES
            For Each item As DataGridViewRow In dgvEntry.Rows
                If item.Cells(dgcAccntCode.Index).Value <> Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, CostCenter, CIP_Code, VATType, ProfitCenter, ATC_Code) " & _
                                " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @CostCenter, @CIP_Code, @VATType, @ProfitCenter, @ATC_Code)"
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransiD)
                    SQL.AddParam("@AccntCode", item.Cells(dgcAccntCode.Index).Value.ToString)
                    If item.Cells(dgcVCECode.Index).Value <> Nothing AndAlso item.Cells(dgcVCECode.Index).Value <> "" Then
                        SQL.AddParam("@VCECode", item.Cells(dgcVCECode.Index).Value.ToString)
                    Else
                        SQL.AddParam("@VCECode", txtVCECode.Text)
                    End If
                    If item.Cells(dgcDebit.Index).Value <> Nothing AndAlso IsNumeric(item.Cells(dgcDebit.Index).Value) Then
                        SQL.AddParam("@Debit", CDec(item.Cells(dgcDebit.Index).Value))
                    Else
                        SQL.AddParam("@Debit", 0)
                    End If
                    If item.Cells(dgcCredit.Index).Value <> Nothing AndAlso IsNumeric(item.Cells(dgcCredit.Index).Value) Then
                        SQL.AddParam("@Credit", CDec(item.Cells(dgcCredit.Index).Value))
                    Else
                        SQL.AddParam("@Credit", 0)
                    End If
                    If item.Cells(dgcParticulars.Index).Value <> Nothing AndAlso item.Cells(dgcParticulars.Index).Value <> "" Then
                        SQL.AddParam("@Particulars", item.Cells(dgcParticulars.Index).Value.ToString)
                    Else
                        SQL.AddParam("@Particulars", txtRemarks.Text)
                    End If
                    If cbCostCenter.SelectedItem = "" Then
                        If item.Cells(dgcCC.Index).Value <> Nothing AndAlso item.Cells(dgcCC.Index).Value <> "" Then
                            SQL.AddParam("@CostCenter", item.Cells(dgcCC.Index).Value.ToString)
                        Else
                            SQL.AddParam("@CostCenter", "")
                        End If
                    Else
                        SQL.AddParam("@CostCenter", HeaderCC)
                    End If
                    If item.Cells(dgcCIP.Index).Value <> Nothing AndAlso item.Cells(dgcCIP.Index).Value <> "" Then
                            SQL.AddParam("@CIP_Code", item.Cells(dgcCIP.Index).Value.ToString)
                        Else
                            SQL.AddParam("@CIP_Code", "")
                        End If
                        If item.Cells(chProfit_Code.Index).Value <> Nothing AndAlso item.Cells(chProfit_Code.Index).Value <> "" Then
                            SQL.AddParam("@ProfitCenter", item.Cells(chProfit_Code.Index).Value.ToString)
                        Else
                            SQL.AddParam("@ProfitCenter", "")
                        End If
                        If item.Cells(dgcRefNo.Index).Value <> Nothing AndAlso item.Cells(dgcRefNo.Index).Value <> "" Then
                            SQL.AddParam("@RefNo", item.Cells(dgcRefNo.Index).Value.ToString)
                        Else
                            SQL.AddParam("@RefNo", "")
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
                        SQL.AddParam("@LineNumber", line)
                        SQL.ExecNonQuery(insertSQL)


                        Dim update As String
                        If item.Cells(chRecordID.Index).Value <> Nothing Then
                            update = " UPDATE tblPCV_Details Set Status = 'Closed' " &
                                 " WHERE RecordID = '" & item.Cells(chRecordID.Index).Value & "'"
                            SQL.ExecNonQuery(update)
                        End If

                        'If item.Cells(dgcRefNo.Index).Value <> Nothing Then
                        '    PCV = dgvEntry.Item(dgcRefNo.Index, line).Value.ToString.Replace("PCV:", "")
                        '    update = " UPDATE tblPCV SET Status ='Closed' WHERE PCV_No = '" & PCV & "'"
                        '    SQL.ExecNonQuery(update)
                        'End If

                        If item.Cells(dgcRefNo.Index).Value <> Nothing Then
                            If item.Cells(dgcRefNo.Index).Value.Contains("PCV:") Then
                                PCV = dgvEntry.Item(dgcRefNo.Index, line).Value.ToString.Replace("PCV:", "")
                                update = " UPDATE tblPCV SET Status ='Closed' WHERE PCV_No = '" & PCV & "'"
                                SQL.ExecNonQuery(update)
                                'ElseIf item.Cells(dgcRefNo.Index).Value.Contains("ATD:") Then
                                '    Dim ATD As String
                                '    ATD = dgvEntry.Item(dgcRefNo.Index, line).Value.ToString.Replace("ATD:", "")
                                '    update = " UPDATE tblATD SET Status ='Posted' WHERE ATD_No = '" & ATD & "'"
                                '    SQL.ExecNonQuery(update)
                            End If
                        End If
                        line += 1
                    End If
            Next
            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "APV_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub txtAmount_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtAmount.KeyDown, txtVAT.KeyDown, txtDiscount.KeyDown, txtNet.KeyDown
        Try
            If e.KeyValue <> 8 And Not (e.KeyValue >= 48 And e.KeyValue <= 57) Then
                e.SuppressKeyPress = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub dgvEntry_RowsRemoved(sender As System.Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgvEntry.RowsRemoved
        TotalDBCR()
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("APV_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("APV")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadAPV(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("APV_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            APVNo = ""


            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbUpload.Enabled = True
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
        If Not AllowAccess("APV_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)

            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbUpload.Enabled = False
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
        Try
            If DataValidated() Then
                If txtVCECode.Text = "" Then
                    Msg("Please enter VCECode!", MsgBoxStyle.Exclamation)
                ElseIf cbCreditAccount.SelectedIndex = -1 Then
                    Msg("Please select default Credit account first!", MsgBoxStyle.Exclamation)
                ElseIf txtTotalDebit.Text <> txtTotalCredit.Text Then
                    MsgBox("Please check the Debit and Credit Amount!", MsgBoxStyle.Exclamation)
                ElseIf txtConversion.Visible = True And txtConversion.Text = "" Then
                    MsgBox("Please check exchange rate!", MsgBoxStyle.Exclamation)
                ElseIf txtAmount.Text = "" Or txtNet.Text = "" Then
                    MsgBox("Please check amount!", MsgBoxStyle.Exclamation)
                ElseIf TransID = "" Then
                    If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                        TransID = GenerateTransID(ColumnID, DBTable)
                        If TransAuto Then
                            APVNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                        Else
                            APVNo = txtTransNum.Text
                        End If
                        txtTransNum.Text = APVNo
                        SaveAPV()
                        Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                        LoadAPV(TransID)
                    End If
                Else
                    If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then

                        If APVNo = txtTransNum.Text Then
                            APVNo = txtTransNum.Text
                            UpdateAPV()
                            Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                            APVNo = txtTransNum.Text
                            LoadAPV(TransID)
                        Else
                            If Not IfExist(txtTransNum.Text) Then
                                APVNo = txtTransNum.Text
                                UpdateAPV()
                                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                                APVNo = txtTransNum.Text
                                LoadAPV(TransID)
                            Else
                                MsgBox("APV" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
                            End If
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
        query = " SELECT * FROM tblAPV WHERE APV_No ='" & ID & "'  "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function DataValidated() As Boolean
        If validateDGV() = False Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function validateDGV() As Boolean
        Dim AccntCode, query, costcenter As String
        Dim value As Boolean = True
        
        Return value
    End Function

    Private Function isUsed(ByVal strTransNo As String) As Boolean
        Dim query As String = " SELECT * FROM tblAPV WHERE APV_No = '" & strTransNo & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("APV", TransID)
        f.Dispose()
    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("APV_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If txtStatus.Text <> "Cancelled" AndAlso MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim updateSQL As String
                        updateSQL = " UPDATE  tblAPV SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        SQL.AddParam("@TransID", TransID)
                        SQL.ExecNonQuery(updateSQL)

                        updateSQL = " UPDATE  tblJE_Header SET Status ='Cancelled' WHERE RefTransID = @RefTransID  AND RefType ='APV' "
                        SQL.FlushParams()
                        SQL.AddParam("@RefTransID", TransID)
                        SQL.ExecNonQuery(updateSQL)

                        If isReversalEntry = True Then
                            Dim insertSQL As String
                            insertSQL = " INSERT INTO tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, OtherRef) " & _
                                        " SELECT JE_No, AccntCode, VCECode, Credit, Debit, Particulars, RefNo, 0 AS LineNumber, OtherRef FROM tblJE_Details " & _
                                        " WHERE JE_No IN (SELECT DISTINCT JE_No FROM tblJE_Header WHERE RefType ='" & ModuleID & "' AND RefTransID ='" & TransID & "') "
                            SQL.ExecNonQuery(insertSQL)
                        End If

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

                        APVNo = txtTransNum.Text
                        LoadAPV(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "APV_No", APVNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                ElseIf txtStatus.Text = "Cancelled" AndAlso MsgBox("Are you sure you want to un-cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim updateSQL As String
                        updateSQL = " UPDATE  tblAPV SET Status = @Status WHERE TransID = @TransID "
                        SQL.FlushParams()
                        SQL.AddParam("@TransID", TransID)
                        If ForApproval Then
                            SQL.AddParam("@Status", "Draft")
                        Else
                            SQL.AddParam("@Status", "Active")
                        End If
                        SQL.ExecNonQuery(updateSQL)

                        updateSQL = " UPDATE  tblJE_Header SET Status = @Status WHERE RefTransID = @RefTransID  AND RefType ='APV' "
                        SQL.FlushParams()
                        SQL.AddParam("@RefTransID", TransID)
                        If ForApproval Then
                            SQL.AddParam("@Status", "Draft")
                        Else
                            SQL.AddParam("@Status", "Saved")
                        End If
                        SQL.ExecNonQuery(updateSQL)

                        If isReversalEntry = True Then
                            Dim insertSQL As String
                            insertSQL = " DELETE FROM tblJE_Details " & _
                                        " WHERE JE_No IN (SELECT DISTINCT JE_No FROM tblJE_Header WHERE RefType ='" & ModuleID & "' AND RefTransID ='" & TransID & "') AND LineNumber = 0 "
                            SQL.ExecNonQuery(insertSQL)
                        End If

                        Msg("Record un-cancel successfully", MsgBoxStyle.Information)

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

                        APVNo = txtTransNum.Text
                        LoadAPV(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "UN-CANCEL", "APV_No", APVNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If APVNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblAPV  WHERE APV_No < '" & APVNo & "' ORDER BY APV_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadAPV(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If APVNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblAPV  WHERE APV_No > '" & APVNo & "' ORDER BY APV_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadAPV(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If TransID = "" Then
            ClearText()
            EnableControl(False)
            RR_RefID = 0
            RefID = 0
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadAPV(TransID)
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

    Private Sub btnSearchVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchVCE.Click
        Dim f As New frmVCE_Search
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        txtVCEName.Text = f.VCEName
        LoadCurrency()

    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
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
            ElseIf e.KeyCode = Keys.C Then
                If tsbCopy.Enabled = True Then tsbCopy.ShowDropDown()
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

    Private Sub tsbCopyPO_Click(sender As System.Object, e As System.EventArgs) Handles tsbCopyPO.Click
        If cbCreditAccount.SelectedIndex = -1 Then
            Msg("Please select default Credit account first!", MsgBoxStyle.Exclamation)
        Else
            Dim f As New frmLoadTransactions
            f.cbFilter.SelectedItem = "Status"
            f.txtFilter.Text = "Closed"
            f.txtFilter.Enabled = False
            f.cbFilter.Enabled = False
            f.btnSearch.Enabled = False
            f.ShowDialog("PO")
            LoadPO(f.transID)
            f.Dispose()
        End If
    End Sub

    Private Sub TestToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles TestToolStripMenuItem1.Click
        Dim f As New frmReport_Filter
        f.Report = "APV List"
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub cbCreditAccount_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbCreditAccount.KeyPress
        e.Handled = True
    End Sub


    Private Sub dgvEntry_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntry.CellContentClick
        If e.ColumnIndex = chCompute.Index Then
            Dim f As New frmTaxComputation
            Dim VATamount As Decimal
            Dim VCECode As String
            If dgvEntry.Item(dgcVCECode.Index, e.RowIndex).Value = "" Then
                VCECode = txtVCECode.Text
            Else
                VCECode = dgvEntry.Item(dgcVCECode.Index, e.RowIndex).Value
            End If
            VATamount = CDec(IIf(IIf(IsNothing(dgvEntry.Item(dgcDebit.Index, e.RowIndex).Value), "0.00", dgvEntry.Item(dgcDebit.Index, e.RowIndex).Value).ToString = "0.00", IIf(IsNothing(dgvEntry.Item(dgcCredit.Index, e.RowIndex).Value), "0.00", dgvEntry.Item(dgcCredit.Index, e.RowIndex).Value).ToString, IIf(IsNothing(dgvEntry.Item(dgcDebit.Index, e.RowIndex).Value), "0.00", dgvEntry.Item(dgcDebit.Index, e.RowIndex).Value).ToString)).ToString("N2")
            f.ShowDialog(VATamount, "", "", VCECode)
            If IsNothing(dgvEntry.Item(dgcDebit.Index, e.RowIndex).Value) Then
                dgvEntry.Item(dgcCredit.Index, e.RowIndex).Value = CDec(f.GrossAmount).ToString("N2")
                dgvEntry.Item(dgcDebit.Index, e.RowIndex).Value = ""
                dgvEntry.Item(chVATType.Index, e.RowIndex).Value = f.VATType
                dgvEntry.Item(dgcVCECode.Index, e.RowIndex).Value = f.VCECode
                dgvEntry.Item(dgcVCEName.Index, e.RowIndex).Value = GetVCEName(f.VCECode)
            Else
                dgvEntry.Item(dgcDebit.Index, e.RowIndex).Value = CDec(f.GrossAmount).ToString("N2")
                dgvEntry.Item(dgcCredit.Index, e.RowIndex).Value = ""
                dgvEntry.Item(chVATType.Index, e.RowIndex).Value = f.VATType
                dgvEntry.Item(dgcVCECode.Index, e.RowIndex).Value = f.VCECode
                dgvEntry.Item(dgcVCEName.Index, e.RowIndex).Value = GetVCEName(f.VCECode)

            End If
            If f.VATAmount <> 0 Then
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = f.TAX_IV
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(f.TAX_IV)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = CDec(f.VATAmount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcRefNo.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = "Input VAT"
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = f.VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = GetVCEName(f.VCECode)

            End If
            If f.EWTAmount <> 0 Then
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = f.TAX_EWT
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(f.TAX_EWT)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = CDec(f.EWTAmount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcRefNo.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = "EWT"
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chATCCode.Index).Value = f.ATCCode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = f.VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = GetVCEName(f.VCECode)
            End If
            If cbCreditAccount.SelectedIndex = -1 Then
                AccntCode = ""
            Else
                AccntCode = GetAccntCode(cbCreditAccount.SelectedItem)
            End If
            dgvEntry.Rows.Add("")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = AccntCode
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(AccntCode)
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = CDec(VATamount - f.EWTAmount).ToString("N2")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcRefNo.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = f.VCECode
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = GetVCEName(f.VCECode)

            TotalDBCR()
        End If
    End Sub

    Private Sub dgvEntry_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dgvEntry.CurrentCellDirtyStateChanged

    End Sub

    Dim eColIndex As Integer = 0
    Private Sub dgvEntry_CellBeginEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvEntry.CellBeginEdit
        eColIndex = e.ColumnIndex
    End Sub

    'Start of Cost Center insert to Table
    Dim strDefaultGridView As String = ""
    Dim strDefaultGridCode As String = ""
    Public Function LoadCostCenterGridView()

        Dim selectSQL As String = " SELECT Code, Description FROM tblCC WHERE Status <> 'Inactive' "
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
        Return cbvGridviewCell

    End Function

    Public Function LoadCIPGridView()

        Dim selectSQL As String = " SELECT CIP_Code, CIP_Desc FROM tblCIP_Maintenance WHERE Status <> 'Inactive' "
        SQL.ReadQuery(selectSQL, 2)

        Dim cbvGridviewCell As New DataGridViewComboBoxCell

        Dim count As Integer = 1
        cbvGridviewCell.Items.Add("")
        While SQL.SQLDR2.Read
            If count = 1 Then
                strDefaultGridCode = SQL.SQLDR2("CIP_Code").ToString
                strDefaultGridView = SQL.SQLDR2("CIP_Desc").ToString
            End If
            cbvGridviewCell.Items.Add(SQL.SQLDR2("CIP_Desc").ToString)
            count += 1
        End While
        Return cbvGridviewCell

    End Function

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

    Public Sub LoadCIPCode(ByVal CIP As String, ByVal RowIndex As Integer, ByVal CodeIndex As Integer, ByVal CIPIndex As Integer)

        Dim selectSQL As String
        selectSQL = " SELECT CIP_Code, CIP_Desc FROM tblCIP_Maintenance WHERE CIP_Desc = '" & CIP & "' AND Status <> 'Inactive'"
        SQL.ReadQuery(selectSQL, 2)

        strDefaultGridView = ""
        strDefaultGridCode = ""

        While SQL.SQLDR2.Read
            strDefaultGridView = SQL.SQLDR2("CIP_Desc").ToString
            strDefaultGridCode = SQL.SQLDR2("CIP_Code").ToString
        End While
        dgvEntry.Rows(RowIndex).Cells(CIPIndex).Value = strDefaultGridView
        dgvEntry.Rows(RowIndex).Cells(CodeIndex).Value = strDefaultGridCode

    End Sub

    Public Sub LoadProfitCenterCode(ByVal ProfitCenter As String, ByVal RowIndex As Integer, ByVal CodeIndex As Integer, ByVal CostIndex As Integer)

        Dim selectSQL As String
        selectSQL = " SELECT Code, Description FROM tblPC WHERE Description = '" & ProfitCenter & "' AND Status <> 'Inactive'"
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

    Public Function GetCIPName(ByVal CCCode As String) As String
        Dim query As String
        query = " SELECT CIP_Desc FROM tblCIP_Maintenance WHERE CIP_Code ='" & CCCode & "' AND Status <> 'Inactive'"
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("CIP_Desc").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetCCName(ByVal CCCode As String) As String
        Dim query As String
        query = " SELECT Description FROM tblCC WHERE Code ='" & CCCode & "' AND Status <> 'Inactive'"
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("Description").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetPCName(ByVal PCCode As String) As String
        Dim query As String
        query = " SELECT Description FROM tblPC WHERE Code ='" & PCCode & "' AND Status <> 'Inactive'"
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("Description").ToString
        Else
            Return ""
        End If
    End Function

    Private Sub FromPCVToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromPCVToolStripMenuItem.Click
        If cbCreditAccount.SelectedIndex = -1 Then
            Msg("Please select default Credit account first!", MsgBoxStyle.Exclamation)
        Else
            Dim f As New frmLoadTransactions
            f.cbFilter.SelectedItem = "Status"
            f.txtFilter.Text = "Active"
            f.txtFilter.Enabled = False
            f.cbFilter.Enabled = False
            f.btnSearch.Enabled = False
            f.ShowDialog("PCV")
            f.Dispose()
        End If
    End Sub

    Public Sub LoadPCV(ByVal PCVID As String)
        accntCR = GetAccntCode(cbCreditAccount.SelectedItem)
        Try
            Dim query As String
            query = " SELECT TransID, PCV_No, BranchCode, tblPCV.VCECode, VCEName " & _
                    " FROM tblPCV LEFT JOIN tblVCE_Master " & _
                    " ON tblPCV.VCECode = tblVCE_Master.VCECode " & _
                    " WHERE TransID ='" & PCVID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                txtVCEName.Text = SQL.SQLDR("VCEName").ToString
                PCV = SQL.SQLDR("PCV_No")

                query = " SELECT tblPCV.TransID, tblPCV.BranchCode, AccountCode, AccountTitle, CodePayee, RecordPayee, tblPCV_Details.Type, tblPCV_Details.BaseAmount, tblPCV_Details.InputVAT, CostCenter, tblPCV_Details.Status, tblPCV_Details.RecordID, CIP_Code " & _
                          " FROM tblPCV INNER JOIN tblPCV_Details " & _
                          " ON tblPCV.TransID = tblPCV_Details.TransID " & _
                          " LEFT JOIN tblRFP_Type " & _
                          " ON tblPCV_Details.Type = tblRFP_Type.Type " & _
                          " LEFT JOIN tblCOA_Master " & _
                          " ON tblCOA_Master.AccountCode = tblRFP_Type.DefaultAccount " & _
                          " WHERE tblPCV.TransID = '" & PCVID & "' AND tblPCV_Details.Status ='Active' "
                SQL.ReadQuery(query)

                Dim rowsCount As Integer = dgvEntry.Rows.Count - 1
                While SQL.SQLDR.Read


                    dgvEntry.Rows.Add("")
                    dgvEntry.Rows(rowsCount).Cells(dgcAccntCode.Index).Value = SQL.SQLDR("AccountCode").ToString
                    dgvEntry.Rows(rowsCount).Cells(dgcAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                    dgvEntry.Rows(rowsCount).Cells(dgcDebit.Index).Value = CDec(SQL.SQLDR("BaseAmount")).ToString("N2")
                    dgvEntry.Rows(rowsCount).Cells(dgcCredit.Index).Value = "0.00"
                    dgvEntry.Rows(rowsCount).Cells(dgcVCECode.Index).Value = SQL.SQLDR("CodePayee").ToString
                    dgvEntry.Rows(rowsCount).Cells(dgcVCEName.Index).Value = SQL.SQLDR("RecordPayee").ToString
                    dgvEntry.Rows(rowsCount).Cells(dgcParticulars.Index).Value = SQL.SQLDR("Type").ToString
                    dgvEntry.Rows(rowsCount).Cells(dgcRefNo.Index).Value = "PCV:" & PCV
                    dgvEntry.Rows(rowsCount).Cells(dgcCC.Index).Value = SQL.SQLDR("CostCenter").ToString
                    dgvEntry.Rows(rowsCount).Cells(chCost_Center.Index).Value = GetCCName(SQL.SQLDR("CostCenter").ToString)
                    dgvEntry.Rows(rowsCount).Cells(dgcCIP.Index).Value = SQL.SQLDR("CIP_Code").ToString
                    dgvEntry.Rows(rowsCount).Cells(chCIP_Description.Index).Value = GetCIPName(SQL.SQLDR("CIP_Code").ToString)




                    dgvEntry.Rows(rowsCount).Cells(chRecordID.Index).Value = SQL.SQLDR("RecordID").ToString
                    txtAmount.Text = CDec(txtAmount.Text) + IIf(CDec(SQL.SQLDR("BaseAmount")) = 0, 0, CDec(SQL.SQLDR("BaseAmount")))

                    If SQL.SQLDR("InputVAT") <> 0 Then
                        rowsCount += 1
                        dgvEntry.Rows.Add("")
                        dgvEntry.Rows(rowsCount).Cells(dgcAccntCode.Index).Value = accntInputVAT
                        dgvEntry.Rows(rowsCount).Cells(dgcDebit.Index).Value = CDec(SQL.SQLDR("InputVAT")).ToString("N2")
                        dgvEntry.Rows(rowsCount).Cells(dgcCredit.Index).Value = "0.00"
                        dgvEntry.Rows(rowsCount).Cells(dgcVCECode.Index).Value = SQL.SQLDR("CodePayee").ToString
                        dgvEntry.Rows(rowsCount).Cells(dgcVCEName.Index).Value = SQL.SQLDR("RecordPayee").ToString
                        dgvEntry.Rows(rowsCount).Cells(dgcParticulars.Index).Value = SQL.SQLDR("Type").ToString
                        dgvEntry.Rows(rowsCount).Cells(dgcRefNo.Index).Value = "PCV:" & PCV
                        dgvEntry.Rows(rowsCount).Cells(dgcCC.Index).Value = SQL.SQLDR("CostCenter").ToString
                        dgvEntry.Rows(rowsCount).Cells(chCost_Center.Index).Value = GetCCName(SQL.SQLDR("CostCenter").ToString)
                        dgvEntry.Rows(rowsCount).Cells(dgcCIP.Index).Value = SQL.SQLDR("CIP_Code").ToString
                        dgvEntry.Rows(rowsCount).Cells(chCIP_Description.Index).Value = GetCIPName(SQL.SQLDR("CIP_Code").ToString)

                        dgvEntry.Rows(rowsCount).Cells(chRecordID.Index).Value = SQL.SQLDR("RecordID").ToString
                        txtVAT.Text = CDec(txtVAT.Text) + IIf(CDec(SQL.SQLDR("InputVAT")) = 0, 0, CDec(SQL.SQLDR("InputVAT")))
                        dgvEntry.Rows(rowsCount).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(accntInputVAT)
                    End If

                    rowsCount += 1
                End While
            End If

            TotalDBCR()

            If CDec(txtTotalDebit.Text) <> 0 Then
                Dim CreditAmnt As Decimal = 0.0
                CreditAmnt = CDec(txtTotalDebit.Text).ToString("N2")

                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcAccntCode.Index).Value = accntCR
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(accntCR)
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcDebit.Index).Value = "0.00"
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcCredit.Index).Value = CreditAmnt
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcVCECode.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcVCEName.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcParticulars.Index).Value = txtRemarks.Text
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcRefNo.Index).Value = ""

                TotalDBCR()
            End If

            txtNet.Text = CDec(txtAmount.Text - txtDiscount.Text) + CDec(txtVAT.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
        query = "  SELECT    tblRR.TransID, tblRR.RR_No, DateRR AS Date, tblRR.VCECode, tblVCE_Master.VCEName,  " & _
                " ISNULL(ADV_Amount,0) AS Advances,  Credit AS NetPurchase,  " & _
                " tblRR.Remarks,  ADV.AccntCode  AS ADVAccount, View_GL.AccntCode as APPendingAccount  " & _
                " FROM tblRR " & _
                " INNER JOIN tblVCE_Master  ON  " & _
                " tblRR.VCECode = tblVCE_Master.VCECode " & _
                " INNER JOIN View_GL  ON " & _
                " tblRR.TransID = View_GL.RefTransID " & _
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
            txtDiscount.Text = "0.00"
            txtVAT.Text = "0.00"
            txtNet.Text = CDec(SQL.SQLDR("NetPurchase")).ToString("N2")
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            accntDR = SQL.SQLDR("APPendingAccount").ToString
            Adv_Amount = SQL.SQLDR("Advances").ToString
            accntAdvance = SQL.SQLDR("ADVAccount").ToString
            LoadCurrency()
            GenerateEntryRR(ID)
        End If
    End Sub

    Private Sub LoadPayroll_RUBY(ByVal Period As String, GroupName As String)
        SetPayrollDatabase()
        dgvEntry.Rows.Clear()
        Dim query As String
        query = "spPayroll_Entries"
        SQL_RUBY.FlushParams()
        SQL_RUBY.AddParam("@PayrollPeriod", Period)
        SQL_RUBY.AddParam("@GroupName", GroupName)
        SQL_RUBY.ReadQuery(query)
        Dim amount As Decimal = 0
        While SQL_RUBY.SQLDR.Read
            txtVCECode.Text = SQL_RUBY.SQLDR("VCECode")
            Dim VCE As String = ""
            txtDiscount.Text = "0.00"
            txtVAT.Text = "0.00"
            Payroll_OrgID = SQL_RUBY.SQLDR("OrgCost_ID")
            txtRemarks.Text = SQL_RUBY.SQLDR("GroupName").ToString & " " & CDate(SQL_RUBY.SQLDR("Paydate")).ToString("MMdd")
            If cbCreditAccount.Items.Contains(GetAccntTitle(SQL_RUBY.SQLDR("Account_Code").ToString)) Then
                disableEvent = True
                cbCreditAccount.SelectedItem = GetAccntTitle(SQL_RUBY.SQLDR("Account_Code").ToString)
                disableEvent = False
            End If
            dgvEntry.Rows.Add("")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = SQL_RUBY.SQLDR("Account_Code").ToString
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(SQL_RUBY.SQLDR("Account_Code").ToString)
            If IsNumeric(SQL_RUBY.SQLDR("Debit")) AndAlso CDec(SQL_RUBY.SQLDR("Debit")) > 0 Then
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = CDec(SQL_RUBY.SQLDR("Debit")).ToString("N2")
                If Not IsNothing(cbCreditAccount.SelectedItem) AndAlso GetAccntCode(cbCreditAccount.SelectedItem) = SQL_RUBY.SQLDR("Account_Code").ToString Then
                    amount -= CDec(SQL_RUBY.SQLDR("Debit"))
                End If
            Else
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = ""
            End If
            If IsNumeric(SQL_RUBY.SQLDR("Credit")) AndAlso CDec(SQL_RUBY.SQLDR("Credit")) > 0 Then
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = CDec(SQL_RUBY.SQLDR("Credit")).ToString("N2")
                If Not IsNothing(cbCreditAccount.SelectedItem) AndAlso GetAccntCode(cbCreditAccount.SelectedItem) = SQL_RUBY.SQLDR("Account_Code").ToString Then
                    amount += CDec(SQL_RUBY.SQLDR("Credit"))
                End If
            Else
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = ""
            End If
            If SQL_RUBY.SQLDR("EmpID").ToString = "" Then
                VCE = SQL_RUBY.SQLDR("VCECode").ToString
            Else
                VCE = SQL_RUBY.SQLDR("EmpID").ToString
            End If
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = VCE
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = GetVCEName(VCE)
            If SQL_RUBY.SQLDR("Ref") <> "" Then
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcParticulars.Index).Value = SQL_RUBY.SQLDR("Ref")
            Else
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcParticulars.Index).Value = txtRemarks.Text
            End If
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = ""
        End While
        txtVCEName.Text = GetVCEName(txtVCECode.Text)
        txtAmount.Text = amount.ToString("N2")
        txtNet.Text = amount.ToString("N2")
        TotalDBCR()
        LoadCurrency()

    End Sub
    Private Sub GenerateEntryRR(ByVal PO_ID As Integer)
        Dim query As String
        Dim amount As Decimal = 0
        If pendingAPsetup = False Then
            Dim baseAmt As Decimal = 0
            query = " SELECT BaseAmount, AD_Inv FROM viewAPV_InvEntry WHERE TransID = '" & PO_ID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                accntDR = SQL.SQLDR("AD_Inv").ToString
                baseAmt = SQL.SQLDR("BaseAmount").ToString
            End If
            accntCR = GetAccntCode(cbCreditAccount.SelectedItem)
            dgvEntry.Rows.Clear()
            dgvEntry.Rows.Add("")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = accntDR
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(accntDR)
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = CDec(txtNet.Text - txtVAT.Text).ToString("N2")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcRefNo.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = ""


            If CDec(txtVAT.Text) <> 0 Then
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = accntInputVAT
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(accntInputVAT)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = CDec(txtVAT.Text).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcRefNo.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = ""

            End If

            If Adv_Amount <> 0 Then
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = accntAdvance
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(accntAdvance)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = CDec(Adv_Amount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcRefNo.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = ""
            End If

            If CDec(txtNet.Text) - Adv_Amount <> 0 Then
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = accntCR
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(accntCR)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = CDec(txtNet.Text - Adv_Amount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcRefNo.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = ""
            End If

            TotalDBCR()
        Else
            'accntCR = GetAccntCode(cbCreditAccount.SelectedItem)
            query = " SELECT RefNo, SUM(Credit-Debit) AS Debit, VCECode, VCEName  " & _
                    " FROM View_GL WHERE RefType IN ('RR','PJ') AND RefTransID IN (SELECT TransID FROM tblRR WHERE TransID = '" & PO_ID & "') AND AccntCode ='" & accntAPpending & "' " & _
                    " GROUP BY RefNo, VCECode, VCEName  "
            SQL.GetQuery(query)
            dgvEntry.Rows.Clear()
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                    dgvEntry.Rows.Add("")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = accntAPpending
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(accntAPpending)
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = CDec(row.Item(1)).ToString("N2")
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = ""
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = txtVCECode.Text
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = txtVCEName.Text
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcRefNo.Index).Value = row.Item(0).ToString
                    dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = ""

                    amount += row.Item(1)
                Next
                'dgvEntry.Rows.Add("")
                'dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = accntCR
                'dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(accntCR)
                'dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = ""
                'dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = CDec(amount).ToString("N2")
                'dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = txtVCECode.Text
                'dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = txtVCEName.Text
                'dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcRefNo.Index).Value = "APV:" & txtTransNum.Text
                'dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = ""

                ''Auto Entry Grid View Cost Center
                'If IsNothing(dgvEntry.Item(chCostCenter.Index, dgvEntry.Rows.Count - 2).Value) Then
                '    Dim cbvCostCenter As DataGridViewComboBoxCell = LoadCostCenterGridView()
                '    cbvCostCenter.Value = strDefaultGridView
                '    dgvEntry.Item(chCostCenter.Index, dgvEntry.Rows.Count - 2) = cbvCostCenter

                '    Dim dgvCostCenter As String
                '    dgvCostCenter = dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCostCenter.Index).Value
                '    LoadCostCenterCode(dgvCostCenter, dgvEntry.Rows.Count - 2, chCostCenter.Index, dgcCC.Index)
                'End If

                ''Auto Entry Grid View Cost CIP
                'If IsNothing(dgvEntry.Item(chCIP_Desc.Index, dgvEntry.Rows.Count - 2).Value) Then
                '    Dim cbvCIP As DataGridViewComboBoxCell = LoadCIPGridView()
                '    cbvCIP.Value = strDefaultGridView
                '    dgvEntry.Item(chCIP_Desc.Index, dgvEntry.Rows.Count - 2) = cbvCIP

                '    Dim dgvCIP As String
                '    dgvCIP = dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCIP_Desc.Index).Value
                '    LoadCIPCode(dgvCIP, dgvEntry.Rows.Count - 2, chCIP_Desc.Index, dgcCIP.Index)
                'End If

                ''Auto Entry Grid View Profit Center
                'If IsNothing(dgvEntry.Item(chProfit_Desc.Index, dgvEntry.Rows.Count - 2).Value) Then
                '    Dim cbvPC As DataGridViewComboBoxCell = LoadProfitCenterGridView()
                '    cbvPC.Value = strDefaultGridView
                '    dgvEntry.Item(chProfit_Desc.Index, dgvEntry.Rows.Count - 2) = cbvPC

                '    Dim dgvPC As String
                '    dgvPC = dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chProfit_Desc.Index).Value
                '    LoadProfitCenterCode(dgvPC, dgvEntry.Rows.Count - 2, chProfit_Desc.Index, chProfit_Code.Index)
                'End If
            End If
        End If

    End Sub

    Private Sub cbCreditAccount_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbCreditAccount.SelectedIndexChanged
        If disableEvent = False Then
            If cbCreditAccount.SelectedIndex <> -1 Then

                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = GetAccntCode(cbCreditAccount.SelectedItem)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = cbCreditAccount.SelectedItem
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = CDec(CDec(txtTotalDebit.Text) - CDec(txtTotalCredit.Text)).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcRefNo.Index).Value = "APV:" & txtTransNum.Text
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = ""
                TotalDBCR()
            End If
        End If
    End Sub

    Private Sub PrintCVToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PrintCVToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("APV", TransID, "APV Printout")
        f.Dispose()
    End Sub

    Private Sub BIR2307ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BIR2307ToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("BIR_2307", TransID, "APV")
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

    Private Sub tsbUpload_Click(sender As System.Object, e As System.EventArgs) Handles tsbUpload.Click
        If tsbUpload.Text = "Upload" Then
            With (OpenFileDialog1)
                .InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                .Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx"
                .FilterIndex = 2
            End With
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                If MessageBox.Show("Uploading APV" & vbNewLine & "Are you sure you want to Contiue?", "Message Alert", MessageBoxButtons.YesNo) = MsgBoxResult.Yes Then
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

    Private Sub tsbDownload_Click(sender As System.Object, e As System.EventArgs) Handles tsbDownload.Click
        Dim fileName As String = "APV Uploader.xlsx"
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
            For i As Integer = 0 To 16
                If i = 0 Then
                    xlWorkSheet.Cells(1, i + 1) = templateName
                End If
                xlWorkSheet.Cells(2, i + 1) = dgvEntry.Columns(i).Name
                xlWorkSheet.Cells(3, i + 1) = dgvEntry.Columns(i).HeaderText
            Next
            xlWorkSheet.Protect(excelPW)
            Dim ctr As Integer = 1
            Do
                fileName = "APV Uploader -" & ctr.ToString & ".xlsx"
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

                        If j = dgcAccntCode.Index Then
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
                        ElseIf j = dgcAccntTitle.Index Then
                            ' Check if has valid Account Title
                            If dgvEntry.Item(j - 1, rowCount - 1).Value = "" Then
                                ' if not check VCEref on excel template
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)

                            End If
                        ElseIf j = dgcDebit.Index Then
                            ' Debit
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = dgcCredit.Index Then
                            ' Credit
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If

                        ElseIf j = dgcVCECode.Index Then
                            ' Check if has valid VCEcode
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                ' if not check VCEref on excel template
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = dgcVCEName.Index Then
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
                        ElseIf j = dgcParticulars.Index Then
                            ' Particulars
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = dgcRefNo.Index Then
                            ' Ref No
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                ' if not check VCEref on excel template
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = dgcCC.Index Then
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
                                    ChangeCellColor(rowCount - 1, j)
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
                                    ChangeCellColor(rowCount - 1, j)
                                    Valid = False
                                Else
                                    ' if existing  Profit Center
                                    dgvEntry.Item(j, rowCount - 1).Value = GetPCName(ObjectText)
                                End If
                            End If
                        ElseIf j = dgcCIP.Index Then
                            ' Check if has valid CIP ID
                            If IsNothing(dgvEntry.Item(j, rowCount - 1).Value) OrElse dgvEntry.Item(j, rowCount - 1).Value = "" Then
                                ' if not check VCEref on excel template
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chCIP_Description.Index Then
                            ' Check if has valid CIP ID
                            If ObjectText <> "" Then
                                If Not validateCIP_Code(ObjectText) Then
                                    ' if not exist, change color.
                                    ChangeCellColor(rowCount - 1, j - 1)
                                    ChangeCellColor(rowCount - 1, j)
                                    Valid = False
                                Else
                                    ' if existing  CIP Desc
                                    dgvEntry.Item(j, rowCount - 1).Value = GetCIPName(ObjectText)
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


    Private Sub FromPayrollRubyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromPayrollRubyToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("PAYROLL-APV")
        PayPeriod = f.transID
        LoadPayroll_RUBY(PayPeriod, f.itemCode)
        f.Dispose()

    End Sub

    Private Sub cbCostCenter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCostCenter.SelectedIndexChanged
        If cbCostCenter.SelectedItem = "" Then
            For Each item As DataGridViewRow In dgvEntry.Rows
                item.Cells(dgcCC.Index).Value = String.Empty
                item.Cells(chCost_Center.Index).Value = String.Empty
            Next
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
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = SQL.SQLDR("Account_Code").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = CDec(SQL.SQLDR("Total_Amount")).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcRefNo.Index).Value = "ATD:" & SQL.SQLDR("ATD_No")
            End If
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    
    Private Sub FromPJToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromPJToolStripMenuItem.Click
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
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = DOV_Account
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(DOV_Account)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = CDec(VatAmount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = VCEName
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcParticulars.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcRefNo.Index).Value = Reference


                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = OV_Account
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(OV_Account)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = CDec(VatAmount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = VCEName
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcParticulars.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcRefNo.Index).Value = Reference

                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = AP_Account
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(AP_Account)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = CDec(Balance).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = VCEName
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcParticulars.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcRefNo.Index).Value = Reference


            Else
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = AP_Account
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(AP_Account)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = CDec(Balance).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCECode.Index).Value = VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcVCEName.Index).Value = VCEName
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcParticulars.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcRefNo.Index).Value = Reference

            End If
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FromAdvancesToSupplierToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromAdvancesToSupplierToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("ADV")
        LoadADV(f.transID)
        f.Dispose()
    End Sub

    Private Sub LoadADV(ByVal ADV As String)
        Try
            Dim query As String
            query = " SELECT TransID, ADV_No,  tblADV.VCECode, VCEName, DateADV AS DateADV, ADV_Amount AS Net_Purchase, Remarks,  AccntCode, AccountTitle " &
                    " FROM   tblADV INNER JOIN tblVCE_Master " &
                    " ON     tblADV.VCECode = tblVCE_Master.VCECode " &
                    " INNER JOIN tblCOA_Master " &
                    " ON     tblADV.AccntCode = tblCOA_Master.AccountCode " &
                    " WHERE  TransID ='" & ADV & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                txtVCEName.Text = SQL.SQLDR("VCEName").ToString

                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = SQL.SQLDR("AccntCode").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcDebit.Index).Value = CDec(SQL.SQLDR("Net_Purchase")).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(dgcRefNo.Index).Value = "ADV:" & SQL.SQLDR("ADV_No").ToString



            End If
            LoadCurrency()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
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
End Class