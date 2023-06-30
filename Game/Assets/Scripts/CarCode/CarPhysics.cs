using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour
{

    [SerializeField] private SoundControls soundControls;
    [SerializeField] private CountdownTimer countdownTimer;

    // car specific variables
    Rigidbody carRb;
    [SerializeField] private WheelTransforms _wheelTransforms;

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
    [SerializeField] private float _tireMass = 10f;

    // other params
    public bool _isFinished = false;
    private bool _canMove = false;
    private float _dragFactor = -0.1f;

    [SerializeField] private AnimationCurve _powerCurve;
    [SerializeField] private AnimationCurve _fontTireGrip;
    [SerializeField] private AnimationCurve _rearTireGrip;
    [SerializeField] private bool _isBoostEnabled = false;
    [SerializeField] ParticleSystem _turboparticle;

     // Start is called before the first frame update
    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        countdownTimer.OnCountdownFinished += EnableMovement;
    }
    
    public void Update()
    {
        // Turbo boost and its effect
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(!_isBoostEnabled)
            {
                _isBoostEnabled = true;
                _speedmultiplier = 3f;
                _turboparticle.startSpeed = 10f;
                _turboparticle.Play();
                
                DoDelay(1f, () => {
                    _isBoostEnabled = false;
                    _accelInput = 0f;
                    _speedmultiplier = 1f;
                    _turboparticle.Stop();
                });
            }
        }
        
    }

    void DoDelay(float delayTime, System.Action callback)
    {
        StartCoroutine(DelayCoroutine(delayTime, callback));
    }

    IEnumerator DelayCoroutine(float delayTime, System.Action callback)
    {
        yield return new WaitForSeconds(delayTime);
        callback?.Invoke();
    }


    void FixedUpdate()
    {
        _steerInput = Mathf.Clamp(Input.GetAxis("Horizontal"), -0.5f, 0.5f);
        _accelInput = Mathf.Clamp(Input.GetAxis("Vertical"), -1f, 1f);

        if (countdownTimer._canMove && !_isFinished)
        {
            Debug.Log("Car is moving");
            // Handle movement only if the countdown has reached "GO"
            RaycastHit carRay;       
            bool rayDidHit = Physics.Raycast(
                _wheelTransforms.bLWheel.position, -transform.up, out carRay, _restSupensionLen);
            HandleSuspension();
            
            if (rayDidHit) {
                HandleSteering();
                HandleAcceleration();
            }

            HandleDrag();
        }

        // Handle the car sounds
        if (_accelInput > 0 && !soundControls.isPlaying())
        {
            soundControls.playSound("acceleration");
        }
        if (_accelInput == 0)
        {
            soundControls.clipStop();
        }

        // Background Motor
        if ((carRb.velocity.x > 2) && (carRb.velocity.z > 2))
        {
            soundControls.playSound("backgroundMotor");
        }
        else
        {
            soundControls.backgroundMotorStop();
        }
    }


    private void EnableMovement()
    {
        _canMove = true;
    }

    public void ReduceSpeed(float reductionAmount, float duration)
    {
        // Get the current velocity of the car
        Vector3 currentVelocity = carRb.velocity;

        // Calculate the reduced velocity
        Vector3 reducedVelocity = currentVelocity * (1f - reductionAmount);

        // Set the reduced velocity to the rigidbody
        carRb.velocity = reducedVelocity;

        // Wait for the specified duration
        StartCoroutine(RestoreSpeedAfterDelay(duration, currentVelocity));
    }

    private IEnumerator RestoreSpeedAfterDelay(float delay, Vector3 originalVelocity)
    {
        yield return new WaitForSeconds(delay);

        // Restore the original velocity
        carRb.velocity = originalVelocity;
    }


    void HandleDrag() {
        // apply the drag if car is not in motion
        if (_steerInput == 0 && _accelInput == 0) {
            UpdateDrag(_wheelTransforms.fLWheel);
            UpdateDrag(_wheelTransforms.fRWheel);
            UpdateDrag(_wheelTransforms.bLWheel);
            UpdateDrag(_wheelTransforms.bRWheel);
        }
    }

    void HandleSuspension() {
        UpdateSuspension(_wheelTransforms.fLWheel);
        UpdateSuspension(_wheelTransforms.fRWheel);
        UpdateSuspension(_wheelTransforms.bRWheel);
        UpdateSuspension(_wheelTransforms.bLWheel);
    }

    void HandleSteering () {
    
        // handle front tires
        UpdateSteering(_wheelTransforms.fRWheel, _fontTireGrip);
        UpdateSteering(_wheelTransforms.fLWheel, _fontTireGrip);        

        // handle rear tires
        UpdateSteering(_wheelTransforms.bRWheel, _rearTireGrip);
        UpdateSteering(_wheelTransforms.bLWheel, _rearTireGrip);        
    }


    void HandleAcceleration () {

        UpdateAcceleration(_wheelTransforms.fLWheel);
        UpdateAcceleration(_wheelTransforms.fRWheel);
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
            Vector3 tireWorldVel = carRb.GetPointVelocity(trans.position);

            float offset = _restSupensionLen - tireRay.distance;

            // calculate the force and velocity
            float vel = Vector3.Dot(springDir, tireWorldVel);
            float force = (offset * _springStrength) - (vel * _springDamper);

            Debug.DrawLine(
                trans.position,
                trans.position + (vel * springDir),
                Color.green);
            
            carRb.AddForceAtPosition(
                (springDir * force), 
                trans.position);
        }
    }


    void UpdateSteering (
        Transform trans, 
        AnimationCurve gripCurve
    ) {
    
        Vector3 steeringDir = trans.right;
        Vector3 tireWorldVel = carRb.GetPointVelocity(trans.position);

        float steeringVel = Vector3.Dot(steeringDir, tireWorldVel);
        float tireGripFactor = gripCurve.Evaluate(steeringVel); 

        float desiredChange = -steeringVel * tireGripFactor;
        float desiredAccel = desiredChange / Time.fixedDeltaTime;

        carRb.AddForceAtPosition(
            (steeringDir * desiredAccel * _tireMass), 
            trans.position);

        Debug.DrawLine(
            trans.position,
            trans.position + (steeringDir * desiredAccel * _tireMass),
            Color.red);

    }
    

    void UpdateAcceleration(Transform trans) {
        // update the acceleration of the car
        RaycastHit tireRay;       
        bool rayDidHit = Physics.Raycast(
            trans.position, -transform.up, out tireRay, _restSupensionLen);


        if (rayDidHit) {
            Vector3 accelDir = trans.forward;
            Vector3 steeringDir = trans.right;

            // calculate the car speed
            float carSpeed = Vector3.Dot(transform.forward, carRb.velocity);

            // normalize the car speed
            float normSpeed = Mathf.Clamp(Mathf.Abs(carSpeed) / _carTopSpeed, 0, 1);
            float availableTorque = _powerCurve.Evaluate(normSpeed) * _accelInput * _speedmultiplier;
            Vector3 accelerationForce = accelDir * availableTorque ;

            // calculate the steering angle
            float steeringSensitivity = 0.5f; // Adjust the sensitivity as needed
            float clampedSteeringAngle = _steerInput;

            Vector3 totalForce = accelDir * availableTorque + steeringDir * _steerInput;
           
            // Vector3 foreceProjection = Vector3.Project(accelerationForce, combinedForce).normalized;
            carRb.AddForceAtPosition(
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
        Vector3 tireWorldVel = carRb.GetPointVelocity(trans.position);
        carRb.AddForceAtPosition(
            tireWorldVel * _dragFactor * carRb.mass,
            trans.position
        );
    }
}


[System.Serializable]
public class WheelTransforms
{
    public Transform fRWheel;
    public Transform fLWheel;
    public Transform bRWheel;
    public Transform bLWheel;
}