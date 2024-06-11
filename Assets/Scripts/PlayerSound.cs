using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip footStepSound;

    private AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void PlayFootsepSound()
    {
        source.Stop();
        source.clip = footStepSound;
        source.Play();
    }
}
