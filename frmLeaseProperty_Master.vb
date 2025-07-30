Public Class frmLeaseProperty_Master
    Dim PropCode As String = ""
    Dim moduleID As String = "LPM"
    Dim picPath As String

    Public Overloads Function ShowDialog(ByVal Prop As String) As Boolean
        PropCode = Prop
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmLeaseProperty_Master_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub LoadProperty(ByVal Code As String)
        Dim query As String
        query = " SELECT    PropCode, Description, PropType,  " &
                 "          Address_Unit, Address_Bldg, Address_Lot_Blk, Address_Street, Address_Subd, Address_Brgy, Address_City, Address_Province, " &
                 "          RatePerMonth, PromptRate, VATable, VATInc, Status " &
                 " FROM     tblLeaseProperty " &
                 " WHERE    PropCode = '" & Code & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtPropCode.Text = SQL.SQLDR("PropCode").ToString
            txtDescription.Text = SQL.SQLDR("Description").ToString
            cbType.SelectedItem = SQL.SQLDR("PropType").ToString
            txtUnit.Text = SQL.SQLDR("Address_Unit").ToString
            txtBldg.Text = SQL.SQLDR("Address_Bldg").ToString
            txtBlkLot.Text = SQL.SQLDR("Address_Lot_Blk").ToString
            txtStreet.Text = SQL.SQLDR("Address_Street").ToString
            txtSubd.Text = SQL.SQLDR("Address_Subd").ToString
            txtBrgy.Text = SQL.SQLDR("Address_Brgy").ToString
            txtCity.Text = SQL.SQLDR("Address_City").ToString
            txtProvince.Text = SQL.SQLDR("Address_Province").ToString
            txtRate.Text = SQL.SQLDR("RatePerMonth").ToString
            txtPromptRate.Text = SQL.SQLDR("PromptRate").ToString
            chkVAT.Checked = SQL.SQLDR("VATable")
            chkVATInc.Text = SQL.SQLDR("VATInc")
            txtStatus.Text = SQL.SQLDR("Status").ToString

            ' TOOLSTRIP BUTTONS
            If txtStatus.Text = "Inactive" Then
                tsbEdit.Enabled = False
                tsbInactive.Enabled = False
            Else
                tsbEdit.Enabled = True
                tsbInactive.Enabled = True
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
        If Not AllowAccess("LPM_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("LPM")
            PropCode = f.transID
            LoadProperty(PropCode)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("LPM_ADD") Then
            msgRestricted()
        Else
            LoadType()
            PropCode = ""
            txtPropCode.Text = GeneratePropCode()
            txtDescription.Clear()
            cbType.Text = ""
            txtUnit.Clear()
            txtBldg.Clear()
            txtBlkLot.Clear()
            txtStreet.Clear()
            txtSubd.Clear()
            txtBrgy.Clear()
            txtCity.Clear()
            txtProvince.Clear()
            txtRate.Text = "0.00"
            txtPromptRate.Text = "0.00"
            txtStatus.Text = "Open"

            txtPropCode.Select()
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
    Private Sub LoadType()
        Dim query As String
        cbType.Items.Clear()
        query = " SELECT DISTINCT PropType FROM tblLeaseProperty WHERE Status ='Active' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            cbType.Items.Add(SQL.SQLDR("PropType").ToString)
        End While
    End Sub
    Private Sub EnableControl(ByVal Value As Boolean)
        txtDescription.Enabled = Value
        cbType.Enabled = Value
        txtUnit.Enabled = Value
        txtBldg.Enabled = Value
        txtBlkLot.Enabled = Value
        txtStreet.Enabled = Value
        txtSubd.Enabled = Value
        txtBrgy.Enabled = Value
        txtCity.Enabled = Value
        txtProvince.Enabled = Value
        txtRate.Enabled = Value
        txtPromptRate.Enabled = Value
        chkVAT.Enabled = Value
        chkVATInc.Enabled = Value
        LoadType()
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("LPM_ADD") Then
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
        If Not AllowAccess("LPM_DEL") Then
            msgRestricted()
        Else
            If txtPropCode.Text <> "" Then
                If MsgBox("Are you sure you want to inactive this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE tblLeaseProperty SET Status = 'Inactive' WHERE PropCode = @PropCode "
                        SQL.FlushParams()
                        SQL.AddParam("@PropCode", txtPropCode.Text)
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
                        RecordActivity(UserID, moduleID, Me.Name.ToString, "INACTIVE", "PropCode", txtPropCode.Text, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub


    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If txtPropCode.Text = "" Or txtDescription.Text = "" Then
            Msg("Please enter Property Description!", MsgBoxStyle.Exclamation)
        ElseIf PropCode = "" Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                If txtPropCode.Enabled = False Then
                    txtPropCode.Text = GeneratePropCode()
                    SaveProperty()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    PropCode = txtPropCode.Text
                    LoadProperty(PropCode)
                Else
                    If RecordExist(txtPropCode.Text) Then
                        Msg("Property Code already in used! Please change Property Code", MsgBoxStyle.Exclamation)
                    Else
                        txtPropCode.Text = GeneratePropCode()
                        SaveProperty()
                        Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                        PropCode = txtPropCode.Text
                        LoadProperty(PropCode)
                    End If
                End If
            End If
        Else
            Dim Validated As Boolean = True
            If PropCode <> txtPropCode.Text Then
                If RecordExist(txtPropCode.Text) Then
                    Validated = False
                Else
                    Validated = True
                End If
            End If

            If Validated Then
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    UpdateProperty()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    PropCode = txtPropCode.Text
                    LoadProperty(PropCode)
                End If
            Else
                Msg("Property Code is already in used! Please change Property Code", MsgBoxStyle.Exclamation)
                txtPropCode.Text = PropCode
                txtPropCode.SelectAll()
            End If

        End If
    End Sub

    Private Sub SaveProperty()
        Try
            activityStatus = True
            Dim insertSQL As String
            If Not IsNumeric(txtRate.Text) Then txtRate.Text = "0.00"
            insertSQL = " INSERT INTO " &
                         " tblLeaseProperty(PropCode, Description, PropType, Address_Unit, Address_Bldg, Address_Lot_Blk,  " &
                         "                Address_Street, Address_Subd, Address_Brgy, Address_City, Address_Province, " &
                         " 	              RatePerMonth, PromptRate, VATable, VATInc, Status, DateCreated, WhoCreated, TransAuto) " &
                         " VALUES(@PropCode, @Description, @PropType, @Address_Unit, @Address_Bldg, @Address_Lot_Blk,  " &
                         "                @Address_Street, @Address_Subd, @Address_Brgy, @Address_City, @Address_Province, " &
                         " 	              @RatePerMonth, @PromptRate, @VATable, @VATInc, @Status, GETDATE(), @WhoCreated, @TransAuto) "
            SQL.FlushParams()
            SQL.AddParam("@PropCode", txtPropCode.Text)
            SQL.AddParam("@Description", txtDescription.Text)
            SQL.AddParam("@PropType", cbType.Text)
            SQL.AddParam("@Address_Unit", txtUnit.Text)
            SQL.AddParam("@Address_Bldg", txtBldg.Text)
            SQL.AddParam("@Address_Lot_Blk", txtBlkLot.Text)
            SQL.AddParam("@Address_Street", txtStreet.Text)
            SQL.AddParam("@Address_Subd", txtSubd.Text)
            SQL.AddParam("@Address_Brgy", txtBrgy.Text)
            SQL.AddParam("@Address_City", txtCity.Text)
            SQL.AddParam("@Address_Province", txtProvince.Text)
            SQL.AddParam("@RatePerMonth", CDec(txtRate.Text))
            SQL.AddParam("@PromptRate", CDec(txtPromptRate.Text))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInc", chkVATInc.Checked)
            SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.AddParam("@TransAuto", False)

            SQL.ExecNonQuery(insertSQL)
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        Finally
            RecordActivity(UserID, moduleID, Me.Name.ToString, "INSERT", "PropCode", txtPropCode.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub UpdateProperty()
        Try
            activityStatus = True
            If Not IsNumeric(txtRate.Text) Then txtRate.Text = "0.00"
            Dim updateSQL As String
            updateSQL = " UPDATE   tblLeaseProperty " &
                         " SET     Description = @Description, PropType = @PropType,  " &
                         "         Address_Unit = @Address_Unit, Address_Lot_Blk = @Address_Lot_Blk, Address_Bldg = @Address_Bldg, " &
                         "         Address_Street = @Address_Street, Address_Subd = @Address_Subd, Address_Brgy = @Address_Brgy,  " &
                         "         Address_City = @Address_City, Address_Province = @Address_Province, " &
                         "         RatePerMonth = @RatePerMonth, PromptRate = @PromptRate, VATable = @VATable, VATInc = @VATInc, " &
                         "         WhoModified = @WhoModified, DateModified = GETDATE(), TransAuto = @TransAuto " &
                         " WHERE   PropCode = @PropCode "
            SQL.FlushParams()
            SQL.AddParam("@PropCode", txtPropCode.Text)
            SQL.AddParam("@Description", txtDescription.Text)
            SQL.AddParam("@PropType", cbType.Text)
            SQL.AddParam("@Address_Unit", txtUnit.Text)
            SQL.AddParam("@Address_Bldg", txtBldg.Text)
            SQL.AddParam("@Address_Lot_Blk", txtBlkLot.Text)
            SQL.AddParam("@Address_Street", txtStreet.Text)
            SQL.AddParam("@Address_Subd", txtSubd.Text)
            SQL.AddParam("@Address_Brgy", txtBrgy.Text)
            SQL.AddParam("@Address_City", txtCity.Text)
            SQL.AddParam("@Address_Province", txtProvince.Text)
            SQL.AddParam("@PromptRate", CDec(txtPromptRate.Text))
            SQL.AddParam("@RatePerMonth", CDec(txtRate.Text))
            SQL.AddParam("@VATable", chkVAT.Checked)
            SQL.AddParam("@VATInc", chkVATInc.Checked)
            SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoModified", UserID)
            SQL.AddParam("@TransAuto", False)


            SQL.ExecNonQuery(updateSQL)
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        Finally
            RecordActivity(UserID, moduleID, Me.Name.ToString, "UPDATE", "PropCode", txtPropCode.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub
    Private Function RecordExist(ByVal Record As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblLeaseProperty WHERE PropCode ='" & Record & "' "
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
            query = " SELECT Top 1 PropCode FROM tblLeaseProperty  WHERE PropCode < '" & PropCode & "' ORDER BY PropCode DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                PropCode = SQL.SQLDR("PropCode").ToString
                LoadProperty(PropCode)
            Else
                MsgBox("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If

    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If PropCode <> "" Then
            Dim query As String
            query = " SELECT Top 1 PropCode FROM tblLeaseProperty  WHERE PropCode > '" & PropCode & "' ORDER BY PropCode ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                PropCode = SQL.SQLDR("PropCode").ToString
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
        txtPropCode.Enabled = False
        Dim query As String
        query = "  SELECT   RIGHT('000000' +  Cast(ISNULL(Max(Cast(PropCode as int)+1),1) AS nvarchar),6) AS PropCode " &
                " FROM     tblLeaseProperty "

        SQL.ReadQuery(query)
        SQL.SQLDR.Read()
        Return SQL.SQLDR("PropCode").ToString
    End Function

    Private Sub ContractHistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContractHistoryToolStripMenuItem.Click
        Dim f As New frmReport_Display
        f.ShowDialog("LC_ContractHistory", PropCode)
        f.Dispose()
    End Sub
End Class