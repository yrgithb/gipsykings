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
	private bool jumping = false;
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

	// Use this for initialization
	void Start ()
	{

	}

	void Update () 
	{

		// jump flag flip irrespective of fixed update time
		if (Input.GetButtonDown ("Jump") == true && grounded == true) {
			rigidbody2D.AddForce (new Vector2 (0.0f, jumpForce));
			jumping = false;
		}

	}

	void FixedUpdate ()
	{

		Direction direction = CalculateDirection ();
		FlipIfNeeded (direction);	
		PerformMovement (direction);

	}
	
	void OnCollisionExit2D(Collision2D collision)
	{
		foreach (ContactPoint2D contact in collision.contacts)
		{
			if (contact.otherCollider == groundCollider) {
				if (jumping == false) {
					grounded = false;
				}
			} else if (contact.otherCollider == bodyCollider) {
				hitWall = false;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		foreach (ContactPoint2D contact in collision.contacts)
		{
			if (contact.otherCollider == groundCollider) {
				if (jumping == false) {
					grounded = true;
				}
			} else if (contact.otherCollider == bodyCollider) {
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
