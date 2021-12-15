    namespace Code.Model.UseCases.CreateTask
    {
        public class NewTaskCreatedEvent
        {
            public readonly int Id;
            public readonly string Text;

            public NewTaskCreatedEvent(int id, string text)
            {
                Id = id;
                Text = text;
            }
        }
    }
