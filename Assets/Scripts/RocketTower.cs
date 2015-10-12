using UnityEngine;
using System.Collections;

public class RocketTower : EnemyController
{

    private Weapon weapon;


    protected override void Start()
    {
        base.Start();
        weapon = GetComponent<Weapon>();
    }


    protected override void Update()
    {
        if (isShooting)
            weapon.Shot(false);
    }
    

    protected override void Death()
    {
        Destroy(gameObject.transform.parent.gameObject);                         // We need destroy all object in this case
    }
}   // Karol Sobanski
