using Common.Enums;
using Player;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;
using Zenject;

namespace UI.Helpers
{
    
    public sealed class PlayerUpgradeHelper : MonoBehaviour
    {
        [SerializeField] private int _expValue;
        [SerializeField] private StatsType _statType;
        [SerializeField] private int _statValue;
        [SerializeField] private int _newStatValue;
        [SerializeField] private string _newName;
        [SerializeField] private string _newDescription;
        [SerializeField] private Sprite _newIcon;
        private PlayerLevel _playerLevel;
        private PlayerStatInfo _playerStatInfo;
        private UserInfo _userInfo;

        [Inject]
        private void Construct(PlayerLevel playerLevel, PlayerStatInfo playerStatInfo, UserInfo userInfo)
        {
            _playerLevel = playerLevel;
            _playerStatInfo = playerStatInfo;
            _userInfo = userInfo;
        }

        [Button]
        public void ChangeName()
        {
            _userInfo.ChangeName(_newName);
        }

        [Button]
        public void ChangeDescription()
        {
            _userInfo.ChangeDescription(_newDescription);
        }

        [Button]
        public void ChangeIcon()
        {
            _userInfo.ChangeIcon(_newIcon);
        }

        [Button]
        public void ChangePlayerLevel()
        {
            _playerLevel.LevelUp();
        }

        [Button]
        public void AddExperience()
        {
            _playerLevel.AddExperience(_expValue);
        }

        [Button]
        public void ChangeStatValue()
        {
            var stat = _playerStatInfo.GetStat(_statType); 
            _playerStatInfo.ChangeValue(stat, _newStatValue);
        }

        [Button]
        public void AddStat()
        {
            _playerStatInfo.AddStat(_statType, _statValue);
        }


        [Button]
        public void RemoveStat()
        {
            var stat = _playerStatInfo.GetStat(_statType);
            _playerStatInfo.RemoveStat(stat);
        }

    }
}




