Imports System.ComponentModel
Imports System.IO
Imports Microsoft.Office.Interop

Public Class frmIPL
    Dim ModuleID As String = "IPL"
    Dim disableEvent As Boolean = False
    Dim TransID As String
    Dim IPLNo As String
    Dim ColumnPK As String = "IPL_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblIPL"
    Dim TransAuto As Boolean
    Dim FileName As String

    Private Sub frmItem_Pricelist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Text = "(" & database & ") - Item Price List "
            TransAuto = GetTransSetup(ModuleID)
            LoadFilters()
            LoadList()

            If TransID <> "" Then
                If Not AllowAccess("IPL_MANAGE") Then
                    msgRestricted()
                    tsbSearch.Enabled = True
                    tsbNew.Enabled = True
                    tsbEdit.Enabled = False
                    tsbSave.Enabled = False
                    tsbCancel.Enabled = False
                    tsbClose.Enabled = False
                    tsbPrevious.Enabled = False
                    tsbNext.Enabled = False
                    tsbExit.Enabled = True
                    EnableControl(False)
                Else
                    LoadIPL(TransID)
                End If
            Else
                tsbNew.Enabled = True
                tsbEdit.Enabled = False
                tsbSave.Enabled = False
                tsbCancel.Enabled = False
                tsbClose.Enabled = False
                tsbPrevious.Enabled = False
                tsbNext.Enabled = False
                tsbExit.Enabled = True
                EnableControl(False)
            End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub EnableControl(Value As Boolean)
        txtDescription.Enabled = Value
        rbImport.Enabled = Value
        rbManual.Enabled = Value
        rbMarkup.Enabled = Value
        cbUOM.Enabled = Value
        btnAdd.Enabled = Value
        btnDelete.Enabled = Value
        dtpDocDate.Enabled = Value
        cbMarkupMethod.Enabled = Value
        cbMarkupBasedOn.Enabled = Value
        txtMarkupValue.Enabled = Value
        chkApplyAllUOM.Enabled = Value
        If chkApplyAllUOM.Checked = True Then
            cbUOM.Enabled = False
            btnAdd.Enabled = False
            btnDelete.Enabled = False
        Else
            cbUOM.Enabled = Value
            btnAdd.Enabled = Value
            btnDelete.Enabled = Value
        End If
        If rbMarkup.Checked = False Then
            cbMarkupMethod.Enabled = False
            cbMarkupBasedOn.Enabled = False
            txtMarkupValue.Enabled = False
        Else
            cbMarkupMethod.Enabled = Value
            cbMarkupBasedOn.Enabled = Value
            txtMarkupValue.Enabled = Value
        End If
        If rbManual.Checked = False Then
            dgvList.Columns(dgcNewPrice.Index).ReadOnly = True
            dgvList.Columns(dgcQTY.Index).ReadOnly = True
            dgvList.Columns(dgcVATInc.Index).ReadOnly = True
        Else
            dgvList.Columns(dgcNewPrice.Index).ReadOnly = Not Value
            dgvList.Columns(dgcQTY.Index).ReadOnly = Not Value
            dgvList.Columns(dgcVATInc.Index).ReadOnly = Not Value
        End If

        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Private Sub LoadFilters()
        dgvFilter.Rows.Clear()
        Dim query As String
        query = " SELECT GroupID AS DBField, Description FROM viewItem_PriceFilter "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvFilter.Rows.Add(SQL.SQLDR("DBField").ToString, SQL.SQLDR("Description").ToString, "All")
        End While
    End Sub

    Private Sub LoadList()
        dgvList.Rows.Clear()
        Dim query As String
        query = " SELECT  ItemCode, Barcode, ItemName, G1, G2, G3, G4, G5, UOM, PriceList, UnitPrice, VATInc, QTY  " &
                " FROM    viewItem_SellingPrice " &
                " WHERE   1=1 "
        SQL.FlushParams()
        Dim filter As String = ""
        For Each row As DataGridViewRow In dgvFilter.Rows
            If row.Visible = True AndAlso row.Cells(dgcValue.Index).Value <> "All" Then
                Dim fieldName As String = row.Cells(dgcDBField.Index).Value
                Dim value As String = row.Cells(dgcValue.Index).Value
                Dim param As String = "@" & fieldName
                filter = filter & " AND " & fieldName & " = " & param
                SQL.AddParam(param, value)
            End If
        Next
        query = query & filter
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvList.Rows.Add(SQL.SQLDR("ItemCode").ToString, SQL.SQLDR("Barcode").ToString, SQL.SQLDR("ItemName").ToString,
                            SQL.SQLDR("UOM").ToString, CDec(SQL.SQLDR("UnitPrice")).ToString("N2"), "", CDec(SQL.SQLDR("QTY")).ToString("N2"),
                            CBool(SQL.SQLDR("VATInc")), SQL.SQLDR("PriceList").ToString)
        End While
        lblCounter.Text = "Record Count : " & dgvList.Rows.Count
    End Sub

    Private Sub dgvFilter_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFilter.CellEndEdit
        If e.RowIndex <> -1 AndAlso e.ColumnIndex = dgcValue.Index Then
            Dim f As New frmSearchList
            Dim filter As String = ""
            If dgvFilter.Item(dgcValue.Index, e.RowIndex).Value <> "All" Then
                filter = dgvFilter.Item(dgcValue.Index, e.RowIndex).Value
            End If
            f.ShowDialog("IPL", dgvFilter.Item(dgcDBField.Index, e.RowIndex).Value, dgvFilter.Item(dgcField.Index, e.RowIndex).Value, filter)
            If f.result <> "" Then
                dgvFilter.Item(e.ColumnIndex, e.RowIndex).Value = f.result
            Else
                dgvFilter.Item(e.ColumnIndex, e.RowIndex).Value = "All"
            End If
            f.Dispose()
            LoadList()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As Object, e As EventArgs) Handles tsbNew.Click
        If Not AllowAccess("IPL_MANAGE") Then
            msgRestricted()
        Else
            ' CLEAR TRANSACTION RECORDS
            ClearText()
            TransID = ""
            IPLNo = ""
            LoadUOM()
            LoadList()
            ' SET TOOLSTRIP BUTTONS
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbSave.Enabled = True
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
            EnableControl(True)

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
        End If
    End Sub

    Private Sub ClearText()
        txtDescription.Clear()
        rbMarkup.Checked = True
        cbMarkupMethod.SelectedIndex = 0
        cbMarkupBasedOn.SelectedIndex = 0
        txtMarkupValue.Clear()
        lbUOM.Items.Clear()
        chkApplyAllUOM.Checked = True
    End Sub
    Private Sub btnUOM_Click(sender As Object, e As EventArgs) Handles btnUOM.Click
        frmItem_UOM.ShowDialog()
        LoadUOM()
        frmItem_UOM.Dispose()
    End Sub

    Private Sub LoadUOM()
        cbUOM.Items.Clear()
        Dim query As String
        query = " SELECT UnitCode FROM tblUOM "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            Dim exist As Boolean = False
            Dim code As String = SQL.SQLDR("UnitCode").ToString
            For Each item In lbUOM.Items
                If item.ToString = code Then
                    exist = True
                    Exit For
                End If
            Next
            If exist = False Then
                cbUOM.Items.Add(code)
            End If
        End While
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If cbUOM.SelectedIndex <> -1 Then
            lbUOM.Items.Add(cbUOM.SelectedItem)
            LoadUOM()
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If lbUOM.SelectedIndex <> -1 Then
            lbUOM.Items.RemoveAt(lbUOM.SelectedIndex)
            LoadUOM()
        End If
    End Sub

    Private Sub tsbClose_Click(sender As Object, e As EventArgs) Handles tsbClose.Click
        ' Toolstrip Buttons
        If IPLNo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
        Else
            LoadIPL(IPLNo)
            tsbEdit.Enabled = True
            tsbCancel.Enabled = True
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
        End If
        tsbSearch.Enabled = True
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
    End Sub

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub chkApplyAllUOM_CheckedChanged(sender As Object, e As EventArgs) Handles chkApplyAllUOM.CheckedChanged
        cbUOM.Enabled = Not chkApplyAllUOM.Checked
        btnAdd.Enabled = Not chkApplyAllUOM.Checked
        btnDelete.Enabled = Not chkApplyAllUOM.Checked
    End Sub

    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        If txtDescription.Text = "" Then
            Msg("Please enter price description!", MsgBoxStyle.Exclamation)
        ElseIf chkApplyAllUOM.Checked = False AndAlso lbUOM.Items.Count = 0 Then
            Msg("Please add UOM to the list!", MsgBoxStyle.Exclamation)
        ElseIf TransID = "" Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                TransID = GenerateTransID(ColumnID, DBTable)
                IPLNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                txtTransNum.Text = IPLNo
                SaveIPL()
                Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                IPLNo = txtTransNum.Text
                LoadIPL(TransID)
            End If
        Else
            If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                UpdateIPL()
                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                IPLNo = txtTransNum.Text
                LoadIPL(TransID)
            End If
        End If
    End Sub

    Private Sub SaveIPL()
        Dim SQL As New SQLControl
        SQL.BeginTransaction()
        Try
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " &
                                " tblIPL  (TransID, IPL_No, EffectivityDate, Description, Type, ApplyAllUOM,   " &
                                "         MarkupMethod, MarkupValue, MarkupBasis, WhoCreated, Status, DateCreated) " &
                                " VALUES (@TransID, @IPL_No, @EffectivityDate, @Description, @Type, @ApplyAllUOM, " &
                                "         @MarkupMethod, @MarkupValue, @MarkupBasis, @WhoCreated, @Status, GETDATE()) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@IPL_No", IPLNo)
            SQL.AddParam("@EffectivityDate", dtpDocDate.Value)
            SQL.AddParam("@Description", txtDescription.Text)
            If rbManual.Checked = True Then
                SQL.AddParam("@Type", "Manual")
            ElseIf rbMarkup.Checked = True Then
                SQL.AddParam("@Type", "Markup")
            ElseIf rbImport.Checked = True Then
                SQL.AddParam("@Type", "Import")
            End If
            SQL.AddParam("@ApplyAllUOM", chkApplyAllUOM.Checked)
            SQL.AddParam("@MarkupMethod", cbMarkupMethod.SelectedItem)
            If IsNumeric(txtMarkupValue.Text) Then
                SQL.AddParam("@MarkupValue", CDec(txtMarkupValue.Text))
            Else
                SQL.AddParam("@MarkupValue", 0)
            End If
            SQL.AddParam("@MarkupBasis", cbMarkupBasedOn.SelectedItem)
            SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)
            If rbManual.Checked Or rbImport.Checked Then
                SaveItems(SQL)
            End If
            SaveFilters(SQL)
            SaveUOM(SQL)
            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
            TransID = ""
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", ColumnPK, txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub SaveItems(SQL As SQLControl)
        Dim query As String
        query = " DELETE FROM tblIPL_Details WHERE TransID = @TransID "
        SQL.FlushParams()
        SQL.AddParam("@TransID", TransID)
        SQL.ExecNonQuery(query)
        Dim unitPrice As Decimal = 0
        For Each row As DataGridViewRow In dgvList.Rows
            query = " INSERT INTO " &
               " tblIPL_Details(TransID, ItemCode, UOM, UOMQTY, UnitPrice, OldPrice, VATInclusive) " &
               " VALUES(@TransID, @ItemCode, @UOM, @UOMQTY, @UnitPrice, @OldPrice, @VATInclusive)"
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@ItemCode", row.Cells(dgcCode.Index).Value)
            SQL.AddParam("@UOM", row.Cells(dgcUOM.Index).Value)
            SQL.AddParam("@UOMQTY", CDec(row.Cells(dgcQTY.Index).Value))
            If IsNumeric(row.Cells(dgcNewPrice.Index).Value) Then
                SQL.AddParam("@UnitPrice", CDec(row.Cells(dgcNewPrice.Index).Value))
            Else
                SQL.AddParam("@UnitPrice", CDec(row.Cells(dgcUnitPrice.Index).Value))
            End If
            SQL.AddParam("@OldPrice", CDec(row.Cells(dgcUnitPrice.Index).Value))
            SQL.AddParam("@VATInclusive", row.Cells(dgcVATInc.Index).Value)
            SQL.ExecNonQuery(query)
        Next
    End Sub

    Private Sub UpdateIPL()
        Dim SQL As New SQLControl
        SQL.BeginTransaction()
        Try
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " UPDATE    tblIPL " &
                        " SET       IPL_No = @IPL_No, EffectivityDate = @EffectivityDate, Description = @Description, Type = @Type, ApplyAllUOM = @ApplyAllUOM,   " &
                        "           MarkupMethod = @MarkupMethod, MarkupValue = @MarkupValue, MarkupBasis = @MarkupBasis, " &
                        "           WhoModified = @WhoModified, Status = @Status, DateModified = GETDATE() " &
                        " WHERE     TransID = @TransID  "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@IPL_No", IPLNo)
            SQL.AddParam("@EffectivityDate", dtpDocDate.Text)
            SQL.AddParam("@Description", txtDescription.Text)
            If rbManual.Checked = True Then
                SQL.AddParam("@Type", "Manual")
            ElseIf rbMarkup.Checked = True Then
                SQL.AddParam("@Type", "Markup")
            ElseIf rbImport.Checked = True Then
                SQL.AddParam("@Type", "Import")
            End If
            SQL.AddParam("@ApplyAllUOM", chkApplyAllUOM.Checked)
            SQL.AddParam("@MarkupMethod", cbMarkupMethod.SelectedItem)
            If IsNumeric(txtMarkupValue.Text) Then
                SQL.AddParam("@MarkupValue", CDec(txtMarkupValue.Text))
            Else
                SQL.AddParam("@MarkupValue", 0)
            End If
            SQL.AddParam("@MarkupBasis", cbMarkupBasedOn.SelectedItem)
            SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(insertSQL)
            If rbManual.Checked Or rbImport.Checked Then
                SaveItems(SQL)
            End If
            SaveFilters(SQL)
            SaveUOM(SQL)
            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", ColumnPK, txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub SaveFilters(SQL As SQLControl)
        Dim query As String
        query = " DELETE FROM tblIPL_Filter WHERE TransID = @TransID "
        SQL.FlushParams()
        SQL.AddParam("@TransID", TransID)
        SQL.ExecNonQuery(query)

        For Each row As DataGridViewRow In dgvFilter.Rows
            query = " INSERT INTO " &
               " tblIPL_Filter(TransID, DBField, Field, Value) " &
               " VALUES(@TransID, @DBField, @Field, @Value)"
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@DBField", row.Cells(dgcDBField.Index).Value)
            SQL.AddParam("@Field", row.Cells(dgcField.Index).Value)
            SQL.AddParam("@Value", row.Cells(dgcValue.Index).Value)
            SQL.ExecNonQuery(query)
        Next
    End Sub

    Private Sub SaveUOM(SQL As SQLControl)
        Dim query As String

        query = " DELETE FROM tblIPL_UOM WHERE TransID = @TransID "
        SQL.FlushParams()
        SQL.AddParam("@TransID", TransID)
        SQL.ExecNonQuery(query)

        For Each item In lbUOM.Items
            query = " INSERT INTO " &
               " tblIPL_UOM(TransID, UOM) " &
               " VALUES(@TransID, @UOM)"
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@UOM", item.ToString)
            SQL.ExecNonQuery(query)
        Next
    End Sub

    Dim ReadonlyColor As Color = Color.FromArgb(234, 234, 234)
    Dim DefaultColor As Color = Color.White
    Private Sub rbManual_CheckedChanged(sender As Object, e As EventArgs) Handles rbManual.CheckedChanged, rbMarkup.CheckedChanged, rbImport.CheckedChanged
        If disableEvent = False Then
            With dgvList
                .Columns(dgcCode.Index).DefaultCellStyle.BackColor = DefaultColor
                .Columns(dgcName.Index).DefaultCellStyle.BackColor = DefaultColor
                .Columns(dgcBarcode.Index).DefaultCellStyle.BackColor = DefaultColor
                .Columns(dgcUOM.Index).DefaultCellStyle.BackColor = DefaultColor
                .Columns(dgcUnitPrice.Index).DefaultCellStyle.BackColor = DefaultColor
                .Columns(dgcPriceList.Index).DefaultCellStyle.BackColor = DefaultColor
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .EditMode = DataGridViewEditMode.EditProgrammatically
            End With
            If rbManual.Checked = True Then
                With dgvList
                    .SelectionMode = DataGridViewSelectionMode.CellSelect
                    .EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
                    .Columns(dgcCode.Index).DefaultCellStyle.BackColor = ReadonlyColor
                    .Columns(dgcName.Index).DefaultCellStyle.BackColor = ReadonlyColor
                    .Columns(dgcBarcode.Index).DefaultCellStyle.BackColor = ReadonlyColor
                    .Columns(dgcUOM.Index).DefaultCellStyle.BackColor = ReadonlyColor
                    .Columns(dgcUnitPrice.Index).DefaultCellStyle.BackColor = ReadonlyColor
                    .Columns(dgcPriceList.Index).DefaultCellStyle.BackColor = ReadonlyColor
                    .EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
                End With
            ElseIf rbMarkup.Checked = True Then
                dgvList.EditMode = DataGridViewEditMode.EditProgrammatically
            ElseIf rbImport.Checked = True Then
                dgvList.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
            End If
            tsbDownload.Enabled = rbImport.Checked
            tsbUpload.Enabled = rbImport.Checked
            cbMarkupMethod.Enabled = rbMarkup.Checked
            cbMarkupBasedOn.Enabled = rbMarkup.Checked
            txtMarkupValue.Enabled = rbMarkup.Checked
            dgvList.Columns(dgcNewPrice.Index).Visible = rbManual.Checked
            dgvList.Columns(dgcNewPrice.Index).ReadOnly = Not rbManual.Checked
            dgvList.Columns(dgcQTY.Index).ReadOnly = Not rbManual.Checked
            dgvList.Columns(dgcVATInc.Index).ReadOnly = Not rbManual.Checked

        End If
    End Sub

    Private Sub tsbSearch_Click(sender As Object, e As EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("IPL_MANAGE") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("IPL")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadIPL(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub LoadIPL(ByVal ID As String)
        Dim query As String
        query = " SELECT     tblIPL.TransID, IPL_No, EffectivityDate, Description, Type, ApplyAllUOM,  MarkupMethod, MarkupValue, MarkupBasis, Status  " &
                " FROM       tblIPL  " &
                " WHERE      tblIPL.TransId = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            TransID = SQL.SQLDR("TransID").ToString
            IPLNo = SQL.SQLDR("IPL_No").ToString
            txtTransNum.Text = IPLNo
            txtDescription.Text = SQL.SQLDR("Description").ToString
            chkApplyAllUOM.Checked = SQL.SQLDR("ApplyAllUOM")
            txtStatus.Text = SQL.SQLDR("Status").ToString
            dtpDocDate.Text = SQL.SQLDR("EffectivityDate").ToString
            If SQL.SQLDR("Type").ToString = "Markup" Then
                rbMarkup.Checked = True
            ElseIf SQL.SQLDR("Type").ToString = "Manual" Then
                rbManual.Checked = True
            ElseIf SQL.SQLDR("Type").ToString = "Import" Then
                rbImport.Checked = True
            End If
            cbMarkupMethod.SelectedItem = SQL.SQLDR("MarkupMethod").ToString
            txtMarkupValue.Text = CDec(SQL.SQLDR("MarkupValue")).ToString("N2")
            cbMarkupBasedOn.SelectedItem = SQL.SQLDR("MarkupBasis").ToString

            tsbDownload.Enabled = rbImport.Checked
            tsbUpload.Enabled = rbImport.Checked
            LoadItems(TransID)
            LoadFilter(TransID)
            If chkApplyAllUOM.Checked = False Then
                LoadUOM(TransID)
            Else
                lbUOM.Items.Clear()
            End If

            ' TOOLSTRIP BUTTONS
            If txtStatus.Text = "Cancelled" Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            Else
                tsbEdit.Enabled = True
                tsbCancel.Enabled = True
            End If
            tsbClose.Enabled = True
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbSave.Enabled = False
            tsbNew.Enabled = True
            tsbSearch.Enabled = True
            tsbExit.Enabled = True

            EnableControl(False)
        Else
            ClearText()
        End If

    End Sub

    Private Sub LoadItems(ID As Integer)
        Dim query As String
        dgvList.Rows.Clear()

        query = " SELECT        ItemCode, Barcode, ItemName, UOM, UnitPrice, OldPrice, UOMQTY, VATInc  " &
                 " FROM         viewIPL_PriceList " &
                 " WHERE        TransID = @TransID "
        SQL.FlushParams()
        SQL.AddParam("@TransID", ID)
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            If rbManual.Checked = True Then
                dgvList.Rows.Add(SQL.SQLDR("ItemCode").ToString, SQL.SQLDR("Barcode").ToString, SQL.SQLDR("ItemName").ToString, SQL.SQLDR("UOM").ToString,
                                   CDec(SQL.SQLDR("OldPrice")).ToString("N2"), IIf(SQL.SQLDR("UnitPrice").ToString = SQL.SQLDR("OldPrice").ToString, "", CDec(SQL.SQLDR("UnitPrice")).ToString("N2")),
                                   CDec(SQL.SQLDR("UOMQTY")).ToString("N2"), SQL.SQLDR("VATInc").ToString, txtDescription.Text)
            Else
                dgvList.Rows.Add(SQL.SQLDR("ItemCode").ToString, SQL.SQLDR("Barcode").ToString, SQL.SQLDR("ItemName").ToString, SQL.SQLDR("UOM").ToString,
                                   CDec(SQL.SQLDR("UnitPrice")).ToString("N2"), CDec(SQL.SQLDR("UnitPrice")).ToString("N2"),
                                   CDec(SQL.SQLDR("UOMQTY")).ToString("N2"), SQL.SQLDR("VATInc").ToString, txtDescription.Text)

            End If

        End While
    End Sub

    Private Sub LoadFilter(ID As Integer)
        Dim querty As String
        dgvFilter.Rows.Clear()
        querty = " SELECT DBField, Field, Value FROM tblIPL_Filter WHERE TransID = @TransID ORDER BY LineNum "
        SQL.FlushParams()
        SQL.AddParam("@TransID", ID)
        SQL.ReadQuery(querty)
        While SQL.SQLDR.Read
            dgvFilter.Rows.Add(SQL.SQLDR("DBField").ToString, SQL.SQLDR("Field").ToString, SQL.SQLDR("Value").ToString)
        End While
    End Sub

    Private Sub LoadUOM(ID As Integer)
        Dim querty As String
        lbUOM.Items.Clear()
        querty = " SELECT UOM FROM tblIPL_UOM WHERE TransID = @TransID ORDER BY UOM "
        SQL.FlushParams()
        SQL.AddParam("@TransID", ID)
        SQL.ReadQuery(querty)
        While SQL.SQLDR.Read
            lbUOM.Items.Add(SQL.SQLDR("UOM").ToString)
        End While
        LoadUOM()
    End Sub

    Private Sub frmItem_Pricelist_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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
            ElseIf e.KeyCode = Keys.Right Then
                If tsbNext.Enabled = True Then tsbNext.PerformClick()
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

    Private Sub tsbNext_Click(sender As Object, e As EventArgs) Handles tsbNext.Click
        If IPLNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblIPL  WHERE IPL_No > '" & IPLNo & "' ORDER BY IPL_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadIPL(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As Object, e As EventArgs) Handles tsbPrevious.Click
        If IPLNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblIPL  WHERE IPL_No < '" & IPLNo & "' ORDER BY IPL_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadIPL(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbEdit_Click(sender As Object, e As EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("IPL_MANAGE") Then
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
        End If
    End Sub

    Private Sub tsbDownload_Click(sender As Object, e As EventArgs) Handles tsbDownload.Click
        tsbLabel.Visible = True
        tsbLabel.Text = "Downloading Template..."
        dgvFilter.Enabled = False
        GroupBox2.Enabled = False
        tsbBar.Visible = True
        bgwDownload.RunWorkerAsync()
    End Sub

    Private Sub bgwDownload_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDownload.DoWork
        Dim templateName As String = "PRICE_LIST.xlsx"
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim ctr As Integer = 0
        Dim SQL As New SQLControl
        xlApp = New Excel.Application
        Dim fileSuffix As String = (CDate(Date.Now).ToString("MMddYYYhhmmss"))
        SetPGBmax(dgvList.Rows.Count * (dgvList.Columns.Count - 1))
        If dgvList.Rows.Count > 0 Then
            App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName & "\Templates"
            If My.Computer.FileSystem.FileExists(App_Path + "\" & templateName) Then
                xlWorkBook = xlApp.Workbooks.Open(App_Path + "\" & templateName)
                xlWorkSheet = xlWorkBook.Worksheets("Template")
                xlWorkSheet.Unprotect("@dm1nEvo")
                For i As Integer = 0 To dgvList.Rows.Count - 1
                    For j As Integer = 0 To dgvList.Columns.Count - 2
                        If j < dgcNewPrice.Index Then  ' FIELDS BEFORE OLD PRICE
                            xlWorkSheet.Cells(i + 2, j + 1) = dgvList.Item(j, i).Value
                            bgwDownload.ReportProgress((i * (dgvList.Columns.Count - 1)) + (j + 1))
                        ElseIf j > dgcNewPrice.Index Then  ' FIELDS AFTER OLD PRICE
                            xlWorkSheet.Cells(i + 2, j) = dgvList.Item(j, i).Value
                            bgwDownload.ReportProgress((i * (dgvList.Columns.Count - 1)) + (j + 1))
                        End If
                    Next
                    ctr += 1
                    xlWorkSheet.Rows(2 + 1 + ctr).Insert
                Next
                xlWorkSheet.Protect("@dm1nEvo")
                xlWorkBook.SaveAs(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & Replace(templateName, ".xlsx", "") & fileSuffix & ".xlsx")
                xlWorkBook.Close(Type.Missing, Type.Missing, Type.Missing)
                xlApp.Quit()

                ' CLEAN UP. (CLOSE INSTANCES OF EXCEL OBJECTS.)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet) : xlWorkSheet = Nothing
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook) : xlWorkBook = Nothing
            Else
                MsgBox("No Template found!" & vbNewLine & "Please contact your systems administrator", MsgBoxStyle.Exclamation)
            End If
        End If
        ' CLEAN UP. (CLOSE INSTANCES OF EXCEL OBJECTS.)
        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp) : xlApp = Nothing
        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & Replace(templateName, ".xlsx", "") & fileSuffix & ".xlsx") Then
            Dim xls As Excel.Application
            Dim workbook As Excel.Workbook
            xls = New Excel.Application
            xls.Visible = True
            workbook = xls.Workbooks.Open(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & Replace(templateName, ".xlsx", "") & fileSuffix & ".xlsx")
        End If
    End Sub

    Private Delegate Sub SetPGBmaxInvoker(ByVal Value As String)
    Private Sub SetPGBmax(ByVal Value As String)
        If Me.InvokeRequired Then
            Me.Invoke(New SetPGBmaxInvoker(AddressOf SetPGBmax), Value)
        Else
            tsbBar.Maximum = Value
            tsbBar.Value = 0
        End If
    End Sub

    Private Sub bgwDownload_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgwDownload.ProgressChanged
        tsbBar.Value = e.ProgressPercentage
    End Sub

    Private Sub bgwDownload_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwDownload.RunWorkerCompleted
        tsbBar.Visible = False
        tsbLabel.Visible = False
        tsbBar.Value = 0
        dgvFilter.Enabled = True
        GroupBox2.Enabled = True
    End Sub

    Private Sub tsbUpload_Click(sender As Object, e As EventArgs) Handles tsbUpload.Click
        Dim OpenFileDialog As New OpenFileDialog
        tsbLabel.Visible = True
        tsbLabel.Text = "Uploading Data..."
        dgvFilter.Enabled = False
        tsbSave.Enabled = False
        GroupBox2.Enabled = False
        tsbBar.Visible = True
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            FileName = OpenFileDialog.FileName
            dgvList.Rows.Clear()
            bgwUpload.RunWorkerAsync()
        End If

    End Sub

    Private Sub bgwUpload_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwUpload.DoWork
        Dim firstRow As Integer
        Dim ctr As Integer = 0
        Dim str As String
        Dim objExcel As New Microsoft.Office.Interop.Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim range As Excel.Range

        xlWorkBook = objExcel.Workbooks.Open(FileName)
        xlWorkSheet = xlWorkBook.Worksheets(1)
        range = xlWorkSheet.UsedRange
        str = "a"
        firstRow = 2
        disableEvent = True

        SetPGBmax(range.Rows.Count)
        Do While str <> ""
            Dim ItemCode, Barcode, ItemName, UOM As String
            Dim UnitPrice As Decimal = 0
            Dim UOMQTY As Decimal = 0
            Dim VATInc As Boolean = True
            ItemCode = RTrim(objExcel.Range("a" & CStr(firstRow)).Value)
            Barcode = RTrim(objExcel.Range("b" & CStr(firstRow)).Value)
            ItemName = RTrim(objExcel.Range("c" & CStr(firstRow)).Value)
            UOM = RTrim(objExcel.Range("d" & CStr(firstRow)).Value)
            If Not IsNumeric(RTrim(objExcel.Range("e" & CStr(firstRow)).Value)) Then
                UnitPrice = 0
            Else
                UnitPrice = RTrim(objExcel.Range("e" & CStr(firstRow)).Value)
            End If

            If Not IsNumeric(RTrim(objExcel.Range("f" & CStr(firstRow)).Value)) Then
                UOMQTY = 0
            Else
                UOMQTY = RTrim(objExcel.Range("f" & CStr(firstRow)).Value)
            End If
            If RTrim(objExcel.Range("g" & CStr(firstRow)).Value) = "FALSE" Then
                VATInc = False
            Else
                VATInc = True
            End If
            If ItemCode <> "" Then
                AddRow()
                AddValue(ItemCode, ctr, 0, DbType.String)
                AddValue(Barcode, ctr, 1, DbType.String)
                AddValue(ItemName, ctr, 2, DbType.String)
                AddValue(UOM, ctr, 3, DbType.String)
                AddValue(UnitPrice, ctr, 4, DbType.Decimal)
                AddValue(UnitPrice, ctr, 5, DbType.Decimal)
                AddValue(UOMQTY, ctr, 6, DbType.Decimal)
                AddValue(VATInc, ctr, 7, DbType.Boolean)
                AddValue("", ctr, 8, DbType.String)
            End If


            firstRow += 1
            ctr += 1
            str = RTrim(objExcel.Range("a" & CStr(ctr)).Value)
            bgwUpload.ReportProgress(ctr)
        Loop
        disableEvent = False
        objExcel.Workbooks.Close()
        ValidateDGV()
    End Sub
    Private Delegate Sub AddValueInvoker(ByVal Value As String, ByVal row As Integer, ByVal col As Integer, DataType As DbType)
    Private Sub AddValue(ByVal Value As String, ByVal row As Integer, ByVal col As Integer, DataType As DbType)
        If Me.InvokeRequired Then
            Me.Invoke(New AddValueInvoker(AddressOf AddValue), Value, row, col, DataType)
        Else
            Select Case DataType
                Case DbType.Decimal
                    dgvList.Item(col, row).Value = CDec(Value).ToString("N2")
                Case DbType.Boolean
                    dgvList.Item(col, row).Value = CBool(Value)
                Case Else
                    dgvList.Item(col, row).Value = Value.ToString

            End Select
        End If
    End Sub

    Private Sub AddRow()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf AddRow))
        Else
            dgvList.Rows.Add("")
        End If
    End Sub
    Private Function ValidateDGV() As Boolean
        If dgvList.Rows.Count = 0 Then
            MsgBox("There are no item on the list!", MsgBoxStyle.Exclamation)
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub bgwUpload_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgwUpload.ProgressChanged
        tsbBar.Value = e.ProgressPercentage
        lblCounter.Text = "Record Count : " & dgvList.Rows.Count
    End Sub

    Private Sub bgwUpload_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwUpload.RunWorkerCompleted
        tsbBar.Visible = False
        tsbBar.Value = 0
        tsbSave.Enabled = True
        tsbLabel.Visible = False
        tsbSave.Enabled = True
        dgvFilter.Enabled = True
        GroupBox2.Enabled = True
        lblCounter.Text = "Record Count : " & dgvList.Rows.Count
    End Sub

    Private Sub dgvList_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles dgvList.CellValidating
        If e.ColumnIndex = dgcNewPrice.Index Or e.ColumnIndex = dgcQTY.Index Then
            Dim dc As Decimal
            If e.FormattedValue.ToString <> String.Empty AndAlso Not Decimal.TryParse(e.FormattedValue.ToString, dc) Then
                Msg("Invalid Amount!", vbExclamation)
                e.Cancel = True
            End If

        End If
    End Sub

    Private Sub dgvList_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellEndEdit
        If e.ColumnIndex <> -1 AndAlso e.RowIndex <> -1 Then
            If e.ColumnIndex = dgcNewPrice.Index Or e.ColumnIndex = dgcQTY.Index Then
                If IsNumeric(dgvList.Item(e.ColumnIndex, e.RowIndex).Value) Then
                    dgvList.Item(e.ColumnIndex, e.RowIndex).Value = CDec(dgvList.Item(e.ColumnIndex, e.RowIndex).Value).ToString("N2")
                End If
            End If
        End If
    End Sub

    Private Sub dgvList_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvList.KeyDown
        If rbManual.Checked = True AndAlso tsbSave.Enabled = True Then
            If e.KeyCode = Keys.Tab Then
                If dgvList.CurrentCell.ColumnIndex = dgcVATInc.Index Then
                    If dgvList.CurrentCell.RowIndex + 1 < dgvList.Rows.Count Then
                        Dim rowInd As Integer = dgvList.CurrentCell.RowIndex + 1
                        dgvList.CurrentCell = Nothing
                        dgvList.CurrentCell = dgvList.Item(dgcNewPrice.Index, rowInd)
                        e.SuppressKeyPress = True
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub dgvFilter_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvFilter.KeyDown
        If e.KeyCode = Keys.F2 Then
            If dgvFilter.CurrentCell.ColumnIndex = dgcValue.Index Then
                If dgvFilter.CurrentCell.RowIndex >= 0 Then
                    dgvFilter.CurrentCell.Value = ""
                End If
            End If
        End If
    End Sub

    Private Sub dgvFilter_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFilter.CellContentClick

    End Sub
End Class