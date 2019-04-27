using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPlayer : MonoBehaviour
{
	public int PlayerID;
	public GameObject Image;
	public GameObject Text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Selected(Color color)
	{
		Image.GetComponent<Image>().color = color;
		Image.SetActive(true);
		Text.SetActive(false);
	}

	public void DeSelected()
	{
		Image.SetActive(false);
		Text.SetActive(true);
	}
}
