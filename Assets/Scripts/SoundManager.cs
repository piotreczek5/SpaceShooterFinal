using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    [Header("AudioSources")]
    public static SoundManager instance;
    public AudioSource efxSource;
    public AudioSource musicSource;

    [Header("Parameters")]
    public float lowPitchRange = 0.95f;            // +/-5% original pitch
    public float highPitchRange = 1.05f;



    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    public void RandomizeSfx( ref AudioClip[] clips, ref AudioSource audioSource)                                // change a little sound of efx, and audioSource settings
    {
        int randomIndex = Random.Range(0, clips.Length);                   
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        audioSource.pitch = randomPitch;
        audioSource.clip = clips[randomIndex];
        audioSource.Play();
    }


    public void RandomizeSfx(ref AudioClip clip, ref AudioSource audioSource)                                   // change a little sound of efx, and audioSource settings
    {
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        audioSource.pitch = randomPitch;
        audioSource.clip = clip;
        audioSource.Play();
    }
}   // Karol Sobanski
