Public Class frmBooking

    Dim TransID, RefID As String
    Dim BMNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "BM"
    Dim ColumnPK As String = "BM_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblBM"
    Dim TransAuto As Boolean
    Dim eColIndex As Integer = 0
    Dim accntDR As String = ""
    Dim accntInputVAT As String = ""
    Public Overloads Function ShowDialog(ByVal DocNumber As String) As Boolean
        TransID = DocNumber
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmBooking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Text = "(" & database & ") - Booking Management "
            TransAuto = GetTransSetup(ModuleID)
            LoadLocation()
            GetAPAccount()

            If TransID <> "" Then
                If Not AllowAccess("BM_VIEW") Then
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
                    EnableControl(False)
                Else
                    LoadBM(TransID)
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
                EnableControl(False)
            End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        dtpDocDate.Enabled = Value
        txtVCEName.Enabled = Value
        txtClientName.Enabled = Value
        cbLocation.Enabled = Value
        txtRemarks.Enabled = Value
        btnSearchVCE.Enabled = Value
        Button1.Enabled = Value

        dgvList.AllowUserToAddRows = Value
        dgvList.AllowUserToDeleteRows = Value
        If Value = True Then
            dgvList.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvList.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Private Sub btnSearchVCE_Click(sender As Object, e As EventArgs) Handles btnSearchVCE.Click
        Dim f As New frmVCE_Search
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        txtVCEName.Text = f.VCEName
        f.Dispose()
    End Sub

    Private Sub txtVCEName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtVCEName.KeyDown
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

    Private Sub tsbNew_Click(sender As Object, e As EventArgs) Handles tsbNew.Click
        If Not AllowAccess("BM_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            BMNo = ""
            JETransiD = 0
            LoadLocation()
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
            txtStatus.Text = "Open"
            EnableControl(True)

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
            txtVCEName.Select()
        End If
    End Sub

    Private Sub ClearText()
        txtVCECode.Text = ""
        txtClientName.Text = ""
        txtVCEName.Text = ""
        txtRemarks.Text = ""
        txtTransNum.Text = ""
        txtStatus.Text = ""
        dgvEntry.Rows.Clear()

        dgvList.Rows.Clear()
        dtpDocDate.Value = Date.Today.Date
    End Sub

    Private Sub tsbClose_Click(sender As Object, e As EventArgs) Handles tsbClose.Click
        ' Toolstrip Buttons
        If BMNo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadBM(TransID)
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

    Private Sub LoadLocation()
        Dim query As String
        cbLocation.Items.Clear()
        query = " SELECT DISTINCT Location FROM tblBM_Tariff WHERE Status ='Active' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            cbLocation.Items.Add(SQL.SQLDR("Location").ToString)
        End While
        If cbLocation.Items.Count > 0 Then
            cbLocation.SelectedIndex = 0
        End If
    End Sub

    Private Sub cbLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLocation.SelectedIndexChanged
        If disableEvent = False Then
            If cbLocation.SelectedIndex <> -1 Then
                For Each item As DataGridViewRow In dgvList.Rows

                Next

            End If
        End If

    End Sub

    Private Sub TruckingRatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TruckingRatesToolStripMenuItem.Click
        frmLogistics_Tariff.Show()
    End Sub

    Private Sub dgvList_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellEndEdit
        If e.RowIndex <> -1 Then
            If e.ColumnIndex = dgcDate.Index Then
                If IsDate(dgvList.Item(e.ColumnIndex, e.RowIndex).Value) Then
                    dgvList.Item(e.ColumnIndex, e.RowIndex).Value = CDate(dgvList.Item(e.ColumnIndex, e.RowIndex).Value).ToString("MM/dd/yyyy")
                End If

            ElseIf e.ColumnIndex = dgcDestination.Index Then

                Dim f As New frmData_Search
                Dim param As String = ""

                If Not IsNothing(dgvList.Item(e.ColumnIndex, e.RowIndex).Value) Then param = dgvList.Item(e.ColumnIndex, e.RowIndex).Value
                f.ShowDialog("BM_Destination", cbLocation.SelectedItem, param)
                dgvList.Item(e.ColumnIndex, e.RowIndex).Value = f.data
                f.Dispose()
                LoadVehicle(cbLocation.SelectedItem, dgvList.Item(e.ColumnIndex, e.RowIndex).Value, e.RowIndex)
                dgvList.Item(dgcRate.Index, e.RowIndex).Value = GetRate(cbLocation.SelectedItem, dgvList.Item(e.ColumnIndex, e.RowIndex).Value, dgvList.Item(dgcVehicleType.Index, e.RowIndex).Value, e.RowIndex)
            ElseIf e.ColumnIndex = dgcRefNo.Index Then
                Dim Ref As String = ""

                If Not IsNothing(dgvList.Item(e.ColumnIndex, e.RowIndex).Value) Then Ref = dgvList.Item(e.ColumnIndex, e.RowIndex).Value
                If Ref <> "" Then
                    Dim ID As String = ""
                    ID = RefExist(txtVCECode.Text, Ref)
                    If ID <> "" Then
                        MsgBox("Reference no. already exist!, Please check booking No. " & ID, MsgBoxStyle.Exclamation)
                        dgvList.Item(e.ColumnIndex, e.RowIndex).Value = ""
                    End If
                    If isCompleted() Then
                        GenerateEntryBM()
                    End If
                End If
            ElseIf e.ColumnIndex = dgcRate.Index Then
                If dgvList.Item(e.ColumnIndex, e.RowIndex).Value = "" OrElse IsNothing(dgvList.Item(e.ColumnIndex, e.RowIndex).Value) Then
                    dgvList.Item(e.ColumnIndex, e.RowIndex).Value = 0
                ElseIf IsNumeric(dgvList.Item(dgcRate.Index, e.RowIndex).Value) Then
                    dgvList.Item(dgcRate.Index, e.RowIndex).Value = CDec(dgvList.Item(dgcRate.Index, e.RowIndex).Value).ToString("N2")
                End If
                Recompute(e.RowIndex, e.ColumnIndex)
                ComputeTotal()
            ElseIf e.ColumnIndex = dgcAdditional.Index Then
                If dgvList.Item(e.ColumnIndex, e.RowIndex).Value = "" OrElse IsNothing(dgvList.Item(e.ColumnIndex, e.RowIndex).Value) Then
                    dgvList.Item(e.ColumnIndex, e.RowIndex).Value = 0
                ElseIf IsNumeric(dgvList.Item(dgcAdditional.Index, e.RowIndex).Value) Then
                    dgvList.Item(dgcAdditional.Index, e.RowIndex).Value = CDec(dgvList.Item(dgcAdditional.Index, e.RowIndex).Value).ToString("N2")
                End If
                Recompute(e.RowIndex, e.ColumnIndex)
                ComputeTotal()
            End If
        End If
    End Sub

    Private Function isCompleted() As Boolean
        Dim Completed As Boolean = True
        For Each item As DataGridViewRow In dgvList.Rows
            If item.Cells(dgcDestination.Index).Value <> "" Then
                If item.Cells(dgcRefNo.Index).Value = "" Then
                    Completed = False
                    Exit For
                End If
            End If
        Next
        Return Completed
    End Function
    Private Function RefExist(VCECode As String, Ref As String) As String
        Dim query As String
        query = " SELECT BM_No  " &
                    " FROM       tblBM " &
                    " INNER JOIN tblBM_Details " &
                    " ON         tblBM.TransID = tblBM_Details.TransID " &
                    " WHERE      tblBM.VCECode= @VCECode And ReferenceNo = @ReferenceNo " &
                    " AND        tblBM.Status ='Active' AND tblBM.TransID <>  @TransID "
        SQL.FlushParams()
        SQL.AddParam("@VCECode", VCECode)
        SQL.AddParam("@ReferenceNo", Ref)
        SQL.AddParam("@TransID", TransID)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("BM_No").ToString
        Else
            Return ""
        End If
    End Function
    Private Sub LoadVehicle(Source As String, Destination As String, ByVal SelectedIndex As Integer)
        Try
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvList.Item(dgcVehicleType.Index, SelectedIndex)
            dgvCB.Items.Clear()
            ' ADD ALL UOM
            Dim query As String
            Dim temp As String = ""

            query = " SELECT DISTINCT VehicleType FROM tblBM_Tariff " &
                    " WHERE Location = @Location And Destination = @Destination "
            SQL.FlushParams()
            SQL.AddParam("@Location", Source)
            SQL.AddParam("@Destination", Destination)
            SQL.ReadQuery(query)
            dgvCB.Items.Clear()
            While SQL.SQLDR.Read
                dgvCB.Items.Add(SQL.SQLDR("VehicleType").ToString)
                temp = SQL.SQLDR("VehicleType").ToString
            End While
            If dgvCB.Items.Count = 1 Then
                dgvCB.Value = temp
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub tsbSearch_Click(sender As Object, e As EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("BM_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("BM")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadBM(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbEdit_Click(sender As Object, e As EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("BM_EDIT") Then
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
            tsbOption.Enabled = True
        End If
    End Sub

    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        If validateDGV() Then
            If txtVCECode.Text = "" Then
                Msg("Please enter VCECode!", MsgBoxStyle.Exclamation)
            ElseIf TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Gr8Books Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID(ColumnID, DBTable)
                    BMNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    txtTransNum.Text = BMNo
                    SaveBM()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    BMNo = txtTransNum.Text
                    LoadBM(TransID)
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Gr8Books Message Alert") = MsgBoxResult.Yes Then
                    UpdateBM()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    BMNo = txtTransNum.Text
                    LoadBM(TransID)
                End If
            End If
        End If
    End Sub
    Private Function validateDGV() As Boolean
        Dim Customer, Destination, Vehicle, Plate, Rate As String
        Dim value As Boolean = True
        For Each row As DataGridViewRow In dgvList.Rows
            If Not row.Cells(dgcCustomer.Index).Value Is Nothing OrElse Not row.Cells(dgcDestination.Index).Value Is Nothing Then
                If IsNothing(row.Cells(dgcCustomer.Index).Value) Then Customer = "" Else Customer = row.Cells(dgcCustomer.Index).Value
                If IsNothing(row.Cells(dgcDestination.Index).Value) Then Destination = "" Else Destination = row.Cells(dgcDestination.Index).Value
                If IsNothing(row.Cells(dgcVehicleType.Index).Value) Then Vehicle = "" Else Vehicle = row.Cells(dgcVehicleType.Index).Value
                If IsNothing(row.Cells(dgcPlate.Index).Value) Then Plate = "" Else Plate = row.Cells(dgcPlate.Index).Value
                If IsNothing(row.Cells(dgcRate.Index).Value) Then Rate = "" Else Rate = row.Cells(dgcRate.Index).Value

                If Customer = "" Then
                    Msg("There are line item without customer, please check.", MsgBoxStyle.Exclamation)
                    value = False
                    Exit For
                ElseIf Destination = "" Then
                    Msg("There are line item without destination, please check.", MsgBoxStyle.Exclamation)
                    value = False
                    Exit For
                ElseIf Vehicle = "" Then
                    Msg("There are line item without vehicle type, please check.", MsgBoxStyle.Exclamation)
                    value = False
                    Exit For

                ElseIf Plate = "" Then
                    Msg("There are line item without plate number, please check.", MsgBoxStyle.Exclamation)
                    value = False
                    Exit For

                ElseIf Rate = "" Then
                    Msg("There are line item without rate, please check.", MsgBoxStyle.Exclamation)
                    value = False
                    Exit For
                End If
            End If
        Next
        Return value

    End Function

    Private Sub SaveBM()
        Dim SQL As New SQLControl
        Try
            SQL.BeginTransaction()
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " &
                        " tblBM(TransID, BM_No, BranchCode, BusinessCode, DateBM, VCECode, ClientCode, Location, TotalAmount,  " &
                        "       Remarks, Status,  WhoCreated, DateCreated, TransAuto) " &
                        " VALUES (@TransID, @BM_No, @BranchCode, @BusinessCode, @DateBM, @VCECode, @ClientCode, @Location, @TotalAmount,  " &
                        "           @Remarks, @Status, @WhoCreated, GETDATE(), @TransAuto) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@BM_No", BMNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateBM", dtpDocDate.Value.Date)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@ClientCode", txtClientCode.Text)
            SQL.AddParam("@Location", cbLocation.SelectedItem)
            SQL.AddParam("@TotalAmount", CDec(txtTotalAmount.Text))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.AddParam("@TransAuto", TransAuto)
            SQL.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            Dim DeliveryDate As Date
            Dim Customer, Destination, VehicleType, PlateNo, ReferenceNo As String
            Dim Rate, Additional, VATAmt, Total As Decimal
            Dim VATable, VATInc As Boolean
            For Each row As DataGridViewRow In dgvList.Rows
                If Not row.Cells(dgcCustomer.Index).Value Is Nothing AndAlso Not row.Cells(dgcDestination.Index).Value Is Nothing Then
                    If IsDate(row.Cells(dgcDate.Index).Value) Then
                        DeliveryDate = CDate(row.Cells(dgcDate.Index).Value)
                    Else
                        DeliveryDate = CDate("01/01/1900")
                    End If
                    Customer = IIf(row.Cells(dgcCustomer.Index).Value = Nothing, "", row.Cells(dgcCustomer.Index).Value)
                    Destination = IIf(row.Cells(dgcDestination.Index).Value = Nothing, "", row.Cells(dgcDestination.Index).Value)
                    VehicleType = IIf(row.Cells(dgcVehicleType.Index).Value = Nothing, "", row.Cells(dgcVehicleType.Index).Value)
                    PlateNo = IIf(row.Cells(dgcPlate.Index).Value = Nothing, "", row.Cells(dgcPlate.Index).Value)
                    ReferenceNo = IIf(row.Cells(dgcRefNo.Index).Value = Nothing, "", row.Cells(dgcRefNo.Index).Value)

                    If IsNumeric(row.Cells(dgcRate.Index).Value) Then Rate = CDec(row.Cells(dgcRate.Index).Value) Else Rate = 0
                    If IsNumeric(row.Cells(dgcAdditional.Index).Value) Then Additional = CDec(row.Cells(dgcAdditional.Index).Value) Else Additional = 0
                    If IsNumeric(row.Cells(dgcVATAmount.Index).Value) Then VATAmt = CDec(row.Cells(dgcVATAmount.Index).Value) Else VATAmt = 0
                    If IsNumeric(row.Cells(dgcTotal.Index).Value) Then Total = CDec(row.Cells(dgcTotal.Index).Value) Else Total = 0
                    VATable = IIf(row.Cells(dgcVAT.Index).Value = Nothing, False, row.Cells(dgcVAT.Index).Value)
                    VATInc = IIf(row.Cells(dgcVATinc.Index).Value = Nothing, False, row.Cells(dgcVATinc.Index).Value)

                    insertSQL = " INSERT INTO " &
                         " tblBM_Details(TransId, DeliveryDate, ReferenceNo, Customer, Destination, VehicleType, PlateNo, " &
                         "              Rate, Additional, VATAmount, TotalAmount, VATable, VATInc, LineNum, WhoCreated, DateCreated) " &
                         " VALUES(@TransId, @DeliveryDate, @ReferenceNo, @Customer, @Destination, @VehicleType, @PlateNo, " &
                         "       @Rate, @Additional, @VATAmount, @TotalAmount, @VATable, @VATInc, @LineNum, @WhoCreated, GETDATE()) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@DeliveryDate", DeliveryDate)
                    SQL.AddParam("@ReferenceNo", ReferenceNo)
                    SQL.AddParam("@Customer", Customer)
                    SQL.AddParam("@Destination", Destination)
                    SQL.AddParam("@VehicleType", VehicleType)
                    SQL.AddParam("@PlateNo", PlateNo)
                    SQL.AddParam("@Rate", Rate)
                    SQL.AddParam("@Additional", Additional)
                    SQL.AddParam("@VATAmount", VATAmt)
                    SQL.AddParam("@TotalAmount", Total)
                    SQL.AddParam("@VATable", VATable)
                    SQL.AddParam("@VATInc", VATInc)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
            If dgvEntry.Rows.Count > 0 Then
                SaveEntries(SQL)
            End If
            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "BM_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub
    Private Sub UpdateBM()
        Dim SQL As New SQLControl
        Try
            SQL.BeginTransaction()
            activityStatus = True
            Dim insertSQL, updateSQL, deleteSQL As String
            updateSQL = " UPDATE tblBM " &
                        " SET    BM_No = @BM_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, DateBM = @DateBM, " &
                        "        VCECode = @VCECode, ClientCode = @ClientCode, Location = @Location, TotalAmount = @TotalAmount, Remarks = @Remarks, " &
                        "        Status = @Status,  WhoModified = @WhoModified, DateModified = GETDATE() " &
                        " WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@BM_No", BMNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateBM", dtpDocDate.Value.Date)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@ClientCode", txtClientCode.Text)
            SQL.AddParam("@Location", cbLocation.SelectedItem)
            SQL.AddParam("@TotalAmount", CDec(txtTotalAmount.Text))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)

            deleteSQL = " DELETE FROM tblBM_Details WHERE TransID =@TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.ExecNonQuery(deleteSQL)


            Dim line As Integer = 1
            Dim DeliveryDate As Date
            Dim Customer, Destination, VehicleType, PlateNo, ReferenceNo As String
            Dim Rate, Additional, VATAmt, Total As Decimal
            Dim VATable, VATInc As Boolean
            For Each row As DataGridViewRow In dgvList.Rows
                If Not row.Cells(dgcCustomer.Index).Value Is Nothing AndAlso Not row.Cells(dgcDestination.Index).Value Is Nothing Then
                    If IsDate(row.Cells(dgcDate.Index).Value) Then
                        DeliveryDate = CDate(row.Cells(dgcDate.Index).Value)
                    Else
                        DeliveryDate = CDate("01/01/1900")
                    End If
                    Customer = IIf(row.Cells(dgcCustomer.Index).Value = Nothing, "", row.Cells(dgcCustomer.Index).Value)
                    Destination = IIf(row.Cells(dgcDestination.Index).Value = Nothing, "", row.Cells(dgcDestination.Index).Value)
                    VehicleType = IIf(row.Cells(dgcVehicleType.Index).Value = Nothing, "", row.Cells(dgcVehicleType.Index).Value)
                    PlateNo = IIf(row.Cells(dgcPlate.Index).Value = Nothing, "", row.Cells(dgcPlate.Index).Value)
                    ReferenceNo = IIf(row.Cells(dgcRefNo.Index).Value = Nothing, "", row.Cells(dgcRefNo.Index).Value)

                    If IsNumeric(row.Cells(dgcRate.Index).Value) Then Rate = CDec(row.Cells(dgcRate.Index).Value) Else Rate = 0
                    If IsNumeric(row.Cells(dgcAdditional.Index).Value) Then Additional = CDec(row.Cells(dgcAdditional.Index).Value) Else Additional = 0
                    If IsNumeric(row.Cells(dgcVATAmount.Index).Value) Then VATAmt = CDec(row.Cells(dgcVATAmount.Index).Value) Else VATAmt = 0
                    If IsNumeric(row.Cells(dgcTotal.Index).Value) Then Total = CDec(row.Cells(dgcTotal.Index).Value) Else Total = 0
                    VATable = IIf(row.Cells(dgcVAT.Index).Value = Nothing, False, row.Cells(dgcVAT.Index).Value)
                    VATInc = IIf(row.Cells(dgcVATinc.Index).Value = Nothing, False, row.Cells(dgcVATinc.Index).Value)

                    insertSQL = " INSERT INTO " &
                         " tblBM_Details(TransId, DeliveryDate, ReferenceNo, Customer, Destination, VehicleType, PlateNo, " &
                         "              Rate, Additional, VATAmount, TotalAmount, VATable, VATInc, LineNum, WhoCreated, DateCreated) " &
                         " VALUES(@TransId, @DeliveryDate, @ReferenceNo, @Customer, @Destination, @VehicleType, @PlateNo, " &
                         "       @Rate, @Additional, @VATAmount, @TotalAmount, @VATable, @VATInc, @LineNum, @WhoCreated, GETDATE()) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@DeliveryDate", DeliveryDate)
                    SQL.AddParam("@ReferenceNo", ReferenceNo)
                    SQL.AddParam("@Customer", Customer)
                    SQL.AddParam("@Destination", Destination)
                    SQL.AddParam("@VehicleType", VehicleType)
                    SQL.AddParam("@PlateNo", PlateNo)
                    SQL.AddParam("@Rate", Rate)
                    SQL.AddParam("@Additional", Additional)
                    SQL.AddParam("@VATAmount", VATAmt)
                    SQL.AddParam("@TotalAmount", Total)
                    SQL.AddParam("@VATable", VATable)
                    SQL.AddParam("@VATInc", VATInc)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
            If dgvEntry.Rows.Count > 0 Then
                SaveEntries(SQL)
            End If
            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "BM_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub
    Private Function GetRate(Source As String, Destination As String, Type As String, ByVal SelectedIndex As Integer) As Decimal
        Dim query As String
        If Destination IsNot Nothing Then
            query = " SELECT DISTINCT ISNULL(SupplierRate,0) AS SupplierRate FROM tblBM_Tariff " &
               " WHERE Location = @Location And Destination = @Destination And VehicleType = @VehicleType "
            SQL.FlushParams()
            SQL.AddParam("@Location", Source)
            SQL.AddParam("@Destination", Destination)
            SQL.AddParam("@VehicleType", Type)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read() Then
                Return SQL.SQLDR("SupplierRate")
            Else
                Return 0
            End If
        Else
            Return 0
        End If
    End Function

    Private Sub LoadBM(ByVal ID As String)
        Dim query As String
        query = " SELECT   tblBM.TransID, tblBM.BM_No, VCECode, ClientCode, DateBM, Location, ISNULL(TotalAmount,0) AS TotalAmount, Remarks, viewBM_Status.Status " &
                " FROM     tblBM INNER JOIN viewBM_Status" &
                " ON       tblBM.TransID = viewBM_Status.TransID" &
                " WHERE    tblBM.TransId = '" & ID & "' " &
                " ORDER BY TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            BMNo = SQL.SQLDR("BM_No").ToString
            txtTransNum.Text = BMNo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtClientCode.Text = SQL.SQLDR("ClientCode").ToString
            dtpDocDate.Text = SQL.SQLDR("DateBM").ToString
            cbLocation.SelectedItem = SQL.SQLDR("Location").ToString
            txtTotalAmount.Text = CDec(SQL.SQLDR("TotalAmount")).ToString("N2")
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            txtClientName.Text = GetVCEName(txtClientCode.Text)


            LoadBMDetails(TransID)
            LoadAccountingEntry(TransID)

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
    Private Sub LoadAccountingEntry(ByVal TransID As Integer)
        Try
            Dim query As String
            query = " SELECT ID, JE_No, View_GL.BranchCode, View_GL.AccntCode, AccntTitle, View_GL.VCECode, View_GL.VCEName, Particulars, " &
                    "        ISNULL(Debit,0) AS Debit, ISNULL(Credit,0) AS Credit, RefNo, CostCenter, SL_Code, CIP_Code , VATType, ProfitCenter, ATC_Code " &
                    " FROM   View_GL  " &
                    " WHERE  RefType ='BM' AND RefTransID ='" & TransID & "' AND isUpload <> 1" &
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
                    dgvEntry.Rows(rowsCount).Cells(dgcRef.Index).Value = SQL.SQLDR("RefNo").ToString
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
    Private Sub LoadBMDetails(ByVal TransID As String)
        Dim ctr As Integer = 0
        Dim DelDate As Date
        Dim query As String
        query = " SELECT	ISNULL(DeliveryDate,'01/01/1900') AS DeliveryDate, ReferenceNo, Customer, Destination, VehicleType, PlateNo, " &
                "           ISNULL(Rate,0) AS Rate, ISNULL(Additional,0) AS Additional, ISNULL(VATAmount,0) AS VATAmount, ISNULL(TotalAmount,0) AS TotalAmount,  " &
                "           ISNULL(VATable,0) AS VATable, ISNULL(VATInc,0) AS VATInc  " &
                " FROM	    tblBM_Details " &
                " WHERE     TransId = " & TransID & " " &
                " ORDER BY  LineNum "
        disableEvent = True
        dgvList.Rows.Clear()
        disableEvent = False
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                DelDate = row(0)
                dgvList.Rows.Add(IIf(DelDate = "01/01/1900", "", DelDate), row(1).ToString, row(2).ToString, row(3).ToString, row(4).ToString, row(5).ToString,
                                  CDec(row(6)).ToString("N2"), CDec(row(7)).ToString("N2"), CDec(row(8)).ToString("N2"), CDec(row(9)).ToString("N2"),
                                  row(10), row(11))
                LoadVehicle(cbLocation.SelectedItem, row(3).ToString, ctr)
                ctr += 1
            Next
            ComputeTotal()
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As Object, e As EventArgs) Handles tsbPrevious.Click
        If BMNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblBM  " &
                    "   INNER JOIN tblBranch  ON	          " &
                    "   tblBM.BranchCode = tblBranch.BranchCode    " &
                    "     WHERE          " &
                    " 	( tblBM.BranchCode IN  " &
                    " 	  ( " &
                    "       SELECT    DISTINCT tblBranch.BranchCode  " &
                    " 	    FROM      tblBranch   " &
                    " 	    INNER JOIN tblUser_Access    ON          " &
                    " 	    tblBranch.BranchCode = tblUser_Access.Code   " &
                    " 	    AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " &
                    " 	    AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " &
                    " 	    WHERE     UserID ='" & UserID & "' " &
                    " 	   ) " &
                    "    )   " &
                    " AND BM_No < '" & BMNo & "' ORDER BY BM_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadBM(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As Object, e As EventArgs) Handles tsbNext.Click
        If BMNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblBM  " &
                    "   INNER JOIN tblBranch  ON	          " &
                    "   tblBM.BranchCode = tblBranch.BranchCode    " &
                    "     WHERE          " &
                    " 	( tblBM.BranchCode IN  " &
                    " 	  ( " &
                    "       SELECT    DISTINCT tblBranch.BranchCode  " &
                    " 	    FROM      tblBranch   " &
                    " 	    INNER JOIN tblUser_Access    ON          " &
                    " 	    tblBranch.BranchCode = tblUser_Access.Code   " &
                    " 	    AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " &
                    " 	    AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " &
                    " 	    WHERE     UserID ='" & UserID & "' " &
                    " 	   ) " &
                    "    )   " &
                    " AND BM_No > '" & BMNo & "' ORDER BY BM_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadBM(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbCancel_Click(sender As Object, e As EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("BM_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If txtStatus.Text <> "Cancelled" AndAlso MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim updateSQL As String
                        updateSQL = " UPDATE  tblBM SET Status ='Cancelled' WHERE TransID = @TransID "
                        SQL.FlushParams()
                        BMNo = txtTransNum.Text
                        SQL.AddParam("@TransID", TransID)
                        SQL.ExecNonQuery(updateSQL)

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

                        BMNo = txtTransNum.Text
                        LoadBM(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "BM_No", BMNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub dgvList_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvList.DataError
        Try

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub frmBooking_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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
            ElseIf e.KeyCode = Keys.R Then
                If tsbReports.Enabled = True Then tsbReports.ShowDropDown()
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

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbPrint_Click(sender As Object, e As EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("BM", TransID)
        f.Dispose()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim f As New frmVCE_Search
        f.ShowDialog()
        txtClientCode.Text = f.VCECode
        txtClientName.Text = f.VCEName
        f.Dispose()
    End Sub

    Private Sub UnpaidBookingsToSupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnpaidBookingsToSupplierToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("BM_Unpaid", "", "Summary")
        f.Dispose()
    End Sub

    Private Sub UnbilledBookingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnbilledBookingsToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("BM_Unbilled", "", "Summary")
        f.Dispose()
    End Sub

    Private Sub CVListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CVListToolStripMenuItem.Click
        Dim f As New frmReport_Filter
        f.Report = "BM List"
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub TripSummaryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TripSummaryToolStripMenuItem.Click
        Dim f As New frmReport_Filter
        f.Report = "BM Trip Summary"
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub ComputeTotal()
        Dim a As Decimal = 0
        Dim b As Decimal = 0
        ' COMPUTE TOTAL
        For i As Integer = 0 To dgvList.Rows.Count - 1
            If IsNumeric(dgvList.Item(dgcTotal.Index, i).Value) Then
                a = a + CDec(dgvList.Item(dgcTotal.Index, i).Value)
            End If
        Next
        txtTotalAmount.Text = a.ToString("N2")


        ' COMPUTE VAT
        For i As Integer = 0 To dgvList.Rows.Count - 1
            If IsNumeric(dgvList.Item(dgcVATAmount.Index, i).Value) Then
                b = b + Double.Parse(dgvList.Item(dgcVATAmount.Index, i).Value).ToString
            End If
        Next
        txtVATAmount.Text = b.ToString("N2")
    End Sub

    Private Sub dgvList_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvList.CurrentCellDirtyStateChanged
        If dgvList.SelectedCells.Count > 0 AndAlso (dgvList.SelectedCells(0).ColumnIndex = dgcVAT.Index OrElse dgvList.SelectedCells(0).ColumnIndex = dgcVATinc.Index) Then
            If dgvList.SelectedCells(0).RowIndex <> -1 Then
                Recompute(dgvList.SelectedCells(0).RowIndex, dgvList.SelectedCells(0).ColumnIndex)
                dgvList.SelectedCells(0).Selected = False
                dgvList.EndEdit()
            End If
        End If
    End Sub

    Private Sub Recompute(ByVal RowID As Integer, ByVal ColID As Integer)
        Dim rate As Decimal = 0
        Dim addl As Decimal = 0
        Dim total, VAT, net, baseVAT As Decimal
        If RowID <> -1 Then
            ' GET GROSS AMOUNT (VAT INCLUSIVE)
            If IsNumeric(dgvList.Item(dgcRate.Index, RowID).Value) Then rate = CDec(dgvList.Item(dgcRate.Index, RowID).Value)
            If IsNumeric(dgvList.Item(dgcAdditional.Index, RowID).Value) Then addl = CDec(dgvList.Item(dgcAdditional.Index, RowID).Value)
            total = rate + addl
            baseVAT = total
            ' COMPUTE VAT IF VATABLE
            If ColID = dgcVAT.Index Then
                If dgvList.Item(dgcVAT.Index, RowID).Value = True Then
                    dgvList.Item(dgcVAT.Index, RowID).Value = False

                    dgvList.Item(dgcVATinc.Index, RowID).Value = False
                    VAT = 0
                    dgvList.Item(dgcVATinc.Index, RowID).ReadOnly = True
                Else
                    dgvList.Item(dgcVAT.Index, RowID).Value = True

                    dgvList.Item(dgcVATinc.Index, RowID).ReadOnly = False
                    If dgvList.Item(dgcVATinc.Index, RowID).Value = False Then
                        VAT = CDec(total * 0.12).ToString("N2")
                    Else
                        baseVAT = (total / 1.12)
                        VAT = CDec(baseVAT * 0.12).ToString("N2")
                    End If
                End If
            ElseIf ColID = dgcVATinc.Index Then
                If dgvList.Item(dgcVAT.Index, RowID).Value = False Then
                    VAT = 0
                Else
                    If dgvList.Item(dgcVATinc.Index, RowID).Value = True Then
                        dgvList.Item(dgcVATinc.Index, RowID).Value = False
                        VAT = CDec(baseVAT * 0.12).ToString("N2")
                    Else
                        dgvList.Item(dgcVATinc.Index, RowID).Value = True
                        baseVAT = (total / 1.12)
                        VAT = CDec(baseVAT * 0.12).ToString("N2")
                    End If

                End If
            Else
                If dgvList.Item(dgcVAT.Index, RowID).Value = False Then
                    baseVAT = total
                    VAT = 0
                    dgvList.Item(dgcVATinc.Index, RowID).ReadOnly = True
                Else
                    dgvList.Item(dgcVATinc.Index, RowID).ReadOnly = False
                    If dgvList.Item(dgcVATinc.Index, RowID).Value = True Then ' IF VAT INCLUSIVE, BASE AMOUNT WILL BE GROSS / 1.12
                        baseVAT = (total / 1.12)
                    End If
                    VAT = CDec(baseVAT * 0.12).ToString("N2")
                End If
            End If

            net = baseVAT + VAT
            dgvList.Item(dgcVATAmount.Index, RowID).Value = Format(VAT, "#,###,###,###.00").ToString()
            dgvList.Item(dgcTotal.Index, RowID).Value = Format(net, "#,###,###,###.00").ToString()
            ComputeTotal()

        End If

    End Sub

    Private Sub GenerateEntryBM()
        Dim query As String
        dgvEntry.Rows.Clear()
        Dim baseAmt As Decimal = 0
        Dim VATamount As Decimal = 0
        Dim Total As Decimal = 0
        Dim BM_COS As String = ""
        Dim Ref As String = ""
        If AccntAP = "" Then
            MsgBox("Please maintain default AP Account to generate accounting entry for this transaction", MsgBoxStyle.Exclamation)
        Else
            If IsNumeric(txtTotalAmount.Text) Then Total = CDec(txtTotalAmount.Text)
            If IsNumeric(txtVATAmount.Text) Then VATamount = CDec(txtVATAmount.Text)
            baseAmt = Total - VATamount

            query = " SELECT TAX_IV, BM_COS FROM tblSystemSetup  "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                accntDR = SQL.SQLDR("BM_COS").ToString
                accntInputVAT = SQL.SQLDR("TAX_IV").ToString
            End If

            dgvEntry.Rows.Clear()
            dgvEntry.Rows.Add("")
            dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcAccntCode.Index).Value = accntDR
            dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(accntDR)
            dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcDebit.Index).Value = (baseAmt).ToString("N2")
            dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcCredit.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcVCECode.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcVCEName.Index).Value = ""
            dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcRef.Index).Value = "BM:" & txtTransNum.Text
            dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(chVATType.Index).Value = ""


            If VATamount <> 0 Then
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcAccntCode.Index).Value = accntInputVAT
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(accntInputVAT)
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcDebit.Index).Value = CDec(VATamount).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcCredit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcVCECode.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcVCEName.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcRef.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(chVATType.Index).Value = ""

            End If

            If Total <> 0 Then
                dgvEntry.Rows.Add("")
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcAccntCode.Index).Value = AccntAP
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcAccntTitle.Index).Value = GetAccntTitle(AccntAP)
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcDebit.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcCredit.Index).Value = CDec(Total).ToString("N2")
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcVCECode.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcVCEName.Index).Value = ""
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(dgcRef.Index).Value = "BM:" & txtTransNum.Text
                dgvEntry.Rows(dgvEntry.Rows.Count - 1).Cells(chVATType.Index).Value = ""
            End If

            TotalDBCR()

        End If

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
    Dim AccntAP As String = ""
    Private Sub GetAPAccount()
        Dim query As String
        query = " SELECT    BM_AP " &
                " FROM      tblSystemSetup "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            AccntAP = SQL.SQLDR("BM_AP").ToString
        End While
    End Sub
    Dim JETransiD As Integer = 0

    Private Sub SaveEntries(SQL1 As SQLControl)
        Dim insertSQL, updateSQL, deleteSQL As String
        JETransiD = LoadJE("BM", TransID)
        If JETransiD = 0 Then
            JETransiD = GenerateTransID("JE_No", "tblJE_Header")

            insertSQL = " INSERT INTO " &
                            " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Currency, Exchange_Rate, Remarks, WhoCreated) " &
                            " VALUES(@JE_No,@AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Currency, @Exchange_Rate, @Remarks, @WhoCreated)"
            SQL1.FlushParams()
            SQL1.AddParam("@JE_No", JETransiD)
            SQL1.AddParam("@AppDate", dtpDocDate.Value.Date)
            SQL1.AddParam("@RefType", "BM")
            SQL1.AddParam("@RefTransID", TransID)
            SQL1.AddParam("@Book", "Accounts Payable")
            SQL1.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
            SQL1.AddParam("@Currency", "PHP")
            SQL1.AddParam("@Exchange_Rate", "0.0000")
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.ExecNonQuery(insertSQL)
        Else
            updateSQL = " UPDATE tblJE_Header " &
                            " SET    AppDate = @AppDate, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " &
                            "        RefType = @RefType, RefTransID = @RefTransID, Book = @Book, TotalDBCR = @TotalDBCR, Currency = @Currency, Exchange_Rate = @Exchange_Rate, " &
                            "        Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE() " &
                            " WHERE  JE_No = @JE_No "
            SQL1.FlushParams()
            SQL1.AddParam("@JE_No", JETransiD)
            SQL1.AddParam("@AppDate", dtpDocDate.Value.Date)
            SQL1.AddParam("@RefType", "BM")
            SQL1.AddParam("@RefTransID", TransID)
            SQL1.AddParam("@Book", "Accounts Payable")
            SQL1.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
            SQL1.AddParam("@Currency", "PHP")
            SQL1.AddParam("@Exchange_Rate", "0.0000")
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@WhoModified", UserID)
            SQL1.ExecNonQuery(updateSQL)
        End If

        ' DELETE ACCOUNTING ENTRIES
        deleteSQL = " DELETE FROM tblJE_Details  WHERE  JE_No = @JE_No "
        SQL.FlushParams()
        SQL.AddParam("@JE_No", JETransiD)
        SQL.ExecNonQuery(deleteSQL)


        Dim line As Integer = 1
        For Each item As DataGridViewRow In dgvEntry.Rows
            If item.Cells(dgcAccntCode.Index).Value <> Nothing Then
                insertSQL = " INSERT INTO " &
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, CIP_Code, CostCenter, VATType, ProfitCenter, ATC_Code) " &
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
                If item.Cells(dgcCC.Index).Value <> Nothing AndAlso item.Cells(dgcCC.Index).Value <> "" Then
                    SQL1.AddParam("@CostCenter", item.Cells(dgcCC.Index).Value.ToString)
                Else
                    SQL1.AddParam("@CostCenter", "")
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
                If item.Cells(dgcRef.Index).Value <> Nothing AndAlso item.Cells(dgcRef.Index).Value <> "" Then
                    SQL1.AddParam("@RefNo", item.Cells(dgcRef.Index).Value.ToString)
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

                line += 1
            End If
        Next
    End Sub
End Class