using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelCameraBehavior : MonoBehaviour
{
    //public GameObject renderPlane;

    public Camera renderCamera;
    public Camera outputCamera;
    public Texture pixelizationTexture;
    [Range(1f, 7f)]
    public int pixelizationFactor = 1;
    public float pixelSize;
    public GameObject follow;
    private Vector3 camDistance;
    private Quaternion camRotation;
    public bool rotating = false;

    public Vector3 currentPos;
    public Vector3 newPos = Vector3.zero;
    public Vector3 movingPos;
    public float camSpeed = 1;
    public float lerpTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        pixelizationTexture.width = 1920 / pixelizationFactor;
        pixelizationTexture.height = 1080 / pixelizationFactor;
        TexelGrid();
        camDistance = transform.position - follow.transform.position;
        camRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotating || Input.GetKeyDown(KeyCode.E))
        {
            RotateRight();
        }
        else if (rotating || Input.GetKeyDown(KeyCode.Q))
        {
            RotateLeft();
        }
        else
         {
             if (transform.rotation != camRotation)
             {
                 camDistance = transform.position - follow.transform.position;
                 camRotation = transform.rotation;
                 TexelGrid();
             }
             outputCamera.transform.position = transform.position;
             // Estas funciones sirven para que la cámara siga al jugador y obtener un delta que le dice a la cámara hacia dónde moverse de su posición inicial
             Vector3 targetPosition = follow.transform.position + camDistance;
             Vector3 worldDelta = targetPosition - transform.position;

             //Esto transforma el delta en las coordenadas del mundo a las coordenadas locales de la cámara (teniendo en cuenta la rotación que tiene)
             //y lo divide por el tamaño del píxel 
             Vector3 localDelta = transform.InverseTransformDirection(worldDelta);

             Vector3 snappedPos;
             //Despues de dividirlo, se aproxima y de esa manera, la cámara se "snapea" a los píxeles 
             snappedPos.x = Mathf.RoundToInt(localDelta.x / pixelSize) * pixelSize;
             snappedPos.y = Mathf.RoundToInt(localDelta.y / pixelSize) * pixelSize;
             snappedPos.z = Mathf.RoundToInt(localDelta.z / pixelSize) * pixelSize;

             //Aqui símplemente se aplica el snapeo a los píxeles en el movimiento en los ejes de la cámara
             transform.position +=
               transform.right * snappedPos.x
             + transform.up * snappedPos.y
             + transform.forward * snappedPos.z;

             Vector3 finalPosition = transform.position;

             Vector3 offset = new Vector3(finalPosition.x - targetPosition.x, finalPosition.y - targetPosition.y, 0);

             outputCamera.transform.position -= offset;
         }
    }
  
    private void RotateRight()
    {
       
        transform.RotateAround(follow.transform.position, follow.transform.up, -45);
    }
    private void RotateLeft()
    {
        transform.RotateAround(follow.transform.position, follow.transform.up, 45);
    }

    private void TexelGrid()
    {
        //Con esto averiguamos los píxeles renderizados en el espacio. 
        //(Se multiplica orthographicSize por 2 porque es la mitad del tamaño de la cámara y se divide entre el numero de pixeles en la altura de esta)
        pixelSize = 2f * renderCamera.orthographicSize / renderCamera.pixelHeight;
    }
}
