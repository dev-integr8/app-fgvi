
Public Class frmSettings
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "Settings"
    Dim ATC_uFlag As Boolean = False ' UPDATE FLAG OF WTAX
    Dim ATC_vFlag As Boolean = False ' VALIDATE FLAG OF WTAX
    Dim branch_uFlag As Boolean = False ' UPDATE FLAG OF BRANCH
    Dim busType_uFlag As Boolean = False ' UPDATE FLAG OF BUSINESS TYPE
    Dim WTAX_dtCellBackColor As New DataTable
    Dim a As Integer = 0
    Dim BIR_RecordID As String = ""
    Dim BIR_DaysWithin As Integer = 0
    Public isSetup As Boolean = False

    Private Sub TreeView1_AfterSelect(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        Select Case TreeView1.SelectedNode.Text
            Case "User Account"
                tcSettings.SelectedTab = tpUA
            Case "Chart of Account"
                tcSettings.SelectedTab = tpCOA
            Case "Transaction ID"
                tcSettings.SelectedTab = tpGeneral
            Case "VCE Setup"
                tcSettings.SelectedTab = tpGeneral
            Case "Purchasing"
                tcSettings.SelectedTab = tpPurchase
            Case "ATC Table"
                tcSettings.SelectedTab = tpATC
            Case "Branch Setup"
                tcSettings.SelectedTab = tpBranch
            Case "Business Type Setup"
                tcSettings.SelectedTab = tpBusiType
            Case "Maintenance Group"
                tcSettings.SelectedTab = tpMaintGroup
            Case "Sales"
                tcSettings.SelectedTab = tpSales
            Case "Inventory"
                tcSettings.SelectedTab = tpInventory
            Case "Production"
                tcSettings.SelectedTab = tpProduction
            Case "Cooperative"
                tcSettings.SelectedTab = tpCoop
            Case "Default Entries"
                tcSettings.SelectedTab = tpEntries
            Case "Email"
                tcSettings.SelectedTab = tpEmail
            Case "SLS and SLP Maintenance"
                tcSettings.SelectedTab = tpSLS_SLP
            Case "BIR Reminders"
                tcSettings.SelectedTab = tpBIRReminders

        End Select
    End Sub

    Private Sub frmSettings_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If isSetup = True Then
            btnSave.Visible = False
            btnClose.Visible = False
            btnUpdateReport.Visible = False
            btnUpdate.Visible = False
        Else
            btnSave.Visible = True
            btnClose.Visible = True
            btnUpdateReport.Visible = True
            btnUpdate.Visible = True
        End If
        LoadTransType()
        loadATC()
        loadBranch()
        loadBusinessType()
        loadCC()
        loadPC()
        LoadWH()
        loadSales()
        loadProduction()
        loadPurchases()
        loadInventory()
        loadItemGroup()
        loadCoop()
        loadEntries()
        loademail()
        loadPOSEntries()
        LoadTaxDeadline()

        ' HIDE UNUSED TAB
        TreeView1.Nodes("nodeGeneral").Nodes("nodeUser").Remove()
        TreeView1.Nodes("nodeGeneral").Nodes("nodeVCE").Remove()
        TreeView1.Nodes("nodeCollection").Remove()
        TreeView1.Nodes("nodePurchasing").Remove()
        TreeView1.Nodes("nodeSales").Remove()
        TreeView1.Nodes("nodeProduction").Remove()
        TreeView1.Nodes("nodeCooperative").Remove()
        If UserID <> 1 Then
            TreeView1.Nodes("nodeGeneral").Nodes("nodeCOA").Remove()
            TreeView1.Nodes("nodeGeneral").Nodes("nodeTrans").Remove()
            TreeView1.Nodes("nodeGeneral").Nodes("nodeATC").Remove()
            TreeView1.Nodes("nodeGeneral").Nodes("nodeGroup").Remove()
            TreeView1.Nodes("nodeInventory").Remove()
            TreeView1.Nodes("nodeSPS_SLP").Remove()
        End If
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        updateATCTable()
        updateBranch()
        updateBusinessType()
        updateCC()
        updatePC()
        UpdateWH()
        updateSales()
        updatePurchases()
        updateProduction()
        updateInventory()
        updateItemGroup()
        updateTrans()
        updateCoop()
        updateEntries()
        updateEmail()
        updatePOSEntries()
        Msg("Changes Saved Successfully!", MsgBoxStyle.Information)
    End Sub

    Private Sub updateEmail()
        Dim updateSQL As String
        updateSQL = " UPDATE tblSystemSetup " &
                    " SET       EmailUsername = @EmailUsername, EmailPassword = @EmailPassword "
        SQL.FlushParams()
        SQL.AddParam("@EmailUsername", txtEmailAddress.Text)
        SQL.AddParam("@EmailPassword", txtEmailPass.Text)
        SQL.ExecNonQuery(updateSQL)
        Msg("Record Updated Successfully", MsgBoxStyle.Information)
    End Sub

    Private Sub UpdateCOAsetup()
        Dim updateSQL As String
        updateSQL = " UPDATE tblSystemSetup " &
                    " SET       COA_Auto = @COA_Auto, COA_AccntFormat = @COA_AccntFormat "
        SQL.FlushParams()
        SQL.AddParam("@COA_Auto", chkCOAauto.Checked)
        SQL.AddParam("@COA_AccntFormat", cbCOAformat.SelectedItem)
        SQL.ExecNonQuery(updateSQL)
        Msg("Record Updated Successfully", MsgBoxStyle.Information)
    End Sub

    Private Sub chkCOAauto_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCOAauto.CheckedChanged
        If chkCOAauto.Checked = True Then
            cbCOAformat.DropDownStyle = ComboBoxStyle.DropDown
        Else
            cbCOAformat.DropDownStyle = ComboBoxStyle.Simple
        End If
    End Sub






    Private Sub LoadTaxDeadline()
        Dim query As String
        query = " SELECT   RecordID, BIRFrm, Period, Month, Date " &
                " FROM     tblTax_Deadline ORDER BY  Month, Date, Period"
        SQL.FlushParams()
        SQL.ReadQuery(query)
        lvlBIRReminders.Items.Clear()
        While SQL.SQLDR.Read
            lvlBIRReminders.Items.Add(SQL.SQLDR("RecordID").ToString)
            lvlBIRReminders.Items(lvlBIRReminders.Items.Count - 1).SubItems.Add(SQL.SQLDR("BIRFrm").ToString)
            lvlBIRReminders.Items(lvlBIRReminders.Items.Count - 1).SubItems.Add(SQL.SQLDR("Period").ToString)
            lvlBIRReminders.Items(lvlBIRReminders.Items.Count - 1).SubItems.Add(SQL.SQLDR("Month").ToString)
            lvlBIRReminders.Items(lvlBIRReminders.Items.Count - 1).SubItems.Add(SQL.SQLDR("Date").ToString)
        End While

    End Sub


#Region "TRANSACTIONS ID SETUP"
    Private Sub LoadTransType()
        LoadTransHeader()
        LoadTransDetails()
    End Sub

    Private Sub LoadTransHeader()
        Dim query As String
        query = " SELECT TransType, Description, AutoSeries, GlobalSeries, ISNULL(isForApproval,0) AS isForApproval, ISNULL(ForReversal,0) AS ForReversal FROM tblTransactionSetup "
        SQL.ReadQuery(query)
        dgvTransID.Rows.Clear()
        While SQL.SQLDR.Read
            dgvTransID.Rows.Add({SQL.SQLDR("TransType").ToString, SQL.SQLDR("Description").ToString, SQL.SQLDR("AutoSeries").ToString, SQL.SQLDR("GlobalSeries").ToString, SQL.SQLDR("isForApproval").ToString, SQL.SQLDR("ForReversal").ToString})
        End While
    End Sub

    Private Sub LoadTransDetails()
        Dim query As String
        query = " SELECT TransType, BranchCode, BusinessCode, Prefix, Digits, ISNULL(StartRecord, 1) AS StartRecord  " &
                " FROM tblTransactionDetails " &
                " UNION ALL " &
                " SELECT TransType, 'All', 'All', '', '6', '1'  " &
                " FROM tblTransactionSetup WHERE TransType NOT IN (SELECT TransType FROM tblTransactionDetails) "
        SQL.ReadQuery(query)
        dgvTransDetailsAll.Rows.Clear()
        While SQL.SQLDR.Read
            dgvTransDetailsAll.Rows.Add({SQL.SQLDR("TransType").ToString, SQL.SQLDR("BranchCode").ToString, SQL.SQLDR("BusinessCode").ToString,
                                    SQL.SQLDR("Prefix").ToString, SQL.SQLDR("Digits").ToString, SQL.SQLDR("StartRecord").ToString})
        End While
    End Sub

    Private Sub chkTransAuto_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkTransAuto.CheckedChanged
        If disableEvent = False Then
            If chkTransAuto.Checked = False Then
                dgvTransDetail.Enabled = False
                dgvTransDetail.Rows.Clear()
            Else
                dgvTransDetail.Enabled = True
                If dgvTransID.SelectedRows.Count > 0 Then
                    LoadSeriesDetails(dgvTransID.SelectedRows(0).Cells(dgcTransType.Index).Value.ToString)
                End If
            End If
            dgvTransID.SelectedRows(0).Cells(dgcTransAuto.Index).Value = chkTransAuto.Checked
        End If
    End Sub

    Private Sub chkGlobal_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkGlobal.CheckedChanged
        If disableEvent = False Then
            If dgvTransID.SelectedRows.Count > 0 Then
                LoadSeriesDetails(dgvTransID.SelectedRows(0).Cells(dgcTransType.Index).Value.ToString)
            End If
            dgvTransID.SelectedRows(0).Cells(dgcTransGlobal.Index).Value = chkGlobal.Checked
        End If
    End Sub

    Private Sub dgvTransID_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTransID.CellClick
        If dgvTransID.SelectedRows.Count = 1 Then
            disableEvent = True
            chkTransAuto.Checked = dgvTransID.SelectedRows(0).Cells(dgcTransAuto.Index).Value
            chkGlobal.Checked = dgvTransID.SelectedRows(0).Cells(dgcTransGlobal.Index).Value
            chkForApproval.Checked = dgvTransID.SelectedRows(0).Cells(dgcForApproval.Index).Value
            chkReversalEntries.Checked = dgvTransID.SelectedRows(0).Cells(dgcForReversal.Index).Value
            LoadSeriesDetails(dgvTransID.SelectedRows(0).Cells(dgcTransType.Index).Value.ToString)
            disableEvent = False
        End If
    End Sub

    Private Sub LoadSeriesDetails(ByRef Type As String)
        Dim query As String
        If chkGlobal.Checked = True Then
            dgvTransDetail.Rows.Clear()
            'For Each row As DataGridViewRow In dgvTransDetailsAll.Rows
            '    If row.Cells(dgcTransAllType.Index).Value.ToString = Type Then
            '        dgvTransDetail.Rows.Add({row.Cells(dgcTransAllBranch.Index).Value.ToString, row.Cells(dgcTransAllBus.Index).Value.ToString, _
            '                                     row.Cells(dgcTransAllPrefix.Index).Value.ToString, row.Cells(dgcTransAlldigit.Index).Value.ToString, row.Cells(dgcTransAllStart.Index).Value.ToString})
            '    End If
            'Next
            For row = dgvTransDetailsAll.RowCount - 1 To 0 Step -1
                If dgvTransDetailsAll(dgcTransAllType.Index, row).Value.ToString = Type Then
                    dgvTransDetailsAll.Rows.RemoveAt(row)
                End If
            Next

            If dgvTransDetail.Rows.Count = 0 Then
                dgvTransDetail.Rows.Add({"All", "All", "", "6", "1"})
                dgvTransDetailsAll.Rows.Add({Type, "All", "All", "", "6", "1"})
            End If
        Else


            For row = dgvTransDetailsAll.RowCount - 1 To 0 Step -1
                If dgvTransDetailsAll(dgcTransAllType.Index, row).Value.ToString = Type Then
                    dgvTransDetailsAll.Rows.RemoveAt(row)
                End If
            Next

            query = " SELECT	A.BranchCode, A.BusinessCode,  " &
                " 		ISNULL(B.Prefix,ROW_NUMBER() OVER (ORDER BY A.BranchCode, A.BusinessCode)) AS Prefix, ISNULL(B.Digits,6) AS Digits, ISNULL(B.StartRecord,1) AS StartRecord  " &
                " FROM  " &
                " ( " &
                " 	SELECT	tblBranch.BranchCode, BusinessCode  " &
                " 	FROM	tblBranch CROSS JOIN tblBusinessType " &
                " ) AS A LEFT JOIN  " &
                " ( " &
                " 	SELECT TransType, BranchCode, BusinessCode, Prefix, Digits, StartRecord  " &
                " 	FROM tblTransactionDetails " &
                " 	WHERE  TransType ='" & Type & "' " &
                " ) AS B " &
                " ON		A.BranchCode = B.BranchCode " &
                " AND		A.BusinessCode = B.BusinessCode "
            SQL.ReadQuery(query)
            dgvTransDetail.Rows.Clear()
            While SQL.SQLDR.Read
                dgvTransDetail.Rows.Add({SQL.SQLDR("BranchCode").ToString, SQL.SQLDR("BusinessCode").ToString,
                                            SQL.SQLDR("Prefix").ToString, SQL.SQLDR("Digits").ToString, SQL.SQLDR("StartRecord").ToString})
                dgvTransDetailsAll.Rows.Add({Type, SQL.SQLDR("BranchCode").ToString, SQL.SQLDR("BusinessCode").ToString,
                                  SQL.SQLDR("Prefix").ToString, SQL.SQLDR("Digits").ToString, SQL.SQLDR("StartRecord").ToString})

            End While


        End If
    End Sub

    Private Sub updateTrans()
        Try
            Dim updateSQL As String
            For Each row As DataGridViewRow In dgvTransID.Rows
                updateSQL = " UPDATE    tblTransactionSetup " &
                                   " SET       AutoSeries = @AutoSeries, GlobalSeries = @GlobalSeries, isForApproval = @isForApproval, ForReversal = @ForReversal " &
                                   " WHERE     TransType = @TransType"
                SQL.FlushParams()
                SQL.AddParam("@TransType", row.Cells(dgcTransType.Index).Value)
                SQL.AddParam("@AutoSeries", row.Cells(dgcTransAuto.Index).Value)
                SQL.AddParam("@GlobalSeries", row.Cells(dgcTransGlobal.Index).Value)
                SQL.AddParam("@isForApproval", row.Cells(dgcForApproval.Index).Value)
                SQL.AddParam("@ForReversal", row.Cells(dgcForReversal.Index).Value)
                SQL.ExecNonQuery(updateSQL)
            Next
            Dim deleteSQL As String
            deleteSQL = " DELETE FROM tblTransActionDetails "
            SQL.ExecNonQuery(deleteSQL)

            Dim insertSQL As String
            For Each row As DataGridViewRow In dgvTransDetailsAll.Rows
                insertSQL = " INSERT INTO " &
                            " tblTransactionDetails(TransType, BranchCode, BusinessCode, Prefix, Digits, StartRecord) " &
                            " VALUES(@TransType, @BranchCode, @BusinessCode, @Prefix, @Digits, @StartRecord) "
                SQL.FlushParams()
                SQL.AddParam("@TransType", row.Cells(dgcTransAllType.Index).Value)
                SQL.AddParam("@BranchCode", row.Cells(dgcTransAllBranch.Index).Value)
                SQL.AddParam("@BusinessCode", row.Cells(dgcTransAllBus.Index).Value)
                SQL.AddParam("@Prefix", row.Cells(dgcTransAllPrefix.Index).Value)
                SQL.AddParam("@Digits", row.Cells(dgcTransAlldigit.Index).Value)
                SQL.AddParam("@StartRecord", row.Cells(dgcTransAllStart.Index).Value)
                SQL.ExecNonQuery(insertSQL)
            Next
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub dgvTransDetail_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTransDetail.CellEndEdit
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            For Each row As DataGridViewRow In dgvTransDetailsAll.Rows
                If dgvTransDetail.Item(dgcTransBranch.Index, e.RowIndex).Value = row.Cells(dgcTransAllBranch.Index).Value _
                    AndAlso dgvTransDetail.Item(dgcTransBusType.Index, e.RowIndex).Value = row.Cells(dgcTransAllBus.Index).Value _
                    AndAlso dgvTransID.SelectedRows(0).Cells(dgcTransType.Index).Value = row.Cells(dgcTransAllType.Index).Value Then

                    If e.ColumnIndex = dgcTransPrefix.Index Then
                        row.Cells(dgcTransAllPrefix.Index).Value = dgvTransDetail.Item(dgcTransPrefix.Index, e.RowIndex).Value
                    ElseIf e.ColumnIndex = dgcTransDigits.Index Then
                        row.Cells(dgcTransAlldigit.Index).Value = dgvTransDetail.Item(dgcTransDigits.Index, e.RowIndex).Value
                    ElseIf e.ColumnIndex = dgcTransStart.Index Then
                        row.Cells(dgcTransAllStart.Index).Value = dgvTransDetail.Item(dgcTransStart.Index, e.RowIndex).Value
                    End If

                End If
            Next
        End If
    End Sub
#End Region

#Region "TAX MAINTENANCE"

#Region "ATC"

    ' LOAD WTAX TABLE INTO DATAGRIDVIEW
    Private Sub loadATC()
        Dim query As String
        query = " SELECT   Code, Description, Rate " &
                " FROM     tblATC " &
                " WHERE    tblATC.Status = 'Active' "
        SQL.ReadQuery(query)
        dgvATC.Rows.Clear()
        While SQL.SQLDR.Read
            dgvATC.Rows.Add(SQL.SQLDR("Code").ToString, SQL.SQLDR("Description").ToString, CDec(SQL.SQLDR("Rate")).ToString("N2"))
        End While
        dgvATC.ReadOnly = True
    End Sub


    ' SET WTAX UPDATED FLAG TO TRUE WHEN THERE ARE CHANGES IN VAT DATAGRIDVIEW
    Private Sub dgvATC_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvATC.CellEndEdit
        If disableEvent = False Then
            ATC_uFlag = True
            ValueCheck(e.RowIndex, e.ColumnIndex)
        End If
    End Sub


    ' FUNCTION FOR VALIDATING CELL VALUES OF WTAX DATAGRIDVIEW 
    Private Function WTAX_Validated(Optional colInd As Integer = -1) As Boolean
        Dim valid As Boolean = True
        dgvATC.DefaultCellStyle.BackColor = Color.White
        If dgvATC.Rows.Count > 1 Then
            For i As Integer = 0 To dgvATC.Rows.Count - 2
                If colInd = -1 Then
                    For Each col As DataGridViewColumn In dgvATC.Columns
                        ValueCheck(i, col.Index)
                    Next
                Else
                    ValueCheck(i, colInd)
                End If
            Next
        End If
        Return valid
    End Function

    ' FUNCTION FOR VALIDATING CELL VALUES OF WTAX DATAGRIDVIEW 
    Private Function ValueCheck(ByRef RowIndex As Integer, ByRef ColIndex As Integer) As Boolean
        Select Case ColIndex
            Case dgcATCCode.Index
                If IsNothing(dgvATC.Item(ColIndex, RowIndex).Value) Then   ' VALIDATE TAX CODE
                    dgvATC.Item(ColIndex, RowIndex).Style.BackColor = Color.Yellow
                    WTAX_dtCellBackColor.Rows.Add(ColIndex, RowIndex)
                    Msg("Please enter tax code", MsgBoxStyle.Exclamation)
                    Return False
                Else
                    dgvATC.Item(ColIndex, RowIndex).Style.BackColor = Color.White
                End If
            Case dgcATCDesc.Index
                If IsNothing(dgvATC.Item(ColIndex, RowIndex).Value) Then   ' VALIDATE TAX DESCRIPTION
                    dgvATC.Item(ColIndex, RowIndex).Style.BackColor = Color.Yellow
                    Msg("Please enter tax description", MsgBoxStyle.Exclamation)
                    Return False
                Else
                    dgvATC.Item(ColIndex, RowIndex).Style.BackColor = Color.White
                End If
            Case dgcATCRate.Index
                If IsNothing(dgvATC.Item(ColIndex, RowIndex).Value) Then   ' VALIDATE TAX RATE
                    dgvATC.Item(ColIndex, RowIndex).Style.BackColor = Color.Yellow
                    Msg("Please enter tax rate", MsgBoxStyle.Exclamation)
                    Return False
                ElseIf Not IsNumeric(dgvATC.Item(ColIndex, RowIndex).Value) Then
                    dgvATC.Item(ColIndex, RowIndex).Style.BackColor = Color.Yellow
                    Msg("Invalid Tax Rate", MsgBoxStyle.Exclamation)

                    dgvATC.Item(ColIndex, RowIndex).Selected = True
                    Return False
                Else
                    dgvATC.Item(ColIndex, RowIndex).Style.BackColor = Color.White
                End If
        End Select
    End Function

    ' SUB FOR UPDATING WTAX TABLE
    Private Sub updateATCTable()
        Try
            Dim updateSQL As String
            Dim insertSQL As String
            ' UPDATE WTAX TABLE IF THERE ARE CHANGES IN WTAX DATAGRID
            If ATC_uFlag Then
                '       SET OLD RECORDS AS INACTIVE
                updateSQL = "UPDATE tblATC SET Status ='Inactive', DateModified = GETDATE(), WhoModified = '" & UserID & "' WHERE Type = 'EWT' AND Status ='Active' "
                SQL.ExecNonQuery(updateSQL)

                '       INSERT UPDATED RECORDS
                insertSQL = " INSERT INTO " &
                                " tblTaxMaintenance(Type, Code, Description, Rate,  Status, WhoCreated) " &
                                " VALUES(@Type, @Code, @Description, @Rate, 'Active', @WhoCreated) "

                For Each row As DataGridViewRow In dgvATC.Rows
                    If row.Cells(dgcATCCode.Index).Value = Nothing Then
                        Exit For
                    End If
                    SQL.FlushParams()
                    SQL.AddParam("@Type", "EWT")
                    SQL.AddParam("@Code", row.Cells(dgcATCCode.Index).Value.ToString)

                    If row.Cells(dgcATCDesc.Index).Value = Nothing Then
                        SQL.AddParam("@Description", "")
                    Else
                        SQL.AddParam("@Description", row.Cells(dgcATCDesc.Index).Value.ToString)
                    End If

                    If row.Cells(dgcATCRate.Index).Value = Nothing Then
                        SQL.AddParam("@Rate", 0)
                    ElseIf Not IsNumeric(row.Cells(dgcATCRate.Index).Value) Then
                        SQL.AddParam("@Rate", 0)
                    Else
                        SQL.AddParam("@Rate", row.Cells(dgcATCRate.Index).Value)
                    End If

                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)
                Next
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

