Public Class frmProperty_Search
    Public PropCode, PropName, Type As String
    Dim disableEvent As Boolean = False

    Private Sub frmProperty_Search_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If cbFilter.SelectedIndex = -1 Then
            cbFilter.SelectedIndex = 0
        End If
        LoadProjects()
        LoadUnitType()
        LoadModel()
        LoadList()
    End Sub

    Private Sub LoadProjects()
        Dim query As String
        cbProject.Items.Clear()
        query = " SELECT DISTINCT Project FROM tblSaleProperty " &
                " WHERE UnitCode IN (SELECT UnitCode FROM viewRE_UnitStatus WHERE Status ='Open') "
        cbProject.Items.Add("All")
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            cbProject.Items.Add(SQL.SQLDR("Project").ToString)
        End While
        If cbProject.Items.Count > 0 Then
            disableEvent = True
            cbProject.SelectedIndex = 0
            disableEvent = False
        End If
    End Sub

    Private Sub LoadUnitType()
        SQL.FlushParams()
        Dim filter As String = ""
        If cbProject.SelectedItem <> "All" Then
            filter = " AND Project = @Project "
            SQL.AddParam("@Project", cbProject.SelectedItem)
        End If
        Dim query As String
        cbUnitType.Items.Clear()
        query = " SELECT DISTINCT UnitType FROM tblSaleProperty " &
                " WHERE UnitCode IN (SELECT UnitCode FROM viewRE_UnitStatus WHERE Status ='Open') " & filter
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            cbUnitType.Items.Add(SQL.SQLDR("UnitType").ToString)
        End While
        If cbUnitType.Items.Count > 0 Then
            disableEvent = True
            cbUnitType.SelectedIndex = 0
            disableEvent = False
        End If
    End Sub
    Private Sub LoadModel()
        SQL.FlushParams()
        Dim filter As String = ""
        If cbUnitType.Items.Count > 0 AndAlso cbUnitType.SelectedItem <> "Lot Only" Then
            lblModel.Visible = True
            cbModel.Visible = True
            filter = " AND UnitType = @UnitType "
            SQL.AddParam("@UnitType", cbUnitType.SelectedItem)

            If cbProject.SelectedItem <> "All" Then
                filter = filter & " AND Project = @Project "
                SQL.AddParam("@Project", cbProject.SelectedItem)
            End If
            Dim query As String
            cbModel.Items.Clear()
            cbModel.Items.Add("All")

            query = " SELECT DISTINCT Model FROM tblSaleProperty " &
                    " WHERE UnitCode IN (SELECT UnitCode FROM viewRE_UnitStatus WHERE Status ='Open') " & filter
            SQL.ReadQuery(query)
            While SQL.SQLDR.Read
                cbModel.Items.Add(SQL.SQLDR("Model").ToString)
            End While
            If cbModel.Items.Count > 0 Then
                cbModel.SelectedIndex = 0
            End If
        Else
            lblModel.Visible = False
            cbModel.Visible = False
        End If

    End Sub


    Private Sub LoadList()
        SQL.FlushParams()
        Dim filter As String = ""
        If cbProject.SelectedItem <> "All" Then
            filter = " AND Project = @Project "
            SQL.AddParam("@Project", cbProject.SelectedItem)
        End If
        If cbUnitType.SelectedItem IsNot Nothing AndAlso cbUnitType.SelectedItem <> "All" Then
            filter &= " AND UnitType = @UnitType "
            SQL.AddParam("@UnitType", cbUnitType.SelectedItem)
        End If
        If cbModel.SelectedItem IsNot Nothing AndAlso cbModel.Visible = True AndAlso cbModel.SelectedItem <> "All" Then
            filter &= " AND Model = @Model "
            SQL.AddParam("@Model", cbModel.SelectedItem)
        End If
        Dim query As String
        query = " SELECT  PropCode, Project, Model, UnitType, Unit_Lot, Unit_Blk, Unit_Phase, FloorArea, LotArea, ContractPrice, Description, Status " &
                        " FROM " &
                        " ( " &
                        "       SELECT tblSaleProperty.UnitCode AS PropCode, " &
                        "               CASE WHEN UnitType ='House and Lot' THEN Model + ' Blk ' + Unit_Blk  + ' Lot ' + Unit_Lot + ' ' + Project " &
                        "                    WHEN UnitType ='Condominium' THEN Unit_Bldg + ' Unit ' + Unit_No " &
                        "               END AS Description, UnitType , viewRE_UnitStatus.Status, Project, Model,  " &
                        "              Unit_Lot, Unit_Blk, Unit_Phase, FloorArea, LotArea, ContractPrice " &
                        "       FROM   tblSaleProperty LEFT JOIN viewRE_UnitStatus  " &
                        "       ON     tblSaleProperty.UnitCode = viewRE_UnitStatus.UnitCode  " &
                        "       WHERE  viewRE_UnitStatus.Status ='Open' " &
                        " ) AS A " &
                        " WHERE " & cbFilter.SelectedItem & " Like '%" & txtFilter.Text & "%'  " & filter
        SQL.ReadQuery(query)
        lvList.Items.Clear()
        While SQL.SQLDR.Read
            lvList.Items.Add(SQL.SQLDR("PropCode").ToString)
            lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Project").ToString)
            lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Model").ToString)
            lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Unit_Blk").ToString)
            lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Unit_Lot").ToString)
            lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Unit_Phase").ToString)
            lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("FloorArea").ToString)
            lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("LotArea").ToString)
            lvList.Items(lvList.Items.Count - 1).SubItems.Add(CDec(SQL.SQLDR("ContractPrice")).ToString("N2"))
            lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Description").ToString)
            lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("UnitType").ToString)
            lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Status").ToString)
        End While


        If lvList.Items.Count > 0 Then
            lvList.Items(0).Selected = True
        End If
        lvList.Columns(0).Width = 160
    End Sub

    Private Sub lvList_DoubleClick(sender As System.Object, e As System.EventArgs) Handles lvList.DoubleClick
        If lvList.SelectedItems.Count = 1 Then
            PropCode = lvList.SelectedItems(0).SubItems(chPropCode.Index).Text
            PropName = lvList.SelectedItems(0).SubItems(chDesc.Index).Text
            Type = lvList.SelectedItems(0).SubItems(chType.Index).Text
            Me.Close()
        End If
    End Sub

    Private Sub cbStatus_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
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

    Private Sub cbProject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbProject.SelectedIndexChanged
        If disableEvent = False Then
            LoadUnitType()
            LoadList()
        End If
    End Sub

    Private Sub cbModel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbModel.SelectedIndexChanged
        If disableEvent = False Then
            LoadList()
        End If
    End Sub

    Private Sub cbUnitType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbUnitType.SelectedIndexChanged
        If disableEvent = False Then
            LoadModel()
            LoadList()
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
                PropCode = lvList.SelectedItems(0).SubItems(chPropCode.Index).Text
                PropName = lvList.SelectedItems(0).SubItems(chDesc.Index).Text
                Type = lvList.SelectedItems(0).SubItems(chType.Index).Text
                Me.Close()
            End If
        End If
    End Sub

End Class
