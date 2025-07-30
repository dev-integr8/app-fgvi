Public Class frmLoadList
    Dim moduleID As String
    Public transID As String = ""
    Public itemCode As String = ""
    Public batch As Boolean = False

    Public Overloads Function ShowDialog(ByVal ModID As String) As Boolean
        moduleID = ModID
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmLoadTransactions_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            Dim filter As String = ""
            Dim query As String = ""
            dgvList.DataSource = Nothing
            If dgvList.Columns.Count > 0 Then
                dgvList.Columns.Clear()
            End If

            Select Case moduleID
                ' ** LOANS **
                Case "Loan Approval"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND tblSO.SO_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND VCEName '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblLoan.Status LIKE '%Active%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   CAST(tblLoan.TransID AS nvarchar)  AS RecordID, CAST(tblLoan.Loan_No AS nvarchar)  AS [Loan No.],  " & _
                            "          DateLoan AS [Date], VCEName AS [Member], tblLoan.LoanAmount, Terms, tblLoan.Status  " & _
                            " FROM     tblLoan LEFT JOIN viewVCE_Master " & _
                            " ON	   tblLoan.VCECode = viewVCE_Master.VCECode " & _
                            " WHERE    tblLoan.Status = 'Active' " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "Loan Release"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND tblSO.SO_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND VCEName '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblLoan.Status LIKE '%Approved%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   CAST(tblLoan.TransID AS nvarchar)  AS RecordID, CAST(tblLoan.Loan_No AS nvarchar)  AS [Loan No.],  " & _
                            "          DateLoan AS [Date], VCEName AS [Member], tblLoan.LoanAmount, Terms, tblLoan.Status  " & _
                            " FROM     tblLoan LEFT JOIN viewVCE_Master " & _
                            " ON	   tblLoan.VCECode = viewVCE_Master.VCECode " & _
                            " WHERE    tblLoan.Status = 'Approved' " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "Loan Active"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND tblSO.SO_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND VCEName '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblLoan.Status LIKE '%Approved%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   CAST(tblLoan.TransID AS nvarchar)  AS RecordID, CAST(tblLoan.Loan_No AS nvarchar)  AS [Loan No.],  " & _
                            "          DateLoan AS [Date], VCEName AS [Member], tblLoan.LoanAmount, Terms, tblLoan.Status  " & _
                            " FROM     tblLoan LEFT JOIN viewVCE_Master " & _
                            " ON	   tblLoan.VCECode = viewVCE_Master.VCECode " & _
                            " WHERE    tblLoan.Status = 'Approved' " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

            End Select
            If query <> "" Then
                SQL.GetQuery(query)
                If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                    dgvList.DataSource = SQL.SQLDS.Tables(0)
                    dgvList.Columns(0).Visible = False
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
            filter = " "
        Else
            Select Case cbFilter.SelectedItem
                Case "Transaction ID"
                    filter = " AND TransNo LIKE '%' + @Filter + '%' "
                Case "Remarks"
                    filter = " AND Remarks '%' + @Filter + '%' "
                Case "Status"
                    filter = " AND tblCollection.Status LIKE '%' + @Filter + '%' "
            End Select
        End If

        ' QUERY 
        temp = " SELECT   TransID, TransNo AS [TransNo.], DateTrans AS [Date], VCEName AS [VCEName], Remarks, Amount AS [Amount], tblCollection.Status  " & _
                " FROM     tblCollection LEFT JOIN tblVCE_Master " & _
                " ON       tblCollection.VCECode = tblVCE_Master.VCECode " & _
                " WHERE    tblCollection.TransType ='" & Type & "'  " & filter
        Return temp
    End Function

    Private Sub dgvList_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dgvList.CurrentCellDirtyStateChanged

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
            Me.Close()
        End If
    End Sub

    Private Sub dgvList_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles dgvList.KeyDown
        If e.KeyCode = Keys.Enter Then
            ChooseRecord()
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
        Else
            txtFilter.Focus()
            txtFilter.SelectionStart = txtFilter.TextLength
        End If
    End Sub

    Private Sub chkBatch_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkBatch.CheckedChanged
        For Each row As DataGridViewRow In dgvList.Rows
            row.Cells(dgvList.Columns.Count - 1).Value = chkBatch.Checked
        Next
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
End Class