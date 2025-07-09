using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private VehicleColorConfig vehicleColorConfig;
    [SerializeField] private List<MeshRenderer> bodyMaterials;
    [SerializeField] private List<MeshRenderer> wheelMaterials;
    [SerializeField] private List<MeshRenderer> steeringMaterials;
    [SerializeField] private List<MeshRenderer> tubeMaterials;


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        UpdateSkin(vehicleColorConfig.BodyColor.Value);
        UpdateSkin(vehicleColorConfig.WheelColor.Value);
        UpdateSkin(vehicleColorConfig.TubeColor.Value);
        UpdateSkin(vehicleColorConfig.SteeringColor.Value);
    }

    public void UpdateSkin(ColorConfig config)
    {
        switch (config.PartType)
        {
            case PartType.Body:
                UpdateBody(config.Color);
                break;
            case PartType.Wheels:
                UpdateWheels(config.Color);
                break;
            case PartType.Steering:
                UpdateSteering(config.Color);
                break;
            case PartType.Tube:
                UpdateTube(config.Color);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpdateBody(Color color)
    {
        bodyMaterials.ForEach(m=>m.material.color = color);
    }

    private void UpdateWheels(Color color)
    {
        wheelMaterials.ForEach(m=>m.material.color = color);
    }

    private void UpdateSteering(Color color)
    {
        steeringMaterials.ForEach(m=>m.material.color = color);
    }

    private void UpdateTube(Color color)
    {
        tubeMaterials.ForEach(m=>m.material.color = color);
    }
}
