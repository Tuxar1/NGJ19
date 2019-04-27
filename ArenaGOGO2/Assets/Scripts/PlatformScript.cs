using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public bool hasBeenTouched = false;
    private SpriteRenderer spriteRenderer;
    private bool isPlatformWin = false;

    private bool isDeadly = false;
    public bool IsInDeadlySequence = false;
    private Color initColor;
    private Coroutine deadlyPlatformRoutine;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initColor = spriteRenderer.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameController.instance.AfterRestartAction += CheckWinCondition;
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

            if (isDeadly)
            {
                ActionUtilities.GetDeadlyPlatformAction()(col.gameObject.GetComponent<PlayerDeath>());
            }
        }
    }

    public void OnCollisionStay2D(Collision2D col)
    {
        if (isDeadly && col.gameObject.tag == "Player")
        {
            ActionUtilities.GetDeadlyPlatformAction()(col.gameObject.GetComponent<PlayerDeath>());
        }
    }

    public void CheckWinCondition()
    {
        if (WinConditions.TouchAllPlatforms == WinController.instance.winCondition)
        {
            isPlatformWin = true;
        }
    }

    public void InitiateDeadlyPlatform(float deadlyPlatformInterval)
    {
        if (IsInDeadlySequence || !GameController.GameHasStarted)
        {
            return;
        }
        IsInDeadlySequence = true;
        deadlyPlatformRoutine = StartCoroutine(DoDeadlyPlatform(deadlyPlatformInterval));
    }

    private IEnumerator DoDeadlyPlatform(float deadlyPlatformInterval)
    {
        spriteRenderer.color = Color.green;
        yield return new WaitForSeconds(deadlyPlatformInterval);
        spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(deadlyPlatformInterval);
        isDeadly = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(deadlyPlatformInterval);
        ResetDeadlyPlatform();
    }

    public void ResetDeadlyPlatform()
    {
        isDeadly = false;
        if (deadlyPlatformRoutine != null)
        {
            StopCoroutine(deadlyPlatformRoutine);
            spriteRenderer.color = initColor;
            deadlyPlatformRoutine = null;
        }
        IsInDeadlySequence = false;
    }
}
