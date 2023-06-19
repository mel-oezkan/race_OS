using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCar : MonoBehaviour
{

    private Vector3 centerPos;
    private Rigidbody carRigidBody;


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


    private void FixedUpdate()
    {
        centerPos = gameObject.transform.position;
        // RaycastHit hit;


        for (int i = 0; i < WheelRays.Count; i++) {
          int debugDir = (i % 2 == 0) ? 1 : -1;
          Transform tireTransform = WheelTransforms[i];
          RaycastHit tireRay;

          bool hasHit = Physics.Raycast(
              tireTransform.position, Vector3.down, 
              out tireRay, 0.5f
          );

          if (hasHit) {
            Vector3 springDir = tireTransform.up;

            Vector3 tireWorldVel = carRigidBody.
              GetPointVelocity(tireTransform.position);

            // log tire velocity
            Debug.Log("Tire Distance: " + tireRay.distance);

            
            float offset = suspensionRestDist - tireRay.distance;
            float vel = Vector3.Dot(springDir, tireWorldVel);

            float force = (offset + springStrengh) - (vel * springDamper);
            // Debug.Log("Force: " + force);

            carRigidBody.AddForceAtPosition(
              springDir * force / 10, 
              tireTransform.position);

            Debug.DrawRay(
              tireTransform.position, 
              springDir * force, 
              Color.red
            );

          }
          



        }
        // if (hasHit) { 

        //   Vector3 springDir = tireTransform.up;
          


        // }


    }





}
