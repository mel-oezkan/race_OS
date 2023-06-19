using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaPhysics : MonoBehaviour
{
    public GameObject wheelPrefab;
    GameObject[] wheelPrefabs = new GameObject[4];

    Vector3[] wheels = new Vector3[4];
    public Vector2 wheelDistance = new Vector2(1.5f, 2.5f);
    float[] oldDist = new float[4];


    float maxSupensionLen = 3f;
    float supensionMultiplier = 120f;
    float dampSensitivity = 500f;
    float maxDamp = 40f;


    private float groundOffset = 0.5f;


    Rigidbody rb;

    void Awake() 
    {
        for (int i = 0; i < 4; i++) {
            oldDist[i] = maxSupensionLen;

            // instantiate a wheel at the wheel position
            wheelPrefabs[i] = Instantiate(wheelPrefab, wheels[i], Quaternion.identity);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        wheels[0] = transform.right * wheelDistance.x + transform.forward * wheelDistance.y;
        wheels[1] = -transform.right * wheelDistance.x + transform.forward * wheelDistance.y;
        wheels[2] = transform.right * wheelDistance.x - transform.forward * wheelDistance.y;
        wheels[3] = -transform.right * wheelDistance.x - transform.forward * wheelDistance.y;

        for (int i = 0; i < 4; i++) {
            RaycastHit hit;
            Physics.Raycast(transform.position + wheels[i], -transform.up, out hit, maxSupensionLen);

            if ( hit.collider != null ) {
                rb.AddForceAtPosition(
                    Mathf.Clamp(maxSupensionLen - hit.distance, 0f, 3f)
                    * supensionMultiplier
                    * transform.up
                    + transform.up 
                    * Mathf.Clamp(
                        (oldDist[i] - hit.distance)
                        * dampSensitivity, 
                        0, maxDamp
                    ) * Time.deltaTime,
                    transform.position + wheels[i]
                );

                wheelPrefabs[i].transform.position = hit.point + transform.up * groundOffset;
                wheelPrefabs[i].transform.rotation = transform.rotation;

            }  else 
            {
                wheelPrefabs[i].transform.position = transform.position + wheels[i] - transform.up * (maxSupensionLen - groundOffset);
                wheelPrefabs[i].transform.rotation = transform.rotation;
            }

            oldDist[i] = hit.distance;
        }

    }
}
