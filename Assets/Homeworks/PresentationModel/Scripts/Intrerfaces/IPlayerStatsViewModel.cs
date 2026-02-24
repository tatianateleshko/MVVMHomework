using R3;
using System.Collections.Generic;

namespace UI
{
    public interface IPlayerStatsViewModel : IViewModel
    {
        IReadOnlyList<IPlayerStatViewModel> PlayerStatsViewModels { get; }
        Observable<IPlayerStatViewModel> StatAdded { get; }
        Observable<IPlayerStatViewModel> StatRemoved { get; }
    }
}

