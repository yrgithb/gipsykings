using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	//
	// collider elements
	//
	public CircleCollider2D groundCollider;
	public BoxCollider2D bodyCollider;

	//
	// Accessible boulder
	//
	// Will be set from the boulder trigger script!
	[HideInInspector]
	public BoulderObjectScript boulderObject;

	//
	// movement related variables
	//
	// jump
	private bool grounded = false;
	private bool doubleJump = false;
	public float jumpForce = 1000.0f;

	private bool hitWall = false;
	public float maxSpeed = 10.0f;

	// movement direction
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

	//
	// other actions 
	//
	public bool isCharging;
	public float boulderChargeAmount;
	public float boulderThrowForce = 2000.0f;
	
	private Animator animator;
	private AudioSource audioSource;
	public AudioClip[] walkingSounds;
	public AudioClip jumpingSound;

	// Use this for initialization
	void Start ()
	{

		isCharging = false;
		animator = GetComponent<Animator> ();
		if (animator == null) {
			print ("Failed fetching animator object for player.");
		}

		// default to idle state
		animator.SetInteger ("Player1AnimationState", 0);

		audioSource = GetComponent<AudioSource> ();
		if (audioSource == null) {
			print ("Failed loading audio source for player.");
		}

	}

	void Update ()
	{

		if (isCharging == false) {
			// jump flag flip irrespective of fixed update time
			if (Input.GetButtonDown ("Jump") == true && (grounded == true || doubleJump == false)) {
				if (grounded == false && doubleJump == false) {
					doubleJump = true;

					// reset y velocity so second jump is as powerful as first one
					rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 0.0f);
				}

				rigidbody2D.AddForce (new Vector2 (0.0f, jumpForce));
				PlayJumpSound ();
			}
		}

		if (Input.GetButtonDown ("Fire1") == true) {
			if (grounded == true) { 
				isCharging = true;
			}
		} 

		if (Input.GetButtonUp ("Fire1") == true) {
			isCharging = false;
		}

		UpdateBoulderPosition ();

	}

	void FixedUpdate ()
	{

		Direction direction = CalculateDirection ();
		FlipIfNeeded (direction);	
		PerformMovement (direction);

	}
	
	void OnCollisionExit2D (Collision2D collision)
	{

		foreach (ContactPoint2D contact in collision.contacts) {
			if (contact.otherCollider == groundCollider) {
				grounded = false;
				animator.SetBool ("Grounded", grounded);
			} else if (contact.otherCollider == bodyCollider) {
				hitWall = false;
			}
		}

	}

	void OnCollisionEnter2D (Collision2D collision)
	{

		foreach (ContactPoint2D contact in collision.contacts) {
			if (contact.otherCollider == groundCollider) {
				grounded = true;
				doubleJump = false;
				animator.SetBool ("Grounded", grounded);
			} else if (contact.otherCollider == bodyCollider) {
				hitWall = true;
				animator.SetInteger ("Player1AnimationState", (int)AnimationState.AnimationStateToIdle);
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

		if (isCharging == true) {
			// stop any movement if charging
			directionMultiplier = 0;
			rigidbody2D.velocity = new Vector2 (0.0f, rigidbody2D.velocity.y); 
		}

		if (hitWall == false) {
			if (directionMultiplier != 0) {
				animator.SetInteger ("Player1AnimationState", (int)AnimationState.AnimationStateToWalk);
			} else {
				animator.SetInteger ("Player1AnimationState", (int)AnimationState.AnimationStateToIdle);
			}

			if (isCharging == false) {
				rigidbody2D.velocity = new Vector2 (directionMultiplier * maxSpeed, rigidbody2D.velocity.y);

				if (directionMultiplier != 0 && grounded == true) {
					PlayWalkingSound ();
				}
			}
		}

	}

	void PlayWalkingSound ()
	{

		if (audioSource.isPlaying == false) {
			int max = walkingSounds.Length;
			int randomIndex = Random.Range (0, max);
			audioSource.clip = walkingSounds [randomIndex];
			audioSource.PlayDelayed (0.1f);
		}

	}

	void PlayJumpSound ()
	{

		audioSource.clip = jumpingSound;
		audioSource.Play ();
	
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

		float horizontalAxis = Input.GetAxis ("Horizontal");

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

	public void FinishedChargingAction ()
	{

		isCharging = false;
		if (boulderObject != null) {
			if (boulderObject.isCarried == false) {
				PickupBoulder ();
			} else {
				ThrowBoulder (1.0f);
			}
		}

	}
	
	public void StoppedChargingAction (float percent)
	{
		
		ThrowBoulder (percent);

	}

	void UpdateBoulderPosition ()
	{

		if (boulderObject != null) {
			if (boulderObject.isCarried == true) {
				boulderObject.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y + 1.0f);
			}
		}

	}

	void PickupBoulder ()
	{

		if (boulderObject != null) {
			if (boulderObject.canBePickedUp == true && boulderObject.isCarried == false) {
				// disable physics
				boulderObject.rigidbody2D.isKinematic = true;

				boulderObject.isCarried = true;
			}
		}

	}

	void ThrowBoulder (float percent)
	{

		if (boulderObject != null) {
			if (boulderObject.isCarried == true) {
				// enable physics
				boulderObject.rigidbody2D.isKinematic = false;

				boulderObject.isCarried = false;

				int directionMultiplier = 0;
				if (facingRight == false) {
					directionMultiplier = -1;
				} else {
					directionMultiplier = 1;
				}
				float force = percent * boulderThrowForce;
				boulderObject.rigidbody2D.AddForce(new Vector2(directionMultiplier * force, 0.25f * force)); // to the side & a bit up
			}
		}

	}
}
