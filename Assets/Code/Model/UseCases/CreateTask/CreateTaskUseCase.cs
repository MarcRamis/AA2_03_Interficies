using Code.Model.Repositories;

namespace Code.Model.UseCases.CreateTask
{
    public class CreateTaskUseCase : ICreateTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEventDispatcherService _eventDispatcherService;

        public CreateTaskUseCase(ITaskRepository taskRepository,
            IEventDispatcherService eventDispatcherService)
        {
            _taskRepository = taskRepository;
            _eventDispatcherService = eventDispatcherService;
        }

        public void Create(string taskText)
        {
            // Crear una TaskEntity
            var taskEntity = _taskRepository.Create(taskText);

            var newTaskEvent = new NewTaskCreatedEvent(taskEntity.Id, taskEntity.Text);
            _eventDispatcherService.Dispatch<NewTaskCreatedEvent>(newTaskEvent);
        }
    }
}
