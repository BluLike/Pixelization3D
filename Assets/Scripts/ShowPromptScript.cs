using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPromptScript : MonoBehaviour
{
    public GameObject prompt;

    private void OnTriggerEnter(Collider other)
    {
        prompt.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        prompt.SetActive(false);
    }
}
