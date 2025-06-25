using System;
using UnityEngine;
using Zenject;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform sphereTransform;
    [SerializeField] private Transform surfaceTransform;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private VehicleView view;

    private PlayerInput _input;
    private VehicleConfig _config;

    private float _targetSpeed, _currentSpeed,_boostSpeed;
    private float _targetRotation, _currentRotation;
    private bool _isGrounded;
    
    public float CurrentSpeed => _currentSpeed;
    
    [Inject] 
    private void Construct(PlayerInput input, VehicleConfig config)
    {
        _input = input;
        _config = config;
        
        rigidBody.drag = _config.Drag;
        view.Init(_config.WheelRotationSpeed, _config.TiltAmount,_config.WheelRotationAngle);
    }

    private void Start()
    {
        view.Boost(false);
    }

    private void Update()
    {
        Calculations();
        view.UpdateView(_input.Throttle, _input.Steering, rigidBody.velocity.magnitude);
        view.Boost(_input.IsDrifting);
    }

    private void FixedUpdate()
    {        
        transform.position = sphereTransform.position-_config.FollowOffset;
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
       if(Physics.Raycast(transform.position,Vector3.down,out RaycastHit hit,1,_config.GroundMask))
       {
          surfaceTransform.up = Vector3.Lerp(surfaceTransform.up, hit.normal, Time.deltaTime * 8f);
          surfaceTransform.Rotate(0,transform.eulerAngles.y,0);
       }
    }

    private void ForceCalculations()
    {
        Vector3 forwardForce = surfaceTransform.forward *_currentSpeed;
        Vector3 sideForce = -surfaceTransform.right *_currentRotation;
        float speedLimit = _input.IsDrifting ? _config.Acceleration * 2 * 2 : _config.Acceleration * 2;
        
        if (rigidBody.velocity.magnitude < speedLimit)
        {
            rigidBody.AddForce(forwardForce + sideForce, ForceMode.Acceleration);
        }
        
		rigidBody.AddForce(Vector3.down*_config.Gravity, ForceMode.Acceleration);
    }

    private void Calculations()
    {
        _boostSpeed = _input.IsDrifting? _config.Acceleration:0;
        _targetSpeed = _input.Throttle * _config.Acceleration + Mathf.Abs(_input.Steering)*_config.Acceleration/2+_boostSpeed;
        _targetRotation = _input.Steering * _config.TurnSpeed;

        _currentSpeed = Mathf.SmoothStep(_currentSpeed, _targetSpeed, Time.deltaTime * 12f);
        _targetSpeed = 0;
        _currentRotation = Mathf.Lerp(_currentRotation, _targetRotation, Time.deltaTime * 4f);
        _targetRotation = 0;
    }
    
}