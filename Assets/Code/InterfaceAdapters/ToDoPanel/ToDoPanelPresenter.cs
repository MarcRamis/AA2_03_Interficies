using System.Linq;
using Code.InterfaceAdapters.TaskItem;
using Code.Model.UseCases.CreateTask;
using Code.Model.UseCases.DeleteTask;
using Code.Utils;
using UniRx;

namespace Code.InterfaceAdapters.ToDoPanel
{
    public class ToDoPanelPresenter : Presenter
    {
        private readonly IEventDispatcherService _eventDispatcherService;
        private readonly IDeleteTaskUseCase _deleteTaskUseCase;
        private readonly ToDoPanelViewModel _viewModel;

        public ToDoPanelPresenter(ToDoPanelViewModel viewModel,
            IDeleteTaskUseCase deleteTaskUseCase,
            IEventDispatcherService eventDispatcherService)
        {
            _viewModel = viewModel;
            _deleteTaskUseCase = deleteTaskUseCase;
            _eventDispatcherService = eventDispatcherService;

            _eventDispatcherService.Subscribe<NewTaskCreatedEvent>(OnNewTaskCreated);
            _eventDispatcherService.Subscribe<TaskDeletedEvent>(OnTaskDeleted);
        }

        public override void Dispose()
        {
            base.Dispose();
            _eventDispatcherService.Unsubscribe<NewTaskCreatedEvent>(OnNewTaskCreated);
            _eventDispatcherService.Unsubscribe<TaskDeletedEvent>(OnTaskDeleted);
        }

        private void OnTaskDeleted(TaskDeletedEvent data)
        {
            var task = _viewModel.Tasks
                .First(entry => { return entry.Id.Equals(data.Id); });
            _viewModel.Tasks.Remove(task);
        }

        private void OnNewTaskCreated(NewTaskCreatedEvent data)
        {
            var taskItemViewModel = new TaskItemViewModel(data.Id, data.Text);
            taskItemViewModel
                .OnDeleteButtonPressed
                .Subscribe(_ =>
                    {
                        _deleteTaskUseCase.Delete(taskItemViewModel.Id);
                    }
                );
            _viewModel.Tasks.Add(taskItemViewModel);
        }
    }
}
