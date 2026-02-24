using Player;
using R3;
using System;
using UnityEngine;

namespace UI
{
    public class PlayerLevelViewModel : IPlayerLevelViewModel, IDisposable
    {
        public ReadOnlyReactiveProperty<string> Level => _level;
        public ReadOnlyReactiveProperty<bool> CanLevelUp => _canLevelUp;
        public ReadOnlyReactiveProperty<string> Experience => _experience;

        private readonly ReactiveProperty<bool> _canLevelUp = new();
        private readonly ReactiveProperty<string> _level = new();
        private readonly ReactiveProperty<string> _experience = new();

        private readonly PlayerLevel _playerLevel;
        private CompositeDisposable _disposable = new CompositeDisposable();

        public PlayerLevelViewModel(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;

            _playerLevel.CurrentExperience.Subscribe(SetExperienceValue)
               .AddTo(_disposable);

            _playerLevel.CanLevelUp
                .Subscribe(UpdateCanLevelUp)
                .AddTo(_disposable);

            _playerLevel.CurrentLevel
                .Subscribe(SetLevelValue)
                .AddTo(_disposable);

        }
        private void SetExperienceValue(int experience)
        {
            _experience.Value = $"{experience.ToString() + "/" + _playerLevel.RequiredExperience}";
        }

        public void LevelUp()
        {
            _playerLevel.LevelUp();
        }

        private void UpdateCanLevelUp(bool canLevelUp)
        {
            _canLevelUp.Value = canLevelUp;
        }

        private void SetLevelValue(int level)
        {
            _level.Value = $"{"Level:" + level.ToString()}";
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }

}
