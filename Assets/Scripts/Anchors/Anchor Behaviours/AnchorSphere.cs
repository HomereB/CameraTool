using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorSphere : CameraAnchor
{
    private Vector3 desiredPosition;
    private Vector3 sphereCenterPos;
    private float parentDistanceRatio;

    public override void MoveCameraAnchor(Vector3 direction)
    {
/*        Vector3 currentPos = transform.position;
        Quaternion horizontalRotation = Quaternion.AngleAxis(direction.x, anchorData.parent.up);
        Debug.Log(horizontalRotation);
        Quaternion verticalRotation = Quaternion.AngleAxis(direction.y, transform.right);
        float previewedAngle = Vector3.Angle(verticalRotation * currentOffset, currentTarget.up);
        if (previewedAngle > cameraData.verticalRotationThresholdUp || previewedAngle < cameraData.verticalRotationThresholdDown)
        {
            verticalRotation = Quaternion.identity;
        }
        currentOffset = horizontalRotation * verticalRotation * currentOffset;*/
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = anchorData.initialPosition;
        //parentDirection = (anchorData.parent.position - transform.position);
        parentDistanceRatio = 1;
    }
}
