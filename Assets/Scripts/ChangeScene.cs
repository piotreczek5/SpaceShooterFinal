using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	// Use this for initialization

	public void changeSceneTo(int noOfScene)
	{
		StartCoroutine(switchWithDelay(noOfScene,2));
	}

	IEnumerator switchWithDelay(int noOfScene,int delay)
	{
		yield return new WaitForSeconds (delay);
		Application.LoadLevel (noOfScene);
	}
	public void Exit()
	{
		Application.Quit ();
	}
}