#End Region

#End Region

#Region "BUSINESS TYPE"
    Private Sub loadBusinessType()
        Dim query As String
        query = " SELECT   BusinessCode, Description  " &
                " FROM     tblBusinessType " &
                " WHERE    Status = 'Active' "
        SQL.ReadQuery(query)
        dgvBusType.Rows.Clear()
        While SQL.SQLDR.Read
            dgvBusType.Rows.Add(SQL.SQLDR("BusinessCode").ToString, SQL.SQLDR("BusinessCode").ToString, SQL.SQLDR("Description").ToString)
        End While
    End Sub

    Private Sub dgvBusType_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvBusType.CellEndEdit
        busType_uFlag = True
    End Sub

    Private Sub updateBusinessType()
        Try
            Dim updateSQL As String
            Dim insertSQL As String
            ' UPDATE WTAX TABLE IF THERE ARE CHANGES IN WTAX DATAGRID
            If busType_uFlag Then
                '       SET OLD RECORDS AS INACTIVE
                updateSQL = "UPDATE tblBusinessType SET Status ='Inactive', DateModified = GETDATE(), WhoModified = '" & UserID & "' WHERE Status ='Active' "
                SQL.ExecNonQuery(updateSQL)


                '       INSERT UPDATED RECORDS
                insertSQL = " INSERT INTO " &
                            " tblBusinessType(BusinessCode, Description, WhoCreated) " &
                            " VALUES(@BusinessCode, @Description, @WhoCreated) "
                updateSQL = " UPDATE tblBusinessType " &
                            " SET  BusinessCode = @BusinessCodeNew, Description = @Description, WhoModified = @WhoModified, DateModified = GETDATE(), Status ='Active' " &
                            " WHERE BusinessCode = @BusinessCodeOld "
                For Each row As DataGridViewRow In dgvBusType.Rows
                    If row.Cells(dgcBusType.Index).Value = Nothing Then
                        Exit For
                    End If
                    If BusinessTypeExist(row.Cells(dgcBusTypeOld.Index).Value) Then ' CHECK IF CODE ALREADY EXIST
                        SQL.FlushParams()
                        SQL.AddParam("@BusinessCodeOld", row.Cells(dgcBusTypeOld.Index).Value.ToString)
                        SQL.AddParam("@BusinessCodeNew", row.Cells(dgcBusType.Index).Value.ToString)
                        If row.Cells(dgcBusTypeDesc.Index).Value = Nothing Then
                            SQL.AddParam("@Description", "")
                        Else
                            SQL.AddParam("@Description", row.Cells(dgcBusTypeDesc.Index).Value.ToString)
                        End If

                        SQL.AddParam("@WhoModified", UserID)
                        SQL.ExecNonQuery(updateSQL)
                    Else
                        SQL.FlushParams()
                        SQL.AddParam("@BusinessCode", row.Cells(dgcBusType.Index).Value.ToString)

                        If row.Cells(dgcBusTypeDesc.Index).Value = Nothing Then
                            SQL.AddParam("@Description", "")
                        Else
                            SQL.AddParam("@Description", row.Cells(dgcBusTypeDesc.Index).Value.ToString)
                        End If

                        SQL.AddParam("@WhoCreated", UserID)
                        SQL.ExecNonQuery(insertSQL)
                    End If
                Next
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

