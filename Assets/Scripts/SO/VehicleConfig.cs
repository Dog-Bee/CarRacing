using UnityEngine;

[CreateAssetMenu(fileName = "VehicleConfig", menuName = "Config/VehicleConfig")]
public class VehicleConfig : ScriptableObject
{
    [field: Header("Stat Config")]
    [field: SerializeField] public float MaxSpeed { get; private set; } = 20f;
    [field: SerializeField] public float Acceleration { get; private set; } = 10f;
    [field: SerializeField] public float TurnSpeed { get; private set; } = 100f;
    [field: SerializeField] public float Drag { get; private set; } = .5f;
    [field: SerializeField] public float Gravity{ get; private set; } = 20f;
    [field: SerializeField] public float SideFriction{ get; private set; } = .1f;
    [field: SerializeField] public float ReachDistance{ get; private set; } = 15f;
    [field: SerializeField] public float BoostMaxCount{ get; private set; } = 10f;
    [field: SerializeField] public float BoostUsage{ get; private set; } = 10f;
    [field: SerializeField] public float BoostDelay{ get; private set; } = 3f;

    [field: Header("Visual Config")]
    [field: SerializeField] public float WheelRotationSpeed { get; private set; } = 360f;
    [field: SerializeField] public float WheelRotationAngle{ get; private set; } = 25f;
    [field: SerializeField] public float TiltAmount { get; private set; } = 10f;
    [field: SerializeField] public float SteeringWheelRotationSpeed { get; private set; } = 40f;
    [field: SerializeField] public Vector3 FollowOffset { get; private set; } =new (0,0.4f,0);
    [field: SerializeField] public LayerMask GroundMask { get; private set; }
    
    

}
