Public Class frmFund_Maintenance

    Dim SQL As New SQLControl
    Dim strCode As String = ""

    Private Sub ClearAll()
        strCode = ""
        txtDesc.Text = ""
        nudMonthsDelay.Value = 0
        pnlSavings.Enabled = False
        dgvPercent.Rows.Clear()
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        ClearAll()
        tsbEdit.Enabled = False
        tsbSave.Enabled = True
        tsbClose.Enabled = True
        tsbDelete.Enabled = False

        pnlSavings.Enabled = True
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Dispose()
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        ClearAll()
        tsbEdit.Enabled = False
        tsbSave.Enabled = False
        tsbDelete.Enabled = False
        tsbClose.Enabled = False
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        pnlSavings.Enabled = True
        tsbEdit.Enabled = False
        tsbSave.Enabled = True
        tsbDelete.Enabled = False
        tsbClose.Enabled = True
    End Sub

    Private Sub frmFund_Maintenance_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadFunds()
    End Sub

    Private Sub LoadFunds()
        Dim selectSQL As String = " SELECT * FROM tblFund ORDER BY Code "
        SQL.ReadQuery(selectSQL)
        dgvFunds.Rows.Clear()
        While SQL.SQLDR.Read
            dgvFunds.Rows.Add(SQL.SQLDR("Code").ToString, SQL.SQLDR("Description").ToString, SQL.SQLDR("MonthsDelay").ToString, SQL.SQLDR("ShareCapital").ToString)
        End While
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        Dim total As Decimal = 0
        For Each row As DataGridViewRow In dgvPercent.Rows
            If row.Cells(colAccntCode.Index).Value <> "" And row.Cells(colDebit.Index).Value = True Then
                total += CDec(row.Cells(colPercent.Index).Value)
            End If
        Next
        If total = 100 Then
            If strCode <> "" Then
                Dim updateSQL As String = "UPDATE tblFund SET MonthsDelay = '" & nudMonthsDelay.Value & "', Description = '" & txtDesc.Text & "', ShareCapital = '" & nudShare.Value & "' WHERE Code = '" & strCode & "' "
                SQL.ExecNonQuery(updateSQL)
                Dim deleteSQL As String = "DELETE FROM tblFund_Details WHERE Code = '" & strCode & "'"
                SQL.ExecNonQuery(deleteSQL)
                For Each row As DataGridViewRow In dgvPercent.Rows
                    If row.Cells(colAccntCode.Index).Value <> "" Then
                        Dim insertSQL As String = " INSERT INTO tblFund_Details(Code, AccntCode, Minimum, PercentRate, Member, Debit) " & vbCrLf & _
                                    " VALUES('" & strCode & "', '" & row.Cells(colAccntCode.Index).Value & "', '" & row.Cells(colMinimum.Index).Value & "', '" & row.Cells(colPercent.Index).Value & "', '" & row.Cells(colMember.Index).Value & "', '" & row.Cells(colDebit.Index).Value & "') "
                        SQL.ExecNonQuery(insertSQL)
                    End If
                Next
                MsgBox("Successfully Updated!", MsgBoxStyle.Information, "Save")
                ClearAll()
                LoadFunds()
                tsbEdit.Enabled = False
                tsbSave.Enabled = False
                tsbClose.Enabled = False
                tsbDelete.Enabled = False
            Else
                Dim insertSQL As String = "INSERT INTO tblFund(Description, MonthsDelay, ShareCapital) VALUES('" & txtDesc.Text & "', '" & nudMonthsDelay.Value & "', '" & nudShare.Value & "')"
                SQL.ExecNonQuery(insertSQL)
                Dim selectSQL As String = "SELECT TOP 1 Code FROM tblFund WHERE Description = '" & txtDesc.Text & "' ORDER BY Code DESC"
                SQL.ReadQuery(selectSQL)
                Dim strCode1 As String = ""
                While SQL.SQLDR.Read
                    strCode1 = SQL.SQLDR("Code").ToString
                End While
                For Each row As DataGridViewRow In dgvPercent.Rows
                    If row.Cells(colAccntCode.Index).Value <> "" Then
                        insertSQL = " INSERT INTO tblFund_Details(Code, AccntCode, Minimum, PercentRate, Member, Debit) " & vbCrLf & _
                                    " VALUES('" & strCode1 & "', '" & row.Cells(colAccntCode.Index).Value & "', '" & row.Cells(colMinimum.Index).Value & "', '" & row.Cells(colPercent.Index).Value & "', '" & row.Cells(colMember.Index).Value & "', '" & row.Cells(colDebit.Index).Value & "') "
                        SQL.ExecNonQuery(insertSQL)
                    End If
                Next
                MsgBox("Successfully Saved!", MsgBoxStyle.Information, "Save")
                ClearAll()
                LoadFunds()
                tsbEdit.Enabled = False
                tsbSave.Enabled = False
                tsbClose.Enabled = False
                tsbDelete.Enabled = False
            End If
        Else
            MsgBox("Invalid Total (" & total & ")!" & vbCrLf & "Debit must be 100 percent in total", MsgBoxStyle.Information, "Error")
        End If
    End Sub

    Private Sub dgvPercent_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPercent.CellEndEdit
        Select Case e.ColumnIndex
            Case colAccntTitle.Index
                Dim f As New frmCOA_Search
                f.ShowDialog("AccntTitle", dgvPercent.Rows(e.RowIndex).Cells(colAccntTitle.Index).Value)
                If f.accttile <> "" And f.accountcode <> "" Then
                    dgvPercent.Rows(e.RowIndex).Cells(colAccntCode.Index).Value = f.accountcode
                    dgvPercent.Rows(e.RowIndex).Cells(colAccntTitle.Index).Value = f.accttile
                    dgvPercent.Rows(e.RowIndex).Cells(colMinimum.Index).Value = 0
                    dgvPercent.Rows(e.RowIndex).Cells(colPercent.Index).Value = 0
                Else
                    dgvPercent.Rows.RemoveAt(e.RowIndex)
                End If
            Case colMinimum.Index
                If Not IsNumeric(dgvPercent.Rows(e.RowIndex).Cells(colMinimum.Index).Value) Then
                    dgvPercent.Rows(e.RowIndex).Cells(colMinimum.Index).Value = 0
                End If
            Case colPercent.Index
                If Not IsNumeric(dgvPercent.Rows(e.RowIndex).Cells(colPercent.Index).Value) Then
                    dgvPercent.Rows(e.RowIndex).Cells(colPercent.Index).Value = 0
                End If
        End Select
    End Sub

    Private Sub dgvFunds_Click(sender As Object, e As System.EventArgs) Handles dgvFunds.Click
        strCode = dgvFunds.Rows(dgvFunds.SelectedCells(0).RowIndex).Cells(colCode.Index).Value
        If strCode <> "" Then
            txtDesc.Text = dgvFunds.Rows(dgvFunds.SelectedCells(0).RowIndex).Cells(colDescription.Index).Value
            nudMonthsDelay.Value = dgvFunds.Rows(dgvFunds.SelectedCells(0).RowIndex).Cells(colMonthsDelay.Index).Value
            nudShare.Value = dgvFunds.Rows(dgvFunds.SelectedCells(0).RowIndex).Cells(colShareCapital.Index).Value
            Dim selectSQL As String = " SELECT AccntCode, AccountTitle, Minimum, PercentRate, Member, Debit FROM tblFund_Details " & vbCrLf & _
                                      " INNER JOIN tblCOA_Master ON tblFund_Details.Accntcode = tblCOA_Master.AccountCode " & vbCrLf & _
                                      " WHERE Code = '" & strCode & "' ORDER BY Code "
            SQL.ReadQuery(selectSQL)
            dgvPercent.Rows.Clear()
            While SQL.SQLDR.Read
                dgvPercent.Rows.Add(SQL.SQLDR("AccntCode").ToString, SQL.SQLDR("AccountTitle").ToString, SQL.SQLDR("Minimum").ToString, SQL.SQLDR("PercentRate").ToString, SQL.SQLDR("Member").ToString, SQL.SQLDR("Debit").ToString)
            End While
            tsbEdit.Enabled = True
        End If
    End Sub

    Private Sub dgvFunds_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFunds.CellContentClick

    End Sub
End Class