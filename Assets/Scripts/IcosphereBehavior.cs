using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcosphereBehavior : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 camLocalcurrentPos;
    private Vector3 localSnappedPosition;
    private Vector3 snappedPosition;
    public PixelCameraBehavior pixelCameraBehavior;
    private Camera renderCamera;
    private float pixelSize;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        renderCamera = pixelCameraBehavior.renderCamera;
    }

    // Update is called once per frame
    void Update()
    {
        if (pixelSize == 0)
        {
            pixelSize = pixelCameraBehavior.pixelSize;
        }

        transform.Rotate(rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime, Space.Self);

        endPos = startPos;
        endPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = endPos;

        Vector3 currentPosition = transform.position;


        camLocalcurrentPos = renderCamera.transform.InverseTransformPoint(currentPosition);


        float snappedX = Mathf.RoundToInt(camLocalcurrentPos.x / pixelSize) * pixelSize;
        float snappedY = Mathf.RoundToInt(camLocalcurrentPos.y / pixelSize) * pixelSize;
        float snappedZ = Mathf.RoundToInt(camLocalcurrentPos.z / pixelSize) * pixelSize;

        localSnappedPosition = new Vector3(snappedX, snappedY, snappedZ);

        snappedPosition = renderCamera.transform.TransformPoint(localSnappedPosition);
        transform.position = snappedPosition;
    }
}
