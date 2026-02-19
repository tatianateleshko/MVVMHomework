using R3;
using System;

namespace UI
{
    public interface IPlayerStatsViewModel : IViewModel , IDisposable
    {
        string Name { get; }
        ReadOnlyReactiveProperty<string> StatValue { get; }
    }

}

