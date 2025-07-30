Public Class Frm_ItemMaster
    Dim itemcode As String
    Dim moduleID As String = "ITM"

    Private Sub Frm_ItemMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tsbSearch.Enabled = True
        tsbNew.Enabled = True
        tsbEdit.Enabled = False
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
        EnableControl(False)
        LoadUOM()
    End Sub

    Private Sub tsbNew_Click(sender As Object, e As EventArgs) Handles tsbNew.Click
        If Not AllowAccess("ITM_ADD") Then
            msgRestricted()
        Else
            txtItemCode.Clear()
            itemcode = ""
            txtItemName.Clear()
            txItemType.Clear()
            cbUOM.Text = ""
            cbStatus.Text = ""
            txtInv_AccntCode.Clear()
            txtInv_AccntName.Clear()
            txtCOS_AccntCode.Clear()
            txtCOS_AccntName.Clear()
            txtSales_AccntCode.Clear()
            txtSales_AccntName.Clear()
            txtExpense_Code.Clear()
            txtExpense_Title.Clear()
            txtATD_Code.Clear()
            txtATD_Title.Clear()
            txtSupCode.Clear()
            txtSupName.Clear()
            txtAmount.Text = "0.00"
            EnableControl(True)

            LoadUOM()
            LoadStatus()

            'cbStatus.SelectedIndex = 1

            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbClose.Enabled = True
            tsbExit.Enabled = False
        End If
    End Sub


    Private Sub EnableControl(ByVal Value As Boolean)
        txtItemCode.Enabled = Value
        txtItemName.Enabled = Value
        txItemType.Enabled = Value
        txtInv_AccntName.Enabled = Value
        txtSales_AccntName.Enabled = Value
        txtCOS_AccntName.Enabled = Value
        txtExpense_Title.Enabled = Value
        txtATD_Title.Enabled = Value
        txtAmount.Enabled = Value
        cbUOM.Enabled = Value
        cbStatus.Enabled = Value
        txtSupName.Enabled = Value
    End Sub


    Private Function validateData() As Boolean
        Dim value As Boolean = True
        If txtItemCode.Text = "" Then
            MsgBox("Please enter Item Code!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf txtItemName.Text = "" Then
            MsgBox("Please enter Item Name!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf txtInv_AccntCode.Text = "" Or txtCOS_AccntCode.Text = "" Or txtSales_AccntCode.Text = "" Then
            MsgBox("Please enter Default Account Title!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf cbUOM.Text = "" Then
            MsgBox("Please enter UOM!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf itemcode = "" And IfExist(txtItemCode.Text) Then
            MsgBox("Item Code exist!", MsgBoxStyle.Exclamation)
            Return False
            'ElseIf cbStatus.Text = "" Then
            '    MsgBox("Please enter Status!", MsgBoxStyle.Exclamation)
            '    Return False
        End If
        Return value
    End Function

    Private Function IfExist(ByVal ID As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblItem_Master WHERE ItemCode ='" & ID & "'  "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        If validateData() Then
            If itemcode = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    SaveItem()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    itemcode = txtItemCode.Text
                    LoadItem(itemcode)
                End If
            Else
                Dim Validated As Boolean = True
                If itemcode <> txtItemCode.Text Then
                    If RecordExist(txtItemCode.Text) Then
                        Validated = False
                    Else
                        Validated = True
                    End If
                End If

                If Validated Then
                    If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                        UpdateItem()
                        Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                        itemcode = txtItemCode.Text
                        LoadItem(itemcode)

                    End If
                Else
                    Msg("Item Code is already in used! Please change Item Code", MsgBoxStyle.Exclamation)
                    txtItemCode.Text = itemcode
                    txtItemName.SelectAll()
                End If
            End If
        End If
    End Sub

    Private Sub SaveItem()
        'activityStatus = True
        Dim insertSQL As String
        insertSQL = " INSERT INTO " & _
                    " tblItem_Master   (ItemCode, ItemName, ItemDescription, ItemType, ItemUOM, ID_SC, AD_Expense, AD_COS, AD_Sales, AD_Inv, AD_ATD, PD_Supplier)  " & _
                    " VALUES                (@ItemCode, @ItemName, @ItemDescription, @ItemType, @ItemUOM, @ID_SC, @AD_Expense, @AD_COS, @AD_Sales, @AD_Inv, @AD_ATD, @PD_Supplier) "

        SQL.FlushParams()
        SQL.AddParam("@ItemCode", txtItemCode.Text)
        SQL.AddParam("@ItemName", txtItemName.Text)
        SQL.AddParam("@ItemDescription", txtItemName.Text)
        SQL.AddParam("@ItemType", txItemType.Text)
        SQL.AddParam("@ItemUOM", cbUOM.Text)
        SQL.AddParam("@ID_SC", IIf(txtAmount.Text = "", 0, CDec(txtAmount.Text)))
        SQL.AddParam("@AD_Inv", txtInv_AccntCode.Text)
        SQL.AddParam("@AD_COS", txtCOS_AccntCode.Text)
        SQL.AddParam("@AD_Sales", txtSales_AccntCode.Text)
        SQL.AddParam("@AD_Expense", txtExpense_Code.Text)
        SQL.AddParam("@AD_ATD", txtATD_Code.Text)
        SQL.AddParam("@PD_Supplier", txtSupCode.Text)

        SQL.ExecNonQuery(insertSQL)
    End Sub

    Private Sub LoadItem(ByVal Code As String)
        Dim query As String
        query = "  SELECT    ItemCode, ItemName, ItemDescription, ItemType , ItemUOM, Status, ISNULL(ID_SC,0) AS ID_SC, AD_Expense, AD_COS, AD_Sales, AD_Inv, AD_ATD, " & _
                "           PD_Supplier" & _
                "  FROM     tblItem_Master  " & _
                 " WHERE    ItemCode = '" & Code & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtItemCode.Text = SQL.SQLDR("ItemCode").ToString
            txtItemName.Text = SQL.SQLDR("ItemName").ToString
            txtItemName.Text = SQL.SQLDR("ItemDescription").ToString
            txItemType.Text = SQL.SQLDR("ItemType").ToString
            cbUOM.Text = SQL.SQLDR("ItemUOM").ToString
            cbStatus.Text = SQL.SQLDR("Status").ToString
            txtAmount.Text = CDec(SQL.SQLDR("ID_SC")).ToString("N2")
            txtInv_AccntCode.Text = SQL.SQLDR("AD_Inv").ToString
            txtCOS_AccntCode.Text = SQL.SQLDR("AD_COS").ToString
            txtSales_AccntCode.Text = SQL.SQLDR("AD_Sales").ToString
            txtExpense_Code.Text = SQL.SQLDR("AD_Expense").ToString
            txtATD_Code.Text = SQL.SQLDR("AD_ATD").ToString
            txtSupCode.Text = SQL.SQLDR("PD_Supplier").ToString
            txtSupName.Text = GetVCEName(txtSupCode.Text)
            txtInv_AccntName.Text = GetAccntTitle(txtInv_AccntCode.Text)
            txtCOS_AccntName.Text = GetAccntTitle(txtCOS_AccntCode.Text)
            txtSales_AccntName.Text = GetAccntTitle(txtSales_AccntCode.Text)
            txtExpense_Title.Text = GetAccntTitle(txtExpense_Code.Text)
            txtATD_Title.Text = GetAccntTitle(txtATD_Code.Text)
        End If
        ' TOOLSTRIP BUTTONS
        tsbSearch.Enabled = True
        tsbNew.Enabled = True
        tsbEdit.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
        EnableControl(False)
    End Sub

    Private Sub LoadType()
        'Dim query As String
        'query = " SELECT DISTINCT ItemType FROM tblItem_Master "
        'SQL.ReadQuery(query)
        'cbUOM.Items.Clear()
        'While SQL.SQLDR.Read
        '    cbUOM.Items.Add(SQL.SQLDR("ItemType").ToString)
        'End While
    End Sub
    Private Sub LoadStatus()
        Dim query As String
        query = " SELECT DISTINCT Status FROM tblItem_Master "
        SQL.ReadQuery(query)
        cbStatus.Items.Clear()
        While SQL.SQLDR.Read
            cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
        End While
    End Sub

    Private Sub LoadUOM()
        'Dim query As String
        'query = " SELECT DISTINCT ItemUOM FROM tblItem_Master WHERE ItemUOM <> null "
        'SQL.ReadQuery(query)
        'If SQL.SQLDR.Read Then
        '    cbUOM.Items.Add("ItemUOM").ToString()
        'End If

        Dim query As String
        query = " SELECT DISTINCT ItemUOM FROM tblItem_Master "
        SQL.ReadQuery(query)
        cbUOM.Items.Clear()
        While SQL.SQLDR.Read
            cbUOM.Items.Add(SQL.SQLDR("ItemUOM").ToString)
        End While
    End Sub

    Private Sub UpdateItem()
        activityStatus = True
        Dim updateSQL As String

        updateSQL = " UPDATE tblItem_Master" & _
                    " SET    ItemCode = @ItemCode, ItemName = @ItemName, ItemDescription = @ItemDescription, ItemType = @ItemType , ItemUOM = @ItemUOM," & _
                    " ID_SC = @ID_SC, AD_Expense = @AD_Expense, AD_COS = @AD_COS, AD_Sales = @AD_Sales, AD_Inv = @AD_Inv, AD_ATD = @AD_ATD, PD_Supplier = @PD_Supplier" & _
                    " WHERE  ItemCode = @ItemCode "
        SQL.FlushParams()
        SQL.AddParam("@ItemCode", txtItemCode.Text)
        SQL.AddParam("@ItemName", txtItemName.Text)
        SQL.AddParam("@ItemDescription", txtItemName.Text)
        SQL.AddParam("@ItemType", txItemType.Text)
        SQL.AddParam("@ItemUOM", cbUOM.Text)
        SQL.AddParam("@ID_SC", IIf(txtAmount.Text = "", 0, CDec(txtAmount.Text)))
        SQL.AddParam("@AD_Inv", txtInv_AccntCode.Text)
        SQL.AddParam("@AD_COS", txtCOS_AccntCode.Text)
        SQL.AddParam("@AD_Sales", txtSales_AccntCode.Text)
        SQL.AddParam("@AD_Expense", txtExpense_Code.Text)
        SQL.AddParam("@AD_ATD", txtATD_Code.Text)
        SQL.AddParam("@PD_Supplier", txtSupCode.Text)
        SQL.ExecNonQuery(updateSQL)
    End Sub

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub
    Private Function RecordExist(ByVal code As String) As Boolean
        Dim query As String
        query = " SELECT * from tblItem_Master where ItemCode = '" & code & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False

        End If
    End Function

    Private Sub tsbSearch_Click(sender As Object, e As EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("ITM_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmItem_Search
            f.ShowDialog()
            If f.ItemCode <> "" Then
                itemcode = f.ItemCode
            End If
            LoadItem(itemcode)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbEdit_Click(sender As Object, e As EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("ITM_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)

            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbClose.Enabled = True
            tsbExit.Enabled = False
        End If
    End Sub

    Private Sub tsbClose_Click(sender As Object, e As EventArgs) Handles tsbClose.Click
        If itemcode <> "" Then
            tsbNew.PerformClick()
            EnableControl(False)
            tsbEdit.Enabled = False
        Else
            LoadItem(itemcode)
            tsbEdit.Enabled = True
        End If
        tsbSearch.Enabled = True
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
        LoadItem(itemcode)
    End Sub

    Private Sub txtAccntName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtInv_AccntName.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtInv_AccntName.Text)
                txtInv_AccntCode.Text = f.accountcode
                txtInv_AccntName.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        End Try
    End Sub


    Private Sub txtAccntName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtInv_AccntName.TextChanged

    End Sub

    Private Sub txtCOS_AccntName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtCOS_AccntName.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtCOS_AccntName.Text)
                txtCOS_AccntCode.Text = f.accountcode
                txtCOS_AccntName.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        End Try
    End Sub

    Private Sub txtCOS_AccntName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCOS_AccntName.TextChanged

    End Sub

    Private Sub txtSales_AccntName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSales_AccntName.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtSales_AccntName.Text)
                txtSales_AccntCode.Text = f.accountcode
                txtSales_AccntName.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        End Try
    End Sub

    Private Sub txtSales_AccntName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSales_AccntName.TextChanged

    End Sub

    Private Sub txtExpense_Title_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtExpense_Title.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtExpense_Title.Text)
                txtExpense_Code.Text = f.accountcode
                txtExpense_Title.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        End Try
    End Sub

    Private Sub txtExpense_Title_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtExpense_Title.TextChanged

    End Sub

    Private Sub txtATD_Title_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtATD_Title.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtATD_Title.Text)
                txtATD_Code.Text = f.accountcode
                txtATD_Title.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        End Try
    End Sub

    Private Sub txtATD_Title_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtATD_Title.TextChanged

    End Sub

    Private Sub txtSupName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSupName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.txtFilter.Text = txtSupName.Text
            f.ShowDialog()
            txtSupCode.Text = f.VCECode
            txtSupName.Text = f.VCEName
            f.Dispose()
        End If
    End Sub


    Private Sub txtSupName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSupName.TextChanged

    End Sub
End Class