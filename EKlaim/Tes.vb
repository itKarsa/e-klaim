Imports MySql.Data.MySqlClient
Public Class Tes
    Sub DaftarPiutangRanap()
        Call koneksiJepe()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        query = "SELECT *
                   FROM t_eklaimjpranap
                  WHERE (SUBSTR(tglKeluar,1,10)) = '2021-12-02'"

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()
            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("noRM"), dr.Item("NoSep"), dr.Item("tglMasuk"), dr.Item("tglKeluar"), dr.Item("namaPasien"), "-", "-")
                daftardokterranap(dr.Item("noRM").ToString, dr.Item("tglKeluar").ToString)
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Sub daftardokterranap(norm As String, krs As Date)
        Call koneksiJepe()

        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        query = "SELECT *
                   FROM t_eklaimjpdokterranap
                  WHERE noRM = '" & norm & "' AND (SUBSTR(tglKeluar,1,10)) = '" & Format(krs, "yyyy-MM-dd") & "'"
        MsgBox(query)
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            Do While dr.Read
                DataGridView1.Rows.Add("-", "-", "-", "-", dr.Item("namaPasien"), dr.Item("drVisite"), dr.Item("drKonsul"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Private Sub Tes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DaftarPiutangRanap()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        MsgBox(Format(DateAdd(DateInterval.Day, -1, DateTimePicker1.Value), "yyyy-MM-dd") & " | " & Format(DateAdd(DateInterval.Day, 1, DateTimePicker1.Value), "yyyy-MM-dd"))
    End Sub
End Class