Imports System.Data.SqlClient

Public Class frmServerSetup

    Dim masterCon As SqlConnection
    Dim masterCmd As SqlCommand
    Dim masterReader As SqlDataReader
    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        DefaultServer = txtServer.Text
        DBUser = txtUser.Text
        DBPassword = txtPass.Text
        database = txtDB.Text
        If HasConnection() Then
            SaveServer()
            If isInitialized() Then
                CheckForUpdates()
                frmUserLogin.Show()
                Me.Hide()
            Else
                frmInit.Show()
            End If
        Else
            MsgBox("Can't connect to specified server!", MsgBoxStyle.Exclamation)
        End If
    End Sub
    Private Sub CheckForUpdates()
        Dim App_Path As String
        App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
        Dim CurrentVersion As String = My.Application.Info.Version.ToString
        Dim a As String = My.Application.Info.DirectoryPath
        Dim name As String = Environment.GetCommandLineArgs(0).ToString.Replace(a, "").Replace("\", "")
        Dim pHelp As New ProcessStartInfo
        If My.Computer.FileSystem.FileExists("Gr8Updater.exe") Then
            pHelp.FileName = "Gr8Updater.exe"
            pHelp.Arguments = CurrentVersion & " " & name & " /S "
            pHelp.UseShellExecute = True
            pHelp.WindowStyle = ProcessWindowStyle.Normal
            Dim proc As Process = Process.Start(pHelp)
        End If
    End Sub

    Private Function HasConnection() As Boolean
        Try
            Dim valid As Boolean = True
            If valid Then
                masterCon = New SqlConnection With {.ConnectionString = ("Server=" & DefaultServer & ";Database=" & database & ";integrated security=sspi;Uid=" & DBUser & ";Pwd=" & DBPassword & ";Trusted_Connection=no;MultipleActiveResultSets=True;Max Pool Size=200;")}
                If masterCon.State = ConnectionState.Open Then
                    masterCon.Close()
                Else
                End If
                masterCon.Open()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
            Msg(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Function

    Private Sub SaveServer()
        Dim deleteSQL, insertSQL As String
        deleteSQL = "  DELETE FROM Database_Info "
        AlphaExecute(deleteSQL)
        insertSQL = " INSERT INTO " & _
                    " Database_Info([Server], [Username], [Password], [DefaultDB]) " & _
                    " VALUES('" & txtServer.Text & "','" & txtUser.Text & "','" & txtPass.Text & "','" & txtDB.Text & "')"
        AlphaExecute(insertSQL)
        DBUser = txtUser.Text
        DBPassword = txtPass.Text
    End Sub
End Class