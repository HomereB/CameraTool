using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraAnchor : MonoBehaviour
{
    public AnchorData anchorData;
    private float currentSpeed;
    public abstract void MoveCameraAnchor(Vector3 direction);
    public float CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }

}
