using Microsoft.Win32;
using System;
using CModels;
using System.IO;
using CCrystal.Properties;

namespace CCrystal {
    public class Install : IInstaller {
        public bool EnsureInstalled(int appType, string installerPath) {
            bool result = false;
            string appName = string.Empty;
            string fileName = string.Empty;
            string uri = string.Empty;
            if (appType == 32) {
                appName = Resources.crname32;
                fileName = Resources.crfname32;
                if (string.IsNullOrEmpty(installerPath)) uri = Resources.cruri32;
            }
            else {
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
                        uri = Uri.UnescapeDataString($"{installerPath}/{fileName}");
                    }
                    using (Downloader downloader = new Downloader()) {
                        downloader.DownloadAsync(uri, fullFileName);
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
