Imports System.Net.NetworkInformation
Imports System.Security.Cryptography
Imports System.Text
Public Class SettingIP
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If TextBox1.Text <> "" Then
                My.Settings.ipServer = TextBox1.Text
                My.Settings.Save()
                MsgBox("Konfigurasi Server Tersimpan!", MsgBoxStyle.Information, MsgBoxResult.Ok)
                'Application.Restart()
                Me.Close()
            Else
                MsgBox("Tolong diisi terlebih dahulu !", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub SettingIP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Settings.Reload()
        TextBox1.Text = My.Settings.ipServer
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim ping As New Ping
        Dim reply As PingReply = ping.Send(TextBox1.Text, 1000)

        If reply.Status.ToString.Contains("TimedOut") Then
            MessageBox.Show(reply.Status.ToString, "Connecting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Button1.Enabled = False
        Else
            MessageBox.Show(reply.Status.ToString, "Connecting", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Button1.Enabled = True
        End If

    End Sub
End Class