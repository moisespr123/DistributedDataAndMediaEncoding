Public Class Form1

    Private Sub Form1_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub
    Private Sub Form1_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        Dim input As String = CType(e.Data.GetData(DataFormats.FileDrop), String())(0)
        If IO.Directory.Exists(input) Then
            ivfDirectoryTxtBox.Text = input
        ElseIf IO.Path.GetExtension(input) = ".opus" Then
            OpusFileTxtBox.Text = input
        ElseIf IO.Path.GetExtension(input) = ".txt" Then
            ConcatTxtFileTxtBox.Text = input
        End If
    End Sub

    Private Sub BrowseOutputBtn_Click(sender As Object, e As EventArgs) Handles BrowseOutputBtn.Click
        OutputFileDialog.Title = "Browse for a path to save the resulting AV1 file"
        OutputFileDialog.Filter = "webm|.webm"
        If Not String.IsNullOrWhiteSpace(OutputFileTxtBox.Text) Then OutputFileDialog.FileName = IO.Path.GetFileName(OutputFileTxtBox.Text)
        Dim result As DialogResult = OutputFileDialog.ShowDialog
        If result = DialogResult.OK Then
            OutputFileTxtBox.Text = OutputFileDialog.FileName
        End If
    End Sub

    Private Sub StartBtn_Click(sender As Object, e As EventArgs) Handles StartBtn.Click
        If String.IsNullOrWhiteSpace(ivfDirectoryTxtBox.Text) Then
            MsgBox(".ivf directory is empty")
        ElseIf String.IsNullOrWhiteSpace(OpusFileTxtBox.Text) Then
            MsgBox("No .opus file was specified")
        ElseIf String.IsNullOrWhiteSpace(ConcatTxtFileTxtBox.Text) Then
            MsgBox("No concatenation file specified")
        Else
            Merge(ivfDirectoryTxtBox.Text, OpusFileTxtBox.Text, ConcatTxtFileTxtBox.Text, OutputFileTxtBox.Text)
        End If
    End Sub
    Private Sub Merge(ivf As String, opus As String, txt As String, output As String)
        IO.Directory.SetCurrentDirectory(ivf)
        If concatenate_video_files(ivf, txt) Then
            If merge_audio_video(ivf, opus, output) Then
                IO.File.Delete(ivf + "/temp.ivf")
                MsgBox("Merge successful!")
            End If
        End If
    End Sub
    Private Function concatenate_video_files(ivfDir As String, concatFile As String)
        Dim ffmpegProcessInfo As New ProcessStartInfo
        Dim ffmpegProcess As Process
        ffmpegProcessInfo.FileName = "ffmpeg.exe"
        ffmpegProcessInfo.Arguments = "-f concat -safe 0 -i """ + concatFile + """ -c copy """ + ivfDir + "\temp.ivf"" -y"
        ffmpegProcessInfo.CreateNoWindow = True
        ffmpegProcessInfo.RedirectStandardOutput = False
        ffmpegProcessInfo.UseShellExecute = False
        ffmpegProcess = Process.Start(ffmpegProcessInfo)
        ffmpegProcess.WaitForExit()
        Return True
    End Function
    Private Function merge_audio_video(ivfDir As String, OpusFile As String, output As String)
        Dim ffmpegProcessInfo As New ProcessStartInfo
        Dim ffmpegProcess As Process
        ffmpegProcessInfo.FileName = "ffmpeg.exe"
        ffmpegProcessInfo.Arguments = "-i """ + ivfDir + "\temp.ivf"" -i """ + OpusFile + """ -c:v copy -c:a copy """ + output + """ -y"
        ffmpegProcessInfo.CreateNoWindow = True
        ffmpegProcessInfo.RedirectStandardOutput = False
        ffmpegProcessInfo.UseShellExecute = False
        ffmpegProcess = Process.Start(ffmpegProcessInfo)
        ffmpegProcess.WaitForExit()
        Return True
    End Function
End Class
