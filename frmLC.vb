Public Class frmLC
    Dim TransID As String
    Dim LCNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "LC"
    Dim ColumnPK As String = "LC_No"
    Dim DBTable As String = "tblLC"
    Dim ColumnID As String = "TransID"
    Dim TransAuto As Boolean


    Public Overloads Function ShowDialog(ByVal docnumber As String) As Boolean
        TransID = docnumber
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmLC_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            If TransID <> "" Then
                LoadLC(TransID)
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
            EnableControl(False)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub btnLPM_Click(sender As Object, e As EventArgs) Handles btnLPM.Click
        frmLeaseProperty_Master.ShowDialog(txtPropCode.Text)
    End Sub


    Private Sub EnableControl(ByVal Value As Boolean)
        txtPropName.Enabled = Value
        txtVCEName.Enabled = Value
        dtpFrom.Enabled = Value
        txtRate.Enabled = Value
        chkVATable.Enabled = Value
        chkVATInc.Enabled = Value
        nupAdvance.Enabled = Value
        nupDeposit.Enabled = Value
        txtDST.Enabled = Value
        cbDST.Enabled = Value
        txtNF.Enabled = Value
        cbNF.Enabled = Value
        btnSearchVCE.Enabled = Value
        btnSearchProp.Enabled = Value
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Private Sub LoadLC(ByVal ID As String)
        Dim query As String
        query = " SELECT   TransID, LC_No, VCECode, PropCode, DateLC, LeaseMonths, PeriodFrom, PeriodTo, RatePerMonth, VATable, VATInc, " &
                "           AdvanceAmount, AdvanceMonths, DepositAmount, DepositMonths, DST_Amount, DST_ShoulderedBy, " &
                "           NF_Amount, NF_ShoulderedBy, Remarks, Status " &
                " FROM     tblLC " &
                " WHERE    TransId = '" & ID & "' " &
                " ORDER BY TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            disableEvent = True
            TransID = SQL.SQLDR("TransID").ToString
            LCNo = SQL.SQLDR("LC_No").ToString
            txtTransNum.Text = LCNo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtPropCode.Text = SQL.SQLDR("PropCode").ToString
            dtpDocDate.Value = SQL.SQLDR("DateLC")
            nupLeaseMonths.Value = SQL.SQLDR("LeaseMonths")
            dtpFrom.Value = SQL.SQLDR("PeriodFrom")
            dtpTo.Value = SQL.SQLDR("PeriodTo")
            txtRate.Text = CDec(SQL.SQLDR("RatePerMonth")).ToString("N2")
            chkVATable.Checked = SQL.SQLDR("VATable")
            chkVATInc.Checked = SQL.SQLDR("VATInc")
            txtAdvance.Text = CDec(SQL.SQLDR("AdvanceAmount")).ToString("N2")
            nupAdvance.Value = CDec(SQL.SQLDR("AdvanceMonths"))
            txtDeposit.Text = CDec(SQL.SQLDR("DepositAmount")).ToString("N2")
            nupDeposit.Value = CDec(SQL.SQLDR("DepositMonths"))
            txtDST.Text = CDec(SQL.SQLDR("DST_Amount")).ToString("N2")
            cbDST.SelectedItem = SQL.SQLDR("DST_ShoulderedBy").ToString
            txtNF.Text = CDec(SQL.SQLDR("NF_Amount")).ToString("N2")
            cbNF.SelectedItem = SQL.SQLDR("NF_ShoulderedBy").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            disableEvent = False

            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            txtPropName.Text = GetPropName(txtPropCode.Text)

            LoadSchedule(TransID)
            If dgvSchedule.Rows.Count = 0 Then
                GenerateSchedule()
                SaveSchedule(TransID)
            End If

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

            EnableControl(False)
        Else
            ClearText()
        End If
    End Sub

    Private Sub LoadSchedule(ID As Integer)
        Dim query As String
        query = " SELECT PaymentNo, PeriodFrom, PeriodTo, Amount FROM tblLC_Schedule WHERE TransID = @TransID ORDER BY PaymentNo "
        SQL.FlushParams()
        SQL.AddParam("@TransID", ID)
        SQL.ReadQuery(query)
        dgvSchedule.Rows.Clear()
        Dim ctr As Integer = 1
        While SQL.SQLDR.Read
            dgvSchedule.Rows.Add(SQL.SQLDR("PaymentNo"), GetPeriod(SQL.SQLDR("PeriodFrom"), SQL.SQLDR("PeriodTo")), SQL.SQLDR("Amount"), SQL.SQLDR("PeriodFrom"), SQL.SQLDR("PeriodTo"))
        End While

    End Sub
    Private Function GetAddress(SO_ID As Integer, VCECode As String) As String
        Dim query As String
        query = " SELECT ISNULL(DeliveryAddress,'') AS DeliveryAddress FROM viewProject_Details WHERE SO_ID = @SO_ID "
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
        txtRate.Text = "0.00"
        txtAdvance.Text = "0.00"
        txtDeposit.Text = "0.00"
        txtDST.Text = "0.00"
        txtNF.Text = "0.00"
        cbDST.SelectedIndex = 0
        cbNF.SelectedIndex = 0
        txtRemarks.Clear()
        txtStatus.Text = "Open"
        disableEvent = False
    End Sub

    Private Sub SaveLC()
        Try
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " &
                        " tblLC(TransID, LC_No, BranchCode, BusinessCode, VCECode, PropCode, DateLC, LeaseMonths, PeriodFrom, PeriodTo, " &
                        "       RatePerMonth, VATable, VATInc, AdvanceAmount, AdvanceMonths, DepositAmount, DepositMonths, " &
                        "       DST_Amount, DST_ShoulderedBy, NF_Amount, NF_ShoulderedBy, Remarks, Status, WhoCreated, DateCreated, TransAuto) " &
                        " VALUES (@TransID, @LC_No, @BranchCode, @BusinessCode, @VCECode, @PropCode, @DateLC, @LeaseMonths, @PeriodFrom, @PeriodTo, " &
                        "       @RatePerMonth, @VATable, @VATInc, @AdvanceAmount, @AdvanceMonths, @DepositAmount, @DepositMonths, " &
                        "       @DST_Amount, @DST_ShoulderedBy, @NF_Amount, @NF_ShoulderedBy, @Remarks, @Status, @WhoCreated, GETDATE(), @TransAuto) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@LC_No", LCNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@PropCode", txtPropCode.Text)
            SQL.AddParam("@DateLC", dtpDocDate.Value.Date)
            SQL.AddParam("@LeaseMonths", nupLeaseMonths.Value)
            SQL.AddParam("@PeriodFrom", dtpFrom.Value.Date)
            SQL.AddParam("@PeriodTo", dtpTo.Value.Date)
            SQL.AddParam("@RatePerMonth", CDec(txtRate.Text))
            SQL.AddParam("@VATable", chkVATable.Checked)
            SQL.AddParam("@VATInc", chkVATInc.Checked)
            SQL.AddParam("@AdvanceAmount", CDec(txtAdvance.Text))
            SQL.AddParam("@AdvanceMonths", nupAdvance.Value)
            SQL.AddParam("@DepositAmount", CDec(txtDeposit.Text))
            SQL.AddParam("@DepositMonths", nupDeposit.Value)
            SQL.AddParam("@DST_Amount", CDec(txtDST.Text))
            SQL.AddParam("@DST_ShoulderedBy", cbDST.SelectedItem)
            SQL.AddParam("@NF_Amount", CDec(txtNF.Text))
            SQL.AddParam("@NF_ShoulderedBy", cbNF.SelectedItem)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.AddParam("@TransAuto", TransAuto)
            SQL.ExecNonQuery(insertSQL)
            SaveSchedule(TransID)
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "LC_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub SaveSchedule(TransID As Integer)
        Dim SQL As New SQLControl
        Dim query As String
        query = "DELETE FROM tblLC_Schedule WHERE TransID = @TransID "
        SQL.FlushParams()
        SQL.AddParam("@TransID", TransID)
        SQL.ExecNonQuery(query)

        query = " INSERT INTO tblLC_Schedule(TransID, PaymentNo, PeriodFrom, PeriodTO, Amount) VALUES (@TransID, @PaymentNo,  @PeriodFrom, @PeriodTo, @Amount)"
        For Each row As DataGridViewRow In dgvSchedule.Rows
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@PaymentNo", row.Cells(chCount.Index).Value)
            SQL.AddParam("@PeriodFrom", row.Cells(chPeriodFrom.Index).Value)
            SQL.AddParam("@PeriodTO", row.Cells(chPeriodTo.Index).Value)
            SQL.AddParam("@Amount", CDec(row.Cells(chAmount.Index).Value))
            SQL.ExecNonQuery(query)
        Next
    End Sub

    Private Sub UpdateLC()
        Try

            activityStatus = True
            Dim updateSQL As String
            updateSQL = " UPDATE    tblLC " &
                        " SET       LC_No = @LC_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, VCECode = @VCECode, PropCode = @PropCode, " &
                        "           DateLC = @DateLC, LeaseMonths = @LeaseMonths, PeriodFrom = @PeriodFrom, PeriodTo = @PeriodTo, RatePerMonth = @RatePerMonth,  " &
                        "           VATable = @VATable, VATInc = @VATInc, AdvanceAmount = @AdvanceAmount, AdvanceMonths = @AdvanceMonths, " &
                        "           DepositAmount = @DepositAmount, DepositMonths = @DepositMonths, DST_Amount = @DST_Amount, DST_ShoulderedBy = @DST_ShoulderedBy, " &
                        "           NF_Amount = @NF_Amount, NF_ShoulderedBy = @NF_ShoulderedBy, Remarks = @Remarks, Status = @Status, " &
                        "           WhoModified = @WhoModified, DateModified = GETDATE(), TransAuto = @TransAuto " &
                        " WHERE     TransID = @TransID  "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@LC_No", LCNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@PropCode", txtPropCode.Text)
            SQL.AddParam("@DateLC", dtpDocDate.Value.Date)
            SQL.AddParam("@LeaseMonths", nupLeaseMonths.Value)
            SQL.AddParam("@PeriodFrom", dtpFrom.Value.Date)
            SQL.AddParam("@PeriodTo", dtpTo.Value.Date)
            SQL.AddParam("@RatePerMonth", CDec(txtRate.Text))
            SQL.AddParam("@VATable", chkVATable.Checked)
            SQL.AddParam("@VATInc", chkVATInc.Checked)
            SQL.AddParam("@AdvanceAmount", CDec(txtAdvance.Text))
            SQL.AddParam("@AdvanceMonths", nupAdvance.Value)
            SQL.AddParam("@DepositAmount", CDec(txtDeposit.Text))
            SQL.AddParam("@DepositMonths", nupDeposit.Value)
            SQL.AddParam("@DST_Amount", CDec(txtDST.Text))
            SQL.AddParam("@DST_ShoulderedBy", cbDST.SelectedItem)
            SQL.AddParam("@NF_Amount", CDec(txtNF.Text))
            SQL.AddParam("@NF_ShoulderedBy", cbNF.SelectedItem)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoModified", UserID)
            SQL.AddParam("@TransAuto", TransAuto)
            SQL.ExecNonQuery(updateSQL)
            SaveSchedule(TransID)
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "LC_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
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
        If Not AllowAccess("LC_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("LC")
            TransID = f.transID
            LoadLC(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("LC_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            LCNo = ""

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
            EnableControl(True)

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
        End If
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("LC_EDIT") Then
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
        If LCNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblLC  WHERE LC_No < '" & LCNo & "' ORDER BY LC_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadLC(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If LCNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblLC  WHERE LC_No > '" & LCNo & "' ORDER BY LC_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadLC(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub


    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        ' Toolstrip Buttons
        If LCNo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadLC(TransID)
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
        If Not AllowAccess("LC_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL, updateSQL As String
                        deleteSQL = " UPDATE  tblLC SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        LCNo = txtTransNum.Text
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
                        EnableControl(False)

                        LCNo = txtTransNum.Text
                        LoadLC(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "LC_No", LCNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub frmDR_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                    LCNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    txtTransNum.Text = LCNo
                    SaveLC()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    LCNo = txtTransNum.Text
                    LoadLC(TransID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    UpdateLC()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    LCNo = txtTransNum.Text
                    LoadLC(TransID)
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
        Dim f As New frmLPM_Search
        f.ShowDialog()
        txtPropCode.Text = f.PropCode
        txtPropName.Text = f.PropName
        LoadProperty(txtPropCode.Text)
        f.Dispose()
    End Sub

    Private Sub LoadProperty(PropCode As String)
        Dim query As String
        query = " SELECT RatePerMonth, VATable, VATInc FROM tblLeaseProperty WHERE PropCode = @PropCode "
        SQL.FlushParams()
        SQL.AddParam("@PropCode", PropCode)
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            txtRate.Text = CDec(SQL.SQLDR("RatePerMonth")).ToString("N2")
            chkVATable.Checked = SQL.SQLDR("VATable")
            chkVATInc.Checked = SQL.SQLDR("VATInc")
        End While
    End Sub


    Private Sub txtPropName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPropName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmProperty_Search
            f.cbFilter.SelectedItem = "Description"
            f.txtFilter.Text = txtPropName.Text
            f.ShowDialog()
            txtPropCode.Text = f.PropCode
            txtPropName.Text = f.PropName
            LoadProperty(txtPropCode.Text)
            f.Dispose()
        End If
    End Sub

    Private Sub nupAdvance_ValueChanged(sender As Object, e As EventArgs) Handles nupAdvance.ValueChanged
        If Not IsNumeric(txtRate.Text) Then txtRate.Text = "0.00"
        txtAdvance.Text = CDec(nupAdvance.Value * CDec(txtRate.Text)).ToString("N2")
    End Sub

    Private Sub nupDeposit_ValueChanged(sender As Object, e As EventArgs) Handles nupDeposit.ValueChanged
        If Not IsNumeric(txtRate.Text) Then txtRate.Text = "0.00"
        txtDeposit.Text = CDec(nupDeposit.Value * CDec(txtRate.Text)).ToString("N2")
    End Sub

    Private Sub txtRate_TextChanged(sender As Object, e As EventArgs) Handles txtRate.TextChanged
        If IsNumeric(txtRate.Text) Then
            txtAdvance.Text = CDec(nupAdvance.Value * CDec(txtRate.Text)).ToString("N2")
            txtDeposit.Text = CDec(nupDeposit.Value * CDec(txtRate.Text)).ToString("N2")
            GenerateSchedule()
        End If
    End Sub

    Private Sub nupLeaseMonths_ValueChanged(sender As Object, e As EventArgs) Handles nupLeaseMonths.ValueChanged, dtpFrom.ValueChanged
        If disableEvent = False Then
            dtpTo.Value = DateAdd(DateInterval.Month, nupLeaseMonths.Value, dtpFrom.Value)
            GenerateSchedule()
        End If
    End Sub

    Private Sub GenerateSchedule()
        dgvSchedule.Rows.Clear()
        If IsNumeric(txtRate.Text) Then
            For i As Integer = 1 To nupLeaseMonths.Value
                dgvSchedule.Rows.Add(i, GetPeriod(dtpFrom.Value.AddMonths(i - 1).AddDays(1), dtpFrom.Value.AddMonths(i)), CDec(txtRate.Text).ToString("N2"), dtpFrom.Value.AddMonths(i - 1).AddDays(1), dtpFrom.Value.AddMonths(i))
            Next
        End If
    End Sub


    Private Function GetPeriod(BegDate As Date, EndDate As Date) As String
        Return BegDate.ToString("MMM d - ") & EndDate.ToString("MMM d, yyyy")
    End Function

    Private Sub PaymentDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PaymentDetailsToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("LC_PaymentDetails", TransID)
        f.Dispose()
    End Sub


    Private Sub ExpiredContractsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExpiredContractsToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("LC_ExpiredContracts", Date.Today.Date)
        f.Dispose()
    End Sub

    Private Sub PastDueAccountsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PastDueAccountsToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("LC_PastDue", Date.Today.Date)
        f.Dispose()
    End Sub

    Private Sub UnpaidDSTNotarialFeeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnpaidDSTNotarialFeeToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("LC_UnpaidCharges", Date.Today.Date)
        f.Dispose()
    End Sub
End Class