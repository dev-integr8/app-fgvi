﻿Public Class frmItem_Master_AddAccount
    Dim moduleID As String = "ITM"
    Dim RecordID As String = ""
    Dim modTbl As String = "tblBI_Type"
    Dim modCol As String = "RecordID"

    Private Sub frmDisbursement_Type_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LoadAccnt()
            EnableControl(False)
            tsbNew.Enabled = True
            tsbEdit.Enabled = False
            tsbSave.Enabled = False
            tsbDelete.Enabled = False
            tsbClose.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ClearText()
        txtAccntCode.Text = ""
        txtAccntTitle.Text = ""
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        txtAccntCode.ReadOnly = True
        txtAccntTitle.ReadOnly = Not Value
    End Sub

    Private Sub lvType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvType.Click
        If lvType.SelectedItems.Count > 0 Then
            RecordID = lvType.Items(lvType.SelectedItems(0).Index).SubItems(chID.Index).Text
            txtItemCode.Text = lvType.Items(lvType.SelectedItems(0).Index).SubItems(chType.Index).Text()
            txtAccntCode.Text = lvType.Items(lvType.SelectedItems(0).Index).SubItems(chAccntCode.Index).Text()
            txtAccntTitle.Text = lvType.Items(lvType.SelectedItems(0).Index).SubItems(chAccntTitle.Index).Text()
            EnableControl(False)

            tsbNew.Enabled = True
            tsbEdit.Enabled = True
            tsbDelete.Enabled = True
            tsbSave.Enabled = False
            tsbClose.Enabled = False
        End If
    End Sub


    Private Sub txtAccntTitle_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAccntTitle.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmCOA_Search
            f.ShowDialog("AccntTitle", txtAccntTitle.Text)
            txtAccntTitle.Text = f.accttile
            txtAccntCode.Text = f.accountcode
            f.Dispose()
        End If
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LoadAccnt()
    End Sub


    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("BI_MNT") Then
            msgRestricted()
        Else
            txtAccntCode.Clear()
            txtAccntTitle.Clear()
            RecordID = ""

            ' Toolstrip Buttons
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbDelete.Enabled = False
            tsbClose.Enabled = True

            EnableControl(True)
        End If
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("BI_MNT") Then
            msgRestricted()
        Else
            EnableControl(True)
            ' Toolstrip Buttons
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbDelete.Enabled = False
            tsbClose.Enabled = True
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If RecordID = "" Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                If RecordExist(txtItemCode.Text) Then
                    Msg(" already in used! Please change SoftwareCode", MsgBoxStyle.Exclamation)
                Else
                    RecordID = GetRecordID(modTbl, modCol)
                    SaveType()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    LoadAccnt()
                End If
            End If
        Else
            ' IF VCECODE IS CHANGED VALIDATE IF NEW CODE EXIST
            If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                UpdateType()
                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                LoadAccnt()
            End If
        End If
    End Sub

    Private Sub SaveType()
        Try
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                         " tblBI_Type(RecordID, Type, Description, DefaultAccountDR, DefaultAccountCR , DefaultAccountDiscount, Amount, WhoCreated) " & _
                         " VALUES(@RecordID, @Type, @Description, @DefaultAccountDR, @DefaultAccountCR, @DefaultAccountDiscount, @Amount,@WhoCreated)"
            SQL.FlushParams()
            SQL.AddParam("@RecordID", RecordID)
            SQL.AddParam("@Type", txtItemCode.Text)
            SQL.AddParam("@Description", txtItemName.Text)
            SQL.AddParam("@DefaultAccountDR", txtAccntCode.Text)
            SQL.AddParam("@WhoCreated", UserID)

            SQL.ExecNonQuery(insertSQL)
        Catch ex As System.Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        Finally
            RecordActivity(UserID, moduleID, Me.Name.ToString, "INSERT", "RecordID", RecordID, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub UpdateType()
        Try
            activityStatus = True
            Dim updateSQL As String
            updateSQL = " UPDATE tblBI_Type " & _
                        " SET    Type = @Type, Description = @Description, DefaultAccountDR = @DefaultAccountDR, DefaultAccountCR = @DefaultAccountCR, DefaultAccountDiscount = @DefaultAccountDiscount, Amount = @Amount, WhoModified = @WhoModified, DateModified = GETDATE() " & _
                        " WHERE  RecordID = @RecordID "
            SQL.FlushParams()
            SQL.AddParam("@RecordID", RecordID)
            SQL.AddParam("@Type", txtItemCode.Text)
            SQL.AddParam("@Description", txtItemName.Text)
            SQL.AddParam("@DefaultAccountDR", txtAccntCode.Text)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)
        Catch ex As System.Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        Finally
            RecordActivity(UserID, moduleID, Me.Name.ToString, "UPDATE", "RecordID", RecordID, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub LoadAccnt()
        Dim query As String
        query = " SELECT   RecordID, Type, Description, DefaultAccountDR, DefaultAccountCR, DefaultAccountDiscount, Amount " & _
                " FROM     tblBI_Type  " & _
                " WHERE    Status ='Active' AND Type LIKE '%' + @Filter + '%' "
        SQL.FlushParams()
        SQL.AddParam("@Filter", "")
        SQL.ReadQuery(query)
        lvType.Items.Clear()
        While SQL.SQLDR.Read
            lvType.Items.Add(SQL.SQLDR("RecordID").ToString)
            lvType.Items(lvType.Items.Count - 1).SubItems.Add(SQL.SQLDR("Type").ToString)
            lvType.Items(lvType.Items.Count - 1).SubItems.Add(SQL.SQLDR("Description").ToString)
            lvType.Items(lvType.Items.Count - 1).SubItems.Add(SQL.SQLDR("Amount").ToString)
            lvType.Items(lvType.Items.Count - 1).SubItems.Add(SQL.SQLDR("DefaultAccountDR").ToString)
            lvType.Items(lvType.Items.Count - 1).SubItems.Add(GetAccntTitle(SQL.SQLDR("DefaultAccountDR").ToString))
            lvType.Items(lvType.Items.Count - 1).SubItems.Add(SQL.SQLDR("DefaultAccountCR").ToString)
            lvType.Items(lvType.Items.Count - 1).SubItems.Add(GetAccntTitle(SQL.SQLDR("DefaultAccountCR").ToString))
            lvType.Items(lvType.Items.Count - 1).SubItems.Add(SQL.SQLDR("DefaultAccountDiscount").ToString)
            lvType.Items(lvType.Items.Count - 1).SubItems.Add(GetAccntTitle(SQL.SQLDR("DefaultAccountDiscount").ToString))
        End While


        ' Toolstrip Buttons
        tsbNew.Enabled = True
        tsbEdit.Enabled = True
        tsbSave.Enabled = False
        tsbDelete.Enabled = True
        EnableControl(False)
    End Sub

    Public Function GetAccntTitle(ByVal AccntCode As String) As String
        Dim query As String
        query = " SELECT AccountTitle FROM tblCOA_Master WHERE AccountCode ='" & AccntCode & "'"
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("AccountTitle").ToString
        Else
            Return ""
        End If
    End Function

    Private Function RecordExist(ByVal Record As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblBI_Type WHERE Type =@Type AND Status ='Active' "
        SQL.FlushParams()
        SQL.AddParam("@Type", Record)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
        SQL.FlushParams()
    End Function

    Private Sub tsbDelete_Click(sender As System.Object, e As System.EventArgs) Handles tsbDelete.Click
        If Not AllowAccess("BI_MNT") Then
            msgRestricted()
        Else
            If RecordID <> "" Then
                If MsgBox("Are you sure you want to delete this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE tblBI_Type SET Status = 'Inactive' WHERE RecordID = @RecordID "
                        SQL.FlushParams()
                        SQL.AddParam("@RecordID", RecordID)
                        SQL.ExecNonQuery(deleteSQL)
                        Msg("Record deleted successfully", MsgBoxStyle.Information)
                        LoadAccnt()
                        tsbNew.PerformClick()

                        tsbNew.Enabled = True
                        tsbEdit.Enabled = False
                        tsbSave.Enabled = False
                        tsbDelete.Enabled = True

                        EnableControl(False)
                    Catch ex As System.Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
                    Finally
                        RecordActivity(UserID, moduleID, Me.Name.ToString, "DELETE", "RecordID", RecordID, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click

        ' Toolstrip Buttons
        If RecordID = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
        Else
            LoadAccnt()
            tsbEdit.Enabled = True
        End If
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
    End Sub

    Private Sub frmRFP_Type_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        ElseIf e.KeyCode = Keys.Escape Then
            If tsbClose.Enabled = True Then tsbClose.PerformClick()
        End If
    End Sub


    Private Sub txtAccntTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtAccntTitle.TextChanged

    End Sub
End Class