using System.Collections.Generic;
using UnityEngine;

public class VehicleView : MonoBehaviour
{
   [SerializeField] private Transform body;
   [SerializeField] private List<Transform> frontWheels;

   private float _wheelRotationSpeed;
   private float _tiltAmount;
   private float _wheelRotationAngle;
   private Quaternion targetTilt;
   public void Init(float wheelRotationSpeed, float tiltAmount,float wheelRotationAngle)
   {
      _wheelRotationSpeed = wheelRotationSpeed;
      _tiltAmount = tiltAmount;
      _wheelRotationAngle = wheelRotationAngle;
      
      
   }

   public void UpdateView(float throttle, float steering, float speed)
   {
      float direction =speed* _wheelRotationSpeed*Time.fixedDeltaTime* Mathf.Sign(throttle);
      
      FrontWheelRotation(direction,steering);
      
      float targetX = -steering * _tiltAmount;
      float targetZ = -throttle * _tiltAmount;
      targetTilt.eulerAngles = new Vector3(targetX,0,targetZ);
      body.localRotation = Quaternion.Lerp(body.localRotation,targetTilt, Time.fixedDeltaTime *5f);
      
      
   }

   public void FrontWheelRotation(float rotationDirection,float steering)
   {
      if (frontWheels.Count < 2) return;
      float targetY = steering * _wheelRotationAngle;
      
      for (int i = 0; i < frontWheels.Count; i++)
      {
         frontWheels[i].localRotation = Quaternion.Euler(0,targetY,0);
         frontWheels[i].Rotate(Vector3.forward * rotationDirection);
      }
   }
}
