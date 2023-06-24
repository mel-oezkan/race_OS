using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private SoundControls soundControls;

    public delegate void CountdownFinishedDelegate();
    public event CountdownFinishedDelegate OnCountdownFinished; // Event declaration

    public float countdownDuration = 3f; // Duration of the countdown in seconds
    public TextMeshProUGUI countdownText; // Reference to the countdown text component

    public bool canMove = false; // Flag to enable movement when countdown reaches "GO"

    private float currentCountdownValue;

    public CarControls carControls; // Reference to the CarController script

    private void Start()
    {
        StartCountdown();
        //soundControls.playSound("gameMusic");
        
        //soundControls.playSound("countdown");
        

    }

    private void StartCountdown()
    {
        // Reinitalize the current countdown value as the 
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
        canMove = true; // Enable movement

        // Invoke the OnCountdownFinished event
        OnCountdownFinished?.Invoke();

        // Hide the countdown text or perform any other desired action
        countdownText.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
    }
}
