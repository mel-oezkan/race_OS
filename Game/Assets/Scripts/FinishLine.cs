using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class FinishLine : MonoBehaviour
{
    public event Action OnCarPassedFinishLine; // Event to be invoked when the car passes the finish line

    public CarControls carControls; // Reference to the CarControls script
    public Timer timer;
    public ImageGood imageGood;
    public ImageNaja imageNaja;
    public ImageBad imageBad;

    public List<Sprite> images;

    private float imageDelay = 2f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCarPassedFinishLine?.Invoke();
            // Other logic for finishing the level or triggering events
            carControls.StopMovement();
            timer.StopTimer();
            StartCoroutine(ShowImageCoroutine()); // Start the coroutine to show the image
        }
    }

    private IEnumerator ShowImageCoroutine()
    {
        yield return new WaitForSeconds(imageDelay);

        Sprite imageToShow;

        float elapsedTime = timer.GetElapsedTime(); // Get the elapsed time from the Timer class
        if (elapsedTime < 3f)
        {
            imageGood.ShowImage();
        }
        else if (elapsedTime >= 3f && elapsedTime <= 7f)
        {
            imageNaja.ShowImageNaja();
        }
        else
        {
            imageBad.ShowImage();
        }
    }
}
