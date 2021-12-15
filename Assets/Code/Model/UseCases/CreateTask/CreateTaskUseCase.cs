using Code.Model.Repositories;
using UnityEngine;
using Code.Model;

namespace Code.Model.UseCases.CreateTask
{
    public class CreateTaskUseCase : ICreateTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEventDispatcherService _eventDispatcherService;
        private readonly IFirebaseStoreService _firebaseStoreService;

        public CreateTaskUseCase(ITaskRepository taskRepository,
            IEventDispatcherService eventDispatcherService,
            IFirebaseStoreService firebaseStoreService)
        {
            _taskRepository = taskRepository;
            _eventDispatcherService = eventDispatcherService;
            _firebaseStoreService = firebaseStoreService;
        }

        public void Create(string taskText)
        {
            // Crear una TaskEntity
            var taskEntity = _taskRepository.Create(taskText);

            var newTaskEvent = new NewTaskCreatedEvent(taskEntity.Id, taskEntity.Text);
            _eventDispatcherService.Dispatch<NewTaskCreatedEvent>(newTaskEvent);

            // Guardar en Firestore
            _firebaseStoreService.Save(taskEntity);
        }
    }
}