Public Class frmBOMC
    Dim TransID, RefID, JETransID As String
    Dim BOMCNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "BOMC"
    Dim ColumnPK As String = "BOMC_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblBOMC"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim JO_ID, LineID As Integer
    Dim ForApproval As Boolean = False

    Public Overloads Function ShowDialog(ByVal docnumber As String) As Boolean
        TransID = docnumber
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmBOMC_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            loadWH()
            If TransID <> "" Then
                LoadBOMC(TransID)
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

    Private Sub LoadBOM()
        Dim query As String
        query = " SELECT BOM_Code FROM tblBOM_SFG WHERE Status ='Active' " & _
                " UNION ALL " & _
                " SELECT BOM_Code FROM tblBOM_FG WHERE Status ='Active' "
        SQL.ReadQuery(query)
        cbBOM.Items.Clear()
        While SQL.SQLDR.Read
            cbBOM.Items.Add(SQL.SQLDR("BOM_Code").ToString)
        End While
    End Sub

    Private Sub loadWHFrom()
        Dim query As String
        query = " SELECT    tblProdWarehouse.Code + ' | ' + Description AS WHSECode " & _
                " FROM      tblProdWarehouse INNER JOIN tblUser_Access " & _
                " ON        tblProdWarehouse.Code = tblUser_Access.Code " & _
                " AND       tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active' " & _
                " AND       tblUser_Access.Type = 'Production' AND isAllowed = 1 " & _
                " WHERE     UserID ='" & UserID & "' "
        SQL.ReadQuery(query)
        cbWHfrom.Items.Clear()
        Dim ctr As Integer = 0
        While SQL.SQLDR.Read
            cbWHfrom.Items.Add(SQL.SQLDR("WHSECode").ToString)
            ctr += 1
        End While
        If cbWHfrom.Items.Count > 0 Then
            cbWHfrom.SelectedIndex = 0
        End If
    End Sub

    Private Sub loadWH()
        Dim query As String
        query = " SELECT tblProdWarehouse.Code + ' | ' + Description AS Description  FROM tblProdWarehouse " & _
                " INNER JOIN tblUser_Access " & _
                " ON tblProdWarehouse.Code = tblUser_Access.Code " & _
                " AND tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active' " & _
                " AND tblUser_Access.Type = 'Production' AND isAllowed = 1 " & _
                " WHERE UserID ='" & UserID & "' " & _
                " UNION ALL " & _
                " SELECT tblWarehouse.Code + ' | ' + Description AS Description  FROM tblWarehouse " & _
                " INNER JOIN tblUser_Access " & _
                " ON tblWarehouse.Code = tblUser_Access.Code " & _
                " AND tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active' " & _
                " AND tblUser_Access.Type = 'Warehouse' AND isAllowed = 1 " & _
                " WHERE UserID ='" & UserID & "' "
        SQL.ReadQuery(query)
        cbWHfrom.Items.Clear()
        While SQL.SQLDR.Read
            cbWHfrom.Items.Add(SQL.SQLDR("Description"))
        End While
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        cbWHfrom.Enabled = Value
        dgvItemMaster.AllowUserToAddRows = Value
        dgvItemMaster.AllowUserToDeleteRows = Value
        dgvItemMaster.ReadOnly = Not Value
        nupQty.Enabled = Value
        btnAddMats.Enabled = Value
        If Value = True Then
            dgvItemMaster.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemMaster.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        txtItemName.Enabled = Value
        txtActualQTY.Enabled = Value
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        cbBOM.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
        dgcBOMQTY.ReadOnly = True
        chStock.ReadOnly = True
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("BOMC_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("BOMC")
            TransID = f.transID
            LoadBOMC(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub LoadBOMC(ByVal ID As String)
        Dim query As String
        Dim WHSE, BOM_Code, ItemCode, UOM, ItemName As String
        Dim BatchQTY, StandardQTY, ActualQTY, AvgCost As Decimal
        query = " SELECT   TransID, BOMC_No, DateBOMC, WHSE, Remarks, Status, ISNULL(Size,1) AS Size, " &
                " BOM_Code, ISNULL(BatchQTY,0) AS BatchQTY, BOM_ItemCode, ISNULL(StandardQTY,0) AS StandardQTY, UOM, ISNULL(ActualQTY,0) AS ActualQTY, ISNULL(JO_Ref,0) AS JO_Ref, ISNULL(AvgCost,0) AS AvgCost, " &
                "  ISNULL(TotalMaterialCost,0) AS TotalMaterialCost, ISNULL(TotalDLCost,0) AS  TotalDLCost, ISNULL(TotalFOCost,0) AS TotalFOCost,ISNULL(TotalCost,0) AS  TotalCost, ISNULL(JO_Ref_LineNum,0) AS JO_Ref_LineNum" &
                " FROM     tblBOMC " &
                " WHERE    TransId = '" & ID & "' " &
                " ORDER BY TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            JO_ID = SQL.SQLDR("JO_Ref").ToString
            LineID = SQL.SQLDR("JO_Ref_LineNum").ToString
            BOMCNo = SQL.SQLDR("BOMC_No").ToString
            txtTransNum.Text = BOMCNo
            dtpDocDate.Text = SQL.SQLDR("DateBOMC").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString

            txtDMcost.Text = CDec(SQL.SQLDR("TotalMaterialCost").ToString).ToString("N2")
            txtDLcost.Text = CDec(SQL.SQLDR("TotalDLCost").ToString).ToString("N2")
            txtFOcost.Text = CDec(SQL.SQLDR("TotalFOCost").ToString).ToString("N2")
            txtTotalCost.Text = CDec(SQL.SQLDR("TotalCost").ToString).ToString("N2")
            txtSize.Text = CDec(SQL.SQLDR("Size").ToString).ToString("N2")
            ActualQTY = CDec(SQL.SQLDR("ActualQTY").ToString).ToString("N2")
            AvgCost = CDec(SQL.SQLDR("AvgCost").ToString).ToString("N2")

            ItemCode = SQL.SQLDR("BOM_ItemCode").ToString
            UOM = SQL.SQLDR("UOM").ToString
            StandardQTY = SQL.SQLDR("StandardQTY").ToString
            BatchQTY = SQL.SQLDR("BatchQTY").ToString
            WHSE = SQL.SQLDR("WHSE").ToString
            BOM_Code = SQL.SQLDR("BOM_Code").ToString
            cbWHfrom.SelectedItem = GetWHSE(WHSE, "View_Warehouse_Production")
            LoadBOMType(ItemCode)
            ItemName = GetItemName(ItemCode)
            cbBOM.SelectedItem = GetBOMCode(BOM_Code)
            txtUOM.Text = UOM
            txtItemCode.Text = ItemCode
            txtItemName.Text = ItemName
            nupQty.Value = BatchQTY
            txtActualQTY.Text = ActualQTY
            txtAVGCost.Text = AvgCost
            txtQTY.Text = StandardQTY
            txtJORef.Text = LoadJONo(JO_ID).ToCharArray + "-" + LineID.ToString
            LoadBOMCDetails(TransID)
            LoadAccountingEntry(TransID)
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
            If dtpDocDate.Value < GetMaxInventoryDate() Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            Else
                If GetLast_InventoryOUT(TransID, ModuleID) Then
                    dtpDocDate.MinDate = GetMaxInventoryDate()
                Else
                    tsbEdit.Enabled = False
                    tsbCancel.Enabled = False
                End If
            End If

            If dtpDocDate.Value <= GetMaxPEC() Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            End If

            EnableControl(False)
        Else
            Cleartext()
        End If
    End Sub

    Private Function LoadJONo(JO_ID As Integer) As String
        Dim query As String
        query = " SELECT JO_No FROM tblJO WHERE TransID = '" & JO_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("JO_No")
        Else
            Return 0
        End If
    End Function

    Protected Sub LoadBOMCDetails(ByVal TransID As String)
        Dim ctr As Integer = 0
        Dim query As String
        query = " SELECT tblBOMC_Details.ItemCode, tblBOMC_Details.Description, tblBOMC_Details.UOM, tblBOMC_Details.BOM_QTY  AS BOM_QTY,  tblBOMC_Details.WHSE, ISNULL(tblBOMC_Details.AvailableStock,0) as AvailableStock, ISNULL(tblBOMC_Details.QTY,0) as QTY, " & _
                " ISNULL(tblBOMC_Details.UnitCost,0) as UnitCost, ISNULL(tblBOMC_Details.TotalCost,0) as TotalCost" & _
                " FROM tblBOMC INNER JOIN tblBOMC_Details " & _
                " ON tblBOMC.TransID = tblBOMC_Details.TransID " & _
                " WHERE tblBOMC_Details.TransId = " & TransID & " " & _
                " ORDER BY tblBOMC_Details.LineNum "
        disableEvent = True
        dgvItemMaster.Rows.Clear()
        disableEvent = False
        SQL.ReadQuery(query)
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvItemMaster.Rows.Add(row(0).ToString, row(1).ToString, row(2).ToString, _
                                     CDec(row(3).ToString), CDec(row(5).ToString), GetWHSEDesc(row(4).ToString), CDec(row(6).ToString), CDec(row(7).ToString), CDec(row(8).ToString))
                LoadUOM(row(0).ToString, ctr)
                LoadWHSE(ctr)
                ctr += 1
            Next
        End If
    End Sub

    Private Sub LoadAccountingEntry(ByVal TransID As Integer)
        Try
            Dim query As String
            query = " SELECT JE_No, VCECode, VCEName, AccntCode, AccntTitle, Particulars, RefNo, " & _
                     "        ISNULL(Debit,0) AS Debit, ISNULL(Credit,0) AS Credit " & _
                     " FROM   View_GL_Transaction  " & _
                     " WHERE  RefType ='BOMC' AND RefTransID = '" & TransID & "' "
            SQL.ReadQuery(query)
            dgvEntry.Rows.Clear()
            If SQL.SQLDR.HasRows Then
                While SQL.SQLDR.Read
                    JETransiD = SQL.SQLDR("JE_No").ToString
                    dgvEntry.Rows.Add(SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccntTitle").ToString, _
                                      IIf(CDec(SQL.SQLDR("Debit")) = 0, "", CDec(SQL.SQLDR("Debit")).ToString("N2")), If(CDec(SQL.SQLDR("Credit")) = 0, "", CDec(SQL.SQLDR("Credit")).ToString("N2")), _
                                      SQL.SQLDR("Particulars").ToString, SQL.SQLDR("VCECode").ToString, SQL.SQLDR("VCEName").ToString)
                End While
                TotalDBCR()
            Else
                JETransiD = 0
                dgvEntry.Rows.Clear()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Function GetBOMCode(ByVal Code As String) As String
        Try
            Dim query As String
            query = " SELECT * FROM ( " & _
                    " SELECT BOM_Code  + ' | ' + ISNULL(Remarks,'') AS Description, ItemCode, BOM_Code  FROm tblBOM_FG " & _
                    " UNION ALL " & _
                    " SELECT BOM_Code  + ' | ' + ISNULL(Remarks,'') AS Description, ItemCode, BOM_Code  FROm tblBOM_SFG ) AS A " & _
                    "  WHERE BOM_Code ='" & Code & "' "
            SQL.FlushParams()
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Return SQL.SQLDR("Description").ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, "modGen", "modGen")
            Return ""
        Finally
            SQL.FlushParams()
        End Try

    End Function


    Private Sub LoadWHSE(Optional ByVal SelectedIndex As Integer = -1)
        Try
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemMaster.Item(chWHSE.Index, SelectedIndex)
            dgvCB.Items.Clear()
            ' ADD ALL WHSEc
            Dim query As String
            query = " SELECT Description FROM View_Warehouse_Production WHERE Status ='Active' "
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
        txtItemCode.Clear()
        txtItemName.Clear()
        txtActualQTY.Clear()
        txtJORef.Clear()
        txtQTY.Clear()
        txtUOM.Clear()
        nupQty.Value = 1
        dgvItemMaster.Rows.Clear()
        dgvEntry.Rows.Clear()

        txtAVGCost.Text = "0.00"
        txtDLcost.Text = "0.00"
        txtDMcost.Text = "0.00"
        txtFOcost.Text = "0.00"
        txtTotalCost.Text = "0.00"

        txtRemarks.Clear()
        txtStatus.Text = "Open"
        dtpDocDate.MinDate = GetMaxPEC().AddDays(1)
        dtpDocDate.Value = Date.Today.Date
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("BOMC_ADD") Then
            msgRestricted()
        Else
            Cleartext()
            TransID = ""
            BOMCNo = ""
            JO_ID = 0
            LineID = 0

            dgvItemMaster.Columns(dgcBOMQTY.Index).Visible = True
            dgvItemMaster.Columns(chStock.Index).Visible = True

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
        If Not AllowAccess("BOMC_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)

            dgvItemMaster.Columns(chItemCode.Index).ReadOnly = True
            dgvItemMaster.Columns(chItemName.Index).ReadOnly = True
            dgvItemMaster.Columns(chUOM.Index).ReadOnly = True
            dgvItemMaster.Columns(dgcBOMQTY.Index).ReadOnly = True
            dgvItemMaster.Columns(dgcBOMQTY.Index).ReadOnly = True
            dgvItemMaster.Columns(chStock.Index).ReadOnly = True
            dgvItemMaster.Columns(chStandardCost.Index).ReadOnly = True
            dgvItemMaster.Columns(chTotalCost.Index).ReadOnly = True

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
        If cbWHfrom.SelectedIndex = -1 Then
            Msg("Please select warehouse!", MsgBoxStyle.Exclamation)
        ElseIf txtActualQTY.Text = "" Then
            Msg("Please input actual usage!", MsgBoxStyle.Exclamation)
        ElseIf Not LoadStock() And TransID = "" Then
            Msg("Invalid request quantity!" & vbNewLine & "Cannot request more than the available stock!", MsgBoxStyle.Exclamation)
        ElseIf TransID = "" Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                TransID = GenerateTransID(ColumnID, DBTable)
                BOMCNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                txtTransNum.Text = BOMCNo
                GenerateEntry()
                SaveBOMC()
                Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                LoadBOMC(TransID)
            End If
        Else
            If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                GenerateEntry()
                UpdateBOMC()
                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                LoadBOMC(TransID)
            End If
        End If
    End Sub


    Private Sub SaveBOMC()
        Try
            Dim WHSE, BOM_Code As String
            WHSE = GetWHSE(cbWHfrom.SelectedItem)
            BOM_Code = GetBOM(cbBOM.SelectedItem)

            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " &
                        " tblBOMC(TransID, BOMC_No, BranchCode, BusinessCode, DateBOMC, WHSE, BOM_Code, BatchQTY, BOM_ItemCode, StandardQTY, Size, UOM, ActualQTY, " &
                        "       AVGCost, TotalMaterialCost, TotalDLCost, TotalFOCost, TotalCost, Remarks, WhoCreated, JO_REf, JO_Ref_LineNum, Status) " &
                        " VALUES (@TransID, @BOMC_No, @BranchCode, @BusinessCode, @DateBOMC, @WHSE, @BOM_Code, @BatchQTY, @BOM_ItemCode, @StandardQTY, @Size, @UOM, @ActualQTY, " &
                        "       @AVGCost, @TotalMaterialCost, @TotalDLCost, @TotalFOCost, @TotalCost, @Remarks, @WhoCreated, @JO_REf, @JO_Ref_LineNum, @Status) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@BOMC_No", BOMCNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateBOMC", dtpDocDate.Value.Date)
            SQL.AddParam("@WHSE", WHSE)
            SQL.AddParam("@BOM_Code", BOM_Code)
            SQL.AddParam("@BatchQTY", nupQty.Value)
            SQL.AddParam("@BOM_ItemCode", txtItemCode.Text)
            SQL.AddParam("@StandardQTY", txtQTY.Text)
            SQL.AddParam("@Size", txtSize.Text)
            SQL.AddParam("@UOM", txtUOM.Text)
            SQL.AddParam("@ActualQTY", CDec(txtActualQTY.Text))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@AVGCost", CDec(txtAVGCost.Text))
            SQL.AddParam("@TotalMaterialCost", CDec(txtDMcost.Text))
            SQL.AddParam("@TotalDLCost", CDec(txtDLcost.Text))
            SQL.AddParam("@TotalFOCost", CDec(txtFOcost.Text))
            SQL.AddParam("@TotalCost", CDec(txtTotalCost.Text))
            SQL.AddParam("@JO_REf", JO_ID)
            SQL.AddParam("@JO_Ref_LineNum", LineID)
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            Dim ItemCode, Description, UOM As String
            Dim QTY, UnitCost, TotalCost, BOM_QTY, AvailableStock As Decimal
            For Each row As DataGridViewRow In dgvItemMaster.Rows
                If Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                    Description = IIf(row.Cells(chItemName.Index).Value = Nothing, "", row.Cells(chItemName.Index).Value)
                    UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                    AvailableStock = IIf(row.Cells(chStock.Index).Value = Nothing, 0, row.Cells(chStock.Index).Value)
                    QTY = IIf(row.Cells(chActualQTY.Index).Value = Nothing, 0, row.Cells(chActualQTY.Index).Value)
                    BOM_QTY = IIf(row.Cells(dgcBOMQTY.Index).Value = Nothing, 0, row.Cells(dgcBOMQTY.Index).Value)
                    'UnitCost = GetAverageCost(ItemCode)
                    UnitCost = IIf(row.Cells(chStandardCost.Index).Value = Nothing, 0, row.Cells(chStandardCost.Index).Value)
                    TotalCost = IIf(row.Cells(chTotalCost.Index).Value = Nothing, 0, row.Cells(chTotalCost.Index).Value)
                    insertSQL = " INSERT INTO " & _
                         " tblBOMC_Details(TransId, ItemCode, Description, UOM, QTY, BOM_QTY, AvailableStock, WHSE, UnitCost, TotalCost, LineNum, WhoCreated) " & _
                         " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @BOM_QTY, @AvailableStock, @WHSE,  @UnitCost, @TotalCost, @LineNum, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", ItemCode)
                    SQL.AddParam("@Description", Description)
                    SQL.AddParam("@UOM", UOM)
                    SQL.AddParam("@QTY", QTY)
                    SQL.AddParam("@BOM_QTY", BOM_QTY)
                    SQL.AddParam("@AvailableStock", AvailableStock)
                    SQL.AddParam("@WHSE", WHSE)
                    SQL.AddParam("@UnitCost", UnitCost)
                    SQL.AddParam("@TotalCost", TotalCost)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1


                    SaveINVTY("OUT", ModuleID, "BOMC", TransID, dtpDocDate.Value.Date, ItemCode, WHSE, QTY, UOM, UnitCost)
                    If txtItemCode.Text = "" Then
                        SaveINVTY("IN", ModuleID, "BOMC", TransID, dtpDocDate.Value.Date, ItemCode, WHSE, QTY, UOM, UnitCost)
                    End If
                End If
            Next
            If txtItemCode.Text <> "" Then
                SaveINVTY("IN", ModuleID, "BOMC", TransID, dtpDocDate.Value.Date, txtItemCode.Text, WHSE, txtActualQTY.Text, txtUOM.Text, txtAVGCost.Text)
            End If
            ComputeWAUC("BOMC", TransID)


            JETransID = GenerateTransID("JE_No", "tblJE_Header")

            insertSQL = " INSERT INTO " & _
                        " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR,   Remarks, WhoCreated, Status) " & _
                        " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR,  @Remarks, @WhoCreated, @Status)"
            SQL.FlushParams()
            SQL.AddParam("@JE_No", JETransID)
            SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
            SQL.AddParam("@RefType", "BOMC")
            SQL.AddParam("@RefTransID", TransID)
            SQL.AddParam("@Book", "Inventory")
            SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Saved")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            line = 1
            For Each item As DataGridViewRow In dgvEntry.Rows
                If item.Cells(chAccntCode.Index).Value <> Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber) " & _
                                " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber)"
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransID)
                    SQL.AddParam("@AccntCode", item.Cells(chAccntCode.Index).Value.ToString)
                    If item.Cells(chVCECode.Index).Value <> Nothing AndAlso item.Cells(chVCECode.Index).Value <> "" Then
                        SQL.AddParam("@VCECode", item.Cells(chVCECode.Index).Value.ToString)
                    Else
                        SQL.AddParam("@VCECode", "")
                    End If
                    If item.Cells(chDebit.Index).Value AndAlso IsNumeric(item.Cells(chDebit.Index).Value) Then
                        SQL.AddParam("@Debit", CDec(item.Cells(chDebit.Index).Value))
                    Else
                        SQL.AddParam("@Debit", 0)
                    End If
                    If item.Cells(chCredit.Index).Value <> Nothing AndAlso IsNumeric(item.Cells(chCredit.Index).Value) Then
                        SQL.AddParam("@Credit", CDec(item.Cells(chCredit.Index).Value))
                    Else
                        SQL.AddParam("@Credit", 0)
                    End If
                    If item.Cells(chParticulars.Index).Value <> Nothing AndAlso item.Cells(chParticulars.Index).Value <> "" Then
                        SQL.AddParam("@Particulars", item.Cells(chParticulars.Index).Value.ToString)
                    Else
                        SQL.AddParam("@Particulars", "")
                    End If
                    If item.Cells(chParticulars.Index).Value <> Nothing AndAlso item.Cells(chParticulars.Index).Value <> "" Then
                        SQL.AddParam("@RefNo", item.Cells(chParticulars.Index).Value.ToString)
                    Else
                        SQL.AddParam("@RefNo", "")
                    End If
                    SQL.AddParam("@LineNumber", line)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next

            'Dim updateSQL As String
            'updateSQL = "UPDATE tblJO SET Status ='Closed' WHERE TransID = " & JO_ID & " "
            'SQL.ExecNonQuery(updateSQL)

        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "BOMC_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub UpdateBOMC()
        Try
            Dim WHSE, BOM_Code As String

            WHSE = GetWHSE(cbWHfrom.SelectedItem)
            BOM_Code = GetBOM(cbBOM.SelectedItem)

            activityStatus = True
            Dim insertSQL, updateSQL, deleteSQL As String
            updateSQL = " UPDATE    tblBOMC " &
                        " SET       BOMC_No = @BOMC_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, DateBOMC = @DateBOMC, " &
                        "           WHSE = @WHSE,  Remarks = @Remarks, BOM_Code = @BOM_Code, BatchQTY = @BatchQTY, Size = @Size," &
                        "           BOM_ItemCode = @BOM_ItemCode, StandardQTY = @StandardQTY, UOM = @UOM, ActualQTY = @ActualQTY, " &
                        "           AVGCost = @AVGCost, TotalMaterialCost = @TotalMaterialCost, TotalDLCost = @TotalDLCost, TotalFOCost = @TotalFOCost, TotalCost = @TotalCost, " &
                        "           JO_Ref_LineNum = @JO_Ref_LineNum, WhoModified = @WhoModified, DateModified = GETDATE() , JO_REF = @JO_REF" &
                        " WHERE     TransID = @TransID  "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@BOMC_No", BOMCNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateBOMC", dtpDocDate.Value.Date)
            SQL.AddParam("@WHSE", WHSE)
            SQL.AddParam("@BOM_Code", BOM_Code)
            SQL.AddParam("@BatchQTY", nupQty.Value)
            SQL.AddParam("@BOM_ItemCode", txtItemCode.Text)
            SQL.AddParam("@StandardQTY", txtQTY.Text)
            SQL.AddParam("@Size", txtSize.Text)
            SQL.AddParam("@UOM", txtUOM.Text)
            SQL.AddParam("@ActualQTY", CDec(txtActualQTY.Text))
            SQL.AddParam("@AVGCost", CDec(txtAVGCost.Text))
            SQL.AddParam("@TotalMaterialCost", CDec(txtDMcost.Text))
            SQL.AddParam("@TotalDLCost", CDec(txtDLcost.Text))
            SQL.AddParam("@TotalFOCost", CDec(txtFOcost.Text))
            SQL.AddParam("@TotalCost", CDec(txtTotalCost.Text))
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@WhoModified", UserID)
            SQL.AddParam("@JO_REF", JO_ID)
            SQL.AddParam("@JO_Ref_LineNum", LineID)
            SQL.ExecNonQuery(updateSQL)

            deleteSQL = " DELETE FROM tblBOMC_Details WHERE TransID =@TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.ExecNonQuery(deleteSQL)


            DELINVTY(ModuleID, "BOMC", TransID)

            Dim line As Integer = 1
            Dim ItemCode, Description, UOM As String
            Dim QTY, UnitCost, TotalCost, BOM_QTY, AvailableStock As Decimal
            For Each row As DataGridViewRow In dgvItemMaster.Rows
                If Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                    Description = IIf(row.Cells(chItemName.Index).Value = Nothing, "", row.Cells(chItemName.Index).Value)
                    UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                    AvailableStock = IIf(row.Cells(chStock.Index).Value = Nothing, 0, row.Cells(chStock.Index).Value)
                    QTY = IIf(row.Cells(chActualQTY.Index).Value = Nothing, 0, row.Cells(chActualQTY.Index).Value)
                    BOM_QTY = IIf(row.Cells(dgcBOMQTY.Index).Value = Nothing, 0, row.Cells(dgcBOMQTY.Index).Value)
                    'UnitCost = GetAverageCost(ItemCode)
                    UnitCost = IIf(row.Cells(chStandardCost.Index).Value = Nothing, 0, row.Cells(chStandardCost.Index).Value)
                    TotalCost = IIf(row.Cells(chTotalCost.Index).Value = Nothing, 0, row.Cells(chTotalCost.Index).Value)
                    insertSQL = " INSERT INTO " & _
                         " tblBOMC_Details(TransId, ItemCode, Description, UOM, QTY, BOM_QTY, AvailableStock, WHSE, UnitCost, TotalCost, LineNum, WhoCreated) " & _
                         " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @BOM_QTY, @AvailableStock, @WHSE,  @UnitCost, @TotalCost, @LineNum, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", ItemCode)
                    SQL.AddParam("@Description", Description)
                    SQL.AddParam("@UOM", UOM)
                    SQL.AddParam("@QTY", QTY)
                    SQL.AddParam("@BOM_QTY", BOM_QTY)
                    SQL.AddParam("@AvailableStock", AvailableStock)
                    SQL.AddParam("@WHSE", WHSE)
                    SQL.AddParam("@UnitCost", UnitCost)
                    SQL.AddParam("@TotalCost", TotalCost)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1


                    SaveINVTY("OUT", ModuleID, "BOMC", TransID, dtpDocDate.Value.Date, ItemCode, WHSE, QTY, UOM, UnitCost)
                    If txtItemCode.Text = "" Then
                        SaveINVTY("IN", ModuleID, "BOMC", TransID, dtpDocDate.Value.Date, ItemCode, WHSE, QTY, UOM, UnitCost)
                    End If
                End If
            Next
            If txtItemCode.Text <> "" Then
                SaveINVTY("IN", ModuleID, "BOMC", TransID, dtpDocDate.Value.Date, txtItemCode.Text, WHSE, txtActualQTY.Text, txtUOM.Text, txtAVGCost.Text)
            End If
            ComputeWAUC("BOMC", TransID)



            JETransID = LoadJE("BOMC", TransID)

            If JETransID = 0 Then

                JETransID = GenerateTransID("JE_No", "tblJE_Header")

                insertSQL = " INSERT INTO " & _
                            " tblJE_Header (JE_No,AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR,   Remarks, WhoCreated) " & _
                            " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR,   @Remarks, @WhoCreated)"
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransID)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "BOMC")
                SQL.AddParam("@RefTransID", TransID)
                SQL.AddParam("@Book", "Inventory")
                SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@WhoCreated", UserID)
                SQL.ExecNonQuery(insertSQL)

                ' JETransiD = LoadJE("JV", TransID)
            Else
                updateSQL = " UPDATE tblJE_Header " & _
                            " SET    AppDate = @AppDate, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " & _
                            "        RefType = @RefType, RefTransID = @RefTransID, Book = @Book, TotalDBCR = @TotalDBCR, " & _
                            "        Remarks = @Remarks, WhoModified = @WhoModified, DateModified = GETDATE() " & _
                            " WHERE  JE_No = @JE_No "
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransID)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "BOMC")
                SQL.AddParam("@RefTransID", TransID)
                SQL.AddParam("@Book", "Inventory")
                SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@WhoModified", UserID)
                SQL.ExecNonQuery(updateSQL)
            End If


            ' DELETE ACCOUNTING ENTRIES
            deleteSQL = " DELETE FROM tblJE_Details  WHERE  JE_No = @JE_No "
            SQL.FlushParams()
            SQL.AddParam("@JE_No", JETransID)
            SQL.ExecNonQuery(deleteSQL)


            line = 1
            ' INSERT NEW ENTRIES
             For Each item As DataGridViewRow In dgvEntry.Rows
                If item.Cells(chAccntCode.Index).Value <> Nothing Then
                    insertSQL = " INSERT INTO " & _
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber) " & _
                                " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber)"
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransID)
                    SQL.AddParam("@AccntCode", item.Cells(chAccntCode.Index).Value.ToString)
                    If item.Cells(chVCECode.Index).Value <> Nothing AndAlso item.Cells(chVCECode.Index).Value <> "" Then
                        SQL.AddParam("@VCECode", item.Cells(chVCECode.Index).Value.ToString)
                    Else
                        SQL.AddParam("@VCECode", "")
                    End If
                    If item.Cells(chDebit.Index).Value AndAlso IsNumeric(item.Cells(chDebit.Index).Value) Then
                        SQL.AddParam("@Debit", CDec(item.Cells(chDebit.Index).Value))
                    Else
                        SQL.AddParam("@Debit", 0)
                    End If
                    If item.Cells(chCredit.Index).Value <> Nothing AndAlso IsNumeric(item.Cells(chCredit.Index).Value) Then
                        SQL.AddParam("@Credit", CDec(item.Cells(chCredit.Index).Value))
                    Else
                        SQL.AddParam("@Credit", 0)
                    End If
                    If item.Cells(chParticulars.Index).Value <> Nothing AndAlso item.Cells(chParticulars.Index).Value <> "" Then
                        SQL.AddParam("@Particulars", item.Cells(chParticulars.Index).Value.ToString)
                    Else
                        SQL.AddParam("@Particulars", "")
                    End If
                    If item.Cells(chParticulars.Index).Value <> Nothing AndAlso item.Cells(chParticulars.Index).Value <> "" Then
                        SQL.AddParam("@RefNo", item.Cells(chParticulars.Index).Value.ToString)
                    Else
                        SQL.AddParam("@RefNo", "")
                    End If
                    SQL.AddParam("@LineNumber", line)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next

            'updateSQL = "UPDATE tblJO SET Status ='Closed' WHERE TransID = " & JO_ID & " "
            'SQL.ExecNonQuery(updateSQL)
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "BOMC_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("BOMC_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE  tblBOMC SET Status ='Cancelled' WHERE BOMC_No = @BOMC_No "
                        SQL.FlushParams()
                        BOMCNo = txtTransNum.Text
                        SQL.AddParam("@BOMC_No", BOMCNo)
                        SQL.ExecNonQuery(deleteSQL)

                        Dim line As Integer = 1
                        Dim ItemCode, Description, UOM, WHSE As String
                        Dim QTY, UnitCost, TotalCost, BOM_QTY, AvailableStock As Decimal
                        WHSE = GetWHSE(cbWHfrom.SelectedItem)
                        For Each row As DataGridViewRow In dgvItemMaster.Rows
                            If Not row.Cells(chItemCode.Index).Value Is Nothing Then
                                ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                                Description = IIf(row.Cells(chItemName.Index).Value = Nothing, "", row.Cells(chItemName.Index).Value)
                                UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                                AvailableStock = IIf(row.Cells(chStock.Index).Value = Nothing, 0, row.Cells(chStock.Index).Value)
                                QTY = IIf(row.Cells(chActualQTY.Index).Value = Nothing, 0, row.Cells(chActualQTY.Index).Value)
                                BOM_QTY = IIf(row.Cells(dgcBOMQTY.Index).Value = Nothing, 0, row.Cells(dgcBOMQTY.Index).Value)
                                UnitCost = GetAverageCost(ItemCode)

                                line += 1
                                SaveINVTY("IN", ModuleID, "BOMC", TransID, dtpDocDate.Value.Date, ItemCode, WHSE, QTY, UOM, UnitCost)
                                If txtItemCode.Text = "" Then
                                    SaveINVTY("OUT", ModuleID, "BOMC", TransID, dtpDocDate.Value.Date, ItemCode, WHSE, QTY, UOM, UnitCost)
                                End If
                                TotalCost += UnitCost
                            End If
                        Next
                        If txtItemCode.Text <> "" Then
                            SaveINVTY("OUT", ModuleID, "BOMC", TransID, dtpDocDate.Value.Date, txtItemCode.Text, WHSE, txtActualQTY.Text, txtUOM.Text, UnitCost)
                        End If
                        ComputeWAUC("BOMC", TransID)



                        JETransID = LoadJE("BOMC", TransID)
                        deleteSQL = " UPDATE tblJE_Header " & _
                          " SET    Status = @Status,  WhoModified = @WhoModified, DateModified = GETDATE()" & _
                          " WHERE  JE_No = @JE_No "
                        SQL.FlushParams()
                        SQL.AddParam("@JE_No", JETransID)
                        SQL.AddParam("@Status", "Cancelled")
                        SQL.AddParam("@WhoModified", UserID)
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

                        LoadBOMC(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "BOMC_No", BOMCNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If BOMCNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblBOMC  " & _
                    "  LEFT JOIN " & _
                    "  (SELECT    DISTINCT tblProdWarehouse.Code                                " & _
                    "  FROM      tblProdWarehouse  " & _
                    "  INNER JOIN tblUser_Access  ON         " & _
                    "  tblProdWarehouse.Code = tblUser_Access.Code   AND        " & _
                    "  tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active'   " & _
                    "   AND       tblUser_Access.Type = 'Production' AND isAllowed = 1   " & _
                    " WHERE UserID ='" & UserID & "' " & _
                    " UNION ALL" & _
                    " SELECT    DISTINCT tblWarehouse.Code                                " & _
                    "  FROM      tblWarehouse  " & _
                    "  INNER JOIN tblUser_Access  ON         " & _
                    "  tblWarehouse.Code = tblUser_Access.Code   AND        " & _
                    "  tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active'   " & _
                    "   AND       tblUser_Access.Type = 'Warehouse' AND isAllowed = 1   " & _
                    " WHERE UserID ='" & UserID & "') AS WAREHOUSE " & _
                    " ON WAREHOUSE.Code = tblBOMC.WHSE WHERE BOMC_No < '" & BOMCNo & "' ORDER BY BOMC_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadBOMC(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If BOMCNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblBOMC  " & _
                    "  LEFT JOIN " & _
                    "  (SELECT    DISTINCT tblProdWarehouse.Code                                " & _
                    "  FROM      tblProdWarehouse  " & _
                    "  INNER JOIN tblUser_Access  ON         " & _
                    "  tblProdWarehouse.Code = tblUser_Access.Code   AND        " & _
                    "  tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active'   " & _
                    "   AND       tblUser_Access.Type = 'Production' AND isAllowed = 1   " & _
                    " WHERE UserID ='" & UserID & "' " & _
                    " UNION ALL" & _
                    " SELECT    DISTINCT tblWarehouse.Code                                " & _
                    "  FROM      tblWarehouse  " & _
                    "  INNER JOIN tblUser_Access  ON         " & _
                    "  tblWarehouse.Code = tblUser_Access.Code   AND        " & _
                    "  tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active'   " & _
                    "   AND       tblUser_Access.Type = 'Warehouse' AND isAllowed = 1   " & _
                    " WHERE UserID ='" & UserID & "') AS WAREHOUSE " & _
                    " ON WAREHOUSE.Code = tblBOMC.WHSE WHERE BOMC_No > '" & BOMCNo & "' ORDER BY BOMC_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadBOMC(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If BOMCNo = "" Then
            Cleartext()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadBOMC(TransID)
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

    Private Sub frmGI_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub cbBOM_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbBOM.SelectedIndexChanged
        If disableEvent = False Then
            LoadBOMData(GetBOM(cbBOM.SelectedItem))
        End If

    End Sub

    Private Function LoadStock() As Boolean
        Dim available As Boolean = True
        If cbWHfrom.SelectedIndex <> -1 Then
            Dim WHSE As String
            WHSE = GetWHSE(cbWHfrom.SelectedItem)
            Dim query As String
            Dim itemCode As String
            Dim StockQTY As Decimal = 0
            Dim ReqQTY As Decimal = 0
            For Each row As DataGridViewRow In dgvItemMaster.Rows
                If Not IsNothing(row.Cells(chItemCode.Index).Value) Then
                    itemCode = row.Cells(chItemCode.Index).Value.ToString
                    If Not IsNumeric(row.Cells(chActualQTY.Index).Value) Then ReqQTY = 0 Else ReqQTY = CDec(row.Cells(chActualQTY.Index).Value)
                    Dim UOM As String = row.Cells(chUOM.Index).Value.ToString
                    query = "   SELECT ISNULL(SUM(QTY),0) AS QTY " &
                            "   FROM viewItem_Stock " &
                            "   WHERE ItemCode ='" & itemCode & "' " &
                            "   AND WHSE = '" & WHSE & "' AND UOM = '" & UOM & "' "
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
                    row.Cells(chStock.Index).Value = CDec(StockQTY).ToString("N2")

                End If

            Next
        End If
        Return available
    End Function

    Private Sub LoadBOMData(ByVal BOM_Code As String)
        If cbBOM.SelectedIndex <> -1 Then
            Dim query As String
            query = " SELECT    BOM_Code, tblBOM_SFG.ItemCode, ItemName, UOM, QTY  " & _
                    " FROM      tblBOM_SFG INNER JOIn tblItem_Master " & _
                    " ON        tblBOM_SFG.ItemCode = tblItem_Master.ItemCode " & _
                    " WHERE     tblBOM_SFG.Status ='Active' " & _
                    " AND       tblItem_Master.Status ='Active' " & _
                    " AND       BOM_Code ='" & BOM_Code & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                txtItemCode.Text = SQL.SQLDR("ItemCode").ToString
                txtItemName.Text = SQL.SQLDR("ItemName").ToString
                txtUOM.Text = SQL.SQLDR("UOM").ToString
                txtQTY.Text = SQL.SQLDR("QTY").ToString * nupQty.Value
            Else
                query = " SELECT    BOM_Code, tblBOM_FG.ItemCode, ItemName, UOM, QTY  " & _
                 " FROM      tblBOM_FG INNER JOIn tblItem_Master " & _
                 " ON        tblBOM_FG.ItemCode = tblItem_Master.ItemCode " & _
                 " WHERE     tblBOM_FG.Status ='Active' " & _
                 " AND       tblItem_Master.Status ='Active' " & _
                 " AND       BOM_Code ='" & BOM_Code & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    txtItemCode.Text = SQL.SQLDR("ItemCode").ToString
                    txtItemName.Text = SQL.SQLDR("ItemName").ToString
                    txtUOM.Text = SQL.SQLDR("UOM").ToString
                    txtQTY.Text = SQL.SQLDR("QTY").ToString * nupQty.Value
                Else
                    txtItemCode.Text = ""
                    txtItemName.Text = ""
                    txtUOM.Text = ""
                    txtQTY.Text = 1
                End If
            End If
        End If
    End Sub

    Private Sub nupQty_ValueChanged(sender As System.Object, e As System.EventArgs) Handles nupQty.ValueChanged
        If disableEvent = False Then
            If cbBOM.SelectedIndex <> -1 Then
                LoadBOMData(GetBOM(cbBOM.SelectedItem))
            End If
        End If
    End Sub

    Private Sub dgvItemMaster_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemMaster.CellEndEdit
        Try
            Dim itemCode As String
            Dim rowIndex As Integer = dgvItemMaster.CurrentCell.RowIndex
            Dim colindex As Integer = dgvItemMaster.CurrentCell.ColumnIndex
            Select Case colindex
                Case chItemCode.Index
                    If dgvItemMaster.Item(chItemCode.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemMaster.Item(chItemCode.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("All Item", itemCode)
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItemDetails(itemCode)
                        Else
                            dgvItemMaster.Rows.RemoveAt(e.RowIndex)
                        End If
                        f.Dispose()
                    End If
                Case chItemName.Index
                    If dgvItemMaster.Item(chItemName.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemMaster.Item(chItemName.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("All Item", itemCode)
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItemDetails(itemCode)
                        Else
                            dgvItemMaster.Rows.RemoveAt(e.RowIndex)
                        End If
                        f.Dispose()
                    End If
                Case chActualQTY.Index
                    If IsNumeric(dgvItemMaster.Item(chActualQTY.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemMaster.Item(chStock.Index, e.RowIndex).Value) Then
                        LoadStock()
                        ComputeTotals()
                    End If
            End Select
        Catch ex1 As InvalidOperationException

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try

    End Sub

    Private Sub LoadItemDetails(ByVal ItemCode As String)
        Dim query As String
        query = " SELECT    ItemCode, ItemName, 1, ISNULL(ID_SC,0) AS ID_SC, ItemUOM " & _
                " FROM       tblItem_Master " & _
                " WHERE     ItemCode = @ItemCode"
        SQL.FlushParams()
        SQL.AddParam("@ItemCode", IIf(ItemCode = Nothing, "", ItemCode))
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            If dgvItemMaster.SelectedCells.Count > 0 Then

                dgvItemMaster.Item(0, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemCode").ToString
                dgvItemMaster.Item(1, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemName").ToString
                dgvItemMaster.Item(2, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemUOM").ToString
                dgvItemMaster.Item(3, dgvItemMaster.SelectedCells(0).RowIndex).Value = 1
                dgvItemMaster.Item(4, dgvItemMaster.SelectedCells(0).RowIndex).Value = IIf(cbWHfrom.SelectedIndex = -1, "", cbWHfrom.SelectedItem)
                dgvItemMaster.Item(7, dgvItemMaster.SelectedCells(0).RowIndex).Value = CDec(SQL.SQLDR("ID_SC").ToString).ToString("N2")
                LoadWHSE(dgvItemMaster.SelectedCells(0).RowIndex)
                LoadUOM(ItemCode, dgvItemMaster.SelectedCells(0).RowIndex)
            End If
        End If
    End Sub


    Private Sub LoadUOM(ItemCode As String, ByVal SelectedIndex As Integer)
        Try
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
                dgvCB.Items.Add(SQL.SQLDR("UnitCode").ToString)
            End While
            dgvCB.Items.Add("")
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub dgvItemMaster_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemMaster.CellClick
        Try
            If e.ColumnIndex = chUOM.Index And dgvItemMaster.Item(chUOM.Index, e.RowIndex).ReadOnly = False Then
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
            End If
        End If

        'Remove the handle to this event. It will be readded each time a new combobox selection causes the EditingControlShowing Event to fire
        RemoveHandler editingComboBox.SelectionChangeCommitted, AddressOf editingComboBox_SelectionChangeCommitted
        'Re-enable the EditingControlShowing event so the above can take place.
        AddHandler dgvItemMaster.EditingControlShowing, AddressOf dgvItemMaster_EditingControlShowing
    End Sub

    Private Sub btnAddMats_Click(sender As System.Object, e As System.EventArgs) Handles btnAddMats.Click
        If cbWHfrom.SelectedIndex = -1 Then
            Msg("Please select warehouse first!", MsgBoxStyle.Exclamation)
        ElseIf cbBOM.SelectedIndex = -1 Then
            Msg("Please select BOM first!", MsgBoxStyle.Exclamation)

        ElseIf txtActualQTY.Text = "" Then
            Msg("Please enter actual QTY!", MsgBoxStyle.Exclamation)
            txtActualQTY.Focus()
        Else
            dgvItemMaster.Rows.Clear()
            Dim ctr As Integer = dgvItemMaster.Rows.Count - 1
            Dim query As String
            Dim BOM_Code As String
            BOM_Code = GetBOM(cbBOM.SelectedItem)
            query = " SELECT    tblBOM_SFG_Details.MaterialCode, ItemName, tblBOM_SFG_Details.UOM, tblBOM_SFG_Details.QTY  " & _
                    " FROM      tblBOM_SFG INNER JOIN tblBOM_SFG_Details " & _
                    " ON        tblBOM_SFG.BOM_Code = tblBOM_SFG_Details.BOM_Code " & _
                    " INNER JOIn tblItem_Master " & _
                    " ON        tblBOM_SFG_Details.MaterialCode = tblItem_Master.ItemCode " & _
                    " WHERE     tblBOM_SFG.Status ='Active' " & _
                    " AND       tblItem_Master.Status ='Active' " & _
                    " AND       tblBOM_SFG.BOM_Code = '" & BOM_Code & "' " & _
                    " ORDER BY  LineNumber "
            SQL.GetQuery(query)
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                    dgvItemMaster.Rows.Add({row(0).ToString, row(1).ToString, row(2).ToString, row(3) * nupQty.Value, 0, GetWHSE(cbWHfrom.SelectedItem), row(3) * nupQty.Value, CDec(GetAverageCost(row(0).ToString)).ToString("N2")})
                    LoadUOM(row(0).ToString, ctr)
                    LoadWHSE(ctr)
                    ctr += 1
                Next
                LoadLabor(BOM_Code)
                LoadOverhead(BOM_Code)
                ComputeTotals()
                GenerateEntry()
            Else
                query = " SELECT    tblBOM_FG_Details.MaterialCode, ItemName, tblBOM_FG_Details.UOM, tblBOM_FG_Details.QTY  " & _
                        " FROM      tblBOM_FG INNER JOIN tblBOM_FG_Details " & _
                        " ON        tblBOM_FG.BOM_Code = tblBOM_FG_Details.BOM_Code " & _
                        " INNER JOIn tblItem_Master " & _
                        " ON        tblBOM_FG_Details.MaterialCode = tblItem_Master.ItemCode " & _
                        " WHERE     tblBOM_FG.Status ='Active' " & _
                        " AND       tblItem_Master.Status ='Active' " & _
                        " AND       tblBOM_FG.BOM_Code = '" & BOM_Code & "' " & _
                        " ORDER BY  LineNumber "
                SQL.GetQuery(query)
                If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                        dgvItemMaster.Rows.Add({row(0).ToString, row(1).ToString, row(2).ToString, row(3) * nupQty.Value, 0, GetWHSE(cbWHfrom.SelectedItem), row(3) * nupQty.Value, CDec(GetAverageCost(row(0).ToString)).ToString("N2"), CDec(GetAverageCost(row(0).ToString) * (row(3) * nupQty.Value)).ToString("N2")})
                        LoadUOM(row(0).ToString, ctr)
                        LoadWHSE(ctr)
                        ctr += 1
                    Next
                    LoadLabor(BOM_Code)
                    LoadOverhead(BOM_Code)
                    ComputeTotals()
                    GenerateEntry()
                    txtAVGCost.Text = CDec(txtTotalCost.Text / txtActualQTY.Text).ToString("N2")
                End If
            End If

            LoadStock()
            dgvItemMaster.Columns(chItemCode.Index).ReadOnly = True
            dgvItemMaster.Columns(chItemName.Index).ReadOnly = True
            dgvItemMaster.Columns(chUOM.Index).ReadOnly = True
            dgvItemMaster.Columns(dgcBOMQTY.Index).ReadOnly = True
            dgvItemMaster.Columns(dgcBOMQTY.Index).ReadOnly = True
            dgvItemMaster.Columns(chStock.Index).ReadOnly = True
            dgvItemMaster.Columns(chStandardCost.Index).ReadOnly = True
            dgvItemMaster.Columns(chTotalCost.Index).ReadOnly = True

        End If
    End Sub

    Public Sub LoadOverhead(ByVal Code As String)
        Dim query As String
        Dim ctr As Integer = 0
        query = " SELECT    TotalCost " & _
                " FROM     tblBOM_Overhead " & _
                " WHERE    BOMCode = '" & Code & "'" 
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() Then
            txtFOcost.Text = CDec(nupQty.Value * SQL.SQLDR("TotalCost").ToString).ToString("N2")
        End If
    End Sub

    Public Sub LoadLabor(ByVal Code As String)
        Dim query As String
        Dim ctr As Integer = 0
        query = " SELECT TotalCost " & _
                " FROM     tblBOM_Labor " & _
                " WHERE    BOMCode = '" & Code & "'"
       SQL.ReadQuery(query)
        If SQL.SQLDR.Read() Then
            txtDLcost.Text = CDec(nupQty.Value * SQL.SQLDR("TotalCost").ToString).ToString("N2")
        End If
    End Sub

    Private Sub ComputeTotals()
        Dim A, B, C As Decimal
        A = 0
        For Each row As DataGridViewRow In dgvItemMaster.Rows
            If Not row.Cells(chItemCode.Index).Value Is Nothing AndAlso IsNumeric(row.Cells(chStandardCost.Index).Value) Then
                row.Cells(chTotalCost.Index).Value = CDec(row.Cells(chActualQTY.Index).Value * row.Cells(chStandardCost.Index).Value).ToString("N2")
                A += row.Cells(chTotalCost.Index).Value
            End If
        Next
        txtDMcost.Text = A.ToString("N2")

        txtTotalCost.Text = (A + txtDLcost.Text + txtFOcost.Text).ToString("N2")
    End Sub

    Private Sub dgvItemMaster_DataError(sender As Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvItemMaster.DataError
        Try

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    'Private Sub LoadStock()
    '    Dim WHSE As String
    '    WHSE = GetWHSE(cbWHfrom.SelectedItem)
    '    Dim query As String
    '    Dim itemCode As String
    '    Dim StockQTY As Decimal = 0
    '    Dim IssueQTY As Decimal = 0
    '    Dim BOMQTY As Decimal = 0
    '    For Each row As DataGridViewRow In dgvItemMaster.Rows
    '        If Not IsNothing(row.Cells(chItemCode.Index).Value) Then
    '            itemCode = row.Cells(chItemCode.Index).Value.ToString
    '            If Not IsNumeric(row.Cells(dgcBOMQTY.Index).Value) Then BOMQTY = 0 Else BOMQTY = CDec(row.Cells(dgcBOMQTY.Index).Value)

    '            query = "   SELECT ISNULL(SUM(QTY),0) AS QTY " & _
    '                    "   FROM viewItem_Stock " & _
    '                    "   WHERE ItemCode ='" & itemCode & "' " & _
    '                    "   AND WHSE = '" & WHSE & "' "
    '            SQL.ReadQuery(query)
    '            If SQL.SQLDR.Read Then
    '                StockQTY = SQL.SQLDR("QTY")
    '                If StockQTY >= BOMQTY Then ' IF AVAILABLE STOCK IS GREATER THAN THE BOM REQUIREMENT THEN ISSUE QTY SHOULD BE THE SAME AS BOM QTY
    '                    IssueQTY = BOMQTY
    '                Else ' IF AVAILABLE STOCK IS LESS THAN THE BOM REQUIREMENT THEN ISSUE QTY SHOULD BE ONLY THE STOCK QTY
    '                    IssueQTY = StockQTY
    '                End If
    '            End If
    '            row.Cells(chStock.Index).Value = CDec(StockQTY).ToString("N2")

    '        End If

    '    Next
    '    dgvItemMaster.Columns(dgcBOMQTY.Index).Visible = True
    '    dgvItemMaster.Columns(chStock.Index).Visible = True
    'End Sub

    Private Sub cbWHfrom_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbWHfrom.SelectedIndexChanged
        LoadStock()
    End Sub

    Private Sub tsbCopyPR_Click(sender As System.Object, e As System.EventArgs) Handles tsbCopyPR.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        'f.ShowDialog("JO-BOMC")
        f.ShowDialog("JO-BOMC-perLine")
        LoadJO(f.transID)
        f.Dispose()
    End Sub


    Private Sub LoadJO(ByVal ID As String)
        Dim query As String
        Dim BOMCode As String = ""
        Dim JOQTY As Decimal

        If ID.Contains("-") Then
            JO_ID = Strings.Left(ID, ID.IndexOf("-"))
            LineID = Strings.Mid(ID, ID.IndexOf("-") + 2, ID.ToString.Length - JO_ID.ToString.Length)
        End If
        'query = " SELECT    tblJO.TransID, tblJO.JO_No, DateJO, tblJO.VCECode, tblJO_Details.ItemCode, tblJO_Details.UOM, tblJO_Details.QTY, tblJO_Details.Size,tblJO.Remarks, tblJO.DateNeeded,  " &
        '         " ProdLine, SO_Ref, SO_Ref_LineNum, tblJO.Status , tblBOM_JOHeader.BOMCode, tblJO_Details.LineNum " &
        '         " FROM       tblJO " &
        '         " INNER JOIN tblJO_Details ON " &
        '         " tblJO_Details.TransID = tblJO.TransID  " &
        '         " AND        tblJO_Details.LineNum = '" & LineID & "' " &
        '         " INNER JOIN tblBOM ON " &
        '         " tblBOM.JO_Ref = tblJO.TransID AND tblBOM.Status = 'Active' " &
        '         " INNER JOIN tblBOM_JOHeader ON " &
        '         " tblBOM.TransID = tblBOM_JOHeader.TransID  " &
        '         " AND        tblBOM_JOHeader.LineNum = '" & LineID & "' " &
        '         " WHERE      tblJO.TransId = '" & JO_ID & "' "

        'REMOVED BOM EXPLOSION CONNECTION
        query = " SELECT    tblJO.TransID, tblJO.JO_No, DateJO, tblJO.VCECode, tblJO_Details.ItemCode, tblJO_Details.UOM, tblJO_Details.QTY, tblJO_Details.Size,tblJO.Remarks, tblJO.DateNeeded,  " &
                 " ProdLine, SO_Ref, SO_Ref_LineNum, tblJO.Status , tblJO_Details.LineNum " &
                 " FROM       tblJO " &
                 " INNER JOIN tblJO_Details ON " &
                 " tblJO_Details.TransID = tblJO.TransID  " &
                 " AND        tblJO_Details.LineNum = '" & LineID & "' " &
                 " WHERE      tblJO.TransId = '" & JO_ID & "' "
        SQL.FlushParams()
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            JO_ID = SQL.SQLDR("TransID").ToString
            txtJORef.Text = SQL.SQLDR("JO_No").ToString & "-" & SQL.SQLDR("LineNum").ToString
            txtItemCode.Text = SQL.SQLDR("ItemCode").ToString
            JOQTY = SQL.SQLDR("QTY").ToString
            txtSize.Text = SQL.SQLDR("Size").ToString
            If Not IsNumeric(txtSize.Text) Then txtSize.Text = 1
            LoadBOMType(txtItemCode.Text)
            cbBOM.SelectedItem = GetBOM(BOMCode, "tblBOM_FG")
            LoadBOMData(GetBOM(cbBOM.SelectedItem))
            If txtQTY.Text = "" Then
                txtQTY.Text = "1"
            End If
            nupQty.Value = CDec((JOQTY * CDec(txtSize.Text)) / txtQTY.Text).ToString("N2")
            txtActualQTY.Text = txtQTY.Text
        End If
    End Sub


    Private Sub txtItemName_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtItemName.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtUOM.Clear()
            txtQTY.Clear()
            cbBOM.Items.Clear()
            nupQty.Value = 1
            dgvItemMaster.Rows.Clear()
            Dim itemCode As String
            itemCode = txtItemName.Text
            Dim f As New frmCopyFrom
            f.ShowDialog("All Item", itemCode)
            If f.TransID <> "" Then
                itemCode = f.TransID
                LoadItem(itemCode)
                LoadBOMType(txtItemCode.Text)
            Else
                txtItemName.Clear()
            End If
            f.Dispose()
        End If
    End Sub

    Private Sub LoadItem(ByVal itemCode As String)
        Dim query As String
        query = "  SELECT ItemCode, ItemName, PD_UOM, PD_UnitCost " & _
                "  FROM   tblItem_Master     " & _
                "  WHERE  ItemCode = '" & itemCode & "'  AND Status ='Active' "
        SQL.FlushParams()
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtItemCode.Text = itemCode
            txtItemName.Text = SQL.SQLDR("ItemName").ToString
        Else
            MsgBox("No Record found!", MsgBoxStyle.Information)
            Me.Close()
        End If
    End Sub

    Private Sub LoadBOMType(ByVal ItemCode As String)
        Dim query As String
        query = " SELECT * FROM ( " & _
                " SELECT BOM_Code  + ' | ' + ISNULL(Remarks,'') AS Description, ItemCode  FROm tblBOM_FG " & _
                " UNION ALL " & _
                " SELECT BOM_Code  + ' | ' + ISNULL(Remarks,'') AS Description, ItemCode  FROm tblBOM_SFG ) AS A " & _
                "  WHERE ItemCode ='" & ItemCode & "' "
        SQL.ReadQuery(query)
        cbBOM.Items.Clear()
        If SQL.SQLDR.HasRows Then
            While SQL.SQLDR.Read
                cbBOM.Items.Add(SQL.SQLDR("Description").ToString)
            End While
        Else
            MsgBox("No BOM for this Item!", MsgBoxStyle.Information)
        End If
    End Sub


    Private Sub txtItemName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtItemName.TextChanged

    End Sub

    Private Sub Label3_Click(sender As System.Object, e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("BOMC", TransID)
        f.Dispose()
    End Sub

    Private Sub dgvItemMaster_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemMaster.CellContentClick

    End Sub

    Private Sub GenerateEntry()
        Dim query As String
        If txtItemCode.Text <> "" Then
            dgvEntry.Rows.Clear()
            query = " SELECT AD_Inv, AccountTitle " & _
                    " FROM   tblItem_Master INNER JOIN tblCOA_Master " & _
                    " ON     tblItem_Master.AD_Inv = tblCOA_Master.AccountCode " & _
                    " WHERE  ItemCode ='" & txtItemCode.Text & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read() Then
                dgvEntry.Rows.Add({SQL.SQLDR("AD_Inv").ToString, SQL.SQLDR("AccountTitle").ToString, CDec(txtTotalCost.Text).ToString("N2"), "0.00", "BOMC: " & txtTransNum.Text})
            End If
        End If



        For Each row As DataGridViewRow In dgvItemMaster.Rows
            If row.Cells(chItemCode.Index).Value <> Nothing AndAlso row.Cells(chStandardCost.Index).Value > 0 Then
                query = " SELECT AD_Inv, AccountTitle " & _
                        " FROM   tblItem_Master INNER JOIN tblCOA_Master " & _
                        " ON     tblItem_Master.AD_Inv = tblCOA_Master.AccountCode " & _
                        " WHERE  ItemCode ='" & row.Cells(chItemCode.Index).Value & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read() Then
                    dgvEntry.Rows.Add({SQL.SQLDR("AD_Inv").ToString, SQL.SQLDR("AccountTitle").ToString, "0.00", CDec(row.Cells(chStandardCost.Index).Value * row.Cells(chActualQTY.Index).Value).ToString("N2"), "BOMC: " & txtTransNum.Text})
                End If
            End If
        Next

        query = " SELECT AccntCode, AccountTitle " & _
                       " FROM   tblDefaultAccount INNER JOIN tblCOA_Master " & _
                       " ON     tblDefaultAccount.AccntCode = tblCOA_Master.AccountCode " & _
                       " WHERE  Type = 'Labor' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() Then
            dgvEntry.Rows.Add({SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccountTitle").ToString, "0.00", CDec(txtDLcost.Text).ToString("N2"), ""})
        End If

        query = " SELECT AccntCode, AccountTitle " & _
                     " FROM   tblDefaultAccount INNER JOIN tblCOA_Master " & _
                     " ON     tblDefaultAccount.AccntCode = tblCOA_Master.AccountCode " & _
                     " WHERE  Type = 'Overhead' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() Then
            dgvEntry.Rows.Add({SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccountTitle").ToString, "0.00", CDec(txtFOcost.Text).ToString("N2"), ""})
        End If
        TotalDBCR()
    End Sub

    Private Sub TotalDBCR()
        ' COMPUTE TOTAL DEBIT
        Dim db As Decimal = 0

        For i As Integer = 0 To dgvEntry.Rows.Count - 1
            If Val(dgvEntry.Item(2, i).Value) <> 0 Then
                db = db + Double.Parse(dgvEntry.Item(2, i).Value).ToString("N2")
            End If
        Next
        txtTotalDebit.Text = db.ToString("N2")

        ' COMPUTE TOTAL CREDIT
        Dim b As Decimal = 0

        For i As Integer = 0 To dgvEntry.Rows.Count - 1
            If Val(dgvEntry.Item(3, i).Value) <> 0 Then
                b = b + Double.Parse(dgvEntry.Item(3, i).Value).ToString("N2")
            End If
        Next
        txtTotalCredit.Text = b.ToString("N2")
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