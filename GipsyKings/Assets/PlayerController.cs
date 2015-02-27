using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float maxSpeed = 10.0f;
	bool facingRight = true;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{

		float move = Input.GetAxis("Horizontal");
		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

		if ((move > 0 && facingRight == false) || (move < 0 && facingRight == true)) {
			Flip();
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
