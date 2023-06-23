using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private SoundControls soundControls; 
    
    public event System.Action OnCarPassedFinishLine; // Event to be invoked when the car passes the finish line

    public CarControls carControls; // Reference to the CarControls script
    public Timer timer;
    public ImageGood imageGood;
    public ImageNaja imageNaja;
    public ImageBad imageBad;
    public List<Sprite> images; // List of image sprites



    private float imageDelay = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCarPassedFinishLine?.Invoke();
            // Other logic for finishing the level or triggering events
            carControls.StopMovement();
            timer.StopTimer();
            StartCoroutine(ShowImageCoroutine());
            soundControls.gameMusicStop();
            soundControls.backgroundMotorStop();
            soundControls.clipStop();
        }
    }

    private IEnumerator ShowImageCoroutine()
    {
        yield return new WaitForSeconds(imageDelay);

        float elapsedTime = timer.GetElapsedTime(); // Get the elapsed time from the Timer class

        Debug.Log(elapsedTime);
        if (elapsedTime < 3f)
        {
            imageGood.ShowImage();
            soundControls.playSound("good");
        }
        else if (elapsedTime >= 3f && elapsedTime <= 7f)
        {
            imageNaja.ShowImage();
            soundControls.playSound("naja");
        }
        else
        {
            imageBad.ShowImage();
            soundControls.playSound("bad");
        }
    }
}
