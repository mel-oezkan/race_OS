using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
   
    [SerializeField] private Rigidbody _carRB;
    [SerializeField] private Transform _car;
    [SerializeField] private Vector3 _offset;

    private void Start() 
    {
        _carRB = _car.GetComponent<Rigidbody>();
    }

    // updated after all other updates
    private void LateUpdate() 
    {   
        // calculate the car speed for dynamic camera movement
        float carSpeed = Vector3.Dot(_car.forward, _carRB.velocity);

        transform.position = Vector3.Lerp(
            transform.position, 
            _car.position + _car.transform.TransformVector(_offset), 
            Time.deltaTime * carSpeed
        );

        transform.LookAt(_car);
    }

}
