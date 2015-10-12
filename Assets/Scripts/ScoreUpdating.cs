using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUpdating : MonoBehaviour {

	int score;
	// Use this for initialization
	void Start () {
		score = GameMaster.instance.score;
		Debug.Log ("Updating score! ");
		HighScoreController.highScoreInstance.UpdateScore (GameMaster.instance.score);
		HighScoreController.highScoreInstance.LoadSettings (10);

		if (HighScoreController.highScoreInstance.NewHighScoreObtained (score))
			GameObject.Find ("ScoreText").GetComponent<Text> ().text = "Congratulations!\n New High Score!\n Score:"+ score;
		else
			GameObject.Find ("ScoreText").GetComponent<Text> ().text = "Score: " + score;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
