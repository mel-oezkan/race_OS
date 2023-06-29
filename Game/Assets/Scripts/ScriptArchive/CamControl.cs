using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    // References
    [SerializeField] private Vector3 offset; 
    [SerializeField] private Transform target; 
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed; 

    private void FixedUpdate()
    {
        HandleTranslation(); // Call the translation function
        HandleRotation(); // Call the rotation function
    }

    private void HandleRotation()
    {
        var direction = target.position - transform.position; // Calculate the direction from the camera to the target
        var rotation = Quaternion.LookRotation(direction, Vector3.up); // Calculate the rotation to face the target
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            rotation,
            rotationSpeed * Time.deltaTime
        ); // Interpolate the camera's rotation towards the target's rotation
    }

    private void HandleTranslation()
    {
        var targetPosition = target.TransformPoint(offset); // Calculate the target position based on the offset
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            translateSpeed * Time.deltaTime
        ); // Interpolate the camera's position towards the target's position
    }
}
