using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using R3;
using UI.Utils;


namespace Player
{
    public sealed class PlayerStatInfo
    {
        public ReactiveCommand <PlayerStat> OnStatAdded =  new();
        public ReactiveCommand <PlayerStat> OnStatRemoved = new();

        public readonly HashSet<PlayerStat> stats = new();

        private int maxStatNumber = 6;
        public void AddStat(StatsType statsType, int value)
        {
            PlayerStat playerStat = new PlayerStat(statsType.GetLocName(), value);
            if (this.stats.Add(playerStat) && stats.Count <= maxStatNumber)
            {
                this.OnStatAdded?.Execute(playerStat);
            }
        }

        public void RemoveStat(PlayerStat playerStat)
        {
            if (this.stats.Remove(playerStat))
            {
                this.OnStatRemoved?.Execute(playerStat);
            }
        }

        public PlayerStat GetStat(StatsType type)
        {
            foreach (var stat in this.stats)
            {
                if (stat.Name == type.GetLocName())
                {
                    return stat;
                }
            }

            throw new Exception($"Stat {type.GetLocName()} is not found!");
        }

        public PlayerStat[] GetStats()
        {
            return this.stats.ToArray();
        }

        public void ChangeValue(PlayerStat playerStat, int value)
        {
            playerStat.ChangeValue(value);
        }
    }
}