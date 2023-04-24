using CCrystalDownloadHelper.Properties;
using System;
using System.IO;

namespace CDownloadHelper {
    public class Install : IInstaller {
        public bool EnsureInstalled(int appType, string installerPath) {
            string packageName = string.Empty;
            string fileName = string.Empty;
            string uri = string.Empty;
            string appName = string.Empty;

            if (appType == 32) {
                packageName = Resources.crname32;
                fileName = Resources.crfname32;
                if (string.IsNullOrEmpty(installerPath)) uri = Resources.cruri32;
                appName = "SAP Crystal Reports runtime engine for .NET Framework (32-bit)";
            } else {
                packageName = Resources.crfname64;
                if (string.IsNullOrEmpty(installerPath)) uri = Resources.cruri64;
                fileName = Resources.crfname64;
                appName = "SAP Crystal Reports runtime engine for .NET Framework (64-bit)";
            }

            long appVersion = Functions.GetInstalledVersion(packageName);
            if (appVersion == 0) appVersion=Functions.GetInstalledVersion(appName);
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
