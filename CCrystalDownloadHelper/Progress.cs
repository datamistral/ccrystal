using CDownloadHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCrystalDownloadHelper {
    public partial class Progress : Form {
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
            }
        }
    }
}
