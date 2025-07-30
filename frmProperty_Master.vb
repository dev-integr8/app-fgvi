Public Class frmProperty_Master
    Dim PropCode As String = ""
    Dim moduleID As String = "REP"
    Dim picPath As String
    Dim disableEvent As Boolean = False

    Public Overloads Function ShowDialog(ByVal Prop As String) As Boolean
        PropCode = Prop
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmLeaseProperty_Master_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCostCenter()

        If PropCode <> "" Then

            LoadProperty(PropCode)
        Else
            tsbSearch.Enabled = True
            tsbNew.Enabled = True
            tsbEdit.Enabled = False
            tsbSave.Enabled = False
            tsbInactive.Enabled = False
            tsbClose.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = True
            EnableControl(False)
        End If

    End Sub
    Private Sub LoadCostCenter()

        'Dim selectSQL As String = " SELECT Code + ' - ' + Description AS Description  FROM tblCC WHERE Status = 'Active'"
        Dim selectSQL As String = " SELECT Code AS Description  FROM tblCC WHERE Status = 'Active'"
        SQL.ReadQuery(selectSQL, 2)
        cbCostCenter.Items.Clear()
        While SQL.SQLDR2.Read
            cbCostCenter.Items.Add(SQL.SQLDR2("Description").ToString)
        End While

    End Sub

    Private Sub LoadProperty(ByVal Code As String)

        Dim query As String
        query = " SELECT     tblSaleProperty.UnitCode, UnitType, Model, Unit_No, Unit_Bldg, Unit_Lot, Unit_Blk, Unit_Phase, Project, Address_Brgy, Address_City, Address_Province, " &
                "            Remarks, FloorArea, LotArea, ISNULL(FloorPrice,0) AS FloorPrice, ISNULL(LotPrice,0) AS LotPrice, " &
                "            ISNULL(TotalFloorPrice,0) AS TotalFloorPrice, ISNULL(TotalLotPrice,0) AS TotalLotPrice, " &
                "            ISNULL(MiscRate,0) AS MiscRate, ISNULL(MiscFee,0) AS MiscFee, ISNULL(AddlCost,0) AS AddlCost,  " &
                "            VATable, VATInc, ISNULL(ContractPrice,0) AS ContractPrice, viewRE_UnitStatus.Status, ISNULL(VATAmount,0) AS VATAmount " &
                " FROM       tblSaleProperty  LEFT JOIN viewRE_UnitStatus" &
                " ON         tblSaleProperty.UnitCode = viewRE_UnitStatus.UnitCOde " &
                " WHERE      tblSaleProperty.UnitCode = '" & Code & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            disableEvent = True
            txtUnitCode.Text = SQL.SQLDR("UnitCode").ToString
            cbUnitType.SelectedItem = SQL.SQLDR("UnitType").ToString
            txtModel.Text = SQL.SQLDR("Model").ToString
            txtBlk.Text = SQL.SQLDR("Unit_Blk").ToString
            txtLot.Text = SQL.SQLDR("Unit_Lot").ToString
            txtPhase.Text = SQL.SQLDR("Unit_Phase").ToString
            txtUnit.Text = SQL.SQLDR("Unit_No").ToString
            txtBuilding.Text = SQL.SQLDR("Unit_Bldg").ToString
            cbCostCenter.SelectedItem = SQL.SQLDR("Project").ToString
            txtBrgy.Text = SQL.SQLDR("Address_Brgy").ToString
            txtCity.Text = SQL.SQLDR("Address_City").ToString
            txtProvince.Text = SQL.SQLDR("Address_Province").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            'If cbUnitType.SelectedItem = "Parking Slot" Then
            '    txtFloorArea.Text = SQL.SQLDR("FloorArea").ToString
            '    txtLotArea.Text = SQL.SQLDR("LotArea").ToString
            '    nupFloorPrice.Value = SQL.SQLDR("FloorPrice").ToString
            '    nupLotPrice.Value = SQL.SQLDR("LotPrice").ToString
            '    txtFloorPrice.Text = CDec(SQL.SQLDR("TotalFloorPrice")).ToString("N2")
            '    txtLotPrice.Text = CDec(SQL.SQLDR("TotalLotPrice")).ToString("N2")
            '    panelCondo.Visible = False
            '    panelHandL.Visible = True
            '    panelCondoDetails.Visible = False
            '    panelHandLDetails.Visible = True
            'ElseIf cbUnitType.SelectedItem = "Condominium" Then
            'txtFloorArea.Text = SQL.SQLDR("FloorArea").ToString
            'txtLotArea.Text = SQL.SQLDR("LotArea").ToString
            'nupFloorPrice.Value = SQL.SQLDR("FloorPrice").ToString
            'nupLotPrice.Value = SQL.SQLDR("LotPrice").ToString
            'txtFloorPrice.Text = CDec(SQL.SQLDR("TotalFloorPrice")).ToString("N2")
            'txtLotPrice.Text = CDec(SQL.SQLDR("TotalLotPrice")).ToString("N2")
            'panelCondo.Visible = True
            'panelHandL.Visible = True
            'panelCondoDetails.Visible = True
            'panelHandLDetails.Visible = True
            txtTotalSQM.Text = SQL.SQLDR("FloorArea").ToString
            nupCostPerSqm.Value = SQL.SQLDR("FloorPrice").ToString
            txtTotalPrice.Text = CDec(SQL.SQLDR("TotalFloorPrice")).ToString("N2")
            panelCondo.Visible = True
            'panelHandL.Visible = True
            panelCondoDetails.Visible = True
            'panelHandLDetails.Visible = False
            'End If
            nupMiscRate.Value = SQL.SQLDR("MiscRate").ToString
            txtMiscFee.Text = CDec(SQL.SQLDR("MiscFee")).ToString("N2")
            txtAddlCost.Text = CDec(SQL.SQLDR("AddlCost")).ToString("N2")
            chkVAT.Checked = SQL.SQLDR("VATable")
            chkVATInc.Checked = SQL.SQLDR("VATInc")
            txtVAT.Text = CDec(SQL.SQLDR("VATAmount")).ToString("N2")
            txtContractPrice.Text = CDec(SQL.SQLDR("ContractPrice")).ToString("N2")
            txtStatus.Text = SQL.SQLDR("Status").ToString
            disableEvent = False


            ' TOOLSTRIP BUTTONS
            tsbEdit.Enabled = True
            tsbInactive.Enabled = True
            If txtStatus.Text = "Reserved" Then
                tsbEdit.Enabled = False
                tsbInactive.Enabled = False
                OpenToolStripMenuItem.Visible = False
                CloseToolStripMenuItem.Visible = False
                ForfeitedToolStripMenuItem.Visible = True
            ElseIf txtStatus.Text = "Open" Then
                OpenToolStripMenuItem.Visible = False
                CloseToolStripMenuItem.Visible = True
                ForfeitedToolStripMenuItem.Visible = False
                SoldToolStripMenuItem.Visible = True
            ElseIf txtStatus.Text = "Close" Then
                OpenToolStripMenuItem.Visible = True
                CloseToolStripMenuItem.Visible = False
                ForfeitedToolStripMenuItem.Visible = False
                SoldToolStripMenuItem.Visible = True
            ElseIf txtStatus.Text = "Forfeited" Then
                OpenToolStripMenuItem.Visible = True
                CloseToolStripMenuItem.Visible = True
                ForfeitedToolStripMenuItem.Visible = False
                SoldToolStripMenuItem.Visible = True
            ElseIf txtStatus.Text = "Sold" Then
                OpenToolStripMenuItem.Visible = True
                CloseToolStripMenuItem.Visible = False
                ForfeitedToolStripMenuItem.Visible = False
                SoldToolStripMenuItem.Visible = False
            End If
            ' Toolstrip Buttons
            tsbSearch.Enabled = True
            tsbNew.Enabled = True
            tsbSave.Enabled = False
            tsbClose.Enabled = False
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbExit.Enabled = True
            EnableControl(False)
        End If
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("REP_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("REP")
            PropCode = f.transID
            LoadProperty(PropCode)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("REP_ADD") Then
            msgRestricted()
        Else
            PropCode = ""
            'txtUnitCode.Text = GeneratePropCode()
            cbUnitType.SelectedIndex = 0
            txtUnit.Clear()
            txtBuilding.Clear()
            txtBlk.Clear()
            txtLot.Clear()
            txtPhase.Clear()
            txtModel.Clear()
            txtLotArea.Text = 0
            txtFloorArea.Text = 0
            cbCostCenter.SelectedIndex = 0
            'txtProject.Clear()
            txtBrgy.Clear()
            txtCity.Clear()
            txtProvince.Clear()
            txtRemarks.Clear()

            nupFloorPrice.Value = 0
            nupLotPrice.Value = 0
            nupCostPerSqm.Value = 0
            nupMiscRate.Value = 0
            txtFloorPrice.Text = "0.00"
            txtLotPrice.Text = "0.00"
            txtTotalPrice.Text = "0.00"
            txtMiscFee.Text = "0.00"
            txtAddlCost.Text = "0.00"
            txtVAT.Text = "0.00"
            txtContractPrice.Text = "0.00"
            txtStatus.Text = "Open"

            txtUnitCode.Select()
            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbInactive.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False

            EnableControl(True)
        End If
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        txtRemarks.Enabled = Value
        cbUnitType.Enabled = Value
        txtUnit.Enabled = Value
        txtBuilding.Enabled = Value
        txtBlk.Enabled = Value
        txtLot.Enabled = Value
        txtPhase.Enabled = Value
        txtModel.Enabled = Value
        txtLotArea.Enabled = Value
        txtFloorArea.Enabled = Value
        'txtProject.Enabled = Value
        cbCostCenter.Enabled = Value
        txtBrgy.Enabled = Value
        txtCity.Enabled = Value
        txtProvince.Enabled = Value
        chkVAT.Enabled = Value
        chkVATInc.Enabled = Value
        txtAddlCost.Enabled = Value
        nupFloorPrice.Enabled = Value
        nupLotPrice.Enabled = Value
        nupMiscRate.Enabled = Value
        txtTotalSQM.Enabled = Value
        nupCostPerSqm.Enabled = Value
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("REP_ADD") Then
            msgRestricted()
        Else
            EnableControl(True)

            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbInactive.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
        End If
    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If PropCode = "" Then
            tsbNew.PerformClick()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbInactive.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
        Else
            LoadProperty(PropCode)
            tsbEdit.Enabled = True
            tsbInactive.Enabled = True
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
        End If
        tsbSearch.Enabled = True
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
    End Sub

    Private Sub tsbInactive_Click(sender As System.Object, e As System.EventArgs) Handles tsbInactive.Click
        If Not AllowAccess("REP_DEL") Then
            msgRestricted()
        Else
            If txtUnitCode.Text <> "" Then
                If MsgBox("Are you sure you want to inactive this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE tblSaleProperty SET Status = 'Inactive' WHERE PropCode = @PropCode "
                        SQL.FlushParams()
                        SQL.AddParam("@PropCode", txtUnitCode.Text)
                        SQL.ExecNonQuery(deleteSQL)
                        Msg("Record deleted successfully", MsgBoxStyle.Information)

                        tsbNew.PerformClick()
                        tsbSearch.Enabled = True
                        tsbNew.Enabled = True
                        tsbEdit.Enabled = False
                        tsbSave.Enabled = False
                        tsbInactive.Enabled = False
                        tsbClose.Enabled = False
                        tsbPrevious.Enabled = False
                        tsbNext.Enabled = False
                        tsbExit.Enabled = True
                        EnableControl(False)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
                    Finally
                        RecordActivity(UserID, moduleID, Me.Name.ToString, "INACTIVE", "PropCode", txtUnitCode.Text, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub


    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If PropCode = "" Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                If txtUnitCode.Enabled = False Then
                    txtUnitCode.Text = GeneratePropCode()
                    SaveProperty()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    PropCode = txtUnitCode.Text
                    LoadProperty(PropCode)
                Else
                    If RecordExist(txtUnitCode.Text) Then
                        Msg("Property Code already in used! Please change Property Code", MsgBoxStyle.Exclamation)
                    Else
                        txtUnitCode.Text = GeneratePropCode()
                        SaveProperty()
                        Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                        PropCode = txtUnitCode.Text
                        LoadProperty(PropCode)
                    End If
                End If
            End If
        Else
            Dim Validated As Boolean = True
            If PropCode <> txtUnitCode.Text Then
                If RecordExist(txtUnitCode.Text) Then
                    Validated = False
                Else
                    Validated = True
                End If
            End If

            If Validated Then
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    UpdateProperty()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    PropCode = txtUnitCode.Text
                    LoadProperty(PropCode)
                End If
            Else
                Msg("Property Code is already in used! Please change Property Code", MsgBoxStyle.Exclamation)
                txtUnitCode.Text = PropCode
                txtUnitCode.SelectAll()
            End If

        End If
    End Sub

    Private Sub SaveProperty()
        Try
            activityStatus = True
            Dim insertSQL As String
            If Not IsNumeric(txtContractPrice.Text) Then txtContractPrice.Text = "0.00"
            insertSQL = " INSERT INTO " &
                         " tblSaleProperty(UnitCode, UnitType, Model, Unit_No, Unit_Bldg, Unit_Lot, Unit_Blk, Unit_Phase, Project,  " &
                         "                Address_Brgy, Address_City, Address_Province, Remarks, FloorArea, LotArea, " &
                         " 	              FloorPrice, LotPrice, TotalFloorPrice, TotalLotPrice, MiscRate, MiscFee, AddlCost, " &
                         "                VATable, VATInc, ContractPrice, Status, DateCreated, WhoCreated, TransAuto, VATAmount) " &
                         " VALUES(@UnitCode, @UnitType, @Model, @Unit_No, @Unit_Bldg, @Unit_Lot, @Unit_Blk, @Unit_Phase, @Project,  " &
                         "                @Address_Brgy, @Address_City, @Address_Province, @Remarks, @FloorArea, @LotArea, " &
                         " 	              @FloorPrice, @LotPrice, @TotalFloorPrice, @TotalLotPrice, @MiscRate, @MiscFee, @AddlCost, " &
                         "                @VATable, @VATInc, @ContractPrice, @Status, GETDATE(), @WhoCreated, @TransAuto, @VATAmount) "
            SQL.FlushParams()
            SQL.AddParam("@UnitCode", txtUnitCode.Text)
            SQL.AddParam("@UnitType", cbUnitType.SelectedItem)
            SQL.AddParam("@Project", cbCostCenter.SelectedItem)
            SQL.AddParam("@Address_Brgy", txtBrgy.Text)
            SQL.AddParam("@Address_City", txtCity.Text)
            SQL.AddParam("@Address_Province", txtProvince.Text)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            'If cbUnitType.SelectedItem = "Parking Slot" Then
            '    SQL.AddParam("@Model", txtModel.Text)
            '    SQL.AddParam("@Unit_Lot", txtLot.Text)
            '    SQL.AddParam("@Unit_Blk", txtBlk.Text)
            '    SQL.AddParam("@Unit_Phase", txtPhase.Text)
            '    SQL.AddParam("@Unit_No", "")
            '    SQL.AddParam("@Unit_Bldg", "")
            '    SQL.AddParam("@FloorArea", CDec(txtFloorArea.Text))
            '    SQL.AddParam("@LotArea", CDec(txtLotArea.Text))
            '    SQL.AddParam("@FloorPrice", CDec(nupFloorPrice.Value))
            '    SQL.AddParam("@LotPrice", CDec(nupLotPrice.Text))
            '    SQL.AddParam("@TotalFloorPrice", CDec(txtFloorPrice.Text))
            '    SQL.AddParam("@TotalLotPrice", CDec(txtLotPrice.Text))
            'ElseIf cbUnitType.SelectedItem = "Condominium" Then
            SQL.AddParam("@Unit_No", txtUnit.Text)
            SQL.AddParam("@Unit_Bldg", txtBuilding.Text)
            SQL.AddParam("@Model", txtModel.Text)
            SQL.AddParam("@Unit_Lot", txtLot.Text)
            SQL.AddParam("@Unit_Blk", txtBlk.Text)
            SQL.AddParam("@Unit_Phase", txtPhase.Text)
            SQL.AddParam("@FloorArea", CDec(txtTotalSQM.Text))
            SQL.AddParam("@LotArea", 1)
            SQL.AddParam("@FloorPrice", CDec(nupCostPerSqm.Value))
            SQL.AddParam("@LotPrice", 0)
            SQL.AddParam("@TotalFloorPrice", CDec(txtTotalPrice.Text))
            SQL.AddParam("@TotalLotPrice", 0)
            'End If

            SQL.AddParam("@MiscRate", CDec(nupMiscRate.Text))
            SQL.AddParam("@MiscFee", CDec(txtMiscFee.Text))
            SQL.AddParam("@AddlCost", CDec(txtAddlCost.Text))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInc", chkVATInc.Checked)
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@ContractPrice", CDec(txtContractPrice.Text))
            SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.AddParam("@TransAuto", False)

            SQL.ExecNonQuery(insertSQL)
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        Finally
            RecordActivity(UserID, moduleID, Me.Name.ToString, "INSERT", "PropCode", txtUnitCode.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub UpdateProperty()
        Try
            activityStatus = True
            If Not IsNumeric(txtContractPrice.Text) Then txtContractPrice.Text = "0.00"
            Dim updateSQL As String
            updateSQL = " UPDATE   tblSaleProperty " &
                         " SET     UnitType = @UnitType, Model = @Model, Unit_No = @Unit_No, Unit_Bldg = @Unit_Bldg, Unit_Lot = @Unit_Lot, Unit_Blk = @Unit_Blk, Unit_Phase = @Unit_Phase, " &
                         "         Project = @Project,  Address_Brgy = @Address_Brgy, Address_City = @Address_City, Address_Province = @Address_Province, " &
                         "         Remarks = @Remarks, FloorArea = @FloorArea, LotArea = @LotArea, FloorPrice = @FloorPrice, LotPrice = @LotPrice, " &
                         "         TotalFloorPrice = @TotalFloorPrice, TotalLotPrice = @TotalLotPrice, MiscRate = @MiscRate, MiscFee = @MiscFee, " &
                         "         AddlCost = @AddlCost, VATable = @VATable, VATInc = @VATInc, ContractPrice = @ContractPrice,  " &
                         "         Status = @Status, WhoModified = @WhoModified, DateModified = GETDATE(), TransAuto = @TransAuto, VATAmount = @VATAmount " &
                         " WHERE   UnitCode = @UnitCode "
            SQL.FlushParams()
            SQL.AddParam("@UnitCode", txtUnitCode.Text)
            SQL.AddParam("@UnitType", cbUnitType.Text)
            SQL.AddParam("@Project", cbCostCenter.Text)
            SQL.AddParam("@Address_Brgy", txtBrgy.Text)
            SQL.AddParam("@Address_City", txtCity.Text)
            SQL.AddParam("@Address_Province", txtProvince.Text)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            'If cbUnitType.SelectedItem = "Parking Slot" Then
            '    SQL.AddParam("@Model", txtModel.Text)
            '    SQL.AddParam("@Unit_Lot", txtLot.Text)
            '    SQL.AddParam("@Unit_Blk", txtBlk.Text)
            '    SQL.AddParam("@Unit_Phase", txtPhase.Text)
            '    SQL.AddParam("@Unit_No", "")
            '    SQL.AddParam("@Unit_Bldg", "")
            '    SQL.AddParam("@FloorArea", CDec(txtFloorArea.Text))
            '    SQL.AddParam("@LotArea", CDec(txtLotArea.Text))
            '    SQL.AddParam("@FloorPrice", CDec(nupFloorPrice.Value))
            '    SQL.AddParam("@LotPrice", CDec(nupLotPrice.Text))
            '    SQL.AddParam("@TotalFloorPrice", CDec(txtFloorPrice.Text))
            '    SQL.AddParam("@TotalLotPrice", CDec(txtLotPrice.Text))
            'ElseIf cbUnitType.SelectedItem = "Condominium" Then
            SQL.AddParam("@Unit_No", txtUnit.Text)
            SQL.AddParam("@Unit_Bldg", txtBuilding.Text)
            SQL.AddParam("@Model", txtModel.Text)
            SQL.AddParam("@Unit_Lot", txtLot.Text)
            SQL.AddParam("@Unit_Blk", txtBlk.Text)
            SQL.AddParam("@Unit_Phase", txtPhase.Text)
            SQL.AddParam("@FloorArea", CDec(txtTotalSQM.Text))
            SQL.AddParam("@LotArea", 0)
            SQL.AddParam("@FloorPrice", CDec(nupCostPerSqm.Value))
            SQL.AddParam("@LotPrice", 0)
            SQL.AddParam("@TotalFloorPrice", CDec(txtTotalPrice.Text))
            SQL.AddParam("@TotalLotPrice", 0)
            'End If

            SQL.AddParam("@MiscRate", CDec(nupMiscRate.Text))
            SQL.AddParam("@MiscFee", CDec(txtMiscFee.Text))
            SQL.AddParam("@AddlCost", CDec(txtAddlCost.Text))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInc", chkVATInc.Checked)
            SQL.AddParam("@VATAmount", CDec(txtVAT.Text))
            SQL.AddParam("@ContractPrice", CDec(txtContractPrice.Text))
            SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoModified", UserID)
            SQL.AddParam("@TransAuto", False)


            SQL.ExecNonQuery(updateSQL)
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        Finally
            RecordActivity(UserID, moduleID, Me.Name.ToString, "UPDATE", "PropCode", txtUnitCode.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub
    Private Function RecordExist(ByVal Record As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblSaleProperty WHERE PropCode ='" & Record & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If PropCode <> "" Then
            Dim query As String
            query = " SELECT Top 1 UnitCode FROM tblSaleProperty  WHERE UnitCode < '" & PropCode & "' ORDER BY UnitCode DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                PropCode = SQL.SQLDR("UnitCode").ToString
                LoadProperty(PropCode)
            Else
                MsgBox("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If

    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If PropCode <> "" Then
            Dim query As String
            query = " SELECT Top 1 UnitCode FROM tblSaleProperty  WHERE UnitCode > '" & PropCode & "' ORDER BY UnitCode ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                PropCode = SQL.SQLDR("UnitCode").ToString
                LoadProperty(PropCode)
            Else
                MsgBox("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub frmLeaseProperty_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                If tsbInactive.Enabled = True Then tsbInactive.PerformClick()
            ElseIf e.KeyCode = Keys.Left Then
                If tsbPrevious.Enabled = True Then tsbPrevious.PerformClick()
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Right Then
                If tsbNext.Enabled = True Then tsbNext.PerformClick()
                e.SuppressKeyPress = True
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
    Private Function GeneratePropCode() As String
        txtUnitCode.Enabled = False
        Dim query As String
        query = "  SELECT   RIGHT('000000' +  Cast(ISNULL(Max(Cast(UnitCode as int)+1),1) AS nvarchar),6) AS UnitCode " &
                " FROM     tblSaleProperty "

        SQL.ReadQuery(query)
        SQL.SQLDR.Read()
        Return SQL.SQLDR("UnitCode").ToString
    End Function

    Private Sub ContractHistoryToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim f As New frmReport_Display
        f.ShowDialog("LC_ContractHistory", PropCode)
        f.Dispose()
    End Sub

    Private Sub LotPrice_ValueChanged(sender As Object, e As EventArgs) Handles nupLotPrice.ValueChanged, txtLotArea.TextChanged
        If disableEvent = False Then
            ComputeLotPrice()
        End If
    End Sub
    Private Sub SQMPrice_ValueChanged(sender As Object, e As EventArgs) Handles nupCostPerSqm.ValueChanged, txtTotalSQM.TextChanged
        If disableEvent = False Then
            ComputeCondoPrice()
        End If
    End Sub

    Private Sub ComputeCondoPrice()
        If IsNumeric(txtTotalSQM.Text) Then
            txtTotalPrice.Text = CDec(txtTotalSQM.Text) * nupCostPerSqm.Value
            txtTotalPrice.Text = CDec(txtTotalPrice.Text).ToString("N2")
            ComputeMiscFee()
        End If
    End Sub

    Private Sub ComputeLotPrice()
        If IsNumeric(txtLotArea.Text) Then
            txtLotPrice.Text = CDec(txtLotArea.Text) * nupLotPrice.Value
            txtLotPrice.Text = CDec(txtLotPrice.Text).ToString("N2")
            ComputeMiscFee()
        End If
    End Sub

    Private Sub FloorPrice_ValueChanged(sender As Object, e As EventArgs) Handles nupFloorPrice.ValueChanged, txtFloorArea.TextChanged
        If disableEvent = False Then
            ComputeFloorPrice()
        End If
    End Sub
    Private Sub ComputeFloorPrice()
        If IsNumeric(txtFloorArea.Text) Then
            txtFloorPrice.Text = CDec(txtFloorArea.Text) * nupFloorPrice.Value
            txtFloorPrice.Text = CDec(txtFloorPrice.Text).ToString("N2")
            ComputeMiscFee()
        End If
    End Sub

    Private Sub ComputeMiscFee()
        Dim LotPrice As Decimal = 0
        Dim FloorPrice As Decimal = 0
        Dim CondoPrice As Decimal = 0
        Dim Total As Decimal = 0
        Dim VAtAmount As Decimal = 0
        If IsNumeric(txtLotPrice.Text) Then LotPrice = CDec(txtLotPrice.Text)
        If IsNumeric(txtFloorPrice.Text) Then FloorPrice = CDec(txtFloorPrice.Text)
        If IsNumeric(txtTotalPrice.Text) Then CondoPrice = CDec(txtTotalPrice.Text)
        If IsNumeric(txtVAT.Text) Then VAtAmount = CDec(txtVAT.Text)
        'If IsNumeric(txtContractPrice.Text) Then TotalPrice = CDec(txtContractPrice.Text)

        'If cbUnitType.SelectedItem = "Parking Slot" Then
        '    Total = LotPrice + FloorPrice
        'ElseIf cbUnitType.SelectedItem = "Condominium" Then
        '    Total = CondoPrice
        'End If
        Total = CondoPrice + VAtAmount


        txtMiscFee.Text = (Total) * (nupMiscRate.Value / 100.0)
        txtMiscFee.Text = CDec(txtMiscFee.Text).ToString("N2")
        ComputeContractPrice()
    End Sub

    Private Sub nupMiscRate_ValueChanged(sender As Object, e As EventArgs) Handles nupMiscRate.ValueChanged
        If disableEvent = False Then
            ComputeMiscFee()
        End If
    End Sub

    Private Sub ComputeContractPrice()
        Dim CondoPrice As Decimal = 0
        Dim LotPrice As Decimal = 0
        Dim FloorPrice As Decimal = 0
        Dim MiscFee As Decimal = 0
        Dim AddlCost As Decimal = 0
        Dim ContractPrice As Decimal = 0
        If IsNumeric(txtTotalPrice.Text) Then CondoPrice = CDec(txtTotalPrice.Text)
        If IsNumeric(txtLotPrice.Text) Then LotPrice = CDec(txtLotPrice.Text)
        If IsNumeric(txtFloorPrice.Text) Then FloorPrice = CDec(txtFloorPrice.Text)
        If IsNumeric(txtMiscFee.Text) Then MiscFee = CDec(txtMiscFee.Text)
        If IsNumeric(txtAddlCost.Text) Then AddlCost = CDec(txtAddlCost.Text)

        'If cbUnitType.SelectedItem = "Parking Slot" Then
        '    ContractPrice = LotPrice + FloorPrice + MiscFee + AddlCost
        'ElseIf cbUnitType.SelectedItem = "Condominium" Then
        '    ContractPrice = CondoPrice + MiscFee + AddlCost
        'End If

        'ContractPrice = CondoPrice + MiscFee + AddlCost
        ContractPrice = CondoPrice + AddlCost


        If chkVAT.Checked Then
            If chkVATInc.Checked Then
                txtVAT.Text = ((CondoPrice / 1.12) * 0.12).ToString("N2")
                txtContractPrice.Text = (ContractPrice).ToString("N2")
            Else
                txtVAT.Text = ((CondoPrice) * 0.12).ToString("N2")
                txtContractPrice.Text = (ContractPrice + CDec(txtVAT.Text)).ToString("N2")
            End If
        Else
            txtVAT.Text = "0.00"
            txtContractPrice.Text = (ContractPrice + CDec(txtVAT.Text)).ToString()
        End If

    

    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click, OpenToolStripMenuItem.Click, ForfeitedToolStripMenuItem.Click, SoldToolStripMenuItem.Click
        UpdateStatus(PropCode, sender.Text)
    End Sub

    Private Sub UpdateStatus(UnitCode As String, Status As String)
        Dim updateSQL
        If MsgBox("Tagging Unit as " & Status & " " & vbNewLine & "Click Yes to Confirm", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.Yes Then
            updateSQL = " UPDATE tblSaleProperty " &
                        " SET   Status = @Status " &
                        " WHERE UnitCode = @UnitCode "
            SQL.FlushParams()
            SQL.AddParam("@UnitCode", UnitCode)
            SQL.AddParam("@Status", Status)
            SQL.ExecNonQuery(updateSQL)
            txtStatus.Text = Status
            MsgBox("Status changed successfully!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub cbUnitType_Enter(sender As Object, e As EventArgs) Handles gb.Enter

    End Sub

    Private Sub cbUnitType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbUnitType.SelectedIndexChanged
        'If disableEvent = False Then
        '    If cbUnitType.SelectedItem = "Parking Slot" Then
        '        panelCondo.Visible = False
        '        panelHandL.Visible = True
        '        panelCondoDetails.Visible = False
        '        panelHandLDetails.Visible = True
        '    ElseIf cbUnitType.SelectedItem = "Condominium" Then
        '        'panelCondo.Visible = False
        '        'panelHandL.Visible = True
        '        'panelCondoDetails.Visible = False
        '        'panelHandLDetails.Visible = True
        '        panelCondo.Visible = True
        '        panelHandL.Visible = False
        '        panelCondoDetails.Visible = True
        '        panelHandLDetails.Visible = False
        '    End If
        'End If
    End Sub

    Private Sub chkVAT_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkVAT.CheckedChanged
        If disableEvent = False Then
            ComputeContractPrice()
        End If
    End Sub


    Private Sub chkVATInc_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkVATInc.CheckedChanged
        If disableEvent = False Then
            ComputeContractPrice()
        End If
    End Sub

    Private Sub txtAddlCost_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtAddlCost.TextChanged
        If disableEvent = False Then
            ComputeContractPrice()
        End If
    End Sub

    Private Sub txtLotPrice_TextChanged(sender As Object, e As EventArgs) Handles txtLotPrice.TextChanged

    End Sub

    Private Sub txtTotalPrice_TextChanged(sender As Object, e As EventArgs) Handles txtTotalPrice.TextChanged
        Dim selectionStart As Integer = txtTotalPrice.SelectionStart

        If disableEvent = False Then
            If txtTotalSQM.Text <> 0 Then
                nupCostPerSqm.Value = CDec(txtTotalPrice.Text) / CDec(txtTotalSQM.Text)
            End If
        End If

        txtTotalPrice.SelectionStart = selectionStart
        txtTotalPrice.SelectionLength = 0
    End Sub

    Private Sub txtTotalPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotalPrice.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            ' If the entered key is not a numeric digit or control character, ignore it
            e.Handled = True
        End If
    End Sub

    Private Sub txtMiscFee_TextChanged(sender As Object, e As EventArgs) Handles txtMiscFee.TextChanged

    End Sub
End Class