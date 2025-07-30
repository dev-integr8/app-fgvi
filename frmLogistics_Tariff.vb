Public Class frmLogistics_Tariff

    Private Sub frmLogistics_Tariff_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadRates()
    End Sub

    Private Sub LoadRates()
        Dim query As String
        query = " SELECT Destination, Location, VehicleType, SupplierRate, SupplierCode FROM tblBM_Tariff "
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvRecords.Rows.Add({SQL.SQLDR("Destination").ToString, SQL.SQLDR("Location").ToString, SQL.SQLDR("VehicleType").ToString,
                                SQL.SQLDR("SupplierRate").ToString, SQL.SQLDR("SupplierCode").ToString,
                                GetVCEName(SQL.SQLDR("SupplierCode").ToString)})
        End While
    End Sub

End Class