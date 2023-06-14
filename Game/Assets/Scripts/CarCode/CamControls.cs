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

    public float minFOV = 60f;
    public float maxFOV = 100f;

    // debug values
    public float currentFOV;


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
                car.transform.TransformVector(offset), 
            (carController.speed / 2) * Time.deltaTime
        );

        transform.LookAt(car);

        // Limit the car FOV to a min and max value
        float desiredFOV = Mathf.Max(minFOV, Mathf.Min(maxFOV, 60f + carController.speed * 0.2f)); 
        float tmp = cam.fieldOfView;
        currentFOV = Mathf.Lerp(tmp, desiredFOV, Time.deltaTime * 5f); // Adjust the interpolation speed as needed

        // Set the new field of view value to the camera
        cam.fieldOfView = currentFOV;
    }
}
