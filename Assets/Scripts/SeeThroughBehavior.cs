using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThroughBehavior : MonoBehaviour
{
    public Camera RenderCamera;
    public float lerpTime = 2f; // El tiempo que tardará en hacer Lerp de 0 a 55

    private bool isLerping = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entering");
        isLerping = true;
        StartCoroutine(LerpCameraClipPlane(0,55));
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exiting");
        isLerping = true;
        StartCoroutine(LerpCameraClipPlane(55, 0));
    }

    IEnumerator LerpCameraClipPlane(float start, float end)
    {
        float timeElapsed = 0;

        while (isLerping)
        {
            RenderCamera.nearClipPlane = Mathf.Lerp(start, end, timeElapsed / lerpTime);
            timeElapsed += Time.deltaTime;

            if (timeElapsed > lerpTime)
            {
                RenderCamera.nearClipPlane = end; // Asegúrate de que nearClipPlane llegue a 55
                isLerping = false;

                Debug.Log("Near Clip Plane" + RenderCamera.nearClipPlane);
            }

            yield return null;
        }
    }
}
