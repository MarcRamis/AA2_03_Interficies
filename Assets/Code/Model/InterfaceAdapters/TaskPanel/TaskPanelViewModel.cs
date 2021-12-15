using UniRx;

namespace Code.InterfaceAdapters.TaskPanel
{
    public class TaskPanelViewModel : ViewModel
    {
        public readonly ReactiveCommand OnDeleteButtonPressed;
        public readonly ReactiveCommand<string> OnAddButtonPressed;

        public readonly ReactiveProperty<bool> IsVisible;
        public readonly ReactiveProperty<string> TaskName;

        public TaskPanelViewModel()
        {
            OnDeleteButtonPressed = new ReactiveCommand()
                .AddTo(_disposables);
            OnAddButtonPressed = new ReactiveCommand<string>()
                .AddTo(_disposables);
        
            IsVisible = new ReactiveProperty<bool>()
                .AddTo(_disposables);
            TaskName = new ReactiveProperty<string>(string.Empty)
                .AddTo(_disposables);
        }
    }
}
