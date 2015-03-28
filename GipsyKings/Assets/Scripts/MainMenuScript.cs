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

		Application.LoadLevel("Level1");

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
