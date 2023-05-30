using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Timer : MonoBehaviour
{
    private float elapsedTime = 0f;
    private bool isTimerRunning = false;
    private TextMeshProUGUI timeText;
    

    private void Start()
    {
        // Start the timer when the game begins
        StartTimer();

        // Get the reference to the UI Text component
        timeText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Display the elapsed time in seconds
            timeText.text = "Time: " + elapsedTime.ToString("F2") + " sec";
        }
    }

    private void StartTimer()
    {
        // Start the timer
        isTimerRunning = true;
    }

    private void StopTimer()
    {
        // Stop the timer
        isTimerRunning = false;
    }
}