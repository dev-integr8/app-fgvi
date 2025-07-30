Public Class frmFormMaintenance

    Private Sub frmFormMaintenance_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadList()
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        If Value = True Then
            dgvEntry.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvEntry.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
    End Sub

    Private Sub LoadList()
        Dim query As String
        query = "SELECT * FROM tblForms"
        SQL.ReadQuery(query)
        dgvEntry.Rows.Clear()
        While SQL.SQLDR.Read
            dgvEntry.Rows.Add(SQL.SQLDR("FormName").ToString, SQL.SQLDR("Verified_By").ToString, SQL.SQLDR("Verified_By_Position").ToString, _
                             SQL.SQLDR("Approved_By").ToString, SQL.SQLDR("Approved_By_Position").ToString)
        End While


        tsbEdit.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
        EnableControl(False)
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click
        EnableControl(True)
        tsbEdit.Enabled = False
        tsbSave.Enabled = True
        tsbClose.Enabled = True
        tsbExit.Enabled = False
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        EnableControl(False)
        tsbEdit.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
        LoadList()
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If MsgBox("Saving Record?, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
            UpdateForm()
            Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
            LoadList()
        End If
    End Sub

    Private Sub UpdateForm()
        Dim updateSQL, FormName As String
        For Each item As DataGridViewRow In dgvEntry.Rows
            If item.Cells(chForm.Index).Value <> Nothing Then
                FormName = item.Cells(chForm.Index).Value
                updateSQL = " UPDATE tblForms " & _
                           " SET    Approved_By = @Approved_By, Approved_By_Position = @Approved_By_Position, Verified_By = @Verified_By, " & _
                           "        Verified_By_Position = @Verified_By_Position" & _
                           " WHERE  FormName = @FormName "
                SQL.FlushParams()
                SQL.AddParam("@FormName", FormName)
                If item.Cells(chApproved.Index).Value <> Nothing AndAlso item.Cells(chApproved.Index).Value <> "" Then
                    SQL.AddParam("@Approved_By", item.Cells(chApproved.Index).Value.ToString)
                Else
                    SQL.AddParam("@Approved_By", "")
                End If
                If item.Cells(chApprovedPosition.Index).Value <> Nothing AndAlso item.Cells(chApprovedPosition.Index).Value <> "" Then
                    SQL.AddParam("@Approved_By_Position", item.Cells(chApprovedPosition.Index).Value.ToString)
                Else
                    SQL.AddParam("@Approved_By_Position", "")
                End If
                If item.Cells(chVerified.Index).Value <> Nothing AndAlso item.Cells(chVerified.Index).Value <> "" Then
                    SQL.AddParam("@Verified_By", item.Cells(chVerified.Index).Value.ToString)
                Else
                    SQL.AddParam("@Verified_By", "")
                End If
                If item.Cells(chVerifiedPosition.Index).Value <> Nothing AndAlso item.Cells(chVerifiedPosition.Index).Value <> "" Then
                    SQL.AddParam("@Verified_By_Position", item.Cells(chVerifiedPosition.Index).Value.ToString)
                Else
                    SQL.AddParam("@Verified_By_Position", "")
                End If
                SQL.ExecNonQuery(updateSQL)
            End If
        Next
    End Sub

    Private Sub dgvEntry_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntry.CellContentClick

    End Sub

    Private Sub dgvEntry_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntry.CellEndEdit
        Dim rowIndex As Integer = dgvEntry.CurrentCell.RowIndex
        Dim colIndex As Integer = dgvEntry.CurrentCell.ColumnIndex
        Try
            Select Case colIndex

                Case chVerified.Index
                    Dim f As New frmVCE_Search
                    f.txtFilter.Text = dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                    f.ShowDialog()
                    dgvEntry.Item(chVerified.Index, e.RowIndex).Value = f.VCEName
                    f.Dispose()
                Case chApproved.Index
                    Dim f As New frmVCE_Search
                    f.txtFilter.Text = dgvEntry.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                    f.ShowDialog()
                    dgvEntry.Item(chApproved.Index, e.RowIndex).Value = f.VCEName
                    f.Dispose()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class