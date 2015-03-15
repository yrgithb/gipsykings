using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarScript : MonoBehaviour
{
	public Scrollbar scrollBar;
	public Vector2 offset;
	public float maxCharge;
	public bool isHealthBar;
	public bool isChargeMeter;
	public bool isCharging;

	public float charge;
	public float lastCharge;

	public GameObject objectToNotify;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{

		if (isChargeMeter == true) {
			// hide/show charger if needed
			CanvasGroup groupElement = GetComponent<CanvasGroup> ();

			if (isCharging == true) {
				groupElement.alpha = 1.0f;

				charge += Time.deltaTime;

				if (charge >= maxCharge) {
					lastCharge = 1.0f;
					charge = 0.0f;
					objectToNotify.SendMessage("FinishedChargingAction");
					isCharging = false;
				}
			} else {
				if (charge > 0.0f) {
					lastCharge = charge / maxCharge;
					objectToNotify.SendMessage("StoppedChargingAction", lastCharge);
				}
				charge = 0.0f;			
				groupElement.alpha = 0.0f;
			}
			scrollBar.size = charge / maxCharge;
		} else {
			scrollBar.size = charge / maxCharge;
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
