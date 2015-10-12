using UnityEngine;
using System.Collections;



public class SlowWeapon : Weapon
{
    public GameObject bullet;                                                             // bullet tkat takes damage to object

    public override bool Shot(bool controllShot)
    {
        if (timeToShot > timeBetweenBullets && Time.timeScale != 0)                       // if player press button, time to next shot left, pause is off)
        {
            timeToShot = 0f;                                                              // reset time

            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;

            if (controllShot)
                newBullet.transform.SetParent(gameObject.transform);                      //  bullet rotate with ship   
            else
                newBullet.transform.SetParent(GameMaster.instance.hierarchyGuard);

            ShotEffects();                                                                // create some effect  sound particle

            return true;                                                                  // weapon can shot because  time to next bullet left
        }
        return false;                                                                     // weapon can't shot
    }
}   // Karol Sobanski
