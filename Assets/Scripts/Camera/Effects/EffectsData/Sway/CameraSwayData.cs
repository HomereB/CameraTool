using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraSwayData", menuName = "ScriptableObjects/CameraEffect/CameraSway", order = 2)]

public class CameraSwayData : CameraEffectData
{
    public float cameraSwayAmount;
    public float cameraUpwardSwayIntensity;
    public float cameraRightwardSwayIntensity;
}
