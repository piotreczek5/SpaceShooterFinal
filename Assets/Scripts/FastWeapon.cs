using UnityEngine;
using System.Collections;

public class FastWeapon : Weapon
{
    [Tooltip("distance where gun can fire")]
    public GameObject destroyEffect;
    public int damage = 10; 									    // This one has to have it beacuse it does not contain bullets

    private float effectDisplayTime = 0.2f;                         // the proportion of the timeBetweenBullets that the effects will display for
    private int shootableMask;                                      // A layer mask so the  raycast only hits things on the shootable layer
    private Ray shootRay;                                           // A ray from the gun end forwards
    private RaycastHit shootHit;                                    // A raycast hit to get information about what was hit
    private LineRenderer gunLine;
    private float range = 130;                                      // distance where  gun can fire



    protected override void Start()
    {
        base.Start();

        shootableMask = LayerMask.GetMask("Enemy");
        gunLine = GetComponent<LineRenderer>();
    }


    protected override void Update()
    {
        base.Update();

        if (timeToShot >= timeBetweenBullets * effectDisplayTime)
            DisableEffects();
    }


    void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }



    public override bool Shot(bool controllShot)
    {
        if (timeToShot > timeBetweenBullets && Time.timeScale != 0)    // if player press button, time to next shot left, pause is off
        {
            timeToShot = 0f;

            ShotEffects();

            gunLine.enabled = true;                       // enable the line renderer
            gunLine.SetPosition(0, transform.position);   // set it's first position to be the end of the gun;

            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                ObjectController controller = shootHit.transform.GetComponent<ObjectController>();

                if (controller)
                    controller.TakeDamage(damage, shootHit.point);

                Instantiate(destroyEffect, shootHit.point, destroyEffect.transform.rotation);
                gunLine.SetPosition(1, shootHit.point);                                                // set the second position of the line renderer to the point the raycast hit
            }
            else
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);                  // set the  second position of the line renderer to the fullest extent of the gun's range

            return true;                                                                               // weapon can shot
        }
        return false;                                                                                  // weapon can't shot because time to shot do not paas
    }
}   // Karol Sobanski
