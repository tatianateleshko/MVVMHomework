using UI.ViewComponents;
using UnityEngine;
using System.Collections.Generic;
using R3;
using System.Linq;
using System;


namespace UI
{
    public class PlayerStatsView : MonoBehaviour
    {
        [SerializeField] private PlayerStatView _playerStatView;
        [SerializeField] private Transform _playerStatContainer;

        private IPlayerStatsViewModel _playerStatsViewModel;
        private List<PlayerStatView> _statViews = new();
        
        private CompositeDisposable _disposable = new CompositeDisposable();

        public void Show(IViewModel viewModel)
        {
            if (viewModel is not IPlayerStatsViewModel statViewModel)
            {
                throw new Exception("Invalid view model type");
            }

            _playerStatsViewModel = statViewModel;

            _playerStatsViewModel.StatAdded
                      .Subscribe(statViewModel => SpawnStatView(statViewModel))
                          .AddTo(_disposable);

            _playerStatsViewModel.StatRemoved
                      .Subscribe(statViewModel => RemoveStatView(statViewModel))
                          .AddTo(_disposable);          
        }
        private void SpawnStatView(IPlayerStatViewModel playerStatViewModel)
        {
            PlayerStatView statView = Instantiate(_playerStatView, _playerStatContainer);
            statView.Initialize(playerStatViewModel);
            _statViews.Add(statView);
        }

        private void RemoveStatView(IPlayerStatViewModel statViewModel)
        {
            var statView = _statViews.FirstOrDefault(v => v.ViewModel == statViewModel);
            if (statView == null) return;
            _statViews.Remove(statView);
            Destroy(statView.gameObject);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }

}
