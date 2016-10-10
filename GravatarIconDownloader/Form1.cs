using System;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace GravatarIconDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.textBox3.Text = "200";
            this.textBox2.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.SelectedPath = textBox2.Text;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                textBox2.Text = dialog.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var webClient = new WebClient();
            foreach (var mailAddress in textBox1.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var hash = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(mailAddress.Trim()))).ToLower().Replace("-", "");
                webClient.DownloadFile(
                    string.Format("https://www.gravatar.com/avatar/{0}.png?s=200&d=identicon", hash),
                    string.Format(@"{0}\{1}.png", textBox2.Text, mailAddress.Trim()));
            }
            MessageBox.Show("ダウンロードしました");
            Process.Start(textBox2.Text);
        }

    }
}
