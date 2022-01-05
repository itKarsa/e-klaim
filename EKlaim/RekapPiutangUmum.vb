Imports System.IO
Imports MySql.Data.MySqlClient
Imports Excel = Microsoft.Office.Interop.Excel
Public Class RekapPiutangUmum

    Dim noRm, pasien As String
    Dim FlNm As String
    Dim lastIndexRj As Integer
    Dim lastIndexRi As Integer
    Dim lastIndexDok As Integer
    Dim lastIndexPerpx As Integer
    Dim ci As IFormatProvider = New System.Globalization.CultureInfo("id-ID", True)

    Sub setColor(button As Button)
        btnHome.BackColor = Color.White
        btnEklaim.BackColor = Color.White
        btnBuku.BackColor = Color.White
        btnPiutang.BackColor = Color.White
        btnUmum.BackColor = Color.White
        btnTotal.BackColor = Color.White
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

    Sub DaftarAsalPx(norm As String, mrs As Date)
        Call koneksiJepe()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        query = "SELECT * FROM t_eklaimjprajalumum 
                  WHERE noRM = '" & norm & "' AND (SUBSTR(tglMasuk,1,10) BETWEEN '" & Format(DateAdd(DateInterval.Day, -1, mrs), "yyyy-MM-dd") & "' AND '" & Format(DateAdd(DateInterval.Day, 1, mrs), "yyyy-MM-dd") & "')"

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            'dgvRanap.Rows.Clear()
            Do While dr.Read
                dgvRanap.Rows.Add("-", "-", dr.Item("tglMasuk"), dr.Item("tglKeluar"), dr.Item("namaPasien"),
                                  dr.Item("unit"), "-", "-", "-", dr.Item("dokter"),
                                  "-", "0", "-", "-", dr.Item("drOperator"),
                                  dr.Item("drAnestesi"), dr.Item("admin"), "0", "0", dr.Item("prosedurbedah"),
                                  dr.Item("endoscopy"), dr.Item("bronkoscopy"), dr.Item("hd"), dr.Item("cvc"), dr.Item("ivp"),
                                  dr.Item("paru"), dr.Item("nonbedahlain"), dr.Item("gizi"), "0", dr.Item("fisioterapi"),
                                  dr.Item("tindakan"), "0", "0", dr.Item("ecg"), dr.Item("holter"),
                                  dr.Item("echocardio"), dr.Item("usg"), dr.Item("rontgen"), dr.Item("ctscan"), dr.Item("mri"),
                                  dr.Item("labpa"), dr.Item("labpk"), dr.Item("darah"), "0", "0",
                                  "0", "0", "0", dr.Item("obat"), dr.Item("alkes"),
                                  dr.Item("oksigen"), dr.Item("kassa"), "0", dr.Item("ventilator"), dr.Item("nebulizer"),
                                  dr.Item("syringe"), "0",
                                  Val(dr.Item("admin") + dr.Item("prosedurbedah") + dr.Item("endoscopy") + dr.Item("bronkoscopy") +
                                      dr.Item("hd") + dr.Item("cvc") + dr.Item("ivp") + dr.Item("paru") + dr.Item("nonbedahlain") +
                                      dr.Item("gizi") + dr.Item("fisioterapi") + dr.Item("tindakan") + dr.Item("ecg") +
                                      dr.Item("holter") + dr.Item("echocardio") + dr.Item("usg") + dr.Item("rontgen") +
                                      dr.Item("ctscan") + dr.Item("mri") + dr.Item("labpa") + dr.Item("labpk") +
                                      dr.Item("darah") + dr.Item("obat") + dr.Item("alkes") + dr.Item("oksigen") +
                                      dr.Item("kassa") + dr.Item("ventilator") + dr.Item("nebulizer") + dr.Item("syringe")), "0")

                Call DaftarDokterAsalPx(dr.Item("noRM"), dr.Item("unit"), dr.Item("tglMasuk"), dr.Item("tglKeluar"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Sub DaftarDokterAsalPx(norm As String, unit As String, mrs As Date, krs As Date)
        Call koneksiJepe()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        query = "SELECT *
                   FROM t_eklaimjpdokterrajalumum
                  WHERE noRM = '" & norm & "' 
                    AND unit = '" & unit & "'
                    AND tglMasuk = '" & Format(mrs, "yyyy-MM-dd HH:mm:ss") & "'
                    AND tglKeluar = '" & Format(krs, "yyyy-MM-dd HH:mm:ss") & "'"

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            'dgvDokterAll.Rows.Clear()
            Do While dr.Read
                dgvRanap.Rows.Add("-", "-", dr.Item("tglMasuk"), dr.Item("tglKeluar"), dr.Item("namaPasien"),
                                  dr.Item("unit"), "-", "-", "-", "-",
                                  "-", dr.Item("jmlVisite"), dr.Item("drVisite"), dr.Item("drKonsul"), "-",
                                  "-", "0", "0", dr.Item("jasaVisite"), "0",
                                  "0", "0", "0", "0", "0",
                                  "0", "0", "0", "0", "0",
                                  "0", "0", "0", "0", "0",
                                  "0", "0", "0", "0", "0",
                                  "0", "0", "0", "0", "0",
                                  "0", "0", "0", "0", "0",
                                  "0", "0", "0", "0", "0",
                                  "0", "0", dr.Item("jasaVisite"), "0", "0")
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Sub DaftarDokterAll(norm As String, krs As Date)
        Call koneksiJepe()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        If txtFilter.Text = "Rawat Jalan" Then
            query = "SELECT *
                   FROM t_eklaimjpdokterrajalumum
                  WHERE noRM = '" & norm & "' AND (SUBSTR(tglKeluar,1,10)) = '" & Format(krs, "yyyy-MM-dd") & "'"
        ElseIf txtFilter.Text = "Rawat Inap" Then
            query = "SELECT *
                   FROM t_eklaimjpdokterranapumum
                  WHERE noRM = '" & norm & "' AND (SUBSTR(tglKeluar,1,10)) = '" & Format(krs, "yyyy-MM-dd") & "'"
        End If

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            'dgvDokterAll.Rows.Clear()
            Do While dr.Read
                If txtFilter.Text = "Rawat Jalan" Then
                    dgvRajal.Rows.Add("-", "-", dr.Item("tglMasuk"), dr.Item("tglKeluar"), dr.Item("namaPasien"),
                                      "-", "-", "-", "-",
                                      dr.Item("drVisite"), dr.Item("drKonsul"), "0", dr.Item("jasaVisite"),
                                      "0", "0", "0", "0", "0",
                                      "0", "0", "0", "0", "0",
                                      "0", "0", "0", "0", "0",
                                      "0", "0", "0", "0", "0",
                                      "0", "0", "0", "0", "0",
                                      "0", "0", "0", "0", dr.Item("jasaVisite"),
                                      "0")
                ElseIf txtFilter.Text = "Rawat Inap" Then
                    dgvRanap.Rows.Add("-", "-", dr.Item("tglMasuk"), dr.Item("tglKeluar"), dr.Item("namaPasien"),
                                      "-", "-", "-", "-", "-",
                                      "-", dr.Item("jmlVisite"), dr.Item("drVisite"), dr.Item("drKonsul"), "-",
                                      "-", "0", "0", dr.Item("jasaVisite"), "0",
                                      "0", "0", "0", "0", "0",
                                      "0", "0", "0", "0", "0",
                                      "0", "0", "0", "0", "0",
                                      "0", "0", "0", "0", "0",
                                      "0", "0", "0", "0", "0",
                                      "0", "0", "0", "0", "0",
                                      "0", "0", "0", "0", "0",
                                      "0", "0", dr.Item("jasaVisite"), "0", "0")
                End If
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
                                  dr.Item("tarifdpjp"), "0", "0", "0", dr.Item("drOperator"),
                                  dr.Item("drAnestesi"), dr.Item("akomodasiAdmin"), dr.Item("akomodasiRuang"), "0", dr.Item("prosedurbedah"),
                                  dr.Item("endoscopy"), dr.Item("bronkoscopy"), dr.Item("hd"), dr.Item("cvc"), dr.Item("ivp"),
                                  dr.Item("paru"), dr.Item("nonbedahlain"), dr.Item("gizi"), dr.Item("farklin"), dr.Item("fisio"),
                                  dr.Item("tindakan"), dr.Item("askep"), dr.Item("kerohanian"), dr.Item("ecg"), dr.Item("holter"),
                                  dr.Item("echocardio"), dr.Item("usg"), dr.Item("rontgen"), dr.Item("ctscan"), dr.Item("mri"),
                                  dr.Item("labpa"), dr.Item("labpk"), dr.Item("darah"), dr.Item("rehab"), dr.Item("icu"),
                                  dr.Item("picu"), dr.Item("nicu"), dr.Item("hcu"), dr.Item("obat"), dr.Item("alkes"),
                                  dr.Item("oksigen"), dr.Item("kassa"), dr.Item("jenazah"), dr.Item("ventilator"), dr.Item("nebulizer"),
                                  dr.Item("syringe"), dr.Item("bedsetmonitor"), dr.Item("total"), dr.Item("tarifinacbg"))
                Call DaftarDokterAll(dr.Item("noRM").ToString, dr.Item("tglKeluar"))
                Call DaftarAsalPx(dr.Item("noRM").ToString, dr.Item("tglMasuk"))
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
                                  dr.Item("unit"), dr.Item("dokter"), dr.Item("drOperator"),
                                  dr.Item("drAnestesi"), dr.Item("visite"), dr.Item("konsultasi"), dr.Item("admin"),
                                  "0", dr.Item("prosedurbedah"), dr.Item("endoscopy"), dr.Item("bronkoscopy"), dr.Item("hd"), dr.Item("cvc"),
                                  dr.Item("ivp"), dr.Item("paru"), dr.Item("nonbedahlain"), dr.Item("gizi"), dr.Item("fisioterapi"),
                                  dr.Item("ecg"), dr.Item("holter"), dr.Item("treadmill"), dr.Item("echocardio"), dr.Item("usg"),
                                  dr.Item("rontgen"), dr.Item("ctscan"), dr.Item("mri"), dr.Item("labpa"), dr.Item("labpk"), dr.Item("darah"),
                                  dr.Item("obat"), dr.Item("alkes"), dr.Item("oksigen"), dr.Item("kassa"), dr.Item("tindakan"), dr.Item("ventilator"),
                                  dr.Item("nebulizer"), dr.Item("syringe"), dr.Item("total"), dr.Item("tarifinacbg"))
                Call DaftarDokterAll(dr.Item("noRM").ToString, dr.Item("tglKeluar"))
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
            .Columns.Add("Jml. Visite", "Jml. Visite")
            .Columns.Add("Visite", "Visite")
            .Columns.Add("Konsultasi", "Konsultasi")
            .Columns.Add("Dokter Operator", "Dokter Operator")
            .Columns.Add("Dokter Anestesi", "Dokter Anestesi")
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
                                     .Rows(i).Cells(42).Value, .Rows(i).Cells(43).Value, .Rows(i).Cells(44).Value,
                                     .Rows(i).Cells(45).Value, .Rows(i).Cells(46).Value, .Rows(i).Cells(47).Value,
                                     .Rows(i).Cells(48).Value, .Rows(i).Cells(49).Value, .Rows(i).Cells(50).Value,
                                     .Rows(i).Cells(51).Value, .Rows(i).Cells(52).Value, .Rows(i).Cells(53).Value,
                                     .Rows(i).Cells(54).Value, .Rows(i).Cells(55).Value, .Rows(i).Cells(56).Value,
                                     .Rows(i).Cells(57).Value, .Rows(i).Cells(58).Value)
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

        FlNm = "C:\Eklaim\Rekap JP UMUM Ranap " & tglFile & ".xml"
        'FlNm = Application.StartupPath & "\Student " _
        '        & Now.Day & "-" & Now.Month & "-" & Now.Year & ".xls"
        If File.Exists(FlNm) Then File.Delete(FlNm)
        ExToExcel(DGV, DGVDOK, FlNm)

        DGV.Dispose()
        DGVDOK.Dispose()
        DGV = Nothing
        DGVDOK = Nothing

        Process.Start("C:\Eklaim\Rekap JP UMUM Ranap " & tglFile & ".xml")
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
            .Columns.Add("Dokter", "Dokter")
            .Columns.Add("Visite", "Visite")
            .Columns.Add("Konsultasi", "Konsultasi")
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
                                .Rows(i).Cells(33).Value, .Rows(i).Cells(34).Value, .Rows(i).Cells(35).Value,
                                .Rows(i).Cells(36).Value, .Rows(i).Cells(37).Value, .Rows(i).Cells(38).Value,
                                .Rows(i).Cells(39).Value, .Rows(i).Cells(40).Value, .Rows(i).Cells(41).Value)
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

        FlNm = "C:\Eklaim\Rekap JP UMUM Rajal " & tglFile & ".xml"
        'FlNm = Application.StartupPath & "\Student " _
        '        & Now.Day & "-" & Now.Month & "-" & Now.Year & ".xls"
        If File.Exists(FlNm) Then File.Delete(FlNm)
        ExToExcel(DGV, DGVDOK, FlNm)

        DGV.Dispose()
        DGVDOK.Dispose()
        DGV = Nothing
        DGVDOK = Nothing

        Process.Start("C:\Eklaim\Rekap JP UMUM Rajal " & tglFile & ".xml")
    End Sub

    Sub CekTglRekap()
        Call koneksiJepe()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim val As String = ""

        If txtFilter.Text = "Rawat Jalan" Then
            query = "SELECT IF(COUNT(tglRekap) = 0,NULL,tglRekap) AS tglRekap
                   FROM t_rekaptotalharianrajalumum
                  WHERE (SUBSTR(tglRekap,1,10)) = '" & Format(CDate(DateTimePicker1.Value), "yyyy-MM-dd") & "'"
        ElseIf txtFilter.Text = "Rawat Inap" Then
            query = "SELECT IF(COUNT(tglRekap) = 0,NULL,tglRekap) AS tglRekap
                   FROM t_rekaptotalharianranapumum
                  WHERE (SUBSTR(tglRekap,1,10)) = '" & Format(CDate(DateTimePicker1.Value), "yyyy-MM-dd") & "'"
        End If

        Try
            'MsgBox(query)
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                'val = dr.Item("tglRekap").ToString
                'MsgBox(val)
                If dr.IsDBNull(0) Then
                    'MsgBox("total Rekap tanggal " & Format(CDate(DateTimePicker1.Value), "dd/MM/yyyy"))
                    If txtFilter.Text = "Rawat Jalan" Then
                        Call simpanTotalHarianRajal()
                    ElseIf txtFilter.Text = "Rawat Inap" Then
                        Call simpanTotalHarianRanap()
                    End If
                Else
                    Dim konfirmasi As MsgBoxResult
                    konfirmasi = MsgBox("Apakah anda ingin meng-update total rekap tanggal " & Format(CDate(dr.Item("tglRekap").ToString), "dd/MM/yyyy") & " ?", vbQuestion + vbYesNo, "Konfirmasi")
                    If konfirmasi = vbYes Then
                        If txtFilter.Text = "Rawat Jalan" Then
                            Call updateTotalHarianRajal()
                        ElseIf txtFilter.Text = "Rawat Inap" Then
                            Call updateTotalHarianRanap()
                        End If
                    End If
                End If
            End If
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Sub simpanTotalHarianRanap()
        Call koneksiJepe()
        Dim strRkp As String = ""
        Dim cmdRkp As MySqlCommand

        strRkp = "INSERT INTO t_rekaptotalharianranap(tglRekap,akomodasiAdmin,akomodasiRuang,jasaVisitKonsul,prosedurbedah,
                                                      endoscopy,bronkoscopy,hd,cvc,ivp,
                                                      paru,nonbedahlain,gizi,farklin,fisio,
                                                      tindakan,askep,kerohanian,ecg,holter,
                                                      echocardio,usg,rontgen,ctscan,mri,
                                                      labpa,labpk,darah,rehab,icu,
                                                      picu,nicu,hcu,obat,alkes,
                                                      oksigen,kassa,jenazah,ventilator,nebulizer,
                                                      syringe,bedsetmonitor,total,tarifinacbg)
                                              VALUES ('" & Format(CDate(DateTimePicker1.Value), "yyyy-MM-dd") & "',@akomodasiAdmin,@akomodasiRuang,@jasaVisitKonsul,@prosedurbedah,
                                                      @endoscopy,@bronkoscopy,@hd,@cvc,@ivp,
                                                      @paru,@nonbedahlain,@gizi,@farklin,@fisio,
                                                      @tindakan,@askep,@kerohanian,@ecg,@holter,
                                                      @echocardio,@usg,@rontgen,@ctscan,@mri,
                                                      @labpa,@labpk,@darah,@rehab,@icu,
                                                      @picu,@nicu,@hcu,@obat,@alkes,
                                                      @oksigen,@kassa,@jenazah,@ventilator,@nebulizer,
                                                      @syringe, @bedsetmonitor,@total,@tarifinacbg)"

        cmdRkp = New MySqlCommand(strRkp, conn)
        'MsgBox(dgvRanap.Rows(lastIndexRi).Cells(4).Value.ToString)

        Try
            cmdRkp.Parameters.AddWithValue("@akomodasiAdmin", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(16).Value, ci))
            cmdRkp.Parameters.AddWithValue("@akomodasiRuang", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(17).Value, ci))
            cmdRkp.Parameters.AddWithValue("@jasaVisitKonsul", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(18).Value, ci))
            cmdRkp.Parameters.AddWithValue("@prosedurbedah", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(19).Value, ci))
            cmdRkp.Parameters.AddWithValue("@endoscopy", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(20).Value, ci))
            cmdRkp.Parameters.AddWithValue("@bronkoscopy", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(21).Value, ci))
            cmdRkp.Parameters.AddWithValue("@hd", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(22).Value, ci))
            cmdRkp.Parameters.AddWithValue("@cvc", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(23).Value, ci))
            cmdRkp.Parameters.AddWithValue("@ivp", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(24).Value, ci))
            cmdRkp.Parameters.AddWithValue("@paru", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(25).Value, ci))
            cmdRkp.Parameters.AddWithValue("@nonbedahlain", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(26).Value, ci))
            cmdRkp.Parameters.AddWithValue("@gizi", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(27).Value, ci))
            cmdRkp.Parameters.AddWithValue("@farklin", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(28).Value, ci))
            cmdRkp.Parameters.AddWithValue("@fisio", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(29).Value, ci))
            cmdRkp.Parameters.AddWithValue("@tindakan", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(30).Value, ci))
            cmdRkp.Parameters.AddWithValue("@askep", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(31).Value, ci))
            cmdRkp.Parameters.AddWithValue("@kerohanian", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(32).Value, ci))
            cmdRkp.Parameters.AddWithValue("@ecg", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(33).Value, ci))
            cmdRkp.Parameters.AddWithValue("@holter", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(34).Value, ci))
            cmdRkp.Parameters.AddWithValue("@echocardio", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(35).Value, ci))
            cmdRkp.Parameters.AddWithValue("@usg", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(36).Value, ci))
            cmdRkp.Parameters.AddWithValue("@rontgen", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(37).Value, ci))
            cmdRkp.Parameters.AddWithValue("@ctscan", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(38).Value, ci))
            cmdRkp.Parameters.AddWithValue("@mri", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(39).Value, ci))
            cmdRkp.Parameters.AddWithValue("@labpa", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(40).Value, ci))
            cmdRkp.Parameters.AddWithValue("@labpk", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(41).Value, ci))
            cmdRkp.Parameters.AddWithValue("@darah", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(42).Value, ci))
            cmdRkp.Parameters.AddWithValue("@rehab", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(43).Value, ci))
            cmdRkp.Parameters.AddWithValue("@icu", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(44).Value, ci))
            cmdRkp.Parameters.AddWithValue("@picu", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(45).Value, ci))
            cmdRkp.Parameters.AddWithValue("@nicu", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(46).Value, ci))
            cmdRkp.Parameters.AddWithValue("@hcu", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(47).Value, ci))
            cmdRkp.Parameters.AddWithValue("@obat", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(48).Value, ci))
            cmdRkp.Parameters.AddWithValue("@alkes", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(49).Value, ci))
            cmdRkp.Parameters.AddWithValue("@oksigen", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(50).Value, ci))
            cmdRkp.Parameters.AddWithValue("@kassa", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(51).Value, ci))
            cmdRkp.Parameters.AddWithValue("@jenazah", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(52).Value, ci))
            cmdRkp.Parameters.AddWithValue("@ventilator", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(53).Value, ci))
            cmdRkp.Parameters.AddWithValue("@nebulizer", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(54).Value, ci))
            cmdRkp.Parameters.AddWithValue("@syringe", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(55).Value, ci))
            cmdRkp.Parameters.AddWithValue("@bedsetmonitor", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(56).Value, ci))
            cmdRkp.Parameters.AddWithValue("@total", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(57).Value, ci))
            cmdRkp.Parameters.AddWithValue("@tarifinacbg", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(58).Value, ci))
            cmdRkp.ExecuteNonQuery()
            cmdRkp.Parameters.Clear()

            MessageBox.Show("Data total rekap harian berhasil tersimpan", "Insert Data Succes", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error simpan rekap harian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        conn.Close()
    End Sub

    Sub simpanTotalHarianRajal()
        Call koneksiJepe()
        Dim strRkp As String = ""
        Dim cmdRkp As MySqlCommand

        strRkp = "INSERT INTO t_rekaptotalharianrajalumum(tglRekap,admin,jasaVisitKonsul,prosedurbedah,
                                                          endoscopy,bronkoscopy,hd,cvc,ivp,
                                                          paru,nonbedahlain,gizi,fisioterapi,ecg,
                                                          holter,treadmill,echocardio,usg,rontgen,
                                                          ctscan,mri,labpa,labpk,darah,
                                                          obat,alkes,oksigen,kassa,tindakan,
                                                          ventilator,nebulizer,syringe,total,tarifinacbg)
                                                  VALUES ('" & Format(CDate(DateTimePicker1.Value), "yyyy-MM-dd") & "',@admin,@jasaVisitKonsul,@prosedurbedah,
                                                          @endoscopy,@bronkoscopy,@hd,@cvc,@ivp,
                                                          @paru,@nonbedahlain,@gizi,@fisioterapi,@ecg,
                                                          @holter,@treadmill,@echocardio,@usg,@rontgen,
                                                          @ctscan,@mri,@labpa,@labpk,@darah,
                                                          @obat,@alkes,@oksigen,@kassa,@tindakan,
                                                          @ventilator,@nebulizer,@syringe,@total,@tarifinacbg)"

        cmdRkp = New MySqlCommand(strRkp, conn)
        'MsgBox(dgvRanap.Rows(lastIndexRi).Cells(4).Value.ToString)

        Try
            cmdRkp.Parameters.AddWithValue("@admin", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(11).Value, ci))
            cmdRkp.Parameters.AddWithValue("@jasaVisitKonsul", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(12).Value, ci))
            cmdRkp.Parameters.AddWithValue("@prosedurbedah", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(13).Value, ci))
            cmdRkp.Parameters.AddWithValue("@endoscopy", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(14).Value, ci))
            cmdRkp.Parameters.AddWithValue("@bronkoscopy", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(15).Value, ci))
            cmdRkp.Parameters.AddWithValue("@hd", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(16).Value, ci))
            cmdRkp.Parameters.AddWithValue("@cvc", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(17).Value, ci))
            cmdRkp.Parameters.AddWithValue("@ivp", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(18).Value, ci))
            cmdRkp.Parameters.AddWithValue("@paru", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(19).Value, ci))
            cmdRkp.Parameters.AddWithValue("@nonbedahlain", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(20).Value, ci))
            cmdRkp.Parameters.AddWithValue("@gizi", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(21).Value, ci))
            cmdRkp.Parameters.AddWithValue("@fisioterapi", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(22).Value, ci))
            cmdRkp.Parameters.AddWithValue("@ecg", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(23).Value, ci))
            cmdRkp.Parameters.AddWithValue("@holter", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(24).Value, ci))
            cmdRkp.Parameters.AddWithValue("@treadmill", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(25).Value, ci))
            cmdRkp.Parameters.AddWithValue("@echocardio", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(26).Value, ci))
            cmdRkp.Parameters.AddWithValue("@usg", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(27).Value, ci))
            cmdRkp.Parameters.AddWithValue("@rontgen", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(28).Value, ci))
            cmdRkp.Parameters.AddWithValue("@ctscan", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(29).Value, ci))
            cmdRkp.Parameters.AddWithValue("@mri", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(30).Value, ci))
            cmdRkp.Parameters.AddWithValue("@labpa", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(31).Value, ci))
            cmdRkp.Parameters.AddWithValue("@labpk", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(32).Value, ci))
            cmdRkp.Parameters.AddWithValue("@darah", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(33).Value, ci))
            cmdRkp.Parameters.AddWithValue("@obat", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(34).Value, ci))
            cmdRkp.Parameters.AddWithValue("@alkes", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(35).Value, ci))
            cmdRkp.Parameters.AddWithValue("@oksigen", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(36).Value, ci))
            cmdRkp.Parameters.AddWithValue("@kassa", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(37).Value, ci))
            cmdRkp.Parameters.AddWithValue("@tindakan", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(38).Value, ci))
            cmdRkp.Parameters.AddWithValue("@ventilator", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(39).Value, ci))
            cmdRkp.Parameters.AddWithValue("@nebulizer", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(40).Value, ci))
            cmdRkp.Parameters.AddWithValue("@syringe", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(41).Value, ci))
            cmdRkp.Parameters.AddWithValue("@total", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(42).Value, ci))
            cmdRkp.Parameters.AddWithValue("@tarifinacbg", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(43).Value, ci))
            cmdRkp.ExecuteNonQuery()
            cmdRkp.Parameters.Clear()

            MessageBox.Show("Data total rekap harian berhasil tersimpan", "Insert Data Succes", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error simpan rekap harian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        conn.Close()
    End Sub

    Sub updateTotalHarianRanap()
        Call koneksiJepe()
        Dim strRkp As String = ""
        Dim cmdRkp As MySqlCommand

        strRkp = "UPDATE t_rekaptotalharianranapumum SET akomodasiAdmin = @akomodasiAdmin, akomodasiRuang = @akomodasiRuang, jasaVisitKonsul = @jasaVisitKonsul, prosedurbedah = @prosedurbedah,
                                                         endoscopy = @endoscopy, bronkoscopy = @bronkoscopy, hd = @hd,
                                                         cvc = @cvc, ivp = @ivp, paru = @paru,
                                                         nonbedahlain = @nonbedahlain, gizi = @gizi, farklin = @farklin,
                                                         fisio = @fisio, tindakan = @tindakan, askep = @askep,
                                                         kerohanian = @kerohanian, ecg = @ecg, holter = @holter,
                                                         echocardio = @echocardio, usg = @usg, rontgen = @rontgen,
                                                         ctscan = @ctscan, mri = @mri, labpa = @labpa,
                                                         labpk = @labpk, darah = @darah, rehab = @rehab,
                                                         icu = @icu, picu = @picu, nicu = @nicu,
                                                         hcu = @hcu, obat = @obat, alkes = @alkes,
                                                         oksigen = @oksigen, kassa = @kassa, jenazah = @jenazah,
                                                         ventilator = @ventilator, nebulizer = @nebulizer, syringe = @syringe,
                                                         bedsetmonitor = @bedsetmonitor, total = @total, tarifinacbg = @tarifinacbg
                                                   WHERE tglRekap = '" & Format(CDate(DateTimePicker1.Value), "yyyy-MM-dd") & "'"

        cmdRkp = New MySqlCommand(strRkp, conn)
        'MsgBox(dgvRanap.Rows(lastIndexRi).Cells(4).Value.ToString)

        Try
            cmdRkp.Parameters.AddWithValue("@akomodasiAdmin", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(16).Value, ci))
            cmdRkp.Parameters.AddWithValue("@akomodasiRuang", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(17).Value, ci))
            cmdRkp.Parameters.AddWithValue("@jasaVisitKonsul", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(18).Value, ci))
            cmdRkp.Parameters.AddWithValue("@prosedurbedah", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(19).Value, ci))
            cmdRkp.Parameters.AddWithValue("@endoscopy", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(20).Value, ci))
            cmdRkp.Parameters.AddWithValue("@bronkoscopy", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(21).Value, ci))
            cmdRkp.Parameters.AddWithValue("@hd", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(22).Value, ci))
            cmdRkp.Parameters.AddWithValue("@cvc", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(23).Value, ci))
            cmdRkp.Parameters.AddWithValue("@ivp", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(24).Value, ci))
            cmdRkp.Parameters.AddWithValue("@paru", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(25).Value, ci))
            cmdRkp.Parameters.AddWithValue("@nonbedahlain", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(26).Value, ci))
            cmdRkp.Parameters.AddWithValue("@gizi", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(27).Value, ci))
            cmdRkp.Parameters.AddWithValue("@farklin", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(28).Value, ci))
            cmdRkp.Parameters.AddWithValue("@fisio", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(29).Value, ci))
            cmdRkp.Parameters.AddWithValue("@tindakan", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(30).Value, ci))
            cmdRkp.Parameters.AddWithValue("@askep", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(31).Value, ci))
            cmdRkp.Parameters.AddWithValue("@kerohanian", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(32).Value, ci))
            cmdRkp.Parameters.AddWithValue("@ecg", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(33).Value, ci))
            cmdRkp.Parameters.AddWithValue("@holter", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(34).Value, ci))
            cmdRkp.Parameters.AddWithValue("@echocardio", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(35).Value, ci))
            cmdRkp.Parameters.AddWithValue("@usg", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(36).Value, ci))
            cmdRkp.Parameters.AddWithValue("@rontgen", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(37).Value, ci))
            cmdRkp.Parameters.AddWithValue("@ctscan", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(38).Value, ci))
            cmdRkp.Parameters.AddWithValue("@mri", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(39).Value, ci))
            cmdRkp.Parameters.AddWithValue("@labpa", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(40).Value, ci))
            cmdRkp.Parameters.AddWithValue("@labpk", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(41).Value, ci))
            cmdRkp.Parameters.AddWithValue("@darah", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(42).Value, ci))
            cmdRkp.Parameters.AddWithValue("@rehab", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(43).Value, ci))
            cmdRkp.Parameters.AddWithValue("@icu", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(44).Value, ci))
            cmdRkp.Parameters.AddWithValue("@picu", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(45).Value, ci))
            cmdRkp.Parameters.AddWithValue("@nicu", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(46).Value, ci))
            cmdRkp.Parameters.AddWithValue("@hcu", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(47).Value, ci))
            cmdRkp.Parameters.AddWithValue("@obat", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(48).Value, ci))
            cmdRkp.Parameters.AddWithValue("@alkes", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(49).Value, ci))
            cmdRkp.Parameters.AddWithValue("@oksigen", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(50).Value, ci))
            cmdRkp.Parameters.AddWithValue("@kassa", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(51).Value, ci))
            cmdRkp.Parameters.AddWithValue("@jenazah", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(52).Value, ci))
            cmdRkp.Parameters.AddWithValue("@ventilator", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(53).Value, ci))
            cmdRkp.Parameters.AddWithValue("@nebulizer", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(54).Value, ci))
            cmdRkp.Parameters.AddWithValue("@syringe", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(55).Value, ci))
            cmdRkp.Parameters.AddWithValue("@bedsetmonitor", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(56).Value, ci))
            cmdRkp.Parameters.AddWithValue("@total", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(57).Value, ci))
            cmdRkp.Parameters.AddWithValue("@tarifinacbg", Convert.ToDouble(dgvRanap.Rows(lastIndexRi).Cells(58).Value, ci))
            cmdRkp.ExecuteNonQuery()
            cmdRkp.Parameters.Clear()

            MessageBox.Show("Update total rekap harian berhasil tersimpan", "Insert Data Succes", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error update rekap harian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        conn.Close()
    End Sub

    Sub updateTotalHarianRajal()
        Call koneksiJepe()
        Dim strRkp As String = ""
        Dim cmdRkp As MySqlCommand

        strRkp = "UPDATE t_rekaptotalharianrajalumum SET admin = @admin,jasaVisitKonsul = @jasaVisitKonsul,
                                                         prosedurbedah = @prosedurbedah,endoscopy = @endoscopy,bronkoscopy = @bronkoscopy,
                                                         hd = @hd,cvc = @cvc,ivp = @ivp,
                                                         paru = @paru,nonbedahlain = @nonbedahlain,gizi = @gizi,
                                                         fisioterapi = @fisioterapi,ecg = @ecg,holter = @holter,
                                                         treadmill = @treadmill,echocardio = @echocardio,usg = @usg,
                                                         rontgen = @rontgen,ctscan = @ctscan,mri = @mri,
                                                         labpa = @labpa,labpk = @labpk,darah = @darah,
                                                         obat = @obat,alkes = @alkes,oksigen = @oksigen,
                                                         kassa = @kassa,tindakan = @tindakan,ventilator = @ventilator,
                                                         nebulizer = @nebulizer,syringe = @syringe,total = @total,
                                                         tarifinacbg = @tarifinacbg
                                                   WHERE tglRekap = '" & Format(CDate(DateTimePicker1.Value), "yyyy-MM-dd") & "'"

        cmdRkp = New MySqlCommand(strRkp, conn)
        'MsgBox(dgvRanap.Rows(lastIndexRi).Cells(4).Value.ToString)

        Try
            cmdRkp.Parameters.AddWithValue("@admin", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(11).Value, ci))
            cmdRkp.Parameters.AddWithValue("@jasaVisitKonsul", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(12).Value, ci))
            cmdRkp.Parameters.AddWithValue("@prosedurbedah", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(13).Value, ci))
            cmdRkp.Parameters.AddWithValue("@endoscopy", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(14).Value, ci))
            cmdRkp.Parameters.AddWithValue("@bronkoscopy", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(15).Value, ci))
            cmdRkp.Parameters.AddWithValue("@hd", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(16).Value, ci))
            cmdRkp.Parameters.AddWithValue("@cvc", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(17).Value, ci))
            cmdRkp.Parameters.AddWithValue("@ivp", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(18).Value, ci))
            cmdRkp.Parameters.AddWithValue("@paru", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(19).Value, ci))
            cmdRkp.Parameters.AddWithValue("@nonbedahlain", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(20).Value, ci))
            cmdRkp.Parameters.AddWithValue("@gizi", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(21).Value, ci))
            cmdRkp.Parameters.AddWithValue("@fisioterapi", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(22).Value, ci))
            cmdRkp.Parameters.AddWithValue("@ecg", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(23).Value, ci))
            cmdRkp.Parameters.AddWithValue("@holter", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(24).Value, ci))
            cmdRkp.Parameters.AddWithValue("@treadmill", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(25).Value, ci))
            cmdRkp.Parameters.AddWithValue("@echocardio", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(26).Value, ci))
            cmdRkp.Parameters.AddWithValue("@usg", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(27).Value, ci))
            cmdRkp.Parameters.AddWithValue("@rontgen", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(28).Value, ci))
            cmdRkp.Parameters.AddWithValue("@ctscan", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(29).Value, ci))
            cmdRkp.Parameters.AddWithValue("@mri", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(30).Value, ci))
            cmdRkp.Parameters.AddWithValue("@labpa", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(31).Value, ci))
            cmdRkp.Parameters.AddWithValue("@labpk", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(32).Value, ci))
            cmdRkp.Parameters.AddWithValue("@darah", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(33).Value, ci))
            cmdRkp.Parameters.AddWithValue("@obat", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(34).Value, ci))
            cmdRkp.Parameters.AddWithValue("@alkes", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(35).Value, ci))
            cmdRkp.Parameters.AddWithValue("@oksigen", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(36).Value, ci))
            cmdRkp.Parameters.AddWithValue("@kassa", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(37).Value, ci))
            cmdRkp.Parameters.AddWithValue("@tindakan", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(38).Value, ci))
            cmdRkp.Parameters.AddWithValue("@ventilator", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(39).Value, ci))
            cmdRkp.Parameters.AddWithValue("@nebulizer", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(40).Value, ci))
            cmdRkp.Parameters.AddWithValue("@syringe", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(41).Value, ci))
            cmdRkp.Parameters.AddWithValue("@total", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(42).Value, ci))
            cmdRkp.Parameters.AddWithValue("@tarifinacbg", Convert.ToDouble(dgvRajal.Rows(lastIndexRj).Cells(43).Value, ci))
            cmdRkp.ExecuteNonQuery()
            cmdRkp.Parameters.Clear()

            MessageBox.Show("Update total rekap harian berhasil tersimpan", "Insert Data Succes", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error update rekap harian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        conn.Close()
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

        'TableLayoutPanel2.RowStyles(3).SizeType = SizeType.Percent
        'TableLayoutPanel2.RowStyles(3).Height = 0

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

    Private Sub btnTotal_Click(sender As Object, e As EventArgs) Handles btnTotal.Click
        TotalRekap.Show()
        Me.Hide()
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

        'Call DaftarDokterAll()
#Region "Comment"
#Region "Total Rajal"
        Dim totAdmin, totVisit, totKonsul, totJasaRj, totEndos,
            totBedah, totBronc, totHemo, totCvc, totIvp,
            totLain, totGizi, totFisio, totECG, totHolter,
            totTread, totEcho, totUsg, totRontgen, totCtScan,
            totMri, totPA, totPK, totDarah, totObat,
            totAlkes, totOxy, totKassa, totTind, totVenti,
            totNebul, totSyr, totTotal, totTotal2 As Double

        For i As Integer = 0 To dgvRajal.RowCount - 1
            totAdmin += dgvRajal.Rows(i).Cells(11).Value
            'totVisit += dgvRajal.Rows(i).Cells(10).Value
            'totKonsul += dgvRajal.Rows(i).Cells(11).Value
            totJasaRj += dgvRajal.Rows(i).Cells(12).Value
            totBedah += dgvRajal.Rows(i).Cells(13).Value

            totEndos += dgvRajal.Rows(i).Cells(14).Value
            totBronc += dgvRajal.Rows(i).Cells(15).Value
            totHemo += dgvRajal.Rows(i).Cells(16).Value
            totCvc += dgvRajal.Rows(i).Cells(17).Value
            totIvp += dgvRajal.Rows(i).Cells(18).Value

            totLain += dgvRajal.Rows(i).Cells(19).Value
            totGizi += dgvRajal.Rows(i).Cells(20).Value
            totFisio += dgvRajal.Rows(i).Cells(21).Value
            totECG += dgvRajal.Rows(i).Cells(22).Value
            totHolter += dgvRajal.Rows(i).Cells(23).Value

            totTread += dgvRajal.Rows(i).Cells(24).Value
            totEcho += dgvRajal.Rows(i).Cells(25).Value
            totUsg += dgvRajal.Rows(i).Cells(26).Value
            totRontgen += dgvRajal.Rows(i).Cells(27).Value
            totCtScan += dgvRajal.Rows(i).Cells(28).Value

            totMri += dgvRajal.Rows(i).Cells(29).Value
            totPA += dgvRajal.Rows(i).Cells(30).Value
            totPK += dgvRajal.Rows(i).Cells(31).Value
            totDarah += dgvRajal.Rows(i).Cells(32).Value
            totObat += dgvRajal.Rows(i).Cells(33).Value

            totAlkes += dgvRajal.Rows(i).Cells(34).Value
            totOxy += dgvRajal.Rows(i).Cells(35).Value
            totKassa += dgvRajal.Rows(i).Cells(36).Value
            totTind += dgvRajal.Rows(i).Cells(37).Value

            totVenti += dgvRajal.Rows(i).Cells(38).Value
            totNebul += dgvRajal.Rows(i).Cells(39).Value
            totSyr += dgvRajal.Rows(i).Cells(40).Value
            totTotal += dgvRajal.Rows(i).Cells(41).Value
            totTotal2 += dgvRajal.Rows(i).Cells(42).Value
        Next

        dgvRajal.Rows.Add("-", "-", "-", "-", "TOTAL", "-", "-", "-", "-",
                          "-", "-", totAdmin, totJasaRj, totBedah,
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
            riAdmin += dgvRanap.Rows(i).Cells(16).Value
            riAkmds += dgvRanap.Rows(i).Cells(17).Value
            riJasa += dgvRanap.Rows(i).Cells(18).Value
            riBedah += dgvRanap.Rows(i).Cells(19).Value
            riEndos += dgvRanap.Rows(i).Cells(20).Value
            '5
            riBronc += dgvRanap.Rows(i).Cells(21).Value
            riHemo += dgvRanap.Rows(i).Cells(22).Value
            riCvc += dgvRanap.Rows(i).Cells(23).Value
            riIvp += dgvRanap.Rows(i).Cells(24).Value
            riLain += dgvRanap.Rows(i).Cells(25).Value
            '10
            riGizi += dgvRanap.Rows(i).Cells(26).Value
            riFarklin += dgvRanap.Rows(i).Cells(27).Value
            riFisio += dgvRanap.Rows(i).Cells(28).Value
            riTind += dgvRanap.Rows(i).Cells(29).Value
            riAskep += dgvRanap.Rows(i).Cells(30).Value
            '15
            riRohani += dgvRanap.Rows(i).Cells(31).Value
            riECG += dgvRanap.Rows(i).Cells(32).Value
            riHolter += dgvRanap.Rows(i).Cells(33).Value
            riEcho += dgvRanap.Rows(i).Cells(34).Value
            riUsg += dgvRanap.Rows(i).Cells(35).Value
            '20
            riRontgen += dgvRanap.Rows(i).Cells(36).Value
            riCtScan += dgvRanap.Rows(i).Cells(37).Value
            riMri += dgvRanap.Rows(i).Cells(38).Value
            riPA += dgvRanap.Rows(i).Cells(39).Value
            riPK += dgvRanap.Rows(i).Cells(40).Value
            '25
            riPmi += dgvRanap.Rows(i).Cells(41).Value
            riRehab += dgvRanap.Rows(i).Cells(42).Value
            riIcu += dgvRanap.Rows(i).Cells(43).Value
            riPicu += dgvRanap.Rows(i).Cells(44).Value
            riNicu += dgvRanap.Rows(i).Cells(45).Value
            '30
            riHcu += dgvRanap.Rows(i).Cells(46).Value
            riObat += dgvRanap.Rows(i).Cells(47).Value
            riAlkes += dgvRanap.Rows(i).Cells(48).Value
            riOxy += dgvRanap.Rows(i).Cells(49).Value
            riKassa += dgvRanap.Rows(i).Cells(50).Value
            '35
            riJenazah += dgvRanap.Rows(i).Cells(51).Value
            riVenti += dgvRanap.Rows(i).Cells(52).Value
            riNebul += dgvRanap.Rows(i).Cells(53).Value
            riSyr += dgvRanap.Rows(i).Cells(54).Value
            riMonitor += dgvRanap.Rows(i).Cells(55).Value
            '40
            riTotal += dgvRanap.Rows(i).Cells(56).Value
            riTotal2 += dgvRanap.Rows(i).Cells(57).Value
        Next

        dgvRanap.Rows.Add("-", "-", "-", "-", "TOTAL",
                          "-", "-", "-", "-", "-",
                          "-", "-", "-", "-", "-",
                          "-", riAdmin, riAkmds, riJasa, riBedah,
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
        '#Region "Total Dokter"
        '        Dim totJasa As Double

        '        For i As Integer = 0 To dgvDokterAll.RowCount - 1
        '            totJasa += dgvDokterAll.Rows(i).Cells(9).Value
        '        Next

        '        dgvDokterAll.Rows.Add("-", "-", "TOTAL", "-", "-",
        '                              "-", "-", "-", "-", totJasa)

        '        lastIndexDok = dgvDokterAll.Rows.Count - 1
        '#End Region
#End Region
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'If txtFilter.Text = "Rawat Jalan" Then
        '    Call simpanTotalHarianRajal()
        'ElseIf txtFilter.Text = "Rawat Inap" Then
        '    Call simpanTotalHarianRanap()
        'End If

        Call CekTglRekap()
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