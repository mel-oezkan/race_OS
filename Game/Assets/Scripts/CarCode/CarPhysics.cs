using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour
{

    [SerializeField] private SoundControls soundControls;
    [SerializeField] private CountdownTimer countdownTimer;

    // car specific variables
    [SerializeField] private Transform[] tireTransform = new Transform[4];
    [SerializeField] private WheelTransforms wheelTransforms;
    Rigidbody rb;

    // handles the parameters for the car suspension
    [SerializeField] private float _restSupensionLen = 1f;
    [SerializeField] private float _springStrength = 100f;
    [SerializeField] private float _springDamper = 50f;

    // handles the parameters for the car acceleration
    [SerializeField] private float _carTopSpeed = 100f;
    [SerializeField] private float _speedmultiplier = 1f;
    [SerializeField] private float _steerInput = 0f; // right left input
    [SerializeField] private float _accelInput = 0f; // forward backward input

    // handles the parameters for the car steering forces
    //[SerializeField] private float maxSteeringAngle = 30f;
    [SerializeField] private float tireMass = 10f;

    // other params
    private float _dragFactor = -0.1f;
    public bool _isFinished = false;
    private bool _canMove = false;

    [SerializeField] private AnimationCurve powerCurve;
    [SerializeField] private AnimationCurve fontTireGrip;
    [SerializeField] private AnimationCurve rearTireGrip;
    [SerializeField] private AnimationCurve steeringCurve;
    [SerializeField] private bool isBoostEnabled = false;
    


     // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        countdownTimer.OnCountdownFinished += EnableMovement;
    }
    
    public void Update()
    {
        // Turbo boost
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(!isBoostEnabled)
            {
                isBoostEnabled = true;
                _speedmultiplier = 100f;
                Debug.Log("Boost enabled");
            }
            else if(isBoostEnabled)
            {
                isBoostEnabled = false;
                Debug.Log("Boost disabled");
                _accelInput = 0f;
                _speedmultiplier = 1f;
            }
        }
        
    }

    void FixedUpdate()
    {
        _steerInput = Input.GetAxis("Horizontal");
        _accelInput = Input.GetAxis("Vertical");

        if (countdownTimer._canMove && !_isFinished)
        {
            // Handle movement only if the countdown has reached "GO"
            HandleSuspension();
            HandleSteering();
            HandleAcceleration();
            HandleDrag();
        }

        for (int i = 0; i < 4; i++)
        {
            Vector3 tireWorldVel = rb.GetPointVelocity(tireTransform[i].position);

            Debug.DrawLine(
                tireTransform[i].position,
                tireTransform[i].position + tireWorldVel,
                Color.yellow);
        }

        // Motor Sounds
        if ((_accelInput > 0) && (!soundControls.isPlaying()))
        {
            soundControls.playSound("acceleration");
        }
        if (_accelInput == 0)
        {
            soundControls.clipStop();
        }

        // Background Motor
        if ((rb.velocity.x > 2) && (rb.velocity.z > 2))
        {
            soundControls.playSound("backgroundMotor");
        }
        else
        {
            soundControls.backgroundMotorStop();
        }
    }

    public void StopMovement()
    {
        Debug.Log("stops");
        _accelInput = 0f;
        _steerInput = 0f;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        _isFinished = true;
    }

    private void EnableMovement()
    {
        _canMove = true;
    }

    public void ReduceSpeed(float reductionAmount, float duration)
    {
        // Get the current velocity of the car
        Vector3 currentVelocity = rb.velocity;

        // Calculate the reduced velocity
        Vector3 reducedVelocity = currentVelocity * (1f - reductionAmount);

        // Set the reduced velocity to the rigidbody
        rb.velocity = reducedVelocity;

        // Wait for the specified duration
        StartCoroutine(RestoreSpeedAfterDelay(duration, currentVelocity));
    }

    private IEnumerator RestoreSpeedAfterDelay(float delay, Vector3 originalVelocity)
    {
        yield return new WaitForSeconds(delay);

        // Restore the original velocity
        rb.velocity = originalVelocity;
    }


    void HandleDrag() {
        // apply the drag if car is not in motion
        if (_steerInput == 0 && _accelInput == 0) {
            UpdateDrag(wheelTransforms.fLWheel);
            UpdateDrag(wheelTransforms.fRWheel);
            UpdateDrag(wheelTransforms.bLWheel);
            UpdateDrag(wheelTransforms.bRWheel);
        }
    }

    void HandleSuspension() {
        UpdateSuspension(wheelTransforms.fLWheel);
        UpdateSuspension(wheelTransforms.fRWheel);
        UpdateSuspension(wheelTransforms.bRWheel);
        UpdateSuspension(wheelTransforms.bLWheel);
    }

    void HandleSteering () {

        RaycastHit tireRay;       
        bool rayDidHit = Physics.Raycast(
            transform.position, -transform.up, out tireRay, _restSupensionLen);
        
        // checking for every individual tire caused some bugs since
        // one wheel can add resistance while one is minimally to far up       
        if (rayDidHit) {
            // handle front tires
            UpdateSteering(wheelTransforms.fRWheel, fontTireGrip);
            UpdateSteering(wheelTransforms.fLWheel, fontTireGrip);        

            // handle rear tires
            UpdateSteering(wheelTransforms.bRWheel, rearTireGrip);
            UpdateSteering(wheelTransforms.bLWheel, rearTireGrip);        
        }
    }


    void HandleAcceleration () {
        UpdateAcceleration(wheelTransforms.fLWheel);
        UpdateAcceleration(wheelTransforms.fRWheel);
    }



    void UpdateSuspension(Transform trans) {
        RaycastHit tireRay;       
        bool rayDidHit = Physics.Raycast(
            trans.position, 
            -transform.up, 
            out tireRay, 
            _restSupensionLen
        );

        // check if the raycast hit the ground
        if (rayDidHit) {

            // handle the tire damping direction 
            // (find in which direction the force should be applied)
            Vector3 springDir = transform.up;
            Vector3 tireWorldVel = rb.GetPointVelocity(trans.position);

            float offset = _restSupensionLen - tireRay.distance;

            // calculate the force and velocity
            float vel = Vector3.Dot(springDir, tireWorldVel);
            float force = (offset * _springStrength) - (vel * _springDamper);

            Debug.DrawLine(
                trans.position,
                trans.position + (vel * springDir),
                Color.green);
            
            rb.AddForceAtPosition(
                (springDir * force), 
                trans.position);
        }
    }


    void UpdateSteering (
        Transform trans, 
        AnimationCurve gripCurve
    ) {
    
        Vector3 steeringDir = trans.right;
        Vector3 tireWorldVel = rb.GetPointVelocity(trans.position);

        float steeringVel = Vector3.Dot(steeringDir, tireWorldVel);
        float tireGripFactor = gripCurve.Evaluate(Mathf.Abs(steeringVel)); 

        float desiredChange = -steeringVel * tireGripFactor;
        float desiredAccel = desiredChange / Time.fixedDeltaTime;

        rb.AddForceAtPosition(
            (steeringDir * desiredAccel * tireMass), 
            trans.position);

        Debug.DrawLine(
            trans.position,
            trans.position + steeringDir * desiredAccel , // (steeringDir * desiredAccel * tireMass),
            Color.red);

        Debug.DrawLine(
            trans.position,
            trans.position + (steeringDir * steeringVel),
            Color.white);
    }

    

    void UpdateAcceleration(Transform trans) {
        RaycastHit tireRay;       
        bool rayDidHit = Physics.Raycast(
            trans.position, -transform.up, out tireRay, _restSupensionLen);


        if (rayDidHit) {
            Vector3 accelDir = trans.forward;
            Vector3 steeringDir = trans.right;

            // calculate the car speed
            float carSpeed = Vector3.Dot(transform.forward, rb.velocity);

            // normalize the car speed
            float normSpeed = Mathf.Clamp(Mathf.Abs(carSpeed) / _carTopSpeed, 0, 1);
            float availableTorque = powerCurve.Evaluate(normSpeed) * _accelInput * _speedmultiplier;
            Vector3 accelerationForce = accelDir * availableTorque ;

            // calculate the steering angle
            float steeringSensitivity = 0.5f; // Adjust the sensitivity as needed
            float clampedSteeringAngle = _steerInput;

            Vector3 totalForce = accelDir * availableTorque + steeringDir * _steerInput;
           
            // Vector3 foreceProjection = Vector3.Project(accelerationForce, combinedForce).normalized;
            rb.AddForceAtPosition(
                totalForce * _carTopSpeed ,
                trans.position
            );

            Debug.DrawLine(
                trans.position,
                trans.position + (accelerationForce),
                Color.white);


        }
    }


    void UpdateDrag(Transform trans) {
        // gets the velocity of the tire and reduces by some factor
        Vector3 tireWorldVel = rb.GetPointVelocity(trans.position);
        rb.AddForceAtPosition(
            tireWorldVel * _dragFactor * rb.mass,
            trans.position
        );
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