using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTimer : MonoBehaviour
{

    public float flippedStateDuration = 3f; 
    public Transform carTransform;


    // public for debugging
    public float currentCountdownValue;

    void Start()
    {
        currentCountdownValue = flippedStateDuration;
        StartCoroutine(CountdownCoroutine());
    }


    private IEnumerator CountdownCoroutine() 
    {
        yield return new WaitForSeconds(1f);

        while (currentCountdownValue > 0)
        {
            // Reduce the countdown value by one
            currentCountdownValue -= 1f;

            // car is not flipped, break the timer
            if (carTransform.up.y > 0.5f) {
            }

            // Wait for one second before updating the countdown
            yield return new WaitForSeconds(1f);
        }
    }



}
