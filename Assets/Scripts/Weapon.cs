using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public abstract class Weapon : MonoBehaviour
{
    public float timeBetweenBullets = 0.15f;                        // time between each shot
    public Transform bulletSpawn;                                   // place where bullet will spawn in weapon
    public AudioClip gunAudio;

    protected float timeToShot;                                     // time to next shot
    protected ParticleSystem gunParticle;
    protected Light gunLight;

    private AudioSource audioSource;



    protected virtual void Update()
    {
        timeToShot += Time.deltaTime;                               // update time to next shot
    }


    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();                                   // get reference to AudioSource
        gunParticle = bulletSpawn.GetComponent<ParticleSystem>();                    // get reference to ParticleSystem
        gunLight = bulletSpawn.GetComponent<Light>();                                // get reference to Light
    }


    protected void ShotEffects()
    {
        if (gunAudio)                                                               // if there is audio clip attached to object
        SoundManager.instance.RandomizeSfx(ref gunAudio, ref audioSource);

        if (gunLight)                                                               // if there is gunLight
            gunLight.enabled = true;

        gunParticle.Stop();
        gunParticle.Play();
    }


    public abstract bool Shot(bool controllShot);                                  // controll shot allows parent bullet to gameobject, when ship is rotate bullet rotate with him

}   // Karol Sobanski
