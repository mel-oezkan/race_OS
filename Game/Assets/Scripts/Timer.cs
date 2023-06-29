using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    // References
    private CountdownTimer countdownTimer; // Reference to the CountdownTimer script
    [SerializeField] private TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component for displaying the timer

    // Variables
    private float _startTime; // The time at which the timer started
    private float _elapsedTime; // The elapsed time since the timer started
    private bool _timerStarted; // Flag indicating if the timer has started or not

    private void Start()
    {
        // initalize countdownTimer and start timer when countdown is finished
        countdownTimer = FindObjectOfType<CountdownTimer>(); 
        countdownTimer.OnCountdownFinished += StartTimer; 
    }

    private void OnDestroy()
    {
        // Unsubscribe from the countdown finished event
        countdownTimer.OnCountdownFinished -= StartTimer; 
    }

    private void StartTimer()
    {
        // set timer variables
        _startTime = Time.time; 
        _timerStarted = true; 
    }

    private void Update()
    {
        if (_timerStarted)
        {
            // calc elapsed time and update timer text
            _elapsedTime = Time.time - _startTime; 
            UpdateTimerText(_elapsedTime); 
        }
    }

    // Update the timer text using the TextMeshProUGUI component
    private void UpdateTimerText(float time)
    {
        timerText.text = "Time: " + time.ToString("F2") + " sec";
    }

    public void StopTimer()
    {
        // Stop the timer by setting the timer started flag to false
        _timerStarted = false; 
    }

    //Calculates the elapsed time
    public float GetElapsedTime()
    {
        // Subtracts 2 sec, since the elapsed time
        // should not represent the time starting
        // from game begin but from end of countdown 
        _elapsedTime = Time.time - _startTime - 2f; 
        return _elapsedTime;
    }

    public void ClockReducesTime()
    {
        // Increase the start time by 4 seconds when the clock reduces time (used in Clock script)
        _startTime += 5f; 
    }
}
