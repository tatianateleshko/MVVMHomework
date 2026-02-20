using System;
using R3;

namespace Player
{
    public sealed class PlayerLevel
    {
        public ReadOnlyReactiveProperty<int> CurrentLevel => _currentLevel;
        public ReadOnlyReactiveProperty<int>  CurrentExperience => _currentExperience;
        public ReadOnlyReactiveProperty<bool> CanLevelUp => _canLevelUp;

        private ReactiveProperty<int> _currentLevel = new (1);
        private ReactiveProperty<int> _currentExperience = new();

        private ReactiveProperty<bool> _canLevelUp = new(false);

        public int RequiredExperience
        {
            get { return 100 * (_currentLevel.Value + 1); }
        }

        public void AddExperience(int range)
        {
            var xp = Math.Min(_currentExperience.Value + range, this.RequiredExperience);
            _currentExperience.Value = xp;
            _canLevelUp.Value = _currentExperience.Value == this.RequiredExperience;
        }

        public void LevelUp()
        {
            if (_canLevelUp.Value)
            {
                _currentExperience.Value = 0;
                _currentLevel.Value++;
            }
        }
    }
}