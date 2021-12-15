using Code.InterfaceAdapters.TaskPanel;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Views
{
    public class TaskPanelView : View
    {
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Button _addButton;
        [SerializeField] private TMP_InputField _inputField;

        private TaskPanelViewModel _viewModel;

        public void SetViewModel(TaskPanelViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel
                .IsVisible
                .Subscribe((isVisible)=> {
                    gameObject.SetActive(isVisible);
                })
                .AddTo(_disposables);
            _viewModel
                .TaskName
                .Subscribe(taskName =>
                {
                    _inputField.SetTextWithoutNotify(taskName);
                })
                .AddTo(_disposables);

            _deleteButton.onClick.AddListener(() =>
            {
                _viewModel.OnDeleteButtonPressed.Execute();
            });

            _addButton.onClick.AddListener(() =>
            {
                _viewModel.OnAddButtonPressed.Execute(_inputField.text);
            });
        }
    }
}
