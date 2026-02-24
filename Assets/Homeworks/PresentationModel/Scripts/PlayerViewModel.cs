using Player;
using R3;
using UnityEngine;

namespace UI
{
    public class PlayerViewModel : IPlayerViewModel
    {
        public ReadOnlyReactiveProperty<Sprite> PlayerIcon => _playerIcon;
        public ReadOnlyReactiveProperty<string> Name => _name;
        public ReadOnlyReactiveProperty<string> Description => _description;

        private readonly ReactiveProperty<Sprite> _playerIcon = new();
        private readonly ReactiveProperty<string> _description = new();
        private readonly ReactiveProperty<string> _name = new();
  

        private readonly UserInfo _userUnfo;

        private readonly CompositeDisposable _disposable = new();

     
        public PlayerViewModel(UserInfo userInfo)
        {
            _userUnfo = userInfo;   
       
            _userUnfo.CurrentName
                .Subscribe(SetName)
                .AddTo(_disposable);

            _userUnfo.Description
                .Subscribe(SetDescription)
                .AddTo(_disposable);

            _userUnfo.Icon
                .Subscribe(SetIcon)
                .AddTo(_disposable);
        }

        private void SetDescription(string description)
        {
            _description.Value = description;
        }

        private void SetIcon(Sprite icon)
        {
            _playerIcon.Value = icon;   
        }

        private void SetName(string name)
        {
            _name.Value = name;
        }

        public void Dispose()
        {
            _disposable.Dispose();  
        }
    }
}
