using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour
{
    [Header("Random start rotation on axis")]
    public bool rotateOnX = true;
    public bool rotateOnY = true;


    [Header("Rotate settings")]
    [Tooltip("Interval rotate values in deegres")]
    public float minXRotate = 0;
    public float maxXRotate = 360;

    [Space(5)]
    public float minYRotate = 0;
    public float maxYRotate = 360;

    [Space(5)]
    [Tooltip("UpdateRotator can change object rotation in every frame")]
    public bool isUpdateRotatorOn = true;
    public float rotateSpeed = 2;



    void Start()
    {
        float x;
        float y;

        if (rotateOnX)
            x = Random.Range(minXRotate, maxXRotate);        // random rotation X
        else
            x = transform.rotation.eulerAngles.x;


        if (rotateOnY)
            y = Random.Range(minYRotate, maxYRotate);                        // random rotation Y
        else
            y = transform.rotation.eulerAngles.y;


        Vector3 startRotation = new Vector3(x, y, transform.rotation.z);
        transform.rotation = Quaternion.Euler(startRotation);

        if (isUpdateRotatorOn)
            StartCoroutine(UpdateRotator(x, y));
    }


    IEnumerator UpdateRotator(float x, float y)
    {
        while (true)
        {
            Vector3 newRotation = new Vector3(x, y, rotateSpeed * Time.time);
            transform.rotation = Quaternion.Euler(newRotation);
            yield return null;
        }
    }
}  // Karol Sobanski
