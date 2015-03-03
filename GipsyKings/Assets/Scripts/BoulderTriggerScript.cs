using UnityEngine;
using System.Collections;

public class BoulderTriggerScript : MonoBehaviour
{
	public Hashtable triggeringColliders;

	// Use this for initialization
	void Start ()
	{

		triggeringColliders = new Hashtable ();
		triggeringColliders.Clear ();
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{

		triggeringColliders[other] = other.gameObject;

	}

	void OnTriggerExit2D (Collider2D other)
	{
		
		triggeringColliders.Remove(other);

	}
}
