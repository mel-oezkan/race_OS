using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Human : MonoBehaviour
{
    [SerializeField] private SoundControls soundControls;
    public event System.Action OnHumanCollision; // Event to be invoked when the car collides with a clock




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            soundControls.playSound("jojo");
            // Logic for when the car collides with a clock
            HandleHumanCollision();

            // Invoke the OnClockCollision event
            OnHumanCollision?.Invoke();

            // Assuming you have a reference to the game object's transform
            Transform objectTransform = gameObject.transform;
            
            // Set the desired rotation around the Z-axis
            float newRotationAngle = 90f; // Specify the angle in degrees
            Vector3 newRotation = objectTransform.rotation.eulerAngles;
            newRotation.z = newRotationAngle;
            objectTransform.rotation = Quaternion.Euler(newRotation);

            

        }
    }



    private void HandleHumanCollision()
    {
        // Add your desired actions to be invoked when the car collides with a clock
        Debug.Log("Car collided with a human!");
        

    }






}