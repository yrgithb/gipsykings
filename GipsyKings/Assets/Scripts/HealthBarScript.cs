using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarScript : MonoBehaviour
{
	public PlayerController parentGameObject;
	public Scrollbar scrollBar;
	public Vector2 offset;
	private float chargeTime;
	public bool isHealthBar;
	public bool isChargeMeter;

	// temporary variable for animation
	private int directionMultiplier;
	private bool goingUp;
	private float charge;

	// Use this for initialization
	void Start ()
	{
		
		goingUp = true;
		directionMultiplier = 1;

	}
	
	// Update is called once per frame
	void Update ()
	{
		// TODO: Move to Start () after determining value, locating this here is needed only for debugging value changes 
		chargeTime = parentGameObject.chargeAmount;
		

		// anochor to parent game object
		Vector2 framePosition = Camera.main.WorldToScreenPoint (parentGameObject.transform.position);
		framePosition.x += offset.x;
		framePosition.y += offset.y;
		
		RectTransform rectTransform = (RectTransform)this.transform;
		rectTransform.anchoredPosition = framePosition;

		if (isHealthBar == true) {
			// basic animation
			if ((goingUp == true && scrollBar.size >= 1.0f) || (goingUp == false && scrollBar.size <= 0.0f)) {
				goingUp = !goingUp;
				directionMultiplier *= -1;
			}

			scrollBar.size += directionMultiplier * Time.deltaTime * 0.5f;
		} else if (isChargeMeter == true) {
			if (parentGameObject.isCharging == true) {
				charge += Time.deltaTime;

				if (charge >= chargeTime) {
					parentGameObject.FinishedChargingAction();
					charge = 0.0f;
				}
			} else {
				charge = 0.0f;
			}
			scrollBar.size = charge / chargeTime;
		}
	}
}
