Public Class frmMasterfile_Terms
    Public a As Integer
    Dim idx As Integer
    Dim moduleID As String = "MT"

    Private Sub frmBanklist_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetDatabase()
        cbDateMode.Items.Add("Day")
        cbDateMode.Items.Add("Month")
        cbDateMode.Items.Add("Year")
        LoadItem()
        tsbNew.Enabled = True
        tsbEdit.Enabled = False
        tsbSave.Enabled = False
        tsbDelete.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
        EnableControl(False)
    End Sub

    Protected Sub LoadItem()
        Dim query As String
        query = " SELECT	RecordID, Description, Days, DateMode  " & _
                " FROM	    tblTerms " & _
                " WHERE     tblTerms.Status = 'Active' "
        SQL.ReadQuery(query)
        lvBank.Items.Clear()
        While SQL.SQLDR.Read
            lvBank.Items.Add(SQL.SQLDR("RecordID"))
            lvBank.Items(lvBank.Items.Count - 1).SubItems.Add(SQL.SQLDR("Description").ToString)
            lvBank.Items(lvBank.Items.Count - 1).SubItems.Add(SQL.SQLDR("Days").ToString)
            lvBank.Items(lvBank.Items.Count - 1).SubItems.Add(SQL.SQLDR("DateMode").ToString)
        End While


        ' Toolstrip Buttons
        tsbNew.Enabled = True
        tsbEdit.Enabled = True
        tsbSave.Enabled = False
        tsbDelete.Enabled = True
        tsbClose.Enabled = False
        tsbExit.Enabled = True
        EnableControl(False)
    End Sub

    Protected Sub ClearText()
        txtDescription.Text = ""
        txtPeriod.Text = ""
        cbDateMode.SelectedIndex = 0
    End Sub

    Protected Sub EnableControl(ByVal Value As Boolean)
        txtDescription.ReadOnly = Not Value
        txtPeriod.ReadOnly = Not Value
    End Sub

    Private Sub lstBank_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvBank.MouseClick
        If lvBank.SelectedItems.Count > 0 Then
            txtDescription.Text = lvBank.Items(lvBank.SelectedItems(0).Index).SubItems(chDescription.Index).Text()
            txtPeriod.Text = lvBank.Items(lvBank.SelectedItems(0).Index).SubItems(chPeriod.Index).Text()
            cbDateMode.SelectedItem = lvBank.Items(lvBank.SelectedItems(0).Index).SubItems(chTermsMode.Index).Text()
            idx = lvBank.Items(lvBank.SelectedItems(0).Index).SubItems(chIDX.Index).Text()
            EnableControl(True)


            ' Toolstrip Buttons
            tsbNew.Enabled = True
            tsbEdit.Enabled = True
            tsbSave.Enabled = False
            tsbDelete.Enabled = True
            tsbClose.Enabled = False
            tsbExit.Enabled = True
            EnableControl(False)
        End If
    End Sub

    Private Sub UpdateBank()

        Dim updateSQL As String
        updateSQL = " UPDATE   tblTerms " & _
                    " SET      Terms = @Terms, " & _
                    "          Description = @Description,  " & _
                    "          Days = @Days,  " & _
                    "          DateMode = @DateMode, " & _
                    "          DateModified = GETDATE(), " & _
                    "          WhoModified = @WhoModified " & _
                    " WHERE    RecordID = @RecordID "
        SQL.AddParam("@Terms", txtDescription.Text)
        SQL.AddParam("@Description", txtDescription.Text)
        SQL.AddParam("@Days", txtPeriod.Text)
        SQL.AddParam("@DateMode", cbDateMode.SelectedItem)
        SQL.AddParam("@WhoModified", UserID)
        SQL.AddParam("@RecordID", idx)
        SQL.ExecNonQuery(updateSQL)

    End Sub

    Private Sub SaveBank()

        Dim insertSQL As String
        insertSQL = " INSERT INTO " & _
                    " tblTerms(Terms, Description, Days, DateMode, Status, WhoCreated)" & _
                    " VALUES (@Terms, @Description, @Days, @DateMode, @Status, @WhoCreated)"
        SQL.AddParam("@Terms", txtDescription.Text)
        SQL.AddParam("@Description", txtDescription.Text)
        SQL.AddParam("@Days", txtPeriod.Text)
        SQL.AddParam("@DateMode", cbDateMode.SelectedItem)
        SQL.AddParam("@WhoCreated", UserID)
        SQL.AddParam("@Status", "Active")
        SQL.ExecNonQuery(insertSQL)

    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("MT_EDIT") Then
            msgRestricted()
        Else
            If idx = 0 Then
                Msg("Please Select Terms!", MsgBoxStyle.Information)
            Else
                EnableControl(True)

                ' Toolstrip Buttons
                tsbNew.Enabled = False
                tsbEdit.Enabled = False
                tsbSave.Enabled = True
                tsbDelete.Enabled = False
                tsbClose.Enabled = True
                tsbExit.Enabled = False
            End If
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If txtDescription.Text = "" Then
            MsgBox("Please enter terms!", MsgBoxStyle.Exclamation)
        ElseIf cbDateMode.SelectedIndex = -1 Then
            MsgBox("Please select Date Mode!", MsgBoxStyle.Exclamation)
        ElseIf idx = 0 Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                SaveBank()
                Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                LoadItem()
                ClearText()
            End If
        Else
            If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                UpdateBank()
                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                LoadItem()
                ClearText()
            End If
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click

        If Not AllowAccess("MT_ADD") Then
            msgRestricted()
        Else
            ClearText()
            EnableControl(True)
            txtDescription.Enabled = True
            idx = 0

            ' Toolstrip Buttons
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbDelete.Enabled = False
            tsbClose.Enabled = True
            tsbExit.Enabled = False
            EnableControl(True)

        End If
    End Sub

    Private Sub tsbDelete_Click(sender As System.Object, e As System.EventArgs) Handles tsbDelete.Click
        If Not AllowAccess("MT_DEL") Then
            msgRestricted()
        Else
            If idx <> 0 Then
                If MsgBox("Are you sure you want to delete this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then

                    Dim updateSQL As String
                    updateSQL = " UPDATE   tblTerms " & _
                                " SET      Status = 'Inactive' " & _
                                " WHERE    RecordID = @RecordID "
                    SQL.AddParam("@RecordID", idx)
                    SQL.ExecNonQuery(updateSQL)
                    MsgBox("Removed Succesfully", MsgBoxStyle.Information)
                    LoadItem()

                    tsbNew.PerformClick()
                    tsbNew.Enabled = True
                    tsbEdit.Enabled = False
                    tsbSave.Enabled = False
                    tsbDelete.Enabled = False
                    tsbClose.Enabled = False
                    tsbExit.Enabled = True
                    EnableControl(False)

                End If
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click

        ' Toolstrip Buttons
        If idx = 0 Then
            tsbNew.PerformClick()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbDelete.Enabled = False
        Else
            idx = 0
            LoadItem()
            ClearText()
            tsbEdit.Enabled = True
            tsbDelete.Enabled = True

        End If
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
    End Sub

    Private Sub lvBank_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lvBank.SelectedIndexChanged

    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub
End Class