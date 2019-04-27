using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public bool hasBeenTouched = false;
    private SpriteRenderer spriteRenderer;
    private bool isPlatformWin = false;
    // Start is called before the first frame update
    void Start()
    {
        GameController.instance.AfterRestartAction += CheckWinCondition;
        spriteRenderer = GetComponent<SpriteRenderer>();
        CheckWinCondition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            hasBeenTouched = true;
            if (isPlatformWin)
            {
				spriteRenderer.color = col.gameObject.GetComponent<SpriteRenderer>().color;
                WinController.instance.CheckWinCondition(gameObject, col.gameObject);
            }
        }
    }

    public void CheckWinCondition()
    {
        // print(WinController.instance.winCondition);
        if (WinConditions.TouchAllPlatforms == WinController.instance.winCondition)
        {
            isPlatformWin = true;
        }
    }

	public void Reset()
	{
		hasBeenTouched = false;
		spriteRenderer.color = Color.white;
	}
}
