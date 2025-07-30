Public Class frmVCE_Add
    Public VCEType, VCECode, VCEName As String

    Private Sub frmVCE_Add_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        cleartext()
        txtVCEName.Text = VCEName
        cbVatType.SelectedIndex = 0
        cbWTaxType.SelectedIndex = 0
    End Sub

    Public Overloads Function ShowDialog(ByVal Type As String, Value As String) As Boolean
        VCEType = Type
        VCEName = Value
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub ClearText()
        txtVCEName.Text = ""
        txtFirstName.Text = ""
        txtMiddleName.Text = ""
        txtTinNo.Text = ""
        txtUnit.Text = ""
        txtBlkLot.Text = ""
        txtStreet.Text = ""
        txtSubd.Text = ""
        txtBrgy.Text = ""
        txtCity.Text = ""
        txtProvince.Text = ""
        txtZipCode.Text = ""
        txtTelephone.Text = ""
        txtCellphone.Text = ""
        txtFax.Text = ""
        txtEmail.Text = ""
        txtWebsite.Text = ""
        txtContact.Text = ""
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        If txtVCEName.Text = "" Then
            MsgBox("Please enter VCE Name!", MsgBoxStyle.Exclamation)
        ElseIf MessageBox.Show("Saving " & VCEType & " information" & vbNewLine & " Continue?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
            VCECode = GetVCECode(VCEType)
            SaveVCE(VCECode, VCEType)
            MsgBox("New Record Saved successfully!", MsgBoxStyle.Information)
            Me.Close()
        End If
    End Sub

    Private Sub SaveVCE(Code As String, Type As String)
        activityStatus = True
        Try
            Dim insertSQL As String
            'insertSQL = " INSERT INTO " & _
            '            " tblMasterfile_VCE(VCECode, VCEName, VCE_Type, Address_Unit, Address_Lot_Blk, Address_Street, Address_Subd, Address_Brgy,  " & _
            '            " 	             Address_City, Address_Province, Address_ZipCode, Contact_Telephone, Contact_Cellphone, Contact_Fax, " & _
            '            "                Contact_Email, Contact_Website, Contact_Person, Terms, TIN_No, VAT_Code, WTax_Code, PEZA) " & _
            '            " VALUES(@VCECode, @VCEName, @VCE_Type, @Address_Unit, @Address_Lot_Blk, @Address_Street, @Address_Subd, @Address_Brgy,  " & _
            '            " 	     @Address_City, @Address_Province, @Address_ZipCode, @Contact_Telephone, @Contact_Cellphone, @Contact_Fax, " & _
            '            "        @Contact_Email, @Contact_Website, @Contact_Person, @Terms, @TIN_No, @VAT_Code, @WTax_Code, @PEZA)"
            'SQL.FlushParams()
            'SQL.AddParam("@VCECode", VCECode)
            'SQL.AddParam("@VCEName", txtVCEName.Text)
            'SQL.AddParam("@VCE_Type", Type)
            'SQL.AddParam("@Address_Unit", txtUnit.Text)
            'SQL.AddParam("@Address_Lot_Blk", txtBlkLot.Text)
            'SQL.AddParam("@Address_Street", txtStreet.Text)
            'SQL.AddParam("@Address_Subd", txtSubd.Text)
            'SQL.AddParam("@Address_Brgy", txtBrgy.Text)
            'SQL.AddParam("@Address_City", txtCity.Text)
            'SQL.AddParam("@Address_Province", txtProvince.Text)
            'SQL.AddParam("@Address_ZipCode", txtZipCode.Text)
            'SQL.AddParam("@Contact_Telephone", txtTelephone.Text)
            'SQL.AddParam("@Contact_Cellphone", txtCellphone.Text)
            'SQL.AddParam("@Contact_Fax", txtFax.Text)
            'SQL.AddParam("@Contact_Email", txtEmail.Text)
            'SQL.AddParam("@Contact_Website", txtWebsite.Text)
            'SQL.AddParam("@Contact_Person", txtContact.Text)
            'SQL.AddParam("@Terms", txtTerms.Text)
            'SQL.AddParam("@TIN_No", txtTinNo.Text)
            'SQL.AddParam("@VAT_Code", cbVatType.SelectedItem)
            'SQL.AddParam("@WTax_Code", cbWTaxType.SelectedItem)
            'SQL.AddParam("@PEZA", chkPEZA.Checked)
            'SQL.ExecNonQuery(insertSQL)


            insertSQL = " INSERT INTO " & _
                        " tblVCE_Master(VCECode, VCEName, TIN_No, isVendor, isCustomer, isMember, isEmployee, isOther, Terms, " & _
                        "               Address_Unit, Address_Lot_Blk, Address_Street, Address_Subd, Address_Brgy,  " & _
                        " 	             Address_City, Address_Province, Address_ZipCode, Contact_Telephone, Contact_Cellphone, Contact_Fax, " & _
                        "               Contact_Email, Contact_Website, Contact_Person,  VAT_Code, WTax_Code, PEZA, WhoCreated, Spouse_Name, Birth_Date,  Membership_Date, Ins_Company, " & _
                        "               Last_Name, First_Name, Middle_Name, BankName, BankAccount, CostCenter, ProfitCenter, CurrencyCodes) " & _
                        " VALUES(@VCECode, @VCEName , @TIN_No, @isVendor, @isCustomer, @isMember, @isEmployee, @isOther, @Terms, " & _
                        "        @Address_Unit, @Address_Lot_Blk, @Address_Street, @Address_Subd, @Address_Brgy,  " & _
                        " 	      @Address_City, @Address_Province, @Address_ZipCode, @Contact_Telephone, @Contact_Cellphone, @Contact_Fax, " & _
                        "        @Contact_Email, @Contact_Website, @Contact_Person, @VAT_Code, @WTax_Code, @PEZA, @WhoCreated, @Spouse_Name, @Birth_Date,  @Membership_Date, @Ins_Company, " & _
                        "        @Last_Name, @First_Name, @Middle_Name, @BankName, @BankAccount, @CostCenter, @ProfitCenter, @CurrencyCodes) "
            SQL.FlushParams()
            SQL.AddParam("@VCECode", Code)
            If rbCompany.Checked = True Then
                SQL.AddParam("@VCEName", txtVCEName.Text)
                SQL.AddParam("@Last_Name", "")
                SQL.AddParam("@First_Name", "")
                SQL.AddParam("@Middle_Name", "")
            Else
                SQL.AddParam("@VCEName", "")
                SQL.AddParam("@Last_Name", txtVCEName.Text)
                SQL.AddParam("@First_Name", txtFirstName.Text)
                SQL.AddParam("@Middle_Name", txtMiddleName.Text)
            End If
            SQL.AddParam("@TIN_No", txtTinNo.Text)
            SQL.AddParam("@isVendor", False)
            SQL.AddParam("@isCustomer", True)
            SQL.AddParam("@isMember", False)
            SQL.AddParam("@isEmployee", False)
            SQL.AddParam("@isOther", False)
            SQL.AddParam("@Address_Unit", txtUnit.Text)
            SQL.AddParam("@Address_Lot_Blk", txtBlkLot.Text)
            SQL.AddParam("@Address_Street", txtStreet.Text)
            SQL.AddParam("@Address_Subd", txtSubd.Text)
            SQL.AddParam("@Address_Brgy", txtBrgy.Text)
            SQL.AddParam("@Address_City", txtCity.Text)
            SQL.AddParam("@Address_Province", txtProvince.Text)
            SQL.AddParam("@Address_ZipCode", txtZipCode.Text)
            SQL.AddParam("@Contact_Telephone", txtTelephone.Text)
            SQL.AddParam("@Contact_Cellphone", txtCellphone.Text)
            SQL.AddParam("@Contact_Fax", txtFax.Text)
            SQL.AddParam("@Contact_Email", txtEmail.Text)
            SQL.AddParam("@Contact_Website", txtWebsite.Text)
            SQL.AddParam("@Contact_Person", txtContact.Text)
            SQL.AddParam("@WTax_Code", IIf(cbWTaxType.SelectedItem = Nothing, "", cbWTaxType.SelectedItem))
            SQL.AddParam("@VAT_Code", IIf(cbVatType.SelectedItem = Nothing, "", cbVatType.SelectedItem))
            SQL.AddParam("@CostCenter", "")
            SQL.AddParam("@ProfitCenter", "")
            SQL.AddParam("@PEZA", chkPEZA.Checked)
            SQL.AddParam("@Terms", "")
            SQL.AddParam("@WhoCreated", UserID)
            SQL.AddParam("@Spouse_Name", "")
            SQL.AddParam("@Birth_Date", Date.Today)
            SQL.AddParam("@Membership_Date", Date.Today)
            SQL.AddParam("@Ins_Company", "")
            SQL.AddParam("@BankName", "")
            SQL.AddParam("@BankAccount", "")
            SQL.AddParam("@CurrencyCodes", BaseCurrency)

            SQL.ExecNonQuery(insertSQL)
            VCECode = Code
            VCEName = txtVCEName.Text
        Catch ex As Exception
            MsgBox(ex.Message)
            activityStatus = False
        Finally
            '        RecordActivity(UserID, ModuleID, "INSERT", "Inserted VCECode : " & VCECode, activityStatus)
        End Try

    End Sub

    Private Function GetVCECode(VCEType As String) As String
        Dim query As String
        query = "  SELECT   LEFT('" & VCEType & "',1) + RIGHT('000000' +  Cast(ISNULL(Max(Cast(REPLACE(VCECode, LEFT(VCECode,1),'')as int)+1),1) AS nvarchar),6) AS VCECode " & _
                    " FROM     tblVCE_Master " & _
                    " WHERE    LEFT(VCECode,1) =LEFT('" & VCEType & "',1) "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("VCECode")
        Else
            Return Strings.Left(VCEType, 1) + "00001"
        End If
    End Function

    Private Sub rbCompany_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbCompany.CheckedChanged, rbIndividual.CheckedChanged
        If rbCompany.Checked = True Then
            Label2.Text = "VCE Name :"
            txtFirstName.Visible = False
            txtMiddleName.Visible = False
            Label1.Visible = False
            Label25.Visible = False
        ElseIf rbIndividual.Checked = True Then
            Label2.Text = "Last Name :"
            txtMiddleName.Visible = True
            txtFirstName.Visible = True
            Label1.Visible = True
            Label25.Visible = True
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class