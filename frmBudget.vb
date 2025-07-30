Public Class frmBudget

    Private Sub frmBudget_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control = True Then
            If e.KeyCode = Keys.S Then
                If tsbSave.Enabled = True Then tsbSave.PerformClick()
            ElseIf e.KeyCode = Keys.N Then
                If tsbNew.Enabled = True Then tsbNew.PerformClick()
            ElseIf e.KeyCode = Keys.E Then
                If tsbEdit.Enabled = True Then tsbEdit.PerformClick()
            ElseIf e.KeyCode = Keys.P Then
                If tsbPrint.Enabled = True Then tsbPrint.PerformClick()
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

    Private Sub frmBudget_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'toolstrip
        tsbNew.Enabled = True
        tsbEdit.Enabled = False
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
        tsbPrint.Enabled = False
        EnableControl(False)
    End Sub
    Private Sub LoadBudgetEntry()
        Dim query As String
        query = " SELECT TblBudget.AccountCode, Accounttitle, Amount, AppDate  " & vbCrLf &
                " FROM TblBudget " & vbCrLf &
                " INNER JOIN tblCOA_Master " & vbCrLf &
                " ON TblBudget.AccountCode = tblCOA_Master.AccountCode " & vbCrLf &
                " WHERE YEAR(Appdate) = '" & nupYear.Value & "' " & vbCrLf &
                " ORDER BY OrderNo "
        SQL.ReadQuery(query)
        dgvEntry.Rows.Clear()
            Dim rowsCount As Integer = 0
            If SQL.SQLDR.HasRows Then
                While SQL.SQLDR.Read
                    dgvEntry.Rows.Add(SQL.SQLDR("AccountCode").ToString)
                    dgvEntry.Rows(rowsCount).Cells(dgcAccntTitle.Index).Value = SQL.SQLDR("Accounttitle").ToString
                    dgvEntry.Rows(rowsCount).Cells(dgcAmount.Index).Value = CDec(SQL.SQLDR("Amount")).ToString("N2")
                rowsCount += 1
            End While
            'toolstrip
            tsbNew.Enabled = True
            tsbEdit.Enabled = True
            tsbSave.Enabled = False
            tsbClose.Enabled = True
            tsbExit.Enabled = True
            tsbPrint.Enabled = True
            EnableControl(True)
        Else
            MsgBox("Year" & " " & nupYear.Value & " has no record!", MsgBoxStyle.Exclamation)
            EnableControl(True)
            'toolstrip
            tsbNew.Enabled = True
            tsbEdit.Enabled = False
            tsbSave.Enabled = False
            tsbClose.Enabled = False
            tsbExit.Enabled = True
            tsbPrint.Enabled = False
            EnableControl(False)
        End If
            TotalAmount()
            
    End Sub
    Private Sub LoadAccounttle()
        Dim query As String
        query = " SELECT AccountCode, AccountTitle" & vbCrLf &
                " FROM tblCOA_Master " & vbCrLf &
                " WHERE AccountType = 'Income Statement' " & vbCrLf &
                " and AccountGroup = 'SubAccount' "
        SQL.ReadQuery(query)
        dgvEntry.Rows.Clear()
        Dim rowsCount As Integer = 0
        If SQL.SQLDR.HasRows Then
            While SQL.SQLDR.Read
                dgvEntry.Rows.Add(SQL.SQLDR("AccountCode").ToString)
                dgvEntry.Rows(rowsCount).Cells(dgcAccntTitle.Index).Value = SQL.SQLDR("Accounttitle").ToString
                rowsCount += 1
            End While
        End If
        
    End Sub
    Private Sub EnableControl(ByVal Value As Boolean)
        dgvEntry.AllowUserToAddRows = Value
        dgvEntry.AllowUserToDeleteRows = Value
        If Value = True Then
            dgvEntry.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvEntry.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
    End Sub

    Private Sub ClearText()
        dgvEntry.Rows.Clear()
    End Sub

    Private Sub tsbNew_Click(sender As Object, e As EventArgs) Handles tsbNew.Click
        ' Toolstrip Buttons
        tsbNew.Enabled = False
        tsbEdit.Enabled = False
        tsbSave.Enabled = True
        tsbClose.Enabled = True
        tsbExit.Enabled = False
        tsbPrint.Enabled = False
        EnableControl(True)
        LoadAccounttle()
    End Sub

    Private Sub tsbEdit_Click(sender As Object, e As EventArgs) Handles tsbEdit.Click
        ' Toolstrip Buttons
        tsbNew.Enabled = False
        tsbEdit.Enabled = False
        tsbSave.Enabled = True
        tsbSave.Text = "Update"
        tsbClose.Enabled = True
        tsbExit.Enabled = False
        tsbPrint.Enabled = False
        EnableControl(True)
    End Sub

    Private Sub tsbClose_Click(sender As Object, e As EventArgs) Handles tsbClose.Click
        tsbNew.Enabled = True
        tsbEdit.Enabled = False
        tsbSave.Enabled = False
        tsbSave.Text = "Save"
        tsbClose.Enabled = False
        tsbExit.Enabled = True
        ClearText()
    End Sub

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub dgvEntry_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEntry.CellContentClick

    End Sub
    Private Sub TotalAmount()
        ' COMPUTE TOTAL AMOUNT
        Dim db As Decimal = 0

        For i As Integer = 0 To dgvEntry.Rows.Count - 1
            If Val(dgvEntry.Item(dgcAmount.Index, i).Value) <> 0 Then
                db = db + Double.Parse(dgvEntry.Item(dgcAmount.Index, i).Value).ToString("N2")
            End If
        Next
        txtTotalAmount.Text = db.ToString("N2")
    End Sub

    Private Sub dgvEntry_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEntry.CellEndEdit
        Dim rowIndex As Integer = dgvEntry.CurrentCell.RowIndex
        Dim colIndex As Integer = dgvEntry.CurrentCell.ColumnIndex
        Dim Accountcode As String
        Try
            Select Case colIndex
                Case dgcAccntCode.Index
                    'dgvEntry.Item(dgcAccntTitle.Index, e.RowIndex).Value = GetAccntTitle((dgvEntry.Item(dgcAccntCode.Index, e.RowIndex).Value))
                    Accountcode = dgvEntry.Item(dgcAccntCode.Index, e.RowIndex).Value
                    Dim f As New frmCOA_Search
                    f.accttile = Accountcode
                    f.ShowDialog("AccntCode", Accountcode)
                    dgvEntry.Item(dgcAccntCode.Index, e.RowIndex).Value = f.accountcode
                    dgvEntry.Item(dgcAccntTitle.Index, e.RowIndex).Value = f.accttile
                    f.Dispose()
                   
                Case dgcAccntTitle.Index
                    Accountcode = dgvEntry.Item(dgcAccntTitle.Index, e.RowIndex).Value
                    Dim f As New frmCOA_Search
                    f.accttile = Accountcode
                    f.ShowDialog("AccntTitle", Accountcode)
                    dgvEntry.Item(dgcAccntCode.Index, e.RowIndex).Value = f.accountcode
                    dgvEntry.Item(dgcAccntTitle.Index, e.RowIndex).Value = f.accttile
                    f.Dispose()
            End Select
            TotalAmount()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function IfExist(ByVal ID As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblBudget WHERE YEAR(Appdate) ='" & nupYear.Value & "'  "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub SaveEntry()
        Dim insertSQL As String
        activityStatus = True
        Dim line As Integer = 1
        For Each item As DataGridViewRow In dgvEntry.Rows
            If item.Cells(dgcAccntCode.Index).Value <> Nothing Then
                insertSQL = " INSERT INTO " & _
                            " TblBudget(AccountCode, Amount, Appdate) " & _
                            " VALUES(@AccountCode, @Amount, @Appdate)"
                SQL.FlushParams()
                SQL.AddParam("@AccountCode", item.Cells(dgcAccntCode.Index).Value.ToString)
                If item.Cells(dgcAmount.Index).Value <> Nothing AndAlso IsNumeric(item.Cells(dgcAmount.Index).Value) Then
                    SQL.AddParam("@Amount", CDec(item.Cells(dgcAmount.Index).Value))
                Else
                    SQL.AddParam("@Amount", 0)
                End If
                SQL.AddParam("@Appdate", nupYear.Value)
                SQL.ExecNonQuery(insertSQL)
                line += 1
            End If
        Next
    End Sub
    Private Sub UpdateEntry()
        ' DELETE ACCOUNTING ENTRIES
        Dim deleteSQL As String
        deleteSQL = " DELETE FROM TblBudget  WHERE  YEAR(Appdate) = '" & nupYear.Value & "' "
        SQL.FlushParams()
        SQL.AddParam("@Appdate", nupYear.Value)
        SQL.ExecNonQuery(deleteSQL)

        Dim insertSQL As String
        activityStatus = True
        Dim line As Integer = 1
        For Each item As DataGridViewRow In dgvEntry.Rows
            If item.Cells(dgcAccntCode.Index).Value <> Nothing Then
                insertSQL = " INSERT INTO " & _
                            " TblBudget(AccountCode, Amount, Appdate) " & _
                            " VALUES(@AccountCode, @Amount, @Appdate)"
                SQL.FlushParams()
                SQL.AddParam("@AccountCode", item.Cells(dgcAccntCode.Index).Value.ToString)
                If item.Cells(dgcAmount.Index).Value <> Nothing AndAlso IsNumeric(item.Cells(dgcAmount.Index).Value) Then
                    SQL.AddParam("@Amount", CDec(item.Cells(dgcAmount.Index).Value))
                Else
                    SQL.AddParam("@Amount", 0)
                End If
                SQL.AddParam("@Appdate", nupYear.Value)
                SQL.ExecNonQuery(insertSQL)
                line += 1
            End If
        Next
    End Sub

    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
      If tsbSave.Text = "Save" Then
            If IfExist(nupYear.Value) Then
                MsgBox("Year" & " " & nupYear.Value & " already exist!", MsgBoxStyle.Exclamation)
            Else
                SaveEntry()
                Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                tsbSave.Text = "Update"
            End If
        Else
            UpdateEntry()
            Msg("Update Record Saved Succesfully!", MsgBoxStyle.Information)
            tsbSave.Text = "Save"
        End If
        LoadBudgetEntry()
    End Sub
    Private Sub btnSearchVCE_Click(sender As Object, e As EventArgs) Handles btnSearchVCE.Click
        LoadBudgetEntry()
    End Sub

End Class