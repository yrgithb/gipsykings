using UnityEngine;
using System.Collections;

public class BoulderTriggerScript : MonoBehaviour
{
	public PlayerController playerController;
	public Transform attachedToBoulder;
	public bool canBePickedUp;

	// Use this for initialization
	void Start ()
	{
	
		canBePickedUp = false;

		// init boulder object
		playerController.boulderObject = this;

	}
	
	// Update is called once per frame
	void Update ()
	{

		// same position as boulder image object
		this.transform.position = attachedToBoulder.transform.position;
	
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject == playerController.gameObject) {
			canBePickedUp = true;
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{

		if (other.gameObject == playerController.gameObject) {
			canBePickedUp = false;
		}
	}
}
