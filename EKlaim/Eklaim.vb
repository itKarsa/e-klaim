Imports MySql.Data.MySqlClient
Public Class Eklaim

    Public noDaftar As String
    Dim txtDpjp, DPJP, instalasi, caraKeluar, statusKeluar, jmlRuang, statusFinal As String
    Dim wsJenisRawat, wsKelas, wsCaraPulang, wspayor_id, wsJk, wsIcu As String
    Dim dateFinal, wsTglLahir As Date
    Dim txtTotDpjp As Integer

    Dim a, b, c, d, ee, f,
        g, h, i, j, k, l,
        m, n, o, p, q, r, t As Integer

    Dim ci As IFormatProvider = New System.Globalization.CultureInfo("id-ID", True)

    Dim EncrypKey As String = "b9192b9f14c33f39153ef32f12dd68fa61eec2f3df34e2b96c24c6078dba568a"

    Sub setColor(button As Button)
        btnHome.BackColor = Color.White
        btnEklaim.BackColor = Color.White
        btnBuku.BackColor = Color.White
        btnPiutang.BackColor = Color.White
        btnUmum.BackColor = Color.White
        button.BackColor = Color.FromArgb(209, 232, 223)
    End Sub

    Sub NewPasien()
        Dim payload As String = ""
        Dim jsonQuery As String = ""
        Dim req As String = ""
        Dim response As String = ""
        wsTglLahir = Convert.ToDateTime(Form1.tglLahir)
        Try
            jsonQuery = "{
                        ""metadata"": {
                            ""method"": ""new_claim""
                        },
                        ""data"": {
                            ""nomor_kartu"": """ & txtSetBpjs.Text & """,
                            ""nomor_sep"": """ & txtSetSep.Text & """,
                            ""nomor_rm"": """ & txtNoRM.Text & """,
                            ""nama_pasien"": """ & txtNamaPasien.Text & """,
                            ""tgl_lahir"": """ & Format(wsTglLahir, "yyyy-MM-dd HH:mm:ss") & """,
                            ""gender"": """ & wsJk & """
                        }
                    }"
            payload = inacbg_encrypt(jsonQuery, EncrypKey)
            req = reqPost(payload)
            response = inacbg_decrypt(req, EncrypKey)
            'MsgBox(response)
        Catch ex As Exception
            MessageBox.Show(ex.ToString & vbNewLine & response, "Error JSON QUERY", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Sub setKlaimDataPasien()
        Dim payload As String = ""
        Dim jsonQuery As String = ""
        Dim req As String = ""
        Dim response As String = ""
        Try
            jsonQuery = "{
                      ""metadata"": {
                        ""method"": ""set_claim_data"",
                        ""nomor_sep"": """ & txtSetSep.Text & """
                      },
                      ""data"": {
                        ""nomor_sep"": """ & txtSetSep.Text & """,
                        ""nomor_kartu"": """ & txtSetBpjs.Text & """,
                        ""tgl_masuk"": """ & Format(CDate(txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & """,
                        ""tgl_pulang"": """ & Format(CDate(txtTglKlrRawat.Text), "yyyy-MM-dd HH:mm:ss") & """,
                        ""jenis_rawat"": """ & wsJenisRawat & """,
                        ""kelas_rawat"": """ & wsKelas & """,
                        ""icu_indikator"": """ & wsIcu & """,
                        ""icu_los"": """ & NumericUpDown1.Value & """,
                        ""add_payment_pct"": ""0"",
                        ""birth_weight"": ""-"",
                        ""discharge_status"": """ & wsCaraPulang & """,
                        ""tarif_rs"": {
                          ""prosedur_non_bedah"": """ & Convert.ToDouble(txtNonBedah.Text, ci) & """,
                          ""prosedur_bedah"": """ & Convert.ToDouble(txtBedah.Text, ci) & """,
                          ""konsultasi"": """ & Convert.ToDouble(txtKonsul.Text, ci) & """,
                          ""tenaga_ahli"": """ & Convert.ToDouble(txtPPA.Text, ci) & """,
                          ""keperawatan"": """ & Convert.ToDouble(txtKeperawatan.Text, ci) & """,
                          ""penunjang"": """ & Convert.ToDouble(txtPenunjang.Text, ci) & """,
                          ""radiologi"": """ & Convert.ToDouble(txtRadiologi.Text, ci) & """,
                          ""laboratorium"": """ & Convert.ToDouble(txtLab.Text, ci) & """,
                          ""pelayanan_darah"": """ & Convert.ToDouble(txtDarah.Text, ci) & """,
                          ""rehabilitasi"": """ & Convert.ToDouble(txtRehab.Text, ci) & """,
                          ""kamar"": """ & Convert.ToDouble(txtAkomodasi.Text, ci) & """,
                          ""rawat_intensif"": """ & Convert.ToDouble(txtIntensif.Text, ci) & """,
                          ""obat"": """ & Convert.ToDouble(txtObat.Text, ci) & """,
                          ""obat_kronis"": """ & Convert.ToDouble(txtObatKronis.Text, ci) & """,
                          ""obat_kemoterapi"": """ & Convert.ToDouble(txtObatKemo.Text, ci) & """,
                          ""alkes"": """ & Convert.ToDouble(txtAlkes.Text, ci) & """,
                          ""bmhp"": """ & Convert.ToDouble(txtBMHP.Text, ci) & """,
                          ""sewa_alat"": """ & Convert.ToDouble(txtSewaAlat.Text, ci) & """
                          },
                        ""tarif_poli_eks"": """ & Convert.ToDouble(txtEksekutif.Text, ci) & """,
                        ""nama_dokter"": """ & txtDokter.Text & """,
                        ""kode_tarif"": ""CP"",
                        ""payor_id"": """ & wspayor_id & """,
                        ""payor_cd"": """ & txtSetJaminan.Text & """,
                        ""coder_nik"": ""123123123123""
                       }
                    }"
            payload = inacbg_encrypt(jsonQuery, EncrypKey)
            req = reqPost(payload)
            response = inacbg_decrypt(req, EncrypKey)
            MsgBox(response)
        Catch ex As Exception
            MessageBox.Show(ex.ToString & vbNewLine & response, "Error JSON QUERY", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub unSelect()
        txtKelas.TabStop = False
        txtCaraPulang.TabStop = False
        txtDokter.TabStop = False
        txtNonBedah.TabStop = False
        txtBedah.TabStop = False
        txtKonsul.TabStop = False
        txtPPA.TabStop = False
        txtKeperawatan.TabStop = False
        txtPenunjang.TabStop = False
        txtRadiologi.TabStop = False
        txtLab.TabStop = False
        txtDarah.TabStop = False
        txtRehab.TabStop = False
        txtAkomodasi.TabStop = False
        txtIntensif.TabStop = False
        txtObat.TabStop = False
        txtObatKronis.TabStop = False
        txtObatKemo.TabStop = False
        txtAlkes.TabStop = False
        txtBMHP.TabStop = False
        txtSewaAlat.TabStop = False
    End Sub

    Sub totalTarif()
        Dim totTarif As Long
        totTarif = 0
        For i As Integer = 0 To dgvDetail.Rows.Count - 1
            totTarif = totTarif + Val(dgvDetail.Rows(i).Cells(4).Value)
        Next
        txtTotalDetail.Text = totTarif.ToString("#,##0")
    End Sub

    Sub autoDokter()
        Call koneksiServer()

        Dim cmd As New MySqlCommand("SELECT namapetugasMedis FROM t_tenagamedis2 WHERE kdKelompokTenagaMedis = 'ktm1'", conn)
        Dim ad As New MySqlDataAdapter(cmd)
        Dim dt As New DataTable
        Dim col As New AutoCompleteStringCollection
        dt.Clear()
        ad.Fill(dt)

        For i As Integer = 0 To dt.Rows.Count - 1
            col.Add(dt.Rows(i)("namapetugasMedis"))
        Next

        txtDokter.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtDokter.AutoCompleteCustomSource = col
        txtDokter.AutoCompleteMode = AutoCompleteMode.Suggest

        conn.Close()
    End Sub

    Sub autoJaminan()
        Call koneksiServer()

        Dim cmd As New MySqlCommand("SELECT jaminan FROM t_eklaimjaminan WHERE aktif = 'enable'", conn)
        Dim ad As New MySqlDataAdapter(cmd)
        Dim ds As New DataSet
        ad.Fill(ds)
        txtSetJaminan.Items.Add("-")
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            txtSetJaminan.Items.Add(ds.Tables(0).Rows(i).Item("jaminan"))
            txtSetJaminan.AutoCompleteMode = AutoCompleteMode.Suggest
            txtSetJaminan.AutoCompleteSource = AutoCompleteSource.ListItems
        Next

        conn.Close()
    End Sub

    Sub autoAsuransi()
        Call koneksiServer()

        Using cmd As New MySqlCommand("SELECT asuransi FROM t_eklaimasuransi WHERE aktif = 'enable' ORDER BY asuransi ASC", conn)
            da = New MySqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)

            txtSetAsuransi.DataSource = dt
            txtSetAsuransi.DisplayMember = "asuransi"
            txtSetAsuransi.ValueMember = "asuransi"
            txtSetAsuransi.AutoCompleteMode = AutoCompleteMode.Suggest
            txtSetAsuransi.AutoCompleteSource = AutoCompleteSource.ListItems
        End Using

        conn.Close()
    End Sub

    Sub tampilDokter(noReg As String)
        Call koneksiServer()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        query = "SELECT
                    ppa.namapetugasMedis,
                    ins.instalasi,
                    cr.caraKeluar,
                    st.statusKeluar
                 FROM
                    t_registrasi AS reg
                    INNER JOIN t_tenagamedis2 AS ppa ON reg.kdTenagaMedis = ppa.kdPetugasMedis
                    INNER JOIN t_instalasiunit AS ins ON reg.kdInstalasi = ins.kdInstalasi
                    INNER JOIN t_carakeluar AS cr ON reg.kdCaraKeluar = cr.kdCaraKeluar
                    INNER JOIN t_statuskeluar AS st ON reg.kdStatusKeluar = st.kdStatusKeluar
                 WHERE reg.noDaftar = '" & noReg & "'"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                DPJP = dr.Item("namapetugasMedis").ToString
                instalasi = dr.Item("instalasi").ToString
                caraKeluar = dr.Item("caraKeluar").ToString
                statusKeluar = dr.Item("statusKeluar").ToString
            End If
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Function cekDataKlaim(rm As String, tipe As String, mrs As String, krs As String) As (idKlaim As String, stKlaim As String)
        Call koneksiServer()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim id As String = ""
        Dim status As String = ""
        query = "SELECT noEklaim,statusKlaim
		           FROM t_eklaimpasien
	              WHERE noRekamMedis = '" & rm & "'
                    AND tipe = '" & tipe & "'
                    AND tglMasuk = '" & mrs & "'
                    AND tglPulang = '" & krs & "'"

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                id = dr.Item("noEklaim").ToString
                status = dr.Item("statusKlaim").ToString
            End If
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()

        Return (id, status)
    End Function

    Sub tampilHasilKlaim()
        Call koneksiServer()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        query = "SELECT *
		           FROM t_eklaimdetailtarif
	              WHERE noEklaim = '" & txtNoEklaimLama.Text & "'"

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                txtNonBedah.Text = dr.Item("nonBedah").ToString
                txtBedah.Text = dr.Item("bedah").ToString
                txtKonsul.Text = dr.Item("konsultasi").ToString
                txtPPA.Text = dr.Item("tenagaAhli").ToString
                txtKeperawatan.Text = dr.Item("keperawatan").ToString
                txtPenunjang.Text = dr.Item("penunjang").ToString
                txtRadiologi.Text = dr.Item("radiologi").ToString
                txtLab.Text = dr.Item("laboratorium").ToString
                txtDarah.Text = dr.Item("bdrs").ToString
                txtRehab.Text = dr.Item("rehab").ToString
                txtAkomodasi.Text = dr.Item("akomodasi").ToString
                txtIntensif.Text = dr.Item("intensif").ToString
                txtObat.Text = dr.Item("obat").ToString
                txtObatKronis.Text = dr.Item("obatKronis").ToString
                txtObatKemo.Text = dr.Item("obatKemo").ToString
                txtAlkes.Text = dr.Item("alkes").ToString
                txtBMHP.Text = dr.Item("bmhp").ToString
                txtSewaAlat.Text = dr.Item("sewa").ToString
                txtTotalTarif.Text = dr.Item("totaltarif").ToString
            End If
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Function cekJmlRuang(noReg As String) As String
        Call koneksiServer()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim jml As String = ""
        query = "SELECT COUNT(tglMasukRawatInap) AS jml
		           FROM vw_daftarruangakomodasi
	              WHERE noDaftar = '" & noReg & "'"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                jml = dr.Item("jml").ToString
            End If
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()

        Return jml
    End Function

    Function cekTarifDpjp() As Integer
        Call koneksiServer()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim tarifDpjp As Integer
        query = "SELECT tarif
		           FROM vw_caritindakan
	              WHERE kdTarif LIKE '0209%' AND kelas = '" & txtKelas.Text & "'"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                tarifDpjp = dr.Item("tarif")
            End If
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()

        Return tarifDpjp
    End Function
