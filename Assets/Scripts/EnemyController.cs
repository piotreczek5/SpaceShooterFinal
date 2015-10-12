using UnityEngine;
using System.Collections;


public class EnemyController : ObjectController
{
    public int pointForKill;                          // points for player
    [HideInInspector]
    public Material white;                           

    [Tooltip("How many frames object will change his material to white after taking damage")]
    private float whiteFrames = 2;
    private MeshRenderer[] meshRenderers;             // all renderer in model will be change to white color after taking damage
    private Material[] orginalMaterials;


    
    protected override void Start()
    {
        base.Start();

        healthBar = GameObject.Find("EnemyHealth").GetComponent<VisualBar>();
        SetMaterialArrays();
    }


    protected virtual void Update()
    {
        if (isShooting)
            Shot();

        if (isMoving)
            Move();
    }


    public void DisplayHealthBar()                                         // Player has access to this method,  if he see anamy he will invoke method
    {
        healthBar.DisplayBar(transform);                                   // Display visual health bar on screen
        healthBar.UpdateBar(currentHealth, maxHealth);                     // Update health bar values
    }


    public override void TakeDamage(int damage, Vector3 damagePosition)            // Player has access to this method, 
    {
        if (!isAlive) return;                                                      // if enemy is killed  do nothing

        StartCoroutine(SwitchMaterial());                                          // switch material for while to white
        ExplosionController.instance.RandomExplosionEffect(damagePosition);
        currentHealth -= damage;
        healthBar.UpdateBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            DestroyEffect();
            Death();
        }
    }


    protected override void DestroyEffect()
    {
        base.DestroyEffect();

        isAlive = false;
        GameMaster.instance.IncreaseScore(pointForKill);                       // points for player 
        GameMaster.instance.DropRandomItem(transform.position);                // choose random item to drop
        Death();
    }


    protected virtual void Death()                                             // virtual because sometimes we will destroy parent (RocketTower)
    {
        Destroy(gameObject);
    }


    protected override void CheckBoundry()                       // Enemy Boundry
    {
        rigidbody.position = new Vector3(
        Mathf.Clamp(rigidbody.position.x, -boundryPosition.x, boundryPosition.x),
        rigidbody.transform.position.y,
        rigidbody.transform.position.z);
    }


    protected virtual void Shot()
    {
        for (int i = 0; i < visualWeapons.Count; i++)      // Shot from all weapons
            visualWeapons[i].weaponScripts.Shot(false);
    }


    /*private bool IsPlayerAlive()
    {
        if (GameObject.FindGameObjectWithTag ("Player") != null)
            return true;
    }*/


    void SetMaterialArrays()
    {
        meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();       // get all mesh renderes in objects

        int size = meshRenderers.Length;
        orginalMaterials = new Material[size];                                    // set array size

        for (int i = 0; i < size; i++)
            orginalMaterials[i] = meshRenderers[i].material;
    }


    IEnumerator SwitchMaterial()
    {
        for (int i = 0; i < meshRenderers.Length; i++)
            meshRenderers[i].material = white;

        for (int i = 0; i < whiteFrames; i++)                                     // wait whiteFrames before change material again
            yield return null;
        
        for (int i = 0; i < meshRenderers.Length; i++)
            meshRenderers[i].material = orginalMaterials[i];
    }

}   // Karol Sobanski