#End Region

#Region "BRANCH"
    Private Sub loadBranch()
        Dim query As String
        query = " SELECT   BranchCode, Description, Main  " &
                " FROM     tblBranch " &
                " WHERE    Status = 'Active' "
        SQL.ReadQuery(query)
        dgvBranch.Rows.Clear()
        While SQL.SQLDR.Read
            dgvBranch.Rows.Add(SQL.SQLDR("BranchCode").ToString, SQL.SQLDR("BranchCode").ToString, SQL.SQLDR("Description").ToString, SQL.SQLDR("Main"))
        End While
    End Sub

    Private Sub dgvBranch_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvBranch.CellEndEdit
        branch_uFlag = True
    End Sub

    Private Sub dgvBranch_CurrentCellDirtyStateChanged(sender As System.Object, e As System.EventArgs) Handles dgvBranch.CurrentCellDirtyStateChanged
        If disableEvent = False Then
            If dgvBranch.SelectedCells.Count > 0 Then
                If dgvBranch.SelectedCells(0).ColumnIndex = dgcBranchMain.Index Then
                    If dgvBranch.SelectedCells(0).RowIndex <> -1 Then
                        dgvBranch.CommitEdit(DataGridViewDataErrorContexts.Commit)
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub dgvBranch_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvBranch.CellValueChanged
        If disableEvent = False Then
            If dgvBranch.SelectedCells.Count > 0 Then
                If e.ColumnIndex = dgcBranchMain.Index Then
                    For Each row As DataGridViewRow In dgvBranch.Rows
                        If row.Index <> e.RowIndex Then
                            disableEvent = True
                            row.Cells(dgcBranchMain.Index).Value = False
                            disableEvent = False
                        End If
                    Next
                End If
            End If
        End If

    End Sub



    Private Sub updateBranch()
        Try
            Dim updateSQL As String
            Dim insertSQL As String
            ' UPDATE WTAX TABLE IF THERE ARE CHANGES IN WTAX DATAGRID
            If branch_uFlag Then
                '       SET OLD RECORDS AS INACTIVE
                updateSQL = "UPDATE tblBranch SET Status ='Inactive', DateModified = GETDATE(), WhoModified = '" & UserID & "' WHERE Status ='Active' "
                SQL.ExecNonQuery(updateSQL)


                '       INSERT UPDATED RECORDS
                insertSQL = " INSERT INTO " &
                            " tblBranch(BranchCode, Description, Main, WhoCreated) " &
                            " VALUES(@BranchCode, @Description, @Main, @WhoCreated) "
                updateSQL = " UPDATE tblBranch " &
                            " SET  BranchCode = @BranchCodeNew, Description = @Description, Main = @Main, WhoModified = @WhoModified, DateModified = GETDATE(), Status ='Active' " &
                            " WHERE BranchCode = @BranchCodeOld "
                For Each row As DataGridViewRow In dgvBranch.Rows
                    If row.Cells(dgcBranchCode.Index).Value = Nothing Then
                        Exit For
                    End If
                    If BranchExist(row.Cells(dgcBranchOldCode.Index).Value) Then ' CHECK IF CODE ALREADY EXIST
                        SQL.FlushParams()
                        SQL.AddParam("@BranchCodeOld", row.Cells(dgcBranchOldCode.Index).Value.ToString)
                        SQL.AddParam("@BranchCodeNew", row.Cells(dgcBranchCode.Index).Value.ToString)
                        If row.Cells(dgcBranchName.Index).Value = Nothing Then
                            SQL.AddParam("@Description", "")
                        Else
                            SQL.AddParam("@Description", row.Cells(dgcBranchName.Index).Value.ToString)
                        End If

                        If row.Cells(dgcBranchMain.Index).Value = Nothing Then
                            SQL.AddParam("@Main", "")
                        Else
                            SQL.AddParam("@Main", row.Cells(dgcBranchMain.Index).Value.ToString)
                        End If

                        SQL.AddParam("@WhoModified", UserID)
                        SQL.ExecNonQuery(updateSQL)
                    Else
                        SQL.FlushParams()
                        SQL.AddParam("@BranchCode", row.Cells(dgcBranchCode.Index).Value.ToString)

                        If row.Cells(dgcBranchName.Index).Value = Nothing Then
                            SQL.AddParam("@Description", "")
                        Else
                            SQL.AddParam("@Description", row.Cells(dgcBranchName.Index).Value.ToString)
                        End If

                        If row.Cells(dgcBranchMain.Index).Value = Nothing Then
                            SQL.AddParam("@Main", "")
                        Else
                            SQL.AddParam("@Main", row.Cells(dgcBranchMain.Index).Value.ToString)
                        End If

                        SQL.AddParam("@WhoCreated", UserID)
                        SQL.ExecNonQuery(insertSQL)
                    End If
                Next
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub



#End Region

#Region "COST CENTER"
    Dim oldCCG1, oldCCG2, oldCCG3, oldCCG4, oldCCG5 As String

    Private Sub loadCC()
        Dim query As String
        query = " SELECT GroupID, Description FROM tblGroup WHERE type ='Cost Center' AND Status ='Active' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            Select Case SQL.SQLDR("GroupID").ToString
                Case "G1"
                    txtCCG1.Text = SQL.SQLDR("Description").ToString
                Case "G2"
                    txtCCG2.Text = SQL.SQLDR("Description").ToString
                Case "G3"
                    txtCCG3.Text = SQL.SQLDR("Description").ToString
                Case "G4"
                    txtCCG4.Text = SQL.SQLDR("Description").ToString
                Case "G5"
                    txtCCG5.Text = SQL.SQLDR("Description").ToString
            End Select
        End While
        oldCCG1 = txtCCG1.Text
        oldCCG2 = txtCCG2.Text
        oldCCG3 = txtCCG3.Text
        oldCCG4 = txtCCG4.Text
        oldCCG5 = txtCCG5.Text
    End Sub

    Private Sub updateCC()
        If txtCCG1.Text <> oldCCG1 Or txtCCG2.Text <> oldCCG2 Or txtCCG3.Text <> oldCCG3 Or txtCCG4.Text <> oldCCG4 Or txtCCG5.Text <> oldCCG5 Then
            Dim updateSQL As String
            updateSQL = " UPDATE tblGroup  SET Status ='Inactive' WHERE Type ='Cost Center' "
            SQL.ExecNonQuery(updateSQL)
            If txtCCG1.Text <> "" Then SaveGroup("Cost Center", "G1", txtCCG1)
            If txtCCG2.Text <> "" Then SaveGroup("Cost Center", "G2", txtCCG2)
            If txtCCG3.Text <> "" Then SaveGroup("Cost Center", "G3", txtCCG3)
            If txtCCG4.Text <> "" Then SaveGroup("Cost Center", "G4", txtCCG4)
            If txtCCG5.Text <> "" Then SaveGroup("Cost Center", "G5", txtCCG5)
        End If
    End Sub
#End Region

#Region "PROFIT CENTER"
    Dim oldPCG1, oldPCG2, oldPCG3, oldPCG4, oldPCG5 As String

    Private Sub loadPC()
        Dim query As String
        query = " SELECT GroupID, Description FROM tblGroup WHERE type ='Profit Center' AND Status ='Active' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            Select Case SQL.SQLDR("GroupID").ToString
                Case "G1"
                    txtPCG1.Text = SQL.SQLDR("Description").ToString
                Case "G2"
                    txtPCG2.Text = SQL.SQLDR("Description").ToString
                Case "G3"
                    txtPCG3.Text = SQL.SQLDR("Description").ToString
                Case "G4"
                    txtPCG4.Text = SQL.SQLDR("Description").ToString
                Case "G5"
                    txtPCG5.Text = SQL.SQLDR("Description").ToString
            End Select
        End While
        oldPCG1 = txtPCG1.Text
        oldPCG2 = txtPCG2.Text
        oldPCG3 = txtPCG3.Text
        oldPCG4 = txtPCG4.Text
        oldPCG5 = txtPCG5.Text
    End Sub

    Private Sub updatePC()
        If txtPCG1.Text <> oldPCG1 Or txtPCG2.Text <> oldPCG2 Or txtPCG3.Text <> oldPCG3 Or txtPCG4.Text <> oldPCG4 Or txtPCG5.Text <> oldPCG5 Then
            Dim updateSQL As String
            updateSQL = " UPDATE tblGroup  SET Status ='Inactive' WHERE Type ='Profit Center' "
            SQL.ExecNonQuery(updateSQL)
            If txtPCG1.Text <> "" Then SaveGroup("Profit Center", "G1", txtPCG1)
            If txtPCG2.Text <> "" Then SaveGroup("Profit Center", "G2", txtPCG2)
            If txtPCG3.Text <> "" Then SaveGroup("Profit Center", "G3", txtPCG3)
            If txtPCG4.Text <> "" Then SaveGroup("Profit Center", "G4", txtPCG4)
            If txtPCG5.Text <> "" Then SaveGroup("Profit Center", "G5", txtCCG5)
        End If
    End Sub
#End Region




#Region "ITEM GROUP"
    Dim oldIG1, oldIG2, oldIG3, oldIG4, oldIG5 As String

    Private Sub loadItemGroup()
        Dim query As String
        query = " SELECT GroupID, Description FROM tblGroup WHERE type ='Item Group' AND Status ='Active' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            Select Case SQL.SQLDR("GroupID").ToString
                Case "G1"
                    txtInv_Group1.Text = SQL.SQLDR("Description").ToString
                Case "G2"
                    txtInv_Group2.Text = SQL.SQLDR("Description").ToString
                Case "G3"
                    txtInv_Group3.Text = SQL.SQLDR("Description").ToString
                Case "G4"
                    txtInv_Group4.Text = SQL.SQLDR("Description").ToString
                Case "G5"
                    txtInv_Group5.Text = SQL.SQLDR("Description").ToString
            End Select
        End While
        oldIG1 = txtInv_Group1.Text
        oldIG2 = txtInv_Group2.Text
        oldIG3 = txtInv_Group3.Text
        oldIG4 = txtInv_Group4.Text
        oldIG5 = txtInv_Group5.Text
    End Sub

    Private Sub updateItemGroup()
        If txtInv_Group1.Text <> oldIG1 Or txtInv_Group2.Text <> oldIG2 Or txtInv_Group3.Text <> oldIG3 Or txtInv_Group4.Text <> oldIG4 Or txtInv_Group5.Text <> oldIG5 Then
            Dim updateSQL As String
            updateSQL = " UPDATE tblGroup  SET Status ='Inactive' WHERE Type ='Item Group' "
            SQL.ExecNonQuery(updateSQL)
            If txtInv_Group1.Text <> "" Then SaveGroup("Item Group", "G1", txtInv_Group1) Else SaveGroup("Item Group", "G1", txtInv_Group1)
            If txtInv_Group2.Text <> "" Then SaveGroup("Item Group", "G2", txtInv_Group2) Else SaveGroup("Item Group", "G2", txtInv_Group2)
            If txtInv_Group3.Text <> "" Then SaveGroup("Item Group", "G3", txtInv_Group3) Else SaveGroup("Item Group", "G3", txtInv_Group3)
            If txtInv_Group4.Text <> "" Then SaveGroup("Item Group", "G4", txtInv_Group4) Else SaveGroup("Item Group", "G4", txtInv_Group4)
            If txtInv_Group5.Text <> "" Then SaveGroup("Item Group", "G5", txtInv_Group5) Else SaveGroup("Item Group", "G5", txtInv_Group5)
        End If
    End Sub
