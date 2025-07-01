using UnityEngine;
using Zenject;

public class BotController : MonoBehaviour
{
    [SerializeField] private Transform surfaceTransform;
    [SerializeField] private Transform sphereTransform;
    [SerializeField]  private Rigidbody rigidBody;
    [SerializeField] private ARaceTracker raceTracker;

    private float _acceleration;
    private float _turnSpeed;
    private float _maxSpeed;
    private float _gravity;
    private Vector3 _followOffset;
    private LayerMask _groundLayer;
    
    private float _currentRotation, _targetRotation;
    private float _currentSpeed, _targetSpeed;
    
    [Inject]
    private void Construct(VehicleConfig vehicleConfig, CheckpointBehaviour checkpointBehaviour)
    {
        _acceleration = vehicleConfig.Acceleration;
        _turnSpeed = vehicleConfig.TurnSpeed;
        _maxSpeed = vehicleConfig.MaxSpeed;
        _gravity = vehicleConfig.Gravity;
        _groundLayer = vehicleConfig.GroundMask;
        _followOffset = vehicleConfig.FollowOffset;
        raceTracker.ReachDistance = vehicleConfig.ReachDistance;
    }

    private void FixedUpdate()
    {
        if (raceTracker.IsStop) return;
        transform.position = sphereTransform.position - _followOffset;
        Calculations();
        ForceCalculations();
        UpdateRotationBySurface();
        UpdateMainRotation();
    }
    private void UpdateMainRotation()
    {
        if (rigidBody.velocity.magnitude > 0.1f)
        {
            Vector3 targetEuler =  transform.eulerAngles + Vector3.up * _currentRotation;
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetEuler, Time.fixedDeltaTime*5f);
        }
    }

    private void UpdateRotationBySurface()
    {
        if(Physics.Raycast(transform.position,Vector3.down,out RaycastHit hit,1,_groundLayer))
        {
            surfaceTransform.up = Vector3.Lerp(surfaceTransform.up, hit.normal, Time.deltaTime * 8f);
            surfaceTransform.Rotate(0,transform.eulerAngles.y,0);
        }
    }

    private void ForceCalculations()
    {
        Vector3 forwardForce = surfaceTransform.forward * _currentSpeed;
        Vector3 sideForce = surfaceTransform.right * _currentRotation;

        if (rigidBody.velocity.magnitude < _maxSpeed/2)
        {
            rigidBody.AddForce(forwardForce+sideForce,ForceMode.Acceleration);
        }
        
        rigidBody.AddForce(Vector3.down * _gravity, ForceMode.Acceleration);
    }

    private void Calculations()
    {
        Vector3 direction = (raceTracker.CurrentCheckpoint - transform.position).normalized;
        float angle = Vector3.SignedAngle(transform.forward,direction,Vector3.up);
        
        _targetSpeed = _acceleration*.7f;
        _currentSpeed = Mathf.SmoothStep(_currentSpeed, _targetSpeed, Time.deltaTime*6f);
        _targetSpeed = 0;
        
        _targetRotation = Mathf.Clamp(angle, -1, 1)*_turnSpeed;
        _currentRotation = Mathf.Lerp(_currentRotation, _targetRotation, Time.deltaTime*12f);
        _targetRotation = 0;
    }
    
    
    
}
