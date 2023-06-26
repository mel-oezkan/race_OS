using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    // References
    private CountdownTimer countdownTimer; // Reference to the CountdownTimer script
    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component for displaying the timer

    // Variables
    private float _startTime; // The time at which the timer started
    private float _elapsedTime; // The elapsed time since the timer started
    private bool _timerStarted; // Flag indicating if the timer has started or not

    private void Start()
    {
        countdownTimer = FindObjectOfType<CountdownTimer>(); // Find the CountdownTimer script in the scene
        countdownTimer.OnCountdownFinished += StartTimer; // Start the timer when the countdown is finished
    }

    private void OnDestroy()
    {
        countdownTimer.OnCountdownFinished -= StartTimer; // Unsubscribe from the countdown finished event
    }

    private void StartTimer()
    {
        _startTime = Time.time; // Set the start time to the current time
        _timerStarted = true; // Set the timer started flag to true
    }

    private void Update()
    {
        if (_timerStarted)
        {
            _elapsedTime = Time.time - _startTime; // Calculate the elapsed time since the timer started
            UpdateTimerText(_elapsedTime); // Update the timer text display
        }
    }

    // Update the timer text using the TextMeshProUGUI component
    private void UpdateTimerText(float time)
    {
        timerText.text = "Time: " + time.ToString("F2") + " sec";
    }

    public void StopTimer()
    {
        _timerStarted = false; // Stop the timer by setting the timer started flag to false
    }

    //Calculates the elapsed time
    public float GetElapsedTime()
    {
        _elapsedTime = Time.time - _startTime - 2f; // Subtracts 2 sec, since the elapsed time
                                                    // should not represent the time starting
                                                    // from game begin but from end of countdown 
        return _elapsedTime;
    }

    public void ClockReducesTime()
    {
        _startTime += 10f; // Increase the start time by 4 seconds when the clock reduces time (used in Clock script)
    }
}
