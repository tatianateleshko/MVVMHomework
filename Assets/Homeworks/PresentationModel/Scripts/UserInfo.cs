using System;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    public sealed class UserInfo
    {
        public ReadOnlyReactiveProperty<string> CurrentName => _name;
        private ReactiveProperty<string> _name = new();

        public ReadOnlyReactiveProperty<string> Description => _description;
        private ReactiveProperty<string> _description = new();

        public ReadOnlyReactiveProperty<Sprite> Icon => _icon;
        private ReactiveProperty<Sprite> _icon = new();

        public void ChangeName(string name)
        {
            _name.Value = name;
        }

        public void ChangeDescription(string description)
        {
            _description.Value = description;
        }

        public void ChangeIcon(Sprite icon)
        {
           _icon.Value = icon;
        }
    }
}