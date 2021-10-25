Public Class Home

    Sub setColor(button As Button)
        btnHome.BackColor = Color.White
        btnEklaim.BackColor = Color.White
        btnBuku.BackColor = Color.White
        btnPiutang.BackColor = Color.White
        btnUmum.BackColor = Color.White
        button.BackColor = Color.FromArgb(209, 232, 223)
    End Sub

    Private Sub Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        btnHome.BackColor = Color.FromArgb(209, 232, 223)
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
        Dim btn As Button = CType(sender, Button)
        setColor(btn)
    End Sub

    Private Sub btnEklaim_Click(sender As Object, e As EventArgs) Handles btnEklaim.Click
        'Dim btn As Button = CType(sender, Button)
        'setColor(btn)
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SettingIP.ShowDialog()
    End Sub
End Class