#End Region

#Region "WAREHOUSE"
    Private Sub LoadWH()
        loadInvWH()
        loadProdWH()
    End Sub

    Private Sub UpdateWH()
        updateInvWH()
        updateProdWH()
    End Sub

#Region "INVENTORY WAREHOUSE"
    Dim oldWHG1, oldWHG2, oldWHG3, oldWHG4, oldWHG5 As String

    Private Sub loadInvWH()
        Dim query As String
        query = " SELECT GroupID, Description FROM tblGroup WHERE type ='Warehouse' AND Status ='Active' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            Select Case SQL.SQLDR("GroupID").ToString
                Case "G1"
                    txtWHG1.Text = SQL.SQLDR("Description").ToString
                Case "G2"
                    txtWHG2.Text = SQL.SQLDR("Description").ToString
                Case "G3"
                    txtWHG3.Text = SQL.SQLDR("Description").ToString
                Case "G4"
                    txtWHG4.Text = SQL.SQLDR("Description").ToString
                Case "G5"
                    txtWHG5.Text = SQL.SQLDR("Description").ToString
            End Select
        End While
        oldWHG1 = txtWHG1.Text
        oldWHG2 = txtWHG2.Text
        oldWHG3 = txtWHG3.Text
        oldWHG4 = txtWHG4.Text
        oldWHG5 = txtWHG5.Text
    End Sub

    Private Sub updateInvWH()
        If txtWHG1.Text <> oldWHG1 Or txtWHG2.Text <> oldWHG2 Or txtWHG3.Text <> oldWHG3 Or txtWHG4.Text <> oldWHG4 Or txtWHG5.Text <> oldWHG5 Then
            Dim updateSQL As String
            updateSQL = " UPDATE tblGroup  SET Status ='Inactive' WHERE Type ='Warehouse' "
            SQL.ExecNonQuery(updateSQL)
            If txtWHG1.Text <> "" Then SaveGroup("Warehouse", "G1", txtWHG1)
            If txtWHG2.Text <> "" Then SaveGroup("Warehouse", "G2", txtWHG2)
            If txtWHG3.Text <> "" Then SaveGroup("Warehouse", "G3", txtWHG3)
            If txtWHG4.Text <> "" Then SaveGroup("Warehouse", "G4", txtWHG4)
            If txtWHG5.Text <> "" Then SaveGroup("Warehouse", "G5", txtWHG5)
        End If
    End Sub
#End Region
#Region "PRODUCTION WAREHOUSE"
    Dim oldPWHG1, oldPWHG2, oldPWHG3, oldPWHG4, oldPWHG5 As String

    Private Sub loadProdWH()
        Dim query As String
        query = " SELECT GroupID, Description FROM tblGroup WHERE type ='Production Warehouse' AND Status ='Active' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            Select Case SQL.SQLDR("GroupID").ToString
                Case "G1"
                    txtPWHG1.Text = SQL.SQLDR("Description").ToString
                Case "G2"
                    txtPWHG2.Text = SQL.SQLDR("Description").ToString
                Case "G3"
                    txtPWHG3.Text = SQL.SQLDR("Description").ToString
                Case "G4"
                    txtPWHG4.Text = SQL.SQLDR("Description").ToString
                Case "G5"
                    txtPWHG5.Text = SQL.SQLDR("Description").ToString
            End Select
        End While
        oldPWHG1 = txtPWHG1.Text
        oldPWHG2 = txtPWHG2.Text
        oldPWHG3 = txtPWHG3.Text
        oldPWHG4 = txtPWHG4.Text
        oldPWHG5 = txtPWHG5.Text
    End Sub

    Private Sub updateProdWH()
        If txtPWHG1.Text <> oldPWHG1 Or txtPWHG2.Text <> oldPWHG2 Or txtPWHG3.Text <> oldPWHG3 Or txtPWHG4.Text <> oldPWHG4 Or txtPWHG5.Text <> oldPWHG5 Then
            Dim updateSQL As String
            updateSQL = " UPDATE tblGroup SET Status ='Inactive' WHERE Type ='Production Warehouse' "
            SQL.ExecNonQuery(updateSQL)
            If txtPWHG1.Text <> "" Then SaveGroup("Production Warehouse", "G1", txtPWHG1)
            If txtPWHG2.Text <> "" Then SaveGroup("Production Warehouse", "G2", txtPWHG2)
            If txtPWHG3.Text <> "" Then SaveGroup("Production Warehouse", "G3", txtPWHG3)
            If txtPWHG4.Text <> "" Then SaveGroup("Production Warehouse", "G4", txtPWHG4)
            If txtPWHG5.Text <> "" Then SaveGroup("Production Warehouse", "G5", txtPWHG5)
        End If
    End Sub
#End Region

#End Region

#Region "SALES"
    Private Sub loadSales()
        Dim query As String
        query = " SELECT  ISNULL(SO_EditPrice,0) AS SO_EditPrice, ISNULL(SO_ReqPO,0) AS SO_ReqPO, " &
                "         ISNULL(SO_ReqDRdate,0) AS SO_ReqDRdate, ISNULL(SO_StaggeredDR,0) AS SO_StaggeredDR " &
                " FROM tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            chkSOeditPrice.Checked = SQL.SQLDR("SO_EditPrice")
            chkSOreqPO.Checked = SQL.SQLDR("SO_ReqPO")
            chkSOreqDelivDate.Checked = SQL.SQLDR("SO_ReqDRdate")
            chkSOstaggered.Checked = SQL.SQLDR("SO_StaggeredDR")
        End If
    End Sub

    Private Sub updateSales()
        Try
            Dim updateSQL As String
            updateSQL = " UPDATE tblSystemSetup " &
                        " SET    SO_EditPrice = @SO_EditPrice, SO_ReqPO = @SO_ReqPO, SO_ReqDRdate = @SO_ReqDRdate, SO_StaggeredDR = @SO_StaggeredDR "
            SQL.FlushParams()
            SQL.AddParam("@SO_EditPrice", chkSOeditPrice.Checked)
            SQL.AddParam("@SO_ReqPO", chkSOreqPO.Checked)
            SQL.AddParam("@SO_ReqDRdate", chkSOreqDelivDate.Checked)
            SQL.AddParam("@SO_StaggeredDR", chkSOstaggered.Checked)
            SQL.ExecNonQuery(updateSQL)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try

    End Sub
#End Region

#Region "PURCHASES"
    Private Sub loadPurchases()
        LoadWarehouseLevel()
        loadPurchaseData()
    End Sub

    Private Sub LoadWarehouseLevel()
        Dim query As String
        query = " SELECT Description FROm tblGroup WHERE Type ='Warehouse' AND Status ='Active' "
        SQL.ReadQuery(query)
        cbPRstock.Items.Clear()
        While SQL.SQLDR.Read
            cbPRstock.Items.Add(SQL.SQLDR("Description").ToString)
        End While
    End Sub

    Private Sub loadPurchaseData()
        Dim WHgroup As String = ""
        Dim query As String
        query = " SELECT  ISNULL(PR_StockLevel,0) AS PR_StockLevel, ISNULL(PO_Approval,0) AS PO_Approval FROM tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            WHgroup = SQL.SQLDR("PR_StockLevel").ToString
            chkPOapproval.Checked = SQL.SQLDR("PO_Approval")
            cbPRstock.SelectedItem = GetGroupDesc(WHgroup)
        End If
    End Sub


    Private Sub updatePurchases()
        Try
            Dim groupID As String = ""
            ' GET WAREHOUSE GROUP ID
            If cbPRstock.SelectedIndex <> -1 Then
                groupID = GetGroupID(cbPRstock.SelectedItem)
            End If

            Dim updateSQL As String
            updateSQL = " UPDATE tblSystemSetup " &
                        " SET    PR_StockLevel = @PR_StockLevel, PO_Approval = @PO_Approval "
            SQL.FlushParams()
            SQL.AddParam("@PR_StockLevel", groupID)
            SQL.AddParam("@PO_Approval", chkPOapproval.Checked)
            SQL.ExecNonQuery(updateSQL)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Function GetGroupDesc(ByVal GroupID As String) As String
        Dim query As String
        query = " SELECT Description FROM tblGroup WHERE Type ='Warehouse' AND Status ='Active' AND GroupID = '" & GroupID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("Description").ToString
        Else
            Return ""
        End If
    End Function

    Private Function GetGroupID(ByVal GroupDesc As String) As String
        Dim query As String
        query = " SELECT GroupID FROM tblGroup WHERE Type ='Warehouse' AND Status ='Active' AND Description = '" & GroupDesc & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("GroupID").ToString
        Else
            Return ""
        End If
    End Function
#End Region

#Region "INVENTORY"
    Private Sub loadInventory()
        loadInventoryData()
    End Sub

    Private Sub loadInventoryData()
        Dim query As String
        query = " SELECT  ISNULL(RR_RestrictWHSEItem,0) AS RR_RestrictWHSEItem, Inv_VarianceAccnt, " &
                " Inv_Computation, RR_BOOK, CSI_COS_BOOK, Inv_Movement FROM tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            chkRR_RestrictWHSEItem.Checked = SQL.SQLDR("RR_RestrictWHSEItem")
            txtInv_VarianceAccntCode.Text = SQL.SQLDR("Inv_VarianceAccnt").ToString
            If SQL.SQLDR("Inv_Computation") = "SC" Then
                rbInv_SC.Checked = True
                rbInv_WAUC.Checked = False
            Else
                rbInv_SC.Checked = False
                rbInv_WAUC.Checked = True
            End If
            If SQL.SQLDR("RR_BOOK") = "Inventory" Then
                rbRR_Inventory.Checked = True
                rbRR_Purchase.Checked = False
            Else
                rbRR_Inventory.Checked = False
                rbRR_Purchase.Checked = True
            End If
            If SQL.SQLDR("CSI_COS_BOOK") = "Inventory" Then
                rbCSI_Inventory.Checked = True
                rbCSI_JV.Checked = False
            Else
                rbCSI_Inventory.Checked = False
                rbCSI_JV.Checked = True
            End If

            If SQL.SQLDR("Inv_Movement") = "CSI" Then
                rbPOS_CSI.Checked = True
                rbPOS_POS.Checked = False
            Else
                rbPOS_CSI.Checked = False
                rbPOS_POS.Checked = True
            End If

            ' GET ACCOUNT TITLES
            txtInv_VarianceAccntTitle.Text = GetAccntTitle(txtInv_VarianceAccntCode.Text)
        End If
    End Sub


    Private Sub updateInventory()
        Try
            Dim updateSQL, Inv_Computation, RR_BOOK, CSI_COS_BOOK, Inv_Movement As String

            If rbInv_SC.Checked = True Then
                Inv_Computation = "SC"
            Else
                Inv_Computation = "WAUC"
            End If

            If rbRR_Inventory.Checked = True Then
                RR_BOOK = "Inventory"
            Else
                RR_BOOK = "Purchase Journal"
            End If

            If rbCSI_Inventory.Checked = True Then
                CSI_COS_BOOK = "Inventory"
            Else
                CSI_COS_BOOK = "General Journal"
            End If

            If rbPOS_CSI.Checked = True Then
                Inv_Movement = "CSI"
            Else
                Inv_Movement = "POS"
            End If

            updateSQL = " UPDATE tblSystemSetup " &
                        " SET    RR_RestrictWHSEItem = @RR_RestrictWHSEItem, Inv_VarianceAccnt = @Inv_VarianceAccnt, " &
                        " Inv_Computation = @Inv_Computation, RR_BOOK = @RR_BOOK, CSI_COS_BOOK = @CSI_COS_BOOK, Inv_Movement = @Inv_Movement "
            SQL.FlushParams()
            SQL.AddParam("@RR_RestrictWHSEItem", chkRR_RestrictWHSEItem.Checked)
            SQL.AddParam("@Inv_VarianceAccnt", txtInv_VarianceAccntCode.Text)
            SQL.AddParam("@Inv_Computation", Inv_Computation)
            SQL.AddParam("@RR_BOOK", RR_BOOK)
            SQL.AddParam("@CSI_COS_BOOK", CSI_COS_BOOK)
            SQL.AddParam("@Inv_Movement", Inv_Movement)
            SQL.ExecNonQuery(updateSQL)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub
