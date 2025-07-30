Public Class frmCashFlow_Add
    Dim AccountCode As String
    Dim AccountType As String
    Dim SubCategory As String

    Public Overloads Function ShowDialog(ByVal AccntCode As String) As Boolean
        AccountCode = AccntCode

        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmCashFlow_Add_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAccount(AccountCode)
        LoadAccountGroup(AccountType)

    End Sub

    Private Sub LoadAccount(ByVal Account As String)
        Dim query As String
        query = " SELECT * FROM tblCOA_Master  " &
                "  WHERE Status = 'Active' AND AccountCode = '" & Account & "' "
        SQL.FlushParams()
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            AccountType = SQL.SQLDR("AccountCategory").ToString
            txtCode.Text = Account
            txtDescription.Text = SQL.SQLDR("AccountTitle").ToString
            If Not IsDBNull(SQL.SQLDR("SubCategory")) Then
                cbDescCategory.SelectedItem = SQL.SQLDR("SubCategory").ToString()
                SubCategory = cbDescCategory.SelectedItem
            Else
                cbDescCategory.SelectedItem = Nothing
                SubCategory = Nothing
            End If
            If Not IsDBNull(SQL.SQLDR("SCF_Group")) Then
                cbCFGroup.SelectedItem = SQL.SQLDR("SCF_Group").ToString()
            Else
                cbCFGroup.SelectedItem = Nothing
            End If
        End If
    End Sub

    Private Sub LoadAccountGroup(ByVal strType As String)
        Dim query As String = " SELECT  AccountTitle " &
                              " FROM    tblCOA_Master " &
                              " WHERE   Status = @Status AND AccountGroup NOT IN ('AccountType', 'SubAccount') AND AccountCategory = @Type  "

        SQL.FlushParams()
        SQL.AddParam("@Status", "Active")
        SQL.AddParam("@Type", strType)
        SQL.ReadQuery(query)

        cbDescCategory.Items.Clear()

        While SQL.SQLDR.Read()
            cbDescCategory.Items.Add(SQL.SQLDR("AccountTitle").ToString())
        End While

        If Not IsDBNull(SubCategory) Then
            cbDescCategory.SelectedItem = SubCategory
        Else
            cbDescCategory.SelectedItem = Nothing
        End If

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSaveClose_Click(sender As Object, e As EventArgs) Handles btnSaveClose.Click
        UpdateAccount(AccountCode)
    End Sub

    Private Sub UpdateAccount(ByVal AccountCode As String)
        Dim updatequery As String
        updatequery = " UPDATE tblCOA_Master " &
                                   " SET SubCategory = @SubCategory, SCF_Group = @SCF_Group, " &
                                   " DateModified = @DateModified " &
                                   " WHERE AccountCode = @AccountCode"
        SQL.FlushParams()
        SQL.AddParam("@AccountCode", AccountCode)
        SQL.AddParam("@SubCategory", cbDescCategory.SelectedItem)
        SQL.AddParam("@SCF_Group", cbCFGroup.SelectedItem)
        SQL.AddParam("@DateModified", Now.Date)
        SQL.ExecNonQuery(updatequery)

        Me.Close()

    End Sub
End Class