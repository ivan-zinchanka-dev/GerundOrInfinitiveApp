namespace GerundOrInfinitive.Presentation.Services;

public static class MauiAssetDeployer
{
    public static async Task<string> DeployAssetIfNeed(string assetFileName)
    {
        string deployedAssetPath = Path.Combine(FileSystem.AppDataDirectory, assetFileName);

        if (!File.Exists(deployedAssetPath))
        {
            await using (Stream sourceFileStream = await FileSystem.OpenAppPackageFileAsync(assetFileName))
            {
                await using (Stream resultFileStream = File.Create(deployedAssetPath))
                {
                    await sourceFileStream.CopyToAsync(resultFileStream);
                }
            }
        }

        return deployedAssetPath;
    }
}