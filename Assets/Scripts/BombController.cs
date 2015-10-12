using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BombController : MonoBehaviour
{

    public GameObject bomb;

    private int maxBombs = 3;
    private int currentBombs = 1;                  // current bomb, that player have after start a game
    private BombGUI[] bombsGUIController;          // boombs controller on GUI controls  image display and animations



    void Start()
    {
        bombsGUIController = new BombGUI[maxBombs];
        FindBombImages();

        for (int i = 0; i < currentBombs; i++)
            bombsGUIController[i].BombCollected();           // Display bombs on gui after start agame   
    }


    void FindBombImages()
    {
        for (int i = 0; i < maxBombs; i++)
            bombsGUIController[i] = GameObject.Find("Bomb" + i).GetComponent<BombGUI>();
    }


    public void CreateBoombs()
    {
        if (currentBombs > 0)          // if player have any boomb
        {
            GameObject newBomb = Instantiate(bomb, transform.position, Quaternion.identity) as GameObject;    // Create bomb on map
            newBomb.transform.SetParent(GameMaster.instance.hierarchyGuard);                                  // parent do guardhierarchy
            bombsGUIController[currentBombs - 1].BombUsed();                                                  // create bomb on GUI
            currentBombs--;
        }
    }

    public void AddBomb(int newBomb)
    {
        currentBombs += newBomb;

        if (currentBombs > maxBombs)                            // check if player has more bomb that he should
            currentBombs = maxBombs;

        bombsGUIController[currentBombs - 1].BombCollected();      // show bomb on screen
    }
}   // Karol Sobanski
