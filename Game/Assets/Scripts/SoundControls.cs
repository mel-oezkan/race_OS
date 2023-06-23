using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControls : MonoBehaviour
{
    [SerializeField] private AudioSource audioclipsSource;

    [SerializeField] private AudioSource audiobackgroundSource;

    [SerializeField] private AudioClip accelerationClip;

 





    void Start()
    {

        //backgroundSource.clip = backgroundClip;
        //backgroundSource.loop = true;
       // backgroundSource.Play();

    }


    public void playSound(string clipName)
    {
        if (clipName == "acceleration")
        {
            audioclipsSource.clip = accelerationClip;
            audioclipsSource.Play();
        }

    }
}