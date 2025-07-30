Public Class frmRE_Ledger
    Dim TransID As String
    Dim RENo As String
    Public Overloads Function ShowDialog(ByVal docnumber As String) As Boolean
        TransID = docnumber
        MyBase.ShowDialog()
        Return True
    End Function
    Private Sub frmRE_Ledger_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDeatils(TransID)
        LoadLedger(TransID)
    End Sub
    Private Sub LoadDeatils(TransID)
        Dim query As String
        query = " SELECT UnitCode, Description, VCECode, VCEName FROM viewRE_UnitDetails WHERE TransID = @TransiD "
        SQL.FlushParams()
        SQL.AddParam("@TransID", TransID)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Label2.Text = SQL.SQLDR("VCECode").ToString & " | " & SQL.SQLDR("VCEName").ToString
            Label1.Text = SQL.SQLDR("UnitCode").ToString & " | " & SQL.SQLDR("Description").ToString
        End If
    End Sub
    Private Sub LoadLedger(TransID)
        Dim query As String
        Dim Description As String = ""
        Dim DueDate As String
        Dim Num As Integer = 0
        Dim Principal As Decimal = 0
        Dim Interest As Decimal = 0
        Dim Penalty As Decimal = 0
        Dim PrevBalance As Decimal = 0
        Dim Total As Decimal = 0
        Dim AmountPaid As Decimal = 0
        Dim PrincipalPaid As Decimal = 0
        Dim InterestPaid As Decimal = 0
        Dim PenaltyPaid As Decimal = 0
        Dim Balance As Decimal = 0
        Dim PrincipalBalance As Decimal = 0
        Dim InterestBalance As Decimal = 0
        Dim PenaltyBalance As Decimal = 0
        Dim PaymentDate As String
        Dim Reference As String

        'query = " SELECT Num, Description, DueDate, Principal, Interest, Penalty, PrevBalance, TotalDue, " & vbCrLf &
        '        "           AmountPaid, PrincipalPaid, InterestPaid, PenaltyPaid, " & vbCrLf &
        '        "           Balance, PrincipalBalance, InterestBalance, PenaltyBalance, PaymentDate, PaymentReference " & vbCrLf &
        '        " FROM viewRE_Ledger WHERE TransID = @TransID " & vbCrLf &
        '        " GROUP BY  Num, Description, DueDate, Principal, Interest, Penalty, PrevBalance, TotalDue,AmountPaid, PrincipalPaid, InterestPaid, PenaltyPaid, Balance, PrincipalBalance, InterestBalance, PenaltyBalance, PaymentDate, PaymentReference"

        'query = "  SELECT Num, Description, DueDate, Principal, Interest, Penalty, PrevBalance, TotalDue,  " & vbCrLf &
        '        "            AmountPaid, PrincipalPaid, InterestPaid, PenaltyPaid,  " & vbCrLf &
        '        "            Balance, PrincipalBalance, InterestBalance, PenaltyBalance, PaymentDate, PaymentReference " & vbCrLf &
        '        "  FROM viewRE_Ledger WHERE TransID = @TransID  " & vbCrLf &
        '        "  GROUP BY  Num, Description, DueDate, Principal, Interest, Penalty, PrevBalance, TotalDue,AmountPaid, PrincipalPaid, InterestPaid, PenaltyPaid, Balance,  " & vbCrLf &
        '        "  PrincipalBalance, InterestBalance, PenaltyBalance, PaymentDate, PaymentReference " & vbCrLf &
        '        "  ORDER BY 
        '        CASE 
        '            WHEN description = 'Reservation' THEN 1E
        '            WHEN description LIKE '%Equity' THEN 2
        '            WHEN description = 'LOGB' THEN 3
        '            WHEN description = 'Loan Difference' THEN 4
        '            ELSE 5  -- Handle any other values not listed (optional)
        '        END, num "

        query = " SELECT Num, Description, DueDate, Principal, Interest, Penalty, 0 AS PrevBalance, Principal AS TotalDue,  
                        AmountPaid, PrincipalPaid, InterestPaid, PenaltyPaid,  
                        Balance, PrincipalBalance, InterestBalance, PenaltyBalance, PaymentDate, PaymentReference 
              FROM viewRE_Ledger WHERE TransID = '" & TransID & "'  
              GROUP BY  Num, Description, DueDate, Principal, Interest, Penalty, PrevBalance, TotalDue,AmountPaid, PrincipalPaid, InterestPaid, PenaltyPaid, Balance,  
              PrincipalBalance, InterestBalance, PenaltyBalance, PaymentDate, PaymentReference 
              ORDER BY 
                CASE 
                    WHEN description = 'Reservation' THEN 1
                    WHEN description LIKE '%Equity' THEN 2
                    WHEN description = 'LOGB' THEN 3
                    WHEN description = 'Loan Difference' THEN 4
                    ELSE 5  -- Handle any other values not listed (optional)
                END , num "
        SQL.FlushParams()
        SQL.AddParam("@TransiD", TransID)
        SQL.ReadQuery(query)
        dgvLedger.Rows.Clear()

        While SQL.SQLDR.Read
            Num = SQL.SQLDR("Num")
            Description = SQL.SQLDR("Description").ToString
            If CDate(SQL.SQLDR("DueDate")) = CDate("01-01-1900") Then
                DueDate = ""
            Else
                DueDate = CDate(SQL.SQLDR("DueDate")).ToString("MM/dd/yyyy")
            End If
            Principal = SQL.SQLDR("Principal")
            Interest = SQL.SQLDR("Interest")
            Penalty = SQL.SQLDR("Penalty")
            PrevBalance = SQL.SQLDR("PrevBalance")
            Total = SQL.SQLDR("TotalDue")
            AmountPaid = SQL.SQLDR("AmountPaid")
            PrincipalPaid = SQL.SQLDR("PrincipalPaid")
            InterestPaid = SQL.SQLDR("InterestPaid")
            PenaltyPaid = SQL.SQLDR("PenaltyPaid")
            Balance = SQL.SQLDR("Balance")
            PrincipalBalance = SQL.SQLDR("PrincipalBalance")
            InterestBalance = SQL.SQLDR("InterestBalance")
            PenaltyBalance = SQL.SQLDR("PenaltyBalance")
            If IsDBNull(SQL.SQLDR("PaymentDate")) OrElse CDate(SQL.SQLDR("PaymentDate")) = CDate("01-01-1900") Then
                PaymentDate = ""
            Else
                PaymentDate = CDate(SQL.SQLDR("PaymentDate")).ToString("MM/dd/yyyy")
            End If
            Reference = SQL.SQLDR("PaymentReference").ToString

            dgvLedger.Rows.Add({Num, Description, DueDate, Principal.ToString("N2"), Interest.ToString("N2"), Penalty.ToString("N2"), PrevBalance.ToString("N2"), Total.ToString("N2"),
                                   AmountPaid.ToString("N2"), PrincipalPaid.ToString("N2"), InterestPaid.ToString("N2"), PenaltyPaid.ToString("N2"),
                                   Balance.ToString("N2"), PrincipalBalance.ToString("N2"), InterestBalance.ToString("N2"), PenaltyBalance.ToString("N2"),
                                   PaymentDate, Reference})
        End While

    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Dim f As New frmReport_Display
        f.ShowDialog("RE_Ledger", TransID)
        f.Dispose()
    End Sub
End Class