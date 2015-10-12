using UnityEngine;
using System.Collections;


public class Bee : EnemyController
{
    [Header("AI Enemy")]
    [Tooltip("random times to change direction frome one to other")]
    public int minDirectionTime = 1;
    public int maxDirectionTime = 3;

    private float directionTimeLeft;            // time left to change direction
    private float timeLeftToPlayAnimation;



    protected override void Move()
    {
        base.Move();

        ChangeDirectionX();
        CheckBoundry();
    }


    void ChangeDirectionX()
    {
        directionTimeLeft -= Time.deltaTime;
        if (directionTimeLeft > 0) return;

        float randomTime = Random.Range(minDirectionTime, maxDirectionTime);        // set new Time to change direction
        directionTimeLeft = randomTime;
        horizontalMove = -horizontalMove;                                           // change direction on X axis                     
    }

}   // Karol Sobanski
