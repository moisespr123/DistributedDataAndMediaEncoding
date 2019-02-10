using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using submit_audio_file;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ProcessStartInfo batchScriptInfo = new ProcessStartInfo();
            batchScriptInfo.FileName = "submit_audio_to_boinc_server.bat";
            if (radioButton1.Checked)
                batchScriptInfo.Arguments = "\"" + PathTxt.Text + "\" \"" + CategoryTxt.Text + "\" flac";
            else
                batchScriptInfo.Arguments = "\"" + PathTxt.Text + "\" \"" + CategoryTxt.Text + "\" opus";
            Process batch = Process.Start(batchScriptInfo);
            batch.WaitForExit();

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
    }
}
