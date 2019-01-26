Imports System.Net.Http

Public Class Form1
    Private GUILoaded As Boolean = False
    Private Sub InputBrowseBtn_Click(sender As Object, e As EventArgs) Handles InputBrowseBtn.Click
        Dim InputBrowser As New OpenFileDialog With {
            .Title = "Browse for a video file",
            .FileName = "",
            .Filter = "All Files|*.*"
        }
        Dim OkAction As MsgBoxResult = InputBrowser.ShowDialog
        If OkAction = MsgBoxResult.Ok Then
            InputTxt.Text = InputBrowser.FileName
        End If
    End Sub

    Private Sub OutputBrowseBtn_Click(sender As Object, e As EventArgs)
        Dim OutputBrowser As New SaveFileDialog With {
            .Title = "Save Video File",
            .FileName = "",
            .Filter = "WebM|*.webm"
        }
        Dim OkAction As MsgBoxResult = OutputBrowser.ShowDialog
        If OkAction = MsgBoxResult.Ok Then
            OutputTxt.Text = OutputBrowser.FileName
        End If
    End Sub

    Private Sub StartBtn_Click(sender As Object, e As EventArgs) Handles StartBtn.Click
        StartBtn.Enabled = False
        InputTxt.Enabled = False
        OutputTxt.Enabled = False
        InputBrowseBtn.Enabled = False
        audioBitrate.Enabled = False
        quantizer.Enabled = False
        speed.Enabled = False
        keyFrameInterval.Enabled = False
        tempLocationPath.Enabled = False
        BrowseTempLocation.Enabled = False
        Dim StartTasks As New Threading.Thread(Sub() StartThreads())
        StartTasks.Start()
    End Sub
    Private Sub StartThreads()
        If split_video_file(InputTxt.Text, OutputTxt.Text, tempLocationPath.Text) Then
            If extract_audio(InputTxt.Text, IO.Directory.GetCurrentDirectory + "\" + OutputTxt.Text) Then
                Dim ItemsToProcess As List(Of String) = New List(Of String)
                For Each File As String In IO.Directory.GetFiles(tempLocationPath.Text)
                    If IO.Path.GetExtension(File) = ".y4m" And File.Contains(OutputTxt.Text + "-part-") Then
                        ItemsToProcess.Add(File)
                    End If
                Next
                ItemsToProcess.Sort()
                ProgressBar1.BeginInvoke(Sub()
                                             ProgressBar1.Maximum = ItemsToProcess.Count
                                             ProgressBar1.Value = 0
                                         End Sub)
                Dim streamWriter As New IO.StreamWriter(IO.Directory.GetCurrentDirectory + "\" + OutputTxt.Text + "-concatenate-list.txt")
                For Counter As Integer = 0 To ItemsToProcess.Count - 1
                    Dim commandLine As String = IO.Path.GetFileName(ItemsToProcess(Counter)) + " -o " + IO.Path.GetFileNameWithoutExtension(ItemsToProcess(Counter)) + ".ivf --quantizer " + quantizer.Value.ToString() + " -s " + speed.Value.ToString() + " --low_latency false  -i " + keyFrameInterval.Value.ToString() + " -I " + keyFrameInterval.Value.ToString()
                    streamWriter.WriteLine("file '" + IO.Path.GetFileNameWithoutExtension(ItemsToProcess(Counter)) + ".ivf" + "'")
                    Upload(UserKey.Text, commandLine, IO.Path.GetFileNameWithoutExtension(ItemsToProcess(Counter)), OutputTxt.Text)
                    ProgressBar1.BeginInvoke(Sub() ProgressBar1.PerformStep())
                Next
                streamWriter.Close()
                Run_opus(My.Settings.bitrate, IO.Directory.GetCurrentDirectory + "\" + OutputTxt.Text)
                StartBtn.BeginInvoke(Sub()
                                         StartBtn.Enabled = True
                                         audioBitrate.Enabled = True
                                         quantizer.Enabled = True
                                         speed.Enabled = True
                                         keyFrameInterval.Enabled = True
                                         tempLocationPath.Enabled = True
                                         BrowseTempLocation.Enabled = True
                                         OutputTxt.Enabled = True
                                         InputTxt.Enabled = True
                                         InputBrowseBtn.Enabled = True
                                     End Sub)
                MsgBox("Finished")
            End If
        End If
    End Sub
    Private Function Upload(key As String, commandLine As String, filename As String, name As String)
        Dim client As New HttpClient()
        Dim formData As New MultipartFormDataContent
        formData.Add(New StringContent(key), "k")
        formData.Add(New StringContent(name), "n")
        formData.Add(New StringContent(commandLine), "c")
        formData.Add(New StringContent(filename), "f")
        Dim Uri As New Uri("http://boinc.moisescardona.me/av1_put.php")
        Dim response As HttpResponseMessage = client.PostAsync(Uri, formData).Result
        If Not response.IsSuccessStatusCode Then

            Console.WriteLine("Error")
        End If
        Dim reader As New IO.StreamReader(response.Content.ReadAsStreamAsync().Result)
        Return reader.ReadToEnd()
    End Function
    Private Function Run_opus(audio_bitrate As Integer, output As String)
        Dim opusProcessInfo As New ProcessStartInfo
        Dim opusProcess As Process
        opusProcessInfo.FileName = "opusenc.exe"
        opusProcessInfo.Arguments = "--music --bitrate " & audio_bitrate & " """ + output + "-rav1e-audio.wav"""
        opusProcessInfo.CreateNoWindow = True
        opusProcessInfo.RedirectStandardOutput = False
        opusProcessInfo.UseShellExecute = False
        opusProcess = Process.Start(opusProcessInfo)
        opusProcess.WaitForExit()
        Return True
    End Function

    Private Function split_video_file(input As String, output As String, tempFolder As String)
        Dim ffmpegProcessInfo As New ProcessStartInfo
        Dim ffmpegProcess As Process
        ffmpegProcessInfo.FileName = "ffmpeg.exe"
        ffmpegProcessInfo.Arguments = "-i """ + input + """ -f segment -segment_time 1 """ + tempFolder + "/" + output + "-part-%6d.y4m"" -y"
        ffmpegProcessInfo.CreateNoWindow = True
        ffmpegProcessInfo.RedirectStandardOutput = False
        ffmpegProcessInfo.UseShellExecute = False
        ffmpegProcess = Process.Start(ffmpegProcessInfo)
        ffmpegProcess.WaitForExit()
        Return True
    End Function

    Private Function extract_audio(input As String, output As String)
        Dim ffmpegProcessInfo As New ProcessStartInfo
        Dim ffmpegProcess As Process
        ffmpegProcessInfo.FileName = "ffmpeg.exe"
        ffmpegProcessInfo.Arguments = "-i """ + input + """ -vn """ + output + "-rav1e-audio.wav"" -y"
        ffmpegProcessInfo.CreateNoWindow = True
        ffmpegProcessInfo.RedirectStandardOutput = False
        ffmpegProcessInfo.UseShellExecute = False
        ffmpegProcess = Process.Start(ffmpegProcessInfo)
        ffmpegProcess.WaitForExit()
        Return True
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        quantizer.Value = My.Settings.quantizer
        speed.Value = My.Settings.speed
        audioBitrate.Value = My.Settings.bitrate
        keyFrameInterval.Value = My.Settings.keyframeInterval
        tempLocationPath.Text = My.Settings.tempFolder
        UserKey.Text = My.Settings.weak_key
        GUILoaded = True
    End Sub
    Private Sub Form1_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub Form1_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        InputTxt.Text = CType(e.Data.GetData(DataFormats.FileDrop), String())(0)
    End Sub
    Private Function ffmpegExists() As Boolean
        If My.Computer.FileSystem.FileExists("ffmpeg.exe") Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function OpusEncExists() As Boolean
        If My.Computer.FileSystem.FileExists("opusenc.exe") Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub tempLocationPath_TextChanged(sender As Object, e As EventArgs) Handles tempLocationPath.TextChanged
        If GUILoaded Then
            My.Settings.tempFolder = tempLocationPath.Text
            My.Settings.Save()
        End If
    End Sub

    Private Sub quantizer_ValueChanged(sender As Object, e As EventArgs) Handles quantizer.ValueChanged
        If GUILoaded Then
            My.Settings.quantizer = quantizer.Value
            My.Settings.Save()
        End If
    End Sub

    Private Sub speed_ValueChanged(sender As Object, e As EventArgs) Handles speed.ValueChanged
        If GUILoaded Then
            My.Settings.speed = speed.Value
            My.Settings.Save()
        End If
    End Sub

    Private Sub audioBitrate_ValueChanged(sender As Object, e As EventArgs) Handles audioBitrate.ValueChanged
        If GUILoaded Then
            My.Settings.bitrate = audioBitrate.Value
            My.Settings.Save()
        End If
    End Sub

    Private Sub BrowseTempLocation_Click(sender As Object, e As EventArgs) Handles BrowseTempLocation.Click
        Dim TempFolderBrowser As New FolderBrowserDialog With {
           .ShowNewFolderButton = False
       }
        Dim OkAction As MsgBoxResult = TempFolderBrowser.ShowDialog
        If OkAction = MsgBoxResult.Ok Then
            tempLocationPath.Text = TempFolderBrowser.SelectedPath
        End If
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub keyFrameInterval_ValueChanged(sender As Object, e As EventArgs) Handles keyFrameInterval.ValueChanged
        If GUILoaded Then
            My.Settings.keyframeInterval = keyFrameInterval.Value
            My.Settings.Save()
        End If
    End Sub

    Private Sub UserKey_TextChanged(sender As Object, e As EventArgs) Handles UserKey.TextChanged
        My.Settings.weak_key = UserKey.Text
    End Sub
End Class
