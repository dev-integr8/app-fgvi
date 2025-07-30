Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security
Imports System.Security.Principal
Imports System.Net.NetworkInformation
Imports System.Text

Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices


Imports System.Configuration

Public Class frmVCE_Updater

    Dim moduleID As String = "VCE"
    Dim FileName As String
    Dim templateName As String = "VCE UPLOADER"
    Dim selectedquery As String = ""

    Private Sub Panel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub tsbUpload_Click(sender As System.Object, e As System.EventArgs) Handles tsbUpload.Click
        Dim OpenFileDialog As New OpenFileDialog
        Dim ctrN As Integer
        Dim str As String
        Dim objExcel As New Microsoft.Office.Interop.Excel.Application
        dgvEntry.Rows.Clear()

        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            FileName = OpenFileDialog.FileName
            objExcel.Workbooks.Open(FileName)
            str = "a"
            ctrN = 2

            Do While str <> ""
                Dim VCECode, VCEName, TIN_No, LastName, FirstName, MiddleName, Address_Unit,
                    Address_Lot_Blk, Address_Street, Address_Subd, Address_Brgy, Address_City, Address_Province,
                    Address_ZipCode, Contact_Telephone, Contact_Cellphone, Contact_Fax, Contact_Email, Contact_Website,
                    Contact_Person, Terms, VCEType, PEZA, BankName, BankAccount, Company As String

                VCECode = RTrim(objExcel.Range("a" & CStr(ctrN)).Value)
                VCEName = RTrim(objExcel.Range("b" & CStr(ctrN)).Value)
                LastName = RTrim(objExcel.Range("c" & CStr(ctrN)).Value)
                FirstName = RTrim(objExcel.Range("d" & CStr(ctrN)).Value)
                MiddleName = RTrim(objExcel.Range("e" & CStr(ctrN)).Value)
                TIN_No = RTrim(objExcel.Range("f" & CStr(ctrN)).Value)
                Terms = RTrim(objExcel.Range("g" & CStr(ctrN)).Value)
                VCEType = RTrim(objExcel.Range("h" & CStr(ctrN)).Value)
                Address_Unit = RTrim(objExcel.Range("i" & CStr(ctrN)).Value)
                Address_Lot_Blk = RTrim(objExcel.Range("j" & CStr(ctrN)).Value)
                Address_Street = RTrim(objExcel.Range("k" & CStr(ctrN)).Value)
                Address_Subd = RTrim(objExcel.Range("l" & CStr(ctrN)).Value)
                Address_Brgy = RTrim(objExcel.Range("m" & CStr(ctrN)).Value)
                Address_City = RTrim(objExcel.Range("n" & CStr(ctrN)).Value)
                Address_Province = RTrim(objExcel.Range("o" & CStr(ctrN)).Value)
                Address_ZipCode = RTrim(objExcel.Range("p" & CStr(ctrN)).Value)
                Contact_Telephone = RTrim(objExcel.Range("q" & CStr(ctrN)).Value)
                Contact_Cellphone = RTrim(objExcel.Range("r" & CStr(ctrN)).Value)
                Contact_Fax = RTrim(objExcel.Range("s" & CStr(ctrN)).Value)
                Contact_Email = RTrim(objExcel.Range("t" & CStr(ctrN)).Value)
                Contact_Website = RTrim(objExcel.Range("u" & CStr(ctrN)).Value)
                Contact_Person = RTrim(objExcel.Range("v" & CStr(ctrN)).Value)
                PEZA = RTrim(objExcel.Range("w" & CStr(ctrN)).Value)
                Company = RTrim(objExcel.Range("x" & CStr(ctrN)).Value)
                BankName = RTrim(objExcel.Range("y" & CStr(ctrN)).Value)
                BankAccount = RTrim(objExcel.Range("z" & CStr(ctrN)).Value)


                dgvEntry.Rows.Add(New String() {
                                         VCECode.ToString,
                VCEName.ToString,
                LastName.ToString,
                FirstName.ToString,
                MiddleName.ToString,
                TIN_No.ToString,
                Terms.ToString,
                VCEType.ToString,
                Address_Unit.ToString,
                Address_Lot_Blk.ToString,
                Address_Street.ToString,
                Address_Subd.ToString,
                Address_Brgy.ToString,
                Address_City.ToString,
                Address_Province.ToString,
                Address_ZipCode.ToString,
                Contact_Telephone.ToString,
                Contact_Cellphone.ToString,
                Contact_Fax.ToString,
                Contact_Email.ToString,
                Contact_Website.ToString,
                Contact_Person.ToString,
                PEZA.ToString,
                Company.ToString,
                BankName.ToString,
                BankAccount.ToString})


                ctrN = ctrN + 1
                str = RTrim(objExcel.Range("a" & CStr(ctrN)).Value)
            Loop
            objExcel.Workbooks.Close()

            EnableControl(True)
            tsbSave.Enabled = True
            tsbClose.Enabled = True
        End If
    End Sub


    Private Sub UpdateVCE()
        Try
            activityStatus = True
            Dim insertSQL As String
            Dim i As Integer = 0
            For Each row As DataGridViewRow In dgvEntry.Rows
                If Not row.Cells(0).Value Is Nothing Then

                    insertSQL = " UPDATE   tblVCE_Master " & _
                         " SET      VCEName = @VCEName, TIN_No = @TIN_No,  " & _
                         "         isVendor = @isVendor, isCustomer = @isCustomer, isMember = @isMember, isEmployee = @isEmployee, isOther = @isOther," & _
                         "         Address_Unit = @Address_Unit, Address_Lot_Blk = @Address_Lot_Blk, " & _
                         "         Address_Street = @Address_Street, Address_Subd = @Address_Subd, Address_Brgy = @Address_Brgy,  " & _
                         "         Address_City = @Address_City, Address_Province = @Address_Province, Address_ZipCode = @Address_ZipCode, " & _
                         "         Contact_Telephone = @Contact_Telephone, Contact_Cellphone = @Contact_Cellphone, Contact_Fax = @Contact_Fax, " & _
                         "         Contact_Email = @Contact_Email, Contact_Website = @Contact_Website, Contact_Person = @Contact_Person, " & _
                         "          PEZA = @PEZA, Terms = @Terms , Ins_Company = @Ins_Company, " & _
                         "          Last_Name = @Last_Name, First_Name = @First_Name, Middle_Name = @Middle_Name, BankName = @BankName, BankAccount = @BankAccount " & _
                         " WHERE   VCECode = @VCECode "
                        SQL.FlushParams()
                        SQL.AddParam("@VCECode", row.Cells(chVCECode.Index).Value.ToString)
                    SQL.AddParam("@VCEName", row.Cells(chVCEName.Index).Value.ToString)
                    SQL.AddParam("@Last_Name", row.Cells(chLastName.Index).Value.ToString)
                    SQL.AddParam("@First_Name", row.Cells(chFirstName.Index).Value.ToString)
                    SQL.AddParam("@Middle_Name", row.Cells(chMiddleName.Index).Value.ToString)
                    SQL.AddParam("@TIN_No", row.Cells(chTinNo.Index).Value.ToString)
                    SQL.AddParam("@Terms", row.Cells(chTerms.Index).Value.ToString)
                        If row.Cells(chVCEType.Index).Value.ToString = "Vendor" Then
                            SQL.AddParam("@isVendor", True)
                            SQL.AddParam("@isCustomer", False)
                            SQL.AddParam("@isMember", False)
                            SQL.AddParam("@isEmployee", False)
                            SQL.AddParam("@isOther", False)
                        ElseIf row.Cells(chVCEType.Index).Value.ToString = "Customer" Then
                            SQL.AddParam("@isVendor", False)
                            SQL.AddParam("@isCustomer", True)
                            SQL.AddParam("@isMember", False)
                            SQL.AddParam("@isEmployee", False)
                            SQL.AddParam("@isOther", False)
                        ElseIf row.Cells(chVCEType.Index).Value.ToString = "Employee" Then
                            SQL.AddParam("@isVendor", False)
                            SQL.AddParam("@isCustomer", False)
                            SQL.AddParam("@isMember", False)
                            SQL.AddParam("@isEmployee", True)
                            SQL.AddParam("@isOther", False)
                        ElseIf row.Cells(chVCEType.Index).Value.ToString = "Employee" Then
                            SQL.AddParam("@isVendor", False)
                            SQL.AddParam("@isCustomer", False)
                            SQL.AddParam("@isMember", False)
                            SQL.AddParam("@isEmployee", True)
                            SQL.AddParam("@isOther", False)
                        Else
                            SQL.AddParam("@isVendor", False)
                            SQL.AddParam("@isCustomer", False)
                            SQL.AddParam("@isMember", False)
                            SQL.AddParam("@isEmployee", False)
                            SQL.AddParam("@isOther", True)
                        End If
                        SQL.AddParam("@Address_Unit", row.Cells(chAddressUnit.Index).Value.ToString)
                        SQL.AddParam("@Address_Lot_Blk", row.Cells(chAddressLotBlk.Index).Value.ToString)
                        SQL.AddParam("@Address_Street", row.Cells(chAddressStreet.Index).Value.ToString)
                        SQL.AddParam("@Address_Subd", row.Cells(chAddressSubd.Index).Value.ToString)
                        SQL.AddParam("@Address_Brgy", row.Cells(chAddressBrgy.Index).Value.ToString)
                        SQL.AddParam("@Address_City", row.Cells(chAddressCity.Index).Value.ToString)
                        SQL.AddParam("@Address_Province", row.Cells(chAddressProvince.Index).Value.ToString)
                        SQL.AddParam("@Address_ZipCode", row.Cells(chAddressZipCode.Index).Value.ToString)
                        SQL.AddParam("@Contact_Telephone", row.Cells(chContact_Tel.Index).Value.ToString)
                        SQL.AddParam("@Contact_Cellphone", row.Cells(chContactCel.Index).Value.ToString)
                        SQL.AddParam("@Contact_Fax", row.Cells(chContactFax.Index).Value.ToString)
                        SQL.AddParam("@Contact_Email", row.Cells(chContactEmail.Index).Value.ToString)
                        SQL.AddParam("@Contact_Website", row.Cells(chContactWeb.Index).Value.ToString)
                    SQL.AddParam("@Contact_Person", row.Cells(chContactPerson.Index).Value.ToString)
                    SQL.AddParam("@PEZA", row.Cells(chPEZA.Index).Value)
                    SQL.AddParam("@Ins_Company", row.Cells(chCompany.Index).Value.ToString)
                        SQL.AddParam("@BankName", row.Cells(chBankName.Index).Value)
                        SQL.AddParam("@BankAccount", row.Cells(chBankAccount.Index).Value)
                        SQL.ExecNonQuery(insertSQL)
                    i += 1
                End If
            Next
        Catch ex As System.Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        Finally
            RecordActivity(UserID, moduleID, Me.Name.ToString, "UPDATE", "VCECode", "UPLOAD", BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Function validateDGV() As Boolean
        Dim value As Boolean = True
        For Each row As DataGridViewRow In dgvEntry.Rows
            'check item code if exist
            If Not IsNothing(row.Cells(chVCECode.Index).Value) Then
                If Not RecordExist(row.Cells(chVCECode.Index).Value) Then
                    ChangeCellColor(row.Index, chVCECode.Index)
                    value = False
                End If
            End If
        Next
        If value = False Then
            MsgBox("Some data are invalid !, Please Check highlighted cells.", MsgBoxStyle.Exclamation, "GR8 Message Alert")
        End If
        Return value
    End Function

    Private Delegate Sub ChangeCellColorInvoker(ByVal row As Integer, ByVal col As Integer)
    Private Sub ChangeCellColor(ByVal row As Integer, ByVal col As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New ChangeCellColorInvoker(AddressOf ChangeCellColor), row, col)
        Else
            dgvEntry.Rows(row).Cells(col).Style.BackColor = Color.Yellow
        End If
    End Sub

    Private Function RecordExist(ByVal Record As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblVCE_Master WHERE VCECode ='" & Record & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        If Not AllowAccess("VCE_ADD") Then
            msgRestricted()
        Else

            dgvEntry.Rows.Clear()

            Dim query As String
                query = " SELECT        ISNULL(VCECode,'') AS VCECode, ISNULL(VCEName,'') AS VCEName, ISNULL(Last_Name,'') AS Last_Name,  " & _
                            " 				ISNULL(First_Name,'') AS First_Name,  ISNULL(Middle_Name,'') AS Middle_Name,  " & _
                            " 			  ISNULL(TIN_No,'') AS TIN_No, ISNULL(Terms,'') AS Terms, " & _
                            " 		CASE WHEN isVendor = 1 THEN 'Vendor' " & _
                            " 		WHEN isCustomer = 1 THEN 'Customer' " & _
                            " 		WHEN isMember = 1 THEN 'Member' " & _
                            " 		WHEN isEmployee = 1 THEN 'Employee' " & _
                            " 		ELSE 'Others' " & _
                            " 		END AS VCEType, " & _
                            " 		 ISNULL(Address_Unit,'') AS Address_Unit ,  ISNULL(Address_Lot_Blk,'') AS Address_Lot_Blk ,  " & _
                            " 		 ISNULL(Address_Street,'') AS Address_Street  , ISNULL(Address_Subd,'') AS Address_Subd  ,  " & _
                            " 		 ISNULL(Address_Brgy,'') AS Address_Brgy , ISNULL(Address_City,'') AS Address_City ,   " & _
                            " 		 ISNULL(Address_Province,'') AS Address_Province , ISNULL(Address_ZipCode,'') AS Address_ZipCode , " & _
                            " 		 ISNULL(Contact_Telephone,'') AS Contact_Telephone , ISNULL(Contact_Cellphone,'') AS Contact_Cellphone , " & _
                            " 		 ISNULL(Contact_Fax,'') AS Contact_Fax , ISNULL(Contact_Email,'') AS Contact_Email,  " & _
                            " 		 ISNULL(Contact_Website,'') AS Contact_Website , ISNULL(Contact_Person,'') AS Contact_Person ,  " & _
                            " 		 CASE WHEN PEZA = 1 THEN 'TRUE' ELSE 'FALSE' END AS PEZA, ISNULL(Ins_Company,'') AS Company ,  " & _
                            " 		  ISNULL(BankName,'') AS BankName ,  ISNULL(BankAccount,'') AS BankAccount  " & _
                            " FROM            dbo.tblVCE_Master " & _
                            " WHERE Status = 'ACTIVE' "
                SQL.ReadQuery(query)
            Dim rowsCount As Integer = 0

            If SQL.SQLDR.HasRows Then
                While SQL.SQLDR.Read
                    dgvEntry.Rows.Add("")
                    dgvEntry.Rows(rowsCount).Cells(chVCECode.Index).Value = SQL.SQLDR("VCECode").ToString
                    dgvEntry.Rows(rowsCount).Cells(chVCEName.Index).Value = SQL.SQLDR("VCEName").ToString
                    dgvEntry.Rows(rowsCount).Cells(chLastName.Index).Value = SQL.SQLDR("Last_Name").ToString
                    dgvEntry.Rows(rowsCount).Cells(chFirstName.Index).Value = SQL.SQLDR("First_Name").ToString
                    dgvEntry.Rows(rowsCount).Cells(chMiddleName.Index).Value = SQL.SQLDR("Middle_Name").ToString
                    dgvEntry.Rows(rowsCount).Cells(chTinNo.Index).Value = SQL.SQLDR("TIN_No").ToString
                    dgvEntry.Rows(rowsCount).Cells(chTerms.Index).Value = SQL.SQLDR("Terms").ToString
                    dgvEntry.Rows(rowsCount).Cells(chVCEType.Index).Value = SQL.SQLDR("VCEType").ToString
                    dgvEntry.Rows(rowsCount).Cells(chAddressUnit.Index).Value = SQL.SQLDR("Address_Unit").ToString
                    dgvEntry.Rows(rowsCount).Cells(chAddressLotBlk.Index).Value = SQL.SQLDR("Address_Lot_Blk").ToString
                    dgvEntry.Rows(rowsCount).Cells(chAddressStreet.Index).Value = SQL.SQLDR("Address_Street").ToString
                    dgvEntry.Rows(rowsCount).Cells(chAddressSubd.Index).Value = SQL.SQLDR("Address_Subd").ToString
                    dgvEntry.Rows(rowsCount).Cells(chAddressBrgy.Index).Value = SQL.SQLDR("Address_Brgy").ToString
                    dgvEntry.Rows(rowsCount).Cells(chAddressCity.Index).Value = SQL.SQLDR("Address_City").ToString
                    dgvEntry.Rows(rowsCount).Cells(chAddressProvince.Index).Value = SQL.SQLDR("Address_Province").ToString
                    dgvEntry.Rows(rowsCount).Cells(chAddressZipCode.Index).Value = SQL.SQLDR("Address_ZipCode").ToString
                    dgvEntry.Rows(rowsCount).Cells(chContact_Tel.Index).Value = SQL.SQLDR("Contact_Telephone").ToString
                    dgvEntry.Rows(rowsCount).Cells(chContactCel.Index).Value = SQL.SQLDR("Contact_Cellphone").ToString
                    dgvEntry.Rows(rowsCount).Cells(chContactFax.Index).Value = SQL.SQLDR("Contact_Fax").ToString
                    dgvEntry.Rows(rowsCount).Cells(chContactEmail.Index).Value = SQL.SQLDR("Contact_Email").ToString
                    dgvEntry.Rows(rowsCount).Cells(chContactWeb.Index).Value = SQL.SQLDR("Contact_Website").ToString
                    dgvEntry.Rows(rowsCount).Cells(chContactPerson.Index).Value = SQL.SQLDR("Contact_Person").ToString
                    dgvEntry.Rows(rowsCount).Cells(chPEZA.Index).Value = SQL.SQLDR("PEZA").ToString
                    dgvEntry.Rows(rowsCount).Cells(chCompany.Index).Value = SQL.SQLDR("Company").ToString
                    dgvEntry.Rows(rowsCount).Cells(chBankName.Index).Value = SQL.SQLDR("BankName").ToString
                    dgvEntry.Rows(rowsCount).Cells(chBankAccount.Index).Value = SQL.SQLDR("BankAccount").ToString
                    rowsCount += 1
                End While

                ' Toolstrip Buttons
                tsbNew.Enabled = False
                tsbExtract.Enabled = True
                tsbSave.Enabled = True
                tsbDownload.Enabled = True
                tsbUpload.Enabled = True
                tsbClose.Enabled = True
                tsbExit.Enabled = False

                EnableControl(True)
            Else

                ' Toolstrip Buttons
                tsbNew.Enabled = True
                tsbSave.Enabled = False
                tsbExtract.Enabled = False
                tsbDownload.Enabled = False
                tsbUpload.Enabled = False
                tsbClose.Enabled = False
                tsbExit.Enabled = False

                EnableControl(False)
                Msg("No Record!", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        dgvEntry.Enabled = Value
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click
        dgvEntry.Rows.Clear()
        EnableControl(False)
        ' Toolstrip Buttons
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbExtract.Enabled = False
        tsbClose.Enabled = False
        tsbDownload.Enabled = False
        tsbUpload.Enabled = True
        tsbExit.Enabled = True
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If validateDGV() Then
            If dgvEntry.Rows.Count = 0 Then
                Msg("Please upload data!", MsgBoxStyle.Exclamation)
            Else
                If MsgBox("Update Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    UpdateVCE()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    tsbClose.PerformClick()

                End If
            End If
        End If
    End Sub

    Private Sub frmVCE_Uploader_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbDownload.Enabled = False
        tsbExtract.Enabled = False
        tsbUpload.Enabled = True
        tsbExit.Enabled = True
        EnableControl(False)
    End Sub

    Private Sub tsbDownload_Click(sender As System.Object, e As System.EventArgs) Handles tsbDownload.Click
        Dim fileName As String = "VCE UPLOADER.xlsx"
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        xlApp = New Excel.Application
        Dim App_Path As String
        App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName & "\Templates"
        If My.Computer.FileSystem.FileExists(App_Path + "\VCE UPLOADER.xlsx") Then
            xlWorkBook = xlApp.Workbooks.Open(App_Path + "\VCE UPLOADER.xlsx")
            xlWorkSheet = xlWorkBook.Worksheets("Sheet1")
            Dim ctr As Integer = 1
            Do
                fileName = "VCE UPLOADER -" & ctr.ToString & ".xlsx"
                If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & fileName) = False Then
                    Exit Do
                End If
                ctr += 1
            Loop
            xlWorkBook.SaveAs(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & fileName)
            xlWorkBook.Close(Type.Missing, Type.Missing, Type.Missing)
            xlApp.Quit()
            ' CLEAN UP. (CLOSE INSTANCES OF EXCEL OBJECTS.)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet) : xlWorkSheet = Nothing
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook) : xlWorkBook = Nothing
        Else
            MsgBox("No Template found!" & vbNewLine & "Please contact your systems administrator", MsgBoxStyle.Exclamation)
        End If
        ' CLEAN UP. (CLOSE INSTANCES OF EXCEL OBJECTS.)
        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp) : xlApp = Nothing
        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & fileName) Then
            Dim xls As Excel.Application
            Dim workbook As Excel.Workbook
            xls = New Excel.Application
            xls.Visible = True
            workbook = xls.Workbooks.Open(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & fileName)
        End If
    End Sub

    Private Sub dgvEntry_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEntry.CellContentClick

    End Sub

    Private Sub tsbExtract_Click(sender As System.Object, e As System.EventArgs) Handles tsbExtract.Click
        Dim Separator As String = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator

        DataGridToCSV(dgvEntry, Separator)
    End Sub

    Private Sub DataGridToCSV(ByRef dt As DataGridView, Qualifier As String)
        Dim TempDirectory As String = "Temp"
        System.IO.Directory.CreateDirectory(TempDirectory)
        Dim file As String = System.IO.Path.GetRandomFileName & ".csv"
        Dim oWrite = New StreamWriter(TempDirectory & "\" & file, False, Encoding.UTF8)

        Dim CSV As StringBuilder = New StringBuilder()

        Dim i As Integer = 1
        Dim CSVHeader As StringBuilder = New StringBuilder()
        For Each c As DataGridViewColumn In dt.Columns
            If i = 1 Then
                CSVHeader.Append(c.HeaderText.ToString())
            Else
                CSVHeader.Append(Qualifier & c.HeaderText.ToString())
            End If
            i += 1
        Next

        'CSV.AppendLine(CSVHeader.ToString())
        oWrite.WriteLine(CSVHeader.ToString())
        oWrite.Flush()

        For r As Integer = 0 To dt.Rows.Count - 2

            Dim CSVLine As StringBuilder = New StringBuilder()
            Dim s As String = ""
            For c As Integer = 0 To dt.Columns.Count - 2
                If c = 0 Then
                    'CSVLine.Append(Qualifier & gridResults.Rows(r).Cells(c).Value.ToString() & Qualifier)
                    s = s & dgvEntry.Rows(r).Cells(c).Value.ToString()
                Else
                    'CSVLine.Append("," & Qualifier & gridResults.Rows(r).Cells(c).Value.ToString() & Qualifier)
                    s = s & Qualifier & dgvEntry.Rows(r).Cells(c).Value.ToString.Replace(ControlChars.CrLf, " ")
                End If

            Next
            oWrite.WriteLine(s)
            oWrite.Flush()
            'CSV.AppendLine(CSVLine.ToString())
            'CSVLine.Clear()
        Next

        'oWrite.Write(CSV.ToString())

        oWrite.Close()
        oWrite = Nothing

        System.Diagnostics.Process.Start(TempDirectory & "\" & file)

        GC.Collect()

    End Sub
End Class