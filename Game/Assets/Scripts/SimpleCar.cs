using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCar : MonoBehaviour
{

    private Vector3 centerPos;
    private Rigidbody carRigidBody;

    private float horizontalInput;
    private float verticalInput;


    public LayerMask groundLayer;
    [SerializeField] public float suspensionRestDist = .250f;
    [SerializeField] public float springStrengh = 1.0f;
    [SerializeField] public float springDamper = 1.0f;

    [SerializeField] private List<Transform> WheelRays;
    [SerializeField] private List<Transform> WheelTransforms;

     
      private void Start() {
        carRigidBody = GetComponent<Rigidbody>();

        Debug.Log(gameObject.transform.position);
      
        centerPos = gameObject.transform.position;
        RaycastHit hit;

        bool hasHit = Physics.Raycast(
            centerPos, Vector3.down, 
            out hit, 100.0f);

        if(hasHit) Debug.Log("Hit Ground");
        else Debug.Log("No Ground");
     }
     
    private void Update() {

    }

    private void GetInput()
    {

    }

    private void HandleSteering()
    {
        
    }

    private void FixedUpdate()
    {
        centerPos = gameObject.transform.position;


        if (Input.GetKeyDown(KeyCode.Space)) {
          for (int i = 0; i < WheelRays.Count; i++) {
            Transform currTireRay = WheelRays[i];
            
            carRigidBody.AddForceAtPosition(
              Vector3.up * 100, currTireRay.position  
            );

          }
        }

        // iterate over the weels and raycast down to check collisions
        for (int i = 0; i < WheelRays.Count; i++) {

          // Transform tireTransform = WheelTransforms[i];
          int debugDir = (i % 2 == 0) ? 1 : -1;
          Transform currTireRay = WheelRays[i];
          
          // create the raycast and check 0.5 units down
          RaycastHit tireRay;
          bool hasHit = Physics.Raycast(
              currTireRay.position, Vector3.down, 
              out tireRay, 0.25f, groundLayer
          );

          Debug.DrawRay(
              currTireRay.position, 
              0.25f * Vector3.down, 
              Color.green
          );

          // Wheel is on the ground
          if (hasHit) {
            Debug.Log("Hit Ground");
            Vector3 springDir = currTireRay.up;
            Vector3 tireWorldVel = carRigidBody.
              GetPointVelocity(currTireRay.position);

            // log tire velocity
            float offset = suspensionRestDist - tireRay.distance;
            float vel = Vector3.Dot(springDir, tireWorldVel);

            float force = (offset + springStrengh) - (vel * springDamper);
            // Debug.Log("Force: " + force);

            carRigidBody.AddForceAtPosition(
              springDir * force , // force 
              currTireRay.position  // position
            );

            Debug.DrawRay(
              currTireRay.position, 
              springDir * force, 
              Color.red
            );
          }
        }
    }





}
