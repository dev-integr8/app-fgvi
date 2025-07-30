Public Class frmItemBarcodePrinting

    'Barcode Printing
    Dim Dir As String
    Dim App_Path As String

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub frmItemBarcodePrinting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CbSearchBy.SelectedItem = "Barcode"
        App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
    End Sub

    Private Sub LoadItems()
        Dim query As String
        Dim condition As String = ""
        Dim searchBy As String = ""
        If CbSearchBy.Text = "Item Name" Then
            condition = "AND ItemName LIKE '%" & TxtSearch.Text & "%'"
            searchBy = "ItemName"
        ElseIf CbSearchBy.Text = "Item Code" Then
            condition = "AND viewItem_Price.ItemCode LIKE '%" & TxtSearch.Text & "%'"
            searchBy = "viewItem_Price.ItemCode"
        ElseIf CbSearchBy.Text = "Barcode" Then
            condition = "AND Barcode LIKE '%" & TxtSearch.Text & "%'"
            searchBy = "Barcode"
        End If

        query = "SELECT TOP 99 viewItem_Price.ItemCode, Barcode, ItemName, viewItem_Price.UOM, UnitPrice " &
                "FROM viewItem_Price " &
                "WHERE  viewItem_Price.PriceStatus = 'Active'  AND viewItem_Price.ItemStatus = 'Active'  " & condition & " " &
                "ORDER BY CASE WHEN " & searchBy & " LIKE '" & TxtSearch.Text & "%' THEN 1 ELSE 2 END, " & searchBy

        SQL.ReadQuery(query)

        lvItem.Items.Clear()
        While SQL.SQLDR.Read
            lvItem.Items.Add(SQL.SQLDR("ItemCode").ToString)
            lvItem.Items(lvItem.Items.Count - 1).SubItems.Add(SQL.SQLDR("Barcode").ToString)
            lvItem.Items(lvItem.Items.Count - 1).SubItems.Add(SQL.SQLDR("ItemName").ToString)
            lvItem.Items(lvItem.Items.Count - 1).SubItems.Add(SQL.SQLDR("UOM").ToString)
            lvItem.Items(lvItem.Items.Count - 1).SubItems.Add(CDec(SQL.SQLDR("UnitPrice")).ToString("N2"))
        End While
    End Sub

    Private Sub frmItemBarcodePrinting_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            BtnBarcodeSearch.PerformClick()
        End If
    End Sub

    Private Sub BtnBarcodeSearch_Click(sender As Object, e As EventArgs) Handles BtnBarcodeSearch.Click
        LoadItems()
    End Sub

    Private Sub lvItem_Click(sender As Object, e As EventArgs) Handles lvItem.Click
        If lvItem.SelectedItems.Count > 0 Then
            txtItemCode.Text = lvItem.Items(lvItem.SelectedItems(0).Index).SubItems(chItemCode.Index).Text
            txtBarcode.Text = lvItem.Items(lvItem.SelectedItems(0).Index).SubItems(chBarcode.Index).Text()
            txtItemName.Text = lvItem.Items(lvItem.SelectedItems(0).Index).SubItems(chItemName.Index).Text()
            txtPrice.Text = lvItem.Items(lvItem.SelectedItems(0).Index).SubItems(chPrice.Index).Text()
            txtUOM.Text = lvItem.Items(lvItem.SelectedItems(0).Index).SubItems(chUOM.Index).Text()
        End If
    End Sub

    Private Sub tsbPrint_Click(sender As Object, e As EventArgs) Handles tsbPrint.Click
        Dim TW As System.IO.TextWriter
        Dim BR As String = Nothing

        Dir = App_Path.ToString.Replace("\Debug", "") & "\BarcodePrinting\"
        TW = System.IO.File.CreateText(Dir + "BarcodePrinting.txt")

        Dim ctr As Integer
        Do While ctr < txtQty.Value
            Dim Barcode As String
            Barcode = txtBarcode.Text.Trim & "\" & ctr

            BR = txtBarcode.Text.Trim & "|" & txtItemName.Text.Trim & "|" & txtPrice.Text.Trim
            TW.WriteLine(BR)
            ctr = ctr + 1
        Loop

        TW.Flush()
        TW.Close()
        Call cmdPrint()
    End Sub

    Private Sub cmdPrint()
        Dim process As New Process
        Try
            process.StartInfo.UseShellExecute = True
            process.StartInfo.WorkingDirectory = Dir
            process.StartInfo.Arguments = "Barcode.btw" + " /P/X"
            process.StartInfo.FileName = Dir + "\BarTender\BarTend.exe"
            process.Start()
            process.WaitForExit()
            process.Close()
            process.Dispose()
        Catch ex As Exception

        Finally

        End Try
    End Sub
End Class