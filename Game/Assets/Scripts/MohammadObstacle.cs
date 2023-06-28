using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MohammadObstacle : MonoBehaviour
{
    //References
    [SerializeField] private SoundControls soundControls;
    [SerializeField] private CarPhysics carPhysics;

    // Collision Event
    public event System.Action OnMohammadCollision; // Event to be invoked when the car collides with a clock

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Plays the respective voice recording of Mohammad
            soundControls.playSound("mohammad");

            // Invoke the OnClockCollision event
            OnMohammadCollision?.Invoke();

            // Assuming you have a reference to the game object's transform
            Transform objectTransform = gameObject.transform;

            // Rotates the gameobject, so that it looks like it falls down
            float newRotationAngle = 90f; // Specify the angle in degrees
            Vector3 newRotation = objectTransform.rotation.eulerAngles;
            newRotation.z = newRotationAngle;
            objectTransform.rotation = Quaternion.Euler(newRotation);

            //Reduces the speed 
            carPhysics.ReduceSpeed(0.25f, 4f);

        }
    }
}