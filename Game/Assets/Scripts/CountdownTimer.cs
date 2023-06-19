using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float countdownDuration = 3f; // Duration of the countdown in seconds
    public TextMeshProUGUI countdownText; // Reference to the countdown text component

    public bool canMove = false; // Flag to enable movement when countdown reaches "GO"

    private float currentCountdownValue;

    public CarControls carControls;



    private void Start()
    {
        StartCountdown();
    }

    private void StartCountdown()
    {
        // reinitalize the current countdown value as the 
        // predefined countdown duration
        currentCountdownValue = countdownDuration;

        // Update the countdown text and start
        // the countdown coroutine
        UpdateCountdownText();
        StartCoroutine(CountdownCoroutine());
    }

    private void UpdateCountdownText()
    {
        countdownText.text = currentCountdownValue.ToString("F0");
    }

    private System.Collections.IEnumerator CountdownCoroutine()
    {   
        
        // Wait for one second before starting the countdown
        yield return new WaitForSeconds(1f);

        while (currentCountdownValue > 0)
        {
            // Reduce the countdown value by one
            currentCountdownValue -= 1f;
            UpdateCountdownText();

            // Wait for one second before updating the countdown
            yield return new WaitForSeconds(1f);
        }

        // Display a message or perform any action when the countdown reaches zero
        countdownText.text = "GO!";
        carControls.isPaused = false;

        canMove = true; // Enable movement
        // Hide the countdown text or perform any other desired action
        countdownText.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

    }
}