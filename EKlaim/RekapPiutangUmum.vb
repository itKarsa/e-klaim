Imports System.IO
Imports MySql.Data.MySqlClient
Imports Excel = Microsoft.Office.Interop.Excel
Public Class RekapPiutangUmum

    Dim noRm, pasien As String
    Dim FlNm As String

    Sub setColor(button As Button)
        btnHome.BackColor = Color.White
        btnEklaim.BackColor = Color.White
        btnBuku.BackColor = Color.White
        btnPiutang.BackColor = Color.White
        btnUmum.BackColor = Color.White
        button.BackColor = Color.FromArgb(209, 232, 223)
    End Sub

    Sub DaftarDokter()
        Call koneksiJepe()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        If txtFilter.Text = "Rawat Jalan" Then
            query = "SELECT *
                   FROM t_eklaimjpdokterrajalumum
                  WHERE noRM = '" & noRm & "'"
        ElseIf txtFilter.Text = "Rawat Inap" Then
            query = "SELECT *
                   FROM t_eklaimjpdokterranapumum
                  WHERE noRM = '" & noRm & "'"
        End If
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()
            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("namaPasien"), dr.Item("unit"), dr.Item("jmlVisite"),
                                       dr.Item("drVisite"), dr.Item("drKonsul"), dr.Item("jasaVisite"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Sub DaftarDokterAll()
        Call koneksiJepe()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        If txtFilter.Text = "Rawat Jalan" Then
            query = "SELECT *
                   FROM t_eklaimjpdokterrajalumum
                  WHERE (SUBSTR(tglKeluar,1,10)) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
        ElseIf txtFilter.Text = "Rawat Inap" Then
            query = "SELECT *
                   FROM t_eklaimjpdokterranapumum
                  WHERE (SUBSTR(tglKeluar,1,10)) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
        End If

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvDokterAll.Rows.Clear()
            Do While dr.Read
                dgvDokterAll.Rows.Add(dr.Item("noRM"), dr.Item("NoSEP"), dr.Item("namaPasien"),
                                       dr.Item("tglMasuk"), dr.Item("tglKeluar"), dr.Item("unit"),
                                       dr.Item("jmlVisite"), dr.Item("drVisite"), dr.Item("drKonsul"),
                                       dr.Item("jasaVisite"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Sub DaftarPiutangRanap()
        Call koneksiJepe()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        query = "SELECT *
                   FROM t_eklaimjpranapumum
                  WHERE (SUBSTR(tglKeluar,1,10)) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvRanap.Rows.Clear()
            Do While dr.Read
                dgvRanap.Rows.Add(dr.Item("noRM"), dr.Item("NoSep"), dr.Item("tglMasuk"), dr.Item("tglKeluar"), dr.Item("namaPasien"),
                                  dr.Item("unit"), dr.Item("hakKelas"), dr.Item("kelas"), dr.Item("jmlHari"), dr.Item("dpjp"),
                                  dr.Item("tarifdpjp"), dr.Item("drOperator"), dr.Item("drAnestesi"), dr.Item("akomodasiAdmin"),
                                  dr.Item("akomodasiRuang"), dr.Item("prosedurbedah"), dr.Item("endoscopy"), dr.Item("bronkoscopy"),
                                  dr.Item("hd"), dr.Item("cvc"), dr.Item("ivp"), dr.Item("nonbedahlain"), dr.Item("gizi"),
                                  dr.Item("farklin"), dr.Item("fisio"), dr.Item("tindakan"), dr.Item("askep"), dr.Item("kerohanian"),
                                  dr.Item("ecg"), dr.Item("holter"), dr.Item("echocardio"), dr.Item("usg"), dr.Item("rontgen"),
                                  dr.Item("ctscan"), dr.Item("mri"), dr.Item("labpa"), dr.Item("labpk"), dr.Item("darah"),
                                  dr.Item("rehab"), dr.Item("icu"), dr.Item("picu"), dr.Item("nicu"), dr.Item("hcu"),
                                  dr.Item("obat"), dr.Item("alkes"), dr.Item("oksigen"), dr.Item("kassa"), dr.Item("jenazah"),
                                  dr.Item("ventilator"), dr.Item("nebulizer"), dr.Item("syringe"), dr.Item("bedsetmonitor"),
                                  dr.Item("total"), dr.Item("tarifinacbg"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Sub DaftarPiutangRajal()
        Call koneksiJepe()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        query = "SELECT *
                   FROM t_eklaimjprajalumum
                  WHERE (SUBSTR(tglKeluar,1,10)) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvRajal.Rows.Clear()
            Do While dr.Read
                dgvRajal.Rows.Add(dr.Item("noRM"), dr.Item("NoSep"), dr.Item("tglMasuk"), dr.Item("tglKeluar"), dr.Item("namaPasien"),
                                       dr.Item("unit"), dr.Item("admin"), dr.Item("endoscopy"), dr.Item("bronkoscopy"), dr.Item("hd"),
                                       dr.Item("cvc"), dr.Item("ivp"), dr.Item("nonbedahlain"), dr.Item("gizi"), dr.Item("fisioterapi"),
                                       dr.Item("ecg"), dr.Item("holter"), dr.Item("treadmill"), dr.Item("echocardio"), dr.Item("usg"),
                                       dr.Item("rontgen"), dr.Item("ctscan"), dr.Item("mri"), dr.Item("labpa"), dr.Item("labpk"),
                                       dr.Item("obat"), dr.Item("oksigen"), dr.Item("kassa"), dr.Item("tindakan"), dr.Item("ventilator"),
                                       dr.Item("nebulizer"), dr.Item("syringe"), dr.Item("total"), dr.Item("tarifinacbg"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Sub tableRanap()
        Dim tglFile, tglSheet As String
        tglSheet = DateTimePicker1.Value.ToString("dd")
        tglFile = DateTimePicker1.Value.ToString("dd MMM yyyy")

        Dim DGV As New DataGridView
        With DGV
            .AllowUserToAddRows = False
            .Name = "Ranap"
            .Visible = False
            .Columns.Clear()
            .Columns.Add("No. RM", "No. RM")
            .Columns.Add("No. SEP", "No. SEP")
            .Columns.Add("Tgl. Masuk", "Tgl. Masuk")
            .Columns.Add("Tgl. Keluar", "Tgl. Keluar")
            .Columns.Add("Nama Pasien", "Nama Pasien")
            .Columns.Add("Ruang", "Ruang")
            .Columns.Add("Hak Kelas", "Hak Kelas")
            .Columns.Add("Kelas", "Kelas")
            .Columns.Add("Jml. Inap", "Jml. Inap")
            .Columns.Add("DPJP", "DPJP")
            .Columns.Add("Tarif DPJP", "Tarif DPJP")
            .Columns.Add("Dokter Operator", "Dokter Operator")
            .Columns.Add("Dokter Anestesi", "Dokter Anestesi")
            .Columns.Add("Admin", "Admin")
            .Columns.Add("Biaya Ruang", "Biaya Ruang")
            .Columns.Add("Prosedur Bedah", "Prosedur Bedah")
            .Columns.Add("Endoscopy", "Endoscopy")
            .Columns.Add("Bronchoscopy", "Bronchoscopy")
            .Columns.Add("Hemodialisa", "Hemodialisa")
            .Columns.Add("CVC", "CVC")
            .Columns.Add("IVP", "IVP")
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
                                     .Rows(i).Cells(42).Value, .Rows(i).Cells(43).Value, .Rows(i).Cells(44).Value,
                                     .Rows(i).Cells(45).Value, .Rows(i).Cells(46).Value, .Rows(i).Cells(47).Value,
                                     .Rows(i).Cells(48).Value, .Rows(i).Cells(49).Value, .Rows(i).Cells(50).Value,
                                     .Rows(i).Cells(51).Value, .Rows(i).Cells(52).Value, .Rows(i).Cells(53).Value)
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

        With dgvDokterAll
            If .Rows.Count > 0 Then
                For i As Integer = 0 To .Rows.Count - 1
                    Application.DoEvents()
                    DGVDOK.Rows.Add(.Rows(i).Cells(0).Value, .Rows(i).Cells(1).Value, .Rows(i).Cells(2).Value,
                                     .Rows(i).Cells(3).Value, .Rows(i).Cells(4).Value, .Rows(i).Cells(5).Value,
                                     .Rows(i).Cells(6).Value, .Rows(i).Cells(7).Value, .Rows(i).Cells(8).Value,
                                     .Rows(i).Cells(9).Value)
                Next
            End If
        End With

        FlNm = "C:\Eklaim\Rekap JP JKN Ranap " & tglFile & ".xml"
        'FlNm = Application.StartupPath & "\Student " _
        '        & Now.Day & "-" & Now.Month & "-" & Now.Year & ".xls"
        If File.Exists(FlNm) Then File.Delete(FlNm)
        ExToExcel(DGV, DGVDOK, FlNm)

        DGV.Dispose()
        DGVDOK.Dispose()
        DGV = Nothing
        DGVDOK = Nothing

        Process.Start("C:\Eklaim\Rekap JP JKN Ranap " & tglFile & ".xml")
    End Sub

    Sub tableRajal()
        Dim tglFile, tglSheet As String
        tglSheet = DateTimePicker1.Value.ToString("dd")
        tglFile = DateTimePicker1.Value.ToString("dd MMM yyyy")

        Dim DGV As New DataGridView
        With DGV
            .AllowUserToAddRows = False
            .Name = "Rajal"
            .Visible = False
            .Columns.Clear()
            .Columns.Add("No. RM", "No. RM")
            .Columns.Add("No. SEP", "No. SEP")
            .Columns.Add("Tgl. Masuk", "Tgl. Masuk")
            .Columns.Add("Tgl. Keluar", "Tgl. Keluar")
            .Columns.Add("Nama Pasien", "Nama Pasien")
            .Columns.Add("Poli/IGD", "Poli/IGD")
            .Columns.Add("Admin", "Admin")
            .Columns.Add("Endoscopy", "Endoscopy")
            .Columns.Add("Bronchoscopy", "Bronchoscopy")
            .Columns.Add("Hemodialisa", "Hemodialisa")
            .Columns.Add("CVC", "CVC")
            .Columns.Add("IVP", "IVP")
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
            .Columns.Add("Obat", "Obat")
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

        With dgvDokterAll
            If .Rows.Count > 0 Then
                For i As Integer = 0 To .Rows.Count - 1
                    Application.DoEvents()
                    DGVDOK.Rows.Add(.Rows(i).Cells(0).Value, .Rows(i).Cells(1).Value, .Rows(i).Cells(2).Value,
                                     .Rows(i).Cells(3).Value, .Rows(i).Cells(4).Value, .Rows(i).Cells(5).Value,
                                     .Rows(i).Cells(6).Value, .Rows(i).Cells(7).Value, .Rows(i).Cells(8).Value,
                                     .Rows(i).Cells(9).Value)
                Next
            End If
        End With

        FlNm = "C:\Eklaim\Rekap JP JKN Rajal " & tglFile & ".xml"
        'FlNm = Application.StartupPath & "\Student " _
        '        & Now.Day & "-" & Now.Month & "-" & Now.Year & ".xls"
        If File.Exists(FlNm) Then File.Delete(FlNm)
        ExToExcel(DGV, DGVDOK, FlNm)

        DGV.Dispose()
        DGVDOK.Dispose()
        DGV = Nothing
        DGVDOK = Nothing

        Process.Start("C:\Eklaim\Rekap JP JKN Rajal " & tglFile & ".xml")
    End Sub

    Private Sub RekapPiutangUmum_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        btnUmum.BackColor = Color.FromArgb(209, 232, 223)
        PicCollapse.Visible = False

        TableLayoutPanel2.RowStyles(3).SizeType = SizeType.Percent
        TableLayoutPanel2.RowStyles(3).Height = 0

        txtUser.Text = Home.txtUser.Text
        DateTimePicker1.Value = DateTime.Now
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "dd MMM yyyy"
        txtFilter.SelectedIndex = 0
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
        Dim btn As Button = CType(sender, Button)
        setColor(btn)
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
        If txtFilter.Text = "ALL" Then

        ElseIf txtFilter.Text = "Rawat Jalan" Then
            dgvRajal.Visible = True
            dgvRanap.Visible = False
            Call DaftarPiutangRajal()
        ElseIf txtFilter.Text = "Rawat Inap" Then
            dgvRajal.Visible = False
            dgvRanap.Visible = True
            Call DaftarPiutangRanap()
        End If

        Call DaftarDokterAll()
    End Sub

    Private Sub dgvRanap_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvRanap.CellFormatting
        For i As Integer = 14 To dgvRanap.Columns.Count - 1
            dgvRanap.Columns(i).DefaultCellStyle.Format = "N0"
        Next

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

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        'For i As Integer = 14 To DataGridView1.Columns.Count - 1
        '    DataGridView1.Columns(i).DefaultCellStyle.Format = "N0"
        'Next

        DataGridView1.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        DataGridView1.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
        DataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black
        DataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Private Sub dgvRanap_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvRanap.RowPostPaint
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

    Private Sub DataGridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
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

    Private Sub dgvRanap_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvRanap.CellMouseClick
        If e.RowIndex = -1 Then
            Return
        End If

        noRm = dgvRanap.Rows(e.RowIndex).Cells(0).Value
        pasien = dgvRanap.Rows(e.RowIndex).Cells(4).Value

        If e.ColumnIndex = 51 Then
            If TableLayoutPanel2.RowStyles(3).Height = 35 Then
                TableLayoutPanel2.RowStyles(3).SizeType = SizeType.Percent
                TableLayoutPanel2.RowStyles(3).Height = 0
            Else
                TableLayoutPanel2.RowStyles(3).SizeType = SizeType.Percent
                TableLayoutPanel2.RowStyles(3).Height = 35
            End If
        End If

        Label4.Text = "Visite Dokter Pasien a.n. " & pasien
        Call DaftarDokter()
    End Sub

    Private Sub dgvRanap_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvRanap.CellMouseDoubleClick
        If e.RowIndex = -1 Then
            Return
        End If

        noRm = dgvRanap.Rows(e.RowIndex).Cells(0).Value
        pasien = dgvRanap.Rows(e.RowIndex).Cells(4).Value

        If TableLayoutPanel2.RowStyles(3).Height = 35 Then
            TableLayoutPanel2.RowStyles(3).SizeType = SizeType.Percent
            TableLayoutPanel2.RowStyles(3).Height = 0
        Else
            TableLayoutPanel2.RowStyles(3).SizeType = SizeType.Percent
            TableLayoutPanel2.RowStyles(3).Height = 35
        End If


        Label4.Text = "Visite Dokter Pasien a.n. " & pasien
        Call DaftarDokter()
    End Sub
End Class