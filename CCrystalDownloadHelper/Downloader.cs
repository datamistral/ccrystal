using System.Net;
using System.Windows.Forms;

namespace CDownloadHelper {
    internal class Downloader : System.IDisposable {
        bool _blnDownloadFinished = false;

        public event DownloadProgressChangedEventHandler ProgressChage;
        public void Download(string uri, string fileName) {
            if (Functions.CheckForInternetConnection()) {
                using (WebClient wc = new WebClient()) {
                    wc.DownloadProgressChanged += Wc_DownloadProgressChanged;
                    wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
                    wc.DownloadFile(uri, fileName);
                }
            }
        }

        public void DownloadInBrowser(string uri) {
            System.Diagnostics.Process.Start(uri);
        }

        public void DownloadAsync(string uri, string fileName) {
            if (Functions.CheckForInternetConnection()) {
                using (WebClient wc = new WebClient()) {
                    wc.DownloadProgressChanged += Wc_DownloadProgressChanged;
                    wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
                    wc.DownloadFileAsync(new System.Uri(uri), fileName);
                    while (!_blnDownloadFinished) {
                        Application.DoEvents();
                    }
                }
            }
        }

        private void Wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e) {
            _blnDownloadFinished = true;
        }

        private void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
            if (ProgressChage != null)
                ProgressChage(sender, e);
        }

        public void Dispose() {
            _blnDownloadFinished = false;
        }
    }
}
