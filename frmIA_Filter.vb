Public Class frmIA_Filter
    Dim groupWHSE As String = ""
    Dim groupPWHSE As String = ""
    Dim type As String = ""
    Dim category As String = ""
    Dim disableEvent As Boolean = False



    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As System.Object, e As System.EventArgs) Handles btnOK.Click
        Dim ItemOwner As String
        Dim ItemCategory As String
        If cbItemOwner.SelectedItem = "ALL" Then
            ItemOwner = ""
        Else
            ItemOwner = cbItemOwner.SelectedItem
        End If
        If cbPWHSEfilter.SelectedItem = "ALL" Then
            ItemCategory = ""
        Else
            ItemCategory = cbItemOwner.SelectedItem
        End If

        frmIA.DateFrom = dtpFrom.Value.Date
        frmIA.DateTo = dtpTo.Value.Date
        frmIA.WHSE = GetWHSE()
        frmIA.Item = GetItem()
        frmIA.ItemOwner = ItemOwner
        Me.Close()
    End Sub

    Private Function GetItem() As String
        Dim strItem As String = ""
        For Each item As ListViewItem In lvItem.CheckedItems
            strItem += "'" & item.SubItems(0).Text & "', "
        Next
        If strItem.Length > 2 Then
            strItem = Strings.Left(strItem, strItem.Length - 2)
            Return strItem
        Else
            strItem = "''"
        End If
    End Function

    Private Function GetWHSE() As String
        Dim strWHSE As String = ""
        For Each item As ListViewItem In lvWHSE.CheckedItems
            strWHSE += "'" & item.SubItems(0).Text & "', "
        Next

        For Each item As ListViewItem In lvPWHSE.CheckedItems
            strWHSE += "'" & item.SubItems(0).Text & "', "
        Next
        If strWHSE.Length > 2 Then
            strWHSE = Strings.Left(strWHSE, strWHSE.Length - 2)
            Return strWHSE
        Else
            strWHSE = "''"
        End If

    End Function

    Private Sub cbPeriod_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbPeriod.SelectedIndexChanged
        If cbPeriod.SelectedItem = "Monthly" Then
            gbPeriodYM.Visible = True
            gbPeriodFT.Visible = False
            chkYTD.Visible = True
            cbMonth.Enabled = True
            cbMonth.Items.Clear()
            cbMonth.Items.Add("January")
            cbMonth.Items.Add("February")
            cbMonth.Items.Add("March")
            cbMonth.Items.Add("April")
            cbMonth.Items.Add("May")
            cbMonth.Items.Add("June")
            cbMonth.Items.Add("July")
            cbMonth.Items.Add("August")
            cbMonth.Items.Add("September")
            cbMonth.Items.Add("October")
            cbMonth.Items.Add("November")
            cbMonth.Items.Add("December")
            lblMonth.Text = "Month :"
        ElseIf cbPeriod.SelectedItem = "Daily" Then
            gbPeriodYM.Visible = False
            gbPeriodFT.Visible = True
            lblTo.Visible = False
            dtpTo.Visible = False
            lblFrom.Text = "Date :"
        ElseIf cbPeriod.SelectedItem = "Date Range" Then
            lblFrom.Text = "From :"
            lblTo.Visible = True
            dtpTo.Visible = True
            gbPeriodYM.Visible = False
            gbPeriodFT.Visible = True '
        ElseIf cbPeriod.SelectedItem = "Yearly" Then
            chkYTD.Visible = False
            LoadPeriod()
            lblFrom.Text = "From :"
            lblTo.Visible = True
            dtpTo.Visible = True
            gbPeriodYM.Visible = True
            gbPeriodFT.Visible = False
            cbMonth.Enabled = False
        ElseIf cbPeriod.SelectedItem = "Quarterly" Then
            chkYTD.Visible = True
            LoadPeriod()
            cbMonth.Items.Clear()
            cbMonth.Items.Add("1st Quarter")
            cbMonth.Items.Add("2nd Quarter")
            cbMonth.Items.Add("3rd Quarter")
            cbMonth.Items.Add("4th Quarter")
            lblMonth.Text = "Quarter :"
            lblFrom.Text = "From :"
            lblTo.Visible = True
            dtpTo.Visible = True
            gbPeriodYM.Visible = True
            gbPeriodFT.Visible = False
            cbMonth.Enabled = True
        End If
    End Sub

    Private Sub LoadPeriod()
        If cbPeriod.SelectedItem = "Yearly" Then
            Dim period As String = "1-1-" & nupYear.Value.ToString
            dtpFrom.Value = Date.Parse(period)
            dtpTo.Value = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Year, 1, Date.Parse(period)))
        Else
            If cbMonth.SelectedIndex <> -1 Then
                Dim period As String = (cbMonth.SelectedIndex + 1).ToString & "-1-" & nupYear.Value.ToString
                If chkYTD.Checked Then
                    dtpFrom.Value = Date.Parse("1-1-" & nupYear.Value.ToString)
                    dtpTo.Value = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, Date.Parse(period)))
                Else
                    dtpFrom.Value = Date.Parse(period)
                    dtpTo.Value = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, Date.Parse(period)))
                End If

            End If
        End If
        
    End Sub

    Private Sub cbMonth_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbMonth.SelectedIndexChanged
        LoadPeriod()
    End Sub


    Private Sub nupYear_ValueChanged(sender As System.Object, e As System.EventArgs) Handles nupYear.ValueChanged
        If disableEvent = False Then
            LoadPeriod()
        End If

    End Sub

    Private Sub frmIA_Filter_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If lvWHSE.SelectedItems.Count = 1 Then
            lvWHSE.SelectedItems(0).Selected = False
        End If
        If lvPWHSE.SelectedItems.Count = 1 Then
            lvPWHSE.SelectedItems(0).Selected = False
        End If
        If lvItem.SelectedItems.Count = 1 Then
            lvItem.SelectedItems(0).Selected = False
        End If
    End Sub

    Private Sub frmIA_Filter_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        disableEvent = True
        nupYear.Value = Date.Today.Year
        disableEvent = False
       nupYear.Value = frmIA.DateFrom.Year
        tcFilter.TabPages.Remove(TabPage3)
        If cbPeriod.SelectedIndex = -1 Then cbPeriod.SelectedIndex = 0
        loadWHSEcategory()
        loadPWHSEcategory()
        disableEvent = True
        LoadItemType()
        LoadItemOwner()
        LoadItemCategory()
        If lvItem.Items.Count = 0 Then
            LoadItem()
        End If
        disableEvent = False
    End Sub

    Private Sub LoadItemType()
        Dim query As String
        If cbItemType.SelectedIndex = -1 Then type = "" Else type = cbItemType.SelectedItem
        query = " SELECT	DISTINCT ItemType " & _
                    " FROM	    tblItem_Master " & _
                    " WHERE     Status ='Active' AND ItemType IS NOT NULL  "
        SQL.ReadQuery(query)
        cbItemType.Items.Clear()
        cbItemType.Items.Add("ALL")
        While SQL.SQLDR.Read
            cbItemType.Items.Add(SQL.SQLDR("ItemType").ToString)
        End While
        cbItemType.SelectedItem = "ALL"
        If cbItemType.Items.Contains(type) Then cbItemType.SelectedItem = type Else cbItemType.SelectedItem = "ALL"
    End Sub

    Private Sub LoadItemCategory()
        If cbItemCategory.SelectedIndex = -1 Then category = "" Else category = cbItemCategory.SelectedItem
        Dim query As String
        query = " SELECT	DISTINCT ItemCategory " & _
                " FROM	    tblItem_Master " & _
                " WHERE     Status ='Active' AND ItemCategory IS NOT NULL "
        SQL.ReadQuery(query)
        cbItemCategory.Items.Clear()
        cbItemCategory.Items.Add("ALL")
        While SQL.SQLDR.Read
            cbItemCategory.Items.Add(SQL.SQLDR("ItemCategory").ToString)
        End While
        If cbItemCategory.Items.Contains(category) Then cbItemCategory.SelectedItem = category Else cbItemCategory.SelectedItem = "ALL"
    End Sub

    Private Sub LoadItemOwner()
        Dim owner As String = ""
        If cbItemOwner.SelectedIndex = -1 Then Owner = "" Else Owner = cbItemOwner.SelectedItem
        Dim query As String
        query = " SELECT	DISTINCT ItemOwner " & _
                " FROM	    tblItem_Master " & _
                " WHERE     Status ='Active' AND ItemOwner IS NOT NULL "
        SQL.ReadQuery(query)
        cbItemOwner.Items.Clear()
        cbItemOwner.Items.Add("ALL")
        While SQL.SQLDR.Read
            cbItemOwner.Items.Add(SQL.SQLDR("ItemOwner").ToString)
        End While
        If cbItemOwner.Items.Contains(Owner) Then cbItemOwner.SelectedItem = Owner Else cbItemOwner.SelectedItem = "ALL"
    End Sub

    Private Sub loadWHSEcategory()
        Dim GroupCategory As String = ""
        If cbWHSECategory.SelectedIndex <> -1 Then GroupCategory = cbWHSECategory.SelectedItem
        Dim query As String
        query = " SELECT Description FROM tblGroup WHERE Type ='Warehouse' AND Status ='Active' ORDER BY GroupID "
        SQL.ReadQuery(query)
        cbWHSECategory.Items.Clear()
        While SQL.SQLDR.Read()
            cbWHSECategory.Items.Add(SQL.SQLDR("Description").ToString)
        End While

        If cbWHSECategory.Items.Contains(GroupCategory) Then
            disableEvent = True
            cbWHSECategory.SelectedItem = GroupCategory
            disableEvent = False
        ElseIf cbWHSECategory.Items.Count > 0 Then
            disableEvent = True
            cbWHSECategory.SelectedIndex = 0
            loadWHSEfilter()
            disableEvent = False
            rbAll.Checked = True
        End If

    End Sub

    Private Sub loadPWHSEcategory()
        Dim GroupCategory As String = ""
        If cbPWHSECategory.SelectedIndex <> -1 Then GroupCategory = cbPWHSECategory.SelectedItem
        Dim query As String
        query = " SELECT Description FROM tblGroup WHERE Type ='Production Warehouse' AND Status ='Active' ORDER BY GroupID "
        SQL.ReadQuery(query)
        cbPWHSECategory.Items.Clear()
        While SQL.SQLDR.Read()
            cbPWHSECategory.Items.Add(SQL.SQLDR("Description").ToString)
        End While
        If cbPWHSECategory.Items.Contains(GroupCategory) Then
            disableEvent = True
            cbPWHSECategory.SelectedItem = GroupCategory
            disableEvent = False
        ElseIf cbPWHSECategory.Items.Count > 0 Then
            disableEvent = True
            cbPWHSECategory.SelectedIndex = 0
            loadPWHSEfilter()
            disableEvent = False
            rbPWHSEall.Checked = True
        End If
    End Sub

    Private Sub cbWHSECategory_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbWHSECategory.SelectedIndexChanged
        If disableEvent = False Then
            loadWHSEfilter()
        End If
    End Sub

    Private Sub cbPWHSECategory_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbPWHSECategory.SelectedIndexChanged
        If disableEvent = False Then
            loadPWHSEfilter()
        End If
    End Sub

    Private Sub cbWHSEFilter_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbWHSEFilter.SelectedIndexChanged
        LoadWHSE()
    End Sub

    Private Sub loadWHSEfilter()
        If cbWHSECategory.SelectedIndex <> -1 Then
            Dim query As String
            query = " SELECT GroupID FROM tblGroup WHERE Description ='" & cbWHSECategory.SelectedItem & "' AND Type ='Warehouse' AND Status ='Active' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                groupWHSE = SQL.SQLDR("GroupID").ToString
                query = " SELECT DISTINCT " & groupWHSE & " AS Filter FROM tblWarehouse WHERE Status ='Active'   " & _
                   " AND  Code IN (SELECT    DISTINCT tblWarehouse.Code   " & _
                   "                              FROM      tblWarehouse INNER JOIN tblUser_Access  " & _
                   "                              ON        tblWarehouse.Code = tblUser_Access.Code  " & _
                   "                              AND       tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active'  " & _
                   "                              AND       tblUser_Access.Type = 'Warehouse' AND isAllowed = 1  " & _
                   "                              WHERE     UserID ='" & UserID & "')  "
                SQL.ReadQuery(query)
                cbWHSEFilter.Items.Clear()
                cbWHSEFilter.Items.Add("ALL")
                While SQL.SQLDR.Read
                    cbWHSEFilter.Items.Add(SQL.SQLDR("Filter").ToString)
                End While
                cbWHSEFilter.SelectedItem = "ALL"
            End If
        End If
    End Sub

    Private Sub loadPWHSEfilter()
        If cbPWHSECategory.SelectedIndex <> -1 Then
            Dim query As String
            query = " SELECT GroupID FROM tblGroup WHERE Description ='" & cbPWHSECategory.SelectedItem & "' AND Type ='Production Warehouse' AND Status ='Active' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                groupPWHSE = SQL.SQLDR("GroupID").ToString
                query = " SELECT DISTINCT " & groupPWHSE & " AS Filter FROM tblProdWarehouse WHERE Status ='Active'   " & _
                   " AND  Code IN (SELECT    DISTINCT tblProdWarehouse.Code   " & _
                   "                              FROM      tblProdWarehouse INNER JOIN tblUser_Access  " & _
                   "                              ON        tblProdWarehouse.Code = tblUser_Access.Code  " & _
                   "                              AND       tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active'  " & _
                   "                              AND       tblUser_Access.Type = 'Production' AND isAllowed = 1  " & _
                   "                              WHERE     UserID ='" & UserID & "')  "
                SQL.ReadQuery(query)
                cbPWHSEfilter.Items.Clear()
                cbPWHSEfilter.Items.Add("ALL")
                While SQL.SQLDR.Read
                    cbPWHSEfilter.Items.Add(SQL.SQLDR("Filter").ToString)
                End While
                cbPWHSEfilter.SelectedItem = "ALL"
            End If
        End If
    End Sub

    Private Sub LoadWHSE()
        If cbWHSEFilter.SelectedIndex <> -1 Then
            Dim query As String
            Dim filter As String
            If cbWHSEFilter.SelectedItem = "ALL" Then
                filter = "1=1"
            Else
                filter = groupWHSE & " ='" & cbWHSEFilter.SelectedItem & "'"
            End If
            query = " SELECT Code, Description FROM tblWarehouse " & _
                    " WHERE  " & filter & " AND  Status ='Active'   " & _
                    " AND  Code IN (SELECT    DISTINCT tblWarehouse.Code   " & _
                    "                              FROM      tblWarehouse INNER JOIN tblUser_Access  " & _
                    "                              ON        tblWarehouse.Code = tblUser_Access.Code  " & _
                    "                              AND       tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active'  " & _
                    "                              AND       tblUser_Access.Type = 'Warehouse' AND isAllowed = 1  " & _
                    "                              WHERE     UserID ='" & UserID & "')  "
            SQL.ReadQuery(query)
            lvWHSE.Items.Clear()
            While SQL.SQLDR.Read
                lvWHSE.Items.Add(SQL.SQLDR("Code").ToString)
                lvWHSE.Items(lvWHSE.Items.Count - 1).SubItems.Add(SQL.SQLDR("Description").ToString)
            End While
            rbAll.Checked = True
        End If
    End Sub

    Private Sub LoadPWHSE()
        If cbPWHSEfilter.SelectedIndex <> -1 Then
            Dim query As String
            Dim filter As String
            If cbPWHSEfilter.SelectedItem = "ALL" Then
                filter = "1=1"
            Else
                filter = groupPWHSE & " ='" & cbPWHSEfilter.SelectedItem & "'"
            End If
            query = " SELECT Code, Description FROM tblProdWarehouse " & _
                    " WHERE  " & filter & " AND  Status ='Active'   " & _
                    " AND  Code IN (SELECT    DISTINCT tblProdWarehouse.Code   " & _
                    "                              FROM      tblProdWarehouse INNER JOIN tblUser_Access  " & _
                    "                              ON        tblProdWarehouse.Code = tblUser_Access.Code  " & _
                    "                              AND       tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active'  " & _
                    "                              AND       tblUser_Access.Type = 'Production' AND isAllowed = 1  " & _
                    "                              WHERE     UserID ='" & UserID & "')  "
            SQL.ReadQuery(query)
            lvPWHSE.Items.Clear()
            While SQL.SQLDR.Read
                lvPWHSE.Items.Add(SQL.SQLDR("Code").ToString)
                lvPWHSE.Items(lvPWHSE.Items.Count - 1).SubItems.Add(SQL.SQLDR("Description").ToString)
            End While
            rbPWHSEall.Checked = True
        End If
    End Sub

    Private Sub rbAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbAll.CheckedChanged, rbNone.CheckedChanged, rbSpecific.CheckedChanged
        If rbAll.Checked = True Then
            disableEvent = True
            For Each item As ListViewItem In lvWHSE.Items
                item.Checked = True
            Next
            disableEvent = False
        ElseIf rbNone.Checked = True Then
            disableEvent = True
            For Each item As ListViewItem In lvWHSE.Items
                item.Checked = False
            Next
            disableEvent = False
        End If
    End Sub

    Private Sub lvWHSE_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lvWHSE.SelectedIndexChanged
        If lvWHSE.SelectedItems.Count = 1 Then
            If lvWHSE.SelectedItems(0).Checked = False Then
                lvWHSE.SelectedItems(0).Checked = True
            Else
                lvWHSE.SelectedItems(0).Checked = False
            End If
            rbSpecific.Checked = True
        End If
    End Sub

    Private Sub lvPWHSE_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lvPWHSE.SelectedIndexChanged
        If lvPWHSE.SelectedItems.Count = 1 Then
            If lvPWHSE.SelectedItems(0).Checked = False Then
                lvPWHSE.SelectedItems(0).Checked = True
            Else
                lvPWHSE.SelectedItems(0).Checked = False
            End If
            rbPWHSEspecific.Checked = True
        End If
    End Sub

    Private Sub lvWHSE_ItemChecked(sender As System.Object, e As System.Windows.Forms.ItemCheckedEventArgs) Handles lvWHSE.ItemChecked
        If disableEvent = False Then
            rbSpecific.Checked = True
        End If

    End Sub

  

    

   


    Private Sub cbPWHSEfilter_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbPWHSEfilter.SelectedIndexChanged
        LoadPWHSE()
    End Sub

    Private Sub rbPWHSEall_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbPWHSEall.CheckedChanged, rbPWHSEnone.CheckedChanged, rbPWHSEspecific.CheckedChanged
        If rbPWHSEall.Checked = True Then
            disableEvent = True
            For Each item As ListViewItem In lvPWHSE.Items
                item.Checked = True
            Next
            disableEvent = False
        ElseIf rbPWHSEnone.Checked = True Then
            disableEvent = True
            For Each item As ListViewItem In lvPWHSE.Items
                item.Checked = False
            Next
            disableEvent = False
        End If
    End Sub

    Private Sub lvPWHSE_ItemChecked(sender As System.Object, e As System.Windows.Forms.ItemCheckedEventArgs) Handles lvPWHSE.ItemChecked
        If disableEvent = False Then
            rbPWHSEspecific.Checked = True
        End If
    End Sub

    Private Sub cbItem_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbItemOwner.SelectedIndexChanged, cbItemCategory.SelectedIndexChanged, cbItemType.SelectedIndexChanged
        If disableEvent = False Then
            If cbItemCategory.SelectedIndex <> -1 AndAlso cbItemType.SelectedIndex <> -1 AndAlso cbItemOwner.SelectedIndex <> -1 Then
                LoadItem()
                rbItemAll.Checked = True
            End If
        End If
    End Sub

    Private Sub LoadItem()
        Dim query As String
        Dim filter As String = ""
        SQL.FlushParams()
        If cbItemCategory.SelectedItem <> "ALL" Then
            filter = " AND ItemCategory = @ItemCategory "
            SQL.AddParam("@itemCategory", cbItemCategory.SelectedItem)
        End If
        If cbItemType.SelectedItem <> "ALL" Then
            filter = filter & " AND ItemType = @ItemType "
            SQL.AddParam("@ItemType", cbItemType.SelectedItem)
        End If
        If cbItemOwner.SelectedItem <> "ALL" Then
            filter = filter & " AND ItemOwner  = @ItemOwner  "
            SQL.AddParam("@ItemOwner", cbItemOwner.SelectedItem)
        End If
        query = " SELECT	ItemCode, ItemDescription " & _
                " FROM	    tblItem_Master " & _
                " WHERE     Status ='Active' " & filter & _
                " ORDER BY  ItemCode "
        SQL.ReadQuery(query)
        lvItem.Items.Clear()
        While SQL.SQLDR.Read
            lvItem.Items.Add(SQL.SQLDR("ItemCode").ToString)
            lvItem.Items(lvItem.Items.Count - 1).SubItems.Add(SQL.SQLDR("ItemDescription").ToString)
        End While
    End Sub

    Private Sub rbItem_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbItemAll.CheckedChanged, rbItemNone.CheckedChanged, rbItemSpecific.CheckedChanged
        If rbItemAll.Checked = True Then
            disableEvent = True
            For Each item As ListViewItem In lvItem.Items
                item.Checked = True
            Next
            disableEvent = False
        ElseIf rbItemNone.Checked = True Then
            disableEvent = True
            For Each item As ListViewItem In lvItem.Items
                item.Checked = False
            Next
            disableEvent = False
        End If
    End Sub

    Private Sub lvItem_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lvItem.SelectedIndexChanged
        If lvItem.SelectedItems.Count = 1 Then
            If lvItem.SelectedItems(0).Checked = False Then
                lvItem.SelectedItems(0).Checked = True
            Else
                lvItem.SelectedItems(0).Checked = False
            End If
            rbItemSpecific.Checked = True
        End If
    End Sub

    Private Sub lvItem_ItemChecked(sender As System.Object, e As System.Windows.Forms.ItemCheckedEventArgs) Handles lvItem.ItemChecked
        If disableEvent = False Then
            rbItemSpecific.Checked = True
        End If
    End Sub

    Private Sub chkYTD_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkYTD.CheckedChanged
        LoadPeriod()
    End Sub
End Class