using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : CameraEffect
{
    Coroutine _shakeCoroutine;

    CameraController cameraController;

    public CameraShakeData cameraShakeData;

    public void SetCameraController(CameraController controller)
    {
        cameraController = controller;
    }

    public override void  SetCameraEffectData(CameraEffectData effectData)
    {
        cameraShakeData = (CameraShakeData)effectData;
    }

    public override void PlayEffect()
    {
        if(_shakeCoroutine!=null)
        {
            StopEffect();
        }
        _shakeCoroutine = StartCoroutine(ShakeCamera());
    }

    public override void StopEffect()
    {
        StopCoroutine(_shakeCoroutine);
        _shakeCoroutine = null;
    }

    IEnumerator ShakeCamera()
    {
        float totalElapsedTime = 0f;
        float iterationElapsedTime = 0f;
        float halfIterationTime = cameraShakeData.effectLength / cameraShakeData.cameraShakeAmount;

        float xdisplacement = Random.Range(-cameraShakeData.cameraShakeIntensity,cameraShakeData.cameraShakeIntensity);
        float ydisplacement = Random.Range(-cameraShakeData.cameraShakeIntensity, cameraShakeData.cameraShakeIntensity);

        Vector3 targetPosition = _camera.transform.position + new Vector3(xdisplacement, ydisplacement, 0f);

        bool going = true;

        while(totalElapsedTime < cameraShakeData.effectLength)
        {
            while(iterationElapsedTime < halfIterationTime && going)
            {
                cameraController.SetDisplacement(targetPosition-Vector3.Lerp(_camera.transform.position, targetPosition, iterationElapsedTime/halfIterationTime));
                iterationElapsedTime += Time.deltaTime;
                totalElapsedTime += Time.deltaTime;
                yield return null;
            }

            going = false;
            iterationElapsedTime = 0f;

            while (iterationElapsedTime < halfIterationTime && !going)
            {
                cameraController.SetDisplacement(targetPosition-Vector3.Lerp(targetPosition, _camera.transform.position, iterationElapsedTime / halfIterationTime));
                iterationElapsedTime += Time.deltaTime;
                totalElapsedTime += Time.deltaTime;
                yield return null;
            }

            going = true;
            iterationElapsedTime = 0f;

            xdisplacement = Random.Range(-cameraShakeData.cameraShakeIntensity, cameraShakeData.cameraShakeIntensity);
            ydisplacement = Random.Range(-cameraShakeData.cameraShakeIntensity, cameraShakeData.cameraShakeIntensity);
            targetPosition = _camera.transform.position + new Vector3(xdisplacement, ydisplacement, 0f);
            yield return null;
        }
    }
}
