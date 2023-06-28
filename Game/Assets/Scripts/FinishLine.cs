using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    // References
    [SerializeField] private SoundControls soundControls;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CarPhysics carPhysics;
    [SerializeField] private Timer timer;
    [SerializeField] private ImageGood imageGood;
    [SerializeField] private ImageNaja imageNaja;
    [SerializeField] private ImageBad imageBad;

    //public Button restart;

    // Variables
    private float _imageDelay = 1f;

    // Boolean Variable
    public bool _isFinished = false;


    // Specifies what happens when car passes the finish line
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer.StopTimer();
            StartCoroutine(ShowImageCoroutine());
            soundControls.gameMusicStop();
            soundControls.backgroundMotorStop();
            soundControls.clipStop();
            uiManager._isFinished = true;

            // Call StopMovement function with a delay of 1 second
            StartCoroutine(DelayedStopMovement());
        }
    }

    // Assigns images and sounds to certain time intervals, showing the images for 3 sec
    private IEnumerator ShowImageCoroutine()
    {
        yield return new WaitForSeconds(_imageDelay);

        float elapsedTime = timer.GetElapsedTime(); // Get the elapsed time from the timer script

        // Specifies which image and sound is played after how much time
        if (elapsedTime < 45f)
        {
            imageGood.ShowImage();
            soundControls.playSound("good");
        }
        else if (elapsedTime >= 45f && elapsedTime <= 75f)
        {
            imageNaja.ShowImage();
            soundControls.playSound("naja");
        }
        else
        {
            imageBad.ShowImage();
            soundControls.playSound("bad");
        }
        yield return new WaitForSeconds(3f); // Delays the call of the following function
        HideImage(); // Calls a method to hide the images
    }

    // Hides the images
    private void HideImage()
    {
        imageGood.HideImage();
        imageNaja.HideImage();
        imageBad.HideImage();
    }

    // Delays the execution of the StopMovement function to make it look more smooth that the car passes the finish line
    private IEnumerator DelayedStopMovement()
    {
        yield return new WaitForSeconds(_imageDelay);
        carPhysics._isFinished = true;
    }
}
