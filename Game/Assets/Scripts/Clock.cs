using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clock : MonoBehaviour
{
    //References
    [SerializeField] private SoundControls _soundControls;
    [SerializeField] private Timer _timer;
    [SerializeField] private TextMeshProUGUI _clockText;

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


    //Invokes certain events
    private void HandleClockCollision()
    {
        _soundControls.playSound("clock"); //Plays bing sound
        _timer.ClockReducesTime(); //Reduces the elapsed time by 5 sec
        _clockText.text = "-5 sec"; //Displays the time reduction
        StartCoroutine(HideClockText()); //Calls 'HideClockText'
    }

    //Specifies that the '-5 sec' information is displayed for 2 seconds and then disappears
    private IEnumerator HideClockText()
    {
        yield return new WaitForSeconds(2f);
        _clockText.text = "";
    }
}