Public Class frmCurrency
    Public a As Integer
    Dim moduleID As String = "CUR"
    Dim code As String

    Private Sub frmCurrency_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetDatabase()
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
        query = " SELECT	Code, Description, Symbol " & _
                " FROM	    tblCurrency " & _
                " WHERE     tblCurrency.Status = 'Active' "
        SQL.ReadQuery(query)
        lvList.Items.Clear()
        If SQL.SQLDR.HasRows Then
            While SQL.SQLDR.Read
                lvList.Items.Add(SQL.SQLDR("Code"))
                lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Description").ToString)
                lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Symbol").ToString)
            End While
        Else
            LoadDefaultItem()
            LoadItem()
        End If
        

        ' Toolstrip Buttons
        tsbNew.Enabled = True
        tsbEdit.Enabled = True
        tsbSave.Enabled = False
        tsbDelete.Enabled = True
        tsbClose.Enabled = False
        tsbExit.Enabled = True
        EnableControl(False)
    End Sub

    Public Sub LoadDefaultItem()
        Dim insertSQL As String
        insertSQL = " INSERT INTO " & _
                 " tblCurrency(Code, Description, Symbol, Status, DateCreated, WhoCreated)" & _
                 " VALUES (@Code, @Description, @Symbol, @Status, GETDATE(), @WhoCreated)"
        SQL.AddParam("@Code", "PHP")
        SQL.AddParam("@Description", "Philippine Peso")
        SQL.AddParam("@Symbol", "")
        SQL.AddParam("@WhoCreated", "")
        SQL.AddParam("@Status", "Active")
        SQL.ExecNonQuery(insertSQL)
    End Sub

    Protected Sub ClearText()
        txtCode.Text = ""
        txtDescription.Text = ""
        txtSymbol.Text = ""
    End Sub

    Protected Sub EnableControl(ByVal Value As Boolean)
        txtCode.ReadOnly = Not Value
        txtDescription.ReadOnly = Not Value
        txtSymbol.ReadOnly = Not Value
    End Sub

    Private Sub lstBank_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvList.MouseClick
        If lvList.SelectedItems.Count > 0 Then
            txtCode.Text = lvList.Items(lvList.SelectedItems(0).Index).SubItems(chCode.Index).Text()
            txtDescription.Text = lvList.Items(lvList.SelectedItems(0).Index).SubItems(chDesc.Index).Text()
            txtSymbol.Text = lvList.Items(lvList.SelectedItems(0).Index).SubItems(chSymbol.Index).Text()
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

    Private Sub UpdateCurrency()
        Dim updateSQL As String
        updateSQL = " UPDATE   tblCurrency " & _
                    " SET      Description = @Description,  " & _
                    "          Symbol = @Symbol,  " & _
                    "          WhoModified = @WhoModified, " & _
                    "          DateModified = GETDATE() " & _
                    " WHERE    Code = @Code "
        SQL.AddParam("@Code", txtCode.Text)
        SQL.AddParam("@Description", txtDescription.Text)
        SQL.AddParam("@Symbol", txtSymbol.Text)
        SQL.AddParam("@WhoModified", UserID)
        SQL.ExecNonQuery(updateSQL)
    End Sub

    Private Sub SaveCurrency()
        Dim insertSQL As String
        insertSQL = " INSERT INTO " & _
                    " tblCurrency(Code, Description, Symbol, Status, DateCreated, WhoCreated)" & _
                    " VALUES (@Code, @Description, @Symbol, @Status, GETDATE(), @WhoCreated)"
        SQL.AddParam("@Code", txtCode.Text)
        SQL.AddParam("@Description", txtDescription.Text)
        SQL.AddParam("@Symbol", txtSymbol.Text)
        SQL.AddParam("@WhoCreated", UserID)
        SQL.AddParam("@Status", "Active")
        SQL.ExecNonQuery(insertSQL)
    End Sub


    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("CUR_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)
            txtCode.Enabled = False ' CODE SHOULD NOT BE EDITABLE
            ' Toolstrip Buttons
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbDelete.Enabled = False
            tsbClose.Enabled = True
            tsbExit.Enabled = False
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If txtCode.Text = "" Then
            MsgBox("Please enter currency code!", MsgBoxStyle.Exclamation)
        ElseIf txtDescription.Text = "" Then
            MsgBox("Please enter currency description!", MsgBoxStyle.Exclamation)
        ElseIf code = "" Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                SaveCurrency()
                Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                LoadItem()
                ClearText()
            End If
        Else
            If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                UpdateCurrency()
                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                LoadItem()
                ClearText()
            End If
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click

        If Not AllowAccess("CUR_ADD") Then
            msgRestricted()
        Else
            ClearText()
            EnableControl(True)
            txtCode.Enabled = True
            code = ""

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
        If Not AllowAccess("CUR_DEL") Then
            msgRestricted()
        Else
            If code <> "" Then
                If MsgBox("Are you sure you want to delete this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then

                    Dim updateSQL As String
                    updateSQL = " UPDATE   tblCurrency " & _
                                " SET      Status = 'Inactive' " & _
                                " WHERE    Code = @Code "
                    SQL.AddParam("@Code", code)
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
        If code = "" Then
            tsbNew.PerformClick()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbDelete.Enabled = False
        Else
            code = ""
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

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub
End Class