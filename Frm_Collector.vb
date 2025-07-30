Public Class Frm_Collector
    Dim Coll_ID As String


    Private Sub Frm_Collector_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tsbSearch.Enabled = True
        tsbNew.Enabled = True
        tsbEdit.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
        EnableControl(False)
    End Sub
    Private Sub EnableControl(ByVal Value As Boolean)
        ' COLLECTOR INFO
        txtCollectorID.Enabled = Value
        txtCollectorName.Enabled = Value
        cbStatus.Enabled = Value
    End Sub

    Private Sub Cleartext(ByVal Value As Boolean)
        'txtCollectorID.Text = ""
        txtCollectorName.Text = ""
        cbStatus.Text = ""
    End Sub


    Private Function GenerateCollectorID()
        Dim query As String
        query = "SELECT RIGHT('000' + ISNULL(CAST(CAST(MAX(Collector_ID) AS int)+1 AS nvarchar(50)),1), 6) AS Collector_ID FROM tblCollector_Master"
        SQL.ReadQuery(query)
        SQL.SQLDR.Read()
        Return SQL.SQLDR("Collector_ID").ToString
    End Function

    Private Sub tsbNew_Click(sender As Object, e As EventArgs) Handles tsbNew.Click

        txtCollectorID.Clear()
        Coll_ID = ""
        txtCollectorName.Clear()
        cbStatus.Text = ""
        ' Toolstrip Buttons
        tsbSearch.Enabled = False
        tsbNew.Enabled = False
        tsbEdit.Enabled = False
        tsbSave.Enabled = True
        tsbClose.Enabled = True
        tsbExit.Enabled = False
        EnableControl(True)

        txtCollectorID.Text = GenerateCollectorID()
    End Sub

    Private Sub tsbClose_Click(sender As Object, e As EventArgs) Handles tsbClose.Click
        ' Toolstrip Buttons
        If Coll_ID = "" Then
            tsbNew.PerformClick()
            EnableControl(False)
            tsbEdit.Enabled = False
        Else
            LoadCollector(Coll_ID)
            tsbEdit.Enabled = True
        End If
        tsbSearch.Enabled = True
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
        LoadCollector(Coll_ID)
    End Sub

    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        If validateDGV() Then
            'If txtCollectorName.Text = "" Then
            '    Msg("Please enter Collector Name!", MsgBoxStyle.Exclamation)
            If Coll_ID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    SaveCollector()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    Coll_ID = txtCollectorID.Text
                    LoadCollector(Coll_ID)
                End If
            Else
                Dim Validated As Boolean = True
                If Coll_ID <> txtCollectorID.Text Then
                    If RecordExist(txtCollectorID.Text) Then
                        Validated = False
                    Else
                        Validated = True
                    End If
                End If

                If Validated Then
                    If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                        UpdateCollector()
                        Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                        Coll_ID = txtCollectorID.Text
                        LoadCollector(Coll_ID)

                    End If
                Else
                    Msg("New VCECode is already in used! Please change VCECode", MsgBoxStyle.Exclamation)
                    txtCollectorID.Text = Coll_ID
                    txtCollectorID.SelectAll()
                End If
            End If
        End If
    End Sub


    Private Function validateDGV() As Boolean
        Dim value As Boolean = True
        If cbStatus.SelectedItem = "" Then
            MsgBox("Please select Status!", MsgBoxStyle.Exclamation)
            Return False
        ElseIf txtCollectorName.Text = "" Then
            MsgBox("Please enter Collector Name!", MsgBoxStyle.Exclamation)
            Return False
        End If
        Return value
    End Function


    Private Function RecordExist(ByVal code As String) As Boolean
        Dim query As String
        query = " SELECT * from tblCollector_Master where Collector_ID = '" & code & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False

        End If
    End Function

    Private Sub UpdateCollector()
        activityStatus = True
        Dim updateSQL As String

        updateSQL = " UPDATE tblCollector_Master" & _
                    " SET    Collector_ID = @Collector_ID, Collector_Name = @Collector_Name, Status = @Status " & _
                    " WHERE  Collector_ID = @Collector_ID "
        SQL.FlushParams()
        SQL.AddParam("@Collector_ID", txtCollectorID.Text)
        SQL.AddParam("@Collector_Name", txtCollectorName.Text)
        SQL.AddParam("@Status", cbStatus.Text)
        SQL.ExecNonQuery(updateSQL)
    End Sub

    Private Sub SaveCollector()
        'activityStatus = True
        Dim insertSQL As String
        insertSQL = " INSERT INTO " & _
                    " tblCollector_Master   (Collector_ID, Collector_Name, Status)  " & _
                    " VALUES                (@Collector_ID, @Collector_Name, @Status) "

        SQL.FlushParams()
        SQL.AddParam("@Collector_ID", txtCollectorID.Text)
        SQL.AddParam("@Collector_Name", txtCollectorName.Text)
        SQL.AddParam("@Status", cbStatus.Text)
        SQL.ExecNonQuery(insertSQL)
    End Sub

    Private Sub tsbSearch_Click(sender As Object, e As EventArgs) Handles tsbSearch.Click
        Dim f As New FrmCollector_Search
        f.ShowDialog()
        If f.CollectorID <> "" Then
            Coll_ID = f.CollectorID
        End If
        LoadCollector(Coll_ID)
        f.Dispose()
    End Sub

    Private Sub LoadCollector(ByVal Code As String)
        Dim query As String
        query = "  SELECT    Collector_ID, Collector_Name, Status  " & _
                "  FROM     tblCollector_Master  " & _
                 " WHERE    Collector_ID = '" & Code & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtCollectorID.Text = SQL.SQLDR("Collector_ID").ToString
            txtCollectorName.Text = SQL.SQLDR("Collector_Name").ToString
            cbStatus.Text = SQL.SQLDR("Status").ToString
        End If

    End Sub

    Private Sub tsbEdit_Click(sender As Object, e As EventArgs) Handles tsbEdit.Click
        EnableControl(True)

        ' Toolstrip Buttons
        tsbSearch.Enabled = False
        tsbNew.Enabled = False
        tsbEdit.Enabled = False
        tsbSave.Enabled = True
        tsbClose.Enabled = True
        tsbExit.Enabled = False
    End Sub

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub
End Class