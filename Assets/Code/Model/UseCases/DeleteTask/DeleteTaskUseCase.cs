using Code.Model.Repositories;
using Code.Utils;

namespace Code.Model.UseCases.DeleteTask
{
    public class DeleteTaskUseCase : IDeleteTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEventDispatcherService _eventDispatcherService;

        public DeleteTaskUseCase(ITaskRepository taskRepository,
            IEventDispatcherService eventDispatcherService)
        {
            _taskRepository = taskRepository;
            _eventDispatcherService = eventDispatcherService;
        }

        public void Delete(int id)
        {
            _taskRepository.Delete(id);

            var taskDeletedEvent = new TaskDeletedEvent(id);
            _eventDispatcherService.Dispatch<TaskDeletedEvent>(taskDeletedEvent);
        }
    }
}
