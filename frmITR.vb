Public Class frmITR
    Dim TransID, RefID As String
    Dim ITRNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "ITR"
    Dim ColumnPK As String = "ITR_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblITR"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim SysCode As String = "EMERALD"
    Dim MR_ID As Integer

    Public Overloads Function ShowDialog(ByVal docnumber As String) As Boolean
        TransID = docnumber
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmITR_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            loadWH()
            If TransID <> "" Then
                LoadITR(TransID)
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

    Private Sub loadWHFrom()
        If cbReceiver.SelectedItem = "Warehouse" Then
            Dim query As String
            query = " SELECT tblWarehouse.Code + ' | ' + Description AS WHSECode " & _
                    " FROM tblWarehouse INNER JOIN tblUser_Access " & _
                    " ON tblWarehouse.Code = tblUser_Access.Code " & _
                    " AND tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active' " & _
                    " AND Type = 'Warehouse' AND isAllowed = 1 " & _
                    " WHERE UserID ='" & UserID & "' "
            SQL.ReadQuery(query)
            cbWHto.Items.Clear()
            Dim ctr As Integer = 0
            While SQL.SQLDR.Read
                cbWHto.Items.Add(SQL.SQLDR("WHSECode").ToString)
                ctr += 1
            End While
            If cbWHto.Items.Count > 0 Then
                cbWHto.SelectedIndex = 0
            End If
        ElseIf cbReceiver.SelectedItem = "Production" Then
            Dim query As String
            query = " SELECT    tblProdWarehouse.Code + ' | ' + Description AS WHSECode " & _
                    " FROM      tblProdWarehouse INNER JOIN tblUser_Access " & _
                    " ON        tblProdWarehouse.Code = tblUser_Access.Code " & _
                    " AND       tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active' " & _
                    " AND       tblUser_Access.Type = 'Production' AND isAllowed = 1 " & _
                    " WHERE     UserID ='" & UserID & "' "
            SQL.ReadQuery(query)
            cbWHto.Items.Clear()
            Dim ctr As Integer = 0
            While SQL.SQLDR.Read
                cbWHto.Items.Add(SQL.SQLDR("WHSECode").ToString)
                ctr += 1
            End While
            If cbWHto.Items.Count > 0 Then
                cbWHto.SelectedIndex = 0
            End If
        End If
    End Sub


    Private Sub loadWH()
        If cbReceivedFrom.SelectedItem = "Warehouse" Then
            Dim query As String
            query = " SELECT tblWarehouse.Code + ' | ' + Description AS Description  FROM tblWarehouse WHERE Status ='Active' "
            SQL.ReadQuery(query)
            cbWHfrom.Items.Clear()
            While SQL.SQLDR.Read
                cbWHfrom.Items.Add(SQL.SQLDR("Description"))
            End While
        ElseIf cbReceivedFrom.SelectedItem = "Production" Then
            Dim query As String
            query = " SELECT tblProdWarehouse.Code + ' | ' + Description AS Description  FROM tblProdWarehouse WHERE Status ='Active' "
            SQL.ReadQuery(query)
            cbWHfrom.Items.Clear()
            While SQL.SQLDR.Read
                cbWHfrom.Items.Add(SQL.SQLDR("Description"))
            End While
        End If

    End Sub

    Private Function LoadWH(ByVal Type As String, WHSEtype As String, WHSECode As String) As String
        Dim query As String = ""
        If Type = "From" Then
            If WHSEtype = "Warehouse" Then
                query = " SELECT tblWarehouse.Code + ' | ' + Description AS WHSE " & _
                        " FROM tblWarehouse INNER JOIN tblUser_Access " & _
                        " ON tblWarehouse.Code = tblUser_Access.Code " & _
                        " AND tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active' " & _
                        " AND Type = 'Warehouse' AND isAllowed = 1 " & _
                        " WHERE UserID ='" & UserID & "' AND tblWarehouse.Code = '" & WHSECode & "'  "
            ElseIf WHSEtype = "Production" Then
                query = " SELECT    tblProdWarehouse.Code + ' | ' + Description AS WHSE " & _
                        " FROM      tblProdWarehouse INNER JOIN tblUser_Access " & _
                        " ON        tblProdWarehouse.Code = tblUser_Access.Code " & _
                        " AND       tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active' " & _
                        " AND       tblUser_Access.Type = 'Production' AND isAllowed = 1 " & _
                        " WHERE     UserID ='" & UserID & "'  AND tblProdWarehouse.Code = '" & WHSECode & "' "
            End If
        ElseIf Type = "To" Then
            If WHSEtype = "Warehouse" Then
                query = " SELECT    tblWarehouse.Code + ' | ' + Description AS WHSE " & _
                        " FROM      tblWarehouse WHERE Status ='Active' " & _
                        " AND       tblWarehouse.Code = '" & WHSECode & "'  "
            ElseIf WHSEtype = "Production" Then
                query = " SELECT    tblProdWarehouse.Code + ' | ' + Description AS WHSE " & _
                        " FROM      tblProdWarehouse WHERE Status ='Active' " & _
                        " AND       tblProdWarehouse.Code = '" & WHSECode & "'  "
            End If
        End If
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("WHSE").ToString
        Else
            Return ""
        End If
    End Function

    Private Sub EnableControl(ByVal Value As Boolean)
        cbWHfrom.Enabled = Value
        cbWHto.Enabled = Value
        cbReceiver.Enabled = Value
        cbReceivedFrom.Enabled = Value
        btnSearchVCE.Enabled = Value
        txtVCEName.Enabled = Value
        dgvItemMaster.AllowUserToAddRows = Value
        dgvItemMaster.AllowUserToDeleteRows = Value
        dgvItemMaster.ReadOnly = Not Value
        If Value = True Then
            dgvItemMaster.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemMaster.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        cbTransType.Enabled = Value
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("ITR_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("ITR")
            TransID = f.transID
            LoadITR(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub LoadITR(ByVal ID As String)
        Dim query As String
        Dim WHSEfrom, WHSEto As String
        query = " SELECT   TransID, ITR_No, VCECode, DateITR, Type, ReceivedBy, ReceivedFrom, WHSE_From, WHSE_To, Remarks, Status, ITI_Ref " & _
                " FROM     tblITR " & _
                " WHERE    TransId = '" & ID & "' " & _
                " ORDER BY TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            TransID = SQL.SQLDR("TransID").ToString
            ITRNo = SQL.SQLDR("ITR_No").ToString
            txtTransNum.Text = ITRNo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            dtpDocDate.Text = SQL.SQLDR("DateITR").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            txtGIRef.Text = SQL.SQLDR("ITI_Ref").ToString
            disableEvent = True
            cbReceiver.SelectedItem = SQL.SQLDR("ReceivedBy").ToString
            cbReceivedFrom.SelectedItem = SQL.SQLDR("ReceivedFrom").ToString
            WHSEfrom = SQL.SQLDR("WHSE_From").ToString
            WHSEto = SQL.SQLDR("WHSE_To").ToString
            cbTransType.SelectedItem = SQL.SQLDR("Type").ToString
            disableEvent = False
            loadWHFrom()
            loadWHto()
            cbWHto.SelectedItem = GetWHSE(WHSEfrom, "tblWarehouse")
            cbWHfrom.SelectedItem = GetWHSE(WHSEto, "tblProdWarehouse")
            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            LoadITRDetails(TransID)
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
            EnableControl(False)
        Else
            Cleartext()
        End If
    End Sub

    Protected Sub LoadITRDetails(ByVal TransID As String)
        Dim ctr As Integer = 0
        Dim query As String
        query = " SELECT	tblITR_Details.ItemCode, tblITR_Details.Description, tblITR_Details.UOM, QTY, tblITR_Details.WHSE " & _
                " FROM	    tblITR INNER JOIN tblITR_Details " & _
                " ON		tblITR.TransID = tblITR_Details.TransID " & _
                " WHERE     tblITR_Details.TransId = " & TransID & " " & _
                " ORDER BY  tblITR_Details.LineNum "
        disableEvent = True
        dgvItemMaster.Rows.Clear()
        disableEvent = False
        SQL.ReadQuery(query)
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvItemMaster.Rows.Add(row(0).ToString, row(1).ToString, row(2).ToString, _
                                     CDec(row(3)).ToString("N2"), GetWHSEDesc(row(4).ToString))
                LoadUOM(row(0).ToString, ctr)
                LoadWHSE(ctr)
                ctr += 1
            Next
        End If
    End Sub

    Private Sub LoadWHSE(Optional ByVal SelectedIndex As Integer = -1)
        Try
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemMaster.Item(chWHSE.Index, SelectedIndex)
            dgvCB.Items.Clear()
            ' ADD ALL WHSEc
            Dim query As String
            If cbReceivedFrom.SelectedItem = "Warehouse" Then
                query = " SELECT Description FROM tblWarehouse WHERE Status ='Active' "
            Else
                query = " SELECT Description FROM tblProdWarehouse WHERE Status ='Active' "
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
        txtVCECode.Clear()
        txtVCEName.Clear()
        txtTransNum.Clear()
        cbWHfrom.SelectedIndex = -1
        cbTransType.SelectedIndex = -1
        dgvItemMaster.Rows.Clear()
        txtRemarks.Clear()
        txtStatus.Text = "Open"
        dtpDocDate.Value = Date.Today.Date
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("ITR_ADD") Then
            msgRestricted()
        Else
            Cleartext()
            TransID = ""
            ITRNo = ""



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
        If Not AllowAccess("ITR_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)
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
        ElseIf TransID = "" Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                TransID = GenerateTransID(ColumnID, DBTable)
                ITRNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                txtTransNum.Text = ITRNo
                SaveITR()
                Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                ITRNo = txtTransNum.Text
                LoadITR(TransID)
            End If
        Else
            If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                UpDateITR()
                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                ITRNo = txtTransNum.Text
                LoadITR(TransID)
            End If
        End If
    End Sub

    Private Sub SaveITR()
        Try
            Dim WHSEfrom, WHSEto, Type As String
            Type = IIf(cbTransType.SelectedIndex = -1, "", cbTransType.SelectedItem)

            WHSEto = GetWHSE(cbWHto.SelectedItem)
            WHSEfrom = GetWHSE(cbWHfrom.SelectedItem)

            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblITR(TransID, ITR_No, BranchCode, BusinessCode, DateITR, VCECode, ReceivedBy, ReceivedFrom, WHSE_From, WHSE_To, Type, Remarks, ITR_Ref, WhoCreated) " & _
                        " VALUES (@TransID, @ITR_No, @BranchCode, @BusinessCode, @DateITR, @VCECode, @ReceivedBy, @ReceivedFrom, @WHSE_From, @WHSE_To, @Type, @Remarks, @ITR_Ref, @WhoCreated) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@ITR_No", ITRNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateITR", dtpDocDate.Value.Date)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@ReceivedBy", cbReceiver.SelectedItem)
            SQL.AddParam("@ReceivedFrom", cbReceivedFrom.SelectedItem)
            SQL.AddParam("@WHSE_From", WHSEfrom)
            SQL.AddParam("@WHSE_To", WHSEto)
            SQL.AddParam("@ITR_Ref", txtGIRef.Text)
            SQL.AddParam("@Type", Type)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            Dim ItemCode, Description, UOM As String
            Dim QTY, UnitCost As Decimal

            For Each row As DataGridViewRow In dgvItemMaster.Rows
                If Not row.Cells(dgcQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                    UnitCost = GetAverageCost(ItemCode)
                    Description = IIf(row.Cells(chItemName.Index).Value = Nothing, "", row.Cells(chItemName.Index).Value)
                    UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                    If IsNumeric(row.Cells(dgcQTY.Index).Value) Then QTY = CDec(row.Cells(dgcQTY.Index).Value) Else QTY = 1
                    insertSQL = " INSERT INTO " & _
                         " tblITR_Details(TransId, ItemCode, Description, UOM, QTY, WHSE, LineNum, WhoCreated) " & _
                         " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @WHSE, @LineNum, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", ItemCode)
                    SQL.AddParam("@Description", Description)
                    SQL.AddParam("@UOM", UOM)
                    SQL.AddParam("@QTY", QTY)
                    SQL.AddParam("@WHSE", WHSEto)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1

                    SaveINVTY("OUT", ModuleID, "ITI", MR_ID, dtpDocDate.Value.Date, ItemCode, WHSEfrom, QTY, UOM, UnitCost)
                    SaveINVTY("IN", ModuleID, "ITR", TransID, dtpDocDate.Value.Date, ItemCode, WHSEto, QTY, UOM, UnitCost)
                End If
            Next
            ComputeWAUC("ITI", MR_ID)
            ComputeWAUC("ITR", TransID)

            Dim updateSQL As String
            updateSQL = " UPDATE tblITI SET Status ='Closed' WHERE TransID = '" & MR_ID & " '"
            SQL.ExecNonQuery(updateSQL)
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "ITR_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub UpDateITR()
        Try
            Dim WHSEfrom, WHSEto, Type As String
            Type = IIf(cbTransType.SelectedIndex = -1, "", cbTransType.SelectedItem)

            WHSEfrom = GetWHSE(cbWHto.SelectedItem)
            WHSEto = GetWHSE(cbWHfrom.SelectedItem)
            activityStatus = True
            Dim insertSQL, updateSQL, deleteSQL As String
            updateSQL = " UPDATE    tblITR " & _
                        " SET       ITR_No = @ITR_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, DateITR = @DateITR, VCECode = @VCECode, ITI_Ref = @ITI_Ref, " & _
                        "           ReceivedBy = @ReceivedBy, ReceivedFrom = @ReceivedFrom, WHSE_From = @WHSE_From, WHSE_To = @WHSE_To, Type = @Type, Remarks = @Remarks, " & _
                        "           WhoModified = @WhoModified, DateModified = GETDATE() " & _
                        " WHERE     TransID = @TransID  "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@ITR_No", ITRNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateITR", dtpDocDate.Value.Date)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@ReceivedBy", cbReceiver.SelectedItem)
            SQL.AddParam("@ReceivedFrom", cbReceivedFrom.SelectedItem)
            SQL.AddParam("@WHSE_From", WHSEfrom)
            SQL.AddParam("@WHSE_To", WHSEto)
            SQL.AddParam("@ITI_Ref", txtGIRef.Text)
            SQL.AddParam("@Type", Type)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)

            deleteSQL = " DELETE FROM tblITR_Details WHERE TransID =@TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.ExecNonQuery(deleteSQL)

            Dim line As Integer = 1
            Dim ItemCode, Description, UOM As String
            Dim QTY As Decimal
            For Each row As DataGridViewRow In dgvItemMaster.Rows
                If Not row.Cells(dgcQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                    Description = IIf(row.Cells(chItemName.Index).Value = Nothing, "", row.Cells(chItemName.Index).Value)
                    UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                    If IsNumeric(row.Cells(dgcQTY.Index).Value) Then QTY = CDec(row.Cells(dgcQTY.Index).Value) Else QTY = 1
                    insertSQL = " INSERT INTO " & _
                         " tblITR_Details(TransId, ItemCode, Description, UOM, QTY, WHSE, LineNum, WhoCreated) " & _
                         " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @WHSE, @LineNum, @WhoCreated) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", ItemCode)
                    SQL.AddParam("@Description", Description)
                    SQL.AddParam("@UOM", UOM)
                    SQL.AddParam("@QTY", QTY)
                    SQL.AddParam("@WHSE", WHSEto)
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
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "ITR_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("ITR_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE  tblITR SET Status ='Cancelled' WHERE ITR_No = @ITR_No "
                        SQL.FlushParams()
                        ITRNo = txtTransNum.Text
                        SQL.AddParam("@ITR_No", ITRNo)
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

                        LoadITR(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "ITR_No", ITRNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If ITRNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblITR  WHERE ITR_No < '" & ITRNo & "' ORDER BY ITR_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadITR(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If ITRNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblITR  WHERE ITR_No > '" & ITRNo & "' ORDER BY ITR_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                ITRNo = SQL.SQLDR("TransID").ToString
                LoadITR(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If ITRNo = "" Then
            Cleartext()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadITR(TransID)
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

    Private Sub frmGR_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub txtVCEName_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVCEName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.txtFilter.Text = txtVCEName.Text
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            txtVCEName.Text = f.VCEName
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
            End Select
        Catch ex1 As InvalidOperationException

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try

    End Sub

    Private Sub LoadItemDetails(ByVal ItemCode As String)
        Dim query As String
        query = " SELECT    ItemCode, ItemName, 1 " & _
                " FROM       tblItem_Master " & _
                " WHERE     ItemCode = @ItemCode"
        SQL.FlushParams()
        SQL.AddParam("@ItemCode", IIf(ItemCode = Nothing, "", ItemCode))
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            If dgvItemMaster.SelectedCells.Count > 0 Then

                dgvItemMaster.Item(0, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemCode").ToString
                dgvItemMaster.Item(1, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemName").ToString
                dgvItemMaster.Item(2, dgvItemMaster.SelectedCells(0).RowIndex).Value = ""
                dgvItemMaster.Item(3, dgvItemMaster.SelectedCells(0).RowIndex).Value = 1
                dgvItemMaster.Item(4, dgvItemMaster.SelectedCells(0).RowIndex).Value = IIf(cbWHfrom.SelectedIndex = -1, "", cbWHfrom.SelectedItem)
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
            End If
        End If

        'Remove the handle to this event. It will be readded each time a new combobox selection causes the EditingControlShowing Event to fire
        RemoveHandler editingComboBox.SelectionChangeCommitted, AddressOf editingComboBox_SelectionChangeCommitted
        'Re-enable the EditingControlShowing event so the above can take place.
        AddHandler dgvItemMaster.EditingControlShowing, AddressOf dgvItemMaster_EditingControlShowing
    End Sub

    Private Sub dgvItemMaster_DataError(sender As Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvItemMaster.DataError
        Try

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub btnSearchVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchVCE.Click
        Dim f As New frmVCE_Search
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        txtVCEName.Text = f.VCEName
    End Sub

    Private Sub cbReceivedFrom_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbReceivedFrom.SelectedIndexChanged
        If disableEvent = False Then
            loadWHto()
        End If
    End Sub

    Private Sub loadWHto()
        If cbReceivedFrom.SelectedItem = "Warehouse" Then
            txtVCECode.ReadOnly = False
            txtVCEName.ReadOnly = False
            btnSearchVCE.Enabled = True
        ElseIf cbReceivedFrom.SelectedItem = "Production" Then
            txtVCECode.ReadOnly = False
            txtVCEName.ReadOnly = False
            btnSearchVCE.Enabled = True
        End If
        loadWH()
        For Each row As DataGridViewRow In dgvItemMaster.Rows
            If row.Cells(chItemCode.Index).Value <> Nothing AndAlso row.Cells(chItemCode.Index).Value <> "" Then
                LoadWHSE(row.Index)
            End If
        Next
    End Sub

    Private Sub cbIssuerFrom_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbReceiver.SelectedIndexChanged
        If disableEvent = False Then
            loadWHFrom()
        End If

    End Sub

    Private Sub tsbCopyPR_Click(sender As System.Object, e As System.EventArgs) Handles tsbCopyPR.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("ITI-ITR")
        LoadITI(f.transID)
        f.Dispose()
    End Sub

    Private Sub LoadITI(ByVal ID As String)
        Try
            Dim WHSEfrom, WHSEto As String
            Dim query As String
            query = " SELECT TransID, ITI_No, IssueFrom, IssueTo, WHSE_From, WHSE_To, DateITI, Remarks " & _
                    " FROM   tblITI " & _
                    " WHERE  TransID ='" & ID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                MR_ID = SQL.SQLDR("TransID")
                txtGIRef.Text = SQL.SQLDR("ITI_No").ToString
                WHSEfrom = SQL.SQLDR("WHSE_From").ToString
                WHSEto = SQL.SQLDR("WHSE_To").ToString
                disableEvent = True
                cbReceiver.SelectedItem = SQL.SQLDR("IssueTo").ToString
                cbReceivedFrom.SelectedItem = SQL.SQLDR("IssueFrom").ToString
                disableEvent = False
                loadWHFrom()
                loadWH()
                cbWHto.SelectedItem = loadWH("From", "Production", WHSEto)
                cbWHfrom.SelectedItem = loadWH("To", "Warehouse", WHSEfrom)


                query = " SELECT tblITI_Details.ItemCode, Description, UOM, IssueQTY, WHSE " & _
                        " FROM   tblITI_Details  " & _
                        " WHERE  tblITI_Details.TransID ='" & ID & "' "
                SQL.GetQuery(query)
                dgvItemMaster.Rows.Clear()
                Dim ctr As Integer = 0
                Dim ctrGroup As Integer = 0
                If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                        dgvItemMaster.Rows.Add(row(0).ToString, row(1).ToString, row(2).ToString, _
                                      CDec(row(3)).ToString("N2"), GetWHSEDesc(row(4).ToString))
                        LoadWHSE(ctr)
                        LoadUOM(row(0).ToString, ctr)
                        ctr += 1
                    Next

                End If
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("ITR", TransID)
        f.Dispose()
    End Sub
End Class