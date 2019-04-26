using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public ArmToHitWith ArmSwingObject;
    private bool isSwinging = false;
    public float SwingCooldown = 0.2f;
    public float SwingTime = 0.2f;
    public PlayerMove playerMovement;
	public PlayerInput input;
    public PlayerDeath playerDeath;

    // Start is called before the first frame update
    void Start()
    {
		input.AttackClicked += HandleAttackPressed;
        playerDeath.PlayerDeathActions += ForceResetAttack;
    }

    // Update is called once per frame
    void Update()
    {
        // HandleAttackPressed();
    }

    private void HandleAttackPressed(bool val)
    {
        if (!isSwinging)
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

    private void ForceResetAttack()
    {
        ArmSwingObject.EndSwing();
        isSwinging = false;
    }
}
