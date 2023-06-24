using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour
{


    public Transform[] tireTransform = new Transform[4];
    public WheelTransforms wheelTransforms;


    public float restSupensionLen = 1f;
    public float springStrength = 100f;
    public float springDamper = 50f;


    public float carTopSpeed = 100f;
    public float accelInput = 0f;

    public float tireMass = 10f;
    public float steerInput = 0f;
    public float maxSteeringAngle = 30f;


    public AnimationCurve powerCurve;
    public AnimationCurve fontTireGrip;
    public AnimationCurve rearTireGrip;

    public float carSpeed = 0f;

    Rigidbody rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void HandleSuspension() {
         for (int i = 0; i < 4; i++) {
            RaycastHit tireRay;       
            bool rayDidHit = Physics.Raycast(
                tireTransform[i].position, 
                -transform.up, 
                out tireRay, 
                restSupensionLen
            );

            // check if the raycast hit the ground
            if (rayDidHit) {

                // handle the tire damping direction 
                // (find in which direction the force should be applied)
                Vector3 springDir = transform.up;
                Vector3 tireWorldVel = rb.GetPointVelocity(tireTransform[i].position);

                float offset = restSupensionLen - tireRay.distance;

                // calculate the force and velocity
                float vel = Vector3.Dot(springDir, tireWorldVel);
                float force = (offset * springStrength) - (vel * springDamper);


                Debug.DrawLine(
                    tireTransform[i].position,
                    tireTransform[i].position + (vel * springDir),
                    Color.green);

                rb.AddForceAtPosition(
                    (springDir * force), 
                    tireTransform[i].position);
            }
        }
    }

    void UpdateSteering (
        Transform trans, 
        AnimationCurve gripCurve
    ) {
    
        Vector3 steeringDir = trans.right;
        Vector3 tireWorldVel = rb.GetPointVelocity(trans.position);

        float steeringVel = Vector3.Dot(steeringDir, tireWorldVel);
        // float tireGripFactor = gripCurve.Evaluate(Mathf.Abs(steeringVel)); 
        float tireGripFactor = 0.10f;

        float desiredChange = -steeringVel * tireGripFactor;
        float desiredAccel = desiredChange / Time.fixedDeltaTime;

        rb.AddForceAtPosition(
            (steeringDir * desiredAccel * tireMass), 
            trans.position);

        Debug.DrawLine(
            trans.position,
            trans.position + (steeringDir * desiredAccel * tireMass),
            Color.red);
    }

    void HandleSteering () {

        RaycastHit tireRay;       
        bool rayDidHit = Physics.Raycast(
            transform.position, -transform.up, out tireRay, restSupensionLen);
        
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

    void UpdateAcceleration(Transform trans) {
        RaycastHit tireRay;       
        bool rayDidHit = Physics.Raycast(
            trans.position, -transform.up, out tireRay, restSupensionLen);


        if (rayDidHit) {
            Vector3 accelDir = trans.forward;
            Vector3 steeringDir = trans.right;

            // calculate the car speed
            float carSpeed = Vector3.Dot(transform.forward, rb.velocity);

            // normalize the car speed
            float normSpeed = Mathf.Clamp(Mathf.Abs(carSpeed) / carTopSpeed, 0, 1);
            float availableTorque = powerCurve.Evaluate(normSpeed) * accelInput;
            Vector3 accelerationForce = accelDir * availableTorque ;

            // calculate the steering angle
            float steeringAngle = steerInput * maxSteeringAngle;
            Debug.Log(steeringAngle);
            Vector3 steeringForce = steeringDir  * steeringAngle;
            Vector3 test = Vector3.Project(
                accelerationForce,
                (accelerationForce + steeringForce.normalized)
            ).normalized;

            // Vector3 foreceProjection = Vector3.Project(accelerationForce, combinedForce).normalized;
            rb.AddForceAtPosition(
                (test * carTopSpeed ),
                trans.position
            );

            Debug.DrawLine(
                trans.position,
                trans.position + (accelerationForce),
                Color.white);

            Debug.DrawLine(
                trans.position,
                trans.position + (steeringForce),
                Color.white);


        }
    }

    void HandleAcceleration () {
        UpdateAcceleration(wheelTransforms.fLWheel);
        UpdateAcceleration(wheelTransforms.fRWheel);
    }

    void HandleDrag() {
        if (steerInput == 0 && accelInput == 0) {
            for (int i = 0; i< 4; i++) {
                Vector3 tireWorldVel = rb.GetPointVelocity(tireTransform[i].position);
                rb.AddForceAtPosition(
                    tireWorldVel * -0.1f * rb.mass,
                    tireTransform[i].position
                );
            }
        }
    }

     void FixedUpdate()
    {
        steerInput = Input.GetAxis("Horizontal");
        accelInput = Input.GetAxis("Vertical");
        
        
        carSpeed = Vector3.Dot(transform.forward, rb.velocity);

        HandleSuspension();
        HandleSteering();
        HandleAcceleration();
        HandleDrag();

        for (int i = 0; i < 4; i++) {
            Vector3 tireWorldVel = rb.GetPointVelocity(tireTransform[i].position);

            Debug.DrawLine(
                tireTransform[i].position,
                tireTransform[i].position + tireWorldVel,
                Color.yellow);
        }
    }
}
