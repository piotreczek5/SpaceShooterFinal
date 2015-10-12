using UnityEngine;
using System.Collections;

public class Mine : EnemyController
{
    public float detectRange;                   // how far mine can detects player

    private int detectLayer;                    // detect only layer "Player"
    private Animator animator;
    private const string explose = "explose";
    private BoxCollider[] minepartsColliders;           // array keeps references to all mine parts box colliders. 


    protected override void Start()
    {
        base.Start();

        minepartsColliders = GetComponentsInChildren<BoxCollider>();    // get references to all DestroyByCollision scripts in children

        foreach (BoxCollider collider in minepartsColliders)                 // disable all boxColliders, it prevents destroy parts where mine is complite. Will be active again when mine will explose
        {
            collider.enabled = false;
        }

        detectLayer = LayerMask.GetMask("Player");                      // get player layer to detect him in range
        animator = GetComponent<Animator>();
    }


    protected override void Update()
    {
        base.Update();

        if (isAlive)
            Detector();
    }


    void Detector()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRange, detectLayer);

        if (colliders.Length > 0)
        {
            print(colliders[0].name);
            base.DestroyEffect();
            Death();
        }
    }


    protected override void Death()
    {
        animator.SetTrigger(explose);
        foreach (BoxCollider collider in minepartsColliders)                 // disable all DestroyByCollision, it prevents destroy parts where mine is complite. Will be active again when mine will explose
            collider.enabled = true;
    }


    void Destroy()                 //  animation "explose" will call this method after finish;
    {
        Destroy(gameObject);
    }

}   // Karol Sobanski


