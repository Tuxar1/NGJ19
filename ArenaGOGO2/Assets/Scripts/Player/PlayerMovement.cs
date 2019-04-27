using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float GroundSpeed = 10f;
	public float AirSpeed = 8f;
	public float GroundJumpSpeed = 10f;
	public float WallJumpSpeed = 12f;
	public Vector3 FacingDirection = new Vector3(-1, 0, 0);

	private float ChangingDirectionBonusFactor = 3;
	private float GroundDeaccelarationBonusFactor = 5;
	private float FlyingXDampFactor = 0.95f;

	private float JumpEarlyBias = 0.1f;
	private float JumpLateBias = 0.1f;
	private float HigherJumpTime = 0.2f;

	public Rigidbody2D Rigidbody;
	public PlayerInput input;
	public BoxCollider2D Collider;

	private bool grounded = false;
	private bool jump = false;
	private bool leftWall = false;
	private bool rightWall = false;
	private float lastGroundTime = float.NegativeInfinity;
	private float lastWallTime = float.NegativeInfinity;
	private float lastJumpInputStart = float.NegativeInfinity;
	private float lastJumpStart = float.NegativeInfinity;

	private float xInput = 0.0f;

	public SpriteRenderer spriteRenderer;
	public Animator Anim;

	// Start is called before the first frame update
	void Start()
    {
		input.JumpChanged += jumpClicked;
		input.XAxis += xChanged;
	}

	private void jumpClicked(bool val)
	{
		if (val && !jump)
		{
			lastJumpInputStart = Time.timeSinceLevelLoad;
		}
		if (!val)
		{
			lastJumpStart = float.NegativeInfinity;
		}
		jump = val;
	}

	private void xChanged(float x)
	{
		xInput = x;
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
		var time = Time.timeSinceLevelLoad;
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
				lastGroundTime = time;
			}
			else if (angle < 100)
			{
				if (contactPoint.normal.x > 0)
				{
					leftWall = true;
					lastWallTime = time;
				}
				else if (contactPoint.normal.x < 0)
				{
					rightWall = true;
					lastWallTime = time;
				}
			}
		}
		if (Anim.GetBool("InAir") == grounded)
		{
			Anim.SetBool("InAir", !grounded);
		}
	}

	void UpdateJump()
	{
		var time = Time.timeSinceLevelLoad;

		var doJump = lastJumpInputStart + JumpEarlyBias > time;
		var continueJump = lastJumpStart + HigherJumpTime > time;
		if (!doJump && !continueJump)
		{
			return;
		}
		var groundedJump = grounded || lastGroundTime + JumpLateBias > time;
		var wallJump = (leftWall || rightWall) || lastWallTime + JumpLateBias > time;
		if (!wallJump && !groundedJump && !continueJump)
		{
			return;
		}
		var momentum = Rigidbody.velocity;
		Vector2 newVelocity = momentum;
		if (groundedJump || continueJump)
		{
			newVelocity = new Vector2(momentum.x, GroundJumpSpeed);
			ResetJumpVars();
			if (doJump)
			{
				lastJumpStart = time;
			}
		}
		else if (wallJump)
		{
			var xVel = leftWall ? 1 : -1;
			var wallJumpVector = new Vector2(xVel, 2).normalized;
			newVelocity = wallJumpVector * WallJumpSpeed;
			ResetJumpVars();
			if (doJump)
			{
				lastJumpStart = time;
			}
		}
		Rigidbody.velocity = newVelocity;
	}

	void ResetJumpVars()
	{
		grounded = false;
		lastGroundTime = float.NegativeInfinity;
		lastJumpInputStart = float.NegativeInfinity;
		lastWallTime = float.NegativeInfinity;
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
			if (changingDirection)
			{
				acceleration *= ChangingDirectionBonusFactor;
			}
			else if (xInput == 0)
			{
				acceleration *= GroundDeaccelarationBonusFactor;
			}
		}
		else
		{
			if (changingDirection)
			{
				acceleration *= ChangingDirectionBonusFactor;
			}
			if (xInput == 0)
			{
				move = momentum.x * FlyingXDampFactor;
			}
			else
			{
				move = xInput * Mathf.Max(Mathf.Sign(xInput) * momentum.x, AirSpeed);
			}
		}

		var newXVel = Mathf.MoveTowards(momentum.x, move, acceleration * Time.fixedDeltaTime);
		Rigidbody.velocity = new Vector2(newXVel, momentum.y);
		Anim.SetFloat("Speed", Mathf.Abs(Rigidbody.velocity.x));
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
