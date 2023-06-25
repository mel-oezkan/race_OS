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
        clockText.gameObject.SetActive(true);
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

        clockText.gameObject.SetActive(true);
        //clockText.text = "Clock Collected!";
        StartCoroutine(HideClockText());

        //Invoke("HideClockText", clockTextDuration);
    }

    private IEnumerator HideClockText()
    {
        Debug.Log("hi");
        //clockText.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        Debug.Log("b");
        clockText.gameObject.SetActive(false);
        Debug.Log("c");
    }
}