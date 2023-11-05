namespace CDownloadHelper {
    public interface IInstaller {
        bool EnsureInstalledApp(int appType, string minVersion, string appName, string packageName, string fileName, string downloadPath);
    }
}
