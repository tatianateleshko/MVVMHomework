using R3;
using System;

using UnityEngine;

namespace UI
{
    public interface IPlayerViewModel : IViewModel, IDisposable
    {
        ReadOnlyReactiveProperty<Sprite> PlayerIcon { get; }
        ReadOnlyReactiveProperty<string> Name { get;}
        ReadOnlyReactiveProperty<string> Description { get;}
    }

}
