using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControls : MonoBehaviour
{

    public Rigidbody carRB;
    public Transform car;
    public Vector3 offset;
    public float speed;
    public CarControls carController;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        carRB = car.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 carVelocity = (carRB.velocity + car.transform.forward).normalized;
        transform.position = Vector3.Lerp(
            transform.position, 
            car.position + 
                car.transform.TransformVector(offset) + 
                carVelocity * (-5.0f), 
            speed * Time.deltaTime
        );

        transform.LookAt(car);


        float desiredFOV = 60f + (carController.speed * 0.2f); // Adjust the coefficient to control the rate of change
        float currentFOV = cam.fieldOfView;
        float newFOV = Mathf.Lerp(currentFOV, desiredFOV, Time.deltaTime * 5f); // Adjust the interpolation speed as needed

        // Set the new field of view value to the camera
        cam.fieldOfView = newFOV;
    }
}
