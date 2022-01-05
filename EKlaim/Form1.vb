Imports System.ComponentModel
Imports MySql.Data.MySqlClient
Public Class Form1

    Public tglMasuk, noDaftar, noRm, nmPasien, tglKeluar, unit, kelas, statusKeluar, carabayar, penjamin, tglLahir, regUnit, tglDaftar, jk As String
    Dim subKelas As String
    Dim KoneksiString As String = "server=192.168.200.2;user=lis;password=lis1234;database=simrs;default command timeout=120;Convert Zero Datetime=True"

    Sub setColor(button As Button)
        btnHome.BackColor = Color.White
        btnEklaim.BackColor = Color.White
        btnBuku.BackColor = Color.White
        btnPiutang.BackColor = Color.White
        btnUmum.BackColor = Color.White
        btnTotal.BackColor = Color.White
        button.BackColor = Color.FromArgb(209, 232, 223)
    End Sub

    Sub autoCaraBayar()
        Call koneksiServer()
        cmd = New MySqlCommand("SELECT 'Semua' AS cara UNION
                                SELECT carabayar AS cara FROM t_carabayar", conn)
        da = New MySqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        txtBayar.DataSource = dt
        txtBayar.DisplayMember = "cara"
        txtBayar.ValueMember = "cara"
        txtBayar.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Sub cariPasien()
        Dim valCari As String = ""
        If IsNumeric(txtCari.Text) Then
            'MsgBox("angka")
            Select Case txtCari.Text.Length
                Case 1
                    valCari = "0000000" & txtCari.Text
                    txtCari.Text = valCari
                Case 2
                    valCari = "000000" & txtCari.Text
                    txtCari.Text = valCari
                Case 3
                    valCari = "00000" & txtCari.Text
                    txtCari.Text = valCari
                Case 4
                    valCari = "0000" & txtCari.Text
                    txtCari.Text = valCari
                Case 5
                    valCari = "000" & txtCari.Text
                    txtCari.Text = valCari
                Case 6
                    valCari = "00" & txtCari.Text
                    txtCari.Text = valCari
                Case 7
                    valCari = "0" & txtCari.Text
                    txtCari.Text = valCari
                Case 8
                    valCari = txtCari.Text
                    txtCari.Text = valCari
            End Select
        Else
            'MsgBox("alfabet")
        End If

        Call koneksiServer()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        query = "SELECT noRekamedis
                   FROM t_pasien
                  WHERE ( nmPasien LIKE '%" & txtCari.Text & "%' OR noRekamedis = '" & txtCari.Text & "' )
               ORDER BY noRekamedis ASC LIMIT 1"
        Try
            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)

            Dim str As New DataTable
            str.Clear()
            da.Fill(str)
            If str.Rows.Count() > 0 Then
                'txtCari.Text = str.Rows(0)(0).ToString
                listRegistrasi(str.Rows(0)(0).ToString)
            Else
                MessageBox.Show("Pasien Tidak Ada / Belum Terdaftar", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        conn.Close()
    End Sub

    Sub cari2(rawat As String, bayar As String, kelas As String, tgl1 As Date, tgl2 As Date)
        Dim cn As New MySqlConnection(KoneksiString)
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        If rawat.Equals("Semua", StringComparison.OrdinalIgnoreCase) And
            bayar.Equals("Semua", StringComparison.OrdinalIgnoreCase) And
            kelas.Equals("Semua", StringComparison.OrdinalIgnoreCase) Then                 'SSS
            'MsgBox("SSS")
            query = "SELECT *
                       FROM vw_pasienkasir
                      WHERE statusKeluar != 'batal'
                        AND ((SUBSTR(tglMasukRawatJalan,1,10) BETWEEN '" & Format(tgl1, "yyyy-MM-dd") & "' AND 
                            '" & Format(DateAdd(DateInterval.Day, 0, tgl2), "yyyy-MM-dd") & "')
                         OR (SUBSTR(tglKeluarRawatJalan,1,10) BETWEEN '" & Format(tgl1, "yyyy-MM-dd") & "' AND 
                            '" & Format(DateAdd(DateInterval.Day, 0, tgl2), "yyyy-MM-dd") & "'))
                      GROUP BY noDaftar  
                      ORDER BY tglKeluarRawatJalan DESC"
        ElseIf rawat.Equals("Semua", StringComparison.OrdinalIgnoreCase) And
                bayar <> "Semua" And
                kelas.Equals("Semua", StringComparison.OrdinalIgnoreCase) Then             'S0S
            'MsgBox("S0S")
            query = "SELECT *
                       FROM vw_pasienkasir
                      WHERE statusKeluar != 'batal'
                        AND carabayar = '" & txtBayar.Text & "'
                        AND ((SUBSTR(tglMasukRawatJalan,1,10) BETWEEN '" & Format(tgl1, "yyyy-MM-dd") & "' AND 
                            '" & Format(DateAdd(DateInterval.Day, 0, tgl2), "yyyy-MM-dd") & "')
                         OR (SUBSTR(tglKeluarRawatJalan,1,10) BETWEEN '" & Format(tgl1, "yyyy-MM-dd") & "' AND 
                            '" & Format(DateAdd(DateInterval.Day, 0, tgl2), "yyyy-MM-dd") & "'))
                      GROUP BY noDaftar 
                      ORDER BY tglKeluarRawatJalan DESC"
        ElseIf rawat.Equals("Rawat Inap", StringComparison.OrdinalIgnoreCase) And
                bayar.Equals("Semua", StringComparison.OrdinalIgnoreCase) And
                kelas.Equals("Semua", StringComparison.OrdinalIgnoreCase) Then             'ISS
            'MsgBox("ISS")
            query = "SELECT *
                       FROM vw_pasienkasir
                      WHERE statusKeluar != 'batal'
                        AND ((SUBSTR(tglMasukRawatJalan,1,10) BETWEEN '" & Format(tgl1, "yyyy-MM-dd") & "' AND 
                            '" & Format(DateAdd(DateInterval.Day, 0, tgl2), "yyyy-MM-dd") & "')
                         OR (SUBSTR(tglKeluarRawatJalan,1,10) BETWEEN '" & Format(tgl1, "yyyy-MM-dd") & "' AND 
                            '" & Format(DateAdd(DateInterval.Day, 0, tgl2), "yyyy-MM-dd") & "'))
                        AND kelas != '-'
                    GROUP BY noDaftar  
                    ORDER BY tglKeluarRawatJalan DESC"
        ElseIf rawat.Equals("Rawat Inap", StringComparison.OrdinalIgnoreCase) And
                bayar <> "Semua" And
                kelas.Equals("Semua", StringComparison.OrdinalIgnoreCase) Then             'I0S
            'MsgBox("I0S")
            query = "SELECT *
                       FROM vw_pasienkasir
                      WHERE statusKeluar != 'batal'
                        AND carabayar = '" & txtBayar.Text & "'
                        AND ((SUBSTR(tglMasukRawatJalan,1,10) BETWEEN '" & Format(tgl1, "yyyy-MM-dd") & "' AND 
                            '" & Format(DateAdd(DateInterval.Day, 0, tgl2), "yyyy-MM-dd") & "')
                         OR (SUBSTR(tglKeluarRawatJalan,1,10) BETWEEN '" & Format(tgl1, "yyyy-MM-dd") & "' AND 
                            '" & Format(DateAdd(DateInterval.Day, 0, tgl2), "yyyy-MM-dd") & "'))
                        AND kelas != '-'
                      GROUP BY noDaftar  
                      ORDER BY tglKeluarRawatJalan DESC"
        ElseIf rawat.Equals("Rawat Inap", StringComparison.OrdinalIgnoreCase) And
                bayar.Equals("Semua", StringComparison.OrdinalIgnoreCase) And
                txtKelas.Text <> "Semua" Then                                                     'IS0
            'MsgBox("IS0")
            query = "SELECT *
                       FROM vw_pasienkasir
                      WHERE statusKeluar != 'batal'
                        AND kelas = '" & subKelas & "'
                        AND ((SUBSTR(tglMasukRawatJalan,1,10) BETWEEN '" & Format(tgl1, "yyyy-MM-dd") & "' AND 
                            '" & Format(DateAdd(DateInterval.Day, 0, tgl2), "yyyy-MM-dd") & "')
                         OR (SUBSTR(tglKeluarRawatJalan,1,10) BETWEEN '" & Format(tgl1, "yyyy-MM-dd") & "' AND 
                            '" & Format(DateAdd(DateInterval.Day, 0, tgl2), "yyyy-MM-dd") & "'))
                      GROUP BY noDaftar  
                      ORDER BY tglKeluarRawatJalan DESC"
        ElseIf rawat.Equals("Rawat Inap", StringComparison.OrdinalIgnoreCase) And
                bayar <> "Semua" And
                txtKelas.Text <> "Semua" Then                                                     'I00
            'MsgBox("I00")
            query = "SELECT *
                       FROM vw_pasienkasir
                      WHERE statusKeluar != 'batal'
                        AND carabayar = '" & txtBayar.Text & "'
                        AND kelas = '" & subKelas & "'
                        AND ((SUBSTR(tglMasukRawatJalan,1,10) BETWEEN '" & Format(tgl1, "yyyy-MM-dd") & "' AND 
                            '" & Format(DateAdd(DateInterval.Day, 0, tgl2), "yyyy-MM-dd") & "')
                         OR (SUBSTR(tglKeluarRawatJalan,1,10) BETWEEN '" & Format(tgl1, "yyyy-MM-dd") & "' AND 
                            '" & Format(DateAdd(DateInterval.Day, 0, tgl2), "yyyy-MM-dd") & "'))
                      GROUP BY noDaftar  
                      ORDER BY tglKeluarRawatJalan DESC"
        ElseIf rawat.Equals("Rawat Jalan", StringComparison.OrdinalIgnoreCase) And
                bayar.Equals("Semua", StringComparison.OrdinalIgnoreCase) And
                kelas.Equals("Semua", StringComparison.OrdinalIgnoreCase) Then            'JSS
            'MsgBox("JSS")
            query = "SELECT *
                       FROM vw_pasienkasir
                      WHERE statusKeluar != 'batal'
                        AND ((SUBSTR(tglMasukRawatJalan,1,10) BETWEEN '" & Format(tgl1, "yyyy-MM-dd") & "' AND 
                            '" & Format(DateAdd(DateInterval.Day, 0, tgl2), "yyyy-MM-dd") & "')
                         OR (SUBSTR(tglKeluarRawatJalan,1,10) BETWEEN '" & Format(tgl1, "yyyy-MM-dd") & "' AND 
                            '" & Format(DateAdd(DateInterval.Day, 0, tgl2), "yyyy-MM-dd") & "'))
                        AND kelas = '-'
                    GROUP BY noDaftar  
                    ORDER BY tglKeluarRawatJalan DESC"
        ElseIf rawat.Equals("Rawat Jalan", StringComparison.OrdinalIgnoreCase) And
                bayar <> "Semua" And
                kelas.Equals("Semua", StringComparison.OrdinalIgnoreCase) Then           'J0S
            'MsgBox("J0S")
            query = "SELECT *
                       FROM vw_pasienkasir
                      WHERE statusKeluar != 'batal'
                        AND carabayar = '" & txtBayar.Text & "'
                        AND ((SUBSTR(tglMasukRawatJalan,1,10) BETWEEN '" & Format(tgl1, "yyyy-MM-dd") & "' AND 
                            '" & Format(DateAdd(DateInterval.Day, 0, tgl2), "yyyy-MM-dd") & "')
                         OR (SUBSTR(tglKeluarRawatJalan,1,10) BETWEEN '" & Format(tgl1, "yyyy-MM-dd") & "' AND 
                            '" & Format(DateAdd(DateInterval.Day, 0, tgl2), "yyyy-MM-dd") & "'))
                        AND kelas = '-'
                      GROUP BY noDaftar  
                      ORDER BY tglKeluarRawatJalan DESC"
        End If

        Try
            cn.Open()
            cmd = New MySqlCommand(query, cn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()
            Do While dr.Read
                DataGridView1.Invoke(New Action(Function() DataGridView1.Rows.Add(dr.Item("tglMasukRawatJalan"), dr.Item("tglPulang"), dr.Item("noRekamedis"),
                                       dr.Item("nmPasien"), dr.Item("unit"), dr.Item("noDaftar"),
                                       dr.Item("kelas"), dr.Item("statusKeluar"), dr.Item("carabayar"),
                                       dr.Item("penjamin"), dr.Item("tglLahir"), dr.Item("noRegistrasiRawatJalan"),
                                       dr.Item("tglDaftar"), dr.Item("jenisKelamin"))))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        cn.Close()
    End Sub

    Sub listRegistrasi(norm As String)
        Call koneksiServer()
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim query As String
        query = "CALL listpasienregistrasi2('" & norm & "')"

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()
            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("tglMasukRawatJalan"), dr.Item("tglPulang"), dr.Item("noRekamedis"),
                                       dr.Item("nmPasien"), dr.Item("unit"), dr.Item("noDaftar"),
                                       dr.Item("kelas"), dr.Item("statusKeluar"), dr.Item("carabayar"),
                                       dr.Item("penjamin"), dr.Item("tglLahir"), dr.Item("noRegistrasiRawatJalan"),
                                       dr.Item("tglDaftar"), dr.Item("jenisKelamin"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Sub DaftarReg()
        Call koneksiServer()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        query = "Call listpasieneklaim('" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Format(DateAdd(DateInterval.Day, 0, DateTimePicker2.Value), "yyyy-MM-dd") & "')"

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()
            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("tglMasukRawatJalan"), dr.Item("tglPulang"), dr.Item("noRekamedis"),
                                       dr.Item("nmPasien"), dr.Item("unit"), dr.Item("noDaftar"),
                                       dr.Item("kelas"), dr.Item("statusKeluar"), dr.Item("carabayar"),
                                       dr.Item("penjamin"), dr.Item("tglLahir"), dr.Item("noRegistrasiRawatJalan"),
                                       dr.Item("tglDaftar"), dr.Item("jenisKelamin"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        TableLayoutPanel2.RowStyles(1).SizeType = SizeType.Absolute
        TableLayoutPanel2.RowStyles(1).Height = 0
        Panel3.Visible = False

        txtUser.Text = Home.txtUser.Text
        btnEklaim.BackColor = Color.FromArgb(209, 232, 223)
        PicCollapse.Visible = False

        DataGridView1.Columns(0).Width = 50
        DataGridView1.Columns(1).Width = 50
        DataGridView1.Columns(2).Width = 50
        DataGridView1.Columns(3).Width = 300
        DataGridView1.Columns(4).Width = 150

        Call DaftarReg()
        Call autoCaraBayar()

        If txtRawat.Text.Equals("Semua", StringComparison.OrdinalIgnoreCase) Then
            txtKelas.Text = "Semua"
            txtKelas.Enabled = False
        ElseIf txtRawat.Text.Equals("Rawat Jalan", StringComparison.OrdinalIgnoreCase) Then
            txtKelas.Text = "Semua"
            txtKelas.Enabled = False
        Else
            txtKelas.Enabled = True
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
        'Dim btn As Button = CType(sender, Button)
        'setColor(btn)
        Home.Show()
        Me.Hide()
    End Sub

    Private Sub btnEklaim_Click(sender As Object, e As EventArgs) Handles btnEklaim.Click
        Dim btn As Button = CType(sender, Button)
        setColor(btn)
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
        TotalRekap.Show()
        Me.Hide()
    End Sub

    Private Sub btnOpsi_Click(sender As Object, e As EventArgs) Handles btnOpsi.Click
        If TableLayoutPanel2.RowStyles(1).Height = 80 Then
            TableLayoutPanel2.RowStyles(1).SizeType = SizeType.Absolute
            TableLayoutPanel2.RowStyles(1).Height = 0
            Panel3.Visible = False
        Else
            TableLayoutPanel2.RowStyles(1).SizeType = SizeType.Absolute
            TableLayoutPanel2.RowStyles(1).Height = 80
            Panel3.Visible = True
            txtRawat.SelectedIndex = 0
            'txtBayar.SelectedIndex = 0
            txtKelas.SelectedIndex = 0
        End If
    End Sub

    Private Sub btnCari1_Click(sender As Object, e As EventArgs) Handles btnCari1.Click
        If txtCari.Text = "" Then
            MsgBox("Masukkan No.RM / Nama Pasien", MsgBoxStyle.Exclamation)
            Me.ErrorProvider1.SetError(Me.txtCari, "Masukkan No.RM / Nama Pasien")
        Else
            Call cariPasien()
        End If
    End Sub

    Private Sub txtCari_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCari.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtCari.Text = "" Then
                Call DaftarReg()
            Else
                Call cariPasien()
            End If
        End If
    End Sub

    Private Sub btnCari2_Click(sender As Object, e As EventArgs) Handles btnCari2.Click
        cari2(txtRawat.Text, txtBayar.Text, txtKelas.Text, DateTimePicker1.Value, DateTimePicker2.Value)
    End Sub

    Private Sub txtRawat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtRawat.SelectedIndexChanged
        If txtRawat.SelectedIndex = 0 Then
            txtKelas.SelectedIndex = 0
        End If

        If txtRawat.Text.Equals("Semua", StringComparison.OrdinalIgnoreCase) Then
            txtKelas.Text = "Semua"
            txtKelas.Enabled = False
        ElseIf txtRawat.Text.Equals("Rawat Jalan", StringComparison.OrdinalIgnoreCase) Then
            txtKelas.Text = "Semua"
            txtKelas.Enabled = False
        Else
            txtKelas.Enabled = True
        End If
    End Sub

    Private Sub txtKelas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtKelas.SelectedIndexChanged
        If txtKelas.Text = "Kelas 1" Then
            subKelas = "Kelas I"
        ElseIf txtKelas.Text = "Kelas 2" Then
            subKelas = "Kelas II"
        ElseIf txtKelas.Text = "Kelas 3" Then
            subKelas = "Kelas III"
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        DataGridView1.Columns(0).DefaultCellStyle.Format = "dd MMM yyyy"
        DataGridView1.Columns(1).DefaultCellStyle.Format = "dd MMM yyyy"
        DataGridView1.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView1.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView1.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView1.DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        DataGridView1.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
        DataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
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

    Private Sub picKeluar_Click(sender As Object, e As EventArgs) Handles picKeluar.Click
        Dim konfirmasi As MsgBoxResult

        konfirmasi = MsgBox("Apakah anda yakin ingin keluar..?", vbQuestion + vbYesNo, "Konfirmasi")
        If konfirmasi = vbYes Then
            Me.Close()
            LoginForm.Show()
        End If
    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        If e.RowIndex = -1 Then
            Return
        End If

        tglMasuk = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString
        tglKeluar = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString
        noRm = DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString
        nmPasien = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString
        unit = DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString
        noDaftar = DataGridView1.Rows(e.RowIndex).Cells(5).Value.ToString
        kelas = DataGridView1.Rows(e.RowIndex).Cells(6).Value.ToString
        statusKeluar = DataGridView1.Rows(e.RowIndex).Cells(7).Value.ToString
        carabayar = DataGridView1.Rows(e.RowIndex).Cells(8).Value.ToString
        penjamin = DataGridView1.Rows(e.RowIndex).Cells(9).Value.ToString
        tglLahir = DataGridView1.Rows(e.RowIndex).Cells(10).Value.ToString
        regUnit = DataGridView1.Rows(e.RowIndex).Cells(11).Value.ToString
        tglDaftar = DataGridView1.Rows(e.RowIndex).Cells(12).Value.ToString
        jk = DataGridView1.Rows(e.RowIndex).Cells(13).Value.ToString

        Me.Hide()
        Eklaim.Show()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex = -1 Then
            Return
        End If

        tglMasuk = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString
        tglKeluar = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString
        noRm = DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString
        nmPasien = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString
        unit = DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString
        noDaftar = DataGridView1.Rows(e.RowIndex).Cells(5).Value.ToString
        kelas = DataGridView1.Rows(e.RowIndex).Cells(6).Value.ToString
        statusKeluar = DataGridView1.Rows(e.RowIndex).Cells(7).Value.ToString
        carabayar = DataGridView1.Rows(e.RowIndex).Cells(8).Value.ToString
        penjamin = DataGridView1.Rows(e.RowIndex).Cells(9).Value.ToString
        tglLahir = DataGridView1.Rows(e.RowIndex).Cells(10).Value.ToString
        regUnit = DataGridView1.Rows(e.RowIndex).Cells(11).Value.ToString
        tglDaftar = DataGridView1.Rows(e.RowIndex).Cells(12).Value.ToString
        jk = DataGridView1.Rows(e.RowIndex).Cells(13).Value.ToString

        Me.Hide()
        Eklaim.Show()
    End Sub

    Private Sub btnCari1_MouseDown(sender As Object, e As MouseEventArgs) Handles btnCari1.MouseDown
        btnCari1.ForeColor = Color.White
        btnCari1.Image = My.Resources.magnifying_glass16
    End Sub

    Private Sub btnCari2_MouseDown(sender As Object, e As MouseEventArgs) Handles btnCari2.MouseDown
        btnCari2.ForeColor = Color.White
        btnCari2.Image = My.Resources.magnifying_glass16
    End Sub

    Private Sub btnOpsi_MouseDown(sender As Object, e As MouseEventArgs) Handles btnOpsi.MouseDown
        btnOpsi.ForeColor = Color.White
    End Sub

    Private Sub btnCari1_MouseUp(sender As Object, e As MouseEventArgs) Handles btnCari1.MouseUp
        btnCari1.ForeColor = Color.FromArgb(26, 141, 95)
        btnCari1.Image = My.Resources.magnifying_glass_green
    End Sub

    Private Sub btnCari2_MouseUp(sender As Object, e As MouseEventArgs) Handles btnCari2.MouseUp
        btnCari2.ForeColor = Color.FromArgb(26, 141, 95)
        btnCari2.Image = My.Resources.magnifying_glass_green
    End Sub

    Private Sub btnOpsi_MouseUp(sender As Object, e As MouseEventArgs) Handles btnOpsi.MouseUp
        btnOpsi.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub bgwCari2_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwCari2.DoWork
        'cari2(txtRawat.Text, txtBayar.Text, txtKelas.Text, DateTimePicker1.Value, DateTimePicker2.Value)
    End Sub
End Class
