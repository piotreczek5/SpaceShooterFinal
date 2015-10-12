using UnityEngine;
using System.Collections;



public class BulletTime : MonoBehaviour                                      // Slow motion effects
{
    [Tooltip("How long bullet time will be turn on")]
    public float bulletTime = 2f;

    [Tooltip("How strong Time will be slower")]
    [Range(0,1)]
    public float timeScale = 0.3f;



    public IEnumerator TurnOnBulletTime()                                             // PickUpController has access to this method
    {
        Time.timeScale = timeScale;                                                   // change  timeScale to new lower timeScale
        Time.fixedDeltaTime = 0.02f * Time.timeScale;                                 // If you lower timeScale it is recommended to also lower Time.fixedDeltaTime by the same amount.

        yield return new WaitForSeconds(bulletTime);

        Time.timeScale = 1;                                                           // change timeScale to default
        Time.fixedDeltaTime = 0.02f * Time.timeScale;                                 // change fixxedDeltaTime to default

    }

}   // Karol Sobanski   

