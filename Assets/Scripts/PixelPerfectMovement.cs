using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectMovement : MonoBehaviour
{
    //Velocidad del jugador
    public float playerSpeed = 1f;

    //Script de la cámara
    public PixelCameraBehavior1 pixelCameraBehavior1;
    private Camera renderCamera;
    public float pixelSize;

    private CharacterController controller;

    //Constant gravitacional y velocidad de caída
    public float gravity = 9.8f;
    private Vector3 velocity;


    Vector3 camLocalcurrentPos;
    Vector3 localSnappedPosition;
    Vector3 snappedPosition;
    private void Start()
    {
        renderCamera = pixelCameraBehavior1.renderCamera;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Se asegura de que el PixelSize se ha asignado
        if(pixelSize == 0)
        {
            pixelSize = pixelCameraBehavior1.pixelSize;
        }

        //Se halla la dirección usando los ejes internos de unity
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            //Se rota al jugador a la dirección que desea
            transform.forward = direction;

            // Mueve al jugador en la dirección transformada. 
            //Se multiplica la velocidad por el pixelSize y por 100 para evitar que el jugador se mueva menos de un píxel y se quede atrapado por el snapping
            controller.Move(direction * (playerSpeed*pixelSize*100) * Time.deltaTime);

            //Se le da gravedad al jugador
            velocity.y += -gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }


        Vector3 currentPosition = transform.position;

        //Se convierte la posición del jugador al eje local de la cámara
         camLocalcurrentPos = renderCamera.transform.InverseTransformPoint(currentPosition);

            //Se calcula el snapeo
            float snappedX = Mathf.RoundToInt(camLocalcurrentPos.x / pixelSize) * pixelSize;
            float snappedY = Mathf.RoundToInt(camLocalcurrentPos.y / pixelSize) * pixelSize;
            float snappedZ = Mathf.RoundToInt(camLocalcurrentPos.z / pixelSize) * pixelSize;

         localSnappedPosition = new Vector3(snappedX,snappedY,snappedZ);

        //Se reconvierte a global la posición de snapeo y se aplica
         snappedPosition = renderCamera.transform.TransformPoint(localSnappedPosition);
            transform.position = snappedPosition;
    }
}
