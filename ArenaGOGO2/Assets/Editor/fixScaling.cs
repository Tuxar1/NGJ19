using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class fixScaling : EditorWindow
{
	[MenuItem("Scaling Fixer/Fixer")]
	public static void Init()
	{
		var gos = GameObject.FindObjectsOfType<PlatformScript>();
		var targetScale = new Vector3(1, 1, 1);
		foreach (var go in gos)
		{
			if (go.gameObject.transform.localScale == targetScale)
			{
				continue;
			}
			var spriteRendere = go.GetComponent<SpriteRenderer>();
			var coll = go.GetComponent<BoxCollider2D>();
			spriteRendere.drawMode = SpriteDrawMode.Tiled;
			var target = new Vector2(go.transform.localScale.x, go.transform.localScale.y);
			spriteRendere.size = target;
			coll.size = target;
			go.transform.localScale = targetScale;
		}
	}
}
