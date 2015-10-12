using UnityEngine;
using System.Collections;



public class ShieldController : MonoBehaviour
{
    public GameObject shieldInstance;              // shield to instance
    public float maxShieldTime;                    // how long shiled will be active

    private float currentShieldTime;               // how many times left to deactivation shield
    private GameObject playerShield;               // instance of GameObject shield
    private VisualBar shieldBar;


    void Start()
    {
        shieldBar = GameObject.Find("ShieldBar").GetComponent<VisualBar>();
        CreateShield();
        ActiveShield(2);
    }


    public void ActiveShield(int howLong)
    {
        currentShieldTime = howLong;                           // set current shield time
        maxShieldTime = howLong;                               // set max shield time
        playerShield.SetActive(true);                          // turn on shield
        shieldBar.ParentandDisplayBar(transform);              // display and parent shield bar to gameobject
        StartCoroutine(DecreaseShieldTime());                  // decrease shield time
        GetComponent<PlayerController>().ActiveShield(true);   // active shield in PlayerController script, that he will resistans by damage
    }


    IEnumerator DecreaseShieldTime()
    {
        while (currentShieldTime > 0)
        {
            shieldBar.UpdateBar(currentShieldTime, maxShieldTime);              // update bar's appierrience
            currentShieldTime -= Time.deltaTime;                                // decrease shield's  value
            yield return null;                                                  // wait frame
        }

        shieldBar.HideBar();                                                    // Hide bar when his value == 0
        GetComponent<PlayerController>().ActiveShield(false);                   // disactive shield in PlayerController script, that he will susceptible by damage
        playerShield.SetActive(false);                                          // turn off shield if time left;
    }


    void CreateShield()
    {
        playerShield = Instantiate(shieldInstance, transform.position, Quaternion.identity) as GameObject;      // create player shield gameobject
        playerShield.transform.SetParent(transform);                                                            // parent to Player
        playerShield.SetActive(false);                                                                          // disable shield
    }

}   // Karol Sobanski
