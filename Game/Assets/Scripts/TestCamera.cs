using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{

    private Vector3 offset = new Vector3(0f, 5f, -10f);
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
            if (isThirdPerson) {
                offset = new Vector3(0f, 100f, 0);
            } else {
                offset = new Vector3(0f, 5f, -10f);

            }

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
