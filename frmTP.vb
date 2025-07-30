Imports Excel = Microsoft.Office.Interop.Excel
Imports System.IO

Public Class frmTP
    Dim TransID, RefID, RR_RefID, JETransiD As String
    Dim TPNo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "TP"
    Dim ColumnID As String = "TransID"
    Dim ColumnPK As String = "TP_No"
    Dim DBTable As String = "tblTP"
    Dim TransAuto As Boolean
    Dim ForApproval As Boolean = False
    Dim picPath As String


    Public Overloads Function ShowDialog(ByVal DocID As String) As Boolean
        TransID = DocID
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub TP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            dtpDocDate.Value = Date.Today.Date
            ForApproval = GetTransApproval(ModuleID)
            If TransID <> "" Then
                If Not AllowAccess("TP_VIEW") Then
                    msgRestricted()
                    dtpDocDate.Value = Date.Today.Date
                    tsbSearch.Enabled = True
                    tsbNew.Enabled = True
                    tsbSave.Enabled = False
                    tsbClose.Enabled = False
                    tsbPrevious.Enabled = False
                    tsbNext.Enabled = False
                    tsbExit.Enabled = True
                    tsbCopy.Enabled = False
                    EnableControl(False)
                Else
                    LoadTP(TransID)
                End If
            Else
                tsbSearch.Enabled = True
                tsbNew.Enabled = True
                tsbSave.Enabled = False
                tsbClose.Enabled = False
                tsbPrevious.Enabled = False
                tsbNext.Enabled = False
                tsbExit.Enabled = True
                tsbCopy.Enabled = False
                EnableControl(False)
            End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        dtpDocDate.Enabled = Value
        dgvList.AllowUserToDeleteRows = Value
        dtpDocDate.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Private Sub LoadTP(TransID As String)
        Dim query As String
        SetPayrollDatabase()
        query = " SELECT    TransID, TP_No, DateTP, Emp_ID, EmployeeName, Clearance, " &
                "           SeparationID, Separation_Type, Separation_Date, Separation_Reason, TP.Status " &
                " FROM      " & database & ".dbo.tblTP AS TP INNER JOIN viewSeparatedEmployee " &
                " ON        TP.EmpID = viewSeparatedEmployee.Emp_ID " &
                " WHERE     TransID = @TransID"
        SQL_RUBY.FlushParams()
        SQL_RUBY.AddParam("@TransID", TransID)
        SQL_RUBY.ReadQuery(query)
        Dim amount As Decimal = 0
        If SQL_RUBY.SQLDR.Read Then
            lblEmpID.Text = SQL_RUBY.SQLDR("Emp_ID").ToString
            lblEmpName.Text = SQL_RUBY.SQLDR("EmployeeName").ToString
            lblReason.Text = SQL_RUBY.SQLDR("Separation_Reason").ToString
            lblSeparationDate.Text = CDate(SQL_RUBY.SQLDR("Separation_Date")).ToString("MM-dd-yyyy")
            lblSeparationID.Text = SQL_RUBY.SQLDR("SeparationID").ToString
            lblType.Text = SQL_RUBY.SQLDR("Separation_Type").ToString
            dtpDocDate.Value = SQL_RUBY.SQLDR("DateTP")
            txtStatus.Text = SQL_RUBY.SQLDR("Status").ToString
            TPNo = SQL_RUBY.SQLDR("TP_No").ToString
            txtTransNum.Text = TPNo
            If Not IsDBNull(SQL_RUBY.SQLDR("Clearance")) Then
                pbClearance.Image = New Bitmap(byteArrayToImage(SQL_RUBY.SQLDR("Clearance")))
                pbClearance.SizeMode = PictureBoxSizeMode.Zoom
                pbClearance.Cursor = Cursors.Hand
            Else
                pbClearance.Cursor = Cursors.Default
                pbClearance.Image = Nothing
            End If
            LoadLastPay(lblEmpID.Text)

            disableEvent = True

            disableEvent = False
            ' TOOLSTRIP BUTTONS
            tsbClose.Enabled = False
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbSave.Enabled = False
            tsbNew.Enabled = True
            tsbSearch.Enabled = True
            tsbExit.Enabled = True
            tsbCopy.Enabled = False
            btnClear.Enabled = True
            EnableControl(False)
        End If

    End Sub


    Private Sub ClearText()
        txtTransNum.Clear()
        lblEmpID.Text = ""
        lblEmpName.Text = ""
        lblReason.Text = ""
        lblSeparationDate.Text = ""
        lblSeparationID.Text = ""
        lblType.Text = ""
        dgvList.Rows.Clear()
        txtStatus.Text = "Open"
        dgvList.Rows.Clear()
        pbClearance.Image = Nothing
        dtpDocDate.MinDate = GetMaxPEC().AddDays(1)
        dtpDocDate.Value = Date.Today.Date
    End Sub

    Public Function GetTransID() As Integer
        Dim query As String
        query = " SELECT MAX(TransID) + 1 AS TransID FROM tblTP"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() And Not IsDBNull(SQL.SQLDR("TransID")) Then
            Return SQL.SQLDR("TransID")
        Else
            Return 1
        End If
    End Function

    Private Sub SaveTP()
        Dim SQL1 As New SQLControl
        SQL1.BeginTransaction()
        Try
            Dim insertSQL As String
            activityStatus = True
            insertSQL = " INSERT INTO " &
                    " tblTP    (TransID, TP_No, DateTP, EmpID, TotalAmount, Clearance, Status,  DateCreated, WhoCreated, TransAuto) " &
                    " VALUES (@TransID, @TP_No, @DateTP, @EmpID, @TotalAmount, @Clearance, @Status, GETDATE(), @WhoCreated, @TransAuto) "
            SQL1.FlushParams()
            SQL1.AddParam("@TransID", TransID)
            SQL1.AddParam("@TP_No", TPNo)
            SQL1.AddParam("@DateTP", dtpDocDate.Value.Date)
            SQL1.AddParam("@EmpID", lblEmpID.Text)
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.AddParam("@TransAuto", TransAuto)
            SQL1.AddParam("@TotalAmount", CDec(txtTotal.Text))
            Dim imgStreamPic As MemoryStream = New MemoryStream()
            If picPath <> "" AndAlso My.Computer.FileSystem.FileExists(picPath) Then
                Image.FromFile(picPath).Save(imgStreamPic, System.Drawing.Imaging.ImageFormat.Png)
                imgStreamPic.Close()
                Dim byteArrayPic As Byte() = imgStreamPic.ToArray()
                SQL1.AddParam("@Clearance", byteArrayPic, SqlDbType.Image)
                SQL1.AddParam("@Status", "Cleared")
            ElseIf pbClearance.Image IsNot Nothing Then
                Dim imgPic As Image
                imgPic = pbClearance.Image
                imgPic.Save(imgStreamPic, System.Drawing.Imaging.ImageFormat.Png)
                imgStreamPic.Close()
                Dim byteArrayPic As Byte() = imgStreamPic.ToArray()
                SQL1.AddParam("@Status", "Cleared")
            Else
                SQL1.AddParam("@Clearance", DBNull.Value)
                SQL1.AddParam("@Status", "Active")
            End If

            SQL1.ExecNonQuery(insertSQL)
            SQL1.Commit()
        Catch ex As Exception
            SQL1.Rollback()
            TransID = ""
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "TP_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL1.FlushParams()
        End Try
    End Sub

    Private Sub txtAmount_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyValue <> 8 And Not (e.KeyValue >= 48 And e.KeyValue <= 57) Then
                e.SuppressKeyPress = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub tsbSearch_Click(sender As System.Object, e As System.EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("TP_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("TP")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadTP(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("TP_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            TPNo = ""

            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbSave.Enabled = True
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
            tsbCopy.Enabled = True
            EnableControl(True)
            btnClear.Enabled = False

            txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
        End If
    End Sub


    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs)
        If Not AllowAccess("TP_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)

            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbSave.Enabled = True
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
            tsbCopy.Enabled = False
        End If
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        Try
            If lblEmpID.Text = "" Then
                Msg("Please copy data from payroll!", MsgBoxStyle.Exclamation)
            ElseIf TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    TransID = GenerateTransID(ColumnID, DBTable)
                    If TransAuto Then
                        TPNo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                    Else
                        TPNo = txtTransNum.Text
                    End If
                    txtTransNum.Text = TPNo
                    SaveTP()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    LoadTP(TransID)
                End If
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub


    Private Sub tsbPrevious_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrevious.Click
        If TPNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblTP  WHERE TP_No < '" & TPNo & "' ORDER BY TP_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadTP(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If TPNo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblTP  WHERE TP_No > '" & TPNo & "' ORDER BY TP_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadTP(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        If TransID = "" Then
            ClearText()
            EnableControl(False)
            RR_RefID = 0
            RefID = 0
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            btnClear.Enabled = False
        Else
            LoadTP(TransID)
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
        End If
        tsbSearch.Enabled = True
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
        tsbCopy.Enabled = False
    End Sub
    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub frmTP_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control = True Then
            If e.KeyCode = Keys.S Then
                If tsbSave.Enabled = True Then tsbSave.PerformClick()
            ElseIf e.KeyCode = Keys.F Then
                If tsbSearch.Enabled = True Then tsbSearch.PerformClick()
            ElseIf e.KeyCode = Keys.N Then
                If tsbNew.Enabled = True Then tsbNew.PerformClick()
            ElseIf e.KeyCode = Keys.C Then
                If tsbCopy.Enabled = True Then tsbCopy.ShowDropDown()
            ElseIf e.KeyCode = Keys.Left Then
                If tsbPrevious.Enabled = True Then tsbPrevious.PerformClick()
            ElseIf e.KeyCode = Keys.Right Then
                If tsbNext.Enabled = True Then tsbNext.PerformClick()
            End If
        ElseIf e.Alt = True Then
            If e.KeyCode = Keys.F4 Then
                If tsbExit.Enabled = True Then
                    tsbExit.PerformClick()
                Else
                    e.SuppressKeyPress = True
                End If
            End If
        ElseIf e.KeyCode = Keys.Escape Then
            If tsbClose.Enabled = True Then tsbClose.PerformClick()
        End If
    End Sub


    'Start of Cost Center insert to Table
    Dim strDefaultGridView As String = ""
    Dim strDefaultGridCode As String = ""

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        With OpenFileDialog1
            .InitialDirectory = "C:\"
            .Filter = "All Files|*.*|PNG|*.png|JPEGs|*.jpg"
            .FilterIndex = 4
        End With
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            With pbClearance
                .Image = Image.FromFile(OpenFileDialog1.FileName)
                .SizeMode = PictureBoxSizeMode.Zoom
                .BorderStyle = BorderStyle.None
                picPath = OpenFileDialog1.FileName
            End With
            If TransID <> "" Then
                Dim updateSQL As String
                updateSQL = " UPDATE tblTP  SET Clearance = @Clearance, Status = @Status WHERE TransID = @TransID "
                SQL.FlushParams()
                SQL.AddParam("@TransID", TransID)
                SQL.AddParam("@Status", "Cleared")
                Dim imgStreamPic As MemoryStream = New MemoryStream()
                If picPath <> "" AndAlso My.Computer.FileSystem.FileExists(picPath) Then
                    Image.FromFile(picPath).Save(imgStreamPic, System.Drawing.Imaging.ImageFormat.Png)
                    imgStreamPic.Close()
                    Dim byteArrayPic As Byte() = imgStreamPic.ToArray()
                    SQL.AddParam("@Clearance", byteArrayPic, SqlDbType.Image)
                ElseIf pbClearance.Image IsNot Nothing Then
                    Dim imgPic As Image
                    imgPic = pbClearance.Image
                    imgPic.Save(imgStreamPic, System.Drawing.Imaging.ImageFormat.Png)
                    imgStreamPic.Close()
                    Dim byteArrayPic As Byte() = imgStreamPic.ToArray()
                Else
                    SQL.AddParam("@Clearance", DBNull.Value)

                End If
                SQL.ExecNonQuery(updateSQL)
                LoadTP(TransID)
            End If
        End If


    End Sub

    Private Sub pbClearance_Click(sender As Object, e As EventArgs) Handles pbClearance.Click
        If pbClearance.Image IsNot Nothing Then
            frmImage.ShowDialog(pbClearance.Image, "Clearance" & lblEmpID.Text)
        End If
    End Sub

    Private Sub FromRUBYSeparatedEmployeesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromRUBYSeparatedEmployeesToolStripMenuItem.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        f.ShowDialog("PAYROLL-TP")
        LoadSeparationDetails(f.transID)
        f.Dispose()
    End Sub

    Private Sub LoadSeparationDetails(ByVal EmpID As String)
        SetPayrollDatabase()
        dgvList.Rows.Clear()
        Dim query As String
        query = " SELECT    Emp_ID, EmployeeName, SeparationID, Separation_Type, Separation_Date, Separation_Reason " &
                " FROM      viewSeparatedEmployee " &
                " WHERE     Emp_ID =@Emp_ID"
        SQL_RUBY.FlushParams()
        SQL_RUBY.AddParam("@Emp_ID", EmpID)
        SQL_RUBY.ReadQuery(query)
        Dim amount As Decimal = 0
        If SQL_RUBY.SQLDR.Read Then
            lblEmpID.Text = SQL_RUBY.SQLDR("Emp_ID").ToString
            lblEmpName.Text = SQL_RUBY.SQLDR("EmployeeName").ToString
            lblReason.Text = SQL_RUBY.SQLDR("Separation_Reason").ToString
            lblSeparationDate.Text = CDate(SQL_RUBY.SQLDR("Separation_Date")).ToString("MM-dd-yyyy")
            lblSeparationID.Text = SQL_RUBY.SQLDR("SeparationID").ToString
            lblType.Text = SQL_RUBY.SQLDR("Separation_Type").ToString

            LoadLastPay(lblEmpID.Text)
            btnClear.Enabled = True
        End If
    End Sub

    Private Sub LoadLastPay(EmpID As String)
        Dim query As String
        Dim total As Decimal = 0
        dgvList.Rows.Clear()
        query = " SELECT AccntTitle, SUM(Credit) AS Amount " &
                " FROM view_GL WHERE VCECode = @VCECode AND AccntCode IN (SELECT AccntCode FROM tblDefaultAccount WHERE Type ='TP') " &
                " GROUP BY AccntTitle  " &
                " HAVING SUM(Credit) <> 0 "
        SQL.FlushParams()
        SQL.AddParam("@VCECode", EmpID)
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvList.Rows.Add({SQL.SQLDR("AccntTitle").ToString, CDec(SQL.SQLDR("Amount")).ToString("N2")})
            total += CDec(SQL.SQLDR("Amount"))
        End While
        txtTotal.Text = total.ToString("N2")
    End Sub
End Class