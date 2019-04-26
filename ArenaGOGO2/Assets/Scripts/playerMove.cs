using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
	public float HorizontalForce = 36f;
	public float MaxXVelocity = 5f;
	public float JumpVelocity = 1000f;
    public Vector3 FacingDirection = new Vector3(1, 0, 0);

	public Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		var horizontalMovement = Input.GetAxis("Horizontal");

		if (horizontalMovement * rigidbody.velocity.x < MaxXVelocity)
		{
            if (horizontalMovement != 0)
                FacingDirection.x = horizontalMovement > 0 ? 1 : -1;
            print(FacingDirection);
			rigidbody.AddForce(Vector2.right * horizontalMovement * HorizontalForce);
		}
		if (Mathf.Abs(rigidbody.velocity.x) > MaxXVelocity)
		{
			rigidbody.velocity = new Vector2(Mathf.Sign(rigidbody.velocity.x) * MaxXVelocity, rigidbody.velocity.y);
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			rigidbody.AddForce(new Vector2(0, JumpVelocity));
		}
    }
}
