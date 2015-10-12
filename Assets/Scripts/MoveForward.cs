using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
public class MoveForward : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        rigidbody.velocity = transform.forward * Time.deltaTime * moveSpeed;
    }
}
