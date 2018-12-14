using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

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
    }
}
