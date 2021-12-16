using Code.Model.Repositories;
using System;

namespace Code.Model.UseCases.DeleteTask
{
    public class DeleteTaskUseCase : IDeleteTaskUseCase, IDisposable
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEventDispatcherService _eventDispatcherService;
        private readonly IFirebaseStoreService _firebaseStoreService;

        public DeleteTaskUseCase(ITaskRepository taskRepository,
            IEventDispatcherService eventDispatcherService,
            IFirebaseStoreService firebaseStoreService)
        {
            _taskRepository = taskRepository;
            _eventDispatcherService = eventDispatcherService;
            _firebaseStoreService = firebaseStoreService;
        }

        public void Delete(int id)
        {
            _taskRepository.Delete(id);

            var taskDeletedEvent = new TaskDeletedEvent(id);
            _eventDispatcherService.Dispatch<TaskDeletedEvent>(taskDeletedEvent);

            // Borrar en firestore
            _firebaseStoreService.Delete(id);
        }

        public void Dispose()
        {
        }
    }
}
