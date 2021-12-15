using System;
using System.Collections.Generic;
using Code.InterfaceAdapters.TaskPanel;
using Code.InterfaceAdapters.ToDoPanel;
using Code.Model.Repositories;
using Code.Model.UseCases.CreateTask;
using Code.Model.UseCases.DeleteTask;
using Code.Model.UseCases.LoadAllTasks;
using Code.Views;
using UniRx;
using UnityEngine;

namespace Code
{
    public class MenuInstaller : MonoBehaviour
    {
        [SerializeField] private RectTransform _canvasParent;

        [SerializeField] private ToDoPanelView _toDoPanelPrefab; 
        [SerializeField] private TaskPanelView _taskPanelPrefab;
        private LoadAllTasksUseCase _loadAllTasksUseCase;

        private List<IDisposable> _disposables = new List<IDisposable>();
        private void Awake()
        {
            var toDoPanelView = Instantiate(_toDoPanelPrefab, _canvasParent);
            var taskPanelView = Instantiate(_taskPanelPrefab, _canvasParent);

            var taskPanelViewModel = new TaskPanelViewModel()
                .AddTo(_disposables);
            var toDoPanelViewModel = new ToDoPanelViewModel()
                .AddTo(_disposables);


            taskPanelView.SetViewModel(taskPanelViewModel);
            toDoPanelView.SetViewModel(toDoPanelViewModel);
            var taskRepository = GetTaskRepository();
            var eventDispatcher = new EventDispatcherService();
            var createTaskUseCase = new CreateTaskUseCase(taskRepository, eventDispatcher);
            var deleteTaskUseCase = new DeleteTaskUseCase(taskRepository, eventDispatcher);
            new TaskPanelController(taskPanelViewModel, createTaskUseCase)
                .AddTo(_disposables);
            new ToDoPanelController(toDoPanelViewModel, taskPanelViewModel)
                .AddTo(_disposables);
            new ToDoPanelPresenter(toDoPanelViewModel, deleteTaskUseCase, eventDispatcher)
                .AddTo(_disposables);

            _loadAllTasksUseCase = new LoadAllTasksUseCase(taskRepository, eventDispatcher);
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }

        private void Start()
        {
            _loadAllTasksUseCase.GetAll();
        }

        private static ITaskRepository GetTaskRepository()
        {
            return new LocalTaskRepository();
        }
    }
}