using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
	public float HorizontalForce = 36f;
	public float MaxXVelocity = 5f;
	public float JumpVelocity = 1000f;
    public Vector3 FacingDirection = new Vector3(1, 0, 0);

	public Rigidbody2D Rigidbody;
	public Transform GroundCheck;

	private bool grounded = false;
	private bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

	void Update()
	{
		grounded = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y - 0.75f), 1 << LayerMask.NameToLayer("Ground"));
		
		if (grounded && Input.GetKeyDown(KeyCode.Space))
		{
			jump = true;
		}
	}

	void FixedUpdate()
    {
		var horizontalMovement = Input.GetAxis("Horizontal");

		if (horizontalMovement * Rigidbody.velocity.x < MaxXVelocity)
		{
            if (horizontalMovement != 0)
                FacingDirection.x = horizontalMovement > 0 ? 1 : -1;
			rigidbody.AddForce(Vector2.right * horizontalMovement * HorizontalForce);
		}
		if (Mathf.Abs(Rigidbody.velocity.x) > MaxXVelocity)
		{
			Rigidbody.velocity = new Vector2(Mathf.Sign(Rigidbody.velocity.x) * MaxXVelocity, Rigidbody.velocity.y);
		}

		if (jump)
		{
			jump = false;
			Rigidbody.AddForce(new Vector2(0, JumpVelocity));
		}
    }
}
