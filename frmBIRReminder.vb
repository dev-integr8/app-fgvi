Public Class frmBIRReminder


    Private Sub frmBIRReminder_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim query As String
        query = " SELECT * FROM  " & _
                " ( " & _
                " SELECT  Birfrm, Period, Month, Date , CAST(CONCAT(Month,'-',DAte,'-', DATEPART(yyyy, getdate())) AS Date) AS Deadline,  DATEDIFF(DAY,GETDATE(),CAST(CONCAT(Month,'-',DAte,'-', DATEPART(yyyy, getdate())) AS Date)) AS DateDifference    " & _
                " 	FROM    tblTax_Deadline    " & _
                " 	WHERE DATEDIFF(DAY,GETDATE(),CAST(CONCAT(Month,'-',DAte,'-',DATEPART(yyyy, getdate())) AS Date)) <=  dbo.GetBIR_RemindersDay() " & _
                " 		AND  GETDATE() <=  CAST(CONCAT(Month,'-',DAte,'-',DATEPART(yyyy, getdate())) AS Date) " & _
                " 	UNION ALL " & _
                " SELECT  Birfrm, Period, Month, Date , CAST(CONCAT(Month,'-',DAte,'-', DATEPART(yyyy, DATEADD(year,1,GETDATE()))) AS Date) AS Deadline,  DATEDIFF(DAY,GETDATE(),CAST(CONCAT(Month,'-',DAte,'-', DATEPART(yyyy, DATEADD(year,1,GETDATE()))) AS Date)) AS DateDifference    " & _
                " 	FROM    tblTax_Deadline " & _
                " 	WHERE DATEDIFF(DAY,GETDATE(),CAST(CONCAT(Month,'-',DAte,'-',DATEPART(yyyy, DATEADD(year,1,GETDATE()))) AS Date)) <=  dbo.GetBIR_RemindersDay()  " & _
                " 		AND  GETDATE() <=  CAST(CONCAT(Month,'-',DAte,'-',DATEPART(yyyy, DATEADD(year,1,GETDATE()))) AS Date)  " & _
                " ) AS DEADLINE " & _
                " ORDER BY Deadline "

        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            lvReminders.Items.Add(SQL.SQLDR("Birfrm").ToString)
            lvReminders.Items(lvReminders.Items.Count - 1).SubItems.Add(SQL.SQLDR("Period").ToString)
            lvReminders.Items(lvReminders.Items.Count - 1).SubItems.Add(CDate(SQL.SQLDR("Deadline").ToString).ToShortDateString)
        End While
    End Sub

    Private Sub lvAmmort_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvReminders.SelectedIndexChanged

    End Sub

    Private Sub btnOkay_Click(sender As Object, e As EventArgs) Handles btnOkay.Click
        Me.Close()
        frmOption.Show()
    End Sub
End Class