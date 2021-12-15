using Code.Model.UseCases.CreateTask;
using UniRx;

namespace Code.InterfaceAdapters.TaskPanel
{
    public class TaskPanelController : Controller
    {
        private readonly IEventDispatcherService _eventDispatcherService;

        public TaskPanelController(TaskPanelViewModel taskPanelViewModel,
            ICreateTaskUseCase createTaskUseCase)
        {
            taskPanelViewModel.OnDeleteButtonPressed.Subscribe(
                    (_) => { taskPanelViewModel.IsVisible.Value = false; }
                )
                .AddTo(_disposables);

            taskPanelViewModel.OnAddButtonPressed.Subscribe(
                    (taskText) =>
                    {
                        createTaskUseCase.Create(taskText);
                        taskPanelViewModel.IsVisible.Value = false;
                    }
                )
                .AddTo(_disposables);
        }
    }
}