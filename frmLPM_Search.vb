Public Class frmLPM_Search
    Public PropCode, PropName, Type As String
    Public ModID As String = "LC"
    Dim disableEvent As Boolean = False

    Private Sub frmProperty_Search_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If cbFilter.SelectedIndex = -1 Then
            cbFilter.SelectedIndex = 0
        End If
        LoadStatus()
        LoadList()
    End Sub

    Private Sub LoadStatus()
        Dim query As String
        If ModID = "LC" Then
            query = " SELECT DISTINCT Status FROM tblLeaseProperty  "
        ElseIf ModID = "RE" Then
            query = " SELECT DISTINCT Status FROM viewRE_UnitStatus  "
        End If
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


    Private Sub LoadList()
        If cbStatus.SelectedIndex <> -1 Then
            Dim query As String

            If ModID = "LC" Then
                query = " SELECT PropCode, Description, PropType, Status " &
                                " FROM tblLeaseProperty  " &
                                "WHERE " & cbFilter.SelectedItem & " LIKE '%" & txtFilter.Text & "%'  " &
                    IIf(cbStatus.SelectedItem = "All", "", " AND Status ='" & cbStatus.SelectedItem & "' ")
            ElseIf ModID = "RE" Then
                query = " SELECT  PropCode, Description, PropType " &
                        " FROM " &
                        " ( " &
                        "       SELECT tblSaleProperty.UnitCode AS PropCode, " &
                        "               CASE WHEN UnitType ='House and Lot' THEN Model + ' Blk ' + Unit_Blk  + ' Lot ' + Unit_Lot + ' ' + Project " &
                        "                    WHEN UnitType ='Condominium' THEN Unit_Bldg + ' Unit ' + Unit_No " &
                        "               END AS Description, UnitType AS PropType, viewRE_UnitStatus.Status " &
                        "       FROM   tblSaleProperty LEFT JOIN viewRE_UnitStatus  " &
                        "       ON     tblSaleProperty.UnitCode = viewRE_UnitStatus.UnitCode  " &
                        "       WHERE  viewRE_UnitStatus.Status ='Open' " &
                        " ) AS A " &
                        " WHERE " & cbFilter.SelectedItem & " Like '%" & txtFilter.Text & "%'  " &
                    IIf(cbStatus.SelectedItem = "All", "", " AND Status ='" & cbStatus.SelectedItem & "' ")
            End If
            SQL.ReadQuery(query)
            lvList.Items.Clear()
            While SQL.SQLDR.Read
                lvList.Items.Add(SQL.SQLDR("PropCode").ToString)
                lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Description").ToString)
                lvList.Items(lvList.Items.Count - 1).SubItems.Add(SQL.SQLDR("PropType").ToString)
            End While


            If lvList.Items.Count > 0 Then
                lvList.Items(0).Selected = True
            End If
            lvList.Columns(0).Width = 160
        End If
    End Sub

    Private Sub lvList_DoubleClick(sender As System.Object, e As System.EventArgs) Handles lvList.DoubleClick
        If lvList.SelectedItems.Count = 1 Then
            PropCode = lvList.SelectedItems(0).SubItems(chPropCode.Index).Text
            PropName = lvList.SelectedItems(0).SubItems(chDesc.Index).Text
            Type = lvList.SelectedItems(0).SubItems(chType.Index).Text
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
                PropCode = lvList.SelectedItems(0).SubItems(chPropCode.Index).Text
                PropName = lvList.SelectedItems(0).SubItems(chDesc.Index).Text
                Type = lvList.SelectedItems(0).SubItems(chType.Index).Text
                Me.Close()
            End If
        End If
    End Sub

End Class
