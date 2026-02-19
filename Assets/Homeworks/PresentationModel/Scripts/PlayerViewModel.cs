using Player;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using Common.Enums;
using UI.Utils;
using Factory;
using System.Linq;

namespace UI
{
    public class PlayerViewModel : IPlayerViewModel
    {
        public Sprite PlayerIcon { get; }

        public string Name { get; }
        public string Description { get; }
        public ReadOnlyReactiveProperty<string> Level => _level;
        public ReadOnlyReactiveProperty<string> Experience => _experience;
        public IReadOnlyList<IPlayerStatsViewModel> PlayerStatsViewModels => _viewModels;


        public ReadOnlyReactiveProperty<bool> CanLevelUp => _canLevelUp;

        public Observable<IPlayerStatsViewModel> StatAdded => _statAdded;

        public Observable<IPlayerStatsViewModel> StatRemoved => _statRemoved;

        public Observable<Sprite> OnPlayerImageChange => _nameChanged;

        public Observable<string> OnPlayerNameChange => _nameChanged;

        private readonly ReactiveProperty<bool> _canLevelUp = new();
        private readonly ReactiveProperty <string> _level = new();
        private readonly ReactiveProperty <string> _experience = new();
        private List<IPlayerStatsViewModel> _viewModels;
        private readonly Subject<IPlayerStatsViewModel> _statAdded = new();
        private readonly Subject<IPlayerStatsViewModel> _statRemoved = new();
        private readonly Subject<string> _nameChanged = new();
        private readonly Subject<Sprite> _iconChanged = new();


        private readonly PlayerData _playerData;
        private readonly PlayerLevel _playerLevel;
        private readonly PlayerStatInfo _playerStatInfo;
        private readonly ViewModelFactory _viewModelsFactory;
        private readonly UserInfo _playerStatsViewModel;

        private readonly CompositeDisposable _disposable = new();

     
        public PlayerViewModel(PlayerData playerData, PlayerLevel playerLevel, PlayerStatInfo playerStatInfo, ViewModelFactory viewModelFactory)
        {
            _playerData = playerData;
            _playerLevel = playerLevel;
            _playerStatInfo = playerStatInfo;
            _viewModelsFactory = viewModelFactory;

            PlayerIcon = _playerData.PlayerIcon;
            Name = _playerData.PlayerName;
            Description = _playerData.Description;

            _playerLevel.CurrentLevel.Subscribe(SetLevelValue)
                .AddTo(_disposable);

            _playerLevel.CurrentExperience.Subscribe(SetExperienceValue) 
                .AddTo(_disposable);

            _playerLevel.CanLevelUp
                .Subscribe(UpdateCanLevelUp)
                .AddTo(_disposable);

            _viewModels = new List<IPlayerStatsViewModel>(playerStatInfo.stats.Count);
            foreach (var stat in playerStatInfo.stats)
            {
              AddStat(stat);
            }

            _playerStatInfo.OnStatAdded
               .Subscribe(AddStat)
               .AddTo(_disposable);

            _playerStatInfo.OnStatRemoved
                .Subscribe(RemoveStat)
                .AddTo(_disposable);
        }

        private void SetLevelValue(int level)
        {
           _level.Value = $"{"Level:" + level.ToString()}";
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

        public void ChangeName()
        {
            

        }


        public void Dispose()
        {
            _disposable.Dispose();  
        }
    }
}
