using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelUnpixelBehavior : MonoBehaviour
{
    public GameObject pixelOn;
    public GameObject pixelOff;

    public GameObject pixelOnCam;
    public GameObject pixelOffCam;
    bool pixel = true;

    public PostProcessManagerScript postProcessManager;
    private bool edgeEnable;
    private bool outlineEnable;

    // Start is called before the first frame update
    void Start()
    {
        edgeEnable = postProcessManager.enableEdges;
        outlineEnable = postProcessManager.enableOutlines;
        Pixelize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pixel = !pixel;
            Pixelize();
        }
    }

    private void Pixelize()
    {
        if (pixel)
        {
            postProcessManager.enableEdges = edgeEnable;
            postProcessManager.enableOutlines = outlineEnable;
            pixelOn.SetActive(true);
            pixelOff.SetActive(false);

            pixelOnCam.SetActive(true);
            pixelOffCam.SetActive(false);
        }
        else
        {
            postProcessManager.enableEdges = false;
            postProcessManager.enableOutlines = false;
            pixelOn.SetActive(false);
            pixelOff.SetActive(true);

            pixelOnCam.SetActive(false);
            pixelOffCam.SetActive(true);
        }
    }
}
