using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

		// initial states
		DidChangeToggle(false);
		DidChangeMusicToggle(true);
		DidChangeSoundToggle(true);

		// remove score keys
		PlayerPrefs.SetInt("scorePlayer1", 0);
		PlayerPrefs.SetInt("scorePlayer2", 0);
		PlayerPrefs.Save();

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void DidPressLevelButton(GameObject obj)
	{

		if (obj.name == "Exit")
		{
			Application.Quit();
		}
		else
		{
			Application.LoadLevel(obj.name);
		}
	}

	void SetBoolStatusForKey(string key, bool status)
	{

		PlayerPrefs.SetInt(key, (status == true) ? 1 : 0);
		PlayerPrefs.Save();

	}

	public void DidChangeToggle(bool status)
	{

		SetBoolStatusForKey("instantPickupOn", status);

	}

	public void DidChangeMusicToggle(bool status)
	{

		SetBoolStatusForKey("musicOn", status);

	}

	public void DidChangeSoundToggle(bool status)
	{

		SetBoolStatusForKey("soundOn", status);

	}

}
