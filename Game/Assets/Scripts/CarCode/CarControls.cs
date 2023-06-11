using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControls : MonoBehaviour
{

    public Rigidbody carRB;
    public PauseScript PauseScript;


    public WheelColliders colliders;
    public WheelTransforms wheelTransforms;

    // user Inputs
    public float steeringInput = 0.0f;
    public float forwardInput = 0.0f;
    public float brakeInput;

    // car physics
    public float slipAngle;
    public float motorPower;
    public float brakePower;
    public float speed;
    public AnimationCurve steeringCurve;


    // debug values


    void Start() {
        carRB = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {   
        speed = carRB.velocity.magnitude;
        HandleInputs();
        ApplyMotorForce();
        ApplyWheelPositions();
        ApplySteering();
        ApplyBreak();
    }


    void ApplySteering() {
        // apply steering
        float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
        colliders.fRWheel.steerAngle = steeringAngle;
        colliders.fLWheel.steerAngle = steeringAngle;
    }

    void ApplyMotorForce() {
        // apply acceleration
        colliders.bRWheel.motorTorque = motorPower * forwardInput;
        colliders.bLWheel.motorTorque = motorPower * forwardInput;
    }        

    void HandleInputs() 
    {
        steeringInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        slipAngle = Vector3.Angle(
            transform.forward, 
            carRB.velocity - transform.forward);

        if (slipAngle > 120f){ 
            if (forwardInput < 0) {
                brakeInput = forwardInput;
                forwardInput = 0f;  
            } 
        } else brakeInput = 0f;

        if (Input.GetKeyDown(KeyCode.Escape)) 
            PauseScript.Setup();

    }


    void ApplyBreak() {
        colliders.bLWheel.brakeTorque = brakePower * brakeInput * 0.3f;
        colliders.bRWheel.brakeTorque = brakePower * brakeInput * 0.3f;
        colliders.fLWheel.brakeTorque = brakePower * brakeInput * 0.7f;
        colliders.fRWheel.brakeTorque = brakePower * brakeInput * 0.7f;
    }

    void ApplyWheelPositions() {
        UpdateWheel(colliders.fRWheel, wheelTransforms.fRWheel);
        UpdateWheel(colliders.fLWheel, wheelTransforms.fLWheel);
        UpdateWheel(colliders.bRWheel, wheelTransforms.bRWheel);
        UpdateWheel(colliders.bLWheel, wheelTransforms.bLWheel);
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


[System.Serializable]
public class WheelColliders {
    public WheelCollider fRWheel;
    public WheelCollider fLWheel;
    public WheelCollider bRWheel;
    public WheelCollider bLWheel;
}

[System.Serializable]
public class WheelTransforms {
    public Transform fRWheel;
    public Transform fLWheel;
    public Transform bRWheel;
    public Transform bLWheel;
}