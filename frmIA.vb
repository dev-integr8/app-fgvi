Public Class frmIA
    Dim ModuleID As String = "IA"
    Dim type As String = ""
    Dim showCost As Boolean = False
    Public DateFrom, DateTo As Date
    Public WHSE As String = "''"
    Public Item As String = "''"
    Public ItemOwner As String = "''"
    Dim insertselect As String = ""

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click

        frmIA_Filter.ShowDialog()
        LoadCount()
    End Sub

    Private Sub LoadCount()
        Dim query As String
        Dim filter As String = ""
        If Item = Nothing Then Item = "''"
        If WHSE = Nothing Then WHSE = "''"
        If ItemOwner = Nothing Then ItemOwner = "''"
        If ItemOwner = "''" Then
            filter = " WHERE '' = ''"
        Else
            filter = " WHERE ItemOwner ='" & ItemOwner & "' "
        End If

        If showCost = True Then
            query = " SELECT ItemDetail.ItemCode, ItemName, UPPER(ISNULL(ID_UOM,ItemUOM)) AS UOM,  " & vbCrLf & _
                    " 		 CAST([BB] / ISNULL(viewItem_UOM.QTY,1) AS decimal(18,2)) AS [BB] ,  " & vbCrLf & _
                    " 		 CAST([IN] / ISNULL(viewItem_UOM.QTY,1) AS decimal(18,2)) AS [IN] ,  " & vbCrLf & _
                    " 		 CAST([OUT] / ISNULL(viewItem_UOM.QTY,1) AS decimal(18,2)) AS [OUT] ,  " & vbCrLf & _
                    " 		 CAST([EB] / ISNULL(viewItem_UOM.QTY,1) AS decimal(18,2)) AS [EB],  " & vbCrLf & _
                    " 		 CAST(AverageCost / ISNULL(viewItem_UOM.QTY,1) AS decimal(18,2)) AS AverageCost, ID_SC AS [Standard Cost], ItemOwner " & vbCrLf & _
                    " FROM " & vbCrLf & _
                    " ( " & vbCrLf & _
                    " 	SELECT	ItemCode, ItemName, Barcode, ItemType, ItemCategory, ItemOwner, ItemGroup, ItemUOM, ItemWeight, ID_UOM, ID_SC  " & vbCrLf & _
                    " 	FROM tblItem_Master " & vbCrLf & _
                    " 	WHERE isInventory = 1 " & vbCrLf & _
                    " ) AS ItemDetail " & vbCrLf & _
                    " INNER JOIN " & vbCrLf & _
                    " ( " & vbCrLf & _
                    " SELECT ItemCode, SUM(ISNULL([BB],0)) AS [BB], SUM(ISNULL([IN],0)) AS [IN], SUM(ISNULL([OUT],0)) AS [OUT], " & vbCrLf & _
                    " 		  SUM(ISNULL([BB],0)) + SUM(ISNULL([IN],0)) - SUM(ISNULL([OUT],0)) AS [EB] " & vbCrLf & _
                    " 	FROm " & vbCrLf & _
                    " 	( " & vbCrLf & _
                    " 		SELECT ItemCode, MovementType, SUM(TransQTY) AS QTY  " & vbCrLf & _
                    " 		FROM tblInventory " & vbCrLf & _
                    " 		WHERE  PostDate BETWEEN '" & DateFrom & "' AND '" & DateTo & "' " & vbCrLf & _
                    "       AND    WHSE IN (" & WHSE & ") " & vbCrLf & _
                    " 		GROUP BY ItemCode, MovementType " & vbCrLf & _
                    " 	 UNION ALL " & vbCrLf & _
                    " 		SELECT ItemCode, 'BB',  SUM(CASE WHEN MovementType ='IN' THEN TransQTY ELSE TransQTY * -1 END) AS QTY  " & vbCrLf & _
                    " 		FROM tblInventory " & vbCrLf & _
                    " 		WHERE  PostDate < '" & DateFrom & "'  " & vbCrLf & _
                    "       AND    WHSE IN (" & WHSE & ") " & vbCrLf & _
                    " 		GROUP BY ItemCode " & vbCrLf & _
                    " 	) AS A " & vbCrLf & _
                    " 	PIVOT  " & vbCrLf & _
                    " 	(  " & vbCrLf & _
                    " 		SUM(QTY) " & vbCrLf & _
                    " 		FOR [MovementType] IN ([BB],[IN],[OUT]) " & vbCrLf & _
                    " 	) AS pvt " & vbCrLf & _
                    " 	GROUP BY ItemCode " & vbCrLf & _
                    " ) AS ItemCount " & vbCrLf & _
                    " ON	ItemDetail.Itemcode = ItemCount.ItemCode " & vbCrLf & _
                    " LEFT JOIN  " & vbCrLf & _
                    " ( " & vbCrLf & _
                    "  SELECT    ItemCode, AverageCost, ROW_NUMBER() OVER (PARTITION BY ItemCode ORDER BY Postdate DESC, DateCreated DESC) AS RowID " & vbCrLf & _
                    "  FROM      tblInventory  " & vbCrLf & _
                    "  ) AS ItemCost " & vbCrLf & _
                    " ON	ItemDetail.Itemcode = ItemCost.ItemCode " & vbCrLf & _
                    " AND RowID = 1 " & vbCrLf & _
                    " LEFT JOIN viewITEM_UOM " & vbCrLf & _
                    " ON ItemDetail.ItemCode = viewItem_UOM.GroupCode " & vbCrLf & _
                    " AND ItemDetail.ID_UOM = viewItem_UOM.UnitCode "
        Else
            query = " SELECT ItemDetail.ItemCode, ItemName, UPPER(ISNULL(ID_UOM,ItemUOM)) AS  UOM,  " & vbCrLf & _
                    " 		 CAST([BB] / ISNULL(viewItem_UOM.QTY,1) AS decimal(18,2)) AS [BB] ,  " & vbCrLf & _
                    " 		 CAST([IN] / ISNULL(viewItem_UOM.QTY,1) AS decimal(18,2)) AS [IN] ,  " & vbCrLf & _
                    " 		 CAST([OUT] / ISNULL(viewItem_UOM.QTY,1) AS decimal(18,2)) AS [OUT] ,  " & vbCrLf & _
                    " 		 CAST([EB] / ISNULL(viewItem_UOM.QTY,1) AS decimal(18,2)) AS [EB],  " & vbCrLf & _
                    " 		 CAST(AverageCost / ISNULL(viewItem_UOM.QTY,1) AS decimal(18,2)) AS AverageCost, ID_SC AS [Standard Cost], " & vbCrLf & _
                    " 		 CAST(([EB] / ISNULL(viewItem_UOM.QTY,1) * ID_SC) AS decimal(18,2)) AS TotalCost " & vbCrLf & _
                    " FROM " & vbCrLf & _
                    " ( " & vbCrLf & _
                    " 	SELECT	ItemCode, ItemName, Barcode, ItemType, ItemCategory, ItemOwner, ItemGroup, ItemUOM, ItemWeight, ID_UOM, ID_SC  " & vbCrLf & _
                    " 	FROM tblItem_Master " & vbCrLf & _
                    " 	WHERE isInventory = 1 " & vbCrLf & _
                    " ) AS ItemDetail " & vbCrLf & _
                    " INNER JOIN " & vbCrLf & _
                    " ( " & vbCrLf & _
                    " SELECT ItemCode, SUM(ISNULL([BB],0)) AS [BB], SUM(ISNULL([IN],0)) AS [IN], SUM(ISNULL([OUT],0)) AS [OUT], " & vbCrLf & _
                    " 		  SUM(ISNULL([BB],0)) + SUM(ISNULL([IN],0)) - SUM(ISNULL([OUT],0)) AS [EB] " & vbCrLf & _
                    " 	FROm " & vbCrLf & _
                    " 	( " & vbCrLf & _
                    " 		SELECT ItemCode, MovementType, SUM(TransQTY) AS QTY  " & vbCrLf & _
                    " 		FROM tblInventory " & vbCrLf & _
                    " 		WHERE  PostDate BETWEEN '" & DateFrom & "' AND '" & DateTo & "' " & vbCrLf & _
                    "       AND    WHSE IN (" & WHSE & ")  AND ISNULL(tblInventory.STATUS,'Active') = 'Active'" & vbCrLf & _
                    "       AND    ItemCode IN (" & Item & ") " & vbCrLf & _
                    " 		GROUP BY ItemCode, MovementType " & vbCrLf & _
                    " 	 UNION ALL " & vbCrLf & _
                    " 		SELECT ItemCode, 'BB',  SUM(CASE WHEN MovementType ='IN' THEN TransQTY ELSE TransQTY * -1 END) AS QTY  " & vbCrLf & _
                    " 		FROM tblInventory " & vbCrLf & _
                    " 		WHERE  PostDate < '" & DateFrom & "'  " & vbCrLf & _
                    "       AND    WHSE IN (" & WHSE & ")  AND ISNULL(tblInventory.STATUS,'Active') = 'Active'" & vbCrLf & _
                    "       AND    ItemCode IN (" & Item & ") " & vbCrLf & _
                    " 		GROUP BY ItemCode " & vbCrLf & _
                    " 	) AS A " & vbCrLf & _
                    " 	PIVOT  " & vbCrLf & _
                    " 	(  " & vbCrLf & _
                    " 		SUM(QTY) " & vbCrLf & _
                    " 		FOR [MovementType] IN ([BB],[IN],[OUT]) " & vbCrLf & _
                    " 	) AS pvt " & vbCrLf & _
                    " 	GROUP BY ItemCode " & vbCrLf & _
                    " ) AS ItemCount " & vbCrLf & _
                    " ON	ItemDetail.Itemcode = ItemCount.ItemCode " & vbCrLf & _
                    " LEFT JOIN  " & vbCrLf & _
                    " ( " & vbCrLf & _
                    "  SELECT    ItemCode, AverageCost, ROW_NUMBER() OVER (PARTITION BY ItemCode ORDER BY Postdate DESC, DateCreated DESC) AS RowID " & vbCrLf & _
                    "  FROM      tblInventory  WHERE ISNULL(tblInventory.STATUS,'Active') = 'Active' " & vbCrLf & _
                    "  ) AS ItemCost " & vbCrLf & _
                    " ON	ItemDetail.Itemcode = ItemCost.ItemCode " & vbCrLf & _
                    " AND RowID = 1 " & vbCrLf & _
                    " LEFT JOIN viewITEM_UOM " & vbCrLf & _
                    " ON ItemDetail.ItemCode = viewItem_UOM.GroupCode " & vbCrLf & _
                    " AND ItemDetail.ID_UOM = viewItem_UOM.UnitCode " & filter

        End If
        insertselect = query
        SQL.GetQuery(query)

        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvList.DataSource = SQL.SQLDS.Tables(0)

            dgvList.Columns(7).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvList.Columns(8).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvList.Columns(9).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvList.Columns(7).Frozen = True
            dgvList.Columns(8).Frozen = True
            dgvList.Columns(9).Frozen = True
            dgvList.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvList.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvList.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            If Inv_ComputationMethod = "SC" Then
                dgvList.Columns(7).Visible = False
            Else
                dgvList.Columns(8).Visible = False
            End If


        Else
            dgvList.DataSource = Nothing
        End If
        dgvList.Refresh()
    End Sub


    Private Sub dgvList_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList.CellDoubleClick
        If dgvList.SelectedRows.Count = 1 Then
            Dim f As New frmIA_Ledger
            f.ShowDialog(dgvList.SelectedRows(0).Cells(0).Value, showCost)
            f.Dispose()
        End If
    End Sub

    Private Sub frmIA_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        DateFrom = Date.Today.Date
        frmIA_Filter.ShowDialog()

        LoadCount()
    End Sub

    Private Sub tsbRefresh_Click(sender As System.Object, e As System.EventArgs) Handles tsbRefresh.Click
        LoadCount()
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim ItemOwner, insert As String
        ItemOwner = frmIA_Filter.cbItemOwner.SelectedItem
        insert = " DELETE tblIA_Filter " & _
                 " INSERT INTO tblIA_Filter (ItemCode, ItemName, UOM, BB, [IN], OUT, EB, AverageCost, StandardCost, TotalCost) " & insertselect & " "
        SQL.ExecNonQuery(insert)


        If frmIA_Filter.cbItemOwner.SelectedItem = "ALL" Then
            Dim f As New frmReport_Display
            f.ShowDialog("IA_Count", DateFrom, DateTo, Item.Replace("'", Chr(34)))
            f.Dispose()
        ElseIf frmIA_Filter.cbItemOwner.SelectedItem = ItemOwner Then
            Dim f As New frmReport_Display
            f.ShowDialog("IA_Count", DateFrom, DateTo, Item.Replace("'", Chr(34)))
            f.Dispose()

        End If
    End Sub

    Private Sub dgvList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList.CellContentClick

    End Sub
End Class