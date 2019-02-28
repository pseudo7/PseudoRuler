using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroStablizer : MonoBehaviour
{
    public Vector3 gyroRot;
    void Start()
    {
        Input.gyro.enabled = true;        
    }

    void Update()
    {
        //transform.rotation = Input.gyro.attitude * Quaternion.Euler(gyroRot);
    }
}
