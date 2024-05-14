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
    private CharacterController controller;
    public Vector3 camLocalcurrentPos;
    public Vector3 localSnappedPosition;
    public Vector3 snappedPosition;

    private void Start()
    {
        renderCamera = pixelCameraBehavior.renderCamera;
        preSnapPos = transform.position;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        transform.position = preSnapPos;

        if(pixelSize == 0)
        {
            pixelSize = pixelCameraBehavior.pixelSize;
        }
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        float velocity = playerSpeed * (pixelSize*100);
        controller.Move(direction * Time.deltaTime * velocity);

       

        Vector3 currentPosition = transform.position;

            preSnapPos = currentPosition;            

            camLocalcurrentPos = renderCamera.transform.InverseTransformPoint(currentPosition);

            
            float snappedX = Mathf.RoundToInt(camLocalcurrentPos.x / pixelSize) * pixelSize;
            float snappedY = Mathf.RoundToInt(camLocalcurrentPos.y / pixelSize) * pixelSize;
            float snappedZ = Mathf.RoundToInt(camLocalcurrentPos.z / pixelSize) * pixelSize;

            localSnappedPosition = new Vector3(snappedX,snappedY,snappedZ);

            snappedPosition = renderCamera.transform.TransformPoint(localSnappedPosition);
            transform.position = snappedPosition;
    }
}
