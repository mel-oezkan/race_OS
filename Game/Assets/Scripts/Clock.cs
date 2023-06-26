using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clock : MonoBehaviour
{
    //References
    [SerializeField] private SoundControls soundControls;
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI clockText;

    //Collision event
    public event System.Action OnClockCollision; // Event to be invoked when the car collides with a clock




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Logic for when the car collides with a clock
            HandleClockCollision();

            // Invoke the OnClockCollision event
            OnClockCollision?.Invoke();

            // Move the location of the clock object so that it is not visible anymore
            gameObject.transform.position = new Vector3(0, -100, 0);
        }
    }



    private void HandleClockCollision()
    {
        soundControls.playSound("clock");
        timer.ClockReducesTime();
        clockText.text = "-10 sec";
        StartCoroutine(HideClockText());
    }

    private IEnumerator HideClockText()
    {
        yield return new WaitForSeconds(2f);
        clockText.text = "";
    }
}