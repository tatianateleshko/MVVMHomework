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
        [SerializeField] private PlayerData _playerData;


        private ViewModelFactory _playerViewModelFactory;
        private PlayerLevel _playerLevel;
        private PlayerStatInfo _playerStatInfo;
        private ViewModelFactory _viewModelFactory;


        [Inject]
        private void Construct(ViewModelFactory factory, PlayerLevel playerLevel, PlayerStatInfo playerStatInfo, ViewModelFactory viewModelFactory)
        {
            _playerViewModelFactory = factory;
            _playerLevel = playerLevel;
            _playerStatInfo = playerStatInfo;
            _viewModelFactory = viewModelFactory;
        }

        [Button]
        public void ShowPlayerView()
        {
            _playerView.Show(_playerViewModelFactory.Create(_playerData, _playerLevel, _playerStatInfo, _viewModelFactory));
        }
    }

}
