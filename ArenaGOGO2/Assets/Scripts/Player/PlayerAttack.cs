using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public ArmToHitWith ArmSwingObject;
    private bool isSwinging = false;
    public float SwingCooldown = 0.2f;
    public float SwingTime = 0.2f;
    public PlayerMovement playerMovement;
	public PlayerInput input;
    public PlayerDeath playerDeath;
    public Sprite Attack;
    public Sprite Standard;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
		input.AttackClicked += HandleAttackPressed;
        playerDeath.PlayerDeathActions += ForceResetAttack;
    }

    private void HandleAttackPressed(bool val)
    {
        if (!isSwinging)
        {
            print(Attack);
            spriteRenderer.sprite = Attack;
            ArmSwingObject.DoSwing(playerMovement.FacingDirection);
            isSwinging = true;
            StartCoroutine(HandleGetArmBack());
        }
    }

    private IEnumerator HandleGetArmBack()
    {
        yield return new WaitForSeconds(SwingTime);
        EndSwing();
        yield return new WaitForSeconds(SwingCooldown);
        isSwinging = false;
    }

    private void ForceResetAttack()
    {
        EndSwing();
        isSwinging = false;
    }

    private void EndSwing()
    {
        ArmSwingObject.EndSwing();
        spriteRenderer.sprite = Standard;
    }
}
