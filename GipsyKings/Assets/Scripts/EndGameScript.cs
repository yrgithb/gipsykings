using UnityEngine;
using System.Collections;

public class EndGameScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetButtonDown("Submit") == true) {
			Application.LoadLevel(Application.loadedLevel);
		}

	}
}
