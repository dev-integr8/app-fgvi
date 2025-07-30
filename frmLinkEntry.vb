Public Class frmLinkEntry
    Public Ref As String = ""
    Private Sub frmLinkEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadType()
    End Sub

    Private Sub LoadType()
        lbType.Items.Clear()
        Dim query As String
        query = " SELECT DISTINCT  SUBSTRING(RefNo,1,CHARINDEX(':',RefNo)-1) AS Type " & _
                " FROM   view_GL " & _
                " WHERE  RefNo LIKE '%:%'  AND RefNo LIKE @Filter + '%' " & _
                " ORDER BY Type "
        SQL.FlushParams()
        SQL.AddParam("@Filter", txtFilterType.Text)
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            lbType.Items.Add(SQL.SQLDR("Type").ToString)
        End While
        If lbType.Items.Count > 0 Then
            lbType.SelectedIndex = 0
        End If
    End Sub

    Private Sub lbType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbType.SelectedIndexChanged
        If lbType.SelectedIndex <> -1 Then
            LoadTransID()
        End If
    End Sub

    Private Sub LoadTransID()
        lbNo.Items.Clear()
        Dim query As String
        query = " SELECT    DISTINCT  SUBSTRING(RefNo,CHARINDEX(':',RefNo)+1, LEN(RefNo)) AS ID " & _
                " FROM      view_GL " & _
                " WHERE     RefNo LIKE '" & lbType.SelectedItem & ":%' AND RefNo LIKE  '%' + @Filter + '%' " & _
                " ORDER BY ID "
        SQL.FlushParams()
        SQL.AddParam("@Filter", txtFilterNo.Text)
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            lbNo.Items.Add(SQL.SQLDR("ID").ToString)
        End While
        If lbNo.Items.Count > 0 Then
            lbNo.SelectedIndex = 0
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtFilterType.TextChanged
        LoadType()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        If lbType.SelectedIndex = -1 Then
            MsgBox("Please select ref. type!", MsgBoxStyle.Exclamation)
        ElseIf lbNo.SelectedIndex = -1 Then
            MsgBox("Please select ref. no.!", MsgBoxStyle.Exclamation)
        Else
            Ref = lbType.SelectedItem & ":" & lbNo.SelectedItem
            Me.Close()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub txtFilterNo_TextChanged(sender As Object, e As EventArgs) Handles txtFilterNo.TextChanged
        LoadTransID()
    End Sub
End Class