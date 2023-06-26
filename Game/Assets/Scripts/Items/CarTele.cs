using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTele : MonoBehaviour
{
    public Rigidbody carRigidbody;
    public Transform teleportPoint;
    public float teleportBoostForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TeleportTrigger"))
        {
            TeleportCar();
        }
    }

    private void TeleportCar()
    {
        transform.position = teleportPoint.position;
        transform.rotation = teleportPoint.rotation;
        carRigidbody.velocity = teleportPoint.forward * teleportBoostForce;
    }
    public void Start()
    {
       // carRigidbody = GetComponent<Rigidbody>();
    }
}

