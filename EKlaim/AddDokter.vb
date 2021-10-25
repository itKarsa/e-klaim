Imports System.ComponentModel
Imports MySql.Data.MySqlClient
Public Class AddDokter
    Public Ambil_Data As String
    Public Form_Ambil_Data As String
    Dim t, j As Integer

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

        txtDokter.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtDokter.AutoCompleteCustomSource = col
        txtDokter.AutoCompleteMode = AutoCompleteMode.Suggest

        conn.Close()
    End Sub

    Private Sub AddDokter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call autoDokter()
        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "VisiteRanap"
                    Label4.Text = "Dokter Visite Rawat Inap"
                Case "KonsulRanap"
                    Label4.Text = "Dokter Konsultasi/Rawat Bersama"
            End Select
        End If
    End Sub

    Private Sub txtJml_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJml.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtTotal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotal.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtJml_TextChanged(sender As Object, e As EventArgs) Handles txtJml.TextChanged
        If txtJml.Text = "" Then
            txtJml.Text = 0
        End If
        j = txtJml.Text
        txtJml.Text = FormatNumber(Val(j), 0)
        txtJml.SelectionStart = Len(txtJml.Text)
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "VisiteRanap"
                    Berakdown.dgvDrVisite.Rows.Add(1)
                    Berakdown.dgvDrVisite.Rows.Item(Berakdown.dgvDrVisite.RowCount - 1).Cells(0).Value = txtDokter.Text
                    Berakdown.dgvDrVisite.Rows.Item(Berakdown.dgvDrVisite.RowCount - 1).Cells(1).Value = txtJml.Text
                    Berakdown.dgvDrVisite.Rows.Item(Berakdown.dgvDrVisite.RowCount - 1).Cells(2).Value = txtTotal.Text
                    Berakdown.dgvDrVisite.Update()
                    Berakdown.totalTarifVisite()
                    Me.Close()
                Case "KonsulRanap"
                    Berakdown.dgvDrKonsulRanap.Rows.Add(1)
                    Berakdown.dgvDrKonsulRanap.Rows.Item(Berakdown.dgvDrKonsulRanap.RowCount - 1).Cells(0).Value = txtDokter.Text
                    Berakdown.dgvDrKonsulRanap.Rows.Item(Berakdown.dgvDrKonsulRanap.RowCount - 1).Cells(1).Value = txtJml.Text
                    Berakdown.dgvDrKonsulRanap.Rows.Item(Berakdown.dgvDrKonsulRanap.RowCount - 1).Cells(2).Value = txtTotal.Text
                    Berakdown.dgvDrKonsulRanap.Update()
                    Berakdown.totalTarifKonsul()
                    Me.Close()
                Case "KonsulIgd"
                    Berakdown.dgvDrIgdKonsul.Rows.Add(1)
                    Berakdown.dgvDrIgdKonsul.Rows.Item(Berakdown.dgvDrIgdKonsul.RowCount - 1).Cells(0).Value = txtDokter.Text
                    Berakdown.dgvDrIgdKonsul.Rows.Item(Berakdown.dgvDrIgdKonsul.RowCount - 1).Cells(1).Value = txtJml.Text
                    Berakdown.dgvDrIgdKonsul.Rows.Item(Berakdown.dgvDrIgdKonsul.RowCount - 1).Cells(2).Value = txtTotal.Text
                    Berakdown.dgvDrIgdKonsul.Update()
                    Berakdown.totalTarifIgdKonsul()
                    Me.Close()
            End Select
        End If
    End Sub

    Private Sub txtTotal_TextChanged(sender As Object, e As EventArgs) Handles txtTotal.TextChanged
        If txtTotal.Text = "" Then
            txtTotal.Text = 0
        End If
        t = txtTotal.Text
        txtTotal.Text = Format(Val(t), "#,##0")
        txtTotal.SelectionStart = Len(txtTotal.Text)
    End Sub

    Private Sub AddDokter_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        txtDokter.Text = "-"
        txtJml.Text = 0
        txtTotal.Text = 0
    End Sub
End Class