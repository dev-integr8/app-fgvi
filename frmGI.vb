Imports Excel = Microsoft.Office.Interop.Excel
Public Class frmGI
    Dim TransID, RefID, JETransID As String
    Dim GINo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "GI"
    Dim ColumnPK As String = "GI_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblGI"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim SysCode As String = "EMERALD"
    Dim MR_ID, JO_ID, IC_ID As Integer
    Dim withConfirm As Boolean = True
    Dim tempWHSE As String = ""
    Dim ForApproval As Boolean = False

    Dim Valid As Boolean = True
    Dim InvalidTemplate As Boolean = False
    Dim path As String
    Dim templateName As String = "TEMPLATE_GI"
    Public excelPW As String = "@dm1nEvo"

    Public Overloads Function ShowDialog(ByVal docnumber As String) As Boolean
        TransID = docnumber
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmGI_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            cbIssueTo.Items.Clear()
            cbIssuerFrom.Items.Clear()
            cbIssuerFrom.Items.Add("Warehouse")
            cbIssueTo.Items.Add("Warehouse")
            loadWH()
            loadGIType("Load")
            If TransID <> "" Then
                LoadGI(TransID)
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

    Private Sub LoadCostCenter()
        Dim query As String
        query = " SELECT Code + '|' + Description AS CostCenter FROM tblCC "
        SQL.ReadQuery(query)
        cbCostCenter.Items.Clear()
        While SQL.SQLDR.Read
            cbCostCenter.Items.Add(SQL.SQLDR("CostCenter").ToString)
        End While
    End Sub


    Private Sub loadWHFrom()
        If cbIssuerFrom.SelectedItem = "Warehouse" Then
            Dim query As String
            query = " SELECT tblWarehouse.Code + ' | ' + Description AS WHSECode " & _
                    " FROM tblWarehouse INNER JOIN tblUser_Access " & _
                    " ON tblWarehouse.Code = tblUser_Access.Code " & _
                    " AND tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active' " & _
                    " AND Type = 'Warehouse' AND isAllowed = 1 " & _
                    " WHERE UserID ='" & UserID & "' "
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
        ElseIf cbIssuerFrom.SelectedItem = "Production" Then
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
        End If
    End Sub

    Private Sub loadGIType(ByVal Status As String)
        Dim query As String
        If Status = "View" Then
            query = " SELECT Description FROM tblGI_Type"
            SQL.ReadQuery(query)
        ElseIf Status = "Load" Then
            query = " SELECT Description FROM tblGI_Type WHERE Status = 'Active'"
            SQL.ReadQuery(query)
        End If
        cbTransType.Items.Clear()
        cbTransType.Items.Add("Material Transfer")
        While SQL.SQLDR.Read
            cbTransType.Items.Add(SQL.SQLDR("Description").ToString)
        End While

    End Sub


    Private Sub loadWH()

        If cbIssueTo.SelectedItem = "Warehouse" Then
            Dim query As String
            query = " SELECT tblWarehouse.Code + ' | ' + Description AS Description  FROM tblWarehouse WHERE Status ='Active' "
            SQL.ReadQuery(query)
            cbWHto.Items.Clear()
            cbWHto.Items.Add("Multiple Warehouse")
            While SQL.SQLDR.Read
                cbWHto.Items.Add(SQL.SQLDR("Description"))
            End While
        ElseIf cbIssueTo.SelectedItem = "Production" Then
            Dim query As String
            query = " SELECT tblProdWarehouse.Code + ' | ' + Description AS Description  FROM tblProdWarehouse WHERE Status ='Active' "
            SQL.ReadQuery(query)
            cbWHto.Items.Clear()
            While SQL.SQLDR.Read
                cbWHto.Items.Add(SQL.SQLDR("Description"))
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
        cbWHto.Enabled = Value
        cbWHfrom.Enabled = Value
        cbIssuerFrom.Enabled = Value
        cbIssueTo.Enabled = Value
        btnSearchVCE.Enabled = Value
        txtVCEName.Enabled = Value
        dgvItemMaster.AllowUserToAddRows = Value
        dgvItemMaster.AllowUserToDeleteRows = Value
        dgvItemMaster.ReadOnly = Not Value
        txtATDNo.Enabled = Value
        cbCurrency.Enabled = Value
        txtConversion.Enabled = Value
        If Value = True Then
            dgvItemMaster.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemMaster.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        cbTransType.Enabled = Value
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value

        If TransAuto = True Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If

        dgcStockQTY.ReadOnly = True
        chAvgCost.ReadOnly = True

    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("GI_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("GI")
            TransID = f.transID
            LoadGI(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub LoadGI(ByVal ID As String)
        Dim query, Currency As String
        Dim WHSEfrom, WHSEto, Type As String
        query = " SELECT   TransID, GI_No, VCECode, DateGI, Type, IssueFrom, IssueTo, WHSE_From, WHSE_To, Remarks, Status, MR_Ref," & _
                "   ISNULL(JO_Ref,0) AS JO_Ref, CostCenter, ISNULL(withConfirm,0) AS withConfirm, Currency,  " & _
                "   ISNULL(Exchange_Rate,0) AS Exchange_Rate, ATDNo, ISNULL(IC_Ref,0) AS IC_Ref " & _
                " FROM     tblGI " & _
                " WHERE    TransId = '" & ID & "' " & _
                " ORDER BY TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            GINo = SQL.SQLDR("GI_No").ToString
            txtTransNum.Text = GINo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            dtpDocDate.Text = SQL.SQLDR("DateGI").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            disableEvent = True
            cbIssuerFrom.SelectedItem = SQL.SQLDR("IssueFrom").ToString
            cbIssueTo.SelectedItem = SQL.SQLDR("IssueTo").ToString
            WHSEfrom = SQL.SQLDR("WHSE_From").ToString
            WHSEto = SQL.SQLDR("WHSE_To").ToString
            MR_ID = SQL.SQLDR("MR_Ref").ToString
            JO_ID = SQL.SQLDR("JO_Ref").ToString
            IC_ID = SQL.SQLDR("IC_Ref").ToString
            withConfirm = SQL.SQLDR("withConfirm").ToString
            cbCostCenter.SelectedItem = SQL.SQLDR("CostCenter").ToString
            Type = SQL.SQLDR("Type").ToString
            Currency = SQL.SQLDR("Currency").ToString
            txtConversion.Text = CDec(SQL.SQLDR("Exchange_Rate")).ToString("N4")
            txtATDNo.Text = SQL.SQLDR("ATDNo").ToString
            loadGIType("View")
            cbTransType.SelectedItem = Type
            RefreshTranType()
            txtMRRef.Text = LoadMRNo(MR_ID)
            txtJORef.Text = LoadJONo(JO_ID)
            txtICRef.Text = LoadICNo(IC_ID)
            If txtMRRef.Text = "" Then
                dgvItemMaster.Columns(dgcReqQTY.Index).Visible = False
            Else
                dgvItemMaster.Columns(dgcReqQTY.Index).Visible = True
            End If
            loadWHFrom()
            loadWHto()
            disableEvent = False
            If cbIssuerFrom.SelectedItem = "Production" Then
                cbWHfrom.SelectedItem = GetWHSE(WHSEfrom, "tblProdWarehouse")
            Else
                cbWHfrom.SelectedItem = GetWHSE(WHSEfrom, "tblWarehouse")
            End If
            If cbIssueTo.SelectedItem = "Production" Then
                cbWHto.SelectedItem = GetWHSE(WHSEto, "tblProdWarehouse")
            Else
                cbWHto.SelectedItem = GetWHSE(WHSEto, "tblWarehouse")
            End If
            txtVCEName.Text = GetVCEName(txtVCECode.Text)


            disableEvent = True
            cbCurrency.Items.Clear()
            If txtVCECode.Visible = True Then
                For Each item In LoadVCECurrency(txtVCECode.Text)
                    cbCurrency.Items.Add(item)
                Next
                If cbCurrency.Items.Count = 0 Then
                    cbCurrency.Items.Add(BaseCurrency)
                End If
                cbCurrency.SelectedItem = Currency

                If cbCurrency.SelectedItem <> BaseCurrency Then
                    lblConversion.Visible = True
                    txtConversion.Visible = True
                Else
                    lblConversion.Visible = False
                    txtConversion.Visible = False
                End If
            End If
            disableEvent = False


            LoadGIDetails(TransID)
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
            tsbClose.Enabled = True
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

    Private Sub LoadEntry(ByVal RRNo As Integer)
        Dim query As String
        query = " SELECT ID, JE_No, View_GL_Transaction.BranchCode, View_GL_Transaction.AccntCode, AccountTitle, View_GL_Transaction.VCECode, View_GL_Transaction.VCEName, Debit, Credit, Particulars, RefNo   " & _
                " FROM   View_GL_Transaction INNER JOIN tblCOA_Master " & _
                " ON     View_GL_Transaction.AccntCode = tblCOA_Master.AccountCode " & _
                " WHERE JE_No = (SELECT  JE_No FROM tblJE_Header WHERE RefType = 'GI' AND RefTransID = " & RRNo & ") " & _
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

    Private Function LoadMRNo(MR_ID As Integer) As String
        Dim query As String
        query = " SELECT MR_No FROM tblMR WHERE TransID = '" & MR_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("MR_No")
        Else
            Return ""
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

    Private Function LoadJONo(MR_ID As Integer) As String
        Dim query As String
        query = " SELECT JO_No FROM tblJO WHERE TransID = '" & JO_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("JO_No")
        Else
            Return ""
        End If
    End Function

    Protected Sub LoadGIDetails(ByVal TransID As String)
        Dim ctr As Integer = 0
        Dim query As String
        query = " SELECT	tblGI_Details.ItemCode, tblGI_Details.Description, tblGI_Details.UOM, RequestQTY, StockQTY, IssueQTY, tblGI_Details.WHSE, ISNULL(AvgCost,0) AS AvgCost, tblGI_Details.AccntCode, ISNULL(Amount,0) AS Amount , ISNULL(FreeItem,0) AS FreeItem," & _
                "    ISNULL(tblGI_Details.GrossAmount, 0) AS GrossAmount, " & _
                "    ISNULL(tblGI_Details.DiscountRate, 0) AS DiscountRate,  ISNULL(tblGI_Details.Discount, 0) AS Discount,  ISNULL(tblGI_Details.VATAmount, 0) AS VATAmount,  ISNULL(tblGI_Details.NetAmount, 0) AS NetAmount, " & _
                "    ISNULL(tblGI_Details.VATable, 0) AS VATable,  ISNULL(tblGI_Details.VatInc, 0) AS VatInc, ISNULL(DateExpired,'') AS DateExpired , ISNULL(LotNo,'') AS LotNo, ISNULL(SerialNo,'') AS SerialNo, " & _
                "    ISNULL(tblGI_Details.Size,'') AS Size, ISNULL(tblGI_Details.Color,'') AS Color " & _
                " FROM	    tblGI INNER JOIN tblGI_Details " & _
                " ON		tblGI.TransID = tblGI_Details.TransID " & _
                " WHERE     tblGI_Details.TransId = " & TransID & " " & _
                " ORDER BY  tblGI_Details.LineNum "
        disableEvent = True
        dgvItemMaster.Rows.Clear()
        disableEvent = False
        SQL.ReadQuery(query)
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            Dim IssuedQTY As Decimal = 0
            Dim ConvertQTY As Decimal = 0
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                IssuedQTY = CDec(row(5))
                ConvertQTY = ConvertToBaseUOM(row(0).ToString, row(2).ToString, IssuedQTY)
                dgvItemMaster.Rows.Add(row(0).ToString, "", row(1).ToString, row(2).ToString, _
                                     CDec(row(3)).ToString("N2"), CDec(row(4)).ToString("N2"), CDec(IssuedQTY).ToString("N2"), GetWHSEDesc(row(6).ToString), _
                                     CDec(ConvertQTY).ToString("N2"), CDec(row(7)).ToString("N2"), row(8).ToString, CDec(row(9)).ToString("N2"), row(10).ToString, _
                                      CDec(row(11)).ToString("N2"), CDec(row(12)).ToString("N2"), CDec(row(13)).ToString("N2"), CDec(row(14)).ToString("N2"), CDec(row(15)).ToString("N2"), _
                                      row(16), row(17), row(18).ToString, row(19).ToString, row(20).ToString, row(21).ToString, row(22).ToString)
                LoadUOM(row(0).ToString, ctr)
                LoadColor(row(0).ToString, ctr)
                LoadSize(row(0).ToString, ctr)
                LoadWHSE(ctr)
                ctr += 1
            Next
            LoadBarCode()
        End If
    End Sub

    Private Function ConvertToBaseUOM(ByVal itemCode As String, UOM As String, QTY As Decimal)
        Dim query As String
        Dim ConvertQTY As Decimal = 0
        query = " SELECT ISNULL(QTY,0) as QTY  FROM viewItem_UOM WHERE GroupCode ='" & itemCode & "' AND UnitCode ='" & UOM & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            ConvertQTY = CDec(SQL.SQLDR("QTY")) * QTY
        End If
        Return ConvertQTY
    End Function


    Private Function ConvertToAltUOM(ByVal itemCode As String, UOM As String, QTY As Decimal)
        Dim query As String
        Dim ConvertQTY As Decimal = 0
        query = " SELECT ISNULL(QTY,0) as QTY  FROM viewItem_UOM WHERE GroupCode ='" & itemCode & "' AND UnitCode ='" & UOM & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            ConvertQTY = QTY / CDec(SQL.SQLDR("QTY"))
        End If
        Return ConvertQTY
    End Function

    Private Sub LoadWHSE(Optional ByVal SelectedIndex As Integer = -1)
        Try
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemMaster.Item(chWHSE.Index, SelectedIndex)
            dgvCB.Items.Clear()
            ' ADD ALL WHSEc
            Dim query As String
            If cbIssueTo.SelectedItem = "Warehouse" Then
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
        txtATDNo.Clear()
        txtMRRef.Clear()
        cbWHto.SelectedIndex = -1
        cbTransType.SelectedIndex = -1
        dgvItemMaster.Rows.Clear()
        lvAccount.Items.Clear()
        txtRemarks.Clear()
        txtStatus.Text = "Open"
        dtpDocDate.MinDate = GetMaxPEC().AddDays(1)
        If Inv_ComputationMethod <> "SC" Then
            dtpDocDate.MinDate = GetMaxInventoryDate()
        End If
        dtpDocDate.Value = Date.Today.Date
        cbCurrency.Items.Clear()
        txtConversion.Text = ""
        loadGIType("Load")
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("GI_ADD") Then
            msgRestricted()
        Else
            Cleartext()
            TransID = ""
            GINo = ""
            MR_ID = 0
            JO_ID = 0
            IC_ID = 0


            dgvItemMaster.Columns(dgcReqQTY.Index).Visible = False

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
        If Not AllowAccess("GI_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)
            LoadStock()
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
            RefreshTranType()
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        Try
            If DataValidated() Then
                If TransID = "" Then
                    If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                        TransID = GenerateTransID(ColumnID, DBTable)
                        If TransAuto Then
                            GINo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                        Else
                            GINo = txtTransNum.Text
                        End If
                        txtTransNum.Text = GINo
                        SaveGI()
                        Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                        LoadGI(TransID)
                    End If
                Else
                    If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                        If GINo = txtTransNum.Text Then
                            GINo = txtTransNum.Text
                            UpdateGI()
                            Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                            GINo = txtTransNum.Text
                            LoadGI(TransID)
                        Else
                            If Not IfExist(txtTransNum.Text) Then
                                GINo = txtTransNum.Text
                                UpdateGI()
                                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                                GINo = txtTransNum.Text
                                LoadGI(TransID)
                            Else
                                MsgBox("GI " & txtTransNum.Text & " already exist!", MsgBoxStyle.Exclamation)
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Function IfExist(ByVal ID As Integer) As Boolean
        Dim query As String
        query = " SELECT * FROM tblGI WHERE TransNo ='" & ID & "'  AND BranchCode = '" & BranchCode & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function


    Private Function DataValidated() As Boolean
        If cbWHto.SelectedIndex = -1 And withConfirm = True Then
            Msg("Please select warehouse!", MsgBoxStyle.Exclamation)
            Return False
            'ElseIf txtATDNo.Text = "" Then  ' CHECK ATD No
            '    Msg("Please check ATD No.!", MsgBoxStyle.Exclamation)
            '    Return False
        ElseIf TransID = "" AndAlso txtATDNo.Text <> "" AndAlso ATDvalid() <> "" Then  ' CHECK PO REF. IF ALREADY EXIST 
            Msg("ATD No. already used for GI No. " & ATDvalid() & "!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf dgvItemMaster.Rows.Count <= 1 Then
            Msg("There are no items on the list!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf txtConversion.Visible = True And txtConversion.Text = "" And withConfirm = False Then
            MsgBox("Please check exchange rate!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf txtVCEName.Visible = True And txtVCECode.Text = "" Then
            Msg("Please insert VCE Name!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf cbTransType.SelectedIndex = -1 Then
            Msg("Please select transaction type!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf validateDGV() = False Then
            Return False
        Else
            Return True
        End If
        Return True

    End Function

    Private Function ATDvalid() As String
        Dim query As String
        query = " SELECT GI_No FROM tblGI WHERE CAST(ATDNo AS INT) = @ATDNo AND Status <> 'Cancelled' "
        SQL.FlushParams()
        SQL.AddParam("@ATDNo", txtATDNo.Text)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("GI_No")
        Else
            Return ""
        End If
    End Function

    Private Function validateDGV() As Boolean
        Dim reqQTY, stockQTY, issueQTY As Decimal
        Dim free As Boolean
        Dim value As Boolean = True
        For Each row As DataGridViewRow In dgvItemMaster.Rows
            If row.Cells(chItemCode.Index).Value <> "" Then
                If Not IsNumeric(row.Cells(dgcReqQTY.Index).Value) Then reqQTY = 0 Else reqQTY = row.Cells(dgcReqQTY.Index).Value
                If Not IsNumeric(row.Cells(dgcStockQTY.Index).Value) Then stockQTY = 0 Else stockQTY = row.Cells(dgcStockQTY.Index).Value
                If Not IsNumeric(row.Cells(dgcIssueQTY.Index).Value) Then issueQTY = 0 Else issueQTY = row.Cells(dgcIssueQTY.Index).Value
                If row.Cells(chkFree.Index).Value = True Then free = True Else free = False

                'If txtATDNo.Text = "" And free = False Then  ' CHECK ATD No
                '    Msg("Please check ATD No.!", MsgBoxStyle.Exclamation)
                '    value = False
                '    Exit For
                'End If

                'If issueQTY > stockQTY Then
                '    Msg("Issue Quantity should not be greater than Stock Quantity", MsgBoxStyle.Exclamation)
                '    value = False
                '    Exit For
                'End If
            End If
        Next
        Return value
    End Function


    Private Sub SaveGI()
        Dim SQL1 As New SQLControl
        SQL1.BeginTransaction()
        Try
            Dim WHSEfrom, WHSEto, IssueTo, Type, CC As String
            Type = IIf(cbTransType.SelectedIndex = -1, "", cbTransType.SelectedItem)

            WHSEfrom = GetWHSE(cbWHfrom.SelectedItem)

            If cbIssueTo.SelectedIndex = -1 Then IssueTo = "" Else IssueTo = cbIssueTo.SelectedItem
            If cbWHto.SelectedIndex = -1 Then WHSEto = "" Else WHSEto = GetWHSE(cbWHto.SelectedItem)
            If cbCostCenter.SelectedIndex = -1 Then CC = "" Else CC = GetCCCode(cbCostCenter.SelectedItem)

            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblGI(TransID, GI_No, BranchCode, BusinessCode, DateGI, VCECode, IssueFrom, IssueTo, WHSE_From, WHSE_To, Type, Remarks, " & _
                        "   CostCenter, withConfirm, JO_Ref, MR_Ref, WhoCreated, Currency, Exchange_Rate, ATDNo, IC_Ref, Status) " & _
                        " VALUES (@TransID, @GI_No, @BranchCode, @BusinessCode, @DateGI, @VCECode, @IssueFrom, @IssueTo, @WHSE_From, @WHSE_To, @Type, @Remarks, " & _
                        "   @CostCenter, @withConfirm, @JO_Ref, @MR_Ref, @WhoCreated, @Currency, @Exchange_Rate, @ATDNo, @IC_Ref, @Status) "
            SQL1.FlushParams()
            SQL1.AddParam("@TransID", TransID)
            SQL1.AddParam("@GI_No", GINo)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@DateGI", dtpDocDate.Value.Date)
            If withConfirm = False Then
                SQL1.AddParam("@VCECode", txtVCECode.Text)
                SQL1.AddParam("@Currency", cbCurrency.SelectedItem)
                SQL1.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            Else
                SQL1.AddParam("@VCECode", "")
                SQL1.AddParam("@Currency", "")
                SQL1.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            End If
            SQL1.AddParam("@IssueFrom", cbIssuerFrom.SelectedItem)
            SQL1.AddParam("@IssueTo", IssueTo)
            SQL1.AddParam("@WHSE_From", WHSEfrom)
            SQL1.AddParam("@WHSE_To", WHSEto)
            SQL1.AddParam("@withConfirm", withConfirm)
            SQL1.AddParam("@CostCenter", CC)
            If IsNumeric(MR_ID) Then SQL1.AddParam("@MR_Ref", MR_ID) Else SQL1.AddParam("@MR_Ref", 0)
            If IsNumeric(JO_ID) Then SQL1.AddParam("@JO_Ref", JO_ID) Else SQL1.AddParam("@JO_Ref", 0)
            If IsNumeric(IC_ID) Then SQL1.AddParam("@IC_Ref", IC_ID) Else SQL1.AddParam("@IC_Ref", 0)
            SQL1.AddParam("@Type", Type)
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@ATDNo", txtATDNo.Text)
            If ForApproval = True Then SQL1.AddParam("@Status", "Draft") Else SQL1.AddParam("@Status", "Active")
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            Dim ItemCode, Description, UOM, AccntCode, DateExpired, LotNo, SerialNo, Size, Color As String
            Dim reqQTY, stockQTY, issueQTY, UnitCost, Amount, GrossAmount, DiscountRate, DiscountAmount, VatAmount, NetAmount As Decimal
            Dim freeitem As Boolean
            For Each row As DataGridViewRow In dgvItemMaster.Rows
                If Not row.Cells(dgcIssueQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                    Description = IIf(row.Cells(chItemName.Index).Value = Nothing, "", row.Cells(chItemName.Index).Value)
                    UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                    AccntCode = IIf(row.Cells(dgcAccntCode.Index).Value = Nothing, "", row.Cells(dgcAccntCode.Index).Value)
                    freeitem = row.Cells(chkFree.Index).Value
                    ' UnitCost = GetAverageCost(ItemCode)
                    UnitCost = IIf(row.Cells(chAvgCost.Index).Value = Nothing, "0.00", row.Cells(chAvgCost.Index).Value)
                    'EPEOPLE
                    Amount = IIf(row.Cells(chAmount.Index).Value = Nothing, "0.00", row.Cells(chAmount.Index).Value)
                    If IsNumeric(row.Cells(dgcReqQTY.Index).Value) Then reqQTY = CDec(row.Cells(dgcReqQTY.Index).Value) Else reqQTY = 1
                    If IsNumeric(row.Cells(dgcStockQTY.Index).Value) Then stockQTY = CDec(row.Cells(dgcStockQTY.Index).Value) Else stockQTY = 1
                    If IsNumeric(row.Cells(dgcIssueQTY.Index).Value) Then issueQTY = CDec(row.Cells(dgcIssueQTY.Index).Value) Else issueQTY = 1

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
                         " tblGI_Details(TransId, ItemCode, Description, UOM, RequestQTY, StockQTY, IssueQTY, WHSE, LineNum, AvgCost, WhoCreated, AccntCode, Amount, FreeItem, " & _
                         "              GrossAmount, VATAmount, DiscountRate, Discount, NetAmount, VATable, VatInc, SerialNo, LotNo, DateExpired, Size, Color) " & _
                         " VALUES(@TransId, @ItemCode, @Description, @UOM, @RequestQTY, @StockQTY, @IssueQTY, @WHSE, @LineNum, @AvgCost, @WhoCreated, @AccntCode, @Amount, @FreeItem, " & _
                         "               @GrossAmount, @VATAmount, @DiscountRate, @Discount, @NetAmount, @VATable, @VatInc, @SerialNo, @LotNo, @DateExpired, @Size, @Color) "
                    SQL1.FlushParams()
                    SQL1.AddParam("@TransID", TransID)
                    SQL1.AddParam("@ItemCode", ItemCode)
                    SQL1.AddParam("@Description", Description)
                    SQL1.AddParam("@UOM", UOM)
                    SQL1.AddParam("@RequestQTY", reqQTY)
                    SQL1.AddParam("@StockQTY", stockQTY)
                    SQL1.AddParam("@IssueQTY", issueQTY)
                    'SQL1.AddParam("@WHSE", WHSEto)
                    If cbWHto.SelectedItem = "Multiple Warehouse" Then
                        SQL1.AddParam("@WHSE", "MW")
                    Else
                        SQL1.AddParam("@WHSE", tempWHSE)
                    End If
                    SQL1.AddParam("@LineNum", line)
                    SQL1.AddParam("@AvgCost", UnitCost)
                    SQL1.AddParam("@WhoCreated", UserID)
                    SQL1.AddParam("@AccntCode", AccntCode)
                    SQL1.AddParam("@Amount", Amount)
                    SQL1.AddParam("@FreeItem", freeitem)
                    SQL1.AddParam("@GrossAmount", GrossAmount)
                    SQL1.AddParam("@DiscountRate", DiscountRate)
                    SQL1.AddParam("@Discount", DiscountAmount)
                    SQL1.AddParam("@VATAmount", VatAmount)
                    SQL1.AddParam("@NetAmount", NetAmount)
                    If IsNothing(row.Cells(chVAT.Index).Value) Then SQL1.AddParam("@VATable", False) Else SQL1.AddParam("@VATable", row.Cells(chVAT.Index).Value)
                    If IsNothing(row.Cells(chVATInc.Index).Value) Then SQL1.AddParam("@VatInc", False) Else SQL1.AddParam("@VatInc", row.Cells(chVATInc.Index).Value)
                    SQL1.AddParam("@LotNo", LotNo)
                    SQL1.AddParam("@SerialNo", SerialNo)
                    SQL1.AddParam("@DateExpired", DateExpired)
                    SQL1.AddParam("@Size", Size)
                    SQL1.AddParam("@Color", Color)

                    SQL1.ExecNonQuery(insertSQL)
                    line += 1

                    If withConfirm = False Then
                        SaveINVTY("OUT", ModuleID, "GI", TransID, dtpDocDate.Value.Date, ItemCode, WHSEfrom, issueQTY, UOM, UnitCost, "Active", SerialNo, LotNo, DateExpired, Size, Color)
                    End If

                End If
            Next

            If withConfirm = False Then
                ComputeWAUC("GI", TransID)
            End If

            If withConfirm = False Then
                If cbTransType.SelectedItem <> "Inventory Count" Then
                    line = 1
                    JETransID = GenerateTransID("JE_No", "tblJE_Header")

                    insertSQL = " INSERT INTO " & _
                               " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, " & _
                               "    Currency, Exchange_Rate, Remarks,  WhoCreated, Status) " & _
                               " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR,  " & _
                               "    @Currency, @Exchange_Rate, @Remarks,  @WhoCreated, @Status)"
                    SQL1.FlushParams()
                    SQL1.AddParam("@JE_No", JETransID)
                    SQL1.AddParam("@AppDate", dtpDocDate.Value.Date)
                    SQL1.AddParam("@RefType", "GI")
                    SQL1.AddParam("@RefTransID", TransID)
                    SQL1.AddParam("@Book", "Inventory")
                    SQL1.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                    SQL1.AddParam("@Currency", cbCurrency.SelectedItem)
                    SQL1.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
                    SQL1.AddParam("@Remarks", txtRemarks.Text)
                    SQL1.AddParam("@BranchCode", BranchCode)
                    SQL1.AddParam("@BusinessCode", BusinessType)
                    If ForApproval = True Then SQL1.AddParam("@Status", "Draft") Else SQL1.AddParam("@Status", "Saved")
                    SQL1.AddParam("@WhoCreated", UserID)
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
            End If
            SQL1.Commit()
        Catch ex As Exception
            SQL1.Rollback()
            TransID = ""
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "GI_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL1.FlushParams()
        End Try
    End Sub

    Private Sub UpdateGI()
        Dim SQL As New SQLControl
        SQL.BeginTransaction()
        Try
            Dim WHSEfrom, WHSEto, Type, IssueTo, CC As String
            Type = IIf(cbTransType.SelectedIndex = -1, "", cbTransType.SelectedItem)

            If cbIssueTo.SelectedIndex = -1 Then IssueTo = "" Else IssueTo = cbIssueTo.SelectedItem
            If cbWHto.SelectedIndex = -1 Then WHSEto = "" Else WHSEto = GetWHSE(cbWHto.SelectedItem)
            If cbCostCenter.SelectedIndex = -1 Then CC = "" Else CC = GetCCCode(cbCostCenter.SelectedItem)
            WHSEfrom = GetWHSE(cbWHfrom.SelectedItem)
            activityStatus = True
            Dim insertSQL, updateSQL, deleteSQL As String
            updateSQL = " UPDATE    tblGI " & _
                        " SET       GI_No = @GI_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, DateGI = @DateGI, VCECode = @VCECode, " & _
                        "           IssueFrom = @IssueFrom, IssueTo = @IssueTo, WHSE_From = @WHSE_From, WHSE_To = @WHSE_To, Type = @Type, Remarks = @Remarks, " & _
                        "           CostCenter = @CostCenter, withConfirm = @withConfirm, JO_Ref = @JO_Ref, MR_Ref = @MR_Ref, WhoModified = @WhoModified, DateModified = GETDATE(), " & _
                        "           Currency = @Currency, Exchange_Rate = @Exchange_Rate, ATDNo = @ATDNo, IC_Ref = @IC_Ref " & _
                        " WHERE     TransID = @TransID  "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@GI_No", GINo)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateGI", dtpDocDate.Value.Date)
            If withConfirm = False Then
                SQL.AddParam("@VCECode", txtVCECode.Text)
                SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            Else
                SQL.AddParam("@VCECode", "")
                SQL.AddParam("@Currency", "")
                SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            End If
            SQL.AddParam("@IssueFrom", cbIssuerFrom.SelectedItem)
            SQL.AddParam("@IssueTo", IssueTo)
            SQL.AddParam("@WHSE_From", WHSEfrom)
            SQL.AddParam("@WHSE_To", WHSEto)
            SQL.AddParam("@withConfirm", withConfirm)
            SQL.AddParam("@CostCenter", CC)
            If IsNumeric(MR_ID) Then SQL.AddParam("@MR_Ref", MR_ID) Else SQL.AddParam("@MR_Ref", 0)
            If IsNumeric(JO_ID) Then SQL.AddParam("@JO_Ref", JO_ID) Else SQL.AddParam("@JO_Ref", 0)
            If IsNumeric(IC_ID) Then SQL.AddParam("@IC_Ref", IC_ID) Else SQL.AddParam("@IC_Ref", 0)
            SQL.AddParam("@Type", Type)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@ATDNo", txtATDNo.Text)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)

            deleteSQL = " DELETE FROM tblGI_Details WHERE TransID =@TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.ExecNonQuery(deleteSQL)

            DELINVTY(ModuleID, "GI", TransID)

            Dim line As Integer = 1
            Dim ItemCode, Description, UOM, AccntCode, DateExpired, LotNo, SerialNo, Size, Color As String
            Dim reqQTY, stockQTY, issueQTY, UnitCost, Amount, GrossAmount, DiscountRate, DiscountAmount, VatAmount, NetAmount As Decimal
            Dim freeitem As Boolean
            For Each row As DataGridViewRow In dgvItemMaster.Rows
                If Not row.Cells(dgcIssueQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                    Description = IIf(row.Cells(chItemName.Index).Value = Nothing, "", row.Cells(chItemName.Index).Value)
                    UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                    AccntCode = IIf(row.Cells(dgcAccntCode.Index).Value = Nothing, "", row.Cells(dgcAccntCode.Index).Value)
                    freeitem = row.Cells(chkFree.Index).Value
                    ' UnitCost = GetAverageCost(ItemCode)
                    UnitCost = IIf(row.Cells(chAvgCost.Index).Value = Nothing, "0.00", row.Cells(chAvgCost.Index).Value)
                    'EPEOPLE
                    Amount = IIf(row.Cells(chAmount.Index).Value = Nothing, "0.00", row.Cells(chAmount.Index).Value)
                    If IsNumeric(row.Cells(dgcReqQTY.Index).Value) Then reqQTY = CDec(row.Cells(dgcReqQTY.Index).Value) Else reqQTY = 1
                    If IsNumeric(row.Cells(dgcStockQTY.Index).Value) Then stockQTY = CDec(row.Cells(dgcStockQTY.Index).Value) Else stockQTY = 1
                    If IsNumeric(row.Cells(dgcIssueQTY.Index).Value) Then issueQTY = CDec(row.Cells(dgcIssueQTY.Index).Value) Else issueQTY = 1

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
                         " tblGI_Details(TransId, ItemCode, Description, UOM, RequestQTY, StockQTY, IssueQTY, WHSE, LineNum, AvgCost, WhoCreated, AccntCode, Amount, FreeItem, " & _
                         "              GrossAmount, VATAmount, DiscountRate, Discount, NetAmount, VATable, VatInc, SerialNo, LotNo, DateExpired, Size, Color) " & _
                         " VALUES(@TransId, @ItemCode, @Description, @UOM, @RequestQTY, @StockQTY, @IssueQTY, @WHSE, @LineNum, @AvgCost, @WhoCreated, @AccntCode, @Amount, @FreeItem, " & _
                         "               @GrossAmount, @VATAmount, @DiscountRate, @Discount, @NetAmount, @VATable, @VatInc, @SerialNo, @LotNo, @DateExpired, @Size, @Color) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@ItemCode", ItemCode)
                    SQL.AddParam("@Description", Description)
                    SQL.AddParam("@UOM", UOM)
                    SQL.AddParam("@RequestQTY", reqQTY)
                    SQL.AddParam("@StockQTY", stockQTY)
                    SQL.AddParam("@IssueQTY", issueQTY)
                    'SQL.AddParam("@WHSE", WHSEto)
                    If cbWHto.SelectedItem = "Multiple Warehouse" Then
                        SQL.AddParam("@WHSE", "MW")
                    Else
                        SQL.AddParam("@WHSE", tempWHSE)
                    End If
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@AvgCost", UnitCost)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.AddParam("@AccntCode", AccntCode)
                    SQL.AddParam("@Amount", Amount)
                    SQL.AddParam("@FreeItem", freeitem)
                    SQL.AddParam("@GrossAmount", GrossAmount)
                    SQL.AddParam("@DiscountRate", DiscountRate)
                    SQL.AddParam("@Discount", DiscountAmount)
                    SQL.AddParam("@VATAmount", VatAmount)
                    SQL.AddParam("@NetAmount", NetAmount)
                    If IsNothing(row.Cells(chVAT.Index).Value) Then SQL.AddParam("@VATable", False) Else SQL.AddParam("@VATable", row.Cells(chVAT.Index).Value)
                    If IsNothing(row.Cells(chVATInc.Index).Value) Then SQL.AddParam("@VatInc", False) Else SQL.AddParam("@VatInc", row.Cells(chVATInc.Index).Value)
                    SQL.AddParam("@LotNo", LotNo)
                    SQL.AddParam("@SerialNo", SerialNo)
                    SQL.AddParam("@DateExpired", DateExpired)
                    SQL.AddParam("@Size", Size)
                    SQL.AddParam("@Color", Color)

                    SQL.ExecNonQuery(insertSQL)
                    line += 1

                    If withConfirm = False Then
                        SaveINVTY("OUT", ModuleID, "GI", TransID, dtpDocDate.Value.Date, ItemCode, WHSEfrom, issueQTY, UOM, UnitCost, "Active", SerialNo, LotNo, DateExpired, Size, Color)
                    End If

                End If
            Next

            If withConfirm = False Then
                ComputeWAUC("GI", TransID)
            End If

            If withConfirm = False Then
                line = 1
                JETransID = LoadJE("GI", TransID)

                ' UPDATE ENTRIES
                If JETransID = 0 Then
                    JETransID = GenerateTransID("JE_No", "tblJE_Header")

                    insertSQL = " INSERT INTO " & _
                        " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, Currency, Exchange_Rate, WhoCreated) " & _
                        " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @Currency, @Exchange_Rate, @WhoCreated)"
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransID)
                    SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                    SQL.AddParam("@RefType", "GI")
                    SQL.AddParam("@RefTransID", TransID)
                    SQL.AddParam("@Book", "Inventory")
                    SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                    SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                    SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
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
                    SQL.AddParam("@RefType", "GI")
                    SQL.AddParam("@RefTransID", TransID)
                    SQL.AddParam("@Book", "Inventory")
                    SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                    SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                    SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
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
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "GI_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("GI_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL, updateSQL As String
                        deleteSQL = " UPDATE  tblGI SET Status ='Cancelled' WHERE GI_No = @GI_No "
                        SQL.FlushParams()
                        GINo = txtTransNum.Text
                        SQL.AddParam("@GI_No", GINo)
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

                        Dim WHSEfrom As String
                        WHSEfrom = GetWHSE(cbWHfrom.SelectedItem)


                        Dim line As Integer = 1
                        Dim ItemCode, Description, UOM As String
                        Dim reqQTY, stockQTY, issueQTY, UnitCost As Decimal
                        For Each row As DataGridViewRow In dgvItemMaster.Rows
                            If Not row.Cells(dgcIssueQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                                ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                                Description = IIf(row.Cells(chItemName.Index).Value = Nothing, "", row.Cells(chItemName.Index).Value)
                                UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                                UnitCost = IIf(row.Cells(chAvgCost.Index).Value = Nothing, "0.00", row.Cells(chAvgCost.Index).Value)
                                If IsNumeric(row.Cells(dgcReqQTY.Index).Value) Then reqQTY = CDec(row.Cells(dgcReqQTY.Index).Value) Else reqQTY = 1
                                If IsNumeric(row.Cells(dgcStockQTY.Index).Value) Then stockQTY = CDec(row.Cells(dgcStockQTY.Index).Value) Else stockQTY = 1
                                If IsNumeric(row.Cells(dgcIssueQTY.Index).Value) Then issueQTY = CDec(row.Cells(dgcIssueQTY.Index).Value) Else issueQTY = 1
                                line += 1

                                If withConfirm = False Then
                                    SaveINVTY("IN", ModuleID, "GI", TransID, Date.Today, ItemCode, WHSEfrom, issueQTY, UOM, UnitCost, "Active")
                                End If

                            End If
                        Next
                        If withConfirm = False Then
                            ComputeWAUC("GI", TransID)

                            JETransID = LoadJE("GI", TransID)
                            updateSQL = " UPDATE tblJE_Header " & _
                              " SET    Status = @Status,  WhoModified = @WhoModified, DateModified = GETDATE()" & _
                              " WHERE  JE_No = @JE_No "
                            SQL.FlushParams()
                            SQL.AddParam("@JE_No", JETransID)
                            SQL.AddParam("@Status", "Cancelled")
                            SQL.AddParam("@WhoModified", UserID)
                            SQL.ExecNonQuery(updateSQL)
                        End If

                        LoadGI(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "GI_No", GINo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If GINo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID " & _
                    "  FROM     tblGI LEFT JOIN tblVCE_Master " & _
                    "  ON	   tblGI .VCECode = tblVCE_Master.VCECode " & _
                    "   LEFT JOIN tblProdWarehouse  ON	          " & _
                    "   tblGI.WHSE_From = tblProdWarehouse.Code    " & _
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
                    "  	 WHERE     UserID ='" & UserID & "'))  AND GI_No < '" & GINo & "' ORDER BY GI_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadGI(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If GINo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID " & _
                    "  FROM     tblGI LEFT JOIN tblVCE_Master " & _
                    "  ON	   tblGI .VCECode = tblVCE_Master.VCECode " & _
                    "   LEFT JOIN tblProdWarehouse  ON	          " & _
                    "   tblGI.WHSE_From = tblProdWarehouse.Code    " & _
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
                    "  	 WHERE     UserID ='" & UserID & "'))  AND GI_No > '" & GINo & "' ORDER BY GI_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadGI(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If GINo = "" Then
            Cleartext()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadGI(TransID)
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
                        f.ShowDialog("SerialItem", itemCode, "itemcode")
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
                        f.ShowDialog("SerialItem", itemCode, "barcode")
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
                        f.ShowDialog("SerialItem", itemCode, "itemname")
                        ' f.ShowDialog("All Item", itemCode)
                        If f.TransID <> "" Then
                            itemCode = f.TransID
                            LoadItemDetails(itemCode)
                        Else
                            dgvItemMaster.Rows.RemoveAt(e.RowIndex)
                        End If
                        f.Dispose()
                    End If
                    If dgvItemMaster.Item(chItemName.Index, e.RowIndex).Value <> "" Then
                        LoadStock()
                    End If
                    GenerateEntry()
                Case dgcReqQTY.Index
                    If dgvItemMaster.Item(chItemName.Index, e.RowIndex).Value <> "" Then
                        LoadStock()
                    End If
                    GenerateEntry()
                Case dgcIssueQTY.Index
                    If dgvItemMaster.Item(e.ColumnIndex, e.RowIndex).Value = "" OrElse IsNothing(dgvItemMaster.Item(e.ColumnIndex, e.RowIndex).Value) Then
                        dgvItemMaster.Item(e.ColumnIndex, e.RowIndex).Value = 0
                    ElseIf IsNumeric(dgvItemMaster.Item(chAvgCost.Index, e.RowIndex).Value) AndAlso IsNumeric(dgvItemMaster.Item(dgcIssueQTY.Index, e.RowIndex).Value) Then
                        Recompute(e.RowIndex, e.ColumnIndex)
                        GenerateEntry()
                        dgvItemMaster.Item(chAvgCost.Index, e.RowIndex).Value = CDec(dgvItemMaster.Item(chAvgCost.Index, e.RowIndex).Value).ToString("N2")
                    End If
                    GenerateEntry()
                Case chUOM.Index
                    If dgvItemMaster.Item(chItemName.Index, e.RowIndex).Value <> "" Then
                        LoadStock()
                    End If
                    GenerateEntry()
                    'EPEOPLE
                Case chAmount.Index
                    GenerateEntry()
                Case chkFree.Index
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

    Private Sub Recompute(ByVal RowID As Integer, ByVal ColID As Integer)
        Dim gross, VAT, discount, net, baseVAT As Decimal
        If RowID <> -1 Then
            ' GET GROSS AMOUNT (VAT INCLUSIVE)
            If IsNumeric(dgvItemMaster.Item(chAvgCost.Index, RowID).Value) AndAlso IsNumeric(dgvItemMaster.Item(dgcIssueQTY.Index, RowID).Value) AndAlso dgvItemMaster.Columns(dgcIssueQTY.Index).Visible = True Then
                gross = CDec(dgvItemMaster.Item(dgcIssueQTY.Index, RowID).Value) * CDec(dgvItemMaster.Item(chAvgCost.Index, RowID).Value)
            ElseIf IsNumeric(dgvItemMaster.Item(chGrossAmount.Index, RowID).Value) Then
                gross = CDec(dgvItemMaster.Item(chGrossAmount.Index, RowID).Value)
            Else
                gross = 0
            End If
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
                        VAT = CDec(baseVAT * 0.12).ToString("N2")
                    Else
                        baseVAT = (gross / 1.12)
                        VAT = CDec(baseVAT * 0.12).ToString("N2")
                    End If

                End If
            ElseIf ColID = chVATInc.Index Then
                If dgvItemMaster.Item(chVAT.Index, RowID).Value = False Then
                    VAT = 0
                Else
                    If dgvItemMaster.Item(chVATInc.Index, RowID).Value = True Then
                        dgvItemMaster.Item(chVATInc.Index, RowID).Value = False
                        VAT = CDec(baseVAT * 0.12).ToString("N2")
                    Else
                        dgvItemMaster.Item(chVATInc.Index, RowID).Value = True
                        baseVAT = (gross / 1.12)
                        VAT = CDec(baseVAT * 0.12).ToString("N2")
                    End If

                End If
            Else
                If dgvItemMaster.Item(chVAT.Index, RowID).Value = False Then
                    VAT = 0
                    dgvItemMaster.Item(chVATInc.Index, RowID).ReadOnly = True
                Else
                    dgvItemMaster.Item(chVATInc.Index, RowID).ReadOnly = False
                    If dgvItemMaster.Item(chVATInc.Index, RowID).Value = True Then ' IF VAT INCLUSIVE, BASE AMOUNT WILL BE GROSS / 1.12
                        baseVAT = (gross / 1.12)
                    End If
                    VAT = CDec(baseVAT * 0.12).ToString("N2")
                End If
            End If


            ' COMPUTE DISCOUNT
            If IsNumeric(dgvItemMaster.Item(chDiscountAmount.Index, RowID).Value) Then
                discount = CDec(dgvItemMaster.Item(chDiscountAmount.Index, RowID).Value)
            Else
                discount = 0
            End If

            net = baseVAT - discount + VAT
            dgvItemMaster.Item(chGrossAmount.Index, RowID).Value = Format(gross, "#,###,###,###.00").ToString()
            dgvItemMaster.Item(chDiscountAmount.Index, RowID).Value = Format(discount, "#,###,###,###.00").ToString()
            dgvItemMaster.Item(chVATAmount.Index, RowID).Value = Format(VAT, "#,###,###,###.00").ToString()
            dgvItemMaster.Item(chNetAmount.Index, RowID).Value = Format(net, "#,###,###,###.00").ToString()
            ' ComputeTotal()
        End If
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

    Private Sub LoadItemDetails(ByVal ID As String, Optional UOM As String = "")
        Dim query, ItemCode As String
        Dim unitcost As Decimal
        'query = " SELECT    tblItem_Master.ItemCode, ItemName, 1, AD_Inv , AverageCost, ID_SC, ItemUOM " & _
        '        '" FROM       tblItem_Master " & _
        '"  INNER JOIN " & _
        '"   ( SELECT    TOP 1  ItemCode, AverageCost  " & _
        '"                  FROM      tblInventory  " & _
        '" 				   WHERE     tblInventory.ItemCode = @ItemCode " & _
        '"                  ORDER BY  PostDate DESC, DateCreated DESC ) AS AvgCost " & _
        '" 				 ON AvgCost.ItemCode = tblItem_Master.ItemCode " & _
        '" WHERE     tblItem_Master.ItemCode = @ItemCode"

        'SQL.FlushParams()
        'SQL.AddParam("@ItemCode", IIf(ItemCode = Nothing, "", ItemCode))
        'SQL.ReadQuery(query)


        query = " SELECT  ItemCode,  ItemName, UOM AS ItemUOM,  " & _
                  "         ISNULL(ID_SC,0) AS ID_SC, WHSE AS  ID_Warehouse,  AD_Inv, DateExpired, LotNo, SerialNo, EB, Size, Color " & _
                  " FROM    viewItem_StockSerial " & _
                  " WHERE   TransID = @TransID "
        SQL.FlushParams()
        SQL.AddParam("@TransID", ID)
        SQL.ReadQuery(query)


        If SQL.SQLDR.Read Then
            If dgvItemMaster.SelectedCells.Count > 0 Then
                ItemCode = SQL.SQLDR("ItemCode").ToString
                If Inv_ComputationMethod = "SC" Then
                    unitcost = CDec(SQL.SQLDR("ID_SC")).ToString("N2")
                Else
                    unitcost = GetAverageCost(ItemCode)
                End If

                If UOM = "" Then
                    UOM = SQL.SQLDR("ItemUOM").ToString
                End If
                dgvItemMaster.Item(chItemCode.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = ItemCode
                dgvItemMaster.Item(chItemName.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemName").ToString
                dgvItemMaster.Item(chUOM.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = UOM
                dgvItemMaster.Item(4, dgvItemMaster.SelectedCells(0).RowIndex).Value = 1
                dgvItemMaster.Item(5, dgvItemMaster.SelectedCells(0).RowIndex).Value = 1
                dgvItemMaster.Item(6, dgvItemMaster.SelectedCells(0).RowIndex).Value = 1
                dgvItemMaster.Item(7, dgvItemMaster.SelectedCells(0).RowIndex).Value = IIf(cbWHto.SelectedIndex = -1, "", cbWHto.SelectedItem)
                dgvItemMaster.Item(dgcAccntCode.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("AD_Inv").ToString
                dgvItemMaster.Item(chAvgCost.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = CDec(unitcost).ToString("N2")
                'Epeople
                dgvItemMaster.Item(chAmount.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = CDec(SQL.SQLDR("ID_SC")).ToString("N2")
                dgvItemMaster.Item(chDateExpired.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("DateExpired").ToString
                dgvItemMaster.Item(chLotNo.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("LotNo").ToString
                dgvItemMaster.Item(chSerialNo.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("SerialNo").ToString
                dgvItemMaster.Item(chSize.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("Size").ToString
                dgvItemMaster.Item(chColor.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("Color").ToString
                LoadWHSE(dgvItemMaster.SelectedCells(0).RowIndex)
                LoadUOM(ItemCode, dgvItemMaster.Rows.Count - 2)
                LoadColor(ItemCode, dgvItemMaster.Rows.Count - 2)
                LoadSize(ItemCode, dgvItemMaster.Rows.Count - 2)
            End If
            LoadBarCode()
            LoadStock()
        End If
    End Sub

    Private Sub GenerateEntry()
        If disableEvent = False Then
            Dim debitaccnt As String
            If cbTransType.SelectedIndex <> -1 Then
                If withConfirm = False Then
                    debitaccnt = GetGITypeAccntCode(cbTransType.SelectedItem)
                    lvAccount.Items.Clear()
                    For Each row As DataGridViewRow In dgvItemMaster.Rows
                        If IsNumeric(row.Cells(chAvgCost.Index).Value) AndAlso Not IsNothing(row.Cells(dgcAccntCode.Index).Value) Then

                            If cbTransType.SelectedItem = "Purchase Return" Then
                                'FOR DEBIT
                                lvAccount.Items.Add(debitaccnt)
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(debitaccnt))
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chNetAmount.Index).Value).ToString("N2"))
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")

                                If row.Cells(chVATAmount.Index).Value <> 0 Then
                                    lvAccount.Items.Add("1140200")
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle("1140200"))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chVATAmount.Index).Value).ToString("N2"))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("Input VAT")
                                End If

                                ' CREDIT 
                                lvAccount.Items.Add(row.Cells(dgcAccntCode.Index).Value)
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("     " & GetAccntTitle(row.Cells(dgcAccntCode.Index).Value))
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chGrossAmount.Index).Value).ToString("N2"))
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("GI:" & txtTransNum.Text)

                            ElseIf cbTransType.SelectedItem = "Consigment Item" Then
                                lvAccount.Items.Add(GetItem_AD_Consignment(row.Cells(chItemCode.Index).Value))
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(GetItem_AD_Consignment(row.Cells(chItemCode.Index).Value)))
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chAvgCost.Index).Value * row.Cells(dgcIssueQTY.Index).Value).ToString("N2"))
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")

                                ' CREDIT 
                                lvAccount.Items.Add(row.Cells(dgcAccntCode.Index).Value)
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("     " & GetAccntTitle(row.Cells(dgcAccntCode.Index).Value))
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chAvgCost.Index).Value * row.Cells(dgcIssueQTY.Index).Value).ToString("N2"))
                                lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("GI:" & txtTransNum.Text)

                            Else

                                If row.Cells(chkFree.Index).Value = False Then
                                    ''DEBIT
                                    'lvAccount.Items.Add(debitaccnt)
                                    'lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(debitaccnt))
                                    'lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chAvgCost.Index).Value * row.Cells(dgcIssueQTY.Index).Value).ToString("N2"))
                                    'lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    'lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")

                                    'FOR EPEOPLE DEBIT
                                    lvAccount.Items.Add(debitaccnt)
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(debitaccnt))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chAmount.Index).Value * row.Cells(dgcIssueQTY.Index).Value).ToString("N2"))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")


                                    ' CREDIT 
                                    lvAccount.Items.Add(row.Cells(dgcAccntCode.Index).Value)
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("     " & GetAccntTitle(row.Cells(dgcAccntCode.Index).Value))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chAvgCost.Index).Value * row.Cells(dgcIssueQTY.Index).Value).ToString("N2"))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("GI:" & txtTransNum.Text)

                                    If CDec((row.Cells(chAmount.Index).Value * row.Cells(dgcIssueQTY.Index).Value) - (row.Cells(chAvgCost.Index).Value * row.Cells(dgcIssueQTY.Index).Value)).ToString("N2") <> 0 Then
                                        ' CREDIT OTHER INCOME EPEOPLE
                                        Dim OtherAmount As Decimal
                                        OtherAmount = CDec((row.Cells(chAmount.Index).Value * row.Cells(dgcIssueQTY.Index).Value) - (row.Cells(chAvgCost.Index).Value * row.Cells(dgcIssueQTY.Index).Value)).ToString("N2")
                                        lvAccount.Items.Add("5701000")
                                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("     " & GetAccntTitle("5701000"))
                                        If OtherAmount > 0 Then
                                            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(OtherAmount).ToString("N2"))
                                        Else
                                            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(OtherAmount * -1).ToString("N2"))
                                            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                        End If
                                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("GI:" & txtTransNum.Text)
                                    End If

                                Else
                                    'FREE ITEM
                                    ' DEBIT 
                                    lvAccount.Items.Add(GetExpenseAccount(row.Cells(chItemCode.Index).Value))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(GetExpenseAccount(row.Cells(chItemCode.Index).Value)))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chAvgCost.Index).Value * row.Cells(dgcIssueQTY.Index).Value).ToString("N2"))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("GI:" & txtTransNum.Text)

                                    ' CREDIT 
                                    lvAccount.Items.Add(row.Cells(dgcAccntCode.Index).Value)
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("     " & GetAccntTitle(row.Cells(dgcAccntCode.Index).Value))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chAvgCost.Index).Value * row.Cells(dgcIssueQTY.Index).Value).ToString("N2"))
                                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("GI:" & txtTransNum.Text)

                                End If

                            End If
                        End If
                    Next

                    TotalDBCR()
                End If
            Else
                Msg("Please select Transaction Type!", MsgBoxStyle.Information)
            End If
        End If
    End Sub



    Private Function GetGITypeAccntCode(ByVal Type As String) As String
        Dim query As String
        query = " SELECT DefaultAccount FROM tblGI_Type WHERE Description = @Type AND Status = 'Active' "
        SQL.FlushParams()
        SQL.AddParam("@Type", Type)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("DefaultAccount").ToString
        Else
            Return ""
        End If
    End Function

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
                LoadStock()
                LoadBarCode()
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
        LoadCurrency()
    End Sub

    Private Sub cbIssueTo_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbIssueTo.SelectedIndexChanged
        If disableEvent = False Then
            loadWHto()
        End If
    End Sub

    Private Sub loadWHto()
        If cbIssueTo.SelectedItem = "Warehouse" Then
            txtVCECode.ReadOnly = False
            txtVCEName.ReadOnly = False
            btnSearchVCE.Enabled = True
        ElseIf cbIssueTo.SelectedItem = "Production" Then
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

    Private Sub cbIssuerFrom_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbIssuerFrom.SelectedIndexChanged
        If disableEvent = False Then
            loadWHFrom()
        End If

    End Sub

    Private Sub LoadStock()
        Dim WHSE As String
        If cbWHfrom.SelectedIndex = -1 Then
            WHSE = ""
        Else
            WHSE = GetWHSE(cbWHfrom.SelectedItem)
        End If

        Dim query As String
        Dim itemCode, UOM As String
        Dim StockQTY As Decimal = 0
        Dim IssueQTY As Decimal = 0
        Dim IssuedQTY As Decimal = 0
        Dim ReqQTY As Decimal = 0
        For Each row As DataGridViewRow In dgvItemMaster.Rows
            If Not IsNothing(row.Cells(chItemCode.Index).Value) Then
                itemCode = row.Cells(chItemCode.Index).Value.ToString
                If Not IsNothing(row.Cells(chUOM.Index).Value) Then UOM = row.Cells(chUOM.Index).Value Else UOM = ""
                If Not IsNumeric(row.Cells(dgcReqQTY.Index).Value) Then ReqQTY = 0 Else ReqQTY = CDec(row.Cells(dgcReqQTY.Index).Value)
                If Not IsNumeric(row.Cells(dgcIssuedQTY.Index).Value) Then IssuedQTY = 0 Else IssuedQTY = CDec(row.Cells(dgcIssuedQTY.Index).Value)

                query = "   SELECT	    ISNULL(SUM(EB),0) AS QTY " & _
                        "   FROM		viewItem_StockSerial " & _
                        "   WHERE       ItemCode ='" & itemCode & "' " & _
                        "   AND         WHSE = '" & WHSE & "' " & _
                        "   AND         UOM = '" & UOM & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    StockQTY = SQL.SQLDR("QTY") + ConvertToAltUOM(itemCode, UOM, IssuedQTY)
                    If StockQTY >= ReqQTY Then ' IF AVAILABLE STOCK IS GREATER THAN THE BOM REQUIREMENT THEN ISSUE QTY SHOULD BE THE SAME AS BOM QTY
                        IssueQTY = ReqQTY
                    Else ' IF AVAILABLE STOCK IS LESS THAN THE BOM REQUIREMENT THEN ISSUE QTY SHOULD BE ONLY THE STOCK QTY
                        IssueQTY = StockQTY
                    End If
                End If
                row.Cells(dgcStockQTY.Index).Value = CDec(StockQTY).ToString("N2")
                row.Cells(dgcIssueQTY.Index).Value = CDec(IssueQTY).ToString("N2")

            End If

        Next
        dgvItemMaster.Columns(dgcStockQTY.Index).Visible = True
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

    Private Sub cbWHfrom_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbWHfrom.SelectedIndexChanged
        If MR_ID <> 0 Then
            LoadMRDEtails(MR_ID)
        Else
            LoadStock()
            GenerateEntry()
        End If

    End Sub

    Private Sub tsbCopyPR_Click(sender As System.Object, e As System.EventArgs) Handles tsbCopyPR.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("MR-GI")
        LoadMR(f.transID)
        f.Dispose()
    End Sub

    Private Sub LoadMR(ByVal ID As String)
        Try
            Dim WHSEfrom, WHSEto As String
            Dim query As String
            query = " SELECT TransID, MR_No, WHSE_From, WHSE_To, DateMR, Remarks " & _
                    " FROM   tblMR " & _
                    " WHERE  TransID ='" & ID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                MR_ID = SQL.SQLDR("TransID")
                txtMRRef.Text = SQL.SQLDR("MR_No").ToString
                WHSEfrom = SQL.SQLDR("WHSE_From").ToString
                WHSEto = SQL.SQLDR("WHSE_To").ToString
                cbTransType.SelectedItem = "Material Transfer"
                If Strings.Left(WHSEto, 2) = "PL" Then
                    cbIssuerFrom.SelectedItem = "Production"
                Else
                    cbIssuerFrom.SelectedItem = "Warehouse"
                End If
                If Strings.Left(WHSEto, 2) = "PL" Then
                    cbIssueTo.SelectedItem = "Production"
                Else
                    cbIssueTo.SelectedItem = "Warehouse"
                End If
                loadWHFrom()
                loadWH()
                If Strings.Left(WHSEto, 2) = "PL" Then
                    cbWHfrom.SelectedItem = loadWH("From", "Production", WHSEto)
                Else
                    cbWHfrom.SelectedItem = loadWH("From", "Warehouse", WHSEto)
                End If

                If Strings.Left(WHSEto, 2) = "PL" Then
                    cbWHto.SelectedItem = loadWH("To", "Production", WHSEfrom)
                Else
                    cbWHto.SelectedItem = loadWH("To", "Warehouse", WHSEfrom)
                End If

                'cbWHto.SelectedItem = loadWH("To", "Production", WHSEfrom)

                LoadMRDEtails(MR_ID)
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadMRDEtails(ByVal ID As Integer)
        Dim query, WHSE As String
        WHSE = GetWHSE(cbWHfrom.SelectedItem)

        query = "  SELECT tblMR_Details.ItemCode, ItemName, UOM, Unserved AS QTY, viewMR_Unserved.WHSE , AD_Expense , ID_SC " & _
                "  FROM   tblMR_Details  " & _
                "  INNER JOIN viewMR_Unserved  ON      " & _
                "  tblMR_Details.RecordID = viewMR_Unserved.RecordID   " & _
                "  INNER JOIN tblItem_Master  ON      " & _
                "  tblItem_Master.ItemCode = tblMR_Details.ItemCode   " & _
                "  WHERE  tblMR_Details.TransID ='" & ID & "' AND tblMR_Details.WHSE = '" & WHSE & "' "
        SQL.GetQuery(query)
        dgvItemMaster.Columns(dgcReqQTY.Index).Visible = True
        dgvItemMaster.Rows.Clear()
        Dim ctr As Integer = 0
        Dim ctrGroup As Integer = 0
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                'dgvItemMaster.Rows.Add(row(0).ToString, row(1).ToString, row(2).ToString, _
                '              CDec(row(3)).ToString("N2"), 0, 0, GetWHSEDesc(row(4).ToString))

                dgvItemMaster.Rows.Add("")
                dgvItemMaster.Rows(dgvItemMaster.Rows.Count - 2).Cells(chItemCode.Index).Value = row(0).ToString
                dgvItemMaster.Rows(dgvItemMaster.Rows.Count - 2).Cells(chItemName.Index).Value = row(1).ToString
                dgvItemMaster.Rows(dgvItemMaster.Rows.Count - 2).Cells(chUOM.Index).Value = row(2).ToString
                dgvItemMaster.Rows(dgvItemMaster.Rows.Count - 2).Cells(dgcReqQTY.Index).Value = CDec(row(3)).ToString("N2")
                dgvItemMaster.Rows(dgvItemMaster.Rows.Count - 2).Cells(chWHSE.Index).Value = GetWHSEDesc(row(4).ToString)
                dgvItemMaster.Rows(dgvItemMaster.Rows.Count - 2).Cells(dgcAccntCode.Index).Value = row(5).ToString
                dgvItemMaster.Rows(dgvItemMaster.Rows.Count - 2).Cells(chAmount.Index).Value = CDec(row(6)).ToString("N2")
                dgvItemMaster.Rows(dgvItemMaster.Rows.Count - 2).Cells(chAvgCost.Index).Value = CDec(GetAverageCost(row(0))).ToString("N2")

                LoadWHSE(ctr)
                LoadUOM(row(0).ToString, ctr)
                LoadColor(row(0).ToString, ctr)
                LoadSize(row(0).ToString, ctr)
                ctr += 1
            Next
            loadbarcode()
            LoadStock()
            GenerateEntry()
        End If
    End Sub

    Private Sub LoadJO(ByVal ID As String)
        Try
            Dim query As String

            cbTransType.SelectedItem = "Issuance to Job"

            query = " SELECT  tblBOM_Details.ItemCode, tblBOM_Details.Description, tblBOM_Details.UOM, tblBOM_Details.GrossQTY  " & _
                    " FROM tblBOM INNER JOIN tblBOM_Details " & _
                    " ON tblBOM.TransID = tblBOM_Details.TransID " & _
                    " WHERE JO_Ref ='" & ID & "' "
            SQL.GetQuery(query)
            dgvItemMaster.Columns(dgcReqQTY.Index).Visible = True
            dgvItemMaster.Rows.Clear()
            Dim ctr As Integer = 0
            Dim ctrGroup As Integer = 0
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                txtJORef.Text = ID
                JO_ID = ID
                For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                    dgvItemMaster.Rows.Add(row(0).ToString, "", row(1).ToString, row(2).ToString, _
                              CDec(row(3)).ToString("N2"), 0, 0, "")
                    LoadWHSE(ctr)
                    LoadUOM(row(0).ToString, ctr)
                    ctr += 1
                Next
                LoadBarCode()
                LoadStock()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub cbTransType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbTransType.SelectedIndexChanged
        RefreshTranType()
    End Sub

    Private Sub RefreshTranType()
        cbIssueTo.Enabled = False
        cbWHto.Enabled = False
        lblJORef.Visible = False
        txtJORef.Visible = False
        cbCostCenter.Visible = False
        lblCostCenter.Visible = False
        cbIssuerFrom.Enabled = False
        cbIssueTo.Enabled = False
        If cbTransType.SelectedItem = "Material Transfer" Then
            cbIssueTo.Enabled = True
            cbWHto.Enabled = True
            cbIssuerFrom.Enabled = True
            cbIssueTo.Enabled = True
            lblCostCenter.Text = "Cost Center :"
            txtVCECode.Visible = False
            Label4.Visible = False
            txtVCEName.Visible = False
            btnSearchVCE.Visible = False
            Label24.Visible = False
            cbCurrency.Visible = False
            withConfirm = True
        ElseIf cbTransType.SelectedItem = "Issuance to Job" Then
            lblJORef.Visible = True
            txtJORef.Visible = True
            cbIssuerFrom.Enabled = True
            lblCostCenter.Text = "Cost Center :"
            txtVCECode.Visible = False
            Label4.Visible = False
            txtVCEName.Visible = False
            btnSearchVCE.Visible = False
            Label24.Visible = False
            cbCurrency.Visible = False
            cbWHto.SelectedIndex = -1
            withConfirm = False
        ElseIf cbTransType.SelectedItem = "Disposal" Then
            LoadCostCenter()
            cbCostCenter.Visible = True
            lblCostCenter.Visible = True
            cbIssuerFrom.SelectedItem = "Warehouse"
            lblCostCenter.Text = "Cost Center :"
            txtVCECode.Visible = False
            Label4.Visible = False
            txtVCEName.Visible = False
            btnSearchVCE.Visible = False
            Label24.Visible = False
            cbCurrency.Visible = False
            cbWHto.SelectedIndex = -1
            withConfirm = False
        ElseIf cbTransType.SelectedItem = "Promotional" Then
            LoadCostCenter()
            cbCostCenter.Visible = True
            lblCostCenter.Visible = True
            cbIssuerFrom.SelectedItem = "Warehouse"
            lblCostCenter.Text = "Cost Center :"
            txtVCECode.Visible = False
            Label4.Visible = False
            txtVCEName.Visible = False
            btnSearchVCE.Visible = False
            Label24.Visible = False
            cbCurrency.Visible = False
            cbWHto.SelectedIndex = -1
            withConfirm = False
        ElseIf cbTransType.SelectedItem = "Material Return" Then
            cbWHto.Enabled = True
            cbIssuerFrom.SelectedItem = "Production"
            cbIssueTo.SelectedItem = "Warehouse"
            lblCostCenter.Text = "Cost Center :"
            txtVCECode.Visible = False
            Label4.Visible = False
            txtVCEName.Visible = False
            btnSearchVCE.Visible = False
            Label24.Visible = False
            cbCurrency.Visible = False
            cbWHto.SelectedIndex = -1
            withConfirm = True
        ElseIf cbTransType.SelectedItem = "Issuance to Department" Then
            cbWHto.Enabled = True
            cbIssuerFrom.SelectedItem = "Warehouse"
            cbWHto.Enabled = False
            LoadCostCenter()
            cbCostCenter.Visible = True
            lblCostCenter.Visible = True
            lblCostCenter.Text = "Cost Center :"
            txtVCECode.Visible = False
            Label4.Visible = False
            txtVCEName.Visible = False
            btnSearchVCE.Visible = False
            Label24.Visible = False
            cbCurrency.Visible = False
            cbWHto.SelectedIndex = -1
            withConfirm = False
        Else
            withConfirm = False
            cbWHto.Enabled = True
            cbIssuerFrom.SelectedItem = "Warehouse"
            cbWHto.Enabled = False
            cbIssueTo.Enabled = False
            cbCostCenter.Visible = False
            lblCostCenter.Visible = True
            lblCostCenter.Text = "VCECode :"
            txtVCECode.Visible = True
            Label4.Visible = True
            txtVCEName.Visible = True
            btnSearchVCE.Visible = True
            Label24.Visible = True
            cbCurrency.Visible = True
            cbWHto.SelectedIndex = -1
            GenerateEntry()
        End If
    End Sub

    Private Sub FromJOToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromJOToolStripMenuItem.Click
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

    Private Sub dgvItemMaster_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemMaster.CellContentClick

    End Sub

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("GI", TransID)
        f.Dispose()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem1.Click
        frmGI_Type.Show()
    End Sub

    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged

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

    Private Sub txtATDNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtATDNo.KeyDown
        Try
            If e.KeyValue <> 8 And Not (e.KeyValue >= 48 And e.KeyValue <= 57) And Not (e.KeyCode >= 96 And e.KeyCode <= 105) Then
                e.SuppressKeyPress = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtATDNo_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtATDNo.TextChanged

    End Sub

    Dim eColIndex As Integer = 0

    Private Sub dgvItemMaster_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dgvItemMaster.CurrentCellDirtyStateChanged
        If eColIndex = chkFree.Index And TypeOf (dgvItemMaster.CurrentRow.Cells(chkFree.Index)) Is DataGridViewComboBoxCell Then
            dgvItemMaster.EndEdit()
        End If
    End Sub

    Private Sub ToolStrip1_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

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

    Private Sub FromRRToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromRRToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        If cbIssuerFrom.SelectedIndex = -1 Then
            Msg("Please Select Warehouse!", MsgBoxStyle.Exclamation)
        Else
            f.ShowDialog("RR")
        End If
        cbTransType.SelectedItem = "Purchase Return"
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        txtConversion.Text = ""
        cbCurrency.Items.Clear()
        LoadRR(f.transID)
        f.Dispose()
    End Sub


    Private Sub LoadRR(ByVal ID As String)
        Try
            Dim query As String

            query = " SELECT tblRR.TransID, tblRR_Details.ItemCode,  tblRR_Details.Description, tblRR_Details.UOM, tblRR_Details.QTY,     " & _
                    " 	ISNULL(tblRR_Details.WHSE,'') AS WHSE, tblRR_Details.AccntCode,   	 " & _
                    " 	ISNULL(tblRR_Details.UnitCost,0) AS UnitCost ,     " & _
                    " 	tblRR_Details.UnitCost AS ItemPrice,  tblRR_Details.GrossAmount,     " & _
                    " 		CAST(CASE WHEN tblRR_Details.Discount <> 0 THEN   ( tblRR_Details.Discount/ tblRR_Details.GrossAmount) * 100    " & _
                    " 		ELSE 0 END AS decimal(18,4)) AS DiscountRate,    " & _
                    " 	tblRR_Details.Discount, tblRR_Details.VATAmount, " & _
                    " 	tblRR_Details.NetAmount, tblRR_Details.VATable, tblRR_Details.VatInc, tblRR_Details.DateExpired,  " & _
                    " 	tblRR_Details.LotNo, tblRR_Details.SerialNo ,tblRR.RR_No, tblRR_Details.Size, tblRR_Details.Color   " & _
                    " FROM tblRR_Details     " & _
                    " 	INNER JOIN tblRR ON   " & _
                    " 	tblRR.TransID = tblRR_Details.TransID         " & _
                    " WHERE  tblRR.TransID ='" & ID & "' "
            SQL.GetQuery(query)
            dgvItemMaster.Rows.Clear()
            Dim ctr As Integer = 0
            Dim ctrGroup As Integer = 0
            If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                    txtRemarks.Text = "RR: " & row(16)
                    dgvItemMaster.Rows.Add(row(1).ToString, "", row(2).ToString, row(3).ToString, _
                                  CDec(0).ToString("N2"), CDec(row(4)).ToString("N2"), CDec(row(4)).ToString("N2"), GetWHSEDesc(row(5).ToString), CDec(0).ToString("N2"), _
                                  CDec(row(7)).ToString("N2"), row(6).ToString, CDec(row(7)).ToString("N2"), False, _
                                  CDec(row(9)).ToString("N2"), CDec(row(10)).ToString("N2"), CDec(row(11)).ToString("N2"), CDec(row(12)).ToString("N2"), row(13), row(14), row(15), row(17), row(18))
                    LoadWHSE(ctr)
                    LoadUOM(row(1).ToString, ctr)
                    LoadColor(row(1).ToString, ctr)
                    LoadSize(row(1).ToString, ctr)
                    ctr += 1
                Next
                LoadBarCode()
                LoadStock()
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

    Private Sub FromInventoryCountToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FromInventoryCountToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        If cbIssuerFrom.SelectedIndex = -1 Then
            Msg("Please Select Warehouse!", MsgBoxStyle.Exclamation)
        Else
            f.ShowDialog("IC-GI")
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

            query = " SELECT TransID, viewIC_GI_Unserved.ItemCode, ItemName, viewIC_GI_Unserved.UOM, " & _
                    " 	viewIC_GI_Unserved.IC_QTY, viewIC_GI_Unserved.WHSE, viewIC_GI_Unserved.IC_No " & _
                    "  FROM viewIC_GI_Unserved " & _
                    " LEFT JOIN " & _
                    " tblItem_Master ON " & _
                    " tblItem_Master.ItemCode = viewIC_GI_Unserved.ItemCode " & _
                    " WHERE  viewIC_GI_Unserved.TransID ='" & ID & "' "
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
                    cbWHfrom.SelectedItem = loadWH("From", "Warehouse", WHSETo)
                    If Inv_ComputationMethod = "SC" Then
                        unitcost = GetStandardCost(row(1))
                    Else
                        unitcost = GetAverageCost(row(1))
                    End If
                    txtRemarks.Text = "Inventory Count IC: " & row(6)
                    dgvItemMaster.Rows.Add(row(1).ToString, "", row(2).ToString, row(3).ToString, _
                                  CDec(0).ToString("N2"), CDec(row(4)).ToString("N2"), CDec(row(4)).ToString("N2"), GetWHSEDesc(row(5).ToString), CDec(0).ToString("N2"), _
                                  CDec(unitcost).ToString("N2"), "", CDec(unitcost).ToString("N2"), False, _
                                  CDec(0).ToString("N2"), CDec(0).ToString("N2"), CDec(0).ToString("N2"), CDec(0).ToString("N2"), CDec(0).ToString("N2"), False, False)
                    LoadWHSE(ctr)
                    LoadUOM(row(1).ToString, ctr)
                    ctr += 1
                Next

                LoadBarCode()
                LoadStock()
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
                            ' Check if has valid VCEcode
                            If IsNothing(dgvItemMaster.Item(j, rowCount - 1).Value) OrElse dgvItemMaster.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = dgcReqQTY.Index Then
                            If IsNothing(dgvItemMaster.Item(j, rowCount - 1).Value) OrElse dgvItemMaster.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = dgcStockQTY.Index Then
                            ' Particulars
                            If IsNothing(dgvItemMaster.Item(j, rowCount - 1).Value) OrElse dgvItemMaster.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chRef.Index Then
                            ' REfno
                            If IsNothing(dgvItemMaster.Item(j, rowCount - 1).Value) OrElse dgvItemMaster.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = dgcIssueQTY.Index Then
                            ' dgcIssueQTY
                            If IsNothing(dgvItemMaster.Item(j, rowCount - 1).Value) OrElse dgvItemMaster.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chAvgCost.Index Then
                            ' chAvgCost
                            If IsNothing(dgvItemMaster.Item(j, rowCount - 1).Value) OrElse dgvItemMaster.Item(j, rowCount - 1).Value = "" Then
                                If IsNothing(Obj.value) Then ObjectText = "0.00" Else ObjectText = Obj.value.ToString
                                AddValue(ObjectText, rowCount - 1, j)
                            End If
                        ElseIf j = chAmount.Index Then
                            ' chAmount
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
                    End If
                Else
                    Exit For
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


    Private Delegate Sub AddValueInvoker(ByVal Value As String, ByVal row As Integer, ByVal col As Integer)
    Private Sub AddValue(ByVal Value As String, ByVal row As Integer, ByVal col As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New AddValueInvoker(AddressOf AddValue), Value, row, col)
        Else
            dgvItemMaster.Item(col, row).Value = Value
        End If
    End Sub
    Private Delegate Sub SetPGBmaxInvoker(ByVal Value As String)
    Private Sub SetPGBmax(ByVal Value As String)
        If Me.InvokeRequired Then
            Me.Invoke(New SetPGBmaxInvoker(AddressOf SetPGBmax), Value)
        Else
            pgbCounter.Maximum = Value
            pgbCounter.Value = 0
        End If
    End Sub

    Private Delegate Sub ChangeCellColorInvoker(ByVal row As Integer, ByVal col As Integer)
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
        Dim fileName As String = "GI Uploader.xlsx"
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
            For i As Integer = 0 To 14
                If i = 0 Then
                    xlWorkSheet.Cells(1, i + 1) = templateName
                End If
                xlWorkSheet.Cells(2, i + 1) = dgvItemMaster.Columns(i).Name
                xlWorkSheet.Cells(3, i + 1) = dgvItemMaster.Columns(i).HeaderText
            Next
            xlWorkSheet.Protect(excelPW)
            Dim ctr As Integer = 1
            Do
                fileName = "GI Uploader -" & ctr.ToString & ".xlsx"
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
                End If
            Else
                dgvItemMaster.Columns(chWHSE.Index).Visible = False
            End If
        End If
    End Sub

    Private Sub FromRRToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles FromRRToolStripMenuItem1.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        If cbIssuerFrom.SelectedIndex = -1 Then
            Msg("Please Select Warehouse!", MsgBoxStyle.Exclamation)
        Else
            f.ShowDialog("RR")
        End If
        cbTransType.SelectedItem = "Material Transfer"
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        txtConversion.Text = ""
        cbCurrency.Items.Clear()
        LoadRR(f.transID)
        f.Dispose()
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