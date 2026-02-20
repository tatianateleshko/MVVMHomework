using System;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using R3;
using System.Collections.Generic;
using System.Linq;
using Player;


namespace UI.ViewComponents
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerName;

        [SerializeField] private TextMeshProUGUI _description;

        [SerializeField] private TextMeshProUGUI _exp;

        [SerializeField] private Image _icon;

        [SerializeField] private Button _levelUpButton;

        [SerializeField] private TextMeshProUGUI _playerLevel;

        [SerializeField] private Button _closeButton;

        [SerializeField] private PlayerStatView _playerStatView;
        [SerializeField] private Transform _playerStatContainer;

        private readonly List<PlayerStatView> _statViews = new();

        private IPlayerViewModel _viewModel;

        private CompositeDisposable _disposable = new CompositeDisposable();

        private void Awake()
        {
            _closeButton.onClick.AddListener(Hide);
            _levelUpButton.onClick.AddListener(OnLevelUpButtonClick);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(Hide);
            _levelUpButton.onClick.RemoveListener(OnLevelUpButtonClick);
        }

        public void Show(IViewModel viewModel)
        {
            if (viewModel is not IPlayerViewModel productViewModel)
            {
                throw new Exception("Invalid view model type");
            }

            _viewModel = productViewModel;

            _viewModel.Name
                .Subscribe(x => { _playerName.text = x; })
                .AddTo(_disposable);

            _viewModel.Description
                .Subscribe(x => { _description.text = x; })
                .AddTo(_disposable);

            _viewModel.PlayerIcon.
                Subscribe(x => { _icon.sprite = x; })
                .AddTo(_disposable);

            _viewModel.Level
                .Subscribe(x => { _playerLevel.text = x; })
                .AddTo(_disposable);

            _viewModel.Experience
                .Subscribe(x => {_exp.text = x;})
                .AddTo(_disposable);

           _viewModel.CanLevelUp
                .Subscribe(OnLevelChanged)
                .AddTo(_disposable);


            _viewModel.StatAdded
                      .Subscribe(statViewModel => SpawnStatView(statViewModel))
                      .AddTo(_disposable);

            _viewModel.StatRemoved
                      .Subscribe(statViewModel => RemoveStatView(statViewModel))
                      .AddTo(_disposable);

            gameObject.SetActive(true);

        }

        private void SpawnStatView(IPlayerStatsViewModel playerStatViewModel)
        {
            PlayerStatView statView = Instantiate(_playerStatView, _playerStatContainer);
            statView.Initialize(playerStatViewModel);
            _statViews.Add(statView);
        }


        private void RemoveStatView(IPlayerStatsViewModel statViewModel)
        {
            var statView = _statViews.FirstOrDefault(v => v.ViewModel == statViewModel);
            if (statView == null) return;
            _statViews.Remove(statView);
            Destroy(statView.gameObject);
        }


        private void OnLevelChanged(bool canLevelUp)
        {
            _levelUpButton.interactable = canLevelUp;
        }

       private void OnLevelUpButtonClick()
        {
            if (_viewModel.CanLevelUp.CurrentValue)
            {
                _viewModel.LevelUp();
            }
       }

        private void Hide()
        {
            gameObject.SetActive(false);
            _disposable.Dispose();
            _viewModel.Dispose();
        }

    }
}


