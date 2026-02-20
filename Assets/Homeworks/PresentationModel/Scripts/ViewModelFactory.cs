using UI;
using Player;


namespace Factory
{
    public class ViewModelFactory
    {
        public PlayerViewModel Create(PlayerLevel playerLevel, PlayerStatInfo playerStatInfo, ViewModelFactory viewModelFactory, UserInfo userInfo)
        {
            return new PlayerViewModel(playerLevel, playerStatInfo, viewModelFactory, userInfo );
        }

        public PlayerStatsViewModel Create(PlayerStat playerStat)
        {
            return new PlayerStatsViewModel(playerStat);
        }
    }

}
