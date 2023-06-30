using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private SoundControls _soundControls;
    [SerializeField] private CarPhysics _carPhysics;

    public event System.Action OnMohammadCollision; 
    public event System.Action OnMelihCollision; 
    public event System.Action OnJojoCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Melih"))
        {
            //Plays the respective voice recording of Melih
            _soundControls.playSound("melih");
            _carPhysics.ReduceSpeed(0.25f, 4f);

            // Invoke the OnClockCollision event
            OnMohammadCollision?.Invoke();
        }

        if (other.CompareTag("Jojo"))
        {
            //Plays the respective voice recording of jojo
            _soundControls.playSound("jojo");
            _carPhysics.ReduceSpeed(0.25f, 4f);

            // Invoke the OnClockCollision event
            OnJojoCollision?.Invoke();
        }

        if (other.CompareTag("Mohammad"))
        {
          
          //Plays the respective voice recording of Mohammad
            _soundControls.playSound("mohammad");
            _carPhysics.ReduceSpeed(0.25f, 4f);

            // Invoke the OnClockCollision event
            OnMohammadCollision?.Invoke();
        }
    }

}
