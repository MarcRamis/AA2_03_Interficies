using System.Collections.Generic;
using System.Linq;
using Code.InterfaceAdapters.TaskItem;
using Code.InterfaceAdapters.ToDoPanel;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Views
{
    public class ToDoPanelView : View
    {
        [SerializeField] private TaskItemView _taskItemViewPrefab;
        [SerializeField] private RectTransform _taskItemContainer;

        [SerializeField] private Button _addTaskButton;
        private ToDoPanelViewModel _viewModel;

        private List<TaskItemView> _instantiatedTaskItems;

        public void SetViewModel(ToDoPanelViewModel viewModel)
        {
            _instantiatedTaskItems = new List<TaskItemView>();
            _viewModel = viewModel;

            _viewModel
                .Tasks
                .ObserveAdd()
                .Subscribe(InstantiateTask)
                .AddTo(_disposables);
        
            _viewModel
                .Tasks
                .ObserveRemove()
                .Subscribe(RemoveTask)
                .AddTo(_disposables);

            _addTaskButton.onClick.AddListener(() => {
                    _viewModel.AddTaskButtonPressed.Execute();
                }
            );
        }

        private void RemoveTask(CollectionRemoveEvent<TaskItemViewModel> taskItemViewModel)
        {
            var item = _instantiatedTaskItems
                .First(item => item.Id.Equals(taskItemViewModel.Value.Id));
      
            Destroy(item.gameObject);
            _instantiatedTaskItems.Remove(item);
        }

        private void InstantiateTask(CollectionAddEvent<TaskItemViewModel> taskEntity)
        {
            var taskItemView = Instantiate(_taskItemViewPrefab, _taskItemContainer);
            taskItemView.SetViewModel(taskEntity.Value);

            _instantiatedTaskItems.Add(taskItemView);
        }
    }
}
