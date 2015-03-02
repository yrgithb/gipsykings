using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarScript : MonoBehaviour {
	public GameObject parentGameObject;
	public Scrollbar scrollBar;
	public Vector2 offset;

	// temporary variable for animation
	private int directionMultiplier;
	private bool goingUp;
	
	// Use this for initialization
	void Start () {
		
		goingUp = true;
		directionMultiplier = 1;
	}
	
	// Update is called once per frame
	void Update () {
		// basic animation
		if ((goingUp == true && scrollBar.size >= 1.0f) || (goingUp == false && scrollBar.size <= 0.0f)) {
			goingUp = !goingUp;
			directionMultiplier *= -1;
		}

		scrollBar.size += directionMultiplier * Time.deltaTime * 0.5f;

		// anochor to parent game object
		Vector2 framePosition = Camera.main.WorldToScreenPoint (parentGameObject.transform.position);
		framePosition.x += offset.x;
		framePosition.y += offset.y;

		RectTransform rectTransform = (RectTransform)this.transform;
		rectTransform.anchoredPosition = framePosition;
	}
}
