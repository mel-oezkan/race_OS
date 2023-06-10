using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControls : MonoBehaviour
{

    public PauseScript PauseScript;


    [SerializeField] private WheelCollider frontRight;
    [SerializeField] private WheelCollider frontLeft;
    [SerializeField] private WheelCollider backRight;
    [SerializeField] private WheelCollider backLeft;

    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform frontRightTransform;

    [SerializeField] Transform backLeftTransform;
    [SerializeField] Transform backRightTransform;

    public float acceleration = 5000.0f;
    public float breakingForce = 300.0f;
    public float maxTurnAngle = 15.0f;
    private float currentAcceleration = 0.0f;
    private float currentBreakForce = 0.0f;
    private float currentTurnAngle = 0.0f;
    private float verticalInput = 0.0f;
    private float horizontalInput = 0.0f;


    private void FixedUpdate() 
    {
        HandleInputs();

        currentAcceleration = acceleration * verticalInput;
        if (Input.GetKey(KeyCode.Space)) 
            currentBreakForce = breakingForce;
        else 
            currentBreakForce = 0.0f;

        // apply acceleration
        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;

        // apply breaking
        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;

        // handle steering
        currentTurnAngle = maxTurnAngle * horizontalInput;
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;

        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(frontRight, frontRightTransform);
        UpdateWheel(backLeft, backLeftTransform);
        UpdateWheel(backRight, backRightTransform);
        
        // Debug line to show the current speed
        Debug.DrawLine(
            transform.position, 
            transform.forward * currentAcceleration, 
            Color.red
        );
    }

    void HandleInputs() 
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Escape)) 
            PauseScript.Setup();

    }

    void UpdateWheel(WheelCollider col, Transform trans) 
    {
        Vector3 pos;
        Quaternion rot;
        col.GetWorldPose(out pos, out rot);

        // set the pos and rot
        trans.position = pos;
        trans.rotation = rot;
    }
}
