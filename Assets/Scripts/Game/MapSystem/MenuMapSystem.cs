using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MenuMapSystem : MonoBehaviour
{
    [SerializeField] private MapCollectionConfig mapCollectionConfig;
    [SerializeField] private MapBuyPopUp mapBuyPopUp;
    [SerializeField] private List<MapLoadButton> mapLoadButtons;

    private SignalBus _signalBus;
    private SceneLoader _sceneLoader;

    [Inject] private void Construct(SignalBus signalBus, SceneLoader sceneLoader)
    {
        _signalBus = signalBus;
        _sceneLoader = sceneLoader;
        mapCollectionConfig.Init();
        _signalBus.Subscribe<TryMapLoadSignal>(OnTryLoadMap);
        _signalBus.Subscribe<MapUnlockSignal>(OnMapUnlock);
        InitButtons();
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<TryMapLoadSignal>(OnTryLoadMap);
        _signalBus.Unsubscribe<MapUnlockSignal>(OnMapUnlock);
    }


    private void InitButtons()
    {
        List<MapConfig> configs = mapCollectionConfig.MapConfigJson.Value;
        for (int i = 0; i < configs.Count; i++)
        {
            mapLoadButtons[i].Init(configs[i],_signalBus);
        }
    }

    private void OnMapUnlock(MapUnlockSignal signal)
    {
        MapConfig config = signal.MapConfig;
        
        mapCollectionConfig.SaveMapConfigs(config);
        mapLoadButtons.Find(b=>b.MapConfig.SceneName == config.SceneName).UpdateView(config);
    }


    private void OnTryLoadMap(TryMapLoadSignal signal)
    {
        MapConfig mapConfig = signal.Config;

        if (!mapConfig.isUnlocked)
        {
            mapBuyPopUp.Activate(mapConfig);
            return;
        }
        mapCollectionConfig.SaveMapConfigs(mapConfig);
        _sceneLoader.LoadScene(mapConfig.SceneName);
        
    }
    
    
    
}
