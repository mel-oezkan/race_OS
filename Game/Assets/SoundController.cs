using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControls : MonoBehaviour
{
    [SerializeField] private AudioSource audioclipsSource;

    [SerializeField] private AudioSource audiobackgroundSource;

    [SerializeField] private AudioClip accelerationClip;

    public void playSound(string clipName)
    {
        if (clipName == "acceleration")
        {
            audioclipsSource.clip = accelerationClip;
            audioclipsSource.Play();
            Debug.Log("playSound");
        }

    }
}