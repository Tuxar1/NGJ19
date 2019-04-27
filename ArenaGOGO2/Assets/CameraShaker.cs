using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{

	private void Start()
	{
		origX = transform.position.x;
		origY = transform.position.y;
		origZ = transform.position.z;
	}

	public void DoShake()
	{
		StartCoroutine(Shake());
	}

	float origX;
	float origY;
	float origZ;
	float duration = 1;
	float magnitude = 0.2f;

	IEnumerator Shake()
	{
		float elapsed = 0.0f;

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;

			float percentComplete = elapsed / duration;
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;

			Camera.main.transform.position = new Vector3(x, y, origZ);

			yield return null;
		}
		Camera.main.transform.position = new Vector3(origX, origY, origZ);
	}
}
