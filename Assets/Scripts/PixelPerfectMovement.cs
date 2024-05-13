using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectMovement : MonoBehaviour
{
    public float playerSpeed = 4.0f;
    public PixelCameraBehavior pixelCameraBehavior;
    private Camera renderCamera;
    public float pixelSize;
    public Vector3 preSnapPos;

    private void Start()
    {
        renderCamera = pixelCameraBehavior.renderCamera;
        preSnapPos = transform.position;
        
    }

    void Update()
    {
        if(pixelSize == 0)
        {
            pixelSize = pixelCameraBehavior.pixelSize;
        }
        else
        {       
             

            Vector3 currentPosition = transform.position;
            preSnapPos = currentPosition;            

            Vector3 camLocalcurrentPos = renderCamera.transform.InverseTransformPoint(currentPosition);

            /*
            float snappedX = Mathf.RoundToInt(camLocalcurrentPos.x / pixelSize) * pixelSize;
            float snappedY = Mathf.RoundToInt(camLocalcurrentPos.y / pixelSize) * pixelSize;
            float snappedZ = Mathf.RoundToInt(camLocalcurrentPos.z / pixelSize) * pixelSize;
            */
            float snappedX = Mathf.RoundToInt(camLocalcurrentPos.x / pixelSize) * pixelSize;
            float snappedY = Mathf.RoundToInt(camLocalcurrentPos.y / pixelSize) * pixelSize;
            float snappedZ = Mathf.RoundToInt(camLocalcurrentPos.z / pixelSize) * pixelSize;



            Vector3 localSnappedPosition = new Vector3(snappedX,snappedY,snappedZ);

            Vector3 snappedPosition = renderCamera.transform.TransformPoint(localSnappedPosition);
            transform.position = snappedPosition;     
        }
    }

    IEnumerator ReturnPosition()
    {
        yield return new WaitForEndOfFrame();
         transform.position = preSnapPos;
    }
}
