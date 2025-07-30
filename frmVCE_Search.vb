Public Class frmVCE_Search
    Public VCECode, VCEName, TIN, Type, CustomerCode As String
    Public rowInd As Integer
    Public ModType As String = "VCE"
    Public ModFrom As String = "ALL"
    Dim disableEvent As Boolean = False


    Private Sub frmSearchVendor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If cbFilter.SelectedIndex = -1 Then
            cbFilter.SelectedIndex = 0
        End If


        If ModFrom = "VCE" Then
            cbFilter.Items.Clear()
            cbFilter.Items.Add("VCECode")
            cbFilter.Items.Add("VCEName")
            cbFilter.Items.Add("Vendor")
            cbFilter.Items.Add("Customer")
            cbFilter.Items.Add("Employee")
            cbFilter.SelectedIndex = 0
        End If

        If Type = "RUBY Employee" Then
            ModFrom = "RUBY Employee"
        End If

        If Type = "Member" Then
            ModFrom = "Member"
        End If
        If ModFrom = "ALL" Or ModFrom = "VCE" Or ModFrom = "Customer" Or ModFrom = "CustomerFilter" Then
            LoadStatus()
        End If
        LoadList()
        If lvList.Items.Count = 0 Then
            Msg("No record found!", MsgBoxStyle.Exclamation)
            Me.Close()
        End If

    End Sub

    Private Sub LoadList()
        If ModType = "RFP" Then
            cbStatus.Items.Clear()
            cbStatus.Enabled = False
            LoadListRFP()
        ElseIf ModFrom = "RUBY Employee" Then
            cbStatus.Items.Clear()
            cbStatus.Enabled = False
            LoadRUBYEmployee()
        ElseIf ModFrom = "ALL" Then
            disableEvent = True
            disableEvent = False
            LoadALL()
            cbStatus.Enabled = True

            
           
        ElseIf ModFrom = "Customer" Then
            disableEvent = True
            disableEvent = False
            LoadCustomer()
            cbStatus.Enabled = True
        ElseIf ModFrom = "CustomerFilter" Then
            disableEvent = True
            disableEvent = False
            LoadCustomerFilter()
            cbStatus.Enabled = True
        ElseIf ModFrom = "VCE" Then
            disableEvent = True
            disableEvent = False

            LoadVCE()
            cbStatus.Enabled = True
        ElseIf ModFrom = "CF" Then
            cbStatus.Items.Clear()
            cbStatus.Enabled = False
            LoadCF()
        ElseIf ModFrom = "UAR" Then
            cbStatus.Items.Clear()
            cbStatus.Enabled = False
            LoadUser()
        ElseIf ModFrom = "Member" Then
            cbStatus.Items.Clear()
            cbStatus.Enabled = False
            LoadMember()
        End If
    End Sub

    Private Sub LoadStatus()
        Dim query As String
        query = " SELECT DISTINCT Status FROM viewVCE_Master WHERE Status <> 'Deleted'  ORDER BY Status ASC "
        SQL.ReadQuery(query)
        cbStatus.Items.Clear()
        cbStatus.Items.Add("All")
        While SQL.SQLDR.Read
            cbStatus.Items.Add(SQL.SQLDR("Status").ToString)
        End While
        If cbStatus.Items.Count = 2 Then
            cbStatus.Items.Remove("All")
        End If
        cbStatus.SelectedIndex = 0
    End Sub

    Private Sub LoadRUBYEmployee()
        cbFilter.Items.Clear()
        cbFilter.Items.Add("Emp_ID")
        cbFilter.Items.Add("Emp_Name")
        cbFilter.SelectedIndex = 1
        Dim query As String
        SQL.CloseCon()
        SetPayrollDatabase()
        query = " SELECT Emp_ID, Emp_Name FROM viewEmployee_Info WHERE " & cbFilter.SelectedItem & " LIKE '%" & txtFilter.Text & "%'  " & _
                " AND Employee_Status ='Active' "
        SQL_RUBY.ReadQuery(query)
        lvList.Items.Clear()
        While SQL_RUBY.SQLDR.Read
            lvList.Items.Add(SQL_RUBY.SQLDR("Emp_ID").ToString)
            lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL_RUBY.SQLDR("Emp_Name").ToString)
        End While
        If lvList.Items.Count > 0 Then
            lvList.Items(0).Selected = True
        End If
        lvList.Columns(0).Width = 160

        SQL_RUBY.CloseCon()
        SetDatabase()
    End Sub


    Private Sub LoadListRFP()
        Dim query As String
        Dim filtertype As String = ""
        If cbFilter.SelectedItem = "TIN" Then
            filtertype = "TIN_No"
        Else
            filtertype = "VCEName"
        End If
        query = " SELECT DISTINCT MAX(VCECode) AS VCECode, VCEName, MAX(TIN_No) AS TIN_No " & _
                " FROM " & _
                " ( " & _
                "       SELECT VCECode AS VCECode,  VCEName, TIN_No FROM viewVCE_Master WHERE " & filtertype & " LIKE '%' + @Filter + '%' " & _
                "       UNION ALL " & _
                "       SELECT '', RecordPayee, TIN FROM tblRFP_Details WHERE " & cbFilter.SelectedItem & " LIKE '%' + @Filter + '%'  " & _
                " ) AS Payee " & _
                " GROUP BY VCEName "
        SQL.FlushParams()
        SQL.AddParam("@Filter", txtFilter.Text)
        SQL.ReadQuery(query)
        lvList.Items.Clear()
        While SQL.SQLDR.Read
            lvList.Items.Add(SQL.SQLDR("VCECode").ToString)
            lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("VCEName").ToString)
            lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("TIN_No").ToString)
        End While
        SQL.FlushParams()
        For Each row As DataGridViewRow In frmRFP.dgvRecords.Rows
            If row.Index <> rowInd Then
                If Type = "RecordPayee" Then
                    If Not row.Cells(frmRFP.dgcPayee.Index).Value Is Nothing Then
                        If Strings.UCase(row.Cells(frmRFP.dgcPayee.Index).Value).ToString.Contains(Strings.UCase(txtFilter.Text)) Then
                            Dim alreadyExist As Boolean = False

                            For Each Item As ListViewItem In lvList.Items
                                If Item.SubItems(chVCEName.Index).Text = row.Cells(frmRFP.dgcPayee.Index).Value.ToString Then
                                    alreadyExist = True
                                End If
                            Next
                            If alreadyExist = False Then
                                lvList.Items.Add("")
                                lvList.Items(lvList.Items.Count - 1).SubItems.Add(row.Cells(frmRFP.dgcPayee.Index).Value.ToString)
                                If row.Cells(frmRFP.dgcTIN.Index).Value Is Nothing Then
                                    lvList.Items(lvList.Items.Count - 1).SubItems.Add("")
                                Else
                                    lvList.Items(lvList.Items.Count - 1).SubItems.Add(row.Cells(frmRFP.dgcTIN.Index).Value.ToString)
                                End If
                            End If
                        End If
                    End If

                ElseIf Type = "TIN" Then
                    If Not row.Cells(frmRFP.dgcTIN.Index).Value Is Nothing Then
                        If row.Cells(frmRFP.dgcTIN.Index).Value = txtFilter.Text Then
                            Dim alreadyExist As Boolean = False

                            For Each Item As ListViewItem In lvList.Items
                                If Item.SubItems(chTIN.Index).Text = row.Cells(frmRFP.dgcTIN.Index).Value.ToString Then
                                    alreadyExist = True
                                End If
                            Next
                            If alreadyExist = False Then
                                lvList.Items.Add("")
                                If row.Cells(frmRFP.dgcPayee.Index).Value Is Nothing Then
                                    lvList.Items(lvList.Items.Count - 1).SubItems.Add("")
                                Else
                                    lvList.Items(lvList.Items.Count - 1).SubItems.Add(row.Cells(frmRFP.dgcPayee.Index).Value.ToString)
                                End If
                                lvList.Items(lvList.Items.Count - 1).SubItems.Add(row.Cells(frmRFP.dgcTIN.Index).Value.ToString)
                            End If
                        End If
                    End If

                End If
            End If

        Next

        lvList.Columns(0).Width = 0
        If lvList.Items.Count > 0 Then
            lvList.Items(0).Selected = True
        Else
            Me.Close()

        End If
    End Sub

    Private Sub LoadCustomerFilter()
        If cbStatus.SelectedIndex <> -1 Then

            Dim query As String
            query = " SELECT VCECode, VCEName, TIN_No FROM viewVCE_Master WHERE " & cbFilter.SelectedItem & " LIKE '%' + @Filter + '%'   AND Ins_Company = '" & CustomerCode & "' " & _
              IIf(cbStatus.SelectedItem = "All", "", " AND Status ='" & cbStatus.SelectedItem & "'")

            'query = " SELECT VCECode, VCEName, TIN_No FROM viewVCE_Master WHERE " & cbFilter.SelectedItem & " LIKE '%" & txtFilter.Text & "%'  " & _
            ' IIf(cbStatus.SelectedItem = "All", "", " AND Status ='" & cbStatus.SelectedItem & "'")
            SQL.FlushParams()
            SQL.AddParam("@Filter", txtFilter.Text)
            SQL.ReadQuery(query)
            lvList.Items.Clear()
            While SQL.SQLDR.Read
                lvList.Items.Add(SQL.SQLDR("VCECode").ToString)
                lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("VCEName").ToString)
            End While
            If lvList.Items.Count > 0 Then
                lvList.Items(0).Selected = True
            End If
            lvList.Columns(0).Width = 160
        End If
    End Sub



    Private Sub LoadCustomer()
        If cbStatus.SelectedIndex <> -1 Then

            Dim query As String
            query = " SELECT VCECode, VCEName, TIN_No FROM viewVCE_Master WHERE " & cbFilter.SelectedItem & " LIKE '%' + @Filter + '%'   AND isCustomer = 1 " & _
              IIf(cbStatus.SelectedItem = "All", "", " AND Status ='" & cbStatus.SelectedItem & "'")
            SQL.FlushParams()
            SQL.AddParam("@Filter", txtFilter.Text)
            SQL.ReadQuery(query)
            lvList.Items.Clear()
            While SQL.SQLDR.Read
                lvList.Items.Add(SQL.SQLDR("VCECode").ToString)
                lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("VCEName").ToString)
            End While
            If lvList.Items.Count > 0 Then
                lvList.Items(0).Selected = True
            End If
            lvList.Columns(0).Width = 160
        End If
    End Sub

    Private Sub LoadVCE()
        Dim filter As String
        If cbStatus.SelectedIndex <> -1 Then
            If cbFilter.SelectedIndex = -1 Then
                filter = " WHERE '' = ''"
            Else
                Select Case cbFilter.SelectedItem
                    Case "VCECode"
                        filter = " WHERE VCECode LIKE '%' + @Filter + '%'  "
                    Case "VCEName"
                        filter = " WHERE VCEName  LIKE '%' + @Filter + '%' "
                    Case "Vendor"
                        filter = " WHERE isVendor = 1 AND VCEName  LIKE '%' + @Filter + '%' "
                    Case "Customer"
                        filter = " WHERE isCustomer = 1  AND VCEName  LIKE '%' + @Filter + '%' "
                    Case "Employee"
                        filter = " WHERE isEmployee = 1 AND VCEName LIKE '%' + @Filter + '%' "
                End Select
            End If

            Dim query As String

            If cbFilter.SelectedItem = "VCECode" Or cbFilter.SelectedItem = "VCEName" Then
                query = " SELECT VCECode, VCEName , Status, isVendor, isEmployee, isMember, isCustomer, isOther " & _
                        " FROM " & _
                        " ( " & _
                        "   SELECT VCECode, CASE WHEN VCEName = '' THEN CONCAT(Last_Name +', ', First_Name + ' ', Middle_Name) ELSE VCEName END AS VCEName, Status, " & _
                        "   ISNULL(isVendor,0) AS isVendor,  ISNULL(isEmployee,0) AS isEmployee,  ISNULL(isMember,0) AS isMember,  ISNULL(isCustomer,0) AS isCustomer, ISNULL(isOther,0) AS isOther" & _
                        "   FROM tblVCE_Master " & _
                        " ) AS VCE " & _
                        filter & _
                IIf(cbStatus.SelectedItem = "All", "", " AND Status ='" & cbStatus.SelectedItem & "' ") & _
                IIf(Type = "Vendor", " AND isVendor = 1 ", "") & _
                IIf(Type = "Employee", " AND isEmployee = 1 ", "") & _
                IIf(Type = "Member", " AND isMember = 1 ", "") & _
                IIf(Type = "Customer", " AND isCustomer = 1 ", "") & _
                IIf(Type = "Other", " AND isOther = 1 ", "")
                SQL.FlushParams()
                SQL.AddParam("@Filter", txtFilter.Text)
                SQL.ReadQuery(query)
            Else
                query = " SELECT VCECode, VCEName , Status, isVendor, isEmployee, isMember, isCustomer, isOther " & _
                       " FROM " & _
                       " ( " & _
                       "   SELECT VCECode, CASE WHEN VCEName = '' THEN CONCAT(Last_Name +', ', First_Name + ' ', Middle_Name) ELSE VCEName END AS VCEName, Status, " & _
                        "   ISNULL(isVendor,0) AS isVendor,  ISNULL(isEmployee,0) AS isEmployee,  ISNULL(isMember,0) AS isMember,  ISNULL(isCustomer,0) AS isCustomer, ISNULL(isOther,0) AS isOther" & _
                       "   FROM tblVCE_Master " & _
                       " ) AS VCE " & _
                       filter & _
               IIf(cbStatus.SelectedItem = "All", "", " AND Status ='" & cbStatus.SelectedItem & "' ")
                SQL.FlushParams()
                SQL.AddParam("@Filter", txtFilter.Text)
                SQL.ReadQuery(query)
            End If
            lvList.Items.Clear()
            While SQL.SQLDR.Read
                lvList.Items.Add(SQL.SQLDR("VCECode").ToString)
                lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("VCEName").ToString)
            End While
            If lvList.Items.Count > 0 Then
                lvList.Items(0).Selected = True
            End If
            lvList.Columns(0).Width = 160
        End If
    End Sub

    Private Sub LoadALL()
        If cbStatus.SelectedIndex <> -1 Then

            Dim query As String
            query = " SELECT VCECode, VCEName, TIN_No FROM viewVCE_Master WHERE " & cbFilter.SelectedItem & " LIKE '%' + @Filter + '%'  " & _
            IIf(cbStatus.SelectedItem = "All", "", " AND Status ='" & cbStatus.SelectedItem & "' ") & _
            IIf(Type = "Vendor", " AND isVendor = 1 ", "") & _
            IIf(Type = "Customer", " AND isCustomer = 1 ", "")
            SQL.FlushParams()
            SQL.AddParam("@Filter", txtFilter.Text)
            SQL.ReadQuery(query)
            lvList.Items.Clear()
            While SQL.SQLDR.Read
                lvList.Items.Add(SQL.SQLDR("VCECode").ToString)
                lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("VCEName").ToString)
                lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("TIN_No").ToString)
            End While
            If lvList.Items.Count > 0 Then
                lvList.Items(0).Selected = True
            End If
            lvList.Columns(0).Width = 160

        End If
    End Sub

    Private Sub LoadCF()
        With frmCF
            Dim query, VCEcode As String
            query = " SELECT VCECode, VCEName, TIN_No FROM tblVCE_Master WHERE VCEName LIKE '%' + @Filter + '%' "
            SQL.FlushParams()
            SQL.AddParam("@Filter", txtFilter.Text)
            SQL.ReadQuery(query)
            lvList.Items.Clear()
            While SQL.SQLDR.Read
                VCEcode = SQL.SQLDR("VCECode").ToString
                If VCEcode <> frmCF.supA And VCEcode <> .supB And VCEcode <> .supC And VCEcode <> .supD Then
                    lvList.Items.Add(SQL.SQLDR("VCECode").ToString)
                    lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("VCEName").ToString)
                    lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("TIN_No").ToString)
                End If
            End While
            SQL.FlushParams()
        End With

        lvList.Columns(0).Width = 0
        If lvList.Items.Count > 0 Then
            lvList.Items(0).Selected = True
        Else
            Me.Close()
        End If
    End Sub

    Private Sub LoadMember()
        With frmCF
            Dim query As String
            query = " SELECT  Member_ID, Full_Name, Member_Type FROM tblMember_Master WHERE Full_Name LIKE '%' + @Filter + '%' AND Status = 'Active' "
            SQL.FlushParams()
            SQL.AddParam("@Filter", txtFilter.Text)
            SQL.ReadQuery(query)
            lvList.Items.Clear()
            While SQL.SQLDR.Read
                lvList.Items.Add(SQL.SQLDR("Member_ID").ToString)
                lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Full_Name").ToString)
                lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Member_Type").ToString)
            End While
            SQL.FlushParams()
        End With
        lvList.Columns(chTIN.Index).Text = "Member Type"
        lvList.Columns(0).Width = 160
        If lvList.Items.Count > 0 Then
            lvList.Items(0).Selected = True
        Else
            Me.Close()
        End If
    End Sub

    Private Sub LoadUser()
        With frmCF
            Dim query, filter As String
            If UserID = 1 Then
                filter = ""
            Else
                filter = "AND UserID <> 1"
            End If
            query = " SELECT  UserID, UserName, Status FROM tblUser WHERE UserName LIKE '%' + @Filter + '%' AND Status = 'Active' " & filter
            SQL.FlushParams()
            SQL.AddParam("@Filter", txtFilter.Text)
            SQL.ReadQuery(query)
            lvList.Items.Clear()
            While SQL.SQLDR.Read
                lvList.Items.Add(SQL.SQLDR("UserID").ToString)
                lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("UserName").ToString)
            End While
            SQL.FlushParams()
            lvList.Columns(chTIN.Index).Width = 0
        End With

        lvList.Columns(0).Width = 0
        If lvList.Items.Count > 0 Then
            lvList.Items(0).Selected = True
        Else
            Me.Close()
        End If
    End Sub

    Private Sub lvList_DoubleClick(sender As System.Object, e As System.EventArgs) Handles lvList.DoubleClick
        If lvList.SelectedItems.Count = 1 Then
            VCECode = lvList.SelectedItems(0).SubItems(chVCECode.Index).Text
            VCEName = lvList.SelectedItems(0).SubItems(chVCEName.Index).Text
            If ModType <> "VCE" Then
                TIN = lvList.SelectedItems(0).SubItems(chTIN.Index).Text
            End If
            If ModFrom = "Member" Then
                TIN = lvList.SelectedItems(0).SubItems(chTIN.Index).Text
            End If
        Me.Close()
        End If
    End Sub

    Private Sub cbStatus_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbStatus.SelectedIndexChanged
        If disableEvent = False Then
            LoadList()
        End If
    End Sub

    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        LoadList()
    End Sub

    Private Sub frmVCE_Search_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub txtFilter_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtFilter.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadList()
        End If
    End Sub

    Private Sub lvList_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles lvList.KeyDown
        If e.KeyCode = Keys.Enter Then
            If lvList.SelectedItems.Count = 1 Then
                VCECode = lvList.SelectedItems(0).SubItems(chVCECode.Index).Text
                VCEName = lvList.SelectedItems(0).SubItems(chVCEName.Index).Text
                If ModType <> "VCE" Then
                    TIN = lvList.SelectedItems(0).SubItems(chTIN.Index).Text
                End If
                Me.Close()
            End If
        End If
    End Sub

    Private Sub lvList_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lvList.SelectedIndexChanged

    End Sub

    Private Sub cbFilter_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbFilter.SelectedIndexChanged

    End Sub
End Class
