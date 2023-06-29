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
    [SerializeField] private AudioSource humanSource;

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
        // play the respective sound clip based on the clipName variable
        switch(clipName) {
            // car sounds
            case "acceleration":
                audioclipsSource.PlayOneShot(accelerationClip);
                break;
            case "backgroundMotor":
                audiobackgroundMotorSource.PlayOneShot(backgroundMotorClip);
                break;

            // finish line sounds
            case "good":
                audioFinishSource.PlayOneShot(goodClip);
                break;
            case "naja":
                audioFinishSource.PlayOneShot(najaClip);
                break;
            case "bad":
                audioFinishSource.PlayOneShot(badClip);
                break;
            
            // gameplay sounds
            case "countdown":
                countdownSource.PlayOneShot(countdownClip);
                break;
            case "gameMusic":
                gameMusicSource.PlayOneShot(gameMusicClip);
                break;
            case "clock":
                clockSource.PlayOneShot(clockClip);
                break;

            // human obsticel sounds
            case "jojo":
                humanSource.PlayOneShot(jojoClip);
                break;
            case "mohammad":
                humanSource.PlayOneShot(mohammadClip);
                break;
            case "melih":
                humanSource.PlayOneShot(melihClip);
                break;
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