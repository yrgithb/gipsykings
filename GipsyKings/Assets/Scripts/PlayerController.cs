using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	//
	// movement related variables
	//

	// jump
	private Transform groundCheck;
	private bool grounded = false;
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

		groundCheck = transform.Find ("GroundCheck");
	
	}

	// Update is called once per frame
	void Update ()
	{

		// check ground collision
		//grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, groundMask);
		grounded = Physics2D.Linecast(groundCheck.position, new Vector2(0.0f, -0.5f));

		// jump flag flip
		if (Input.GetButtonDown ("Jump") == true && grounded == true) {
			jumping = true;
		}

	}

	void FixedUpdate ()
	{

		Direction direction = CalculateDirection ();
		FlipIfNeeded (direction);	
		PerformMovement (direction);

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
		rigidbody2D.velocity = new Vector2 (directionMultiplier * maxSpeed, rigidbody2D.velocity.y);

		if (jumping == true) {
			rigidbody2D.AddForce (new Vector2 (0.0f, jumpForce));
			jumping = false;
		}
	}

	void FlipIfNeeded (Direction direction)
	{
	
		// flip if needed
		if ((direction == Direction.DirectionRight && facingRight == false) || (direction == Direction.DirectionLeft && facingRight == true)) {
			Flip ();
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
}