#End Region


#Region "PRODUCTION"
    Private Sub loadProduction()
        Dim query As String
        query = " SELECT  ISNULL(JO_perSOlineItem,0) AS JO_perSOlineItem " &
                " FROM tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            chkJOperSOitem.Checked = SQL.SQLDR("JO_perSOlineItem")
        End If
    End Sub

    Private Sub updateProduction()
        Try
            Dim updateSQL As String
            updateSQL = " UPDATE tblSystemSetup " &
                        " SET    JO_perSOlineItem = @JO_perSOlineItem "
            SQL.FlushParams()
            SQL.AddParam("@JO_perSOlineItem", chkJOperSOitem.Checked)
            SQL.ExecNonQuery(updateSQL)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try

    End Sub
#End Region


#Region "COOPERATIVE"
    Private Sub loadCoop()
        loadCoopData()
    End Sub

    Private Sub loadCoopData()
        Dim query As String
        query = " SELECT    Coop_PUC_Common, Coop_PUC_Preferred, Coop_SC_Common, Coop_SC_Preferred, Coop_SR_Common, Coop_SR_Preferred, " &
                "           Coop_TC_Common, Coop_TC_Preferred, Coop_DFCS " &
                " FROM      tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtPUCCcode.Text = SQL.SQLDR("Coop_PUC_Common").ToString
            txtPUCPcode.Text = SQL.SQLDR("Coop_PUC_Preferred").ToString
            txtSCCcode.Text = SQL.SQLDR("Coop_SC_Common").ToString
            txtSCPcode.Text = SQL.SQLDR("Coop_SC_Preferred").ToString
            txtSRCcode.Text = SQL.SQLDR("Coop_SR_Common").ToString
            txtSRPcode.Text = SQL.SQLDR("Coop_SR_Preferred").ToString
            txtTCCcode.Text = SQL.SQLDR("Coop_TC_Common").ToString
            txtTCPcode.Text = SQL.SQLDR("Coop_TC_Preferred").ToString
            txtDFCScode.Text = SQL.SQLDR("Coop_DFCS").ToString

            ' GET ACCOUNT TITLES
            txtPUCCtitle.Text = GetAccntTitle(txtPUCCcode.Text)
            txtPUCPtitle.Text = GetAccntTitle(txtPUCPcode.Text)
            txtSCCtitle.Text = GetAccntTitle(txtSCCcode.Text)
            txtSCPtitle.Text = GetAccntTitle(txtSCPcode.Text)
            txtSRCtitle.Text = GetAccntTitle(txtSRCcode.Text)
            txtSRPtitle.Text = GetAccntTitle(txtSRPcode.Text)
            txtTCCtitle.Text = GetAccntTitle(txtTCCcode.Text)
            txtTCPtitle.Text = GetAccntTitle(txtTCPcode.Text)
            txtDFCStitle.Text = GetAccntTitle(txtDFCScode.Text)
        End If
    End Sub

    Private Sub updateCoop()
        Try

            Dim updateSQL As String
            updateSQL = " UPDATE    tblSystemSetup " &
                        " SET       Coop_PUC_Common = @Coop_PUC_Common, Coop_PUC_Preferred = @Coop_PUC_Preferred, " &
                        "           Coop_SC_Common = @Coop_SC_Common, Coop_SC_Preferred = @Coop_SC_Preferred, " &
                        "           Coop_SR_Common = @Coop_SR_Common, Coop_SR_Preferred = @Coop_SR_Preferred, " &
                        "           Coop_TC_Common = @Coop_TC_Common, Coop_TC_Preferred = @Coop_TC_Preferred, " &
                        "           Coop_DFCS = @Coop_DFCS "
            SQL.FlushParams()
            SQL.AddParam("@Coop_PUC_Common", txtPUCCcode.Text)
            SQL.AddParam("@Coop_PUC_Preferred", txtPUCPcode.Text)
            SQL.AddParam("@Coop_SC_Common", txtSCCcode.Text)
            SQL.AddParam("@Coop_SC_Preferred", txtSCPcode.Text)
            SQL.AddParam("@Coop_SR_Common", txtSRCcode.Text)
            SQL.AddParam("@Coop_SR_Preferred", txtSRPcode.Text)
            SQL.AddParam("@Coop_TC_Common", txtTCCcode.Text)
            SQL.AddParam("@Coop_TC_Preferred", txtTCPcode.Text)
            SQL.AddParam("@Coop_DFCS", txtDFCScode.Text)
            SQL.ExecNonQuery(updateSQL)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtSCCtitle_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSCCtitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtSCCtitle.Text)
                txtSCCcode.Text = f.accountcode
                txtSCCtitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtSRCtitle_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSRCtitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtSRCtitle.Text)
                txtSRCcode.Text = f.accountcode
                txtSRCtitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtPUCCtitle_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtPUCCtitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtPUCCtitle.Text)
                txtPUCCcode.Text = f.accountcode
                txtPUCCtitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtTCCtitle_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtTCCtitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtTCCtitle.Text)
                txtTCCcode.Text = f.accountcode
                txtTCCtitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtSCPtitle_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSCPtitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtSCPtitle.Text)
                txtSCPcode.Text = f.accountcode
                txtSCPtitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtSRPtitle_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSRPtitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtSRPtitle.Text)
                txtSRPcode.Text = f.accountcode
                txtSRPtitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtPUCPtitle_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtPUCPtitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtPUCPtitle.Text)
                txtPUCPcode.Text = f.accountcode
                txtPUCPtitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtTCPtitle_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtTCPtitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtTCPtitle.Text)
                txtTCPcode.Text = f.accountcode
                txtTCPtitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtDFCStitle_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtDFCStitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtDFCStitle.Text)
                txtDFCScode.Text = f.accountcode
                txtDFCStitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub
#End Region



