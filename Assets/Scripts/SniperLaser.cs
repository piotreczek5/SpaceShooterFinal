using UnityEngine;
using System.Collections;

public class SniperLaser : MonoBehaviour
{
    [Tooltip("distance where gun can fire")]
    public float range;
    public GameObject laserPoint;    

    private Ray shootRay;
    private LineRenderer gunLine;
    private RaycastHit shootHit;
    private int shotableMask;



    void Start()
    {
        gunLine = GetComponent<LineRenderer>();
        shotableMask = LayerMask.GetMask("Enemy");    // Get reference to enemy layer mask

        gunLine.enabled = true;                        
        gunLine.SetPosition(0, transform.position);
    }


    void Update()
    {
        gunLine.SetPosition(0, transform.position);
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shotableMask))         // if raycast shoot eobject on enemy layer in range
        {
            gunLine.SetPosition(1, shootHit.point);                               // set the second position of the line renderer to the point the raycast hit
            GameObject newPoint = Instantiate(laserPoint, shootHit.point, transform.rotation) as GameObject;
            newPoint.transform.SetParent(GameMaster.instance.hierarchyGuard);
        }
        else
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);  // set the  second position of the line renderer to the fullest extent of the gun's range
    }
}   // Karol Sobanski
