using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelCameraBehavior : MonoBehaviour
{
    //public GameObject renderPlane;

    public Camera renderCamera;
    public Texture pixelizationTexture;
    public float pixelSize;
    public GameObject player;
    private Vector3 targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        TexelGrid();
    }

    // Update is called once per frame
    void Update()
    {
            // Estas funciones sirven para que la cámara siga al jugador y obtener un delta que le dice a la cámara hacia dónde moverse de su posición inicial
             Vector3 point = renderCamera.WorldToViewportPoint(player.transform.position);
             Vector3 worldDelta = player.transform.position - renderCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
             
        //Esto transforma el delta en las coordenadas del mundo a las coordenadas locales de la cámara (teniendo en cuenta la rotación que tiene)
        //y lo divide por el tamaño del píxel 
        Vector3 localDelta = renderCamera.transform.InverseTransformDirection(worldDelta) / pixelSize;

        //Despues de dividirlo, se aproxima y de esa manera, la cámara se "snapea" a los píxeles 
        localDelta.x = Mathf.RoundToInt(localDelta.x);
        localDelta.y = Mathf.RoundToInt(localDelta.y);
        localDelta.z = Mathf.RoundToInt(localDelta.z);

        //Aqui símplemente se aplica el snapeo a los píxeles en el movimiento en los ejes de la cámara
        transform.position +=
        transform.right * localDelta.x * pixelSize
        + transform.up * localDelta.y * pixelSize
        + transform.forward * localDelta.z * pixelSize;

    }

    void TexelGrid()
    {
        //Con esto averiguamos los píxeles renderizados en el espacio. 
        //(Se multiplica orthographicSize por 2 porque es la mitad del tamaño de la cámara y se divide entre el numero de pixeles en la altura de esta)
        pixelSize = 2f * renderCamera.orthographicSize / renderCamera.pixelHeight;
    }
}
