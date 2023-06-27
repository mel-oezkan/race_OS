using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
   
    [SerializeField] private Rigidbody carRB;
    [SerializeField] private Transform car;
    [SerializeField] private Vector3 offset;

    private void Start() 
    {
        carRB = car.GetComponent<Rigidbody>();
    }

    // updated after all other updates
    private void LateUpdate() 
    {   
        // calculate the car speed for dynamic camera movement
        float carSpeed = Vector3.Dot(car.forward, carRB.velocity);

        transform.position = Vector3.Lerp(
            transform.position, 
            car.position + car.transform.TransformVector(offset), 
            Time.deltaTime * carSpeed
        );

        transform.LookAt(car);
    }

}
