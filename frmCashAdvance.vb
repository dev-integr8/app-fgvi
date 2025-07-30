Public Class frmCashAdvance
    Dim TransID, RefID, JETransiD As String
    Dim CANo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "CA"
    Dim ColumnPK As String = "CA_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblCA"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim PO_ID As Integer
    Dim accntDR, accntCR, accntVAT As String
    Dim ForApproval As Boolean = False

    Public Overloads Function ShowDialog(ByVal DocNumber As String) As Boolean
        CANo = DocNumber
        MyBase.ShowDialog()
        Return True
    End Function

 

    Private Sub LoadAccount()
        Dim query As String
        query = " SELECT  tblDefaultAccount.AccntCode, AccountTitle " & _
                " FROM    tblDefaultAccount " & _
                " INNER JOIN tblCOA_Master ON " & _
                " tblCOA_Master.AccountCode = tblDefaultAccount.AccntCode " & _
                " WHERE   tblDefaultAccount.Status = 'Active' AND Type = 'CA'" & _
                " ORDER BY AccountTitle "
        SQL.ReadQuery(query)
        cbDefAccount.Items.Clear()
        While SQL.SQLDR.Read
            cbDefAccount.Items.Add(SQL.SQLDR("AccountTitle"))
        End While
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        txtVCEName.Enabled = Value
        btnSearchVCE.Enabled = Value
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        txtAmount.Enabled = Value
        cbDefAccount.Enabled = Value
        cbCostCenter.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Private Sub LoadCA(ID As String)
        Dim query, costID As String
        'query = " SELECT  TransID, CA_No, tblCA.VCECOde, VCEName, DateCA, ISNULL(Amount,0) AS Amount, AccntCode, " & _
        '        "          Remarks,  tblCA.Status, Code + ' - ' + Description AS Description  " & _
        '        " FROM    tblCA LEFT JOIN viewVCE_Master " & _
        '        " ON      tblCA.VCECode = viewVCE_Master.VCECode " & _
        '        " LEFT JOIN tblCC ON " & _
        '        " tblCC.Code = tblCA.CostID " & _
        '        " WHERE   TransID ='" & ID & "' "
        query = "  SELECT  tblCA.TransID, CA_No, tblCA.VCECOde, View_CA_Balance.VCEName, DateCA, ISNULL(Amount,0) AS Amount, tblCA.AccntCode,  " & vbCrLf & _
                "          tblCA.Remarks,  View_CA_Balance.Status, Code + ' - ' + Description AS Description   " & vbCrLf & _
                "  FROM    tblCA  " & vbCrLf & _
                "  INNER JOIN View_CA_Balance ON " & vbCrLf & _
                "  tblCA.TransID = View_CA_Balance.TransID " & vbCrLf & _
                "  LEFT JOIN viewVCE_Master  " & vbCrLf & _
                "  ON      tblCA.VCECode = viewVCE_Master.VCECode  " & vbCrLf & _
                "  LEFT JOIN tblCC ON  " & vbCrLf & _
                "  tblCC.Code = tblCA.CostID  " & vbCrLf & _
                " WHERE   tblCA.TransID ='" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            CANo = SQL.SQLDR("CA_No").ToString
            txtTransNum.Text = CANo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
            dtpDocDate.Value = SQL.SQLDR("DateCA")
            txtAmount.Text = CDec(SQL.SQLDR("Amount")).ToString("N2")
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            cbCostCenter.SelectedItem = SQL.SQLDR("Description").ToString
            cbDefAccount.SelectedItem = GetAccntTitle(SQL.SQLDR("AccntCode").ToString)
            ' TOOLSTRIP BUTTONS
            If txtStatus.Text = "Cancelled" Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            ElseIf txtStatus.Text = "Closed" Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            ElseIf txtStatus.Text = "Released" Then
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
            If dtpDocDate.Value <= GetMaxPEC() Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            End If
            EnableControl(False)
        Else
            ClearText()
        End If
    End Sub


    Private Sub txtVCEName_KeyDown_1(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.txtFilter.Text = txtVCEName.Text
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            txtVCEName.Text = f.VCEName
        End If
    End Sub

    Private Sub ClearText()
        txtTransNum.Clear()
        txtVCECode.Clear()
        txtVCEName.Clear()
        txtRemarks.Clear()
        txtStatus.Text = "Open"
        cbDefAccount.SelectedIndex = -1
        cbCostCenter.SelectedIndex = -1
        dtpDocDate.MinDate = GetMaxPEC().AddDays(1)
        dtpDocDate.Value = Date.Today.Date
        txtAmount.Text = "0.00"
    End Sub

    Private Sub SaveCA()
        Dim SQL1 As New SQLControl
        SQL1.BeginTransaction()
        Try
            If cbDefAccount.SelectedIndex = -1 Then
                AccntCode = ""
            Else
                AccntCode = GetAccntCode(cbDefAccount.SelectedItem)
            End If
            Dim insertSQL, costID As String
            If cbCostCenter.SelectedIndex = -1 Then
                costID = ""
            Else
                costID = Strings.Left(cbCostCenter.SelectedItem, cbCostCenter.SelectedItem.ToString.IndexOf(" - "))
                activityStatus = True
            End If
            insertSQL = " INSERT INTO " & _
                    " tblCA    (TransID, CA_No, BranchCode, BusinessCode, VCECode, DateCA,  Amount, AccntCode,  Remarks, CostID, WhoCreated, Status) " & _
                    " VALUES (@TransID, @CA_No, @BranchCode, @BusinessCode, @VCECode, @DateCA, @Amount, @AccntCode,  @Remarks, @CostID, @WhoCreated, @Status) "
            SQL1.FlushParams()
            SQL1.AddParam("@TransID", TransID)
            SQL1.AddParam("@CA_No", CANo)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@VCECode", txtVCECode.Text)
            SQL1.AddParam("@DateCA", dtpDocDate.Value.Date)
            SQL1.AddParam("@AccntCode", AccntCode)
            SQL1.AddParam("@Amount", CDec(txtAmount.Text))
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@CostID", costID)
            If ForApproval = True Then SQL1.AddParam("@Status", "Draft") Else SQL1.AddParam("@Status", "Active")
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.ExecNonQuery(insertSQL)
            SQL1.Commit()
        Catch ex As Exception
            SQL1.Rollback()
            TransID = ""
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "CA_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL1.FlushParams()
        End Try
    End Sub

    Private Sub UpdateCA()
        Dim SQL As New SQLControl
        SQL.BeginTransaction()
        Try
            If cbDefAccount.SelectedIndex = -1 Then
                AccntCode = ""
            Else
                AccntCode = GetAccntCode(cbDefAccount.SelectedItem)
            End If
            Dim insertSQL, costID As String
            costID = Strings.Left(cbCostCenter.SelectedItem, cbCostCenter.SelectedItem.ToString.IndexOf(" - "))
            activityStatus = True
            insertSQL = " UPDATE tblCA " & _
                        " SET    CA_No = @CA_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " & _
                        "        VCECode = @VCECode, DateCA = @DateCA, Amount = @Amount, CostID = @CostID,   " & _
                        "         AccntCode = @AccntCode, " & _
                        "        Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE() " & _
                        " WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@CA_No", CANo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateCA", dtpDocDate.Value.Date)
            SQL.AddParam("@AccntCode", AccntCode)
            SQL.AddParam("@Amount", CDec(txtAmount.Text))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@CostID", costID)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(insertSQL)
            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "CA_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub txtAmount_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyValue <> 8 And Not (e.KeyValue >= 48 And e.KeyValue <= 57) Then
                e.SuppressKeyPress = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("CA_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("CA")
            TransID = f.transID
            LoadCA(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("CA_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            CANo = ""

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
        If Not AllowAccess("CA_EDIT") Then
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

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        Try
            If txtVCECode.Text = "" Then
                Msg("Please enter VCECode!", MsgBoxStyle.Exclamation)
            ElseIf txtRemarks.Text = "" Then
                Msg("Please enter a remark/short explanation", MsgBoxStyle.Exclamation)
            ElseIf cbDefAccount.SelectedIndex = -1 Then
                Msg("Please select default advances account!", MsgBoxStyle.Exclamation)
            ElseIf Not IsNumeric(txtAmount.Text) Then
                Msg("Invalid Amount!", MsgBoxStyle.Exclamation)
            ElseIf CDec(txtAmount.Text) <= 0 Then
                Msg("Amount should be greater than zero!", MsgBoxStyle.Exclamation)
            ElseIf TransAuto = False AndAlso IfExist(txtTransNum.Text) And TransID = "" Then
                MsgBox("CA" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
            ElseIf TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID("TransID", DBTable)
                    If TransAuto Then
                        CANo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    Else
                        CANo = txtTransNum.Text
                    End If
                    txtTransNum.Text = CANo
                    SaveCA()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    LoadCA(TransID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    If CANo = txtTransNum.Text Then
                        CANo = txtTransNum.Text
                        UpdateCA()
                        Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                        CANo = txtTransNum.Text
                        LoadCA(TransID)
                    Else
                        If Not IfExist(txtTransNum.Text) Then
                            CANo = txtTransNum.Text
                            UpdateCA()
                            Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                            CANo = txtTransNum.Text
                            LoadCA(TransID)
                        Else
                            MsgBox("CA" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
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
        query = " SELECT * FROM tblCA WHERE CA_No ='" & ID & "'  "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("CA", TransID)
        f.Dispose()
    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("CA_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE  tblCA SET Status ='Cancelled' WHERE CA_No = @CA_No "
                        SQL.FlushParams()
                        CANo = txtTransNum.Text
                        SQL.AddParam("@CA_No", CANo)
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

                        CANo = txtTransNum.Text
                        LoadCA(CANo)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "CA_No", CANo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If CANo <> "" Then
            Dim query As String
            query = " SELECT Top 1 CA_No FROM tblCA  WHERE CA_No < '" & CANo & "' ORDER BY CA_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                CANo = SQL.SQLDR("CA_No").ToString
                LoadCA(CANo)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If CANo <> "" Then
            Dim query As String
            query = " SELECT Top 1 CA_No FROM tblCA  WHERE CA_No > '" & CANo & "' ORDER BY CA_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                CANo = SQL.SQLDR("CA_No").ToString
                LoadCA(CANo)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If CANo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadCA(CANo)
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

    Private Sub btnSearchVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchVCE.Click
        Dim f As New frmVCE_Search
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        txtVCEName.Text = f.VCEName
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub frmADV_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
            ElseIf e.KeyCode = Keys.Right Then
                If tsbNext.Enabled = True Then tsbNext.PerformClick()
            ElseIf e.KeyCode = Keys.P Then
                If tsbPrint.Enabled = True Then tsbPrint.PerformClick()
            End If
        ElseIf e.Alt = True Then
            If e.KeyCode = Keys.F4 Then
                If tsbPrint.Enabled = True Then
                    tsbPrint.PerformClick()
                Else
                    e.SuppressKeyPress = True
                End If
            End If
        ElseIf e.KeyCode = Keys.Escape Then
            If tsbClose.Enabled = True Then tsbClose.PerformClick()
        End If
    End Sub

    Private Sub Validate_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyValue <> 8 And Not (e.KeyValue >= 48 And e.KeyValue <= 57) Then
                e.SuppressKeyPress = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub cbDefAccount_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbDefAccount.KeyPress
        e.Handled = True
    End Sub

    Private Sub txtAdvAmount_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtAmount.KeyDown
        Try
           
            If e.KeyCode = Keys.Enter Then
                If txtAmount.Text = "" Then
                    MsgBox("Please enter an amount", MsgBoxStyle.Exclamation)
                Else
                    txtAmount.Text = CDec(txtAmount.Text).ToString("N2")
                End If
            ElseIf e.KeyValue <> 8 And Not (e.KeyValue >= 48 And e.KeyValue <= 57) And Not (e.KeyCode >= 96 And e.KeyCode <= 105) And Not (e.KeyCode = 110) Then
                e.SuppressKeyPress = True
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmCashAdvance_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            LoadAccount()
            LoadCostCenter()
            If CANo <> "" Then
                LoadCA(CANo)
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

            Dim date1 As DateTime
            date1 = Convert.ToDateTime(Date.Today.Date + " 16:31")
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadCostCenter()

        Dim selectSQL As String = " SELECT Code + ' - ' + Description AS Description  FROM tblCC WHERE Status = 'Active'"
        SQL.ReadQuery(selectSQL, 2)
        cbCostCenter.Items.Clear()
        While SQL.SQLDR2.Read
            cbCostCenter.Items.Add(SQL.SQLDR2("Description").ToString)
        End While

    End Sub

    Private Sub txtAmount_LostFocus(sender As Object, e As System.EventArgs) Handles txtAmount.LostFocus
        If IsNumeric(txtAmount.Text) Then
            txtAmount.Text = CDec(txtAmount.Text).ToString("N2")
        End If
    End Sub
   
    Private Sub txtAmount_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtAmount.TextChanged

    End Sub

    Private Sub PCVListToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PCVListToolStripMenuItem.Click
        Dim f As New frmReport_Filter
        f.Report = "CA List"
        f.ShowDialog()
        f.Dispose()
    End Sub
End Class