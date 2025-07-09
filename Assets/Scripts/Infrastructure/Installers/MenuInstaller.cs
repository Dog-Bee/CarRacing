using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField] private SkinSystem skinSystem;
    
    public override void InstallBindings()
    {
        Container.BindInstance(skinSystem).AsSingle();
        
        Container.DeclareSignal<MenuStateChangeSignal>();
        Container.DeclareSignal<TryColorChangeSignal>();
        Container.DeclareSignal<ColorChangedSignal>();
    }
}