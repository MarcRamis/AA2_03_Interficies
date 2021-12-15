namespace Code.Model
{
    public class TaskEntity
    {
        public readonly int Id;
        public readonly string Text;

        public TaskEntity(int id, string text)
        {
            Id = id;
            Text = text;
        }
    }
}
