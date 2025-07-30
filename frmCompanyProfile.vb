Imports System.IO
Public Class frmCompanyProfile
    Dim SQl As New SQLControl
    Dim disableEvent As Boolean = False
    Dim EmployerID As String
    Dim picPath As String
    Public isSetup As Boolean = False

    Private Sub frmCompanyProfile_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If isSetup = True Then
            btnSave.Visible = False
            btnRemove.Visible = False
        Else
            btnSave.Visible = True
            btnRemove.Visible = True
        End If
        LoadCurrencyList()
        GetEmployerInfo()
    End Sub

    Private Sub GetEmployerInfo()
        Dim query As String
        query = " SELECT  Employer_ID, Employer_Name, Employer_Type, " & _
                "         Address_Unit, Address_Lot_Blk, Address_Street, Address_Subd, Address_Brgy, Address_Municipality, Address_Province, " & _
                "         Foreign_Address, Foreign_Address_Country, Zip_Code, " & _
                "         Contact_Telephone_No, Contact_Cellphone_No, Contact_Fax, Contact_Email_Add, Contact_Website, " & _
                "         Tin_No, Employer_Head, Company_Logo, ISNULL(VAT_Reg,0) AS VAT_Reg, VAT_Type, VATExempt_ATC, " & _
                "         Classification, Name_Last, Name_First, Name_Middle, Trade_Name, Branch_Code, RDO_Code, " & _
                "         Report_Cycle, Fiscal_MonthEnd, Base_Currency " & _
                " FROM    tblCompany "
        SQl.ReadQuery(query)
        If SQl.SQLDR.Read() Then
            disableEvent = True
            EmployerID = SQl.SQLDR("Employer_ID").ToString
            txtRegName.Text = SQl.SQLDR("Employer_Name").ToString
            If SQl.SQLDR("Employer_Type").ToString = "Private" Then
                rbPrivate.Checked = True
            Else
                rbGovt.Checked = True
            End If
            If SQl.SQLDR("Classification") = "Individual" Then
                rbInd.Checked = True
            Else
                rbNonInd.Checked = True
            End If
            txtLastName.Text = SQl.SQLDR("Name_Last").ToString
            txtFirstName.Text = SQl.SQLDR("Name_First").ToString
            txtMiddleName.Text = SQl.SQLDR("Name_Middle").ToString
            txtTradeName.Text = SQl.SQLDR("Trade_Name").ToString
            txtUnit.Text = SQl.SQLDR("Address_Unit").ToString
            txtLotBlk.Text = SQl.SQLDR("Address_Lot_Blk").ToString
            txtStreet.Text = SQl.SQLDR("Address_Street").ToString
            txtSubd.Text = SQl.SQLDR("Address_Subd").ToString
            txtBrgy.Text = SQl.SQLDR("Address_Brgy").ToString
            txtCity.Text = SQl.SQLDR("Address_Municipality").ToString
            txtProvince.Text = SQl.SQLDR("Address_Province").ToString
            txtForeignAddress.Text = SQl.SQLDR("Foreign_Address").ToString
            txtForeignCountry.Text = SQl.SQLDR("Foreign_Address_Country").ToString
            txtZipCode.Text = SQl.SQLDR("Zip_Code").ToString
            txtTelephoneNo.Text = SQl.SQLDR("Contact_Telephone_No").ToString
            txtCellphoneNo.Text = SQl.SQLDR("Contact_Cellphone_No").ToString
            txtFaxNo.Text = SQl.SQLDR("Contact_Fax").ToString
            txtEmailAdd.Text = SQl.SQLDR("Contact_Email_Add").ToString
            txtWebsite.Text = SQl.SQLDR("Contact_Website").ToString
            txtTinNo.Text = SQl.SQLDR("Tin_No").ToString
            txtBranchCode.Text = SQl.SQLDR("Branch_Code").ToString
            txtRDO.Text = SQl.SQLDR("RDO_Code").ToString
            txtEmployerHead.Text = SQl.SQLDR("Employer_Head").ToString
            If SQl.SQLDR("VAT_Type").ToString = "ZeroRated" Then
                rbZeroRated.Checked = True
            ElseIf SQl.SQLDR("VAT_Type").ToString = "Exempt" Then
                rbExempt.Checked = True
            Else
                rbVAT.Checked = True
            End If
            Dim ATCCode As String = SQl.SQLDR("VATExempt_ATC").ToString
            Dim CurrencyCode As String = SQl.SQLDR("Base_Currency").ToString
            cbReportCycle.SelectedItem = SQl.SQLDR("Report_Cycle").ToString
            cbFiscalMonthEnd.SelectedText = SQl.SQLDR("Fiscal_MonthEnd").ToString
            If Not IsDBNull(SQl.SQLDR("Company_Logo")) Then
                pbPicture.Image = New Bitmap(byteArrayToImage(SQl.SQLDR("Company_Logo")))
                pbPicture.SizeMode = PictureBoxSizeMode.StretchImage
            Else
                LoadDefaultImage()
            End If
            cbExemptATC.SelectedItem = GetATCDescription("PT", ATCCode)
            cbCurrency.SelectedItem = CurrencyCode & "-" & GetCurrencyDescription(CurrencyCode)
            disableEvent = False
            btnSave.Text = "Update"
        Else
            btnSave.Text = "Save"
        End If
    End Sub

    Private Function GetCurrencyDescription(Code As String) As String
        Dim query As String
        query = "SELECT Description FROM tblCurrency WHERE Code = @Code "
        SQl.FlushParams()
        SQl.AddParam("@Code", Code)
        SQl.ReadQuery(query)
        If SQl.SQLDR.Read Then
            Return SQl.SQLDR("Description").ToString
        Else
            Return ""
        End If
    End Function

    Private Sub LoadCurrencyList()
        cbCurrency.Items.Clear()
        Dim query As String
        query = " SELECT Code, Description FROM tblCurrency WHERE Status ='Active' "
        SQl.ReadQuery(query)
        If SQl.SQLDR.HasRows Then
            While SQl.SQLDR.Read
                cbCurrency.Items.Add(SQl.SQLDR("Code").ToString & "-" & SQl.SQLDR("Description").ToString)
            End While
            If cbCurrency.Items.Count >= 1 Then
                cbCurrency.SelectedIndex = 0
            End If
        Else
            frmCurrency.LoadDefaultItem()
            LoadCurrencyList()
        End If
    End Sub

    Private Sub LoadDefaultImage()
        Dim App_Path As String
        App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
        pbPicture.ImageLocation = App_Path & "\Images\DefaultLogo.jpg"
        pbPicture.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Function byteArrayToImage(ByVal ByteArrayin As Byte()) As Image
        Using msStream As New MemoryStream(ByteArrayin)
            Return Image.FromStream(msStream)
        End Using
    End Function

    Private Sub Cleartext()
        EmployerID = ""
        txtRegName.Clear()
        rbPrivate.Checked = True
        txtUnit.Clear()
        txtLotBlk.Clear()
        txtStreet.Clear()
        txtSubd.Clear()
        txtBrgy.Clear()
        txtCity.Clear()
        txtProvince.Clear()
        txtForeignAddress.Clear()
        txtForeignCountry.Clear()
        txtZipCode.Clear()
        txtTelephoneNo.Clear()
        txtCellphoneNo.Clear()
        txtFaxNo.Clear()
        txtEmailAdd.Clear()
        txtWebsite.Clear()
        txtTinNo.Clear()
        txtEmployerHead.Clear()

    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        btnUpload.Enabled = Value
        txtRegName.ReadOnly = Not Value
        rbPrivate.Enabled = Value
        rbGovt.Enabled = Value
        txtUnit.ReadOnly = Not Value
        txtLotBlk.ReadOnly = Not Value
        txtStreet.ReadOnly = Not Value
        txtSubd.ReadOnly = Not Value
        txtBrgy.ReadOnly = Not Value
        txtCity.ReadOnly = Not Value
        txtProvince.ReadOnly = Not Value
        txtForeignAddress.ReadOnly = Not Value
        txtForeignCountry.ReadOnly = Not Value
        txtZipCode.ReadOnly = Not Value
        txtTelephoneNo.ReadOnly = Not Value
        txtCellphoneNo.ReadOnly = Not Value
        txtFaxNo.ReadOnly = Not Value
        txtEmailAdd.ReadOnly = Not Value
        txtWebsite.ReadOnly = Not Value
        txtTinNo.ReadOnly = Not Value
        txtEmployerHead.ReadOnly = Not Value
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Try
            If isSetup = True Then
                btnSave.Visible = False
            End If
            If btnSave.Text = "Save" Then
                If txtRegName.Text = "" Then
                    MsgBox("Please enter registered employer name!", MsgBoxStyle.Exclamation)
                ElseIf MsgBox("Are you sure you want to save this information?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    SaveEmployer()
                    btnSave.Text = "Update"
                End If
            Else
                If txtRegName.Text = "" Then
                    MsgBox("Please enter registered employer name!", MsgBoxStyle.Exclamation)
                ElseIf MsgBox("Are you sure you want to update this record?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    UpdateEmployer()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SaveEmployer()
        Dim VatType, Classification As String
        If rbVAT.Checked Then
            VatType = "VATable"
        ElseIf rbExempt.Checked Then
            VatType = "Exempt"
        Else
            VatType = "ZeroRated"
        End If
        If rbInd.Checked Then
            Classification = "Individual"
        Else
            Classification = "Non-Individual"
        End If
        Dim ExemptATC As String = GetATC("PT", cbExemptATC.SelectedItem)
        Dim insertSQl As String
        insertSQl = " INSERT INTO " & _
                    " tblCompany  (Employer_Name, Employer_Type, Address_Unit, Address_Lot_Blk, Address_Street, Address_Subd, Address_Brgy, " & _
                    "              Address_Municipality, Address_Province, Foreign_Address, Foreign_Address_Country, Zip_Code, Contact_Telephone_No,  " & _
                    "              Contact_Cellphone_No, Contact_Fax, Contact_Email_Add, Contact_Website, Tin_No, Employer_Head, " & _
                    "              VAT_Reg, VAT_Type, VATExempt_ATC, Classification, Name_Last, Name_First, Name_Middle, Trade_Name, " & _
                    "              RDO_Code, Branch_Code, Report_Cycle, Fiscal_MonthEnd, Base_Currency, " & _
                    "              Who_Modified) " & _
                    " VALUES     (@Employer_Name, @Employer_Type, @Address_Unit, @Address_Lot_Blk, @Address_Street, @Address_Subd, @Address_Brgy, " & _
                    "             @Address_Municipality, @Address_Province, @Foreign_Address, @Foreign_Address_Country, @Zip_Code, @Contact_Telephone_No,  " & _
                    "             @Contact_Cellphone_No, @Contact_Fax, @Contact_Email_Add, @Contact_Website, @Tin_No, @Employer_Head, " & _
                    "             @VAT_Reg, @VAT_Type, @VATExempt_ATC, @Classification, @Name_Last, @Name_First, @Name_Middle, @Trade_Name, " & _
                    "             @RDO_Code, @Branch_Code, @Report_Cycle, @Fiscal_MonthEnd, Base_Currency, " & _
                    "             @Who_Modified)"
        SQl.FlushParams()
        SQl.AddParam("@Employer_Name", txtRegName.Text)
        SQl.AddParam("@Employer_Type", IIf(rbGovt.Checked = True, "Government", "Private"))
        SQl.AddParam("@Address_Unit", txtUnit.Text)
        SQl.AddParam("@Address_Lot_Blk", txtLotBlk.Text)
        SQl.AddParam("@Address_Street", txtStreet.Text)
        SQl.AddParam("@Address_Subd", txtSubd.Text)
        SQl.AddParam("@Address_Brgy", txtBrgy.Text)
        SQl.AddParam("@Address_Municipality", txtCity.Text)
        SQl.AddParam("@Address_Province", txtProvince.Text)
        SQl.AddParam("@Foreign_Address", txtForeignAddress.Text)
        SQl.AddParam("@Foreign_Address_Country", txtForeignCountry.Text)
        SQl.AddParam("@Zip_Code", txtZipCode.Text)
        SQl.AddParam("@Contact_Telephone_No", txtTelephoneNo.Text)
        SQl.AddParam("@Contact_Cellphone_No", txtCellphoneNo.Text)
        SQl.AddParam("@Contact_Fax", txtFaxNo.Text)
        SQl.AddParam("@Contact_Email_Add", txtEmailAdd.Text)
        SQl.AddParam("@Contact_Website", txtWebsite.Text)
        SQl.AddParam("@Tin_No", txtTinNo.Text)
        SQl.AddParam("@Employer_Head", txtEmployerHead.Text)
        SQl.AddParam("@VAT_Reg", rbVAT.Checked)
        SQl.AddParam("@VAT_Type", VatType)
        SQl.AddParam("@VATExempt_ATC", ExemptATC)
        SQl.AddParam("@Classification", Classification)
        SQl.AddParam("@Name_Last", txtLastName.Text)
        SQl.AddParam("@Name_First", txtFirstName.Text)
        SQl.AddParam("@Name_Middle", txtMiddleName.Text)
        SQl.AddParam("@Trade_Name", txtTradeName.Text)
        SQl.AddParam("@RDO_Code", txtRDO.Text)
        SQl.AddParam("@Branch_Code", txtBranchCode.Text)
        SQl.AddParam("@Report_Cycle", cbReportCycle.SelectedItem)
        SQl.AddParam("@Fiscal_MonthEnd", cbFiscalMonthEnd.SelectedText)
        Dim currency As String() = cbCurrency.SelectedItem.ToString.Split("-")
        SQl.AddParam("@Base_Currency", currency(0))
        BaseCurrency = currency(0)
        SQl.AddParam("@Who_Modified", "")
        If SQl.ExecNonQuery(insertSQl) = 1 Then
            MsgBox("Company Information Saved Successfully!", MsgBoxStyle.Information)
            btnSave.Visible = True
        End If
    End Sub

    Private Function GetATC(Type As String, Description As String) As String
        If Description Is Nothing Then
            Description = ""
        End If
        Dim query As String
        query = " SELECT Code FROM tblATC WHERE Type = @Type AND Description = @Description"
        SQl.FlushParams()
        SQl.AddParam("@Type", Type)
        SQl.AddParam("@Description", Description)
        SQl.ReadQuery(query)
        If SQl.SQLDR.Read Then
            Return SQl.SQLDR("Code")
        Else
            Return ""
        End If
    End Function
    Private Function GetATCDescription(Type As String, Code As String) As String
        If Code Is Nothing Then
            Code = ""
        End If
        Dim query As String
        query = " SELECT Description FROM tblATC WHERE Type = @Type AND Code = @Code"
        SQl.FlushParams()
        SQl.AddParam("@Type", Type)
        SQl.AddParam("@Code", Code)
        SQl.ReadQuery(query)
        If SQl.SQLDR.Read Then
            Return SQl.SQLDR("Description")
        Else
            Return ""
        End If
    End Function
    Private Sub UpdateLogo()
        Dim updateSQL As String
        updateSQL = " UPDATE tblCompany SET Company_Logo = @Company_Logo "
        SQl.FlushParams()
        Dim imgStreamPic As MemoryStream = New MemoryStream()
        If picPath <> "" AndAlso My.Computer.FileSystem.FileExists(picPath) Then
            Image.FromFile(picPath).Save(imgStreamPic, System.Drawing.Imaging.ImageFormat.Png)
        Else
            Dim imgPic As Image
            imgPic = pbPicture.Image
            imgPic.Save(imgStreamPic, System.Drawing.Imaging.ImageFormat.Png)
        End If
        imgStreamPic.Close()
        Dim byteArrayPic As Byte() = imgStreamPic.ToArray()
        SQl.AddParam("@Company_Logo", byteArrayPic, SqlDbType.Image)
        SQl.ExecNonQuery(updateSQL)

    End Sub

    Private Function isDuplicate(ByVal Name As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblCompany WHERE Employer_Name = '" & Name & "' AND Status='Active'"
        SQl.ReadQuery(query)
        If SQl.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function


    Private Sub UpdateEmployer()
        Dim VatType, Classification As String
        If rbVAT.Checked Then
            VatType = "VATable"
        ElseIf rbExempt.Checked Then
            VatType = "Exempt"
        Else
            VatType = "ZeroRated"
        End If
        If rbInd.Checked Then
            Classification = "Individual"
        Else
            Classification = "Non-Individual"
        End If
        Dim ExemptATC As String = GetATC("PT", cbExemptATC.SelectedItem)
        Dim updateSQL As String
        updateSQL = " UPDATE tblCompany" & _
                    " SET    Employer_Name = @Employer_Name, Employer_Type = @Employer_Type, " & _
                    "        Address_Unit = @Address_Unit, Address_Lot_Blk = @Address_Lot_Blk, Address_Street = @Address_Street, " & _
                    "        Address_Subd = @Address_Subd, Address_Brgy = @Address_Brgy, Address_Municipality = @Address_Municipality," & _
                    "        Address_Province = @Address_Province, Foreign_Address = @Foreign_Address, Foreign_Address_Country = @Foreign_Address_Country, " & _
                    "        Zip_Code = @Zip_Code, Contact_Telephone_No = @Contact_Telephone_No, Contact_Cellphone_No = @Contact_Cellphone_No,  " & _
                    "        Contact_Fax = @Contact_Fax, Contact_Email_Add = @Contact_Email_Add, Contact_Website = @Contact_Website, " & _
                    "        Tin_No = @Tin_No, Employer_Head = @Employer_Head, VAT_Reg = @VAT_Reg, VAT_Type = @VAT_Type, VATExempt_ATC = @VATExempt_ATC, " & _
                    "        Classification = @Classification, Name_Last = @Name_Last, Name_First = @Name_First, Name_Middle = @Name_Middle, " & _
                    "        Trade_Name = @Trade_Name, RDO_Code = @RDO_Code, Branch_Code = @Branch_Code, Report_Cycle = @Report_Cycle, Fiscal_MonthEnd = @Fiscal_MonthEnd, " & _
                    "        Base_Currency = @Base_Currency, Who_Modified = @Who_Modified "
        SQl.FlushParams()
        SQl.AddParam("@Employer_Name", txtRegName.Text)
        SQl.AddParam("@Employer_Type", IIf(rbGovt.Checked = True, "Government", "Private"))
        SQl.AddParam("@Address_Unit", txtUnit.Text)
        SQl.AddParam("@Address_Lot_Blk", txtLotBlk.Text)
        SQl.AddParam("@Address_Street", txtStreet.Text)
        SQl.AddParam("@Address_Subd", txtSubd.Text)
        SQl.AddParam("@Address_Brgy", txtBrgy.Text)
        SQl.AddParam("@Address_Municipality", txtCity.Text)
        SQl.AddParam("@Address_Province", txtProvince.Text)
        SQl.AddParam("@Foreign_Address", txtForeignAddress.Text)
        SQl.AddParam("@Foreign_Address_Country", txtForeignCountry.Text)
        SQl.AddParam("@Zip_Code", txtZipCode.Text)
        SQl.AddParam("@Contact_Telephone_No", txtTelephoneNo.Text)
        SQl.AddParam("@Contact_Cellphone_No", txtCellphoneNo.Text)
        SQl.AddParam("@Contact_Fax", txtFaxNo.Text)
        SQl.AddParam("@Contact_Email_Add", txtEmailAdd.Text)
        SQl.AddParam("@Contact_Website", txtWebsite.Text)
        SQl.AddParam("@Tin_No", txtTinNo.Text)
        SQl.AddParam("@Employer_Head", txtEmployerHead.Text)
        SQl.AddParam("@VAT_Reg", rbVAT.Checked)
        SQl.AddParam("@VAT_Type", VatType)
        SQl.AddParam("@VATExempt_ATC", ExemptATC)
        SQl.AddParam("@Classification", Classification)
        SQl.AddParam("@Name_Last", txtLastName.Text)
        SQl.AddParam("@Name_First", txtFirstName.Text)
        SQl.AddParam("@Name_Middle", txtMiddleName.Text)
        SQl.AddParam("@Trade_Name", txtTradeName.Text)
        SQl.AddParam("@RDO_Code", txtRDO.Text)
        SQl.AddParam("@Branch_Code", txtBranchCode.Text)
        SQl.AddParam("@Report_Cycle", cbReportCycle.SelectedItem)
        Dim currency As String() = cbCurrency.SelectedItem.ToString.Split("-")
        SQl.AddParam("@Base_Currency", currency(0))
        BaseCurrency = currency(0)
        SQl.AddParam("@Fiscal_MonthEnd", cbFiscalMonthEnd.SelectedText)
        SQl.AddParam("@Who_Modified", "")
        If SQl.ExecNonQuery(updateSQL) = 1 Then
            UpdateLogo()
            MsgBox("Employer Information Updated Successfully!", MsgBoxStyle.Information)
            btnSave.Visible = True
        End If
    End Sub

    Private Sub RemoveEmployer(ByVal Employer_ID As String)
        Dim updateSQL As String
        updateSQL = " UPDATE tblCompany" & _
                    " SET    Status ='Inactive' " & _
                    " WHERE  Employer_ID ='" & Employer_ID & " '"
        If SQl.ExecNonQuery(updateSQL) = 1 Then
            MsgBox("Employer Removed Successfully!", MsgBoxStyle.Information)
        End If
    End Sub


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles btnUpload.Click
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
                picPath = OpenFileDialog1.FileName
            End With
        End If
    End Sub

    Private Sub btnRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnRemove.Click
        Me.Close()
        Me.Dispose()
    End Sub


    Private Sub rbVAT_CheckedChanged(sender As Object, e As EventArgs) Handles rbVAT.CheckedChanged, rbExempt.CheckedChanged, rbZeroRated.CheckedChanged
        lblExemptATC.Visible = rbExempt.Checked
        cbExemptATC.Visible = rbExempt.Checked
    End Sub

    Private Sub rbInd_CheckedChanged(sender As Object, e As EventArgs) Handles rbInd.CheckedChanged, rbNonInd.CheckedChanged
        If rbInd.Checked Then
            txtFirstName.Enabled = True
            txtLastName.Enabled = True
            txtMiddleName.Enabled = True
            txtRegName.Enabled = False
        ElseIf rbNonInd.Checked Then
            txtRegName.Enabled = True
            txtFirstName.Enabled = False
            txtLastName.Enabled = False
            txtMiddleName.Enabled = False
        End If
    End Sub

    Private Sub cbReportCycle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbReportCycle.SelectedIndexChanged
        If cbReportCycle.SelectedItem = "Fiscal" Then
            cbFiscalMonthEnd.Enabled = True
        Else
            cbFiscalMonthEnd.Enabled = False
        End If
    End Sub

    Private Sub Panel2_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class