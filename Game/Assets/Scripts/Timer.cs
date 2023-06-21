using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    private CountdownTimer countdownTimer; // Reference to the CountdownTimer script
    public TextMeshProUGUI timerText;

    private float startTime;
    private bool timerStarted;

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

        Debug.Log("Timer started!");
    }


    private void Update()
    {
        if (timerStarted)
        {
            float elapsedTime = Time.time - startTime;
            UpdateTimerText(elapsedTime);
        }
    }

    private void UpdateTimerText(float time)
    {
        // Update the timer text using the TextMeshProUGUI component
        // or any other desired method
        // For example:
        timerText.text = time.ToString("F2");
    }

}
