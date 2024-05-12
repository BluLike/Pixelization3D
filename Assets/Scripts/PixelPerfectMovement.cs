using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectMovement : MonoBehaviour
{
    private CharacterController controller;
    public float playerSpeed = 2.0f;
    public PixelCameraBehavior pixelCameraBehavior;
    private Camera renderCamera;
    public float pixelSize;
    private Vector3 preSnapPos;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        renderCamera = pixelCameraBehavior.renderCamera;
        
    }

    void Update()
    {
        if(pixelSize == 0)
        {
            pixelSize = pixelCameraBehavior.pixelSize;
        }
        else
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * Time.deltaTime * playerSpeed);

            Vector3 currentPosition = transform.position;
            preSnapPos = currentPosition;
            Vector3 camLocalcurrentPos = renderCamera.transform.InverseTransformPoint(currentPosition);
            float snappedX = Mathf.RoundToInt(camLocalcurrentPos.x / pixelSize) * pixelSize;
            float snappedY = Mathf.RoundToInt(camLocalcurrentPos.y / pixelSize) * pixelSize;
            float snappedZ = Mathf.RoundToInt(camLocalcurrentPos.z / pixelSize) * pixelSize;

            Vector3 localSnappedPosition = new Vector3(snappedX,snappedY,snappedZ);

            Vector3 snappedPosition = renderCamera.transform.TransformPoint(localSnappedPosition);
            transform.position = snappedPosition;

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }
        }
    }
    private void OnPostRender()
    {
        transform.position = preSnapPos;
    }
}
