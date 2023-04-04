using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCar : MonoBehaviour
{


    public LayerMask groundLayer;

    [SerializeField] private List<Transform> WheelRays;
    [SerializeField] private List<Transform> WheelTransforms;

     
      private void Start() {
        Debug.Log(gameObject.transform.position);
      
        // Transform point = WheelRays[0];
        // float groundLen = .05f;
        // RaycastHit hit;
        
        // bool hasHit = Physics.Raycast(
        //     point.position, Vector3.down, 
        //     out hit, groundLen, groundLayer);
        
        // if(hasHit) Debug.Log("Hit Ground");


     }
     
    private void Update() {

    }


    private void FixedUpdate()
    {

    }




}
