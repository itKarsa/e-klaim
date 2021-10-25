Imports MySql.Data.MySqlClient
Public Class Berakdown

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Dim txtranap As String = Eklaim.txtRawat.Text.Contains("Rawat Inap")
    Dim txtrajal As String = Eklaim.txtRawat.Text.Contains("Rawat Jalan")
    Dim txtigd As String = Eklaim.txtRawat.Text.Contains("Igd")

    Sub setColor(button As Button)
        btnHome.BackColor = Color.White
        btnEklaim.BackColor = Color.White
        btnBuku.BackColor = Color.White
        btnPiutang.BackColor = Color.White
        btnUmum.BackColor = Color.White
        button.BackColor = Color.FromArgb(209, 232, 223)
    End Sub

    Dim a, b, c, d, ee, f,
        g, h, i, j, k, l,
        m, n, o, p, q, r,
        s, t, u, v, w, x,
        y, z, a1, b1, c1, d1,
        e1, f1, g1, h1, i1, j1,
        k1, l1, m1, n1, o1, p1,
        q1, r1, s1, t1 As Integer

    Dim kdUnit As String

    Dim ci As IFormatProvider = New System.Globalization.CultureInfo("id-ID", True)

    Sub autoDokter()
        Call koneksiServer()

        Dim cmd As New MySqlCommand("SELECT namapetugasMedis FROM t_tenagamedis2 WHERE kdKelompokTenagaMedis IN ('ktm1','ktm2','ktm3','ktm5','ktm6','ktm6','ktm7','ktm8')", conn)
        Dim ad As New MySqlDataAdapter(cmd)
        Dim dt As New DataTable
        Dim col As New AutoCompleteStringCollection
        dt.Clear()
        ad.Fill(dt)

        For i As Integer = 0 To dt.Rows.Count - 1
            col.Add(dt.Rows(i)("namapetugasMedis"))
        Next

        txtDrIgd.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtDrIgd.AutoCompleteCustomSource = col
        txtDrIgd.AutoCompleteMode = AutoCompleteMode.Suggest

        txtDrOperator.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtDrOperator.AutoCompleteCustomSource = col
        txtDrOperator.AutoCompleteMode = AutoCompleteMode.Suggest

        txtDrAnestesi.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtDrAnestesi.AutoCompleteCustomSource = col
        txtDrAnestesi.AutoCompleteMode = AutoCompleteMode.Suggest

        txtDpjp.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtDpjp.AutoCompleteCustomSource = col
        txtDpjp.AutoCompleteMode = AutoCompleteMode.Suggest

        conn.Close()
    End Sub

    Sub totalTarifAkomodasi()
        Dim totTarif As Long
        totTarif = 0
        For i As Integer = 0 To dgvAkomodasi.Rows.Count - 1
            totTarif = totTarif + Val(CLng(dgvAkomodasi.Rows(i).Cells(4).Value))
        Next
        txtTotAkomodasi.Text = CLng(totTarif).ToString("#,##0")
    End Sub

    Sub totalTarifIgdKonsul()
        Dim totTarif As Long
        totTarif = 0
        For i As Integer = 0 To dgvDrIgdKonsul.Rows.Count - 1
            totTarif = totTarif + Val(CLng(dgvDrIgdKonsul.Rows(i).Cells(2).Value))
        Next
        txtTotKonsulPoli.Text = CLng(totTarif).ToString("#,##0")
    End Sub

    Sub totalTarifVisite()
        Dim totTarif As Long
        totTarif = 0
        For i As Integer = 0 To dgvDrVisite.Rows.Count - 1
            totTarif = totTarif + Val(CLng(dgvDrVisite.Rows(i).Cells(2).Value))
        Next
        txtTotVisite.Text = CLng(totTarif).ToString("#,##0")
    End Sub

    Sub totalTarifKonsul()
        Dim totTarif As Long
        totTarif = 0
        For i As Integer = 0 To dgvDrKonsulRanap.Rows.Count - 1
            totTarif = totTarif + Val(CLng(dgvDrKonsulRanap.Rows(i).Cells(2).Value))
        Next
        txtTotKonsulRanap.Text = CLng(totTarif).ToString("#,##0")
    End Sub

    Sub totalTarifGizi()
        Dim totTarif As Long
        totTarif = 0
        For i As Integer = 0 To dgvGizi.Rows.Count - 1
            totTarif = totTarif + Val(CLng(dgvGizi.Rows(i).Cells(4).Value))
        Next
        txtTotGizi.Text = CLng(totTarif).ToString("#,##0")
    End Sub

    Sub totalTarifFarklin()
        Dim totTarif As Long
        totTarif = 0
        For i As Integer = 0 To dgvFarklin.Rows.Count - 1
            totTarif = totTarif + Val(CLng(dgvFarklin.Rows(i).Cells(4).Value))
        Next
        txtTotFarklin.Text = CLng(totTarif).ToString("#,##0")
    End Sub

    Sub totalTarifFisio()
        Dim totTarif As Long
        totTarif = 0
        For i As Integer = 0 To dgvFisio.Rows.Count - 1
            totTarif = totTarif + Val(CLng(dgvFisio.Rows(i).Cells(4).Value))
        Next
        txtTotFisio.Text = CLng(totTarif).ToString("#,##0")
    End Sub

    Sub tampilRuang()
        Call koneksiServer()
        Dim cmd As MySqlCommand
        Dim query As String = ""

        If Eklaim.txtRawat.Text.Contains("Rawat Jalan") Or Eklaim.txtRawat.Text.Contains("Igd") Then
            query = "SELECT u.unit AS unit
                       FROM t_registrasirawatjalan AS rj
	                        INNER JOIN t_unit AS u ON rj.kdUnit = u.kdUnit
	                  WHERE rj.noDaftar = '" & Eklaim.noDaftar & "'"
        ElseIf Eklaim.txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT rawatInap AS unit
		               FROM vw_daftarruangakomodasi
	                  WHERE noDaftar = '" & Eklaim.noDaftar & "'"
        End If

        cmd = New MySqlCommand(query, conn)
        da = New MySqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        txtRuang.DataSource = dt
        txtRuang.DisplayMember = "unit"
        txtRuang.ValueMember = "unit"
        txtRuang.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Sub addJpRajal()
        conn.Close()
        Call koneksiJepe()

        Dim strJpRj As String = ""
        Dim cmdJpRj As MySqlCommand

        If Eklaim.txtJaminan.Text.Equals("Pasien Bayar", StringComparison.OrdinalIgnoreCase) Then
            strJpRj = "INSERT INTO t_eklaimjprajalumum(noRM,NoSep,tglMasuk,tglKeluar,namaPasien,
                                                       unit,admin,endoscopy,bronkoscopy,hd,
                                                       cvc,ivp,nonbedahlain,gizi,fisioterapi,
                                                       ecg,holter,treadmill,echocardio,usg,
                                                       rontgen,ctscan,mri,labpa,labpk,
                                                       obat,oksigen,kassa,tindakan,ventilator,
                                                       nebulizer,syringe,total,tarifinacbg) 
                                                VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                        '" & Format(CDate(Eklaim.txtTglKlrRawat.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "','" & txtAdminPoli.Text & "',
                                                        '" & Convert.ToDouble(txtTotAdmPoli.Text, ci) & "','" & Convert.ToDouble(txtTotEndos.Text, ci) & "',
                                                        '" & Convert.ToDouble(txtTotBronc.Text, ci) & "','" & Convert.ToDouble(txtTotHemo.Text, ci) & "','" & Convert.ToDouble(txtTotCvc.Text, ci) & "',
                                                        '" & Convert.ToDouble(txtTotLain.Text, ci) & "','" & Convert.ToDouble(txtTotCathLab.Text, ci) & "','" & Convert.ToDouble(txtTotGizi.Text, ci) & "',
                                                        '" & Convert.ToDouble(txtTotFisio.Text, ci) & "','" & Convert.ToDouble(txtTotEcg.Text, ci) & "','" & Convert.ToDouble(txtTotHolter.Text, ci) & "',
                                                        '" & Convert.ToDouble(txtTotTreadmill.Text, ci) & "','" & Convert.ToDouble(txtTotEcho.Text, ci) & "','" & Convert.ToDouble(txtTotUsg.Text, ci) & "',
                                                        '" & Convert.ToDouble(txtTotRontgen.Text, ci) & "','" & Convert.ToDouble(txtTotCtscan.Text, ci) & "','" & Convert.ToDouble(txtTotMri.Text, ci) & "',
                                                        '" & Convert.ToDouble(txtTotLabPA.Text, ci) & "','" & Convert.ToDouble(txtTotLabPK.Text, ci) & "','" & Convert.ToDouble(txtTotObat.Text, ci) & "',
                                                        '" & Convert.ToDouble(txtTotOxy.Text, ci) & "','" & Convert.ToDouble(txtTotKassa.Text, ci) & "','" & Convert.ToDouble(txtTotTindakan.Text, ci) & "',
                                                        '" & Convert.ToDouble(txtTotVenti.Text, ci) & "','" & Convert.ToDouble(txtTotNebul.Text, ci) & "','" & Convert.ToDouble(txtTotSyr.Text, ci) & "',
                                                        '" & Convert.ToDouble(txtTotalRincian.Text, ci) & "','" & Convert.ToDouble(txtInacbg.Text, ci) & "')"
        Else
            strJpRj = "INSERT INTO t_eklaimjprajal(noRM,NoSep,tglMasuk,tglKeluar,namaPasien,
                                               unit,admin,endoscopy,bronkoscopy,hd,
                                               cvc,ivp,nonbedahlain,gizi,fisioterapi,
                                               ecg,holter,treadmill,echocardio,usg,
                                               rontgen,ctscan,mri,labpa,labpk,
                                               obat,oksigen,kassa,tindakan,ventilator,
                                               nebulizer,syringe,total,tarifinacbg) 
                                        VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                '" & Format(CDate(Eklaim.txtTglKlrRawat.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "','" & txtAdminPoli.Text & "',
                                                '" & Convert.ToDouble(txtTotAdmPoli.Text, ci) & "','" & Convert.ToDouble(txtTotEndos.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotBronc.Text, ci) & "','" & Convert.ToDouble(txtTotHemo.Text, ci) & "','" & Convert.ToDouble(txtTotCvc.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotLain.Text, ci) & "','" & Convert.ToDouble(txtTotCathLab.Text, ci) & "','" & Convert.ToDouble(txtTotGizi.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotFisio.Text, ci) & "','" & Convert.ToDouble(txtTotEcg.Text, ci) & "','" & Convert.ToDouble(txtTotHolter.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotTreadmill.Text, ci) & "','" & Convert.ToDouble(txtTotEcho.Text, ci) & "','" & Convert.ToDouble(txtTotUsg.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotRontgen.Text, ci) & "','" & Convert.ToDouble(txtTotCtscan.Text, ci) & "','" & Convert.ToDouble(txtTotMri.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotLabPA.Text, ci) & "','" & Convert.ToDouble(txtTotLabPK.Text, ci) & "','" & Convert.ToDouble(txtTotObat.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotOxy.Text, ci) & "','" & Convert.ToDouble(txtTotKassa.Text, ci) & "','" & Convert.ToDouble(txtTotTindakan.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotVenti.Text, ci) & "','" & Convert.ToDouble(txtTotNebul.Text, ci) & "','" & Convert.ToDouble(txtTotSyr.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotalRincian.Text, ci) & "','" & Convert.ToDouble(txtInacbg.Text, ci) & "')"
        End If



        Try
            cmdJpRj = New MySqlCommand(strJpRj, conn)
            cmdJpRj.ExecuteNonQuery()

            MessageBox.Show("Insert data rekap berhasil dilakukan", "Insert Data Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error Rekap JP Rajal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        conn.Close()
    End Sub

    Sub addJpRanap()
        conn.Close()
        Call koneksiJepe()

        Dim strJpR As String
        Dim cmdJpR As MySqlCommand

        If Eklaim.txtJaminan.Text.Equals("Pasien Bayar", StringComparison.OrdinalIgnoreCase) Then
            strJpR = "INSERT INTO t_eklaimjpranapumum(noRM,NoSep,tglMasuk,tglKeluar,namaPasien,
                                                     unit,hakKelas,kelas,jmlHari,dpjp,tarifDpjp,drOperator,drAnestesi,akomodasiAdmin,akomodasiRuang,
                                                     prosedurbedah,endoscopy,bronkoscopy,hd,cvc,ivp,nonbedahlain,gizi,farklin,fisio,
                                                     tindakan,askep,kerohanian,ecg,holter,echocardio,usg,rontgen,ctscan,mri,
                                                     labpa,labpk,darah,rehab,icu,picu,nicu,hcu,obat,alkes,
                                                     oksigen,kassa,jenazah,ventilator,nebulizer,syringe,bedsetmonitor,total,tarifinacbg) 
                                            VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                    '" & Format(CDate(Eklaim.txtTglKlrRawat.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "',
                                                    @unit,@hakKelas,@kelas,@jmlHari,'" & txtDpjp.Text & "',
                                                    '" & Convert.ToDouble(txtTotDpjp.Text, ci) & "','" & txtDrOperator.Text & "',
                                                    '" & txtDrAnestesi.Text & "','" & Convert.ToDouble(txtTotAdmPoli.Text, ci) & "',@akomodasiRuang,'" & Convert.ToDouble(txtTotOpe.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotEndos.Text, ci) & "','" & Convert.ToDouble(txtTotBronc.Text, ci) & "','" & Convert.ToDouble(txtTotHemo.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotCvc.Text, ci) & "','" & Convert.ToDouble(txtTotLain.Text, ci) & "','" & Convert.ToDouble(txtTotCathLab.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotGizi.Text, ci) & "','" & Convert.ToDouble(txtTotFarklin.Text, ci) & "','" & Convert.ToDouble(txtTotFisio.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotTindakan.Text, ci) & "','" & Convert.ToDouble(txtTotAskep.Text, ci) & "','" & Convert.ToDouble(txtTotRohani.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotEcg.Text, ci) & "','" & Convert.ToDouble(txtTotHolter.Text, ci) & "','" & Convert.ToDouble(txtTotEcho.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotUsg.Text, ci) & "','" & Convert.ToDouble(txtTotRontgen.Text, ci) & "','" & Convert.ToDouble(txtTotCtscan.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotMri.Text, ci) & "','" & Convert.ToDouble(txtTotLabPA.Text, ci) & "','" & Convert.ToDouble(txtTotLabPK.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotDarah.Text, ci) & "','" & Convert.ToDouble(txtTotRehab.Text, ci) & "','" & Convert.ToDouble(txtTotIcu.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotPicu.Text, ci) & "','" & Convert.ToDouble(txtTotNicu.Text, ci) & "','" & Convert.ToDouble(txtTotHcu.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotObat.Text, ci) & "','" & Convert.ToDouble(txtTotAlkes.Text, ci) & "','" & Convert.ToDouble(txtTotOxy.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotKassa.Text, ci) & "','" & Convert.ToDouble(txtTotJenazah.Text, ci) & "','" & Convert.ToDouble(txtTotVenti.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotNebul.Text, ci) & "','" & Convert.ToDouble(txtTotSyr.Text, ci) & "','" & Convert.ToDouble(txtTotMonitor.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotalRincian.Text, ci) & "','" & Convert.ToDouble(txtInacbg.Text, ci) & "')"
        Else
            strJpR = "INSERT INTO t_eklaimjpranap(noRM,NoSep,tglMasuk,tglKeluar,namaPasien,
                                                 unit,hakKelas,kelas,jmlHari,dpjp,tarifDpjp,drOperator,drAnestesi,akomodasiAdmin,akomodasiRuang,
                                                 prosedurbedah,endoscopy,bronkoscopy,hd,cvc,ivp,nonbedahlain,gizi,farklin,fisio,
                                                 tindakan,askep,kerohanian,ecg,holter,echocardio,usg,rontgen,ctscan,mri,
                                                 labpa,labpk,darah,rehab,icu,picu,nicu,hcu,obat,alkes,
                                                 oksigen,kassa,jenazah,ventilator,nebulizer,syringe,bedsetmonitor,total,tarifinacbg) 
                                        VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                '" & Format(CDate(Eklaim.txtTglKlrRawat.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "',
                                                @unit,@hakKelas,@kelas,@jmlHari,'" & txtDpjp.Text & "',
                                                '" & Convert.ToDouble(txtTotDpjp.Text, ci) & "','" & txtDrOperator.Text & "',
                                                '" & txtDrAnestesi.Text & "','" & Convert.ToDouble(txtTotAdmPoli.Text, ci) & "',@akomodasiRuang,'" & Convert.ToDouble(txtTotOpe.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotEndos.Text, ci) & "','" & Convert.ToDouble(txtTotBronc.Text, ci) & "','" & Convert.ToDouble(txtTotHemo.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotCvc.Text, ci) & "','" & Convert.ToDouble(txtTotLain.Text, ci) & "','" & Convert.ToDouble(txtTotCathLab.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotGizi.Text, ci) & "','" & Convert.ToDouble(txtTotFarklin.Text, ci) & "','" & Convert.ToDouble(txtTotFisio.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotTindakan.Text, ci) & "','" & Convert.ToDouble(txtTotAskep.Text, ci) & "','" & Convert.ToDouble(txtTotRohani.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotEcg.Text, ci) & "','" & Convert.ToDouble(txtTotHolter.Text, ci) & "','" & Convert.ToDouble(txtTotEcho.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotUsg.Text, ci) & "','" & Convert.ToDouble(txtTotRontgen.Text, ci) & "','" & Convert.ToDouble(txtTotCtscan.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotMri.Text, ci) & "','" & Convert.ToDouble(txtTotLabPA.Text, ci) & "','" & Convert.ToDouble(txtTotLabPK.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotDarah.Text, ci) & "','" & Convert.ToDouble(txtTotRehab.Text, ci) & "','" & Convert.ToDouble(txtTotIcu.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotPicu.Text, ci) & "','" & Convert.ToDouble(txtTotNicu.Text, ci) & "','" & Convert.ToDouble(txtTotHcu.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotObat.Text, ci) & "','" & Convert.ToDouble(txtTotAlkes.Text, ci) & "','" & Convert.ToDouble(txtTotOxy.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotKassa.Text, ci) & "','" & Convert.ToDouble(txtTotJenazah.Text, ci) & "','" & Convert.ToDouble(txtTotVenti.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotNebul.Text, ci) & "','" & Convert.ToDouble(txtTotSyr.Text, ci) & "','" & Convert.ToDouble(txtTotMonitor.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotalRincian.Text, ci) & "','" & Convert.ToDouble(txtInacbg.Text, ci) & "')"
        End If


        cmdJpR = New MySqlCommand(strJpR, conn)

        Try
            If txtRuang.Text.Contains("CU") Then
                cmdJpR.Parameters.AddWithValue("@unit", txtRuang.Text)
                cmdJpR.Parameters.AddWithValue("@hakKelas", Eklaim.txtKelas.Text)
                cmdJpR.Parameters.AddWithValue("@kelas", "KELAS I")
                cmdJpR.Parameters.AddWithValue("@jmlHari", "-")
                cmdJpR.Parameters.AddWithValue("@akomodasiRuang", "-")
                cmdJpR.ExecuteNonQuery()
                cmdJpR.Parameters.Clear()
            Else
                For i As Integer = 0 To dgvAkomodasi.Rows.Count - 1
                    cmdJpR.Parameters.AddWithValue("@unit", dgvAkomodasi.Rows(i).Cells(0).Value)
                    cmdJpR.Parameters.AddWithValue("@hakKelas", dgvAkomodasi.Rows(i).Cells(1).Value)
                    cmdJpR.Parameters.AddWithValue("@kelas", dgvAkomodasi.Rows(i).Cells(1).Value)
                    cmdJpR.Parameters.AddWithValue("@jmlHari", dgvAkomodasi.Rows(i).Cells(3).Value)
                    cmdJpR.Parameters.AddWithValue("@akomodasiRuang", Convert.ToDouble(dgvAkomodasi.Rows(i).Cells(4).Value, ci))
                    cmdJpR.ExecuteNonQuery()
                    cmdJpR.Parameters.Clear()
                Next
            End If

            MessageBox.Show("Insert data rekap berhasil dilakukan", "Insert Data Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error Rekap JP Ranap", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        conn.Close()
    End Sub

    Sub addJpdokterVisitePoliIgd()
        conn.Close()
        Call koneksiJepe()

        Dim strJpVI As String = ""
        Dim cmdJpVI As MySqlCommand

        If Eklaim.txtJaminan.Text.Equals("Pasien Bayar", StringComparison.OrdinalIgnoreCase) Then
            strJpVI = "INSERT INTO t_eklaimjpdokterrajalumum(noRM,NoSep,tglMasuk,
                                                             namaPasien,unit,kelas,
                                                             jmlVisite,drVisite,jasaVisite)
                                                     VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                             '" & Eklaim.txtNamaPasien.Text & "','" & Eklaim.txtUnit.Text & "','" & Eklaim.txtKelas.Text & "',
                                                             '1','" & txtDrIgd.Text & "','" & Convert.ToDouble(txtTotDrIgd.Text, ci) & "')"
        Else
            strJpVI = "INSERT INTO t_eklaimjpdokterrajal(noRM,NoSep,tglMasuk,
                                                         namaPasien,unit,kelas,
                                                         jmlVisite,drVisite,jasaVisite)
                                                 VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                         '" & Eklaim.txtNamaPasien.Text & "','" & Eklaim.txtUnit.Text & "','" & Eklaim.txtKelas.Text & "',
                                                         '1','" & txtDrIgd.Text & "','" & Convert.ToDouble(txtTotDrIgd.Text, ci) & "')"
        End If



        Try
            cmdJpVI = New MySqlCommand(strJpVI, conn)
            cmdJpVI.ExecuteNonQuery()

            'MessageBox.Show("Insert data rekap jp dokter visite berhasil dilakukan", "Insert Data Succes", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error Rekap JP Dokter Poli/Igd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        conn.Close()
    End Sub

    Sub addJpdokterKonsulPoliIgd()
        conn.Close()
        Call koneksiJepe()

        Dim strJpI As String = ""
        Dim cmdJpI As MySqlCommand

        If Eklaim.txtJaminan.Text.Equals("Pasien Bayar", StringComparison.OrdinalIgnoreCase) And (txtrajal = True Or txtigd = True) Then
            strJpI = "INSERT INTO t_eklaimjpdokterrajalumum(noRM,NoSep,tglMasuk,tglKeluar,
                                               namaPasien,unit,kelas,jmlVisite,
                                               drKonsul,jasaVisite)
                                       VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                               '" & Format(CDate(Eklaim.txtTglKeluar.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "','" & Eklaim.txtUnit.Text & "','" & Eklaim.txtKelas.Text & "',
                                               @jmlVisite,@drKonsul,@jasaVisite)"
            MsgBox("Umum Rajal/Igd")
        ElseIf Eklaim.txtJaminan.Text.Equals("Pasien Bayar", StringComparison.OrdinalIgnoreCase) And txtranap = True Then
            strJpI = "INSERT INTO t_eklaimjpdokterranapumum(noRM,NoSep,tglMasuk,tglKeluar,
                                               namaPasien,unit,kelas,jmlVisite,
                                               drKonsul,jasaVisite)
                                       VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                               '" & Format(CDate(Eklaim.txtTglKeluar.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "','" & Eklaim.txtUnit.Text & "','" & Eklaim.txtKelas.Text & "',
                                               @jmlVisite,@drKonsul,@jasaVisite)"
            MsgBox("Umum Ranap")
        ElseIf txtrajal = True Or txtigd = True Then
            strJpI = "INSERT INTO t_eklaimjpdokterrajal(noRM,NoSep,tglMasuk,tglKeluar,
                                               namaPasien,unit,kelas,jmlVisite,
                                               drKonsul,jasaVisite)
                                       VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                               '" & Format(CDate(Eklaim.txtTglKeluar.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "','" & Eklaim.txtUnit.Text & "','" & Eklaim.txtKelas.Text & "',
                                               @jmlVisite,@drKonsul,@jasaVisite)"
            MsgBox("JKN Rajal/Igd")
        ElseIf txtranap = True Then
            strJpI = "INSERT INTO t_eklaimjpdokterranap(noRM,NoSep,tglMasuk,tglKeluar,
                                               namaPasien,unit,kelas,jmlVisite,
                                               drKonsul,jasaVisite)
                                       VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                               '" & Format(CDate(Eklaim.txtTglKeluar.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "','" & Eklaim.txtUnit.Text & "','" & Eklaim.txtKelas.Text & "',
                                               @jmlVisite,@drKonsul,@jasaVisite)"
            MsgBox("JKN Ranap")
        End If

        cmdJpI = New MySqlCommand(strJpI, conn)
        Try
            For i As Integer = 0 To dgvDrIgdKonsul.Rows.Count - 1
                cmdJpI.Parameters.AddWithValue("@drKonsul", dgvDrIgdKonsul.Rows(i).Cells(0).Value)
                cmdJpI.Parameters.AddWithValue("@jmlVisite", dgvDrIgdKonsul.Rows(i).Cells(1).Value)
                cmdJpI.Parameters.AddWithValue("@jasaVisite", Convert.ToDouble(dgvDrIgdKonsul.Rows(i).Cells(2).Value, ci))
                cmdJpI.ExecuteNonQuery()
                cmdJpI.Parameters.Clear()
            Next

            'MessageBox.Show("Insert data rekap jp dokter visite berhasil dilakukan", "Insert Data Succes", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error Rekap JP Dokter Poli/Igd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        conn.Close()
    End Sub

    Sub addJpdokterVisite()
        conn.Close()
        Call koneksiJepe()

        Dim strJpV As String = ""
        Dim cmdJpV As MySqlCommand

        If Eklaim.txtJaminan.Text.Equals("Pasien Bayar", StringComparison.OrdinalIgnoreCase) Then
            strJpV = "INSERT INTO t_eklaimjpdokterranapumum(noRM,NoSep,tglMasuk,tglKeluar,
                                                            namaPasien,unit,kelas,jmlVisite,
                                                            drVisite,jasaVisite)
                                                    VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                            '" & Format(CDate(Eklaim.txtTglKeluar.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "','" & Eklaim.txtUnit.Text & "','" & Eklaim.txtKelas.Text & "',
                                                            @jmlVisite,@drVisite,@jasaVisite)"
        Else
            strJpV = "INSERT INTO t_eklaimjpdokterranap(noRM,NoSep,tglMasuk,tglKeluar,
                                                        namaPasien,unit,kelas,jmlVisite,
                                                        drVisite,jasaVisite)
                                                VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                        '" & Format(CDate(Eklaim.txtTglKeluar.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "','" & Eklaim.txtUnit.Text & "','" & Eklaim.txtKelas.Text & "',
                                                        @jmlVisite,@drVisite,@jasaVisite)"
        End If


        cmdJpV = New MySqlCommand(strJpV, conn)
        Try
            For i As Integer = 0 To dgvDrVisite.Rows.Count - 1
                cmdJpV.Parameters.AddWithValue("@drVisite", dgvDrVisite.Rows(i).Cells(0).Value)
                cmdJpV.Parameters.AddWithValue("@jmlVisite", dgvDrVisite.Rows(i).Cells(1).Value)
                cmdJpV.Parameters.AddWithValue("@jasaVisite", Convert.ToDouble(dgvDrVisite.Rows(i).Cells(2).Value, ci))
                cmdJpV.ExecuteNonQuery()
                cmdJpV.Parameters.Clear()
            Next

            'MessageBox.Show("Insert data rekap jp dokter visite berhasil dilakukan", "Insert Data Succes", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error Rekap JP Dokter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        conn.Close()
    End Sub

    Sub addJpdokterKonsul()
        conn.Close()
        Call koneksiJepe()

        Dim strJpK As String = ""
        Dim cmdJpK As MySqlCommand

        If Eklaim.txtJaminan.Text.Equals("Pasien Bayar", StringComparison.OrdinalIgnoreCase) Then
            strJpK = "INSERT INTO t_eklaimjpdokterranapumum(noRM,NoSep,tglMasuk,tglKeluar,namaPasien,unit,
                                                            kelas,jmlVisite,drKonsul,jasaVisite)
                                                    VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                            '" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "','" & Eklaim.txtUnit.Text & "','" & Eklaim.txtKelas.Text & "',
                                                            @jmlKonsul,@drKonsul,@jasaKonsul)"
        Else
            strJpK = "INSERT INTO t_eklaimjpdokterranap(noRM,NoSep,tglMasuk,tglKeluar,namaPasien,unit,
                                                        kelas,jmlVisite,drKonsul,jasaVisite)
                                                 VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                        '" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "','" & Eklaim.txtUnit.Text & "','" & Eklaim.txtKelas.Text & "',
                                                        @jmlKonsul,@drKonsul,@jasaKonsul)"
        End If

        cmdJpK = New MySqlCommand(strJpK, conn)
        Try
            For i As Integer = 0 To dgvDrKonsulRanap.Rows.Count - 1
                cmdJpK.Parameters.AddWithValue("@drKonsul", dgvDrKonsulRanap.Rows(i).Cells(0).Value)
                cmdJpK.Parameters.AddWithValue("@jmlKonsul", dgvDrKonsulRanap.Rows(i).Cells(1).Value)
                cmdJpK.Parameters.AddWithValue("@jasaKonsul", Convert.ToDouble(dgvDrKonsulRanap.Rows(i).Cells(2).Value, ci))
                cmdJpK.ExecuteNonQuery()
                cmdJpK.Parameters.Clear()
            Next

            'MessageBox.Show("Insert data rekap jp dokter konsul berhasil dilakukan", "Insert Data Succes", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error Rekap JP Dokter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        conn.Close()
    End Sub

    Private Sub Breakdown_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        txtUser.Text = Home.txtUser.Text
        Label3.Text = "Pasien a.n. " & Eklaim.txtNamaPasien.Text
        btnEklaim.BackColor = Color.FromArgb(209, 232, 223)
        PicCollapse.Visible = False
        Call tampilRuang()
        Call autoDokter()
    End Sub

    Private Sub picKeluar_Click(sender As Object, e As EventArgs) Handles picKeluar.Click
        Dim konfirmasi As MsgBoxResult

        konfirmasi = MsgBox("Apakah anda yakin ingin keluar..?", vbQuestion + vbYesNo, "Konfirmasi")
        If konfirmasi = vbYes Then
            Me.Close()
            LoginForm.Show()
        End If
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

    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        Home.Show()
        Me.Hide()
    End Sub

    Private Sub btnEklaim_Click(sender As Object, e As EventArgs) Handles btnEklaim.Click
        Dim btn As Button = CType(sender, Button)
        setColor(btn)
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

    Private Sub picBack_Click(sender As Object, e As EventArgs) Handles picBack.Click
        Eklaim.Show()
        Me.Close()
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If Eklaim.txtRawat.Text.Contains("Rawat Jalan") Or Eklaim.txtRawat.Text.Contains("Igd") Then
            Call addJpRajal()
            Call addJpdokterVisitePoliIgd()
            If dgvDrIgdKonsul.Rows.Count <> 0 Then
                Call addJpdokterKonsulPoliIgd()
            End If
        ElseIf Eklaim.txtRawat.Text.Contains("Rawat Inap") Then
            Call addJpRanap()
            If dgvDrVisite.Rows.Count <> 0 Then
                Call addJpdokterVisite()
            End If

            If dgvDrKonsulRanap.Rows.Count <> 0 Then
                Call addJpdokterKonsul()
            End If

            If dgvDrIgdKonsul.Rows.Count <> 0 Then
                Call addJpdokterKonsulPoliIgd()
            End If
        End If
    End Sub

    Private Sub dgvAkomodasi_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvAkomodasi.CellFormatting
        dgvAkomodasi.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        dgvAkomodasi.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
        dgvAkomodasi.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvAkomodasi.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        For i As Integer = 0 To dgvAkomodasi.Rows.Count - 1
            If i Mod 2 = 0 Then
                dgvAkomodasi.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                dgvAkomodasi.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Private Sub dgvDrIgdKonsul_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvDrIgdKonsul.CellFormatting
        dgvDrIgdKonsul.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        dgvDrIgdKonsul.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
        dgvDrIgdKonsul.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvDrIgdKonsul.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        For i As Integer = 0 To dgvDrIgdKonsul.Rows.Count - 1
            If i Mod 2 = 0 Then
                dgvDrIgdKonsul.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                dgvDrIgdKonsul.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Private Sub dgvDrVisite_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvDrVisite.CellFormatting
        dgvDrVisite.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        dgvDrVisite.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
        dgvDrVisite.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvDrVisite.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        For i As Integer = 0 To dgvDrVisite.Rows.Count - 1
            If i Mod 2 = 0 Then
                dgvDrVisite.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                dgvDrVisite.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Private Sub dgvDrKonsulRanap_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvDrKonsulRanap.CellFormatting
        dgvDrKonsulRanap.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        dgvDrKonsulRanap.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
        dgvDrKonsulRanap.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvDrKonsulRanap.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        For i As Integer = 0 To dgvDrKonsulRanap.Rows.Count - 1
            If i Mod 2 = 0 Then
                dgvDrKonsulRanap.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                dgvDrKonsulRanap.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Private Sub dgvGizi_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvGizi.CellFormatting
        dgvGizi.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        dgvGizi.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
        dgvGizi.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvGizi.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        For i As Integer = 0 To dgvGizi.Rows.Count - 1
            If i Mod 2 = 0 Then
                dgvGizi.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                dgvGizi.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Private Sub dgvFarklin_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvFarklin.CellFormatting
        dgvFarklin.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        dgvFarklin.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
        dgvFarklin.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvFarklin.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        For i As Integer = 0 To dgvFarklin.Rows.Count - 1
            If i Mod 2 = 0 Then
                dgvFarklin.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                dgvFarklin.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Private Sub dgvFisio_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvFisio.CellFormatting
        dgvFisio.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        dgvFisio.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
        dgvFisio.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvFisio.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        For i As Integer = 0 To dgvFisio.Rows.Count - 1
            If i Mod 2 = 0 Then
                dgvFisio.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                dgvFisio.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Private Sub dgvDrVisite_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgvDrVisite.RowsAdded

    End Sub

    Private Sub dgvDrVisite_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgvDrVisite.RowsRemoved
        Call totalTarifVisite()
    End Sub

    Private Sub dgvDrKonsulRanap_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgvDrKonsulRanap.RowsAdded

    End Sub

    Private Sub dgvDrKonsulRanap_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgvDrKonsulRanap.RowsRemoved
        Call totalTarifKonsul()
    End Sub

    Private Sub dgvDrIgdKonsul_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgvDrIgdKonsul.RowsAdded

    End Sub

    Private Sub dgvDrIgdKonsul_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgvDrIgdKonsul.RowsRemoved
        Call totalTarifIgdKonsul()
    End Sub

    Private Sub txtRuang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtRuang.SelectedIndexChanged
        Call koneksiServer()

        Dim kdUnit As String = ""
        Dim queryPoli As String
        Dim cmdPoli As MySqlCommand
        Dim drPoli As MySqlDataReader
        If Eklaim.txtRawat.Text.Contains("Rawat Jalan") Or Eklaim.txtRawat.Text.Contains("Igd") Then
            Try
                queryPoli = "SELECT * FROM t_unit WHERE unit = '" & txtRuang.Text & "'"
                cmdPoli = New MySqlCommand(queryPoli, conn)
                drPoli = cmdPoli.ExecuteReader

                While drPoli.Read
                    kdUnit = drPoli.GetString("kdUnit")
                End While
                drPoli.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        'MsgBox(kdUnit)

        Dim queryReg As String = ""
        Dim cmdReg As MySqlCommand
        Dim drReg As MySqlDataReader
        If Eklaim.txtRawat.Text.Contains("Rawat Jalan") Or Eklaim.txtRawat.Text.Contains("Igd") Then
            queryReg = "SELECT noRegistrasiRawatJalan AS NoReg
		               FROM t_registrasirawatjalan
	                  WHERE noDaftar = '" & Eklaim.noDaftar & "'
                        AND kdUnit = '" & kdUnit & "'"
        ElseIf Eklaim.txtRawat.Text.Contains("Rawat Inap") Then
            queryReg = "SELECT noDaftarRawatInap AS NoReg
		               FROM vw_daftarruangakomodasi
	                  WHERE noDaftar = '" & Eklaim.noDaftar & "'
                        AND rawatInap = '" & txtRuang.Text & "'"
        End If
        'MsgBox(queryReg)
        Try
            cmdReg = New MySqlCommand(queryReg, conn)
            drReg = cmdReg.ExecuteReader

            While drReg.Read
                txtNoRanap.Text = drReg.GetString("NoReg")
            End While
            drReg.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()

    End Sub

    Private Sub txtNoRanap_TextChanged(sender As Object, e As EventArgs) Handles txtNoRanap.TextChanged
        If txtNoRanap.Text <> "NoRanap" Then
            'Dll
            If Eklaim.txtRawat.Text.Contains("Rawat Jalan") Or Eklaim.txtRawat.Text.Contains("Igd") Then
                Call detailJpRekapRajal()
            ElseIf Eklaim.txtRawat.Text.Contains("Rawat Inap") Then
                Call detailJpRekapRanap()
                'Akomodasi
                Call detailJpAkomodasi()
                Call totalTarifAkomodasi()
                'IGD
                Call totalTarifIgdKonsul()
                'Visite
                Call detailJpVisite()
                Call totalTarifVisite()
                'Konsul
                Call detailJpKonsul()
                Call totalTarifKonsul()
                'Operator
                Call detailJpOperator()
                Call detailJpOpeParu()
                'DPJP
                Call detailJpDPJP()
                'Tenaga Ahli
                Call detailJpGizi()
                Call totalTarifGizi()
                Call detailJpFarklin()
                Call totalTarifFarklin()
                Call detailJpFisio()
                Call totalTarifFisio()
            End If

            txtTotalRincian.Text = (Val(CInt(txtTotAdmPoli.Text)) + Val(CInt(txtTotAkomodasi.Text)) + Val(CInt(txtTotIcu.Text)) +
                                    Val(CInt(txtTotHcu.Text)) + Val(CInt(txtTotPicu.Text)) + Val(CInt(txtTotNicu.Text)) + Val(CInt(txtTotDrIgd.Text)) +
                                    Val(CInt(txtTotKonsulPoli.Text)) + Val(CInt(txtTotVisite.Text)) + Val(CInt(txtTotKonsulRanap.Text)) + Val(CInt(txtTotOpe.Text)) +
                                    Val(CInt(txtTotEndos.Text)) + Val(CInt(txtTotBronc.Text)) + Val(CInt(txtTotCathLab.Text)) + Val(CInt(txtTotHemo.Text)) +
                                    Val(CInt(txtTotCvc.Text)) + Val(CInt(txtTotLain.Text)) + Val(CInt(txtTotGizi.Text)) + Val(CInt(txtTotFarklin.Text)) +
                                    Val(CInt(txtTotFisio.Text)) + Val(CInt(txtTotObat.Text)) + Val(CInt(txtTotAlkes.Text)) + Val(CInt(txtTotOxy.Text)) +
                                    Val(CInt(txtTotKassa.Text)) + Val(CInt(txtTotVenti.Text)) + Val(CInt(txtTotNebul.Text)) + Val(CInt(txtTotSyr.Text)) +
                                    Val(CInt(txtTotMonitor.Text)) + Val(CInt(txtTotRontgen.Text)) + Val(CInt(txtTotUsg.Text)) + Val(CInt(txtTotCtscan.Text)) +
                                    Val(CInt(txtTotMri.Text)) + Val(CInt(txtTotLabPK.Text)) + Val(CInt(txtTotLabPA.Text)) + Val(CInt(txtTotDarah.Text)) +
                                    Val(CInt(txtTotEcg.Text)) + Val(CInt(txtTotEcho.Text)) + Val(CInt(txtTotEcho.Text)) + Val(CInt(txtTotRehab.Text)) +
                                    Val(CInt(txtTotTindakan.Text)) + Val(CInt(txtTotAskep.Text)) + Val(CInt(txtTotRohani.Text)) + Val(CInt(txtTotDpjp.Text)) +
                                    Val(CInt(txtTotJenazah.Text)) + Val(CInt(txtTotTreadmill.Text)) + Val(CInt(txtInacbg.Text))).ToString("#,##0")
        End If
    End Sub

    Private Sub picVisit_Click(sender As Object, e As EventArgs) Handles picVisit.Click
        AddDokter.Ambil_Data = True
        AddDokter.Form_Ambil_Data = "VisiteRanap"
        AddDokter.ShowDialog()
    End Sub

    Private Sub picKonsul_Click(sender As Object, e As EventArgs) Handles picKonsul.Click
        AddDokter.Ambil_Data = True
        AddDokter.Form_Ambil_Data = "KonsulRanap"
        AddDokter.ShowDialog()
    End Sub

    Private Sub picKonsulIgd_Click(sender As Object, e As EventArgs) Handles picKonsulIgd.Click
        AddDokter.Ambil_Data = True
        AddDokter.Form_Ambil_Data = "KonsulIgd"
        AddDokter.ShowDialog()
    End Sub

    Private Sub txtRuang_MouseWheel(sender As Object, e As MouseEventArgs) Handles txtRuang.MouseWheel
        Dim HMEA As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
        HMEA.Handled = True
    End Sub

    Private Sub dgvAkomodasi_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvAkomodasi.RowPostPaint
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

    Private Sub dgvDrIgdKonsul_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvDrIgdKonsul.RowPostPaint
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

    Private Sub dgvDrVisite_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvDrVisite.RowPostPaint
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

    Private Sub dgvDrKonsulRanap_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvDrKonsulRanap.RowPostPaint
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

    Private Sub dgvGizi_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvGizi.RowPostPaint
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

    Private Sub dgvFarklin_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvFarklin.RowPostPaint
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

    Private Sub dgvFisio_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvFisio.RowPostPaint
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
#Region "JP Akomodasi"
    Sub detailJpAkomodasi()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader


        query = "SELECT rawatInap, tarifKmr,
                            jumlahHariMenginap, totalMenginap, kelas
                   FROM vw_daftarruangakomodasi
                  WHERE noDaftarRawatInap = '" & txtNoRanap.Text & "'
                    AND (rawatInap NOT LIKE '%ICU%' 
                        AND rawatInap NOT LIKE '%HCU%' 
                        AND rawatInap NOT LIKE '%NICU%' 
                        AND rawatInap NOT LIKE '%PICU%'
                        AND rawatInap NOT LIKE '%LAVENDER TANPA VENTILATOR%'
                        AND rawatInap NOT LIKE '%LAVENDER VENTILATOR%')"

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvAkomodasi.Rows.Clear()
            Do While dr.Read
                dgvAkomodasi.Rows.Add(dr.Item("rawatInap"), dr.Item("kelas"), dr.Item("tarifKmr"),
                                      dr.Item("jumlahHariMenginap"), dr.Item("totalMenginap"))
            Loop

            dr.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Detail JP Akomodasi", MessageBoxButtons.OK)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "JP Visite"
    Sub detailJpVisite()
        Call koneksiServer()
        Dim queryVi As String = ""
        Dim cmdVi As MySqlCommand
        Dim drVi As MySqlDataReader

        queryVi = "SELECT dtri.tindakan AS Tindakan,
	                    PPA.namapetugasMedis AS PPA,
	                    SUM(dtri.jumlahTindakan) AS Jml,
	                    dtri.tarif AS Tarif,
	                    (SUM(dtri.jumlahTindakan)*dtri.tarif) AS Total
	               FROM t_tindakanpasienranap AS tri
	         INNER JOIN t_detailtindakanpasienranap AS dtri ON tri.noTindakanPasienRanap = dtri.noTindakanPasienRanap
	         INNER JOIN t_registrasirawatinap AS ri ON tri.noDaftarRawatInap = ri.noDaftarRawatInap
	         INNER JOIN t_registrasi AS reg ON reg.noDaftar = ri.noDaftar
	         INNER JOIN t_tenagamedis2 AS DPJP ON reg.kdTenagaMedis = DPJP.kdPetugasMedis
	         INNER JOIN t_tenagamedis2 AS PPA ON dtri.kdTenagaMedis = PPA.kdPetugasMedis
	              WHERE ri.noDaftarRawatInap = '" & txtNoRanap.Text & "'
	                AND (dtri.tindakan LIKE '%VISITE%')
	                AND PPA.namapetugasMedis LIKE 'dr%'
	           GROUP BY ri.noDaftarRawatInap,dtri.kdTarif,PPA.namapetugasMedis"

        Try
            cmdVi = New MySqlCommand(queryVi, conn)
            drVi = cmdVi.ExecuteReader
            dgvDrVisite.Rows.Clear()
            Do While drVi.Read
                dgvDrVisite.Rows.Add(drVi.Item("PPA"), drVi.Item("Jml"), drVi.Item("Total"), drVi.Item("Tarif"), drVi.Item("Tindakan"))
            Loop
            drVi.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail JP Visite", MsgBoxStyle.Exclamation)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "JP Konsul"
    Sub detailJpKonsul()
        Call koneksiServer()
        Dim queryKo As String = ""
        Dim cmdKo As MySqlCommand
        Dim drKo As MySqlDataReader

        queryKo = "SELECT dtri.tindakan AS Tindakan,
	                    PPA.namapetugasMedis AS PPA,
	                    SUM(dtri.jumlahTindakan) AS Jml,
	                    dtri.tarif AS Tarif,
	                    (SUM(dtri.jumlahTindakan)*dtri.tarif) AS Total
	               FROM t_tindakanpasienranap AS tri
	         INNER JOIN t_detailtindakanpasienranap AS dtri ON tri.noTindakanPasienRanap = dtri.noTindakanPasienRanap
	         INNER JOIN t_registrasirawatinap AS ri ON tri.noDaftarRawatInap = ri.noDaftarRawatInap
	         INNER JOIN t_registrasi AS reg ON reg.noDaftar = ri.noDaftar
	         INNER JOIN t_tenagamedis2 AS DPJP ON reg.kdTenagaMedis = DPJP.kdPetugasMedis
	         INNER JOIN t_tenagamedis2 AS PPA ON dtri.kdTenagaMedis = PPA.kdPetugasMedis
	              WHERE ri.noDaftarRawatInap = '" & txtNoRanap.Text & "'
	                AND (dtri.tindakan LIKE '%KONSULTASI%')
	                AND PPA.namapetugasMedis LIKE 'dr%'
	              GROUP BY ri.noDaftarRawatInap,dtri.kdTarif,PPA.namapetugasMedis"

        Try
            cmdKo = New MySqlCommand(queryKo, conn)
            drKo = cmdKo.ExecuteReader
            dgvDrKonsulRanap.Rows.Clear()
            Do While drKo.Read
                dgvDrKonsulRanap.Rows.Add(drKo.Item("PPA"), drKo.Item("Jml"), drKo.Item("Total"), drKo.Item("Tarif"), drKo.Item("Tindakan"))
            Loop
            drKo.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail JP Visite", MsgBoxStyle.Exclamation)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "JP DPJP"
    Sub detailJpDPJP()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        query = "SELECT tindakan,DPJP,1,tarif,total
			       FROM 
                        (SELECT tindakan,tarif,tarif AS total 
				           FROM vw_caritindakan
						  WHERE kdTarif LIKE '0209%' AND kelas = '" & Eklaim.txtKelas.Text & "') AS tind,
						(SELECT reg.tglDaftar AS mrs, dpjp.namapetugasMedis AS DPJP
						   FROM t_registrasi AS reg
				          INNER JOIN t_tenagamedis2 AS dpjp ON reg.kdTenagaMedis = dpjp.kdPetugasMedis
					      WHERE reg.noDaftar = '" & Eklaim.noDaftar & "') AS dokter"

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                txtDpjp.Text = dr.Item("DPJP")
                txtTotDpjp.Text = CInt(dr.Item("total")).ToString("#,##0")
            End If
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail JP DPJP", MsgBoxStyle.Exclamation)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "JP Operator"
    Sub detailJpOperator()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        query = "SELECT COALESCE(TRIM(LEADING ';' FROM top.dokterOP),'-') AS operator,
                        COALESCE(SUBSTR(an.dokterAnestesi,2),'-') AS anestesi,
                        dtop.tindakan
                   FROM t_tindakanop AS top
                  INNER JOIN t_registrasiop AS rop ON rop.noRegistrasiOP = top.noRegistrasiOP
                  INNER JOIN t_registrasi AS reg ON reg.noDaftar = rop.noDaftarPasien
                  INNER JOIN t_tindakananestesi AS an ON rop.noRegistrasiOP = an.noRegistrasiOP 
                  INNER JOIN t_detailtindakanop AS dtop ON top.noTindakanOP = dtop.noTindakanOP
                  WHERE dtop.statusHapus = 0 
                    AND (tindakan != 'RR' AND kdTarif NOT LIKE '55%')
                    AND reg.noDaftar = '" & Eklaim.noDaftar & "'"

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                txtDrOperator.Text = dr.Item("operator")
                txtDrAnestesi.Text = dr.Item("anestesi")
                txtJenisOperasi.Text = dr.Item("tindakan")
            End If
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail JP Operator", MsgBoxStyle.Exclamation)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "JP OperatorParu"
    Sub detailJpOpeParu()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        query = "SELECT COALESCE(TRIM(LEADING ';' FROM dtparu.operator),'-') AS operator
                   FROM t_tindakanokparu AS tparu
             INNER JOIN t_detailtindakanokparu AS dtparu ON tparu.noTindakanOP = dtparu.noTindakanOP
             INNER JOIN t_registrasiokparu AS rparu ON rparu.noRegistrasiOP = tparu.noRegistrasiOP
             INNER JOIN t_registrasi AS reg ON reg.noDaftar = rparu.noDaftarPasien 
                  WHERE dtparu.statusHapus = 0 
                    AND reg.noDaftar = '" & Eklaim.noDaftar & "'"

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                txtDrParu.Text = dr.Item("operator")
            End If
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail JP Operator", MsgBoxStyle.Exclamation)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "JP PPA GIZI"
    Sub detailJpGizi()
        Call koneksiServer()
        Dim queryGz As String = ""
        Dim cmdGz As MySqlCommand
        Dim drGz As MySqlDataReader

        'If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
        '    query = "Call jpTenagaAhliRajal('" & noDaftar & "')"
        'ElseIf txtRawat.Text.Contains("Rawat Inap") Then
        '    query = "Call jpTenagaAhliRanap('" & noDaftar & "')"
        'End If
        queryGz = "SELECT dtri.tindakan AS Tindakan,
	                    PPA.namapetugasMedis AS PPA,
	                    SUM(dtri.jumlahTindakan) AS Jml,
	                    dtri.tarif AS Tarif,
	                    (SUM(dtri.jumlahTindakan)*dtri.tarif) AS Total
	               FROM t_tindakanpasienranap AS tri
	         INNER JOIN t_detailtindakanpasienranap AS dtri ON tri.noTindakanPasienRanap = dtri.noTindakanPasienRanap
	         INNER JOIN t_registrasirawatinap AS ri ON tri.noDaftarRawatInap = ri.noDaftarRawatInap
	         INNER JOIN t_registrasi AS reg ON reg.noDaftar = ri.noDaftar
             INNER JOIN t_tenagamedis2 AS DPJP ON reg.kdTenagaMedis = DPJP.kdPetugasMedis
	         INNER JOIN t_tenagamedis2 AS PPA ON dtri.kdTenagaMedis = PPA.kdPetugasMedis
	              WHERE ri.noDaftarRawatInap = '" & txtNoRanap.Text & "' 
	                AND tindakan LIKE 'JASA ASUHAN GIZI%'
               GROUP BY dtri.tindakan,PPA.namapetugasMedis"
        Try
            cmdGz = New MySqlCommand(queryGz, conn)
            drGz = cmdGz.ExecuteReader
            dgvGizi.Rows.Clear()
            Do While drGz.Read
                dgvGizi.Rows.Add(drGz.Item("Tindakan"), drGz.Item("Tarif"), drGz.Item("Jml"), drGz.Item("PPA"), drGz.Item("Total"))
            Loop

            drGz.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Detail PPA GIZI", MessageBoxButtons.OK)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "JP PPA FARKLIN"
    Sub detailJpFarklin()
        Call koneksiServer()
        Dim queryFa As String = ""
        Dim cmdFa As MySqlCommand
        Dim drFa As MySqlDataReader

        'If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
        '    query = "Call jpTenagaAhliRajal('" & noDaftar & "')"
        'ElseIf txtRawat.Text.Contains("Rawat Inap") Then
        '    query = "Call jpTenagaAhliRanap('" & noDaftar & "')"
        'End If
        queryFa = "SELECT dtri.tindakan AS Tindakan,
	                    PPA.namapetugasMedis AS PPA,
	                    SUM(dtri.jumlahTindakan) AS Jml,
	                    dtri.tarif AS Tarif,
	                    (SUM(dtri.jumlahTindakan)*dtri.tarif) AS Total
	               FROM t_tindakanpasienranap AS tri
	         INNER JOIN t_detailtindakanpasienranap AS dtri ON tri.noTindakanPasienRanap = dtri.noTindakanPasienRanap
	         INNER JOIN t_registrasirawatinap AS ri ON tri.noDaftarRawatInap = ri.noDaftarRawatInap
	         INNER JOIN t_registrasi AS reg ON reg.noDaftar = ri.noDaftar
             INNER JOIN t_tenagamedis2 AS DPJP ON reg.kdTenagaMedis = DPJP.kdPetugasMedis
	         INNER JOIN t_tenagamedis2 AS PPA ON dtri.kdTenagaMedis = PPA.kdPetugasMedis
	              WHERE ri.noDaftarRawatInap = '" & txtNoRanap.Text & "' 
	                AND tindakan LIKE 'JASA ASUHAN FARMASI%'
               GROUP BY dtri.tindakan,PPA.namapetugasMedis"
        Try
            cmdFa = New MySqlCommand(queryFa, conn)
            drFa = cmdFa.ExecuteReader
            dgvFarklin.Rows.Clear()
            Do While drFa.Read
                dgvFarklin.Rows.Add(drFa.Item("Tindakan"), drFa.Item("Tarif"), drFa.Item("Jml"), drFa.Item("PPA"), drFa.Item("Total"))
            Loop

            drFa.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Detail PPA FARKLIN", MessageBoxButtons.OK)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "JP PPA FISIO"
    Sub detailJpFisio()
        Call koneksiServer()
        Dim queryFis As String = ""
        Dim cmdFis As MySqlCommand
        Dim drFis As MySqlDataReader

        'If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
        '    query = "Call jpTenagaAhliRajal('" & noDaftar & "')"
        'ElseIf txtRawat.Text.Contains("Rawat Inap") Then
        '    query = "Call jpTenagaAhliRanap('" & noDaftar & "')"
        'End If
        queryFis = "SELECT dtri.tindakan AS Tindakan,
	                    PPA.namapetugasMedis AS PPA,
	                    SUM(dtri.jumlahTindakan) AS Jml,
	                    dtri.tarif AS Tarif,
	                    (SUM(dtri.jumlahTindakan)*dtri.tarif) AS Total
	               FROM t_tindakanpasienranap AS tri
	         INNER JOIN t_detailtindakanpasienranap AS dtri ON tri.noTindakanPasienRanap = dtri.noTindakanPasienRanap
	         INNER JOIN t_registrasirawatinap AS ri ON tri.noDaftarRawatInap = ri.noDaftarRawatInap
	         INNER JOIN t_registrasi AS reg ON reg.noDaftar = ri.noDaftar
             INNER JOIN t_tenagamedis2 AS DPJP ON reg.kdTenagaMedis = DPJP.kdPetugasMedis
	         INNER JOIN t_tenagamedis2 AS PPA ON dtri.kdTenagaMedis = PPA.kdPetugasMedis
	              WHERE ri.noDaftarRawatInap = '" & txtNoRanap.Text & "' 
	                AND tindakan LIKE 'FISIOTERAPI%'
               GROUP BY dtri.tindakan,PPA.namapetugasMedis"
        Try
            cmdFis = New MySqlCommand(queryFis, conn)
            drFis = cmdFis.ExecuteReader
            dgvFisio.Rows.Clear()
            Do While drFis.Read
                dgvFisio.Rows.Add(drFis.Item("Tindakan"), drFis.Item("Tarif"), drFis.Item("Jml"), drFis.Item("PPA"), drFis.Item("Total"))
            Loop

            drFis.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Detail PPA FISIO", MessageBoxButtons.OK)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "JP Rekap Ranap"
    Sub detailJpRekapRanap()
        Call koneksiServer()
        Dim queryRe As String = ""
        Dim cmdRe As MySqlCommand
        Dim drRe As MySqlDataReader

        queryRe = "Call rekapJPRanap('" & Eklaim.noDaftar & "','" & txtNoRanap.Text & "','" & Eklaim.txtNoRM.Text & "','" & Format(Eklaim.txtTglMskRawat.Value, "yyyy-MM-dd") & "','" & txtRuang.Text & "')"
        'MsgBox(queryRe)
        Try
            cmdRe = New MySqlCommand(queryRe, conn)
            drRe = cmdRe.ExecuteReader

            dgvDrVisite.Rows.Clear()
            dgvDrKonsulRanap.Rows.Clear()
            Do While drRe.Read
                dgvDrVisite.Rows.Add(drRe.Item("PPAVisite"), drRe.Item("jmlVisite"), drRe.Item("totalKonsul"))
                dgvDrKonsulRanap.Rows.Add(drRe.Item("PPAKonsul"), drRe.Item("jmlVisite"), drRe.Item("totalKonsul"))
            Loop

            'dr.Read()
            If drRe.HasRows Then
                txtTotIcu.Text = CInt(drRe.Item("icu")).ToString("#,##0")
                txtTotHcu.Text = CInt(drRe.Item("hcu")).ToString("#,##0")
                txtTotNicu.Text = CInt(drRe.Item("nicu")).ToString("#,##0")
                txtTotPicu.Text = CInt(drRe.Item("picu")).ToString("#,##0")
                txtTotOpe.Text = CInt(drRe.Item("ope")).ToString("#,##0")
                txtTotRR.Text = CInt(drRe.Item("rr")).ToString("#,##0")
                txtTotParu.Text = CInt(drRe.Item("paru")).ToString("#,##0")
                txtTotEndos.Text = CInt(drRe.Item("endos")).ToString("#,##0")
                txtTotBronc.Text = CInt(drRe.Item("bronc")).ToString("#,##0")
                txtTotCathLab.Text = CInt(drRe.Item("cathe")).ToString("#,##0")
                txtTotHemo.Text = CInt(drRe.Item("hemo")).ToString("#,##0")
                txtTotCvc.Text = CInt(drRe.Item("cvc")).ToString("#,##0")
                txtTotLain.Text = CInt(drRe.Item("ivp")).ToString("#,##0")
                txtTotObat.Text = CInt(drRe.Item("obat")).ToString("#,##0")
                txtTotAlkes.Text = CInt(drRe.Item("alkes")).ToString("#,##0")
                txtTotOxy.Text = CInt(drRe.Item("oksigen")).ToString("#,##0")
                txtTotKassa.Text = CInt(drRe.Item("kassa")).ToString("#,##0")
                txtTotVenti.Text = CInt(drRe.Item("venti")).ToString("#,##0")
                txtTotNebul.Text = CInt(drRe.Item("nebul")).ToString("#,##0")
                txtTotSyr.Text = CInt(drRe.Item("syringe")).ToString("#,##0")
                txtTotMonitor.Text = CInt(drRe.Item("monitor")).ToString("#,##0")
                txtTotRontgen.Text = CInt(drRe.Item("rontgen")).ToString("#,##0")
                txtTotUsg.Text = CInt(drRe.Item("USG")).ToString("#,##0")
                txtTotCtscan.Text = CInt(drRe.Item("ctscan")).ToString("#,##0")
                txtTotMri.Text = CInt(drRe.Item("mri")).ToString("#,##0")
                txtTotLabPK.Text = CInt(drRe.Item("PK")).ToString("#,##0")
                txtTotLabPA.Text = CInt(drRe.Item("PA")).ToString("#,##0")
                txtTotDarah.Text = CInt(drRe.Item("darah")).ToString("#,##0")
                txtTotEcg.Text = CInt(drRe.Item("ekg")).ToString("#,##0")
                txtTotEcho.Text = CInt(drRe.Item("echo")).ToString("#,##0")
                txtTotHolter.Text = CInt(drRe.Item("holter")).ToString("#,##0")
                txtTotRehab.Text = CInt(drRe.Item("rehab")).ToString("#,##0")
                txtTotTindakan.Text = CInt(drRe.Item("tindakan")).ToString("#,##0")
                txtTotAskep.Text = CInt(drRe.Item("askep")).ToString("#,##0")
                txtTotRohani.Text = CInt(drRe.Item("rohani")).ToString("#,##0")
            End If

            drRe.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Detail JP Rekap", MessageBoxButtons.OK)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "JP Rekap Rajal"
    Sub detailJpRekapRajal()
        Call koneksiServer()
        Dim queryRj As String = ""
        Dim cmdRj As MySqlCommand
        Dim drRj As MySqlDataReader

        queryRj = "Call rekapJPRajal('" & Eklaim.noDaftar & "')"
        'MsgBox(queryRj)
        Try
            cmdRj = New MySqlCommand(queryRj, conn)
            drRj = cmdRj.ExecuteReader

            drRj.Read()
            If drRj.HasRows Then
                txtAdminPoli.Text = drRj.Item("unit")
                txtDrIgd.Text = drRj.Item("dokter")
                txtTotAdmPoli.Text = CInt(drRj.Item("admin")).ToString("#,##0")
                txtTotDrIgd.Text = CInt(drRj.Item("visite")).ToString("#,##0")
                txtTotEndos.Text = CInt(drRj.Item("endos")).ToString("#,##0")
                txtTotBronc.Text = CInt(drRj.Item("bronc")).ToString("#,##0")
                txtTotCathLab.Text = CInt(drRj.Item("cathe")).ToString("#,##0")
                txtTotHemo.Text = CInt(drRj.Item("hemo")).ToString("#,##0")
                txtTotCvc.Text = CInt(drRj.Item("cvc")).ToString("#,##0")
                txtTotLain.Text = CInt(drRj.Item("ivp")).ToString("#,##0")
                txtTotObat.Text = CInt(drRj.Item("obat")).ToString("#,##0")
                txtTotOxy.Text = CInt(drRj.Item("oksigen")).ToString("#,##0")
                txtTotKassa.Text = CInt(drRj.Item("kassa")).ToString("#,##0")
                txtTotVenti.Text = CInt(drRj.Item("venti")).ToString("#,##0")
                txtTotNebul.Text = CInt(drRj.Item("nebul")).ToString("#,##0")
                txtTotSyr.Text = CInt(drRj.Item("syringe")).ToString("#,##0")
                txtTotRontgen.Text = CInt(drRj.Item("rontgen")).ToString("#,##0")
                txtTotUsg.Text = CInt(drRj.Item("USG")).ToString("#,##0")
                txtTotCtscan.Text = CInt(drRj.Item("ctscan")).ToString("#,##0")
                txtTotMri.Text = CInt(drRj.Item("mri")).ToString("#,##0")
                txtTotLabPK.Text = CInt(drRj.Item("PK")).ToString("#,##0")
                txtTotLabPA.Text = CInt(drRj.Item("PA")).ToString("#,##0")
                txtTotEcg.Text = CInt(drRj.Item("ekg")).ToString("#,##0")
                txtTotEcho.Text = CInt(drRj.Item("echo")).ToString("#,##0")
                txtTotHolter.Text = CInt(drRj.Item("holter")).ToString("#,##0")
                txtTotTindakan.Text = CInt(drRj.Item("tindakan")).ToString("#,##0")
            End If

            drRj.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Detail JP Rekap", MessageBoxButtons.OK)
        End Try
        conn.Close()
    End Sub
