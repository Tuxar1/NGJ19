using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
	public Image Image;
	public Text Text;
	public int PlayerID;
    
	public void Setup(Color color, string score)
	{
		Image.color = color;
		Text.color = color;
		Text.text = score;
	}
}
