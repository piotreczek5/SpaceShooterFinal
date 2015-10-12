using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour
{
    public float destroyTime;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}   // Karol Sobanski
