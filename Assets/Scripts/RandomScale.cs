using UnityEngine;
using System.Collections;

public class RandomScale : MonoBehaviour
{
    [Tooltip("Diferent random values for x,y,z axis")]
    public bool randomAllAxis = false;

    public float minScale = 0.5f;
    public float maxScale = 3.5f;



    void Start()
    {
        if (randomAllAxis)
        {
            float x = Random.Range(minScale, maxScale);
            float y = Random.Range(minScale, maxScale);
            float z = Random.Range(minScale, maxScale);

            transform.localScale = new Vector3(x, y, z);

        }
        else
        {
            float newScale = Random.Range(minScale, maxScale);
            transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}  // Karol Sobanski
