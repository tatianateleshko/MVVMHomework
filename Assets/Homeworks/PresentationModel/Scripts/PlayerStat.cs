using System;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Player
{
    public sealed class PlayerStat
    {       
        public string Name { get; private set; }
        public ReadOnlyReactiveProperty <int> Value => _value;

        private readonly ReactiveProperty<int> _value = new ();

        public PlayerStat(string name, int value)
        {
            Name = name;
            _value.Value = value;
        }

        public void ChangeValue(int value)
        {
            _value.Value  = value;
        }
    }
}