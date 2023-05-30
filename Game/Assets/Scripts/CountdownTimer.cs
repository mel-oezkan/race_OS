using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float countdownDuration = 3f; // Duration of the countdown in seconds
    public TextMeshProUGUI countdownText; // Reference to the countdown text component

    public bool canMove = false; // Flag to enable movement when countdown reaches "GO"

    private float currentCountdownValue;

    private void Start()
    {
        // Start the countdown
        StartCountdown();
    }

    private void StartCountdown()
    {
        currentCountdownValue = countdownDuration;

        // Update the countdown text once at the beginning
        UpdateCountdownText();

        // Start the countdown coroutine
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

            // Update the countdown text
            UpdateCountdownText();

            // Wait for one second before updating the countdown
            yield return new WaitForSeconds(1f);
        }

        // Display a message or perform any action when the countdown reaches zero
        countdownText.text = "GO!";
        canMove = true; // Enable movement

        yield return new WaitForSeconds(1f);

        // Hide the countdown text or perform any other desired action
        countdownText.gameObject.SetActive(false);
    }
}