using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRotate : MonoBehaviour
{
    public float rotationSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0, rotationSpeed * Time.deltaTime, Space.Self);
    }
}
