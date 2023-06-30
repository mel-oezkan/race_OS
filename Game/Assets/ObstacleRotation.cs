using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotation : MonoBehaviour
{
   
     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           

            // Assuming you have a reference to the game object's transform
            Transform objectTransform = gameObject.transform;

            // Rotates the gameobject, so that it looks like it falls down
            float newRotationAngle = 90f; 
            Vector3 newRotation = objectTransform.rotation.eulerAngles;
            newRotation.z = newRotationAngle;
            objectTransform.rotation = Quaternion.Euler(newRotation);

         
        }
    }
}
