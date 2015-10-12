using UnityEngine;
using System.Collections;

public class AdjustScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.SetResolution (400, 600, false);
		Camera.main.aspect = 320f / 480f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
