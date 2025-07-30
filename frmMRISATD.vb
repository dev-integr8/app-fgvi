Public Class frmMRISATD
    Dim TransID, RefID, JETransID As String
    Dim MRIS_No As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "MRIS"
    Dim ColumnPK As String = "MRIS_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblMRIS"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim SysCode As String = "EMERALD"
    Dim dtATD As New DataTable
    Dim ATDTempID As String = ""
    Dim ATDSaved As Boolean = False
    Dim Emp_ATDID As Dictionary(Of String, String)
    Dim Emp_ATDNo As Dictionary(Of String, String)
    Public Overloads Function ShowDialog(ByVal docnumber As String) As Boolean
        TransID = docnumber
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmMRISATD_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            cbIssuerFrom.Items.Clear()
            cbIssuerFrom.Items.Add("Warehouse")
            If TransID <> "" Then
                LoadMRIS(TransID)
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
        cbIssuerFrom.Enabled = Value
        btnSearchVCE.Enabled = Value
        txtVCEName.Enabled = Value
        dgvItemMaster.AllowUserToAddRows = Value
        dgvItemMaster.AllowUserToDeleteRows = Value
        dgvItemMaster.ReadOnly = Not Value
        cbCurrency.Enabled = Value
        txtConversion.Enabled = Value
        dgvItemMaster.Columns(chATDNo.Index).ReadOnly = True
        If Value = True Then
            dgvItemMaster.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemMaster.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value

        If TransAuto = True Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If

        chAvgCost.ReadOnly = True

    End Sub

    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("MRIS_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("MRIS")
            TransID = f.transID
            LoadMRIS(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub LoadMRIS(ByVal ID As String)
        Dim query, Currency As String
        Dim WHSEfrom As String
        query = " SELECT   TransID, MRIS_No, VCECode, DateMRIS,IssueFrom,  WHSE_From,  Remarks, Status,  Currency,  ISNULL(Exchange_Rate,0) AS Exchange_Rate " & _
                " FROM     tblMRIS " & _
                " WHERE    TransId = '" & ID & "' " & _
                " ORDER BY TransId "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            MRIS_No = SQL.SQLDR("MRIS_No").ToString
            txtTransNum.Text = MRIS_No
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            dtpDocDate.Text = SQL.SQLDR("DateMRIS").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            disableEvent = True
            cbIssuerFrom.SelectedItem = SQL.SQLDR("IssueFrom").ToString
            WHSEfrom = SQL.SQLDR("WHSE_From").ToString
            Currency = SQL.SQLDR("Currency").ToString
            txtConversion.Text = CDec(SQL.SQLDR("Exchange_Rate")).ToString("N4")
            disableEvent = False
            loadWHFrom()

            If cbIssuerFrom.SelectedItem = "Production" Then
                cbWHfrom.SelectedItem = GetWHSE(WHSEfrom, "tblProdWarehouse")
            Else
                cbWHfrom.SelectedItem = GetWHSE(WHSEfrom, "tblWarehouse")
            End If
            txtVCEName.Text = GetVCEName(txtVCECode.Text)


            disableEvent = True
            cbCurrency.Items.Clear()
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

    Private Sub LoadATD(ByVal ATD_No As String, Ledger_Code As String)
        activityStatus = True
        If dtATD.Columns.Count = 0 Then
            dtATD.Columns.Add("ATDID", GetType(Integer))
            dtATD.Columns.Add("ATD_No", GetType(String))
            dtATD.Columns.Add("VCECode", GetType(String))
            dtATD.Columns.Add("Ledger_Code", GetType(String))
            dtATD.Columns.Add("Calc_Method", GetType(String))
            dtATD.Columns.Add("Cutoff", GetType(String))
            dtATD.Columns.Add("Amount_1st", GetType(Decimal))
            dtATD.Columns.Add("Amount_2nd", GetType(Decimal))
            dtATD.Columns.Add("Total_Amount", GetType(Decimal))
            dtATD.Columns.Add("No_of_Payday", GetType(Integer))
            dtATD.Columns.Add("Start_Date", GetType(Date))
            dtATD.Columns.Add("Recurring", GetType(Boolean))
            dtATD.Columns.Add("Remarks", GetType(String))
            dtATD.Columns.Add("ATD_Form_No", GetType(String))
        End If

        Dim query As String
        query = " SELECT    tblATD.TransID, tblATD.ATD_No, tblATD.VCECode, tblATD_Details.Ledger_Code, tblATD_Details.Calc_Method, " & _
                " 		    tblATD_Details.Cutoff, tblATD_Details.Amount_1st, tblATD_Details.Amount_2nd, tblATD_Details.Total_Amount, " & _
                " 		    tblATD_Details.No_of_Payday, tblATD_Details.Start_Date, tblATD_Details.Recurring, tblATD.Remarks, ATD_Form_No " & _
                " FROM	    tblATD INNER JOIN tblATD_Details " & _
                " ON		tblATD.TransID = tblATD_Details.TransID " & _
                " WHERE	    tblATD.ATD_No = @ATDNo AND tblATD_Details.Ledger_Code =@Ledger_Code "
        SQL.FlushParams()
        SQL.AddParam("@ATDNo", ATD_No)
        SQL.AddParam("@Ledger_Code", Ledger_Code)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtATD.Rows.Add()
            dtATD.Rows(dtATD.Rows.Count - 1).Item("ATDID") = SQL.SQLDR("TransID").ToString
            dtATD.Rows(dtATD.Rows.Count - 1).Item("ATD_No") = SQL.SQLDR("ATD_No").ToString
            dtATD.Rows(dtATD.Rows.Count - 1).Item("VCECode") = SQL.SQLDR("VCECode").ToString
            dtATD.Rows(dtATD.Rows.Count - 1).Item("Ledger_Code") = SQL.SQLDR("Ledger_Code").ToString
            dtATD.Rows(dtATD.Rows.Count - 1).Item("Calc_Method") = SQL.SQLDR("Calc_Method").ToString
            dtATD.Rows(dtATD.Rows.Count - 1).Item("Cutoff") = SQL.SQLDR("Cutoff").ToString
            dtATD.Rows(dtATD.Rows.Count - 1).Item("Amount_1st") = SQL.SQLDR("Amount_1st").ToString
            dtATD.Rows(dtATD.Rows.Count - 1).Item("Amount_2nd") = SQL.SQLDR("Amount_2nd").ToString
            dtATD.Rows(dtATD.Rows.Count - 1).Item("Total_Amount") = SQL.SQLDR("Total_Amount").ToString
            dtATD.Rows(dtATD.Rows.Count - 1).Item("No_of_Payday") = SQL.SQLDR("No_of_Payday").ToString
            dtATD.Rows(dtATD.Rows.Count - 1).Item("Start_Date") = SQL.SQLDR("Start_Date").ToString
            dtATD.Rows(dtATD.Rows.Count - 1).Item("Recurring") = SQL.SQLDR("Recurring").ToString
            dtATD.Rows(dtATD.Rows.Count - 1).Item("Remarks") = SQL.SQLDR("Remarks").ToString
            dtATD.Rows(dtATD.Rows.Count - 1).Item("ATD_Form_No") = SQL.SQLDR("ATD_Form_No").ToString
        End If
    End Sub

    Private Sub LoadEntry(ByVal RRNo As Integer)
        Dim query As String
        query = " SELECT ID, JE_No, View_GL.BranchCode, View_GL.AccntCode, AccountTitle, View_GL.VCECode, View_GL.VCEName, Debit, Credit, Particulars, RefNo   " & _
                " FROM   View_GL INNER JOIN tblCOA_Master " & _
                " ON     View_GL.AccntCode = tblCOA_Master.AccountCode " & _
                " WHERE JE_No = (SELECT  JE_No FROM tblJE_Header WHERE RefType = 'MRIS' AND RefTransID = " & RRNo & ") " & _
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
            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(SQL.SQLDR("VCECode").ToString)
            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(SQL.SQLDR("VCEName").ToString)
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

    Protected Sub LoadGIDetails(ByVal TransID As String)
        Dim ctr As Integer = 0
        Dim query As String

        query = "  SELECT	tblMRIS_Details.EmpID, VCEName, tblMRIS_Details.ItemCode, ItemName,  " & _
                "  tblMRIS_Details.UOM, tblMRIS_Details.QTY, tblMRIS_Details.WHSE,  " & _
                "  ISNULL(AvgCost,0) AS AvgCost,  ISNULL(Amount,0) AS Amount ,  tblMRIS_Details.AccntCode, " & _
                "  ISNULL(FreeItem,0) AS FreeItem , ATD, ATDNo, ISNULL(Particulars,'') AS Particulars " & _
                "  FROM	    tblMRIS  " & _
                "  INNER JOIN tblMRIS_Details  ON		 " & _
                "  tblMRIS.TransID = tblMRIS_Details.TransID   " & _
                "  LEFT JOIN viewVCE_Master  ON		 " & _
                "  viewVCE_Master.VCECode = tblMRIS_Details.EmpID   " & _
                "  LEFT JOIN tblItem_Master  ON		 " & _
                "  tblItem_Master.ItemCode = tblMRIS_Details.ItemCode " & _
                "  WHERE     tblMRIS_Details.TransId = " & TransID & " " & _
                "  ORDER BY  tblMRIS_Details.LineNum  "
        disableEvent = True
        dgvItemMaster.Rows.Clear()
        dtATD.Rows.Clear()
        disableEvent = False
        SQL.ReadQuery(query)
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            Dim IssuedQTY As Decimal = 0
            Dim ConvertQTY As Decimal = 0
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                IssuedQTY = CDec(row(5))
                ConvertQTY = ConvertToBaseUOM(row(2).ToString, row(4).ToString, IssuedQTY)

                'dgvItemMaster.Rows.Add(row(0).ToString, row(1).ToString, row(2).ToString, row(3).ToString, _
                '                     row(4).ToString, CDec(row(5)).ToString("N2"), CDec(row(7)).ToString("N2"), CDec(row(8)).ToString("N2"), row(9).ToString, _
                '                     GetWHSEDesc(row(6).ToString), row(10).ToString, row(11).ToString, row(12).ToString)

                dgvItemMaster.Rows.Add(row(0).ToString)
                dgvItemMaster.Rows(ctr).Cells(chEmpName.Index).Value = row(1).ToString
                dgvItemMaster.Rows(ctr).Cells(chItemCode.Index).Value = row(2).ToString
                dgvItemMaster.Rows(ctr).Cells(chItemName.Index).Value = row(3).ToString
                dgvItemMaster.Rows(ctr).Cells(chUOM.Index).Value = row(4).ToString
                dgvItemMaster.Rows(ctr).Cells(dgcQTY.Index).Value = CDec(row(5)).ToString("N2")
                dgvItemMaster.Rows(ctr).Cells(chAvgCost.Index).Value = CDec(row(7)).ToString("N2")
                dgvItemMaster.Rows(ctr).Cells(chAmount.Index).Value = CDec(row(8)).ToString("N2")
                dgvItemMaster.Rows(ctr).Cells(dgcAccntCode.Index).Value = row(9).ToString
                dgvItemMaster.Rows(ctr).Cells(chWHSE.Index).Value = GetWHSEDesc(row(6).ToString)
                dgvItemMaster.Rows(ctr).Cells(chkFree.Index).Value = row(10).ToString
                dgvItemMaster.Rows(ctr).Cells(chATD.Index).Value = row(11).ToString
                dgvItemMaster.Rows(ctr).Cells(chATDNo.Index).Value = row(12).ToString
                dgvItemMaster.Rows(ctr).Cells(chParticulars.Index).Value = row(13).ToString

               
                LoadUOM(row(2).ToString, ctr)
                LoadWHSE(ctr)
                ctr += 1
                LoadATD(row(12).ToString, GetDeductionType(row(2).ToString))
            Next
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
            If cbIssuerFrom.SelectedItem = "Warehouse" Then
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
        dgvItemMaster.Rows.Clear()
        lvAccount.Items.Clear()
        txtRemarks.Clear()
        txtStatus.Text = "Open"
        dtpDocDate.MinDate = GetMaxPEC().AddDays(1)
        dtpDocDate.Value = Date.Today.Date
        cbCurrency.Items.Clear()
        txtConversion.Text = ""
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("MRIS_ADD") Then
            msgRestricted()
        Else
            Cleartext()
            TransID = ""
            MRIS_No = ""

            cbIssuerFrom.SelectedIndex = 0

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
        If Not AllowAccess("MRIS_EDIT") Then
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
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        Try
            If DataValidated() Then
                If TransID = "" Then
                    If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                        TransID = GenerateTransID(ColumnID, DBTable)
                        If TransAuto Then
                            MRIS_No = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                        Else
                            MRIS_No = txtTransNum.Text
                        End If
                        txtTransNum.Text = MRIS_No
                        SaveMRIS()
                        Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                        LoadMRIS(TransID)
                    End If
                Else
                    If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                        If MRIS_No = txtTransNum.Text Then
                            MRIS_No = txtTransNum.Text
                            UpdateMRIS()
                            Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                            MRIS_No = txtTransNum.Text
                            LoadMRIS(TransID)
                        Else
                            If Not IfExist(txtTransNum.Text) Then
                                MRIS_No = txtTransNum.Text
                                UpdateMRIS()
                                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                                MRIS_No = txtTransNum.Text
                                LoadMRIS(TransID)
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
        query = " SELECT * FROM tblMRIS WHERE MRIS_No ='" & ID & "'  AND BranchCode = '" & BranchCode & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function


    Private Function DataValidated() As Boolean
        If cbWHfrom.SelectedIndex = -1 Then
            Msg("Please select warehouse!", MsgBoxStyle.Exclamation)
            Return False
            'ElseIf txtATDNo.Text = "" Then  ' CHECK ATD No
            '    Msg("Please check ATD No.!", MsgBoxStyle.Exclamation)
            '    Return False
        ElseIf dgvItemMaster.Rows.Count <= 1 Then
            Msg("There are no items on the list!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf txtConversion.Visible = True And txtConversion.Text = "" Then
            MsgBox("Please check exchange rate!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf txtVCEName.Visible = True And txtVCECode.Text = "" Then
            Msg("Please insert VCE Name!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf validateDGV() = False Then
            Return False
        Else
            Return True
        End If
        Return True

    End Function

   

    Private Function validateDGV() As Boolean
        Dim stockQTY, issueQTY As Decimal
        Dim free As Boolean
        Dim value As Boolean = True
        For Each row As DataGridViewRow In dgvItemMaster.Rows
            If row.Cells(chItemCode.Index).Value <> "" Then
                If row.Cells(chkFree.Index).Value = "True" Then free = True Else free = False

                If row.Cells(chATDNo.Index).Value = "" And free = False Then  ' CHECK ATD No
                    Msg("Please check ATD No.!", MsgBoxStyle.Exclamation)
                    value = False
                    Exit For
                End If

            End If
        Next

        For Each row As DataGridViewRow In dgvItemSummary.Rows
            If row.Cells(chSUM_ItemCode.Index).Value <> "" Then
                If Not IsNumeric(row.Cells(chSUM_IssuedQTY.Index).Value) Then issueQTY = 0 Else issueQTY = row.Cells(chSUM_IssuedQTY.Index).Value
                If Not IsNumeric(row.Cells(chSUM_StockQTY.Index).Value) Then stockQTY = 0 Else stockQTY = row.Cells(chSUM_StockQTY.Index).Value
                If issueQTY > stockQTY Then
                    Msg("Issue Quantity should not be greater than Stock Quantity", MsgBoxStyle.Exclamation)
                    value = False
                    Exit For
                End If
            End If
        Next
        Return value
    End Function


    Private Sub SaveMRIS()
        Dim SQL1 As New SQLControl
        SQL1.BeginTransaction()
        Try
            Dim WHSEfrom As String

            WHSEfrom = GetWHSE(cbWHfrom.SelectedItem)
            ' SAVE ATD


            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblMRIS(TransID, MRIS_No, BranchCode, BusinessCode, DateMRIS, VCECode, IssueFrom,  WHSE_From,  Remarks,  WhoCreated, Currency, Exchange_Rate) " & _
                        " VALUES (@TransID, @MRIS_No, @BranchCode, @BusinessCode, @DateMRIS, @VCECode, @IssueFrom,  @WHSE_From, @Remarks,  @WhoCreated, @Currency, @Exchange_Rate) "
            SQL1.FlushParams()
            SQL1.AddParam("@TransID", TransID)
            SQL1.AddParam("@MRIS_No", MRIS_No)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
            SQL1.AddParam("@DateMRIS", dtpDocDate.Value.Date)
            SQL1.AddParam("@VCECode", txtVCECode.Text)
            SQL1.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL1.AddParam("@Exchange_Rate", IIf(txtConversion.Text = "", "0.0000", txtConversion.Text))
            SQL1.AddParam("@IssueFrom", cbIssuerFrom.SelectedItem)
            SQL1.AddParam("@WHSE_From", WHSEfrom)
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.ExecNonQuery(insertSQL)

            Dim line As Integer = 1
            Dim EmpID, ItemCode, UOM, AccntCode, ATDTemp, Particulars As String
            Dim ATDNo As String = ""
            Dim issueQTY, UnitCost, Amount As Decimal
            Dim freeitem, ATD As Boolean
            Dim ATDID As Integer = 0
            Emp_ATDID = New Dictionary(Of String, String)
            Emp_ATDNo = New Dictionary(Of String, String)
            For Each row As DataGridViewRow In dgvItemMaster.Rows
                If Not row.Cells(dgcQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    EmpID = IIf(row.Cells(chEmpID.Index).Value = Nothing, "", row.Cells(chEmpID.Index).Value)
                    ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                    UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                    If IsNumeric(row.Cells(dgcQTY.Index).Value) Then issueQTY = CDec(row.Cells(dgcQTY.Index).Value) Else issueQTY = 1
                    AccntCode = IIf(row.Cells(dgcAccntCode.Index).Value = Nothing, "", row.Cells(dgcAccntCode.Index).Value)
                    UnitCost = IIf(row.Cells(chAvgCost.Index).Value = Nothing, "0.00", row.Cells(chAvgCost.Index).Value)
                    Amount = IIf(row.Cells(chAmount.Index).Value = Nothing, "0.00", row.Cells(chAmount.Index).Value)
                    freeitem = row.Cells(chkFree.Index).Value
                    ATD = row.Cells(chATD.Index).Value
                    ATDTemp = IIf(row.Cells(chATDNo.Index).Value = Nothing, "", row.Cells(chATDNo.Index).Value)
                    Particulars = IIf(row.Cells(chParticulars.Index).Value = Nothing, "", row.Cells(chParticulars.Index).Value)
                    If ATDTemp <> "" Then
                        For Each rowATD As DataRow In dtATD.Rows
                            If rowATD.Item("ATD_No") = ATDTemp Then
                                Dim exist As Boolean = False
                                If Emp_ATDID.ContainsKey(rowATD.Item("VCECode")) Then
                                    exist = True
                                End If
                                If Not exist Then
                                    If ATDID = 0 Then
                                        ATDID = GenerateTransID("TransID", "tblATD")
                                    Else
                                        ATDID += 1
                                    End If
                                    If ATDNo = "" Then
                                        ATDNo = GenerateTransNum(True, "ATD", "ATD_No", "tblATD")
                                    Else
                                        Dim a As Integer = CInt(ATDNo) + 1
                                        ATDNo = Strings.Right("000000000" & a.ToString, ATDNo.Length)
                                    End If
                                    Emp_ATDID.Add(rowATD.Item("VCECode"), ATDID)
                                    Emp_ATDNo.Add(rowATD.Item("VCECode"), ATDNo)
                                    insertSQL = " INSERT INTO " & _
                                   " tblATD (TransID, ATD_No, VCECode, DateATD, MRIS_Ref, Remarks, Status, DateCreated, WhoCreated, TransAuto, BranchCode, BusinessCode ) " & _
                                   " VALUES (@TransID, @ATD_No, @VCECode, @DateATD, @MRIS_Ref, @Remarks, @Status,  " & _
                                   "          GETDATE(), @WhoCreated, @TransAuto, @BranchCode, @BusinessCode)"
                                    SQL1.FlushParams()
                                    SQL1.AddParam("@TransID", ATDID)
                                    SQL1.AddParam("@ATD_No", ATDNo)
                                    SQL1.AddParam("@VCECode", rowATD.Item("VCECode"))
                                    SQL1.AddParam("@DateATD", dtpDocDate.Value.Date)
                                    SQL1.AddParam("@MRIS_Ref", TransID)
                                    SQL1.AddParam("@Remarks", rowATD.Item("Remarks"))
                                    SQL1.AddParam("@Status", "Posted")
                                    SQL1.AddParam("@TransAuto", True)
                                    SQL1.AddParam("@WhoCreated", UserID)
                                    SQL1.AddParam("@BranchCode", BranchCode)
                                    SQL1.AddParam("@BusinessCode", BusinessType)
                                    SQL1.ExecNonQuery(insertSQL)
                                Else
                                    ATDID = Emp_ATDID.Item(rowATD.Item("VCECode"))
                                    ATDNo = Emp_ATDNo.Item(rowATD.Item("VCECode"))
                                End If
                                


                                insertSQL = " INSERT INTO " & _
                               " tblATD_Details (TransID, Ledger_Code, Calc_Method, Cutoff, Amount_1st, Amount_2nd, " & _
                               "                Total_Amount, No_of_Payday, Start_Date, Recurring, ATD_Form_No) " & _
                               " VALUES (@TransID, @Ledger_Code, @Calc_Method, @Cutoff, @Amount_1st, @Amount_2nd, " & _
                               "         @Total_Amount, @No_of_Payday, @Start_Date, @Recurring, @ATD_Form_No)"
                                SQL1.FlushParams()
                                SQL1.AddParam("@TransID", ATDID)
                                SQL1.AddParam("@Ledger_Code", rowATD.Item("Ledger_Code"))
                                SQL1.AddParam("@Calc_Method", rowATD.Item("Calc_Method"))
                                SQL1.AddParam("@Cutoff", rowATD.Item("Cutoff"))
                                SQL1.AddParam("@Amount_1st", CDec(rowATD.Item("Amount_1st")))
                                SQL1.AddParam("@Amount_2nd", CDec(rowATD.Item("Amount_2nd")))
                                SQL1.AddParam("@Total_Amount", CDec(rowATD.Item("Total_Amount")))
                                SQL1.AddParam("@No_of_Payday", CInt(rowATD.Item("No_of_Payday")))
                                SQL1.AddParam("@Start_Date", CDate(rowATD.Item("Start_Date")))
                                SQL1.AddParam("@Recurring", rowATD.Item("Recurring"))
                                SQL1.AddParam("@ATD_Form_No", rowATD.Item("ATD_Form_No"))
                                SQL1.ExecNonQuery(insertSQL)
                                Exit For
                            End If
                        Next

                    End If
                    insertSQL = " INSERT INTO " & _
                         " tblMRIS_Details(TransId, EmpID, ItemCode,  UOM,  QTY, WHSE, AccntCode, Amount, FreeItem, ATD, ATDNo, LineNum, AvgCost, WhoCreated, Particulars ) " & _
                         " VALUES(@TransId, @EmpID, @ItemCode,  @UOM,  @QTY, @WHSE, @AccntCode, @Amount, @FreeItem, @ATD, @ATDNo,  @LineNum, @AvgCost, @WhoCreated, @Particulars) "
                    SQL1.FlushParams()
                    SQL1.AddParam("@TransID", TransID)
                    SQL1.AddParam("@EmpID", EmpID)
                    SQL1.AddParam("@ItemCode", ItemCode)
                    SQL1.AddParam("@UOM", UOM)
                    SQL1.AddParam("@Particulars", Particulars)
                    SQL1.AddParam("@QTY", issueQTY)
                    SQL1.AddParam("@WHSE", WHSEfrom)
                    SQL1.AddParam("@AccntCode", AccntCode)
                    SQL1.AddParam("@AvgCost", UnitCost)
                    SQL1.AddParam("@Amount", Amount)
                    SQL1.AddParam("@FreeItem", freeitem)
                    SQL1.AddParam("@ATD", ATD)
                    SQL1.AddParam("@ATDNo", ATDNo)
                    SQL1.AddParam("@LineNum", line)
                    SQL1.AddParam("@WhoCreated", UserID)
                    SQL1.ExecNonQuery(insertSQL)
                    line += 1


                    SaveINVTY("OUT", ModuleID, "MRIS", TransID, dtpDocDate.Value.Date, ItemCode, WHSEfrom, issueQTY, UOM, UnitCost)


                End If
            Next

            ComputeWAUC("MRIS", TransID)

            line = 1
            JETransID = GenerateTransID("JE_No", "tblJE_Header")

            insertSQL = " INSERT INTO " & _
                       " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Currency, Exchange_Rate, Remarks,  WhoCreated) " & _
                       " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR,  @Currency, @Exchange_Rate, @Remarks,  @WhoCreated)"
            SQL1.FlushParams()
            SQL1.AddParam("@JE_No", JETransID)
            SQL1.AddParam("@AppDate", dtpDocDate.Value.Date)
            SQL1.AddParam("@RefType", "MRIS")
            SQL1.AddParam("@RefTransID", TransID)
            SQL1.AddParam("@Book", "Inventory")
            SQL1.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
            SQL1.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL1.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL1.AddParam("@Remarks", txtRemarks.Text)
            SQL1.AddParam("@BranchCode", BranchCode)
            SQL1.AddParam("@BusinessCode", BusinessType)
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
                    SQL1.AddParam("@VCECode", item.SubItems(chEntryVCECode.Index).Text)
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
            SQL1.Commit()
        Catch ex As Exception
            SQL1.Rollback()
            TransID = ""
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "MRIS_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL1.FlushParams()
        End Try
    End Sub

    Private Sub UpdateMRIS()
        Dim SQL As New SQLControl
        SQL.BeginTransaction()
        Try
            Dim WHSEfrom As String
            WHSEfrom = GetWHSE(cbWHfrom.SelectedItem)
            activityStatus = True
            Dim insertSQL, updateSQL, deleteSQL As String
            updateSQL = " UPDATE    tblMRIS " & _
                        " SET       MRIS_No = @MRIS_No, BranchCode = @BranchCode, BusinessCode = @BusinessCode, DateMRIS = @DateMRIS, VCECode = @VCECode, " & _
                        "           IssueFrom = @IssueFrom, WHSE_From = @WHSE_From,  Remarks = @Remarks, " & _
                        "            WhoModified = @WhoModified, DateModified = GETDATE(), " & _
                        "           Currency = @Currency, Exchange_Rate = @Exchange_Rate " & _
                        " WHERE     TransID = @TransID  "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@MRIS_No", MRIS_No)
            SQL.AddParam("@BranchCode", BranchCode)
            SQL.AddParam("@BusinessCode", BusinessType)
            SQL.AddParam("@DateMRIS", dtpDocDate.Value.Date)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@Currency", cbCurrency.SelectedItem)
            SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
            SQL.AddParam("@IssueFrom", cbIssuerFrom.SelectedItem)
            SQL.AddParam("@WHSE_From", WHSEfrom)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)

            deleteSQL = " DELETE FROM tblMRIS_Details WHERE TransID =@TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.ExecNonQuery(deleteSQL)

            DELINVTY(ModuleID, "MRIS", TransID)

            Dim line As Integer = 1
            Dim EmpID, ItemCode, UOM, AccntCode, ATDNo, Particulars As String
            Dim issueQTY, UnitCost, Amount As Decimal
            Dim freeitem, ATD As Boolean
            Dim ATDID As Integer = 0
            For Each row As DataGridViewRow In dgvItemMaster.Rows
                If Not row.Cells(dgcQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                    EmpID = IIf(row.Cells(chEmpID.Index).Value = Nothing, "", row.Cells(chEmpID.Index).Value)
                    ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                    UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                    If IsNumeric(row.Cells(dgcQTY.Index).Value) Then issueQTY = CDec(row.Cells(dgcQTY.Index).Value) Else issueQTY = 1
                    AccntCode = IIf(row.Cells(dgcAccntCode.Index).Value = Nothing, "", row.Cells(dgcAccntCode.Index).Value)
                    UnitCost = IIf(row.Cells(chAvgCost.Index).Value = Nothing, "0.00", row.Cells(chAvgCost.Index).Value)
                    'EPEOPLE
                    Amount = IIf(row.Cells(chAmount.Index).Value = Nothing, "0.00", row.Cells(chAmount.Index).Value)
                    freeitem = row.Cells(chkFree.Index).Value
                    ATDNo = IIf(row.Cells(chATDNo.Index).Value = Nothing, "", row.Cells(chATDNo.Index).Value)
                    ATD = row.Cells(chATD.Index).Value
                    Particulars = IIf(row.Cells(chParticulars.Index).Value = Nothing, "", row.Cells(chParticulars.Index).Value)
                    If ATDNo <> "" Then
                        If ATDNo.Contains("Temp") Then
                            For Each rowATD As DataRow In dtATD.Rows
                                If rowATD.Item("ATD_No") = ATDNo Then
                                    Dim exist As Boolean = False
                                    If Emp_ATDID.ContainsKey(rowATD.Item("VCECode")) Then
                                        exist = True
                                    End If
                                    If Not exist Then
                                        If ATDID = 0 Then
                                            ATDID = GenerateTransID("TransID", "tblATD")
                                        Else
                                            ATDID += 1
                                        End If
                                        If ATDNo = "" Then
                                            ATDNo = GenerateTransNum(True, "ATD", "ATD_No", "tblATD")
                                        Else
                                            Dim a As Integer = CInt(ATDNo) + 1
                                            ATDNo = Strings.Right("000000000" & a.ToString, ATDNo.Length)
                                        End If
                                        Emp_ATDID.Add(rowATD.Item("VCECode"), ATDID)
                                        Emp_ATDNo.Add(rowATD.Item("VCECode"), ATDNo)
                                        insertSQL = " INSERT INTO " & _
                                       " tblATD (TransID, ATD_No, VCECode, DateATD, MRIS_Ref, Remarks, Status, DateCreated, WhoCreated, TransAuto, BranchCode, BusinessCode ) " & _
                                       " VALUES (@TransID, @ATD_No, @VCECode, @DateATD, @MRIS_Ref, @Remarks, @Status,  " & _
                                       "          GETDATE(), @WhoCreated, @TransAuto, @BranchCode, @BusinessCode)"
                                        SQL.FlushParams()
                                        SQL.AddParam("@TransID", ATDID)
                                        SQL.AddParam("@ATD_No", ATDNo)
                                        SQL.AddParam("@VCECode", rowATD.Item("VCECode"))
                                        SQL.AddParam("@DateATD", dtpDocDate.Value.Date)
                                        SQL.AddParam("@MRIS_Ref", TransID)
                                        SQL.AddParam("@Remarks", rowATD.Item("Remarks"))
                                        SQL.AddParam("@Status", "Posted")
                                        SQL.AddParam("@TransAuto", True)
                                        SQL.AddParam("@WhoCreated", UserID)
                                        SQL.AddParam("@BranchCode", BranchCode)
                                        SQL.AddParam("@BusinessCode", BusinessType)
                                        SQL.ExecNonQuery(insertSQL)
                                    Else
                                        ATDID = Emp_ATDID.Item(rowATD.Item("VCECode"))
                                        ATDNo = Emp_ATDNo.Item(rowATD.Item("VCECode"))
                                    End If



                                    insertSQL = " INSERT INTO " & _
                                   " tblATD_Details (TransID, Ledger_Code, Calc_Method, Cutoff, Amount_1st, Amount_2nd, Total_Amount, No_of_Payday, Start_Date, Recurring, ATD_Form_No ) " & _
                                   " VALUES (@TransID, @Ledger_Code, @Calc_Method, @Cutoff, @Amount_1st, @Amount_2nd, " & _
                                   "         @Total_Amount, @No_of_Payday, @Start_Date, @Recurring, @ATD_Form_No)"
                                    SQL.FlushParams()
                                    SQL.AddParam("@TransID", ATDID)
                                    SQL.AddParam("@Ledger_Code", rowATD.Item("Ledger_Code"))
                                    SQL.AddParam("@Calc_Method", rowATD.Item("Calc_Method"))
                                    SQL.AddParam("@Cutoff", rowATD.Item("Cutoff"))
                                    SQL.AddParam("@Amount_1st", CDec(rowATD.Item("Amount_1st")))
                                    SQL.AddParam("@Amount_2nd", CDec(rowATD.Item("Amount_2nd")))
                                    SQL.AddParam("@Total_Amount", CDec(rowATD.Item("Total_Amount")))
                                    SQL.AddParam("@No_of_Payday", CInt(rowATD.Item("No_of_Payday")))
                                    SQL.AddParam("@Start_Date", CDate(rowATD.Item("Start_Date")))
                                    SQL.AddParam("@Recurring", rowATD.Item("Recurring"))
                                    SQL.AddParam("@ATD_Form_No", rowATD.Item("ATD_Form_No"))
                                    SQL.ExecNonQuery(insertSQL)
                                    Exit For
                                End If
                            Next
                        Else
                            For Each rowATD As DataRow In dtATD.Rows
                                If rowATD.Item("ATD_No") = ATDNo AndAlso rowATD.Item("Ledger_Code") = GetDeductionType(ItemCode) Then
                                    updateSQL = " UPDATE    tblATD_Details " & _
                                                " SET       Calc_Method = @Calc_Method, Cutoff = @Cutoff, " & _
                                                "           Amount_1st = @Amount_1st, Amount_2nd = @Amount_2nd, Total_Amount = @Total_Amount, " & _
                                                "           No_of_Payday = @No_of_Payday, Start_Date = @Start_Date, Recurring = @Recurring, ATD_Form_No = @ATD_Form_No  " & _
                                                " WHERE     TransID = @TransID AND Ledger_Code = @Ledger_Code "
                                    SQL.FlushParams()
                                    SQL.AddParam("@TransID", rowATD.Item("ATDID"))
                                    SQL.AddParam("@Ledger_Code", rowATD.Item("Ledger_Code"))
                                    SQL.AddParam("@Calc_Method", rowATD.Item("Calc_Method"))
                                    SQL.AddParam("@Cutoff", rowATD.Item("Cutoff"))
                                    SQL.AddParam("@Amount_1st", CDec(rowATD.Item("Amount_1st")))
                                    SQL.AddParam("@Amount_2nd", CDec(rowATD.Item("Amount_2nd")))
                                    SQL.AddParam("@Total_Amount", CDec(rowATD.Item("Total_Amount")))
                                    SQL.AddParam("@No_of_Payday", CInt(rowATD.Item("No_of_Payday")))
                                    SQL.AddParam("@Start_Date", CDate(rowATD.Item("Start_Date")))
                                    SQL.AddParam("@Recurring", rowATD.Item("Recurring"))
                                    SQL.AddParam("@ATD_Form_No", rowATD.Item("ATD_Form_No"))
                                    SQL.ExecNonQuery(updateSQL)
                                    Exit For
                                End If
                            Next
                        End If
                        

                    End If


                    insertSQL = " INSERT INTO " & _
                         " tblMRIS_Details(TransId, EmpID, ItemCode,  UOM,  QTY, WHSE, AccntCode, Amount, FreeItem, ATD, ATDNo, LineNum, AvgCost, WhoCreated, Particulars ) " & _
                         " VALUES(@TransId, @EmpID, @ItemCode,  @UOM,  @QTY, @WHSE, @AccntCode, @Amount, @FreeItem, @ATD, @ATDNo,  @LineNum, @AvgCost, @WhoCreated, @Particulars) "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", TransID)
                    SQL.AddParam("@EmpID", EmpID)
                    SQL.AddParam("@ItemCode", ItemCode)
                    SQL.AddParam("@UOM", UOM)
                    SQL.AddParam("@Particulars", Particulars)
                    SQL.AddParam("@QTY", issueQTY)
                    SQL.AddParam("@WHSE", WHSEfrom)
                    SQL.AddParam("@AccntCode", AccntCode)
                    SQL.AddParam("@AvgCost", UnitCost)
                    SQL.AddParam("@Amount", Amount)
                    SQL.AddParam("@FreeItem", freeitem)
                    SQL.AddParam("@ATD", ATD)
                    SQL.AddParam("@ATDNo", ATDNo)
                    SQL.AddParam("@LineNum", line)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1


                    SaveINVTY("OUT", ModuleID, "MRIS", TransID, dtpDocDate.Value.Date, ItemCode, WHSEfrom, issueQTY, UOM, UnitCost)


                End If
            Next

            ComputeWAUC("MRIS", TransID)

            line = 1
            JETransID = LoadJE("MRIS", TransID)

            ' UPDATE ENTRIES
            If JETransID = 0 Then
                JETransID = GenerateTransID("JE_No", "tblJE_Header")

                insertSQL = " INSERT INTO " & _
                    " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, Currency, Exchange_Rate, WhoCreated) " & _
                    " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @Currency, @Exchange_Rate, @WhoCreated)"
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransID)
                SQL.AddParam("@AppDate", dtpDocDate.Value.Date)
                SQL.AddParam("@RefType", "MRIS")
                SQL.AddParam("@RefTransID", TransID)
                SQL.AddParam("@Book", "Inventory")
                SQL.AddParam("@TotalDBCR", CDec(txtTotalCredit.Text))
                SQL.AddParam("@Currency", cbCurrency.SelectedItem)
                SQL.AddParam("@Exchange_Rate", CDec(IIf(txtConversion.Text = "", "0.0000", txtConversion.Text)).ToString("N4"))
                SQL.AddParam("@Remarks", txtRemarks.Text)
                SQL.AddParam("@BranchCode", BranchCode)
                SQL.AddParam("@BusinessCode", BusinessType)
                SQL.AddParam("@WhoCreated", UserID)
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
                SQL.AddParam("@RefType", "MRIS")
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
                    SQL.AddParam("@VCECode", item.SubItems(chEntryVCECode.Index).Text)
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
                    SQL.AddParam("@RefNo", item.SubItems(chRef.Index).Text)
                    SQL.AddParam("@LineNumber", line)
                    SQL.AddParam("@BranchCode", BranchCode)
                    SQL.ExecNonQuery(insertSQL)
                    line += 1
                End If
            Next
            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "MRIS_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub tsbCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("MRIS_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL, updateSQL As String
                        deleteSQL = " UPDATE  tblMRIS SET Status ='Cancelled' WHERE MRIS_No = @MRIS_No "
                        SQL.FlushParams()
                        MRIS_No = txtTransNum.Text
                        SQL.AddParam("@MRIS_No", MRIS_No)
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
                            If Not row.Cells(dgcQTY.Index).Value Is Nothing AndAlso Not row.Cells(chItemCode.Index).Value Is Nothing Then
                                ItemCode = IIf(row.Cells(chItemCode.Index).Value = Nothing, "", row.Cells(chItemCode.Index).Value)
                                Description = IIf(row.Cells(chItemName.Index).Value = Nothing, "", row.Cells(chItemName.Index).Value)
                                UOM = IIf(row.Cells(chUOM.Index).Value = Nothing, "", row.Cells(chUOM.Index).Value)
                                If Inv_ComputationMethod = "SC" Then
                                    UnitCost = GetStandardCost(ItemCode)
                                Else
                                    UnitCost = GetAverageCost(ItemCode)
                                End If
                                If IsNumeric(row.Cells(dgcQTY.Index).Value) Then issueQTY = CDec(row.Cells(dgcQTY.Index).Value) Else issueQTY = 1
                                line += 1

                                SaveINVTY("IN", ModuleID, "MRIS", TransID, dtpDocDate.Value.Date, ItemCode, WHSEfrom, issueQTY, UOM, UnitCost, "Active")

                            End If
                        Next
                        ComputeWAUC("MRIS", TransID)

                        JETransID = LoadJE("MRIS", TransID)
                        updateSQL = " UPDATE tblJE_Header " & _
                          " SET    Status = @Status,  WhoModified = @WhoModified, DateModified = GETDATE()" & _
                          " WHERE  JE_No = @JE_No "
                        SQL.FlushParams()
                        SQL.AddParam("@JE_No", JETransID)
                        SQL.AddParam("@Status", "Cancelled")
                        SQL.AddParam("@WhoModified", UserID)
                        SQL.ExecNonQuery(updateSQL)

                        LoadMRIS(TransID)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "MRIS_No", MRIS_No, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If MRIS_No <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID " & _
                    "  FROM     tblMRIS LEFT JOIN viewVCE_Master " & _
                    "  ON	   tblMRIS .VCECode = viewVCE_Master.VCECode " & _
                    "   LEFT JOIN tblProdWarehouse  ON	          " & _
                    "   tblMRIS.WHSE_From = tblProdWarehouse.Code    " & _
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
                    "  	 WHERE     UserID ='" & UserID & "'))  AND MRIS_No < '" & MRIS_No & "' ORDER BY MRIS_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadMRIS(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If MRIS_No <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID " & _
                    "  FROM     tblMRIS LEFT JOIN viewVCE_Master " & _
                    "  ON	   tblMRIS .VCECode = viewVCE_Master.VCECode " & _
                    "   LEFT JOIN tblProdWarehouse  ON	          " & _
                    "   tblMRIS.WHSE_From = tblProdWarehouse.Code    " & _
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
                    "  	 WHERE     UserID ='" & UserID & "'))  AND MRIS_No > '" & MRIS_No & "' ORDER BY MRIS_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadMRIS(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If MRIS_No = "" Then
            Cleartext()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadMRIS(TransID)
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
            f.ModFrom = "Customer"
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
            Dim itemCode As String
            Dim rowIndex As Integer = dgvItemMaster.CurrentCell.RowIndex
            Dim colindex As Integer = dgvItemMaster.CurrentCell.ColumnIndex
            Select Case colindex
                Case chEmpID.Index Or chEmpName.Index
                    If txtVCECode.Text <> "" Then
                        Dim f As New frmVCE_Search
                        f.CustomerCode = txtVCECode.Text
                        f.ModFrom = "CustomerFilter"
                        f.txtFilter.Text = dgvItemMaster.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                        f.ShowDialog()
                        dgvItemMaster.Item(chEmpID.Index, e.RowIndex).Value = f.VCECode
                        dgvItemMaster.Item(chEmpName.Index, e.RowIndex).Value = f.VCEName
                        f.Dispose()
                    Else
                        Msg("Please enter Client Name!", MsgBoxStyle.Exclamation)
                        txtVCEName.Focus()
                    End If
                Case chItemCode.Index
                    If dgvItemMaster.Item(chEmpID.Index, e.RowIndex).Value <> "" Then
                        If dgvItemMaster.Item(chItemCode.Index, e.RowIndex).Value <> "" Then
                            itemCode = dgvItemMaster.Item(chItemCode.Index, e.RowIndex).Value
                            Dim f As New frmCopyFrom
                            f.ShowDialog("All Item", itemCode)
                            If f.TransID <> "" Then
                                itemCode = f.TransID
                                LoadItemDetails(itemCode)
                            End If
                            f.Dispose()
                        End If
                        LoadItemSummary()
                        GenerateEntry()
                    Else
                        Msg("Please enter Employee!", MsgBoxStyle.Exclamation)
                    End If
                Case chItemName.Index
                    If dgvItemMaster.Item(chEmpID.Index, e.RowIndex).Value <> "" Then
                        If dgvItemMaster.Item(chItemName.Index, e.RowIndex).Value <> "" Then
                            itemCode = dgvItemMaster.Item(chItemName.Index, e.RowIndex).Value
                            Dim f As New frmCopyFrom
                            f.ShowDialog("All Item", itemCode)
                            If f.TransID <> "" Then
                                itemCode = f.TransID
                                LoadItemDetails(itemCode)
                            End If
                            f.Dispose()
                        End If
                        LoadItemSummary()
                        GenerateEntry()
                    Else
                        Msg("Please enter Employee!", MsgBoxStyle.Exclamation)
                    End If
                Case dgcQTY.Index
                    LoadItemSummary()
                    GenerateEntry()
                Case chUOM.Index
                    GenerateEntry()
                    'EPEOPLE
                Case chAmount.Index
                    If dgvItemMaster.Item(chATDNo.Index, e.RowIndex).Value <> "" Then
                        For Each row As DataRow In dtATD.Rows
                            If row.Item("ATD_No").ToString = dgvItemMaster.Item(chATDNo.Index, e.RowIndex).Value Then
                                row.Item("Total_Amount") = dgvItemMaster.Item(chAmount.Index, e.RowIndex).Value
                                row.Item("Amount_1st") = row.Item("Total_Amount") / row.Item("No_of_Payday")
                            End If
                        Next
                    End If
                    GenerateEntry()
                Case chkFree.Index
                    GenerateEntry()
                Case chATD.Index
                    If dgvItemMaster.Item(chATDNo.Index, e.RowIndex).Value = "" Then
                        dgvItemMaster.Item(chkFree.Index, e.RowIndex).Value = True
                        dgvItemMaster.Item(chATD.Index, e.RowIndex).Value = False
                    End If
                    GenerateEntry()

            End Select
        Catch ex1 As InvalidOperationException

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try

    End Sub

    Private Sub LoadItemDetails(ByVal ItemCode As String)
        Dim query As String
        Dim unitcost As Decimal
        query = " SELECT    tblItem_Master.ItemCode, ItemName, 1, AD_Inv , ISNULL(AverageCost,ID_SC) AS AverageCost, ID_SC, ItemUOM " & _
                " FROM       tblItem_Master " & _
                "  LEFT JOIN " & _
                "   ( SELECT    TOP 1  ItemCode, AverageCost  " & _
                "                  FROM      tblInventory  " & _
                " 				   WHERE     tblInventory.ItemCode = @ItemCode " & _
                "                  ORDER BY  PostDate DESC, DateCreated DESC ) AS AvgCost " & _
                " 				 ON AvgCost.ItemCode = tblItem_Master.ItemCode " & _
                " WHERE     tblItem_Master.ItemCode = @ItemCode"
        SQL.FlushParams()
        SQL.AddParam("@ItemCode", IIf(ItemCode = Nothing, "", ItemCode))
        SQL.ReadQuery(query)
        If Inv_ComputationMethod = "SC" Then
            unitcost = GetStandardCost(ItemCode)
        Else
            unitcost = GetAverageCost(ItemCode)
        End If
        If SQL.SQLDR.Read Then
            If dgvItemMaster.SelectedCells.Count > 0 Then
                dgvItemMaster.Item(chItemCode.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemCode").ToString
                dgvItemMaster.Item(chItemName.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemName").ToString
                dgvItemMaster.Item(chUOM.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemUOM").ToString
                dgvItemMaster.Item(dgcQTY.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = 1
                dgvItemMaster.Item(chWHSE.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = IIf(cbWHfrom.SelectedIndex = -1, "", cbWHfrom.SelectedItem)
                dgvItemMaster.Item(dgcAccntCode.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("AD_Inv").ToString
                dgvItemMaster.Item(chAvgCost.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = CDec(unitcost).ToString("N2")
                'Epeople
                dgvItemMaster.Item(chAmount.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = CDec(SQL.SQLDR("ID_SC")).ToString("N2")
                dgvItemMaster.Item(chkFree.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = True
                dgvItemMaster.Item(chATD.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = False
                dgvItemMaster.Item(chATDNo.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = ""
                LoadWHSE(dgvItemMaster.SelectedCells(0).RowIndex)
                LoadUOM(ItemCode, dgvItemMaster.SelectedCells(0).RowIndex)

            End If

        End If
    End Sub

    Private Sub LoadItemSummary()
        Dim ItemCodeDetails, itemUOM As String
        dgvItemSummary.Rows.Clear()

        'check summary datagrid
        For Each row As DataGridViewRow In dgvItemMaster.Rows
            If row.Cells(chItemCode.Index).Value <> "" Then
                ItemCodeDetails = row.Cells(chItemCode.Index).Value
                itemUOM = row.Cells(chUOM.Index).Value
                Dim ItemInSummary As Boolean = False
                For Each rows As DataGridViewRow In dgvItemSummary.Rows
                    If rows.Cells(chSUM_ItemCode.Index).Value <> "" Then
                        If ItemCodeDetails = rows.Cells(chSUM_ItemCode.Index).Value Then
                            ItemInSummary = True
                        End If
                    End If
                Next
                If ItemInSummary = False Then
                    Dim query As String
                    query = " SELECT    tblItem_Master.ItemCode, ItemName, 1, AD_Expense , ISNULL(AverageCost,ID_SC) AS AverageCost, ID_SC, ItemUOM " & _
                " FROM       tblItem_Master " & _
                "  LEFT JOIN " & _
                "   ( SELECT    TOP 1  ItemCode, AverageCost  " & _
                "                  FROM      tblInventory  " & _
                " 				   WHERE     tblInventory.ItemCode = @ItemCode " & _
                "                  ORDER BY  PostDate DESC, DateCreated DESC ) AS AvgCost " & _
                " 				 ON AvgCost.ItemCode = tblItem_Master.ItemCode " & _
                " WHERE     tblItem_Master.ItemCode = @ItemCode"
                    SQL.FlushParams()
                    SQL.AddParam("@ItemCode", IIf(ItemCodeDetails = Nothing, "", ItemCodeDetails))
                    SQL.ReadQuery(query)
                    If SQL.SQLDR.Read Then
                        dgvItemSummary.Rows.Add("")
                        dgvItemSummary.Rows(dgvItemSummary.Rows.Count - 1).Cells(chSUM_ItemCode.Index).Value = SQL.SQLDR("ItemCode").ToString
                        dgvItemSummary.Rows(dgvItemSummary.Rows.Count - 1).Cells(chSUM_ItemName.Index).Value = SQL.SQLDR("ItemName").ToString
                        dgvItemSummary.Rows(dgvItemSummary.Rows.Count - 1).Cells(chSUM_UOM.Index).Value = SQL.SQLDR("ItemUOM").ToString
                        LoadStock()

                    End If

                    
                End If
            End If
        Next

        'update req qty in summary datagrid

        For Each row As DataGridViewRow In dgvItemMaster.Rows
            If row.Cells(chItemCode.Index).Value <> "" Then
                ItemCodeDetails = row.Cells(chItemCode.Index).Value
                itemUOM = row.Cells(chUOM.Index).Value
                Dim ItemInSummary As Boolean = False
                'debit compute & print in textbox
                Dim a As Double = 0
                For i As Integer = 0 To dgvItemMaster.Rows.Count - 1
                    If dgvItemMaster.Item(chItemCode.Index, i).Value <> "" Then
                        If dgvItemMaster.Item(chItemCode.Index, i).Value = ItemCodeDetails Then
                            If dgvItemMaster.Item(chItemCode.Index, i).Value <> "" AndAlso Val(dgvItemMaster.Item(dgcQTY.Index, i).Value) <> 0 Then
                                a = a + Double.Parse(dgvItemMaster.Item(dgcQTY.Index, i).Value).ToString("N2")
                            End If
                        End If
                    End If
                Next

                For i As Integer = 0 To dgvItemSummary.Rows.Count - 1
                    If dgvItemSummary.Item(chSUM_ItemCode.Index, i).Value <> "" Then
                        If ItemCodeDetails = dgvItemSummary.Item(chSUM_ItemCode.Index, i).Value Then
                            dgvItemSummary.Item(chSUM_IssuedQTY.Index, i).Value = a.ToString("N2")
                        End If
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub GenerateEntry()

        lvAccount.Items.Clear()
        For Each row As DataGridViewRow In dgvItemMaster.Rows
            If IsNumeric(row.Cells(chAvgCost.Index).Value) AndAlso Not IsNothing(row.Cells(dgcAccntCode.Index).Value) Then
                If row.Cells(chkFree.Index).Value = False Then
                    ''DEBIT
                    'lvAccount.Items.Add(debitaccnt)
                    'lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(debitaccnt))
                    'lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chAvgCost.Index).Value * row.Cells(dgcIssueQTY.Index).Value).ToString("N2"))
                    'lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    'lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")

                    'FOR EPEOPLE DEBIT
                    lvAccount.Items.Add(GetATDAccount(row.Cells(chItemCode.Index).Value))
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(GetATDAccount(row.Cells(chItemCode.Index).Value)))
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chAmount.Index).Value * row.Cells(dgcQTY.Index).Value).ToString("N2"))
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chEmpID.Index).Value)
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chEmpName.Index).Value)

                    ' CREDIT 
                    lvAccount.Items.Add(row.Cells(dgcAccntCode.Index).Value)
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("     " & GetAccntTitle(row.Cells(dgcAccntCode.Index).Value))
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chAvgCost.Index).Value * row.Cells(dgcQTY.Index).Value).ToString("N2"))
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("MRIS:" & txtTransNum.Text)
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chEmpID.Index).Value)
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chEmpName.Index).Value)

                    If CDec((row.Cells(chAmount.Index).Value * row.Cells(dgcQTY.Index).Value) - (row.Cells(chAvgCost.Index).Value * row.Cells(dgcQTY.Index).Value)).ToString("N2") <> 0 Then
                        ' CREDIT OTHER INCOME EPEOPLE
                        lvAccount.Items.Add("5701000")
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("     " & GetAccntTitle("5701000"))
                        If CDec((row.Cells(chAmount.Index).Value * row.Cells(dgcQTY.Index).Value) - (row.Cells(chAvgCost.Index).Value * row.Cells(dgcQTY.Index).Value)) < 0 Then
                            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(((row.Cells(chAmount.Index).Value * row.Cells(dgcQTY.Index).Value) - (row.Cells(chAvgCost.Index).Value * row.Cells(dgcQTY.Index).Value)) * -1).ToString("N2"))
                            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                        Else
                            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec((row.Cells(chAmount.Index).Value * row.Cells(dgcQTY.Index).Value) - (row.Cells(chAvgCost.Index).Value * row.Cells(dgcQTY.Index).Value)).ToString("N2"))
                        End If
                       lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("MRIS:" & txtTransNum.Text)
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chEmpID.Index).Value)
                        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chEmpName.Index).Value)
                    End If
                Else
                    'FREE ITEM
                    ' DEBIT 
                    lvAccount.Items.Add(GetExpenseAccount(row.Cells(chItemCode.Index).Value))
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(GetExpenseAccount(row.Cells(chItemCode.Index).Value)))
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chAvgCost.Index).Value * row.Cells(dgcQTY.Index).Value).ToString("N2"))
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("MRIS:" & txtTransNum.Text)
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chEmpID.Index).Value)
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chEmpName.Index).Value)

                    ' CREDIT 
                    lvAccount.Items.Add(row.Cells(dgcAccntCode.Index).Value)
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("     " & GetAccntTitle(row.Cells(dgcAccntCode.Index).Value))
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(CDec(row.Cells(chAvgCost.Index).Value * row.Cells(dgcQTY.Index).Value).ToString("N2"))
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("MRIS:" & txtTransNum.Text)
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chEmpID.Index).Value)
                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chEmpName.Index).Value)

                End If
            End If
        Next

        TotalDBCR()

    End Sub

    Public Function GetATDAccount(ByVal ItemCode As String) As String
        Dim query As String
        query = " SELECT AD_ATD FROM tblItem_Master WHERE ItemCode ='" & ItemCode & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("AD_ATD").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetExpenseAccount(ByVal ItemCode As String) As String
        Dim query As String
        query = " SELECT AD_Expense FROM tblItem_Master WHERE ItemCode ='" & ItemCode & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("AD_Expense").ToString
        Else
            Return ""
        End If
    End Function

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
        f.ModFrom = "Customer"
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        txtVCEName.Text = f.VCEName
        LoadCurrency()
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
        For Each row As DataGridViewRow In dgvItemSummary.Rows
            If Not IsNothing(row.Cells(chSUM_ItemCode.Index).Value) Then
                itemCode = row.Cells(chSUM_ItemCode.Index).Value.ToString
                If Not IsNumeric(row.Cells(chSUM_IssuedQTY.Index).Value) Then IssuedQTY = 0 Else IssuedQTY = CDec(row.Cells(chSUM_IssuedQTY.Index).Value)
                If Not IsNothing(row.Cells(chSUM_UOM.Index).Value) Then UOM = row.Cells(chSUM_UOM.Index).Value Else UOM = ""
                query = "   SELECT	    ISNULL(SUM(QTY),0) AS QTY " & _
                        "   FROM		viewItem_Stock " & _
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
                row.Cells(chSUM_StockQTY.Index).Value = CDec(StockQTY).ToString("N2")
                'row.Cells(chSUM_IssuedQTY.Index).Value = CDec(IssueQTY).ToString("N2")

            End If

        Next
        dgvItemSummary.Columns(chSUM_StockQTY.Index).Visible = True
    End Sub

    Private Sub cbWHfrom_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbWHfrom.SelectedIndexChanged
            LoadStock()
    End Sub


    Private Sub dgvItemMaster_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemMaster.CellContentClick
        If dgvItemMaster.EditMode <> DataGridViewEditMode.EditProgrammatically Then

            If e.ColumnIndex = chkFree.Index Then
                If dgvItemMaster.CurrentRow.Cells(chkFree.Index).Value = False Then
                    dgvItemMaster.CurrentRow.Cells(chATD.Index).Value = False
                    dgvItemMaster.CurrentRow.Cells(chATDNo.Index).Value = ""
                Else
                    Dim VCECode As String = dgvItemMaster.CurrentRow.Cells(chEmpID.Index).Value
                    Dim VCEName As String = dgvItemMaster.CurrentRow.Cells(chEmpName.Index).Value
                    Dim Amount As Decimal = dgvItemMaster.CurrentRow.Cells(chAmount.Index).Value * dgvItemMaster.CurrentRow.Cells(dgcQTY.Index).Value
                    Dim ItemCode As String = dgvItemMaster.CurrentRow.Cells(chItemCode.Index).Value
                    dgvItemMaster.CurrentRow.Cells(chATD.Index).Value = True
                    dgvItemMaster.CurrentRow.Cells(chATDNo.Index).Value = CreateATD(VCECode, VCEName, Amount, ItemCode)
                    If dgvItemMaster.CurrentRow.Cells(chATDNo.Index).Value = "" Then
                        dgvItemMaster.CurrentRow.Cells(chkFree.Index).Value = True
                        dgvItemMaster.CurrentRow.Cells(chATD.Index).Value = False
                    End If
                End If
                dgvItemMaster.EndEdit()
                dgvItemMaster.CurrentCell = Nothing
            ElseIf e.ColumnIndex = chATD.Index Then
                If dgvItemMaster.CurrentRow.Cells(chATD.Index).Value = False Then
                    Dim VCECode As String = dgvItemMaster.CurrentRow.Cells(chEmpID.Index).Value
                    Dim VCEName As String = dgvItemMaster.CurrentRow.Cells(chEmpName.Index).Value
                    Dim Amount As Decimal = dgvItemMaster.CurrentRow.Cells(chAmount.Index).Value * dgvItemMaster.CurrentRow.Cells(dgcQTY.Index).Value
                    Dim ItemCode As String = dgvItemMaster.CurrentRow.Cells(chItemCode.Index).Value
                    dgvItemMaster.CurrentRow.Cells(chkFree.Index).Value = False
                    dgvItemMaster.CurrentRow.Cells(chATDNo.Index).Value = CreateATD(VCECode, VCEName, Amount, ItemCode)
                Else
                    dgvItemMaster.CurrentRow.Cells(chkFree.Index).Value = True
                    dgvItemMaster.CurrentRow.Cells(chATDNo.Index).Value = ""
                End If
                dgvItemMaster.EndEdit()
                dgvItemMaster.CurrentCell = Nothing
            End If
        End If
    End Sub


    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("MRIS", TransID)
        f.Dispose()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem1.Click
        frmGI_Type.Show()
    End Sub

    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged

    End Sub

    'Private Sub GenerateEntry()
    '    Dim vat, pendingAP, amount As Decimal
    '    Dim accountExist As Boolean = False
    '    vat = 0
    '    pendingAP = 0
    '    amount = 0
    '    If pendingAPsetup Then
    '        lvAccount.Items.Clear()
    '        For Each row As DataGridViewRow In dgvItemMaster.Rows
    '            accountExist = False
    '            If IsNumeric(row.Cells(dgcNet.Index).Value) AndAlso Not IsNothing(row.Cells(dgcAccountCode.Index).Value) Then
    '                pendingAP += row.Cells(dgcNet.Index).Value
    '                vat += row.Cells(dgcVATamt.Index).Value
    '                amount = row.Cells(dgcNet.Index).Value - row.Cells(dgcVATamt.Index).Value
    '                'For Each item As ListViewItem In lvAccount.Items
    '                '    If row.Cells(dgcAccountCode.Index).Value = item.SubItems(chAccountCode.Index).Text Then
    '                '        accountExist = True
    '                '        item.SubItems(chDebit.Index).Text = CDec(CDec(item.SubItems(chDebit.Index).Text) + amount).ToString("N2")
    '                '        Exit For
    '                '    End If
    '                'Next
    '                If accountExist = False Then
    '                    lvAccount.Items.Add(row.Cells(dgcAccountCode.Index).Value)
    '                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(GetAccntTitle(row.Cells(dgcAccountCode.Index).Value))
    '                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(amount.ToString("N2"))
    '                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
    '                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
    '                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chRRQTY.Index).Value)
    '                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chUnitCost.Index).Value)
    '                    lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(row.Cells(chItemCode.Index).Value)
    '                End If
    '            End If
    '        Next
    '        ' INPUT VAT ENTRY
    '        If vat <> 0 Then
    '            lvAccount.Items.Add(accntInputVAT)
    '            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("     " & GetAccntTitle(accntInputVAT))
    '            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(vat.ToString("N2"))
    '            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
    '            lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
    '        End If


    '        ' PENDING AP ENTRY
    '        lvAccount.Items.Add(accntAPpending)
    '        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("     " & GetAccntTitle(accntAPpending))
    '        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("")
    '        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add(pendingAP.ToString("N2"))
    '        lvAccount.Items(lvAccount.Items.Count - 1).SubItems.Add("RR:" & txtTransNum.Text)
    '        TotalDBCR()
    '    End If

    'End Sub

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

    Private Sub txtATDNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyValue <> 8 And Not (e.KeyValue >= 48 And e.KeyValue <= 57) And Not (e.KeyCode >= 96 And e.KeyCode <= 105) Then
                e.SuppressKeyPress = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub dgvItemMaster_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvItemMaster.CellContentDoubleClick
        If dgvItemMaster.EditMode <> DataGridViewEditMode.EditProgrammatically Then

            If e.ColumnIndex = chkFree.Index Then
                If dgvItemMaster.CurrentRow.Cells(chkFree.Index).Value = False Then
                    dgvItemMaster.CurrentRow.Cells(chATD.Index).Value = False
                    dgvItemMaster.CurrentRow.Cells(chATDNo.Index).Value = ""
                Else
                    Dim VCECode As String = dgvItemMaster.CurrentRow.Cells(chEmpID.Index).Value
                    Dim VCEName As String = dgvItemMaster.CurrentRow.Cells(chEmpName.Index).Value
                    Dim Amount As Decimal = dgvItemMaster.CurrentRow.Cells(chAmount.Index).Value
                    Dim ItemCode As String = dgvItemMaster.CurrentRow.Cells(chItemCode.Index).Value
                    dgvItemMaster.CurrentRow.Cells(chATD.Index).Value = True
                    dgvItemMaster.CurrentRow.Cells(chATDNo.Index).Value = CreateATD(VCECode, VCEName, Amount, ItemCode)
                    If dgvItemMaster.CurrentRow.Cells(chATDNo.Index).Value = "" Then
                        dgvItemMaster.CurrentRow.Cells(chkFree.Index).Value = True
                        dgvItemMaster.CurrentRow.Cells(chATD.Index).Value = False
                    End If
                End If
                dgvItemMaster.EndEdit()
                dgvItemMaster.CurrentCell = Nothing
            ElseIf e.ColumnIndex = chATD.Index Then
                If dgvItemMaster.CurrentRow.Cells(chATD.Index).Value = False Then
                    Dim VCECode As String = dgvItemMaster.CurrentRow.Cells(chEmpID.Index).Value
                    Dim VCEName As String = dgvItemMaster.CurrentRow.Cells(chEmpName.Index).Value
                    Dim Amount As Decimal = dgvItemMaster.CurrentRow.Cells(chAmount.Index).Value
                    Dim ItemCode As String = dgvItemMaster.CurrentRow.Cells(chItemCode.Index).Value
                    dgvItemMaster.CurrentRow.Cells(chkFree.Index).Value = False
                    dgvItemMaster.CurrentRow.Cells(chATDNo.Index).Value = CreateATD(VCECode, VCEName, Amount, ItemCode)
                    If dgvItemMaster.CurrentRow.Cells(chATDNo.Index).Value = "" Then
                        dgvItemMaster.CurrentRow.Cells(chkFree.Index).Value = True
                        dgvItemMaster.CurrentRow.Cells(chATD.Index).Value = False
                    End If
                Else
                    dgvItemMaster.CurrentRow.Cells(chkFree.Index).Value = True
                    dgvItemMaster.CurrentRow.Cells(chATDNo.Index).Value = ""
                End If
                dgvItemMaster.EndEdit()
                dgvItemMaster.CurrentCell = Nothing
            ElseIf e.ColumnIndex = chATDNo.Index Then
                If dgvItemMaster.CurrentRow.Cells(chATDNo.Index).Value <> "" Then
                    Dim ItemCode As String = dgvItemMaster.CurrentRow.Cells(chItemCode.Index).Value
                    UpdateATD(dgvItemMaster.CurrentRow.Cells(chATDNo.Index).Value, ItemCode)
                End If

            End If
        End If
    End Sub
    Private Function UpdateATD(ATDNo As String, ItemCode As String) As String
        Dim f As New frmATD
        f.DateDoc = dtpDocDate.Value.Date
        f.DedCode = GetDeductionType(ItemCode)
        f.ShowDialog(dtATD, ATDNo)
        dtATD = f.dtData
        If f.isSaved = True Then
            ATDTempID = f.txtTransNum.Text
        Else
            ATDTempID = ""
        End If
        f.Dispose()
        Return ATDTempID
    End Function
    Private Function CreateATD(VCECode As String, VCEName As String, Amount As Decimal, ItemCode As String) As String
        Dim f As New frmATD
        f.VCECode = VCECode
        f.VCEName = VCEName
        f.DedCode = GetDeductionType(ItemCode)
        f.Amount = Amount.ToString("N2")
        f.DateDoc = dtpDocDate.Value.Date
        f.ShowDialog(dtATD)
        dtATD = f.dtData
        If f.isSaved = True Then
            ATDTempID = f.txtTransNum.Text
        Else
            ATDTempID = ""
        End If
        f.Dispose()
        Return ATDTempID
    End Function
    Private Function GetDeductionType(ItemCode As String) As String
        Dim query As String
        query = "SELECT TOP  1 Ledger_Code Ledger_Code FROM RUBY_ePeople.dbo.viewLedger_Type WHERE Account_code = (SELECT AD_ATD FROM tblItem_Master WHERE ItemCode = @ItemCode ) " & _
                " AND Status = 'Active' AND  Category = 'Deduction'"
        SQL.FlushParams()
        SQL.AddParam("@ItemCode", ItemCode)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("Ledger_Code").ToString
        Else
            Return ""
        End If
    End Function


    Private Sub TestToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles TestToolStripMenuItem1.Click
        Dim f As New frmReport_Filter
        f.Report = "MRIS List"
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub dgvItemMaster_RowsRemoved(sender As Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgvItemMaster.RowsRemoved
        Try
            GenerateEntry()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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