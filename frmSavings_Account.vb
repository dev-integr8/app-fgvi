Public Class frmSavings_Account

    Dim SQL As New SQLControl
    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Dispose()
    End Sub

    Private Sub txtVCEName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
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

    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged

    End Sub

    Private Sub frmSavings_Account_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadType()
    End Sub

    Private Sub LoadType()
        Dim selectSQL As String = "SELECT SavAccountCode as DepositType FROM tblSavings_Type"
        SQL.ReadQuery(selectSQL)
        cmbType.Items.Clear()
        While SQL.SQLDR.Read
            cmbType.Items.Add(GetAccntTitle(SQL.SQLDR("DepositType").ToString))
        End While
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        ClearAll()
        pnlSavings.Enabled = True
        tsbSave.Enabled = True
        tsbClose.Enabled = True
        tsbEdit.Enabled = False
        tsbDelete.Enabled = False
    End Sub

    Private Sub ClearAll()
        txtVCECode.Clear()
        txtVCEName.Clear()
        cmbType.SelectedIndex = -1
        txtAccountNumber.Clear()
        txtAccountNumber.ReadOnly = False
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If txtVCECode.Text <> "" And txtAccountNumber.Text <> "" Then
            If MsgBox("Are you sure you want to save this account?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                Dim insertSQL As String = " INSERT INTO tblSavings_Account(VCECode, DepositType, AccountNumber) " & vbCrLf & _
                                          " VALUES('" & txtVCECode.Text & "', '" & cmbType.SelectedItem & "', '" & txtAccountNumber.Text & "') "
                SQL.CloseCon()
                SQL.ExecNonQuery(insertSQL)
                tsbSave.Enabled = False
                tsbClose.Enabled = False
                tsbEdit.Enabled = False
                tsbDelete.Enabled = False
                MsgBox("Successfully Saved!", MsgBoxStyle.Information, "Save")
                tsbNew.PerformClick()
            End If
        Else
            MsgBox("Please input all fields")
        End If

    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click

        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbEdit.Enabled = False
        tsbDelete.Enabled = False
    End Sub

    Private Sub tsbDelete_Click(sender As System.Object, e As System.EventArgs) Handles tsbDelete.Click
        If MsgBox("", MsgBoxStyle.YesNo, "Delete") = MsgBoxResult.Yes Then
            Dim deleteSQL As String = "DELETE FROM tblSavings_Account WHERE VCECode = '" & txtVCECode.Text & "' AND DepositType = '" & cmbType.SelectedItem & "' "
            SQL.ExecNonQuery(deleteSQL)
            MsgBox("Successfully Deleted!", MsgBoxStyle.Information, "Delete")
            tsbSave.Enabled = False
            tsbClose.Enabled = False
            tsbEdit.Enabled = False
            tsbDelete.Enabled = False
        End If
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        pnlSavings.Enabled = True
        tsbSave.Enabled = True
        tsbClose.Enabled = True
        tsbEdit.Enabled = False
        tsbDelete.Enabled = False
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        Dim f As New frmSavings_AccountSearch
        f.ShowDialog()
        txtVCECode.Text = f.strVCECode
        txtVCEName.Text = f.strVCEName
        cmbType.SelectedItem = f.strDepositType
        txtAccountNumber.Text = f.strAccountNumber
        txtAccountNumber.ReadOnly = True
        If f.strVCECode.Length > 0 Then
            tsbEdit.Enabled = False
            tsbDelete.Enabled = True
        End If
    End Sub

    Private Sub cmbType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbType.SelectedIndexChanged
        Dim selectSQL As String = ""
        Dim prefix As String = ""
        Dim strType As String = "SD"
        selectSQL = "SELECT Prefix FROM tblSavings_Type WHERE Description = '" & cmbType.SelectedItem & "'"
        SQL.ReadQuery(selectSQL)
        If SQL.SQLDR.Read Then
            prefix = SQL.SQLDR("Prefix").ToString
        End If
        prefix = strType & prefix
        If cmbType.SelectedIndex >= 0 Then
            If cmbType.SelectedItem.ToString.ToLower.Contains("time") Then
                strType = "TD"
            End If
            selectSQL = " SELECT    ISNULL(MAX(SUBSTRING(AccountNumber," & prefix.Length + 1 & ",4))+ 1,1) AS TransID  " & _
                               " FROM      tblSavings_Account " & _
                               " WHERE    AccountNumber LIKE '" & prefix & "%' "
            SQL.ReadQuery(selectSQL)
            If SQL.SQLDR.Read Then
                txtAccountNumber.Text = SQL.SQLDR("TransID")
                For i As Integer = 1 To 4
                    txtAccountNumber.Text = "0" & txtAccountNumber.Text
                Next
                txtAccountNumber.Text = prefix & Strings.Right(txtAccountNumber.Text, 4)
            End If
        End If
    End Sub
End Class