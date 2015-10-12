using UnityEngine;
using System.Collections;

public class GunPickUp : MonoBehaviour {

	public GameObject gunReference;
    public int additionalAmmo;



	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{ 
            PlayerController playerController = other.GetComponent<PlayerController>();        // get reference to player script
            playerController.ChangeWeapons(ref gunReference);                                  // change player weapon 
            playerController.AddAmmo(additionalAmmo);                                          // add aditional ammo

			Destroy(gameObject);
		}
	}
}   // Piotr Pusz, edited by Karol Sobański
