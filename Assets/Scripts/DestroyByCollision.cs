using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(AudioSource))]
public class DestroyByCollision : MonoBehaviour
{
    public int damage = 30;
    public float volume = 0.5f;
    public GameObject[] destroyEffects;
    public AudioClip destroySounds;

    [Tooltip("if projectile hit some object, he can disable his ability to move or shot for while")]
    public bool disableControl;
    public float howLong = 2;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Background" || gameObject.tag == other.tag || other.tag == "PickUp") return;
        if (tag == "PlayerBullet" && other.tag == "Player" || tag == "EnemyBullet" && other.tag == "Enemy")
            return;


        if (destroyEffects.Length > 0)                                                                              // if ther is some destroyeffects attached
        {
            int randIndex = Random.Range(0, destroyEffects.Length);                                                 // choose random index in array
            GameObject randEffect = destroyEffects[randIndex];                                                      // choose GameObject with randIndex from array
            GameObject newEffect = Instantiate(randEffect, transform.position, transform.rotation) as GameObject;
            newEffect.transform.SetParent(GameMaster.instance.hierarchyGuard);
        }
        //else
        //Debug.Log("There is no destroy effect attached to " + gameObject.name);

        if (destroySounds)                             // if there is any sounds
            AudioSource.PlayClipAtPoint(destroySounds, transform.position, volume);

        ObjectController controller = other.GetComponent<ObjectController>();    // get reference to object controller


        if (controller)                          // if there is a script attached  to other
        {
            controller.TakeDamage(damage, transform.position);

            if (disableControl)
                controller.StartCoroutine("DisableShotAndMove", howLong);
        }

        //else
        // Debug.Log(other.name + " hasn't ObjectController script!");
        //if (other.GetComponent<ObjectController>())            // if it exist
        //    other.GetComponent<ObjectController>().TakeDamage(damage * GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().weaponLevel);
        //else
        //Debug.Log("There is no destroy sound attached to " + gameObject.name);

        //print("Zniszczony " + gameObject.name);
        Destroy(gameObject);
    }

}   // Karol Sobanski