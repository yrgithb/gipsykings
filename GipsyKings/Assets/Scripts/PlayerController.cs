using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public enum OwningPlayer
	{
		Player1,
		Player2
	}
	public OwningPlayer player;
	
	public float health = 10.0f;
	public HealthBarScript healthBar;
	public HealthBarScript chargeBar;
	public Text endGameScreen;

	private GameObject potentialBoulder;
	private GameObject heldBoulder; 

	// movement
	public CircleCollider2D groundCollider;
	public BoxCollider2D bodyCollider;

	public bool grounded = false;
	private bool doubleJump = false;
	public float jumpForce = 1000.0f;
	public bool hitWall = false;
	public float maxSpeed = 10.0f;

	private bool facingRight = true;
	enum Direction
	{
		DirectionNone,
		DirectionLeft,
		DirectionRight,
	};
	enum AnimationState
	{
		AnimationStateToIdle = 0,
		AnimationStateToWalk = 1,
	}

	public float boulderChargeAmount;
	public float boulderThrowForce = 2000.0f;
	public GameObject detectedCollisionBoulder; 

	// video & audio
	private Animator animator;
	private AudioSource audioSource;
	public AudioClip[] walkingSounds;
	private float lastStepTime;
	public float stepDelay;
	public AudioClip jumpingSound;
	public AudioClip[] pickupSounds;
	
	void Start ()
	{

		lastStepTime = -1;

		animator = GetComponent<Animator> ();
		animator.SetInteger ("PlayerAnimationState", 0);

		audioSource = GetComponent<AudioSource> ();

		potentialBoulder = null;
		heldBoulder = null;
		detectedCollisionBoulder = null;

		healthBar.charge = health;
		healthBar.maxCharge = health;
		healthBar.objectToNotify = this.gameObject;
		chargeBar.maxCharge = boulderChargeAmount;
		chargeBar.objectToNotify = this.gameObject;

		// hide end game text view
		endGameScreen.gameObject.SetActive(false);

	}

	void Update ()
	{

		ProcessInput ();

		Direction direction = CalculateDirection ();
		FlipIfNeeded (direction);	
		PerformMovement (direction);

	}

	string playerButton (string buttonName)
	{

		string result = buttonName;

		string prefix = "P1";
		if (player == OwningPlayer.Player2) {
			prefix = "P2";
		}

		result = result.Insert (0, prefix);

		return result;

	}

	void ProcessInput ()
	{

		// jump flag flip irrespective of fixed update time
		if (Input.GetButtonDown (playerButton ("Jump")) == true && (grounded == true || doubleJump == false)) {
			if (grounded == false && doubleJump == false) {
				doubleJump = true;
					
				// reset y velocity so second jump is as powerful as first one
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, 0.0f);
			}
				
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0.0f, jumpForce));
			PlayJumpSound ();
		}
		
		if (Input.GetButtonUp (playerButton ("Action")) == true) {
			chargeBar.StopCharging ();
		}

		if (Input.GetButtonDown (playerButton ("Action")) == true && (potentialBoulder != null || heldBoulder != null)) {
			chargeBar.Charge ();
		} 


	}

	void CheckForBoulderDamage (GameObject boulderObject)
	{

		if (detectedCollisionBoulder == null) {
			BoulderObject body = boulderObject.GetComponent<BoulderObject> ();
			GameObject visuals = body.visuals;
			Rigidbody2D rigidBody = visuals.GetComponent<Rigidbody2D> ();
			float boulderMagnitude = rigidBody.velocity.sqrMagnitude;
			if (boulderMagnitude > 0.0f) { // needs some horizontal velocity, too
				detectedCollisionBoulder = boulderObject;
				print ("Boulder velocity squared magnitude " + boulderMagnitude);

				// take damage
				health -= 3.5f;
				healthBar.charge = health;

				// Death
				if (health <= 0.0f) {
					ShowEndGameText	();

					// hide UI bars
					healthBar.gameObject.SetActive(false);
					chargeBar.gameObject.SetActive(false);
					this.gameObject.SetActive(false);
				}
			}
		}

	}

	void ShowEndGameText () 
	{

		string winningPlayer = "Player 1";
		if (player == OwningPlayer.Player1) {
			winningPlayer = "Player 2";
		}
		string currentText = winningPlayer + " is victorious.\nPress Enter to restart\nEscape to quit";
		
		endGameScreen.gameObject.SetActive(true);
		endGameScreen.text = currentText;

	}

	void OnTriggerEnter2D (Collider2D other)
	{

		if (other.gameObject.tag == "Boulders") {
			CheckForBoulderDamage (other.gameObject);

			if (potentialBoulder == null) {
				// select this boulder as potential
				// a queue collection would solve any possible problems with more than 1 object in range
				potentialBoulder = other.gameObject;
			}
		}

	}

	void OnTriggerExit2D (Collider2D other)
	{

		if (other.gameObject == potentialBoulder) {
			potentialBoulder = null;
		}

		if (other.gameObject == detectedCollisionBoulder) {
			detectedCollisionBoulder = null;
		}

	}


	void OnCollisionEnter2D (Collision2D collision)
	{

		foreach (ContactPoint2D contact in collision.contacts) {
			// process collisions with map
			if (contact.collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
				if (contact.otherCollider == groundCollider) {
					grounded = true;
					doubleJump = false;
					animator.SetBool ("Grounded", grounded);
				} else if (contact.otherCollider == bodyCollider) {
					hitWall = true;
					animator.SetInteger ("PlayerAnimationState", (int)AnimationState.AnimationStateToIdle);
				}
			}
		}

	}
	
	void OnCollisionExit2D (Collision2D collision)
	{
		
		foreach (ContactPoint2D contact in collision.contacts) {
			// process collisions with map
			if (contact.collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
				if (contact.otherCollider == groundCollider) {
					grounded = false;
					animator.SetBool ("Grounded", grounded);
				} else if (contact.otherCollider == bodyCollider) {
					hitWall = false;
				}
			}
		}
		
	}

	void PerformMovement (Direction direction)
	{

		// add movement
		int directionMultiplier = 0;
		if (direction == Direction.DirectionLeft) {
			directionMultiplier = -1;
		} else if (direction == Direction.DirectionRight) {
			directionMultiplier = 1;
		}

		if (hitWall == false) {
			if (directionMultiplier != 0) {
				animator.SetInteger ("PlayerAnimationState", (int)AnimationState.AnimationStateToWalk);
			} else {
				animator.SetInteger ("PlayerAnimationState", (int)AnimationState.AnimationStateToIdle);
			}

			if (potentialBoulder != null && heldBoulder == null && chargeBar.isCharging == true) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.0f, 0.0f);
				return;
			}

			GetComponent<Rigidbody2D> ().velocity = new Vector2 (directionMultiplier * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);

			// play step sound
			if (directionMultiplier != 0 && grounded == true && HasEnoughTimePassedSinceLastStep () == true) {
				PlayWalkingSound ();
			}
		}

		// update held boulder position
		if (heldBoulder != null) {
			BoulderObject obj = heldBoulder.GetComponent<BoulderObject> ();

			Vector3 boulderPosition = this.transform.position;
			boulderPosition.y += 1.0f;
			obj.visuals.transform.position = boulderPosition;
		}

		// update UI bar positions
		// anochor to parent game object
		healthBar.UpdateWithParentPosition(this.transform.position);
		chargeBar.UpdateWithParentPosition(this.transform.position);

	}

	bool HasEnoughTimePassedSinceLastStep ()
	{

		bool result = false; 

		if (Time.time - lastStepTime > stepDelay) {
			result = true;
		}

		return result;
	}

	void PlaySoundClip (AudioClip clip)
	{

		audioSource.PlayOneShot (clip, 1.0f);

	}

	void PlayWalkingSound ()
	{

		lastStepTime = Time.time;
		int max = walkingSounds.Length;
		int randomIndex = Random.Range (0, max);
		PlaySoundClip (walkingSounds [randomIndex]);

	}

	void PlayPickupSound ()
	{

		int max = pickupSounds.Length;
		int randomIndex = Random.Range (0, max);
		PlaySoundClip (pickupSounds [randomIndex]);

	}

	void PlayJumpSound ()
	{

		PlaySoundClip (jumpingSound);
	
	}

	void FlipIfNeeded (Direction direction)
	{

		// flip if needed
		if ((direction == Direction.DirectionRight && facingRight == false) || (direction == Direction.DirectionLeft && facingRight == true)) {
			Flip ();

			// if direction was changed then wall flag should be reset
			ResetHitWallFlag ();
		}	

	}

	Direction CalculateDirection ()
	{

		Direction result = Direction.DirectionNone;

		float horizontalAxis = Input.GetAxis (playerButton ("Horizontal"));

		if (horizontalAxis > 0.0f) {
			result = Direction.DirectionRight;
		} else if (horizontalAxis < 0.0f) {
			result = Direction.DirectionLeft;
		}

		return result;

	}

	void Flip ()
	{

		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	
	}

	void ResetHitWallFlag ()
	{

		hitWall = false;

	}

	void ThrowBoulder (float percent)
	{

		if (heldBoulder != null) {
			BoulderObject obj = heldBoulder.GetComponent<BoulderObject> ();
			Rigidbody2D visualsBody = obj.visuals.GetComponent<Rigidbody2D> ();
			visualsBody.isKinematic = false;

			int directionMultiplier = 0;
			if (facingRight == false) {
				directionMultiplier = -1;
			} else {
				directionMultiplier = 1;
			}
			float force = percent * boulderThrowForce;
			visualsBody.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (directionMultiplier * force, 0.5f * force)); // to the side & a bit up

			heldBoulder = null;
		}

	}

	public void FinishedChargingAction ()
	{

		if (heldBoulder == null && potentialBoulder != null) {
			heldBoulder = potentialBoulder;
			potentialBoulder = null;

			BoulderObject obj = heldBoulder.GetComponent<BoulderObject> ();
			Rigidbody2D visualsBody = obj.visuals.GetComponent<Rigidbody2D> ();
			visualsBody.isKinematic = true;
		} else if (heldBoulder != null) {
			ThrowBoulder (1.0f);
		}

	}
	
	public void StoppedChargingAction (float percent)
	{

		if (heldBoulder != null) {
			ThrowBoulder (percent);
		}

	}

}
