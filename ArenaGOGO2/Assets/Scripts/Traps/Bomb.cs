using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	public float TimeToExplosion = 0.75f;
	public GameObject ExplosionEffectPrefab;

    // Update is called once per frame
    void Update()
    {
		TimeToExplosion -= Time.deltaTime;
		if (TimeToExplosion < 0)
		{
			Instantiate(ExplosionEffectPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
    }
}
