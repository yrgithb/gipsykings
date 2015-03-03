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
	// movement related variables
	//
	// jump
	private bool grounded = false;
	private bool hitWall = false;
	public float maxSpeed = 10.0f;
	public float jumpForce = 1000.0f;
	public LayerMask groundMask;

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
		AnimationStateToJump = 1,
		AnimationStateToWalk = 2,
	}

	//
	// other actions 
	//
	public bool isCharging;

	private Animator animator;

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
	
	}

	void Update ()
	{

		// jump flag flip irrespective of fixed update time
		if (Input.GetButtonDown ("Jump") == true && grounded == true) {
			rigidbody2D.AddForce (new Vector2 (0.0f, jumpForce));
		}

		if (Input.GetButtonDown ("Fire1") == true) {
			isCharging = true;
		} 

		if (Input.GetButtonUp ("Fire1") == true) {
			isCharging = false;
		}

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
				animator.SetInteger ("Player1AnimationState", (int)AnimationState.AnimationStateToJump);
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
				animator.SetInteger ("Player1AnimationState", (int)AnimationState.AnimationStateToIdle);
			} else if (contact.otherCollider == bodyCollider) {
				if (grounded == true) {
					animator.SetInteger ("Player1AnimationState", (int)AnimationState.AnimationStateToIdle);
				}
				hitWall = true;
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
			if (grounded == true) {
				if (directionMultiplier != 0) {
					animator.SetInteger ("Player1AnimationState", (int)AnimationState.AnimationStateToWalk);
				} else {
					animator.SetInteger ("Player1AnimationState", (int)AnimationState.AnimationStateToIdle);
				}
			}
			rigidbody2D.velocity = new Vector2 (directionMultiplier * maxSpeed, rigidbody2D.velocity.y);
		}

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
}
