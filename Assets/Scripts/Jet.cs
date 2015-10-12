using UnityEngine;
using System.Collections;

public class Jet : EnemyController
{

    protected override void Update()
    {
        if (isShooting)
            for (int i = 0; i < visualWeapons.Count; i++)             // Shot from all weapons
                visualWeapons[i].weaponScripts.Shot(true);      // parented bullets

        if (isMoving)
            Move();
    }
}
