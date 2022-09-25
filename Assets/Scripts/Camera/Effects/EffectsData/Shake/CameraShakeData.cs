using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CameraShakeData", menuName = "ScriptableObjects/Camera/CameraEffect/CameraShake", order = 1)]
public class CameraShakeData : CameraEffectData
{
    public float cameraShakeIntensity;
    public float cameraShakeAmount;
}
