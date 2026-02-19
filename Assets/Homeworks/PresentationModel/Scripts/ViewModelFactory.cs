using UI;
using Player;


namespace Factory
{
    public class ViewModelFactory
    {
        public PlayerViewModel Create(PlayerData playerData, PlayerLevel playerLevel, PlayerStatInfo playerStatInfo, ViewModelFactory viewModelFactory)
        {
            return new PlayerViewModel(playerData, playerLevel, playerStatInfo, viewModelFactory);
        }

        public PlayerStatsViewModel Create(PlayerStat playerStat)
        {
            return new PlayerStatsViewModel(playerStat);
        }
    }

}
