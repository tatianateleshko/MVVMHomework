using Factory;
using Player;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller
    {
        public PlayerInstaller(DiContainer container)
        {
            container.Bind<ViewModelFactory>().AsSingle().NonLazy();
            container.Bind<PlayerLevel>().AsSingle().NonLazy();
            container.Bind<PlayerStatInfo>().AsSingle().NonLazy();
            container.Bind<UserInfo>().AsSingle().NonLazy();
        }
    }

}
