using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private SceneLoader sceneLoader;
    public override void InstallBindings()
    {
        Container.BindInstance(sceneLoader).AsSingle();
        Container.Bind<PlayerInput>().AsSingle();
        Container.Bind<DefaultInput>().AsSingle();
        
        SignalBusInstaller.Install(Container);
        
        Container.DeclareSignal<LapFinishedSignal>();
        Container.DeclareSignal<CoinCollectedSignal>();
    }
    
    
}
