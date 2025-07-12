using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSkinChanger : ASkinChanger
{
    protected override void Init()
    {
        UpdateSkin(vehicleColorConfig.BodyColorConfigs.Value[Random.Range(0, vehicleColorConfig.BodyColorConfigs.Value.Count)]);
        UpdateSkin(vehicleColorConfig.WheelColorConfigs.Value[Random.Range(0, vehicleColorConfig.WheelColorConfigs.Value.Count)]);
        UpdateSkin(vehicleColorConfig.TubeColorConfigs.Value[Random.Range(0, vehicleColorConfig.TubeColorConfigs.Value.Count)]);
        UpdateSkin(vehicleColorConfig.SteeringColorConfigs.Value[Random.Range(0, vehicleColorConfig.SteeringColorConfigs.Value.Count)]);
    }
}
