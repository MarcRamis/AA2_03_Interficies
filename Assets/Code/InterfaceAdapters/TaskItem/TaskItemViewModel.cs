using UniRx;

namespace Code.InterfaceAdapters.TaskItem
{
    public class TaskItemViewModel
    {
        public readonly int Id;
        public readonly int v;

        public readonly ReactiveProperty<string> Text;
        public readonly ReactiveCommand OnDeleteButtonPressed;
        public readonly ReactiveCommand OnEditButtonPressed;

        public TaskItemViewModel(int id, string taskText)
        {
            Id = id;

            Text = new ReactiveProperty<string>(taskText);
            OnDeleteButtonPressed = new ReactiveCommand();
            OnEditButtonPressed = new ReactiveCommand();
        }
    }
}
