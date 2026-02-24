using Sirenix.OdinInspector;
using UI.ViewComponents;
using UnityEngine;
using Zenject;
using Factory;
using Player;

namespace UI.Helpers
{
    public class UIHelper : MonoBehaviour
    {
        [SerializeField] private PlayerView  _playerView;
        [SerializeField] private PlayerStatsView _statsView;
        [SerializeField] private PlayerLevelView _levelView;
        private ViewModelFactory _playerViewModelFactory;
        private PlayerLevel _playerLevel;
        private PlayerStatInfo _playerStatInfo;
        private ViewModelFactory _viewModelFactory;
        private UserInfo _userInfo;


        [Inject]
        private void Construct(ViewModelFactory factory, PlayerLevel playerLevel, PlayerStatInfo playerStatInfo, ViewModelFactory viewModelFactory, UserInfo userInfo)
        {
            _playerViewModelFactory = factory;
            _playerLevel = playerLevel;
            _playerStatInfo = playerStatInfo;
            _viewModelFactory = viewModelFactory;
            _userInfo = userInfo;
        }

        [Button]
        public void ShowPlayerView()
        {
            _playerView.Show(_playerViewModelFactory.Create(_userInfo));
            _statsView.Show(_playerViewModelFactory.Create(_playerStatInfo, _viewModelFactory));
            _levelView.Show(_playerViewModelFactory.Create(_playerLevel));
        }
    }

}
