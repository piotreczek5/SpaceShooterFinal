using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerController : ObjectController
{
    public Transform weaponSpawnMain;                       // reference to main weapon spawn transform
    public Transform weaponSpawnLeft;
    public Transform weaponSpawnRight;

    [Header("Player Statistics")]
    public int ammunition = 100;                            // how many ammunition player have   
    public float tiltSpeed = 5f;                            // how fast wings'll be rotate
    public float sight = 20f;                               // how far player can display enemy health bar                    
    public float changeColor = 3f;                          // time to change color after take damage
    public float timeToRestart = 1f;                        // time to restart game after player's dead

    [Header("Fuel")]
    public int maxFuel = 100;                               // how many fuel player has
    [Tooltip("when player fuel will over, we change his movement speed by divide a factor")]
    public float factorMoveWithoutFuel = 3;
    [Tooltip("How fast player change his velocity from normal value to value without fuel")]
    public float speedFallDownFuel = 3f;

    [Header("Audio")]
    public AudioClip emptyMagazineSnd;                      // if player doesn't have ammo play empty magazine sound;
    public AudioClip pickUpSnd;                             // sound will play when player pick up some item
    public AudioClip wingsTilt;

    [Space(10)]
    private float currentFuel;
    private bool isFuelOver;                                // flag which inform if there is any fuel
    private bool isAmmoOver;                                // flag which inform if there is any ammo
    private Ray ray;                                        // ray from player to forward
    private int enemyLayer;                                 // player shoots ray and check if its enemy layer
    private Image enemyHealthBar;                           // it is visual enemy health bar on screen
    private VisualBar fuelBar;                              // it is visual bar script, that control color of fuel bar on screen
    private Text ammunitionText;                            // ammount of ammunition display on screen
    private BombController bombController;                  // controlls all bomb operations
    private Image damageImage;                              // damage image will show on screen when player take damage
    private Color damageColor = new Color(255, 0, 0, 0.2f); // what color screen will have when player'll take damage

    //private float orignalHorizontalMove;                    // save original player horizontal movement,
    //private float originalVerticalMove;                     // save original player vertical   movement

    private const string namePlayerHealth = "PlayerHealth";
    private const string namePlayerFuel = "PlayerFuel";



    void Awake()
    {
        AdjustVisualBars();               // this must be in Awake because other script ShieldController need reference to VisualBar
    }


    protected override void Start()
    {
        base.Start();

        bombController = GetComponent<BombController>();
        damageImage = GameObject.Find("DamageImage").GetComponent<Image>();
        enemyHealthBar = GameObject.Find("EnemyHealth").GetComponent<Image>();
        ammunitionText = GameObject.Find("Ammunition").GetComponent<Text>();
        ammunitionText.text = "Ammo: " + ammunition;

        enemyLayer = LayerMask.GetMask("Enemy");                   // Get enemy mask
        currentFuel = maxFuel;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            bombController.CreateBoombs();

        IsEnemyOnSight();                                          // if player can see enemy, display his health bar
        CheckBoundry();                                            // check if player try leave mthe map

        if (isShooting)                                            // if player is able to shot
            Shot();                                                // shot

        if (isMoving) Move();                                      // if player is able to move, move his ship
    }


    public void Refuel(float newFuel)                              // pick up fuel tank has access 
    {
        currentFuel += newFuel;                                    // add new fuel

        if (currentFuel > maxFuel)                                 // if current fuel is greatest than max
            currentFuel = maxFuel;                                 // set current value to max

        if (isFuelOver)
        {
            isFuelOver = false;                                    // if fuel over was true, now ship can fly

           float newHorizontal = horizontalMove *factorMoveWithoutFuel;               // change his horizontal movement speed to normal after refuel
           float newVertical = verticalMove * factorMoveWithoutFuel;                  // change his vertical movement speed to normal after refuel

            StartCoroutine(LerpMovementChange(newHorizontal, newVertical));           // use Lerp to change velocity       
        }
    }


    public void IncreaseHealth(int health)                                               // pick Ups increase Health has access
    {
        currentHealth += health;

        if (currentHealth > maxHealth)                                                   // if current  health is greatest that max
            currentHealth = maxHealth;                                                   // set current helath to max    value

        healthBar.UpdateBar(currentHealth, maxHealth);                                   // update health bar on screen
    }


    public override void TakeDamage(int damage, Vector3 damagePosition)
    {
        if (isShieldActive) return;                                                     // if shield is active don't take damage to player

        ExplosionController.instance.RandomExplosionEffect(damagePosition);
        currentHealth -= damage;
        StartCoroutine(DamageScreen());                                                 // show red screen

        if (currentHealth <= 0 && isAlive)                                              // if player haven't health but he isn't dead
            StartCoroutine(Death());                                                    // kill the player

        CameraShake.instance.DamageShake();                                             // shake camera effect on screen
        healthBar.UpdateBar(currentHealth, maxHealth);                                  // update healthBar 
    }


    public void AddAmmo(int ammo)
    {
        ammunition += ammo;                                 // add amunition
        ammunitionText.text = "Ammo: " + ammunition;        // update text on GUI

        if (ammunitionText.color != Color.white)                                 // if ammo color is different than white
            ammunitionText.color = Color.white;                                        // set Color to white

        isAmmoOver = false;                                 // change flag
    }


    public void ActiveShield(bool active)                   // ShieldController has access to this method
    {
        isShieldActive = active;
        healthBar.BlueIcone(active);                        // if shiled is active, heart icon will  change his color to blue
    }


    public void ChangeWeapons(ref GameObject newWeapon)
    {
        if (visualWeapons.Count == 1 && visualWeapons[0].weapon != newWeapon)                        // change one weapon to another if player have different that he found 
        {
            Destroy(weaponSpawnMain.GetChild(0).gameObject);                                         // destroy current weapon on main spawner
            visualWeapons.Clear();                                                                   // cleaer list 
            visualWeapons.Add(new VisualWeapon(newWeapon, weaponSpawnMain));                         // use constructor  to create new visual Weapon
            visualWeapons[0].weaponScripts = WeaponCreator(ref newWeapon, ref weaponSpawnMain);      // get reference to new  created weapon  script
        }
        else if (visualWeapons.Count == 1 && visualWeapons[0].weapon == newWeapon)                   // add second weapon if player have the same that he found
        {
            Destroy(weaponSpawnMain.GetChild(0).gameObject);                                         // destroy current weapon on main spawner
            visualWeapons.Clear();                                                                   // cleaer list 
            visualWeapons.Add(new VisualWeapon(newWeapon, weaponSpawnRight));
            visualWeapons[0].weaponScripts = WeaponCreator(ref newWeapon, ref weaponSpawnLeft);      // get reference to new  created weapon  script

            visualWeapons.Add(new VisualWeapon(newWeapon, weaponSpawnLeft));
            visualWeapons[1].weaponScripts = WeaponCreator(ref newWeapon, ref weaponSpawnRight);     // get reference to new  created weapon  script
        }
        else if (visualWeapons.Count == 2 && visualWeapons[0].weapon != newWeapon)
        {
            Destroy(weaponSpawnLeft.GetChild(0).gameObject);                                         // destroy current weapon on LeftSpawner
            Destroy(weaponSpawnRight.GetChild(0).gameObject);                                        // destroy current weapon on RightSpawner
            visualWeapons.Clear();                                                                   // cleaer list 
            visualWeapons.Add(new VisualWeapon(newWeapon, weaponSpawnRight));
            visualWeapons[0].weaponScripts = WeaponCreator(ref newWeapon, ref weaponSpawnLeft);      // get reference to new  created weapon  script

            visualWeapons.Add(new VisualWeapon(newWeapon, weaponSpawnLeft));
            visualWeapons[1].weaponScripts = WeaponCreator(ref newWeapon, ref weaponSpawnRight);     // get reference to new  created weapon  script
        }
    }


	protected override void Move()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		
		if (x == 0 && y == 0)                                                         // if player move
			SoundManager.instance.RandomizeSfx(ref wingsTilt, ref audioSource);       // play ship sound
		
		Vector3 movement = new Vector3(x * horizontalMove, 0, y * verticalMove);
		
		if (!isFuelOver)
			CheckFuel();                                                                 // check if there is any fuel in  fuel tank
		
		if (Time.timeScale != 0)
			rigidbody.velocity = movement / Time.timeScale;                           // it's ignore bulelt Time effect
		
		ChangeShipRotation();                                                         // Tilt wings
	}






    protected override void CheckBoundry()                                            // Check if player try leave the boundy
    {
        rigidbody.position = new Vector3(                                             // set his position in boundry
        Mathf.Clamp(rigidbody.position.x, -boundryPosition.x, boundryPosition.x),
        rigidbody.transform.position.y,
        Mathf.Clamp(rigidbody.position.z, 3, boundryPosition.z));
    }


    void ChangeShipRotation()
    {
        float TiltDeegres = tiltSpeed * -rigidbody.velocity.x;                                        // how far ship's wings can tilt

        if (Time.timeScale == 1)                                                                      // if there is no bullet time
            rigidbody.rotation = Quaternion.Euler(new Vector3(0, 0, TiltDeegres));
        else
            rigidbody.rotation = Quaternion.Euler(new Vector3(0, 0, TiltDeegres * Time.timeScale));   // if there is bullet time, we change player velocity so we need to reduce his rotation by timeScale
    }


    void AdjustVisualBars()                                                                          // Get references to GUI  bars after start game
    {
        healthBar = GameObject.Find(namePlayerHealth).GetComponent<VisualBar>();
        fuelBar = GameObject.Find(namePlayerFuel).GetComponent<VisualBar>();
    }


    void IsEnemyOnSight()                                                                            // function display health bar enemy, if player can see him on forward
    {
        ray.origin = transform.position;                                                             // set ray from player position
        ray.direction = transform.forward;                                                           // to forward direction
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, sight, enemyLayer))                                 // if ray hit enemy
        {

            raycastHit.transform.GetComponent<EnemyController>().DisplayHealthBar();
        }
        else
            enemyHealthBar.color = new Color(0, 0, 0, 0);                                            // bar will be unvisible
    }


    void CheckFuel()
    {
        currentFuel -= Time.deltaTime;                                              // decrease fuel
        fuelBar.UpdateBar(currentFuel, maxFuel);                                    // update visual fuel bar on screen

        if (currentFuel < 0)                                                        // if fuel is over 
        {
            isFuelOver = true;                                                      // set flag fuel is over

            float noFuelHorizontal = horizontalMove / factorMoveWithoutFuel;        // set noFuel horizontal speed
            float noFuelVertical = verticalMove / factorMoveWithoutFuel;            // set noFuel vertical speed

            StartCoroutine(LerpMovementChange(noFuelHorizontal, noFuelVertical));   // decrease player speed to speed without fuel
        }
    }



    IEnumerator LerpMovementChange(float newHorizontal, float newVertical)                  // change player ship movement  from one value to another by using lerp
    {
        while (!Mathf.Approximately(horizontalMove, newHorizontal))                                // if  velocities aren't the same
        {
            print(horizontalMove);
            horizontalMove = Mathf.Lerp(horizontalMove, newHorizontal, speedFallDownFuel * Time.deltaTime);
            verticalMove = Mathf.Lerp(verticalMove, newVertical, speedFallDownFuel * Time.deltaTime);
            yield return null;
        }
        print("AAAAAAAAAA");
    }


    IEnumerator DamageScreen()
    {
        damageImage.color = damageColor;                                                                        // change screen color to red
        yield return null;

        while (damageImage.color != Color.clear)
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, changeColor * Time.deltaTime);       // change color from red to clear
            yield return null;
        }
    }


    IEnumerator Death()
    {
        isAlive = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;                           // disenabled collider
        transform.FindChild("Model").transform.gameObject.SetActive(false);                  // hide player model;

        Instantiate(destroyExplosion, transform.position, Quaternion.identity);
        GameMaster.instance.DescreaseLifes();

        AudioSource.PlayClipAtPoint(destroySound, transform.position, 0.7f);                 // create destroy ship sound

        if (GameMaster.instance.lifes < 1)
            GameMaster.instance.GameOver();
        else
            GameMaster.instance.PlayerDeath();

        yield return new WaitForSeconds(timeToRestart);
        Destroy(gameObject);
    }


    void Shot()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !isAmmoOver)                                            // if player press fire button and he have ammo
        {
            CameraShake.instance.ShotShake();                                                       // shake camera when player shot

            for (int i = 0; i < visualWeapons.Count; i++)                                           // Shot from all weapons
                if (visualWeapons[i].weaponScripts.Shot(false))                                     // if gun can fire because time to shot next bullet is left
                    DecreaseAmmunition();                                                           // decrease ammunition

        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))                                                  // else if  player press fire button and he haven't ammo
            SoundManager.instance.RandomizeSfx(ref emptyMagazineSnd, ref audioSource);              // play sound empty magazine
    }


    void DecreaseAmmunition()
    {
        if (ammunition > 0)
        {
            ammunition--;                                                                  // decrease ammunition amount
            ammunitionText.text = "Ammo: " + ammunition;                                   // Update GUI text

            if (ammunition < 10)                                                           // if ammunition is less than 10 bullets
                ammunitionText.color = Color.red;                                          // change ammo label to red Color
        }
        else
            isAmmoOver = true;                                                             // there is no ammo in magazine so player can't shot
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("PickUp"))
            SoundManager.instance.RandomizeSfx(ref pickUpSnd, ref audioSource);            // set pickup sound
    }

}   // Karol Sobański