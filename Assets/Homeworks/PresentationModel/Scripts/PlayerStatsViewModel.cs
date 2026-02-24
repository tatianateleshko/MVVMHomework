using Factory;
using Player;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UI
{
    public class PlayerStatsViewModel : IPlayerStatsViewModel, IDisposable
    {

        private List<IPlayerStatViewModel> _viewModels = new List<IPlayerStatViewModel>();
        private readonly Subject<IPlayerStatViewModel> _statAdded = new();
        private readonly Subject<IPlayerStatViewModel> _statRemoved = new();

        public IReadOnlyList<IPlayerStatViewModel> PlayerStatsViewModels => _viewModels;

        public Observable<IPlayerStatViewModel> StatAdded => _statAdded;

        public Observable<IPlayerStatViewModel> StatRemoved => _statRemoved;

        private readonly PlayerStatInfo _playerStatInfo;
        private readonly ViewModelFactory _viewModelsFactory;

        private CompositeDisposable _disposable = new();

        public PlayerStatsViewModel(PlayerStatInfo playerStatInfo, ViewModelFactory viewModelFactory)
        {
            _playerStatInfo = playerStatInfo;
            _viewModelsFactory = viewModelFactory;

            _playerStatInfo.OnStatAdded
                .Subscribe(AddStat)
                .AddTo(_disposable);

           _playerStatInfo.OnStatRemoved
                .Subscribe(RemoveStat)
                .AddTo(_disposable);
        }
        public void AddStat(PlayerStat playerStat)
        {
            var viewModel = _viewModelsFactory.Create(playerStat);
            _viewModels.Add(viewModel);
            _statAdded.OnNext(viewModel);
        }

        public void RemoveStat(PlayerStat playerStat)
        {
            var viewModel = _viewModels.FirstOrDefault();
            if (viewModel == null) return;
            _viewModels.Remove(viewModel);
            _statRemoved.OnNext(viewModel);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
