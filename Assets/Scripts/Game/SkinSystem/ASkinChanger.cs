using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASkinChanger : MonoBehaviour
{
    [SerializeField] protected VehicleColorConfig vehicleColorConfig;
    [SerializeField] protected List<MeshRenderer> bodyMaterials;
    [SerializeField] protected List<MeshRenderer> wheelMaterials;
    [SerializeField] protected List<MeshRenderer> steeringMaterials;
    [SerializeField] protected List<MeshRenderer> tubeMaterials;


    private void Start()
    {
        Init();
    }

    protected abstract void Init();
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
