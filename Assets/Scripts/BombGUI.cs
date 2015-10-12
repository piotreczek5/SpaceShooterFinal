using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BombGUI : MonoBehaviour
{
    private Animator animator;
    private const string bombUsed = "bombUsed";
    private const string bombCollected = "bombCollected";
    private Image bombImage;


    void Awake()
    {
        bombImage = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }


    public void BombCollected()
    {
        animator.SetTrigger(bombCollected);
    }


    public void BombUsed()
    {
        animator.SetTrigger(bombUsed);
    }


    void HideImage()                 // Animation has access to this method
    {
        bombImage.enabled = false;
    }


    void ShowImage()                 // Animation has access to this method
    {
        bombImage.enabled = true;
    }
}   // Karol Sobanski
