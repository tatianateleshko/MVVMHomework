using R3;
using System;

namespace UI
{
    public interface IPlayerStatViewModel : IViewModel , IDisposable
    {
        string Name { get; }
        ReadOnlyReactiveProperty<string> StatValue { get; }
    }

}

