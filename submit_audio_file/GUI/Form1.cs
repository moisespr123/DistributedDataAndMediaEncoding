using System.Collections.Generic;
using System;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;


namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private bool validateFile(string encoder, string file)
        {
            Dictionary<string, List<string>> extensionsDict = new Dictionary<string, List<string>>();
            extensionsDict.Add("flac", new List<string> { ".flac", ".wav" });
            extensionsDict.Add("mp3packer", new List<string> { ".mp3" });
            extensionsDict.Add("opus", new List<string> { ".flac", ".wav" });
            extensionsDict.Add("opus_ffmpeg_libopus", new List<string> { ".flac", ".mp3", ".m4a", ".wav" });
            if (extensionsDict[encoder].Contains(Path.GetExtension(file)))
            {
                return true;
            }
            return false;
        }
        private string returnOutputExtension(string encoder, string file)
        {
            Dictionary<string, string> extensionsDict = new Dictionary<string, string>();
            extensionsDict.Add("flac", ".flac");
            extensionsDict.Add("mp3packer", ".mp3");
            extensionsDict.Add("opus", ".opus");
            extensionsDict.Add("opus_ffmpeg_libopus", ".opus");
            return Path.GetFileNameWithoutExtension(file) + extensionsDict[encoder];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string[] items = Directory.GetFiles(PathTxt.Text);
            string encoder;
            string commandline;
            if (flacRadioButton.Checked)
            {
                encoder = "flac";
                commandline = "--lax - l 32 - b 4096 - e - m - p - r 15 - A bartlett - A bartlett_hann - A blackman - A blackman_harris_4term_92db - A connes - A flattop - A gauss(0.5) - A hamming - A hann - A kaiser_bessel - A nuttall - A rectangle - A triangle - A tukey(0.5) - A partial_tukey(2) - A punchout_tukey(3) - A welch - V";
            }
            else if (opusRadioButton.Checked)
            {
                encoder = "opus";
                commandline = "--music --bitrate " + bitrateUpDown.Value.ToString();
            }
            else if (mp3packer.Checked)
            {
                encoder = "mp3packer";
                commandline = "-z -R --nice 0";
            }
            else
            {
                encoder = "opus_ffmpeg_libopus";
                commandline = "-c:a libopus -b:a " + bitrateUpDown.Value.ToString() + "K";
            }
            foreach (string item in items)
            {
                if (validateFile(encoder, item.ToLower()))
                    Upload(userKey.Text.Trim(), encoder, commandline, returnOutputExtension(encoder, item), CategoryTxt.Text, item);
            }
            MessageBox.Show("File(s) submitted");
        }

        private void PathTxt_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(PathTxt.Text)) CategoryTxt.Text = Path.GetFileName(PathTxt.Text);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            PathTxt.Text = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
        }

        private static string Upload(string key, string format, string commandline, string filename, string album, string file)
        {
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                {
                    formData.Add(new StringContent(key), "k");
                    formData.Add(new StreamContent(new FileStream(file, FileMode.Open)), "filedata", Path.GetFileName(file));
                    formData.Add(new StringContent(album), "a");
                    formData.Add(new StringContent(format), "f");
                    formData.Add(new StringContent(commandline.Replace('\'', '`')), "c");
                    formData.Add(new StringContent(filename), "n");
                    Uri uri = new Uri("http://boinc.moisescardona.me/media_put.php");
                    client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en-US;q=0.8,en;q=0.6,ru;q=0.4");
                    HttpResponseMessage response = client.PostAsync(uri, formData).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Error");
                        Console.WriteLine(response.StatusCode);
                    }
                    StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result);
                    string result = reader.ReadToEnd();
                    return result;
                }
            }
        }

    }
}
