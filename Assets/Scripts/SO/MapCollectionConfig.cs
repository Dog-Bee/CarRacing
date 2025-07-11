using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[Serializable]
public class MapConfig
{
    public bool isUnlocked;
    public int price;
    public Sprite MapSprite;
    [SceneName]
    public string SceneName;
}

[CreateAssetMenu(fileName = "MapCollectionData", menuName = "MapCollectionData/MapCollectionData")]
public class MapCollectionConfig : ScriptableObject
{
    [SerializeField] private List<MapConfig> mapConfigs;
    
    public JsonSavableValue<List<MapConfig>> MapConfigJson{get; private set;}
    
    public List<MapConfig> MapConfigs => mapConfigs;

    public void Init()
    {
        Reset();
        MapConfigJson = new JsonSavableValue<List<MapConfig>>("MapCollectionData",mapConfigs);
        mapConfigs = MapConfigJson.Value;
    }

    public void Reset()
    {
        mapConfigs.ForEach(m=>m.isUnlocked = false);
        mapConfigs[0].isUnlocked = true;
    }

    public void SaveMapConfigs(MapConfig config)
    {
        mapConfigs.Find(m=>m.SceneName==config.SceneName).isUnlocked = true;
        MapConfigJson.Value = mapConfigs;
    }

    public MapConfig getMapConfig(string sceneName)
    {
        return mapConfigs.Find(m=>m.SceneName==sceneName);
    }
}
