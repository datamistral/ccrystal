using CCrystalDownloadHelper.Properties;
using System;
using System.IO;

namespace CDownloadHelper {
    public class Install : IInstaller {
        public bool EnsureInstalled(int appType, string installerPath) {
            string appName = string.Empty;
            string fileName = string.Empty;
            string uri = string.Empty;
            if (appType == 32) {
                appName = Resources.crname32;
                fileName = Resources.crfname32;
                if (string.IsNullOrEmpty(installerPath)) uri = Resources.cruri32;
            } else {
                appName = Resources.crfname64;
                if (string.IsNullOrEmpty(installerPath)) uri = Resources.cruri32;
                fileName = Resources.crfname64;
            }

            long appVersion = Functions.GetInstalledVersion(appName);
            long requiredVersion = Convert.ToInt64(Resources.crminversion);

            bool needInstall = (appVersion == 0);
            if (!needInstall) {
                if (appVersion < requiredVersion) {
                    needInstall = true;
                }
            }

            if (needInstall) {
                string fullFileName = Path.Combine(Functions.GetAssemblyPath(), fileName);
                if (!File.Exists(fullFileName)) {
                    if (!string.IsNullOrEmpty(installerPath)) {
                        if (string.Compare(installerPath.Substring(installerPath.Length - 1), "/", true) == 0)
                            installerPath = installerPath.Substring(0, installerPath.Length - 1);
                        uri = Uri.UnescapeDataString(string.Format("{0}/{1}", installerPath, fileName));
                    }

                    using (CCrystalDownloadHelper.Progress progress = new CCrystalDownloadHelper.Progress()) {
                        progress.Show();
                        progress.SetCaptionLabel(string.Format("Downloading {0}", appName));
                        progress.StartDownload(uri, fullFileName);
                    }
                }

                if (Functions.InstallMsi(fullFileName, "upgrade=1")) {
                    if (appVersion < requiredVersion) {
                        needInstall = false;
                    }
                }
            }

            return !needInstall;
        }


    }
}
