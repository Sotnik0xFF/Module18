namespace Module18.FinalPractice.UI;

public class ConsoleUserInterface : IUserInterface
{
    public ConsoleUserInterface()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
    }

    public string ReadValue(string message)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(message);
        return Console.ReadLine() ?? String.Empty;
    }

    public void UpdateMessage(string message)
    {
        Console.Write($"\r{message}");
    }

    public void WriteMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message);
    }

    public void WriteWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
    }
}
