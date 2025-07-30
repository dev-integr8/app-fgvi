Public Class frmCopyFrom
    Dim Mod_Ref As String
    Dim SQL As New SQLControl
    Public TransID As String = ""
    Public UOM As String = ""
    Public ItemCode As String = ""
    Public ItemList As String = ""
    Dim param1 As String = ""
    Dim param2 As String = ""
    Dim param3 As String = ""
    Dim param4 As String = ""

    Public Overloads Function ShowDialog(ByVal Mod_Used As String, Optional ByVal P1 As String = "", Optional ByVal P2 As String = "", Optional ByVal P3 As String = "", Optional ByVal P4 As String = "") As Boolean
        Mod_Ref = Mod_Used
        param1 = P1
        param2 = P2
        param3 = P3
        param4 = P4
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmCopyFrom_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub frmSearchBP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Mod_Ref = "PO" Then
            LoadPO()
        ElseIf Mod_Ref = "Import" Then
            LoadImport()
        ElseIf Mod_Ref = "APV" Then
            LoadAPV()
        ElseIf Mod_Ref = "All Item" Then
            LoadAllItem(param1, param2)
        ElseIf Mod_Ref = "SerialItem" Then
            LoadSerialItem(param1, param2)
        ElseIf Mod_Ref = "ItemGroup" Then
            LoadItemGroup(param1)
        ElseIf Mod_Ref = "ItemMaster" Then
            LoadItemMaster(param1, param2)
        ElseIf Mod_Ref = "FinishedGood" Then
            LoadItemFG(param1, param2)
        ElseIf Mod_Ref = "ItemListPR" Then
            LoadPRItem(param1, param3)
        ElseIf Mod_Ref = "ItemListPO" Then
            LoadPOItem(param1, param3, param4)
        ElseIf Mod_Ref = "ItemListSO" Then
            LoadSOItem(param1)
        ElseIf Mod_Ref = "ItemListSoftware" Then
            LoadSoftwareItem(param1, param2, param3)
        ElseIf Mod_Ref = "SO" Then
            LoadSO()
        ElseIf Mod_Ref = "DR" Then
            LoadDR()
        ElseIf Mod_Ref = "BI_Type" Then
            LoadBIType(param1)
        ElseIf Mod_Ref = "BS_Type" Then
            LoadBSType(param1)
        End If
    End Sub


    Private Sub LoadBIType(ByVal Filter As String)
        Dim query As String
        query = " SELECT RecordID, Description, Amount FROM tblBI_Type " & _
                "  WHERE  Description LIKE '%" & Filter & "%'  AND Status ='Active' "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvCopyList.DataSource = SQL.SQLDS.Tables(0)
            dgvCopyList.Refresh()
            dgvCopyList.AutoResizeColumns()
        Else
            MsgBox("No Record found!", MsgBoxStyle.Information)
            Me.Close()
        End If
    End Sub

    Private Sub LoadBSType(ByVal Filter As String)
        Dim query As String
        query = " SELECT RecordID, Description, Amount FROM tblBS_Charges " & _
                "  WHERE  Description LIKE '%" & Filter & "%'  AND Status ='Active' "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvCopyList.DataSource = SQL.SQLDS.Tables(0)
            dgvCopyList.Refresh()
            dgvCopyList.AutoResizeColumns()
        Else
            MsgBox("No Record found!", MsgBoxStyle.Information)
            Me.Close()
        End If
    End Sub

    Private Sub LoadSoftwareItem(ByVal Filter As String, ByVal Type As String, ByVal Size As String)
        Dim query As String
        query = " SELECT  DISTINCT  SoftwareCode, SoftwareName, Price " & _
                " FROM    tblSP_Software" & _
               "  WHERE   SoftwareName LIKE '%" & Filter & "%' AND Type = '" & Type & "' AND Size = '" & Size & "' "

        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            If SQL.SQLDS.Tables(0).Rows.Count = 1 Then
                TransID = SQL.SQLDS.Tables(0).Rows(0).Item(0).ToString
                Me.Close()
            Else
                dgvCopyList.DataSource = SQL.SQLDS.Tables(0)
                dgvCopyList.Refresh()
                dgvCopyList.AutoResizeColumns()
            End If
            
        Else
            MsgBox("No Record found!", MsgBoxStyle.Information)
            Me.Close()
        End If
    End Sub

    Private Sub LoadPO()
        Dim query As String
        query = " SELECT TransID, DatePO AS Date, tblVCE_Master.VCEName AS Supplier,  TotalAmount AS Amount,  Remarks, DeliverTo " & _
                " FROM   tblPO INNER JOIN tblVCE_Master " & _
                " ON     tblPO.VCECode = tblVCE_Master.VCECode " & _
                " WHERE tblPO.Status ='Active' "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvCopyList.DataSource = SQL.SQLDS.Tables(0)
            dgvCopyList.Refresh()
            dgvCopyList.AutoResizeColumns()
        Else
            MsgBox("No Record found!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub LoadItemMaster(ByVal Filter As String, ByVal Type As String)
        Dim query As String
        'query = "  SELECT RecordID, ItemCode, ItemName,  " & _
        '        "         CASE WHEN VATInclusive = 1 THEN UnitPrice ELSE UnitPrice*1.12 END AS UnitPrice, " & _
        '        "         UOM,  Type " & _
        '        "  FROM   viewItem_Price " & _
        '        "  WHERE  ItemName LIKE '%' +" & "@Filter" & "+ '%'  AND Category ='" & Type & "' AND ItemStatus ='Active' AND PriceStatus ='Active' "


        query = " SELECT  ItemCode, ItemName, UOM, Type, Unitprice as Amount " & _
                " FROM viewItem_Price " & _
                " WHERE  ItemName LIKE '%' +" & "@Filter" & "+ '%'  AND PriceStatus ='Active'  "


        SQL.FlushParams()
        SQL.AddParam("@Filter", Filter)
        SQL.GetQuery(query)

        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvCopyList.DataSource = SQL.SQLDS.Tables(0)
            dgvCopyList.Refresh()
            dgvCopyList.AutoResizeColumns()
        Else
            MsgBox("No Record found!", MsgBoxStyle.Information)
            Me.Close()
        End If
        SQL.FlushParams()
    End Sub

    Private Sub LoadItemFG(ByVal Filter As String, ByVal Type As String)
        Dim query As String
        query = "  SELECT ItemCode, ItemCode, ItemName,  " & _
                "         ID_SC, " & _
                "         ItemUOM,  ItemType " & _
                "  FROM   tblItem_Master " & _
                "  WHERE  ItemName LIKE '%' +" & "@Filter" & "+ '%'  AND ItemType ='" & Type & "' AND Status ='Active' "
        SQL.FlushParams()
        SQL.AddParam("@Filter", Filter)
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvCopyList.DataSource = SQL.SQLDS.Tables(0)
            dgvCopyList.Refresh()
            dgvCopyList.AutoResizeColumns()
        Else
            MsgBox("No Record found!", MsgBoxStyle.Information)
            Me.Close()
        End If
        SQL.FlushParams()
    End Sub

    Private Sub LoadSerialItem(ByVal Filter As String, Optional ByVal Type As String = "")
        Try
            Dim query As String = ""

            Select Case Type
                Case "ItemName"
                    query = "  SELECT TransID, ItemCode, ItemName, UOM, EB AS Stock, DateExpired, LotNo, SerialNo, WHSE, Size, Color   " & _
                     "  FROM   viewItem_StockSerial     " & _
                     "  WHERE  ItemName LIKE '%' + @Filter + '%' AND EB > 0"
                Case "ItemCode"
                    query = "  SELECT TransID, ItemCode, ItemName, UOM, EB AS Stock, DateExpired, LotNo, SerialNo, WHSE, Size, Color   " & _
                    "  FROM   viewItem_StockSerial     " & _
                    "  WHERE  ItemCode LIKE '%' + @Filter + '%' AND EB > 0"
                Case "Barcode"
                    query = "  SELECT TransID, viewItem_StockSerial.ItemCode,  viewItem_StockSerial.ItemName, viewItem_StockSerial.UOM, " & _
                        "  EB AS Stock, DateExpired, LotNo, SerialNo, WHSE, Size, Color      " & _
                       "  FROM   tblItem_Barcode     " & _
                       "  INNER JOIN     " & _
                       "  viewItem_StockSerial ON     " & _
                       "  tblItem_Barcode.ItemCode =  viewItem_StockSerial.ItemCode   " & _
                       "  WHERE  tblItem_Barcode.Barcode = @Filter  AND tblItem_Barcode.Status ='Active' "
            End Select
            SQL.FlushParams()
            SQL.AddParam("@Filter", Filter)
            SQL.GetQuery(query)
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                dgvCopyList.DataSource = SQL.SQLDS.Tables(0)
                dgvCopyList.Refresh()
                dgvCopyList.AutoResizeColumns()

                If dgvCopyList.Rows.Count = 1 Then
                    TransID = dgvCopyList.SelectedRows(0).Cells(0).Value
                    UOM = dgvCopyList.SelectedRows(0).Cells(3).Value
                    Me.Close()
                End If
            Else
                MsgBox("No Record found!", MsgBoxStyle.Information)
                Me.Close()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, "frmCopyFrom")
        End Try
    End Sub


    Private Sub LoadAllItem(ByVal Filter As String, Optional ByVal Type As String = "")
        Try
            Dim query As String = ""

            Select Case Type
                Case "ItemName"
                    query = "  SELECT ItemCode, ItemName, PD_UOM, PD_UnitCost " & _
                            "  FROM   tblItem_Master     " & _
                            "  WHERE  ItemName LIKE '%' + @Filter + '%'  AND Status ='Active' "
                Case "ItemCode"
                    query = "  SELECT ItemCode, ItemName, PD_UOM, PD_UnitCost " & _
                           "  FROM   tblItem_Master     " & _
                           "  WHERE  ItemCode LIKE '%' + @Filter + '%'  AND Status ='Active' "
                Case "Barcode"
                    query = "  SELECT tblItem_Master.ItemCode, tblItem_Master.ItemName, UOM, PD_UnitCost " & _
                       "  FROM   tblItem_Barcode     " & _
                       "  INNER JOIN     " & _
                       "  tblItem_Master ON     " & _
                       "  tblItem_Barcode.ItemCode =  tblItem_Master.ItemCode   " & _
                       "  WHERE  tblItem_Barcode.Barcode = @Filter  AND tblItem_Barcode.Status ='Active' "
            End Select

            SQL.FlushParams()
            SQL.AddParam("@Filter", Filter)
            SQL.GetQuery(query)
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                dgvCopyList.DataSource = SQL.SQLDS.Tables(0)
                dgvCopyList.Refresh()
                dgvCopyList.AutoResizeColumns()


                If dgvCopyList.Rows.Count = 1 Then
                    TransID = dgvCopyList.SelectedRows(0).Cells(0).Value
                    UOM = dgvCopyList.SelectedRows(0).Cells(2).Value
                    Me.Close()
                End If
            Else
                MsgBox("No Record found!", MsgBoxStyle.Information)
                Me.Close()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, "frmCopyFrom")
        End Try
    End Sub

    Private Sub LoadItemGroup(ByVal Filter As String)
        Try
            Dim query As String
            query = "  SELECT DISTINCT BOMGroup, UOM " & _
                    "  FROM   tblBOM_Group     " & _
                    "  WHERE  BOMGroup LIKE '%' + @Filter + '%'  AND Status ='Active' "
            SQL.FlushParams()
            SQL.AddParam("@Filter", Filter)
            SQL.GetQuery(query)
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                dgvCopyList.DataSource = SQL.SQLDS.Tables(0)
                dgvCopyList.Refresh()
                dgvCopyList.AutoResizeColumns()
            Else
                MsgBox("No Record found!", MsgBoxStyle.Information)
                Me.Close()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, "frmCopyFrom")
        End Try
    End Sub

    Private Sub LoadPOItem(ByVal Filter As String, Optional ByVal FixedAsset As String = "", Optional ByVal Type As String = "")

        Dim query As String = ""
        If FixedAsset = "True" Then
            query = "  SELECT ItemCode, ItemName, PD_UOM, PD_UnitCost " & _
               "  FROM   tblItem_Master     " & _
               "  WHERE  ItemName LIKE '%' + @Filter + '%'  AND Status ='Active' "
            '"  WHERE  ItemName LIKE '%' + @Filter + '%'  AND Status ='Active' AND isFixAsset = 1 "
            SQL.FlushParams()
            SQL.AddParam("@Filter", Filter)
            SQL.GetQuery(query)
        Else
            Select Case Type
                Case "ItemName"
                    query = "  SELECT ItemCode, ItemName, PD_UOM, PD_UnitCost " & _
                       "  FROM   tblItem_Master     " & _
                       "  WHERE  ItemName LIKE '%' + @Filter + '%'  AND Status ='Active' "
                Case "ItemCode"
                    query = "  SELECT ItemCode, ItemName, PD_UOM, PD_UnitCost " & _
                           "  FROM   tblItem_Master     " & _
                           "  WHERE  ItemCode LIKE '%' + @Filter + '%'  AND Status ='Active' "
                Case "Barcode"
                    query = "  SELECT tblItem_Master.ItemCode, tblItem_Master.ItemName, UOM, PD_UnitCost " & _
                       "  FROM   tblItem_Barcode     " & _
                       "  INNER JOIN     " & _
                       "  tblItem_Master ON     " & _
                       "  tblItem_Barcode.ItemCode =  tblItem_Master.ItemCode   " & _
                       "  WHERE  tblItem_Barcode.Barcode = @Filter  AND tblItem_Barcode.Status ='Active' "
            End Select
            SQL.FlushParams()
            SQL.AddParam("@Filter", Filter)
            SQL.GetQuery(query)
        End If
        SQL.FlushParams()
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvCopyList.DataSource = SQL.SQLDS.Tables(0)
            dgvCopyList.Refresh()
            dgvCopyList.AutoResizeColumns()

            If dgvCopyList.Rows.Count = 1 Then
                TransID = dgvCopyList.SelectedRows(0).Cells(0).Value
                UOM = dgvCopyList.SelectedRows(0).Cells(2).Value
                Me.Close()
            End If
        Else
            Msg("No Record found!", MsgBoxStyle.Information)
            Me.Close()
        End If
    End Sub

    Private Sub LoadSOItem(ByVal Filter As String)
        Dim query As String
        query = "  SELECT RecordID, ItemCode, ItemName, UOM, UnitPrice " & _
                "  FROM   viewItem_Price     " & _
                "  WHERE  ItemName LIKE '%" & Filter & "%'  AND PriceStatus ='Active' "

        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvCopyList.DataSource = SQL.SQLDS.Tables(0)
            dgvCopyList.Refresh()
            dgvCopyList.AutoResizeColumns()
        Else
            MsgBox("No Record found!", MsgBoxStyle.Information)
            Me.Close()
        End If
    End Sub

    Private Sub LoadPRItem(ByVal Filter As String, Optional ByVal Type As String = "")
        Dim query As String = ""
        Select Case Type
            Case "ItemName"
                query = "  SELECT ItemCode, ItemName, PD_UOM " & _
                        "  FROM   tblItem_Master     " & _
                        "  WHERE  ItemName LIKE '%" & Filter & "%'  AND Status ='Active' "
            Case "ItemCode"
                query = "  SELECT ItemCode, ItemName, PD_UOM " & _
                        "  FROM   tblItem_Master     " & _
                        "  WHERE  ItemCode LIKE '%" & Filter & "%'  AND Status ='Active' "
            Case "Barcode"
                query = "  SELECT tblItem_Master.ItemCode, tblItem_Master.ItemName, UOM " & _
                   "  FROM   tblItem_Barcode     " & _
                   "  INNER JOIN     " & _
                   "  tblItem_Master ON     " & _
                   "  tblItem_Barcode.ItemCode =  tblItem_Master.ItemCode   " & _
                   "  WHERE  tblItem_Barcode.Barcode = '" & Filter & "'   AND tblItem_Barcode.Status ='Active' "
        End Select
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvCopyList.DataSource = SQL.SQLDS.Tables(0)
            dgvCopyList.Refresh()
            dgvCopyList.AutoResizeColumns()
            If dgvCopyList.Rows.Count = 1 Then
                TransID = dgvCopyList.SelectedRows(0).Cells(0).Value
                UOM = dgvCopyList.SelectedRows(0).Cells(2).Value
                Me.Close()
            End If
        Else
            MsgBox("No Record found!", MsgBoxStyle.Information)
            Me.Close()
        End If
    End Sub

    Private Sub LoadAPV()
        Dim query As String
        query = " SELECT Ref_TransID, Date, Supplier, Amount, Remarks, Reference FROM View_APV_Balance "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvCopyList.DataSource = SQL.SQLDS.Tables(0)
            dgvCopyList.Refresh()
            dgvCopyList.AutoResizeColumns()
        Else
            MsgBox("No Record found!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub LoadSO()
        Dim query As String
        query = " SELECT TransID AS [SO No.],  DateSO AS Date, tblVCE_Master.VCEName AS Customer,  Remarks " & _
                " FROM   tblSO INNER JOIN tblVCE_Master " & _
                " ON     tblSO.VCECode = tblVCE_Master.VCECode " & _
                " WHERE  tblSO.Status ='Active' "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvCopyList.DataSource = SQL.SQLDS.Tables(0)
            dgvCopyList.Refresh()
            dgvCopyList.AutoResizeColumns()
        Else
            MsgBox("No Record found!", MsgBoxStyle.Information)
            Me.Close()
        End If
    End Sub

    Private Sub LoadDR()
        Dim query As String
        query = " SELECT TransID AS [DR No.],  Date_DR AS Date, tblVCE_Master.VCEName AS Customer,  Remarks " & _
                " FROM   tblDR_Header INNER JOIN tblVCE_Master " & _
                " ON     tblDR_Header.VCECode = tblVCE_Master.VCECode " & _
                " WHERE  tblDR_Header.Status ='Open' "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvCopyList.DataSource = SQL.SQLDS.Tables(0)
            dgvCopyList.Refresh()
            dgvCopyList.AutoResizeColumns()
        Else
            MsgBox("No Record found!", MsgBoxStyle.Information)
            Me.Close()
        End If
    End Sub

    Private Sub LoadImport()
        Dim query As String
        query = " SELECT Import_No, VCEName AS Client, Client_Supplier AS [Client's Supplier], BL_No AS [BL No.],   " & _
                " 		CASE WHEN LCL = 1  " & _
                " 			 THEN 'LCL' " & _
                " 			 ELSE CAST(Volume AS nvarchar) + 'x' + CAST(Size AS nvarchar) " & _
                " 		END AS Container " & _
                " FROM tblImport_Maintenance INNER JOIN tblVCE_Master " & _
                " ON    tblImport_Maintenance.Client_Code = tblVCE_Master.VCECode " & _
                " WHERE Import_No NOT IN " & _
                " ( " & _
                " 	SELECT REPLACE(Reference_Other,'IMP:','') AS Import_No  " & _
                " 	FROM tblAPV " & _
                " 	WHERE Reference_Other LIKE '%IMP:%' " & _
                " ) "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvCopyList.DataSource = SQL.SQLDS.Tables(0)
            dgvCopyList.Refresh()
            dgvCopyList.AutoResizeColumns()
        Else
            MsgBox("No Record found!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub dgvCopyList_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvCopyList.CellMouseClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            TransID = dgvCopyList.SelectedRows(0).Cells(0).Value
            If dgvCopyList.Columns.Count > 1 Then
                ItemCode = dgvCopyList.SelectedRows(0).Cells(0).Value
            End If
            Me.Close()
        End If
    End Sub

    Private Sub dgvCopyList_CellMouseDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvCopyList.CellMouseDoubleClick
        If dgvCopyList.SelectedRows.Count > 0 Then
            TransID = dgvCopyList.SelectedRows(0).Cells(0).Value
            If dgvCopyList.Columns.Count > 1 Then
                ItemCode = dgvCopyList.SelectedRows(0).Cells(0).Value
            End If
            Me.Close()
        End If
    End Sub

    Private Sub dgvCopyList_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles dgvCopyList.KeyDown
        If e.KeyCode = Keys.Enter Then
            If dgvCopyList.SelectedRows.Count > 0 Then
                TransID = dgvCopyList.SelectedRows(0).Cells(0).Value
                If dgvCopyList.Columns.Count > 1 Then
                    ItemCode = dgvCopyList.SelectedRows(0).Cells(0).Value
                End If
                Me.Close()
            End If
        End If
    End Sub

    Private Sub dgvCopyList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCopyList.CellContentClick

    End Sub
End Class