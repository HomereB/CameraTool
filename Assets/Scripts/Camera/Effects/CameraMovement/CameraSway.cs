using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSway : CameraEffect
{
    Coroutine _swayCoroutine;

    CameraController cameraController;

    public CameraSwayData cameraSwayData;

    public void SetCameraController(CameraController controller)
    {
        cameraController = controller;
    }

    public override void SetCameraEffectData(CameraEffectData effectData)
    {
        cameraSwayData = (CameraSwayData)effectData;
    }

    public override void PlayEffect()
    {
        if (_swayCoroutine != null)
        {
            StopEffect();
        }
        _swayCoroutine = StartCoroutine(SwayCamera());
    }

    public override void StopEffect()
    {
        StopCoroutine(_swayCoroutine);
        _swayCoroutine = null;
    }

    IEnumerator SwayCamera()
    {
        float totalElapsedTime = 0f;
        float iterationElapsedTime = 0f;
        float halfIterationTime = cameraSwayData.effectLength / cameraSwayData.cameraSwayAmount;

        //use Euler
        float rightwardDisplacement = Random.Range(-cameraSwayData.cameraRightwardSwayIntensity, cameraSwayData.cameraRightwardSwayIntensity);
        float upwardDisplacement = Random.Range(-cameraSwayData.cameraUpwardSwayIntensity, cameraSwayData.cameraUpwardSwayIntensity);

        Quaternion targetRotation = Quaternion.Euler(0f,0f,rightwardDisplacement) ;
        bool going = true;

        while (totalElapsedTime < cameraSwayData.effectLength)
        {            Debug.Log("aller");

            while (iterationElapsedTime < halfIterationTime && going)
            {
                Quaternion angle = Quaternion.Slerp(Quaternion.identity, targetRotation, iterationElapsedTime / halfIterationTime);
                cameraController.SetSway(angle);
                iterationElapsedTime += Time.deltaTime;
                totalElapsedTime += Time.deltaTime;
                yield return null;
            }
            going = false;
            iterationElapsedTime = 0f;
            Debug.Log("retour");

            while (iterationElapsedTime < halfIterationTime && !going)
            {
                Quaternion angle = Quaternion.Slerp(targetRotation, Quaternion.identity, iterationElapsedTime / halfIterationTime);
                cameraController.SetSway(angle);
                iterationElapsedTime += Time.deltaTime;
                totalElapsedTime += Time.deltaTime;
                yield return null;
            }

            going = true;
            iterationElapsedTime = 0f;

            rightwardDisplacement = Random.Range(-cameraSwayData.cameraRightwardSwayIntensity, cameraSwayData.cameraRightwardSwayIntensity);
            upwardDisplacement = Random.Range(-cameraSwayData.cameraUpwardSwayIntensity, cameraSwayData.cameraUpwardSwayIntensity);

            targetRotation = Quaternion.Euler(0f, 0f, rightwardDisplacement);
            yield return null;
        }
    }
}
