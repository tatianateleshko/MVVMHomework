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
        public ReadOnlyReactiveProperty<string> Level => _level;
        public ReadOnlyReactiveProperty<string> Experience => _experience;
        public IReadOnlyList<IPlayerStatsViewModel> PlayerStatsViewModels => _viewModels;

        public ReadOnlyReactiveProperty<bool> CanLevelUp => _canLevelUp;

        public Observable<IPlayerStatsViewModel> StatAdded => _statAdded;

        public Observable<IPlayerStatsViewModel> StatRemoved => _statRemoved;

        public ReadOnlyReactiveProperty<Sprite> PlayerIcon => _playerIcon;

        public ReadOnlyReactiveProperty<string> Name => _name;

        public ReadOnlyReactiveProperty<string> Description => _description;

        private readonly ReactiveProperty<bool> _canLevelUp = new();
        private readonly ReactiveProperty <string> _level = new();
        private readonly ReactiveProperty <string> _experience = new();
        private readonly ReactiveProperty<Sprite> _playerIcon = new();
        private readonly ReactiveProperty<string> _description = new();
        private readonly ReactiveProperty<string> _name = new();
        private List<IPlayerStatsViewModel> _viewModels = new List<IPlayerStatsViewModel>();
        private readonly Subject<IPlayerStatsViewModel> _statAdded = new();
        private readonly Subject<IPlayerStatsViewModel> _statRemoved = new();


        private readonly PlayerLevel _playerLevel;
        private readonly PlayerStatInfo _playerStatInfo;
        private readonly ViewModelFactory _viewModelsFactory;
        private readonly UserInfo _userUnfo;

        private readonly CompositeDisposable _disposable = new();

     
        public PlayerViewModel(PlayerLevel playerLevel, PlayerStatInfo playerStatInfo, ViewModelFactory viewModelFactory, UserInfo userInfo)
        {
            _playerLevel = playerLevel;
            _playerStatInfo = playerStatInfo;
            _viewModelsFactory = viewModelFactory;
            _userUnfo = userInfo;   

            _playerLevel.CurrentLevel.Subscribe(SetLevelValue)
                .AddTo(_disposable);

            _playerLevel.CurrentExperience.Subscribe(SetExperienceValue) 
                .AddTo(_disposable);

            _playerLevel.CanLevelUp
                .Subscribe(UpdateCanLevelUp)
                .AddTo(_disposable);

     
            _playerStatInfo.OnStatAdded
               .Subscribe(AddStat)
               .AddTo(_disposable);

            _playerStatInfo.OnStatRemoved
                .Subscribe(RemoveStat)
                .AddTo(_disposable);

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

        public void Dispose()
        {
            _disposable.Dispose();  
        }
    }
}
