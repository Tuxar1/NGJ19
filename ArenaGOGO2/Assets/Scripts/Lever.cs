﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject PurpleDoor;
    public GameObject PinkDoor;
    public Color standardColor;
    private int leverState;
    public SpriteRenderer spriteRenderer;
	public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        GameController.instance.RestartAction += ResetLeverState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLeverState()
    {
        leverState++;
        if (leverState > 2)
        {
            leverState = 1;
        }

        switch(leverState)
        {
            case 0:
                PurpleDoor.SetActive(true);
                PinkDoor.SetActive(true);
				spriteRenderer.sprite = sprites[0];
				//spriteRenderer.color = standardColor;
                break;
            case 1:
                PinkDoor.SetActive(false);
				spriteRenderer.sprite = sprites[1];
				//spriteRenderer.color = PinkDoor.GetComponent<SpriteRenderer>().color;
                PurpleDoor.SetActive(true);
                break;
            case 2:
                PurpleDoor.SetActive(false);
				spriteRenderer.sprite = sprites[2];
                // spriteRenderer.color = PurpleDoor.GetComponent<SpriteRenderer>().color;
                PinkDoor.SetActive(true);
                break;
        }
    }

    private void ResetLeverState()
    {
        leverState = -1;
        ChangeLeverState();
    }

}
