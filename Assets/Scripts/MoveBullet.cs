using UnityEngine;
using System.Collections;

public class MoveBullet : MonoBehaviour
{
    public float moveSpeed = 500;
    private Rigidbody rigidbody;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        rigidbody.velocity = transform.forward * moveSpeed * Time.deltaTime;
    }
}   // Karol Sobanski
