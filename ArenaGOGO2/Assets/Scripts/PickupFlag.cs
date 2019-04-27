using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFlag : MonoBehaviour
{
    public Vector3 flagLocalPositionOffset = new Vector3(0.27f, 0.5f, 0);
    private Rigidbody2D rigidBody;
    public bool isPickedUp;
    public Transform spawnPoint;
    public BoxCollider2D DontFallcollider;
    public GameObject EndZone;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        GameController.instance.RestartAction += ResetState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetState()
    {
        RemoveDropFlagOnDeath();
        gameObject.transform.SetParent(null);
		if (rigidBody != null)
		{
			rigidBody.constraints = RigidbodyConstraints2D.None;
			rigidBody.isKinematic = false;
		}
		isPickedUp = false;
        gameObject.transform.position = spawnPoint.position;
        EndZone.SetActive(false);
        gameObject.SetActive(false);
        DontFallcollider.enabled = true;
    }

    public void StartCTF()
    {
        gameObject.transform.position = spawnPoint.transform.position;
        gameObject.SetActive(true);
        EndZone.SetActive(true);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
		if (!GameController.GameHasStarted)
		{
			return;
		}
        if (col.gameObject.tag == "Player")
        {
            if (isPickedUp)
                RemoveDropFlagOnDeath();
            DontFallcollider.enabled = false;
            gameObject.transform.SetParent(col.gameObject.transform);
            col.gameObject.GetComponent<PlayerDeath>().PlayerDeathActions += DropFlagOnDeath;
            gameObject.transform.localPosition = flagLocalPositionOffset;
            rigidBody.isKinematic = true;
            isPickedUp = true;
        }
        else if (col.gameObject.tag == "EndZone")
        {
            WinController.instance.CheckWinCondition(gameObject, gameObject.transform.parent.gameObject);
        }
    }

    public void DropFlagOnDeath()
    {
        RemoveDropFlagOnDeath();
        gameObject.transform.SetParent(null);
        isPickedUp = false;
        DontFallcollider.enabled = true;
        rigidBody.isKinematic = false;
    }

    private void RemoveDropFlagOnDeath()
    {
		if (gameObject.transform?.parent != null)
		{
			gameObject.transform.parent.GetComponent<PlayerDeath>().PlayerDeathActions -= DropFlagOnDeath;
		}
    }
}
