using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTele : MonoBehaviour
{
    public Transform teleportPoint;

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
    }
}

