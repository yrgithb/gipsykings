using UnityEngine;
using System.Collections;

public class BoulderObjectScript : MonoBehaviour
{
	public BoulderTriggerScript boulderTriggerObject;
	public PlayerController playerController;
	public bool canBePickedUp;
	public bool isCarried;

	// Use this for initialization
	void Start ()
	{

		canBePickedUp = false;
		isCarried = false;        

		// ignore collisions with players
		Physics2D.IgnoreLayerCollision (gameObject.layer, LayerMask.NameToLayer ("Players"));

		// assign to player object
		playerController.boulderObject = this;
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		// trigger position should match boulder image object
		//boulderTriggerObject.transform.position = boulderImageObject.transform.position;

		canBePickedUp = false;

		if (boulderTriggerObject.triggeringCollider != null) {
			if (boulderTriggerObject.triggeringCollider.gameObject == playerController.gameObject) {
				canBePickedUp = true;
			}
		}
	
	}
}
