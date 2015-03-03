using UnityEngine;
using System.Collections;

public class BoulderTriggerScript : MonoBehaviour
{

	public Collider2D triggeringCollider; 

	// Use this for initialization
	void Start ()
	{

		triggeringCollider = null;
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{

		triggeringCollider = other;

	}

	void OnTriggerExit2D (Collider2D other)
	{

		triggeringCollider = null;

	}
}
