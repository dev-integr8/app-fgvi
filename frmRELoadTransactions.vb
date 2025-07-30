Public Class frmRELoadTransactions
    Dim moduleID As String
    Public transID As String = ""
    Public ResCenter As String = ""
    Public BranchCode As String = ""
    Public itemCode As String = ""
    Public p1 As String = ""
    Public batch As Boolean = False
    Dim disableEvent As Boolean = False

    Public Overloads Function ShowDialog(ByVal ModID As String, Optional ByVal Param1 As String = "") As Boolean
        p1 = Param1
        moduleID = ModID
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmRELoadTransactions_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadData()
        LoadBranches()
        CostCenter()
        If moduleID = "Copy Member" Then
            cbFilter.Items.Clear()
            cbFilter.Items.Add("Member ID")
            cbFilter.Items.Add("Full Name")
            cbFilter.Items.Add("Status")
            Label2.Visible = True
            cbBranch.Visible = True
        End If
    End Sub
    Sub CostCenter()
        Try
            Dim query = "SELECT DISTINCT CODE FROM tblCC"
            SQL.ReadQuery(query)
            cbCostCenter.Items.Clear()
            cbCostCenter.Items.Add("ALL")
            While SQL.SQLDR.Read
                Dim Code As String = SQL.SQLDR("Code").ToString()
                cbCostCenter.Items.Add(Code)

            End While
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub LoadBranches()
        Dim query As String
        query = " SELECT    DISTINCT  tblBranch.BranchCode + ' - ' + Description AS BranchCode  " &
                " FROM      tblBranch    "
        SQL.ReadQuery(query)
        cbBranch.Items.Clear()
        While SQL.SQLDR.Read
            cbBranch.Items.Add(SQL.SQLDR("BranchCode").ToString)
        End While
        cbBranch.SelectedIndex = 0
    End Sub

    Private Sub LoadData()
        Try
            Dim filter As String = ""
            Dim query As String = ""

            Select Case moduleID
                'Case "RE"
                '    ' CONDITION OF QUERY
                '    If cbFilter.SelectedIndex = -1 Then
                '        If cbCostCenter.SelectedItem = "ALL" Then
                '            filter = " WHERE tblCC.Code LIKE '%' + @CostCenter + '%' "
                '        Else
                '            filter = " WHERE '' = ''"
                '        End If
                '    Else
                '        Select Case cbFilter.SelectedItem
                '            Case "Transaction ID"
                '                filter = " WHERE RE_No LIKE '%' + @Filter + '%' AND tblCC.Code = '' + @CostCenter + '' "
                '            Case "VCE Name"
                '                filter = " WHERE VCEName LIKE '%' + @Filter + '%'  AND tblCC.Code = '' + @CostCenter + '' "
                '            Case "VCE Code"
                '                filter = " WHERE VCECode LIKE '%' + @Filter + '%'  AND tblCC.Code = '' + @CostCenter + '' "
                '            Case "Remarks"
                '                filter = " WHERE Remarks LIKE '%' + @Filter + '%'  AND tblCC.Code = '' + @CostCenter + '' "
                '            Case "Status"
                '                filter = " WHERE tblRE.Status LIKE '%' + @Filter + '%'  AND tblCC.Code = '' + @CostCenter + '' "
                '            Case "Date Range"
                '                filter = " AND tblRE.DateRE BETWEEN  @DateFrom AND @DateTo ORDER BY DR_No "

                '        End Select
                '    End If

                '    ' QUERY 
                '    query = "  SELECT   TransID, RE_No AS [RE No.], DateRE AS [Date], VCEName AS [Customer],  Model + ' Blk ' + Unit_Blk  + ' Lot ' + " &
                '            "  Unit_Lot + ' ' + Project AS Property, tblRE.Remarks, tblRE.Status, tblCC.Code   " &
                '            "  FROM     tblRE  " &
                '            "  LEFT JOIN viewVCE_Master  ON	   tblRE.VCECode = viewVCE_Master.VCECode   " &
                '            "  LEFT JOIN tblSaleProperty  ON	   tblRE.PropCode = tblSaleProperty.UnitCode   " &
                '            "  LEFT JOIN tblCC  On tblSaleProperty.Project = tblCC.Code   " &
                '            "  WHERE (tblRE.VCECode LIKE '%' + @Filter + '%' OR VCEName LIKE '%' + @Filter + '%') AND ISNULL(tblCC.Code,'') = Case When @CostCenter = 'ALL' OR @CostCenter = '' THEN ISNULL(tblCC.Code,'') Else @CostCenter END "
                '    SQL.FlushParams()
                '    SQL.AddParam("@Filter", txtFilter.Text)
                '    SQL.AddParam("@CostCenter", cbCostCenter.Text)
                '    If cbFilter.SelectedItem = "Date Range" Then
                '        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                '        SQL.AddParam("@DateTo", dtpDateTo.Value)
                '    End If


                Case "RE"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        If cbCostCenter.SelectedItem = "ALL" Then
                            filter = " WHERE tblCC.Code LIKE '%' + @CostCenter + '%' "
                        Else
                           filter = " WHERE '' = ''"
                        End If
                    Else
                            Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE RE_No LIKE '%' + @Filter + '%' AND Project LIKE IIF(@CostCenter = 'ALL', '%%', '%' + @CostCenter + '%' ) "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' AND Project LIKE IIF(@CostCenter = 'ALL', '%%', '%' + @CostCenter + '%' ) "
                            Case "VCE Code"
                                filter = " WHERE VCECode LIKE '%' + @Filter + '%' AND Project LIKE IIF(@CostCenter = 'ALL', '%%', '%' + @CostCenter + '%' ) "
                            Case "Remarks"
                                filter = " WHERE tblRE.Remarks LIKE '%' + @Filter + '%' AND Project LIKE IIF(@CostCenter = 'ALL', '%%', '%' + @CostCenter + '%' ) "
                            Case "Status"
                                filter = " WHERE tblRE.Status LIKE '%' + @Filter + '%' AND Project LIKE IIF(@CostCenter = 'ALL', '%%', '%' + @CostCenter + '%' )"
                            Case "Date Range"
                                filter = " AND tblRE.DateRE BETWEEN  @DateFrom AND @DateTo ORDER BY DR_No "

                        End Select
                    End If

                    ' QUERY 
                    query = "  SELECT   TransID, RE_No AS [RE No.], DateRE AS [Date], VCEName AS [Customer],  Model + ' Blk ' + Unit_Blk  + ' Lot ' + " &
                            "  Unit_Lot + ' ' + Project AS Property, tblRE.Remarks, tblRE.Status, tblCC.Code   " &
                            "  FROM     tblRE  " &
                            "  LEFT JOIN viewVCE_Master  ON	   tblRE.VCECode = viewVCE_Master.VCECode   " &
                            "  LEFT JOIN tblSaleProperty  ON	   tblRE.PropCode = tblSaleProperty.UnitCode   " &
                            "  LEFT JOIN tblCC  On tblSaleProperty.Project = tblCC.Code   " &
                            "  " & filter & " "
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    SQL.AddParam("@CostCenter", cbCostCenter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If

            End Select
            If query <> "" Then
                SQL.GetQuery(query)
                If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                    dgvList.DataSource = SQL.SQLDS.Tables(0)
                    dgvList.Columns(0).Visible = False

                    If moduleID = "CV" Or moduleID = "OR" Or moduleID = "AR" Or moduleID = "CR" Or moduleID = "PR" Then
                        dgvList.Columns(5).DefaultCellStyle.Format = "N2"
                        dgvList.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    ElseIf moduleID = "User_Level" Then
                        dgvList.Columns(1).Width = 300
                    End If
                    If chkBatch.Visible = True Then
                        If chkBatch.Checked = True Then
                            Dim colX As New DataGridViewCheckBoxColumn
                            colX.HeaderText = "Include"
                            colX.Name = "dgcInc"
                            colX.Width = 50
                            colX.DefaultCellStyle.NullValue = False
                            dgvList.Columns.Add(colX)
                            colX.DisplayIndex = 1
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        Finally
            SQL.FlushParams()
        End Try
    End Sub

    Private Function GetQueryCollection(ByVal Type As String) As String
        ' CONDITION OF QUERY
        Dim filter As String = ""
        Dim temp As String = ""
        If cbFilter.SelectedIndex = -1 Then
            filter = " ORDER BY TransNo ASC"
        Else
            Select Case cbFilter.SelectedItem
                Case "Transaction ID"
                    filter = " AND TransNo LIKE '%' + @Filter + '%' ORDER BY TransNo ASC"
                Case "Remarks"
                    filter = " AND Remarks LIKE '%' + @Filter + '%' ORDER BY TransNo ASC"
                Case "Status"
                    filter = " AND tblCollection.Status LIKE '%' + @Filter + '%' ORDER BY TransNo ASC"
                Case "VCE Name"
                    filter = " AND VCEName LIKE '%' + @Filter + '%' ORDER BY TransNo ASC"
                Case "Date Range"
                    filter = " AND tblCollection.DateTrans BETWEEN  @DateFrom AND @DateTo ORDER BY TransNo "
            End Select
        End If

        ' QUERY 
        temp = " SELECT   TransID, tblBranch.Description as Branch, TransNo AS [TransNo.], DateTrans AS [Date], VCEName AS [VCEName], Amount AS [Amount], Remarks, tblCollection.Status  " &
                " FROM     tblCollection LEFT JOIN viewVCE_Master " &
                " ON       tblCollection.VCECode = viewVCE_Master.VCECode " &
                 " LEFT JOIN tblBranch  ON	           " &
                " tblCollection.BranchCode = tblBranch.BranchCode      " &
                " WHERE          " &
                " 	( tblCollection.BranchCode IN  " &
                " 	  (SELECT    DISTINCT tblBranch.BranchCode  " &
                " 	  FROM      tblBranch   " &
                " 	  INNER JOIN tblUser_Access    ON          " &
                " 	  tblBranch.BranchCode = tblUser_Access.Code   " &
                " 	   AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " &
                " 	   AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " &
                " 	   WHERE     UserID ='" & UserID & "'" &
                " 	   ) " &
                "     )   " &
                " AND    tblCollection.TransType ='" & Type & "'  " & filter
        Return temp
    End Function

    Private Sub dgvList_CellBeginEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvList.CellBeginEdit

    End Sub

    Private Sub dgvList_DoubleClick(sender As System.Object, e As System.EventArgs) Handles dgvList.DoubleClick
        ChooseRecord()
    End Sub


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        ChooseRecord()
    End Sub

    Private Sub ChooseRecord()
        If dgvList.SelectedRows.Count = 1 Then
            transID = dgvList.SelectedRows(0).Cells(0).Value.ToString
            itemCode = dgvList.SelectedRows(0).Cells(1).Value.ToString
            batch = chkBatch.Checked
            If cbBranch.Visible = True Then
                BranchCode = Strings.Left(cbBranch.SelectedItem, cbBranch.SelectedItem.ToString.IndexOf(" - "))
            End If
            If moduleID = "PCVRR" Then
                frmCV.LoadPCV(transID)
                Me.Close()
            End If
            Me.Close()
        End If
    End Sub

    Private Sub dgvList_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles dgvList.KeyDown
        If e.KeyCode = Keys.Enter Then
            ChooseRecord()
        ElseIf e.KeyCode = Keys.Space Then
            If chkBatch.Checked = True Then
                If dgvList.SelectedRows.Count > 0 Then
                    If IsNothing(dgvList.SelectedRows(0).Cells(dgvList.Columns.Count - 1).Value) OrElse dgvList.SelectedRows(0).Cells(dgvList.Columns.Count - 1).Value = False Then
                        dgvList.SelectedRows(0).Cells(dgvList.Columns.Count - 1).Value = True
                    Else
                        dgvList.SelectedRows(0).Cells(dgvList.Columns.Count - 1).Value = False
                    End If
                End If
            End If
        End If
    End Sub


    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        LoadData()
    End Sub

    Private Sub txtFilter_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtFilter.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadData()
        End If
    End Sub

    Private Sub frmLoadTransactions_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            ' CHANGE FOCUS TO DATAGRID SELECTION ON WHEN KEY DOWN OR KEY UP IS PRESSED
            Dim RowIndex As Integer = 0
            If dgvList.Focused = False Then
                If dgvList.SelectedRows.Count = 0 Then ' IF THERE ARE NO ROWS SELECTED THEN SELECT A DEFAUL ROW IF THERE ARE EXISTING ROW
                    If dgvList.Rows.Count > 0 Then
                        dgvList.Rows(0).Selected = True
                    End If
                Else
                    If e.KeyCode = Keys.Down Then
                        If dgvList.Rows.Count > dgvList.SelectedRows(0).Index + 1 Then
                            dgvList.Focus()
                            RowIndex = dgvList.SelectedRows(0).Index
                            dgvList.Rows(dgvList.SelectedRows(0).Index).Selected = False
                            dgvList.Rows(RowIndex + 1).Selected = True
                        End If
                    ElseIf e.KeyCode = Keys.Up Then
                        If dgvList.SelectedRows(0).Index > 0 Then
                            dgvList.Rows(dgvList.SelectedRows(0).Index - 1).Selected = True
                        End If
                    End If
                End If
                dgvList.Focus()
            End If
        ElseIf e.KeyValue >= 112 AndAlso e.KeyValue <= 113 Then
            ' If Keypressed is between F1 to F12
            Dim index As Integer = e.KeyValue - 112
            If cbFilter.Items.Count >= (index + 1) Then
                cbFilter.SelectedIndex = index
            End If
        Else
            txtFilter.Focus()
            txtFilter.SelectionStart = txtFilter.TextLength
        End If
    End Sub

    Private Sub chkAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAll.CheckedChanged
        If disableEvent = False Then
            For Each row As DataGridViewRow In dgvList.Rows
                row.Cells(dgvList.Columns.Count - 1).Value = chkAll.Checked
            Next
        End If
    End Sub
    Private Sub txtFilter_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtFilter.TextChanged
        'LoadData()
    End Sub

    Private Sub chkBatch_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkBatch.CheckedChanged
        chkAll.Visible = chkBatch.Checked
        If dgvList.Rows.Count > 0 Then
            If chkBatch.Checked = True Then
                Dim colX As New DataGridViewCheckBoxColumn
                colX.HeaderText = "Include"
                colX.Name = "dgcInc"
                colX.Width = 50
                colX.DefaultCellStyle.NullValue = False
                dgvList.Columns.Add(colX)
                colX.DisplayIndex = 1
            Else
                dgvList.Columns.RemoveAt(dgvList.Columns("dgcInc").Index)
                disableEvent = True
                chkAll.Checked = False
                disableEvent = False
            End If
        End If
    End Sub

    Private Sub dgvList_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList.CellClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            If chkBatch.Checked = True Then
                If IsNothing(dgvList.Item(dgvList.Columns.Count - 1, e.RowIndex).Value) OrElse dgvList.Item(dgvList.Columns.Count - 1, e.RowIndex).Value = False Then
                    dgvList.Item(dgvList.Columns.Count - 1, e.RowIndex).Value = True
                Else
                    dgvList.Item(dgvList.Columns.Count - 1, e.RowIndex).Value = False
                End If
            End If
        End If
    End Sub
    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cbFilter_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbFilter.SelectedIndexChanged
        If cbFilter.SelectedItem = "Date Range" Then
            dtpDateFrom.Visible = True
            dtpDateTo.Visible = True
            txtFilter.Visible = False
        Else
            txtFilter.Visible = True
            dtpDateFrom.Visible = False
            dtpDateTo.Visible = False
        End If
    End Sub

    Private Sub cbCostCenter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCostCenter.SelectedIndexChanged

        If cbCostCenter.SelectedIndex <> -1 Then
            LoadData()
            ResCenter = cbCostCenter.SelectedItem
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("TL-CC", ResCenter)
        f.Dispose()
    End Sub



    'Private Sub LoadCostCenter(cb As ComboBox)
    '    Dim query As String
    '    query = " SELECT DISTINCT Code FROM tblCC WHERE Status = 'Active'  ORDER BY Code "
    '    SQL.ReadQuery(query)
    '    cb.Items.Clear()
    '    If cb Is cbCostCenter Then
    '        cb.Items.Add("ALL")
    '    End If
    '    'cb.Items.Add("ALL")
    '    While SQL.SQLDR.Read
    '        cb.Items.Add(SQL.SQLDR("Description").ToString)
    '    End While
    '    If cb.Items.Count > 0 Then
    '        cb.SelectedIndex = 0
    '    End If
    '    cb.Enabled = True
    'End Sub

    'Public Function GetCostCenter(ByVal Code As String) As String
    '    Dim query As String
    '    query = " SELECT Code FROM tblCC WHERE Code ='" & Code & "'"
    '    SQL.ReadQuery(query)
    '    If SQL.SQLDR.Read Then
    '        Return SQL.SQLDR("Code").ToString
    '    Else
    '        Return ""
    '    End If
    'End Function

    'Private Sub dgvList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellContentClick

    'End Sub
End Class