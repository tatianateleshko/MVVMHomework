using UI;
using Player;


namespace Factory
{
    public class ViewModelFactory
    {
        public PlayerViewModel Create(UserInfo userInfo)
        {
            return new PlayerViewModel(userInfo );
        }
        public PlayerLevelViewModel Create(PlayerLevel playerLevel)
        {
            return new PlayerLevelViewModel(playerLevel);
        }

        public PlayerStatViewModel Create(PlayerStat playerStat)
        {
            return new PlayerStatViewModel(playerStat);
        }

        public PlayerStatsViewModel Create(PlayerStatInfo playerStatInfo, ViewModelFactory viewModelFactory)
        {
            return new PlayerStatsViewModel(playerStatInfo, viewModelFactory);
        }
    }

}
