Public Class frmRE_List
    Dim SQL As New SQLControl
    Public VCECode As String = Nothing
    Public CollectDate As Date
    Public batch As Boolean = False
    Dim TransID As Integer
    Dim disableEvent As Boolean = False
    Public isCancel As Boolean = False

    Public Overloads Function ShowDialog(ByVal docID As String) As Boolean
        TransID = docID
        MyBase.ShowDialog()
        Return True
    End Function
    Private Sub frmLM_List_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadList()
        lvARList.Focus()
    End Sub
    Private Sub LoadList()
        Dim query As String
        query = " SELECT ChargeID, Type, PeriodFrom, PeriodTo, Amount, Checked FROM viewLM_Charges WHERE TransID = @TransID "
        SQL.FlushParams()
        SQL.AddParam("@TransID", TransID)
        SQL.ReadQuery(query)
        lvARList.Items.Clear()
        If SQL.SQLDR.HasRows Then
            While SQL.SQLDR.Read
                lvARList.Items.Add(SQL.SQLDR("ChargeID").ToString)
                lvARList.Items(lvARList.Items.Count - 1).SubItems.Add(SQL.SQLDR("Type").ToString)
                lvARList.Items(lvARList.Items.Count - 1).SubItems.Add(String.Format("{0:n2}", SQL.SQLDR("Amount")))
                lvARList.Items(lvARList.Items.Count - 1).Checked = SQL.SQLDR("Checked")
            End While
            If lvARList.Items.Count = 1 Then
                lvARList.Items(0).Checked = True
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub lvARList_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles lvARList.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.Close()
        End If
    End Sub


    Private Sub frmReceivables_Enter(sender As System.Object, e As System.EventArgs) Handles MyBase.Enter, btnDone.Click
        Me.Close()
    End Sub

    Private Sub chkAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAll.CheckedChanged
        If disableEvent = False Then
            For Each row As ListViewItem In lvARList.Items
                row.Checked = chkAll.Checked
            Next
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        isCancel = True
    End Sub

    Private Sub lvARList_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles lvARList.ItemChecked
        e.Item.Selected = e.Item.Checked
    End Sub

    Private Sub lvARList_Click(sender As Object, e As EventArgs) Handles lvARList.Click
        If disableEvent = False Then
            disableEvent = True
            For Each row As ListViewItem In lvARList.CheckedItems
                row.Selected = True
            Next
            disableEvent = False
        End If
    End Sub


End Class