#Region "ACCOUNTING ENTRIES"
    Private Sub loadEntries()
        loadEntriesData()
    End Sub


    Private Sub loadPOSEntries()
        Dim query As String
        query = " SELECT    POS_VATableSales, POS_VATamount, POS_VATexempt, POS_ZeroRated, POS_Discount " &
                " FROM      tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtPOS_VATSalesCode.Text = SQL.SQLDR("POS_VATableSales").ToString
            txtPOS_VATAmountCode.Text = SQL.SQLDR("POS_VATamount").ToString
            txtPOS_VATExemptCode.Text = SQL.SQLDR("POS_VATexempt").ToString
            txtPOS_ZeroRatedCode.Text = SQL.SQLDR("POS_ZeroRated").ToString
            txtPOS_DiscountCode.Text = SQL.SQLDR("POS_Discount").ToString


            ' GET ACCOUNT TITLES
            txtPOS_VATSalesTitle.Text = GetAccntTitle(txtPOS_VATSalesCode.Text)
            txtPOS_VATAmountTitle.Text = GetAccntTitle(txtPOS_VATAmountCode.Text)
            txtPOS_VATExemptTitle.Text = GetAccntTitle(txtPOS_VATExemptCode.Text)
            txtPOS_ZeroRatedTitle.Text = GetAccntTitle(txtPOS_ZeroRatedCode.Text)
            txtPOS_DiscountTitle.Text = GetAccntTitle(txtPOS_DiscountCode.Text)
        End If
    End Sub

    Private Sub loademail()
        Dim query As String
        query = " SELECT    EmailUsername,  EmailPassword" &
                " FROM      tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtEmailAddress.Text = SQL.SQLDR("EmailUsername").ToString
            txtEmailPass.Text = SQL.SQLDR("EmailPassword").ToString

        End If
    End Sub

    Private Sub loadEntriesData()
        Dim query As String
        query = " SELECT    AP_Pending,  AP_AdvancesSupplier, AP_SetupPendingAP, TAX_Percentage, TAX_VatPayable, " &
                "           TAX_IV, TAX_OV, TAX_DOV, TAX_EWT, TAX_CWT, TAX_FWT, PEC_Account, CASH_ACCOUNT, " &
                "           LM_AdvanceRent , LM_Deposit , LM_DST , LM_NF , LM_RentalIncome, " &
                "           RE_Account , RE_Equity , RE_Interest , RE_Penalty , RE_Reserve, RE_Commission, withIN, BM_SI, BM_COS, BM_AP, ISNULL(Bank_StaledPeriod,0) AS Bank_StaledPeriod, " &
                "            RE_Sales, RE_NetOfVat, RE_OutputVat, RE_MiscFee, RE_ARMiscfee, RE_AR, NI " &
                " FROM      tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtPAPcode.Text = SQL.SQLDR("AP_Pending").ToString
            txtIVcode.Text = SQL.SQLDR("TAX_IV").ToString
            txtOV_Code.Text = SQL.SQLDR("TAX_OV").ToString
            txtDOV_Code.Text = SQL.SQLDR("TAX_DOV").ToString
            txtATScode.Text = SQL.SQLDR("AP_AdvancesSupplier").ToString
            CheckBox15.Checked = SQL.SQLDR("AP_SetupPendingAP")
            txtPT_Code.Text = SQL.SQLDR("TAX_Percentage").ToString
            txtVP_Code.Text = SQL.SQLDR("TAX_VatPayable").ToString
            txtEWT_Code.Text = SQL.SQLDR("TAX_EWT").ToString
            txtCWT_Code.Text = SQL.SQLDR("TAX_CWT").ToString
            txtFWT_Code.Text = SQL.SQLDR("TAX_FWT").ToString
            txtPEC_Code.Text = SQL.SQLDR("PEC_Account").ToString
            txtCOH_Code.Text = SQL.SQLDR("CASH_ACCOUNT").ToString
            txtNI_Code.Text = SQL.SQLDR("NI").ToString
            nudBIR_WithIN.Value = SQL.SQLDR("withIN").ToString
            nupBankPeriod.Value = SQL.SQLDR("Bank_StaledPeriod").ToString

            'LM
            txtLM_AdvanceRentCode.Text = SQL.SQLDR("LM_AdvanceRent").ToString
            txtLM_DepositCode.Text = SQL.SQLDR("LM_Deposit").ToString
            txtLM_DSTCode.Text = SQL.SQLDR("LM_DST").ToString
            txtLM_NFCode.Text = SQL.SQLDR("LM_NF").ToString
            txtLM_RentalIncomeCode.Text = SQL.SQLDR("LM_RentalIncome").ToString


            'RM
            txtRE_AccountCode.Text = SQL.SQLDR("RE_Account").ToString
            txtRE_EquityCode.Text = SQL.SQLDR("RE_Equity").ToString
            txtRE_InterestCode.Text = SQL.SQLDR("RE_Interest").ToString
            txtRE_PenaltyCode.Text = SQL.SQLDR("RE_Penalty").ToString
            txtRE_ReserveCode.Text = SQL.SQLDR("RE_Reserve").ToString
            txtRE_CommissionCode.Text = SQL.SQLDR("RE_Commission").ToString

            txtRE_SalesCode.Text = SQL.SQLDR("RE_Sales").ToString
            txtRE_NetOfVATCode.Text = SQL.SQLDR("RE_NetOfVat").ToString
            txtRE_OutputVatCode.Text = SQL.SQLDR("RE_OutputVat").ToString
            txtRE_MiscFeeCode.Text = SQL.SQLDR("RE_MiscFee").ToString
            txtRE_ARMiscFeeCode.Text = SQL.SQLDR("RE_ARMiscfee").ToString
            txtRE_ARCode.Text = SQL.SQLDR("RE_AR").ToString

            txtRE_SalesTitle.Text = GetAccntTitle(SQL.SQLDR("RE_Sales").ToString)
            txtRE_NetOfVATTitle.Text = GetAccntTitle(SQL.SQLDR("RE_NetOfVat").ToString)
            txtRE_OutputVatTitle.Text = GetAccntTitle(SQL.SQLDR("RE_OutputVat").ToString)
            txtRE_MiscFeeTitle.Text = GetAccntTitle(SQL.SQLDR("RE_MiscFee").ToString)
            txtRE_ARMiscFeeTitle.Text = GetAccntTitle(SQL.SQLDR("RE_ARMiscfee").ToString)
            txtRE_ARTitle.Text = GetAccntTitle(SQL.SQLDR("RE_AR").ToString)



            'BM
            txtBM_SRCode.Text = SQL.SQLDR("BM_SI").ToString
            txtBM_COSCode.Text = SQL.SQLDR("BM_COS").ToString
            txtBM_APCode.Text = SQL.SQLDR("BM_AP").ToString


            ' LM
            txtLM_AdvanceRentTitle.Text = GetAccntTitle(txtLM_AdvanceRentCode.Text)
            txtLM_DepositTitle.Text = GetAccntTitle(txtLM_DepositCode.Text)
            txtLM_DSTTitle.Text = GetAccntTitle(txtLM_DSTCode.Text)
            txtLM_NFTitle.Text = GetAccntTitle(txtLM_NFCode.Text)
            txtLM_RentalIncomeTitle.Text = GetAccntTitle(txtLM_RentalIncomeCode.Text)


            ' RM
            txtRE_AccountTitle.Text = GetAccntTitle(txtRE_AccountCode.Text)
            txtRE_EquityTitle.Text = GetAccntTitle(txtRE_EquityCode.Text)
            txtRE_InterestTitle.Text = GetAccntTitle(txtRE_InterestCode.Text)
            txtRE_PenaltyTitle.Text = GetAccntTitle(txtRE_PenaltyCode.Text)
            txtRE_ReserveTitle.Text = GetAccntTitle(txtRE_ReserveCode.Text)
            txtRE_CommissionTitle.Text = GetAccntTitle(txtRE_CommissionCode.Text)

            'BM
            txtBM_SRTitle.Text = GetAccntTitle(txtBM_SRCode.Text)
            txtBM_COSTitle.Text = GetAccntTitle(txtBM_COSCode.Text)
            txtBM_APTitle.Text = GetAccntTitle(txtBM_APCode.Text)

            ' GET ACCOUNT TITLES
            txtPAPtitle.Text = GetAccntTitle(txtPAPcode.Text)
            txtIVtitle.Text = GetAccntTitle(txtIVcode.Text)
            txtOV_Title.Text = GetAccntTitle(txtOV_Code.Text)
            txtDOV_Title.Text = GetAccntTitle(txtDOV_Code.Text)
            txtATStitle.Text = GetAccntTitle(txtATScode.Text)
            txtPT_Title.Text = GetAccntTitle(txtPT_Code.Text)
            txtVP_Title.Text = GetAccntTitle(txtVP_Code.Text)
            txtEWT_Title.Text = GetAccntTitle(txtEWT_Code.Text)
            txtCWT_Title.Text = GetAccntTitle(txtCWT_Code.Text)
            txtFWT_Title.Text = GetAccntTitle(txtFWT_Code.Text)
            txtPEC_Title.Text = GetAccntTitle(txtPEC_Code.Text)
            txtCOH_Title.Text = GetAccntTitle(txtCOH_Code.Text)
            txtNI_Title.Text = GetAccntTitle(txtNI_Code.Text)
        End If
        LoadAPAccount()
        LoadARAccount()
        LoadCAAccount()
    End Sub

    Private Sub LoadCAAccount()
        Dim query As String
        query = " SELECT    tblCOA_Master.AccountCode + ' - ' + tblCOA_Master.AccountTitle AS Account " &
                " FROM      tblDefaultAccount INNER JOIN tblCOA_Master " &
                " ON        tblDefaultAccount.AccntCode = tblCOA_Master.AccountCode " &
                " WHERE     tblDefaultAccount.Type = 'CA' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            lblCashAdvance.Items.Add(SQL.SQLDR("Account").ToString)
        End While
    End Sub

    Private Sub LoadARAccount()
        Dim query As String
        query = " SELECT    tblCOA_Master.AccountCode + ' - ' + tblCOA_Master.AccountTitle AS Account " &
                " FROM      tblDefaultAccount INNER JOIN tblCOA_Master " &
                " ON        tblDefaultAccount.AccntCode = tblCOA_Master.AccountCode " &
                " WHERE     tblDefaultAccount.Type = 'AR' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            lbReceivables.Items.Add(SQL.SQLDR("Account").ToString)
        End While
    End Sub
    Private Sub LoadAPAccount()
        Dim query As String
        query = " SELECT    tblCOA_Master.AccountCode + ' - ' + tblCOA_Master.AccountTitle AS Account " &
                " FROM      tblDefaultAccount INNER JOIN tblCOA_Master " &
                " ON        tblDefaultAccount.AccntCode = tblCOA_Master.AccountCode " &
                " WHERE     tblDefaultAccount.Type = 'AP' "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            lbPayables.Items.Add(SQL.SQLDR("Account").ToString)
        End While
    End Sub


    Private Sub updateEntries()
        Try

            Dim updateSQL As String
            updateSQL = " UPDATE    tblSystemSetup " &
                        " SET       AP_Pending = @AP_Pending, " &
                        "           AP_AdvancesSupplier = @AP_AdvancesSupplier, AP_SetupPendingAP = @AP_SetupPendingAP, " &
                        "           TAX_Percentage = @TAX_Percentage, TAX_VatPayable = @TAX_VatPayable, " &
                        "           TAX_EWT = @TAX_EWT, TAX_CWT = @TAX_CWT, TAX_FWT = @TAX_FWT, " &
                        "           TAX_IV = @TAX_IV, TAX_OV = @TAX_OV, TAX_DOV = @TAX_DOV, PEC_Account = @PEC_Account, CASH_ACCOUNT = @CASH_ACCOUNT, " &
                        "           LM_AdvanceRent = @LM_AdvanceRent , LM_Deposit = @LM_Deposit , LM_DST = @LM_DST , LM_NF = @LM_NF , LM_RentalIncome = @LM_RentalIncome, " &
                        "           RE_Account = @RE_Account , RE_Equity = @RE_Equity , RE_Interest = @RE_Interest , RE_Penalty = @RE_Penalty , RE_Reserve = @RE_Reserve , RE_Commission = @RE_Commission, withIN = @withIN, " &
                        "           BM_SI = @BM_SI , BM_COS = @BM_COS , BM_AP = @BM_AP, Bank_StaledPeriod = @Bank_StaledPeriod,  " &
                        "           RE_Sales = @RE_Sales, RE_NetOfVat = @RE_NetOfVat, RE_OutputVat = @RE_OutputVat, RE_MiscFee = @RE_MiscFee, RE_ARMiscfee = @RE_ARMiscfee, RE_AR = @RE_AR, NI = @NI "
            SQL.FlushParams()
            SQL.AddParam("@AP_Pending", txtPAPcode.Text)
            SQL.AddParam("@AP_AdvancesSupplier", txtATScode.Text)
            SQL.AddParam("@AP_SetupPendingAP", CheckBox15.Checked)
            SQL.AddParam("@TAX_IV", txtIVcode.Text)
            SQL.AddParam("@TAX_OV", txtOV_Code.Text)
            SQL.AddParam("@TAX_DOV", txtDOV_Code.Text)
            SQL.AddParam("@TAX_Percentage", txtPT_Code.Text)
            SQL.AddParam("@TAX_Payable", txtPT_Code.Text)
            SQL.AddParam("@TAX_VatPayable", txtVP_Code.Text)
            SQL.AddParam("@TAX_EWT", txtEWT_Code.Text)
            SQL.AddParam("@TAX_CWT", txtCWT_Code.Text)
            SQL.AddParam("@TAX_FWT", txtFWT_Code.Text)
            SQL.AddParam("@PEC_Account", txtPEC_Code.Text)
            SQL.AddParam("@CASH_ACCOUNT", txtCOH_Code.Text)
            SQL.AddParam("@withIN", nudBIR_WithIN.Value)
            'LM
            SQL.AddParam("@LM_AdvanceRent", txtLM_AdvanceRentCode.Text)
            SQL.AddParam("@LM_Deposit", txtLM_DepositCode.Text)
            SQL.AddParam("@LM_DST", txtLM_DSTCode.Text)
            SQL.AddParam("@LM_NF", txtLM_NFCode.Text)
            SQL.AddParam("@LM_RentalIncome", txtLM_RentalIncomeCode.Text)
            'RE
            SQL.AddParam("@RE_Account", txtRE_AccountCode.Text)
            SQL.AddParam("@RE_Equity", txtRE_EquityCode.Text)
            SQL.AddParam("@RE_Interest", txtRE_InterestCode.Text)
            SQL.AddParam("@RE_Penalty", txtRE_PenaltyCode.Text)
            SQL.AddParam("@RE_Reserve", txtRE_ReserveCode.Text)
            SQL.AddParam("@RE_Commission", txtRE_CommissionCode.Text)
            SQL.AddParam("@RE_Sales", txtRE_SalesCode.Text)
            SQL.AddParam("@RE_NetOfVat", txtRE_NetOfVATCode.Text)
            SQL.AddParam("@RE_OutputVat", txtRE_OutputVatCode.Text)
            SQL.AddParam("@RE_MiscFee", txtRE_MiscFeeCode.Text)
            SQL.AddParam("@RE_ARMiscfee", txtRE_ARMiscFeeCode.Text)
            SQL.AddParam("@RE_AR", txtRE_ARCode.Text)
            SQL.AddParam("@NI", txtNI_Code.Text)
            'BM
            SQL.AddParam("@BM_SI", txtBM_SRCode.Text)
            SQL.AddParam("@BM_COS", txtBM_COSCode.Text)
            SQL.AddParam("@BM_AP", txtBM_APCode.Text)


            SQL.AddParam("@Bank_StaledPeriod", nupBankPeriod.Value)

            SQL.ExecNonQuery(updateSQL)

            UpdatePayableAccount()
            UpdateReceivablesAccount()
            UpdateCAAccount()
            UpdateSLSType()
            UpdateSLPType()
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub UpdateSLSType()
        Dim deleteSQL As String
        deleteSQL = " DELETE FROM tblTax_RefType WHERE Report = 'SLS' "
        SQL.ExecNonQuery(deleteSQL)
        Dim account As String()
        For Each item In lvlSLS.Items
            account = Strings.Split(item, " - ")
            Dim insertSQL As String
            insertSQL = " INSERT INTO tblTax_RefType(Report, Reftype) " &
                        " VALUES(@Report, @Reftype)"
            SQL.FlushParams()
            SQL.AddParam("@Report", "SLS")
            SQL.AddParam("@Reftype", account(0))
            SQL.ExecNonQuery(insertSQL)
        Next
    End Sub

    Private Sub UpdateSLPType()
        Dim deleteSQL As String
        deleteSQL = " DELETE FROM tblTax_RefType WHERE Report = 'SLP' "
        SQL.ExecNonQuery(deleteSQL)
        Dim account As String()
        For Each item In lvlSLP.Items
            account = Strings.Split(item, " - ")
            Dim insertSQL As String
            insertSQL = " INSERT INTO tblTax_RefType(Report, Reftype) " &
                        " VALUES(@Report, @Reftype)"
            SQL.FlushParams()
            SQL.AddParam("@Report", "SLP")
            SQL.AddParam("@Reftype", account(0))
            SQL.ExecNonQuery(insertSQL)
        Next
    End Sub
    Private Sub UpdateCAAccount()
        Dim deleteSQL As String
        deleteSQL = " DELETE FROM tblDefaultAccount WHERE Type = 'CA' "
        SQL.ExecNonQuery(deleteSQL)
        Dim account As String()
        For Each item In lblCashAdvance.Items
            account = Strings.Split(item, " - ")
            Dim insertSQL As String
            insertSQL = " INSERT INTO tblDefaultAccount(Type, AccntCode, Status, DateCreated, WhoCreated) " &
                        " VALUES(@Type, @AccntCode, 'Active', GETDATE(), @WhoCreated)"
            SQL.FlushParams()
            SQL.AddParam("@Type", "CA")
            SQL.AddParam("@AccntCode", account(0))
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)
        Next
    End Sub

    Private Sub UpdatePayableAccount()
        Dim deleteSQL As String
        deleteSQL = " DELETE FROM tblDefaultAccount WHERE Type = 'AP' "
        SQL.ExecNonQuery(deleteSQL)
        Dim account As String()
        For Each item In lbPayables.Items
            account = Strings.Split(item, " - ")
            Dim insertSQL As String
            insertSQL = " INSERT INTO tblDefaultAccount(Type, AccntCode, Status, DateCreated, WhoCreated) " &
                        " VALUES(@Type, @AccntCode, 'Active', GETDATE(), @WhoCreated)"
            SQL.FlushParams()
            SQL.AddParam("@Type", "AP")
            SQL.AddParam("@AccntCode", account(0))
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)
        Next
    End Sub

    Private Sub UpdateReceivablesAccount()
        Dim deleteSQL As String
        deleteSQL = " DELETE FROM tblDefaultAccount WHERE Type = 'AR' "
        SQL.ExecNonQuery(deleteSQL)
        Dim account As String()
        For Each item In lbReceivables.Items
            account = Strings.Split(item, " - ")
            Dim insertSQL As String
            insertSQL = " INSERT INTO tblDefaultAccount(Type, AccntCode, Status, DateCreated, WhoCreated) " &
                        " VALUES(@Type, @AccntCode, 'Active', GETDATE(), @WhoCreated)"
            SQL.FlushParams()
            SQL.AddParam("@Type", "AR")
            SQL.AddParam("@AccntCode", account(0))
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)
        Next
    End Sub

    Private Sub updatePOSEntries()
        Try

            Dim updateSQL As String
            updateSQL = " UPDATE    tblSystemSetup " &
                        " SET       POS_VATableSales = @POS_VATableSales, " &
                        "           POS_VATamount = @POS_VATamount, POS_VATexempt = @POS_VATexempt, " &
                        "           POS_ZeroRated = @POS_ZeroRated, POS_Discount = @POS_Discount "
            SQL.FlushParams()
            SQL.AddParam("@POS_VATableSales", txtPOS_VATSalesCode.Text)
            SQL.AddParam("@POS_VATamount", txtPOS_VATAmountCode.Text)
            SQL.AddParam("@POS_VATexempt", txtPOS_VATExemptCode.Text)
            SQL.AddParam("@POS_ZeroRated", txtPOS_ZeroRatedCode.Text)
            SQL.AddParam("@POS_Discount", txtPOS_DiscountCode.Text)
            SQL.ExecNonQuery(updateSQL)

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtPAPtitle_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtPAPtitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtPAPtitle.Text)
                txtPAPcode.Text = f.accountcode
                txtPAPtitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtIVtitle_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtIVtitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtIVtitle.Text)
                txtIVcode.Text = f.accountcode
                txtIVtitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub
    Private Sub txtOV_title_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtOV_Title.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtOV_Title.Text)
                txtOV_Code.Text = f.accountcode
                txtOV_Title.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub
    Private Sub txtDOV_title_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtDOV_Title.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtDOV_Title.Text)
                txtDOV_Code.Text = f.accountcode
                txtDOV_Title.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub
    Private Sub txtATStitle_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtATStitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtATStitle.Text)
                txtATScode.Text = f.accountcode
                txtATStitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

