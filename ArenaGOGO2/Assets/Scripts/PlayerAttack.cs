using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public ArmToHitWith ArmSwingObject;
    private bool isSwinging = false;
    public float SwingCooldown = 0.2f;
    public float SwingTime = 0.2f;
    public playerMove playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleAttackPressed();
    }

    private void HandleAttackPressed()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isSwinging)
        {
            ArmSwingObject.DoSwing(playerMovement.FacingDirection);
            isSwinging = true;
            StartCoroutine(HandleGetArmBack());
        }
    }

    private IEnumerator HandleGetArmBack()
    {
        yield return new WaitForSeconds(SwingTime);
        ArmSwingObject.EndSwing();
        yield return new WaitForSeconds(SwingCooldown);
        isSwinging = false;
    }
}
