using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InstantPickupToggleScript : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		LoadInstantStatus();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void LoadInstantStatus()
	{

		int status = 0;
		if (PlayerPrefs.HasKey("instantPickupOn") == true)
		{
			status = PlayerPrefs.GetInt("instantPickupOn");
		}

		Toggle toggle = this.GetComponent<Toggle>();
		if (status == 1)
		{
			toggle.isOn = true;
		}
		else
		{
			toggle.isOn = false;
		}

	}
}
