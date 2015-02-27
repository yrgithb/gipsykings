using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float maxSpeed = 10.0f;
	public float jumpForce = 1000.0f;
	public LayerMask groundMask;

	private bool facingRight = true; // required for sprite flipping on direction change
	private Transform groundCheck;
	private float groundRadius = 0.2f;
	private bool grounded = false;
	private bool jumping = false;

	// Use this for initialization
	void Start ()
	{
		groundCheck = transform.Find("GroundCheck");
	}

	// Update is called once per frame
	void Update ()
	{

		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);

		// jump
		if (Input.GetButtonDown("Jump") == true && grounded == true) {
			jumping = true;
		}

	}

	void FixedUpdate ()
	{
		// horizontal movement
		float move = Input.GetAxis ("Horizontal");
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);

		if ((move > 0 && facingRight == false) || (move < 0 && facingRight == true)) {
			Flip ();
		}

		if (jumping == true) {
			rigidbody2D.AddForce (new Vector2 (0.0f, jumpForce));
			jumping = false;
		}
	}

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
