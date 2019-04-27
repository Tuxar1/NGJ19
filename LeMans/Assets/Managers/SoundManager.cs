using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SoundManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("SoundManager");
                    _instance = container.AddComponent<SoundManager>();
                }
            }

            return _instance;
        }
    }

    private AudioClip Shoot;
    private AudioClip Jump;

    //Awake is always called before any Start functions
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        Shoot = (AudioClip)Resources.Load("Sounds/Shoot");
        Jump = (AudioClip)Resources.Load("Sounds/Jump");
    }

    public void PlayJump()
    {
        AudioSource.PlayClipAtPoint(Jump, Camera.main.transform.position);
    }

    public void PlayShoot()
    {
        AudioSource.PlayClipAtPoint(Shoot, Camera.main.transform.position);

    }
}
