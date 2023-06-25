using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControls : MonoBehaviour
{
    [SerializeField] private AudioSource audioclipsSource;

    [SerializeField] private AudioSource audiobackgroundMotorSource;

    [SerializeField] private AudioSource audioFinishSource;

    [SerializeField] private AudioSource countdownSource;

    [SerializeField] private AudioSource gameMusicSource;

    [SerializeField] private AudioSource clockSource;

    [SerializeField] private AudioSource HumanSource;


    [SerializeField] private AudioClip accelerationClip;

    [SerializeField] private AudioClip backgroundMotorClip;

    [SerializeField] private AudioClip goodClip;

    [SerializeField] private AudioClip najaClip;

    [SerializeField] private AudioClip badClip;

    [SerializeField] private AudioClip countdownClip;

    [SerializeField] private AudioClip gameMusicClip;

    [SerializeField] private AudioClip clockClip;

    [SerializeField] private AudioClip JojoClip;

    //[SerializeField] private AudioClip MelihClip;

    //[SerializeField] private AudioClip MohammadClip;






    void Start()
    {


    }


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
            clockSource.PlayOneShot(JojoClip);
        }
    }



    public bool isPlaying()
    {
        return audioclipsSource.isPlaying;
    }

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