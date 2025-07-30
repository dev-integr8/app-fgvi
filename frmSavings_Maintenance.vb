Public Class frmSavings_Maintenance
    Dim TransID As Integer


    Dim SQL As New SQLControl
    Private Sub frmSavings_Maintenance_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadSavings()
    End Sub

    Private Sub btnNew_Click(sender As System.Object, e As System.EventArgs)
        ClearAll()
    End Sub

    Private Sub LoadSavings()
        Dim selectSQL As String = " SELECT SavingsCode, Description FROM tblSavings_Type "
        SQL.ReadQuery(selectSQL)
        dgvSavings.Rows.Clear()
        While SQL.SQLDR.Read
            dgvSavings.Rows.Add(SQL.SQLDR("SavingsCode").ToString, SQL.SQLDR("Description").ToString)
        End While
    End Sub

    Private Sub ClearAll()
        TransID = 0
        txtDesc.Clear()
        cmbType.SelectedIndex = -1
        txtAPR.Clear()
        cmbMethod.SelectedIndex = -1
        cmbPeriod.SelectedIndex = -1
        nudNoDaysMonth.Value = 30
        nudNoDaysYear.Value = 365
        txtMinimum.Clear()
        txtPrefix.Clear()
        txtSavAccntCode.Clear()
        txtSavAccntTitle.Clear()
        txtSavAccntCode2.Clear()
        txtSavAccntTitle2.Clear()
        txtExpAccntCode.Clear()
        txtExpAccntTitle.Clear()
        pnlSavings.Enabled = False
    End Sub

    Private Sub dgvSavings_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSavings.CellClick
        If dgvSavings.SelectedCells.Count > 0 Then
            LoadSavingsDetail(dgvSavings.Rows(dgvSavings.SelectedCells(0).RowIndex).Cells(colSavingsCode.Index).Value)
            TransID = dgvSavings.Rows(dgvSavings.SelectedCells(0).RowIndex).Cells(colSavingsCode.Index).Value
            tsbEdit.Enabled = True
            tsbSave.Enabled = False
            tsbClose.Enabled = True
            tsbDelete.Enabled = True
        End If
    End Sub

    Private Sub LoadSavingsDetail(ByVal strSavingsCode As String)
        Dim selectSQL As String = "SELECT * FROM tblSavings_Type WHERE SavingsCode = " & strSavingsCode
        SQL.ReadQuery(selectSQL)
        ClearAll()
        If SQL.SQLDR.Read Then
            txtDesc.Text = SQL.SQLDR("Description").ToString
            cmbType.SelectedItem = SQL.SQLDR("Type").ToString
            txtAPR.Text = SQL.SQLDR("APR").ToString
            cmbMethod.SelectedItem = SQL.SQLDR("Method").ToString
            cmbPeriod.SelectedItem = SQL.SQLDR("Period").ToString
            nudNoDaysMonth.Value = SQL.SQLDR("NoDaysMonth").ToString
            nudNoDaysYear.Value = SQL.SQLDR("NoDaysYear").ToString
            txtMinimum.Text = SQL.SQLDR("Minimum").ToString
            txtSavAccntCode.Text = SQL.SQLDR("SavAccountCode").ToString
            nudSav.Value = SQL.SQLDR("SavPercent").ToString
            txtSavAccntCode2.Text = SQL.SQLDR("SavAccountCode2").ToString
            nudSav2.Value = SQL.SQLDR("SavPercent2").ToString
            txtExpAccntCode.Text = SQL.SQLDR("ExpAccountCode").ToString
            txtPrefix.Text = SQL.SQLDR("Prefix").ToString
            LoadAccountTitle()
        End If
    End Sub

    Private Sub LoadAccountTitle()
        Dim selectSQL As String = "SELECT AccountTitle FROM tblCOA_Master WHERE AccountCode = '" & txtSavAccntCode.Text & "'"
        SQL.ReadQuery(selectSQL)
        If SQL.SQLDR.Read Then
            txtSavAccntTitle.Text = SQL.SQLDR("AccountTitle").ToString
        End If
        selectSQL = "SELECT AccountTitle FROM tblCOA_Master WHERE AccountCode = '" & txtSavAccntCode2.Text & "'"
        SQL.ReadQuery(selectSQL)
        If SQL.SQLDR.Read Then
            txtSavAccntTitle2.Text = SQL.SQLDR("AccountTitle").ToString
        End If
        selectSQL = "SELECT AccountTitle FROM tblCOA_Master WHERE AccountCode = '" & txtExpAccntCode.Text & "'"
        SQL.ReadQuery(selectSQL)
        If SQL.SQLDR.Read Then
            txtExpAccntTitle.Text = SQL.SQLDR("AccountTitle").ToString
        End If
    End Sub

    Private Sub txtSavAccntTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSavAccntTitle.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmCOA_Search
            f.ShowDialog("AccntTitle", txtSavAccntTitle.Text)
            txtSavAccntCode.Text = f.accountcode
            LoadAccountTitle()
        End If
    End Sub

    Private Sub txtExpAccntTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtExpAccntTitle.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmCOA_Search
            f.ShowDialog("AccntTitle", txtExpAccntTitle.Text)
            txtExpAccntCode.Text = f.accountcode
            LoadAccountTitle()
        End If
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        ClearAll()
        tsbEdit.Enabled = False
        tsbSave.Enabled = True
        tsbClose.Enabled = True
        tsbDelete.Enabled = False

        pnlSavings.Enabled = True
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Dispose()
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        ClearAll()
        tsbEdit.Enabled = False
        tsbSave.Enabled = False
        tsbDelete.Enabled = False
        tsbClose.Enabled = False
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("SM_EDIT") Then
            msgRestricted()
        Else
            pnlSavings.Enabled = True
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbDelete.Enabled = False
            tsbClose.Enabled = True
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If Not AllowAccess("SM_ADD") Then
            msgRestricted()
        Else
            If txtDesc.Text <> "" And cmbType.SelectedIndex > -1 And IsNumeric(txtAPR.Text) And cmbMethod.SelectedIndex > -1 And cmbPeriod.SelectedIndex > -1 And IsNumeric(txtMinimum.Text) And txtSavAccntCode.Text <> "" And txtExpAccntCode.Text <> "" Then
                If TransID = 0 Then
                    If MsgBox("Are you sure you want to save this type?", MsgBoxStyle.YesNo, "Save") = MsgBoxResult.Yes Then
                        Dim strDescription As String = txtDesc.Text
                        Dim strType As String = cmbType.SelectedItem
                        Dim strAPR As String = txtAPR.Text
                        Dim strMethod As String = cmbMethod.SelectedItem
                        Dim strPeriod As String = cmbPeriod.SelectedItem
                        Dim strNoDaysMonth As String = nudNoDaysMonth.Value
                        Dim strNoDaysYear As String = nudNoDaysYear.Value
                        Dim strMinimum As String = txtMinimum.Text
                        Dim strSavAccountCode As String = txtSavAccntCode.Text
                        Dim strSavPercent As String = nudSav.Value
                        Dim strSavAccountCode2 As String = txtSavAccntCode2.Text
                        Dim strSavPercent2 As String = nudSav2.Value
                        Dim strExpAccountCode As String = txtExpAccntCode.Text
                        Dim strPrefix As String = txtPrefix.Text
                        Dim insertSQL As String = "INSERT INTO tblSavings_Type(Description, Type, APR, Method, Period, NoDaysMonth, NoDaysYear, Minimum, SavAccountCode, SavPercent, ExpAccountCode, Prefix, SavAccountCode2, SavPercent2)" & _
                                                  "VALUES('" & strDescription & "', '" & strType & "', '" & strAPR & "', '" & strMethod & "', '" & strPeriod & "', '" & strNoDaysMonth & "', '" & strNoDaysYear & "', '" & strMinimum & "', '" & strSavAccountCode & "', '" & strSavPercent & "', '" & strExpAccountCode & "', '" & strPrefix & "', '" & strSavAccountCode2 & "', '" & strSavPercent2 & "')"
                        SQL.ExecNonQuery(insertSQL)
                        MsgBox("Sucessfully Saved!", MsgBoxStyle.Information, "Save")
                        ClearAll()
                        LoadSavings()
                        tsbEdit.Enabled = False
                        tsbSave.Enabled = False
                        tsbClose.Enabled = False
                        tsbDelete.Enabled = False
                    End If
                Else
                    If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                        Dim UpdateSql As String = " UPDATE tblSavings_Type  " & _
                                                   " SET     Type = @Type, APR = @APR, Method = @Method, " & _
                                                   "        Period = @Period, NoDaysMonth = @NoDaysMonth, NoDaysYear = @NoDaysYear, " & _
                                                   " Minimum = @Minimum, SavAccountCode = @SavAccountCode, SavPercent = @SavPercent, SavAccountCode2 = @SavAccountCode2, SavPercent2 = @SavPercent2, ExpAccountCode = @ExpAccountCode, Prefix =@Prefix " & _
                                                   " WHERE SavingsCode = @SavingsCode "
                        SQL.FlushParams()
                        SQL.AddParam("@SavingsCode", TransID)
                        SQL.AddParam("@Type", cmbType.SelectedItem)
                        SQL.AddParam("@APR", txtAPR.Text)
                        SQL.AddParam("@Method", cmbMethod.SelectedItem)
                        SQL.AddParam("@Period", cmbPeriod.SelectedItem)
                        SQL.AddParam("@NoDaysMonth", nudNoDaysMonth.Value)
                        SQL.AddParam("@NoDaysYear", nudNoDaysYear.Value)
                        SQL.AddParam("@Minimum", txtMinimum.Text)
                        SQL.AddParam("@SavAccountCode", txtSavAccntCode.Text)
                        SQL.AddParam("@SavPercent", nudSav.Value)
                        SQL.AddParam("@SavAccountCode2", txtSavAccntCode2.Text)
                        SQL.AddParam("@SavPercent2", nudSav2.Value)
                        SQL.AddParam("@ExpAccountCode", txtExpAccntCode.Text)
                        SQL.AddParam("@Prefix", txtPrefix.Text)
                        SQL.ExecNonQuery(UpdateSql)
                        MsgBox("Update Sucessfully!", MsgBoxStyle.Information, "Save")
                        ClearAll()
                        LoadSavings()
                        tsbEdit.Enabled = False
                        tsbSave.Enabled = False
                        tsbClose.Enabled = False
                        tsbDelete.Enabled = False
                    End If
                End If
            Else
                MsgBox("Invalid Input", MsgBoxStyle.Information, "Error")
            End If
        End If
    End Sub

    Private Sub tsbDelete_Click(sender As System.Object, e As System.EventArgs) Handles tsbDelete.Click
        If Not AllowAccess("SM_ADD") Then
            msgRestricted()
        Else
            If MsgBox("Are you sure you want to delete this type?", MsgBoxStyle.YesNo, "Save") = MsgBoxResult.Yes Then
                Dim deleteSQL As String = "DELETE FROM tblSavings_Type WHERE SavingsCode = '" & dgvSavings.Rows(dgvSavings.SelectedCells(0).RowIndex).Cells(colSavingsCode.Index).Value & "'"
                SQL.ExecNonQuery(deleteSQL)
                MsgBox("Sucessfully Deleted!", MsgBoxStyle.Information, "Delete")
                ClearAll()
                LoadSavings()
                tsbEdit.Enabled = False
                tsbSave.Enabled = False
                tsbClose.Enabled = False
                tsbDelete.Enabled = False
            End If
        End If
    End Sub

    Private Sub txtSavAccntTitle2_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSavAccntTitle2.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmCOA_Search
            f.ShowDialog("AccntTitle", txtSavAccntTitle2.Text)
            txtSavAccntCode2.Text = f.accountcode
            LoadAccountTitle()
        End If
    End Sub

   
    Private Sub txtSavAccntTitle2_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSavAccntTitle2.TextChanged

    End Sub
End Class