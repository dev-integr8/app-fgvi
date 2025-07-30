Public Class frmBOMSelect
    Dim ModuleID As String = ""
    Dim BOMType As String = ""

    Public Overloads Function ShowDialog(ByVal modID As String, ByVal Type As String) As Boolean
        ModuleID = modID
        BOMType = Type
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmBOMSelect_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadItem()
    End Sub
    Private Sub LoadItem()
        If BOMType = "SFG" Then
            Dim query As String
            query = " SELECT DISTINCT ItemName FROM tblBOM_SFG  " & _
                    " LEFT JOIN " & _
                    " tblItem_Master ON " & _
                    " tblItem_Master.ItemCode = tblBOM_SFG.ItemCode " & _
                    " WHERE tblBOM_SFG.Status ='Active' "
            SQL.ReadQuery(query)
            cbItemName.Items.Clear()
            While SQL.SQLDR.Read
                cbItemName.Items.Add(SQL.SQLDR("ItemName").ToString)
            End While
        ElseIf BOMType = "FG" Then
            Dim query As String
            query = " SELECT DISTINCT ItemName FROM tblBOM_FG  " & _
                    " LEFT JOIN " & _
                    " tblItem_Master ON " & _
                    " tblItem_Master.ItemCode = tblBOM_FG.ItemCode " & _
                    " WHERE tblBOM_FG.Status ='Active' "
            SQL.ReadQuery(query)
            cbItemName.Items.Clear()
            While SQL.SQLDR.Read
                cbItemName.Items.Add(SQL.SQLDR("ItemName").ToString)
            End While
        End If

    End Sub

    Private Sub LoadBOM(ByVal ItemCode As String)
        If BOMType = "SFG" Then
            Dim query As String
            query = " SELECT BOM_Code FROM tblBOM_SFG WHERE ItemCode = '" & ItemCode & "' AND Status ='Active' "
            SQL.ReadQuery(query)
            cbBOM.Items.Clear()
            While SQL.SQLDR.Read
                cbBOM.Items.Add(SQL.SQLDR("BOM_Code").ToString)
            End While
        ElseIf BOMType = "FG" Then
            Dim query As String
            query = " SELECT BOM_Code FROM tblBOM_FG WHERE ItemCode = '" & ItemCode & "' AND Status ='Active' "
            SQL.ReadQuery(query)
            cbBOM.Items.Clear()
            While SQL.SQLDR.Read
                cbBOM.Items.Add(SQL.SQLDR("BOM_Code").ToString)
            End While
        End If

    End Sub

    Private Sub btnAddMats_Click(sender As System.Object, e As System.EventArgs) Handles btnAddMats.Click
        Me.Close()
    End Sub

    Private Sub cbBOM_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbBOM.SelectedIndexChanged
        Dim Code As String
        Code = cbBOM.SelectedItem
        If BOMType = "SFG" Then
            Dim query As String
            query = " SELECT BOM_Code, tblBOM_SFG.ItemCode, ItemName, UOM, QTY FROM tblBOM_SFG " & _
                    " INNER JOIN tblItem_Master ON " & _
                    " tblItem_Master.ItemCode = tblBOM_SFG.ItemCode " & _
                    " WHERE BOM_Code = '" & Code & "'"
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                txtItemCode.Text = SQL.SQLDR("ItemCode")
                cbItemName.SelectedItem = SQL.SQLDR("ItemName")
                txtQTY.Text = SQL.SQLDR("QTY")
                txtUOM.Text = SQL.SQLDR("UOM")
            End If

        ElseIf BOMType = "FG" Then
            Dim query As String
            query = " SELECT BOM_Code, tblBOM_FG.ItemCode, ItemName, UOM, QTY FROM tblBOM_FG " & _
                    " INNER JOIN tblItem_Master ON " & _
                    " tblItem_Master.ItemCode = tblBOM_FG.ItemCode " & _
                    " WHERE BOM_Code = '" & Code & "'"
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                txtItemCode.Text = SQL.SQLDR("ItemCode")
                cbItemName.SelectedItem = SQL.SQLDR("ItemName")
                txtQTY.Text = SQL.SQLDR("QTY")
                txtUOM.Text = SQL.SQLDR("UOM")
            End If
        End If
    End Sub

    Private Sub cbItemName_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbItemName.SelectedIndexChanged
        Dim itemcode As String
        If cbItemName.SelectedIndex <> -1 Then
            If BOMType = "SFG" Then
                itemcode = GetItemCodeSF(cbItemName.SelectedItem)
                LoadBOM(itemcode)
            ElseIf BOMType = "FG" Then
                itemcode = GetItemCode(cbItemName.SelectedItem)
                LoadBOM(itemcode)
            End If
        End If
    End Sub

    Private Function GetItemCodeSF(ByVal ItemName As String) As String
        Dim query As String
        query = " SELECT ItemCode FROM tblItem_Master WHERE ItemName ='" & ItemName & "' AND ItemCode like 'SF%'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("ItemCode").ToString
        Else
            Return ""
        End If
    End Function
End Class