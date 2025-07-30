Public Class frmRE
    Dim TransID, JETransID As String
    Dim RENo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "RE"
    Dim ColumnPK As String = "RE_No"
    Dim DBTable As String = "tblRE"
    Dim ColumnID As String = "TransID"
    Dim TransAuto As Boolean
    Dim tpHidden As New Dictionary(Of String, System.Windows.Forms.TabPage)
    Dim tpOrder As New List(Of String)
    Dim tpHidden2 As New Dictionary(Of String, System.Windows.Forms.TabPage)
    Dim tpOrder2 As New List(Of String)
    Public RE_Sales, RE_NetOfVat, RE_OutputVat, RE_MiscFee, RE_ARMiscfee, RE_AR As String
    Dim ForApproval As Boolean = False


    Public Overloads Function ShowDialog(ByVal docnumber As String) As Boolean
        TransID = docnumber
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmRE_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            If cbPaymentType.Items.Count > 0 Then
                cbPaymentType.SelectedIndex = 0
            End If

            If TransID <> "" Then
                LoadRE(TransID)
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
            btnViewLedger.Enabled = False
            EnableControl(False)
            LoadSetup()
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub btnLPM_Click(sender As Object, e As EventArgs) Handles btnLPM.Click
        frmProperty_Master.ShowDialog(txtPropCode.Text)
    End Sub


    Private Sub EnableControl(ByVal Value As Boolean)
        txtPropName.Enabled = Value
        txtVCEName.Enabled = Value
        dtpStart.Enabled = Value
        txtReserve.Enabled = Value
        txtTotalDP.Enabled = Value
        'txtDiscount.Enabled = Value
        cbPaymentType.Enabled = Value
        btnSearchVCE.Enabled = Value
        btnSearchProp.Enabled = Value
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        txtSalesName.Enabled = Value
        nupCommission.Enabled = Value
        Button2.Enabled = Value
        Button1.Enabled = Value

        If cbPaymentType.SelectedItem = "Cash Sales" Then
            tpCash.Enabled = Value

        ElseIf cbPaymentType.SelectedItem = "Deferred Cash" Then
            tpDeferred.Enabled = Value

        ElseIf cbPaymentType.SelectedItem = "Financing" Then
            tpFinancing.Enabled = Value

        End If
        tpAccountingEntries.Enabled = True
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub
    Private Sub LoadSetup()
        Dim query As String
        query = " SELECT  RE_Sales, RE_NetOfVat, RE_OutputVat, RE_MiscFee, RE_ARMiscfee, RE_AR  FROM tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            RE_Sales = SQL.SQLDR("RE_Sales").ToString
            RE_NetOfVat = SQL.SQLDR("RE_NetOfVat").ToString
            RE_OutputVat = SQL.SQLDR("RE_OutputVat").ToString
            RE_MiscFee = SQL.SQLDR("RE_MiscFee").ToString
            RE_ARMiscfee = SQL.SQLDR("RE_ARMiscfee").ToString
            RE_AR = SQL.SQLDR("RE_AR").ToString
        End If
    End Sub

    Private Sub GenerateEntry()
        Dim dataEntry As New DataTable
        dgvEntry.Rows.Clear()


        dgvEntry.Rows.Add({RE_AR, GetAccntTitle(RE_AR), CDec(txtTCP.Text).ToString("N2"), "0.00", "", "", "", "RE:" & txtTransNum.Text})
        If txtMisc.Text <> "0.00" Then
            'dgvEntry.Rows.Add({RE_ARMiscfee, GetAccntTitle(RE_ARMiscfee), CDec(txtMisc.Text).ToString("N2"), "0.00", "", "", "", "RE:" & txtTransNum.Text})
            dgvEntry.Rows.Add({RE_MiscFee, GetAccntTitle(RE_MiscFee), "0.00", CDec(txtMisc.Text).ToString("N2"), "", "", "", ""})
        End If
        If txtSelling.Text <> "0.00" And txtVAT.Text <> "0.00" Then
            dgvEntry.Rows.Add({RE_NetOfVat, GetAccntTitle(RE_NetOfVat), "0.00", CDec(txtSelling.Text).ToString("N2"), "", "", "", ""})
        ElseIf txtSelling.Text <> "0.00" And txtVAT.Text = "0.00" Then
            dgvEntry.Rows.Add({RE_Sales, GetAccntTitle(RE_Sales), "0.00", CDec(txtSelling.Text).ToString("N2"), "", "", "", ""})
        End If
        If txtVAT.Text <> "0.00" Then
            dgvEntry.Rows.Add({RE_OutputVat, GetAccntTitle(RE_OutputVat), "0.00", CDec(txtVAT.Text).ToString("N2"), "", "", "", ""})
        End If


    End Sub

    Private Sub LoadRE(ByVal ID As String)
        Dim query As String
        query = " SELECT   TransID, RE_No, VCECode, PropCode, DateRE, SellingAmount, MiscAmount, AddlAmount, VATAmount, TCPAmount, PaymentType,  " &
                "          SpotTCP, TCP_CashWithin, TCP_Reservation, TCP_DiscountRate, TCP_DiscountAmount, TCP_Balance, TCP_PayableUntil, " &
                "          DC_Reservation, DC_Terms, DC_Monthly, DC_Start, DC_End, DP_Rate, FinancingMode, " &
                "          ReservationAmount, DP_Rate, TotalDP,  SpotDP, DP_WithinDays, Termed, DiscountRate, Discount, DP_Balance, DP_PayableUntil, " &
                "          MonthlyPayment, Balance, VATable, VATInc,  Terms, EquityStart, " &
                "          SalesCode, NetSellingPrice, CommissionRate, CommissionAmount, Remarks, Status, SellingCommission " &
                " FROM     tblRE " &
                " WHERE    TransId = '" & ID & "' " &
                " ORDER BY TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            disableEvent = True
            TransID = SQL.SQLDR("TransID").ToString
            RENo = SQL.SQLDR("RE_No").ToString
            txtTransNum.Text = RENo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtPropCode.Text = SQL.SQLDR("PropCode").ToString
            dtpDocDate.Value = SQL.SQLDR("DateRE")
            nupTerm.Value = SQL.SQLDR("Terms")
            txtSelling.Text = CDec(SQL.SQLDR("SellingAmount")).ToString("N2")
            txtMisc.Text = CDec(SQL.SQLDR("MiscAmount")).ToString("N2")
            txtAddl.Text = CDec(SQL.SQLDR("AddlAmount")).ToString("N2")
            txtVAT.Text = CDec(SQL.SQLDR("VATAmount")).ToString("N2")
            txtSellingCommission.Text = CDec(SQL.SQLDR("SellingCommission")).ToString("N2")
            txtTCP.Text = CDec(SQL.SQLDR("TCPAmount")).ToString("N2")
            If SQL.SQLDR("SpotTCP") Then
                rbTCPSpot.Checked = True
            Else
                rbTCPWithin.Checked = True
                nupWithInDays.Value = SQL.SQLDR("TCP_CashWithin")
                'If SQL.SQLDR("TCP_CashWithin") = "" Then
                '    nupWithInDays.Value = 0
                'Else
                '    nupWithInDays.Value = SQL.SQLDR("TCP_CashWithin")
                'End If

            End If
            txtTCPReserve.Text = CDec(SQL.SQLDR("TCP_Reservation")).ToString("N2")
            nupTCPDiscount.Value = SQL.SQLDR("TCP_DiscountRate")
            txtTCPDiscount.Text = CDec(SQL.SQLDR("TCP_DiscountAmount")).ToString("N2")
            txtTCPBalance.Text = CDec(SQL.SQLDR("TCP_Balance")).ToString("N2")
            lblCashPayableUntil.Text = CDate(SQL.SQLDR("TCP_PayableUntil")).ToString("MMMM dd, yyyy")

            txtDeferredReserve.Text = CDec(SQL.SQLDR("DC_Reservation")).ToString("N2")
            nupDeferredTerms.Value = SQL.SQLDR("DC_Terms")
            txtDeferredMonthly.Text = CDec(SQL.SQLDR("DC_Monthly")).ToString("N2")
            dtpDeferredStart.Text = CDate(SQL.SQLDR("DC_Start"))
            dtpDeferredEnd.Text = CDate(SQL.SQLDR("DC_End"))

            txtReserve.Text = CDec(SQL.SQLDR("ReservationAmount")).ToString("N2")
            nupDPRate.Value = CDec(SQL.SQLDR("DP_Rate"))
            txtTotalDP.Text = CDec(SQL.SQLDR("TotalDP")).ToString("N2")
            If SQL.SQLDR("SpotDP") Then
                rbSpotDp.Checked = True
            ElseIf SQL.SQLDR("Termed") Then
                rbTerm.Checked = True
            Else
                rbCashWithin.Checked = True
                nupDPDays.Value = SQL.SQLDR("DP_WithinDays")
            End If
            nupDiscountRate.Text = CDec(SQL.SQLDR("DiscountRate")).ToString("N2")
            txtDiscount.Text = CDec(SQL.SQLDR("Discount")).ToString("N2")
            txtBalanceDP.Text = CDec(SQL.SQLDR("DP_Balance")).ToString("N2")
            lblDPPayableUntil.Text = CDate(SQL.SQLDR("DP_PayableUntil")).ToString("MMMM dd, yyyy")

            txtMonthly.Text = CDec(SQL.SQLDR("MonthlyPayment")).ToString("N2")
            txtLoanable.Text = CDec(SQL.SQLDR("Balance")).ToString("N2")
            chkVATable.Checked = SQL.SQLDR("VATable")
            chkVATInc.Checked = SQL.SQLDR("VATInc")
            dtpStart.Value = SQL.SQLDR("EquityStart")
            cbFinancingMode.Text = SQL.SQLDR("FinancingMode").ToString

            txtSalesCode.Text = SQL.SQLDR("SalesCode").ToString
            txtNetSelling.Text = CDec(SQL.SQLDR("NetSellingPrice")).ToString("N2")
            nupCommission.Value = CDec(SQL.SQLDR("CommissionRate"))
            txtCommission.Text = CDec(SQL.SQLDR("CommissionAmount")).ToString("N2")

            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString

            'disableEvent = True
            cbPaymentType.SelectedItem = SQL.SQLDR("PaymentType").ToString
            disableEvent = False


            txtSJNo.Text = LoadSJNo(TransID)

            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            txtPropName.Text = GetPropName(txtPropCode.Text)
            txtSalesName.Text = GetVCEName(txtSalesCode.Text)
            'GenerateEntry()
            LoadSchedule(TransID)
            LoadAccountingEntry(TransID)
            If cbPaymentType.Text = "Financing" AndAlso dgvDPSchedule.Rows.Count = 0 Then
                GenerateSchedule()
                SaveSchedule(TransID)
            ElseIf cbPaymentType.Text = "Deferred Cash" AndAlso dgvDeferredSched.Rows.Count = 0 Then
                GenerateDeferredSchedule()
                SaveSchedule(TransID)
            End If


            If cbPaymentType.Text = "Financing" Then
                LoadFinancingSchedule(TransID)

                If cbFinancingMode.Text = "In-House" AndAlso dgvInhouse.Rows.Count = 0 Then
                    GenerateInhouseSched()
                    SaveLoanDiffSchedule(TransID)
                ElseIf cbFinancingMode.Text = "Pagibig" AndAlso dgvPagibig.Rows.Count = 0 Then
                    GenerateLoanDiffSchedule()
                    SaveLoanDiffSchedule(TransID)
                ElseIf cbFinancingMode.Text = "Bank" AndAlso dgvBank.Rows.Count = 0 Then
                    GenerateLoanDiffSchedule()
                    SaveLoanDiffSchedule(TransID)
                End If
            End If

            ' TOOLSTRIP BUTTONS
            If txtStatus.Text = "Cancelled" Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
                'ElseIf txtStatus.Text = "Active" Then
                '    tsbEdit.Enabled = False

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
            btnViewLedger.Enabled = True

            EnableControl(False)
        Else
            ClearText()
        End If
    End Sub

    Private Sub LoadAccountingEntry(ByVal TransID As Integer)
        Try
            Dim query As String
            query = " SELECT JE_No, VCECode, VCEName, AccntCode, AccntTitle, Particulars, RefNo, " &
                     "        ISNULL(Debit,0) AS Debit, ISNULL(Credit,0) AS Credit " &
                     " FROM   View_GL " &
                     " WHERE  RefType ='SJ' AND RefTransID IN (SELECT TransID FROM tblSJ WHERE RE_ID = '" & TransID & "') "
            SQL.ReadQuery(query)
            dgvEntry.Rows.Clear()
            If SQL.SQLDR.HasRows Then
                While SQL.SQLDR.Read
                    JETransID = SQL.SQLDR("JE_No").ToString
                    dgvEntry.Rows.Add(SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccntTitle").ToString,
                                      IIf(CDec(SQL.SQLDR("Debit")) = 0, "", CDec(SQL.SQLDR("Debit")).ToString("N2")), If(CDec(SQL.SQLDR("Credit")) = 0, "", CDec(SQL.SQLDR("Credit")).ToString("N2")),
                                      SQL.SQLDR("Particulars").ToString, SQL.SQLDR("VCECode").ToString, SQL.SQLDR("VCEName").ToString, SQL.SQLDR("RefNo").ToString)
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

    Private Sub LoadSchedule(ID As Integer)
        Dim query As String
        query = " SELECT PaymentNo, DueDate, Principal FROM tblRE_Schedule WHERE TransID = @TransID AND Type = @Type ORDER BY PaymentNo "
        SQL.FlushParams()
        SQL.AddParam("@TransID", ID)
        SQL.AddParam("@Type", "DP")
        SQL.ReadQuery(query)
        dgvDPSchedule.Rows.Clear()
        dgvDeferredSched.Rows.Clear()
        Dim ctr As Integer = 1
        While SQL.SQLDR.Read
            If cbPaymentType.SelectedItem = "Deferred Cash" Then
                dgvDeferredSched.Rows.Add(SQL.SQLDR("PaymentNo"), CDate(SQL.SQLDR("DueDate")).ToString("MM/dd/yyyy"), CDec(SQL.SQLDR("Principal")).ToString("N2"))
            ElseIf cbPaymentType.SelectedItem = "Financing" Then
                dgvDPSchedule.Rows.Add(SQL.SQLDR("PaymentNo"), CDate(SQL.SQLDR("DueDate")).ToString("MM/dd/yyyy"), CDec(SQL.SQLDR("Principal")).ToString("N2"))
            End If
        End While

    End Sub

    Private Sub LoadFinancingSchedule(ID As Integer)
        Dim query As String
        query = " SELECT PaymentNo, DueDate, Principal, Interest, Principal + Interest AS Total FROM tblRE_Schedule WHERE TransID = @TransID AND Type = @Type ORDER BY PaymentNo "
        SQL.FlushParams()
        SQL.AddParam("@TransID", ID)
        SQL.AddParam("@Type", "Financing")
        SQL.ReadQuery(query)
        dgvInhouse.Rows.Clear()
        dgvPagibig.Rows.Clear()
        dgvBank.Rows.Clear()
        Dim ctr As Integer = 1
        While SQL.SQLDR.Read
            If cbFinancingMode.SelectedItem = "In-House" Then
                dgvInhouse.Rows.Add(SQL.SQLDR("PaymentNo"), CDate(SQL.SQLDR("DueDate")).ToString("MM/dd/yyyy"), CDec(SQL.SQLDR("Interest")).ToString("N2"), CDec(SQL.SQLDR("Principal")).ToString("N2"), CDec(SQL.SQLDR("Total")).ToString("N2"))
            ElseIf cbPaymentType.SelectedItem = "Pagibig" Then
                dgvPagibig.Rows.Add(SQL.SQLDR("PaymentNo"), CDate(SQL.SQLDR("DueDate")).ToString("MM/dd/yyyy"), CDec(SQL.SQLDR("Principal")).ToString("N2"))
            ElseIf cbPaymentType.SelectedItem = "Bank" Then
                dgvBank.Rows.Add(SQL.SQLDR("PaymentNo"), CDate(SQL.SQLDR("DueDate")).ToString("MM/dd/yyyy"), CDec(SQL.SQLDR("Principal")).ToString("N2"))
            End If
        End While

    End Sub


    Private Function GetAddress(SO_ID As Integer, VCECode As String) As String
        Dim query As String
        query = " Select ISNULL(DeliveryAddress,'') AS DeliveryAddress FROM viewProject_Details WHERE SO_ID = @SO_ID "
        SQL.FlushParams()
        SQL.AddParam("@SO_ID", SO_ID)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read AndAlso SQL.SQLDR("DeliveryAddress") <> "" Then
            Return SQL.SQLDR("DeliveryAddress").ToString
        Else
            query = " SELECT Address FROM ViewVCE_Details WHERE VCECode = @VCECode "
            SQL.FlushParams()
            SQL.AddParam("@VCECode", VCECode)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Return SQL.SQLDR("Address").ToString
            Else
                Return ""
            End If
        End If
    End Function

    Private Sub txtVCEName_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.txtFilter.Text = txtVCEName.Text
            f.Type = "ALL"
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            txtVCEName.Text = f.VCEName
            f.Dispose()
        End If
    End Sub


    Private Sub ClearText()
        disableEvent = True
        txtTransNum.Clear()
        txtPropCode.Clear()
        txtPropName.Clear()
        txtVCECode.Clear()
        txtVCEName.Clear()
        txtSJNo.Clear()
        txtSelling.Text = "0.00"
        txtMisc.Text = "0.00"
        txtAddl.Text = "0.00"
        txtVAT.Text = "0.00"
        txtTCP.Text = "0.00"
        txtSellingCommission.Text = "0.00"
        txtReserve.Text = "0.00"
        txtTotalDP.Text = "0.00"
        txtMonthly.Text = "0.00"
        txtDiscount.Text = "0.00"
        txtLoanable.Text = "0.00"
        txtTotalCredit.Text = "0.00"
        txtTotalDebit.Text = "0.00"
        txtRemarks.Clear()
        txtStatus.Text = "Open"
        disableEvent = False
        dgvEntry.Rows.Clear()
    End Sub

    Private Sub SaveRE()
        Try
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " &
                        " tblRE(TransID, RE_No, BranchCode, BusinessCode, VCECode, PropCode, DateRE, SellingAmount, VATable, VATInc, MiscAmount, AddlAmount, VATAmount, TCPAmount, " &
                        "       PaymentType, SpotTCP, TCP_CashWithin, TCP_Reservation, TCP_DiscountRate, TCP_DiscountAmount, TCP_Balance, TCP_PayableUntil, " &
                        "       DC_Reservation, DC_Terms, DC_Monthly, DC_Start, DC_End, FinancingMode, " &
                        "       ReservationAmount, DP_Rate, TotalDP,  SpotDP, DP_WithinDays, Termed, DiscountRate, Discount, DP_Balance, DP_PayableUntil, " &
                        "       InHouseRate, InHouseYear, InHousePaymentPerYear, InHouseTerms, InHouseMonthly, InHouseStart, InHouseUntil, " &
                        "       MonthlyPayment, Balance, ApprovedLoanable, LoanDifference, LoanDiffTerms, LoanDiffStart, LoanDiffMonthly, Terms, EquityStart, " &
                        "       SalesCode, NetSellingPrice, CommissionRate, CommissionAmount, Remarks, Status, WhoCreated, DateCreated, TransAuto, SellingCommission, isEdit) " &
                        " VALUES (@TransID, @RE_No, @BranchCode, @BusinessCode, @VCECode, @PropCode, @DateRE, @SellingAmount, @VATable, @VATInc, @MiscAmount, @AddlAmount, @VATAmount, @TCPAmount, " &
                        "       @PaymentType, @SpotTCP, @TCP_CashWithin, @TCP_Reservation, @TCP_DiscountRate, @TCP_DiscountAmount, @TCP_Balance, @TCP_PayableUntil, " &
                        "       @DC_Reservation, @DC_Terms, @DC_Monthly, @DC_Start, @DC_End, @FinancingMode, " &
                        "       @ReservationAmount, @DP_Rate, @TotalDP, @SpotDP, @DP_WithinDays, @Termed, @DiscountRate, @Discount, @DP_Balance, @DP_PayableUntil, " &
                        "       @InHouseRate, @InHouseYear, @InHousePaymentPerYear, @InHouseTerms, @InHouseMonthly, @InHouseStart, @InHouseUntil, " &
                        "       @MonthlyPayment, @Balance, @ApprovedLoanable, @LoanDifference, @LoanDiffTerms, @LoanDiffStart, @LoanDiffMonthly, @Terms, @EquityStart, " &
                        "       @SalesCode, @NetSellingPrice, @CommissionRate, @CommissionAmount, @Remarks, @Status, @WhoCreated, GETDATE(), @TransAuto, @SellingCommission, @isEdit) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@RE_No", RENo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@PropCode", txtPropCode.Text)
            SQL.AddParam("@DateRE", dtpDocDate.Value.Date)
            SQL.AddParam("@SellingAmount", CDec(txtSelling.Text))
            SQL.AddParam("@MiscAmount", CDec(txtMisc.Text))
            SQL.AddParam("@AddlAmount", CDec(txtAddl.Text))
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@SellingCommission", CDec(txtSellingCommission.Text))
            SQL.AddParam("@TCPAmount", CDec(txtTCP.Text))
            SQL.AddParam("@PaymentType", cbPaymentType.SelectedItem)
            If cbPaymentType.SelectedItem = "Cash Sales" Then
                SQL.AddParam("@SpotTCP", rbTCPSpot.Checked)
            Else
                SQL.AddParam("@SpotTCP", False)
            End If
            If rbTCPWithin.Checked Then
                SQL.AddParam("@TCP_CashWithin", nupWithInDays.Value)
            Else
                SQL.AddParam("@TCP_CashWithin", 0)
            End If
            SQL.AddParam("@TCP_Reservation", CDec(txtTCPReserve.Text))
            SQL.AddParam("@TCP_DiscountRate", CDec(nupTCPDiscount.Value))
            SQL.AddParam("@TCP_DiscountAmount", CDec(txtTCPDiscount.Text))
            SQL.AddParam("@TCP_Balance", CDec(txtTCPBalance.Text))
            SQL.AddParam("@TCP_PayableUntil", CDate(lblCashPayableUntil.Text))

            SQL.AddParam("@DC_Reservation", CDec(txtDeferredReserve.Text))
            SQL.AddParam("@DC_Terms", nupDeferredTerms.Value)
            SQL.AddParam("@DC_Monthly", CDec(txtDeferredMonthly.Text))
            SQL.AddParam("@DC_Start", dtpDeferredStart.Value.Date)
            SQL.AddParam("@DC_End", dtpDeferredEnd.Value.Date)

            SQL.AddParam("@ReservationAmount", CDec(txtReserve.Text))
            SQL.AddParam("@DP_Rate", nupDPRate.Value)
            SQL.AddParam("@TotalDP", CDec(txtTotalDP.Text))

            If cbPaymentType.SelectedItem = "Financing" Then
                SQL.AddParam("@SpotDP", rbSpotDp.Checked)
            Else
                SQL.AddParam("@SpotDP", False)
            End If
            If rbCashWithin.Checked Then
                SQL.AddParam("@DP_WithinDays", nupDPDays.Value)
            Else
                SQL.AddParam("@DP_WithinDays", 0)
            End If
            SQL.AddParam("@Termed", rbTerm.Checked)
            SQL.AddParam("@DiscountRate", nupDiscountRate.Value)
            SQL.AddParam("@Discount", CDec(txtDiscount.Text))
            SQL.AddParam("@DP_Balance", CDec(txtBalanceDP.Text))
            SQL.AddParam("@DP_PayableUntil", CDate(lblDPPayableUntil.Text))

            SQL.AddParam("@MonthlyPayment", CDec(txtMonthly.Text))
            SQL.AddParam("@Balance", CDec(txtLoanable.Text))
            SQL.AddParam("@VATable", chkVATable.Checked)
            SQL.AddParam("@VATInc", chkVATInc.Checked)
            SQL.AddParam("@Terms", nupTerm.Value)
            SQL.AddParam("@EquityStart", dtpStart.Value.Date)

            If cbPaymentType.SelectedItem = "Financing" Then
                SQL.AddParam("@FinancingMode", cbFinancingMode.SelectedItem)
            Else
                SQL.AddParam("@FinancingMode", "")
            End If
            If cbPaymentType.SelectedItem = "Financing" AndAlso cbFinancingMode.SelectedItem = "Pagibig" Then
                SQL.AddParam("@ApprovedLoanable", CDec(txtPagibigLoanable.Text))
                SQL.AddParam("@LoanDifference", CDec(txtPagibiLoanDiff.Text))
                SQL.AddParam("@LoanDiffTerms", nupPagibigTerms.Value)
                SQL.AddParam("@LoanDiffStart", dtpPagibigStart.Value)
                SQL.AddParam("@LoanDiffMonthly", CDec(txtPagibigMonthly.Text))
                SQL.AddParam("@LoanDiffUntil", CDate(lblPagibigUntil.Text))
            ElseIf cbPaymentType.SelectedItem = "Financing" AndAlso cbFinancingMode.SelectedItem = "Bank" Then
                SQL.AddParam("@ApprovedLoanable", CDec(txtBankLoanable.Text))
                SQL.AddParam("@LoanDifference", CDec(txtBankLoanDiff.Text))
                SQL.AddParam("@LoanDiffTerms", nupBankTerms.Value)
                SQL.AddParam("@LoanDiffStart", dtpBankStart.Value)
                SQL.AddParam("@LoanDiffMonthly", CDec(txtBankMonthly.Text))
                SQL.AddParam("@LoanDiffUntil", CDate(lblBankUntil.Text))
            Else
                SQL.AddParam("@ApprovedLoanable", 0)
                SQL.AddParam("@LoanDifference", 0)
                SQL.AddParam("@LoanDiffTerms", 0)
                SQL.AddParam("@LoanDiffStart", dtpDocDate.Value.Date)
                SQL.AddParam("@LoanDiffMonthly", 0)
                SQL.AddParam("@LoanDiffUntil", dtpDocDate.Value.Date)
            End If

            If cbPaymentType.SelectedItem = "Financing" AndAlso cbFinancingMode.SelectedItem = "In-House" Then
                SQL.AddParam("@InHouseRate", CDec(nupInhouseRate.Value))
                SQL.AddParam("@InHouseYear", CDec(nupInhouseYear.Value))
                SQL.AddParam("@InHousePaymentPerYear", CDec(nupInhousePaymentPerYear.Value))
                SQL.AddParam("@InHouseTerms", CDec(nupInhouseTotalMonths.Value))
                SQL.AddParam("@InHouseMonthly", CDec(txtInhouseMonthly.Text))
                SQL.AddParam("@InHouseStart", dtpInhouseStart.Value.Date)
                SQL.AddParam("@InHouseUntil", CDate(lblInhouseUntil.Text))
            Else
                SQL.AddParam("@InHouseRate", 0)
                SQL.AddParam("@InHouseYear", 0)
                SQL.AddParam("@InHousePaymentPerYear", 0)
                SQL.AddParam("@InHouseTerms", 0)
                SQL.AddParam("@InHouseMonthly", 0)
                SQL.AddParam("@InHouseStart", dtpDocDate.Value.Date)
                SQL.AddParam("@InHouseUntil", dtpDocDate.Value.Date)
            End If

            SQL.AddParam("@SalesCode", txtSalesCode.Text)
            SQL.AddParam("@NetSellingPrice", CDec(txtNetSelling.Text))
            SQL.AddParam("@CommissionRate", nupCommission.Value)
            SQL.AddParam("@CommissionAmount", CDec(txtCommission.Text))

            SQL.AddParam("@Remarks", txtRemarks.Text)
            'SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.AddParam("@TransAuto", TransAuto)
            SQL.AddParam("@isEdit", 0)
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Active")
            SQL.ExecNonQuery(insertSQL)
            SaveSchedule(TransID)
            SaveLoanDiffSchedule(TransID)
            If cbFinancingMode.Text <> "In-House" Then
                SaveLoanApprovedSchedule(TransID)
            End If

        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "RE_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub SaveSchedule(TransID As Integer)
        Dim SQL As New SQLControl
        Dim query As String
        query = "DELETE FROM tblRE_Schedule WHERE TransID = @TransID AND Type = @Type "
        SQL.FlushParams()
        SQL.AddParam("@Type", "DP")
        SQL.AddParam("@TransID", TransID)
        SQL.ExecNonQuery(query)

        query = " INSERT INTO tblRE_Schedule(TransID, Type, PaymentNo, DueDate, Principal, Interest) VALUES (@TransID, @Type, @PaymentNo,  @DueDate, @Principal, @Interest)"

        If cbPaymentType.SelectedItem = "Deferred Cash" Then
            For Each row As DataGridViewRow In dgvDeferredSched.Rows
                SQL.FlushParams()
                SQL.AddParam("@TransID", TransID)
                SQL.AddParam("@Type", "DP")
                SQL.AddParam("@PaymentNo", row.Cells(DataGridViewTextBoxColumn1.Index).Value)
                SQL.AddParam("@DueDate", row.Cells(DataGridViewTextBoxColumn2.Index).Value)
                SQL.AddParam("@Principal", CDec(row.Cells(DataGridViewTextBoxColumn3.Index).Value))
                SQL.AddParam("@Interest", 0)
                SQL.ExecNonQuery(query)
            Next
        ElseIf cbPaymentType.SelectedItem = "Financing" Then
            If rbTerm.Checked = True Then
                For Each row As DataGridViewRow In dgvDPSchedule.Rows
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@Type", "DP")
                    SQL.AddParam("@PaymentNo", row.Cells(chCount.Index).Value)
                    SQL.AddParam("@DueDate", row.Cells(chDateDue.Index).Value)
                    SQL.AddParam("@Principal", CDec(row.Cells(chAmount.Index).Value))
                    SQL.AddParam("@Interest", 0)
                    SQL.ExecNonQuery(query)
                Next
            End If
        End If
    End Sub

    Private Sub SaveLoanDiffSchedule(TransID As Integer)
        Dim SQL As New SQLControl
        Dim query As String
        query = "DELETE FROM tblRE_Schedule WHERE TransID = @TransID AND Type = @Type "
        SQL.FlushParams()
        SQL.AddParam("@TransID", TransID)
        SQL.AddParam("@Type", "Financing")
        SQL.ExecNonQuery(query)

        query = " INSERT INTO tblRE_Schedule(TransID, Type, PaymentNo, DueDate, Principal, Interest) VALUES (@TransID, @Type, @PaymentNo, @DueDate, @Principal, @Interest)"
        If cbPaymentType.SelectedItem = "Financing" Then
            If cbFinancingMode.SelectedItem = "In-House" Then
                For Each row As DataGridViewRow In dgvInhouse.Rows
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@Type", "Financing")
                    SQL.AddParam("@PaymentNo", row.Cells(dgcInHouseNo.Index).Value)
                    SQL.AddParam("@DueDate", row.Cells(dgcInHouseDueDate.Index).Value)
                    SQL.AddParam("@Principal", CDec(row.Cells(dgcInHousePrincipal.Index).Value))
                    SQL.AddParam("@Interest", CDec(row.Cells(dgcInhouseInterest.Index).Value))
                    SQL.ExecNonQuery(query)
                Next
            ElseIf cbFinancingMode.SelectedItem = "Pagibig" Then
                For Each row As DataGridViewRow In dgvPagibig.Rows
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@Type", "Financing")
                    SQL.AddParam("@PaymentNo", row.Cells(dgcPagibigPaymentNo.Index).Value)
                    SQL.AddParam("@DueDate", row.Cells(dgcPagibigDuedate.Index).Value)
                    SQL.AddParam("@Principal", CDec(row.Cells(dgcPagibigAmount.Index).Value))
                    SQL.AddParam("@Interest", 0)
                    SQL.ExecNonQuery(query)
                Next
            ElseIf cbFinancingMode.SelectedItem = "Bank" Then
                For Each row As DataGridViewRow In dgvBank.Rows
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@Type", "Financing")
                    SQL.AddParam("@PaymentNo", row.Cells(dgcBankPaymentNo.Index).Value)
                    SQL.AddParam("@DueDate", row.Cells(dgcBankDuedate.Index).Value)
                    SQL.AddParam("@Principal", CDec(row.Cells(dgcBankAmount.Index).Value))
                    SQL.AddParam("@Interest", 0)
                    SQL.ExecNonQuery(query)
                Next
            End If
        End If

    End Sub

    Private Sub SaveLoanApprovedSchedule(TransID As Integer)
        Dim SQL As New SQLControl
        Dim query As String
        Dim Terms As Decimal = 0
        query = "DELETE FROM tblRE_Schedule WHERE TransID = @TransID AND Type = @Type "
        SQL.FlushParams()
        SQL.AddParam("@TransID", TransID)
        SQL.AddParam("@Type", "LOGB")
        SQL.ExecNonQuery(query)
        query = " INSERT INTO tblRE_Schedule(TransID, Type, PaymentNo, DueDate, Principal, Interest) VALUES (@TransID, @Type, @PaymentNo, @DueDate, @Principal, @Interest)"
        If cbPaymentType.SelectedItem = "Financing" Then
            If cbFinancingMode.SelectedItem = "Pagibig" Then
                Terms = nupPagibigTerms.Value
                SQL.FlushParams()
                SQL.AddParam("@TransID", TransID)
                SQL.AddParam("@Type", "LOGB")
                SQL.AddParam("@PaymentNo", 1)
                SQL.AddParam("@DueDate", CDate(lblPagibigUntil.Text).ToString("yyyy-MM-dd"))
                SQL.AddParam("@Principal", CDec(txtPagibigLoanable.Text))
                SQL.AddParam("@Interest", 0)
                SQL.ExecNonQuery(query)
            ElseIf cbFinancingMode.SelectedItem = "Bank" Then
                Terms = nupBankTerms.Value
                SQL.FlushParams()
                SQL.AddParam("@TransID", TransID)
                SQL.AddParam("@Type", "LOGB")
                SQL.AddParam("@PaymentNo", 1)
                SQL.AddParam("@DueDate", CDate(lblBankUntil.Text).ToString("yyyy-MM-dd"))
                SQL.AddParam("@Principal", CDec(txtBankLoanable.Text))
                SQL.AddParam("@Interest", 0)
                SQL.ExecNonQuery(query)
            End If
        End If

    End Sub

    Private Sub UpdateRE()
        Try

            activityStatus = True
            Dim updateSQL, insertSQL, deleteSQL As String
            updateSQL = " UPDATE    tblRE " &
                        " SET       RE_No = @RE_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, VCECode = @VCECode, PropCode = @PropCode, " &
                        "           DateRE = @DateRE, SellingAmount = @SellingAmount, VATable = @VATable, VATInc = @VATInc, MiscAmount = @MiscAmount, AddlAmount = @AddlAmount, VATAmount = @VATAmount, TCPAmount = @TCPAmount,  " &
                        "           PaymentType = @PaymentType, SpotTCP = @SpotTCP, TCP_CashWithin = @TCP_CashWithin, TCP_Reservation = @TCP_Reservation,  " &
                        "           TCP_DiscountRate = @TCP_DiscountRate, TCP_DiscountAmount = @TCP_DiscountAmount, TCP_Balance = @TCP_Balance, TCP_PayableUntil = @TCP_PayableUntil, " &
                        "           DC_Reservation = @DC_Reservation, DC_Terms = @DC_Terms, DC_Monthly = @DC_Monthly, DC_Start = @DC_Start, DC_End = @DC_End, " &
                        "           ReservationAmount = @ReservationAmount, DP_Rate = @DP_Rate, TotalDP = @TotalDP, SpotDP = @SpotDP, DP_WithinDays = @DP_WithinDays, " &
                        "           Termed = @Termed, DiscountRate = @DiscountRate, Discount = @Discount, DP_Balance = @DP_Balance, DP_PayableUntil = @DP_PayableUntil, " &
                        "           MonthlyPayment = @MonthlyPayment,  Balance = @Balance,  Terms = @Terms, EquityStart = @EquityStart, FinancingMode = @FinancingMode, " &
                        "           InHouseRate = @InHouseRate, InHouseYear = @InHouseYear, InHousePaymentPerYear = @InHousePaymentPerYear, InHouseTerms = @InHouseTerms, " &
                        "           InHouseMonthly = @InHouseMonthly, InHouseStart = @InHouseStart, InHouseUntil = @InHouseUntil, ApprovedLoanable = @ApprovedLoanable, " &
                        "           LoanDifference = @LoanDifference, LoanDiffTerms = @LoanDiffTerms, LoanDiffStart = @LoanDiffStart, LoanDiffMonthly = @LoanDiffMonthly, LoanDiffUntil = @LoanDiffUntil, " &
                        "           SalesCode = @SalesCode, NetSellingPrice = @NetSellingPrice, CommissionRate = @CommissionRate, CommissionAmount = @CommissionAmount, Remarks = @Remarks, Status = @Status, " &
                        "           WhoModified = @WhoModified, DateModified = GETDATE(), TransAuto = @TransAuto, SellingCommission = @SellingCommission, isEdit = @isEdit" &
                        " WHERE     TransID = @TransID  "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@RE_No", RENo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@PropCode", txtPropCode.Text)
            SQL.AddParam("@DateRE", dtpDocDate.Value.Date)
            SQL.AddParam("@SellingAmount", CDec(txtSelling.Text))
            SQL.AddParam("@MiscAmount", CDec(txtMisc.Text))
            SQL.AddParam("@AddlAmount", CDec(txtAddl.Text))
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@SellingCommission", CDec(txtSellingCommission.Text))
            SQL.AddParam("@TCPAmount", CDec(txtTCP.Text))
            SQL.AddParam("@PaymentType", cbPaymentType.SelectedItem)

            If cbPaymentType.SelectedItem = "Cash Sales" Then
                SQL.AddParam("@SpotTCP", rbTCPSpot.Checked)
            Else
                SQL.AddParam("@SpotTCP", False)
            End If
            If rbTCPWithin.Checked Then
                SQL.AddParam("@TCP_CashWithin", nupWithInDays.Value)
            Else
                SQL.AddParam("@TCP_CashWithin", 0)
            End If
            SQL.AddParam("@TCP_Reservation", CDec(txtTCPReserve.Text))
            SQL.AddParam("@TCP_DiscountRate", CDec(nupTCPDiscount.Value))
            SQL.AddParam("@TCP_DiscountAmount", CDec(txtTCPDiscount.Text))
            SQL.AddParam("@TCP_Balance", CDec(txtTCPBalance.Text))
            SQL.AddParam("@TCP_PayableUntil", CDate(lblCashPayableUntil.Text))

            SQL.AddParam("@DC_Reservation", CDec(txtDeferredReserve.Text))
            SQL.AddParam("@DC_Terms", nupDeferredTerms.Value)
            SQL.AddParam("@DC_Monthly", CDec(txtDeferredMonthly.Text))
            SQL.AddParam("@DC_Start", dtpDeferredStart.Value.Date)
            SQL.AddParam("@DC_End", dtpDeferredEnd.Value.Date)

            SQL.AddParam("@ReservationAmount", CDec(txtReserve.Text))
            SQL.AddParam("@DP_Rate", nupDPRate.Value)
            SQL.AddParam("@TotalDP", CDec(txtTotalDP.Text))
            If cbPaymentType.SelectedItem = "Financing" Then
                SQL.AddParam("@SpotDP", rbSpotDp.Checked)
            Else
                SQL.AddParam("@SpotDP", False)
            End If
            If rbCashWithin.Checked Then
                SQL.AddParam("@DP_WithinDays", nupDPDays.Value)
            Else
                SQL.AddParam("@DP_WithinDays", 0)
            End If
            SQL.AddParam("@Termed", rbTerm.Checked)
            SQL.AddParam("@DiscountRate", nupDiscountRate.Value)
            SQL.AddParam("@Discount", CDec(txtDiscount.Text))
            SQL.AddParam("@DP_Balance", CDec(txtBalanceDP.Text))
            SQL.AddParam("@DP_PayableUntil", CDate(lblDPPayableUntil.Text))

            SQL.AddParam("@MonthlyPayment", CDec(txtMonthly.Text))
            SQL.AddParam("@Balance", CDec(txtLoanable.Text))
            SQL.AddParam("@VATable", chkVATable.Checked)
            SQL.AddParam("@VATInc", chkVATInc.Checked)
            SQL.AddParam("@Terms", nupTerm.Value)
            SQL.AddParam("@EquityStart", dtpStart.Value.Date)


            If cbPaymentType.SelectedItem = "Financing" Then
                SQL.AddParam("@FinancingMode", cbFinancingMode.SelectedItem)
            Else
                SQL.AddParam("@FinancingMode", "")
            End If
            If cbPaymentType.SelectedItem = "Financing" AndAlso cbFinancingMode.SelectedItem = "Pagibig" Then
                SQL.AddParam("@ApprovedLoanable", CDec(txtPagibigLoanable.Text))
                SQL.AddParam("@LoanDifference", CDec(txtPagibiLoanDiff.Text))
                SQL.AddParam("@LoanDiffTerms", nupPagibigTerms.Value)
                SQL.AddParam("@LoanDiffStart", dtpPagibigStart.Value)
                SQL.AddParam("@LoanDiffMonthly", CDec(txtPagibigMonthly.Text))
                SQL.AddParam("@LoanDiffUntil", CDate(lblPagibigUntil.Text))
            ElseIf cbPaymentType.SelectedItem = "Financing" AndAlso cbFinancingMode.SelectedItem = "Bank" Then
                SQL.AddParam("@ApprovedLoanable", CDec(txtBankLoanable.Text))
                SQL.AddParam("@LoanDifference", CDec(txtBankLoanDiff.Text))
                SQL.AddParam("@LoanDiffTerms", nupBankTerms.Value)
                SQL.AddParam("@LoanDiffStart", dtpBankStart.Value)
                SQL.AddParam("@LoanDiffMonthly", CDec(txtBankMonthly.Text))
                SQL.AddParam("@LoanDiffUntil", CDate(lblBankUntil.Text))
            Else
                SQL.AddParam("@ApprovedLoanable", 0)
                SQL.AddParam("@LoanDifference", 0)
                SQL.AddParam("@LoanDiffTerms", 0)
                SQL.AddParam("@LoanDiffStart", dtpDocDate.Value.Date)
                SQL.AddParam("@LoanDiffMonthly", 0)
                SQL.AddParam("@LoanDiffUntil", dtpDocDate.Value.Date)
            End If

            If cbPaymentType.SelectedItem = "Financing" AndAlso cbFinancingMode.SelectedItem = "In-House" Then
                SQL.AddParam("@InHouseRate", CDec(nupInhouseRate.Value))
                SQL.AddParam("@InHouseYear", CDec(nupInhouseYear.Value))
                SQL.AddParam("@InHousePaymentPerYear", CDec(nupInhousePaymentPerYear.Value))
                SQL.AddParam("@InHouseTerms", CDec(nupInhouseTotalMonths.Value))
                SQL.AddParam("@InHouseMonthly", CDec(txtInhouseMonthly.Text))
                SQL.AddParam("@InHouseStart", dtpInhouseStart.Value.Date)
                SQL.AddParam("@InHouseUntil", CDate(lblInhouseUntil.Text))
            Else
                SQL.AddParam("@InHouseRate", 0)
                SQL.AddParam("@InHouseYear", 0)
                SQL.AddParam("@InHousePaymentPerYear", 0)
                SQL.AddParam("@InHouseTerms", 0)
                SQL.AddParam("@InHouseMonthly", 0)
                SQL.AddParam("@InHouseStart", dtpDocDate.Value.Date)
                SQL.AddParam("@InHouseUntil", dtpDocDate.Value.Date)
            End If

            SQL.AddParam("@SalesCode", txtSalesCode.Text)
            SQL.AddParam("@NetSellingPrice", CDec(txtNetSelling.Text))
            SQL.AddParam("@CommissionRate", nupCommission.Value)
            SQL.AddParam("@CommissionAmount", CDec(txtCommission.Text))

            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@Status", "Draft")
            SQL.AddParam("@WhoModified", UserID)
            SQL.AddParam("@TransAuto", TransAuto)
            SQL.AddParam("@isEdit", 1)
            SQL.ExecNonQuery(updateSQL)
            SaveSchedule(TransID)
            If cbPaymentType.Text = "Financing" Then
                SaveLoanDiffSchedule(TransID)
                If cbFinancingMode.Text <> "In-House" Then
                    SaveLoanApprovedSchedule(TransID)
                End If
            End If


            Dim query, JE_ID As String
            query = " SELECT JE_No " &
                    " FROM tblJE_Details " &
                    " WHERE RefNo LIKE '%" & txtTransNum.Text & "' "
            SQL.FlushParams()
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                JE_ID = SQL.SQLDR("JE_No").ToString
            End If

            If Not IsNothing(JE_ID) Then
                'DELETE ACCOUNTING ENTRIES
                deleteSQL = " DELETE FROM tblJE_Details  WHERE  JE_No = @JE_No "
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JE_ID)
                SQL.ExecNonQuery(deleteSQL)
            End If

            'Dim line As Integer = 1
            'Dim SJID As Integer
            'SJID = LoadSJID(TransID)
            'Dim SJNo As String
            'SJNo = LoadSJNo(TransID)
            'insertSQL = " UPDATE tblSJ " &
            '            " SET    SJ_No = @SJ_No, VCECode  = @VCECode, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " &
            '            "        DateSJ = @DateSJ, TotalAmount = @TotalAmount, Remarks = @Remarks, TransAuto = @TransAuto, WhoModified = @WhoModified, " &
            '            "        Terms = @Terms, DueDate= @DueDate, DateModified = GETDATE() " &
            '            " WHERE  TransID = @TransID "
            'SQL.FlushParams()
            'SQL.AddParam("@TransID", SJID)
            'SQL.AddParam("@SJ_No", SJNo)
            'SQL.AddParam("@VCECode", txtVCECode.Text)
            'SQL.AddParam("@BranchCode", BranchCode)
            'SQL.AddParam("@BusinessCode", BusinessType)
            'SQL.AddParam("@DateSJ", dtpDocDate.Value.Date)
            'SQL.AddParam("@DueDate", dtpDocDate.Value.Date)
            'SQL.AddParam("@TotalAmount", CDec(txtTCP.Text))
            'SQL.AddParam("@Remarks", txtRemarks.Text)
            'SQL.AddParam("@TransAuto", TransAuto)
            'SQL.AddParam("@WhoModified", UserID)
            'SQL.AddParam("@Terms", nupDeferredTerms.Value)
            'SQL.AddParam("@RE_Ref", txtTransNum.Text)
            'SQL.ExecNonQuery(insertSQL)

            'JETransID = LoadJE("SJ", SJID)

            'If JETransID = 0 Then
            '    insertSQL = " INSERT INTO " &
            '                " tblJE_Header (AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, WhoCreated) " &
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
            '    updateSQL = " UPDATE tblJE_Header " &
            '                " SET    AppDate = @AppDate, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " &
            '                "        RefType = @RefType, RefTransID = @RefTransID, Book = @Book, TotalDBCR = @TotalDBCR, " &
            '                "        Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE() " &
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
            '        insertSQL = " INSERT INTO " &
            '                    " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber) " &
            '                    " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber)"
            '        SQL.FlushParams()
            '        SQL.AddParam("@JE_No", JETransID)
            '        SQL.AddParam("@AccntCode", item.Cells(chAccntCode.Index).Value.ToString)
            '        If item.Cells(chVCECode.Index).Value <> Nothing AndAlso item.Cells(chVCECode.Index).Value <> "" Then
            '            SQL.AddParam("@VCECode", item.Cells(chVCECode.Index).Value.ToString)
            '        Else
            '            SQL.AddParam("@VCECode", txtVCECode.Text)
            '        End If
            '        If item.Cells(chDebit.Index).Value <> Nothing AndAlso IsNumeric(item.Cells(chDebit.Index).Value) Then
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



        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "RE_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub btnSearchVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchVCE.Click
        Dim f As New frmVCE_Search
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        txtVCEName.Text = f.VCEName
        f.Dispose()
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("RE_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmRELoadTransactions
            f.ShowDialog("RE")
            TransID = f.transID
            LoadRE(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("RE_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            RENo = ""

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
            btnViewLedger.Enabled = False

            cbPaymentType.SelectedItem = "Financing"

            EnableControl(True)

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
        End If
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("RE_EDIT") Then
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
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If RENo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblRE  WHERE RE_No < '" & RENo & "' ORDER BY RE_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadRE(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If RENo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblRE  WHERE RE_No > '" & RENo & "' ORDER BY RE_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadRE(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub


    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        ' Toolstrip Buttons
        If RENo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
            btnViewLedger.Enabled = False
        Else
            LoadRE(TransID)
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
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click

        If Not AllowAccess("RE_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim updateSQL As String
                        updateSQL = " UPDATE  tblRE SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        RENo = txtTransNum.Text
                        SQL.AddParam("@TransID", TransID)
                        SQL.ExecNonQuery(updateSQL)

                        If txtSJNo.Text <> "" Then
                            Dim SJ_ID As String = ""
                            Dim selectSQL As String
                            selectSQL = "SELECT TransID, SJ_No FROM tblSJ WHERE SJ_No = @SJ_No"
                            SQL.FlushParams()
                            SQL.AddParam("@SJ_No", txtSJNo.Text)
                            SQL.ReadQuery(selectSQL)
                            If SQL.SQLDR.Read Then
                                SJ_ID = SQL.SQLDR("TransID")
                            End If

                            Dim updateSQL2 As String
                            updateSQL2 = " UPDATE  tblSJ SET Status ='Cancelled' WHERE TransID = @TransID "
                            SQL.FlushParams()
                            SQL.AddParam("@TransID", SJ_ID)
                            SQL.ExecNonQuery(updateSQL2)

                            Dim updateSQL3 As String
                            updateSQL3 = " UPDATE  tblJE_Header SET Status ='Cancelled' WHERE RefType = 'SJ' AND RefTransID = @TransID "
                            SQL.FlushParams()
                            SQL.AddParam("@TransID", SJ_ID)
                            SQL.ExecNonQuery(updateSQL3)


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
                        EnableControl(False)

                        RENo = txtTransNum.Text
                        LoadRE(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "RE_No", RENo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub frmRE_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            If txtPropCode.Text = "" Then
                Msg("Please enter VCECode!", MsgBoxStyle.Exclamation)
            ElseIf TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID(ColumnID, DBTable)
                    RENo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    txtTransNum.Text = RENo
                    SaveRE()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    RENo = txtTransNum.Text
                    LoadRE(TransID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    UpdateRE()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    RENo = txtTransNum.Text
                    LoadRE(TransID)
                End If
            End If
        End If
    End Sub

    Private Function DataValidated() As Boolean
        If txtPropCode.Text = "" Then
            Msg("Please select Customer!", MsgBoxStyle.Exclamation)
            Return False
        Else
            Return True
        End If
        Return True

    End Function



    Private Sub btnVCE_Click(sender As Object, e As EventArgs) Handles btnVCE.Click
        frmVCE_Master.ShowDialog(txtVCECode.Text)
    End Sub

    Private Sub btnSearchProp_Click(sender As Object, e As EventArgs) Handles btnSearchProp.Click
        Dim f As New frmProperty_Search
        f.ShowDialog()
        txtPropCode.Text = f.PropCode
        txtPropName.Text = f.PropName
        f.Dispose()
        LoadDetails(txtPropCode.Text)
    End Sub

    Private Sub LoadDetails(Code As String)
        Dim query As String
        query = " SELECT   ISNULL( TotalFloorPrice + TotalLotPrice,0) AS SellingPrice, ISNULL(MiscFee,0) AS MiscFee, AddlCost, VATable, VATInc, ContractPrice, VATAmount " &
                " FROM      tblSaleProperty " &
                " WHERE     Unitcode = @Unitcode "
        SQL.FlushParams()
        SQL.AddParam("@UnitCOde", Code)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            disableEvent = True
            txtSelling.Text = CDec(SQL.SQLDR("SellingPrice")).ToString("N2")
            txtMisc.Text = CDec(SQL.SQLDR("MiscFee")).ToString("N2")
            txtAddl.Text = CDec(SQL.SQLDR("AddlCost")).ToString("N2")
            chkVATable.Checked = SQL.SQLDR("VATable")
            chkVATInc.Checked = SQL.SQLDR("VATInc")
            txtSellingCommission.Text = "0.00"
            txtTCP.Text = CDec(SQL.SQLDR("ContractPrice")).ToString("N2")
            txtVAT.Text = If(IsDBNull(SQL.SQLDR("VATAmount")), "0.00", CDec(SQL.SQLDR("VATAmount")).ToString("N2"))

            disableEvent = False
            ComputeCommission()
            Recompute()
            'GenerateEntry()
        End If
    End Sub


    Private Sub txtPropName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPropName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmProperty_Search
            f.cbFilter.SelectedItem = "Description"
            f.txtFilter.Text = txtPropName.Text
            f.ShowDialog()
            txtPropCode.Text = f.PropCode
            txtPropName.Text = f.PropName
            f.Dispose()
        End If
    End Sub


    Private Sub GenerateSchedule()
        Dim DP As Decimal = 0
        Dim Monthly As Decimal = 0
        Dim Terms As Decimal = 0
        Dim RunningTotal As Decimal = 0
        Dim Amort As Decimal = 0
        If IsNumeric(txtBalanceDP.Text) Then DP = CDec(txtBalanceDP.Text)
        If IsNumeric(txtMonthly.Text) Then Monthly = CDec(txtMonthly.Text)
        Terms = nupTerm.Value

        If dgvDPSchedule.Columns.Count > 0 Then
            dgvDPSchedule.Rows.Clear()
            For i As Integer = 0 To Terms - 1
                RunningTotal += Monthly
                If RunningTotal > DP Then
                    Amort = Monthly - (RunningTotal - DP)
                Else
                    Amort = Monthly
                End If
                dgvDPSchedule.Rows.Add(i + 1, dtpStart.Value.AddMonths(i).ToString("MM/dd/yyyy"), Amort.ToString("N2"))
            Next
            lblDPPayableUntil.Text = dtpStart.Value.AddMonths(Terms - 1).ToString("MMMM dd, yyyy")
        End If
    End Sub

    Private Sub GenerateLoanDiffSchedule()
        Dim LoanDiff As Decimal = 0
        Dim Monthly As Decimal = 0
        Dim Terms As Decimal = 0
        Dim RunningTotal As Decimal = 0
        Dim Amort As Decimal = 0
        If cbFinancingMode.Text = "Pagibig" Then
            If IsNumeric(txtPagibiLoanDiff.Text) Then LoanDiff = CDec(txtPagibiLoanDiff.Text)
            If IsNumeric(txtPagibigMonthly.Text) Then Monthly = CDec(txtPagibigMonthly.Text)
            Terms = nupPagibigTerms.Value
            If dgvPagibig.Columns.Count > 0 Then
                dgvPagibig.Rows.Clear()
                For i As Integer = 0 To Terms - 1
                    RunningTotal += Monthly
                    If RunningTotal > LoanDiff Then
                        Amort = Monthly - (RunningTotal - LoanDiff)
                    Else
                        Amort = Monthly
                    End If
                    dgvPagibig.Rows.Add(i + 1, dtpPagibigStart.Value.AddMonths(i).ToString("MM/dd/yyyy"), Amort.ToString("N2"))
                Next
                lblPagibigUntil.Text = dtpPagibigStart.Value.AddMonths(Terms - 1).ToString("MMMM dd, yyyy")
            End If
        ElseIf cbFinancingMode.Text = "Bank" Then
            If IsNumeric(txtBankLoanDiff.Text) Then LoanDiff = CDec(txtBankLoanDiff.Text)
            If IsNumeric(txtBankMonthly.Text) Then Monthly = CDec(txtBankMonthly.Text)
            Terms = nupBankTerms.Value
            If dgvBank.Columns.Count > 0 Then
                dgvBank.Rows.Clear()
                For i As Integer = 0 To Terms - 1
                    RunningTotal += Monthly
                    If RunningTotal > LoanDiff Then
                        Amort = Monthly - (RunningTotal - LoanDiff)
                    Else
                        Amort = Monthly
                    End If
                    dtpBankStart.Value = dtpStart.Value.AddMonths(nupTerm.Value).ToString("MM/dd/yyyy")
                    dgvBank.Rows.Add(i + 1, dtpBankStart.Value.AddMonths(i).ToString("MM/dd/yyyy"), Amort.ToString("N2"))
                Next
                lblBankUntil.Text = dtpBankStart.Value.AddMonths(Terms - 1).ToString("MMMM dd, yyyy")
            End If
        End If

    End Sub

    Private Function GetPeriod(BegDate As Date, EndDate As Date) As String
        Return BegDate.ToString("MMM d - ") & EndDate.ToString("MMM d, yyyy")
    End Function

    Private Sub PaymentDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim f As New frmReport_Display
        f.ShowDialog("RE_PaymentDetails", TransID)
        f.Dispose()
    End Sub


    Private Sub ExpiredContractsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim f As New frmReport_Display
        f.ShowDialog("RE_ExpiredContracts", Date.Today.Date)
        f.Dispose()
    End Sub

    Private Sub PastDueAccountsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim f As New frmReport_Display
        f.ShowDialog("RE_PastDue", Date.Today.Date)
        f.Dispose()
    End Sub

    Private Sub UnpaidDSTNotarialFeeToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim f As New frmReport_Display
        f.ShowDialog("RE_UnpaidCharges", Date.Today.Date)
        f.Dispose()
    End Sub



    Private Sub chkVAT_CheckedChanged(sender As Object, e As EventArgs) Handles chkVATable.CheckedChanged, chkVATInc.CheckedChanged, txtSelling.TextChanged, txtMisc.TextChanged, txtAddl.TextChanged
        Recompute()
    End Sub

    Private Sub Recompute()
        'Dim Price As Decimal = 0
        'Dim Selling As Decimal = 0
        'Dim Misc As Decimal = 0
        'Dim Addl As Decimal = 0

        'If IsNumeric(txtSelling.Text) Then Selling = txtSelling.Text
        'If IsNumeric(txtMisc.Text) Then Misc = txtMisc.Text
        'If IsNumeric(txtAddl.Text) Then Addl = txtAddl.Text
        'Price = Selling + Misc + Addl
        'If chkVATable.Checked Then
        '    If chkVATInc.Checked Then
        '        txtVAT.Text = ((Price / 1.12) * 0.12).ToString("N2")
        '        txtTCP.Text = (Price).ToString("N2")
        '    Else
        '        txtVAT.Text = ((Price) * 0.12).ToString("N2")
        '        txtTCP.Text = (Price + CDec(txtVAT.Text)).ToString("N2")
        '    End If
        'Else
        '    txtVAT.Text = "0.00"
        'End If

        ComputePayment()
    End Sub

    Private Sub nupTerm_ValueChanged(sender As Object, e As EventArgs) Handles nupTerm.ValueChanged, txtTotalDP.TextChanged, txtReserve.TextChanged
        ComputeMonthly()
        ComputeDPBalance()
    End Sub

    Private Sub ComputeMonthly()
        Dim Reserve As Decimal = 0
        Dim TotalDP As Decimal = 0
        Dim Terms As Decimal = 0
        Dim Monthly As Decimal = 0

        If IsNumeric(txtReserve.Text) Then Reserve = CDec(txtReserve.Text)
        If IsNumeric(txtTotalDP.Text) Then TotalDP = CDec(txtTotalDP.Text)
        Terms = nupTerm.Value

        If Terms > 0 Then
            Monthly = ((TotalDP - Reserve) / Terms)
            txtMonthly.Text = Monthly.ToString("N2")
            GenerateSchedule()
        End If
    End Sub

    Private Sub ComputeLoanDifference()
        Dim Loanable As Decimal = 0
        Dim ApproveLoanable As Decimal = 0
        Dim LoanDiff As Decimal = 0
        Dim Terms As Decimal = 0
        Dim Monthly As Decimal = 0

        If IsNumeric(txtLoanable.Text) Then Loanable = CDec(txtLoanable.Text)

        If cbFinancingMode.SelectedItem = "Pagibig" Then
            If IsNumeric(txtPagibigLoanable.Text) Then ApproveLoanable = CDec(txtPagibigLoanable.Text)
            If ApproveLoanable = 0 Then
                disableEvent = True
                ApproveLoanable = Loanable
                txtPagibigLoanable.Text = ApproveLoanable.ToString("N2")
                disableEvent = False
            End If
            LoanDiff = Loanable - ApproveLoanable
            txtPagibiLoanDiff.Text = LoanDiff.ToString("N2")

            Terms = nupPagibigTerms.Value
            If Terms > 0 Then
                Monthly = ((LoanDiff) / Terms)
                txtPagibigMonthly.Text = Monthly.ToString("N2")
                GenerateLoanDiffSchedule()
            End If
        ElseIf cbFinancingMode.SelectedItem = "Bank" Then
            If IsNumeric(txtBankLoanable.Text) Then ApproveLoanable = CDec(txtBankLoanable.Text)
            If ApproveLoanable = 0 Then
                disableEvent = True
                ApproveLoanable = Loanable
                txtBankLoanable.Text = ApproveLoanable.ToString("N2")
                disableEvent = False
            End If

            LoanDiff = Loanable - ApproveLoanable
            txtBankLoanDiff.Text = LoanDiff.ToString("N2")

            Terms = nupBankTerms.Value
            If Terms > 0 Then
                Monthly = ((LoanDiff) / Terms)
                txtBankMonthly.Text = Monthly.ToString("N2")
                GenerateLoanDiffSchedule()
            End If
        End If



    End Sub

    Private Sub dtpStart_ValueChanged(sender As Object, e As EventArgs) Handles dtpStart.ValueChanged, nupTerm.ValueChanged
        GenerateSchedule()
    End Sub

    Private Sub ReservationAgreementToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReservationAgreementToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("RE_Reservation", TransID)
        f.Dispose()
    End Sub

    Private Sub ContractToSellToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContractToSellToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("RE_CTS", TransID)
        f.Dispose()
    End Sub

    Private Sub NoticeOfUnpaidEquityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoticeOfUnpaidEquityToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("RE_NUE", TransID)
        f.Dispose()
    End Sub

    Private Sub NoticeOfDisapprovalForCAMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoticeOfDisapprovalForCAMToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("RE_NDC", TransID)
        f.Dispose()
    End Sub

    Private Sub WaiverToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WaiverToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("RE_Waiver", "")
        f.Dispose()
    End Sub

    Private Sub nupDPRate_ValueChanged(sender As Object, e As EventArgs) Handles nupDPRate.ValueChanged
        ComputeRequiredDP()
    End Sub

    Private Sub ComputeRequiredDP()
        Dim TCP As Decimal = 0
        Dim TotalDP As Decimal = 0
        If IsNumeric(txtSelling.Text) Then TCP = CDec(txtSelling.Text) + CDec(txtVAT.Text) - CDec(txtSellingCommission.Text)

        TotalDP = TCP * (nupDPRate.Value / 100.0)

        txtTotalDP.Text = TotalDP.ToString("N2")
        ComputeLoanable()
    End Sub

    Private Sub NoticeOfNonComplianceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoticeOfNonComplianceToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("RE_NC", TransID)
        f.Dispose()
    End Sub

    Private Sub NoticeOfCancellationForDocumentationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoticeOfCancellationForDocumentationToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("RE_NCD", TransID)
        f.Dispose()
    End Sub

    Private Sub CreditApprovalMemorandumFormToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreditApprovalMemorandumFormToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("RE_CAM", TransID)
        f.Dispose()
    End Sub

    Private Sub ListOfRequirementsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListOfRequirementsToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("RE_LOR", TransID)
        f.Dispose()
    End Sub

    Private Sub BuyersInformationSheetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BuyersInformationSheetToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("RE_BIS", TransID)
        f.Dispose()
    End Sub

    Private Sub AccountMonitoringReservedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AccountMonitoringReservedToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("RE_AMR", "")
        f.Dispose()
    End Sub

    Private Sub cbPaymentType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPaymentType.SelectedIndexChanged
        If disableEvent = False Then
            tcPayment.Visible = True
            HideTabPageAll()
            ShowTabPage(cbPaymentType.SelectedItem)
            If cbPaymentType.SelectedItem = "Cash Sales" Then
                tpCash.Enabled = True
                tcPayment.SelectedTab = tpCash
            ElseIf cbPaymentType.SelectedItem = "Deferred Cash" Then
                tpDeferred.Enabled = True
                tcPayment.SelectedTab = tpDeferred
            ElseIf cbPaymentType.SelectedItem = "Financing" Then
                tpFinancing.Enabled = True
                tcPayment.SelectedTab = tpFinancing
                If cbFinancingMode.Items.Count > 0 Then
                    If TransID = "" Then
                        'cbFinancingMode.SelectedIndex = 0
                        cbFinancingMode.SelectedItem = "Bank"

                    End If
                End If
            End If
            'GenerateEntry()
            ComputePayment()

        End If
    End Sub



    Private Sub ComputePayment()
        If cbPaymentType.SelectedItem = "Cash Sales" Then
            ComputeCashSales()
        ElseIf cbPaymentType.SelectedItem = "Deferred Cash" Then
            ComputeDeferredSales()
        ElseIf cbPaymentType.SelectedItem = "Financing" Then
            ComputeFinancing()
        End If
    End Sub

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

    Public Sub HideTabPageAll2()
        If tpOrder2 Is Nothing Then
            ' The first time the Hide method is called, save the original order of the TabPages
            For Each TabPageCurrent As TabPage In tcFinancing.TabPages
                tpOrder2.Add(TabPageCurrent.Name)
            Next
        End If
        For Each TabPageCurrent As TabPage In tcFinancing.TabPages
            Dim TabPageToHide As TabPage

            ' Get the TabPage object
            TabPageToHide = tcFinancing.TabPages(TabPageCurrent.Name)
            ' Add the TabPage to the internal List
            tpHidden2.Add(TabPageCurrent.Text, TabPageToHide)
            ' Remove the TabPage from the TabPages collection of the TabControl
            tcFinancing.TabPages.Remove(TabPageToHide)
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


        Dim TabPageToShow2 As TabPage
        ' Get the TabPage object
        TabPageToShow2 = tpHidden("Accounting Entries")
        ' Add the TabPage to the TabPages collection of the TabControl
        tcPayment.TabPages.Insert(GetTabPageInsertionPoint("Accounting Entries"), TabPageToShow2)
        ' Remove the TabPage from the internal List
        tpHidden.Remove("Accounting Entries")

    End Sub
    Public Sub ShowTabPage2(ByVal TabPageName As String)
        If tpHidden2.ContainsKey(TabPageName) Then
            Dim TabPageToShow As TabPage

            ' Get the TabPage object
            TabPageToShow = tpHidden2(TabPageName)
            ' Add the TabPage to the TabPages collection of the TabControl
            tcFinancing.TabPages.Insert(GetTabPageInsertionPoint2(TabPageName), TabPageToShow)
            ' Remove the TabPage from the internal List
            tpHidden2.Remove(TabPageName)
        End If
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
    Private Function GetTabPageInsertionPoint2(ByVal TabPageName As String) As Integer
        Dim TabPageIndex As Integer
        Dim TabPageCurrent As TabPage
        Dim TabNameIndex As Integer
        Dim TabNameCurrent As String

        For TabPageIndex = 0 To tcFinancing.TabPages.Count - 1
            TabPageCurrent = tcFinancing.TabPages(TabPageIndex)
            For TabNameIndex = TabPageIndex To tpOrder2.Count - 1
                TabNameCurrent = tpOrder2(TabNameIndex)
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

    Private Sub rbTCPWithin_CheckedChanged(sender As Object, e As EventArgs) Handles rbTCPWithin.CheckedChanged, rbTCPSpot.CheckedChanged, nupWithInDays.ValueChanged
        If disableEvent = False Then
            ComputeCashSales()
        End If
    End Sub


    Private Sub ComputeCashSales()
        SetCashSalesDiscount()
        ComputeCashDiscount()
        ComputeCashBalance()
    End Sub

    Private Sub ComputeDeferredSales()
        ComputeDeferredMonthly()
    End Sub

    Private Sub ComputeCashBalance()
        Dim Reserve As Decimal = 0
        Dim TCP As Decimal = 0
        Dim Discount As Decimal = 0
        Dim Balance As Decimal = 0
        If IsNumeric(txtSelling.Text) Then TCP = CDec(txtSelling.Text) + CDec(txtVAT.Text) - CDec(txtSellingCommission.Text)
        If IsNumeric(txtTCPDiscount.Text) Then Discount = CDec(txtTCPDiscount.Text)
        If IsNumeric(txtTCPReserve.Text) Then Reserve = CDec(txtTCPReserve.Text)

        Balance = TCP - Reserve - Discount

        txtTCPBalance.Text = Balance.ToString("N2")
    End Sub

    Private Sub SetCashSalesDiscount()
        disableEvent = True

        If rbTCPSpot.Checked Then
            nupWithInDays.Enabled = False
            txtTCPReserve.Enabled = False
            nupTCPDiscount.Value = 7
            lblCashPayableUntil.Text = dtpDocDate.Value.ToString("MMMM dd, yyyy")
        ElseIf rbTCPWithin.Checked Then
            'If cbTCPDays.SelectedIndex = -1 Then cbTCPDays.SelectedIndex = 0
            nupWithInDays.Enabled = True
            txtTCPReserve.Enabled = True
            lblCashPayableUntil.Text = dtpDocDate.Value.AddDays(nupWithInDays.Value).ToString("MMMM dd, yyyy")
            'If cbTCPDays.SelectedItem = 7 Then
            '    nupTCPDiscount.Value = 5
            'ElseIf cbTCPDays.SelectedItem = 30 Then
            '    nupTCPDiscount.Value = 3
            'End If
        End If
        disableEvent = False
    End Sub

    Private Sub nupTCPDiscount_ValueChanged(sender As Object, e As EventArgs) Handles nupTCPDiscount.ValueChanged
        If disableEvent = False Then
            ComputeCashDiscount()
            ComputeCashBalance()
        End If
    End Sub

    Private Sub ComputeCashDiscount()
        Dim TCP As Decimal = 0
        Dim Discount As Decimal = 0
        Dim DiscountRate As Decimal = 0

        If IsNumeric(txtSelling.Text) Then TCP = CDec(txtSelling.Text) + CDec(txtVAT.Text) - CDec(txtSellingCommission.Text)
        DiscountRate = nupTCPDiscount.Value / 100.0

        Discount = TCP * DiscountRate
        txtTCPDiscount.Text = Discount.ToString("N2")
        'ComputeCommission()
    End Sub

    Private Sub txtTCPReserve_TextChanged(sender As Object, e As EventArgs) Handles txtTCPReserve.TextChanged
        If disableEvent = False Then
            ComputeCashBalance()
        End If
    End Sub

    Private Sub txtTCPReserve_Leave(sender As Object, e As EventArgs) Handles txtTCPReserve.Leave, txtDeferredReserve.Leave, txtReserve.Leave, txtPagibigLoanable.Leave, txtBankLoanable.Leave, txtTotalDP.Leave
        If IsNumeric(sender.Text) Then
            sender.Text = CDec(sender.Text).ToString("N2")
        End If
    End Sub

    Private Sub ComputeDeferredMonthly()
        Dim TCP As Decimal = 0
        Dim Reserve As Decimal = 0
        Dim Monthly As Decimal = 0
        Dim Terms As Decimal = 1
        If IsNumeric(txtSelling.Text) Then TCP = CDec(txtSelling.Text) + CDec(txtVAT.Text) - CDec(txtSellingCommission.Text)
        If IsNumeric(txtDeferredReserve.Text) Then Reserve = CDec(txtDeferredReserve.Text)
        Terms = nupDeferredTerms.Value

        If Terms > 0 Then
            Monthly = (TCP - Reserve) / Terms

            txtDeferredMonthly.Text = Monthly.ToString("N2")

            GenerateDeferredSchedule()
        End If
    End Sub

    Private Sub txtDeferredTerms_ValueChanged(sender As Object, e As EventArgs) Handles nupDeferredTerms.ValueChanged, txtDeferredReserve.TextChanged
        If disableEvent = False Then
            ComputeDeferredMonthly()
        End If
    End Sub

    Private Sub dtpDeferredStart_ValueChanged(sender As Object, e As EventArgs) Handles dtpDeferredStart.ValueChanged, nupDeferredTerms.ValueChanged
        If disableEvent = False Then
            dtpDeferredEnd.Value = dtpDeferredStart.Value.AddMonths(nupDeferredTerms.Value - 1)
            GenerateDeferredSchedule()
        End If
    End Sub

    Private Sub GenerateDeferredSchedule()
        Dim TCP As Decimal = 0
        Dim Monthly As Decimal = 0
        Dim Terms As Decimal = 0
        Dim RunningTotal As Decimal = 0
        Dim Amort As Decimal = 0
        If IsNumeric(txtSelling.Text) Then TCP = CDec(txtSelling.Text) + CDec(txtVAT.Text) - CDec(txtSellingCommission.Text)
        If IsNumeric(txtDeferredMonthly.Text) Then Monthly = CDec(txtDeferredMonthly.Text)
        Terms = nupDeferredTerms.Value

        If dgvDeferredSched.Columns.Count > 0 Then
            dgvDeferredSched.Rows.Clear()
            For i As Integer = 0 To Terms - 1
                RunningTotal += Monthly
                If RunningTotal > TCP Then
                    Amort = Monthly - (RunningTotal - TCP)
                Else
                    Amort = Monthly
                End If
                dgvDeferredSched.Rows.Add(i + 1, dtpDeferredStart.Value.AddMonths(i).ToString("MM/dd/yyyy"), Amort.ToString("N2"))
            Next
        End If

    End Sub

    Private Sub txtBalance_TextChanged(sender As Object, e As EventArgs) Handles txtLoanable.TextChanged

    End Sub

    Private Sub ComputeLoanable()
        Dim TCP As Decimal = 0
        Dim TotalDP As Decimal = 0
        Dim Loanable As Decimal = 0

        If IsNumeric(txtSelling.Text) Then TCP = CDec(txtSelling.Text) + CDec(txtVAT.Text) - CDec(txtSellingCommission.Text)
        If IsNumeric(txtTotalDP.Text) Then TotalDP = CDec(txtTotalDP.Text)

        Loanable = TCP - TotalDP
        txtLoanable.Text = Loanable.ToString("N2")
        ComputeLoanDifference()
    End Sub

    Private Sub rbSpotDp_CheckedChanged(sender As Object, e As EventArgs) Handles rbSpotDp.CheckedChanged, rbCashWithin.CheckedChanged, rbTerm.CheckedChanged, nupDPDays.ValueChanged
        If disableEvent = False Then
            ComputeDP()
        End If
    End Sub

    Private Sub ComputeFinancing()
        ComputeRequiredDP()
        ComputeDP()
        ComputeLoanable()
        ComputeFinancingPayment()
    End Sub

    Private Sub ComputeDP()
        SetDPCashDiscount()
        ComputeDPDiscount()
        ComputeDPBalance()
    End Sub
    Private Sub SetDPCashDiscount()
        disableEvent = True

        If rbSpotDp.Checked Then
            nupDPDays.Enabled = False
            txtReserve.Enabled = False
            nupTerm.Visible = False
            txtMonthly.Visible = False
            dtpStart.Visible = False
            dgvDPSchedule.Visible = False
            Label21.Visible = False
            Label22.Visible = False
            Label11.Visible = False

            nupDiscountRate.Value = 7
            lblDPPayableUntil.Text = dtpDocDate.Value.ToString("MMMM dd, yyyy")
        ElseIf rbCashWithin.Checked Then
            'If cbDPDays.SelectedIndex = -1 Then cbDPDays.SelectedIndex = 0
            nupDPDays.Enabled = True
            txtReserve.Enabled = True
            nupTerm.Visible = False
            txtMonthly.Visible = False
            dtpStart.Visible = False
            dgvDPSchedule.Visible = False
            Label21.Visible = False
            Label22.Visible = False
            Label11.Visible = False

            lblDPPayableUntil.Text = dtpDocDate.Value.AddDays(nupDPDays.Value).ToString("MMMM dd, yyyy")
            'If cbDPDays.SelectedItem = 7 Then
            '    nupDiscountRate.Value = 5
            'ElseIf cbDPDays.SelectedItem = 30 Then
            '    nupDiscountRate.Value = 3
            'End If
        ElseIf rbTerm.Checked Then
            nupDPDays.Enabled = False
            txtReserve.Enabled = True
            nupTerm.Visible = True
            txtMonthly.Visible = True
            dtpStart.Visible = True
            dgvDPSchedule.Visible = True
            Label21.Visible = True
            Label22.Visible = True
            Label11.Visible = True

            nupDiscountRate.Value = 0
            lblDPPayableUntil.Text = dtpStart.Value.AddMonths(nupTerm.Value - 1).ToString("MMMM dd, yyyy")
        End If

        disableEvent = False
    End Sub


    Private Sub nupDiscountRate_ValueChanged(sender As Object, e As EventArgs) Handles nupDiscountRate.ValueChanged
        If disableEvent = False Then
            ComputeDPDiscount()
            ComputeDPBalance()
        End If
    End Sub

    Private Sub ComputeDPDiscount()
        Dim RequiredDP As Decimal = 0
        Dim Reserve As Decimal = 0
        Dim Discount As Decimal = 0
        Dim DiscountRate As Decimal = 0

        If IsNumeric(txtTotalDP.Text) Then RequiredDP = CDec(txtTotalDP.Text)
        If IsNumeric(txtReserve.Text) Then Reserve = CDec(txtReserve.Text)
        DiscountRate = nupDiscountRate.Value / 100.0

        Discount = (RequiredDP - Reserve) * DiscountRate
        txtDiscount.Text = Discount.ToString("N2")
        ComputeCommission()
    End Sub

    Private Sub ComputeDPBalance()
        Dim RequiredDP As Decimal = 0
        Dim Reserve As Decimal = 0
        Dim Discount As Decimal = 0
        Dim Balance As Decimal = 0
        If IsNumeric(txtTotalDP.Text) Then RequiredDP = CDec(txtTotalDP.Text)
        If IsNumeric(txtDiscount.Text) Then Discount = CDec(txtDiscount.Text)
        If IsNumeric(txtReserve.Text) Then Reserve = CDec(txtReserve.Text)

        Balance = RequiredDP - Reserve - Discount

        txtBalanceDP.Text = Balance.ToString("N2")
    End Sub

    Private Sub cbFinancingMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFinancingMode.SelectedIndexChanged
        If disableEvent = False Then
            tcFinancing.Visible = True
            HideTabPageAll2()
            ShowTabPage2(cbFinancingMode.SelectedItem)
            If cbFinancingMode.SelectedItem = "In-House" Then
                tcFinancing.SelectedTab = tpInhouse
            ElseIf cbFinancingMode.SelectedItem = "Pagibig" Then
                tcFinancing.SelectedTab = tpPagibig
            ElseIf cbFinancingMode.SelectedItem = "Bank" Then
                tcFinancing.SelectedTab = tpBank

            End If
            ComputeFinancingPayment()
        End If
    End Sub


    Private Sub ComputeFinancingPayment()

        If cbFinancingMode.SelectedItem = "In-House" Then
            ComputeInhouseMonthly()
        ElseIf cbFinancingMode.SelectedItem = "Pagibig" Then
            ComputeLoanDifference()
            GeneratePagibigComputation()
        ElseIf cbFinancingMode.SelectedItem = "Bank" Then
            ComputeLoanDifference()
            GenerateBankComputation()
        End If
    End Sub

    Private Sub ComputeInhouseTotalMonths()
        Dim TotalMonthlyPayments As Decimal = 0
        TotalMonthlyPayments = nupInhouseYear.Value * nupInhousePaymentPerYear.Value
        nupInhouseTotalMonths.Value = TotalMonthlyPayments
    End Sub
    Private Sub ComputeInhouseMonthly()
        Dim Loanable As Decimal = 0
        Dim Rate As Decimal = 0
        Dim TotalMonthlyPayments As Decimal = 0
        Dim Monthly As Decimal = 0

        If IsNumeric(txtLoanable.Text) Then Loanable = CDec(txtLoanable.Text)
        Rate = (nupInhouseRate.Value / 100.0) / 12
        TotalMonthlyPayments = nupInhouseTotalMonths.Value

        If Loanable > 0 And TotalMonthlyPayments > 0 Then
            Monthly = Pmt(Rate, TotalMonthlyPayments, Loanable * -1)
            txtInhouseMonthly.Text = Monthly.ToString("N2")
            GenerateInhouseSched()
        End If

    End Sub

    Private Sub GenerateInhouseSched()
        dgvInhouse.Rows.Clear()
        Dim Loanable As Decimal = 0
        Dim TotalMonthlyPayments As Decimal = 0
        Dim Rate As Decimal = 0
        Dim Principal As Decimal = 0
        Dim Interest As Decimal = 0
        TotalMonthlyPayments = nupInhouseTotalMonths.Value
        Rate = (nupInhouseRate.Value / 100.0) / 12
        If IsNumeric(txtLoanable.Text) Then Loanable = CDec(txtLoanable.Text)
        lblInhouseUntil.Text = dtpInhouseStart.Value.AddMonths(TotalMonthlyPayments - 1).ToString("MMMM dd, yyyy")
        For i As Integer = 1 To TotalMonthlyPayments
            Principal = PPmt(Rate, i, TotalMonthlyPayments, Loanable * -1)
            Interest = IPmt(Rate, i, TotalMonthlyPayments, Loanable * -1)
            dgvInhouse.Rows.Add({i, dtpInhouseStart.Value.AddMonths(i - 1).ToString("MM/dd/yyyy"), Interest.ToString("N2"), Principal.ToString("N2"), (Principal + Interest).ToString("N2")})
        Next
    End Sub

    Private Sub GenerateBankComputation()
        lvBankComputation.Items.Clear()

        Dim Loanable As Decimal = 0
        Dim Monthly As Decimal = 0
        Dim MonthlyRate As Decimal = 0
        Dim Terms() As Decimal = {20, 15, 10, 5}
        Dim TotalMonthlyPayments As Decimal = 0
        Dim NDI As Decimal = 0

        If IsNumeric(txtLoanable.Text) Then Loanable = CDec(txtLoanable.Text)
        MonthlyRate = (nupBankRate.Value / 100.0) / 12

        For Each term As Decimal In Terms
            TotalMonthlyPayments = term * 12
            Monthly = Pmt(MonthlyRate, TotalMonthlyPayments, Loanable * -1)
            NDI = Monthly / 0.4
            lvBankComputation.Items.Add(term)
            lvBankComputation.Items(lvBankComputation.Items.Count - 1).SubItems.Add(Monthly.ToString("N2"))
            lvBankComputation.Items(lvBankComputation.Items.Count - 1).SubItems.Add(NDI.ToString("N2"))
        Next
    End Sub

    Private Sub GeneratePagibigComputation()
        lvPagibig.Items.Clear()

        Dim Loanable As Decimal = 0
        Dim Monthly As Decimal = 0
        Dim MonthlyRate As Decimal = 0
        Dim Terms() As Decimal = {20, 15, 10, 5}
        Dim TotalMonthlyPayments As Decimal = 0
        Dim NDI As Decimal = 0

        If IsNumeric(txtLoanable.Text) Then Loanable = CDec(txtLoanable.Text)
        MonthlyRate = (nupPagibigRate.Value / 100.0) / 12

        For Each term As Decimal In Terms
            TotalMonthlyPayments = term * 12
            Monthly = Pmt(MonthlyRate, TotalMonthlyPayments, Loanable * -1)
            NDI = Monthly / 0.4
            lvPagibig.Items.Add(term)
            lvPagibig.Items(lvPagibig.Items.Count - 1).SubItems.Add(Monthly.ToString("N2"))
            lvPagibig.Items(lvPagibig.Items.Count - 1).SubItems.Add(NDI.ToString("N2"))
        Next
    End Sub


    Private Sub nupBankRate_ValueChanged(sender As Object, e As EventArgs) Handles nupBankRate.ValueChanged
        If disableEvent = False Then
            GenerateBankComputation()
        End If
    End Sub

    Private Sub dtpInhouseStart_ValueChanged(sender As Object, e As EventArgs) Handles dtpInhouseStart.ValueChanged
        If disableEvent = False Then
            GenerateInhouseSched()
        End If
    End Sub

    Private Sub nupInhouseRate_ValueChanged(sender As Object, e As EventArgs) Handles nupInhouseRate.ValueChanged, nupInhouseTotalMonths.ValueChanged
        If disableEvent = False Then
            ComputeInhouseMonthly()
        End If
    End Sub

    Private Sub nupInhouseYear_ValueChanged(sender As Object, e As EventArgs) Handles nupInhouseYear.ValueChanged, nupInhousePaymentPerYear.ValueChanged
        If disableEvent = False Then
            ComputeInhouseTotalMonths()
            ComputeInhouseMonthly()
        End If
    End Sub

    Private Sub nupPagibigRate_ValueChanged(sender As Object, e As EventArgs) Handles nupPagibigRate.ValueChanged
        If disableEvent = False Then
            GeneratePagibigComputation()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim f As New frmVCE_Search
        f.ShowDialog()
        txtSalesCode.Text = f.VCECode
        txtSalesName.Text = f.VCEName
        f.Dispose()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmVCE_Master.ShowDialog(txtSalesCode.Text)
    End Sub

    Private Sub txtSalesName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSalesName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.txtFilter.Text = txtSalesName.Text
            f.Type = "ALL"
            f.ShowDialog()
            txtSalesCode.Text = f.VCECode
            txtSalesName.Text = f.VCEName
            f.Dispose()
        End If
    End Sub

    Private Sub nupCommission_ValueChanged(sender As Object, e As EventArgs) Handles nupCommission.ValueChanged
        If disableEvent = False Then
            ComputeCommission()
        End If
    End Sub

    Private Sub ComputeCommission()
        Dim SellingPrice As Decimal = 0
        Dim NetSellingPrice As Decimal = 0
        Dim Discount As Decimal = 0
        Dim Rate As Decimal = 0
        Dim Commission As Decimal = 0

        If IsNumeric(txtSelling.Text) Then SellingPrice = CDec(txtSelling.Text)
        If cbPaymentType.SelectedItem = "Cash Sales" Then
            If IsNumeric(txtTCPDiscount.Text) Then Discount = CDec(txtTCPDiscount.Text)
        ElseIf cbPaymentType.SelectedItem = "Financing" Then
            If IsNumeric(txtDiscount.Text) Then Discount = CDec(txtDiscount.Text)
        End If


        If chkVATable.Checked AndAlso chkVATInc.Checked Then
            NetSellingPrice = (SellingPrice / 1.12) - Discount
        Else
            NetSellingPrice = SellingPrice - Discount
        End If

        Rate = (nupCommission.Value / 100.0)

        Commission = NetSellingPrice * Rate

        txtNetSelling.Text = NetSellingPrice.ToString("N2")
        txtCommission.Text = Commission.ToString("N2")
    End Sub

    Private Sub txtDiscount_TextChanged(sender As Object, e As EventArgs) Handles txtDiscount.TextChanged

    End Sub

    Private Sub txtTCPDiscount_TextChanged(sender As Object, e As EventArgs) Handles txtTCPDiscount.TextChanged

    End Sub

    Private Sub ListOfAvailableUnitsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListOfAvailableUnitsToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("RE_AU", "")
        f.Dispose()
    End Sub

    Private Sub nupBankTerms_ValueChanged(sender As Object, e As EventArgs) Handles nupBankTerms.ValueChanged, nupPagibigTerms.ValueChanged,
        cbFinancingMode.SelectedIndexChanged, txtPagibigLoanable.TextChanged, txtBankLoanable.TextChanged
        If disableEvent = False Then
            ComputeLoanDifference()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnViewLedger.Click
        Dim f As New frmRE_Ledger
        f.ShowDialog(TransID)
        f.Dispose()
    End Sub


    Public Function LoadSJID(ByVal BSNo As String) As String
        Dim query As String
        query = "SELECT TransID FROM tblSJ WHERE RE_ID = @BSID "
        SQL.FlushParams()
        SQL.AddParam("@BSID", BSNo)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read AndAlso Not IsDBNull(SQL.SQLDR("TransID")) Then
            Return SQL.SQLDR("TransID")
        Else
            Return 1
        End If
    End Function

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub InventoryPropertyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InventoryPropertyToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("Inventory_Property", TransID)
        f.Dispose()
    End Sub

    Private Sub txtVAT_TextChanged(sender As Object, e As EventArgs) Handles txtVAT.TextChanged
        If String.IsNullOrEmpty(txtVAT.Text) Then
            txtVAT.Text = "0.00"
        End If
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        For i As Integer = 1 To 576
            LoadRE(i)
        Next
    End Sub

    Public Function LoadSJNo(ByVal BSNo As String) As String
        Dim query As String
        query = "SELECT SJ_No AS TransID FROM tblSJ WHERE RE_ID = @BSID "
        SQL.FlushParams()
        SQL.AddParam("@BSID", BSNo)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read AndAlso Not IsDBNull(SQL.SQLDR("TransID")) Then
            Return SQL.SQLDR("TransID")
        Else
            Return 1
        End If
    End Function
    Private Sub txtDiscount_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDiscount.KeyDown

    End Sub

    Private Sub txtTCPDiscount_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTCPDiscount.KeyDown

    End Sub

    Private Sub txtDiscount_KeyUp(sender As Object, e As KeyEventArgs) Handles txtDiscount.KeyUp
        If disableEvent = False Then
            'ComputeDPDiscount()
            ComputeDPBalance()
        End If
    End Sub

    Private Sub txtTCPDiscount_KeyUp(sender As Object, e As KeyEventArgs) Handles txtTCPDiscount.KeyUp
        If disableEvent = False Then
            'ComputeCashDiscount()
            ComputeCashBalance()
        End If
    End Sub

    Private Sub txtSellingCommission_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSellingCommission.KeyUp
        'Dim CommAmount As String = txtCommission.Text
        'If CommAmount = "" Then
        '    CommAmount = 0.00
        'Else
        '    CommAmount = txtCommission.Text
        'End If
        If String.IsNullOrEmpty(txtSellingCommission.Text) Then
            txtSellingCommission.Text = "0.00"
        End If

        txtTCP.Text = CDec(CDec(txtSelling.Text) + CDec(txtVAT.Text) - CDec(txtSellingCommission.Text)).ToString("N2")
        'txtTCP.Text = CDec(CDec(txtSelling.Text) + CDec(txtVAT.Text) - CDec(CommAmount).ToString("N2"))
        Recompute()
    End Sub
End Class