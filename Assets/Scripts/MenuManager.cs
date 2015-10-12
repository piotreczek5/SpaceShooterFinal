using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public Menu currentMenu;
	public AudioClip pressedSound;
	public AudioClip hoveredSound;


	public void Start()
	{
		ShowMenu (currentMenu);
	}

	public void ShowMenu(Menu menu)
	{
		if (currentMenu == null)
			currentMenu.isOpen = false;

		currentMenu = menu;
		currentMenu.isOpen = true;
	}

	public void ResetHighScores()
	{
		HighScoreController.highScoreInstance.ResetHighScores (10);
	}
	public void CloseMenu()
	{
		currentMenu.isOpen = false;
	}
}
