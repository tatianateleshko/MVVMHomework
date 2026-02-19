using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public interface IPlayerViewModel : IViewModel, IDisposable
    {
        Sprite PlayerIcon { get; }
        string Name { get;}

        string Description { get;}

        ReadOnlyReactiveProperty<string> Experience { get; }
        ReadOnlyReactiveProperty<string> Level { get;}       
        ReadOnlyReactiveProperty<bool> CanLevelUp { get; }

        IReadOnlyList<IPlayerStatsViewModel> PlayerStatsViewModels { get; }
        Observable<IPlayerStatsViewModel> StatAdded { get; }
        Observable<IPlayerStatsViewModel> StatRemoved { get; }

        Observable<Sprite> OnPlayerImageChange { get; }
        Observable<string> OnPlayerNameChange { get; }


        void LevelUp();

    }

}
