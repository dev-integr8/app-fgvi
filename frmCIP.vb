Public Class frmCIP
    Dim Code As String
    Dim moduleID As String = "CIP"

    Private Sub frmCostCenter_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub loadData()
        LoadRecords() ' LOAD DATAGRIDVIEW DATA

        ClearText()
        tsbNew.Enabled = True
        tsbEdit.Enabled = False
        tsbSave.Enabled = False
        tsbDelete.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
        EnableControl(False)
    End Sub


    Private Sub LoadRecords() ' LOAD CIP
        Dim query As String
        query = " select CIP_Code, CIP_Desc, tblCIP_Maintenance.AccntCode, AccountTitle " & _
                " FROM tblCIP_Maintenance " & _
                " LEFT JOIN tblCOA_Master ON " & _
                " tblCOA_Master.AccountCode = tblCIP_Maintenance.AccntCode " & _
                " WHERE Status ='Active' "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvData.DataSource = SQL.SQLDS.Tables(0)
            dgvData.Columns(0).Width = 100
            dgvData.Columns(1).Width = 300
            dgvData.Columns(2).Width = 100
            dgvData.Columns(3).Width = 300
        Else
            dgvData.DataSource = Nothing
        End If
        dgvData.Refresh()
    End Sub

    Private Sub dgvCC_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvData.CellClick
        LoadRecord()
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("CIP_ADD") Then
            msgRestricted()
        Else
            Code = ""
            ClearText()
            ' Toolstrip Buttons
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbDelete.Enabled = False
            tsbClose.Enabled = True


            EnableControl(True)
        End If
    End Sub

    Private Sub ClearText()
        txtCode.Clear()
        txtDescription.Clear()
        txtAccntCode.Clear()
        txtAccntTitle.Clear()
        txtOldCode.Clear()
        cbG1.Text = ""
        cbG2.Text = ""
        cbG3.Text = ""
        cbG4.Text = ""
        cbG5.Text = ""
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)

        txtCode.Enabled = Value
        txtDescription.Enabled = Value
        txtAccntTitle.Enabled = Value
        cbG1.Enabled = Value
        cbG2.Enabled = Value
        cbG3.Enabled = Value
        cbG4.Enabled = Value
        cbG5.Enabled = Value
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If Code = "" Then
            If Validated() Then
                If MsgBox("Saving New CIP, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    SaveRecord()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    loadData()
                End If
            End If
        Else
            If MsgBox("Updating CIP, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                UpdateRecord()
                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                loadData()
            End If
        End If

    End Sub

    Private Sub SaveRecord()
        Try
            activityStatus = True
            ' INSERT QUERY
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblCIP_Maintenance(CIP_Code, CIP_Desc, AccntCode, WhoCreated) " & _
                        " VALUES(@CIP_Code, @CIP_Desc, @AccntCode, @WhoCreated) "
            SQL.FlushParams()
            SQL.AddParam("@CIP_Code", txtCode.Text)
            SQL.AddParam("@CIP_Desc", txtDescription.Text)
            SQL.AddParam("@AccntCode", txtAccntCode.Text)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        Finally
            RecordActivity(UserID, moduleID, Me.Name.ToString, "INSERT", "Code", txtCode.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub UpdateRecord()
        Try
            activityStatus = True
            ' INSERT QUERY
            Dim updateSQL As String
            updateSQL = " UPDATE tblCIP_Maintenance " & _
                        " SET    CIP_Code = @CIP_Code, CIP_Desc = @CIP_Desc, AccntCode = @AccntCode, WhoModified = @WhoModified, DateModified = GETDATE() " & _
                        " WHERE  CIP_Code = @CIP_Code "
            SQL.FlushParams()
            SQL.AddParam("@CIP_Code", txtCode.Text)
            SQL.AddParam("@CIP_Desc", txtDescription.Text)
            SQL.AddParam("@AccntCode", txtAccntCode.Text)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        Finally
            RecordActivity(UserID, moduleID, Me.Name.ToString, "UPDATE", "Code", txtCode.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Shadows Function Validated() As Boolean
        If txtDescription.Text = "" Then
            Msg("Please enter CIP description!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf txtCode.Text = "" Then
            Msg("Please enter CIP Code!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf txtAccntCode.Text = "" Then
            Msg("Please enter Account Code!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf ValidCIP_Code() <> "" Then
            Msg("CIP code already used. " & ValidCIP_Code() & "!", MsgBoxStyle.Exclamation)
            Return False
        Else
            Return True
        End If
    End Function

    Private Function ValidCIP_Code() As String
        Dim query As String
        query = " SELECT CIP_Code FROM tblCIP_Maintenance WHERE  CIP_Code = @CIP_Code  "
        SQL.FlushParams()
        SQL.AddParam("@CIP_Code", txtCode.Text)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("CIP_Code")
        Else
            Return ""
        End If
    End Function

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        ' Toolstrip Buttons
        If Code = "" Then
            ClearText()
            tsbNew.PerformClick()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbDelete.Enabled = False
        Else
            ClearText()
            LoadRecord()
            tsbEdit.Enabled = True
            tsbDelete.Enabled = True
        End If

        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
    End Sub

    Private Sub LoadRecord()
        If dgvData.SelectedRows.Count = 1 Then
            txtCode.Text = dgvData.SelectedRows(0).Cells(0).Value.ToString
            Code = dgvData.SelectedRows(0).Cells(0).Value.ToString
            txtDescription.Text = dgvData.SelectedRows(0).Cells(1).Value.ToString
            txtAccntCode.Text = dgvData.SelectedRows(0).Cells(2).Value.ToString
            txtAccntTitle.Text = GetAccntTitle(dgvData.SelectedRows(0).Cells(2).Value.ToString)
            tsbNew.Enabled = True
            tsbEdit.Enabled = True
            tsbSave.Enabled = False
            tsbDelete.Enabled = True
            tsbClose.Enabled = False
            tsbExit.Enabled = True
            EnableControl(False)
        End If
    End Sub

    Private Sub frmCostCenter_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control = True Then
            If e.KeyCode = Keys.S Then
                If tsbSave.Enabled = True Then tsbSave.PerformClick()
            ElseIf e.KeyCode = Keys.N Then
                If tsbNew.Enabled = True Then tsbNew.PerformClick()
            ElseIf e.KeyCode = Keys.E Then
                If tsbEdit.Enabled = True Then tsbEdit.PerformClick()
            ElseIf e.KeyCode = Keys.D Then
                If tsbDelete.Enabled = True Then tsbDelete.PerformClick()
            End If
        ElseIf e.Alt = True Then
            If e.KeyCode = Keys.F4 Then
                If tsbClose.Enabled = True Then
                    tsbClose.PerformClick()
                Else
                    e.SuppressKeyPress = True
                End If
            End If
        ElseIf e.KeyCode = Keys.Escape Then
            If tsbClose.Enabled = True Then tsbClose.PerformClick()
        End If
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("CIP_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)

            ' Toolstrip Buttons
            txtCode.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbDelete.Enabled = False
            tsbClose.Enabled = True
            tsbExit.Enabled = False
        End If
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbDelete_Click(sender As System.Object, e As System.EventArgs) Handles tsbDelete.Click
        If Not AllowAccess("CIP_DEL") Then
            msgRestricted()
        Else
            If txtCode.Text <> "" Then
                If MsgBox("Are you sure you want to delete this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim updateSQL As String
                        updateSQL = " UPDATE tblCIP_Maintenance SET Status ='Inactive', DateModified = GETDATE(), WhoModified = @WhoModified WHERE CIP_Code = @CIP_Code "
                        SQL.FlushParams()
                        Code = txtCode.Text
                        SQL.AddParam("@CIP_Code", Code)
                        SQL.AddParam("@WhoModified", UserID)
                        SQL.ExecNonQuery(updateSQL)
                        Msg("Record deleted successfully", MsgBoxStyle.Information)

                        loadData()
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
                    Finally
                        RecordActivity(UserID, moduleID, Me.Name.ToString, "DELETE", "Code", Code, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub txtAccntTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtAccntTitle.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmCOA_Search
            f.ShowDialog("AccntTitle", txtAccntTitle.Text)
            txtAccntTitle.Text = f.accttile
            txtAccntCode.Text = f.accountcode
            f.Dispose()
        End If
    End Sub

    Private Sub dgvData_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvData.CellContentClick

    End Sub
End Class