using JetBrains.Annotations;
using Player;
using R3;
using UI;
using UnityEngine;

namespace UI
{
    public class PlayerStatsViewModel : IPlayerStatsViewModel
    {
        public string Name { get; }

        public ReadOnlyReactiveProperty<string> StatValue => _statValue;
        private readonly ReactiveProperty<string> _statValue = new();

        private readonly PlayerStat _playerStat;

        private CompositeDisposable _disposables = new CompositeDisposable();


        public PlayerStatsViewModel(PlayerStat stat) 
        {   
            _playerStat = stat;
            Name = _playerStat.Name;

           _playerStat.Value.Subscribe(SetStatValue).AddTo(_disposables);      
        }

        private void SetStatValue(int statValue)
        {
            _statValue.Value = $"{":" + statValue.ToString()}";
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }

}

