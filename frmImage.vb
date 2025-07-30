Public Class frmImage
    Dim img As Image
    Dim FileName As String
    Public Overloads Function ShowDialog(ByVal img1 As Image, File As String) As Boolean
        img = img1
        FileName = File
        MyBase.ShowDialog()
        Return True
    End Function
    Private Sub frmImage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackgroundImage = img
        Me.BackgroundImageLayout = ImageLayout.Zoom
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub frmImage_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub DownloadImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DownloadImageToolStripMenuItem.Click
        If Me.BackgroundImage IsNot Nothing Then
            Dim filePath As String = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyPictures, FileName & Date.Now.ToString("yyyymmddhhmmss") & ".jpg")
            Me.BackgroundImage.Save(filePath)
            Process.Start("explorer.exe", "/select," & filePath)
        End If
    End Sub

End Class