#End Region




    Private Sub TextBox9_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPT_Title.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtPT_Title.Text)
                txtPT_Code.Text = f.accountcode
                txtPT_Title.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtVP_Title_KeyDown(sender As Object, e As KeyEventArgs) Handles txtVP_Title.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtVP_Title.Text)
                txtVP_Code.Text = f.accountcode
                txtVP_Title.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtEWT_Title_KeyDown(sender As Object, e As KeyEventArgs) Handles txtEWT_Title.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtEWT_Title.Text)
                txtEWT_Code.Text = f.accountcode
                txtEWT_Title.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub
    Private Sub txtCWT_Title_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCWT_Title.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtCWT_Title.Text)
                txtCWT_Code.Text = f.accountcode
                txtCWT_Title.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub
    Private Sub txtFWT_Title_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFWT_Title.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtFWT_Title.Text)
                txtFWT_Code.Text = f.accountcode
                txtFWT_Title.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub
    Private Sub txtPEC_Title_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPEC_Title.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtPEC_Title.Text)
                txtPEC_Code.Text = f.accountcode
                txtPEC_Title.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtNI_Title_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNI_Title.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtNI_Title.Text)
                txtNI_Code.Text = f.accountcode
                txtNI_Title.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub btnAddReceivables_Click(sender As Object, e As EventArgs) Handles btnAddReceivables.Click
        Dim f As New frmCOA_Search
        f.ShowDialog("AccntTitle", "")
        If f.accountcode <> "" Then
            lbReceivables.Items.Add(f.accountcode + " - " + f.accttile)
        End If
    End Sub

    Private Sub btnRemoveReceivable_Click(sender As Object, e As EventArgs) Handles btnRemoveReceivable.Click
        Do While (lbReceivables.SelectedItems.Count > 0)
            lbReceivables.Items.Remove(lbReceivables.SelectedItem)
        Loop
    End Sub

    Private Sub btnAddPayables_Click(sender As Object, e As System.EventArgs) Handles btnAddPayables.Click
        Dim f As New frmCOA_Search
        f.ShowDialog("AccntTitle", "")
        If f.accountcode <> "" Then
            lbPayables.Items.Add(f.accountcode + " - " + f.accttile)
        End If
    End Sub

    Private Sub btnRemovePayables_Click(sender As Object, e As System.EventArgs) Handles btnRemovePayables.Click
        Do While (lbPayables.SelectedItems.Count > 0)
            lbPayables.Items.Remove(lbPayables.SelectedItem)
        Loop
    End Sub

    Private Sub Label52_Click(sender As System.Object, e As System.EventArgs) Handles Label52.Click

    End Sub

    Private Sub btnAddCA_Click(sender As System.Object, e As System.EventArgs) Handles btnAddCA.Click
        Dim f As New frmCOA_Search
        f.ShowDialog("AccntTitle", "")
        If f.accountcode <> "" Then
            lblCashAdvance.Items.Add(f.accountcode + " - " + f.accttile)
        End If
    End Sub

    Private Sub btnRemoveCA_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveCA.Click
        Do While (lblCashAdvance.SelectedItems.Count > 0)
            lblCashAdvance.Items.Remove(lblCashAdvance.SelectedItem)
        Loop
    End Sub

    Private Sub txtPT_Title_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPT_Title.TextChanged

    End Sub

    Private Sub txtCOH_Title_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtCOH_Title.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtCOH_Title.Text)
                txtCOH_Code.Text = f.accountcode
                txtCOH_Title.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtCOH_Title_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCOH_Title.TextChanged

    End Sub

    Private Sub txtATStitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtATStitle.TextChanged

    End Sub



    Private Sub txtInv_VarianceAccntTitle_TextChanged(sender As System.Object, e As System.EventArgs)

    End Sub


    Private Sub rbInv_SC_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbInv_SC.CheckedChanged, rbInv_WAUC.CheckedChanged

    End Sub

    Private Sub rbRR_Inventory_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbRR_Inventory.CheckedChanged, rbRR_Purchase.CheckedChanged

    End Sub

    Private Sub rbCSI_Inventory_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbCSI_Inventory.CheckedChanged, rbCSI_JV.CheckedChanged

    End Sub

    Private Sub rbPOS_CSI_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbPOS_CSI.CheckedChanged

    End Sub

    Private Sub dgvTransID_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTransID.CellContentClick

    End Sub

    Private Sub chkForApproval_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkForApproval.CheckedChanged
        If disableEvent = False Then
            If dgvTransID.SelectedRows.Count > 0 Then
                LoadSeriesDetails(dgvTransID.SelectedRows(0).Cells(dgcTransType.Index).Value.ToString)
            End If
            dgvTransID.SelectedRows(0).Cells(dgcForApproval.Index).Value = chkForApproval.Checked
        End If
    End Sub



    Private Sub btnAdd_SLP_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_SLP.Click
        Dim f As New frmModule_Search
        f.ShowDialog("Module", "")
        If f.accountcode <> "" Then
            lvlSLP.Items.Add(f.accountcode + " - " + f.accttile)
        End If
    End Sub

    Private Sub txtATScode_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtATScode.TextChanged

    End Sub

    Private Sub txtGPA_SalesAccntTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtGPA_SalesAccntTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtGPA_SalesAccntTitle.Text)
                txtGPA_SalesAccntCode.Text = f.accountcode
                txtGPA_SalesAccntTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtGPA_SalesAccntTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtGPA_SalesAccntTitle.TextChanged

    End Sub

    Private Sub txtGPA_COSAccntTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtGPA_COSAccntTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtGPA_COSAccntTitle.Text)
                txtGPA_COSAccntCode.Text = f.accountcode
                txtGPA_COSAccntTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtGPA_COSAccntTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtGPA_COSAccntTitle.TextChanged

    End Sub

    Private Sub txtGPA_SaleDiscountAccntTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtGPA_SaleDiscountAccntTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtGPA_SaleDiscountAccntTitle.Text)
                txtGPA_SaleDiscountAccntCode.Text = f.accountcode
                txtGPA_SaleDiscountAccntTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtGPA_SaleDiscountAccntTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtGPA_SaleDiscountAccntTitle.TextChanged

    End Sub

    Private Sub txtGPA_SaleReturnAccntTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtGPA_SaleReturnAccntTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtGPA_SaleReturnAccntTitle.Text)
                txtGPA_SaleReturnAccntCode.Text = f.accountcode
                txtGPA_SaleReturnAccntTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtGPA_SaleReturnAccntTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtGPA_SaleReturnAccntTitle.TextChanged

    End Sub

    Private Sub btnAdd_SLS_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_SLS.Click
        Dim f As New frmModule_Search
        f.ShowDialog("Module", "")
        If f.accountcode <> "" Then
            lvlSLS.Items.Add(f.accountcode + " - " + f.accttile)
        End If
    End Sub

    Private Sub btnRemove_SLS_Click(sender As Object, e As System.EventArgs) Handles btnRemove_SLS.Click
        Do While (lvlSLS.SelectedItems.Count > 0)
            lvlSLS.Items.Remove(lvlSLS.SelectedItem)
        Loop
    End Sub

    Private Sub btnRemove_SLP_Click(sender As System.Object, e As System.EventArgs) Handles btnRemove_SLP.Click
        Do While (lvlSLP.SelectedItems.Count > 0)
            lvlSLP.Items.Remove(lvlSLP.SelectedItem)
        Loop
    End Sub

    Private Sub txtRE_AccountTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtRE_AccountTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtRE_AccountTitle.Text)
                txtRE_AccountCode.Text = f.accountcode
                txtRE_AccountTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub txtRE_EquityTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtRE_EquityTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtRE_EquityTitle.Text)
                txtRE_EquityCode.Text = f.accountcode
                txtRE_EquityTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub txtRE_InterestTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtRE_InterestTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtRE_InterestTitle.Text)
                txtRE_InterestCode.Text = f.accountcode
                txtRE_InterestTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub txtRE_PenaltyTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtRE_PenaltyTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtRE_PenaltyTitle.Text)
                txtRE_PenaltyCode.Text = f.accountcode
                txtRE_PenaltyTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub txtRE_ReserveTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtRE_ReserveTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtRE_ReserveTitle.Text)
                txtRE_ReserveCode.Text = f.accountcode
                txtRE_ReserveTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtRE_CommissionTitle_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRE_CommissionTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtRE_CommissionTitle.Text)
                txtRE_CommissionCode.Text = f.accountcode
                txtRE_CommissionTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub



    Private Sub txtLM_DepositTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtLM_DepositTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtLM_DepositTitle.Text)
                txtLM_DepositCode.Text = f.accountcode
                txtLM_DepositTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub txtLM_DSTTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtLM_DSTTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtLM_DSTTitle.Text)
                txtLM_DSTCode.Text = f.accountcode
                txtLM_DSTTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub txtLM_NFTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtLM_NFTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtLM_NFTitle.Text)
                txtLM_NFCode.Text = f.accountcode
                txtLM_NFTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub txtLM_RentalIncomeTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtLM_RentalIncomeTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtLM_RentalIncomeTitle.Text)
                txtLM_RentalIncomeCode.Text = f.accountcode
                txtLM_RentalIncomeTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub



    Private Sub btnUpdate_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdate.Click
        If MsgBox("Update reports?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Process.Start("FTPUpdate.exe")
        End If
    End Sub

    Private Sub btnUpdateReport_Click_1(sender As System.Object, e As System.EventArgs) Handles btnUpdateReport.Click
        If MsgBox("Update reports?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Process.Start("ftpRPTUpdate.exe")
        End If
    End Sub

    Private Sub lvlBIRReminders_Click(sender As Object, e As System.EventArgs) Handles lvlBIRReminders.Click
        If lvlBIRReminders.SelectedItems.Count > 0 Then
            BIR_RecordID = lvlBIRReminders.Items(lvlBIRReminders.SelectedItems(0).Index).SubItems(chBIR_ID.Index).Text
            txtBIR_Reminder.Text = lvlBIRReminders.Items(lvlBIRReminders.SelectedItems(0).Index).SubItems(chDesc.Index).Text()
            cbBIR_Month.SelectedItem = lvlBIRReminders.Items(lvlBIRReminders.SelectedItems(0).Index).SubItems(chBIR_Month.Index).Text()
            cbBIR_Date.SelectedItem = lvlBIRReminders.Items(lvlBIRReminders.SelectedItems(0).Index).SubItems(chBIR_Date.Index).Text()
            cbBIR_Period.SelectedItem = lvlBIRReminders.Items(lvlBIRReminders.SelectedItems(0).Index).SubItems(chBIR_Period.Index).Text()


            btnBIR_Add.Enabled = True
            btnBIR_Add.Text = "Update"
            btnBIR_Remove.Enabled = True
            btnBIR_New.Enabled = True
        End If
    End Sub


    Private Sub lvlBIRReminders_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lvlBIRReminders.SelectedIndexChanged

    End Sub

    Private Sub btnBIR_Remove_Click(sender As System.Object, e As System.EventArgs) Handles btnBIR_Remove.Click
        If BIR_RecordID <> "" Then
            If MsgBox("Are you sure you want to delete this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                Try
                    activityStatus = True
                    Dim deleteSQL As String
                    deleteSQL = " DELETE tblTax_Deadline WHERE RecordID = @RecordID "
                    SQL.FlushParams()
                    SQL.AddParam("@RecordID", BIR_RecordID)
                    SQL.ExecNonQuery(deleteSQL)
                    Msg("Record deleted successfully", MsgBoxStyle.Information)
                    LoadTaxDeadline()


                    BIR_RecordID = ""
                    txtBIR_Reminder.Text = ""
                    cbBIR_Date.SelectedIndex = -1
                    cbBIR_Month.SelectedIndex = -1
                    cbBIR_Period.SelectedIndex = -1

                    btnBIR_Add.Enabled = False
                    btnBIR_Add.Text = "Add"
                    btnBIR_Remove.Enabled = False
                    btnBIR_New.Enabled = True
                Catch ex As System.Exception
                    activityStatus = True
                    SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                Finally
                    RecordActivity(UserID, ModuleID, Me.Name.ToString, "DELETE", "Tax Deadline", BIR_RecordID, BusinessType, BranchCode, "", activityStatus)
                    SQL.FlushParams()
                End Try
            End If
        End If
    End Sub

    Private Sub btnBIR_New_Click(sender As System.Object, e As System.EventArgs) Handles btnBIR_New.Click

        btnBIR_Add.Enabled = True
        btnBIR_Add.Text = "Add"
        btnBIR_Remove.Enabled = False
        btnBIR_New.Enabled = False

        BIR_RecordID = ""
        txtBIR_Reminder.Text = ""
        cbBIR_Date.SelectedIndex = -1
        cbBIR_Month.SelectedIndex = -1
        cbBIR_Period.SelectedIndex = -1
    End Sub

    Private Sub btnBIR_Add_Click(sender As System.Object, e As System.EventArgs) Handles btnBIR_Add.Click
        If BIR_RecordID = "" Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                If RecordExist_BIR(txtBIR_Reminder.Text) Then
                    Msg(" already in used! Please change SoftwareCode", MsgBoxStyle.Exclamation)
                Else
                    BIR_RecordID = GetRecordID("tblTax_Deadline", "RecordID")
                    SaveBIR_Reminder()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    LoadTaxDeadline()


                    BIR_RecordID = ""
                    txtBIR_Reminder.Text = ""
                    cbBIR_Date.SelectedIndex = -1
                    cbBIR_Month.SelectedIndex = -1
                    cbBIR_Period.SelectedIndex = -1

                    btnBIR_Add.Enabled = False
                    btnBIR_Add.Text = "Add"
                    btnBIR_Remove.Enabled = False
                    btnBIR_New.Enabled = True
                End If
            End If
        Else
            ' IF VCECODE IS CHANGED VALIDATE IF NEW CODE EXIST
            If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                UpdateBIR_Reminder()
                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                LoadTaxDeadline()


                BIR_RecordID = ""
                txtBIR_Reminder.Text = ""
                cbBIR_Date.SelectedIndex = -1
                cbBIR_Month.SelectedIndex = -1
                cbBIR_Period.SelectedIndex = -1

                btnBIR_Add.Enabled = False
                btnBIR_Add.Text = "Add"
                btnBIR_Remove.Enabled = False
                btnBIR_New.Enabled = True
            End If
        End If
    End Sub

    Private Sub SaveBIR_Reminder()
        Try
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " &
                         " tblTax_Deadline(BirFrm, Period, Month, Date) " &
                         " VALUES( @BirFrm, @Period, @Month, @Date)"
            SQL.FlushParams()
            SQL.AddParam("@BirFrm", txtBIR_Reminder.Text)
            SQL.AddParam("@Period", cbBIR_Period.SelectedItem)
            SQL.AddParam("@Month", cbBIR_Month.SelectedItem)
            SQL.AddParam("@Date", cbBIR_Date.SelectedItem)

            SQL.ExecNonQuery(insertSQL)
        Catch ex As System.Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "Tax Deadline", BIR_RecordID, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub UpdateBIR_Reminder()
        Try
            activityStatus = True
            Dim updateSQL As String
            updateSQL = " UPDATE tblTax_Deadline " &
                        " SET    BirFrm = @BirFrm, Period = @Period, Month = @Month, Date = @Date " &
                        " WHERE  RecordID = @RecordID "
            SQL.FlushParams()
            SQL.AddParam("@RecordID", BIR_RecordID)
            SQL.AddParam("@BirFrm", txtBIR_Reminder.Text)
            SQL.AddParam("@Period", cbBIR_Period.SelectedItem)
            SQL.AddParam("@Month", cbBIR_Month.SelectedItem)
            SQL.AddParam("@Date", cbBIR_Date.SelectedItem)
            SQL.ExecNonQuery(updateSQL)
        Catch ex As System.Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "Tax Deadline", BIR_RecordID, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Function RecordExist_BIR(ByVal Record As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblTax_Deadline WHERE BirFRM =@BirFRM "
        SQL.FlushParams()
        SQL.AddParam("@BirFRM", Record)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
        SQL.FlushParams()
    End Function

    Private Sub dgvTransDetail_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTransDetail.CellContentClick

    End Sub

    Private Sub txtInv_Group1_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtInv_Group1.TextChanged

    End Sub

    Private Sub txtInv_VarianceAccntTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtInv_VarianceAccntTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtInv_VarianceAccntTitle.Text)
                txtInv_VarianceAccntCode.Text = f.accountcode
                txtInv_VarianceAccntTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub txtInv_VarianceAccntTitle_TextChanged_1(sender As System.Object, e As System.EventArgs) Handles txtInv_VarianceAccntTitle.TextChanged

    End Sub

    Private Sub txtRE_ReserveTitle_TextChanged(sender As Object, e As EventArgs) Handles txtRE_ReserveTitle.TextChanged

    End Sub

    Private Sub Label90_Click(sender As Object, e As EventArgs) Handles Label90.Click

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles txtRE_SalesCode.TextChanged

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles txtRE_SalesTitle.TextChanged

    End Sub

    Private Sub txtRE_AccountTitle_TextChanged(sender As Object, e As EventArgs) Handles txtRE_AccountTitle.TextChanged

    End Sub

    Private Sub txtPOS_VATSalesTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtPOS_VATSalesTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtPOS_VATSalesTitle.Text)
                txtPOS_VATSalesCode.Text = f.accountcode
                txtPOS_VATSalesTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub lblCashAdvance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lblCashAdvance.SelectedIndexChanged

    End Sub

    Private Sub txtPOS_VATSalesTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPOS_VATSalesTitle.TextChanged

    End Sub

    Private Sub txtPOS_VATAmountTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtPOS_VATAmountTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtPOS_VATAmountTitle.Text)
                txtPOS_VATAmountCode.Text = f.accountcode
                txtPOS_VATAmountTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub txtPOS_VATAmountTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPOS_VATAmountTitle.TextChanged

    End Sub

    Private Sub txtPOS_VATExemptTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtPOS_VATExemptTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtPOS_VATExemptTitle.Text)
                txtPOS_VATExemptCode.Text = f.accountcode
                txtPOS_VATExemptTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtPOS_VATExemptTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPOS_VATExemptTitle.TextChanged

    End Sub

    Private Sub txtNI_Title_TextChanged(sender As Object, e As EventArgs) Handles txtNI_Title.TextChanged

    End Sub

    Private Sub txtPOS_ZeroRatedTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtPOS_ZeroRatedTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtPOS_ZeroRatedTitle.Text)
                txtPOS_ZeroRatedCode.Text = f.accountcode
                txtPOS_ZeroRatedTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtPOS_DiscountTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtPOS_DiscountTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtPOS_DiscountTitle.Text)
                txtPOS_DiscountCode.Text = f.accountcode
                txtPOS_DiscountTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtPOS_DiscountTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPOS_DiscountTitle.TextChanged

    End Sub

    Private Sub txtLM_AdvanceRentTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtLM_AdvanceRentTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtLM_AdvanceRentTitle.Text)
                txtLM_AdvanceRentCode.Text = f.accountcode
                txtLM_AdvanceRentTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub txtLM_AdvanceRentTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtLM_AdvanceRentTitle.TextChanged

    End Sub

    Private Sub txtLM_DepositTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtLM_DepositTitle.TextChanged

    End Sub

    Private Sub txtLM_DSTTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtLM_DSTTitle.TextChanged

    End Sub

    Private Sub txtBM_SRTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtBM_SRTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtBM_SRTitle.Text)
                txtBM_SRCode.Text = f.accountcode
                txtBM_SRTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtBM_SRTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtBM_SRTitle.TextChanged

    End Sub

    Private Sub txtBM_COSTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtBM_COSTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtBM_COSTitle.Text)
                txtBM_COSCode.Text = f.accountcode
                txtBM_COSTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtBM_COSTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtBM_COSTitle.TextChanged

    End Sub

    Private Sub txtBM_APTitle_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtBM_APTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtBM_APTitle.Text)
                txtBM_APCode.Text = f.accountcode
                txtBM_APTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtBM_APTitle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtBM_APTitle.TextChanged

    End Sub

    Private Sub chkReversalEntries_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkReversalEntries.CheckedChanged
        If disableEvent = False Then
            If dgvTransID.SelectedRows.Count > 0 Then
                LoadSeriesDetails(dgvTransID.SelectedRows(0).Cells(dgcTransType.Index).Value.ToString)
            End If
            dgvTransID.SelectedRows(0).Cells(dgcForReversal.Index).Value = chkReversalEntries.Checked
        End If
    End Sub

    Private Sub txtRE_SalesTitle_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRE_SalesTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtRE_SalesTitle.Text)
                txtRE_SalesCode.Text = f.accountcode
                txtRE_SalesTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtRE_NetOfVATTitle_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRE_NetOfVATTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtRE_NetOfVATTitle.Text)
                txtRE_NetOfVATCode.Text = f.accountcode
                txtRE_NetOfVATTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtRE_OutputVatTitle_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRE_OutputVatTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtRE_OutputVatTitle.Text)
                txtRE_OutputVatCode.Text = f.accountcode
                txtRE_OutputVatTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtRE_MiscFeeTitle_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRE_MiscFeeTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtRE_MiscFeeTitle.Text)
                txtRE_MiscFeeCode.Text = f.accountcode
                txtRE_MiscFeeTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub txtRE_ARMiscFeeTitle_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRE_ARMiscFeeTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtRE_ARMiscFeeTitle.Text)
                txtRE_ARMiscFeeCode.Text = f.accountcode
                txtRE_ARMiscFeeTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub



    Private Sub txtRE_ARTitle_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRE_ARTitle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", txtRE_ARTitle.Text)
                txtRE_ARCode.Text = f.accountcode
                txtRE_ARTitle.Text = f.accttile
                f.Dispose()
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub
End Class