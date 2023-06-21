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

        // only steer the car with front tires
        for (int i = 0; i < 2; i++) 
        {
            RaycastHit tireRay;       
            bool rayDidHit = Physics.Raycast(
                tireTransform[i].position, -transform.up, out tireRay, restSupensionLen);

            Debug.DrawLine(
                tireTransform[i].position,
                tireTransform[i].position + tireTransform[i].up * 10f,
                Color.blue);

            accelInput = Input.GetAxis("Vertical");
            if (rayDidHit) {
                Vector3 accelDir = tireTransform[i].forward;

                float carSpeed = Vector3.Dot(transform.forward, rb.velocity);

                // normalize the car speed
                float normSpeed = Mathf.Clamp01(carSpeed / carTopSpeed);
                float availableTorque = normSpeed * accelInput;

                if (accelInput > 0.0f) {
                    

                    rb.AddForceAtPosition(
                        accelDir * availableTorque,
                        tireTransform[i].position
                    );
                }


                Debug.DrawLine(
                    tireTransform[i].position,
                    tireTransform[i].position + (accelDir * availableTorque),
                    Color.blue);
            }
        }


    }
}
