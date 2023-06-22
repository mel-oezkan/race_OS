using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public event Action OnCarPassedFinishLine; // Event to be invoked when the car passes the finish line

    public CarControls carControls; // Reference to the CarControls script
    public Timer timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCarPassedFinishLine?.Invoke();
            // Other logic for finishing the level or triggering events
            carControls.StopMovement();
            timer.StopTimer();
        }
    }
}