#Region "Non Bedah"
    Function tampilNonBedah() As String
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value As String = ""

        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT (nonHD+HD+paru) AS nonBedah
                       FROM (SELECT COALESCE(SUM(totalTarif),0) AS nonHD
	                           FROM vw_tindakanpasienrajaldetail 
	                          WHERE noDaftar = '" & noDaftar & "'
		                        AND tindakan LIKE '%kateterisasi%'
		                        AND tindakan LIKE '%BRONK%'
		                        AND tindakan LIKE '%CVC%'
		                        AND kdTarif LIKE '40%'
		                        AND kdTarif LIKE '56%'
                            ) AS nonHD,
                            (SELECT COALESCE(SUM(subtotal),0) AS HD	
	                           FROM vw_tindakanhdrajaldetail
	                          WHERE noRegistrasi = '" & noDaftar & "'
                            ) AS HD,
                            (SELECT COALESCE(SUM(dtparu.subTotal),0) AS paru
		                       FROM t_tindakanokparu AS tparu
		                      INNER JOIN t_detailtindakanokparu AS dtparu ON tparu.noTindakanOP = dtparu.noTindakanOP
		                      INNER JOIN t_registrasiokparu AS rparu ON rparu.noRegistrasiOP = tparu.noRegistrasiOP
		                      INNER JOIN t_registrasi AS reg ON reg.noDaftar = rparu.noDaftarPasien 
		                      WHERE dtparu.statusHapus = 0 
			                    AND (dtparu.tindakan != 'RR' AND dtparu.kdTarif NOT LIKE '55%') 
			                    AND reg.noDaftar = '" & noDaftar & "' 
	                        ) AS okparu"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT (nonHD+HD+paru) AS nonBedah
                       FROM (SELECT COALESCE(SUM(totalTarif),0) AS nonHD
	                           FROM vw_tindakanpasienranapdetail 
	                          WHERE noDaftar = '" & noDaftar & "'
		                        AND tindakan LIKE '%kateterisasi%'
		                        AND tindakan LIKE '%BRONK%'
		                        AND tindakan LIKE '%CVC%'
		                        AND kdTarif LIKE '40%'
		                        AND kdTarif LIKE '56%'
                            ) AS nonHD,
                            (SELECT COALESCE(SUM(subtotal),0) AS HD	
	                           FROM vw_tindakanhdranapdetail
	                          WHERE noRegistrasi = '" & noDaftar & "'
                            ) AS HD,
                            (SELECT COALESCE(SUM(dtparu.subTotal),0) AS paru
		                       FROM t_tindakanokparu AS tparu
		                      INNER JOIN t_detailtindakanokparu AS dtparu ON tparu.noTindakanOP = dtparu.noTindakanOP
		                      INNER JOIN t_registrasiokparu AS rparu ON rparu.noRegistrasiOP = tparu.noRegistrasiOP
		                      INNER JOIN t_registrasi AS reg ON reg.noDaftar = rparu.noDaftarPasien 
		                      WHERE dtparu.statusHapus = 0 
			                    AND (dtparu.tindakan != 'RR' AND dtparu.kdTarif NOT LIKE '55%') 
			                    AND reg.noDaftar = '" & noDaftar & "' 
	                        ) AS okparu"
        End If

        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            value = dr.Item("nonBedah")
        End If
        conn.Close()
        Return value
    End Function

    Sub detailNonBedah()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT tglTindakan,tindakan,tarif,jumlahTindakan,totalTarif,PPA
                       FROM vw_tindakanpasienrajaldetail 
                      WHERE noDaftar = '" & noDaftar & "' 
	                    AND tindakan LIKE '%kateterisasi%'
	                    AND tindakan LIKE '%BRONK%'
	                    AND tindakan LIKE '%CVC%'
	                    AND kdTarif LIKE '40%'
	                    AND kdTarif LIKE '56%'
                      UNION ALL
                     SELECT tglTindakan,tindakan,tarifTindakan,jumlahTindakan,subtotal,PPA	
	                   FROM vw_tindakanhdrajaldetail
                      WHERE noRegistrasi = '" & noDaftar & "'
                      UNION ALL
                     SELECT tparu.tglTindakan,dtparu.tindakan,dtparu.tarif,dtparu.jmlTindakan,
                            dtparu.subTotal,dtparu.operator
                       FROM t_tindakanokparu AS tparu
                 INNER JOIN t_detailtindakanokparu AS dtparu ON tparu.noTindakanOP = dtparu.noTindakanOP
                 INNER JOIN t_registrasiokparu AS rparu ON rparu.noRegistrasiOP = tparu.noRegistrasiOP
                 INNER JOIN t_registrasi AS reg ON reg.noDaftar = rparu.noDaftarPasien
                      WHERE dtparu.statusHapus = 0 
                        AND (dtparu.tindakan != 'RR' AND dtparu.kdTarif NOT LIKE '55%')
                        AND reg.noDaftar = '" & noDaftar & "'"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT tglTindakan,tindakan,tarif,jumlahTindakan,totalTarif,PPA
                       FROM vw_tindakanpasienranapdetail 
                      WHERE noDaftar = '" & noDaftar & "' 
	                    AND tindakan LIKE '%kateterisasi%'
	                    AND tindakan LIKE '%BRONK%'
	                    AND tindakan LIKE '%CVC%'
	                    AND kdTarif LIKE '40%'
	                    AND kdTarif LIKE '56%'
                      UNION ALL
                     SELECT tglTindakan,tindakan,tarifTindakan,jumlahTindakan,subtotal,PPA	
	                   FROM vw_tindakanhdranapdetail
                      WHERE noRegistrasi = '" & noDaftar & "'
                      UNION ALL
                     SELECT tparu.tglTindakan,dtparu.tindakan,dtparu.tarif,dtparu.jmlTindakan,
                            dtparu.subTotal,dtparu.operator
                       FROM t_tindakanokparu AS tparu
                 INNER JOIN t_detailtindakanokparu AS dtparu ON tparu.noTindakanOP = dtparu.noTindakanOP
                 INNER JOIN t_registrasiokparu AS rparu ON rparu.noRegistrasiOP = tparu.noRegistrasiOP
                 INNER JOIN t_registrasi AS reg ON reg.noDaftar = rparu.noDaftarPasien
                      WHERE dtparu.statusHapus = 0 
                        AND (dtparu.tindakan != 'RR' AND dtparu.kdTarif NOT LIKE '55%')
                        AND reg.noDaftar = '" & noDaftar & "'"
        End If

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvDetail.Rows.Clear()
            Do While dr.Read
                dgvDetail.Rows.Add(dr.Item("tglTindakan"), dr.Item("tindakan"), dr.Item("tarif"),
                                   dr.Item("jumlahTindakan"), dr.Item("totalTarif"), dr.Item("PPA"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail Non Bedah", MsgBoxStyle.Exclamation)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "Bedah"
    Function tampilBedah() As String
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value As String = ""

        query = "SELECT COALESCE(SUM(dtop.subTotal),0) AS bedah
		           FROM t_tindakanop AS top
		          INNER JOIN t_detailtindakanop AS dtop ON top.noTindakanOP = dtop.noTindakanOP
		          INNER JOIN t_registrasiop AS rop ON rop.noRegistrasiOP = top.noRegistrasiOP
		          INNER JOIN t_registrasi AS reg ON reg.noDaftar = rop.noDaftarPasien
		          WHERE dtop.statusHapus = 0 
	                AND (dtop.tindakan != 'RR' AND dtop.kdTarif NOT LIKE '55%') 
	                AND reg.noDaftar = '" & noDaftar & "'"

        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            value = dr.Item("bedah")
        End If
        conn.Close()
        Return value
    End Function

    Sub detailBedah()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        query = "SELECT top.tglTindakan,dtop.tindakan,dtop.tarif,
                        dtop.jmlTindakan,dtop.subTotal,TRIM(LEADING ';' FROM top.dokterOP) AS operator
                   FROM t_tindakanop AS top
             INNER JOIN t_registrasiop AS rop ON rop.noRegistrasiOP = top.noRegistrasiOP
             INNER JOIN t_registrasi AS reg ON reg.noDaftar = rop.noDaftarPasien
             INNER JOIN t_detailtindakanop AS dtop ON top.noTindakanOP = dtop.noTindakanOP
             INNER JOIN t_tindakananestesi AS an ON rop.noRegistrasiOP = an.noRegistrasiOP
                  WHERE dtop.statusHapus = 0 
                    AND (dtop.tindakan != 'RR' AND dtop.kdTarif NOT LIKE '55%')
                    AND reg.noDaftar = '" & noDaftar & "'"

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvDetail.Rows.Clear()
            Do While dr.Read
                dgvDetail.Rows.Add(dr.Item("tglTindakan"), dr.Item("tindakan"), dr.Item("tarif"),
                                   dr.Item("jmlTindakan"), dr.Item("subTotal"), dr.Item("operator"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail Bedah", MsgBoxStyle.Exclamation)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "Tenaga Ahli"
    Function tampilJasa() As String
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value As String = ""

        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT COALESCE(SUM(totalTarif),0) AS tenaga_ahli
                   FROM vw_tindakanpasienrajaldetail 
                  WHERE noDaftar = '" & noDaftar & "' 
                    AND (tindakan LIKE 'JASA%' OR tindakan LIKE 'FISIOTERAPI%')
                    AND (tindakan NOT LIKE 'JASA VISITE%' 
                         AND tindakan NOT LIKE 'JASA ASUHAN KEPERAWATAN%'
                         AND tindakan NOT LIKE 'JASA PEMERIKSAAN%'
                         AND tindakan NOT LIKE 'JASA KONSULTASI%'
                         AND kdtarif NOT LIKE '38%' 
                         AND kdtarif NOT LIKE '54%')"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT COALESCE(SUM(totalTarif),0) AS tenaga_ahli
                   FROM vw_tindakanpasienranapdetail 
                  WHERE noDaftar = '" & noDaftar & "' 
                    AND (tindakan LIKE 'JASA%' OR tindakan LIKE 'FISIOTERAPI%')
                    AND (tindakan NOT LIKE 'JASA VISITE%'
                         AND tindakan NOT LIKE 'JASA ASUHAN KEPERAWATAN%'
                         AND tindakan NOT LIKE 'JASA KONSULTASI%'
                         AND kdtarif NOT LIKE '38%' 
                         AND kdtarif NOT LIKE '54%')"
        End If

        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            value = dr.Item("tenaga_ahli")
        End If
        conn.Close()
        Return value
    End Function

    Sub detailJasa()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT tglTindakan,tindakan,tarif,jumlahTindakan,totalTarif,PPA
                   FROM vw_tindakanpasienrajaldetail 
                  WHERE noDaftar = '" & noDaftar & "' 
                    AND tindakan LIKE 'JASA%'
                    AND (tindakan NOT LIKE 'JASA VISITE%' 
                         AND tindakan NOT LIKE 'JASA ASUHAN KEPERAWATAN%'
                         AND tindakan NOT LIKE 'JASA PEMERIKSAAN%'
                         AND tindakan NOT LIKE 'JASA KONSULTASI%'
                         AND kdtarif NOT LIKE '38%' 
                         AND kdtarif NOT LIKE '54%')"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT tglTindakan,tindakan,tarif,jumlahTindakan,totalTarif,PPA
                   FROM vw_tindakanpasienranapdetail 
                  WHERE noDaftar = '" & noDaftar & "' 
                    AND tindakan LIKE 'JASA%'
                    AND (tindakan NOT LIKE 'JASA VISITE%' 
                         AND tindakan NOT LIKE 'JASA ASUHAN KEPERAWATAN%'
                         AND tindakan NOT LIKE 'JASA KONSULTASI%'
                         AND kdtarif NOT LIKE '38%' 
                         AND kdtarif NOT LIKE '54%')"
        End If

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvDetail.Rows.Clear()
            Do While dr.Read
                dgvDetail.Rows.Add(dr.Item("tglTindakan"), dr.Item("tindakan"), dr.Item("tarif"),
                                   dr.Item("jumlahTindakan"), dr.Item("totalTarif"), dr.Item("PPA"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail Tenaga Ahli", MsgBoxStyle.Exclamation)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "Konsultasi"
    Function tampilVisite() As String
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value As String = ""
        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT 
                    (SELECT COALESCE(SUM(totalTarif),0) AS visite
                       FROM vw_tindakanpasienrajaldetail 
                      WHERE noDaftar = '" & noDaftar & "' 
                        AND (tindakan LIKE 'JASA PEMERIKSAAN DOKTER%' 
                         OR tindakan LIKE 'JASA KONSULTASI%'))
                            +
                    (SELECT COALESCE(SUM(rrj.konsulDokter),0) AS visite
                       FROM t_registrasirawatjalan AS rrj
                 INNER JOIN t_registrasi AS reg ON reg.noDaftar = rrj.noDaftar
                 INNER JOIN t_tenagamedis2 AS ppa ON reg.kdTenagaMedis = ppa.kdPetugasMedis
                      WHERE rrj.noDaftar = '" & noDaftar & "')
                         AS visite"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT COALESCE(SUM(totalTarif),0) AS visite
                       FROM vw_tindakanpasienranapdetail 
                      WHERE noDaftar = '" & noDaftar & "' 
                        AND (tindakan LIKE 'JASA VISITE%'
                         OR tindakan LIKE 'JASA KONSULTASI PER TELPHON%' 
                         OR tindakan LIKE 'JASA KONSULTASI DOKTER SPESIALIS KONSULTAN%'
                         OR tindakan LIKE 'JASA KONSULTASI SPESIALIS (PER KALI)%'
                         OR tindakan LIKE 'DOKTER PENANGGUNG JAWAB PASIEN%')"
        End If

        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            value = dr.Item("visite").ToString
        End If
        conn.Close()
        Return value
    End Function

    Sub detailVisite()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT tglTindakan,tindakan, tarif, 
                            jumlahTindakan, totalTarif, PPA
                       FROM vw_tindakanpasienrajaldetail 
                      WHERE noDaftar = '" & noDaftar & "' 
                        AND (tindakan LIKE 'JASA PEMERIKSAAN DOKTER%' 
                         OR tindakan LIKE 'JASA KONSULTASI%')
                      UNION ALL
                     SELECT rrj.tglMasukRawatJalan,'Konsultasi' AS tindakan,rrj.konsulDokter,
                            '1' AS jml,rrj.konsulDokter,ppa.namapetugasMedis
                       FROM t_registrasirawatjalan AS rrj
                 INNER JOIN t_registrasi AS reg ON reg.noDaftar = rrj.noDaftar
                 INNER JOIN t_tenagamedis2 AS ppa ON reg.kdTenagaMedis = ppa.kdPetugasMedis
                      WHERE rrj.noDaftar = '" & noDaftar & "'"
        ElseIf txtRawat.Text.Contains("Rawat Inap") And txtIntensif.Text = 0 Then
            query = "SELECT tglTindakan,tindakan, tarif, 
                            jumlahTindakan, totalTarif, PPA
                       FROM vw_tindakanpasienranapdetail 
                      WHERE noDaftar = '" & noDaftar & "' 
                        AND (tindakan LIKE 'JASA VISITE%' 
                         OR tindakan LIKE 'JASA KONSULTASI%' 
                         OR tindakan LIKE 'DOKTER PENANGGUNG JAWAB PASIEN%')
                      UNION ALL
                     SELECT mrs,tindakan,tarif,'1',total,DPJP
                       FROM (SELECT tindakan,tarif,tarif AS total 
	                           FROM vw_caritindakan
                              WHERE kdTarif LIKE '0209%' AND kelas = '" & txtKelas.Text & "') AS tind,
                            (SELECT reg.tglDaftar AS mrs, dpjp.namapetugasMedis AS DPJP
                               FROM t_registrasi AS reg
                              INNER JOIN t_tenagamedis2 AS dpjp ON reg.kdTenagaMedis = dpjp.kdPetugasMedis
                              WHERE reg.noDaftar = '" & noDaftar & "') AS dokter"
        ElseIf txtRawat.Text.Contains("Rawat Inap") And txtIntensif.Text <> 0 Then
            query = "SELECT tglTindakan,tindakan, tarif, 
                            jumlahTindakan, totalTarif, PPA
                       FROM vw_tindakanpasienranapdetail 
                      WHERE noDaftar = '" & noDaftar & "' 
                        AND (tindakan LIKE 'JASA VISITE%' 
                         OR tindakan LIKE 'JASA KONSULTASI%' 
                         OR tindakan LIKE 'DOKTER PENANGGUNG JAWAB PASIEN%')"
        End If

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvDetail.Rows.Clear()
            Do While dr.Read
                dgvDetail.Rows.Add(dr.Item("tglTindakan"), dr.Item("tindakan"), dr.Item("tarif"),
                                   dr.Item("jumlahTindakan"), dr.Item("totalTarif"), dr.Item("PPA"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail Konsultasi", MsgBoxStyle.Exclamation)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "Keperawatan"
    Function tampilTindakan() As String
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value As String = ""
        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT COALESCE(SUM(totalTarif),0) AS tindakan
                       FROM vw_tindakanpasienrajaldetail 
                      WHERE noDaftar = '" & noDaftar & "'
                        AND (tindakan NOT LIKE 'JASA%'
                            AND tindakan NOT LIKE 'OKSIGEN%'
                            AND tindakan NOT LIKE '%SEWA VENTILATOR%'
                            AND tindakan NOT LIKE '%SEWA SYRINGE%'
                            AND tindakan NOT LIKE '%NEBUL%'
                            XOR tindakan LIKE '%JASA ASUHAN KEPERAWATAN%')
                       AND (kdTarif NOT IN (0500040,1300011,4800042,4900042,5500060,020910,020920,020930,
                                             020940,020950,020960,020970,480710,490810,552501)
                            AND kdTarif NOT LIKE '38%' 
                            AND kdTarif NOT LIKE '54%'
                            XOR kdTarif LIKE '45%')"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT COALESCE(SUM(totalTarif),0) AS tindakan
                       FROM vw_tindakanpasienranapdetail 
                      WHERE noDaftar = '" & noDaftar & "' 
                        AND (tindakan NOT LIKE 'JASA%'
                            AND tindakan NOT LIKE 'OKSIGEN%'
                            AND tindakan NOT LIKE '%SEWA VENTILATOR%'
                            AND tindakan NOT LIKE '%SEWA SYRINGE%'
                            AND tindakan NOT LIKE '%NEBUL%'
                            XOR tindakan LIKE '%JASA ASUHAN KEPERAWATAN%')
                        AND (kdTarif NOT IN (0500040,1300011,4800042,4900042,5500060,020910,020920,020930,
                                             020940,020950,020960,020970,480710,490810,552501)
                            AND kdtarif NOT LIKE '38%' 
                            AND kdtarif NOT LIKE '54%'
                            XOR kdTarif LIKE '45%')"
        End If

        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            value = dr.Item("tindakan").ToString
        End If
        conn.Close()
        Return value
    End Function

    Sub detailTindakan()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT tglTindakan,tindakan, tarif, 
                            jumlahTindakan, totalTarif, PPA
                       FROM vw_tindakanpasienrajaldetail 
                      WHERE noDaftar = '" & noDaftar & "'
                        AND (tindakan NOT LIKE 'JASA%'
                            AND tindakan NOT LIKE 'OKSIGEN%'
                            AND tindakan NOT LIKE '%SEWA VENTILATOR%'
                            AND tindakan NOT LIKE '%SEWA SYRINGE%'
                            AND tindakan NOT LIKE '%NEBUL%'
                            XOR tindakan LIKE '%JASA ASUHAN KEPERAWATAN%')
                        AND (kdTarif NOT IN (0500040,1300011,4800042,4900042,5500060,020910,020920,020930,
                                             020940,020950,020960,020970,480710,490810,552501)
                            AND kdtarif NOT LIKE '38%' 
                            AND kdtarif NOT LIKE '54%'
                            XOR kdTarif LIKE '45%')"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT tglTindakan,tindakan, tarif, 
                            jumlahTindakan, totalTarif, PPA
                       FROM vw_tindakanpasienranapdetail 
                      WHERE noDaftar = '" & noDaftar & "' 
                        AND (tindakan NOT LIKE 'JASA%'
                            AND tindakan NOT LIKE 'OKSIGEN%'
                            AND tindakan NOT LIKE '%SEWA VENTILATOR%'
                            AND tindakan NOT LIKE '%SEWA SYRINGE%'
                            AND tindakan NOT LIKE '%NEBUL%'
                            XOR tindakan LIKE '%JASA ASUHAN KEPERAWATAN%')
                        AND (kdTarif NOT IN (0500040,1300011,4800042,4900042,5500060,020910,020920,020930,
                                             020940,020950,020960,020970,480710,490810,552501)
                            AND kdtarif NOT LIKE '38%' 
                            AND kdtarif NOT LIKE '54%'
                            XOR kdTarif LIKE '45%')"
        End If

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvDetail.Rows.Clear()
            Do While dr.Read
                dgvDetail.Rows.Add(dr.Item("tglTindakan"), dr.Item("tindakan"), dr.Item("tarif"),
                                   dr.Item("jumlahTindakan"), dr.Item("totalTarif"), dr.Item("PPA"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail Keperawatan", MsgBoxStyle.Exclamation)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "Penunjang"
    Function tampilPenunjang() As String
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value As String = ""
        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT COALESCE(SUM(totalTarif),0) AS tindakan
                       FROM vw_tindakanpasienrajaldetail 
                      WHERE noDaftar = '" & noDaftar & "'
                        AND tindakan LIKE 'ECHO%'
                        AND tindakan LIKE 'EKG%'
                        AND tindakan LIKE 'ECG%'
                        AND tindakan LIKE 'HOLTER%'"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT COALESCE(SUM(totalTarif),0) AS tindakan
                       FROM vw_tindakanpasienranapdetail 
                      WHERE noDaftar = '" & noDaftar & "' 
                        AND tindakan LIKE 'ECHO%'
                        AND tindakan LIKE 'EKG%'
                        AND tindakan LIKE 'ECG%'
                        AND tindakan LIKE 'HOLTER%'"
        End If

        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            value = dr.Item("tindakan").ToString
        End If
        conn.Close()
        Return value
    End Function

    Sub detailPenunjang()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT tglTindakan,tindakan, tarif, 
                            jumlahTindakan, totalTarif, PPA
                       FROM vw_tindakanpasienrajaldetail 
                      WHERE noDaftar = '" & noDaftar & "'
                        AND tindakan LIKE 'ECHO%'
                        AND tindakan LIKE 'EKG%'
                        AND tindakan LIKE 'ECG%'
                        AND tindakan LIKE 'HOLTER%'"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT tglTindakan,tindakan, tarif, 
                            jumlahTindakan, totalTarif, PPA
                       FROM vw_tindakanpasienranapdetail 
                      WHERE noDaftar = '" & noDaftar & "' 
                        AND tindakan LIKE 'ECHO%'
                        AND tindakan LIKE 'EKG%'
                        AND tindakan LIKE 'ECG%'
                        AND tindakan LIKE 'HOLTER%'"
        End If

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvDetail.Rows.Clear()
            Do While dr.Read
                dgvDetail.Rows.Add(dr.Item("tglTindakan"), dr.Item("tindakan"), dr.Item("tarif"),
                                   dr.Item("jumlahTindakan"), dr.Item("totalTarif"), dr.Item("PPA"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail Penunjang", MsgBoxStyle.Exclamation)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "Laboratorium"
    Function tampilLab() As Integer
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value As Integer = 0

        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT (pk+pa) AS lab
                       FROM (SELECT COALESCE(SUM(totalTarif),0) AS pk
                               FROM vw_datadetaillabrajal
                              WHERE noDaftar = '" & noDaftar & "' 
                            ) labpk,
                            (SELECT COALESCE(SUM(totalTarif),0) AS pa
                               FROM vw_datadetailparajal
                              WHERE noDaftar = '" & noDaftar & "'
                            ) labpa"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT (pk+pa) AS lab
                       FROM (SELECT COALESCE(SUM(totalTarif),0) AS pk
                               FROM vw_datadetaillabranap
                              WHERE noDaftar = '" & noDaftar & "'
                            ) labpk,       
                            (SELECT COALESCE(SUM(totalTarif),0) AS pa
                               FROM vw_datadetailparanap
                              WHERE noDaftar = '" & noDaftar & "'
                            ) labpa"
        End If

        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            value = dr.Item("lab")
        End If
        conn.Close()
        Return value
    End Function

    Sub detailLab()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT tglMasukPenunjangRajal AS tglMasuk,tindakan, tarif, 
                            jumlahTindakan, totalTarif, PPA
                       FROM vw_datadetaillabrajal
                      WHERE noDaftar = '" & noDaftar & "' 
                      UNION ALL
                     SELECT tglMasukPARajal AS tglMasuk,tindakan, tarif, 
                            jumlahTindakan, totalTarif, PPA
                       FROM vw_datadetailparajal
                      WHERE noDaftar = '" & noDaftar & "'"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT tglMasukPenunjangRanap AS tglMasuk,tindakan, tarif, 
                            jumlahTindakan, totalTarif, PPA
                       FROM vw_datadetaillabranap
                      WHERE noDaftar = '" & noDaftar & "' 
                      UNION ALL
                     SELECT tglMasukPARanap AS tglMasuk,tindakan, tarif, 
                            jumlahTindakan, totalTarif, PPA
                       FROM vw_datadetailparanap
                      WHERE noDaftar = '" & noDaftar & "'"
        End If

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvDetail.Rows.Clear()
            Do While dr.Read
                dgvDetail.Rows.Add(dr.Item("tglMasuk"), dr.Item("tindakan"), dr.Item("tarif"),
                                   dr.Item("jumlahTindakan"), dr.Item("totalTarif"), dr.Item("PPA"))
            Loop
            dr.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Detail Laboratorium", MessageBoxButtons.OK)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "Radiologi"
    Function tampilRad() As String
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value As String = ""

        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT COALESCE(SUM(totalTarif),0) AS rad
                       FROM vw_pasienradrajaldetail
                      WHERE noDaftar = '" & noDaftar & "'"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT COALESCE(SUM(totalTarif),0) AS rad
                       FROM vw_pasienradranapdetail
                      WHERE noDaftar = '" & noDaftar & "'"
        End If

        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            value = dr.Item("rad").ToString
        End If
        conn.Close()
        Return value
    End Function

    Sub detailRad()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT tglMasukRadiologiRajal AS tglMasuk,tindakan, tarif, 
                        jumlahTindakan, totalTarif, PPA
                   FROM vw_pasienradrajaldetail
                  WHERE noDaftar = '" & noDaftar & "'"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT tglMasukRadiologiRanap AS tglMasuk,tindakan, tarif, 
                        jumlahTindakan, totalTarif, PPA
                   FROM vw_pasienradranapdetail
                  WHERE noDaftar = '" & noDaftar & "'"
        End If

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvDetail.Rows.Clear()
            Do While dr.Read
                dgvDetail.Rows.Add(dr.Item("tglMasuk"), dr.Item("tindakan"), dr.Item("tarif"),
                                   dr.Item("jumlahTindakan"), dr.Item("totalTarif"), dr.Item("PPA"))
            Loop
            dr.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Detail Radiologi", MessageBoxButtons.OK)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "BDRS"
    Function tampilDarah() As String
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value As String = ""

        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT COALESCE(SUM(dtlr.totalTarif),0) AS bdrs
                       FROM t_registrasibdrsrajal AS rlr
	             INNER JOIN t_tindakanbdrs AS tlr ON rlr.noRegistrasiBDRS = tlr.noRegistrasiBDRS
	             INNER JOIN t_detailtindakanbdrs AS dtlr ON tlr.noTindakanBDRS = dtlr.noTindakanBDRS
                      WHERE rlr.noDaftar = '" & noDaftar & "'"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT COALESCE(SUM(dtlr.totalTarif),0) AS bdrs
                       FROM t_registrasibdrsranap AS rlr
	             INNER JOIN t_tindakanbdrs AS tlr ON rlr.noRegistrasiBDRS = tlr.noRegistrasiBDRS
	             INNER JOIN t_detailtindakanbdrs AS dtlr ON tlr.noTindakanBDRS = dtlr.noTindakanBDRS
                      WHERE rlr.noDaftar = '" & noDaftar & "'"
        End If

        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            value = dr.Item("bdrs").ToString
        End If
        conn.Close()
        Return value
    End Function

    Sub detailDarah()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT rlr.tglMasukBDRS AS tglMasuk,
	                        dtlr.tindakan,
	                        dtlr.tarif,
	                        dtlr.jumlahTindakan,
	                        dtlr.totalTarif,
	                        COALESCE(ppa.namapetugasMedis,'-') AS PPA 
                       FROM t_registrasibdrsrajal AS rlr
	             INNER JOIN t_tindakanbdrs AS tlr ON rlr.noRegistrasiBDRS = tlr.noRegistrasiBDRS
	             INNER JOIN t_detailtindakanbdrs AS dtlr ON tlr.noTindakanBDRS = dtlr.noTindakanBDRS
	              LEFT JOIN t_tenagamedis2 AS ppa ON rlr.kdDokterPemeriksa = ppa.kdPetugasMedis 
                      WHERE rlr.noDaftar = '" & noDaftar & "'"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT rlr.tglMasukBDRS AS tglMasuk,
	                        dtlr.tindakan,
	                        dtlr.tarif,
	                        dtlr.jumlahTindakan,
	                        dtlr.totalTarif,
	                        COALESCE(ppa.namapetugasMedis,'-') AS PPA 
                       FROM t_registrasibdrsranap AS rlr
	             INNER JOIN t_tindakanbdrs AS tlr ON rlr.noRegistrasiBDRS = tlr.noRegistrasiBDRS
	             INNER JOIN t_detailtindakanbdrs AS dtlr ON tlr.noTindakanBDRS = dtlr.noTindakanBDRS
	              LEFT JOIN t_tenagamedis2 AS ppa ON rlr.kdDokterPemeriksa = ppa.kdPetugasMedis 
                      WHERE rlr.noDaftar = '" & noDaftar & "'"
        End If

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvDetail.Rows.Clear()
            Do While dr.Read
                dgvDetail.Rows.Add(dr.Item("tglMasuk"), dr.Item("tindakan"), dr.Item("tarif"),
                                   dr.Item("jumlahTindakan"), dr.Item("totalTarif"), dr.Item("PPA"))
            Loop
            dr.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Detail BDRS", MessageBoxButtons.OK)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "Rehab"
    Function tampilRehab() As String
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value As String = ""

        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT COALESCE(SUM(totalTarif),0) AS rehab
                   FROM vw_tindakanpasienrajaldetail 
                  WHERE noDaftar = '" & noDaftar & "' 
                    AND (kdTarif LIKE '38%' OR kdTarif LIKE '54%')"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT COALESCE(SUM(totalTarif),0) AS rehab
                   FROM vw_tindakanpasienranapdetail 
                  WHERE noDaftar = '" & noDaftar & "' 
                    AND (kdTarif LIKE '38%' OR kdTarif LIKE '54%')"
        End If

        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            value = dr.Item("rehab")
        End If
        conn.Close()
        Return value
    End Function

    Sub detailRehab()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT tglTindakan,tindakan,tarif,jumlahTindakan,totalTarif,PPA
                   FROM vw_tindakanpasienrajaldetail 
                  WHERE noDaftar = '" & noDaftar & "' 
                    AND (kdTarif LIKE '38%' OR kdTarif LIKE '54%')"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT tglTindakan,tindakan,tarif,jumlahTindakan,totalTarif,PPA
                   FROM vw_tindakanpasienranapdetail 
                  WHERE noDaftar = '" & noDaftar & "' 
                    AND (kdTarif LIKE '38%' OR kdTarif LIKE '54%')"
        End If

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvDetail.Rows.Clear()
            Do While dr.Read
                dgvDetail.Rows.Add(dr.Item("tglTindakan"), dr.Item("tindakan"), dr.Item("tarif"),
                                   dr.Item("jumlahTindakan"), dr.Item("totalTarif"), dr.Item("PPA"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail Rehab Medik", MsgBoxStyle.Exclamation)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "Akomodasi"
    Function tampilAkomodasi() As String
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value As String = ""

        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT karciPendaftaran AS akomodasi
                       FROM t_registrasirawatjalan
                      WHERE noDaftar = '" & noDaftar & "'"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT COALESCE(SUM(totalMenginap),0) AS akomodasi
                       FROM vw_daftarruangakomodasi
                      WHERE noDaftar = '" & noDaftar & "'
                        AND (rawatInap NOT LIKE '%ICU%' 
                         AND rawatInap NOT LIKE '%HCU%' 
                         AND rawatInap NOT LIKE '%NICU%' 
                         AND rawatInap NOT LIKE '%PICU%'
                         AND rawatInap NOT LIKE '%LAVENDER TANPA VENTILATOR%'
                         AND rawatInap NOT LIKE '%LAVENDER VENTILATOR%')"
        End If

        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            value = dr.Item("akomodasi").ToString
        End If
        conn.Close()
        Return value
    End Function

    Sub detailAkomodasi()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT rj.tglMasukRawatJalan AS tglMasuk,
                            u.unit AS unit,
                            rj.karciPendaftaran AS karcis,
                            rj.konsulDokter AS tarifPoli,
                            (rj.KarciPendaftaran + rj.konsulDokter) AS totalTarif,
                            '-' AS kelas
                       FROM t_registrasirawatjalan AS rj
                            INNER JOIN t_unit AS u ON u.kdUnit = rj.kdUnit
                      WHERE noDaftar = '" & noDaftar & "'"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT tglMasukRawatInap, rawatInap, tarifKmr,
                            jumlahHariMenginap, totalMenginap, kelas
                       FROM vw_daftarruangakomodasi
                      WHERE noDaftar = '" & noDaftar & "'
                        AND (rawatInap NOT LIKE '%ICU%' 
                         AND rawatInap NOT LIKE '%HCU%' 
                         AND rawatInap NOT LIKE '%NICU%' 
                         AND rawatInap NOT LIKE '%PICU%'
                         AND rawatInap NOT LIKE '%LAVENDER TANPA VENTILATOR%'
                         AND rawatInap NOT LIKE '%LAVENDER VENTILATOR%')"
        End If

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvDetail.Rows.Clear()
            If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
                Do While dr.Read
                    dgvDetail.Rows.Add(dr.Item("tglMasuk"), dr.Item("unit"), dr.Item("karcis"),
                                   dr.Item("tarifPoli"), dr.Item("totalTarif"), dr.Item("kelas"))
                Loop
            ElseIf txtRawat.Text.Contains("Rawat Inap") Then
                Do While dr.Read
                    dgvDetail.Rows.Add(dr.Item("tglMasukRawatInap"), dr.Item("rawatInap"), dr.Item("tarifKmr"),
                                   dr.Item("jumlahHariMenginap"), dr.Item("totalMenginap"), dr.Item("kelas"))
                Loop
            End If

            dr.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Detail Akomodasi", MessageBoxButtons.OK)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "Intensif"
    Function tampilIntensif() As String
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value As String = ""

        query = "SELECT COALESCE(SUM(totalMenginap),0) AS akomodasi
                   FROM vw_daftarruangakomodasi
                  WHERE noDaftar = '" & noDaftar & "'
                    AND (rawatInap LIKE '%ICU%'
                         OR rawatInap LIKE '%HCU%'
                         OR rawatInap LIKE '%LAVENDER TANPA VENTILATOR%'
                         OR rawatInap LIKE '%LAVENDER VENTILATOR%')"

        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            value = dr.Item("akomodasi").ToString
        ElseIf dr.IsDBNull(0) Then
            value = "0"
        End If
        conn.Close()
        Return value
    End Function

    Sub detailIntensif()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        query = "SELECT tglMasukRawatInap, kelas, tarifKmr,
                        jumlahHariMenginap, totalMenginap, rawatInap
                   FROM vw_daftarruangakomodasi
                  WHERE noDaftar = '" & noDaftar & "'
                    AND (rawatInap LIKE '%ICU%'
                         OR rawatInap LIKE '%HCU%'
                         OR rawatInap LIKE '%LAVENDER TANPA VENTILATOR%'
                         OR rawatInap LIKE '%LAVENDER VENTILATOR%')"

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvDetail.Rows.Clear()
            Do While dr.Read
                dgvDetail.Rows.Add(dr.Item("tglMasukRawatInap"), dr.Item("kelas"), dr.Item("tarifKmr"),
                                   dr.Item("jumlahHariMenginap"), dr.Item("totalMenginap"), dr.Item("rawatInap"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail Intensif", MsgBoxStyle.Exclamation)
        End Try
        conn.Close()
    End Sub
#End Region
#Region "Obat"
    Function tampilObat() As (kronis As String, nonKronis As String)
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value1 As String = ""
        Dim value2 As String = ""

        If txtRawat.Text.Contains("Rawat Jalan") Then
            query = "SELECT (totalKronisRJ+totalKronisOK) AS totalKronis,
                            (totalNonKronisRJ+totalNonKronisOK) AS totalNonKronis
                       FROM (SELECT COALESCE(SUM(detail.harga * detail.diberikanKronis),0) AS totalKronisRJ,
		                            COALESCE(SUM(detail.harga * detail.diberikanNonKronis),0) AS totalNonKronisRJ
	                           FROM simrs.t_registrasi AS reg
		                            INNER JOIN farmasi2.t_penjualanobatrajal AS jual ON reg.noDaftar = jual.noDaftar
		                            INNER JOIN farmasi2.t_detailpenjualanobatrajal AS detail ON jual.noPenjualanObatRajal = detail.noPenjualanObatRajal
		                            INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
	                          WHERE reg.noDaftar = '" & noDaftar & "' AND
		                            obat.kdKelompokObat IN ('KO03','KO04')
                            ) AS rajal,
                            (SELECT COALESCE(SUM(detail.harga * detail.diberikanKronis),0) AS totalKronisOK,
	                                COALESCE(SUM(detail.harga * detail.diberikanNonKronis),0) AS totalNonKronisOK
                               FROM simrs.t_registrasi AS reg
	                                INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
	                                INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
	                                INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
                              WHERE reg.noDaftar = '" & noDaftar & "' AND
	                                obat.kdKelompokObat IN ('KO03','KO04')
                            ) AS okrajal"
        ElseIf txtRawat.Text.Contains("Igd") Then
            Select Case txtUnit.Text
                Case "IGD"
                    query = "SELECT (totalKronisIGD+totalKronisOK) AS totalKronis,
                            (totalNonKronisIGD+totalNonKronisOK) AS totalNonKronis
                       FROM (SELECT COALESCE(SUM(detail.harga * detail.diberikanKronis),0) AS totalKronisIGD,
	                                COALESCE(SUM(detail.harga * detail.diberikanNonKronis),0) AS totalNonKronisIGD
                               FROM simrs.t_registrasi AS reg
	                                INNER JOIN farmasi2.t_penjualanobatigd AS jual ON reg.noDaftar = jual.noDaftar
	                                INNER JOIN farmasi2.t_detailpenjualanobatigd AS detail ON jual.noPenjualanObatIGD = detail.noPenjualanObatIGD
	                                INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
                              WHERE reg.noDaftar = '" & noDaftar & "' AND
	                                obat.kdKelompokObat IN ('KO03','KO04')
                            ) AS igd,
                            (SELECT COALESCE(SUM(detail.harga * detail.diberikanKronis),0) AS totalKronisOK,
	                                COALESCE(SUM(detail.harga * detail.diberikanNonKronis),0) AS totalNonKronisOK
                               FROM simrs.t_registrasi AS reg
	                                INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
	                                INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
	                                INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
                              WHERE reg.noDaftar = '" & noDaftar & "' AND
	                                obat.kdKelompokObat IN ('KO03','KO04')
                            ) AS okigd"
                Case "IGD PINERE"
                    query = "SELECT (totalKronisRI+totalKronisOK) AS totalKronis,
                            (totalNonKronisRI+totalNonKronisOK) AS totalNonKronis
                       FROM (SELECT COALESCE(SUM(detail.harga * detail.diberikanKronis),0) AS totalKronisRI,
		                            COALESCE(SUM(detail.harga * detail.diberikanNonKronis),0) AS totalNonKronisRI
	                           FROM simrs.t_registrasi AS reg
		                            INNER JOIN farmasi2.t_penjualanobatranap AS jual ON reg.noDaftar = jual.noDaftar
		                            INNER JOIN farmasi2.t_detailpenjualanobatranap AS detail ON jual.noPenjualanObatRanap = detail.noPenjualanObatRanap
		                            INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
	                          WHERE reg.noDaftar = '" & noDaftar & "' AND
		                            obat.kdKelompokObat IN ('KO03','KO04')
                            ) AS ranap,
                            (SELECT COALESCE(SUM(detail.harga * detail.diberikanKronis),0) AS totalKronisOK,
	                                COALESCE(SUM(detail.harga * detail.diberikanNonKronis),0) AS totalNonKronisOK
                               FROM simrs.t_registrasi AS reg
	                                INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
	                                INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
	                                INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
                              WHERE reg.noDaftar = '" & noDaftar & "' AND
	                                obat.kdKelompokObat IN ('KO03','KO04')
                            ) AS okranap"
            End Select
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT (totalKronisRI+totalKronisOK) AS totalKronis,
                            (totalNonKronisRI+totalNonKronisOK) AS totalNonKronis
                       FROM (SELECT COALESCE(SUM(detail.harga * detail.diberikanKronis),0) AS totalKronisRI,
		                            COALESCE(SUM(detail.harga * detail.diberikanNonKronis),0) AS totalNonKronisRI
	                           FROM simrs.t_registrasi AS reg
		                            INNER JOIN farmasi2.t_penjualanobatranap AS jual ON reg.noDaftar = jual.noDaftar
		                            INNER JOIN farmasi2.t_detailpenjualanobatranap AS detail ON jual.noPenjualanObatRanap = detail.noPenjualanObatRanap
		                            INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
	                          WHERE reg.noDaftar = '" & noDaftar & "' AND
		                            obat.kdKelompokObat IN ('KO03','KO04')
                            ) AS ranap,
                            (SELECT COALESCE(SUM(detail.harga * detail.diberikanKronis),0) AS totalKronisOK,
	                                COALESCE(SUM(detail.harga * detail.diberikanNonKronis),0) AS totalNonKronisOK
                               FROM simrs.t_registrasi AS reg
	                                INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
	                                INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
	                                INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
                              WHERE reg.noDaftar = '" & noDaftar & "' AND
	                                obat.kdKelompokObat IN ('KO03','KO04')
                            ) AS okranap"
        End If
        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            value1 = dr.Item("totalKronis").ToString
            value2 = dr.Item("totalNonKronis").ToString
            'ElseIf dr.IsDBNull(0) Then
            '    value1 = "0"
            'ElseIf dr.IsDBNull(1) Then
            '    value2 = "0"
        End If
        conn.Close()
        Return (value1, value2)
    End Function

    Sub detailObatNonKronis()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        If txtRawat.Text.Contains("Rawat Jalan") And txtObat.Text = "0" Then
            Return
        ElseIf txtRawat.Text.Contains("Rawat Jalan") And txtObat.Text <> "0" Then
            query = "SELECT jual.tglPenjualanObatRajal AS tglJual,
                            detail.namaObat,
                            detail.harga,
                            detail.diberikanNonKronis,
                            (detail.harga * detail.diberikanNonKronis) AS total,
                            ppa.namapetugasMedis
	                   FROM
		                    simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatrajal AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatrajal AS detail ON jual.noPenjualanObatRajal = detail.noPenjualanObatRajal
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
                            INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
	                  WHERE 
		                    reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO03','KO04')
                      UNION ALL
                     SELECT jual.tglPenjualanObatOK AS tglJual,
				            detail.namaObat,
				            detail.harga,
				            detail.diberikanKronis,
				            (detail.harga * detail.diberikanKronis) AS total,
				            ppa.namapetugasMedis
                       FROM simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
		                    INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
                      WHERE reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO03','KO04')"
        ElseIf txtRawat.Text.Contains("Igd") And txtObat.Text = "0" Then
            Return
        ElseIf txtRawat.Text.Contains("Igd") And txtObat.Text <> "0" Then
            Select Case txtUnit.Text
                Case "IGD"
                    query = "SELECT jual.tglPenjualanObatIGD AS tglJual,
				            detail.namaObat,
				            detail.harga,
				            detail.diberikanKronis,
				            (detail.harga * detail.diberikanKronis) AS total,
				            ppa.namapetugasMedis
                       FROM
		                    simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatigd AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatigd AS detail ON jual.noPenjualanObatIGD = detail.noPenjualanObatIGD
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
		                    INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
                      WHERE 
		                    reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO03','KO04')
                      UNION ALL
                     SELECT jual.tglPenjualanObatOK AS tglJual,
				            detail.namaObat,
				            detail.harga,
				            detail.diberikanKronis,
				            (detail.harga * detail.diberikanKronis) AS total,
				            ppa.namapetugasMedis
                       FROM simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
		                    INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
                      WHERE reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO03','KO04')"
                Case "IGD PINERE"
                    query = "SELECT jual.tglPenjualanObatRanap AS tglJual,
                            detail.namaObat,
                            detail.harga,
                            detail.diberikanNonKronis,
                            (detail.harga * detail.diberikanNonKronis) AS total,
                            ppa.namapetugasMedis
	                   FROM
		                    simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatranap AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatranap AS detail ON jual.noPenjualanObatRanap = detail.noPenjualanObatRanap
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
                            INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
	                  WHERE 
		                    reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO03','KO04')
                      UNION ALL
                     SELECT jual.tglPenjualanObatOK AS tglJual,
				            detail.namaObat,
				            detail.harga,
				            detail.diberikanKronis,
				            (detail.harga * detail.diberikanKronis) AS total,
				            ppa.namapetugasMedis
                       FROM simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
		                    INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
                      WHERE reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO03','KO04')"
            End Select
        ElseIf txtRawat.Text.Contains("Rawat Inap") And txtObat.Text = "0" Then
            Return
        ElseIf txtRawat.Text.Contains("Rawat Inap") And txtObat.Text <> "0" Then
            query = "SELECT jual.tglPenjualanObatRanap AS tglJual,
                            detail.namaObat,
                            detail.harga,
                            detail.diberikanNonKronis,
                            (detail.harga * detail.diberikanNonKronis) AS total,
                            ppa.namapetugasMedis
	                   FROM
		                    simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatranap AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatranap AS detail ON jual.noPenjualanObatRanap = detail.noPenjualanObatRanap
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
                            INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
	                  WHERE 
		                    reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO03','KO04')
                      UNION ALL
                     SELECT jual.tglPenjualanObatOK AS tglJual,
				            detail.namaObat,
				            detail.harga,
				            detail.diberikanKronis,
				            (detail.harga * detail.diberikanKronis) AS total,
				            ppa.namapetugasMedis
                       FROM simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
		                    INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
                      WHERE reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO03','KO04')"
        End If

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvDetail.Rows.Clear()

            If txtRawat.Text.Contains("Rawat Jalan") And txtObat.Text = "0" Then
                Return
            ElseIf txtRawat.Text.Contains("Rawat Jalan") And txtObat.Text <> "0" Then
                Do While dr.Read
                    dgvDetail.Rows.Add(dr.Item("tglJual"), dr.Item("namaObat"), dr.Item("harga"),
                                       dr.Item("diberikanNonKronis"), dr.Item("total"), dr.Item("namapetugasMedis"))
                Loop
            ElseIf (txtRawat.Text.Contains("Rawat Inap") Or txtRawat.Text.Contains("Igd")) And txtObat.Text = "0" Then
                Return
            ElseIf (txtRawat.Text.Contains("Rawat Inap") Or txtRawat.Text.Contains("Igd")) And txtObat.Text <> "0" Then
                Do While dr.Read
                    dgvDetail.Rows.Add(dr.Item("tglJual"), dr.Item("namaObat"), dr.Item("harga"),
                                       dr.Item("diberikanNonKronis"), dr.Item("total"), dr.Item("namapetugasMedis"))
                Loop
            End If

            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail Non Kronis", MsgBoxStyle.Exclamation)
        End Try
        conn.Close()

        Dim totTarif As Long
        totTarif = 0
        For i As Integer = 0 To dgvDetail.Rows.Count - 1
            totTarif = totTarif + Val(dgvDetail.Rows(i).Cells(4).Value)
        Next
        txtTotalDetail.Text = (Math.Ceiling(totTarif / 100) * 100).ToString("#,##0")
    End Sub

    Sub detailObatKronis()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        If txtRawat.Text.Contains("Rawat Jalan") And txtObatKronis.Text = "0" Then
            Return
        ElseIf txtRawat.Text.Contains("Rawat Jalan") And txtObatKronis.Text <> "0" Then
            query = "SELECT jual.tglPenjualanObatRajal AS tglJual,
                            detail.namaObat,
                            detail.harga,
                            detail.diberikanKronis,
                            (detail.harga * detail.diberikanKronis) AS total,
                            ppa.namapetugasMedis
	                   FROM simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatrajal AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatrajal AS detail ON jual.noPenjualanObatRajal = detail.noPenjualanObatRajal
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
                            INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
	                  WHERE reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO03','KO04')
                      UNION ALL
                     SELECT jual.tglPenjualanObatOK AS tglJual,
				            detail.namaObat,
				            detail.harga,
				            detail.diberikanKronis,
				            (detail.harga * detail.diberikanKronis) AS total,
				            ppa.namapetugasMedis
                       FROM simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
		                    INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
                      WHERE reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO03','KO04')"
        ElseIf txtRawat.Text.Contains("Igd") And txtObatKronis.Text = "0" Then
            Return
        ElseIf txtRawat.Text.Contains("Igd") And txtObatKronis.Text <> "0" Then
            Select Case txtUnit.Text
                Case "IGD"
                    query = "SELECT jual.tglPenjualanObatIGD AS tglJual,
				            detail.namaObat,
				            detail.harga,
				            detail.diberikanKronis,
				            (detail.harga * detail.diberikanKronis) AS total,
				            ppa.namapetugasMedis
                       FROM
		                    simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatigd AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatigd AS detail ON jual.noPenjualanObatIGD = detail.noPenjualanObatIGD
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
		                    INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
                      WHERE 
		                    reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO03','KO04')
                      UNION ALL
                     SELECT jual.tglPenjualanObatOK AS tglJual,
				            detail.namaObat,
				            detail.harga,
				            detail.diberikanKronis,
				            (detail.harga * detail.diberikanKronis) AS total,
				            ppa.namapetugasMedis
                       FROM simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
		                    INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
                      WHERE reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO03','KO04')"
                Case "IGD PINERE"
            End Select
            query = "SELECT jual.tglPenjualanObatRanap AS tglJual,
                            detail.namaObat,
                            detail.harga,
                            detail.diberikanKronis,
                            (detail.harga * detail.diberikanKronis) AS total,
                            ppa.namapetugasMedis
	                   FROM
		                    simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatranap AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatranap AS detail ON jual.noPenjualanObatRanap = detail.noPenjualanObatRanap
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
                            INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
	                  WHERE 
		                    reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO03','KO04')
                      UNION ALL
                     SELECT jual.tglPenjualanObatOK AS tglJual,
				            detail.namaObat,
				            detail.harga,
				            detail.diberikanKronis,
				            (detail.harga * detail.diberikanKronis) AS total,
				            ppa.namapetugasMedis
                       FROM simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
		                    INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
                      WHERE reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO03','KO04')"
        ElseIf txtRawat.Text.Contains("Rawat Inap") And txtObatKronis.Text = "0" Then
            Return
        ElseIf txtRawat.Text.Contains("Rawat Inap") And txtObatKronis.Text <> "0" Then
            query = "SELECT jual.tglPenjualanObatRanap AS tglJual,
                            detail.namaObat,
                            detail.harga,
                            detail.diberikanKronis,
                            (detail.harga * detail.diberikanKronis) AS total,
                            ppa.namapetugasMedis
	                   FROM
		                    simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatranap AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatranap AS detail ON jual.noPenjualanObatRanap = detail.noPenjualanObatRanap
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
                            INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
	                  WHERE 
		                    reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO03','KO04')
                      UNION ALL
                     SELECT jual.tglPenjualanObatOK AS tglJual,
				            detail.namaObat,
				            detail.harga,
				            detail.diberikanKronis,
				            (detail.harga * detail.diberikanKronis) AS total,
				            ppa.namapetugasMedis
                       FROM simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
		                    INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
                      WHERE reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO03','KO04')"
        End If

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvDetail.Rows.Clear()

            If txtRawat.Text.Contains("Rawat Jalan") And txtObatKronis.Text = "0" Then
                Return
            ElseIf txtRawat.Text.Contains("Rawat Jalan") And txtObatKronis.Text <> "0" Then
                Do While dr.Read
                    dgvDetail.Rows.Add(dr.Item("tglJual"), dr.Item("namaObat"), dr.Item("harga"),
                                       dr.Item("diberikanKronis"), dr.Item("total"), dr.Item("namapetugasMedis"))
                Loop
            ElseIf (txtRawat.Text.Contains("Rawat Inap") Or txtRawat.Text.Contains("Igd")) And txtObatKronis.Text = "0" Then
                Return
            ElseIf (txtRawat.Text.Contains("Rawat Inap") Or txtRawat.Text.Contains("Igd")) And txtObatKronis.Text <> "0" Then
                Do While dr.Read
                    dgvDetail.Rows.Add(dr.Item("tglJual"), dr.Item("namaObat"), dr.Item("harga"),
                                       dr.Item("diberikanKronis"), dr.Item("total"), dr.Item("namapetugasMedis"))
                Loop
            End If

            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail Kronis", MsgBoxStyle.Exclamation)
        End Try
        conn.Close()

        Dim totTarif As Long
        totTarif = 0
        For i As Integer = 0 To dgvDetail.Rows.Count - 1
            totTarif = totTarif + Val(dgvDetail.Rows(i).Cells(4).Value)
        Next
        txtTotalDetail.Text = (Math.Ceiling(totTarif / 100) * 100).ToString("#,##0")
    End Sub
#End Region
#Region "Alkes"
    Function tampilAlkes() As String
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value As String = ""

        If txtRawat.Text.Contains("Rawat Jalan") Then
            query = "SELECT (Alkes1RJ+Alkes2RJ+Alkes1OK+Alkes2OK) AS Alkes
                       FROM (SELECT COALESCE(SUM(detail.harga * detail.diberikanKronis),0) AS Alkes1RJ,
                                    COALESCE(SUM(detail.harga * detail.diberikanNonKronis),0) AS Alkes2RJ
	                           FROM simrs.t_registrasi AS reg
		                            INNER JOIN farmasi2.t_penjualanobatrajal AS jual ON reg.noDaftar = jual.noDaftar
		                            INNER JOIN farmasi2.t_detailpenjualanobatrajal AS detail ON jual.noPenjualanObatRajal = detail.noPenjualanObatRajal
		                            INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
	                          WHERE reg.noDaftar = '" & noDaftar & "' AND
		                            obat.kdKelompokObat IN ('KO01')
                            ) AS alkesrajal,
                            (SELECT COALESCE(SUM(detail.harga * detail.diberikanKronis),0) AS Alkes1OK,
		                            COALESCE(SUM(detail.harga * detail.diberikanNonKronis),0) AS Alkes2OK
	                           FROM simrs.t_registrasi AS reg
		                            INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
		                            INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
		                            INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
	                          WHERE reg.noDaftar = '" & noDaftar & "' AND
		                            obat.kdKelompokObat IN ('KO01')	
                            ) AS alkesok"
        ElseIf txtRawat.Text.Contains("Igd") Then
            Select Case txtUnit.Text
                Case "IGD"
                    query = "SELECT (Alkes1IGD+Alkes2IGD+Alkes1OK+Alkes2OK) AS Alkes
                       FROM (SELECT COALESCE(SUM(detail.harga * detail.diberikanKronis),0) AS Alkes1IGD,
                                    COALESCE(SUM(detail.harga * detail.diberikanNonKronis),0) AS Alkes2IGD
	                           FROM simrs.t_registrasi AS reg
		                            INNER JOIN farmasi2.t_penjualanobatigd AS jual ON reg.noDaftar = jual.noDaftar
	                                INNER JOIN farmasi2.t_detailpenjualanobatigd AS detail ON jual.noPenjualanObatIGD = detail.noPenjualanObatIGD
	                                INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
	                          WHERE reg.noDaftar = '" & noDaftar & "' AND
		                            obat.kdKelompokObat IN ('KO01')
                            ) AS alkesigd,
                            (SELECT COALESCE(SUM(detail.harga * detail.diberikanKronis),0) AS Alkes1OK,
		                            COALESCE(SUM(detail.harga * detail.diberikanNonKronis),0) AS Alkes2OK
	                           FROM simrs.t_registrasi AS reg
		                            INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
		                            INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
		                            INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
	                          WHERE reg.noDaftar = '" & noDaftar & "' AND
		                            obat.kdKelompokObat IN ('KO01')	
                            ) AS alkesok"
                Case "IGD PINERE"
                    query = "SELECT (Alkes1RI+Alkes2RI+Alkes1OK+Alkes2OK) AS Alkes
                       FROM (SELECT COALESCE(SUM(detail.harga * detail.diberikanKronis),0) AS Alkes1RI,
                                    COALESCE(SUM(detail.harga * detail.diberikanNonKronis),0) AS Alkes2RI
	                           FROM simrs.t_registrasi AS reg
		                            INNER JOIN farmasi2.t_penjualanobatranap AS jual ON reg.noDaftar = jual.noDaftar
		                            INNER JOIN farmasi2.t_detailpenjualanobatranap AS detail ON jual.noPenjualanObatRanap = detail.noPenjualanObatRanap
		                            INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
	                          WHERE reg.noDaftar = '" & noDaftar & "' AND
		                            obat.kdKelompokObat IN ('KO01')
                            ) AS alkesranap,
                            (SELECT COALESCE(SUM(detail.harga * detail.diberikanKronis),0) AS Alkes1OK,
		                            COALESCE(SUM(detail.harga * detail.diberikanNonKronis),0) AS Alkes2OK
	                           FROM simrs.t_registrasi AS reg
		                            INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
		                            INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
		                            INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
	                          WHERE reg.noDaftar = '" & noDaftar & "' AND
		                            obat.kdKelompokObat IN ('KO01')	
                            ) AS alkesok"
            End Select

        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT (Alkes1RI+Alkes2RI+Alkes1OK+Alkes2OK) AS Alkes
                       FROM (SELECT COALESCE(SUM(detail.harga * detail.diberikanKronis),0) AS Alkes1RI,
                                    COALESCE(SUM(detail.harga * detail.diberikanNonKronis),0) AS Alkes2RI
	                           FROM simrs.t_registrasi AS reg
		                            INNER JOIN farmasi2.t_penjualanobatranap AS jual ON reg.noDaftar = jual.noDaftar
		                            INNER JOIN farmasi2.t_detailpenjualanobatranap AS detail ON jual.noPenjualanObatRanap = detail.noPenjualanObatRanap
		                            INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
	                          WHERE reg.noDaftar = '" & noDaftar & "' AND
		                            obat.kdKelompokObat IN ('KO01')
                            ) AS alkesranap,
                            (SELECT COALESCE(SUM(detail.harga * detail.diberikanKronis),0) AS Alkes1OK,
		                            COALESCE(SUM(detail.harga * detail.diberikanNonKronis),0) AS Alkes2OK
	                           FROM simrs.t_registrasi AS reg
		                            INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
		                            INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
		                            INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
	                          WHERE reg.noDaftar = '" & noDaftar & "' AND
		                            obat.kdKelompokObat IN ('KO01')	
                            ) AS alkesok"
        End If
        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            value = dr.Item("Alkes").ToString
        End If
        conn.Close()
        Return value
    End Function

    Sub detailAlkes()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        If txtRawat.Text.Contains("Rawat Jalan") Then
            query = "SELECT jual.tglPenjualanObatRajal AS tglJual,
                            detail.namaObat,
                            detail.harga,
                            (detail.diberikanKronis + detail.diberikanNonKronis) AS jml,
				            ((detail.harga * detail.diberikanKronis) + (detail.harga * detail.diberikanNonKronis))  AS total,
                            ppa.namapetugasMedis
	                   FROM simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatrajal AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatrajal AS detail ON jual.noPenjualanObatRajal = detail.noPenjualanObatRajal
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
                            INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
	                  WHERE reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO01')
                      UNION ALL
                     SELECT jual.tglPenjualanObatOK AS tglJual,
				            detail.namaObat,
				            detail.harga,
				            (detail.diberikanKronis + detail.diberikanNonKronis) AS jml,
				            ((detail.harga * detail.diberikanKronis) + (detail.harga * detail.diberikanNonKronis)) AS total,
				            ppa.namapetugasMedis
                       FROM simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
		                    INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
                      WHERE reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO01')"
        ElseIf txtRawat.Text.Contains("Igd") Then
            Select Case txtUnit.Text
                Case "IGD"
                    query = "SELECT jual.tglPenjualanObatIGD AS tglJual,
				            detail.namaObat,
				            detail.harga,
				            (detail.diberikanKronis + detail.diberikanNonKronis) AS jml,
				            ((detail.harga * detail.diberikanKronis) + (detail.harga * detail.diberikanNonKronis)) AS total,
				            ppa.namapetugasMedis
                       FROM simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatigd AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatigd AS detail ON jual.noPenjualanObatIGD = detail.noPenjualanObatIGD
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
		                    INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
                      WHERE reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO01')
                      UNION ALL
                     SELECT jual.tglPenjualanObatOK AS tglJual,
				            detail.namaObat,
				            detail.harga,
				            (detail.diberikanKronis + detail.diberikanNonKronis) AS jml,
				            ((detail.harga * detail.diberikanKronis) + (detail.harga * detail.diberikanNonKronis))  AS total,
				            ppa.namapetugasMedis
                       FROM simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
		                    INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
                      WHERE reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO01')"
                Case "IGD PINERE"
                    query = "SELECT jual.tglPenjualanObatRanap AS tglJual,
                            detail.namaObat,
                            detail.harga,
                            (detail.diberikanKronis + detail.diberikanNonKronis) AS jml,
				            ((detail.harga * detail.diberikanKronis) + (detail.harga * detail.diberikanNonKronis)) AS total,
                            ppa.namapetugasMedis
	                   FROM simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatranap AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatranap AS detail ON jual.noPenjualanObatRanap = detail.noPenjualanObatRanap
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
                            INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
	                  WHERE reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO01')
                      UNION ALL
                     SELECT jual.tglPenjualanObatOK AS tglJual,
				            detail.namaObat,
				            detail.harga,
				            (detail.diberikanKronis + detail.diberikanNonKronis) AS jml,
				            ((detail.harga * detail.diberikanKronis) + (detail.harga * detail.diberikanNonKronis)) AS total,
				            ppa.namapetugasMedis
                       FROM simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
		                    INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
                      WHERE reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO01')"
            End Select
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT jual.tglPenjualanObatRanap AS tglJual,
                            detail.namaObat,
                            detail.harga,
                            (detail.diberikanKronis + detail.diberikanNonKronis) AS jml,
				            ((detail.harga * detail.diberikanKronis) + (detail.harga * detail.diberikanNonKronis)) AS total,
                            ppa.namapetugasMedis
	                   FROM simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatranap AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatranap AS detail ON jual.noPenjualanObatRanap = detail.noPenjualanObatRanap
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
                            INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
	                  WHERE reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO01')
                      UNION ALL
                     SELECT jual.tglPenjualanObatOK AS tglJual,
				            detail.namaObat,
				            detail.harga,
				            (detail.diberikanKronis + detail.diberikanNonKronis) AS jml,
				            ((detail.harga * detail.diberikanKronis) + (detail.harga * detail.diberikanNonKronis)) AS total,
				            ppa.namapetugasMedis
                       FROM simrs.t_registrasi AS reg
		                    INNER JOIN farmasi2.t_penjualanobatok AS jual ON reg.noDaftar = jual.noDaftar
		                    INNER JOIN farmasi2.t_detailpenjualanobatok AS detail ON jual.noPenjualanObatOK = detail.noPenjualanObatOK
		                    INNER JOIN farmasi2.t_obat AS obat ON detail.kdObat = obat.kdObat
		                    INNER JOIN simrs.t_tenagamedis2 AS ppa ON jual.dokterPemberiResep = ppa.kdPetugasMedis
                      WHERE reg.noDaftar = '" & noDaftar & "' AND
		                    obat.kdKelompokObat IN ('KO01')"
        End If

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvDetail.Rows.Clear()

            Do While dr.Read
                dgvDetail.Rows.Add(dr.Item("tglJual"), dr.Item("namaObat"), dr.Item("harga"),
                                   dr.Item("jml"), dr.Item("total"), dr.Item("namapetugasMedis"))
            Loop

            dr.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Detail Alkes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        conn.Close()

        Dim totTarif As Long
        totTarif = 0
        For i As Integer = 0 To dgvDetail.Rows.Count - 1
            totTarif = totTarif + Val(dgvDetail.Rows(i).Cells(4).Value)
        Next
        txtTotalDetail.Text = (Math.Ceiling(totTarif / 100) * 100).ToString("#,##0")
    End Sub
#End Region
#Region "BMHP"
    Function tampilBMHP() As String
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value As String = ""

        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT
	                        COALESCE(SUM(detail.totalTarif),0) AS bmhp
                        FROM
	                        t_tindakanpasienrajal AS trans
	                        INNER JOIN t_detailtindakanpasienrajal AS detail ON trans.noTindakanPasienRajal = detail.noTindakanPasienRajal
	                        INNER JOIN t_registrasirawatjalan AS regrj ON trans.noRegistrasiRawatJalan = regrj.noRegistrasiRawatJalan
	                        INNER JOIN t_registrasi AS reg ON reg.noDaftar = regrj.noDaftar
	                        INNER JOIN t_tariftindakan2 AS tarif ON detail.kdTarif = tarif.kdTarif
	                        INNER JOIN t_tindakan2 AS kel ON tarif.kdTindakan = kel.kdTindakan
                        WHERE 
	                        reg.noDaftar = '" & noDaftar & "' AND 
	                        (kel.kdKelompokTindakan IN (62, 63) OR
                            kel.tindakan LIKE 'OKSIGEN%')"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT
	                        COALESCE(SUM(detail.totalTarif),0) AS bmhp
                        FROM
	                        t_tindakanpasienranap AS trans
	                        INNER JOIN t_detailtindakanpasienranap AS detail ON trans.noTindakanPasienRanap = detail.noTindakanPasienRanap
	                        INNER JOIN t_registrasirawatinap AS regri ON trans.noDaftarRawatInap = regri.noDaftarRawatInap
	                        INNER JOIN t_registrasi AS reg ON reg.noDaftar = regri.noDaftar
	                        INNER JOIN t_tariftindakan2 AS tarif ON detail.kdTarif = tarif.kdTarif
	                        INNER JOIN t_tindakan2 AS kel ON tarif.kdTindakan = kel.kdTindakan
                        WHERE 
	                        reg.noDaftar = '" & noDaftar & "' AND 
	                        (kel.kdKelompokTindakan IN (62, 63) OR
                            kel.tindakan LIKE 'OKSIGEN%')"
        End If
        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            value = dr.Item("bmhp").ToString
        End If
        conn.Close()
        Return value
    End Function

    Sub detailBmhp()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT
                            trans.tglTindakan,
                            detail.tindakan,
                            detail.tarif,
                            detail.jumlahTindakan,
                            detail.totalTarif,
                            ppa.namapetugasMedis
                       FROM t_tindakanpasienrajal AS trans
                            INNER JOIN t_detailtindakanpasienrajal AS detail ON trans.noTindakanPasienRajal = detail.noTindakanPasienRajal
                            INNER JOIN t_registrasirawatjalan AS regrj ON trans.noRegistrasiRawatJalan = regrj.noRegistrasiRawatJalan
                            INNER JOIN t_registrasi AS reg ON reg.noDaftar = regrj.noDaftar
                            INNER JOIN t_tariftindakan2 AS tarif ON detail.kdTarif = tarif.kdTarif
                            INNER JOIN t_tindakan2 AS kel ON tarif.kdTindakan = kel.kdTindakan
                            INNER JOIN t_tenagamedis2 AS ppa ON detail.kdTenagaMedis = ppa.kdPetugasMedis
                      WHERE reg.noDaftar = '" & noDaftar & "' AND 
                            (kel.kdKelompokTindakan IN (62, 63) OR
                            kel.tindakan LIKE 'OKSIGEN%')"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT
                            trans.tglTindakan,
                            detail.tindakan,
                            detail.tarif,
                            detail.jumlahTindakan,
                            detail.totalTarif,
                            ppa.namapetugasMedis
                       FROM t_tindakanpasienranap AS trans
                            INNER JOIN t_detailtindakanpasienranap AS detail ON trans.noTindakanPasienRanap = detail.noTindakanPasienRanap
                            INNER JOIN t_registrasirawatinap AS regri ON trans.noDaftarRawatInap = regri.noDaftarRawatInap
                            INNER JOIN t_registrasi AS reg ON reg.noDaftar = regri.noDaftar
                            INNER JOIN t_tariftindakan2 AS tarif ON detail.kdTarif = tarif.kdTarif
                            INNER JOIN t_tindakan2 AS kel ON tarif.kdTindakan = kel.kdTindakan
                            INNER JOIN t_tenagamedis2 AS ppa ON detail.kdTenagaMedis = ppa.kdPetugasMedis
                      WHERE reg.noDaftar = '" & noDaftar & "' AND 
                            (kel.kdKelompokTindakan IN (62, 63) OR
                            kel.tindakan LIKE 'OKSIGEN%')"
        End If

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvDetail.Rows.Clear()

            Do While dr.Read
                dgvDetail.Rows.Add(dr.Item("tglTindakan"), dr.Item("tindakan"), dr.Item("tarif"),
                                   dr.Item("jumlahTindakan"), dr.Item("totalTarif"), dr.Item("namapetugasMedis"))
            Loop

            dr.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Detail BMHP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        conn.Close()

        Dim totTarif As Long
        totTarif = 0
        For i As Integer = 0 To dgvDetail.Rows.Count - 1
            totTarif = totTarif + Val(dgvDetail.Rows(i).Cells(4).Value)
        Next
        txtTotalDetail.Text = (Math.Ceiling(totTarif / 100) * 100).ToString("#,##0")
    End Sub
#End Region
#Region "Sewa Alat"
    Function tampilSewa() As String
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value As String = ""
        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT COALESCE(SUM(totalTarif),0) AS tindakan
                       FROM vw_tindakanpasienrajaldetail 
                      WHERE noDaftar = '" & noDaftar & "' 
                        AND (tindakan LIKE '%SEWA VENTILATOR%'
                         OR tindakan LIKE '%SEWA SYRINGE%'
                         OR tindakan LIKE '%NEBUL%'
                         OR kdTarif IN (480710,490810,552501))"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT COALESCE(SUM(totalTarif),0) AS tindakan
                       FROM vw_tindakanpasienranapdetail 
                      WHERE noDaftar = '" & noDaftar & "' 
                        AND (tindakan LIKE '%SEWA VENTILATOR%'
                         OR tindakan LIKE '%SEWA SYRINGE%'
                         OR tindakan LIKE '%NEBUL%'
                         OR kdTarif IN (480710,490810,552501))"
        End If

        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            value = dr.Item("tindakan").ToString
        End If
        conn.Close()
        Return value
    End Function

    Sub detailSewa()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            query = "SELECT tglTindakan,tindakan, tarif, 
                            jumlahTindakan, totalTarif, PPA
                       FROM vw_tindakanpasienrajaldetail 
                      WHERE noDaftar = '" & noDaftar & "' 
                        AND (tindakan LIKE '%SEWA VENTILATOR%'
                         OR tindakan LIKE '%SEWA SYRINGE%'
                         OR tindakan LIKE '%NEBUL%'
                         OR kdTarif IN (480710,490810,552501))"
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            query = "SELECT tglTindakan,tindakan, tarif, 
                            jumlahTindakan, totalTarif, PPA
                       FROM vw_tindakanpasienranapdetail 
                      WHERE noDaftar = '" & noDaftar & "' 
                        AND (tindakan LIKE '%SEWA VENTILATOR%'
                         OR tindakan LIKE '%SEWA SYRINGE%'
                         OR tindakan LIKE '%NEBUL%'
                         OR kdTarif IN (480710,490810,552501))"
        End If

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvDetail.Rows.Clear()
            Do While dr.Read
                dgvDetail.Rows.Add(dr.Item("tglTindakan"), dr.Item("tindakan"), dr.Item("tarif"),
                                   dr.Item("jumlahTindakan"), dr.Item("totalTarif"), dr.Item("PPA"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail Keperawatan", MsgBoxStyle.Exclamation)
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
						  WHERE kdTarif LIKE '0209%' AND kelas = '" & txtKelas.Text & "') AS tind,
						(SELECT reg.tglDaftar AS mrs, dpjp.namapetugasMedis AS DPJP
						   FROM t_registrasi AS reg
				          INNER JOIN t_tenagamedis2 AS dpjp ON reg.kdTenagaMedis = dpjp.kdPetugasMedis
					      WHERE reg.noDaftar = '" & noDaftar & "') AS dokter"

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                txtDpjp = dr.Item("DPJP")
                txtTotDpjp = dr.Item("total")
            End If
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, "Detail JP DPJP", MsgBoxStyle.Exclamation)
        End Try
        conn.Close()
    End Sub
#End Region
    Sub autoNoId()
        Dim noEklaim As String

        Try
            Call koneksiServer()
            Dim query As String
            query = "SELECT SUBSTR(noEklaim,16) FROM t_eklaimpasien ORDER BY CAST(SUBSTR(noEklaim,16) AS UNSIGNED) DESC LIMIT 1"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Read()
                noEklaim = "EK" + Format(Now, "ddMMyyHHmmss") + "-" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                txtNoEklaimBaru.Text = noEklaim
            Else
                noEklaim = "EK" + Format(Now, "ddMMyyHHmmss") + "-1"
                txtNoEklaimBaru.Text = noEklaim
            End If
            dr.Close()
            conn.Close()
        Catch ex As Exception

        End Try

    End Sub

    Sub addPasien()
        Dim dt As String
        dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

        conn.Close()
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmd As MySqlCommand
            str = "INSERT INTO t_eklaimpasien(noEklaim,noDaftar,noRekamMedis,namaPasien,
                                             dpjp,jaminan,unit,noPeserta,
                                             noSEP,asuransi,tipe,tglMasuk,
                                             tglPulang,los,caraPulang,statusKlaim,
                                             dateFinal,dateModified,petugas) 
                   VALUES ('" & txtNoEklaimBaru.Text & "','" & noDaftar & "','" & txtNoRM.Text & "','" & txtNamaPasien.Text & "',
                           '" & txtDokter.Text & "','" & txtJaminan.Text & "','" & txtUnit.Text & "','" & txtSetBpjs.Text & "',
                           '" & txtSetSep.Text & "','" & txtSetAsuransi.Text & "','" & txtRawat.Text & "','" & Format(CDate(txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss") & "',
                           '" & Format(CDate(txtTglKlrRawat.Text), "yyyy-MM-dd HH:mm:ss") & "','" & txtLos.Text & "','" & txtCaraPulang.Text & "','" & statusFinal & "',
                           '" & Format(CDate(dateFinal), "yyyy-MM-dd HH:mm:ss") & "','" & dt & "','" & txtUser.Text & "')"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MsgBox("Insert data E-klaim berhasil dilakukan", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Insert data E-klaim gagal dilakukan.", MsgBoxStyle.Critical)
        End Try
        conn.Close()
    End Sub

    Sub addDetail()
        Dim dt As String
        dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

        conn.Close()
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmd As MySqlCommand
            str = "INSERT INTO t_eklaimdetailtarif(noEklaim,nonBedah,bedah,konsultasi,tenagaAhli,
                                                   keperawatan,penunjang,radiologi,laboratorium,bdrs,
                                                   rehab,akomodasi,intensif,obat,obatKronis,
                                                   obatKemo,alkes,bmhp,sewa,totalTarif,statusKlaim) 
                   VALUES ('" & txtNoEklaimBaru.Text & "','" & Convert.ToDouble(txtNonBedah.Text, ci) & "','" & Convert.ToDouble(txtBedah.Text, ci) & "','" & Convert.ToDouble(txtKonsul.Text, ci) & "','" & Convert.ToDouble(txtPPA.Text, ci) & "',
      '" & Convert.ToDouble(txtKeperawatan.Text, ci) & "','" & Convert.ToDouble(txtPenunjang.Text, ci) & "','" & Convert.ToDouble(txtRadiologi.Text, ci) & "','" & Convert.ToDouble(txtLab.Text, ci) & "','" & Convert.ToDouble(txtDarah.Text, ci) & "',
            '" & Convert.ToDouble(txtRehab.Text, ci) & "','" & Convert.ToDouble(txtAkomodasi.Text, ci) & "','" & Convert.ToDouble(txtIntensif.Text, ci) & "','" & Convert.ToDouble(txtObat.Text, ci) & "','" & Convert.ToDouble(txtObatKronis.Text, ci) & "',
         '" & Convert.ToDouble(txtObatKemo.Text, ci) & "','" & Convert.ToDouble(txtAlkes.Text, ci) & "','" & Convert.ToDouble(txtBMHP.Text, ci) & "','" & Convert.ToDouble(txtSewaAlat.Text, ci) & "','" & Convert.ToDouble(txtTotalTarif.Text, ci) & "',
                                    '" & statusFinal & "')"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MsgBox("Insert detail E-klaim berhasil dilakukan", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Insert detail E-klaim gagal dilakukan.", MsgBoxStyle.Critical)
        End Try
        conn.Close()
    End Sub

    Sub cariDiagnosa(kode As String)
        Dim payload As String = ""
        Dim jsonQuery As String = ""
        Dim req As String = ""
        Dim response As String
        jsonQuery = "{""metadata"": {
                        ""method"": ""search_diagnosis""
                        },
                        ""data"": {
                        ""keyword"": """ & kode & """
                        }
                     }"
        payload = inacbg_encrypt(jsonQuery, EncrypKey)
        req = reqPost(payload)
        response = inacbg_decrypt(req, EncrypKey)
        MsgBox(response)
    End Sub

    Sub rincianBiaya()
        'Rincian Biaya
        txtNonBedah.Text = CInt(tampilNonBedah()).ToString("#,##0")
        txtBedah.Text = CInt(tampilBedah()).ToString("#,##0")
        txtPPA.Text = CInt(tampilJasa()).ToString("#,##0")
        txtKeperawatan.Text = CInt(tampilTindakan()).ToString("#,##0")
        txtPenunjang.Text = CInt(tampilPenunjang()).ToString("#,##0")
        txtLab.Text = CInt(tampilLab()).ToString("#,##0")
        txtRadiologi.Text = CInt(tampilRad()).ToString("#,##0")
        txtDarah.Text = CInt(tampilDarah()).ToString("#,##0")
        txtRehab.Text = CInt(tampilRehab()).ToString("#,##0")
        txtAkomodasi.Text = CInt(tampilAkomodasi()).ToString("#,##0")
        Dim nonkronis = tampilObat()
        Dim kronis = tampilObat()
        txtObat.Text = (Math.Ceiling(CInt(nonkronis.nonKronis) / 100) * 100).ToString("#,##0")
        txtObatKronis.Text = (Math.Ceiling(CInt(kronis.kronis) / 100) * 100).ToString("#,##0")
        txtAlkes.Text = (Math.Ceiling(CInt(tampilAlkes()) / 100) * 100).ToString("#,##0")
        txtBMHP.Text = (Math.Ceiling(CInt(tampilBMHP()) / 100) * 100).ToString("#,##0")
        txtIntensif.Text = CInt(tampilIntensif()).ToString("#,##0")
        txtSewaAlat.Text = CInt(tampilSewa()).ToString("#,##0")

        If instalasi.Equals("Rawat Inap", StringComparison.OrdinalIgnoreCase) And txtIntensif.Text = 0 Then
            If txtUnit.Text.Contains("Lavender") Or
               txtUnit.Text.Contains("Kemuning") Then
                txtKonsul.Text = CInt(tampilVisite()).ToString("#,##0")
            End If
            txtKonsul.Text = Val(CInt(tampilVisite()) + cekTarifDpjp()).ToString("#,##0")
        Else
            txtKonsul.Text = CInt(tampilVisite()).ToString("#,##0")
        End If

        'Total Biaya
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
    End Sub

    Private Sub Eklaim_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        txtUser.Text = Home.txtUser.Text
        btnEklaim.BackColor = Color.FromArgb(209, 232, 223)
        PicCollapse.Visible = False

        txtTglMskRawat.Format = DateTimePickerFormat.Custom
        txtTglKlrRawat.Format = DateTimePickerFormat.Custom
        txtTglMskRawat.CustomFormat = "dd MMM yyyy HH:mm:ss"
        txtTglKlrRawat.CustomFormat = "dd MMM yyyy HH:mm:ss"

        noDaftar = Form1.noDaftar
        Call tampilDokter(noDaftar)
        Call unSelect()
        Call autoDokter()
        Call autoJaminan()
        Call autoAsuransi()
        jmlRuang = cekJmlRuang(noDaftar)
        CheckedListBox1.SelectionMode = System.Windows.Forms.SelectionMode.None

        'Data Pasien
        txtTglMasuk.Text = Form1.tglMasuk
        txtTglKeluar.Text = Form1.tglKeluar
        txtNoRM.Text = Form1.noRm
        txtNamaPasien.Text = Form1.nmPasien
        txtJK.Text = Form1.jk
        txtTglLahir.Text = CDate(Form1.tglLahir).ToString("dd/MM/yyyy")
        txtJaminan.Text = Form1.penjamin
        txtUnit.Text = Form1.unit
        txtKelas.Text = Form1.kelas
        txtTglMskRawat.Value = Form1.tglMasuk
        txtTglKlrRawat.Value = If(Form1.tglKeluar <> "", Form1.tglKeluar, DateTime.Now)

        If txtJK.Text = "Laki-Laki" Then
            wsJk = "1"
        ElseIf txtJK.Text = "Perempuan" Then
            wsJk = "2"
        End If

        If instalasi.Equals("Rawat Jalan", StringComparison.OrdinalIgnoreCase) Then
            txtLos.Text = 1
            txtKelas.Text = "-"
            pnleksekutif.Visible = True
            pnlIntensif.Visible = False
        ElseIf instalasi.Equals("Rawat Inap", StringComparison.OrdinalIgnoreCase) Then
            txtLos.Text = Val(DateAndTime.DateDiff(DateInterval.Day, txtTglMskRawat.Value, txtTglKlrRawat.Value) + 1)
            pnleksekutif.Visible = False
            pnlIntensif.Visible = True
        End If

        txtDokter.Text = DPJP
        txtCaraPulang.Text = "-"
        txtRawat.Text = Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(instalasi)
        Dim dt As DateTime = Convert.ToDateTime(Form1.tglLahir)
        Dim cul As IFormatProvider = New System.Globalization.CultureInfo("id-ID", True)
        Dim dt1 As DateTime = DateTime.Parse(Form1.tglLahir, cul, System.Globalization.DateTimeStyles.AssumeLocal)
        txtUmur.Text = hitungUmur(dt1.ToShortDateString)

        'Cek Data Eklaim
        Dim idCek = cekDataKlaim(txtNoRM.Text, txtRawat.Text, Format(CDate(txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss"), Format(CDate(txtTglKlrRawat.Text), "yyyy-MM-dd HH:mm:ss"))
        Dim statCek = cekDataKlaim(txtNoRM.Text, txtRawat.Text, Format(CDate(txtTglMskRawat.Text), "yyyy-MM-dd HH:mm:ss"), Format(CDate(txtTglKlrRawat.Text), "yyyy-MM-dd HH:mm:ss"))
        txtNoEklaimLama.Text = idCek.idKlaim
        statusFinal = statCek.stKlaim

        If txtNoEklaimLama.Text = "" And statusFinal = "" Then
            Call autoNoId()
            CheckedListBox1.SetItemChecked(0, False)
            'MsgBox("Data Klaim Belum Ada")
            Call rincianBiaya()
        ElseIf txtNoEklaimLama.Text = "" And statusFinal = "-" Then
            Call autoNoId()
            CheckedListBox1.SetItemChecked(0, False)
            'MsgBox("Data Klaim Belum Ada(2)")
            Call rincianBiaya()
        ElseIf txtNoEklaimLama.Text <> "" And statusFinal = "Final" Then
            CheckedListBox1.SetItemChecked(0, True)
            CheckedListBox1.Enabled = False
            btnSimpan.Enabled = False
            'MsgBox("Data Klaim Ada !!!!")
            Call tampilHasilKlaim()
        End If

        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 0

        If txtCaraPulang.Text.Equals("Atas Persetujuan Dokter", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 1
        ElseIf txtCaraPulang.Text.Equals("Dirujuk", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 2
        ElseIf txtCaraPulang.Text.Equals("Atas Permintaan Sendiri", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 3
        ElseIf txtCaraPulang.Text.Equals("Meninggal", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 4
        ElseIf txtCaraPulang.Text.Equals("Lain-lain", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 5
        End If

        If txtCaraPulang.Text.Equals("Atas Persetujuan Dokter", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 1
        ElseIf txtCaraPulang.Text.Equals("Dirujuk", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 2
        ElseIf txtCaraPulang.Text.Equals("Atas Permintaan Sendiri", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 3
        ElseIf txtCaraPulang.Text.Equals("Meninggal", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 4
        ElseIf txtCaraPulang.Text.Equals("Lain-lain", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 5
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
        Form1.Show()
        Me.Close()
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

    Private Sub txtSetJaminan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtSetJaminan.SelectedIndexChanged
        txtJaminan.Text = txtSetJaminan.Text
        Call koneksiServer()

        Try
            Dim query As String
            query = "SELECT id FROM t_eklaimjaminan WHERE jaminan = '" & txtSetJaminan.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                wspayor_id = dr.GetString("id")
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub CheckedListBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles CheckedListBox1.MouseDown
        Dim Index As Integer = CheckedListBox1.IndexFromPoint(e.Location)
        CheckedListBox1.SetItemChecked(Index, Not CheckedListBox1.GetItemChecked(Index))
    End Sub

    Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        If e.NewValue = CheckState.Checked Then
            statusFinal = "FINAL"
            dateFinal = Format(DateTime.Now, "yyyy-MM-dd HH:mm:ss")
        Else
            statusFinal = "-"
        End If
    End Sub

    Private Sub picKeluar_Click(sender As Object, e As EventArgs) Handles picKeluar.Click
        Dim konfirmasi As MsgBoxResult

        konfirmasi = MsgBox("Apakah anda yakin ingin keluar..?", vbQuestion + vbYesNo, "Konfirmasi")
        If konfirmasi = vbYes Then
            Me.Close()
            LoginForm.Show()
        End If
    End Sub

    Private Sub picBack_Click(sender As Object, e As EventArgs) Handles picBack.Click
        Form1.Show()
        Me.Close()
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim konfirmasi As MsgBoxResult
        If CheckedListBox1.GetItemChecked(0) Then
            konfirmasi = MsgBox("Apakah anda yakin data yang diinputkan sudah benar ?", vbQuestion + vbYesNo, "Konfirmasi")

            If konfirmasi = vbYes Then
                Call addPasien()
                Call addDetail()
                Call NewPasien()
                Call setKlaimDataPasien()
            End If
        Else
            MsgBox("Mohon klik centang jika data telah sesuai !!", MsgBoxStyle.Information)
            Me.ErrorProvider1.SetError(Me.CheckedListBox1, "Mohon klik centang dahulu")
        End If
    End Sub

    Private Sub PicClose1_Click(sender As Object, e As EventArgs) Handles PicClose1.Click
        If TableLayoutPanel3.ColumnStyles(1).Width = 30 Then
            TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
            TableLayoutPanel3.ColumnStyles(1).Width = 0
        Else
            TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
            TableLayoutPanel3.ColumnStyles(1).Width = 30
        End If
    End Sub

    Private Sub txtRawat_TextChanged(sender As Object, e As EventArgs) Handles txtRawat.TextChanged
        If txtRawat.Text.Equals("Rawat Inap", StringComparison.OrdinalIgnoreCase) Then
            wsJenisRawat = 1
        ElseIf txtRawat.Text.Equals("Rawat Jalan", StringComparison.OrdinalIgnoreCase) Then
            wsJenisRawat = 2
        ElseIf txtRawat.Text.Equals("Igd", StringComparison.OrdinalIgnoreCase) Then
            wsJenisRawat = 2
        End If
    End Sub

    Private Sub txtKelas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtKelas.SelectedIndexChanged
        If txtKelas.Text.Equals("Kelas III", StringComparison.OrdinalIgnoreCase) Then
            wsKelas = 3
        ElseIf txtKelas.Text.Equals("Kelas II", StringComparison.OrdinalIgnoreCase) Then
            wsKelas = 2
        ElseIf txtKelas.Text.Equals("Kelas I", StringComparison.OrdinalIgnoreCase) Then
            wsKelas = 1
        End If
    End Sub

    Private Sub txtKelas_TextChanged(sender As Object, e As EventArgs) Handles txtKelas.TextChanged
        If txtKelas.Text.Equals("Kelas III", StringComparison.OrdinalIgnoreCase) Then
            wsKelas = 3
        ElseIf txtKelas.Text.Equals("Kelas II", StringComparison.OrdinalIgnoreCase) Then
            wsKelas = 2
        ElseIf txtKelas.Text.Equals("Kelas I", StringComparison.OrdinalIgnoreCase) Then
            wsKelas = 1
        End If
    End Sub

    Private Sub txtCaraPulang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtCaraPulang.SelectedIndexChanged
        If txtCaraPulang.Text.Equals("Atas Persetujuan Dokter", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 1
        ElseIf txtCaraPulang.Text.Equals("Dirujuk", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 2
        ElseIf txtCaraPulang.Text.Equals("Atas Permintaan Sendiri", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 3
        ElseIf txtCaraPulang.Text.Equals("Meninggal", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 4
        ElseIf txtCaraPulang.Text.Equals("Lain-lain", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 5
        End If
    End Sub

    Private Sub txtCaraPulang_TextChanged(sender As Object, e As EventArgs) Handles txtCaraPulang.TextChanged
        If txtCaraPulang.Text.Equals("Atas Persetujuan Dokter", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 1
        ElseIf txtCaraPulang.Text.Equals("Dirujuk", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 2
        ElseIf txtCaraPulang.Text.Equals("Atas Permintaan Sendiri", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 3
        ElseIf txtCaraPulang.Text.Equals("Meninggal", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 4
        ElseIf txtCaraPulang.Text.Equals("Lain-lain", StringComparison.OrdinalIgnoreCase) Then
            wsCaraPulang = 5
        End If
    End Sub

    Private Sub txtTglMskRawat_ValueChanged(sender As Object, e As EventArgs) Handles txtTglMskRawat.ValueChanged
        If instalasi.Equals("Rawat Jalan", StringComparison.OrdinalIgnoreCase) Then
            txtLos.Text = 1
        ElseIf instalasi.Equals("Rawat Inap", StringComparison.OrdinalIgnoreCase) Then
            txtLos.Text = Val(DateAndTime.DateDiff(DateInterval.Day, txtTglMskRawat.Value, txtTglKlrRawat.Value) + 1)
        End If
    End Sub

    Private Sub txtTglKlrRawat_ValueChanged(sender As Object, e As EventArgs) Handles txtTglKlrRawat.ValueChanged
        If instalasi.Equals("Rawat Jalan", StringComparison.OrdinalIgnoreCase) Then
            txtLos.Text = 1
        ElseIf instalasi.Equals("Rawat Inap", StringComparison.OrdinalIgnoreCase) Then
            txtLos.Text = Val(DateAndTime.DateDiff(DateInterval.Day, txtTglMskRawat.Value, txtTglKlrRawat.Value) + 1)
        End If
    End Sub

    Private Sub dgvDetail_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvDetail.CellFormatting
        dgvDetail.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvDetail.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvDetail.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvDetail.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        dgvDetail.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
        dgvDetail.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvDetail.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        For i As Integer = 0 To dgvDetail.Rows.Count - 1
            If i Mod 2 = 0 Then
                dgvDetail.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                dgvDetail.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Private Sub TextBox_keyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub dgvDetail_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvDetail.RowPostPaint
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

    Private Sub dgvjpRanap_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs)
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

    Private Sub dgvjpRajal_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs)
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

    Private Sub checkIntensif_CheckedChanged(sender As Object, e As EventArgs) Handles checkIntensif.CheckedChanged
        If checkIntensif.Checked = True Then
            NumericUpDown1.Visible = True
            NumericUpDown2.Visible = True
            Label38.Visible = True
            Label45.Visible = True
            Label46.Visible = True
            wsIcu = "1"
        Else
            NumericUpDown1.Visible = False
            NumericUpDown2.Visible = False
            Label38.Visible = False
            Label45.Visible = False
            Label46.Visible = False
            wsIcu = "0"
            NumericUpDown1.Value = 0
            NumericUpDown2.Value = 0
        End If
    End Sub

    Private Sub checkEksekutif_CheckedChanged(sender As Object, e As EventArgs) Handles checkEksekutif.CheckedChanged
        If checkEksekutif.Checked = True Then
            txtEksekutif.Visible = True
            Label47.Visible = True
        ElseIf checkEksekutif.Checked = False Then
            txtEksekutif.Visible = False
            Label47.Visible = False
        End If
    End Sub

    Private Sub txtTotalTarif_Click(sender As Object, e As EventArgs) Handles txtTotalTarif.Click
        t = txtTotalTarif.Text
        txtTotalTarif.Text = Val(t).ToString("#,##0")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
        '    Call detailJpRekapRajal()
        '    Panel2.AutoScroll = True
        '    
        '    dgvjpRajal.Visible = True
        '    Call totalJpRajal()
        'ElseIf txtRawat.Text.Contains("Rawat Inap") Then
        '    Call detailJpRekapRanap()
        '    Panel2.AutoScroll = True
        '    
        '    dgvjpRanap.Visible = True
        '    Call totalJpRanap()
        'End If

        Berakdown.Show()
        Me.Hide()
    End Sub

#Region "Non Bedah"
    Private Sub txtNonBedah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNonBedah.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtNonBedah_TextChanged(sender As Object, e As EventArgs) Handles txtNonBedah.TextChanged
        If txtNonBedah.Text = "" Then
            txtNonBedah.Text = 0
        End If
        a = txtNonBedah.Text
        txtNonBedah.Text = Format(Val(a), "#,##0")
        txtNonBedah.SelectionStart = Len(txtNonBedah.Text)
        txtTotalTarif.Text = Format(a + b + c + d + ee + f +
                                    g + h + i + j + k + l +
                                    m + n + o + p + q + r, "###,###")
    End Sub

    Private Sub txtNonBedah_GotFocus(sender As Object, e As EventArgs) Handles txtNonBedah.GotFocus
        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 30
        txtLabelDetail.Text = "Detail Item - Prosedur Non Bedah"
        dgvDetail.Rows.Clear()
        Call tampilNonBedah()
        Call detailNonBedah()
        Panel2.AutoScroll = True

    End Sub

    Private Sub txtNonBedah_LostFocus(sender As Object, e As EventArgs) Handles txtNonBedah.LostFocus
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
    End Sub
#End Region
#Region "Bedah"
    Private Sub txtBedah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBedah.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtBedah_TextChanged(sender As Object, e As EventArgs) Handles txtBedah.TextChanged
        If txtBedah.Text = "" Then
            txtBedah.Text = 0
        End If
        b = txtBedah.Text
        txtBedah.Text = Format(Val(b), "#,##0")
        txtBedah.SelectionStart = Len(txtBedah.Text)
        txtTotalTarif.Text = Format(a + b + c + d + ee + f +
                                    g + h + i + j + k + l +
                                    m + n + o + p + q + r, "###,###")
    End Sub

    Private Sub txtBedah_GotFocus(sender As Object, e As EventArgs) Handles txtBedah.GotFocus
        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 30
        txtLabelDetail.Text = "Detail Item - Prosedur Bedah"
        dgvDetail.Rows.Clear()
        Call tampilBedah()
        Call detailBedah()
        Panel2.AutoScroll = True

    End Sub

    Private Sub txtBedah_LostFocus(sender As Object, e As EventArgs) Handles txtBedah.LostFocus
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
    End Sub
#End Region
#Region "Konsul"
    Private Sub txtKonsul_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKonsul.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtKonsul_TextChanged(sender As Object, e As EventArgs) Handles txtKonsul.TextChanged
        If txtKonsul.Text = "" Then
            txtKonsul.Text = 0
        End If
        c = txtKonsul.Text
        txtKonsul.Text = Format(Val(c), "#,##0")
        txtKonsul.SelectionStart = Len(txtKonsul.Text)
        txtTotalTarif.Text = Format(a + b + c + d + ee + f +
                                    g + h + i + j + k + l +
                                    m + n + o + p + q + r, "###,###")
    End Sub

    Private Sub txtKonsul_GotFocus(sender As Object, e As EventArgs) Handles txtKonsul.GotFocus
        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 30
        txtLabelDetail.Text = "Detail Item - Konsultasi"
        dgvDetail.Rows.Clear()
        Call detailVisite()
        Call totalTarif()
        Panel2.AutoScroll = True

    End Sub

    Private Sub txtKonsul_LostFocus(sender As Object, e As EventArgs) Handles txtKonsul.LostFocus
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
    End Sub
#End Region
#Region "PPA"
    Private Sub txtPPA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPPA.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtPPA_TextChanged(sender As Object, e As EventArgs) Handles txtPPA.TextChanged
        If txtPPA.Text = "" Then
            txtPPA.Text = 0
        End If
        d = txtPPA.Text
        txtPPA.Text = Format(Val(d), "#,##0")
        txtPPA.SelectionStart = Len(txtPPA.Text)
        txtTotalTarif.Text = Format(a + b + c + d + ee + f +
                                    g + h + i + j + k + l +
                                    m + n + o + p + q + r, "###,###")
    End Sub

    Private Sub txtPPA_GotFocus(sender As Object, e As EventArgs) Handles txtPPA.GotFocus
        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 30
        txtLabelDetail.Text = "Detail Item - Tenaga Ahli"
        dgvDetail.Rows.Clear()
        Call detailJasa()
        Call totalTarif()
        Panel2.AutoScroll = True

    End Sub

    Private Sub txtPPA_LostFocus(sender As Object, e As EventArgs) Handles txtPPA.LostFocus
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
    End Sub

#End Region
#Region "Keperawatan"
    Private Sub txtKeperawatan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKeperawatan.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtKeperawatan_TextChanged(sender As Object, e As EventArgs) Handles txtKeperawatan.TextChanged
        If txtKeperawatan.Text = "" Then
            txtKeperawatan.Text = 0
        End If
        ee = txtKeperawatan.Text
        txtKeperawatan.Text = Format(Val(ee), "#,##0")
        txtKeperawatan.SelectionStart = Len(txtKeperawatan.Text)
        txtTotalTarif.Text = Format(a + b + c + d + ee + f +
                                    g + h + i + j + k + l +
                                    m + n + o + p + q + r, "###,###")
    End Sub

    Private Sub txtKeperawatan_GotFocus(sender As Object, e As EventArgs) Handles txtKeperawatan.GotFocus
        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 30
        txtLabelDetail.Text = "Detail Item - Keperawatan"
        dgvDetail.Rows.Clear()
        Call detailTindakan()
        Call totalTarif()
        Panel2.AutoScroll = True

    End Sub

    Private Sub txtKeperawatan_LostFocus(sender As Object, e As EventArgs) Handles txtKeperawatan.LostFocus
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
    End Sub

#End Region
#Region "Penunjang"
    Private Sub txtPenunjang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPenunjang.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtPenunjang_TextChanged(sender As Object, e As EventArgs) Handles txtPenunjang.TextChanged
        If txtPenunjang.Text = "" Then
            txtPenunjang.Text = 0
        End If
        f = txtPenunjang.Text
        txtPenunjang.Text = Format(Val(f), "#,##0")
        txtPenunjang.SelectionStart = Len(txtPenunjang.Text)
        txtTotalTarif.Text = Format(a + b + c + d + ee + f +
                                    g + h + i + j + k + l +
                                    m + n + o + p + q + r, "###,###")
    End Sub

    Private Sub txtPenunjang_GotFocus(sender As Object, e As EventArgs) Handles txtPenunjang.GotFocus
        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 30
        txtLabelDetail.Text = "Detail Item - Penunjang"
        Call tampilPenunjang()
        Call detailPenunjang()
        Panel2.AutoScroll = True

    End Sub

    Private Sub txtPenunjang_LostFocus(sender As Object, e As EventArgs) Handles txtPenunjang.LostFocus
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
    End Sub
#End Region
#Region "Rad"
    Private Sub txtRadiologi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRadiologi.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtRadiologi_TextChanged(sender As Object, e As EventArgs) Handles txtRadiologi.TextChanged
        If txtRadiologi.Text = "" Then
            txtRadiologi.Text = 0
        End If
        g = txtRadiologi.Text
        txtRadiologi.Text = Format(Val(g), "#,##0")
        txtRadiologi.SelectionStart = Len(txtRadiologi.Text)
        txtTotalTarif.Text = Format(a + b + c + d + ee + f +
                                    g + h + i + j + k + l +
                                    m + n + o + p + q + r, "###,###")
    End Sub

    Private Sub txtRadiologi_GotFocus(sender As Object, e As EventArgs) Handles txtRadiologi.GotFocus
        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 30
        txtLabelDetail.Text = "Detail Item - Radiologi"
        dgvDetail.Rows.Clear()
        Call detailRad()
        Call totalTarif()
        Panel2.AutoScroll = True

    End Sub

    Private Sub txtRadiologi_LostFocus(sender As Object, e As EventArgs) Handles txtRadiologi.LostFocus
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
    End Sub
#End Region
#Region "Lab"
    Private Sub txtLab_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLab.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtLab_TextChanged(sender As Object, e As EventArgs) Handles txtLab.TextChanged
        If txtLab.Text = "" Then
            txtLab.Text = 0
        End If
        h = txtLab.Text
        txtLab.Text = Format(Val(h), "#,##0")
        txtLab.SelectionStart = Len(txtLab.Text)
        txtTotalTarif.Text = Format(a + b + c + d + ee + f +
                                    g + h + i + j + k + l +
                                    m + n + o + p + q + r, "###,###")
    End Sub

    Private Sub txtLab_GotFocus(sender As Object, e As EventArgs) Handles txtLab.GotFocus
        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 30
        txtLabelDetail.Text = "Detail Item - Laboratorium"
        dgvDetail.Rows.Clear()
        Call detailLab()
        Call totalTarif()
        Panel2.AutoScroll = True

    End Sub

    Private Sub txtLab_LostFocus(sender As Object, e As EventArgs) Handles txtLab.LostFocus
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
    End Sub
#End Region
#Region "Darah"
    Private Sub txtDarah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDarah.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtDarah_TextChanged(sender As Object, e As EventArgs) Handles txtDarah.TextChanged
        If txtDarah.Text = "" Then
            txtDarah.Text = 0
        End If
        i = txtDarah.Text
        txtDarah.Text = Format(Val(i), "#,##0")
        txtDarah.SelectionStart = Len(txtDarah.Text)
        txtTotalTarif.Text = Format(a + b + c + d + ee + f +
                                    g + h + i + j + k + l +
                                    m + n + o + p + q + r, "###,###")
    End Sub

    Private Sub txtDarah_GotFocus(sender As Object, e As EventArgs) Handles txtDarah.GotFocus
        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 30
        txtLabelDetail.Text = "Detail Item - Pelayanan Darah"
        Call detailDarah()
        Call totalTarif()
        Panel2.AutoScroll = True

    End Sub

    Private Sub txtDarah_LostFocus(sender As Object, e As EventArgs) Handles txtDarah.LostFocus
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
    End Sub
#End Region
#Region "Rehab"
    Private Sub txtRehab_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRehab.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtRehab_TextChanged(sender As Object, e As EventArgs) Handles txtRehab.TextChanged
        If txtRehab.Text = "" Then
            txtRehab.Text = 0
        End If
        j = txtRehab.Text
        txtRehab.Text = Format(Val(j), "#,##0")
        txtRehab.SelectionStart = Len(txtRehab.Text)
        txtTotalTarif.Text = Format(a + b + c + d + ee + f +
                                    g + h + i + j + k + l +
                                    m + n + o + p + q + r, "###,###")
    End Sub

    Private Sub txtRehab_GotFocus(sender As Object, e As EventArgs) Handles txtRehab.GotFocus
        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 30
        txtLabelDetail.Text = "Detail Item - Rahabilitasi"
        dgvDetail.Rows.Clear()
        Call detailRehab()
        Call totalTarif()
        Panel2.AutoScroll = True

    End Sub

    Private Sub txtRehab_LostFocus(sender As Object, e As EventArgs) Handles txtRehab.LostFocus
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
    End Sub
#End Region
#Region "Akomodasi"
    Private Sub txtAkomodasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAkomodasi.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtAkomodasi_TextChanged(sender As Object, e As EventArgs) Handles txtAkomodasi.TextChanged
        If txtAkomodasi.Text = "" Then
            txtAkomodasi.Text = 0
        End If
        k = txtAkomodasi.Text
        txtAkomodasi.Text = Format(Val(k), "#,##0")
        txtAkomodasi.SelectionStart = Len(txtAkomodasi.Text)
        txtTotalTarif.Text = Format(a + b + c + d + ee + f +
                                    g + h + i + j + k + l +
                                    m + n + o + p + q + r, "###,###")
    End Sub

    Private Sub txtAkomodasi_GotFocus(sender As Object, e As EventArgs) Handles txtAkomodasi.GotFocus
        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 30
        txtLabelDetail.Text = "Detail Item - Kamar/Akomodasi"
        dgvDetail.Rows.Clear()
        If txtRawat.Text.Contains("Rawat Jalan") Or txtRawat.Text.Contains("Igd") Then
            dgvDetail.Columns(0).HeaderText = "Tgl. Masuk"
            dgvDetail.Columns(1).HeaderText = "Poli"
            dgvDetail.Columns(2).HeaderText = "Karcis"
            dgvDetail.Columns(3).HeaderText = "Konsul Dokter"
            dgvDetail.Columns(5).HeaderText = "Kelas"
            dgvDetail.Columns(3).DefaultCellStyle.Format = "N2"
            dgvDetail.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        ElseIf txtRawat.Text.Contains("Rawat Inap") Then
            dgvDetail.Columns(0).HeaderText = "Tgl.Masuk"
            dgvDetail.Columns(1).HeaderText = "Ruang"
            dgvDetail.Columns(5).HeaderText = "Kelas"
        End If
        Call detailAkomodasi()
        Call totalTarif()
        Panel2.AutoScroll = True

    End Sub

    Private Sub txtAkomodasi_LostFocus(sender As Object, e As EventArgs) Handles txtAkomodasi.LostFocus
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
        dgvDetail.Columns(0).HeaderText = "Tgl. Tindakan"
        dgvDetail.Columns(1).HeaderText = "Tindakan"
        dgvDetail.Columns(2).HeaderText = "Tarif"
        dgvDetail.Columns(3).HeaderText = "QTY"
        dgvDetail.Columns(5).HeaderText = "PPA"
        dgvDetail.Columns(3).DefaultCellStyle.Format = "N0"
        dgvDetail.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub
#End Region
#Region "Intensif"
    Private Sub txtIntensif_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIntensif.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtIntensif_TextChanged(sender As Object, e As EventArgs) Handles txtIntensif.TextChanged
        If txtIntensif.Text = "" Then
            txtIntensif.Text = 0
        End If
        l = txtIntensif.Text
        txtIntensif.Text = Format(Val(l), "#,##0")
        txtIntensif.SelectionStart = Len(txtIntensif.Text)
        txtTotalTarif.Text = Format(a + b + c + d + ee + f +
                                    g + h + i + j + k + l +
                                    m + n + o + p + q + r, "###,###")
    End Sub

    Private Sub txtIntensif_GotFocus(sender As Object, e As EventArgs) Handles txtIntensif.GotFocus
        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 30
        txtLabelDetail.Text = "Detail Item - Intensif"
        dgvDetail.Rows.Clear()
        dgvDetail.Columns(0).HeaderText = "Tgl. Masuk"
        dgvDetail.Columns(1).HeaderText = "Kelas"
        dgvDetail.Columns(5).HeaderText = "Ruang"
        Call detailIntensif()
        Call totalTarif()
        Panel2.AutoScroll = True

    End Sub

    Private Sub txtIntensif_LostFocus(sender As Object, e As EventArgs) Handles txtIntensif.LostFocus
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
        dgvDetail.Columns(0).HeaderText = "Tgl. Tindakan"
        dgvDetail.Columns(1).HeaderText = "Tindakan"
        dgvDetail.Columns(5).HeaderText = "PPA"
    End Sub
#End Region
#Region "Obat"
    Private Sub txtObat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtObat.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtObat_TextChanged(sender As Object, e As EventArgs) Handles txtObat.TextChanged
        If txtObat.Text = "" Then
            txtObat.Text = 0
        End If
        m = txtObat.Text
        txtObat.Text = Format(Val(m), "#,##0")
        txtObat.SelectionStart = Len(txtObat.Text)
        txtTotalTarif.Text = Format(a + b + c + d + ee + f +
                                    g + h + i + j + k + l +
                                    m + n + o + p + q + r, "###,###")
    End Sub

    Private Sub txtObat_GotFocus(sender As Object, e As EventArgs) Handles txtObat.GotFocus
        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 30
        txtLabelDetail.Text = "Detail Item - Obat"
        dgvDetail.Rows.Clear()
        dgvDetail.Columns(0).HeaderText = "Tgl. Penjualan"
        dgvDetail.Columns(1).HeaderText = "Obat"
        dgvDetail.Columns(2).DefaultCellStyle.Format = "N2"
        dgvDetail.Columns(4).DefaultCellStyle.Format = "N2"
        Call detailObatNonKronis()
        Call totalTarif()
        Panel2.AutoScroll = True

    End Sub

    Private Sub txtObat_LostFocus(sender As Object, e As EventArgs) Handles txtObat.LostFocus
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
        dgvDetail.Columns(0).HeaderText = "Tgl. Tindakan"
        dgvDetail.Columns(1).HeaderText = "Tindakan"
        dgvDetail.Columns(2).DefaultCellStyle.Format = "N0"
        dgvDetail.Columns(4).DefaultCellStyle.Format = "N0"
    End Sub
#End Region
#Region "Obat Kronis"
    Private Sub txtObatKronis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtObatKronis.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtObatKronis_TextChanged(sender As Object, e As EventArgs) Handles txtObatKronis.TextChanged
        If txtObatKronis.Text = "" Then
            txtObatKronis.Text = 0
        End If
        n = txtObatKronis.Text
        txtObatKronis.Text = Format(Val(n), "#,##0")
        txtObatKronis.SelectionStart = Len(txtObatKronis.Text)
        txtTotalTarif.Text = Format(a + b + c + d + ee + f +
                                    g + h + i + j + k + l +
                                    m + n + o + p + q + r, "###,###")
    End Sub

    Private Sub txtObatKronis_GotFocus(sender As Object, e As EventArgs) Handles txtObatKronis.GotFocus
        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 30
        txtLabelDetail.Text = "Detail Item - Obat Kronis"
        dgvDetail.Rows.Clear()
        dgvDetail.Columns(0).HeaderText = "Tgl. Penjualan"
        dgvDetail.Columns(1).HeaderText = "Obat"
        dgvDetail.Columns(2).DefaultCellStyle.Format = "N2"
        dgvDetail.Columns(4).DefaultCellStyle.Format = "N2"
        Call detailObatKronis()
        Call totalTarif()
        Panel2.AutoScroll = True

    End Sub

    Private Sub txtObatKronis_LostFocus(sender As Object, e As EventArgs) Handles txtObatKronis.LostFocus
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
        dgvDetail.Columns(0).HeaderText = "Tgl. Tindakan"
        dgvDetail.Columns(1).HeaderText = "Tindakan"
        dgvDetail.Columns(2).DefaultCellStyle.Format = "N0"
        dgvDetail.Columns(4).DefaultCellStyle.Format = "N0"
    End Sub
#End Region
#Region "Obat Kemo"
    Private Sub txtObatKemo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtObatKemo.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtObatKemo_TextChanged(sender As Object, e As EventArgs) Handles txtObatKemo.TextChanged
        If txtObatKemo.Text = "" Then
            txtObatKemo.Text = 0
        End If
        o = txtObatKemo.Text
        txtObatKemo.Text = Format(Val(o), "#,##0")
        txtObatKemo.SelectionStart = Len(txtObatKemo.Text)
        txtTotalTarif.Text = Format(a + b + c + d + ee + f +
                                    g + h + i + j + k + l +
                                    m + n + o + p + q + r, "###,###")
    End Sub

    Private Sub txtObatKemo_GotFocus(sender As Object, e As EventArgs) Handles txtObatKemo.GotFocus
        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 30
        txtLabelDetail.Text = "Detail Item - Obat Kemo"
        Panel2.AutoScroll = True

    End Sub

    Private Sub txtObatKemo_LostFocus(sender As Object, e As EventArgs) Handles txtObatKemo.LostFocus
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
    End Sub
#End Region
#Region "Alkes"
    Private Sub txtAlkes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAlkes.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtAlkes_TextChanged(sender As Object, e As EventArgs) Handles txtAlkes.TextChanged
        If txtAlkes.Text = "" Then
            txtAlkes.Text = 0
        End If
        p = txtAlkes.Text
        txtAlkes.Text = Format(Val(p), "#,##0")
        txtAlkes.SelectionStart = Len(txtAlkes.Text)
        txtTotalTarif.Text = Format(a + b + c + d + ee + f +
                                    g + h + i + j + k + l +
                                    m + n + o + p + q + r, "###,###")
    End Sub

    Private Sub txtAlkes_GotFocus(sender As Object, e As EventArgs) Handles txtAlkes.GotFocus
        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 30
        txtLabelDetail.Text = "Detail Item - Alkes"
        dgvDetail.Rows.Clear()
        dgvDetail.Columns(0).HeaderText = "Tgl. Penjualan"
        dgvDetail.Columns(1).HeaderText = "Obat"
        dgvDetail.Columns(2).DefaultCellStyle.Format = "N2"
        dgvDetail.Columns(4).DefaultCellStyle.Format = "N2"
        Call detailAlkes()
        Call totalTarif()
        Panel2.AutoScroll = True

    End Sub

    Private Sub txtAlkes_LostFocus(sender As Object, e As EventArgs) Handles txtAlkes.LostFocus
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
        dgvDetail.Columns(0).HeaderText = "Tgl. Tindakan"
        dgvDetail.Columns(1).HeaderText = "Tindakan"
        dgvDetail.Columns(2).DefaultCellStyle.Format = "N0"
        dgvDetail.Columns(4).DefaultCellStyle.Format = "N0"
    End Sub
#End Region
#Region "BMHP"
    Private Sub txtBMHP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBMHP.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtBMHP_TextChanged(sender As Object, e As EventArgs) Handles txtBMHP.TextChanged
        If txtBMHP.Text = "" Then
            txtBMHP.Text = 0
        End If
        q = txtBMHP.Text
        txtBMHP.Text = Format(Val(q), "#,##0")
        txtBMHP.SelectionStart = Len(txtBMHP.Text)
        txtTotalTarif.Text = Format(a + b + c + d + ee + f +
                                    g + h + i + j + k + l +
                                    m + n + o + p + q + r, "###,###")
    End Sub

    Private Sub txtBMHP_GotFocus(sender As Object, e As EventArgs) Handles txtBMHP.GotFocus
        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 30
        txtLabelDetail.Text = "Detail Item - BMHP"
        dgvDetail.Rows.Clear()
        Call detailBmhp()
        Call totalTarif()
        Panel2.AutoScroll = True

    End Sub

    Private Sub txtBMHP_LostFocus(sender As Object, e As EventArgs) Handles txtBMHP.LostFocus
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
    End Sub
#End Region
#Region "Sewa"
    Private Sub txtSewaAlat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSewaAlat.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtSewaAlat_TextChanged(sender As Object, e As EventArgs) Handles txtSewaAlat.TextChanged
        If txtSewaAlat.Text = "" Then
            txtSewaAlat.Text = 0
        End If
        r = txtSewaAlat.Text
        txtSewaAlat.Text = Format(Val(r), "#,##0")
        txtSewaAlat.SelectionStart = Len(txtSewaAlat.Text)
        txtTotalTarif.Text = Format(a + b + c + d + ee + f +
                                    g + h + i + j + k + l +
                                    m + n + o + p + q + r, "###,###")
    End Sub

    Private Sub txtSewaAlat_GotFocus(sender As Object, e As EventArgs) Handles txtSewaAlat.GotFocus
        TableLayoutPanel3.ColumnStyles(1).SizeType = SizeType.Percent
        TableLayoutPanel3.ColumnStyles(1).Width = 30
        txtLabelDetail.Text = "Detail Item - Sewa Alat"
        dgvDetail.Rows.Clear()
        Call detailSewa()
        Call totalTarif()
        Panel2.AutoScroll = True
    End Sub

    Private Sub txtSewaAlat_LostFocus(sender As Object, e As EventArgs) Handles txtSewaAlat.LostFocus
        txtTotalTarif.Text = (Val(CInt(txtNonBedah.Text)) + Val(CInt(txtBedah.Text)) + Val(CInt(txtKonsul.Text)) +
                              Val(CInt(txtPPA.Text)) + Val(CInt(txtKeperawatan.Text)) + Val(CInt(txtPenunjang.Text)) +
                              Val(CInt(txtRadiologi.Text)) + Val(CInt(txtLab.Text)) + Val(CInt(txtDarah.Text)) +
                              Val(CInt(txtRehab.Text)) + Val(CInt(txtAkomodasi.Text)) + Val(CInt(txtIntensif.Text)) +
                              Val(CInt(txtObat.Text)) + Val(CInt(txtObatKronis.Text)) + Val(CInt(txtObatKemo.Text)) +
                              Val(CInt(txtAlkes.Text)) + Val(CInt(txtBMHP.Text)) + Val(CInt(txtSewaAlat.Text))
                             ).ToString("#,##0")
    End Sub
#End Region
End Class