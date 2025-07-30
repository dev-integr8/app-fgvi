Public Class frmMR
    Dim TransID, RefID As String
    Dim MRNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "MR"
    Dim ColumnPK As String = "MR_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblMR"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim tempWHSE As String = ""
    Dim ForApproval As Boolean = False

    Public Overloads Function ShowDialog(ByVal docnumber As String) As Boolean
        TransID = docnumber
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmMR_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            loadPWHSE()
            loadWH()
            If TransID <> "" Then
                LoadMR(TransID)
            End If
            tsbSearch.Enabled = True
            tsbNew.Enabled = True
            tsbEdit.Enabled = False
            tsbSave.Enabled = False
            tsbCancel.Enabled = False
            tsbClose.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = True
            tsbPrint.Enabled = False
            tsbCopy.Enabled = False
            EnableControl(False)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub loadPWHSE()
        Dim query As String
        query = " SELECT    DISTINCT tblProdWarehouse.Code + ' | ' + Description AS Description  " & _
                " FROM      tblProdWarehouse INNER JOIN tblUser_Access " & _
                " ON        tblProdWarehouse.Code = tblUser_Access.Code " & _
                " AND       tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active' " & _
                " AND       tblUser_Access.Type = 'Production' AND isAllowed = 1 " & _
                " WHERE     UserID ='" & UserID & "' " & _
                " UNION ALL " & _
                " SELECT    DISTINCT tblWarehouse.Code + ' | ' + Description AS Description  " & _
                " FROM      tblWarehouse INNER JOIN tblUser_Access " & _
                " ON        tblWarehouse.Code = tblUser_Access.Code " & _
                " AND       tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active' " & _
                " AND       tblUser_Access.Type = 'Warehouse' AND isAllowed = 1 " & _
                " WHERE     UserID ='" & UserID & "' "
        SQL.ReadQuery(query)
        cbWHfrom.Items.Clear()
        Dim ctr As Integer = 0
        While SQL.SQLDR.Read
            cbWHfrom.Items.Add(SQL.SQLDR("Description").ToString)
            ctr += 1
        End While
        If cbWHfrom.Items.Count > 0 Then
            cbWHfrom.SelectedIndex = 0
        End If
    End Sub


    Private Sub loadWH()
        Dim query As String
        query = " SELECT tblWarehouse.Code + ' | ' + Description AS Description  FROM tblWarehouse WHERE Status ='Active' " & _
                " UNION ALL " & _
                " SELECT tblProdWarehouse.Code + ' | ' + Description AS Description  FROM tblProdWarehouse WHERE Status ='Active' "
        SQL.ReadQuery(query)
        cbWHto.Items.Clear()
        cbWHto.Items.Add("Multiple Warehouse")
        While SQL.SQLDR.Read
            cbWHto.Items.Add(SQL.SQLDR("Description"))
        End While
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        cbWHto.Enabled = Value
        cbWHfrom.Enabled = Value
        dgvItemMaster.AllowUserToAddRows = Value
        dgvItemMaster.AllowUserToDeleteRows = Value
        dgvItemMaster.ReadOnly = Not Value
        If Value = True Then
            dgvItemMaster.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemMaster.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
        dgcBOMQTY.ReadOnly = True
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("MR_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("MR")
            TransID = f.transID
            LoadMR(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub LoadMR(ByVal ID As String)
        Dim query As String
        Dim WHSEfrom, WHSEto As String
        query = " SELECT   TransID, MR_No, DateMR,  WHSE_From, " & _
                " CASE WHEN View_Warehouse_Production.Description IS NOT NULL " & _
                " THEN tblMR.WHSE_To + ' | ' + View_Warehouse_Production.Description " & _
                " ELSE 'Multiple Warehouse' " & _
                " END AS WHSE_To, Remarks, tblMR.Status " & _
                " FROM     tblMR " & _
                " LEFT JOIN View_Warehouse_Production " & _
                " ON       tblMR.WHSE_To = View_Warehouse_Production.Code " & _
                " AND      View_Warehouse_Production.Status ='Active' " & _
                " WHERE    TransId = '" & ID & "' " & _
                " ORDER BY TransId "
        SQL.ReadQuery(query)

        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            MRNo = SQL.SQLDR("MR_No").ToString
            txtTransNum.Text = MRNo
            dtpDocDate.Text = SQL.SQLDR("DateMR").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            WHSEfrom = SQL.SQLDR("WHSE_From").ToString
            WHSEto = SQL.SQLDR("WHSE_To").ToString
            cbWHfrom.SelectedItem = GetWHSE(WHSEfrom, "tblProdWarehouse")
            cbWHto.SelectedItem = WHSEto
            LoadMRDetails(TransID)
            ' TOOLSTRIP BUTTONS
            If txtStatus.Text = "Cancelled" Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            Else
                tsbEdit.Enabled = True
                tsbCancel.Enabled = True
            End If
            tsbPrint.Enabled = True
            tsbClose.Enabled = False
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbPrint.Enabled = True
            tsbSave.Enabled = False
            tsbNew.Enabled = True
            tsbSearch.Enabled = True
            tsbExit.Enabled = True
            tsbCopy.Enabled = False
            If dtpDocDate.Value <= GetMaxPEC() Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            End If
            EnableControl(False)
        Else
            Cleartext()
        End If
    End Sub



    Protected Sub LoadMRDetails(ByVal TransID As String)
        Dim ctr As Integer = 0
        Dim query As String
        query = " SELECT	tblMR_Details.ItemCode, tblMR_Details.Description, tblMR_Details.UOM, tblMR_Details.QTY  AS QTY, tblMR_Details.StockQTY  AS StockQTY,  tblMR_Details.ReqQTY as ReqQTY, " & _
                " CASE WHEN View_Warehouse_Production.Description IS NOT NULL " & _
                " THEN tblMR_Details.WHSE + ' | ' + View_Warehouse_Production.Description " & _
                " ELSE 'Multiple Warehouse' " & _
                " END AS WHSE " & _
                " FROM	    tblMR INNER JOIN tblMR_Details " & _
                " ON		tblMR.TransID = tblMR_Details.TransID " & _
                " LEFT JOIN View_Warehouse_Production " & _
                " ON       tblMR_Details.WHSE = View_Warehouse_Production.Code  " & _
                " AND      View_Warehouse_Production.Status ='Active' " & _
                " WHERE     tblMR_Details.TransId = " & TransID & " " & _
                " ORDER BY  tblMR_Details.LineNum "
        disableEvent = True
        dgvItemMaster.Rows.Clear()
        disableEvent = False
        SQL.ReadQuery(query)
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows

                dgvItemMaster.Rows.Add(row(0).ToString, "", row(1).ToString, row(2).ToString, _
                                     CDec(row(3)).ToString("N2"), CDec(row(5)).ToString("N2"), CDec(row(4)).ToString("N2"), row(6).ToString)
                LoadUOM(row(0).ToString, ctr)
                LoadWHSE(ctr)
                ctr += 1
            Next
            LoadBarCode()
        End If
    End Sub

    Private Sub LoadWHSE(Optional ByVal SelectedIndex As Integer = -1)
        Try
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemMaster.Item(chWHSE.Index, SelectedIndex)
            dgvCB.Items.Clear()
            ' ADD ALL WHSEc
            Dim query As String
            If cbWHto.SelectedItem = "Multiple Warehouse" Then
                query = " SELECT    DISTINCT tblWarehouse.Code + ' | ' + Description AS Description   " & _
                        " FROM      tblWarehouse WHERE tblWarehouse.Status ='Active' " & _
                        " UNION ALL " & _
                        " SELECT    DISTINCT tblProdWarehouse.Code + ' | ' + Description AS Description " & _
                        " FROM      tblProdWarehouse WHERE tblProdWarehouse.Status ='Active' "
            ElseIf cbWHto.SelectedItem = "Warehouse" Then
                query = " SELECT    DISTINCT tblWarehouse.Code + ' | ' + Description AS Description   " & _
                        " FROM      tblWarehouse WHERE tblWarehouse.Status ='Active' "
            Else
                query = " SELECT    DISTINCT tblProdWarehouse.Code + ' | ' + Description AS Description " & _
                        " FROM      tblProdWarehouse WHERE tblProdWarehouse.Status ='Active' "
            End If
            SQL.ReadQuery(query)
            dgvCB.Items.Clear()
            While SQL.SQLDR.Read
                dgvCB.Items.Add(SQL.SQLDR("Description").ToString)
            End While
            dgvCB.Items.Add("")
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub Cleartext()
        txtTransNum.Clear()
        txtJORef.Clear()
        txtFGRef.Clear()
        txtSFGRef.Clear()
        cbWHto.SelectedIndex = -1
        dgvItemMaster.Rows.Clear()
        txtRemarks.Clear()
        txtStatus.Text = "Open"
        dtpDocDate.MinDate = GetMaxPEC().AddDays(1)
        dtpDocDate.Value = Date.Today.Date
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("MR_ADD") Then
            msgRestricted()
        Else
            Cleartext()
            TransID = ""
            MRNo = ""


            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbCancel.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
            tsbPrint.Enabled = False
            tsbCopy.Enabled = True
            EnableControl(True)

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)


        End If
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("MR_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)
            If cbWHto.SelectedItem = "Multiple Warehouse" Then
                LoadStockPerLine()
            Else
                LoadStock()
            End If

            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbCancel.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
            tsbPrint.Enabled = False
            tsbCopy.Enabled = False
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If cbWHto.SelectedIndex = -1 Then
            Msg("Please select warehouse!", MsgBoxStyle.Exclamation)
            'ElseIf Not LoadStockPerLine() Then
            '    Msg("Invalid request quantity!" & vbNewLine & "Cannot request more than the available stock!", MsgBoxStyle.Exclamation)
        ElseIf TransID = "" Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                TransID = GenerateTransID(ColumnID, DBTable)
                MRNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                txtTransNum.Text = MRNo
                SaveMR()
                Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                MRNo = txtTransNum.Text
                LoadMR(TransID)
            End If
        Else
            If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                UpdateMR()
                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                MRNo = txtTransNum.Text
                LoadMR(TransID)
            End If
        End If
    End Sub



    Private Sub SaveMR()
        Try
            Dim WHSEfrom, WHSEto As String

            WHSEfrom = GetWHSE(cbWHfrom.SelectedItem)
            If cbWHto.SelectedItem = "Multiple Warehouse" Then
                WHSEto = "Multiple Warehouse"
            Else
                WHSEto = GetWHSE(cbWHto.SelectedItem)
            End If

            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblMR(TransID, MR_No, BranchCode, BusinessCode, DateMR, WHSE_from, WHSE_to, Remarks, JO_Ref, " & _
                        "   FG_Ref_Code, SFG_Ref_Code, WhoCreated, Status) " & _
                        " VALUES (@TransID, @MR_No, @BranchCode, @BusinessCode, @DateMR, @WHSE_from, @WHSE_to, @Remarks, @JO_Ref, " & _
                        "   @FG_Ref_Code, @SFG_Ref_Code, @WhoCreated, @Status) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@MR_No", MRNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateMR", dtpDocDate.Value.Date)
            SQL.AddParam("@WHSE_from", WHSEfrom)
            SQL.AddParam("@WHSE_to", WHSEto)
            SQL.AddParam("@JO_Ref", txtJORef.Text)
            SQL.AddParam("@FG_Ref_Code", txtFGRef.Text)
            SQL.AddParam("@SFG_Ref_Code", txtSFGRef.Text)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            Dim ItemCode, Description, UOM, WHSE As String
            Dim QTY, Stock, UnitCost, ReqQTY As Decimal
            For Each row As DataGridViewRow In dgvItemMaster.Rows
                If Not row.Cells(dgcBOMQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                    Description = IIf(row.Cells(chItemName.Index).Value = Nothing, "", row.Cells(chItemName.Index).Value)
                    UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                    UnitCost = GetAverageCost(ItemCode)
                    WHSE = GetWHSE(IIf(row.Cells(chWHSE.Index).Value = Nothing, "", row.Cells(chWHSE.Index).Value))
                    If IsNumeric(row.Cells(dgcBOMQTY.Index).Value) Then QTY = CDec(row.Cells(dgcBOMQTY.Index).Value) Else QTY = 1
                    If IsNumeric(row.Cells(dgcReqQTY.Index).Value) Then ReqQTY = CDec(row.Cells(dgcReqQTY.Index).Value) Else ReqQTY = 1
                    If IsNumeric(row.Cells(chStock.Index).Value) Then Stock = CDec(row.Cells(chStock.Index).Value) Else Stock = 1
                    insertSQL = " INSERT INTO " & _
                         " tblMR_Details(TransId, ItemCode, Description, UOM, QTY, StockQTY, ReqQTY, WHSE,  LineNum, WhoCreated) " & _
                         " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @StockQTY, @ReqQTY, @WHSE, @LineNum, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", ItemCode)
                    SQL.AddParam("@Description", Description)
                    SQL.AddParam("@UOM", UOM)
                    SQL.AddParam("@QTY", QTY)
                    SQL.AddParam("@StockQTY", Stock)
                    SQL.AddParam("@ReqQTY", ReqQTY)
                    SQL.AddParam("@WHSE", WHSE)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "MR_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub UpdateMR()
        Try
            Dim WHSEfrom, WHSEto As String

            WHSEfrom = GetWHSE(cbWHfrom.SelectedItem)
            If cbWHto.SelectedItem = "Multiple Warehouse" Then
                WHSEto = "Multiple Warehouse"
            Else
                WHSEto = GetWHSE(cbWHto.SelectedItem)
            End If

            activityStatus = True
            Dim insertSQL, updateSQL, deleteSQL As String
            updateSQL = " UPDATE    tblMR " & _
                        " SET       MR_No = @MR_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, DateMR = @DateMR, " & _
                        "           WHSE_from = @WHSE_from, WHSE_to = @WHSE_to, Remarks = @Remarks, JO_Ref = @JO_Ref, FG_Ref_Code = @FG_Ref_Code, SFG_Ref_Code = @SFG_Ref_Code,  " & _
                        "           WhoModified = @WhoModified, DateModified = GETDATE() " & _
                        " WHERE     TransID = @TransID  "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@MR_No", MRNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateMR", dtpDocDate.Value.Date)
            SQL.AddParam("@WHSE_from", WHSEfrom)
            SQL.AddParam("@WHSE_to", WHSEto)
            SQL.AddParam("@JO_Ref", txtJORef.Text)
            SQL.AddParam("@FG_Ref_Code", txtFGRef.Text)
            SQL.AddParam("@SFG_Ref_Code", txtSFGRef.Text)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)

            deleteSQL = " DELETE FROM tblMR_Details WHERE TransID =@TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.ExecNonQuery(deleteSQL)

            Dim line As Integer = 1
            Dim ItemCode, Description, UOM, WHSE As String
            Dim QTY, Stock, UnitCost, ReqQTY As Decimal
            For Each row As DataGridViewRow In dgvItemMaster.Rows
                If Not row.Cells(dgcBOMQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                    Description = IIf(row.Cells(chItemName.Index).Value = Nothing, "", row.Cells(chItemName.Index).Value)
                    UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                    WHSE = GetWHSE(IIf(row.Cells(chWHSE.Index).Value = Nothing, "", row.Cells(chWHSE.Index).Value))
                    UnitCost = GetAverageCost(ItemCode)
                    If IsNumeric(row.Cells(dgcBOMQTY.Index).Value) Then QTY = CDec(row.Cells(dgcBOMQTY.Index).Value) Else QTY = 1
                    If IsNumeric(row.Cells(dgcReqQTY.Index).Value) Then ReqQTY = CDec(row.Cells(dgcReqQTY.Index).Value) Else ReqQTY = 1
                    If IsNumeric(row.Cells(chStock.Index).Value) Then Stock = CDec(row.Cells(chStock.Index).Value) Else Stock = 1
                    insertSQL = " INSERT INTO " & _
                         " tblMR_Details(TransId, ItemCode, Description, UOM, QTY, StockQTY, ReqQTY, WHSE,  LineNum, WhoCreated) " & _
                         " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @StockQTY, @ReqQTY, @WHSE, @LineNum, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", ItemCode)
                    SQL.AddParam("@Description", Description)
                    SQL.AddParam("@UOM", UOM)
                    SQL.AddParam("@QTY", QTY)
                    SQL.AddParam("@StockQTY", Stock)
                    SQL.AddParam("@ReqQTY", ReqQTY)
                    SQL.AddParam("@WHSE", WHSE)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next

        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "MR_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("MR_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE  tblMR SET Status ='Cancelled' WHERE MR_No = @MR_No "
                        SQL.FlushParams()
                        MRNo = txtTransNum.Text
                        SQL.AddParam("@MR_No", MRNo)
                        SQL.ExecNonQuery(deleteSQL)
                        Msg("Record cancelled successfully", MsgBoxStyle.Information)

                        tsbSearch.Enabled = True
                        tsbNew.Enabled = True
                        tsbEdit.Enabled = False
                        tsbSave.Enabled = False
                        tsbCancel.Enabled = False
                        tsbClose.Enabled = False
                        tsbPrevious.Enabled = False
                        tsbNext.Enabled = False
                        tsbExit.Enabled = True
                        tsbPrint.Enabled = True
                        tsbCopy.Enabled = False
                        EnableControl(False)

                        LoadMR(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "MR_No", MRNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If MRNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID  " & _
                    "  FROM     tblMR " & _
                    "   LEFT JOIN tblProdWarehouse  ON	          " & _
                    "   tblMR.WHSE_From = tblProdWarehouse.Code    " & _
                    "   WHERE          " & _
                    "   ( WHSE_From IN (SELECT    DISTINCT tblWarehouse.Code  " & _
                    "   FROM      tblWarehouse   " & _
                    "   INNER JOIN tblUser_Access    ON          " & _
                    "   tblWarehouse.Code = tblUser_Access.Code   " & _
                    "    AND       tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active'   " & _
                    "    AND       tblUser_Access.Type = 'Warehouse' AND isAllowed = 1  " & _
                    "    WHERE     UserID ='" & UserID & "')                  " & _
                    "    OR  " & _
                    "     WHSE_From IN (SELECT    DISTINCT tblProdWarehouse.Code   " & _
                    "     FROM      tblProdWarehouse INNER JOIN tblUser_Access     " & _
                    "      ON        tblProdWarehouse.Code = tblUser_Access.Code    " & _
                    "  	 AND       tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active'    " & _
                    "  	 AND       tblUser_Access.Type = 'Production' AND isAllowed = 1    " & _
                    "  	 WHERE     UserID ='" & UserID & "'))  AND MR_No < '" & MRNo & "' ORDER BY MR_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadMR(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If MRNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID " & _
                    " FROM     tblMR " & _
                    "   LEFT JOIN tblProdWarehouse  ON	          " & _
                    "   tblMR.WHSE_From = tblProdWarehouse.Code    " & _
                    "   WHERE          " & _
                    "   ( WHSE_From IN (SELECT    DISTINCT tblWarehouse.Code  " & _
                    "   FROM      tblWarehouse   " & _
                    "   INNER JOIN tblUser_Access    ON          " & _
                    "   tblWarehouse.Code = tblUser_Access.Code   " & _
                    "    AND       tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active'   " & _
                    "    AND       tblUser_Access.Type = 'Warehouse' AND isAllowed = 1  " & _
                    "    WHERE     UserID ='" & UserID & "')                  " & _
                    "    OR  " & _
                    "     WHSE_From IN (SELECT    DISTINCT tblProdWarehouse.Code   " & _
                    "     FROM      tblProdWarehouse INNER JOIN tblUser_Access     " & _
                    "      ON        tblProdWarehouse.Code = tblUser_Access.Code    " & _
                    "  	 AND       tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active'    " & _
                    "  	 AND       tblUser_Access.Type = 'Production' AND isAllowed = 1    " & _
                    "  	 WHERE     UserID ='" & UserID & "'))  AND MR_No > '" & MRNo & "' ORDER BY MR_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadMR(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If MRNo = "" Then
            Cleartext()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadMR(TransID)
            tsbEdit.Enabled = True
            tsbCancel.Enabled = True
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbPrint.Enabled = True
        End If
        tsbSearch.Enabled = True
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
        tsbCopy.Enabled = False
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub frmMR_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control = True Then
            If e.KeyCode = Keys.S Then
                If tsbSave.Enabled = True Then tsbSave.PerformClick()
            ElseIf e.KeyCode = Keys.F Then
                If tsbSearch.Enabled = True Then tsbSearch.PerformClick()
            ElseIf e.KeyCode = Keys.N Then
                If tsbNew.Enabled = True Then tsbNew.PerformClick()
            ElseIf e.KeyCode = Keys.E Then
                If tsbEdit.Enabled = True Then tsbEdit.PerformClick()
            ElseIf e.KeyCode = Keys.D Then
                If tsbCancel.Enabled = True Then tsbCancel.PerformClick()
            ElseIf e.KeyCode = Keys.Left Then
                If tsbPrevious.Enabled = True Then tsbPrevious.PerformClick()
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Right Then
                If tsbNext.Enabled = True Then tsbNext.PerformClick()
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.P Then
                If tsbPrint.Enabled = True Then tsbPrint.PerformClick()
            ElseIf e.KeyCode = Keys.R Then
                If tsbReports.Enabled = True Then tsbReports.PerformClick()
            End If
        ElseIf e.Alt = True Then
            If e.KeyCode = Keys.F4 Then
                If tsbExit.Enabled = True Then
                    tsbExit.PerformClick()
                Else
                    e.SuppressKeyPress = True
                End If
            End If
        ElseIf e.KeyCode = Keys.Escape Then
            If tsbClose.Enabled = True Then tsbClose.PerformClick()
        End If
    End Sub

    Private Sub dgvItemMaster_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemMaster.CellEndEdit
        Try
            Dim itemCode, UOM As String
            Dim rowIndex As Integer = dgvItemMaster.CurrentCell.RowIndex
            Dim colindex As Integer = dgvItemMaster.CurrentCell.ColumnIndex
            Select Case colindex
                Case chItemCode.Index
                    If dgvItemMaster.Item(chItemCode.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemMaster.Item(chItemCode.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("All Item", itemCode, "ItemCode")
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItemDetails(itemCode)
                        Else
                            dgvItemMaster.Rows.RemoveAt(e.RowIndex)
                        End If
                        f.Dispose()
                    End If
                Case chBarcode.Index
                    If dgvItemMaster.Item(chBarcode.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemMaster.Item(chBarcode.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("All Item", itemCode, "Barcode")
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            UOM = f.UOM
                            LoadItemDetails(itemCode, UOM)
                        Else
                            dgvItemMaster.Rows.RemoveAt(e.RowIndex)
                        End If
                        f.Dispose()
                    End If
                Case chItemName.Index
                    If dgvItemMaster.Item(chItemName.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemMaster.Item(chItemName.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("All Item", itemCode, "ItemName")
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItemDetails(itemCode)
                        Else
                            dgvItemMaster.Rows.RemoveAt(e.RowIndex)
                        End If
                        f.Dispose()
                    End If
                Case chWHSE.Index
                    LoadStockPerLine()
            End Select
        Catch ex1 As InvalidOperationException

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try

    End Sub

    Private Sub LoadItemDetails(ByVal ItemCode As String, Optional UOM As String = "")
        Dim query As String
        query = " SELECT    ItemCode, ItemName, 1, ID_UOM" & _
                " FROM       tblItem_Master " & _
                " WHERE     ItemCode = @ItemCode"
        SQL.FlushParams()
        SQL.AddParam("@ItemCode", IIf(ItemCode = Nothing, "", ItemCode))
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            If UOM = "" Then
                UOM = SQL.SQLDR("ID_UOM").ToString
            End If
            If dgvItemMaster.SelectedCells.Count > 0 Then
                dgvItemMaster.Item(chItemCode.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemCode").ToString
                dgvItemMaster.Item(chItemName.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemName").ToString
                dgvItemMaster.Item(chUOM.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = UOM
                dgvItemMaster.Item(dgcBOMQTY.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = 1
                dgvItemMaster.Item(dgcReqQTY.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = 0
                dgvItemMaster.Item(chWHSE.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = IIf(cbWHto.SelectedIndex = -1, "", cbWHto.SelectedItem)
                LoadWHSE(dgvItemMaster.SelectedCells(0).RowIndex)
                LoadUOM(ItemCode, dgvItemMaster.SelectedCells(0).RowIndex)
            End If
            LoadBarCode()
            LoadStockPerLine()
        End If
    End Sub


    Private Sub LoadUOM(ItemCode As String, ByVal SelectedIndex As Integer)
        Try
            Dim bool As Boolean = True
            Dim strUOM As String = dgvItemMaster.Item(chUOM.Index, SelectedIndex).Value
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemMaster.Item(chUOM.Index, SelectedIndex)
            dgvCB.Items.Clear()
            ' ADD ALL WHSEc
            Dim query As String

            query = " SELECT DISTINCT UOM.UnitCode FROM tblItem_Master INNER JOIN  " & _
               " ( " & _
               " SELECT GroupCode, UnitCode FROM tblUOM_Group WHERE Status ='Active' " & _
               " UNION ALL " & _
               " SELECT GroupCode, UnitCodeFrom FROM tblUOM_GroupDetails " & _
               "  UNION ALL  " & _
               "    SELECT ItemCode, ItemUOM FROM tblItem_Master WHERE Status ='Active'  " & _
               " UNION ALL " & _
               " SELECT GroupCode, UnitCodeTo FROM tblUOM_GroupDetails " & _
               " ) AS UOM " & _
               " ON tblItem_Master.ItemUOMGroup = UOM.GroupCode " & _
               " OR  tblItem_Master.ItemCode = UOM.GroupCode " & _
               " WHERE ItemCode ='" & ItemCode & "' "
            SQL.ReadQuery(query)
            dgvCB.Items.Clear()
            While SQL.SQLDR.Read
                If strUOM = SQL.SQLDR("UnitCode").ToString Then
                    bool = False
                End If
                dgvCB.Items.Add(SQL.SQLDR("UnitCode").ToString)
            End While
            dgvCB.Items.Add("")
            If bool = True Then
                dgvCB.Value = ""
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub dgvItemMaster_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemMaster.CellClick
        Try
            If e.ColumnIndex = chUOM.Index Then
                If e.RowIndex <> -1 AndAlso dgvItemMaster.Rows.Count > 0 AndAlso dgvItemMaster.Item(chItemCode.Index, e.RowIndex).Value <> Nothing Then

                    LoadUOM(dgvItemMaster.Item(chItemCode.Index, e.RowIndex).Value.ToString, e.RowIndex)
                    Dim dgvCol As New DataGridViewComboBoxColumn
                    dgvCol = dgvItemMaster.Columns.Item(e.ColumnIndex)
                    dgvItemMaster.BeginEdit(True)
                    dgvCol.Selected = True
                    DirectCast(dgvItemMaster.EditingControl, DataGridViewComboBoxEditingControl).DroppedDown = True
                    Dim editingComboBox As ComboBox = TryCast(sender, ComboBox)

                End If
            ElseIf e.ColumnIndex = chWHSE.Index Then
                If e.RowIndex <> -1 AndAlso dgvItemMaster.Rows.Count > 0 AndAlso dgvItemMaster.Item(chItemCode.Index, e.RowIndex).Value <> Nothing Then
                    LoadWHSE(e.RowIndex)
                    Dim dgvCol As DataGridViewComboBoxColumn
                    dgvCol = dgvItemMaster.Columns.Item(e.ColumnIndex)
                    dgvItemMaster.BeginEdit(True)
                    dgvCol.Selected = True
                    DirectCast(dgvItemMaster.EditingControl, DataGridViewComboBoxEditingControl).DroppedDown = True
                    Dim editingComboBox As ComboBox = TryCast(sender, ComboBox)

                End If
            End If

        Catch ex As NullReferenceException
            If dgvItemMaster.ReadOnly = False Then
                SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub dgvItemMaster_EditingControlShowing(sender As System.Object, e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvItemMaster.EditingControlShowing
        ' GET THE EDITING CONTROL
        Dim editingComboBox As ComboBox = TryCast(e.Control, ComboBox)
        If Not editingComboBox Is Nothing Then
            ' REMOVE AN EXISTING EVENT-HANDLER TO AVOID ADDING MULTIPLE HANDLERS WHEN THE EDITING CONTROL IS REUSED
            RemoveHandler editingComboBox.SelectionChangeCommitted, New EventHandler(AddressOf editingComboBox_SelectionChangeCommitted)

            ' ADD THE EVENT HANDLER
            AddHandler editingComboBox.SelectionChangeCommitted, AddressOf editingComboBox_SelectionChangeCommitted

            ' PREVENT THIS HANDLER FROM FIRING TWICE
            RemoveHandler dgvItemMaster.EditingControlShowing, AddressOf dgvItemMaster_EditingControlShowing
        End If
    End Sub

    Private Sub editingComboBox_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rowIndex, columnIndex As Integer
        'Get the editing control
        Dim editingComboBox As ComboBox = TryCast(sender, ComboBox)
        If editingComboBox Is Nothing Then Exit Sub
        'Show your Message Boxes
        If editingComboBox.SelectedIndex <> -1 Then
            rowIndex = dgvItemMaster.SelectedCells(0).RowIndex
            columnIndex = dgvItemMaster.SelectedCells(0).ColumnIndex
            If dgvItemMaster.SelectedCells.Count > 0 Then
                Dim tempCell As DataGridViewComboBoxCell = dgvItemMaster.Item(columnIndex, rowIndex)
                Dim temp As String = editingComboBox.Text
                dgvItemMaster.Item(columnIndex, rowIndex).Selected = False
                dgvItemMaster.EndEdit(True)
                tempCell.Value = temp
                LoadStockPerLine()
                LoadBarcode()
            End If
        End If

        'Remove the handle to this event. It will be readded each time a new combobox selection causes the EditingControlShowing Event to fire
        RemoveHandler editingComboBox.SelectionChangeCommitted, AddressOf editingComboBox_SelectionChangeCommitted
        'Re-enable the EditingControlShowing event so the above can take place.
        AddHandler dgvItemMaster.EditingControlShowing, AddressOf dgvItemMaster_EditingControlShowing
    End Sub

    Private Sub LoadBarCode()
        Dim query As String
        Dim itemCode, UOM As String
        Dim Barcode As String
        For Each row As DataGridViewRow In dgvItemMaster.Rows
            If Not IsNothing(row.Cells(chItemCode.Index).Value) AndAlso Not IsNothing(row.Cells(chUOM.Index).Value) Then
                itemCode = row.Cells(chItemCode.Index).Value.ToString
                UOM = row.Cells(chUOM.Index).Value.ToString
                ' QUERY Barcode
                query = " SELECT Barcode FROM tblItem_Barcode WHERE UOM = @UOM AND ItemCode= @ItemCode AND STATUS <> 'Inactive'"
                SQL.FlushParams()
                SQL.AddParam("@ItemCode", itemCode)
                SQL.AddParam("@UOM", UOM)
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    Barcode = SQL.SQLDR("Barcode")
                    row.Cells(chBarcode.Index).Value = Barcode
                Else
                    row.Cells(chBarcode.Index).Value = ""
                End If
            End If

        Next
    End Sub

    Private Sub dgvItemMaster_DataError(sender As Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvItemMaster.DataError
        Try

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub tsbCopyPR_Click(sender As System.Object, e As System.EventArgs) Handles tsbCopyPR.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False

        f.ShowDialog("JO_GI")
        LoadJO(f.transID)
        f.Dispose()
    End Sub

    Private Sub LoadJO(ByVal ID As String)
        Try
            Dim qty, lotsize, cycle As Decimal
            Dim query, bom_code As String
            query = " SELECT tblJO.TransID, JO_No, DateJO, tblJO.ItemCode,  tblJO.Description, LotSize, tblBOM.UOM, tblJO.QTY, ProdLine, BOMCode " & _
                    " FROM   tblBOM INNER JOIN tblJO " & _
                    " ON     tblBOM.JO_Ref = tblJO.TransID " & _
                    " WHERE  tblJO.TransID ='" & ID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                txtJORef.Text = SQL.SQLDR("TransID")
                qty = SQL.SQLDR("QTY")
                lotsize = SQL.SQLDR("LotSize")
                bom_code = SQL.SQLDR("BOMCode")
                cycle = qty / lotsize
                Dim ctr As Integer = dgvItemMaster.Rows.Count - 1
                query = " SELECT    tblBOM_FG_Details.MaterialCode, ItemName, tblBOM_FG_Details.UOM, tblBOM_FG_Details.QTY  " & _
                        " FROM      tblBOM_FG INNER JOIN tblBOM_FG_Details " & _
                        " ON        tblBOM_FG.BOM_Code = tblBOM_FG_Details.BOM_Code " & _
                        " INNER JOIn tblItem_Master " & _
                        " ON        tblBOM_FG_Details.MaterialCode = tblItem_Master.ItemCode " & _
                        " WHERE     tblBOM_FG.Status ='Active' " & _
                        " AND       tblItem_Master.Status ='Active' " & _
                        " AND tblBOM_FG_Details.BOM_Code = '" & bom_code & "' " & _
                        " ORDER BY  LineNumber "
                SQL.GetQuery(query)
                If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                        dgvItemMaster.Rows.Add({row(0).ToString, "", row(1).ToString, row(2).ToString, row(3) * cycle, 0, 0, cbWHto.SelectedItem})
                        LoadUOM(row(0).ToString, ctr)
                        LoadWHSE(ctr)
                        ctr += 1
                    Next
                    LoadBarCode()
                    LoadStockPerLine()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Function LoadStock() As Boolean
        Dim available As Boolean = True
        If cbWHto.SelectedIndex <> -1 Then
            Dim WHSE As String
            WHSE = GetWHSE(cbWHto.SelectedItem)
            Dim query As String
            Dim itemCode As String
            Dim StockQTY As Decimal = 0
            Dim ReqQTY As Decimal = 0
            For Each row As DataGridViewRow In dgvItemMaster.Rows
                If Not IsNothing(row.Cells(chItemCode.Index).Value) Then
                    itemCode = row.Cells(chItemCode.Index).Value.ToString
                    If Not IsNumeric(row.Cells(dgcReqQTY.Index).Value) Then ReqQTY = 0 Else ReqQTY = CDec(row.Cells(dgcReqQTY.Index).Value)

                    query = "   SELECT	    ISNULL(SUM(QTY),0) AS QTY " & _
                            "   FROM		viewItem_Stock " & _
                            "   WHERE       ItemCode ='" & itemCode & "' " & _
                            "   AND         WHSE = '" & WHSE & "' "
                    SQL.ReadQuery(query)
                    If SQL.SQLDR.Read Then
                        StockQTY = SQL.SQLDR("QTY")
                        If StockQTY >= ReqQTY Then ' IF AVAILABLE STOCK IS GREATER THAN THE BOM REQUIREMENT THEN ISSUE QTY SHOULD BE THE SAME AS BOM QTY
                            row.DefaultCellStyle.BackColor = Color.White
                        Else ' IF AVAILABLE STOCK IS LESS THAN THE BOM REQUIREMENT THEN ISSUE QTY SHOULD BE ONLY THE STOCK QTY
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128)
                            available = False
                        End If
                    End If
                    '   row.Cells(chStock.Index).Value = CDec(StockQTY).ToString("N2")

                End If

            Next
        End If
        Return available
    End Function

    Private Function LoadStockPerLine() As Boolean
        Dim available As Boolean = True
        If cbWHto.SelectedIndex <> -1 Then
            Dim WHSE As String

            Dim query As String
            Dim itemCode As String
            Dim UOM As String
            Dim StockQTY As Decimal = 0
            Dim ReqQTY As Decimal = 0
            For Each row As DataGridViewRow In dgvItemMaster.Rows

                If Not IsNothing(row.Cells(chItemCode.Index).Value) AndAlso Not IsNothing(row.Cells(chUOM.Index).Value) Then
                    WHSE = GetWHSE(row.Cells(chWHSE.Index).Value)
                    itemCode = row.Cells(chItemCode.Index).Value.ToString
                    UOM = row.Cells(chUOM.Index).Value.ToString
                    If Not IsNumeric(row.Cells(dgcBOMQTY.Index).Value) Then ReqQTY = 0 Else ReqQTY = CDec(row.Cells(dgcBOMQTY.Index).Value)

                    query = "   SELECT	    ISNULL(SUM(BaseQTY),0) AS QTY " & _
                            "   FROM		viewItemStock_BaseUnit " & _
                            "   WHERE       ItemCode ='" & itemCode & "' " & _
                            "   AND         WHSE = '" & WHSE & "' "
                    SQL.ReadQuery(query)
                    If SQL.SQLDR.Read Then
                        StockQTY = SQL.SQLDR("QTY")

                        query = " SELECT ISNULL(SUM(QTY),1) AS QTY " &
                            " FROM viewItem_UOM " &
                            " WHERE GroupCode = @ItemCode AND UnitCode  = @UOM "
                        SQL.FlushParams()
                        SQL.AddParam("@ItemCode", itemCode)
                        SQL.AddParam("@UOM", UOM)
                        SQL.ReadQuery(query)
                        If SQL.SQLDR.Read Then
                            StockQTY = StockQTY / SQL.SQLDR("QTY")

                            If StockQTY >= ReqQTY Then ' IF AVAILABLE STOCK IS GREATER THAN THE BOM REQUIREMENT THEN ISSUE QTY SHOULD BE THE SAME AS BOM QTY
                                row.DefaultCellStyle.BackColor = Color.White
                            Else ' IF AVAILABLE STOCK IS LESS THAN THE BOM REQUIREMENT THEN ISSUE QTY SHOULD BE ONLY THE STOCK QTY
                                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128)
                                available = False
                            End If
                        End If

                    End If
                    'row.Cells(chStock.Index).Value = CDec(StockQTY).ToString("N2")
                    row.Cells(chStock.Index).Value = StockQTY

                End If

            Next
        End If
        Return available
    End Function

    Private Sub cbWHfrom_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbWHfrom.SelectedIndexChanged
        LoadStockPerLine()
    End Sub

    Private Sub FromBOMSFGToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromBOMSFGToolStripMenuItem.Click
        Dim f As New frmBOMSelect
        f.ShowDialog(ModuleID, "SFG")
        If Not IsNothing(f.cbBOM.SelectedItem) AndAlso f.nupQty.Value > 0 Then
            LoadMaterials("SFG", f.cbBOM.SelectedItem, f.nupQty.Value)
        End If
        f.Dispose()
    End Sub

    Private Sub LoadMaterials(ByVal Type As String, ByVal Code As String, ByVal QTY As Decimal)
        If cbWHfrom.SelectedIndex = -1 Then
            Msg("Please select warehouse first!", MsgBoxStyle.Exclamation)
        Else
            If Type = "SFG" Then
                Dim ctr As Integer = dgvItemMaster.Rows.Count - 1
                Dim query As String
                query = " SELECT    tblBOM_SFG_Details.MaterialCode, ItemName, tblBOM_SFG_Details.UOM, tblBOM_SFG_Details.QTY  " & _
                        " FROM      tblBOM_SFG INNER JOIN tblBOM_SFG_Details " & _
                        " ON        tblBOM_SFG.BOM_Code = tblBOM_SFG_Details.BOM_Code " & _
                        " INNER JOIn tblItem_Master " & _
                        " ON        tblBOM_SFG_Details.MaterialCode = tblItem_Master.ItemCode " & _
                        " WHERE     tblBOM_SFG.Status ='Active' " & _
                        " AND       tblItem_Master.Status ='Active' " & _
                        " AND       tblBOM_SFG.BOM_Code = '" & Code & "' " & _
                        " ORDER BY  LineNumber "
                SQL.GetQuery(query)
                If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                    txtSFGRef.Text = Code
                    For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                        dgvItemMaster.Rows.Add({row(0).ToString, "", row(1).ToString, row(2).ToString, row(3) * QTY, 0, 0, cbWHfrom.SelectedItem})
                        LoadUOM(row(0).ToString, ctr)
                        LoadWHSE(ctr)
                        ctr += 1
                    Next
                End If
            ElseIf Type = "FG" Then
                Dim ctr As Integer = dgvItemMaster.Rows.Count - 1
                Dim query As String
                query = " SELECT    tblBOM_FG_Details.MaterialCode, ItemName, tblBOM_FG_Details.UOM, tblBOM_FG_Details.QTY  " & _
                        " FROM      tblBOM_FG INNER JOIN tblBOM_FG_Details " & _
                        " ON        tblBOM_FG.BOM_Code = tblBOM_FG_Details.BOM_Code " & _
                        " INNER JOIn tblItem_Master " & _
                        " ON        tblBOM_FG_Details.MaterialCode = tblItem_Master.ItemCode " & _
                        " WHERE     tblBOM_FG.Status ='Active' " & _
                        " AND       tblItem_Master.Status ='Active' " & _
                        " AND       tblBOM_FG.BOM_Code = '" & Code & "' " & _
                        " ORDER BY  LineNumber "
                SQL.GetQuery(query)
                If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                    txtFGRef.Text = Code
                    For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                        dgvItemMaster.Rows.Add({row(0).ToString, "", row(1).ToString, row(2).ToString, row(3) * QTY, 0, 0, cbWHfrom.SelectedItem})
                        LoadUOM(row(0).ToString, ctr)
                        LoadWHSE(ctr)
                        ctr += 1
                    Next

                End If
            End If
            
            LoadBarCode()
            LoadStockPerLine()
        End If
    End Sub

    Private Sub cbWHto_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbWHto.SelectedIndexChanged

        If disableEvent = False Then
            If cbWHto.SelectedIndex <> -1 Then
                If cbWHto.SelectedItem = "Multiple Warehouse" Then
                    If tempWHSE <> "" Then
                        For Each row As DataGridViewRow In dgvItemMaster.Rows
                            row.Cells(chWHSE.Index).Value = tempWHSE
                        Next
                    End If
                    dgvItemMaster.Columns(chWHSE.Index).Visible = True
                Else
                    dgvItemMaster.Columns(chWHSE.Index).Visible = False
                    tempWHSE = Strings.Left(cbWHto.SelectedItem, cbWHto.SelectedItem.ToString.IndexOf(" | "))
                    For Each row As DataGridViewRow In dgvItemMaster.Rows
                        row.Cells(chWHSE.Index).Value = tempWHSE
                    Next
                    LoadStockPerLine()
                End If
            End If
        End If
    End Sub

    Private Sub FromBOMFGToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromBOMFGToolStripMenuItem.Click
        Dim f As New frmBOMSelect
        f.ShowDialog(ModuleID, "FG")
        If Not IsNothing(f.cbBOM.SelectedItem) AndAlso f.nupQty.Value > 0 Then
            LoadMaterials("FG", f.cbBOM.SelectedItem, f.nupQty.Value)
        End If
        f.Dispose()
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("MR", TransID)
        f.Dispose()
    End Sub

    Private Sub dgvItemMaster_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemMaster.CellContentClick

    End Sub


    Private Sub dgvItemMaster_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs)
        If e.ColumnIndex <> -1 AndAlso e.RowIndex <> -1 AndAlso e.Button = MouseButtons.Right Then
            Dim c As DataGridViewCell = sender(e.ColumnIndex, e.RowIndex)
            If Not c.Selected Then
                c.DataGridView.ClearSelection()
                c.DataGridView.CurrentCell = c
                c.Selected = True
            End If
        End If


    End Sub

    Private Sub dgvItemMaster_KeyDown(sender As Object, e As KeyEventArgs)
        If (e.KeyCode = Keys.F10 AndAlso e.Shift) OrElse e.KeyCode = Keys.Apps Then
            e.SuppressKeyPress = True
            Dim currentCell As DataGridViewCell = sender.CurrentCell
            If currentCell Is Nothing Then
                Dim cms As ContextMenuStrip = currentCell.ContextMenuStrip
                If cms IsNot Nothing Then
                    Dim r As Rectangle = currentCell.DataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, False)
                    Dim p As Point = New Point(r.X + r.Width, r.Y + r.Height)
                    cms.Show(currentCell.DataGridView, p)
                End If
            End If
        End If
    End Sub


    Private Sub ViewItemInformationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewItemInformationToolStripMenuItem.Click

        Dim f As New frmItem_Master
        If dgvItemMaster.CurrentCell IsNot Nothing Then
            If dgvItemMaster.CurrentCell.RowIndex <> -1 Then
                Dim code As String = dgvItemMaster.Rows(dgvItemMaster.CurrentCell.RowIndex).Cells(chItemCode.Index).Value.ToString
                If code <> "" Then
                    f.ShowDialog(code)
                Else
                    MsgBox("Please select an item first!", vbExclamation)
                End If
            End If
        End If
        f.Dispose()
    End Sub
End Class