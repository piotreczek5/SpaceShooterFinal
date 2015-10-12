using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class RandomRotator : MonoBehaviour
{
    public float rotateSpeed;


    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * Time.deltaTime * rotateSpeed;
    }

}   // Karol Sobanski
