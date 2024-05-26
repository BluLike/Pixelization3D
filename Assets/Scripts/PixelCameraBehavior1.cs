using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelCameraBehavior1 : MonoBehaviour
{

    public Camera renderCamera;
    public Texture pixelizationTexture;
    public Camera outputCamera;

    //La proporción pixel/unidad
    public float pixelSize;

    //Para seguir al jugador
    public GameObject follow;

    //Para mantener fija la distancia de la cámara al jugador
    private Vector3 camDistance;

    private void Start()
    {
        TexelGrid();

        camDistance = transform.position - follow.transform.position;
    }

    private void Update()
    {
        //Outputcamera se reestablece a su posición
        outputCamera.transform.position = transform.position;

        // Estas funciones sirven para que la cámara siga al jugador y obtener un delta que le dice a la cámara hacia dónde moverse de su posición inicial
        Vector3 targetPosition = follow.transform.position + camDistance;
        Vector3 worldDelta = targetPosition - transform.position;

        //Esto transforma el delta en las coordenadas del mundo a las coordenadas locales de la cámara (teniendo en cuenta la rotación que tiene)
        //y lo divide por el tamaño del píxel 
        Vector3 localDelta = transform.InverseTransformDirection(worldDelta);


        //Despues de dividirlo, se aproxima y de esa manera, la cámara se "snapea" a los píxeles 
        Vector3 snappedPos;
        snappedPos.x = Mathf.RoundToInt(localDelta.x / pixelSize) * pixelSize;
        snappedPos.y = Mathf.RoundToInt(localDelta.y / pixelSize) * pixelSize;
        snappedPos.z = Mathf.RoundToInt(localDelta.z / pixelSize) * pixelSize;

        //Aqui símplemente se aplica el snapeo a los píxeles en el movimiento en los ejes de la cámara
        transform.position +=
          transform.right * snappedPos.x
        + transform.up * snappedPos.y
        + transform.forward * snappedPos.z;

        //Se registra la posición final de la cámara
        Vector3 finalPosition = transform.position;

        //Se resta a la output camera el opuesto del movimiento que hace la rendercamera
        Vector3 offset = new Vector3(finalPosition.x - targetPosition.x, finalPosition.y - targetPosition.y, finalPosition.z - targetPosition.z);
        outputCamera.transform.position -= offset;
    }
    private void TexelGrid()
    {
        //Con esto averiguamos los píxeles renderizados en el espacio. 
        //(Se multiplica orthographicSize por 2 porque orthographicSize la mitad del tamaño de la cámara y se divide entre el numero de pixeles en la altura de esta)
        pixelSize = 2f * renderCamera.orthographicSize / renderCamera.pixelHeight;
    }
}
