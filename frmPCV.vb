Public Class frmPCV

    Dim TransID, RefID, JETransiD As String
    Dim PCVNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "PCV"
    Dim ColumnPK As String = "PCV_No"
    Dim DBTable As String = "tblPCV"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim APV_ID, ADV_ID As Integer
    Dim bankID As Integer
    Dim SQL1 As New SQLControl
    Dim controlDisabled As Boolean = False
    Dim ForApproval As Boolean = False

    Public Overloads Function ShowDialog(ByVal DocNumber As String) As Boolean
        TransID = DocNumber
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub Disbursement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            LoadPaymentType()
            If cbPaymentType.Items.Count > 0 Then
                cbPaymentType.SelectedIndex = 0
            End If
            dtpDocDate.Value = Date.Today.Date
            If TransID <> "" Then
                LoadPCV(TransID)
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
        dtpDocDate.Enabled = Value
        dgvRecords.AllowUserToAddRows = Value
        dgvRecords.AllowUserToDeleteRows = Value
        If Value = True Then
            dgvRecords.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
            RefreshType()
        Else
            dgvRecords.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        txtRemarks.Enabled = Value
        txtAmount.Enabled = Value
        cbPaymentType.Enabled = Value
        cbDisburseType.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub
    Private Sub LoadPaymentType()
        Dim query As String
        query = " SELECT Payment_Type FROM tblPCV_PaymentType "
        SQL.ReadQuery(query)
        cbPaymentType.Items.Clear()
        While SQL.SQLDR.Read
            cbPaymentType.Items.Add(SQL.SQLDR("Payment_Type").ToString)
        End While
    End Sub


    Private Sub LoadDisburseType()
        Dim query As String
        query = " SELECT  DISTINCT ISNULL(Expense_Description,'') AS Expense_Description " & _
                " FROM    tblPCV_ExpenseType " & _
                " WHERE   Status ='Active' "
        SQL.ReadQuery(query)
        cbDisburseType.Items.Clear()
        While SQL.SQLDR.Read
            cbDisburseType.Items.Add(SQL.SQLDR("Expense_Description").ToString)
        End While
    End Sub



    Private Sub cbCategory_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        Try
            LoadDisburseType()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub ClearText()
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        txtAmount.Text = ""
        txtRemarks.Text = ""
        If cbPaymentType.Items.Count > 0 Then cbPaymentType.SelectedIndex = 0 Else cbPaymentType.SelectedIndex = -1
        txtTransNum.Text = ""
        txtStatus.Text = ""
        dgvRecords.Rows.Clear()
        dtpDocDate.MinDate = GetMaxPEC().AddDays(1)
        dtpDocDate.Value = Date.Today.Date
    End Sub

    Private Sub SavePCV()
        Try
            Dim insertSQL As String
            activityStatus = True
            insertSQL = " INSERT INTO " & _
                        " tblPCV (TransID, PCV_No, BranchCode, BusinessCode, VCECode, DatePCV, Amount, InputVAT, BaseAmount, Remarks,  WhoCreated, Status) " & _
                        " VALUES(@TransID, @PCV_No, @BranchCode, @BusinessCode,  @VCECode, @DatePCV, @Amount, @InputVAT, @BaseAmount,  @Remarks, @WhoCreated, @Status)"
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@PCV_No", PCVNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DatePCV", dtpDocDate.Value.Date)
            SQL.AddParam("@Amount", CDec(txtAmount.Text))
            SQL.AddParam("@InputVAT", CDec(txtInputVAT.Text))
            SQL.AddParam("@BaseAmount", CDec(txtBaseAmount.Text))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)


            Dim line As Integer = 1
            For Each item As DataGridViewRow In dgvRecords.Rows
                Dim VCECode, TIN As String

                If item.Cells(dgcVCECode.Index).Value Is Nothing Then
                    VCECode = ""
                Else
                    VCECode = item.Cells(dgcVCECode.Index).Value.ToString
                End If

                If item.Cells(dgcTIN.Index).Value Is Nothing Then
                    TIN = ""
                Else
                    TIN = item.Cells(dgcTIN.Index).Value.ToString
                End If

                If IsNothing(item.Cells(dgcVCECode.Index).Value) OrElse item.Cells(dgcVCECode.Index).Value.ToString = "" Then

                    If item.Cells(dgcPayee.Index).Value <> Nothing Then
                        VCECode = GetExistingCode(item.Cells(dgcPayee.Index).Value.ToString, TIN)
                    End If

                    If VCECode = "" And item.Cells(dgcPayee.Index).Value <> Nothing Then
                        Dim query As String
                        query = "  SELECT   'V' + RIGHT('000000' +  Cast(ISNULL(Max(Cast(REPLACE(VCECode, LEFT(VCECode,1),'')as int)+1),1) AS nvarchar),6) AS VCECode " & _
                                " FROM     tblVCE_Master " & _
                                " WHERE    LEFT(VCECode,1) ='V' "
                        SQL.ReadQuery(query)
                        If SQL.SQLDR.Read Then
                            VCECode = SQL.SQLDR("VCECode").ToString
                            AutoSaveVendor(VCECode, item.Cells(dgcPayee.Index).Value.ToString, TIN)
                        Else
                            VCECode = ""
                        End If
                    End If
                Else
                    VCECode = item.Cells(dgcVCECode.Index).Value.ToString
                    UpdateTIN(VCECode, TIN)
                End If

                If VCECode <> "" Or item.Cells(dgcPayee.Index).Value <> Nothing Then
                    insertSQL = " INSERT INTO " & _
                                       " tblPCV_Details(TransID,  RecordDate, CodePayee, RecordPayee, OR_Ref, TIN, Type, Category, Particulars, Amount, VATable, InputVAT, BaseAmount, LineNum, CostCenter, CIP_Code, ProfitCenter) " & _
                                       " VALUES(@TransID,  @RecordDate, @CodePayee, @RecordPayee, @OR_Ref, @TIN, @Type, @Category, @Particulars, @Amount, @VATable, @InputVAT, @BaseAmount, @LineNum, @CostCenter, @CIP_Code, @ProfitCenter)"
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@RecordDate", item.Cells(dgcDate.Index).Value.ToString)
                    SQL.AddParam("@CodePayee", VCECode)
                    If item.Cells(dgcPayee.Index).Value Is Nothing Then
                        SQL.AddParam("@RecordPayee", "")
                    Else
                        SQL.AddParam("@RecordPayee", item.Cells(dgcPayee.Index).Value.ToString)
                    End If

                    If item.Cells(dgcOR.Index).Value Is Nothing Then
                        SQL.AddParam("@OR_Ref", "")
                    Else
                        SQL.AddParam("@OR_Ref", item.Cells(dgcOR.Index).Value.ToString)
                    End If

                    SQL.AddParam("@TIN", TIN)

                    If item.Cells(dgcType.Index).Value Is Nothing Then
                        SQL.AddParam("@Type", "")
                    Else
                        SQL.AddParam("@Type", item.Cells(dgcType.Index).Value.ToString)
                    End If

                    If item.Cells(dgcCategory.Index).Value Is Nothing Then
                        SQL.AddParam("@Category", "")
                    Else
                        SQL.AddParam("@Category", item.Cells(dgcCategory.Index).Value.ToString)
                    End If
                    If item.Cells(dgcParticulars.Index).Value Is Nothing Then
                        SQL.AddParam("@Particulars", "")
                    Else
                        SQL.AddParam("@Particulars", item.Cells(dgcParticulars.Index).Value.ToString)
                    End If

                    SQL.AddParam("@Amount", CDec(item.Cells(dgcAmount.Index).Value))
                    If item.Cells(dgcVAT.Index).Value Is Nothing Then
                        SQL.AddParam("@VATable", False)
                    Else
                        SQL.AddParam("@VATable", item.Cells(dgcVAT.Index).Value.ToString)
                    End If
                    SQL.AddParam("@InputVAT", CDec(item.Cells(dgcInputVAT.Index).Value))
                    SQL.AddParam("@BaseAmount", CDec(item.Cells(dgcBase.Index).Value))
                    SQL.AddParam("@LineNum", line)
                    If item.Cells(chCostID.Index).Value <> Nothing AndAlso item.Cells(chCostID.Index).Value <> "" Then
                        SQL.AddParam("@CostCenter", item.Cells(chCostID.Index).Value.ToString)
                    Else
                        SQL.AddParam("@CostCenter", "")
                    End If
                    If item.Cells(chCIP_Code.Index).Value <> Nothing AndAlso item.Cells(chCIP_Code.Index).Value <> "" Then
                        SQL.AddParam("@CIP_Code", item.Cells(chCIP_Code.Index).Value.ToString)
                    Else
                        SQL.AddParam("@CIP_Code", "")
                    End If
                    If item.Cells(chProfit_Code.Index).Value <> Nothing AndAlso item.Cells(chProfit_Code.Index).Value <> "" Then
                        SQL.AddParam("@ProfitCenter", item.Cells(chProfit_Code.Index).Value.ToString)
                    Else
                        SQL.AddParam("@ProfitCenter", "")
                    End If
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "PCV_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub AutoSaveVendor(ByVal Code As String, Name As String, TIN As String)
        Dim query As String
        query = "SELECT * FROM tblVCE_Master WHERE VCECode = @VCECode  "
        SQL.FlushParams()
        SQL.AddParam("@VCECode", Code)
        If Not SQL.SQLDR.Read Then
            Dim insertSQl As String
            insertSQl = " INSERT INTO " & _
                        " tblVCE_Master(VCECode, VCEName, TIN_No, isVendor, SystemGen) " & _
                        " VALUES (@VCECode, @VCEName, @TIN_No, @isVendor, @SystemGen) "
            SQL.FlushParams()
            SQL.AddParam("@VCECode", Code)
            SQL.AddParam("@VCEName", Name)
            SQL.AddParam("@TIN_No", TIN)
            SQL.AddParam("@isVendor", True)
            SQL.AddParam("@SystemGen", True)
            SQL.ExecNonQuery(insertSQl)
        End If
    End Sub

    Private Sub UpdateTIN(ByVal Code As String, TIN As String)
        Dim updateSQL As String
        updateSQL = " UPDATE    tblVCE_Master " & _
                    " SET       TIN_No = @TIN_No, isVendor = @isVendor " & _
                    " WHERE     VCECode = @VCECode "
        SQL.FlushParams()
        SQL.AddParam("@VCECode", Code)
        SQL.AddParam("@TIN_No", TIN)
        SQL.AddParam("@isVendor", True)
        SQL.ExecNonQuery(updateSQL)
    End Sub

    Private Function GetExistingCode(Name As String, TIN As String) As String
        Dim query As String
        query = "SELECT VCECode FROM tblVCE_Master WHERE VCEName = @VCEName  OR TIN_NO = @TIN "
        SQL.FlushParams()
        SQL.AddParam("@VCEName", Name)
        SQL.AddParam("@TIN", TIN)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("VCECode").ToString
        Else
            Return ""
        End If
    End Function

    Private Sub LoadPCV(ByVal ID As String)
        Dim query As String
        query = " SELECT  TransID, PCV_No, tblPCV.VCECode, VCEName, DatePCV, Amount, InputVAT, BaseAmount," & _
                "          Remarks,  CASE WHEN TransID IN (SELECT PCV_TransID FROM tblPCVRR_Details WHERE Status = 'Active') THEN 'Closed' ELSE tblPCV.Status END AS Status " & _
                " FROM    tblPCV LEFT JOIN viewVCE_Master " & _
                " ON      tblPCV.VCECode = viewVCE_Master.VCECode " & _
                " WHERE   TransID = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            PCVNo = SQL.SQLDR("PCV_No").ToString
            txtTransNum.Text = PCVNo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
            dtpDocDate.Value = SQL.SQLDR("DatePCV")
            txtAmount.Text = CDec(SQL.SQLDR("Amount")).ToString("N2")
            txtInputVAT.Text = CDec(SQL.SQLDR("InputVAT")).ToString("N2")
            txtBaseAmount.Text = CDec(SQL.SQLDR("BaseAmount")).ToString("N2")
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            LoadPCVDetails(TransID)

            dgvRecords.ClearSelection()
            ' TOOLSTRIP BUTTONS
            If txtStatus.Text = "Cancelled" Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            ElseIf txtStatus.Text = "Closed" Then
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
            If dtpDocDate.Value <= GetMaxPEC() Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            End If
            EnableControl(False)
        Else
            ClearText()
        End If
    End Sub
    Private Sub LoadPCVDetails(ByVal ID As Integer)
        Dim query As String
        query = " SELECT TransID,  RecordDate, CodePayee, RecordPayee, OR_Ref, TIN, Type, Category, Particulars, Amount, VATable, InputVAT, BaseAmount, BranchCode, CostCenter, CIP_Code, ProfitCenter " & _
                " FROM   tblPCV_Details " & _
                " WHERE  TransID = '" & ID & "' " & _
                " ORDER BY LineNum "
        SQL.ReadQuery(query)
        dgvRecords.Rows.Clear()
        Dim rowsCount As Integer = 0
        While SQL.SQLDR.Read

            dgvRecords.Rows.Add(SQL.SQLDR("BranchCode").ToString)
            dgvRecords.Rows(rowsCount).Cells(dgcDate.Index).Value = SQL.SQLDR("RecordDate").ToString
            dgvRecords.Rows(rowsCount).Cells(dgcVCECode.Index).Value = SQL.SQLDR("CodePayee").ToString
            dgvRecords.Rows(rowsCount).Cells(dgcPayee.Index).Value = SQL.SQLDR("RecordPayee").ToString
            dgvRecords.Rows(rowsCount).Cells(dgcOR.Index).Value = SQL.SQLDR("OR_Ref").ToString
            dgvRecords.Rows(rowsCount).Cells(dgcTIN.Index).Value = SQL.SQLDR("TIN").ToString
            dgvRecords.Rows(rowsCount).Cells(dgcAmount.Index).Value = CDec(SQL.SQLDR("Amount")).ToString("N2")
            dgvRecords.Rows(rowsCount).Cells(dgcVAT.Index).Value = SQL.SQLDR("VATable")
            dgvRecords.Rows(rowsCount).Cells(dgcType.Index).Value = SQL.SQLDR("Type")
            dgvRecords.Rows(rowsCount).Cells(dgcCategory.Index).Value = SQL.SQLDR("Category").ToString
            dgvRecords.Rows(rowsCount).Cells(dgcParticulars.Index).Value = SQL.SQLDR("Particulars").ToString
            dgvRecords.Rows(rowsCount).Cells(dgcInputVAT.Index).Value = CDec(SQL.SQLDR("InputVAT")).ToString("N2")
            dgvRecords.Rows(rowsCount).Cells(dgcBase.Index).Value = CDec(SQL.SQLDR("BaseAmount")).ToString("N2")


            'CostCenter
            Dim strCCCode As String = SQL.SQLDR("CostCenter").ToString
            Dim cbvCostCenter As DataGridViewComboBoxCell = LoadCostCenterGridView()
            Dim strCostCenter As String = GetCCName(strCCCode)
            If cbvCostCenter.Items.Contains(IIf(IsNothing(strCostCenter), "", strCostCenter)) Then
                cbvCostCenter.Value = strCostCenter
            End If
            dgvRecords.Rows(rowsCount).Cells(chCostCenter.Index) = cbvCostCenter
            dgvRecords.Rows(rowsCount).Cells(chCostID.Index).Value = SQL.SQLDR("CostCenter").ToString

            'CIP
            Dim strCIP_Code As String = SQL.SQLDR("CIP_Code").ToString
            Dim cbvCIP As DataGridViewComboBoxCell = LoadCIPGridView()
            Dim strCIP As String = GetCIPName(strCIP_Code)
            If cbvCIP.Items.Contains(IIf(IsNothing(strCIP), "", strCIP)) Then
                cbvCIP.Value = strCIP
            End If
            dgvRecords.Rows(rowsCount).Cells(chCIP_Desc.Index) = cbvCIP
            dgvRecords.Rows(rowsCount).Cells(chCIP_Code.Index).Value = SQL.SQLDR("CIP_Code").ToString

            'PC
            Dim strPC_Code As String = SQL.SQLDR("ProfitCenter").ToString
            Dim cbvPC As DataGridViewComboBoxCell = LoadProfitCenterGridView()
            Dim strPC As String = GetPCName(strPC_Code)
            If cbvPC.Items.Contains(IIf(IsNothing(strPC), "", strPC)) Then
                cbvPC.Value = strPC
            End If
            dgvRecords.Rows(rowsCount).Cells(chProfit_Desc.Index) = cbvPC
            dgvRecords.Rows(rowsCount).Cells(chProfit_Code.Index).Value = SQL.SQLDR("ProfitCenter").ToString


            rowsCount += 1


            'dgvRecords.Rows.Add(SQL.SQLDR("BranchCode").ToString, SQL.SQLDR("RecordDate").ToString, SQL.SQLDR("CodePayee").ToString, SQL.SQLDR("RecordPayee").ToString, SQL.SQLDR("OR_Ref").ToString, _
            '                   SQL.SQLDR("TIN").ToString, CDec(SQL.SQLDR("Amount")).ToString("N2"), SQL.SQLDR("VATable"), SQL.SQLDR("Type").ToString, SQL.SQLDR("Category").ToString, SQL.SQLDR("Particulars").ToString, _
            '                  CDec(SQL.SQLDR("InputVAT")).ToString("N2"), CDec(SQL.SQLDR("BaseAmount")).ToString("N2"))

        End While
        RefreshType()

        ComputeTotal()
    End Sub

    Public Function GetCIPName(ByVal CCCode As String) As String
        Dim query As String
        query = " SELECT CIP_Desc FROM tblCIP_Maintenance WHERE CIP_Code ='" & CCCode & "' "
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("CIP_Desc").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetCCName(ByVal CCCode As String) As String
        Dim query As String
        query = " SELECT Description FROM tblCC WHERE Code ='" & CCCode & "' "
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("Description").ToString
        Else
            Return ""
        End If
    End Function

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
        dgvRecords.Rows(RowIndex).Cells(CodeIndex).Value = strDefaultGridView
        dgvRecords.Rows(RowIndex).Cells(CostIndex).Value = strDefaultGridCode

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

    Private Sub RefreshType()
        For i As Integer = 0 To dgvRecords.Rows.Count - 1
            LoadType(i)
        Next
    End Sub

    Private Sub UpdatePCV()
        Try
            Dim insertSQL, updateSQL, deleteSQL As String
            activityStatus = True
            updateSQL = " UPDATE tblPCV  " & _
                        " SET    PCV_No = @PCV_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, VCECode = @VCECode, DatePCV = @DatePCV, " & _
                        "        Amount = @Amount, InputVAT = @InputVAT, BaseAmount = @BaseAmount, Remarks = @Remarks,  WhoModified = @WhoModified, DateModified = GETDATE() " & _
                        " WHERE TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@PCV_No", PCVNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DatePCV", dtpDocDate.Value.Date)
            SQL.AddParam("@Amount", CDec(txtAmount.Text))
            SQL.AddParam("@InputVAT", CDec(txtInputVAT.Text))
            SQL.AddParam("@BaseAmount", CDec(txtBaseAmount.Text))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)



            ' DELETE ACCOUNTING ENTRIES
            deleteSQL = " DELETE FROM tblPCV_Details  WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.ExecNonQuery(deleteSQL)

            Dim line As Integer = 1
            For Each item As DataGridViewRow In dgvRecords.Rows
                Dim VCECode, TIN As String
                If item.Cells(dgcVCECode.Index).Value Is Nothing Then
                    VCECode = ""
                Else
                    VCECode = item.Cells(dgcVCECode.Index).Value.ToString
                End If

                If item.Cells(dgcTIN.Index).Value Is Nothing Then
                    TIN = ""
                Else
                    TIN = item.Cells(dgcTIN.Index).Value.ToString
                End If

                If IsNothing(item.Cells(dgcVCECode.Index).Value) OrElse item.Cells(dgcVCECode.Index).Value.ToString = "" Then
                    If item.Cells(dgcPayee.Index).Value <> Nothing Then
                        VCECode = GetExistingCode(item.Cells(dgcPayee.Index).Value.ToString, TIN)
                    End If
                    If VCECode = "" And item.Cells(dgcPayee.Index).Value <> Nothing Then
                        Dim query As String
                        query = "  SELECT   'V' + RIGHT('000000' +  Cast(ISNULL(Max(Cast(REPLACE(VCECode, LEFT(VCECode,1),'')as int)+1),1) AS nvarchar),6) AS VCECode " & _
                                " FROM     tblVCE_Master " & _
                                " WHERE    LEFT(VCECode,1) ='V' "
                        SQL.ReadQuery(query)
                        If SQL.SQLDR.Read Then
                            VCECode = SQL.SQLDR("VCECode").ToString
                            AutoSaveVendor(VCECode, item.Cells(dgcPayee.Index).Value.ToString, TIN)
                        Else
                            VCECode = ""
                        End If
                    End If
                Else
                    VCECode = item.Cells(dgcVCECode.Index).Value.ToString
                    UpdateTIN(VCECode, TIN)
                End If

                If VCECode <> "" Or item.Cells(dgcPayee.Index).Value <> Nothing Then
                    insertSQL = " INSERT INTO " & _
                                       " tblPCV_Details(TransID,  RecordDate, CodePayee, RecordPayee, OR_Ref, TIN, Type, Category, Particulars, Amount, VATable, InputVAT, BaseAmount, LineNum, CostCenter, CIP_Code, ProfitCenter) " & _
                                       " VALUES(@TransID,  @RecordDate, @CodePayee, @RecordPayee, @OR_Ref, @TIN, @Type, @Category, @Particulars, @Amount, @VATable, @InputVAT, @BaseAmount, @LineNum, @CostCenter, @CIP_Code, @ProfitCenter)"
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@RecordDate", item.Cells(dgcDate.Index).Value.ToString)
                    SQL.AddParam("@CodePayee", VCECode)
                    If item.Cells(dgcPayee.Index).Value Is Nothing Then
                        SQL.AddParam("@RecordPayee", "")
                    Else
                        SQL.AddParam("@RecordPayee", item.Cells(dgcPayee.Index).Value.ToString)
                    End If

                    If item.Cells(dgcOR.Index).Value Is Nothing Then
                        SQL.AddParam("@OR_Ref", "")
                    Else
                        SQL.AddParam("@OR_Ref", item.Cells(dgcOR.Index).Value.ToString)
                    End If

                    SQL.AddParam("@TIN", TIN)

                    If item.Cells(dgcType.Index).Value Is Nothing Then
                        SQL.AddParam("@Type", "")
                    Else
                        SQL.AddParam("@Type", item.Cells(dgcType.Index).Value.ToString)
                    End If

                    If item.Cells(dgcCategory.Index).Value Is Nothing Then
                        SQL.AddParam("@Category", "")
                    Else
                        SQL.AddParam("@Category", item.Cells(dgcCategory.Index).Value.ToString)
                    End If
                    If item.Cells(dgcParticulars.Index).Value Is Nothing Then
                        SQL.AddParam("@Particulars", "")
                    Else
                        SQL.AddParam("@Particulars", item.Cells(dgcParticulars.Index).Value.ToString)
                    End If

                    SQL.AddParam("@Amount", CDec(item.Cells(dgcAmount.Index).Value))
                    If item.Cells(dgcVAT.Index).Value Is Nothing Then
                        SQL.AddParam("@VATable", False)
                    Else
                        SQL.AddParam("@VATable", item.Cells(dgcVAT.Index).Value.ToString)
                    End If
                    SQL.AddParam("@InputVAT", CDec(item.Cells(dgcInputVAT.Index).Value))
                    SQL.AddParam("@BaseAmount", CDec(item.Cells(dgcBase.Index).Value))
                    SQL.AddParam("@LineNum", line)
                    If item.Cells(chCostID.Index).Value <> Nothing AndAlso item.Cells(chCostID.Index).Value <> "" Then
                        SQL.AddParam("@CostCenter", item.Cells(chCostID.Index).Value.ToString)
                    Else
                        SQL.AddParam("@CostCenter", "")
                    End If
                    If item.Cells(chCIP_Code.Index).Value <> Nothing AndAlso item.Cells(chCIP_Code.Index).Value <> "" Then
                        SQL.AddParam("@CIP_Code", item.Cells(chCIP_Code.Index).Value.ToString)
                    Else
                        SQL.AddParam("@CIP_Code", "")
                    End If
                    If item.Cells(chProfit_Code.Index).Value <> Nothing AndAlso item.Cells(chProfit_Code.Index).Value <> "" Then
                        SQL.AddParam("@ProfitCenter", item.Cells(chProfit_Code.Index).Value.ToString)
                    Else
                        SQL.AddParam("@ProfitCenter", "")
                    End If
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "PCV_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Public Sub LoadCostCenterCode(ByVal CostCenter As String, ByVal RowIndex As Integer, ByVal CodeIndex As Integer, ByVal CostIndex As Integer)

        Dim selectSQL As String
        selectSQL = " SELECT Code, Description FROM tblCC WHERE Description = '" & CostCenter & "'"
        SQL.ReadQuery(selectSQL, 2)

        strDefaultGridView = ""
        strDefaultGridCode = ""

        While SQL.SQLDR2.Read
            strDefaultGridView = SQL.SQLDR2("Description").ToString
            strDefaultGridCode = SQL.SQLDR2("Code").ToString
        End While
        dgvRecords.Rows(RowIndex).Cells(CodeIndex).Value = strDefaultGridView
        dgvRecords.Rows(RowIndex).Cells(CostIndex).Value = strDefaultGridCode

    End Sub

    Public Sub LoadCIPCode(ByVal CIP As String, ByVal RowIndex As Integer, ByVal CodeIndex As Integer, ByVal CIPIndex As Integer)

        Dim selectSQL As String
        selectSQL = " SELECT CIP_Code, CIP_Desc FROM tblCIP_Maintenance WHERE CIP_Desc = '" & CIP & "'"
        SQL.ReadQuery(selectSQL, 2)

        strDefaultGridView = ""
        strDefaultGridCode = ""

        While SQL.SQLDR2.Read
            strDefaultGridView = SQL.SQLDR2("CIP_Desc").ToString
            strDefaultGridCode = SQL.SQLDR2("CIP_Code").ToString
        End While
        dgvRecords.Rows(RowIndex).Cells(CIPIndex).Value = strDefaultGridView
        dgvRecords.Rows(RowIndex).Cells(CIPIndex).Value = strDefaultGridCode

    End Sub

    'Start of Cost Center insert to Table
    Dim strDefaultGridView As String = ""
    Dim strDefaultGridCode As String = ""
    Public Function LoadCostCenterGridView()

        Dim selectSQL As String = " SELECT Code, Description FROM tblCC"
        SQL.ReadQuery(selectSQL, 2)

        Dim cbvGridviewCell As New DataGridViewComboBoxCell

        Dim count As Integer = 1
        cbvGridviewCell.Items.Add("")
        strDefaultGridView = ""
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

    Public Function LoadCIPGridView()

        Dim selectSQL As String = " SELECT CIP_Code, CIP_Desc FROM tblCIP_Maintenance"
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
        strDefaultGridView = ""
        Return cbvGridviewCell

    End Function

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("PCV_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("PCV_Load")
            TransID = f.transID
            LoadPCV(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("PCV_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            PCVNo = ""

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
            tsbOption.Enabled = False
            txtStatus.Text = "Open"
            EnableControl(True)
            LoadType()
            If dgvRecords.Rows.Count > 0 Then
                dgvRecords.Rows(0).Cells(dgcDate.Index).Value = dtpDocDate.Value
            End If
            If dgvRecords.SelectedCells.Count > 0 Then
                dgvRecords.SelectedCells(0).Selected = False
            End If

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
            txtVCEName.Select()
        End If
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("PCV_EDIT") Then
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
            tsbOption.Enabled = True
        End If
    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("PCV_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE  tblPCV SET Status ='Cancelled' WHERE PCV_No = @PCV_No "
                        SQL.FlushParams()
                        PCVNo = txtTransNum.Text
                        SQL.AddParam("@PCV_No", PCVNo)
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

                        PCVNo = txtTransNum.Text
                        LoadPCV(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "PCV_No", PCVNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbCopy_Click(sender As System.Object, e As System.EventArgs) Handles tsbCopy.Click

    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs)
        Dim f As New frmReport_Display
        f.ShowDialog("PCV", TransID)
        f.Dispose()
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If PCVNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblPCV  WHERE PCV_No < '" & PCVNo & "' ORDER BY PCV_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                PCVNo = SQL.SQLDR("TransID").ToString
                LoadPCV(PCVNo)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If PCVNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblPCV  WHERE PCV_No > '" & PCVNo & "' ORDER BY PCV_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                PCVNo = SQL.SQLDR("TransID").ToString
                LoadPCV(PCVNo)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If PCVNo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadPCV(TransID)
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

    Private Function validateDGV() As Boolean
        Dim value As Boolean = True
       
        For Each row As DataGridViewRow In dgvRecords.Rows
            If row.Cells(dgcVCECode.Index).Value <> "" Or row.Cells(dgcPayee.Index).Value <> Nothing Then
                If row.Cells(dgcDate.Index).Value Is Nothing Then
                    Msg("There are line entry without date, please check.", MsgBoxStyle.Exclamation)
                    value = False
                    Exit For
                End If
            End If
        Next
            Return value

    End Function

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If validateDGV() Then
            If txtVCECode.Text = "" Then
                Msg("Please enter VCECode!", MsgBoxStyle.Exclamation)
            ElseIf txtRemarks.Text = "" Then
                MsgBox("Please enter a remark/short explanation", MsgBoxStyle.Exclamation)
            ElseIf txtAmount.Text = "" Then
                MsgBox("Please check amount!", MsgBoxStyle.Exclamation)
            ElseIf dgvRecords.Rows.Count = 0 Then
                MsgBox("No entries, Please check!", MsgBoxStyle.Exclamation)
            ElseIf TransAuto = False And txtTransNum.Text = "" Then
                MsgBox("Please Enter PCV No.!", MsgBoxStyle.Exclamation)
            ElseIf TransAuto = False AndAlso IfExist(txtTransNum.Text) And TransID = "" Then
                MsgBox("PCV" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
            ElseIf TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID(ColumnPK, DBTable)
                    If TransAuto = True Then
                        PCVNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    Else
                        PCVNo = txtTransNum.Text
                    End If
                    SavePCV()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    LoadPCV(TransID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    If PCVNo = txtTransNum.Text Then
                        PCVNo = txtTransNum.Text
                        UpdatePCV()
                        Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                        PCVNo = txtTransNum.Text
                        LoadPCV(TransID)
                    Else
                        If Not IfExist(txtTransNum.Text) Then
                            PCVNo = txtTransNum.Text
                            UpdatePCV()
                            Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                            PCVNo = txtTransNum.Text
                            LoadPCV(TransID)
                        Else
                            MsgBox("PCV" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Function IfExist(ByVal ID As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblPCV WHERE PCV_No ='" & ID & "'  "
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
    End Sub

    Private Sub txtVCEName_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.txtFilter.Text = txtVCEName.Text
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            txtVCEName.Text = f.VCEName
            f.Dispose()
        End If
    End Sub

    Private Sub frmPCV_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
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

    Private Sub tsbPrint_ButtonClick(sender As System.Object, e As System.EventArgs) Handles tsbPrint.ButtonClick
        Dim f As New frmReport_Display
        f.ShowDialog("PCV", TransID)
        f.Dispose()
    End Sub

    Private Sub ChequieToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        Dim f As New frmCheckPrinting
        f.CVTransID = TransID
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub BIR2307ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        Dim f As New frmReport_Display
        f.ShowDialog("BIR_2307", TransID)
        f.Dispose()
    End Sub

    Dim eColIndex As Integer = 0
    Private Sub dgvRecords_CellBeginEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvRecords.CellBeginEdit
        eColIndex = e.ColumnIndex
    End Sub

    Private Sub dgvRecords_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvRecords.CellContentClick

    End Sub

    Private Sub dgvRecords_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvRecords.CellEndEdit
        If e.ColumnIndex = dgcTIN.Index Then
            Dim CC, PC As String
            Dim filter As String = ""
            If Not dgvRecords.Item(e.ColumnIndex, e.RowIndex).Value Is Nothing Then
                filter = dgvRecords.Item(e.ColumnIndex, e.RowIndex).Value.ToString
            End If
            If LoadListPCV("TIN", filter, e.RowIndex) Then
                Dim f As New frmVCE_Search
                f.ModType = "RFP"
                f.cbFilter.Items.Clear()
                f.cbFilter.Items.Add("RecordPayee")
                f.cbFilter.Items.Add("TIN")
                f.cbFilter.SelectedItem = "TIN"
                f.Type = "TIN"
                f.rowInd = e.RowIndex
                f.txtFilter.Text = filter
                f.ShowDialog()
                If Not f.TIN Is Nothing Or Not f.VCEName Is Nothing Then
                    dgvRecords.Item(dgcTIN.Index, e.RowIndex).Value = f.TIN
                    dgvRecords.Item(dgcVCECode.Index, e.RowIndex).Value = f.VCECode
                    dgvRecords.Item(dgcPayee.Index, e.RowIndex).Value = f.VCEName
                    CC = GetCC(f.VCECode)
                    PC = GetPC(f.VCECode)
                End If
                f.Dispose()
            End If

            'Auto Entry Grid View Cost Center
            If IsNothing(dgvRecords.Item(chCostCenter.Index, e.RowIndex).Value) And IsNothing(CC) Then
                Dim cbvCostCenter As DataGridViewComboBoxCell = LoadCostCenterGridView()
                cbvCostCenter.Value = strDefaultGridView
                dgvRecords.Item(chCostCenter.Index, e.RowIndex) = cbvCostCenter

                Dim dgvCostCenter As String
                dgvCostCenter = dgvRecords.Rows(e.RowIndex).Cells(chCostCenter.Index).Value
                LoadCostCenterCode(dgvCostCenter, e.RowIndex, chCostCenter.Index, chCostID.Index)
            Else
                'CostCenter
                Dim strCCCode As String = CC
                Dim cbvCostCenter As DataGridViewComboBoxCell = LoadCostCenterGridView()
                Dim strCostCenter As String = GetCCName(strCCCode)
                If cbvCostCenter.Items.Contains(IIf(IsNothing(strCostCenter), "", strCostCenter)) Then
                    cbvCostCenter.Value = strCostCenter
                End If
                dgvRecords.Rows(e.RowIndex).Cells(chCostCenter.Index) = cbvCostCenter
                dgvRecords.Rows(e.RowIndex).Cells(chCostID.Index).Value = CC
            End If

            'Auto Entry Grid View Cost CIP
            If IsNothing(dgvRecords.Item(chCIP_Desc.Index, e.RowIndex).Value) Then
                Dim cbvCIP As DataGridViewComboBoxCell = LoadCIPGridView()
                cbvCIP.Value = strDefaultGridView
                dgvRecords.Item(chCIP_Desc.Index, e.RowIndex) = cbvCIP

                Dim dgvCIP As String
                dgvCIP = dgvRecords.Rows(e.RowIndex).Cells(chCIP_Desc.Index).Value
                LoadCIPCode(dgvCIP, e.RowIndex, chCIP_Desc.Index, chCIP_Code.Index)
            End If

            'Auto Entry Grid View Profit Center
            If IsNothing(dgvRecords.Item(chProfit_Desc.Index, dgvRecords.Rows.Count - 2).Value) And IsNothing(PC) Then
                Dim cbvPC As DataGridViewComboBoxCell = LoadProfitCenterGridView()
                cbvPC.Value = strDefaultGridView
                dgvRecords.Item(chProfit_Desc.Index, dgvRecords.Rows.Count - 2) = cbvPC

                Dim dgvPC As String
                dgvPC = dgvRecords.Rows(dgvRecords.Rows.Count - 2).Cells(chProfit_Desc.Index).Value
                LoadProfitCenterCode(dgvPC, dgvRecords.Rows.Count - 2, chProfit_Desc.Index, chProfit_Code.Index)
            Else
                'PC
                Dim strPC_Code As String = PC
                Dim cbvPC As DataGridViewComboBoxCell = LoadProfitCenterGridView()
                Dim strPC As String = GetPCName(strPC_Code)
                If cbvPC.Items.Contains(IIf(IsNothing(strPC), "", strPC)) Then
                    cbvPC.Value = strPC
                End If
                dgvRecords.Rows(e.RowIndex).Cells(chProfit_Desc.Index) = cbvPC
                dgvRecords.Rows(e.RowIndex).Cells(chProfit_Code.Index).Value = PC
            End If



        ElseIf e.ColumnIndex = dgcPayee.Index Then
            Dim CC, PC As String
            Dim filter As String = ""
            If Not dgvRecords.Item(e.ColumnIndex, e.RowIndex).Value Is Nothing Then
                filter = dgvRecords.Item(e.ColumnIndex, e.RowIndex).Value.ToString
            End If
            If LoadListPCV("RecordPayee", filter, e.RowIndex) Then
                Dim f As New frmVCE_Search
                f.ModType = "RFP"
                f.cbFilter.Items.Clear()
                f.cbFilter.Items.Add("RecordPayee")
                f.cbFilter.Items.Add("TIN")
                f.cbFilter.SelectedItem = "RecordPayee"
                f.Type = "RecordPayee"
                f.rowInd = e.RowIndex
                f.txtFilter.Text = filter
                f.ShowDialog()
                If Not f.TIN Is Nothing Or Not f.VCEName Is Nothing Then
                    dgvRecords.Item(dgcTIN.Index, e.RowIndex).Value = f.TIN
                    dgvRecords.Item(dgcVCECode.Index, e.RowIndex).Value = f.VCECode
                    dgvRecords.Item(dgcPayee.Index, e.RowIndex).Value = f.VCEName
                    CC = GetCC(f.VCECode)
                    PC = GetPC(f.VCECode)
                End If
                f.Dispose()
            End If

            'Auto Entry Grid View Cost Center
            If IsNothing(dgvRecords.Item(chCostCenter.Index, e.RowIndex).Value) And IsNothing(CC) Then
                Dim cbvCostCenter As DataGridViewComboBoxCell = LoadCostCenterGridView()
                cbvCostCenter.Value = strDefaultGridView
                dgvRecords.Item(chCostCenter.Index, e.RowIndex) = cbvCostCenter

                Dim dgvCostCenter As String
                dgvCostCenter = dgvRecords.Rows(e.RowIndex).Cells(chCostCenter.Index).Value
                LoadCostCenterCode(dgvCostCenter, e.RowIndex, chCostCenter.Index, chCostID.Index)
            Else
                'CostCenter
                Dim strCCCode As String = CC
                Dim cbvCostCenter As DataGridViewComboBoxCell = LoadCostCenterGridView()
                Dim strCostCenter As String = GetCCName(strCCCode)
                If cbvCostCenter.Items.Contains(IIf(IsNothing(strCostCenter), "", strCostCenter)) Then
                    cbvCostCenter.Value = strCostCenter
                End If
                dgvRecords.Rows(e.RowIndex).Cells(chCostCenter.Index) = cbvCostCenter
                dgvRecords.Rows(e.RowIndex).Cells(chCostID.Index).Value = CC
            End If

            'Auto Entry Grid View Cost CIP
            If IsNothing(dgvRecords.Item(chCIP_Desc.Index, e.RowIndex).Value) Then
                Dim cbvCIP As DataGridViewComboBoxCell = LoadCIPGridView()
                cbvCIP.Value = strDefaultGridView
                dgvRecords.Item(chCIP_Desc.Index, e.RowIndex) = cbvCIP

                Dim dgvCIP As String
                dgvCIP = dgvRecords.Rows(e.RowIndex).Cells(chCIP_Desc.Index).Value
                LoadCIPCode(dgvCIP, e.RowIndex, chCIP_Desc.Index, chCIP_Code.Index)
            End If

            'Auto Entry Grid View Profit Center
            If IsNothing(dgvRecords.Item(chProfit_Desc.Index, dgvRecords.Rows.Count - 2).Value) And IsNothing(PC) Then
                Dim cbvPC As DataGridViewComboBoxCell = LoadProfitCenterGridView()
                cbvPC.Value = strDefaultGridView
                dgvRecords.Item(chProfit_Desc.Index, dgvRecords.Rows.Count - 2) = cbvPC

                Dim dgvPC As String
                dgvPC = dgvRecords.Rows(dgvRecords.Rows.Count - 2).Cells(chProfit_Desc.Index).Value
                LoadProfitCenterCode(dgvPC, dgvRecords.Rows.Count - 2, chProfit_Desc.Index, chProfit_Code.Index)
            Else
                'PC
                Dim strPC_Code As String = PC
                Dim cbvPC As DataGridViewComboBoxCell = LoadProfitCenterGridView()
                Dim strPC As String = GetPCName(strPC_Code)
                If cbvPC.Items.Contains(IIf(IsNothing(strPC), "", strPC)) Then
                    cbvPC.Value = strPC
                End If
                dgvRecords.Rows(e.RowIndex).Cells(chProfit_Desc.Index) = cbvPC
                dgvRecords.Rows(e.RowIndex).Cells(chProfit_Code.Index).Value = PC
            End If

        ElseIf e.ColumnIndex = dgcType.Index Then
            LoadType(e.RowIndex)

            'Auto Entry Grid View Cost Center
            If IsNothing(dgvRecords.Item(chCostCenter.Index, e.RowIndex).Value) Then
                Dim cbvCostCenter As DataGridViewComboBoxCell = LoadCostCenterGridView()
                cbvCostCenter.Value = strDefaultGridView
                dgvRecords.Item(chCostCenter.Index, e.RowIndex) = cbvCostCenter

                Dim dgvCostCenter As String
                dgvCostCenter = dgvRecords.Rows(e.RowIndex).Cells(chCostCenter.Index).Value
                LoadCostCenterCode(dgvCostCenter, e.RowIndex, chCostCenter.Index, chCostID.Index)
            End If

            'Auto Entry Grid View Cost CIP
            If IsNothing(dgvRecords.Item(chCIP_Desc.Index, e.RowIndex).Value) Then
                Dim cbvCIP As DataGridViewComboBoxCell = LoadCIPGridView()
                cbvCIP.Value = strDefaultGridView
                dgvRecords.Item(chCIP_Desc.Index, e.RowIndex) = cbvCIP

                Dim dgvCIP As String
                dgvCIP = dgvRecords.Rows(e.RowIndex).Cells(chCIP_Desc.Index).Value
                LoadCIPCode(dgvCIP, e.RowIndex, chCIP_Desc.Index, chCIP_Code.Index)
            End If

            'Auto Entry Grid View Profit Center
            If IsNothing(dgvRecords.Item(chProfit_Desc.Index, dgvRecords.Rows.Count - 2).Value) Then
                Dim cbvPC As DataGridViewComboBoxCell = LoadProfitCenterGridView()
                cbvPC.Value = strDefaultGridView
                dgvRecords.Item(chProfit_Desc.Index, dgvRecords.Rows.Count - 2) = cbvPC

                Dim dgvPC As String
                dgvPC = dgvRecords.Rows(dgvRecords.Rows.Count - 2).Cells(chProfit_Desc.Index).Value
                LoadProfitCenterCode(dgvPC, dgvRecords.Rows.Count - 2, chProfit_Desc.Index, chProfit_Code.Index)
            End If

        ElseIf e.ColumnIndex = dgcDate.Index Then
            If IsDate(dgvRecords.Item(e.ColumnIndex, e.RowIndex).Value) Then
                dgvRecords.Item(e.ColumnIndex, e.RowIndex).Value = CDate(dgvRecords.Item(e.ColumnIndex, e.RowIndex).Value)
            End If
            LoadPeriod()

            'Auto Entry Grid View Cost Center
            If IsNothing(dgvRecords.Item(chCostCenter.Index, e.RowIndex).Value) Then
                Dim cbvCostCenter As DataGridViewComboBoxCell = LoadCostCenterGridView()
                cbvCostCenter.Value = strDefaultGridView
                dgvRecords.Item(chCostCenter.Index, e.RowIndex) = cbvCostCenter

                Dim dgvCostCenter As String
                dgvCostCenter = dgvRecords.Rows(e.RowIndex).Cells(chCostCenter.Index).Value
                LoadCostCenterCode(dgvCostCenter, e.RowIndex, chCostCenter.Index, chCostID.Index)
            End If

            'Auto Entry Grid View Cost CIP
            If IsNothing(dgvRecords.Item(chCIP_Desc.Index, e.RowIndex).Value) Then
                Dim cbvCIP As DataGridViewComboBoxCell = LoadCIPGridView()
                cbvCIP.Value = strDefaultGridView
                dgvRecords.Item(chCIP_Desc.Index, e.RowIndex) = cbvCIP

                Dim dgvCIP As String
                dgvCIP = dgvRecords.Rows(e.RowIndex).Cells(chCIP_Desc.Index).Value
                LoadCIPCode(dgvCIP, e.RowIndex, chCIP_Desc.Index, chCIP_Code.Index)
            End If

            'Auto Entry Grid View Profit Center
            If IsNothing(dgvRecords.Item(chProfit_Desc.Index, dgvRecords.Rows.Count - 2).Value) Then
                Dim cbvPC As DataGridViewComboBoxCell = LoadProfitCenterGridView()
                cbvPC.Value = strDefaultGridView
                dgvRecords.Item(chProfit_Desc.Index, dgvRecords.Rows.Count - 2) = cbvPC

                Dim dgvPC As String
                dgvPC = dgvRecords.Rows(dgvRecords.Rows.Count - 2).Cells(chProfit_Desc.Index).Value
                LoadProfitCenterCode(dgvPC, dgvRecords.Rows.Count - 2, chProfit_Desc.Index, chProfit_Code.Index)
            End If
        ElseIf e.ColumnIndex = dgcAmount.Index Then
            If IsNumeric(dgvRecords.Item(dgcAmount.Index, e.RowIndex).Value) Then
                If Not dgvRecords.Item(dgcVAT.Index, e.RowIndex).Value Is Nothing AndAlso dgvRecords.Item(dgcVAT.Index, e.RowIndex).Value.ToString <> "" AndAlso dgvRecords.Item(dgcVAT.Index, e.RowIndex).Value = True Then
                    dgvRecords.Item(dgcBase.Index, e.RowIndex).Value = (dgvRecords.Item(dgcAmount.Index, e.RowIndex).Value / 1.12)
                    dgvRecords.Item(dgcInputVAT.Index, e.RowIndex).Value = (dgvRecords.Item(dgcAmount.Index, e.RowIndex).Value / 1.12) * 0.12
                Else
                    dgvRecords.Item(dgcBase.Index, e.RowIndex).Value = (dgvRecords.Item(dgcAmount.Index, e.RowIndex).Value)
                    dgvRecords.Item(dgcInputVAT.Index, e.RowIndex).Value = 0.0
                End If
                dgvRecords.Item(dgcAmount.Index, e.RowIndex).Value = CDec(dgvRecords.Item(dgcAmount.Index, e.RowIndex).Value).ToString("N2")
            End If

            'Auto Entry Grid View Cost Center
            If IsNothing(dgvRecords.Item(chCostCenter.Index, e.RowIndex).Value) Then
                Dim cbvCostCenter As DataGridViewComboBoxCell = LoadCostCenterGridView()
                cbvCostCenter.Value = strDefaultGridView
                dgvRecords.Item(chCostCenter.Index, e.RowIndex) = cbvCostCenter

                Dim dgvCostCenter As String
                dgvCostCenter = dgvRecords.Rows(e.RowIndex).Cells(chCostCenter.Index).Value
                LoadCostCenterCode(dgvCostCenter, e.RowIndex, chCostCenter.Index, chCostID.Index)
            End If

            'Auto Entry Grid View Cost CIP
            If IsNothing(dgvRecords.Item(chCIP_Desc.Index, e.RowIndex).Value) Then
                Dim cbvCIP As DataGridViewComboBoxCell = LoadCIPGridView()
                cbvCIP.Value = strDefaultGridView
                dgvRecords.Item(chCIP_Desc.Index, e.RowIndex) = cbvCIP

                Dim dgvCIP As String
                dgvCIP = dgvRecords.Rows(e.RowIndex).Cells(chCIP_Desc.Index).Value
                LoadCIPCode(dgvCIP, e.RowIndex, chCIP_Desc.Index, chCIP_Code.Index)
            End If

            'Auto Entry Grid View Profit Center
            If IsNothing(dgvRecords.Item(chProfit_Desc.Index, dgvRecords.Rows.Count - 2).Value) Then
                Dim cbvPC As DataGridViewComboBoxCell = LoadProfitCenterGridView()
                cbvPC.Value = strDefaultGridView
                dgvRecords.Item(chProfit_Desc.Index, dgvRecords.Rows.Count - 2) = cbvPC

                Dim dgvPC As String
                dgvPC = dgvRecords.Rows(dgvRecords.Rows.Count - 2).Cells(chProfit_Desc.Index).Value
                LoadProfitCenterCode(dgvPC, dgvRecords.Rows.Count - 2, chProfit_Desc.Index, chProfit_Code.Index)
            End If

        ElseIf e.ColumnIndex = chCostCenter.Index Then
            Dim dgvCostCenter As String
            dgvCostCenter = dgvRecords.Rows(e.RowIndex).Cells(chCostCenter.Index).Value
            LoadCostCenterCode(dgvCostCenter, e.RowIndex, chCostCenter.Index, chCostID.Index)

        ElseIf e.ColumnIndex = chCIP_Desc.Index Then
            Dim dgvCIP As String
            dgvCIP = dgvRecords.Rows(e.RowIndex).Cells(chCIP_Desc.Index).Value
            LoadCIPCode(dgvCIP, e.RowIndex, chCIP_Desc.Index, chCIP_Code.Index)
        ElseIf e.ColumnIndex = chProfit_Desc.Index Then
            Dim dgvPC As String
            dgvPC = dgvRecords.Rows(e.RowIndex).Cells(chProfit_Desc.Index).Value
            LoadProfitCenterCode(dgvPC, e.RowIndex, chProfit_Desc.Index, chProfit_Code.Index)
        End If

        ComputeTotal()
    End Sub
    Private Sub LoadPeriod()
        Dim minPeriod, maxPeriod, tempDate As Date
        For Each row As DataGridViewRow In dgvRecords.Rows
            If IsDate(row.Cells(dgcDate.Index).Value) Then
                tempDate = row.Cells(dgcDate.Index).Value
                If minPeriod = #12:00:00 AM# OrElse tempDate <= minPeriod Then
                    minPeriod = tempDate
                End If
                If maxPeriod = #12:00:00 AM# OrElse tempDate >= maxPeriod Then
                    maxPeriod = tempDate
                End If
            End If
        Next
    End Sub

    Public Sub ComputeTotal()
        Try
            ' COMPUTE TOTAL AMOUNT
            Dim a As Double = 0
            For i As Integer = 0 To dgvRecords.Rows.Count - 1
                If dgvRecords.Item(dgcPayee.Index, i).Value <> "" AndAlso Val(dgvRecords.Item(dgcAmount.Index, i).Value) <> 0 Then
                    a = a + Double.Parse(dgvRecords.Item(dgcAmount.Index, i).Value).ToString("N2")
                End If
            Next
            txtAmount.Text = a.ToString("N2")

            ' COMPUTE TOTAL INPUT VAT 
            Dim b As Double = 0
            For i As Integer = 0 To dgvRecords.Rows.Count - 1
                If dgvRecords.Item(dgcPayee.Index, i).Value <> "" AndAlso Val(dgvRecords.Item(dgcInputVAT.Index, i).Value) <> 0 Then
                    b = b + Double.Parse(dgvRecords.Item(dgcInputVAT.Index, i).Value).ToString("N2")
                End If
            Next
            txtInputVAT.Text = b.ToString("N2")

            ' COMPUTE TOTAL BASE AMOUNT
            Dim c As Double = 0
            For i As Integer = 0 To dgvRecords.Rows.Count - 1
                If dgvRecords.Item(dgcPayee.Index, i).Value <> "" AndAlso Val(dgvRecords.Item(dgcBase.Index, i).Value) <> 0 Then
                    c = c + Double.Parse(dgvRecords.Item(dgcBase.Index, i).Value).ToString("N2")
                End If
            Next
            txtBaseAmount.Text = c.ToString("N2")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function LoadListPCV(Type As String, Text As String, RowInd As Integer) As Boolean
        Dim query As String
        Dim exist As Boolean = False
        Dim filtertype As String = ""
        If Type = "TIN" Then
            filtertype = "TIN_No"
        Else
            filtertype = "VCEName"
        End If
        query = " SELECT DISTINCT MAX(VCECode) AS VCECode, VCEName, MAX(TIN_No) AS TIN_No " & _
                " FROM " & _
                " ( " & _
                "       SELECT VCECode, VCEName, TIN_No FROM viewVCE_Master WHERE  " & filtertype & " LIKE '%' + @Filter + '%'  " & _
                "       UNION ALL " & _
                "       SELECT '', RecordPayee, TIN FROM tblRFP_Details WHERE " & Type & " LIKE '%' + @Filter + '%' " & _
                " ) AS A " & _
                " GROUP BY VCEName, TIN_No "
        SQL.FlushParams()

        SQL.AddParam("@Filter", Text)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            exist = True
        Else
            For Each row As DataGridViewRow In dgvRecords.Rows
                If row.Index <> RowInd Then
                    If Type = "RecordPayee" Then
                        If Not row.Cells(dgcPayee.Index).Value Is Nothing Then
                            If Strings.UCase(row.Cells(dgcPayee.Index).Value).ToString.Contains(Strings.UCase(Text)) Then
                                exist = True
                                Exit For
                            End If
                        End If
                    ElseIf Type = "TIN" Then
                        If Not row.Cells(dgcTIN.Index).Value Is Nothing Then
                            If row.Cells(dgcTIN.Index).Value = Text Then
                                exist = True
                                Exit For
                            End If
                        End If
                    End If
                End If

            Next
        End If
        Return exist
        SQL.FlushParams()
    End Function
    Private Sub LoadType(Optional ByVal SelectedIndex As Integer = 0)
        Try
            Dim dgvCB As DataGridViewComboBoxCell
            dgvCB = dgvRecords.Item(dgcType.Index, SelectedIndex)
            dgvCB.Items.Clear()
            ' ADD ALL WHSEc
            Dim query As String
            query = " SELECT Type FROM tblRFP_Type WHERE Status = 'Active'"
            SQL.ReadQuery(query)
            dgvCB.Items.Clear()
            While SQL.SQLDR.Read
                dgvCB.Items.Add(SQL.SQLDR("Type").ToString)
            End While
            dgvCB.Items.Add("")
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub dgvRecords_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgvRecords.CellValidating
        If e.ColumnIndex = dgcDate.Index Then
            Dim dt As DateTime
            If e.FormattedValue.ToString <> String.Empty AndAlso Not DateTime.TryParse(e.FormattedValue.ToString, dt) Then
                MessageBox.Show("Enter correct Date")
                e.Cancel = True
            End If

        End If
    End Sub

    Private Sub dgvRecords_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvRecords.CellValueChanged
        If dgvRecords.SelectedCells.Count > 0 AndAlso e.ColumnIndex = dgcVAT.Index Then
            If dgvRecords.SelectedCells(0).RowIndex <> -1 Then
                If IsNumeric(dgvRecords.Item(dgcAmount.Index, dgvRecords.SelectedCells(0).RowIndex).Value) Then
                    If dgvRecords.Item(dgcVAT.Index, dgvRecords.SelectedCells(0).RowIndex).Value Then
                        dgvRecords.Item(dgcBase.Index, dgvRecords.SelectedCells(0).RowIndex).Value = (dgvRecords.Item(dgcAmount.Index, dgvRecords.SelectedCells(0).RowIndex).Value / 1.12)
                        dgvRecords.Item(dgcInputVAT.Index, dgvRecords.SelectedCells(0).RowIndex).Value = (dgvRecords.Item(dgcAmount.Index, dgvRecords.SelectedCells(0).RowIndex).Value / 1.12) * 0.12
                    Else
                        dgvRecords.Item(dgcBase.Index, dgvRecords.SelectedCells(0).RowIndex).Value = (dgvRecords.Item(dgcAmount.Index, dgvRecords.SelectedCells(0).RowIndex).Value)
                        dgvRecords.Item(dgcInputVAT.Index, dgvRecords.SelectedCells(0).RowIndex).Value = 0.0
                    End If
                    ComputeTotal()
                End If
            End If
        End If
    End Sub

    Private Sub dgvRecords_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dgvRecords.CurrentCellDirtyStateChanged
        If dgvRecords.SelectedCells.Count > 0 AndAlso dgvRecords.SelectedCells(0).ColumnIndex = dgcVAT.Index Then
            If dgvRecords.SelectedCells(0).RowIndex <> -1 Then
                dgvRecords.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End If
        ElseIf eColIndex = chCostCenter.Index And TypeOf (dgvRecords.CurrentRow.Cells(chCostCenter.Index)) Is DataGridViewComboBoxCell Then
            dgvRecords.EndEdit()
        ElseIf eColIndex = dgcType.Index And TypeOf (dgvRecords.CurrentRow.Cells(dgcType.Index)) Is DataGridViewComboBoxCell Then
            dgvRecords.EndEdit()
        ElseIf eColIndex = chCIP_Desc.Index And TypeOf (dgvRecords.CurrentRow.Cells(chCIP_Desc.Index)) Is DataGridViewComboBoxCell Then
            dgvRecords.EndEdit()
        End If
    End Sub

    Private Sub dgvRecords_DataError(sender As Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvRecords.DataError
        Try

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub dgvRecords_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles dgvRecords.KeyDown
        If e.Shift = True AndAlso e.KeyCode = Keys.Space Then
            e.SuppressKeyPress = True

        ElseIf e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
        ElseIf e.Shift = True AndAlso e.KeyCode = Keys.Down Then
            If Not controlDisabled Then
                If dgvRecords.SelectedCells(0).RowIndex >= 1 AndAlso Not IsNothing(dgvRecords.Item(dgvRecords.SelectedCells(0).ColumnIndex, dgvRecords.SelectedCells(0).RowIndex - 1).Value) Then
                    dgvRecords.SelectedCells(0).Value = dgvRecords.Item(dgvRecords.SelectedCells(0).ColumnIndex, dgvRecords.SelectedCells(0).RowIndex - 1).Value
                End If
            End If
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Delete Then
            dgvRecords.SelectedCells(0).Value = Nothing
        End If
    End Sub

    Private Sub dgvRecords_RowsRemoved(sender As Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgvRecords.RowsRemoved
        Try
            ComputeTotal()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub PrintPCVToolStripMenuItem_Click_1(sender As System.Object, e As System.EventArgs) Handles PrintPCVToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("PCV", TransID)
        f.Dispose()
    End Sub

    Private Sub PCVListToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PCVListToolStripMenuItem.Click
        Dim f As New frmReport_Filter
        f.Report = "PCV List"
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub ToolStripMenuItem1_Click_1(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem1.Click
        If Not AllowAccess("RFP_MNT") Then
            msgRestricted()
        Else
            Dim f As New frmRFP_Type
            f.Type = "PCV"
            f.ShowDialog()
            f.Dispose()
            RefreshType()
        End If
    End Sub

    Private Sub btnUOMGroup_Click(sender As System.Object, e As System.EventArgs) Handles btnUOMGroup.Click
        frmRFP_Type.Show()
    End Sub
End Class