using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Renderer))]
public class WrapImage : MonoBehaviour
{
    public float wrapSpeed;      // How fast can I wrap background

    Renderer renderer;           // Background rendedrer


    void Start()
    {
        renderer = GetComponent<Renderer>();
    }


    
    void Update()
    {
        Vector2 offset = new Vector2(0, wrapSpeed * Time.time);
        renderer.material.mainTextureOffset = offset;
    }
}   // Karol Sobanski
