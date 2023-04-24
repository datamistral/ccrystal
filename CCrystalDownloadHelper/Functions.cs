using CCrystalDownloadHelper.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;

namespace CDownloadHelper {
    internal class Functions {

        internal static long GetInstalledVersion(string appName) {
            long ret = 0;
            try {
                // "SAP Crystal Reports runtime engine for .NET Framework (64-bit)"
                List<string> keys = new List<string>() {
                      @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
                      @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
                    };
                string version = FindInstalledVersion(RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64), keys, appName);
                if (string.IsNullOrEmpty(version))
                    version = FindInstalledVersion(RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64), keys, appName);

                if (!string.IsNullOrEmpty(version))
                    ret = Convert.ToInt64(version.Replace(".", ""));

            } catch (System.Exception ex) {
                // do nothing
            }

            return ret;
        }

        private static string FindInstalledVersion(RegistryKey regKey, List<string> keys, string appName) {
            string result = string.Empty;
            foreach (string key in keys) {
                using (RegistryKey rk = regKey.OpenSubKey(key)) {
                    if (rk == null) {
                        continue;
                    }
                    foreach (string skName in rk.GetSubKeyNames()) {
                        using (RegistryKey sk = rk.OpenSubKey(skName)) {
                            try {
                                string displayValue = Convert.ToString(sk.GetValue("DisplayName"));
                                if (string.Compare(displayValue, appName, true) == 0) {
                                    result = Convert.ToString(sk.GetValue("DisplayVersion"));
                                    break;
                                }
                            } catch (Exception ex) { }
                        }
                    }
                    if (!string.IsNullOrEmpty(result))
                        break;
                }
            }
            return result;
        }

        internal static bool CheckForInternetConnection() {
            try {
                using (var client = new System.Net.WebClient()) {
                    using (var stream = client.OpenRead("http://www.google.com")) {
                        return true;
                    }
                }
            } catch {
                return false;
            }
        }

        internal static string GetAssemblyPath() {
            string ret = string.Empty;
            try {
                string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                System.UriBuilder ub = new System.UriBuilder(assemblyPath);
                ret = System.IO.Path.GetDirectoryName(System.Uri.UnescapeDataString(ub.Path));
            } catch (System.Exception) {
                // do nothing
            }

            if (string.IsNullOrEmpty(ret)) {
                ret = System.IO.Path.GetTempPath();
            }

            return ret;
        }

        public static bool InstallMsi(string path, string arguments) {
            bool ret = false;

            if (File.Exists(path))
                using (Process process = new Process()) {
                    process.StartInfo.FileName = path;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    process.StartInfo.Arguments = arguments;
                    process.StartInfo.UseShellExecute = true;
                    ret = process.Start();
                    process.WaitForExit();
                }

            return ret;
        }
    }
}
