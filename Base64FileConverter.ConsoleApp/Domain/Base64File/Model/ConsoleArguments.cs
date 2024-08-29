using Microsoft.Extensions.Configuration;

namespace Base64File.ConsoleApp.Domain.Base64File.Model;

public class ConsoleArguments
{
    internal const string ACTION_ARG_NAME = "action";
    internal const string SOURCE_FILE_PATH_ARG_NAME = "sourceFilePath";
    private const string SOURCE_FILE_PATH_ARG_NAME_ALT = "source";
    internal const string SINK_FILE_PATH_ARG_NAME = "sinkFilePath";
    private const string SINK_FILE_PATH_ARG_NAME_ALT = "sink";
    internal const string OVERWRITE_PATH_ARG_NAME = "overwrite";

    public ConsoleAction Action { get; private set; }

    public string SourceFilePath { get; private set; }

    public string SinkFilePath { get; private set; }

    public bool Overwrite { get; private set; }


    public ConsoleArguments(IConfiguration configuration)
    {
        string actionValue = configuration[ACTION_ARG_NAME] ?? string.Empty;
        Action = Enum.TryParse(actionValue, true, out ConsoleAction action) ? action : ConsoleAction.Unknown;

        SourceFilePath = configuration[SOURCE_FILE_PATH_ARG_NAME] ?? configuration[SOURCE_FILE_PATH_ARG_NAME_ALT] ?? string.Empty;
        SinkFilePath = configuration[SINK_FILE_PATH_ARG_NAME] ?? configuration[SINK_FILE_PATH_ARG_NAME_ALT] ?? string.Empty;
        Overwrite = bool.Parse(configuration[OVERWRITE_PATH_ARG_NAME] ?? bool.TrueString);
    }

    public bool AreValidArguments()
    {
        return Action != ConsoleAction.Unknown &&
               !string.IsNullOrEmpty(SourceFilePath) &&
               !string.IsNullOrEmpty(SinkFilePath);
    }
}
