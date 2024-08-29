using Base64File.ConsoleApp.Domain.Base64File.Model;
using Base64File.ConsoleApp.Infra;
using Puffix.ConsoleLogMagnifier;

namespace Base64File.ConsoleApp.Domain.Base64File;

public class Base64FileService(IFileService fileService) : IBase64FileService
{
    private readonly IFileService fileService = fileService;

    public async Task ProcessFile(ConsoleArguments consoleArguments)
    {
        switch (consoleArguments.Action)
        {
            case ConsoleAction.ToBase64:
                ConsoleHelper.WriteInfo($"Convert the file ('{consoleArguments.SourceFilePath}') to a Base 64 text file ('{consoleArguments.SinkFilePath}').");
                await TransformToBase64(consoleArguments.SourceFilePath, consoleArguments.SinkFilePath, consoleArguments.Overwrite);
                ConsoleHelper.WriteSuccess("The file is converted to a Base 64 text file.");
                break;

            case ConsoleAction.FromBase64:
                ConsoleHelper.WriteInfo($"Convert the file ('{consoleArguments.SourceFilePath}') from a Base 64 text file to a regular file ('{consoleArguments.SinkFilePath}').");
                await TransformFromBase64(consoleArguments.SourceFilePath, consoleArguments.SinkFilePath, consoleArguments.Overwrite);
                ConsoleHelper.WriteSuccess($"The file is converted from a Base 64 text file.");
                break;
            default:
                throw new ArgumentOutOfRangeException($"The action {consoleArguments.Action} is unknonw.");
        }
    }

    public async Task TransformToBase64(string sourceFilePath, string sinkFilePath, bool overwriteFiles)
    {
        byte[] fileContent = await fileService.GetFileBytes(sourceFilePath);
        string base64FileContent = Convert.ToBase64String(fileContent);
        await fileService.WriteFileText(sinkFilePath, base64FileContent, overwriteFiles);
    }

    public async Task TransformFromBase64(string sourceFilePath, string sinkFilePath, bool overwriteFiles)
    {
        string fileContent = await fileService.GetFileText(sourceFilePath);
        byte[] bytesFileContent = Convert.FromBase64String(fileContent);
        await fileService.WriteFileBytes(sinkFilePath, bytesFileContent, overwriteFiles);
    }
}
