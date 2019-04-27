using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
	public enum GravityMode
	{
		Default,
		Low,
		High,
		Pulsating,
		Rotatiting,
		HeavyWinds,
	};

	public static GravityMode mode = GravityMode.Default;

	private static Vector2 Default = new Vector2(0, -20);
	private static Vector2 Low = new Vector2(0, -5);
	private static Vector2 High = new Vector2(0, -70);
	private static Vector2 Winds = new Vector2(7.5f, -20);
	private GravityMode activeMode = GravityMode.Default;

	private float timeSinceRotate = 0;
	private float timeBetweenRotations = 10;

    // Update is called once per frame
    void Update()
    {
        if (activeMode != mode)
		{
			activeMode = mode;
			SetGravity(activeMode);
		}
		if (activeMode == GravityMode.Pulsating)
		{
			var newYGrav = Mathf.Lerp(Low.y, High.y, Mathf.PingPong(Time.time, 10) / 10);
			Physics2D.gravity = new Vector2(Physics2D.gravity.x, newYGrav);
		}
		if (activeMode == GravityMode.Rotatiting)
		{
			timeSinceRotate += Time.deltaTime;
			if (timeSinceRotate > timeBetweenRotations)
			{
				timeSinceRotate = 0;
				var current = Physics2D.gravity;
				var newGrav = new Vector2(0, 0);
				if (current.x > 0)
				{
					newGrav.y = -current.x;
				}
				else if (current.x < 0)
				{
					newGrav.y = -current.x;
				} 
				else if (current.y > 0)
				{
					newGrav.x = current.y;
				}
				else
				{
					newGrav.x = current.y;
				}
				Debug.Log(current);
				Debug.Log(newGrav);
				Physics2D.gravity = newGrav;
			}
		}
		if (activeMode == GravityMode.HeavyWinds)
		{
			timeSinceRotate += Time.deltaTime;
			if (timeSinceRotate > timeBetweenRotations)
			{
				timeSinceRotate = 0;
				var current = Physics2D.gravity;
				Physics2D.gravity = new Vector2(-current.x, current.y);
			}
		}
    }

	void SetGravity(GravityMode mode)
	{
		switch (mode)
		{
			case GravityMode.Default:
				Physics2D.gravity = Default;
				break;
			case GravityMode.Low:
				Physics2D.gravity = Low;
				break;
			case GravityMode.High:
				Physics2D.gravity = High;
				break;
			case GravityMode.Pulsating:
				Physics2D.gravity = Default;
				break;
			case GravityMode.Rotatiting:
				Physics2D.gravity = Default;
				timeSinceRotate = 0;
				break;
			case GravityMode.HeavyWinds:
				Physics2D.gravity = Winds;
				timeSinceRotate = 0;
				break;
			default:
				break;
		}
	}
}
