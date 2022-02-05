using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float standardCameraMovementSpeed;
    [SerializeField]
    float standardCameraRotationSpeed;


    [SerializeField]
    Transform currentAnchor;
    [SerializeField]
    Transform currentTarget;

    [SerializeField]
    Vector3 currentPosition;
    [SerializeField]
    Vector3 currentOffset;
    [SerializeField]
    Quaternion currentRotation;

    [SerializeField]
    Vector3 currentDisplacement;
    [SerializeField]
    Quaternion currentSway;



    [SerializeField]
    float verticalRotationThresholdUp;
    [SerializeField]
    float verticalRotationThresholdDown;

    [SerializeField]
    float minZoomThreshold;
    [SerializeField]
    float maxZoomThreshold;

    [SerializeField]
    Camera currentCamera;

    bool isMoving = false;

    public Transform testAnchor;
    public List<Vector3> startpos;
    public List<Vector3> endpos;
    public List<Quaternion> startrot;
    public List<Quaternion> endrot;
    public List<bool> attached;
    public List<float> time;
    public List<float> waitingTime;

    void Awake()
    {
        currentCamera = gameObject.GetComponent<Camera>();
        currentPosition = currentCamera.transform.position;
        currentRotation = currentCamera.transform.rotation;
        if(currentTarget != null)
        {
            SetTarget(currentTarget);
        }
        if (currentAnchor != null)
        {
            SetAnchor(currentAnchor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAnchor != null)
        {
            FollowAnchor();
        }
        if (currentTarget != null)
        {
            LookAtTarget();
        }
        /*        if(Input.GetKeyDown(KeyCode.A) && !isMoving)
                {
                    StartCoroutine(MoveCameraSmooth(currentCamera.transform.position, Vector3.zero + currentOffset, currentCamera.transform.rotation, Quaternion.identity, false, 4f));
                }*/
        /*        if (Input.GetKeyDown(KeyCode.R) && !isMoving)
                {
                    TeleportCamera(Vector3.zero + currentOffset, Quaternion.identity, null);
                }*/
        if (Input.GetKeyDown(KeyCode.E) && !isMoving)
        {
            StartCoroutine(AttachCamera(testAnchor,null, Vector3.zero, 1f));
        }
/*        if (Input.GetKeyDown(KeyCode.T) && !isMoving)
        {
            StartCoroutine(MoveCameraPath(startpos, endpos, startrot, endrot, attached, time, waitingTime));
        }*/
        if ( Input.GetKey(KeyCode.X))
        {
            ZoomCamera(0.1f);
        }
        if (Input.GetKey(KeyCode.C))
        {
            ZoomCamera(-0.1f);
        }
        if(Input.GetKey(KeyCode.L))
        {
            RotateCamera(standardCameraRotationSpeed, 0);
        }
        if (Input.GetKey(KeyCode.J))
        {
            RotateCamera(-standardCameraRotationSpeed, 0);
        }
        if (Input.GetKey(KeyCode.I))
        {
            RotateCamera(0, standardCameraRotationSpeed);
        }
        if (Input.GetKey(KeyCode.K))
        {
            RotateCamera(0, -standardCameraRotationSpeed);
        }

        ComputeTransform();

        //TODO : Handle obstacles (raycast probably)
    }

    //TODO : Check bounds

    private void RotateCamera(float rightward, float upward)
    {   
        //TODO : add horizontal thresholds
        if(currentTarget != null && currentAnchor == null)
        {
            Quaternion horizontalRotation = Quaternion.AngleAxis(rightward, currentTarget.up);
            Debug.Log(horizontalRotation);
            Quaternion verticalRotation = Quaternion.AngleAxis(upward, currentCamera.transform.right);
            float previewedAngle = Vector3.Angle(verticalRotation * currentOffset, currentTarget.up);
            if (previewedAngle > verticalRotationThresholdUp || previewedAngle < verticalRotationThresholdDown)
            {
                verticalRotation = Quaternion.identity;
            }
            currentOffset = horizontalRotation * verticalRotation * currentOffset;
        }
        else if(currentTarget == null)
        {
            Vector3 upVector = currentCamera.transform.up;
            if (currentAnchor != null)
            {
                upVector = currentAnchor.up;
            }
            Quaternion verticalRotation = Quaternion.AngleAxis(upward, currentCamera.transform.right);
            float previewedAngle = Vector3.Angle(verticalRotation * currentCamera.transform.forward, upVector);
            if (previewedAngle > verticalRotationThresholdUp || previewedAngle < verticalRotationThresholdDown)
            {
                verticalRotation = Quaternion.identity;
            }
            Quaternion horizontalRotation = Quaternion.AngleAxis(rightward, Vector3.up);
            currentRotation = verticalRotation * horizontalRotation * currentRotation;
        }

    }

    private void MoveCamera(Vector3 movement)
    {

    }

    private void ZoomCamera(float intensity)
    {
        Vector3 previewedOffset = currentOffset + currentCamera.transform.forward * intensity;     
        if(previewedOffset.magnitude <= maxZoomThreshold && previewedOffset.magnitude >= minZoomThreshold)
        {
            currentOffset = previewedOffset;
        }
    }

    private void ComputeTransform()
    {
        currentCamera.transform.position = currentPosition + currentDisplacement;
        currentCamera.transform.rotation = currentSway * currentRotation;
        currentCamera.transform.position += currentOffset;
        currentDisplacement = Vector3.zero;
        currentSway = Quaternion.identity;
    }

    public void SetDisplacement(Vector3 displacement)
    {
        currentDisplacement = displacement;
    }

    public void SetSway(Quaternion sway)
    {
        currentSway = sway;
    }

    public void SetAnchor(Transform anchor)
    {
        currentAnchor = anchor;
        currentPosition = anchor.transform.position;
    }

    public void SetTarget(Transform target)
    {
        currentTarget = target;
        LookAtTarget();
        if(currentAnchor == null)
        {
            currentOffset = currentPosition - currentTarget.position;
            currentPosition = currentTarget.position;
        }

    }

    private void FollowAnchor()
    {
        currentPosition = currentAnchor.position;
    }

    private void LookAtTarget()
    {
        Vector3 direction = currentTarget.position - currentCamera.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        currentRotation = lookRotation;
    }

    public void TeleportCamera(Vector3 position, Quaternion rotation, Transform cameraAnchor, Transform cameraTarget)
    {
        currentAnchor = cameraAnchor;
        currentTarget = cameraTarget;
        currentCamera.transform.position = position;
        currentRotation = rotation;
    }

    public IEnumerator MoveCameraTo(Vector3 startPosition,Vector3 endPosition, Quaternion startRotation, Quaternion endRotation, Transform cameraAnchor, float time)
    {
        currentAnchor = null;

        isMoving = true;
        float elapsedTime = 0;

        while(elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            currentCamera.transform.position =  Vector3.Lerp(startPosition, endPosition, elapsedTime / time) + currentDisplacement;
            currentRotation = currentSway * Quaternion.Slerp(startRotation, endRotation, elapsedTime / time);
            yield return null;
        }

        currentAnchor = cameraAnchor;
        isMoving = false;
    }

    public IEnumerator MoveCameraPath(List<Vector3> startPositions, List<Vector3> endPositions, List<Quaternion> startRotations, List<Quaternion> endRotations, List<Transform> cameraAnchors, List<float> time, List<float> waitingTime)
    {
        currentAnchor = null;
        isMoving = true;
        for (int i = 0; i < startPositions.Count; i++)
        {
            StartCoroutine(MoveCameraTo(startPositions[i], endPositions[i], startRotations[i], endRotations[i], cameraAnchors[i], time[i]));
            yield return new WaitForSeconds(time[i] + waitingTime[i]);
        }
        isMoving = false;
    }

    public IEnumerator AttachCamera(Transform cameraAnchor, Transform cameraTarget, Vector3 offset, float timeToAttach)
    {
        isMoving = true;

        Vector3 startPosition = currentPosition;
        Quaternion startRotation = currentRotation;
        currentTarget = cameraTarget;
        currentOffset = offset;
        Quaternion lookRotation = Quaternion.LookRotation(-currentOffset);
        float elapsedTime = 0;

        while (elapsedTime < timeToAttach)
        {
            elapsedTime += Time.deltaTime;
            currentCamera.transform.position = Vector3.Lerp(startPosition, cameraAnchor.position + currentOffset, elapsedTime / timeToAttach);
            currentRotation = Quaternion.Slerp(startRotation, lookRotation, elapsedTime / timeToAttach);
            yield return null;
        }
        isMoving = false;
        currentAnchor = cameraAnchor;
    }
}
