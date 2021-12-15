using Firebase.Firestore;

[FirestoreData]
public class TextStore
{
    [FirestoreProperty]
    public string Text { get; set; }
    public TextStore()
    {
    }
    public TextStore(string text)
    {
        Text = text;
    }
}