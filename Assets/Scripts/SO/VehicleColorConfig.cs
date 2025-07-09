using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ColorConfig
{
    public PartType PartType;
    public bool IsUnlocked;
    public int Price;
    public Color Color;
}

public enum PartType
{
    Body,
    Wheels,
    Steering,
    Tube
}

[CreateAssetMenu(fileName = "VehicleColorConfig", menuName = "VehicleColorConfig/VehicleColorConfig")]
public class VehicleColorConfig : ScriptableObject
{
    [SerializeField] private List<ColorConfig> bodyColorConfigs;
    [SerializeField] private List<ColorConfig> wheelColorConfigs;
    [SerializeField] private List<ColorConfig> steeringColorConfigs;
    [SerializeField] private List<ColorConfig> tubeColorConfigs;

    public JsonSavableValue<List<ColorConfig>> BodyColorConfigs;
    public JsonSavableValue<List<ColorConfig>> WheelColorConfigs;
    public JsonSavableValue<List<ColorConfig>> SteeringColorConfigs;
    public JsonSavableValue<List<ColorConfig>> TubeColorConfigs;
    
    public JsonSavableValue<ColorConfig> BodyColor { get; private set; }
    public JsonSavableValue<ColorConfig> WheelColor { get; private set; }
    public JsonSavableValue<ColorConfig> SteeringColor { get; private set; }
    public JsonSavableValue<ColorConfig> TubeColor { get; private set; }


    public void Init()
    {
        
            BodyColor = new JsonSavableValue<ColorConfig>("CurrenBodyColor", bodyColorConfigs[0]);
            WheelColor = new JsonSavableValue<ColorConfig>("CurrenWheelColor", wheelColorConfigs[0]);
            SteeringColor = new JsonSavableValue<ColorConfig>("CurrenSteeringColor", steeringColorConfigs[0]);
            TubeColor = new JsonSavableValue<ColorConfig>("CurrenTubeColor", tubeColorConfigs[0]); 
        

        
            BodyColorConfigs = new JsonSavableValue<List<ColorConfig>>("BodyColorConfigs", bodyColorConfigs);
            WheelColorConfigs = new JsonSavableValue<List<ColorConfig>>("WheelColorConfigs", wheelColorConfigs);
            SteeringColorConfigs = new JsonSavableValue<List<ColorConfig>>("SteeringColorConfigs", steeringColorConfigs);
            TubeColorConfigs = new JsonSavableValue<List<ColorConfig>>("TubeColorConfigs", tubeColorConfigs);
       
            bodyColorConfigs = BodyColorConfigs.Value;
            wheelColorConfigs = WheelColorConfigs.Value;
            steeringColorConfigs = SteeringColorConfigs.Value;
            tubeColorConfigs = TubeColorConfigs.Value;
        
    }
    
    public void SaveColor(ColorConfig config)
    {
        switch (config.PartType)
        {
            case PartType.Body:
                ChangeBodyColor(config);
                break;
            case PartType.Wheels:
                ChangeWheelColor(config);
                break;
            case PartType.Steering:
                ChangeSteeringColor(config);
                break;
            case PartType.Tube:
                ChangeTubeColor(config);
                break;
        }
    }

    private void ChangeBodyColor(ColorConfig config)
    {
        int index = bodyColorConfigs.IndexOf(config);
        bodyColorConfigs[index].IsUnlocked = true;
        BodyColor.Value = bodyColorConfigs[index];
        BodyColorConfigs.Value = bodyColorConfigs;
    }

    private void ChangeWheelColor(ColorConfig config)
    {
        int index = wheelColorConfigs.IndexOf(config);
        wheelColorConfigs[index].IsUnlocked = true;
        WheelColor.Value = wheelColorConfigs[index];
        WheelColorConfigs.Value = wheelColorConfigs;
    }

    private void ChangeSteeringColor(ColorConfig config)
    {
        int index = steeringColorConfigs.IndexOf(config);
        steeringColorConfigs[index].IsUnlocked = true;
        SteeringColor.Value = steeringColorConfigs[index];
        SteeringColorConfigs.Value = steeringColorConfigs;
    }

    private void ChangeTubeColor(ColorConfig Config)
    {
        int index = tubeColorConfigs.IndexOf(Config);
        tubeColorConfigs[index].IsUnlocked = true;
        TubeColor.Value = tubeColorConfigs[index];
        TubeColorConfigs.Value = tubeColorConfigs;
    }

}
