using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDelayed : MonoBehaviour
{
    AudioSource myAudio;
    // Use this for initialization
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        Invoke("playAudio", 0.7f);
    }

    void playAudio()
    {
        myAudio.Play();
    }
}
