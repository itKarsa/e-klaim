﻿Imports MySql.Data.MySqlClient
Public Class LoginForm

    Private Sub txtUsername_GotFocus(sender As Object, e As EventArgs) Handles txtUsername.GotFocus
        txtUsername.ForeColor = Color.FromArgb(26, 141, 95)
        txtUsername.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        If txtUsername.Text.Equals("Username", StringComparison.OrdinalIgnoreCase) Then
            txtUsername.Text = String.Empty
        End If
    End Sub

    Private Sub txtUsername_LostFocus(sender As Object, e As EventArgs) Handles txtUsername.LostFocus
        txtUsername.ForeColor = Color.FromArgb(26, 141, 95)
        txtUsername.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        If String.IsNullOrEmpty(txtUsername.Text) Then
            txtUsername.Text = "Username"
        End If
    End Sub

    Private Sub txtPass_GotFocus(sender As Object, e As EventArgs) Handles txtPass.GotFocus
        txtPass.ForeColor = Color.FromArgb(26, 141, 95)
        txtPass.Font = New Font("Segoe UI", 12)
        If txtPass.Text.Equals("Password", StringComparison.OrdinalIgnoreCase) Then
            txtPass.Text = String.Empty
        End If
    End Sub

    Private Sub txtPass_LostFocus(sender As Object, e As EventArgs) Handles txtPass.LostFocus
        txtPass.ForeColor = Color.FromArgb(26, 141, 95)
        txtPass.Font = New Font("Segoe UI", 12)
        If String.IsNullOrEmpty(txtPass.Text) Then
            txtPass.Text = "Password"
        End If
    End Sub

    Private Sub btnLogin_MouseEnter(sender As Object, e As EventArgs) Handles btnLogin.MouseEnter
        btnLogin.BackgroundImage = My.Resources.btn_greenv2
    End Sub

    Private Sub btnLogin_MouseLeave(sender As Object, e As EventArgs) Handles btnLogin.MouseLeave
        btnLogin.BackgroundImage = My.Resources.btn_green
    End Sub

    Private Sub txtUsername_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUsername.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Call koneksiServer()
            Dim str As String
            str = "SELECT
	                    t_pemakai.username,
	                    t_pemakai.password,
	                    t_aksesmenu.namaUser 
                    FROM
	                    t_aksesmenu
	                    INNER JOIN t_pemakai ON t_pemakai.username = t_aksesmenu.username 
                    WHERE t_pemakai.username = '" & txtUsername.Text & "' 
                      AND t_pemakai.password = '" & txtPass.Text & "'
                      AND t_aksesmenu.loket_unit = 'LOKET KASIR RI'"
            cmd = New MySqlCommand(str, conn)
            dr = cmd.ExecuteReader
            If dr.HasRows Then
                'MessageBox.Show("Login berhasil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                While dr.Read
                    txtlevel.Text = dr.GetString("namaUser")
                End While
                Home.Show()
                Home.txtUser.Text = txtlevel.Text
                Me.Hide()
            Else
                dr.Close()
                MessageBox.Show("Login gagal, username atau Password salah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtPass.Text = ""
                txtUsername.Text = ""
                txtUsername.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtPass_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPass.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                Call koneksiServer()
                Dim str As String
                str = "SELECT
	                    t_pemakai.username,
	                    t_pemakai.password,
	                    t_aksesmenu.namaUser 
                    FROM
	                    t_aksesmenu
	                    INNER JOIN t_pemakai ON t_pemakai.username = t_aksesmenu.username 
                    WHERE t_pemakai.username = '" & txtUsername.Text & "' 
                      AND t_pemakai.password = '" & txtPass.Text & "'
                      AND t_aksesmenu.loket_unit = 'LOKET KASIR RI'"
                cmd = New MySqlCommand(str, conn)
                dr = cmd.ExecuteReader
                If dr.HasRows Then
                    'MessageBox.Show("Login berhasil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    While dr.Read
                        txtlevel.Text = dr.GetString("namaUser")
                    End While
                    Home.Show()
                    Home.txtUser.Text = txtlevel.Text
                    Me.Hide()
                Else
                    dr.Close()
                    MessageBox.Show("Login gagal, username atau Password salah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtPass.Text = ""
                    txtUsername.Text = ""
                    txtUsername.Focus()
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class