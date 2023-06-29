using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JojoObstacle : MonoBehaviour
{
    //References
    [SerializeField] private SoundControls _soundControls;
    [SerializeField] private CarPhysics _carPhysics;

    // Collision Event
    public event System.Action OnJojoCollision; // Event to be invoked when the car collides with a clock


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _soundControls.playSound("jojo");


            // Invoke the OnClockCollision event
            OnJojoCollision?.Invoke();

            // Assuming you have a reference to the game object's transform
            Transform objectTransform = gameObject.transform;
            
            // Set the desired rotation around the Z-axis
            float newRotationAngle = 90f; // Specify the angle in degrees
            Vector3 newRotation = objectTransform.rotation.eulerAngles;
            newRotation.z = newRotationAngle;
            objectTransform.rotation = Quaternion.Euler(newRotation);

            //Reduces the speed 
            _carPhysics.ReduceSpeed(0.25f, 4f);

        }
    }
}