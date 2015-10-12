using UnityEngine;
using System.Collections;

public class PickUpsController : MonoBehaviour
{
    public int value;
    public float volume = 0.15f;
    public AudioClip pickUpSnd;

    public bool recoveryPackage;
    public bool fuel;
    public bool ammo;
    public bool bomb;
    public bool shield;
    public bool star;
    public bool bulletTime;

    private Animator animator;
    private const string collected = "collected";



    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();          // Get reference to player controller

            if (recoveryPackage)
                playerController.IncreaseHealth(value);
            else if (fuel)
                playerController.Refuel(value);
            else if (ammo)
                playerController.AddAmmo(value);
            else if (bomb)
                other.GetComponent<BombController>().AddBomb(value);
            else if (shield)
                other.GetComponent<ShieldController>().ActiveShield(value);                      // get refenence to shield Controller and active player shield
            else if (star)
                GameMaster.instance.IncreaseScore(value);                                        // increase player score
            else if (bulletTime)
                GameMaster.instance.BulletTimeOn();                                              // change timeScale to achive bullettime effect

            if (pickUpSnd)                                                                       // if there is sound attached
                AudioSource.PlayClipAtPoint(pickUpSnd, transform.position, volume);

            if (animator)                                                                         // if there is Animator attached
                animator.SetTrigger(collected);
            else
                Destroy(gameObject);               
        }
    }


    void DestroyPickUp()                                                                 // Animation will use this method after play collected animation
    {
        Destroy(gameObject);
    }

}   // Karol Sobanski
