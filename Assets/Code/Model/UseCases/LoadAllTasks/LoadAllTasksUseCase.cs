using Code.Model.Repositories;
using Code.Model.UseCases.CreateTask;

namespace Code.Model.UseCases.LoadAllTasks
{
    public class LoadAllTasksUseCase : ILoadAllTasksUseCase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEventDispatcherService _eventDispatcherService;

        public LoadAllTasksUseCase(ITaskRepository taskRepository,
            IEventDispatcherService eventDispatcherService)
        {
            _taskRepository = taskRepository;
            _eventDispatcherService = eventDispatcherService;
        }

        public void GetAll()
        {
            var tasks = _taskRepository.GetAll();
            foreach(var task in tasks)
            {
                var evetData = new NewTaskCreatedEvent(task.Id, task.Text);
                _eventDispatcherService.Dispatch<NewTaskCreatedEvent>(evetData);
            }
        }
    }
}
