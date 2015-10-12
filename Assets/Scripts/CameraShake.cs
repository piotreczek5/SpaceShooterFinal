using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    [Header("Player shake settings")]
    [SerializeField]
    private float damageAmplitude = 0.2f;      // how strong camera will be shake after take damage
    [SerializeField]
    private float damageDuration = 0.15f;      // how long player will see red screen
    [SerializeField]
    private float shotAmplitude = 0.01f;        // how strong camera will be shake after shooting
    [SerializeField]
    private float shotDuration = 0.2f;        // how long player will see red screen

    private Vector3 initialPosition;
    private float amplitude = 0.1f;
    private bool isShaking;



    void Update()
    {
        if (isShaking)
            transform.localPosition = initialPosition + Random.insideUnitSphere * amplitude;
    }


    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        initialPosition = transform.position;
    }


    public void Shake(float amplitude, float duration)
    {
        this.amplitude = amplitude;
        isShaking = true;
        CancelInvoke();
        Invoke("StopShaking", duration);
    }


    public void ShotShake()
    {
        Shake(shotAmplitude, shotDuration);
    }

    public void DamageShake()
    {
        Shake(damageAmplitude, damageDuration);
    }

    public void StopShaking()
    {
        isShaking = false;
    }
}   // Karol Sobanski
