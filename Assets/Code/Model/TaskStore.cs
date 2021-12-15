using Firebase.Firestore;

[FirestoreData]
public class TaskStore
{
    [FirestoreDocumentId]
    public int Id { get; set; }
    [FirestoreProperty]
    public string Text { get; set; }
    public TaskStore()
    {
    }
    public TaskStore(int id, string text)
    {
        Id = id;
        Text = text;
    }
}