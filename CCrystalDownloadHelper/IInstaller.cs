namespace CDownloadHelper {
    public interface IInstaller {
        bool EnsureInstalled(int appType, string installerPath);
    }
}
