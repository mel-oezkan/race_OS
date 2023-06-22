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



    Rigidbody rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
     void FixedUpdate()
    {
        for (int i = 0; i < 4; i++) {
            RaycastHit tireRay;       
            bool rayDidHit = Physics.Raycast(
                tireTransform[i].position, -transform.up, out tireRay, restSupensionLen);

            Debug.DrawLine(
                    tireTransform[i].position,
                    tireTransform[i].position + tireTransform[i].up,
                    Color.green);

            if (rayDidHit) {
                
                Vector3 springDir = tireTransform[i].up;
                Vector3 tireWorldVel = rb.GetPointVelocity(tireTransform[i].position);

                float offset = restSupensionLen - tireRay.distance;

                float vel = Vector3.Dot(springDir, tireWorldVel);

                float force = (offset * springStrength) - (vel * springDamper);

                rb.AddForceAtPosition(
                    (springDir * force), 
                    tireTransform[i].position);
            }
        }

        steerInput = Input.GetAxis("Horizontal");

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

                // if (i > 2) {
                //     float carSpeed = Vector3.Dot(transform.forward, rb.velocity);
                //     float normSpeed = Mathf.Clamp01(carSpeed / carTopSpeed);
                    
                //     // based on the speed of the car, get the possible steering angle
                //     // float availableSteerTorque = steeringCurve.Evaluate(normSpeed) * steerInput;
                //     float availableSteerTorque = 0.5f * steerInput;
                    
                //     if (steerInput != 0) {
                //     rb.AddForceAtPosition(
                //         (steeringDir * availableSteerTorque),
                //         tireTransform[i].position
                //     );}
                // }


                float tireGripFactor = (i < 2) 
                    ? fontTireGrip.Evaluate(steeringVel) 
                    : rearTireGrip.Evaluate(steeringVel); 

                Debug.Log(tireGripFactor);
                
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

        // only steer the car with front tires
        for (int i = 0; i < 2; i++) 
        {
            RaycastHit tireRay;       
            bool rayDidHit = Physics.Raycast(
                tireTransform[i].position, -transform.up, out tireRay, restSupensionLen);


            accelInput = Input.GetAxis("Vertical");

            if (rayDidHit) {
                Vector3 accelDir = tireTransform[i].forward;


                float carSpeed = Vector3.Dot(transform.forward, rb.velocity);

                // normalize the car speed
                float normSpeed = Mathf.Clamp01(carSpeed / carTopSpeed);
                float availableTorque = powerCurve.Evaluate(normSpeed * accelInput) / Time.fixedDeltaTime;

                if (accelInput > 0.0f) {
                    Debug.Log(availableTorque);

                    Debug.DrawLine(
                        tireTransform[i].position,
                        tireTransform[i].position + (accelDir * availableTorque),
                        Color.red);

                    rb.AddForceAtPosition(
                        (accelDir * availableTorque),
                        tireTransform[i].position
                    );
                } else {
                    
                    Debug.DrawLine(
                        tireTransform[i].position,
                        tireTransform[i].position + (-accelDir * availableTorque) * 0.6f,
                        Color.red);

                    rb.AddForceAtPosition(
                        (-accelDir * availableTorque) * 0.6f,
                        tireTransform[i].position
                    );

                } 

            }
        }


    }
}
