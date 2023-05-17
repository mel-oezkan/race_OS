using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour

{

    // declaring the offsets for the two camera modes
    // third person where the camera follows the car
    // eye in the sky where the camera is above the car
    private static Vector3 offsetThirdPerson = new Vector3(0f, 2f, -2f);
    private static Vector3 offsetEye = new Vector3(0f, 100f, 0);

    // start with 3d person camera
    private Vector3 offset = offsetThirdPerson;

    private bool isThirdPerson = true;

    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;


    private void FixedUpdate() 
    {
        HandleTranslation();
        HandleRotation();
        HandleCameraMode();
    }

    private void HandleCameraMode () {
        if (Input.GetKeyDown(KeyCode.C)) {
            // changes the camera mode
            offset = isThirdPerson 
                ? offsetThirdPerson
                : offsetEye;

            isThirdPerson = !isThirdPerson;
        }
    }
    private void HandleRotation() 
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(
            transform.rotation, 
            rotation, 
            rotationSpeed * Time.deltaTime
        );
    }

    private void HandleTranslation() 
    {
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(
            transform.position, 
            targetPosition, 
            translateSpeed * Time.deltaTime
        );
    }

}
