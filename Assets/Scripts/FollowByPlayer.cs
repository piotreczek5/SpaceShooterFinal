using UnityEngine;
using System.Collections;

public class FollowByPlayer : MonoBehaviour
{
    public float rotateSpeed = 500;

    private Transform player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
       transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotateSpeed * Time.deltaTime);
    }
}   // Karol Sobanski
