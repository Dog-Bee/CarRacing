using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinChanger : ASkinChanger
{
    protected override void Init()
    {
        UpdateSkin(vehicleColorConfig.BodyColor.Value);
        UpdateSkin(vehicleColorConfig.WheelColor.Value);
        UpdateSkin(vehicleColorConfig.TubeColor.Value);
        UpdateSkin(vehicleColorConfig.SteeringColor.Value);
    }
}