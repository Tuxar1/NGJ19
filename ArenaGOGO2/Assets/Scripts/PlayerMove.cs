using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	public float HorizontalForce = 365f;
	public float MaxXVelocity = 5f;
	public float JumpVelocity = 1000f;
	public Vector3 FacingDirection = new Vector3(-1, 0, 0);

	public Rigidbody2D Rigidbody;
	public PlayerInput input;

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

	void Update()
	{
		grounded = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y - 0.75f), 1 << LayerMask.NameToLayer("Ground"));
		leftWall = Physics2D.Linecast(transform.position, new Vector2(transform.position.x - 0.75f, transform.position.y), 1 << LayerMask.NameToLayer("Ground"));
		rightWall = Physics2D.Linecast(transform.position, new Vector2(transform.position.x + 0.75f, transform.position.y), 1 << LayerMask.NameToLayer("Ground"));
	}

	public void Hit(Transform arm, float strength)
	{
		var dir = transform.position.x - arm.position.x;
		var force = new Vector2(Mathf.Sign(dir) * strength, 0);
		Rigidbody.AddForce(force);
	}

	void FixedUpdate()
	{
		if (xInput * Rigidbody.velocity.x < MaxXVelocity)
		{
            Rigidbody.AddForce(Vector2.right * xInput * HorizontalForce);
		}
		if (Mathf.Abs(Rigidbody.velocity.x) > MaxXVelocity)
		{
			//var yvel = 0.0f;
			//var newXVel = Mathf.SmoothDamp(Rigidbody.velocity.x, MaxXVelocity, ref yvel, 0.5f);
			//Rigidbody.velocity = new Vector2(newXVel, Rigidbody.velocity.y);
			Rigidbody.velocity = new Vector2(Mathf.Sign(Rigidbody.velocity.x) * MaxXVelocity, Rigidbody.velocity.y);
		}

		if (xInput != 0)
		{
			FacingDirection.x = xInput > 0 ? 1 : -1;
			spriteRenderer.flipX = FacingDirection.x == 1;
		}

		if (jump)
		{
			jump = false;
			Rigidbody.AddForce(new Vector2(0, JumpVelocity));
		} else if (wallJump)
		{
			wallJump = false;
			Rigidbody.AddForce(new Vector2(leftWall ? 1000 : -1000, JumpVelocity / 2));
		}
	}
}
