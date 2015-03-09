using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarScript : MonoBehaviour
{
	public Scrollbar scrollBar;
	public Vector2 offset;
	public float chargeTime;
	public bool isHealthBar;
	public bool isChargeMeter;
	public bool isCharging;

	private float charge;
	public float lastCharge;

	public GameObject objectToNotify;

	// Use this for initialization
	void Start ()
	{

		if (isHealthBar == true) {
			scrollBar.size = 1.0f;
		}
		chargeTime = 0.0f;
		charge = 0.0f;
		lastCharge = 0.0f;
		objectToNotify = null;

	}
	
	// Update is called once per frame
	void Update ()
	{

		if (isChargeMeter == true) {
			if (isCharging == true) {
				charge += Time.deltaTime;

				if (charge >= chargeTime) {
					lastCharge = 1.0f;
					charge = 0.0f;
					objectToNotify.SendMessage("FinishedChargingAction");
					isCharging = false;
				}
			} else {
				if (charge > 0.0f) {
					lastCharge = charge / chargeTime;
					objectToNotify.SendMessage("StoppedChargingAction", lastCharge);
				}
				charge = 0.0f;
			}
			scrollBar.size = charge / chargeTime;
		}

	}

	public void UpdateWithParentPosition (Vector3 position)
	{

		// anochor to parent game object
		Vector2 framePosition = Camera.main.WorldToScreenPoint (position);
		framePosition.x += offset.x;
		framePosition.y += offset.y;
		
		RectTransform rectTransform = (RectTransform)this.transform;
		rectTransform.anchoredPosition = framePosition;

	}

	public void Charge ()
	{

		isCharging = true;

	}

	public void StopCharging ()
	{

		isCharging = false;

	}
}
