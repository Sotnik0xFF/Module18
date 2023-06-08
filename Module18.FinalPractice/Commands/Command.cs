namespace Module18.FinalPractice.Commands;

public abstract class Command
{
    public abstract void Execute();

    protected abstract string[] CommandStrings { get; }
    public bool IsCommandFor(string input) => CommandStrings.Contains(input.ToLower());
}
