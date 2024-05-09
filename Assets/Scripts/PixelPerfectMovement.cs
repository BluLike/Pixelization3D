using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectMovement : MonoBehaviour
{
    private CharacterController controller;
    private float playerSpeed = 2.0f;
    public PixelCameraBehavior pixelCameraBehavior;
    private Camera renderCamera;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        renderCamera = pixelCameraBehavior.renderCamera;
    }

    void Update()
    {
        if (pixelCameraBehavior.pixelSize != 0)
        {
            float pixelSize = pixelCameraBehavior.pixelSize;
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 worldDelta = move * Time.deltaTime * playerSpeed;
            Vector3 localDelta = renderCamera.transform.InverseTransformDirection(worldDelta) / pixelSize;

            localDelta.x = Mathf.RoundToInt(localDelta.x);
            localDelta.y = Mathf.RoundToInt(localDelta.y);
            localDelta.z = Mathf.RoundToInt(localDelta.z);

            controller.Move(localDelta*pixelSize);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }
        }
    }
}
