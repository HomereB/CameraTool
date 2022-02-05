using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraEffect : MonoBehaviour
{
    [SerializeField]
    protected Camera _camera;
    protected CameraEffectData _cameraEffectData;
    public abstract void PlayEffect();
    public abstract void StopEffect();

    public void SetCamera(Camera camera)
    {
        _camera = camera;
    }

    public virtual void SetCameraEffectData(CameraEffectData effectData)
    {
        _cameraEffectData = effectData;
    }
}
