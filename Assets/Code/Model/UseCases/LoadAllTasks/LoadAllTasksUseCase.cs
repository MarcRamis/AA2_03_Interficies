using Code.Model.Repositories;
using Code.Model.UseCases.CreateTask;
using System;
using UnityEngine;
using System.Collections;

namespace Code.Model.UseCases.LoadAllTasks
{
    public class LoadAllTasksUseCase : ILoadAllTasksUseCase, IDisposable
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEventDispatcherService _eventDispatcherService;
        private readonly IFirebaseStoreService _storeService;

        public LoadAllTasksUseCase(ITaskRepository taskRepository,
            IEventDispatcherService eventDispatcherService,
            IFirebaseStoreService storeService)
        {
            _taskRepository = taskRepository;
            _eventDispatcherService = eventDispatcherService;
            _storeService = storeService;

            eventDispatcherService.Subscribe<SaveTaskEvent>(SaveTask);
        }

        public void GetAll()
        {
            var tasks = _taskRepository.GetAll();
            foreach(var task in tasks)
            {
                var evetData = new NewTaskCreatedEvent(task.Id, task.Text);
                _eventDispatcherService.Dispatch<NewTaskCreatedEvent>(evetData);
            }

            if (tasks.Count == 0)
            {
                _storeService.LoadAll();
            }
        }
        private void SaveTask(SaveTaskEvent data)
        {
            _taskRepository.Create(data.Text);
        }

        public IEnumerator GetTasks(float time)
        {
            yield return new WaitForSeconds(time);
            GetAll();
        }
        public void Dispose()
        {
            _eventDispatcherService.Unsubscribe<SaveTaskEvent>(SaveTask);
        }
    }
}
