Public Class frmUpload_FileArchive
    Dim SQl As New SQLControl
    Dim query As String

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click

        With OpenFileDialog1
            .InitialDirectory = "C:\"
            .Filter = "All Files|*.*|Bitmaps|*.bmp|GIFs|*.gif|JPEGs|*.jpg"
            .FilterIndex = 4
        End With
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            With pbPicture
                .Image = Image.FromFile(OpenFileDialog1.FileName)
                .SizeMode = PictureBoxSizeMode.StretchImage
                .BorderStyle = BorderStyle.Fixed3D
                txtFilePath.Text = OpenFileDialog1.FileName
            End With
        End If

    End Sub

    Private Sub LoadFileNo()
        Dim query As String
        query = " SELECT max(FileNo) as FileNo FROM tblFile_Archive "
        SQL.ReadQuery(query)

        If SQL.SQLDR.Read Then
            txtFileNo.Text = Val(SQL.SQLDR("FileNo").ToString) + 1
        Else
            txtFileNo.Text = 1
        End If
    End Sub

    Private Sub frmUpload_FileArchive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnEdit.Enabled = False
        btnDelete.Enabled = False
        btnCancel.Enabled = False
        EnableControl(False)
        LoadAllItem()


    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        dtpDocDate.Enabled = Value
        txtFileName.Enabled = Value
        txtDesc.Enabled = Value
        btnUpload.Enabled = Value
        btnSave.Enabled = Value
    End Sub

    Private Sub ClearItems()
        dtpDocDate.Value = Now()
        txtFileName.Clear()
        txtDesc.Clear()
        txtFileNo.Clear()
        txtFilePath.Clear()
        'btnUpload.Enabled = True
        'btnSave.Enabled = True
    End Sub

    Protected Sub LoadAllItem()
        Try
            query = "select * " & _
            " from tblFile_Archive " & _
            "where status <> 'Cancelled' order by FileNo desc"
            SQl.ReadQuery(query)
            lvItems.Items.Clear()
            While SQl.SQLDR.Read
                lvItems.Items.Add(SQl.SQLDR("FileNo").ToString)
                lvItems.Items(lvItems.Items.Count - 1).SubItems.Add(SQl.SQLDR("Docdate").ToString)
                lvItems.Items(lvItems.Items.Count - 1).SubItems.Add(SQl.SQLDR("FileName").ToString)
                lvItems.Items(lvItems.Items.Count - 1).SubItems.Add(SQl.SQLDR("FileDesc").ToString)
                lvItems.Items(lvItems.Items.Count - 1).SubItems.Add(SQl.SQLDR("UploadDate").ToString)
                lvItems.Items(lvItems.Items.Count - 1).SubItems.Add(SQl.SQLDR("FilePath").ToString)
                lvItems.Items(lvItems.Items.Count - 1).SubItems.Add(SQl.SQLDR("Status").ToString)
            End While

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub tsbNew_Click(sender As Object, e As EventArgs) Handles tsbNew.Click
        ClearItems()
        txtFileName.Enabled = True
        txtDesc.Enabled = True
        dtpDocDate.Enabled = True
        btnUpload.Enabled = True
        btnSave.Enabled = True
        btnEdit.Enabled = False
        btnDelete.Enabled = False
        btnCancel.Enabled = True
        btnSave.Text = "&Save"
        tsbCancel.Enabled = True
        tsbPrint.Enabled = False
        LoadFileNo()
        With pbPicture
            .Image = pbPicture.InitialImage
            .SizeMode = PictureBoxSizeMode.StretchImage
            .BorderStyle = BorderStyle.Fixed3D
        End With
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If btnSave.Text = "&Save" Then
                If txtFileName.Text = "" Then
                    MsgBox("Please enter file name!", MsgBoxStyle.Exclamation)
                ElseIf txtFilePath.Text = "" Then
                    MsgBox("Please upload file to be saved!", MsgBoxStyle.Exclamation)
                ElseIf MsgBox("Are you sure you want to save this information?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    SaveItem()
                End If
            Else
                If txtFileName.Text = "" Then
                    MsgBox("Please enter file name!", MsgBoxStyle.Exclamation)
                ElseIf txtFilePath.Text = "" Then
                    MsgBox("Please upload file to be saved!", MsgBoxStyle.Exclamation)
                ElseIf MsgBox("Are you sure you want to update this record?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    UpdateItem()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SaveItem()
        Dim insertSQL As String
        insertSQL = " INSERT INTO " & _
                    " tblFile_Archive(FileNo, DocDate, FileName, FileDesc, FilePath, UploadDate, Status) " & _
                    " VALUES (@FileNo, @DocDate, @FileName, @FileDesc, @FilePath, @UploadDate, @Status)"
        SQl.FlushParams()
        SQl.AddParam("@FileNo", txtFileNo.Text)
        SQl.AddParam("@DocDate", dtpDocDate.Text)
        SQl.AddParam("@FileName", txtFileName.Text)
        SQl.AddParam("@FileDesc", txtDesc.Text)
        SQl.AddParam("@FilePath", txtFilePath.Text)
        SQl.AddParam("@UploadDate", Now().Date)
        SQl.AddParam("@Status", "Saved")
        SQl.ExecNonQuery(insertSQL)

        MsgBox("File Saved!", vbInformation, "Saved")

        EnableControl(False)
        btnEdit.Enabled = True
        btnCancel.Enabled = False
        txtSearch.Clear()
        LoadAllItem()

    End Sub

    Private Sub UpdateItem()
        Dim UpdateSQL As String
        UpdateSQL = " Update tblFile_Archive set Docdate=@Docdate, FileName=@FileName," & _
            " FileDessc=@FileDessc, FilePath=@FilePath, UploadDate=@UploadDate, Status=@Status" & _
            " where FileNo = '" & txtFileNo.Text & "'"
        SQl.FlushParams()
        SQl.AddParam("@Docdate", dtpDocDate.Text)
        SQl.AddParam("@FileName", txtFileName.Text)
        SQl.AddParam("@FileDesc", txtDesc.Text)
        SQl.AddParam("@FilePath", txtFilePath.Text)
        SQl.AddParam("@UploadDate", Now().Date)
        SQl.AddParam("@Status", "Saved")
        SQl.ExecNonQuery(UpdateSQL)
        MsgBox("File has been Updated!", vbInformation, "Updated!")

        EnableControl(False)
        btnEdit.Enabled = True
        txtSearch.Clear()
        btnSave.Text = "&Save"
        LoadAllItem()
    End Sub

    Private Sub Preview()
        With pbPicture
            .Image = Image.FromFile(txtFilePath.Text)
            .SizeMode = PictureBoxSizeMode.StretchImage
            .BorderStyle = BorderStyle.Fixed3D
        End With
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        EnableControl(True)
        btnSave.Text = "&Update"
        tsbCancel.Enabled = True
        UpdateItem()

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearItems()
        EnableControl(False)
        btnEdit.Enabled = False
        btnCancel.Enabled = False
    End Sub

    Private Sub lvItems_Click(sender As Object, e As EventArgs) Handles lvItems.Click
        On Error Resume Next
        If lvItems.SelectedItems.Count > 0 Then
            txtFileNo.Text = lvItems.SelectedItems(0).SubItems(chFileNo.Index).Text
            dtpDocDate.Text = lvItems.SelectedItems(0).SubItems(chDocDate.Index).Text
            txtFileName.Text = lvItems.SelectedItems(0).SubItems(chFileName.Index).Text
            txtDesc.Text = lvItems.SelectedItems(0).SubItems(chFileDesc.Index).Text
            txtFilePath.Text = lvItems.SelectedItems(0).SubItems(chFilePath.Index).Text
            btnEdit.Enabled = True
            btnDelete.Enabled = True
            btnSave.Text = "&Update"
            Preview()
            tsbPrint.Enabled = True
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim delSQL As String = "Update tblFile_Archive set Status = 'Cancelled' where FileNo = '" & txtFileNo.Text & "'"
        If txtFileNo.Text <> "" Then
            If MsgBox("Are you sure to delete this selected record?", vbYesNo + MsgBoxStyle.DefaultButton2, "Confirm Delete?") = vbYes Then
                SQl.ExecNonQuery(delSQL)
                MsgBox("File deleted from the list!", vbExclamation, "Deleted")
                LoadAllItem()
                ClearItems()
                btnDelete.Enabled = False
                btnEdit.Enabled = False
            Else
                Exit Sub
            End If

        Else
            MsgBox("Please Select item to delete!", vbExclamation, "No Selected Record")

        End If
    End Sub

    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        txtSearch.Clear()
        Dim filter As String = ""
        If cbFilter.Text = "Document Date" Then
            filter = "DocDate"
        ElseIf cbFilter.Text = "Upload Date" Then
            filter = "UploadDate"
        Else
            MsgBox("Please Select filter!", vbExclamation, "No Selected Filter")
        End If
        Try
            query = "select * " & _
            " from tblFile_Archive " & _
            "where status <> 'Cancelled' and " & filter & " between '" & dtpFrom.Value & "' and '" & dtpTo.Value & "' order by FileNo desc"
            SQl.ReadQuery(query)
            lvItems.Items.Clear()
            While SQl.SQLDR.Read
                lvItems.Items.Add(SQl.SQLDR("FileNo").ToString)
                lvItems.Items(lvItems.Items.Count - 1).SubItems.Add(SQl.SQLDR("Docdate").ToString)
                lvItems.Items(lvItems.Items.Count - 1).SubItems.Add(SQl.SQLDR("FileName").ToString)
                lvItems.Items(lvItems.Items.Count - 1).SubItems.Add(SQl.SQLDR("FileDesc").ToString)
                lvItems.Items(lvItems.Items.Count - 1).SubItems.Add(SQl.SQLDR("UploadDate").ToString)
                lvItems.Items(lvItems.Items.Count - 1).SubItems.Add(SQl.SQLDR("FilePath").ToString)
                lvItems.Items(lvItems.Items.Count - 1).SubItems.Add(SQl.SQLDR("Status").ToString)
            End While

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            query = "select * " & _
            " from tblFile_Archive " & _
            "where status <> 'Cancelled' and FileName like '%" & txtSearch.Text & "%' order by FileNo desc"
            SQl.ReadQuery(query)
            lvItems.Items.Clear()
            While SQl.SQLDR.Read
                lvItems.Items.Add(SQl.SQLDR("FileNo").ToString)
                lvItems.Items(lvItems.Items.Count - 1).SubItems.Add(SQl.SQLDR("Docdate").ToString)
                lvItems.Items(lvItems.Items.Count - 1).SubItems.Add(SQl.SQLDR("FileName").ToString)
                lvItems.Items(lvItems.Items.Count - 1).SubItems.Add(SQl.SQLDR("FileDesc").ToString)
                lvItems.Items(lvItems.Items.Count - 1).SubItems.Add(SQl.SQLDR("UploadDate").ToString)
                lvItems.Items(lvItems.Items.Count - 1).SubItems.Add(SQl.SQLDR("FilePath").ToString)
                lvItems.Items(lvItems.Items.Count - 1).SubItems.Add(SQl.SQLDR("Status").ToString)
            End While

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbCancel_Click(sender As Object, e As EventArgs) Handles tsbCancel.Click
        ClearItems()
        EnableControl(False)
        btnEdit.Enabled = False
        btnCancel.Enabled = False
        txtSearch.Clear()
    End Sub

    Private Sub tsbPrint_Click(sender As Object, e As EventArgs) Handles tsbPrint.Click
        Process.Start(txtFilePath.Text)
    End Sub
End Class