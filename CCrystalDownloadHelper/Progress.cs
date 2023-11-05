using CDownloadHelper;
using System;
using System.Windows.Forms;

namespace CCrystalDownloadHelper {
    public partial class Progress : Form {
        public string FileName { get; set; }
        public string Url { get; set; }
        public Progress() {
            InitializeComponent();
        }

        public void SetCaptionLabel(string text) {
            lCaption.Text = text;
        }
        public void ProgressChage(object sender, System.Net.DownloadProgressChangedEventArgs e) {
            try {
                decimal progressValue = (decimal)e.BytesReceived / e.TotalBytesToReceive;
                progressBar.Value = Convert.ToInt32(progressValue * 100);
                lProgress.Text = string.Format("{0} Mb / {1} Mb   {2}", ToMb(e.BytesReceived), ToMb(e.TotalBytesToReceive), progressValue.ToString("P", System.Globalization.CultureInfo.InvariantCulture));
            } catch (Exception ex) {
                progressBar.Style = ProgressBarStyle.Continuous;
                lProgress.Text = "Downloading ...";
                toolTip1.SetToolTip(lTooltip, ex.Message);
                lTooltip.Text = ".";
            }
        }

        public string ToMb(Int64 value) {
            return (value / (double)Math.Pow(1024, 2)).ToString("0.00");

        }

        internal void StartDownload(string uri, string fileName) {
            using (Downloader downloader = new Downloader()) {
                downloader.ProgressChage += ProgressChage;
                downloader.DownloadAsync(uri, fileName);
                downloader.ProgressChage -= ProgressChage;
                this.Close();
            }
        }

        private void Progress_Load(object sender, EventArgs e) {
            tUrl.Text = this.Url;
        }

        private void bDownload_Click(object sender, EventArgs e) {
            tUrl.Visible = false;
            bDownload.Visible = false;
            lCaption.Text = string.Format("Downloading:{0}", tUrl.Text);
            progressBar.Visible = true;
            Application.DoEvents();
            StartDownload(tUrl.Text, this.FileName);
        }
    }
}
