namespace GerundOrInfinitive.Presentation.Services;

public class ResourcePreloader
{
    private const string DatabaseName = "gerund_or_infinitive.db";
    
    public static async Task CopyDatabaseIfNotExists()
    {
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, DatabaseName);

        if (!File.Exists(dbPath))
        {
            await using (Stream sourceFileStream = await FileSystem.OpenAppPackageFileAsync(DatabaseName))
            {
                await using (Stream resultFileStream = File.Create(dbPath))
                {
                    await sourceFileStream.CopyToAsync(resultFileStream);
                }
            }
        }
    }
}