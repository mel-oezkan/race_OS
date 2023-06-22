using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControls : MonoBehaviour
{
    public Rigidbody carRB;
    public PauseScript PauseScript;
    public WheelColliders colliders;
    public WheelTransforms wheelTransforms;

    // User Inputs
    public float steeringInput = 0.0f;
    public float forwardInput = 0.0f;
    public float brakeInput;

    // Car physics
    public float slipAngle;
    public float brakePower;
    public float speed;
    public AnimationCurve motorPower;
    public AnimationCurve steeringCurve;

    // Environment
    public bool isPaused = false;
    public GameObject roadPath;

    // Debug values

    public bool isFinished = false; // Flag to indicate if the car has finished

    private void Start()
    {
        carRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isPaused || isFinished) // Check if the car is paused or finished
            return;

        speed = carRB.velocity.magnitude;
        HandleInputs();
        ApplyMotorForce();
        ApplyWheelPositions();
        ApplySteering();
        ApplyBreak();
        CheckFlip();

        Debug.DrawLine(transform.position, transform.position + (carRB.velocity * 100f), Color.red);
    }

    private void ApplySteering()
    {
        // Apply steering
        float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
        colliders.fRWheel.steerAngle = steeringAngle;
        colliders.fLWheel.steerAngle = steeringAngle;
    }

    private void ApplyMotorForce()
    {
        // Apply acceleration
        float motorForce = motorPower.Evaluate(speed);
        colliders.fRWheel.motorTorque = motorForce * forwardInput;
        colliders.fLWheel.motorTorque = motorForce * forwardInput;
        colliders.bRWheel.motorTorque = motorForce * forwardInput;
        colliders.bLWheel.motorTorque = motorForce * forwardInput;
    }

    private void HandleInputs()
    {
        steeringInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        Debug.DrawLine(transform.position, transform.forward + (carRB.velocity * 100f), Color.blue);

        slipAngle = Vector3.Angle(transform.forward, carRB.velocity - transform.forward);

        // Difference of the two vectors is greater than 120 degrees
        if (forwardInput < 0)
        {
            if (slipAngle > 120f)
            {
                brakeInput = forwardInput;
                forwardInput = 0f;
            }
            else
            {
                brakeInput = 0f;
            }
        }
        else
        {
            brakeInput = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape pressed");
            Debug.Log("Closest Node: " + GetClosesNode().ToString());
            // PauseScript.Setup();
        }
    }

    private void ApplyBreak()
    {
        colliders.bLWheel.brakeTorque = brakePower * brakeInput * 0.3f;
        colliders.bRWheel.brakeTorque = brakePower * brakeInput * 0.3f;
        colliders.fLWheel.brakeTorque = brakePower * brakeInput * 0.7f;
        colliders.fRWheel.brakeTorque = brakePower * brakeInput * 0.7f;
    }

    private void ApplyWheelPositions()
    {
        UpdateWheel(colliders.fRWheel, wheelTransforms.fRWheel);
        UpdateWheel(colliders.fLWheel, wheelTransforms.fLWheel);
        UpdateWheel(colliders.bRWheel, wheelTransforms.bRWheel);
        UpdateWheel(colliders.bLWheel, wheelTransforms.bLWheel);
    }

    private void UpdateWheel(WheelCollider col, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        col.GetWorldPose(out pos, out rot);

        // Set the pos and rot
        trans.position = pos;
        trans.rotation = rot;
    }

    private bool isActiveFlip = false;

    private void CheckFlip()
    {
        // If the car is upside down, flip it
        if (transform.up.y < 0)
        {
            Debug.Log("Car is flipped");
            if (!isActiveFlip)
            {
                StartCoroutine(CountdownCoroutine(3f));
                isActiveFlip = true;
            }
        }
    }

    private IEnumerator CountdownCoroutine(float countdownTime)
    {
        Debug.Log("Starting countdown");
        float currentCountdownValue = countdownTime;

        yield return new WaitForSeconds(1f);
        while (currentCountdownValue > 0)
        {
            // Reduce the countdown value by one
            currentCountdownValue -= 1f;

            // Car is not flipped, break the timer
            if (transform.up.y > 0.5f)
            {
                isActiveFlip = false;
                yield break;
            }

            // Wait for one second before updating the countdown
            yield return new WaitForSeconds(1f);
        }

        // Get the closest path node
        int closestIndex = GetClosesNode();
        if (closestIndex != -1)
        {
            // Get the closest node
            GameObject closestNode = roadPath.transform.GetChild(closestIndex).gameObject;

            // Get the node's position and rotation
            Vector3 closestNodePos = closestNode.transform.position;
            Quaternion closestNodeRot = closestNode.transform.rotation;

            // Set the car according to the values
            transform.position = closestNodePos;

            Vector3 closestNodeRotValues = closestNodeRot.eulerAngles;
            transform.rotation = Quaternion.Euler(closestNodeRotValues.x, closestNodeRotValues.y, 0f);

            carRB.velocity = Vector3.zero;
            carRB.angularVelocity = Vector3.zero;
            speed = 0f;
        }
        else
        {
            Debug.Log("No closest node found");
        }

        isActiveFlip = false;
        yield break;
    }

    private int GetClosesNode()
    {
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

    public void StopMovement()
    {
        isFinished = true; // Set the isFinished flag to true
        forwardInput = 0f;
        steeringInput = 0f;
        brakeInput = 1f;
        ApplyMotorForce();
        ApplyBreak();
    }
    public void StartMovement()
    {
        isFinished = false; // Reset the isFinished flag to false
        forwardInput = 1f; // Set the forward input to start moving forward
        steeringInput = 0f; // Reset the steering input
        brakeInput = 0f; // Reset the brake input
    }
}

[System.Serializable]
public class WheelColliders
{
    public WheelCollider fRWheel;
    public WheelCollider fLWheel;
    public WheelCollider bRWheel;
    public WheelCollider bLWheel;
}

[System.Serializable]
public class WheelTransforms
{
    public Transform fRWheel;
    public Transform fLWheel;
    public Transform bRWheel;
    public Transform bLWheel;
}
