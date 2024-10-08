﻿Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Security.Cryptography
Module inacbg_encryption
    ' ENCRYPT
    Public Function inacbg_encrypt(text As String, key As String) As String
        Dim keys = Encoding.[Default].GetBytes(hex2bin(key))
        Dim aes As New AesCryptoServiceProvider()
        aes.BlockSize = 128
        aes.KeySize = 256
        aes.GenerateIV()
        Dim iv = aes.IV
        aes.Key = keys
        aes.Mode = CipherMode.CBC
        aes.Padding = PaddingMode.PKCS7
        Dim src As Byte() = Encoding.[Default].GetBytes(text)
        Using enc As ICryptoTransform = aes.CreateEncryptor()
            Dim data As Byte() = enc.TransformFinalBlock(src, 0, src.Length)
            Dim hashObject As New HMACSHA256(keys)
            Dim hash_sign = hashObject.ComputeHash(data)
            Dim signature As Byte() = New Byte(9) {}
            Array.Copy(hash_sign, 0, signature, 0, 10)
            Dim ret As Byte() = New Byte(signature.Length + iv.Length + (data.Length - 1)) {}
            Array.Copy(signature, 0, ret, 0, signature.Length)
            Array.Copy(iv, 0, ret, signature.Length, iv.Length)
            Array.Copy(data, 0, ret, signature.Length + iv.Length, data.Length)
            Return Convert.ToBase64String(ret)
        End Using
    End Function
    ' DECRYPT
    Public Function inacbg_decrypt(strencrypt As String, key As String) As String
        Dim encoded_str As String = strencrypt
        Dim chiper As Byte() = Convert.FromBase64String(encoded_str)
        Dim length = chiper.Length
        Dim new_byte_iv As Byte() = New Byte(15) {}
        Dim new_byte_msg As Byte() = New Byte(length - 27) {}
        Array.Copy(chiper, 10, new_byte_iv, 0, 16)
        Array.Copy(chiper, 26, new_byte_msg, 0, length - 26)
        Dim byte_key As Byte() = Encoding.[Default].GetBytes(hex2bin(key))
        Dim aes As New RijndaelManaged()
        aes.KeySize = 256
        aes.BlockSize = 128
        aes.Padding = PaddingMode.PKCS7
        aes.Mode = CipherMode.CBC
        aes.Key = byte_key
        aes.IV = new_byte_iv
        Dim AESDecrypt As ICryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV)
        Return Encoding.[Default].GetString(AESDecrypt.TransformFinalBlock(new_byte_msg,
                                            0, new_byte_msg.Length))
    End Function

    Private Function hex2bin(input As String) As String
        input = input.Replace("-", "")
        Dim raw As Byte() = New Byte(input.Length / 2 - 1) {}
        For i As Integer = 0 To raw.Length - 1
            raw(i) = Convert.ToByte(input.Substring(i * 2, 2), 16)
        Next
        Return Encoding.[Default].GetString(raw)
    End Function

    Function reqPost(strEncryp As String)
        Dim myReq As HttpWebRequest
        Dim myResp As HttpWebResponse
        Dim myResponse As String
        myReq = HttpWebRequest.Create("http://" & My.Settings.ipServer & "/E-Klaim/ws.php")

        myReq.Method = "POST"
        myReq.ContentType = "application/json"
        myReq.Headers.Add("Authorization", "Basic " & Convert.ToBase64String(Encoding.UTF8.GetBytes("test:test")))
        Dim myData As String = strEncryp
        myReq.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(myData), 0, System.Text.Encoding.UTF8.GetBytes(myData).Count)
        myResp = myReq.GetResponse
        Dim myreader As New System.IO.StreamReader(myResp.GetResponseStream)
        Dim myText As String
        myText = myreader.ReadToEnd
        myResponse = myText.Substring(30, myText.Length - 30 - 30)
        Return myResponse
    End Function
End Module
