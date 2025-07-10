using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameInstaller:MonoInstaller
    {
        [SerializeField] private GameplayObserver observer;
        [SerializeField] private VehicleConfig vehicleConfig;
        [SerializeField] private CheckpointBehaviour checkpointBehaviour;
        public override void InstallBindings()
        {
            Container.BindInstance(vehicleConfig).AsSingle();
            Container.BindInstance(checkpointBehaviour).AsSingle();
            Container.BindInstance(observer).AsSingle();
            
            Container.Bind<LeaderboardService>().AsSingle();
            
            Container.DeclareSignal<LapFinishedSignal>();
            Container.DeclareSignal<GameplayStateChangedSignal>();
        }
    }
}