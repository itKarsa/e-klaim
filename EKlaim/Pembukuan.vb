Imports MySql.Data.MySqlClient
Imports Excel = Microsoft.Office.Interop.Excel
Public Class Pembukuan

    Sub setColor(button As Button)
        btnHome.BackColor = Color.White
        btnEklaim.BackColor = Color.White
        btnBuku.BackColor = Color.White
        btnPiutang.BackColor = Color.White
        button.BackColor = Color.FromArgb(209, 232, 223)
    End Sub

    Sub DaftarKlaimPulang()
        Call koneksiServer()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        query = "SELECT px.noEklaim,px.tglMasuk,px.tglPulang,px.noSEP,
                        px.noRekamMedis,px.namaPasien,px.dpjp,px.unit,
                        detail.totalTarif,detail.statusKlaim,px.dateFinal,
                        detail.nonBedah,detail.bedah,detail.konsultasi,
                        detail.tenagaAhli,detail.keperawatan,detail.penunjang,
                        detail.radiologi,detail.laboratorium,detail.bdrs,
                        detail.rehab,detail.akomodasi,detail.intensif,
                        detail.obat,detail.obatKronis,detail.obatKemo,
                        detail.alkes,detail.bmhp,detail.sewa
                   FROM t_eklaimpasien AS px
             INNER JOIN t_eklaimdetailtarif AS detail ON px.noEklaim = detail.noEklaim
                  WHERE (SUBSTR(px.tglPulang,1,10)) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()
            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("noEklaim"), dr.Item("tglMasuk"), dr.Item("tglPulang"), dr.Item("noSEP"),
                                       dr.Item("noRekamMedis"), dr.Item("namaPasien"), dr.Item("dpjp"), dr.Item("unit"),
                                       dr.Item("totalTarif"), dr.Item("statusKlaim"), dr.Item("dateFinal"),
                                       dr.Item("nonBedah"), dr.Item("bedah"), dr.Item("konsultasi"),
                                       dr.Item("tenagaAhli"), dr.Item("keperawatan"), dr.Item("penunjang"),
                                       dr.Item("radiologi"), dr.Item("laboratorium"), dr.Item("bdrs"),
                                       dr.Item("rehab"), dr.Item("akomodasi"), dr.Item("intensif"),
                                       dr.Item("obat"), dr.Item("obatKronis"), dr.Item("obatKemo"),
                                       dr.Item("alkes"), dr.Item("bmhp"), dr.Item("sewa"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Sub DaftarKlaim()
        Call koneksiServer()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        query = "SELECT px.noEklaim,px.tglMasuk,px.tglPulang,px.noSEP,
                        px.noRekamMedis,px.namaPasien,px.dpjp,px.unit,
                        detail.totalTarif,detail.statusKlaim,px.dateFinal,
                        detail.nonBedah,detail.bedah,detail.konsultasi,
                        detail.tenagaAhli,detail.keperawatan,detail.penunjang,
                        detail.radiologi,detail.laboratorium,detail.bdrs,
                        detail.rehab,detail.akomodasi,detail.intensif,
                        detail.obat,detail.obatKronis,detail.obatKemo,
                        detail.alkes,detail.bmhp,detail.sewa
                   FROM t_eklaimpasien AS px
             INNER JOIN t_eklaimdetailtarif AS detail ON px.noEklaim = detail.noEklaim
                  WHERE (SUBSTR(px.dateFinal,1,10)) = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "'"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()
            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("noEklaim"), dr.Item("tglMasuk"), dr.Item("tglPulang"), dr.Item("noSEP"),
                                       dr.Item("noRekamMedis"), dr.Item("namaPasien"), dr.Item("dpjp"), dr.Item("unit"),
                                       dr.Item("totalTarif"), dr.Item("statusKlaim"), dr.Item("dateFinal"),
                                       dr.Item("nonBedah"), dr.Item("bedah"), dr.Item("konsultasi"),
                                       dr.Item("tenagaAhli"), dr.Item("keperawatan"), dr.Item("penunjang"),
                                       dr.Item("radiologi"), dr.Item("laboratorium"), dr.Item("bdrs"),
                                       dr.Item("rehab"), dr.Item("akomodasi"), dr.Item("intensif"),
                                       dr.Item("obat"), dr.Item("obatKronis"), dr.Item("obatKemo"),
                                       dr.Item("alkes"), dr.Item("bmhp"), dr.Item("sewa"))
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Private Sub Pembukuan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        btnBuku.BackColor = Color.FromArgb(209, 232, 223)
        PicCollapse.Visible = False

        txtUser.Text = Home.txtUser.Text
        DateTimePicker1.Value = DateTime.Now
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "dd MMM yyyy"
        txtFilter.SelectedIndex = 0

        Call DaftarKlaimPulang()
        txtJmlKlaim.Text = "(" & DataGridView1.Rows.Count & " Klaim)"
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
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub btnBuku_Click(sender As Object, e As EventArgs) Handles btnBuku.Click
        Dim btn As Button = CType(sender, Button)
        setColor(btn)
    End Sub

    Private Sub btnPiutang_Click(sender As Object, e As EventArgs) Handles btnPiutang.Click
        RekapPiutang.Show()
        Me.Hide()
    End Sub

    Private Sub btnUmum_Click(sender As Object, e As EventArgs) Handles btnUmum.Click
        RekapPiutangUmum.Show()
        Me.Hide()
    End Sub

    Private Sub picKeluar_Click(sender As Object, e As EventArgs) Handles picKeluar.Click
        Dim konfirmasi As MsgBoxResult

        konfirmasi = MsgBox("Apakah anda yakin ingin keluar..?", vbQuestion + vbYesNo, "Konfirmasi")
        If konfirmasi = vbYes Then
            Me.Close()
            LoginForm.Show()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim tglFile As String
        tglFile = DateTime.Now.ToString("dd MMM yyyy")

        Try
            Dim ExcelApp As Microsoft.Office.Interop.Excel.Application
            Dim ExcelWorkBook As Microsoft.Office.Interop.Excel.Workbook
            Dim ExcelWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim a As Integer
            Dim b As Integer

            ExcelApp = New Microsoft.Office.Interop.Excel.Application
            ExcelWorkBook = ExcelApp.Workbooks.Add(misValue)
            ExcelWorkSheet = ExcelWorkBook.Sheets("sheet1")

            For a = 0 To DataGridView1.RowCount - 1
                For b = 0 To DataGridView1.ColumnCount - 1
                    For c As Integer = 1 To DataGridView1.ColumnCount
                        ExcelWorkSheet.Cells(1, c) = DataGridView1.Columns(c - 1).HeaderText
                    Next
                    ExcelWorkSheet.Cells(a + 2, b + 1) = DataGridView1(b, a).Value.ToString()
                Next
            Next

            ExcelWorkSheet.SaveAs("C:\EKlaim\Klaim-" & tglFile & ".xlsx")
            ExcelWorkBook.Close()
            ExcelApp.Quit()

            releaseObject(ExcelApp)
            releaseObject(ExcelWorkBook)
            releaseObject(ExcelWorkSheet)

            MsgBox("Hasil export tersimpan di C:\EKlaim, dengan nama Klaim-" & tglFile & ".xlsx")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try

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

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If txtFilter.Text = "Tanggal Pulang" Then
            Call DaftarKlaimPulang()
            txtJmlKlaim.Text = "(" & DataGridView1.Rows.Count & " Klaim)"
        ElseIf txtFilter.Text = "Tanggal Klaim" Then
            Call DaftarKlaim()
            txtJmlKlaim.Text = "(" & DataGridView1.Rows.Count & " Klaim)"
        End If
    End Sub

    Private Sub txtFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtFilter.SelectedIndexChanged
        If txtFilter.Text = "Tanggal Pulang" Then
            Call DaftarKlaimPulang()
            txtJmlKlaim.Text = "(" & DataGridView1.Rows.Count & " Klaim)"
        ElseIf txtFilter.Text = "Tanggal Klaim" Then
            Call DaftarKlaim()
            txtJmlKlaim.Text = "(" & DataGridView1.Rows.Count & " Klaim)"
        End If
    End Sub

End Class