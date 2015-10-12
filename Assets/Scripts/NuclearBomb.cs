using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource), typeof(BoxCollider))]
public class NuclearBomb : MonoBehaviour
{
    public float explosionVolume = 0.2f;
    public AudioClip explosionSound;
    public GameObject explosionEffect;
    public int damage = 1000;



    void Start()
    {
        AudioSource.PlayClipAtPoint(explosionSound, transform.position, explosionVolume);
        GameObject effect = Instantiate(explosionEffect, transform.position, transform.rotation) as GameObject;
        effect.transform.SetParent(GameMaster.instance.hierarchyGuard);
    }


    void OnTriggerStay(Collider other)
    {
        if (other.tag != "Enemy") return;                           // if it is not enemy do nothing

        if (other.GetComponent<EnemyController>())
            other.GetComponent<EnemyController>().TakeDamage(damage, other.transform.position);   // Destroy enemy
    }
}   // Karol Sobanski
