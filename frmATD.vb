Public Class frmATD
    Dim TransID, RefID, JETransiD As String
    Dim ATDNo As String
    Dim disableEvent As Boolean = False
    Dim bankEvent As Boolean = False
    Dim ModuleID As String = "ATD"
    Dim ColumnPK As String = "ATD_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblATD"
    Dim TransAuto As Boolean
    Dim isTemp As Boolean = False
    Public dtData As New DataTable
    Public VCECode, VCEName, DedCode As String
    Public Amount As Decimal = 0
    Public DateDoc As Date
    Public isSaved As Boolean = False
    Public ATDNum As String = ""

    Public Overloads Function ShowDialog(ByVal dt As DataTable) As Boolean
        dtData = dt
        isTemp = True
        MyBase.ShowDialog()
        Return True
    End Function
    Public Overloads Function ShowDialog(ByVal DocNumber As String) As Boolean
        TransID = DocNumber
        MyBase.ShowDialog()
        Return True
    End Function
    Public Overloads Function ShowDialog(ByVal dt As DataTable, ByVal DocNumber As String) As Boolean
        dtData = dt
        isTemp = True
        ATDNum = DocNumber
        MyBase.ShowDialog()
        Return True
    End Function
    Private Sub frmATD_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetPayrollDatabase()
            Me.Text = "(" & database & ") - Authority to Deduct "
            TransAuto = GetTransSetup(ModuleID)
            LoadDeductionType()
            nupStartYear.Value = Date.Today.Year
            cbStartCutoff.SelectedIndex = 0
            If TransID <> "" Then
                If Not AllowAccess("ATD_VIEW") Then
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
                End If
            ElseIf isTemp Then
                If ATDNum = "" Then
                    tsbNew.PerformClick()
                    tsbClose.Enabled = False
                    tsbReports.Enabled = False
                    tsbPrint.Enabled = False
                    txtStatus.Text = "Temporary"
                    txtVCEName.Enabled = False
                    txtTotalAmount.Enabled = False
                    btnSearchVCE.Enabled = False
                    txtVCECode.Text = VCECode
                    txtVCEName.Text = VCEName
                    txtTotalAmount.Text = Amount.ToString("N2")
                    cbDedtype.SelectedItem = DedCode
                    dtpDocDate.Value = DateDoc
                    dtpDocDate.Enabled = False
                    cbCutoffPeriod.SelectedItem = "All"
                    chkRecurring.Visible = False
                    GetStartPaymentDate()
                    txt1stCutoff.Enabled = False
                    txt2ndCutoff.Enabled = False
                    ATDNo = GenerateTempID()
                    txtTransNum.Text = "Temp:" & ATDNo
                    txtNoOfPayday.Select()
                    txtNoOfPayday.Focus()
                Else
                    tsbSearch.Enabled = False
                    tsbNew.Enabled = False
                    tsbEdit.Enabled = False
                    tsbSave.Enabled = True
                    tsbCancel.Enabled = False
                    tsbPrevious.Enabled = False
                    tsbNext.Enabled = False
                    tsbExit.Enabled = False
                    tsbCopy.Enabled = True
                    txtTransNum.Enabled = False
                    tsbClose.Enabled = False
                    tsbReports.Enabled = False
                    tsbPrint.Enabled = False
                    txtVCEName.Enabled = False
                    txtTotalAmount.Enabled = False
                    btnSearchVCE.Enabled = False
                    dtpDocDate.Enabled = False
                    chkRecurring.Visible = False
                    txt1stCutoff.Enabled = False
                    txt2ndCutoff.Enabled = False
                    LoadATDfromDT(dtData, DedCode)
                    txtNoOfPayday.Select()
                    txtNoOfPayday.Focus()
                End If
                
            Else

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
            End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try

    End Sub
    Private Sub EnableControl(ByVal Value As Boolean)

        dtpDocDate.Enabled = Value
        txtVCEName.Enabled = Value
        txtCustomerName.Enabled = Value
        btnSearchVCE.Enabled = Value
        txt1stCutoff.Enabled = Value
        txt2ndCutoff.Enabled = Value
        txtNoOfPayday.Enabled = Value
        txtTotalAmount.Enabled = Value
        nupStartYear.Enabled = Value
        cbStartCutoff.Enabled = Value
        cbCalcMethod.Enabled = Value
        cbCutoffPeriod.Enabled = Value
        cbATDType.Enabled = Value
        cbDedtype.Enabled = Value
        cbStartMonth.Enabled = Value
        chkRecurring.Enabled = Value
        txtRemarks.Enabled = Value
        txtManual.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Private Sub LoadDeductionType()
        Dim query As String
        query = " SELECT Description FROM tblDeduction_Type WHERE Status ='Active' AND System_Default = 0 ORDER BY Description"
        cbDedtype.Items.Clear()
        SQL_RUBY.ReadQuery(query)
        While SQL_RUBY.SQLDR.Read
            cbDedtype.Items.Add(SQL_RUBY.SQLDR("Description").ToString)
        End While
    End Sub

    Private Sub cbDedCalc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCalcMethod.SelectedIndexChanged
        If cbCalcMethod.SelectedItem = "Variable Amount" Then
            cbCutoffPeriod.SelectedItem = "All"
            lblDed1st.Text = "1st Cutoff"
            lblDed1st.Location = New Point(418, lblDed1st.Top)
            txt2ndCutoff.Visible = True
            lblDed2nd.Visible = True
        Else
            txt2ndCutoff.Visible = False
            lblDed2nd.Visible = False
            lblDed1st.Location = New Point(414, lblDed1st.Top)
            lblDed1st.Text = "Per Cutoff"
        End If
    End Sub

    Private Sub txtDedTotal_TextChanged(sender As Object, e As EventArgs) Handles txtTotalAmount.TextChanged
        If IsNumeric(txtNoOfPayday.Text) AndAlso CDec(txtNoOfPayday.Text) > 0 Then
            If IsNumeric(txtTotalAmount.Text) AndAlso CDec(txtTotalAmount.Text) > 0 Then
                txt1stCutoff.Text = CDec(CDec(txtTotalAmount.Text) / CDec(txtNoOfPayday.Text)).ToString("N2")
            End If
        End If
    End Sub

    Private Sub txtDedCutoff_TextChanged(sender As Object, e As EventArgs) Handles txtNoOfPayday.TextChanged
        If IsNumeric(txtNoOfPayday.Text) AndAlso CDec(txtNoOfPayday.Text) > 0 Then
            If IsNumeric(txtTotalAmount.Text) AndAlso CDec(txtTotalAmount.Text) > 0 Then
                txt1stCutoff.Text = CDec(CDec(txtTotalAmount.Text) / CDec(txtNoOfPayday.Text)).ToString("N2")
            Else
                txtTotalAmount.Text = CDec(CDec(txt1stCutoff.Text) / CDec(txtNoOfPayday.Text)).ToString("N2")
            End If
        End If
    End Sub

    Private Sub LoadATD(ByVal ID As String)
        Dim query As String
        query = " SELECT    tblATD.TransID, tblATD.ATD_No, tblATD.DateATD, tblATD.VCECode, VCEName,MRIS_Ref, " & _
                "           tblATD_Details.Ledger_Code, tblATD_Details.Calc_Method, tblATD_Details.Cutoff, " & _
                "           tblATD_Details.Amount_1st, tblATD_Details.Amount_2nd, tblATD_Details.Total_Amount, " & _
                "           tblATD_Details.No_of_Payday, tblATD_Details.Start_Date, tblATD_Details.Recurring, " & _
                "           tblATD.Remarks, tblATD.Status, tblATD_Details.ATD_Form_No, " & _
                "           BranchCode, BusinessCode, tblATD.CustomerCode, tblATD.ATDType " & _
                " FROM      tblATD INNER JOIN tblATD_Details " & _
                " ON        tblATD.TransID = tblATD_Details.TransID " & _
                "           LEFT JOIN viewVCE_Master " & _
                " ON        tblATD.VCECode = viewVCE_Master.VCECode " & _
                " WHERE     tblATD.TransID = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Dim code As String = ""
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            ATDNo = SQL.SQLDR("ATD_No").ToString
            txtTransNum.Text = ATDNo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
            txtCustomerCode.Text = SQL.SQLDR("CustomerCode").ToString
            disableEvent = True
            dtpDocDate.Value = SQL.SQLDR("DateATD")
            code = SQL.SQLDR("Ledger_Code").ToString
            cbATDType.SelectedItem = SQL.SQLDR("ATDType").ToString
            cbCalcMethod.SelectedItem = SQL.SQLDR("Calc_Method").ToString
            cbCutoffPeriod.SelectedItem = SQL.SQLDR("Cutoff").ToString
            txt1stCutoff.Text = CDec(SQL.SQLDR("Amount_1st")).ToString("N2")
            txt2ndCutoff.Text = CDec(SQL.SQLDR("Amount_2nd")).ToString("N2")
            txtTotalAmount.Text = CDec(SQL.SQLDR("Total_Amount")).ToString("N2")
            txtNoOfPayday.Text = CInt(SQL.SQLDR("No_of_Payday"))
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            chkRecurring.Checked = SQL.SQLDR("Recurring")
            txtStatus.Text = SQL.SQLDR("Status").ToString
            dtpPaydate.Value = SQL.SQLDR("Start_Date").ToString
            txtManual.Text = SQL.SQLDR("ATD_Form_No").ToString
            cbStartCutoff.SelectedItem = SQL.SQLDR("Cutoff").ToString
            LoadStartDate(txtVCECode.Text, SQL.SQLDR("Start_Date").ToString)
            cbDedtype.SelectedItem = GetLedgerName(code)
            txtCustomerName.Text = GetVCEName(txtCustomerCode.Text)
            disableEvent = False



            ' TOOLSTRIP BUTTONS
            tsbCancel.Text = "Cancel"
            If txtStatus.Text = "Cancelled" Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            ElseIf txtStatus.Text = "Posted" Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            Else
                tsbEdit.Enabled = True
                tsbCancel.Enabled = True
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
            If dtpDocDate.Value < GetMaxPEC() Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            End If
            EnableControl(False)
        Else
            ClearText()
        End If
    End Sub

    Private Sub LoadATDfromDT(ByVal DT As DataTable, Dedcode As String)
        For Each row As DataRow In DT.Rows
            If row.Item("ATD_No").ToString = ATDNum AndAlso row.Item("Ledger_Code").ToString = Dedcode Then
                dtpDocDate.MinDate = "01-01-1900"
                TransID = row.Item("ATDID")
                ATDNo = row.Item("ATD_No")
                txtTransNum.Text = ATDNo
                txtVCECode.Text = row.Item("VCECode")
                txtVCEName.Text = GetVCEName(txtVCECode.Text)
                disableEvent = True
                cbCalcMethod.SelectedItem = row.Item("Calc_Method")
                cbCutoffPeriod.SelectedItem = row.Item("Cutoff")
                txt1stCutoff.Text = CDec(row.Item("Amount_1st")).ToString("N2")
                txt2ndCutoff.Text = CDec(row.Item("Amount_2nd")).ToString("N2")
                txtTotalAmount.Text = CDec(row.Item("Total_Amount")).ToString("N2")
                txtNoOfPayday.Text = CInt(row.Item("No_of_Payday"))
                txtRemarks.Text = row.Item("Remarks").ToString
                chkRecurring.Checked = row.Item("Recurring")
                dtpPaydate.Value = row.Item("Start_Date")
                txtManual.Text = row.Item("ATD_Form_No")
                txtStatus.Text = "Posted"
                LoadStartDate(txtVCECode.Text, row.Item("Start_Date").ToString)
                cbDedtype.SelectedItem = GetLedgerName(row.Item("Ledger_Code"))
                disableEvent = False
                Exit For
            End If
        Next
    End Sub
    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        Try
            If txtVCECode.Text = "" Then
                Msg("Please enter Employee Name!", MsgBoxStyle.Exclamation)
            ElseIf txtCustomerCode.Text = "" Then
                Msg("Please enter Customer Name!", MsgBoxStyle.Exclamation)
            ElseIf cbATDType.SelectedIndex = -1 Then
                Msg("Please select ATD Type!", MsgBoxStyle.Exclamation)
            ElseIf cbStartCutoff.SelectedIndex = -1 Then
                Msg("Please select payment starting cutoff!", MsgBoxStyle.Exclamation)
            ElseIf TransAuto = False And txtTransNum.Text = "" Then
                MsgBox("Please Enter ATD Number!", MsgBoxStyle.Exclamation)
            ElseIf TransAuto = False AndAlso IfExist(txtTransNum.Text) And TransID = "" Then
                MsgBox("ATD" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
            ElseIf cbDedtype.SelectedIndex = -1 Then
                MsgBox("Please sleect Deduction Type!", MsgBoxStyle.Exclamation)
            ElseIf Not IsNumeric(txtNoOfPayday.Text) AndAlso CInt(txtNoOfPayday.Text) <= 0 Then
                MsgBox("Please input valid no. of SD paydays", MsgBoxStyle.Exclamation)
            ElseIf Not IsNumeric(txtTotalAmount.Text) AndAlso CDec(txtTotalAmount.Text) <= 0 Then
                MsgBox("Please input total amount", MsgBoxStyle.Exclamation)
            ElseIf cbStartCutoff.SelectedIndex = -1 Then
                MsgBox("Please select SD Start date!", MsgBoxStyle.Exclamation)
            ElseIf TransID = "" Then
                If isTemp Then
                    ATDNo = txtTransNum.Text
                    SaveTempATD()
                    isSaved = True
                    Me.Close()
                Else
                    If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                        TransID = GenerateTransID(ColumnID, DBTable)
                        If TransAuto Then
                            ATDNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                        Else
                            ATDNo = txtTransNum.Text
                        End If
                        txtTransNum.Text = ATDNo
                        SaveATD()
                        Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                        LoadATD(TransID)
                    End If
                End If
            Else
                If isTemp Then
                    UpdateATDtoDT()
                    isSaved = True
                    Me.Close()
                Else
                    If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                        If ATDNo = txtTransNum.Text Then
                            ATDNo = txtTransNum.Text
                            UpdateATD()
                            Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                            ATDNo = txtTransNum.Text
                            LoadATD(TransID)
                        Else
                            If Not IfExist(txtTransNum.Text) Then
                                ATDNo = txtTransNum.Text
                                UpdateATD()
                                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                                ATDNo = txtTransNum.Text
                                LoadATD(TransID)
                            Else
                                MsgBox("ATD" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub
    Public Function GenerateTempID(Optional ByVal ID As String = "") As Integer
        Dim TransID As String = ID
        Dim exist As Boolean = False
        ' GENERATE TRANS ID 
        If dtData.Rows.Count > 0 Then
            For Each row As DataRow In dtData.Rows
                If TransID = "" Then
                    TransID = row.Item("ATD_No").ToString.Replace("Temp:", "")
                Else
                    TransID = TransID.ToString.Replace("Temp:", "")
                End If
                If "Temp:" & (TransID + 1).ToString = row.Item("ATD_No") Then
                    exist = True
                    TransID = row.Item("ATD_No")
                End If
            Next
            If exist Then
                Return GenerateTempID(TransID)
            Else
                Return (TransID + 1).ToString.Replace("Temp:", "")
            End If
        Else
            Dim query As String
            query = " SELECT CAST(ISNULL(MAX(CAST(REPLACE(ATD_No,'Temp:','') AS Int)),0) + 1 AS nvarchar)  AS TransID" & _
                    " FROM tblATD WHERE Status ='Temporary'"
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID")
            Else
                TransID = 0
            End If
        End If

        Return TransID
    End Function
    Private Function IfExist(ByVal ID As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblATD WHERE ATD_No ='" & ID & "'  "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub SaveATD()
        Dim SQL1 As New SQLControl
        SQL1.BeginTransaction()
        Try
            Dim insertSQL As String
            activityStatus = True

            Dim code As String = GetCode(cbDedtype.SelectedItem)
            insertSQL = " INSERT INTO " & _
                                  " tblATD (TransID, ATD_No, VCECode, DateATD, Remarks, Status, DateCreated, WhoCreated, TransAuto, BranchCode, BusinessCode, CustomerCode, ATDType ) " & _
                                  " VALUES (@TransID, @ATD_No, @VCECode, @DateATD,  @Remarks, @Status,  " & _
                                  "          GETDATE(), @WhoCreated, @TransAuto, @BranchCode, @BusinessCode, @CustomerCode, @ATDType)"

                SQL1.FlushParams()
            SQL1.AddParam("@TransID", TransID)
            SQL1.AddParam("@ATD_No", ATDNo)
            SQL1.AddParam("@VCECode", txtVCECode.Text)
            SQL1.AddParam("@DateATD", dtpDocDate.Value.Date)
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@Status", "Active")
            SQL1.AddParam("@TransAuto", TransAuto)
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@CustomerCode", txtCustomerCode.Text)
            SQL1.AddParam("@ATDType", cbATDType.SelectedItem)
            SQL1.ExecNonQuery(insertSQL)

            insertSQL = " INSERT INTO " & _
                          " tblATD_Details (TransID, Ledger_Code, Calc_Method, Cutoff, Amount_1st, Amount_2nd, " & _
                          " Total_Amount, No_of_Payday, Start_Date, Recurring, ATD_Form_No ) " & _
                          " VALUES (@TransID, @Ledger_Code, @Calc_Method, @Cutoff, @Amount_1st, @Amount_2nd, " & _
                          "         @Total_Amount, @No_of_Payday, @Start_Date, @Recurring, @ATD_Form_No)"

            SQL1.FlushParams()
            SQL1.AddParam("@TransID", TransID)
            SQL1.AddParam("@Ledger_Code", code)
            SQL1.AddParam("@Calc_Method", cbCalcMethod.SelectedItem)
            SQL1.AddParam("@Cutoff", cbCutoffPeriod.SelectedItem)
            SQL1.AddParam("@Amount_1st", CDec(txt1stCutoff.Text))
            SQL1.AddParam("@Amount_2nd", CDec(txt2ndCutoff.Text))
            SQL1.AddParam("@Total_Amount", CDec(txtTotalAmount.Text))
            SQL1.AddParam("@No_of_Payday", CInt(txtNoOfPayday.Text))
            SQL1.AddParam("@Start_Date", dtpPaydate.Value.Date)
            SQL1.AddParam("@Recurring", chkRecurring.Checked)
            SQL1.AddParam("@ATD_Form_No", txtManual.Text)
            SQL1.ExecNonQuery(insertSQL)
            SQL1.Commit()
        Catch ex As Exception
            SQL1.Rollback()
            TransID = ""
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "ATD_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL1.FlushParams()
        End Try
    End Sub


    Private Sub SaveTempATD()
        activityStatus = True
        If dtData.Columns.Count = 0 Then
            dtData.Columns.Add("ATDID", GetType(Integer))
            dtData.Columns.Add("ATD_No", GetType(String))
            dtData.Columns.Add("VCECode", GetType(String))
            dtData.Columns.Add("Ledger_Code", GetType(String))
            dtData.Columns.Add("Calc_Method", GetType(String))
            dtData.Columns.Add("Cutoff", GetType(String))
            dtData.Columns.Add("Amount_1st", GetType(Decimal))
            dtData.Columns.Add("Amount_2nd", GetType(Decimal))
            dtData.Columns.Add("Total_Amount", GetType(Decimal))
            dtData.Columns.Add("No_of_Payday", GetType(Integer))
            dtData.Columns.Add("Start_Date", GetType(Date))
            dtData.Columns.Add("Recurring", GetType(Boolean))
            dtData.Columns.Add("Remarks", GetType(String))
            dtData.Columns.Add("ATD_Form_No", GetType(String))
        End If


        Dim code As String = GetCode(cbDedtype.SelectedItem)
        dtData.Rows.Add()
        dtData.Rows(dtData.Rows.Count - 1).Item("ATDID") = dtData.Rows.Count - 1
        dtData.Rows(dtData.Rows.Count - 1).Item("ATD_No") = ATDNo
        dtData.Rows(dtData.Rows.Count - 1).Item("VCECode") = txtVCECode.Text
        dtData.Rows(dtData.Rows.Count - 1).Item("Ledger_Code") = code
        dtData.Rows(dtData.Rows.Count - 1).Item("Calc_Method") = cbCalcMethod.SelectedItem
        dtData.Rows(dtData.Rows.Count - 1).Item("Cutoff") = cbCutoffPeriod.SelectedItem
        dtData.Rows(dtData.Rows.Count - 1).Item("Amount_1st") = IIf(IsNumeric(txt1stCutoff.Text), txt1stCutoff.Text, 0)
        dtData.Rows(dtData.Rows.Count - 1).Item("Amount_2nd") = IIf(IsNumeric(txt2ndCutoff.Text), txt2ndCutoff.Text, 0)
        dtData.Rows(dtData.Rows.Count - 1).Item("Total_Amount") = IIf(IsNumeric(txtTotalAmount.Text), txtTotalAmount.Text, 0)
        dtData.Rows(dtData.Rows.Count - 1).Item("No_of_Payday") = IIf(IsNumeric(txtNoOfPayday.Text), txtNoOfPayday.Text, 0)
        dtData.Rows(dtData.Rows.Count - 1).Item("Start_Date") = dtpPaydate.Value.Date
        dtData.Rows(dtData.Rows.Count - 1).Item("Recurring") = chkRecurring.Checked
        dtData.Rows(dtData.Rows.Count - 1).Item("Remarks") = txtRemarks.Text
        dtData.Rows(dtData.Rows.Count - 1).Item("ATD_Form_No") = txtManual.Text
    End Sub
    Private Sub UpdateATDtoDT()
        For Each row As DataRow In dtData.Rows
            If row.Item("ATD_No").ToString = ATDNo AndAlso row.Item("Ledger_Code").ToString = DedCode Then
                Dim code As String = GetCode(cbDedtype.SelectedItem)
                row.Item("ATD_No") = ATDNo
                row.Item("VCECode") = txtVCECode.Text
                row.Item("Ledger_Code") = code
                row.Item("Calc_Method") = cbCalcMethod.SelectedItem
                row.Item("Cutoff") = cbCutoffPeriod.SelectedItem
                row.Item("Amount_1st") = IIf(IsNumeric(txt1stCutoff.Text), txt1stCutoff.Text, 0)
                row.Item("Amount_2nd") = IIf(IsNumeric(txt2ndCutoff.Text), txt2ndCutoff.Text, 0)
                row.Item("Total_Amount") = IIf(IsNumeric(txtTotalAmount.Text), txtTotalAmount.Text, 0)
                row.Item("No_of_Payday") = IIf(IsNumeric(txtNoOfPayday.Text), txtNoOfPayday.Text, 0)
                row.Item("Start_Date") = dtpPaydate.Value.Date
                row.Item("Recurring") = chkRecurring.Checked
                row.Item("Remarks") = txtRemarks.Text
                row.Item("ATD_Form_No") = txtManual.Text
                Exit For
            End If
        Next
        
    End Sub
    Private Sub UpdateATD()
        Dim SQL1 As New SQLControl
        SQL1.BeginTransaction()
        Try
            Dim updateSQL As String
            activityStatus = True

            Dim code As String = GetCode(cbDedtype.SelectedItem)
            updateSQL = " UPDATE    tblATD " & _
                        " SET       ATD_No = @ATD_No, VCECode = @VCECode, DateATD = @DateATD, " & _
                        "           Remarks = @Remarks, Status = @Status, " & _
                        "           DateModified = GETDATE(), WhoModified = @WhoModified, " & _
                        "           TransAuto = @TransAuto, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " & _
                        "           CustomerCode = @CustomerCode, ATDType = @ATDType " & _
                        " WHERE     TransID = @TransID"
            SQL1.FlushParams()
            SQL1.AddParam("@TransID", TransID)
            SQL1.AddParam("@ATD_No", ATDNo)
            SQL1.AddParam("@VCECode", txtVCECode.Text)
            SQL1.AddParam("@DateATD", dtpDocDate.Value.Date)
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@Status", txtStatus.Text)
            SQL1.AddParam("@TransAuto", TransAuto)
            SQL1.AddParam("@WhoModified", UserID)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@CustomerCode", txtCustomerCode.Text)
            SQL1.AddParam("@ATDType", cbATDType.SelectedItem)
            SQL1.ExecNonQuery(updateSQL)

            updateSQL = " UPDATE    tblATD_Details " & _
                        " SET       Ledger_Code = @Ledger_Code, " & _
                        "           Calc_Method = @Calc_Method, Cutoff = @Cutoff, Amount_1st = @Amount_1st, Amount_2nd = @Amount_2nd, " & _
                        "           Total_Amount = @Total_Amount, No_of_Payday = @No_of_Payday, Start_Date = @Start_Date, " & _
                        "           Recurring = @Recurring, ATD_Form_No = @ATD_Form_No " & _
                        " WHERE     TransID = @TransID"
            SQL1.FlushParams()
            SQL1.AddParam("@TransID", TransID)
            SQL1.AddParam("@Ledger_Code", code)
            SQL1.AddParam("@Calc_Method", cbCalcMethod.SelectedItem)
            SQL1.AddParam("@Cutoff", cbStartCutoff.SelectedItem)
            SQL1.AddParam("@Amount_1st", CDec(txt1stCutoff.Text))
            SQL1.AddParam("@Amount_2nd", CDec(txt2ndCutoff.Text))
            SQL1.AddParam("@Total_Amount", CDec(txtTotalAmount.Text))
            SQL1.AddParam("@No_of_Payday", CInt(txtNoOfPayday.Text))
            SQL1.AddParam("@Start_Date", dtpPaydate.Value.Date)
            SQL1.AddParam("@Recurring", chkRecurring.Checked)
            SQL1.AddParam("@ATD_Form_No", txtManual.Text)
            SQL1.ExecNonQuery(updateSQL)
        
            SQL1.Commit()
        Catch ex As Exception
            SQL1.Rollback()
            TransID = ""
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "ATD_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL1.FlushParams()
        End Try
    End Sub

    Private Function GetCode(ByVal Title As String) As String
        Dim query As String
        query = " SELECT Ledger_Code FROM RUBY_Epeople.dbo.viewLedger_Type " & _
                " WHERE  Description = @Description AND Status ='Active' AND Category ='Deduction'"
        SQL.FlushParams()
        SQL.AddParam("@Description", Title)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("Ledger_Code").ToString
        Else
            Return ""
        End If
    End Function

    Private Function GetLedgerName(ByVal Code As String) As String
        Dim query As String
        query = " SELECT Description FROM RUBY_Epeople.dbo.viewLedger_Type " & _
                " WHERE  Ledger_Code ='" & Code & "' AND Status ='Active' AND Category ='Deduction'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("Description").ToString
        Else
            Return ""
        End If
    End Function
    Private Function LoadPayDate(ByVal EmpID As String) As Date
        Dim query As String
        query = " SELECT Paydate FROM RUBY_Epeople.dbo.tblPayroll_Period " & _
                " WHERE  PayrollMonth = " & cbStartMonth.SelectedIndex + 1 & " " & _
                " AND    PayrollYear = " & nupStartYear.Value & " " & _
                " AND    Cutoff_Identity='" & cbStartCutoff.SelectedItem & "' " & _
                " AND    Period_ID = (SELECT Period_ID FROM RUBY_Epeople.dbo.tblOrg_CostHeader WHERE OrgCost_ID =(SELECT OrgCost_ID FROM RUBY_Epeople.dbo.viewEmployee_Info WHERE Emp_ID ='" & EmpID & "'))"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("Paydate")
        Else
            Return "01-01-1900"
        End If
    End Function
    Private Sub LoadStartDate(ByVal EmpID As String, Paydate As Date)
        Dim query As String
        query = " SELECT PayrollMonth, PayrollYear, Cutoff_Identity FROM RUBY_Epeople.dbo.tblPayroll_Period " & _
                " WHERE  Paydate = '" & Paydate & "' " & _
                " AND    Period_ID = (SELECT Period_ID FROM RUBY_Epeople.dbo.tblOrg_CostHeader WHERE OrgCost_ID =(SELECT OrgCost_ID FROM RUBY_Epeople.dbo.viewEmployee_Info WHERE Emp_ID ='" & EmpID & "'))"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            disableEvent = True
            cbStartCutoff.SelectedItem = SQL.SQLDR("Cutoff_Identity")
            cbStartMonth.SelectedIndex = SQL.SQLDR("PayrollMonth") - 1
            nupStartYear.Value = SQL.SQLDR("PayrollYear")
            disableEvent = False
        End If
    End Sub

    Private Sub tsbNew_Click(sender As Object, e As EventArgs) Handles tsbNew.Click
        If Not AllowAccess("ATD_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            ATDNo = ""

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
            txtStatus.Text = "Open"
            EnableControl(True)

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
            txtVCEName.Select()
        End If
    End Sub

    Private Sub ClearText()
        txtCustomerCode.Text = ""
        txtCustomerName.Text = ""
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        txt1stCutoff.Text = "0.00"
        txt2ndCutoff.Text = "0.00"
        txtNoOfPayday.Text = 1
        txtTotalAmount.Text = "0.00"
        chkRecurring.Checked = 0
        cbATDType.SelectedIndex = 0
        cbStartCutoff.SelectedIndex = -1
        cbCalcMethod.SelectedIndex = 1
        cbCutoffPeriod.SelectedItem = "All"
        cbDedtype.SelectedIndex = -1
        cbStartMonth.SelectedIndex = Date.Today.Month - 1
        nupStartYear.Value = Date.Today.Year
        txtManual.Text = ""
        txtRemarks.Text = ""
        txtTransNum.Text = ""
        txtStatus.Text = ""
        dtpDocDate.MinDate = GetMaxPEC()

    End Sub

    Private Sub tsbEdit_Click(sender As Object, e As EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("ATD_EDIT") Then
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

    Private Sub tsbCancel_Click(sender As Object, e As EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("ATD_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim updateSQL As String
                        updateSQL = " UPDATE  tblATD SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        ATDNo = txtTransNum.Text
                        SQL.AddParam("@TransID", TransID)
                        SQL.ExecNonQuery(updateSQL)

                      
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

                        ATDNo = txtTransNum.Text
                        LoadATD(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "ATD_No", ATDNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As Object, e As EventArgs) Handles tsbClose.Click

        ' Toolstrip Buttons
        If ATDNo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadATD(TransID)
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

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub txtVCEName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtVCEName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.CustomerCode = txtCustomerCode.Text
            f.ModFrom = "CustomerFilter"
            f.txtFilter.Text = txtVCEName.Text
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            txtVCEName.Text = f.VCEName
            f.Dispose()
        End If
    End Sub

    Private Sub chkEndDate_CheckedChanged(sender As Object, e As EventArgs) Handles chkRecurring.CheckedChanged
        txtTotalAmount.Visible = Not chkRecurring.Checked
        lblTotal.Visible = Not chkRecurring.Checked
    End Sub

    Private Sub cbDedCutoff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCutoffPeriod.SelectedIndexChanged
        If cbCutoffPeriod.SelectedItem = "All" Then
            cbStartCutoff.Visible = True
        Else
            cbStartCutoff.Visible = False
        End If
    End Sub

    Private Sub GetStartPaymentDate()
        Dim query, Cutoff_Identity As String
        query = " SELECT  TOP 1 PayrollYear, PayrollMonth, Cutoff_Identity FROM RUBY_ePeople.dbo.tblPayroll_Period  " & _
                " WHERE Period_ID IN (SELECT Period_ID FROM RUBY_ePeople.dbo.tblOrg_CostHeader WHERE VCECode ='C00003') " & _
                " AND Paydate >= @Date " & _
                " ORDER BY Paydate "
        SQL.FlushParams()
        SQL.AddParam("@Date", dtpDocDate.Value.Date)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Cutoff_Identity = SQL.SQLDR("Cutoff_Identity")
            nupStartYear.Value = SQL.SQLDR("PayrollYear")
            cbStartMonth.SelectedIndex = SQL.SQLDR("PayrollMonth")
            cbStartCutoff.SelectedItem = Cutoff_Identity
        Else
            nupStartYear.Value = Date.Today.Year
            cbStartMonth.SelectedIndex = Date.Today.Month
        End If
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("ATD", TransID)
        f.Dispose()
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("ATD_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("ATD")
            TransID = f.transID
            LoadATD(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub cbStartCutoff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbStartCutoff.SelectedIndexChanged
        If disableEvent = False Then
            dtpPaydate.Value = LoadPayDate(txtVCECode.Text)
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If ATDNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblATD WHERE ATD_No < '" & ATDNo & "' ORDER BY ATD_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadATD(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If ATDNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblATD  WHERE ATD_No > '" & ATDNo & "' ORDER BY ATD_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadATD(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub cbStartMonth_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbStartMonth.SelectedIndexChanged
        If disableEvent = False Then
            dtpPaydate.Value = LoadPayDate(txtVCECode.Text)
        End If
    End Sub

    Private Sub nupStartYear_ValueChanged(sender As System.Object, e As System.EventArgs) Handles nupStartYear.ValueChanged
        If disableEvent = False Then
            dtpPaydate.Value = LoadPayDate(txtVCECode.Text)
        End If
    End Sub

    Private Sub cbDedtype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDedtype.SelectedIndexChanged

    End Sub

    Private Sub btnSearchVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchVCE.Click

    End Sub

    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged

    End Sub

    Private Sub txtCustomerName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtCustomerName.KeyDown

        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.ModFrom = "Customer"
            f.cbFilter.SelectedItem = "VCEName"
            f.txtFilter.Text = txtCustomerName.Text
            f.ShowDialog()
            txtCustomerCode.Text = f.VCECode
            txtCustomerName.Text = f.VCEName
        End If
    End Sub


    Private Sub txtCustomerName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCustomerName.TextChanged

    End Sub
End Class