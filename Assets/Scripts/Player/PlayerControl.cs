using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))] 
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private VehicleView view;

    private PlayerInput _input;
    private VehicleConfig _config;
    
    [Inject] 
    private void Construct(PlayerInput input, VehicleConfig config)
    {
        _input = input;
        _config = config;
        
        rigidBody.drag = _config.Drag;
        view.Init(_config.WheelRotationSpeed, _config.TiltAmount, _config.SteeringWheelRotationSpeed,_config.WheelRotationAngle);
    }

    private void FixedUpdate()
    {
        Vector3 forwardForce = transform.forward * (_input.Throttle * _config.Acceleration);

        if (rigidBody.velocity.magnitude < _config.MaxSpeed)
        {
            rigidBody.AddForce(forwardForce, ForceMode.Acceleration);
        }
        
        float turnAmount = _input.Steering * _config.TurnSpeed * Time.fixedDeltaTime;
        Quaternion turnOffset = Quaternion.Euler(0,turnAmount,0);
        rigidBody.MoveRotation(rigidBody.rotation * turnOffset);
        
        view.UpdateView(_input.Throttle, _input.Steering, rigidBody.velocity.magnitude);
    }
    
}