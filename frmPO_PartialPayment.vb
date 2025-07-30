Public Class frmPO_PartialPayment
    Public TotalAmount As Decimal
    Public TransID As String
    Dim validate As Boolean = False


    Private Sub frmPO_PartialPayment_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
     
        If TransID <> "" Then
            LoadPO_Partial(TransID)
        Else
            If cbType.Items.Count > 0 Then
                cbType.SelectedIndex = 0
            End If
            If cbDownPayment_Type.Items.Count > 0 Then
                cbDownPayment_Type.SelectedIndex = 0
            End If
            EnableControl(True)
        End If

    End Sub

    Private Sub LoadPO_Partial(ByVal ID As String)

        Dim query As String
        query = " SELECT  TransID, PaymentType, Method, DP_Type, DP_Terms_Percent, DP_Amount, PO_TotalAmount, NoOfMonths " & _
                " FROM    tblPO_PartialPayment_Header " & _
                " WHERE   TransID = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            TransID = SQL.SQLDR("TransID").ToString
            TotalAmount = CDec(SQL.SQLDR("PO_TotalAmount")).ToString("N2")
            cbType.SelectedItem = SQL.SQLDR("PaymentType").ToString
            cbMethod.SelectedItem = SQL.SQLDR("Method").ToString
            cbDownPayment_Type.SelectedItem = SQL.SQLDR("DP_Type").ToString
            txtDownpayment_Percent.Text = CDec(SQL.SQLDR("DP_Terms_Percent")).ToString("N2")
            txtDownpayment_Amount.Text = CDec(SQL.SQLDR("DP_Amount")).ToString("N2")
            txtNoOfMonths.Text = CDec(SQL.SQLDR("NoOfMonths")).ToString("N2")
            LoadDetails(TransID)
            EnableControl(True)

            If cbType.SelectedItem = "Monthly" Then
                dgvMonthly.AllowUserToAddRows = False
                dgvMonthly.AllowUserToDeleteRows = False
                dgvMonthly.Columns(dgcM_Terms.Index).ReadOnly = True
                dgvMonthly.Columns(dgcM_Terms.Index).DefaultCellStyle.Format = "#.##"
                dgvMonthly.Columns(dgcT_Amount.Index).DefaultCellStyle.Format = "#.##"
                dgvMonthly.Columns(dgcM_Terms.Index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                dgvMonthly.Columns(dgcT_Amount.Index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Else
                dgvMonthly.AllowUserToAddRows = True
                dgvMonthly.AllowUserToDeleteRows = True
                dgvMonthly.Columns(dgcM_Terms.Index).DefaultCellStyle.Format = "d"
                dgvMonthly.Columns(dgcT_Amount.Index).DefaultCellStyle.Format = "#.##"
                dgvMonthly.Columns(dgcM_Terms.Index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                dgvMonthly.Columns(dgcT_Amount.Index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
        End If

    End Sub

    Private Sub LoadDetails(ByVal ID As Integer)
        Dim query As String


        query = " SELECT TransID, Des_Terms, Value  " & _
                " FROM   tblPO_PartialPayment_Details WHERE TransID = " & ID & ""
        SQL.ReadQuery(query)
        dgvMonthly.Rows.Clear()

        Dim rowsCount As Integer = 0
        If SQL.SQLDR.HasRows Then
            While SQL.SQLDR.Read
                dgvMonthly.Rows.Add(SQL.SQLDR("Des_Terms").ToString)
                dgvMonthly.Rows(rowsCount).Cells(dgcT_Amount.Index).Value = CDec(SQL.SQLDR("Value")).ToString("N2")
                rowsCount += 1
            End While

        End If

       
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)

        cbType.Enabled = Value
        cbMethod.Enabled = Value
        txtDownpayment_Percent.Enabled = Value
        cbDownPayment_Type.Enabled = Value
        'dgvEntry.Enabled = Value
        dgvMonthly.AllowUserToAddRows = Value
        dgvMonthly.AllowUserToDeleteRows = Value
        If Value = True Then
            dgvMonthly.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvMonthly.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
    End Sub


    Private Sub cbDownPayment_Type_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbDownPayment_Type.SelectedIndexChanged

        If cbDownPayment_Type.SelectedItem = "Amount" Then
            txtDownpayment_Amount.Enabled = True
            txtDownpayment_Percent.Visible = False
            lblDownpaymen_Percent.Visible = False
            txtDownpayment_Percent.Text = "0.00"
        Else
            txtDownpayment_Amount.Enabled = False
            txtDownpayment_Percent.Visible = True
            lblDownpaymen_Percent.Visible = True
            txtDownpayment_Amount.Text = "0.00"
        End If
    End Sub


    Private Sub cbType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbType.SelectedIndexChanged

        If cbType.SelectedItem = "Monthly" Then
            dgvMonthly.Columns(dgcM_Terms.Index).HeaderText = "Terms"
            dgvMonthly.Columns(dgcT_Amount.Index).HeaderText = "Amount"
            lblMethod.Visible = True
            cbMethod.Visible = False
            lblMethod.Text = "No. of Months :"
            txtNoOfMonths.Visible = True
        Else
            dgvMonthly.Columns(dgcM_Terms.Index).HeaderText = "Description"
            dgvMonthly.Columns(dgcT_Amount.Index).HeaderText = "Value"
            lblMethod.Visible = True
            cbMethod.Visible = True
            lblMethod.Text = "Method :"
            txtNoOfMonths.Visible = False
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If validate = True Then
            Me.Hide()
        Else
            Msg("Please check total amount/percentage value.", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub txtDownpayment_Percent_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtDownpayment_Percent.KeyDown
        Try
            If e.KeyValue <> 8 And Not (e.KeyValue >= 48 And e.KeyValue <= 57) And Not (e.KeyCode >= 96 And e.KeyCode <= 105) And Not (e.KeyCode = 110) Then
                e.SuppressKeyPress = True
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtDownpayment_Percent_MouseWheel(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles txtDownpayment_Percent.MouseWheel

    End Sub

    Private Sub txtDownpayment_Percent_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtDownpayment_Percent.TextChanged

        If IsNumeric(txtDownpayment_Percent.Text) Then
            If CDec(txtDownpayment_Percent.Text) > 100 Then
                txtDownpayment_Percent.Text = "100.00"
                txtDownpayment_Percent.SelectionStart = txtDownpayment_Percent.TextLength
            ElseIf CDec(txtDownpayment_Percent.Text) > 0 Then
                txtDownpayment_Amount.Text = CDec((TotalAmount) * (txtDownpayment_Percent.Text) / 100.0).ToString("N2")
            End If
        Else
            txtDownpayment_Amount.Text = "0.00"
        End If
    End Sub

    Private Sub cbMethod_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbMethod.SelectedIndexChanged

    End Sub

    Private Sub txtNoOfMonths_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtNoOfMonths.KeyDown

        Try

            If e.KeyCode = Keys.Enter Then
                If cbType.SelectedItem = "Monthly" Then
                    dgvMonthly.Rows.Clear()
                    For i As Integer = 1 To txtNoOfMonths.Text
                        dgvMonthly.Rows.Add(i, "")
                    Next
                    dgvMonthly.AllowUserToAddRows = False
                    dgvMonthly.AllowUserToDeleteRows = False
                    dgvMonthly.Columns(dgcM_Terms.Index).ReadOnly = True
                    dgvMonthly.Columns(dgcM_Terms.Index).DefaultCellStyle.Format = "#.##"
                    dgvMonthly.Columns(dgcT_Amount.Index).DefaultCellStyle.Format = "#.##"
                    dgvMonthly.Columns(dgcM_Terms.Index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvMonthly.Columns(dgcT_Amount.Index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Else
                    dgvMonthly.AllowUserToAddRows = True
                    dgvMonthly.AllowUserToDeleteRows = True
                    dgvMonthly.Columns(dgcM_Terms.Index).DefaultCellStyle.Format = "d"
                    dgvMonthly.Columns(dgcT_Amount.Index).DefaultCellStyle.Format = "#.##"
                    dgvMonthly.Columns(dgcM_Terms.Index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    dgvMonthly.Columns(dgcT_Amount.Index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
            ElseIf Not (e.KeyValue >= 48 And e.KeyValue <= 57) And Not (e.KeyCode >= 96 And e.KeyCode <= 105) Then
                e.SuppressKeyPress = True
            End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, "")
        End Try
    End Sub

    Private Sub txtNoOfMonths_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtNoOfMonths.TextChanged

    End Sub

    Private Sub dgvMonthly_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMonthly.CellContentClick

    End Sub

    Private Sub dgvMonthly_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMonthly.CellEndEdit
        Try
            Dim rowIndex As Integer = dgvMonthly.CurrentCell.RowIndex
            Dim colindex As Integer = dgvMonthly.CurrentCell.ColumnIndex
            Select Case colindex
                Case dgcT_Amount.Index
                    ComputeTotal()
            End Select
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, "")
        End Try
    End Sub

    Private Sub ComputeTotal()
        Dim total As Decimal = 0
        For Each row As DataGridViewRow In dgvMonthly.Rows
            total += CDec(row.Cells(dgcT_Amount.Index).Value).ToString()
        Next
        If cbMethod.SelectedItem = "Percent" And cbType.SelectedItem = "Progressive" Then
            If total > 100 Then
                Msg("Value should be 100% only.", MsgBoxStyle.Exclamation)
                validate = False
            Else
                validate = True
            End If
        Else
            total += CDec(txtDownpayment_Amount.Text).ToString("N2")
            If total > TotalAmount Then
                Msg("Amount should be not more than net amount.", MsgBoxStyle.Exclamation)
                validate = False
            Else
                validate = True
            End If
        End If
    End Sub

    Private Sub txtDownpayment_Amount_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtDownpayment_Amount.TextChanged
        ComputeTotal()
    End Sub
End Class