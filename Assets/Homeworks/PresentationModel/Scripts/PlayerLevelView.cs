using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerLevelView : MonoBehaviour
    {
        [SerializeField] private Button _levelUpButton;

        [SerializeField] private TextMeshProUGUI _playerLevel;
        [SerializeField] private TextMeshProUGUI _exp;

        private IPlayerLevelViewModel _playerLevelViewModel;
        private CompositeDisposable _disposable = new CompositeDisposable();

        private void Awake()
        {
            _levelUpButton.onClick.AddListener(OnLevelUpButtonClick);
        }

        private void OnDestroy()
        {
            _levelUpButton.onClick.RemoveListener(OnLevelUpButtonClick);
            _disposable.Dispose();
        }
        public void Show(IViewModel viewModel)
        {
            if (viewModel is not IPlayerLevelViewModel playerLevelViewModel)
            {
                throw new Exception("Invalid view model type");
            }
            _playerLevelViewModel = playerLevelViewModel;

            _playerLevelViewModel.Level
               .Subscribe(x => { _playerLevel.text = x; })
               .AddTo(_disposable);

            _playerLevelViewModel.CanLevelUp
                 .Subscribe(OnLevelChanged)
                 .AddTo(_disposable);

            _playerLevelViewModel.Experience
                .Subscribe(x => { _exp.text = x; })
                .AddTo(_disposable);
        }

        private void OnLevelChanged(bool canLevelUp)
        {
            _levelUpButton.interactable = canLevelUp;
        }

        private void OnLevelUpButtonClick()
        {
            if (_playerLevelViewModel.CanLevelUp.CurrentValue)
            {
                _playerLevelViewModel.LevelUp();
            }
        }
    }

}
