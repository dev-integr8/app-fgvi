Imports Excel = Microsoft.Office.Interop.Excel
Public Class frmGR
    Dim TransID, RefID, JETransID As String
    Dim GRNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "GR"
    Dim ColumnPK As String = "GR_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblGR"
    Dim TransAuto As Boolean
    Dim SysCode As String = "EMERALD"
    Dim GI_ID, IC_ID, MRIS_ID As Integer
    Dim accntVAT As String
    Dim ForApproval As Boolean = False


    Dim Valid As Boolean = True
    Dim InvalidTemplate As Boolean = False
    Dim path As String
    Dim templateName As String = "TEMPLATE_GR"
    Public excelPW As String = "@dm1nEvo"

    Public Overloads Function ShowDialog(ByVal docnumber As String) As Boolean
        TransID = docnumber
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmGR_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            LoadSetup()
            loadWH()
            loadGRType("Load")
            If TransID <> "" Then
                LoadGR(TransID)
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

    Private Sub LoadSetup()
        Dim query As String
        query = " SELECT    TAX_OV" & _
                " FROM      tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            accntVAT = SQL.SQLDR("TAX_OV")
        End If
    End Sub

    Private Sub loadGRType(ByVal Status As String)
        Dim query As String
        If Status = "View" Then
            query = " SELECT Description FROM tblGR_Type"
            SQL.ReadQuery(query)
        ElseIf Status = "Load" Then
            query = " SELECT Description FROM tblGR_Type WHERE Status = 'Active'"
            SQL.ReadQuery(query)
        End If
        cbTransType.Items.Clear()
        cbTransType.Items.Add("Material Transfer Confirmation")
        While SQL.SQLDR.Read
            cbTransType.Items.Add(SQL.SQLDR("Description").ToString)
        End While

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
        cbCurrency.Enabled = Value
        txtConversion.Enabled = Value
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
        If Not AllowAccess("GR_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("GR")
            TransID = f.transID
            LoadGR(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub LoadGR(ByVal ID As String)
        Dim query As String
        Dim WHSEfrom, WHSEto, Type As String
        query = " SELECT   TransID, GR_No, VCECode, DateGR, Type, ReceivedBy, ReceivedFrom, WHSE_From, WHSE_To, Remarks, ISNULL(GI_Ref,0) AS GI_Ref, Status, ISNULL(IC_Ref,0) AS IC_Ref, ISNULL(MRIS_Ref,0) AS MRIS_Ref " & _
                " FROM     tblGR " & _
                " WHERE    TransId = '" & ID & "' " & _
                " ORDER BY TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            GRNo = SQL.SQLDR("GR_No").ToString
            GI_ID = SQL.SQLDR("GI_Ref").ToString
            IC_ID = SQL.SQLDR("IC_Ref").ToString
            MRIS_ID = SQL.SQLDR("MRIS_Ref").ToString
            txtTransNum.Text = GRNo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            dtpDocDate.Text = SQL.SQLDR("DateGR").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            disableEvent = True
            cbReceiver.SelectedItem = SQL.SQLDR("ReceivedBy").ToString
            cbReceivedFrom.SelectedItem = SQL.SQLDR("ReceivedFrom").ToString
            WHSEfrom = SQL.SQLDR("WHSE_From").ToString
            WHSEto = SQL.SQLDR("WHSE_To").ToString
            Type = SQL.SQLDR("Type").ToString
            txtMRRef.Text = SQL.SQLDR("GI_Ref").ToString
            loadGRType("View")
            cbTransType.SelectedItem = Type
            disableEvent = False
            loadWHFrom()
            loadWHto()

            If cbReceivedFrom.SelectedItem = "Production" Then
                cbWHfrom.SelectedItem = GetWHSE(WHSEfrom, "tblProdWarehouse")
            Else
                cbWHfrom.SelectedItem = GetWHSE(WHSEfrom, "tblWarehouse")
            End If
            If cbReceiver.SelectedItem = "Production" Then
                cbWHto.SelectedItem = GetWHSE(WHSEto, "tblProdWarehouse")
            Else
                cbWHto.SelectedItem = GetWHSE(WHSEto, "tblWarehouse")
            End If

            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            txtICRef.Text = LoadICNo(IC_ID)
            txtMRIS_Ref.Text = LoadMRIS(MRIS_ID)
            LoadGRDetails(TransID)
            LoadEntry(TransID)
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

            If Inv_ComputationMethod <> "SC" Then
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

    Private Function LoadMRIS(ID As Integer) As String
        Dim query As String
        query = " SELECT MRIS_No FROM tblMRIS WHERE TransID = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("MRIS_No")
        Else
            Return 0
        End If
    End Function

    Private Function LoadICNo(IC_ID As Integer) As String
        Dim query As String
        query = " SELECT IC_No FROM tblIC WHERE TransID = '" & IC_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("IC_No")
        Else
            Return 0
        End If
    End Function

    Private Sub LoadEntry(ByVal RRNo As Integer)
        Dim query As String
        query = " SELECT ID, JE_No, View_GL_Transaction.BranchCode, View_GL_Transaction.AccntCode, AccountTitle, View_GL_Transaction.VCECode, View_GL_Transaction.VCEName, Debit, Credit, Particulars, RefNo   " & _
                " FROM   View_GL_Transaction INNER JOIN tblCOA_Master " & _
                " ON     View_GL_Transaction.AccntCode = tblCOA_Master.AccountCode " & _
                " WHERE JE_No = (SELECT  JE_No FROM tblJE_Header WHERE RefType = 'GR' AND RefTransID = " & RRNo & ") " & _
                " ORDER BY LineNumber "
        SQL.ReadQuery(query)
        lvAccount.Items.Clear()
        While SQL.SQLDR.Read
            JETransID = SQL.SQLDR("JE_No")
            lvAccount.Items.Add(SQL.SQLDR("AccntCode").ToString)
            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(SQL.SQLDR("AccountTitle").ToString)
            If SQL.SQLDR("Debit").ToString = 0 Then lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("") Else lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(SQL.SQLDR("Debit")).ToString("N2"))
            If SQL.SQLDR("Credit").ToString = 0 Then lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("") Else lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(SQL.SQLDR("Credit")).ToString("N2"))
            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(SQL.SQLDR("RefNo").ToString)
        End While

        TotalDBCR()
    End Sub

    Protected Sub LoadGRDetails(ByVal TransID As String)
        Dim ctr As Integer = 0
        Dim query As String
        query = " SELECT tblGR_Details.ItemCode, tblGR_Details.Description, tblGR_Details.UOM, QTY, tblGR_Details.WHSE, ISNULL(AvgCost,0) AS AvgCost, AccntCode, " & _
                " ISNULL(tblGR_Details.UnitPrice, 0) AS UnitPrice,  ISNULL(tblGR_Details.GrossAmount, 0) AS GrossAmount,  ISNULL(tblGR_Details.VATAmount, 0) AS VATAmount, " & _
                " ISNULL(tblGR_Details.DiscountRate, 0) AS DiscountRate,  ISNULL(tblGR_Details.Discount, 0) AS Discount,  ISNULL(tblGR_Details.NetAmount, 0) AS NetAmount, " & _
                " ISNULL(tblGR_Details.VATable, 0) AS VATable,  ISNULL(tblGR_Details.VatInc, 0) AS VatInc, ISNULL(SerialNo,'') AS SerialNo, ISNULL(LotNo,'') AS LotNo, " & _
                " ISNULL(DateExpired,'') AS DateExpired, ISNULL(tblGR_Details.Size,'') AS Size ,ISNULL(tblGR_Details.Color,'') AS Color   " & _
                " FROM tblGR INNER JOIN tblGR_Details " & _
                " ON tblGR.TransID = tblGR_Details.TransID " & _
                " WHERE  tblGR_Details.TransId = " & TransID & " " & _
                " ORDER BY tblGR_Details.LineNum "
        disableEvent = True
        dgvItemMaster.Rows.Clear()
        disableEvent = False
        SQL.ReadQuery(query)
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvItemMaster.Rows.Add(row(0).ToString, "", row(1).ToString, row(2).ToString, _
                                     CDec(row(3)).ToString("N2"), GetWHSEDesc(row(4).ToString), row(6).ToString, CDec(row(5)).ToString("N2"), _
                                     CDec(row(7)).ToString("N2"), CDec(row(8)).ToString("N2"), CDec(row(9)).ToString("N2"), CDec(row(10)).ToString("N2"), _
                                       CDec(row(11)).ToString("N2"), CDec(row(12)).ToString("N2"), row(13), row(14), row(17).ToString, row(16).ToString, _
                                       row(15).ToString, row(18).ToString, row(19).ToString)
                LoadUOM(row(0).ToString, ctr)
                LoadColor(row(0).ToString, ctr)
                LoadSize(row(0).ToString, ctr)
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
        txtICRef.Clear()
        txtMRRef.Clear()
        txtMRIS_Ref.Clear()
        txtTotalDebit.Clear()
        txtTotalCredit.Clear()
        lvAccount.Items.Clear()
        cbWHfrom.SelectedIndex = -1
        cbTransType.SelectedIndex = -1
        dgvItemMaster.Rows.Clear()
        Valid = True
        txtRemarks.Clear()
        txtStatus.Text = "Open"
        dtpDocDate.MinDate = GetMaxPEC().AddDays(1)
        If Inv_ComputationMethod <> "SC" Then
            dtpDocDate.MinDate = GetMaxInventoryDate()
        End If
        dtpDocDate.Value = Date.Today.Date
        cbCurrency.Items.Clear()
        txtConversion.Text = ""
        loadGRType("Load")
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("GR_ADD") Then
            msgRestricted()
        Else
            Cleartext()
            TransID = ""
            GRNo = ""
            GI_ID = 0
            IC_ID = 0
            MRIS_ID = 0



            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbCancel.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbUpload.Enabled = True
            tsbDownload.Enabled = True
            tsbExit.Enabled = False
            tsbPrint.Enabled = False
            tsbCopy.Enabled = True
            EnableControl(True)

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)


        End If
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("GR_EDIT") Then
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
        Try
            If DataValidated() Then
                If TransID = "" Then
                    If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                        TransID = GenerateTransID(ColumnID, DBTable)
                        GRNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                        txtTransNum.Text = GRNo
                        SaveGR()
                        Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                        GRNo = txtTransNum.Text
                        LoadGR(TransID)
                    End If
                Else
                    If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                        UpdateGR()
                        Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                        GRNo = txtTransNum.Text
                        LoadGR(TransID)
                    End If
                End If
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Function DataValidated() As Boolean
        If cbWHto.SelectedIndex = -1 Then
            Msg("Please select warehouse!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf cbWHfrom.SelectedIndex = -1 And cbTransType.SelectedItem = "Material Transfer Confirmation" Then
            Msg("Please select from warehouse!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf dgvItemMaster.Rows.Count <= 1 Then
            Msg("There are no items on the list!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf cbTransType.SelectedIndex = -1 Then
            Msg("Please select transaction type!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf Valid = False Then
            Msg("Please check uploaded items!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf validateDGV() = False Then
            Return False
        Else
            Return True
        End If
        Return True

    End Function

    Private Function validateDGV() As Boolean
        Dim QTY As Decimal
        Dim value As Boolean = True
        For Each row As DataGridViewRow In dgvItemMaster.Rows
            If row.Cells(chItemCode.Index).Value <> "" Then
                If Not IsNumeric(row.Cells(dgcQTY.Index).Value) Then QTY = 0 Else QTY = row.Cells(dgcQTY.Index).Value
                If QTY = 0 Then
                    Msg("Quantity should not be equal to zero.", MsgBoxStyle.Exclamation)
                    value = False
                    Exit For
                End If

                If row.Cells(chItemCode.Index).Value <> "" And row.Cells(chUOM.Index).Value = "" Then
                    Msg("Some items dont have UOM.", MsgBoxStyle.Exclamation)
                    value = False
                    Exit For
                End If


                'If row.Cells(chDateExpired.Index).Value <> "" And row.Cells(chDateExpired.Index).Value = "" Then
                '    Msg("Some items dont have expiry date.", MsgBoxStyle.Exclamation)
                '    value = False
                '    Exit For
                'End If

                'If row.Cells(chLotNo.Index).Value <> "" And row.Cells(chLotNo.Index).Value = "" Then
                '    Msg("Some items dont have lot number.", MsgBoxStyle.Exclamation)
                '    value = False
                '    Exit For
                'End If

                'If row.Cells(chSerialNo.Index).Value <> "" And row.Cells(chSerialNo.Index).Value = "" Then
                '    Msg("Some items dont have serial number.", MsgBoxStyle.Exclamation)
                '    value = False
                '    Exit For
                'End If
            End If
        Next
        Return value
    End Function

    Private Sub SaveGR()
        Dim SQL1 As New SQLControl
        SQL1.BeginTransaction()
        Try
            Dim WHSEfrom, WHSEto, Type As String
            Type = IIf(cbTransType.SelectedIndex = -1, "", cbTransType.SelectedItem)

            WHSEto = GetWHSE(cbWHto.SelectedItem)
            If cbTransType.SelectedItem = "Material Transfer Confirmation" Then
                WHSEfrom = GetWHSE(cbWHfrom.SelectedItem)
            End If

            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblGR(TransID, GR_No, BranchCode, BusinessCode, DateGR, VCECode, ReceivedBy, ReceivedFrom, WHSE_From, WHSE_To, Type," & _
                        " Remarks, GI_Ref, WhoCreated, Currency, Exchange_Rate, IC_Ref, Status, MRIS_Ref) " & _
                        " VALUES (@TransID, @GR_No, @BranchCode, @BusinessCode, @DateGR, @VCECode, @ReceivedBy, @ReceivedFrom, @WHSE_From, @WHSE_To, " & _
                        " @Type, @Remarks, @GI_Ref, @WhoCreated, @Currency, @Exchange_Rate, @IC_Ref, @Status, @MRIS_Ref) "
            SQL1.FlushParams()
            SQL1.AddParam("@TransID", TransID)
            SQL1.AddParam("@GR_No", GRNo)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@DateGR", dtpDocDate.Value.Date)
            SQL1.AddParam("@ReceivedBy", cbReceiver.SelectedItem)
            SQL1.AddParam("@ReceivedFrom", IIf(IsNothing(cbReceivedFrom.SelectedItem), "", cbReceivedFrom.SelectedItem))
            SQL1.AddParam("@WHSE_From", IIf(WHSEfrom = "", "", WHSEfrom))
            SQL1.AddParam("@WHSE_To", WHSEto)
            SQL1.AddParam("@Type", Type)
            SQL1.AddParam("@GI_Ref", GI_ID)
            SQL1.AddParam("@IC_Ref", IC_ID)
            SQL1.AddParam("@MRIS_Ref", MRIS_ID)
            If cbTransType.SelectedItem <> "Material Transfer Confirmation" Then
                SQL1.AddParam("@VCECode", txtVCECode.Text)
                SQL1.AddParam("@Currency", IIf(IsNothing(cbCurrency.SelectedItem), "", cbCurrency.SelectedItem))
                SQL1.AddParam("@Exchange_Rate", IIf(txtConversion.Text = "", "0.0000", txtConversion.Text))
            Else
                SQL1.AddParam("@VCECode", "")
                SQL1.AddParam("@Currency", "")
                SQL1.AddParam("@Exchange_Rate", IIf(txtConversion.Text = "", "0.0000", txtConversion.Text))
            End If
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@WhoCreated", UserID)
            If ForApproval = True Then SQL1.AddParam("@Status", "Draft") Else SQL1.AddParam("@Status", "Active")
            SQL1.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            Dim ItemCode, Description, UOM, AccntCode, DateExpired, LotNo, SerialNo, Size, Color As String
            Dim QTY, UnitCost, ItemPrice, GrossAmount, DiscountRate, DiscountAmount, VatAmount, NetAmount As Decimal

            For Each row As DataGridViewRow In dgvItemMaster.Rows
                If Not row.Cells(dgcQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                    Description = IIf(row.Cells(chItemName.Index).Value = Nothing, "", row.Cells(chItemName.Index).Value)
                    UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                    If IsNumeric(row.Cells(dgcQTY.Index).Value) Then QTY = CDec(row.Cells(dgcQTY.Index).Value) Else QTY = 1
                    AccntCode = IIf(row.Cells(chAccntCode.Index).Value = Nothing, "", row.Cells(chAccntCode.Index).Value)
                    UnitCost = IIf(row.Cells(chAvgCost.Index).Value = Nothing, "0.00", row.Cells(chAvgCost.Index).Value)

                    ItemPrice = IIf(row.Cells(chItemPrice.Index).Value = Nothing, "0.00", row.Cells(chItemPrice.Index).Value)
                    GrossAmount = IIf(row.Cells(chGrossAmount.Index).Value = Nothing, "0.00", row.Cells(chGrossAmount.Index).Value)
                    DiscountRate = IIf(row.Cells(chDiscountRate.Index).Value = Nothing, "0.00", row.Cells(chDiscountRate.Index).Value)
                    DiscountAmount = IIf(row.Cells(chDiscountAmount.Index).Value = Nothing, "0.00", row.Cells(chDiscountAmount.Index).Value)
                    VatAmount = IIf(row.Cells(chVATAmount.Index).Value = Nothing, "0.00", row.Cells(chVATAmount.Index).Value)
                    NetAmount = IIf(row.Cells(chNetAmount.Index).Value = Nothing, "0.00", row.Cells(chNetAmount.Index).Value)
                    DateExpired = IIf(row.Cells(chDateExpired.Index).Value = Nothing, "", row.Cells(chDateExpired.Index).Value)
                    LotNo = IIf(row.Cells(chLotNo.Index).Value = Nothing, "", row.Cells(chLotNo.Index).Value)
                    SerialNo = IIf(row.Cells(chSerialNo.Index).Value = Nothing, "", row.Cells(chSerialNo.Index).Value)
                    Size = IIf(row.Cells(chSize.Index).Value = Nothing, "", row.Cells(chSize.Index).Value)
                    Color = IIf(row.Cells(chColor.Index).Value = Nothing, "", row.Cells(chColor.Index).Value)

                    insertSQL = " INSERT INTO " & _
                         " tblGR_Details(TransId, ItemCode, Description, UOM, QTY, WHSE, LineNum, WhoCreated, AvgCost, AccntCode, " & _
                         "      UnitPrice, GrossAmount, DiscountRate, Discount, VATAmount, NetAmount, VATable, VatInc, SerialNo, " & _
                         "      LotNo, DateExpired, Size, Color) " & _
                         " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @WHSE, @LineNum, @WhoCreated, @AvgCost, @AccntCode, " & _
                         "      @UnitPrice, @GrossAmount, @DiscountRate, @Discount, @VATAmount, @NetAmount, @VATable, @VatInc, @SerialNo, " & _
                         "      @LotNo, @DateExpired, @Size, @Color) "
                    SQL1.FlushParams()
                    SQL1.AddParam("@TransID", TransID)
                    SQL1.AddParam("@ItemCode", ItemCode)
                    SQL1.AddParam("@Description", Description)
                    SQL1.AddParam("@UOM", UOM)
                    SQL1.AddParam("@QTY", QTY)
                    SQL1.AddParam("@AvgCost", UnitCost)
                    SQL1.AddParam("@AccntCode", AccntCode)
                    SQL1.AddParam("@WHSE", WHSEto)
                    SQL1.AddParam("@LineNum", line)
                    SQL1.AddParam("@UnitPrice", ItemPrice)
                    SQL1.AddParam("@GrossAmount", GrossAmount)
                    SQL1.AddParam("@DiscountRate", DiscountRate)
                    SQL1.AddParam("@Discount", DiscountAmount)
                    SQL1.AddParam("@VATAmount", VatAmount)
                    SQL1.AddParam("@NetAmount", NetAmount)
                    If IsNothing(row.Cells(chVAT.Index).Value) Then SQL1.AddParam("@VATable", False) Else SQL1.AddParam("@VATable", row.Cells(chVAT.Index).Value)
                    If IsNothing(row.Cells(chVATInc.Index).Value) Then SQL1.AddParam("@VatInc", False) Else SQL1.AddParam("@VatInc", row.Cells(chVATInc.Index).Value)
                    SQL1.AddParam("@SerialNo", SerialNo)
                    SQL1.AddParam("@LotNo", LotNo)
                    SQL1.AddParam("@DateExpired", DateExpired)
                    SQL1.AddParam("@Size", Size)
                    SQL1.AddParam("@Color", Color)
                    SQL1.AddParam("@WhoCreated", UserID)
                    SQL1.ExecNonQuery(insertSQL)
                    line += 1

                    If cbTransType.SelectedItem = "Material Transfer Confirmation" Then
                        SaveINVTY("OUT", ModuleID, "GI", GI_ID, dtpDocDate.Value.Date, ItemCode, WHSEfrom, QTY, UOM, UnitCost, "Active", SerialNo, LotNo, DateExpired, Size, Color)
                        SaveINVTY("IN", ModuleID, "GR", TransID, dtpDocDate.Value.Date, ItemCode, WHSEto, QTY, UOM, UnitCost, "Active", SerialNo, LotNo, DateExpired, Size, Color)
                    Else
                        SaveINVTY("IN", ModuleID, "GR", TransID, dtpDocDate.Value.Date, ItemCode, WHSEto, QTY, UOM, UnitCost, "Active", SerialNo, LotNo, DateExpired, Size, Color)
                    End If
                End If
            Next

            If cbTransType.SelectedItem = "Material Transfer Confirmation" Then
                ComputeWAUC("GI", GI_ID)
                ComputeWAUC("GR", TransID)

                Dim updateSQL As String
                updateSQL = " UPDATE tblGI SET Status ='Closed' WHERE TransID = '" & GI_ID & " '"
                SQL1.ExecNonQuery(updateSQL)
            Else
                ComputeWAUC("GR", TransID)
            End If

            line = 1
            JETransID = GenerateTransID("JE_No", "tblJE_Header")

            If cbTransType.SelectedItem <> "Inventory Count" Then
                insertSQL = " INSERT INTO " & _
                           " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Currency, Exchange_Rate, Remarks,  WhoCreated, Status) " & _
                           " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR,  @Currency, @Exchange_Rate, @Remarks,  @WhoCreated, @Status)"
                SQL1.FlushParams()
                SQL1.AddParam("@JE_No", JETransID)
                SQL1.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL1.AddParam("@RefType", "GR")
                SQL1.AddParam("@RefTransID", TransID)
                SQL1.AddParam("@Book", "Inventory")
                SQL1.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                SQL1.AddParam("@Currency", IIf(IsNothing(cbCurrency.SelectedItem), "", cbCurrency.SelectedItem))
                SQL1.AddParam("@Exchange_Rate", IIf(txtConversion.Text = "", "0.0000", txtConversion.Text))
                SQL1.AddParam("@Remarks", txtRemarks.Text)
                SQL1.AddParam("@BranchCode", BranchCode)
                SQL1.AddParam("@BusinessCode", BusinessType)
                If ForApproval = True Then SQL1.AddParam("@Status", "Draft") Else SQL1.AddParam("@Status", "Saved")
                SQL1.AddParam("@WhoCreated", "")
                SQL1.ExecNonQuery(insertSQL)

                'JETransID = LoadJE("RR", TransID)

                For Each item As ListViewItem In lvAccount.Items
                    If item.SubItems(chAccountCode.Index).Text <> "" Then
                        insertSQL = " INSERT INTO " & _
                                    " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, BranchCode) " & _
                                    " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @BranchCode)"
                        SQL1.FlushParams()
                        SQL1.AddParam("@JE_No", JETransID)
                        SQL1.AddParam("@AccntCode", item.SubItems(chAccountCode.Index).Text)
                        SQL1.AddParam("@VCECode", txtVCECode.Text)
                        If IsNumeric(item.SubItems(chDebit.Index).Text) Then
                            SQL1.AddParam("@Debit", CDec(item.SubItems(chDebit.Index).Text))
                        Else
                            SQL1.AddParam("@Debit", 0)
                        End If
                        If IsNumeric(item.SubItems(chCredit.Index).Text) Then
                            SQL1.AddParam("@Credit", CDec(item.SubItems(chCredit.Index).Text))
                        Else
                            SQL1.AddParam("@Credit", 0)
                        End If
                        SQL1.AddParam("@Particulars", "")
                        SQL1.AddParam("@RefNo", item.SubItems(chRef.Index).Text)
                        SQL1.AddParam("@LineNumber", line)
                        SQL1.AddParam("@BranchCode", BranchCode)
                        SQL1.ExecNonQuery(insertSQL)
                        line += 1
                    End If
                Next
            End If
            SQL1.Commit()
        Catch ex As Exception
            SQL1.Rollback()
            TransID = ""
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "GR_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL1.FlushParams()
        End Try
    End Sub

    Private Sub UpdateGR()
        Dim SQL As New SQLControl
        SQL.BeginTransaction()
        Try
            Dim WHSEfrom, WHSEto, Type As String
            Type = IIf(cbTransType.SelectedIndex = -1, "", cbTransType.SelectedItem)

            WHSEto = GetWHSE(cbWHto.SelectedItem)
            If cbTransType.SelectedItem = "Material Transfer Confirmation" Then
                WHSEfrom = GetWHSE(cbWHfrom.SelectedItem)
            End If
            
            activityStatus = True
            Dim insertSQL, updateSQL, deleteSQL As String
            updateSQL = " UPDATE    tblGR " & _
                        " SET       GR_No = @GR_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, DateGR = @DateGR, VCECode = @VCECode, " & _
                        "           ReceivedBy = @ReceivedBy, ReceivedFrom = @ReceivedFrom, WHSE_From = @WHSE_From, WHSE_To = @WHSE_To, Type = @Type, Remarks = @Remarks, GI_Ref = @GI_Ref, " & _
                        "           WhoModified = @WhoModified, DateModified = GETDATE(), Currency = @Currency, Exchange_Rate = @Exchange_Rate, IC_Ref = @IC_Ref, MRIS_Ref = @MRIS_Ref " & _
                        " WHERE     TransID = @TransID  "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@GR_No", GRNo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateGR", dtpDocDate.Value.Date)
            SQL.AddParam("@ReceivedBy", cbReceiver.SelectedItem)
            SQL.AddParam("@ReceivedFrom", IIf(IsNothing(cbReceivedFrom.SelectedItem), "", cbReceivedFrom.SelectedItem))
            SQL.AddParam("@WHSE_From", IIf(WHSEfrom = "", "", WHSEfrom))
            SQL.AddParam("@WHSE_To", WHSEto)
            SQL.AddParam("@Type", Type)
            SQL.AddParam("@GI_Ref", GI_ID)
            SQL.AddParam("@IC_Ref", IC_ID)
            SQL.AddParam("@MRIS_Ref", MRIS_ID)
            If cbTransType.SelectedItem <> "Material Transfer Confirmation" Then
                SQL.AddParam("@VCECode", txtVCECode.Text)
                SQL.AddParam("@Currency", IIf(IsNothing(cbCurrency.SelectedItem), "", cbCurrency.SelectedItem))
                SQL.AddParam("@Exchange_Rate", IIf(txtConversion.Text = "", "0.0000", txtConversion.Text))
            Else
                SQL.AddParam("@VCECode", "")
                SQL.AddParam("@Currency", "")
                SQL.AddParam("@Exchange_Rate", IIf(txtConversion.Text = "", "0.0000", txtConversion.Text))
            End If
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)

            deleteSQL = " DELETE FROM tblGR_Details WHERE TransID =@TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.ExecNonQuery(deleteSQL)

            DELINVTY(ModuleID, "GI", GI_ID)
            DELINVTY(ModuleID, "GR", TransID)


            Dim line As Integer = 1
            Dim ItemCode, Description, UOM, AccntCode, DateExpired, LotNo, SerialNo, Size, Color As String
            Dim QTY, UnitCost, ItemPrice, GrossAmount, DiscountRate, DiscountAmount, VatAmount, NetAmount As Decimal

            For Each row As DataGridViewRow In dgvItemMaster.Rows
                If Not row.Cells(dgcQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                    Description = IIf(row.Cells(chItemName.Index).Value = Nothing, "", row.Cells(chItemName.Index).Value)
                    UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                    If IsNumeric(row.Cells(dgcQTY.Index).Value) Then QTY = CDec(row.Cells(dgcQTY.Index).Value) Else QTY = 1
                    AccntCode = IIf(row.Cells(chAccntCode.Index).Value = Nothing, "", row.Cells(chAccntCode.Index).Value)
                    UnitCost = IIf(row.Cells(chAvgCost.Index).Value = Nothing, "0.00", row.Cells(chAvgCost.Index).Value)

                    ItemPrice = IIf(row.Cells(chItemPrice.Index).Value = Nothing, "0.00", row.Cells(chItemPrice.Index).Value)
                    GrossAmount = IIf(row.Cells(chGrossAmount.Index).Value = Nothing, "0.00", row.Cells(chGrossAmount.Index).Value)
                    DiscountRate = IIf(row.Cells(chDiscountRate.Index).Value = Nothing, "0.00", row.Cells(chDiscountRate.Index).Value)
                    DiscountAmount = IIf(row.Cells(chDiscountAmount.Index).Value = Nothing, "0.00", row.Cells(chDiscountAmount.Index).Value)
                    VatAmount = IIf(row.Cells(chVATAmount.Index).Value = Nothing, "0.00", row.Cells(chVATAmount.Index).Value)
                    NetAmount = IIf(row.Cells(chNetAmount.Index).Value = Nothing, "0.00", row.Cells(chNetAmount.Index).Value)
                    DateExpired = IIf(row.Cells(chDateExpired.Index).Value = Nothing, "", row.Cells(chDateExpired.Index).Value)
                    LotNo = IIf(row.Cells(chLotNo.Index).Value = Nothing, "", row.Cells(chLotNo.Index).Value)
                    SerialNo = IIf(row.Cells(chSerialNo.Index).Value = Nothing, "", row.Cells(chSerialNo.Index).Value)
                    Size = IIf(row.Cells(chSize.Index).Value = Nothing, "", row.Cells(chSize.Index).Value)
                    Color = IIf(row.Cells(chColor.Index).Value = Nothing, "", row.Cells(chColor.Index).Value)

                    insertSQL = " INSERT INTO " & _
                         " tblGR_Details(TransId, ItemCode, Description, UOM, QTY, WHSE, LineNum, WhoCreated, AvgCost, AccntCode, " & _
                         "      UnitPrice, GrossAmount, DiscountRate, Discount, VATAmount, NetAmount, VATable, VatInc, SerialNo, " & _
                         "      LotNo, DateExpired, Size, Color) " & _
                         " VALUES(@TransId, @ItemCode, @Description, @UOM, @QTY, @WHSE, @LineNum, @WhoCreated, @AvgCost, @AccntCode, " & _
                         "      @UnitPrice, @GrossAmount, @DiscountRate, @Discount, @VATAmount, @NetAmount, @VATable, @VatInc, @SerialNo, " & _
                         "      @LotNo, @DateExpired, @Size, @Color) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", ItemCode)
                    SQL.AddParam("@Description", Description)
                    SQL.AddParam("@UOM", UOM)
                    SQL.AddParam("@QTY", QTY)
                    SQL.AddParam("@AvgCost", UnitCost)
                    SQL.AddParam("@AccntCode", AccntCode)
                    SQL.AddParam("@WHSE", WHSEto)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@UnitPrice", ItemPrice)
                    SQL.AddParam("@GrossAmount", GrossAmount)
                    SQL.AddParam("@DiscountRate", DiscountRate)
                    SQL.AddParam("@Discount", DiscountAmount)
                    SQL.AddParam("@VATAmount", VatAmount)
                    SQL.AddParam("@NetAmount", NetAmount)
                    If IsNothing(row.Cells(chVAT.Index).Value) Then SQL.AddParam("@VATable", False) Else SQL.AddParam("@VATable", row.Cells(chVAT.Index).Value)
                    If IsNothing(row.Cells(chVATInc.Index).Value) Then SQL.AddParam("@VatInc", False) Else SQL.AddParam("@VatInc", row.Cells(chVATInc.Index).Value)
                    SQL.AddParam("@SerialNo", SerialNo)
                    SQL.AddParam("@LotNo", LotNo)
                    SQL.AddParam("@DateExpired", DateExpired)
                    SQL.AddParam("@Size", Size)
                    SQL.AddParam("@Color", Color)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1

                    If cbTransType.SelectedItem = "Material Transfer Confirmation" Then
                        SaveINVTY("OUT", ModuleID, "GI", GI_ID, dtpDocDate.Value.Date, ItemCode, WHSEfrom, QTY, UOM, UnitCost, "Active", SerialNo, LotNo, DateExpired, Size, Color)
                        SaveINVTY("IN", ModuleID, "GR", TransID, dtpDocDate.Value.Date, ItemCode, WHSEto, QTY, UOM, UnitCost, "Active", SerialNo, LotNo, DateExpired, Size, Color)
                    Else
                        SaveINVTY("IN", ModuleID, "GR", TransID, dtpDocDate.Value.Date, ItemCode, WHSEto, QTY, UOM, UnitCost, "Active", SerialNo, LotNo, DateExpired, Size, Color)
                    End If
                End If
            Next



            If cbTransType.SelectedItem = "Material Transfer Confirmation" Then
                ComputeWAUC("GI", GI_ID)
                ComputeWAUC("GR", TransID)

                updateSQL = " UPDATE tblGI SET Status ='Closed' WHERE TransID = '" & GI_ID & " '"
                SQL.ExecNonQuery(updateSQL)
            Else
                ComputeWAUC("GR", TransID)
            End If


            line = 1
            JETransID = LoadJE("GR", TransID)

            If cbTransType.SelectedItem <> "Inventory Count" Then
                ' UPDATE ENTRIES
                If JETransID = 0 Then
                    JETransID = GenerateTransID("JE_No", "tblJE_Header")

                    insertSQL = " INSERT INTO " & _
                        " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, Currency, Exchange_Rate, WhoCreated) " & _
                        " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @Currency, @Exchange_Rate, @WhoCreated)"
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransID)
                    SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                    SQL.AddParam("@RefType", "GR")
                    SQL.AddParam("@RefTransID", TransID)
                    SQL.AddParam("@Book", "Inventory")
                    SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                    SQL.AddParam("@Currency", IIf(IsNothing(cbCurrency.SelectedItem), "", cbCurrency.SelectedItem))
                    SQL.AddParam("@Exchange_Rate", IIf(txtConversion.Text = "", "0.0000", txtConversion.Text))
                    SQL.AddParam("@Remarks", txtRemarks.Text)
                    SQL.AddParam("@BranchCode", BranchCode)
                    SQL.AddParam("@BusinessCode", BusinessType)
                    SQL.AddParam("@WhoCreated", "")
                    SQL.ExecNonQuery(insertSQL)
                Else
                    updateSQL = " UPDATE tblJE_Header " & _
                               " SET    AppDate = @AppDate, BranchCode = @BranchCode, BusinessCode = @BusinessCode, " & _
                               "        RefType = @RefType, RefTransID = @RefTransID, Book = @Book, TotalDBCR = @TotalDBCR, " & _
                               "        Remarks = @Remarks, Currency = @Currency, Exchange_Rate = @Exchange_Rate, WhoModified = @WhoModified, DateModified = GETDATE() " & _
                               " WHERE  JE_No = @JE_No "
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransID)
                    SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                    SQL.AddParam("@RefType", "GR")
                    SQL.AddParam("@RefTransID", TransID)
                    SQL.AddParam("@Book", "Inventory")
                    SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                    SQL.AddParam("@Currency", IIf(IsNothing(cbCurrency.SelectedItem), "", cbCurrency.SelectedItem))
                    SQL.AddParam("@Exchange_Rate", IIf(txtConversion.Text = "", "0.0000", txtConversion.Text))
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

                For Each item As ListViewItem In lvAccount.Items
                    If item.SubItems(chAccountCode.Index).Text <> "" Then
                        insertSQL = " INSERT INTO " & _
                                    " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, LineNumber, BranchCode) " & _
                                    " VALUES(@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @RefNo, @LineNumber, @BranchCode)"
                        SQL.FlushParams()
                        SQL.AddParam("@JE_No", JETransID)
                        SQL.AddParam("@AccntCode", item.SubItems(chAccountCode.Index).Text)
                        SQL.AddParam("@VCECode", txtVCECode.Text)
                        If IsNumeric(item.SubItems(chDebit.Index).Text) Then
                            SQL.AddParam("@Debit", CDec(item.SubItems(chDebit.Index).Text))
                        Else
                            SQL.AddParam("@Debit", 0)
                        End If
                        If IsNumeric(item.SubItems(chCredit.Index).Text) Then
                            SQL.AddParam("@Credit", CDec(item.SubItems(chCredit.Index).Text))
                        Else
                            SQL.AddParam("@Credit", 0)
                        End If
                        SQL.AddParam("@Particulars", "")
                        If item.SubItems(chRef.Index).Text <> "" Then
                            SQL.AddParam("@RefNo", item.SubItems(chRef.Index).Text)
                        Else
                            SQL.AddParam("@RefNo", "")
                        End If
                        SQL.AddParam("@LineNumber", line)
                        SQL.AddParam("@BranchCode", BranchCode)
                        SQL.ExecNonQuery(insertSQL)
                        line += 1
                    End If
                Next
            End If
            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "GR_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("GR_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL, updateSQL As String
                        Dim WHSEfrom, WHSEto, Type As String

                        deleteSQL = " UPDATE  tblGR SET Status ='Cancelled' WHERE GR_No = @GR_No "
                        SQL.FlushParams()
                        GRNo = txtTransNum.Text
                        SQL.AddParam("@GR_No", GRNo)
                        SQL.ExecNonQuery(deleteSQL)

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


                        If cbTransType.SelectedItem = "Material Transfer Confirmation" Then
                            WHSEto = GetWHSE(cbWHto.SelectedItem)
                            WHSEfrom = GetWHSE(cbWHfrom.SelectedItem)
                        Else
                            WHSEto = GetWHSE(cbWHto.SelectedItem)
                        End If


                        'If cbTransType.SelectedItem = "Material Transfer Confirmation" Then
                        '    DELINVTY(ModuleID, "GI", GI_ID)
                        '    DELINVTY(ModuleID, "GR", TransID)
                        'Else
                        '    DELINVTY(ModuleID, "GR", TransID)
                        'End If



                        Dim line As Integer = 1
                        Dim ItemCode, Description, UOM As String
                        Dim QTY, UnitCost As Decimal
                        For Each row As DataGridViewRow In dgvItemMaster.Rows
                            If Not row.Cells(dgcQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                                ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                                Description = IIf(row.Cells(chItemName.Index).Value = Nothing, "", row.Cells(chItemName.Index).Value)
                                UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                                If IsNumeric(row.Cells(dgcQTY.Index).Value) Then QTY = CDec(row.Cells(dgcQTY.Index).Value) Else QTY = 1
                                UnitCost = IIf(row.Cells(chAvgCost.Index).Value = Nothing, "0.00", row.Cells(chAvgCost.Index).Value)
                                line += 1

                                If cbTransType.SelectedItem = "Material Transfer Confirmation" Then
                                    SaveINVTY("IN", ModuleID, "GI", GI_ID, Date.Today, ItemCode, WHSEfrom, QTY, UOM, UnitCost, "Active")
                                    SaveINVTY("OUT", ModuleID, "GR", TransID, Date.Today, ItemCode, WHSEto, QTY, UOM, UnitCost, "Active")
                                Else
                                    SaveINVTY("OUT", ModuleID, "GR", TransID, Date.Today, ItemCode, WHSEto, QTY, UOM, UnitCost, "Active")
                                End If
                            End If
                        Next

                        If cbTransType.SelectedItem = "Material Transfer Confirmation" Then
                            ComputeWAUC("GI", GI_ID)
                            ComputeWAUC("GR", TransID)
                        Else
                            ComputeWAUC("GR", TransID)
                        End If

                        JETransID = LoadJE("GR", TransID)
                        updateSQL = " UPDATE tblJE_Header " & _
                          " SET    Status = @Status,  WhoModified = @WhoModified, DateModified = GETDATE()" & _
                          " WHERE  JE_No = @JE_No "
                        SQL.FlushParams()
                        SQL.AddParam("@JE_No", JETransID)
                        SQL.AddParam("@Status", "Cancelled")
                        SQL.AddParam("@WhoModified", UserID)
                        SQL.ExecNonQuery(updateSQL)

                        Msg("Record cancelled successfully", MsgBoxStyle.Information)
                        LoadGR(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "GR_No", GRNo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If GRNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblGR  WHERE GR_No < '" & GRNo & "' ORDER BY GR_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadGR(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If GRNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblGR  WHERE GR_No > '" & GRNo & "' ORDER BY GR_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadGR(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If GRNo = "" Then
            Cleartext()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadGR(TransID)
            tsbEdit.Enabled = True
            tsbCancel.Enabled = True
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbPrint.Enabled = True
        End If
        tsbSearch.Enabled = True
        tsbUpload.Enabled = False
        tsbDownload.Enabled = False
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
            LoadCurrency()
        End If
    End Sub

    Private Sub LoadCurrency()
        cbCurrency.Items.Clear()
        For Each item In LoadVCECurrency(txtVCECode.Text)
            cbCurrency.Items.Add(item)
        Next
        If cbCurrency.Items.Count = 0 Then
            cbCurrency.Items.Add(BaseCurrency)
        End If
        cbCurrency.SelectedIndex = 0

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
                        f.ShowDialog("SerialItem", itemCode, "ItemCode")
                        ' f.ShowDialog("All Item", itemCode)
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItemDetails(itemCode)
                        Else
                            dgvItemMaster.Rows.RemoveAt(e.RowIndex)
                        End If
                        f.Dispose()
                    End If
                    GenerateEntry()

                Case chBarcode.Index
                    If dgvItemMaster.Item(chBarcode.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemMaster.Item(chBarcode.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("SerialItem", itemCode, "Barcode")
                        ' f.ShowDialog("All Item", itemCode)
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            UOM = f.UOM
                            LoadItemDetails(itemCode, UOM)
                        Else
                            dgvItemMaster.Rows.RemoveAt(e.RowIndex)
                        End If
                        f.Dispose()
                    End If
                    GenerateEntry()
                Case chItemName.Index
                    If dgvItemMaster.Item(chItemName.Index, e.RowIndex).Value <> "" Then
                        itemCode = dgvItemMaster.Item(chItemName.Index, e.RowIndex).Value
                        Dim f As New frmCopyFrom
                        f.ShowDialog("SerialItem", itemCode, "ItemName")
                        ' f.ShowDialog("All Item", itemCode)
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItemDetails(itemCode)
                        Else
                            dgvItemMaster.Rows.RemoveAt(e.RowIndex)
                        End If
                        f.Dispose()
                    End If
                    GenerateEntry()
                Case dgcQTY.Index
                    Recompute(e.RowIndex, e.ColumnIndex)
                    GenerateEntry()
                Case chUOM.Index
                    GenerateEntry()

                Case chDateExpired.Index
                    If IsDate(dgvItemMaster.Item(e.ColumnIndex, e.RowIndex).Value) Then
                        dgvItemMaster.Item(e.ColumnIndex, e.RowIndex).Value = CDate(dgvItemMaster.Item(e.ColumnIndex, e.RowIndex).Value)
                    End If
                    LoadPeriod()

            End Select
        Catch ex1 As InvalidOperationException

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try

    End Sub

    Private Sub LoadPeriod()
        Dim minPeriod, maxPeriod, tempDate As Date
        For Each row As DataGridViewRow In dgvItemMaster.Rows
            If IsDate(row.Cells(chDateExpired.Index).Value) Then
                tempDate = row.Cells(chDateExpired.Index).Value
                If minPeriod = #12:00:00 AM# OrElse tempDate <= minPeriod Then
                    minPeriod = tempDate
                End If
                If maxPeriod = #12:00:00 AM# OrElse tempDate >= maxPeriod Then
                    maxPeriod = tempDate
                End If
            End If
        Next
    End Sub

    Private Sub Recompute(ByVal RowID As Integer, ByVal ColID As Integer)
        Dim gross, VAT, discount, net, baseVAT As Decimal
        If RowID <> -1 Then
            If IsNumeric(dgvItemMaster.Item(chGrossAmount.Index, RowID).Value) Then
                ' GET GROSS AMOUNT (VAT INCLUSIVE)
                gross = CDec(dgvItemMaster.Item(chItemPrice.Index, RowID).Value) * CDec(dgvItemMaster.Item(dgcQTY.Index, RowID).Value)
                baseVAT = gross
                ' COMPUTE VAT IF VATABLE
                If ColID = chVAT.Index Then
                    If dgvItemMaster.Item(chVAT.Index, RowID).Value = True Then
                        dgvItemMaster.Item(chVAT.Index, RowID).Value = False

                        dgvItemMaster.Item(chVATInc.Index, RowID).Value = False
                        VAT = 0
                        dgvItemMaster.Item(chVATInc.Index, RowID).ReadOnly = True
                    Else
                        dgvItemMaster.Item(chVAT.Index, RowID).Value = True

                        dgvItemMaster.Item(chVATInc.Index, RowID).ReadOnly = False
                        If dgvItemMaster.Item(chVATInc.Index, RowID).Value = False Then
                            VAT = Math.Round(CDec(baseVAT * 0.12), 2).ToString("N2")
                        Else
                            baseVAT = Math.Round((gross / 1.12), 2)
                            VAT = Math.Round(CDec(baseVAT * 0.12), 2).ToString("N2")
                        End If
                    End If
                ElseIf ColID = chVATInc.Index Then

                    If dgvItemMaster.Item(chVAT.Index, RowID).Value = False Then
                        VAT = 0
                    Else
                        If dgvItemMaster.Item(chVATInc.Index, RowID).Value = True Then
                            dgvItemMaster.Item(chVATInc.Index, RowID).Value = False
                            VAT = Math.Round(CDec(baseVAT * 0.12), 2).ToString("N2")
                        Else
                            dgvItemMaster.Item(chVATInc.Index, RowID).Value = True
                            'baseVAT = (gross / 1.12)
                            'VAT = CDec(baseVAT * 0.12).ToString("N2")
                            'baseVAT = gross
                            VAT = gross - Math.Round((gross / 1.12), 2)
                            gross = Math.Round((gross / 1.12), 2)
                        End If

                    End If
                Else
                    If dgvItemMaster.Item(chVAT.Index, RowID).Value = False Then
                        VAT = 0
                        dgvItemMaster.Item(chVATInc.Index, RowID).ReadOnly = True
                    Else
                        dgvItemMaster.Item(chVATInc.Index, RowID).ReadOnly = False
                        If dgvItemMaster.Item(chVATInc.Index, RowID).Value = True Then ' IF VAT INCLUSIVE, BASE AMOUNT WILL BE GROSS / 1.12
                            'baseVAT = (gross / 1.12)

                            VAT = gross - Math.Round((gross / 1.12), 2)
                            gross = Math.Round((gross / 1.12), 2)
                        End If
                        VAT = Math.Round(CDec(baseVAT * 0.12), 2).ToString("N2")
                    End If
                End If

                ' COMPUTE DISCOUNT

                If IsNumeric(dgvItemMaster.Item(chDiscountRate.Index, RowID).Value) Then
                    discount = CDec(baseVAT * (CDec(dgvItemMaster.Item(chDiscountRate.Index, RowID).Value) / 100.0)).ToString("N2")
                ElseIf IsNumeric(dgvItemMaster.Item(chDiscountAmount.Index, RowID).Value) Then
                    discount = CDec(dgvItemMaster.Item(chDiscountAmount.Index, RowID).Value)
                Else
                    discount = 0
                End If

                If dgvItemMaster.Item(chVATInc.Index, RowID).Value = False Then

                    net = baseVAT - discount + VAT
                Else
                    net = baseVAT - discount
                End If
                'net = baseVAT - discount + VAT
                dgvItemMaster.Item(chGrossAmount.Index, RowID).Value = Format(gross, "#,###,###,###.00").ToString()
                dgvItemMaster.Item(chDiscountAmount.Index, RowID).Value = Format(discount, "#,###,###,###.00").ToString()
                dgvItemMaster.Item(chVATAmount.Index, RowID).Value = Format(VAT, "#,###,###,###.00").ToString()
                dgvItemMaster.Item(chNetAmount.Index, RowID).Value = Format(net, "#,###,###,###.00").ToString()

            End If
        End If

    End Sub

    Private Sub GenerateEntry()
        Dim creditaccnt As String
        If cbTransType.SelectedIndex <> -1 Then
            creditaccnt = GetGRTypeAccntCode(cbTransType.SelectedItem)
            lvAccount.Items.Clear()
            For Each row As DataGridViewRow In dgvItemMaster.Rows
                If IsNumeric(row.Cells(chAvgCost.Index).Value) AndAlso Not IsNothing(row.Cells(chAccntCode.Index).Value) Then
                    If cbTransType.SelectedItem = "Material Transfer Confirmation" Then
                        'DEBIT
                        lvAccount.Items.Add(row.Cells(chAccntCode.Index).Value)
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(row.Cells(chAccntCode.Index).Value))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chAvgCost.Index).Value * row.Cells(dgcQTY.Index).Value).ToString("N2"))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("GR:" & txtTransNum.Text)


                        ' CREDIT 
                        lvAccount.Items.Add(row.Cells(chAccntCode.Index).Value)
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(row.Cells(chAccntCode.Index).Value))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chAvgCost.Index).Value * row.Cells(dgcQTY.Index).Value).ToString("N2"))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("GR:" & txtTransNum.Text)
                    ElseIf cbTransType.SelectedItem = "Sales Return" Then
                        Dim query As String
                        Dim AvgCost As Decimal = 0
                        If row.Cells(chItemCode.Index).Value <> Nothing AndAlso row.Cells(chNetAmount.Index).Value > 0 Then
                            If Inv_ComputationMethod = "SC" Then
                                AvgCost = GetStandardCost(row.Cells(chItemCode.Index).Value)
                            Else
                                AvgCost = GetAverageCost(row.Cells(chItemCode.Index).Value)
                            End If
                            query = " SELECT AD_COS, AccountTitle " & _
                                    " FROM   tblItem_Master INNER JOIN tblCOA_Master " & _
                                    " ON     tblItem_Master.AD_COS = tblCOA_Master.AccountCode " & _
                                    " WHERE  ItemCode ='" & row.Cells(chItemCode.Index).Value & "' "
                            SQL.ReadQuery(query)
                            If SQL.SQLDR.Read() Then
                                lvAccount.Items.Add(SQL.SQLDR("AD_COS").ToString)
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(SQL.SQLDR("AD_COS").ToString))
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chAvgCost.Index).Value * row.Cells(dgcQTY.Index).Value).ToString("N2"))
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("GR:" & txtTransNum.Text)
                            End If
                        End If

                        AvgCost = 0
                        If row.Cells(chItemCode.Index).Value <> Nothing AndAlso row.Cells(chNetAmount.Index).Value > 0 Then
                            If Inv_ComputationMethod = "SC" Then
                                AvgCost = GetStandardCost(row.Cells(chItemCode.Index).Value)
                            Else
                                AvgCost = GetAverageCost(row.Cells(chItemCode.Index).Value)
                            End If
                            query = " SELECT AD_Inv, AccountTitle " & _
                                    " FROM   tblItem_Master INNER JOIN tblCOA_Master " & _
                                    " ON     tblItem_Master.AD_Inv = tblCOA_Master.AccountCode " & _
                                    " WHERE  ItemCode ='" & row.Cells(chItemCode.Index).Value & "' "
                            SQL.ReadQuery(query)
                            If SQL.SQLDR.Read() Then
                                lvAccount.Items.Add(SQL.SQLDR("AD_Inv").ToString)
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(SQL.SQLDR("AD_Inv").ToString))
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chAvgCost.Index).Value * row.Cells(dgcQTY.Index).Value).ToString("N2"))
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("GR:" & txtTransNum.Text)
                            End If
                        End If


                        'CREDIT
                        lvAccount.Items.Add(creditaccnt)
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(creditaccnt))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chGrossAmount.Index).Value).ToString("N2"))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("GR:" & txtTransNum.Text)


                        'VAT Amount
                        If row.Cells(chGrossAmount.Index).Value <> row.Cells(chNetAmount.Index).Value Then
                            lvAccount.Items.Add(accntVAT)
                            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(accntVAT))
                            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chVATAmount.Index).Value).ToString("N2"))
                            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        End If


                        'Discount Account
                        If row.Cells(chItemCode.Index).Value <> Nothing AndAlso row.Cells(chDiscountAmount.Index).Value > 0 Then
                            query = " SELECT AD_Discount, AccountTitle " & _
                                    " FROM   tblItem_Master INNER JOIN tblCOA_Master " & _
                                    " ON     tblItem_Master.AD_Discount = tblCOA_Master.AccountCode " & _
                                    " WHERE  ItemCode ='" & row.Cells(chItemCode.Index).Value & "' "
                            SQL.ReadQuery(query)
                            If SQL.SQLDR.Read() Then
                                lvAccount.Items.Add(SQL.SQLDR("AD_Discount").ToString)
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(SQL.SQLDR("AD_Discount").ToString))
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chDiscountAmount.Index).Value).ToString("N2"))
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                            End If
                        End If

                        'AR Account
                        lvAccount.Items.Add(row.Cells(chAccntCode.Index).Value)
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(row.Cells(chAccntCode.Index).Value))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chNetAmount.Index).Value).ToString("N2"))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("GR:" & txtTransNum.Text)

                    Else
                        lvAccount.Items.Add(row.Cells(chAccntCode.Index).Value)
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(row.Cells(chAccntCode.Index).Value))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chAvgCost.Index).Value * row.Cells(dgcQTY.Index).Value).ToString("N2"))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("GR:" & txtTransNum.Text)

                        'CREDIT
                        lvAccount.Items.Add(creditaccnt)
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(creditaccnt))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chAvgCost.Index).Value * row.Cells(dgcQTY.Index).Value).ToString("N2"))
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("GR:" & txtTransNum.Text)

                    End If
                End If
            Next

            TotalDBCR()
        Else
            Msg("Please select Transaction Type!", MsgBoxStyle.Information)
        End If

    End Sub

    Private Sub TotalDBCR()
        Try
            'debit compute & print in textbox
            Dim a As Double = 0
            For i As Integer = 0 To lvAccount.Items.Count - 1
                If (lvAccount.Items(i).SubItems(chDebit.Index).Text) <> "" Then
                    a = a + Double.Parse((lvAccount.Items(i).SubItems(chDebit.Index).Text))
                End If
            Next
            txtTotalDebit.Text = a.ToString("N2")
            'credit compute & print in textbox
            Dim b As Double = 0
            For i As Integer = 0 To lvAccount.Items.Count - 1
                If (lvAccount.Items(i).SubItems(chCredit.Index).Text) <> "" Then
                    b = b + Double.Parse((lvAccount.Items(i).SubItems(chCredit.Index).Text))
                End If
            Next
            txtTotalCredit.Text = b.ToString("N2")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function GetGRTypeAccntCode(ByVal Type As String) As String
        Dim query As String
        query = " SELECT DefaultAccount FROM tblGR_Type WHERE Description = @Type AND Status = 'Active' "
        SQL.FlushParams()
        SQL.AddParam("@Type", Type)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("DefaultAccount").ToString
        Else
            Return ""
        End If
    End Function

    Private Sub LoadItemDetails(ByVal ID As String, Optional UOM As String = "")
        Dim query, ItemCode As String
        Dim unitcost As Decimal
        'query = " SELECT tblItem_Master.ItemCode, ItemName, 1 , ItemUOM, AD_Inv, ISNULL(AverageCost,ID_SC) as AverageCost" & _
        '        " FROM tblItem_Master " & _
        '        "  LEFT JOIN " & _
        '        " ( SELECT TOP 1  ItemCode, AverageCost  " & _
        '        " FROM tblInventory  " & _
        '        " WHERE tblInventory.ItemCode = @ItemCode " & _
        '        " ORDER BY  PostDate DESC, DateCreated DESC ) AS AvgCost " & _
        '        " ON AvgCost.ItemCode = tblItem_Master.ItemCode " & _
        '        " WHERE tblItem_Master.ItemCode = @ItemCode"

        query = " SELECT  ItemCode,  ItemName, UOM AS ItemUOM,  " & _
                 "         ISNULL(ID_SC,0) AS ID_SC, WHSE AS  ID_Warehouse,  AD_Inv, DateExpired, LotNo, SerialNo, EB, Size, Color " & _
                 " FROM    viewItem_StockSerial " & _
                 " WHERE   TransID = @TransID "
        SQL.FlushParams()
        SQL.AddParam("@TransID", ID)
        SQL.ReadQuery(query)
        'SQL.FlushParams()
        'SQL.AddParam("@ItemCode", IIf(ID = Nothing, "", ID))
        'SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            If dgvItemMaster.SelectedCells.Count > 0 Then
                ItemCode = SQL.SQLDR("ItemCode").ToString
                If Inv_ComputationMethod = "SC" Then
                    unitcost = GetStandardCost(ItemCode)
                Else
                    unitcost = GetAverageCost(ItemCode)
                End If

                If UOM = "" Then
                    UOM = SQL.SQLDR("ItemUOM").ToString
                End If
                dgvItemMaster.Item(chItemCode.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemCode").ToString
                dgvItemMaster.Item(chItemName.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemName").ToString
                dgvItemMaster.Item(chUOM.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = UOM
                dgvItemMaster.Item(4, dgvItemMaster.SelectedCells(0).RowIndex).Value = 1
                dgvItemMaster.Item(5, dgvItemMaster.SelectedCells(0).RowIndex).Value = IIf(cbWHfrom.SelectedIndex = -1, "", cbWHfrom.SelectedItem)
                dgvItemMaster.Item(chAccntCode.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("AD_Inv").ToString
                dgvItemMaster.Item(7, dgvItemMaster.SelectedCells(0).RowIndex).Value = CDec(unitcost).ToString("N2")
                dgvItemMaster.Item(chDateExpired.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("DateExpired").ToString
                dgvItemMaster.Item(chLotNo.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("LotNo").ToString
                dgvItemMaster.Item(chSerialNo.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("SerialNo").ToString
                dgvItemMaster.Item(chSize.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("Size").ToString
                dgvItemMaster.Item(chColor.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("Color").ToString
                LoadWHSE(dgvItemMaster.SelectedCells(0).RowIndex)
                LoadUOM(ItemCode, dgvItemMaster.SelectedCells(0).RowIndex)
                LoadColor(ItemCode, dgvItemMaster.SelectedCells(0).RowIndex)
                LoadSize(ItemCode, dgvItemMaster.SelectedCells(0).RowIndex)
            End If
            LoadBarCode()
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
                LoadBarCode()
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
                query = " SELECT Barcode FROM tblItem_Barcode WHERE UOM = @UOM AND ItemCode= @ItemCode  AND STATUS <> 'Inactive'"
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

    Private Sub btnSearchVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchVCE.Click
        Dim f As New frmVCE_Search
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        txtVCEName.Text = f.VCEName
        LoadCurrency()
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
        If cbReceiver.SelectedIndex = -1 Then
            Msg("Please Select receiver!", MsgBoxStyle.Exclamation)
        ElseIf cbReceiver.SelectedItem = "Production" Then
            f.ShowDialog("GI-GR-PWHSE")
        Else
            f.ShowDialog("GI-GR-WHSE")
        End If
        cbTransType.SelectedItem = "Material Transfer Confirmation"
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        txtConversion.Text = ""
        cbCurrency.Items.Clear()
        LoadGI(f.transID)
        f.Dispose()
    End Sub

    Private Sub LoadGI(ByVal ID As String)
        Try
            Dim WHSEfrom, WHSEto As String
            Dim query As String
            query = " SELECT TransID, GI_No, IssueFrom, IssueTo, WHSE_From, WHSE_To, DateGI, Remarks " & _
                    " FROM   tblGI " & _
                    " WHERE  TransID ='" & ID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                GI_ID = SQL.SQLDR("TransID")
                txtMRRef.Text = SQL.SQLDR("GI_No").ToString
                WHSEfrom = SQL.SQLDR("WHSE_From").ToString
                WHSEto = SQL.SQLDR("WHSE_To").ToString
                disableEvent = True
                cbReceiver.SelectedItem = SQL.SQLDR("IssueTo").ToString
                cbReceivedFrom.SelectedItem = SQL.SQLDR("IssueFrom").ToString
                disableEvent = False
                loadWHFrom()
                loadWH()


                If cbReceiver.SelectedItem = "Production" Then
                    cbWHto.SelectedItem = loadWH("From", "Production", WHSEto)
                Else
                    cbWHto.SelectedItem = loadWH("From", "Warehouse", WHSEto)
                End If
                If cbReceivedFrom.SelectedItem = "Production" Then
                    cbWHfrom.SelectedItem = loadWH("To", "Production", WHSEfrom)
                Else
                    cbWHfrom.SelectedItem = loadWH("To", "Warehouse", WHSEfrom)
                End If

                query = "  SELECT tblGI_Details.ItemCode, Description, UOM, IssueQTY, WHSE  , AD_Inv, ISNULL(AverageCost,ID_SC) as AverageCost, " & _
                        "   SerialNo, LotNo, DateExpired, tblGI_Details.Size,	tblGI_Details.Color" & _
                        "  FROM   tblGI_Details   " & _
                        "  INNER JOIN " & _
                        "  tblItem_Master ON " & _
                        "  tblItem_Master.ItemCode = tblGI_Details.ItemCode " & _
                        "   LEFT JOIN  " & _
                        "    ( SELECT    TOP 1  ItemCode, AverageCost   " & _
                        "     FROM      tblInventory   " & _
                        "     ORDER BY  PostDate DESC, DateCreated DESC ) AS AvgCost  " & _
                        " ON AvgCost.ItemCode = tblGI_Details.ItemCode  " & _
                        " WHERE  tblGI_Details.TransID ='" & ID & "' "
                SQL.GetQuery(query)
                dgvItemMaster.Rows.Clear()
                Dim ctr As Integer = 0
                Dim ctrGroup As Integer = 0
                If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                        dgvItemMaster.Rows.Add(row(0).ToString, "", row(1).ToString, row(2).ToString, _
                                      CDec(row(3)).ToString("N2"), GetWHSEDesc(row(4).ToString), row(5).ToString, CDec(row(6)).ToString("N2"), _
                                       "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", False, False, row(9), row(8).ToString, row(7).ToString, _
                                       row(10).ToString, row(11).ToString)
                        LoadWHSE(ctr)
                        LoadUOM(row(0).ToString, ctr)
                        LoadColor(row(0).ToString, ctr)
                        LoadSize(row(0).ToString, ctr)
                        ctr += 1
                    Next
                    LoadBarCode()
                    GenerateEntry()
                End If
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadSI(ByVal ID As String)
        Try
            Dim query As String

            query = "  SELECT tblSI.TransID, tblSI_Details.ItemCode,  tblSI_Details.Description, tblSI_Details.UOM, tblSI_Details.QTY,  " & _
                    " ISNULL(tblDR_Details.WHSE,'') AS WHSE, tblSI.DebitAccnt, ISNULL(tblDR_Details.UnitPrice,0) AS UnitCost ,  " & _
                    "  tblSI_Details.UnitPrice AS ItemPrice,  tblSI_Details.GrossAmount,  " & _
                    "  CAST(CASE WHEN tblSI_Details.Discount <> 0 THEN " & _
                    "  ( tblSI_Details.Discount/ tblSI_Details.GrossAmount) * 100  " & _
                    "  ELSE 0 END AS decimal(18,4)) AS DiscountRate, " & _
                    "  tblSI_Details.Discount, tblSI_Details.VATAmount, tblSI_Details.NetAmount, tblSI_Details.VATable, tblSI_Details.VatInc, tblSI.SI_No, tblSI_Details.Size, tblSI_Details.Color " & _
                    "  FROM tblSI_Details   " & _
                    "  INNER JOIN tblSI ON  tblSI.TransID = tblSI_Details.TransID   " & _
                    "  LEFT JOIN tblDR ON  tblDR.TransID = tblSI.DR_Ref   " & _
                    "  INNER JOIN tblDR_Details ON  tblDR_Details.TransID = tblDR.TransID  " & _
                    "  AND tblDR_Details.ItemCode = tblSI_Details.ItemCode   " & _
                    " WHERE  tblSI.TransID ='" & ID & "' "
            SQL.GetQuery(query)
            dgvItemMaster.Rows.Clear()
            Dim ctr As Integer = 0
            Dim ctrGroup As Integer = 0
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                    txtRemarks.Text = "Sales Return SI: " & row(16)
                    dgvItemMaster.Rows.Add(row(1).ToString, "", row(2).ToString, row(3).ToString, _
                                  CDec(row(4)).ToString("N2"), GetWHSEDesc(row(5).ToString), row(6).ToString, CDec(row(7)).ToString("N2"), _
                                  CDec(row(8)).ToString("N2"), CDec(row(9)).ToString("N2"), CDec(row(10)).ToString("N2"), CDec(row(11)).ToString("N2"), _
                                  CDec(row(12)).ToString("N2"), CDec(row(13)).ToString("N2"), row(14), row(15), row(17), row(18))
                    LoadWHSE(ctr)
                    LoadUOM(row(1).ToString, ctr)
                    LoadColor(row(1).ToString, ctr)
                    LoadSize(row(1).ToString, ctr)
                    ctr += 1
                Next
                LoadBarCode()
                GenerateEntry()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadColor(ItemCode As String, ByVal SelectedIndex As Integer)
        Try
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemMaster.Item(chColor.Index, SelectedIndex)
            dgvCB.Items.Clear()
            ' ADD ALL WHSEc
            Dim query As String

            query = " SELECT DISTINCT Color FROM tblitem_SIzecolor " & _
                    " WHERE ItemCode ='" & ItemCode & "' "
            SQL.ReadQuery(query)
            dgvCB.Items.Clear()
            While SQL.SQLDR.Read
                dgvCB.Items.Add(SQL.SQLDR("Color").ToString)
            End While
            dgvCB.Items.Add("")
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub LoadSize(ItemCode As String, ByVal SelectedIndex As Integer)
        Try
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemMaster.Item(chSize.Index, SelectedIndex)
            dgvCB.Items.Clear()
            ' ADD ALL WHSEc
            Dim query As String

            query = " SELECT DISTINCT Size FROM tblitem_SIzecolor " & _
                    " WHERE ItemCode ='" & ItemCode & "' "
            SQL.ReadQuery(query)
            dgvCB.Items.Clear()
            While SQL.SQLDR.Read
                dgvCB.Items.Add(SQL.SQLDR("Size").ToString)
            End While
            dgvCB.Items.Add("")
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("GR", TransID)
        f.Dispose()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem1.Click
        frmGR_Type.Show()
    End Sub

    Private Sub cbTransType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbTransType.SelectedIndexChanged
        RefreshTranType()
    End Sub

    Private Sub RefreshTranType()
        cbReceiver.Enabled = False
        cbWHto.Enabled = False
        cbReceivedFrom.Visible = False
        txtMRRef.Visible = False
        cbReceiver.Enabled = False
        cbWHfrom.Enabled = False

        txtVCECode.Visible = False
        Label14.Visible = False
        Label4.Visible = False
        txtVCEName.Visible = False
        btnSearchVCE.Visible = False
        If cbTransType.SelectedItem = "Material Transfer Confirmation" Then
            cbReceiver.Enabled = True
            cbReceiver.SelectedItem = "Warehouse"
            cbWHto.Enabled = True
            txtMRRef.Visible = True
            cbWHfrom.Visible = True
            cbWHfrom.Enabled = True
            Label15.Visible = True
            Label6.Visible = True
            cbReceivedFrom.Visible = True
            txtVCECode.Visible = False
            Label14.Visible = False
            Label4.Visible = False
            txtVCEName.Visible = False
            btnSearchVCE.Visible = False
            cbCurrency.Visible = False
            Label24.Visible = False
        Else
            cbReceiver.Enabled = True
            cbReceiver.SelectedItem = "Warehouse"
            cbWHto.Enabled = True
            cbReceiver.Enabled = True
            cbWHfrom.Visible = False
            cbReceivedFrom.Visible = False
            txtMRRef.Visible = False
            txtVCECode.Visible = True
            Label6.Visible = False
            Label15.Visible = False
            Label14.Visible = True
            Label4.Visible = True
            txtVCEName.Visible = True
            btnSearchVCE.Visible = True
            cbCurrency.Visible = True
            Label24.Visible = True
        End If
    End Sub

    Private Sub dgvItemMaster_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemMaster.CellContentClick

    End Sub

    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged

    End Sub

    Private Sub cbCurrency_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbCurrency.SelectedIndexChanged
        If disableEvent = False Then
            If cbCurrency.SelectedItem <> BaseCurrency Then
                lblConversion.Visible = True
                txtConversion.Visible = True
                MsgBox("Please input " & cbCurrency.SelectedItem & " amount in Debit and Credit.", MsgBoxStyle.Exclamation)
            Else
                lblConversion.Visible = False
                txtConversion.Visible = False
            End If
        End If
    End Sub

    Private Sub FromSIToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromSIToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        If cbReceiver.SelectedIndex = -1 Then
            Msg("Please Select receiver!", MsgBoxStyle.Exclamation)
        Else
            f.ShowDialog("SI")
        End If
        cbTransType.SelectedItem = "Sales Return"
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        txtConversion.Text = ""
        cbCurrency.Items.Clear()
        LoadSI(f.transID)
        f.Dispose()
    End Sub

    Private Sub dgvItemMaster_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dgvItemMaster.CurrentCellDirtyStateChanged
        If dgvItemMaster.SelectedCells.Count > 0 AndAlso (dgvItemMaster.SelectedCells(0).ColumnIndex = chVAT.Index OrElse dgvItemMaster.SelectedCells(0).ColumnIndex = chVATInc.Index) Then
            If dgvItemMaster.SelectedCells(0).RowIndex <> -1 Then
                Recompute(dgvItemMaster.SelectedCells(0).RowIndex, dgvItemMaster.SelectedCells(0).ColumnIndex)
                dgvItemMaster.SelectedCells(0).Selected = False
                dgvItemMaster.EndEdit()
                GenerateEntry()
            End If
        End If
    End Sub

    Private Sub dgvItemMaster_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgvItemMaster.CellValidating
        If e.ColumnIndex = chDateExpired.Index Then
            Dim dt As DateTime
            If e.FormattedValue.ToString <> String.Empty AndAlso Not DateTime.TryParse(e.FormattedValue.ToString, dt) Then
                MessageBox.Show("Enter correct Date")
                e.Cancel = True
            End If

        End If
    End Sub

    Private Sub FromInventoryCountToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromInventoryCountToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        If cbReceiver.SelectedIndex = -1 Then
            Msg("Please Select receiver!", MsgBoxStyle.Exclamation)
        Else
            f.ShowDialog("IC-GR")
        End If
        cbTransType.SelectedItem = "Inventory Count"
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        txtConversion.Text = ""
        cbCurrency.Items.Clear()
        LoadIC(f.transID)
        f.Dispose()
    End Sub


    Private Sub LoadIC(ByVal ID As String)
        Try
            Dim query, WHSETo As String

            query = " SELECT TransID, viewIC_GR_Unserved.ItemCode, ItemName, viewIC_GR_Unserved.UOM, " & _
                    " viewIC_GR_Unserved.IC_QTY, viewIC_GR_Unserved.WHSE, viewIC_GR_Unserved.IC_No " & _
                    "  FROM viewIC_GR_Unserved " & _
                    " LEFT JOIN " & _
                    " tblItem_Master ON " & _
                    " tblItem_Master.ItemCode = viewIC_GR_Unserved.ItemCode " & _
                    " WHERE  viewIC_GR_Unserved.TransID ='" & ID & "' "
            SQL.GetQuery(query)
            dgvItemMaster.Rows.Clear()
            Dim ctr As Integer = 0
            Dim unitcost As Decimal
            Dim ctrGroup As Integer = 0
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                    IC_ID = row(0)
                    txtICRef.Text = row(6).ToString
                    WHSETo = row(5).ToString
                    cbWHto.SelectedItem = loadWH("From", "Warehouse", WHSETo)
                    If Inv_ComputationMethod = "SC" Then
                        unitcost = GetStandardCost(row(1))
                    Else
                        unitcost = GetAverageCost(row(1))
                    End If
                    txtRemarks.Text = "Inventory Count IC: " & row(6)
                    dgvItemMaster.Rows.Add(row(1).ToString, "", row(2).ToString, row(3).ToString, _
                                  CDec(row(4)).ToString("N2"), GetWHSEDesc(row(5).ToString), "", CDec(unitcost).ToString("N2"), _
                                  CDec(0).ToString("N2"), CDec(0).ToString("N2"), CDec(0).ToString("N2"), CDec(0).ToString("N2"), _
                                  CDec(0).ToString("N2"), CDec(0).ToString("N2"), False, False)
                    LoadWHSE(ctr)
                    LoadUOM(row(1).ToString, ctr)
                    LoadColor(row(1).ToString, ctr)
                    LoadSize(row(1).ToString, ctr)
                    ctr += 1
                Next
                LoadBarCode()
                'GenerateEntry()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub bgwUpload_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUpload.DoWork
        Dim startIndex As Integer
        Dim excelRangeCount As Integer
        Dim rowCount As Integer
        Dim ObjectText As String = ""
        Dim ObjectText1 As String = ""
        Dim ObjectText2 As String = ""
        Dim intAmount As Decimal = 0
        Dim prinAmount As Decimal = 0
        Dim AccountID As Integer = 0
        Dim VCECode As String = ""

        ' For Addditional Interest field when autobreakdown is checked.
        Dim addlCol As Integer = 0
        ' OPENING EXCEL FILE VARIABLES
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim Obj As Object
        Dim Obj1 As Object
        Dim Obj2 As Object
        Dim sheetName As String
        Dim range As Excel.Range

        ' OPEN EXCEL FILE
        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Open(path)
        sheetName = xlWorkBook.Worksheets(1).Name.ToString
        xlWorkSheet = xlWorkBook.Worksheets(sheetName)
        range = xlWorkSheet.UsedRange

        Valid = True
        InvalidTemplate = False
        Dim summary As Boolean = False
        Dim rowSumCount As Integer = 0
        Dim report As String = ""

        startIndex = 4

        SetPGBmax(range.Rows.Count)  ' Set Progress bar max value.
        excelRangeCount = range.Rows.Count
        Dim maxCol As Integer = range.Columns.Count - 1
        ' Loop through rows of excel.
        For i As Integer = startIndex To range.Rows.Count
            If bgwUpload.CancellationPending Then
                e.Cancel = True
                Exit For
            End If
            ' Reset AddCol.
            addlCol = 0

            ' Loop through columns of excel.
            For j As Integer = 0 To maxCol
                Obj = CType(range.Cells(i, j + 1), Excel.Range)

                ' Exit loop if first row and first column is not equal to template name.
                If i = 1 AndAlso j = 0 Then
                    If (Obj.value Is Nothing OrElse Obj.value.ToString <> templateName) Then

                        Dim test As String = Obj.value
                        bgwUpload.CancelAsync()
                        InvalidTemplate = True

                        If bgwUpload.CancellationPending Then
                            e.Cancel = True
                        End If
                    End If
                    Exit For


                ElseIf i > 1 Then
                    ' Update cell value, exit loop after updating the last column of a row
                    If dgvItemMaster.Columns.Count > j + addlCol Then

                        If j = chItemCode.Index Then
                            ' On the first column of the current row, exit loop when Code, Ref and Name is blank, should have atleast one record. 
                            Obj1 = CType(range.Cells(i, j + 2), Excel.Range)
                            Obj2 = CType(range.Cells(i, j + 3), Excel.Range)
                            If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                            If IsNothing(Obj1.value) Then ObjectText1 = "" Else ObjectText1 = Obj1.value.ToString
                            If IsNothing(Obj2.value) Then ObjectText2 = "" Else ObjectText2 = Obj2.value.ToString
                            If ObjectText = "" AndAlso ObjectText1 = "" AndAlso ObjectText2 = "" Then
                                Exit For
                            End If

                            ' Add Row to Datagridview on the first column loop.
                            AddRow()
                            rowCount += 1

                            ' ItemCode No
                            If IsNothing(dgvItemMaster.Item(j, rowCount - 1).Value) OrElse dgvItemMaster.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                                LoadUOM(ObjectText, rowCount - 1)
                            End If
                        ElseIf j = chItemName.Index Then
                            ' Check if has valid ItemName
                            If ObjectText <> "" Then
                                If Not validateItem(ObjectText) Then
                                    ' if not exist, change color.
                                    ChangeCellColor(rowCount - 1, j)
                                    Valid = False
                                Else
                                    ' if existing  AccountCode
                                    dgvItemMaster.Item(j, rowCount - 1).Value = GetItemName(ObjectText)
                                End If
                            End If
                        ElseIf j = chUOM.Index Then
                            ' UOM
                            If IsNothing(dgvItemMaster.Item(j, rowCount - 1).Value) OrElse dgvItemMaster.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = dgcQTY.Index Then
                            ' QTY
                            If IsNothing(dgvItemMaster.Item(j, rowCount - 1).Value) OrElse dgvItemMaster.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chDateExpired.Index Then
                            If IsNothing(dgvItemMaster.Item(j, rowCount - 1).Value) OrElse dgvItemMaster.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chLotNo.Index Then
                            ' LotNo
                            If IsNothing(dgvItemMaster.Item(j, rowCount - 1).Value) OrElse dgvItemMaster.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chSerialNo.Index Then
                            ' Serial
                            If IsNothing(dgvItemMaster.Item(j, rowCount - 1).Value) OrElse dgvItemMaster.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        End If
                    Else
                        Exit For
                    End If
                End If
            Next
            ' Reset AddCol.
            addlCol = 0
            bgwUpload.ReportProgress(i)
        Next


        NAR(Obj)
        NAR(range)
        NAR(xlWorkSheet)
        xlWorkBook.Close(False)
        NAR(xlWorkBook)
        xlApp.Quit()
        NAR(xlApp)
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Private Sub bgwUpload_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwUpload.ProgressChanged
        pgbCounter.Value = e.ProgressPercentage
    End Sub

    Private Sub bgwUpload_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwUpload.RunWorkerCompleted
        pgbCounter.Visible = False
        If InvalidTemplate Then
            MsgBox("Invalid Template, Please select a valid File!", MsgBoxStyle.Exclamation)
        Else
            TotalDBCR()
            If Valid Then
                If dgvItemMaster.Rows.Count > 1 Then
                    MsgBox(dgvItemMaster.Rows.Count - 1 & " File Data Uploaded Successfully!", vbInformation)
                Else
                    MsgBox(dgvItemMaster.Rows.Count & " File Data Uploaded Successfully!", vbInformation)
                End If

            Else
                MsgBox("Some data are invalid !, Please Check highlighted cells.", MsgBoxStyle.Exclamation)
            End If
            If dgvItemMaster.Rows.Count > 1 Then
                dgvItemMaster.Rows(dgvItemMaster.Rows.Count - 1).ReadOnly = True
            End If
        End If
        tsbUpload.Text = "Upload"
    End Sub
    Private Delegate Sub AddValueInvoker(ByVal Value As String, ByVal row As Integer, ByVal col As Integer)
    Private Delegate Sub SetPGBmaxInvoker(ByVal Value As String)
    Private Delegate Sub ChangeCellColorInvoker(ByVal row As Integer, ByVal col As Integer)

    Public Function validateItem(ByVal ItemCode As String) As Boolean
        Try
            Dim query As String
            query = " SELECT    * " &
                    " FROM      tblItem_Master " &
                    " WHERE     ItemCode = @ItemCode "
            SQL.FlushParams()
            SQL.AddParam("@ItemCode", ItemCode)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SQL.FlushParams()
        End Try

    End Function

    Private Sub NAR(ByRef o As Object)
        Try
            While (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0)
            End While
        Catch
        Finally
            o = Nothing
        End Try
    End Sub
    Private Sub AddRow()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf AddRow))
        Else
            dgvItemMaster.Rows.Add("")
        End If
    End Sub
    Private Sub AddValue(ByVal Value As String, ByVal row As Integer, ByVal col As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New AddValueInvoker(AddressOf AddValue), Value, row, col)
        Else
            dgvItemMaster.Item(col, row).Value = Value
        End If
    End Sub
    Private Sub SetPGBmax(ByVal Value As String)
        If Me.InvokeRequired Then
            Me.Invoke(New SetPGBmaxInvoker(AddressOf SetPGBmax), Value)
        Else
            pgbCounter.Maximum = Value
            pgbCounter.Value = 0
        End If
    End Sub

    Private Sub ChangeCellColor(ByVal row As Integer, ByVal col As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New ChangeCellColorInvoker(AddressOf ChangeCellColor), row, col)
        Else
            dgvItemMaster.Rows(row).Cells(col).Style.BackColor = Color.Yellow
        End If
    End Sub
    Public Function validateVCE(ByVal VCEName As String) As Boolean
        Try
            Dim query As String
            query = " SELECT    * " &
                    " FROM      viewVCE_Master " &
                    " WHERE     VCECode = @VCECode "
            SQL.FlushParams()
            SQL.AddParam("@VCECode", VCEName)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SQL.FlushParams()
        End Try

    End Function
    Public Function validateAccountCode(ByVal ItemCode As String) As Boolean
        Try
            Dim query As String
            query = " SELECT    * " &
                    " FROM      tblItem_Master " &
                    " WHERE     ItemCode = @ItemCode "
            SQL.FlushParams()
            SQL.AddParam("@ItemCode", ItemCode)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SQL.FlushParams()
        End Try

    End Function

    Private Sub tsbDownload_Click(sender As System.Object, e As System.EventArgs) Handles tsbDownload.Click
        Dim fileName As String = "GR Uploader.xlsx"
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        xlApp = New Excel.Application
        Dim App_Path As String

        App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName & "\Templates"
        If My.Computer.FileSystem.FileExists(App_Path + "\" & templateName & ".xlsx") Then
            xlWorkBook = xlApp.Workbooks.Open(App_Path + "\" & templateName & ".xlsx")
            xlWorkSheet = xlWorkBook.Worksheets("Template")
            xlWorkSheet.Unprotect(excelPW)
            For i As Integer = 0 To 16
                If i = 0 Then
                    xlWorkSheet.Cells(1, i + 1) = templateName
                End If
                xlWorkSheet.Cells(2, i + 1) = dgvItemMaster.Columns(i).Name
                xlWorkSheet.Cells(3, i + 1) = dgvItemMaster.Columns(i).HeaderText
            Next
            xlWorkSheet.Protect(excelPW)
            Dim ctr As Integer = 1
            Do
                fileName = "GR Uploader -" & ctr.ToString & ".xlsx"
                If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & fileName) = False Then
                    Exit Do
                End If
                ctr += 1
            Loop

            xlWorkBook.SaveAs(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & fileName)
            xlWorkBook.Close(Type.Missing, Type.Missing, Type.Missing)
            xlApp.Quit()

            ' CLEAN UP. (CLOSE INSTANCES OF EXCEL OBJECTS.)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet) : xlWorkSheet = Nothing
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook) : xlWorkBook = Nothing
        Else
            MsgBox("No Template found!" & vbNewLine & "Please contact your systems administrator", MsgBoxStyle.Exclamation)
        End If

        ' CLEAN UP. (CLOSE INSTANCES OF EXCEL OBJECTS.)
        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp) : xlApp = Nothing
        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & fileName) Then
            Dim xls As Excel.Application
            Dim workbook As Excel.Workbook
            xls = New Excel.Application
            xls.Visible = True
            workbook = xls.Workbooks.Open(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & fileName)
        End If
    End Sub

    Private Sub tsbUpload_Click(sender As System.Object, e As System.EventArgs) Handles tsbUpload.Click
        If tsbUpload.Text = "Upload" Then
            With (OpenFileDialog1)
                .InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                .Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx"
                .FilterIndex = 2
            End With
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                If MessageBox.Show("Uploading " & vbNewLine & "Are you sure you want to Contiue?", "Message Alert", MessageBoxButtons.YesNo) = MsgBoxResult.Yes Then
                    path = OpenFileDialog1.FileName
                    dgvItemMaster.Rows.Clear()
                    'dgvItemMaster.ReadOnly = True
                    pgbCounter.Visible = True
                    bgwUpload.RunWorkerAsync()
                    tsbUpload.Text = "Stop"
                End If
            End If
        Else
            If (bgwUpload.IsBusy = True) Then
                tsbUpload.Text = "Upload"
                pgbCounter.Value = 0
                bgwUpload.CancelAsync()
            End If
        End If
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

    Private Sub FromMRISToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromMRISToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Cancelled"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        If cbReceiver.SelectedIndex = -1 Then
            Msg("Please Select receiver!", MsgBoxStyle.Exclamation)
        Else
            f.ShowDialog("MRIS-GR")
        End If
        cbTransType.SelectedItem = "Material Issuance"
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        txtConversion.Text = ""
        cbCurrency.Items.Clear()
        LoadMRIS(f.transID)
        f.Dispose()
    End Sub

    Private Sub LoadMRIS(ByVal ID As String)
        Try
            Dim query, WHSETo As String

            query = " SELECT TransID, viewMRIS_GR_Unserved.ItemCode, ItemName, viewMRIS_GR_Unserved.UOM, " & _
                    " viewMRIS_GR_Unserved.MRIS_QTY, viewMRIS_GR_Unserved.WHSE_From, viewMRIS_GR_Unserved.MRIS_No " & _
                    "  FROM viewMRIS_GR_Unserved " & _
                    " LEFT JOIN " & _
                    " tblItem_Master ON " & _
                    " tblItem_Master.ItemCode = viewMRIS_GR_Unserved.ItemCode " & _
                    " WHERE  viewMRIS_GR_Unserved.TransID ='" & ID & "' "
            SQL.GetQuery(query)
            dgvItemMaster.Rows.Clear()
            Dim ctr As Integer = 0
            Dim unitcost As Decimal
            Dim ctrGroup As Integer = 0
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                    MRIS_ID = row(0)
                    txtMRIS_Ref.Text = row(6).ToString
                    WHSETo = row(5).ToString
                    cbWHto.SelectedItem = loadWH("From", "Warehouse", WHSETo)
                    If Inv_ComputationMethod = "SC" Then
                        unitcost = GetStandardCost(row(1))
                    Else
                        unitcost = GetAverageCost(row(1))
                    End If
                    txtRemarks.Text = "Material Issuance MRIS: " & row(6)
                    dgvItemMaster.Rows.Add(row(1).ToString, "", row(2).ToString, row(3).ToString, _
                                  CDec(row(4)).ToString("N2"), GetWHSEDesc(row(5).ToString), "", CDec(unitcost).ToString("N2"), _
                                  CDec(0).ToString("N2"), CDec(0).ToString("N2"), CDec(0).ToString("N2"), CDec(0).ToString("N2"), _
                                  CDec(0).ToString("N2"), CDec(0).ToString("N2"), False, False)
                    LoadWHSE(ctr)
                    LoadUOM(row(1).ToString, ctr)
                    LoadColor(row(1).ToString, ctr)
                    LoadSize(row(1).ToString, ctr)
                    ctr += 1
                Next
                LoadBarCode()
                'GenerateEntry()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub
End Class