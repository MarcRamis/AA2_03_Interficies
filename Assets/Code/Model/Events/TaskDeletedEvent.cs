namespace Code.Model.UseCases.DeleteTask
{
    public class TaskDeletedEvent{
        public readonly int Id;

        public TaskDeletedEvent(int id)
        {
            Id = id;
        }
    }
}