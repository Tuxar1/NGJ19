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
            // print(col);
            hasBeenTouched = true;
            if (isPlatformWin)
            {
                spriteRenderer.color = Color.green;
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
}
