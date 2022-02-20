using CCrystalDownloadHelper.Properties;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;

namespace CDownloadHelper {
    internal class Functions {

        internal static long GetInstalledVersion(string appName) {
            long ret = 0;
            try {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(Resources.uninstall_registry_key)) {
                    foreach (string subkey_name in key.GetSubKeyNames()) {
                        using (RegistryKey subkey = key.OpenSubKey(subkey_name)) {
                            var data = subkey.GetValue("DisplayName");
                            if (data != null) {
                                string displayValue = (string)data;
                                if (string.Compare(appName, displayValue, true) == 0) {
                                    ret = Convert.ToInt64(((string)subkey.GetValue("DisplayVersion")).Replace(".", ""));
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception) {
                // do nothing
            }

            return ret;
        }

        internal static bool CheckForInternetConnection() {
            try {
                using (var client = new System.Net.WebClient()) {
                    using (var stream = client.OpenRead("http://www.google.com")) {
                        return true;
                    }
                }
            }
            catch {
                return false;
            }
        }

        internal static string GetAssemblyPath() {
            string ret = string.Empty;
            try {
                string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                System.UriBuilder ub = new System.UriBuilder(assemblyPath);
                ret = System.IO.Path.GetDirectoryName(System.Uri.UnescapeDataString(ub.Path));
            }
            catch (System.Exception) {
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
