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
    public float brakePower;
    public float speed;
    public AnimationCurve motorPower;
    public AnimationCurve steeringCurve;


    // environment 
    public bool isPaused = false;
    public GameObject roadPath;


    // debug values


    void Start() {
        carRB = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {   
        if (isPaused) return;

        speed = carRB.velocity.magnitude;
        HandleInputs();
        ApplyMotorForce();
        ApplyWheelPositions();
        ApplySteering();
        ApplyBreak();
        // CheckFlip();

        Debug.DrawLine(
            transform.position, 
            transform.position + (carRB.velocity * 100f), 
            Color.red);
    }


    void ApplySteering() {
        // apply steering
        float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
        colliders.fRWheel.steerAngle = steeringAngle;
        colliders.fLWheel.steerAngle = steeringAngle;
    }

    void ApplyMotorForce() {
        // apply acceleration
        float motorForce = motorPower.Evaluate(speed);
        colliders.fRWheel.motorTorque = motorForce * forwardInput;
        colliders.fLWheel.motorTorque = motorForce * forwardInput;
        colliders.bRWheel.motorTorque = motorForce * forwardInput;
        colliders.bLWheel.motorTorque = motorForce * forwardInput;
    }        

    void HandleInputs() 
    {
        steeringInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        Debug.DrawLine(
            transform.position, 
            transform.forward + (carRB.velocity * 100f), 
            Color.blue);
        
        slipAngle = Vector3.Angle(
            transform.forward, 
            carRB.velocity - transform.forward);

        // difference of the two vectors is greater than 120 degrees
        if (slipAngle > 120f){ 

            // makes the car drive backwards
            if (forwardInput < 0) {
                brakeInput = forwardInput;
                forwardInput = 0f;  
            } else brakeInput = 0f;
        } else brakeInput = 0f;

        if (Input.GetKeyDown(KeyCode.Escape)){ 
            Debug.Log("Escape pressed");
            Debug.Log("Closest Node: " + GetClosesNode().ToString());
            // PauseScript.Setup();
        }
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


    void CheckFlip() {
        // if the car is upside down, flip it
        if (transform.up.y < 0) {
            // get the closest path node
            int closestIndex = GetClosesNode();
            if (closestIndex != -1) {
                // get the closest node
                GameObject closestNode = roadPath.transform.GetChild(closestIndex).gameObject;

                // get the nodes position and rotation
                Vector3 closestNodePos = closestNode.transform.position;
                Quaternion closestNodeRot = closestNode.transform.rotation;
                
                // set the car according to the values
                transform.position = closestNodePos;
                transform.rotation = closestNodeRot;
                carRB.velocity = Vector3.zero;
                carRB.angularVelocity = Vector3.zero;
                speed = 0f;
            } else {
                Debug.Log("No closest node found");
            }
        }
    }


    int GetClosesNode() {

        int closestIndex = -1;

        float closestDistance = Mathf.Infinity;
        Vector3 carPosition = transform.position;

        // Iterate through the children of the parent object
        for (int i = 0; i < roadPath.transform.childCount; i++)
        {   

            GameObject obj = roadPath.transform.GetChild(i).gameObject;
            float distance = Vector3.Distance(carPosition, obj.transform.position);
            
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        return closestIndex;
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