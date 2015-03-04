using UnityEngine;
using System.Collections;

public class BoulderObjectScript : MonoBehaviour
{
	public BoulderTriggerScript boulderTriggerObject;
	public PlayerController playerController;
	public bool canBePickedUp;
	public bool isCarried;

	private AudioSource audioSource;
	public AudioClip[] throwingSounds;

	// Use this for initialization
	void Start ()
	{

		canBePickedUp = false;
		isCarried = false;        

		// ignore collisions with players
		Physics2D.IgnoreLayerCollision (gameObject.layer, LayerMask.NameToLayer ("Players"));

		// assign to player object
		playerController.boulderObject = this;

		audioSource = GetComponent<AudioSource> ();
		if (audioSource == null) {
			print ("Failed loading audio source for player.");
		}
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		canBePickedUp = false;

		if (boulderTriggerObject.triggeringColliders.Count > 0) {
			foreach (DictionaryEntry entry in boulderTriggerObject.triggeringColliders) {
				// if collider hashtable contains player object, mark as pickable
				Collider2D collider = (Collider2D)entry.Key;
				if (collider.gameObject == playerController.gameObject) {
					canBePickedUp = true;
				}
			}
		}
	
	}

	public void PlayThrowSound ()
	{

		int max = throwingSounds.Length;
		int randomIndex = Random.Range (0, max);
		audioSource.clip = throwingSounds [randomIndex];
		audioSource.Play ();

	}
}
