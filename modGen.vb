Imports System.IO
Imports System.Data.OleDb

Module modGen
    Public DBUser, DBPassword, DefaultServer As String
    Public UserID, UserName, Password, Name, UserLevel, database, Inv_ComputationMethod As String
    Public BusinessType, BranchCode As String
    Public SQL As SQLControl
    Public SQL_RUBY As SQLControl
    Public SQL_AKO As SQLControl
    Public Module_Import As Integer = 0
    Public App_Path As String = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
    Public activityStatus As Boolean
    Public AlphaCmd As OleDbCommand
    Public AlphaReader As OleDbDataReader
    Public AlphaCon As OleDbConnection
    Public SubscriptionID As Integer

    Public WorkFactor As Integer = 12
    Public isTrial As Boolean = False
    Public TrialExpired As Boolean = False
    Public SubsExpired As Boolean = False
    Public isRegistered As Boolean = False
    Public DaysLeft As Integer = 30
    Public BaseCurrency As String = ""
    Public Const CR_Factor As Decimal = 10.0
    Public Sub AlphaConnect()
        AlphaCon = New OleDbConnection With {.ConnectionString = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\Alpha.accdb;Jet OLEDB:Database Password=eVoSolution1;")}
    End Sub

    Public Sub AlphaQuery(ByVal query As String)
        AlphaConnect()
        If AlphaCon.State = ConnectionState.Open Then
            AlphaCon.Close()
        End If
        Using AlphaCmd = New OleDbCommand(query, AlphaCon)
            AlphaCon.Open()
            AlphaReader = AlphaCmd.ExecuteReader
        End Using
    End Sub

    Public Function AlphaExecute(ByVal query As String) As Integer
        Try
            AlphaConnect()
            If AlphaCon.State = ConnectionState.Open Then
                AlphaCon.Close()
            End If
            Using AlphaCmd = New OleDbCommand(query, AlphaCon)
                AlphaCon.Open()
                Return AlphaCmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            Msg(ex.Message, MsgBoxStyle.Exclamation)
            Return -1
        End Try
    End Function

    Public Sub SetDatabase()
        SQL = New SQLControl
        activityStatus = True
    End Sub

    Public Sub SetPayrollDatabase()
        SQL_RUBY = New SQLControl("RUBY")
        SQL_AKO = New SQLControl("RUBY_AKO")
        activityStatus = True
    End Sub

    Public Function byteArrayToImage(ByVal ByteArrayin As Byte()) As Image
        Using msStream As New MemoryStream(ByteArrayin)
            Return Image.FromStream(msStream)
        End Using
    End Function

    Public Function LoadDefaultImage(ByVal ImageType As String, Optional ByVal Gender As String = "Male") As String
        Dim App_Path As String
        App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
        If ImageType = "Picture" Then
            If Gender = "Male" Then
                Return App_Path & "\Images\Male.jpg"
            Else
                Return App_Path & "\Images\Female.png"
            End If
        Else
            Return App_Path & "\Images\Signature.png"
        End If
    End Function

    Public Function RequireField(ByVal obj As Object) As Boolean
        If TypeOf obj Is System.Windows.Forms.TextBox Then
            If CType(obj, System.Windows.Forms.TextBox).Text = "" Then
                CType(obj, System.Windows.Forms.TextBox).BackColor = Color.FromArgb(255, 255, 128)
                Return False
            Else
                CType(obj, System.Windows.Forms.TextBox).BackColor = SystemColors.Window
                Return True
            End If
        ElseIf TypeOf obj Is System.Windows.Forms.ComboBox Then
            If CType(obj, Windows.Forms.ComboBox).DropDownStyle = ComboBoxStyle.DropDown AndAlso CType(obj, Windows.Forms.ComboBox).Text = "" Then
                CType(obj, Windows.Forms.ComboBox).BackColor = Color.FromArgb(255, 255, 128)
                Return False
            Else
                CType(obj, Windows.Forms.ComboBox).BackColor = SystemColors.Window
                Return True
            End If
        Else
            Return True
        End If
    End Function

    Public Sub RecordActivity(ByVal UserID As String, ModuleID As String, formName As String, LogType As String, RefType As String, ByVal RefID As String, _
                              ByVal BusinessCode As String, ByVal BranchCode As String, ByVal Remarks As String, ByVal Status As Boolean, Optional ByVal UpdateCTR As Integer = 0)
        Dim insertSQL As String
        insertSQL = " INSERT INTO " & _
                    " tblAuditTrail(UserID, ModID, FormName, LogType, RefType, RefID, BusinessCode, BranchCode, Remarks, LogStatus) " & _
                    " VALUES (@UserID, @ModID, @FormName, @LogType, @RefType, @RefID, @BusinessCode, @BranchCode, @Remarks, @LogStatus) "
        SQL.FlushParams()
        SQL.AddParam("@UserID", IIf(IsNumeric(UserID), UserID, DBNull.Value))
        SQL.AddParam("@ModID", ModuleID)
        SQL.AddParam("@FormName", formName)
        SQL.AddParam("@LogType", LogType)
        SQL.AddParam("@RefType", RefType)
        SQL.AddParam("@RefID", RefID)
        SQL.AddParam("@BusinessCode", BusinessCode)
        SQL.AddParam("@BranchCode", BranchCode)
        SQL.AddParam("@Remarks", Remarks)
        SQL.AddParam("@LogStatus", Status)
        SQL.ExecNonQuery(insertSQL)
        SQL.FlushParams()
    End Sub

    Public Function LoadTransID(ByVal Table As String, Column As String, Optional FilterCol As String = "", Optional Filter As String = "") As String
        Dim query As String
        query = "SELECT MAX(" & Column & ") + 1 AS TransID FROM " & Table & " " & IIf(Filter = "", "", "WHERE " & FilterCol & " = '" & Filter & "' ")
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read AndAlso Not IsDBNull(SQL.SQLDR("TransID")) Then
            Return SQL.SQLDR("TransID")
        Else
            Return 1
        End If
    End Function

    Public Function LoadJE(ByVal Ref_Type As String, Ref_TransID As String) As String
        Dim query As String
        query = " SELECT JE_No FROM tblJE_Header WHERE RefType='" & Ref_Type & "' AND RefTransID ='" & Ref_TransID & "' AND isUpload = 0 "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read AndAlso Not IsDBNull(SQL.SQLDR("JE_No")) Then
            Return SQL.SQLDR("JE_No")
        Else
            Return 0
        End If
    End Function

    Public Function LoadDefaultAccount(ByVal Type As String) As String
        Dim query As String
        query = " SELECT Account_Code FROM tblCollection_PaymentType WHERE Payment_Type ='" & Type & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("Account_Code").ToString
        Else
            Return ""
        End If
    End Function

    Public Function LoadBankAccount(ByVal BankID As Integer) As String
        Dim query As String
        query = " SELECT AccountCode FROM tblBank_Master WHERE BankID ='" & BankID & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("AccountCode").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetVCEName(ByVal VCECode As String) As String
        Dim query As String
        query = " SELECT VCEName FROM viewVCE_Master WHERE VCECode ='" & VCECode & "'"
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("VCEName").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetCostCenter(ByVal Code As String) As String
        Dim query As String
        If Code = "All" Then
            query = " SELECT Code FROM tblCC WHERE Code LIKE '%%'"
        Else
            query = " SELECT Code FROM tblCC WHERE Code ='" & Code & "'"
        End If
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("Code").ToString
        Else
            Return ""
        End If
    End Function

    Public Function validateCostID(ByVal CostID As String) As Boolean
        Try
            Dim query As String
            query = " SELECT    * " &
                    " FROM      tblCC " &
                    " WHERE     Code = @Code "
            SQL.FlushParams()
            SQL.AddParam("@Code", CostID)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SQL.FlushParams()
        End Try

    End Function


    Public Function validateProfitID(ByVal PCID As String) As Boolean
        Try
            Dim query As String
            query = " SELECT    * " &
                    " FROM      tblPC " &
                    " WHERE     Code = @Code "
            SQL.FlushParams()
            SQL.AddParam("@Code", PCID)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SQL.FlushParams()
        End Try

    End Function

    Public Function validateCIP_Code(ByVal CIP_Code As String) As Boolean
        Try
            Dim query As String
            query = " SELECT    * " &
                    " FROM      tblCIP_Maintenance " &
                    " WHERE     CIP_Code = @Code "
            SQL.FlushParams()
            SQL.AddParam("@Code", CIP_Code)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SQL.FlushParams()
        End Try

    End Function

    Public Function GetVCECode(ByVal VCEName As String) As String
        Dim query As String
        query = " SELECT VCECode FROM viewVCE_Master WHERE VCEName ='" & VCEName & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("VCECode").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetCCCode(ByVal CCName As String) As String
        Dim query As String
        query = " SELECT Code FROM tblCC WHERE Description ='" & CCName & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("Code").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetCCName(ByVal CCCode As String) As String
        Dim query As String
        query = " SELECT Description FROM tblCC WHERE Code ='" & CCCode & "' "
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("Description").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetPCCode(ByVal CCName As String) As String
        Dim query As String
        query = " SELECT Code FROM tblPC WHERE Description ='" & CCName & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("Code").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetPCName(ByVal CCCode As String) As String
        Dim query As String
        query = " SELECT Description FROM tblPC WHERE Code ='" & CCCode & "' "
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("Description").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetItem_InvAccnt(ByVal ItemCode As String) As String
        Dim query As String
        query = " SELECT AD_Inv FROM tblItem_Master WHERE ItemCode ='" & ItemCode & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("AD_Inv").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetExpenseAccount(ByVal ItemCode As String) As String
        Dim query As String
        query = " SELECT AD_Expense FROM tblItem_Master WHERE ItemCode ='" & ItemCode & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("AD_Expense").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetItem_AD_Consignment(ByVal ItemCode As String) As String
        Dim query As String
        query = " SELECT AD_Consignment FROM tblItem_Master WHERE ItemCode ='" & ItemCode & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("AD_Consignment").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetDefaultAccnt(ByVal Type As String) As String
        Dim query As String
        query = " SELECT  AccntCode FROM tblDefaultAccount WHERE Type ='" & Type & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("AccntCode").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetAccntTitle(ByVal AccntCode As String) As String
        Dim query As String
        query = " SELECT AccountTitle FROM tblCOA_Master WHERE AccountCode ='" & AccntCode & "'"
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("AccountTitle").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetItemCode(ByVal ItemName As String) As String
        Dim query As String
        query = " SELECT ItemCode FROM tblItem_Master WHERE ItemName ='" & ItemName & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("ItemCode").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetCashAccountCode() As String
        Dim query As String
        query = " SELECT CASH_ACCOUNT, AccountTitle FROM tblSystemSetup " & _
               " INNER JOIN " & _
               " tblCOA_Master ON " & _
               " tblCOA_Master.AccountCode = tblSystemSetup.CASH_ACCOUNT "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("CASH_ACCOUNT").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetItemName(ByVal ItemCode As String) As String
        Dim query As String
        query = " SELECT ItemName FROM tblItem_Master WHERE ItemCode ='" & ItemCode & "'"
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("ItemName").ToString
        Else
            Return ""
        End If
    End Function


    Public Function GetItemType(ByVal ItemCode As String) As String
        Dim query As String
        query = " SELECT ItemType FROM tblItem_Master WHERE ItemCode ='" & ItemCode & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("ItemType").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetRecord(ByVal TransID As String, type As String, table As String) As Integer
        Dim query As String = ""
        If type = "Prev" Then
            If TransID = "" Then
                query = " SELECT MIN(TransID) AS TransID FROM " & table
            Else
                query = " SELECT MAX(TransID) AS TransID FROM " & table & " WHERE TransID < '" & TransID & "'"
            End If
        ElseIf type = "Next" Then
            If TransID = "" Then
                query = " SELECT MAX(TransID) AS TransID FROM " & table
            Else
                query = " SELECT MIN(TransID) AS TransID FROM " & table & " WHERE TransID > '" & TransID & "'"
            End If
        End If
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() AndAlso Not IsDBNull(SQL.SQLDR("TransID")) Then
            Return SQL.SQLDR("TransID")
        Else
            Return 0
        End If
    End Function


    Public Function GetRefNo(ByVal strVCECode As String, strAccntCode As String)
        Dim strRefNo As String = ""
        Dim selectSQL As String = ""
        selectSQL = " SELECT TOP 1 REPLACE(REPLACE(REPLACE(RefNo, 'SD:', ''), 'TD:', ''), 'LN:', '') AS RefNo FROM View_GL WHERE AccntCode = '" & strAccntCode & "' AND VCECode = '" & strVCECode & "' AND RefNo <> '' AND AccntCode IN (SELECT AccountCode FROM tblCOA_Master WHERE AccountType = 'Balance Sheet') " & vbCrLf & _
                    " ORDER BY AppDate DESC "
        SQL.ReadQuery(selectSQL)
        While SQL.SQLDR.Read
            strRefNo = SQL.SQLDR("RefNo").ToString
        End While
        Return strRefNo
    End Function

    Public Function GetRefNoVCECode(ByVal strRefNo As String)
        Dim strVCECode As String = ""
        Dim selectSQL As String = " SELECT TOP 1 VCECode FROM View_GL WHERE RefNo = '" & strRefNo & "' AND AccntCode IN (SELECT AccountCode FROM tblCOA_Master WHERE AccountType = 'Balance Sheet') " & vbCrLf & _
                                  " ORDER BY AppDate DESC "
        SQL.ReadQuery(selectSQL)
        While SQL.SQLDR.Read
            strVCECode = SQL.SQLDR("VCECode").ToString
        End While
        Return strVCECode
    End Function

    Public Function GetRefNoAccntCode(ByVal strRefNo As String)
        Dim strAccntCode As String = ""
        Dim selectSQL As String = " SELECT TOP 1 AccntCode FROM View_GL WHERE RefNo = '" & strRefNo & "' AND AccntCode IN (SELECT AccountCode FROM tblCOA_Master WHERE AccountType = 'Balance Sheet') " & vbCrLf & _
                                  " ORDER BY AppDate DESC "
        SQL.ReadQuery(selectSQL)
        While SQL.SQLDR.Read
            strAccntCode = SQL.SQLDR("AccntCode").ToString
        End While
        Return strAccntCode
    End Function

    Public Function GetRefNoLoan(ByVal strRefNo As String)
        Dim strAccntCode As String = ""
        If strRefNo <> "" Then
            Dim selectSQL As String = " SELECT IntIncomeAccount FROM tblLoan_Type WHERE LoanCode IN (SELECT LoanCode FROM tblLoan WHERE Loan_No = '" & strRefNo & "') "
            SQL.ReadQuery(selectSQL)
            While SQL.SQLDR.Read
                strAccntCode = SQL.SQLDR("IntIncomeAccount").ToString
            End While
        End If
        Return strAccntCode
    End Function

    Public Sub SaveEntry(ByVal JE As Integer, AccntCode As Integer, VCECode As Integer, Debit As Decimal, Credit As Decimal, Particulars As String, Ref_No As String, Line As Integer)
        Dim insertSQL As String
        insertSQL = " INSERT INTO " & _
                    " tblJE_Details(JE_No, AccntCode, VCECode, Debit, Credit, Particulars, Ref_No, Line_Number) " & _
                    " VALUES (@JE_No, @AccntCode, @VCECode, @Debit, @Credit, @Particulars, @Ref_No, @Line_Number) "
        SQL.FlushParams()
        SQL.AddParam("JE_No", JE)
        SQL.AddParam("AccntCode", AccntCode)
        SQL.AddParam("VCECode", VCECode)
        SQL.AddParam("Debit", Debit)
        SQL.AddParam("Credit", Credit)
        SQL.AddParam("Particulars", Particulars)
        SQL.AddParam("Ref_No", Ref_No)
        SQL.AddParam("Line_Number", Line)
        SQL.ExecNonQuery(insertSQL)
    End Sub

    Public Function LoadDefaultAccnt(ByVal Category As String, Type As String) As String
        Dim query As String
        query = " SELECT AccntCode FROM tblDefaultAccount WHERE Category ='" & Category & "' AND Type ='" & Type & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("AccntCode")
        Else
            Return ""
        End If

    End Function

    Public Sub msgRequired()
        MsgBox("Please fill-up the required fields", vbInformation, "GR8 Message Alert")
    End Sub

    Public Sub SaveError(ErrorMessage As String, Stacktrace As String, Winform As String, ModID As String)
        Dim insertSQL As String
        insertSQL = " INSERT INTO " & _
                    " tblError(ErrorMessage, ErrorStacktrace, WinForm, ModuleID) " & _
                    " VALUES (@ErrorMessage, @ErrorStacktrace, @WinForm, @ModuleID) "
        SQL.FlushParams()
        SQL.AddParam("@ErrorMessage", ErrorMessage)
        SQL.AddParam("@ErrorStacktrace", Stacktrace)
        SQL.AddParam("@WinForm", Winform)
        SQL.AddParam("@ModuleID", ModID)
        SQL.ExecNonQuery(insertSQL)
        Msg(ErrorMessage, MsgBoxStyle.Exclamation)
    End Sub

    Public Sub Msg(ByVal Message As String, style As MsgBoxStyle)
        MsgBox(Message, style, "GR8 Message Alert")
    End Sub

    Public Function IfAccntCodeUsed(ByVal AccountCode As String) As Boolean
        Dim query As String
        query = " SELECT AccntCode FROM viewAccountCode_Used WHERE AccntCode ='" & AccountCode & "'  "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetTransSetup(ByVal Type As String) As Boolean
        Dim query As String
        query = " SELECT AutoSeries FROM tblTransactionSetup WHERE TransType ='" & Type & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("AutoSeries")
        Else
            Return False
        End If
    End Function



    Public Function GetTransApproval(ByVal Type As String) As Boolean
        Dim query As String
        query = " SELECT ISNULL(isForApproval,0) AS isForApproval FROM tblTransactionSetup WHERE TransType ='" & Type & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("isForApproval")
        Else
            Return False
        End If
    End Function

    Public Function GetTransisReversal(ByVal Type As String) As Boolean
        Dim query As String
        query = " SELECT ISNULL(ForReversal,0) AS ForReversal FROM tblTransactionSetup WHERE TransType ='" & Type & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("ForReversal")
        Else
            Return False
        End If
    End Function

    Public Function GenerateTransNum(ByRef Auto As Boolean, ModuleID As String, ColumnPK As String, Table As String) As String
        Dim TransNum As String = ""
        If Auto = True Then
            ' GENERATE TRANS ID 
            Dim query As String
            Do
                query = " SELECT	GlobalSeries, ISNULL(BranchCode,'All') AS BranchCode, ISNULL(BusinessCode,'All') AS BusinessCode,  " & _
                                    " 		    ISNULL(TransGroup,0) AS TransGroup, ISNULL(Prefix,'') AS Prefix, ISNULL(Digits,6) AS Digits, " & _
                                    "           ISNULL(StartRecord,1) AS StartRecord, LEN(ISNULL(Prefix,'')) + ISNULL(Digits,6) AS ID_Length " & _
                                    " FROM	    tblTransactionSetup LEFT JOIN tblTransactionDetails " & _
                                    " ON		tblTransactionSetup.TransType = tblTransactionDetails.TransType " & _
                                    " WHERE	    tblTransactionSetup.TransType ='" & ModuleID & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    If SQL.SQLDR("GlobalSeries") = True Then
                        If SQL.SQLDR("BranchCode") = "All" AndAlso SQL.SQLDR("BusinessCode") = "All" Then
                            Dim digits As Integer = SQL.SQLDR("Digits")
                            Dim prefix As String = SQL.SQLDR("Prefix")
                            Dim startrecord As Integer = SQL.SQLDR("StartRecord")
                            query = " SELECT    ISNULL(MAX(SUBSTRING(" & ColumnPK & "," & prefix.Length + 1 & "," & digits & "))+ 1," & startrecord & ") AS TransID  " & _
                                    " FROM      " & Table & "  " & _
                                    " WHERE     " & ColumnPK & " LIKE '" & prefix & "%' AND LEN(" & ColumnPK & ") = '" & digits & "'  AND TransAuto = 1 "
                            SQL.ReadQuery(query)
                            If SQL.SQLDR.Read Then
                                TransNum = SQL.SQLDR("TransID")
                                For i As Integer = 1 To digits
                                    TransNum = "0" & TransNum
                                Next
                                TransNum = prefix & Strings.Right(TransNum, digits)

                                ' CHECK IF GENERATED TRANSNUM ALREADY EXIST
                                query = " SELECT    " & ColumnPK & " AS TransID  " & _
                                        " FROM      " & Table & "  " & _
                                        " WHERE     " & ColumnPK & " = '" & TransNum & "' "
                                SQL.ReadQuery(query)
                                If SQL.SQLDR.Read Then
                                    Dim updateSQL As String
                                    updateSQL = " UPDATE  " & Table & "  SET TransAuto = 1 WHERE " & ColumnPK & " = '" & TransNum & "' "
                                    SQL.ExecNonQuery(updateSQL)
                                    TransNum = "-1"
                                End If
                            End If
                        End If
                    Else
                        Dim digits As Integer = SQL.SQLDR("Digits")
                        Dim prefix As String = SQL.SQLDR("Prefix")
                        Dim startrecord As Integer = SQL.SQLDR("StartRecord")
                        query = " SELECT    ISNULL(MAX(SUBSTRING(" & ColumnPK & "," & prefix.Length + 1 & "," & digits & "))+ 1,1) AS TransID  " & _
                                " FROM      " & Table & "  " & _
                                " WHERE     " & ColumnPK & " LIKE '" & prefix & "%'   AND TransAuto = 1 AND BranchCode = '" & BranchCode & "'"
                        SQL.ReadQuery(query)
                        If SQL.SQLDR.Read Then
                            TransNum = SQL.SQLDR("TransID")
                            For i As Integer = 1 To digits
                                TransNum = "0" & TransNum
                            Next
                            TransNum = prefix & Strings.Right(TransNum, digits)

                            ' CHECK IF GENERATED TRANSNUM ALREADY EXIST
                            query = " SELECT    " & ColumnPK & " AS TransID  " & _
                                    " FROM      " & Table & "  " & _
                                    " WHERE     " & ColumnPK & " = '" & TransNum & "' AND BranchCode = '" & BranchCode & "'"
                            SQL.ReadQuery(query)
                            If SQL.SQLDR.Read Then
                                Dim updateSQL As String
                                updateSQL = " UPDATE  " & Table & "  SET TransAuto = 1 WHERE " & ColumnPK & " = '" & TransNum & "' "
                                SQL.ExecNonQuery(updateSQL)
                                TransNum = "-1"
                            End If
                        End If
                    End If
                End If
                If TransNum <> "-1" Then Exit Do
            Loop

            Return TransNum
        Else
            Return ""
        End If
    End Function


    Public Function GenerateTransID(ColID As String, Table As String) As String
        Dim TransID As String = ""
        ' GENERATE TRANS ID 
        Dim query As String
        query = " SELECT    ISNULL(MAX(" & ColID & ")+ 1,1) AS TransID  " & _
                " FROM      " & Table & "  "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            TransID = SQL.SQLDR("TransID")
        Else
            TransID = 0
        End If
        Return TransID
    End Function

    Public Function GetTransID(ColumnPK As String, Table As String, ColumnTrans As String, TransNo As String, Optional ByVal ColumnType As String = "", Optional ByVal Type As String = "") As String
        Dim TransID As String = ""
        ' GENERATE TRANS ID 
        Dim query As String
        query = " SELECT     TransID  " & _
                " FROM      " & Table & " WHERE  " & ColumnTrans & " = '" & TransNo & "' " & IIf(ColumnType = "", "", " AND " & ColumnType & " = '" & Type & "'")
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            TransID = SQL.SQLDR("TransID")
        Else
            TransID = 0
        End If
        Return TransID
    End Function

    Public Function AllowAccess(ByVal FunctionID As String, Optional ByVal Type As String = "Module Access") As Boolean
        Dim query As String
        query = " SELECT * FROM tblUser_Access " & _
                " WHERE Status ='Active' AND  UserID ='" & UserID & "' AND Code ='" & FunctionID & "' AND Type ='" & Type & "' AND isAllowed ='1' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetUserName(ByVal UserID As String)
        Dim query As String
        query = " SELECT Username FROM tblUser " & _
                " WHERE Status ='Active' AND  UserID ='" & UserID & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("Username").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetPC(ByVal PC As String) As String
        Dim query As String
        query = " SELECT ProfitCenter FROM tblVCE_Master WHERE VCECode = @VCECode "
        SQL.FlushParams()
        SQL.AddParam("@VCECode", PC)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("ProfitCenter").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetCC_RE(ByVal RE_TransID As String) As String
        Dim query As String
        query = " 		SELECT TransID, RE_No, PropCode, Project AS CostCenter FROM tblRE  " &
                " 		LEFT JOIN tblSaleProperty " &
                " 		ON tblRE.PropCode = tblSaleProperty.UnitCode " &
                " 		WHERE TransID = @TransID "
        SQL.FlushParams()
        SQL.AddParam("@TransID", RE_TransID)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("CostCenter").ToString
        Else
            Return ""
        End If
    End Function
    Public Function GetCC(ByVal CC As String) As String
        Dim query As String
        query = " SELECT CostCenter FROM tblVCE_Master WHERE VCECode = @VCECode "
        SQL.FlushParams()
        SQL.AddParam("@VCECode", CC)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("CostCenter").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetAccntCode(ByVal AccntTitle As String) As String
        Dim query As String
        query = " SELECT AccountCode FROM tblCOA_Master WHERE AccountTitle = @AccountTitle AND AccountGroup = 'SubAccount' "
        SQL.FlushParams()
        SQL.AddParam("@AccountTitle", AccntTitle)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("AccountCode").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetAccntCode_MainAccount(ByVal AccntTitle As String) As String
        Dim query As String
        query = " SELECT AccountCode FROM tblCOA_Master WHERE AccountTitle = @AccountTitle  "
        SQL.FlushParams()
        SQL.AddParam("@AccountTitle", AccntTitle)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("AccountCode").ToString
        Else
            Return ""
        End If
    End Function

    Public Sub SaveINVTY(Type As String, ModuleID As String, RefType As String, RefID As String, PostDate As Date, ItemCode As String, WHSE As String, QTY As Decimal, _
                          UOM As String, UnitCost As Decimal, Optional ByVal Status As String = "Active", Optional ByVal SerialNo As String = "", _
                          Optional ByVal LotNo As String = "", Optional ByVal DateExpired As String = "", Optional ByVal Size As String = "", Optional ByVal Color As String = "")
        Try
            Dim TransQTY As Decimal = QTY * ConvertUOM(UOM, ItemCode)
            Dim TransValue As Decimal = QTY * UnitCost
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblInventory(MovementType, RefType, RefID, PostDate, ItemCode, WHSE, QTY, UOM, UnitCost, TransQTY, TransValue, Status,   " & _
                        " SerialNo, LotNo, DateExpired, Size, Color)  " & _
                        " VALUES(@MovementType, @RefType, @RefID, @PostDate, @ItemCode, @WHSE, @QTY, @UOM, @UnitCost, @TransQTY, @TransValue, @Status, " & _
                        " @SerialNo, @LotNo, @DateExpired, @Size, @Color) "
            SQL.FlushParams()
            SQL.AddParam("@MovementType", Type)
            SQL.AddParam("@RefType", RefType)
            SQL.AddParam("@RefID", RefID)
            SQL.AddParam("@PostDate", PostDate)
            SQL.AddParam("@ItemCode", ItemCode)
            SQL.AddParam("@WHSE", WHSE)
            SQL.AddParam("@QTY", QTY)
            SQL.AddParam("@UOM", UOM)
            SQL.AddParam("@UnitCost", UnitCost)
            SQL.AddParam("@TransQTY", TransQTY)
            SQL.AddParam("@TransValue", TransValue)
            SQL.AddParam("@SerialNo", SerialNo)
            SQL.AddParam("@LotNo", LotNo)
            SQL.AddParam("@DateExpired", DateExpired)
            SQL.AddParam("@Size", Size)
            SQL.AddParam("@Color", Color)
            SQL.AddParam("@Status", Status)
            SQL.ExecNonQuery(insertSQL)
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, "modGen", ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, "modGen", "SAVE", RefType, RefID, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Public Sub UpdateINVTY(Type As String, ModuleID As String, RefType As String, RefID As String, PostDate As Date, ItemCode As String, WHSE As String, QTY As Decimal, _
                          UOM As String, UnitCost As Decimal)
        Try
            If Type = "OUT" Then
                QTY = QTY * -1
            End If
            Dim TransQTY As Decimal = QTY * ConvertUOM(UOM, ItemCode)
            Dim TransValue As Decimal = QTY * UnitCost
            Dim updateSQL As String
            updateSQL = " UPDATE  tblInventory " & _
                        " SET     MovementType = @MovementType ,PostDate = @PostDate, ItemCode = @ItemCode, WHSE = @WHSE, " & _
                        "         QTY = @QTY, UOM = @UOM, UnitCost = @UnitCost, TransQTY = @TransQTY, TransValue = @TransValue  " & _
                        " WHERE   RefType = @RefType AND RefID = @RefID "
            SQL.FlushParams()
            SQL.AddParam("@MovementType", Type)
            SQL.AddParam("@RefType", RefType)
            SQL.AddParam("@RefID", RefID)
            SQL.AddParam("@PostDate", PostDate)
            SQL.AddParam("@ItemCode", ItemCode)
            SQL.AddParam("@WHSE", WHSE)
            SQL.AddParam("@QTY", QTY)
            SQL.AddParam("@UOM", UOM)
            SQL.AddParam("@UnitCost", UnitCost)
            SQL.AddParam("@TransQTY", TransQTY)
            SQL.AddParam("@TransValue", TransValue)
            SQL.ExecNonQuery(updateSQL)
            ComputeWAUC(RefType, RefID)
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, "modGen", ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, "modGen", "UPDATE", RefType, RefID, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Public Sub DELINVTY(ModuleID As String, RefType As String, RefID As String)
        Try

            Dim updateSQL As String
            updateSQL = " DELETE tblInventory " & _
                        " WHERE   RefType = @RefType AND RefID = @RefID "
            SQL.FlushParams()
            SQL.AddParam("@RefType", RefType)
            SQL.AddParam("@RefID", RefID)
            SQL.ExecNonQuery(updateSQL)
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, "modGen", ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, "modGen", "DEL", RefType, RefID, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Public Function GetWHSECode(ByVal Description As String, Optional Table As String = "tblWarehouse") As String
        If Description = Nothing Then
            Description = ""
        End If
        Try
            Dim query As String
            query = " SELECT Code FROM " & Table & " WHERE Description = @Description "

            SQL.FlushParams()
            SQL.AddParam("@Description", Description)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Return SQL.SQLDR("Code").ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, "modGen", "modGen")
            Return ""
        Finally
            SQL.FlushParams()
        End Try
    End Function



    Public Function GetWHSEDesc(ByVal Code As String, Optional Table As String = "tblWarehouse") As String
        Try
            Dim query As String
            query = " SELECT Description FROM " & Table & " WHERE Code = @Code "
            SQL.FlushParams()
            SQL.AddParam("@Code", Code)
            SQL.ReadQuery(query, 2)
            If SQL.SQLDR2.Read Then
                Return SQL.SQLDR2("Description").ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, "modGen", "modGen")
            Return ""
        Finally
            SQL.FlushParams()
        End Try

    End Function

    Public Function GetWHSE(ByVal Code As String, Table As String) As String
        Try
            Dim query As String
            query = " SELECT Code + ' | ' + Description AS Description FROM " & Table & " WHERE Code = @Code "
            SQL.FlushParams()
            SQL.AddParam("@Code", Code)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Return SQL.SQLDR("Description").ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, "modGen", "modGen")
            Return ""
        Finally
            SQL.FlushParams()
        End Try

    End Function

    Public Function GetWHSE(WHSEDesc As String) As String
        Dim WHSE As String
        If WHSEDesc.Contains("|") Then
            WHSE = Strings.Left(WHSEDesc, WHSEDesc.ToString.IndexOf(" | "))
        Else
            WHSE = WHSEDesc
        End If
        Return WHSE
    End Function

    Public Function GetBOM(ByVal Code As String, Table As String) As String
        Try
            Dim query As String
            query = " SELECT BOM_Code + ' | ' + Remarks AS Description FROM " & Table & " WHERE BOM_Code = @Code "
            SQL.FlushParams()
            SQL.AddParam("@Code", Code)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Return SQL.SQLDR("Description").ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, "modGen", "modGen")
            Return ""
        Finally
            SQL.FlushParams()
        End Try

    End Function

    Public Function GetBOM(BOMDesc As String) As String
        Dim BOM As String
        If BOMDesc.Contains("|") Then
            BOM = Strings.Left(BOMDesc, BOMDesc.ToString.IndexOf(" | "))
        Else
            BOM = BOMDesc
        End If
        Return BOM
    End Function

    Public Function ConvertUOM(ByVal UOM As String, ItemCode As String) As Decimal
        Dim query As String
        query = " SELECT QTY FROM viewItem_UOM WHERE GroupCode ='" & ItemCode & "' AND UnitCode ='" & UOM & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("QTY")
        Else
            Return 1
        End If
    End Function

    Public Sub ComputeWAUC(Type As String, TransID As Integer)
        Try
            ' GET THE LAST COM QTY OF AN ITEM 
            Dim updateSQL As String
            updateSQL = " UPDATE tblInventory " & _
                        " SET		ComQTY = A.ComQTY, ComValue = A.ComValue, AverageCost =  (CASE WHEN A.ComQTY = 0 THEN 0 ELSE A.ComValue/A.ComQTY END) " & _
                        " FROM " & _
                        " ( " & _
                        " 	SELECT	Selected.RecordID, Selected.RefType, Selected.RefID, Selected.ItemCode,  " & _
                        " 			CASE WHEN ComQTY IS NULL THEN Selected.TransQTY ELSE ComQTY + Selected.TransQTY END AS ComQTY, " & _
                        " 			CASE WHEN ComValue IS NULL THEN Selected.TransValue ELSE ComValue + Selected.TransValue END AS ComValue, " & _
                        " 			tblInventory.DateCreated, tblInventory.PostDate,  " & _
                        " 			ROW_NUMBER() OVER (PARTITION BY Selected.ItemCode ORDER BY tblInventory.Postdate DESC, tblInventory.DateCreated DESC) AS RowNum " & _
                        " 	FROM	 " & _
                        " 	( " & _
                        " 		SELECT	RecordID, MoveMentType, RefType, RefID, PostDate, DateCreated, ItemCode, " & _
                        "               CASE WHEN MovementType = 'IN' THEN TransQTY ELSE TransQTY * -1 END AS TransQTY, " & _
                        "               CASE WHEN MovementType = 'IN' THEN TransValue ELSE TransValue * -1 END AS TransValue " & _
                        " 		FROM	tblInventory  " & _
                        " 		WHERE	RefType ='" & Type & "' AND RefID = " & TransID & " " & _
                        " 	) AS Selected " & _
                        " 	LEFT JOIN tblInventory " & _
                        " 	ON		tblInventory.ItemCode = Selected.ItemCode " & _
                        " 	AND     (tblInventory.PostDate < Selected.PostDate " & _
                        " 	OR     (tblInventory.PostDate = Selected.PostDate AND  tblInventory.DateCreated < Selected.DateCreated)) " & _
                        " ) AS A " & _
                        " WHERE tblInventory.RecordID = A.RecordID " & _
                        "  AND RowNum = 1 "
            SQL.ExecNonQuery(updateSQL)

            updateSQL = " UPDATE  tblInventory " & _
                        " SET     tblInventory.ComQTY = A.ComQTY, " & _
                        " 		  tblInventory.ComValue = A.ComValue, " & _
                        "         AverageCost = CASE WHEN A.ComQTY = 0 THEN 0 ELSE A.ComValue/A.ComQTY  END " & _
                        " FROM " & _
                        " ( " & _
                        " 	SELECT	tblInventory.RecordID, " & _
                        " 			Selected.ComQTY  + SUM(CASE WHEN MovementType = 'IN' THEN TransQTY ELSE TransQTY * -1 END) OVER (PARTITION BY Selected.ItemCode ORDER BY tblInventory.Postdate ASC, tblInventory.DateCreated ASC, tblInventory.RecordID) AS ComQTY, " & _
                        " 			Selected.ComValue + SUM(CASE WHEN MovementType = 'IN' THEN TransValue ELSE TransValue * -1 END) OVER (PARTITION BY Selected.ItemCode ORDER BY tblInventory.Postdate ASC, tblInventory.DateCreated ASC, tblInventory.RecordID) AS ComValue " & _
                        " 		FROM	 " & _
                        " 		( " & _
                        " 			SELECT	RecordID, RefType, RefID, PostDate, DateCreated, ItemCode, ComQTY, ComValue " & _
                        " 			FROM	tblInventory  " & _
                        " 			WHERE	RefType ='" & Type & "' AND RefID = " & TransID & "   " & _
                        " 		) AS Selected " & _
                        " 	INNER JOIN tblInventory " & _
                        " 	ON		tblInventory.ItemCode = Selected.ItemCode " & _
                        " 	AND     (tblInventory.PostDate > Selected.PostDate " & _
                        " 	OR      (tblInventory.PostDate = Selected.PostDate AND  tblInventory.DateCreated > Selected.DateCreated)) " & _
                        " ) AS A " & _
                        " WHERE  tblInventory.RecordID = A.RecordID "
            SQL.ExecNonQuery(updateSQL)
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, "modGen", "modGen")
        End Try
    End Sub

    Public Function IfFixedAssetItem(ByVal ItemCode As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblItem_Master WHERE ItemCode ='" & ItemCode & "' AND isFixAsset = 1 "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function IfInventoriableItem(ByVal ItemCode As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblItem_Master WHERE ItemCode ='" & ItemCode & "' AND isInventory = 1 "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetStandardCost(ByVal ItemCode As String) As Decimal
        Dim query As String
        query = " SELECT    ISNULL(ID_SC,0) AS ID_SC " & _
                " FROM      tblItem_Master " & _
                " WHERE     ItemCode ='" & ItemCode & "'  "
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("ID_SC")
        Else
            Return 0
        End If
    End Function

    Public Function GetAverageCost(ByVal ItemCode As String) As Decimal
        Dim query As String
        query = " SELECT    TOP 1 AverageCost " & _
                " FROM      tblInventory " & _
                " WHERE     ItemCode ='" & ItemCode & "'  AND ISNULL(STATUS,'Active') = 'Active' " & _
                " ORDER BY  PostDate DESC, DateCreated DESC "
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("AverageCost")
        Else
            Return 0
        End If
    End Function

    Public Sub InitializeDB()
        Dim deleteSQL As String
        deleteSQL = " DELETE FROM tblSI "
        SQL.ExecNonQuery(deleteSQL)
        deleteSQL = " DELETE FROM tblJE_Header "
        SQL.ExecNonQuery(deleteSQL)
        deleteSQL = " DELETE FROM tblUploadHistory "
        SQL.ExecNonQuery(deleteSQL)
        MsgBox("Database initialized successfully!", MsgBoxStyle.Information)
    End Sub

    Public Sub msgsave()
        'Required Fields
        Msg("Data has been Successfully saved", MsgBoxStyle.Information)
    End Sub

    Public Sub msgupdated()
        Msg("Data has been successfully updated", MsgBoxStyle.Information)
    End Sub

    Public Function GetRecordID(ByVal Table As String, Column As String) As Integer
        Dim query As String
        query = "SELECT ISNULL(MAX(" & Column & "),0) + 1 AS RecordID FROm " & Table & ""
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("RecordID")
        Else
            Return 0
        End If
    End Function

    Public Function GetUOMdescription(ByVal UoMCode As String)
        Dim query As String
        query = " SELECT * FROM tblUOM WHERE UnitCode = @UnitCode "
        SQL.FlushParams()
        SQL.AddParam("@UnitCode", UoMCode)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("Description").ToString
        Else
            Return ""
        End If
        SQL.FlushParams()
    End Function

    Public Sub msgRestricted()
        Msg("You have insufficient privileges to access this function!", MsgBoxStyle.Information)
    End Sub

    Public Sub msgRestrictedMod()
        Msg("You have insufficient privileges to access this Module!", MsgBoxStyle.Information)
    End Sub

    Public Function BranchExist(ByVal Code As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblBranch WHERE BranchCode = @BranchCode "
        SQL.FlushParams()
        SQL.AddParam("@BranchCode", Code)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function BusinessTypeExist(ByVal Code As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblBusinessType WHERE BusinessCode = @BusinessCode "
        SQL.FlushParams()
        SQL.AddParam("@BusinessCode", Code)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Sub SaveGroup(Type As String, GroupID As String, txtGroup As TextBox)
        Dim insertSQL As String
        insertSQL = " INSERT INTO " & _
                       " tblGroup(Type, GroupID, Description, Status, WhoCreated) " & _
                       " VALUES(@Type, @GroupID, @Description, @Status, @WhoCreated) "
        SQL.FlushParams()
        SQL.AddParam("@Type", Type)
        SQL.AddParam("@GroupID", GroupID)
        SQL.AddParam("@Description", txtGroup.Text)
        If txtGroup.Text = "" Then
            SQL.AddParam("@Status", "Inactive")
        Else
            SQL.AddParam("@Status", "Active")
        End If
        SQL.AddParam("@WhoCreated", UserID)
        SQL.ExecNonQuery(insertSQL)
    End Sub

    Public Function GetLast_InventoryOUT(RefTransID As String, RefType As String) As Boolean
        Dim query As String
        query = " SELECT * FROM viewDateMax_Inventory WHERE RefID ='" & RefTransID & "' AND RefType ='" & RefType & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetMaxInventoryDate() As Date
        Dim query As String
        query = " SELECT ISNULL(MAX(PostDate),'01-01-1900') AS PostDate FROM viewDateMax_Inventory "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read AndAlso Not IsDBNull(SQL.SQLDR("PostDate")) Then
            Return SQL.SQLDR("PostDate")
        Else
            Return "01-01-1900"
        End If
    End Function

    Public Function GetMaxPEC() As Date
        Dim query As String
        query = " SELECT ISNULL(MAX(datePEC),'01-01-1900') AS datePEC FROM tblPEC WHERE Status ='Active' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read AndAlso Not IsDBNull(SQL.SQLDR("datePEC")) Then
            Return SQL.SQLDR("datePEC")
        Else
            Return "01-01-1900"
        End If
    End Function


    Public Function GetUpdateCtr(ByVal Table As String, Column As String, ByVal TransNo As String) As Integer
        Dim query As String
        query = "SELECT ISNULL(MAX(UpdateCTR),0) + 1 AS UpdateCtr FROM " & Table & " WHERE " & Column & " = '" & TransNo & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("UpdateCtr")
        Else
            Return 0
        End If
    End Function

    Public Function GetDate() As DateTime
        Dim query As String
        query = " SELECT GETDATE() AS getDate "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return SQL.SQLDR("getDate")
        Else
            Return Nothing
        End If
    End Function

    Public Function isInitialized() As Boolean
        SetDatabase()
        Dim query As String
        query = " SELECT  SubscriptionID, IsInit FROM tblLicenseInfo "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Select Case SQL.SQLDR("IsInit")
                Case 1
                    isTrial = True
                    isRegistered = False
                    Return True
                Case 2
                    SubscriptionID = SQL.SQLDR("SubscriptionID")
                    isRegistered = True
                    Return True
                Case 0
                    Return False
            End Select
        Else
            Return False
        End If
    End Function

    Public Function SubsRemainingDays() As Integer
        Dim query As String
        query = " SELECT   SubscriptionValidty FROM tblLicenseInfo"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Dim daysLeft As Integer = DateDiff(DateInterval.Day, Date.Today, SQL.SQLDR("SubscriptionValidty"))
            If daysLeft < 0 Then
                Return 0
            Else
                Return daysLeft
            End If
        Else
            Return -1
        End If
    End Function
    Public Function TrialRemainingDay() As Integer
        Dim query As String
        query = " SELECT   TrialDays, TrialStart FROM tblLicenseInfo"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Dim daysLeft As Integer = SQL.SQLDR("TrialDays") - DateDiff(DateInterval.Day, SQL.SQLDR("TrialStart"), Date.Today)
            If daysLeft < 0 Then
                Return 0
            Else
                Return daysLeft
            End If
        Else
            Return -1
        End If
    End Function

    Public Function LoadVCECurrency(VCECode As String) As List(Of String)
        Dim list As New List(Of String)
        Dim query As String
        query = " SELECT  CurrencyCodes FROM tblVCE_Master WHERE VCECode = @VCECode "
        SQL.FlushParams()
        SQL.AddParam("@VCECode", VCECode)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Dim codes As String() = SQL.SQLDR("CurrencyCodes").ToString.Split("|")
            list.AddRange(codes)
        End If

        Return list
    End Function

    Public Sub LoadBaseCurrency()
        Dim query As String
        query = " SELECT ISNULL(Base_Currency,'') AS Base_Currency FROM tblCompany "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            BaseCurrency = SQL.SQLDR("Base_Currency").ToString
        Else
            BaseCurrency = ""
        End If
    End Sub
    Public Function GetCheckTemplateID(TemplateName As String) As Integer
        Dim query As String
        query = " SELECT ID FROM tblCheck_Template WHERE TemplateName = @TemplateName AND Status ='Active' "
        SQL.FlushParams()
        SQL.AddParam("@TemplateName", TemplateName)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() Then
            Return SQL.SQLDR("ID").ToString
        Else
            Return 0
        End If
    End Function

    Public Function GetCheckTemplateName(ID As Integer) As String
        Dim query As String
        query = " SELECT TemplateName FROM tblCheck_Template WHERE ID = @ID  "
        SQL.FlushParams()
        SQL.AddParam("@ID", ID)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read() Then
            Return SQL.SQLDR("TemplateName").ToString
        Else
            Return ""
        End If
    End Function


    Public Sub SaveTransactionList(RefTransID As Integer, RefType As String, Status As String)
        Try
            Dim insertSQL As String
            insertSQL = " INSERT INTO " & _
                        " tblTransaction_Approval (RefTransID, RefType, Status, WhoApproved)  " & _
                        " VALUES (@RefTransID, @RefType,  @Status, @WhoApproved) "
            SQL.FlushParams()
            SQL.AddParam("@RefTransID", RefTransID)
            SQL.AddParam("@RefType", RefType)
            SQL.AddParam("@Status", Status)
            SQL.AddParam("@WhoApproved", UserID)
            SQL.ExecNonQuery(insertSQL)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function GetPropName(ByVal PropCode As String) As String
        Dim query As String
        query = " SELECT UnitType + ' ' + Model + ' Blk ' + Unit_Blk + ' Lot ' + Unit_Lot AS Description  FROM tblSaleProperty WHERE UnitCode ='" & PropCode & "'"
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("Description").ToString
        Else
            Return ""
        End If
    End Function

    Public Function GetCIPName(ByVal CCCode As String) As String
        Dim query As String
        query = " SELECT CIP_Desc FROM tblCIP_Maintenance WHERE CIP_Code ='" & CCCode & "' AND Status <> 'Inactive'"
        SQL.ReadQuery(query, 2)
        If SQL.SQLDR2.Read Then
            Return SQL.SQLDR2("CIP_Desc").ToString
        Else
            Return ""
        End If
    End Function
End Module
