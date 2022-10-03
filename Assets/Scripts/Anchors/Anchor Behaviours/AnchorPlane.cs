using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPlane : CameraAnchor
{
    private Vector3 desiredPosition;
    [SerializeField]
    private Vector3 directionPlaneX;
    [SerializeField]
    private Vector3 directionPlaneY;

    public override void MoveCameraAnchor(Vector3 direction)
    {
        Vector3 movement = direction.x * directionPlaneX + direction.y * directionPlaneY;
        desiredPosition += movement * anchorData.initialSpeed * Time.deltaTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = anchorData.initialPosition;
        desiredPosition = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,desiredPosition, anchorData.lerpIntensity);
    }
}
