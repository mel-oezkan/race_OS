using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour
{


    public Transform[] tireTransform = new Transform[4];


    public float restSupensionLen = 1f;
    public float springStrength = 100f;
    public float springDamper = 50f;


    public float carTopSpeed = 100f;
    public float accelInput = 0f;

    public float tireMass = 10f;
    public float steerInput = 0f;


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
                Vector3 rotationDir = transform.up * steerInput * 5f;
                transform.Rotate(rotationDir * Time.deltaTime);

                // handle the tire damping direction 
                // (find in which direction the force should be applied)
                Vector3 springDir = transform.up;
                Vector3 tireWorldVel = rb.GetPointVelocity(tireTransform[i].position);

                float offset = restSupensionLen - tireRay.distance;

                // calculate the force and velocity
                float vel = Vector3.Dot(springDir, tireWorldVel);
                float force = (offset * springStrength) - (vel * springDamper);

                // Debug.DrawLine(
                //     tireTransform[i].position,
                //     tireTransform[i].position + (springDir * force),
                //     Color.green);

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

    void HandleSteering () {
        for (int i = 0; i < 4; i++) {
            RaycastHit tireRay;       
            bool rayDidHit = Physics.Raycast(
                tireTransform[i].position, -transform.up, out tireRay, restSupensionLen);

            Debug.DrawLine(
                    tireTransform[i].position,
                    tireTransform[i].position + tireTransform[i].right,
                    Color.green);
            
            if (rayDidHit) {
                Vector3 steeringDir = tireTransform[i].right;
                Vector3 tireWorldVel = rb.GetPointVelocity(tireTransform[i].position);

                float steeringVel = Vector3.Dot(steeringDir, tireWorldVel);

                float tireGripFactor = (i < 2) 
                    ? fontTireGrip.Evaluate(steeringVel) 
                    : rearTireGrip.Evaluate(steeringVel); 

                
                float desiredChange = -steeringVel * tireGripFactor;
                float desiredAccel = desiredChange / Time.fixedDeltaTime;

                rb.AddForceAtPosition(
                    (steeringDir * desiredAccel * tireMass), 
                    tireTransform[i].position);

                Debug.DrawLine(
                    tireTransform[i].position,
                    tireTransform[i].position + (steeringVel * steeringDir),
                    Color.black);

            }

        }
    }

    void HandleAcceleration () {
        // only steer the car with front tires
        for (int i = 0; i < 2; i++) 
        {
            RaycastHit tireRay;       
            bool rayDidHit = Physics.Raycast(
                tireTransform[i].position, -transform.up, out tireRay, restSupensionLen);


            if (rayDidHit) {
                Vector3 accelDir = tireTransform[i].forward;


                float carSpeed = Vector3.Dot(transform.forward, rb.velocity);

                // normalize the car speed
                float normSpeed = Mathf.Clamp01(carSpeed / carTopSpeed);
                float availableTorque = powerCurve.Evaluate(normSpeed * accelInput) / Time.fixedDeltaTime;

                if (accelInput > 0.0f) {

                    Debug.DrawLine(
                        tireTransform[i].position,
                        tireTransform[i].position + (accelDir * availableTorque),
                        Color.red);

                    rb.AddForceAtPosition(
                        (accelDir * availableTorque),
                        tireTransform[i].position
                    );
                } else if (accelInput < 0.0f) {
                    
                    Debug.DrawLine(
                        tireTransform[i].position,
                        tireTransform[i].position + (-accelDir * availableTorque) * 0.6f,
                        Color.red);


                    rb.AddForceAtPosition(
                        (-accelDir * availableTorque) * 0.6f,
                        tireTransform[i].position
                    );

                } else {

                }

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
        
        if (steerInput == 0 && accelInput == 0) {
            for (int i = 0; i< 4; i++) {
                Vector3 tireWorldVel = rb.GetPointVelocity(tireTransform[i].position);
                rb.AddForceAtPosition(
                    tireWorldVel * -0.1f * rb.mass,
                    tireTransform[i].position
                );

                Debug.DrawLine(
                    tireTransform[i].position,
                    tireTransform[i].position + tireWorldVel,
                    Color.yellow);
            }
        }



    }
}
