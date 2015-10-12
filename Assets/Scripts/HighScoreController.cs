using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour {
	
	public static HighScoreController highScoreInstance;
	public Text[] textScoreGroup;


	private List<int> highScore;
	private int scoreToUpdate;	
	// Use this for initialization
	void Awake () 
	{
		highScoreInstance = GetComponent<HighScoreController> ();
		highScore = new List<int>();
		AssignTextFields (10);
		//SetExemplaryScore ();
		//SaveScore ();
		highScore.Clear ();
		//PlayerPrefs.DeleteAll ();
		LoadHighScores ();
		SetTop (10);
		DontDestroyOnLoad (gameObject);
		//DebugScores ();
		
		
		Debug.Log ("Start of HighScoreController");
	}

	void Start()
	{
		if (highScoreInstance == null)
			highScoreInstance = this;
		else if (highScoreInstance != this)
			Destroy(gameObject);
	}
	
	void SetExemplaryScore()
	{
		UpdateScore (200);
		UpdateScore (20213);
		UpdateScore (200123);
		UpdateScore (55555);
		UpdateScore (212300);
		//UpdateScore (3, SearchForIndex(3));
	}
	
	public void LoadHighScores()									//Loads highScore from playerPrefs to list
	{
		int tempScore = 0;
		int counter = 0;
		highScore.Clear ();
		while( ((tempScore = PlayerPrefs.GetInt(""+counter))!= 0 ))			//continue when have the key and not reached less than five positions
		{
			highScore.Add (tempScore);
			counter++;
		}
	}
	
	public void UpdateScore(int score)
	{
		Debug.Log ("Adding score: " + score);
		SetScoreToUpdate (score);
		int key = SearchForIndex(score);
		highScore.Insert (key, score);
		SaveScore ();
		
	}
	
	public int SearchForIndex(int score)
	{
		for (int i=0;i<highScore.Count;i++) 
		{
			if(score > highScore[i])
			{
				return i;
			}
		}
		return highScore.Count;
	}
	
	public void SaveScore()
	{
		for (int i = 0; i < highScore.Count; i++) {
			PlayerPrefs.SetInt ("" + i, highScore [i]);
		}
		PlayerPrefs.Save ();
	}
	
	public void SetTop(int numberOfScores)
	{
		int counter = 0;
		while (highScore[counter]!=null && counter < numberOfScores && counter < highScore.Count) 
		{
			//Debug.Log("Counter: "+counter+" Assigning "+textScoreGroup[counter]+" Value: "+highScore[counter]);
			textScoreGroup[counter].text = highScore[counter].ToString();
			counter++;
		}
	}
	
	public void SetScoreToUpdate(int score)
	{
		scoreToUpdate = score;
	}
	
	void OnLevelWasLoaded(int level)
	{
		//Debug.Log("Level " + level + " loaded");
		if (level == 0) {
			LoadSettings(10);
		}
	}

	public void LoadSettings(int numberOfScores)
	{
		AssignTextFields(numberOfScores);
		LoadHighScores();
		SetTop(numberOfScores);
		DebugScores();
	}

	public int getScoreToUpdate()
	{
		return scoreToUpdate;
	}
	
	public void AssignTextFields(int numberOfScores)
	{
		textScoreGroup = new Text[numberOfScores];
		Transform scores = GameObject.Find ("Scores").transform;
		for (int i=0; i < scores.childCount; i++) 
		{
			textScoreGroup[i] = scores.GetChild(i).GetComponent<Text>();
		}
	}

	public bool NewHighScoreObtained(int score)
	{
		int key = SearchForIndex(score);
		if (key < 5)
			return true;
		else
			return false;
	}

	public void DebugScores()
	{
		int i = 0;
		foreach (int k in highScore) 
		{
			Debug.Log ("index: " + (i++)+"k: "+k);
		}
	}

	public void ResetHighScores(int numberOfScores)
	{
		PlayerPrefs.DeleteAll ();
		highScore.Clear ();
		HighScoreController.highScoreInstance.SetTop (numberOfScores);
	}
	
}
