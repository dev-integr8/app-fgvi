Public Class frmPJ
    Dim TransID, JETransiD As String
    Dim PJNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "PJ"
    Dim ColumnPK As String = "PJ_No"
    Dim DBTable As String = "tblPJ"
    Dim TransAuto As Boolean
    Dim MemTransfer As Boolean
    Dim ForApproval As Boolean = False
    Dim BranchTransfer As String
    Dim AccntCode As String
    Dim accntDR, accntCR, accntVAT As String
    Dim LOAN_ID As Integer
    Public interestgen As Boolean = False
    Public interestBranchCode As String = ""
    Dim isReversalEntry As Boolean = False

    Public Overloads Function ShowDialog(ByVal docnumber As String) As Boolean
        TransID = docnumber
        MyBase.ShowDialog()
        Return True
    End Function

   
    Private Sub EnableControl(ByVal Value As Boolean)
        txtVCEName.Enabled = Value
        btnSearchVCE.Enabled = Value
        cbCurrency.Enabled = Value
        txtConversion.Enabled = Value
        dgvEntry.AllowUserToAddRows = Value
        dgvEntry.AllowUserToDeleteRows = Value
        If Value = True Then
            dgvEntry.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
            LoadBranch()
        Else
            dgvEntry.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        dtpDueDate.Enabled = Value
        cbTerms.Enabled = Value
        txtSIRef.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Dim eColIndex As Integer = 0
    Private Sub dgvEntry_CellBeginEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvEntry.CellBeginEdit
        eColIndex = e.ColumnIndex
    End Sub

    Private Sub dgvEntry_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntry.CellEndEdit
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
            ElseIf e.ColumnIndex = chAccntTitle.Index Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value.ToString)
                dgvEntry.Item(chAccntCode.Index, e.RowIndex).Value = f.accountcode
                dgvEntry.Item(chAccntTitle.Index, e.RowIndex).Value = f.accttile
                f.Dispose()
                dgvEntry.Item(chDebit.Index, e.RowIndex).Selected = True



                ''Auto Entry RefN
                'Dim strVCECode As String = ""
                'Dim strAccntCode As String = ""
                'strVCECode = txtVCECode.Text
                'strAccntCode = dgvEntry.Item(chAccntCode.Index, e.RowIndex).Value
                'If dgvEntry.Item(chVCECode.Index, e.RowIndex).Value <> "" Then
                '    strVCECode = dgvEntry.Item(chVCECode.Index, e.RowIndex).Value
                'End If
                'dgvEntry.Item(chRefNo.Index, e.RowIndex).Value = GetRefNo(strVCECode, strAccntCode)
                'Dim strRefNo As String = ""
                'Dim strRefNoLoan As String = ""
                'strRefNo = dgvEntry.Item(chRefNo.Index, e.RowIndex).Value
                'strRefNoLoan = GetRefNoLoan(strRefNo)
                'If strRefNoLoan <> "" Then
                '    dgvEntry.Rows.Add("")
                '    dgvEntry.Item(chVCECode.Index, e.RowIndex + 1).Value = GetRefNoVCECode(strRefNo)
                '    dgvEntry.Item(chVCEName.Index, e.RowIndex + 1).Value = GetVCEName(dgvEntry.Item(chVCECode.Index, e.RowIndex + 1).Value)
                '    dgvEntry.Item(chAccntCode.Index, e.RowIndex + 1).Value = strRefNoLoan
                '    dgvEntry.Item(chAccntTitle.Index, e.RowIndex + 1).Value = GetAccntTitle(dgvEntry.Item(chAccntCode.Index, e.RowIndex + 1).Value)
                '    dgvEntry.Item(chRefNo.Index, e.RowIndex + 1).Value = strRefNo
                'End If


                TotalDBCR()
            ElseIf e.ColumnIndex = chVCECode.Index Or e.ColumnIndex = chVCEName.Index Then
                Dim f As New frmVCE_Search
                f.txtFilter.Text = dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                f.ShowDialog()
                dgvEntry.Item(chVCECode.Index, e.RowIndex).Value = f.VCECode
                dgvEntry.Item(chVCEName.Index, e.RowIndex).Value = f.VCEName
                f.Dispose()

               

                ''Auto Entry RefNo
                'Dim strVCECode As String = ""
                'Dim strAccntCode As String = ""
                'strVCECode = txtVCECode.Text
                'strAccntCode = dgvEntry.Item(chAccntCode.Index, e.RowIndex).Value
                'If dgvEntry.Item(chVCECode.Index, e.RowIndex).Value <> "" Then
                '    strVCECode = dgvEntry.Item(chVCECode.Index, e.RowIndex).Value
                'End If
                'If strAccntCode <> "" Then
                '    dgvEntry.Item(chRefNo.Index, e.RowIndex).Value = GetRefNo(strVCECode, strAccntCode)
                '    Dim strRefNo As String = ""
                '    Dim strRefNoLoan As String = ""
                '    strRefNo = dgvEntry.Item(chRefNo.Index, e.RowIndex).Value
                '    strRefNoLoan = GetRefNoLoan(strRefNo)
                '    If strRefNoLoan <> "" Then
                '        dgvEntry.Rows.Add("")
                '        dgvEntry.Item(chVCECode.Index, e.RowIndex + 1).Value = GetRefNoVCECode(strRefNo)
                '        dgvEntry.Item(chVCEName.Index, e.RowIndex + 1).Value = GetVCEName(dgvEntry.Item(chVCECode.Index, e.RowIndex + 1).Value)
                '        dgvEntry.Item(chAccntCode.Index, e.RowIndex + 1).Value = strRefNoLoan
                '        dgvEntry.Item(chAccntTitle.Index, e.RowIndex + 1).Value = GetAccntTitle(dgvEntry.Item(chAccntCode.Index, e.RowIndex + 1).Value)
                '        dgvEntry.Item(chRefNo.Index, e.RowIndex + 1).Value = strRefNo
                '    End If
                'End If
          

            ElseIf e.ColumnIndex = chRefNo.Index Then
                'Dim strRefNo As String = ""
                'strRefNo = dgvEntry.Item(chRefNo.Index, e.RowIndex).Value
                'If strRefNo <> "" Then
                '    dgvEntry.Item(chVCECode.Index, e.RowIndex).Value = GetRefNoVCECode(strRefNo)
                '    dgvEntry.Item(chVCEName.Index, e.RowIndex).Value = GetVCEName(dgvEntry.Item(chVCECode.Index, e.RowIndex).Value)
                '    dgvEntry.Item(chAccntCode.Index, e.RowIndex).Value = GetRefNoAccntCode(strRefNo)
                '    dgvEntry.Item(chAccntTitle.Index, e.RowIndex).Value = GetAccntTitle(dgvEntry.Item(chAccntCode.Index, e.RowIndex).Value)
                '    Dim strRefNoLoan As String = ""
                '    strRefNoLoan = GetRefNoLoan(strRefNo)
                '    If strRefNoLoan <> "" Then
                '        dgvEntry.Rows.Add("")
                '        dgvEntry.Item(chVCECode.Index, e.RowIndex + 1).Value = GetRefNoVCECode(strRefNo)
                '        dgvEntry.Item(chVCEName.Index, e.RowIndex + 1).Value = GetVCEName(dgvEntry.Item(chVCECode.Index, e.RowIndex + 1).Value)
                '        dgvEntry.Item(chAccntCode.Index, e.RowIndex + 1).Value = strRefNoLoan
                '        dgvEntry.Item(chAccntTitle.Index, e.RowIndex + 1).Value = GetAccntTitle(dgvEntry.Item(chAccntCode.Index, e.RowIndex + 1).Value)
                '        dgvEntry.Item(chRefNo.Index, e.RowIndex + 1).Value = strRefNo
                '    End If
                'End If
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
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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


    Public Function LoadProfitCenterGridView()

        Dim selectSQL As String = " SELECT Code, Description FROM tblPC"
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

    Public Function LoadREFCode(ByVal RowIndex As Integer, ByVal CodeIndex As Integer, ByVal CIPIndex As Integer)


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
        dgvEntry.Rows(RowIndex).Cells(CodeIndex).Value = strDefaultGridView
        dgvEntry.Rows(RowIndex).Cells(CostIndex).Value = strDefaultGridCode

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
        dgvEntry.Rows(RowIndex).Cells(CIPIndex).Value = strDefaultGridView
        dgvEntry.Rows(RowIndex).Cells(CIPIndex).Value = strDefaultGridCode

    End Sub

    Public Sub LoadProfitCenterCode(ByVal ProfitCenter As String, ByVal RowIndex As Integer, ByVal CodeIndex As Integer, ByVal CostIndex As Integer)

        Dim selectSQL As String
        selectSQL = " SELECT Code, Description FROM tblPC WHERE Description = '" & ProfitCenter & "'"
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

    Public Function GetPCName(ByVal PCCode As String) As String
        Dim query As String
        query = " SELECT Description FROM tblPC WHERE Code ='" & PCCode & "' "
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("Description").ToString
        Else
            Return ""
        End If
    End Function

    Public Sub TotalDBCR()
        ' COMPUTE TOTAL DEBIT
        Dim db As Decimal = 0

        For i As Integer = 0 To dgvEntry.Rows.Count - 1
            If dgvEntry.Item(chAccntCode.Index, i).Value <> "" AndAlso Val(dgvEntry.Item(chDebit.Index, i).Value) <> 0 Then
                db = db + Double.Parse(dgvEntry.Item(chDebit.Index, i).Value).ToString("N2")
            End If
        Next
        txtDebit.Text = db.ToString("N2")

        ' COMPUTE TOTAL CREDIT
        Dim b As Decimal = 0

        For i As Integer = 0 To dgvEntry.Rows.Count - 1
            If dgvEntry.Item(chAccntCode.Index, i).Value <> "" AndAlso Val(dgvEntry.Item(chCredit.Index, i).Value) <> 0 Then
                b = b + Double.Parse(dgvEntry.Item(chCredit.Index, i).Value).ToString("N2")
            End If
        Next
        txtCredit.Text = b.ToString("N2")


        txtDifference.Text = CDec(txtDebit.Text - txtCredit.Text).ToString("N2")
    End Sub

    Private Sub dgvEntry_RowsRemoved(sender As System.Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgvEntry.RowsRemoved
        Try
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub ClearText()
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        txtCredit.Text = "0.00"
        txtDebit.Text = "0.00"
        txtDifference.Text = "0.00"
        txtTransNum.Clear()
        txtRemarks.Clear()
        txtStatus.Clear()
        txtSIRef.Clear()
        dgvEntry.Rows.Clear()
        interestgen = False
        interestBranchCode = ""
        MemTransfer = False
        BranchTransfer = ""
        cbTerms.SelectedIndex = -1
        dtpDocDate.MinDate = GetMaxPEC().AddDays(1)
        dtpDocDate.Value = Date.Today.Date
        dtpDueDate.Value = Date.Today.Date
        cbCurrency.Items.Clear()
        txtConversion.Text = ""
    End Sub


    Private Sub SavePJ()
        Dim SQL1 As New SQLControl
        SQL1.BeginTransaction()
        Try
            Dim insertSQL As String
            activityStatus = True
            insertSQL = " INSERT INTO " & _
                        " tblPJ (TransID, PJ_No, VCECode, BranchCode, BusinessCode, DatePJ, TotalAmount, " & _
                        "       Currency, Exchange_Rate, Remarks, LN_Ref, TransAuto, WhoCreated, MemTransfer, Terms, DueDate, SIRef, Status) " & _
                        " VALUES(@TransID, @PJ_No, @VCECode, @BranchCode, @BusinessCode, @DatePJ, @TotalAmount, " & _
                        "       @Currency, @Exchange_Rate, @Remarks, @LN_Ref, @TransAuto, @WhoCreated, @MemTransfer, @Terms, @DueDate, @SIRef, @Status)"
            SQL1.FlushParams()
            SQL1.AddParam("@TransID", TransID)
            SQL1.AddParam("@PJ_No", PJNo)
            SQL1.AddParam("@VCECode", txtVCECode.Text)
            SQL1.AddParam("@BranchCode", IIf(interestBranchCode <> "", interestBranchCode, BranchCode))
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@DatePJ", dtpDocDate.Value.Date)
            SQL1.AddParam("@LN_Ref", LOAN_ID)
            If IsNumeric(txtDebit.Text) Then
                SQL1.AddParam("@TotalAmount", CDec(txtDebit.Text))
            Else
                SQL1.AddParam("@TotalAmount", 0)
            End If
            SQL1.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL1.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@TransAuto", TransAuto)
            SQL1.AddParam("@MemTransfer", MemTransfer)
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.AddParam("@DueDate", dtpDueDate.Value.Date)
            SQL1.AddParam("@Terms", cbTerms.Text)
            SQL1.AddParam("@SIRef", txtSIRef.Text)
            If ForApproval = True Then SQL1.AddParam("@Status", "Draft") Else SQL1.AddParam("@Status", "Active")
            SQL1.ExecNonQuery(insertSQL)

            If LOAN_ID > 0 Then
                Dim updateSQL As String
                updateSQL = " UPDATE tblLoan SET Status ='Released', DateRelease = '" & dtpDocDate.Value.Date & "', RefType = 'PJ', RefTransID = '" & TransID & "' WHERE TransID = '" & LOAN_ID & "'"
                SQL1.ExecNonQuery(updateSQL)
            End If

            JETransiD = GenerateTransID("JE_No", "tblJE_Header")

            insertSQL = " INSERT INTO " & _
                        " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Currency, Exchange_Rate, Remarks, WhoCreated, Status) " & _
                        " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Currency, @Exchange_Rate, @Remarks, @WhoCreated, @Status)"
            SQL1.FlushParams()
            SQL1.AddParam("@JE_No", JETransiD)
            SQL1.AddParam("@AppDate", dtpDocDate.Value.Date)
            SQL1.AddParam("@RefType", "PJ")
            SQL1.AddParam("@RefTransID", TransID)
            SQL1.AddParam("@Book", "Purchase Journal")
            SQL1.AddParam("@TotalDBCR", CDec(txtDebit.Text))
            SQL1.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL1.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@BranchCode", IIf(interestBranchCode <> "", interestBranchCode, BranchCode))
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@WhoCreated", UserID)
            If ForApproval = True Then SQL1.AddParam("@Status", "Draft") Else SQL1.AddParam("@Status", "Saved")
            SQL1.ExecNonQuery(insertSQL)

            ' JETransiD = LoadJE("PJ", TransID)

            Dim strRefNo As String = ""
            Dim VCECode As String = ""

            Dim line As Integer = 1
            For Each item As DataGridViewRow In dgvEntry.Rows
                If item.Cells(chAccntCode.Index).Value <> Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, CostCenter, CIP_Code, BranchCode, ProfitCenter, VATType) " & _
                                " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @CostCenter, @CIP_Code, @BranchCode, @ProfitCenter, @VATType)"
                    SQL1.FlushParams()
                    SQL1.AddParam("@JE_No", JETransiD)
                    SQL1.AddParam("@AccntCode", item.Cells(chAccntCode.Index).Value.ToString)
                    If item.Cells(chVCECode.Index).Value <> Nothing AndAlso item.Cells(chVCECode.Index).Value <> "" Then
                        SQL1.AddParam("@VCECode", item.Cells(chVCECode.Index).Value.ToString)
                        VCECode = item.Cells(chVCECode.Index).Value.ToString
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
                        SQL1.AddParam("@Particulars", "")
                    End If
                    If item.Cells(chCostID.Index).Value <> Nothing AndAlso item.Cells(chCostID.Index).Value <> "" Then
                        SQL1.AddParam("@CostCenter", item.Cells(chCostID.Index).Value.ToString)
                    Else
                        SQL1.AddParam("@CostCenter", "")
                    End If
                    If item.Cells(chCIP_Code.Index).Value <> Nothing AndAlso item.Cells(chCIP_Code.Index).Value <> "" Then
                        SQL1.AddParam("@CIP_Code", item.Cells(chCIP_Code.Index).Value.ToString)
                    Else
                        SQL1.AddParam("@CIP_Code", "")
                    End If
                    If item.Cells(chProfit_Code.Index).Value <> Nothing AndAlso item.Cells(chProfit_Code.Index).Value <> "" Then
                        SQL1.AddParam("@ProfitCenter", item.Cells(chProfit_Code.Index).Value.ToString)
                    Else
                        SQL1.AddParam("@ProfitCenter", "")
                    End If
                    If item.Cells(chRefNo.Index).Value <> Nothing AndAlso item.Cells(chRefNo.Index).Value <> "" Then
                        SQL1.AddParam("@RefNo", item.Cells(chRefNo.Index).Value.ToString)
                        If strRefNo.Length = 0 Then
                            strRefNo = item.Cells(chRefNo.Index).Value.ToString
                        Else
                            strRefNo = strRefNo & "-" & item.Cells(chRefNo.Index).Value.ToString
                        End If
                    Else
                        SQL1.AddParam("@RefNo", "")
                    End If
                    SQL1.AddParam("@LineNumber", line)
                    If item.Cells(chBranchCode.Index).Value <> Nothing AndAlso item.Cells(chBranchCode.Index).Value <> "" Then
                        SQL1.AddParam("@BranchCode", item.Cells(chBranchCode.Index).Value.ToString)
                    Else
                        SQL1.AddParam("@BranchCode", BranchCode)
                    End If
                    If item.Cells(chVATType.Index).Value <> Nothing AndAlso item.Cells(chVATType.Index).Value <> "" Then
                        SQL1.AddParam("@VATType", item.Cells(chVATType.Index).Value.ToString)
                    Else
                        SQL1.AddParam("@VATType", "")
                    End If
                    SQL1.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
            If strRefNo.Contains("LN:") Then
                strRefNo = strRefNo.Replace("LN:", "")
                Dim count As Integer = strRefNo.Split("-").Length - 1
                For i As Integer = 0 To count
                    Dim updateSQL As String
                    updateSQL = " UPDATE tblLoan SET Status ='Released', DateRelease = '" & dtpDocDate.Value.Date & "', RefType = 'PJ', RefTransID = '" & TransID & "' WHERE Loan_No = '" & strRefNo.Split("-")(i) & "'"
                    SQL1.ExecNonQuery(updateSQL)
                Next
            End If

            If MemTransfer = True Then
                Dim updateSQL As String
                updateSQL = " UPDATE tblMember_Master SET BranchCode ='" & BranchTransfer & "' WHERE Member_ID = '" & VCECode & "'"
                SQL1.ExecNonQuery(updateSQL)
            End If
            SQL1.Commit()
        Catch ex As Exception
            SQL1.Rollback()
            TransID = ""
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "PJ_No", txtTransNum.Text, BusinessType, IIf(interestBranchCode <> "", interestBranchCode, BranchCode), "", activityStatus)
            SQL1.FlushParams()
        End Try
    End Sub

    Private Sub UpdatePJ()
        Dim SQL As New SQLControl
        SQL.BeginTransaction()
        Try
            Dim insertSQL, updateSQL, deleteSQL As String
            activityStatus = True
            updateSQL = " UPDATE tblPJ " & _
                        " SET    PJ_No = @PJ_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode,  VCECode = @VCECode, DatePJ = @DatePJ, " & _
                        "        TotalAmount = @TotalAmount, Currency = @Currency, Exchange_Rate = @Exchange_Rate,  Remarks = @Remarks,  LN_Ref = @LN_Ref, WhoModified = @WhoModified, DateModified = GETDATE(), " & _
                        "       MemTransfer = @MemTransfer, Terms = @Terms, DueDate = @DueDate, SIRef = @SIRef " & _
                        " WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@PJ_No", PJNo)
            SQL.AddParam("@BranchCode", IIf(interestBranchCode <> "", interestBranchCode, BranchCode))
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DatePJ", dtpDocDate.Value.Date)
            SQL.AddParam("@LN_Ref", LOAN_ID)
            If IsNumeric(txtDebit.Text) Then
                SQL.AddParam("@TotalAmount", CDec(txtDebit.Text))
            Else
                SQL.AddParam("@TotalAmount", 0)
            End If
            SQL.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@MemTransfer", MemTransfer)
            SQL.AddParam("@WhoModified", UserID)
            SQL.AddParam("@DueDate", dtpDueDate.Value.Date)
            SQL.AddParam("@Terms", cbTerms.Text)
            SQL.AddParam("@SIRef", txtSIRef.Text)
            SQL.ExecNonQuery(updateSQL)

            If LOAN_ID > 0 Then
                updateSQL = " UPDATE tblLoan SET Status ='Released', DateRelease = '" & dtpDocDate.Value.Date & "', RefType = 'PJ', RefTransID = '" & TransID & "' WHERE TransID = '" & LOAN_ID & "'"
                SQL.ExecNonQuery(updateSQL)
            End If

            JETransiD = LoadJE("PJ", TransID)

            If JETransiD = 0 Then
                insertSQL = " INSERT INTO " & _
                            " tblJE_Header (AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Currency, Exchange_Rate, Remarks, WhoCreated) " & _
                            " VALUES(@AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Currency, @Exchange_Rate, @Remarks, @WhoCreated)"
                SQL.FlushParams()
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "PJ")
                SQL.AddParam("@RefTransID", TransID)
                SQL.AddParam("@Book", "Purchase Journal")
                SQL.AddParam("@TotalDBCR", CDec(txtCredit.Text))
                SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", IIf(interestBranchCode <> "", interestBranchCode, BranchCode))
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@WhoCreated", UserID)
                SQL.ExecNonQuery(insertSQL)

                JETransiD = LoadJE("PJ", TransID)
            Else
                updateSQL = " UPDATE tblJE_Header " & _
                            " SET    AppDate = @AppDate, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " & _
                            "        RefType = @RefType, RefTransID = @RefTransID, Book = @Book, TotalDBCR = @TotalDBCR,  Currency = @Currency, Exchange_Rate = @Exchange_Rate," & _
                            "        Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE() " & _
                            " WHERE  JE_No = @JE_No "
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransiD)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "PJ")
                SQL.AddParam("@RefTransID", TransID)
                SQL.AddParam("@Book", "Purchase Journal")
                SQL.AddParam("@TotalDBCR", CDec(txtCredit.Text))
                SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", IIf(interestBranchCode <> "", interestBranchCode, BranchCode))
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@WhoModified", UserID)
                SQL.ExecNonQuery(updateSQL)
            End If

            Dim line As Integer = 1

            ' DELETE ACCOUNTING ENTRIES
            deleteSQL = " DELETE FROM tblJE_Details  WHERE  JE_No = @JE_No "
            SQL.FlushParams()
            SQL.AddParam("@JE_No", JETransiD)
            SQL.ExecNonQuery(deleteSQL)

            Dim strRefNo As String = ""

            ' INSERT NEW ENTRIES
            For Each item As DataGridViewRow In dgvEntry.Rows
                If item.Cells(chAccntCode.Index).Value <> Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, CostCenter, CIP_Code, BranchCode, ProfitCenter, VATType) " & _
                                " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @CostCenter, @CIP_Code, @BranchCode, @ProfitCenter, @VATType)"
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
                        SQL.AddParam("@Particulars", "")
                    End If
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
                    If item.Cells(chRefNo.Index).Value <> Nothing AndAlso item.Cells(chRefNo.Index).Value <> "" Then
                        SQL.AddParam("@RefNo", item.Cells(chRefNo.Index).Value.ToString)
                        If strRefNo.Length = 0 Then
                            strRefNo = item.Cells(chRefNo.Index).Value.ToString
                        Else
                            strRefNo = strRefNo & "-" & item.Cells(chRefNo.Index).Value.ToString
                        End If
                    Else
                        SQL.AddParam("@RefNo", "")
                    End If
                    SQL.AddParam("@LineNumber", line)
                    If item.Cells(chBranchCode.Index).Value <> Nothing AndAlso item.Cells(chBranchCode.Index).Value <> "" Then
                        SQL.AddParam("@BranchCode", item.Cells(chBranchCode.Index).Value.ToString)
                    Else
                        SQL.AddParam("@BranchCode", BranchCode)
                    End If
                    If item.Cells(chVATType.Index).Value <> Nothing AndAlso item.Cells(chVATType.Index).Value <> "" Then
                        SQL.AddParam("@VATType", item.Cells(chVATType.Index).Value.ToString)
                    Else
                        SQL.AddParam("@VATType", "")
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
                        updateSQL1 = " UPDATE tblLoan SET Status ='Released', DateRelease = '" & dtpDocDate.Value.Date & "', RefType = 'PJ', RefTransID = '" & TransID & "' WHERE Loan_No = '" & strRefNo.Split("-")(i) & "'"
                        SQL.ExecNonQuery(updateSQL1)
                Next
            End If
            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "PJ_No", txtTransNum.Text, BusinessType, IIf(interestBranchCode <> "", interestBranchCode, BranchCode), "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub LoadPJ(ByVal ID As String)
        Dim query, terms, Currency, RefID, RefType As String
        query = " SELECT  TransID, PJ_No, tblPJ.VCECode, VCEName, DatePJ, TotalAmount, Remarks, ISNULL(LN_Ref,0) as LN_Ref, tblPJ.Status, ISNULL(MemTransfer,0) AS MemTransfer, tblPJ.Terms, ISNULL(DueDate,'1900-01-01') AS DueDate, ISNULL(SIRef, '') as SIRef, Currency, ISNULL(Exchange_Rate,0) AS Exchange_Rate ,    " & _
                " ISNULL(Reftype,'') AS Reftype, ISNULL(RefID,0) AS RefID " & _
                " FROM    tblPJ LEFT JOIN viewVCE_Master " & _
                " ON      tblPJ.VCECode = viewVCE_Master.VCECode " & _
                " WHERE   TransID ='" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            PJNo = SQL.SQLDR("PJ_No").ToString
            LOAN_ID = SQL.SQLDR("LN_Ref").ToString
            txtTransNum.Text = PJNo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
            disableEvent = True
            dtpDocDate.Value = SQL.SQLDR("DatePJ")
            dtpDueDate.Value = SQL.SQLDR("DueDate")
            terms = SQL.SQLDR("Terms").ToString
            Currency = SQL.SQLDR("Currency").ToString
            txtConversion.Text = CDec(SQL.SQLDR("Exchange_Rate")).ToString("N4")
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            MemTransfer = SQL.SQLDR("MemTransfer").ToString
            txtSIRef.Text = SQL.SQLDR("SIRef").ToString
            RefID = SQL.SQLDR("RefID")
            RefType = SQL.SQLDR("RefType")
            cbTerms.Text = terms

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
            txtLoanRef.Text = GetLoanNo(LOAN_ID)
            LoadAccountingEntry(TransID)

            tsbCancel.Text = "Cancel"
            ' TOOLSTRIP BUTTONS
            If txtStatus.Text = "Cancelled" Then
                tsbEdit.Enabled = False
                tsbCancel.Text = "Un-Can"
                If RefID <> 0 Or RefType <> "" Then
                    tsbEdit.Enabled = False
                    tsbCancel.Enabled = False
                    tsbDelete.Enabled = False
                Else
                    tsbCancel.Enabled = True
                    tsbDelete.Enabled = True
                End If
            Else

                If RefID <> 0 Or RefType <> "" Then
                    tsbEdit.Enabled = False
                    tsbCancel.Enabled = False
                    tsbDelete.Enabled = False
                Else
                    tsbEdit.Enabled = True
                    tsbCancel.Enabled = True
                    tsbDelete.Enabled = True
                End If
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
                tsbDelete.Enabled = False
            End If
            EnableControl(False)
        Else
            ClearText()
        End If
    End Sub

    Private Function GetLoanNo(ByVal ID As Integer) As String
        Dim query As String
        query = " SELECT Loan_No FROM tblLoan WHERE TransID = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("Loan_No")
        Else
            Return 0
        End If
    End Function

    Private Sub LoadAccountingEntry(ByVal TransID As Integer)
        Try
            Dim query As String
            query = " SELECT JE_No, VCECode, VCEName, AccntCode, AccntTitle, Particulars, RefNo," & _
                    "        ISNULL(Debit,0) AS Debit, ISNULL(Credit,0) AS Credit, CostCenter, CIP_Code, BranchCode, ProfitCenter, VATType " & _
                    " FROM   View_GL_Transaction  " & _
                    " WHERE JE_No = (SELECT  JE_No FROM tblJE_Header WHERE RefType = 'PJ' AND RefTransID = " & TransID & " AND isUpload <> 1) "
            SQL.ReadQuery(query)
            dgvEntry.Rows.Clear()
            Dim rowsCount As Integer = 0
            If SQL.SQLDR.HasRows Then

                While SQL.SQLDR.Read

                    JETransiD = SQL.SQLDR("JE_No").ToString
                    dgvEntry.Rows.Add(SQL.SQLDR("AccntCode").ToString)
                    dgvEntry.Rows(rowsCount).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccntTitle").ToString
                    dgvEntry.Rows(rowsCount).Cells(chDebit.Index).Value = IIf(SQL.SQLDR("Debit") = 0, "", CDec(SQL.SQLDR("Debit")).ToString("N2"))
                    dgvEntry.Rows(rowsCount).Cells(chCredit.Index).Value = IIf(SQL.SQLDR("Credit") = 0, "", CDec(SQL.SQLDR("Credit")).ToString("N2"))
                    dgvEntry.Rows(rowsCount).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                    dgvEntry.Rows(rowsCount).Cells(chVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                    dgvEntry.Rows(rowsCount).Cells(chParticulars.Index).Value = SQL.SQLDR("Particulars").ToString
                    dgvEntry.Rows(rowsCount).Cells(chRefNo.Index).Value = SQL.SQLDR("RefNo").ToString
                    dgvEntry.Rows(rowsCount).Cells(chBranchCode.Index).Value = SQL.SQLDR("BranchCode").ToString
                    dgvEntry.Rows(rowsCount).Cells(chVATType.Index).Value = SQL.SQLDR("VATType").ToString
                    dgvEntry.Rows(rowsCount).Cells(chCostID.Index).Value = SQL.SQLDR("CostCenter").ToString
                    dgvEntry.Rows(rowsCount).Cells(chCost_Center.Index).Value = GetCCName(SQL.SQLDR("CostCenter").ToString)
                    dgvEntry.Rows(rowsCount).Cells(chProfit_Code.Index).Value = SQL.SQLDR("ProfitCenter").ToString
                    dgvEntry.Rows(rowsCount).Cells(chProfit_Center.Index).Value = GetPCName(SQL.SQLDR("ProfitCenter").ToString)
                    dgvEntry.Rows(rowsCount).Cells(chCIP_Code.Index).Value = SQL.SQLDR("CIP_Code").ToString
                    dgvEntry.Rows(rowsCount).Cells(chCIP_Desc.Index).Value = GetCIPName(SQL.SQLDR("CIP_Code").ToString)

                   
                    rowsCount += 1
                End While

                LoadBranch()
                TotalDBCR()
            Else
                JETransiD = 0
                dgvEntry.Rows.Clear()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("PJ_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("PJ")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadPJ(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("PJ_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            PJNo = ""

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
            EnableControl(True)

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
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
    '                            " WHERE     " & ColumnPK & " LIKE '" & prefix & "%'   AND TransAuto = 1 AND BranchCode = '" & IIf(interestBranchCode <> "", interestBranchCode, BranchCode) & "'"
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
    '                                " WHERE     " & ColumnPK & " = '" & TransNum & "' AND BranchCode = '" & IIf(interestBranchCode <> "", interestBranchCode, BranchCode) & "'"
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
        If Not AllowAccess("PJ_EDIT") Then
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
            ElseIf CDec(txtDebit.Text) <> CDec(txtCredit.Text) Then
                MsgBox("Please check the Debit and Credit Amount!", MsgBoxStyle.Exclamation)
            ElseIf txtConversion.Visible = True And txtConversion.Text = "" Then
                MsgBox("Please check exchange rate!", MsgBoxStyle.Exclamation)
            ElseIf txtConversion.Visible = True And txtConversion.Text = "" Then
                MsgBox("Please check exchange rate!", MsgBoxStyle.Exclamation)
            ElseIf dgvEntry.Rows.Count = 0 Then
                MsgBox("No entries, Please check!", MsgBoxStyle.Exclamation)
            ElseIf TransAuto = False AndAlso IfExist(txtTransNum.Text) And TransID = "" Then
                MsgBox("PJ" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
            ElseIf TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID("TransID", DBTable)
                    If TransAuto Then
                        PJNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    Else
                        PJNo = txtTransNum.Text
                    End If
                    txtTransNum.Text = PJNo
                    SavePJ()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    LoadPJ(TransID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    If PJNo = txtTransNum.Text Then
                        PJNo = txtTransNum.Text
                        UpdatePJ()
                        Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                        PJNo = txtTransNum.Text
                        LoadPJ(TransID)
                    Else
                        If Not IfExist(txtTransNum.Text) Then
                            PJNo = txtTransNum.Text
                            UpdatePJ()
                            Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                            PJNo = txtTransNum.Text
                            LoadPJ(TransID)
                        Else
                            MsgBox("PJ" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
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
        query = " SELECT * FROM tblPJ WHERE PJ_No ='" & ID & "'  "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("PJ_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If txtStatus.Text <> "Cancelled" AndAlso MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim updateSQL As String
                        updateSQL = " UPDATE  tblPJ SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        SQL.AddParam("@TransID", TransID)
                        SQL.ExecNonQuery(updateSQL)

                        updateSQL = " UPDATE  tblJE_Header SET Status ='Cancelled' WHERE RefTransID = @RefTransID  AND RefType ='PJ' "
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


                        If LOAN_ID > 0 Then
                            updateSQL = " UPDATE tblLoan SET Status ='Approved', DateRelease = '', RefType = '', RefTransID = '' WHERE TransID = '" & LOAN_ID & "'"
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

                        PJNo = txtTransNum.Text
                        LoadPJ(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "PJ_No", PJNo, BusinessType, IIf(interestBranchCode <> "", interestBranchCode, BranchCode), "", activityStatus)
                        SQL.FlushParams()
                    End Try
                ElseIf txtStatus.Text = "Cancelled" AndAlso MsgBox("Are you sure you want to un-cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim updateSQL As String
                        updateSQL = " UPDATE  tblPJ SET Status ='Active' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        SQL.AddParam("@TransID", TransID)
                        SQL.ExecNonQuery(updateSQL)

                        updateSQL = " UPDATE  tblJE_Header SET Status ='Saved' WHERE RefTransID = @RefTransID  AND RefType ='PJ' "
                        SQL.FlushParams()
                        SQL.AddParam("@RefTransID", TransID)
                        SQL.ExecNonQuery(updateSQL)

                        If isReversalEntry = True Then
                            Dim insertSQL As String
                            insertSQL = " DELETE FROM tblJE_Details " &
                                        " WHERE JE_No IN (SELECT DISTINCT JE_No FROM tblJE_Header WHERE RefType ='" & ModuleID & "'  AND RefTransID ='" & TransID & "') AND LineNumber = 0 "
                            SQL.ExecNonQuery(insertSQL)
                        End If
                        Msg("Record un-cancelled successfully", MsgBoxStyle.Information)


                        If LOAN_ID > 0 Then
                            updateSQL = " UPDATE tblLoan SET Status ='Released', DateRelease = '" & dtpDocDate.Value & "', RefType = 'PJ' RefTransID = '" & TransID & "' WHERE TransID = '" & LOAN_ID & "'"
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

                        PJNo = txtTransNum.Text
                        LoadPJ(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "PJ_No", PJNo, BusinessType, IIf(interestBranchCode <> "", interestBranchCode, BranchCode), "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("PJ", TransID)
        f.Dispose()
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If PJNo <> "" Then
            Dim query As String
            query = "   SELECT Top 1 TransID FROM tblPJ  " & _
                    "   INNER JOIN tblBranch  ON	          " & _
                    "   tblPJ.BranchCode = tblBranch.BranchCode    " & _
                    "     WHERE          " & _
                    " 	( tblPJ.BranchCode IN  " & _
                    " 	  (SELECT    DISTINCT tblBranch.BranchCode  " & _
                    " 	  FROM      tblBranch   " & _
                    " 	  INNER JOIN tblUser_Access    ON          " & _
                    " 	  tblBranch.BranchCode = tblUser_Access.Code   " & _
                    " 	   AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                    " 	   AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                    " 	   WHERE     UserID ='" & UserID & "' " & _
                    " 	   ) " & _
                    "     )   " & _
                    "    AND PJ_No < '" & PJNo & "' ORDER BY PJ_No DESC  "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadPJ(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If PJNo <> "" Then
            Dim query As String
            query = "   SELECT Top 1 TransID FROM tblPJ  " & _
                    "   INNER JOIN tblBranch  ON	          " & _
                    "   tblPJ.BranchCode = tblBranch.BranchCode    " & _
                    "     WHERE          " & _
                    " 	( tblPJ.BranchCode IN  " & _
                    " 	  (SELECT    DISTINCT tblBranch.BranchCode  " & _
                    " 	  FROM      tblBranch   " & _
                    " 	  INNER JOIN tblUser_Access    ON          " & _
                    " 	  tblBranch.BranchCode = tblUser_Access.Code   " & _
                    " 	   AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                    " 	   AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                    " 	   WHERE     UserID ='" & UserID & "' " & _
                    " 	   ) " & _
                    "     )   " & _
                    "    AND PJ_No > '" & PJNo & "' ORDER BY PJ_No ASC  "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadPJ(TransID)
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
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbDelete.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadPJ(TransID)
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

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub frmJV_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub dgvEntry_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntry.CellContentClick
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
                dgvEntry.Rows.Add(f.TAX_IV)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(f.TAX_IV)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(f.VATAmount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = "Input VAT"
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = f.VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(f.VCECode)
            End If

            If f.EWTAmount <> 0 Then
               dgvEntry.Rows.Add(f.TAX_EWT)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(f.TAX_EWT)
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(f.EWTAmount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVATType.Index).Value = "EWT"
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = f.VCECode
                dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(f.VCECode)
            End If
            dgvEntry.Rows.Add("")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(VATamount - f.EWTAmount).ToString("N2")
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = f.VCECode
            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(f.VCECode)

            TotalDBCR()
        End If
    End Sub

    Private Sub dgvEntry_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dgvEntry.CurrentCellDirtyStateChanged

    End Sub

    Private Sub btnSearchVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchVCE.Click
        Dim f As New frmVCE_Search
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        txtVCEName.Text = f.VCEName
        If txtVCECode.Text <> "" Then
            Dim query As String
            query = " SELECT  Terms" & _
                " FROM     tblVCE_Master " & _
                " WHERE    VCECode = '" & txtVCECode.Text & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                cbTerms.Text = SQL.SQLDR("Terms").ToString
            End If
        End If
        LoadCurrency()
        f.Dispose()
    End Sub

    Private Sub txtVCEName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.txtFilter.Text = txtVCEName.Text
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            txtVCEName.Text = f.VCEName
            If txtVCECode.Text <> "" Then
                Dim query As String
                query = " SELECT  Terms" & _
                    " FROM     tblVCE_Master " & _
                    " WHERE    VCECode = '" & txtVCECode.Text & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    cbTerms.Text = SQL.SQLDR("Terms").ToString
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

    Private Sub FromLoansToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromLoansToolStripMenuItem.Click
        If Not AllowAccess("JV_LOAN") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.cbFilter.SelectedItem = "Status"
            f.txtFilter.Text = "Approve"
            f.txtFilter.Enabled = False
            f.cbFilter.Enabled = False
            f.btnSearch.Enabled = False
            f.ShowDialog("Copy Loan DR")

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

    Sub LoadLoan(ByVal Loan As String)
        Try
            Dim LoanAccount, IntIncomeAccount, UnearnedAccount, IntRecAccount, Loan_No, VCECode, IntAmortMethod, BranchCode As String
            Dim SetupUnearned As Boolean
            Dim LoanAmount, LoanPayable, IntAmount, cashAmount As Decimal
            Dim query As String
            query = " SELECT	tblLoan.LoanCode, SetupUnearned, CASE WHEN (SELECT Employer_Name FROM tblCompany WHERE Employer_Name LIKE '%lending%') IS NULL THEN LoanAccount ELSE '11702' END, IntIncomeAccount, UnearnedAccount, IntRecAccount, " & _
                    " 		    LoanAmount, IntAmount, tblLoan.IntAmortMethod, Loan_No, VCECode, LoanPayable, tblLoan.BranchCode " & _
                    " FROM	    tblLoan_Type INNER JOIN tblLoan " & _
                    " ON		tblLoan_Type.LoanCode = tblLoan.LoanCode " & _
                    " WHERE     TransID = '" & Loan & "' "
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
                    LoanPayable = .Item(11)
                    BranchCode = .Item(12)
                    txtLoanRef.Text = Loan_No
                    If IntAmortMethod = "Less to Proceeds" Then    ' IF INTEREST IS LESS TO PROCEED 
                        ' LOAN RECEIVABLE ENTRY
                        dgvEntry.Rows.Add("")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = LoanAccount
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(LoanAccount)
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(LoanPayable).ToString("N2")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = "0.00"
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(VCECode)
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRefNo.Index).Value = "LN:" & Loan_No
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chBranchCode.Index).Value = BranchCode


                        ' INTEREST INCOME ENTRY
                        dgvEntry.Rows.Add("")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = IntIncomeAccount
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(IntIncomeAccount)
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(IntAmount).ToString("N2")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = "0.00"
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(VCECode)
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRefNo.Index).Value = Loan_No
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chBranchCode.Index).Value = BranchCode

                        cashAmount = LoanAmount - IntAmount
                    ElseIf IntAmortMethod = "Add On" Then    ' IF INTEREST IS Add On
                        ' LOAN RECEIVABLE ENTRY
                        dgvEntry.Rows.Add("")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = LoanAccount
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(LoanAccount)
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(LoanPayable).ToString("N2")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = "0.00"
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(VCECode)
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRefNo.Index).Value = "LN:" & Loan_No
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chBranchCode.Index).Value = BranchCode
                        ' INTEREST INCOME ENTRY
                        dgvEntry.Rows.Add(IntIncomeAccount, GetAccntTitle(IntIncomeAccount), "0.00", CDec(IntAmount).ToString("N2"), "", VCECode, GetVCEName(VCECode), Loan_No, "", "", "", "", BranchCode)
                        cashAmount = LoanAmount - IntAmount
                    ElseIf IntAmortMethod = "Amortize" Then
                        If SetupUnearned = False Then  ' IF WITHOUT SETUP OF UNEARNED INCOME
                            ' LOAN RECEIVABLE ENTRY
                            dgvEntry.Rows.Add(LoanAccount, GetAccntTitle(LoanAccount), CDec(LoanAmount).ToString("N2"), "0.00", "", VCECode, GetVCEName(VCECode), "LN:" & Loan_No, "", "", "", "", BranchCode)
                        ElseIf SetupUnearned = True AndAlso LoanAccount = IntIncomeAccount Then  ' IF WITH SETUP OF UNEARNED INCOME AND INT. REC IS SAME AS LOAN REC. ACCOUNT
                            ' LOAN RECEIVABLE ENTRY
                            dgvEntry.Rows.Add(LoanAccount, GetAccntTitle(LoanAccount), CDec(LoanAmount + IntAmount).ToString("N2"), "0.00", "", VCECode, GetVCEName(VCECode), "LN:" & Loan_No, "", "", "", "", BranchCode)
                            ' UNEARNED INCOME ENTRY
                            dgvEntry.Rows.Add(UnearnedAccount, GetAccntTitle(UnearnedAccount), "0.00", CDec(IntAmount).ToString("N2"), "", VCECode, GetVCEName(VCECode), Loan_No, "", "", "", "", BranchCode)
                        ElseIf SetupUnearned = True AndAlso LoanAccount <> IntIncomeAccount Then  ' IF WITH SETUP OF UNEARNED INCOME AND INT. REC IS SAME AS LOAN REC. ACCOUNT
                            ' LOAN RECEIVABLE ENTRY
                            dgvEntry.Rows.Add("")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = LoanAccount
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(LoanAccount)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(LoanAmount).ToString("N2")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = "0.00"
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(VCECode)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRefNo.Index).Value = "LN:" & Loan_No
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chBranchCode.Index).Value = BranchCode

                           
                            ' INTEREST RECEIVABLE ENTRY
                            dgvEntry.Rows.Add("")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = IntIncomeAccount
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(IntIncomeAccount)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = CDec(IntAmount).ToString("N2")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = "0.00"
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(VCECode)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRefNo.Index).Value = Loan_No
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chBranchCode.Index).Value = BranchCode

                          

                            ' UNEARNED INCOME ENTRY          
                            dgvEntry.Rows.Add("")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = UnearnedAccount
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(UnearnedAccount)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = "0.00"
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(IntAmount).ToString("N2")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = VCECode
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(VCECode)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRefNo.Index).Value = Loan_No
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chBranchCode.Index).Value = BranchCode

                        End If
                        cashAmount = LoanAmount
                    End If
                End With


                query = "    SELECT TransID, AccountCode, Amount, Description, VCECode, " & _
                        "       CASE WHEN SetupCharges = 1  " & _
                        " 	    THEN '' ELSE   RefID END AS RefID    " & _
                        "    FROM tblLoan_Details " & _
                        "    WHERE	    tblLoan_Details.TransID = '" & Loan & "' AND tblLoan_Details.AmortMethod IN ( 'Less to Proceeds' , 'Add On') "

                SQL.GetQuery(query)
                If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                        If row(2) > 0 Then
                            dgvEntry.Rows.Add("")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = row(1).ToString
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(row(1).ToString)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chDebit.Index).Value = "0.00"
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chCredit.Index).Value = CDec(row(2)).ToString("N2")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = row(4).ToString
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = GetVCEName(row(4).ToString)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRefNo.Index).Value = row(5).ToString
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chBranchCode.Index).Value = BranchCode

                            cashAmount -= row(2)
                        End If
                    Next
                End If
            End If
            LoadBranch()
            TotalDBCR()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub tsbDelete_Click(sender As System.Object, e As System.EventArgs) Handles tsbDelete.Click
        If MsgBox("Are you sure you want to delete this transaction?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Delete") = MsgBoxResult.Yes Then
            Dim deleteSQL As String = ""
            deleteSQL = "DELETE FROM tblJE_Header WHERE RefType = 'PJ' AND RefTransID = '" & TransID & "'"
            SQL.ExecNonQuery(deleteSQL)
            MsgBox("Successfully Deleted!", MsgBoxStyle.Information, "Delete")
            tsbNew.PerformClick()
        End If
    End Sub

    Private Sub GroupBox1_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub FromFundsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromFundsToolStripMenuItem.Click
        frmFund_Copy.strType = "JV"
        frmFund_Copy.ShowDialog()
    End Sub

    Private Sub dgvEntry_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles dgvEntry.KeyDown
        If e.KeyCode = Keys.Enter Then
            Select Case dgvEntry.SelectedCells(0).ColumnIndex
                Case chRefNo.Index
                    Dim f As New frmSeachSL
                    f.ShowDialog()
                    If f.strVCECode <> "" Then
                        dgvEntry.Rows.Add("")
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = f.strVCECode
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = f.strVCEName
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = f.strAccntCode
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = f.strAccntTitle
                        dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRefNo.Index).Value = f.strRefNo
                        Dim selectSQL As String = " SELECT * FROM tblLoan_Type WHERE LoanAccount = '" & f.strAccntCode & "' "
                        SQL.ReadQuery(selectSQL)
                        If SQL.SQLDR.Read Then
                            dgvEntry.Rows.Add("")
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCECode.Index).Value = f.strVCECode
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chVCEName.Index).Value = f.strVCEName
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntCode.Index).Value = SQL.SQLDR("IntIncomeAccount").ToString
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chAccntTitle.Index).Value = GetAccntTitle(SQL.SQLDR("IntIncomeAccount").ToString)
                            dgvEntry.Rows(dgvEntry.Rows.Count - 2).Cells(chRefNo.Index).Value = f.strRefNo
                        End If
                    End If
            End Select
        End If
    End Sub

    Private Sub dtpDocDate_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpDocDate.ValueChanged
        If disableEvent = False Then
            If TransID = "" Then
                txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
            End If
            Dim query, DateMode As String
            Dim Days As Integer = 0
            query = " SELECT  Description, Days, DateMode " & _
                    " FROM    tblTerms " & _
                    " WHERE   tblTerms.Status = 'Active' AND Description = '" & cbTerms.SelectedItem & "'"
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                DateMode = SQL.SQLDR("DateMode")
                Days = SQL.SQLDR("Days")
                If DateMode = "Year" Then
                    dtpDueDate.Value = DateAdd(DateInterval.Year, CDec(Days), dtpDocDate.Value.Date) ' GET MATURITY DATE BASED ON TERMS AND STARTING DATE
                ElseIf DateMode = "Month" Then
                    dtpDueDate.Value = DateAdd(DateInterval.Month, CDec(Days), dtpDocDate.Value.Date) ' GET MATURITY DATE BASED ON TERMS AND STARTING DATE
                Else
                    dtpDueDate.Value = DateAdd(DateInterval.Day, CDec(Days), dtpDocDate.Value.Date) ' GET MATURITY DATE BASED ON TERMS AND STARTING DATE
                End If
            End If
        End If
    End Sub

    Private Sub FromSavingsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromSavingsToolStripMenuItem.Click
        frmFund_Copy.strType = "Savings"
        frmFund_Copy.ShowDialog()
    End Sub

    Private Sub FromMemberToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromMemberToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.txtFilter.Enabled = True
        f.cbFilter.Enabled = True
        f.btnSearch.Enabled = True

        f.ShowDialog("Copy Member")
        LoadMember(f.transID, f.BranchCode)
        f.Dispose()
    End Sub

    Sub LoadMember(ByVal MemID As String, ByVal BranchCode As String)
        Try
            'Transfer Branch
            Dim query As String
            Dim rowsCount As Integer = 0
            BranchTransfer = BranchCode
            query = " SELECT        dbo.tblJE_Details.VCECode, dbo.viewVCE_Master.VCEName, dbo.tblJE_Details.AccntCode, dbo.tblCOA_Master.AccountTitle, CASE WHEN SUM(Debit - Credit) > 0 THEN SUM(Debit - Credit) ELSE 0 END AS Credit, " & _
                    "                          CASE WHEN SUM(Credit - Debit) > 0 THEN SUM(Credit - Debit) ELSE 0 END AS Debit, ISNULL(dbo.tblJE_Details.BranchCode, dbo.tblJE_Header.BranchCode) AS BranchCode" & _
                    " FROM            dbo.tblJE_Details INNER JOIN" & _
                    "                          dbo.viewVCE_Master ON dbo.tblJE_Details.VCECode = dbo.viewVCE_Master.VCECode INNER JOIN" & _
                    "                         dbo.tblCOA_Master ON dbo.tblJE_Details.AccntCode = dbo.tblCOA_Master.AccountCode  INNER JOIN " & _
                    "                         dbo.tblJE_Header ON dbo.tblJE_Header.JE_No = dbo.tblJE_Details.JE_No " & _
                    " 						 WHERE tblJE_Details.AccntCode IN (SELECT AccntCode FROM tblDefault_TransferAccnt) AND  dbo.tblJE_Details.VCECode = '" & MemID & "' " & _
                     " GROUP BY dbo.tblJE_Details.VCECode, dbo.viewVCE_Master.VCEName, dbo.tblJE_Details.AccntCode, dbo.tblCOA_Master.AccountTitle, ISNULL(dbo.tblJE_Details.BranchCode, dbo.tblJE_Header.BranchCode) "
            SQL.ReadQuery(query)
            dgvEntry.Rows.Clear()
            If SQL.SQLDR.HasRows Then

                While SQL.SQLDR.Read
                    dgvEntry.Rows.Add(SQL.SQLDR("AccntCode").ToString)
                    dgvEntry.Rows(rowsCount).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                    dgvEntry.Rows(rowsCount).Cells(chDebit.Index).Value = CDec(SQL.SQLDR("Debit")).ToString("N2")
                    dgvEntry.Rows(rowsCount).Cells(chCredit.Index).Value = CDec(SQL.SQLDR("Credit")).ToString("N2")
                    dgvEntry.Rows(rowsCount).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                    dgvEntry.Rows(rowsCount).Cells(chVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                    dgvEntry.Rows(rowsCount).Cells(chParticulars.Index).Value = ""
                    dgvEntry.Rows(rowsCount).Cells(chRefNo.Index).Value = ""
                    dgvEntry.Rows(rowsCount).Cells(chBranchCode.Index).Value = SQL.SQLDR("BranchCode").ToString
                    rowsCount += 1
                End While
            End If

            'Current Branch
            query = " SELECT        dbo.tblJE_Details.VCECode, dbo.viewVCE_Master.VCEName, dbo.tblJE_Details.AccntCode, dbo.tblCOA_Master.AccountTitle, CASE WHEN SUM(Debit - Credit) > 0 THEN SUM(Debit - Credit) ELSE 0 END AS Debit, " & _
                    "                          CASE WHEN SUM(Credit - Debit) > 0 THEN SUM(Credit - Debit) ELSE 0 END AS Credit, ISNULL(dbo.tblJE_Details.BranchCode, dbo.tblJE_Header.BranchCode) AS BranchCode" & _
                    " FROM            dbo.tblJE_Details INNER JOIN" & _
                    "                          dbo.viewVCE_Master ON dbo.tblJE_Details.VCECode = dbo.viewVCE_Master.VCECode INNER JOIN" & _
                    "                         dbo.tblCOA_Master ON dbo.tblJE_Details.AccntCode = dbo.tblCOA_Master.AccountCode INNER JOIN " & _
                    "                         dbo.tblJE_Header ON dbo.tblJE_Header.JE_No = dbo.tblJE_Details.JE_No " & _
                    " 						 WHERE tblJE_Details.AccntCode IN (SELECT AccntCode FROM tblDefault_TransferAccnt) AND  dbo.tblJE_Details.VCECode = '" & MemID & "' " & _
                     " GROUP BY dbo.tblJE_Details.VCECode, dbo.viewVCE_Master.VCEName, dbo.tblJE_Details.AccntCode, dbo.tblCOA_Master.AccountTitle, ISNULL(dbo.tblJE_Details.BranchCode, dbo.tblJE_Header.BranchCode) "
            SQL.ReadQuery(query)
            If SQL.SQLDR.HasRows Then

                While SQL.SQLDR.Read
                    dgvEntry.Rows.Add(SQL.SQLDR("AccntCode").ToString)
                    dgvEntry.Rows(rowsCount).Cells(chAccntTitle.Index).Value = SQL.SQLDR("AccountTitle").ToString
                    dgvEntry.Rows(rowsCount).Cells(chDebit.Index).Value = CDec(SQL.SQLDR("Debit")).ToString("N2")
                    dgvEntry.Rows(rowsCount).Cells(chCredit.Index).Value = CDec(SQL.SQLDR("Credit")).ToString("N2")
                    dgvEntry.Rows(rowsCount).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                    dgvEntry.Rows(rowsCount).Cells(chVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                    dgvEntry.Rows(rowsCount).Cells(chParticulars.Index).Value = ""
                    dgvEntry.Rows(rowsCount).Cells(chRefNo.Index).Value = ""
                    dgvEntry.Rows(rowsCount).Cells(chBranchCode.Index).Value = BranchCode
                    rowsCount += 1
                End While
            End If
            TotalDBCR()
            MemTransfer = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadBranch()
        Try
            Dim dgvCB As New DataGridViewComboBoxColumn
            dgvCB = dgvEntry.Columns(chBranchCode.Index)
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

    Private Sub frmPJ_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            isReversalEntry = GetTransisReversal(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            LoadTerms()
            If interestgen = False Then
                If TransID <> "" Then
                    If Not AllowAccess("PJ_VIEW") Then
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
                        LoadPJ(TransID)
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
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadTerms()
        Dim query As String
        query = " SELECT  Description " & _
                " FROM    tblTerms " & _
                " WHERE   tblTerms.Status = 'Active'" & _
                " ORDER BY Days "
        SQL.ReadQuery(query)
        cbTerms.Items.Clear()
        While SQL.SQLDR.Read
            cbTerms.Items.Add(SQL.SQLDR("Description"))
        End While
    End Sub

    Private Sub cbTerms_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbTerms.SelectedIndexChanged
        Dim query, DateMode As String
        Dim Days As Integer = 0
        query = " SELECT  Description, Days, DateMode " & _
                " FROM    tblTerms " & _
                " WHERE   tblTerms.Status = 'Active' AND Description = '" & cbTerms.SelectedItem & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            DateMode = SQL.SQLDR("DateMode")
            Days = SQL.SQLDR("Days")
            If DateMode = "Year" Then
                dtpDueDate.Value = DateAdd(DateInterval.Year, CDec(Days), dtpDocDate.Value.Date) ' GET MATURITY DATE BASED ON TERMS AND STARTING DATE
            ElseIf DateMode = "Month" Then
                dtpDueDate.Value = DateAdd(DateInterval.Month, CDec(Days), dtpDocDate.Value.Date) ' GET MATURITY DATE BASED ON TERMS AND STARTING DATE
            Else
                dtpDueDate.Value = DateAdd(DateInterval.Day, CDec(Days), dtpDocDate.Value.Date) ' GET MATURITY DATE BASED ON TERMS AND STARTING DATE
            End If
        End If
    End Sub


    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged

    End Sub

    Private Sub TestToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles TestToolStripMenuItem1.Click
        Dim f As New frmReport_Filter
        f.Report = "PJ List"
        f.ShowDialog("")
        f.Dispose()
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
End Class

