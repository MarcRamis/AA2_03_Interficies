using Code.InterfaceAdapters.TaskItem;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Views
{
    public class TaskItemView : View
    {
        public int Id { get; private set; }

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Button _editButton;

        public void SetViewModel(TaskItemViewModel taskItemViewModel)
        {
            Id = taskItemViewModel.Id;

            taskItemViewModel
                .Text
                .Subscribe(newText =>
                {
                    _text.SetText(newText);
                })
                .AddTo(_disposables);

        
            _deleteButton.onClick.AddListener(()=> {
                taskItemViewModel.OnDeleteButtonPressed.Execute();
            });

            _editButton.onClick.AddListener(()=> {
                taskItemViewModel.OnEditButtonPressed.Execute();
            });
        }
    }
}
