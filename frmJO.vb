Public Class frmJO
    Dim TransID, RefID As String
    Dim JONo As String
    Dim disableEvent As Boolean = False
    Dim ModuleID As String = "JO"
    Dim ColumnPK As String = "JO_No"
    Dim ColumnID As String = "TransID"
    Dim DBTable As String = "tblJO"
    Dim TransAuto As Boolean
    Dim AccntCode As String
    Dim SO_ID, LineID As Integer
    Dim ForApproval As Boolean = False

    Dim perSOline As Boolean

    Public Overloads Function ShowDialog(ByVal DocID As String) As Boolean
        TransID = DocID
        MyBase.ShowDialog()
        Return True
    End Function
    Private Sub EnableControl(ByVal Value As Boolean)

        dgvItemMaster.AllowUserToAddRows = Value
        dgvItemMaster.AllowUserToDeleteRows = Value
        If Value = True Then
            dgvItemMaster.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvItemMaster.EditMode = DataGridViewEditMode.EditProgrammatically
        End If

        txtRemarks.Enabled = Value
        dtpDocDate.Enabled = Value
        cbProdLine.Enabled = Value
        chkForBOM.Enabled = Value
        txtDescription.Enabled = Value
        txtVCEName.Enabled = Value
        txtQTY.Enabled = Value
        dtpDelivery.Enabled = Value
        If TransAuto Then
            txtTransNum.Enabled = False
        Else
            txtTransNum.Enabled = Value
        End If
    End Sub

    Private Sub ClearText()
        dgvItemMaster.Rows.Clear()
        txtTransNum.Text = ""
        txtItemCode.Text = ""
        txtDescription.Text = ""
        txtUOM.Text = ""
        txtQTY.Text = ""
        txtRefNo.Text = ""
        cbProdLine.Text = ""
        txtVCECode.Text = ""
        txtVCEName.Text = ""
        txtRemarks.Text = ""
        txtStatus.Text = "Open"
        dtpDocDate.MinDate = GetMaxPEC().AddDays(1)
        dtpDocDate.Value = Date.Today.Date
        dtpDelivery.Value = Date.Today.Date
    End Sub

    Private Sub LoadJO(ByVal ID As String)
        Dim query As String
        query = " SELECT    tblJO.TransID, JO_No, DateJO, VCECode,  Remarks, DateNeeded,  SO_Ref, SO_Ref_LineNum, ForBOM, viewJO_Status.Status " &
                " FROM       tblJO LEFT JOIN " &
                 " ( SELECT TransID, CASE WHEN COUNT(CASE WHEN STATUS ='Closed' THEN 0 ELSE 1  END) >=1 THEN 'Active' ELSE 'Closed' END AS Status   " &
                 " FROM viewJO_Status  GROUP BY TransID) AS   viewJO_Status  " &
                 " ON viewJO_Status.TransID = tblJO.TransID  " &
                " WHERE      tblJO.TransId = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            dtpDocDate.MinDate = "01-01-1900"
            TransID = SQL.SQLDR("TransID").ToString
            JONo = SQL.SQLDR("JO_No").ToString
            txtTransNum.Text = JONo
            txtVCECode.Text = SQL.SQLDR("VCECode").ToString
            txtRemarks.Text = SQL.SQLDR("Remarks").ToString
            txtStatus.Text = SQL.SQLDR("Status").ToString
            dtpDocDate.Text = SQL.SQLDR("DateJO").ToString
            dtpDelivery.Text = SQL.SQLDR("DateNeeded").ToString
            SO_ID = SQL.SQLDR("SO_Ref").ToString
            LineID = SQL.SQLDR("SO_Ref_LineNum").ToString
            chkForBOM.Checked = SQL.SQLDR("ForBOM")
            txtVCEName.Text = GetVCEName(txtVCECode.Text)
            txtRefNo.Text = LoadSONo(SO_ID).ToCharArray + "-" + LineID.ToString

            LoadDetails(TransID)
            ' TOOLSTRIP BUTTONS
            If txtStatus.Text = "Cancelled" Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            ElseIf txtStatus.Text = "Closed" Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            Else
                tsbEdit.Enabled = True
                tsbCancel.Enabled = True
            End If
            tsbPrint.Enabled = True
            tsbClose.Enabled = False
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbPrint.Enabled = True
            tsbSave.Enabled = False
            tsbNew.Enabled = True
            tsbSearch.Enabled = True
            tsbExit.Enabled = True
            tsbCopy.Enabled = False

            If dtpDocDate.Value <= GetMaxPEC() Then
                tsbEdit.Enabled = False
                tsbCancel.Enabled = False
            End If

            EnableControl(False)
        Else
            ClearText()
        End If

    End Sub

    Public Sub LoadDetails(ByVal TransID As String)
        Dim query As String
        Dim ctr As Integer = 0
        query = " SELECT   tblJO_Details.ItemCode, ItemName, Description, tblJO_Details.UOM, tblJO_Details.QTY, viewJO_Status.Status" &
                " FROM     tblJO_Details LEFT JOIN tblItem_Master " &
                " ON       tblJO_Details.ItemCode = tblItem_Master.ItemCode " &
                " AND tblItem_Master.Status ='Active'" &
                " LEFT JOIN viewJO_Status " &
                " ON       viewJO_Status.LineNum = tblJO_Details.LineNum " &
                " AND  viewJO_Status.TransID = tblJO_Details.TransID " &
                " WHERE    tblJO_Details.TransID = '" & TransID & "'" &
                " ORDER BY tblJO_Details.LineNum  "
        SQL.GetQuery(query)
        dgvItemMaster.Rows.Clear()
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In SQL.SQLDS.Tables(0).Rows
                dgvItemMaster.Rows.Add(New String() {row(0).ToString,
                                             row(1).ToString,
                                             row(2).ToString,
                                             row(3).ToString,
                                             CDec(row(4)).ToString("N2")})
                LoadUOM(row(0).ToString, ctr)
                If row(4).ToString = "Closed" Then
                    dgvItemMaster.Rows(ctr).ReadOnly = True
                    dgvItemMaster.AllowUserToDeleteRows = False
                    For i = 0 To dgvItemMaster.Columns.Count - 1
                        dgvItemMaster.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
                    Next i
                End If
                ctr += 1
            Next

        End If
    End Sub

    Private Function LoadSONo(SO_ID As Integer) As String
        Dim query As String
        query = " SELECT SO_No FROM tblSO WHERE TransID = '" & SO_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("SO_No")
        Else
            Return 0
        End If
    End Function

    Private Sub frmJO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            TransAuto = GetTransSetup(ModuleID)
            ForApproval = GetTransApproval(ModuleID)
            LoadSetup()
            dtpDocDate.Value = Date.Today.Date
            dtpDelivery.Value = Date.Today.Date
            If TransID <> "" Then
                LoadJO(TransID)
            End If
            LoadSetup
            LoadProdWarehouse()
            tsbSearch.Enabled = True
            tsbNew.Enabled = True
            tsbEdit.Enabled = False
            tsbSave.Enabled = False
            tsbCancel.Enabled = False
            tsbClose.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = True
            tsbPrint.Enabled = False
            tsbCopy.Enabled = False
            EnableControl(False)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadSetup()
        Dim query As String
        query = " SELECT  ISNULL(JO_perSOlineItem,0) AS JO_perSOlineItem " & _
                " FROM tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            perSOline = SQL.SQLDR("JO_perSOlineItem")
        End If
    End Sub

    Private Sub LoadProdWarehouse()
        Dim query As String
        query = " SELECT Code FROM tblProdWarehouse WHERE Status ='Active' "
        SQL.ReadQuery(query)
        cbProdLine.Items.Clear()
        While SQL.SQLDR.Read
            cbProdLine.Items.Add(SQL.SQLDR("Code").ToString)
        End While
    End Sub

    Private Sub tsbSearch_Click(sender As Object, e As EventArgs) Handles tsbSearch.Click
        If Not AllowAccess("JO_VIEW") Then
            msgRestricted()
        Else
            Dim f As New frmLoadTransactions
            f.ShowDialog("JO")
            If f.transID <> "" Then
                TransID = f.transID
            End If
            LoadJO(TransID)
            f.Dispose()
        End If
    End Sub

    Private Sub tsbNew_Click(sender As Object, e As EventArgs) Handles tsbNew.Click
        If Not AllowAccess("JO_ADD") Then
            msgRestricted()
        Else
            ClearText()
            TransID = ""
            JONo = ""
            SO_ID = 0
            LineID = 0
            LoadSetup()
            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbCancel.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
            tsbPrint.Enabled = False
            tsbCopy.Enabled = True
            EnableControl(True)


            ' GENERATE TRANSACTION NUMBER
           txtTransNum.Text = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
        End If
    End Sub

    Private Sub tsbEdit_Click(sender As Object, e As EventArgs) Handles tsbEdit.Click
        If Not AllowAccess("JO_EDIT") Then
            msgRestricted()
        Else
            EnableControl(True)
            LoadDetails(TransID)
            ' Toolstrip Buttons
            tsbSearch.Enabled = False
            tsbNew.Enabled = False
            tsbEdit.Enabled = False
            tsbSave.Enabled = True
            tsbCancel.Enabled = False
            tsbClose.Enabled = True
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbExit.Enabled = False
            tsbPrint.Enabled = False
            tsbCopy.Enabled = False
        End If
    End Sub
    Private Sub SaveJO()
        Try
            activityStatus = True
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                                " tblJO  (TransID, JO_No, VCECode, DateJO,    " & _
                                "         DateNeeded, Remarks,  SO_Ref, SO_Ref_LineNum, ForBOm,  WhoCreated, Status) " & _
                                " VALUES (@TransID, @JO_No, @VCECode, @DateJO,  " & _
                                "         @DateNeeded, @Remarks, @SO_Ref, @SO_Ref_LineNum, @ForBOm, @WhoCreated, @Status) "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@JO_No", JONo)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateJO", dtpDocDate.Text)
            SQL.AddParam("@DateNeeded", dtpDelivery.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@SO_Ref", SO_ID)
            SQL.AddParam("@SO_Ref_LineNum", LineID)
            SQL.AddParam("@ForBOm", IIf(chkForBOM.Checked = True, True, False))
            If ForApproval = True Then SQL.AddParam("@Status", "Draft") Else SQL.AddParam("@Status", "Active")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.ExecNonQuery(insertSQL)

            SaveDetails(TransID)

        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "JO_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub SaveDetails(ByVal TransID As String)
        Dim deleteSQL As String
        deleteSQL = " DELETE FROM tblJO_Details WHERE TransID = '" & TransID & "' "
        SQL.ExecNonQuery(deleteSQL)

        Dim insertSQL As String
        Dim line As Integer = 1
        For Each row As DataGridViewRow In dgvItemMaster.Rows

            If Not row.Cells(chBOMItemCode.Index).Value = Nothing Then

                insertSQL = " INSERT INTO " &
                            " tblJO_Details(TransID, ItemCode, Description, UOM, QTY, LineNum) " &
                            " VALUES (@TransID, @ItemCode, @Description, @UOM, @QTY, @LineNum) "
                SQL.FlushParams()
                SQL.AddParam("@TransID", TransID)
                SQL.AddParam("@ItemCode", IIf(row.Cells(chBOMItemCode.Index).Value = Nothing, "", row.Cells(chBOMItemCode.Index).Value))
                SQL.AddParam("@Description", IIf(row.Cells(chDescription.Index).Value = Nothing, "", row.Cells(chDescription.Index).Value))
                SQL.AddParam("@UOM", IIf(row.Cells(chBOMUOM.Index).Value = Nothing, "", row.Cells(chBOMUOM.Index).Value))
                SQL.AddParam("@QTY", IIf(row.Cells(chBOMQTY.Index).Value = Nothing, 0, CDec(row.Cells(chBOMQTY.Index).Value)))
                SQL.AddParam("@LineNum", line)
                SQL.ExecNonQuery(insertSQL)
                line += 1
            End If
        Next
    End Sub

    Private Sub UpdateJO()
        Try
            activityStatus = True
            Dim updateSQL As String

            updateSQL = " UPDATE tblJO " & _
                        " SET    JO_No = @JO_No,  DateJO = @DateJO, VCECode = @VCECode, " & _
                        "       DateNeeded = @DateNeeded, Remarks = @Remarks, " & _
                        "        SO_Ref = @SO_Ref, SO_Ref_LineNum = @SO_Ref_LineNum, ForBOm = @ForBOm, WhoModified = @WhoModified, DateModified = GETDATE() " & _
                        " WHERE  TransID = @TransID "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@JO_No", JONo)
            SQL.AddParam("@VCECode", txtVCECode.Text)
            SQL.AddParam("@DateJO", dtpDocDate.Text)
            SQL.AddParam("@DateNeeded", dtpDelivery.Value.Date)
            SQL.AddParam("@Remarks", txtRemarks.Text)
            SQL.AddParam("@SO_Ref", SO_ID)
            SQL.AddParam("@SO_Ref_LineNum", LineID)
            SQL.AddParam("@ForBOm", IIf(chkForBOM.Checked = True, True, False))
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)

            SaveDetails(TransID)

        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "JO_No", txtTransNum.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        If txtVCECode.Text = "" Then
            Msg("Please enter VCECode!", MsgBoxStyle.Exclamation)
        ElseIf dgvItemMaster.Rows.Count <= 1 Then
            Msg("There are no items on the list!", MsgBoxStyle.Exclamation)
        ElseIf TransID = "" Then
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                TransID = GenerateTransID(ColumnID, DBTable)
                JONo = GenerateTransNum(TransAuto, ModuleID, ColumnPK, DBTable)
                txtTransNum.Text = JONo
                SaveJO()
                Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                JONo = txtTransNum.Text
                LoadJO(TransID)
            End If
        Else
            If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                UpdateJO()
                Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                JONo = txtTransNum.Text
                LoadJO(TransID)
            End If
        End If
    End Sub

    Private Sub tsbCancel_Click(sender As Object, e As EventArgs) Handles tsbCancel.Click
        If Not AllowAccess("JO_DEL") Then
            msgRestricted()
        Else
            If txtTransNum.Text <> "" Then
                If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "JADE Message Alert") = MsgBoxResult.Yes Then
                    Try
                        activityStatus = True
                        Dim deleteSQL As String
                        deleteSQL = " UPDATE  tblJO SET Status ='Cancelled', WhoModified = @WhoModified WHERE JO_No = @JO_No "
                        SQL.FlushParams()
                        JONo = txtTransNum.Text
                        SQL.AddParam("@JO_No", JONo)
                        SQL.AddParam("@WhoModified", UserID)
                        SQL.ExecNonQuery(deleteSQL)
                        Msg("Record cancelled successfully", MsgBoxStyle.Information)

                        tsbSearch.Enabled = True
                        tsbNew.Enabled = True
                        tsbEdit.Enabled = False
                        tsbSave.Enabled = False
                        tsbCancel.Enabled = False
                        tsbClose.Enabled = False
                        tsbPrevious.Enabled = False
                        tsbNext.Enabled = False
                        tsbExit.Enabled = True
                        tsbPrint.Enabled = True
                        tsbCopy.Enabled = False
                        EnableControl(False)

                        JONo = txtTransNum.Text
                        LoadJO(JONo)
                    Catch ex As Exception
                        activityStatus = True
                        SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
                    Finally
                        RecordActivity(UserID, ModuleID, Me.Name.ToString, "CANCEL", "JO_No", JONo, BusinessType, BranchCode, "", activityStatus)
                        SQL.FlushParams()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub LoadSO(ByVal ID As String)
        If perSOline Then
            If ID.Contains("-") Then
                SO_ID = Strings.Left(ID, ID.IndexOf("-"))
                LineID = Strings.Mid(ID, ID.IndexOf("-") + 2, ID.ToString.Length - SO_ID.ToString.Length)
            End If
            Dim query As String
            query = " SELECT     tblSO.TransID, SO_No, tblSO_Details.VCECode, DateSO, tblSO_Details.DateDeliver, Remarks, " &
                    "            ItemCode, Description, UOM, QTY, tblSO_Details.Status, LineNum " &
                    " FROM       tblSO INNER JOIN tblSO_Details " &
                    " ON         tblSO.TransID = tblSO_Details.TransID " &
                    " AND        tblSO_Details.LineNum = '" & LineID & "' " &
                    " WHERE      tblSO.TransId = '" & SO_ID & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                txtRefNo.Text = SQL.SQLDR("SO_No").ToString & "-" & SQL.SQLDR("LineNum").ToString
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                dtpDelivery.Text = SQL.SQLDR("DateDeliver").ToString
                txtItemCode.Text = SQL.SQLDR("ItemCode").ToString
                txtDescription.Text = SQL.SQLDR("Description").ToString
                txtUOM.Text = SQL.SQLDR("UOM").ToString
                txtQTY.Text = SQL.SQLDR("QTY").ToString
                txtRemarks.Text = SQL.SQLDR("Remarks").ToString
                txtVCEName.Text = GetVCEName(txtVCECode.Text)
            Else
                ClearText()
            End If
        Else ' IF NOT PER LINE ITEM
            Dim query As String
            query = " SELECT    CAST(TransID AS nvarchar) + '-' + CAST(RowID AS nvarchar)  AS TransID,  " &
                    " SO_No, DateSO, VCECode, ItemCode, Description, UOM, QTY, DateDeliver, ReferenceNo, Status, Remarks   " &
                    " FROM      viewSO_SKU " &
                    " WHERE     TransId = '" & ID & "' "
            SQL.ReadQuery(query)
            dgvItemMaster.Rows.Clear()

            While SQL.SQLDR.Read
                txtRefNo.Text = SQL.SQLDR("SO_No").ToString
                txtVCECode.Text = SQL.SQLDR("VCECode").ToString
                dtpDelivery.Text = SQL.SQLDR("DateDeliver").ToString
                txtRemarks.Text = SQL.SQLDR("Remarks").ToString
                txtVCEName.Text = GetVCEName(txtVCECode.Text)
                dgvItemMaster.Rows.Add({SQL.SQLDR("ItemCode").ToString, SQL.SQLDR("Description").ToString, "", SQL.SQLDR("UOM").ToString, SQL.SQLDR("QTY").ToString})

                LoadUOM(SQL.SQLDR("ItemCode").ToString, dgvItemMaster.Rows.Count - 2)
            End While
        End If
        

    End Sub

    Private Sub tsbCopyPR_Click(sender As Object, e As EventArgs) Handles tsbCopyPR.Click
        Dim f As New frmLoadTransactions
        f.cbFilter.SelectedItem = "Status"
        f.txtFilter.Text = "Active"
        f.txtFilter.Enabled = False
        f.cbFilter.Enabled = False
        f.btnSearch.Enabled = False
        If perSOline Then
            f.ShowDialog("SO-JO-perLine")
        Else
            f.ShowDialog("SO-JO")
        End If
        LoadSO(f.transID)
        f.Dispose()
    End Sub


    Private Sub tsbNext_Click(sender As Object, e As EventArgs) Handles tsbNext.Click
        If JONo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblJO  WHERE JO_No > '" & JONo & "' ORDER BY JO_No ASC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadJO(TransID)
            Else
                Msg("Reached the end of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As Object, e As EventArgs) Handles tsbPrevious.Click
        If JONo <> "" Then
            Dim query As String
            query = " SELECT Top 1 TransID FROM tblJO  WHERE JO_No < '" & JONo & "' ORDER BY JO_No DESC "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                TransID = SQL.SQLDR("TransID").ToString
                LoadJO(TransID)
            Else
                Msg("Reached the beginning of record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub btnSearchVCE_Click(sender As Object, e As EventArgs)
        Dim f As New frmVCE_Search
        f.Type = "Customer"
        f.ShowDialog()
        txtVCECode.Text = f.VCECode
        txtVCEName.Text = f.VCEName
    End Sub

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbClose_Click(sender As Object, e As EventArgs) Handles tsbClose.Click
        ' Toolstrip Buttons
        If JONo = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbCancel.Enabled = False
            tsbPrevious.Enabled = False
            tsbNext.Enabled = False
            tsbPrint.Enabled = False
        Else
            LoadJO(JONo)
            tsbEdit.Enabled = True
            tsbCancel.Enabled = True
            tsbPrevious.Enabled = True
            tsbNext.Enabled = True
            tsbPrint.Enabled = True
        End If
        tsbSearch.Enabled = True
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
        tsbCopy.Enabled = False
    End Sub

    Private Sub txtVCEName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtVCEName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmVCE_Search
            f.cbFilter.SelectedItem = "VCEName"
            f.txtFilter.Text = txtVCEName.Text
            f.ShowDialog()
            txtVCECode.Text = f.VCECode
            txtVCEName.Text = f.VCEName
        End If
    End Sub

    Private Sub ToBOMToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub frmJO_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control = True Then
            If e.KeyCode = Keys.S Then
                If tsbSave.Enabled = True Then tsbSave.PerformClick()
            ElseIf e.KeyCode = Keys.F Then
                If tsbSearch.Enabled = True Then tsbSearch.PerformClick()
            ElseIf e.KeyCode = Keys.N Then
                If tsbNew.Enabled = True Then tsbNew.PerformClick()
            ElseIf e.KeyCode = Keys.E Then
                If tsbEdit.Enabled = True Then tsbEdit.PerformClick()
            ElseIf e.KeyCode = Keys.D Then
                If tsbCancel.Enabled = True Then tsbCancel.PerformClick()
            ElseIf e.KeyCode = Keys.Left Then
                If tsbPrevious.Enabled = True Then tsbPrevious.PerformClick()
            ElseIf e.KeyCode = Keys.Right Then
                If tsbNext.Enabled = True Then tsbNext.PerformClick()
            ElseIf e.KeyCode = Keys.P Then
                If tsbPrint.Enabled = True Then tsbPrint.PerformClick()
            ElseIf e.KeyCode = Keys.R Then
                If tsbReports.Enabled = True Then tsbReports.PerformClick()
            ElseIf e.KeyCode = Keys.C Then
                If tsbReports.Enabled = True Then tsbCopy.ShowDropDown()
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

    Private Sub tsbPrint_Click(sender As System.Object, e As System.EventArgs) Handles tsbPrint.Click
        Dim f As New frmReport_Display
        f.ShowDialog("JO", TransID)
        f.Dispose()
    End Sub

    Private Sub txtDescription_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtDescription.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim f As New frmCopyFrom
            f.ShowDialog("All Item", txtDescription.Text)
            txtItemCode.Text = f.TransID
            txtDescription.Text = GetItemName(txtItemCode.Text)
            LoadUOM(txtItemCode.Text)
            txtQTY.Select()
            f.Dispose()
        End If
    End Sub

    Private Sub LoadUOM(ByVal ItemCode As String)
        Dim query As String
        query = " SELECT TOP 1  UOM.UnitCode FROM tblItem_Master INNER JOIN  " & _
               " ( " & _
               " SELECT GroupCode, UnitCode FROM tblUOM_Group WHERE Status ='Active' " & _
               " UNION ALL " & _
               " SELECT GroupCode, UnitCodeFrom FROM tblUOM_GroupDetails " & _
               "  UNION ALL  " & _
               "    SELECT ItemCode, ItemUOM FROM tblItem_Master WHERE Status ='Active'  " & _
               " UNION ALL " & _
               " SELECT GroupCode, UnitCodeTo FROM tblUOM_GroupDetails " & _
               " ) AS UOM " & _
               " ON tblItem_Master.ItemUOMGroup = UOM.GroupCode " & _
               " OR  tblItem_Master.ItemCode = UOM.GroupCode " & _
               " WHERE ItemCode ='" & ItemCode & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtUOM.Text = SQL.SQLDR("UnitCode").ToString
        End If

    End Sub
   

    Private Sub txtDescription_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtDescription.TextChanged

    End Sub

    Private Sub txtVCEName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVCEName.TextChanged

    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick

    End Sub

    Private Sub dgvItemMaster_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemMaster.CellClick
        If e.ColumnIndex = chBOMUOM.Index Then
            If dgvItemMaster.Rows(e.RowIndex).ReadOnly = False Then
                If e.RowIndex <> -1 AndAlso dgvItemMaster.Rows.Count > 0 Then
                    If dgvItemMaster.Item(chBOMItemCode.Index, e.RowIndex).Value <> Nothing Then
                        LoadUOM(dgvItemMaster.Item(chBOMItemCode.Index, e.RowIndex).Value.ToString, e.RowIndex)
                    End If

                    Dim dgvCol As DataGridViewComboBoxColumn
                    dgvCol = dgvItemMaster.Columns.Item(e.ColumnIndex)
                    dgvItemMaster.BeginEdit(True)
                    dgvCol.Selected = True
                    DirectCast(dgvItemMaster.EditingControl, DataGridViewComboBoxEditingControl).DroppedDown = True
                    Dim editingComboBox As ComboBox = TryCast(sender, ComboBox)
                Else

                End If
            End If
        End If
    End Sub


    Private Sub dgvItemMaster_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemMaster.CellContentClick

    End Sub

    Private Sub dgvItemMaster_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItemMaster.CellEndEdit
        Dim itemCode As String
        Dim rowIndex As Integer = dgvItemMaster.CurrentCell.RowIndex
        Dim colindex As Integer = dgvItemMaster.CurrentCell.ColumnIndex
        Select Case colindex
            Case chBOMItemCode.Index
                If dgvItemMaster.Item(chBOMItemCode.Index, e.RowIndex).Value <> "" Then
                    itemCode = dgvItemMaster.Item(chBOMItemCode.Index, e.RowIndex).Value
                    Dim f As New frmCopyFrom
                    f.ShowDialog("All Item", itemCode, "ItemCode")
                    If f.TransID <> "" Then
                        itemCode = f.TransID
                        LoadItemDetails(itemCode)
                    Else
                        dgvItemMaster.Rows.RemoveAt(e.RowIndex)
                    End If
                    f.Dispose()
                End If
            Case chItemName.Index
                If dgvItemMaster.Item(chItemName.Index, e.RowIndex).Value <> "" Then
                    itemCode = dgvItemMaster.Item(chItemName.Index, e.RowIndex).Value
                    Dim f As New frmCopyFrom
                    f.ShowDialog("All Item", itemCode, "ItemName")
                    If f.TransID <> "" Then
                        itemCode = f.TransID
                        LoadItemDetails(itemCode)
                    Else
                        dgvItemMaster.Rows.RemoveAt(e.RowIndex)
                    End If
                    f.Dispose()
                End If
        End Select
    End Sub

    Private Sub LoadItemDetails(ByVal ItemCode As String)
        Dim query As String
        query = " SELECT     ItemCode, ItemName, ItemUOM, 1 " & _
                " FROM      tblItem_Master " & _
                " WHERE     ItemCode = @ItemCode"
        SQL.FlushParams()
        SQL.AddParam("@ItemCode", IIf(ItemCode = Nothing, "", ItemCode))
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
                If dgvItemMaster.SelectedCells.Count > 0 Then
                dgvItemMaster.Item(chBOMItemCode.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemCode").ToString
                dgvItemMaster.Item(chItemName.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemName").ToString
                dgvItemMaster.Item(chBOMUOM.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = SQL.SQLDR("ItemUOM").ToString
                dgvItemMaster.Item(chBOMQTY.Index, dgvItemMaster.SelectedCells(0).RowIndex).Value = 1
                LoadUOM(SQL.SQLDR("ItemCode").ToString, dgvItemMaster.SelectedCells(0).RowIndex)
                End If
        End If
    End Sub

    Private Sub LoadUOM(ItemCode As String, ByVal SelectedIndex As Integer)
        Try
            Dim dgvCB As New DataGridViewComboBoxCell
            dgvCB = dgvItemMaster.Item(chBOMUOM.Index, SelectedIndex)
            Dim selected As String = dgvCB.Value
            dgvCB.Items.Clear()
            ' ADD ITEM UOM
            Dim query As String
            query = " SELECT    DISTINCT viewItem_UOM.UnitCode " &
              " FROM      tblItem_Master INNER JOIN viewItem_UOM " &
              " ON        tblItem_Master.ItemUOMGroup = viewItem_UOM.GroupCode " &
              " OR        tblItem_Master.ItemCode = viewItem_UOM.GroupCode " &
              " WHERE     ItemCode ='" & ItemCode & "'  "

            SQL.ReadQuery(query)
            dgvCB.Items.Clear()
            While SQL.SQLDR.Read
                dgvCB.Items.Add(SQL.SQLDR("UnitCode").ToString)
            End While
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub dgvItemMaster_DataError(sender As Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvItemMaster.DataError
        Try

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub dgvItemMaster_EditingControlShowing(sender As Object, e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvItemMaster.EditingControlShowing
        'Get the Editing Control. I personally prefer Trycast for this as it will not throw an error
        Dim editingComboBox As ComboBox = TryCast(e.Control, ComboBox)
        If Not editingComboBox Is Nothing Then
            ' Remove an existing event-handler, if present, to avoid 
            ' adding multiple handlers when the editing control is reused.
            RemoveHandler editingComboBox.SelectionChangeCommitted, New EventHandler(AddressOf editingComboBox_SelectionChangeCommitted)

            ' Add the event handler. 
            AddHandler editingComboBox.SelectionChangeCommitted, AddressOf editingComboBox_SelectionChangeCommitted
        End If

        'Prevent this event from firing twice, as is normally the case.
        RemoveHandler dgvItemMaster.EditingControlShowing, AddressOf dgvItemMaster_EditingControlShowing
    End Sub

    Private Sub editingComboBox_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rowIndex, columnIndex As Integer
        'Get the editing control
        Dim editingComboBox As ComboBox = TryCast(sender, ComboBox)
        If editingComboBox Is Nothing Then Exit Sub
        'Show your Message Boxes
        If editingComboBox.SelectedIndex <> -1 Then
            rowIndex = dgvItemMaster.SelectedCells(0).RowIndex
            columnIndex = dgvItemMaster.SelectedCells(0).ColumnIndex
            If dgvItemMaster.SelectedCells.Count > 0 Then
                Dim tempCell As DataGridViewComboBoxCell = dgvItemMaster.Item(columnIndex, rowIndex)
                Dim temp As String = editingComboBox.Text
                dgvItemMaster.Item(columnIndex, rowIndex).Selected = False
                dgvItemMaster.EndEdit(True)
                tempCell.Value = temp
            End If
        End If

        'Remove the handle to this event. It will be readded each time a new combobox selection causes the EditingControlShowing Event to fire
        RemoveHandler editingComboBox.SelectionChangeCommitted, AddressOf editingComboBox_SelectionChangeCommitted
        'Re-enable the EditingControlShowing event so the above can take place.
        AddHandler dgvItemMaster.EditingControlShowing, AddressOf dgvItemMaster_EditingControlShowing
    End Sub


    Private Sub dgvItemMaster_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs)
        If e.ColumnIndex <> -1 AndAlso e.RowIndex <> -1 AndAlso e.Button = MouseButtons.Right Then
            Dim c As DataGridViewCell = sender(e.ColumnIndex, e.RowIndex)
            If Not c.Selected Then
                c.DataGridView.ClearSelection()
                c.DataGridView.CurrentCell = c
                c.Selected = True
            End If
        End If


    End Sub

    Private Sub dgvItemMaster_KeyDown(sender As Object, e As KeyEventArgs)
        If (e.KeyCode = Keys.F10 AndAlso e.Shift) OrElse e.KeyCode = Keys.Apps Then
            e.SuppressKeyPress = True
            Dim currentCell As DataGridViewCell = sender.CurrentCell
            If currentCell Is Nothing Then
                Dim cms As ContextMenuStrip = currentCell.ContextMenuStrip
                If cms IsNot Nothing Then
                    Dim r As Rectangle = currentCell.DataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, False)
                    Dim p As Point = New Point(r.X + r.Width, r.Y + r.Height)
                    cms.Show(currentCell.DataGridView, p)
                End If
            End If
        End If
    End Sub


    Private Sub ViewItemInformationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewItemInformationToolStripMenuItem.Click

        Dim f As New frmItem_Master
        If dgvItemMaster.CurrentCell IsNot Nothing Then
            If dgvItemMaster.CurrentCell.RowIndex <> -1 Then
                Dim code As String = dgvItemMaster.Rows(dgvItemMaster.CurrentCell.RowIndex).Cells(chBOMItemCode.Index).Value.ToString
                If code <> "" Then
                    f.ShowDialog(code)
                Else
                    MsgBox("Please select an item first!", vbExclamation)
                End If
            End If
        End If
        f.Dispose()
    End Sub
End Class