using System;
using System.IO;

namespace CDownloadHelper {
    public class Install : IInstaller {
        public bool EnsureInstalledApp(int appType, string minVersion, string appName, string packageName, string fileName, string downloadPath) {

            string appVersion = Functions.GetInstalledVersionString(packageName);
            if (string.IsNullOrEmpty(appVersion)) appVersion = Functions.GetInstalledVersionString(appName);
            bool needInstall = (string.IsNullOrEmpty(appVersion));
            if (!needInstall)
                needInstall = Functions.NeedsInstall(packageName, appName, minVersion);

            if (needInstall) {
                string fullFileName = Path.Combine(Functions.GetAssemblyPath(), fileName);
                if (!Functions.FileExists(fullFileName)) {
                    string uri = string.Empty;
                    if (!string.IsNullOrEmpty(downloadPath)) {
                        uri = Uri.UnescapeDataString(downloadPath);
                        using (CCrystalDownloadHelper.Progress progress = new CCrystalDownloadHelper.Progress()) {
                            progress.SetCaptionLabel(string.Format("Downloading {0} from URL:", packageName));
                            progress.Url = uri;
                            progress.FileName = fullFileName;
                            progress.ShowDialog();
                        }
                    }
                }
                if (Functions.InstallMsi(fullFileName, "upgrade=1"))
                    needInstall = Functions.NeedsInstall(packageName, appName, minVersion);
            }

            return !needInstall;
        }

    }
}
