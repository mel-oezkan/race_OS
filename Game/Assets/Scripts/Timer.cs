using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    private CountdownTimer countdownTimer; // Reference to the CountdownTimer script
    public TextMeshProUGUI timerText;

    private float startTime;
    private bool timerStarted;
    private float elapsedTime;

    private void Start()
    {
        countdownTimer = FindObjectOfType<CountdownTimer>(); // Find the CountdownTimer script in the scene
        countdownTimer.OnCountdownFinished += StartTimer;
    }

    private void OnDestroy()
    {
        countdownTimer.OnCountdownFinished -= StartTimer;
    }

    private void StartTimer()
    {
        startTime = Time.time;
        timerStarted = true;

    }


    private void Update()
    {
        if (timerStarted)
        {
            elapsedTime = Time.time - startTime;
            UpdateTimerText(elapsedTime);
        }
    }

    private void UpdateTimerText(float time)
    {
        // Update the timer text using the TextMeshProUGUI component
        // or any other desired method
        // For example:
        // Display the elapsed time in seconds
        timerText.text = "Time: " + time.ToString("F2") + " sec";
        Debug.Log("timer");
    }

    public void StopTimer()
    {
        timerStarted = false;
    }
    
    public float GetElapsedTime()
    {
        elapsedTime = Time.time - startTime - 2f;
        return elapsedTime;
    }

    public void ClockReducesTime()
    {
        startTime += 4f;
    }

}
