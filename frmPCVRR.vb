Public Class frmPCVRR

    Dim TransID, RefID, JETransiD As String
    Dim PCVNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "PCFRR"
    Dim ColumnPK As String = "PCVRR_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblPCVRR"
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
        Else
            dgvRecords.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        txtRemarks.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub


    Private Sub ClearText()
        txtBaseAmount.Text = ""
        txtInputVAT.Text = ""
        txtAmount.Text = ""
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        txtRemarks.Text = ""
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
                        " tblPCVRR (TransID, PCVRR_No, VCECode, DatePCVRR, Remarks,  WhoCreated, Amount, InputVAT, BaseAmount, Status) " & _
                        " VALUES(@TransID, @PCVRR_No,  @VCECode, @DatePCVRR,  @Remarks, @WhoCreated, @Amount, @InputVAT, @BaseAmount, @Status)"
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@PCVRR_No", PCVNo)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DatePCVRR", dtpDocDate.Value.Date)
            SQL.AddParam("@Amount", CDec(txtAmount.Text))
            SQL.AddParam("@InputVAT", CDec(txtInputVAT.Text))
            SQL.AddParam("@BaseAmount", CDec(txtBaseAmount.Text))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)


            Dim line As Integer = 1
            For Each item As DataGridViewRow In dgvRecords.Rows

                If item.Cells(chTransID.Index).Value <> Nothing Then
                    insertSQL = " INSERT INTO " & _
                                       " tblPCVRR_Details(TransID,  PCV_TransID, VCECode,  Amount, InputVAT, BaseAmount, Remarks, LineNum) " & _
                                       " VALUES(@TransID,  @PCV_TransID, @VCECode,  @Amount, @InputVAT, @BaseAmount, @Remarks, @LineNum)"
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@PCV_TransID", item.Cells(chTransID.Index).Value.ToString)
                    SQL.AddParam("@VCECode", item.Cells(chVCECode.Index).Value.ToString)
                    SQL.AddParam("@Amount", CDec(item.Cells(chTotalAmount.Index).Value))
                    SQL.AddParam("@InputVAT", CDec(item.Cells(chInputVAT.Index).Value))
                    SQL.AddParam("@BaseAmount", CDec(item.Cells(chAmountofVAT.Index).Value))
                    If item.Cells(chRemarks.Index).Value Is Nothing Then
                        SQL.AddParam("@Remarks", "")
                    Else
                        SQL.AddParam("@Remarks", item.Cells(chRemarks.Index).Value.ToString)
                    End If
                    SQL.AddParam("@LineNum", line)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "PCVRR_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub LoadPCVDetails(ByVal ID As Integer)
        
    End Sub
    
    Private Sub UpdatePCV()
        Try
            Dim insertSQL, updateSQL, deleteSQL As String
            activityStatus = True
            updateSQL = " UPDATE tblPCVRR  " & _
                        " SET    PCVRR_No = @PCVRR_No, VCECode = @VCECode, DatePCVRR = @DatePCVRR, Amount = @Amount, InputVAT = @InputVAT, BaseAmount = @BaseAmount, " & _
                        "         Remarks = @Remarks,  WhoModified = @WhoModified, DateModified = GETDATE() " & _
                        " WHERE TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@PCVRR_No", PCVNo)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DatePCVRR", dtpDocDate.Value.Date)
            SQL.AddParam("@Amount", CDec(txtAmount.Text))
            SQL.AddParam("@InputVAT", CDec(txtInputVAT.Text))
            SQL.AddParam("@BaseAmount", CDec(txtBaseAmount.Text))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)



            ' DELETE ACCOUNTING ENTRIES
            deleteSQL = " DELETE FROM tblPCVRR_Details  WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.ExecNonQuery(deleteSQL)

             Dim line As Integer = 1
            For Each item As DataGridViewRow In dgvRecords.Rows

                If item.Cells(chTransID.Index).Value <> Nothing Then
                    insertSQL = " INSERT INTO " & _
                                       " tblPCVRR_Details(TransID,  PCV_TransID, VCECode,  Amount, InputVAT, BaseAmount, Remarks, LineNum) " & _
                                       " VALUES(@TransID,  @PCV_TransID, @VCECode,  @Amount, @InputVAT, @BaseAmount, @Remarks, @LineNum)"
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@PCV_TransID", item.Cells(chTransID.Index).Value.ToString)
                    SQL.AddParam("@VCECode", item.Cells(chVCECode.Index).Value.ToString)
                    SQL.AddParam("@Amount", CDec(item.Cells(chTotalAmount.Index).Value))
                    SQL.AddParam("@InputVAT", CDec(item.Cells(chInputVAT.Index).Value))
                    SQL.AddParam("@BaseAmount", CDec(item.Cells(chAmountofVAT.Index).Value))
                    If item.Cells(chRemarks.Index).Value Is Nothing Then
                        SQL.AddParam("@Remarks", "")
                    Else
                        SQL.AddParam("@Remarks", item.Cells(chRemarks.Index).Value.ToString)
                    End If
                    SQL.AddParam("@LineNum", line)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "PCVRR_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

   

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("PCFRR_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("PCVRR")
            TransID = f.transID
            LoadPCVRR(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub LoadPCVRR(ByVal ID As String)
        Dim query As String
        query = " SELECT tblPCVRR.TransID, tblPCVRR.PCVRR_No, tblPCVRR.VCECode, VCEName, tblPCVRR.DatePCVRR,  Amount, InputVAT, BaseAmount, " & _
                "          tblPCVRR.Remarks, View_PCVRR_Balance.Status  " & _
                " FROM    tblPCVRR INNER JOIN View_PCVRR_Balance " & _
                " ON      tblPCVRR.TransID = View_PCVRR_Balance.TransID " & _
                " WHERE   tblPCVRR.TransID = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            PCVNo = SQL.SQLDR("PCVRR_No").ToString
            txtTransNum.Text = PCVNo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtVCEName.Text = SQL.SQLDR("VCEName").ToString
            dtpDocDate.Value = SQL.SQLDR("DatePCVRR")
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtAmount.Text = CDec(SQL.SQLDR("Amount")).ToString("N2")
            txtInputVAT.Text = CDec(SQL.SQLDR("InputVAT")).ToString("N2")
            txtBaseAmount.Text = CDec(SQL.SQLDR("BaseAmount")).ToString("N2")
            txtStatus.Text = SQL.SQLDR("Status").ToString
            LoadPCVRRDetails(TransID)

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

    Private Sub LoadPCVRRDetails(ByVal ID As Integer)
        Dim query As String
        query = " SELECT tblPCVRR_Details.TransID,  PCV_TransID, DatePCV, PCV_No, tblPCVRR_Details.VCECode, VCEName, tblPCVRR_Details.Amount, tblPCVRR_Details.InputVAT, tblPCVRR_Details.BaseAmount, tblPCVRR_Details.Remarks" & _
                " FROM    tblPCVRR_Details " & _
                " LEFT JOIN viewVCE_Master " & _
                " ON      tblPCVRR_Details.VCECode = viewVCE_Master.VCECode " & _
                " LEFT JOIN tblPCV " & _
                " ON      tblPCV.TransID = tblPCVRR_Details.PCV_TransID " & _
                " WHERE  tblPCVRR_Details.TransID = '" & ID & "' " & _
                " ORDER BY LineNum "
        SQL.ReadQuery(query)
        dgvRecords.Rows.Clear()
        Dim rowsCount As Integer = 0
        While SQL.SQLDR.Read

            dgvRecords.Rows.Add(SQL.SQLDR("PCV_TransID").ToString)
            dgvRecords.Rows(rowsCount).Cells(chPCVDate.Index).Value = CDate(SQL.SQLDR("DatePCV").ToString)
            dgvRecords.Rows(rowsCount).Cells(chPCVNo.Index).Value = SQL.SQLDR("PCV_No").ToString
            dgvRecords.Rows(rowsCount).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
            dgvRecords.Rows(rowsCount).Cells(chVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
            dgvRecords.Rows(rowsCount).Cells(chTotalAmount.Index).Value = CDec(SQL.SQLDR("Amount")).ToString("N2")
            dgvRecords.Rows(rowsCount).Cells(chInputVAT.Index).Value = CDec(SQL.SQLDR("InputVAT")).ToString("N2")
            dgvRecords.Rows(rowsCount).Cells(chAmountofVAT.Index).Value = CDec(SQL.SQLDR("BaseAmount")).ToString("N2")
            dgvRecords.Rows(rowsCount).Cells(chRemarks.Index).Value = SQL.SQLDR("Remarks").ToString

            rowsCount += 1

        End While
        ComputeTotal()
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("PCFRR_ADD") Then
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
            txtStatus.Text = "Open"
            EnableControl(True)


            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
            txtVCEName.Select()
        End If
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("PCFRR_EDIT") Then
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

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("PCFRR_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE  tblPCVRR SET Status ='Cancelled' WHERE PCVRR_No = @PCVRR_No "
                        SQL.FlushParams()
                        PCVNo = txtTransNum.Text
                        SQL.AddParam("@PCVRR_No", PCVNo)
                        SQL.ExecNonQuery(deleteSQL)

                        deleteSQL = " UPDATE  tblPCVRR_Details SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        PCVNo = txtTransNum.Text
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
                        tsbCopy.Enabled = False
                        EnableControl(False)

                        PCVNo = txtTransNum.Text
                        LoadPCV(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "PCVRR_No", PCVNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub


    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If PCVNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblPCVRR  WHERE PCVRR_No < '" & PCVNo & "' ORDER BY PCVRR_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                PCVNo = SQL.SQLDR("TransID").ToString
                LoadPCVRR(PCVNo)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If PCVNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblPCVRR  WHERE PCVRR_No > '" & PCVNo & "' ORDER BY PCVRR_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                PCVNo = SQL.SQLDR("TransID").ToString
                LoadPCVRR(PCVNo)
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
            LoadPCVRR(TransID)
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

   

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If txtVCECode.Text = "" Then
            Msg("Please enter VCECode!", MsgBoxStyle.Exclamation)
        ElseIf dgvRecords.Rows.Count = 0 Then
            MsgBox("No entries, Please check!", MsgBoxStyle.Exclamation)
        ElseIf TransAuto = False And txtTransNum.Text = "" Then
            MsgBox("Please Enter PCVRR No.!", MsgBoxStyle.Exclamation)
        ElseIf TransAuto = False AndAlso IfExist(txtTransNum.Text) And TransID = "" Then
            MsgBox("PCVRR" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
        ElseIf TransID = "" Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                TransID = GenerateTransID(ColumnID, DBTable)
                If TransAuto = True Then
                    PCVNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                Else
                    PCVNo = txtTransNum.Text
                End If
                SavePCV()
                Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                LoadPCVRR(TransID)
            End If
        Else
            If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                If PCVNo = txtTransNum.Text Then
                    PCVNo = txtTransNum.Text
                    UpdatePCV()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    PCVNo = txtTransNum.Text
                    LoadPCVRR(TransID)
                Else
                    If Not IfExist(txtTransNum.Text) Then
                        PCVNo = txtTransNum.Text
                        UpdatePCV()
                        Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                        PCVNo = txtTransNum.Text
                        LoadPCVRR(TransID)
                    Else
                        MsgBox("PCV" & " " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
                    End If
                End If
            End If
        End If
    End Sub

    Private Function IfExist(ByVal ID As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblPCVRR WHERE PCVRR_No ='" & ID & "'  "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

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
                If tsbPrint.Enabled = True Then tsbPrint.PerformClick()
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



    Dim eColIndex As Integer = 0
    Private Sub dgvRecords_CellBeginEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvRecords.CellBeginEdit
        eColIndex = e.ColumnIndex
    End Sub



    Private Sub dgvRecords_DataError(sender As Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvRecords.DataError
        Try

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub dgvRecords_RowsRemoved(sender As Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgvRecords.RowsRemoved
        Try
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FromPCVToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromPCVToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.chkBatch.Visible = True
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("PCVRR-PCV")
        If f.batch = True Then
            For Each row As DataGridViewRow In f.dgvList.Rows
                If row.Cells(f.dgvList.Columns.Count - 1).Value = True Then
                    LoadPCV(row.Cells(0).Value)
                End If
            Next
        Else
            If f.transID <> "" Then
                LoadPCV(f.transID)
            End If
        End If
        f.Dispose()
    End Sub

    Private Sub LoadPCV(ByVal PCV As String)
        Try
            Dim query As String
            query = " SELECT TransID, DatePCV, PCV_No, tblPCV.VCECode, VCEName, Amount, BaseAmount, InputVAT, Remarks " & _
                    " FROM tblPCV " & _
                    " INNER JOIN " & _
                    " viewVCE_Master ON " & _
                    " viewVCE_Master.VCECode = tblPCV.VCECode " & _
                    " WHERE  TransID ='" & PCV & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                dgvRecords.Rows.Add(SQL.SQLDR("TransID").ToString, CDate(SQL.SQLDR("DatePCV").ToString), SQL.SQLDR("PCV_No").ToString, SQL.SQLDR("VCECode").ToString, SQL.SQLDR("VCEName").ToString, _
                                    CDec(SQL.SQLDR("Amount").ToString).ToString("N2"), CDec(SQL.SQLDR("BaseAmount").ToString).ToString("N2"), CDec(SQL.SQLDR("InputVAT").ToString).ToString("N2"), SQL.SQLDR("Remarks").ToString)
            End If
            ComputeTotal()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("PCVRR", TransID)
        f.Dispose()
    End Sub

    Private Sub dgvRecords_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvRecords.CellContentClick

    End Sub

    Public Sub ComputeTotal()
        Try
            ' COMPUTE TOTAL AMOUNT
            Dim a As Double = 0
            For i As Integer = 0 To dgvRecords.Rows.Count - 1
                If dgvRecords.Item(chTransID.Index, i).Value <> "" AndAlso Val(dgvRecords.Item(chTotalAmount.Index, i).Value) <> 0 Then
                    a = a + Double.Parse(dgvRecords.Item(chTotalAmount.Index, i).Value).ToString("N2")
                End If
            Next
            txtAmount.Text = a.ToString("N2")

            ' COMPUTE TOTAL INPUT VAT 
            Dim b As Double = 0
            For i As Integer = 0 To dgvRecords.Rows.Count - 1
                If dgvRecords.Item(chTransID.Index, i).Value <> "" AndAlso Val(dgvRecords.Item(chInputVAT.Index, i).Value) <> 0 Then
                    b = b + Double.Parse(dgvRecords.Item(chInputVAT.Index, i).Value).ToString("N2")
                End If
            Next
            txtInputVAT.Text = b.ToString("N2")

            ' COMPUTE TOTAL BASE AMOUNT
            Dim c As Double = 0
            For i As Integer = 0 To dgvRecords.Rows.Count - 1
                If dgvRecords.Item(chTransID.Index, i).Value <> "" AndAlso Val(dgvRecords.Item(chAmountofVAT.Index, i).Value) <> 0 Then
                    c = c + Double.Parse(dgvRecords.Item(chAmountofVAT.Index, i).Value).ToString("N2")
                End If
            Next
            txtBaseAmount.Text = c.ToString("N2")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class