using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    _instance = container.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    public bool Player1Active = false;
    public bool Player2Active = false;
    public bool Player3Active = false;
    public bool Player4Active = false;

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
    }

    //Update is called every frame.
    void Update()
    {

    }
}
