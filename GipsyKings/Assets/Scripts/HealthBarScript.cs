using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarScript : MonoBehaviour
{
	public PlayerController owningPlayer;
	public Scrollbar scrollBar;
	public Vector2 offset;
	private float chargeTime;
	public bool isHealthBar;
	public bool isChargeMeter;

	private float charge;

	// Use this for initialization
	void Start ()
	{

		if (isHealthBar == true) {
			scrollBar.size = 1.0f;
		}
		chargeTime = owningPlayer.boulderChargeAmount;

	}
	
	// Update is called once per frame
	void Update ()
	{

		// anochor to parent game object
		Vector2 framePosition = Camera.main.WorldToScreenPoint (owningPlayer.transform.position);
		framePosition.x += offset.x;
		framePosition.y += offset.y;
		
		RectTransform rectTransform = (RectTransform)this.transform;
		rectTransform.anchoredPosition = framePosition;

		if (isChargeMeter == true) {
			if (owningPlayer.isCharging == true) {
				charge += Time.deltaTime;

				if (charge >= chargeTime) {
					owningPlayer.FinishedChargingAction ();
					charge = 0.0f;
				}
			} else {
				if (charge > 0.0f) {
					owningPlayer.StoppedChargingAction (charge / chargeTime);
				}
				charge = 0.0f;
			}
			scrollBar.size = charge / chargeTime;
		}

	}
}
