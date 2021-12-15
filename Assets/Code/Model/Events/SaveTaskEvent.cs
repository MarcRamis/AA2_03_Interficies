public class SaveTaskEvent
{
    public readonly string Text;

    public SaveTaskEvent(string text)
    {
        Text = text;
    }
}