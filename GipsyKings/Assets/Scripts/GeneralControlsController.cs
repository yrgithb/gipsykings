using UnityEngine;
using System.Collections;

public class GeneralControlsController : MonoBehaviour
{

	private static GeneralControlsController _instance;

	public static GeneralControlsController instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<GeneralControlsController>();

				//Tell unity not to destroy this object when loading a new scene
				DontDestroyOnLoad(_instance.gameObject);
			}

			return _instance;
		}
	}

	void Awake()
	{

		AudioSource musicSource = this.GetComponent<AudioSource>();
		if (musicSource != null)
		{
			int status = 0;
			if (PlayerPrefs.HasKey("musicOn") == true)
			{
				status = PlayerPrefs.GetInt("musicOn");
			}

			if (status == 0)
			{
				musicSource.volume = 0.0f;
			}
		}

		if (_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if (this != _instance)
			{
				Destroy(this.gameObject);
			}
		}

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey("escape"))
		{
			Application.LoadLevel("MainMenu");

			// remove singleton
			Destroy(_instance.gameObject);
		}
	}
}
