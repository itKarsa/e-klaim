Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class WebService

    Dim EncrypKey As String = "b9192b9f14c33f39153ef32f12dd68fa61eec2f3df34e2b96c24c6078dba568a"

    Sub NewPasien(noBpjs As String, noSep As String, noRm As String, nama As String, tglLahir As String, jk As String)
        Dim payload As String = ""
        Dim jsonQuery As String = ""
        Dim req As String = ""
        Dim response As String
        jsonQuery = "{
                        ""metadata"": {
                            ""method"": ""new_claim""
                        },
                        ""data"": {
                            ""nomor_kartu"": """ & noBpjs & """,
                            ""nomor_sep"": """ & noSep & """,
                            ""nomor_rm"": """ & noRm & """,
                            ""nama_pasien"": """ & nama & """,
                            ""tgl_lahir"": """ & tglLahir & """,
                            ""gender"": """ & jk & """
                        }
                    }"
        payload = inacbg_encrypt(jsonQuery, EncrypKey)
        req = reqPost(payload)
        response = inacbg_decrypt(req, EncrypKey)
        MsgBox(response)
    End Sub

    Sub UpdatePasien(noBpjs As String, noRm As String, nama As String, tglLahir As String, jk As String)
        Dim payload As String = ""
        Dim jsonQuery As String = ""
        Dim req As String = ""
        Dim response As String
        jsonQuery = "{
                        ""metadata"": {
                            ""method"": ""update_patient"",
                            ""nomor_rm"": """ & noRm & """
                        },
                        ""data"": {
                            ""nomor_kartu"": """ & noBpjs & """,
                            ""nomor_rm"": """ & noRm & """,
                            ""nama_pasien"": """ & nama & """,
                            ""tgl_lahir"": """ & tglLahir & """,
                            ""gender"": """ & jk & """
                        }
                    }"
        payload = inacbg_encrypt(jsonQuery, EncrypKey)
        req = reqPost(payload)
        response = inacbg_decrypt(req, EncrypKey)
        MsgBox(response)
    End Sub

    Private Sub WebService_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'payload = inacbg_encrypt(jsonQuery, TextBox1.Text)
        'RichTextBox1.Text = reqPost(payload)
        'response = inacbg_decrypt(RichTextBox1.Text, TextBox1.Text)
        ''RichTextBox2.Text = response

        'Dim result As preferenceModel = JsonConvert.DeserializeObject(Of preferenceModel)(response)
        ''Dim i As Integer = 0
        'RichTextBox2.Text = result.data(0)

        Call NewPasien(txtBpjs.Text, txtSep.Text, txtRm.Text, txtNama.Text, txtTglLahir.Text, txtJk.Text)

    End Sub
End Class