using UnityEngine;
using System.Collections;

public class BoulderScript : MonoBehaviour
{
	public bool isThrown;

	// Use this for initialization
	void Start ()
	{
	
		isThrown = false;

		// disable player layer collisions
		Physics2D.IgnoreLayerCollision (gameObject.layer, LayerMask.NameToLayer ("Players"));

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate ()
	{

	}
}
