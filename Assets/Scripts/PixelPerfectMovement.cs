using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectMovement : MonoBehaviour
{
    private CharacterController controller;
    private float playerSpeed = 2.0f;
    public PixelCameraBehavior pixelCameraBehavior;
    private Camera renderCamera;
    public float pixelSize;

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

            float snappedX = Mathf.RoundToInt(currentPosition.x / pixelSize) * pixelSize;
            float snappedY = Mathf.RoundToInt(currentPosition.y / pixelSize) * pixelSize;
            float snappedZ = Mathf.RoundToInt(currentPosition.z / pixelSize) * pixelSize;

            Vector3 snappedPosition = new Vector3(snappedX,snappedY,snappedZ);

            transform.position = snappedPosition;

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }
        }
    }
}
