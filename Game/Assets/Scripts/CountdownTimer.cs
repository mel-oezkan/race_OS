using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    //References
    [SerializeField] private SoundControls soundControls;
    [SerializeField] private CarControls carControls;
    [SerializeField] private TextMeshProUGUI countdownText;

    public delegate void CountdownFinishedDelegate();
    //Collision Event
    public event CountdownFinishedDelegate OnCountdownFinished;

    //Variables
    private float _currentCountdownValue;
    public float _countdownDuration = 3f;
    //Boolean Variables
    public bool _canMove = false; // Flag to enable movement when countdown reaches "GO"

    //Starts countdown with its sound and the game music 
    private void Start()
    {
        StartCountdown();
        soundControls.playSound("gameMusic");
        soundControls.playSound("countdown");
    }

    //Starts the countdown
    private void StartCountdown()
    {
        // Reinitalize the current countdown value as the 
        // predefined countdown duration
        _currentCountdownValue = _countdownDuration;

        // Update the countdown text and start
        // the countdown coroutine
        UpdateCountdownText();
        StartCoroutine(CountdownCoroutine());
    }

    //Updates the text component representing the countdown
    private void UpdateCountdownText()
    {
        countdownText.text = _currentCountdownValue.ToString("F0");
    }

    //Manages the visibility of countdown values that follow each other with a delay of 1 sec
    private System.Collections.IEnumerator CountdownCoroutine()
    {
        // Wait for one second before starting the countdown
        yield return new WaitForSeconds(1f);

        while (_currentCountdownValue > 0)
        {
            // Reduce the countdown value by one
            _currentCountdownValue -= 1f;
            UpdateCountdownText();

            // Wait for one second before updating the countdown
            yield return new WaitForSeconds(1f);
        }

        // Display a message or perform any action when the countdown reaches zero
        countdownText.text = "GO!";
        _canMove = true; // Enable movement

        // Invoke the OnCountdownFinished event
        OnCountdownFinished?.Invoke();

        // Hide the countdown text or perform any other desired action
        countdownText.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
    }
}
