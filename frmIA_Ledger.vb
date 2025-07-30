Public Class frmIA_Ledger
    Dim itemCode As String
    Dim showCost As Boolean
    Dim ModuleID As String = "IA"
    Dim EnableEvent As Boolean

    Public Overloads Function ShowDialog(ByVal code As String, ByVal withCost As Boolean) As Boolean
        itemCode = code
        showCost = withCost
        MyBase.ShowDialog()
        Return True
    End Function


    Private Sub frmIA_Ledger_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'Ref Type
        cbRefType.Items.Clear()
        cbRefType.Items.Add("ALL")
        cbRefType.Items.Add("BB")
        cbRefType.Items.Add("GI")
        cbRefType.Items.Add("GR")
        cbRefType.Items.Add("RR")
        cbRefType.Items.Add("MRIS")
        cbRefType.SelectedItem = "ALL"
        'Movement Type
        cbMovementType.Items.Clear()
        cbMovementType.Items.Add("ALL")
        cbMovementType.Items.Add("IN")
        cbMovementType.Items.Add("OUT")
        cbMovementType.SelectedItem = "ALL"
        EnableEvent = False
        LoadLedger(cbMovementType.SelectedItem, cbRefType.SelectedItem)
        LoadStock
    End Sub

    Private Sub LoadStock()
        Dim query As String
        query = " SELECT CASE WHEN tblWarehouse.Code IS NULL THEN tblProdWarehouse.Code ELSE tblWarehouse.Code END AS Code,  " & vbCrLf & _
                " 	   CASE WHEN tblWarehouse.Code IS NULL THEN tblProdWarehouse.Description ELSE tblWarehouse.Description END AS Description,  " & vbCrLf & _
                " 	   CAST(SUM(QTY) AS decimal(18,2)) AS QTY " & vbCrLf & _
                " FROM " & vbCrLf & _
                " ( " & vbCrLf & _
                " 	SELECT  tblInventory.ItemCode,  " & vbCrLf & _
                " 			 CAST((CASE WHEN MovementType = 'IN'  " & vbCrLf & _
                " 						THEN tblInventory.TransQTY  / ISNULL(viewItem_UOM.QTY,1)  " & vbCrLf & _
                " 						ELSE (tblInventory.TransQTY  / ISNULL(viewItem_UOM.QTY,1)) * -1  " & vbCrLf & _
                " 				   END) AS decimal(18,2)) AS QTY,  " & vbCrLf & _
                " 			 WHSE " & vbCrLf & _
                " 	FROM     tblInventory INNER JOIN tblItem_Master " & vbCrLf & _
                " 	ON		 tblInventory.ItemCode = tblItem_Master.ItemCode " & vbCrLf & _
                " 	LEFT JOIN viewITEM_UOM " & vbCrLf & _
                " 	ON		tblItem_Master.ItemCode = viewItem_UOM.GroupCode " & vbCrLf & _
                " 	AND		tblItem_Master.ID_UOM = viewItem_UOM.UnitCode " & vbCrLf & _
                " 	WHERE   PostDate  <='" & frmIA.DateTo & "' " & vbCrLf & _
                "   AND     WHSE IN (" & frmIA.WHSE & ") " & vbCrLf & _
                "   AND     tblInventory.ItemCode = @ItemCode " & vbCrLf & _
                " ) AS Inv " & vbCrLf & _
                " LEFT JOIN tblWarehouse " & vbCrLf & _
                " ON inv.WHSE = tblWarehouse.Code " & vbCrLf & _
                " LEFT JOIN tblProdWarehouse " & vbCrLf & _
                " ON inv.WHSE = tblProdWarehouse.Code " & vbCrLf & _
                " GROUP BY ItemCode,  " & vbCrLf & _
                " 	   CASE WHEN tblWarehouse.Code IS NULL THEN tblProdWarehouse.Code ELSE tblWarehouse.Code END, " & vbCrLf & _
                " 	   CASE WHEN tblWarehouse.Code IS NULL THEN tblProdWarehouse.Description ELSE tblWarehouse.Description END  "
        SQL.FlushParams()
        SQL.AddParam("@ItemCode", ItemCode)
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvWHSE.Rows.Add({SQL.SQLDR("Code").ToString, SQL.SQLDR("Description").ToString, SQL.SQLDR("QTY").ToString, 0})
        End While
    End Sub

    Private Sub LoadLedger(ByVal strMovement As String, ByVal strRef As String)
        Try
            Dim query, mt, rt As String
            If strRef = "ALL" Then
                rt = "'BB', 'GI', 'GR', 'RR', 'MRIS'"
            Else
                rt = "'" & strRef & "'"
            End If

            If strMovement = "ALL" Then
                mt = "'IN', 'OUT'"
            Else
                mt = "'" & strMovement & "'"
            End If

            ' ADD/MODIFY ADDITIONAL FIELDS AFTER TransNo ONLY
            If showCost Then
                query = " SELECT PostDate AS Date, RefID, RefType, TransNo,  MovementType AS Movement, QTY, UOM,   InvQTY,  InvValue, ComQTY, ComValue, AverageCost, StandardCost, UnitPrice, TotalPrice, WHSE, DateExpired, DateProduced " & vbCrLf &
                        " FROM viewIA_Ledger " & vbCrLf &
                        " WHERE ItemCode = @ItemCode " & vbCrLf &
                        " AND RefType IN (" & rt & ")  AND Movement IN (" & mt & ") AND    PostDate BETWEEN "
            Else
                query = "SELECT * FROM ( " & vbCrLf &
                        " SELECT '" & frmIA.DateFrom & "' AS Date,  0 AS RefID, 'BB' AS RefType, '' AS TransNo, 'IN' AS Movement,  SUM(InvQTY) AS QTY, UPPER(InvUOM) AS UOM, SUM(UnitCost) AS UnitCost, SUM(AverageCost) AS AverageCost,  StandardCost, SUM(InvQTY) AS InvQTY, SUM(InvQTY)  AS [Cum QTY], '' AS  WHSE, VCEName, DateCreated " & vbCrLf &
                        " FROM viewIA_Ledger " & vbCrLf &
                        " LEFT JOIN  viewVCE_Master" & vbCrLf &
                        " ON viewVCE_Master.VCECode = viewIA_Ledger.VCECode " & vbCrLf &
                        " WHERE ItemCode = @ItemCode AND PostDate < '" & frmIA.DateFrom & "'  " & vbCrLf &
                        " AND    WHSE IN (" & frmIA.WHSE & ") " & vbCrLf &
                        " GROUP BY InvUOM , VCEName, DateCreated, StandardCost" & vbCrLf &
                        " UNION ALL " & vbCrLf &
                        " SELECT PostDate AS Date,  RefID, RefType, TransNo, MovementType AS Movement, QTY, UPPER(UOM), AverageCost, StandardCost,  UnitCost, InvQTY, ComQTY  AS [Cum QTY], WHSE, VCEName, DateCreated " & vbCrLf &
                        " FROM viewIA_Ledger " & vbCrLf &
                        " LEFT JOIN  viewVCE_Master" & vbCrLf &
                        " ON viewVCE_Master.VCECode = viewIA_Ledger.VCECode " & vbCrLf &
                        " WHERE ItemCode = @ItemCode AND PostDate BETWEEN '" & frmIA.DateFrom & "' AND '" & frmIA.DateTo & "' " & vbCrLf &
                        " AND    WHSE IN (" & frmIA.WHSE & ") ) AS Ledger " & vbCrLf &
                        " WHERE RefType IN (" & rt & ")  AND Movement IN (" & mt & ") " & vbCrLf &
                        " ORDER BY Date, DateCreated "
            End If
            SQL.FlushParams()
            SQL.AddParam("@ItemCode", itemCode)
            SQL.GetQuery(query)
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                dgvLedger.DataSource = SQL.SQLDS.Tables(0)
                dgvLedger.Columns(1).Visible = False
                dgvLedger.Columns(14).Visible = False
                If Inv_ComputationMethod = "SC" Then
                    dgvLedger.Columns(8).Visible = False
                Else
                    dgvLedger.Columns(9).Visible = False
                End If
            Else
                dgvLedger.DataSource = Nothing
            End If
            dgvLedger.Refresh()

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            SQL.FlushParams()
        End Try
        EnableEvent = True
    End Sub

    Private Sub dgvLedger_DoubleClick(sender As System.Object, e As System.EventArgs) Handles dgvLedger.DoubleClick
        If dgvLedger.SelectedRows.Count = 1 Then
            Dim transID As String = dgvLedger.SelectedRows(0).Cells(1).Value
            Select Case dgvLedger.SelectedRows(0).Cells(2).Value
                Case "RR"
                    frmRR.ShowDialog(transID)
                    frmRR.Dispose()
                Case "DR"
                    frmDR.ShowDialog(transID)
                    frmDR.Dispose()
                Case "GI"
                    frmGI.ShowDialog(transID)
                    frmGI.Dispose()
                Case "GR"
                    frmGR.ShowDialog(transID)
                    frmGR.Dispose()
                Case "IT"
                    frmIT.ShowDialog(transID)
                    frmIT.Dispose()
                Case "BOMC"
                    frmBOMC.ShowDialog(transID)
                    frmBOMC.Dispose()
            End Select
         
        End If
    End Sub

    Private Sub dgvLedger_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLedger.CellContentClick

    End Sub

    Private Sub cbMovementType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbMovementType.SelectedIndexChanged

        If EnableEvent = True Then
            EnableEvent = False
            LoadLedger(cbMovementType.SelectedItem, cbRefType.SelectedItem)
        End If
    End Sub

    Private Sub cbRefType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbRefType.SelectedIndexChanged

        If EnableEvent = True Then
            EnableEvent = False
            LoadLedger(cbMovementType.SelectedItem, cbRefType.SelectedItem)
        End If
    End Sub
End Class