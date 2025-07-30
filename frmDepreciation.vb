Public Class frmDepreciation

    Dim TransID, JETransiD As String
    Dim DPNo As String
    Dim disableEvent As Boolean = False
    Dim bankEvent As Boolean = False
    Dim ModuleID As String = "DP"
    Dim ColumnPK As String = "DP_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblDepreciation"
    Dim TransAuto As Boolean
    Dim bankID As Integer
    Dim RR_ID, LineID As Integer
    Dim EUL As Decimal
    Dim Dir As String


    Private Sub txtTerms_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTerms.KeyDown
        If e.KeyCode = Keys.Enter Then
            
        End If
    End Sub

    Private Sub SaveDepreciation()
        Dim SQL1 As New SQLControl
        SQL1.BeginTransaction()
        Try
            Dim insertSQL As String
            insertSQL = " INSERT INTO tblDepreciation (TransID, DP_No, DateDP, VCECode, AccntCode, ItemCode, " & vbCrLf & _
                        " QTY, UOM, ItemType,  RR_Ref, RR_Ref_LineNum,  " & vbCrLf & _
                        " StartDate, EndOfLife, Terms, AcqCost, AmortCost, Remarks, DateCreated, WhoCreated, BranchCode, SalvageValue, InvoiceNo) " & vbCrLf & _
                        " VALUES (@TransID, @DP_No, @DateDP, @VCECode, @AccntCode, @ItemCode,  " & vbCrLf & _
                        " @QTY, @UOM, @ItemType,  @RR_Ref, @RR_Ref_LineNum, " & vbCrLf & _
                        " @StartDate, @EndOfLife, @Terms, @AcqCost, @AmortCost, @Remarks, GETDATE(), @WhoCreated, @BranchCode, @SalvageValue, @InvoiceNo) "
            SQL1.FlushParams()
            SQL1.AddParam("@TransID", TransID)
            SQL1.AddParam("@DP_No", txtTransNum.Text)
            SQL1.AddParam("@DateDP", dtpDocDate.Value)
            SQL1.AddParam("@VCECode", txtVCECode.Text)
            SQL1.AddParam("@AccntCode", txtAccountCode.Text)
            SQL1.AddParam("@ItemCode", txtItemCode.Text)
            SQL1.AddParam("@QTY", txtQTY.Text)
            SQL1.AddParam("@UOM", txtUOM.Text)
            SQL1.AddParam("@ItemType", txtItemType.Text)
            SQL1.AddParam("@StartDate", dtpStartDate.Value)
            SQL1.AddParam("@EndOfLife", dtpEndDate.Value)
            SQL1.AddParam("@Terms", txtTerms.Text)
            SQL1.AddParam("@AcqCost", CDec(txtAcqCost.Text))
            SQL1.AddParam("@AmortCost", CDec(txtAmort.Text))
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@SalvageValue", txtSalvage.Text)
            SQL1.AddParam("@InvoiceNo", txtInvoiceNo.Text)
            SQL1.AddParam("@RR_Ref", RR_ID)
            SQL1.AddParam("@RR_Ref_LineNum", LineID)
            SQL1.ExecNonQuery(insertSQL)


            SQL1.Commit()
            SaveSched(TransID)
        Catch ex As Exception
            SQL1.Rollback()
            TransID = ""
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "DP_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL1.FlushParams()
        End Try
    End Sub


    Private Sub SaveSched(ByVal ID As String)

        Dim deleteSQL As String
        deleteSQL = " DELETE FROM tblDepreciation_Schedule WHERE TransID = @TransID "
        SQL.FlushParams()
        SQL.AddParam("@TransID", ID)
        SQL.ExecNonQuery(deleteSQL)

        For Each item As ListViewItem In lvAmmort.Items
            Dim insertSQL As String
            Dim count As Integer = lvAmmort.Columns.Count
            insertSQL = " INSERT INTO " & _
                        " tblDepreciation_Schedule(AmortNum, TransID, Date, BegBalance, Amort, NetBookValue, WhoCreated, DateCreated)   " & _
                        " VALUES       (@AmortNum, @TransID, @Date, @BegBalance, @Amort,  @NetBookValue, @WhoCreated, GETDATE()) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", ID)
            SQL.AddParam("@AmortNum", item.SubItems(0).Text)
            SQL.AddParam("@Date", item.SubItems(1).Text)
            SQL.AddParam("@BegBalance", CDec(item.SubItems(2).Text))
            SQL.AddParam("@Amort", CDec(item.SubItems(3).Text))
            SQL.AddParam("@NetBookValue", CDec(item.SubItems(4).Text))
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)
        Next
    End Sub


    Private Sub EditDepreciation()
        Dim SQL As New SQLControl
        SQL.BeginTransaction()
        Dim updateSQL As String
        Try
            updateSQL = " UPDATE tblDepreciation " & _
                        " SET    DP_No = @DP_No, DateDP = @DateDP, VCECode = @VCECode, AccntCode = @AccntCode, ItemCode = @ItemCode, " & vbCrLf & _
                        "       QTY = @QTY, UOM = @UOM, ItemType = @ItemType,  RR_Ref = @RR_Ref, RR_Ref_LineNum = @RR_Ref_LineNum,  " & vbCrLf & _
                        "       StartDate = @StartDate, EndOfLife = @EndOfLife, Terms = @Terms, AcqCost = @AcqCost, AmortCost = @AmortCost,   " & vbCrLf & _
                        "       BranchCode = @BranchCode, SalvageValue = @SalvageValue, InvoiceNo= @InvoiceNo, " & _
                        "        Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE() " & _
                        " WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@DP_No", txtTransNum.Text)
            SQL.AddParam("@DateDP", dtpDocDate.Value)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@AccntCode", txtAccountCode.Text)
            SQL.AddParam("@ItemCode", txtItemCode.Text)
            SQL.AddParam("@QTY", txtQTY.Text)
            SQL.AddParam("@UOM", txtUOM.Text)
            SQL.AddParam("@ItemType", txtItemType.Text)
            SQL.AddParam("@StartDate", dtpStartDate.Value)
            SQL.AddParam("@EndOfLife", dtpEndDate.Value)
            SQL.AddParam("@Terms", txtTerms.Text)
            SQL.AddParam("@AcqCost", CDec(txtAcqCost.Text))
            SQL.AddParam("@AmortCost", CDec(txtAmort.Text))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@WhoModified", UserID)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@SalvageValue", txtSalvage.Text)
            SQL.AddParam("@InvoiceNo", txtInvoiceNo.Text)
            SQL.AddParam("@RR_Ref", RR_ID)
            SQL.AddParam("@RR_Ref_LineNum", LineID)
            SQL.ExecNonQuery(updateSQL)

            SQL.Commit()
            SaveSched(TransID)
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "DP_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub txtAcqCost_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAcqCost.KeyDown
        If txtAcqCost.Text = "" Then
            txtAcqCost.Text = 0
        End If
    End Sub

    Private Sub txtAcqCost_TextChanged(sender As Object, e As EventArgs) Handles txtAcqCost.TextChanged
        If txtAcqCost.Text = "" Then
            txtAcqCost.Text = 0
        End If

    End Sub
    Private Sub AcqComp()
        If txtAmort.Text > 0 Or txtAmort.Text <> "" Then
            Dim a As Integer = txtTerms.Text
            dtpEndDate.Value = CDate(dtpStartDate.Value.AddMonths(1).Month & "-01-" & dtpStartDate.Value.AddMonths(1).Year).AddDays(-1)
            dtpEndDate.Value = dtpEndDate.Value.AddMonths(a)
            txtAmort.Text = CDec((CDec(txtAcqCost.Text) - CDec(txtSalvage.Text)) / txtTerms.Text).ToString("N2")

            GenerateSchedule()
        End If


    End Sub

    Private Sub ClearText()
        txtTransNum.Clear()
        txtVCECode.Clear()
        txtVCEName.Clear()
        txtAccountCode.Clear()
        txtAccountTitle.Clear()
        txtItemCode.Clear()
        txtItemName.Clear()
        txtItemType.Clear()
        lvAmmort.Items.Clear()
        txtTerms.Text = "0"
        txtAcqCost.Text = "0"
        txtAmort.Text = "0"
        txtSalvage.Text = "0"
        txtQTY.Text = "0"
        txtUOM.Clear()
        txtRefRR.Clear()
        txtStatus.Text = "Active"
        txtRemarks.Clear()
        dtpDocDate.MinDate = GetMaxPEC().AddDays(1)
        dtpDocDate.Value = Date.Today.Date
        dtpStartDate.Value = DateTime.Now
        dtpEndDate.Value = DateTime.Now
    End Sub

    Private Sub frmDepreciation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            If TransID <> "" Then
                If Not AllowAccess("CV_VIEW") Then
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
                    tsbCopy.Enabled = False
                    EnableControl(False)
                Else
                    LoadDep(TransID)
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
                tsbCopy.Enabled = False
                EnableControl(False)
            End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)

    End Sub



    Private Sub LoadDep(ByVal ID As String)

        Dim query As String
        query = " SELECT  TransID, DP_No,  VCECode, AccntCode, ItemCode, QTY, UOM, ItemType, DateDP, StartDate, EndOfLife, Terms, " & _
                "          AcqCost,  AmortCost, SalvageValue, Remarks,  RR_Ref, RR_Ref_LineNum, Status" & _
                " FROM    tblDepreciation " & _
                " WHERE   TransID ='" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            DPNo = SQL.SQLDR("DP_No").ToString
            txtTransNum.Text = DPNo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtVCEName.Text = GetVCEName(SQL.SQLDR("VCECode").ToString)
            txtAccountCode.Text = SQL.SQLDR("AccntCode").ToString
            txtAccountTitle.Text = GetAccntTitle(SQL.SQLDR("AccntCode").ToString)
            txtItemCode.Text = SQL.SQLDR("ItemCode").ToString
            txtItemName.Text = GetItemName(SQL.SQLDR("ItemCode").ToString)
            txtQTY.Text = SQL.SQLDR("QTY").ToString
            txtUOM.Text = SQL.SQLDR("UOM").ToString
            txtItemType.Text = SQL.SQLDR("ItemType").ToString
            dtpDocDate.Value = SQL.SQLDR("DateDP").ToString
            dtpStartDate.Value = SQL.SQLDR("StartDate").ToString
            dtpEndDate.Value = SQL.SQLDR("EndOfLife").ToString
            txtTerms.Text = SQL.SQLDR("Terms").ToString
            txtAcqCost.Text = CDec(SQL.SQLDR("AcqCost").ToString).ToString("N2")
            txtAmort.Text = CDec(SQL.SQLDR("AmortCost")).ToString("N2")
            txtSalvage.Text = SQL.SQLDR("SalvageValue").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            RR_ID = SQL.SQLDR("RR_Ref").ToString
            LineID = SQL.SQLDR("RR_Ref_LineNum").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            txtRefRR.Text = LoadRRNo(RR_ID).ToCharArray + "-" + LineID.ToString

            LoadSchedule(TransID)

            ' TOOLSTRIP BUTTONS
            If txtStatus.Text = "Cancelled" Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            Else
                tsbEdit.Enabled = True
                tsbCancel.Enabled = True
            End If
            tsbClose.Enabled = False
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbSave.Enabled = False
            tsbNew.Enabled = True
            tsbSearch.Enabled = True
            tsbExit.Enabled = True
            If dtpDocDate.Value <= GetMaxPEC() Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            End If
            EnableControl(False)
        Else
            ClearText()
        End If
    End Sub


    Private Sub LoadSchedule(ByVal LoanID As String)
        Try
            lvAmmort.Columns.Clear()
            lvAmmort.Columns.Add("No.")
            lvAmmort.Columns.Add("Date")
            lvAmmort.Columns.Add("BB")
            lvAmmort.Columns.Add("Monthly Dep.")
            lvAmmort.Columns.Add("Net Book Value")
            lvAmmort.Columns(0).Width = 35
            lvAmmort.Columns(1).Width = 80
            lvAmmort.Columns(2).Width = 100
            lvAmmort.Columns(3).Width = 100
            lvAmmort.Columns(4).Width = 100
            lvAmmort.Columns(2).TextAlign = HorizontalAlignment.Right
            lvAmmort.Columns(3).TextAlign = HorizontalAlignment.Right
            lvAmmort.Columns(4).TextAlign = HorizontalAlignment.Right
            Dim query As String
            query = " SELECT  AmortNum, Date, BegBalance, Amort, NetBookValue " & _
                    " FROM    tblDepreciation_Schedule " & _
                    " WHERE   TransID = '" & LoanID & "' "
            SQL.CloseCon()
            SQL.ReadQuery(query)
            lvAmmort.Items.Clear()
            While SQL.SQLDR.Read
                lvAmmort.Items.Add(SQL.SQLDR("AmortNum"))
                With lvAmmort.Items(lvAmmort.Items.Count - 1).SubItems
                    .Add(SQL.SQLDR("Date"))
                    .Add(CDec(SQL.SQLDR("BegBalance")).ToString("N2"))
                    .Add(CDec(SQL.SQLDR("Amort")).ToString("N2"))
                    .Add(CDec(SQL.SQLDR("NetBookValue")).ToString("N2"))
                End With
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Function LoadRRNo(SO_ID As Integer) As String
        Dim query As String
        query = " SELECT RR_No FROM tblRR WHERE TransID = '" & SO_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("RR_No")
        Else
            Return 0
        End If
    End Function

    Private Sub txtTerms_TextChanged(sender As Object, e As EventArgs) Handles txtTerms.TextChanged
        'If txtAmort.Text > 0 Or txtAmort.Text <> "" Then
        '    Dim a As Integer = txtTerms.Text
        '    dtpEndDate.Value = CDate(dtpStartDate.Value.AddMonths(1).Month & "-01-" & dtpStartDate.Value.AddMonths(1).Year).AddDays(-1)
        '    dtpEndDate.Value = dtpEndDate.Value.AddMonths(a)

        'End If
        If txtTerms.Text = "" Then
            txtTerms.Text = 0
        End If


    End Sub

    Private Sub GenerateSchedule()
        Try
            If IsNumeric(txtAcqCost.Text) Then
                Dim terms As Integer
                Dim temp(20) As Decimal
                Dim a As Integer = 0
                Dim index As Integer = 5
                dtpEndDate.Value = DateAdd(DateInterval.Month, CDec(txtTerms.Text), dtpStartDate.Value.Date) ' GET MATURITY DATE BASED ON TERMS AND STARTING DATE

                ' ADD COLUMNS
                lvAmmort.Items.Clear()
                lvAmmort.Columns.Clear()
                lvAmmort.Columns.Add("No.")
                lvAmmort.Columns.Add("Date")
                lvAmmort.Columns.Add("BB")
                lvAmmort.Columns.Add("Monthly Dep.")
                lvAmmort.Columns.Add("Net Book Value")
                lvAmmort.Columns(0).Width = 35
                lvAmmort.Columns(1).Width = 80
                lvAmmort.Columns(2).Width = 100
                lvAmmort.Columns(3).Width = 100
                lvAmmort.Columns(4).Width = 100
                lvAmmort.Columns(2).TextAlign = HorizontalAlignment.Right
                lvAmmort.Columns(3).TextAlign = HorizontalAlignment.Right
                lvAmmort.Columns(4).TextAlign = HorizontalAlignment.Right

                terms = LoadPaymentDate()
                disableEvent = True
                txtTerms.Text = terms
                disableEvent = False
                ' GET AMORTIZATION BREAKDOWN BASED ON AMORT COUNT
                If lvAmmort.Items.Count > 0 Then
                    GenerateSchedSL(terms)
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function LoadPaymentDate() As Integer
        Dim terms As Integer
                ' FOR MONTHLY PAYMENT, GET ACTUAL NUMBER OF AMORTIZATION BY ADDING SELECTED DAY OF THE MONTH FROM STARTING DATE TO MATURITY DATE 
                terms = 0
        If IsNumeric(txtTerms.Text) AndAlso txtTerms.Text > 0 Then
            EUL = txtTerms.Text
        Else
            EUL = txtTerms.Text
        End If
        Do Until terms = CDec(EUL)
            lvAmmort.Items.Add(terms + 1)
            lvAmmort.Items(lvAmmort.Items.Count - 1).SubItems.Add(CDate(DateAdd(DateInterval.Month, terms, dtpStartDate.Value.Date)).ToString("MM/dd/yyyy"))
            terms += 1
            If terms = CDec(EUL) Then
                dtpEndDate.Value = DateAdd(DateInterval.Month, terms - 1, dtpStartDate.Value.Date)
            End If
        Loop
        Return terms
    End Function




    Private Sub GenerateSchedSL(ByVal terms As Integer)
        Dim principalBalance, interestBalance, totalBalance As Double
        Dim principalAmort, interestAmort, amortTotal As Decimal
        Dim a As Integer
        Dim temp(20) As Decimal

        ' SET REMAINING BALANCES VARIABLE

      
        principalBalance = CDec(txtAcqCost.Text)

        totalBalance = CDec(principalBalance + interestBalance)

        ' SET AMORT. VALUES
        principalAmort = CDbl(Math.Round(((txtAcqCost.Text - txtSalvage.Text) / terms), 2))
        amortTotal = 0
        Dim BB, Prin, Int, Amort As Decimal
        For i As Integer = 0 To terms - 1
            BB = principalBalance

            ' IF PRINCIPAL BALANCE IS LESS THAN PRICIPAL AMORT OR  PRINCIPAL BALANCE - PRINCIPAL AMORT IS LESS THAN 1
            If i = terms - 1 Then
                Prin = principalBalance - CDec(txtSalvage.Text)
                principalBalance -= principalBalance - CDec(txtSalvage.Text)
            Else
                Prin = principalAmort
                principalBalance -= principalAmort
            End If

            ' ADD INTEREST AMOUNT TO LIST VIEW
            ' IF 

            If interestBalance < interestAmort Or ((interestBalance - interestAmort) < 1) Then
                Int = interestBalance
                interestBalance -= interestBalance
            Else
                Int = interestAmort
                interestBalance -= interestAmort
            End If

            ' ADD TO LIST VIEW
            lvAmmort.Items(i).SubItems.Add(CDec(Math.Round(BB, 2)).ToString("N2")) '  <-- Beg Bal
            ' ADD OTHER VALUES TO LIST VIEW
            Amort = 0
            a = 0
            Amort = Prin + Int + Amort
            amortTotal = Amort + amortTotal
            lvAmmort.Items(i).SubItems.Add(CDec(Math.Round(Amort, 2)).ToString("N2")) ' ADD REMAINING BALANCE TO LIST VIEW
            lvAmmort.Items(i).SubItems.Add(CDec(Math.Round(principalBalance, 2)).ToString("N2")) ' ADD REMAINING BALANCE TO LIST VIEW
        Next
    End Sub



    Private Sub txtSalvage_TextChanged(sender As Object, e As EventArgs) Handles txtSalvage.TextChanged
        If txtSalvage.Text = "" Then
            txtSalvage.Text = 0
        End If

    End Sub

    Private Sub txtSalvage_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSalvage.KeyPress
        'If e.KeyChar <> ChrW(Keys.Back) Then
        '    If Char.IsNumber(e.KeyChar) Then

        '    Else
        '        MessageBox.Show("Invalid Input! Numbers Only.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        e.Handled = True
        '    End If
        'End If

        If (Not Char.IsControl(e.KeyChar) _
                       AndAlso (Not Char.IsDigit(e.KeyChar) _
                       AndAlso (e.KeyChar <> Microsoft.VisualBasic.ChrW(46)))) Then
            e.Handled = True
        End If

    End Sub

    Private Sub txtAcqCost_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAcqCost.KeyPress
        If (Not Char.IsControl(e.KeyChar) _
                      AndAlso (Not Char.IsDigit(e.KeyChar) _
                      AndAlso (e.KeyChar <> Microsoft.VisualBasic.ChrW(46)))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAmort_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmort.KeyPress

    End Sub

    Private Sub txtAmort_TextChanged(sender As Object, e As EventArgs) Handles txtAmort.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnCompute.Click
        AcqComp()
    End Sub


    Private Sub txtSalvage_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSalvage.KeyDown

    End Sub

    Private Sub tsbCopyAPV_Click(sender As System.Object, e As System.EventArgs)
        Try
            Dim f As New frmLoadTransactions
            f.txtFilter.Text = "Active"
            f.txtFilter.Enabled = False
            f.cbFilter.Enabled = False
            f.btnSearch.Enabled = False

            f.ShowDialog("APV")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("DP_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            DPNo = ""
            RR_ID = 0
            LineID = 0

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
            tsbCopy.Enabled = True
            EnableControl(True)

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
        End If
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        txtItemCode.ReadOnly = True

        If RR_ID <> 0 And LineID <> 0 Then
            txtVCEName.Enabled = False
            txtItemName.Enabled = False
            txtAccountTitle.Enabled = False
            txtAcqCost.Enabled = False
        Else
            txtVCEName.Enabled = Value
            txtItemName.Enabled = Value
            txtAccountTitle.Enabled = Value
            txtAcqCost.Enabled = Value
        End If


        btnCompute.Enabled = Value
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        dtpStartDate.Enabled = Value
        txtQTY.Enabled = False
        txtRefRR.Enabled = False
        txtInvoiceNo.Enabled = Value
        txtSalvage.Enabled = Value
        txtTerms.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If

    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        Try
            If TransAuto = False AndAlso IfExist(txtTransNum.Text) And TransID = "" Then
                MsgBox("DP" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
            ElseIf TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID(ColumnID, DBTable)
                    If TransAuto Then
                        DPNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    Else
                        DPNo = txtTransNum.Text
                    End If
                    txtTransNum.Text = DPNo
                    SaveDepreciation()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    LoadDep(TransID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    If DPNo = txtTransNum.Text Then
                        DPNo = txtTransNum.Text
                        EditDepreciation()
                        Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                        DPNo = txtTransNum.Text
                        LoadDep(TransID)
                    Else
                        If Not IfExist(txtTransNum.Text) Then
                            DPNo = txtTransNum.Text
                            EditDepreciation()
                            Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                            DPNo = txtTransNum.Text
                            LoadDep(TransID)
                        Else
                            MsgBox("DP" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
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
        query = " SELECT * FROM tblDepreciation WHERE DP_No ='" & ID & "'  "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("DP_EDIT") Then
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
            tsbCopy.Enabled = False
        End If
    End Sub

    Private Sub gbDep_Enter(sender As System.Object, e As System.EventArgs) Handles gbDep.Enter

    End Sub

    Private Sub txtAccntCode_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)

    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click

        ' Toolstrip Buttons
        If DPNo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
        Else
            LoadDep(TransID)
            tsbEdit.Enabled = True
            tsbCancel.Enabled = True
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
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

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If DPNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblDepreciation  " & _
                    "   INNER JOIN tblBranch  ON	          " & _
                    "   tblDepreciation.BranchCode = tblBranch.BranchCode    " & _
                    "     WHERE          " & _
                    " 	( tblDepreciation.BranchCode IN  " & _
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
                    " AND DP_No < '" & DPNo & "' ORDER BY DP_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadDep(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If DPNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblDepreciation  " & _
                    "   INNER JOIN tblBranch  ON	          " & _
                    "   tblDepreciation.BranchCode = tblBranch.BranchCode    " & _
                    "     WHERE          " & _
                    " 	( tblDepreciation.BranchCode IN  " & _
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
                    " AND DP_No > '" & DPNo & "' ORDER BY DP_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadDep(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("DP_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If txtStatus.Text <> "Cancelled" AndAlso MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim updateSQL As String
                        updateSQL = " UPDATE  tblDepreciation SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        DPNo = txtTransNum.Text
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
                        tsbCopy.Enabled = False
                        EnableControl(False)

                        DPNo = txtTransNum.Text
                        LoadDep(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "DP_No", DPNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try

                End If
            End If
        End If
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("DP_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("DP")
            TransID = f.transID
            LoadDep(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub FromRRToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromRRToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("RR-Depre-perLine")
        LoadRR(f.transID)
        f.Dispose()
    End Sub


    Private Sub LoadRR(ByVal ID As String)
        If ID.Contains("-") Then
            RR_ID = Strings.Left(ID, ID.IndexOf("-"))
            LineID = Strings.Mid(ID, ID.IndexOf("-") + 2, ID.ToString.Length - RR_ID.ToString.Length)
        End If
        Dim query As String
        query = " SELECT     tblRR.TransID, tblRR.VCECode, RR_No,  DateRR,  Remarks, AD_FixedAsset," & _
                "            tblRR_Details.ItemCode, tblItem_Master.ItemName, tblRR_Details.UOM, QTY, ItemType, LineNum, tblRR_Details.GrossAmount " & _
                " FROM       tblRR INNER JOIN tblRR_Details " & _
                " ON         tblRR.TransID = tblRR_Details.TransID " & _
                " AND        tblRR_Details.LineNum = '" & LineID & "' " & _
                "   INNER JOIN tblItem_Master " & _
                " ON         tblItem_Master.ItemCode = tblRR_Details.ItemCode " & _
                " WHERE      tblRR.TransId = '" & RR_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtRefRR.Text = SQL.SQLDR("RR_No").ToString & "-" & SQL.SQLDR("LineNum").ToString
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtVCEName.Text = GetVCEName(SQL.SQLDR("VCECode").ToString)
            txtAccountCode.Text = SQL.SQLDR("AD_FixedAsset").ToString
            txtAccountTitle.Text = GetAccntTitle(SQL.SQLDR("AD_FixedAsset").ToString)
            txtItemCode.Text = SQL.SQLDR("ItemCode").ToString
            txtItemName.Text = SQL.SQLDR("ItemName").ToString
            txtItemType.Text = SQL.SQLDR("ItemType").ToString
            txtQTY.Text = SQL.SQLDR("QTY").ToString
            txtUOM.Text = SQL.SQLDR("UOM").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtAcqCost.Text = CDec(SQL.SQLDR("GrossAmount")).ToString("N2")

            txtVCEName.Enabled = False
            txtItemName.Enabled = False
            txtAccountTitle.Enabled = False
            txtAcqCost.Enabled = False
        Else
            ClearText()
        End If
    End Sub

    Private Sub Label17_Click(sender As System.Object, e As System.EventArgs) Handles Label17.Click

    End Sub

    Private Sub txtAccountTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtAccountTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtAccountTitle.Text)
                txtAccountTitle.Text = f.accttile
                txtAccountCode.Text = f.accountcode
                f.Dispose()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtAccountTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtAccountTitle.TextChanged

    End Sub

    Private Sub txtItemName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtItemName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmCopyFrom
            f.ShowDialog("All Item", txtItemName.Text)
            txtItemCode.Text = f.TransID
            txtItemName.Text = GetItemName(txtItemCode.Text)
            txtItemType.Text = GetItemType(txtItemCode.Text)
            f.Dispose()
        End If
    End Sub

    Private Sub txtItemName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtItemName.TextChanged

    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim TW As System.IO.TextWriter
        Dim QR As String = Nothing

        Dir = App_Path.ToString.Replace("\Debug", "") & "\QRCodePrinting\"
        TW = System.IO.File.CreateText(Dir + "QRCodePrinting.txt")

        QR = txtTransNum.Text.Trim & "|" & txtItemCode.Text.Trim & "|" & txtItemName.Text.Trim
            TW.WriteLine(QR)

        TW.Flush()
        TW.Close()
        Call cmdPrint()
    End Sub

    Private Sub cmdPrint()
        Dim process As New Process
        Try
            process.StartInfo.UseShellExecute = True
            process.StartInfo.WorkingDirectory = Dir
            process.StartInfo.Arguments = "QRCode.btw" + " /P/X"
            process.StartInfo.FileName = Dir + "\BarTender\BarTend.exe"
            process.Start()
            process.WaitForExit()
            process.Close()
            process.Dispose()
        Catch ex As Exception

        Finally

        End Try
    End Sub

    Private Sub TestToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles TestToolStripMenuItem1.Click
        Dim f As New frmReport_Filter
        f.Report = "Fixed Asset List"
        f.ShowDialog("FA")
        f.Dispose()
    End Sub
End Class