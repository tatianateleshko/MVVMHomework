using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public interface IPlayerViewModel : IViewModel, IDisposable
    {
        ReadOnlyReactiveProperty<Sprite> PlayerIcon { get; }
        ReadOnlyReactiveProperty<string> Name { get;}
        ReadOnlyReactiveProperty<string> Description { get;}
        ReadOnlyReactiveProperty<string> Experience { get; }
        ReadOnlyReactiveProperty<string> Level { get;}       
        ReadOnlyReactiveProperty<bool> CanLevelUp { get; }

        IReadOnlyList<IPlayerStatsViewModel> PlayerStatsViewModels { get; }
        Observable<IPlayerStatsViewModel> StatAdded { get; }
        Observable<IPlayerStatsViewModel> StatRemoved { get; }

        void LevelUp();

    }

}
