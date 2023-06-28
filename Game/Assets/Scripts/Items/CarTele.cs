using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTele : MonoBehaviour
{
    [SerializeField] private Rigidbody carRigidbody;
    [SerializeField] private Transform teleportPoint;

    private float _teleportBoostForce = 10f;

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
        carRigidbody.velocity = teleportPoint.forward * _teleportBoostForce;
    }
}

