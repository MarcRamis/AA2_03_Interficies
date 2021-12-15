using Code.InterfaceAdapters.TaskPanel;
using UniRx;

namespace Code.InterfaceAdapters.ToDoPanel
{
    public class ToDoPanelController : Controller
    {
        private readonly ToDoPanelViewModel _toDoPanelViewModel;
        private readonly TaskPanelViewModel _taskPanelViewModel;

        public ToDoPanelController(ToDoPanelViewModel viewModel
            , TaskPanelViewModel taskPanelViewModel)
        {
            _toDoPanelViewModel = viewModel;
            _taskPanelViewModel = taskPanelViewModel;

            _toDoPanelViewModel
                .AddTaskButtonPressed
                .Subscribe((_) =>
                {
                    _taskPanelViewModel.IsVisible.Value = true;
                    _taskPanelViewModel.TaskName.SetValueAndForceNotify(string.Empty);
                })
                .AddTo(_disposables);
        }
    }
}
