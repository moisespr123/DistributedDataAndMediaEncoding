using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                batchScriptInfo.Arguments = "\"" + textBox1.Text + "\" \"" + textBox2.Text + "\" flac";
            else
                batchScriptInfo.Arguments = "\"" + textBox1.Text + "\" \"" + textBox2.Text + "\" opus";
            Process batch = Process.Start(batchScriptInfo);
            batch.WaitForExit();

        }
    }
}
