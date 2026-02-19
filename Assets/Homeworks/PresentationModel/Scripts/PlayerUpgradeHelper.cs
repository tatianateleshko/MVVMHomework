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
        [SerializeField] private int _currentStatValue;
        [SerializeField] private int _expValue;
        [SerializeField] private StatsType _statType;
        [SerializeField] private int _statValue;
        [SerializeField] private int _newStatValue;
        private PlayerLevel _playerLevel;
        private PlayerStatInfo _playerStatInfo;

        [Inject]
        private void Construct(PlayerLevel playerLevel, PlayerStatInfo playerStatInfo)
        {
            _playerLevel = playerLevel;
            _playerStatInfo = playerStatInfo;
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
            var stat = _playerStatInfo.stats.FirstOrDefault();
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
            var stat = _playerStatInfo.stats.FirstOrDefault();
            _playerStatInfo.RemoveStat(stat);
        }

    }
}




