using System.Text;

namespace Base64File.ConsoleApp.Infra;

public class FileService : IFileService
{
    private static readonly Encoding DefaultEncoding = Encoding.UTF8;
    public async Task<byte[]> GetFileBytes(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"The file '{filePath}' does not exist.");

        return await File.ReadAllBytesAsync(filePath);
    }

    public async Task<string> GetFileText(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"The file '{filePath}' does not exist.");

        return await File.ReadAllTextAsync(filePath, DefaultEncoding);
    }

    public async Task WriteFileBytes(string filePath, byte[] fileContent, bool overwrite)
    {
        if (!overwrite && File.Exists(filePath))
            throw new FileLoadException($"The file '{filePath}' already exists.");
        else if (File.Exists(filePath))
            File.Delete(filePath);

        await File.WriteAllBytesAsync(filePath, fileContent);
    }

    public async Task WriteFileText(string filePath, string fileContent, bool overwrite)
    {
        if (!overwrite && File.Exists(filePath))
            throw new FileLoadException($"The file '{filePath}' already exists.");
        else if (File.Exists(filePath))
            File.Delete(filePath);

        await File.WriteAllTextAsync(filePath, fileContent, DefaultEncoding);
    }
}
