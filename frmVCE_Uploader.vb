Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmVCE_Uploader

    Dim moduleID As String = "VCE"
    Dim FileName As String
    Dim templateName As String = "VCE UPLOADER"
    Public isSetup As Boolean = False


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
                    Contact_Person, Terms, VAT_Code, WTax_Code, PEZA, BankName, BankAccount As String

                VCECode = RTrim(objExcel.Range("a" & CStr(ctrN)).Value)
                VCEName = RTrim(objExcel.Range("b" & CStr(ctrN)).Value)
                LastName = RTrim(objExcel.Range("c" & CStr(ctrN)).Value)
                FirstName = RTrim(objExcel.Range("d" & CStr(ctrN)).Value)
                MiddleName = RTrim(objExcel.Range("e" & CStr(ctrN)).Value)
                TIN_No = RTrim(objExcel.Range("f" & CStr(ctrN)).Value)
                Terms = RTrim(objExcel.Range("g" & CStr(ctrN)).Value)
                Address_Unit = RTrim(objExcel.Range("h" & CStr(ctrN)).Value)
                Address_Lot_Blk = RTrim(objExcel.Range("i" & CStr(ctrN)).Value)
                Address_Street = RTrim(objExcel.Range("j" & CStr(ctrN)).Value)
                Address_Subd = RTrim(objExcel.Range("k" & CStr(ctrN)).Value)
                Address_Brgy = RTrim(objExcel.Range("l" & CStr(ctrN)).Value)
                Address_City = RTrim(objExcel.Range("m" & CStr(ctrN)).Value)
                Address_Province = RTrim(objExcel.Range("n" & CStr(ctrN)).Value)
                Address_ZipCode = RTrim(objExcel.Range("o" & CStr(ctrN)).Value)
                Contact_Telephone = RTrim(objExcel.Range("p" & CStr(ctrN)).Value)
                Contact_Cellphone = RTrim(objExcel.Range("q" & CStr(ctrN)).Value)
                Contact_Fax = RTrim(objExcel.Range("r" & CStr(ctrN)).Value)
                Contact_Email = RTrim(objExcel.Range("s" & CStr(ctrN)).Value)
                Contact_Website = RTrim(objExcel.Range("t" & CStr(ctrN)).Value)
                Contact_Person = RTrim(objExcel.Range("u" & CStr(ctrN)).Value)
                VAT_Code = RTrim(objExcel.Range("v" & CStr(ctrN)).Value)
                WTax_Code = RTrim(objExcel.Range("w" & CStr(ctrN)).Value)
                PEZA = RTrim(objExcel.Range("x" & CStr(ctrN)).Value)
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
                VAT_Code.ToString,
                WTax_Code.ToString,
                PEZA.ToString,
                BankName.ToString,
                BankAccount.ToString})


                ctrN = ctrN + 1
                str = RTrim(objExcel.Range("a" & CStr(ctrN)).Value)
            Loop
            objExcel.Workbooks.Close()

        End If
    End Sub


    Private Sub SaveVCE_upload()
        Try
            activityStatus = True
            Dim insertSQL As String
            Dim i As Integer = 0
            For Each row As DataGridViewRow In dgvEntry.Rows
                If Not row.Cells(0).Value Is Nothing Then

                    If Not RecordExist(row.Cells(chVCECode.Index).Value.ToString) Then
                        insertSQL = " INSERT INTO " &
                       " tblVCE_Master(VCECode, VCEName, Last_Name, First_Name, Middle_Name, TIN_No, isVendor, isCustomer, isMember, isEmployee, Terms, " &
                       "               Address_Unit, Address_Lot_Blk, Address_Street, Address_Subd, Address_Brgy,  " &
                       " 	             Address_City, Address_Province, Address_ZipCode, Contact_Telephone, Contact_Cellphone, Contact_Fax, " &
                       "                Contact_Email, Contact_Website, Contact_Person,  VAT_Code, WTax_Code, PEZA, BankName, BankAccount, CurrencyCodes) " &
                       " VALUES(@VCECode, @VCEName, @Last_Name, @First_Name, @Middle_Name , @TIN_No, @isVendor, @isCustomer, @isMember, @isEmployee, @Terms, " &
                       "        @Address_Unit, @Address_Lot_Blk, @Address_Street, @Address_Subd, @Address_Brgy,  " &
                       " 	      @Address_City, @Address_Province, @Address_ZipCode, @Contact_Telephone, @Contact_Cellphone, @Contact_Fax, " &
                       "        @Contact_Email, @Contact_Website, @Contact_Person, @VAT_Code, @WTax_Code, @PEZA, @BankName, @BankAccount, @CurrencyCodes)"
                        SQL.FlushParams()
                        SQL.AddParam("@VCECode", row.Cells(chVCECode.Index).Value.ToString)
                        SQL.AddParam("@VCEName", row.Cells(chVCEName.Index).Value.ToString)
                        SQL.AddParam("@Last_Name", row.Cells(chLastName.Index).Value.ToString)
                        SQL.AddParam("@First_Name", row.Cells(chFirstName.Index).Value.ToString)
                        SQL.AddParam("@Middle_Name", row.Cells(chMiddleName.Index).Value.ToString)
                        SQL.AddParam("@TIN_No", row.Cells(chTinNo.Index).Value.ToString)
                        If cbType.SelectedItem = "Vendor" Then
                            SQL.AddParam("@isVendor", True)
                            SQL.AddParam("@isCustomer", False)
                            SQL.AddParam("@isMember", False)
                            SQL.AddParam("@isEmployee", False)
                        ElseIf cbType.SelectedItem = "Customer" Then
                            SQL.AddParam("@isVendor", False)
                            SQL.AddParam("@isCustomer", True)
                            SQL.AddParam("@isMember", False)
                            SQL.AddParam("@isEmployee", False)
                        ElseIf cbType.SelectedItem = "Employee" Then
                            SQL.AddParam("@isVendor", False)
                            SQL.AddParam("@isCustomer", False)
                            SQL.AddParam("@isMember", False)
                            SQL.AddParam("@isEmployee", True)
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
                        SQL.AddParam("@VAT_Code", row.Cells(chVATCode.Index).Value.ToString)
                        SQL.AddParam("@WTax_Code", row.Cells(chWTAXCode.Index).Value.ToString)
                        SQL.AddParam("@Terms", row.Cells(chTerms.Index).Value.ToString)
                        SQL.AddParam("@PEZA", row.Cells(chPEZA.Index).Value)
                        SQL.AddParam("@BankName", row.Cells(chBankName.Index).Value)
                        SQL.AddParam("@BankAccount", row.Cells(chBankAccount.Index).Value)
                        SQL.AddParam("@CurrencyCodes", BaseCurrency)
                        SQL.ExecNonQuery(insertSQL)
                    Else
                        MsgBox("Duplicated VCE Code " & row.Cells(chVCECode.Index).Value.ToString & "", MsgBoxStyle.Exclamation)
                        i -= 1
                    End If
                    i += 1
                End If
            Next
            If i > 0 Then
                MessageBox.Show("Added Successfully!")
            End If
        Catch ex As System.Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        Finally
            RecordActivity(UserID, moduleID, Me.Name.ToString, "INSERT", "VCECode", "UPLOAD", BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
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

            cbType.SelectedIndex = -1
            dgvEntry.Rows.Clear()
            ' Toolstrip Buttons
            tsbNew.Enabled = False
            tsbSave.Enabled = True
            tsbDownload.Enabled = True
            tsbUpload.Enabled = True
            tsbClose.Enabled = True
            tsbExit.Enabled = False


            EnableControl(True)
        End If
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        cbType.Enabled = Value
        dgvEntry.Enabled = Value
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click


        ' Toolstrip Buttons
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbDownload.Enabled = False
        tsbUpload.Enabled = False
        tsbExit.Enabled = True
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        If isSetup = True Then
            tsbSave.Visible = False
        End If
        If cbType.SelectedIndex = -1 Then
            Msg("Please select VCE Type!", MsgBoxStyle.Exclamation)
        ElseIf dgvEntry.Rows.Count = 0 Then
            Msg("Please upload data!", MsgBoxStyle.Exclamation)
        Else
            If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                SaveVCE_upload()
            End If
        End If
    End Sub

    Private Sub frmVCE_Uploader_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If isSetup = False Then
            tsbNew.Enabled = True
            tsbSave.Enabled = False
            tsbClose.Enabled = False
            tsbDownload.Enabled = False
            tsbUpload.Enabled = False
            tsbExit.Enabled = True
            EnableControl(False)
        Else
            tsbSave.Visible = False
            tsbClose.Visible = False
            tsbExit.Visible = False
            tsbNew.PerformClick()
        End If
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
End Class