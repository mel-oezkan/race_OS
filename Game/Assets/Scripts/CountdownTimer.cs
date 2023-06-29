// CountdownTimer.cs

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    // References
    [SerializeField] private SoundControls _soundControls;
    [SerializeField] private CarPhysics _carPhysics;
    [SerializeField] private TextMeshProUGUI _countdownText;

    public delegate void CountdownFinishedDelegate();
    // Collision Event
    public event CountdownFinishedDelegate OnCountdownFinished;

    // Variables
    private float _currentCountdownValue;
    [SerializeField] private float _countdownDuration = 3f;

    // Boolean Variables
    public bool _canMove = false; // Flag to enable movement when countdown reaches "GO"

    // Starts countdown with its sound and the game music
    private void Start()
    {
        StartCountdown();
        _soundControls.playSound("gameMusic");
        _soundControls.playSound("countdown");
    }

    // Starts the countdown
    private void StartCountdown()
    {
        // Reinitalize the current countdown value as the 
        // predefined countdown duration
        _currentCountdownValue = _countdownDuration;

        // Update the countdown text and start
        // the countdown coroutine
        UpdateCountdownText();
        StartCoroutine(CountdownCoroutine());
        StartCoroutine(StartCanMove());
    }

    // Updates the text component representing the countdown
    private void UpdateCountdownText()
    {
        _countdownText.text = _currentCountdownValue.ToString("F0");
    }

    // Manages the visibility of countdown values that follow each other with a delay of 1 sec
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

        // Invoke the OnCountdownFinished event
        OnCountdownFinished?.Invoke();

        // Hide the countdown text or perform any other desired action
        _countdownText.gameObject.SetActive(false);
    }

    //This function enables movement in line with the end of the countdown
    private System.Collections.IEnumerator StartCanMove()
    {
        yield return new WaitForSeconds(2.5f);
        _canMove = true;
    }
}