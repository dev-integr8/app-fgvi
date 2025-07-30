Public Class frmOption
    Dim ModuleID As String = "UL"

    Private Sub frmOptionBusiness_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadBusinessType()
        LoadBranches()
        LoadDefaultType()
    End Sub

    Private Sub LoadBusinessType()
        Dim query As String
        query = " SELECT BusinessCode + ' - ' + Description AS BusinessCode FROM tblBusinessType WHERE Status ='Active' "
        SQL.ReadQuery(query)
        cbBusinessType.Items.Clear()
        While SQL.SQLDR.Read
            cbBusinessType.Items.Add(SQL.SQLDR("BusinessCode").ToString)
        End While
    End Sub

    Private Sub LoadBranches()
        Dim query As String
        query = "  SELECT tblBranch.BranchCode + ' - ' + Description AS BranchCode  " & _
                "  FROM      tblBranch  " & _
                "  LEFT JOIN tblUser_Access  ON         " & _
                "  tblBranch.BranchCode = tblUser_Access.Code   " & _
                "  AND       tblUser_Access.Type = 'BranchCode'  " & _
                "  AND  tblUser_Access.Status ='Active'  AND        " & _
                "  tblUser_Access.UserID = '" & UserID & "'  " & _
                "  WHERE     tblBranch.Status ='Active'   AND isAllowed = 1  "
        SQL.ReadQuery(query)
        cbBranch.Items.Clear()
        While SQL.SQLDR.Read
            cbBranch.Items.Add(SQL.SQLDR("BranchCode").ToString)
        End While
    End Sub

    Private Sub LoadDefaultType()
        Dim query As String
        query = " SELECT    TOP 1 tblAuditTrail.BusinessCode + ' - ' + tblBusinessType.Description AS BusinessCode, " & _
                "           tblAuditTrail.BranchCode+ ' - ' + tblBranch.Description AS BranchCode " & _
                " FROM      tblAuditTrail LEFT JOIN tblBusinessType " & _
                " ON		tblAuditTrail.BusinessCode = tblBusinessType.BusinessCode " & _
                " LEFT JOIN tblBranch " & _
                " ON		tblAuditTrail.BranchCode = tblBranch.BranchCode " & _
                " WHERE     UserID = '" & UserID & "' AND ModID ='UL' AND Logstatus =1 " & _
                " ORDER BY  LogTimestamp DESC "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            cbBusinessType.SelectedItem = SQL.SQLDR("BusinessCode").ToString
            cbBranch.SelectedItem = SQL.SQLDR("BranchCode").ToString
        End If
    End Sub

    Private Sub btnOption_Click(sender As System.Object, e As System.EventArgs) Handles btnOK.Click
        Try
            activityStatus = True
            If cbBusinessType.SelectedIndex = -1 Then
                Msg("Select business type!", MsgBoxStyle.Exclamation)
            ElseIf cbBranch.SelectedIndex = -1 Then
                Msg("Select your branch!", MsgBoxStyle.Exclamation)
            Else
                BusinessType = Strings.Left(cbBusinessType.SelectedItem, cbBusinessType.SelectedItem.ToString.IndexOf(" - "))
                BranchCode = Strings.Left(cbBranch.SelectedItem, cbBranch.SelectedItem.ToString.IndexOf(" - "))
                LoadSetup()
                Main_JADE.Show()
                Me.Close()
            End If
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "Login", "UserID", UserName, BusinessType, BranchCode, "", activityStatus)
        End Try
    End Sub

    Private Sub LoadSetup()
        Dim query As String
        query = " SELECT    Inv_Computation" & _
                " FROM      tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Inv_ComputationMethod = SQL.SQLDR("Inv_Computation")
        End If
    End Sub

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        frmUserLogin.Show()
        Me.Close()
    End Sub

    Private Sub frmOptionBusiness_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SQL.CloseCon()
    End Sub

  
    Private Sub GroupBox1_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class