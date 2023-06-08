using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

using Module18.FinalPractice.Commands;
using Module18.FinalPractice.UI;

namespace Module18.FinalPractice;

public class YouTubeLoaderService : IYouTubeLoaderService, IProgress<double>
{
    private readonly IUserInterface _ui;
    private readonly YoutubeClient _client;
    private readonly List<Command> _commands;
    private string _outputDir;
    private string? _url;
    private Video? _video;


    public YouTubeLoaderService(IUserInterface userInterface)
    {
        _ui = userInterface;
        _client = new YoutubeClient();

        _outputDir = Path.Combine(Environment.CurrentDirectory, "download");
        Directory.CreateDirectory(_outputDir);
        Environment.CurrentDirectory = Path.Combine(Environment.CurrentDirectory, "libs");

        _commands = new List<Command>()
        {
            new DownloadVideoCommand(this),
            new GetVideoDetailsCommand(this)
        };

    }

    public void Run()
    {
        _url = _ui.ReadValue("Введите URL: ");
        try
        {
            _video = _client.Videos.GetAsync(_url).Result;
            _ui.WriteMessage(_video.Title);
            _ui.WriteMessage($"\ninfo или i \t- информация о видео");
            _ui.WriteMessage($"download или d \t- загрузка видео\n");
        }
        catch (Exception e)
        {
            _ui.WriteWarning(e.Message);
            Console.ReadKey();
            return;
        }

        while (true)
        {
            Command? command = GetCommand(_ui.ReadValue("Введите команду: "));
            if (command != null)
            {
                command.Execute();
            }
            else
            {
                _ui.WriteWarning("Неизвестная команда.\n");
            }
        }
    }

    public void DownloadVideo()
    {
        var manifest = _client.Videos.Streams.GetManifestAsync(_url!).AsTask().Result;
        var streamInfo = manifest.GetMuxedStreams().Where(x => x.Container == Container.Mp4 && x.VideoQuality.Label == "360p").First();

        string path = Path.Combine(_outputDir, $"video.{streamInfo.Container.Name}");
        _ui.WriteMessage($"Загрузка видео в \"{path}\"...");
        _client.Videos.Streams.DownloadAsync(streamInfo, path, this).GetAwaiter().GetResult();
        _ui.WriteMessage("\nЗагрузка завершена.");
    }

    public void ShowVideoDetaills()
    {
        _ui.WriteMessage($"Название: {_video!.Title}");
        _ui.WriteMessage($"Автор: {_video.Author}");
        _ui.WriteMessage($"Продолжительность: {_video.Duration}");
        _ui.WriteMessage($"\nОписание:");
        _ui.WriteMessage(_video.Description);
        _ui.WriteMessage("");
    }

    private Command? GetCommand(string input)
    {
        return _commands.FirstOrDefault(c => c.IsCommandFor(input));
    }

    public void Report(double value)
    {
        _ui.UpdateMessage($"Загружено {((int)(value * 100))}%");
    }
}
