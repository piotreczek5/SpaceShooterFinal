using UnityEngine;
using System.Collections;

public class WeaponMultiplierBonus : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponentInChildren<PlayerController>().weaponLevel++;
            Destroy(gameObject);
        }
    }
}
