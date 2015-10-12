using UnityEngine;
using System.Collections;

public class RocketVibration : MonoBehaviour
{
    [Tooltip("max amount, where rocket can move")]
    public float amplitude = 0.01f;
    public float vibrationSpeed = 6f;


    IEnumerator Start()
    {
        while (true)
        {

            Vector3 minOffset = new Vector3(transform.position.x - amplitude, transform.position.y, transform.position.z);    // save new min offset position
            Vector3 maxOffset = new Vector3(transform.position.x + amplitude, transform.position.y, transform.position.z);    // save new max offset position


            float t = Mathf.PingPong(Time.time * vibrationSpeed, 1);    //
            transform.position = Vector3.Lerp(minOffset, maxOffset, t); // move object from a->b->a...
            yield return null;
        }
    }
}   // Karol Sobański 
