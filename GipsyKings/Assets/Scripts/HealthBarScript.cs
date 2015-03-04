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

	private float charge;

	// Use this for initialization
	void Start ()
	{

		if (isHealthBar == true) {
			scrollBar.size = 1.0f;
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		// TODO: Move to Start () after determining value, locating this here is needed only for debugging value changes 
		chargeTime = parentGameObject.boulderChargeAmount;

		// anochor to parent game object
		Vector2 framePosition = Camera.main.WorldToScreenPoint (parentGameObject.transform.position);
		framePosition.x += offset.x;
		framePosition.y += offset.y;
		
		RectTransform rectTransform = (RectTransform)this.transform;
		rectTransform.anchoredPosition = framePosition;

		if (isChargeMeter == true) {
			if (parentGameObject.isCharging == true) {
				charge += Time.deltaTime;

				if (charge >= chargeTime) {
					parentGameObject.FinishedChargingAction ();
					charge = 0.0f;
				}
			} else {
				if (charge > 0.0f) {
					parentGameObject.StoppedChargingAction (charge / chargeTime);
				}
				charge = 0.0f;
			}
			scrollBar.size = charge / chargeTime;
		}
	}
}
