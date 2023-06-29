using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CarController : MonoBehaviour
{
    //References
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private FinishLine finishLine;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider wheelFrontLeft;
    [SerializeField] private WheelCollider wheelFrontRight;
    [SerializeField] private WheelCollider wheelBackLeft;
    [SerializeField] private WheelCollider wheelBackRight;

    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform backLeftTransform;
    [SerializeField] private Transform backRightTransform;

    //Variables
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentBrakeForce;
    private float currentSteerAngle;
    private bool isBreaking;
    private bool canMove;



    private void Start()
    {
        if (countdownTimer != null)
        {
            countdownTimer.OnCountdownFinished += EnableMovement;
        }
    }

    private void OnDestroy()
    {
        if (countdownTimer != null)
        {
            countdownTimer.OnCountdownFinished -= EnableMovement;
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            GetInput();
            HandleMotor();
            HandleSteering();
            UpdateWheels();

            speedText.text = "Speed: " + (int)wheelBackLeft.rpm;
        }
    }

    private void HandleMotor()
    {
        wheelBackLeft.motorTorque = verticalInput * motorForce;
        wheelBackRight.motorTorque = verticalInput * motorForce;

        currentBrakeForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        wheelFrontLeft.brakeTorque = currentBrakeForce;
        wheelFrontRight.brakeTorque = currentBrakeForce;
        wheelBackLeft.brakeTorque = currentBrakeForce;
        wheelBackRight.brakeTorque = currentBrakeForce;
    }

    private void GetInput()
    {
        // Get input from player
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        wheelFrontLeft.steerAngle = currentSteerAngle;
        wheelFrontRight.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(wheelFrontLeft, frontLeftTransform);
        UpdateSingleWheel(wheelFrontRight, frontRightTransform);
        UpdateSingleWheel(wheelBackLeft, backLeftTransform);
        UpdateSingleWheel(wheelBackRight, backRightTransform);
    }

    private void UpdateSingleWheel(
        WheelCollider collider,
        Transform transform
    )
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        transform.rotation = rot;
        transform.position = pos;
    }

    private void EnableMovement()
    {
        canMove = true;
    }

    public void StopMovement()
    {
        //wheelBackLeft.motorTorque = 0f;
        //wheelBackRight.motorTorque = 0f;
        canMove = false;
    }

}
