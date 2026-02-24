using R3;

namespace UI
{
    public interface IPlayerLevelViewModel : IViewModel
    {
        ReadOnlyReactiveProperty<string> Level { get; }
        ReadOnlyReactiveProperty<string> Experience { get; }
        ReadOnlyReactiveProperty<bool> CanLevelUp { get; }
        void LevelUp();
    }

}
