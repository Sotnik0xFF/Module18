namespace Module18.FinalPractice.UI;

public interface IUserInterface
{
    string ReadValue(string message);
    void WriteMessage(string message);
    void WriteWarning(string message);
    void UpdateMessage(string message);
}
