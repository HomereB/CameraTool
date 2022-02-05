using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    Camera currentCamera;

    [SerializeField]
    CameraController currentCameraController;

    CameraShake currentCameraShake;
    CameraSway currentCameraSway;


    public CameraSwayData effectData;

    private void Awake()
    {
        //currentCamera = Camera.main;
        currentCameraController = currentCamera.gameObject.GetComponent<CameraController>();
    }

    private void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.A))
        {
            AddCameraShake(effectData);
        }*/
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddCameraSway(effectData);
        }
    }

    public void AddCameraShake(CameraShakeData effectData)
    {
        if (currentCameraShake==null)
        {
            currentCameraShake = currentCamera.gameObject.AddComponent<CameraShake>();
        }
        currentCameraShake.SetCamera(currentCamera);
        currentCameraShake.SetCameraController(currentCameraController);
        currentCameraShake.SetCameraEffectData(effectData);
        currentCameraShake.PlayEffect();
    }

    public void AddCameraSway(CameraSwayData effectData)
    {
        if (currentCameraSway == null)
        {
            currentCameraSway = currentCamera.gameObject.AddComponent<CameraSway>();
        }
        currentCameraSway.SetCamera(currentCamera);
        currentCameraSway.SetCameraController(currentCameraController);
        currentCameraSway.SetCameraEffectData(effectData);
        currentCameraSway.PlayEffect();
    }

    public void AddCameraFilter()
    {
        //TODO
    }

    public void SwitchCamera(Camera camera, bool keepCurrentEffects)
    {
        currentCamera.enabled = false;
        currentCameraController.enabled = false;
        currentCamera = camera;
        currentCameraController = camera.GetComponent<CameraController>();
        currentCamera.enabled = true;
        currentCameraController.enabled = true;
        //TODO : add efefct transfer from one camera to another
    }
}
