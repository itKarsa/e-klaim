Imports System.IO
Imports MySql.Data.MySqlClient
Public Class TotalRekap

    Dim FlNm As String
    Dim lastIndexRj As Integer
    Dim lastIndexRi As Integer

    Sub setColor(button As Button)
        btnHome.BackColor = Color.White
        btnEklaim.BackColor = Color.White
        btnBuku.BackColor = Color.White
        btnPiutang.BackColor = Color.White
        btnUmum.BackColor = Color.White
        button.BackColor = Color.FromArgb(209, 232, 223)
    End Sub

    Sub DaftarTotalRanapJkn()
        Call koneksiJepe()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        query = "SELECT *
                   FROM t_rekaptotalharianranap
                  WHERE (SUBSTR(tglRekap,1,8)) LIKE '" & Format(DateTimePicker1.Value, "yyyy-MM") & "%'
                  ORDER BY tglRekap ASC"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvRanap.Rows.Clear()
            Do While dr.Read
                dgvRanap.Rows.Add(dr.Item("tglRekap"), dr.Item("akomodasiAdmin"), dr.Item("akomodasiRuang"), dr.Item("jasaVisitKonsul"), dr.Item("prosedurbedah"),
                                  dr.Item("endoscopy"), dr.Item("bronkoscopy"), dr.Item("hd"), dr.Item("cvc"), dr.Item("ivp"),
                                  dr.Item("paru"), dr.Item("nonbedahlain"), dr.Item("gizi"), dr.Item("farklin"), dr.Item("fisio"),
                                  dr.Item("tindakan"), dr.Item("askep"), dr.Item("kerohanian"), dr.Item("ecg"), dr.Item("holter"),
                                  dr.Item("echocardio"), dr.Item("usg"), dr.Item("rontgen"), dr.Item("ctscan"), dr.Item("mri"),
                                  dr.Item("labpa"), dr.Item("labpk"), dr.Item("darah"), dr.Item("rehab"), dr.Item("icu"),
                                  dr.Item("picu"), dr.Item("nicu"), dr.Item("hcu"), dr.Item("obat"), dr.Item("alkes"),
                                  dr.Item("oksigen"), dr.Item("kassa"), dr.Item("jenazah"), dr.Item("ventilator"), dr.Item("nebulizer"),
                                  dr.Item("syringe"), dr.Item("bedsetmonitor"), dr.Item("total"), dr.Item("tarifinacbg"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Sub DaftarTotalRajalJkn()
        Call koneksiJepe()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        query = "SELECT *
                   FROM t_rekaptotalharianrajal
                  WHERE (SUBSTR(tglRekap,1,8)) LIKE '" & Format(DateTimePicker1.Value, "yyyy-MM") & "%'
                  ORDER BY tglRekap ASC"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvRajal.Rows.Clear()
            Do While dr.Read
                dgvRajal.Rows.Add(dr.Item("tglRekap"), dr.Item("admin"), dr.Item("jasaVisitKonsul"),
                                  dr.Item("prosedurbedah"), dr.Item("endoscopy"), dr.Item("bronkoscopy"), dr.Item("hd"), dr.Item("cvc"),
                                  dr.Item("ivp"), dr.Item("paru"), dr.Item("nonbedahlain"), dr.Item("gizi"), dr.Item("fisioterapi"),
                                  dr.Item("ecg"), dr.Item("holter"), dr.Item("treadmill"), dr.Item("echocardio"), dr.Item("usg"),
                                  dr.Item("rontgen"), dr.Item("ctscan"), dr.Item("mri"), dr.Item("labpa"), dr.Item("labpk"), dr.Item("darah"),
                                  dr.Item("obat"), dr.Item("alkes"), dr.Item("oksigen"), dr.Item("kassa"), dr.Item("tindakan"), dr.Item("ventilator"),
                                  dr.Item("nebulizer"), dr.Item("syringe"), dr.Item("total"), dr.Item("tarifinacbg"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Sub DaftarTotalRanapUmum()
        Call koneksiJepe()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        query = "SELECT *
                   FROM t_rekaptotalharianranapumum
                  WHERE (SUBSTR(tglRekap,1,8)) LIKE '" & Format(DateTimePicker1.Value, "yyyy-MM") & "%'
                  ORDER BY tglRekap ASC"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvRanap.Rows.Clear()
            Do While dr.Read
                dgvRanap.Rows.Add(dr.Item("tglRekap"), dr.Item("akomodasiAdmin"), dr.Item("akomodasiRuang"), dr.Item("jasaVisitKonsul"), dr.Item("prosedurbedah"),
                                  dr.Item("endoscopy"), dr.Item("bronkoscopy"), dr.Item("hd"), dr.Item("cvc"), dr.Item("ivp"),
                                  dr.Item("paru"), dr.Item("nonbedahlain"), dr.Item("gizi"), dr.Item("farklin"), dr.Item("fisio"),
                                  dr.Item("tindakan"), dr.Item("askep"), dr.Item("kerohanian"), dr.Item("ecg"), dr.Item("holter"),
                                  dr.Item("echocardio"), dr.Item("usg"), dr.Item("rontgen"), dr.Item("ctscan"), dr.Item("mri"),
                                  dr.Item("labpa"), dr.Item("labpk"), dr.Item("darah"), dr.Item("rehab"), dr.Item("icu"),
                                  dr.Item("picu"), dr.Item("nicu"), dr.Item("hcu"), dr.Item("obat"), dr.Item("alkes"),
                                  dr.Item("oksigen"), dr.Item("kassa"), dr.Item("jenazah"), dr.Item("ventilator"), dr.Item("nebulizer"),
                                  dr.Item("syringe"), dr.Item("bedsetmonitor"), dr.Item("total"), dr.Item("tarifinacbg"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Sub DaftarTotalRajalUmum()
        Call koneksiJepe()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        query = "SELECT *
                   FROM t_rekaptotalharianrajalumum
                  WHERE (SUBSTR(tglRekap,1,8)) LIKE '" & Format(DateTimePicker1.Value, "yyyy-MM") & "%'
                  ORDER BY tglRekap ASC"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvRajal.Rows.Clear()
            Do While dr.Read
                dgvRajal.Rows.Add(dr.Item("tglRekap"), dr.Item("admin"), dr.Item("jasaVisitKonsul"),
                                  dr.Item("prosedurbedah"), dr.Item("endoscopy"), dr.Item("bronkoscopy"), dr.Item("hd"), dr.Item("cvc"),
                                  dr.Item("ivp"), dr.Item("paru"), dr.Item("nonbedahlain"), dr.Item("gizi"), dr.Item("fisioterapi"),
                                  dr.Item("ecg"), dr.Item("holter"), dr.Item("treadmill"), dr.Item("echocardio"), dr.Item("usg"),
                                  dr.Item("rontgen"), dr.Item("ctscan"), dr.Item("mri"), dr.Item("labpa"), dr.Item("labpk"), dr.Item("darah"),
                                  dr.Item("obat"), dr.Item("alkes"), dr.Item("oksigen"), dr.Item("kassa"), dr.Item("tindakan"), dr.Item("ventilator"),
                                  dr.Item("nebulizer"), dr.Item("syringe"), dr.Item("total"), dr.Item("tarifinacbg"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Sub tableRajal()
        Dim tglFile, tglSheet As String
        tglSheet = DateTimePicker1.Value.ToString("dd")
        tglFile = DateTimePicker1.Value.ToString("MMMM yyyy")

        Dim DGV As New DataGridView
        With DGV
            .AllowUserToAddRows = False
            .Name = "Rajal"
            .Visible = False
            .Columns.Clear()
            .Columns.Add("Tanggal", "Tanggal")
            .Columns.Add("Admin", "Admin")
            .Columns.Add("Jasa Visite/Konsul", "Jasa Visite/Konsul")
            .Columns.Add("Prosedur Bedah", "Prosedur Bedah")
            .Columns.Add("Endoscopy", "Endoscopy")
            .Columns.Add("Bronchoscopy", "Bronchoscopy")
            .Columns.Add("Hemodialisa", "Hemodialisa")
            .Columns.Add("CVC", "CVC")
            .Columns.Add("IVP", "IVP")
            .Columns.Add("OK PARU", "OK PARU")
            .Columns.Add("Lain-Lain", "Lain-Lain")
            .Columns.Add("Nutrisionis", "Nutrisionis")
            .Columns.Add("Fisioterapis", "Fisioterapis")
            .Columns.Add("ECG", "ECG")
            .Columns.Add("Holter", "Holter")
            .Columns.Add("Treadmill", "Treadmill")
            .Columns.Add("Echocardiograf", "Echocardiograf")
            .Columns.Add("USG", "USG")
            .Columns.Add("RONTGEN", "RONTGEN")
            .Columns.Add("CT-SCAN", "CT-SCAN")
            .Columns.Add("MRI", "MRI")
            .Columns.Add("Lab. PA", "Lab. PA")
            .Columns.Add("Lab. PK", "Lab. PK")
            .Columns.Add("Darah/PMI", "Darah/PMI")
            .Columns.Add("Obat", "Obat")
            .Columns.Add("Alkes", "Alkes")
            .Columns.Add("Oksigen", "Oksigen")
            .Columns.Add("Kassa", "Kassa")
            .Columns.Add("Tindakan", "Tindakan")
            .Columns.Add("Ventilator", "Ventilator")
            .Columns.Add("Nebulizer", "Nebulizer")
            .Columns.Add("Syr. Pump", "Syr. Pump")
            .Columns.Add("Total", "Total")
            .Columns.Add("INACBG", "INACBG")
        End With

        With dgvRajal
            If .Rows.Count > 0 Then
                For i As Integer = 0 To .Rows.Count - 1
                    Application.DoEvents()
                    DGV.Rows.Add(.Rows(i).Cells(0).Value, .Rows(i).Cells(1).Value, .Rows(i).Cells(2).Value,
                                     .Rows(i).Cells(3).Value, .Rows(i).Cells(4).Value, .Rows(i).Cells(5).Value,
                                     .Rows(i).Cells(6).Value, .Rows(i).Cells(7).Value, .Rows(i).Cells(8).Value,
                                     .Rows(i).Cells(9).Value, .Rows(i).Cells(10).Value, .Rows(i).Cells(11).Value,
                                     .Rows(i).Cells(12).Value, .Rows(i).Cells(13).Value, .Rows(i).Cells(14).Value,
                                     .Rows(i).Cells(15).Value, .Rows(i).Cells(16).Value, .Rows(i).Cells(17).Value,
                                     .Rows(i).Cells(18).Value, .Rows(i).Cells(19).Value, .Rows(i).Cells(20).Value,
                                     .Rows(i).Cells(21).Value, .Rows(i).Cells(22).Value, .Rows(i).Cells(23).Value,
                                     .Rows(i).Cells(24).Value, .Rows(i).Cells(25).Value, .Rows(i).Cells(26).Value,
                                     .Rows(i).Cells(27).Value, .Rows(i).Cells(28).Value, .Rows(i).Cells(29).Value,
                                     .Rows(i).Cells(30).Value, .Rows(i).Cells(31).Value, .Rows(i).Cells(32).Value,
                                     .Rows(i).Cells(33).Value)
                Next
            End If
        End With

        Dim DGVDOK As New DataGridView
        With DGVDOK
            .AllowUserToAddRows = False
            .Name = "Dokter"
            .Visible = False
            .Columns.Clear()
            .Columns.Add("No. RM", "No. RM")
            .Columns.Add("No. SEP", "No. SEP")
            .Columns.Add("Nama Pasien", "Nama Pasien")
            .Columns.Add("Tgl. Masuk", "Tgl. Masuk")
            .Columns.Add("Tgl. Keluar", "Tgl. Keluar")
            .Columns.Add("Ruang", "Ruang")
            .Columns.Add("Jml. Visite", "Jml. Visite")
            .Columns.Add("Visite", "Visite")
            .Columns.Add("Konsultasi", "Konsultasi")
            .Columns.Add("Jasa Visite", "Jasa Visite")
        End With

        'With dgvDokterAll
        '    If .Rows.Count > 0 Then
        '        For i As Integer = 0 To .Rows.Count - 1
        '            Application.DoEvents()
        '            DGVDOK.Rows.Add(.Rows(i).Cells(0).Value, .Rows(i).Cells(1).Value, .Rows(i).Cells(2).Value,
        '                             .Rows(i).Cells(3).Value, .Rows(i).Cells(4).Value, .Rows(i).Cells(5).Value,
        '                             .Rows(i).Cells(6).Value, .Rows(i).Cells(7).Value, .Rows(i).Cells(8).Value,
        '                             .Rows(i).Cells(9).Value)
        '        Next
        '    End If
        'End With

        If txtPenjamin.Text = "JKN" Then
            FlNm = "C:\Eklaim\Total Rekap JP JKN Rajal " & tglFile & ".xml"
        ElseIf txtPenjamin.Text = "Umum" Then
            FlNm = "C:\Eklaim\Total Rekap JP Umum Rajal " & tglFile & ".xml"
        End If

        'FlNm = Application.StartupPath & "\Student " _
        '        & Now.Day & "-" & Now.Month & "-" & Now.Year & ".xls"
        Try
            If File.Exists(FlNm) Then File.Delete(FlNm)
            ExToExcel(DGV, DGVDOK, FlNm)
            'MsgBox(DGV.Rows.Count)
            'MsgBox(DGVDOK.Rows.Count)
            DGV.Dispose()
            DGVDOK.Dispose()
            DGV = Nothing
            DGVDOK = Nothing
        Catch ex As Exception
            MsgBox("Export file gagal", MsgBoxStyle.Exclamation)
        End Try

        If txtPenjamin.Text = "JKN" Then
            Process.Start("C:\Eklaim\Total Rekap JP JKN Rajal " & tglFile & ".xml")
        ElseIf txtPenjamin.Text = "Umum" Then
            Process.Start("C:\Eklaim\Total Rekap JP Umum Rajal " & tglFile & ".xml")
        End If
    End Sub

    Sub tableRanap()
        Dim tglFile, tglSheet As String
        tglSheet = DateTimePicker1.Value.ToString("dd")
        tglFile = DateTimePicker1.Value.ToString("MMM yyyy")

        Dim DGV As New DataGridView
        With DGV
            .AllowUserToAddRows = False
            .Name = "Ranap"
            .Visible = False
            .Columns.Clear()
            .Columns.Add("Tanggal", "Tanggal")
            .Columns.Add("Admin", "Admin")
            .Columns.Add("Biaya Ruang", "Biaya Ruang")
            .Columns.Add("Jasa Visite", "Jasa Visite")
            .Columns.Add("Prosedur Bedah", "Prosedur Bedah")
            .Columns.Add("Endoscopy", "Endoscopy")
            .Columns.Add("Bronchoscopy", "Bronchoscopy")
            .Columns.Add("Hemodialisa", "Hemodialisa")
            .Columns.Add("CVC", "CVC")
            .Columns.Add("IVP", "IVP")
            .Columns.Add("OK PARU", "OK PARU")
            .Columns.Add("Lain-Lain", "Lain-Lain")
            .Columns.Add("Nutrisionis", "Nutrisionis")
            .Columns.Add("Farklin", "Farklin")
            .Columns.Add("Fisioterapis", "Fisioterapis")
            .Columns.Add("Tindakan Ruang", "Tindakan Ruang")
            .Columns.Add("ASKEP", "ASKEP")
            .Columns.Add("Kerohanian", "Kerohanian")
            .Columns.Add("ECG", "ECG")
            .Columns.Add("Holter", "Holter")
            .Columns.Add("Echocardiograf", "Echocardiograf")
            .Columns.Add("USG", "USG")
            .Columns.Add("RONTGEN", "RONTGEN")
            .Columns.Add("CT-SCAN", "CT-SCAN")
            .Columns.Add("MRI", "MRI")
            .Columns.Add("Lab. PA", "Lab. PA")
            .Columns.Add("Lab. PK", "Lab. PK")
            .Columns.Add("Darah/PMI", "Darah/PMI")
            .Columns.Add("Rehabilitasi", "Rehabilitasi")
            .Columns.Add("ICU", "ICU")
            .Columns.Add("PICU", "PICU")
            .Columns.Add("NICU", "NICU")
            .Columns.Add("HCU", "HCU")
            .Columns.Add("Obat", "Obat")
            .Columns.Add("Alkes", "Alkes")
            .Columns.Add("Oksigen", "Oksigen")
            .Columns.Add("Kassa", "Kassa")
            .Columns.Add("R. Jenazah", "R. Jenazah")
            .Columns.Add("Ventilator", "Ventilator")
            .Columns.Add("Nebulizer", "Nebulizer")
            .Columns.Add("Syr. Pump", "Syr. Pump")
            .Columns.Add("Monitor", "Monitor")
            .Columns.Add("Total", "Total")
            .Columns.Add("INACBG", "INACBG")
        End With

        With dgvRanap
            If .Rows.Count > 0 Then
                For i As Integer = 0 To .Rows.Count - 1
                    Application.DoEvents()
                    DGV.Rows.Add(.Rows(i).Cells(0).Value, .Rows(i).Cells(1).Value, .Rows(i).Cells(2).Value,
                                     .Rows(i).Cells(3).Value, .Rows(i).Cells(4).Value, .Rows(i).Cells(5).Value,
                                     .Rows(i).Cells(6).Value, .Rows(i).Cells(7).Value, .Rows(i).Cells(8).Value,
                                     .Rows(i).Cells(9).Value, .Rows(i).Cells(10).Value, .Rows(i).Cells(11).Value,
                                     .Rows(i).Cells(12).Value, .Rows(i).Cells(13).Value, .Rows(i).Cells(14).Value,
                                     .Rows(i).Cells(15).Value, .Rows(i).Cells(16).Value, .Rows(i).Cells(17).Value,
                                     .Rows(i).Cells(18).Value, .Rows(i).Cells(19).Value, .Rows(i).Cells(20).Value,
                                     .Rows(i).Cells(21).Value, .Rows(i).Cells(22).Value, .Rows(i).Cells(23).Value,
                                     .Rows(i).Cells(24).Value, .Rows(i).Cells(25).Value, .Rows(i).Cells(26).Value,
                                     .Rows(i).Cells(27).Value, .Rows(i).Cells(28).Value, .Rows(i).Cells(29).Value,
                                     .Rows(i).Cells(30).Value, .Rows(i).Cells(31).Value, .Rows(i).Cells(32).Value,
                                     .Rows(i).Cells(33).Value, .Rows(i).Cells(34).Value, .Rows(i).Cells(35).Value,
                                     .Rows(i).Cells(36).Value, .Rows(i).Cells(37).Value, .Rows(i).Cells(38).Value,
                                     .Rows(i).Cells(39).Value, .Rows(i).Cells(40).Value, .Rows(i).Cells(41).Value,
                                     .Rows(i).Cells(42).Value, .Rows(i).Cells(43).Value)
                Next
            End If
        End With

        Dim DGVDOK As New DataGridView
        With DGVDOK
            .AllowUserToAddRows = False
            .Name = "Dokter"
            .Visible = False
            .Columns.Clear()
            .Columns.Add("No. RM", "No. RM")
            .Columns.Add("No. SEP", "No. SEP")
            .Columns.Add("Nama Pasien", "Nama Pasien")
            .Columns.Add("Tgl. Masuk", "Tgl. Masuk")
            .Columns.Add("Tgl. Keluar", "Tgl. Keluar")
            .Columns.Add("Ruang", "Ruang")
            .Columns.Add("Jml. Visite", "Jml. Visite")
            .Columns.Add("Visite", "Visite")
            .Columns.Add("Konsultasi", "Konsultasi")
            .Columns.Add("Jasa Visite", "Jasa Visite")
        End With

        'With dgvDokterAll
        '    If .Rows.Count > 0 Then
        '        For i As Integer = 0 To .Rows.Count - 1
        '            Application.DoEvents()
        '            DGVDOK.Rows.Add(.Rows(i).Cells(0).Value, .Rows(i).Cells(1).Value, .Rows(i).Cells(2).Value,
        '                             .Rows(i).Cells(3).Value, .Rows(i).Cells(4).Value, .Rows(i).Cells(5).Value,
        '                             .Rows(i).Cells(6).Value, .Rows(i).Cells(7).Value, .Rows(i).Cells(8).Value,
        '                             .Rows(i).Cells(9).Value)
        '        Next
        '    End If
        'End With

        If txtPenjamin.Text = "JKN" Then
            FlNm = "C:\Eklaim\Total Rekap JP JKN Ranap " & tglFile & ".xml"
        ElseIf txtPenjamin.Text = "Umum" Then
            FlNm = "C:\Eklaim\Total Rekap JP Umum Ranap " & tglFile & ".xml"
        End If

        'FlNm = Application.StartupPath & "\Student " _
        '        & Now.Day & "-" & Now.Month & "-" & Now.Year & ".xls"
        If File.Exists(FlNm) Then File.Delete(FlNm)
        ExToExcel(DGV, DGVDOK, FlNm)

        DGV.Dispose()
        DGVDOK.Dispose()
        DGV = Nothing
        DGVDOK = Nothing

        If txtPenjamin.Text = "JKN" Then
            Process.Start("C:\Eklaim\Total Rekap JP JKN Ranap " & tglFile & ".xml")
        ElseIf txtPenjamin.Text = "Umum" Then
            Process.Start("C:\Eklaim\Total Rekap JP Umum Ranap " & tglFile & ".xml")
        End If
    End Sub

    Private Sub TotalRekap_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        btnTotal.BackColor = Color.FromArgb(209, 232, 223)
        PicCollapse.Visible = False

        txtUser.Text = Home.txtUser.Text
        DateTimePicker1.Value = DateTime.Now
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "MMMM yyyy"
        DateTimePicker1.ShowUpDown = True
        txtFilter.SelectedIndex = 0
        txtPenjamin.SelectedIndex = 0
    End Sub

    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        Home.Show()
        Me.Hide()
    End Sub

    Private Sub btnEklaim_Click(sender As Object, e As EventArgs) Handles btnEklaim.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub btnBuku_Click(sender As Object, e As EventArgs) Handles btnBuku.Click
        Pembukuan.Show()
        Me.Hide()
    End Sub

    Private Sub btnPiutang_Click(sender As Object, e As EventArgs) Handles btnPiutang.Click
        RekapPiutang.Show()
        Me.Hide()
    End Sub

    Private Sub btnUmum_Click(sender As Object, e As EventArgs) Handles btnUmum.Click
        RekapPiutangUmum.Show()
        Me.Hide()
    End Sub

    Private Sub btnTotal_Click(sender As Object, e As EventArgs) Handles btnTotal.Click
        Dim btn As Button = CType(sender, Button)
        setColor(btn)
    End Sub

    Private Sub PicExpand_Click(sender As Object, e As EventArgs) Handles PicExpand.Click
        TableLayoutPanel1.ColumnStyles(0).SizeType = SizeType.Percent
        TableLayoutPanel1.ColumnStyles(0).Width = 0
        PicCollapse.Visible = True
    End Sub

    Private Sub PicCollapse_Click(sender As Object, e As EventArgs) Handles PicCollapse.Click
        TableLayoutPanel1.ColumnStyles(0).SizeType = SizeType.Percent
        TableLayoutPanel1.ColumnStyles(0).Width = 15
        PicCollapse.Visible = False
    End Sub

    Private Sub picKeluar_Click(sender As Object, e As EventArgs) Handles picKeluar.Click
        Dim konfirmasi As MsgBoxResult

        konfirmasi = MsgBox("Apakah anda yakin ingin keluar..?", vbQuestion + vbYesNo, "Konfirmasi")
        If konfirmasi = vbYes Then
            Me.Close()
            LoginForm.Show()
        End If
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        If txtFilter.Text = "Rawat Jalan" And txtPenjamin.Text = "JKN" Then
            dgvRajal.Visible = True
            dgvRanap.Visible = False
            Call DaftarTotalRajalJkn()
        ElseIf txtFilter.Text = "Rawat Jalan" And txtPenjamin.Text = "Umum" Then
            dgvRajal.Visible = True
            dgvRanap.Visible = False
            Call DaftarTotalRajalUmum()
        ElseIf txtFilter.Text = "Rawat Inap" And txtPenjamin.Text = "JKN" Then
            dgvRajal.Visible = False
            dgvRanap.Visible = True
            Call DaftarTotalRanapJkn()
        ElseIf txtFilter.Text = "Rawat Inap" And txtPenjamin.Text = "Umum" Then
            dgvRajal.Visible = False
            dgvRanap.Visible = True
            Call DaftarTotalRanapUmum()
        End If

#Region "Comment"
#Region "Total Rajal"
        Dim totAdmin, totJasaRj, totEndos,
            totBedah, totBronc, totHemo, totCvc, totIvp,
            totLain, totGizi, totFisio, totECG, totHolter,
            totTread, totEcho, totUsg, totRontgen, totCtScan,
            totMri, totPA, totPK, totDarah, totObat,
            totAlkes, totOxy, totKassa, totTind, totVenti,
            totNebul, totSyr, totTotal, totTotal2 As Double

        For i As Integer = 0 To dgvRajal.RowCount - 1
            totAdmin += dgvRajal.Rows(i).Cells(1).Value
            totJasaRj += dgvRajal.Rows(i).Cells(2).Value
            totBedah += dgvRajal.Rows(i).Cells(3).Value

            totEndos += dgvRajal.Rows(i).Cells(4).Value
            totBronc += dgvRajal.Rows(i).Cells(5).Value
            totHemo += dgvRajal.Rows(i).Cells(6).Value
            totCvc += dgvRajal.Rows(i).Cells(7).Value
            totIvp += dgvRajal.Rows(i).Cells(8).Value

            totLain += dgvRajal.Rows(i).Cells(9).Value
            totGizi += dgvRajal.Rows(i).Cells(10).Value
            totFisio += dgvRajal.Rows(i).Cells(11).Value
            totECG += dgvRajal.Rows(i).Cells(12).Value
            totHolter += dgvRajal.Rows(i).Cells(13).Value

            totTread += dgvRajal.Rows(i).Cells(14).Value
            totEcho += dgvRajal.Rows(i).Cells(15).Value
            totUsg += dgvRajal.Rows(i).Cells(16).Value
            totRontgen += dgvRajal.Rows(i).Cells(17).Value
            totCtScan += dgvRajal.Rows(i).Cells(18).Value

            totMri += dgvRajal.Rows(i).Cells(19).Value
            totPA += dgvRajal.Rows(i).Cells(20).Value
            totPK += dgvRajal.Rows(i).Cells(21).Value
            totDarah += dgvRajal.Rows(i).Cells(22).Value
            totObat += dgvRajal.Rows(i).Cells(23).Value

            totAlkes += dgvRajal.Rows(i).Cells(24).Value
            totOxy += dgvRajal.Rows(i).Cells(25).Value
            totKassa += dgvRajal.Rows(i).Cells(26).Value
            totTind += dgvRajal.Rows(i).Cells(27).Value

            totVenti += dgvRajal.Rows(i).Cells(28).Value
            totNebul += dgvRajal.Rows(i).Cells(29).Value
            totSyr += dgvRajal.Rows(i).Cells(30).Value
            totTotal += dgvRajal.Rows(i).Cells(31).Value
            totTotal2 += dgvRajal.Rows(i).Cells(32).Value
        Next

        dgvRajal.Rows.Add("TOTAL", totAdmin, totJasaRj, totBedah,
                          totEndos, totBronc, totHemo, totCvc, totIvp,
                          totLain, totGizi, totFisio, totECG, totHolter,
                          totTread, totEcho, totUsg, totRontgen, totCtScan,
                          totMri, totPA, totPK, totDarah, totObat,
                          totAlkes, totOxy, totKassa, totTind, totVenti,
                          totNebul, totSyr, totTotal, totTotal2, 0)

        lastIndexRj = dgvRajal.Rows.Count - 1
#End Region
#Region "Total Ranap"
        Dim riAdmin, riAkmds, riJasa, riBedah, riEndos,
            riBronc, riHemo, riCvc, riIvp, riLain,
            riGizi, riFarklin, riFisio, riTind, riAskep,
            riRohani, riECG, riHolter, riEcho, riUsg,
            riRontgen, riCtScan, riMri, riPA, riPK,
            riPmi, riRehab, riIcu, riPicu, riNicu,
            riHcu, riObat, riAlkes, riOxy, riKassa,
            riJenazah, riVenti, riNebul, riSyr, riMonitor,
            riTotal, riTotal2 As Double

        For i As Integer = 0 To dgvRanap.RowCount - 1
            riAdmin += dgvRanap.Rows(i).Cells(1).Value
            riAkmds += dgvRanap.Rows(i).Cells(2).Value
            riJasa += dgvRanap.Rows(i).Cells(3).Value
            riBedah += dgvRanap.Rows(i).Cells(4).Value
            riEndos += dgvRanap.Rows(i).Cells(5).Value
            '5
            riBronc += dgvRanap.Rows(i).Cells(6).Value
            riHemo += dgvRanap.Rows(i).Cells(7).Value
            riCvc += dgvRanap.Rows(i).Cells(8).Value
            riIvp += dgvRanap.Rows(i).Cells(9).Value
            riLain += dgvRanap.Rows(i).Cells(10).Value
            '10
            riGizi += dgvRanap.Rows(i).Cells(11).Value
            riFarklin += dgvRanap.Rows(i).Cells(12).Value
            riFisio += dgvRanap.Rows(i).Cells(13).Value
            riTind += dgvRanap.Rows(i).Cells(14).Value
            riAskep += dgvRanap.Rows(i).Cells(15).Value
            '15
            riRohani += dgvRanap.Rows(i).Cells(16).Value
            riECG += dgvRanap.Rows(i).Cells(17).Value
            riHolter += dgvRanap.Rows(i).Cells(18).Value
            riEcho += dgvRanap.Rows(i).Cells(19).Value
            riUsg += dgvRanap.Rows(i).Cells(20).Value
            '20
            riRontgen += dgvRanap.Rows(i).Cells(21).Value
            riCtScan += dgvRanap.Rows(i).Cells(22).Value
            riMri += dgvRanap.Rows(i).Cells(23).Value
            riPA += dgvRanap.Rows(i).Cells(24).Value
            riPK += dgvRanap.Rows(i).Cells(25).Value
            '25
            riPmi += dgvRanap.Rows(i).Cells(26).Value
            riRehab += dgvRanap.Rows(i).Cells(27).Value
            riIcu += dgvRanap.Rows(i).Cells(28).Value
            riPicu += dgvRanap.Rows(i).Cells(29).Value
            riNicu += dgvRanap.Rows(i).Cells(30).Value
            '30
            riHcu += dgvRanap.Rows(i).Cells(31).Value
            riObat += dgvRanap.Rows(i).Cells(32).Value
            riAlkes += dgvRanap.Rows(i).Cells(33).Value
            riOxy += dgvRanap.Rows(i).Cells(34).Value
            riKassa += dgvRanap.Rows(i).Cells(35).Value
            '35
            riJenazah += dgvRanap.Rows(i).Cells(36).Value
            riVenti += dgvRanap.Rows(i).Cells(37).Value
            riNebul += dgvRanap.Rows(i).Cells(38).Value
            riSyr += dgvRanap.Rows(i).Cells(39).Value
            riMonitor += dgvRanap.Rows(i).Cells(40).Value
            '40
            riTotal += dgvRanap.Rows(i).Cells(41).Value
            riTotal2 += dgvRanap.Rows(i).Cells(42).Value
        Next

        dgvRanap.Rows.Add("TOTAL", riAdmin, riAkmds, riJasa, riBedah,
                          riEndos, riBronc, riHemo, riCvc, riIvp,
                          riLain, riGizi, riFarklin, riFisio, riTind,
                          riAskep, riRohani, riECG, riHolter, riEcho,
                          riUsg, riRontgen, riCtScan, riMri, riPA,
                          riPK, riPmi, riRehab, riIcu, riPicu,
                          riNicu, riHcu, riObat, riAlkes, riOxy,
                          riKassa, riJenazah, riVenti, riNebul, riSyr,
                          riMonitor, riTotal, riTotal2, 0)

        lastIndexRi = dgvRanap.Rows.Count - 1
#End Region
#End Region
    End Sub

    Private Sub dgvRanap_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvRanap.CellFormatting
        'For i As Integer = 14 To dgvRanap.Columns.Count - 1
        '    dgvRanap.Columns(i).DefaultCellStyle.Format = "N0"
        'Next

        dgvRanap.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        dgvRanap.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
        dgvRanap.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvRanap.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        For i As Integer = 0 To dgvRanap.Rows.Count - 1
            If i Mod 2 = 0 Then
                dgvRanap.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                dgvRanap.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Private Sub dgvRajal_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvRajal.CellFormatting
        'For i As Integer = 14 To dgvRajal.Columns.Count - 1
        '    dgvRajal.Columns(i).DefaultCellStyle.Format = "N0"
        'Next

        dgvRajal.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        dgvRajal.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
        dgvRajal.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvRajal.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        For i As Integer = 0 To dgvRajal.Rows.Count - 1
            If i Mod 2 = 0 Then
                dgvRajal.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                dgvRajal.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Private Sub dgvRanap_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvRajal.RowPostPaint
        Dim dg As DataGridView = DirectCast(sender, DataGridView)
        Dim rowNumber As String = (e.RowIndex + 1).ToString()

        Dim bold As New Font("Segoe UI", 8, FontStyle.Bold)
        Dim size As SizeF = e.Graphics.MeasureString(rowNumber, bold)
        If dg.RowHeadersWidth < CInt(size.Width + 20) Then
            dg.RowHeadersWidth = CInt(size.Width + 20)
        End If

        Dim b As Brush = Brushes.SeaGreen

        e.Graphics.DrawString(rowNumber, dg.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2))
    End Sub

    Private Sub dgvRajal_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvRajal.RowPostPaint
        Dim dg As DataGridView = DirectCast(sender, DataGridView)
        Dim rowNumber As String = (e.RowIndex + 1).ToString()

        Dim bold As New Font("Segoe UI", 8, FontStyle.Bold)
        Dim size As SizeF = e.Graphics.MeasureString(rowNumber, bold)
        If dg.RowHeadersWidth < CInt(size.Width + 20) Then
            dg.RowHeadersWidth = CInt(size.Width + 20)
        End If

        Dim b As Brush = Brushes.SeaGreen

        e.Graphics.DrawString(rowNumber, dg.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2))
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.Text = "Please Wait..."
        Button1.Enabled = False
        Application.DoEvents()

        If txtFilter.Text = "Rawat Jalan" Then
            Call tableRajal()
        ElseIf txtFilter.Text = "Rawat Inap" Then
            Call tableRanap()
        End If

        Button1.Text = "Export To Excel"
        Button1.Enabled = True
    End Sub
End Class