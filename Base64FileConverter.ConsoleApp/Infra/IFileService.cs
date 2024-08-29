using System.IO.Compression;

namespace Base64File.ConsoleApp.Infra;

public interface IFileService
{
    Task<byte[]> GetFileBytes(string filePath);

    Task<string> GetFileText(string filePath);

    Task WriteFileBytes(string filePath, byte[] fileContent, bool overwrite = true);

    Task WriteFileText(string filePath, string fileContent, bool overwrite = true);
}
