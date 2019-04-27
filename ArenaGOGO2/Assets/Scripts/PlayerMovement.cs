using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float GroundSpeed = 10f;
	public float AirSpeed = 8f;
	public float GroundJumpSpeed = 15f;
	public float WallJumpSpeed = 15f;
	public Vector3 FacingDirection = new Vector3(-1, 0, 0);

	public Rigidbody2D Rigidbody;
	public PlayerInput input;
	public BoxCollider2D Collider;

	private bool grounded = false;
	private bool jump = false;
	private bool leftWall = false;
	private bool rightWall = false;
	private bool wallJump = false;

	private float xInput = 0.0f;

	public SpriteRenderer spriteRenderer;

	// Start is called before the first frame update
	void Start()
    {
		input.JumpChanged += jumpClicked;
		input.XAxis += xChanged;
	}

	private void jumpClicked(bool val)
	{
		if (grounded && val)
		{
			jump = true;
		}
		else if ((leftWall || rightWall) && val)
		{
			wallJump = true;
		}
	}

	private void xChanged(float x)
	{
		xInput = x;
	}

	// Update is called once per frame
	void Update()
    {
		//grounded = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y - 0.75f), 1 << LayerMask.NameToLayer("Ground"));
		//leftWall = Physics2D.Linecast(transform.position, new Vector2(transform.position.x - 0.5f, transform.position.y), 1 << LayerMask.NameToLayer("Ground"));
		//rightWall = Physics2D.Linecast(transform.position, new Vector2(transform.position.x + 0.5f, transform.position.y), 1 << LayerMask.NameToLayer("Ground"));
		//Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - 0.75f), Color.red);
		//Debug.DrawLine(transform.position, new Vector2(transform.position.x - 0.5f, transform.position.y), Color.blue);
		//Debug.DrawLine(transform.position, new Vector2(transform.position.x + 0.5f, transform.position.y), Color.green);
	}

	public void Hit(Transform arm, float strength)
	{
		var dir = transform.position.x - arm.position.x;
		var force = new Vector2(Mathf.Sign(dir) * strength, 0);
		Rigidbody.AddForce(force);
	}

	void FixedUpdate()
	{
		UpdateCollisions();
		UpdateJump();
		UpdateMove();
		UpdateSprite();
	}

	void UpdateCollisions()
	{
		grounded = false;
		leftWall = false;
		rightWall = false;

		var contacts = new ContactPoint2D[4];
		var contactAmount = Collider.GetContacts(contacts);
		
		for (var i = 0; i < contactAmount; i++)
		{
			var contactPoint = contacts[i];
			var angle = Vector2.Angle(Vector2.up, contactPoint.normal);
			if (angle < 60)
			{
				grounded = true;
			}
			else if (angle < 100)
			{
				if (contactPoint.normal.x > 0)
				{
					leftWall = true;
				}
				else if (contactPoint.normal.x < 0)
				{
					rightWall = true;
				}
			}
			
		}
	}

	void UpdateJump()
	{
		// TODO needs late bias
		if (!jump && !wallJump)
		{
			return;
		}
		jump = false;
		wallJump = false;
		var momentum = Rigidbody.velocity;
		Vector2 newVelocity = momentum;
		if (grounded)
		{
			newVelocity = new Vector2(momentum.x, GroundJumpSpeed);
		}
		else if (leftWall || rightWall)
		{
			var xVel = leftWall ? 1 : -1;
			var wallJumpVector = new Vector2(xVel, 2).normalized;
			newVelocity = wallJumpVector * WallJumpSpeed;
		}
		Rigidbody.velocity = newVelocity;
	}

	void UpdateMove()
	{
		var momentum = Rigidbody.velocity;
		var acceleration = grounded ? GroundSpeed : AirSpeed;
		var move = 0f;
		var changingDirection = Mathf.Sign(momentum.x) != Mathf.Sign(xInput);
		if (grounded)
		{
			move = xInput * GroundSpeed;
			if (changingDirection || xInput == 0)
			{
				acceleration *= 2;
			}
		}
		else
		{
			if (changingDirection)
			{
				acceleration *= 2;
			}
			if (xInput == 0)
			{
				move = momentum.x;
			}
			else
			{
				move = xInput * Mathf.Max(Mathf.Sign(xInput) * momentum.x, AirSpeed);
			}
		}

		var newXVel = Mathf.MoveTowards(momentum.x, move, acceleration * Time.fixedDeltaTime);
		Rigidbody.velocity = new Vector2(newXVel, momentum.y);
	}

	void UpdateSprite()
	{
		if (xInput != 0)
		{
			FacingDirection.x = xInput > 0 ? 1 : -1;
			spriteRenderer.flipX = FacingDirection.x == 1;
		}
	}

	private static Vector2 AverageNormal(Vector2[] vectors)
	{
		var x = 0f;
		var y = 0f;
		foreach (var vector in vectors)
		{
			x += vector.x;
			y += vector.y;
		}
		return new Vector2(x / vectors.Length, y / vectors.Length).normalized;
	}
}
