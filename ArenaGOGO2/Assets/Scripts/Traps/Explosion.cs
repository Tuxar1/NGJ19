using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	public float TimeBetweenSprites = 0.2f;
	public Sprite[] Sprites;
	public SpriteRenderer Renderer;
	public CircleCollider2D collider;

	private float timeSinceChange = 0;
	private int spriteID = 0;
	private float[] colliderSizes = new float[]
	{
		0.21f,0.3f,0.36f
	};

    // Update is called once per frame
    void Update()
    {
		timeSinceChange += Time.deltaTime;
		if (timeSinceChange > TimeBetweenSprites)
		{
			timeSinceChange = 0;
			spriteID++;
			if (spriteID == Sprites.Length)
			{
				Destroy(gameObject);
				return;
			}
			Renderer.sprite = Sprites[spriteID];
			collider.radius = colliderSizes[spriteID];
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			ActionUtilities.GetBombAction()(collision.gameObject.GetComponent<PlayerDeath>());
		}
	}
}
