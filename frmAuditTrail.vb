Public Class frmAuditTrail
    Dim SQL As New SQLControl

    Private Sub frmAuditTrail_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        dtpFrom.Value = dtpFrom.Value.Date
        LoadUser()
        LoadActivity()
        LoadRefType()
    End Sub

    Private Sub LoadRefType()

        Dim user As String = GetUserID()
        Dim query As String
        query = "SELECT DISTINCT tblAuditTrail.Reftype as Type  FROM tblAuditTrail  " & _
        " LEFT JOIN tblUser  ON     tblAuditTrail.UserID = tblUser.UserID " & _
        " LEFT JOIN tblModule ON   tblAuditTrail.ModID = tblModule.ModuleID  " & _
        " WHERE  LogTimestamp BETWEEN '" & dtpFrom.Value & "' AND '" & dtpTo.Value & "' " & _
        IIf(user = "", "", " AND    tblUser.UserID = '" & user & "' ")
        SQL.ReadQuery(query)
        cmbType.Items.Clear()
        While SQL.SQLDR.Read
            cmbType.Items.Add(SQL.SQLDR("Type").ToString)
        End While
    End Sub

    Private Sub LoadUser()
        Dim query As String
        query = "SELECT Username as Name FROM tblUser "
        SQL.ReadQuery(query)
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("All")
        While SQL.SQLDR.Read
            ComboBox1.Items.Add(SQL.SQLDR("Name").ToString)
        End While
        ComboBox1.SelectedItem = "All"
    End Sub

    Private Sub LoadActivity()
        Dim user As String = GetUserID()
        Dim query As String
        'query = " SELECT Datetime_Stamp, username AS Name, Module_Name, Activity_Type, Activity_Description  " & _
        '        " FROM tblUser_Activity LEFT JOIN tblUser " & _
        '        " ON     tblUser_Activity.UserID = tblUser.UserID " & _
        '        " LEFT JOIN tblModule " & _
        '        " ON     tblUser_Activity.ModuleID = tblModule.Module_ID " & _
        '        " WHERE  Datetime_Stamp BETWEEN '" & dtpFrom.Value & "' AND '" & dtpTo.Value & "' " & _
        '        IIf(user = "", "", " AND    tblUser.UserID = '" & user & "' ") & _
        '        " ORDER BY Datetime_Stamp "
        query = "SELECT LogTimestamp, username AS Name, ModuleName, LogType, tblAuditTrail.Reftype as Type,tblAuditTrail.RefID as RefID  FROM tblAuditTrail  " & _
        " LEFT JOIN tblUser  ON     tblAuditTrail.UserID = tblUser.UserID " & _
        " LEFT JOIN tblModule ON   tblAuditTrail.ModID = tblModule.ModuleID  " & _
        " WHERE  LogTimestamp BETWEEN '" & dtpFrom.Value & "' AND '" & dtpTo.Value & "' " & _
        IIf(user = "", "", " AND    tblUser.UserID = '" & user & "' ") & _
        IIf(cmbType.SelectedIndex = -1, "", " AND    tblAuditTrail.Reftype = '" & cmbType.SelectedItem & "' ") & _
         IIf(txtRefID.Text = "", "", " AND    tblAuditTrail.RefID like '%" & txtRefID.Text & "%' ") & _
        " ORDER BY LogTimestamp "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvUserActivity.DataSource = SQL.SQLDS.Tables(0)
        Else
            dgvUserActivity.DataSource = Nothing
        End If
    End Sub

    Private Function GetUserID() As String
        Dim query As String
        query = " SELECT UserID FROM tblUser WHERE UserName ='" & ComboBox1.SelectedItem & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("UserID").ToString
        Else
            Return ""
        End If
    End Function

    Private Sub dtpFrom_ValueChanged(sender As System.Object, e As System.EventArgs)
        LoadRefType()
        LoadActivity()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        LoadRefType()
        LoadActivity()
    End Sub

    Private Sub dtpFrom_ValueChanged_1(sender As Object, e As EventArgs) Handles dtpFrom.ValueChanged
        LoadRefType()
        LoadActivity()
    End Sub

    Private Sub dtpTo_ValueChanged(sender As Object, e As EventArgs) Handles dtpTo.ValueChanged
        LoadActivity()
    End Sub

    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton1.Click

    End Sub

    Private Sub GroupBox1_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub cmbType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbType.SelectedIndexChanged
        LoadActivity()
    End Sub

    Private Sub txtRefID_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtRefID.TextChanged
        LoadActivity()
    End Sub
End Class