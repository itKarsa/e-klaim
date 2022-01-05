Imports MySql.Data.MySqlClient
Imports System.ComponentModel
Imports System.Threading
Public Class Berakdown

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Dim KoneksiString As String = "server=192.168.200.2;user=lis;password=lis1234;database=simrs;default command timeout=120;Convert Zero Datetime=True"
    Dim txtranap As String = Eklaim.txtRawat.Text.Contains("Rawat Inap")
    Dim txtrajal As String = Eklaim.txtRawat.Text.Contains("Rawat Jalan")
    Dim txtigd As String = Eklaim.txtRawat.Text.Contains("Igd")
    Private mySource As New AutoCompleteStringCollection
    Private myStringSource() As String

    Sub setColor(button As Button)
        btnHome.BackColor = Color.White
        btnEklaim.BackColor = Color.White
        btnBuku.BackColor = Color.White
        btnPiutang.BackColor = Color.White
        btnUmum.BackColor = Color.White
        btnTotal.BackColor = Color.White
        button.BackColor = Color.FromArgb(209, 232, 223)
    End Sub

    Dim aa, ab, ac, ad, ae, af,
        ag, ah, ai, aj, ak, al,
        am, an, ao, ap, aq, ar,
        a_s, at, au, av, aw, ax,
        ay, az, ba, bb, bc, bd,
        be, bf, bg, bh, bi, bj,
        bk, bl, bm, bn, bo, bp,
        bq, br, bs, bt, bu, bv As Integer

    Dim kdUnit As String

    Dim ci As IFormatProvider = New System.Globalization.CultureInfo("id-ID", True)

    Sub autoDokter()
        'Call koneksiServer()
        Dim cnDok As New MySqlConnection(KoneksiString)
        Dim cmd As New MySqlCommand("SELECT namapetugasMedis FROM t_tenagamedis2 WHERE kdKelompokTenagaMedis IN ('ktm1')", cnDok)
        Dim ad As New MySqlDataAdapter(cmd)
        Dim dt As New DataTable
        Dim tbl As New DataSet
        'Dim col As New AutoCompleteStringCollection

        'dt.Clear()
        Try
            ad.Fill(tbl, "t_tenagamedis2")
            ReDim myStringSource(tbl.Tables(0).Rows.Count - 1)
            Dim intIndex As Integer

            For Each row As DataRow In tbl.Tables(0).Rows
                myStringSource(intIndex) = row(0).ToString()
                intIndex += 1
            Next
            mySource.AddRange(myStringSource)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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
        Dim totTarifrj, totTarifri As Long
        totTarifrj = 0
        totTarifri = 0
        For i As Integer = 0 To dgvDrIgdKonsul.Rows.Count - 1
            totTarifrj = totTarifrj + Val(CLng(dgvDrIgdKonsul.Rows(i).Cells(2).Value))
        Next
        txtTotKonsulPoli.Text = CLng(totTarifrj).ToString("#,##0")

        For i As Integer = 0 To dgvDrKonsulRanap.Rows.Count - 1
            totTarifri = totTarifri + Val(CLng(dgvDrKonsulRanap.Rows(i).Cells(2).Value))
        Next
        txtTotKonsulRanap.Text = CLng(totTarifri).ToString("#,##0")
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
	                  WHERE rj.noDaftar = '" & noRegister & "'"
        ElseIf Eklaim.txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT rawatInap AS unit
		               FROM vw_daftarruangakomodasi
	                  WHERE noDaftar = '" & noRegister & "'"
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

        If Eklaim.txtJaminan.Text.Equals("Pasien Bayar", StringComparison.OrdinalIgnoreCase) Or Eklaim.txtJaminan.Text.Equals("Umum", StringComparison.OrdinalIgnoreCase) Then
            strJpRj = "INSERT INTO t_eklaimjprajalumum(noRM,NoSep,tglMasuk,tglKeluar,namaPasien,
                                                       unit,dokter,drOperator,drAnestesi,admin,
                                                       visite,konsultasi,prosedurbedah,endoscopy,bronkoscopy,
                                                       hd,cvc,ivp,paru,nonbedahlain,
                                                       gizi,fisioterapi,ecg,holter,treadmill,
                                                       echocardio,usg,rontgen,ctscan,mri,
                                                       labpa,labpk,darah,obat,alkes,
                                                       oksigen,kassa,tindakan,ventilator,nebulizer,
                                                       syringe,total,tarifinacbg) 
                                                VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                        '" & Format(CDate(Eklaim.txtTglKlrRawat.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "','" & txtAdminPoli.Text & "',
                                                        '" & txtDrIgd.Text & "','" & txtDrOperator.Text & "','" & txtDrAnestesi.Text & "',
                                                        '" & Convert.ToDouble(txtTotAdmPoli.Text, ci) & "','-','-',
                                                        '" & Convert.ToDouble(Val(txtTotOpe.Text + txtTotRR.Text), ci) & "','" & Convert.ToDouble(txtTotEndos.Text, ci) & "','" & Convert.ToDouble(txtTotBronc.Text, ci) & "',
                                                        '" & Convert.ToDouble(txtTotHemo.Text, ci) & "','" & Convert.ToDouble(txtTotCvc.Text, ci) & "','" & Convert.ToDouble(txtTotLain.Text, ci) & "',
                                                        '" & Convert.ToDouble(txtTotParu.Text, ci) & "','" & Convert.ToDouble(txtTotCathLab.Text, ci) & "','" & Convert.ToDouble(txtTotGizi.Text, ci) & "',
                                                        '" & Convert.ToDouble(txtTotFisio.Text, ci) & "','" & Convert.ToDouble(txtTotEcg.Text, ci) & "','" & Convert.ToDouble(txtTotHolter.Text, ci) & "',
                                                        '" & Convert.ToDouble(txtTotTreadmill.Text, ci) & "','" & Convert.ToDouble(txtTotEcho.Text, ci) & "','" & Convert.ToDouble(txtTotUsg.Text, ci) & "',
                                                        '" & Convert.ToDouble(txtTotRontgen.Text, ci) & "','" & Convert.ToDouble(txtTotCtscan.Text, ci) & "','" & Convert.ToDouble(txtTotMri.Text, ci) & "',
                                                        '" & Convert.ToDouble(txtTotLabPA.Text, ci) & "','" & Convert.ToDouble(txtTotLabPK.Text, ci) & "','" & Convert.ToDouble(txtTotDarah.Text, ci) & "',
                                                        '" & Convert.ToDouble(txtTotObat.Text, ci) & "','" & Convert.ToDouble(txtTotAlkes.Text, ci) & "','" & Convert.ToDouble(txtTotOxy.Text, ci) & "',
                                                        '" & Convert.ToDouble(txtTotKassa.Text, ci) & "','" & Convert.ToDouble(txtTotTindakan.Text, ci) & "','" & Convert.ToDouble(txtTotVenti.Text, ci) & "',
                                                        '" & Convert.ToDouble(txtTotNebul.Text, ci) & "','" & Convert.ToDouble(txtTotSyr.Text, ci) & "',
                                                        '" & Val(Convert.ToDouble(txtTotalRincian.Text, ci) - Val(Convert.ToDouble(txtTotDrIgd.Text, ci) + Convert.ToDouble(txtTotKonsulPoli.Text, ci))) & "',
                                                        '" & Convert.ToDouble(txtInacbg.Text, ci) & "')"
        Else
            strJpRj = "INSERT INTO t_eklaimjprajal(noRM,NoSep,tglMasuk,tglKeluar,namaPasien,
                                                    unit,dokter,drOperator,drAnestesi,admin,
                                                    visite,konsultasi,prosedurbedah,endoscopy,bronkoscopy,
                                                    hd,cvc,ivp,paru,nonbedahlain,
                                                    gizi,fisioterapi,ecg,holter,treadmill,
                                                    echocardio,usg,rontgen,ctscan,mri,
                                                    labpa,labpk,darah,obat,alkes,
                                                    oksigen,kassa,tindakan,ventilator,nebulizer,
                                                    syringe,total,tarifinacbg) 
                                            VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                    '" & Format(CDate(Eklaim.txtTglKlrRawat.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "','" & txtAdminPoli.Text & "',
                                                    '" & txtDrIgd.Text & "','" & txtDrOperator.Text & "','" & txtDrAnestesi.Text & "',
                                                    '" & Convert.ToDouble(txtTotAdmPoli.Text, ci) & "','-','-',
                                                    '" & Convert.ToDouble(Val(txtTotOpe.Text + txtTotRR.Text), ci) & "','" & Convert.ToDouble(txtTotEndos.Text, ci) & "','" & Convert.ToDouble(txtTotBronc.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotHemo.Text, ci) & "','" & Convert.ToDouble(txtTotCvc.Text, ci) & "','" & Convert.ToDouble(txtTotLain.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotParu.Text, ci) & "','" & Convert.ToDouble(txtTotCathLab.Text, ci) & "','" & Convert.ToDouble(txtTotGizi.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotFisio.Text, ci) & "','" & Convert.ToDouble(txtTotEcg.Text, ci) & "','" & Convert.ToDouble(txtTotHolter.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotTreadmill.Text, ci) & "','" & Convert.ToDouble(txtTotEcho.Text, ci) & "','" & Convert.ToDouble(txtTotUsg.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotRontgen.Text, ci) & "','" & Convert.ToDouble(txtTotCtscan.Text, ci) & "','" & Convert.ToDouble(txtTotMri.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotLabPA.Text, ci) & "','" & Convert.ToDouble(txtTotLabPK.Text, ci) & "','" & Convert.ToDouble(txtTotDarah.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotObat.Text, ci) & "','" & Convert.ToDouble(txtTotAlkes.Text, ci) & "','" & Convert.ToDouble(txtTotOxy.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotKassa.Text, ci) & "','" & Convert.ToDouble(txtTotTindakan.Text, ci) & "','" & Convert.ToDouble(txtTotVenti.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotNebul.Text, ci) & "','" & Convert.ToDouble(txtTotSyr.Text, ci) & "',
                                                    '" & Val(Convert.ToDouble(txtTotalRincian.Text, ci) - Val(Convert.ToDouble(txtTotDrIgd.Text, ci) + Convert.ToDouble(txtTotKonsulPoli.Text, ci))) & "',
                                                    '" & Convert.ToDouble(txtInacbg.Text, ci) & "')"
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

        If Eklaim.txtJaminan.Text.Equals("Pasien Bayar", StringComparison.OrdinalIgnoreCase) Or Eklaim.txtJaminan.Text.Equals("Umum", StringComparison.OrdinalIgnoreCase) Then
            strJpR = "INSERT INTO t_eklaimjpranapumum(noRM,NoSep,tglMasuk,tglKeluar,namaPasien,
                                                     unit,hakKelas,kelas,jmlHari,dpjp,tarifDpjp,drOperator,drAnestesi,akomodasiAdmin,akomodasiRuang,
                                                     prosedurbedah,endoscopy,bronkoscopy,hd,cvc,ivp,paru,nonbedahlain,gizi,farklin,
                                                     fisio,tindakan,askep,kerohanian,ecg,holter,echocardio,usg,rontgen,ctscan,
                                                     mri,labpa,labpk,darah,rehab,icu,picu,nicu,hcu,obat,
                                                     alkes,oksigen,kassa,jenazah,ventilator,nebulizer,syringe,bedsetmonitor,total,tarifinacbg) 
                                            VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                    '" & Format(CDate(Eklaim.txtTglKlrRawat.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "',
                                                    @unit,@hakKelas,@kelas,@jmlHari,'" & txtDpjp.Text & "',
                                                    '" & Convert.ToDouble(txtTotDpjp.Text, ci) & "','" & txtDrOperator.Text & "','" & txtDrAnestesi.Text & "',
                                                    '" & Convert.ToDouble(txtTotAdmPoli.Text, ci) & "',@akomodasiRuang,'" & Convert.ToDouble(Val(txtTotOpe.Text + txtTotRR.Text), ci) & "',
                                                    '" & Convert.ToDouble(txtTotEndos.Text, ci) & "','" & Convert.ToDouble(txtTotBronc.Text, ci) & "','" & Convert.ToDouble(txtTotHemo.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotCvc.Text, ci) & "','" & Convert.ToDouble(txtTotParu.Text, ci) & "','" & Convert.ToDouble(txtTotLain.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotCathLab.Text, ci) & "','" & Convert.ToDouble(txtTotGizi.Text, ci) & "','" & Convert.ToDouble(txtTotFarklin.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotFisio.Text, ci) & "','" & Convert.ToDouble(txtTotTindakan.Text, ci) & "','" & Convert.ToDouble(txtTotAskep.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotRohani.Text, ci) & "','" & Convert.ToDouble(txtTotEcg.Text, ci) & "','" & Convert.ToDouble(txtTotHolter.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotEcho.Text, ci) & "','" & Convert.ToDouble(txtTotUsg.Text, ci) & "','" & Convert.ToDouble(txtTotRontgen.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotCtscan.Text, ci) & "','" & Convert.ToDouble(txtTotMri.Text, ci) & "','" & Convert.ToDouble(txtTotLabPA.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotLabPK.Text, ci) & "','" & Convert.ToDouble(txtTotDarah.Text, ci) & "','" & Convert.ToDouble(txtTotRehab.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotIcu.Text, ci) & "','" & Convert.ToDouble(txtTotPicu.Text, ci) & "','" & Convert.ToDouble(txtTotNicu.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotHcu.Text, ci) & "','" & Convert.ToDouble(txtTotObat.Text, ci) & "','" & Convert.ToDouble(txtTotAlkes.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotOxy.Text, ci) & "','" & Convert.ToDouble(txtTotKassa.Text, ci) & "','" & Convert.ToDouble(txtTotJenazah.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotVenti.Text, ci) & "','" & Convert.ToDouble(txtTotNebul.Text, ci) & "','" & Convert.ToDouble(txtTotSyr.Text, ci) & "',
                                                    '" & Convert.ToDouble(txtTotMonitor.Text, ci) & "','" & Convert.ToDouble(txtTotalRincian.Text, ci) & "','" & Convert.ToDouble(txtInacbg.Text, ci) & "')"
        Else
            strJpR = "INSERT INTO t_eklaimjpranap(noRM,NoSep,tglMasuk,tglKeluar,namaPasien,
                                                 unit,hakKelas,kelas,jmlHari,dpjp,tarifDpjp,drOperator,drAnestesi,akomodasiAdmin,akomodasiRuang,
                                                 prosedurbedah,endoscopy,bronkoscopy,hd,cvc,ivp,paru,nonbedahlain,gizi,farklin,
                                                 fisio,tindakan,askep,kerohanian,ecg,holter,echocardio,usg,rontgen,ctscan,
                                                 mri,labpa,labpk,darah,rehab,icu,picu,nicu,hcu,obat,
                                                 alkes,oksigen,kassa,jenazah,ventilator,nebulizer,syringe,bedsetmonitor,total,tarifinacbg) 
                                        VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                '" & Format(CDate(Eklaim.txtTglKlrRawat.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "',
                                                @unit,@hakKelas,@kelas,@jmlHari,'" & txtDpjp.Text & "',
                                                '" & Convert.ToDouble(txtTotDpjp.Text, ci) & "','" & txtDrOperator.Text & "','" & txtDrAnestesi.Text & "',
                                                '" & Convert.ToDouble(txtTotAdmPoli.Text, ci) & "',@akomodasiRuang,'" & Convert.ToDouble(Val(txtTotOpe.Text + txtTotRR.Text), ci) & "',
                                                '" & Convert.ToDouble(txtTotEndos.Text, ci) & "','" & Convert.ToDouble(txtTotBronc.Text, ci) & "','" & Convert.ToDouble(txtTotHemo.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotCvc.Text, ci) & "','" & Convert.ToDouble(txtTotParu.Text, ci) & "','" & Convert.ToDouble(txtTotLain.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotCathLab.Text, ci) & "','" & Convert.ToDouble(txtTotGizi.Text, ci) & "','" & Convert.ToDouble(txtTotFarklin.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotFisio.Text, ci) & "','" & Convert.ToDouble(txtTotTindakan.Text, ci) & "','" & Convert.ToDouble(txtTotAskep.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotRohani.Text, ci) & "','" & Convert.ToDouble(txtTotEcg.Text, ci) & "','" & Convert.ToDouble(txtTotHolter.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotEcho.Text, ci) & "','" & Convert.ToDouble(txtTotUsg.Text, ci) & "','" & Convert.ToDouble(txtTotRontgen.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotCtscan.Text, ci) & "','" & Convert.ToDouble(txtTotMri.Text, ci) & "','" & Convert.ToDouble(txtTotLabPA.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotLabPK.Text, ci) & "','" & Convert.ToDouble(txtTotDarah.Text, ci) & "','" & Convert.ToDouble(txtTotRehab.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotIcu.Text, ci) & "','" & Convert.ToDouble(txtTotPicu.Text, ci) & "','" & Convert.ToDouble(txtTotNicu.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotHcu.Text, ci) & "','" & Convert.ToDouble(txtTotObat.Text, ci) & "','" & Convert.ToDouble(txtTotAlkes.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotOxy.Text, ci) & "','" & Convert.ToDouble(txtTotKassa.Text, ci) & "','" & Convert.ToDouble(txtTotJenazah.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotVenti.Text, ci) & "','" & Convert.ToDouble(txtTotNebul.Text, ci) & "','" & Convert.ToDouble(txtTotSyr.Text, ci) & "',
                                                '" & Convert.ToDouble(txtTotMonitor.Text, ci) & "','" & Convert.ToDouble(txtTotalRincian.Text, ci) & "','" & Convert.ToDouble(txtInacbg.Text, ci) & "')"
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

            ElseIf txtRuang.Text.Equals("CVCU", StringComparison.OrdinalIgnoreCase) Then
                For i As Integer = 0 To dgvAkomodasi.Rows.Count - 1
                    cmdJpR.Parameters.AddWithValue("@unit", dgvAkomodasi.Rows(i).Cells(0).Value)
                    cmdJpR.Parameters.AddWithValue("@hakKelas", dgvAkomodasi.Rows(i).Cells(1).Value)
                    cmdJpR.Parameters.AddWithValue("@kelas", dgvAkomodasi.Rows(i).Cells(1).Value)
                    cmdJpR.Parameters.AddWithValue("@jmlHari", dgvAkomodasi.Rows(i).Cells(3).Value)
                    cmdJpR.Parameters.AddWithValue("@akomodasiRuang", Convert.ToDouble(dgvAkomodasi.Rows(i).Cells(4).Value, ci))
                    cmdJpR.ExecuteNonQuery()
                    cmdJpR.Parameters.Clear()
                Next
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

        If Eklaim.txtJaminan.Text.Equals("Pasien Bayar", StringComparison.OrdinalIgnoreCase) Or Eklaim.txtJaminan.Text.Equals("Umum", StringComparison.OrdinalIgnoreCase) Then
            strJpVI = "INSERT INTO t_eklaimjpdokterrajalumum(noRM,NoSep,tglMasuk,
                                                             tglKeluar,
                                                             namaPasien,unit,kelas,
                                                             jmlVisite,drVisite,jasaVisite)
                                                     VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                             '" & Format(CDate(Eklaim.txtTglKlrRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                             '" & Eklaim.txtNamaPasien.Text & "','" & Eklaim.txtUnit.Text & "','" & Eklaim.txtKelas.Text & "',
                                                             '1','" & txtDrIgd.Text & "','" & Convert.ToDouble(txtTotDrIgd.Text, ci) & "')"
        Else
            strJpVI = "INSERT INTO t_eklaimjpdokterrajal(noRM,NoSep,tglMasuk,
                                                         tglKeluar,
                                                         namaPasien,unit,kelas,
                                                         jmlVisite,drVisite,jasaVisite)
                                                 VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                         '" & Format(CDate(Eklaim.txtTglKlrRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
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

        If (Eklaim.txtJaminan.Text.Equals("Pasien Bayar", StringComparison.OrdinalIgnoreCase) Or Eklaim.txtJaminan.Text.Equals("Umum", StringComparison.OrdinalIgnoreCase)) And (txtrajal = True Or txtigd = True) Then
            strJpI = "INSERT INTO t_eklaimjpdokterrajalumum(noRM,NoSep,tglMasuk,tglKeluar,
                                                           namaPasien,unit,kelas,jmlVisite,
                                                           drKonsul,jasaVisite)
                                                   VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                           '" & Format(CDate(Eklaim.txtTglKeluar.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "','" & Eklaim.txtUnit.Text & "','" & Eklaim.txtKelas.Text & "',
                                                           @jmlVisite,@drKonsul,@jasaVisite)"
            'MsgBox("Umum Rajal/Igd")
        ElseIf (Eklaim.txtJaminan.Text.Equals("Pasien Bayar", StringComparison.OrdinalIgnoreCase) Or Eklaim.txtJaminan.Text.Equals("Umum", StringComparison.OrdinalIgnoreCase)) And txtranap = True Then
            strJpI = "INSERT INTO t_eklaimjpdokterranapumum(noRM,NoSep,tglMasuk,tglKeluar,
                                                           namaPasien,unit,kelas,jmlVisite,
                                                           drKonsul,jasaVisite)
                                                   VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                           '" & Format(CDate(Eklaim.txtTglKeluar.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "','" & Eklaim.txtUnit.Text & "','" & Eklaim.txtKelas.Text & "',
                                                           @jmlVisite,@drKonsul,@jasaVisite)"
            'MsgBox("Umum Ranap")
        ElseIf txtrajal = True Or txtigd = True Then
            strJpI = "INSERT INTO t_eklaimjpdokterrajal(noRM,NoSep,tglMasuk,tglKeluar,
                                                       namaPasien,unit,kelas,jmlVisite,
                                                       drKonsul,jasaVisite)
                                               VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                       '" & Format(CDate(Eklaim.txtTglKeluar.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "','" & Eklaim.txtUnit.Text & "','" & Eklaim.txtKelas.Text & "',
                                                       @jmlVisite,@drKonsul,@jasaVisite)"
            'MsgBox("JKN Rajal/Igd")
        ElseIf txtranap = True Then
            strJpI = "INSERT INTO t_eklaimjpdokterranap(noRM,NoSep,tglMasuk,tglKeluar,
                                                       namaPasien,unit,kelas,jmlVisite,
                                                       drKonsul,jasaVisite)
                                               VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                       '" & Format(CDate(Eklaim.txtTglKeluar.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "','" & Eklaim.txtUnit.Text & "','" & Eklaim.txtKelas.Text & "',
                                                       @jmlVisite,@drKonsul,@jasaVisite)"
            'MsgBox("JKN Ranap")
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

        If Eklaim.txtJaminan.Text.Equals("Pasien Bayar", StringComparison.OrdinalIgnoreCase) Or Eklaim.txtJaminan.Text.Equals("Umum", StringComparison.OrdinalIgnoreCase) Then
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

        If Eklaim.txtJaminan.Text.Equals("Pasien Bayar", StringComparison.OrdinalIgnoreCase) Or Eklaim.txtJaminan.Text.Equals("Umum", StringComparison.OrdinalIgnoreCase) Then
            strJpK = "INSERT INTO t_eklaimjpdokterranapumum(noRM,NoSep,tglMasuk,tglKeluar,namaPasien,unit,
                                                            kelas,jmlVisite,drKonsul,jasaVisite)
                                                    VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                            '" & Format(CDate(Eklaim.txtTglKeluar.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "','" & Eklaim.txtUnit.Text & "','" & Eklaim.txtKelas.Text & "',
                                                            @jmlKonsul,@drKonsul,@jasaKonsul)"
        Else
            strJpK = "INSERT INTO t_eklaimjpdokterranap(noRM,NoSep,tglMasuk,tglKeluar,namaPasien,unit,
                                                        kelas,jmlVisite,drKonsul,jasaVisite)
                                                 VALUES ('" & Eklaim.txtNoRM.Text & "','" & Eklaim.txtSetSep.Text & "','" & Format(CDate(Eklaim.txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                                                        '" & Format(CDate(Eklaim.txtTglKeluar.Text), "yyyy-MM-dd HH:mm:ss") & "','" & Eklaim.txtNamaPasien.Text & "','" & Eklaim.txtUnit.Text & "','" & Eklaim.txtKelas.Text & "',
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

        ProgressBar1.Minimum = 0
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            ProgressBar1.Maximum = 14
        ElseIf unit.Contains("Rawat Inap") Then
            ProgressBar1.Maximum = 19
        End If

        ProgressBar1.Value = 0
        ProgressBar1.Visible = True

        txtUser.Text = Home.txtUser.Text
        Label3.Text = "Pasien a.n. " & Eklaim.txtNamaPasien.Text
        btnEklaim.BackColor = Color.FromArgb(209, 232, 223)
        PicCollapse.Visible = False
        Call tampilRuang()
        Call autoDokter()

        With Me.txtDrIgd
            .AutoCompleteCustomSource = Me.mySource
            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
            .AutoCompleteSource = AutoCompleteSource.CustomSource
            .Visible = True
        End With

        With Me.txtDrAnestesi
            .AutoCompleteCustomSource = Me.mySource
            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
            .AutoCompleteSource = AutoCompleteSource.CustomSource
            .Visible = True
        End With

        With Me.txtDrOperator
            .AutoCompleteCustomSource = Me.mySource
            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
            .AutoCompleteSource = AutoCompleteSource.CustomSource
            .Visible = True
        End With

        With Me.txtDrParu
            .AutoCompleteCustomSource = Me.mySource
            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
            .AutoCompleteSource = AutoCompleteSource.CustomSource
            .Visible = True
        End With

        With Me.txtDpjp
            .AutoCompleteCustomSource = Me.mySource
            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
            .AutoCompleteSource = AutoCompleteSource.CustomSource
            .Visible = True
        End With
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
        Me.Close()
    End Sub

    Private Sub btnEklaim_Click(sender As Object, e As EventArgs) Handles btnEklaim.Click
        Dim btn As Button = CType(sender, Button)
        setColor(btn)
        Form1.Show()
        Me.Close()
    End Sub

    Private Sub btnBuku_Click(sender As Object, e As EventArgs) Handles btnBuku.Click
        Pembukuan.Show()
        Me.Close()
    End Sub

    Private Sub btnPiutang_Click(sender As Object, e As EventArgs) Handles btnPiutang.Click
        RekapPiutang.Show()
        Me.Close()
    End Sub

    Private Sub btnUmum_Click(sender As Object, e As EventArgs) Handles btnUmum.Click
        RekapPiutangUmum.Show()
        Me.Close()
    End Sub

    Private Sub btnTotal_Click(sender As Object, e As EventArgs) Handles btnTotal.Click
        TotalRekap.Show()
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
        ruang = txtRuang.Text
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

    Dim thRajal, thKonsulRj As Thread
    Dim thRanap, thAkoRi, thVisitRi, thKonsulRi,
        thOpeRi, thOpeParuRi, thDpjpRi, thGiziRi,
        thFarklinRi, thFisioRi As Thread

    Private Sub txtNoRanap_TextChanged(sender As Object, e As EventArgs) Handles txtNoRanap.TextChanged
        ProgressBar1.Visible = True
        ProgressBar1.Value = 0
        btnSimpan.Enabled = False
        txtRuang.Enabled = False
        If txtNoRanap.Text <> "NoRanap" Then
            'Dll
            If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
                bgwAkomodasi.RunWorkerAsync()
                bgwBedah.RunWorkerAsync()
                bgwKonsul.RunWorkerAsync()
                bgwNonBedah.RunWorkerAsync()
                bgwTenagaAhli.RunWorkerAsync()
                bgwFarmasi.RunWorkerAsync()
                bgwAlkes.RunWorkerAsync()
                bgwBmhp.RunWorkerAsync()
                bgwAlatMedis.RunWorkerAsync()
                bgwRadiologi.RunWorkerAsync()
                bgwLaborat.RunWorkerAsync()
                bgwPenunjang.RunWorkerAsync()
                bgwRehab.RunWorkerAsync()
                bgwTindakan.RunWorkerAsync()
                CheckForIllegalCrossThreadCalls = False
            ElseIf unit.Contains("Rawat Inap") Then
                bgwAkomodasi.RunWorkerAsync()
                bgwVisite.RunWorkerAsync()
                bgwKonsul.RunWorkerAsync()
                bgwBedah.RunWorkerAsync()
                bgwNonBedah.RunWorkerAsync()
                bgwTenagaAhli.RunWorkerAsync()
                bgwGizi.RunWorkerAsync()
                bgwFarklin.RunWorkerAsync()
                bgwFisio.RunWorkerAsync()
                bgwFarmasi.RunWorkerAsync()
                bgwAlkes.RunWorkerAsync()
                bgwBmhp.RunWorkerAsync()
                bgwAlatMedis.RunWorkerAsync()
                bgwRadiologi.RunWorkerAsync()
                bgwLaborat.RunWorkerAsync()
                bgwPenunjang.RunWorkerAsync()
                bgwRehab.RunWorkerAsync()
                bgwTindakan.RunWorkerAsync()
                bgwAskep.RunWorkerAsync()
                CheckForIllegalCrossThreadCalls = False
                'Call detailJpRekapRanap()
                ''Akomodasi
                'Call detailJpAkomodasi()
                'Call totalTarifAkomodasi()
                ''IGD
                'Call totalTarifIgdKonsul()
                ''Visite
                'Call detailJpVisite()
                'Call totalTarifVisite()
                ''Konsul
                'Call detailJpKonsul()
                'Call totalTarifKonsul()
                ''Operator
                'Call detailJpOperator()
                'Call detailJpOpeParu()
                ''DPJP
                'Call detailJpDPJP()
                ''Tenaga Ahli
                'Call detailJpGizi()
                'Call totalTarifGizi()
                'Call detailJpFarklin()
                'Call totalTarifFarklin()
                'Call detailJpFisio()
                'Call totalTarifFisio()
            End If

            txtTotalRincian.Text = (Val(CInt(txtTotAdmPoli.Text)) + Val(CInt(txtTotAkomodasi.Text)) + Val(CInt(txtTotIcu.Text)) + Val(CInt(txtTotHcu.Text)) +
                                    Val(CInt(txtTotPicu.Text)) + Val(CInt(txtTotNicu.Text)) + Val(CInt(txtTotDrIgd.Text)) + Val(CInt(txtTotKonsulPoli.Text)) +
                                    Val(CInt(txtTotVisite.Text)) + Val(CInt(txtTotKonsulRanap.Text)) + Val(CInt(txtTotOpe.Text)) + Val(CInt(txtTotRR.Text)) +
                                    Val(CInt(txtTotEndos.Text)) + Val(CInt(txtTotBronc.Text)) + Val(CInt(txtTotCathLab.Text)) + Val(CInt(txtTotHemo.Text)) +
                                    Val(CInt(txtTotCvc.Text)) + Val(CInt(txtTotLain.Text)) + Val(CInt(txtTotParu.Text)) + Val(CInt(txtTotGizi.Text)) +
                                    Val(CInt(txtTotFarklin.Text)) + Val(CInt(txtTotFisio.Text)) + Val(CInt(txtTotObat.Text)) + Val(CInt(txtTotAlkes.Text)) +
                                    Val(CInt(txtTotOxy.Text)) + Val(CInt(txtTotKassa.Text)) + Val(CInt(txtTotVenti.Text)) + Val(CInt(txtTotNebul.Text)) +
                                    Val(CInt(txtTotSyr.Text)) + Val(CInt(txtTotMonitor.Text)) + Val(CInt(txtTotRontgen.Text)) + Val(CInt(txtTotUsg.Text)) +
                                    Val(CInt(txtTotCtscan.Text)) + Val(CInt(txtTotMri.Text)) + Val(CInt(txtTotLabPK.Text)) + Val(CInt(txtTotLabPA.Text)) +
                                    Val(CInt(txtTotDarah.Text)) + Val(CInt(txtTotEcg.Text)) + Val(CInt(txtTotEcho.Text)) + Val(CInt(txtTotEcho.Text)) +
                                    Val(CInt(txtTotRehab.Text)) + Val(CInt(txtTotTindakan.Text)) + Val(CInt(txtTotAskep.Text)) + Val(CInt(txtTotRohani.Text)) +
                                    Val(CInt(txtTotDpjp.Text)) + Val(CInt(txtTotJenazah.Text)) + Val(CInt(txtTotTreadmill.Text)) + Val(CInt(txtInacbg.Text))).ToString("#,##0")
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

    Private Sub bgwAkomodasi_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwAkomodasi.DoWork
        bd_Akomodasi()
        Call totalTarifAkomodasi()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub
    Private Sub bgwBedah_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwBedah.DoWork
        detailJpOperator()
        bd_Bedah()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub bgwNonBedah_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwNonBedah.DoWork
        detailJpOpeParu()
        bd_NonBedah()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub bgwVisite_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwVisite.DoWork
        Call detailJpVisite()
        Call totalTarifVisite()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub bgwKonsul_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwKonsul.DoWork
        Call detailJpKonsul()
        Call totalTarifKonsul()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub bgwTenagaAhli_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwTenagaAhli.DoWork
        bd_TenagaAhli()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub bgwGizi_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwGizi.DoWork
        Call detailJpGizi()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub bgwFarklin_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwFarklin.DoWork
        Call detailJpFarklin()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub bgwFisio_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwFisio.DoWork
        Call detailJpFisio()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub bgwFarmasi_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwFarmasi.DoWork
        bd_Farmasi()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub bgwAlkes_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwAlkes.DoWork
        bd_Alkes()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub bgwBmhp_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwBmhp.DoWork
        bd_BMHP()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub bgwAlatMedis_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwAlatMedis.DoWork
        bd_Almed()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub bgwRadiologi_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwRadiologi.DoWork
        bd_Radiologi()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub bgwLaborat_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwLaborat.DoWork
        bd_Laborat()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub bgwPenunjang_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwPenunjang.DoWork
        bd_Penunjang()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub bgwRehab_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwRehab.DoWork
        bd_Rehab()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub bgwTindakan_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwTindakan.DoWork
        bd_Tindakan()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub

    Private Sub bgwAskep_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwAskep.DoWork
        bd_Askep()
        ProgressBar1.Value = ProgressBar1.Value + 1
        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            If ProgressBar1.Value = 14 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        ElseIf unit.Contains("Rawat Inap") Then
            If ProgressBar1.Value = 19 Then
                ProgressBar1.Visible = False
                btnSimpan.Enabled = True
                txtRuang.Enabled = True
            End If
        End If

        System.Threading.Thread.Sleep(1000)
    End Sub

#Region "BD Akomodasi"
    Sub bd_Akomodasi()
        Dim cnBD As New MySqlConnection(KoneksiString)
        Dim queryBD As String = ""
        Dim cmdBD As MySqlCommand
        Dim drBD As MySqlDataReader

        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            queryBD = "Call bdRj_Akomodasi('" & noRegister & "')"
        ElseIf unit.Contains("Rawat Inap") Then
            queryBD = "Call bdRi_Akomodasi('" & txtNoRanap.Text & "','" & noRM & "','" & tglDaftar & "')"
        End If

        Try
            cnBD.Open()
            cmdBD = New MySqlCommand(queryBD, cnBD)
            drBD = cmdBD.ExecuteReader

            If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
                drBD.Read()
                If drBD.HasRows Then
                    txtAdminPoli.Text = drBD.Item("unit")
                    txtDrIgd.Text = drBD.Item("dokter")
                    txtTotAdmPoli.Text = CInt(drBD.Item("admin")).ToString("#,##0")
                    txtTotDrIgd.Text = CInt(drBD.Item("visite")).ToString("#,##0")
                End If
            ElseIf unit.Contains("Rawat Inap") Then
                dgvAkomodasi.Rows.Clear()
                Do While drBD.Read
                    dgvAkomodasi.Invoke(New Action(Function() dgvAkomodasi.Rows.Add(drBD.Item("rawatInap"), drBD.Item("kls"), drBD.Item("tarif"), drBD.Item("jmlInap"), drBD.Item("biayaInap"))))
                Loop

                txtTotIcu.Text = CInt(drBD.Item("icu")).ToString("#,##0")
                txtTotHcu.Text = CInt(drBD.Item("hcu")).ToString("#,##0")
                txtTotNicu.Text = CInt(drBD.Item("nicu")).ToString("#,##0")
                txtTotPicu.Text = CInt(drBD.Item("picu")).ToString("#,##0")
            End If

            drBD.Close()
            cnBD.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Breakdown Akomodasi", MessageBoxButtons.OK)
            cnBD.Close()
        End Try
    End Sub
#End Region
#Region "BD Bedah"
    Sub bd_Bedah()
        Dim cnBD As New MySqlConnection(KoneksiString)
        Dim queryBD As String = ""
        Dim cmdBD As MySqlCommand
        Dim drBD As MySqlDataReader

        queryBD = "Call bdALL_Bedah('" & noRegister & "')"

        Try
            cnBD.Open()
            cmdBD = New MySqlCommand(queryBD, cnBD)
            drBD = cmdBD.ExecuteReader
            drBD.Read()
            If drBD.HasRows Then
                txtTotOpe.Text = CInt(drBD.Item("ope")).ToString("#,##0")
                txtTotRR.Text = CInt(drBD.Item("rr")).ToString("#,##0")
            End If

            drBD.Close()
            cnBD.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Breakdown Bedah", MessageBoxButtons.OK)
            cnBD.Close()
        End Try
    End Sub
#End Region
#Region "BD Non Bedah"
    Sub bd_NonBedah()
        Dim cnBD As New MySqlConnection(KoneksiString)
        Dim queryBD As String = ""
        Dim cmdBD As MySqlCommand
        Dim drBD As MySqlDataReader

        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            queryBD = "Call bdRj_NonBedah('" & noRegister & "')"
        ElseIf unit.Contains("Rawat Inap") Then
            queryBD = "Call bdRi_NonBedah('" & noRanap & "','" & noRegister & "','" & ruang & "')"
        End If

        Try
            cnBD.Open()
            cmdBD = New MySqlCommand(queryBD, cnBD)
            drBD = cmdBD.ExecuteReader
            drBD.Read()
            If drBD.HasRows Then
                If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
                    txtTotEndos.Text = CInt(drBD.Item("endos")).ToString("#,##0")
                    txtTotBronc.Text = CInt(drBD.Item("bronc")).ToString("#,##0")
                    txtTotCathLab.Text = CInt(drBD.Item("cathe")).ToString("#,##0")
                    txtTotHemo.Text = CInt(drBD.Item("hemo")).ToString("#,##0")
                    txtTotCvc.Text = CInt(drBD.Item("cvc")).ToString("#,##0")
                    txtTotLain.Text = CInt(drBD.Item("ivp")).ToString("#,##0")
                    txtTotParu.Text = CInt(drBD.Item("paru")).ToString("#,##0")
                ElseIf unit.Contains("Rawat Inap") Then
                    txtTotEndos.Text = CInt(drBD.Item("endos")).ToString("#,##0")
                    txtTotBronc.Text = CInt(drBD.Item("bronc")).ToString("#,##0")
                    txtTotCathLab.Text = CInt(drBD.Item("cathe")).ToString("#,##0")
                    txtTotHemo.Text = CInt(drBD.Item("hemo")).ToString("#,##0")
                    txtTotCvc.Text = CInt(drBD.Item("cvc")).ToString("#,##0")
                    txtTotLain.Text = CInt(drBD.Item("ivp")).ToString("#,##0")
                    txtTotParu.Text = CInt(drBD.Item("paru")).ToString("#,##0")
                End If
            End If

            drBD.Close()
            cnBD.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Breakdown Non Bedah", MessageBoxButtons.OK)
            cnBD.Close()
        End Try
    End Sub
#End Region
#Region "BD Tenaga Ahli"
    Sub bd_TenagaAhli()
        Dim cnBD As New MySqlConnection(KoneksiString)
        Dim queryBD As String = ""
        Dim cmdBD As MySqlCommand
        Dim drBD As MySqlDataReader

        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            queryBD = "Call bdRj_TenagaAhli('" & noRegister & "')"
        ElseIf unit.Contains("Rawat Inap") Then
            queryBD = "Call bdRi_TenagaAhli('" & txtNoRanap.Text & "')"
        End If

        Try
            cnBD.Open()
            cmdBD = New MySqlCommand(queryBD, cnBD)
            drBD = cmdBD.ExecuteReader
            drBD.Read()
            If drBD.HasRows Then
                If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
                    txtTotGizi.Text = CInt(drBD.Item("gizi")).ToString("#,##0")
                    txtTotFisio.Text = CInt(drBD.Item("fisio")).ToString("#,##0")
                ElseIf unit.Contains("Rawat Inap") Then
                    txtTotGizi.Text = CInt(drBD.Item("gizi")).ToString("#,##0")
                    txtTotFisio.Text = CInt(drBD.Item("fisio")).ToString("#,##0")
                    txtTotFarklin.Text = CInt(drBD.Item("farklin")).ToString("#,##0")
                End If
            End If

            drBD.Close()
            cnBD.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Breakdown Tenaga Ahli", MessageBoxButtons.OK)
            cnBD.Close()
        End Try
    End Sub
#End Region
#Region "BD Farmasi"
    Sub bd_Farmasi()
        Dim cnBD As New MySqlConnection(KoneksiString)
        Dim queryBD As String = ""
        Dim cmdBD As MySqlCommand
        Dim drBD As MySqlDataReader

        If unit.Contains("Rawat Jalan") Then
            queryBD = "Call bdRj_Farmasi('" & noRegister & "')"
        ElseIf unit.Contains("Igd") Then
            queryBD = "Call bdRj_FarmasiIgd('" & noRegister & "')"
        ElseIf unit.Contains("Rawat Inap") Then
            queryBD = "Call bdRi_Farmasi('" & noRegister & "','" & ruang & "')"
        End If

        Try
            cnBD.Open()
            cmdBD = New MySqlCommand(queryBD, cnBD)
            drBD = cmdBD.ExecuteReader
            drBD.Read()
            If drBD.HasRows Then
                If unit.Contains("Rawat Jalan") Then
                    txtTotObat.Text = CInt(drBD.Item("obat")).ToString("#,##0")
                ElseIf unit.Contains("Igd") Then
                    txtTotObat.Text = CInt(drBD.Item("obat")).ToString("#,##0")
                ElseIf unit.Contains("Rawat Inap") Then
                    txtTotObat.Text = CInt(drBD.Item("obat")).ToString("#,##0")
                End If
            End If

            drBD.Close()
            cnBD.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Breakdown Obat", MessageBoxButtons.OK)
            cnBD.Close()
        End Try
    End Sub
#End Region
#Region "BD Alkes"
    Sub bd_Alkes()
        Dim cnBD As New MySqlConnection(KoneksiString)
        Dim queryBD As String = ""
        Dim cmdBD As MySqlCommand
        Dim drBD As MySqlDataReader

        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            queryBD = "Call bdRj_Alkes('" & noRegister & "')"
        ElseIf unit.Contains("Rawat Inap") Then
            queryBD = "Call bdRi_Alkes('" & noRegister & "','" & ruang & "')"
        End If

        Try
            cnBD.Open()
            cmdBD = New MySqlCommand(queryBD, cnBD)
            drBD = cmdBD.ExecuteReader
            drBD.Read()
            If drBD.HasRows Then
                If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
                    txtTotAlkes.Text = CInt(drBD.Item("alkes")).ToString("#,##0")
                ElseIf unit.Contains("Rawat Inap") Then
                    txtTotAlkes.Text = CInt(drBD.Item("alkes")).ToString("#,##0")
                End If
            End If

            drBD.Close()
            cnBD.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Breakdown Alkes", MessageBoxButtons.OK)
            cnBD.Close()
        End Try
    End Sub
#End Region
#Region "BD BMHP"
    Sub bd_BMHP()
        Dim cnBD As New MySqlConnection(KoneksiString)
        Dim queryBD As String = ""
        Dim cmdBD As MySqlCommand
        Dim drBD As MySqlDataReader

        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            queryBD = "Call bdRj_Bmhp('" & noRegister & "')"
        ElseIf unit.Contains("Rawat Inap") Then
            queryBD = "Call bdRi_Bmhp('" & txtNoRanap.Text & "')"
        End If

        Try
            cnBD.Open()
            cmdBD = New MySqlCommand(queryBD, cnBD)
            drBD = cmdBD.ExecuteReader
            drBD.Read()
            If drBD.HasRows Then
                If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
                    txtTotOxy.Text = CInt(drBD.Item("oksigen")).ToString("#,##0")
                    txtTotKassa.Text = CInt(drBD.Item("kassa")).ToString("#,##0")
                ElseIf unit.Contains("Rawat Inap") Then
                    txtTotOxy.Text = CInt(drBD.Item("oksigen")).ToString("#,##0")
                    txtTotKassa.Text = CInt(drBD.Item("kassa")).ToString("#,##0")
                End If
            End If

            drBD.Close()
            cnBD.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Breakdown BMHP", MessageBoxButtons.OK)
            cnBD.Close()
        End Try
    End Sub
#End Region
#Region "BD Alat Medis"
    Sub bd_Almed()
        Dim cnBD As New MySqlConnection(KoneksiString)
        Dim queryBD As String = ""
        Dim cmdBD As MySqlCommand
        Dim drBD As MySqlDataReader

        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            queryBD = "Call bdRj_Almed('" & noRegister & "')"
        ElseIf unit.Contains("Rawat Inap") Then
            queryBD = "Call bdRi_Almed('" & txtNoRanap.Text & "')"
        End If

        Try
            cnBD.Open()
            cmdBD = New MySqlCommand(queryBD, cnBD)
            drBD = cmdBD.ExecuteReader
            drBD.Read()
            If drBD.HasRows Then
                If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
                    txtTotVenti.Text = CInt(drBD.Item("venti")).ToString("#,##0")
                    txtTotNebul.Text = CInt(drBD.Item("nebul")).ToString("#,##0")
                    txtTotSyr.Text = CInt(drBD.Item("syringe")).ToString("#,##0")
                ElseIf unit.Contains("Rawat Inap") Then
                    txtTotVenti.Text = CInt(drBD.Item("venti")).ToString("#,##0")
                    txtTotNebul.Text = CInt(drBD.Item("nebul")).ToString("#,##0")
                    txtTotSyr.Text = CInt(drBD.Item("syringe")).ToString("#,##0")
                    txtTotMonitor.Text = CInt(drBD.Item("monitor")).ToString("#,##0")
                End If
            End If

            drBD.Close()
            cnBD.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Breakdown Alat Medis", MessageBoxButtons.OK)
            cnBD.Close()
        End Try
    End Sub
#End Region
#Region "BD Radiologi"
    Sub bd_Radiologi()
        Dim cnBD As New MySqlConnection(KoneksiString)
        Dim queryBD As String = ""
        Dim cmdBD As MySqlCommand
        Dim drBD As MySqlDataReader

        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            queryBD = "Call bdRj_Radiologi('" & noRegister & "')"
        ElseIf unit.Contains("Rawat Inap") Then
            queryBD = "Call bdRi_Radiologi('" & noRegister & "','" & ruang & "')"
        End If

        Try
            cnBD.Open()
            cmdBD = New MySqlCommand(queryBD, cnBD)
            drBD = cmdBD.ExecuteReader
            drBD.Read()
            If drBD.HasRows Then
                If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
                    txtTotRontgen.Text = CInt(drBD.Item("rontgen")).ToString("#,##0")
                    txtTotUsg.Text = CInt(drBD.Item("USG")).ToString("#,##0")
                    txtTotCtscan.Text = CInt(drBD.Item("ctscan")).ToString("#,##0")
                    txtTotMri.Text = CInt(drBD.Item("mri")).ToString("#,##0")
                ElseIf unit.Contains("Rawat Inap") Then
                    txtTotRontgen.Text = CInt(drBD.Item("rontgen")).ToString("#,##0")
                    txtTotUsg.Text = CInt(drBD.Item("USG")).ToString("#,##0")
                    txtTotCtscan.Text = CInt(drBD.Item("ctscan")).ToString("#,##0")
                    txtTotMri.Text = CInt(drBD.Item("mri")).ToString("#,##0")
                End If
            End If

            drBD.Close()
            cnBD.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Breakdown Radiologi", MessageBoxButtons.OK)
            cnBD.Close()
        End Try
    End Sub
#End Region
#Region "BD Laborat"
    Sub bd_Laborat()
        Dim cnBD As New MySqlConnection(KoneksiString)
        Dim queryBD As String = ""
        Dim cmdBD As MySqlCommand
        Dim drBD As MySqlDataReader

        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            queryBD = "Call bdRj_Laborat('" & noRegister & "')"
        ElseIf unit.Contains("Rawat Inap") Then
            queryBD = "Call bdRi_Laborat('" & noRegister & "','" & ruang & "')"
        End If

        Try
            cnBD.Open()
            cmdBD = New MySqlCommand(queryBD, cnBD)
            drBD = cmdBD.ExecuteReader
            drBD.Read()
            If drBD.HasRows Then
                If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
                    txtTotLabPK.Text = CInt(drBD.Item("PK")).ToString("#,##0")
                    txtTotLabPA.Text = CInt(drBD.Item("PA")).ToString("#,##0")
                    txtTotDarah.Text = CInt(drBD.Item("darah")).ToString("#,##0")
                ElseIf unit.Contains("Rawat Inap") Then
                    txtTotLabPK.Text = CInt(drBD.Item("PK")).ToString("#,##0")
                    txtTotLabPA.Text = CInt(drBD.Item("PA")).ToString("#,##0")
                    txtTotDarah.Text = CInt(drBD.Item("darah")).ToString("#,##0")
                End If
            End If

            drBD.Close()
            cnBD.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Breakdown Laborat", MessageBoxButtons.OK)
            cnBD.Close()
        End Try
    End Sub
#End Region
#Region "BD Penunjang"
    Sub bd_Penunjang()
        Dim cnBD As New MySqlConnection(KoneksiString)
        Dim queryBD As String = ""
        Dim cmdBD As MySqlCommand
        Dim drBD As MySqlDataReader

        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            queryBD = "Call bdRj_Penunjang('" & noRegister & "')"
        ElseIf unit.Contains("Rawat Inap") Then
            queryBD = "Call bdRi_Penunjang('" & txtNoRanap.Text & "')"
        End If

        Try
            cnBD.Open()
            cmdBD = New MySqlCommand(queryBD, cnBD)
            drBD = cmdBD.ExecuteReader
            drBD.Read()
            If drBD.HasRows Then
                If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
                    txtTotEcg.Text = CInt(drBD.Item("ekg")).ToString("#,##0")
                    txtTotEcho.Text = CInt(drBD.Item("echo")).ToString("#,##0")
                    txtTotHolter.Text = CInt(drBD.Item("holter")).ToString("#,##0")
                    txtTotTreadmill.Text = CInt(drBD.Item("tread")).ToString("#,##0")
                ElseIf unit.Contains("Rawat Inap") Then
                    txtTotEcg.Text = CInt(drBD.Item("ekg")).ToString("#,##0")
                    txtTotEcho.Text = CInt(drBD.Item("echo")).ToString("#,##0")
                    txtTotHolter.Text = CInt(drBD.Item("holter")).ToString("#,##0")
                    txtTotTreadmill.Text = CInt(drBD.Item("tread")).ToString("#,##0")
                End If
            End If

            drBD.Close()
            cnBD.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Breakdown Penunjang", MessageBoxButtons.OK)
            cnBD.Close()
        End Try
    End Sub
#End Region
#Region "BD Rehab"
    Sub bd_Rehab()
        Dim cnBD As New MySqlConnection(KoneksiString)
        Dim queryBD As String = ""
        Dim cmdBD As MySqlCommand
        Dim drBD As MySqlDataReader

        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            queryBD = "Call bdRj_Rehab('" & noRegister & "')"
        ElseIf unit.Contains("Rawat Inap") Then
            queryBD = "Call bdRi_Rehab('" & txtNoRanap.Text & "')"
        End If

        Try
            cnBD.Open()
            cmdBD = New MySqlCommand(queryBD, cnBD)
            drBD = cmdBD.ExecuteReader
            drBD.Read()
            If drBD.HasRows Then
                If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
                    txtTotRehab.Text = CInt(drBD.Item("rehab")).ToString("#,##0")
                ElseIf unit.Contains("Rawat Inap") Then
                    txtTotRehab.Text = CInt(drBD.Item("rehab")).ToString("#,##0")
                End If
            End If

            drBD.Close()
            cnBD.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Breakdown Rehab", MessageBoxButtons.OK)
            cnBD.Close()
        End Try
    End Sub
#End Region
#Region "BD Keperawatan"
    Sub bd_Tindakan()
        Dim cnBD As New MySqlConnection(KoneksiString)
        Dim queryBD As String = ""
        Dim cmdBD As MySqlCommand
        Dim drBD As MySqlDataReader

        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            queryBD = "Call bdRj_Tindakan('" & noRegister & "')"
        ElseIf unit.Contains("Rawat Inap") Then
            queryBD = "Call bdRi_Tindakan('" & txtNoRanap.Text & "')"
        End If

        Try
            cnBD.Open()
            cmdBD = New MySqlCommand(queryBD, cnBD)
            drBD = cmdBD.ExecuteReader
            drBD.Read()
            If drBD.HasRows Then
                If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
                    txtTotTindakan.Text = CInt(drBD.Item("tindakan")).ToString("#,##0")
                ElseIf unit.Contains("Rawat Inap") Then
                    txtTotTindakan.Text = CInt(drBD.Item("tindakan")).ToString("#,##0")
                End If
            End If

            drBD.Close()
            cnBD.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Breakdown Tindakan", MessageBoxButtons.OK)
            cnBD.Close()
        End Try
    End Sub
#End Region
#Region "BD Askep"
    Sub bd_Askep()
        Dim cnBD As New MySqlConnection(KoneksiString)
        Dim queryBD As String = ""
        Dim cmdBD As MySqlCommand
        Dim drBD As MySqlDataReader

        queryBD = "Call bdRi_Askep('" & txtNoRanap.Text & "','" & kelas & "','" & noRegister & "','" & ruang & "')"

        Try
            cnBD.Open()
            cmdBD = New MySqlCommand(queryBD, cnBD)
            drBD = cmdBD.ExecuteReader
            drBD.Read()
            If drBD.HasRows Then
                txtTotAskep.Text = CInt(drBD.Item("askep")).ToString("#,##0")
                txtTotRohani.Text = CInt(drBD.Item("rohani")).ToString("#,##0")
                txtDpjp.Text = drBD.Item("DPJP").ToString
                txtTotDpjp.Text = CInt(drBD.Item("total")).ToString("#,##0")
            End If

            drBD.Close()
            cnBD.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Breakdown Askep", MessageBoxButtons.OK)
            cnBD.Close()
        End Try
    End Sub
#End Region

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
        'Call koneksiServer()
        Dim cnVisit As New MySqlConnection(KoneksiString)
        Dim queryVi As String = ""
        Dim cmdVi As MySqlCommand
        Dim drVi As MySqlDataReader

        queryVi = "CALL bdRi_Visite('" & txtNoRanap.Text & "')"

        Try
            cnVisit.Open()
            cmdVi = New MySqlCommand(queryVi, cnVisit)
            drVi = cmdVi.ExecuteReader
            dgvDrVisite.Rows.Clear()
            Do While drVi.Read
                dgvDrVisite.Rows.Add(drVi.Item("PPA"), drVi.Item("Jml"), drVi.Item("Total"), drVi.Item("Tarif"), drVi.Item("Tindakan"))
            Loop
            drVi.Close()
            cnVisit.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail JP Visite", MsgBoxStyle.Exclamation)
            cnVisit.Close()
        End Try

    End Sub
#End Region
#Region "JP Konsul"
    Sub detailJpKonsul()
        'Call koneksiServer()
        Dim cnKonsul As New MySqlConnection(KoneksiString)
        Dim queryKo As String = ""
        Dim cmdKo As MySqlCommand
        Dim drKo As MySqlDataReader

        If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
            queryKo = "CALL bdRj_Konsul('" & txtNoRanap.Text & "')"
        ElseIf unit.Contains("Rawat Inap") Then
            queryKo = "CALL bdRi_Konsul('" & txtNoRanap.Text & "')"
        End If
        'MsgBox(queryKo)
        Try
            cnKonsul.Open()
            cmdKo = New MySqlCommand(queryKo, cnKonsul)
            drKo = cmdKo.ExecuteReader
            dgvDrKonsulRanap.Rows.Clear()
            Do While drKo.Read
                If unit.Contains("Rawat Jalan") Or unit.Contains("Igd") Then
                    dgvDrIgdKonsul.Rows.Add(drKo.Item("PPA"), drKo.Item("Jml"), drKo.Item("Total"), drKo.Item("Tarif"), drKo.Item("Tindakan"))
                ElseIf unit.Contains("Rawat Inap") Then
                    dgvDrKonsulRanap.Rows.Add(drKo.Item("PPA"), drKo.Item("Jml"), drKo.Item("Total"), drKo.Item("Tarif"), drKo.Item("Tindakan"))
                End If

            Loop
            drKo.Close()
            cnKonsul.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
        End Try
        cnKonsul.Close()
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
        'Call koneksiServer()
        Dim cnOpe As New MySqlConnection(KoneksiString)
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        query = "CALL bdAll_Operator('" & noRegister & "')"

        Try
            cnOpe.Open()
            cmd = New MySqlCommand(query, cnOpe)
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
        cnOpe.Close()
    End Sub
#End Region
#Region "JP OperatorParu"
    Sub detailJpOpeParu()
        'Call koneksiServer()
        Dim cnOpeParu As New MySqlConnection(KoneksiString)
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        query = "CALL bdAll_OpeParu('" & noRegister & "')"

        Try
            cnOpeParu.Open()
            cmd = New MySqlCommand(query, cnOpeParu)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                txtDrParu.Text = dr.Item("operator")
            End If
            dr.Close()
            cnOpeParu.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail JP Operator", MsgBoxStyle.Exclamation)
            cnOpeParu.Close()
        End Try
    End Sub
#End Region
#Region "JP PPA GIZI"
    Sub detailJpGizi()
        'Call koneksiServer()
        Dim cnGz As New MySqlConnection(KoneksiString)
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
            cnGz.Open()
            cmdGz = New MySqlCommand(queryGz, cnGz)
            drGz = cmdGz.ExecuteReader
            dgvGizi.Rows.Clear()
            Do While drGz.Read
                dgvGizi.Rows.Add(drGz.Item("Tindakan"), drGz.Item("Tarif"), drGz.Item("Jml"), drGz.Item("PPA"), drGz.Item("Total"))
            Loop

            drGz.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Detail PPA GIZI", MessageBoxButtons.OK)
        End Try
        cnGz.Close()
    End Sub
#End Region
#Region "JP PPA FARKLIN"
    Sub detailJpFarklin()
        'Call koneksiServer()
        Dim cnFa As New MySqlConnection(KoneksiString)
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
            cnFa.Open()
            cmdFa = New MySqlCommand(queryFa, cnFa)
            drFa = cmdFa.ExecuteReader
            dgvFarklin.Rows.Clear()
            Do While drFa.Read
                dgvFarklin.Rows.Add(drFa.Item("Tindakan"), drFa.Item("Tarif"), drFa.Item("Jml"), drFa.Item("PPA"), drFa.Item("Total"))
            Loop

            drFa.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Detail PPA FARKLIN", MessageBoxButtons.OK)
        End Try
        cnFa.Close()
    End Sub
#End Region
#Region "JP PPA FISIO"
    Sub detailJpFisio()
        'Call koneksiServer()
        Dim cnFis As New MySqlConnection(KoneksiString)
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
            cnFis.Open()
            cmdFis = New MySqlCommand(queryFis, cnFis)
            drFis = cmdFis.ExecuteReader
            dgvFisio.Rows.Clear()
            Do While drFis.Read
                dgvFisio.Rows.Add(drFis.Item("Tindakan"), drFis.Item("Tarif"), drFis.Item("Jml"), drFis.Item("PPA"), drFis.Item("Total"))
            Loop

            drFis.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Detail PPA FISIO", MessageBoxButtons.OK)
        End Try
        cnFis.Close()
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
        'Call koneksiServer()
        MsgBox("Step 1")
        Dim cnRekapRajal As New MySqlConnection(KoneksiString)
        Dim queryRj As String = ""
        Dim cmdRj As MySqlCommand
        Dim drRj As MySqlDataReader

        queryRj = "Call rekapJPRajal('" & noRegister & "')"
        MsgBox(queryRj)
        Try
            MsgBox("Step 2")
            cnRekapRajal.Open()
            cmdRj = New MySqlCommand(queryRj, cnRekapRajal)
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
                txtTotParu.Text = CInt(drRj.Item("paru")).ToString("#,##0")
                txtTotObat.Text = CInt(drRj.Item("obat")).ToString("#,##0")
                txtTotAlkes.Text = CInt(drRj.Item("alkes")).ToString("#,##0")
                txtTotOxy.Text = CInt(drRj.Item("oksigen")).ToString("#,##0")
                txtTotKassa.Text = CInt(drRj.Item("kassa")).ToString("#,##0")
                txtTotVenti.Text = CInt(drRj.Item("venti")).ToString("#,##0")
                txtTotNebul.Text = CInt(drRj.Item("nebul")).ToString("#,##0")
                txtTotSyr.Text = CInt(drRj.Item("syringe")).ToString("#,##0")
                txtTotRontgen.Text = CInt(drRj.Item("rontgen")).ToString("#,##0")
                txtTotUsg.Text = CInt(drRj.Item("USG")).ToString("#,##0")
                txtTotCtscan.Text = CInt(drRj.Item("ctscan")).ToString("#,##0")
                txtTotMri.Text = CInt(drRj.Item("mri")).ToString("#,##0")
                txtTotRehab.Text = CInt(drRj.Item("rehab")).ToString("#,##0")
                txtTotLabPK.Text = CInt(drRj.Item("PK")).ToString("#,##0")
                txtTotLabPA.Text = CInt(drRj.Item("PA")).ToString("#,##0")
                txtTotEcg.Text = CInt(drRj.Item("ekg")).ToString("#,##0")
                txtTotEcho.Text = CInt(drRj.Item("echo")).ToString("#,##0")
                txtTotHolter.Text = CInt(drRj.Item("holter")).ToString("#,##0")
                txtTotTreadmill.Text = CInt(drRj.Item("tread")).ToString("#,##0")
                txtTotTindakan.Text = CInt(drRj.Item("tindakan")).ToString("#,##0")
            End If

            drRj.Close()
            cnRekapRajal.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Detail JP Rekap", MessageBoxButtons.OK)
            cnRekapRajal.Close()
        End Try
    End Sub
#End Region

#Region "Adm Poli #aa"
    Private Sub txtTotAdmPoli_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotAdmPoli.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotAdmPoli_TextChanged(sender As Object, e As EventArgs) Handles txtTotAdmPoli.TextChanged
        If txtTotAdmPoli.Text = "" Then
            txtTotAdmPoli.Text = 0
        End If
        aa = txtTotAdmPoli.Text
        txtTotAdmPoli.Text = Format(Val(aa), "#,##0")
        txtTotAdmPoli.SelectionStart = Len(txtTotAdmPoli.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Akomodasi #ab"
    Private Sub txtTotAkomodasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotAkomodasi.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotAkomodasi_TextChanged(sender As Object, e As EventArgs) Handles txtTotAkomodasi.TextChanged
        If txtTotAkomodasi.Text = "" Then
            txtTotAkomodasi.Text = 0
        End If
        ab = txtTotAkomodasi.Text
        txtTotAkomodasi.Text = Format(Val(ab), "#,##0")
        txtTotAkomodasi.SelectionStart = Len(txtTotAkomodasi.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub
#End Region
#Region "ICU #ac"
    Private Sub txtTotIcu_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotIcu.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotIcu_TextChanged(sender As Object, e As EventArgs) Handles txtTotIcu.TextChanged
        If txtTotIcu.Text = "" Then
            txtTotIcu.Text = 0
        End If
        ac = txtTotIcu.Text
        txtTotIcu.Text = Format(Val(ac), "#,##0")
        txtTotIcu.SelectionStart = Len(txtTotIcu.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub
#End Region
#Region "HCU #ad"
    Private Sub txtTotHcu_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotHcu.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotHcu_TextChanged(sender As Object, e As EventArgs) Handles txtTotHcu.TextChanged
        If txtTotHcu.Text = "" Then
            txtTotHcu.Text = 0
        End If
        ad = txtTotHcu.Text
        txtTotHcu.Text = Format(Val(ad), "#,##0")
        txtTotHcu.SelectionStart = Len(txtTotHcu.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub
#End Region
#Region "NICU #ae"
    Private Sub txtTotNicu_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotNicu.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotNicu_TextChanged(sender As Object, e As EventArgs) Handles txtTotNicu.TextChanged
        If txtTotNicu.Text = "" Then
            txtTotNicu.Text = 0
        End If
        ae = txtTotNicu.Text
        txtTotNicu.Text = Format(Val(ae), "#,##0")
        txtTotNicu.SelectionStart = Len(txtTotNicu.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "PICU #af"
    Private Sub txtTotPicu_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotPicu.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotPicu_TextChanged(sender As Object, e As EventArgs) Handles txtTotPicu.TextChanged
        If txtTotPicu.Text = "" Then
            txtTotPicu.Text = 0
        End If
        af = txtTotPicu.Text
        txtTotPicu.Text = Format(Val(af), "#,##0")
        txtTotPicu.SelectionStart = Len(txtTotPicu.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub
#End Region
#Region "dr IGD #ag"
    Private Sub TxtTotDrIgd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotDrIgd.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub TxtTotDrIgd_TextChanged(sender As Object, e As EventArgs) Handles txtTotDrIgd.TextChanged
        If txtTotDrIgd.Text = "" Then
            txtTotDrIgd.Text = 0
        End If
        ag = txtTotDrIgd.Text
        txtTotDrIgd.Text = Format(Val(ag), "#,##0")
        txtTotDrIgd.SelectionStart = Len(txtTotDrIgd.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub
#End Region
#Region "dr Poli #ah"
    Private Sub txtTotKonsulPoli_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotKonsulPoli.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotKonsulPoli_TextChanged(sender As Object, e As EventArgs) Handles txtTotKonsulPoli.TextChanged
        If txtTotKonsulPoli.Text = "" Then
            txtTotKonsulPoli.Text = 0
        End If
        ah = txtTotKonsulPoli.Text
        txtTotKonsulPoli.Text = Format(Val(ah), "#,##0")
        txtTotKonsulPoli.SelectionStart = Len(txtTotKonsulPoli.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Visite #ai"
    Private Sub txtTotVisite_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotVisite.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotVisite_TextChanged(sender As Object, e As EventArgs) Handles txtTotVisite.TextChanged
        If txtTotVisite.Text = "" Then
            txtTotVisite.Text = 0
        End If
        ai = txtTotVisite.Text
        txtTotVisite.Text = Format(Val(ai), "#,##0")
        txtTotVisite.SelectionStart = Len(txtTotVisite.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Konsultasi #aj"
    Private Sub txtTotKonsulRanap_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotKonsulRanap.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotKonsulRanap_TextChanged(sender As Object, e As EventArgs) Handles txtTotKonsulRanap.TextChanged
        If txtTotKonsulRanap.Text = "" Then
            txtTotKonsulRanap.Text = 0
        End If
        aj = txtTotKonsulRanap.Text
        txtTotKonsulRanap.Text = Format(Val(aj), "#,##0")
        txtTotKonsulRanap.SelectionStart = Len(txtTotKonsulRanap.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Operasi #ak"
    Private Sub txtTotOpe_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotOpe.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotOpe_TextChanged(sender As Object, e As EventArgs) Handles txtTotOpe.TextChanged
        If txtTotOpe.Text = "" Then
            txtTotOpe.Text = 0
        End If
        ak = txtTotOpe.Text
        txtTotOpe.Text = Format(Val(ak), "#,##0")
        txtTotOpe.SelectionStart = Len(txtTotOpe.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub
#End Region
#Region "RR #al"
    Private Sub txtTotRR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotRR.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotRR_TextChanged(sender As Object, e As EventArgs) Handles txtTotRR.TextChanged
        If txtTotRR.Text = "" Then
            txtTotRR.Text = 0
        End If
        al = txtTotRR.Text
        txtTotRR.Text = Format(Val(al), "#,##0")
        txtTotRR.SelectionStart = Len(txtTotRR.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub
#End Region
#Region "Endos #am"
    Private Sub txtTotEndos_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotEndos.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotEndos_TextChanged(sender As Object, e As EventArgs) Handles txtTotEndos.TextChanged
        If txtTotEndos.Text = "" Then
            txtTotEndos.Text = 0
        End If
        am = txtTotEndos.Text
        txtTotEndos.Text = Format(Val(am), "#,##0")
        txtTotEndos.SelectionStart = Len(txtTotEndos.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Bronc #an"
    Private Sub txtTotBronc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotBronc.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotBronc_TextChanged(sender As Object, e As EventArgs) Handles txtTotBronc.TextChanged
        If txtTotBronc.Text = "" Then
            txtTotBronc.Text = 0
        End If
        an = txtTotBronc.Text
        txtTotBronc.Text = Format(Val(an), "#,##0")
        txtTotBronc.SelectionStart = Len(txtTotBronc.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Cath Lab #ao"
    Private Sub txtTotCathLab_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotCathLab.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotCathLab_TextChanged(sender As Object, e As EventArgs) Handles txtTotCathLab.TextChanged
        If txtTotCathLab.Text = "" Then
            txtTotCathLab.Text = 0
        End If
        ao = txtTotCathLab.Text
        txtTotCathLab.Text = Format(Val(ao), "#,##0")
        txtTotCathLab.SelectionStart = Len(txtTotCathLab.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Hemo #ap"
    Private Sub txtTotHemo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotHemo.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotHemo_TextChanged(sender As Object, e As EventArgs) Handles txtTotHemo.TextChanged
        If txtTotHemo.Text = "" Then
            txtTotHemo.Text = 0
        End If
        ap = txtTotHemo.Text
        txtTotHemo.Text = Format(Val(ap), "#,##0")
        txtTotHemo.SelectionStart = Len(txtTotHemo.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Cvc #aq"
    Private Sub txtTotCvc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotCvc.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotCvc_TextChanged(sender As Object, e As EventArgs) Handles txtTotCvc.TextChanged
        If txtTotCvc.Text = "" Then
            txtTotCvc.Text = 0
        End If
        aq = txtTotCvc.Text
        txtTotCvc.Text = Format(Val(aq), "#,##0")
        txtTotCvc.SelectionStart = Len(txtTotCvc.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Paru #ar"
    Private Sub txtTotparu_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotParu.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotParu_TextChanged(sender As Object, e As EventArgs) Handles txtTotParu.TextChanged
        If txtTotParu.Text = "" Then
            txtTotParu.Text = 0
        End If
        ar = txtTotParu.Text
        txtTotParu.Text = Format(Val(ar), "#,##0")
        txtTotParu.SelectionStart = Len(txtTotParu.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Lain2 #as"
    Private Sub txtTotLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotLain.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotLain_TextChanged(sender As Object, e As EventArgs) Handles txtTotLain.TextChanged
        If txtTotLain.Text = "" Then
            txtTotLain.Text = 0
        End If
        a_s = txtTotLain.Text
        txtTotLain.Text = Format(Val(a_s), "#,##0")
        txtTotLain.SelectionStart = Len(txtTotLain.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Gizi #at"
    Private Sub txtTotGizi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotGizi.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotGizi_TextChanged(sender As Object, e As EventArgs) Handles txtTotGizi.TextChanged
        If txtTotGizi.Text = "" Then
            txtTotGizi.Text = 0
        End If
        at = txtTotGizi.Text
        txtTotGizi.Text = Format(Val(at), "#,##0")
        txtTotGizi.SelectionStart = Len(txtTotGizi.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Farklin #au"
    Private Sub txtTotFarklin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotFarklin.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotFarklin_TextChanged(sender As Object, e As EventArgs) Handles txtTotFarklin.TextChanged
        If txtTotFarklin.Text = "" Then
            txtTotFarklin.Text = 0
        End If
        au = txtTotFarklin.Text
        txtTotFarklin.Text = Format(Val(au), "#,##0")
        txtTotFarklin.SelectionStart = Len(txtTotFarklin.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Fisio #av"
    Private Sub txtTotFisio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotFisio.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotFisio_TextChanged(sender As Object, e As EventArgs) Handles txtTotFisio.TextChanged
        If txtTotFisio.Text = "" Then
            txtTotFisio.Text = 0
        End If
        av = txtTotFisio.Text
        txtTotFisio.Text = Format(Val(av), "#,##0")
        txtTotFisio.SelectionStart = Len(txtTotFisio.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Obat #aw"
    Private Sub txtTotObat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotObat.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotObat_TextChanged(sender As Object, e As EventArgs) Handles txtTotObat.TextChanged
        If txtTotObat.Text = "" Then
            txtTotObat.Text = 0
        End If
        aw = txtTotObat.Text
        txtTotObat.Text = Format(Val(aw), "#,##0")
        txtTotObat.SelectionStart = Len(txtTotObat.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Alkes #ax"
    Private Sub txtTotAlkes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotAlkes.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotAlkes_TextChanged(sender As Object, e As EventArgs) Handles txtTotAlkes.TextChanged
        If txtTotAlkes.Text = "" Then
            txtTotAlkes.Text = 0
        End If
        ax = txtTotAlkes.Text
        txtTotAlkes.Text = Format(Val(ax), "#,##0")
        txtTotAlkes.SelectionStart = Len(txtTotAlkes.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Oksigen #ay"
    Private Sub txtTotOxy_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotOxy.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotOxy_TextChanged(sender As Object, e As EventArgs) Handles txtTotOxy.TextChanged
        If txtTotOxy.Text = "" Then
            txtTotOxy.Text = 0
        End If
        ay = txtTotOxy.Text
        txtTotOxy.Text = Format(Val(ay), "#,##0")
        txtTotOxy.SelectionStart = Len(txtTotOxy.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Kassa #az"
    Private Sub txtTotKassa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotKassa.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotKassa_TextChanged(sender As Object, e As EventArgs) Handles txtTotKassa.TextChanged
        If txtTotKassa.Text = "" Then
            txtTotKassa.Text = 0
        End If
        az = txtTotKassa.Text
        txtTotKassa.Text = Format(Val(az), "#,##0")
        txtTotKassa.SelectionStart = Len(txtTotKassa.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Venti #ba"
    Private Sub txtTotVenti_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotVenti.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotVenti_TextChanged(sender As Object, e As EventArgs) Handles txtTotVenti.TextChanged
        If txtTotVenti.Text = "" Then
            txtTotVenti.Text = 0
        End If
        ba = txtTotVenti.Text
        txtTotVenti.Text = Format(Val(ba), "#,##0")
        txtTotVenti.SelectionStart = Len(txtTotVenti.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Nebul #bb"
    Private Sub txtTotNebul_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotNebul.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotNebul_TextChanged(sender As Object, e As EventArgs) Handles txtTotNebul.TextChanged
        If txtTotNebul.Text = "" Then
            txtTotNebul.Text = 0
        End If
        bb = txtTotNebul.Text
        txtTotNebul.Text = Format(Val(bb), "#,##0")
        txtTotNebul.SelectionStart = Len(txtTotNebul.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Syringe #bc"
    Private Sub txtTotSyr_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotSyr.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotSyr_TextChanged(sender As Object, e As EventArgs) Handles txtTotSyr.TextChanged
        If txtTotSyr.Text = "" Then
            txtTotSyr.Text = 0
        End If
        bc = txtTotSyr.Text
        txtTotSyr.Text = Format(Val(bc), "#,##0")
        txtTotSyr.SelectionStart = Len(txtTotSyr.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Monitor #bd"
    Private Sub txtTotMonitor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotMonitor.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotMonitor_TextChanged(sender As Object, e As EventArgs) Handles txtTotMonitor.TextChanged
        If txtTotMonitor.Text = "" Then
            txtTotMonitor.Text = 0
        End If
        bd = txtTotMonitor.Text
        txtTotMonitor.Text = Format(Val(bd), "#,##0")
        txtTotMonitor.SelectionStart = Len(txtTotMonitor.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Rontgen #be"
    Private Sub txtTotRontgen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotRontgen.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotRontgen_TextChanged(sender As Object, e As EventArgs) Handles txtTotRontgen.TextChanged
        If txtTotRontgen.Text = "" Then
            txtTotRontgen.Text = 0
        End If
        be = txtTotRontgen.Text
        txtTotRontgen.Text = Format(Val(be), "#,##0")
        txtTotRontgen.SelectionStart = Len(txtTotRontgen.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "USG #bf"
    Private Sub txtTotUsg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotUsg.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotUsg_TextChanged(sender As Object, e As EventArgs) Handles txtTotUsg.TextChanged
        If txtTotUsg.Text = "" Then
            txtTotUsg.Text = 0
        End If
        bf = txtTotUsg.Text
        txtTotUsg.Text = Format(Val(bf), "#,##0")
        txtTotUsg.SelectionStart = Len(txtTotUsg.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "CT SCAN #bg"
    Private Sub txtTotCtscan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotCtscan.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotCtscan_TextChanged(sender As Object, e As EventArgs) Handles txtTotCtscan.TextChanged
        If txtTotCtscan.Text = "" Then
            txtTotCtscan.Text = 0
        End If
        bg = txtTotCtscan.Text
        txtTotCtscan.Text = Format(Val(bg), "#,##0")
        txtTotCtscan.SelectionStart = Len(txtTotCtscan.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "MRI #bh"
    Private Sub txtTotMri_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotMri.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotMri_TextChanged(sender As Object, e As EventArgs) Handles txtTotMri.TextChanged
        If txtTotMri.Text = "" Then
            txtTotMri.Text = 0
        End If
        bh = txtTotMri.Text
        txtTotMri.Text = Format(Val(bh), "#,##0")
        txtTotMri.SelectionStart = Len(txtTotMri.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub
#End Region
#Region "LAB PK #bi"
    Private Sub txtTotLabPK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotLabPK.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotLabPK_TextChanged(sender As Object, e As EventArgs) Handles txtTotLabPK.TextChanged
        If txtTotLabPK.Text = "" Then
            txtTotLabPK.Text = 0
        End If
        bi = txtTotLabPK.Text
        txtTotLabPK.Text = Format(Val(bi), "#,##0")
        txtTotLabPK.SelectionStart = Len(txtTotLabPK.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "LAB PA #bj"
    Private Sub txtTotLabPA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotLabPA.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotLabPA_TextChanged(sender As Object, e As EventArgs) Handles txtTotLabPA.TextChanged
        If txtTotLabPA.Text = "" Then
            txtTotLabPA.Text = 0
        End If
        bj = txtTotLabPA.Text
        txtTotLabPA.Text = Format(Val(bj), "#,##0")
        txtTotLabPA.SelectionStart = Len(txtTotLabPA.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Darah #bk"
    Private Sub txtTotDarah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotDarah.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotDarah_TextChanged(sender As Object, e As EventArgs) Handles txtTotDarah.TextChanged
        If txtTotDarah.Text = "" Then
            txtTotDarah.Text = 0
        End If
        bk = txtTotDarah.Text
        txtTotDarah.Text = Format(Val(bk), "#,##0")
        txtTotDarah.SelectionStart = Len(txtTotDarah.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "ECG #bl"
    Private Sub txtTotEcg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotEcg.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotEcg_TextChanged(sender As Object, e As EventArgs) Handles txtTotEcg.TextChanged
        If txtTotEcg.Text = "" Then
            txtTotEcg.Text = 0
        End If
        bl = txtTotEcg.Text
        txtTotEcg.Text = Format(Val(bl), "#,##0")
        txtTotEcg.SelectionStart = Len(txtTotEcg.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Echo #bm"
    Private Sub txtTotEcho_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotEcho.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotEcho_TextChanged(sender As Object, e As EventArgs) Handles txtTotEcho.TextChanged
        If txtTotEcho.Text = "" Then
            txtTotEcho.Text = 0
        End If
        bm = txtTotEcho.Text
        txtTotEcho.Text = Format(Val(bm), "#,##0")
        txtTotEcho.SelectionStart = Len(txtTotEcho.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Holter #bn"
    Private Sub txtTotHolter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotHolter.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotHolter_TextChanged(sender As Object, e As EventArgs) Handles txtTotHolter.TextChanged
        If txtTotHolter.Text = "" Then
            txtTotHolter.Text = 0
        End If
        bn = txtTotHolter.Text
        txtTotHolter.Text = Format(Val(bn), "#,##0")
        txtTotHolter.SelectionStart = Len(txtTotHolter.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Treadmill #bo"
    Private Sub txtTotTreadmill_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotTreadmill.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotTreadmill_TextChanged(sender As Object, e As EventArgs) Handles txtTotTreadmill.TextChanged
        If txtTotTreadmill.Text = "" Then
            txtTotTreadmill.Text = 0
        End If
        bo = txtTotTreadmill.Text
        txtTotTreadmill.Text = Format(Val(bo), "#,##0")
        txtTotTreadmill.SelectionStart = Len(txtTotTreadmill.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Rehab #bp"
    Private Sub txtTotRehab_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotRehab.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotRehab_TextChanged(sender As Object, e As EventArgs) Handles txtTotRehab.TextChanged
        If txtTotRehab.Text = "" Then
            txtTotRehab.Text = 0
        End If
        bp = txtTotRehab.Text
        txtTotRehab.Text = Format(Val(bp), "#,##0")
        txtTotRehab.SelectionStart = Len(txtTotRehab.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Tindakan #bq"
    Private Sub txtTotTindakan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotTindakan.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotTindakan_TextChanged(sender As Object, e As EventArgs) Handles txtTotTindakan.TextChanged
        If txtTotTindakan.Text = "" Then
            txtTotTindakan.Text = 0
        End If
        bq = txtTotTindakan.Text
        txtTotTindakan.Text = Format(Val(bq), "#,##0")
        txtTotTindakan.SelectionStart = Len(txtTotTindakan.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Askep #br"
    Private Sub txtTotAskep_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotAskep.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotAskep_TextChanged(sender As Object, e As EventArgs) Handles txtTotAskep.TextChanged
        If txtTotAskep.Text = "" Then
            txtTotAskep.Text = 0
        End If
        br = txtTotAskep.Text
        txtTotAskep.Text = Format(Val(br), "#,##0")
        txtTotAskep.SelectionStart = Len(txtTotAskep.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Rohani #bs"
    Private Sub txtTotRohani_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotRohani.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotRohani_TextChanged(sender As Object, e As EventArgs) Handles txtTotRohani.TextChanged
        If txtTotRohani.Text = "" Then
            txtTotRohani.Text = 0
        End If
        bs = txtTotRohani.Text
        txtTotRohani.Text = Format(Val(bs), "#,##0")
        txtTotRohani.SelectionStart = Len(txtTotRohani.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "DPJP #bt"
    Private Sub txtTotDpjp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotDpjp.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotDpjp_TextChanged(sender As Object, e As EventArgs) Handles txtTotDpjp.TextChanged
        If txtTotDpjp.Text = "" Then
            txtTotDpjp.Text = 0
        End If
        bt = txtTotDpjp.Text
        txtTotDpjp.Text = Format(Val(bt), "#,##0")
        txtTotDpjp.SelectionStart = Len(txtTotDpjp.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "Jenazah #bu"
    Private Sub txtTotJenazah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotJenazah.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotJenazah_TextChanged(sender As Object, e As EventArgs) Handles txtTotJenazah.TextChanged
        If txtTotJenazah.Text = "" Then
            txtTotJenazah.Text = 0
        End If
        bu = txtTotJenazah.Text
        txtTotJenazah.Text = Format(Val(bu), "#,##0")
        txtTotJenazah.SelectionStart = Len(txtTotJenazah.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region
#Region "INACBG #bv"
    Private Sub txtInacbg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtInacbg.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtInacbg_TextChanged(sender As Object, e As EventArgs) Handles txtInacbg.TextChanged
        If txtInacbg.Text = "" Then
            txtInacbg.Text = 0
        End If
        bv = txtInacbg.Text
        txtInacbg.Text = Format(Val(bv), "#,##0")
        txtInacbg.SelectionStart = Len(txtInacbg.Text)
        txtTotalRincian.Text = Format(aa + ab + ac + ad + ae + af + ag + ah + ai + aj + ak + al +
                                      am + an + ao + ap + aq + ar + a_s + at + au + av + aw + ax +
                                      ay + az + ba + bb + bc + bd + be + bf + bg + bh + bi + bj +
                                      bk + bl + bm + bn + bo + bp + bq + br + bs + bt + bu + bv, "###,###")
    End Sub

#End Region

    Private Sub bgwAkomodasi_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwAkomodasi.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub bgwNonBedah_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwNonBedah.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub bgwBedah_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwBedah.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub bgwVisite_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwVisite.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub bgwKonsul_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwKonsul.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub bgwTenagaAhli_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwTenagaAhli.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub bgwGizi_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwGizi.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub bgwFarklin_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwFarklin.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub bgwFisio_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwFisio.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub bgwFarmasi_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwFarmasi.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub bgwAlkes_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwAlkes.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub bgwBmhp_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwBmhp.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub bgwAlatMedis_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwAlatMedis.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub bgwRadiologi_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwRadiologi.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub bgwLaborat_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwLaborat.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub bgwPenunjang_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwPenunjang.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub bgwRehab_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwRehab.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub bgwTindakan_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwTindakan.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub bgwAskep_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwAskep.RunWorkerCompleted
        If e.Cancelled = True Then
            Console.WriteLine("Proses Dibatalkan")
        Else
            Console.WriteLine("Proses Selesai")
        End If
    End Sub

    Private Sub Berakdown_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed

    End Sub
End Class