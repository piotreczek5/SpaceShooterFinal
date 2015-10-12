using UnityEngine;
using System.Collections;


public class MoveObject : MonoBehaviour
{
    public float minOffsetZ = -0.4f, maxOffsetZ = -0.6f;

    private Vector3 offset;



    void Start()
    {
        offset = new Vector3(0, 0, Random.Range(minOffsetZ, maxOffsetZ));         // set randoum value on Z axis  
    }


    void Update()
    {
        transform.position += offset * Time.deltaTime;                             // set new position
    }
}  // Karol Sobanski


