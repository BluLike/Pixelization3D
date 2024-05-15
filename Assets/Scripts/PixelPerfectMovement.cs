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
    private bool rotating;
    private Animator animator;

    private void Start()
    {
        rotating = pixelCameraBehavior.rotating;
        renderCamera = pixelCameraBehavior.renderCamera;
        preSnapPos = transform.position;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //transform.position = preSnapPos;

        if(pixelSize == 0)
        {
            pixelSize = pixelCameraBehavior.pixelSize;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("running", true);
            // Obtiene la direcci�n de la c�mara
            Vector3 camForward = renderCamera.transform.forward;
            Vector3 camRight = renderCamera.transform.right;

            // Ignora la rotaci�n en el eje Y de la c�mara
            camForward.y = 0;
            camRight.y = 0;
            camForward = camForward.normalized;
            camRight = camRight.normalized;

            // Transforma la direcci�n del movimiento a la direcci�n de la c�mara
            direction = camForward * direction.z + camRight * direction.x;

            transform.forward = direction;
            // Mueve al jugador en la direcci�n transformada
            controller.Move(direction * (playerSpeed*pixelSize*100) * Time.deltaTime);
        }
        else
        {
            animator.SetBool("running", false);
        }



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
