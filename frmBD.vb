Public Class frmBD
    Dim TransID, RefID, JETransiD As String
    Dim Adv_Amount As Decimal
    Dim BDNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "BD"
    Dim ColumnID As String = "TransID"
    Dim ColumnPK As String = "BD_No"
    Dim DBTable As String = "tblBD"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim Collection_ID As Integer
    Dim bankID As Integer
    Dim branch As String

    Public Overloads Function ShowDialog(ByVal DocNumber As String) As Boolean
        TransID = DocNumber
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmBD_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Text = "(" & database & ") - Bank Deposit "
            TransAuto = GetTransSetup(ModuleID)
            PopulateDenomination()
            LoadBranches()
            LoadUndeposits()
            LoadBankList()

            dtpDocDate.Value = Date.Today.Date
            If TransID <> "" Then
                LoadBD(TransID)
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

    Private Sub LoadBranches()
        Dim query As String
        query = " SELECT    DISTINCT  tblBranch.BranchCode + ' - ' + Description AS BranchCode  " & _
                " FROM      tblBranch    " & _
                " INNER JOIN tblUser_Access    ON   " & _
                " tblBranch.BranchCode = tblUser_Access.Code    " & _
                " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                " WHERE     UserID ='" & UserID & "'  "
        SQL.ReadQuery(query)
        cbBranch.Items.Clear()
        While SQL.SQLDR.Read
            cbBranch.Items.Add(SQL.SQLDR("BranchCode").ToString)
        End While
        cbBranch.SelectedIndex = 0
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
        End While
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        dtpDocDate.Enabled = Value
        cbDepositType.Enabled = Value
        cbBranch.Enabled = Value
        cbBank.Enabled = Value
        txtRemarks.ReadOnly = Not Value
        dtpFrom.Enabled = Value
        dtpTo.Enabled = Value
        If Value = True Then
            dgvUndeposited.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
            dgvDenomination.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvUndeposited.EditMode = DataGridViewEditMode.EditProgrammatically
            dgvDenomination.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        chkAll.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
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
            cbBank.Items.Add(SQL.SQLDR("BankDetails").ToString)
        End While
    End Sub

    Private Sub LoadUndeposits()
        Dim query As String
        branch = Strings.Left(cbBranch.SelectedItem, cbBranch.SelectedItem.ToString.IndexOf(" - "))
        Try
            If cbDepositType.Text = "Cash" Then
                query = " SELECT CAST(0 AS BIT) AS [Dep], TransID AS [Collection ID],  " & _
                        "         TransType + ':' + RIGHT('000000' + CAST(tblCollection.TransNo AS nvarchar),6) AS [Ref No],  " & _
                        "         DateTrans AS [OR Date], viewVCE_Master.VCEName, Amount, tblCollection.Remarks, View_GL.AccntCode, tblCollection.VCECode " & _
                        "  FROM   tblCollection LEFT JOIn viewVCE_Master " & _
                        "  ON	 tblCollection.VCECode = viewVCE_Master.VCECode  " & _
                        "  LEFT JOIN " & _
                        "  view_GL ON " & _
                        "  view_GL.RefTransID = tblCollection.TransID AND view_GL.RefType in ('OR','CR','CSI','AR') " & _
                        "  WHERE  Deposit_ID = 0 AND PaymentType ='Cash' " & _
                        "  AND    DateTrans BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(branch = "ALL", "AND tblCollection.BranchCode in ( SELECT    DISTINCT  tblBranch.BranchCode   " & _
                        " FROM      tblBranch    " & _
                        " INNER JOIN tblUser_Access    ON   " & _
                        " tblBranch.BranchCode = tblUser_Access.Code    " & _
                        " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                        " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                        " WHERE     UserID ='" & UserID & "'  )", " AND tblCollection.BranchCode = '" & branch & "' ") & _
                        "  AND AccntCode in (SELECT AccntCode from tblDefaultAccount WHERE ModuleID = 'BD') AND tblCollection.Status <> 'Cancelled'" & _
                        "  ORDER BY TransType, tblCollection.TransNo    "
                SQL.GetQuery(query)
                dgvUndeposited.DataSource = Nothing
                dgvUndeposited.DataSource = SQL.SQLDS.Tables(0)
                dgvUndeposited.Refresh()
                dgvUndeposited.Columns(0).Width = 40
                dgvUndeposited.Columns(1).Width = 0
                dgvUndeposited.Columns(2).Width = 100
                dgvUndeposited.Columns(3).Width = 80
                dgvUndeposited.Columns(4).Width = 180
                dgvUndeposited.Columns(5).Width = 80
                dgvUndeposited.Columns(6).Width = 300
                dgvUndeposited.Columns(7).Width = 0
                dgvUndeposited.Columns(8).Width = 0
            Else
                query = " SELECT  CAST(0 AS BIT) AS [Dep], TransID AS [Collection ID],  " & _
                        "         TransType + ':' + RIGHT('000000' + CAST(tblCollection.TransNo AS nvarchar),6) AS [Ref No],   " & _
                        "         DateTrans AS [OR Date], viewVCE_Master.VCEName, Amount, CheckRef AS [Check], BankRef AS [Bank],   " & _
                        "         CASE WHEN BankRef <> '' THEN CheckDate ELSE NULL END AS CheckDate, tblCollection.Remarks , View_GL.AccntCode, tblCollection.VCECode " & _
                        "  FROM   tblCollection LEFT JOIn viewVCE_Master  " & _
                        "  ON	 tblCollection.VCECode = viewVCE_Master.VCECode  " & _
                        "   LEFT JOIN " & _
                        "  view_GL ON " & _
                        "  view_GL.RefTransID = tblCollection.TransID AND view_GL.RefType in ('OR','CR','CSI','AR') " & _
                        "  WHERE  Deposit_ID = 0 AND PaymentType ='Check'  " & _
                        "  AND    DateTrans BETWEEN '" & dtpFrom.Value.Date & "' AND '" & dtpTo.Value.Date & "'  " & IIf(branch = "ALL", "AND tblCollection.BranchCode in ( SELECT    DISTINCT  tblBranch.BranchCode   " & _
                        " FROM      tblBranch    " & _
                        " INNER JOIN tblUser_Access    ON   " & _
                        " tblBranch.BranchCode = tblUser_Access.Code    " & _
                        " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                        " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                        " WHERE     UserID ='" & UserID & "'  )", " AND tblCollection.BranchCode = '" & branch & "' ") & _
                        "  AND AccntCode in (SELECT AccntCode from tblDefaultAccount WHERE ModuleID = 'BD') AND tblCollection.Status <> 'Cancelled'" & _
                        "  ORDER BY TransType, tblCollection.TransNo    "
                SQL.GetQuery(query)
                dgvUndeposited.DataSource = Nothing
                dgvUndeposited.DataSource = SQL.SQLDS.Tables(0)
                dgvUndeposited.Refresh()
                dgvUndeposited.Columns(0).Width = 40
                dgvUndeposited.Columns(1).Width = 0
                dgvUndeposited.Columns(2).Width = 100
                dgvUndeposited.Columns(3).Width = 80
                dgvUndeposited.Columns(4).Width = 180
                dgvUndeposited.Columns(5).Width = 80
                dgvUndeposited.Columns(6).Width = 80
                dgvUndeposited.Columns(7).Width = 80
                dgvUndeposited.Columns(8).Width = 80
                dgvUndeposited.Columns(9).Width = 80
                dgvUndeposited.Columns(10).Width = 0
                dgvUndeposited.Columns(11).Width = 0
            End If

            If dgvUndeposited.RowCount <> 0 Then
                dgvUndeposited.ReadOnly = False
                For i As Integer = 0 To dgvUndeposited.RowCount - 1
                    For ii As Integer = 1 To dgvUndeposited.ColumnCount - 1
                        dgvUndeposited.Rows(i).Cells(ii).ReadOnly = True
                    Next
                Next
                btnCompute.PerformClick()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadDeposit(ByVal Deposit_ID As String)
        Dim query As String

        Try
            If cbDepositType.Text = "Cash" Then
                query = " SELECT CAST(1 AS BIT) AS [Dep], TransID AS [Collection ID],  " & _
                        "         TransType + ':' + RIGHT('000000' + CAST(tblCollection.TransNo AS nvarchar),6) AS [Ref No],  " & _
                        "         DateTrans AS [OR Date], viewVCE_Master.VCEName, Amount, tblCollection.Remarks, View_GL.AccntCode, tblCollection.VCECode " & _
                        "  FROM   tblCollection LEFT JOIn viewVCE_Master " & _
                        "  ON	 tblCollection.VCECode = viewVCE_Master.VCECode  " & _
                        "  LEFT JOIN " & _
                        "  view_GL ON " & _
                        "  view_GL.RefTransID = tblCollection.TransID AND view_GL.RefType in ('OR','CR','CSI','AR') " & _
                        "  WHERE  Deposit_ID = '" & Deposit_ID & "' AND PaymentType ='Cash' " & _
                        "  AND AccntCode in (SELECT AccntCode from tblDefaultAccount WHERE ModuleID = 'BD') " & _
                        "  ORDER BY TransType, tblCollection.TransNo    "
                SQL.GetQuery(query)
                dgvUndeposited.DataSource = Nothing
                dgvUndeposited.DataSource = SQL.SQLDS.Tables(0)
                dgvUndeposited.Refresh()
                dgvUndeposited.Columns(0).Width = 40
                dgvUndeposited.Columns(1).Width = 0
                dgvUndeposited.Columns(2).Width = 100
                dgvUndeposited.Columns(3).Width = 80
                dgvUndeposited.Columns(4).Width = 180
                dgvUndeposited.Columns(5).Width = 80
                dgvUndeposited.Columns(6).Width = 300
                dgvUndeposited.Columns(7).Width = 0
                dgvUndeposited.Columns(8).Width = 0
            Else
                query = " SELECT  CAST(1 AS BIT) AS [Dep], TransID AS [Collection ID],  " & _
                        "         TransType + ':' + RIGHT('000000' + CAST(tblCollection.TransNo AS nvarchar),6) AS [Ref No],   " & _
                        "         DateTrans AS [OR Date], viewVCE_Master.VCEName, Amount, CheckRef AS [Check], BankRef AS [Bank],   " & _
                        "         CASE WHEN BankRef <> '' THEN CheckDate ELSE NULL END AS CheckDate, tblCollection.Remarks , View_GL.AccntCode, tblCollection.VCECode " & _
                        "  FROM   tblCollection LEFT JOIn viewVCE_Master  " & _
                        "  ON	 tblCollection.VCECode = viewVCE_Master.VCECode  " & _
                        "   LEFT JOIN " & _
                        "  view_GL ON " & _
                        "  view_GL.RefTransID = tblCollection.TransID AND view_GL.RefType in ('OR','CR','CSI','AR') " & _
                        "  WHERE  Deposit_ID = '" & Deposit_ID & "' AND PaymentType ='Check'  " & _
                        "  AND AccntCode in (SELECT AccntCode from tblDefaultAccount WHERE ModuleID = 'BD') " & _
                        "  ORDER BY TransType, tblCollection.TransNo    "
                SQL.GetQuery(query)
                dgvUndeposited.DataSource = Nothing
                dgvUndeposited.DataSource = SQL.SQLDS.Tables(0)
                dgvUndeposited.Refresh()
                dgvUndeposited.Columns(0).Width = 40
                dgvUndeposited.Columns(1).Width = 0
                dgvUndeposited.Columns(2).Width = 100
                dgvUndeposited.Columns(3).Width = 80
                dgvUndeposited.Columns(4).Width = 180
                dgvUndeposited.Columns(5).Width = 80
                dgvUndeposited.Columns(6).Width = 80
                dgvUndeposited.Columns(7).Width = 80
                dgvUndeposited.Columns(8).Width = 80
                dgvUndeposited.Columns(9).Width = 80
                dgvUndeposited.Columns(10).Width = 0
                dgvUndeposited.Columns(11).Width = 0
            End If

            If dgvUndeposited.RowCount <> 0 Then
                dgvUndeposited.ReadOnly = False
                For i As Integer = 0 To dgvUndeposited.RowCount - 1
                    For ii As Integer = 1 To dgvUndeposited.ColumnCount - 1
                        dgvUndeposited.Rows(i).Cells(ii).ReadOnly = True
                    Next
                Next
                btnCompute.PerformClick()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function ComputeTotal() As Decimal
        Dim amount As Decimal = 0
        For Each row As DataGridViewRow In dgvUndeposited.Rows
            If row.Cells(0).Value = True Then
                amount += row.Cells(5).Value
            End If
        Next
        Return amount

    End Function

    Private Function ComputeTotalUndeposit() As Decimal
        Dim amount As Decimal = 0
        For Each row As DataGridViewRow In dgvUndeposited.Rows
            If row.Cells(0).Value = False Then
                amount += row.Cells(5).Value
            End If
        Next
        Return amount

    End Function

    Private Sub PopulateDenomination()
        Dim query As String
        query = " SELECT    TOP (100) PERCENT Denomination, Description " & _
                " FROM      tblDenomination " & _
                " ORDER BY  Denomination DESC "
        SQL.ReadQuery(query)
        dgvDenomination.Rows.Clear()
        While SQL.SQLDR.Read
            dgvDenomination.Rows.Add(New String() {SQL.SQLDR("Denomination").ToString, "0", "0"})
        End While
    End Sub

    Private Sub dgvDenomination_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDenomination.CellEndEdit
        dgvDenomination.Item(2, e.RowIndex).Value = Double.Parse(dgvDenomination.Item(1, e.RowIndex).Value) * Double.Parse(dgvDenomination.Item(0, e.RowIndex).Value)
        TotalAmt()
    End Sub

    Private Sub TotalAmt()
        Dim a As Double = 0
        For i As Integer = 0 To dgvDenomination.Rows.Count - 1
            a = a + Double.Parse(dgvDenomination.Item(2, i).Value)
        Next
        txtTotalAmount.Text = a.ToString("N2")
    End Sub

    Private Sub SaveDenomination(ByVal TransId As String)
        Dim insertSQL As String
        Dim Denomination, PCS, Amount As String
        For i As Integer = 0 To dgvDenomination.Rows.Count - 1
            Denomination = dgvDenomination.Item(0, i).Value
            PCS = dgvDenomination.Item(1, i).Value
            Amount = dgvDenomination.Item(2, i).Value
                insertSQL = " INSERT INTO  " & _
                            " tblBD_Denomination(TransID, Denomination, PCS, Amount) " & _
                            " VALUES(@TransID, @Denomination, @PCS, @Amount) "
                SQL.FlushParams()
                SQL.AddParam("@TransID", TransId)
                SQL.AddParam("@Denomination", Denomination)
                SQL.AddParam("@PCS", PCS)
                SQL.AddParam("@Amount", Amount)
                SQL.ExecNonQuery(insertSQL)
        Next
    End Sub

    Private Sub LoadDepositDetails(ByVal DepositID As Integer)
        Dim query As String

        query = " SELECT Collection_ID AS [Collection ID], " & _
                "        Base_Type + ':' + RIGHT('000000' + CAST(Base_ID AS nvarchar),6) AS [Ref No],  " & _
                "        Base_Date AS [CV Date], VCEName, Amount, Check_Ref AS [Check], Bank_Ref AS [Bank],  " & _
                "        CASE WHEN Bank_Ref <> '' THEN Check_Date ELSE NULL END AS [Check Date], Remarks " & _
                " FROM   tblCollection LEFT JOIn viewVCE_Master " & _
                " ON	 tblCollection.VCECode = viewVCE_Master.VCECode " & _
                " WHERE  Deposit_ID = " & DepositID & " "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvUndeposited.DataSource = SQL.SQLDS.Tables(0)
            dgvUndeposited.Columns(0).Width = 70
            dgvUndeposited.Columns(1).Width = 70
            dgvUndeposited.Columns(2).Width = 80
            dgvUndeposited.Columns(3).Width = 180
            dgvUndeposited.Columns(4).Width = 80
            dgvUndeposited.Columns(5).Width = 80
            dgvUndeposited.Columns(6).Width = 180
        Else
            dgvUndeposited.DataSource = Nothing
        End If
    End Sub

    Private Sub LoadDenomination(ByVal DepositID As String)
        Dim query As String
        query = " SELECT Denomination, PCS, Amount FROM tblBD_Denomination WHERE TransID = " & DepositID & " "
        SQL.ReadQuery(query)
        dgvDenomination.Rows.Clear()
        While SQL.SQLDR.Read
            dgvDenomination.Rows.Add(New String() {SQL.SQLDR("Denomination").ToString, SQL.SQLDR("PCS"), SQL.SQLDR("Amount")})
        End While
        TotalAmt()
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("BD", TransID)
        f.Dispose()
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("BD_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("BD")
            TransID = f.transID
            LoadBD(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        Dim Denom, DepAmount, variance As Decimal
        If IsNumeric(txtTotalAmount.Text) Then Denom = CDec(txtTotalAmount.Text) Else Denom = 0
        If IsNumeric(txtTotalDeposit.Text) Then DepAmount = CDec(txtTotalDeposit.Text) Else DepAmount = 0
        variance = DepAmount - Denom
        If variance <> 0 And Denom <> 0 Then
            Msg("Denomination Amount should be equal to Total Amount for Deposit!", MsgBoxStyle.Exclamation)
        ElseIf cbBank.SelectedIndex = -1 Then
            MsgBox("Please select bank!", MsgBoxStyle.Exclamation)
        ElseIf DepAmount = 0 Then
            MsgBox("No amount for Deposit!", MsgBoxStyle.Exclamation)
        ElseIf TransID = "" Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                TransID = GenerateTransID(ColumnID, DBTable)
                If TransAuto Then
                    BDNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                Else
                    BDNo = txtTransNum.Text
                End If
                txtTransNum.Text = BDNo
                UpdateCollection()
                SaveBD()
                Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                BDNo = txtTransNum.Text
                LoadBD(TransID)
            End If
        Else
            If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                If BDNo = txtTransNum.Text Then
                    BDNo = txtTransNum.Text
                    UpdateBD()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    BDNo = txtTransNum.Text
                    LoadBD(TransID)
                Else
                    If Not IfExist(txtTransNum.Text) Then
                        BDNo = txtTransNum.Text
                        UpdateBD()
                        Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                        BDNo = txtTransNum.Text
                        LoadBD(TransID)
                    Else
                        MsgBox("Deposit" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
                    End If
                End If
            End If
        End If
    End Sub

    Private Function IfExist(ByVal ID As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblBD WHERE BD_No ='" & ID & "'  "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub SaveBD()
        Try
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblBD  (TransID, BD_No, BranchCode, BusinessCode, DateBD, BankID, TotalAmount, Status, Remarks, BD_Type, DateCreated, WhoCreated) " & _
                        " VALUES (@TransID, @BD_No, @BranchCode, @BusinessCode, @DateBD,  @BankID, @TotalAmount, @Status, @Remarks, @BD_Type, GETDATE(), @WhoCreated) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@BD_No", BDNo)
            SQL.AddParam("@BranchCode", branch)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateBD", dtpDocDate.Value.Date)
            SQL.AddParam("@BankID", txtBankID.Text)
            If IsNumeric(txtTotalDeposit.Text) Then SQL.AddParam("@TotalAmount", CDec(txtTotalDeposit.Text)) Else SQL.AddParam("@TotalAmount", 0)
            SQL.AddParam("@Status", "Active")
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@BD_Type", cbDepositType.Text)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            SaveDenomination(TransID)



            insertSQL = " INSERT INTO " & _
                       " tblJE_Header (AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, WhoCreated) " & _
                       " VALUES(@AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @WhoCreated)"
            SQL.FlushParams()
            SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
            SQL.AddParam("@RefType", "DEPOSIT")
            SQL.AddParam("@RefTransID", TransID)
            SQL.AddParam("@Book", "Cash Receipts")
            SQL.AddParam("@TotalDBCR", CDec(txtTotalDeposit.Text))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@BranchCode", branch)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            JETransiD = LoadJE("DEPOSIT", TransID)
            SaveDebitEntry()
            SaveCreditEntry()

        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "BD_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub SaveDebitEntry()
        Dim insertSQL As String
        Dim line As Integer = 1
        Dim CashAmount As Decimal = 0
        If cbDepositType.Text = "Cash" Then
            For Each item As DataGridViewRow In dgvUndeposited.Rows
                If item.Cells(0).Value = True Then
                    CashAmount = CashAmount + Double.Parse(item.Cells(5).Value).ToString("N2")
                End If
            Next

            If CashAmount > 0 Then
                insertSQL = " INSERT INTO " & _
                                      " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, BranchCode) " & _
                                      " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @BranchCode)"
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransiD)
                SQL.AddParam("@AccntCode", txtAccountCode.Text)
                SQL.AddParam("@VCECode", "")
                SQL.AddParam("@Debit", CDec(CashAmount))
                SQL.AddParam("@Credit", 0)
                SQL.AddParam("@Particulars", "")
                SQL.AddParam("@RefNo", "")
                SQL.AddParam("@LineNumber", line)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.ExecNonQuery(insertSQL)
            End If
        ElseIf cbDepositType.Text = "Check" Then
            For Each item As DataGridViewRow In dgvUndeposited.Rows
                If item.Cells(0).Value = True Then
                    insertSQL = " INSERT INTO " & _
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, BranchCode) " & _
                                " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @BranchCode)"
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransiD)
                    SQL.AddParam("@AccntCode", txtAccountCode.Text)
                    SQL.AddParam("@VCECode", item.Cells(11).Value.ToString)
                    SQL.AddParam("@Debit", CDec(item.Cells(5).Value))
                    SQL.AddParam("@Credit", 0)
                    If item.Cells(9).Value <> Nothing AndAlso item.Cells(9).Value <> "" Then
                        SQL.AddParam("@Particulars", item.Cells(9).Value.ToString)
                    Else
                        SQL.AddParam("@Particulars", "")
                    End If
                    SQL.AddParam("@RefNo", "")
                    SQL.AddParam("@LineNumber", line)
                    SQL.AddParam("@BranchCode", BranchCode)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
        End If
    End Sub

    Private Sub SaveCreditEntry()
        Dim insertSQL As String
        Dim line As Integer = 1
        If cbDepositType.Text = "Cash" Then
            For Each item As DataGridViewRow In dgvUndeposited.Rows
                If item.Cells(0).Value = True Then
                    insertSQL = " INSERT INTO " & _
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, BranchCode) " & _
                                " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @BranchCode)"
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransiD)
                    SQL.AddParam("@AccntCode", item.Cells(7).Value.ToString)
                    SQL.AddParam("@VCECode", item.Cells(8).Value.ToString)
                    SQL.AddParam("@Debit", 0)
                    SQL.AddParam("@Credit", CDec(item.Cells(5).Value))
                    If item.Cells(6).Value <> Nothing AndAlso item.Cells(6).Value <> "" Then
                        SQL.AddParam("@Particulars", item.Cells(6).Value.ToString)
                    Else
                        SQL.AddParam("@Particulars", "")
                    End If
                    SQL.AddParam("@RefNo", "")
                    SQL.AddParam("@LineNumber", line)
                    SQL.AddParam("@BranchCode", branch)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
        ElseIf cbDepositType.Text = "Check" Then
            For Each item As DataGridViewRow In dgvUndeposited.Rows
                If item.Cells(0).Value = True Then
                    insertSQL = " INSERT INTO " & _
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, BranchCode) " & _
                                " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @BranchCode)"
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransiD)
                    SQL.AddParam("@AccntCode", item.Cells(10).Value.ToString)
                    SQL.AddParam("@VCECode", item.Cells(11).Value.ToString)
                    SQL.AddParam("@Debit", 0)
                    SQL.AddParam("@Credit", CDec(item.Cells(5).Value))
                    If item.Cells(9).Value <> Nothing AndAlso item.Cells(9).Value <> "" Then
                        SQL.AddParam("@Particulars", item.Cells(9).Value.ToString)
                    Else
                        SQL.AddParam("@Particulars", "")
                    End If
                    SQL.AddParam("@RefNo", "")
                    SQL.AddParam("@LineNumber", line)

                    SQL.AddParam("@BranchCode", branch)
                SQL.ExecNonQuery(insertSQL)
                line += 1
                End If
            Next
        End If
    End Sub

    Private Sub SaveDenomination(ByVal TransId As Integer)
        Dim insertSQL As String
        Dim Denomination, PCS, Amount As String
        For i As Integer = 0 To dgvDenomination.Rows.Count - 1
            Denomination = dgvDenomination.Item(0, i).Value
            PCS = dgvDenomination.Item(1, i).Value
            Amount = dgvDenomination.Item(2, i).Value
                insertSQL = " INSERT INTO  " & _
                            " tblBD_Denomination(TransID, Denomination, PCS, Amount) " & _
                            " VALUES(@TransID, @Denomination, @PCS, @Amount) "
                SQL.FlushParams()
                SQL.AddParam("@TransID", TransId)
                SQL.AddParam("@Denomination", Denomination)
                SQL.AddParam("@PCS", PCS)
                SQL.AddParam("@Amount", Amount)
                SQL.ExecNonQuery(insertSQL)
        Next
    End Sub

    Private Sub LoadDenomination(ByVal DepositID As Integer)
        Dim query As String
        query = " SELECT Denomination, PCS, Amount FROM tblBD_Denomination WHERE TransId = " & DepositID & " "
        SQL.ReadQuery(query)
        dgvDenomination.Rows.Clear()
        While SQL.SQLDR.Read
            dgvDenomination.Rows.Add(New String() {SQL.SQLDR("Denomination").ToString, SQL.SQLDR("PCS"), SQL.SQLDR("Amount")})
        End While
    End Sub

    Private Sub UpdateBD()
        Try
            activityStatus = True
            Dim updateSQL As String
            updateSQL = " UPDATE tblBD " & _
                        " SET    BD_No = @BD_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, DateBD = @DateBD, BankID = @BankID, " & _
                        "        TotalAmount = @TotalAmount, Status = @Status, Remarks = @Remarks, BD_Type = @BD_Type, DateModified = GETDATE(), WhoModified = @WhoModified " & _
                        " WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@BD_No", BDNo)
            SQL.AddParam("@BranchCode", branch)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateBD", dtpDocDate.Value.Date)
            SQL.AddParam("@BankID", txtBankID.Text)
            If IsNumeric(txtTotalDeposit.Text) Then SQL.AddParam("@TotalAmount", CDec(txtTotalDeposit.Text)) Else SQL.AddParam("@TotalAmount", 0)
            SQL.AddParam("@Status", "Active")
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@BD_Type", cbDepositType.Text)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "BD_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub


    Private Sub UpdateCollection()
        Try
            Dim updateSQL As String
            Dim Collection_TransID As Integer
            For Each row As DataGridViewRow In dgvUndeposited.Rows
                If row.Cells(0).Value = True Then
                    Collection_TransID = row.Cells(1).Value
                    updateSQL = " UPDATE  tblCollection " &
                              " SET     Deposit_ID = @Deposit_ID  " &
                              " WHERE   TransID= @TransID AND PaymentType = @PaymentType "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", Collection_TransID)
                    SQL.AddParam("@PaymentType", cbDepositType.Text)
                    SQL.AddParam("@Deposit_ID", TransID)
                    SQL.ExecNonQuery(updateSQL)
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
        End Try
    End Sub

    Private Sub LoadBD(ByVal TransID As String)
        Dim query As String
        Dim bankID As String
        query = " SELECT   TransID, BD_No, DateBD, BankID, TotalAmount, tblBD.Status, Remarks, BD_Type," & _
                " 		   tblBD.BranchCode + ' - ' + tblBranch.Description AS BranchCode " & _
                " FROM     tblBD " & _
                "  LEFT JOIN tblBranch ON " & _
                "  tblBD.BranchCode = tblBranch.BranchCode AND tblBranch.Status = 'Active' " & _
                " WHERE    TransId = '" & TransID & "' " & _
                " ORDER BY TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            disableEvent = True
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            bankID = SQL.SQLDR("BankID").ToString
            BDNo = SQL.SQLDR("BD_No").ToString
            cbBranch.SelectedItem = SQL.SQLDR("BranchCode").ToString
            txtTransNum.Text = BDNo
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            dtpDocDate.Text = SQL.SQLDR("DateBD").ToString
            txtTotalDeposit.Text = CDec(SQL.SQLDR("TotalAmount").ToString).ToString("N2")
            cbDepositType.Text = SQL.SQLDR("BD_Type").ToString
            disableEvent = False
            SelectBank(bankID)
            LoadDeposit(TransID)
            LoadDenomination(TransID)
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

    Private Sub ClearText()
        txtTransNum.Text = ""
        txtAccountCode.Clear()
        txtAccountTitle.Clear()
        txtBankAccountNo.Clear()
        txtTotalAmount.Clear()
        txtTotalDeposit.Clear()
        txtTotalUndeposit.Clear()
        txtBankID.Clear()
        cbBank.SelectedIndex = -1
        cbDepositType.SelectedIndex = 0
        cbBranch.SelectedIndex = 0
        txtRemarks.Text = ""
        dgvUndeposited.DataSource = Nothing
        PopulateDenomination()
        txtStatus.Text = "Open"
        dtpDocDate.MinDate = GetMaxPEC()
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

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("BD_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            BDNo = ""

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
        If Not AllowAccess("BD_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)
            dtpDocDate.Enabled = False
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

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        ' Toolstrip Buttons
        If BDNo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadBD(TransID)
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

    Private Sub cbBank_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbBank.SelectedIndexChanged
   If disableEvent = False Then
            If cbBank.SelectedIndex <> -1 Then
                LoadBankDetail(cbBank.SelectedItem)
            End If
        End If
    End Sub

    Private Sub LoadBankDetail(bank As String)
        Dim query As String
        Dim bank1() As String = Strings.Split(bank, "-")
        bankID = bank1(0)
        query = " SELECT   BankID, Bank, Branch, tblBank_Master.AccountCode, AccountTitle,  AccountNo" & _
                " FROM     tblBank_Master LEFT JOIN tblCOA_Master " & _
                " ON       tblBank_Master.AccountCode = tblCOA_Master.AccountCode " & _
                " WHERE    BankID = '" & bankID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtBankID.Text = SQL.SQLDR("BankID")
            txtAccountCode.Text = SQL.SQLDR("AccountCode")
            txtAccountTitle.Text = SQL.SQLDR("AccountTitle")
            txtBankAccountNo.Text = SQL.SQLDR("AccountNo")
        End If
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

    Private Sub cbDepositType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbDepositType.SelectedIndexChanged
        LoadUndeposits()
    End Sub

    Private Sub dgvUndeposited_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvUndeposited.CellContentClick
        btnCompute.PerformClick()
    End Sub

    Private Sub chkAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAll.CheckedChanged
        For Each row As DataGridViewRow In dgvUndeposited.Rows
            row.Cells(0).Value = chkAll.Checked
        Next
        btnCompute.PerformClick()
    End Sub

    Private Sub dtpFrom_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpFrom.ValueChanged
        LoadUndeposits()
    End Sub

    Private Sub dtpTo_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpTo.ValueChanged
        LoadUndeposits()
    End Sub

    Private Sub btnCompute_Click(sender As System.Object, e As System.EventArgs) Handles btnCompute.Click
        txtTotalDeposit.Text = ComputeTotal()
        txtTotalUndeposit.Text = ComputeTotalUndeposit()
    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("BD_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If txtStatus.Text <> "Cancelled" AndAlso MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim updateSQL As String
                        updateSQL = " UPDATE  tblBD SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        BDNo = txtTransNum.Text
                        SQL.AddParam("@TransID", TransID)
                        SQL.ExecNonQuery(updateSQL)

                        Dim Collection_TransID As Integer
                        For Each row As DataGridViewRow In dgvUndeposited.Rows
                            If row.Cells(0).Value = True Then
                                Collection_TransID = row.Cells(1).Value
                                updateSQL = " UPDATE  tblCollection " &
                                          " SET     Deposit_ID = 0  " &
                                          " WHERE   TransID= @TransID AND PaymentType = @PaymentType AND Deposit_ID = @Deposit_ID"
                                SQL.FlushParams()
                                SQL.AddParam("@TransID", Collection_TransID)
                                SQL.AddParam("@PaymentType", cbDepositType.Text)
                                SQL.AddParam("@Deposit_ID", TransID)
                                SQL.ExecNonQuery(updateSQL)
                            End If
                        Next

                        updateSQL = " UPDATE  tblJE_Header SET Status ='Cancelled' WHERE RefTransID = @RefTransID  AND RefType ='DEPOSIT' "
                        SQL.FlushParams()
                        SQL.AddParam("@RefTransID", TransID)
                        SQL.ExecNonQuery(updateSQL)


                        Dim insertSQL As String
                        insertSQL = " INSERT INTO tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, OtherRef) " & _
                                    " SELECT JE_No, AccntCode, VCECode, Credit, Debit, Particulars, RefNo, 0 AS LineNumber, OtherRef FROM tblJE_Details " & _
                                    " WHERE JE_No IN (SELECT DISTINCT JE_No FROM tblJE_Header WHERE RefType ='DEPOSIT' AND RefTransID ='" & TransID & "') "
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

                        BDNo = txtTransNum.Text
                        LoadBD(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "BN_No", BDNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try

                End If
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If BDNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblBD   " & _
                    " INNER JOIN tblBranch  ON	          " & _
                    "   tblBD.BranchCode = tblBranch.BranchCode    " & _
                    "     WHERE          " & _
                    " 	( tblBD.BranchCode IN  " & _
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
                    " AND BD_No > '" & BDNo & "' ORDER BY BD_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadBD(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If BDNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblBD  " & _
                    "   INNER JOIN tblBranch  ON	          " & _
                    "   tblBD.BranchCode = tblBranch.BranchCode    " & _
                    "     WHERE          " & _
                    " 	( tblBD.BranchCode IN  " & _
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
                    " AND BD_No < '" & BDNo & "' ORDER BY BD_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadBD(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub cbBranch_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbBranch.SelectedIndexChanged
        If disableEvent = False Then
            LoadUndeposits()
        End If
    End Sub

    Private Sub GroupBox3_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox3.Enter

    End Sub
End Class