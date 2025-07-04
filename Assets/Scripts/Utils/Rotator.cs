using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    
    [SerializeField] private Vector3 axisRotation;
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        transform.Rotate(axisRotation, rotationSpeed * Time.deltaTime);
    }
}
