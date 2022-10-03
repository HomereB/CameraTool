using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorStatic : CameraAnchor
{
    public override void MoveCameraAnchor(Vector3 direction) {}

    // Start is called before the first frame update
    void Start()
    {
        transform.position = anchorData.initialPosition;
    }
}
