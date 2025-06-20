using System.Collections.Generic;
using UnityEngine;

public class VehicleView : MonoBehaviour
{
   [SerializeField] private Transform body;
   [SerializeField] private List<Transform> frontWheels;

   private float _wheelRotationSpeed;
   private float _tiltAmount;
   private float _steeringWheelRotationSpeed;
   private float _steeringWheelRotationAngle;
   
   public void Init(float wheelRotationSpeed, float tiltAmount, float steeringWheelRotationSpeed,float wheelRotationAngle)
   {
      _wheelRotationSpeed = wheelRotationSpeed;
      _tiltAmount = tiltAmount;
      _steeringWheelRotationSpeed = steeringWheelRotationSpeed;
      _steeringWheelRotationAngle = wheelRotationAngle;
      
      
   }

   public void UpdateView(float throttle, float steering, float speed)
   {
      float direction =speed* _wheelRotationSpeed*Time.deltaTime* Mathf.Sign(throttle);
      
      FrontWheelRotation(direction,steering);
      
      float targetX = -steering * _tiltAmount;
      float targetZ = -throttle * _tiltAmount;
      body.localRotation = Quaternion.Euler(targetX,0,targetZ);
      
      
   }

   public void FrontWheelRotation(float rotationDirection,float steering)
   {
      if (frontWheels.Count < 2) return;
      float targetY = steering * _steeringWheelRotationAngle;
      
      for (int i = 0; i < frontWheels.Count; i++)
      {
         frontWheels[i].localRotation = Quaternion.Euler(0,targetY,0);
         frontWheels[i].Rotate(Vector3.forward * rotationDirection);
      }
   }
}
