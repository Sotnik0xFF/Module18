namespace Module18.FinalPractice.Commands;

internal class DownloadVideoCommand : Command
{
    private readonly IYouTubeLoaderService _service;

    public DownloadVideoCommand(IYouTubeLoaderService youTubeLoaderService)
    {
        _service = youTubeLoaderService;
    }

    protected override string[] CommandStrings => new string[] { "download", "d" };

    public override void Execute()
    {
        _service.DownloadVideo();
    }
}
