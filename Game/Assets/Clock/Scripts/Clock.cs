using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] private SoundControls soundControls;
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI clockText;
    [SerializeField] private float clockTextDuration = 2f;
    public event System.Action OnClockCollision; // Event to be invoked when the car collides with a clock

    private void Start()
    {
        // Hide the clock text initially
        //.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Logic for when the car collides with a clock
            HandleClockCollision();

            // Invoke the OnClockCollision event
            OnClockCollision?.Invoke();

            // Disable the clock object
            gameObject.SetActive(false);
        }
    }

    private void HandleClockCollision()
    {
        // Add your desired actions to be invoked when the car collides with a clock
        Debug.Log("Car collided with a clock!");
        soundControls.playSound("clock");
        timer.ClockReducesTime();

        ShowClockText();
        clockText.text = "Clock Collected!";
        Invoke("HideClockText", clockTextDuration);

        // Example actions:
        // - Increase the player's time
        // - Play a sound effect
        // - Show a visual effect
    }

    private void ShowClockText()
    {
        clockText.gameObject.SetActive(true);
    }

    private void HideClockText()
    {
        clockText.gameObject.SetActive(false);
    }
}
