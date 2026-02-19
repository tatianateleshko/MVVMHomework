using UnityEngine;
using Zenject;

namespace Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            new PlayerInstaller(Container);
        }
    }

}
