namespace Module18.FinalPractice.Commands;

public class GetVideoDetailsCommand : Command
{
    private readonly IYouTubeLoaderService _service;

    public GetVideoDetailsCommand(IYouTubeLoaderService youTubeLoaderService)
    {
        _service = youTubeLoaderService;
    }

    protected override string[] CommandStrings => new string[] { "info", "i" };

    public override void Execute()
    {
        _service.ShowVideoDetaills();
    }
}
