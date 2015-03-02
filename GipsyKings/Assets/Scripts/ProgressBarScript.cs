using UnityEngine;
using System.Collections;

public class ProgressBarScript : MonoBehaviour
{
	public float barDisplay; //current progress
	public Vector2 offset;
	public Vector2 size;

	// temporary variable for animation
	private int directionMultiplier;
	private bool goingUp;

	void OnGUI ()
	{
		Vector2 framePosition = Camera.main.WorldToScreenPoint (gameObject.transform.position);
		framePosition.y = Screen.height - framePosition.y;

		// center
		framePosition.x -= size.x / 2;
		framePosition.y -= size.y / 2;

		// add offset
		framePosition.x += offset.x;
		framePosition.y += offset.y;

		////draw the background:
		GUI.BeginGroup (new Rect (framePosition.x, framePosition.y, size.x, size.y));
		GUI.Box (new Rect (0, 0, size.x, size.y), "");
		
		////draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, size.x * barDisplay, size.y));
		GUI.Box (new Rect (0, 0, size.x, size.y), "");
		GUI.EndGroup ();
		GUI.EndGroup ();
	}

	void Start ()
	{

		goingUp = true;
		directionMultiplier = 1;

	}
	
	void Update ()
	{

		// basic animation
		if ((goingUp == true && barDisplay >= 1.0f) || (goingUp == false && barDisplay <= 0.0f)) {
			goingUp = !goingUp;
			directionMultiplier *= -1;
		}

		barDisplay += directionMultiplier * Time.deltaTime * 0.5f;
	}
}