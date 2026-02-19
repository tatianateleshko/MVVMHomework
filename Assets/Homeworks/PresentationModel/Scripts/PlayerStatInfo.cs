using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using R3;
using Sirenix.OdinInspector;
using UI.Utils;
using Zenject;


namespace Player
{
    public sealed class PlayerStatInfo
    {
        public ReactiveCommand <PlayerStat> OnStatAdded =  new();
        public ReactiveCommand <PlayerStat> OnStatRemoved = new();

        public readonly HashSet<PlayerStat> stats = new();

        public void AddStat(StatsType statsType, int value)
        {
            PlayerStat playerStat = new PlayerStat(statsType.GetLocName(), value);
            if (this.stats.Add(playerStat))
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

        public PlayerStat GetStat(string name)
        {
            foreach (var stat in this.stats)
            {
                if (stat.Name == name)
                {
                    return stat;
                }
            }

            throw new Exception($"Stat {name} is not found!");
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