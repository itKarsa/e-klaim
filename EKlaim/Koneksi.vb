Imports MySql.Data.MySqlClient
Module Koneksi

    Public conn As MySqlConnection
    Public cmd As MySqlCommand
    Public da As MySqlDataAdapter
    Public dr As MySqlDataReader
    Public ds As DataSet
    Public dt As New DataTable()
    Public str As String

    Public noRM As String
    Public noRegister As String
    Public noRanap As String
    Public tglDaftar As String
    Public unit As String
    Public ruang As String
    Public kelas As String

    Public Sub koneksiServer()
        Try
            Dim str As String = "Server=192.168.200.2;user id=lis;password=lis1234;database=simrs;default command timeout=120"
            conn = New MySqlConnection(str)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            MsgBox("Terputus dari server, Silahkan Login internet ke alamat IP 192.168.30.1 / Hubungi Tim IT", MsgBoxStyle.Exclamation, "Eklaim : Information")
            'LoginForm.Close()
        End Try
    End Sub

    Public Sub koneksiJepe()
        Try
            Dim str As String = "Server=192.168.200.2;user id=lis;password=lis1234;database=jepe;default command timeout=120"
            conn = New MySqlConnection(str)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            MsgBox("Terputus dari server, Silahkan Login internet ke alamat IP 192.168.30.1 / Hubungi Tim IT", MsgBoxStyle.Exclamation, "Eklaim : Information")
            'LoginForm.Close()
        End Try
    End Sub

    Function hitungUmur(ByVal tanggal As Date) As String
        Dim y, m, d As Integer
        y = Now.Year - tanggal.Year
        m = Now.Month - tanggal.Month
        d = Now.Day - tanggal.Day

        If Math.Sign(d) = -1 Then
            d = 30 - Math.Abs(d)
            m -= 1
        End If
        If Math.Sign(m) = -1 Then
            m = 12 - Math.Abs(m)
            y -= 1
        End If

        Return y & " th, " & m & " bln, " & d & " hr"
    End Function

End Module
