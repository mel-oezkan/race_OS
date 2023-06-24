using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelepotationPoint : MonoBehaviour
{
    [SerializeField] private Transform targetTeleportPoint;

    private void TeleportPlayer(Transform playerTransform)
    {
        playerTransform.position = targetTeleportPoint.position;
    }
}