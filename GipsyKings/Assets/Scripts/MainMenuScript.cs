using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
	
		// initial state of instant pickup: off
		DidChangeToggle(false);

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void DidPressLevelButton(GameObject obj)
	{

		print(obj.name);
		if (obj.name == "L1")
		{

		}
		else if (obj.name == "L2")
		{

		} 
		else if (obj.name == "L3")
		{

		}
		else if (obj.name == "Exit")
		{
			Application.Quit();
		}

		Application.LoadLevel("GameScene");

	}

	public void DidChangeToggle(bool status)
	{

		PlayerPrefs.SetInt("instantPickupOn", (status == true) ? 1 : 0);
		PlayerPrefs.Save();

	}

}
