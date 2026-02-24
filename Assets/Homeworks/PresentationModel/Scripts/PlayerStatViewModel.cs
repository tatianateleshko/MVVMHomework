using Player;
using R3;
using System.Collections.Generic;
using UI.ViewComponents;

namespace UI
{
    public class PlayerStatViewModel : IPlayerStatViewModel
    {
        public string Name { get; }

        public ReadOnlyReactiveProperty<string> StatValue => _statValue;
        private readonly ReactiveProperty<string> _statValue = new();

        private readonly PlayerStat _playerStat;

        private CompositeDisposable _disposables = new CompositeDisposable();

        public PlayerStatViewModel(PlayerStat stat) 
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

