using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace retrieve_files
{

    public partial class Form1 : Form
    {
        private dynamic items;
        private List<string> categories = new List<string> { };
        private List<string> files = new List<string> { };
        private List<string> keys = new List<string> { };
        public Form1()
        {
            InitializeComponent();
        }
        private static string GetFiles(string key)
        {
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                {
                    formData.Add(new StringContent(key), "k");
                    Uri uri = new Uri("http://boinc.moisescardona.me/data_retriever_get_files.php");
                    HttpResponseMessage response = client.PostAsync(uri, formData).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Error");
                    }
                    StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result);
                    string responseString = reader.ReadToEnd();
                    response.Content.Dispose();
                    return responseString;
                }
            }
        }
        private static async Task<bool> DownloadFile(string key, string filekey, string path)
        {
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                {
                    formData.Add(new StringContent(key), "k");
                    formData.Add(new StringContent(filekey), "f");
                    Uri uri = new Uri("http://boinc.moisescardona.me/download_file.php");
                    HttpResponseMessage response = client.PostAsync(uri, formData).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        return false;
                    }
                    FileStream fileStream = new FileStream(path, FileMode.Create);
                    await response.Content.CopyToAsync(fileStream);
                    response.Content.Dispose();
                    fileStream.Close();
                    return true;
                }
            }
        }
        private void updateListBoxes()
        {
            string reply = GetFiles(userKey.Text);
            try
            {
                items = Newtonsoft.Json.JsonConvert.DeserializeObject(reply);
                categories.Clear();
                categories.Add("None");
                foreach (dynamic item in items)
                {
                    string category = item["category"];
                    if (category != "None")
                        if (!categories.Any(category.Equals))
                            categories.Add(category);
                }
                categoriesListBox.DataSource = null;
                categoriesListBox.DataSource = categories;
            }
            catch
            {
                MessageBox.Show("Could not retrieve items.");
            }
        }
        private void showFilesButton_Click(object sender, EventArgs e)
        {
            updateListBoxes();
        }

        private void populateFilesListBox(string category)
        {
            files.Clear();
            keys.Clear();
            foreach (dynamic item in items)
            {
                if (item["category"] == category)
                {
                    files.Add(item["name"].ToString());
                    keys.Add(item["key"].ToString());
                }
            }
            filesListBox.DataSource = null;
            filesListBox.DataSource = files;
        }
        private void categoriesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoriesListBox.SelectedIndex >= 0)
            {
                populateFilesListBox(categoriesListBox.SelectedItem.ToString());
            }
        }

        private async void downloadAll_Click(object sender, EventArgs e)
        {
            if (categoriesListBox.SelectedIndex >= 0)
            {
                if (!String.IsNullOrWhiteSpace(downloadPath.Text))
                {
                    string FolderPath = downloadPath.Text;
                    if (categoriesListBox.SelectedItem.ToString() != "None" && !checkBox1.Checked)
                    {
                        FolderPath += "\\" + categoriesListBox.SelectedItem.ToString().Replace('\\', '_').Replace('/', '_').Replace(':', '_').Replace('*', '_').Replace('\"', '_').Replace('?', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_');
                    }
                    Directory.CreateDirectory(FolderPath);
                    for (int i = 0; i < files.Count; i++)
                    {
                        filesListBox.SelectedItems.Clear();
                        filesListBox.SelectedIndex = i;
                        using (WebClient client = new WebClient())
                        {
                            await DownloadFile(userKey.Text, keys[i], FolderPath + "\\" + files[i]);
                        }
                    }
                    MessageBox.Show("Downloads completed!");
                    updateListBoxes();
                }
            }

        }

        private async void downloadSelected_Click(object sender, EventArgs e)
        {
            if (categoriesListBox.SelectedIndex >= 0)
            {
                if (!String.IsNullOrWhiteSpace(downloadPath.Text))
                {
                    string FolderPath = downloadPath.Text;
                    if (categoriesListBox.SelectedItem.ToString() != "None" && !checkBox1.Checked)
                    {
                        FolderPath += "\\" + categoriesListBox.SelectedItem.ToString().Replace('\\', '_').Replace('/', '_').Replace(':', '_').Replace('*', '_').Replace('\"', '_').Replace('?', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_');
                    }
                    Directory.CreateDirectory(FolderPath);
                    foreach (int index in filesListBox.SelectedIndices)
                    {
                        using (WebClient client = new WebClient())
                        {
                            await DownloadFile(userKey.Text, keys[index], FolderPath + "\\" + files[index]);
                        }
                    }
                    MessageBox.Show("Downloads completed!");
                    updateListBoxes();
                }
            }
        }

        private async void downloadAllBtn_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(downloadPath.Text))
            {
                for (int j = 0; j < categories.Count; j++)
                {
                    categoriesListBox.SelectedIndex = j;
                    string FolderPath = downloadPath.Text;
                    if (categories[j] != "None")
                    {
                        FolderPath += "\\" + categories[j].Replace('\\', '_').Replace('/', '_').Replace(':', '_').Replace('*', '_').Replace('\"', '_').Replace('?', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_');
                    }
                    Directory.CreateDirectory(FolderPath);
                    for (int i = 0; i < files.Count; i++)
                    {
                        filesListBox.SelectedItems.Clear();
                        filesListBox.SelectedIndex = i;
                        using (WebClient client = new WebClient())
                        {
                            await DownloadFile(userKey.Text, keys[i], FolderPath + "\\" + files[i]);
                        }
                    }

                }
                MessageBox.Show("Downloads completed!");
                updateListBoxes();
            }
        }

        private void userKey_TextChanged(object sender, EventArgs e)
        {
            if (Remember.Checked)
            {
                Properties.Settings.Default.key = userKey.Text;
                Properties.Settings.Default.Save();
            }

        }

        private void Remember_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Remember = Remember.Checked;
            if (!Remember.Checked)
                Properties.Settings.Default.key = String.Empty;
            Properties.Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Remember.Checked = Properties.Settings.Default.Remember;
            if (Remember.Checked)
                userKey.Text = Properties.Settings.Default.key;
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.Description = "Browse for a folder to save the files.";
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
                downloadPath.Text = folderBrowserDialog1.SelectedPath;
        }
    }
}
