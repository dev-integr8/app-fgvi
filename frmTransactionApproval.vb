Public Class frmTransactionApproval
    Dim TransAuto As Boolean
    Dim strRE_No, strRE_ID, strCode, strRemarks As String
    Dim dtpDateRE As Date
    Dim TotalAmount As Decimal
    Private Sub frmTransactionApproval_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadType()
        LoadData()
        LoadCC()
    End Sub

    Private Sub LoadType()
        Dim query As String
        query = "  SELECT       DISTINCT Type  FROM          viewTransaction    " &
                "  INNER JOIN " &
                "  ( " &
                "  SELECT ModuleID, UserID FROM tblUser_Access INNER JOIN tblModuleAccess " &
                " 	ON tblModuleAccess.FunctionID = tblUser_Access.Code " &
                " 	WHERE Status ='Active' AND Code LIKE '%_Approved'  AND isAllowed = 1 AND UserID = " & UserID & " " &
                "  ) AS module_access ON module_access.ModuleID = Type"
        SQL.ReadQuery(query)
        cbType.Items.Clear()
        cbType.Items.Add("ALL")
        While SQL.SQLDR.Read
            cbType.Items.Add(SQL.SQLDR("Type").ToString)
        End While
        If cbType.Items.Count > 0 Then
            cbType.SelectedIndex = 0
        End If
        cbType.Enabled = True
    End Sub

    Private Sub LoadData()
        Try
            Dim filter As String = ""
            Dim filtertype As String = ""
            Dim cc As String = ""
            Dim query As String = ""

            ' CONDITION OF QUERY

            If cbFilter.SelectedIndex = -1 Then
                filter = " WHERE '' = '' AND viewTransaction.Status IN ( 'Draft','Active')"
            Else
                Select Case cbFilter.SelectedItem
                    Case "VCE Code"
                        filter = " WHERE VCECode LIKE '%' + @Filter + '%' AND viewTransaction.Status IN ( 'Draft') "
                    Case "VCE Name"
                        filter = " WHERE VCEName LIKE '%' + @Filter + '%' AND viewTransaction.Status IN ( 'Draft')"
                    Case "Remarks"
                        filter = " WHERE Remarks LIKE '%' + @Filter + '%' AND viewTransaction.Status IN ( 'Draft')"
                End Select
            End If

            If cbType.SelectedIndex = -1 Or cbType.SelectedItem = "ALL" Then
                filtertype = " AND '' = ''"
            Else
                filtertype = " AND Type = '" & cbType.SelectedItem & "'"
            End If


            If cmbCC.SelectedIndex = -1 Or cmbCC.SelectedItem = "ALL" Then
                cc = " AND '' = ''"
            Else
                cc = " AND CostCenter = '" & cmbCC.SelectedItem & "'"
            End If

            ' QUERY 
            query = "  SELECT        *   FROM          viewTransaction    " &
                    "  INNER JOIN " &
                    "  ( " &
                    "  SELECT ModuleID, UserID FROM tblUser_Access INNER JOIN tblModuleAccess " &
                    " 	ON tblModuleAccess.FunctionID = tblUser_Access.Code " &
                    " 	WHERE Status ='Active' AND Code LIKE '%_Approved'  AND isAllowed = 1 AND UserID = " & UserID & " " &
                    "  ) AS module_access " &
                    " 	ON module_access.ModuleID = Type " & filter & filtertype & cc
            SQL.FlushParams()
            SQL.AddParam("@Filter", txtFilter.Text)
            dgvList.Columns.Clear()
            If query <> "" Then
                SQL.GetQuery(query)
                If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                    dgvList.DataSource = SQL.SQLDS.Tables(0)
                    dgvList.Columns(0).Visible = False


                    dgvList.Columns(6).DefaultCellStyle.Format = "N2"
                    dgvList.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                    Dim colX As New DataGridViewCheckBoxColumn
                    colX.HeaderText = "Include"
                    colX.Name = "dgcInc"
                    colX.Width = 50
                    colX.DefaultCellStyle.NullValue = False
                    dgvList.Columns.Add(colX)
                    colX.DisplayIndex = 1
                End If
            End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, "")
        Finally
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        LoadData()
    End Sub

    Private Sub dgvList_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList.CellContentClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
           If IsNothing(dgvList.Item(dgvList.Columns.Count - 1, e.RowIndex).Value) OrElse dgvList.Item(dgvList.Columns.Count - 1, e.RowIndex).Value = False Then
                    dgvList.Item(dgvList.Columns.Count - 1, e.RowIndex).Value = True
                Else
                    dgvList.Item(dgvList.Columns.Count - 1, e.RowIndex).Value = False
                End If
        End If
    End Sub

    Private Sub dgvList_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList.CellDoubleClick
        Try
            Dim RefType As String = dgvList.CurrentRow.Cells(1).Value.ToString
            Dim RefID As String = dgvList.CurrentRow.Cells(0).Value.ToString
            Select Case RefType
                Case "CV"
                    Dim f As New frmCV
                    f.ShowDialog(RefID)
                    f.Dispose()
                Case "APV"
                    Dim f As New frmAPV
                    f.ShowDialog(RefID)
                    f.Dispose()
                Case "JV"
                    Dim f As New frmJV
                    f.ShowDialog(RefID)
                    f.Dispose()
                Case "OR"
                    Dim f As New frmCollection
                    f.TransType = "OR"
                    f.ShowDialog(RefID)
                    f.Dispose()
                Case "AR"
                    Dim f As New frmCollection
                    f.TransType = "AR"
                    f.ShowDialog(RefID)
                    f.Dispose()
                Case "CR"
                    Dim f As New frmCollection
                    f.TransType = "CR"
                    f.ShowDialog(RefID)
                    f.Dispose()
                Case "PR"
                    Dim f As New frmCollection
                    f.TransType = "PR"
                    f.ShowDialog(RefID)
                    f.Dispose()
                Case "PJ"
                    Dim f As New frmPJ
                    f.ShowDialog(RefID)
                    f.Dispose()
                Case "SJ"
                    Dim f As New frmSJ
                    f.ShowDialog(RefID)
                    f.Dispose()
                Case "PO"
                    Dim f As New frmPO
                    f.ShowDialog(RefID)
                    f.Dispose()
                Case "RR"
                    Dim f As New frmRR
                    f.ShowDialog(RefID)
                    f.Dispose()
                Case "ADV"
                    Dim f As New frmAdvances
                    f.ShowDialog(RefID)
                    f.Dispose()
                Case "RE"
                    Dim f As New frmRE
                    f.ShowDialog(RefID)
                    f.Dispose()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cbFilter_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbFilter.SelectedIndexChanged
        LoadData()
    End Sub

    Private Sub cbType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbType.SelectedIndexChanged
        LoadData()
    End Sub


    Private Sub txtFilter_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtFilter.TextChanged
        LoadData()
    End Sub

    Private Sub tsbApprove_Click(sender As System.Object, e As System.EventArgs) Handles tsbApprove.Click
        Dim msgValue As Boolean = False
        If MsgBox("Are you sure you want to approve this transaction, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
            For Each row As DataGridViewRow In dgvList.Rows
                If row.Cells(dgvList.Columns.Count - 1).Value = True Then
                    msgValue = True
                    Dim RefType As String = row.Cells(1).Value.ToString
                    Dim RefID As String = row.Cells(0).Value.ToString
                    UpdateTransaction(RefType, RefID, "Approved")
                End If
            Next
            If msgValue = True Then
                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                LoadData()
            End If
        End If
    End Sub

    Private Sub UpdateTransaction(ByVal Reftype As String, ByVal RefTransID As Integer, ByVal Status As String)
        Dim SJ_ID As String
        Dim JE_ID As String
        Try
            Dim JETransiD As Integer = 0
            Dim tableName As String = ""
            Dim updateSQL As String
            Select Case Reftype
                Case "CV"
                    tableName = "tblCV"
                Case "APV"
                    tableName = "tblAPV"
                Case "JV"
                    tableName = "tblJV"
                Case "OR"
                    tableName = "tblCollection"
                Case "AR"
                    tableName = "tblCollection"
                Case "CR"
                    tableName = "tblCollection"
                Case "PR"
                    tableName = "tblCollection"
                Case "PJ"
                    tableName = "tblPJ"
                Case "SJ"
                    tableName = "tblSJ"
                Case "PO"
                    tableName = "tblPO"
                Case "RR"
                    tableName = "tblRR"
                Case "ADV"
                    tableName = "tblADV"
                Case "RE"
                    tableName = "tblRE"
            End Select


            If tableName = "tblCollection" Then
                updateSQL = " UPDATE " & tableName & " SET Status = @Status WHERE TransID =  @TransID  AND TransType = @TransType "
                SQL.FlushParams()
                SQL.AddParam("@TransID", RefTransID)
                SQL.AddParam("@TransType", Reftype)
                If Status = "Approved" Then
                    SQL.AddParam("@Status", "Active")
                Else
                    SQL.AddParam("@Status", "Draft")
                End If
                SQL.ExecNonQuery(updateSQL)
            ElseIf Reftype = "BS" Then
                updateSQL = " UPDATE " & tableName & " SET Status = @Status WHERE TransID =  @TransID  "
                SQL.FlushParams()
                SQL.AddParam("@TransID", RefTransID)
                If Status = "Approved" Then
                    SQL.AddParam("@Status", "Active")
                Else
                    SQL.AddParam("@Status", "Draft")
                End If
                SQL.ExecNonQuery(updateSQL)

                Dim SJID As Integer
                SJID = LoadSJID(RefTransID)

                updateSQL = " UPDATE tblSJ SET Status = @Status WHERE  TransID =  @TransID  "
                SQL.FlushParams()
                SQL.AddParam("@TransID", SJID)
                If Status = "Approved" Then
                    SQL.AddParam("@Status", "Active")
                Else
                    SQL.AddParam("@Status", "Draft")
                End If
                SQL.ExecNonQuery(updateSQL)

                Reftype = "SJ"
                RefTransID = SJID

            ElseIf Reftype = "RE" Then
                updateSQL = " UPDATE " & tableName & " SET Status = @Status WHERE TransID =  @TransID  "
                SQL.FlushParams()
                SQL.AddParam("@TransID", RefTransID)
                If Status = "Approved" Then
                    SQL.AddParam("@Status", "Active")
                Else
                    SQL.AddParam("@Status", "Draft")
                End If
                SQL.ExecNonQuery(updateSQL)

                'Dim SJID As Integer
                'SJID = LoadSJID_RE(RefTransID)

                'updateSQL = " UPDATE tblSJ SET Status = @Status WHERE  TransID =  @TransID  "
                'SQL.FlushParams()
                'SQL.AddParam("@TransID", SJID)
                'If Status = "Approved" Then
                '    SQL.AddParam("@Status", "Active")
                'Else
                '    SQL.AddParam("@Status", "Draft")
                'End If
                'SQL.ExecNonQuery(updateSQL)

                'Reftype = "SJ"
                'RefTransID = SJID

                TransAuto = GetTransSetup("SJ")

                Dim selectSQL As String
                selectSQL = "SELECT TransID,RE_No,VCECode,DateRE,TCPAmount, Remarks FROM tblRE WHERE TransID = @TransID "
                SQL.FlushParams()
                SQL.AddParam("@TransID", RefTransID)
                SQL.ReadQuery(selectSQL)
                If SQL.SQLDR.Read Then
                    strRE_No = SQL.SQLDR("RE_No")
                    strRE_ID = SQL.SQLDR("TransID")
                    strCode = SQL.SQLDR("VCECode")
                    strRemarks = SQL.SQLDR("Remarks")
                    dtpDateRE = SQL.SQLDR("DateRE")
                    TotalAmount = SQL.SQLDR("TCPAmount")
                End If

                Dim query As String
                Dim EditStatus As Boolean = 0
                query = " SELECT isEdit FROM tblRE WHERE TransID = @TransID "
                SQL.FlushParams()
                SQL.AddParam("@TransID", RefTransID)
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    EditStatus = SQL.SQLDR("isEdit")
                End If

                'STATUS EDIT
                If EditStatus = 0 Then

                    Dim insertSQL As String
                    Dim line As Integer = 1
                    Dim SJID As Integer
                    SJID = GenerateTransID("TransID", "tblSJ")
                    Dim SJNo As String
                    SJNo = GenerateTransNum(True, "SJ", "SJ_No", "tblSJ")
                    insertSQL = " INSERT INTO " &
                            " tblSJ (TransID, SJ_No, VCECode, BranchCode, BusinessCode, DateSJ, TotalAmount, Remarks, TransAuto, WhoCreated, Terms, DueDate, RE_ID, RE_Ref, Status) " &
                            " VALUES(@TransID, @SJ_No, @VCECode, @BranchCode, @BusinessCode, @DateSJ, @TotalAmount, @Remarks, @TransAuto, @WhoCreated,  @Terms, @DueDate, @RE_ID, @RE_Ref, @Status)"
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", SJID)
                    SQL.AddParam("@SJ_No", SJNo)
                    SQL.AddParam("@VCECode", strCode)
                    SQL.AddParam("@BranchCode", BranchCode)
                    SQL.AddParam("@BusinessCode", BusinessType)
                    SQL.AddParam("@DateSJ", dtpDateRE)
                    SQL.AddParam("@DueDate", dtpDateRE)
                    SQL.AddParam("@TotalAmount", TotalAmount)
                    SQL.AddParam("@Remarks", strRemarks)
                    SQL.AddParam("@TransAuto", TransAuto)
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.AddParam("@Terms", "")
                    SQL.AddParam("@RE_Ref", strRE_No)
                    SQL.AddParam("@RE_ID", strRE_ID)
                    SQL.AddParam("@Status", "Active")
                    SQL.ExecNonQuery(insertSQL)

                    JETransiD = GenerateTransID("JE_No", "tblJE_Header")
                    insertSQL = " INSERT INTO " &
                            " tblJE_Header (JE_No, AppDate, BranchCode, BusinessCode, RefType, RefTransID, Book, TotalDBCR, Remarks, WhoCreated, Status) " &
                            " VALUES(@JE_No, @AppDate, @BranchCode, @BusinessCode, @RefType, @RefTransID, @Book, @TotalDBCR, @Remarks, @WhoCreated, @Status)"
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransiD)
                    SQL.AddParam("@AppDate", dtpDateRE)
                    SQL.AddParam("@RefType", "SJ")
                    SQL.AddParam("@RefTransID", SJID)
                    SQL.AddParam("@Book", "Sales")
                    SQL.AddParam("@TotalDBCR", TotalAmount)
                    SQL.AddParam("@Remarks", strRemarks)
                    SQL.AddParam("@BranchCode", BranchCode)
                    SQL.AddParam("@BusinessCode", BusinessType)
                    SQL.AddParam("@Status", "Saved")
                    SQL.AddParam("@WhoCreated", UserID)
                    SQL.ExecNonQuery(insertSQL)

                    insertSQL = " INSERT INTO " &
                            " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, CostCenter, LineNumber) " &
                            " SELECT @JE_No AS JE_No,AccountCode, VCECode, Debit, Credit, Particulars, RefNo, CostCenter,  " &
                            "  ROW_NUMBER() OVER(ORDER BY Debit DESC, Credit ASC) AS LineNumber  " &
                            "  FROM  " &
                            "  (  " &
                            "  SELECT TransID, RE_No, (SELECT RE_AR FROM tblSystemSetup ) AS AccountCode, VCECode,  " &
                            "  ISNULL(SellingAmount,0) + ISNULL(tblRE.VATAmount,0) - ISNULL(tblRE.SellingCommission,0) AS Debit,  " &
                            "  '0.00' AS Credit,  " &
                            "  '' AS Particulars,   " &
                            "  'RE:' + RE_No AS RefNo,  Project AS CostCenter  " &
                            "  FROM tblRE  " &
                            "  LEFT JOIN tblSaleProperty  " &
                            "  ON tblRE.PropCode = tblSaleProperty.UnitCode  " &
                            "  WHERE ISNULL(SellingAmount,0) + ISNULL(tblRE.VATAmount,0) - ISNULL(tblRE.SellingCommission,0) > 0 AND TransID = @TransID  " &
                            "  UNION ALL  " &
                            "  SELECT TransID, RE_No, (SELECT RE_Commission FROM tblSystemSetup ) AS AccountCode, VCECode,  " &
                            "  ISNULL(SellingCommission,0) AS Debit,  " &
                            "  '0.00' AS Credit,  " &
                            "  '' AS Particulars,   " &
                            "  'RE:' + RE_No AS RefNo,  Project AS CostCenter  " &
                            "  FROM tblRE  " &
                            "  LEFT JOIN tblSaleProperty  " &
                            "  ON tblRE.PropCode = tblSaleProperty.UnitCode  " &
                            "  WHERE ISNULL(SellingCommission,0) > 0 AND TransID = @TransID  " &
                            "  UNION ALL  " &
                            "  SELECT TransID, RE_No, (SELECT RE_ARMiscfee FROM tblSystemSetup ) AS AccountCode, VCECode,  " &
                            "  ISNULL(MiscAmount,0) AS Debit,  " &
                            "  '0.00' AS Credit,  " &
                            "  '' AS Particulars,   " &
                            "  'RE:' + RE_No AS RefNo, Project AS CostCenter   " &
                            "  FROM tblRE  " &
                            "  LEFT JOIN tblSaleProperty  " &
                            "  ON tblRE.PropCode = tblSaleProperty.UnitCode  " &
                            "  WHERE  MiscAmount > 0 AND TransID = @TransID  " &
                            "  UNION ALL  " &
                            "  SELECT TransID, RE_No, (SELECT RE_Miscfee FROM tblSystemSetup ) AS AccountCode, VCECode,  " &
                            "  '0.00' AS Debit,  " &
                            "  ISNULL(MiscAmount,0) AS Credit,  " &
                            "  '' AS Particulars,   " &
                            "  '' AS RefNo, Project AS CostCenter   " &
                            "  FROM tblRE  " &
                            "   LEFT JOIN tblSaleProperty  " &
                            "  ON tblRE.PropCode = tblSaleProperty.UnitCode  " &
                            "  WHERE  MiscAmount > 0 AND TransID = @TransID  " &
                            "  UNION ALL  " &
                            "  SELECT TransID, RE_No, (SELECT RE_OutputVat FROM tblSystemSetup ) AS AccountCode, VCECode,  " &
                            "  '0.00' AS Debit,  " &
                            "  ISNULL(tblRE.VATAmount,0) AS Credit,  " &
                            "  '' AS Particulars,   " &
                            "  '' AS RefNo, Project AS CostCenter   " &
                            "  FROM tblRE  " &
                            "   LEFT JOIN tblSaleProperty  " &
                            "  ON tblRE.PropCode = tblSaleProperty.UnitCode  " &
                            "  WHERE  tblRE.VATAmount > 0 AND TransID = @TransID   " &
                            "  UNION ALL  " &
                            "  SELECT TransID, RE_No, CASE WHEN tblRE.VatAmount > 0 THEN (SELECT RE_NetOfVat FROM tblSystemSetup ) ELSE (SELECT RE_Sales FROM tblSystemSetup ) END AS AccountCode, VCECode,  " &
                            "  '0.00' AS Debit,  " &
                            "  ISNULL(SellingAmount,0) AS Credit,  " &
                            "  '' AS Particulars,   " &
                            "  '' AS RefNo, Project AS CostCenter   " &
                            "  FROM tblRE  " &
                            "   LEFT JOIN tblSaleProperty  " &
                            "  ON tblRE.PropCode = tblSaleProperty.UnitCode  " &
                            "  WHERE  SellingAmount > 0 AND TransID = @TransID   " &
                            "  ) RE_Entry "
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JETransiD)
                    SQL.AddParam("@TransID", RefTransID)
                    SQL.ExecNonQuery(insertSQL)

                Else

                    'UPDATE REAL ESTATE ENTRIES

                    'GET REAL ESTATE NO.
                    Dim insertSQL As String
                    'Dim SJ_ID As String
                    'Dim JE_ID As String
                    query = " SELECT    TransID " &
                            " FROM      tblSJ " &
                            " WHERE     RE_ID = '" & strRE_ID & "' "
                    SQL.FlushParams()
                    SQL.ReadQuery(query)
                    If SQL.SQLDR.Read Then
                        SJ_ID = SQL.SQLDR("TransID").ToString
                    End If


                    'UPDATE TABLE SALES JOURNAL
                    Dim line As Integer = 1
                    Dim SJID As Integer
                    Dim SJNo As String
                    updateSQL = " UPDATE    tblSJ " &
                                " SET       VCECode = @VCECode, BranchCode =@BranchCode, BusinessCode =@BusinessCode, DateSJ =@DateSJ, TotalAmount= @TotalAmount,  " &
                                " 		    Remarks = @Remarks, TransAuto = @TransAuto, WhoModified = @WhoModified, Terms = @Terms, DueDate =@DueDate,  " &
                                " 		    RE_ID = @RE_ID, RE_Ref =@RE_Ref, Status =@Status " &
                                " WHERE     TransID = @TransID "
                    SQL.FlushParams()
                    SQL.AddParam("@TransID", SJ_ID)
                    SQL.AddParam("@VCECode", strCode)
                    SQL.AddParam("@BranchCode", BranchCode)
                    SQL.AddParam("@BusinessCode", BusinessType)
                    SQL.AddParam("@DateSJ", dtpDateRE)
                    SQL.AddParam("@DueDate", dtpDateRE)
                    SQL.AddParam("@TotalAmount", TotalAmount)
                    SQL.AddParam("@Remarks", strRemarks)
                    SQL.AddParam("@TransAuto", TransAuto)
                    SQL.AddParam("@WhoModified", UserID)
                    SQL.AddParam("@Terms", "")
                    SQL.AddParam("@RE_Ref", strRE_No)
                    SQL.AddParam("@RE_ID", strRE_ID)
                    SQL.AddParam("@Status", "Active")
                    SQL.ExecNonQuery(updateSQL)


                    'GET JE NO.
                    query = " SELECT    JE_No " &
                            " FROM      tblJE_Header " &
                            " WHERE     RefType = 'SJ' AND RefTransID = '" & SJ_ID & "'"
                    SQL.FlushParams()
                    SQL.ReadQuery(query)
                    If SQL.SQLDR.Read Then
                        JE_ID = SQL.SQLDR("JE_No").ToString
                    End If

                    'UPDATE JE HEADER ENTRY
                    updateSQL = " 	UPDATE  tblJE_Header " &
                                " 	SET     AppDate = @AppDate, BranchCode =@BranchCode, BusinessCode =@BusinessCode, RefType = @RefType, RefTransID =@RefTransID,  " &
                                " 	        Book =@Book, TotalDBCR =@TotalDBCR, Remarks =@Remarks, WhoModified = @WhoModified, Status = @Status " &
                                " 	WHERE   JE_No = @JE_No "
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JE_ID)
                    SQL.AddParam("@AppDate", dtpDateRE)
                    SQL.AddParam("@RefType", "SJ")
                    SQL.AddParam("@RefTransID", SJ_ID)
                    SQL.AddParam("@Book", "Sales")
                    SQL.AddParam("@TotalDBCR", TotalAmount)
                    SQL.AddParam("@Remarks", strRemarks)
                    SQL.AddParam("@BranchCode", BranchCode)
                    SQL.AddParam("@BusinessCode", BusinessType)
                    SQL.AddParam("@Status", "Saved")
                    SQL.AddParam("@WhoModified", UserID)
                    SQL.ExecNonQuery(updateSQL)

                    'INSERT JE DETAILS
                    insertSQL = " INSERT INTO " &
                                " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, RefNo, CostCenter, LineNumber) " &
                                " SELECT @JE_No AS JE_No,AccountCode, VCECode, Debit, Credit, Particulars, RefNo, CostCenter,  " &
                                "  ROW_NUMBER() OVER(ORDER BY Debit DESC, Credit ASC) AS LineNumber  " &
                                "  FROM  " &
                                "  (  " &
                                "  SELECT TransID, RE_No, (SELECT RE_AR FROM tblSystemSetup ) AS AccountCode, VCECode,  " &
                                "  ISNULL(SellingAmount,0) + ISNULL(tblRE.VATAmount,0) - ISNULL(tblRE.SellingCommission,0) AS Debit,  " &
                                "  '0.00' AS Credit,  " &
                                "  '' AS Particulars,   " &
                                "  'RE:' + RE_No AS RefNo,  Project AS CostCenter  " &
                                "  FROM tblRE  " &
                                "  LEFT JOIN tblSaleProperty  " &
                                "  ON tblRE.PropCode = tblSaleProperty.UnitCode  " &
                                "  WHERE ISNULL(SellingAmount,0) + ISNULL(tblRE.VATAmount,0) - ISNULL(tblRE.SellingCommission,0) > 0 AND TransID = @TransID  " &
                                "  UNION ALL  " &
                                "  SELECT TransID, RE_No, (SELECT RE_Commission FROM tblSystemSetup ) AS AccountCode, VCECode,  " &
                                "  ISNULL(SellingCommission,0) AS Debit,  " &
                                "  '0.00' AS Credit,  " &
                                "  '' AS Particulars,   " &
                                "  'RE:' + RE_No AS RefNo,  Project AS CostCenter  " &
                                "  FROM tblRE  " &
                                "  LEFT JOIN tblSaleProperty  " &
                                "  ON tblRE.PropCode = tblSaleProperty.UnitCode  " &
                                "  WHERE ISNULL(SellingCommission,0) > 0 AND TransID = @TransID  " &
                                "  UNION ALL  " &
                                "  SELECT TransID, RE_No, (SELECT RE_ARMiscfee FROM tblSystemSetup ) AS AccountCode, VCECode,  " &
                                "  ISNULL(MiscAmount,0) AS Debit,  " &
                                "  '0.00' AS Credit,  " &
                                "  '' AS Particulars,   " &
                                "  'RE:' + RE_No AS RefNo, Project AS CostCenter   " &
                                "  FROM tblRE  " &
                                "  LEFT JOIN tblSaleProperty  " &
                                "  ON tblRE.PropCode = tblSaleProperty.UnitCode  " &
                                "  WHERE  MiscAmount > 0 AND TransID = @TransID  " &
                                "  UNION ALL  " &
                                "  SELECT TransID, RE_No, (SELECT RE_Miscfee FROM tblSystemSetup ) AS AccountCode, VCECode,  " &
                                "  '0.00' AS Debit,  " &
                                "  ISNULL(MiscAmount,0) AS Credit,  " &
                                "  '' AS Particulars,   " &
                                "  '' AS RefNo, Project AS CostCenter   " &
                                "  FROM tblRE  " &
                                "   LEFT JOIN tblSaleProperty  " &
                                "  ON tblRE.PropCode = tblSaleProperty.UnitCode  " &
                                "  WHERE  MiscAmount > 0 AND TransID = @TransID  " &
                                "  UNION ALL  " &
                                "  SELECT TransID, RE_No, (SELECT RE_OutputVat FROM tblSystemSetup ) AS AccountCode, VCECode,  " &
                                "  '0.00' AS Debit,  " &
                                "  ISNULL(tblRE.VATAmount,0) AS Credit,  " &
                                "  '' AS Particulars,   " &
                                "  '' AS RefNo, Project AS CostCenter   " &
                                "  FROM tblRE  " &
                                "   LEFT JOIN tblSaleProperty  " &
                                "  ON tblRE.PropCode = tblSaleProperty.UnitCode  " &
                                "  WHERE  tblRE.VATAmount > 0 AND TransID = @TransID   " &
                                "  UNION ALL  " &
                                "  SELECT TransID, RE_No, CASE WHEN tblRE.VatAmount > 0 THEN (SELECT RE_NetOfVat FROM tblSystemSetup ) ELSE (SELECT RE_Sales FROM tblSystemSetup ) END AS AccountCode, VCECode,  " &
                                "  '0.00' AS Debit,  " &
                                "  ISNULL(SellingAmount,0) AS Credit,  " &
                                "  '' AS Particulars,   " &
                                "  '' AS RefNo, Project AS CostCenter   " &
                                "  FROM tblRE  " &
                                "   LEFT JOIN tblSaleProperty  " &
                                "  ON tblRE.PropCode = tblSaleProperty.UnitCode  " &
                                "  WHERE  SellingAmount > 0 AND TransID = @TransID   " &
                                "  ) RE_Entry "
                    SQL.FlushParams()
                    SQL.AddParam("@JE_No", JE_ID)
                    SQL.AddParam("@TransID", RefTransID)
                    SQL.ExecNonQuery(insertSQL)
                End If


            Else
                updateSQL = " UPDATE " & tableName & " SET Status = @Status WHERE TransID =  @TransID  "
                SQL.FlushParams()
                SQL.AddParam("@TransID", RefTransID)
                If Status = "Approved" Then
                    SQL.AddParam("@Status", "Active")
                Else
                    SQL.AddParam("@Status", "Draft")
                End If
                SQL.ExecNonQuery(updateSQL)
            End If


            SaveTransactionList(RefTransID, Reftype, Status)

            If Reftype = "RE" Then
                JETransiD = LoadJE("SJ", SJ_ID)
            Else
                JETransiD = LoadJE(Reftype, RefTransID)
            End If


            If JETransiD <> 0 Then
                updateSQL = " UPDATE  tblJE_Header SET Status = @Status WHERE JE_No = @JE_No"
                SQL.FlushParams()
                SQL.AddParam("@JE_No", JETransiD)
                If Status = "Approved" Then
                    SQL.AddParam("@Status", "Saved")
                Else
                    SQL.AddParam("@Status", "Draft")
                End If
                SQL.ExecNonQuery(updateSQL)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Function LoadSJID_RE(ByVal BSNo As String) As String
        Dim query As String
        query = "SELECT TransID FROM tblSJ WHERE RE_ID = @RE_ID "
        SQL.FlushParams()
        SQL.AddParam("@RE_ID", BSNo)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read AndAlso Not IsDBNull(SQL.SQLDR("TransID")) Then
            Return SQL.SQLDR("TransID")
        Else
            Return 1
        End If
    End Function


    Public Function LoadSJID(ByVal BSNo As String) As String
        Dim query As String
        query = "SELECT TransID FROM tblSJ WHERE BSID = @BSID "
        SQL.FlushParams()
        SQL.AddParam("@BSID", BSNo)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read AndAlso Not IsDBNull(SQL.SQLDR("TransID")) Then
            Return SQL.SQLDR("TransID")
        Else
            Return 1
        End If
    End Function
    Private Sub LoadCC()
        Dim query As String
        query = " SELECT DISTINCT CostCenter from viewTransaction "
        'SQL.FlushParams()
        SQL.ReadQuery(query)
        cmbCC.Items.Clear()
        cmbCC.Items.Add("")
        While SQL.SQLDR.Read
            cmbCC.Items.Add(SQL.SQLDR("CostCenter").ToString)
        End While
    End Sub

    Private Sub cmbCC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCC.SelectedIndexChanged
        LoadData()
    End Sub

    Private Sub tsbDisapprovedd_Click(sender As System.Object, e As System.EventArgs) Handles tsbDisapprovedd.Click
        Dim msgValue As Boolean = False
        If MsgBox("Are you sure you want to disapprove this transaction, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
            For Each row As DataGridViewRow In dgvList.Rows
                If row.Cells(dgvList.Columns.Count - 1).Value = True Then
                    msgValue = True
                    Dim RefType As String = row.Cells(1).Value.ToString
                    Dim RefID As String = row.Cells(0).Value.ToString
                    UpdateTransaction(RefType, RefID, "Disapproved")
                End If
            Next
            If msgValue = True Then
                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                LoadData()
            End If
        End If
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub chkAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAll.CheckedChanged
            For Each row As DataGridViewRow In dgvList.Rows
                row.Cells(dgvList.Columns.Count - 1).Value = chkAll.Checked
            Next
    End Sub
End Class