#End Region

#Region "Adm Poli"
    Private Sub txtTotAdmPoli_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotAdmPoli.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotAdmPoli_TextChanged(sender As Object, e As EventArgs) Handles txtTotAdmPoli.TextChanged
        If txtTotAdmPoli.Text = "" Then
            txtTotAdmPoli.Text = 0
        End If
        a = txtTotAdmPoli.Text
        txtTotAdmPoli.Text = Format(Val(a), "#,##0")
        txtTotAdmPoli.SelectionStart = Len(txtTotAdmPoli.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Akomodasi"
    Private Sub txtTotAkomodasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotAkomodasi.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotAkomodasi_TextChanged(sender As Object, e As EventArgs) Handles txtTotAkomodasi.TextChanged
        If txtTotAkomodasi.Text = "" Then
            txtTotAkomodasi.Text = 0
        End If
        c = txtTotAkomodasi.Text
        txtTotAkomodasi.Text = Format(Val(c), "#,##0")
        txtTotAkomodasi.SelectionStart = Len(txtTotAkomodasi.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub
#End Region
#Region "ICU"
    Private Sub txtTotIcu_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotIcu.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotIcu_TextChanged(sender As Object, e As EventArgs) Handles txtTotIcu.TextChanged
        If txtTotIcu.Text = "" Then
            txtTotIcu.Text = 0
        End If
        d = txtTotIcu.Text
        txtTotIcu.Text = Format(Val(d), "#,##0")
        txtTotIcu.SelectionStart = Len(txtTotIcu.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub
#End Region
#Region "HCU"
    Private Sub txtTotHcu_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotHcu.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotHcu_TextChanged(sender As Object, e As EventArgs) Handles txtTotHcu.TextChanged
        If txtTotHcu.Text = "" Then
            txtTotHcu.Text = 0
        End If
        ee = txtTotHcu.Text
        txtTotHcu.Text = Format(Val(ee), "#,##0")
        txtTotHcu.SelectionStart = Len(txtTotHcu.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub
#End Region
#Region "NICU"
    Private Sub txtTotNicu_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotNicu.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotNicu_TextChanged(sender As Object, e As EventArgs) Handles txtTotNicu.TextChanged
        If txtTotNicu.Text = "" Then
            txtTotNicu.Text = 0
        End If
        f = txtTotNicu.Text
        txtTotNicu.Text = Format(Val(f), "#,##0")
        txtTotNicu.SelectionStart = Len(txtTotNicu.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "PICU"
    Private Sub txtTotPicu_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotPicu.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotPicu_TextChanged(sender As Object, e As EventArgs) Handles txtTotPicu.TextChanged
        If txtTotPicu.Text = "" Then
            txtTotPicu.Text = 0
        End If
        g = txtTotPicu.Text
        txtTotPicu.Text = Format(Val(g), "#,##0")
        txtTotPicu.SelectionStart = Len(txtTotPicu.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub
#End Region
#Region "dr IGD"
    Private Sub TxtTotDrIgd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotDrIgd.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub TxtTotDrIgd_TextChanged(sender As Object, e As EventArgs) Handles txtTotDrIgd.TextChanged
        If txtTotDrIgd.Text = "" Then
            txtTotDrIgd.Text = 0
        End If
        h = txtTotDrIgd.Text
        txtTotDrIgd.Text = Format(Val(h), "#,##0")
        txtTotDrIgd.SelectionStart = Len(txtTotDrIgd.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub
#End Region
#Region "dr Poli"
    Private Sub txtTotKonsulPoli_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotKonsulPoli.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotKonsulPoli_TextChanged(sender As Object, e As EventArgs) Handles txtTotKonsulPoli.TextChanged
        If txtTotKonsulPoli.Text = "" Then
            txtTotKonsulPoli.Text = 0
        End If
        i = txtTotKonsulPoli.Text
        txtTotKonsulPoli.Text = Format(Val(i), "#,##0")
        txtTotKonsulPoli.SelectionStart = Len(txtTotKonsulPoli.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Visite"
    Private Sub txtTotVisite_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotVisite.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotVisite_TextChanged(sender As Object, e As EventArgs) Handles txtTotVisite.TextChanged
        If txtTotVisite.Text = "" Then
            txtTotVisite.Text = 0
        End If
        j = txtTotVisite.Text
        txtTotVisite.Text = Format(Val(j), "#,##0")
        txtTotVisite.SelectionStart = Len(txtTotVisite.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Konsultasi"
    Private Sub txtTotKonsulRanap_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotKonsulRanap.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotKonsulRanap_TextChanged(sender As Object, e As EventArgs) Handles txtTotKonsulRanap.TextChanged
        If txtTotKonsulRanap.Text = "" Then
            txtTotKonsulRanap.Text = 0
        End If
        k = txtTotKonsulRanap.Text
        txtTotKonsulRanap.Text = Format(Val(k), "#,##0")
        txtTotKonsulRanap.SelectionStart = Len(txtTotKonsulRanap.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Operasi"
    Private Sub txtTotOpe_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotOpe.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotOpe_TextChanged(sender As Object, e As EventArgs) Handles txtTotOpe.TextChanged
        If txtTotOpe.Text = "" Then
            txtTotOpe.Text = 0
        End If
        l = txtTotOpe.Text
        txtTotOpe.Text = Format(Val(l), "#,##0")
        txtTotOpe.SelectionStart = Len(txtTotOpe.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub
#End Region
#Region "Endos"
    Private Sub txtTotEndos_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotEndos.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotEndos_TextChanged(sender As Object, e As EventArgs) Handles txtTotEndos.TextChanged
        If txtTotEndos.Text = "" Then
            txtTotEndos.Text = 0
        End If
        m = txtTotEndos.Text
        txtTotEndos.Text = Format(Val(m), "#,##0")
        txtTotEndos.SelectionStart = Len(txtTotEndos.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Bronc"
    Private Sub txtTotBronc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotBronc.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotBronc_TextChanged(sender As Object, e As EventArgs) Handles txtTotBronc.TextChanged
        If txtTotBronc.Text = "" Then
            txtTotBronc.Text = 0
        End If
        n = txtTotBronc.Text
        txtTotBronc.Text = Format(Val(n), "#,##0")
        txtTotBronc.SelectionStart = Len(txtTotBronc.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Cath Lab"
    Private Sub txtTotCathLab_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotCathLab.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotCathLab_TextChanged(sender As Object, e As EventArgs) Handles txtTotCathLab.TextChanged
        If txtTotCathLab.Text = "" Then
            txtTotCathLab.Text = 0
        End If
        o = txtTotCathLab.Text
        txtTotCathLab.Text = Format(Val(o), "#,##0")
        txtTotCathLab.SelectionStart = Len(txtTotCathLab.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Hemo"
    Private Sub txtTotHemo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotHemo.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotHemo_TextChanged(sender As Object, e As EventArgs) Handles txtTotHemo.TextChanged
        If txtTotHemo.Text = "" Then
            txtTotHemo.Text = 0
        End If
        p = txtTotHemo.Text
        txtTotHemo.Text = Format(Val(p), "#,##0")
        txtTotHemo.SelectionStart = Len(txtTotHemo.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Cvc"
    Private Sub txtTotCvc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotCvc.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotCvc_TextChanged(sender As Object, e As EventArgs) Handles txtTotCvc.TextChanged
        If txtTotCvc.Text = "" Then
            txtTotCvc.Text = 0
        End If
        q = txtTotCvc.Text
        txtTotCvc.Text = Format(Val(q), "#,##0")
        txtTotCvc.SelectionStart = Len(txtTotCvc.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Lain2"
    Private Sub txtTotLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotLain.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotLain_TextChanged(sender As Object, e As EventArgs) Handles txtTotLain.TextChanged
        If txtTotLain.Text = "" Then
            txtTotLain.Text = 0
        End If
        r = txtTotLain.Text
        txtTotLain.Text = Format(Val(r), "#,##0")
        txtTotLain.SelectionStart = Len(txtTotLain.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Gizi"
    Private Sub txtTotGizi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotGizi.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotGizi_TextChanged(sender As Object, e As EventArgs) Handles txtTotGizi.TextChanged
        If txtTotGizi.Text = "" Then
            txtTotGizi.Text = 0
        End If
        s = txtTotGizi.Text
        txtTotGizi.Text = Format(Val(s), "#,##0")
        txtTotGizi.SelectionStart = Len(txtTotGizi.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Farklin"
    Private Sub txtTotFarklin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotFarklin.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotFarklin_TextChanged(sender As Object, e As EventArgs) Handles txtTotFarklin.TextChanged
        If txtTotFarklin.Text = "" Then
            txtTotFarklin.Text = 0
        End If
        t = txtTotFarklin.Text
        txtTotFarklin.Text = Format(Val(t), "#,##0")
        txtTotFarklin.SelectionStart = Len(txtTotFarklin.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Fisio"
    Private Sub txtTotFisio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotFisio.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotFisio_TextChanged(sender As Object, e As EventArgs) Handles txtTotFisio.TextChanged
        If txtTotFisio.Text = "" Then
            txtTotFisio.Text = 0
        End If
        t = txtTotFisio.Text
        txtTotFisio.Text = Format(Val(t), "#,##0")
        txtTotFisio.SelectionStart = Len(txtTotFisio.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Obat"
    Private Sub txtTotObat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotObat.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotObat_TextChanged(sender As Object, e As EventArgs) Handles txtTotObat.TextChanged
        If txtTotObat.Text = "" Then
            txtTotObat.Text = 0
        End If
        t = txtTotObat.Text
        txtTotObat.Text = Format(Val(t), "#,##0")
        txtTotObat.SelectionStart = Len(txtTotObat.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Alkes"
    Private Sub txtTotAlkes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotAlkes.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotAlkes_TextChanged(sender As Object, e As EventArgs) Handles txtTotAlkes.TextChanged
        If txtTotAlkes.Text = "" Then
            txtTotAlkes.Text = 0
        End If
        u = txtTotAlkes.Text
        txtTotAlkes.Text = Format(Val(u), "#,##0")
        txtTotAlkes.SelectionStart = Len(txtTotAlkes.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Oksigen"
    Private Sub txtTotOxy_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotOxy.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotOxy_TextChanged(sender As Object, e As EventArgs) Handles txtTotOxy.TextChanged
        If txtTotOxy.Text = "" Then
            txtTotOxy.Text = 0
        End If
        v = txtTotOxy.Text
        txtTotOxy.Text = Format(Val(v), "#,##0")
        txtTotOxy.SelectionStart = Len(txtTotOxy.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Kassa"
    Private Sub txtTotKassa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotKassa.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotKassa_TextChanged(sender As Object, e As EventArgs) Handles txtTotKassa.TextChanged
        If txtTotKassa.Text = "" Then
            txtTotKassa.Text = 0
        End If
        w = txtTotKassa.Text
        txtTotKassa.Text = Format(Val(w), "#,##0")
        txtTotKassa.SelectionStart = Len(txtTotKassa.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Venti"
    Private Sub txtTotVenti_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotVenti.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotVenti_TextChanged(sender As Object, e As EventArgs) Handles txtTotVenti.TextChanged
        If txtTotVenti.Text = "" Then
            txtTotVenti.Text = 0
        End If
        x = txtTotVenti.Text
        txtTotVenti.Text = Format(Val(x), "#,##0")
        txtTotVenti.SelectionStart = Len(txtTotVenti.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Nebul"
    Private Sub txtTotNebul_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotNebul.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotNebul_TextChanged(sender As Object, e As EventArgs) Handles txtTotNebul.TextChanged
        If txtTotNebul.Text = "" Then
            txtTotNebul.Text = 0
        End If
        y = txtTotNebul.Text
        txtTotNebul.Text = Format(Val(y), "#,##0")
        txtTotNebul.SelectionStart = Len(txtTotNebul.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Syringe"
    Private Sub txtTotSyr_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotSyr.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotSyr_TextChanged(sender As Object, e As EventArgs) Handles txtTotSyr.TextChanged
        If txtTotSyr.Text = "" Then
            txtTotSyr.Text = 0
        End If
        z = txtTotSyr.Text
        txtTotSyr.Text = Format(Val(z), "#,##0")
        txtTotSyr.SelectionStart = Len(txtTotSyr.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Monitor"
    Private Sub txtTotMonitor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotMonitor.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotMonitor_TextChanged(sender As Object, e As EventArgs) Handles txtTotMonitor.TextChanged
        If txtTotMonitor.Text = "" Then
            txtTotMonitor.Text = 0
        End If
        a1 = txtTotMonitor.Text
        txtTotMonitor.Text = Format(Val(a1), "#,##0")
        txtTotMonitor.SelectionStart = Len(txtTotMonitor.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Rontgen"
    Private Sub txtTotRontgen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotRontgen.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotRontgen_TextChanged(sender As Object, e As EventArgs) Handles txtTotRontgen.TextChanged
        If txtTotRontgen.Text = "" Then
            txtTotRontgen.Text = 0
        End If
        b1 = txtTotRontgen.Text
        txtTotRontgen.Text = Format(Val(b1), "#,##0")
        txtTotRontgen.SelectionStart = Len(txtTotRontgen.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "USG"
    Private Sub txtTotUsg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotUsg.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotUsg_TextChanged(sender As Object, e As EventArgs) Handles txtTotUsg.TextChanged
        If txtTotUsg.Text = "" Then
            txtTotUsg.Text = 0
        End If
        c1 = txtTotUsg.Text
        txtTotUsg.Text = Format(Val(c1), "#,##0")
        txtTotUsg.SelectionStart = Len(txtTotUsg.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "CT SCAN"
    Private Sub txtTotCtscan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotCtscan.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotCtscan_TextChanged(sender As Object, e As EventArgs) Handles txtTotCtscan.TextChanged
        If txtTotCtscan.Text = "" Then
            txtTotCtscan.Text = 0
        End If
        d1 = txtTotCtscan.Text
        txtTotCtscan.Text = Format(Val(d1), "#,##0")
        txtTotCtscan.SelectionStart = Len(txtTotCtscan.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "MRI"
    Private Sub txtTotMri_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotMri.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotMri_TextChanged(sender As Object, e As EventArgs) Handles txtTotMri.TextChanged
        If txtTotMri.Text = "" Then
            txtTotMri.Text = 0
        End If
        e1 = txtTotMri.Text
        txtTotMri.Text = Format(Val(e1), "#,##0")
        txtTotMri.SelectionStart = Len(txtTotMri.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub
#End Region
#Region "LAB PK"
    Private Sub txtTotLabPK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotLabPK.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotLabPK_TextChanged(sender As Object, e As EventArgs) Handles txtTotLabPK.TextChanged
        If txtTotLabPK.Text = "" Then
            txtTotLabPK.Text = 0
        End If
        f1 = txtTotLabPK.Text
        txtTotLabPK.Text = Format(Val(f1), "#,##0")
        txtTotLabPK.SelectionStart = Len(txtTotLabPK.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "LAB PA"
    Private Sub txtTotLabPA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotLabPA.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotLabPA_TextChanged(sender As Object, e As EventArgs) Handles txtTotLabPA.TextChanged
        If txtTotLabPA.Text = "" Then
            txtTotLabPA.Text = 0
        End If
        g1 = txtTotLabPA.Text
        txtTotLabPA.Text = Format(Val(g1), "#,##0")
        txtTotLabPA.SelectionStart = Len(txtTotLabPA.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Darah"
    Private Sub txtTotDarah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotDarah.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotDarah_TextChanged(sender As Object, e As EventArgs) Handles txtTotDarah.TextChanged
        If txtTotDarah.Text = "" Then
            txtTotDarah.Text = 0
        End If
        h1 = txtTotDarah.Text
        txtTotDarah.Text = Format(Val(h1), "#,##0")
        txtTotDarah.SelectionStart = Len(txtTotDarah.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "ECG"
    Private Sub txtTotEcg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotEcg.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotEcg_TextChanged(sender As Object, e As EventArgs) Handles txtTotEcg.TextChanged
        If txtTotEcg.Text = "" Then
            txtTotEcg.Text = 0
        End If
        i1 = txtTotEcg.Text
        txtTotEcg.Text = Format(Val(i1), "#,##0")
        txtTotEcg.SelectionStart = Len(txtTotEcg.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Echo"
    Private Sub txtTotEcho_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotEcho.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotEcho_TextChanged(sender As Object, e As EventArgs) Handles txtTotEcho.TextChanged
        If txtTotEcho.Text = "" Then
            txtTotEcho.Text = 0
        End If
        j1 = txtTotEcho.Text
        txtTotEcho.Text = Format(Val(j1), "#,##0")
        txtTotEcho.SelectionStart = Len(txtTotEcho.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Holter"
    Private Sub txtTotHolter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotHolter.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotHolter_TextChanged(sender As Object, e As EventArgs) Handles txtTotHolter.TextChanged
        If txtTotHolter.Text = "" Then
            txtTotHolter.Text = 0
        End If
        k1 = txtTotHolter.Text
        txtTotHolter.Text = Format(Val(k1), "#,##0")
        txtTotHolter.SelectionStart = Len(txtTotHolter.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Rehab"
    Private Sub txtTotRehab_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotRehab.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotRehab_TextChanged(sender As Object, e As EventArgs) Handles txtTotRehab.TextChanged
        If txtTotRehab.Text = "" Then
            txtTotRehab.Text = 0
        End If
        l1 = txtTotRehab.Text
        txtTotRehab.Text = Format(Val(l1), "#,##0")
        txtTotRehab.SelectionStart = Len(txtTotRehab.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Tindakan"
    Private Sub txtTotTindakan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotTindakan.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotTindakan_TextChanged(sender As Object, e As EventArgs) Handles txtTotTindakan.TextChanged
        If txtTotTindakan.Text = "" Then
            txtTotTindakan.Text = 0
        End If
        m1 = txtTotTindakan.Text
        txtTotTindakan.Text = Format(Val(m1), "#,##0")
        txtTotTindakan.SelectionStart = Len(txtTotTindakan.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Askep"
    Private Sub txtTotAskep_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotAskep.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotAskep_TextChanged(sender As Object, e As EventArgs) Handles txtTotAskep.TextChanged
        If txtTotAskep.Text = "" Then
            txtTotAskep.Text = 0
        End If
        n1 = txtTotAskep.Text
        txtTotAskep.Text = Format(Val(n1), "#,##0")
        txtTotAskep.SelectionStart = Len(txtTotAskep.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Rohani"
    Private Sub txtTotRohani_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotRohani.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotRohani_TextChanged(sender As Object, e As EventArgs) Handles txtTotRohani.TextChanged
        If txtTotRohani.Text = "" Then
            txtTotRohani.Text = 0
        End If
        o1 = txtTotRohani.Text
        txtTotRohani.Text = Format(Val(o1), "#,##0")
        txtTotRohani.SelectionStart = Len(txtTotRohani.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "DPJP"
    Private Sub txtTotDpjp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotDpjp.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotDpjp_TextChanged(sender As Object, e As EventArgs) Handles txtTotDpjp.TextChanged
        If txtTotDpjp.Text = "" Then
            txtTotDpjp.Text = 0
        End If
        p1 = txtTotDpjp.Text
        txtTotDpjp.Text = Format(Val(p1), "#,##0")
        txtTotDpjp.SelectionStart = Len(txtTotDpjp.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Jenazah"
    Private Sub txtTotJenazah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotJenazah.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotJenazah_TextChanged(sender As Object, e As EventArgs) Handles txtTotJenazah.TextChanged
        If txtTotJenazah.Text = "" Then
            txtTotJenazah.Text = 0
        End If
        q1 = txtTotJenazah.Text
        txtTotJenazah.Text = Format(Val(q1), "#,##0")
        txtTotJenazah.SelectionStart = Len(txtTotJenazah.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "Treadmill"
    Private Sub txtTotTreadmill_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotTreadmill.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotTreadmill_TextChanged(sender As Object, e As EventArgs) Handles txtTotTreadmill.TextChanged
        If txtTotTreadmill.Text = "" Then
            txtTotTreadmill.Text = 0
        End If
        r1 = txtTotTreadmill.Text
        txtTotTreadmill.Text = Format(Val(r1), "#,##0")
        txtTotTreadmill.SelectionStart = Len(txtTotTreadmill.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
#Region "INACBG"
    Private Sub txtInacbg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtInacbg.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtInacbg_TextChanged(sender As Object, e As EventArgs) Handles txtInacbg.TextChanged
        If txtInacbg.Text = "" Then
            txtInacbg.Text = 0
        End If
        s1 = txtInacbg.Text
        txtInacbg.Text = Format(Val(s1), "#,##0")
        txtInacbg.SelectionStart = Len(txtInacbg.Text)
        txtTotalRincian.Text = Format(a + b + c + d + ee + f + g + h + i + j + k + l +
                                      m + n + o + p + q + r + s + t + u + v + w + x +
                                      y + z + a1 + b1 + c1 + d1 + e1 + f1 + g1 + h1 + i1 + j1 +
                                      k1 + l1 + m1 + n1 + o1 + p1 + q1 + r1 + s1, "###,###")
    End Sub

#End Region
End Class