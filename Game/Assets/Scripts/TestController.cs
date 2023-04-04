using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private float horizontalInput;
    private float verticalInput;


    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(
            new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime
        );

    }

}
