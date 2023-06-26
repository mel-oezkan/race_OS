using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControls : MonoBehaviour
{
    //Audio Sources
    [SerializeField] private AudioSource audioclipsSource;
    [SerializeField] private AudioSource audiobackgroundMotorSource;
    [SerializeField] private AudioSource audioFinishSource;
    [SerializeField] private AudioSource countdownSource;
    [SerializeField] private AudioSource gameMusicSource;
    [SerializeField] private AudioSource clockSource;
    [SerializeField] private AudioSource HumanSource;

    //Audio Clips
    [SerializeField] private AudioClip accelerationClip;
    [SerializeField] private AudioClip backgroundMotorClip;
    [SerializeField] private AudioClip goodClip;
    [SerializeField] private AudioClip najaClip;
    [SerializeField] private AudioClip badClip;
    [SerializeField] private AudioClip countdownClip;
    [SerializeField] private AudioClip gameMusicClip;
    [SerializeField] private AudioClip clockClip;
    [SerializeField] private AudioClip jojoClip;
    [SerializeField] private AudioClip melihClip;
    [SerializeField] private AudioClip mohammadClip;

    // Function that plays a specific sound clip based on the provided clipName variable
    public void playSound(string clipName)
    {
        if (clipName == "acceleration")
        {
            audioclipsSource.PlayOneShot(accelerationClip);
        }
        else if (clipName == "backgroundMotor")
        {
            audiobackgroundMotorSource.PlayOneShot(backgroundMotorClip);
        }
        else if (clipName == "good")
        {
            audioFinishSource.PlayOneShot(goodClip);
        }
        else if (clipName == "naja")
        {
            audioFinishSource.PlayOneShot(najaClip);
        }
        else if (clipName == "bad")
        {
            audioFinishSource.PlayOneShot(badClip);
        }
        else if (clipName == "countdown")
        {
            countdownSource.PlayOneShot(countdownClip);
        }
        else if (clipName == "gameMusic")
        {
            gameMusicSource.PlayOneShot(gameMusicClip);
        }
        else if (clipName == "clock")
        {
            clockSource.PlayOneShot(clockClip);
        }
        else if (clipName == "jojo")
        {
            clockSource.PlayOneShot(jojoClip);
        }
        else if (clipName == "mohammad")
        {
            clockSource.PlayOneShot(mohammadClip);
        }
        else if (clipName == "melih")
        {
            clockSource.PlayOneShot(mohammadClip);
        }
    }


    //Provides the information whether the audioclipsSource is playing
    public bool isPlaying()
    {
        return audioclipsSource.isPlaying;
    }

    // Function that stops a specific sound clip 
    public void clipStop()
    {
        audioclipsSource.Stop();
    }

    public void gameMusicStop()
    {
        gameMusicSource.Stop();
    }

    public void backgroundMotorStop()
    {
        audiobackgroundMotorSource.Stop();
    }
}