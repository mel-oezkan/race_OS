using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControls : MonoBehaviour
{

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

    private void FixedUpdate() 
    {

        currentAcceleration = acceleration * Input.GetAxis("Vertical");
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
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
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
        // handle acceleration
        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        // handle breaking
        if (Input.GetKey(KeyCode.Space)) 
            currentBreakForce = breakingForce;
        else 
            currentBreakForce = 0.0f;

        // handle steering
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
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
