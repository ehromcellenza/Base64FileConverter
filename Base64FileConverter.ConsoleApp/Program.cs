using Base64File.ConsoleApp.Domain.Base64File;
using Base64File.ConsoleApp.Domain.Base64File.Model;
using Base64File.ConsoleApp.Infra;
using Microsoft.Extensions.Configuration;
using Puffix.ConsoleLogMagnifier;
using Puffix.IoC.Configuration;

Console.WriteLine("Welcome to the the Base64File console app");

IIoCContainerWithConfiguration container;

try
{
    ConsoleHelper.WriteVerbose("Initialize application.");
    IConfigurationRoot configuration = new ConfigurationBuilder()
          .AddCommandLine(args)
          .Build();

    container = IoCContainer.BuildContainer(configuration);

    ConsoleHelper.ClearLastLines(1);

    ConsoleHelper.WriteInfo("The application is initialized.");
    ConsoleHelper.WriteNewLine(2);
}
catch (Exception error)
{
    ConsoleHelper.WriteError("Error while initializing the BuildPom console app.", error);
    return;
}

try
{
    ConsoleArguments consoleArguments = container.Resolve<ConsoleArguments>();

    if (!consoleArguments.AreValidArguments())
    {
        ConsoleHelper.WriteWarning("Invalid arguments. The following arguments are mandatory for the BuildPom console app:");

        ConsoleHelper.Write($"- {ConsoleArguments.ACTION_ARG_NAME}: action to perform on files, '{ConsoleAction.ToBase64}', to convert a file to a Base64 text file, '{ConsoleAction.FromBase64}' to convert a Base 64 text file to a regular file.");
        ConsoleHelper.Write($"- {ConsoleArguments.SOURCE_FILE_PATH_ARG_NAME}: path to the source file to convert.");
        ConsoleHelper.Write($"- {ConsoleArguments.SINK_FILE_PATH_ARG_NAME}: path to the sink file (converted).");
        ConsoleHelper.Write($"- (option) {ConsoleArguments.OVERWRITE_PATH_ARG_NAME}: indicates whether to overwrite existing files.");

        return;
    }
    else
    {
        IBase64FileService base64FileService = container.Resolve<IBase64FileService>();
        await base64FileService.ProcessFile(consoleArguments);

        ConsoleHelper.WriteInfo("Press any key to exit");
        Console.ReadKey();
    }
}
catch (Exception error)
{
    ConsoleHelper.WriteError("Error while converting a file.", error);
    return;
}

