Public Class LoadingScreen
    Private Sub LoadingScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Sub DoHeavyWork()
        System.Threading.Thread.Sleep(100)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = ">" Then
            If Not BackgroundWorker1.IsBusy = True Then
                BackgroundWorker1.RunWorkerAsync()
            End If
        ElseIf Button1.Text = "| |" Then
            If BackgroundWorker1.WorkerSupportsCancellation = True Then
                BackgroundWorker1.CancelAsync()
            End If
        End If

    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        For i As Integer = 0 To 100
            If BackgroundWorker1.CancellationPending = True Then
                e.Cancel = True
                Exit For
            Else
                DoHeavyWork()
                BackgroundWorker1.ReportProgress(i)
            End If
        Next
    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        Label1.Text = "Loading ... " + e.ProgressPercentage.ToString() + "%"
        Button1.Text = "| |"
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If e.Cancelled = True Then
            MessageBox.Show("Cancelled")
            ProgressBar1.Value = 0
            Button1.Text = ">"
        ElseIf e.Error IsNot Nothing Then
            MessageBox.Show(e.Error.Message)
        Else
            MessageBox.Show("Done")
            Button1.Text = ">"
        End If
    End Sub
End Class