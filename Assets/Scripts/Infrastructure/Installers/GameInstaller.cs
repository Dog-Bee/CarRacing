using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameInstaller:MonoInstaller
    {
        [SerializeField] private VehicleConfig vehicleConfig;
        public override void InstallBindings()
        {
            Container.BindInstance(vehicleConfig).AsSingle();
        }
    }
}