using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionController : MonoBehaviour     
{
    public static ExplosionController instance;

    [Range(0, 100)]
    [Tooltip("How offen random explosions will be appear")]
    public float percentRandExplosions = 20;

    [Range(0, 1)]
    [Tooltip("How loud explosion will be")]
    public float volume= 0.5f;

    [SerializeField]
    private GameObject[] randomExplosions;                     // visual particle effect attached to gameobject

    [SerializeField]
    private AudioClip randomExplosionsSnd;      



    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }


    public void RandomExplosionEffect(Vector3 explodePosition)  // allows sometimes  create random explosion
    {
        float randPercent = Random.Range(0, 100);

        if (randPercent > percentRandExplosions) return;            // if random percent is greatest than percentRandExplosions don't create explosion

        GameObject newExplosion = Instantiate(randomExplosions[Random.Range(0, randomExplosions.Length)], explodePosition, Quaternion.identity) as GameObject;     // random explosion from array
        newExplosion.transform.SetParent(GameMaster.instance.hierarchyGuard);
        AudioSource.PlayClipAtPoint(randomExplosionsSnd, transform.position, volume);
    }
}   // Karol Sobanski
