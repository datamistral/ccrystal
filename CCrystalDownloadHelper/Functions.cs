using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CDownloadHelper {
    internal class Functions {

        internal static long GetInstalledVersion(string appName) {
            long ret = 0;
            try {
                string version = GetInstalledVersionString(appName);
                if (!string.IsNullOrEmpty(version))
                    ret = Convert.ToInt64(version.Replace(".", ""));

            } catch (System.Exception ex) {
                // do nothing
            }

            return ret;
        }

        internal static bool NeedsInstall(string packageName, string appName, string minVersion) {
            bool ret = true;
            string appVersion = Functions.GetInstalledVersionString(packageName);
            if (string.IsNullOrEmpty(appVersion)) appVersion = Functions.GetInstalledVersionString(appName);


            bool needInstall = (string.IsNullOrEmpty(appVersion));
            if (!needInstall) {
                if (Functions.CompareVersions(minVersion, appVersion) < 0) {
                    ret = false;
                }
            }
            return ret;
        }

        internal static string GetInstalledVersionString(string appName) {
            string ret = string.Empty;
            try {
                // "SAP Crystal Reports runtime engine for .NET Framework (64-bit)"
                List<string> keys = new List<string>() {
                      @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
                      @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
                    };
                ret = FindInstalledVersion(RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64), keys, appName);
                if (string.IsNullOrEmpty(ret))
                    ret = FindInstalledVersion(RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64), keys, appName);

            } catch (System.Exception ex) {
                // do nothing
            }

            return ret;
        }

        internal static bool FileExists(string path) {
            bool ret = false;
            try {
                if (File.Exists(path)) {
                    if (new System.IO.FileInfo(path).Length > 0) {
                        ret = true;
                    }
                }
            } catch (Exception) {
                // do nothing
            }

            return ret;
        }
        internal static int CompareVersions(string ver1, string ver2) {
            Version vers1 = new Version(ver1);
            Version vers2 = new Version(ver2);
            return vers1.CompareTo(vers2);
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
