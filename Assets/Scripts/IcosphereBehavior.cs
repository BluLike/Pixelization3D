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

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;     
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime, Space.Self);

        endPos = startPos;
        endPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = endPos;
    }
}
