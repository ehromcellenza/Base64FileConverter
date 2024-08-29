using Base64File.ConsoleApp.Domain.Base64File.Model;

namespace Base64File.ConsoleApp.Domain.Base64File;

public interface IBase64FileService
{
    Task ProcessFile(ConsoleArguments consoleArguments);

    Task TransformToBase64(string sourceFilePath, string sinkFilePath, bool overwriteFiles);

    Task TransformFromBase64(string sourceFilePath, string sinkFilePath, bool overwriteFiles);
}
